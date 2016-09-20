using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Tester.OUI.Entities
{
    [Serializable]
    public class LotEntity
    {
        public LotEntity()
        {
            SoftBins = new Hashtable();
        }

        public string LotNo { get; set; }
        public string DeviceId { get; set; }
        public string RecipeName { get; set; }
        public int LotQty { get; set; }
        public string LotAttribute { get; set; }
        public string RecipePath { get; set; }
        public string SpmGdcal { get; set; }
        public Hashtable SoftBins { set; get; }
        public bool IsInsertionLot { get; set; }

        public bool IsCamstarStatusUpdated { get; set; }
    }
}
