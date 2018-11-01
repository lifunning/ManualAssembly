using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ManualAssembly
{
    class SPort
    {
        private static SPort instance;
        private static object threadLock = new object();

        public static SPort Instance
        {
            get
            {
                lock (threadLock)
                {
                    if (instance == null)
                    {
                        instance = new SPort();
                    }
                    return instance;
                }
            }
        }

        public int GetBarcode(out string strbarcode)
        {
            strbarcode = "";
            if (BasicLib.Instance.Barcodesp.IsOpen == false)
            {
                ConnectBarcode();
            }

            if (BasicLib.Instance.Barcodesp.IsOpen == true)
            {
                string str2 = "";
                //Thread.Sleep(100);
                ReceiveASCII2(BasicLib.Instance.Barcodesp, ref str2);
                strbarcode = str2;
                //Thread.Sleep(50);
            }
            else
            {
                return 1;
            }
            return 0;
        }
        public void ReceiveASCII2(SerialPort sp, ref string StrToRead)
        {
            try
            {
                //Stopwatch sw = new Stopwatch();
                //sw.Start();
                //sp.NewLine = "\n";
                //sp.DiscardInBuffer();
                StrToRead = sp.ReadExisting();

                //if (StrToRead != "")
                //{
                //    StrToRead += sp.ReadExisting();
                //}
                StrToRead = StrToRead.Replace("\r", "");
                ////sp.ReadTo(StrToRead);
                //sw.Stop();
                //double InkjetT1 = sw.ElapsedTicks / (double)Stopwatch.Frequency;
                //Thread.Sleep(100);
            }
            catch (Exception)
            {

            }
        }
        public void ReceiveASCII(SerialPort sp, ref string StrToRead)
        {
            try
            {
                //Stopwatch sw = new Stopwatch();
                //sw.Start();
                sp.ReadTimeout = 5000;
                //sp.NewLine = "\n";

                StrToRead = sp.ReadLine();
                StrToRead = StrToRead.Replace("\r", "");
                // sp.DiscardOutBuffer();
                ////sp.ReadTo(StrToRead);
                //sw.Stop();
                //double InkjetT1 = sw.ElapsedTicks / (double)Stopwatch.Frequency;
                //Thread.Sleep(10);
            }
            catch (Exception)
            {

            }
        }
        public void ConnectScrew()
        {
            if (!BasicLib.Instance.Screw1sp.IsOpen)
            {
                try
                {
                    BasicLib.Instance.Screw1sp.PortName = BasicLib.Instance.Screw1Port;
                    BasicLib.Instance.Screw1sp.BaudRate = BasicLib.Instance.Screw1Bautrate;
                    BasicLib.Instance.Screw1sp.Parity = (Parity)BasicLib.Instance.Screw1Parity;
                    BasicLib.Instance.Screw1sp.StopBits = (StopBits)BasicLib.Instance.Screw1StopBit;
                    BasicLib.Instance.Screw1sp.DataBits = BasicLib.Instance.Screw1DataBit;
                    BasicLib.Instance.Screw1sp.Open();
                    BasicLib.Instance.Write();
                }
                catch
                {

                }
            }
        }
        public void ConnectBarcode()
        {
            if (!BasicLib.Instance.Barcodesp.IsOpen)
            {
                try
                {
                    BasicLib.Instance.Barcodesp.PortName = BasicLib.Instance.BarcodePort;
                    BasicLib.Instance.Barcodesp.BaudRate = BasicLib.Instance.BarcodeBautrate;
                    BasicLib.Instance.Barcodesp.Parity = (Parity)BasicLib.Instance.BarcodeParity;
                    BasicLib.Instance.Barcodesp.StopBits = (StopBits)BasicLib.Instance.BarcodeStopBit;
                    BasicLib.Instance.Barcodesp.DataBits = BasicLib.Instance.BarcodeDataBit;
                    BasicLib.Instance.Barcodesp.Open();
                    BasicLib.Instance.Write();
                }
                catch { }
            }
        }
        public int ReceiveScrewString(ref string str)
        {
            if (BasicLib.Instance.Screw1sp.IsOpen == false)
            {
                ConnectScrew();
            }

            else if (BasicLib.Instance.Screw1sp.IsOpen == true)
            {
                BasicLib.Instance.Screw1sp.DiscardOutBuffer();
                BasicLib.Instance.Screw1sp.DiscardInBuffer();
                ReceiveASCII(BasicLib.Instance.Screw1sp, ref str);
                //while (str == "")
                //{
                //    ReceiveASCII(BasicLib.Instance.Screwsp, ref str);
                //    Thread.Sleep(100);
                //    cTime++;
                //}
                Thread.Sleep(20);
                return 0;
            }
            return 1;
        }
    }
}
