using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Brooks.WinSECS;
using Tester.OUI.Entities;
using Tester.OUI.SummaryLogHander;
using Tester.OUI.Utilities;
using Tester.OUI.Winsecs;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Reflection;

namespace Tester.OUI
{
    public partial class FrmTesterOUI_ST : Form
    {
        #region "Variable Parameter"
        WinSECS gv_WinSecs;
        Samil_HostAgent gv_HostAgent;
        SECSLibrary gv_HostLib;
        UtilIniFile iniFile;
        UtilAutoSizeForm asc = new UtilAutoSizeForm();
        SBLConfig sbl = new SBLConfig();
        LotEntity lotEntity = new LotEntity();
        Hashtable softBins = new Hashtable();
        Hashtable hardAndSoftBin = new Hashtable();
        Handler gHandler = new Handler();
        TesterMachine gTester = new TesterMachine();
        MachineConfig machine = new MachineConfig();
        Hashtable gH_HardBins = new Hashtable();
        #endregion

        #region "Variable"
        bool gIsReceived = true;
        string sMsg = "";
        string sTag = "";
        private static string clientAppConfig = "config\\Config.xml";
        #endregion

        #region "Web Service"
        TesterRecipe_Service.TesterRecipe_Service ts_rmsService = new TesterRecipe_Service.TesterRecipe_Service();
        Tester_Service.TestSummary_Service tsummary_Service = new Tester_Service.TestSummary_Service();
        #endregion

        #region "Event Handler"
        delegate void SetRealtimeMessageCallback(string messageColorName, string message);
        #endregion

        public FrmTesterOUI_ST()
        {
            InitializeComponent();
        }

        private void FrmTest_Load(object sender, EventArgs e)
        {
            try
            {
                asc.controllInitializeSize(this);

                // InitStatusValues();

                InitMachine();

                InitialStatusLabel();

                InitHanlderSecsGem();

                LoadLotInfo();

                LoadHardAndSoftBinRelation();

                InitButtonStatus();

                gv_HostAgent = new Samil_HostAgent();
                gv_HostAgent.SecsMessageIn += SecsMessageHandler;
                gv_HostAgent.ConnectStatusChange += ConnectStatusUpdate;

                // open port
                gv_WinSecs.OpenPort(gv_HostAgent, gv_HostLib);
                MidMsgTransferModule.SecsGemConfig_Update += new Config_Update(HandlerSecsGemConfig_Update);
                MidMsgTransferModule.DownloadRecipe_Tester += new DownloadRecipe(HandleTesterProgram);

                sMsg = "Tester OUI启动成功!";
                SetRealtimeMessage(UtilConstants.COLOR_BLUE_NAME, sMsg);

                TmrDownloadRecipe.Enabled = false;
            }
            catch (Exception ex)
            {
                sMsg = "启动失败：" + ex.Message;
                UtilLog.Error(ex);
                SetRealtimeMessage(UtilConstants.COLOR_RED_NAME, sMsg);
            }
        }

        private void FrmTest_SizeChanged(object sender, EventArgs e)
        {
            asc.controlAutoSize(this);
        }

        private void FrmTest_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (gv_WinSecs.PortIsOpen)
            {
                gv_WinSecs.ClosePort();
            }
        }

        #region "1.1 Init Method"

        private void InitStatusValues()
        {
            UtilCommonInfo.UserName = "W0011831";
            UtilCommonInfo.Password = "xx";
            UtilCommonInfo.Dept = "QA";
            sMsg = "Set Status Values Success!";
            SetRealtimeMessage(UtilConstants.COLOR_BLUE_NAME, sMsg);
        }

        private void InitMachine()
        {
            XmlSerializer serializer = new XmlSerializer(sbl.GetType());
            FileStream stream = new FileStream(clientAppConfig, FileMode.Open);
            sbl = (SBLConfig)serializer.Deserialize(stream);
            stream.Close();
            machine = sbl.McConfig;
            foreach (TesterMachine tester in sbl.Testers)
            {
                if (tester.Type == machine.TesterType)
                {
                    gTester = tester;
                }
            }

            foreach (Handler handler in sbl.Handlers)
            {
                if (handler.Type == machine.HandlerType)
                {
                    gHandler = handler;
                }
            }
        }
        private void InitialStatusLabel()
        {
            machine.TesterNo = UtilNetwork.GetHostName();
            machine.Employee_No = UtilCommonInfo.UserName;
            this.siUserName.Text = string.Format("User Name: {0}", UtilCommonInfo.UserName);
            this.siDepartment.Text = string.Format("Department: {0}", UtilCommonInfo.Dept);
            this.siHostName.Text = string.Format("Host Name: {0}", machine.TesterNo);
            this.siIPAddress.Text = string.Format("Ip Address: {0}", UtilNetwork.GetLocalIPAddress().ToString());
            this.siMachineModel.Text = string.Format("Test Model: {0}", machine.TesterType);
            this.siHandlerModel.Text = string.Format("Handler Model: {0}", machine.HandlerType);

            Assembly assem = Assembly.GetEntryAssembly();
            AssemblyName assemName = assem.GetName();

            this.Text = machine.HandlerNo + " Tester OUI "+assemName.Version.ToString();
            UtilControls.InitializeFormColorListBox(this.lstRuntimeInfo);

            sMsg = "Set Status Label Success!";
            SetRealtimeMessage(UtilConstants.COLOR_BLUE_NAME, sMsg);

        }

        private void InitHanlderSecsGem()
        {
            try
            {
                string xmlFilePath = Application.StartupPath + "\\config\\SamilTech.xml";
                gv_WinSecs = new WinSECS();
                gv_WinSecs.PortType = SECS_PORT_TYPE.HSMS;
                gv_WinSecs.AutoDevice = true;
                gv_WinSecs.Hsms.RemoteIPAddress = machine.HandlerIp;
                gv_WinSecs.Hsms.RemoteIPPort = (uint)machine.HandlerPort;
                gv_WinSecs.Hsms.ConnectionMode = HSMS_CONNECTION_MODE.ACTIVE;
                gv_WinSecs.Hsms.T3 = 45;
                gv_WinSecs.Hsms.T5 = 10;
                gv_WinSecs.Hsms.T6 = 5;
                gv_WinSecs.Hsms.T7 = 10;
                gv_WinSecs.Hsms.T8 = 5;

                gv_HostLib = new SECSLibrary("SamilTest", "Default one");
                gv_HostLib.Load(xmlFilePath);
                UtilLog.Info("Init SecsGem successfully!");
            }
            catch (Exception ex)
            {
                UtilLog.Error(ex);
            }
        }

