using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace TesterMI.AutoUpdater
{
    /// <summary>
    /// STP AutoUpdater Client
    /// </summary>
    /// Owner:Andy Gao 2011-11-15 09:45:46
    public partial class FrmAutoUpdater : Form
    {
        private const string DATETIME_FORMAT = "yyyy-MM-dd HH:mm:ss.fff";

        [StructLayout(LayoutKind.Sequential)]
        private struct NETRESOURCE
        {
            public ResourceScope dwScope;
            public ResourceType dwType;
            public ResourceDisplayType dwDisplayType;
            public ResourceUsage dwUsage;
            public string lpLocalName;
            public string lpRemoteName;
            public string lpComment;
            public string lpProvider;
        }

        private enum ResourceScope
        {
            RESOURCE_CONNECTED = 1,
            RESOURCE_GLOBALNET,
            RESOURCE_REMEMBERED,
            RESOURCE_RECENT,
            RESOURCE_CONTEXT
        }

        private enum ResourceType
        {
            RESOURCETYPE_ANY = 0,
            RESOURCETYPE_DISK,
            RESOURCETYPE_PRINT,
            RESOURCETYPE_RESERVED
        }

        private enum ResourceUsage
        {
            RESOURCEUSAGE_CONNECTABLE = 0x00000001,
            RESOURCEUSAGE_CONTAINER = 0x00000002,
            RESOURCEUSAGE_NOLOCALDEVICE = 0x00000004,
            RESOURCEUSAGE_SIBLING = 0x00000008,
            RESOURCEUSAGE_ATTACHED = 0x00000010,
            RESOURCEUSAGE_ALL = (RESOURCEUSAGE_CONNECTABLE | RESOURCEUSAGE_CONTAINER | RESOURCEUSAGE_ATTACHED)
        }

        private enum ResourceDisplayType
        {
            RESOURCEDISPLAYTYPE_GENERIC = 0,
            RESOURCEDISPLAYTYPE_DOMAIN,
            RESOURCEDISPLAYTYPE_SERVER,
            RESOURCEDISPLAYTYPE_SHARE,
            RESOURCEDISPLAYTYPE_FILE,
            RESOURCEDISPLAYTYPE_GROUP,
            RESOURCEDISPLAYTYPE_NETWORK,
            RESOURCEDISPLAYTYPE_ROOT,
            RESOURCEDISPLAYTYPE_SHAREADMIN,
            RESOURCEDISPLAYTYPE_DIRECTORY,
            RESOURCEDISPLAYTYPE_TREE,
            RESOURCEDISPLAYTYPE_NDSCONTAINER
        }

        [DllImport("mpr.dll")]
        private static extern int WNetAddConnection2(ref NETRESOURCE lpNetResource, string lpPassword, string lpUsername, int dwFlags);

        [DllImport("mpr.dll")]
        private static extern int WNetCancelConnection2(string lpLocalName, int dwFlags, bool fForce);

        [DllImport("user32.dll", EntryPoint = "GetSystemMenu")]
        private extern static IntPtr GetSystemMenu(IntPtr hWnd, IntPtr bRevert);

        [DllImport("user32.dll", EntryPoint = "RemoveMenu")]
        private extern static IntPtr RemoveMenu(IntPtr hMenu, int uPosition, int uFlags);

        private readonly string sourceDirectory = ConfigurationManager.AppSettings["SourceDirectory"];
        private readonly string sourceDirectoryUserName = ConfigurationManager.AppSettings["SourceDirectoryUserName"];
        private readonly string sourceDirectoryPassword = ConfigurationManager.AppSettings["SourceDirectoryPassword"];
        private readonly string destinationDirectory = ConfigurationManager.AppSettings["DestinationDirectory"];
        private readonly string executeFile = ConfigurationManager.AppSettings["ExecuteFile"];
        private readonly string logFileName = ConfigurationManager.AppSettings["LogFileName"];

        private FileStream logFileStream = null;
        private StreamWriter logStreamWriter = null;

        public FrmAutoUpdater()
        {
            InitializeComponent();
        }

        private void FrmAutoUpdater_Load(object sender, EventArgs e)
        {
            #region Disable the close button on the form window

            IntPtr menuHandle = GetSystemMenu(this.Handle, IntPtr.Zero);

            RemoveMenu(menuHandle, 0xF060, 0x0000); //0xF060 is position of the close button

            #endregion
        }

        private void FrmAutoUpdater_Shown(object sender, EventArgs e)
        {
            try
            {
                #region Delete the log file when size greater than 1M bytes

                if (File.Exists(this.logFileName))
                {
                    FileInfo fileInfo = new FileInfo(this.logFileName);

                    if (fileInfo.Length > 1048576)
                    {
                        fileInfo.Delete();
                    }
                }

                #endregion

                this.lblUpdateInfo.Text = "Tester OUI AutoUpdater is starting......";

                DirectoryInfo srcDirectoryInfo = new DirectoryInfo(sourceDirectory);

                if (!srcDirectoryInfo.Exists)
                {
                    WriteLog("Application is Starting Connect to Network Drive......", false);
                    ConnectNetResource(this.sourceDirectory, this.sourceDirectoryUserName, this.sourceDirectoryPassword);
                }

                WriteLog("Application is updating......", false);

                UpdateDirectory(this.sourceDirectory, this.destinationDirectory);

                WriteLog("Application was updated", true);

                //DisconnectNetResource(this.sourceDirectory);

                this.lblUpdateInfo.Text = "Tester OUI AutoUpdater was finished";

                Process.Start(this.executeFile);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Tester OUI AutoUpdater", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Application.Exit();
            }
        }

        private void FrmAutoUpdater_FormClosing(object sender, FormClosingEventArgs e)
        {
            switch (e.CloseReason)
            {
                case CloseReason.UserClosing:
                    e.Cancel = true;
                    break;
            }
        }

        /// <summary>
        /// Connect specified path of network resource by username and password
        /// </summary>
        /// <param name="netResourcePath"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// Owner:Andy Gao 2013-04-24 20:17:14
        private bool ConnectNetResource(string netResourcePath, string userName, string password)
        {
            NETRESOURCE netResource = new NETRESOURCE();

            netResource.dwScope = ResourceScope.RESOURCE_GLOBALNET;
            netResource.dwType = ResourceType.RESOURCETYPE_DISK;
            netResource.dwDisplayType = ResourceDisplayType.RESOURCEDISPLAYTYPE_SHARE;
            netResource.dwUsage = ResourceUsage.RESOURCEUSAGE_CONNECTABLE;
            netResource.lpRemoteName = netResourcePath;

            return WNetAddConnection2(ref netResource, password, userName, 0) == 0;
        }

        /// <summary>
        /// Disconnect specified path of network resource
        /// </summary>
        /// <param name="netResourcePath"></param>
        /// <returns></returns>
        /// Owner:Andy Gao 2013-04-24 20:18:30
        private bool DisconnectNetResource(string netResourcePath)
        {
            return WNetCancelConnection2(netResourcePath, 0, true) == 0;
        }

        /// <summary>
        /// Update specified files and subdirectories from source directory to destination directory
        /// </summary>
        /// <param name="srcDirectory"></param>
        /// <param name="dstDirectory"></param>
        /// Owner:Andy Gao 2011-11-15 14:18:40
        private void UpdateDirectory(string srcDirectory, string dstDirectory)
        {
            DirectoryInfo srcDirectoryInfo = new DirectoryInfo(srcDirectory);
            DirectoryInfo dstDirectoryInfo = new DirectoryInfo(dstDirectory);

            if (srcDirectoryInfo.Exists)
            {
                if (!dstDirectoryInfo.Exists)
                {
                    dstDirectoryInfo.Create();

                    WriteLog(string.Format("Created directory \"{0}\"", dstDirectoryInfo.FullName), false);
                }

                FileInfo[] fileInfos = srcDirectoryInfo.GetFiles();

                foreach (FileInfo fileInfo in fileInfos)
                {
                    UpdateFile(fileInfo.FullName, string.Format("{0}\\{1}", dstDirectoryInfo.FullName, fileInfo.Name));

                    Application.DoEvents();
                }

                DirectoryInfo[] directoryInfos = srcDirectoryInfo.GetDirectories();

                foreach (DirectoryInfo directoryInfo in directoryInfos)
                {
                    UpdateDirectory(directoryInfo.FullName, string.Format("{0}\\{1}", dstDirectoryInfo.FullName, directoryInfo.Name));

                    Application.DoEvents();
                }
            }
            else
            {
                WriteLog(string.Format("Source directory \"{0}\" does not exist", srcDirectoryInfo.FullName), false);
            }
        }

        /// <summary>
        /// Update specified file from source file to destination file
        /// </summary>
        /// <param name="srcFile"></param>
        /// <param name="dstFile"></param>
        /// Owner:Andy Gao 2011-11-15 14:28:58
        private void UpdateFile(string srcFile, string dstFile)
        {
            FileInfo srcFileInfo = new FileInfo(srcFile);
            FileInfo desFileInfo = new FileInfo(dstFile);

            if (srcFileInfo.Exists)
            {
                if (desFileInfo.Exists)
                {
                    if (srcFileInfo.LastWriteTime.CompareTo(desFileInfo.LastWriteTime) != 0)
                    {
                        this.lblUpdateInfo.Text = string.Format("Updating file \"{0}\"......", srcFileInfo.Name);

                        File.SetAttributes(desFileInfo.FullName, FileAttributes.Normal);

                        File.Copy(srcFileInfo.FullName, desFileInfo.FullName, true);

                        WriteLog(string.Format("Updated file \"{0}\" to \"{1}\"", srcFileInfo.Name, desFileInfo.FullName), false);
                    }
                }
                else
                {
                    this.lblUpdateInfo.Text = string.Format("Copying file \"{0}\"......", srcFileInfo.Name);

                    File.Copy(srcFileInfo.FullName, desFileInfo.FullName, true);

                    WriteLog(string.Format("Copied file \"{0}\" to \"{1}\"", srcFileInfo.Name, desFileInfo.FullName), false);
                }
            }
            else
            {
                WriteLog(string.Format("Source file \"{0}\" does not exist", srcFileInfo.FullName), false);
            }
        }

        /// <summary>
        /// Write message to specified log file
        /// </summary>
        /// <param name="message"></param>
        /// <param name="isDispose"></param>
        /// Owner:Andy Gao 2011-11-15 11:54:08
        private void WriteLog(string message, bool isDispose)
        {
            if (this.logFileStream == null)
            {
                this.logFileStream = new FileStream(this.logFileName, FileMode.Append, FileAccess.Write, FileShare.Read);
            }

            if (this.logStreamWriter == null)
            {
                this.logStreamWriter = new StreamWriter(this.logFileStream, Encoding.UTF8);
            }

            this.logStreamWriter.WriteLine("{0} - {1}", DateTime.Now.ToString(DATETIME_FORMAT), message);

            if (isDispose)
            {
                this.logStreamWriter.Flush();
                this.logStreamWriter.Close();

                this.logStreamWriter.Dispose();
                this.logFileStream.Dispose();

                this.logStreamWriter = null;
                this.logFileStream = null;
            }
        }
    }
}
