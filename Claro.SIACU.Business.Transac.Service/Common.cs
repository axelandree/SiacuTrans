using Claro.SIACU.Entity.Transac.Service.Common;
using Claro.SIACU.Data.Transac.Service.Configuration;
using System;
using System.Net.Mail;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Constant = Claro.SIACU.Transac.Service;
using Datainfreatructure = Claro.SIACU.Data.Transac.Service;
using networkconfig = Claro.SIACU.Data.Transac.Service.Configuration;
using KEY = Claro.ConfigurationManager;
using COMMON = Claro.SIACU.Entity.Transac.Service.Common;
using PREPAID = Claro.SIACU.Entity.Transac.Service.Prepaid;
using PROSPAID = Claro.SIACU.Entity.Transac.Service.Postpaid;
using ClaroService = Claro.SIACU.Transac.Service;
using Claro.Data;
using System.Threading;
using Claro.SIACU.Entity.Transac.Service.Common.GetContractByPhoneNumber;
using Claro.SIACU.Entity.Transac.Service.Common.GetRegisterServiceCommercial;
using Claro.SIACU.Entity.Transac.Service.Common.GetUpdateInter30;
using Claro.SIACU.Entity.Transac.Service.Common.GetQuestionsAnswerSecurity;
using PostBusinessTransacService = Claro.SIACU.Business.Transac.Service.Postpaid.Postpaid;
using Claro.SIACU.Entity.Transac.Service.Common.GetReadDataUser;
using Claro.SIACU.Entity.Transac.Service.Common.GetUploadDocumentOnBase;
//namespace Claro.SIACU.Entity.Transac.Service.Common.GetRegAudit

namespace Claro.SIACU.Business.Transac.Service
{
    public class Common
    {
        private static List<string> _arrConexionesRed = new List<string>();
        private static System.Timers.Timer tmrController = null;
        private static Thread thrProcess = null;
        private static int intTimeTranscurrido = Claro.Constants.NumberZero;
        private static int intTimeout = Claro.Constants.NumberZero;


       
        public static COMMON.GetMotiveSot.MotiveSotResponse getMotiveSot(COMMON.GetMotiveSot.MotiveSotRequest objMotiveSotRequest)
        {
            var objMotiveSot = new COMMON.GetMotiveSot.MotiveSotResponse
            {
                getMotiveSot = Claro.Web.Logging.ExecuteMethod<List<Claro.SIACU.Entity.Transac.Service.Common.ListItem>>
                (
                    objMotiveSotRequest.Audit.Session,
                    objMotiveSotRequest.Audit.Transaction,
                    () =>
                    {
                        return Data.Transac.Service.Common.GetMotiveSot(objMotiveSotRequest.Audit.Session,
                            objMotiveSotRequest.Audit.Transaction);
                    })
            };
            return objMotiveSot;
        }
        public static bool ValidateSchedule(Claro.SIACU.Entity.Transac.Service.Common.GetSchedule.ScheduleRequest objScheduleRequest)
        {
            return Claro.Web.Logging.ExecuteMethod(
                   objScheduleRequest.Audit.Session,
                   objScheduleRequest.Audit.Transaction,
                   () =>
                   {
                       return Data.Transac.Service.Common.ValidateSchedule(objScheduleRequest.Audit.Session, objScheduleRequest.Audit.Transaction, objScheduleRequest);
                   }
                  );

        }

        

        

        public static COMMON.GetListClientDocumentType.ListClientDocumentTypeResponse GetListClientDocumentType(COMMON.GetListClientDocumentType.ListClientDocumentTypeRequest request)
        {
            COMMON.GetListClientDocumentType.ListClientDocumentTypeResponse objResponse = new COMMON.GetListClientDocumentType.ListClientDocumentTypeResponse()
            {
                ListClientDoctType = Claro.Web.Logging.ExecuteMethod<List<COMMON.ListItem>>(request.Audit.Session, request.Audit.Transaction,
                () => { return Data.Transac.Service.Common.GetListClientDocumentType(); })
            };
            return objResponse;
        }

        public static COMMON.GetParameterData.ParameterDataResponse GetParameterData(COMMON.GetParameterData.ParameterDataRequest request)
        {
            string message = "";
            COMMON.GetParameterData.ParameterDataResponse objResponse = new COMMON.GetParameterData.ParameterDataResponse()
            {
                Parameter = Claro.Web.Logging.ExecuteMethod<COMMON.ParameterData>(request.Audit.Session, request.Audit.Transaction,
                () => { return Data.Transac.Service.Common.GetParameterData(request.Audit.Session, request.Audit.Transaction, request.Name, ref message); })
            };
            objResponse.Message = message;
            return objResponse;
        }

        public static COMMON.GetInsertLogTrx.InsertLogTrxResponse InsertLogTrx(COMMON.GetInsertLogTrx.InsertLogTrxRequest request)
        {
            string flagInsertion = "";
            COMMON.GetInsertLogTrx.InsertLogTrxResponse objResponse = new COMMON.GetInsertLogTrx.InsertLogTrxResponse()
            {
                Exito = Claro.Web.Logging.ExecuteMethod<bool>(request.Audit.Session, request.Audit.Transaction,
                () =>
                {
                    return Data.Transac.Service.Common.InsertLogTrx(request.Aplicacion, request.Transaccion, request.Opcion, request.Accion,
                        request.Phone, request.IdInteraction, request.IdTypification, request.User, request.IPPCClient, request.PCClient, request.IPServer,
                        request.NameServer, request.InputParameters, request.OutpuParameters, ref flagInsertion);
                })
            };
            objResponse.FlagInsertion = flagInsertion;
            return objResponse;
        }

        public static COMMON.GetGenerateConstancy.GenerateConstancyResponse GetGenerateContancyPDF(COMMON.GetGenerateConstancy.GenerateConstancyRequest request)
        {
            string errorMessage = string.Empty;

            COMMON.GetGenerateConstancy.GenerateConstancyResponse objResponse = new COMMON.GetGenerateConstancy.GenerateConstancyResponse()
            {
                Generated = Claro.Web.Logging.ExecuteMethod<bool>(request.Audit.Session, request.Audit.Transaction,
                () =>
                {
                    return Data.Transac.Service.Common.GenerateConstancyPDF(request.Audit.Session, request.Audit.Transaction,
                        request.ParametersGeneratePDFGeneric, ref errorMessage);
                })
            };

            objResponse.ErrorMessage = errorMessage;
 
            return objResponse;
        }

        public static COMMON.GetGenerateConstancy.GenerateConstancyResponse GetConstancyPDFWithOnbase(COMMON.GetGenerateConstancy.GenerateConstancyRequest request)
        {
            string errorMessage = string.Empty;

            COMMON.GetGenerateConstancy.GenerateConstancyResponse objResponse =
                 Claro.Web.Logging.ExecuteMethod<COMMON.GetGenerateConstancy.GenerateConstancyResponse>(request.Audit.Session, request.Audit.Transaction,
                () =>
                {
                    return Data.Transac.Service.Common.GetConstancyPDFWithOnbase(request.Audit.Session, request.Audit.Transaction,
                        request.ParametersGeneratePDFGeneric, ref errorMessage);
                });
            objResponse.ErrorMessage = errorMessage;

            return objResponse;
        }

        public static COMMON.GetUpdateNotes.UpdateNotesResponse UpdateNotes(COMMON.GetUpdateNotes.UpdateNotesRequest request)
        {
            string strFlag = "", strMessage = "";
            COMMON.GetUpdateNotes.UpdateNotesResponse objResponse = new COMMON.GetUpdateNotes.UpdateNotesResponse();
            Claro.Web.Logging.ExecuteMethod<bool>(request.Audit.Session, request.Audit.Transaction, () =>
            {
                return Data.Transac.Service.Common.UpdateNotes(request.Audit.Session, request.Audit.Transaction, request.StrObjId, request.StrText, request.StrOrder, out strFlag, out strMessage);
            });
            objResponse.Message = strMessage;
            objResponse.Flag = strFlag;
            return objResponse;
        }
        

        /// <summary>
        /// Método para obtener los reglas comerciales.
        /// </summary>
        /// <param name="objBusinessRulesRequest">Contiene el código de sub clase.</param>
        /// <returns>Devuelve objeto BusinessRulesResponse con información de las reglas de negocio.</returns>
        public static COMMON.GetBusinessRules.BusinessRulesResponse GetBusinessRules(COMMON.GetBusinessRules.BusinessRulesRequest objBusinessRulesRequest)
        {
            COMMON.GetBusinessRules.BusinessRulesResponse objBusinessRulesResponse = new COMMON.GetBusinessRules.BusinessRulesResponse();
           try 
            {
                objBusinessRulesResponse.ListBusinessRules = Claro.Web.Logging.ExecuteMethod<List<COMMON.BusinessRules>>(objBusinessRulesRequest.Audit.Session, objBusinessRulesRequest.Audit.Transaction, () => { return Data.Transac.Service.Common.GetBusinessRules(objBusinessRulesRequest.Audit.Session, objBusinessRulesRequest.Audit.Transaction, objBusinessRulesRequest.SUB_CLASE); });
	        }
	        catch (Exception ex)
	        {
                Claro.Web.Logging.Error(objBusinessRulesRequest.Audit.Session,objBusinessRulesRequest.Audit.Transaction, ex.InnerException.Message);
	        }

            return objBusinessRulesResponse;
        }

        /// <summary>
        /// Método para obtener las regiones.
        /// </summary>
        /// <param name="objRegionRequest">Contiene los datos de auditoria.</param>
        /// <returns>Devuelve objeto RegionResponse con información de las regiones.</returns>
        public static COMMON.GetRegion.RegionResponse GetRegions(COMMON.GetRegion.RegionRequest objRegionRequest)
        {
            COMMON.GetRegion.RegionResponse objRegionResponse = new COMMON.GetRegion.RegionResponse()
            {
                ListRegion = Claro.Web.Logging.ExecuteMethod<List<COMMON.Region>>(objRegionRequest.Audit.Session, objRegionRequest.Audit.Transaction, () => { return Data.Transac.Service.Common.GetRegions(objRegionRequest.Audit.Session, objRegionRequest.Audit.Transaction); })
            };
            return objRegionResponse;
        }

        /// <summary>
        /// Método para obtener tipo de cac o dac.
        /// </summary>
        /// <param name="objCacDacTypeRequest">No contiene información.</param>
        /// <returns>Devuelve objeto CacDacTypeResponse con los tipos de cac o dac.</returns>
        public static COMMON.GetCacDacType.CacDacTypeResponse GetCacDacType(COMMON.GetCacDacType.CacDacTypeRequest objCacDacTypeRequest)
        {
            var objCacDacTypeResponse = new COMMON.GetCacDacType.CacDacTypeResponse
            {
                CacDacTypes = Claro.Web.Logging.ExecuteMethod<List<Claro.SIACU.Entity.Transac.Service.Common.ListItem>>
                (objCacDacTypeRequest.Audit.Session,
                    objCacDacTypeRequest.Audit.Transaction,
                    () =>
                    {
                        return Data.Transac.Service.Common.GetCacDacType(objCacDacTypeRequest.Audit.Session,
                            objCacDacTypeRequest.Audit.Transaction,
                            Data.Transac.Service.Common.GetCodeList(objCacDacTypeRequest.Audit.Session,
                                objCacDacTypeRequest.Audit.Transaction,
                                Claro.SIACU.Constants.DAC));
                    })
            };

            return objCacDacTypeResponse;
        }

        public static COMMON.GetValueXml.ValueXmlResponse GetValueXml(COMMON.GetValueXml.ValueXmlRequest objRequest)
        {
            var objResponse = new COMMON.GetValueXml.ValueXmlResponse
            {
                ValueFromXml = Web.Logging.ExecuteMethod
                (objRequest.Audit.Session,
                    objRequest.Audit.Transaction,
                    () =>
                    {
                        return Data.Transac.Service.Common.GetValueXml(objRequest.Audit.Session,
                            objRequest.Audit.Transaction, objRequest.FileName, objRequest.Clave);
                    })
            };

            return objResponse;
        }

        /// <summary>
        /// Método para obtener el tipo cliente
        /// </summary>
        /// <param name="GetListValueXML"></param>
        /// <returns>Devuelve objeto ListItemResponse.</returns>
        public static COMMON.GetListItem.ListItemResponse GetListValueXML(COMMON.GetListItem.ListItemRequest objListItemRequest)
        {
            COMMON.GetListItem.ListItemResponse objListItemResponse = new COMMON.GetListItem.ListItemResponse()
            {
                lstListItem = Claro.Web.Logging.ExecuteMethod<List<COMMON.ListItem>>(objListItemRequest.Audit.Session,
            objListItemRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Common.GetListValuesXML(objListItemRequest.Audit.Session,
                        objListItemRequest.Audit.Transaction, objListItemRequest.strNameFunction, objListItemRequest.strFlagCode, objListItemRequest.fileName);
                })
            };

            return objListItemResponse;
        }

        public static COMMON.GetTypification.TypificationResponse GetTypification(COMMON.GetTypification.TypificationRequest objTypificationRequest)
        {
            COMMON.GetTypification.TypificationResponse objTypificationResponse = new COMMON.GetTypification.TypificationResponse()
            {
                ListTypification = Claro.Web.Logging.ExecuteMethod<List<COMMON.Typification>>(objTypificationRequest.Audit.Session, objTypificationRequest.Audit.Transaction, () => { return Data.Transac.Service.Common.GetTypification(objTypificationRequest.Audit.Session, objTypificationRequest.Audit.Transaction, objTypificationRequest.TRANSACTION_NAME); })
            };

            return objTypificationResponse;
        }

        public static COMMON.GetReceipts.ReceiptsResponse GetReceipts(COMMON.GetReceipts.ReceiptsRequest objRequest)
        {
            var objResponse = new COMMON.GetReceipts.ReceiptsResponse();
            try
            {
                var rMsgText = string.Empty;

                objResponse.LstReceipts = Web.Logging.ExecuteMethod<List<Claro.SIACU.Entity.Transac.Service.Fixed.GenericItem>>(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Data.Transac.Service.Common.GetReceipts(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.vCODCLIENTE, ref rMsgText);
                    });

                objResponse.MSG_ERROR = rMsgText;
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }




        public static COMMON.GetCivilStatus.CivilStatusResponse GetCivilStatus(COMMON.GetCivilStatus.CivilStatusRequest objRequest)
        {
            COMMON.GetCivilStatus.CivilStatusResponse objResponse = new COMMON.GetCivilStatus.CivilStatusResponse()
            {
                ListCivilStatus = Claro.Web.Logging.ExecuteMethod<List<COMMON.ListItem>>(objRequest.Audit.Session, objRequest.Audit.Transaction, () => { return Data.Transac.Service.Common.GetCivilStatus(objRequest.Audit.Session, objRequest.Audit.Transaction); })

            };
            return objResponse;
        }

        public static COMMON.GetOccupationClient.OccupationClientResponse GetOccupationClient(COMMON.GetOccupationClient.OccupationClientRequest objRequest)
        {
            COMMON.GetOccupationClient.OccupationClientResponse objResponse = new COMMON.GetOccupationClient.OccupationClientResponse()
            {
                ListOccupationClient = Claro.Web.Logging.ExecuteMethod<List<COMMON.ListItem>>(objRequest.Audit.Session, objRequest.Audit.Transaction, () => { return Data.Transac.Service.Common.GetOccupationClient(objRequest.Audit.Session, objRequest.Audit.Transaction); })
            };
            return objResponse;
        }

        public static COMMON.GetReasonRegistry.ReasonRegistryResponse GetReasonRegistry(COMMON.GetReasonRegistry.ReasonRegistryRequest objRequest)
        {
            COMMON.GetReasonRegistry.ReasonRegistryResponse objResponse = new COMMON.GetReasonRegistry.ReasonRegistryResponse()
            {
                ListReasonRegistry = Claro.Web.Logging.ExecuteMethod<List<COMMON.ListItem>>(objRequest.Audit.Session, objRequest.Audit.Transaction, () => { return Data.Transac.Service.Common.GetReasonRegistry(objRequest.Audit.Session, objRequest.Audit.Transaction); })
            };
            return objResponse;
        }

        public static COMMON.GetBrand.BrandResponse GetBrandList(COMMON.GetBrand.BrandRequest objRequest)
        {
            COMMON.GetBrand.BrandResponse objResponse = new COMMON.GetBrand.BrandResponse()
            {
                ListBrand = Claro.Web.Logging.ExecuteMethod<List<COMMON.ListItem>>(objRequest.Audit.Session, objRequest.Audit.Transaction, () => { return Data.Transac.Service.Common.GetBrandList(objRequest.Audit.Session, objRequest.Audit.Transaction); })
            };
            return objResponse;
        }

        public static COMMON.GetConsultDepartment.ConsultDepartmentResponse GetConsultDepartment(COMMON.GetConsultDepartment.ConsultDepartmentRequest objRequest)
        {
            COMMON.GetConsultDepartment.ConsultDepartmentResponse objResponse = new COMMON.GetConsultDepartment.ConsultDepartmentResponse()
            {
                ListConsultDepartment = Claro.Web.Logging.ExecuteMethod<List<COMMON.ConsultDepartment>>(objRequest.Audit.Session, objRequest.Audit.Transaction, () => { return Data.Transac.Service.Common.GetConsultDepartment(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.CodRegion, objRequest.CodDepartment, objRequest.CodState); })
            };
            return objResponse;
        }

        public static COMMON.GetBrandModel.BrandModelResponse GetBrandModel(COMMON.GetBrandModel.BrandModelRequest objRequest)
        {
            COMMON.GetBrandModel.BrandModelResponse objResponse = new COMMON.GetBrandModel.BrandModelResponse()
            {
                ListBrandModel = Claro.Web.Logging.ExecuteMethod<List<COMMON.ListItem>>(objRequest.Audit.Session, objRequest.Audit.Transaction, () => { return Data.Transac.Service.Common.GetBrandModel(objRequest.Audit.Session, objRequest.Audit.Transaction); })
            };
            return objResponse;
        }

