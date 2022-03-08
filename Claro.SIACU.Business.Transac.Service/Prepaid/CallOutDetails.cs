using Claro.SIACU.Entity.Transac.Service.Prepaid;
using Claro.SIACU.Entity.Transac.Service.Prepaid.GetCall;
using System.Collections.Generic;
using ENTITY = Claro.SIACU.Entity.Transac.Service.Prepaid;
using DATA = Claro.SIACU.Data.Transac.Service.Prepaid;

namespace Claro.SIACU.Business.Transac.Service.Prepaid
{
    /// <summary>
    /// 
    /// </summary>
    public class CallOutDetails
    {
        public static ENTITY.GetTipifCallOutPrep.TipifCallOutPrepResponse GetTipifCallOutPrep(ENTITY.GetTipifCallOutPrep.TipifCallOutPrepRequest objRequest)
        {
            ENTITY.GetTipifCallOutPrep.TipifCallOutPrepResponse objResponse = new ENTITY.GetTipifCallOutPrep.TipifCallOutPrepResponse();

            objResponse = Claro.Web.Logging.ExecuteMethod<ENTITY.GetTipifCallOutPrep.TipifCallOutPrepResponse>(
                objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Prepaid.CallOutDetails.GetTipifCallOutPrep(objRequest.Audit.Session,
                                            objRequest.Audit.Transaction,
                                            objRequest.vTransaccion
                                            );
                });
            return objResponse;
        }

        /// <summary>
        /// Método para obtener el listado de detalles de llamadas salientes
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns>Objeto de respuesta del servicio</returns>
 
        public static CallResponse GetCallOutDetailsLoad(CallRequest objRequest)
        {
            CallResponse objResponse = new CallResponse()
            {
                lstCall = Claro.Web.Logging.ExecuteMethod<List<Call>>(
                    objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                    {
                        return Data.Transac.Service.Prepaid.CallOutDetails.GetCallDetails(
                                                objRequest.Audit.Session,
                                                objRequest.Audit.Transaction,
                                                objRequest.linea,
                                                objRequest.Audit.Transaction,
                                                objRequest.Audit.IPAddress,
                                                objRequest.Audit.ApplicationName,
                                                objRequest.Audit.UserName,
                                                objRequest.strfechaInicio,
                                                objRequest.strfechaFin,
                                                objRequest.strTipoConsulta,
                                                objRequest.tp);
                    })
            };

            return objResponse;

        }

    }
}
