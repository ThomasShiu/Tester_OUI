using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Tester.OUI.Entities;
using System.Collections;
using Tester.OUI.Utilities;

namespace Tester.OUI
{
    public partial class frmSoftBins : Form
    {
        Hashtable gv_softbins = new Hashtable();
        public frmSoftBins(Hashtable softbins)
        {
            gv_softbins = softbins;
            InitializeComponent();
        }

        private void frmSecsGem_Load(object sender, EventArgs e)
        {
            UpdateSoftBinListBox();
        }

        private void UpdateSoftBinListBox()
        {
            try
            {
                foreach (Bin softbin in gv_softbins.Values)
                {
                    lstSoftBins.Items.Add(softbin.Name + ": "+softbin.Qty);
                }
            }
            catch (Exception ex)
            {
                UtilLog.Error(ex);
            }
        }

    }
}
