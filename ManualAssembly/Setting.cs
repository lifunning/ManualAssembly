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
using System.Xml.Serialization;

namespace ManualAssembly
{
    public delegate void TP(string str);
    public partial class Setting : Form
    {
        private TP _tp;
        string data_input = "";
        string data_output = "";
        int[] Input = new int[4];
        int[] Output = new int[4];

        public Setting(TP tp)
        {
            this._tp = tp;
            InitializeComponent();
        }

        private void BOK_Click(object sender, EventArgs e)
        {
            //螺丝批
            this._tp(txtProcess.Text.Trim());
            //BasicLib.Instance.ScrewPort = cboScrewPort.Text;
            //BasicLib.Instance.ScrewBautrate = Convert.ToInt32(cboScrewBaut.Text);
            //BasicLib.Instance.ScrewParity = Convert.ToInt16(cboScrewParity.SelectedIndex);
            //BasicLib.Instance.ScrewStopBit = Convert.ToInt16(cboScrewStop.Text);
            //BasicLib.Instance.ScrewDataBit = Convert.ToInt16(cboScrewData.Text);
            BasicLib.Instance.TorqueUnit = cboTorqueUnit.Text;

            //条码枪
            BasicLib.Instance.BarcodePort = cboBarcodePort.Text;
            BasicLib.Instance.BarcodeBautrate = Convert.ToInt32(cboBarcodeBaut.Text);
            BasicLib.Instance.BarcodeParity = Convert.ToInt16(cboBarcodeParity.SelectedIndex);
            BasicLib.Instance.BarcodeStopBit = Convert.ToInt16(cboBarcodeStop.Text);
            BasicLib.Instance.BarcodeDataBit = Convert.ToInt16(cboBarcodeData.Text);

            //阻挡气缸
            BasicLib.Instance.CylPort = cboIOPort.Text;
            BasicLib.Instance.CylBautrate = Convert.ToInt32(cboIOBaut.Text);
            BasicLib.Instance.CylParity = Convert.ToInt16(cboIOParity.SelectedIndex);
            BasicLib.Instance.CylStopBit = Convert.ToInt16(cboIOStop.Text);
            BasicLib.Instance.CylDataBit = Convert.ToInt16(cboIOData.Text);

            //生产数据
            BasicLib.Instance.ThisProcess = txtProcess.Text.ToLower();
            BasicLib.Instance.TestID = txttestID.Text.ToLower();
            BasicLib.Instance.ThisProductionline = cboProductionline.Text.ToLower();
            BasicLib.Instance.TorqueUpperlimit = Convert.ToDouble(txtTorqueUpperlimit.Text);
            BasicLib.Instance.TorqueLowerlimit = Convert.ToDouble(txtTorqueLowerlimit.Text);
            if (cboScrewDriverType.Text == "奇力速")
            {
                BasicLib.Instance.ScrewDriverType = 0;
            }
            if (cboScrewDriverType.Text == "马头")
            {
                BasicLib.Instance.ScrewDriverType = 1;
            }
            if (cboTime.Checked == true)
            {
                BasicLib.Instance.ScrewTimeDisplay = true;
            }
            else
            {
                BasicLib.Instance.ScrewTimeDisplay = false;
            }
            if (cboThread.Checked == true)
            {
                BasicLib.Instance.ScrewThreadDisplay = true;
            }
            else
            {
                BasicLib.Instance.ScrewThreadDisplay = false;
            }
            BasicLib.Instance.Write();
            this.Close();
        }

