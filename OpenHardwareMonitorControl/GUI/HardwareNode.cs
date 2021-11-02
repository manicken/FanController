/*
 
  This Source Code Form is subject to the terms of the Mozilla Public
  License, v. 2.0. If a copy of the MPL was not distributed with this
  file, You can obtain one at http://mozilla.org/MPL/2.0/.
 
  Copyright (C) 2009-2012 Michael Möller <mmoeller@openhardwaremonitor.org>
	
*/

using System;
using System.Collections.Generic;
using System.Drawing;
using OpenHardwareMonitor.Hardware;

namespace OpenHardwareMonitor.GUI
{

    public class HardwareNode : Node
    {

        //public delegate void SensorAddedEventHandler(SensorNode sensorNode);
        public Action<ISensor> SensorAdded;

        private PersistentSettings settings;
        private UnitManager unitManager;
        private IHardware hardware;

        private List<TypeNode> typeNodes = new List<TypeNode>();

        public HardwareNode(IHardware hardware, PersistentSettings settings, UnitManager unitManager, Action<ISensor> SensorAddedMethod) : base() 
        {
            this.settings = settings;
            this.unitManager = unitManager;
            this.hardware = hardware;
            this.Image = HardwareTypeImage.Instance.GetImage(hardware.HardwareType);

            this.SensorAdded = SensorAddedMethod;

            hardware.SensorAdded += new SensorEventHandler(_SensorAdded);
            hardware.SensorRemoved += new SensorEventHandler(SensorRemoved);

            foreach (SensorType sensorType in Enum.GetValues(typeof(SensorType)))
                typeNodes.Add(new TypeNode(sensorType));

            foreach (ISensor sensor in hardware.Sensors)
                _SensorAdded(sensor);
        }

        

        public override string Text
        {
            get { return hardware.Name; }
            set { hardware.Name = value; }
        }

        public IHardware Hardware {
            get { return hardware; }
        }

        private void UpdateNode(TypeNode node)
        {  
            if (node.Nodes.Count > 0)
            {
                if (!Nodes.Contains(node)) {
                int i = 0;
                while (i < Nodes.Count && ((TypeNode)Nodes[i]).SensorType < node.SensorType)
                    i++;
                Nodes.Insert(i, node);  
            }
            } else {
                if (Nodes.Contains(node))
                    Nodes.Remove(node);
            }
        }

        private void SensorRemoved(ISensor sensor) {
          foreach (TypeNode typeNode in typeNodes)
            if (typeNode.SensorType == sensor.SensorType) { 
              SensorNode sensorNode = null;
              foreach (Node node in typeNode.Nodes) {
                SensorNode n = node as SensorNode;
                if (n != null && n.Sensor == sensor)
                  sensorNode = n;
              }
              if (sensorNode != null) {
                sensorNode.PlotSelectionChanged -= SensorPlotSelectionChanged;
                //sensorNode.MonitoredSelectionChanged -= SensorMonitoredSelectionChanged;
                typeNode.Nodes.Remove(sensorNode);
                UpdateNode(typeNode);
              }
            }
                SensorPlotSelectionChanged(this, null);
        }

        private void InsertSorted(Node node, ISensor sensor) {
          int i = 0;
          while (i < node.Nodes.Count &&
            ((SensorNode)node.Nodes[i]).Sensor.Index < sensor.Index)
            i++;
          SensorNode sensorNode = new SensorNode(sensor, settings, unitManager);
          sensorNode.PlotSelectionChanged += SensorPlotSelectionChanged;
          //sensorNode.MonitoredSelectionChanged += SensorMonitoredSelectionChanged;
          node.Nodes.Insert(i, sensorNode);
            if (SensorAdded != null)
                SensorAdded(sensor);
        }

        private void SensorPlotSelectionChanged(object sender, EventArgs e) {
          if (PlotSelectionChanged != null)
            PlotSelectionChanged(this, null);
        }
        private void SensorMonitoredSelectionChanged(SensorNode sensorNode, bool settingAdded)
            {
                if (MonitoredSelectionChanged != null)
                    MonitoredSelectionChanged(sensorNode, settingAdded);
            }

        private void _SensorAdded(ISensor sensor) {
          foreach (TypeNode typeNode in typeNodes)
            if (typeNode.SensorType == sensor.SensorType) {
              InsertSorted(typeNode, sensor);
              UpdateNode(typeNode);          
            }
                SensorPlotSelectionChanged(this, null);
        }

        
        public event MonitoredSelectionChangedEventHandler MonitoredSelectionChanged = null;
            public event EventHandler PlotSelectionChanged;
      }
}
