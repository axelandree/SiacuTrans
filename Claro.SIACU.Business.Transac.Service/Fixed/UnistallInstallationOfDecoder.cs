using BE = Claro.SIACU.Entity.Transac.Service.Fixed;
using CSTS = Claro.SIACU.Transac.Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Claro.SIACU.Business.Transac.Service.Fixed
{
    public class UnistallInstallationOfDecoder
    {
        public static BE.GetProductDetail.ProductDetailResponse GetProductDetail(BE.GetProductDetail.ProductDetailRequest objProductDetailRequest)
        {
            var objProductDetailResponse = new BE.GetProductDetail.ProductDetailResponse();

            try
            {
                int rResultado = 0;
                string rMensaje = string.Empty;

                objProductDetailResponse.listDecoder = Web.Logging.ExecuteMethod<List<BE.Decoder>>(objProductDetailRequest.Audit.Session, objProductDetailRequest.Audit.Transaction,
                    () =>
                    {
                        return Data.Transac.Service.Fixed.UnistallInstallationOfDecoder.GetProductDetail(
                            objProductDetailRequest.Audit.Session,
                            objProductDetailRequest.Audit.Transaction,
                            objProductDetailRequest.vCustomerId,
                            objProductDetailRequest.vCoId,
                            objProductDetailRequest.tipoBusqueda,
                            ref rResultado,
                            ref rMensaje);
                    });
                objProductDetailResponse.rResultado = rResultado;
                objProductDetailResponse.rMensaje = rMensaje;
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objProductDetailRequest.Audit.Session, objProductDetailRequest.Audit.Transaction, ex.Message);
            }

            return objProductDetailResponse;
        }
        public static BE.GetAddtionalEquipment.AddtionalEquipmentResponse GetAddtionalEquipment(BE.GetAddtionalEquipment.AddtionalEquipmentRequest objRequest)
        {
            string estado = string.Empty;
            var objResponse = new BE.GetAddtionalEquipment.AddtionalEquipmentResponse();
            List<BE.PlanService> objListPlanService = null;
            BE.PlanService objPlanService = null;

            try
            {
                var lstEntity = Web.Logging.ExecuteMethod<List<BE.PlanService>>(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Data.Transac.Service.Fixed.AdditionalServices.GetPlanServices(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.IdPlan, objRequest.TypeProduct);
                    });

                objListPlanService = new List<BE.PlanService>();
                foreach (BE.PlanService item in lstEntity)
                {
                    objPlanService = new BE.PlanService();
                    estado = Web.Logging.ExecuteMethod<string>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                    {
                        return Data.Transac.Service.Fixed.PlanMigrationHfc.GetDecoderServiceStatus(objRequest.Audit.Session, objRequest.Audit.Transaction, CSTS.Functions.CheckInt(objRequest.coid), CSTS.Functions.CheckInt(item.SNCode));
                    });

                    if (estado == "O" || estado == "A" || estado == "S")
                    { }
                    else { objPlanService = item; }

                    objListPlanService.Add(objPlanService);
                }

                objResponse.LstPlanServices = objListPlanService;
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }
        public static BE.GetProcessingServices.ProcessingServicesResponse GetProcessingServices(BE.GetProcessingServices.ProcessingServicesRequest objProcessingServicesRequest)
        {
            BE.GetProcessingServices.ProcessingServicesResponse objProcessingServicesResponse = new BE.GetProcessingServices.ProcessingServicesResponse();

            try
            {
                string rResultado = "";
                string rMensaje = string.Empty;

                objProcessingServicesResponse.rResult = Web.Logging.ExecuteMethod(objProcessingServicesRequest.Audit.Session, objProcessingServicesRequest.Audit.Transaction,
                    () =>
                    {
                        return Data.Transac.Service.Fixed.UnistallInstallationOfDecoder.GetProcessingServices(objProcessingServicesRequest.Audit.Session,
                                                                        objProcessingServicesRequest.Audit.Transaction,
                                                                        objProcessingServicesRequest.vCoId,
                                                                        objProcessingServicesRequest.vCustomerId,
                                                                        objProcessingServicesRequest.vCadena,
                                                                        ref rResultado,
                                                                        ref rMensaje);
                    });

                objProcessingServicesResponse.rResultado = rResultado;
                objProcessingServicesResponse.rMensaje = rMensaje;
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objProcessingServicesRequest.Audit.Session, objProcessingServicesRequest.Audit.Transaction, ex.Message);
            }

            return objProcessingServicesResponse;
        }
        public static BE.GetDecoDetailByIdService.DecoDetailByIdServiceResponse GetDecoDetailByIdService(BE.GetDecoDetailByIdService.DecoDetailByIdServiceRequest objDecoDetailByIdServiceRequest)
        {
            var objDecoDetailByIdServiceResponse = new BE.GetDecoDetailByIdService.DecoDetailByIdServiceResponse();

            try
            {
                objDecoDetailByIdServiceResponse.listaServicio = Web.Logging.ExecuteMethod<List<BE.DetailInteractionService>>(objDecoDetailByIdServiceRequest.Audit.Session, objDecoDetailByIdServiceRequest.Audit.Transaction,
                    () =>
                    {
                        return Data.Transac.Service.Fixed.UnistallInstallationOfDecoder.GetDecoDetailByIdService(
                            objDecoDetailByIdServiceRequest.Audit.Session,
                            objDecoDetailByIdServiceRequest.Audit.Transaction,
                            objDecoDetailByIdServiceRequest.strIdServicio
                            );
                    });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objDecoDetailByIdServiceRequest.Audit.Session, objDecoDetailByIdServiceRequest.Audit.Transaction, ex.Message);
            }

            return objDecoDetailByIdServiceResponse;
        }
        public static BE.GetProductDetail.ProductDetailResponse GetProductDown(BE.GetProductDetail.ProductDetailRequest objProductDetailRequest)
        {
            var objProductDetailResponse = new BE.GetProductDetail.ProductDetailResponse();

            try
            {
                int rResultado = 0;
                string rMensaje = string.Empty;

                objProductDetailResponse.listDecoder = Web.Logging.ExecuteMethod<List<BE.Decoder>>(objProductDetailRequest.Audit.Session, objProductDetailRequest.Audit.Transaction,
                    () =>
                    {
                        return Data.Transac.Service.Fixed.UnistallInstallationOfDecoder.GetProductDown(
                            objProductDetailRequest.Audit.Session,
                            objProductDetailRequest.Audit.Transaction,
                            objProductDetailRequest.vCustomerId,
                            objProductDetailRequest.vCoId,
                            objProductDetailRequest.tipoBusqueda,
                            ref rResultado,
                            ref rMensaje);
                    });
                objProductDetailResponse.rResultado = rResultado;
                objProductDetailResponse.rMensaje = rMensaje;
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objProductDetailRequest.Audit.Session, objProductDetailRequest.Audit.Transaction, ex.Message);
            }

            return objProductDetailResponse;
        }
        public static BE.GetLoyaltyAmount.LoyaltyAmountResponse GetLoyaltyAmount(BE.GetLoyaltyAmount.LoyaltyAmountRequest objLoyaltyAmountRequest)
        {
            var objLoyaltyAmountResponse = new BE.GetLoyaltyAmount.LoyaltyAmountResponse();

            try
            {
                objLoyaltyAmountResponse.strMonto = Web.Logging.ExecuteMethod<string>(objLoyaltyAmountRequest.Audit.Session, objLoyaltyAmountRequest.Audit.Transaction,
                    () =>
                    {
                        return Data.Transac.Service.Fixed.UnistallInstallationOfDecoder.GetLoyaltyAmount(
                            objLoyaltyAmountRequest.Audit.Session,
                            objLoyaltyAmountRequest.Audit.Transaction,
                            objLoyaltyAmountRequest.iTipo);
                    });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objLoyaltyAmountRequest.Audit.Session, objLoyaltyAmountRequest.Audit.Transaction, ex.Message);
            }

            return objLoyaltyAmountResponse;
        }


        public static Entity.Transac.Service.Fixed.GetServicesByPlan.PlanServiceResponse GetServicesByPlan(Entity.Transac.Service.Fixed.GetServicesByPlan.PlanServiceRequest objServicesRequest)
        {
            List<Entity.Transac.Service.Fixed.ServiceByPlan> listServiceResponse = null;
            List<Entity.Transac.Service.Fixed.ServiceByPlan> listService = null;
            List<Entity.Transac.Service.Fixed.ServiceByPlan> listServiceOrdered;
            try
            {
                listService = Web.Logging.ExecuteMethod<List<Entity.Transac.Service.Fixed.ServiceByPlan>>(objServicesRequest.Audit.Session, objServicesRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.UnistallInstallationOfDecoder.GetServicesByPlan(objServicesRequest.Audit.Session, objServicesRequest.Audit.Transaction, objServicesRequest.idplan, objServicesRequest.strTipoProducto);
                    
                });

            }
            catch (Exception ex)
            {
                Web.Logging.Error(objServicesRequest.Audit.Session, objServicesRequest.Audit.Transaction, ex.Message);
            }

            if (listService != null)
            {
                listServiceResponse = new List<Entity.Transac.Service.Fixed.ServiceByPlan>();
                listServiceOrdered = listService.OrderBy(a => a.CodeExternal).ToList();
                listServiceResponse = listServiceOrdered;
            }
            Entity.Transac.Service.Fixed.GetServicesByPlan.PlanServiceResponse objPlansResponse = new Entity.Transac.Service.Fixed.GetServicesByPlan.PlanServiceResponse()
            {
                listServicio = listServiceResponse
            };
            return objPlansResponse;
        }

        public static Entity.Transac.Service.Fixed.GetDecoMatriz.DecoMatrizResponse GetDecoMatriz(Entity.Transac.Service.Fixed.GetDecoMatriz.DecoMatrizRequest objDecoMatrizRequest)
        {
            
            var objDecoMatrizResponse = new Entity.Transac.Service.Fixed.GetDecoMatriz.DecoMatrizResponse();


            try
            {
                objDecoMatrizResponse = Web.Logging.ExecuteMethod<Entity.Transac.Service.Fixed.GetDecoMatriz.DecoMatrizResponse>(objDecoMatrizRequest.Audit.Session, objDecoMatrizRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.UnistallInstallationOfDecoder.GetDecoMatriz(objDecoMatrizRequest);

                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objDecoMatrizRequest.Audit.Session, objDecoMatrizRequest.Audit.Transaction, ex.Message);
            }
            
            return objDecoMatrizResponse;
        }


        public static Entity.Transac.Service.Fixed.GetDecoType.DecoTypeResponse GetDecoType(Entity.Transac.Service.Fixed.GetDecoType.DecoTypeRequest objDecoTypeRequest)
        {

            var objDecoTypeResponse = new Entity.Transac.Service.Fixed.GetDecoType.DecoTypeResponse();


            try
            {
                objDecoTypeResponse = Web.Logging.ExecuteMethod<Entity.Transac.Service.Fixed.GetDecoType.DecoTypeResponse>(objDecoTypeRequest.Audit.Session, objDecoTypeRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.UnistallInstallationOfDecoder.GetDecoType(objDecoTypeRequest);

                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objDecoTypeRequest.Audit.Session, objDecoTypeRequest.Audit.Transaction, ex.Message);
            }

            return objDecoTypeResponse;
        }

        public static Entity.Transac.Service.Fixed.PostExecuteDecosLte.DecosLteResponse PostExecuteDecosLte(Entity.Transac.Service.Fixed.PostExecuteDecosLte.DecosLteRequest objRequest)
        {

            var objResponse = new Entity.Transac.Service.Fixed.PostExecuteDecosLte.DecosLteResponse();


            try
            {
                objResponse = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.UnistallInstallationOfDecoder.ExecuteUninstallInstallDecosLte(objRequest);

                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }

        public static BE.GetLoyaltyAmount.LoyaltyAmountResponse GetLoyaltyAmountLte(BE.GetLoyaltyAmount.LoyaltyAmountRequest objLoyaltyAmountRequest)
        {
            var objLoyaltyAmountResponse = new BE.GetLoyaltyAmount.LoyaltyAmountResponse();

            try
            {
                objLoyaltyAmountResponse.strMonto = Web.Logging.ExecuteMethod(objLoyaltyAmountRequest.Audit.Session, objLoyaltyAmountRequest.Audit.Transaction,
                    () =>
                    {
                        return Data.Transac.Service.Fixed.UnistallInstallationOfDecoder.GetLoyaltyAmountLte(
                            objLoyaltyAmountRequest.Audit.Session,
                            objLoyaltyAmountRequest.Audit.Transaction,
                            objLoyaltyAmountRequest.iTipo);
                    });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objLoyaltyAmountRequest.Audit.Session, objLoyaltyAmountRequest.Audit.Transaction, CSTS.Functions.GetExceptionMessage(ex));
            }

            return objLoyaltyAmountResponse;
        }
    }
}
