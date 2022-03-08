using Claro.SIACU.Entity.Transac.Service.Common;
using Claro.SIACU.ProxyService.Transac.Service.SIACPre.BondTFI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSC = Claro.SIACU.Data.Transac.Service.Configuration.WebServiceConfiguration;

namespace Claro.SIACU.Data.Transac.Service.Prepaid
{
    public class BolsaTFIBono5
    {
        
        
        public static List<Account> GetBono5TFI_Prepago(string idTransaction
                                           , string ipApplication
                                           , string nameApplication
                                           , string numberMsisdn
                                           , string numberIn)
        {
            List<Account> list = new List<Account>();
            Account nodo;
            int timeOut = 0;
  
            consultarLineaINRequest request = new consultarLineaINRequest();
            consultarLineaINResponse response = new consultarLineaINResponse();
            ebsOperacionesINService service = new ebsOperacionesINService();
            service.Url = WebSC.PrepaidService.Url;
            service.Credentials = System.Net.CredentialCache.DefaultCredentials;

            if (int.TryParse(Claro.ConfigurationManager.AppSettings("intTimeOutWSWebUrlWSBono5Prepago"), out timeOut))
            {
                service.Timeout = timeOut;
                Claro.Web.Logging.Info("s", idTransaction, string.Format("El TimeOut configurado en el key intTimeOutWSWebUrlWSBono5Prepago es: {0}", timeOut));
            }
            else
            {
                Claro.Web.Logging.Error("s", idTransaction, "No se ha encontrado el key intTimeOutWSWebUrlWSBono5Prepago en el WebConfig del BackEnd o éste tiene un valor incorrecto");
            }
             
            ParametrosObjectType[] ParaList = new ParametrosObjectType[4];

            request.idTransaccion = idTransaction;
            request.ipAplicacion = ipApplication;
            request.nombreAplicacion = nameApplication;
            request.msisdn = numberMsisdn;
            request.@in = numberIn;

            Claro.Web.Logging.Info("S", idTransaction, string.Format("Ejecucion del WS url:{0} consumiendo el procedimiento ejecutarOperacionIN ", service.Url));
            Claro.Web.Logging.Info("S", idTransaction, string.Format("Parametros de Entrada : idTransaccion :{0}, ipAplicacion :{1}, nombreAplicacion :{2}, msisdn:{3}, @in :{4}", idTransaction, ipApplication, nameApplication, numberMsisdn, numberIn));

            string bag51Ba = Claro.ConfigurationManager.AppSettings("strbolsa51Ba");
            string bag51Ex = Claro.ConfigurationManager.AppSettings("strbolsa51Ex");
            string bag53Ba = Claro.ConfigurationManager.AppSettings("strbolsa53Ba");
            string bag53Ex = Claro.ConfigurationManager.AppSettings("strbolsa53Ex");

            request.listaParametrosRequest = new String[]{
                bag51Ba,bag51Ex,bag53Ba,bag53Ex};

            Claro.Web.Logging.Info("S", idTransaction, string.Format("Parametros de entrada ::: adicionales: parametro0 :{0} , parametro1 :{1}, parametro2 :{2}, parametro3 :{3}:",
                bag51Ba, bag51Ex, bag53Ba, bag53Ex));
            Claro.Web.Logging.Info("S", idTransaction, "Consumiento procedimento WS consultarLineaIN");
            response = service.consultarLineaIN(request);

            Int16 sw = 0;
            string Saldo = "";
            string FechaExpiracion = "";
            string Nombre = "";

            if (response != null)
            {
                if (response.codigoRespuesta == "0")
                {
                    Claro.Web.Logging.Info("S", idTransaction, "Parametro salida ::: Consulta exitosa devolucion de informacion");
                    foreach (ParametrosObjectType item in response.listaParametrosResponse)
                    {
                        if (item.parametro == bag51Ex || item.parametro == bag51Ba)
                        {
                            Saldo = (sw == 0 ? item.valor : Saldo);
                            FechaExpiracion = (sw == 1 ? item.valor : FechaExpiracion);
                            Nombre = Claro.ConfigurationManager.AppSettings("strBolCMACC1");
                        }
                        if (item.parametro == bag53Ba || item.parametro == bag53Ex)
                        {
                            Saldo = (sw == 0 ? item.valor : Saldo);
                            FechaExpiracion = (sw == 1 ? item.valor : FechaExpiracion);
                            Nombre = Claro.ConfigurationManager.AppSettings("strBolCMACC3");
                        }
                        sw += 1;
                        if (sw == 2)
                        {
                            sw = 0;
                            nodo = new Account();
                            nodo.Balance = Saldo;
                            nodo.ExpirationDate = FechaExpiracion;
                            nodo.Name = Nombre;
                            list.Add(nodo);
                            Saldo = "";
                            FechaExpiracion = "";
                            Nombre = "";
                        }
                    }

                }
                else
                {
                    Claro.Web.Logging.Error("S", idTransaction, string.Format("Parametros salida ::: Error :{0}", response.mensajeRespuesta));
                    return list;
                } 
            }

            if (list.Count > 0)
                Claro.Web.Logging.Info("S", idTransaction, string.Format("Parametros de salida ::: Numero de elementos devuelvos: {0}", list.Count));
            else
                Claro.Web.Logging.Error("S", idTransaction, string.Format("Parametros de salida ::: Numero de elementos devuelvos: {0}", 0));

            return list;
        }

     
    }
}
