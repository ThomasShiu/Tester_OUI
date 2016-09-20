using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Tester.OUI.Utilities
{
    public sealed class UtilMessage
    {
        private UtilMessage()
        {

        }

        public static void ShowInfo(string msgCode)
        {
            ShowInfoMessage(null, msgCode);
        }

        //public static void ShowInfo(IWin32Window owner, string msgCode)
        //{
        //    ShowInfoMessage(owner, UtilLanguage.GetValue(msgCode));
        //}

        public static void ShowInfoMessage(IWin32Window owner, string msg)
        {
            //MessageBox.Show(owner, msg, UtilLanguage.GetValue("Message.Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            MessageBox.Show(owner, msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void ShowWarn(string msgCode)
        {
            ShowWarnMessage(null, msgCode);
        }

        //public static void ShowWarn(IWin32Window owner, string msgCode)
        //{
        //    ShowWarnMessage(owner, UtilLanguage.GetValue(msgCode));
        //}

        public static void ShowWarnMessage(IWin32Window owner, string msg)
        {
            //MessageBox.Show(owner, msg, UtilLanguage.GetValue("Message.Warning"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            MessageBox.Show(owner, msg, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static DialogResult ShowQues(string msgCode)
        {
            return ShowQuesMessage(null, msgCode);
        }

        //public static DialogResult ShowQues(IWin32Window owner, string msgCode)
        //{
        //    return ShowQuesMessage(owner, UtilLanguage.GetValue(msgCode));
        //}

        public static DialogResult ShowQuesMessage(IWin32Window owner, string msg)
        {
            //return MessageBox.Show(owner, msg, UtilLanguage.GetValue("Message.Question"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            return MessageBox.Show(owner, msg, "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        public static void ShowError(string msgCode)
        {
            ShowErrorMessage(null, msgCode);
        }

        //public static void ShowError(IWin32Window owner, string msgCode)
        //{
        //    ShowErrorMessage(owner, UtilLanguage.GetValue(msgCode));
        //}

        public static void ShowErrorMessage(IWin32Window owner, string msg)
        {
            //MessageBox.Show(owner, msg, UtilLanguage.GetValue("Message.Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            MessageBox.Show(owner, msg, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void ShowError(Exception ex)
        {
            ShowError(null, ex);
        }

        public static void ShowError(IWin32Window owner, Exception ex)
        {
            //UtilLog.SEMIOUIClientLogger.Error(ex);

            MessageBox.Show(owner, ex.Message,"错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
