using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using ManualAssembly.ServiceReference1;
using System.Diagnostics;

namespace ManualAssembly
{
    public partial class MainPage : Form
    {
        bool isRun;
        private bool isProcessEnd = false;
        bool isBarcodeRun = false;
        bool isLabelMatch;     //验证label是否通过MES认证
        bool LastScrewStatus = true;  //最后一颗螺丝如果是NG，则为false
        string flag_REV1 = "";
        string flag_REV2 = "";
        bool UploadToMES = false;
        string Status_Product;
        string rawdata;
        string ScrewTime;
        string ScrewThread;
        string Count;
        string TotalScrewCount;
        string ScrewStatus;
        bool isEnd;   //产品是否结束组装
        int Number;
        int NGtimes;
        string return_MES;
                string firstScrew;
        bool isScrew;  //新产品是否已打螺丝
        bool OKALLconfirm=true; //OKALL有时接收不止一次，用confirm来滤掉多次发送的信号，需和REV后的OKALL区分开

        //List<string> Torque = new List<string>();
        List<string> Label = new List<string>();
        ServiceReference1.TraceabilitySoapClient Trace = null;
        Thread thread1;
        Thread thread2;
        public MainPage()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;   //可给不同行程控件赋值
            string Path = Application.StartupPath + @"\测试记录";
            if (!Directory.Exists(Path))
            {
                Directory.CreateDirectory(Path);
            }
        }

