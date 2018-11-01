using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ManualAssembly
{
    public class XMLsetting
    {
        public void add(string filepath, string barcode, string processinfo)
        {
            XmlWriterSettings xws = new XmlWriterSettings();
            xws.Indent = true;
            xws.IndentChars = "   ";
            using (XmlWriter xw = XmlWriter.Create(filepath, xws))
            {
                xw.WriteStartElement("dsWIPTD");
                xw.WriteStartElement("WIPTDHeader");
                xw.WriteElementString("Process", processinfo);
                xw.WriteEndElement();
                xw.WriteEndElement();
            }
        }
    }
}
