using System;
using System.Collections.Generic;
using Claro.SIACU.Transac.Service;
using EntitiesFixed = Claro.SIACU.Entity.Transac.Service.Fixed;
using KEY = Claro.ConfigurationManager;
using ConstantsHFC = Claro.SIACU.Transac.Service.Constants;

namespace Claro.SIACU.Business.Transac.Service.Fixed
{
    public class AdditionalServices
    {
        public static EntitiesFixed.GetCommercialServices.CommercialServicesResponse GetCommercialServices(EntitiesFixed.GetCommercialServices.CommercialServicesRequest objRequest)
        {
            var objResponse = new EntitiesFixed.GetCommercialServices.CommercialServicesResponse();
            
            try
            {
                var lstCommercialService = Claro.Web.Logging.ExecuteMethod<List<EntitiesFixed.CommercialService>>(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Data.Transac.Service.Fixed.AdditionalServices.GetCommercialServices(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.StrCoId);
                    });

                objResponse.LstCommercialServices = lstCommercialService;
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }

        public static EntitiesFixed.GetCommertialPlan.CommertialPlanResponse GetCommertialPlan(EntitiesFixed.GetCommertialPlan.CommertialPlanRequest objRequest)
        {
            var objResponse = new EntitiesFixed.GetCommertialPlan.CommertialPlanResponse();

            try
            {
                var rCodigoPlan = string.Empty;
                int rintCodigoError = 0;
                var rstrDescripcionError = string.Empty;

                objResponse.rResult = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Data.Transac.Service.Fixed.AdditionalServices.GetCommertialPlan(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.StrCoId, ref rCodigoPlan, ref rintCodigoError, ref rstrDescripcionError);
                    });

                objResponse.rCodigoPlan = rCodigoPlan;
                objResponse.rintCodigoError = rintCodigoError;
                objResponse.rstrDescripcionError = rstrDescripcionError;
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }

