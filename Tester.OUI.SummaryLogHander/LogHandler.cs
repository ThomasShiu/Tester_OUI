using System;
using System.Collections;
using System.Text;
using System.IO;
using Tester.OUI.Entities;
using Tester.OUI.Utilities;
using System.Collections.Generic;

namespace Tester.OUI.SummaryLogHander
{
    public sealed class LogHandler
    {
        public static Hashtable readEAGLEFile(string filePath, string fileName)
        {
            System.Text.RegularExpressions.Regex rex = new System.Text.RegularExpressions.Regex(@"^\d+$");
            Hashtable softBins = new Hashtable();
            string testStart = "", testEnd = "";
            int intRowCount = 0;
            bool boolStart = false;
            FileStream fileStream = null;
            StreamReader reader = null;
            try
            {
                fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Delete);
                reader = new StreamReader(fileStream);
                string ln = null;

                #region 解析文件
                while ((ln = reader.ReadLine()) != null)
                {
                    ln = ln.Trim();

                    if (ln.Contains("Data Collection Start Date"))
                    {
                        testStart = ln.Substring(ln.IndexOf(":") + 1).Trim();
                        continue;
                    }
                    if (ln.Contains(("Data Collection Stop  Date")))
                    {
                        testEnd = ln.Substring(ln.IndexOf(":") + 1).Trim();
                        continue;
                    }

                    if (ln.Contains("Sfwr"))
                    {
                        boolStart = true;
                        intRowCount++;
                        continue;
                    }

                    if (boolStart)
                    {
                        intRowCount++;
                        if (intRowCount > 3)
                        {
                            ln = UtilString.HandleSpace(ln, ";");

                            string[] strArray = ln.Split(';');

                            if (strArray.Length < 2)
                            {
                                continue;
                            }

                            if (rex.IsMatch(strArray[strArray.Length - 3]))
                            {
                                Bin bin = new Bin();
                                bin.Name = "SoftBin" + strArray[0];
                                bin.Qty = int.Parse(strArray[strArray.Length - 3]);
                                softBins.Add(bin.Name, bin);
                            }
                        }
                    }

                    if (ln.Contains("Hdwr"))
                    {
                        break;
                    }
                }
                #endregion

            }
            catch (IOException e)
            {
                UtilLog.Error("Handle Read File IOErr :", e);
            }
            catch (Exception ex)
            {
                UtilLog.Error("Handle Read File Err :", ex);
            }
            finally
            {
                try
                {
                    if (reader != null)
                    {
                        reader.Close();
                        reader.Dispose();
                        reader = null;
                    }
                    if (fileStream != null)
                    {
                        fileStream.Close();
                        fileStream.Dispose();
                        fileStream = null;
                    }
                }
                catch (IOException e)
                {
                    throw (e);
                }
            }
            return softBins;
        }

        public static Hashtable readStatecFile(string filePath, string fileName)
        {         
            Hashtable softBins = new Hashtable();
            string testStart = "", testEnd = "", totalTestCount = "", totalGoodCount = "";
            int intRowCount = 0;
            bool boolFoundSoftBin = false;
            FileStream fileStream = null;
            StreamReader reader = null;
            try
            {
                fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Delete);
                reader = new StreamReader(fileStream);
                string ln = null;

                #region 解析文件
                while ((ln = reader.ReadLine()) != null)
                {
                    ln = ln.Trim();
                    if (ln.StartsWith("Total Test Count"))
                    {
                        totalTestCount = ln.Substring(ln.IndexOf(":") + 1).Trim();
                        continue;
                    }

                    if (ln.StartsWith("Total Good Count"))
                    {
                        totalGoodCount = ln.Substring(ln.IndexOf(":") + 1).Trim();
                        continue;
                    }

                    if (ln.StartsWith("Test Start"))
                    {
                        ln = ln.Replace(".......", ":");
                        testStart = ln.Substring(ln.IndexOf(":") + 1).Trim();
                        testStart = testStart.Replace("   ", " ");
                        continue;
                    }

                    if (ln.StartsWith("Test End"))
                    {
                        ln = ln.Replace(".......", ":");
                        testEnd = ln.Substring(ln.IndexOf(":") + 1).Trim();
                        testEnd = testEnd.Replace("   ", " ");
                        continue;
                    }

                    if (ln.Contains("<< TEST  BIN  REPORT >>"))
                    {
                        boolFoundSoftBin = true;
                        intRowCount++;
                        continue;
                    }

                    if (boolFoundSoftBin)
                    {
                        intRowCount++;
                        if (intRowCount > 4)
                        {
                            if (ln.Length < 3)
                            {
                                break;
                            }

                            ln = UtilString.HandleSpace(ln, "_");
                            string[] strArray = ln.Split('_');

                            for (int i = 0; i < strArray.Length / 3; i++)
                            {
                                int rejectQty = int.Parse(strArray[i * 3 + 1]);
                                if (rejectQty > 0)
                                {
                                    Bin bin = new Bin();
                                    //bin.BinNumber = int.Parse(strArray[i * 3]);
                                    bin.Name = "SoftBin" + strArray[i * 3];
                                    bin.Qty = rejectQty;                            
                                    softBins.Add(bin.Name, bin);
                                    UtilLog.Info("Test Logger Name: "+fileName+", "+bin.Name+", Bin Qty: "+bin.Qty);
                                }
                            }
                        }
                    }
                }
                #endregion
            }
            catch (IOException e)
            {
                UtilLog.Error("Handle Read File IOErr :", e);
            }
            catch (Exception ex)
            {
                UtilLog.Error("Handle Read File Err :", ex);
            }
            finally
            {
                try
                {
                    if (reader != null)
                    {
                        reader.Close();
                        reader.Dispose();
                        reader = null;
                    }
                    if (fileStream != null)
                    {
                        fileStream.Close();
                        fileStream.Dispose();
                        fileStream = null;
                    }
                }
                catch (IOException e)
                {
                    throw (e);
                }
            }

