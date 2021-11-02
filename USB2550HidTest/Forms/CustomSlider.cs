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
    public partial class CustomSlider : UserControl
    {
        public CustomSlider()
        {
            InitializeComponent();
            pnlBar.MouseWheel += new MouseEventHandler(pnlBar_MouseWheel);
            pnlRoot.MouseWheel += new MouseEventHandler(pnlRoot_MouseWheel);

            pnlRoot.MouseDown += new System.Windows.Forms.MouseEventHandler(pnlRoot_MouseDown);
            pnlRoot.MouseEnter += new System.EventHandler(pnlRoot_MouseEnter);
            pnlRoot.MouseMove += new System.Windows.Forms.MouseEventHandler(pnlRoot_MouseMove);
        }

        

        public delegate void SliderMovedEventHandler(object tag, int value);
        public event SliderMovedEventHandler SliderMoved = null;

        public delegate void ValueChangedEventHandler(object tag, int value);
        public event ValueChangedEventHandler ValueChanged = null;

        public Action<string> DebugMessageDelegate;

        private void DebugMessage(string message)
        {
            if (DebugMessageDelegate != null)
                DebugMessageDelegate(message);
        }

        private int _valueMax = 100;
        private int _valueMin = 0;
        private int _value;

        private int _valueSteps = 15;

        public int ValueSteps
        {
            get { return _valueSteps; }

            set { _valueSteps = value; }
        }

        public int ValueStepSize
        {
            get { return ValueRange / _valueSteps; }
        }

        public int ValueRange
        {
            get { return (_valueMax - _valueMin); }
        }

        [Description("The max value"), Category("Data")]
        public int ValueMax
        {
            get { return _valueMax; }
            set
            {
                _valueMax = value;
                Value = _value;
            }
        }
        [Description("the min value"), Category("Data")]
        public int ValueMin
        {
            get { return _valueMin; }
            set
            {
                _valueMin = value;
                Value = _value;
            }
        }

        [Description("Current value"), Category("Data")]
        public int Value
        {
            get
            {
                
                return _value;
            }
            set
            {
                _value = value;
                if (_value < _valueMin) _value = _valueMin;
                else if (_value > _valueMax) _value = _valueMax;
                SetBarFromValue();
                if (ValueChanged != null) ValueChanged(this.Tag, _value);
                lblValue.Text = _value.ToString();
            }
        }

        [Description("color of slider"), Category("Data")]
        public Color SliderBarColor
        {
            get { return pnlBar.BackColor; }
            set { pnlBar.BackColor = value; }
        }

        private void SetBarFromValue()
        {
            SetBarByPosition(pnlRoot.Height - ((_value - _valueMin) * pnlRoot.Height) / ValueRange);
        }

        private void SetBarByPosition(int rootPos)
        {
            if (rootPos < 0) rootPos = 0;
            else if (rootPos >= pnlRoot.Height) rootPos = pnlRoot.Height;

            pnlBar.Height = pnlRoot.Height - rootPos;
            pnlBar.Top = rootPos;
        }


        private void SliderUserMove(int rootPos)
        {
            SetBarByPosition(rootPos);
            // calculate value from slider GUI position
            _value = _valueMin + (ValueRange * (pnlRoot.Height - pnlBar.Top)) / pnlRoot.Height;
            lblValue.Text = _value.ToString();

            if (ValueChanged != null) ValueChanged(this.Tag, _value);
            SliderMoved(this.Tag, _value);
        }

        private void pnlBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (SliderMoved == null) return; // if the event is not set then this slider is output only
            if (e.Button != MouseButtons.Left) return;

            SliderUserMove(pnlBar.Top + e.Y);
        }

        private void pnlBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (SliderMoved == null) return; // if the event is not set then this slider is output only
            if (e.Button != MouseButtons.Left) return;

            SliderUserMove(pnlBar.Top + e.Y);
        }

        private void pnlRoot_MouseDown(object sender, MouseEventArgs e)
        {
            if (SliderMoved == null) return; // if the event is not set then this slider is output only
            if (e.Button != MouseButtons.Left) return;

            SliderUserMove(e.Y);
        }

        private void pnlRoot_MouseMove(object sender, MouseEventArgs e)
        {
            if (SliderMoved == null) return; // if the event is not set then this slider is output only
            if (e.Button != MouseButtons.Left) return;

            SliderUserMove(e.Y);
        }

        private void pnlRoot_MouseWheel(object sender, MouseEventArgs e)
        {
            if (SliderMoved == null) return; // if the event is not set then this slider is output only
            if (e.Delta == 0) return;

            MouseScroll_ChangeValue(e.Delta);
            SliderMoved(this.Tag, Value);
        }

        private void pnlBar_MouseWheel(object sender, MouseEventArgs e)
        {
            if (SliderMoved == null) return; // if the event is not set then this slider is output only
            if (e.Delta == 0) return;

            MouseScroll_ChangeValue(e.Delta);
            SliderMoved(this.Tag, Value);
        }

        private void CustomSlider_Resize(object sender, EventArgs e)
        {
            SetBarFromValue();
        }

        private void MouseScroll_ChangeValue(int delta)
        {
            if (delta < 0) delta = -1;
            else delta = 1;
            Value = _value + delta * ValueStepSize;
        }

        private void CustomSlider_KeyUp(object sender, KeyEventArgs e)
        {
            if (SliderMoved == null) return; // if the event is not set then this slider is output only

            if (e.KeyCode == Keys.Up)
                Value = _value + ValueStepSize;
            else if (e.KeyCode == Keys.Down)
                Value = _value - ValueStepSize;
        }

        private void pnlRoot_MouseEnter(object sender, EventArgs e)
        {
            pnlRoot.Focus();
        }

        private void pnlBar_MouseEnter(object sender, EventArgs e)
        {
            this.Focus();
        }
    }
}
