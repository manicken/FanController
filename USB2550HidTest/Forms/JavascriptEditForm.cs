using FastColoredTextBoxNS;
using System.Text.RegularExpressions;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace USB2550HidTest.Forms
{
    public partial class JavascriptEditForm : Form
    {
        public delegate void SaveApplyEventHandler(string script);
        public event SaveApplyEventHandler SaveApply = null;

        TextStyle BlueStyle = new TextStyle(Brushes.Blue, null, FontStyle.Regular);
        TextStyle BoldStyle = new TextStyle(null, null, FontStyle.Bold | FontStyle.Underline);
        TextStyle GrayStyle = new TextStyle(Brushes.Gray, null, FontStyle.Regular);
        TextStyle MagentaStyle = new TextStyle(Brushes.Magenta, null, FontStyle.Regular);
        TextStyle GreenStyle = new TextStyle(Brushes.Green, null, FontStyle.Italic);
        TextStyle BrownStyle = new TextStyle(Brushes.Brown, null, FontStyle.Italic);
        TextStyle MaroonStyle = new TextStyle(Brushes.Maroon, null, FontStyle.Regular);
        MarkerStyle SameWordsStyle = new MarkerStyle(new SolidBrush(Color.FromArgb(40, Color.Gray)));

        public JavascriptEditForm()
        {
            InitializeComponent();
        }

        public void Show(string script)
        {
            fastColoredTextBox.Text = script;
            this.Show();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="script"></param>
        /// <param name="linePos">the first line index is 1</param>
        /// <param name="colPos">the first column index is 1</param>
        public void Show(string script, int linePos, int colPos, string logMessage)
        {
            fastColoredTextBox.Text = script;

            SelectCharAtPos(linePos, colPos, logMessage);

            this.Show();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="linePos">the first line index is 1</param>
        /// <param name="colPos">the first column index is 1</param>
        public void SelectCharAtPos(int linePos, int colPos, string logMessage)
        {
            FastColoredTextBoxNS.Range range = fastColoredTextBox.GetLine(linePos);
            range.Start = new FastColoredTextBoxNS.Place(colPos-1, linePos-1);
            range.End = new FastColoredTextBoxNS.Place(colPos , linePos-1);
            fastColoredTextBox.Selection = range;

            rtxtLog.AppendText(logMessage + "\n");
        }

        private void JavascriptEditForm_Shown(object sender, EventArgs e)
        {
             btnSaveApply.Enabled = (SaveApply != null);
        }

        private void btnSaveApply_Click(object sender, EventArgs e)
        {
            SaveApply(fastColoredTextBox.Text);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void JavascriptEditForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Visible = false;
            }
        }

        public void AppendText(string text)
        {
            fastColoredTextBox.SelectedText = text;
        }

        private void btnClearLog_Click(object sender, EventArgs e)
        {
            rtxtLog.Clear();
            rtxtLog.ClearUndo();
        }

        private void fastColoredTextBox_TextChanged(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
        {
            //old edition
            //Range range = e.ChangedRange;

            //new edition
            Range range = fastColoredTextBox.VisibleRange;//or (sender as 
                                                                      //FastColoredTextBox).Range

            //clear style of changed range
            range.ClearStyle(GreenStyle);
            //comment highlighting
            range.SetStyle(GreenStyle, @"//.*$", RegexOptions.Multiline);
            range.SetStyle(GreenStyle, @"(/\*.*?\*/)|(/\*.*)", RegexOptions.Singleline);
            range.SetStyle(GreenStyle, @"(/\*.*?\*/)|(.*\*/)", RegexOptions.Singleline |
                        RegexOptions.RightToLeft);
        }
    }
    
    
}
