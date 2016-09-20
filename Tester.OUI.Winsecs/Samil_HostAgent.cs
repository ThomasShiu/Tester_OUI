using System;
using System.Collections.Generic;
using System.Text;
using Brooks.WinSECS;

namespace Tester.OUI.Winsecs
{
    public delegate void MessageInHandler(object sender);
    public delegate void StatusChangeHandler(object sender);

    public class Samil_HostAgent : INotifyAgent
    {
        public MessageInHandler SecsMessageIn;
        public StatusChangeHandler ConnectStatusChange;

        void INotifyAgent.OnConnect()
        {
            ConnectStatusChange(this);
        }

        void INotifyAgent.OnDisconnect(ERRORS ErrorCode, string ErrorText)
        {
            ConnectStatusChange(this);
        }

        void INotifyAgent.OnError(ERRORS ErrorCode, string ErrorText)
        {
            //throw new NotImplementedException();
        }

        void INotifyAgent.OnMonitor(DateTime Time, bool bSent, string evt, string sHex)
        {
            //throw new NotImplementedException();
        }

        void INotifyAgent.OnPrimaryIn(SECSTransaction t, ERRORS ErrorCode, string ErrorText)
        {
            try
            {
                SecsHelper.RecordSecsMessage(t.Primary);

                if (t.Primary.Stream == 1)
                {
                    switch (t.Primary.Function)
                    {
                        case 1:
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
                else if (t.Primary.Stream == 5)
                {
                    if (t.Primary.Function == 1)
                    {
                        t.Secondary.Root.Item("ACKC5").Value = 0;
                        t.Reply();
                    }
                }
                else if (t.Primary.Stream == 6)
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

        void INotifyAgent.OnPrimaryOut(SECSTransaction t, ERRORS ErrorCode, string ErrorText)
        {
            SecsHelper.RecordSecsMessage(t.Primary);
        }

        void INotifyAgent.OnSecondaryIn(SECSTransaction t, ERRORS ErrorCode, string ErrorText)
        {
            SecsHelper.RecordSecsMessage(t.Secondary);
            SecsMessageIn(t);
        }

        void INotifyAgent.OnSecondaryOut(SECSTransaction t, ERRORS ErrorCode, string ErrorText)
        {
            SecsHelper.RecordSecsMessage(t.Secondary);
        }

        void INotifyAgent.OnWarning(ERRORS ErrorCode, string ErrorText)
        {
            //throw new NotImplementedException();
        }
    }
}
