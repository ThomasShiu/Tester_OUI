namespace Tester.OUI
{
    partial class FrmTesterOUI_ST
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTesterOUI_ST));
            this.btnReTest = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSecsGemConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.menuClearlot = new System.Windows.Forms.ToolStripMenuItem();
            this.menuData = new System.Windows.Forms.ToolStripMenuItem();
            this.menuLotInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSoftBins = new System.Windows.Forms.ToolStripMenuItem();
            this.menuLogOut = new System.Windows.Forms.ToolStripMenuItem();
            this.grpHardBins = new System.Windows.Forms.GroupBox();
            this.txtMark_Count = new System.Windows.Forms.TextBox();
            this.chkVision_Reject = new System.Windows.Forms.CheckBox();
            this.txtVision_Reject = new System.Windows.Forms.TextBox();
            this.chkIso_Reject = new System.Windows.Forms.CheckBox();
            this.txtIso_Reject = new System.Windows.Forms.TextBox();
            this.btnRetry = new System.Windows.Forms.Button();
            this.chkHardBin7 = new System.Windows.Forms.CheckBox();
            this.chkHardBin6 = new System.Windows.Forms.CheckBox();
            this.chkHardBin5 = new System.Windows.Forms.CheckBox();
            this.chkHardBin4 = new System.Windows.Forms.CheckBox();
            this.chkHardBin3 = new System.Windows.Forms.CheckBox();
            this.chkHardBin2 = new System.Windows.Forms.CheckBox();
            this.txtHardBin5 = new System.Windows.Forms.TextBox();
            this.txtHardBin7 = new System.Windows.Forms.TextBox();
            this.txtHardBin6 = new System.Windows.Forms.TextBox();
            this.txtHardBin4 = new System.Windows.Forms.TextBox();
            this.txtHardBin3 = new System.Windows.Forms.TextBox();
            this.txtHardBin2 = new System.Windows.Forms.TextBox();
            this.txtHardBin1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.ssFooter = new System.Windows.Forms.StatusStrip();
            this.siUserName = new System.Windows.Forms.ToolStripStatusLabel();
            this.siDepartment = new System.Windows.Forms.ToolStripStatusLabel();
            this.siHostName = new System.Windows.Forms.ToolStripStatusLabel();
            this.siIPAddress = new System.Windows.Forms.ToolStripStatusLabel();
            this.siMachineModel = new System.Windows.Forms.ToolStripStatusLabel();
            this.siHandlerModel = new System.Windows.Forms.ToolStripStatusLabel();
            this.grpRuntimeInfo = new System.Windows.Forms.GroupBox();
            this.lstRuntimeInfo = new System.Windows.Forms.ListBox();
            this.grpEquipmentStatus = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnEndLot = new System.Windows.Forms.Button();
            this.txtLotNo = new System.Windows.Forms.TextBox();
            this.TmrDownloadRecipe = new System.Windows.Forms.Timer(this.components);
            this.TmrRetry = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            this.grpHardBins.SuspendLayout();
            this.ssFooter.SuspendLayout();
            this.grpRuntimeInfo.SuspendLayout();
            this.grpEquipmentStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnReTest
            // 
            this.btnReTest.BackColor = System.Drawing.Color.LightGray;
            this.btnReTest.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReTest.ForeColor = System.Drawing.Color.Red;
            this.btnReTest.Location = new System.Drawing.Point(356, 247);
            this.btnReTest.Name = "btnReTest";
            this.btnReTest.Size = new System.Drawing.Size(115, 31);
            this.btnReTest.TabIndex = 0;
            this.btnReTest.Text = "Re Test";
            this.btnReTest.UseVisualStyleBackColor = false;
            this.btnReTest.Click += new System.EventHandler(this.btnReTest_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuSettings,
            this.menuData,
            this.menuLogOut});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(935, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuSettings
            // 
            this.menuSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuSecsGemConfig,
            this.menuClearlot});
            this.menuSettings.Name = "menuSettings";
            this.menuSettings.Size = new System.Drawing.Size(61, 20);
            this.menuSettings.Text = "Settings";
            // 
            // menuSecsGemConfig
            // 
            this.menuSecsGemConfig.Name = "menuSecsGemConfig";
            this.menuSecsGemConfig.Size = new System.Drawing.Size(167, 22);
            this.menuSecsGemConfig.Text = "SecsGem Settings";
            this.menuSecsGemConfig.Click += new System.EventHandler(this.menuSecsGemConfig_Click);
            // 
            // menuClearlot
            // 
            this.menuClearlot.Name = "menuClearlot";
            this.menuClearlot.Size = new System.Drawing.Size(167, 22);
            this.menuClearlot.Text = "Reset Lot";
            this.menuClearlot.Click += new System.EventHandler(this.menuClearlot_Click);
            // 
            // menuData
            // 
            this.menuData.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuLotInfo,
            this.menuSoftBins});
            this.menuData.Name = "menuData";
            this.menuData.Size = new System.Drawing.Size(43, 20);
            this.menuData.Text = "Data";
            // 
            // menuLotInfo
            // 
            this.menuLotInfo.Name = "menuLotInfo";
            this.menuLotInfo.Size = new System.Drawing.Size(120, 22);
            this.menuLotInfo.Text = "Lot Info";
            this.menuLotInfo.Click += new System.EventHandler(this.menuLotInfo_Click);
            // 
            // menuSoftBins
            // 
            this.menuSoftBins.Name = "menuSoftBins";
            this.menuSoftBins.Size = new System.Drawing.Size(120, 22);
            this.menuSoftBins.Text = "Soft Bins";
            this.menuSoftBins.Click += new System.EventHandler(this.menuSoftBins_Click);
            // 
            // menuLogOut
            // 
            this.menuLogOut.Name = "menuLogOut";
            this.menuLogOut.Size = new System.Drawing.Size(62, 20);
            this.menuLogOut.Text = "Log Out";
            this.menuLogOut.Click += new System.EventHandler(this.menuLogOut_Click);
            // 
            // grpHardBins
            // 
            this.grpHardBins.Controls.Add(this.txtMark_Count);
            this.grpHardBins.Controls.Add(this.chkVision_Reject);
            this.grpHardBins.Controls.Add(this.txtVision_Reject);
            this.grpHardBins.Controls.Add(this.chkIso_Reject);
            this.grpHardBins.Controls.Add(this.txtIso_Reject);
            this.grpHardBins.Controls.Add(this.btnRetry);
            this.grpHardBins.Controls.Add(this.chkHardBin7);
            this.grpHardBins.Controls.Add(this.btnReTest);
            this.grpHardBins.Controls.Add(this.chkHardBin6);
            this.grpHardBins.Controls.Add(this.chkHardBin5);
            this.grpHardBins.Controls.Add(this.chkHardBin4);
            this.grpHardBins.Controls.Add(this.chkHardBin3);
            this.grpHardBins.Controls.Add(this.chkHardBin2);
            this.grpHardBins.Controls.Add(this.txtHardBin5);
            this.grpHardBins.Controls.Add(this.txtHardBin7);
            this.grpHardBins.Controls.Add(this.txtHardBin6);
            this.grpHardBins.Controls.Add(this.txtHardBin4);
            this.grpHardBins.Controls.Add(this.txtHardBin3);
            this.grpHardBins.Controls.Add(this.txtHardBin2);
            this.grpHardBins.Controls.Add(this.txtHardBin1);
            this.grpHardBins.Controls.Add(this.label4);
            this.grpHardBins.Controls.Add(this.label2);
            this.grpHardBins.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpHardBins.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grpHardBins.ForeColor = System.Drawing.Color.White;
            this.grpHardBins.Location = new System.Drawing.Point(0, 99);
            this.grpHardBins.Name = "grpHardBins";
            this.grpHardBins.Size = new System.Drawing.Size(935, 294);
            this.grpHardBins.TabIndex = 62;
            this.grpHardBins.TabStop = false;
            this.grpHardBins.Text = "Handler Bin Data";
            // 
            // txtMark_Count
            // 
            this.txtMark_Count.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtMark_Count.Location = new System.Drawing.Point(596, 114);
            this.txtMark_Count.Name = "txtMark_Count";
            this.txtMark_Count.ReadOnly = true;
            this.txtMark_Count.Size = new System.Drawing.Size(102, 26);
            this.txtMark_Count.TabIndex = 110;
            // 
            // chkVision_Reject
            // 
            this.chkVision_Reject.AutoSize = true;
            this.chkVision_Reject.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkVision_Reject.Location = new System.Drawing.Point(506, 34);
            this.chkVision_Reject.Name = "chkVision_Reject";
            this.chkVision_Reject.Size = new System.Drawing.Size(88, 17);
            this.chkVision_Reject.TabIndex = 109;
            this.chkVision_Reject.Text = "Vision Reject";
            this.chkVision_Reject.UseVisualStyleBackColor = true;
            // 
            // txtVision_Reject
            // 
            this.txtVision_Reject.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtVision_Reject.Location = new System.Drawing.Point(596, 31);
            this.txtVision_Reject.Name = "txtVision_Reject";
            this.txtVision_Reject.ReadOnly = true;
            this.txtVision_Reject.Size = new System.Drawing.Size(102, 26);
            this.txtVision_Reject.TabIndex = 108;
            // 
            // chkIso_Reject
            // 
            this.chkIso_Reject.AutoSize = true;
            this.chkIso_Reject.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkIso_Reject.Location = new System.Drawing.Point(506, 78);
            this.chkIso_Reject.Name = "chkIso_Reject";
            this.chkIso_Reject.Size = new System.Drawing.Size(78, 17);
            this.chkIso_Reject.TabIndex = 107;
            this.chkIso_Reject.Text = "ISO Reject";
            this.chkIso_Reject.UseVisualStyleBackColor = true;
            // 
            // txtIso_Reject
            // 
            this.txtIso_Reject.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtIso_Reject.Location = new System.Drawing.Point(596, 71);
            this.txtIso_Reject.Name = "txtIso_Reject";
            this.txtIso_Reject.ReadOnly = true;
            this.txtIso_Reject.Size = new System.Drawing.Size(102, 26);
            this.txtIso_Reject.TabIndex = 106;
            // 
            // btnRetry
            // 
            this.btnRetry.BackColor = System.Drawing.Color.LightGray;
            this.btnRetry.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnRetry.ForeColor = System.Drawing.Color.Black;
            this.btnRetry.Location = new System.Drawing.Point(87, 247);
            this.btnRetry.Name = "btnRetry";
            this.btnRetry.Size = new System.Drawing.Size(115, 31);
            this.btnRetry.TabIndex = 65;
            this.btnRetry.Text = "Retry";
            this.btnRetry.UseVisualStyleBackColor = false;
            this.btnRetry.Click += new System.EventHandler(this.btnRetry_Click);
            // 
            // chkHardBin7
            // 
            this.chkHardBin7.AutoSize = true;
            this.chkHardBin7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkHardBin7.Location = new System.Drawing.Point(303, 74);
            this.chkHardBin7.Name = "chkHardBin7";
            this.chkHardBin7.Size = new System.Drawing.Size(47, 17);
            this.chkHardBin7.TabIndex = 103;
            this.chkHardBin7.Text = "Bin7";
            this.chkHardBin7.UseVisualStyleBackColor = true;
            // 
            // chkHardBin6
            // 
            this.chkHardBin6.AutoSize = true;
            this.chkHardBin6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkHardBin6.Location = new System.Drawing.Point(304, 33);
            this.chkHardBin6.Name = "chkHardBin6";
            this.chkHardBin6.Size = new System.Drawing.Size(47, 17);
            this.chkHardBin6.TabIndex = 102;
            this.chkHardBin6.Text = "Bin6";
            this.chkHardBin6.UseVisualStyleBackColor = true;
            // 
            // chkHardBin5
            // 
            this.chkHardBin5.AutoSize = true;
            this.chkHardBin5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkHardBin5.Location = new System.Drawing.Point(71, 190);
            this.chkHardBin5.Name = "chkHardBin5";
            this.chkHardBin5.Size = new System.Drawing.Size(47, 17);
            this.chkHardBin5.TabIndex = 101;
            this.chkHardBin5.Text = "Bin5";
            this.chkHardBin5.UseVisualStyleBackColor = true;
            // 
            // chkHardBin4
            // 
            this.chkHardBin4.AutoSize = true;
            this.chkHardBin4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkHardBin4.Location = new System.Drawing.Point(71, 152);
            this.chkHardBin4.Name = "chkHardBin4";
            this.chkHardBin4.Size = new System.Drawing.Size(47, 17);
            this.chkHardBin4.TabIndex = 100;
            this.chkHardBin4.Text = "Bin4";
            this.chkHardBin4.UseVisualStyleBackColor = true;
            // 
            // chkHardBin3
            // 
            this.chkHardBin3.AutoSize = true;
            this.chkHardBin3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkHardBin3.Location = new System.Drawing.Point(72, 114);
            this.chkHardBin3.Name = "chkHardBin3";
            this.chkHardBin3.Size = new System.Drawing.Size(47, 17);
            this.chkHardBin3.TabIndex = 99;
            this.chkHardBin3.Text = "Bin3";
            this.chkHardBin3.UseVisualStyleBackColor = true;
            // 
            // chkHardBin2
            // 
            this.chkHardBin2.AutoSize = true;
            this.chkHardBin2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkHardBin2.Location = new System.Drawing.Point(74, 76);
            this.chkHardBin2.Name = "chkHardBin2";
            this.chkHardBin2.Size = new System.Drawing.Size(47, 17);
            this.chkHardBin2.TabIndex = 98;
            this.chkHardBin2.Text = "Bin2";
            this.chkHardBin2.UseVisualStyleBackColor = true;
            // 
            // txtHardBin5
            // 
            this.txtHardBin5.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtHardBin5.Location = new System.Drawing.Point(128, 186);
            this.txtHardBin5.Name = "txtHardBin5";
            this.txtHardBin5.ReadOnly = true;
            this.txtHardBin5.Size = new System.Drawing.Size(102, 26);
            this.txtHardBin5.TabIndex = 95;
            // 
            // txtHardBin7
            // 
            this.txtHardBin7.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtHardBin7.Location = new System.Drawing.Point(356, 71);
            this.txtHardBin7.Name = "txtHardBin7";
            this.txtHardBin7.ReadOnly = true;
            this.txtHardBin7.Size = new System.Drawing.Size(102, 26);
            this.txtHardBin7.TabIndex = 91;
            // 
            // txtHardBin6
            // 
            this.txtHardBin6.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtHardBin6.Location = new System.Drawing.Point(356, 30);
            this.txtHardBin6.Name = "txtHardBin6";
            this.txtHardBin6.ReadOnly = true;
            this.txtHardBin6.Size = new System.Drawing.Size(102, 26);
            this.txtHardBin6.TabIndex = 89;
            // 
            // txtHardBin4
            // 
            this.txtHardBin4.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtHardBin4.Location = new System.Drawing.Point(128, 147);
            this.txtHardBin4.Name = "txtHardBin4";
            this.txtHardBin4.ReadOnly = true;
            this.txtHardBin4.Size = new System.Drawing.Size(102, 26);
            this.txtHardBin4.TabIndex = 87;
            // 
            // txtHardBin3
            // 
            this.txtHardBin3.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtHardBin3.Location = new System.Drawing.Point(128, 108);
            this.txtHardBin3.Name = "txtHardBin3";
            this.txtHardBin3.ReadOnly = true;
            this.txtHardBin3.Size = new System.Drawing.Size(102, 26);
            this.txtHardBin3.TabIndex = 85;
            // 
            // txtHardBin2
            // 
            this.txtHardBin2.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtHardBin2.Location = new System.Drawing.Point(128, 69);
            this.txtHardBin2.Name = "txtHardBin2";
            this.txtHardBin2.ReadOnly = true;
            this.txtHardBin2.Size = new System.Drawing.Size(102, 26);
            this.txtHardBin2.TabIndex = 83;
            // 
            // txtHardBin1
            // 
            this.txtHardBin1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtHardBin1.Location = new System.Drawing.Point(128, 30);
            this.txtHardBin1.Name = "txtHardBin1";
            this.txtHardBin1.ReadOnly = true;
            this.txtHardBin1.Size = new System.Drawing.Size(102, 26);
            this.txtHardBin1.TabIndex = 79;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(44, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 24);
            this.label4.TabIndex = 69;
            this.label4.Text = "Good Bin :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(498, 114);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 24);
            this.label2.TabIndex = 111;
            this.label2.Text = "Mark Count :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnSubmit
            // 
            this.btnSubmit.BackColor = System.Drawing.Color.LightGray;
            this.btnSubmit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnSubmit.ForeColor = System.Drawing.Color.Black;
            this.btnSubmit.Location = new System.Drawing.Point(726, 31);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(115, 31);
            this.btnSubmit.TabIndex = 63;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = false;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // ssFooter
            // 
            this.ssFooter.BackColor = System.Drawing.Color.LightGray;
            this.ssFooter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.ssFooter.GripMargin = new System.Windows.Forms.Padding(0);
            this.ssFooter.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.siUserName,
            this.siDepartment,
            this.siHostName,
            this.siIPAddress,
            this.siMachineModel,
            this.siHandlerModel});
            this.ssFooter.Location = new System.Drawing.Point(0, 543);
            this.ssFooter.Name = "ssFooter";
            this.ssFooter.ShowItemToolTips = true;
            this.ssFooter.Size = new System.Drawing.Size(935, 25);
            this.ssFooter.TabIndex = 64;
            // 
            // siUserName
            // 
            this.siUserName.AutoSize = false;
            this.siUserName.AutoToolTip = true;
            this.siUserName.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.siUserName.Margin = new System.Windows.Forms.Padding(0);
            this.siUserName.Name = "siUserName";
            this.siUserName.Size = new System.Drawing.Size(125, 25);
            this.siUserName.Text = "User Name:";
            this.siUserName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // siDepartment
            // 
            this.siDepartment.AutoSize = false;
            this.siDepartment.AutoToolTip = true;
            this.siDepartment.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.siDepartment.Margin = new System.Windows.Forms.Padding(0);
            this.siDepartment.Name = "siDepartment";
            this.siDepartment.Size = new System.Drawing.Size(125, 25);
            this.siDepartment.Text = "Department:";
            this.siDepartment.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // siHostName
            // 
            this.siHostName.AutoSize = false;
            this.siHostName.AutoToolTip = true;
            this.siHostName.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.siHostName.Margin = new System.Windows.Forms.Padding(0);
            this.siHostName.Name = "siHostName";
            this.siHostName.Size = new System.Drawing.Size(125, 25);
            this.siHostName.Text = "Host Name:";
            this.siHostName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // siIPAddress
            // 
            this.siIPAddress.AutoSize = false;
            this.siIPAddress.AutoToolTip = true;
            this.siIPAddress.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.siIPAddress.Margin = new System.Windows.Forms.Padding(0);
            this.siIPAddress.Name = "siIPAddress";
            this.siIPAddress.Size = new System.Drawing.Size(175, 25);
            this.siIPAddress.Text = "Ip Address:";
            this.siIPAddress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // siMachineModel
            // 
            this.siMachineModel.AutoSize = false;
            this.siMachineModel.AutoToolTip = true;
            this.siMachineModel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.siMachineModel.Margin = new System.Windows.Forms.Padding(0);
            this.siMachineModel.Name = "siMachineModel";
            this.siMachineModel.Size = new System.Drawing.Size(125, 25);
            this.siMachineModel.Text = "Test Model:";
            this.siMachineModel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // siHandlerModel
            // 
            this.siHandlerModel.AutoSize = false;
            this.siHandlerModel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.siHandlerModel.Name = "siHandlerModel";
            this.siHandlerModel.Size = new System.Drawing.Size(130, 20);
            this.siHandlerModel.Text = "Handler Model:";
            this.siHandlerModel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // grpRuntimeInfo
            // 
            this.grpRuntimeInfo.Controls.Add(this.lstRuntimeInfo);
            this.grpRuntimeInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grpRuntimeInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpRuntimeInfo.ForeColor = System.Drawing.Color.White;
            this.grpRuntimeInfo.Location = new System.Drawing.Point(0, 393);
            this.grpRuntimeInfo.Name = "grpRuntimeInfo";
            this.grpRuntimeInfo.Size = new System.Drawing.Size(935, 150);
            this.grpRuntimeInfo.TabIndex = 66;
            this.grpRuntimeInfo.TabStop = false;
            this.grpRuntimeInfo.Text = "Run-time Info";
            // 
            // lstRuntimeInfo
            // 
            this.lstRuntimeInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(108)))), ((int)(((byte)(134)))));
            this.lstRuntimeInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstRuntimeInfo.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.lstRuntimeInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstRuntimeInfo.ForeColor = System.Drawing.Color.White;
            this.lstRuntimeInfo.FormattingEnabled = true;
            this.lstRuntimeInfo.HorizontalScrollbar = true;
            this.lstRuntimeInfo.ItemHeight = 16;
            this.lstRuntimeInfo.Location = new System.Drawing.Point(3, 22);
            this.lstRuntimeInfo.Name = "lstRuntimeInfo";
            this.lstRuntimeInfo.ScrollAlwaysVisible = true;
            this.lstRuntimeInfo.Size = new System.Drawing.Size(929, 125);
            this.lstRuntimeInfo.TabIndex = 0;
            // 
            // grpEquipmentStatus
            // 
            this.grpEquipmentStatus.Controls.Add(this.label1);
            this.grpEquipmentStatus.Controls.Add(this.btnEndLot);
            this.grpEquipmentStatus.Controls.Add(this.btnSubmit);
            this.grpEquipmentStatus.Controls.Add(this.txtLotNo);
            this.grpEquipmentStatus.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpEquipmentStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpEquipmentStatus.ForeColor = System.Drawing.Color.White;
            this.grpEquipmentStatus.Location = new System.Drawing.Point(0, 24);
            this.grpEquipmentStatus.Name = "grpEquipmentStatus";
            this.grpEquipmentStatus.Size = new System.Drawing.Size(935, 75);
            this.grpEquipmentStatus.TabIndex = 67;
            this.grpEquipmentStatus.TabStop = false;
            this.grpEquipmentStatus.Text = "Start Lot";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(60, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 24);
            this.label1.TabIndex = 70;
            this.label1.Text = "Lot No :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnEndLot
            // 
            this.btnEndLot.BackColor = System.Drawing.Color.LightGray;
            this.btnEndLot.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnEndLot.ForeColor = System.Drawing.Color.Black;
            this.btnEndLot.Location = new System.Drawing.Point(560, 31);
            this.btnEndLot.Name = "btnEndLot";
            this.btnEndLot.Size = new System.Drawing.Size(115, 31);
            this.btnEndLot.TabIndex = 2;
            this.btnEndLot.Text = "End Lot";
            this.btnEndLot.UseVisualStyleBackColor = false;
            this.btnEndLot.Click += new System.EventHandler(this.btnEndLot_Click);
            // 
            // txtLotNo
            // 
            this.txtLotNo.Location = new System.Drawing.Point(141, 32);
            this.txtLotNo.Name = "txtLotNo";
            this.txtLotNo.Size = new System.Drawing.Size(153, 26);
            this.txtLotNo.TabIndex = 1;
            this.txtLotNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtLotNo_KeyDown);
            // 
            // TmrDownloadRecipe
            // 
            this.TmrDownloadRecipe.Interval = 12000;
            this.TmrDownloadRecipe.Tick += new System.EventHandler(this.TmrDownloadRecipe_Tick);
            // 
            // TmrRetry
            // 
            this.TmrRetry.Interval = 45000;
            // 
            // FrmTesterOUI_ST
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(108)))), ((int)(((byte)(134)))));
            this.ClientSize = new System.Drawing.Size(935, 568);
            this.Controls.Add(this.grpHardBins);
            this.Controls.Add(this.grpRuntimeInfo);
            this.Controls.Add(this.grpEquipmentStatus);
            this.Controls.Add(this.ssFooter);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmTesterOUI_ST";
            this.Text = "FrmTest";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmTest_FormClosed);
            this.Load += new System.EventHandler(this.FrmTest_Load);
            this.SizeChanged += new System.EventHandler(this.FrmTest_SizeChanged);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.grpHardBins.ResumeLayout(false);
            this.grpHardBins.PerformLayout();
            this.ssFooter.ResumeLayout(false);
            this.ssFooter.PerformLayout();
            this.grpRuntimeInfo.ResumeLayout(false);
            this.grpEquipmentStatus.ResumeLayout(false);
            this.grpEquipmentStatus.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnReTest;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuSettings;
        private System.Windows.Forms.ToolStripMenuItem menuSecsGemConfig;
        private System.Windows.Forms.ToolStripMenuItem menuClearlot;
        private System.Windows.Forms.GroupBox grpHardBins;
        private System.Windows.Forms.CheckBox chkHardBin7;
        private System.Windows.Forms.CheckBox chkHardBin6;
        private System.Windows.Forms.CheckBox chkHardBin5;
        private System.Windows.Forms.CheckBox chkHardBin4;
        private System.Windows.Forms.CheckBox chkHardBin3;
        private System.Windows.Forms.CheckBox chkHardBin2;
        private System.Windows.Forms.TextBox txtHardBin5;
        private System.Windows.Forms.TextBox txtHardBin7;
        private System.Windows.Forms.TextBox txtHardBin6;
        private System.Windows.Forms.TextBox txtHardBin4;
        private System.Windows.Forms.TextBox txtHardBin3;
        private System.Windows.Forms.TextBox txtHardBin2;
        private System.Windows.Forms.TextBox txtHardBin1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.ToolStripMenuItem menuData;
        private System.Windows.Forms.ToolStripMenuItem menuLotInfo;
        private System.Windows.Forms.ToolStripMenuItem menuSoftBins;
        private System.Windows.Forms.StatusStrip ssFooter;
        private System.Windows.Forms.ToolStripStatusLabel siUserName;
        private System.Windows.Forms.ToolStripStatusLabel siDepartment;
        private System.Windows.Forms.ToolStripStatusLabel siHostName;
        private System.Windows.Forms.ToolStripStatusLabel siIPAddress;
        private System.Windows.Forms.ToolStripStatusLabel siMachineModel;
        private System.Windows.Forms.Button btnRetry;
        private System.Windows.Forms.GroupBox grpRuntimeInfo;
        private System.Windows.Forms.ListBox lstRuntimeInfo;
        private System.Windows.Forms.GroupBox grpEquipmentStatus;
        private System.Windows.Forms.Button btnEndLot;
        private System.Windows.Forms.TextBox txtLotNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripStatusLabel siHandlerModel;
        private System.Windows.Forms.ToolStripMenuItem menuLogOut;
        private System.Windows.Forms.Timer TmrDownloadRecipe;
        private System.Windows.Forms.CheckBox chkVision_Reject;
        private System.Windows.Forms.TextBox txtVision_Reject;
        private System.Windows.Forms.CheckBox chkIso_Reject;
        private System.Windows.Forms.TextBox txtIso_Reject;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMark_Count;
        private System.Windows.Forms.Timer TmrRetry;
    }
}