        public static COMMON.GetConsultProvince.ConsultProvinceResponse GetConsultProvince(COMMON.GetConsultProvince.ConsultProvinceRequest objRequest)
        {
            COMMON.GetConsultProvince.ConsultProvinceResponse objResponse = new COMMON.GetConsultProvince.ConsultProvinceResponse()
            {
                ListConsultProvince = Claro.Web.Logging.ExecuteMethod<List<COMMON.ConsultProvince>>(objRequest.Audit.Session, objRequest.Audit.Transaction, () => { return Data.Transac.Service.Common.GetConsultProvince(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.CodDepartment, objRequest.CodProvince, objRequest.CodState); })
            };
            return objResponse;
        }

        public static COMMON.GetConsultDistrict.ConsultDistrictResponse GetConsultDistrict(COMMON.GetConsultDistrict.ConsultDistrictRequest objRequest)
        {
            COMMON.GetConsultDistrict.ConsultDistrictResponse objResponse = new COMMON.GetConsultDistrict.ConsultDistrictResponse()
            {
                ListConsultDistrict = Claro.Web.Logging.ExecuteMethod<List<COMMON.ConsultDistrict>>(objRequest.Audit.Session, objRequest.Audit.Transaction, () => { return Data.Transac.Service.Common.GetConsultDistrict(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.CodProvince, objRequest.CodDistrict, objRequest.CodState); })
            };
            return objResponse;
        }

        public static COMMON.GetConsultNationality.ConsultNationalityResponse GetConsultNationality(COMMON.GetConsultNationality.ConsultNationalityRequest objRequest)
        {
            COMMON.GetConsultNationality.ConsultNationalityResponse objResponse = new COMMON.GetConsultNationality.ConsultNationalityResponse()
            {
                ListConsultNationality = Claro.Web.Logging.ExecuteMethod<List<COMMON.ListItem>>(objRequest.Audit.Session, objRequest.Audit.Transaction, () => { return Data.Transac.Service.Common.GetConsultNationality(objRequest.Audit.Session, objRequest.Audit.Transaction); })
            };
            return objResponse;
        }

        public static COMMON.GetServicesVAS.ServicesVASResponse GetServicesVAS(COMMON.GetServicesVAS.ServicesVASRequest objRequest)
        {
            COMMON.GetServicesVAS.ServicesVASResponse objResponse = new COMMON.GetServicesVAS.ServicesVASResponse()
            {
                ListServicesVAS = Claro.Web.Logging.ExecuteMethod<List<COMMON.ListItem>>(objRequest.Audit.Session, objRequest.Audit.Transaction, () => { return Data.Transac.Service.Common.GetServicesVAS(objRequest.Audit.Session, objRequest.Audit.Transaction); })
            };
            return objResponse;
        }

        public static COMMON.GetMigratedElements.MigratedElementsResponse GetMigratedElements(COMMON.GetMigratedElements.MigratedElementsRequest objRequest)
        {
            COMMON.GetMigratedElements.MigratedElementsResponse objResponse = new COMMON.GetMigratedElements.MigratedElementsResponse()
            {
                ListMigratedElements = Claro.Web.Logging.ExecuteMethod<List<COMMON.ListItem>>(objRequest.Audit.Session, objRequest.Audit.Transaction, () => { return Data.Transac.Service.Common.GetMigratedElements(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.Id); })
            };
            return objResponse;
        }

