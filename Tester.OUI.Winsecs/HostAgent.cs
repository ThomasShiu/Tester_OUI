using System;
using System.Collections.Generic;
using System.Text;
using Brooks.WinSECS;
using Tester.OUI.Utilities;

namespace Tester.OUI.Winsecs
{
    public delegate void StatusChangeHandler1(object sender);
    public delegate void QueryHardBinQtyHandler(object sender);
    public delegate void RemoteCommandHandler(object sender);

    public class HostAgent : INotifyAgent
    {
        public StatusChangeHandler1 StatusChangeEvent;
        public QueryHardBinQtyHandler QueryHardBinQty;
        public RemoteCommandHandler RMDHandler;

        public void OnConnect()
        {
            //throw new NotImplementedException();
            StatusChangeEvent(this);
        }

        public void OnDisconnect(ERRORS ErrorCode, string ErrorText)
        {
            //throw new NotImplementedException();
            StatusChangeEvent(this);
        }

        public void OnError(ERRORS ErrorCode, string ErrorText)
        {
            //throw new NotImplementedException();
        }

        public void OnMonitor(DateTime Time, bool bSent, string evt, string sHex)
        {
            //throw new NotImplementedException();
        }

        public void OnPrimaryIn(SECSTransaction t, ERRORS ErrorCode, string ErrorText)
        {
            try
            {

                SecsHelper.RecordSecsMessage(t.Primary);

                if (t.Primary.Stream == 1)
                {
                    switch (t.Primary.Function)
                    {
                        case 1:
                            //SECSItem mdln = t.Secondary.Root.AddNew("L").AddNew("MDLN");
                            //mdln.Format = SECS_FORMAT.ASCII;
                            //mdln.Value = "";

                            //SECSItem softver = mdln.Duplicate();
                            //softver.Format = SECS_FORMAT.ASCII;
                            //softver.Value = "";
                            t.Secondary.Root.Item("L").Item("MDLN").Value = "";
                            t.Secondary.Root.Item("L").Item("SOFTREV").Value = "";
                            t.Reply();
                            break;
                        case 13:
                            t.Secondary.Root.Item("COMMACK").Value = 0;
                            t.Secondary.Root.Item("MDLN").Delete();
                            t.Secondary.Root.Item("SOFTREV").Delete();
                            t.Reply();
                            break;
                    }
                }
                else if(t.Primary.Stream==6)
                {
                    if (t.Primary.Function == 11)
                    {
                        t.Secondary.Root.Item("ACKC6").Value = 0;
                        t.Reply();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Host Primary In message Error:" + ex.Message);
            }
        }

        public void OnPrimaryOut(SECSTransaction t, ERRORS ErrorCode, string ErrorText)
        {
            SecsHelper.RecordSecsMessage(t.Primary);
        }

        public void OnSecondaryIn(SECSTransaction t, ERRORS ErrorCode, string ErrorText)
        {
            SecsHelper.RecordSecsMessage(t.Secondary);
            if (t.Secondary.Stream == 1 && t.Secondary.Function == 4)
            {
                if (t.Tag == "QryHardBinQty")
                {
                    QueryHardBinQty(t);
                }
            }

            if (t.Secondary.Stream == 2 && t.Secondary.Function == 16)
            {
                RMDHandler(t);
            }

            if (t.Secondary.Stream == 2 && t.Secondary.Function == 42)
            {
                RMDHandler(t);
            }
        }

        public void OnSecondaryOut(SECSTransaction t, ERRORS ErrorCode, string ErrorText)
        {
            //throw new NotImplementedException();
            SecsHelper.RecordSecsMessage(t.Secondary);
        }

        public void OnWarning(ERRORS ErrorCode, string ErrorText)
        {
            //throw new NotImplementedException();
        }

        #region " Test Methods"
        private void BuildS1F14(SECSTransaction trans)
        {
            trans.Secondary.Root.Name = "CRA";
            SECSItem item = trans.Secondary.Root.AddNew("L");
            SECSItem itemCommAck = item.AddNew("COMMACK", "Establish communications acknowledge code");
            itemCommAck.Format = SECS_FORMAT.BINARY;
            itemCommAck.Value = 0;
            SECSItem itemMCInfo = item.AddNew("L");
            itemMCInfo.AddNew("MDLN", "Equipment Model Type");
            itemMCInfo.Item("MDLN").Format = SECS_FORMAT.ASCII;
            itemMCInfo.Item("MDLN").Value = "LH620";
            itemMCInfo.AddNew("SOFTREV", "Software revision code");
            itemMCInfo.Item("SOFTREV").Format = SECS_FORMAT.ASCII;
            itemMCInfo.Item("SOFTREV").Value = "1.2.0";
            trans.Reply();
        }

        private void SendS1F3R(WinSECS host)
        {
            SECSTransaction trans = new SECSTransaction(1, 3);
            trans.Primary.Root.Name = "SSR";
            trans.Primary.Root.Description = "Selected equipment status request";
            SECSItem item = trans.Primary.Root.AddNew("L");
            SECSItem svid = item.AddNew("SVID", "Status variable ID");
            svid.Format = SECS_FORMAT.U4;
            svid.Value = 1001;
            SECSItem svid2 = svid.Duplicate();
            svid2.Format = SECS_FORMAT.U4;
            svid2.Value = 1002;
            SECSItem svid3 = svid.Duplicate();
            svid3.Format = SECS_FORMAT.U4;
            svid3.Value = 1003;
            trans.ReplyExpected = true;
            trans.Send(host); ;
        }

        public void SendS1F3(WinSECS host)
        {
            SECSTransaction trans = new SECSTransaction(1, 3);

            trans.Primary.Root.Name = "SSR";
            trans.Primary.Root.Description = "Selected equipment status request";
            SECSItem item = trans.Primary.Root.AddNew("L");
            SECSItem svid = item.AddNew("SVID", "Status variable ID");
            svid.Format = SECS_FORMAT.U4;
            svid.Value = 1001;
            SECSItem svid2 = svid.Duplicate();
            svid2.Format = SECS_FORMAT.U4;
            svid2.Value = 1002;
            SECSItem svid3 = svid.Duplicate();
            svid3.Format = SECS_FORMAT.U4;
            svid3.Value = 1003;
            trans.ReplyExpected = true;
            trans.Send(host); ;
        }

        public void SendS1F13(WinSECS host)
        {
            SECSTransaction trans = new SECSTransaction(1, 13);

            trans.Primary.Root.Name = "CR";
            trans.Primary.Root.Description = "Establish communications request";
            SECSItem item = trans.Primary.Root.AddNew("L");
            trans.ReplyExpected = true;
            trans.Send(host); ;
        }
        #endregion

    }
}
