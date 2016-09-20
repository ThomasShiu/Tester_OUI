using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;

namespace TestProgram_Auto_Download_Client
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {


            string XmlUrl = "C:/config/ClientConfig.ini";
            string Purl = "config/PortConfig.ini";


            if (!Directory.Exists("C:\\config"))
            {
                Directory.CreateDirectory("C:\\config");
            }
            else
            {

            }
            if (!Directory.Exists("log"))
            {

                Directory.CreateDirectory("log");
            }
            else
            {

            }
            if (File.Exists(XmlUrl) & File.Exists(Purl))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Login());
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new init());
            }
        }
    }
}