            return softBins;
        }

        public static List<Bin> readStatecList(string filePath, string fileName)
        {
            List<Bin> softBins = new List<Bin>();
            string testStart = "", testEnd = "", totalTestCount = "", totalGoodCount = "";
            int intRowCount = 0;
            bool boolFoundSoftBin = false;
            FileStream fileStream = null;
            StreamReader reader = null;
            try
            {
                fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Delete);
                reader = new StreamReader(fileStream);
                string ln = null;

                #region 解析文件
                while ((ln = reader.ReadLine()) != null)
                {
                    ln = ln.Trim();
                    if (ln.StartsWith("Total Test Count"))
                    {
                        totalTestCount = ln.Substring(ln.IndexOf(":") + 1).Trim();
                        continue;
                    }

                    if (ln.StartsWith("Total Good Count"))
                    {
                        totalGoodCount = ln.Substring(ln.IndexOf(":") + 1).Trim();
                        continue;
                    }

                    if (ln.StartsWith("Test Start"))
                    {
                        ln = ln.Replace(".......", ":");
                        testStart = ln.Substring(ln.IndexOf(":") + 1).Trim();
                        testStart = testStart.Replace("   ", " ");
                        continue;
                    }

                    if (ln.StartsWith("Test End"))
                    {
                        ln = ln.Replace(".......", ":");
                        testEnd = ln.Substring(ln.IndexOf(":") + 1).Trim();
                        testEnd = testEnd.Replace("   ", " ");
                        continue;
                    }

                    if (ln.Contains("<< TEST  BIN  REPORT >>"))
                    {
                        boolFoundSoftBin = true;
                        intRowCount++;
                        continue;
                    }

                    if (boolFoundSoftBin)
                    {
                        intRowCount++;
                        if (intRowCount > 4)
                        {
                            if (ln.Length < 3)
                            {
                                break;
                            }

                            ln = UtilString.HandleSpace(ln, "_");
                            string[] strArray = ln.Split('_');

                            for (int i = 0; i < strArray.Length / 3; i++)
                            {
                                int rejectQty = int.Parse(strArray[i * 3 + 1]);
                                if (rejectQty > 0)
                                {
                                    Bin bin = new Bin();
                                    //bin.BinNumber = int.Parse(strArray[i * 3]);
                                    bin.Name = "SoftBin" + strArray[i * 3];
                                    bin.Qty = rejectQty;
                                    softBins.Add(bin);
                                    UtilLog.Info("Test Logger Name: " + fileName + ", " + bin.Name + ", Bin Qty: " + bin.Qty);
                                }
                            }
                        }
                    }
                }
                #endregion
            }
            catch (IOException e)
            {
                UtilLog.Error("Handle Read File IOErr :", e);
            }
            catch (Exception ex)
            {
                UtilLog.Error("Handle Read File Err :", ex);
            }
            finally
            {
                try
                {
                    if (reader != null)
                    {
                        reader.Close();
                        reader.Dispose();
                        reader = null;
                    }
                    if (fileStream != null)
                    {
                        fileStream.Close();
                        fileStream.Dispose();
                        fileStream = null;
                    }
                }
                catch (IOException e)
                {
                    throw (e);
                }
            }

            return softBins;
        }
    }
}
