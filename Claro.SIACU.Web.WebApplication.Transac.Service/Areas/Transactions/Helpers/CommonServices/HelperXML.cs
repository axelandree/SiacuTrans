using Claro.SIACU.Web.WebApplication.Transac.Service.App_Code;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.CommonServices
{
    public class HelperXML
    {
        public static string CreateXMLString(Dictionary<string, string> dict, string folderTemplate, string nameTemplate)
        {
            string strXML = string.Empty;
            XsltArgumentList xslArg = new XsltArgumentList();
            StringBuilder xml = new StringBuilder();
            XmlWriter xmlWriter = XmlWriter.Create(xml); 
            foreach (var item in dict)
            {
                xslArg.AddParam(item.Key, "", item.Value!=null ? item.Value:String.Empty);
            }
        
            try
            { 
                string filename = String.Format("{0}/{1}/{2}.xml", Common.GetApplicationRoute(), folderTemplate, nameTemplate);
                string stylesheet = String.Format("{0}/{1}/{2}.xslt", Common.GetApplicationRoute(), folderTemplate, nameTemplate);

                XPathDocument doc = new XPathDocument(filename); 
                XslCompiledTransform xslCT = new XslCompiledTransform(); 
                xslCT.Load(stylesheet);
                xslCT.Transform(doc, xslArg, xmlWriter, null);
                xmlWriter.Close();
                string myString = xml.ToString();
                byte[] myByteArray = System.Text.Encoding.UTF8.GetBytes(myString);
                MemoryStream ms = new MemoryStream(myByteArray);
                StreamReader sr = new StreamReader(ms);
                var reader = XmlReader.Create(sr);
                reader.MoveToContent();
                var inputXml = XDocument.ReadFrom(reader);
                strXML = inputXml.ToString();
            }
            catch (Exception ex)
            {
                //Claro.Web.Logging.Error(strIdSession, straudit.transaction, Claro.SIACU.Transac.Service.Functions.GetExceptionMessage(ex));
                strXML = String.Empty; 
            }
            return strXML;
        }
    }
}