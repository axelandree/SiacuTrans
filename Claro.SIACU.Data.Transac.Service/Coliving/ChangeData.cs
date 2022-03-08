using System;
using Claro.Data;
using COLIVING = Claro.SIACU.Entity.Transac.Service.Coliving;
using CONFIGURATION = Claro.SIACU.Data.Transac.Service.Configuration;
using Claro.SIACU.Entity.Transac.Service.Coliving.GetUpdateDataClient;
using Newtonsoft.Json;
using Claro.SIACU.Entity.Transac.Service.Coliving.PutBillingAddress;
using Claro.SIACU.Entity.Transac.Service.Coliving.PostHistoryClient;
using System.Collections;
using Claro.SIACU.Entity.Transac.Service.Coliving;
using Claro.SIACU.Entity.Transac.Service.Coliving.PutDataClient;
using System.Collections.Generic;
using System.Linq;
using Claro.SIACU.Entity.Transac.Service.Postpaid.GetDataCustomerCBIO.Request;
using Claro.SIACU.Entity.Transac.Service.Postpaid.GetDataCustomerCBIO.Response;

namespace Claro.SIACU.Data.Transac.Service.Coliving
{
    public class ChangeData
    {

        /// <summary>
        /// Actualizacion de datos del cliente lineas migradas
        /// </summary>
        /// <param name="objRequest"></param>
        /// <param name="strIdSession"></param>
        /// <returns></returns>
        public static COLIVING.GetUpdateDataClient.GetUpdateDataClientResponse GetUpdateDataClient(DataClientRequest objRequest, string strIdSession, string strIdTransaccion)
        {
            Claro.Web.Logging.Info(objRequest.IdTransaccion, objRequest.IdTransaccion, "GetUpdateDataClient Inicio");
            GetUpdateDataClientResponse response = new GetUpdateDataClientResponse();
            GetUpdateClientResponse oresponse = null;
            try
            {

                Hashtable paramHeader = new Hashtable();
                paramHeader.Add("idTransaccion", objRequest.IdTransaccion);
                paramHeader.Add("msgid", objRequest.MsgId);
                paramHeader.Add("timestamp", objRequest.TimesTamp);
                paramHeader.Add("userId", objRequest.UserId);
                paramHeader.Add("channel", objRequest.Channel);
                paramHeader.Add("idApplication", objRequest.IpAplicacion);

               
                //Claro.Web.Logging.Info(strIdSession, "IdTransaccion", String.Format("[GetUpdateDataClient] Parametros Entrada: tipoDocumento:{0}, numeroDocumento:{1}, casoEspecial:{2}", objRequest.ValidateCollaboratorMessageRequest.ValidateCollaboratorBodyRequest.ValidateCollaboratorRequest.KindDocument, objRequest.ValidateCollaboratorMessageRequest.ValidateCollaboratorBodyRequest.ValidateCollaboratorRequest.NumberDocument, objRequest.ValidateCollaboratorMessageRequest.ValidateCollaboratorBodyRequest.ValidateCollaboratorRequest.CaseSpecial));

                Claro.Web.Logging.Info(objRequest.IdTransaccion, objRequest.IdTransaccion, "GetUpdateDataClient Invocación servicio :" + CONFIGURATION.RestServiceConfiguration.ACTUALIZAR_CLIENTE_CBIO);
                oresponse = RestService.PostInvoqueSDP<COLIVING.GetUpdateDataClient.GetUpdateClientResponse>(CONFIGURATION.RestServiceConfiguration.ACTUALIZAR_CLIENTE_CBIO, paramHeader, objRequest.GetDataClientRequest.GetUpdateClientRequest);

                Claro.Web.Logging.Info(objRequest.IdTransaccion, objRequest.IdTransaccion, "GetUpdateDataClient Fin Invocación servicio :" + CONFIGURATION.RestServiceConfiguration.ACTUALIZAR_CLIENTE_CBIO);
                //Claro.Web.Logging.Info(strIdSession, "IdTransaccion", String.Format("[GetUpdateDataClient] Parametros Salida: IdTransaccion:{0}, CodigoRespuesta:{1}, MensajeRespuesta:{2}", response.GetUpdateClientResponse.AuditResponse.IdTransaction, response.GetUpdateClientResponse.AuditResponse.CodeResponse, response.GetUpdateClientResponse.AuditResponse.MessageResponse));
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.IdTransaccion, objRequest.IdTransaccion, Claro.MessageException.GetOriginalExceptionMessage(ex));
                string sep = " - ";
                int posResponse = ex.Message.IndexOf(sep);
                string result = ex.Message.Substring(posResponse + sep.Length);
                oresponse = JsonConvert.DeserializeObject<GetUpdateClientResponse>(result);
            }
            response.GetUpdateClientResponse = oresponse;
            return response;
        }

