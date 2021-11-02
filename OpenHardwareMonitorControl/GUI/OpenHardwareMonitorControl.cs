/*
 
  This Source Code Form is subject to the terms of the Mozilla Public
  License, v. 2.0. If a copy of the MPL was not distributed with this
  file, You can obtain one at http://mozilla.org/MPL/2.0/.
 
  Copyright (C) 2009-2013 Michael Möller <mmoeller@openhardwaremonitor.org>
	Copyright (C) 2010 Paul Werelds <paul@werelds.net>
	Copyright (C) 2012 Prince Samuel <prince.samuel@gmail.com>

*/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Aga.Controls.Tree;
using Aga.Controls.Tree.NodeControls;
using OpenHardwareMonitor.Hardware;
using OpenHardwareMonitor.WMI;
using OpenHardwareMonitor.Utilities;

namespace OpenHardwareMonitor.GUI
{
    public partial class OpenHardwareMonitorControl : UserControl
    {
        public delegate void SensorValuesUpdatedEventHandler();
        public event SensorValuesUpdatedEventHandler SensorValuesUpdated = null;

        public delegate void SensorPathRequestedEventHandler(string path);
        public event SensorPathRequestedEventHandler SensorPathRequested = null;

        public event Hardware.SensorEventHandler SensorAdded = null;

        public Dictionary<string, ISensor> SensorList = null;

        public Form ParentRootForm = null;

    public PersistentSettings settings;
    private UnitManager unitManager;
    private Computer computer;
    public Node rootNode;
    private TreeModel treeModel;
    private IDictionary<ISensor, Color> sensorPlotColors = 
      new Dictionary<ISensor, Color>();
    private Color[] plotColorPalette;
    private SystemTray systemTray;    
    private StartupManager startupManager = new StartupManager();
    private UpdateVisitor updateVisitor = new UpdateVisitor();
    private SensorGadget gadget;
    private Form plotForm;
    private PlotPanel plotPanel;

    private UserOption showHiddenSensors;
    private UserOption showPlot;
    private UserOption showValue;
    private UserOption showMin;
    private UserOption showMax;
    private UserOption startMinimized;
    private UserOption minimizeToTray;
    private UserOption minimizeOnClose;
    private UserOption autoStart;

    private UserOption readMainboardSensors;
    private UserOption readCpuSensors;
    private UserOption readRamSensors;
    private UserOption readGpuSensors;
    private UserOption readFanControllersSensors;
    private UserOption readHddSensors;

    private UserOption showGadget;
    private UserRadioGroup plotLocation;
    private WmiProvider wmiProvider;

    private UserOption runWebServer;
    public HttpServer server;

    private UserOption logSensors;
    private UserRadioGroup loggingInterval;
    private Logger logger;

        public System.Windows.Forms.Timer timerMonitorUpdate;

        private bool selectionDragging = false;

        public bool ShowMemoryStatus = true;
        public bool ShowHddStatus = true;

