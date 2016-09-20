using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TesterMI.AutoUpdater
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            Application.Run(new FrmAutoUpdater());
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message, "SEMI AutoUpdater", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
