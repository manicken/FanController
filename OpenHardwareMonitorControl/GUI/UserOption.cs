/*
 
  This Source Code Form is subject to the terms of the Mozilla Public
  License, v. 2.0. If a copy of the MPL was not distributed with this
  file, You can obtain one at http://mozilla.org/MPL/2.0/.
 
  Copyright (C) 2009-2010 Michael Möller <mmoeller@openhardwaremonitor.org>
	
*/

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using OpenHardwareMonitor.Utilities;

namespace OpenHardwareMonitor.GUI {
    public class UserOption {
        private string name;
        private bool value;
        private MenuItem menuItem;
        private ToolStripMenuItem toolStripMenuItem;
        private event EventHandler changed;
        private PersistentSettings settings;

        public UserOption(string name, bool value, ToolStripMenuItem toolStripMenuItem, PersistentSettings settings) {

          this.settings = settings;
          this.name = name;
          if (name != null)
            this.value = settings.GetValue(name, value);
          else
            this.value = value;
          this.toolStripMenuItem = toolStripMenuItem;
          this.toolStripMenuItem.Checked = this.value;
          this.toolStripMenuItem.Click += new EventHandler(Item_Click);
        }

        public UserOption(string name, bool value, MenuItem menuItem, PersistentSettings settings)
        {

            this.settings = settings;
            this.name = name;
            if (name != null)
                this.value = settings.GetValue(name, value);
            else
                this.value = value;
            this.menuItem = menuItem;
            this.menuItem.Checked = this.value;
            this.menuItem.Click += new EventHandler(Item_Click);
        }

        private void Item_Click(object sender, EventArgs e) {
            this.Value = !this.Value;
        }    

        private void SetMenuItemChecked(bool value)
        {
            if (this.menuItem != null)
                this.menuItem.Checked = value;
            else if (this.toolStripMenuItem != null)
                this.toolStripMenuItem.Checked = value;
        }

        public bool Value {
            get { return value; }
            set
            {
                if (this.value == value)
                    return;
                this.value = value;
                if (this.name != null)
                    settings.SetValue(name, value);

                SetMenuItemChecked(value);

                if (changed != null)
                    changed(this, null);
            
            }
        }

        public event EventHandler Changed {
          add {
            changed += value;
            if (changed != null)
              changed(this, null);
          }
          remove {
            changed -= value;
          }
        }
    }
}
