using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Tester.OUI.Entities
{
    public class TesterLogEntity
    {
        public string LogName { get; set; }
        public string LotNumber { get; set; }
        public string MachineNumber { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Hashtable HardBins { set; get; }
        public Hashtable SoftBins { set; get; }
    }
}
