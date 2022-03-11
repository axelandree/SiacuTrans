using System;
using Claro.Web;

namespace Claro.SIACU.Data.Transac.Service
{
    public class GenericDataMethods
    {
        public static void LogException(string strIdSession, string strTransaction, string text, Exception error)
        {
            Exception realerror = error;
            while (realerror.InnerException != null)
                realerror = realerror.InnerException;
            Logging.Error(strIdSession, strTransaction, text + realerror.Message);
        }  
    }
}
