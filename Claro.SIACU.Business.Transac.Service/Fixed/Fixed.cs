using Claro.SIACU.Entity.Transac.Service.Fixed.GetGenerateOCC;
using System;
using System.Collections.Generic;
using System.Linq;
using EntitiesFixed = Claro.SIACU.Entity.Transac.Service.Fixed;
using FIXED = Claro.SIACU.Entity.Transac.Service.Fixed;
using COMMON = Claro.SIACU.Entity.Transac.Service.Common;
using KEY = Claro.ConfigurationManager;
using CONSTANTS = Claro.SIACU.Transac.Service.Constants;
using Claro.SIACU.Entity.Transac.Service.Fixed.GetCaseInsert;
using Claro.SIACU.Entity.Transac.Service.Fixed;
using Claro.SIACU.Entity.Transac.Service.Fixed.GetConsultationServiceByContract;
using Claro.SIACU.Entity.Transac.Service.Postpaid;
using Claro.SIACU.Entity.Transac.Service.Postpaid.GetQueryAssociatedLines;
using Claro.SIACU.Entity.Transac.Service.Fixed.GetCallDetailInputFixed;
using Claro.SIACU.Entity.Transac.Service.Fixed.GetServiceDTH;
using Claro.SIACU.Entity.Transac.Service.Fixed.BlackWhiteList; 
using Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo;
using Claro.SIACU.Entity.Transac.Service.Fixed.getConsultaLineaCuenta;
using POSTPREDATA = Claro.SIACU.ProxyService.Transac.Service.SIAC.Post.DatosPrePost_V2;
using Tools.Entity;
using Claro.SIACU.Entity.Transac.Service.Fixed.Discard;
using Claro.SIACU.Entity.Transac.Service.Fixed.Discard.ProcesarContinue; //INICIATIVA-871

namespace Claro.SIACU.Business.Transac.Service.Fixed
{
    public class Fixed
    {
        public static FIXED.GetCustomer.CustomerResponse GetCustomer(FIXED.GetCustomer.GetCustomerRequest objRequest)
        {
            FIXED.GetCustomer.CustomerResponse objResponse = new FIXED.GetCustomer.CustomerResponse();
            objResponse.rMsgText = string.Empty;
            objResponse.Customer = new FIXED.Customer();
            objResponse.vFlagConsulta = string.Empty;
            var vFlagConsulta = string.Empty;
            var rMsgText = string.Empty;
            try
            {
                var objEntity = Claro.Web.Logging.ExecuteMethod<FIXED.Customer>(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Data.Transac.Service.Fixed.Fixed.GetCustomer(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.vPhone, objRequest.vAccount, objRequest.vContactobjid1, objRequest.vFlagReg, ref vFlagConsulta, ref rMsgText);
                    });

                objResponse.rMsgText = rMsgText;
                objResponse.Customer = objEntity;
                objResponse.vFlagConsulta = vFlagConsulta;
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }

        /// <summary>
        /// Método para obtener el número de teléfono por código de contrato.
        /// </summary>
        /// <param name="objServiceRequest">Contiene id de contrato</param>
        /// <returns>Devuelve objeto ServiceResponse con información del servicio.</returns>
        public static FIXED.GetService.ServiceResponse GetTelephoneByContractCode(FIXED.GetService.ServiceRequest objServiceRequest)
        {
            FIXED.GetService.ServiceResponse objServiceResponse = null;
            if (objServiceRequest.ProductType.Equals(Claro.SIACU.Constants.LTE))
            {
                objServiceResponse = new FIXED.GetService.ServiceResponse()
                {
                    ListService = Claro.Web.Logging.ExecuteMethod<List<FIXED.Service>>(objServiceRequest.Audit.Session, objServiceRequest.Audit.Transaction, () => { return Data.Transac.Service.Fixed.Fixed.GetTelephoneByContractCodeLTE(objServiceRequest.Audit.Session, objServiceRequest.Audit.Transaction, objServiceRequest.Audit.IPAddress, objServiceRequest.Audit.ApplicationName, objServiceRequest.Audit.UserName, objServiceRequest.ContractID); })
                };
            }
            else if (objServiceRequest.ProductType.Equals(Claro.SIACU.Constants.HFC))
            {
                objServiceResponse = new FIXED.GetService.ServiceResponse()
                {
                    ListService = Claro.Web.Logging.ExecuteMethod<List<FIXED.Service>>(objServiceRequest.Audit.Session, objServiceRequest.Audit.Transaction, () => { return Data.Transac.Service.Fixed.Fixed.GetTelephoneByContractCodeHFC(objServiceRequest.Audit.Session, objServiceRequest.Audit.Transaction, objServiceRequest.Audit.IPAddress, objServiceRequest.Audit.ApplicationName, objServiceRequest.Audit.UserName, objServiceRequest.ContractID); })
                };
            }
            return objServiceResponse;
        }
        public static GenerateOCCResponse GenerateOCC(GenerateOCCRequest objRequest)
        {
            GenerateOCCResponse objResponse = new GenerateOCCResponse();

            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<GenerateOCCResponse>(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Data.Transac.Service.Fixed.Fixed.GenerateOCC(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);

            }

            return objResponse;
        }

        #region "Inst/Desinst Decodificadores"
        public static EntitiesFixed.GetJobTypes.JobTypesResponse GetJobTypes(EntitiesFixed.GetJobTypes.JobTypesRequest objJobTypesRequest)
        {
            List<EntitiesFixed.JobType> listServiceResponse = null;
            List<EntitiesFixed.JobType> listService = null;
            List<EntitiesFixed.JobType> listServiceOrdered;

            try
            {
                listService = Claro.Web.Logging.ExecuteMethod<List<EntitiesFixed.JobType>>(objJobTypesRequest.Audit.Session, objJobTypesRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.Fixed.GetJobTypes(objJobTypesRequest.Audit.Session, objJobTypesRequest.Audit.Transaction, objJobTypesRequest.p_tipo);
                });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objJobTypesRequest.Audit.Session, objJobTypesRequest.Audit.Transaction, ex.Message);
            }

            if (listService != null)
            {
                listServiceResponse = new List<EntitiesFixed.JobType>();
                listServiceOrdered = listService.OrderBy(a => a.descripcion).ToList();
                listServiceResponse = listServiceOrdered;
            }

            EntitiesFixed.GetJobTypes.JobTypesResponse objJobTypesResponse = new EntitiesFixed.GetJobTypes.JobTypesResponse()
            {
                JobTypes = listServiceResponse
            };

