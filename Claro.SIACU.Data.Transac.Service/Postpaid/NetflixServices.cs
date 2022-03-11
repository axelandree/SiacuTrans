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
using Claro.SIACU.Entity.Transac.Service.Postpaid.GetNetflixServices;
using Claro.SIACU.Entity.Transac.Service.Postpaid.Legacy.GetTypeProductDat;
using Claro.SIACU.Entity.Transac.Service.Postpaid.Legacy.GetAdditionalServices;

namespace Claro.SIACU.Data.Transac.Service.Postpaid
{
    public class NetflixServices
    {
        /// <summary>
        /// Permite obtener los servicios usados por contrato tecnologia móvil.
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strIdTransaccion"></param>
        /// <param name="intCoId"></param>
        /// <returns></returns>
        public static string validarAccesoRegistroLinkMovil(string strIdSession, string strIdTransaccion, int intCoId, int intSnCode)
        {
            Claro.Web.Logging.Info(strIdSession, strIdTransaccion, "validarAccesoRegistroLinkMovil - Inicio");
            string strEstado = string.Empty;

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("V_ESTADO", DbType.String,1, ParameterDirection.ReturnValue,strEstado),
                new DbParameter("P_CO_ID", DbType.Int64, ParameterDirection.Input, intCoId),
                new DbParameter("P_SNCODE", DbType.Int64, ParameterDirection.Input, intSnCode),
            };
            Claro.Web.Logging.Info(strIdSession, strIdTransaccion, string.Format("validarAccesoRegistroLinkMovil - {0}-{1}", intCoId, intSnCode));
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
            Claro.Web.Logging.Info(strIdSession, strIdTransaccion, "validarAccesoRegistroLinkMovil - Fin");
            return strEstado;
        }
        /// <summary>
        /// Permite obtener el Contrato de la línea TOBE.
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strIdTransaccion"></param>
        /// <param name="oTypeProductDatRequest"></param>
        /// <returns></returns>
        public static string obtenerContratoMovilOne(string strIdSession, string strIdTransaccion, TypeProductDatRequest oTypeProductDatRequest)
        {
            Claro.Web.Logging.Info(strIdSession, strIdTransaccion, "obtenerContratoMovilOne - Inicio");
            TypeProductDatResponse oTypeProductDatResponse = null;
            string strIdPublicoContrato = string.Empty;
            try
            {
                Claro.Web.Logging.Info(strIdSession, strIdTransaccion, "Request :" + JsonConvert.SerializeObject(oTypeProductDatRequest, Formatting.Indented));
                oTypeProductDatResponse = RestService.PostInvoque<TypeProductDatResponse>(RestServiceConfiguration.OBTENER_TIPO_PRODUCTO_DAT_TOBE, oTypeProductDatRequest.Audit, oTypeProductDatRequest, false);
                Claro.Web.Logging.Info(strIdSession, strIdTransaccion, "Response :" + JsonConvert.SerializeObject(oTypeProductDatResponse, Formatting.Indented));
                if (oTypeProductDatResponse != null)
                {
                    if ((oTypeProductDatResponse.responseStatus != null) && 
                        (oTypeProductDatResponse.responseStatus.codigoRespuesta == Constants.NumberZeroString))
                    {
                        if (oTypeProductDatResponse.responseData != null) {
                            if (oTypeProductDatResponse.responseData.contrato != null)
                            {
                                if (oTypeProductDatResponse.responseData.contrato.Count > 0)
                                {
                                    strIdPublicoContrato = oTypeProductDatResponse.responseData.contrato.ElementAt(0).idPublicoContrato;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
               Claro.Web.Logging.Error(strIdSession, strIdTransaccion, Claro.MessageException.GetOriginalExceptionMessage(ex));
            }
            Claro.Web.Logging.Info(strIdSession, strIdTransaccion, "obtenerContratoMovilOne - Fin");
            return strIdPublicoContrato;
        }
        /// <summary>
        /// Permite obtener los servicios usados por contrato tecnologia móvil ONE.
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strIdTransaccion"></param>
        /// <param name="strServicio"></param>
        /// <param name="oAdditionalServiceRequest"></param>
        /// <returns></returns>
        public static bool validarAccesoRegistroLinkMovilOne(string strIdSession, string strIdTransaccion, string strServicio, AdditionalServiceRequest oAdditionalServiceRequest)
        {
            Claro.Web.Logging.Info(strIdSession, strIdTransaccion, "validarAccesoRegistroLinkMovilOne - Inicio");
            AdditionalServiceResponse oAdditionalServiceResponse = null;
            string strCodigoServicio = string.Empty;
            string strEstadoServicio = string.Empty;
            bool bValidarServicio = false;
            try
            {
                Claro.Web.Logging.Info(strIdSession, strIdTransaccion, "Request :" + JsonConvert.SerializeObject(oAdditionalServiceRequest, Formatting.Indented));
                oAdditionalServiceResponse = RestService.PostInvoque<AdditionalServiceResponse>(RestServiceConfiguration.OBTENER_LISTA_SERVICIOS_CONTRATO, oAdditionalServiceRequest.Audit, oAdditionalServiceRequest, false);
                Claro.Web.Logging.Info(strIdSession, strIdTransaccion, "Response :" + JsonConvert.SerializeObject(oAdditionalServiceResponse, Formatting.Indented));
                if (oAdditionalServiceResponse != null)
                {
                    if (oAdditionalServiceResponse.obtenerServiciosPlanPorContratoResponse != null)
                    {
                        if ((oAdditionalServiceResponse.obtenerServiciosPlanPorContratoResponse.responseAudit != null) &&
                            ((oAdditionalServiceResponse.obtenerServiciosPlanPorContratoResponse.responseAudit.codigoRespuesta == Constants.NumberZeroString)))
                        {
                            if (oAdditionalServiceResponse.obtenerServiciosPlanPorContratoResponse.responseData != null)
                            {
                                if (oAdditionalServiceResponse.obtenerServiciosPlanPorContratoResponse.responseData.serviciosAsociados.Count > 0)
                                {
                                    strCodigoServicio = oAdditionalServiceResponse.obtenerServiciosPlanPorContratoResponse.responseData.serviciosAsociados.ElementAt(0).codigoServicio;
                                    strEstadoServicio = oAdditionalServiceResponse.obtenerServiciosPlanPorContratoResponse.responseData.serviciosAsociados.ElementAt(0).estado;
                                    if ((strCodigoServicio.Equals(strServicio)) && (strEstadoServicio.Equals(Constants.LetterA)))
                                    {
                                        bValidarServicio = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strIdTransaccion, Claro.MessageException.GetOriginalExceptionMessage(ex));
            }
            Claro.Web.Logging.Info(strIdSession, strIdTransaccion, "validarAccesoRegistroLinkMovilOne - Fin");
            return bValidarServicio;
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
                
                oResponse = RestService.PostInvoqueDP<ServicesNXResponse>("strServicesNetflixRESTDPEnvioNotif", oAudit, oRequest, CDP.getCredentials(oAudit, "strUserAjustesDP", "strPassAjustesDP"), "enviarurlactivacion", true, "");

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