        public static COMMON.GetMigratedElements.MigratedElementsResponse GetMigratedElements2(COMMON.GetMigratedElements.MigratedElementsRequest objRequest)
        {
            objRequest.Id = Claro.Web.Logging.ExecuteMethod<int>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
            {
                return Data.Transac.Service.Common.GetCodeList(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.NameFunction);
            });
            COMMON.GetMigratedElements.MigratedElementsResponse objResponse = new COMMON.GetMigratedElements.MigratedElementsResponse()
            {
                ListMigratedElements = Claro.Web.Logging.ExecuteMethod<List<COMMON.ListItem>>(objRequest.Audit.Session, objRequest.Audit.Transaction, () => { return Data.Transac.Service.Common.GetMigratedElements(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.Id); })
            };
            return objResponse;
        }


        #region interacciones 

        //obtener interacciones de Cliente
        //nombre original ObtenerInteraccionesCliente
        /// <summary>
        /// Método para obtener los datos de la interaccion del clientne .
        /// </summary>
        /// <param name="objInteractionRequest"> contiene los datos de la uditoria.</param>
        /// <returns>Devuelve objeto CacDacTypeResponse con los tipos de cac o dac.</returns>
        public static COMMON.GetInteractionClient.InteractionClientResponse GetInteractionClient(COMMON.GetInteractionClient.InteractionClientRequest objInteractionRequest)
        {
            string msgText = "";
            string flagcretion = "";
            COMMON.GetInteractionClient.InteractionClientResponse objInteractionResponse =
                    new COMMON.GetInteractionClient.InteractionClientResponse()
                    {
                        ListInteractionClient = Claro.Web.Logging.ExecuteMethod<List<COMMON.Iteraction>>(objInteractionRequest.Audit.Session, objInteractionRequest.Audit.Transaction,
                        () =>
                        {
                            return Claro.SIACU.Data.Transac.Service.Common.GetIteractionClient(objInteractionRequest.Audit.Session, objInteractionRequest.Audit.Transaction, objInteractionRequest.straccount, objInteractionRequest.strtelephone, objInteractionRequest.intcontactobjid1, objInteractionRequest.intsiteobjid1, objInteractionRequest.strtipification, objInteractionRequest.intnrorecordshow, out flagcretion, out msgText);
                        })
                    };
            objInteractionResponse.Flagcreation = flagcretion;
            objInteractionResponse.Msgtext = msgText;
            return objInteractionResponse;
        }

        //registrar detalle interacciones de Cliente
        //nombre original InsSubClaseDet


        public static COMMON.GetInteractionSubClasDetail.InteractionSubClasDetailResponse GetInteractionSubClaseDetail(COMMON.GetInteractionSubClasDetail.InteractionSubClasDetailRequest objresquest)
        {
            int codeError = 0;
            string msgError = "";
            COMMON.GetInteractionSubClasDetail.InteractionSubClasDetailResponse objresponse =
                    new COMMON.GetInteractionSubClasDetail.InteractionSubClasDetailResponse()
                    {
                        ProcesSucess = Claro.Web.Logging.ExecuteMethod<bool>(objresquest.Audit.Session, objresquest.Audit.Transaction,
                        () =>
                        {
                            return Claro.SIACU.Data.Transac.Service.Common.InsertRecordSubClaseDetail(objresquest.Audit.Session, objresquest.Audit.Transaction, objresquest.item, out codeError, out msgError);
                        })

                    };
            objresponse.CodeError = codeError;
            objresponse.MsgError = msgError;
            return objresponse;
        }

        //registra plantilla interaccion
        //NOMBRE ORIGINAL insertaPlantillainteraccion

        public static COMMON.GetInsertTemplateInteraction.InsertTemplateInteractionResponse GetInsertInteractionTemplate(COMMON.GetInsertTemplateInteraction.InsertTemplateInteractionRequest objrequest)
        {
            string flagInsercion = "";
            string msgText = "";
            COMMON.GetInsertTemplateInteraction.InsertTemplateInteractionResponse objresponse =
             new COMMON.GetInsertTemplateInteraction.InsertTemplateInteractionResponse()
               {
                   ProcesSucess = Claro.Web.Logging.ExecuteMethod<bool>(objrequest.Audit.Session, objrequest.Audit.Transaction,
                   () =>
                   {
                       return Claro.SIACU.Data.Transac.Service.Common.RegistrationInsertTemplateInteraction(objrequest.Audit.Session, objrequest.Audit.Transaction, objrequest.item, objrequest.IdInteraction, out flagInsercion, out msgText);

                   })
               };
            objresponse.FlagInsercion = flagInsercion;
            objresponse.MsgText = msgText;
            return objresponse;

        }

        //inserta plantilla interaccion
        //similar a la funcion de arriba pero se usa diferente  variables y sp 
        //NOMBRE ORIGINAL insertaPlantillainteraccion
        public static COMMON.GetInsTemplateInteraction.InsTemplateInteractionResponse GetInsInteractionTemplate(COMMON.GetInsTemplateInteraction.InsTemplateInteractionRequest objrequest)
        {
            string flagInsercion = "";
            string msgText = "";
            COMMON.GetInsTemplateInteraction.InsTemplateInteractionResponse objresponse =
                new COMMON.GetInsTemplateInteraction.InsTemplateInteractionResponse()

                {
                    ProcessSucess = Claro.Web.Logging.ExecuteMethod<bool>(objrequest.Audit.Session, objrequest.Audit.Transaction,
                    () =>
                    {
                        return Claro.SIACU.Data.Transac.Service.Common.InsTemplateInteraction(objrequest.Audit.Session, objrequest.Audit.Transaction, objrequest.item, objrequest.IdInteraction, out flagInsercion, out msgText);

                    })
                };
            objresponse.FlagInsercion = flagInsercion;
            objresponse.MsgText = msgText;
            return objresponse;


        }

        //insertar interaccion
        //la funcion original se llama insertarinteraccion
        public static COMMON.GetInsertInteract.InsertInteractResponse GetInsertInteract(COMMON.GetInsertInteract.InsertInteractRequest objrequest)
        {
            Claro.Web.Logging.Info("Session: 270492", "Transaction: Entra a GetInsertInteract", "");

            string interactionid = "";
            string flagInsercio = "";
            string msgText = "";
            COMMON.GetInsertInteract.InsertInteractResponse objresponse =
            new COMMON.GetInsertInteract.InsertInteractResponse()
            {
                ProcesSucess = Claro.Web.Logging.ExecuteMethod<bool>(objrequest.Audit.Session, objrequest.Audit.Transaction,
                  () =>
                  {
                      return Claro.SIACU.Data.Transac.Service.Common.InsertInteraction(objrequest.Audit.Session, objrequest.Audit.Transaction, objrequest.item, out interactionid, out flagInsercio, out  msgText);

                  })

            };
            objresponse.Interactionid = interactionid;
            objresponse.FlagInsercion = flagInsercio;
            objresponse.MsgText = msgText;

            Claro.Web.Logging.Info("Session: 270492", "Transaction: sale de GetInsertInteract", "objresponse.Interactionid: " + objresponse.Interactionid + "objresponse.FlagInsercion: " + objresponse.FlagInsercion + "objresponse.MsgText: " + objresponse.MsgText);

            return objresponse;
        }

        //insertar interacion
        //lafuncion original se llama insertar
        public static COMMON.GetInsertInt.InsertIntResponse GetInsertInt(COMMON.GetInsertInt.InsertIntRequest objrequest)
        {
            Claro.Web.Logging.Info("Session: 270492", "Transaction: Entra a GetInsertInt","");

            string interactionid = "";
            string flagInsercio = "";
            string msgText = "";
            COMMON.GetInsertInt.InsertIntResponse objresponse =
                new COMMON.GetInsertInt.InsertIntResponse()
                {
                    ProcesSucess = Claro.Web.Logging.ExecuteMethod<bool>(objrequest.Audit.Session, objrequest.Audit.Transaction,
                       () =>
                       {
                           return Claro.SIACU.Data.Transac.Service.Common.Insert(objrequest.Audit.Session, objrequest.Audit.Transaction, objrequest.item, out interactionid, out flagInsercio, out msgText);

                       })
                };
            objresponse.Interactionid = interactionid;
            objresponse.FlagInsercion = flagInsercio;
            objresponse.MsgText = msgText;

            Claro.Web.Logging.Info("Session: 270492", "Transaction: sale de GetInsertInt", "Interactionid: " + objresponse.Interactionid + "FlagInsercion: " + objresponse.FlagInsercion + "MsgText: " + objresponse.MsgText);

            return objresponse;
        }


        //obtener cliente
        //obitne la lista de clientes
        //funcion original se llama obtenercliente
        public static COMMON.GetClient.ClientResponse GetObtClient(COMMON.GetClient.ClientRequest objrequest)
        {

            Claro.Web.Logging.Info("Session: 270492", "Transaction: Entra a GetObtClient", "strphone" + objrequest.strphone + "//straccount : " + objrequest.straccount + "// strContactobjid : " + objrequest.strContactobjid + "//strflagreg : " + objrequest.strflagreg);

            string MsgText = "";
            string flagquery = "";
            COMMON.GetClient.ClientResponse objresponse =
                new COMMON.GetClient.ClientResponse()
                {
                    listClient = Claro.Web.Logging.ExecuteMethod<COMMON.Client>(objrequest.Audit.Session, objrequest.Audit.Transaction,
                        //listClient = Claro.Web.Logging.ExecuteMethod<List<COMMON.Client>>(objrequest.Audit.Session, objrequest.Audit.Transaction,

                    () =>
                    {
                        return Claro.SIACU.Data.Transac.Service.Common.GetClient(objrequest.Audit.Session, objrequest.Audit.Transaction, objrequest.strphone, objrequest.straccount, objrequest.strContactobjid, objrequest.strflagreg, out  flagquery, out MsgText);
                    })

                };
            objresponse.Flagquery = flagquery;
            objresponse.MsgText = MsgText;

            Claro.Web.Logging.Info("Session: 270492", "Transaction: sale de GetObtClient", "Flagquery" + objresponse.Flagquery + "//MsgText : " + objresponse.MsgText);

            return objresponse;




        }

        ///funcion insertartinteraccionesnegaocio2

        public static COMMON.GetBusinessInteraction2.BusinessInteraction2Response GetBusinessInteraction2(COMMON.GetBusinessInteraction2.BusinessInteraction2Request request)
        {
            string strInteractionId = "", strFlagInsertion = "", strMessage = "";
            COMMON.GetBusinessInteraction2.BusinessInteraction2Response objResponse =
                new COMMON.GetBusinessInteraction2.BusinessInteraction2Response()
                {
                    ProcessOK = Claro.Web.Logging.ExecuteMethod<bool>(request.Audit.Session, request.Audit.Transaction,
                    () =>
                    {
                        return Claro.SIACU.Data.Transac.Service.Common.InsertBusinessInteraction2(request.Audit.Session, request.Audit.Transaction, request.Item, out strInteractionId,
                            out strFlagInsertion, out strMessage);
                    })
                };
            objResponse.InteractionId = strInteractionId;
            objResponse.FlagInsertion = strFlagInsertion;
            objResponse.MsgText = strMessage;
            return objResponse;
        }



        //funcion insertarplantillainteraccion general

        public static COMMON.GetInsertTemplateGeneral.InsertTemplateGeneralResponse GetInserInteractionTemplateresponse(COMMON.GetInsertTemplateGeneral.InsertTemplateGeneralRequest objrequest)
        {
            Claro.Web.Logging.Info("Session: 270492", "Transaction: Entra a GetInserInteractionTemplateresponse", "");

            var objresponse = new COMMON.GetInsertTemplateGeneral.InsertTemplateGeneralResponse();
            bool resultadoPlantilla = false;
            string strTransaccion = ClaroService.Functions.CheckStr(objrequest.InteractionTemplate._NOMBRE_TRANSACCION);
            string strcontingeciaClarify = KEY.AppSettings("gConstContingenciaClarify");

            if (strcontingeciaClarify != ClaroService.Constants.blcasosVariableSI)
            {
                var flagInssercion = string.Empty;
                var msgtext = string.Empty;
                objresponse.rResult = Web.Logging.ExecuteMethod(objrequest.Audit.Session, objrequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Common.RegistrationInsertTemplateInteraction(objrequest.Audit.Session, objrequest.Audit.Transaction, objrequest.InteractionTemplate, objrequest.vInteraccionId, out flagInssercion, out msgtext);
                });
                objresponse.rFlagInsercion = flagInssercion;
                objresponse.rMsgText = msgtext;
                resultadoPlantilla = objresponse.rResult;


            }
            else
            {
                var flaginsercion = string.Empty;
                var msgtext = string.Empty;
                objresponse.rResult = Web.Logging.ExecuteMethod(objrequest.Audit.Session, objrequest.Audit.Transaction, () =>
                  {
                      return Data.Transac.Service.Common.InsTemplateInteraction(objrequest.Audit.Session, objrequest.Audit.Transaction, objrequest.InteractionTemplate, objrequest.vInteraccionId, out flaginsercion, out msgtext);

                  });
                objresponse.rFlagInsercion = flaginsercion;
                objresponse.rMsgText = msgtext;
                resultadoPlantilla = objresponse.rResult;

            }
            objrequest.InteractionTemplate._ID_INTERACCION = objrequest.vInteraccionId;
            objresponse.rResult = true;
            if (strTransaccion != string.Empty && objrequest.vEjecutarTransaccion)
            {
                objresponse.rCodigoRetornoTransaccion = ClaroService.Constants.strCero;
                objresponse.rMensajeErrorTransaccion = string.Empty;
            }

            Claro.Web.Logging.Info("Session: 270492", "Transaction: sale de GetInserInteractionTemplateresponse", "");


            return objresponse;
        }


        //funcion general insertart,
        //insertartinteracciones negocio

        public static COMMON.GetInsertGeneral.InsertGeneralResponse GetIsertInteractionBusiness(COMMON.GetInsertGeneral.InsertGeneralRequest objrequest)
        {
            var objGetInsertInteractionMixedResponse = new COMMON.GetInsertGeneral.InsertGeneralResponse();
            var ContingenciaClarify = KEY.AppSettings("gConstContingenciaClarify");
            var strMsg1 = string.Empty;
            var strMsg2 = string.Empty;
            var strcoderetorn = string.Empty;
            var strmessagetextTransaccion = string.Empty;
            var rtrmsgTextInteraccion = string.Empty;
            string strTelefono;
            var strFlagInsercionInteraccion = string.Empty;
            var rInteraccionId = string.Empty;
            var rFlagInsercion = string.Empty;
            var rMsgText = string.Empty;
            bool resultado;
            if (objrequest.vNroTelefono == objrequest.Interaction.TELEFONO)
            {

                strTelefono = objrequest.vNroTelefono;
            }
            else
            {
                strTelefono = objrequest.Interaction.TELEFONO;
            }
            var objrequestClient = new COMMON.GetClient.ClientRequest
            {
                strphone = strTelefono,
                straccount = string.Empty,
                strContactobjid = objrequest.Interaction.OBJID_CONTACTO,
                strflagreg = ClaroService.Constants.strUno,
                Audit = objrequest.Audit
            };
            var objreponseClient = GetObtClient(objrequestClient);

            if (objreponseClient != null)
            {
                if (objreponseClient.listClient != null)
                {
                    objrequest.Interaction.OBJID_CONTACTO = objreponseClient.listClient.OBJID_CONTACTO;
                    objrequest.Interaction.OBJID_SITE = objreponseClient.listClient.OBJID_SITE;
                }
                strMsg1 = objreponseClient.Flagquery;
                strMsg2 = objreponseClient.MsgText;

            }
            if (ContingenciaClarify != ClaroService.Constants.blcasosVariableSI)
            {
                var objInsertrequest = new COMMON.GetInsertInt.InsertIntRequest
               {
                   item = objrequest.Interaction,
                   Audit = objrequest.Audit
               };
                var objinsertireeponse = GetInsertInt(objInsertrequest);
                resultado = objinsertireeponse.ProcesSucess;
                rInteraccionId = objinsertireeponse.Interactionid;
                rFlagInsercion = objinsertireeponse.FlagInsercion;
                rMsgText = objinsertireeponse.MsgText;
            }
            else
            {


                var objinserinteractionrequest = new COMMON.GetInsertInteract.InsertInteractRequest()
                {
                    item = objrequest.Interaction,
                    Audit = objrequest.Audit
                };
                var objinsertinteractionresponse = GetInsertInteract(objinserinteractionrequest);
                resultado = objinsertinteractionresponse.ProcesSucess;
                rInteraccionId = objinsertinteractionresponse.Interactionid;
                rMsgText = objinsertinteractionresponse.MsgText;


            }

            if (!String.IsNullOrEmpty(rInteraccionId))
            {
                if (objrequest.InteractionTemplate != null)
                {
                    var objgetinsertinteractiontemplaterequest = new COMMON.GetInsertTemplateGeneral.InsertTemplateGeneralRequest
                {
                    InteractionTemplate = objrequest.InteractionTemplate,
                    vInteraccionId = rInteraccionId,
                    vNroTelefono = objrequest.vNroTelefono,
                    vUSUARIO_SISTEMA = objrequest.vUSUARIO_SISTEMA,
                    vUSUARIO_APLICACION = objrequest.vPASSWORD_USUARIO,
                    vEjecutarTransaccion = objrequest.vEjecutarTransaccion,
                    Audit = objrequest.Audit

                };
                    var objgetinsertinteractiontemplateresponse = GetInserInteractionTemplateresponse(objgetinsertinteractiontemplaterequest);
                    strcoderetorn = objgetinsertinteractiontemplateresponse.rCodigoRetornoTransaccion;
                    strmessagetextTransaccion = objgetinsertinteractiontemplateresponse.rMensajeErrorTransaccion;
                    rtrmsgTextInteraccion = objgetinsertinteractiontemplateresponse.rMsgText;
                    strFlagInsercionInteraccion = objgetinsertinteractiontemplateresponse.rFlagInsercion;
                }

            }


            objGetInsertInteractionMixedResponse.rInteraccionId = rInteraccionId;
            objGetInsertInteractionMixedResponse.rFlagInsercion = rFlagInsercion;

            //cambiadas
            objGetInsertInteractionMixedResponse.rFlagInsercionInteraccion = strFlagInsercionInteraccion;
            objGetInsertInteractionMixedResponse.rMsgTextInteraccion = rtrmsgTextInteraccion;
            objGetInsertInteractionMixedResponse.rCodigoRetornoTransaccion = strcoderetorn;
            objGetInsertInteractionMixedResponse.rMsgTextTransaccion = strmessagetextTransaccion;
            objGetInsertInteractionMixedResponse.rResult = resultado;
            objGetInsertInteractionMixedResponse.rMsgText = rMsgText;

            return objGetInsertInteractionMixedResponse;

        }


        //funcion insertarDetalle
        //insertar detalle de interaccion

        public static COMMON.GetInsertDetail.InsertDetailResponse GetInsertDeatil(COMMON.GetInsertDetail.InsertDetailRequest request)
        {
            string strflag = "";
            COMMON.GetInsertDetail.InsertDetailResponse objresponse =
            new COMMON.GetInsertDetail.InsertDetailResponse()
            {
                ProcessSucces = Claro.Web.Logging.ExecuteMethod<bool>(request.Audit.Session, request.Audit.Transaction,
                () =>
                {
                    return Claro.SIACU.Data.Transac.Service.Common.InsertDetail(request.Audit.Session, request.Audit.Transaction, request.Item, out strflag);
                })

            };
            objresponse.strFlagInsercion = strflag;
            return objresponse;

        }

        #endregion

        public static COMMON.GetUpdateXInter29.UpdateXInter29Response UpdateXInter29(COMMON.GetUpdateXInter29.UpdateXInter29Request objRequest)
        {
            var objResponse = new COMMON.GetUpdateXInter29.UpdateXInter29Response();
            string menssage = string.Empty;
            try
            {

                objResponse.Flag = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Common.UpdateXinter29(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.IdInteract, objRequest.Text, objRequest.Order, out menssage);
                });

                objResponse.Mensaje = menssage;
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }

        #region auditoria fdq
        public static COMMON.GetSaveAudit.SaveAuditResponse SaveAudit(COMMON.GetSaveAudit.SaveAuditRequest objGrabarAuditoria)
        {
            COMMON.GetSaveAudit.SaveAuditResponse objAuditResponse = new COMMON.GetSaveAudit.SaveAuditResponse();

            objAuditResponse = Web.Logging.ExecuteMethod(objGrabarAuditoria.Audit.Session, objGrabarAuditoria.Audit.Transaction,
                () =>
                {
                    return Claro.SIACU.Data.Transac.Service.Common.SaveAudit(objGrabarAuditoria.Audit.Session, objGrabarAuditoria.Audit.Transaction, objGrabarAuditoria.vCuentaUsuario, objGrabarAuditoria.vIpCliente, objGrabarAuditoria.vIpServidor, objGrabarAuditoria.vMonto,
                                                                                        objGrabarAuditoria.vNombreCliente, objGrabarAuditoria.vNombreServidor, objGrabarAuditoria.vServicio, objGrabarAuditoria.vTelefono, objGrabarAuditoria.vTexto,
                                                                                         objGrabarAuditoria.vTransaccion);

                });

            return objAuditResponse;
        }

        public static COMMON.GetSaveAuditM.SaveAuditMResponse SaveAuditM(COMMON.GetSaveAuditM.SaveAuditMRequest objRegistAuditoria)
        {
            COMMON.GetSaveAuditM.SaveAuditMResponse oRegistroAuditoriaResponse = new COMMON.GetSaveAuditM.SaveAuditMResponse();

            oRegistroAuditoriaResponse = Web.Logging.ExecuteMethod(objRegistAuditoria.Audit.Session, objRegistAuditoria.Audit.Transaction,
                   () =>
                   {
                       return Claro.SIACU.Data.Transac.Service.Common.SaveAuditM(objRegistAuditoria);

                   });

            return oRegistroAuditoriaResponse;
        }
        #endregion

        #region jcaa

        public static COMMON.GeneratePDF.GeneratePDFDataResponse GeneratePDF(COMMON.GeneratePDF.GeneratePDFDataRequest RequestParam)
        {
            COMMON.GeneratePDF.GeneratePDFDataResponse serviceResponse = null;
            try
            {
                Claro.Web.Logging.Info("123123123", "1", "Antes de entrar al metodo generateConstancePDF- business ");
                serviceResponse = Claro.Web.Logging.ExecuteMethod<COMMON.GeneratePDF.GeneratePDFDataResponse>(RequestParam.Audit.Session, RequestParam.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.GeneratePDF.generateConstancePDF(RequestParam);
                });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info("123123123", "1", "Error en el business HPXTREAM" + ex.Message);
                Claro.Web.Logging.Error(RequestParam.Audit.Session, RequestParam.Audit.Transaction, ex.Message);



            }

            COMMON.GeneratePDF.GeneratePDFDataResponse Result = new COMMON.GeneratePDF.GeneratePDFDataResponse()
            {
                Generated = serviceResponse.Generated,
                FilePath = serviceResponse.FilePath
            };
            return Result;
        }
        #endregion

        public static Entity.Transac.Service.Common.ETAFlowValidate.ETAFlowResponse ETAFlowValidate(Entity.Transac.Service.Common.ETAFlowValidate.ETAFlowRequest RequestParam)
        {
            Entity.Transac.Service.Common.ETAFlow listServiceResponse = null;
            Entity.Transac.Service.Common.ETAFlow listService = null;
            try
            {
                listService = Claro.Web.Logging.ExecuteMethod<Entity.Transac.Service.Common.ETAFlow>(RequestParam.Audit.Session, RequestParam.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Common.ETAFlowValidate(RequestParam.Audit.Session, RequestParam.Audit.Transaction, RequestParam.as_origen, RequestParam.av_idplano, RequestParam.av_ubigeo, RequestParam.an_tiptra, RequestParam.an_tipsrv);
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
            Entity.Transac.Service.Common.ETAFlowValidate.ETAFlowResponse Resultado = new Entity.Transac.Service.Common.ETAFlowValidate.ETAFlowResponse()
            {
                ETAFlow = listServiceResponse
            };
            return Resultado;
        }


        #region Redireccion
        public static Entity.Transac.Service.Common.GetRedirectSession.RedirectSessionResponse GetRedirectSession(Entity.Transac.Service.Common.GetRedirectSession.RedirectSessionRequest objRedirectSessionRequest)
        {
            Entity.Transac.Service.Common.GetRedirectSession.RedirectSessionResponse objRedirectSessionResponse = null;
            string strErrorMsg = "", strCodError = "";
            try
            {
                objRedirectSessionResponse = new Entity.Transac.Service.Common.GetRedirectSession.RedirectSessionResponse()
                {
                    listRedirect = Claro.Web.Logging.ExecuteMethod<List<Redirect>>(objRedirectSessionRequest.Audit.Session, objRedirectSessionRequest.Audit.Transaction, () => { return Data.Transac.Service.Common.GetRedirectSession(objRedirectSessionRequest.Audit, objRedirectSessionRequest.Application, objRedirectSessionRequest.Option, out  strErrorMsg, out strCodError); }),
                    ErrorMsg = strErrorMsg,
                    CodeError = strCodError
                };
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRedirectSessionRequest.Audit.Session, objRedirectSessionRequest.Audit.Transaction, ex.Message);
                throw new Claro.MessageException(ex.Message.ToString());
            }
            return objRedirectSessionResponse;
        }
        /// <summary>
        /// Método para insertar la información necesaria para realizar redirección utilizando el ValidarCredencialesWS.
        /// </summary>
        /// <param name="objInsertRedirectComRequest">Contiene información necesaria para redireccionar.</param>
        /// <returns>Devuelve objeto InsertRedirectComResponse con información para realizar la redirección externa.</returns>
        public static Entity.Transac.Service.Common.InsertRedirectCommunication.InsertRedirectComResponse InsertRedirectCommunication(Entity.Transac.Service.Common.InsertRedirectCommunication.InsertRedirectComRequest objInsertRedirectComRequest)
        {
            Entity.Transac.Service.Common.InsertRedirectCommunication.InsertRedirectComResponse objInsertRedirectComResponse = null;
            string strSequence = "", strUrl = "";
            try
            {
                objInsertRedirectComResponse = new Entity.Transac.Service.Common.InsertRedirectCommunication.InsertRedirectComResponse()
                {
                    ResultRegCommunication = Claro.Web.Logging.ExecuteMethod<string>(objInsertRedirectComRequest.Audit.Session, objInsertRedirectComRequest.Audit.Transaction, () => { return Data.Transac.Service.Common.InsertRedirectCommunication(objInsertRedirectComRequest.Audit, objInsertRedirectComRequest.AppDest, objInsertRedirectComRequest.Option, objInsertRedirectComRequest.IpClient, objInsertRedirectComRequest.IpServer, objInsertRedirectComRequest.JsonParameters, out strSequence, out strUrl); }),
                    Sequence = strSequence,
                    Url = strUrl
                };
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objInsertRedirectComRequest.Audit.Session, objInsertRedirectComRequest.Audit.Transaction, ex.Message);
                throw new Claro.MessageException(ex.Message.ToString());
            }
            return objInsertRedirectComResponse;
        }

        public static Entity.Transac.Service.Common.GetValidateCommunication.ValidateCommunicationResponse ValidateRedirectCommunication(Entity.Transac.Service.Common.GetValidateCommunication.ValidateCommunicationRequest objValidateRedirectRequest)
        {
            Entity.Transac.Service.Common.GetValidateCommunication.ValidateCommunicationResponse objValidateRedirectResponse = null;
            string strUrlDest = "", strAvailability = "", strErrorMsg = "", strJsonParameters = "";
            try
            {
                Claro.Web.Logging.Info("1234566666", "1234566666", "Business sequence1 " + objValidateRedirectRequest.Sequence);
                objValidateRedirectResponse = new Entity.Transac.Service.Common.GetValidateCommunication.ValidateCommunicationResponse()
                {
                    ResultValCommunication = Claro.Web.Logging.ExecuteMethod<Boolean>(objValidateRedirectRequest.Audit.Session, objValidateRedirectRequest.Audit.Transaction, () => { return Data.Transac.Service.Common.ValidateRedirectCommunication(objValidateRedirectRequest.Audit, objValidateRedirectRequest.Sequence, out strErrorMsg, objValidateRedirectRequest.Server, out strUrlDest, out strAvailability, out strJsonParameters); }),
                    Availability = strAvailability,
                    url = strUrlDest,
                    ErrorMessage = strErrorMsg,
                    JsonString = strJsonParameters
                };
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info("1234566666", "1234566666", "Business ex " + ex + "ex_inner" + ex.InnerException);
                Claro.Web.Logging.Error(objValidateRedirectRequest.Audit.Session, objValidateRedirectRequest.Audit.Transaction, ex.Message);
                throw ex;
            }
            return objValidateRedirectResponse;
        }
        #endregion

        #region ENVIAR EMAIL

        public static COMMON.GetSendEmail.SendEmailResponse GetSendEmail(COMMON.GetSendEmail.SendEmailRequest request)
        {
            COMMON.GetSendEmail.SendEmailResponse objresponse = new COMMON.GetSendEmail.SendEmailResponse();
            string strExit = "";
            MailAddress strFrom = new MailAddress(request.strSender);
            MailAddress strTo = new MailAddress(request.strTo);
            MailMessage strmessage = new MailMessage(strFrom, strTo);
            try
            {
                strmessage.IsBodyHtml = true;
                strmessage.Body = request.strMessage;
                strmessage.BodyEncoding = System.Text.Encoding.UTF8;
                strmessage.Subject = request.strSubject;
                strmessage.SubjectEncoding = System.Text.Encoding.UTF8;
                if (!string.IsNullOrEmpty(request.strCC))
                    strmessage.CC.Add(request.strCC);

                if (!string.IsNullOrEmpty(request.strBCC))
                    strmessage.Bcc.Add(request.strBCC);

                strmessage.Priority = MailPriority.High;

                //CAMBIO PARA ENVIAR CORREO POR ARREGLO DE BYTES


                System.IO.MemoryStream memStream = null;
                System.IO.StreamWriter streamWriter = null;
                Attachment thisAttachment = null;

                if (request.AttachedByte != null)
                {
                    memStream = new System.IO.MemoryStream(request.AttachedByte);
                    streamWriter = new System.IO.StreamWriter(memStream);
                    streamWriter.Flush();
                    memStream.Position = 0;
                    thisAttachment = new Attachment(memStream, "application/pdf");
                    thisAttachment.ContentDisposition.FileName = request.strAttached;
                    strmessage.Attachments.Add(thisAttachment);
                }


                SmtpClient smtpClient = new System.Net.Mail.SmtpClient();
                smtpClient.Host = "LIMMAILF1.tim.com.pe";
                smtpClient.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
                smtpClient.EnableSsl = false;
                smtpClient.Send(strmessage);
                strExit = Constant.Constants.Message_OK;
                smtpClient.Dispose();

            }
            catch (Exception ex)
            {
                strExit = ex.Message.ToString();
            }
            finally
            {
                strmessage = null;
            }
            objresponse.Exit = strExit;
            return objresponse;
        }

        //enviar correo
        //nombre original EnviarEmailAttach
        //envia correo e internamente copia los archivos e envia ala carpeta del servidor
        public static COMMON.GetSendEmail.SendEmailResponse GetSendEmailAttach(COMMON.GetSendEmail.SendEmailRequest request)
        {
            COMMON.GetSendEmail.SendEmailResponse objresponse = new COMMON.GetSendEmail.SendEmailResponse();
            string strExit = "";

            string strhost = KEY.AppSettings("ServerEmail");
            string strlogginguser = KEY.AppSettings("Userlogging");
            string strpassword = KEY.AppSettings("Userpassword");
            string strdestinationpath = KEY.AppSettings("CopyDestinationpath");
            // string strRouteDestiny = KEY.AppSettings("strDirectorioDetLlamada");
            List<string> lsArchivo = new List<string>();
            lsArchivo = request.lsAttached;
            MailAddress strFrom = new MailAddress(request.strSender);
            MailAddress strTo = new MailAddress(request.strTo);
            MailMessage strmessage = new MailMessage(strFrom, strTo);
            try
            {
                strmessage.IsBodyHtml = true;
                strmessage.Body = request.strMessage;
                strmessage.BodyEncoding = System.Text.Encoding.UTF8;
                strmessage.Subject = request.strSubject;
                strmessage.SubjectEncoding = System.Text.Encoding.UTF8;
                //strmessage.CC.Add("");
                //strmessage.Bcc.Add("");

                if (!string.IsNullOrEmpty(request.strCC))
                    strmessage.CC.Add(request.strCC);

                if (!string.IsNullOrEmpty(request.strBCC))
                    strmessage.Bcc.Add(request.strBCC);

                strmessage.Priority = MailPriority.High;
                //string[] File = request.strAttached.Split('|');
                if (lsArchivo != null)
                {
                    foreach (string item in lsArchivo)
                    {
                        if (System.IO.File.Exists(@item))
                        {
                            if (!System.IO.Directory.Exists(strdestinationpath))
                            {
                                System.IO.Directory.CreateDirectory(strdestinationpath);
                            }

                            string filename = System.IO.Path.GetFileName(item);
                            string newdestiny = System.IO.Path.Combine(strdestinationpath, filename);
                            if (string.IsNullOrEmpty(strdestinationpath))
                            {
                                System.IO.File.Copy(item, newdestiny, true);
                            }

                            System.IO.File.Copy(item, newdestiny, true);
                            strmessage.Attachments.Add(new Attachment(@item));
                        }
                    }
                }

                SmtpClient smtpClient = new System.Net.Mail.SmtpClient(strhost);
                smtpClient.Port = Convert.ToInt(KEY.AppSettings("PortServer"));
                smtpClient.ServicePoint.MaxIdleTime = 2;
                smtpClient.Credentials = new System.Net.NetworkCredential(strlogginguser, strpassword);
                smtpClient.Timeout = Convert.ToInt(KEY.AppSettings("TimeOutEmail"));
                smtpClient.EnableSsl = true;
                smtpClient.Send(strmessage);
                strExit = Constant.Constants.Message_OK;
                smtpClient.Dispose();


            }
            catch (Exception ex)
            {
                strExit = ex.Message.ToString();
            }
            objresponse.Exit = strExit;
            return objresponse;

        }

        //3er metodo EnviarCorreoAlt
        public static COMMON.GetSendEmail.SendEmailResponse GetSendEmailAlt(COMMON.GetSendEmail.SendEmailRequest request)
        {
            COMMON.GetSendEmail.SendEmailResponse objresponse = new COMMON.GetSendEmail.SendEmailResponse();
            string strExit = "";
            string strhost = KEY.AppSettings("ServerEmail");
            string strlogginguser = KEY.AppSettings("Userlogging");
            string strpassword = KEY.AppSettings("Userpassword");
            MailAddress strFrom = new MailAddress(request.strSender);
            MailAddress strTo = new MailAddress(request.strTo);
            MailMessage strmessage = new MailMessage(strFrom, strTo);
            int i = 0;
            bool blfileCopy = false;
            string strrouteBegin = "";
            string strRouteDestiny = KEY.AppSettings("CopyDestinationpath");
            try
            {
                byte[] arrbuffer;
                System.IO.FileStream stream;
                strmessage.IsBodyHtml = true;
                strmessage.Body = request.strMessage;
                strmessage.BodyEncoding = System.Text.Encoding.UTF8;
                strmessage.Subject = request.strSubject;
                strmessage.SubjectEncoding = System.Text.Encoding.UTF8;
                if (!string.IsNullOrEmpty(request.strCC))
                    strmessage.CC.Add(request.strCC);

                if (!string.IsNullOrEmpty(request.strBCC))
                    strmessage.Bcc.Add(request.strBCC);

                strmessage.Priority = MailPriority.High;
                GlobalDocument objglobal = new GlobalDocument();
                if (request.strAttached != null)
                {
                    string[] File = request.strAttached.Split('|');
                    string[] FileAtach = request.strJoinfile.Split('|');

                    GlobalDocument global = new GlobalDocument();
                    foreach (string item in File)
                    {
                        strrouteBegin = File[i].Substring(0, File[i].Length - FileAtach[i].Length);
                        string c = strrouteBegin + FileAtach[i];
                        COMMON.GetFileDefaultImpersonation.GetFileDefaultImpersonationRequest getfiledefaultrequest = new COMMON.GetFileDefaultImpersonation.GetFileDefaultImpersonationRequest()
                        {

                            Audit = request.Audit,
                            strPath = c
                        };
                        global = GetfileDefaultImpersonation(getfiledefaultrequest).objGlobalDocument;
                        arrbuffer = global.Document;
                        string strnewdestiny = System.IO.Path.Combine(strRouteDestiny, FileAtach[i]);
                        stream = new System.IO.FileStream(strnewdestiny, System.IO.FileMode.Create);
                        stream.Write(arrbuffer, 0, arrbuffer.Length);
                        stream.Close();
                        blfileCopy = false;
                        blfileCopy = System.IO.File.Exists(strnewdestiny);

                        if (blfileCopy == true)
                        {
                            strmessage.Attachments.Add(new Attachment(strnewdestiny));
                        }
                        i = i + 1;
                    }
                }

                SmtpClient smtpClient = new System.Net.Mail.SmtpClient(strhost);
                smtpClient.Port = Convert.ToInt(KEY.AppSettings("PortServer"));
                smtpClient.ServicePoint.MaxIdleTime = 2;
                smtpClient.Credentials = new System.Net.NetworkCredential(strlogginguser, strpassword);
                smtpClient.Timeout = Convert.ToInt(KEY.AppSettings("TimeOutEmail"));
                smtpClient.EnableSsl = true;
                smtpClient.Send(strmessage);
                strExit = Constant.Constants.Message_OK;
                smtpClient.Dispose();

            }
            catch (Exception ex)
            {
                strExit = ex.Message.ToString();
            }
            objresponse.Exit = strExit;
            return objresponse;

        }

        //metodo para obtener ruta
        public static COMMON.GetFileDefaultImpersonation.GetFileDefaultImpersonationResponse GetfileDefaultImpersonation(COMMON.GetFileDefaultImpersonation.GetFileDefaultImpersonationRequest request)
        {
            byte[] arrbuffer = null;

            COMMON.GlobalDocument objGlobalDoc = new GlobalDocument();
            INetworkConfiguration objInetWorkConfiguration = NetworkConfiguration.SIAC_POST_DirFacturas;

            string strFileDefault = Claro.Data.Network.Connect(request.Audit.Session, request.Audit.Transaction, objInetWorkConfiguration, System.IO.Path.GetPathRoot(request.strPath)).ToString();

            if (int.TryParse(KEY.AppSettings("timeOutElectronicBills"), out intTimeout))
            {
                intTimeTranscurrido = Claro.Constants.NumberZero;
                tmrController = new System.Timers.Timer(1);
                thrProcess = new Thread(new ThreadStart(() =>
                    {
                        arrbuffer = GetFileDefaultImpersonationTime(request.Audit.Session, request.Audit.Transaction, request.strPath);

                    }));
                thrProcess.Start();
                tmrController.Elapsed += new System.Timers.ElapsedEventHandler(CheckTimeOut);
                tmrController.Start();
                thrProcess.Join();

                objGlobalDoc.strCodeError = Claro.Constants.NumberZeroString;
                objGlobalDoc.Document = arrbuffer;
                objGlobalDoc.strMesaggeError = "";

                if ((intTimeTranscurrido >= intTimeout))
                {
                    Claro.Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, "Se excedio el tiempo de espera: " + intTimeout);
                    objGlobalDoc.strMesaggeError = Claro.SIACU.Constants.MessageTimeOut;
                    objGlobalDoc.strCodeError = Claro.Constants.NumberOneString;
                }
                else if (arrbuffer == null)
                {
                    objGlobalDoc.strMesaggeError = Claro.SIACU.Constants.MessageFileNotExist;
                    objGlobalDoc.strCodeError = Claro.Constants.NumberTwoString;
                }

            }
            else
            {
                throw new Exception("no se ha definido el valor del timeout en el key : strValorTimeOutFacturaSE");

            }
            COMMON.GetFileDefaultImpersonation.GetFileDefaultImpersonationResponse response = new COMMON.GetFileDefaultImpersonation.GetFileDefaultImpersonationResponse();

            response.objGlobalDocument = objGlobalDoc;


            return response;
        }


        #region metodos que son llamados por el enviaremailalt
        //metodo que conecta al arcchivo de suplantacion

        public static byte[] GetFileDefaultImpersonationTime(string strIdSession, string strTransaction, string strPath)
        {
            ConectUnityNetwork(strIdSession, strTransaction, Path.GetPathRoot(strPath));
            byte[] arrbuffer = GetFileInternal(strIdSession, strTransaction, strPath);
            return arrbuffer;
        }


        //metodo que conecta a la unidad de red
        public static void ConectUnityNetwork(string strIdsession, string strTransaction, string strPath)
        {
            List<string> arrConectionNetwork = new List<string>();
            if (!arrConectionNetwork.Exists(item => item == strPath))
            {
                arrConectionNetwork.Add(strPath);
            }
        }

        //metodo que obtiene archivo internal
        public static byte[] GetFileInternal(string strIdsession, string strTransaction, string strPath)
        {
            byte[] buffer = null;
            if (File.Exists(strPath))
            {
                Stream stream = null;
                try
                {
                    using (stream = File.Open(strPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        long length = stream.Length;
                        if (length > 0L)
                        {
                            buffer = new byte[length];
                            stream.Read(buffer, 0, (int)length);

                        }
                    }
                }
                catch (Exception exception)
                {
                    Claro.Web.Logging.Error(strIdsession, strTransaction, "el stream se cierra correctamente luego de la excepcion - " + exception.Message);


                }
                finally
                {

                    if (stream != null)
                    {
                        stream.Close();
                    }
                }

            }
            return buffer;
        }

        //metodo para verificar TimeOut
        public static void CheckTimeOut(object sender, System.Timers.ElapsedEventArgs e)
        {
            if ((intTimeTranscurrido >= intTimeout))
            {
                tmrController.Stop();
                if (thrProcess != null)
                {
                    thrProcess.Abort();
                    thrProcess = null;
                }
            }
        }
        #endregion

        #endregion
        public static COMMON.GetInsertInteractHFC.InsertInteractHFCResponse GetInsertInteractHFC(COMMON.GetInsertInteractHFC.InsertInteractHFCRequest objRequest)
        {
            var objResponse = new COMMON.GetInsertInteractHFC.InsertInteractHFCResponse();

            try
            {
                var rInteraccionId = string.Empty;
                var rFlagInsercion = string.Empty;
                var rMsgText = string.Empty;

                objResponse.rResult = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Common.GetInsertInteractHFC(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.Interaction, ref rInteraccionId, ref rFlagInsercion, ref rMsgText);
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

        public static COMMON.GetDatTempInteraction.DatTempInteractionResponse GetInfoInteractionTemplate(COMMON.GetDatTempInteraction.DatTempInteractionRequest objRequest)
        {
            var objResponse = new COMMON.GetDatTempInteraction.DatTempInteractionResponse();
            var vFLAG_CONSULTA = string.Empty;
            var vMSG_TEXT = string.Empty;
            try
            {
                var entity = Claro.Web.Logging.ExecuteMethod<COMMON.InteractionTemplate>(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Data.Transac.Service.Common.GetDatTempInteraction(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.vInteraccionID, ref vFLAG_CONSULTA, ref vMSG_TEXT);
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

        public static COMMON.GetNumberGWP.NumberGWPResponse GetNumberGWP(COMMON.GetNumberGWP.NumberGWPRequest objRequest)
        {
            string vNumber = "";
            COMMON.GetNumberGWP.NumberGWPResponse objResponse = new COMMON.GetNumberGWP.NumberGWPResponse()
            {
                Number = Claro.Web.Logging.ExecuteMethod<string>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Common.GetNumberGWP(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.Msisdn, ref vNumber);
                })
            };
            return objResponse;
        }

        public static COMMON.GetNumberEAI.NumberEAIResponse GetNumberEAI(COMMON.GetNumberEAI.NumberEAIRequest objRequest)
        {
            string vNumber = "";
            COMMON.GetNumberEAI.NumberEAIResponse objResponse = new COMMON.GetNumberEAI.NumberEAIResponse()
            {
                Number = Claro.Web.Logging.ExecuteMethod<string>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Common.GetNumberEAI(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.Msisdn, ref vNumber);
                })
            };
            return objResponse;
        }

        public static COMMON.GetVerifyUser.VerifyUserResponse GetVerifyUser(COMMON.GetVerifyUser.VerifyUserRequest objRequest)
        {
            var objResponse = new COMMON.GetVerifyUser.VerifyUserResponse();
            try
            {
                objResponse.LstConsultSecurities = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Common.VerifyUser(objRequest.SessionId, objRequest.TransactionId, objRequest.AppId, objRequest.AppName, objRequest.Username, objRequest.AppCode);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }

        public static COMMON.GetEvaluateAmount.EvaluateAmountResponse GetEvaluateAmount(COMMON.GetEvaluateAmount.EvaluateAmountRequest objRequest)
        {
            var objResponse = new COMMON.GetEvaluateAmount.EvaluateAmountResponse();
            try
            {
                objResponse.Resultado = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Common.GetEvaluateAmount(objRequest.StrIdSession, objRequest.StrTransaction, objRequest.VListaPerfil, objRequest.VMonto, objRequest.VUnidad, objRequest.VModalidad, objRequest.VTipoTelefono);
                });
            }

            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }

        public static COMMON.GetEvaluateAmount.EvaluateAmountResponse GetEvaluateAmount_DCM(COMMON.GetEvaluateAmount.EvaluateAmountRequest objRequest)
        {
            var objResponse = new COMMON.GetEvaluateAmount.EvaluateAmountResponse();
            try
            {
                objResponse.Resultado = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Common.GetEvaluateAmount_DCM(objRequest.StrIdSession, objRequest.StrTransaction, objRequest.VListaPerfil, objRequest.VMonto, objRequest.VUnidad, objRequest.VModalidad, objRequest.VTipoTelefono);
                });
            }

            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }

        #region External/ Internal Transfer

        public static COMMON.GetMzBloEdiType.MzBloEdiTypeResponse GetMzBloEdiTypePVU(COMMON.GetMzBloEdiType.MzBloEdiTypeRequest objMzBloEdiTypeRequest)
        {

            COMMON.GetMzBloEdiType.MzBloEdiTypeResponse objMzBloEdiTypeResponse = new COMMON.GetMzBloEdiType.MzBloEdiTypeResponse()
            {
                ListMzBloEdiType = Claro.Web.Logging.ExecuteMethod<List<ListItem>>
                (objMzBloEdiTypeRequest.Audit.Session,
                objMzBloEdiTypeRequest.Audit.Transaction,
                () =>
                {
                    return Data.Transac.Service.Common.GetMzBloEdiTypePVU(objMzBloEdiTypeRequest.Audit.Session,
                        objMzBloEdiTypeRequest.Audit.Transaction
                       );
                })
            };

            return objMzBloEdiTypeResponse;
        }

        public static COMMON.GetTipDptInt.TipDptIntResponse GetTipDptInt(COMMON.GetTipDptInt.TipDptIntRequest objTipDptIntRequest)
        {

            COMMON.GetTipDptInt.TipDptIntResponse objTipDptIntResponse = new COMMON.GetTipDptInt.TipDptIntResponse()
            {
                LisTipDptIntType = Claro.Web.Logging.ExecuteMethod<List<ListItem>>
                (objTipDptIntRequest.Audit.Session,
                objTipDptIntRequest.Audit.Transaction,
                () =>
                {
                    return Data.Transac.Service.Common.GetTipDptIntCOBS(objTipDptIntRequest.Audit.Session,
                        objTipDptIntRequest.Audit.Transaction
                       );
                })
            };

            return objTipDptIntResponse;
        }

        public static COMMON.GetDepartmentsPVU.DepartmentsPvuResponse GetDepartmentsPVU(COMMON.GetDepartmentsPVU.DepartmentsPvuRequest objDepartmentsRequest)
        {
            objDepartmentsRequest.CodDep = Constant.Constants.strDobleCero;
            objDepartmentsRequest.CodStatus = Constant.Constants.strLetraA;

            COMMON.GetDepartmentsPVU.DepartmentsPvuResponse objDepartmentsResponse = new COMMON.GetDepartmentsPVU.DepartmentsPvuResponse()
            {

                ListDepartments = Claro.Web.Logging.ExecuteMethod<List<ListItem>>
                (objDepartmentsRequest.Audit.Session,
                objDepartmentsRequest.Audit.Transaction,

                () =>
                {
                    return Data.Transac.Service.Common.GetDepartmentsPVU(objDepartmentsRequest.Audit.Session, objDepartmentsRequest.Audit.Transaction,
                        objDepartmentsRequest.CodDep, objDepartmentsRequest.CodStatus);
                })
            };
            return objDepartmentsResponse;
        }

        public static COMMON.GetProvincesPVU.ProvincesPvuResponse GetProvincesPVU(COMMON.GetProvincesPVU.ProvincesPvuRequest objProvincesRequest)
        {
            objProvincesRequest.CodProv = Constant.Constants.constCeroCeroCero;
            objProvincesRequest.CodStatus = Constant.Constants.strLetraA;

            COMMON.GetProvincesPVU.ProvincesPvuResponse objProvincesResponse = new COMMON.GetProvincesPVU.ProvincesPvuResponse()
            {
                ListProvinces = Claro.Web.Logging.ExecuteMethod<List<ListItem>>
                (objProvincesRequest.Audit.Session,
                objProvincesRequest.Audit.Transaction,

                () =>
                {
                    return Data.Transac.Service.Common.GetProvincesPVU(objProvincesRequest.Audit.Session,
                        objProvincesRequest.Audit.Transaction,
                        objProvincesRequest.CodProv, objProvincesRequest.CodDep, objProvincesRequest.CodStatus);
                })
            };

            return objProvincesResponse;
        }

        public static COMMON.GetDistrictsPVU.DistrictsPvuResponse GetDistrictsPVU(COMMON.GetDistrictsPVU.DistrictsPvuRequest objDistrictsRequest)
        {
            objDistrictsRequest.CodDistr = Constant.Constants.strDobleCero + Constant.Constants.strDobleCero;
            objDistrictsRequest.CodStatus = Constant.Constants.strLetraA;


            COMMON.GetDistrictsPVU.DistrictsPvuResponse objDistrictsResponse = new COMMON.GetDistrictsPVU.DistrictsPvuResponse()
            {
                ListDistricts = Claro.Web.Logging.ExecuteMethod<List<ListItem>>
                (objDistrictsRequest.Audit.Session,
                objDistrictsRequest.Audit.Transaction,

                () =>
                {
                    return Data.Transac.Service.Common.GetDistrictsPVU(objDistrictsRequest.Audit.Session,
                        objDistrictsRequest.Audit.Transaction, objDistrictsRequest.CodDistr,
                        objDistrictsRequest.CodProv, objDistrictsRequest.CodDepart, objDistrictsRequest.CodStatus
                       );
                })
            };

            return objDistrictsResponse;
        }

        public static COMMON.GetCenterPopulatPVU.CenterPopulatPvuRespose GetCenterPopulatPVU(COMMON.GetCenterPopulatPVU.CenterPopulatPvuRequest objCenterPopulatRequest)
        {
            objCenterPopulatRequest.CodStatus = Claro.Constants.LetterA;


            COMMON.GetCenterPopulatPVU.CenterPopulatPvuRespose objCenterPopulatResponse = new COMMON.GetCenterPopulatPVU.CenterPopulatPvuRespose()
            {
                idUbigeo = Claro.Web.Logging.ExecuteMethod<string>
                (objCenterPopulatRequest.Audit.Session,
                objCenterPopulatRequest.Audit.Transaction,
                () =>
                {
                    return Data.Transac.Service.Common.GetCenterPopulatPVU(objCenterPopulatRequest.Audit.Session,
                        objCenterPopulatRequest.Audit.Transaction, objCenterPopulatRequest.CodDepart, objCenterPopulatRequest.CodProv,
                        objCenterPopulatRequest.CodDistr,
                         objCenterPopulatRequest.CodStatus
                       );
                })
            };

            return objCenterPopulatResponse;
        }

        public static COMMON.GetZoneTypeCOBS.ZoneTypeCobsResponse GetZoneTypeCOBS(COMMON.GetZoneTypeCOBS.ZoneTypeCobsRequest objZoneTypeRequest)
        {

            COMMON.GetZoneTypeCOBS.ZoneTypeCobsResponse objZoneTypeResponse = new COMMON.GetZoneTypeCOBS.ZoneTypeCobsResponse()
            {
                ListZoneType = Claro.Web.Logging.ExecuteMethod<List<ListItem>>
                (objZoneTypeRequest.Audit.Session,
                objZoneTypeRequest.Audit.Transaction,
                () =>
                {
                    return Data.Transac.Service.Common.GetZoneTypeCOBS(objZoneTypeRequest.Audit.Session,
                        objZoneTypeRequest.Audit.Transaction
                       );
                })
            };

            return objZoneTypeResponse;
        }

        public static COMMON.GetBuildingsPVU.BuildingsPvuResponse GetBuildingsPVU(COMMON.GetBuildingsPVU.BuildingsPvuRequest objBuildingsRequest)
        {
            COMMON.GetBuildingsPVU.BuildingsPvuResponse objBuildingsResponse = new COMMON.GetBuildingsPVU.BuildingsPvuResponse()
            {
                ListBuildings = Claro.Web.Logging.ExecuteMethod<List<ListItem>>
                (objBuildingsRequest.Audit.Session,
                objBuildingsRequest.Audit.Transaction,
                () =>
                {
                    return Data.Transac.Service.Common.GetBuildingsPVU(objBuildingsRequest.Audit.Session,
                        objBuildingsRequest.Audit.Transaction,
                        objBuildingsRequest.CodFlant, objBuildingsRequest.CodBuildings);
                })
            };

            return objBuildingsResponse;
        }

        public static COMMON.GetWorkType.WorkTypeResponse GetWorkType(COMMON.GetWorkType.WorkTypeRequest objWorkTypeRequest)
        {
            COMMON.GetWorkType.WorkTypeResponse objWorkTypeResponse = new COMMON.GetWorkType.WorkTypeResponse()
            {
                WorkTypes = Claro.Web.Logging.ExecuteMethod<List<ListItem>>
                (objWorkTypeRequest.Audit.Session,
                objWorkTypeRequest.Audit.Transaction,
                () =>
                {
                    return Data.Transac.Service.Common.GetWorkType(objWorkTypeRequest.Audit.Session,
                        objWorkTypeRequest.Audit.Transaction,
                        objWorkTypeRequest.TransacType);
                })
            };

            return objWorkTypeResponse;
        }

        public static COMMON.GetWorkSubType.WorkSubTypeResponse GetWorkSubType(COMMON.GetWorkSubType.WorkSubTypeRequest objWorkSubTypeRequest)
        {
            COMMON.GetWorkSubType.WorkSubTypeResponse objWorkSubTypeResponse = new COMMON.GetWorkSubType.WorkSubTypeResponse()
            {
                WorkSubTypes = Claro.Web.Logging.ExecuteMethod<List<ListItem>>
                (objWorkSubTypeRequest.Audit.Session,
                objWorkSubTypeRequest.Audit.Transaction,
                () =>
                {
                    return Data.Transac.Service.Common.GetWorkSubType(objWorkSubTypeRequest.Audit.Session,
                        objWorkSubTypeRequest.Audit.Transaction,
                        objWorkSubTypeRequest.CodTypeWork);
                })
            };

            return objWorkSubTypeResponse;
        }
        public static List<ListItem> GetDocumentTypeCOBS(string strIdSession, string strTransaction, string strCodCargaDdl)
        {
            List<ListItem> listItem = null;

            listItem = Claro.Web.Logging.ExecuteMethod<List<ListItem>>(strIdSession, strTransaction, () =>
            {
                return Data.Transac.Service.Common.GetDocumentTypeCOBS(strIdSession, strTransaction, strCodCargaDdl);
            });
            return listItem;
        }

        #endregion

        #region audit
        public static COMMON.GetPagOptionXuserNV.PagOptionXuserNVResponse GetPagOptionXuserVN(COMMON.GetPagOptionXuserNV.PagOptionXuserNVRequest objRequest)
        {
            COMMON.GetPagOptionXuserNV.PagOptionXuserNVResponse oRegistroAuditoriaResponse = new COMMON.GetPagOptionXuserNV.PagOptionXuserNVResponse();

            oRegistroAuditoriaResponse = Web.Logging.ExecuteMethod<COMMON.GetPagOptionXuserNV.PagOptionXuserNVResponse>(objRequest.Audit.Session, objRequest.Audit.Transaction,
                () =>
                {
                    return Claro.SIACU.Data.Transac.Service.Common.GetPagOptionXuserVN(objRequest);

                });

            return oRegistroAuditoriaResponse;
        }

        public static COMMON.GetPagOptionXuser.PagOptionXuserResponse GetPagOptionXuser(COMMON.GetPagOptionXuser.PagOptionXuserRequest objRequest)
        {
            COMMON.GetPagOptionXuser.PagOptionXuserResponse oRegistroAuditoriaResponse = new COMMON.GetPagOptionXuser.PagOptionXuserResponse();

            oRegistroAuditoriaResponse = Web.Logging.ExecuteMethod<COMMON.GetPagOptionXuser.PagOptionXuserResponse>(
                () =>
                {
                    return Claro.SIACU.Data.Transac.Service.Common.GetPagOptionXuser(objRequest);

                });

            return oRegistroAuditoriaResponse;
        }
        #endregion

        public static COMMON.GetUser.UserResponse GetUser(COMMON.GetUser.UserRequest objUserRequest)
        {
            COMMON.GetUser.UserResponse objTypificationResponse = new COMMON.GetUser.UserResponse()
            {
                UserModel = Claro.Web.Logging.ExecuteMethod<User>(objUserRequest.Audit.Session, objUserRequest.Audit.Transaction,
                    () =>
                    {
                        return Data.Transac.Service.Common.GetUser(objUserRequest.Audit.Session, objUserRequest.Audit.Transaction, objUserRequest.CodeUser, objUserRequest.CodeRol, objUserRequest.CodeCac, objUserRequest.State);
                    })
            };

            return objTypificationResponse;
        }

        public static COMMON.GetPenaltyChangePlans.PenaltyChangePlanResponse GetPenaltyChangePlan(COMMON.GetPenaltyChangePlans.PenaltyChangePlanRequest objRequest)
        {
            var model = new COMMON.GetPenaltyChangePlans.PenaltyChangePlanResponse();

            var objDatBscsExtRequest = new Entity.Transac.Service.Common.GetDatBscsExt.DatBscsExtRequest
            {
                Audit = objRequest.Audit,
                NroTelephone = objRequest.NroTelephone,
                CodeNewPlan = objRequest.CodeNewPlan
            };

            try
            {
                var resultDatBsc = GetDatBscsExt(objDatBscsExtRequest);
                model.NroFacture = resultDatBsc.NroFacture;
                model.CargCurrentFixed = resultDatBsc.CargCurrentFixed;
                model.CargFixedNewPlan = resultDatBsc.CargFixedNewPlan;
                model.Result = resultDatBsc.Result;
                if (resultDatBsc.Result)
                {
                    var objGetPenaltyExtRequest = new Entity.Transac.Service.Common.GetPenaltyExterns.GetPenaltyExtRequest
                    {
                        Audit = objRequest.Audit,
                        NroTelephone = objRequest.NroTelephone,
                        DatePenalty = objRequest.DatePenalty,
                        NroFacture = objDatBscsExtRequest.NroFacture,
                        CargFixedCurrent = objDatBscsExtRequest.CargFixedCurrent,
                        CargFixedNewPlan = objDatBscsExtRequest.CargFixedNewPlan,
                        DayXMonth = objRequest.DayXMonth,
                        CodeNewPlan = objRequest.CodeNewPlan
                    };

                    var resultPenalty = GetPenaltyExt(objGetPenaltyExtRequest);
                    model.AgreementIdExit = resultPenalty.AgreementIdExit;
                    model.DayPendient = resultPenalty.DayPendient;
                    model.CargFiexdDiar = resultPenalty.CargFiexdDiar;
                    model.PrecList = resultPenalty.PrecList;
                    model.PrecVent = resultPenalty.PrecVent;
                    model.PenaltyPcs = resultPenalty.PenaltyPcs;
                    model.PenaltyApadece = resultPenalty.PenaltyApadece;
                    model.Result = resultPenalty.Result;
                }

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
                throw ex;
            }

            return model;
        }
        public static Entity.Transac.Service.Common.GetDatBscsExt.DatBscsExtResponse GetDatBscsExt(Entity.Transac.Service.Common.GetDatBscsExt.DatBscsExtRequest objRequest)
        {
            var objResponse = new Entity.Transac.Service.Common.GetDatBscsExt.DatBscsExtResponse();

            try
            {
                double nroFacture = 0;
                double cargCurrentFixed = 0;
                double cargFixedNewPlan = 0;

                objResponse.Result = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {

                    return Claro.SIACU.Data.Transac.Service.Common.GetDatBscsExt(objRequest.Audit.Session, objRequest.Audit.Transaction,
                        objRequest.NroTelephone, objRequest.CodeNewPlan, ref nroFacture, ref cargCurrentFixed, ref cargFixedNewPlan);
                });


                objResponse.NroFacture = nroFacture;
                objResponse.CargCurrentFixed = cargCurrentFixed;
                objResponse.CargFixedNewPlan = cargFixedNewPlan;

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
                throw ex;
            }

            return objResponse;
        }

        public static Entity.Transac.Service.Common.GetPenaltyExterns.GetPenaltyExtResponse GetPenaltyExt(Entity.Transac.Service.Common.GetPenaltyExterns.GetPenaltyExtRequest objRequest)
        {
            var objResponse = new Entity.Transac.Service.Common.GetPenaltyExterns.GetPenaltyExtResponse();

            try
            {
                double dblAgreementIdExit = 0;
                double dblDayPendient = 0;
                double dblCargFiexdDiar = 0;
                double dblPrecList = 0;
                double dblPrecVent = 0;
                double dblPenaltyPcs = 0;
                double dblPenaltyApadece = 0;

                objResponse.Result = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {

                    return Claro.SIACU.Data.Transac.Service.Common.GetPenaltyExt(objRequest.Audit.Session, objRequest.Audit.Transaction,
                        objRequest.NroTelephone, objRequest.DatePenalty, objRequest.NroFacture,
                        objRequest.CargFixedCurrent, objRequest.CargFixedNewPlan, objRequest.DayXMonth,
                        objRequest.CodeNewPlan, ref dblAgreementIdExit, ref dblDayPendient, ref dblCargFiexdDiar,
                        ref dblPrecList, ref dblPrecVent, ref dblPenaltyPcs, ref dblPenaltyApadece);
                });


                objResponse.AgreementIdExit = dblAgreementIdExit;
                objResponse.DayPendient = dblDayPendient;
                objResponse.CargFiexdDiar = dblCargFiexdDiar;
                objResponse.PrecList = dblPrecList;
                objResponse.PrecVent = dblPrecVent;
                objResponse.PenaltyPcs = dblPenaltyPcs;
                objResponse.PenaltyApadece = dblPenaltyApadece;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
                throw ex;
            }

            return objResponse;
        }


        public static COMMON.GetBusinessInteraction2.BusinessInteraction2Response GetBusinessInteractionFixed(COMMON.GetBusinessInteraction2.BusinessInteraction2Request request)
        {
            string strInteractionId = "", strFlagInsertion = "", strMessage = "";
            COMMON.GetBusinessInteraction2.BusinessInteraction2Response objResponse =
                new COMMON.GetBusinessInteraction2.BusinessInteraction2Response()
                {
                    ProcessOK = Claro.Web.Logging.ExecuteMethod<bool>(request.Audit.Session, request.Audit.Transaction,
                    () =>
                    {
                        return Claro.SIACU.Data.Transac.Service.Common.InsertBusinessInteractionFixed(request.Audit.Session, request.Audit.Transaction, request.Item, ref strInteractionId,
                            ref strFlagInsertion, ref strMessage);
                    })
                };
            objResponse.InteractionId = strInteractionId;
            objResponse.FlagInsertion = strFlagInsertion;
            objResponse.MsgText = strMessage;
            return objResponse;
        }


        public static COMMON.GetEmployerDate.GetEmployerDateResponse GetEmployerDate(COMMON.GetEmployerDate.GetEmployerDateRequest objDatosEmpleadoRequest)
        {

            COMMON.GetEmployerDate.GetEmployerDateResponse objAuditResponse = new COMMON.GetEmployerDate.GetEmployerDateResponse();

            objAuditResponse = Web.Logging.ExecuteMethod(objDatosEmpleadoRequest.Audit.Session, objDatosEmpleadoRequest.Audit.Transaction,
                () =>
                {
                    return Datainfreatructure.Common.GetEmployerDate(objDatosEmpleadoRequest);

                });

            return objAuditResponse;
        }

        public static COMMON.GetConsultIGV.ConsultIGVResponse GetConsultIGV(COMMON.GetConsultIGV.ConsultIGVRequest objRequest)
        {
            var objResponse = new COMMON.GetConsultIGV.ConsultIGVResponse();
            try
            {
                objResponse.ListConsultIGV = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Common.GetConsultIGV(objRequest.SessionId, objRequest.TransactionId, objRequest.AppId, objRequest.AppName, objRequest.Username);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }
        public static ContractByPhoneNumberResponse GetContractByPhoneNumber(ContractByPhoneNumberRequest objRequest)
        {
            ContractByPhoneNumberResponse objResponse = new ContractByPhoneNumberResponse();

            objResponse = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction,
                () =>
                {
                    return Data.Transac.Service.Common.GetContractByPhoneNumber(objRequest);
                });
            return objResponse;
        }
        /*PROY-32650*/
        public static COMMON.GetSendEmailSB.SendEmailSBResponse GetSendEmailSB(COMMON.GetSendEmailSB.SendEmailSBRequest objRequest)
        {
            var objResponse = new COMMON.GetSendEmailSB.SendEmailSBResponse();
            try
            {
                objResponse = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Common.GetSendEmailSB(
                        objRequest.SessionId,
                        objRequest.TransactionId,
                        objRequest.strAppID,
                        objRequest.strAppCode,
                        objRequest.strAppUser,
                        objRequest.strRemitente,
                        objRequest.strDestinatario,
                        objRequest.strAsunto,
                        objRequest.strMensaje,
                        objRequest.strHTMLFlag,
                        objRequest.Archivo,
                        objRequest.strNomFile
                        );
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }

        public static COMMON.GetNoteInteraction.NoteInteractionResponse GetNoteInteraction(COMMON.GetNoteInteraction.NoteInteractionRequest objRequest)
        {
            var objResponse = new COMMON.GetNoteInteraction.NoteInteractionResponse();

            try
            {
                string rFlagInsercion = string.Empty;
                string rMsgText = string.Empty;

                var nota = Claro.Web.Logging.ExecuteMethod<string>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Claro.SIACU.Data.Transac.Service.Common.GetNoteInteraction(
                        objRequest.Audit.Session,
                        objRequest.Audit.Transaction,
                        objRequest.vInteraccionId,
                        ref rFlagInsercion,
                        ref rMsgText);
                });
                objResponse.strNota = nota;
                objResponse.rFlagInsercion = rFlagInsercion;
                objResponse.rMsgText = rMsgText;

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
                throw ex;
            }

            return objResponse;
        }
        public static COMMON.GetDynamicCaseTemplateData.DynamicCaseTemplateDataResponse GetDynamicCaseTemplateData(COMMON.GetDynamicCaseTemplateData.DynamicCaseTemplateDataRequest objRequest)
        {
            var objResponse = new COMMON.GetDynamicCaseTemplateData.DynamicCaseTemplateDataResponse();

            try
            {
                string vFLAG_CONSULTA = string.Empty;
                string vMSG_TEXT = string.Empty;

                var CaseTemplate = Claro.Web.Logging.ExecuteMethod<CaseTemplate>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Claro.SIACU.Data.Transac.Service.Common.GetDynamicCaseTemplateData(
                        objRequest.Audit.Session,
                        objRequest.Audit.Transaction,
                        objRequest.vCasoID,
                        ref vFLAG_CONSULTA,
                        ref vMSG_TEXT);
                });
                objResponse.oCaseTemplate = CaseTemplate;
                objResponse.vFLAG_CONSULTA = vFLAG_CONSULTA;
                objResponse.vMSG_TEXT = vMSG_TEXT;

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
                throw ex;
            }

            return objResponse;
        }

        public static RegisterServiceCommercialResponse GetRegisterServiceCommercial(RegisterServiceCommercialRequest objRequest)
        {
            RegisterServiceCommercialResponse objResponse = new RegisterServiceCommercialResponse();
            objResponse = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
            {
                return Data.Transac.Service.Common.GetRegisterServiceCommercial(objRequest);
            });
            return objResponse;
        }

        public static UpdatexInter30Response GetUpdatexInter30(UpdatexInter30Request objRequest)
        {
            var objResponse = new UpdatexInter30Response();

            try
            {
                var rFlagInsercion = string.Empty;
                var rMsgText = string.Empty;

                objResponse.rResult = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Data.Transac.Service.Common.UpdatexInter30(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.p_objid, objRequest.p_texto, ref rFlagInsercion, ref rMsgText);
                    });

                objResponse.rFlagInsercion = rFlagInsercion;
                objResponse.rMsgText = rMsgText;
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }

        public static COMMON.GetProgramTask.ProgramTaskResponse GetProgramTask(COMMON.GetProgramTask.ProgramTaskRequest objRequest)
        {
            var objResponse = new COMMON.GetProgramTask.ProgramTaskResponse();
            try
            {
                objResponse.ListProgramTask = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Common.GetProgramTask(objRequest.SessionId, objRequest.TransactionId, objRequest.StrIdLista);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }
            return objResponse;
        }

        public static COMMON.GetTypeTransaction.TypeTransactionResponse GetTypeTransaction(COMMON.GetTypeTransaction.TypeTransactionRequest objRequest)
        {
            var objResponse = new COMMON.GetTypeTransaction.TypeTransactionResponse();
            try
            {
                objResponse.TypeTransaction = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Common.GetTypeTransaction(objRequest.SessionId, objRequest.TransactionId);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }
            return objResponse;
        }

        public static COMMON.GetParameterTerminalTPI.ParameterTerminalTPIResponse GetParameterTerminalTPI(COMMON.GetParameterTerminalTPI.ParameterTerminalTPIRequest objRequest)
        {
            string Message = "";
            COMMON.GetParameterTerminalTPI.ParameterTerminalTPIResponse objResponse = new COMMON.GetParameterTerminalTPI.ParameterTerminalTPIResponse()
            {
                ListParameterTeminalTPI = Claro.Web.Logging.ExecuteMethod<List<ParameterTerminalTPI>>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Common.GetParameterTerminalTPI(objRequest.Audit.Session, objRequest.Audit.Transaction,
                        objRequest.ParameterID, ref Message);
                })
            };
            objResponse.Message = Message;
            return objResponse;
        }

        public static COMMON.GetInsertEvidence.InsertEvidenceResponse GetInsertEvidence(COMMON.GetInsertEvidence.InsertEvidenceRequest objRequest)
        {
            var objResponse = new COMMON.GetInsertEvidence.InsertEvidenceResponse();
            try
            {
                objResponse = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Common.GetInsertEvidence(objRequest);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }
            return objResponse;
        }


        public static COMMON.GetSendEmail.SendEmailResponse GetSendEmailFixed(COMMON.GetSendEmail.SendEmailRequest request)
        {
            COMMON.GetSendEmail.SendEmailResponse objresponse = new COMMON.GetSendEmail.SendEmailResponse();
            string strExit = "";           
            MailAddress strFrom = new MailAddress(request.strSender);
            MailAddress strTo = new MailAddress(request.strTo);
            MailMessage strmessage = new MailMessage(strFrom, strTo);
            try
            {
                strmessage.IsBodyHtml = true;
                strmessage.Body = request.strMessage;
                strmessage.BodyEncoding = System.Text.Encoding.UTF8;
                strmessage.Subject = request.strSubject;
                strmessage.SubjectEncoding = System.Text.Encoding.UTF8;
                if (!string.IsNullOrEmpty(request.strCC))
                    strmessage.CC.Add(request.strCC);

                if (!string.IsNullOrEmpty(request.strBCC))
                    strmessage.Bcc.Add(request.strBCC);

                strmessage.Priority = MailPriority.High;

                //CAMBIO PARA ENVIAR CORREO POR ARREGLO DE BYTES              

                if (request.strAttached != null)
                {
                    string[] File = request.strAttached.Split('|');
                    foreach (var item in File)
                    {
                        if (System.IO.File.Exists(item))
                        {
                            strmessage.Attachments.Add(new Attachment(item));
                        }
                    }
                }


                System.IO.MemoryStream memStream = null;
                System.IO.StreamWriter streamWriter = null;
                Attachment thisAttachment = null;

                if (request.AttachedByte != null)
                {
                    memStream = new System.IO.MemoryStream(request.AttachedByte);
                    streamWriter = new System.IO.StreamWriter(memStream);
                    streamWriter.Flush();
                    memStream.Position = 0;
                    thisAttachment = new Attachment(memStream, "application/pdf");
                    thisAttachment.ContentDisposition.FileName = request.strAttached;
                    strmessage.Attachments.Add(thisAttachment);
                }


                SmtpClient smtpClient = new System.Net.Mail.SmtpClient();
                smtpClient.Host = "LIMMAILF1.tim.com.pe";
                smtpClient.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
                smtpClient.EnableSsl = false;
                smtpClient.Send(strmessage);
                strExit = Constant.Constants.Message_OK;
                smtpClient.Dispose();

            }
            catch (Exception ex)
            {
                strExit = ex.Message.ToString();
            }
            finally
            {
                strmessage = null;
            }
            objresponse.Exit = strExit;
            return objresponse;

        }
        public static COMMON.GetConsultImei.ConsultImeiResponse GetConsultImei(COMMON.GetConsultImei.ConsultImeiRequest objRequest)
        {
            var objResponse = new COMMON.GetConsultImei.ConsultImeiResponse();
            try
            {
                objResponse.ListConsultImei = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Common.GetConsultImei(objRequest.SessionId, objRequest.TransactionId,objRequest.Nro_phone);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }
        public static COMMON.GetConsultMarkModel.ConsultMarkModelResponse GetConsultMarkModel(COMMON.GetConsultMarkModel.ConsultMarkModelRequest objRequest)
        {
            var objResponse = new COMMON.GetConsultMarkModel.ConsultMarkModelResponse();
            try
            {
                objResponse.result = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Common.GetConsultMarkModel(objRequest.SessionId, objRequest.TransactionId, objRequest.Nro_imei);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }
  public static COMMON.GetSot.GetSotResponse GetSotMtto(COMMON.GetSot.GetSotRequest objSotRequest)
        {
            var objSotResponse = new COMMON.GetSot.GetSotResponse
            {
                ListSot = Claro.Web.Logging.ExecuteMethod<List<Claro.SIACU.Entity.Transac.Service.Common.ListItem>>
                (objSotRequest.Audit.Session,
                    objSotRequest.Audit.Transaction,
                    () =>
                    {
                        return Data.Transac.Service.Common.GetSotMtto(objSotRequest.Audit.Session,
                            objSotRequest.Audit.Transaction, Int64.Parse(objSotRequest.CUSTOMER_ID), Int64.Parse(objSotRequest.COD_ID));
                    })
            };

            return objSotResponse;
        }
        public static QuestionsAnswerSecurityResponse GetQuestionsAnswerSecurity(COMMON.GetQuestionsAnswerSecurity.QuestionsAnswerSecurityRequest objRequest)
        {

            QuestionsAnswerSecurityResponse oQuestionsAnswerSecurity = null;
            oQuestionsAnswerSecurity = new QuestionsAnswerSecurityResponse();
            try
            {
                oQuestionsAnswerSecurity.ListQuestionsSecurity = Claro.Web.Logging.ExecuteMethod<List<QuestionsSecurity>>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
            {
                return Data.Transac.Service.Common.GetQuestionsSecurity(objRequest.SessionId, objRequest.TransactionId, objRequest.Typeclient, objRequest.Groupclient);
            });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }
            try
            {
                oQuestionsAnswerSecurity.ListAnswerSecurity = Claro.Web.Logging.ExecuteMethod<List<AnswerSecurity>>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Common.GetAnswerSecurity(objRequest.SessionId, objRequest.TransactionId, objRequest.Typeclient, objRequest.Groupclient);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }
            return oQuestionsAnswerSecurity;
        }

       
        public static COMMON.GetEquipmentForeign.EquipmentForeignResponse GetEquipmentForeign(COMMON.GetEquipmentForeign.EquipmentForeignRequest objRequest)
        {
            int codeError = 0;
            string msgError = "";

            var objResponse = new COMMON.GetEquipmentForeign.EquipmentForeignResponse();
            try
            {
                objResponse.ListEquipmentForeign = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Common.GetEquipmentForeign(objRequest.SessionId, objRequest.TransactionId, objRequest.CustomerId, objRequest.Estado, ref codeError, ref msgError);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            objResponse.CodeError = codeError;
            objResponse.MsgError = msgError;
            return objResponse;
        }
        public static COMMON.GetInteraction.InteractionResponse GetInteraction(COMMON.GetInteraction.InteractionRequest objRequest)
        {

            var objResponse = new COMMON.GetInteraction.InteractionResponse();
            try
            {
                objResponse.Restul = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Common.GetInteraction(objRequest.Audit.Transaction, objRequest.objTypification, objRequest.Audit);
                });

            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }
            
            return objResponse;
        }

        public static COMMON.GetInsertEquipmentForeign.InsertEquipmentForeignResponse GetInsertEquipmentForeign(COMMON.GetInsertEquipmentForeign.InsertEquipmentForeignRequest objRequest)
        {
            int rCodeResult = 0;
            string rMsgText = "";
            const string NumeracionUNO = "1";
            const string NumeracionDOS = "2";
            const string NumeracionCUATRO = "4";

            var objResponse = new COMMON.GetInsertEquipmentForeign.InsertEquipmentForeignResponse();
            try
            {
                COMMON.GetListEquipmentRegistered.ListEquipmentRegisteredRequest objListEquipmentRegisteredRequest = new COMMON.GetListEquipmentRegistered.ListEquipmentRegisteredRequest()
                {
                    Audit = objRequest.Audit,
                    CustomerId = objRequest.item.REDEN_CUSTOMERID,
                    Imei = objRequest.item.REDEV_NUMERO_IMEI,
                    EstadoId = objRequest.item.REDEV_ESTADO,
                    NumMaximo = objRequest.item.MAXIMO,
                };                
                COMMON.GetListEquipmentRegistered.ListEquipmentRegisteredResponse objListEquipmentRegisteredResponse = new COMMON.GetListEquipmentRegistered.ListEquipmentRegisteredResponse();
      
                Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "IN Business GetInsertEquipmentForeign >  Entrando a GetListEquipmentRegistered()");
                objListEquipmentRegisteredResponse = GetListEquipmentRegistered(objListEquipmentRegisteredRequest);
                if (objListEquipmentRegisteredResponse.CodeError == 0) {
                    
                    COMMON.GetTypification.TypificationRequest objTypificationRequest = new COMMON.GetTypification.TypificationRequest()
                    {
                        Audit = objRequest.Audit,
                        TRANSACTION_NAME = objRequest.nameTransaction
                    };

                    Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "IN Business GetInsertEquipmentForeign >  Entrando a GetTypification()");
                    COMMON.GetTypification.TypificationResponse objTypificationResponse = new COMMON.GetTypification.TypificationResponse();
                    objTypificationResponse = GetTypification(objTypificationRequest);

                    COMMON.GetInsertInt.InsertIntRequest objInsertIntRequest = new COMMON.GetInsertInt.InsertIntRequest()
                    {
                        Audit = objRequest.Audit,
                        item = new Iteraction()
                        {
                            FECHA_CREACION = DateTime.Now.ToShortDateString(),
                            TELEFONO = objRequest.customerTelephone,
                            TIPO = objTypificationResponse.ListTypification[0].TIPO,
                            CLASE = objTypificationResponse.ListTypification[0].CLASE,
                            SUBCLASE = objTypificationResponse.ListTypification[0].SUBCLASE,
                            TIPO_INTER = KEY.AppSettings("strEntrante"),
                            METODO = KEY.AppSettings("strTelefono"),
                            RESULTADO = ConfigurationManager.AppSettings("Ninguno"),
                            HECHO_EN_UNO = Claro.Constants.NumberOneString,
                            NOTAS = objRequest.notes,
                            FLAG_CASO = Claro.Constants.NumberZeroString,
                            USUARIO_PROCESO = KEY.AppSettings("USRProcesoSU"),
                            AGENTE = objRequest.userAccesslogin,

                        }
                    };
                    Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "IN Business GetInsertEquipmentForeign >  Entrando a GetInsertInt()");
                    COMMON.GetInsertInt.InsertIntResponse objInsertIntResponse = new COMMON.GetInsertInt.InsertIntResponse();
                    objInsertIntResponse = GetInsertInt(objInsertIntRequest);

                    if (objInsertIntResponse.ProcesSucess == true)
                    {
                        COMMON.GetInsertTemplateInteraction.InsertTemplateInteractionRequest objInsertTemplateInteractionRequest = new COMMON.GetInsertTemplateInteraction.InsertTemplateInteractionRequest()
                        {
                            Audit = objRequest.Audit,
                            IdInteraction = objInsertIntResponse.Interactionid,

                            item = new InsertTemplateInteraction()
                            {
                                _X_INTER_5 = objRequest.area,
                                _X_INTER_15 = objRequest.nameCac,
                                _X_INTER_16 = objRequest.documentTypeText,
                                _X_INTER_20 = objRequest.markModel,
                                _X_FIRST_NAME = objRequest.customerName,
                                _X_LAST_NAME = objRequest.customerLastName,
                                _X_DOCUMENT_NUMBER = objRequest.customerNumberDocument,
                                _X_REFERENCE_PHONE = objRequest.referencePhone,
                                _X_NAME_LEGAL_REP = objRequest.customerLegalAgent,
                                _X_DNI_LEGAL_REP = objRequest.customerNumberDocumentRRLL,
                                _X_BIRTHDAY = Convert.ToDate(DateTime.Now.ToString("dd/MM/yyyy")),
                                _X_OTHER_FIRST_NAME = objRequest.firstName,
                                _X_OTHER_LAST_NAME = objRequest.lastName,
                                _X_OTHER_DOC_NUMBER = objRequest.documentNumber,
                                _X_IMEI = objRequest.imei,
                                _X_CLARO_NUMBER = objRequest.customerTelephone,
                                _X_INTER_1  = objRequest.imeiFisico
                            }
                        };
                        switch (objRequest.tipoPersona)
                        {
                            case NumeracionUNO:
                                objInsertTemplateInteractionRequest.item._X_FLAG_TITULAR = Claro.Constants.NumberOneString;
                                objInsertTemplateInteractionRequest.item._X_FLAG_REGISTERED = Claro.Constants.NumberZeroString;
                                objInsertTemplateInteractionRequest.item._X_FLAG_LEGAL_REP = Claro.Constants.NumberZeroString;
                                objInsertTemplateInteractionRequest.item._X_FLAG_OTHER = Claro.Constants.NumberZeroString;
                                break;
                            case NumeracionDOS:
                                objInsertTemplateInteractionRequest.item._X_FLAG_TITULAR = Claro.Constants.NumberZeroString;
                                objInsertTemplateInteractionRequest.item._X_FLAG_REGISTERED = Claro.Constants.NumberOneString;
                                objInsertTemplateInteractionRequest.item._X_FLAG_LEGAL_REP = Claro.Constants.NumberZeroString;
                                objInsertTemplateInteractionRequest.item._X_FLAG_OTHER = Claro.Constants.NumberZeroString;
                                objInsertTemplateInteractionRequest.item._X_INTER_17 = objRequest.parient;
                                break;
                            case NumeracionCUATRO:
                                objInsertTemplateInteractionRequest.item._X_FLAG_TITULAR = Claro.Constants.NumberZeroString;
                                objInsertTemplateInteractionRequest.item._X_FLAG_REGISTERED = Claro.Constants.NumberZeroString;
                                objInsertTemplateInteractionRequest.item._X_FLAG_LEGAL_REP = Claro.Constants.NumberZeroString;
                                objInsertTemplateInteractionRequest.item._X_FLAG_OTHER = Claro.Constants.NumberOneString;
                                objInsertTemplateInteractionRequest.item._X_INTER_17 = objRequest.parient;
                                objInsertTemplateInteractionRequest.item._X_INTER_30 = objRequest.listLegalAgent;
                                break;
                        }
                        Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "IN Business GetInsertEquipmentForeign >  Entrando a GetInsertInteractionTemplate()");
                        COMMON.GetInsertTemplateInteraction.InsertTemplateInteractionResponse objInsertTemplateInteractionResponse = new COMMON.GetInsertTemplateInteraction.InsertTemplateInteractionResponse();
                        objInsertTemplateInteractionResponse = GetInsertInteractionTemplate(objInsertTemplateInteractionRequest);

                        if (objInsertTemplateInteractionResponse.ProcesSucess == true)
                        {
                            PROSPAID.GetTypeTransactionBRMS.TypeTransactionBRMSRequest objTypeTransactionBRMSRequest = new PROSPAID.GetTypeTransactionBRMS.TypeTransactionBRMSRequest()
                            {
                                Audit = objRequest.Audit,
                                StrIdentifier = objRequest.customerTelephone,
                                StrOperationCodSubClass = objTypificationResponse.ListTypification[0].SUBCLASE_CODE,
                                StrTransactionM = KEY.AppSettings("strTransaccion")

                            };
                            Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "IN Business GetInsertEquipmentForeign >  Entrando a GetTypeTransactionBRMS()");
                            PROSPAID.GetTypeTransactionBRMS.TypeTransactionBRMSResponse objTypeTransactionBRMSResponse = new PROSPAID.GetTypeTransactionBRMS.TypeTransactionBRMSResponse();
                            objTypeTransactionBRMSResponse = PostBusinessTransacService.GetTypeTransactionBRMS(objTypeTransactionBRMSRequest);

                            if (objTypeTransactionBRMSResponse.StrResult != null)
                            {
                                string strServidorLeerPDF = KEY.AppSettings("strServidorLeerPDF");
                                string strCarpetaPDFs = KEY.AppSettings("strCarpetaPDFsSiacu");
                                string strCarpetaTransaccion = KEY.AppSettings("strCarpetaTransaccionRegDesEquMov");
                                var strTerminacionPDF = KEY.AppSettings("strTerminacionPDF");
                                string strCasoInter = objInsertIntResponse.Interactionid;
                                string strTransactionName = objTypeTransactionBRMSResponse.StrResult;

                                COMMON.GetGenerateConstancy.GenerateConstancyRequest objGenerateConstancyRequest = new COMMON.GetGenerateConstancy.GenerateConstancyRequest()
                                {
                                    Audit = objRequest.Audit,
                                    ParametersGeneratePDFGeneric = new ParametersGeneratePDF()
                                    {
                                        StrCarpetaPDFs = strCarpetaPDFs,
                                        StrCarpetaTransaccion = strCarpetaTransaccion,
                                        StrCasoInter = strCasoInter,
                                        StrNombreArchivoTransaccion = strTransactionName,
                                        StrNombresApellidosCliente = objRequest.customerFullName,
                                        StrFechaTransaccionProgram = DateTime.Now.ToString("dd/MM/yyyy"),
                                        StrRepresLegal = objRequest.firstName + " " + objRequest.lastName,
                                        StrNumeroLinea = objRequest.customerTelephone,
                                        StrTipoDocIdentidad = objRequest.documentTypeText,
                                        StrNroCaso = objInsertIntResponse.Interactionid,
                                        StrNroDocIdentidad = objRequest.documentNumber,
                                        StrCentroAtencionArea = objRequest.nameCac,
                                        StrImeiEquipos = objRequest.imei,
                                        StrImeiFisico = objRequest.imeiFisico,
                                        StrMarcaModelo = objRequest.markModel,
                                        StrLineasAsociadas = objRequest.customerTelephone,
                                        StrCodigoAsesor = objRequest.codeadviser,
                                        StrNombreAsesor = objRequest.adviser,
                                        StrFlagFirmaDigital = objRequest.flagFirmaDigital,
                                        StrNroSolicitud = string.Empty,
                                        StrNombresSolicitante = objRequest.firstName + " " + objRequest.lastName,
                                        StrNroDocSolicitante = objRequest.documentNumber,
                                        StrFechaHora = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"),
                                        StrHuellaMinuciaCliente = objRequest.strHuellaMinucia,
                                        StrHuellaEncode = objRequest.strHuellaEncode,
                                    }
                                };
                                Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "IN Business GetInsertEquipmentForeign >  Entrando a GetGenerateContancyPDF()");
                                COMMON.GetGenerateConstancy.GenerateConstancyResponse objGenerateConstancyResponse = new COMMON.GetGenerateConstancy.GenerateConstancyResponse();
                                objGenerateConstancyResponse = GetGenerateContancyPDF(objGenerateConstancyRequest);

                                if (objGenerateConstancyResponse.Generated == true)
                                {
                                    string strFechaTransaccion = DateTime.Today.ToShortDateString().Replace("/", "_");
                                    string strNamePDF = string.Format("{0}{1}{2}{3}_{4}_{5}_{6}.pdf", strServidorLeerPDF, strCarpetaPDFs, strCarpetaTransaccion, strCasoInter, strFechaTransaccion, strTransactionName.Replace("/", "_"), strTerminacionPDF);
                                    string strNamePath = string.Format("{0}{1}{2}", strServidorLeerPDF, strCarpetaPDFs, strCarpetaTransaccion);
                                    string strDocumentName = string.Format("{0}_{1}_{2}_{3}", strCasoInter, strFechaTransaccion, strTransactionName, strTerminacionPDF);

                                    COMMON.GetInsertEvidence.InsertEvidenceResponse oInsertEvidenceResponse = new COMMON.GetInsertEvidence.InsertEvidenceResponse();
                                    COMMON.GetInsertEvidence.InsertEvidenceRequest oInsertEvidenceRequest = new COMMON.GetInsertEvidence.InsertEvidenceRequest();

                        
                                    oInsertEvidenceRequest.Audit = objRequest.Audit;
                                    oInsertEvidenceRequest.Evidence = new Evidence();
                                    oInsertEvidenceRequest.Evidence.StrTransactionType = KEY.AppSettings("strTransactionType");
                                    oInsertEvidenceRequest.Evidence.StrTransactionCode = strCasoInter;
                                    oInsertEvidenceRequest.Evidence.StrCustomerCode = Convert.ToString(objRequest.customerId);
                                    oInsertEvidenceRequest.Evidence.StrPhoneNumber = objRequest.customerTelephone; //PRE-POST: MovilNumber HFC-LTE: Contract
                                    oInsertEvidenceRequest.Evidence.StrTypificationCode = objTypificationResponse.ListTypification[0].SUBCLASE_CODE;
                                    oInsertEvidenceRequest.Evidence.StrTypificationDesc = objTypificationResponse.ListTypification[0].SUBCLASE;
                                    oInsertEvidenceRequest.Evidence.StrCommercialDesc = objTypificationResponse.ListTypification[0].SUBCLASE;
                                    oInsertEvidenceRequest.Evidence.StrProductType = string.Empty;
                                    oInsertEvidenceRequest.Evidence.StrServiceChannel = objRequest.typeCac;
                                    oInsertEvidenceRequest.Evidence.StrTransactionDate = DateTime.Today.ToShortDateString();
                                    oInsertEvidenceRequest.Evidence.StrActivationDate = null;
                                    oInsertEvidenceRequest.Evidence.StrSuspensionDate = null;
                                    oInsertEvidenceRequest.Evidence.StrServiceStatus = objRequest.strStatusLinea;

                                    oInsertEvidenceRequest.Evidence.StrDocumentPath = strNamePath;
                                    oInsertEvidenceRequest.Evidence.StrUserName = objRequest.userAccesslogin;
                                    oInsertEvidenceRequest.Evidence.StrDocumentName = strDocumentName;
                                    oInsertEvidenceRequest.Evidence.StrDocumentType = strTransactionName;
                                    oInsertEvidenceRequest.Evidence.StrDocumentExtension = KEY.AppSettings("strDocumentoExtension");
                                  
                                    try
                                    {
                                        Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "IN Business GetInsertEquipmentForeign >  Entrando a GetInsertEvidence()");
                                        oInsertEvidenceResponse = Claro.Web.Logging.ExecuteMethod<COMMON.GetInsertEvidence.InsertEvidenceResponse>(() =>
                                        {
                                            return GetInsertEvidence(oInsertEvidenceRequest);
                                        });
                                        Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "OUT Business GetInsertEquipmentForeign > GetInsertEvidence() Ok : " + oInsertEvidenceResponse.StrMsgText);
                                    }
                                    catch (Exception ex)
                                    {
                                        Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, "ERROR Business GetInsertEquipmentForeign > GetInsertEvidence() > " + ex.Message);
                                        Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
                                    }
                                   
                                    objResponse.ProcesSucess = Claro.Web.Logging.ExecuteMethod<bool>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                                    {
                                        return Claro.SIACU.Data.Transac.Service.Common.GetInsertEquipmentForeign(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.item, ref rCodeResult, ref rMsgText);
                                    });
                                    objResponse.codeResult = rCodeResult;
                                    objResponse.msgResult = rMsgText;
                                    objResponse.namePDF = strNamePDF;
                                }
                                else
                                {
                                    objResponse.ProcesSucess = false;
                                    objResponse.codeResult = 1;
                                    objResponse.codeFailed = 4;

                                    Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, "Error al generar constancia." + objGenerateConstancyResponse.ErrorMessage);

                                }
                            }
                            else
                            {
                                objResponse.ProcesSucess = false;
                                objResponse.codeResult = 1;
                                objResponse.codeFailed = 3;
                                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, "Error al Obtener datos del BRMS. " + objTypeTransactionBRMSResponse.StrError);
                            }

                        }
                        else
                        {
                            objResponse.ProcesSucess = false;
                            objResponse.codeResult = 1;
                            objResponse.codeFailed = 2;
                            Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, "Error al registrar la Interacción Detalle.");
                        }

                    }
                    else
                    {
                        objResponse.ProcesSucess = false;
                        objResponse.codeResult = 1;
                        objResponse.codeFailed = 1;
                        Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, "Error al registrar la Interacción." + objInsertIntResponse.MsgText);
                    }
                }
                else
                {
                    objResponse.ProcesSucess = true;
                    objResponse.codeResult = 2;
                    Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "OUT Business GetInsertEquipmentForeign > Saliendo de GetListEquipmentRegistered() rCodeResult=  " + objResponse.codeResult);
                    Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "OUT Business GetInsertEquipmentForeign > Saliendo de GetListEquipmentRegistered() ProcesSucess=  " + objResponse.ProcesSucess);
                }          
             
            }
            catch (Exception ex)
            {
                objResponse.ProcesSucess = false;
                objResponse.codeResult = 1;
                objResponse.codeFailed = 999;                
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;

        }

        public static COMMON.GetListEquipmentRegistered.ListEquipmentRegisteredResponse GetListEquipmentRegistered(COMMON.GetListEquipmentRegistered.ListEquipmentRegisteredRequest objRequest)
        {
            int rCodeResult = 0;
            string rMsgText = "";
            Web.Logging.Info(objRequest.SessionId, objRequest.TransactionId, "entro business GetListEquipmentRegistered ");
            var objResponse = new COMMON.GetListEquipmentRegistered.ListEquipmentRegisteredResponse();
            try
            {
                Web.Logging.Info(objRequest.SessionId, objRequest.TransactionId, "entro business GetListEquipmentRegistered  entrando a conexion Data ");
                objResponse.ListEquipmentRegistered = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Common.GetListEquipmentRegistered(objRequest.SessionId, objRequest.TransactionId, objRequest.CustomerId, objRequest.Imei,  objRequest.EstadoId, objRequest.NumMaximo, ref  rCodeResult, ref  rMsgText);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }
            Web.Logging.Info(objRequest.SessionId, objRequest.TransactionId, "entro business GetListEquipmentRegistered rCodeResult=  " + rCodeResult);
            objResponse.CodeError = rCodeResult;
            Web.Logging.Info(objRequest.SessionId, objRequest.TransactionId, "entro business GetListEquipmentRegistered  rMsgText=  " + rMsgText);
            objResponse.MsgError = rMsgText;
            Web.Logging.Info(objRequest.SessionId, objRequest.TransactionId, "entro business GetListEquipmentRegistered retorna response  ");
            return objResponse;
        }

        public static COMMON.GetConsultByGroupParameter.ConsultByGroupParameterResponse GetConsultByGroupParameter(COMMON.GetConsultByGroupParameter.ConsultByGroupParameterRequest objRequest)
        {
            var objResponse = new COMMON.GetConsultByGroupParameter.ConsultByGroupParameterResponse();
            try
            {
                objResponse.ListConsultByGroupParameter = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Common.GetConsultByGroupParameter(objRequest.SessionId, objRequest.TransactionId, objRequest.intCodGrupo);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }
        public static string GetIdTrazabilidad(string strIdSession, string strTransaction, Int32 intCodGrupo)
        {
            string objResponse = string.Empty;
            string rResult = "";
            try
            {
                objResponse = Web.Logging.ExecuteMethod(strIdSession, strTransaction, () =>
                {
                    return Data.Transac.Service.Common.GetIdTrazabilidad(strIdSession, strTransaction, intCodGrupo);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
                throw ex;
            }
            return objResponse;
        }

        public static Entity.Transac.Service.Common.GetBlackList.BlackListResponse GetBlackListOsiptel(Entity.Transac.Service.Common.GetBlackList.BlackListRequest objBlackListRequest)
        {
            Claro.Web.Logging.Info(objBlackListRequest.Audit.Session, objBlackListRequest.Audit.Transaction, "Entrada: business Método: GetBlackListOsiptel");
            COMMON.GetBlackList.BlackListResponse objResponse = null;
            try
            {
               objResponse = Claro.Web.Logging.ExecuteMethod(objBlackListRequest.Audit.Session, objBlackListRequest.Audit.Transaction, () =>
                {
                   return Data.Transac.Service.Common.GetBlackListOsiptel(objBlackListRequest);
                });

               if (objResponse!= null)
               {
                   if (objResponse.MessageResponseBlackList.Body.responseStatus != null)
                   {
                       Web.Logging.Info(objBlackListRequest.Audit.Session, objBlackListRequest.Audit.Transaction, "Parametros Salida: Data Método: GetBlackList ::: estado: " + objResponse.MessageResponseBlackList.Body.responseStatus.estado);
                       Web.Logging.Info(objBlackListRequest.Audit.Session, objBlackListRequest.Audit.Transaction, "Parametros Salida: Data Método: GetBlackList ::: codigoRespuesta: " + objResponse.MessageResponseBlackList.Body.responseStatus.codigoRespuesta);
                       Web.Logging.Info(objBlackListRequest.Audit.Session, objBlackListRequest.Audit.Transaction, "Parametros Salida: Data Método: GetBlackList ::: descripcionRespuesta: " + objResponse.MessageResponseBlackList.Body.responseStatus.descripcionRespuesta); 
                   }
               }  
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objBlackListRequest.Audit.Session, objBlackListRequest.Audit.Transaction, ex.Message);
            }
            return objResponse;
        }

        #region TOA

        public static COMMON.GetTimeZones.TimeZonesResponse GetTimeZones(COMMON.GetTimeZones.TimeZonesRequest RequestParam)
        {
            List<COMMON.TimeZone> listServiceResponse = null;
            List<COMMON.TimeZone> listService = null;
            List<COMMON.TimeZone> listServiceOrdered;

            try
            {
                listService = Claro.Web.Logging.ExecuteMethod<List<COMMON.TimeZone>>(RequestParam.Audit.Session, RequestParam.Audit.Transaction, () => { return Data.Transac.Service.Common.GetTimeZones(RequestParam.Audit.Session, RequestParam.Audit.Transaction, RequestParam.strAnUbigeo, RequestParam.strAnTiptra, RequestParam.strAdFecagenda); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(RequestParam.Audit.Session, RequestParam.Audit.Transaction, ex.Message);
            }

            if (listService != null)
            {
                listServiceResponse = new List<COMMON.TimeZone>();
                listServiceOrdered = listService.OrderBy(a => a.CODCON).ToList();
                listServiceResponse = listServiceOrdered;
            }

            COMMON.GetTimeZones.TimeZonesResponse Resultado = new COMMON.GetTimeZones.TimeZonesResponse() { TimeZones = listServiceResponse };

            return Resultado;
        }

        public static COMMON.GetETAAuditRequestCapacity.ETAAuditResponseCapacity GetETAAuditRequestCapacity(COMMON.GetETAAuditRequestCapacity.ETAAuditRequestCapacity objETAAuditRequestCapacity)
        {
            var objResponse = new COMMON.GetETAAuditRequestCapacity.ETAAuditResponseCapacity();

            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(objETAAuditRequestCapacity.Audit.Session, objETAAuditRequestCapacity.Audit.Transaction, () => { return Data.Transac.Service.Common.ConsultarCapacidadCuadrillas(objETAAuditRequestCapacity); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objETAAuditRequestCapacity.Audit.Session, objETAAuditRequestCapacity.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }

        public static int RegisterEtaRequest(COMMON.GetRegisterEta.RegisterEtaRequest objRegisterEtaRequest)
        {
            int vidreturn = 0;

            try
            {
                vidreturn = Claro.Web.Logging.ExecuteMethod(objRegisterEtaRequest.Audit.Session, objRegisterEtaRequest.Audit.Transaction, () => { return Data.Transac.Service.Common.RegisterEtaRequest(objRegisterEtaRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRegisterEtaRequest.Audit.Session, objRegisterEtaRequest.Audit.Transaction, ex.Message);
            }

            return vidreturn;
        }

        public static string RegisterEtaResponse(COMMON.GetRegisterEta.RegisterEtaResponse objRegisterEtaResponse)
        {
            string vidreturn = string.Empty;

            try
            {
                vidreturn = Claro.Web.Logging.ExecuteMethod(objRegisterEtaResponse.Audit.Session, objRegisterEtaResponse.Audit.Transaction, () => { return Data.Transac.Service.Common.RegisterEtaResponse(objRegisterEtaResponse); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRegisterEtaResponse.Audit.Session, objRegisterEtaResponse.Audit.Transaction, ex.Message);
            }

            return vidreturn;
        }

        public static COMMON.GetTransactionScheduled.TransactionScheduledResponse GetSchedulingRule(COMMON.GetTransactionScheduled.TransactionScheduledRequest objRequest)
        {
            var objResponse = new COMMON.GetTransactionScheduled.TransactionScheduledResponse();

            try
            {
                objResponse.ListTransactionScheduled = Claro.Web.Logging.ExecuteMethod<List<COMMON.TransactionScheduled>>(objRequest.Audit.Session, objRequest.Audit.Transaction, () => { return Claro.SIACU.Data.Transac.Service.Common.GetSchedulingRule(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.vstrIdParametro); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }

        #endregion

        public static Entity.Transac.Service.Common.GetPuntosAtencion.responseDataObtenerTabPuntosAtencionPost GetPuntosAtencion(COMMON.GetPuntosAtencion.PuntosAtencionRequest objRequest)
        {
            Entity.Transac.Service.Common.GetPuntosAtencion.responseDataObtenerTabPuntosAtencionPost objResponse = new Entity.Transac.Service.Common.GetPuntosAtencion.responseDataObtenerTabPuntosAtencionPost();
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Common.GetPuntosAtencion(objRequest);
                });

            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);


            }
            return objResponse;
        }
 
        /*PROY-32650 F2*/
        public static COMMON.GetReceipts.ReceiptsResponse GetParamsBSCS(COMMON.GetReceipts.ReceiptsRequest objRequest)
        {
            var objResponse = new COMMON.GetReceipts.ReceiptsResponse();
            try
            {
                var rMsgText = string.Empty;

                objResponse.LstReceipts = Web.Logging.ExecuteMethod<List<Claro.SIACU.Entity.Transac.Service.Fixed.GenericItem>>(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Data.Transac.Service.Common.GetParamsBSCS(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.vCODCLIENTE, ref rMsgText);
                    });

                objResponse.MSG_ERROR = rMsgText;
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }
        //GetParamsBSCS

        public static COMMON.GetOffice.OfficeResponse GetOffice(COMMON.GetOffice.OfficeRequest objOfficeRequest)
        {
            var objOfficeResponse = new COMMON.GetOffice.OfficeResponse();

            objOfficeResponse = Web.Logging.ExecuteMethod(objOfficeRequest.Audit.Session, objOfficeRequest.Audit.Transaction, () =>
            {
                return Data.Transac.Service.Common.GetOffice(objOfficeRequest);
            });
            return objOfficeResponse;
        }

        public static COMMON.GetConsultNationality.ConsultNationalityResponse GetNationalityList(COMMON.GetConsultNationality.ConsultNationalityRequest objRequest)
        {
            COMMON.GetConsultNationality.ConsultNationalityResponse objResponse = new COMMON.GetConsultNationality.ConsultNationalityResponse()
            {
                ListConsultNationality = Claro.Web.Logging.ExecuteMethod<List<COMMON.ListItem>>(objRequest.Audit.Session, objRequest.Audit.Transaction, () => { return Data.Transac.Service.Common.GetNationalityList(objRequest.Audit.Session, objRequest.Audit.Transaction); })
            };
            return objResponse;
        }

        public static COMMON.GetCivilStatus.CivilStatusResponse GetCivilStatusList(COMMON.GetCivilStatus.CivilStatusRequest objRequest)
        {
            COMMON.GetCivilStatus.CivilStatusResponse objResponse = new COMMON.GetCivilStatus.CivilStatusResponse()
            {
                ListCivilStatus = Claro.Web.Logging.ExecuteMethod<List<COMMON.ListItem>>(objRequest.Audit.Session, objRequest.Audit.Transaction, () => { return Data.Transac.Service.Common.GetCivilStatusList(objRequest.Audit.Session, objRequest.Audit.Transaction); })

            };
            return objResponse;
        }

        #region  PROY-140245-IDEA140240

        public static COMMON.GetValidateCollaborator.GetValidateCollaboratorResponse GetValidateCollaborator(COMMON.GetValidateCollaborator.GetValidateCollaboratorRequest objRequest, string strIdSession)
        {
            Entity.Transac.Service.Common.GetValidateCollaborator.GetValidateCollaboratorResponse objResponse = null;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() => Data.Transac.Service.Common.GetValidateCollaborator(objRequest, strIdSession));
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, "", ex.Message);
                objResponse = null;
            }
            return objResponse;
        }

        public static COMMON.GetConsultServiceBono.GetConsultServiceBonoResponse GetConsultServiceBono(COMMON.GetConsultServiceBono.GetConsultServiceBonoRequest objRequest, string strIdSession)
        {
            Entity.Transac.Service.Common.GetConsultServiceBono.GetConsultServiceBonoResponse objResponse = null;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() => Data.Transac.Service.Common.GetConsultServiceBono(objRequest, strIdSession));
               
            }catch(Exception ex){
                Web.Logging.Error(strIdSession, "", ex.Message);
                objResponse = null;
            }
            return objResponse;
        }

        public static COMMON.GetRegisterBonoSpeed.GetRegisterBonoSpeedResponse GetRegisterBonoSpeed(COMMON.GetRegisterBonoSpeed.GetRegisterBonoSpeedRequest objRequest, string strIdSession)
        {
            Entity.Transac.Service.Common.GetRegisterBonoSpeed.GetRegisterBonoSpeedResponse objResponse = null;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() => Data.Transac.Service.Common.GetRegisterBonoSpeed(objRequest, strIdSession));
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, "", ex.Message);
                objResponse = null;
            }
            return objResponse;
        }

        public static COMMON.PostValidateDeliveryBAV.PostValidateDeliveryBAVResponse PostValidateDeliveryBAV(COMMON.PostValidateDeliveryBAV.PostValidateDeliveryBAVRequest objRequest, string strIdSession)
        {
            Entity.Transac.Service.Common.PostValidateDeliveryBAV.PostValidateDeliveryBAVResponse objResponse = null;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() => Data.Transac.Service.Common.PostValidateDeliveryBAV(objRequest, strIdSession));

            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, "", ex.Message);
                objResponse = null;

            }
            return objResponse;
        }

        public static COMMON.GetConsultCampaign.GetConsultCampaignResponse GetConsultCampaign(COMMON.GetConsultCampaign.GetConsultCampaignRequest objRequest, string strIdSession)
        {
            Entity.Transac.Service.Common.GetConsultCampaign.GetConsultCampaignResponse objResponse = null;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() => Data.Transac.Service.Common.GetConsultCampaign(objRequest, strIdSession));
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, "", ex.Message);
                objResponse = null;
            }
            return objResponse;
        }

        public static COMMON.GetRegisterCampaign.GetRegisterCampaignResponse GetRegisterCampaign(COMMON.GetRegisterCampaign.GetRegisterCampaignRequest objRequest, string strIdSession)
        {
            Entity.Transac.Service.Common.GetRegisterCampaign.GetRegisterCampaignResponse objResponse = null;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() => Data.Transac.Service.Common.GetRegisterCampaign(objRequest, strIdSession));
                if (objResponse.RegisterCampaignMessageResponse.RegisterCampaignBodyResponse.RegisterCampaignResponse.RegisterCampaignAuditResponse.CodeResponse != "0")
                {
                    EmailSubmitRegistreCampaign(objRequest, strIdSession);
                }
            }
            catch (Exception ex)
            {
                EmailSubmitRegistreCampaign(objRequest, strIdSession);
                Web.Logging.Error(strIdSession, "", ex.Message);
                objResponse = null;
            }
            return objResponse;
        }

        public static GetReadDataUserResponse GetReadDataUser(GetReadDataUserRequest objRequest, string strIdSession)
        {

            GetReadDataUserResponse objResponse = null;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() => Data.Transac.Service.Common.GetReadDataUser(objRequest, strIdSession));
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, "", ex.Message);
                objResponse = null;
            }
            return objResponse;
        }

        public static COMMON.GetValidateQuantityCampaign.GetValidateQuantityCampaignResponse GetValidateQuantityCampaign(COMMON.GetValidateQuantityCampaign.GetValidateQuantityCampaignRequest objRequest, string strIdSession)
        {
            Entity.Transac.Service.Common.GetValidateQuantityCampaign.GetValidateQuantityCampaignResponse objResponse = null;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() => Data.Transac.Service.Common.GetValidateQuantityCampaign(objRequest, strIdSession));
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, objRequest.ValidateQuantityCampaignMessageRequest.ValidateQuantityCampaignBodyRequest.ValidateQuantityCampaignRequest.ValidateQuantityCampaignAuditRequest.IdTransaction, ex.Message);
                objResponse = null;
            }
            return objResponse;
        }

        public static COMMON.GetSubmitNotificationEmail.EmailSubmitResponse EmailSubmit(COMMON.GetSubmitNotificationEmail.EmailSubmitRequest objRequest)
        {
            COMMON.GetSubmitNotificationEmail.EmailSubmitResponse objResponse = null;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() => Data.Transac.Service.Common.EmailSubmit(objRequest));
            }
            catch (Exception ex)
            {
                Web.Logging.Error("", "", ex.Message);
                objResponse = null;
            }
            return objResponse;
        }

        public static COMMON.GetSubmitNotificationEmail.EmailSubmitResponse EmailSubmitRegistreCampaign(COMMON.GetRegisterCampaign.GetRegisterCampaignRequest objRequest, string strIdSession)
        {
            COMMON.GetSubmitNotificationEmail.EmailSubmitResponse objResponse = null;
            try
            {
                var audit = objRequest.RegisterCampaignMessageRequest.RegisterCampaignBodyRequest.RegisterCampaignRequest.RegisterCampaignAuditRequest;
                var nroContrato = objRequest.RegisterCampaignMessageRequest.RegisterCampaignBodyRequest.RegisterCampaignRequest.RegisterCampaign.CoId;
                string content = string.Format(KEY.AppSettings("strContentEnviarNotificacion"), audit.IdTransaction, nroContrato, DateTime.Now.ToString());
                COMMON.GetSubmitNotificationEmail.EmailSubmitRequest objEmailSubmitRequest = new COMMON.GetSubmitNotificationEmail.EmailSubmitRequest()
                {
                    Audit = new Claro.Entity.AuditRequest()
                   {
                       ApplicationName = audit.NameAplication,
                       IPAddress = audit.IpAplication,
                       Session = strIdSession,
                       Transaction = audit.IdTransaction,
                       UserName = audit.UserAplication
                   },
                    Header = new COMMON.GetDataPower.HeadersRequest()
                    {
                        HeaderRequest = objRequest.RegisterCampaignMessageRequest.Header.HeaderRequest
                    },
                    CampaignName = objRequest.RegisterCampaignMessageRequest.RegisterCampaignBodyRequest.RegisterCampaignRequest.RegisterCampaign.CampaignDescription,
                    CampaignDesc = KEY.AppSettings("strCampaignDesc"),
                    CampaignCategory = KEY.AppSettings("strCampaignCategory"),
                    PromotionalCategory = KEY.AppSettings("strPromotionalCategory"),
                    CallBackURL = KEY.AppSettings("strCallBackURL"),
                    ScheduledDateTime = String.Format("{0:s}z", DateTime.Now),
                    ExpiryDateTime = "",
                    SenderRequest = new COMMON.GetSubmitNotificationEmail.SenderRequest()
                    {
                        Username = KEY.AppSettings("strUsernameEnviarNotificacion"),
                        Password = KEY.AppSettings("strPasswordEnviarNotificacion"),
                        FromAddress = KEY.AppSettings("strFromAddressEnviarNotificacion"),
                        DisplayName = KEY.AppSettings("strDisplayNameEnviarNotificacion"),
                    },
                    MsgDetailsResquest = new COMMON.GetSubmitNotificationEmail.MsgDetailsResquest()
                    {
                        Subject = KEY.AppSettings("strSubjectEnviarNotificacion"),
                        SimpleRequest = new COMMON.GetSubmitNotificationEmail.SimpleRequest()
                        {
                            Content = content,
                            RecipientsRequest = new COMMON.GetSubmitNotificationEmail.RecipientsRequest()
                            {
                                Recipient = KEY.AppSettings("strRecipientEnviarNotificacion"),
                            }
                        },
                        FallbackRequest = new COMMON.GetSubmitNotificationEmail.FallbackRequest()
                        {

                        },

                        DeliveryReport = true,
                    }
                };
                objResponse = EmailSubmit(objEmailSubmitRequest);
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, objRequest.RegisterCampaignMessageRequest.RegisterCampaignBodyRequest.RegisterCampaignRequest.RegisterCampaignAuditRequest.IdTransaction, ex.Message);
                objResponse = null;
            }
            return objResponse;
        }
        #endregion


