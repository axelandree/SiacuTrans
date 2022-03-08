using System;
using System.ServiceModel;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Claro.SIACU.Entity.Transac.Service.Common.GetInsertInteractHFC;
using Claro.SIACU.Entity.Transac.Service.Common.GetUser;
using COMMON = Claro.SIACU.Entity.Transac.Service.Common;
using EntitiesCommon = Claro.SIACU.Entity.Transac.Service.Common;
using Claro.SIACU.Entity.Transac.Service.Common;
using Claro.SIACU.Entity.Transac.Service.Common.GetContractByPhoneNumber;
using Claro.SIACU.Entity.Transac.Service.Common.GetDatBscsExt;
using Claro.SIACU.Entity.Transac.Service.Common.GetRegisterServiceCommercial;
using Claro.SIACU.Entity.Transac.Service.Common.GetUpdateInter30;
using Claro.SIACU.ProxyService.Transac.Service.ServiciosPostpagoWS;
using FUNCTIONS = Claro.SIACU.Transac.Service;
using Claro.SIACU.Entity.Transac.Service.Common.GetQuestionsAnswerSecurity;
using Claro.SIACU.Entity.Transac.Service.Common.GetValidateCollaborator;
using Claro.SIACU.Entity.Transac.Service.Common.GetReadDataUser;
using Claro.SIACU.Entity.Transac.Service.Common.GetUploadDocumentOnBase;

