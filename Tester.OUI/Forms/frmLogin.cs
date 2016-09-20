using System;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Xml;
using Tester.OUI.Entities;
using Tester.OUI.Utilities;
using System.Net;
using System.IO;
using System.Reflection;

namespace Tester.OUI
{

    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {

                using (TesterRecipe_Service.TesterRecipe_Service ts_rmsService = new TesterRecipe_Service.TesterRecipe_Service())
                {
                    string strDepartment = "";
                    UtilLog.Info("Verify user login :" + txtUserName.Text.Trim());
                    string sVerfiyResult = ts_rmsService.GetDepartment(txtUserName.Text.Trim(), txtPassword.Text.Trim());
                    UtilLog.Info("Verify user login Result:" + sVerfiyResult);

                    if (sVerfiyResult.Substring(0, 4) == "True")
                    {
                        if (sVerfiyResult.Length != 8)
                        {
                            strDepartment = sVerfiyResult.Substring(4, 5);
                        }
                        else
                        {
                            strDepartment = "NONE*";
                        }

                        #region Set User Info Data
                        UtilCommonInfo.UserName = txtUserName.Text.Trim();
                        UtilCommonInfo.Password = txtPassword.Text.Trim();
                        UtilCommonInfo.Dept = strDepartment;
                        #endregion

                        this.DialogResult = DialogResult.OK;
                    }
                    else if (sVerfiyResult.Substring(0, 4) == "miss")
                    {
                        string sMsg = "UserID or PassWord missmatch!用户名或密码不正确！";
                        UtilMessage.ShowError(sMsg);
                        this.DialogResult = DialogResult.None;
                        //return;
                    }
                }
            }
            catch (Exception ex)
            {
                string sMsg = "Remote Host Not Responding!!远端服务器没有响应！";
                UtilMessage.ShowWarn(sMsg);
                UtilLog.Error("LogIn Handle Send Message to Host Error :", ex);
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.btnLogin_Click(sender, e);
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            string clientAppConfig = "config\\Config.xml";
            Assembly assem = Assembly.GetEntryAssembly();
            AssemblyName assemName = assem.GetName();

            this.label1.Text += assemName.Version.ToString();
           
            if (!File.Exists(clientAppConfig))
            {
                UtilMessage.ShowErrorMessage(this, "缺少Config文件！");
                Application.Exit();
            }
        }
    }

}