        private void BCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Setting_Load(object sender, EventArgs e)
        {
            //条码枪
            cboBarcodePort.Text = BasicLib.Instance.BarcodePort;
            cboBarcodeBaut.Text = BasicLib.Instance.BarcodeBautrate.ToString();
            cboBarcodeParity.SelectedIndex = BasicLib.Instance.BarcodeParity;
            cboBarcodeStop.Text = BasicLib.Instance.BarcodeStopBit.ToString();
            cboBarcodeData.Text = BasicLib.Instance.BarcodeDataBit.ToString();
            //螺丝批
            //cboScrewPort.Text = BasicLib.Instance.ScrewPort;
            //cboScrewBaut.Text = BasicLib.Instance.ScrewBautrate.ToString();
            //cboScrewParity.SelectedIndex = BasicLib.Instance.ScrewParity;
            //cboScrewStop.Text = BasicLib.Instance.ScrewStopBit.ToString();
            //cboScrewData.Text = BasicLib.Instance.ScrewDataBit.ToString();
            cboTorqueUnit.Text = BasicLib.Instance.TorqueUnit;
            //生产设置
            txtProcess.Text = BasicLib.Instance.ThisProcess;
            txttestID.Text = BasicLib.Instance.TestID;
            cboProductionline.Text = BasicLib.Instance.ThisProductionline;
            txtTorqueUpperlimit.Text = BasicLib.Instance.TorqueUpperlimit.ToString();
            txtTorqueLowerlimit.Text = BasicLib.Instance.TorqueLowerlimit.ToString();
            if (BasicLib.Instance.ScrewThreadDisplay == true)
            {
                cboThread.Checked = true;
            }
            else
            {
                cboThread.Checked = false;
            }
            if (BasicLib.Instance.ScrewTimeDisplay == true)
            {
                cboTime.Checked = true;
            }
            else
            {
                cboTime.Checked = false;
            }

            //输入输出模块
            cboIOPort.Text = BasicLib.Instance.CylPort;
            cboIOBaut.Text = BasicLib.Instance.CylBautrate.ToString();
            cboIOParity.SelectedIndex = BasicLib.Instance.CylParity;
            cboIOStop.Text = BasicLib.Instance.CylStopBit.ToString();
            cboIOData.Text = BasicLib.Instance.CylDataBit.ToString();
        }
        
        private void Setting_Shown(object sender, EventArgs e)
        {
            Access access = new Access();
            access.ShowDialog();
            if (access.isOK == false)
            {
                this.Close();
                return;
            }
        }

        private void SendtoSP(string sendStr)
        {
            if (!BasicLib.Instance.IOModulesp.IsOpen)
            {
                return;
            }

            if (sendStr.Equals("")) return;
            byte[] reb = null;

            reb = HexStringBcd(sendStr);
            BasicLib.Instance.IOModulesp.Write(reb, 0, reb.Length);
        }

        private byte[] HexStringBcd(string hexStr)
        {
            //string res = "";
            byte[] brr = null;

            bool reb = System.Text.RegularExpressions.Regex.IsMatch(hexStr, "^[0-9A-F]{2,}$");
            if (reb && hexStr.Length % 2 == 0)
            {
                brr = new byte[hexStr.Length / 2];

                for (int i = 0; i < hexStr.Length; i += 2)
                {
                    brr[i / 2] = (Convert.ToByte(hexStr.Substring(i, 2), 16));//"33">0x33                    
                }
                //res = Encoding.UTF8.GetString(brr);
            }
            // return res;
            return brr;
        }


        public string GetFinalCmd(string data)
        {
            //字符串压缩("01" >> 0x01)
            byte[] dataBytesTemp = HexStringBcd(data);
            byte[] dataBytes = new byte[dataBytesTemp.Length + 2];
            dataBytesTemp.CopyTo(dataBytes, 0);
            byte[] crcBytes = new byte[2];
            //获得CRC16校验值
            GetCRC(dataBytes, ref crcBytes);
            //将CRC16校验值放在帧最后
            crcBytes.CopyTo(dataBytes, dataBytesTemp.Length);

            string showStr = string.Empty;
            string tempStr = string.Empty;
            for (int i = 0; i < dataBytes.Length; i++)
            {
                showStr += dataBytes[i].ToString("X2");
            }
            return showStr;
        }

        public void GetCRC(byte[] message, ref byte[] CRC)
        {
            ushort CRCFull = 0xFFFF;
            byte CRCHigh = 0xFF, CRCLow = 0xFF;
            char CRCLSB;
            for (int i = 0; i < message.Length - 2; i++)
            {
                CRCFull = (ushort)(CRCFull ^ message[i]);
                for (int j = 0; j < 8; j++)
                {
                    CRCLSB = (char)(CRCFull & 0x0001);
                    CRCFull = (ushort)((CRCFull >> 1) & 0x7FFF);
                    if (CRCLSB == 1)
                        CRCFull = (ushort)(CRCFull ^ 0xA001);
                }
            }
            CRC[1] = CRCHigh = (byte)((CRCFull >> 8) & 0xFF);
            CRC[0] = CRCLow = (byte)(CRCFull & 0xFF);
        }

        private void btnLight_Yellow_Click(object sender, EventArgs e)
        {
            string data;
            if (btnLight_Yellow.BackColor == Color.LightGreen)
            {
                data = "01" + "05" + "00" + "03" + "00" + "00";
                SendtoSP(GetFinalCmd(data));
                btnLight_Yellow.BackColor = Color.IndianRed;
            }
            else
            {
                data = "01" + "05" + "00" + "03" + "FF" + "00";
                SendtoSP(GetFinalCmd(data));
                btnLight_Yellow.BackColor = Color.LightGreen;
            }
        }

