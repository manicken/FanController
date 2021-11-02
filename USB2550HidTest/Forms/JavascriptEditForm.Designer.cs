namespace USB2550HidTest.Forms
{
    partial class JavascriptEditForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JavascriptEditForm));
            this.fastColoredTextBox = new FastColoredTextBoxNS.FastColoredTextBox();
            this.btnSaveApply = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.rtxtLog = new System.Windows.Forms.RichTextBox();
            this.btnClearLog = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.fastColoredTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // fastColoredTextBox
            // 
            this.fastColoredTextBox.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.fastColoredTextBox.AutoIndentCharsPatterns = "\n^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>[^;]+);\n";
            this.fastColoredTextBox.AutoScrollMinSize = new System.Drawing.Size(627, 1106);
            this.fastColoredTextBox.BackBrush = null;
            this.fastColoredTextBox.BracketsHighlightStrategy = FastColoredTextBoxNS.BracketsHighlightStrategy.Strategy2;
            this.fastColoredTextBox.CharHeight = 14;
            this.fastColoredTextBox.CharWidth = 8;
            this.fastColoredTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.fastColoredTextBox.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.fastColoredTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fastColoredTextBox.IsReplaceMode = false;
            this.fastColoredTextBox.Language = FastColoredTextBoxNS.Language.JS;
            this.fastColoredTextBox.LeftBracket = '(';
            this.fastColoredTextBox.LeftBracket2 = '{';
            this.fastColoredTextBox.Location = new System.Drawing.Point(0, 0);
            this.fastColoredTextBox.Name = "fastColoredTextBox";
            this.fastColoredTextBox.Paddings = new System.Windows.Forms.Padding(0);
            this.fastColoredTextBox.RightBracket = ')';
            this.fastColoredTextBox.RightBracket2 = '}';
            this.fastColoredTextBox.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.fastColoredTextBox.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("fastColoredTextBox.ServiceColors")));
            this.fastColoredTextBox.Size = new System.Drawing.Size(804, 499);
            this.fastColoredTextBox.TabIndex = 1;
            this.fastColoredTextBox.Text = resources.GetString("fastColoredTextBox.Text");
            this.fastColoredTextBox.Zoom = 100;
            this.fastColoredTextBox.TextChanged += new System.EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(this.fastColoredTextBox_TextChanged);
            // 
            // btnSaveApply
            // 
            this.btnSaveApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveApply.Location = new System.Drawing.Point(618, 629);
            this.btnSaveApply.Name = "btnSaveApply";
            this.btnSaveApply.Size = new System.Drawing.Size(109, 23);
            this.btnSaveApply.TabIndex = 2;
            this.btnSaveApply.Text = "Save and Apply";
            this.btnSaveApply.UseVisualStyleBackColor = true;
            this.btnSaveApply.Click += new System.EventHandler(this.btnSaveApply_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(741, 629);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 12);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.fastColoredTextBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.rtxtLog);
            this.splitContainer1.Size = new System.Drawing.Size(804, 611);
            this.splitContainer1.SplitterDistance = 499;
            this.splitContainer1.SplitterWidth = 8;
            this.splitContainer1.TabIndex = 4;
            // 
            // rtxtLog
            // 
            this.rtxtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxtLog.Location = new System.Drawing.Point(0, 0);
            this.rtxtLog.Name = "rtxtLog";
            this.rtxtLog.Size = new System.Drawing.Size(804, 104);
            this.rtxtLog.TabIndex = 0;
            this.rtxtLog.Text = "";
            // 
            // btnClearLog
            // 
            this.btnClearLog.Location = new System.Drawing.Point(12, 629);
            this.btnClearLog.Name = "btnClearLog";
            this.btnClearLog.Size = new System.Drawing.Size(75, 23);
            this.btnClearLog.TabIndex = 5;
            this.btnClearLog.Text = "clear log";
            this.btnClearLog.UseVisualStyleBackColor = true;
            this.btnClearLog.Click += new System.EventHandler(this.btnClearLog_Click);
            // 
            // JavascriptEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(828, 664);
            this.Controls.Add(this.btnClearLog);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSaveApply);
            this.Name = "JavascriptEditForm";
            this.Text = "JavascriptEditForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.JavascriptEditForm_FormClosing);
            this.Shown += new System.EventHandler(this.JavascriptEditForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.fastColoredTextBox)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private FastColoredTextBoxNS.FastColoredTextBox fastColoredTextBox;
        private System.Windows.Forms.Button btnSaveApply;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.RichTextBox rtxtLog;
        private System.Windows.Forms.Button btnClearLog;
    }
}