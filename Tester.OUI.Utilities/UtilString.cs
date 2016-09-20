using System;
using System.Collections.Generic;
using System.Text;

namespace Tester.OUI.Utilities
{
    public sealed class UtilString
    {
        public static string HandleSpace(string originalStr, string connectionStr)
        {
            string afterStr = "";
            
            string[] strArray = originalStr.Split(' ');

            for (int i = 0; i < strArray.Length; i++)
            {
                if (strArray[i].Trim().Length > 0)
                {
                    afterStr += strArray[i] + connectionStr;
                }
            }

            return afterStr.Trim();
        }

    }
}