        private void btnLight_Green_Click(object sender, EventArgs e)
        {
            string data;
            if (btnLight_Green.BackColor == Color.LightGreen)
            {
                data = "01" + "05" + "00" + "03" + "00" + "00";
                SendtoSP(GetFinalCmd(data));
                btnLight_Green.BackColor = Color.IndianRed;
            }
            else
            {
                data = "01" + "05" + "00" + "03" + "FF" + "00";
                SendtoSP(GetFinalCmd(data));
                btnLight_Green.BackColor = Color.LightGreen;
            }
        }

        private void btnLight_Red_Click(object sender, EventArgs e)
        {
            string data;
            if (btnLight_Green.BackColor == Color.LightGreen)
            {
                data = "01" + "05" + "00" + "03" + "00" + "00";
                SendtoSP(GetFinalCmd(data));
                btnLight_Green.BackColor = Color.IndianRed;
            }
            else
            {
                data = "01" + "05" + "00" + "03" + "FF" + "00";
                SendtoSP(GetFinalCmd(data));
                btnLight_Green.BackColor = Color.LightGreen;
            }
        }

        private void btnBlockCylinder_Click(object sender, EventArgs e)
        {
            string data;
            if (btnLight_Green.BackColor == Color.LightGreen)
            {
                data = "01" + "05" + "00" + "03" + "00" + "00";
                SendtoSP(GetFinalCmd(data));
                btnLight_Green.BackColor = Color.IndianRed;
            }
            else
            {
                data = "01" + "05" + "00" + "03" + "FF" + "00";
                SendtoSP(GetFinalCmd(data));
                btnLight_Green.BackColor = Color.LightGreen;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string data_Input = "01" + "01" + "00" + "00" + "00" + "04";
            SendtoSP(GetFinalCmd(data_Input));
            Thread.Sleep(100);
            BasicLib.Instance.GetIOModuleData(out data_input);
            int Bitdata_Input = Convert.ToInt16(data_input.Substring(6, 2));
            BasicLib.Instance.AssignBitValue(Bitdata_Input, out Input);
            if (Input[0] == 1)
            {
                btnBlockCylinder.BackColor = Color.LightGreen;
            }
            else
            {
                btnBlockCylinder.BackColor = Color.IndianRed;
            }

            if (Input[1] == 1)
            {
                btnLight_Red.BackColor = Color.LightGreen;
            }
            else
            {
                btnLight_Red.BackColor = Color.IndianRed;
            }

            if (Input[2] == 1)
            {
                btnLight_Yellow.BackColor = Color.LightGreen;
            }
            else
            {
                btnLight_Yellow.BackColor = Color.IndianRed;
            }

            if (Input[3] == 1)
            {
                btnLight_Green.BackColor = Color.LightGreen;
            }
            else
            {
                btnLight_Green.BackColor = Color.IndianRed;
            }
            string data_Output = "01" + "02" + "00" + "00" + "00" + "04";
            SendtoSP(GetFinalCmd(data_Output));
            BasicLib.Instance.GetIOModuleData(out data_output);
            int Bitdata_Output = Convert.ToInt16(data_output.Substring(6, 2));
            BasicLib.Instance.AssignBitValue(Bitdata_Output, out Output);

            if (Output[0] == 1)
            {
                btnGreen.BackColor = Color.LightGreen;
            }
            else
            {
                btnGreen.BackColor = Color.IndianRed;
            }
            if (Output[1] == 1)
            {
                btnRed.BackColor = Color.LightGreen;
            }
            else
            {
                btnRed.BackColor = Color.IndianRed;
            }
            if (Output[2] == 1)
            {
                btnYellow.BackColor = Color.LightGreen;
            }
            else
            {
                btnYellow.BackColor = Color.IndianRed;
            }
            if (Output[3] == 1)
            {
                btnPedal.BackColor = Color.LightGreen;
            }
            else
            {
                btnPedal.BackColor = Color.IndianRed;
            }
        }

        private void Screwdriver1_Click(object sender, EventArgs e)
        {
            Screwdriver1 sd1 = new Screwdriver1();
            sd1.Show();
        }

        private void Screwdriver2_Click(object sender, EventArgs e)
        {
            Screwdriver2 sd2 = new Screwdriver2();
            sd2.Show();
        }

        private void Screwdriver3_Click(object sender, EventArgs e)
        {
            Screwdriver3 sd3 = new Screwdriver3();
            sd3.Show();
        }
    }
}