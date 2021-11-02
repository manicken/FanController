using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace USB2550HidTest.Forms
{
    public class FlashingLabel : Label
    {
        public Color State1_BackGroundColor = Color.White;
        public Color State2_BackGroundColor = Color.Red;
        public Color State1_TextColor = Color.Red;
        public Color State2_TextColor = Color.White;

        public System.Windows.Forms.Timer timer;
        public bool IsState_One = true;

        public FlashingLabel()
        {
            timer = new Timer();
            timer.Tick += Timer_Tick;
            timer.Interval = 1000;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (IsState_One)
            {
                IsState_One = false;
                this.BackColor = State2_BackGroundColor;
                this.ForeColor = State2_TextColor;
            }
            else
            {
                IsState_One = true;
                this.BackColor = State1_BackGroundColor;
                this.ForeColor = State1_TextColor;
            }
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            timer.Enabled = this.Visible;
        }

    }
}
