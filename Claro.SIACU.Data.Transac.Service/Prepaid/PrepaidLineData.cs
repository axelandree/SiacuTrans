using Claro.SIACU.Data.Transac.Service.Configuration;
using Claro.SIACU.ProxyService.Transac.Service.SIACPre.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using PREPAID_SERVICE = Claro.SIACU.ProxyService.Transac.Service.SIACPre.Service;

namespace Claro.SIACU.Data.Transac.Service.Prepaid
{
    public class PrepaidLineData
    {
        public static String GetCode(){
            string codigo = "";
            string mensaje = "";
            
            int intTimeOut = (int)Claro.SIACU.Data.Transac.Service.Common.GetParameterData(Claro.ConfigurationManager.AppSettings("gParamWebServiceDatosPregagoTimeOut"), ref mensaje).Value_N;
            string flagNDP = Claro.ConfigurationManager.AppSettings("FlagNuevoDatosPrepago");

            INDatosPrepagoRequest OBJ = new INDatosPrepagoRequest();
           
            if (flagNDP.Equals("2")) {
                EbsDatosPrepagoService preService = new EbsDatosPrepagoService();
                preService.Url = WebServiceConfiguration.PREPAID_SERVICE.Url;
                preService.Credentials = System.Net.CredentialCache.DefaultCredentials;
                preService.Timeout = intTimeOut;

            }

            return codigo;
        }
    }
}