        private void LoadHardAndSoftBinRelation()
        {
            try
            {
                hardAndSoftBin.Clear();

                if (lotEntity.LotNo.Length > 2)
                {
                    DataTable dtHardAndSoftBins = tsummary_Service.GetHardBinAndSoftBinTable(lotEntity.DeviceId, "");

                    if (dtHardAndSoftBins.Rows.Count == 0)
                    {
                        UtilLog.Info(lotEntity.DeviceId + " LoadHardAndSoftBinRelation from DB Fail!");
                        return;
                    }

                    for (int i = 0; i < dtHardAndSoftBins.Rows.Count - 1; i++)
                    {
                        if (!hardAndSoftBin.ContainsKey(dtHardAndSoftBins.Rows[i][0]))
                        {
                            hardAndSoftBin.Add(dtHardAndSoftBins.Rows[i][0], dtHardAndSoftBins.Rows[i][1]);
                        }
                    }
                }
                UtilLog.Info(lotEntity.DeviceId + " LoadHardAndSoftBinRelation from DB successfully!");
            }
            catch (Exception ex)
            {
                UtilLog.Error(ex);
            }
        }

        private void LoadLotInfo()
        {
            try
            {
                string binName = "", binQty = "";
                string iniFilePath = Application.StartupPath + "\\config\\LotInfo.ini";
                iniFile = new UtilIniFile(iniFilePath);

                if (iniFile != null)
                {
                    lotEntity.LotNo = iniFile.ReadString("CurrentLot", "LotNo");
                    lotEntity.DeviceId = iniFile.ReadString("CurrentLot", "DeviceId");
                    lotEntity.RecipeName = iniFile.ReadString("CurrentLot", "RecipeName");
                    lotEntity.LotQty = int.Parse(iniFile.ReadString("CurrentLot", "LotQty"));
                    lotEntity.RecipePath = iniFile.ReadString("CurrentLot", "RecipePath");
                    lotEntity.IsInsertionLot = iniFile.ReadBool("CurrentLot", "IsInsertionLot");
                    lotEntity.IsCamstarStatusUpdated = iniFile.ReadBool("CurrentLot", "IsCamstarStatusUpdated");
                }

                if (lotEntity.LotNo.Length > 2)
                {
                    txtLotNo.Text = lotEntity.LotNo;

                    foreach (Bin hardbin in gHandler.Bins)
                    {
                        hardbin.Qty = iniFile.ReadInt("HardBin", hardbin.Name);

                        if (hardbin.Qty > 0)
                        {
                            ((TextBox)(this.Controls.Find("txt" + hardbin.Name, true)[0])).Text = hardbin.Qty.ToString();
                            gH_HardBins.Add(hardbin.Name, hardbin);
                        }
                    }

                    //soft bin data
                    for (int i = 1; i <= 30; i++)
                    {
                        binName = "SoftBin" + i;
                        binQty = iniFile.ReadString("SoftBin", binName);

                        if (int.Parse(binQty) != 0)
                        {
                            Bin bin = new Bin();
                            bin.Name = binName;
                            bin.Qty = int.Parse(binQty);
                            lotEntity.SoftBins.Add(bin.Name, bin);
                        }
                    }

                    sMsg = "成功加载" + lotEntity.LotNo + "信息！";
                    SetRealtimeMessage(UtilConstants.COLOR_BLUE_NAME, sMsg);
                }
            }
            catch (Exception ex)
            {
                UtilLog.Error(ex);
            }
        }

        private void InitButtonStatus()
        {
            if (lotEntity.LotNo.Length > 2)
            {
                txtLotNo.Enabled = false;
                btnRetry.Enabled = false;
                btnEndLot.Enabled = true;

                // lot info contain failed bins 
                if (lotEntity.SoftBins.Count > 0)
                {
                    btnReTest.Enabled = true;
                    btnSubmit.Enabled = true;
                }
                else
                {
                    btnReTest.Enabled = false;
                    btnSubmit.Enabled = false;
                }
            }
            else
            {
                btnEndLot.Enabled = false;
                btnReTest.Enabled = false;
                btnSubmit.Enabled = false;
                btnRetry.Enabled = false;
                txtLotNo.Enabled = true;
            }
            UtilLog.Info("InitButtonStatus successfully!");
        }
        #endregion

        #region "1.2 RMS Bussiness Logical"
        private void txtLotNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    bool IsDownloadSuccess = true;

                    ClearBins();

                    string sLotNo = txtLotNo.Text.Trim().ToUpper();

                    txtLotNo.Text = sLotNo;

                    if (sLotNo.StartsWith("X"))
                    {
                        sLotNo = sLotNo.Substring(0, 10);
                    }

                    if (!gv_WinSecs.Connected)
                    {
                        sMsg = "Handler 断线，请先检查Handler连线状况!";
                        SetRealtimeMessage(UtilConstants.COLOR_RED_NAME, sMsg);
                        UtilMessage.ShowWarn(sMsg);
                        return;
                    }

                    sMsg = sLotNo + ",开始下载程序!";
                    SetRealtimeMessage(UtilConstants.COLOR_BLUE_NAME, sMsg);

                    string sRecipeInfo = ts_rmsService.GetTestProgram(sLotNo, machine.TesterType, UtilCommonInfo.Dept,machine.TesterNo);

                    //get test program fail
                    if (!sRecipeInfo.Contains(","))
                    {
                        SetRealtimeMessage(UtilConstants.COLOR_RED_NAME, sRecipeInfo);
                        return;
                    }

                    //start to download recipe
                    if (gTester.Type == UtilConstants.TESTER_STATEC)
                    {
                        IsDownloadSuccess = HanldeStatecRecipeMessage(sRecipeInfo);
                    }
                    else if (gTester.Type == UtilConstants.TESTER_EAGLE)
                    {
                        IsDownloadSuccess = HanldeEagleRecipeMessage(sRecipeInfo);
                    }

                    //download recipe fail
                    if (!IsDownloadSuccess)
                    {
                        sMsg = lotEntity.LotNo + ",程序下载失败,请重试！";
                        SetRealtimeMessage(UtilConstants.COLOR_RED_NAME, sMsg);
                        return;
                    }

                    //show messages about download info 
                    sMsg = lotEntity.LotNo + ",程序下载成功!";
                    SetRealtimeMessage(UtilConstants.COLOR_BLUE_NAME, sMsg);

                    //new lot command send to Handler
                    ClearProductCount();

