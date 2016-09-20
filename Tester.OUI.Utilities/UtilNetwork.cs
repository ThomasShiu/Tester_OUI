using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;


namespace Tester.OUI.Utilities
{
    public sealed class UtilNetwork
    {
        private UtilNetwork()
        {

        }

        public static string GetHostName()
        {
            return Dns.GetHostName();
        }

        public static IPAddress GetLocalIPAddress()
        {
            IPAddress localIPAddress = IPAddress.Any;

            IPAddress[] ipAddresses = Dns.GetHostAddresses(Dns.GetHostName());

            foreach (IPAddress ipAddress in ipAddresses)
            {
                if (ipAddress.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIPAddress = ipAddress;

                    break;
                }
            }

            return localIPAddress;
        }

        public static string DownloadRecipe(string ftpuri, string DownloadDir, string ext)
        {
            string uri = ftpuri.Split(',')[0];
            string ftpUserID = ftpuri.Split(',')[1];
            string ftpPassword = ftpuri.Split(',')[2];

            string fileName = uri.Replace("/", "@").Split('@')[4].ToString() + "." + ext;
            uri = uri + "." + ext;
            FtpWebRequest reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
            reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
            reqFTP.UseBinary = true;
            reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
            reqFTP.Proxy = null;

            System.Text.StringBuilder strMsg;

            int bufferSize = 2048;
            byte[] buffer = new byte[bufferSize];
            string targetFilePath = DownloadDir + "\\" + fileName;
            FileStream outputStream = null;
            FtpWebResponse response = null;
            Stream ftpStream = null;
            try
            {
                outputStream = new FileStream(targetFilePath, FileMode.Create);
                response = (FtpWebResponse)reqFTP.GetResponse();
                ftpStream = response.GetResponseStream();

                long cl = response.ContentLength;
                int readCount = ftpStream.Read(buffer, 0, bufferSize);
                while (readCount > 0)
                {
                    outputStream.Write(buffer, 0, readCount);
                    readCount = ftpStream.Read(buffer, 0, bufferSize);
                }

                strMsg = new System.Text.StringBuilder("OK");
            }
            catch (Exception ex)
            {
                string ErrStr = ex.ToString();
                UtilLog.Error(ex);
                if (ErrStr.Substring(0, 67).Trim() == "System.Net.WebException: The remote server returned an error: (550)")
                {
                    strMsg = new System.Text.StringBuilder("Test Program loading error!-程序调用错误！" + "\r\n");
                    strMsg.Append(fileName + " Program version is not correct!-程序版本号错误！" + "\r\n");
                }
                else
                {
                    strMsg = new System.Text.StringBuilder("Test Program loading error!-程序调用错误！" + "\r\n");
                    strMsg.Append(fileName + " Program file doesn't exist!-程序文件不存在！" + "\r\n");
                    strMsg.Append("Pls try again!-请重新扫描！" + "\r\n");
                }
                if (ftpStream != null)
                    ftpStream.Close();
                if (outputStream != null)
                    outputStream.Close();
                if (response != null)
                    response.Close();
            }
            finally
            {
                if (ftpStream != null)
                    ftpStream.Close();
                if (outputStream != null)
                    outputStream.Close();
                if (response != null)
                    response.Close();
            }

            return strMsg.ToString();
        }
    }
}
