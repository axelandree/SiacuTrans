using Claro.Data.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace Claro.Data
{
    public class RestService
    {
        private static WebHeaderCollection GetHeaders(Hashtable table)
        {
            WebHeaderCollection Headers = new WebHeaderCollection();
            foreach (DictionaryEntry entry in table)
            {
                Headers.Add(entry.Key.ToString(), entry.Value != null ? entry.Value.ToString() : null);
            }
            Headers.Add("Authorization", "Basic " + System.Configuration.ConfigurationManager.AppSettings["strEncriptSiacu"]);

            return Headers;
        }
        private static WebHeaderCollection GetHeaderswithOutDP(Hashtable table)
        {
            WebHeaderCollection Headers = new WebHeaderCollection();
            foreach (DictionaryEntry entry in table)
            {
                Headers.Add(entry.Key.ToString(), entry.Value != null ? entry.Value.ToString() : null);
            }


            return Headers;
        }
        private static WebHeaderCollection GetHeadersSDP(Hashtable table)
        {
            WebHeaderCollection Headers = new WebHeaderCollection();

            foreach (DictionaryEntry entry in table)
            {
                Headers.Add(entry.Key.ToString(), entry.Value != null ? entry.Value.ToString() : null);
            }

            return Headers;
        }

        private static RestServiceConfigurationElement configServices(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException("Name");
            }
            if (name.Length == 0)
            {
                throw new ArgumentException("Name", "Size non-zero.");
            }
            Claro.Web.Logging.Info("RestconfigServices", "RestconfigServices", name);
            RestServiceConfigurationElement cfgSservice = DataSettings.RestServices[name];
            if (cfgSservice == null)
            {
                throw new ArgumentException(string.Format("The key \"{0}\" not found in webservice section config.", name));
            }
            Claro.Web.Logging.Error(DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ"), DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ"), string.Format("Key Service Rest: \"{0}\" , URL Service Rest: \"{1}\"", name, cfgSservice.Url));

            return cfgSservice;
        }

        private static string urlServices(string name, Hashtable paramUrl, string[] param, out int timeout)
        {
            string strUrl = configServices(name).Url;
            Claro.Web.Logging.Info(DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ"), DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ"), string.Format("Ejecucion de servicio REST: {0} ", strUrl));
            timeout = configServices(name).Timeout;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(strUrl);
            if (param != null && param.Length > 0)
                foreach (string item in param)
                {
                    sb.Append("/" + item.ToString());
                }
            if (paramUrl != null && paramUrl.Count > 0)
            {
                bool blState = true;
                foreach (DictionaryEntry entry in paramUrl)
                {
                    if (entry.Value == null || entry.Key == null) continue;
                    if (blState)
                    {
                        sb.Append("?" + entry.Key.ToString() + "=" + entry.Value.ToString());
                        blState = false;
                    }
                    else
                    {
                        sb.Append("&" + entry.Key.ToString() + "=" + entry.Value.ToString());
                    }
                }
            }
            return sb.ToString();
        }

        public static T GetInvoque<T>(string name, object obj, Hashtable paramUrl, params string[] param)
        {
            int strtimeout;
            HttpWebRequest request = HttpWebRequest.Create(urlServices(name, paramUrl, param, out strtimeout)) as HttpWebRequest;
            request.Method = "Get";
            request.Headers = GetHeaders(paramHeader(obj));
            request.Accept = "application/json";
            request.Timeout = strtimeout;
            String responseString;
            try
            {
                WebResponse ws = request.GetResponse();
                using (Stream stream = ws.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                    responseString = reader.ReadToEnd();
                }
                return JsonConvert.DeserializeObject<T>(responseString);
            }
            catch (WebException ex)
            {
                WebResponse ws = ex.Response;
                responseString = string.Empty;
                using (Stream stream = ws.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                    responseString = reader.ReadToEnd();
                    Claro.Web.Logging.Error("", "responseString catch-WebException", responseString);
                }
                throw new Exception(string.Format("{0} - {1}", ex.Message, responseString));
            }
            catch
            {
                Claro.Web.Logging.Error("", "responseString catch", "");
                throw;
            }
        }

        public static T PostInvoque<T>(string name, object Audit, object obj, bool osb)
        {
            HttpWebRequest request = HttpWebRequest.Create(configServices(name).Url) as HttpWebRequest;
            request.Method = "POST";
            request.Headers = !osb ? GetHeaderswithOutDP(paramHeaderOsb(Audit)) : GetHeaders(paramHeaderOsb(Audit));
            request.Accept = "application/json";
            request.Timeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["strTimeOut"]);
            JavaScriptSerializer Serializer = new JavaScriptSerializer();
            string data = Newtonsoft.Json.JsonConvert.SerializeObject(obj,Formatting.Indented);
            Claro.Web.Logging.Info(name, "requestString ", data);
            byte[] byteArray = Encoding.UTF8.GetBytes(data);
            request.ContentType = "application/json";
            request.ContentLength = byteArray.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            try
            {
                WebResponse ws = request.GetResponse();
                using (Stream stream = ws.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                    String responseString = reader.ReadToEnd();
                    Claro.Web.Logging.Info(name, "responseString ", responseString);
                    T result = JsonConvert.DeserializeObject<T>(responseString);
                    return result;
                }
            }
            catch (WebException ex)
            {
                WebResponse ws = ex.Response;
                String responseString = string.Empty;
                using (Stream stream = ws.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                    responseString = reader.ReadToEnd();
                    Claro.Web.Logging.Error(name, "responseString catch-WebException", responseString);
                }
                throw new Exception(string.Format("{0} - {1}", ex.Message, responseString));
            }
            catch
            {
                Claro.Web.Logging.Error("", "responseString catch", "");
                throw;
            }
        }

        public sealed class StringWriterWithEncoding : StringWriter
        {
            private readonly Encoding encoding;

            public StringWriterWithEncoding(StringBuilder sb, Encoding encoding)
                : base(sb)
            {
                this.encoding = encoding;
            }

            public override Encoding Encoding
            {
                get
                {
                    return encoding;
                }
            }
        }
        public static T PostInvoqueXML<T>(string name, object obj)
        {
            HttpWebRequest request = HttpWebRequest.Create(configServices(name).Url) as HttpWebRequest;
            request.Method = "POST";
            request.Timeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["strTimeOut"]);

            XmlSerializer xmlSerializer;
            xmlSerializer = new XmlSerializer(obj.GetType());

            var stringBuilder = new StringBuilder();
            new XmlSerializer(obj.GetType()).Serialize(new StringWriterWithEncoding(stringBuilder, System.Text.Encoding.UTF8), obj);
            string xml = stringBuilder.ToString();
            Claro.Web.Logging.Error("", "requestString xml ", xml);
            byte[] byteArray;
            byteArray = System.Text.Encoding.ASCII.GetBytes(xml);
            request.ContentType = "text/xml; encoding='utf-8'";
            request.ContentLength = byteArray.Length;

            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            try
            {
                WebResponse ws = request.GetResponse();
                using (Stream stream = ws.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                    String responseString = reader.ReadToEnd();
                    Claro.Web.Logging.Error("", "requestString xml ", responseString);
                    xmlSerializer = new XmlSerializer(typeof(T));
                    StringReader stringreader = new StringReader(responseString);
                    T result = (T)xmlSerializer.Deserialize(stringreader);
                    return result;

                }
            }
            catch (WebException ex)
            {
                WebResponse ws = ex.Response;
                String responseString = string.Empty;
                using (Stream stream = ws.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                    responseString = reader.ReadToEnd();
                    Claro.Web.Logging.Error("", "responseString catch-WebException", responseString);
                }
                throw new Exception(string.Format("{0} - {1}", ex.Message, responseString));
            }
            catch
            {
                Claro.Web.Logging.Error("", "responseString catch", "");
                throw;
            }
        }
        //Sin DataPower
        //Sin DataPower
        public static T PostInvoqueSDP<T>(string name, Hashtable objHeader, object obj)
        {

            HttpWebRequest request = HttpWebRequest.Create(configServices(name).Url) as HttpWebRequest;
            request.Method = "POST";
            request.Headers = GetHeadersSDP(objHeader);
            request.Accept = "application/json";
            request.Timeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["strTimeOut"]);
            JavaScriptSerializer Serializer = new JavaScriptSerializer();
            Claro.Web.Logging.Info(name, "", string.Format("Key Service Rest: \"{0}\" , URL Service Rest: \"{1}\"", name, configServices(name).Url));
            string data = Newtonsoft.Json.JsonConvert.SerializeObject(obj,Formatting.Indented);
            Claro.Web.Logging.Info("REQUEST", name, data);
            byte[] byteArray = Encoding.UTF8.GetBytes(data);
            request.ContentType = "application/json";
            request.ContentLength = byteArray.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            try
            {
                WebResponse ws = request.GetResponse();
                using (Stream stream = ws.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                    String responseString = reader.ReadToEnd();
                    T result = JsonConvert.DeserializeObject<T>(responseString);
                    Claro.Web.Logging.Info("RESPONSE", name, responseString);
                    return result;
                }

            }

            catch (WebException ex)
            {
                WebResponse ws = ex.Response;
                String responseString = string.Empty;
                using (Stream stream = ws.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                    responseString = reader.ReadToEnd();
                    Claro.Web.Logging.Error("", "responseString catch-WebException", responseString);
                }
                throw new Exception(string.Format("{0} - {1}", ex.Message, responseString));
            }
            catch
            {
                Claro.Web.Logging.Error("", "responseString catch", "");
                throw;
            }
        }
        public static T PutInvoque<T>(string name, object Audit, object obj)
        {
            HttpWebRequest request = HttpWebRequest.Create(configServices(name).Url) as HttpWebRequest;
            request.Method = "PUT";
            request.Headers = GetHeaders(paramHeader(Audit));
            request.Accept = "application/json";
            JavaScriptSerializer Serializer = new JavaScriptSerializer();
            string data = Newtonsoft.Json.JsonConvert.SerializeObject(obj,Formatting.Indented);
            byte[] byteArray = Encoding.UTF8.GetBytes(data);
            request.ContentType = "application/json";
            request.ContentLength = byteArray.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            try
            {
                WebResponse ws = request.GetResponse();
                using (Stream stream = ws.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                    String responseString = reader.ReadToEnd();
                    T result = JsonConvert.DeserializeObject<T>(responseString);
                    return result;
                }
            }
            catch (WebException ex)
            {
                WebResponse ws = ex.Response;
                String responseString = string.Empty;
                using (Stream stream = ws.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                    responseString = reader.ReadToEnd();
                    Claro.Web.Logging.Error("", "responseString catch-WebException", responseString);
                }
                throw new Exception(string.Format("{0} - {1}", ex.Message, responseString));
            }
            catch
            {
                Claro.Web.Logging.Error("", "responseString catch", "");
                throw;
            }
        }

        public static Hashtable paramHeader(object obj)
        {
            Hashtable paramHeaders = new Hashtable();
            PropertyInfo[] props = obj.GetType().GetProperties();
            foreach (PropertyInfo p in props)
            {
                object valor = p.GetValue(obj, null);
                if (p.Name.Equals("Transaction"))
                {
                    paramHeaders.Add("idTransaccion", valor);
                    paramHeaders.Add("msgid", valor);
                }
                else if (p.Name.Equals("UserName"))
                {
                    paramHeaders.Add("userId", valor);
                }
            }
            paramHeaders.Add("timestamp", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ"));
            return paramHeaders;
        }

        public static Hashtable paramHeaderOsb(object obj)
        {
            Hashtable paramHeaders = new Hashtable();
            PropertyInfo[] props = obj.GetType().GetProperties();
            foreach (PropertyInfo p in props)
            {
                object valor = p.GetValue(obj, null);
                if (p.Name.Equals("Transaction"))
                {
                    paramHeaders.Add("idTransaccionESB", valor);
                    paramHeaders.Add("idTransaccionNegocio", valor);
                    paramHeaders.Add("idTransaccion", valor);
                    paramHeaders.Add("msgid", valor);
                }
                else if (p.Name.Equals("UserName"))
                {
                    paramHeaders.Add("usuarioSesion", valor);
                    paramHeaders.Add("userId", valor);
                }
            }
            paramHeaders.Add("canal", System.Configuration.ConfigurationManager.AppSettings["strCanal"]);
            paramHeaders.Add("idAplicacion", System.Configuration.ConfigurationManager.AppSettings["strIdAplicacion"]);
            paramHeaders.Add("usuarioAplicacion", System.Configuration.ConfigurationManager.AppSettings["strIdTransaccionESB"]);
            paramHeaders.Add("fechaInicio", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ"));
            paramHeaders.Add("nodoAdicional", System.Configuration.ConfigurationManager.AppSettings["strNodoAdicional"]);
            paramHeaders.Add("timestamp", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ"));    
            return paramHeaders;
        } 

        public static T GetInvoqueDP<T>(string name, object obj, Hashtable paramUrl, Entity.UsernameToken objCredentails, string strAccept = "", params string[] param)
        {
            int strtimeout;
            HttpWebRequest request = HttpWebRequest.Create(urlServices(name, paramUrl, param, out strtimeout)) as HttpWebRequest;
            request.Method = "Get";
            request.Headers = GetHeadersDP(paramHeader(obj), objCredentails);
            request.Accept = strAccept == "" ? "application/json" : strAccept;
            request.Timeout = strtimeout;
            String responseString;
            try
            {
                WebResponse ws = request.GetResponse();
                using (Stream stream = ws.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                    responseString = reader.ReadToEnd();
                }
                return JsonConvert.DeserializeObject<T>(responseString);
            }
            catch (WebException ex)
            {
                WebResponse ws = ex.Response;
                responseString = string.Empty;
                using (Stream stream = ws.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                    responseString = reader.ReadToEnd();
                    Claro.Web.Logging.Error("", "responseString catch-WebException", responseString);
                }
                throw new Exception(string.Format("{0} - {1}", ex.Message, responseString));
            }
            catch
            {
                Claro.Web.Logging.Error("", "responseString catch", "");
                throw;
            }
        }

        public static T PostInvoqueDP<T>(string name, object Audit, object obj, bool osb, Entity.UsernameToken objCredentails, string serviceName = "", bool flagLogRequest = false, string strAccept = "")
        {
            HttpWebRequest request = HttpWebRequest.Create(configServices(name).Url + serviceName) as HttpWebRequest;
            request.Method = "POST";
            request.Headers = GetHeadersDP(osb ? paramHeaderOsb(Audit) : paramHeader(Audit), objCredentails);
            request.Accept = strAccept == "" ? "application/json" : strAccept;
            JavaScriptSerializer Serializer = new JavaScriptSerializer();
            string data = Newtonsoft.Json.JsonConvert.SerializeObject(obj,Formatting.Indented);
            if (flagLogRequest)
            {
                Claro.Web.Logging.Info(serviceName, "Request JSON Rest", data);
            }

            byte[] byteArray = Encoding.UTF8.GetBytes(data);
            request.ContentType = "application/json";
            request.ContentLength = byteArray.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            WebResponse ws = request.GetResponse();
            using (Stream stream = ws.GetResponseStream())
            {
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                String responseString = reader.ReadToEnd();
                T result = JsonConvert.DeserializeObject<T>(responseString);
                return result;
            }
        }

        public static T GetInvoqueBack<T>(string name, object obj, Hashtable paramUrl, params string[] param)
        {
            int strtimeout;
            HttpWebRequest request = HttpWebRequest.Create(urlServices(name, paramUrl, param, out strtimeout)) as HttpWebRequest;
            request.Method = "Get";
            request.Headers = GetHeadersBack(paramHeader(obj));
            request.Accept = "application/json";
            request.Timeout = strtimeout;
            String responseString;
            try
            {
                WebResponse ws = request.GetResponse();
                using (Stream stream = ws.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                    responseString = reader.ReadToEnd();
                }
                return JsonConvert.DeserializeObject<T>(responseString);
            }
            catch (WebException ex)
            {
                WebResponse ws = ex.Response;
                responseString = string.Empty;
                using (Stream stream = ws.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                    responseString = reader.ReadToEnd();
                    Claro.Web.Logging.Error("", "responseString catch-WebException", responseString);
                }
                throw new Exception(string.Format("{0} - {1}", ex.Message, responseString));
            }
            catch
            {
                Claro.Web.Logging.Error("", "responseString catch", "");
                throw;
            }
        }

        private static WebHeaderCollection GetHeadersBack(Hashtable table)
        {
            WebHeaderCollection Headers = new WebHeaderCollection();
            foreach (DictionaryEntry entry in table)
            {
                Headers.Add(entry.Key.ToString(), entry.Value != null ? entry.Value.ToString() : null);
            }

            return Headers;
        }
        private static WebHeaderCollection GetHeadersDP(Hashtable table, Entity.UsernameToken objCredentials)
        {
            WebHeaderCollection Headers = new WebHeaderCollection();
            foreach (DictionaryEntry entry in table)
            {
                Headers.Add(entry.Key.ToString(), entry.Value != null ? entry.Value.ToString() : null);
            }
            string auth = string.Format("{0}:{1}", objCredentials.Username, objCredentials.Password.Value);
            Headers.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes(auth)));
            return Headers;
        }

//INICIO PRO-140447
        public static T PostInvoque2<T>(string url, object Audit, object obj, bool osb, string userEncriptado, string passEncriptado)
        {
            HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
            request.Method = "POST";
            Claro.Web.Logging.Error("", "", "inicio GetHeaders2");
            request.Headers = GetHeaders2(paramHeaderOsb(Audit), userEncriptado,passEncriptado);
            Claro.Web.Logging.Error("", "", "fin GetHeaders2");
            request.Accept = "application/json";
            request.Timeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["strTimeOut"]);
            JavaScriptSerializer Serializer = new JavaScriptSerializer();
            string data = Newtonsoft.Json.JsonConvert.SerializeObject(obj,Formatting.Indented);
            Claro.Web.Logging.Error("", "",string.Format("data MiClaroApp => {0}",data));
            byte[] byteArray = Encoding.UTF8.GetBytes(data);
            request.ContentType = "application/json";
            request.ContentLength = byteArray.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            try
            {
                WebResponse ws = request.GetResponse();
                using (Stream stream = ws.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                    String responseString = reader.ReadToEnd();
                    T result = JsonConvert.DeserializeObject<T>(responseString);
                    return result;
                }
            }
            catch (WebException ex)
            {
                WebResponse ws = ex.Response;
                String responseString = string.Empty;
                using (Stream stream = ws.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                    responseString = reader.ReadToEnd();
                    Claro.Web.Logging.Error("", "responseString catch-WebException X", responseString);
                }
                throw new Exception(string.Format("{0} - {1}", ex.Message, responseString));
            }
            catch
            {
                Claro.Web.Logging.Error("", "responseString catch X", "");
                throw;
            }
        }

        private static WebHeaderCollection GetHeaders2(Hashtable table, string userEncriptado, string passEncriptado)
        {
            WebHeaderCollection Headers = new WebHeaderCollection();
            foreach (DictionaryEntry entry in table)
            {
                Headers.Add(entry.Key.ToString(), entry.Value != null ? entry.Value.ToString() : null);
            }
            Headers.Add("Authorization", "Basic " + System.Configuration.ConfigurationManager.AppSettings["strEncriptSiacuREST"]);

            return Headers;
        }
    }
}