        public ToolStripMenuItem GetMenuItems()
        {
            ToolStripMenuItem tsmi = new ToolStripMenuItem("O.H.M.");
            tsmi.TextImageRelation = TextImageRelation.ImageAboveText;
            tsmi.Image = imageList.Images["icon.ico"]; //).ToBitmap();
            tsmi.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tsmi.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenuItem,
            this.viewMenuItem,
            this.optionsMenuItem,
            this.helpMenuItem});
            return tsmi;
        }

        public void SetupParentRootForm(Form parentRootForm)
        {
            this.ParentRootForm = parentRootForm;
            if (ParentRootForm == null)
                return;

            this.ParentRootForm.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.ParentRootForm.Load += new System.EventHandler(this.ParentRootForm_Load);
            this.ParentRootForm.Resize += new System.EventHandler(this.ParentRootForm_MoveOrResize);
            this.ParentRootForm.Move += new System.EventHandler(this.ParentRootForm_MoveOrResize);
            this.ParentRootForm.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ParentRootForm_FormClosed);

            // Create a handle, otherwise calling Close() does not fire FormClosed     
           // IntPtr handle = this.ParentRootForm.Handle;

            //this.ParentForm.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            //this.ContextMenuStrip = this.mainMenu;
        }

        public OpenHardwareMonitorControl() {
            InitializeComponent();

            SensorList = new Dictionary<string, ISensor>();

            this.settings = new PersistentSettings();

            this.settings.Load(Path.ChangeExtension(Application.ExecutablePath, ".config"));

            // 
            // timer
            // 

            this.timerMonitorUpdate = new System.Windows.Forms.Timer();
            this.timerMonitorUpdate.Interval = 1000;
            this.timerMonitorUpdate.Tick += new System.EventHandler(this.timerMonitorUpdate_Tick);
        }
        private int delayCount = 0;

        private void timerMonitorUpdate_Tick(object sender, EventArgs e)
        {
            UpdateValues();
            if (SensorValuesUpdated != null)
                SensorValuesUpdated();
        }

        public void UpdateValues()
        {
            computer.Accept(updateVisitor);
            treeView.Invalidate();
            plotPanel.InvalidatePlot();
            systemTray.Redraw();

            if (gadget != null)
                gadget.Redraw();

            if (wmiProvider != null)
                wmiProvider.Update();

            if (logSensors != null && logSensors.Value && delayCount >= 4)
            {
                if (delayCount < 4) delayCount++;
                else
                {
                    delayCount = 0;
                    logger.Log();
                }
            }
        }

        public float GetSensorValue(string name)
        {
            if (!SensorList.ContainsKey(name)) return -1.0f;

            return SensorList[name].Value.GetValueOrDefault(-1.0f);
        }

        public void SetSensorControlSoftwareValue(string name, float newValue)
        {
            if (!SensorList.ContainsKey(name)) return;

            IControl control = SensorList[name].Control;

            if (control == null) return;

            if (newValue < control.MinSoftwareValue)
                newValue = control.MinSoftwareValue;
            else if (newValue > control.MaxSoftwareValue)
                newValue = control.MaxSoftwareValue;

            control.SetSoftware(newValue);
        }

        public void SetSensorControlDefaultValue(string name)
        {
            if (!SensorList.ContainsKey(name)) return;

            IControl control = SensorList[name].Control;

            if (control == null) return;

            control.SetDefault();
        }


        private void InitializePlotForm()
        {
            plotForm = new Form();
            plotForm.FormBorderStyle = FormBorderStyle.SizableToolWindow;
            plotForm.ShowInTaskbar = false;
            plotForm.StartPosition = FormStartPosition.Manual;

           if (this.ParentRootForm != null)
                this.ParentRootForm.AddOwnedForm(plotForm);

            plotForm.Bounds = new Rectangle {
                X = settings.GetValue("plotForm.Location.X", -100000),
                Y = settings.GetValue("plotForm.Location.Y", 100),
                Width = settings.GetValue("plotForm.Width", 600),
                Height = settings.GetValue("plotForm.Height", 400)
            };

            showPlot = new UserOption("plotMenuItem", false, plotMenuItem, settings);

            plotLocation = new UserRadioGroup("plotLocation", 0, new[] { plotWindowMenuItem, plotBottomMenuItem, plotRightMenuItem }, settings);

            showPlot.Changed += delegate(object sender, EventArgs e)
            {
                if (plotLocation.Value == 0)
                {
                    if (showPlot.Value && this.Visible)
                        plotForm.Show();
                    else
                        plotForm.Hide();
                }
                else
                {
                    splitContainer.Panel2Collapsed = !showPlot.Value;
                }
                treeView.Invalidate();
            };

            plotLocation.Changed += delegate(object sender, EventArgs e)
            {
                switch (plotLocation.Value)
                {
                    case 0:
                    splitContainer.Panel2.Controls.Clear();
                    splitContainer.Panel2Collapsed = true;
                    plotForm.Controls.Add(plotPanel);
                    if (showPlot.Value && this.Visible)
                        plotForm.Show();
                    break;
                    case 1:
                    plotForm.Controls.Clear();
                    plotForm.Hide();
                    splitContainer.Orientation = Orientation.Horizontal;
                    splitContainer.Panel2.Controls.Add(plotPanel);
                    splitContainer.Panel2Collapsed = !showPlot.Value;
                    break;
                    case 2:
                    plotForm.Controls.Clear();
                    plotForm.Hide();
                    splitContainer.Orientation = Orientation.Vertical;
                    splitContainer.Panel2.Controls.Add(plotPanel);
                    splitContainer.Panel2Collapsed = !showPlot.Value;
                    break;
                }
            };

            plotForm.FormClosing += delegate(object sender, FormClosingEventArgs e)
            {
                if (e.CloseReason == CloseReason.UserClosing)
                {
                    // just switch off the plotting when the user closes the form
                    if (plotLocation.Value == 0)
                        showPlot.Value = false;
                    
                    e.Cancel = true;
                }
            };

            EventHandler moveOrResizePlotForm = delegate(object sender, EventArgs e)
            {
                if (plotForm.WindowState != FormWindowState.Minimized)
                {
                    settings.SetValue("plotForm.Location.X", plotForm.Bounds.X);
                    settings.SetValue("plotForm.Location.Y", plotForm.Bounds.Y);
                    settings.SetValue("plotForm.Width", plotForm.Bounds.Width);
                    settings.SetValue("plotForm.Height", plotForm.Bounds.Height);
                }
            };

            plotForm.Move += moveOrResizePlotForm;
            plotForm.Resize += moveOrResizePlotForm;

            plotForm.VisibleChanged += delegate(object sender, EventArgs e)
            {
                Rectangle bounds = new Rectangle(plotForm.Location, plotForm.Size);
                Screen screen = Screen.FromRectangle(bounds);
                Rectangle intersection = Rectangle.Intersect(screen.WorkingArea, bounds);

                if (intersection.Width < Math.Min(16, bounds.Width) || intersection.Height < Math.Min(16, bounds.Height))
                {
                    plotForm.Location = new Point(
                    screen.WorkingArea.Width / 2 - bounds.Width / 2,
                    screen.WorkingArea.Height / 2 - bounds.Height / 2);
                }
            };

            this.VisibleChanged += delegate(object sender, EventArgs e) {
                if (this.Visible && showPlot.Value && plotLocation.Value == 0)
                    plotForm.Show();
                else
                    plotForm.Hide();
            };
        }

        private void InsertSorted(Collection<Node> nodes, HardwareNode node)
        {
            int i = 0;
                while ((i < nodes.Count) &&
                        (nodes[i] is HardwareNode) &&
                        (((HardwareNode)nodes[i]).Hardware.HardwareType < node.Hardware.HardwareType))
                { i++; }

            
                nodes.Insert(i, node);
        }

        private void HardwareSensorAdded(ISensor sensor)
        {
            string path = sensor.Identifier.ToString();
            SensorList.Add(path, sensor);

            if (SensorAdded != null)
                SensorAdded(sensor);
            
        }
    
        private void SubHardwareAdded(IHardware hardware, Node node)
        {
            //hardware.SensorAdded += HardwareSensorAdded;

            HardwareNode hardwareNode = new HardwareNode(hardware, settings, unitManager, HardwareSensorAdded);

            hardwareNode.PlotSelectionChanged += PlotSelectionChanged;
            hardwareNode.MonitoredSelectionChanged += SensorMonitoredSelectionChanged;
            // hardwareNode.SensorAdded += HardwareSensorAdded;

            InsertSorted(node.Nodes, hardwareNode);

            foreach (IHardware subHardware in hardware.SubHardware)
                SubHardwareAdded(subHardware, hardwareNode);  
        }


        private void HardwareAdded(IHardware hardware) {

              /* if (ShowMemoryStatus && (hardware.HardwareType == HardwareType.RAM))
                    return;
                if (ShowHddStatus && (hardware.HardwareType == HardwareType.HDD))
                    return;
*/
                SubHardwareAdded(hardware, rootNode);

                PlotSelectionChanged(this, null);
        }

        private void HardwareRemoved(IHardware hardware) {
          List<HardwareNode> nodesToRemove = new List<HardwareNode>();
          foreach (Node node in rootNode.Nodes) {
            HardwareNode hardwareNode = node as HardwareNode;
            if (hardwareNode != null && hardwareNode.Hardware == hardware)
              nodesToRemove.Add(hardwareNode);
          }
          foreach (HardwareNode hardwareNode in nodesToRemove) {
            rootNode.Nodes.Remove(hardwareNode);
            hardwareNode.PlotSelectionChanged -= PlotSelectionChanged;
                    hardwareNode.MonitoredSelectionChanged -= SensorMonitoredSelectionChanged;
                }
          PlotSelectionChanged(this, null);
        }

    private void nodeTextBoxText_DrawText(object sender, DrawEventArgs e) {       
      Node node = e.Node.Tag as Node;
      if (node != null) {
        Color color;
        if (node.IsVisible) {
          SensorNode sensorNode = node as SensorNode;
          if (plotMenuItem.Checked && sensorNode != null &&
            sensorPlotColors.TryGetValue(sensorNode.Sensor, out color))
            e.TextColor = color;
        } else {
          e.TextColor = Color.DarkGray;
        }
      }
    }

        private void SensorMonitoredSelectionChanged(SensorNode sensorNode,  bool settingAdded)
        {
            MessageBox.Show("SensorMonitor id: " + new Identifier(sensorNode.Sensor.Identifier, "monitored", "code").ToString() + "\n" + "settingAdded: " + settingAdded.ToString());
        }

    private void PlotSelectionChanged(object sender, EventArgs e) {
      List<ISensor> selected = new List<ISensor>();
      IDictionary<ISensor, Color> colors = new Dictionary<ISensor, Color>();
      int colorIndex = 0;
      foreach (TreeNodeAdv node in treeView.AllNodes) {
        SensorNode sensorNode = node.Tag as SensorNode;
        if (sensorNode != null) {
          if (sensorNode.Plot) {
            colors.Add(sensorNode.Sensor,
              plotColorPalette[colorIndex % plotColorPalette.Length]);
            selected.Add(sensorNode.Sensor);
          }
          colorIndex++;
        }
      }

      // if a sensor is assigned a color that's already being used by another 
      // sensor, try to assign it a new color. This is done only after the 
      // previous loop sets an unchanging default color for all sensors, so that 
      // colors jump around as little as possible as sensors get added/removed 
      // from the plot
      var usedColors = new List<Color>();
      foreach (var curSelectedSensor in selected) {
        var curColor = colors[curSelectedSensor];
        if (usedColors.Contains(curColor)) {
          foreach (var potentialNewColor in plotColorPalette) {
            if (!colors.Values.Contains(potentialNewColor)) {
              colors[curSelectedSensor] = potentialNewColor;
              usedColors.Add(potentialNewColor);
              break;
            }
          }
        } else {
          usedColors.Add(curColor);
        }
      }  

      sensorPlotColors = colors;
      plotPanel.SetSensors(selected, colors);
    }

    private void nodeTextBoxText_EditorShowing(object sender,
      CancelEventArgs e) 
    {
      e.Cancel = !(treeView.CurrentNode != null &&
        (treeView.CurrentNode.Tag is SensorNode || 
         treeView.CurrentNode.Tag is HardwareNode));
    }

    private void nodeCheckBox_IsVisibleValueNeeded(object sender, 
      NodeControlValueEventArgs e) {
      SensorNode node = e.Node.Tag as SensorNode;
            
      e.Value = (node != null) && plotMenuItem.Checked;
    }

    private void exitClick(object sender, EventArgs e) {
            if (this.ParentRootForm != null)
            this.ParentRootForm.Close();
    }

        

    private void SaveConfiguration() {
      plotPanel.SetCurrentSettings();
      foreach (TreeColumn column in treeView.Columns)
        settings.SetValue("treeView.Columns." + column.Header + ".Width",
          column.Width);

      this.settings.SetValue("listenerPort", server.ListenerPort);

      string fileName = Path.ChangeExtension(
          System.Windows.Forms.Application.ExecutablePath, ".config");
      try {
        settings.Save(fileName);
      } catch (UnauthorizedAccessException) {
        MessageBox.Show("Access to the path '" + fileName + "' is denied. " +
          "The current settings could not be saved.",
          "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      } catch (IOException) {
        MessageBox.Show("The path '" + fileName + "' is not writeable. " +
          "The current settings could not be saved.",
          "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void ParentRootForm_Load(object sender, EventArgs e) {
      Rectangle newBounds = new Rectangle {
        X = settings.GetValue("mainForm.Location.X", Location.X),
        Y = settings.GetValue("mainForm.Location.Y", Location.Y),
        Width = settings.GetValue("mainForm.Width", 470),
        Height = settings.GetValue("mainForm.Height", 640)
      };

      Rectangle fullWorkingArea = new Rectangle(int.MaxValue, int.MaxValue,
        int.MinValue, int.MinValue);

      foreach (Screen screen in Screen.AllScreens)
        fullWorkingArea = Rectangle.Union(fullWorkingArea, screen.Bounds);

      Rectangle intersection = Rectangle.Intersect(fullWorkingArea, newBounds);
      if (intersection.Width < 20 || intersection.Height < 20 ||
        !settings.Contains("mainForm.Location.X")
      ) {
        newBounds.X = (Screen.PrimaryScreen.WorkingArea.Width / 2) -
                      (newBounds.Width/2);

        newBounds.Y = (Screen.PrimaryScreen.WorkingArea.Height / 2) -
                      (newBounds.Height / 2);
      }

      this.Bounds = newBounds;

            
    }
    
    private void ParentRootForm_FormClosed(object sender, FormClosedEventArgs e) {
      Visible = false;      
      systemTray.IsMainIconEnabled = false;
      timerMonitorUpdate.Enabled = false;            
      computer.Close();
      SaveConfiguration();
      if (runWebServer.Value)
          server.Quit();
      systemTray.Dispose();
    }

    private void aboutMenuItem_Click(object sender, EventArgs e) {
      new AboutBox().ShowDialog();
    }

    private void saveReportMenuItem_Click(object sender, EventArgs e) {
      string report = computer.GetReport();
      if (saveFileDialog.ShowDialog() == DialogResult.OK) {
        using (TextWriter w = new StreamWriter(saveFileDialog.FileName)) {
          w.Write(report);
        }
      }
    }

    private void SysTrayHideShow() {
      Visible = !Visible;
      if (Visible && (this.ParentRootForm != null))
                this.ParentRootForm.Activate();    
    }

    

    private void hideShowClick(object sender, EventArgs e) {
      SysTrayHideShow();
    }

    private void ShowParameterForm(ISensor sensor) {
      ParameterForm form = new ParameterForm();
      form.Parameters = sensor.Parameters;
      form.captionLabel.Text = sensor.Name;
      form.ShowDialog();
    }

    private void treeView_NodeMouseDoubleClick(object sender, 
      TreeNodeAdvMouseEventArgs e) {
      SensorNode node = e.Node.Tag as SensorNode;
      if (node != null && node.Sensor != null && 
        node.Sensor.Parameters.Length > 0) {
        ShowParameterForm(node.Sensor);
      }
    }

    

    private void ParentRootForm_MoveOrResize(object sender, EventArgs e) {
      if (this.ParentRootForm.WindowState != FormWindowState.Minimized) {
        settings.SetValue("mainForm.Location.X", Bounds.X);
        settings.SetValue("mainForm.Location.Y", Bounds.Y);
        settings.SetValue("mainForm.Width", Bounds.Width);
        settings.SetValue("mainForm.Height", Bounds.Height);
      }
    }

    private void resetClick(object sender, EventArgs e) {
      // disable the fallback MainIcon during reset, otherwise icon visibility
      // might be lost 
      systemTray.IsMainIconEnabled = false;
      computer.Close();
      computer.Open();
      // restore the MainIcon setting
      systemTray.IsMainIconEnabled = minimizeToTray.Value;
    }

    private void treeView_MouseMove(object sender, MouseEventArgs e) {
      selectionDragging = selectionDragging &
        (e.Button & (MouseButtons.Left | MouseButtons.Right)) > 0; 

      if (selectionDragging)
        treeView.SelectedNode = treeView.GetNodeAt(e.Location);     
    }

    private void treeView_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button != MouseButtons.Left)
                return;
      selectionDragging = true;
    }

    private void treeView_MouseUp(object sender, MouseEventArgs e) {
            if (e.Button != MouseButtons.Left)
                return;
            selectionDragging = false;
    }

        private void celsiusMenuItem_Click(object sender, EventArgs e)
        {
            celsiusMenuItem.Checked = true;
            fahrenheitMenuItem.Checked = false;
            unitManager.TemperatureUnit = TemperatureUnit.Celsius;
        }

        private void fahrenheitMenuItem_Click(object sender, EventArgs e)
        {
            celsiusMenuItem.Checked = false;
            fahrenheitMenuItem.Checked = true;
            unitManager.TemperatureUnit = TemperatureUnit.Fahrenheit;
        }

        private void sumbitReportMenuItem_Click(object sender, EventArgs e)
        {
            ReportForm form = new ReportForm();
            form.Report = computer.GetReport();
            form.ShowDialog();
        }

        private void resetMinMaxMenuItem_Click(object sender, EventArgs e)
        {
            computer.Accept(new SensorVisitor(delegate (ISensor sensor) {
                sensor.ResetMin();
                sensor.ResetMax();
            }));
        }
        private void serverPortMenuItem_Click(object sender, EventArgs e)
        {
            new PortForm(this).ShowDialog();
        }

        public HttpServer Server {
      get { return server; }
    }

        private void gadgetMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void hiddenMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void OpenHardwareMonitorControl_Load(object s, EventArgs ea)
        {
            this.unitManager = new UnitManager(settings);

            // make sure the buffers used for double buffering are not disposed 
            // after each draw call
            BufferedGraphicsManager.Current.MaximumBuffer =
              Screen.PrimaryScreen.Bounds.Size;

            // set the DockStyle here, to avoid conflicts with the MainMenu
            this.splitContainer.Dock = DockStyle.Fill;

            this.Font = SystemFonts.MessageBoxFont;
            treeView.Font = SystemFonts.MessageBoxFont;

            plotPanel = new PlotPanel(settings, unitManager);
            plotPanel.Font = SystemFonts.MessageBoxFont;
            plotPanel.Dock = DockStyle.Fill;

            nodeCheckBox.IsVisibleValueNeeded += nodeCheckBox_IsVisibleValueNeeded;
            nodeTextBoxText.DrawText += nodeTextBoxText_DrawText;
            nodeTextBoxValue.DrawText += nodeTextBoxText_DrawText;
            nodeTextBoxMin.DrawText += nodeTextBoxText_DrawText;
            nodeTextBoxMax.DrawText += nodeTextBoxText_DrawText;
            nodeTextBoxText.EditorShowing += nodeTextBoxText_EditorShowing;

            foreach (TreeColumn column in treeView.Columns)
                column.Width = Math.Max(20, Math.Min(400,
                  settings.GetValue("treeView.Columns." + column.Header + ".Width",
                  column.Width)));

            treeModel = new TreeModel();
            rootNode = new Node(System.Environment.MachineName);
            rootNode.Image = Utilities.EmbeddedResources.GetImage("computer.png");

            treeModel.Nodes.Add(rootNode);
            treeView.Model = treeModel;

            this.computer = new Computer(settings);

            systemTray = new SystemTray(computer, settings, unitManager);
            systemTray.HideShowCommand += hideShowClick;
            systemTray.ExitCommand += exitClick;

            PlatformID pId = Environment.OSVersion.Platform;

            if ((pId == PlatformID.Unix) || (pId == PlatformID.MacOSX))
            { // Unix
                treeView.RowHeight = Math.Max(treeView.RowHeight, 18);
                splitContainer.BorderStyle = BorderStyle.None;
                splitContainer.Border3DStyle = Border3DStyle.Adjust;
                splitContainer.SplitterWidth = 4;
                treeView.BorderStyle = BorderStyle.Fixed3D;
                plotPanel.BorderStyle = BorderStyle.Fixed3D;
                /*gadgetMenuItem.Visible = false;
                minCloseMenuItem.Visible = false;
                minTrayMenuItem.Visible = false;
                startMinMenuItem.Visible = false;*/
            }
            else
            { // Windows
                
                treeView.RowHeight = Math.Max(treeView.Font.Height + 1, 18);

                gadget = new SensorGadget(computer, settings, unitManager);
                
                gadget.HideShowCommand += hideShowClick;

                wmiProvider = new WmiProvider(computer);
            }

            logger = new Logger(computer);

            plotColorPalette = new Color[13];
            plotColorPalette[0] = Color.Blue;
            plotColorPalette[1] = Color.OrangeRed;
            plotColorPalette[2] = Color.Green;
            plotColorPalette[3] = Color.LightSeaGreen;
            plotColorPalette[4] = Color.Goldenrod;
            plotColorPalette[5] = Color.DarkViolet;
            plotColorPalette[6] = Color.YellowGreen;
            plotColorPalette[7] = Color.SaddleBrown;
            plotColorPalette[8] = Color.RoyalBlue;
            plotColorPalette[9] = Color.DeepPink;
            plotColorPalette[10] = Color.MediumSeaGreen;
            plotColorPalette[11] = Color.Olive;
            plotColorPalette[12] = Color.Firebrick;

            computer.HardwareAdded += new HardwareEventHandler(HardwareAdded);
            computer.HardwareRemoved += new HardwareEventHandler(HardwareRemoved);

            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(Computer_Open_Threaded));
            t.Start();
           // computer.Open();

           // timerMonitorUpdate.Enabled = true;

            server = new HttpServer(rootNode, this.settings.GetValue("listenerPort", 8085));
            if (server.PlatformNotSupported)
            {
                webMenuItemSeparator.Visible = false;
                webMenuItem.Visible = false;
            }

            Init_UserOptions();


            

            

            InitializePlotForm();

            startupMenuItem.Visible = startupManager.IsAvailable;

            if (this.ParentRootForm != null)
            {
                if (startMinMenuItem.Checked)
                {
                    if (!minTrayMenuItem.Checked)
                    {

                        this.ParentRootForm.WindowState = FormWindowState.Minimized;
                        this.ParentRootForm.Show();

                    }
                }
                else {
                    this.ParentRootForm.Show();
                }
            }



            // Make sure the settings are saved when the user logs off
            Microsoft.Win32.SystemEvents.SessionEnded += delegate {
                computer.Close();
                SaveConfiguration();
                if (runWebServer.Value)
                    server.Quit();
            };

            treeView.ContextMenuStrip = treeContextMenu;
        }

        private void Computer_Open_Threaded()
        {
            computer.Open();
        }

        private void Init_UserOptions()
        {
            showHiddenSensors = new UserOption("hiddenMenuItem", false,
              hiddenMenuItem, settings);
            showHiddenSensors.Changed += delegate (object sender, EventArgs e) {
                treeModel.ForceVisible = showHiddenSensors.Value;
            };

            showValue = new UserOption("valueMenuItem", true, valueMenuItem,
              settings);
            showValue.Changed += delegate (object sender, EventArgs e) {
                treeView.Columns[1].IsVisible = showValue.Value;
            };

            showMin = new UserOption("minMenuItem", false, minMenuItem, settings);
            showMin.Changed += delegate (object sender, EventArgs e) {
                treeView.Columns[2].IsVisible = showMin.Value;
            };

            showMax = new UserOption("maxMenuItem", true, maxMenuItem, settings);
            showMax.Changed += delegate (object sender, EventArgs e) {
                treeView.Columns[3].IsVisible = showMax.Value;
            };

            startMinimized = new UserOption("startMinMenuItem", false,
              startMinMenuItem, settings);

            minimizeToTray = new UserOption("minTrayMenuItem", true,
              minTrayMenuItem, settings);
            minimizeToTray.Changed += delegate (object sender, EventArgs e) {
                systemTray.IsMainIconEnabled = minimizeToTray.Value;
            };

            minimizeOnClose = new UserOption("minCloseMenuItem", false,
              minCloseMenuItem, settings);

            autoStart = new UserOption(null, startupManager.Startup,
              startupMenuItem, settings);
            autoStart.Changed += delegate (object sender, EventArgs e) {
                try
                {
                    startupManager.Startup = autoStart.Value;
                }
                catch (InvalidOperationException)
                {
                    MessageBox.Show("Updating the auto-startup option failed.", "Error",
                      MessageBoxButtons.OK, MessageBoxIcon.Error);
                    autoStart.Value = startupManager.Startup;
                }
            };

            readMainboardSensors = new UserOption("mainboardMenuItem", true,
              mainboardMenuItem, settings);
            readMainboardSensors.Changed += delegate (object sender, EventArgs e) {
                computer.MainboardEnabled = readMainboardSensors.Value;
            };

            readCpuSensors = new UserOption("cpuMenuItem", true,
              cpuMenuItem, settings);
            readCpuSensors.Changed += delegate (object sender, EventArgs e) {
                computer.CPUEnabled = readCpuSensors.Value;
            };

            readRamSensors = new UserOption("ramMenuItem", true,
              ramMenuItem, settings);
            readRamSensors.Changed += delegate (object sender, EventArgs e) {
                computer.RAMEnabled = readRamSensors.Value;
            };

            readGpuSensors = new UserOption("gpuMenuItem", true,
              gpuMenuItem, settings);
            readGpuSensors.Changed += delegate (object sender, EventArgs e) {
                computer.GPUEnabled = readGpuSensors.Value;
            };

            readFanControllersSensors = new UserOption("fanControllerMenuItem", true,
              fanControllerMenuItem, settings);
            readFanControllersSensors.Changed += delegate (object sender, EventArgs e) {
                computer.FanControllerEnabled = readFanControllersSensors.Value;
            };

            readHddSensors = new UserOption("hddMenuItem", true, hddMenuItem,
              settings);
            readHddSensors.Changed += delegate (object sender, EventArgs e) {
                computer.HDDEnabled = readHddSensors.Value;
            };

            showGadget = new UserOption("gadgetMenuItem", false, gadgetMenuItem,
              settings);
            showGadget.Changed += delegate (object sender, EventArgs e) {
                if (gadget != null)
                    gadget.Visible = showGadget.Value;
            };

            celsiusMenuItem.Checked =
              unitManager.TemperatureUnit == TemperatureUnit.Celsius;
            fahrenheitMenuItem.Checked = !celsiusMenuItem.Checked;

            runWebServer = new UserOption("runWebServerMenuItem", false,
              runWebServerMenuItem, settings);
            runWebServer.Changed += delegate (object sender, EventArgs e) {
                if (runWebServer.Value)
                    server.StartHTTPListener();
                else
                    server.StopHTTPListener();
            };

            logSensors = new UserOption("logSensorsMenuItem", false, logSensorsMenuItem,
              settings);

            loggingInterval = new UserRadioGroup("loggingInterval", 0,
                new[] { log1sMenuItem, log2sMenuItem, log5sMenuItem, log10sMenuItem,
                        log30sMenuItem, log1minMenuItem, log2minMenuItem, log5minMenuItem,
                        log10minMenuItem, log30minMenuItem, log1hMenuItem, log2hMenuItem,
                        log6hMenuItem}, settings);
            loggingInterval.Changed += (sender, e) => {
                switch (loggingInterval.Value)
                {
                    case 0: logger.LoggingInterval = new TimeSpan(0, 0, 1); break;
                    case 1: logger.LoggingInterval = new TimeSpan(0, 0, 2); break;
                    case 2: logger.LoggingInterval = new TimeSpan(0, 0, 5); break;
                    case 3: logger.LoggingInterval = new TimeSpan(0, 0, 10); break;
                    case 4: logger.LoggingInterval = new TimeSpan(0, 0, 30); break;
                    case 5: logger.LoggingInterval = new TimeSpan(0, 1, 0); break;
                    case 6: logger.LoggingInterval = new TimeSpan(0, 2, 0); break;
                    case 7: logger.LoggingInterval = new TimeSpan(0, 5, 0); break;
                    case 8: logger.LoggingInterval = new TimeSpan(0, 10, 0); break;
                    case 9: logger.LoggingInterval = new TimeSpan(0, 30, 0); break;
                    case 10: logger.LoggingInterval = new TimeSpan(1, 0, 0); break;
                    case 11: logger.LoggingInterval = new TimeSpan(2, 0, 0); break;
                    case 12: logger.LoggingInterval = new TimeSpan(6, 0, 0); break;
                }
            };
        }

        private void treeView_SelectionChanged(object sender, EventArgs e)
        {
            if (treeView.SelectedNode == null)
                return;

            SensorNode node = treeView.SelectedNode.Tag as SensorNode;
            if (node != null)
            {
                if (node.Sensor != null)
                    SensorNodeSelected(node);
                node = null;
            }
            else
            {
                HardwareNode hardwareNode = treeView.SelectedNode.Tag as HardwareNode;
                if (hardwareNode == null) return;

                if (hardwareNode.Hardware != null)
                    HardwareNodeSelected();
                hardwareNode = null;
            }
        }

        private void SensorNodeSelected(SensorNode node)
        {
            treeContextMenu.Items.Clear();

            ToolStripMenuItem item = new ToolStripMenuItem("Get current node path");
            item.Tag = node;
            //item.Checked = node.Monitored;
            item.Click += delegate (object obj, EventArgs args)
            {
                SensorNode sn = (obj as ToolStripMenuItem).Tag as SensorNode;
                string idPath = sn.Sensor.Identifier.ToString();

                if (SensorPathRequested != null)
                    SensorPathRequested(idPath);
                else
                    Clipboard.SetText(idPath);
            };
            treeContextMenu.Items.Add(item);

            if (node.Sensor.Parameters.Length > 0)
            {
                item = new ToolStripMenuItem("Parameters...");
                item.Tag = node;
                item.Click += delegate (object obj, EventArgs args)
                {
                    SensorNode sn = (obj as ToolStripMenuItem).Tag as SensorNode;
                    ShowParameterForm(sn.Sensor);
                };
                treeContextMenu.Items.Add(item);
            }
            if (nodeTextBoxText.EditEnabled)
            {
                item = new ToolStripMenuItem("Rename");
                item.Tag = node;
                item.Click += delegate (object obj, EventArgs args)
                {
                    nodeTextBoxText.BeginEdit();
                };
                treeContextMenu.Items.Add(item);
            }

            if (node.IsVisible)
            {
                item = new ToolStripMenuItem("Hide");
                item.Tag = node;
                item.Click += delegate (object obj, EventArgs args)
                {
                    SensorNode sn = (obj as ToolStripMenuItem).Tag as SensorNode;
                    sn.IsVisible = false;
                };
                treeContextMenu.Items.Add(item);
            }
            else {
                item = new ToolStripMenuItem("Unhide");
                item.Tag = node;
                item.Click += delegate (object obj, EventArgs args)
                {
                    SensorNode sn = (obj as ToolStripMenuItem).Tag as SensorNode;
                    sn.IsVisible = true;
                };
                treeContextMenu.Items.Add(item);
            }
            treeContextMenu.Items.Add(new ToolStripSeparator());
            {
                item = new ToolStripMenuItem("Show in Tray");
                item.Tag = node;
                item.Checked = systemTray.Contains(node.Sensor);
                item.Click += delegate (object obj, EventArgs args)
                {
                    SensorNode sn = (obj as ToolStripMenuItem).Tag as SensorNode;
                    if (item.Checked)
                        systemTray.Remove(sn.Sensor);
                    else
                        systemTray.Add(sn.Sensor, true);
                };
                treeContextMenu.Items.Add(item);
            }
            if (gadget != null)
            {
                item = new ToolStripMenuItem("Show in Gadget");
                item.Tag = node;
                item.Checked = gadget.Contains(node.Sensor);
                item.Click += delegate (object obj, EventArgs args)
                {
                    SensorNode sn = (obj as ToolStripMenuItem).Tag as SensorNode;
                    if (item.Checked)
                    {
                        gadget.Remove(sn.Sensor);
                    }
                    else {
                        gadget.Add(sn.Sensor);
                    }
                };
                treeContextMenu.Items.Add(item);
            }
            if (node.Sensor.Control != null)
            {
                treeContextMenu.Items.Add(new ToolStripSeparator());
                IControl control = node.Sensor.Control;
                ToolStripMenuItem controlItem = new ToolStripMenuItem("Control");
                ToolStripMenuItem defaultItem = new ToolStripMenuItem("Default");
                
                
                defaultItem.Checked = control.ControlMode == ControlMode.Default;
                controlItem.DropDownItems.Add(defaultItem);
                defaultItem.Click += delegate (object obj, EventArgs args)
                {
                    control.SetDefault();
                };
                ToolStripMenuItem manualItem = new ToolStripMenuItem("Manual");
                controlItem.DropDownItems.Add(manualItem);
                manualItem.Checked = control.ControlMode == ControlMode.Software;

                ToolStripTextBox manualInput = new ToolStripTextBox("txtManualInput");
                int manualInputValue = Convert.ToInt32(control.SoftwareValue);
                manualInput.Text = manualInputValue.ToString();
                manualInput.Tag = manualInputValue;
                manualInput.KeyUp += delegate (object s, KeyEventArgs kea)
                {
                    ToolStripTextBox manualInputRef = s as ToolStripTextBox;
                     
                    if (kea.KeyCode == Keys.Enter)
                    {
                        int newValue = 0;
                        if (Int32.TryParse(manualInputRef.Text, out newValue))
                        {
                            if (newValue < control.MinSoftwareValue)
                            {
                                newValue = Convert.ToInt32(control.MinSoftwareValue);
                                manualInputRef.Text = newValue.ToString();
                            }
                            else if (newValue > control.MaxSoftwareValue)
                            {
                                newValue = Convert.ToInt32(control.MaxSoftwareValue);
                                manualInputRef.Text = newValue.ToString();
                            }
                            manualInput.Tag = newValue;
                            control.SetSoftware(newValue);
                        }
                        else
                        {
                            manualInputRef.Text = ((int)manualInput.Tag).ToString();
                        }
                    }
                };
                manualItem.DropDownItems.Add(manualInput);
                /*
                for (int i = 0; i <= 100; i += 5)
                {
                    if (i <= control.MaxSoftwareValue &&
                        i >= control.MinSoftwareValue)
                    {
                        item = new ToolStripMenuItem(i + " %");
                        item.Checked = true;
                        manualItem.DropDownItems.Add(item);
                        item.Checked = control.ControlMode == ControlMode.Software &&
                          Math.Round(control.SoftwareValue) == i;
                        int softwareValue = i;
                        item.Click += delegate (object obj, EventArgs args)
                        {
                            control.SetSoftware(softwareValue);
                        };
                    }
                }*/
                treeContextMenu.Items.Add(controlItem);
            }

            treeContextMenu.Show(treeView, treeView.PointToClient(Cursor.Position));
        }

        private void HardwareNodeSelected()
        {
            treeContextMenu.Items.Clear();

            if (nodeTextBoxText.EditEnabled)
            {
                ToolStripMenuItem item = new ToolStripMenuItem("Rename");
                //  item.Tag = node;
                item.Click += delegate (object obj, EventArgs args)
                {
                    // SensorNode sn = (obj as MenuItem).Tag as SensorNode;
                    nodeTextBoxText.BeginEdit();
                };
                treeContextMenu.Items.Add(item);
            }

            treeContextMenu.Show(treeView, treeView.PointToClient(Cursor.Position));
        }

        private void plotMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void resetPlotAndGadgetWindowPositionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gadget.Location = this.ParentRootForm.Location;
            plotForm.Location = this.ParentRootForm.Location;
        }
    }
}
