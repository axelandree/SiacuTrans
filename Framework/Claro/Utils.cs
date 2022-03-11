using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Claro
{
    public class Utils
    {
        public string GetApplicationPath()
        {
            return AppDomain.CurrentDomain.BaseDirectory.ToString();
        }

        static public DateTime GetDDMMYYYYAsDateTime(string pisValue)
        {
            string sDay, sMonth, sYear;
            if (pisValue.Length != 10)
            {
                //log.Info(string.Format("La cadena ingresada [" + pisValue + "] no tiene el formato correcto DD/MM/YYYY"));
            }
            sDay = pisValue.Substring(0, 2);
            sMonth = pisValue.Substring(3, 2);
            sYear = pisValue.Substring(6, 4);
            DateTime dtValue = new DateTime(int.Parse(sYear), int.Parse(sMonth), int.Parse(sDay));
            return (dtValue);
        }

        /// <summary>
        /// Obtener Formato para HHMMSS recibiendo cantidad
        /// </summary>
        /// <param name="cantidad"></param>
        /// <returns></returns>
        public string GetFormatHHMMSS(int cantidad)
        {
            string result;
            int intHoras = 0;
            int intSegundos;
            if (cantidad >= 3600)
            {
                intHoras = System.Convert.ToInt32(cantidad / 3600);
                intSegundos = System.Convert.ToInt32(cantidad % 3600);
                result = GetFormatMMSS(intSegundos);
            }
            else
            {
                result = GetFormatMMSS(cantidad);
            }
            result = intHoras.ToString("00") + ":" + result;
            return result;
        }

        /// <summary>
        /// Obtener Formato para MMSS Recibiendo cantidad
        /// </summary>
        /// <param name="cantidad"></param>
        /// <returns></returns>
        public string GetFormatMMSS(int cantidad)
        {
            string result;
            if (cantidad > 60)
            {
                int intMinutos = System.Convert.ToInt32(cantidad / 60);
                int intSegundos = System.Convert.ToInt32(cantidad % 60);
                result = String.Format("{0}:{1}", intMinutos.ToString("00"), intSegundos.ToString("00"));
            }
            else
            {
                result = String.Format("00:{0}", cantidad.ToString("00"));
            }
            return result;
        }

        public string GetDivision(double dblDividend, double dblDivisor)
        {
            double dblResult;
            double dblConstantCero = 0.0;
            double COMPARISON_VALUE = 0.0000001;

            if (((dblDividend - dblConstantCero) < COMPARISON_VALUE) || ((dblDivisor - dblConstantCero) < COMPARISON_VALUE))
            {
                dblResult = 0.0;
            }
            else
            {
                dblResult = dblDividend / dblDivisor;
            }

            return dblResult.ToString(CultureInfo.InvariantCulture);
        }

        public static string CheckFormatHHMMSS(double cantidad, string tipo, bool isPrepago)
        {
            string result;
            int indexdescription = tipo.IndexOf("LLAMADA", StringComparison.InvariantCulture);
            int indexdescripcionmoc = tipo.IndexOf("MOC", StringComparison.InvariantCulture);

            if (isPrepago)
            {
                if (indexdescription != -1 || indexdescripcionmoc != -1)
                {
                    cantidad = cantidad * 60;
                    result = new Utils().GetFormatHHMMSS(System.Convert.ToInt32(cantidad));
                }
                else
                {
                    result = cantidad.ToString(CultureInfo.InvariantCulture);
                }
            }
            else
            {
                if (indexdescription != -1)
                {
                    result = new Utils().GetFormatHHMMSS(System.Convert.ToInt32(cantidad));
                }
                else
                {
                    result = cantidad.ToString(CultureInfo.InvariantCulture);
                }
            }

            return result;
        }

        public static string ConvertSoles(object value)
        {
            string salida;

            if (value == null || value == System.DBNull.Value)
            {
                salida = "0.00";
            }
            else
            {
                salida = (System.Convert.ToString(value) == "" ? "0.00" : string.Format("{0:n}", System.Convert.ToDouble(value) / 100));
            }

            return salida;
        }
       
        public static string GetValueFromConfigFileIFI(string clave, string fileName)
        {
            string strApplicationPath = AppDomain.CurrentDomain.BaseDirectory.ToString();
            //string fullPath = strApplicationPath + fileName;
            string fullPath = string.Format("{0}{1}\\{2}", strApplicationPath, Claro.Constants.FileSiacutDataIfi, fileName);//DataTransac
            try
            {
                XmlDocument documento = new XmlDocument();
                documento.Load(fullPath);
                XmlNodeList UserList = documento.GetElementsByTagName(clave);
                string strGetValue = UserList.Item(0).InnerText.ToString();
                return strGetValue;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return "";
            }
        }

        public static string GetExceptionMessage(Exception ex)
        {
            var strExcepMsg = string.Empty;
            if (ex.InnerException == null)
            {
                strExcepMsg = ex.Message;
            }
            else if (ex.InnerException.Message == null)
            {
                strExcepMsg = ex.Message;
            }
            else
            {
                strExcepMsg = ex.InnerException.Message;
            }

            return strExcepMsg;
        }
        static public string CheckStr(object value)
        {
            string salida = "";
            if (value == null || value == System.DBNull.Value)
                salida = "";
            else
                salida = value.ToString();
            return salida.Trim();
        }

        static public double CheckDbl(object value)
        {
            double salida = 0;
            if (value == null || value == System.DBNull.Value || value == "null")
            {
                salida = 0;
            }
            else
            {
                if (Convert.ToString(value) == "" || Convert.ToString(value) == "&nbsp;" || Convert.ToString(value) == "&nbsp")
                    salida = 0;
                else
                    salida = Convert.ToDouble(value);
            }
            return salida;
        }
        static public object CheckDblDB(object value)
        {
            double salida = 0;
            if (value == null || value == System.DBNull.Value)
            {
                return System.DBNull.Value;
            }
            salida = Convert.ToDouble(value);
            return salida;
        }
        static public DateTime CheckDate(object value)
        {
            if (value == null || value == System.DBNull.Value)
                return new DateTime(1, 1, 1);

            if (value.ToString() == "")
                return new DateTime(1, 1, 1);
            return System.Convert.ToDateTime(value);
        }
        static public Int64 CheckInt64(object value)
        {
            Int64 salida = 0;
            if (value == null || value == System.DBNull.Value)
            {
                salida = 0;
            }
            else
            {
                if (Convert.ToString(value) == "" || Convert.ToString(value) == "null")
                    salida = 0;
                else
                    salida = Convert.ToInt64(value);
            }
            return salida;
        }
        static public int CheckInt(object value)
        {
            int salida = 0;
            if (value == null || value == System.DBNull.Value)
            {
                salida = 0;
            }
            else
            {
                if (Convert.ToString(value) == "" || Convert.ToString(value) == "&nbsp;" || Convert.ToString(value) == "&nbsp")
                    salida = 0;
                else
                    salida = System.Convert.ToInt32(value);
            }
            return salida;
        }
        public static string CreateXML(Object YourClassObject)
        {
            XmlDocument xmlDoc = new XmlDocument();   //Represents an XML document, 
            // Initializes a new instance of the XmlDocument class.          
            XmlSerializer xmlSerializer = new XmlSerializer(YourClassObject.GetType());
            // Creates a stream whose backing store is memory. 
            using (MemoryStream xmlStream = new MemoryStream())
            {
                xmlSerializer.Serialize(xmlStream, YourClassObject);
                xmlStream.Position = 0;
                //Loads the XML document from the specified string.
                xmlDoc.Load(xmlStream);
                return xmlDoc.InnerXml;
            }
        }

        public static T? DbValueToNullable<T>(object dbValue) where T : struct
        {
            T? returnValue = null;

            if ((dbValue != null) && (dbValue != DBNull.Value))
            {
                returnValue = (T)dbValue;
            }

            return returnValue;
        }

        public static T DbValueToDefault<T>(object obj)
        {
            if (obj == null || obj == DBNull.Value) return default(T);
            return (T)obj;
        }
      
        static public bool IsNumeric(object value)
        {
            double numero;
            return Double.TryParse(Convert.ToString(value), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out numero);
        }

        public class ItemGeneric
        {
            public ItemGeneric() { }
            public ItemGeneric(string vCode, string vCode2, string vDescription)
            {
                Code = vCode;
                Code2 = vCode2;
                Description = vDescription;
            }
            public string Code { get; set; }
            public string Description { get; set; }
            public string Code2 { get; set; }
        }

        public static List<ItemGeneric> GetListValuesXMLIFI(string strNameFunction, string strFlagCode, string fileName)
        {

            string strApplicationPath = AppDomain.CurrentDomain.BaseDirectory.ToString();

            string fullPath = string.Format("{0}{1}\\{2}", strApplicationPath, Constants.FileSiacutDataIfi, fileName);

            XmlDocument documento = new XmlDocument();
            documento.Load(fullPath);
            XmlNodeList nodeList = documento.SelectNodes("descendant::" + strNameFunction + "/item");

            var lstItem = new List<ItemGeneric>();

            for (int i = 0; i < nodeList.Count; i++)
            {
                ItemGeneric objItemVM = null;
                switch (strFlagCode)
                {
                    case "1":
                        objItemVM = new ItemGeneric(nodeList.Item(i).ChildNodes[0].InnerText,
                                                 nodeList.Item(i).ChildNodes[1].InnerText,
                                                 nodeList.Item(i).ChildNodes[2].InnerText);
                        break;
                    case "2":
                        objItemVM = new ItemGeneric(nodeList.Item(i).ChildNodes[0].InnerText,
                                                    (nodeList.Item(i).ChildNodes[2] != null ? nodeList.Item(i).ChildNodes[2].InnerText : string.Empty),
                                                 nodeList.Item(i).ChildNodes[1].InnerText);
                        break;
                    default:
                        objItemVM = new ItemGeneric(nodeList.Item(i).ChildNodes[0].InnerText, "",
                                                 nodeList.Item(i).ChildNodes[1].InnerText);
                        break;
                }
                lstItem.Add(objItemVM);
            }

            return lstItem;
        }
        // Recibe un DateTime con valores en hora, minutos y segundos y lo trunca a 00:00:00
        public static DateTime TruncDateTime(DateTime pidtValue)
        {
            int iDay, iMonth, iYear;

            iDay = pidtValue.Day;
            iMonth = pidtValue.Month;
            iYear = pidtValue.Year;

            DateTime dtValue = new DateTime(iYear, iMonth, iDay);

            return (dtValue);

        }

        public static string f_obtieneContentType(string pExtArchivo)
        {
            string salida = "";
            if (pExtArchivo == ".htm" || pExtArchivo == ".html")
            {
                salida = "text/html";
            }
            else if (pExtArchivo == ".xls")
            {
                salida = "application/vnd.ms-excel";
            }
            else if (pExtArchivo == ".txt")
            {
                salida = "text/plain";
            }
            //else if (pExtArchivo == ".xls")
            //{
            //    salida = ".pdf";
            //}
            else if (pExtArchivo == ".pdf")
            {
                salida = "application/pdf";
            }
            else if (pExtArchivo == ".xml")
            {
                salida = "text/xml";
            }
            else if (pExtArchivo == ".doc" || pExtArchivo == ".docx")
            {
                salida = "application/msword";
            }
            else if (pExtArchivo == ".rtf")
            {
                salida = "application/rtf";
            }
            else if (pExtArchivo == ".odt")
            {
                salida = "application/vnd.oasis.opendocument.text";
            }
            else if (pExtArchivo == ".ods")
            {
                salida = "application/vnd.oasis.opendocument.spreadsheet";
            }
            else if (pExtArchivo == ".png")
            {
                salida = "image/png";
            }
            else if (pExtArchivo == ".jpg" || pExtArchivo == ".jpeg")
            {
                salida = "image/jpeg";
            }
            else if (pExtArchivo == ".gif")
            {
                salida = "image/gif";
            }
            else if (pExtArchivo == ".bmp")
            {
                salida = "image/bmp";
            }
            else if (pExtArchivo == ".tif" || pExtArchivo == ".tiff")
            {
                salida = "image/tiff";
            }
            else if (pExtArchivo == ".zip")
            {
                salida = "application/zip";
            }
            else if (pExtArchivo == ".rar")
            {
                salida = "application/x-rar-compressed";
            }
            else if (pExtArchivo == ".ppt")
            {
                salida = "application/mspowerpoint";
            }
            else if (pExtArchivo == ".swf")
            {
                salida = "application/x-shockwave-flash";
            }
            else
            {
                salida = "application/octet-stream";
            }

            return salida;
        }
    }
}
