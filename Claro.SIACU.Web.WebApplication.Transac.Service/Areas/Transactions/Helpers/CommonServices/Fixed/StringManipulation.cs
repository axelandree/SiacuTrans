using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.CommonServices.Fixed
{
    public class StringManipulation
    {
        public static string RemoveAccentsAndUpper(string accentedStr){
            byte[] tempBytes;
            tempBytes = System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(accentedStr);
            string asciiStr = System.Text.Encoding.UTF8.GetString(tempBytes);
            asciiStr = asciiStr.ToUpper();

            return asciiStr;
    }
    }
}