        public static EntitiesFixed.GetProductTracDeacServ.ProductTracDeacServResponse GetProductTracDeacServ(EntitiesFixed.GetProductTracDeacServ.ProductTracDeacServRequest objRequest)
        {
            var objResponse = new EntitiesFixed.GetProductTracDeacServ.ProductTracDeacServResponse();

            try
            {

                objResponse.IdProductoMayor = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Data.Transac.Service.Fixed.AdditionalServices.GetProductTracDeacServ(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.vstrIdentificador, objRequest.vstrCoId, objRequest.vod, objRequest.match);
                    });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }

        public static EntitiesFixed.GetCamapaign.CamapaignResponse GetCamapaign(EntitiesFixed.GetCamapaign.CamapaignRequest objRequest)
        {
            var objResponse = new EntitiesFixed.GetCamapaign.CamapaignResponse();

            try
            {
                var lstCommercialService = Claro.Web.Logging.ExecuteMethod<List<EntitiesFixed.GenericItem>>(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Data.Transac.Service.Fixed.AdditionalServices.GetCamapaign(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.Coid, objRequest.Sncode);
                    });

                objResponse.LstCampaign = lstCommercialService;
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }

        public static EntitiesFixed.GetInsertInteractHFC.InsertInteractHFCResponse GetInsertInteractHFC(EntitiesFixed.GetInsertInteractHFC.GetInsertInteractHFCRequest objRequest)
        {
            var objResponse = new EntitiesFixed.GetInsertInteractHFC.InsertInteractHFCResponse();

            try
            {
                var rInteraccionId = string.Empty;
                var rFlagInsercion = string.Empty;
                var rMsgText = string.Empty;

                objResponse.rResult = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.AdditionalServices.GetInsertInteractHFC(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.Interaction, ref rInteraccionId, ref rFlagInsercion, ref rMsgText);
                });

                objResponse.rInteraccionId = rInteraccionId;
                objResponse.rFlagInsercion = rFlagInsercion;
                objResponse.rMsgText = rMsgText;
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }

        public static EntitiesFixed.GetPlanServices.PlanServicesResponse GetPlanServices(EntitiesFixed.GetPlanServices.PlanServicesRequest objRequest)
        {
            var objResponse = new EntitiesFixed.GetPlanServices.PlanServicesResponse();

            try
            {
                var lstEntity = Claro.Web.Logging.ExecuteMethod<List<EntitiesFixed.PlanService>>(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Data.Transac.Service.Fixed.AdditionalServices.GetPlanServices(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.IdPlan, objRequest.TypeProduct);
                    });

                objResponse.LstPlanServices = lstEntity;
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }

        public static EntitiesFixed.GetInfoInteractionTemplate.InfoInteractionTemplateResponse GetInfoInteractionTemplate(EntitiesFixed.GetInfoInteractionTemplate.InfoInteractionTemplateRequest objRequest)
        {
            var objResponse = new EntitiesFixed.GetInfoInteractionTemplate.InfoInteractionTemplateResponse();
            var vFLAG_CONSULTA = string.Empty;
            var vMSG_TEXT = string.Empty;
            try
            {
                var entity = Claro.Web.Logging.ExecuteMethod<EntitiesFixed.InteractionTemplate>(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Data.Transac.Service.Fixed.AdditionalServices.GetInfoInteractionTemplate(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.vInteraccionID, ref vFLAG_CONSULTA, ref vMSG_TEXT);
                    });
                objResponse.vFLAG_CONSULTA = vFLAG_CONSULTA;
                objResponse.vMSG_TEXT = vMSG_TEXT;
                objResponse.InteractionTemplate = entity;
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }

        public static EntitiesFixed.GetCustomer.CustomerResponse GetCustomer(EntitiesFixed.GetCustomer.GetCustomerRequest objRequest)
        {
            var objResponse = new EntitiesFixed.GetCustomer.CustomerResponse();
            var vFlagConsulta = string.Empty;
            var rMsgText = string.Empty;
            try
            { 
                var objEntity = Claro.Web.Logging.ExecuteMethod<EntitiesFixed.Customer>(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Data.Transac.Service.Fixed.Fixed.GetCustomer(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.vPhone, objRequest.vAccount, objRequest.vContactobjid1, objRequest.vFlagReg, ref vFlagConsulta, ref rMsgText);
                    });
                objResponse.rMsgText = rMsgText;
                objResponse.vFlagConsulta = vFlagConsulta;
                objResponse.Customer = objEntity;
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;

        }

        public static EntitiesFixed.GetInsertInteractionMixed.GetInsertInteractionMixedResponse GetInsertInteractionMixed(EntitiesFixed.GetInsertInteractionMixed.GetInsertInteractionMixedRequest objRequest)
        {
            var objGetInsertInteractionMixedResponse = new EntitiesFixed.GetInsertInteractionMixed.GetInsertInteractionMixedResponse();
            var ContingenciaClarify = KEY.AppSettings("gConstContingenciaClarify");
            var strMsg1 = string.Empty;
            var strMsg2 = string.Empty;
            string strTelefono;
            var rInteraccionId = string.Empty;
            var rFlagInsercion = string.Empty;
            var rMsgText = string.Empty;
            bool resultado;

            if (objRequest.vNroTelefono == objRequest.Interaction.TELEFONO)
                strTelefono = objRequest.vNroTelefono;
            else
                strTelefono = objRequest.Interaction.TELEFONO;

            var objRequestCustomer = new EntitiesFixed.GetCustomer.GetCustomerRequest
            {
                vPhone = strTelefono,
                vAccount = string.Empty,
                vContactobjid1 = objRequest.Interaction.OBJID_CONTACTO,
                vFlagReg = SIACU.Transac.Service.Constants.strUno
            };

            var objResponseCustomer = GetCustomer(objRequestCustomer);

            if (objResponseCustomer != null)
            {
                objRequest.Interaction.OBJID_CONTACTO = objResponseCustomer.Customer.OBJID_CONTACTO;
                objRequest.Interaction.OBJID_SITE = objResponseCustomer.Customer.OBJID_SITE;
                strMsg1 = objResponseCustomer.vFlagConsulta;
                strMsg2 = objResponseCustomer.rMsgText;
            }

            if (ContingenciaClarify != ConstantsHFC.blcasosVariableSI)
            {
                var objInsertInteractHFCRequest = new EntitiesFixed.GetInsertInteractHFC.GetInsertInteractHFCRequest
                {
                    Interaction = objRequest.Interaction
                };
                var objInsertInteractHFCResponse = GetInsertInteractHFC(objInsertInteractHFCRequest);
                resultado = objInsertInteractHFCResponse.rResult;
                rInteraccionId = objInsertInteractHFCResponse.rInteraccionId;
                rFlagInsercion = objInsertInteractHFCResponse.rFlagInsercion;
                rMsgText = objInsertInteractHFCResponse.rMsgText;
            }
            else
            {
                var objInsertInteractionRequest = new EntitiesFixed.GetInsertInteraction.GetInsertInteractionRequest()
                {
                    Interaction = objRequest.Interaction
                };
                var objInsertInteractionResponse = GetInsertInteraction(objInsertInteractionRequest);
                resultado = objInsertInteractionResponse.rResult;
                rInteraccionId = objInsertInteractionResponse.rInteraccionId;
                rFlagInsercion = objInsertInteractionResponse.rFlagInsercion;
                rMsgText = objInsertInteractionResponse.rMsgText;
            }

            if (rInteraccionId != string.Empty)
            {
                if (objRequest.InteractionTemplate != null)
                {
                    var objGetInsertInteractionTemplateRequest = new EntitiesFixed.GetInsertInteractionTemplate.GetInsertInteractionTemplateRequest
                    {
                        InteractionTemplate = objRequest.InteractionTemplate,
                        vInteraccionId = rInteraccionId,
                        vNroTelefono = objRequest.vNroTelefono,
                        vUSUARIO_SISTEMA = objRequest.vUSUARIO_SISTEMA,
                        vUSUARIO_APLICACION = objRequest.vUSUARIO_APLICACION,
                        vPASSWORD_USUARIO = objRequest.vPASSWORD_USUARIO,
                        vEjecutarTransaccion = objRequest.vEjecutarTransaccion
                    };

                    var objGetInsertInteractionTemplateResponse = GetInsertInteractionTemplateResponse(objGetInsertInteractionTemplateRequest);
                }
            }
            objGetInsertInteractionMixedResponse.rResult = resultado;

            return objGetInsertInteractionMixedResponse;

        }

        public static EntitiesFixed.GetInsertInteraction.InsertInteractionResponse GetInsertInteraction(EntitiesFixed.GetInsertInteraction.GetInsertInteractionRequest objRequest)
        {
            var objResponse = new EntitiesFixed.GetInsertInteraction.InsertInteractionResponse();

            try
            {
                var rInteraccionId = string.Empty;
                var rFlagInsercion = string.Empty;
                var rMsgText = string.Empty;

                objResponse.rResult = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.AdditionalServices.GetInsertInteraction(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.Interaction, ref rInteraccionId, ref rFlagInsercion, ref rMsgText);
                });

                objResponse.rInteraccionId = rInteraccionId;
                objResponse.rFlagInsercion = rFlagInsercion;
                objResponse.rMsgText = rMsgText;
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }

        public static EntitiesFixed.GetInsertInteractionTemplate.GetInsertInteractionTemplateResponse GetInsertInteractionTemplateResponse(EntitiesFixed.GetInsertInteractionTemplate.GetInsertInteractionTemplateRequest objRequest)
        {
            var objResponse = new EntitiesFixed.GetInsertInteractionTemplate.GetInsertInteractionTemplateResponse();
            bool resultadoPlantilla = false;
            string strTransaccion = Functions.CheckStr(objRequest.InteractionTemplate.NOMBRE_TRANSACCION);
            string ContingenciaClarify = KEY.AppSettings("gConstContingenciaClarify");
            
            if (ContingenciaClarify != ConstantsHFC.blcasosVariableSI)
            {
                var rFlagInsercion = string.Empty;
                var rMsgText = string.Empty;
                objResponse.rResult = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.AdditionalServices.GetInsertInteractionTemplate(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.InteractionTemplate, objRequest.vInteraccionId, ref rFlagInsercion, ref rMsgText);
                });
                objResponse.rFlagInsercion = rFlagInsercion;
                objResponse.rMsgText = rMsgText;
                resultadoPlantilla = objResponse.rResult;
            }
            else
            {
                var rFlagInsercion = string.Empty;
                var rMsgText = string.Empty;
                objResponse.rResult = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.AdditionalServices.GetInsertInteractionTemplateConti(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.InteractionTemplate, objRequest.vInteraccionId, ref rFlagInsercion, ref rMsgText);
                });
                objResponse.rFlagInsercion = rFlagInsercion;
                objResponse.rMsgText = rMsgText;
                resultadoPlantilla = objResponse.rResult;
            }

            objRequest.InteractionTemplate.ID_INTERACCION = objRequest.vInteraccionId;
            objResponse.rResult = true;
            if (strTransaccion != string.Empty && objRequest.vEjecutarTransaccion)
            {
            }
            else
            {
                objResponse.rCodigoRetornoTransaccion = ConstantsHFC.strCero;
                objResponse.rMensajeErrorTransaccion = String.Empty;
            }

            return objResponse;
        }

        public static EntitiesFixed.GetPlanServices.PlanServicesResponse GetPlanServicesLte(EntitiesFixed.GetPlanServices.PlanServicesRequest objRequest)
        {
            var objResponse = new EntitiesFixed.GetPlanServices.PlanServicesResponse();

            try
            {
                var lstEntity = Claro.Web.Logging.ExecuteMethod<List<EntitiesFixed.PlanService>>(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Data.Transac.Service.Fixed.AdditionalServices.GetPlanServicesLte(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.IdPlan, objRequest.CodeProduct);
                    });

                objResponse.LstPlanServices = lstEntity;
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }

        public static EntitiesFixed.GetActivationDesactivation.ActivationDesactivationResponse GetActivationDesactivation(EntitiesFixed.GetActivationDesactivation.ActivationDesactivationRequest objRequest)
        {
            EntitiesFixed.GetActivationDesactivation.ActivationDesactivationResponse objResponse = new EntitiesFixed.GetActivationDesactivation.ActivationDesactivationResponse();
            try
            {

                objResponse = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Claro.SIACU.Data.Transac.Service.Fixed.AdditionalServices.GetActivationDesactivation(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest);

                    });

                return objResponse;
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }


        public static EntitiesFixed.GetComServiceActivation.ComServiceActivationResponse GetComServiceActivation(EntitiesFixed.GetComServiceActivation.ComServiceActivationRequest objrequest) {

            EntitiesFixed.GetComServiceActivation.ComServiceActivationResponse objResponse = new EntitiesFixed.GetComServiceActivation.ComServiceActivationResponse();
            try
            {

                objResponse = Web.Logging.ExecuteMethod(objrequest.Audit.Session, objrequest.Audit.Transaction,
                    () =>
                    {
                        return Claro.SIACU.Data.Transac.Service.Fixed.AdditionalServices.GetComServiceActivation(objrequest);

                    });

                return objResponse;
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objrequest.Audit.Session, objrequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }

        public static EntitiesFixed.GetComServiceActivation.ComServiceActivationResponse GetComServiceDesactivation(EntitiesFixed.GetComServiceActivation.ComServiceActivationRequest objrequest)
        {

            EntitiesFixed.GetComServiceActivation.ComServiceActivationResponse objResponse = new EntitiesFixed.GetComServiceActivation.ComServiceActivationResponse();
            try
            {

                objResponse = Web.Logging.ExecuteMethod(objrequest.Audit.Session, objrequest.Audit.Transaction,
                    () =>
                    {
                        return Claro.SIACU.Data.Transac.Service.Fixed.AdditionalServices.GetComServiceDesactivation(objrequest);

                    });

                return objResponse;
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objrequest.Audit.Session, objrequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }

        public static EntitiesFixed.GetValidateActDesService.ValidateActDesServiceResponse GetValidateActDesService(EntitiesFixed.GetValidateActDesService.ValidateActDesServiceRequest objRequest)
        {
            EntitiesFixed.GetValidateActDesService.ValidateActDesServiceResponse objResponse = new EntitiesFixed.GetValidateActDesService.ValidateActDesServiceResponse();
            try
            {

                objResponse = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Claro.SIACU.Data.Transac.Service.Fixed.AdditionalServices.GetValidateActDesService(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest);

                    });

                return objResponse;
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }

        public static EntitiesFixed.GetPlanCommercial.PlanCommercialResponse GetPlanCommercial(EntitiesFixed.GetPlanCommercial.PlanCommercialRequest objRequest)
        {
            EntitiesFixed.GetPlanCommercial.PlanCommercialResponse objResponse = new EntitiesFixed.GetPlanCommercial.PlanCommercialResponse();
            try
            {

                objResponse = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Claro.SIACU.Data.Transac.Service.Fixed.AdditionalServices.GetPlanCommercial(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest);

                    });

                return objResponse;
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }

        public static EntitiesFixed.GetProductTracDeacServ.ProductTracDeacServResponse GetProdIdTraDesacServ(EntitiesFixed.GetProductTracDeacServ.ProductTracDeacServRequest objRequest)
        {
           
            EntitiesFixed.GetProductTracDeacServ.ProductTracDeacServResponse oProductTracDeacServResponse = new EntitiesFixed.GetProductTracDeacServ.ProductTracDeacServResponse();
            try
            {
                oProductTracDeacServResponse.IdProductoMayor = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Claro.SIACU.Data.Transac.Service.Fixed.AdditionalServices.GetProdIdTraDesacServ(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.vstrIdentificador,objRequest.vstrCoId,objRequest.vod,objRequest.match);

                    });

                return oProductTracDeacServResponse;
            }
            catch (Exception ex)
            {
               Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return oProductTracDeacServResponse;
        }

    }
}
