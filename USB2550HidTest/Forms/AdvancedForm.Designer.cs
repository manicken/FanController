namespace USB2550HidTest.Forms
{
    partial class AdvancedForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdvancedForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.cmbRegSel = new System.Windows.Forms.ComboBox();
            this.chkBoxAutoRefresh = new System.Windows.Forms.CheckBox();
            this.btnSetData = new System.Windows.Forms.Button();
            this.btnGetData = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbtnOpenFile = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.btnGetLastCaptureValues = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.dgv);
            this.panel1.Controls.Add(this.cmbRegSel);
            this.panel1.Controls.Add(this.chkBoxAutoRefresh);
            this.panel1.Controls.Add(this.btnSetData);
            this.panel1.Controls.Add(this.btnGetData);
            this.panel1.Location = new System.Drawing.Point(2, 42);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(312, 534);
            this.panel1.TabIndex = 28;
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AllowUserToResizeRows = false;
            this.dgv.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Location = new System.Drawing.Point(3, 32);
            this.dgv.Name = "dgv";
            this.dgv.Size = new System.Drawing.Size(303, 497);
            this.dgv.TabIndex = 0;
            this.dgv.DataSourceChanged += new System.EventHandler(this.dgv_DataSourceChanged);
            // 
            // cmbRegSel
            // 
            this.cmbRegSel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRegSel.FormattingEnabled = true;
            this.cmbRegSel.Location = new System.Drawing.Point(5, 5);
            this.cmbRegSel.Name = "cmbRegSel";
            this.cmbRegSel.Size = new System.Drawing.Size(81, 21);
            this.cmbRegSel.TabIndex = 25;
            this.cmbRegSel.SelectedIndexChanged += new System.EventHandler(this.cmbRegSel_SelectedIndexChanged);
            // 
            // chkBoxAutoRefresh
            // 
            this.chkBoxAutoRefresh.AutoSize = true;
            this.chkBoxAutoRefresh.Location = new System.Drawing.Point(92, 7);
            this.chkBoxAutoRefresh.Name = "chkBoxAutoRefresh";
            this.chkBoxAutoRefresh.Size = new System.Drawing.Size(82, 17);
            this.chkBoxAutoRefresh.TabIndex = 26;
            this.chkBoxAutoRefresh.Text = "auto refresh";
            this.chkBoxAutoRefresh.UseVisualStyleBackColor = true;
            // 
            // btnSetData
            // 
            this.btnSetData.Location = new System.Drawing.Point(246, 3);
            this.btnSetData.Name = "btnSetData";
            this.btnSetData.Size = new System.Drawing.Size(60, 23);
            this.btnSetData.TabIndex = 24;
            this.btnSetData.Text = "SetData";
            this.btnSetData.UseVisualStyleBackColor = true;
            this.btnSetData.Click += new System.EventHandler(this.btnSetData_Click);
            // 
            // btnGetData
            // 
            this.btnGetData.Location = new System.Drawing.Point(180, 3);
            this.btnGetData.Name = "btnGetData";
            this.btnGetData.Size = new System.Drawing.Size(60, 23);
            this.btnGetData.TabIndex = 24;
            this.btnGetData.Text = "GetData";
            this.btnGetData.UseVisualStyleBackColor = true;
            this.btnGetData.Click += new System.EventHandler(this.btnGetData_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnOpenFile,
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(615, 39);
            this.toolStrip1.TabIndex = 29;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbtnOpenFile
            // 
            this.tsbtnOpenFile.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnOpenFile.Image")));
            this.tsbtnOpenFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnOpenFile.Name = "tsbtnOpenFile";
            this.tsbtnOpenFile.Size = new System.Drawing.Size(93, 36);
            this.tsbtnOpenFile.Text = "Open File";
            this.tsbtnOpenFile.Click += new System.EventHandler(this.btnLoadFile_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(127, 36);
            this.toolStripButton1.Text = "Save To File As..";
            this.toolStripButton1.Click += new System.EventHandler(this.btnSaveFile_Click);
            // 
            // btnGetLastCaptureValues
            // 
            this.btnGetLastCaptureValues.Location = new System.Drawing.Point(320, 42);
            this.btnGetLastCaptureValues.Name = "btnGetLastCaptureValues";
            this.btnGetLastCaptureValues.Size = new System.Drawing.Size(148, 23);
            this.btnGetLastCaptureValues.TabIndex = 30;
            this.btnGetLastCaptureValues.Text = "get last IC capture values";
            this.btnGetLastCaptureValues.UseVisualStyleBackColor = true;
            this.btnGetLastCaptureValues.Click += new System.EventHandler(this.btnGetLastCaptureValues_Click);
            // 
            // AdvancedForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(615, 577);
            this.Controls.Add(this.btnGetLastCaptureValues);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.panel1);
            this.Name = "AdvancedForm";
            this.Text = "frmAdvanced";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AdvancedForm_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.ComboBox cmbRegSel;
        private System.Windows.Forms.CheckBox chkBoxAutoRefresh;
        private System.Windows.Forms.Button btnSetData;
        private System.Windows.Forms.Button btnGetData;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbtnOpenFile;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.Button btnGetLastCaptureValues;
    }
}