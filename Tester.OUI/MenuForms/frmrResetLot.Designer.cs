namespace Tester.OUI
{
    partial class frmResetLot
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmResetLot));
            this.txtLotNo = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comb_Resetreason = new System.Windows.Forms.ComboBox();
            this.btnConfrim = new System.Windows.Forms.Button();
            this.btnCancle = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtLotNo
            // 
            this.txtLotNo.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtLotNo.Location = new System.Drawing.Point(207, 46);
            this.txtLotNo.Name = "txtLotNo";
            this.txtLotNo.ReadOnly = true;
            this.txtLotNo.Size = new System.Drawing.Size(123, 20);
            this.txtLotNo.TabIndex = 87;
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label12.Location = new System.Drawing.Point(96, 42);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(131, 24);
            this.label12.TabIndex = 85;
            this.label12.Text = "Lot No :";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(87, 109);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 24);
            this.label3.TabIndex = 93;
            this.label3.Text = "Reason :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // comb_Resetreason
            // 
            this.comb_Resetreason.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.comb_Resetreason.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comb_Resetreason.FormattingEnabled = true;
            this.comb_Resetreason.Location = new System.Drawing.Point(207, 111);
            this.comb_Resetreason.Name = "comb_Resetreason";
            this.comb_Resetreason.Size = new System.Drawing.Size(121, 21);
            this.comb_Resetreason.TabIndex = 94;
            // 
            // btnConfrim
            // 
            this.btnConfrim.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnConfrim.Location = new System.Drawing.Point(99, 190);
            this.btnConfrim.Name = "btnConfrim";
            this.btnConfrim.Size = new System.Drawing.Size(100, 23);
            this.btnConfrim.TabIndex = 95;
            this.btnConfrim.Text = "确认";
            this.btnConfrim.UseVisualStyleBackColor = true;
            this.btnConfrim.Click += new System.EventHandler(this.btnConfrim_Click);
            // 
            // btnCancle
            // 
            this.btnCancle.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancle.Location = new System.Drawing.Point(247, 190);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(100, 23);
            this.btnCancle.TabIndex = 96;
            this.btnCancle.Text = "取消";
            this.btnCancle.UseVisualStyleBackColor = true;
            // 
            // frmResetLot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(468, 225);
            this.Controls.Add(this.btnCancle);
            this.Controls.Add(this.btnConfrim);
            this.Controls.Add(this.comb_Resetreason);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtLotNo);
            this.Controls.Add(this.label12);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmResetLot";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reset Current Lot";
            this.Load += new System.EventHandler(this.frmResetLot_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtLotNo;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comb_Resetreason;
        private System.Windows.Forms.Button btnConfrim;
        private System.Windows.Forms.Button btnCancle;
    }
}