            return objJobTypesResponse;
        }
        public static EntitiesFixed.ETAFlowValidate.ETAFlowResponse ETAFlowValidate(EntitiesFixed.ETAFlowValidate.ETAFlowRequest RequestParam)
        {
            EntitiesFixed.ETAFlow listServiceResponse = null;
            EntitiesFixed.ETAFlow listService = null;
            try
            {
                listService = Claro.Web.Logging.ExecuteMethod<EntitiesFixed.ETAFlow>(RequestParam.Audit.Session, RequestParam.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.Fixed.ETAFlowValidate(RequestParam.Audit.Session, RequestParam.Audit.Transaction, RequestParam.as_origen, RequestParam.av_idplano, RequestParam.av_ubigeo, RequestParam.an_tiptra, RequestParam.an_tipsrv);
                });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(RequestParam.Audit.Session, RequestParam.Audit.Transaction, ex.Message);
            }

            if (listService != null)
            {
                listServiceResponse = listService;
            }

            EntitiesFixed.ETAFlowValidate.ETAFlowResponse Resultado = new EntitiesFixed.ETAFlowValidate.ETAFlowResponse()
            {
                ETAFlow = listServiceResponse
            };

            return Resultado;
        }
        public static EntitiesFixed.GetOrderType.OrderTypesResponse GetOrderType(EntitiesFixed.GetOrderType.OrderTypesRequest RequestParam)
        {
            List<EntitiesFixed.OrderType> listServiceResponse = null;
            List<EntitiesFixed.OrderType> listService = null;
            List<EntitiesFixed.OrderType> listServiceOrdered;
            try
            {
                listService = Claro.Web.Logging.ExecuteMethod<List<EntitiesFixed.OrderType>>(RequestParam.Audit.Session, RequestParam.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.Fixed.GetOrderType(RequestParam.Audit.Session, RequestParam.Audit.Transaction, RequestParam.vIdtiptra);
                });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(RequestParam.Audit.Session, RequestParam.Audit.Transaction, ex.Message);
            }

            if (listService != null)
            {
                listServiceResponse = new List<EntitiesFixed.OrderType>();
                listServiceOrdered = listService.OrderBy(a => a.VALOR).ToList();
                listServiceResponse = listServiceOrdered;
            }

            EntitiesFixed.GetOrderType.OrderTypesResponse Resultado = new EntitiesFixed.GetOrderType.OrderTypesResponse()
            {
                ordertypes = listServiceResponse
            };

            return Resultado;
        }
        public static EntitiesFixed.GetOrderSubType.OrderSubTypesResponse GetOrderSubType(EntitiesFixed.GetOrderSubType.OrderSubTypesRequest RequestParam)
        {
            List<EntitiesFixed.OrderSubType> listServiceResponse = null;
            List<EntitiesFixed.OrderSubType> listService = null;
            List<EntitiesFixed.OrderSubType> listServiceOrdered;
            try
            {
                listService = Claro.Web.Logging.ExecuteMethod<List<EntitiesFixed.OrderSubType>>(RequestParam.Audit.Session, RequestParam.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.Fixed.GetOrderSubType(RequestParam.Audit.Session, RequestParam.Audit.Transaction, RequestParam.av_cod_tipo_orden);
                });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(RequestParam.Audit.Session, RequestParam.Audit.Transaction, ex.Message);
            }

            if (listService != null)
            {
                listServiceResponse = new List<EntitiesFixed.OrderSubType>();
                listServiceOrdered = listService.OrderBy(a => a.COD_SUBTIPO_ORDEN).ToList();
                listServiceResponse = listServiceOrdered;
            }

            EntitiesFixed.GetOrderSubType.OrderSubTypesResponse Resultado = new EntitiesFixed.GetOrderSubType.OrderSubTypesResponse()
            {
                OrderSubTypes = listServiceResponse
            };

            return Resultado;
        }

        public static EntitiesFixed.GetInsertDecoAdditional.InsertDecoAdditionalResponse GetInsertDecoAdditional(EntitiesFixed.GetInsertDecoAdditional.InsertDecoAdditionalRequest RequestParam)
        {
            var objInsertDecoAdditionalResponse = new EntitiesFixed.GetInsertDecoAdditional.InsertDecoAdditionalResponse();

            try
            {
                string rCodRes = string.Empty;
                string rDescRes = string.Empty;

                objInsertDecoAdditionalResponse.rResultado = Claro.Web.Logging.ExecuteMethod<bool>(RequestParam.Audit.Session, RequestParam.Audit.Transaction,
                    () =>
                    {
                        return Claro.SIACU.Data.Transac.Service.Fixed.Fixed.GetInsertDecoAdditional(
                            RequestParam.Audit.Session,
                            RequestParam.Audit.Transaction,
                            RequestParam.vInter,
                            RequestParam.vServ,
                            RequestParam.vGrupoPrincipal,
                            RequestParam.vGrupo,
                            RequestParam.vCantidadInst,
                            RequestParam.vDscServ,
                            RequestParam.vBandwid,
                            RequestParam.vFlagLc,
                            RequestParam.vCantIdLinea,
                            RequestParam.vTipoEquipo,
                            RequestParam.vCodTipoEquipo,
                            RequestParam.vCantidad,
                            RequestParam.vDscEquipo,
                            RequestParam.vCodigoExt,
                            RequestParam.vSnCode,
                            RequestParam.vSpCode,
                            RequestParam.vCargoFijo,
                            ref rCodRes,
                            ref rDescRes);
                    });
                objInsertDecoAdditionalResponse.rCodRes = rCodRes;
                objInsertDecoAdditionalResponse.rDescRes = rDescRes;
            }
            catch (Exception ex)
            {
                Web.Logging.Error(RequestParam.Audit.Session, RequestParam.Audit.Transaction, ex.Message);
            }

            return objInsertDecoAdditionalResponse;
        }
        public static EntitiesFixed.GetInsertDetailServiceInteraction.InsertDetailServiceInteractionResponse GetInsertDetailServiceInteraction(EntitiesFixed.GetInsertDetailServiceInteraction.InsertDetailServiceInteractionRequest RequestParam)
        {
            var objInsertDetailServiceInteractionResponse = new EntitiesFixed.GetInsertDetailServiceInteraction.InsertDetailServiceInteractionResponse();

            try
            {
                string resultado = "";
                string mensaje = string.Empty;

                objInsertDetailServiceInteractionResponse.rResul = Claro.Web.Logging.ExecuteMethod<bool>(RequestParam.Audit.Session, RequestParam.Audit.Transaction,
                    () =>
                    {
                        return Claro.SIACU.Data.Transac.Service.Fixed.Fixed.GetInsertDetailServiceInteraction(
                            RequestParam.Audit.Session,
                            RequestParam.Audit.Transaction,
                            RequestParam.codinterac,
                            RequestParam.nombreserv,
                            RequestParam.tiposerv,
                            RequestParam.gruposerv,
                            RequestParam.cf,
                            RequestParam.equipo,
                            RequestParam.cantidad,
                            ref resultado,
                            ref mensaje);
                    });
                objInsertDetailServiceInteractionResponse.resultado = resultado;
                objInsertDetailServiceInteractionResponse.mensaje = mensaje;
            }
            catch (Exception ex)
            {
                Web.Logging.Error(RequestParam.Audit.Session, RequestParam.Audit.Transaction, ex.Message);
            }

            return objInsertDetailServiceInteractionResponse;
        }
        public static EntitiesFixed.GetInsertETASelection.InsertETASelectionResponse GetInsertETASelection(EntitiesFixed.GetInsertETASelection.InsertETASelectionRequest RequestParam)
        {
            var objInsertETASelectionResponse = new EntitiesFixed.GetInsertETASelection.InsertETASelectionResponse();

            try
            {
                string vresp = string.Empty;

                objInsertETASelectionResponse.vresp = Claro.Web.Logging.ExecuteMethod<string>(RequestParam.Audit.Session, RequestParam.Audit.Transaction,
                    () =>
                    {
                        return Claro.SIACU.Data.Transac.Service.Fixed.Fixed.GetInsertETASelection(
                            RequestParam.Audit.Session,
                            RequestParam.Audit.Transaction,
                            RequestParam.vidconsulta,
                            RequestParam.vidInteraccion,
                            RequestParam.vfechaCompromiso,
                            RequestParam.vfranja,
                            RequestParam.vid_bucket,
                            ref vresp);
                    });
                objInsertETASelectionResponse.vresp = vresp;
            }
            catch (Exception ex)
            {
                Web.Logging.Error(RequestParam.Audit.Session, RequestParam.Audit.Transaction, ex.Message);
            }

            return objInsertETASelectionResponse;
        }
        public static EntitiesFixed.GetInsertTransaction.InsertTransactionResponse GetInsertTransaction(EntitiesFixed.GetInsertTransaction.InsertTransactionRequest RequestParam)
        {
            var objInsertTransactionResponse = new EntitiesFixed.GetInsertTransaction.InsertTransactionResponse();

            try
            {
                string rstrResCod = string.Empty;
                string rstrResDes = string.Empty;

                objInsertTransactionResponse.intNumSot = Claro.Web.Logging.ExecuteMethod<string>(RequestParam.Audit.Session, RequestParam.Audit.Transaction,
                    () =>
                    {
                        return Claro.SIACU.Data.Transac.Service.Fixed.Fixed.GetInsertTransaction(
                            RequestParam.Audit.Session,
                            RequestParam.Audit.Transaction,
                            RequestParam.oTransfer,
                            ref rstrResCod,
                            ref rstrResDes);
                    });
                objInsertTransactionResponse.rintResCod = rstrResCod;
                objInsertTransactionResponse.rstrResDes = rstrResDes;
            }
            catch (Exception ex)
            {
                Web.Logging.Error(RequestParam.Audit.Session, RequestParam.Audit.Transaction, ex.Message);
            }

            return objInsertTransactionResponse;
        }
        public static Entity.Transac.Service.Fixed.GetServicesByInteraction.InteractionServiceResponse GetServicesByInteraction(Entity.Transac.Service.Fixed.GetServicesByInteraction.InteractionServiceRequest objInteractionServiceRequest)
        {
            List<Entity.Transac.Service.Fixed.ServiceByInteraction> listServiceResponse = null;
            List<Entity.Transac.Service.Fixed.ServiceByInteraction> listService = null;
            List<Entity.Transac.Service.Fixed.ServiceByInteraction> listServiceOrdered;
            try
            {
                listService = Claro.Web.Logging.ExecuteMethod<List<Entity.Transac.Service.Fixed.ServiceByInteraction>>(objInteractionServiceRequest.Audit.Session, objInteractionServiceRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.Fixed.GetServicesByInteraction(objInteractionServiceRequest.Audit.Session, objInteractionServiceRequest.Audit.Transaction, objInteractionServiceRequest.idInteraccion);
                });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objInteractionServiceRequest.Audit.Session, objInteractionServiceRequest.Audit.Transaction, ex.Message);
            }

            if (listService != null)
            {
                listServiceResponse = new List<Entity.Transac.Service.Fixed.ServiceByInteraction>();
                listServiceOrdered = listService.OrderBy(a => a.COD_INTERAC).ToList();
                listServiceResponse = listServiceOrdered;
            }
            Entity.Transac.Service.Fixed.GetServicesByInteraction.InteractionServiceResponse objInteractionServiceResponse = new Entity.Transac.Service.Fixed.GetServicesByInteraction.InteractionServiceResponse()
            {
                Services = listServiceResponse
            };
            return objInteractionServiceResponse;
        }
        #endregion


        public static FIXED.GetInsertInteractionBusiness.InsertInteractionBusinessResponse GetInsertInteractionBusiness(FIXED.GetInsertInteractionBusiness.InsertInteractionBusinessRequest objRequest)
        {
            string strTelefono = string.Empty;
            bool resultado;
            string sInteraccionId = string.Empty;
            string sFlagInsercion = string.Empty;
            string sMsgText = string.Empty;
            string sFlagInsercionInteraccion = string.Empty;
            string sMsgTextInteraccion = string.Empty;
            string sCodigoRetornoTransaccion = string.Empty;
            string sMensajeErrorTransaccion = string.Empty;

            string ContingenciaClarify = KEY.AppSettings("gConstContingenciaClarify");

            FIXED.GetInsertInteractionBusiness.InsertInteractionBusinessResponse oInsertInteractionBusinessResponse = new FIXED.GetInsertInteractionBusiness.InsertInteractionBusinessResponse();



            FIXED.GetCustomer.CustomerResponse oCustomerResponse;

            if (objRequest.Phone == objRequest.Interaction.TELEFONO)
            {
                strTelefono = objRequest.Phone;
            }
            else
            {
                strTelefono = objRequest.Interaction.TELEFONO;
            }

            FIXED.GetCustomer.GetCustomerRequest oCustomerRequest = new FIXED.GetCustomer.GetCustomerRequest();
            oCustomerRequest.vPhone = strTelefono;
            oCustomerRequest.vAccount = string.Empty;
            oCustomerRequest.vContactobjid1 = objRequest.Interaction.OBJID_CONTACTO;
            oCustomerRequest.vFlagReg = CONSTANTS.strUno;
            oCustomerRequest.Audit = objRequest.Audit;

            oCustomerResponse = GetCustomer(oCustomerRequest);

            if (oCustomerResponse.Customer != null)
            {
                objRequest.Interaction.OBJID_CONTACTO = oCustomerResponse.Customer.OBJID_CONTACTO;//TODO
            }

            if (ContingenciaClarify != Constants.Yes)
            {
                #region GetBusinessInteraction2
                COMMON.GetBusinessInteraction2.BusinessInteraction2Request oBusinessInteraction2Request = new COMMON.GetBusinessInteraction2.BusinessInteraction2Request();
                oBusinessInteraction2Request.Item = new COMMON.Iteraction();
                oBusinessInteraction2Request.Item = objRequest.Interaction;
                oBusinessInteraction2Request.Audit = objRequest.Audit;


                COMMON.GetBusinessInteraction2.BusinessInteraction2Response oBusinessInteraction2Response = Common.GetBusinessInteractionFixed(oBusinessInteraction2Request);
                resultado = oBusinessInteraction2Response.ProcessOK;
                sInteraccionId = oBusinessInteraction2Response.InteractionId;
                sFlagInsercion = oBusinessInteraction2Response.FlagInsertion;
                sMsgText = oBusinessInteraction2Response.MsgText ?? "";
                #endregion

            }
            else
            {
                #region GetInsertInteract
                COMMON.GetInsertInteract.InsertInteractRequest oInsertInteractRequest = new COMMON.GetInsertInteract.InsertInteractRequest();
                oInsertInteractRequest.item = new COMMON.Iteraction();
                oInsertInteractRequest.item = objRequest.Interaction;
                oInsertInteractRequest.Audit = objRequest.Audit;


                COMMON.GetInsertInteract.InsertInteractResponse oInsertInteractResponse = Common.GetInsertInteract(oInsertInteractRequest);
                resultado = oInsertInteractResponse.ProcesSucess;
                sInteraccionId = oInsertInteractResponse.Interactionid;
                sFlagInsercion = oInsertInteractResponse.FlagInsercion;
                sMsgText = oInsertInteractResponse.MsgText;
                #endregion
            }

            if (!string.IsNullOrEmpty(sInteraccionId))
            {
                if (objRequest.InteractionTemplate != null)
                {
                    if (ContingenciaClarify != Constants.Yes)
                    {
                        #region GetInsertInteractionTemplate
                        COMMON.GetInsertTemplateInteraction.InsertTemplateInteractionRequest oInsertTemplateInteractionRequest = new COMMON.GetInsertTemplateInteraction.InsertTemplateInteractionRequest();
                        oInsertTemplateInteractionRequest.IdInteraction = sInteraccionId;
                        oInsertTemplateInteractionRequest.item = new COMMON.InsertTemplateInteraction();
                        oInsertTemplateInteractionRequest.item = objRequest.InteractionTemplate;
                        oInsertTemplateInteractionRequest.Audit = objRequest.Audit;

                        COMMON.GetInsertTemplateInteraction.InsertTemplateInteractionResponse oInsertTemplateInteractionResponse = new COMMON.GetInsertTemplateInteraction.InsertTemplateInteractionResponse();
                        oInsertTemplateInteractionResponse = Common.GetInsertInteractionTemplate(oInsertTemplateInteractionRequest);

                        resultado = oInsertTemplateInteractionResponse.ProcesSucess;
                        sFlagInsercionInteraccion = oInsertTemplateInteractionResponse.FlagInsercion;
                        sMsgTextInteraccion = oInsertTemplateInteractionResponse.MsgText;
                        sCodigoRetornoTransaccion = string.Empty;
                        #endregion
                    }
                    else
                    {
                        #region GetInsInteractionTemplate
                        COMMON.GetInsTemplateInteraction.InsTemplateInteractionRequest oInsTemplateInteractionRequest = new COMMON.GetInsTemplateInteraction.InsTemplateInteractionRequest();
                        oInsTemplateInteractionRequest.IdInteraction = sInteraccionId;
                        oInsTemplateInteractionRequest.item = new COMMON.InsertTemplateInteraction();
                        oInsTemplateInteractionRequest.item = objRequest.InteractionTemplate;
                        oInsTemplateInteractionRequest.Audit = objRequest.Audit;

                        COMMON.GetInsTemplateInteraction.InsTemplateInteractionResponse oInsTemplateInteractionResponse = Common.GetInsInteractionTemplate(oInsTemplateInteractionRequest);

                        resultado = oInsTemplateInteractionResponse.ProcessSucess;
                        sFlagInsercionInteraccion = oInsTemplateInteractionResponse.FlagInsercion;
                        sMsgTextInteraccion = oInsTemplateInteractionResponse.MsgText;
                        sCodigoRetornoTransaccion = string.Empty;
                        #endregion
                    }


                    string strTransaccion = objRequest.InteractionTemplate._NOMBRE_TRANSACCION;
                    if (!string.IsNullOrEmpty(strTransaccion) && objRequest.ExecuteTransactation == true)
                    {
                        sCodigoRetornoTransaccion = string.Empty;
                        sMensajeErrorTransaccion = string.Empty;
                    }
                    else
                    {
                        sCodigoRetornoTransaccion = Constants.Zero;
                        sMensajeErrorTransaccion = string.Empty;

                    }
                }
            }

            oInsertInteractionBusinessResponse.InteractionId = sInteraccionId;
            oInsertInteractionBusinessResponse.Result = resultado;
            oInsertInteractionBusinessResponse.FlagInsercion = sFlagInsercion;
            oInsertInteractionBusinessResponse.MsgText = sMsgText;
            oInsertInteractionBusinessResponse.FlagInsercionInteraction = sFlagInsercionInteraccion;
            oInsertInteractionBusinessResponse.MsgTextInteraction = sMsgTextInteraccion;
            oInsertInteractionBusinessResponse.CodReturnTransaction = sCodigoRetornoTransaccion;
            oInsertInteractionBusinessResponse.MsgTextTransaccion = sMensajeErrorTransaccion;

            return oInsertInteractionBusinessResponse;
        }


        public static FIXED.GetCustomer.CustomerResponse GetValidateCustomer(EntitiesFixed.GetCustomer.GetCustomerRequest oGetCustomerRequest)
        {

            EntitiesFixed.GetCustomer.CustomerResponse oCustomerResponse = new EntitiesFixed.GetCustomer.CustomerResponse();
            try
            {
              oCustomerResponse =  Claro.Web.Logging.ExecuteMethod(oGetCustomerRequest.Audit.Session, oGetCustomerRequest.Audit.Transaction, () =>
                                    {
                                        return Data.Transac.Service.Common.GetValidateCustomer(oGetCustomerRequest);
                                    });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(oGetCustomerRequest.Audit.Session, oGetCustomerRequest.Audit.Transaction, ex.Message);
            }
            
            return oCustomerResponse;

        }


        public static EntitiesFixed.Interaction GetCreateCase(EntitiesFixed.Interaction oRequest)
        {
            EntitiesFixed.Interaction oResponse = new EntitiesFixed.Interaction();

            try
            {
                oResponse = Claro.Web.Logging.ExecuteMethod<EntitiesFixed.Interaction>(oRequest.Audit.Session, oRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.Fixed.GetCreateCase(oRequest);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(oRequest.Audit.Session, oRequest.Audit.Transaction, ex.Message);
            }

            return oResponse;

        }

        public static EntitiesFixed.Interaction GetInsertCase(EntitiesFixed.Interaction oItem)
        {
            EntitiesFixed.Interaction oResponse = new EntitiesFixed.Interaction();

            try
            {
                oResponse = Claro.Web.Logging.ExecuteMethod<EntitiesFixed.Interaction>(oItem.Audit.Session, oItem.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.Fixed.GetInsertCase(oItem);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(oItem.Audit.Session, oItem.Audit.Transaction, ex.Message);
            }

            return oResponse;

        }

        public static EntitiesFixed.CaseTemplate GetInsertTemplateCase(EntitiesFixed.CaseTemplate oItem)
        {
            EntitiesFixed.CaseTemplate oResponse = new EntitiesFixed.CaseTemplate();

            try
            {
                oResponse = Claro.Web.Logging.ExecuteMethod<EntitiesFixed.CaseTemplate>(oItem.Audit.Session, oItem.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.Fixed.GetInsertTemplateCase(oItem);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(oItem.Audit.Session, oItem.Audit.Transaction, ex.Message);
            }

            return oResponse;

        }

        public static EntitiesFixed.CaseTemplate GetInsertTemplateCaseContingent(EntitiesFixed.CaseTemplate oItem)
        {
            EntitiesFixed.CaseTemplate oResponse = new EntitiesFixed.CaseTemplate();

            try
            {
                oResponse = Claro.Web.Logging.ExecuteMethod<EntitiesFixed.CaseTemplate>(oItem.Audit.Session, oItem.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.Fixed.GetInsertTemplateCaseContingent(oItem);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(oItem.Audit.Session, oItem.Audit.Transaction, ex.Message);
            }

            return oResponse;

        }


        public static EntitiesFixed.CaseTemplate ActualizaPlantillaCaso(EntitiesFixed.CaseTemplate oItem)
        {
            EntitiesFixed.CaseTemplate oResponse = new EntitiesFixed.CaseTemplate();

            try
            {
                oResponse = Claro.Web.Logging.ExecuteMethod<EntitiesFixed.CaseTemplate>(oItem.Audit.Session, oItem.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.Fixed.ActualizaPlantillaCaso(oItem);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(oItem.Audit.Session, oItem.Audit.Transaction, ex.Message);
            }
            return oResponse;
        }

        public static ConsultationServiceByContractResponse GetConsultationServiceByContract(ConsultationServiceByContractRequest oConsultationServiceByContractRequest)
        {
            ConsultationServiceByContractResponse oConsultationServiceByContractResponse = new ConsultationServiceByContractResponse();

            try
            {
                oConsultationServiceByContractResponse = Claro.Web.Logging.ExecuteMethod<ConsultationServiceByContractResponse>(oConsultationServiceByContractRequest.Audit.Session, oConsultationServiceByContractRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.Fixed.GetConsultationServiceByContract(oConsultationServiceByContractRequest);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(oConsultationServiceByContractRequest.Audit.Session, oConsultationServiceByContractRequest.Audit.Transaction, ex.Message);
            }

            return oConsultationServiceByContractResponse;

        }


        
        public static CallDetailInputFixedResponse GetCallDetailInputFixed(Entity.Transac.Service.Fixed.GetBpelCallDetail.BpelCallDetailRequest objRequest)
        {

            CallDetailInputFixedResponse oCallDetailInputFixedResponse = null;

            try
            {

                oCallDetailInputFixedResponse = Claro.Web.Logging.ExecuteMethod<CallDetailInputFixedResponse>(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Data.Transac.Service.Fixed.CallsDetail.GetCallDetailInputFixed(objRequest);
                    });


                
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return oCallDetailInputFixedResponse;
            

        }

        public static EntitiesFixed.GetTransactionScheduled.TransactionScheduledResponse GetTransactionScheduled(EntitiesFixed.GetTransactionScheduled.TransactionScheduledRequest objRequest) {
            var objResponse = new EntitiesFixed.GetTransactionScheduled.TransactionScheduledResponse();

            try
            {
                if (objRequest.vstrEstado.Equals(Claro.Constants.NumberOneNegativeString))
                    objRequest.vstrEstado = string.Empty;
                if (objRequest.vstrTipoTran.Equals(Claro.Constants.NumberOneNegativeString))
                    objRequest.vstrTipoTran = string.Empty;
                if (objRequest.vstrCacDac.Equals(Claro.Constants.NumberOneNegativeString))
                    objRequest.vstrCacDac = string.Empty;

                objResponse.ListTransactionScheduled = Claro.Web.Logging.ExecuteMethod<List<TransactionScheduled>>(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Claro.SIACU.Data.Transac.Service.Fixed.Fixed.GetTransactionScheduled(
                            objRequest.Audit.Session,
                            objRequest.Audit.Transaction,
                            objRequest.vstrCoId,
                            objRequest.vstrCuenta,
                            objRequest.vstrFDesde,
                            objRequest.vstrFHasta,
                            objRequest.vstrEstado,
                            objRequest.vstrAsesor,
                            objRequest.vstrTipoTran,
                            objRequest.vstrCodInter,
                            objRequest.vstrCacDac
                            );
                    });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }


        public static ServiceDTHResponse GetServiceDTH(ServiceDTHRequest objRequest)
        {
            ServiceDTHResponse objResponse = new ServiceDTHResponse();
            try
            {
                objResponse.ListServicesDTH = Web.Logging.ExecuteMethod<List<BEDeco>>(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Data.Transac.Service.Fixed.Fixed.GetServiceDTH(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.strCustomerId, objRequest.strCoid);

                    });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }
            return objResponse;
        }

        public static EntitiesFixed.GetSaveOCC.SaveOCCResponse GetSaveOCC(EntitiesFixed.GetSaveOCC.SaveOCCRequest objRequest)
        {
            string vCodResult = string.Empty;
            string vResultado = string.Empty;
            var objResponse = new EntitiesFixed.GetSaveOCC.SaveOCCResponse();

            try
            {

                var salida = Claro.Web.Logging.ExecuteMethod<bool>(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Claro.SIACU.Data.Transac.Service.Fixed.Fixed.GetSaveOCC(
                            objRequest.Audit.Session,
                            objRequest.Audit.Transaction,
                            objRequest.vCodSot,
                            objRequest.vCustomerId,
                            objRequest.vFechaVig,
                            objRequest.vMonto,
                            objRequest.vComentario,
                            objRequest.vflag,
                            objRequest.vAplicacion,
                            objRequest.vUsuarioAct,
                            objRequest.vFechaAct,
                            objRequest.vCodId,
                            ref vCodResult,
                            ref vResultado
                            );
                    });

                objResponse.rSalida = salida;
                objResponse.rCodResult = vCodResult;
                objResponse.rResultado = vResultado;
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }

        public static EntitiesFixed.GetInsertLoyalty.InsertLoyaltyResponse GetInsertLoyalty(EntitiesFixed.GetInsertLoyalty.InsertLoyaltyRequest objRequest)
        {
            string vCodResult = string.Empty;
            string vResultado = string.Empty;
            var objResponse = new EntitiesFixed.GetInsertLoyalty.InsertLoyaltyResponse();

            try
            {

                var salida = Claro.Web.Logging.ExecuteMethod<bool>(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Claro.SIACU.Data.Transac.Service.Fixed.Fixed.GetInsertLoyalty(
                            objRequest.Audit.Session,
                            objRequest.Audit.Transaction,
                            objRequest.oCustomer,
                            objRequest.vCodSoLot,
                            objRequest.vFlagDirecFact,
                            objRequest.vUser,
                            objRequest.vFechaReg,
                            ref vCodResult,
                            ref vResultado
                            );
                    });

                objResponse.rSalida = salida;
                objResponse.rCodResult = vCodResult;
                objResponse.rResultado = vResultado;
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }

        public static ConsultationServiceByContractResponse GetCustomerLineNumber(ConsultationServiceByContractRequest objRequest)
        {
            ConsultationServiceByContractResponse oConsultationServiceByContractResponse = new ConsultationServiceByContractResponse();

            try
            {
                oConsultationServiceByContractResponse = Claro.Web.Logging.ExecuteMethod<ConsultationServiceByContractResponse>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.Fixed.GetCustomerLineNumber(objRequest);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return oConsultationServiceByContractResponse;

        }

        public static EntitiesFixed.GetCaseInsert.CaseInsertResponse GetInteractIDforCaseID(EntitiesFixed.GetCaseInsert.CaseInsertRequest objRequest)
        {
            EntitiesFixed.GetCaseInsert.CaseInsertResponse Response = new EntitiesFixed.GetCaseInsert.CaseInsertResponse();

            try
            {
                Response = Claro.Web.Logging.ExecuteMethod<EntitiesFixed.GetCaseInsert.CaseInsertResponse>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.Fixed.GetInteractIDforCaseID(objRequest);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return Response;

        }
        public static Entity.Transac.Service.Fixed.GetCamapaign.CamapaignResponse GetCampaign(Entity.Transac.Service.Fixed.GetCamapaign.CamapaignRequest objRequest)
        {
            var objResponse = new Entity.Transac.Service.Fixed.GetCamapaign.CamapaignResponse();
            try
            {
                objResponse = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.Fixed.GetCampaign(objRequest);
                });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }


            return objResponse;
        }
        public static Entity.Transac.Service.Fixed.GetPlans.PlansResponse GetNewPlans(Entity.Transac.Service.Fixed.GetPlans.PlansRequest objRequest)
        {
            var objResponse = new Entity.Transac.Service.Fixed.GetPlans.PlansResponse();
            try
            {
                objResponse = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.Fixed.GetNewPlans(objRequest);
                });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }


            return objResponse;
        }

        public static Entity.Transac.Service.Fixed.GetValidateDepVelLte.ValidateDepVelLteResponse ValidateDepVelLTE(Entity.Transac.Service.Fixed.GetValidateDepVelLte.ValidateDepVelLteRequest objRequest)
        {
            var objResponse = new Entity.Transac.Service.Fixed.GetValidateDepVelLte.ValidateDepVelLteResponse();
            try
            {
                objResponse = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.Fixed.ValidateDepVelLTE(objRequest);
                });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }


            return objResponse;
        }

        public static Claro.SIACU.Entity.Transac.Service.Fixed.GetETAAuditoriaRequestCapacity.BEETAAuditoriaResponseCapacity GetETAAuditoriaRequestCapacity(Claro.SIACU.Entity.Transac.Service.Fixed.GetETAAuditoriaRequestCapacity.BEETAAuditoriaRequestCapacity objBEETAAuditoriaRequestCapacity)
        {

            var objResponse = new Claro.SIACU.Entity.Transac.Service.Fixed.GetETAAuditoriaRequestCapacity.BEETAAuditoriaResponseCapacity();
            try
            {
                objResponse = Web.Logging.ExecuteMethod(objBEETAAuditoriaRequestCapacity.Audit.Session, objBEETAAuditoriaRequestCapacity.Audit.Transaction,
                    () =>
                    {

                        return Data.Transac.Service.Fixed.Fixed.ConsultarCapacidadCuadrillas(objBEETAAuditoriaRequestCapacity);
                    });
            }
            catch (Exception ex)
            {

                Web.Logging.Error(objBEETAAuditoriaRequestCapacity.Audit.Session, objBEETAAuditoriaRequestCapacity.Audit.Transaction, ex.Message);
            }
            return objResponse;
        }

        public static EntitiesFixed.GetOrderSubType.OrderSubTypesResponse GetOrderSubTypeWork(EntitiesFixed.GetOrderSubType.OrderSubTypesRequest RequestParam)
        {
            List<EntitiesFixed.OrderSubType> listServiceResponse = null;
            List<EntitiesFixed.OrderSubType> listService = null;
            List<EntitiesFixed.OrderSubType> listServiceOrdered;
            try
            {
                listService = Claro.Web.Logging.ExecuteMethod<List<EntitiesFixed.OrderSubType>>(RequestParam.Audit.Session, RequestParam.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.Fixed.GetOrderSubTypeWork(RequestParam.Audit.Session, RequestParam.Audit.Transaction, RequestParam.av_cod_tipo_trabajo);
                });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(RequestParam.Audit.Session, RequestParam.Audit.Transaction, ex.Message);
            }

            if (listService != null)
            {
                listServiceResponse = new List<EntitiesFixed.OrderSubType>();
                listServiceOrdered = listService.OrderBy(a => a.COD_SUBTIPO_ORDEN).ToList();
                listServiceResponse = listServiceOrdered;
            }

            EntitiesFixed.GetOrderSubType.OrderSubTypesResponse Resultado = new EntitiesFixed.GetOrderSubType.OrderSubTypesResponse()
            {
                OrderSubTypes = listServiceResponse
            };

            return Resultado;
        }

        public static EntitiesFixed.GetTransactionScheduled.TransactionScheduledResponse GetSchedulingRule(EntitiesFixed.GetTransactionScheduled.TransactionScheduledRequest objRequest)
        {
            var objResponse = new EntitiesFixed.GetTransactionScheduled.TransactionScheduledResponse();

            try
            {

                objResponse.ListTransactionScheduled = Claro.Web.Logging.ExecuteMethod<List<TransactionScheduled>>(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Claro.SIACU.Data.Transac.Service.Fixed.Fixed.GetSchedulingRule(
                            objRequest.Audit.Session,
                            objRequest.Audit.Transaction,
                            objRequest.vstrIdParametro
                            );
                    });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }

        public static Entity.Transac.Service.Fixed.OrderSubType GetValidationSubTypeWork(Entity.Transac.Service.Fixed.GetOrderSubType.OrderSubTypesRequest objRequest)
        {
            Entity.Transac.Service.Fixed.OrderSubType Response = new Entity.Transac.Service.Fixed.OrderSubType();

            try
            {
                Response = Claro.Web.Logging.ExecuteMethod<Entity.Transac.Service.Fixed.OrderSubType>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.Fixed.GetValidationSubTypeWork(objRequest);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return Response;

        }

        public static FIXED.GetGenerateSOT.GenerateSOTResponse registraEta(FIXED.GetGenerateSOT.GenerateSOTRequest objGetGenerateSOTRequest)
        {
            Claro.SIACU.Entity.Transac.Service.Common.ListItem ItemGenerateSOT = new Claro.SIACU.Entity.Transac.Service.Common.ListItem();
            ItemGenerateSOT = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Transac.Service.Common.ListItem>(
               objGetGenerateSOTRequest.Audit.Session, objGetGenerateSOTRequest.Audit.Transaction,
               () =>
               {
                   return Data.Transac.Service.Fixed.Fixed.registraEta(objGetGenerateSOTRequest.Audit.Session,
                     objGetGenerateSOTRequest.Audit.Transaction
                     , objGetGenerateSOTRequest.vCoID
                     , objGetGenerateSOTRequest.idSubTypeWork
                     , objGetGenerateSOTRequest.vFeProg
                     , objGetGenerateSOTRequest.vFranja
                     , objGetGenerateSOTRequest.idBucket);

               });
            FIXED.GetGenerateSOT.GenerateSOTResponse objGetGenerateSOTResponse = new FIXED.GetGenerateSOT.GenerateSOTResponse()
            {
                IdGenerateSOT = ItemGenerateSOT.Code,
                DescMessaTransfer = ItemGenerateSOT.Description
            };
            return objGetGenerateSOTResponse;
        }

        public static FIXED.GetGenerateSOT.GenerateSOTResponse UpdateEta(FIXED.GetGenerateSOT.GenerateSOTRequest objGetGenerateSOTRequest)
        {
            Claro.SIACU.Entity.Transac.Service.Common.ListItem ItemGenerateSOT = new Claro.SIACU.Entity.Transac.Service.Common.ListItem();
            ItemGenerateSOT = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Transac.Service.Common.ListItem>(
               objGetGenerateSOTRequest.Audit.Session, objGetGenerateSOTRequest.Audit.Transaction,
               () =>
               {
                   return Data.Transac.Service.Fixed.Fixed.UpdateEta(objGetGenerateSOTRequest.Audit.Session,
                     objGetGenerateSOTRequest.Audit.Transaction
                     , objGetGenerateSOTRequest.vCoID
                     , objGetGenerateSOTRequest.vPlano
                     , objGetGenerateSOTRequest.Ubigeo
                     , objGetGenerateSOTRequest.idSubTypeWork
                     , objGetGenerateSOTRequest.FechaProgramada
                     , objGetGenerateSOTRequest.vFranja
                     , objGetGenerateSOTRequest.idBucket);

               });
            FIXED.GetGenerateSOT.GenerateSOTResponse objGetGenerateSOTResponse = new FIXED.GetGenerateSOT.GenerateSOTResponse()
            {
                IdGenerateSOT = ItemGenerateSOT.Code,
                DescMessaTransfer = ItemGenerateSOT.Description
            };
            return objGetGenerateSOTResponse;
        }

        public static Entity.Transac.Service.Fixed.GetDataServ.DataServByIdResponse GetDataServById(Entity.Transac.Service.Fixed.GetDataServ.DataServByIdRequest objRequest)
        {
            Entity.Transac.Service.Fixed.GetDataServ.DataServByIdResponse Response = new Entity.Transac.Service.Fixed.GetDataServ.DataServByIdResponse();

            try
            {
                Response = Claro.Web.Logging.ExecuteMethod<Entity.Transac.Service.Fixed.GetDataServ.DataServByIdResponse>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.Fixed.GetDataServById(objRequest);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return Response;

        }

        public static Entity.Transac.Service.Fixed.GetSavePostventa.DataSavePostventaDetServResponse SavePostventaDetServ(Entity.Transac.Service.Fixed.GetSavePostventa.DataSavePostventaDetServRequest objRequest)
        {
            Entity.Transac.Service.Fixed.GetSavePostventa.DataSavePostventaDetServResponse Response = new Entity.Transac.Service.Fixed.GetSavePostventa.DataSavePostventaDetServResponse();

            try
            {
                Response = Claro.Web.Logging.ExecuteMethod<Entity.Transac.Service.Fixed.GetSavePostventa.DataSavePostventaDetServResponse>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.Fixed.SavePostventaDetServ(objRequest);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return Response;

        }

        //AVANCE
        public static Entity.Transac.Service.Fixed.GetDetEquipmentLTE.DataEquipmentResponse GetDetEquipo_LTE(Entity.Transac.Service.Fixed.GetDetEquipmentLTE.DataEquipmentRequest objRequest)
        {
            Entity.Transac.Service.Fixed.GetDetEquipmentLTE.DataEquipmentResponse Response = new Entity.Transac.Service.Fixed.GetDetEquipmentLTE.DataEquipmentResponse();

            try
            {
                Response = Claro.Web.Logging.ExecuteMethod<Entity.Transac.Service.Fixed.GetDetEquipmentLTE.DataEquipmentResponse>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.Fixed.GetDetEquipo_LTE(objRequest);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return Response;

        }

        public static Claro.SIACU.Entity.Transac.Service.Fixed.PostEtaInboundToa.EtaInboundToaResponse PostGestionarOrdenesToa(Claro.SIACU.Entity.Transac.Service.Fixed.PostEtaInboundToa.EtaInboundToaRequest objRequestInboundEta)
        {

            var objResponse = new Claro.SIACU.Entity.Transac.Service.Fixed.PostEtaInboundToa.EtaInboundToaResponse();
            try
            {
                objResponse = Web.Logging.ExecuteMethod(objRequestInboundEta.Audit.Session, objRequestInboundEta.Audit.Transaction,
                    () =>
                    {

                        return Data.Transac.Service.Fixed.Fixed.PostGestionarOrdenesToa(objRequestInboundEta);
                    });
            }
            catch (Exception ex)
            {

                Web.Logging.Error(objRequestInboundEta.Audit.Session, objRequestInboundEta.Audit.Transaction, ex.Message);
            }
            return objResponse;
        }

        public static Claro.SIACU.Entity.Transac.Service.Fixed.PostEtaInboundToa.EtaInboundToaResponse PostGestionarCancelaToa(Claro.SIACU.Entity.Transac.Service.Fixed.PostEtaInboundToa.EtaInboundToaRequest objRequestInboundEta)
        {

            var objResponse = new Claro.SIACU.Entity.Transac.Service.Fixed.PostEtaInboundToa.EtaInboundToaResponse();
            try
            {
                objResponse = Web.Logging.ExecuteMethod(objRequestInboundEta.Audit.Session, objRequestInboundEta.Audit.Transaction,
                    () =>
                    {

                        return Data.Transac.Service.Fixed.Fixed.PostGestionarCancelaToa(objRequestInboundEta);
                    });
            }
            catch (Exception ex)
            {

                Web.Logging.Error(objRequestInboundEta.Audit.Session, objRequestInboundEta.Audit.Transaction, ex.Message);
            }
            return objResponse;
        }

        public static Claro.SIACU.Entity.Transac.Service.Fixed.GetHistoryToa.HistoryToaResponse GetHistoryToa(Claro.SIACU.Entity.Transac.Service.Fixed.GetHistoryToa.HistoryToaRequest objRequest)
        {

            var objResponse = new Claro.SIACU.Entity.Transac.Service.Fixed.GetHistoryToa.HistoryToaResponse();
            try
            {
                objResponse = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {

                        return Data.Transac.Service.Fixed.Fixed.GetHistoryToa(objRequest);
                    });
            }
            catch (Exception ex)
            {

                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }
            return objResponse;
        }

        public static Claro.SIACU.Entity.Transac.Service.Fixed.GetHistoryToa.HistoryToaResponse GetUpdateHistoryToa(Claro.SIACU.Entity.Transac.Service.Fixed.GetHistoryToa.HistoryToaRequest objRequest)
        {

            var objResponse = new Claro.SIACU.Entity.Transac.Service.Fixed.GetHistoryToa.HistoryToaResponse();
            try
            {
                objResponse = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {

                        return Data.Transac.Service.Fixed.Fixed.GetUpdateHistoryToa(objRequest);
                    });
            }
            catch (Exception ex)
            {

                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }
            return objResponse;
        }

        public static EntitiesFixed.ETAFlowValidate.ETAFlowResponse ETAFlowValidateReservation(EntitiesFixed.ETAFlowValidate.ETAFlowRequest RequestParam)
        {
            EntitiesFixed.ETAFlow listServiceResponse = null;
            EntitiesFixed.ETAFlow listService = null;
            try
            {
                listService = Claro.Web.Logging.ExecuteMethod<EntitiesFixed.ETAFlow>(RequestParam.Audit.Session, RequestParam.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.Fixed.ETAFlowValidateReservation(RequestParam.Audit.Session, RequestParam.Audit.Transaction,RequestParam.an_tiptra, RequestParam.an_tipsrv);
                });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(RequestParam.Audit.Session, RequestParam.Audit.Transaction, ex.Message);
            }

            if (listService != null)
            {
                listServiceResponse = listService;
            }

            EntitiesFixed.ETAFlowValidate.ETAFlowResponse Resultado = new EntitiesFixed.ETAFlowValidate.ETAFlowResponse()
            {
                ETAFlow = listServiceResponse
            };

            return Resultado;
        }

      

        public static FIXED.BlackWhiteList.GetStateLineEmail.SearchStateLineEmailResponse SearchStateLineEmail(FIXED.BlackWhiteList.GetStateLineEmail.SearchStateLineEmailRequest objRequest)
        {
            FIXED.BlackWhiteList.GetStateLineEmail.SearchStateLineEmailResponse Response = new FIXED.BlackWhiteList.GetStateLineEmail.SearchStateLineEmailResponse();

            try
            {
                Response = Claro.Web.Logging.ExecuteMethod<FIXED.BlackWhiteList.GetStateLineEmail.SearchStateLineEmailResponse>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.Fixed.SearchStateLineEmail(objRequest);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return Response;

        }

        public static FIXED.BlackWhiteList.GetUpdateStateLineEmail.UpdateStateLineEmailResponse UpdateStateLineEmail(FIXED.BlackWhiteList.GetUpdateStateLineEmail.UpdateStateLineEmailRequest objRequest)
        {
            FIXED.BlackWhiteList.GetUpdateStateLineEmail.UpdateStateLineEmailResponse Response = new FIXED.BlackWhiteList.GetUpdateStateLineEmail.UpdateStateLineEmailResponse();

            try
            {
                Response = Claro.Web.Logging.ExecuteMethod<FIXED.BlackWhiteList.GetUpdateStateLineEmail.UpdateStateLineEmailResponse>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.Fixed.UpdateStateLineEmail(objRequest);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return Response;

        }
            
        public static EntitiesFixed.GetValidateLine.ValidateLineResponse GetListValidateLine(EntitiesFixed.GetValidateLine.ValidateLineRequest request)
        {
         
            string codigoRespuesta = "";
            string mensajeRespuesta = "";
            List<EntitiesFixed.GetValidateLine.lineaConsolidada> list = Web.Logging.ExecuteMethod(request.Audit.Session, request.Audit.Transaction, () =>
            {
                return Data.Transac.Service.Fixed.Fixed.GetListValidateLine(request.Audit.Session, 
                                                                            request.Audit.Transaction,
                                                                            request.Audit.IPAddress, 
                                                                            request.Audit.ApplicationName, 
                                                                            request.Audit.UserName,
                                                                            request.numeroDocumento,
                                                                            request.straplicativo,
                                                                            request.valor,
                                                                            request.nombreCampo,
                                                                            ref codigoRespuesta,
                                                                            ref mensajeRespuesta);
            });

            EntitiesFixed.GetValidateLine.listaLineasConsolidadasType listaLineasConsolidadasType = new EntitiesFixed.GetValidateLine.listaLineasConsolidadasType()
            {
                lineaConsolidada = list
            };

            EntitiesFixed.GetValidateLine.ValidateLineResponse objResponse = new EntitiesFixed.GetValidateLine.ValidateLineResponse()
            {
                listaLineasConsolidadasType = listaLineasConsolidadasType,
                codRespuesta = codigoRespuesta,
                msjRespuesta = mensajeRespuesta
            };
            return objResponse;
        }


        public static bool GetInsertNoCliente(Claro.SIACU.Entity.Transac.Service.Fixed.Customer objRequest)
        {
            try
            {
                return Claro.Web.Logging.ExecuteMethod<bool>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.Fixed.GetInsertNoCliente(objRequest);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
                return false;
            }
        }


        public static FIXED.Discard.GetConsultDiscardRTI.ConsultDiscardRTIResponse ConsultDiscardRTI(FIXED.Discard.GetConsultDiscardRTI.ConsultDiscardRTIRequest objRequest)
        {
            FIXED.Discard.GetConsultDiscardRTI.ConsultDiscardRTIResponse Response = new FIXED.Discard.GetConsultDiscardRTI.ConsultDiscardRTIResponse();

            try
            {
                Response = Claro.Web.Logging.ExecuteMethod<FIXED.Discard.GetConsultDiscardRTI.ConsultDiscardRTIResponse>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.Fixed.ConsultDiscardRTI(objRequest);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return Response;
        }
		
        public static FIXED.Discard.GetConsultDiscardRTI.ConsultDiscardRTIResponse ConsultDiscardGrupoRTI(FIXED.Discard.GetConsultDiscardRTI.ConsultDiscardRTIRequestGrupo objRequest)
        {
            FIXED.Discard.GetConsultDiscardRTI.ConsultDiscardRTIResponse Response = new FIXED.Discard.GetConsultDiscardRTI.ConsultDiscardRTIResponse();

            try
            {
                Response = Claro.Web.Logging.ExecuteMethod<FIXED.Discard.GetConsultDiscardRTI.ConsultDiscardRTIResponse>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.Fixed.ConsultDiscardGrupoRTI(objRequest);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return Response;
        }

        public static FIXED.Discard.GetConsultDiscardRTITOBE.ConsultDiscartRTITOBEResponse ConsultDiscardRTIToBe(FIXED.Discard.GetConsultDiscardRTITOBE.ConsultDiscartRTITOBERequest objRequest, Tools.Entity.AuditRequest audirRequest)
        {
            FIXED.Discard.GetConsultDiscardRTITOBE.ConsultDiscartRTITOBEResponse Response = new FIXED.Discard.GetConsultDiscardRTITOBE.ConsultDiscartRTITOBEResponse();

            try
            {
                Response = Claro.Web.Logging.ExecuteMethod<FIXED.Discard.GetConsultDiscardRTITOBE.ConsultDiscartRTITOBEResponse>(audirRequest.Session, audirRequest.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.Fixed.ConsultDiscardRTIToBe(objRequest, audirRequest);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(audirRequest.Session, audirRequest.Transaction, ex.Message);
            }

            return Response;
        }
		
         public static FIXED.Discard.GetConsultDiscardRTITOBE.ConsultDiscartRTITOBEResponse ConsultDiscardRTIToBeGrupo(FIXED.Discard.GetConsultDiscardRTITOBE.ConsultDiscartRTITOBERequestGrupo objRequest, Tools.Entity.AuditRequest audirRequest)
        {
            FIXED.Discard.GetConsultDiscardRTITOBE.ConsultDiscartRTITOBEResponse Response = new FIXED.Discard.GetConsultDiscardRTITOBE.ConsultDiscartRTITOBEResponse();

            try
            {
                Response = Claro.Web.Logging.ExecuteMethod<FIXED.Discard.GetConsultDiscardRTITOBE.ConsultDiscartRTITOBEResponse>(audirRequest.Session, audirRequest.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.Fixed.ConsultDiscardRTIToBeGrupo(objRequest, audirRequest);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(audirRequest.Session, audirRequest.Transaction, ex.Message);
            }
        
            return Response;
        }

		 public static FIXED.ClaroVideo.GetConsultClientSN.ConsultClientSNResponse ConsultClientSN(FIXED.ClaroVideo.GetConsultClientSN.ConsultClientSNRequest objRequest)
        {
            FIXED.ClaroVideo.GetConsultClientSN.ConsultClientSNResponse Response = new FIXED.ClaroVideo.GetConsultClientSN.ConsultClientSNResponse();

            try
            {
                Response = Claro.Web.Logging.ExecuteMethod<FIXED.ClaroVideo.GetConsultClientSN.ConsultClientSNResponse>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.Fixed.ConsultClientSN(objRequest);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return Response;

        }

        public static FIXED.ClaroVideo.GetConsultSN.ConsultSNResponse ConsultSN(FIXED.ClaroVideo.GetConsultSN.ConsultSNRequest objRequest)
        {
            FIXED.ClaroVideo.GetConsultSN.ConsultSNResponse Response = new FIXED.ClaroVideo.GetConsultSN.ConsultSNResponse();

            try
            {
                Response = Claro.Web.Logging.ExecuteMethod<FIXED.ClaroVideo.GetConsultSN.ConsultSNResponse>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.Fixed.ConsultSN(objRequest);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return Response;

        }


        public static FIXED.ClaroVideo.GetProvisionSubscription.ProvisionSubscriptionResponse ProvisionSubscription(FIXED.ClaroVideo.GetProvisionSubscription.ProvisionSubscriptionRequest objRequest)
        {
            FIXED.ClaroVideo.GetProvisionSubscription.ProvisionSubscriptionResponse Response = new FIXED.ClaroVideo.GetProvisionSubscription.ProvisionSubscriptionResponse();

            try
            {
                Response = Claro.Web.Logging.ExecuteMethod<FIXED.ClaroVideo.GetProvisionSubscription.ProvisionSubscriptionResponse>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.Fixed.ProvisionSubscription(objRequest);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return Response;

        }


        public static FIXED.ClaroVideo.GetCancelSubscriptionSN.CancelSubscriptionSNResponse CancelSubscriptionSN(FIXED.ClaroVideo.GetCancelSubscriptionSN.CancelSubscriptionSNRequest objRequest)
        {
            FIXED.ClaroVideo.GetCancelSubscriptionSN.CancelSubscriptionSNResponse Response = new FIXED.ClaroVideo.GetCancelSubscriptionSN.CancelSubscriptionSNResponse();

            try
            {
                Response = Claro.Web.Logging.ExecuteMethod<FIXED.ClaroVideo.GetCancelSubscriptionSN.CancelSubscriptionSNResponse>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.Fixed.CancelSubscriptionSN(objRequest);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return Response;

        }

        public static FIXED.ClaroVideo.GetUpdateClientSN.UpdateClientSNResponse UpdateClientSN(FIXED.ClaroVideo.GetUpdateClientSN.UpdateClientSNRequest objRequest)
        {
            FIXED.ClaroVideo.GetUpdateClientSN.UpdateClientSNResponse Response = new FIXED.ClaroVideo.GetUpdateClientSN.UpdateClientSNResponse();

            try
            {
                Response = Claro.Web.Logging.ExecuteMethod<FIXED.ClaroVideo.GetUpdateClientSN.UpdateClientSNResponse>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.Fixed.UpdateClientSN(objRequest);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return Response;
        }

        public static FIXED.ClaroVideo.GetHistoryDevice.HistoryDeviceResponse HistoryDevice(FIXED.ClaroVideo.GetHistoryDevice.HistoryDeviceRequest objRequest)
        {
            FIXED.ClaroVideo.GetHistoryDevice.HistoryDeviceResponse Response = new FIXED.ClaroVideo.GetHistoryDevice.HistoryDeviceResponse();

            try
            {
                Response = Claro.Web.Logging.ExecuteMethod<FIXED.ClaroVideo.GetHistoryDevice.HistoryDeviceResponse>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.Fixed.HistoryDevice(objRequest);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return Response;
        }
        public static FIXED.ClaroVideo.GetRegisterClientSN.RegisterClientSNResponse RegisterClientSN(FIXED.ClaroVideo.GetRegisterClientSN.RegisterClientSNRequest objRequest)
        {
            FIXED.ClaroVideo.GetRegisterClientSN.RegisterClientSNResponse Response = new FIXED.ClaroVideo.GetRegisterClientSN.RegisterClientSNResponse();

            try
            {
                Response = Claro.Web.Logging.ExecuteMethod<FIXED.ClaroVideo.GetRegisterClientSN.RegisterClientSNResponse>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.Fixed.RegisterClientSN(objRequest);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return Response;
        }

        public static FIXED.ClaroVideo.GetContractedBusinessServices.ContractedBusinessServicesResponse GetContractServices(FIXED.ClaroVideo.GetContractedBusinessServices.ContractedBusinessServicesRequest objRequest)
        {
            FIXED.ClaroVideo.GetContractedBusinessServices.ContractedBusinessServicesResponse objContractedBusinessServicesResponse = new FIXED.ClaroVideo.GetContractedBusinessServices.ContractedBusinessServicesResponse();
          

            try
            {
                objContractedBusinessServicesResponse.ContractServices = Claro.Web.Logging.ExecuteMethod<List<FIXED.ClaroVideo.ContractServices>>(objRequest.Audit.Session, objRequest.Audit.Transaction, () => { return Data.Transac.Service.Fixed.Fixed.GetContractServicesHFCLTE(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.ContractId); });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }
            return objContractedBusinessServicesResponse;

        }

        public static FIXED.ClaroVideo.GetRegistrarControles.RegistrarControlesResponse RegistrarControles(FIXED.ClaroVideo.GetRegistrarControles.RegistrarControlesRequest objRequest)
        {
            FIXED.ClaroVideo.GetRegistrarControles.RegistrarControlesResponse Response = new FIXED.ClaroVideo.GetRegistrarControles.RegistrarControlesResponse();

            try
            {
                Response = Claro.Web.Logging.ExecuteMethod<FIXED.ClaroVideo.GetRegistrarControles.RegistrarControlesResponse>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.Fixed.RegistrarControles(objRequest);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return Response;
        }

        public static FIXED.ClaroVideo.GetValidateElegibility.ValidateElegibilityResponse ValidateElegibility(FIXED.ClaroVideo.GetValidateElegibility.ValidateElegibilityRequest objRequest)
        {
            FIXED.ClaroVideo.GetValidateElegibility.ValidateElegibilityResponse Response = new FIXED.ClaroVideo.GetValidateElegibility.ValidateElegibilityResponse();

            try
            {
                Response = Claro.Web.Logging.ExecuteMethod<FIXED.ClaroVideo.GetValidateElegibility.ValidateElegibilityResponse>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.Fixed.ValidateElegibility(objRequest);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return Response;
        }

        public static FIXED.ClaroVideo.GetPersonalizaMensajeOTT.PersonalizaMensajeOTTResponse PersonalizaMensajeOTT(FIXED.ClaroVideo.GetPersonalizaMensajeOTT.PersonalizaMensajeOTTRequest objRequest)
        {
            FIXED.ClaroVideo.GetPersonalizaMensajeOTT.PersonalizaMensajeOTTResponse Response = new FIXED.ClaroVideo.GetPersonalizaMensajeOTT.PersonalizaMensajeOTTResponse();

            try
            {
                Response = Claro.Web.Logging.ExecuteMethod<FIXED.ClaroVideo.GetPersonalizaMensajeOTT.PersonalizaMensajeOTTResponse>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.Fixed.PersonalizaMensajeOTT(objRequest);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return Response;

        }

    #region PROY-140510 - AMCO - Modulo de consulta y eliminar cuenta de Claro Video
        public static FIXED.ClaroVideo.GetDeleteClientSN.DeleteClientSNResponse DeleteClientSN(FIXED.ClaroVideo.GetDeleteClientSN.DeleteClientSNRequest objRequest)
        {
            FIXED.ClaroVideo.GetDeleteClientSN.DeleteClientSNResponse objResponse = new FIXED.ClaroVideo.GetDeleteClientSN.DeleteClientSNResponse();
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<FIXED.ClaroVideo.GetDeleteClientSN.DeleteClientSNResponse>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.Fixed.DeleteClientSN(objRequest);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }
            return objResponse;
        }

        public static POSTPREDATA.consultarDatosLineaResponse GetConsultaDatosLinea(Tools.Entity.AuditRequest objaudit, string strTelephone)
        {
            POSTPREDATA.consultarDatosLineaResponse objResponse = null;

            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<POSTPREDATA.consultarDatosLineaResponse>(objaudit.Session, objaudit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.Fixed.GetConsultaDatosLinea(objaudit, strTelephone);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objaudit.Session, objaudit.Transaction, ex.Message);
            }
            return objResponse;
        }

        public static ConsultaLineaResponse ConsultarLineaCuenta(ConsultaLineaRequest objConsultaLineaRequest)
        {
            Claro.Web.Logging.Info("IdSession: " + objConsultaLineaRequest.Audit.Session,
                "Transaccion: " + objConsultaLineaRequest.Audit.Transaction,
                string.Format("Capa Business-Metodo: {0}, Type:{1}, Value{2} ", "ConsultarLineaCuenta", objConsultaLineaRequest.Type, objConsultaLineaRequest.Value));

            ConsultaLineaResponse objConsultaLineaResponse = null;
            try
            {
                var migrateOne = KEY.AppSettings("strkeyMigrateOne").Split('|');
                objConsultaLineaResponse = Claro.Web.Logging.ExecuteMethod<ConsultaLineaResponse>(objConsultaLineaRequest.Audit.Session, objConsultaLineaRequest.Audit.Transaction,
               () =>
               {
                   return Data.Transac.Service.Fixed.Fixed.ConsultarLineaCuenta(objConsultaLineaRequest);
               });
                if (objConsultaLineaResponse != null)
                {
                    var result = migrateOne.Where(x => x.Split(';')[0] == objConsultaLineaResponse.ResponseValue).FirstOrDefault().Split(';')[1];
                    objConsultaLineaResponse.ResponseDescription = result;
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objConsultaLineaRequest.Audit.Session, objConsultaLineaRequest.Audit.Transaction, ex.Message);
            }
            return objConsultaLineaResponse;
        }

        public static EntitiesFixed.GetTypeProductDat.GetTypeProductDatResponse ConsultarContrato(string strIdSession, string strIdTransaction, EntitiesFixed.GetTypeProductDat.GetTypeProductDatRequest objRequest)
        {
            EntitiesFixed.GetTypeProductDat.GetTypeProductDatResponse objResponse = null;

            try
            {
                //objResponse = new FIXED.GetTypeProductDat.Response.response();

                objResponse = Claro.Web.Logging.ExecuteMethod<EntitiesFixed.GetTypeProductDat.GetTypeProductDatResponse>(strIdSession, strIdTransaction, () =>
                {
                    return Data.Transac.Service.Fixed.Fixed.ConsultarContrato(strIdSession, strIdTransaction, objRequest);
                }); 
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strIdTransaction, ex.Message);
            }
            return objResponse;
        }
        #endregion

        //INICIATIVA-794
        public static EntitiesFixed.ClaroVideo.GetConsultIPTV.ConsultIPTVResponse ConsultarServicioIPTV(EntitiesFixed.ClaroVideo.GetConsultIPTV.ConsultIPTVRequest Request)
        {
            EntitiesFixed.ClaroVideo.GetConsultIPTV.ConsultIPTVResponse objResponse = new EntitiesFixed.ClaroVideo.GetConsultIPTV.ConsultIPTVResponse();
            objResponse.lstConsultIPTV = new List<EntitiesFixed.ClaroVideo.ConsultIPTV>();
            List<EntitiesFixed.ClaroVideo.ConsultIPTV> lstConsultIPTV = new List<EntitiesFixed.ClaroVideo.ConsultIPTV>();
            try
            {
                lstConsultIPTV = Claro.Web.Logging.ExecuteMethod<List<EntitiesFixed.ClaroVideo.ConsultIPTV>>(Request.Audit.Session, Request.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.Fixed.ConsultarServicioIPTV(Request.Audit.Session, Request.Audit.Transaction, Request.strProducto);
                });
                objResponse.lstConsultIPTV = lstConsultIPTV;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(Request.Audit.Session, Request.Audit.Transaction, ex.Message);
                throw ex;
            }
            return objResponse;
        }
        public static EntitiesFixed.ClaroVideo.GetValidateIPTV.ValidateIPTVResponse ValidarServicioIPTV(EntitiesFixed.ClaroVideo.GetValidateIPTV.ValidateIPTVRequest Request)
        {
            EntitiesFixed.ClaroVideo.GetValidateIPTV.ValidateIPTVResponse objResponse = new EntitiesFixed.ClaroVideo.GetValidateIPTV.ValidateIPTVResponse();
            objResponse.lstValidateIPTV = new List<EntitiesFixed.ClaroVideo.ValidateIPTV>();
            List<EntitiesFixed.ClaroVideo.ValidateIPTV> lstValidateIPTV = new List<EntitiesFixed.ClaroVideo.ValidateIPTV>();
            try
            {
                lstValidateIPTV = Claro.Web.Logging.ExecuteMethod<List<EntitiesFixed.ClaroVideo.ValidateIPTV>>(Request.Audit.Session, Request.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.Fixed.ValidarServicioIPTV(Request.Audit.Session, Request.Audit.Transaction, Request.strCodNum, Request.strOpc);
                });
                objResponse.lstValidateIPTV = lstValidateIPTV;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(Request.Audit.Session, Request.Audit.Transaction, ex.Message);
                throw ex;
            }
            return objResponse;
        }

        //INI: INICIATIVA-871
        public static DatosSIMPrepago ObtenerDatosSIMPrepago(string strIdSession, string strTransactionID, string strApplicationUser, string strPhoneNumber)
        {
            DatosSIMPrepago objResponse = new DatosSIMPrepago();

            objResponse = Claro.Web.Logging.ExecuteMethod<DatosSIMPrepago>(strIdSession, strTransactionID, () =>
            {
                return Data.Transac.Service.Fixed.Fixed.ObtenerDatosSIMPrepago(strIdSession, strTransactionID, strApplicationUser, strPhoneNumber);
            });

            return objResponse;
        }

        public static Claro.SIACU.ProxyService.Transac.Service.InstantLinkSOA.Response ActivarDesactivarContinueWS(string strIdSession, string strTransaccion, AplicarRetirarContingencia objRequestContinue)
        {
            var objResponse = new Claro.SIACU.ProxyService.Transac.Service.InstantLinkSOA.Response();

            objResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.ProxyService.Transac.Service.InstantLinkSOA.Response>(strIdSession, strTransaccion, () =>
            {
                return Data.Transac.Service.Fixed.Fixed.ActivarDesactivarContinueWS(strIdSession, strTransaccion, objRequestContinue);
            });

            return objResponse;
        }
        //FIN: INICIATIVA-871
        
        //INI: INICIATIVA-986
        public static MessageResponseRegistrarProcesoContinue RegistrarActualizarContingencia(MessageRequestRegistrarProcesoContinue objRequest, Tools.Entity.AuditRequest objAuditRequest)
        {
            MessageResponseRegistrarProcesoContinue objResponse = new MessageResponseRegistrarProcesoContinue();

            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<MessageResponseRegistrarProcesoContinue>(objAuditRequest.Session, objAuditRequest.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.Fixed.RegistrarActualizarContingencia(objRequest, objAuditRequest);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objAuditRequest.Session, objAuditRequest.Transaction, ex.Message);
            }

            return objResponse;
        }
        //FIN: INICIATIVA-986
    }
}
