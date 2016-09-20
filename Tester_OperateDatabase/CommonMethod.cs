using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualBasic;

namespace Tester_OperateDatabase
{
    public class CommonMethod
    {
        public void WriteLog(string sMsg, string sLogType)
        {
            string sFile = null;
            string sPath = null;
            sPath = @"C:\RMSLOG\" + sLogType + "\\";
            if (!Directory.Exists(sPath))
            {
                Directory.CreateDirectory(sPath);
            }
            sFile = sPath + sLogType + "_" + DateTime.Now.ToString("yyyyMMdd") + ".LOG";
            File.AppendAllText(sFile, DateTime.Now.ToString("HH:mm:ss") + "  " + sMsg + "\n");
        }

        public string HandleWireSize(string sWireSize)
        {
            string sTrimWireSize = string.Empty;
            string sAfterTrimWireSize = string.Empty;

            sTrimWireSize = sWireSize.Substring(0, 4);
            sTrimWireSize = sTrimWireSize.Replace("M", "");
            sTrimWireSize = sTrimWireSize.Replace("m", "");

            // Split line for WireSize on comma
            string[] split1 = sTrimWireSize.Split(new Char[] { ' ', ',', '.', ':', Convert.ToChar(Constants.vbTab) });

            foreach (string line1_loopVariable in split1)
            {
                sAfterTrimWireSize += line1_loopVariable.Trim();
            }

            return sAfterTrimWireSize;
        }

        public string HandleSpace(string strSource)
        {
            string[] strA = strSource.Split(' ');

            if (strA.Length > 1)
            {
                strSource = strA[0] + "-" + strA[strA.Length - 1];
            }
            return strSource;
        }
    }
}