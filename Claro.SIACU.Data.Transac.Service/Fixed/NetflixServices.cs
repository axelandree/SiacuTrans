using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Claro.Data;
using CDP = Claro.SIACU.Data.Transac.Service.Common;
using Claro.SIACU.Data.Transac.Service.Configuration;
using Claro.SIACU.Entity.Transac.Service.Fixed.GetNetflixServices;

namespace Claro.SIACU.Data.Transac.Service.Fixed
{
    public class NetflixServices
    {
        /// <summary>
        /// Permite obtener los servicios usados por contrato tecnologia HFC.
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strIdTransaccion"></param>
        /// <param name="intContrato"></param>
        /// <returns></returns>
        public static string validarAccesoRegistroLinkHFC(string strIdSession, string strIdTransaccion, int intContrato, int intSnCode)
        {
            Claro.Web.Logging.Info(strIdSession, strIdTransaccion, "validarAccesoRegistroLinkHFC - Inicio");
            string strEstado = string.Empty;

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("V_ESTADO", DbType.String,1, ParameterDirection.ReturnValue,strEstado),
                new DbParameter("P_CO_ID", DbType.Int64, ParameterDirection.Input, intContrato),
                new DbParameter("P_SNCODE", DbType.Int64, ParameterDirection.Input, intSnCode),
            };
            Claro.Web.Logging.Info(strIdSession, strIdTransaccion, string.Format("validarAccesoRegistroLinkHFC - {0}-{1}", intContrato, intSnCode));
            try
            {
                DbFactory.ExecuteNonQuery(strIdSession, strIdTransaccion, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_TFUN015_ESTADO_SERVICIO, parameters);
                if (parameters[0].Value.ToString() != "")
                {
                    strEstado = parameters[0].Value.ToString();
                }
                Claro.Web.Logging.Info(strIdSession, strIdTransaccion, string.Format("validarAccesoRegistroLinkMovil - {0}", strEstado));
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strIdTransaccion, Claro.MessageException.GetOriginalExceptionMessage(ex));
            }
            Claro.Web.Logging.Info(strIdSession, strIdTransaccion, "validarAccesoRegistroLinkHFC - Fin");
            return strEstado;
        }
        /// <summary>
        /// Permite realizar el envio de link para la suscripción al servicio Netflix.
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strIdTransaccion"></param>
        /// <param name="oRequest"></param>
        /// <returns></returns>
        public static ServicesNXResponse envioNotificacionRegistroNX(string strIdSession, string strIdTransaccion, ServicesNXRequest oRequest, Claro.Entity.AuditRequest oAudit)
        {
            Claro.Web.Logging.Info(strIdSession, strIdTransaccion, "envioNotificacionRegistroNX - Inicio");
            ServicesNXResponse oResponse = null;
            try
            {
                oResponse = new ServicesNXResponse();

                Claro.Web.Logging.Info(strIdSession, strIdTransaccion, "Request envioNotificacionRegistroNX: " + JsonConvert.SerializeObject(oRequest, Formatting.Indented));

                oResponse = RestService.PostInvoqueDP<ServicesNXResponse>("strServicesNetflixRESTDPEnvioNotif", oAudit, oRequest, CDP.getCredentials(oAudit, "strUserAjustesDP", "strPassAjustesDP"), "enviarurlactivacion", false, "");
                
                Claro.Web.Logging.Info(strIdSession, strIdTransaccion, "Response envioNotificacionRegistroNX: " + JsonConvert.SerializeObject(oResponse, Formatting.Indented));
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strIdTransaccion, Claro.MessageException.GetOriginalExceptionMessage(ex));
            }
            Claro.Web.Logging.Info(strIdSession, strIdTransaccion, "envioNotificacionRegistroNX - Fin");
            return oResponse;
        }
    }
}