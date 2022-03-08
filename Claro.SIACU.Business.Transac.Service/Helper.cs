using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Claro.SIACU.Entity;

namespace Claro.SIACU.Business.Transac.Service
{
    public static class Helper
    {
        public static string GetNumberReceipt(string strIdSession, string strTransactionID, string strInvoiceNumber, string strFechaEmision)
        {
            string strNroReciboTemp = "";
            int intLongitud = Claro.Constants.NumberZero;
            string strParteNro = "";
            string sFechaEmisionCorta = "";
            string sFechaEmision = strFechaEmision.Trim();
            string strFechaCorte = Claro.SIACU.Constants.Receipt8;
            string strFechaCorte1 = Claro.SIACU.Constants.Receipt9;
            string lSalida = "";
            string lSalida7 = "";
            string lSalida8 = "";
            string lSalida9 = "";

            try
            {
                sFechaEmisionCorta = (sFechaEmision.Length > 8 ? sFechaEmision.Substring(6, 4) + sFechaEmision.Substring(3, 2) + sFechaEmision.Substring(0, 2) : sFechaEmision);
                strNroReciboTemp = strInvoiceNumber.Substring(0, strInvoiceNumber.Length - 6);
                intLongitud = strNroReciboTemp.Length;

                if (intLongitud >= 7)
                {
                    lSalida7 = Claro.SIACU.Constants.T001_ + strNroReciboTemp.Substring(intLongitud - 7);
                    lSalida8 = Claro.SIACU.Constants.T001_ + strNroReciboTemp.Substring(intLongitud - 8);
                    lSalida9 = Claro.SIACU.Constants.T001_ + strNroReciboTemp.Substring(0, 10);
                }
                else
                {
                    for (int i = 1; i <= 7 - intLongitud; i++) strParteNro = Claro.Constants.NumberZeroString + strParteNro;
                    lSalida7 = Claro.SIACU.Constants.T001_ + strParteNro + strNroReciboTemp;
                    for (int i = 1; i <= 8 - intLongitud; i++) strParteNro = Claro.Constants.NumberZeroString + strParteNro;
                    lSalida8 = Claro.SIACU.Constants.T001_ + strParteNro + strNroReciboTemp;
                }

                var comparacionFechas = string.Compare(sFechaEmisionCorta, strFechaCorte, StringComparison.InvariantCulture);

                if (comparacionFechas < 0)
                {
                    lSalida = lSalida7;
                }
                else
                {
                    comparacionFechas = string.Compare(sFechaEmisionCorta, strFechaCorte1, StringComparison.InvariantCulture);
                    lSalida = (comparacionFechas < 0 ? lSalida8 : lSalida9);
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransactionID, ex.Message);
                throw ex;
            }

            return lSalida;
        }
    }
}
