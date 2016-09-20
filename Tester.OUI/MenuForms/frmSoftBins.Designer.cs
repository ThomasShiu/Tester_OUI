namespace Tester.OUI
{
    partial class frmSoftBins
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSoftBins));
            this.lstSoftBins = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // lstSoftBins
            // 
            this.lstSoftBins.FormattingEnabled = true;
            this.lstSoftBins.Location = new System.Drawing.Point(32, 10);
            this.lstSoftBins.Name = "lstSoftBins";
            this.lstSoftBins.Size = new System.Drawing.Size(356, 290);
            this.lstSoftBins.TabIndex = 0;
            // 
            // frmSoftBins
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(468, 315);
            this.Controls.Add(this.lstSoftBins);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSoftBins";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmSoftBins";
            this.Load += new System.EventHandler(this.frmSecsGem_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstSoftBins;

    }
}