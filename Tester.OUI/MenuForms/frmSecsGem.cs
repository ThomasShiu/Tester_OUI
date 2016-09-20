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
    public partial class frmSecsGem : Form
    {
        MachineConfig gv_machine = new MachineConfig();

        public frmSecsGem(MachineConfig machine)
        {
            gv_machine = machine;
            InitializeComponent();
        }

        private void btnShowSecsGem_Click(object sender, EventArgs e)
        {
            try
            {
                gv_machine.HandlerIp = txtHandlerIP.Text.Trim();
                gv_machine.HandlerPort = int.Parse(txtHandlerPort.Text.Trim());
                UtilLog.Info("IP = " + gv_machine.HandlerIp + "; Port = " + gv_machine.HandlerPort);
                MidMsgTransferModule.UpdateSecsGem(gv_machine);
                UtilMessage.ShowInfo("Secs/gem config Update Success!");
            }
            catch (Exception ex)
            {
                UtilLog.Error(ex);
            }
        }

        private void frmSecsGem_Load(object sender, EventArgs e)
        {
            this.Text = gv_machine.HandlerNo + " SECSGEM CONFIG";
            txtHandlerIP.Text = gv_machine.HandlerIp;
            txtHandlerPort.Text = gv_machine.HandlerPort.ToString();
            txtConnectState.Text = gv_machine.ConnectionStatus;
        }

        private void chkHandlerIp_CheckedChanged(object sender, EventArgs e)
        {
            if (chkHandlerIp.Checked)
            {
                txtHandlerIP.ReadOnly = false;
            }
            else
            {
                txtHandlerIP.ReadOnly = true;
            }
        }

        private void chkHandlerPort_CheckedChanged(object sender, EventArgs e)
        {
            if (chkHandlerPort.Checked)
            {
                txtHandlerPort.ReadOnly = false;
            }
            else
            {
                txtHandlerPort.ReadOnly = true;
            }
        }

    }
}
