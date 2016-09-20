using System;

namespace Tester.OUI.Entities
{
    public class TestSummary
    {
        public TestSummary()
        {
            this.TotalGoodCount = 0;
            this.TotalTestCount = 0;
        }

        private long id;
        public long Id
        {
            get { return id; }
            set { id = value; }
        }
        private string lotNo;

        public string LotNo
        {
            get { return lotNo; }
            set { lotNo = value; }
        }
        private string tester;

        public string Tester
        {
            get { return tester; }
            set { tester = value; }
        }

        private string testertype;

        public string TesterType
        {
            get { return testertype; }
            set { testertype = value; }
        }

        private string itemName;

        public string ItemName
        {
            get { return itemName; }
            set { itemName = value; }
        }
        private DateTime testStartTime;

        public DateTime TestStartTime
        {
            get { return testStartTime; }
            set { testStartTime = value; }
        }
        private DateTime testEndTime;

        public DateTime TestEndTime
        {
            get { return testEndTime; }
            set { testEndTime = value; }
        }
        private int totalTestCount;

        public int TotalTestCount
        {
            get { return totalTestCount; }
            set { totalTestCount = value; }
        }
        private int totalGoodCount;

        public int TotalGoodCount
        {
            get { return totalGoodCount; }
            set { totalGoodCount = value; }
        }
        private string fileName;

        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }
        private DateTime analyzeTime;

        public DateTime AnalyzeTime
        {
            get { return analyzeTime; }
            set { analyzeTime = value; }
        }
    }
}
