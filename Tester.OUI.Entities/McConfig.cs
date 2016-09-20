using System;
using System.Collections.Generic;
using System.Text;

namespace Tester.OUI.Entities
{
    public class SBLConfig
    {
        public SBLConfig()
        {
 
        }

    }
    public class McConfig
    {
        public string MachineNo { get; set; }
        public string TesterType { get; set; }
        public string HandlerType { get; set; }
        public string HandlerIpAddress { get; set; }
        public int HandlerPort { get; set; }
        public string QADeptName { get; set; }
    }

    public class Tester
    {
        public string TesterType { get; set; }
        public string InfoFileDir { get; set; }
        public string InfoFileName { get; set; }
        public string RecipeDownloadDir { get; set; }
        public string LogPath { get; set; }
        public string LogFilter { get; set; }
        public string EwbPath { get; set; }
    }

    public class Handler
    {
        public string HandlerType { get; set; }
        public string HandlerType { get; set; }
        public string HandlerIpAddress { get; set; }
        public int HandlerPort { get; set; }
    }
}