        /// <summary>
        /// Actualiza los datos de facturación para lineas migradas
        /// </summary>
        /// <param name="objRequest"></param>
        /// <param name="strIdSession"></param>
        /// <returns></returns> 

        public static BillingAddressResponse UpdateDataBillingResponse(DataClientRequest objRequest, string strIdSession)
        {
            BillingAddressResponse response = null;
            try
            {

                Hashtable paramUrl = new Hashtable();
                paramUrl.Add("idTransaccion", objRequest.IdTransaccion);
                paramUrl.Add("msgid", objRequest.MsgId);
                paramUrl.Add("timestamp", objRequest.TimesTamp);
                paramUrl.Add("userId", objRequest.UserId);
                paramUrl.Add("channel", objRequest.Channel);
                paramUrl.Add("idApplication", objRequest.IpAplicacion);

                response = RestService.PostInvoqueSDP<BillingAddressResponse>(CONFIGURATION.RestServiceConfiguration.ACTUALIZAR_DIRECCION_FACTURACION_TOBE, paramUrl, objRequest.BillingAddressRequest.BillingAddress.BillingAddressRequest);

                Claro.Web.Logging.Info(objRequest.IdTransaccion, objRequest.IdTransaccion, String.Format("[UpdateDataBillingResponse] Parametros Salida: IdTransaccion:{0}, CodigoRespuesta:{1}, MensajeRespuesta:{2}", response.responseAudit.idTransaccion, response.responseAudit.codigoRespuesta, response.responseAudit.mensajeRespuesta));
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.IdTransaccion, objRequest.IdTransaccion, Claro.MessageException.GetOriginalExceptionMessage(ex));
                string sep = " - ";
                int posResponse = ex.Message.IndexOf(sep);
                string result = ex.Message.Substring(posResponse + sep.Length);
                response = JsonConvert.DeserializeObject<BillingAddressResponse>(result);
            }
            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objRequest"></param>
        /// <param name="strIdSession"></param>
        /// <returns></returns>
        public static COLIVING.GetDataBilling.GetDataBillingResponse GetDataBilling(COLIVING.GetDataBilling.DataBillingRequest objRequest, string strIdSession)
        {
            COLIVING.GetDataBilling.GetDataBillingResponse response = null;
            try
            {

                Claro.Entity.AuditRequest objAudit   = new Claro.Entity.AuditRequest(){
                    ApplicationName = "",
                    IPAddress = "",
                    Transaction= objRequest.IdTransaccion,
                    UserName = objRequest.UserId
                };

                //Claro.Web.Logging.Info(strIdSession, "IdTransaccion", String.Format("[GetUpdateDataClient] Parametros Entrada: tipoDocumento:{0}, numeroDocumento:{1}, casoEspecial:{2}", objRequest.ValidateCollaboratorMessageRequest.ValidateCollaboratorBodyRequest.ValidateCollaboratorRequest.KindDocument, objRequest.ValidateCollaboratorMessageRequest.ValidateCollaboratorBodyRequest.ValidateCollaboratorRequest.NumberDocument, objRequest.ValidateCollaboratorMessageRequest.ValidateCollaboratorBodyRequest.ValidateCollaboratorRequest.CaseSpecial));

                response = RestService.PostInvoque<COLIVING.GetDataBilling.GetDataBillingResponse>(CONFIGURATION.RestServiceConfiguration.OBTENER_DATOS_FACTURACION, objAudit, objRequest.oDataBillingRequest, false);

                Claro.Web.Logging.Info(objRequest.IdTransaccion, objRequest.IdTransaccion, String.Format("[GetDataBilling] Parametros Salida: IdTransaccion:{0}, CodigoRespuesta:{1}, MensajeRespuesta:{2}", response.GetBillingResponse.AuditResponse.IdTransaction, response.GetBillingResponse.AuditResponse.CodeResponse, response.GetBillingResponse.AuditResponse.MessageResponse));
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.IdTransaccion, objRequest.IdTransaccion, Claro.MessageException.GetOriginalExceptionMessage(ex));
                string sep = " - ";
                int posResponse = ex.Message.IndexOf(sep);
                string result = ex.Message.Substring(posResponse + sep.Length);
            }
            return response;
        }



        /// <summary>
        /// Registra los datos midifcados del cliente
        /// </summary>
        /// <param name="objRequest"></param>
        /// <param name="strIdSession"></param>
        /// <returns></returns> 

        public static HistoryClientResponse PostHistoryClientResponse(DataClientRequest objRequest, HistoryClient request, string strIdSession)
        {
            Claro.Web.Logging.Info(objRequest.IdTransaccion, objRequest.IdTransaccion, "PostHistoryClientResponse Inicio");
            HistoryClientResponse response = null;
            try
            {
                Hashtable paramUrl = new Hashtable();
                paramUrl.Add("idTransaccion", objRequest.IdTransaccion);
                paramUrl.Add("msgid", objRequest.MsgId);
                paramUrl.Add("timestamp", objRequest.TimesTamp);
                paramUrl.Add("userId", objRequest.UserId);
                paramUrl.Add("channel", objRequest.Channel);
                paramUrl.Add("idApplication", objRequest.IpAplicacion);
                Claro.Web.Logging.Info(objRequest.IdTransaccion, objRequest.IdTransaccion, "PostHistoryClientResponse Invocación servicio :" + CONFIGURATION.RestServiceConfiguration.REGISTRAR_HISTORIAL_CLIENTE);

                response = RestService.PostInvoqueSDP<HistoryClientResponse>(CONFIGURATION.RestServiceConfiguration.REGISTRAR_HISTORIAL_CLIENTE, paramUrl, request);

                Claro.Web.Logging.Info( objRequest.IdTransaccion, objRequest.IdTransaccion, String.Format("[PostHistoryClientResponse] Parametros Salida: IdTransaccion:{0}, CodigoRespuesta:{1}, MensajeRespuesta:{2}", response.responseAudit.IdTransaction, response.responseAudit.CodeResponse, response.responseAudit.MessageResponse));
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.IdTransaccion, objRequest.IdTransaccion, Claro.MessageException.GetOriginalExceptionMessage(ex));
                string sep = " - ";
                int posResponse = ex.Message.IndexOf(sep);
                string result = ex.Message.Substring(posResponse + sep.Length);
                response = JsonConvert.DeserializeObject<HistoryClientResponse>(result);
            }
            return response;
        }

        //vtorremo
        public static List<DataHistorical> HistoryDataClientTobe(Claro.Entity.AuditRequest audit, string strCustomerID, string flagconvivencia)
        {
            List<DataHistorical> ListaHistorico = new List<DataHistorical>();
            var flagconv = string.Empty;
            Claro.Web.Logging.Info(audit.Session, audit.Transaction, "HistoryDataClientTobe Inicio");
            try
            {
                
                if (flagconvivencia.Equals(Claro.Constants.NumberZeroString))
                {
                    flagconv = Claro.Constants.NumberOneString;
                }
                else
                {
                    flagconv = Claro.Constants.NumberTwoString;
                }

                COLIVING.GetDataHistoryClient.GetDataHistoryRequest request = new COLIVING.GetDataHistoryClient.GetDataHistoryRequest()
                {
                    consultarHistoricoDatosRequest = new COLIVING.GetDataHistoryClient.ConsultarHistoricoDatosRequest()
                    {
                        customerId = strCustomerID,
                        listaOpcional = new COLIVING.ListOptional() { clave = "flagConvivencia", valor = flagconv }
                    }
                };

                Claro.Web.Logging.Info(audit.Session, audit.Transaction, "HistoryDataClientTobe Invocación Inicio" + CONFIGURATION.RestServiceConfiguration.OBTENER_DATA_HISTORICO_CLIENTE);
                COLIVING.GetDataHistoryClient.GetDataHistoryResponse response = RestService.PostInvoque<COLIVING.GetDataHistoryClient.GetDataHistoryResponse>(CONFIGURATION.RestServiceConfiguration.OBTENER_DATA_HISTORICO_CLIENTE, audit, request, false);

                if (response != null &&
                    response.consultarHistoricoDatosResponse != null &&
                    response.consultarHistoricoDatosResponse.responseAudit != null &&
                    response.consultarHistoricoDatosResponse.responseAudit.CodeResponse == Claro.Constants.NumberZeroString &&
                    response.consultarHistoricoDatosResponse.responseData != null &&
                    response.consultarHistoricoDatosResponse.responseData.listaHistoricoDatos != null)
                {

                    foreach (var item in response.consultarHistoricoDatosResponse.responseData.listaHistoricoDatos)
                    {
                        DataHistorical temp = new DataHistorical();

                        temp.CustomerId = item.customerId;
                        temp.NroDoc = item.dniRuc;
                        temp.Fax = item.dniRucRepLegal;
                        temp.LegalRep = item.repreLegal;
                        temp.DocType = item.tipoDocRepLegal;
                        temp.BusinessName = item.razonSocial;
                        temp.FirstName = item.nombres;
                        temp.LastName = item.apellido;
                        temp.Telephone = item.numTelefono;
                        temp.Movil = item.numCelular;
                        temp.Email = item.email;
                        temp.Contact = item.contactoCliente;
                        temp.NationalityDesc = item.nacionalidad;
                        temp.Sex = item.sexo;
                        temp.MaritalStatus = item.estadoCivil;

                        temp.AddressFact = item.direccionFact;
                        temp.AddressNoteFact = item.notasDireccionFact;
                        temp.DistrictFact = item.distritoFact;
                        temp.ProvinceFact = item.provinciaFact;
                        temp.DepartmentFact = item.departamentoFact;
                        temp.CountryFact = item.pais;

                        temp.AddressLegal = item.direccionLeg;
                        temp.AddressNoteLegal = item.notasDireccionLeg;
                        temp.DistrictLegal = item.distritoLeg;
                        temp.ProvinceLegal = item.provinciaLeg;
                        temp.DepartmentLegal = item.departamentoLeg;
                        temp.CountryLegal = item.pais;

                        temp.ChangeMot = item.motivo;
                        temp.fechaRegistroCli = string.IsNullOrEmpty(item.fechaRegistro) ? string.Empty : item.fechaRegistro.Substring(8, 2) + "/" + item.fechaRegistro.Substring(5, 2) + "/" + item.fechaRegistro.Substring(0, 4);
                        temp.updCliente = item.updCliente;
                        temp.updDataMinor = item.updDataMinor;
                        temp.updDirLegal = item.updDirLegal;
                        temp.updDirFac = item.updDirFac;
                        temp.updRepLegal = item.updRepLegal;
                        temp.updContact = item.updContact;

                        ListaHistorico.Add(temp);
                    }

                }
            }
            catch (Exception ex)
            {
                Web.Logging.Error(audit.Session, audit.Transaction, Claro.MessageException.GetOriginalExceptionMessage(ex));
            }

            ListaHistorico = ListaHistorico.OrderBy(p => p.fechaRegistroCli).Reverse().ToList();

            return ListaHistorico;

        }

        //vtorremo
        public static ResponseCUParticipante GetCuParticipante(RequestCUParticipante objRequest, HeaderToBe objAudit)
        {
            Claro.Web.Logging.Info(objAudit.IdTransaccion, objAudit.IdTransaccion, "GetCuParticipante Inicio");
            ResponseCUParticipante response = null;
            try
            {
                Hashtable paramUrl = new Hashtable();
                paramUrl.Add("idTransaccion", objAudit.IdTransaccion);
                paramUrl.Add("msgid", "MsgId");
                paramUrl.Add("timestamp", DateTime.Now.ToString("s") + "Z");
                paramUrl.Add("userId", "Eai");
                paramUrl.Add("channel", "Channel");
                paramUrl.Add("idApplication", objAudit.IpAplicacion);
                response = RestService.PostInvoqueSDP<ResponseCUParticipante>(CONFIGURATION.RestServiceConfiguration.OBTENER_DATA_CUPARTICIPANTE, paramUrl, objRequest);

                Claro.Web.Logging.Info(objAudit.IdTransaccion, objAudit.IdTransaccion, String.Format("[ResponseCUParticipante] Parametros Salida: CodigoRespuesta:{0}, MensajeRespuesta:{1}", response.codigoRespuesta, response.mensajeRespuesta));
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objAudit.IdTransaccion, objAudit.IdTransaccion, Claro.MessageException.GetOriginalExceptionMessage(ex));
                string sep = " - ";
                int posResponse = ex.Message.IndexOf(sep);
                string result = ex.Message.Substring(posResponse + sep.Length);
                response = JsonConvert.DeserializeObject<ResponseCUParticipante>(result);
            }
            return response;
        }

        public static ResponseCBIO getDataCustomerCBIO(Claro.Entity.AuditRequest audit,request objRequest) {

            response objResponse = new response();
            ResponseCBIO objResCbio = new ResponseCBIO();

            try
            {
                objResponse = RestService.PostInvoque<response>(Claro.SIACU.Data.Transac.Service.Configuration.RestServiceConfiguration.OBTENER_DATOS_CLIENTE_TOBE, audit, objRequest, false);
                objResCbio.replegal = objResponse.obtenerDatosClienteResponse.responseData.clienteData.repLegal;
                Claro.Web.Logging.Info(audit.Session, audit.Transaction, "RepLegal: " + objResCbio.replegal);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(audit.Session, audit.Transaction, ex.Message);
            }
            return objResCbio;

        }
    }
}
