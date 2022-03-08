using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;
using KEY = System.Configuration.ConfigurationManager;

namespace Claro.SIACU.Transac.Service
{
    public class ItemGeneric
    {
        public ItemGeneric(){ }
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

    public class Functions
    {
        //private static readonly ILog log = LogManager.GetLogger(typeof(Funciones).Name);
        public Functions()
        {
        }

        public static List<ItemGeneric> GetListValuesXML(string strNameFunction, string strFlagCode, string fileName)
        {

            string strApplicationPath = AppDomain.CurrentDomain.BaseDirectory.ToString();
          
           string fullPath =string.Format("{0}{1}\\{2}", strApplicationPath,Constants.FileSiacutData,fileName);
           
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
                        objItemVM = new ItemGeneric(nodeList.Item(i).ChildNodes[0].InnerText,"",
                                                 nodeList.Item(i).ChildNodes[1].InnerText);
                        break;
                }
                lstItem.Add(objItemVM);
            }

            return lstItem;
        }
    
        public static string GetValueFromConfigSiacUnico(string clave){
            //return GetValueFromConfigFile( clave,  "SiacUnico.config");
            return GetValueFromConfigFile(clave, "SiactUnico.config");  //DataTransac
        }

        public static string GetValueFromConfigFile(string clave, string fileName)
        {
            string strApplicationPath = AppDomain.CurrentDomain.BaseDirectory.ToString();
            //string fullPath = strApplicationPath + fileName;
            string fullPath = string.Format("{0}{1}\\{2}", strApplicationPath, Constants.FileSiacutData, fileName);//DataTransac
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
        public static bool IsRestrictedPlan(string planCode, string strRestrictedPlans, string strPermissions)
        {
            bool retorno = false;
            bool flagPerfilAutorizado = false;
            bool flagPlanRestringido = false;

            string strkeyRestringirDetalleLlamada = KEY.AppSettings["gConstkeyRestringirConsultaDetalleLlamada"];

            if (strPermissions.IndexOf(strkeyRestringirDetalleLlamada) >= 0)
                flagPerfilAutorizado = true;

            if (!string.IsNullOrEmpty(planCode))
            {
                if (!string.IsNullOrEmpty(strRestrictedPlans))
                {
                    string[] arrPlanesRestringidos = strRestrictedPlans.Split(',');
                    foreach (string pr in arrPlanesRestringidos)
                    {
                        if (planCode.Trim().Equals(pr.Trim()))
                        {
                            flagPlanRestringido = true;
                            break;
                        }
                    }
                }
            }

            if (flagPlanRestringido)
            {
                if (!flagPerfilAutorizado)
                    retorno = true;
            }

            return retorno;
        }

        public static string CadenaAleatoria()
        {
            string strValue = "";
            Random objAleatorio = new Random();
            try
            {
                for (int i = 0; i < 8; i++)
                {
                    strValue = strValue + objAleatorio.Next(0, 10).ToString();
                }
            }
            catch (Exception ex)
            {
                //log.Info(string.Format("Error: {0}", ex.Message));
                return "";
            }
            finally
            {
                objAleatorio = null;
            }
            return strValue;
        }

        public static Nullable<T> DbValueToNullable<T>(object dbValue) where T : struct
        {
            Nullable<T> returnValue = null;

            if ((dbValue != null) && (dbValue != DBNull.Value))
            {
                returnValue = (T)dbValue;
            }

            return returnValue;
        }

        public static T DbValueToDefault<T>(object obj)
        {
            if (obj == null || obj == DBNull.Value) return default(T);
            else { return (T)obj; }
        }

        static public bool IsNumeric(object value)
        {
            double numero;
            return  Double.TryParse(Convert.ToString(value), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out numero);
        }

        static public string ConvertSoles(object value)
        {
            string salida = "0";
            if (value == null || value == System.DBNull.Value)
            {
                salida = "0";
            }
            else
            {
                if (Convert.ToString(value) == "")
                    salida = "0";
                else
                    salida = Convert.ToString((Convert.ToDouble(value) / 100));
            }
            return salida;
        }
         
        static public bool IsNumeric3(string input)
        {
            bool flag = true;
            string pattern = @"^[0-9]*$";
            Regex validate = new Regex(pattern);
            if (!validate.IsMatch(input))
            {
                flag = false;
            }
            return flag;
        }
        bool IsNumeric2(string inputString)
        {
            return Regex.IsMatch(inputString, "^[0-9]+$");
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

        static public float CheckFloat(object value)
        {
            float salida = 0;
            if (value == null || value == System.DBNull.Value)
            {
                salida = 0;
            }
            else
            {
                if (Convert.ToString(value) == "")
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
                    salida = Convert.ToInt32(value);
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

        static public double CheckDbl(object value, int nroDecimales)
        {
            double salida = CheckDbl(value);
            if (salida == 0) return salida;
            return redondearMontos(salida, nroDecimales);
        }

        static public decimal CheckDecimal(object value)
        {
            decimal salida = 0;
            if (value == null || value == System.DBNull.Value)
            {
                salida = 0;
            }
            else
            {
                if (Convert.ToString(value) == "" || Convert.ToString(value) == "&nbsp;" || Convert.ToString(value) == "&nbsp")
                    salida = 0;
                else
                    salida = Convert.ToDecimal(value);
            }
            return salida;
        }

        static public double redondearMontos(double value, int nroDecimales)
        {
            return Math.Round(value, nroDecimales);
        }
        static public DateTime CheckDate(object value)
        {
            if (value == null || value == System.DBNull.Value)
                return new DateTime(1, 1, 1);

            if (value.ToString() == "")
                return new DateTime(1, 1, 1);
            return Convert.ToDateTime(value);
        }

        public static System.Data.DataTable dtParams()
        {
            System.Data.DbType tipo = new System.Data.DbType();
            System.Data.ParameterDirection direccion = new System.Data.ParameterDirection();
            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Columns.Add("Nombre", System.Type.GetType("System.String"));
            dt.Columns.Add("Tipo", tipo.GetType());
            dt.Columns.Add("Size", System.Type.GetType("System.Int32"));
            dt.Columns.Add("Direccion", direccion.GetType());
            dt.Columns.Add("Valor", System.Type.GetType("System.Object"));

            return dt;
        }

        public static bool InsertarParam(System.Data.DataTable vdtParams,
            string vName,
            System.Data.DbType vType,
            int vSize,
            System.Data.ParameterDirection vDirection,
            object vValue)
        {

            System.Data.DataRow dr = vdtParams.NewRow();
            dr["Nombre"] = vName;
            dr["Tipo"] = vType;
            if (vSize == 0)
                dr["Size"] = 0;
            else
                dr["Size"] = vSize;

            dr["Direccion"] = vDirection;

            if (vValue == null)
                dr["Valor"] = DBNull.Value;
            else
                dr["Valor"] = vValue;

            vdtParams.Rows.Add(dr);
            return true;
        }

        public static double ConvertSolesToCentimos(double vMonto)
        {
            return (vMonto * 100);
        }

        public static DataTable TablaActividad()
        {
            DataTable dt;
            dt = new DataTable();
            dt.Columns.Add("start", typeof(DateTime));
            dt.Columns.Add("end", typeof(DateTime));
            dt.Columns.Add("name", typeof(string));
            dt.Columns.Add("id", typeof(string));
            dt.Columns.Add("eventColor", typeof(string));

            return dt;
        }
        public static int UltimoDiaMes(int mes, int anno)
        {
            int dia = 0;
            if (mes == 1 || mes == 3 || mes == 5 || mes == 7 || mes == 10 || mes == 12)
            {
                dia = 31;
            }
            else if (mes == 4 || mes == 6 || mes == 8 || mes == 9 || mes == 11)
            {
                dia = 30;
            }
            else if (mes == 2)
            {
                if ((anno % 4) == 0 & (anno % 100) == 0)
                {
                    dia = 29;
                }
                else
                {
                    dia = 28;
                }
            }
            return dia;
        }
        public static string NVLString(string valor1, string valor2)
        {
            string v1 = CheckStr(valor1);
            string v2 = CheckStr(valor2);
            if (v1 != "")
                return v1;
            else
                return v2;
        }
        public static DateTime NVLDate(DateTime valor1, DateTime valor2)
        {
            DateTime v1 = CheckDate(valor1);
            DateTime v2 = CheckDate(valor2);
            if (v1 != new DateTime(1, 1, 1))
                return v1;
            else
                return v2;
        }


        public static string FormarNroDocumentoIdentidad(string nro)
        {
            string salida = nro;
            if (salida.Equals("")) return "";
            salida = nro.PadLeft(16, '0');
            return salida;
        }

        public static string ObtenerResultadoTelefono(string estado)
        {
            string Resultado;
            switch (estado)
            {
                case "1": Resultado = "SE REGISTRÓ EXITOSAMENTE"; break;
                case "0": Resultado = "NO SE REGISTRÓ EL TELEFONO "; break;
                case "N": Resultado = "EL REGISTRO EXISTE COMO: NUEVO"; break;
                case "R": Resultado = "EL REGISTRO EXISTE COMO: RESERVADO"; break;
                case "E": Resultado = "EL REGISTRO EXISTE COMO: ENVIADO POR ACTIVACION"; break;
                case "A": Resultado = "EL REGISTRO EXISTE COMO: ACTIVADO"; break;
                case "": Resultado = "EL REGISTRO EXISTE"; break;
                default: Resultado = "VERIFICAR DATOS"; break;
            }
            return Resultado;
        }
        public static string ReemplazarCaracterInvalido(string valor)
        {
            if (valor == null) return "";
            if (valor.Trim() == "") return "";
            int intPos = 0;
            string strCadenaInvalida = "ñ,Ñ,á,é,í,ó,ú,Á,É,Í,Ó,Ú,ä,ë,ï,ö,ü,Ä,Ë,Ï,Ö,Ü";
            string strCadenaValida = "n,N,a,e,i,o,u,A,E,I,O,U,a,e,i,o,u,A,E,I,O,U";
            string[] ArrInvalida = strCadenaInvalida.Split(',');
            string[] ArrValida = strCadenaValida.Split(',');
            int i = 0;
            for (i = 0; i < ArrInvalida.Length; i++)
            {
                intPos = valor.IndexOf(ArrInvalida[i]);
                if (intPos != -1)
                {
                    valor = valor.Replace(ArrInvalida[i], ArrValida[i]);
                }
            }
            return valor;
        }


        static public Int16 CheckInt16(object value)
        {
            Int16 salida = 0;
            if (value == null || value == System.DBNull.Value)
            {
                salida = 0;
            }
            else
            {
                if (Convert.ToString(value) == "")
                    salida = 0;
                else
                    salida = Convert.ToInt16(value);
            }
            return salida;
        }

        public static string validarvacio(string cadena)
        {
            string newvalor;
            if (cadena.Equals(string.Empty) || cadena.Equals("0") || cadena.Equals("-1"))
            {
                newvalor = null;
            }
            else
            {
                newvalor = cadena;
            }
            return newvalor;
        }

        public static DateTime DevuelveFormatoFecha(string cadena)
        {
            DateTime fecha;
            if (cadena.Equals(string.Empty) || cadena.Equals("0"))
            {
                fecha = new DateTime(1900, 1, 1);
            }
            else
            {
                string nuevacad = cadena.Substring(6, 2) + "/" + cadena.Substring(4, 2) + "/" + cadena.Substring(0, 4);
                fecha = Convert.ToDateTime(nuevacad);
            }
            return fecha;
        }

        public static string DevuelveFormatoFechaStr(string cadena)
        {
            String fecha;
            if (cadena.Equals(string.Empty) || cadena.Equals("0"))
            {
                fecha = "";
            }
            else
            {
                String nuevacad = cadena.Substring(0, 2) + "/" + cadena.Substring(2, 2) + "/" + cadena.Substring(4, 4);
                fecha = nuevacad;
            }

            try
            {
                DateTime dt = Convert.ToDateTime(fecha);
                if (Convert.ToInt16(cadena.Substring(4, 4)) <= 1913)
                {
                    String nuevacad = cadena.Substring(6, 2) + "/" + cadena.Substring(4, 2) + "/" + cadena.Substring(0, 4);
                    fecha = nuevacad;
                }

            }
            catch (Exception ex)
            {
                //log.Info(string.Format("Error: {0}", ex.Message));
                String nuevacad = cadena.Substring(6, 2) + "/" + cadena.Substring(4, 2) + "/" + cadena.Substring(0, 4);
                fecha = nuevacad;
            }



            return fecha;
        }

        public static string ConvertirFecha(string vFecha)
        {
            string fecha = "";
            if (vFecha == "00000000")
            {
                return "";
            }
            if (vFecha.Length >= 6)
            {
                fecha = String.Format("{0}/{1}/{2}", vFecha.Substring(6, 2), vFecha.Substring(4, 2), vFecha.Substring(0, 4));
                fecha = Convert.ToDateTime(fecha).ToShortDateString();
            }
            return fecha;

        }
        public static string ConvertirFecha2(string vFecha)
        {
            string fecha = "";
            if (vFecha == "00000000")
            {
                return "";
            }
            if (vFecha.Length >= 6)
            {
                fecha = String.Format("{0}{1}{2}", vFecha.Substring(6, 4), vFecha.Substring(3, 2), vFecha.Substring(0, 2));
                //fecha = Convert.ToDateTime(fecha).ToShortDateString();
            }
            return fecha;

        }
        static public bool IsContratoVacio(string sNroContrato)
        {
            sNroContrato = sNroContrato.Replace("0", "");
            return (sNroContrato.Trim().Equals(""));
        }

        public static string FormatoFecha(string Fecha)
        {
            if (Fecha.Length > 0)
                return Fecha.Substring(0, 4) + "/" + Fecha.Substring(5, 2) + "/" + Fecha.Substring(8, 2);
            else
                return "0000/00/00";
        }

        public static decimal FormatoDec(string valor)
        {
            decimal res = 0;
            if (valor.Trim() != "")
            {
                res = Convert.ToDecimal(valor);
            }
            return res;
        }

        public static string FormatoDecStr(string valor)
        {
            string res = "0";
            if (valor.Trim() != "")
            {
                res = valor;
            }
            return res;
        }

        public static string FormatoFechaSap(string fecha)
        {
            if (fecha.Length > 0)
                return fecha.Substring(6, 4) + "/" + fecha.Substring(3, 2) + "/" + fecha.Substring(0, 2);
            else
                return "0000/00/00";
        }

        public static string GetDateTimeAsYYYYMM(DateTime pidtValue)
        {
            string sYYYYMM, sMonth, sYear;

            sMonth = Convert.ToString(pidtValue.Month);
            sMonth = sMonth.PadLeft(2, Convert.ToChar("0"));

            sYear = Convert.ToString(pidtValue.Year);

            sYYYYMM = sYear + sMonth;

            return (sYYYYMM);
        }

        public static string FormatDateTimeAsYYYYMMDD(DateTime pidtValue)
        {
            string sYYYYMMDD, sDay, sMonth, sYear;

            sDay = Convert.ToString(pidtValue.Day);
            sDay = sDay.PadLeft(2, Convert.ToChar("0"));

            sMonth = Convert.ToString(pidtValue.Month);
            sMonth = sMonth.PadLeft(2, Convert.ToChar("0"));

            sYear = Convert.ToString(pidtValue.Year);

            sYYYYMMDD = sYear + sMonth + sDay;

            return (sYYYYMMDD);
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


        public static string GetFormatMMSS(Int64 cantidad)
        {
            string result;
            if (cantidad > 60)
            {
                Int64 intMinutos = Convert.ToInt64(CheckDbl(cantidad / 60, 2));
                Int64 intSegundos = Convert.ToInt64(cantidad % 60);
                result = String.Format("{0}:{1}", intMinutos.ToString("00"), intSegundos.ToString("00"));
            }
            else
            {
                result = String.Format("00:{0}", cantidad.ToString("00"));
            }
            return result;
        }

        public static string GetFormatHHMMSS(double cantidad, string tipo, bool isPrepago)
        {
            string result;
            if (isPrepago == true)
            {
                if (tipo.ToUpper().IndexOf("LLAMADA") != -1 || tipo.ToUpper().IndexOf("MOC") != -1)
                {
                    //Para DBPre, como vienen en minutos lo pasamos a segundos
                    cantidad = CheckDbl(cantidad, 2) * 60;
                    //Para ODSPre, dejamos como viene pues llega en segundos
                    result = GetFormatHHMMSS(CheckInt64(cantidad));
                }
                else
                {
                    result = cantidad.ToString();
                }
            }
            else
            {
                if (tipo.ToUpper().IndexOf("LLAMADA") != -1)
                {
                    result = GetFormatHHMMSS(CheckInt64(CheckDbl(cantidad, 2)));
                }
                else
                {
                    result = cantidad.ToString();
                }
            }
            return result;
        }

        public static string GetFormatMMSS(Int64 cantidad, string tipo)
        {
            string result;
            if (tipo.ToUpper().IndexOf("LLAMADA") != -1)
            {
                if (cantidad > 60)
                {
                    Int64 intMinutos = Convert.ToInt64(CheckDbl(cantidad / 60, 2));
                    Int64 intSegundos = Convert.ToInt64(cantidad % 60);
                    result = String.Format("{0}:{1}", intMinutos.ToString("00"), intSegundos.ToString("00"));
                }
                else
                {
                    result = String.Format("00:{0}", cantidad.ToString("00"));
                }
            }
            else
            {
                result = cantidad.ToString();
            }
            return result;
        }

        public static string GetFormatHHMMSS(Int64 cantidad)
        {
            string result;
            Int64 intHoras = 0;
            Int64 intSegundos = 0;
            if (cantidad >= 3600)
            {
                intHoras = Convert.ToInt64(CheckDbl(cantidad / 3600, 2));
                intSegundos = Convert.ToInt64(cantidad % 3600);
                result = GetFormatMMSS(CheckInt64(intSegundos));
            }
            else
            {
                result = GetFormatMMSS(CheckInt64(cantidad));
            }
            result = intHoras.ToString("00") + ":" + result;
            return result;
        }

        public static string SeguridadFormatTelf(string pTelefono)
        {
            string sTelefono;
            string sNumeroFormat = ConfigurationManager.AppSettings["gConstNroDigSeguridadTelefono"];
            int iNumero = System.Int32.Parse(sNumeroFormat);

            if (pTelefono.Length <= iNumero)
            {
                sTelefono = "XXXXXXXXXXX".Substring(0, pTelefono.Length);
            }
            else
            {
                sTelefono = pTelefono.Substring(0, pTelefono.Length - iNumero);
                sTelefono = sTelefono + "XXXXXXXXXXX".Substring(0, iNumero);
            }


            return (sTelefono);
        }


        public static string GetFormatHHMMSS24AsHHMMSSAMPM(string isHora)
        {
            string sRetorno = "";
            string[] arrDatos = isHora.Split(Convert.ToChar(":"));
            if (arrDatos.Length > 1)
            {
                if (arrDatos.Length > 2)
                {
                    Int64 iHoras = CheckInt64(CheckDbl(arrDatos[0], 2));
                    Int64 iMinutos = CheckInt64(CheckDbl(arrDatos[1], 2));
                    Int64 iSegundos = CheckInt64(CheckDbl(arrDatos[2], 2));
                    if (iHoras >= 12)
                    {
                        iHoras = iHoras - 12;
                        sRetorno = String.Format("{0}:{1}:{2} p.m.", iHoras.ToString("00"), iMinutos.ToString("00"), iSegundos.ToString("00"));
                    }
                    else
                    {
                        sRetorno = String.Format("{0}:{1}:{2} a.m.", iHoras.ToString("00"), iMinutos.ToString("00"), iSegundos.ToString("00"));
                    }
                }
                else
                {
                    sRetorno = GetFormatMMSSAsHHMMSS(isHora);
                }
            }
            else
            {
                sRetorno = isHora;
            }
            return sRetorno;
        }

        public static string GetFormatMMSSAsHHMMSS(string isHora)
        {
            string sRetorno = "";
            string[] arrDatos = isHora.Split(Convert.ToChar(":"));
            if (arrDatos.Length > 1)
            {
                if (arrDatos.Length > 2)
                {
                    sRetorno = isHora;
                }
                else
                {
                    Int64 iMinutos = CheckInt64(CheckDbl(arrDatos[0], 2));
                    Int64 iSegundos = CheckInt64(CheckDbl(arrDatos[1], 2));
                    if (iMinutos >= 60)
                    {
                        Int64 iHoras = Convert.ToInt64(CheckDbl(iMinutos / 60, 2));
                        iMinutos = Convert.ToInt64(iMinutos % 60);
                        sRetorno = String.Format("{0}:{1}:{2}", iHoras.ToString("00"), iMinutos.ToString("00"), iSegundos.ToString("00"));
                    }
                    else
                    {
                        sRetorno = "00:" + String.Format("{0}:{1}", iMinutos.ToString("00"), iSegundos.ToString("00"));
                    }
                }
            }
            else
            {
                sRetorno = isHora;
            }
            return sRetorno;
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

        // Pasa un DateTime de C# a DD/MM/YYYY
        // no valen DateTime nulos (simplemente porque no hay DateTime nulos)
        public static string GetDateTimeAsDDMMYYYY(DateTime pidtValue)
        {
            string sDDMMYYYY, sDay, sMonth, sYear;

            sDay = Convert.ToString(pidtValue.Day);
            sDay = sDay.PadLeft(2, Convert.ToChar("0"));

            sMonth = Convert.ToString(pidtValue.Month);
            sMonth = sMonth.PadLeft(2, Convert.ToChar("0"));

            sYear = Convert.ToString(pidtValue.Year);

            sDDMMYYYY = sDay + "/" +
                        sMonth + "/" +
                        sYear;

            return (sDDMMYYYY);
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
            else if (pExtArchivo == ".xls")
            {
                salida = ".pdf";
            }
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

        public static string GetExceptionMessage(Exception ex)
        {
            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
            }

            var strExcepMsg = ex.Message;

            return strExcepMsg;
            }

        public static string GetConstancyUrlFromServerToApp(string urlConstancyFromServer, string serverReadPdf, string folderTransaction)
            {
            var urlConstancy = string.Format("{0}{1}{2}", serverReadPdf, folderTransaction, Path.GetFileName(urlConstancyFromServer));
            return urlConstancy;
        }
    }
}
