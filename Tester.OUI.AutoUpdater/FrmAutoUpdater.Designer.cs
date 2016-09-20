namespace TesterMI.AutoUpdater
{
    partial class FrmAutoUpdater
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAutoUpdater));
            this.lblUpdateInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblUpdateInfo
            // 
            this.lblUpdateInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblUpdateInfo.ForeColor = System.Drawing.Color.Blue;
            this.lblUpdateInfo.Location = new System.Drawing.Point(0, 0);
            this.lblUpdateInfo.Name = "lblUpdateInfo";
            this.lblUpdateInfo.Size = new System.Drawing.Size(444, 75);
            this.lblUpdateInfo.TabIndex = 0;
            this.lblUpdateInfo.Text = "Tester OUI AutoUpdater is starting......";
            this.lblUpdateInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmAutoUpdater
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 75);
            this.Controls.Add(this.lblUpdateInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAutoUpdater";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tester OUI AutoUpdater";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmAutoUpdater_FormClosing);
            this.Load += new System.EventHandler(this.FrmAutoUpdater_Load);
            this.Shown += new System.EventHandler(this.FrmAutoUpdater_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblUpdateInfo;
    }
}

