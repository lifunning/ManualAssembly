using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Security.Cryptography;
using System.CodeDom;
using System.Collections.Specialized;
using System.Runtime.Serialization;

namespace ManualAssembly
{
    public class Hasher
    {
        public string _HashText; //待加密的字符串
        // 需要产生加密哈希的字符串

        public string HashText
        {
            set
            {
                _HashText = value;
            }
            get
            {
                return _HashText;
            }
        }

        /// 使用MD5CryptoServiceProvider类产生哈希值。不需要提供密钥。

        /// </summary>
        /// <returns></returns>

        public string MD5Hasher()
        {
            byte[] MD5Data = System.Text.Encoding.UTF8.GetBytes(HashText);
            MD5 Md5 = new MD5CryptoServiceProvider();
            byte[] Result = Md5.ComputeHash(MD5Data);
            return Convert.ToBase64String(Result); //返回长度为字节字符串
        }

        //获取cpu序列号

        public String GetCpuID()
        {
            ManagementClass mc = new ManagementClass("Win32_Processor");
            ManagementObjectCollection moc = mc.GetInstances();
            String strCpuID = null;
            foreach (ManagementObject mo in moc)
            {
                strCpuID = mo.Properties["ProcessorId"].Value.ToString();
                break;
            }
            return strCpuID;
        }//end method

        //获取硬盘序列号

        public String GetDriveID()
        {
            ManagementObject mo = new ManagementObject("win32_LogicalDisk.deviceid=\"c:\"");
            mo.Get();
            return mo.GetPropertyValue("VolumeSerialNumber").ToString();
        }

        public String strJia()
        {
            string strJiami = "";
            strJiami = 56 + GetCpuID() + 9987 + GetDriveID();
            return strJiami;
        }
        public string jiqixuelie;
        public string duijiqima = null;

        ///得到机器码
        public string hashGetDriveID()
        {
            Hasher hs = new Hasher();
            hs.HashText = hs.strJia();
            string jiqi = hs.MD5Hasher();
            duijiqima = jiqi.Substring(7, 5);
            return duijiqima;
        }

        public string Sern()
        //最终的序列号//m为配置文件值
        //x为序列号值
        //css进行比较该注册码是否正确
        {
            //获取机器码
            Hasher hs = new Hasher();
            //获取序列号
            Hasher hash = new Hasher();
            hash.HashText = hs.hashGetDriveID().ToString();
            jiqixuelie = hash.MD5Hasher();
            return jiqixuelie;
        }
    }
}