                    //start to get lot insertion qty
                    TmrDownloadRecipe.Enabled = true;
                }
                catch (Exception ex)
                {
                    sMsg = "下载程序失败" + ex.Message;
                    SetRealtimeMessage(UtilConstants.COLOR_RED_NAME, sMsg);
                    UtilLog.Error(ex);
                }
            }
        }

        #region "Statec Recipe Download"
        private bool HanldeStatecRecipeMessage(string receivedmessage)
        {
            try
            {
                bool IsHandleRecipeMessageSuccess = true;

                UtilLog.Info("Get Recipe Info From WebService :" + receivedmessage);

                string[] sArray = receivedmessage.Split(',');
                lotEntity.LotNo = txtLotNo.Text.Trim();
                lotEntity.RecipePath = sArray[0].ToString() + "," + sArray[1].ToString() + "," + sArray[2].ToString();
                lotEntity.DeviceId = sArray[3].ToString();
                lotEntity.RecipeName = sArray[4].ToString();
                lotEntity.SpmGdcal = sArray[5].ToString();

                if (sArray[6] != "")
                {
                    lotEntity.LotQty = int.Parse(sArray[6].ToString());
                }
                else
                {
                    lotEntity.LotQty = 0;
                }

                lotEntity.IsCamstarStatusUpdated = false;
                lotEntity.IsInsertionLot = false;

                IsHandleRecipeMessageSuccess = HandleTesterRMS();

                return IsHandleRecipeMessageSuccess;
            }
            catch (Exception ex)
            {
                UtilLog.Error("Handler Receive Data Error", ex);
                return false;
            }
        }

        private void HandleTesterProgram()
        {
            bool isDownloadSuccess = true;

            try
            {
                //generate recipe info file & download recipe
                if (machine.TesterType == UtilConstants.TESTER_STATEC)
                {
                    UtilCommonMethod.CreateStatecFile(lotEntity, gTester.StartupFilePath, gTester.StartupFileName, "ST1");
                    isDownloadSuccess = DownloadStatecRecipe();
                }
                else if (machine.TesterType == UtilConstants.TESTER_EAGLE)
                {
                    UtilCommonMethod.CreateEagleFile(lotEntity, gTester.StartupFilePath, gTester.StartupFileName);
                }

                //handle summary logs
               // CleanTesterLog();
            }
            catch (Exception ex)
            {
                UtilLog.Error(ex);
                isDownloadSuccess = false;
            }

            //download recipe fail
            if (!isDownloadSuccess)
            {
                sMsg = lotEntity.LotNo + ",程序下载失败,请重试！";
                SetRealtimeMessage(UtilConstants.COLOR_RED_NAME, sMsg);
                return;
            }

            //show messages about download info 
            sMsg = lotEntity.LotNo + ",Tester程序下载成功！";
            SetRealtimeMessage(UtilConstants.COLOR_BLUE_NAME, sMsg);

            //new lot command send to Handler
            ClearProductCount();
        }

        private bool HandleTesterRMS()
        {
            try
            {
                bool isDownloadSuccess = true;

                //generate recipe info file & download recipe
                if (machine.TesterType == UtilConstants.TESTER_STATEC)
                {
                    UtilCommonMethod.CreateStatecFile(lotEntity, gTester.StartupFilePath, gTester.StartupFileName, "ST1");
                    isDownloadSuccess = DownloadStatecRecipe();
                }
                else if (machine.TesterType == UtilConstants.TESTER_EAGLE)
                {
                    UtilCommonMethod.CreateEagleFile(lotEntity, gTester.StartupFilePath, gTester.StartupFileName);
                }

                //handle summary logs
               // CleanTesterLog();
                return isDownloadSuccess;
            }
            catch (Exception ex)
            {
                UtilLog.Error(ex);
                return false;
            }
        }

        private bool DownloadStatecRecipe()
        {
            try
            {
                string sMsg = UtilNetwork.DownloadRecipe(lotEntity.RecipePath, gTester.RecipeDownloadPath, "job");

                if (sMsg == "OK")
                {
                    UtilLog.Info("Download job Recipe successfully");
                    sMsg = UtilNetwork.DownloadRecipe(lotEntity.RecipePath, gTester.RecipeDownloadPath, "tst");

                    if (sMsg == "OK")
                    {
                        UtilLog.Info("Download tst Recipe successfully");
                        sMsg = UtilNetwork.DownloadRecipe(lotEntity.RecipePath, gTester.RecipeDownloadPath, "jif");
                    }
                }

                if (sMsg != "OK")
                {
                    UtilLog.Info("Download Recipe successfully,starting delete recipe");
                    UtilCommonMethod.DeleteFile(gTester.RecipeDownloadPath, "*.tst");
                    UtilCommonMethod.DeleteFile(gTester.RecipeDownloadPath, "*.jif");
                    UtilCommonMethod.DeleteFile(gTester.RecipeDownloadPath, "*.job");
                    return false;
                }
                else
                {
                    UtilLog.Info("Download jif Recipe successfully");
                    return true;
                }
            }
            catch (Exception ex)
            {
                UtilLog.Error(ex);
                return false;
            }
        }

        #endregion

        #region "Eagle Recipe Download"
        private bool HanldeEagleRecipeMessage(string receivedmessage)
        {
            try
            {
                bool IsHandleRecipeMessageSuccess = true;

                UtilLog.Info("Get Recipe Info From WebService :" + receivedmessage);

                string[] sArray = receivedmessage.Split(',');
                lotEntity.LotNo = txtLotNo.Text.Trim();
                lotEntity.DeviceId = lotEntity.LotNo.StartsWith("COR") ? sArray[1].ToString() : sArray[0].ToString();
                lotEntity.RecipeName = sArray[1].ToString();

                if (sArray[2] != "")
                {
                    lotEntity.LotQty = int.Parse(sArray[2].ToString());
                }
                else
                {
                    lotEntity.LotQty = 0;
                }

                lotEntity.IsInsertionLot = false;
                lotEntity.IsCamstarStatusUpdated = false;

                IsHandleRecipeMessageSuccess = HandleTesterRMS();
                return IsHandleRecipeMessageSuccess;
            }
            catch (Exception ex)
            {
                UtilLog.Error("Handler Receive Data Error", ex);
                return false;
            }
        }
        #endregion
        #endregion

        #region "1.3 End Lot ,Get Bin Data"
        private void btnEndLot_Click(object sender, EventArgs e)
        {
            if (txtLotNo.Text.Trim().Length < 2)
            {
                this.txtLotNo.Text = "";
                return;
            }

            if (!txtLotNo.Text.Trim().StartsWith("X"))
            {
                this.txtLotNo.Text = "";
                return;
            }

            string sName = "", sFullname = ""; //临时存放文件名称
            DirectoryInfo gDir = new DirectoryInfo(gTester.SummaryLogPath);
            bool isFindSBL = false;

            try
            {
                //update camstar recipe status to recipe-complete
                if (!lotEntity.IsCamstarStatusUpdated)
                {
                    sMsg = tsummary_Service.UpdateCamstarStatus(machine.HandlerNo, "Recipe-Complete");

                    if (sMsg == "")
                    {
                        lotEntity.IsCamstarStatusUpdated = true;
                        UtilLog.Info("Update Machine: " + machine.HandlerNo + " Recipe Status Success!");
                        iniFile.Write("CurrentLot", "IsCamstarStatusUpdated", lotEntity.IsCamstarStatusUpdated.ToString());
                    }
                }

                //check machine secs/gem communication status
                if (!gv_WinSecs.Connected)
                {
                    sMsg = "Handler 断线，请先检查Handler连线状况!";
                    SetRealtimeMessage(UtilConstants.COLOR_RED_NAME, sMsg);
                    UtilMessage.ShowWarn(sMsg);
                    return;
                }

                softBins.Clear();

                // read summary log files
                foreach (FileInfo file in gDir.GetFiles())
                {
                    sName = file.Name;
                    sFullname = file.FullName;

                    if (HandleTesterLog(sFullname, sName))
                    {
                        isFindSBL = true;

                        sMsg = "读取Tester Log File: " + sName;
                        SetRealtimeMessage(UtilConstants.COLOR_BLUE_NAME, sMsg);

                        Hashtable testSoftBins = new Hashtable();
                        if (gTester.Type == UtilConstants.TESTER_STATEC)
                        {
                            testSoftBins = LogHandler.readStatecFile(sFullname, sName);
                        }
                        else if (gTester.Type == UtilConstants.TESTER_EAGLE)
                        {
                            testSoftBins = LogHandler.readEAGLEFile(sFullname, sName);
                        }

                        if (lotEntity.SoftBins.Count > 0)
                        {
                            foreach (Bin softbin in testSoftBins.Values)
                            {
                                if (lotEntity.SoftBins.ContainsKey(softbin.Name))
                                {
                                    Bin softBin2 = (Bin)lotEntity.SoftBins[softbin.Name];
                                    softbin.Qty += softBin2.Qty;
                                    lotEntity.SoftBins.Remove(softbin.Name);
                                }

                                lotEntity.SoftBins.Add(softbin.Name, softbin);
                                UtilLog.Info("End lot: Get SoftBin Again " + softbin.Name + " = " + softbin.Qty);
                            }
                        }
                        else
                        {
                            lotEntity.SoftBins = testSoftBins;
                        }
                    }

                    UtilCommonMethod.MoveFile(sFullname, sName, gTester.EWBLogPath);
                }

                if (!isFindSBL)
                {
                    sMsg = lotEntity.LotNo + " 找不到summary log！";
                    UtilMessage.ShowWarn(sMsg);
                    SetRealtimeMessage(UtilConstants.COLOR_RED_NAME, sMsg);
                    return;
                }
                else
                {
                    btnEndLot.Enabled = false;
                    SaveLotInfoToFile();
                }

                //get hard bin data
                sTag = UtilConstants.QUERY_BIN_QTY;
                QueryHardBinQty();
            }
            catch (Exception ex2)
            {
                UtilLog.Error(ex2);
            }
        }

        #region "Handle Tester Log"

        private bool HandleTesterLog(string sFullname, string sName)
        {
            try
            {
                bool isProdLog = false;
                sName = sName.ToUpper();
                if (sName.StartsWith("COR"))
                {
                    UtilCommonMethod.MoveFile(sFullname, sName, gTester.EWBLogPath);
                    return isProdLog;
                }

                if (sName.StartsWith("QSB"))
                {
                    UtilCommonMethod.MoveFile(sFullname, sName, gTester.EWBLogPath);
                    return isProdLog;
                }

                if (sName.IndexOf("CCS") > 0)
                {
                    UtilCommonMethod.MoveFile(sFullname, sName, gTester.EWBLogPath);
                    return isProdLog;
                }

                if (sName.IndexOf(gTester.SummaryLogFilter) > 0)
                {
                    if (sName.Contains(lotEntity.LotNo.Substring(0,10)))
                    {
                        UtilLog.Info("HandleTesterLog: " + sName + " successfully!");
                        isProdLog = true;
                    }
                }

                return isProdLog;
            }
            catch (Exception ex2)
            {
                UtilLog.Error(ex2);
                return false;
            }
        }

        #endregion

        #endregion

        #region "1.4 Select Reject Bin, Do Retest"

        private void btnReTest_Click(object sender, EventArgs e)
        {
            try
            {
                bool boolRetest = false;
                string sLotNo = txtLotNo.Text.Trim().ToUpper();

                if (sLotNo.Length < 2 || !sLotNo.StartsWith("X"))
                {
                    return;
                }

                foreach (Bin hardbin in gH_HardBins.Values)
                {
                    if (hardbin.Name.Contains("Mark"))
                        continue;

                    if (hardbin.Name.Substring(7, hardbin.Name.Length - 7) == "1")
                        continue;
                    if (((CheckBox)(this.Controls.Find("chk" + hardbin.Name, true)[0])).Checked)
                    {
                        boolRetest = true;
                        break;
                    }
                }

                if (!boolRetest)
                {
                    sMsg = "请先选择重测的项目！";
                    UtilMessage.ShowInfo(sMsg);
                    return;
                }

                //update lot no text box
                if (!lotEntity.LotNo.Contains("REJ"))
                {
                    txtLotNo.Text = lotEntity.LotNo + "REJ1";
                }
                else
                {
                    int rejtimes = int.Parse(lotEntity.LotNo.Substring(lotEntity.LotNo.Length - 1)) + 1;
                    txtLotNo.Text = lotEntity.LotNo.Substring(0, lotEntity.LotNo.Length - 1) + rejtimes;
                }
                lotEntity.LotNo = txtLotNo.Text.Trim().ToUpper(); // Modify by nancy 2015/10/16

                UtilLog.Info("LotNo = " + lotEntity.LotNo + "开始下载程序");

                if (!HandleTesterRMS())
                {
                    sMsg = "Rej Test程序下载失败，请重试！";
                    SetRealtimeMessage(UtilConstants.COLOR_RED_NAME, sMsg);
                    UtilMessage.ShowError(sMsg);

                    int rejtimes = int.Parse(lotEntity.LotNo.Substring(lotEntity.LotNo.Length - 1)) - 1;
                    if (rejtimes == 0)
                    {
                        lotEntity.LotNo = lotEntity.LotNo.Substring(0, 10);
                    }
                    else
                    {
                        lotEntity.LotNo = lotEntity.LotNo.Substring(0, lotEntity.LotNo.Length - 1) + rejtimes;
                    }
                    txtLotNo.Text = lotEntity.LotNo;
                    return;
                }
                else
                {
                    //show messages about download info 
                    sMsg = lotEntity.LotNo + ",Tester程序下载成功！";
                    SetRealtimeMessage(UtilConstants.COLOR_BLUE_NAME, sMsg);
                    btnRetry.Enabled = false;
                }

                foreach (Bin hardbin in gH_HardBins.Values)
                {
                    if (hardbin.Name.Contains("Mark"))
                        continue;

                    if (hardbin.Name.Substring(7, hardbin.Name.Length - 7) == "1")
                        continue;

                    if (((CheckBox)(this.Controls.Find("chk" + hardbin.Name, true)[0])).Checked)
                    {
                        ((TextBox)(this.Controls.Find("txt" + hardbin.Name, true)[0])).Text = "0";

                        ((CheckBox)(this.Controls.Find("chk" + hardbin.Name, true)[0])).Checked = false;

                        UtilLog.Info("Retest hard bin name = " + hardbin.Name + " Qty is:" + hardbin.Qty);

                        hardbin.Qty = 0;

                        if (hardbin.Name.Contains("_"))
                            continue;
                        string binnum = hardbin.Name.Substring(7, hardbin.Name.Length - 7);

                        if (hardAndSoftBin.Count > 0 && hardAndSoftBin.ContainsKey(binnum))
                        {
                            string sSoftBins = hardAndSoftBin[binnum].ToString();
                            UtilLog.Info("Retest softbins = " + sSoftBins);
                            string[] array = sSoftBins.Split(',');

                            for (int j = 0; j < array.Length; j++)
                            {
                                if (lotEntity.SoftBins.Contains("SoftBin" + array[j]))
                                {
                                    Bin softbin = (Bin)lotEntity.SoftBins["SoftBin" + array[j]];
                                    UtilLog.Info("Removed softbin: " + softbin.Name + " Qty is:" + softbin.Qty);
                                    lotEntity.SoftBins.Remove(softbin.Name);
                                }
                            }
                        }
                    }
                }

                //send new lot info to handler
                ClearProductCount();

                //save prod lot info into lot file
                txtLotNo.Enabled = false;
                SaveLotInfoToFile();
                btnEndLot.Enabled = true;
                btnSubmit.Enabled = false;
            }
            catch (Exception ex)
            {
                UtilLog.Error(ex);
            }
        }

        #endregion

        #region "1.5 Confirm Test Result ,Submit To Camstar"
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                //hard bin data
                string hardbins = "", softbins = "";
                int goodhardbinsQty = 0, goodsoftbinsQty = 0;

                if (lotEntity.IsInsertionLot)
                {
                    sMsg = " LOT ID:" + lotEntity.LotNo + " New Insertion lot,不上传reject到camstar，请手动上传！";
                    UtilMessage.ShowWarn(sMsg);
                    SetRealtimeMessage(UtilConstants.COLOR_RED_NAME, sMsg);

                    ClearBins();

                    lotEntity.LotNo = "";
                    SaveLotInfoToFile();
                    txtLotNo.Text = "";
                    txtLotNo.Enabled = true;
                    btnSubmit.Enabled = false;
                    return;
                }

                if (txtHardBin1.Text.Trim() == "")
                {
                    sMsg = "Hard Bin 信息为空，请稍后再试！";
                    UtilMessage.ShowWarn(sMsg);
                    SetRealtimeMessage(UtilConstants.COLOR_RED_NAME, sMsg);
                    return;
                }

                for (int i = 1; i <= 7; i++)
                {
                    string binName = "HardBin" + i;
                    string binQty = ((TextBox)(this.Controls.Find("txt" + binName, true)[0])).Text;
                    if (binQty != "" && binQty != "0")
                    {
                        if (i == 1)
                        {
                            goodhardbinsQty = int.Parse(binQty);
                        }
                        else
                        {
                            hardbins += "FTBIN" + i + "," + binQty + ";";
                        }
                    }
                }

                if (txtVision_Reject.Text.Trim().Length > 0 && txtVision_Reject.Text.Trim() != "0")
                {
                    string binName = "打印错误_INCORRECT_MARK";
                    string binQty = txtVision_Reject.Text;
                    hardbins += binName + "," + binQty + ";";
                }

                if (txtIso_Reject.Text.Trim().Length > 0 && txtIso_Reject.Text.Trim() != "0")
                {
                    string binName = "绝缘性不良_ISO_FAILURE";
                    string binQty = txtIso_Reject.Text;
                    hardbins += binName + "," + binQty + ";";
                }

                UtilLog.Info(lotEntity.LotNo + " hardbins = " + hardbins);
                foreach (Bin softbin in lotEntity.SoftBins.Values)
                {
                    if (softbin.Name == "SoftBin1")
                    {
                        goodsoftbinsQty = softbin.Qty;
                    }
                    else
                    {
                        softbins += softbin.Name + "," + softbin.Qty + ";";
                    }
                }
                UtilLog.Info(lotEntity.LotNo + " softbins = " + softbins);
                if (hardbins.Length > 2)
                {
                    UtilLog.Info("goodsoftbinsQty = " + goodsoftbinsQty + "; goodhardbinsQty = " + goodhardbinsQty);
                    if (goodsoftbinsQty < goodhardbinsQty)
                    {
                        LotEntity_Server lot = ConvertLotEntity();
                        lot.Hold_Reason = "goodsoftbinsQty = " + goodsoftbinsQty + "; goodhardbinsQty = " + goodhardbinsQty;
                        tsummary_Service.HoldLot(UtilCommonMethod.SerializeObject(lot), UtilCommonMethod.SerializeObject(machine));
                        sMsg = "Good SoftBins数量 " + goodsoftbinsQty + " < Good HardBins数量 " + goodhardbinsQty + " Lot Hold！";
                        SetRealtimeMessage(UtilConstants.COLOR_RED_NAME, sMsg);
                        EndSubmit();
                        return;
                    }
                    string[] hardbinlist = hardbins.TrimEnd(';').Split(';');

                    if (tsummary_Service.UpdateRejectDataToCamstar(lotEntity.LotNo.ToUpper().Trim().Substring(0, 10), machine.HandlerNo, hardbinlist))
                    {
                        sMsg = "成功上传REJECT DATA到CAMSTAR！";
                        SetRealtimeMessage(UtilConstants.COLOR_BLUE_NAME, sMsg);
                    }
                    else
                    {
                        sMsg = "上传REJECT DATA到CAMSTAR失败,请先在Camstar中track in lot到机台！";
                        SetRealtimeMessage(UtilConstants.COLOR_RED_NAME, sMsg);
                        UtilMessage.ShowInfo(sMsg);
                        return;
                    }
                }

                string sresult = tsummary_Service.UploadTestResult(machine.HandlerNo, lotEntity.LotNo.Trim(), softbins, hardbins);
                UtilLog.Info("sresult = " + sresult + "; Length = " + sresult.Length);
                if (sresult.Length > 3)
                {
                    sMsg = "上传测试结果到数据库失败！";
                    SetRealtimeMessage(UtilConstants.COLOR_RED_NAME, sMsg);
                }
                else
                {
                    sMsg = "上传测试结果到数据库成功！";
                    SetRealtimeMessage(UtilConstants.COLOR_BLUE_NAME, sMsg);
                }

                EndSubmit();
            }
            catch (Exception ex)
            {
                UtilLog.Error(ex);
            }
        }

        private void EndSubmit()
        {
            ClearBins();

            lotEntity.LotNo = "";
            SaveLotInfoToFile();
            txtLotNo.Text = "";
            txtLotNo.Enabled = true;
            btnSubmit.Enabled = false;
        }
        private LotEntity_Server ConvertLotEntity()
        {
            LotEntity_Server lotserver = new LotEntity_Server();

            lotserver.DeviceId = lotEntity.DeviceId;
            lotserver.LotNo = lotEntity.LotNo.Substring(0,10);
            lotserver.LotQty = lotEntity.LotQty;
            lotserver.HardBins = new DictionaryEntry[gH_HardBins.Count];
            gH_HardBins.CopyTo(lotserver.HardBins, 0);

            lotserver.SoftBins = new DictionaryEntry[lotEntity.SoftBins.Count];
            lotEntity.SoftBins.CopyTo(lotserver.SoftBins, 0);
            return lotserver;
        }
        #endregion

        #region "Samil Tech Secs/Gem Communication"
        private void SecsMessageHandler(object obj)
        {
            SECSTransaction trans = (SECSTransaction)obj;

            if (trans.Secondary.Stream == 1 && trans.Secondary.Function == 14)
            {
                sMsg = "Tester OUI和Handler 连线成功！";
                SetRealtimeMessage(UtilConstants.COLOR_BLUE_NAME, sMsg);
            }

            if (trans.Secondary.Stream == 1 && trans.Secondary.Function == 4)
            {
                if (trans.Tag == UtilConstants.QUERY_BIN_QTY)
                {
                    gIsReceived = true;
                    sTag = string.Empty;

                    HandleQueryHardBinQty(trans);
                }
            }

            if (trans.Secondary.Stream == 2 && trans.Secondary.Function == 16)
            {
                HandleUpdateLotInfo(trans);
            }

            if (trans.Secondary.Stream == 2 && trans.Secondary.Function == 42)
            {
                HandleRemoteCommand(trans);
            }
        }

        private void ConnectStatusUpdate(object obj)
        {
            if (gv_WinSecs.Connected)
            {
                machine.ConnectionStatus = "Connected";
                SendS1F13();
            }
            else
            {
                machine.ConnectionStatus = "Disconnected";
                sMsg = "Tester OUI和Handler 通讯中断！";
                SetRealtimeMessage(UtilConstants.COLOR_RED_NAME, sMsg);
            }
        }

        private void QueryHardBinQty()
        {
            try
            {
                gIsReceived = false;
                TmrRetry.Enabled = true;
                sTag= UtilConstants.QUERY_BIN_QTY;

                if (gv_WinSecs.Connected)
                {
                    SECSTransaction trans = new SECSTransaction(1, 3);
                    trans.Primary.Root.Name = "SSR";
                    trans.Primary.Root.Description = "Selected equipment status request";
                    SECSItem item = trans.Primary.Root.AddNew("L");

                    trans.Tag = UtilConstants.QUERY_BIN_QTY;

                    foreach (Bin hardbin in gHandler.Bins)
                    {
                        SECSItem newSvid = item.AddNew("SVID");
                        newSvid.Format = SECS_FORMAT.U4;
                        newSvid.Value = hardbin.SVID;
                    }
                    trans.ReplyExpected = true;
                    trans.Send(gv_WinSecs);

                    btnReTest.Enabled = true;
                    btnSubmit.Enabled = true;

                    sMsg = "开始从Handler获取Hard Bin数据！";
                    SetRealtimeMessage(UtilConstants.COLOR_BLUE_NAME, sMsg);
                }
                else
                {
                    btnRetry.Enabled = true;
                    sMsg = "Handler断线，请重新连线后在重试！";
                    UtilMessage.ShowWarn(sMsg);
                    UtilLog.Info(sMsg);
                    SetRealtimeMessage(UtilConstants.COLOR_RED_NAME, sMsg);
                }
            }
            catch (Exception ex)
            {
                btnRetry.Enabled = true;
                UtilLog.Error(ex);
            }
        }

        private void HandleQueryHardBinQty(SECSTransaction trans)
        {
            try
            {
                int i = 0,qty=0;
                string binname = string.Empty;
                foreach (Bin hardbin in gHandler.Bins)
                {
                    qty = int.Parse(trans.Secondary.Root.Item(0).Item(i).Value.ToString());
                    binname = hardbin.Name;
                    i++;
                    if (qty == 0)
                        continue;

                    UtilLog.Info(lotEntity.LotNo + ",Hard Bin " + binname + " Qty: " + qty);

                    if (gH_HardBins.Contains(binname))
                    {
                        Bin hardbin2 = (Bin)gH_HardBins[binname];
                        qty += hardbin2.Qty;
                        gH_HardBins.Remove(binname);
                    }

                    gH_HardBins.Add(binname, new Bin(binname,qty));
                }

                sTag = "";
                MethodInvoker In = new MethodInvoker(UpdateHardBinText);
                this.BeginInvoke(In);
            }
            catch (Exception ex)
            {
                UtilLog.Error(ex);
            }
        }

        public void SendS1F13()
        {
            try
            {
                SECSTransaction trans = new SECSTransaction(1, 13);
                trans.Primary.Root.Name = "CR";
                trans.Primary.Root.Description = "Establish communications request";
                SECSItem mdln = trans.Primary.Root.AddNew("L").AddNew("MDLN");
                mdln.Format = SECS_FORMAT.ASCII;
                mdln.Value = "";

                SECSItem softver = mdln.Duplicate();
                softver.Format = SECS_FORMAT.ASCII;
                softver.Value = "";

                trans.ReplyExpected = true;
                trans.Send(gv_WinSecs);
            }
            catch (Exception ex)
            {
                UtilLog.Error(ex);
            }
        }

        private void NewLot()
        {
            try
            {
                gIsReceived = false;
                TmrRetry.Enabled = true;
                sTag = UtilConstants.NEW_LOT;

                if (gv_WinSecs.Connected)
                {
                    SECSTransaction trans = gv_HostLib.Find("S2F15");
                    trans.Tag = UtilConstants.NEW_LOT;

                    SECSItem lotitem = trans.Primary.Root.Item(0).Item(0);
                    lotitem.Item(0).Format = SECS_FORMAT.U2;
                    lotitem.Item(0).Value = 131;
                    lotitem.Item(1).Value = txtLotNo.Text.Trim().ToUpper();

                    SECSItem deviceitem = lotitem.Duplicate();
                    deviceitem.Item(0).Value = 130;
                    deviceitem.Item(1).Value = lotEntity.DeviceId;

                    trans.Send(gv_WinSecs);
                    btnRetry.Enabled = false;
                }
                else
                {
                    btnRetry.Enabled = true;
                    sMsg = "Handler断线，请点击Retry重试！";
                    UtilMessage.ShowWarn(sMsg);
                    SetRealtimeMessage(UtilConstants.COLOR_RED_NAME, sMsg);
                }
            }
            catch (Exception ex)
            {
                btnRetry.Enabled = true;
                sMsg = "Handler断线，请点击Retry重试！" + ex.Message;
                UtilMessage.ShowWarn(sMsg);
                SetRealtimeMessage(UtilConstants.COLOR_RED_NAME, sMsg);

                UtilLog.Error(ex);
            }
        }

        private void ClearProductCount()
        {
            try
            {
                gIsReceived = false;
                TmrRetry.Enabled = true;
                sTag = UtilConstants.CLEAR_PROD_COUNT;

                if (gv_WinSecs.Connected)
                {
                    SECSTransaction trans = gv_HostLib.Find("S2F41");
                    trans.Tag = UtilConstants.CLEAR_PROD_COUNT;
                    trans.Primary.Root.Item(0).Item(0).Value = "CNTRST";
                    trans.Primary.Root.Item(0).Item(1).Item(0).Delete();
                    trans.Send(gv_WinSecs);
                    btnRetry.Enabled = false;
                }
                else
                {
                    btnRetry.Enabled = true;
                    sMsg = "Handler断线，请点击Retry重试！";
                    UtilMessage.ShowWarn(sMsg);
                    SetRealtimeMessage(UtilConstants.COLOR_RED_NAME, sMsg);
                }
            }
            catch (Exception ex)
            {
                btnRetry.Enabled = true;
                sMsg = "Handler断线，请点击Retry重试！" + ex.Message;
                UtilMessage.ShowWarn(sMsg);
                SetRealtimeMessage(UtilConstants.COLOR_RED_NAME, sMsg);

                UtilLog.Error(ex);
            }
        }

        private void HandleRemoteCommand(SECSTransaction trans)
        {
            try
            {
                int ack = int.Parse(trans.Secondary.Root.Item(0).Item(0).Value.ToString());

                if (ack != 0)
                {
                    MethodInvoker In = new MethodInvoker(UpdateRetryButton);
                    this.BeginInvoke(In);
                    sMsg = "清除Handler上Lot信息失败！";
                    UtilMessage.ShowError(sMsg);
                    SetRealtimeMessage(UtilConstants.COLOR_RED_NAME, sMsg);
                }
                else
                {
                    gIsReceived = true;
                    sTag = string.Empty;

                    sMsg = "清除Handler上Lot信息成功！";
                    SetRealtimeMessage(UtilConstants.COLOR_BLUE_NAME, sMsg);

                    NewLot();
                }
            }
            catch (Exception ex)
            {
                UtilLog.Error(ex);
            }
        }

        private void HandleUpdateLotInfo(SECSTransaction trans)
        {
            try
            {
                int eac = int.Parse(trans.Secondary.Root.Item(0).Value.ToString());

                if (eac != 0)
                {
                    MethodInvoker In = new MethodInvoker(UpdateRetryButton);
                    this.BeginInvoke(In);
                    sMsg = "发送Lot信息到Handler失败！";
                    UtilMessage.ShowError(sMsg);
                    SetRealtimeMessage(UtilConstants.COLOR_RED_NAME, sMsg);
                }
                else
                {
                    gIsReceived = true;
                    sTag = string.Empty;

                    sMsg = "发送Lot信息到Handler成功！";
                    SetRealtimeMessage(UtilConstants.COLOR_BLUE_NAME, sMsg);
                }
            }
            catch (Exception ex)
            {
                UtilLog.Error(ex);
            }
        }

        #endregion

        private void btnRetry_Click(object sender, EventArgs e)
        {
            if (!gv_WinSecs.Connected)
            {
                SendS1F13();
                return;
            }

            if (gIsReceived)
            {
                return;
            }

            if (sTag == UtilConstants.QUERY_BIN_QTY)
            {
                QueryHardBinQty();
            }

            if (sTag == UtilConstants.CLEAR_PROD_COUNT)
            {
                ClearProductCount();
            }

            if (sTag == UtilConstants.NEW_LOT)
            {
                NewLot();
            }
        }

        private void UpdateHardBinText()
        {
            try
            {
                foreach (Bin hardbin in gH_HardBins.Values)
                {
                    TextBox txtHardBin = (TextBox)(this.Controls.Find("txt" + hardbin.Name, true)[0]);
                    txtHardBin.Text = hardbin.Qty.ToString();
                }

                //save lot info into config files
                SaveLotInfoToFile();
            }
            catch (Exception ex)
            {
                UtilLog.Error(ex);
            }
        }

        private void UpdateRetryButton()
        {
            btnRetry.Enabled = true;
        }

        private void HandlerSecsGemConfig_Update(object objmachine)
        {
            MachineConfig machineconfig = (MachineConfig)objmachine;

            gv_HostAgent = new Samil_HostAgent();
            gv_HostAgent.SecsMessageIn += SecsMessageHandler;
            gv_HostAgent.ConnectStatusChange += ConnectStatusUpdate;

            gv_WinSecs.Hsms.RemoteIPAddress = machineconfig.HandlerIp;
            gv_WinSecs.Hsms.RemoteIPPort = (uint)machineconfig.HandlerPort;
            if (!gv_WinSecs.PortIsOpen)
            {
                gv_WinSecs.OpenPort(gv_HostAgent, gv_HostLib);
            }
            else
            {
                gv_WinSecs.ClosePort();
                gv_WinSecs.OpenPort(gv_HostAgent, gv_HostLib);
            }
            UtilXml.UpdateXmlNodeAttributeValue(clientAppConfig, "//SBLConfig//McConfig//HandlerIp", machineconfig.HandlerIp);
            UtilXml.UpdateXmlNodeAttributeValue(clientAppConfig, "//SBLConfig//McConfig//HandlerPort", machineconfig.HandlerPort.ToString());
            
        }

        private void menuSecsGemConfig_Click(object sender, EventArgs e)
        {
            frmSecsGem frmsecgem = new frmSecsGem(machine);
            frmsecgem.TopMost = true;
            frmsecgem.Text = machine.HandlerType + " Secs Gem Config";
            frmsecgem.Show();
        }

        private void menuSoftBins_Click(object sender, EventArgs e)
        {
            frmSoftBins frmsoftin = new frmSoftBins(lotEntity.SoftBins);
            frmsoftin.TopMost = true;
            frmsoftin.Text = machine.TesterType + " Soft Bins";
            frmsoftin.Show();
        }

        private void menuLotInfo_Click(object sender, EventArgs e)
        {
            frmLotInfo frmlot = new frmLotInfo(lotEntity);
            frmlot.TopMost = true;
            frmlot.Text = lotEntity.LotNo + " Info";
            frmlot.Show();
        }

        private void menuClearlot_Click(object sender, EventArgs e)
        {
            try
            {
                LotEntity_Server lot = ConvertLotEntity();

                using (frmResetLot frmreset = new frmResetLot(lot, machine))
                {
                    if (frmreset.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }
                }

                UtilLog.Info( lotEntity.LotNo+ " Manual Reset Current Lot Info Success!");
                txtLotNo.Text = "";
                txtLotNo.Enabled = true;
                lotEntity.LotNo = "";
                SaveLotInfoToFile();
                sMsg = "成功清除当前LOT信息！";
                SetRealtimeMessage(UtilConstants.COLOR_BLUE_NAME, sMsg);
            }
            catch (Exception ex)
            {
                UtilLog.Error(ex);
            }
        }

        private void CleanTesterLog()
        {
            string sName = "", sFullname = "";
            DirectoryInfo testerlogdir = new DirectoryInfo(gTester.SummaryLogPath);
            try
            {
                foreach (FileInfo file in testerlogdir.GetFiles())
                {
                    sName = file.Name.ToUpper();
                    sFullname = file.FullName;
                    UtilCommonMethod.MoveFile(sFullname, sName, gTester.EWBLogPath);
                }
            }
            catch (Exception ex2)
            {
                UtilLog.Error("Handle Directory Log Err:", ex2);
            }
        }

        private void SaveLotInfoToFile()
        {
            if (lotEntity.LotNo.Length > 0)
            {
                // lot info 
                iniFile.Write("CurrentLot", "LotNo", txtLotNo.Text.Trim());
                iniFile.Write("CurrentLot", "LotQty", lotEntity.LotQty.ToString());
                iniFile.Write("CurrentLot", "DeviceId", lotEntity.DeviceId);
                iniFile.Write("CurrentLot", "RecipeName", lotEntity.RecipeName);
                iniFile.Write("CurrentLot", "RecipePath", lotEntity.RecipePath);
                iniFile.Write("CurrentLot", "SpmGdcal", lotEntity.SpmGdcal);
                iniFile.Write("CurrentLot", "IsInsertionLot", lotEntity.IsInsertionLot.ToString());
                iniFile.Write("CurrentLot", "IsCamstarStatusUpdated", lotEntity.IsCamstarStatusUpdated.ToString());
                // soft bin data
                for (int i = 1; i <= 30; i++)
                {
                    if (lotEntity.SoftBins.Contains("SoftBin" + i))
                    {
                        Bin softBin = (Bin)lotEntity.SoftBins["SoftBin" + i];
                        iniFile.Write("SoftBin", "SoftBin" + i, softBin.Qty.ToString());
                    }
                    else
                    {
                        iniFile.Write("SoftBin", "SoftBin" + i, "0");
                    }
                }

                //hard bin data
                foreach (Bin hardbin in gH_HardBins.Values)
                {
                    iniFile.Write("HardBin", hardbin.Name, hardbin.Qty);
                }
            }
            else
            {
                // lot info 
                iniFile.Write("CurrentLot", "LotNo", "");
                iniFile.Write("CurrentLot", "LotQty", "0");
                iniFile.Write("CurrentLot", "DeviceId", "");
                iniFile.Write("CurrentLot", "RecipeName", "");
                iniFile.Write("CurrentLot", "RecipePath", "");
                iniFile.Write("CurrentLot", "SpmGdcal", "");
                iniFile.Write("CurrentLot", "IsInsertionLot", "false");
                iniFile.Write("CurrentLot", "IsCamstarStatusUpdated", "false");

                // soft bin data
                for (int i = 1; i <= 30; i++)
                {
                    iniFile.Write("SoftBin", "SoftBin" + i, "0");
                }

                //hard bin data
                foreach (Bin hardbin in gHandler.Bins)
                {
                    iniFile.Write("HardBin", hardbin.Name, "0");
                }
            }
        }

        private void ClearBins()
        {
            lotEntity.SoftBins.Clear();
            //hard bin data
            foreach (Bin hardbin in gH_HardBins.Values)
            {
                ((TextBox)(this.Controls.Find("txt" + hardbin.Name, true)[0])).Text = "";
                if (hardbin.Name.Substring(7, hardbin.Name.Length - 7) != "1" && (!hardbin.Name.Contains("Mark")))
                    ((CheckBox)(this.Controls.Find("chk" + hardbin.Name, true)[0])).Checked = false;
            }

            gH_HardBins.Clear();
        }

        private void HandleRealtimeMessage(string sMsg)
        {
            UtilLog.Info(sMsg);
            UtilMessage.ShowWarn(sMsg);
            SetRealtimeMessage(UtilConstants.COLOR_RED_NAME, sMsg);
        }

        private void SetRealtimeMessage(string messageColorName, string message)
        {
            try
            {
                if (this.lstRuntimeInfo.InvokeRequired)
                {
                    SetRealtimeMessageCallback d = new SetRealtimeMessageCallback(SetRealtimeMessage);
                    this.Invoke(d, new object[] { messageColorName, message });
                }
                else
                {
                    int itemsCount = this.lstRuntimeInfo.Items.Count;

                    string dtnow = DateTime.Now.ToString("MM/dd HH:mm:ss");

                    if (itemsCount > UtilConstants.LISTBOX_MAX_ITEMS_COUNT)
                    {
                        this.lstRuntimeInfo.Items.RemoveAt(itemsCount - 1);
                    }

                    Color messageColor = Color.White;

                    switch (messageColorName.ToUpper().Split('-')[0])
                    {
                        case UtilConstants.COLOR_BLUE_NAME:
                            messageColor = Color.PowderBlue;
                            break;
                        case UtilConstants.COLOR_GREEN_NAME:
                            messageColor = Color.LawnGreen;
                            break;
                        case UtilConstants.COLOR_YELLOW_NAME:
                            messageColor = Color.Yellow;
                            break;
                        case UtilConstants.COLOR_RED_NAME:
                            messageColor = Color.OrangeRed;
                            break;
                    }

                    this.lstRuntimeInfo.Items.Insert(0, new ColorItem(dtnow + ": " + message, messageColor));
                    UtilLog.Info(message);
                }
            }
            catch (Exception ex)
            {
                UtilLog.Error(ex);
            }
        }

        private void menuLogOut_Click(object sender, EventArgs e)
        {
            using (frmLogin frmLogin = new frmLogin())
            {
                if (frmLogin.ShowDialog() == DialogResult.OK)
                {
                    InitialStatusLabel();
                }
            }
        }

        private void TmrDownloadRecipe_Tick(object sender, EventArgs e)
        {
            try
            {
                TmrDownloadRecipe.Enabled = false;
                // QA department account ,only support one time recipe download
                if (UtilCommonInfo.Dept == UtilConstants.QADEPT)
                {
                    txtLotNo.Text = "";
                    txtLotNo.Enabled = true;
                    this.Hide();
                    frmLogin login = new frmLogin();
                    login.Show();
                    return;
                }

                if (txtLotNo.Text.Contains("CCS"))
                {
                    txtLotNo.Text = "";
                    txtLotNo.Enabled = true;
                    return;
                }

                //COR LOT Only download recipe
                if (!txtLotNo.Text.StartsWith("X"))
                {
                    txtLotNo.Text = "";
                    txtLotNo.Enabled = true;
                    return;
                }

                //save prod lot info into lot file
                txtLotNo.Enabled = false;
                SaveLotInfoToFile();

                UtilLog.Info(lotEntity.LotNo + " Save lot info to LotInfo.ini successfully!");
                btnEndLot.Enabled = true;

                // load hard bin & soft bin relationship
                LoadHardAndSoftBinRelation();

                int insertionQty = int.Parse(tsummary_Service.GetInsertionQty(lotEntity.LotNo));
                UtilLog.Info("Lot No: " + lotEntity.LotNo + " Get Insertion Qty From Camstar: " + insertionQty.ToString());
                if (insertionQty > 1)
                {
                    lotEntity.IsInsertionLot = true;
                    iniFile.Write("CurrentLot", "IsInsertionLot", lotEntity.IsInsertionLot.ToString());
                }
            }
            catch (Exception ex)
            {
                lotEntity.IsInsertionLot = false;
                UtilLog.Error(ex);
            }
        }
        private void TmrRetry_Tick(object sender, EventArgs e)
        {
            if (!gIsReceived)
                btnRetry.Enabled = true;

            TmrRetry.Enabled = false;
        }
    }
}
