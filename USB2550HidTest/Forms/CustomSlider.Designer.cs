namespace USB2550HidTest.Forms
{
    partial class CustomSlider
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlBar = new System.Windows.Forms.Panel();
            this.pnlRoot = new System.Windows.Forms.Panel();
            this.lblValue = new System.Windows.Forms.Label();
            this.pnlRoot.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBar
            // 
            this.pnlBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlBar.BackColor = System.Drawing.Color.Lime;
            this.pnlBar.Location = new System.Drawing.Point(0, 114);
            this.pnlBar.Name = "pnlBar";
            this.pnlBar.Size = new System.Drawing.Size(53, 145);
            this.pnlBar.TabIndex = 0;
            this.pnlBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlBar_MouseDown);
            this.pnlBar.MouseEnter += new System.EventHandler(this.pnlBar_MouseEnter);
            this.pnlBar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlBar_MouseMove);
            // 
            // pnlRoot
            // 
            this.pnlRoot.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlRoot.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlRoot.Controls.Add(this.pnlBar);
            this.pnlRoot.Location = new System.Drawing.Point(3, 3);
            this.pnlRoot.Name = "pnlRoot";
            this.pnlRoot.Size = new System.Drawing.Size(55, 292);
            this.pnlRoot.TabIndex = 1;
            // 
            // lblValue
            // 
            this.lblValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblValue.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValue.Location = new System.Drawing.Point(-3, 298);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(67, 14);
            this.lblValue.TabIndex = 2;
            this.lblValue.Text = "0";
            this.lblValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CustomSlider
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblValue);
            this.Controls.Add(this.pnlRoot);
            this.Name = "CustomSlider";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Size = new System.Drawing.Size(61, 317);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.CustomSlider_KeyUp);
            this.Resize += new System.EventHandler(this.CustomSlider_Resize);
            this.pnlRoot.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlBar;
        private System.Windows.Forms.Panel pnlRoot;
        private System.Windows.Forms.Label lblValue;
    }
}
