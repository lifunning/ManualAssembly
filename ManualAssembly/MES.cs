using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManualAssembly
{
    public class SerialData
    {
        public List<string> RawData { get; set; }
        public List<string> Torque { get; set; }
        public string TorqueUnit { get; set; }
        public List<string> FastenTime { get; set; }
        public List<string> FastenThread { get; set; }
        public List<string> ScrewCount { get; set; }
        public List<string> TotalScrewCount { get; set; }
        public List<string> Status { get; set; }

        public void dataprocess()
        {

        }
    }
    public class MES
    {

        static int SubstringCount(string str, string substring)
        {
            if (str.Contains(substring))
            {
                string strReplaced = str.Replace(substring, "");
                return (str.Length - strReplaced.Length) / substring.Length;
            }
            return 0;
        }
    }
}