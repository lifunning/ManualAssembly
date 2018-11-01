using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ManualAssembly
{
    public class Raw
    {
        public string Rawdata { get; set; }
        public string Torque { get; set; }
        public string TorqueUnit { get; set; }
        public string FastenTime { get; set; }
        public string FastenThread { get; set; }
        public string ScrewCount { get; set; }
        public string TotalScrewCount { get; set; }
        public string Status { get; set; }
        public string screwcount { get; set; }
        public string Totalcount { get; set; }

        public Raw()
        {
            
        }
        private int SubstringCount(string str, string substring)
        {
            if (str.Contains(substring))
            {
                string strReplaced = str.Replace(substring, "");
                return (str.Length - strReplaced.Length) / substring.Length;
            }

            return 0;
        }
        public int NewRow(string Rawdata)
        {
            try
            {
                int CommaCount;   //逗号个数
                string seperate = ",";
                string[] sArray = Regex.Split(Rawdata, ",", RegexOptions.IgnoreCase);

                //CommaCount = SubstringCount(Rawdata, seperate);
                if (sArray.Length > 12)
                {
                    Torque = sArray[8];
                    TorqueUnit = sArray[9];
                    FastenTime = sArray[10];
                    FastenThread = sArray[11];
                    Status = sArray[14];
                    screwcount = sArray[12].Substring(4, 3);
                    Totalcount = sArray[12].Substring(4, 3);
                }
                else
                {
                    return -1;
                }
            }catch(Exception e)
            {
                return -2;
            }
            
            
            return 0;
        }
        
    }
   public class RawProcess
    {
        private static RawProcess instance;
        private static object threadLock = new object();
        public List<Raw> rawlist;

        public static RawProcess Instance
        {
            get
            {
                lock (threadLock)
                {
                    if (instance == null)
                    {
                        instance = new RawProcess();
                    }
                    return instance;
                }
            }
        }
        public  RawProcess()
        {
            rawlist = new List<Raw>();
        }
        public int setListNum(int num)
        {
            rawlist.Clear();
            for (int i = 0; i < num; i++)
            {
                rawlist.Add(new Raw());
            }
            return 0;
        }
        
        public int addRaw(string Rawdata)
        {

            Raw aRaw = new Raw();
            aRaw.NewRow(Rawdata);
            int num = Convert.ToInt16(aRaw.Totalcount) - Convert.ToInt16(aRaw.ScrewCount);
            if (num == 0 && rawlist.Count==0)
            {
                setListNum(Convert.ToInt16(aRaw.ScrewCount));
            }
            if (num < 1 || num> rawlist.Count)
            {
                return -1;
            }
            if (aRaw.screwcount!= rawlist[num].ScrewCount && aRaw.Status!= rawlist[num].Status)
            {
                rawlist[num] = aRaw;
            }

            //string rawdata = "DATA0,1,001,B52700106___________,0___________________,0000000166,01,01,0000.0000,kgf.cm,0000.0000,0000.0000,001 / 003,DEC,REV__,1,0,0,2018,10,25,16,34,09,2112,7550";

                return 0;
        }
    }
}
