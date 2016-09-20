using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Collections;

namespace Tester.OUI.Entities
{
    public class SBLConfig
    {
        public SBLConfig()
        {
        }

        public MachineConfig McConfig;
        public List<TesterMachine> Testers;
        public List<Handler> Handlers;
    }

    [Serializable]
    public class MachineConfig
    {
        public string Employee_No { get; set; }
        public string HandlerNo { get; set; }
        public string TesterNo { get; set; }
        public string TesterType { get; set; }
        public string HandlerType { get; set; }
        public string QADeptName { get; set; }
        public string HandlerIp { get; set; }
        public int HandlerPort { get; set; }
        public string ConnectionStatus { get; set; }

    }

    public class TesterMachine
    {
        public TesterMachine()
        {
 
        }

        [XmlAttribute]
        public string Type { get; set; }
        public string StartupFilePath { get; set; }
        public string StartupFileName { get; set; }
        public string RecipeDownloadPath { get; set; }
        public string SummaryLogPath { get; set; }
        public string SummaryLogFilter { get; set; }
        public string EWBLogPath { get; set; }

    }

    public class Handler
    {
        public Handler()
        {

        }

        [XmlAttribute]
        public string Type { get; set; }    
        public List<Bin> Bins;
    }

    [Serializable]
    public class Bin
    {
        public Bin()
        {

        }

        public Bin(string name,int qty)
        {
            this.Name = name;
            this.Qty = qty;
        }
      
        [XmlAttribute]
        public string Name { get; set; }
        [XmlAttribute]
        public int SVID { get; set; }
        public int Qty { get; set; }
    }
}
