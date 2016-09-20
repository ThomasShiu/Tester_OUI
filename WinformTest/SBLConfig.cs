using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace WinformTest
{
    public class SBLConfig
    {
        public SBLConfig()
        {
        }

        public MachineConfig McConfig;
        public List<Tester> Testers;
        public List<Handler> Handlers;
    }

    public class MachineConfig
    {
        public string MachineNo { get; set; }
        public string TesterType { get; set; }
        public string HandlerType { get; set; }
        public string QADeptName { get; set; }
        public string HandlerIp { get; set; }
        public int HandlerPort { get; set; }
    }

    public class Tester
    {
        public Tester()
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

    public class Bin
    {
        public Bin()
        {

        }
      
        [XmlAttribute]
        public string Name { get; set; }
        [XmlAttribute]
        public string SVID { get; set; }

        public int Qty { get; set; }
    }
}