public static COMMON.GetGenerateConstancy.GenerateConstancyResponse GenerateContancyWithXml(COMMON.GetGenerateConstancy.GenerateConstancyRequest request, string xml)
        {
            string errorMessage = string.Empty;

            COMMON.GetGenerateConstancy.GenerateConstancyResponse objResponse = new COMMON.GetGenerateConstancy.GenerateConstancyResponse()
            {
                Generated = Claro.Web.Logging.ExecuteMethod<bool>(request.Audit.Session, request.Audit.Transaction,
                () =>
                {
                    return Data.Transac.Service.Common.GenerateContancyWithXml(request.Audit.Session, request.Audit.Transaction,
                        request.ParametersGeneratePDFGeneric, ref errorMessage, xml);
                })
            };

            objResponse.ErrorMessage = errorMessage;

            return objResponse;
        }

        //PROY-32650
         
        public static COMMON.CheckingUser.CheckingUserResponse CheckingUser(COMMON.CheckingUser.CheckingUserRequest objCheckingUserRequest)
        {

            COMMON.CheckingUser.CheckingUserResponse objCheckingUserResponse;

            objCheckingUserResponse = Claro.Web.Logging.ExecuteMethod<COMMON.CheckingUser.CheckingUserResponse>(objCheckingUserRequest.Audit.Session, objCheckingUserRequest.Audit.Transaction, () =>
            {
                return Data.Transac.Service.Common.CheckingUser(objCheckingUserRequest.Audit.Session, objCheckingUserRequest.Audit.Transaction, objCheckingUserRequest.IpAplicacion, objCheckingUserRequest.Audit.ApplicationName, objCheckingUserRequest.Usuario, objCheckingUserRequest.AppCod);
            });

            return objCheckingUserResponse;
        }

        public static COMMON.GetEmployeByUser.EmployeeResponse GetEmployeByUserwithDP(COMMON.GetEmployeByUser.EmployeeRequest objEmployeeRequest)
        {
            COMMON.GetEmployeByUser.EmployeeResponse objEmployeeResponse = new COMMON.GetEmployeByUser.EmployeeResponse();
            string User = string.Empty;
            string pass = string.Empty;

            bool result = Data.Transac.Service.Common.IsOkGetKey(objEmployeeRequest.Audit.Session, objEmployeeRequest.Audit.Transaction, objEmployeeRequest.Audit.IPAddress, objEmployeeRequest.Audit.IPAddress, objEmployeeRequest.UserName, objEmployeeRequest.idAplicacion, out User, out pass);

            if (result)
            {

                objEmployeeResponse.lstEmployee = Claro.Web.Logging.ExecuteMethod<List<COMMON.Employee>>(objEmployeeRequest.Audit.Session, objEmployeeRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Common.GetEmployeByUserwithDP(objEmployeeRequest.Audit.Session, objEmployeeRequest.Audit.Transaction, objEmployeeRequest.UserName, objEmployeeRequest.Audit.IPAddress, objEmployeeRequest.Audit.ApplicationName, User, pass);
                });
            }


            return objEmployeeResponse;
        }

        public static COMMON.ReadOptionsByUser.ReadOptionsByUserResponse ReadOptionsByUserwithDP(COMMON.ReadOptionsByUser.ReadOptionsByUserRequest objReadOptionsByUserRequest)
        {

            COMMON.ReadOptionsByUser.ReadOptionsByUserResponse objReadOptionsByUserResponse = new COMMON.ReadOptionsByUser.ReadOptionsByUserResponse();
            string User = string.Empty;
            string pass = string.Empty;
            bool result = Data.Transac.Service.Common.IsOkGetKey(objReadOptionsByUserRequest.Audit.Session, objReadOptionsByUserRequest.Audit.Transaction, objReadOptionsByUserRequest.Audit.IPAddress, objReadOptionsByUserRequest.Audit.IPAddress, objReadOptionsByUserRequest.Audit.UserName, objReadOptionsByUserRequest.IdAplicacion, out User, out pass);

            if (result)
            {
                objReadOptionsByUserResponse.ListOption = Claro.Web.Logging.ExecuteMethod<List<COMMON.PaginaOption>>(objReadOptionsByUserRequest.Audit.Session, objReadOptionsByUserRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Common.ReadOptionsByUserwithDP(objReadOptionsByUserRequest.Audit.Session, objReadOptionsByUserRequest.Audit.Transaction, objReadOptionsByUserRequest.Audit.UserName, objReadOptionsByUserRequest.Audit.IPAddress, objReadOptionsByUserRequest.Audit.ApplicationName, objReadOptionsByUserRequest.IdAplication, objReadOptionsByUserRequest.IdUser, User, pass);
                });
            }
            return objReadOptionsByUserResponse;
        }
        //PROY-32650
		
		
		
		        #region OnBase
        public static Entity.Transac.Service.Common.TargetOnBase.OnBaseCargaResponse TargetDocumentoOnBase(Entity.Transac.Service.Common.TargetOnBase.OnBaseCargaRequest objRequest)
        {
            return Data.Transac.Service.Common.TargetDocumentoOnBase(objRequest, GetCredentials(KEY.AppSettings("LeyPromoCargaOnBaseUsuario"), KEY.AppSettings("LeyPromoCargaOnBaseClave")));
        }


        public static UploadDocumentOnBaseResponse GetUploadDocumentOnBase(UploadDocumentOnBaseRequest UploadDocumentOnBaseRequest)
        {
            return Data.Transac.Service.Common.GetUploadDocumentOnBase(UploadDocumentOnBaseRequest);
        }

        #endregion

        #region DataPower
        public static System.Collections.Hashtable GetCredentials(string usuario, string clave)
        {
            System.Collections.Hashtable objCredentials = new System.Collections.Hashtable();

            objCredentials.Add("idAplicacion", KEY.AppSettings("idAplicacion"));
            objCredentials.Add("codigoAplicacion", KEY.AppSettings("codigoAplicacion"));
            objCredentials.Add("usuarioAplicacionEncriptado", usuario);
            objCredentials.Add("claveEncriptado", clave);
            return objCredentials;
        }

        #endregion

        public static Boolean UploadSftp(Tools.Entity.AuditRequest objAuditRequest, ConnectionSFTP objConnectionSFTP, string fileName, byte[] objFile)
        {
            Boolean status = false;

            try
            {
                status = Data.Transac.Service.Common.UploadSftp(objAuditRequest, objConnectionSFTP, fileName, objFile);

            }
            catch (Exception ex)
            {
                status = false;
                throw ex;
            }

            return status;
        }
        public static COMMON.GetGenerateConstancy.GenerateConstancyResponse GetGenerateContancyNamePDF(COMMON.GetGenerateConstancy.GenerateConstancyRequest request,string NombrePDF)
        {
            string errorMessage = string.Empty;

            COMMON.GetGenerateConstancy.GenerateConstancyResponse objResponse = new COMMON.GetGenerateConstancy.GenerateConstancyResponse()
            {
                Generated = Claro.Web.Logging.ExecuteMethod<bool>(request.Audit.Session, request.Audit.Transaction,
                () =>
                {
                    return Data.Transac.Service.Common.GenerateConstancyNamePDF(request.Audit.Session, request.Audit.Transaction,
                        request.ParametersGeneratePDFGeneric, ref errorMessage, NombrePDF);
                })
            };

            objResponse.ErrorMessage = errorMessage;

            return objResponse;
        }
    }
}

