namespace Tester.OUI
{
    partial class frmSecsGem
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSecsGem));
            this.chkHandlerPort = new System.Windows.Forms.CheckBox();
            this.chkHandlerIp = new System.Windows.Forms.CheckBox();
            this.txtHandlerPort = new System.Windows.Forms.TextBox();
            this.txtHandlerIP = new System.Windows.Forms.TextBox();
            this.txtConnectState = new System.Windows.Forms.TextBox();
            this.btnShowSecsGem = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // chkHandlerPort
            // 
            this.chkHandlerPort.AutoSize = true;
            this.chkHandlerPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkHandlerPort.Location = new System.Drawing.Point(97, 118);
            this.chkHandlerPort.Name = "chkHandlerPort";
            this.chkHandlerPort.Size = new System.Drawing.Size(97, 17);
            this.chkHandlerPort.TabIndex = 91;
            this.chkHandlerPort.Text = "Handler Port";
            this.chkHandlerPort.UseVisualStyleBackColor = true;
            this.chkHandlerPort.CheckedChanged += new System.EventHandler(this.chkHandlerPort_CheckedChanged);
            // 
            // chkHandlerIp
            // 
            this.chkHandlerIp.AutoSize = true;
            this.chkHandlerIp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkHandlerIp.Location = new System.Drawing.Point(97, 86);
            this.chkHandlerIp.Name = "chkHandlerIp";
            this.chkHandlerIp.Size = new System.Drawing.Size(86, 17);
            this.chkHandlerIp.TabIndex = 90;
            this.chkHandlerIp.Text = "Handler IP";
            this.chkHandlerIp.UseVisualStyleBackColor = true;
            this.chkHandlerIp.CheckedChanged += new System.EventHandler(this.chkHandlerIp_CheckedChanged);
            // 
            // txtHandlerPort
            // 
            this.txtHandlerPort.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtHandlerPort.Location = new System.Drawing.Point(207, 119);
            this.txtHandlerPort.Name = "txtHandlerPort";
            this.txtHandlerPort.ReadOnly = true;
            this.txtHandlerPort.Size = new System.Drawing.Size(123, 20);
            this.txtHandlerPort.TabIndex = 89;
            // 
            // txtHandlerIP
            // 
            this.txtHandlerIP.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtHandlerIP.Location = new System.Drawing.Point(207, 81);
            this.txtHandlerIP.Name = "txtHandlerIP";
            this.txtHandlerIP.ReadOnly = true;
            this.txtHandlerIP.Size = new System.Drawing.Size(123, 20);
            this.txtHandlerIP.TabIndex = 88;
            // 
            // txtConnectState
            // 
            this.txtConnectState.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtConnectState.Location = new System.Drawing.Point(207, 46);
            this.txtConnectState.Name = "txtConnectState";
            this.txtConnectState.ReadOnly = true;
            this.txtConnectState.Size = new System.Drawing.Size(123, 20);
            this.txtConnectState.TabIndex = 87;
            // 
            // btnShowSecsGem
            // 
            this.btnShowSecsGem.BackColor = System.Drawing.Color.Gainsboro;
            this.btnShowSecsGem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnShowSecsGem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnShowSecsGem.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnShowSecsGem.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnShowSecsGem.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnShowSecsGem.Location = new System.Drawing.Point(130, 174);
            this.btnShowSecsGem.Name = "btnShowSecsGem";
            this.btnShowSecsGem.Size = new System.Drawing.Size(170, 26);
            this.btnShowSecsGem.TabIndex = 86;
            this.btnShowSecsGem.Text = "Save SECS Config";
            this.btnShowSecsGem.UseVisualStyleBackColor = false;
            this.btnShowSecsGem.Click += new System.EventHandler(this.btnShowSecsGem_Click);
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label12.Location = new System.Drawing.Point(96, 42);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(131, 24);
            this.label12.TabIndex = 85;
            this.label12.Text = "Connect State :";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmSecsGem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(468, 225);
            this.Controls.Add(this.chkHandlerPort);
            this.Controls.Add(this.chkHandlerIp);
            this.Controls.Add(this.txtHandlerPort);
            this.Controls.Add(this.txtHandlerIP);
            this.Controls.Add(this.txtConnectState);
            this.Controls.Add(this.btnShowSecsGem);
            this.Controls.Add(this.label12);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSecsGem";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmSecsGem";
            this.Load += new System.EventHandler(this.frmSecsGem_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkHandlerPort;
        private System.Windows.Forms.CheckBox chkHandlerIp;
        private System.Windows.Forms.TextBox txtHandlerPort;
        private System.Windows.Forms.TextBox txtHandlerIP;
        private System.Windows.Forms.TextBox txtConnectState;
        private System.Windows.Forms.Button btnShowSecsGem;
        private System.Windows.Forms.Label label12;
    }
}