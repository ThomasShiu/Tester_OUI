using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Tester.OUI.Entities;
using Tester.OUI.Utilities;

namespace Tester.OUI
{
    public partial class frmResetLot : Form
    {
        LotEntity_Server gv_lot = new LotEntity_Server();
        MachineConfig gv_machine = new MachineConfig();

        public frmResetLot(LotEntity_Server lot, MachineConfig machine)
        {
            gv_lot = lot;
            gv_machine = machine;
            InitializeComponent();
        }

        private void frmResetLot_Load(object sender, EventArgs e)
        {
            this.Text = gv_machine.HandlerNo + "--" + gv_lot.LotNo;
            txtLotNo.Text = gv_lot.LotNo;
            string clientAppConfig = "config\\Config_Default.xml";
            DataSet ds = UtilXml.GetXMLFile(clientAppConfig);

            comb_Resetreason.DataSource = ds.Tables["reason"];
            comb_Resetreason.ValueMember = "EN_VALUE";
            comb_Resetreason.DisplayMember = "CH_VALUE";
        }

        private void btnConfrim_Click(object sender, EventArgs e)
        {
            if (comb_Resetreason.Text.Trim() == "")
            {
                UtilMessage.ShowError("请选择reset lot 原因！！！");
                this.DialogResult = DialogResult.None;
            }
            else
            {
                using (Tester_Service.TestSummary_Service service = new Tester_Service.TestSummary_Service())
                {
                    gv_lot.Hold_Reason = comb_Resetreason.SelectedValue.ToString().Trim();
                    service.RecordResetLot(UtilCommonMethod.SerializeObject(gv_lot), UtilCommonMethod.SerializeObject(gv_machine));
                }

                this.DialogResult = DialogResult.OK;
            }
        }

    }
}
