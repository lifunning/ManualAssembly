using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ManualAssembly
{
    public class BasicLib
    {
        [XmlIgnoreAttribute]
        private static BasicLib instance;
        [XmlIgnoreAttribute]
        private static object threadLock = new object();
        [XmlIgnoreAttribute]
        public SerialPort Barcodesp = new SerialPort();
        public string BarcodePort { get; set; }  // 端口
        public int BarcodeBautrate { get; set; }  //波特率
        public int BarcodeParity { get; set; }  //奇偶位
        public int BarcodeStopBit { get; set; }  //停止位
        public int BarcodeDataBit { get; set; }  //数据位
        [XmlIgnoreAttribute]
        public SerialPort Cylsp = new SerialPort();
        public string CylPort { get; set; }  // 端口
        public int CylBautrate { get; set; }  //波特率
        public int CylParity { get; set; }  //奇偶位
        public int CylStopBit { get; set; }  //停止位
        public int CylDataBit { get; set; }  //数据位
        [XmlIgnoreAttribute]
        public SerialPort Screw1sp = new SerialPort();
        public string Screw1Port { get; set; }
        public int Screw1Bautrate { get; set; }
        public int Screw1Parity { get; set; }
        public int Screw1StopBit { get; set; }
        public int Screw1DataBit { get; set; }
        [XmlIgnoreAttribute]
        public SerialPort Screw2sp = new SerialPort();
        public string Screw2Port { get; set; }
        public int Screw2Bautrate { get; set; }
        public int Screw2Parity { get; set; }
        public int Screw2StopBit { get; set; }
        public int Screw2DataBit { get; set; }
        [XmlIgnoreAttribute]
        public SerialPort Screw3sp = new SerialPort();
        public string Screw3Port { get; set; }
        public int Screw3Bautrate { get; set; }
        public int Screw3Parity { get; set; }
        public int Screw3StopBit { get; set; }
        public int Screw3DataBit { get; set; }
        [XmlIgnoreAttribute]
        public SerialPort IOModulesp = new SerialPort();
        public string IOModulePort { get; set; }  // 端口
        public int IOModuleBautrate { get; set; }  //波特率
        public int IOModuleParity { get; set; }  //奇偶位
        public int IOModuleStopBit { get; set; }  //停止位
        public int IOModuleDataBit { get; set; }  //数据位
        public string Strbarcode { get; set; }
        public string TorqueUnit { get; set; }  //扭矩单位
        public bool ScrewThreadDisplay { get; set; } //界面锁付圈数是否显示
        public bool ScrewTimeDisplay { get; set; } //界面锁付时间是否显示
        public string ThisProcess { get; set; } //当前流程
        public string ThisProductionline { get; set; } //产线编号
        public string TestID { get; set; } //设备编号
        public double TorqueUpperlimit { get; set; } //螺丝扭矩上限
        public double TorqueLowerlimit { get; set; } //螺丝扭矩下限
                                                     ////[XmlIgnoreAttribute]
                                                     //string rerere { get; set; } //螺丝扭矩下限
        public int ScrewDriverType { get; set; } //螺丝批种类，0为奇力速，1为马头

        public string SN { get; set; }

        public BasicLib()
        {

        }
        public bool Initialize()
        {
            return true;
        }
        public static BasicLib Instance
        {
            get
            {
                lock (threadLock)
                {
                    if (instance == null)
                    {
                        instance = new BasicLib();
                        instance.Read();
                    }
                    return instance;
                }
            }
        }
        public bool Read()
        {
            string Path = Application.StartupPath;
            lock (threadLock)
            {
                try
                {
                    using (TextReader reader = new StreamReader(Path + "\\Config\\ComPara.xml"))
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(BasicLib));
                        instance = (BasicLib)serializer.Deserialize(reader);
                        reader.Close();
                    }
                }
                catch (Exception ex)
                {

                }
                return (instance != null);
            }
        }
        private static void GetXMLInformation(string xmlFilePath)
        {

        }

        public bool Write()
        {
            string Path = Application.StartupPath;
            StreamWriter writer = new StreamWriter(Path + "\\Config\\ComPara.xml");
            XmlSerializer serializer = new XmlSerializer(typeof(BasicLib));
            serializer.Serialize(writer, instance);
            writer.Close();
            return true;
        }

        public void AddItem(string xmldoc, string i)
        {
            //XmlDocument xmldoc = new XmlDocument();
            ////xmldoc = ConvertStringToXmlDocument(str);
            //xmldoc.LoadXml(str);
            XElement xe = XElement.Parse(xmldoc);
            IEnumerable<XElement> elements1 = from element in xe.Elements("WIPTDItem")//创建IEnumerable泛型接口对象
                                              select element;
            //生成新的编号
            //string str = (Convert.ToInt32(elements1.Max(element => element.Attribute("ID").Value)) + 1).ToString("000");
            XElement WIPTDItem = new XElement(//创建XML元素
                "WIPTDItem", //new XAttribute("ID", str),//为XML元素设置属性
                new XElement("Item", i),
                new XElement("TestStep", i + @".1"),
                new XElement("TestName", "222扭矩"),
                new XElement("OutputName", ""),
                new XElement("InputCondition", ""),
                new XElement("OutputLoading", ""),
                new XElement("LowerLimit", ""),
                new XElement("InputCondition", ""),
                new XElement("Result", ""),
                new XElement("UpperLimit", ""),
                new XElement("Unit", ""),
                new XElement("Status", ""),
                new XElement("IPSReference", ""),
                new XElement("TestID", ""),
                new XElement("StartTime", ""),
                new XElement("EndTime", "")
                );
            xe.Add(WIPTDItem);//添加XML元素
        }

        /********************************************************************************
        --- CRC校验
        *********************************************************************************/
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
        public byte[] HexStringBcd(string hexStr)
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

        public void GetIOModuleData(out string receiveStr)
        {
            receiveStr = null;
            System.Threading.Thread.Sleep(100);//延迟100毫秒等待数据接收到接收缓冲区

            if (IOModulesp.BytesToRead <= 0) return;//接收缓冲区无数据就退出
            byte[] receiveBytes = new byte[IOModulesp.BytesToRead];
            IOModulesp.Read(receiveBytes, 0, receiveBytes.Length);
            //重新计算CRC
            byte[] validCrc = new byte[2];
            BasicLib.Instance.GetCRC(receiveBytes, ref validCrc);
            string crcString = validCrc[0].ToString("X2") + validCrc[1].ToString("X2");

            for (int k = 0; k < receiveBytes.Length; k++)
            {
                receiveStr += (receiveBytes[k].ToString("X2"));
            }

            //比较CRC
            if (receiveStr.Substring(receiveStr.Length - 4).Equals(crcString))
            {
                receiveStr = "接收到的Hex=[" + receiveStr + "],CRC校验已通过";
            }
            else
            {
                receiveStr = "接收到的Hex=[" + receiveStr + "],CRC校验未通过[重新计算的CRC=" + crcString + "]";
            }
            receiveStr += receiveStr + "\r\n";
            IOModulesp.DiscardInBuffer();//丢弃接受缓冲区数据
        }


        public void AssignBitValue(int data, out int[] bit)
        {
            bit = new int[4];

            if (((data & (1 << 0)) >= 1))
            {
                bit[0] = 1;
            }
            else
            {
                bit[0] = 0;
            }

            if (((data & (1 << 1)) >= 1))
            {
                bit[1] = 1;
            }
            else
            {
                bit[1] = 0;
            }

            if (((data & (1 << 2)) >= 1))
            {
                bit[2] = 1;
            }
            else
            {
                bit[2] = 0;
            }

            if (((data & (1 << 3)) >= 1))
            {
                bit[3] = 1;
            }
            else
            {
                bit[3] = 0;
            }
        }
    }
}