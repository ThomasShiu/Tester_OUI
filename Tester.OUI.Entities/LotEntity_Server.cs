using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Tester.OUI.Entities
{
    [Serializable]
    public class LotEntity_Server
    {
        public LotEntity_Server()
        {
        }

        public string LotNo { get; set; }
        public string DeviceId { get; set; }
        public string RecipeName { get; set; }
        public int LotQty { get; set; }
        public string LotAttribute { get; set; }
        public string RecipePath { get; set; }
        public string SpmGdcal { get; set; }
        public DictionaryEntry[] HardBins { get; set; }
        public DictionaryEntry[] SoftBins { get; set; }
        public bool IsInsertionLot { get; set; }
        public bool IsCamstarStatusUpdated { get; set; }
        public string Hold_Reason { get; set; }
    }
}
