using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Tester.OUI.Entities;

namespace Tester.OUI
{
    public partial class frmLotInfo : Form
    {
        LotEntity gv_lot = new LotEntity();

        public frmLotInfo(LotEntity lot)
        {
            gv_lot = lot;
            InitializeComponent();
        }

        private void frmSecsGem_Load(object sender, EventArgs e)
        {
            txtLotNo.Text = gv_lot.LotNo;
            txtLotQty.Text = gv_lot.LotQty.ToString();
            txtDeviceID.Text = gv_lot.DeviceId;
            txtRecipeName.Text = gv_lot.RecipeName;
            txtRecipePath.Text = gv_lot.RecipePath;
        }

        private void btnRedownload_Click(object sender, EventArgs e)
        {
            MidMsgTransferModule.DownloadTesterRecipe();
        }

    }
}
