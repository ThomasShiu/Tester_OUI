using System;
using System.Collections.Generic;
using System.Text;
using Tester.OUI.Entities;
using Tester.OUI.Utilities;

namespace Tester.OUI
{
    /// <summary>
    /// 定义一个信息委托
    /// </summary>
    /// <param name="sender">发布者</param>
    /// <param name="msg">发送内容</param>
    public delegate void MsgDlg(object sender, object msg);

    /// <summary>
    /// 定义一个信息委托
    /// </summary>
    /// <param name="msg">发送内容</param>
    public delegate void Config_Update(object config);

    public delegate void DownloadRecipe();

    public class MidMsgTransferModule
    {
        /// <summary>
        /// 消息发送事件
        /// </summary>
        public static event MsgDlg EventSend;

        /// <summary>
        /// 消息发送事件
        /// </summary>
        public static event Config_Update SecsGemConfig_Update;

        public static event DownloadRecipe DownloadRecipe_Tester;
        public static void SendMessage(object sender, object msg)
        {
            if (EventSend != null)//
            {
                EventSend(sender, msg);
            }
        }

        public static void UpdateSecsGem(object machine)
        {
            if (SecsGemConfig_Update != null)//
            {
                UtilLog.Info("Entering SecsGemConfig_Update");
                SecsGemConfig_Update(machine);
            }
        }

        public static void DownloadTesterRecipe()
        {
            if (DownloadRecipe_Tester != null)//
            {
                UtilLog.Info("Redownload Tester Recipe");
                DownloadRecipe_Tester();
            }
        }
    }
}
