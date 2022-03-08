using Claro.SIACU.Transac.Service;
using Claro.SIACU.Web.WebApplication.Transac.Service.App_Code;
using Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.CommonServices.Fixed;
using Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.HFC.MigrationPlan;
using Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models.HFC;
using Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KEY = Claro.ConfigurationManager;
using System.Web.Mvc;
using oTransacServ = Claro.SIACU.Transac.Service;
using AuditRequestFixed = Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService.AuditRequest;
using Newtonsoft.Json;
using System.Xml.Xsl;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.IO;
using System.Xml.Linq;
using Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models.LTE;
using CSTS = Claro.SIACU.Transac.Service;
using Constant = Claro.SIACU.Transac.Service.Constants;
using Newtonsoft;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Controllers.HFC
{
    public class PlanMigrationController : CommonServicesController
    {

        public PlanMigrationController() {
            Claro.Web.Logging.Configure();//Temporal
        }
        private readonly CommonTransacService.CommonTransacServiceClient objCommonTransacService = new CommonTransacService.CommonTransacServiceClient();
        //
        // GET: /Transactions/PlanMigration/
        /// <summary>
        /// Vista inicial para Cambio de Plan
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <returns></returns>
        public ActionResult HfcPlanMigration(string strIdSession)
        {
           
            Claro.Web.Logging.Info("HFCLoad", "HFCPlanMigration", "Entro a Load"); // Temporal
            ViewBag.server = KEY.AppSettings("strServidorLeerPDF").ToString();
            ViewBag.ErrorMessageIgv = KEY.AppSettings("strMensajeErrorConsultaIGV").ToString();
            ViewBag.strMessageValidationETA = KEY.AppSettings("strMessageETAValidation").ToString();
            ViewBag.strMessageSave = KEY.AppSettings("strAlertaEstaSegGuarCam").ToString();
            Models.HFC.MigrationModel oMigracionModel = new Models.HFC.MigrationModel();
            oMigracionModel.ServerDate = DateTime.Now.ToString("yyyy/MM/dd");
            oMigracionModel.strEstadoContratoInactivo = KEY.AppSettings("strEstadoContratoInactivo");
            oMigracionModel.strEstadoContratoSuspendido = KEY.AppSettings("strEstadoContratoSuspendido");
            oMigracionModel.strEstadoContratoReservado = KEY.AppSettings("strEstadoContratoReservado");
            oMigracionModel.strMsjEstadoContratoInactivo = KEY.AppSettings("strMsjEstadoContratoInactivo");
            oMigracionModel.strMsjEstadoContratoSuspendido = KEY.AppSettings("strMsjEstadoContratoSuspendido");
            oMigracionModel.strMsjEstadoContratoReservado = KEY.AppSettings("strMsjEstadoContratoReservado");
            oMigracionModel.strMensajeTransaccionFTTH = KEY.AppSettings("strMensajeBackOfficeFTTH"); //RONALDRR - INICIO
            oMigracionModel.strPlanoFTTH = KEY.AppSettings("strPlanoFTTH"); //RONALDRR - FIN
            ViewBag.oMigracionModel = oMigracionModel;
            Claro.Web.Logging.Info("HFCLoad", "HFCPlanMigration", "Se procesó Load");

            int number = Convert.ToInt(KEY.AppSettings("strIncrementDefault", "1"));
            ViewData["SERVERDATEDEFAULT"] = DateTime.Now.AddDays(number).ToString("yyyy/MM/dd");


            return PartialView(oMigracionModel);
        }
        public JsonResult GetSessionParameters(string strIdSession, string strContrato, string strCustomerId, string strData)
        {
            Claro.Web.Logging.Info("GetSessionParameters", "PlanMigration", "Metodo GetSessionParameters");
            Claro.Web.Logging.Info("GetSessionParameters", "SessionTransacHFC: ", strData);
            GetHubsRequest request = new GetHubsRequest
            {
                strContrato = strContrato,
                strCustomerId = strCustomerId,
                audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession)
            };
            GetHubsResponse response = null;
            try
            {
                response = Claro.Web.Logging.ExecuteMethod<GetHubsResponse>(() =>
                {
                    return new FixedTransacServiceClient().GetHubsHfc(request);
                });
            }
            catch (Exception ex)
            {
                response = null;
                Claro.Web.Logging.Error(strIdSession, request.audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }
            Claro.Web.Logging.Info(strIdSession, request.audit.transaction, JsonConvert.SerializeObject(response));
            Claro.Web.Logging.Info("LTEGetSessionParameters", "LTEPlanMigration", "Fin GetSessionParameters"); // Temporal
            return Json(new { data = response });
        }

        public List<Helpers.LoadSelectHelper> GetCarriers(string strIDSession)
        {
            List<Helpers.LoadSelectHelper> lstCarriers = new List<Helpers.LoadSelectHelper>();
            FixedTransacService.CarrierResponseHfc objCarrierResponse = new CarrierResponseHfc();
            FixedTransacService.CarrierRequestHfc objCarrierRequest = new CarrierRequestHfc()
            {
                audit = App_Code.Common.CreateAuditRequest<AuditRequest>(strIDSession)
            };

            try
            {
                objCarrierResponse = Claro.Web.Logging.ExecuteMethod<CarrierResponseHfc>(() =>
                {
                    return new FixedTransacServiceClient().GetCarrierList(objCarrierRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objCarrierRequest.audit.Session, objCarrierRequest.audit.transaction, ex.Message);
            }

            Claro.Web.Logging.Info(objCarrierRequest.audit.Session, objCarrierRequest.audit.transaction, JsonConvert.SerializeObject(objCarrierResponse));

            if (objCarrierResponse != null || objCarrierResponse.carriers != null)
                {
                foreach (Carrier item in objCarrierResponse.carriers)
                                                   {
                    lstCarriers.Add(new Helpers.LoadSelectHelper()
                {
                        strCode = item.IDCARRIER,
                        strDescription = StringManipulation.RemoveAccentsAndUpper(item.OPERADOR)
                    });
                }
            }

            return lstCarriers;
                }

        public JsonResult LoadWorkTypes(string strIDSession, int intType)
                {
            PlanMigrationLoadModel oModel = new PlanMigrationLoadModel();
            oModel.lstWorkType = GetWorkType(strIDSession, intType);

            if (oModel.lstWorkType.Count == Claro.Constants.NumberOne)
                                                   {
                oModel.lstSubWorkType = GetSubWorkType(strIDSession, oModel.lstWorkType[Claro.Constants.NumberZero].strCode);
                }
                else
                {
                oModel.lstSubWorkType = null;
            }

            oModel.lstCarriers = GetCarriers(strIDSession);

            return Json(new { data = oModel });
                }

        public List<Helpers.LoadSelectHelper> GetWorkType(string strIDSession, int intType)
        {
            List<Helpers.LoadSelectHelper> lstWorkType = new List<Helpers.LoadSelectHelper>();
            JobTypesResponseHfc oResponse;
            JobTypesRequestHfc oRequest = new JobTypesRequestHfc
            {
                p_tipo = intType,
                audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIDSession)
            };

            try
            {
                oResponse = Claro.Web.Logging.ExecuteMethod<JobTypesResponseHfc>(() =>
                {
                    return new FixedTransacServiceClient().GetJobTypes(oRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIDSession, oRequest.audit.transaction, ex.Message);
                throw new Exception(ex.Message);
                }

            Claro.Web.Logging.Info(strIDSession, oRequest.audit.transaction, JsonConvert.SerializeObject(oResponse));

            foreach (JobType item in oResponse.JobTypes)
                {
                lstWorkType.Add(new Helpers.LoadSelectHelper()
                {
                    strCode = item.tiptra,
                    strDescription = item.descripcion
                });
            }

            return lstWorkType;
        }

       public List<Helpers.LoadSelectHelper> GetSubWorkType(string strIDSession, string strJobType)
        {
            List<Helpers.LoadSelectHelper> lstSubWorkType = new List<Helpers.LoadSelectHelper>();
            OrderSubTypesResponseHfc oResponse = new OrderSubTypesResponseHfc();
            OrderSubTypesRequestHfc oRequest = new OrderSubTypesRequestHfc
            {
                av_cod_tipo_trabajo = strJobType,
                audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIDSession)
            };

            MigrationModel objMigracionModel = new MigrationModel();
            try
            {
                oResponse = Claro.Web.Logging.ExecuteMethod<OrderSubTypesResponseHfc>(() =>
                {
                    return new FixedTransacServiceClient().GetOrderSubTypeWork(oRequest);
                });
            }
            catch (Exception ex)
                {
                Claro.Web.Logging.Error(oRequest.audit.Session, oRequest.audit.transaction, ex.Message);
                }

            Claro.Web.Logging.Info(oRequest.audit.Session, oRequest.audit.transaction, JsonConvert.SerializeObject(oResponse));

            if (oResponse != null || oResponse.OrderSubTypes != null)
                {
                foreach (OrderSubType item in oResponse.OrderSubTypes)
                {
                    lstSubWorkType.Add(new Helpers.LoadSelectHelper()
                {
                        strCode = string.Format("{0}|{1}|{2}", item.COD_SUBTIPO_ORDEN, item.TIEMPO_MIN, item.ID_SUBTIPO_ORDEN),
                        strDescription = item.DESCRIPCION,
                        strTypeService = item.TIPO_SERVICIO
                });
            }
            }

            return lstSubWorkType;
        }

        public JsonResult ListServicesByPlan(string strIdSession, string idPlan, string strProductType)
        {
            Claro.Web.Logging.Info("HFCListServicesByPlan", "HFCPlanMigration", "Entro a ListServicesByPlan"); // Temporal

            Models.HFC.MigrationModel oMigracionModel = new Models.HFC.MigrationModel();

            FixedTransacService.PlanServiceResponse objServicesResponse;
            FixedTransacService.PlanServiceRequest objServicesRequest = new FixedTransacService.PlanServiceRequest
            {
                idplan = idPlan,
                strTipoProducto = strProductType,
                audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession)
            };
            Claro.Web.Logging.Info("HFCListServicesByPlan", "HFCPlanMigration", "Request_HFCListServicesByPlan : " + JsonConvert.SerializeObject(objServicesRequest)); // Temporal
            try
            {
                objServicesResponse = Claro.Web.Logging.ExecuteMethod<FixedTransacService.PlanServiceResponse>(() => { return new FixedTransacService.FixedTransacServiceClient().HfcGetServicesByPlan(objServicesRequest); });
                Claro.Web.Logging.Info("HFCListServicesByPlan", "HFCPlanMigration", "Response_HFCListServicesByPlan : " + JsonConvert.SerializeObject(objServicesResponse)); // Temporal

                if (objServicesResponse.listServicio != null)
                {
                    oMigracionModel.ServicesByPlan = objServicesResponse.listServicio;
                    oMigracionModel.ServicesByPlan = (from ele in oMigracionModel.ServicesByPlan
                                                      group ele by ele.CodServSisact
                                                          into groups
                                                          select groups.OrderBy(x => x.CodServiceType).First()).ToList();

                    var valueIgv = GetCommonConsultIgv(strIdSession).igvD + 1;
                    foreach (var item in oMigracionModel.ServicesByPlan)
                    {
                        if (!string.IsNullOrEmpty(item.CF))
                        {
                            item.CfWithIgv = string.Format("{0:0.00}", Double.Parse(item.CF) * valueIgv);
                        }
                    }

                    Claro.Web.Logging.Info("HFCListServicesByPlan", "HFCPlanMigration", "oMigracionModel.ServicesByPlan : " + JsonConvert.SerializeObject(oMigracionModel.ServicesByPlan)); // Temporal

                }
            }
            catch (Exception ex)
            {
                objServicesResponse = null;
                Claro.Web.Logging.Info("HFCListServicesByPlan", "HFCPlanMigration", "HFCListServicesByPlan_en_catch : " + JsonConvert.SerializeObject(ex)); // Temporal
                Claro.Web.Logging.Error(strIdSession, objServicesRequest.audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }
            Claro.Web.Logging.Info(strIdSession, objServicesRequest.audit.transaction, JsonConvert.SerializeObject(oMigracionModel));
            Claro.Web.Logging.Info("HFCListServicesByPlan", "HFCPlanMigration", "Finalizó ListServicesByPlan"); // Temporal

            if (oMigracionModel.ServicesByPlan == null)
            {
                Claro.Web.Logging.Info("HFCPlanMigration", "ListServicesByPlan", " oMigracionModel.ServicesByPlan == null");
                oMigracionModel.ServicesByPlan = new List<ServiceByPlan>();
            }
            //string temporal = JsonConvert.SerializeObject(oMigracionModel.ServicesByPlan);//Persquash
            return Json(new { data = oMigracionModel.ServicesByPlan });
        }
        public JsonResult ListCoreServicesByPlan(string strIdSession, string idPlan, string strProductType)
        {
            Claro.Web.Logging.Info("HFCListServicesByPlan", "HFCPlanMigration", "Entro a ListServicesByPlan"); // Temporal

            Models.HFC.MigrationModel oMigracionModel = new Models.HFC.MigrationModel();

            FixedTransacService.PlanServiceResponse objServicesResponse;
            FixedTransacService.PlanServiceRequest objServicesRequest = new FixedTransacService.PlanServiceRequest
            {
                idplan = idPlan,
                strTipoProducto = strProductType,
                audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession)
            };
            try
            {
                objServicesResponse = Claro.Web.Logging.ExecuteMethod<FixedTransacService.PlanServiceResponse>(() => { return new FixedTransacService.FixedTransacServiceClient().HfcGetServicesByPlan(objServicesRequest); });
                if (objServicesResponse.listServicio != null)
                {
                    oMigracionModel.ServicesByPlan = objServicesResponse.listServicio;
                    oMigracionModel.ServicesByPlan = (from ele in oMigracionModel.ServicesByPlan
                                                      group ele by ele.CodServSisact
                                                          into groups
                                                          select groups.OrderBy(x => x.CodServiceType).First()).ToList();
                }
            }
            catch (Exception ex)
            {
                objServicesResponse = null;
                Claro.Web.Logging.Error(strIdSession, objServicesRequest.audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }
            Claro.Web.Logging.Info(strIdSession, objServicesRequest.audit.transaction, JsonConvert.SerializeObject(oMigracionModel));
            Claro.Web.Logging.Info("HFCListServicesByPlan", "HFCPlanMigration", "Finalizó ListServicesByPlan"); // Temporal

            return Json(new { data = oMigracionModel.ServicesByPlan });
        }
        public ActionResult _Cable()
        {
            return PartialView();
        }
        public ActionResult _Internet()
        {
            return PartialView();
        }

        public ActionResult TablePhoneServices(string strIdSession, string idPlan, string identificadorDescIdEquipo, string strProductType)
        {
            Claro.Web.Logging.Info("HFCTablePhoneServices", "HFCPlanMigration", "Entro a TablePhoneServices");
            Models.HFC.MigrationModel oMigracionModel = new Models.HFC.MigrationModel();

            FixedTransacService.PlanServiceResponse objServicesResponse;
            FixedTransacService.PlanServiceRequest objServicesRequest = new FixedTransacService.PlanServiceRequest
            {
                idplan = idPlan,
                strTipoProducto = strProductType,
                audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession)
            };
            try
            {
                objServicesResponse = Claro.Web.Logging.ExecuteMethod<FixedTransacService.PlanServiceResponse>(() => { return new FixedTransacService.FixedTransacServiceClient().HfcGetServicesByPlan(objServicesRequest); });
                oMigracionModel.ServicesByPlan = objServicesResponse.listServicio;
                oMigracionModel.ServicesByPlan = (from ele in oMigracionModel.ServicesByPlan
                                                  group ele by ele.CodServSisact
                                                      into groups
                                                      select groups.OrderBy(x => x.CodServiceType).First()).ToList();

                var myClause = new string[] { "1", "6" };
                oMigracionModel.ServicesByPlan = (from ele in oMigracionModel.ServicesByPlan
                                                  where myClause.Contains(ele.CodGroupServ) & ele.CodServiceType.Equals("1")
                                                  select ele).ToList();
            }
            catch (Exception ex)
            {
                objServicesResponse = null;
                Claro.Web.Logging.Error(strIdSession, objServicesRequest.audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }
            Claro.Web.Logging.Info(strIdSession, objServicesRequest.audit.transaction, JsonConvert.SerializeObject(oMigracionModel));
            Claro.Web.Logging.Info("HFCTablePhoneServices", "HFCPlanMigration", "Fin TablePhoneServices");
            return PartialView(oMigracionModel);
        }
        public ActionResult CoreCable(string strIdSession, string idPlan, string strProductType)
        {
            Claro.Web.Logging.Info("HFCCoreCable", "HFCPlanMigration", "Entro a CoreCable");
            Models.HFC.MigrationModel oMigracionModel = new Models.HFC.MigrationModel();

            FixedTransacService.PlanServiceResponse objServicesResponse;
            FixedTransacService.PlanServiceRequest objServicesRequest = new FixedTransacService.PlanServiceRequest
            {
                idplan = idPlan,
                strTipoProducto = strProductType,
                audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession)
            };
            try
            {
                objServicesResponse = Claro.Web.Logging.ExecuteMethod<FixedTransacService.PlanServiceResponse>(() => { return new FixedTransacService.FixedTransacServiceClient().HfcGetServicesByPlan(objServicesRequest); });
                oMigracionModel.ServicesByPlan = objServicesResponse.listServicio;
                oMigracionModel.ServicesByPlan = (from ele in oMigracionModel.ServicesByPlan
                                                  group ele by ele.CodServSisact
                                                      into groups
                                                      select groups.OrderBy(x => x.CodServiceType).First()).ToList();

                var myClause = new string[] { "3", "5", "4" };
                oMigracionModel.ServicesByPlan = (from ele in oMigracionModel.ServicesByPlan
                                                  where myClause.Contains(ele.CodGroupServ) & ele.CodServiceType.Equals("1")
                                                  select ele).ToList();
            }
            catch (Exception ex)
            {
                objServicesResponse = null;
                Claro.Web.Logging.Error(strIdSession, objServicesRequest.audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }
            Claro.Web.Logging.Info(strIdSession, objServicesRequest.audit.transaction, JsonConvert.SerializeObject(oMigracionModel));
            Claro.Web.Logging.Info("HFCCoreCable", "HFCPlanMigration", "Fin CoreCable");
            return PartialView("~/Areas/Transactions/Views/PlanMigration/HFCCoreCable.cshtml", oMigracionModel);

        }

        public ActionResult CoreInternet(string strIdSession, string idPlan, string strProductType)
        {
            Claro.Web.Logging.Info("HFCCoreInternet", "HFCPlanMigration", "Entro a CoreInternet");
            Models.HFC.MigrationModel oMigracionModel = new Models.HFC.MigrationModel();

            FixedTransacService.PlanServiceResponse objServicesResponse;
            FixedTransacService.PlanServiceRequest objServicesRequest = new FixedTransacService.PlanServiceRequest
            {
                idplan = idPlan,
                strTipoProducto = strProductType,
                audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession)
            };
            try
            {
                objServicesResponse = Claro.Web.Logging.ExecuteMethod<FixedTransacService.PlanServiceResponse>(() => { return new FixedTransacService.FixedTransacServiceClient().HfcGetServicesByPlan(objServicesRequest); });
                oMigracionModel.ServicesByPlan = objServicesResponse.listServicio;
                oMigracionModel.ServicesByPlan = (from ele in oMigracionModel.ServicesByPlan
                                                  group ele by ele.CodServSisact
                                                      into groups
                                                      select groups.OrderBy(x => x.CodServiceType).First()).ToList();

                var myClause = new string[] { "2", "7" };
                oMigracionModel.ServicesByPlan = (from ele in oMigracionModel.ServicesByPlan
                                                  where myClause.Contains(ele.CodGroupServ) & ele.CodServiceType.Equals("1")
                                                  select ele).ToList();
            }
            catch (Exception ex)
            {
                objServicesResponse = null;
                Claro.Web.Logging.Error(strIdSession, objServicesRequest.audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }
            Claro.Web.Logging.Info(strIdSession, objServicesRequest.audit.transaction, JsonConvert.SerializeObject(oMigracionModel));
            Claro.Web.Logging.Info("HFCCoreInternet", "HFCPlanMigration", "Fin CoreInternet");
            return PartialView("~/Areas/Transactions/Views/PlanMigration/HFCCoreInternet.cshtml", oMigracionModel);
        }
        public ActionResult CorePhone(string strIdSession, string idPlan, string strProductType)
        {
            Claro.Web.Logging.Info("HFCCorePhone", "HFCPlanMigration", "Entro a CorePhone");
            Models.HFC.MigrationModel oMigracionModel = new Models.HFC.MigrationModel();

            FixedTransacService.PlanServiceResponse objServicesResponse;
            FixedTransacService.PlanServiceRequest objServicesRequest = new FixedTransacService.PlanServiceRequest
            {
                idplan = idPlan,
                strTipoProducto = strProductType,
                audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession)
            };
            try
            {
                objServicesResponse = Claro.Web.Logging.ExecuteMethod<FixedTransacService.PlanServiceResponse>(() => { return new FixedTransacService.FixedTransacServiceClient().HfcGetServicesByPlan(objServicesRequest); });
                oMigracionModel.ServicesByPlan = objServicesResponse.listServicio;
                oMigracionModel.ServicesByPlan = (from ele in oMigracionModel.ServicesByPlan
                                                  group ele by ele.CodServSisact
                                                      into groups
                                                      select groups.OrderBy(x => x.CodServiceType).First()).ToList();

                var myClause = new string[] { "1", "6" };
                oMigracionModel.ServicesByPlan = (from ele in oMigracionModel.ServicesByPlan
                                                  where myClause.Contains(ele.CodGroupServ) & ele.CodServiceType.Equals("1")
                                                  select ele).ToList();
            }
            catch (Exception ex)
            {
                objServicesResponse = null;
                Claro.Web.Logging.Error(strIdSession, objServicesRequest.audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }
            Claro.Web.Logging.Info(strIdSession, objServicesRequest.audit.transaction, JsonConvert.SerializeObject(oMigracionModel));
            Claro.Web.Logging.Info("HFCCorePhone", "HFCPlanMigration", "Fin CorePhone");
            return PartialView("~/Areas/Transactions/Views/PlanMigration/HFCCorePhone.cshtml", oMigracionModel);

        }

        public ActionResult _ListServicesByPlan(string strIdSession, string idPlan, string strProductType, string strSelectEquipments)
        {
            List<ServiceByPlan> aditionalEquipments = JsonConvert.DeserializeObject<List<ServiceByPlan>>(strSelectEquipments);
            MigrationModel oMigrationModel = new MigrationModel();
            oMigrationModel.AditionalServices = aditionalEquipments;
            string[] selectedServices = (from ele in aditionalEquipments
                                         select ele.CodServSisact).ToArray();
            oMigrationModel.SelectedServices = selectedServices;
            PlanServiceResponse objServicesResponse;
            PlanServiceRequest objServicesRequest = new PlanServiceRequest
            {
                idplan = idPlan,
                strTipoProducto = strProductType,
                audit = App_Code.Common.CreateAuditRequest<AuditRequest>(strIdSession)
            };
            try
            {
                objServicesResponse = Claro.Web.Logging.ExecuteMethod<PlanServiceResponse>(() =>
                {
                    return new FixedTransacServiceClient().HfcGetServicesByPlan(objServicesRequest);
                });
                oMigrationModel.ServicesByPlan = objServicesResponse.listServicio;
                oMigrationModel.ServicesByPlan = (from ele in oMigrationModel.ServicesByPlan
                                                  group ele by ele.CodServSisact
                                                      into groups
                                                      select groups.OrderBy(x => x.CodServiceType).First()).ToList();
                var myClause = new string[] { oTransacServ.Constants.PresentationLayer.NumeracionOCHO };
                oMigrationModel.ServicesByPlan = (from ele in oMigrationModel.ServicesByPlan
                                                  where myClause.Contains(ele.CodGroupServ)
                                                  select ele).ToList();

                var valueIgv = GetCommonConsultIgv(strIdSession).igvD + 1;
                foreach (var item in oMigrationModel.ServicesByPlan)
                {
                    if (!string.IsNullOrEmpty(item.CF))
                    {
                        item.CfWithIgv = string.Format("{0:0.00}", Double.Parse(item.CF) * valueIgv);
                    }
                }
            }
            catch (Exception ex)
            {
                objServicesResponse = null;
                Claro.Web.Logging.Error(strIdSession, objServicesRequest.audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }
            Claro.Web.Logging.Info(strIdSession, objServicesRequest.audit.transaction, JsonConvert.SerializeObject(oMigrationModel));
            Claro.Web.Logging.Info("HFC_ListServicesByPlan", "HFCPlanMigration", "Fin _ListServicesByPlan");
            return PartialView("~/Areas/Transactions/Views/PlanMigration/HFCListServicesByPlan.cshtml", oMigrationModel);

        }
        public ActionResult ListServicesByPlanWithEquipment(string strIdSession, string idPlan, string idServicio, string strProductType)
        {
            Claro.Web.Logging.Info("HFCListServicesByPlanWithEquipment", "HFCPlanMigration", "Entro a ListServicesByPlanWithEquipment");
            Models.HFC.MigrationModel oHfcMigrationModel = new Models.HFC.MigrationModel();

            FixedTransacService.PlanServiceResponse objServicesResponse;
            FixedTransacService.PlanServiceRequest objServicesRequest = new FixedTransacService.PlanServiceRequest
            {
                idplan = idPlan,
                strTipoProducto = strProductType,
                audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession)
            };
            try
            {
                objServicesResponse = Claro.Web.Logging.ExecuteMethod<FixedTransacService.PlanServiceResponse>(() => { return new FixedTransacService.FixedTransacServiceClient().HfcGetServicesByPlan(objServicesRequest); });
                if (objServicesResponse.listServicio != null)
                {
                    oHfcMigrationModel.ServicesByPlan = objServicesResponse.listServicio;
                    oHfcMigrationModel.IdServicio = idServicio;
                }
            }
            catch (Exception ex)
            {
                objServicesResponse = null;
                Claro.Web.Logging.Error(strIdSession, objServicesRequest.audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }

            Claro.Web.Logging.Info(strIdSession, objServicesRequest.audit.transaction, JsonConvert.SerializeObject(objServicesResponse));
            Claro.Web.Logging.Info("HFCListServicesByPlanWithEquipment", "HFCPlanMigration", "Fin ListServicesByPlanWithEquipment");
            return PartialView("~/Areas/Transactions/Views/PlanMigration/HFCEquipmentByService.cshtml", oHfcMigrationModel);
        }
        /// <summary>
        /// Funcion que guarda planes migrados.
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="List<ServiceByPlan>"></param>
        /// <param name="tipification"></param>
        /// <param name="paramSaved"></param>
        /// <param name="listParamConstancyPDF"></param>
        /// <returns>JsonResult</returns>
        /// <remarks>SaveMigratedPlan</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>20/05/2019.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>PROY-140245-Oferta Colaborador</Mot></item></list>
        public JsonResult SaveMigratedPlan(string strIdSession, List<ServiceByPlan> ServicesList, TypificationItem tipification, ParametersSaved paramSaved, List<string> listParamConstancyPDF)
        {
          
            List<ServiceByPlan> lstServiceListTypification = new List<ServiceByPlan>();
            string message = string.Empty;
            var valueIgv = GetCommonConsultIgv(strIdSession).igvD + Claro.Constants.NumberOne;
            foreach (var item in ServicesList)
            {
                ServiceByPlan objPivot = new ServiceByPlan()
                {
                    CantEquipment = item.CantEquipment,
                    CF =  !string.IsNullOrEmpty(item.CF) ? string.Format("{0:0.00}", Math.Round(Double.Parse(item.CF) * valueIgv, Claro.Constants.NumberTwo)) : "0.00",
                    CfWithIgv = item.CfWithIgv,
                    CodeExternal = item.CodeExternal,
                    CodGroupServ = item.CodGroupServ,
                    CodPlanSisact = item.CodPlanSisact,
                    CodPrincipalGroup = item.CodPrincipalGroup,
                    CodServiceType = item.CodServiceType,
                    CodServSisact = item.CodServSisact,
                    Codtipequ = item.Codtipequ,
                    DesCodeExternal = item.DesCodeExternal,
                    DesPlanSisact = item.DesPlanSisact,
                    DesServSisact = item.DesServSisact,
                    Dscequ = item.Dscequ,
                    Equipment = item.Equipment,
                    ExtensionData = item.ExtensionData,
                    GroupServ = item.GroupServ,
                    IDEquipment = item.IDEquipment,
                    IdLineQuantity = item.IdLineQuantity,
                    Quantity = item.Quantity,
                    ServiceType = item.ServiceType,
                    ServvUserCrea = item.ServvUserCrea,
                    Sncode = item.Sncode,
                    Solution = item.Solution,
                    Spcode = item.Spcode,
                    Tipequ = item.Tipequ,
                    Tmcode = item.Tmcode
                };

                lstServiceListTypification.Add(objPivot);
            }
            paramSaved.ConstanceXml = CreateDictionaryConstancyXML(strIdSession, listParamConstancyPDF, lstServiceListTypification);

            AuditRequestFixed objAuditRequest = App_Code.Common.CreateAuditRequest<AuditRequestFixed>(strIdSession);
            string transaction = objAuditRequest.transaction;
            MigratedPlanResponseLte oMigratedPlanResponseLte = new MigratedPlanResponseLte();
            GenerateSOTResponseFixed oUpdateEtaSot = new GenerateSOTResponseFixed();

            Claro.Web.Logging.Info(strIdSession, objAuditRequest.transaction, string.Format("SaveMigratedPlan - {0} - ServicesList:{1}", paramSaved.StrIdContract, JsonConvert.SerializeObject(ServicesList)));
            Claro.Web.Logging.Info(strIdSession, objAuditRequest.transaction, string.Format("SaveMigratedPlan - {0} - tipification:{1}", paramSaved.StrIdContract, JsonConvert.SerializeObject(tipification)));
            Claro.Web.Logging.Info(strIdSession, objAuditRequest.transaction, string.Format("SaveMigratedPlan - {0} - paramSaved:{1}", paramSaved.StrIdContract, JsonConvert.SerializeObject(paramSaved)));
            

            RetentionCancelServicesResponse objDiscountBondResponse = new RetentionCancelServicesResponse();

            try
            {
                string strmsisdn = ConfigurationManager.AppSettings("gConstKeyCustomerInteract").ToString() + paramSaved.StrIdCustomer;
                var strcontactObjId = String.Empty;
                try
                {
                    strcontactObjId = new CommonServicesController().GetOBJID(strIdSession, strmsisdn);
                }
                catch (Exception e)
                {
                    Claro.Web.Logging.Error(objAuditRequest.Session, objAuditRequest.transaction, e.Message);
                }
                //ETA SEL
                if (Functions.CheckInt(paramSaved.StrHdnRequestActId) > Constant.numeroCero)
                {
                    if (paramSaved.StrFProgramacion != null || paramSaved.StrFProgramacion != string.Empty)
                    {
                        if (paramSaved.StrFranjaHorariaETA != null)
                        {
                            if (paramSaved.StrFranjaHorariaETA != Constant.strMenosUno)
                            {
                                try
                                {
                                    FixedTransacService.InsertETASelectionResponse objInsertETASelectionResponse = null;
                                    FixedTransacService.InsertETASelectionRequest objInsertETASelectionRequest = null;
                                    objInsertETASelectionResponse = new FixedTransacService.InsertETASelectionResponse();
                                    objInsertETASelectionRequest = new FixedTransacService.InsertETASelectionRequest()
                                    {
                                        audit = objAuditRequest,
                                        vidconsulta = Functions.CheckInt(paramSaved.StrHdnRequestActId),
                                        vidInteraccion = Constant.Notes_IncomingCallsPrepaid.SubscriberStatusDefault.Substring(0, 9 - paramSaved.StrHdnRequestActId.Trim().Length) + paramSaved.StrHdnRequestActId.Trim(),
                                        vfechaCompromiso = DateTime.Parse(paramSaved.StrFProgramacion),
                                        vfranja = paramSaved.StrFranjaHorariaETA.Split('+')[0],
                                        vid_bucket = paramSaved.StrFranjaHorariaETA.Split('+')[1]//model.FranjaHorariaETA.Split('+')[1]
                                    };
                                    objInsertETASelectionResponse = Claro.Web.Logging.ExecuteMethod<FixedTransacService.InsertETASelectionResponse>(() => { return new FixedTransacServiceClient().GetInsertETASelection(objInsertETASelectionRequest); });
                                }
                                catch (Exception ex)
                                {
                                    Claro.Web.Logging.Error(objAuditRequest.Session, objAuditRequest.transaction, ex.Message);
                                }
                            }
                        }
                    }
                }


                ClientParameters oClientParameters = new ClientParameters()
            {
                strflagReg = Claro.SIACU.Transac.Service.Constants.PresentationLayer.NumeracionUNO,
                straccount = string.Empty,
                strmsisdn = strmsisdn,
                strcontactObjId = strcontactObjId
            };

                MainParameters oMainParameters = new MainParameters()
                {
                    strType = tipification.TIPO,
                    strClass = tipification.CLASE,
                    strSubClass = tipification.SUBCLASE,
                    strContactMethod = ConfigurationManager.AppSettings("MetodoContactoTelefonoDefault"),
                    strInterType = ConfigurationManager.AppSettings("AtencionDefault"),
                    strAgent = paramSaved.StrLogin,
                    strUserProcess = ConfigurationManager.AppSettings("USRProcesoSU"),
                    strMadeInOne = Claro.SIACU.Transac.Service.Constants.PresentationLayer.NumeracionCERO,
                    strNotes = paramSaved.StrNotes,
                    strFlagCase = Claro.SIACU.Transac.Service.Constants.PresentationLayer.NumeracionCERO,
                    strResult = ConfigurationManager.AppSettings("Ninguno"),
                    strServAfect = string.Empty,
                    strInconven = string.Empty,
                    strServAfectCode = string.Empty,
                    strInconvenCode = string.Empty,
                    strCoId = paramSaved.StrIdContract,
                    strCodPlan = paramSaved.StrPlanoInst,
                    strValueOne = string.Empty,
                    trValueTwo = string.Empty
                };

                PlusParameters PlusParameters = new PlusParameters()
                {
                    strInter1 = paramSaved.StrBillingCycle,
                    strInter3 = paramSaved.StrActivationDate,
                    strInter4 = paramSaved.StrTermContract,
                    strInter5 = paramSaved.StrStateLine,
                    strInter6 = paramSaved.StrExpirationDate,
                    strInter7 = paramSaved.StrOfficeAddress,
                    strInter15 = paramSaved.StrCacDac,
                    strInter16 = paramSaved.StrLegalAddress,
                    strInter17 = paramSaved.StrLegalDistrict,
                    strInter18 = paramSaved.StrLegalCountry,
                    strInter19 = paramSaved.StrLegalProvince,
                    strInter20 = paramSaved.StrPlaneCodeInstallation,
                    strInter21 = listParamConstancyPDF[11], //listParamConstancyPDF[12], inc 28-05
                    strInter29 = string.Empty, //paramSaved.StrContractID,
                    strInter30 = paramSaved.StrNotes,
                    strAmountUnit = paramSaved.StrLegalUrbanization,
                    strBirthday = DateTime.Now.ToString(oTransacServ.Constants.PresentationLayer.dateDefaultFormat),
                    strClaroLdn1 = paramSaved.StrDocumentNumber,
                    strFirstName = paramSaved.StrFullName,
                    strNameLegalRep = paramSaved.StrLegalAgent,
                    strOldClaroLdn2 = listParamConstancyPDF[13],// paramSaved.StrPlan, inc 28-05
                    strOldClaroLdn3 = paramSaved.StrPresuscritoStatus,
                    strOldClaroLdn4 = paramSaved.StrNoLetter,
                    strOldClaroLocal1 = paramSaved.StrDdlOperator,
                    strOldClaroLocal2 = paramSaved.StrPublishFinalStatus,
                    strOldClaroLocal3 = paramSaved.StrReintegro,
                    strOldClaroLocal4 = paramSaved.StrMontoFideliza,
                    strOldClaroLocal5 = paramSaved.StrTotalPenalty,
                    strOldClaroLocal6 = paramSaved.StrFidelizaFinalStatus,
                    strOldFirstName = paramSaved.StrOCCFinalStatus,
                    strOtherPhone = paramSaved.StrDocumentType,
                    strPhoneLegalRep = paramSaved.StrValidaETAStatus,
                    strReferencePhone = paramSaved.StrTotalPenalty,
                    strReason = paramSaved.StrCustomerContact,
                    strRegistrationReason = paramSaved.StrContractID,
                    strBasket = paramSaved.StrPlan,
                    strExpireDate = DateTime.Now.ToString(oTransacServ.Constants.PresentationLayer.dateDefaultFormat),
                    strCity = DateTime.Now.ToShortDateString(),
                    strOccupation = paramSaved.StrHayServicioCoreTelefono,
                    strPosition = paramSaved.StrFProgramacion,
                    strTypeDocument = paramSaved.StrCustomerType,
                    strZipCode = paramSaved.StrCargoFijoTotalPlanCIGV,
                    //dstino
                    strAdjustmentReason = listParamConstancyPDF[25],
                    //origen
                    strOperationType = listParamConstancyPDF[26]
                };

                string strFranja = String.Empty;
                string strIdBucket = String.Empty;
                string strSubTipoTrabajo = String.Empty;
                string strFeProg = String.Empty;

                if (paramSaved.StrHdnValidaEta == Constant.strCero)
                {
                    strFranja = String.Empty;
                    strIdBucket = String.Empty;
                }
                else if (paramSaved.StrFranjaHorariaETA == Constant.strMenosUno || paramSaved.StrFranjaHorariaETA == null)
                {
                    strFranja = String.Empty;
                    strIdBucket = String.Empty;
                }
                else
                {
                    strFranja = paramSaved.StrFranjaHorariaETA.Split('+')[0];
                    strIdBucket = paramSaved.StrFranjaHorariaETA.Split('+')[1];
                }
                if (paramSaved.StrFProgramacion == string.Empty || paramSaved.StrFProgramacion == null)
                {
                    strFeProg = string.Empty;
                }
                else
                {
                    strFeProg = paramSaved.StrFProgramacion;
                }

                strSubTipoTrabajo = paramSaved.StrSubTypeWork.Split('|')[0];



                EtaSelection EtaSelection = new EtaSelection()
                {
                    strFechaCompromiso = paramSaved.StrFProgramacion,
                    strFranja = strFranja,
                    strIdBucket = strIdBucket,
                    strIdConsulta = paramSaved.StrHdnRequestActId
                };

                if (paramSaved.StrCodMoTot == null || paramSaved.StrCodMoTot == string.Empty)
                {
                    paramSaved.StrCodMoTot = KEY.AppSettings("strHFCPlanMigrationCodMotDefault");
                }

                paramSaved.StrNotes = paramSaved.StrNotes != null ? paramSaved.StrNotes.Replace('|', '-') : string.Empty;

                SotParameters SotParameters = new SotParameters()
                {
                    strCoId = paramSaved.StrContractID,
                    strCustomerId = paramSaved.StrCustomerID,
                    strTransTipo = ConfigurationManager.AppSettings("gConstKeyTipoTranMPHFC"),
                    strFechaProgramada = CSTS.Functions.CheckDate(paramSaved.StrFProgramacion).ToShortDateString(),
                    strFranjaHoraria = paramSaved.StrFranjaHora == null ? CSTS.Functions.GetValueFromConfigFile("strDefectoHorario", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig")) : paramSaved.StrFranjaHora,
                    strCodEdif = paramSaved.StrCodMoTot,
                    strNumCarta = String.IsNullOrEmpty(paramSaved.StrNoLetter) ? oTransacServ.Constants.strCero : paramSaved.StrNoLetter,
                    //Verifica Observacion
                    strObservacion = string.Format("{0}|{1}|{2}", paramSaved.StrNotes, paramSaved.StrHdnRequestActId, paramSaved.strVisitTecAnotaciones),
                    strUsrRegistro = paramSaved.StrLogin,
                    strOperador = Convert.ToInt(paramSaved.StrDdlOperatorStatus).ToString(),
                    strPresuscrito = paramSaved.StrPresuscritoStatus,
                    strPublicar = paramSaved.StrPublishFinalStatus,
                    strTmCode = paramSaved.StrHdnListaFTMCode
                };

                Claro.Web.Logging.Info(strIdSession, objAuditRequest.transaction, string.Format("SaveMigratedPlan - {0} - oClientParameters:{1}", paramSaved.StrIdContract, JsonConvert.SerializeObject(oClientParameters)));
                Claro.Web.Logging.Info(strIdSession, objAuditRequest.transaction, string.Format("SaveMigratedPlan - {0} - oMainParameters:{1}", paramSaved.StrIdContract, JsonConvert.SerializeObject(oMainParameters)));
                Claro.Web.Logging.Info(strIdSession, objAuditRequest.transaction, string.Format("SaveMigratedPlan - {0} - PlusParameters:{1}", paramSaved.StrIdContract, JsonConvert.SerializeObject(PlusParameters)));
                Claro.Web.Logging.Info(strIdSession, objAuditRequest.transaction, string.Format("SaveMigratedPlan - {0} - EtaSelection:{1}", paramSaved.StrIdContract, JsonConvert.SerializeObject(EtaSelection)));
                Claro.Web.Logging.Info(strIdSession, objAuditRequest.transaction, string.Format("SaveMigratedPlan - {0} - SotParameters:{1}", paramSaved.StrIdContract, JsonConvert.SerializeObject(SotParameters)));

                EtaParameters EtaParameters = new EtaParameters()
                {
                    strDniTecnico = "",
                    strFechaCreacion = strFeProg,
                    strIdBucket = strIdBucket,
                    strIdFranja = strFranja,//strIdFranja,
                    strIdPoblado = "",
                    strIpCreacion = "",
                    strPlano = paramSaved.StrPlanoInst,
                    strSubtipo = strSubTipoTrabajo,
                    strUsrCreacion = paramSaved.StrLogin
                };
                List<RegServiciosType> lstServices = (from ele in ServicesList
                                                      select new RegServiciosType
                                                      {
                                                          strCoId = Claro.SIACU.Transac.Service.Constants.PresentationLayer.NumeracionCERO,
                                                          strSnCode = ele.Sncode,
                                                          strSpCode = ele.Spcode,
                                                          strProfileId = Claro.SIACU.Transac.Service.Constants.PresentationLayer.NumeracionCERO,
                                                          CamposAdicionalesDescuento = new CamposAdicionalesDcto()
                                                          {
                                                              strTipoCostoServicioAvanzado = ConfigurationManager.AppSettings("strConstTipoCostoServicioCrearContraroHFC"),
                                                              strCostoServicioAvanzado = ele.CF,
                                                              strPeriodoCostoServicioAvanzado = ConfigurationManager.AppSettings("strConstPeriodoServicioCrearContraroLTE")
                                                          },
                                                          CamposAdicionalesCargo = new CamposAdicionalesCargo()
                                                          {
                                                              strTipoCostoServicio = ConfigurationManager.AppSettings("strConstTipoCostoServicioCrearContraroHFC"),
                                                              strCostoServicio = ele.CF,
                                                              strPeriodoCostoServicio = ConfigurationManager.AppSettings("strConstPeriodoServicioCrearContraroLTE")
                                                          }
                                                      }).ToList();


                List<Campo> Campos = new List<Campo>() { new Campo { strIndice = "28", strTipo = "1", strValor = ConfigurationManager.AppSettings("gPrefijoCodigoPlanLTE").ToString().Trim() + paramSaved.StrHdnCodigoPlan } };
                InformacionContrato InformacionContrato = new InformacionContrato()
                {
                    Campos = Campos
                };


                ContractElement oContractElement = new ContractElement()
                {
                    strPlanTarifario = paramSaved.StrHdnListaFTMCode,
                    strIdSubmercado = ConfigurationManager.AppSettings("strConstIDSubmercadoCrearContratoHFC"),
                    strIdMercado = ConfigurationManager.AppSettings("strConstIDMercadoCrearContratoHFC"),
                    strRed = ConfigurationManager.AppSettings("strConstRedCrearContratoHFC"),
                    strEstadoUmbral = ConfigurationManager.AppSettings("strConstEstadoUmbralCrearContratoLTE"),
                    strCantidadUmbral = ConfigurationManager.AppSettings("strConstCantidadUmbralCrearContratoLTE"),
                    strArchivoLlamadas = ConfigurationManager.AppSettings("strConstArchivoLlamadasCrearContratoLTE"),
                    ListServices = lstServices,
                    ActualizacionContrato = new ActualizacionContrato() { strRazon = ConfigurationManager.AppSettings("lngConstRazonActualizacionContratoHFC") },
                    InformacionContrato = InformacionContrato
                };

                Contract Contract = new Contract()
                {
                    strIpAplicacion = Common.GetApplicationIp(),
                    strNombreAplicacion = ConfigurationManager.AppSettings("gConstTipoHFCHFC"),
                    strTipoPostpago = ConfigurationManager.AppSettings("strConstTipoPostpagoCrearContratoHFC"),
                    ContractList = new List<ContractElement>() { oContractElement }
                };

                ActualizarTipificacion ActualizarTipificacion = new ActualizarTipificacion()
                {
                    Orden = Claro.SIACU.Transac.Service.Constants.strLetraI
                };

                bool FlagContingencia = System.Convert.ToBoolean(ConfigurationManager.AppSettings("FlagContingenciaHFC").ToString());
                bool FlagCrearPlantilla = System.Convert.ToBoolean(ConfigurationManager.AppSettings("FlagCrearPlantillaHFC").ToString());
                AuditRegister AuditRegister = new AuditRegister()
                {
                    strIdTransaccion = transaction,//ConfigurationManager.AppSettings("strCodAuditoriaMPLTE"),
                    strServicio = ConfigurationManager.AppSettings("gConstEvtServicio"),
                    strIpCliente = Common.GetClientIP(),
                    strNombreCliente = paramSaved.StrFullName,
                    strIpServidor = Common.GetApplicationIp(),
                    strNombreServidor = Common.GetApplicationName(),
                    strMonto = Claro.SIACU.Transac.Service.Constants.PresentationLayer.NumeracionCERO,
                    strCuentaUsuario = paramSaved.StrLogin,
                    strTelefono = paramSaved.StrCustomerID,
                    strTexto = String.Empty//Functions.GetValueFromConfigFile("strMsgAuditExitoMP", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"))
                };

                List<Coser> ListCoser = (from ele in ServicesList
                                         select new Coser
                                         {
                                             strCargoFijo = ele.CF,
                                             strSnCode = ele.Sncode,
                                             strSpCode = ele.Spcode,
                                             strTipoServicio = ele.ServiceType
                                         }).ToList();

                bool FlagValidaEta = System.Convert.ToBoolean(ConfigurationManager.AppSettings("FlagValidaEtaHFC").ToString());
                string ParametrosConstancia = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + paramSaved.ConstanceXml;
                string DestinatarioCorreo = paramSaved.StrEmail;
                string Notes = paramSaved.StrNotes;
                MigratedPlanRequestLte oMigratedPlanRequestLte = new MigratedPlanRequestLte()
                {
                    TransactionId = transaction,
                    ServicesList = lstServiceListTypification,
                    Tipification = tipification,
                    ClientParameters = oClientParameters,
                    MainParameters = oMainParameters,
                    PlusParameters = PlusParameters,
                    EtaSelection = EtaSelection,
                    SotParameters = SotParameters,
                    EtaParameters = EtaParameters,
                    Contract = Contract,
                    ActualizarTipificacion = ActualizarTipificacion,
                    FlagContingencia = FlagContingencia,
                    FlagCrearPlantilla = FlagCrearPlantilla,
                    AuditRegister = AuditRegister,
                    ListCoser = ListCoser,
                    FlagValidaEta = FlagValidaEta,
                    ParametrosConstancia = ParametrosConstancia,
                    DestinatarioCorreo = DestinatarioCorreo,
                    Notes = Notes,
                    strTipoProducto = paramSaved.TipoProducto,
                    audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession)
                };

                Claro.Web.Logging.Info(strIdSession, objAuditRequest.transaction, string.Format("SaveMigratedPlan - {0} - oMigratedPlanRequestLte:{1}", paramSaved.StrIdContract, JsonConvert.SerializeObject(oMigratedPlanRequestLte)));

                oMigratedPlanResponseLte = Claro.Web.Logging.ExecuteMethod<MigratedPlanResponseLte>(() =>
                {
                    return new FixedTransacServiceClient().ExecutePlanMigrationHfc(oMigratedPlanRequestLte);
                });

                Claro.Web.Logging.Info(strIdSession, objAuditRequest.transaction, string.Format("SaveMigratedPlan - {0} - oMigratedPlanResponseLte:{1}", paramSaved.StrIdContract, JsonConvert.SerializeObject(oMigratedPlanResponseLte)));



               
                //INI PROY-32650
                
                
                    Claro.Web.Logging.Info(strIdSession, objAuditRequest.transaction, "entro al flag strFlag32650");
                if (!string.IsNullOrEmpty(oMigratedPlanResponseLte.result.InteractionCode)) // se valida si ejecuto el cambio de plan
                {
                    Claro.Web.Logging.Info(strIdSession, objAuditRequest.transaction, "entro al oMigratedPlanResponseLte.result.InteractionCode");
                    //Primero se consulta si existe descuento vigente de cargo fijo 
                    RetentionCancelServicesRequest objCurrentDiscountRequest = new RetentionCancelServicesRequest();
                    objCurrentDiscountRequest.audit = objAuditRequest;
                    objCurrentDiscountRequest.CodId = Convert.ToInt(paramSaved.StrIdContract);
                    RetentionCancelServicesResponse objCurrentDiscountResponse = new RetentionCancelServicesResponse();
                    Claro.Web.Logging.Info(strIdSession, objAuditRequest.transaction, string.Format("GetCurrentDiscountFixedCharge - {0} - objCurrentDiscountRequest:{1}", paramSaved.StrIdContract, JsonConvert.SerializeObject(objCurrentDiscountRequest)));
                    objCurrentDiscountResponse = Claro.Web.Logging.ExecuteMethod<RetentionCancelServicesResponse>(() =>
                    {
                        return new FixedTransacServiceClient().GetCurrentDiscountFixedCharge(objCurrentDiscountRequest);
                    });
                    Claro.Web.Logging.Info(strIdSession, objAuditRequest.transaction, "objCurrentDiscountResponse.CurrentDiscounts.Count" + objCurrentDiscountResponse.CurrentDiscounts.Count);

                    if (objCurrentDiscountResponse.CurrentDiscounts.Count > 0)
                    {
                        if (Convert.ToDouble(objCurrentDiscountResponse.CurrentDiscounts[0].TOTAL_DESCUENTO) > 0
                           && DateTime.Parse(objCurrentDiscountResponse.CurrentDiscounts[0].FEC_FIN) > DateTime.Today)
                        {
                            Claro.Web.Logging.Info(strIdSession, objAuditRequest.transaction, "entro al segundo if");
                            if (objCurrentDiscountResponse.CurrentDiscounts[0].FLAG == "0")
                            {
                                //si existe se registra el bono de cambio de plan   paramSaved.StrHdnCodigoPlan
                                RetentionCancelServicesRequest objDiscountBondRequest = new RetentionCancelServicesRequest();
                                objDiscountBondRequest.audit = objAuditRequest;
                                objDiscountBondRequest.CustumerId = Convert.ToInt(paramSaved.StrIdCustomer);
                                objDiscountBondRequest.CodId = Convert.ToInt(paramSaved.StrIdContract);
                                objDiscountBondRequest.SnCode = Claro.SIACU.Transac.Service.Constants.strCero;
                                objDiscountBondRequest.TmCodeCom = Convert.ToInt(paramSaved.StrHdnCodigoPlan);// CODIGO DE NUEVO PLAN COMERCIAL                     
                                Claro.Web.Logging.Info(strIdSession, objAuditRequest.transaction, string.Format("InsertDiscountBondExchangePlan - {0} ", JsonConvert.SerializeObject(objDiscountBondRequest)));
                                objDiscountBondResponse = Claro.Web.Logging.ExecuteMethod<RetentionCancelServicesResponse>(() =>
                                {
                                    return new FixedTransacServiceClient().InsertDiscountBondExchangePlan(objDiscountBondRequest);
                                });

                            }
                            else if (objCurrentDiscountResponse.CurrentDiscounts[0].FLAG == "1")
                            {
                                DateTime fecha_inicio = DateTime.Parse(string.Format("{0:dd/MM/yyyy HH:mm:ss}",objCurrentDiscountResponse.CurrentDiscounts[0].FEC_INICIO));//, new System.Globalization.CultureInfo("es-ES"));
                                DateTime fecha_fin = DateTime.Parse(string.Format("{0:dd/MM/yyyy HH:mm:ss}", objCurrentDiscountResponse.CurrentDiscounts[0].FEC_FIN));//, new System.Globalization.CultureInfo("es-ES"));
                                int nDias = ((fecha_fin.Year - fecha_inicio.Year) * 12) + fecha_fin.Month - fecha_inicio.Month;
                                string msgConfig = ConfigurationManager.AppSettings("strMsjCambioPlanHFC");
                                if (msgConfig != "")
                                    message = msgConfig
                                        .Replace("#Months#", nDias.ToString())
                                        .Replace("#Date#", fecha_inicio.ToString("dd/MM/yyyy"));
                            }
               
                        }
                    }

                }
                

                //FIN PROY-32650
                Claro.Web.Logging.Info(strIdSession, objAuditRequest.transaction, "termino el proceso de savemigration" + " llave : " + message);
                if (oMigratedPlanResponseLte.result.result == String.Empty)
                {
                    oMigratedPlanResponseLte.result.result = Functions.GetValueFromConfigFile("strTextoErrorSiacu001", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                }

                if (oMigratedPlanResponseLte.result.ConstancyRoute == null || oMigratedPlanResponseLte.result.ConstancyRoute == string.Empty)
                {

                    Claro.Web.Logging.Info(objAuditRequest.Session, objAuditRequest.transaction, "La ruta de la constancia  para cambio de plan HFC es : Nula o vacia");
                }
            }
            catch (Exception ex)
                {
                Claro.Web.Logging.Error(objAuditRequest.Session, objAuditRequest.transaction, ex.Message);
            }
            return Json(new { data = oMigratedPlanResponseLte.result, CurrentDiscounts = new { Message = message }});
                }

        public FixedTransacService.GenerateSOTResponseFixed registrarEtaSot(string strIdSession, ParametersSaved objGetRecordTransactionRequest)
        {
            FixedTransacService.GenerateSOTRequestFixed objRequestGenerateSOT = new FixedTransacService.GenerateSOTRequestFixed();
            FixedTransacService.GenerateSOTResponseFixed objResponseGenerateSOT = new FixedTransacService.GenerateSOTResponseFixed();
            FixedTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);

            objRequestGenerateSOT.vCoID = objGetRecordTransactionRequest.StrCodSot;
            objRequestGenerateSOT.idSubTypeWork = objGetRecordTransactionRequest.StrSubTypeWork.Split('|')[2];
            objRequestGenerateSOT.vFranja = objGetRecordTransactionRequest.StrFranjaHorariaETA.Split('+')[0];
            objRequestGenerateSOT.idBucket = objGetRecordTransactionRequest.StrFranjaHorariaETA.Split('+')[1];
            objRequestGenerateSOT.vFeProg = objGetRecordTransactionRequest.StrFProgramacion;
            try
            {
                Claro.Web.Logging.Info(strIdSession, audit.transaction, "IN registrarEtaSot - WFC - HFC ");
                objRequestGenerateSOT.audit = audit;
                objResponseGenerateSOT = Claro.Web.Logging.ExecuteMethod<FixedTransacService.GenerateSOTResponseFixed>(() => { return new FixedTransacServiceClient().registraEtaSot(objRequestGenerateSOT); });

                Claro.Web.Logging.Info(strIdSession, audit.transaction, string.Format("OUT registrarEtaSot  - WFC -  {0}", objResponseGenerateSOT.DescMessaTransfer));

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, audit.transaction, ex.Message);
            }

            return objResponseGenerateSOT;
            }

        public FixedTransacService.GenerateSOTResponseFixed updateEtaSot(string strIdSession, EtaParameters EtaParameters, string strSolot)
        {
            FixedTransacService.GenerateSOTRequestFixed objRequestGenerateSOT = new FixedTransacService.GenerateSOTRequestFixed();
            FixedTransacService.GenerateSOTResponseFixed objResponseGenerateSOT = new FixedTransacService.GenerateSOTResponseFixed();
            FixedTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);

            objRequestGenerateSOT.vCoID = strSolot;
            objRequestGenerateSOT.idSubTypeWork = EtaParameters.strSubtipo;
            objRequestGenerateSOT.vPlano = EtaParameters.strPlano;
            objRequestGenerateSOT.Ubigeo = EtaParameters.strIdPoblado;
            objRequestGenerateSOT.vFranja = EtaParameters.strIdFranja;
            objRequestGenerateSOT.idBucket = EtaParameters.strIdBucket;
            objRequestGenerateSOT.FechaProgramada = DateTime.Parse(EtaParameters.strFechaCreacion);
            try
            {
                Claro.Web.Logging.Info(strIdSession, audit.transaction, "IN updateEtaSot - WFC - HFC ");
                objRequestGenerateSOT.audit = audit;
                objResponseGenerateSOT = Claro.Web.Logging.ExecuteMethod<FixedTransacService.GenerateSOTResponseFixed>(() => { return new FixedTransacServiceClient().UpdateEta(objRequestGenerateSOT); });

                Claro.Web.Logging.Info(strIdSession, audit.transaction, string.Format("OUT updateEtaSot  - WFC -  {0}", objResponseGenerateSOT.DescMessaTransfer));

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, audit.transaction, ex.Message);
        }

            return objResponseGenerateSOT;
        }

        public JsonResult CoreInternetJson(string strIdSession, string idPlan, string strProductType)
        {
            Claro.Web.Logging.Info("HFCCoreInternetJson", "HFCPlanMigration", "Entro a CoreInternetJson");
            Models.LTE.LteMigrationModel oLteMigrationModel = new Models.LTE.LteMigrationModel();

            FixedTransacService.PlanServiceResponse objServicesResponse;
            FixedTransacService.PlanServiceRequest objServicesRequest = new FixedTransacService.PlanServiceRequest
            {
                idplan = idPlan,
                strTipoProducto = strProductType,
                audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession)
            };
            try
            {
                objServicesResponse = Claro.Web.Logging.ExecuteMethod<FixedTransacService.PlanServiceResponse>(() => { return new FixedTransacService.FixedTransacServiceClient().HfcGetServicesByPlan(objServicesRequest); });
                oLteMigrationModel.ServicesByPlan = objServicesResponse.listServicio;
                oLteMigrationModel.ServicesByPlan = (from ele in oLteMigrationModel.ServicesByPlan
                                                     group ele by ele.CodServSisact
                                                         into groups
                                                         select groups.OrderBy(x => x.CodServiceType).First()).ToList();

                var myClause = new string[] { "2", "7" };
                oLteMigrationModel.ServicesByPlan = (from ele in oLteMigrationModel.ServicesByPlan
                                                     where myClause.Contains(ele.CodGroupServ) & ele.CodServiceType.Equals("1")
                                                     select ele).ToList();
            }
            catch (Exception ex)
            {
                objServicesResponse = null;
                Claro.Web.Logging.Error(strIdSession, objServicesRequest.audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }
            Claro.Web.Logging.Info(strIdSession, objServicesRequest.audit.transaction, JsonConvert.SerializeObject(objServicesResponse));
            Claro.Web.Logging.Info("HFCCoreInternetJson", "HFCPlanMigration", "Fin CoreInternetJson");
            return Json(new { data = oLteMigrationModel.ServicesByPlan });
        }
        public JsonResult CorePhoneJson(string strIdSession, string idPlan, string strProductType)
        {
            Claro.Web.Logging.Info("HFCCorePhoneJson", "HFCPlanMigration", "Entro a CorePhoneJson");
            Models.HFC.MigrationModel oHfcMigrationModel = new Models.HFC.MigrationModel();

            FixedTransacService.PlanServiceResponse objServicesResponse;
            FixedTransacService.PlanServiceRequest objServicesRequest = new FixedTransacService.PlanServiceRequest
            {
                idplan = idPlan,
                strTipoProducto = strProductType,
                audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession)
            };
            try
            {
                objServicesResponse = Claro.Web.Logging.ExecuteMethod<FixedTransacService.PlanServiceResponse>(() => { return new FixedTransacService.FixedTransacServiceClient().HfcGetServicesByPlan(objServicesRequest); });
                oHfcMigrationModel.ServicesByPlan = objServicesResponse.listServicio;
                oHfcMigrationModel.ServicesByPlan = (from ele in oHfcMigrationModel.ServicesByPlan
                                                     group ele by ele.CodServSisact
                                                         into groups
                                                         select groups.OrderBy(x => x.CodServiceType).First()).ToList();

                var myClause = new string[] { "1", "6" };
                oHfcMigrationModel.ServicesByPlan = (from ele in oHfcMigrationModel.ServicesByPlan
                                                     where myClause.Contains(ele.CodGroupServ) & ele.CodServiceType.Equals("1")
                                                     select ele).ToList();
            }
            catch (Exception ex)
            {
                objServicesResponse = null;
                Claro.Web.Logging.Error(strIdSession, objServicesRequest.audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }
            Claro.Web.Logging.Info(strIdSession, objServicesRequest.audit.transaction, JsonConvert.SerializeObject(objServicesResponse));
            Claro.Web.Logging.Info("HFCCorePhoneJson", "HFCPlanMigration", "Fin CorePhoneJson");
            return Json(new { data = oHfcMigrationModel.ServicesByPlan });

        }
        public JsonResult CoreCableJson(string strIdSession, string idPlan, string strProductType)
        {
            Claro.Web.Logging.Info("HFCCoreCableJson", "HFCPlanMigration", "Entro a CoreCableJson");
            Models.HFC.MigrationModel oHfcMigrationModel = new Models.HFC.MigrationModel();

            FixedTransacService.PlanServiceResponse objServicesResponse;
            FixedTransacService.PlanServiceRequest objServicesRequest = new FixedTransacService.PlanServiceRequest
            {
                idplan = idPlan,
                strTipoProducto = strProductType,
                audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession)
            };
            try
            {
                objServicesResponse = Claro.Web.Logging.ExecuteMethod<FixedTransacService.PlanServiceResponse>(() => { return new FixedTransacService.FixedTransacServiceClient().HfcGetServicesByPlan(objServicesRequest); });
                oHfcMigrationModel.ServicesByPlan = objServicesResponse.listServicio;
                oHfcMigrationModel.ServicesByPlan = (from ele in oHfcMigrationModel.ServicesByPlan
                                                     group ele by ele.CodServSisact
                                                         into groups
                                                         select groups.OrderBy(x => x.CodServiceType).First()).ToList();

                var myClause = new string[] { "3", "5", "4" };
                oHfcMigrationModel.ServicesByPlan = (from ele in oHfcMigrationModel.ServicesByPlan
                                                     where myClause.Contains(ele.CodGroupServ) & ele.CodServiceType.Equals("1")
                                                     select ele).ToList();
            }
            catch (Exception ex)
            {
                objServicesResponse = null; Claro.Web.Logging.Error(strIdSession, objServicesRequest.audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }
            Claro.Web.Logging.Info(strIdSession, objServicesRequest.audit.transaction, JsonConvert.SerializeObject(objServicesResponse));
            Claro.Web.Logging.Info("HFCCoreCableJson", "HFCPlanMigration", "Fin CoreCableJson");
            return Json(new { data = oHfcMigrationModel.ServicesByPlan });

        }
        public JsonResult GetServicesByCurrentPlan(string strIdSession, string strIdContract)
        {
            Claro.Web.Logging.Info("HFCGetServicesByCurrentPlan", "HFCPlanMigration", "Entro a GetServicesByCurrentPlan");
            MigrationModel oHfcMigrationModel = new MigrationModel();

            ServicesByCurrentPlanRequest objServicesByCurrentPlanRequest = new ServicesByCurrentPlanRequest
            {
                ContractId = strIdContract,
                audit = App_Code.Common.CreateAuditRequest<AuditRequest>(strIdSession)
            };
            ServicesByCurrentPlanResponse objServicesByCurrentPlanResponse;

            try
            {
                objServicesByCurrentPlanResponse = Claro.Web.Logging.ExecuteMethod<ServicesByCurrentPlanResponse>(() =>
{
    return new FixedTransacServiceClient().GetServicesByCurrentPlan(objServicesByCurrentPlanRequest);
});
                oHfcMigrationModel.ServicesByCurrentPlan = objServicesByCurrentPlanResponse.ServicesByCurrentPlan;
                if (oHfcMigrationModel.ServicesByCurrentPlan != null)
                {
                    oHfcMigrationModel.ServicesByCurrentPlanCharges = CalculatePlanCharges.CalculateCharges(oHfcMigrationModel.ServicesByCurrentPlan);
                    var valueIgv = GetCommonConsultIgv(strIdSession).igvD + 1;
                    foreach (var item in oHfcMigrationModel.ServicesByCurrentPlan)
                    {
                        if (!string.IsNullOrEmpty(item.CargoFijo))
                        {
                            item.CargoFijoConIgv = string.Format("{0:0.00}", Double.Parse(item.CargoFijo) * valueIgv);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objServicesByCurrentPlanRequest.audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }
            Claro.Web.Logging.Info(strIdSession, objServicesByCurrentPlanRequest.audit.transaction, JsonConvert.SerializeObject(oHfcMigrationModel));
            Claro.Web.Logging.Info("HFCGetServicesByCurrentPlan", "HFCPlanMigration", "Fin GetServicesByCurrentPlan");
            return Json(new { data = oHfcMigrationModel });
        }
        public JsonResult ListEquipmentsByService(SelectedServiceConsolidate oConsolidate, string strProductType)
        {
            Claro.Web.Logging.Info("HFCListEquipmentsByService", "HFCPlanMigration", "Entro a ListEquipmentsByService"); // Temporal
            Models.HFC.MigrationModel oMigrationModel = new Models.HFC.MigrationModel();
            string[] arrHFCGroupCable = ConfigurationManager.AppSettings("strHFCGroupCable").ToString().Split(',');
            string[] arrHFCGroupInternet = ConfigurationManager.AppSettings("strHFCGroupInternet").ToString().Split(',');
            string[] arrHFCGroupTelephony = ConfigurationManager.AppSettings("strHFCGroupTelephony").ToString().Split(',');
            PlanServiceResponse objServicesResponse;
            PlanServiceRequest objServicesRequest = new PlanServiceRequest
            {
                idplan = oConsolidate.idPlan,
                strTipoProducto = strProductType,
                audit = App_Code.Common.CreateAuditRequest<AuditRequest>(oConsolidate.strIdSession)
            };
            try
            {
                objServicesResponse = Claro.Web.Logging.ExecuteMethod<PlanServiceResponse>(() => { return new FixedTransacServiceClient().HfcGetServicesByPlan(objServicesRequest); });
                if (objServicesResponse.listServicio != null)
                {
                    oMigrationModel.ServicesByPlan = objServicesResponse.listServicio;
                }
                var strSolution = oMigrationModel.ServicesByPlan[0].Solution;
                oMigrationModel.ServicesByPlan = (from ele in oMigrationModel.ServicesByPlan
                                                    where ele.Solution == strSolution
                                                    select ele).ToList();
                oMigrationModel.ServicesByPlan = (from ele in oMigrationModel.ServicesByPlan
                                                  where !String.IsNullOrEmpty(ele.Equipment) 
                                                  && ele.CodGroupServ != oTransacServ.Constants.PresentationLayer.NumeracionOCHO
                                                  && (arrHFCGroupInternet.Contains(ele.CodGroupServ) ||
                                                      arrHFCGroupTelephony.Contains(ele.CodGroupServ) ||
                                                      arrHFCGroupCable.Contains(ele.CodGroupServ)
                                                        ) &&
                                                        (oConsolidate.strServiceCoreInternet == ele.CodServSisact || oConsolidate.strServiceCoreCable == ele.CodServSisact || oConsolidate.strServiceCoreTelephony == ele.CodServSisact)
                      
                                                  select ele).ToList();
                oMigrationModel.ServicesByPlan = (from ele in oMigrationModel.ServicesByPlan
                    group ele by ele.Codtipequ
                    into groups
                    select groups.OrderBy(x => x.Codtipequ).First()).ToList();

            }
            catch (Exception ex)
            {
                objServicesResponse = null;
                Claro.Web.Logging.Error(oConsolidate.strIdSession, objServicesRequest.audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }
            Claro.Web.Logging.Info("HFCListEquipmentsByService", "HFCPlanMigration", "ServicesByPlan(Antiguo)=> " + JsonConvert.SerializeObject(oMigrationModel.ServicesByPlan));
            return Json(new { data = oMigrationModel.ServicesByPlan });
        }

        public JsonResult SendNewPlanServices(SelectedServiceGroupByPlan objServicesByPlan)
        {
            Claro.Web.Logging.Info("HFCSendNewPlanServices", "HFCPlanMigration", "Entro a SendNewPlanServices"); // 
            NewPlanServicesRequest oNewPlanServicesRequest = new NewPlanServicesRequest
{
    Services = objServicesByPlan.ServicesByPlan,
    audit = App_Code.Common.CreateAuditRequest<AuditRequest>(objServicesByPlan.strIdSession)
};
            NewPlanServicesResponse oNewPlanServicesResponse = new NewPlanServicesResponse();
            try
            {
                oNewPlanServicesResponse = Claro.Web.Logging.ExecuteMethod<NewPlanServicesResponse>(() =>
                {
                    return new FixedTransacServiceClient().SendNewPlanServices(oNewPlanServicesRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oNewPlanServicesRequest.audit.Session, oNewPlanServicesRequest.audit.transaction, ex.Message);
            }
            Claro.Web.Logging.Info(oNewPlanServicesRequest.audit.Session, oNewPlanServicesRequest.audit.transaction, JsonConvert.SerializeObject(oNewPlanServicesResponse));
            Claro.Web.Logging.Info("HFCSendNewPlanServices", "HFCPlanMigration", "Fin SendNewPlanServices");

            return Json(new { oNewPlanServicesResponse.result });
        }
        public JsonResult GetEquipmentByCurrentPlan(string strIdSession, string strIdContract)
        {
            Claro.Web.Logging.Info("HFCGetEquipmentByCurrentPlan", "HFCPlanMigration", "Entro a GetEquipmentByCurrentPlan"); // Temporal

            EquipmentsByCurrentPlanRequest oRequest = new EquipmentsByCurrentPlanRequest
            {
                strIdContract = strIdContract,
                audit = App_Code.Common.CreateAuditRequest<AuditRequest>(strIdSession)
            };
            EquipmentsByCurrentPlanResponse oResponse = new EquipmentsByCurrentPlanResponse();
            try
            {
                oResponse = Claro.Web.Logging.ExecuteMethod<EquipmentsByCurrentPlanResponse>(() =>
                {
                    return new FixedTransacServiceClient().GetEquipmentByCurrentPlan(oRequest);
                });
                Claro.Web.Logging.Info("HFCGetEquipmentByCurrentPlan", "HFCPlanMigration", "Fin GetEquipmentByCurrentPlan"); // 
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oRequest.audit.Session, oRequest.audit.transaction, ex.Message);
            }
            Claro.Web.Logging.Info(oRequest.audit.Session, oRequest.audit.transaction, JsonConvert.SerializeObject(oResponse));
            Claro.Web.Logging.Info("HFCGetEquipmentByCurrentPlan", "HFCPlanMigration", "Fin GetEquipmentByCurrentPlan");

            if (oResponse.Equipments == null)
            {
                Claro.Web.Logging.Info("HFCGetEquipmentByCurrentPlan", "HFCPlanMigration", " oResponse.Equipments==null");
                oResponse.Equipments = new List<EquipmentByCurrentPlan>();
            }



            return Json(new { data = oResponse.Equipments });
        }
        public JsonResult GetTechnicalVisitResult(string strIdSession, string strIdContract, string strCustomerId, string strTmCode, string strCodPlanSisact
            , List<ServiceByPlan> listEquipments, List<ServiceByPlan> listEquipmentsBase, List<ServiceByPlan> listEquipmentsAS, List<ServiceByPlan> listEquipmentsASR)
        {
            Claro.Web.Logging.Info("HFCGetTechnicalVisitResult", "HFCPlanMigration", "Entro a GetTechnicalVisitResult"); // Temporal    
            string strHFCGroupCable = ConfigurationManager.AppSettings("strHFCGroupCable").ToString();
            string strHFCGroupInternet = ConfigurationManager.AppSettings("strHFCGroupInternet").ToString();
            string strHFCGroupTelephony = ConfigurationManager.AppSettings("strHFCGroupTelephony").ToString();
          
            // =============== Transformacion ===============
            List<ServiceByPlan> ServicesList = new List<ServiceByPlan>();
            if (listEquipmentsAS == null) listEquipmentsAS = new List<ServiceByPlan>();
            if (listEquipmentsASR == null) listEquipmentsASR = new List<ServiceByPlan>();

            
            foreach (ServiceByPlan sbp in listEquipmentsBase)
            {
                ServicesList.Add(sbp);
            }
            foreach (ServiceByPlan sbp in listEquipmentsASR)
            {
                ServicesList.Add(sbp);
            }

            for (int i = 0; i < ServicesList.Count; i++)
            {
                if (strHFCGroupCable.Contains(ServicesList[i].CodGroupServ))
                {
                    ServicesList[i].ServiceType = Constant.CAMBIO_DE_PLAN.TipoServicio_Cable;
                }
                else if (strHFCGroupInternet.Contains(ServicesList[i].CodGroupServ))
                {
                    ServicesList[i].ServiceType = Constant.CAMBIO_DE_PLAN.TipoServicio_Internet;
                }
                else if (strHFCGroupTelephony.Contains(ServicesList[i].CodGroupServ))
                {
                    ServicesList[i].ServiceType = Constant.CAMBIO_DE_PLAN.TipoServicio_Telefonia;
                }
            }

            string fieldSeparator = oTransacServ.Constants.PresentationLayer.fieldSeparator;
            string trama = string.Empty;
            string aftertrama = string.Empty;

            foreach (var item in ServicesList)
            {
                if (Functions.CheckInt(item.CantEquipment) > 0)
                {
                    trama = trama + item.CodServSisact + fieldSeparator + item.Sncode + fieldSeparator + item.CodGroupServ + fieldSeparator + item.Codtipequ +
                                                                                                fieldSeparator + item.Tipequ
                                                                                                + fieldSeparator + item.IDEquipment + fieldSeparator + item.CantEquipment + fieldSeparator + item.ServiceType + ";";
                }
            }


            Claro.Web.Logging.Info("HFCGetTechnicalVisitResult", "HFCPlanMigration", "GetTechnicalVisitResult la trama que se envia es: " + trama.ToString());

            TechnicalVisitResultRequest oRequest = new TechnicalVisitResultRequest
            {
                strCoId = strIdContract,
                strCustomerId = strCustomerId,
                strCodPlanSisact = strCodPlanSisact,
                strTmCode = strTmCode,
                strTrama = trama,
                audit = App_Code.Common.CreateAuditRequest<AuditRequest>(strIdSession)
            };
            TechnicalVisitResultResponse oResponse = new TechnicalVisitResultResponse();
            try
            {
                var userDataString = JsonConvert.SerializeObject(oRequest);
                Claro.Web.Logging.Info("HFCGetTechnicalVisitResult", "HFCPlanMigration", "GetTechnicalVisitResult objeto enviado a sp: " + userDataString); // Temporal

                oResponse = Claro.Web.Logging.ExecuteMethod<TechnicalVisitResultResponse>(() =>
                {
                    return new FixedTransacServiceClient().GetTechnicalVisitResult(oRequest);
                });
                if (Convert.ToInt(oResponse.Result.Anerror) < oTransacServ.Constants.numeroCero)
                {
                    Claro.Web.Logging.Info("HFCGetTechnicalVisitResult", "HFCPlanMigration", "GetTechnicalVisitResult_AvError_valor: " + oResponse.Result.Averror);
                }
                Claro.Web.Logging.Info("HFCGetTechnicalVisitResult", "HFCPlanMigration", "Fin GetTechnicalVisitResult"); 
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oRequest.audit.Session, oRequest.audit.transaction, ex.Message);
            }
            Claro.Web.Logging.Info(oRequest.audit.Session, oRequest.audit.transaction, JsonConvert.SerializeObject(oResponse));
            return Json(new { data = oResponse.Result });
        }
        public string GetHourAgendaETA(string IdSession, string FranjaHorariaETA)
        {
            Claro.Web.Logging.Info("HFCGetHourAgendaETA", "HFCPlanMigration", "Entro a GetHourAgendaETA");
            string strHora = string.Empty;
            FixedTransacService.AuditRequest straudit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(IdSession);
            try
            {
                strHora = Functions.GetValueFromConfigFile("strDefectoHorario", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig"));
                var listaHorariosAux = Functions.GetListValuesXML("ListaFranjasHorariasETA", "", "HFCDatos.xml");
                foreach (var item in listaHorariosAux)
                {
                    if (FranjaHorariaETA.Split('+')[0] == item.Code)
                    {
                        strHora = item.Code2;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(IdSession, "GetHourAgendaETA", ex.Message);
            }
            Claro.Web.Logging.Info(IdSession, straudit.transaction, JsonConvert.SerializeObject(strHora));
            Claro.Web.Logging.Info("HFCGetHourAgendaETA", "HFCPlanMigration", "Fin GetHourAgendaETA");
            return strHora;
        }
        //        public JsonResult GetConstanceTemplateLabels(string strIdSession)
        //{
        //    Claro.Web.Logging.Info("HFCGetConstanceTemplateLabels", "HFCPlanMigration", "Entro a GetConstanceTemplateLabels");
        //    FixedTransacService.AuditRequest straudit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);
        //            List<string> Labels = CommonServicesController.GetXmlToString(Common.GetApplicationRoute() + "/DataTransac/HFCConstance.xml");
        //            Claro.Web.Logging.Info(strIdSession, straudit.transaction, JsonConvert.SerializeObject(Labels));
        //            return Json(new { data = Labels });
        //        }
        public ActionResult ChooseServicesByPlan(string strIdSession, string idPlan, string strProductType, string ServicesList, int intOneCoreCable, int intOneCoreInternet, int intOneCorePhone)
        {

            List<ServiceByPlan> aditionalServices = JsonConvert.DeserializeObject<List<ServiceByPlan>>(ServicesList);
            Claro.Web.Logging.Info("HFCChooseServicesByPlan", "HFCPlanMigration", "Entro a ChooseServicesByPlan"); // Temporal
            Models.HFC.MigrationModel oHfcMigrationModel = new Models.HFC.MigrationModel();
            string[] cables = ConfigurationManager.AppSettings("strHFCGroupCable").ToString().Split(',');
            string[] internets = ConfigurationManager.AppSettings("strHFCGroupInternet").ToString().Split(',');
            string[] telephonys = ConfigurationManager.AppSettings("strHFCGroupTelephony").ToString().Split(',');
            oHfcMigrationModel.ConfigurationData = new ConfigurationData();
            oHfcMigrationModel.ConfigurationData.cables = cables;
            oHfcMigrationModel.ConfigurationData.internets = internets;
            oHfcMigrationModel.ConfigurationData.telephonys = telephonys;
            oHfcMigrationModel.ConfigurationData.intOneCoreCable = intOneCoreCable;
            oHfcMigrationModel.ConfigurationData.intOneCoreInternet = intOneCoreInternet;
            oHfcMigrationModel.ConfigurationData.intOneCorePhone = intOneCorePhone;
            oHfcMigrationModel.AditionalServices = aditionalServices;
            string[] selectedServices = (from ele in aditionalServices
                                         select ele.CodServSisact).ToArray();
            oHfcMigrationModel.SelectedServices = selectedServices;
            FixedTransacService.PlanServiceResponse objServicesResponse;
            FixedTransacService.PlanServiceRequest objServicesRequest = new FixedTransacService.PlanServiceRequest
            {
                idplan = idPlan,
                strTipoProducto = strProductType,
                audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession)
            };
            try
            {
                objServicesResponse = Claro.Web.Logging.ExecuteMethod<FixedTransacService.PlanServiceResponse>(() => { return new FixedTransacService.FixedTransacServiceClient().HfcGetServicesByPlan(objServicesRequest); });
                if (objServicesResponse.listServicio != null)
                {
                    oHfcMigrationModel.ServicesByPlan = objServicesResponse.listServicio;
                    oHfcMigrationModel.ServicesByPlan = (from ele in oHfcMigrationModel.ServicesByPlan
                                                         group ele by ele.CodServSisact
                                                             into groups
                                                             select groups.OrderBy(x => x.CodServiceType).First()).ToList();

                    var valueIgv = GetCommonConsultIgv(strIdSession).igvD + 1;
                    foreach (var item in oHfcMigrationModel.ServicesByPlan)
                    {
                        if (!string.IsNullOrEmpty(item.CF))
                        {
                            item.CfWithIgv = string.Format("{0:0.00}", Double.Parse(item.CF) * valueIgv);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                objServicesResponse = null;
                Claro.Web.Logging.Error(strIdSession, objServicesRequest.audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }
            Claro.Web.Logging.Info(strIdSession, objServicesRequest.audit.transaction, JsonConvert.SerializeObject(objServicesResponse));
            Claro.Web.Logging.Info("HFCGetConstanceTemplateLabels", "HFCPlanMigration", "fin GetConstanceTemplateLabels"); // Temporal

            return PartialView("~/Areas/Transactions/Views/PlanMigration/HFCChooseServicesByPlan.cshtml", oHfcMigrationModel);
        }

        public ActionResult ChooseCoreServicesByPlan(string strIdSession, string idPlan, string strProductType, string ServicesList)
        {
            Claro.Web.Logging.Info("HFCChooseCoreServicesByPlan", "HFCPlanMigration", "Entro a ChooseCoreServicesByPlan");
            List<ServiceByPlan> coreServices = JsonConvert.DeserializeObject<List<ServiceByPlan>>(ServicesList);
            Claro.Web.Logging.Info("HFCChooseServicesByPlan", "HFCPlanMigration", "Entro a ChooseCoreServicesByPlan"); // Temporal

            Models.HFC.MigrationModel oHfcMigrationModel = new Models.HFC.MigrationModel();
            oHfcMigrationModel.ConfigurationData = new ConfigurationData();
            oHfcMigrationModel.ConfigurationData.strHFCGroupCable = ConfigurationManager.AppSettings("strHFCGroupCable").ToString();
            oHfcMigrationModel.ConfigurationData.strHFCGroupInternet = ConfigurationManager.AppSettings("strHFCGroupInternet").ToString();
            oHfcMigrationModel.ConfigurationData.strHFCGroupTelephony = ConfigurationManager.AppSettings("strHFCGroupTelephony").ToString();
            string[] cables = ConfigurationManager.AppSettings("strHFCGroupCable").ToString().Split(',');
            string[] internets = ConfigurationManager.AppSettings("strHFCGroupInternet").ToString().Split(',');
            string[] telephonys = ConfigurationManager.AppSettings("strHFCGroupTelephony").ToString().Split(',');
            oHfcMigrationModel.ConfigurationData.cables = cables;
            oHfcMigrationModel.ConfigurationData.internets = internets;
            oHfcMigrationModel.ConfigurationData.telephonys = telephonys;
            oHfcMigrationModel.CoreServices = coreServices;
            string[] selectedServices = (from ele in coreServices
                                         select ele.CodServSisact).ToArray();
            oHfcMigrationModel.SelectedServices = selectedServices;
            FixedTransacService.PlanServiceResponse objServicesResponse;
            FixedTransacService.PlanServiceRequest objServicesRequest = new FixedTransacService.PlanServiceRequest
            {
                idplan = idPlan,
                strTipoProducto = strProductType,
                audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession)
            };
            Claro.Web.Logging.Info("HFCChooseServicesByPlan", "HFCPlanMigration", "ChooseCoreServicesByPlan_request: " + JsonConvert.SerializeObject(objServicesRequest));
            try
            {
                objServicesResponse = Claro.Web.Logging.ExecuteMethod<FixedTransacService.PlanServiceResponse>(() => { return new FixedTransacService.FixedTransacServiceClient().HfcGetServicesByPlan(objServicesRequest); });
                if (objServicesResponse.listServicio != null)
                {
                    oHfcMigrationModel.ServicesByPlan = objServicesResponse.listServicio;
                    oHfcMigrationModel.ServicesByPlan = (from ele in oHfcMigrationModel.ServicesByPlan
                                                         group ele by ele.CodServSisact
                                                             into groups
                                                             select groups.OrderBy(x => x.CodServiceType).First()).ToList();

                    var valueIgv = GetCommonConsultIgv(strIdSession).igvD + 1;
                    foreach (var item in oHfcMigrationModel.ServicesByPlan)
                    {
                        if (!string.IsNullOrEmpty(item.CF))
                        {
                            item.CfWithIgv = string.Format("{0:0.00}", Double.Parse(item.CF) * valueIgv);
                        }
                    }

                }
                Claro.Web.Logging.Info("HFCChooseServicesByPlan", "HFCPlanMigration", "ChooseCoreServicesByPlan_objServicesResponse: " + JsonConvert.SerializeObject(objServicesResponse));

            }
            catch (Exception ex)
            {
                objServicesResponse = null;
                Claro.Web.Logging.Error(strIdSession, objServicesRequest.audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }
            Claro.Web.Logging.Info(strIdSession, objServicesRequest.audit.transaction, JsonConvert.SerializeObject(objServicesResponse));
            Claro.Web.Logging.Info("HFCChooseCoreServicesByPlan", "HFCPlanMigration", "fin ChooseCoreServicesByPlan"); // Temporal

            return PartialView("~/Areas/Transactions/Views/PlanMigration/HFCChooseCoreServicesByPlan.cshtml", oHfcMigrationModel);
        }
        public JsonResult GetServicesGroups(string strIdSession)
        {
            Claro.Web.Logging.Info("HFCGetServicesGroups", "HFCPlanMigration", "Entro a GetServicesGroups");
            FixedTransacService.AuditRequest straudit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);
            ConfigurationData configurationData = new ConfigurationData();
            try
            {
                configurationData.strHFCGroupCable = ConfigurationManager.AppSettings("strHFCGroupCable").ToString();
                configurationData.strHFCGroupInternet = ConfigurationManager.AppSettings("strHFCGroupInternet").ToString();
                configurationData.strHFCGroupTelephony = ConfigurationManager.AppSettings("strHFCGroupTelephony").ToString();
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error("HFC", "HFC_PlanMigration", ex.Message);
            }

            Claro.Web.Logging.Info(strIdSession, straudit.transaction, JsonConvert.SerializeObject(configurationData));
            Claro.Web.Logging.Info("HFCGetServicesGroups", "HFCPlanMigration", "fin GetServicesGroups");

            return Json(new { data = configurationData });
        }
        //public JsonResult CreateConstanceXmlString(Dictionary<string, string> dict)
        //{
        //    Claro.Web.Logging.Info("HFCCreateConstanceXmlString", "HFCPlanMigration", "Entro a CreateConstanceXmlString");
        //    string resultado = String.Empty;
        //    try
        //    {
        //    XsltArgumentList xslArg = new XsltArgumentList();
        //    foreach (var item in dict)
        //    {
        //        xslArg.AddParam(item.Key, "", item.Value);
        //    }
        //    StringBuilder xml = new StringBuilder();
        //    XmlWriter xmlWriter = XmlWriter.Create(xml);
        //    string filename = Common.GetApplicationRoute() + "/DataTransac/HFCConstance.xml";
        //    string stylesheet = Common.GetApplicationRoute() + "/DataTransac/HFCConstance.xslt";
        //    XPathDocument doc = new XPathDocument(filename);

        //    //XslTransform xslt = new XslTransform();
        //    XslCompiledTransform xslCT = new XslCompiledTransform();
        //    xslCT.Load(stylesheet);
        //    xslCT.Transform(doc, xslArg, xmlWriter, null);
        //    xmlWriter.Close();
        //    string myString = xml.ToString();
        //    byte[] myByteArray = System.Text.Encoding.UTF8.GetBytes(myString);
        //    MemoryStream ms = new MemoryStream(myByteArray);
        //    StreamReader sr = new StreamReader(ms);
        //    var reader = XmlReader.Create(sr);
        //    reader.MoveToContent();
        //    var inputXml = XDocument.ReadFrom(reader);
        //        resultado = inputXml.ToString();
        //    }
        //    catch (Exception ex) {
        //        Claro.Web.Logging.Error("HFC", "CreateConstanceXmlString", ex.Message);
        //    }
        //    Claro.Web.Logging.Info("HFCCreateConstanceXmlString", "HFCPlanMigration", "fin CreateConstanceXmlString");
        //    return Json(new { data = resultado });
        //}

        [HttpPost]
        public JsonResult ValidateProfile(string strIdSession)
        {
            Claro.Web.Logging.Info("HFCValidateProfile", "HFCPlanMigration", "Entro a ValidateProfile");
            FixedTransacService.AuditRequest straudit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);
            var dictionaryResponse = new Dictionary<string, object>();
            dictionaryResponse["strOpcActivaPuedeFideMP"] = ConfigurationManager.AppSettings("strOpcActivaPuedeFideMP");
            dictionaryResponse["strOpcActivaNoPuedeFideMP"] = ConfigurationManager.AppSettings("strOpcActivaNoPuedeFideMP");
            dictionaryResponse["strOpcActivaAutorizaFideMP"] = ConfigurationManager.AppSettings("strOpcActivaAutorizaFideMP");
            dictionaryResponse["strOpcActivaPuedeIngMonMP"] = ConfigurationManager.AppSettings("strOpcActivaPuedeIngMonMP");
            dictionaryResponse["strOpcActivaAutorizaIngMonMP"] = ConfigurationManager.AppSettings("strOpcActivaAutorizaIngMonMP");
            dictionaryResponse["strOpcActivaNoPuedeIngMonMP"] = ConfigurationManager.AppSettings("strOpcActivaNoPuedeIngMonMP");
            Claro.Web.Logging.Info("HFCValidateProfile", "HFCPlanMigration", "fin ValidateProfile");
            Claro.Web.Logging.Error(strIdSession, straudit.transaction, JsonConvert.SerializeObject(dictionaryResponse));
            return Json(new { data = dictionaryResponse });
        }
        public JsonResult GetCurrentPlanServicesGroups(string strIdSession)
        {
            Claro.Web.Logging.Info("HFCGetCurrentPlanServicesGroups", "HFCPlanMigration", "Entro a GetCurrentPlanServicesGroups");
            FixedTransacService.AuditRequest straudit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);
            ConfigurationData configuration = new ConfigurationData();
            configuration.strPlanActualCable = ConfigurationManager.AppSettings("strPlanActualCable").ToString();
            configuration.strPlanActualInternet = ConfigurationManager.AppSettings("strPlanActualInternet").ToString();
            configuration.strPlanActualTelephony = ConfigurationManager.AppSettings("strPlanActualTelephony").ToString();
            Claro.Web.Logging.Info("HFCGetCurrentPlanServicesGroups", "HFCPlanMigration", "fin GetCurrentPlanServicesGroups");
            Claro.Web.Logging.Error(strIdSession, straudit.transaction, JsonConvert.SerializeObject(configuration));
            return Json(new { data = configuration });
        }
        //public JsonResult GetConstancyParameters()
        //{
        //    Claro.Web.Logging.Info("HFCGetConstancyParameters", "HFCPlanMigration", "Entro a GetConstancyParameters");

        //    ConstancyParameters oConstancyParameters = new ConstancyParameters();
        //    oConstancyParameters.PlanMigrationContentCommercial = Functions.GetValueFromConfigFile("PlanMigrationContentCommercial",
        //            Claro.ConfigurationManager.AppSettings("strConstArchivoSIACPOConfigMsg"));
        //    oConstancyParameters.PlanMigrationContentCommercial2 = Functions.GetValueFromConfigFile("PlanMigrationContentCommercial2",
        //            Claro.ConfigurationManager.AppSettings("strConstArchivoSIACPOConfigMsg"));

        //    Claro.Web.Logging.Info("HFCGetConstancyParameters", "HFCPlanMigration", "fin GetConstancyParameters");
        //    return Json(new { data = oConstancyParameters });
        //}
        //public JsonResult LoguearCores(string cantidadCables, string cantidadInternets, string cantidadPhones, string huboCable, string huboInternet, string huboPhone, string arrayCable, string arrayInternet, string arrayPhone)
        //{
        //    Claro.Web.Logging.Info("HFCGetCarriers", "HFCPlanMigration", "Entro a Hfc_PlanMigration_LoguearCores cantidadCables:" + cantidadCables + ", cantidadInternets:" + cantidadInternets + ", cantidadPhones:" + cantidadPhones + ", huboCable:" + huboCable + ", huboInternet:" + huboInternet + ", huboPhone:" + huboPhone + "arrayCable: " + arrayCable + ", arrayInternet:" + arrayInternet + ", arrayPhone:" + arrayPhone); // Temporal
        //    Claro.Web.Logging.Info("HFCLoguearCores", "HFCPlanMigration", "fin LoguearCores");
        //    return Json(new { data = "" });
        //}

        #region Funciones Privadas
        private string CreateDictionaryConstancyXML(string strIdSession, List<string> listParamConstancyPDF, List<ServiceByPlan> lstServices)
        {
            string XmlGenerated = "";
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            FixedTransacService.AuditRequest straudit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);
            try
            {
                //listParamConstancyPDF.Add(Functions.GetValueFromConfigFile("PlanMigrationContentCommercial",
                //    Claro.ConfigurationManager.AppSettings("strConstArchivoSIACPOConfigMsg")));
                listParamConstancyPDF.Add(Functions.GetValueFromConfigFile("PlanMigrationContentCommercial2",
                        Claro.ConfigurationManager.AppSettings("strConstArchivoSIACPOConfigMsg")));

                XmlGenerated += "<PLANTILLA>\r\n ";

                List<string> listLabels = CommonServicesController.GetXmlToString(Common.GetApplicationRoute() + "/DataTransac/HFCConstance.xml");

                if (listParamConstancyPDF.Count >= listLabels.Count)
                {
                    int contador = 0;
                    foreach (string key in listLabels)
                    {
                        if (key == "NOMBRE_SERVICIO")
                        {
                            foreach (ServiceByPlan item in lstServices)
                            {
                                //dictionary.Add("NOMBRE_SERVICIO", item.DesServSisact);
                                //dictionary.Add("TIPO_SERVICIO", item.ServiceType);
                                //dictionary.Add("GRUPO_SERVICIO", item.GroupServ);
                                //dictionary.Add("CF_TOTAL_IGV", item.CF);
                                XmlGenerated += "<NOMBRE_SERVICIO>" + item.DesServSisact + "</NOMBRE_SERVICIO>\r\n ";
                                XmlGenerated += "<TIPO_SERVICIO>" + item.ServiceType + "</TIPO_SERVICIO>\r\n ";
                                XmlGenerated += "<GRUPO_SERVICIO>" + item.GroupServ + "</GRUPO_SERVICIO>\r\n ";
                                XmlGenerated += "<CF_TOTAL_IGV>" + item.CF + "</CF_TOTAL_IGV>\r\n ";
                            }
                            contador++;
                        }
                        else if (key == "TIPO_SERVICIO" || key == "GRUPO_SERVICIO" || key == "CF_TOTAL_IGV")
                        {
                            contador++;
                        }
                        else
                        {
                            // dictionary.Add(key, listParamConstancyPDF[contador]);
                            XmlGenerated += "<" + key + ">" + listParamConstancyPDF[contador] + "</" + key + ">\r\n "; 
                            contador++;
                        }
                    }
                }

                XmlGenerated += "</PLANTILLA>";

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(straudit.Session, straudit.transaction, ex.Message);
            }
            return XmlGenerated;
        }
        #endregion


        //[Proy 32581]
        #region Proy 32581


            #region HFCChoosePlan
            public ActionResult HFCChoosePlan()
            {
                return PartialView();
            }
           
            [HttpPost]
            public JsonResult HFCChoosePlanLoad(LteMigrationPlanRequest objModel)
            {
                Claro.Web.Logging.Info("HFCChoosePlanLoad", "HFCPlanMigration-Request", JsonConvert.SerializeObject(objModel));
                FixedTransacService.AuditRequest straudit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(objModel.strIdSession);
                var objHfcChoosePlanLoadModel = new LteChoosePlanLoadModel();
                objHfcChoosePlanLoadModel.bolPermition = false;
                var strOffice = GetOffice(objModel.strIdSession, objModel.strCodeUser).strCodeOffice;
                var strOfficeDefault = GetValueConfig("strPuntoVentaDefault", objModel.strIdSession, "HFC-MIGRACIoN DE PLAN-Config");
                FixedTransacService.PlansResponseHfc objPlansResponse;
                FixedTransacService.PlansRequestHfc objPlansRequest = new FixedTransacService.PlansRequestHfc
                {
                    strOffice = strOffice,
                    strOfficeDefault = strOfficeDefault,
                    strOferta = oTransacServ.Constants.PresentationLayer.NumeracionCERO + oTransacServ.Constants.PresentationLayer.NumeracionUNO,
                    strTipoProducto = objModel.strTypeProduct,
                    strFlagEjecution = GetValueConfig("strFlagFiltroPDV", objModel.strIdSession, "LTE-MIGRACIoN DE PLAN-Config"),
                    audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(objModel.strIdSession)
                };

                FixedTransacService.CamapaignResponse objCamapaignResponse;
                FixedTransacService.CamapaignRequest objCamapaignRequest = new FixedTransacService.CamapaignRequest
                {
                    audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(objModel.strIdSession),
                    Active = 0
                };

                try
                {
                    objPlansResponse = Claro.Web.Logging.ExecuteMethod<FixedTransacService.PlansResponseHfc>(() => { return new FixedTransacService.FixedTransacServiceClient().GetNewPlans(objPlansRequest); });
                    objHfcChoosePlanLoadModel.lstSearchOptions = Functions.GetListValuesXML("BuscaPlanOpcionesXML", Constants.ZeroNumber, oTransacServ.Constants.SiacutDataXML);
                    Claro.Web.Logging.Info("HFCChoosePlanLoad", "HFCPlanMigration- lstSearchOptions", JsonConvert.SerializeObject(objHfcChoosePlanLoadModel.lstSearchOptions));
                    var strPermitionNot = GetValueConfig("gConstNoVigentePlanMigracion", objModel.strIdSession, "HFC-MIGRACIÓN DE PLAN-Config");
                    var intPositionAccess = objModel.strPermitions.ToUpper().IndexOf(strPermitionNot.ToUpper(), StringComparison.OrdinalIgnoreCase);
                    Claro.Web.Logging.Info("HFCChoosePlanLoad", "HFCPlanMigration- strPermitionNot", "-" + strPermitionNot + "-" + intPositionAccess.ToString() + "-");
                        
                    if (Functions.CheckStr(objModel.strPlaneCodeInst).ToUpper().IndexOf(Functions.CheckStr(ConfigurationManager.AppSettings("strPlanoFTTH")).ToUpper()) > -1)
                    {
                        objPlansResponse.listPlan = (from x in objPlansResponse.listPlan
                                                     where x.strDesPlanSisact.ToUpper().Contains(Functions.CheckStr(KEY.AppSettings("strTipoProductoFTTH")).ToUpper())
                                                     select x).ToList();
                        Claro.Web.Logging.Info("HFCChoosePlanLoad", " HFCPlanMigration - listPlan FTTH : ", Functions.CheckStr(objModel.strPlaneCodeInst).ToUpper());
                    
                    }
                    else{
                        objPlansResponse.listPlan = (from x in objPlansResponse.listPlan
                                                     where !x.strDesPlanSisact.ToUpper().Contains(Functions.CheckStr(KEY.AppSettings("strTipoProductoFTTH")).ToUpper())
                                                     select x).ToList();
                        Claro.Web.Logging.Info("HFCChoosePlanLoad", " HFCPlanMigration - listPlan HFC :", Functions.CheckStr(objModel.strPlaneCodeInst).ToUpper());
                    }
                       
                     
                    
                    if (intPositionAccess < 0)
                    {
                        var itemToRemove = objHfcChoosePlanLoadModel.lstSearchOptions.Single(w => w.Description.Equals("NO VIGENTES"));
                        objHfcChoosePlanLoadModel.lstSearchOptions.Remove(itemToRemove);
                        itemToRemove = objHfcChoosePlanLoadModel.lstSearchOptions.Single(w => w.Description.Equals("TODOS"));
                        objHfcChoosePlanLoadModel.lstSearchOptions.Remove(itemToRemove);
                        objHfcChoosePlanLoadModel.lstPlans = (from x in objPlansResponse.listPlan
                            where x.strStatus == "1"
                            select x).ToList();
                        objHfcChoosePlanLoadModel.bolPermition = false;
                        Claro.Web.Logging.Info("HFCChoosePlanLoad", "HFCPlanMigration- intPositionAccess", "Eliminó elementos");

                    }
                    else
                    {
                        objHfcChoosePlanLoadModel.lstPlans = objPlansResponse.listPlan;
                        Claro.Web.Logging.Info("HFCChoosePlanLoad", "HFCPlanMigration- strPermitionNot", "-No Elim elementos-");
                        objHfcChoosePlanLoadModel.bolPermition = true;

                    }
                    objHfcChoosePlanLoadModel.lstCampaniasAndSolutions = objPlansResponse.listPlan;
                    objHfcChoosePlanLoadModel.lstCampaigns = (from sol in objHfcChoosePlanLoadModel.lstPlans
                        select sol.strCampaignDescription).Distinct();

                    objHfcChoosePlanLoadModel.lstSolutions = (from sol in objHfcChoosePlanLoadModel.lstPlans
                        select sol.strSolucion).Distinct();
                    objHfcChoosePlanLoadModel.lstPlans = objHfcChoosePlanLoadModel.lstPlans.OrderBy(x => x.strDesPlanSisact).ToList();


                }
                catch (Exception ex)
                {
                    objPlansResponse = null;
                    Claro.Web.Logging.Error(objModel.strIdSession, straudit.transaction, Claro.SIACU.Transac.Service.Functions.GetExceptionMessage(ex));

                }
                Claro.Web.Logging.Info(objModel.strIdSession, straudit.transaction, JsonConvert.SerializeObject(objHfcChoosePlanLoadModel));
                Claro.Web.Logging.Info("HFCChoosePlanLoad", "HFCPlanMigration", "fin ChoosePlanLoad");

                return Json(new { data = objHfcChoosePlanLoadModel}, JsonRequestBehavior.AllowGet);
            }
            #endregion
        #endregion

        #region PROY-140245-IDEA140240
            /// <summary>
            /// metodo que valida el colaborador.
            /// </summary>
            /// <param name="objRequest"></param>
            /// <param name="strIdSession"></param>
            /// <returns>JsonResult</returns>
            /// <remarks>GetValidateCollaborator</remarks>
            /// <list type="bullet">
            /// <item><CreadoPor>Everis</CreadoPor></item>
            /// <item><FecCrea>20/05/2019.</FecCrea></item></list>
            /// <list type="bullet">
            /// <item><FecActu>20/05/2019.</FecActu></item>
            /// <item><Resp>Everis</Resp></item>
            /// <item><Mot>PROY-140245-Oferta Colaborador</Mot></item></list>
        public JsonResult GetValidateCollaborator(CommonTransacService.GetValidateCollaboratorRequest objRequest, string strIdSession, string strTipoDocumento)
        {
            CommonTransacService.GetValidateCollaboratorResponse objValidateCollaboratorResponse = null;
            AuditRequestFixed objAuditRequest = App_Code.Common.CreateAuditRequest<AuditRequestFixed>(strIdSession);

            try
            {
                objRequest.MessageRequest.Header = GetHeader(KEY.AppSettings("strOperationValColab"));

                objRequest.MessageRequest.Body.validarColaboradorRequest.auditRequest = new CommonTransacService.GetValidateCollaboratorAuditRequest()
                {
                    idTransaccion = objAuditRequest.transaction,
                    ipAplicacion = objAuditRequest.ipAddress,
                    nombreAplicacion = objAuditRequest.applicationName,
                    usuarioAplicacion = objAuditRequest.userName
                };
                
                objRequest.MessageRequest.Body.validarColaboradorRequest.tipoDocumento = ObtenerCodTipoDocSISACT(strTipoDocumento);
                objRequest.MessageRequest.Body.validarColaboradorRequest.casoEspecial = KEY.AppSettings("strCasoEspecial");

                objValidateCollaboratorResponse = Claro.Web.Logging.ExecuteMethod<CommonTransacService.GetValidateCollaboratorResponse>(() =>
                {
                    return objCommonTransacService.GetValidateCollaborator(objRequest, objAuditRequest.Session);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objAuditRequest.Session, objAuditRequest.transaction, ex.Message);
                throw new Exception(ex.Message);
            }

            return Json(objValidateCollaboratorResponse, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Metodo que consulta las campanias.
        /// </summary>
        /// <param name="objRequest"></param>
        /// <param name="strIdSession"></param>
        /// <param name="strTipoDocumento"></param>
        /// <returns>JsonResult</returns>
        /// <remarks>GetConsultCampaign</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>20/05/2019.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>PROY-140245-Oferta Colaborador</Mot></item></list>
        public JsonResult GetConsultCampaign(CommonTransacService.GetConsultCampaignRequest objRequest, string strIdSession, string strTipoDocumento)
        {
            CommonTransacService.GetConsultCampaignResponse objConsultarCampaniaResponse = null;
            AuditRequestFixed objAuditRequest = App_Code.Common.CreateAuditRequest<AuditRequestFixed>(strIdSession);
            try
            {
                objRequest.MessageRequest.Header = GetHeader(KEY.AppSettings("strOperationConsultarCampania"));
                
                objRequest.MessageRequest.Body.consultarCampaniaRequest.auditRequest = new CommonTransacService.GetConsultCampaignAuditRequest()
                {
                    idTransaccion = objAuditRequest.transaction,
                    ipAplicacion = objAuditRequest.ipAddress,
                    nombreAplicacion = objAuditRequest.applicationName,
                    usuarioAplicacion = objAuditRequest.userName
                };

                objRequest.MessageRequest.Body.consultarCampaniaRequest.consultaCampania.tipoDoc = ObtenerCodTipoDocSISACT(strTipoDocumento);
                objRequest.MessageRequest.Body.consultarCampaniaRequest.consultaCampania.nroPed = Constant.strConstVacio;
                objRequest.MessageRequest.Body.consultarCampaniaRequest.consultaCampania.nroPedDet = Constant.strConstVacio;
                objRequest.MessageRequest.Body.consultarCampaniaRequest.consultaCampania.nroCont = Constant.strConstVacio;
                objRequest.MessageRequest.Body.consultarCampaniaRequest.consultaCampania.nroContDet = Constant.strConstVacio;

                objConsultarCampaniaResponse = Claro.Web.Logging.ExecuteMethod<CommonTransacService.GetConsultCampaignResponse>(() =>
                {
                    return objCommonTransacService.GetConsultCampaign(objRequest, objAuditRequest.Session);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objAuditRequest.Session, objAuditRequest.transaction, ex.Message);
                throw new Exception(ex.Message);
            }
            return Json(objConsultarCampaniaResponse, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Metodo que registra la campania.
        /// </summary>
        /// <param name="objRequest"></param>
        /// <param name="strIdSession"></param>
        /// <param name="strTipoDocumento"></param>
        /// <returns>JsonResult</returns>
        /// <remarks>GetRegisterCampaign</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>20/05/2019.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>PROY-140245-Oferta Colaborador</Mot></item></list>
        public JsonResult GetRegisterCampaign(CommonTransacService.GetRegisterCampaignRequest objRequest, string strIdSession, string strTipoDocumento)
        {
            CommonTransacService.GetRegisterCampaignResponse objRegisterCampaignResponse = null;
            AuditRequestFixed objAuditRequest = App_Code.Common.CreateAuditRequest<AuditRequestFixed>(strIdSession);
            try
            {
                objRequest.MessageRequest.Header = GetHeader(KEY.AppSettings("strOperationRegCampania"));

                objRequest.MessageRequest.Body.registrarCampaniaRequest.auditRequest = new CommonTransacService.GetRegisterCampaignAuditRequest()
                {
                    idTransaccion = objAuditRequest.transaction,
                    ipAplicacion = objAuditRequest.ipAddress,
                    nombreAplicacion = objAuditRequest.applicationName,
                    usuarioAplicacion = objAuditRequest.userName
                };

                objRequest.MessageRequest.Body.registrarCampaniaRequest.registrarCampania.tipoDocumento = ObtenerCodTipoDocSISACT(strTipoDocumento);
                objRequest.MessageRequest.Body.registrarCampaniaRequest.registrarCampania.nroSec = Constant.strCero;
                objRequest.MessageRequest.Body.registrarCampaniaRequest.registrarCampania.nroPed = Constant.strCero;
                objRequest.MessageRequest.Body.registrarCampaniaRequest.registrarCampania.nroPedDet = Constant.strCero;
                objRequest.MessageRequest.Body.registrarCampaniaRequest.registrarCampania.nroCont = Constant.strCero;
                objRequest.MessageRequest.Body.registrarCampaniaRequest.registrarCampania.nroContDet = Constant.strCero;
                objRequest.MessageRequest.Body.registrarCampaniaRequest.registrarCampania.estado = Constant.strConstP;
                objRequest.MessageRequest.Body.registrarCampaniaRequest.registrarCampania.fechaCrea = DateTime.Now.ToShortDateString();
                objRequest.MessageRequest.Body.registrarCampaniaRequest.registrarCampania.usuarioModifica = Constant.strConstVacio;
                objRequest.MessageRequest.Body.registrarCampaniaRequest.registrarCampania.fechaModifica = Constant.strConstVacio;
                objRequest.MessageRequest.Body.registrarCampaniaRequest.registrarCampania.fechaActivacion = DateTime.Now.ToShortDateString();

                objRequest.MessageRequest.Body.registrarCampaniaRequest.registrarCampania.tipoOpeCodigo = ConfigurationManager.AppSettings("strCodTipoOperacionCamp").ToString();
                objRequest.MessageRequest.Body.registrarCampaniaRequest.registrarCampania.tipoOpeDescripcion = ConfigurationManager.AppSettings("strTipoOperacion").ToString();

                objRegisterCampaignResponse = Claro.Web.Logging.ExecuteMethod<CommonTransacService.GetRegisterCampaignResponse>(() =>
                {
                    return objCommonTransacService.GetRegisterCampaign(objRequest, objAuditRequest.Session);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objAuditRequest.Session, objAuditRequest.transaction, ex.Message);
                throw new Exception(ex.Message);
            }
            return Json(objRegisterCampaignResponse, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Metodo que obtiene los valores necesarios para la cabecera de los servicios.
        /// </summary>
        /// <param name="strOperation"></param>
        /// <returns>HeadersRequest</returns>
        /// <remarks>GetHeader</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>20/05/2019.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>PROY-140245-Oferta Colaborador</Mot></item></list>
        public CommonTransacService.HeadersRequest GetHeader(string strOperation)
        {
            var objRequest = new CommonTransacService.HeadersRequest()
            {
                HeaderRequest = new CommonTransacService.HeaderRequest()
                {
                    country = KEY.AppSettings("country"),
                    language = KEY.AppSettings("language"),
                    consumer = KEY.AppSettings("consumer"),
                    system = KEY.AppSettings("system"),
                    modulo = KEY.AppSettings("modulo"),
                    pid = DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                    userId = App_Code.Common.CurrentUser,
                    dispositivo = App_Code.Common.GetClientIP(),
                    wsIp = App_Code.Common.GetApplicationIp(),
                    operation = strOperation,
                    timestamp = DateTime.Now.ToString(),
                    msgType = KEY.AppSettings("msgType"),
                    VarArg = KEY.AppSettings("VarArg")
                }
            };
            return objRequest;
        }
        /// <summary>
        /// Metodo que obtiene codigo de tipo de documento.
        /// </summary>
        /// <param name="strTipoDocumento"></param>
        /// <returns>string</returns>
        /// <remarks>ObtenerCodTipoDocSISACT</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>20/05/2019.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>PROY-140245-Oferta Colaborador</Mot></item></list>
        public string ObtenerCodTipoDocSISACT(string strTipoDocumento )
        {
            var strCodTipoDoc = ConfigurationManager.AppSettings("strCodTipoDocDefualt").ToString();
            try
            {
                string[] strNroDocumento = ConfigurationManager.AppSettings("strTipoDocumentoCampania").ToString().Split('|');

                for (int i = 0; i < strNroDocumento.Length; i++)
                {
                    string[] strDoc = strNroDocumento[i].ToString().Split(',');
                    if (strDoc[0].ToString() == strTipoDocumento)
                    {
                        strCodTipoDoc = strDoc[1].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error("", "", "Error:" + ex.Message);
            }
            return strCodTipoDoc;
        }
        /// <summary>
        /// Metodo que obtiene datos del usuario.
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <returns>JsonResult</returns>
        /// <remarks>IsUserLogin</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>20/05/2019.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>PROY-140245-Oferta Colaborador</Mot></item></list>
        public JsonResult IsUserLogin(string strIdSession)
        {

            CommonTransacService.leerDatosUsuarioResponse objleerDatosUsuarioResponse = null;
            AuditRequestFixed objAuditRequest = App_Code.Common.CreateAuditRequest<AuditRequestFixed>(strIdSession);

            try
            {
                CommonTransacService.leerDatosUsuario objRequest = new CommonTransacService.leerDatosUsuario()
                {
                    audit = new CommonTransacService.AuditRequest()
                    {
                        transaction = objAuditRequest.transaction,
                        ipAddress = objAuditRequest.ipAddress,
                        applicationName = objAuditRequest.applicationName,
                        userName = objAuditRequest.userName,
                        Session = objAuditRequest.Session
                    },
                    aplicacion = ConfigurationManager.AppSettings("CodAplicacion_SIACUNICO").ToString()
                };
                objleerDatosUsuarioResponse = Claro.Web.Logging.ExecuteMethod<CommonTransacService.leerDatosUsuarioResponse>(() =>
                {
                    return objCommonTransacService.GetReadDataUser(objRequest, objAuditRequest.Session);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objAuditRequest.Session, objAuditRequest.transaction, ex.Message);
                throw new Exception(ex.Message);
            }

            return Json(objleerDatosUsuarioResponse, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Metodo que valida cantidad campania.
        /// </summary>
        /// <param name="objRequest"></param>
        /// <param name="strIdSession"></param>
        /// <param name="strTipoDocumento"></param>
        /// <returns>JsonResult</returns>
        /// <remarks>GetValidateQuantityCampaign</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>20/05/2019.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>PROY-140245-Oferta Colaborador</Mot></item></list>
        public JsonResult GetValidateQuantityCampaign(CommonTransacService.GetValidateQuantityCampaignRequest objRequest, string strIdSession, string strTipoDocumento)
        {
            CommonTransacService.GetValidateQuantityCampaignResponse objValidateQuantityCampaignResponse = null;
            AuditRequestFixed objAuditRequest = App_Code.Common.CreateAuditRequest<AuditRequestFixed>(strIdSession);

            try
            {
                objRequest.MessageRequest.Header = GetHeader(KEY.AppSettings("strOperationCantidadMaxima"));

                objRequest.MessageRequest.Body.validarCantidadCampaniaRequest.auditRequest = new CommonTransacService.GetValidateQuantityCampaignAuditRequest()
                {
                    idTransaccion = objAuditRequest.transaction,
                    ipAplicacion = objAuditRequest.ipAddress,
                    nombreAplicacion = objAuditRequest.applicationName,
                    usuarioAplicacion = objAuditRequest.userName
                };

                objRequest.MessageRequest.Body.validarCantidadCampaniaRequest.tipoDocumento = ObtenerCodTipoDocBSCS(strTipoDocumento);
                objRequest.MessageRequest.Body.validarCantidadCampaniaRequest.casoEspecial = KEY.AppSettings("strCasoEspecial");
                objRequest.MessageRequest.Body.validarCantidadCampaniaRequest.tipoOperacion = KEY.AppSettings("strTipoOperacion");
                objRequest.MessageRequest.Body.validarCantidadCampaniaRequest.codAplicativo = KEY.AppSettings("strCodAplicativo");

                objValidateQuantityCampaignResponse = Claro.Web.Logging.ExecuteMethod<CommonTransacService.GetValidateQuantityCampaignResponse>(() =>
                {
                    return objCommonTransacService.GetValidateQuantityCampaign(objRequest, objAuditRequest.Session);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objAuditRequest.Session, objAuditRequest.transaction, ex.Message);
                throw new Exception(ex.Message);
            }
            return Json(objValidateQuantityCampaignResponse, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Metodo que codigo de tipo de documento de BSCS.
        /// </summary>
        /// <param name="strTipoDocumento"></param>
        /// <returns>string</returns>
        /// <remarks>ObtenerCodTipoDocBSCS</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>20/05/2019.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>PROY-140245-Oferta Colaborador</Mot></item></list>
        public string ObtenerCodTipoDocBSCS(string strTipoDocumento)
        {
            var strCodTipoDoc = ConfigurationManager.AppSettings("strCodTipoDocDefualtBSCS").ToString();
            try{
                string[] strNroDocumento = ConfigurationManager.AppSettings("strTipoDocumentoCampaniaBSCS").ToString().Split('|');

                for (int i = 0; i < strNroDocumento.Length; i++)
                {
                    string[] strDoc = strNroDocumento[i].ToString().Split(',');
                    if (strDoc[0].ToString() == strTipoDocumento)
                    {
                        strCodTipoDoc = strDoc[1].ToString();
                    }
                }
            }catch(Exception ex){
                Claro.Web.Logging.Error("","", "Error:" + ex.Message);
            }
            return strCodTipoDoc;
        }

        #endregion


        
    }
}