namespace Claro.SIACU.Web.Service.Transac.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CommonTransacService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select CommonTransacService.svc or CommonTransacService.svc.cs at the Solution Explorer and start debugging.
    public class CommonTransacService : ICommonTransacService
    {
        

        public Entity.Transac.Service.Common.GetMotiveSot.MotiveSotResponse GetMotiveSot(Entity.Transac.Service.Common.GetMotiveSot.MotiveSotRequest objMotiveSotRequest)
        {
            Entity.Transac.Service.Common.GetMotiveSot.MotiveSotResponse objMotiveSotResponse;

            try
            {
                objMotiveSotResponse = Claro.Web.Logging.ExecuteMethod<Entity.Transac.Service.Common.GetMotiveSot.MotiveSotResponse>(() => { return Business.Transac.Service.Common.getMotiveSot(objMotiveSotRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objMotiveSotRequest.Audit.Session, objMotiveSotRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objMotiveSotResponse;
        }
        public bool ValidateSchedule(Claro.SIACU.Entity.Transac.Service.Common.GetSchedule.ScheduleRequest objScheduleRequest)
        {

            try
            {
                return Claro.Web.Logging.ExecuteMethod(
                objScheduleRequest.Audit.Session,
                objScheduleRequest.Audit.Transaction,
                () =>
                {
                    return Business.Transac.Service.Common.ValidateSchedule(objScheduleRequest);
                }
               );
            }
            catch (Exception ex)
            {

                Claro.Web.Logging.Error(objScheduleRequest.Audit.Session, objScheduleRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }


        }


        

        
        public COMMON.GetGenerateConstancy.GenerateConstancyResponse GetGenerateContancyPDF(COMMON.GetGenerateConstancy.GenerateConstancyRequest request) {
            COMMON.GetGenerateConstancy.GenerateConstancyResponse objResponse = null;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<COMMON.GetGenerateConstancy.GenerateConstancyResponse>(() =>
                {
                    return Business.Transac.Service.Common.GetGenerateContancyPDF(request);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        public COMMON.GetGenerateConstancy.GenerateConstancyResponse GetConstancyPDFWithOnbase(COMMON.GetGenerateConstancy.GenerateConstancyRequest request)
        {
            COMMON.GetGenerateConstancy.GenerateConstancyResponse objResponse = null;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<COMMON.GetGenerateConstancy.GenerateConstancyResponse>(() =>
                {
                    return Business.Transac.Service.Common.GetConstancyPDFWithOnbase(request);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        public COMMON.GetMigratedElements.MigratedElementsResponse GetMigratedElements2(COMMON.GetMigratedElements.MigratedElementsRequest objRequest)
        {
            COMMON.GetMigratedElements.MigratedElementsResponse objResponse = null;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<COMMON.GetMigratedElements.MigratedElementsResponse>(() =>
                {
                    return Business.Transac.Service.Common.GetMigratedElements2(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        public COMMON.GetUpdateNotes.UpdateNotesResponse UpdateNotes(COMMON.GetUpdateNotes.UpdateNotesRequest request)
        {
            COMMON.GetUpdateNotes.UpdateNotesResponse objResponse = null;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<COMMON.GetUpdateNotes.UpdateNotesResponse>(() =>
                {
                    return Business.Transac.Service.Common.UpdateNotes(request);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }
        
        public Entity.Transac.Service.Common.GetBusinessRules.BusinessRulesResponse GetBusinessRules(Entity.Transac.Service.Common.GetBusinessRules.BusinessRulesRequest objBusinessRulesRequest)
        {
            Entity.Transac.Service.Common.GetBusinessRules.BusinessRulesResponse objBusinessRulesResponse = new Entity.Transac.Service.Common.GetBusinessRules.BusinessRulesResponse();

            try
            {
                objBusinessRulesResponse = Claro.Web.Logging.ExecuteMethod<Entity.Transac.Service.Common.GetBusinessRules.BusinessRulesResponse>(() => { return Business.Transac.Service.Common.GetBusinessRules(objBusinessRulesRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objBusinessRulesRequest.Audit.Session, objBusinessRulesRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objBusinessRulesResponse;
        }

        public Entity.Transac.Service.Common.GetRegion.RegionResponse GetRegions(Claro.SIACU.Entity.Transac.Service.Common.GetRegion.RegionRequest objRegionRequest)
        {
            Claro.SIACU.Entity.Transac.Service.Common.GetRegion.RegionResponse objRegionResponse = null;

            try
            {
                objRegionResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Transac.Service.Common.GetRegion.RegionResponse>(() => { return Business.Transac.Service.Common.GetRegions(objRegionRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRegionRequest.Audit.Session, objRegionRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objRegionResponse;
        }

        public Entity.Transac.Service.Common.GetCacDacType.CacDacTypeResponse GetCacDacType(Entity.Transac.Service.Common.GetCacDacType.CacDacTypeRequest objCacDacTypeRequest)
        {
            Entity.Transac.Service.Common.GetCacDacType.CacDacTypeResponse objCacDacTypeResponse;

            try
            {
                objCacDacTypeResponse = Claro.Web.Logging.ExecuteMethod<Entity.Transac.Service.Common.GetCacDacType.CacDacTypeResponse>(() => { return Business.Transac.Service.Common.GetCacDacType(objCacDacTypeRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objCacDacTypeRequest.Audit.Session, objCacDacTypeRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objCacDacTypeResponse;
        }

        public Entity.Transac.Service.Common.GetValueXml.ValueXmlResponse GetValueXml(Entity.Transac.Service.Common.GetValueXml.ValueXmlRequest objRequest)
        {
            Entity.Transac.Service.Common.GetValueXml.ValueXmlResponse objResponse;

            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<Entity.Transac.Service.Common.GetValueXml.ValueXmlResponse>(() => { return Business.Transac.Service.Common.GetValueXml(objRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objResponse;
        }

        public Entity.Transac.Service.Common.GetListItem.ListItemResponse GetListValueXML(Entity.Transac.Service.Common.GetListItem.ListItemRequest objListItemRequest)
        {
            Claro.SIACU.Entity.Transac.Service.Common.GetListItem.ListItemResponse objListItemResponse =
                new Claro.SIACU.Entity.Transac.Service.Common.GetListItem.ListItemResponse();

            try
            {
                objListItemResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Transac.Service.Common.GetListItem.ListItemResponse>(
                    () =>
                    {
                        return Business.Transac.Service.Common.GetListValueXML(objListItemRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objListItemRequest.Audit.Session, objListItemRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objListItemResponse;
        }

        public COMMON.GetParameterData.ParameterDataResponse GetParameterData(COMMON.GetParameterData.ParameterDataRequest request)
        {
            COMMON.GetParameterData.ParameterDataResponse objResponse;

            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<COMMON.GetParameterData.ParameterDataResponse>(() => { return Business.Transac.Service.Common.GetParameterData(request); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objResponse;
        }

        public Entity.Transac.Service.Common.GetTypification.TypificationResponse GetTypification(Claro.SIACU.Entity.Transac.Service.Common.GetTypification.TypificationRequest objTypificationRequest)
        {
            Claro.SIACU.Entity.Transac.Service.Common.GetTypification.TypificationResponse objTypificationResponse = null;

            try
            {
                objTypificationResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Transac.Service.Common.GetTypification.TypificationResponse>(() => { return Business.Transac.Service.Common.GetTypification(objTypificationRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objTypificationRequest.Audit.Session, objTypificationRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objTypificationResponse;
        }

        public Entity.Transac.Service.Common.GetReceipts.ReceiptsResponse GetReceipts(Entity.Transac.Service.Common.GetReceipts.ReceiptsRequest objRequest)
        {
            Entity.Transac.Service.Common.GetReceipts.ReceiptsResponse objResponse;

            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<Entity.Transac.Service.Common.GetReceipts.ReceiptsResponse>(
                    () =>
                    {
                        return Business.Transac.Service.Common.GetReceipts(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        public COMMON.GetListClientDocumentType.ListClientDocumentTypeResponse GetListClientDocumentType(COMMON.GetListClientDocumentType.ListClientDocumentTypeRequest request)
        {
            COMMON.GetListClientDocumentType.ListClientDocumentTypeResponse objResponse;

            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<COMMON.GetListClientDocumentType.ListClientDocumentTypeResponse>(() => { return Business.Transac.Service.Common.GetListClientDocumentType(request); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objResponse;
        }

        public COMMON.GetInsertLogTrx.InsertLogTrxResponse InsertLogTrx(COMMON.GetInsertLogTrx.InsertLogTrxRequest request)
        {
            COMMON.GetInsertLogTrx.InsertLogTrxResponse objResponse;

            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<COMMON.GetInsertLogTrx.InsertLogTrxResponse>(() => { return Business.Transac.Service.Common.InsertLogTrx(request); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objResponse;
        }

        #region interaaciones 



        //obtener interacciones de cliente

        public COMMON.GetInteractionClient.InteractionClientResponse GetInteractionClient(COMMON.GetInteractionClient.InteractionClientRequest objInteractionRequest)
        {
            COMMON.GetInteractionClient.InteractionClientResponse objInteractionResponse;
            try
            {
                objInteractionResponse = Claro.Web.Logging.ExecuteMethod<COMMON.GetInteractionClient.InteractionClientResponse>(() => { return Business.Transac.Service.Common.GetInteractionClient(objInteractionRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objInteractionRequest.Audit.Session, objInteractionRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objInteractionResponse;

        }

        //regitrar sub detalle de interacciones de cliente
        public COMMON.GetInteractionSubClasDetail.InteractionSubClasDetailResponse InsertRecordSubClaseDetail(COMMON.GetInteractionSubClasDetail.InteractionSubClasDetailRequest request)
        {
            COMMON.GetInteractionSubClasDetail.InteractionSubClasDetailResponse obj;
            try
            {
                obj = Claro.Web.Logging.ExecuteMethod<COMMON.GetInteractionSubClasDetail.InteractionSubClasDetailResponse>
                    (() => { return Business.Transac.Service.Common.GetInteractionSubClaseDetail(request); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));

            }
            return obj;
        }
        //registra plantilla interaccion
        public COMMON.GetInsertTemplateInteraction.InsertTemplateInteractionResponse GetInsertInteractionTemplate(COMMON.GetInsertTemplateInteraction.InsertTemplateInteractionRequest request)
        {
            COMMON.GetInsertTemplateInteraction.InsertTemplateInteractionResponse obj;
            try
            {
                obj = Claro.Web.Logging.ExecuteMethod<COMMON.GetInsertTemplateInteraction.InsertTemplateInteractionResponse>
                (() => { return Business.Transac.Service.Common.GetInsertInteractionTemplate(request); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }


            return obj;


        }

        //insertar plantila de interaccion
        //
        public COMMON.GetInsTemplateInteraction.InsTemplateInteractionResponse GetInsInteractionTemplate(COMMON.GetInsTemplateInteraction.InsTemplateInteractionRequest request)
        {
            COMMON.GetInsTemplateInteraction.InsTemplateInteractionResponse obj;
            try
            {
                obj = Claro.Web.Logging.ExecuteMethod<COMMON.GetInsTemplateInteraction.InsTemplateInteractionResponse>
                 (() => { return Business.Transac.Service.Common.GetInsInteractionTemplate(request); });
            }

            catch (Exception ex)
            {
                Claro.Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return obj;
        }

        //insertar interaccion
        //falta probar
        public COMMON.GetInsertInteract.InsertInteractResponse InsertInteract(COMMON.GetInsertInteract.InsertInteractRequest request)
        {
            COMMON.GetInsertInteract.InsertInteractResponse obj;
            try
            {
                obj = Claro.Web.Logging.ExecuteMethod<COMMON.GetInsertInteract.InsertInteractResponse>
                    (() => { return Business.Transac.Service.Common.GetInsertInteract(request); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return obj;

        }

        //insertar interaccion
        //registra interaccion
        //la funcion original se llama insertar

        public COMMON.GetInsertInt.InsertIntResponse InsertInt(COMMON.GetInsertInt.InsertIntRequest request)
        {
            COMMON.GetInsertInt.InsertIntResponse obj;
            try
            {
                obj = Claro.Web.Logging.ExecuteMethod<COMMON.GetInsertInt.InsertIntResponse>
                    (() => { return Business.Transac.Service.Common.GetInsertInt(request); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return obj;

        }

        //obtner cliente
        //nombre original  obtenerCliente
        //regresa la lista de clientes 
        public COMMON.GetClient.ClientResponse GetObClient(COMMON.GetClient.ClientRequest request)
        {
            COMMON.GetClient.ClientResponse obj;
            try
            {
                obj = Claro.Web.Logging.ExecuteMethod<COMMON.GetClient.ClientResponse>(() =>
                { return Business.Transac.Service.Common.GetObtClient(request); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return obj;
        }

        //insertar 
        //funcion original insertarinteraccionnegocios2

        public COMMON.GetBusinessInteraction2.BusinessInteraction2Response GetInsertBusinnesInteraction2(COMMON.GetBusinessInteraction2.BusinessInteraction2Request request)
        {
            COMMON.GetBusinessInteraction2.BusinessInteraction2Response obj;
            try
            {
                obj = Claro.Web.Logging.ExecuteMethod<COMMON.GetBusinessInteraction2.BusinessInteraction2Response>
                    (() => { return Business.Transac.Service.Common.GetBusinessInteraction2(request); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return obj;
        }


        //insertarinteraccionnegocios
        //funcion general insertar
        public COMMON.GetInsertGeneral.InsertGeneralResponse GetinsertInteractionGeneral(COMMON.GetInsertGeneral.InsertGeneralRequest objrequest)
        {
            COMMON.GetInsertGeneral.InsertGeneralResponse objresponse;

            try
            {
                objresponse = Claro.Web.Logging.ExecuteMethod<COMMON.GetInsertGeneral.InsertGeneralResponse>(() =>

                { return Business.Transac.Service.Common.GetIsertInteractionBusiness(objrequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objrequest.Audit.Session, objrequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objresponse;

        }

        //insertaplantilla interaccion general

        public COMMON.GetInsertTemplateGeneral.InsertTemplateGeneralResponse GetinsertInteractionTemplateGeneral(COMMON.GetInsertTemplateGeneral.InsertTemplateGeneralRequest objrequest)
        {
            COMMON.GetInsertTemplateGeneral.InsertTemplateGeneralResponse objresponse;
            try
            {
                objresponse = Claro.Web.Logging.ExecuteMethod<COMMON.GetInsertTemplateGeneral.InsertTemplateGeneralResponse>(() =>
                    {
                        return Business.Transac.Service.Common.GetInserInteractionTemplateresponse(objrequest);
                    });


            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objrequest.Audit.Session, objrequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objresponse;

        }

        //insertar detalle de interaccion
        //insertarDetalle

        public COMMON.GetInsertDetail.InsertDetailResponse GetInsertDetail(COMMON.GetInsertDetail.InsertDetailRequest objrequest)
        {
            COMMON.GetInsertDetail.InsertDetailResponse objresponse;
            try
            {
                objresponse = Claro.Web.Logging.ExecuteMethod<COMMON.GetInsertDetail.InsertDetailResponse>(() =>
                    {
                        return Business.Transac.Service.Common.GetInsertDeatil(objrequest);
                    });
            }
            catch (Exception ex)
            {

                Claro.Web.Logging.Error(objrequest.Audit.Session, objrequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objresponse;
        }
        #endregion

        public COMMON.GetUpdateXInter29.UpdateXInter29Response UpdateXInter29(COMMON.GetUpdateXInter29.UpdateXInter29Request objRequest)
        {
            COMMON.GetUpdateXInter29.UpdateXInter29Response objResponse;

            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<COMMON.GetUpdateXInter29.UpdateXInter29Response>(() => { return Business.Transac.Service.Common.UpdateXInter29(objRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objResponse;
        }



        public COMMON.GetCivilStatus.CivilStatusResponse GetCivilStatus(COMMON.GetCivilStatus.CivilStatusRequest objRequest)
        {
            COMMON.GetCivilStatus.CivilStatusResponse objResponse = null;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<COMMON.GetCivilStatus.CivilStatusResponse>(() =>
                {
                    return Business.Transac.Service.Common.GetCivilStatus(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        public COMMON.GetOccupationClient.OccupationClientResponse GetOccupationClient(COMMON.GetOccupationClient.OccupationClientRequest objRequest)
        {
            COMMON.GetOccupationClient.OccupationClientResponse objResponse = null;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<COMMON.GetOccupationClient.OccupationClientResponse>(() =>
                {
                    return Business.Transac.Service.Common.GetOccupationClient(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        public COMMON.GetReasonRegistry.ReasonRegistryResponse GetReasonRegistry(COMMON.GetReasonRegistry.ReasonRegistryRequest objRequest)
        {
            COMMON.GetReasonRegistry.ReasonRegistryResponse objResponse = null;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<COMMON.GetReasonRegistry.ReasonRegistryResponse>(() =>
                {
                    return Business.Transac.Service.Common.GetReasonRegistry(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }


        public COMMON.GetBrand.BrandResponse GetBrandList(COMMON.GetBrand.BrandRequest objRequest)
        {
            COMMON.GetBrand.BrandResponse objResponse = null;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<COMMON.GetBrand.BrandResponse>(() =>
                {
                    return Business.Transac.Service.Common.GetBrandList(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        public COMMON.GetConsultDepartment.ConsultDepartmentResponse GetConsultDepartment(COMMON.GetConsultDepartment.ConsultDepartmentRequest objRequest)
        {
            COMMON.GetConsultDepartment.ConsultDepartmentResponse objResponse = null;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<COMMON.GetConsultDepartment.ConsultDepartmentResponse>(() =>
                {
                    return Business.Transac.Service.Common.GetConsultDepartment(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        public COMMON.GetBrandModel.BrandModelResponse GetBrandModel(COMMON.GetBrandModel.BrandModelRequest objRequest)
        {
            COMMON.GetBrandModel.BrandModelResponse objResponse = null;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<COMMON.GetBrandModel.BrandModelResponse>(() =>
                {
                    return Business.Transac.Service.Common.GetBrandModel(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            //objResponse.ListBrandModel = objResponse.ListBrandModel.GetRange(0,3);
            return objResponse;
        }

        public COMMON.GetConsultProvince.ConsultProvinceResponse GetConsultProvince(COMMON.GetConsultProvince.ConsultProvinceRequest objRequest)
        {
            COMMON.GetConsultProvince.ConsultProvinceResponse objResponse = null;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<COMMON.GetConsultProvince.ConsultProvinceResponse>(() =>
                {
                    return Business.Transac.Service.Common.GetConsultProvince(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        public COMMON.GetConsultDistrict.ConsultDistrictResponse GetConsultDistrict(COMMON.GetConsultDistrict.ConsultDistrictRequest objRequest)
        {
            COMMON.GetConsultDistrict.ConsultDistrictResponse objResponse = null;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<COMMON.GetConsultDistrict.ConsultDistrictResponse>(() =>
                {
                    return Business.Transac.Service.Common.GetConsultDistrict(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            //objResponse.ListConsultDistrict = objResponse.ListConsultDistrict.GetRange(0, 5);
            return objResponse;
        }

        public COMMON.GetConsultNationality.ConsultNationalityResponse GetConsultNationality(COMMON.GetConsultNationality.ConsultNationalityRequest objRequest)
        {
            COMMON.GetConsultNationality.ConsultNationalityResponse objResponse = null;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<COMMON.GetConsultNationality.ConsultNationalityResponse>(() =>
                {
                    return Business.Transac.Service.Common.GetConsultNationality(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            //objResponse.ListBrandModel = objResponse.ListBrandModel.GetRange(0,3);
            return objResponse;
        }

        public COMMON.GetServicesVAS.ServicesVASResponse GetServicesVAS(COMMON.GetServicesVAS.ServicesVASRequest objRequest)
        {
            COMMON.GetServicesVAS.ServicesVASResponse objResponse = null;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<COMMON.GetServicesVAS.ServicesVASResponse>(() =>
                {
                    return Business.Transac.Service.Common.GetServicesVAS(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        public COMMON.GetMigratedElements.MigratedElementsResponse GetMigratedElements(COMMON.GetMigratedElements.MigratedElementsRequest objRequest)
        {
            COMMON.GetMigratedElements.MigratedElementsResponse objResponse = null;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<COMMON.GetMigratedElements.MigratedElementsResponse>(() =>
                {
                    return Business.Transac.Service.Common.GetMigratedElements(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }



        public Claro.SIACU.Entity.Transac.Service.Common.GetSaveAudit.SaveAuditResponse SaveAudit(Claro.SIACU.Entity.Transac.Service.Common.GetSaveAudit.SaveAuditRequest objGrabarAuditReq)
        {
            Claro.SIACU.Entity.Transac.Service.Common.GetSaveAudit.SaveAuditResponse objGrabarAuditResp = null;

            try
            {
                objGrabarAuditResp = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Transac.Service.Common.GetSaveAudit.SaveAuditResponse>(() => { return Business.Transac.Service.Common.SaveAudit(objGrabarAuditReq); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objGrabarAuditReq.Audit.Session, objGrabarAuditReq.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objGrabarAuditResp;
        }

        public Claro.SIACU.Entity.Transac.Service.Common.GetSaveAuditM.SaveAuditMResponse SaveAuditM(Claro.SIACU.Entity.Transac.Service.Common.GetSaveAuditM.SaveAuditMRequest objRegAuditReq)
        {
            Claro.SIACU.Entity.Transac.Service.Common.GetSaveAuditM.SaveAuditMResponse objRegAuditResp = null;

            try
            {
                objRegAuditResp = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Transac.Service.Common.GetSaveAuditM.SaveAuditMResponse>(() => { return Business.Transac.Service.Common.SaveAuditM(objRegAuditReq); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRegAuditReq.Audit.Session, objRegAuditReq.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objRegAuditResp;
        }

        public COMMON.GeneratePDF.GeneratePDFDataResponse GeneratePDF(COMMON.GeneratePDF.GeneratePDFDataRequest objGeneratePDFRequest)
        {
            COMMON.GeneratePDF.GeneratePDFDataResponse objGeneratePDFResponse = null;
            try
            {
                objGeneratePDFResponse = Claro.Web.Logging.ExecuteMethod<COMMON.GeneratePDF.GeneratePDFDataResponse>(objGeneratePDFRequest.Audit.Session, objGeneratePDFRequest.Audit.Transaction, () =>
                {
                    return Business.Transac.Service.Common.GeneratePDF(objGeneratePDFRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objGeneratePDFRequest.Audit.Session, objGeneratePDFRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));

            }
            return objGeneratePDFResponse;
        }

        public InsertInteractHFCResponse GetInsertInteractHFC(InsertInteractHFCRequest objRequest)
        {
            COMMON.GetInsertInteractHFC.InsertInteractHFCResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Transac.Service.Common.GetInsertInteractHFC.InsertInteractHFCResponse>(
                    () =>
                    {
                        return Business.Transac.Service.Common.GetInsertInteractHFC(objRequest);
                    });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        public COMMON.GetInsertInteract.InsertInteractResponse GetInsertInteract(COMMON.GetInsertInteract.InsertInteractRequest objRequest)
        {
            COMMON.GetInsertInteract.InsertInteractResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Transac.Service.Common.GetInsertInteract.InsertInteractResponse>(
                    () =>
                    {
                        return Business.Transac.Service.Common.GetInsertInteract(objRequest);
                    });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }


        #region funciones para eviar email
        //enviaremail
        public COMMON.GetSendEmail.SendEmailResponse GetSendEmail(COMMON.GetSendEmail.SendEmailRequest objrequest)
        {
            COMMON.GetSendEmail.SendEmailResponse objresponse = new COMMON.GetSendEmail.SendEmailResponse();
            try
            {
                objresponse = Claro.Web.Logging.ExecuteMethod<COMMON.GetSendEmail.SendEmailResponse>(objrequest.Audit.Session, objrequest.Audit.Transaction, () =>
                  {
                      return Business.Transac.Service.Common.GetSendEmail(objrequest);
                  });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objrequest.Audit.Session, objrequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));

                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objresponse;
        }

        //enviaremailatacht
        public COMMON.GetSendEmail.SendEmailResponse GetSendEmailAttach(COMMON.GetSendEmail.SendEmailRequest objrequest)
        {
            //List<string> list = new List<string>() { "d:/archivo1.txt", "d:/archivo2.txt" };
            //objrequest.lsAttached = list;
            COMMON.GetSendEmail.SendEmailResponse objresponse = null;
            try
            {
                objresponse = Claro.Web.Logging.ExecuteMethod<COMMON.GetSendEmail.SendEmailResponse>(objrequest.Audit.Session, objrequest.Audit.Transaction, () =>
                    {

                        return Business.Transac.Service.Common.GetSendEmailAttach(objrequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objrequest.Audit.Session, objrequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objresponse;

        }

        //EnviarEmailAlt
        public COMMON.GetSendEmail.SendEmailResponse GetSendEmailAlt(COMMON.GetSendEmail.SendEmailRequest objrequest)
        {
            COMMON.GetSendEmail.SendEmailResponse objresponse = null;
            try
            {
                objresponse = Claro.Web.Logging.ExecuteMethod<COMMON.GetSendEmail.SendEmailResponse>(objrequest.Audit.Session, objrequest.Audit.Transaction, () =>
                    {
                        return Business.Transac.Service.Common.GetSendEmailAlt(objrequest);

                    });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objrequest.Audit.Session, objrequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));

                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));

            }
            return objresponse;
        }

        //getfiledefaultimpersonation
        public COMMON.GetFileDefaultImpersonation.GetFileDefaultImpersonationResponse GetfileDefaultImpersonation(COMMON.GetFileDefaultImpersonation.GetFileDefaultImpersonationRequest objrequest)
        {
            COMMON.GetFileDefaultImpersonation.GetFileDefaultImpersonationResponse objreponse = null;
            try
            {
                objreponse = Claro.Web.Logging.ExecuteMethod<COMMON.GetFileDefaultImpersonation.GetFileDefaultImpersonationResponse>(() =>
                {
                    return Business.Transac.Service.Common.GetfileDefaultImpersonation(objrequest);
                });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objrequest.Audit.Session, objrequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw;
            }
            return objreponse;
        }


        #endregion

        public COMMON.GetDatTempInteraction.DatTempInteractionResponse GetInfoInteractionTemplate(COMMON.GetDatTempInteraction.DatTempInteractionRequest objRequest)
        {
            var objResponse = new COMMON.GetDatTempInteraction.DatTempInteractionResponse();
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Transac.Service.Common.GetDatTempInteraction.DatTempInteractionResponse>(
                    () =>
                    {
                        return Business.Transac.Service.Common.GetInfoInteractionTemplate(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        public COMMON.GetNumberGWP.NumberGWPResponse GetNumberGWP(COMMON.GetNumberGWP.NumberGWPRequest objRequest)
        {
            COMMON.GetNumberGWP.NumberGWPResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<COMMON.GetNumberGWP.NumberGWPResponse>(() =>
                {
                    return Business.Transac.Service.Common.GetNumberGWP(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        public COMMON.GetNumberEAI.NumberEAIResponse GetNumberEAI(COMMON.GetNumberEAI.NumberEAIRequest objRequest)
        {
            COMMON.GetNumberEAI.NumberEAIResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<COMMON.GetNumberEAI.NumberEAIResponse>(() =>
                {
                    return Business.Transac.Service.Common.GetNumberEAI(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        public COMMON.GetVerifyUser.VerifyUserResponse GetVerifyUser(COMMON.GetVerifyUser.VerifyUserRequest objRequest)
        {
            var objResponse = new COMMON.GetVerifyUser.VerifyUserResponse();
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(
                    () => Business.Transac.Service.Common.GetVerifyUser(objRequest)
                );
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        public COMMON.GetEvaluateAmount.EvaluateAmountResponse GetEvaluateAmount(COMMON.GetEvaluateAmount.EvaluateAmountRequest objRequest)
        {
            var objResponse = new COMMON.GetEvaluateAmount.EvaluateAmountResponse();
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(
                    () => Business.Transac.Service.Common.GetEvaluateAmount(objRequest)
                );
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        public COMMON.GetEvaluateAmount.EvaluateAmountResponse GetEvaluateAmount_DCM(COMMON.GetEvaluateAmount.EvaluateAmountRequest objRequest)
        {
            var objResponse = new COMMON.GetEvaluateAmount.EvaluateAmountResponse();
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(
                    () => Business.Transac.Service.Common.GetEvaluateAmount_DCM(objRequest)
                );
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objResponse;
        }

        #region Redirect
        public Entity.Transac.Service.Common.GetRedirectSession.RedirectSessionResponse GetRedirectSession(Entity.Transac.Service.Common.GetRedirectSession.RedirectSessionRequest objRedirectSessionRequest)
        {
            Entity.Transac.Service.Common.GetRedirectSession.RedirectSessionResponse objRedirectSessionResponse = null;

            try
            {
                objRedirectSessionResponse = Business.Transac.Service.Common.GetRedirectSession(objRedirectSessionRequest);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRedirectSessionRequest.Audit.Session, objRedirectSessionRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objRedirectSessionResponse;
        }
        public Entity.Transac.Service.Common.InsertRedirectCommunication.InsertRedirectComResponse InsertRedirectCommunication(Entity.Transac.Service.Common.InsertRedirectCommunication.InsertRedirectComRequest objInsertRedirectComRequest)
        {
            Entity.Transac.Service.Common.InsertRedirectCommunication.InsertRedirectComResponse onjInsertRedirectComResponse = null;
            try
            {
                onjInsertRedirectComResponse = Business.Transac.Service.Common.InsertRedirectCommunication(objInsertRedirectComRequest);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objInsertRedirectComRequest.Audit.Session, objInsertRedirectComRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }
            return onjInsertRedirectComResponse;
        }

        public Entity.Transac.Service.Common.GetValidateCommunication.ValidateCommunicationResponse ValidateRedirectCommunication(Entity.Transac.Service.Common.GetValidateCommunication.ValidateCommunicationRequest objValidateCommunicationRequest)
        {
            Entity.Transac.Service.Common.GetValidateCommunication.ValidateCommunicationResponse onjValidateCommunicationResponse = null;
            try
            {
                onjValidateCommunicationResponse = Business.Transac.Service.Common.ValidateRedirectCommunication(objValidateCommunicationRequest);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objValidateCommunicationRequest.Audit.Session, objValidateCommunicationRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return onjValidateCommunicationResponse;
        }
        #endregion


        #region External/ Internal Transfer

        public COMMON.GetMzBloEdiType.MzBloEdiTypeResponse GetMzBloEdiTypePVU(EntitiesCommon.GetMzBloEdiType.MzBloEdiTypeRequest objMzBloEdiTypeRequest)
        {
            EntitiesCommon.GetMzBloEdiType.MzBloEdiTypeResponse objMzBloEdiTypeResponse;

            try
            {
                objMzBloEdiTypeResponse = Claro.Web.Logging.ExecuteMethod<EntitiesCommon.GetMzBloEdiType.MzBloEdiTypeResponse>(
                    () => { return Business.Transac.Service.Common.GetMzBloEdiTypePVU(objMzBloEdiTypeRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objMzBloEdiTypeRequest.Audit.Session, objMzBloEdiTypeRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objMzBloEdiTypeResponse;
        }

        public COMMON.GetTipDptInt.TipDptIntResponse GetTipDptInt(EntitiesCommon.GetTipDptInt.TipDptIntRequest objTipDptIntRequest)
        {
            EntitiesCommon.GetTipDptInt.TipDptIntResponse objTipDptIntResponse;

            try
            {
                objTipDptIntResponse = Claro.Web.Logging.ExecuteMethod<EntitiesCommon.GetTipDptInt.TipDptIntResponse>(() => { return Business.Transac.Service.Common.GetTipDptInt(objTipDptIntRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objTipDptIntRequest.Audit.Session, objTipDptIntRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objTipDptIntResponse;

        }

        public COMMON.GetDepartmentsPVU.DepartmentsPvuResponse GetDepartmentsPVU(EntitiesCommon.GetDepartmentsPVU.DepartmentsPvuRequest objDepartmentsRequest)
        {
            EntitiesCommon.GetDepartmentsPVU.DepartmentsPvuResponse objDepartmentsResponse;

            try
            {
                objDepartmentsResponse = Claro.Web.Logging.ExecuteMethod<EntitiesCommon.GetDepartmentsPVU.DepartmentsPvuResponse>(() => { return Business.Transac.Service.Common.GetDepartmentsPVU(objDepartmentsRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objDepartmentsRequest.Audit.Session, objDepartmentsRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objDepartmentsResponse;
        }

        public COMMON.GetProvincesPVU.ProvincesPvuResponse GetProvincesPVU(EntitiesCommon.GetProvincesPVU.ProvincesPvuRequest objProvincesRequest)
        {
            EntitiesCommon.GetProvincesPVU.ProvincesPvuResponse objProvincesResponse;

            try
            {
                objProvincesResponse = Claro.Web.Logging.ExecuteMethod<EntitiesCommon.GetProvincesPVU.ProvincesPvuResponse>(
                    () => { return Business.Transac.Service.Common.GetProvincesPVU(objProvincesRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objProvincesRequest.Audit.Session, objProvincesRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objProvincesResponse;
        }

        public COMMON.GetDistrictsPVU.DistrictsPvuResponse GetDistrictsPVU(EntitiesCommon.GetDistrictsPVU.DistrictsPvuRequest objDistrictsRequest)
        {
            EntitiesCommon.GetDistrictsPVU.DistrictsPvuResponse objDistrictsResponse;

            try
            {
                objDistrictsResponse = Claro.Web.Logging.ExecuteMethod<EntitiesCommon.GetDistrictsPVU.DistrictsPvuResponse>(
                    () => { return Business.Transac.Service.Common.GetDistrictsPVU(objDistrictsRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objDistrictsRequest.Audit.Session, objDistrictsRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objDistrictsResponse;
        }

        public COMMON.GetCenterPopulatPVU.CenterPopulatPvuRespose GetCenterPopulatPVU(EntitiesCommon.GetCenterPopulatPVU.CenterPopulatPvuRequest objCenterPopulatRequest)
        {
            EntitiesCommon.GetCenterPopulatPVU.CenterPopulatPvuRespose objCenterPopulatResponse;

            try
            {
                objCenterPopulatResponse = Claro.Web.Logging.ExecuteMethod<EntitiesCommon.GetCenterPopulatPVU.CenterPopulatPvuRespose>(() => { return Business.Transac.Service.Common.GetCenterPopulatPVU(objCenterPopulatRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objCenterPopulatRequest.Audit.Session, objCenterPopulatRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objCenterPopulatResponse;
        }

        public COMMON.GetZoneTypeCOBS.ZoneTypeCobsResponse GetZoneTypeCOBS(EntitiesCommon.GetZoneTypeCOBS.ZoneTypeCobsRequest objZoneTypeRequest)
        {

            EntitiesCommon.GetZoneTypeCOBS.ZoneTypeCobsResponse objZoneTypeResponse;

            try
            {
                objZoneTypeResponse = Claro.Web.Logging.ExecuteMethod<EntitiesCommon.GetZoneTypeCOBS.ZoneTypeCobsResponse>(() => { return Business.Transac.Service.Common.GetZoneTypeCOBS(objZoneTypeRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objZoneTypeRequest.Audit.Session, objZoneTypeRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objZoneTypeResponse;
        }

        public COMMON.GetBuildingsPVU.BuildingsPvuResponse GetBuildingsPVU(EntitiesCommon.GetBuildingsPVU.BuildingsPvuRequest objBuildingsRequest)
        {
            EntitiesCommon.GetBuildingsPVU.BuildingsPvuResponse objBuildingsResponse;

            try
            {
                objBuildingsResponse = Claro.Web.Logging.ExecuteMethod<EntitiesCommon.GetBuildingsPVU.BuildingsPvuResponse>(() => { return Business.Transac.Service.Common.GetBuildingsPVU(objBuildingsRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objBuildingsRequest.Audit.Session, objBuildingsRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objBuildingsResponse;
        }

        public COMMON.GetWorkType.WorkTypeResponse GetWorkType(EntitiesCommon.GetWorkType.WorkTypeRequest objWorkTypeRequest)
        {
            EntitiesCommon.GetWorkType.WorkTypeResponse objWorkTypeResponse;

            try
            {
                objWorkTypeResponse = Claro.Web.Logging.ExecuteMethod<EntitiesCommon.GetWorkType.WorkTypeResponse>(() => { return Business.Transac.Service.Common.GetWorkType(objWorkTypeRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objWorkTypeRequest.Audit.Session, objWorkTypeRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objWorkTypeResponse;
        }

        public COMMON.GetWorkSubType.WorkSubTypeResponse GetWorkSubType(EntitiesCommon.GetWorkSubType.WorkSubTypeRequest objWorkSubTypeRequest)
        {
            EntitiesCommon.GetWorkSubType.WorkSubTypeResponse objWorkSubTypeResponse;
            try
            {
                objWorkSubTypeResponse = Claro.Web.Logging.ExecuteMethod<EntitiesCommon.GetWorkSubType.WorkSubTypeResponse>(() => { return Business.Transac.Service.Common.GetWorkSubType(objWorkSubTypeRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objWorkSubTypeRequest.Audit.Session, objWorkSubTypeRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objWorkSubTypeResponse;
        }


        public List<ListItem> GetDocumentTypeCOBS(string strIdSession, string strTransaction, string strCodCargaDdl)
        {
            List<ListItem> listItem = null;

            try
            {
                listItem = Claro.Web.Logging.ExecuteMethod<List<ListItem>>(() =>
                {
                    return Business.Transac.Service.Common.GetDocumentTypeCOBS(strIdSession, strTransaction, strCodCargaDdl);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return listItem;

        }

        #endregion



        public COMMON.GetPagOptionXuserNV.PagOptionXuserNVResponse GetPagOptionXuserVn(COMMON.GetPagOptionXuserNV.PagOptionXuserNVRequest objRequest)
        {
            var objResponse = new COMMON.GetPagOptionXuserNV.PagOptionXuserNVResponse();
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() => Business.Transac.Service.Common.GetPagOptionXuserVN(objRequest)
                );
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objResponse;
        }

        public COMMON.GetPagOptionXuser.PagOptionXuserResponse GetPagOptionXuser(COMMON.GetPagOptionXuser.PagOptionXuserRequest objRequest)
        {
            var objResponse = new COMMON.GetPagOptionXuser.PagOptionXuserResponse();
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() => Business.Transac.Service.Common.GetPagOptionXuser(objRequest)
                );
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objResponse;
        }

        #region getfileinvoice
        //public COMMON.GetFileInvoice.FileInvoiceResponse GetFileInvoice(COMMON.GetFileInvoice.FileInvoiceRequest objrequest)
        //{
        //    COMMON.GetFileInvoice.FileInvoiceResponse objresponse = null;
        //    try
        //    {
        //        objresponse = Claro.Web.Logging.ExecuteMethod<COMMON.GetFileInvoice.FileInvoiceResponse>(() =>
        //            {
        //                return Business.Transac.Service.Common.GetFileInvoice(objrequest);

        //            });
        //    }
        //    catch (Exception ex)
        //    {
        //        Claro.Web.Logging.Error(objrequest.Audit.Session, objrequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
        //        throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
        //    }
        //    return objresponse;

        //}

        //public COMMON.GetTypeMIME.TypeMIMEResponse GetTypeMIME(COMMON.GetTypeMIME.TypeMIMERequest objrequest)
        //{
        //    COMMON.GetTypeMIME.TypeMIMEResponse objresponse= null;
        //    try
        //    {
        //        objresponse = Claro.Web.Logging.ExecuteMethod<COMMON.GetTypeMIME.TypeMIMEResponse>(() =>
        //        {
        //            return Business.Transac.Service.Common.GetTypeMIME(objrequest);
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        Claro.Web.Logging.Error(objrequest.Audit.Session, objrequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
        //        throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
        //    }
        //    return objresponse;
        //}


        #endregion
        public COMMON.GetUser.UserResponse GetUser(COMMON.GetUser.UserRequest objRequest)
        {
            var objResponse = new COMMON.GetUser.UserResponse();
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() => Business.Transac.Service.Common.GetUser(objRequest)
                );
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objResponse;
        }

        public COMMON.GetPenaltyChangePlans.PenaltyChangePlanResponse GetPenaltyChangePlan(COMMON.GetPenaltyChangePlans.PenaltyChangePlanRequest objRequest)
        {
            COMMON.GetPenaltyChangePlans.PenaltyChangePlanResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() => Business.Transac.Service.Common.GetPenaltyChangePlan(objRequest));
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objResponse;
        }

        public COMMON.GetEmployerDate.GetEmployerDateResponse GetEmployerDate(COMMON.GetEmployerDate.GetEmployerDateRequest objDatosEmpleadoRequest)
        {

            COMMON.GetEmployerDate.GetEmployerDateResponse objDatosEmpleadoResponse = new COMMON.GetEmployerDate.GetEmployerDateResponse();
            try
            {
                objDatosEmpleadoResponse = Claro.Web.Logging.ExecuteMethod(() => Business.Transac.Service.Common.GetEmployerDate(objDatosEmpleadoRequest));
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objDatosEmpleadoRequest.Audit.Session, objDatosEmpleadoRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }


            return objDatosEmpleadoResponse;
        }

        public COMMON.GetConsultIGV.ConsultIGVResponse GetConsultIGV(COMMON.GetConsultIGV.ConsultIGVRequest objRequest)
        {
            var objResponse = new COMMON.GetConsultIGV.ConsultIGVResponse();
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(
                    () => Business.Transac.Service.Common.GetConsultIGV(objRequest)
                );
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        public ContractByPhoneNumberResponse GetContractByPhoneNumber(ContractByPhoneNumberRequest objRequest)
        {
            ContractByPhoneNumberResponse objResponse = new ContractByPhoneNumberResponse();
            try
            {
                objResponse =
                    Claro.Web.Logging.ExecuteMethod(
                        () => Business.Transac.Service.Common.GetContractByPhoneNumber(objRequest));
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }
        public COMMON.GetSendEmailSB.SendEmailSBResponse GetSendEmailSB(COMMON.GetSendEmailSB.SendEmailSBRequest objRequest)
        {
            var objResponse = new COMMON.GetSendEmailSB.SendEmailSBResponse();
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(
                    () => Business.Transac.Service.Common.GetSendEmailSB(objRequest)
                );
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        public COMMON.GetNoteInteraction.NoteInteractionResponse GetNoteInteraction(COMMON.GetNoteInteraction.NoteInteractionRequest objRequest)
        {
            COMMON.GetNoteInteraction.NoteInteractionResponse objResponse = new COMMON.GetNoteInteraction.NoteInteractionResponse();

            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<COMMON.GetNoteInteraction.NoteInteractionResponse>(() =>
                {
                    return Business.Transac.Service.Common.GetNoteInteraction(objRequest);
                });
            }
            catch (Exception ex)
            {

                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objResponse;
        }

        public COMMON.GetDynamicCaseTemplateData.DynamicCaseTemplateDataResponse GetDynamicCaseTemplateData(COMMON.GetDynamicCaseTemplateData.DynamicCaseTemplateDataRequest objRequest)
        {
            COMMON.GetDynamicCaseTemplateData.DynamicCaseTemplateDataResponse objResponse = new COMMON.GetDynamicCaseTemplateData.DynamicCaseTemplateDataResponse();

            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<COMMON.GetDynamicCaseTemplateData.DynamicCaseTemplateDataResponse>(() =>
                {
                    return Business.Transac.Service.Common.GetDynamicCaseTemplateData(objRequest);
                });
            }
            catch (Exception ex)
            {

                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objResponse;
        }

        public RegisterServiceCommercialResponse GetRegisterServiceCommercial(RegisterServiceCommercialRequest objRequest)
        {
            RegisterServiceCommercialResponse objResponse = new RegisterServiceCommercialResponse();
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return Business.Transac.Service.Common.GetRegisterServiceCommercial(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objResponse;
        }

        public UpdatexInter30Response GetUpdatexInter30(UpdatexInter30Request objRequest)
        {
            UpdatexInter30Response objResponse;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<UpdatexInter30Response>(() => { return Business.Transac.Service.Common.GetUpdatexInter30(objRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        public COMMON.GetProgramTask.ProgramTaskResponse GetProgramTask(COMMON.GetProgramTask.ProgramTaskRequest objRequest)
        {
            var objResponse = new COMMON.GetProgramTask.ProgramTaskResponse();

            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<COMMON.GetProgramTask.ProgramTaskResponse>(() =>
                {
                    return Business.Transac.Service.Common.GetProgramTask(objRequest);
                });
            }
            catch (Exception ex)
            {

                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objResponse;
        }

        public COMMON.GetTypeTransaction.TypeTransactionResponse GetTypeTransaction(COMMON.GetTypeTransaction.TypeTransactionRequest objRequest)
        {
            var objResponse = new COMMON.GetTypeTransaction.TypeTransactionResponse();

            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<COMMON.GetTypeTransaction.TypeTransactionResponse>(() =>
                {
                    return Business.Transac.Service.Common.GetTypeTransaction(objRequest);
                });
            }
            catch (Exception ex)
            {

                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objResponse;
        }

        public COMMON.GetParameterTerminalTPI.ParameterTerminalTPIResponse GetParameterTerminalTPI(COMMON.GetParameterTerminalTPI.ParameterTerminalTPIRequest objRequest)
        {
            COMMON.GetParameterTerminalTPI.ParameterTerminalTPIResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<COMMON.GetParameterTerminalTPI.ParameterTerminalTPIResponse>
                    (() => { return Business.Transac.Service.Common.GetParameterTerminalTPI(objRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }
        public COMMON.GetInsertEvidence.InsertEvidenceResponse GetInsertEvidence(COMMON.GetInsertEvidence.InsertEvidenceRequest objRequest)
        {
            COMMON.GetInsertEvidence.InsertEvidenceResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<COMMON.GetInsertEvidence.InsertEvidenceResponse>
                    (() => { return Business.Transac.Service.Common.GetInsertEvidence(objRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }


        public EntitiesCommon.GetSendEmail.SendEmailResponse GetSendEmailFixed(EntitiesCommon.GetSendEmail.SendEmailRequest objrequest)
        {
            COMMON.GetSendEmail.SendEmailResponse objresponse = new COMMON.GetSendEmail.SendEmailResponse();
            try
            {
                objresponse = Claro.Web.Logging.ExecuteMethod<COMMON.GetSendEmail.SendEmailResponse>(objrequest.Audit.Session, objrequest.Audit.Transaction, () =>
                {
                    return Business.Transac.Service.Common.GetSendEmailFixed(objrequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objrequest.Audit.Session, objrequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));

                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objresponse;
        }
        public COMMON.GetConsultImei.ConsultImeiResponse GetConsultImei(COMMON.GetConsultImei.ConsultImeiRequest objRequest)
        {
            var objResponse = new COMMON.GetConsultImei.ConsultImeiResponse();
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(
                    () => Business.Transac.Service.Common.GetConsultImei(objRequest)
                );
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        public Entity.Transac.Service.Common.GetSot.GetSotResponse GetSot(Entity.Transac.Service.Common.GetSot.GetSotRequest objSotRequest)
        {
            Entity.Transac.Service.Common.GetSot.GetSotResponse objSotResponse;

            try
            {
                objSotResponse = Claro.Web.Logging.ExecuteMethod<Entity.Transac.Service.Common.GetSot.GetSotResponse>(() => { return Business.Transac.Service.Common.GetSotMtto(objSotRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objSotRequest.Audit.Session, objSotRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objSotResponse;
        }
        public COMMON.GetConsultMarkModel.ConsultMarkModelResponse GetConsultMarkModel(COMMON.GetConsultMarkModel.ConsultMarkModelRequest objRequest)
        {
            var objResponse = new COMMON.GetConsultMarkModel.ConsultMarkModelResponse();
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(
                    () => Business.Transac.Service.Common.GetConsultMarkModel(objRequest)
                );
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }
        public COMMON.GetQuestionsAnswerSecurity.QuestionsAnswerSecurityResponse GetQuestionsAnswerSecurity(COMMON.GetQuestionsAnswerSecurity.QuestionsAnswerSecurityRequest objRequest)
        {
            QuestionsAnswerSecurityResponse oQuestionsAnswerSecurity = null;
            oQuestionsAnswerSecurity = new COMMON.GetQuestionsAnswerSecurity.QuestionsAnswerSecurityResponse();
            try
            {
                oQuestionsAnswerSecurity = Claro.Web.Logging.ExecuteMethod<QuestionsAnswerSecurityResponse>(() =>
                {
                    return Business.Transac.Service.Common.GetQuestionsAnswerSecurity(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return oQuestionsAnswerSecurity;

        }

        public COMMON.GetEquipmentForeign.EquipmentForeignResponse GetEquipmentForeign(COMMON.GetEquipmentForeign.EquipmentForeignRequest objRequest)
        {
            var objResponse = new COMMON.GetEquipmentForeign.EquipmentForeignResponse();
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(
                    () => Business.Transac.Service.Common.GetEquipmentForeign(objRequest)
                );
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        public COMMON.GetInsertEquipmentForeign.InsertEquipmentForeignResponse GetInsertEquipmentForeign(COMMON.GetInsertEquipmentForeign.InsertEquipmentForeignRequest request)
        {
            COMMON.GetInsertEquipmentForeign.InsertEquipmentForeignResponse obj;
            try
            {
                obj = Claro.Web.Logging.ExecuteMethod<COMMON.GetInsertEquipmentForeign.InsertEquipmentForeignResponse>
                    (() => { return Business.Transac.Service.Common.GetInsertEquipmentForeign(request); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));

            }
            return obj;
        }

        public COMMON.GetListEquipmentRegistered.ListEquipmentRegisteredResponse GetListEquipmentRegistered(COMMON.GetListEquipmentRegistered.ListEquipmentRegisteredRequest objRequest)
        {
            Claro.Web.Logging.Info(objRequest.SessionId, objRequest.TransactionId, "entro services GetListEquipmentRegistered ");
            var objResponse = new COMMON.GetListEquipmentRegistered.ListEquipmentRegisteredResponse();
            try
            {
                Claro.Web.Logging.Info(objRequest.SessionId, objRequest.TransactionId, "entro services GetListEquipmentRegistered entrando conexion con capa business");
                objResponse = Claro.Web.Logging.ExecuteMethod(
                    () => Business.Transac.Service.Common.GetListEquipmentRegistered(objRequest)
                );
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            Claro.Web.Logging.Info(objRequest.SessionId, objRequest.TransactionId, "entro services GetListEquipmentRegistered retorna response ");
            return objResponse;
        }

        public COMMON.GetConsultByGroupParameter.ConsultByGroupParameterResponse GetConsultByGroupParameter(COMMON.GetConsultByGroupParameter.ConsultByGroupParameterRequest objRequest)
        {

            var objResponse = new COMMON.GetConsultByGroupParameter.ConsultByGroupParameterResponse();
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(
                    () => Business.Transac.Service.Common.GetConsultByGroupParameter(objRequest)
                );
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;

        }
        public COMMON.GetInteraction.InteractionResponse GetInteraction(COMMON.GetInteraction.InteractionRequest objRequest)
        {
            var objResponse = new COMMON.GetInteraction.InteractionResponse();
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(
                     () => Business.Transac.Service.Common.GetInteraction(objRequest)
                );
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objResponse;
        }

        public string GetIdTrazabilidad(string strIdSession, string strTransaction, Int32 intCodGrupo)
        {
            string objResponse = string.Empty;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(
                     () => Business.Transac.Service.Common.GetIdTrazabilidad(strIdSession, strTransaction, intCodGrupo)
                );
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objResponse;
        }

        public COMMON.GetBlackList.BlackListResponse GetBlackListOsiptel(Entity.Transac.Service.Common.GetBlackList.BlackListRequest objBlackListRequest)
        {
            var objResponse = new COMMON.GetBlackList.BlackListResponse();
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(
                     () => Business.Transac.Service.Common.GetBlackListOsiptel(objBlackListRequest)
                );
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objBlackListRequest.Audit.Session, objBlackListRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;       
        }


        #region TOA

        public Entity.Transac.Service.Common.GetTimeZones.TimeZonesResponse GetTimeZones(Entity.Transac.Service.Common.GetTimeZones.TimeZonesRequest objTimeZonesRequest)
        {
            EntitiesCommon.GetTimeZones.TimeZonesResponse objTimeZonesResponse = null;
            try
            {
                objTimeZonesResponse = Claro.Web.Logging.ExecuteMethod(objTimeZonesRequest.Audit.Session, objTimeZonesRequest.Audit.Transaction, () => { return Business.Transac.Service.Common.GetTimeZones(objTimeZonesRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objTimeZonesRequest.Audit.Session, objTimeZonesRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objTimeZonesResponse;
        }

        public EntitiesCommon.GetETAAuditRequestCapacity.ETAAuditResponseCapacity GetETAAuditRequestCapacity(EntitiesCommon.GetETAAuditRequestCapacity.ETAAuditRequestCapacity objETAAuditRequestCapacity)
        {
            EntitiesCommon.GetETAAuditRequestCapacity.ETAAuditResponseCapacity objResponse;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() => { return Business.Transac.Service.Common.GetETAAuditRequestCapacity(objETAAuditRequestCapacity); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objETAAuditRequestCapacity.Audit.Session, objETAAuditRequestCapacity.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objResponse;
        }

        public int RegisterEtaRequest(EntitiesCommon.GetRegisterEta.RegisterEtaRequest objRegisterEtaRequest)
        {
            var objResponse = 0;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(objRegisterEtaRequest.Audit.Session, objRegisterEtaRequest.Audit.Transaction, () => { return Business.Transac.Service.Common.RegisterEtaRequest(objRegisterEtaRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRegisterEtaRequest.Audit.Session, objRegisterEtaRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objResponse;
        }

        public string RegisterEtaResponse(EntitiesCommon.GetRegisterEta.RegisterEtaResponse objRegisterEtaResponse)
        {
            string vidreturn = string.Empty;
            try
            {
                vidreturn = Claro.Web.Logging.ExecuteMethod(objRegisterEtaResponse.Audit.Session, objRegisterEtaResponse.Audit.Transaction, () => { return Business.Transac.Service.Common.RegisterEtaResponse(objRegisterEtaResponse); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRegisterEtaResponse.Audit.Session, objRegisterEtaResponse.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return vidreturn;
        }

        public EntitiesCommon.GetTransactionScheduled.TransactionScheduledResponse GetSchedulingRule(EntitiesCommon.GetTransactionScheduled.TransactionScheduledRequest objRequest)
        {
            EntitiesCommon.GetTransactionScheduled.TransactionScheduledResponse oResponse = new EntitiesCommon.GetTransactionScheduled.TransactionScheduledResponse();
            try
            {
                oResponse = Claro.Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () => { return Business.Transac.Service.Common.GetSchedulingRule(objRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return oResponse;
        }

        #endregion

        public Entity.Transac.Service.Common.GetPuntosAtencion.responseDataObtenerTabPuntosAtencionPost GetPuntosAtencion(COMMON.GetPuntosAtencion.PuntosAtencionRequest objRequest)
        {
            Entity.Transac.Service.Common.GetPuntosAtencion.responseDataObtenerTabPuntosAtencionPost objResponse;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<Entity.Transac.Service.Common.GetPuntosAtencion.responseDataObtenerTabPuntosAtencionPost>
                    (() => { return Business.Transac.Service.Common.GetPuntosAtencion(objRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;

        }

        public COMMON.GetOffice.OfficeResponse GetOffice(COMMON.GetOffice.OfficeRequest objOfficeRequest)
        {
            var objOfficeResponse = new COMMON.GetOffice.OfficeResponse();

            try
            {
                objOfficeResponse = Claro.Web.Logging.ExecuteMethod(() => Business.Transac.Service.Common.GetOffice(objOfficeRequest)
                );
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objOfficeRequest.Audit.Session, objOfficeRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objOfficeResponse;
        }

        public COMMON.GetConsultNationality.ConsultNationalityResponse GetNationalityList(COMMON.GetConsultNationality.ConsultNationalityRequest objRequest)
        {
            COMMON.GetConsultNationality.ConsultNationalityResponse objResponse = null;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<COMMON.GetConsultNationality.ConsultNationalityResponse>(() =>
                {
                    return Business.Transac.Service.Common.GetNationalityList(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        public COMMON.GetCivilStatus.CivilStatusResponse GetCivilStatusList(COMMON.GetCivilStatus.CivilStatusRequest objRequest)
        {
            COMMON.GetCivilStatus.CivilStatusResponse objResponse = null;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<COMMON.GetCivilStatus.CivilStatusResponse>(() =>
                {
                    return Business.Transac.Service.Common.GetCivilStatusList(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        #region  PROY-140245-IDEA140240

        public COMMON.GetValidateCollaborator.GetValidateCollaboratorResponse GetValidateCollaborator(Entity.Transac.Service.Common.GetValidateCollaborator.GetValidateCollaboratorRequest objRequest, string strIdSession)
        {
            COMMON.GetValidateCollaborator.GetValidateCollaboratorResponse objResponse = null;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() => Business.Transac.Service.Common.GetValidateCollaborator(objRequest, strIdSession));
            }
            catch (Exception ex)
            {
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;

        }

        public COMMON.GetConsultServiceBono.GetConsultServiceBonoResponse GetConsultServiceBono(Entity.Transac.Service.Common.GetConsultServiceBono.GetConsultServiceBonoRequest objRequest, string strIdSession)
        {
            COMMON.GetConsultServiceBono.GetConsultServiceBonoResponse objResponse = null;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() => Business.Transac.Service.Common.GetConsultServiceBono(objRequest, strIdSession));
            }
            catch (Exception ex)
            {
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        public COMMON.GetRegisterBonoSpeed.GetRegisterBonoSpeedResponse GetRegisterBonoSpeed(Entity.Transac.Service.Common.GetRegisterBonoSpeed.GetRegisterBonoSpeedRequest objRequest, string strIdSession)
        {
            COMMON.GetRegisterBonoSpeed.GetRegisterBonoSpeedResponse objResponse = null;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() => Business.Transac.Service.Common.GetRegisterBonoSpeed(objRequest, strIdSession));

            }
            catch (Exception ex)
            {
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        public COMMON.PostValidateDeliveryBAV.PostValidateDeliveryBAVResponse PostValidateDeliveryBAV(Entity.Transac.Service.Common.PostValidateDeliveryBAV.PostValidateDeliveryBAVRequest objRequest, string strIdSession)
        {
            COMMON.PostValidateDeliveryBAV.PostValidateDeliveryBAVResponse objResponse = null;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() => Business.Transac.Service.Common.PostValidateDeliveryBAV(objRequest, strIdSession));

            }
            catch (Exception ex)
            {
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));

            }
            return objResponse;
        }

        public COMMON.GetConsultCampaign.GetConsultCampaignResponse GetConsultCampaign(Entity.Transac.Service.Common.GetConsultCampaign.GetConsultCampaignRequest objRequest, string strIdSession)
        {
            COMMON.GetConsultCampaign.GetConsultCampaignResponse objResponse = null;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() => Business.Transac.Service.Common.GetConsultCampaign(objRequest, strIdSession));
            }
            catch (Exception ex)
            {
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        public COMMON.GetRegisterCampaign.GetRegisterCampaignResponse GetRegisterCampaign(Entity.Transac.Service.Common.GetRegisterCampaign.GetRegisterCampaignRequest objRequest, string strIdSession)
        {
            COMMON.GetRegisterCampaign.GetRegisterCampaignResponse objResponse = null;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() => Business.Transac.Service.Common.GetRegisterCampaign(objRequest, strIdSession));
            }
            catch (Exception ex)
            {
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        public GetReadDataUserResponse GetReadDataUser(GetReadDataUserRequest objRequest, string strIdSession)
        {

            GetReadDataUserResponse objResponse = null;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() => Business.Transac.Service.Common.GetReadDataUser(objRequest, strIdSession));
            }
            catch (Exception ex)
            {
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        public COMMON.GetValidateQuantityCampaign.GetValidateQuantityCampaignResponse GetValidateQuantityCampaign(Entity.Transac.Service.Common.GetValidateQuantityCampaign.GetValidateQuantityCampaignRequest objRequest, string strIdSession)
        {
            COMMON.GetValidateQuantityCampaign.GetValidateQuantityCampaignResponse objResponse = null;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() => Business.Transac.Service.Common.GetValidateQuantityCampaign(objRequest, strIdSession));
            }
            catch (Exception ex)
            {
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        public COMMON.GetSubmitNotificationEmail.EmailSubmitResponse EmailSubmit(Entity.Transac.Service.Common.GetSubmitNotificationEmail.EmailSubmitRequest objRequest)
        {
            COMMON.GetSubmitNotificationEmail.EmailSubmitResponse objResponse = null;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() => Business.Transac.Service.Common.EmailSubmit(objRequest));
            }
            catch (Exception ex)
            {
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        #endregion
public COMMON.GetGenerateConstancy.GenerateConstancyResponse GenerateContancyWithXml(COMMON.GetGenerateConstancy.GenerateConstancyRequest request, string xml)
        {
            COMMON.GetGenerateConstancy.GenerateConstancyResponse objResponse = null;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<COMMON.GetGenerateConstancy.GenerateConstancyResponse>(() =>
                {
                    return Business.Transac.Service.Common.GenerateContancyWithXml(request, xml);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }

         public COMMON.CheckingUser.CheckingUserResponse CheckingUser(COMMON.CheckingUser.CheckingUserRequest objCheckingUserRequest)
        {


            COMMON.CheckingUser.CheckingUserResponse objCheckingUserResponse = new COMMON.CheckingUser.CheckingUserResponse();
            try
            {
                objCheckingUserResponse = Claro.Web.Logging.ExecuteMethod<COMMON.CheckingUser.CheckingUserResponse>(() => { return Business.Transac.Service.Common.CheckingUser(objCheckingUserRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objCheckingUserRequest.Audit.Session, objCheckingUserRequest.Audit.Transaction, ex.Message);
            }
            return objCheckingUserResponse;


        }

        public EntitiesCommon.GetReceipts.ReceiptsResponse GetParamsBSCS(EntitiesCommon.GetReceipts.ReceiptsRequest objRequest)
        {
            Entity.Transac.Service.Common.GetReceipts.ReceiptsResponse objResponse;

            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<Entity.Transac.Service.Common.GetReceipts.ReceiptsResponse>(
                    () =>
                    {
                        return Business.Transac.Service.Common.GetParamsBSCS(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }


        public COMMON.GetEmployeByUser.EmployeeResponse GetEmployeByUserwithDP(COMMON.GetEmployeByUser.EmployeeRequest objRequest)
        {
            COMMON.GetEmployeByUser.EmployeeResponse objResponse;

            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<COMMON.GetEmployeByUser.EmployeeResponse>(
                    () =>
                    {
                        return Business.Transac.Service.Common.GetEmployeByUserwithDP(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        public COMMON.ReadOptionsByUser.ReadOptionsByUserResponse ReadOptionsByUserwithDP(COMMON.ReadOptionsByUser.ReadOptionsByUserRequest objRequest)
        {
            COMMON.ReadOptionsByUser.ReadOptionsByUserResponse objResponse;

            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<COMMON.ReadOptionsByUser.ReadOptionsByUserResponse>(
                    () =>
                    {
                        return Business.Transac.Service.Common.ReadOptionsByUserwithDP(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        public COMMON.GetGenerateConstancy.GenerateConstancyResponse GetGenerateContancyNamePDF(COMMON.GetGenerateConstancy.GenerateConstancyRequest request, string NombrePDF)
        {
            COMMON.GetGenerateConstancy.GenerateConstancyResponse objResponse = null;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<COMMON.GetGenerateConstancy.GenerateConstancyResponse>(() =>
                {
                    return Business.Transac.Service.Common.GetGenerateContancyNamePDF(request, NombrePDF);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }


         public Entity.Transac.Service.Common.GetConsultarClaroPuntos.ConsultarClaroPuntosResponse ConsultarClaroPuntos(Entity.Transac.Service.Common.GetConsultarClaroPuntos.ConsultarClaroPuntosRequest objRequest)
         {
             Entity.Transac.Service.Common.GetConsultarClaroPuntos.ConsultarClaroPuntosResponse objResponse = new Entity.Transac.Service.Common.GetConsultarClaroPuntos.ConsultarClaroPuntosResponse();
             objResponse = Claro.Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
             {
                 return Business.Transac.Service.Fixed.PackagePurchaseService.ConsultarClaroPuntos(objRequest);
             });
             return objResponse;
         }
         public Entity.Transac.Service.Common.GetConsultarPaqDisponibles.ConsultarPaqDisponiblesResponse ConsultarPaqDisponibles(Entity.Transac.Service.Common.GetConsultarPaqDisponibles.ConsultarPaqDisponiblesRequest objRequest)
         {
             Entity.Transac.Service.Common.GetConsultarPaqDisponibles.ConsultarPaqDisponiblesResponse objResponse = new Entity.Transac.Service.Common.GetConsultarPaqDisponibles.ConsultarPaqDisponiblesResponse();
             objResponse = Claro.Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
             {
                 return Business.Transac.Service.Fixed.PackagePurchaseService.ConsultarPaqDisponibles(objRequest);
             });
             return objResponse;
         }

         public Entity.Transac.Service.Common.GetComprarPaquetes.ComprarPaquetesBodyResponse ComprarPaquetes(Entity.Transac.Service.Common.GetComprarPaquetes.ComprarPaquetesRequest objRequest)
         {
             Entity.Transac.Service.Common.GetComprarPaquetes.ComprarPaquetesBodyResponse objResponse = new Entity.Transac.Service.Common.GetComprarPaquetes.ComprarPaquetesBodyResponse();
             objResponse = Claro.Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
             {
                 return Business.Transac.Service.Fixed.PackagePurchaseService.ComprarPaquetes(objRequest);
             });
             return objResponse;
         }

         public Entity.Transac.Service.Common.GetPCRFConsultation.PCRFConnectorResponse ConsultarPCRFDegradacion(Entity.Transac.Service.Common.GetPCRFConsultation.PCRFConnectorRequest objRequest)
         {
             Entity.Transac.Service.Common.GetPCRFConsultation.PCRFConnectorResponse objResponse = new Entity.Transac.Service.Common.GetPCRFConsultation.PCRFConnectorResponse();
             objResponse = Claro.Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction,
              () =>
              {
                  return Business.Transac.Service.Fixed.PackagePurchaseService.ConsultarPCRFDegradacion(objRequest);
              });

             return objResponse;
         }

         public List<Entity.Transac.Service.Common.Client> GetDatosporNroDocumentos(string strIdSession, string strTransaction, string strTipDoc, string strDocumento, string strEstado)
         {
             var objResponse = new List<Entity.Transac.Service.Common.Client>();
             objResponse = Claro.Web.Logging.ExecuteMethod(strIdSession, strTransaction,
              () =>
              {
                  return Business.Transac.Service.Fixed.PackagePurchaseService.GetDatosporNroDocumentos(strIdSession, strTransaction, strTipDoc, strDocumento, strEstado);
              });

             return objResponse;
         }     

        //CAYCHOJJ onBase
         public UploadDocumentOnBaseResponse GetUploadDocumentOnBase(UploadDocumentOnBaseRequest objUploadDocumentOnBaseRequest)
         {

             UploadDocumentOnBaseResponse objUploadDocumentOnBaseResponse;

             try
             {
                 objUploadDocumentOnBaseResponse = Claro.Web.Logging.ExecuteMethod<UploadDocumentOnBaseResponse>(() => { return Business.Transac.Service.Common.GetUploadDocumentOnBase(objUploadDocumentOnBaseRequest); });
             }
             catch (Exception ex)
             {
                 Claro.Web.Logging.Error(objUploadDocumentOnBaseRequest.Audit.Session, objUploadDocumentOnBaseRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                 throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
             }
             return objUploadDocumentOnBaseResponse;
         }


         /// <summary>
         /// Obtener Telefonos Cliente LTE
         /// </summary>
         /// <param name="objRequest"></param>
         /// <returns></returns>
         /// <remarks>ObtenerTelefonosClienteLTE</remarks>
         /// <list type="bullet">
         /// <item><CreadoPor>Everis</CreadoPor></item>
         /// <item><FecCrea>06-02-2019</FecCrea></item></list>
         /// <list type="bullet">
         /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
         /// <item><Resp>Hitss</Resp></item>
         /// <item><Mot>Motivo por el cual se hace la modificación</Mot></item></list>
         public COMMON.GetPCRFConsultation.PCRFConnectorResponse ObtenerTelefonosClienteLTE(COMMON.GetPCRFConsultation.PCRFConnectorRequest objRequest)
         {
             COMMON.GetPCRFConsultation.PCRFConnectorResponse objTelefonoCliente = new COMMON.GetPCRFConsultation.PCRFConnectorResponse();
             try
             {
                 objTelefonoCliente = Claro.Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                 {
                     return Business.Transac.Service.Fixed.PackagePurchaseService.ObtenerTelefonosClienteLTE(objRequest);
                 });
             }
             catch (Exception ex)
             {
                 Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, Claro.Utils.GetExceptionMessage(ex));
                 throw new FaultException(Claro.Utils.GetExceptionMessage(ex));
             }
             return objTelefonoCliente;
         }

       
    }
}
