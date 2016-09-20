using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Reflection;
using Tester.OUI.Utilities;
using System.IO;

namespace Tester.OUI
{
    static class Program
    {

        [DllImport("User32.dll")]
        static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);

        [DllImport("User32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        const int SW_HIDE = 0;
        const int SW_SHOWNORMAL = 1;
        const int SW_SHOWMINIMIZED = 2;
        const int SW_SHOWMAXIMIZED = 3;
        const int SW_SHOWNOACTIVATE = 4;
        const int SW_RESTORE = 9;
        const int SW_SHOWDEFAULT = 10;

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Process instance = GetRunningInstance();

            if (instance == null)
            {
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                using (frmLogin frmLogin = new frmLogin())
                {
                    if (frmLogin.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }
                }

                var date = DateTime.Now.AddDays(-60);
                CleanUp(Application.StartupPath+"\\Log", date);

                string clientAppConfig = "config\\Config.xml";
                string handlertype = UtilXml.GetXmlNodeValue(clientAppConfig, "//SBLConfig//McConfig//HandlerType");

                if (handlertype == UtilConstants.HANDLER_PM52|| handlertype==UtilConstants.HANDLER_PM52OLD)
                {
                    Application.Run(new FrmTesterOUI_PM());
                }
                else if (handlertype == UtilConstants.HANDLER_ST)
                {
                    Application.Run(new FrmTesterOUI_ST());
                }
            }
            else
            {
                HandleRunningInstance(instance);
            }
        }

        static Process GetRunningInstance()
        {
            Process currentProcess = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(currentProcess.ProcessName);

            foreach (Process process in processes)
            {
                if (process.Id != currentProcess.Id)
                {
                    if (Assembly.GetExecutingAssembly().Location.Replace("/", "\\") == currentProcess.MainModule.FileName)
                    {
                        return process;
                    }
                }
            }

            return null;
        }

        static void HandleRunningInstance(Process instance)
        {
            ShowWindowAsync(instance.MainWindowHandle, SW_SHOWNORMAL);

            SetForegroundWindow(instance.MainWindowHandle);
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e != null && e.ExceptionObject != null)
            {
                Exception exception = e.ExceptionObject as Exception;

                if (exception != null)
                {
                    UtilMessage.ShowError(exception);
                    UtilLog.Error(exception);
                }
            }
        }

        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            if (e != null && e.Exception != null)
            {
                UtilMessage.ShowError(e.Exception);
                UtilLog.Error(e.Exception);
            }
        }

        static void CleanUp(string logDirectory,DateTime date)
        {
            if (string.IsNullOrEmpty(logDirectory))
                throw new ArgumentException("logDirectory is missing");

            var dirInfo = new DirectoryInfo(logDirectory);
            if (!dirInfo.Exists)
                return;

            var fileInfos = dirInfo.GetFiles();
            if (fileInfos.Length == 0)
                return;

            foreach (var info in fileInfos)
            {
                if (info.CreationTime < date)
                {
                    info.Delete();
                }
            }
        }
    }
}
