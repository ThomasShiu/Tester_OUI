using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using Tester.OUI.Entities;
using Tester.OUI.Utilities;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;

namespace Tester.OUI.Utilities
{
    public sealed class UtilCommonMethod
    {
        /// <summary>
        /// 运行CMD命令
        /// </summary>
        /// <param name="cmd">命令</param>
        /// <returns></returns>
        private static void StartCommand(string cmd)
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            p.StandardInput.AutoFlush = true;
            p.StandardInput.WriteLine(cmd.ToString());
            p.StandardInput.WriteLine("exit");
            p.Dispose();
        }

        public static void CreateEagleFile(LotEntity lotEntity, string localdir, string infofile)
        {
            string date = System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            string filename = localdir + "\\" + infofile;
            string cmd = "shellnt program " + lotEntity.RecipeName + " -at -b";

            try
            {
                StreamWriter sw = File.CreateText(filename);
                sw.WriteLine("Time=" + date + "\n");
                sw.WriteLine("Product=" + lotEntity.DeviceId + "\n");
                sw.WriteLine("RecipeName =" + lotEntity.RecipeName + "\n");
                sw.WriteLine("LotNo =" + lotEntity.LotNo + "\n");
                sw.WriteLine("SubLotNo = 1" + "\n");
                sw.WriteLine("LotSize = "+lotEntity.LotQty + "\n");
                sw.Flush();
                sw.Close();

                StartCommand(cmd);

                date = System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                System.Text.StringBuilder strMsg = new System.Text.StringBuilder("Time=" + date + "\r\n");
                strMsg.Append("Product=" + lotEntity.DeviceId + "\r\n");
                strMsg.Append("shellnt program " + lotEntity.RecipeName + " -at -b");
            }
            catch (Exception ex)
            {
                UtilLog.Error(ex);
            }
        }

        public static void CreateStatecFile(LotEntity lotEntity, string localdir, string infofile,string station)
        {
            UtilLog.Info("starting Create statec file:");
            string date = System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            string filename = localdir + "\\" + infofile;
            string cmd = "shellnt program " + lotEntity.RecipeName + " -at -b";

            try
            {
                
                DeleteFile(localdir, infofile.Substring(infofile.Length - 4, 4));
                
                StreamWriter sw = File.CreateText(filename);
                sw.WriteLine("STATION:" + station);
                sw.WriteLine("DEVICE_NAME:" + lotEntity.DeviceId);
                sw.WriteLine("PROGRAM_NAME:" + lotEntity.RecipeName);
                sw.WriteLine("LOT_NUMBER:" + lotEntity.LotNo);
                sw.WriteLine("LOT_QUANTITY:" + lotEntity.LotQty);
                sw.WriteLine("GDCAL:" + lotEntity.SpmGdcal);

                sw.Flush();
                sw.Close();

            }
            catch (Exception ex)
            {
                UtilLog.Error(ex);
            }
        }

        public static void MoveFile(string fullpath, string fileName, string destdir)
        {
            try
            {
                if (!Directory.Exists(destdir))
                {
                    Directory.CreateDirectory(destdir);
                }
                string destpath = destdir + fileName;   //目标路径

                if (File.Exists(destpath))
                {
                    File.Delete(destpath);    //目标目录中有此文件，则先删除，再移过来。
                }
                File.Move(fullpath, destpath);
                UtilLog.Info("Move file: " + fullpath + " to path: " + destdir);
            }
            catch (Exception ex)
            {
                UtilLog.Error("Handle Move File Err:", ex);
            }
        }

        public static bool CopyFile(string fullpath, string fileName, string destdir)
        {
            try
            {
                if (!Directory.Exists(destdir))
                {
                    Directory.CreateDirectory(destdir);
                }
                string destpath = destdir + fileName;   //目标路径

                if (File.Exists(destpath))
                {
                    File.Delete(destpath);    //目标目录中有此文件，则先删除，再移过来。
                }

                File.Copy(fullpath, destpath);
                return true;
            }
            catch (Exception ex)
            {
                UtilLog.Error("Handle Copy File Err:", ex);
                return false;
            }
        }

        public static void DeleteFile(string localDir, string type)
        {
            DirectoryInfo di = new DirectoryInfo(@localDir);
            FileInfo[] ff = di.GetFiles(type);
            try
            {
                if (ff.Length != 0)
                {
                    foreach (FileInfo fi in ff)
                    {
                        fi.Delete();
                    }
                }
            }
            catch (Exception ex)
            {
                UtilLog.Error(ex);
            }
        }
        public static byte[] SerializeObject(object pObj)
        {
            if (pObj == null) return null;
            System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(memoryStream, pObj);
            memoryStream.Position = 0;
            byte[] read = new byte[memoryStream.Length];
            memoryStream.Read(read, 0, read.Length);
            memoryStream.Close();
            return read;
        }
    }
}
