using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KEY = System.Configuration.ConfigurationManager;


namespace Claro.SIACU.Transac.Service
{
    public static class TemplateExcel
    {        
        public static string CONST_EXPORT_HFCINCOMINGCALL = System.Configuration.ConfigurationManager.AppSettings["CONST_EXPORT_HFCINCOMINGCALL"].ToString();
        public static string CONST_EXPORT_LTEINCOMINGCALL = System.Configuration.ConfigurationManager.AppSettings["CONST_EXPORT_LTEINCOMINGCALL"].ToString();

       
        public static string CONST_EXPORT_POSTPAID_OUTGOINGCALLSNB =   KEY.AppSettings["CONST_EXPORT_POSTPAID_OUTGOINGCALLSNB"];
        public static string CONST_EXPORT_PREPAID_INCOMINGCALLDETAIL = KEY.AppSettings["CONST_EXPORT_PREPAID_INCOMINGCALLDETAIL"];
        public static string CONST_EXPORT_POSTPAID_BILLEDOUTCALLDETAIL = KEY.AppSettings["CONST_EXPORT_CALLDETAILS_POST"];
       
        public static string CONST_EXPORT_POSTPAIDINCOMINGCALL = KEY.AppSettings["CONST_EXPORT_POSTPAIDINCOMINGCALL"];
       
    }
}
