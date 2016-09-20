using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;
using System.Collections;
using Tester.OUI.Utilities;
using Tester.OUI.SummaryLogHander;
using Tester.OUI.Entities;

namespace WinformTest
{
    public partial class Form1 : Form
    {
        List<Bin> hardBins = new List<Bin>();
        List<Bin> softBins = new List<Bin>();

        Hashtable gHardBins = new Hashtable();
        Hashtable gSoftBins = new Hashtable();

        UtilIniFile iniFile;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = "hardbin1";
            string num = name.Substring(7, name.Length - 7);
            this.textBox1.Text = num;
          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string clientAppConfig = "config\\McConfig.xml";
            SBLConfig sbl = new SBLConfig();
            XmlSerializer serializer = new XmlSerializer(sbl.GetType());
            FileStream stream = new FileStream(clientAppConfig, FileMode.Open);
            sbl = (SBLConfig)serializer.Deserialize(stream);
            stream.Close();
            stream.Dispose();

            TesterMachine gtester = new TesterMachine();
            foreach (TesterMachine tester in sbl.Testers)
            {
                if (tester.Type == sbl.McConfig.TesterType)
                {
                    gtester = tester;
                }
            }
            Handler ghandler = new Handler();

            foreach (Handler handler in sbl.Handlers)
            {
                if (handler.Type == sbl.McConfig.HandlerType)
                {
                    ghandler = handler;
                    hardBins = ghandler.Bins;
                }
            }

            LoadLotInfo();
        }

        private void LoadLotInfo()
        {
            try
            {
                string iniFilePath = Application.StartupPath + "\\config\\LotInfo.ini";
                iniFile = new UtilIniFile(iniFilePath);

                foreach (Bin bin in hardBins)
                {
                    bin.Qty = iniFile.ReadInt("HardBin", bin.Name);

                    if (bin.Qty > 0)
                    {
                        ((TextBox)(this.Controls.Find("txt" + bin.Name, true)[0])).Text = bin.Qty.ToString();
                        gHardBins.Add(bin.Name, bin);
                    }
                }

                //soft bin data
                for (int i = 1; i <= 30; i++)
                {
                    int binQty = iniFile.ReadInt("SoftBin", "SoftBin" + i);

                    if (binQty> 0)
                    {
                        Bin bin = new Bin();
                        bin.Name = "SoftBin" + i;
                        bin.Qty = binQty;
                        softBins.Add(bin);
                        gSoftBins.Add(bin.Name, bin);
                    }
                }

            }
            catch (Exception ex)
            {
                UtilLog.Error(ex);
            }
        }


        private void btnReTest_Click(object sender, EventArgs e)
        {
            foreach (Bin bin in hardBins)
            {
                if (bin.Qty > 0)
                {
                    if (((CheckBox)(this.Controls.Find("chk" + bin.Name, true)[0])).Checked)
                    {
 
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string filepath = "D:\\IVILOGBK";
            filepath = Application.StartupPath + "\\config\\";
            string filename = "NBC9614_X000612435_20160316_134738.sum";

            //Hashtable sbins = LogHandler.readStatecFile(filepath + filename, filename);
            List<Bin> bins = LogHandler.readStatecList(filepath+filename, filename);

            foreach (Bin bin in bins)
            {
                if (gSoftBins.Contains(bin.Name))
                {
 
                }
            }

        }


    }
}
