using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Claro.SIACU.Data.Transac.Service.Configuration;
using Claro.SIACU.Entity.Transac.Service.Fixed.GetNetflixServices;

namespace Claro.SIACU.Business.Transac.Service.Fixed
{
    public class NetflixServices
    {
        /// <summary>
        /// Permite obtener los servicios usados por contrato tecnologia HFC.
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strIdTransaccion"></param>
        /// <param name="intContrato"></param>
        /// <param name="strCodigoServicio"></param>
        /// <returns></returns>
        public static bool validarAccesoRegistroLinkHFC(string strIdSession, string strIdTransaccion, int intContrato, string strCodigoServicio)
        {
            bool booEnviarLink = false;
            string[] arrSnCode = strCodigoServicio.Split(',');
            string strEstado = string.Empty;
            
            foreach (var objItem in arrSnCode)
            {
                strEstado = Claro.Web.Logging.ExecuteMethod<string>(strIdSession, strIdTransaccion,
               () =>
               {
                   return Data.Transac.Service.Fixed.NetflixServices.validarAccesoRegistroLinkHFC(strIdSession, strIdTransaccion, intContrato, Convert.ToInt(objItem));
               });
                if (strEstado.Equals("A"))
                {
                    booEnviarLink = true;
                    break;
                }
            }
            return booEnviarLink;
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
            ServicesNXResponse oResponse = null;
            oResponse = Claro.Web.Logging.ExecuteMethod(strIdSession, strIdTransaccion, () =>
            {
                return Claro.SIACU.Data.Transac.Service.Fixed.NetflixServices.envioNotificacionRegistroNX(strIdSession, strIdTransaccion, oRequest, oAudit);
            });
            return oResponse;
        }
    }
}