        private void MainPage_Load(object sender, EventArgs e)
        {
            this.Width = (System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size.Width) / 5;
            this.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size.Height;
            int x = (System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size.Width) * 4 / 5;
            //int y = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size.Height - 1080;
            int y = 0;
            this.SetDesktopLocation(x, y);
            //int Y_bStart = this.Height - 72 - 28;
            ////int X_bStart = (this.Width - 36) / 4;
            //int X_bStart = x-8;
            ////bStart.Location(X_bStart, Y_bStart);
            //bStart.Location = new Point(X_bStart, Y_bStart);

            SPort.Instance.ConnectBarcode();
            SPort.Instance.ConnectScrew();
            if (BasicLib.Instance.Screw1sp.IsOpen == false)
            {
                richTextBox1.Text += " 连接螺丝批失败" + "\r\n";
            }
            if (BasicLib.Instance.Barcodesp.IsOpen == false)
            {
                richTextBox1.Text += " 连接扫码枪失败" + "\r\n";
            }

            if (BasicLib.Instance.TorqueUnit == "kgf.cm")
            {
                lTorqueUnit.Text = "kgf.cm";
            }
            else
            {
                lTorqueUnit.Text = "N.m";
            }
            if (BasicLib.Instance.ScrewThreadDisplay == false)
            {
                lThread.Hide();
                lThreaddata.Hide();
                lThreadUnit.Hide();
            }
            if (BasicLib.Instance.ScrewTimeDisplay == false)
            {
                lTime.Hide();
                lTimedata.Hide();
                lTimeUnit.Hide();
            }
            Trace = new TraceabilitySoapClient();
            try
            {
                //Trace.SFC_GetLastTestResultNewPlatform("aaa");
            }
            catch (Exception)
            {

                lNGtype.Text = "连接MES系统出错！";
            }
            bStart.Enabled = true;
            bStart.BackColor = Color.FromArgb(192, 255, 192);
            bStop.Enabled = false;
            bStop.BackColor = Color.Gray;
            bClose.Enabled = true;
            bClose.BackColor = Color.FromArgb(192, 255, 192);
            //timer1.Enabled = true;
            txtProcess.Text = BasicLib.Instance.ThisProcess;
            txtProcess.Enabled = false;
            txtBarcode.Enabled = false;
            pictureBox1.BackgroundImage = Properties.Resources.grey_ball;
            pictureBox2.BackgroundImage = Properties.Resources.grey_ball;
        }
        private void MainPage_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (bStart.Enabled == false)
            {
                if (MessageBox.Show("程序还在运行，您确认要关闭吗?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Stop) == DialogResult.Yes)
                {
                    isProcessEnd = true;
                    if (isRun == true)
                    {
                        isProcessEnd = false;
                    }
                    isBarcodeRun = false;
                    isRun = false;
                    while (isProcessEnd == false)
                    {
                        Thread.Sleep(10);
                    }
                }
            }
            System.Environment.Exit(0);
        }

        /********************************************************************************
        --- 流程1：扫码
        *********************************************************************************/
        private void Process_Barcode()
        {
            while (isBarcodeRun == true)
            {
                if (BasicLib.Instance.Barcodesp.IsOpen == true)
                {
                    SPort.Instance.GetBarcode(out string Barcode_ScrewController);
                    //if (Barcode_ScrewController != "" && Barcode_ScrewController != " ")
                    if (Barcode_ScrewController.Trim() != "")
                    {
                        //if (Label.Count > 2)
                        //{
                        //    Label.Clear();
                        //}
                        bool isExit = false;
                        for (int i = 1; i <= Label.Count; i++)
                        {
                            if (Label[i - 1] == Barcode_ScrewController)
                            {
                                isExit = true;
                            }
                        }
                        if (isExit == true)
                        {
                            continue;
                        }
                        txtBarcode.Text = Barcode_ScrewController;
                        Label.Add(Barcode_ScrewController);
                        if (Label.Count == 2)
                        {
                            if (Label[0] == Label[1])
                            {
                                Label.Remove(Label[1]);
                                continue;
                            }
                        }
                        isLabelMatch = false;
                        string PrevousStationStatus = "";

                        if (Label[0] != "")
                        {
                            try
                            {
                                Stopwatch sw1 = new Stopwatch();
                                sw1.Start();

                                PrevousStationStatus = Trace.SFC_GetLastTestResultNewPlatform(Label[0]);

                                sw1.Stop();
                                label6.Text = (sw1.ElapsedTicks / (double)Stopwatch.Frequency).ToString();
                            }
                            catch (Exception)
                            {
                                lMES.Text = "连接服务器失败";
                                Thread.Sleep(1500);
                                Sound_Fail();
                                continue;
                            }
                        }

                        Return_Status(PrevousStationStatus, out string _Process, out string PreviousStatus);
                        _Process = _Process.ToLower();
                        if (_Process == BasicLib.Instance.ThisProcess && PreviousStatus == "PASS")
                        {
                            isLabelMatch = true;
                        }

                        else if (_Process != BasicLib.Instance.ThisProcess)
                        {
                            lNGtype.Text = "请确认工序名，应为：" + _Process;
                            if (Label.Count != 0)
                            {
                                Label.Remove(Label[0]);
                            }
                            Thread.Sleep(1500);
                            continue;
                        }

                        else if (PreviousStatus == "FAIL")
                        {
                            lNGtype.Text = "请确认上工序状态为PASS";
                            if (Label.Count != 0)
                            {
                                Label.Remove(Label[0]);
                            }
                            Thread.Sleep(1500);
                            continue;
                        }
                        Thread.Sleep(20);
                        BasicLib.Instance.Barcodesp.DiscardInBuffer();
                        BasicLib.Instance.Barcodesp.DiscardOutBuffer();
                    }
                }
            }
        }

        public void ProcessMain()
        {
            while (isRun == true)
            {
                Process();
            }
            isProcessEnd = true;
        }

        private void Process()
        {
            ////********************************************************************************//
            //流程2：开始循环
            //*********************************************************************************//
            if (UploadToMES==true)
            {
                Status_Product = "FAIL";
                rawdata = "";
                ScrewTime = "";
                ScrewThread = "";
                Count = "0";
                TotalScrewCount = "1";
                ScrewStatus = "";
                isEnd = false;   //产品是否结束组装
                Number = 0;
                NGtimes = 0;
                lNGtype.Text = "";
                lMES.Text = "";
                firstScrew = "0";
                isScrew = false;  //新产品是否已打螺丝
            }
            UploadToMES = false;
            while (Label.Count == 0)
            {
                Thread.Sleep(200);
                if (isRun == false)
                {
                    return;
                }
            }
            string mLab = Label[0];

            while (isLabelMatch == false)
            {
                Thread.Sleep(100);
                if (isRun == false)
                {
                    return;
                }
            }

            string MESdata = "";
            string MESdataHeader = "";
            pictureBox2.BackgroundImage = Properties.Resources.grey_ball;
            isEnd = false;

            //********************************************************************************//
            //产品循环
            //********************************************************************************//
            while (isEnd == false)
            {
                if (isRun == false)
                {
                    return;
                }
                BasicLib.Instance.Screw1sp.DiscardInBuffer();
                BasicLib.Instance.Screw1sp.DiscardOutBuffer();
                bool a = object.ReferenceEquals(pictureBox1.BackgroundImage, Properties.Resources.redlight);
                if (Number==0 && a==false)
                {
                    pictureBox1.BackgroundImage = Properties.Resources.grey_ball;
                }

                if (TotalScrewCount != "")
                {
                    //int item1 = Convert.ToInt16(TotalScrewCount) - Convert.ToInt16(Count);
                    if ((Number != Convert.ToInt16(TotalScrewCount)) && Label.Count == 2 && Number != 0)
                    {
                        pictureBox2.BackgroundImage = Properties.Resources.redlight;
                        lNGtype.Text = "产品螺丝未打完，请复位控制器";
                        Status_Product = "FAIL";
                        isEnd = true;
                        Thread.Sleep(3000);
                        break;
                    }
                }
                //if (Label.Count == 2 && item ==0)
                //{
                //    break;
                //}

                //if (rawdata != "" && rawdata.Length == 164)
                //{
                //    BasicLib.Instance.DataProcess(rawdata, out ScrewTorque, out TorqueUnit, out ScrewTime, out ScrewThread,
                //      out Count, out TotalScrewCount, out ScrewStatus, out CurrentDateTime, out Program);
                //    totalcount = Convert.ToInt16(TotalScrewCount);
                //}
                //string Productiondata = production;

                //for (int i = 0; i < totalcount; i++)
                //{
                //int len = rawdata.Length;


                //if (rawdata != "" && rawdata.Length == 164)
                SPort.Instance.ReceiveScrewString(ref rawdata);
                if (rawdata == "")
                {
                    continue;
                }
                int index = rawdata.IndexOf(BasicLib.Instance.TorqueUnit);
                while (index == -1)
                {
                    if (isRun == false)
                    {
                        return;
                    }
                    Thread.Sleep(50);

                    SPort.Instance.ReceiveScrewString(ref rawdata);
index = rawdata.IndexOf(BasicLib.Instance.TorqueUnit);
                    continue;
                }

                DataProcess(rawdata, out string ScrewTorque, out ScrewTime, out ScrewThread, out Count, out TotalScrewCount, out ScrewStatus);
                richTextBox1.Text = "firstScrew:" + firstScrew + "\r\n" + "item:" + Number + "\r\n" + "NGtimes:" + NGtimes + "\r\n" + "Count:" + Count;

                lScrewdata.Text = (Convert.ToInt16(TotalScrewCount) - (Convert.ToInt16(Count))).ToString() + @" / " + Convert.ToInt16(TotalScrewCount).ToString();
                lTorquedata.Text = Convert.ToDouble(ScrewTorque).ToString();
                lThreaddata.Text = Convert.ToDouble(ScrewThread).ToString();
                lTimedata.Text = Convert.ToDouble(ScrewTime).ToString();
                lTorqueUnit.Text = BasicLib.Instance.TorqueUnit;

                if (ScrewTime != "" && ScrewThread != "" && Count != "" && TotalScrewCount != "" && ScrewStatus != "")
                {
                    //if (isScrew == false)
                    //{
                    //    NGtimes = 0;
                    //}

                    //if (ScrewStatus == @"REV__")
                    //{
                    //    flag_REV1 = rawdata.Substring(63, 2);
                    //    if (flag_REV1 != flag_REV2)
                    //    {
                    //        lScrewdata.Text = (Convert.ToInt16(TotalScrewCount) - (Convert.ToInt16(Count))).ToString() + @" / " + Convert.ToInt16(TotalScrewCount).ToString();
                    //        lTorquedata.Text = Convert.ToDouble(ScrewTorque).ToString();
                    //        lThreaddata.Text = Convert.ToDouble(ScrewThread).ToString();
                    //        lTimedata.Text = Convert.ToDouble(ScrewTime).ToString();
                    //        lTorqueUnit.Text = BasicLib.Instance.TorqueUnit;
                    //        if (Count == TotalScrewCount)
                    //        {
                    //            firstScrew = "0";
                    //        }
                    //        else
                    //        {
                    //            firstScrew = (Convert.ToInt16(Count) - 1).ToString();
                    //        }
                    //        if (MESdata.Trim() != "")
                    //        {
                    //            int start = MESdata.LastIndexOf(@"<WIPTDItem>");
                    //            MESdata = MESdata.Substring(0, start);
                    //        }
                    //        flag_REV2 = flag_REV1;
                    //        continue;
                    //    }
                    //}
                    //if (Convert.ToDouble(ScrewTorque) != 0)
                    if (Convert.ToDouble(ScrewTorque) == 0 && ScrewStatus != "REV__")
                    {
                        return;
                    }
                    //totalcount = Convert.ToInt16(TotalScrewCount);

                    if (Count == TotalScrewCount)
                    {
                        //lScrewdata.Text = @"0 / 0";
                        //lScrewdata.Text = (Convert.ToInt16(TotalScrewCount) - (Convert.ToInt16(Count))).ToString() + @" / " + Convert.ToInt16(TotalScrewCount).ToString();
                        //lTorquedata.Text = "0";
                        //lThreaddata.Text = "0";
                        //lTimedata.Text = "0";
                        //lTorqueUnit.Text = BasicLib.Instance.TorqueUnit;
                        firstScrew = "0";
                        Number = 0;
                        //MESdata = "";
                        MESdataHeader = "";
                    }
                    //else
                    //{
                    //    lScrewdata.Text = (Convert.ToInt16(TotalScrewCount) - (Convert.ToInt16(Count))).ToString() + @" / " + Convert.ToInt16(TotalScrewCount).ToString();
                    //    lTorquedata.Text = Convert.ToDouble(ScrewTorque).ToString();
                    //    lThreaddata.Text = Convert.ToDouble(ScrewThread).ToString();
                    //    lTimedata.Text = Convert.ToDouble(ScrewTime).ToString();
                    //    lTorqueUnit.Text = BasicLib.Instance.TorqueUnit;
                    //}
                    Number = Convert.ToInt16(TotalScrewCount) - Convert.ToInt16(Count);

                    //if (item > 0)
                    //if (Number <= 0 && LastScrewStatus == true && ScrewStatus!= @"NGT__" && ScrewStatus != @"NGQ__" && ScrewStatus != @"NGC__" && NGtimes!=0)
                    //{
                    //    return;
                    //}
                    if (Number <= 0 && LastScrewStatus == true)
                    {
                        return;
                    }
                    isScrew = true;
                    switch (ScrewStatus)
                    {
                        case @"REV__":
                            OKALLconfirm = true;  //翻转后激活OKALL，防止最后一颗NG螺丝重复发送OKALL，增加NGtimes
                            pictureBox1.BackgroundImage = Properties.Resources.grey_ball;
                            flag_REV1 = rawdata.Substring(63, 2);
                            if (flag_REV1 != flag_REV2)
                            {

                                if (Count == TotalScrewCount)
                                {
                                    firstScrew = "0";
                                }
                                //else if (Convert.ToInt16(Count)==2)
                                //{
                                //    firstScrew = "0";
                                //}
                                else
                                {
                                    //firstScrew = (Convert.ToInt16(Count) + 1).ToString();
                                    firstScrew = Convert.ToInt16(Count).ToString();
                                }
                                if (MESdata.Trim() != "")
                                {
                                    int start = MESdata.LastIndexOf(@"<WIPTDItem>");
                                    MESdata = MESdata.Substring(0, start);
                                }
                                flag_REV2 = flag_REV1;
                                continue;
                            }
                            break;
                        case @"OK___":

                            if (Count != firstScrew && LastScrewStatus == true)
                            {
                                MESdata += @"    <WIPTDItem>";
                                MESdata += @"        <Item>" + Number + "</Item>";
                                MESdata += @"        <TestStep>" + Number + ".1" + "</TestStep>";
                                MESdata += @"        <TestName>" + "扭矩" + "</TestName>";
                                MESdata += @"        <OutputName></OutputName>";
                                MESdata += @"        <InputCondition></InputCondition>";
                                MESdata += @"        <OutputLoading></OutputLoading>";
                                MESdata += @"        <LowerLimit>" + BasicLib.Instance.TorqueLowerlimit.ToString() + @"</LowerLimit>";
                                MESdata += @"        <Result>" + lTorquedata.Text + @"</Result>";
                                MESdata += @"        <UpperLimit>" + BasicLib.Instance.TorqueUpperlimit.ToString() + @"</UpperLimit>";
                                MESdata += @"        <Unit>" + lTorqueUnit.Text + "</Unit>";
                                if (Convert.ToDouble(Convert.ToDouble(ScrewTorque).ToString()) > BasicLib.Instance.TorqueUpperlimit ||
                                Convert.ToDouble(Convert.ToDouble(ScrewTorque).ToString()) < BasicLib.Instance.TorqueLowerlimit)
                                {
                                    pictureBox1.BackgroundImage = Properties.Resources.redlight;
                                    //Thread.Sleep(800);
                                    MESdata += @"        <Status>" + "FAIL" + "</Status>";
                                    MESdata += @"        <IPSReference></IPSReference>";
                                    MESdata += @"        <TestID>" + BasicLib.Instance.TestID + @"</TestID>";
                                    MESdata += @"        <StartTime></StartTime>";
                                    MESdata += @"        <EndTime></EndTime>";
                                    MESdata += @"    </WIPTDItem>";
                                    Sound_Fail();
                                    NGtimes += 1;
                                    if (NGtimes == 5)
                                    {
                                        pictureBox2.BackgroundImage = Properties.Resources.redlight;
                                        lNGtype.Text = "螺丝NG次数超限";
                                        Status_Product = "FAIL";
                                        isEnd = true;
                                        break;
                                    }
                                }
                                else
                                {
                                    MESdata += @"        <Status>" + "PASS" + "</Status>";
                                    MESdata += @"        <IPSReference></IPSReference>";
                                    MESdata += @"        <TestID>" + BasicLib.Instance.TestID + @"</TestID>";
                                    MESdata += @"        <StartTime></StartTime>";
                                    MESdata += @"        <EndTime></EndTime>";
                                    MESdata += @"    </WIPTDItem>";
                                    pictureBox1.BackgroundImage = Properties.Resources.greenlight;
                                    Thread.Sleep(800);
                                }

                                firstScrew = Count;
                                if ((Number != Convert.ToInt16(TotalScrewCount)) && Label.Count == 2 && Number != 0)
                                {
                                    pictureBox2.BackgroundImage = Properties.Resources.redlight;
                                    lNGtype.Text = "产品螺丝数量未打完";
                                    Status_Product = "FAIL";
                                    isEnd = true;
                                    break;
                                }
                            }
                            break;
                        case @"NGT__":
                            if (Count != firstScrew)
                            {
                                MESdata += @"    <WIPTDItem>";
                                MESdata += @"        <Item>" + Number + "</Item>";
                                MESdata += @"        <TestStep>" + Number + ".1" + "</TestStep>";
                                MESdata += @"        <TestName>" + "扭矩" + "</TestName>";
                                MESdata += @"        <OutputName></OutputName>";
                                MESdata += @"        <InputCondition></InputCondition>";
                                MESdata += @"        <OutputLoading></OutputLoading>";
                                MESdata += @"        <LowerLimit>" + BasicLib.Instance.TorqueLowerlimit.ToString() + @"</LowerLimit>";
                                MESdata += @"        <Result>" + lTorquedata.Text + @"</Result>";
                                MESdata += @"        <UpperLimit>" + BasicLib.Instance.TorqueUpperlimit.ToString() + @"</UpperLimit>";
                                MESdata += @"        <Unit>" + lTorqueUnit.Text + "</Unit>";
                                MESdata += @"        <Status>" + "FAIL" + "</Status>";
                                MESdata += @"        <IPSReference></IPSReference>";
                                MESdata += @"        <TestID>" + BasicLib.Instance.TestID + @"</TestID>";
                                MESdata += @"        <StartTime></StartTime>";
                                MESdata += @"        <EndTime></EndTime>";
                                MESdata += @"    </WIPTDItem>";
                                firstScrew = Count;
                            }
                            Sound_Fail();
                            //Status_Product = "FAIL";
                            pictureBox1.BackgroundImage = Properties.Resources.redlight;
                            lNGtype.Text = "螺丝锁付时间不在要求范围内";
                            //Thread.Sleep(500);
                            NGtimes += 1;
                            if (NGtimes == 5)
                            {
                                pictureBox2.BackgroundImage = Properties.Resources.redlight;
                                lNGtype.Text = "螺丝NG次数超限";
                                Status_Product = "FAIL";
                                isEnd = true;
                                break;
                            }
                            if ((Number != Convert.ToInt16(TotalScrewCount)) && Label.Count == 2 && Number != 0)
                            {
                                pictureBox2.BackgroundImage = Properties.Resources.redlight;
                                lNGtype.Text = "产品螺丝数量未打完";
                                Status_Product = "FAIL";
                                isEnd = true;
                                break;
                            }
                            break;
                        case @"NGQ__":
                            if (Count != firstScrew)
                            {
                                MESdata += @"    <WIPTDItem>";
                                MESdata += @"        <Item>" + Number + "</Item>";
                                MESdata += @"        <TestStep>" + Number + ".1" + "</TestStep>";
                                MESdata += @"        <TestName>" + "扭矩" + "</TestName>";
                                MESdata += @"        <OutputName></OutputName>";
                                MESdata += @"        <InputCondition></InputCondition>";
                                MESdata += @"        <OutputLoading></OutputLoading>";
                                MESdata += @"        <LowerLimit>" + BasicLib.Instance.TorqueLowerlimit.ToString() + @"</LowerLimit>";
                                MESdata += @"        <Result>" + lTorquedata.Text + @"</Result>";
                                MESdata += @"        <UpperLimit>" + BasicLib.Instance.TorqueUpperlimit.ToString() + @"</UpperLimit>";
                                MESdata += @"        <Unit>" + lTorqueUnit.Text + "</Unit>";
                                MESdata += @"        <Status>" + "FAIL" + "</Status>";
                                MESdata += @"        <IPSReference></IPSReference>";
                                MESdata += @"        <TestID>" + BasicLib.Instance.TestID + @"</TestID>";
                                MESdata += @"        <StartTime></StartTime>";
                                MESdata += @"        <EndTime></EndTime>";
                                MESdata += @"    </WIPTDItem>";
                                firstScrew = Count;
                            }
                            Sound_Fail();
                            pictureBox1.BackgroundImage = Properties.Resources.redlight;
                            lNGtype.Text = "螺丝扭力不在要求范围内";
                            //Thread.Sleep(800);
                            NGtimes += 1;
                            if (NGtimes == 5)
                            {
                                pictureBox2.BackgroundImage = Properties.Resources.redlight;
                                lNGtype.Text = "螺丝NG次数超限";
                                Status_Product = "FAIL";
                                isEnd = true;
                                break;
                            }
                            if ((Number != Convert.ToInt16(TotalScrewCount)) && Label.Count == 2 && Number != 0)
                            {
                                pictureBox2.BackgroundImage = Properties.Resources.redlight;
                                lNGtype.Text = "产品螺丝数量未打完";
                                Status_Product = "FAIL";
                                isEnd = true;
                                break;
                            }
                            break;
                        case @"NGC__":
                            if (Count != firstScrew)
                            {
                                MESdata += @"    <WIPTDItem>";
                                MESdata += @"        <Item>" + Number + "</Item>";
                                MESdata += @"        <TestStep>" + Number + ".1" + "</TestStep>";
                                MESdata += @"        <TestName>" + "扭矩" + "</TestName>";
                                MESdata += @"        <OutputName></OutputName>";
                                MESdata += @"        <InputCondition></InputCondition>";
                                MESdata += @"        <OutputLoading></OutputLoading>";
                                MESdata += @"        <LowerLimit>" + BasicLib.Instance.TorqueLowerlimit.ToString() + @"</LowerLimit>";
                                MESdata += @"        <Result>" + lTorquedata.Text + @"</Result>";
                                MESdata += @"        <UpperLimit>" + BasicLib.Instance.TorqueUpperlimit.ToString() + @"</UpperLimit>";
                                MESdata += @"        <Unit>" + lTorqueUnit.Text + "</Unit>";
                                MESdata += @"        <Status>" + "FAIL" + "</Status>";
                                MESdata += @"        <IPSReference></IPSReference>";
                                MESdata += @"        <TestID>" + BasicLib.Instance.TestID + @"</TestID>";
                                MESdata += @"        <StartTime></StartTime>";
                                MESdata += @"        <EndTime></EndTime>";
                                MESdata += @"    </WIPTDItem>";
                                firstScrew = Count;
                            }
                            Sound_Fail();
                            //Status_Product = "FAIL";
                            pictureBox1.BackgroundImage = Properties.Resources.redlight;
                            lNGtype.Text = "螺丝锁付圈数不在要求范围内";
                            //Thread.Sleep(500);
                            NGtimes += 1;
                            if (NGtimes == 5)
                            {
                                pictureBox2.BackgroundImage = Properties.Resources.redlight;
                                lNGtype.Text = "螺丝NG次数超限";
                                Status_Product = "FAIL";
                                isEnd = true;
                                break;
                            }
                            if ((Number != Convert.ToInt16(TotalScrewCount)) && Label.Count == 2 && Number != 0)
                            {
                                pictureBox2.BackgroundImage = Properties.Resources.redlight;
                                lNGtype.Text = "产品螺丝数量未打完";
                                Status_Product = "FAIL";
                                isEnd = true;
                                break;
                            }
                            break;
                        case @"OKALL":
                            
                            if (OKALLconfirm==true)
                            {
                                OKALLconfirm = false;
                                MESdata += @"    <WIPTDItem>";
                                MESdata += @"        <Item>" + Number + "</Item>";
                                MESdata += @"        <TestStep>" + Number + ".1" + "</TestStep>";
                                MESdata += @"        <TestName>" + "扭矩" + "</TestName>";
                                MESdata += @"        <OutputName></OutputName>";
                                MESdata += @"        <InputCondition></InputCondition>";
                                MESdata += @"        <OutputLoading></OutputLoading>";
                                MESdata += @"        <LowerLimit>" + BasicLib.Instance.TorqueLowerlimit.ToString() + @"</LowerLimit>";
                                MESdata += @"        <Result>" + lTorquedata.Text + @"</Result>";
                                MESdata += @"        <UpperLimit>" + BasicLib.Instance.TorqueUpperlimit.ToString() + @"</UpperLimit>";
                                MESdata += @"        <Unit>" + lTorqueUnit.Text + "</Unit>";
                                if (Convert.ToDouble(Convert.ToDouble(ScrewTorque).ToString()) > BasicLib.Instance.TorqueUpperlimit ||
                                Convert.ToDouble(Convert.ToDouble(ScrewTorque).ToString()) < BasicLib.Instance.TorqueLowerlimit)
                                {
                                    pictureBox1.BackgroundImage = Properties.Resources.redlight;
                                    MESdata += @"        <Status>" + "FAIL" + "</Status>";

                                    MESdata += @"        <IPSReference></IPSReference>";
                                    MESdata += @"        <TestID>" + BasicLib.Instance.TestID + @"</TestID>";
                                    MESdata += @"        <StartTime></StartTime>";
                                    MESdata += @"        <EndTime></EndTime>";
                                    MESdata += @"    </WIPTDItem>";
                                    Sound_Fail();
                                    NGtimes += 1;
                                    if (NGtimes == 5)
                                    {
                                        pictureBox2.BackgroundImage = Properties.Resources.redlight;
                                        lNGtype.Text = "螺丝NG次数超限";
                                        Status_Product = "FAIL";
                                        isEnd = true;
                                        break;
                                    }
                                    isEnd = false;
                                    LastScrewStatus = false;
                                    break;
                                }
                                else
                                {
                                    MESdata += @"        <Status>" + "PASS" + "</Status>";

                                    MESdata += @"        <IPSReference></IPSReference>";
                                    MESdata += @"        <TestID>" + BasicLib.Instance.TestID + @"</TestID>";
                                    MESdata += @"        <StartTime></StartTime>";
                                    MESdata += @"        <EndTime></EndTime>";
                                    MESdata += @"    </WIPTDItem>";
                                    pictureBox1.BackgroundImage = Properties.Resources.greenlight;
                                    isEnd = true;
                                }

                                firstScrew = Count;
                                break;
                            }
                            else
                            {
                                break;
                            }

                        default:
                            break;
                    }
                }
                else
                {
                    Thread.Sleep(100);
                    continue;
                }
            }

            if (Label.Count > 0)
            {
                Label.Remove(Label[0]);
            }

            StringExistTimes(MESdata, @"<Item>", out int Itemexisttimes);
            StringExistTimes(MESdata, @"PASS", out int passtimes);
            if (Itemexisttimes < Convert.ToInt16(TotalScrewCount) || passtimes != Itemexisttimes)
            {
                Status_Product = "FAIL";
            }
            if (Itemexisttimes >= Convert.ToInt16(TotalScrewCount) && passtimes == Itemexisttimes)
            {
                Status_Product = "PASS";
            }

            MESdataHeader = @"<dsWIPTD>";
            MESdataHeader += @"    <WIPTDHeader>";
            MESdataHeader += @"        <IntSerialNo>" + mLab + "</IntSerialNo>";
            MESdataHeader += @"        <CommSN></CommSN>";
            MESdataHeader += @"        <Process>" + BasicLib.Instance.ThisProcess + @"</Process>";
            MESdataHeader += @"        <Result>" + Status_Product + @"</Result>";
            MESdataHeader += @"        <TesterNo>" + BasicLib.Instance.TestID + @"</TesterNo>";
            MESdataHeader += @"        <ProdLine>" + BasicLib.Instance.ThisProductionline + @"</ProdLine>";
            MESdataHeader += @"        <ProgramName></ProgramName>";
            MESdataHeader += @"        <ProgramVersion></ProgramVersion>";
            MESdataHeader += @"        <IPSNo></IPSNo>";
            MESdataHeader += @"        <IPSVersion></IPSVersion>";
            MESdataHeader += @"        <OperatorName></OperatorName>";
            MESdataHeader += @"        <Remark></Remark>";
            MESdataHeader += @"        <StartTime></StartTime>";
            MESdataHeader += @"        <EndTime></EndTime>";
            MESdataHeader += @"    </WIPTDHeader>";

            MESdata = MESdata.Insert(0, MESdataHeader);
            MESdata += @"</dsWIPTD>";

            string TxtPath = Application.StartupPath + @"\测试记录" + @"\" + mLab + @".txt";
            using (StreamWriter sw = File.AppendText(TxtPath))
            {
                DateTime dt = DateTime.Now;

                //sw.WriteLine(dt.ToLongDateString() + "\r\n" + MESdata + "\r\n" + "\r\n");
                sw.WriteLine(dt.ToLongDateString() + "  " + dt.ToLongTimeString() + "\r\n" + MESdata + "\r\n" + "\r\n");
            }
            int pp = Label.Count;
            if (Status_Product == "PASS")
            {
                pictureBox2.BackgroundImage = Properties.Resources.greenlight;
            }
            //if (Status_Product == "FAIL")
            else
            {
                pictureBox2.BackgroundImage = Properties.Resources.redlight;
            }
            int aa = 1;
            int bb = 2;
            int cc = 3;
            int dd = 4;
            int ee = 5;
            try
            {
                return_MES = Trace.SFC_WIPATEOutput(MESdata);
                int uploadtimes=0;
                while (return_MES == "FAIL" && uploadtimes<5)
                {
                    Thread.Sleep(300);
                    return_MES = Trace.SFC_WIPATEOutput(MESdata);
                    uploadtimes++;
                }
                if (return_MES == "PASS")
                {
                    lMES.ForeColor = Color.FromArgb(73, 155, 62);
                    lMES.Text = "上传MES成功";
                    Thread.Sleep(800);
                }
                else
                {
                    lMES.Text = "上传MES失败";
                    lMES.ForeColor = Color.Red;
                    Thread.Sleep(800);
                }
                UploadToMES = true;
            }
            catch (Exception)
            {

            }
            //}
            //isProcessEnd = true;
        }
                public void Return_Status(string MESstring, out string process, out string status)
        {
            int ProcessStart = MESstring.IndexOf(@"<Process>");
            int ProcessEnd = MESstring.IndexOf(@"</Process>");
            int StatusStart = MESstring.IndexOf(@"<Status>");
            int StatusEnd = MESstring.IndexOf(@"</Status>");
            process = MESstring.Substring(ProcessStart + 9, ProcessEnd - ProcessStart - 9);
            if (StatusStart == -1 && StatusEnd == -1)
            {
                status = "PASS";
            }
            else
            {
                status = MESstring.Substring(StatusStart + 8, StatusEnd - StatusStart - 8);
            }
        }
        private void BStart_Click(object sender, EventArgs e)
        {
            if (isRun == true || isBarcodeRun == true)
            {
                return;
            }
            label6.Text = "";
            txtBarcode.Text = "";
            lNGtype.Text = "";
            lMES.Text = "";
            isRun = true;
            bStart.Enabled = false;
            bStart.BackColor = Color.Gray;
            bStop.Enabled = true;
            bStop.BackColor = Color.FromArgb(192, 255, 192);
            bClose.Enabled = false;
            bClose.BackColor = Color.Gray;
            bSetting.Enabled = true;
            bSetting.BackColor = Color.FromArgb(192, 255, 192);
            if (isBarcodeRun == false)
            {
                isBarcodeRun = true;
            }

            thread1 = new Thread(new ThreadStart(ProcessMain));
            thread1.Start();
            thread2 = new Thread(new ThreadStart(Process_Barcode));
            thread2.Start();
            //isBarcodeRun = true;
            pictureBox1.BackgroundImage = Properties.Resources.grey_ball;
            pictureBox2.BackgroundImage = Properties.Resources.grey_ball;
        }

        private void BClose_Click(object sender, EventArgs e)
        {
            isRun = false;
            isBarcodeRun = false;
            System.Environment.Exit(0);
        }

        private void BStop_Click(object sender, EventArgs e)
        {
            isProcessEnd = true;
            if (isRun == true)
            {
                isProcessEnd = false;
            }

            isRun = false;
            isBarcodeRun = false;
            lScrewdata.Text = @"0 / 0";
            lTorquedata.Text = "0";
            lTimedata.Text = "0";
            lThreaddata.Text = "0";
            lMES.Text = "";
            bStart.Enabled = true;
            bStart.BackColor = Color.FromArgb(192, 255, 192);
            bStop.Enabled = false;
            bStop.BackColor = Color.Gray;
            bClose.Enabled = true;
            bClose.BackColor = Color.FromArgb(192, 255, 192);
            Thread.Sleep(100);
            thread1.Abort();
            thread2.Abort();
            if (Label.Count == 1)
            {
                Label.Remove(Label[0]);
            }
        }

        //private void timer1_Tick(object sender, EventArgs e)
        //{
        //Thread thread2 = new Thread(new ThreadStart(Process_Barcode));
        //thread2.Start();
        //Q: 用timer调用扫描枪经常扫码不全
        //}

        private void BSetting_Click(object sender, EventArgs e)
        {
            Setting setting = new Setting(Showtext);
            setting.Show();
            //Access access = new Access();
            //access.ShowDialog();
        }

        public void Showtext(string str)
        {
            txtProcess.Text = str;
        }
        private void MainPage_Shown(object sender, EventArgs e)
        {
            Hasher HDid = new Hasher();
            string sn = HDid.Sern();
            if (BasicLib.Instance.SN != sn)
            {
                //SN sn1 = new SN();
                //sn1.ShowDialog();
                //if (sn1.isOK == false)
                //{
                //    this.Close();
                //    return;
                //}
            }
        }
        public void Sound_Fail()
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(Properties.Resources.Windows_Hardware_Fail);
            player.Play();
        }
        public void StringExistTimes(string str1, string str2, out int times)
        {
            times = 0;
            string r = str1.Replace(str2, "");
            times = (str1.Length - r.Length) / str2.Length;
        }
        public void DataProcess(string inputString, out string screwtorque, out string screwtime, out string screwthread,
    out string currentcount, out string totalcount, out string screwstatus)
        {
            screwtorque = "";
            screwtime = "";
            screwthread = "";
            currentcount = "";
            totalcount = "";
            screwstatus = "";
            try
            {
                int index = inputString.IndexOf(BasicLib.Instance.TorqueUnit);
                string tor = inputString.Substring(index - 10, 9);
                screwtorque = Convert.ToDouble(tor).ToString();
                string st = inputString.Substring(index + 7, 9);
                screwtime = Convert.ToDouble(st).ToString();
                string sthread = inputString.Substring(index + 17, 9);
                screwthread = Convert.ToDouble(sthread).ToString();
                string cc = inputString.Substring(index + 27, 3);
                currentcount = Convert.ToDouble(cc).ToString();
                string tc = inputString.Substring(index + 31, 3);
                totalcount = Convert.ToDouble(tc).ToString();
                screwstatus = inputString.Substring(index + 39, 5);
            }
            catch (Exception e)
            {
                //lNGtype.Text = "控制器收到的字符串不完整，请重新开始";
                //Thread.Sleep(2500);
                //lNGtype.Text = "";
            }
        }
   }
}