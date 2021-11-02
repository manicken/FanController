namespace USB2550HidTest.Forms
{
    partial class Mainform
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Mainform));
            this.btnClearLog = new System.Windows.Forms.Button();
            this.spcoMain = new System.Windows.Forms.SplitContainer();
            this.panelOpenHardwareMonitor = new System.Windows.Forms.Panel();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.tbarPWM5 = new USB2550HidTest.Forms.CustomSlider();
            this.lblRPM6 = new System.Windows.Forms.Label();
            this.lblRPM5 = new System.Windows.Forms.Label();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.tbarPWM2 = new USB2550HidTest.Forms.CustomSlider();
            this.lblRPM2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tbarPWM3 = new USB2550HidTest.Forms.CustomSlider();
            this.lblRPM4 = new System.Windows.Forms.Label();
            this.lblRPM3 = new System.Windows.Forms.Label();
            this.grpCpuPump = new System.Windows.Forms.GroupBox();
            this.tbarPWM1 = new USB2550HidTest.Forms.CustomSlider();
            this.lblRPM1 = new System.Windows.Forms.Label();
            this.splitOHM = new System.Windows.Forms.SplitContainer();
            this.chkBoxDebugPwmSet = new System.Windows.Forms.CheckBox();
            this.rtxtLog = new System.Windows.Forms.RichTextBox();
            this.chkBoxShowGetRPMdata = new System.Windows.Forms.CheckBox();
            this.btnClearCommLog = new System.Windows.Forms.Button();
            this.rtxtHidCommLog = new System.Windows.Forms.RichTextBox();
            this.tsMain = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnConnect = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsddAdvanced = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsbtnOtherSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.tsddEEPROM = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsmiEEPROM_loadSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEEPROM_saveSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.developmentTestsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testFailsafeWithDivitionByZeroToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testMethodToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testSystemGoingToSleepToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testSystemResumeFromSleepToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnOpenScriptEditor = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.imgArray_dis_connect = new System.Windows.Forms.ImageList(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.helloworldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helloToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.stss = new System.Windows.Forms.StatusStrip();
            this.tsslblScriptRunCountLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslblScriptRunCountText = new System.Windows.Forms.ToolStripStatusLabel();
            this.pnlHidFanCtrl = new System.Windows.Forms.Panel();
            this.splitHid = new System.Windows.Forms.SplitContainer();
            this.pnlHidCommLog = new System.Windows.Forms.Panel();
            this.pnlMainLog = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.spcoMain)).BeginInit();
            this.spcoMain.Panel1.SuspendLayout();
            this.spcoMain.Panel2.SuspendLayout();
            this.spcoMain.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.grpCpuPump.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitOHM)).BeginInit();
            this.splitOHM.Panel1.SuspendLayout();
            this.splitOHM.Panel2.SuspendLayout();
            this.splitOHM.SuspendLayout();
            this.tsMain.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.stss.SuspendLayout();
            this.pnlHidFanCtrl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitHid)).BeginInit();
            this.splitHid.Panel1.SuspendLayout();
            this.splitHid.Panel2.SuspendLayout();
            this.splitHid.SuspendLayout();
            this.pnlHidCommLog.SuspendLayout();
            this.pnlMainLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClearLog
            // 
            this.btnClearLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearLog.Location = new System.Drawing.Point(584, 3);
            this.btnClearLog.Name = "btnClearLog";
            this.btnClearLog.Size = new System.Drawing.Size(64, 22);
            this.btnClearLog.TabIndex = 16;
            this.btnClearLog.Text = "clear log";
            this.btnClearLog.UseVisualStyleBackColor = true;
            this.btnClearLog.Click += new System.EventHandler(this.btnClearLog_Click);
            // 
            // spcoMain
            // 
            this.spcoMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.spcoMain.Location = new System.Drawing.Point(3, 73);
            this.spcoMain.Name = "spcoMain";
            // 
            // spcoMain.Panel1
            // 
            this.spcoMain.Panel1.Controls.Add(this.splitHid);
            this.spcoMain.Panel1.Padding = new System.Windows.Forms.Padding(3);
            // 
            // spcoMain.Panel2
            // 
            this.spcoMain.Panel2.Controls.Add(this.splitOHM);
            this.spcoMain.Panel2.Padding = new System.Windows.Forms.Padding(3);
            this.spcoMain.Size = new System.Drawing.Size(925, 536);
            this.spcoMain.SplitterDistance = 252;
            this.spcoMain.SplitterWidth = 8;
            this.spcoMain.TabIndex = 20;
            // 
            // panelOpenHardwareMonitor
            // 
            this.panelOpenHardwareMonitor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelOpenHardwareMonitor.Location = new System.Drawing.Point(3, 3);
            this.panelOpenHardwareMonitor.Name = "panelOpenHardwareMonitor";
            this.panelOpenHardwareMonitor.Size = new System.Drawing.Size(651, 387);
            this.panelOpenHardwareMonitor.TabIndex = 37;
            // 
            // groupBox6
            // 
            this.groupBox6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox6.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox6.Controls.Add(this.groupBox8);
            this.groupBox6.Controls.Add(this.groupBox9);
            this.groupBox6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox6.Location = new System.Drawing.Point(118, 3);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(109, 344);
            this.groupBox6.TabIndex = 34;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Graphic Card";
            // 
            // groupBox8
            // 
            this.groupBox8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox8.Controls.Add(this.tbarPWM5);
            this.groupBox8.Controls.Add(this.lblRPM6);
            this.groupBox8.Controls.Add(this.lblRPM5);
            this.groupBox8.Location = new System.Drawing.Point(53, 19);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(50, 319);
            this.groupBox8.TabIndex = 35;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Fans";
            // 
            // tbarPWM5
            // 
            this.tbarPWM5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbarPWM5.Location = new System.Drawing.Point(7, 49);
            this.tbarPWM5.Name = "tbarPWM5";
            this.tbarPWM5.Padding = new System.Windows.Forms.Padding(3);
            this.tbarPWM5.Size = new System.Drawing.Size(32, 262);
            this.tbarPWM5.SliderBarColor = System.Drawing.Color.DeepSkyBlue;
            this.tbarPWM5.TabIndex = 39;
            this.tbarPWM5.Value = 1;
            this.tbarPWM5.ValueMax = 50;
            this.tbarPWM5.ValueMin = 0;
            this.tbarPWM5.ValueSteps = 15;
            // 
            // lblRPM6
            // 
            this.lblRPM6.Location = new System.Drawing.Point(1, 32);
            this.lblRPM6.Name = "lblRPM6";
            this.lblRPM6.Size = new System.Drawing.Size(47, 13);
            this.lblRPM6.TabIndex = 37;
            this.lblRPM6.Text = "label1";
            this.lblRPM6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRPM5
            // 
            this.lblRPM5.Location = new System.Drawing.Point(1, 16);
            this.lblRPM5.Name = "lblRPM5";
            this.lblRPM5.Size = new System.Drawing.Size(47, 13);
            this.lblRPM5.TabIndex = 36;
            this.lblRPM5.Text = "label1";
            this.lblRPM5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox9
            // 
            this.groupBox9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox9.Controls.Add(this.tbarPWM2);
            this.groupBox9.Controls.Add(this.lblRPM2);
            this.groupBox9.Location = new System.Drawing.Point(6, 19);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(48, 319);
            this.groupBox9.TabIndex = 35;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Pump";
            // 
            // tbarPWM2
            // 
            this.tbarPWM2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbarPWM2.Location = new System.Drawing.Point(6, 49);
            this.tbarPWM2.Name = "tbarPWM2";
            this.tbarPWM2.Padding = new System.Windows.Forms.Padding(3);
            this.tbarPWM2.Size = new System.Drawing.Size(32, 262);
            this.tbarPWM2.SliderBarColor = System.Drawing.Color.Aqua;
            this.tbarPWM2.TabIndex = 39;
            this.tbarPWM2.Value = 1;
            this.tbarPWM2.ValueMax = 50;
            this.tbarPWM2.ValueMin = 1;
            this.tbarPWM2.ValueSteps = 15;
            // 
            // lblRPM2
            // 
            this.lblRPM2.Location = new System.Drawing.Point(1, 16);
            this.lblRPM2.Name = "lblRPM2";
            this.lblRPM2.Size = new System.Drawing.Size(47, 13);
            this.lblRPM2.TabIndex = 36;
            this.lblRPM2.Text = "label1";
            this.lblRPM2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Controls.Add(this.grpCpuPump);
            this.groupBox3.Location = new System.Drawing.Point(3, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(109, 344);
            this.groupBox3.TabIndex = 34;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "CPU ";
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox4.Controls.Add(this.tbarPWM3);
            this.groupBox4.Controls.Add(this.lblRPM4);
            this.groupBox4.Controls.Add(this.lblRPM3);
            this.groupBox4.Location = new System.Drawing.Point(53, 19);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(50, 319);
            this.groupBox4.TabIndex = 35;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Fans";
            // 
            // tbarPWM3
            // 
            this.tbarPWM3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbarPWM3.Location = new System.Drawing.Point(7, 49);
            this.tbarPWM3.Name = "tbarPWM3";
            this.tbarPWM3.Padding = new System.Windows.Forms.Padding(3);
            this.tbarPWM3.Size = new System.Drawing.Size(32, 262);
            this.tbarPWM3.SliderBarColor = System.Drawing.Color.DeepSkyBlue;
            this.tbarPWM3.TabIndex = 39;
            this.tbarPWM3.Value = 1;
            this.tbarPWM3.ValueMax = 50;
            this.tbarPWM3.ValueMin = 1;
            this.tbarPWM3.ValueSteps = 15;
            // 
            // lblRPM4
            // 
            this.lblRPM4.Location = new System.Drawing.Point(1, 32);
            this.lblRPM4.Name = "lblRPM4";
            this.lblRPM4.Size = new System.Drawing.Size(47, 13);
            this.lblRPM4.TabIndex = 37;
            this.lblRPM4.Text = "label1";
            this.lblRPM4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRPM3
            // 
            this.lblRPM3.Location = new System.Drawing.Point(1, 16);
            this.lblRPM3.Name = "lblRPM3";
            this.lblRPM3.Size = new System.Drawing.Size(47, 13);
            this.lblRPM3.TabIndex = 36;
            this.lblRPM3.Text = "label1";
            this.lblRPM3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grpCpuPump
            // 
            this.grpCpuPump.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.grpCpuPump.Controls.Add(this.tbarPWM1);
            this.grpCpuPump.Controls.Add(this.lblRPM1);
            this.grpCpuPump.Location = new System.Drawing.Point(6, 19);
            this.grpCpuPump.Name = "grpCpuPump";
            this.grpCpuPump.Size = new System.Drawing.Size(48, 319);
            this.grpCpuPump.TabIndex = 35;
            this.grpCpuPump.TabStop = false;
            this.grpCpuPump.Text = "Pump";
            // 
            // tbarPWM1
            // 
            this.tbarPWM1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbarPWM1.Location = new System.Drawing.Point(6, 49);
            this.tbarPWM1.Name = "tbarPWM1";
            this.tbarPWM1.Padding = new System.Windows.Forms.Padding(3);
            this.tbarPWM1.Size = new System.Drawing.Size(32, 262);
            this.tbarPWM1.SliderBarColor = System.Drawing.Color.Aqua;
            this.tbarPWM1.TabIndex = 39;
            this.tbarPWM1.Value = 1;
            this.tbarPWM1.ValueMax = 50;
            this.tbarPWM1.ValueMin = 1;
            this.tbarPWM1.ValueSteps = 15;
            // 
            // lblRPM1
            // 
            this.lblRPM1.Location = new System.Drawing.Point(1, 16);
            this.lblRPM1.Name = "lblRPM1";
            this.lblRPM1.Size = new System.Drawing.Size(47, 13);
            this.lblRPM1.TabIndex = 36;
            this.lblRPM1.Text = "label1";
            this.lblRPM1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // splitOHM
            // 
            this.splitOHM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitOHM.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitOHM.Location = new System.Drawing.Point(3, 3);
            this.splitOHM.Name = "splitOHM";
            this.splitOHM.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitOHM.Panel1
            // 
            this.splitOHM.Panel1.Controls.Add(this.panelOpenHardwareMonitor);
            this.splitOHM.Panel1.Padding = new System.Windows.Forms.Padding(3);
            // 
            // splitOHM.Panel2
            // 
            this.splitOHM.Panel2.Controls.Add(this.pnlMainLog);
            this.splitOHM.Panel2.Padding = new System.Windows.Forms.Padding(3);
            this.splitOHM.Size = new System.Drawing.Size(659, 530);
            this.splitOHM.SplitterDistance = 395;
            this.splitOHM.SplitterWidth = 8;
            this.splitOHM.TabIndex = 17;
            this.splitOHM.SizeChanged += new System.EventHandler(this.splitContainer2_SizeChanged);
            // 
            // chkBoxDebugPwmSet
            // 
            this.chkBoxDebugPwmSet.AutoSize = true;
            this.chkBoxDebugPwmSet.Location = new System.Drawing.Point(5, 3);
            this.chkBoxDebugPwmSet.Name = "chkBoxDebugPwmSet";
            this.chkBoxDebugPwmSet.Size = new System.Drawing.Size(106, 17);
            this.chkBoxDebugPwmSet.TabIndex = 17;
            this.chkBoxDebugPwmSet.Text = "Debug - PwmSet";
            this.chkBoxDebugPwmSet.UseVisualStyleBackColor = true;
            // 
            // rtxtLog
            // 
            this.rtxtLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtxtLog.Location = new System.Drawing.Point(5, 22);
            this.rtxtLog.Name = "rtxtLog";
            this.rtxtLog.Size = new System.Drawing.Size(643, 94);
            this.rtxtLog.TabIndex = 0;
            this.rtxtLog.Text = "";
            this.rtxtLog.TextChanged += new System.EventHandler(this.rtxtLog_TextChanged);
            // 
            // chkBoxShowGetRPMdata
            // 
            this.chkBoxShowGetRPMdata.AutoSize = true;
            this.chkBoxShowGetRPMdata.Location = new System.Drawing.Point(9, 3);
            this.chkBoxShowGetRPMdata.Name = "chkBoxShowGetRPMdata";
            this.chkBoxShowGetRPMdata.Size = new System.Drawing.Size(153, 17);
            this.chkBoxShowGetRPMdata.TabIndex = 19;
            this.chkBoxShowGetRPMdata.Text = "Show \"GetRPM\" response";
            this.chkBoxShowGetRPMdata.UseVisualStyleBackColor = true;
            this.chkBoxShowGetRPMdata.CheckedChanged += new System.EventHandler(this.chkBoxShowGetRPMdata_CheckedChanged);
            // 
            // btnClearCommLog
            // 
            this.btnClearCommLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearCommLog.Location = new System.Drawing.Point(171, 0);
            this.btnClearCommLog.Name = "btnClearCommLog";
            this.btnClearCommLog.Size = new System.Drawing.Size(64, 21);
            this.btnClearCommLog.TabIndex = 18;
            this.btnClearCommLog.Text = "clear log";
            this.btnClearCommLog.UseVisualStyleBackColor = true;
            this.btnClearCommLog.Click += new System.EventHandler(this.btnClearCommLog_Click);
            // 
            // rtxtHidCommLog
            // 
            this.rtxtHidCommLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtxtHidCommLog.Location = new System.Drawing.Point(3, 19);
            this.rtxtHidCommLog.Name = "rtxtHidCommLog";
            this.rtxtHidCommLog.Size = new System.Drawing.Size(232, 134);
            this.rtxtHidCommLog.TabIndex = 1;
            this.rtxtHidCommLog.Text = "";
            this.rtxtHidCommLog.TextChanged += new System.EventHandler(this.rtxtCommLog_TextChanged);
            // 
            // tsMain
            // 
            this.tsMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsMain.ImageScalingSize = new System.Drawing.Size(48, 48);
            this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator4,
            this.tsbtnConnect,
            this.toolStripSeparator1,
            this.tsddAdvanced,
            this.toolStripSeparator2,
            this.tsbtnOpenScriptEditor,
            this.toolStripSeparator3});
            this.tsMain.Location = new System.Drawing.Point(0, 0);
            this.tsMain.Name = "tsMain";
            this.tsMain.Size = new System.Drawing.Size(928, 70);
            this.tsMain.TabIndex = 30;
            this.tsMain.Text = "toolStrip1";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 70);
            // 
            // tsbtnConnect
            // 
            this.tsbtnConnect.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnConnect.Image")));
            this.tsbtnConnect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnConnect.Name = "tsbtnConnect";
            this.tsbtnConnect.Size = new System.Drawing.Size(56, 67);
            this.tsbtnConnect.Text = "Connect";
            this.tsbtnConnect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbtnConnect.Click += new System.EventHandler(this.tsbtnConnect_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 70);
            // 
            // tsddAdvanced
            // 
            this.tsddAdvanced.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnOtherSettings,
            this.tsddEEPROM,
            this.developmentTestsToolStripMenuItem});
            this.tsddAdvanced.Image = ((System.Drawing.Image)(resources.GetObject("tsddAdvanced.Image")));
            this.tsddAdvanced.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddAdvanced.Name = "tsddAdvanced";
            this.tsddAdvanced.Size = new System.Drawing.Size(73, 67);
            this.tsddAdvanced.Text = "Advanced";
            this.tsddAdvanced.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tsbtnOtherSettings
            // 
            this.tsbtnOtherSettings.Name = "tsbtnOtherSettings";
            this.tsbtnOtherSettings.Size = new System.Drawing.Size(180, 22);
            this.tsbtnOtherSettings.Text = "Other Settings";
            this.tsbtnOtherSettings.Click += new System.EventHandler(this.tsbtnAdvanced_Click);
            // 
            // tsddEEPROM
            // 
            this.tsddEEPROM.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiEEPROM_loadSettings,
            this.tsmiEEPROM_saveSettings});
            this.tsddEEPROM.Image = ((System.Drawing.Image)(resources.GetObject("tsddEEPROM.Image")));
            this.tsddEEPROM.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddEEPROM.Name = "tsddEEPROM";
            this.tsddEEPROM.Size = new System.Drawing.Size(114, 52);
            this.tsddEEPROM.Text = "EEPROM";
            // 
            // tsmiEEPROM_loadSettings
            // 
            this.tsmiEEPROM_loadSettings.Name = "tsmiEEPROM_loadSettings";
            this.tsmiEEPROM_loadSettings.Size = new System.Drawing.Size(180, 22);
            this.tsmiEEPROM_loadSettings.Text = "Load Settings";
            this.tsmiEEPROM_loadSettings.Click += new System.EventHandler(this.readSettingsToolStripMenuItem_Click);
            // 
            // tsmiEEPROM_saveSettings
            // 
            this.tsmiEEPROM_saveSettings.Name = "tsmiEEPROM_saveSettings";
            this.tsmiEEPROM_saveSettings.Size = new System.Drawing.Size(180, 22);
            this.tsmiEEPROM_saveSettings.Text = "Save Settings";
            this.tsmiEEPROM_saveSettings.Click += new System.EventHandler(this.tsmiEEPROM_saveSettings_Click);
            // 
            // developmentTestsToolStripMenuItem
            // 
            this.developmentTestsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testFailsafeWithDivitionByZeroToolStripMenuItem,
            this.testMethodToolStripMenuItem});
            this.developmentTestsToolStripMenuItem.Name = "developmentTestsToolStripMenuItem";
            this.developmentTestsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.developmentTestsToolStripMenuItem.Text = "Development tests";
            // 
            // testFailsafeWithDivitionByZeroToolStripMenuItem
            // 
            this.testFailsafeWithDivitionByZeroToolStripMenuItem.Name = "testFailsafeWithDivitionByZeroToolStripMenuItem";
            this.testFailsafeWithDivitionByZeroToolStripMenuItem.Size = new System.Drawing.Size(245, 22);
            this.testFailsafeWithDivitionByZeroToolStripMenuItem.Text = "Test failsafe with divition by zero";
            this.testFailsafeWithDivitionByZeroToolStripMenuItem.Click += new System.EventHandler(this.testFailsafeWithDivitionByZeroToolStripMenuItem_Click);
            // 
            // testMethodToolStripMenuItem
            // 
            this.testMethodToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testSystemGoingToSleepToolStripMenuItem,
            this.testSystemResumeFromSleepToolStripMenuItem});
            this.testMethodToolStripMenuItem.Name = "testMethodToolStripMenuItem";
            this.testMethodToolStripMenuItem.Size = new System.Drawing.Size(245, 22);
            this.testMethodToolStripMenuItem.Text = "Test method:";
            // 
            // testSystemGoingToSleepToolStripMenuItem
            // 
            this.testSystemGoingToSleepToolStripMenuItem.Name = "testSystemGoingToSleepToolStripMenuItem";
            this.testSystemGoingToSleepToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.testSystemGoingToSleepToolStripMenuItem.Text = "SystemGoingToSleep()";
            this.testSystemGoingToSleepToolStripMenuItem.Click += new System.EventHandler(this.testSystemGoingToSleepToolStripMenuItem_Click);
            // 
            // testSystemResumeFromSleepToolStripMenuItem
            // 
            this.testSystemResumeFromSleepToolStripMenuItem.Name = "testSystemResumeFromSleepToolStripMenuItem";
            this.testSystemResumeFromSleepToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.testSystemResumeFromSleepToolStripMenuItem.Text = "SystemResumeFromSleep()";
            this.testSystemResumeFromSleepToolStripMenuItem.Click += new System.EventHandler(this.testSystemResumeFromSleepToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 70);
            // 
            // tsbtnOpenScriptEditor
            // 
            this.tsbtnOpenScriptEditor.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnOpenScriptEditor.Image")));
            this.tsbtnOpenScriptEditor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnOpenScriptEditor.Name = "tsbtnOpenScriptEditor";
            this.tsbtnOpenScriptEditor.Size = new System.Drawing.Size(75, 67);
            this.tsbtnOpenScriptEditor.Text = "Script Editor";
            this.tsbtnOpenScriptEditor.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbtnOpenScriptEditor.Click += new System.EventHandler(this.tsbtnOpenNewScriptEditor_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 70);
            // 
            // imgArray_dis_connect
            // 
            this.imgArray_dis_connect.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgArray_dis_connect.ImageStream")));
            this.imgArray_dis_connect.TransparentColor = System.Drawing.Color.Transparent;
            this.imgArray_dis_connect.Images.SetKeyName(0, "connect");
            this.imgArray_dis_connect.Images.SetKeyName(1, "disconnect");
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helloworldToolStripMenuItem,
            this.toolStripTextBox1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(161, 51);
            // 
            // helloworldToolStripMenuItem
            // 
            this.helloworldToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helloToolStripMenuItem});
            this.helloworldToolStripMenuItem.Name = "helloworldToolStripMenuItem";
            this.helloworldToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.helloworldToolStripMenuItem.Text = "helloworld";
            // 
            // helloToolStripMenuItem
            // 
            this.helloToolStripMenuItem.Name = "helloToolStripMenuItem";
            this.helloToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.helloToolStripMenuItem.Text = "hello";
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(100, 23);
            // 
            // stss
            // 
            this.stss.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslblScriptRunCountLabel,
            this.tsslblScriptRunCountText});
            this.stss.Location = new System.Drawing.Point(0, 612);
            this.stss.Name = "stss";
            this.stss.Size = new System.Drawing.Size(928, 22);
            this.stss.TabIndex = 31;
            this.stss.Text = "statusStrip1";
            // 
            // tsslblScriptRunCountLabel
            // 
            this.tsslblScriptRunCountLabel.Name = "tsslblScriptRunCountLabel";
            this.tsslblScriptRunCountLabel.Size = new System.Drawing.Size(94, 17);
            this.tsslblScriptRunCountLabel.Text = "ScriptRunCount:";
            // 
            // tsslblScriptRunCountText
            // 
            this.tsslblScriptRunCountText.Name = "tsslblScriptRunCountText";
            this.tsslblScriptRunCountText.Size = new System.Drawing.Size(118, 17);
            this.tsslblScriptRunCountText.Text = "tsslblScriptRunCount";
            // 
            // pnlHidFanCtrl
            // 
            this.pnlHidFanCtrl.Controls.Add(this.groupBox3);
            this.pnlHidFanCtrl.Controls.Add(this.groupBox6);
            this.pnlHidFanCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHidFanCtrl.Location = new System.Drawing.Point(3, 3);
            this.pnlHidFanCtrl.Name = "pnlHidFanCtrl";
            this.pnlHidFanCtrl.Size = new System.Drawing.Size(238, 350);
            this.pnlHidFanCtrl.TabIndex = 18;
            // 
            // splitHid
            // 
            this.splitHid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitHid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitHid.Location = new System.Drawing.Point(3, 3);
            this.splitHid.Name = "splitHid";
            this.splitHid.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitHid.Panel1
            // 
            this.splitHid.Panel1.Controls.Add(this.pnlHidFanCtrl);
            this.splitHid.Panel1.Padding = new System.Windows.Forms.Padding(3);
            // 
            // splitHid.Panel2
            // 
            this.splitHid.Panel2.Controls.Add(this.pnlHidCommLog);
            this.splitHid.Panel2.Padding = new System.Windows.Forms.Padding(3);
            this.splitHid.Size = new System.Drawing.Size(246, 530);
            this.splitHid.SplitterDistance = 358;
            this.splitHid.SplitterWidth = 8;
            this.splitHid.TabIndex = 0;
            // 
            // pnlHidCommLog
            // 
            this.pnlHidCommLog.Controls.Add(this.chkBoxShowGetRPMdata);
            this.pnlHidCommLog.Controls.Add(this.rtxtHidCommLog);
            this.pnlHidCommLog.Controls.Add(this.btnClearCommLog);
            this.pnlHidCommLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHidCommLog.Location = new System.Drawing.Point(3, 3);
            this.pnlHidCommLog.Name = "pnlHidCommLog";
            this.pnlHidCommLog.Size = new System.Drawing.Size(238, 156);
            this.pnlHidCommLog.TabIndex = 0;
            // 
            // pnlMainLog
            // 
            this.pnlMainLog.Controls.Add(this.chkBoxDebugPwmSet);
            this.pnlMainLog.Controls.Add(this.rtxtLog);
            this.pnlMainLog.Controls.Add(this.btnClearLog);
            this.pnlMainLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMainLog.Location = new System.Drawing.Point(3, 3);
            this.pnlMainLog.Name = "pnlMainLog";
            this.pnlMainLog.Size = new System.Drawing.Size(651, 119);
            this.pnlMainLog.TabIndex = 38;
            // 
            // Mainform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(928, 634);
            this.Controls.Add(this.stss);
            this.Controls.Add(this.tsMain);
            this.Controls.Add(this.spcoMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Mainform";
            this.Text = "PIC24FJ256GB106 - microsan development board";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Load += new System.EventHandler(this.FrmMainLoad);
            this.Shown += new System.EventHandler(this.Mainform_Shown);
            this.spcoMain.Panel1.ResumeLayout(false);
            this.spcoMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spcoMain)).EndInit();
            this.spcoMain.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.grpCpuPump.ResumeLayout(false);
            this.splitOHM.Panel1.ResumeLayout(false);
            this.splitOHM.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitOHM)).EndInit();
            this.splitOHM.ResumeLayout(false);
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMenuStrip1.PerformLayout();
            this.stss.ResumeLayout(false);
            this.stss.PerformLayout();
            this.pnlHidFanCtrl.ResumeLayout(false);
            this.splitHid.Panel1.ResumeLayout(false);
            this.splitHid.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitHid)).EndInit();
            this.splitHid.ResumeLayout(false);
            this.pnlHidCommLog.ResumeLayout(false);
            this.pnlHidCommLog.PerformLayout();
            this.pnlMainLog.ResumeLayout(false);
            this.pnlMainLog.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnClearLog;
        private System.Windows.Forms.SplitContainer spcoMain;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ToolStrip tsMain;
        private System.Windows.Forms.ToolStripButton tsbtnConnect;
        private System.Windows.Forms.ToolStripDropDownButton tsddEEPROM;
        private System.Windows.Forms.ToolStripMenuItem tsmiEEPROM_loadSettings;
        private System.Windows.Forms.ToolStripMenuItem tsmiEEPROM_saveSettings;
        private System.Windows.Forms.ImageList imgArray_dis_connect;
        private System.Windows.Forms.RichTextBox rtxtLog;
        private System.Windows.Forms.Label lblRPM1;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Label lblRPM5;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.Label lblRPM2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label lblRPM3;
        private System.Windows.Forms.GroupBox grpCpuPump;
        private CustomSlider tbarPWM1;
        private CustomSlider tbarPWM2;
        private CustomSlider tbarPWM5;
        private CustomSlider tbarPWM3;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem helloworldToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helloToolStripMenuItem;
        private System.Windows.Forms.Panel panelOpenHardwareMonitor;
        private System.Windows.Forms.ToolStripDropDownButton tsddAdvanced;
        private System.Windows.Forms.ToolStripMenuItem tsbtnOtherSettings;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem developmentTestsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testFailsafeWithDivitionByZeroToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.ToolStripButton tsbtnOpenScriptEditor;
        private System.Windows.Forms.SplitContainer splitOHM;
        private System.Windows.Forms.RichTextBox rtxtHidCommLog;
        private System.Windows.Forms.Button btnClearCommLog;
        private System.Windows.Forms.ToolStripMenuItem testMethodToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testSystemGoingToSleepToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testSystemResumeFromSleepToolStripMenuItem;
        public System.Windows.Forms.ToolStripStatusLabel tsslblScriptRunCountText;
        public System.Windows.Forms.CheckBox chkBoxShowGetRPMdata;
        public System.Windows.Forms.CheckBox chkBoxDebugPwmSet;
        public System.Windows.Forms.StatusStrip stss;
        public System.Windows.Forms.ToolStripStatusLabel tsslblScriptRunCountLabel;
        private System.Windows.Forms.Label lblRPM6;
        private System.Windows.Forms.Label lblRPM4;
        private System.Windows.Forms.SplitContainer splitHid;
        private System.Windows.Forms.Panel pnlHidFanCtrl;
        private System.Windows.Forms.Panel pnlHidCommLog;
        private System.Windows.Forms.Panel pnlMainLog;
    }
}

