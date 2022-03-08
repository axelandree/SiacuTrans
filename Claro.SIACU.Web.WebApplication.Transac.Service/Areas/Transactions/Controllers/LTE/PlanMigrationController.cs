using Claro.SIACU.Transac.Service;
using Claro.SIACU.Web.WebApplication.Transac.Service.App_Code;
using Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.CommonServices.Fixed;
using Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.LTE.MigrationPlan;
using Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models.LTE;
using Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService;
using Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KEY = Claro.ConfigurationManager;
using System.Web.Mvc;
using oTransacServ = Claro.SIACU.Transac.Service;
using AuditRequestFixed = Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService.AuditRequest;
using Newtonsoft.Json;
using Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models;
using Constant = Claro.SIACU.Transac.Service.Constants;
using HELPERS = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers;
using MODEL = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models;
namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Controllers.LTE
{
    public class PlanMigrationController : CommonServicesController
    {
        private readonly FixedTransacServiceClient _oServiceFixed = new FixedTransacServiceClient();

        #region Load
        public ActionResult LtePlanMigration()
        {
            try
            {
                Models.HFC.MigrationModel oMigracionModel = new Models.HFC.MigrationModel();
                oMigracionModel.ServerDate = DateTime.Now.ToString("yyyy/MM/dd");
                Claro.Web.Logging.Info("HFCLoad", "HFCPlanMigration", "Se procesó Load");

                int number = Convert.ToInt(KEY.AppSettings("strIncrementDefault", "1"));
                ViewData["SERVERDATEDEFAULT"] = DateTime.Now.AddDays(number).ToString("yyyy/MM/dd");
                return PartialView(oMigracionModel);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error("No existe vista", "LTE-CAMBIO DE PLAN", "LtePlanMigration");
                return null;
            }
        }
        [HttpPost]
        public JsonResult LTEPlanMigrationLoad(LteMigrationPlanRequest objModel)
        {
            var objLteMigrationPlanLoadModel = new LteMigrationPlanLoadModel();
            try
            {
                objLteMigrationPlanLoadModel.objLteMigrationPlanMessage = GetMessageObj(objModel.strIdSession);
                objLteMigrationPlanLoadModel.objLteMigrationPlanMessage.strValidationLine = StatusLineValidate(objModel.strIdSession, 5, objModel.strStateLine);
                objLteMigrationPlanLoadModel.strServerDate = DateTime.Now.ToString("yyyy/MM/dd");
                if (objLteMigrationPlanLoadModel.objLteMigrationPlanMessage.strValidationLine != "")
                    return Json(new { data = objLteMigrationPlanLoadModel }, JsonRequestBehavior.AllowGet);
                objLteMigrationPlanLoadModel.strPhone = GetCustomerPhone(objModel.strIdSession, objModel.strIdContract, objModel.strTypeProduct);
                var objTypificationModel = new TypificationItem();
                string lblMensaje = string.Empty;
                var objtipification = LoadTypifications(objModel.strIdSession, GetValueConfig("strCodigoMigrationPlan", objModel.strIdSession, "LTE-CAMBIO DE PLAN, GetTipi"), ref lblMensaje);
                if (objtipification != null)
                {
                    objTypificationModel.TIPO = objtipification.TIPO;
                    objTypificationModel.CLASE = objtipification.CLASE;
                    objTypificationModel.SUBCLASE = objtipification.SUBCLASE;
                    objTypificationModel.INTERACCION_CODE = objtipification.INTERACCION_CODE;
                    objTypificationModel.TIPO_CODE = objtipification.TIPO_CODE;
                    objTypificationModel.CLASE_CODE = objtipification.CLASE_CODE;
                    objTypificationModel.SUBCLASE_CODE = objtipification.SUBCLASE_CODE;

                    if (objTypificationModel.TIPO != null)
                        objLteMigrationPlanLoadModel.strMessage = String.Empty;
                    else
                        objLteMigrationPlanLoadModel.strMessage = "Sin Cargar la Tipificación";
                }
                else
                {
                    objTypificationModel.TIPO = string.Empty;
                    objTypificationModel.CLASE = string.Empty;
                    objTypificationModel.SUBCLASE = string.Empty;
                    objTypificationModel.INTERACCION_CODE = string.Empty;
                    objTypificationModel.TIPO_CODE = string.Empty;
                    objTypificationModel.CLASE_CODE = string.Empty;
                    objTypificationModel.SUBCLASE_CODE = string.Empty;
                    objLteMigrationPlanLoadModel.strMessage = "Sin Cargar la Tipificación";
                }
                objLteMigrationPlanLoadModel.objTypification = objTypificationModel;
                if (objLteMigrationPlanLoadModel.strMessage != "" || objLteMigrationPlanLoadModel.strPhone == "")
                    return Json(new { data = objLteMigrationPlanLoadModel }, JsonRequestBehavior.AllowGet);
                objLteMigrationPlanLoadModel.strUserCac = GetUsersStr(objModel.strIdSession, objModel.strCodeUser);
                objLteMigrationPlanLoadModel.lstCacDacTypes = GetListCacDac(objModel.strIdSession);
                objLteMigrationPlanLoadModel.objOffice = GetOffice(objModel.strIdSession, objModel.strCodeUser);
                objLteMigrationPlanLoadModel.lstBusinessRules = GetBusinessRulesLst(objModel.strIdSession, objTypificationModel.SUBCLASE_CODE);
                objLteMigrationPlanLoadModel.lstEquipmentByCurrenPlan = GetEquipmentByCurrentPlan(objModel.strIdSession, objModel.strIdContract);
                GetCarriers(objModel.strIdSession, objModel.strIdContract, ref objLteMigrationPlanLoadModel);
                GetServicesByCurrentPlan(objModel.strIdSession, objModel.strIdContract, ref objLteMigrationPlanLoadModel);
                var objIgv = GetCommonConsultIgv(objModel.strIdSession);
                if (objIgv != null)
                    objLteMigrationPlanLoadModel.dblIgv = objIgv.igvD;
                else
                    objLteMigrationPlanLoadModel.dblIgv = Convert.ToDouble(GetValueConfig("valorIGV", objModel.strIdSession, "LTE-CAMBIO DE PLAN-Config"));

                objLteMigrationPlanLoadModel.dblIgvView = objLteMigrationPlanLoadModel.dblIgv + 1;

                FixedTransacService.ValidateDepVelLteResponse objValidateServiceResponse;
                FixedTransacService.ValidateDepVelLteRequest objValidateServiceRequest = new FixedTransacService.ValidateDepVelLteRequest
                {
                    audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(objModel.strIdSession),
                    intContract = int.Parse(objModel.strIdContract)
                };

                objValidateServiceResponse = Claro.Web.Logging.ExecuteMethod<FixedTransacService.ValidateDepVelLteResponse>(() => { return new FixedTransacService.FixedTransacServiceClient().ValidateDepVelLTE(objValidateServiceRequest); });

                if (objValidateServiceResponse.intResult != null)
                {
                    objLteMigrationPlanLoadModel.intLTEValidateVel = objValidateServiceResponse.intResult;
                }

                objLteMigrationPlanLoadModel.lstActions = Functions.GetListValuesXML("ListaAccionServicios", "0", "HFCDatos.xml");
                objLteMigrationPlanLoadModel.lstOptions = Functions.GetListValuesXML("ListaADTopesConsumo", "0", "HFCDatos.xml");
                objLteMigrationPlanLoadModel.strIdIdentifyTypeWork = KEY.AppSettings("strIdIdentifyTypeWork");
                objLteMigrationPlanLoadModel.strIdTypeWork = KEY.AppSettings("strTiptraLTE");

                Claro.Web.Logging.Info("Session:" + objModel.strIdSession + ", Contrato:" + objModel.strIdContract, "LTE-MIGRACIÓN DE PLAN", "LTEPlanMigrationLoad-OK");

                Claro.Web.Logging.Info("Session:" + objModel.strIdSession + ", Contrato:" + objModel.strIdContract, "LTE-CAMBIO DE PLAN", "LTEPlanMigrationLoad-OK");

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error("Session:" + objModel.strIdSession + ", Contrato:" + objModel.strIdContract, "LTE-CAMBIO DE PLAN, LTEPlanMigrationLoad", Claro.SIACU.Transac.Service.Functions.GetExceptionMessage(ex));
            }
            return Json(new { data = objLteMigrationPlanLoadModel }, JsonRequestBehavior.AllowGet);
        }

        private void GetCarriers(string strIdSession, string strIdContract, ref LteMigrationPlanLoadModel objLteMigrationPlanLoadModel)
        {
            FixedTransacService.CarrierResponseHfc objCarrierResponse;

            var objCarrierRequestHfc = new CarrierRequestHfc()
            {
                audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession)
            };

            try
            {
                objCarrierResponse = Claro.Web.Logging.ExecuteMethod<CarrierResponseHfc>(() =>
                {
                    return new FixedTransacServiceClient().GetCarrierList(objCarrierRequestHfc);
                });

                if (objCarrierResponse.carriers != null)
                {
                    objLteMigrationPlanLoadModel.lstCarriers = (from ele in objCarrierResponse.carriers
                        select new Carrier
                        {
                            IDCARRIER = ele.IDCARRIER,
                            OPERADOR = StringManipulation.RemoveAccentsAndUpper(ele.OPERADOR)
                        }).ToList();
                }
                else
                {
                    objLteMigrationPlanLoadModel.lstCarriers = new List<Carrier>();

                }
                Claro.Web.Logging.Info("Session:" + strIdSession + ", Contrato:" + strIdContract, "LTE-MIGRACIÓN DE PLAN", "GetCarriers-OK");

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error("Session:" + strIdSession + ", Contrato:" + strIdContract, "LTE-MIGRACIÓN DE PLAN, GetCarriers", Claro.SIACU.Transac.Service.Functions.GetExceptionMessage(ex));

            }

        }
        private void GetServicesByCurrentPlan(string strIdSession, string strIdContract, ref LteMigrationPlanLoadModel objLteMigrationPlanLoadModel)
        {
            ServicesByCurrentPlanRequest objServicesByCurrentPlanRequest = new ServicesByCurrentPlanRequest
            {
                ContractId = strIdContract,
                audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession)
            };
            ServicesByCurrentPlanResponse objServicesByCurrentPlanResponse;
            try
            {
                objServicesByCurrentPlanResponse = Claro.Web.Logging.ExecuteMethod<ServicesByCurrentPlanResponse>(() =>
                {
                    return new FixedTransacServiceClient().GetServicesByCurrentPlan(objServicesByCurrentPlanRequest);
                });
                objLteMigrationPlanLoadModel.lstServicesByCurrentPlan = objServicesByCurrentPlanResponse.ServicesByCurrentPlan;
                if (objLteMigrationPlanLoadModel.lstServicesByCurrentPlan != null)
                {
                    objLteMigrationPlanLoadModel.objServicesByCurrentPlanCharges = CalculatePlanCharges.CalculateCharges(objLteMigrationPlanLoadModel.lstServicesByCurrentPlan);
                }
                Claro.Web.Logging.Info("Session:" + strIdSession + ", Contrato:" + strIdContract, "LTE-MIGRACIÓN DE PLAN", "GetServicesByCurrentPlan-OK");

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error("Session:" + strIdSession + ", Contrato:" + strIdContract, "LTE-MIGRACIÓN DE PLAN, GetServicesByCurrentPlan", Claro.SIACU.Transac.Service.Functions.GetExceptionMessage(ex));
            }
        }
        private List<DetEquipmentService> GetEquipmentByCurrentPlan(string strIdSession, string strIdContract)
        {
            FixedTransacService.AuditRequest straudit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);
            DataEquipmentRequest objEquipmentsByCurrentPlanRequest = new DataEquipmentRequest
            {
                strK_cod_id = strIdContract,
                audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession)
            };

            var objEquipmentsByCurrentPlanResponse = new DataEquipmentResponse();

            try
            {
                objEquipmentsByCurrentPlanResponse = Claro.Web.Logging.ExecuteMethod<DataEquipmentResponse>(() =>
                {
                    return new FixedTransacServiceClient().GetDetEquipo_LTE(objEquipmentsByCurrentPlanRequest);
                });
                Claro.Web.Logging.Info("Session:" + strIdSession + ", Contrato:" + strIdContract, "LTE-MIGRACIÓN DE PLAN", "GetEquipmentByCurrentPlan-OK");
                if (objEquipmentsByCurrentPlanResponse.Data_k_cod_id != null)
                    return objEquipmentsByCurrentPlanResponse.Data_k_cod_id;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error("Session:" + strIdSession + ", Contrato:" + strIdContract, "LTE-MIGRACIÓN DE PLAN, GetEquipmentByCurrentPlan", Claro.SIACU.Transac.Service.Functions.GetExceptionMessage(ex));
            }

            return new List<DetEquipmentService>();
        }
        private string GetCustomerPhone(string strIdSession, string strIdContract, string strTypeProduct)
        {
            var objAuditRequest = App_Code.Common.CreateAuditRequest<Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService.AuditRequest>(strIdSession);
            var objRequest = new ConsultationServiceByContractRequest()
            {
                audit = objAuditRequest,
                strCodContrato = strIdContract,
                typeProduct = strTypeProduct
            };

            var objCustomerPhoneResponse = Claro.Web.Logging.ExecuteMethod(() =>
            {
                return _oServiceFixed.GetCustomerLineNumber(objRequest);
            });
            var strPhone = objCustomerPhoneResponse.msisdn;
            return strPhone;
        }
        private LteMigrationPlanMessageModel GetMessageObj(string strIdSession)
        {

            var objMigrationPlanMessageModel = new LteMigrationPlanMessageModel();

            objMigrationPlanMessageModel.strCurrentCablePlan = GetValueConfig("strPlanActualCable", strIdSession, "LTE-MIGRACIÓN DE PLAN-Config");
            objMigrationPlanMessageModel.strCurrentInternetPlan = GetValueConfig("strPlanActualInternet", strIdSession, "LTE-MIGRACIÓN DE PLAN-Config");
            objMigrationPlanMessageModel.strCurrentPhonePlan = GetValueConfig("strPlanActualTelephony", strIdSession, "LTE-MIGRACIÓN DE PLAN-Config");
            objMigrationPlanMessageModel.strLTEGroupCable = GetValueConfig("strLTEGroupCable", strIdSession, "LTE-MIGRACIÓN DE PLAN-Config");
            objMigrationPlanMessageModel.strLTEGroupInternet = GetValueConfig("strLTEGroupInternet", strIdSession, "LTE-MIGRACIÓN DE PLAN-Config");
            objMigrationPlanMessageModel.strLTEGroupTelephony = GetValueConfig("strLTEGroupTelephony", strIdSession, "LTE-MIGRACIÓN DE PLAN-Config");
            objMigrationPlanMessageModel.strCanLoyaltyActive = GetValueConfig("strOpcActivaPuedeFideMPLTE", strIdSession, "LTE-MIGRACIÓN DE PLAN-Config");
            objMigrationPlanMessageModel.strCantLoyaltyActive = GetValueConfig("strOpcActivaNoPuedeFideMPLTE", strIdSession, "LTE-MIGRACIÓN DE PLAN-Config");
            objMigrationPlanMessageModel.strAuthorizeLoyaltyActive = GetValueConfig("strOpcActivaAutorizaFideMPLTE", strIdSession, "LTE-MIGRACIÓN DE PLAN-Config");
            objMigrationPlanMessageModel.strCanEnterActive = GetValueConfig("strOpcActivaPuedeIngMonMPLTE", strIdSession, "LTE-MIGRACIÓN DE PLAN-Config");
            objMigrationPlanMessageModel.strAuthorizeEnterActive = GetValueConfig("strOpcActivaAutorizaIngMonMPLTE", strIdSession, "LTE-MIGRACIÓN DE PLAN-Config");
            objMigrationPlanMessageModel.strCantEnterActive = GetValueConfig("strOpcActivaNoPuedeIngMonMPLTE", strIdSession, "LTE-MIGRACIÓN DE PLAN-Config");
            objMigrationPlanMessageModel.strErrorMessageIgv = Functions.GetValueFromConfigFile("strMensajeErrorConsultaIGV", KEY.AppSettings("strConstArchivoSIACUTLTEConfigMsg"));
            objMigrationPlanMessageModel.strErrorInteraction = Functions.GetValueFromConfigFile("strMsjErrorInteraction", KEY.AppSettings("strConstArchivoSIACUTLTEConfigMsg"));
            objMigrationPlanMessageModel.strWantToSave = Functions.GetValueFromConfigFile("strAlertaEstaSegGuarCam", KEY.AppSettings("strConstArchivoSIACUTLTEConfigMsg"));
            objMigrationPlanMessageModel.strValidationFormat = Functions.GetValueFromConfigFile("strMsjValidacionCampoFormato", KEY.AppSettings("strConstArchivoSIACUTLTEConfigMsg"));
            objMigrationPlanMessageModel.strSaveSuccessfully = Functions.GetValueFromConfigFile("strMsjTranGrabSatis", KEY.AppSettings("strConstArchivoSIACUTLTEConfigMsg"));
            objMigrationPlanMessageModel.strFieldValidation = Functions.GetValueFromConfigFile("strMsjValidacionCampo", KEY.AppSettings("strConstArchivoSIACUTLTEConfigMsg"));
            objMigrationPlanMessageModel.strFieldValidationSlt = Functions.GetValueFromConfigFile("strMsjValidacionCampoSlt", KEY.AppSettings("strConstArchivoSIACUTLTEConfigMsg"));
            objMigrationPlanMessageModel.strFieldValidationQuantity = Functions.GetValueFromConfigFile("strMsjValidacionCampoCantidad", KEY.AppSettings("strConstArchivoSIACUTLTEConfigMsg"));
            objMigrationPlanMessageModel.strValidationChargueList = Functions.GetValueFromConfigFile("strMsjValidacionCargadoListado", KEY.AppSettings("strConstArchivoSIACUTLTEConfigMsg"));
            objMigrationPlanMessageModel.strYesTechnicalVisit = Functions.GetValueFromConfigFile("strMsjSiVisitaTecnica", KEY.AppSettings("strConstArchivoSIACUTLTEConfigMsg"));
            objMigrationPlanMessageModel.strNotTechnicalVisit = Functions.GetValueFromConfigFile("strMsjNoVisitaTecnica", KEY.AppSettings("strConstArchivoSIACUTLTEConfigMsg"));
            objMigrationPlanMessageModel.strElementWasAdded = Functions.GetValueFromConfigFile("strMsjYaSeAgregoElemento", KEY.AppSettings("strConstArchivoSIACUTLTEConfigMsg"));
            objMigrationPlanMessageModel.strNotHaveAuthorization = Functions.GetValueFromConfigFile("strMsjNoTieneAutorizacion", KEY.AppSettings("strConstArchivoSIACUTLTEConfigMsg"));
            objMigrationPlanMessageModel.strErrorRequestAjax = Functions.GetValueFromConfigFile("strMsjErrorPeticionAJAX", KEY.AppSettings("strConstArchivoSIACUTLTEConfigMsg"));
            objMigrationPlanMessageModel.strSelectToRemove = Functions.GetValueFromConfigFile("strMsjSeleccionarParaEliminar", KEY.AppSettings("strConstArchivoSIACUTLTEConfigMsg"));
            objMigrationPlanMessageModel.strSelectMaximumEquipment = Functions.GetValueFromConfigFile("strMsjMaximoEquipos", KEY.AppSettings("strConstArchivoSIACUTLTEConfigMsg"));
            objMigrationPlanMessageModel.strSelectToRent = Functions.GetValueFromConfigFile("strMsjSeleccionarParaAlquilar", KEY.AppSettings("strConstArchivoSIACUTLTEConfigMsg"));
            objMigrationPlanMessageModel.strErrorLoading = Functions.GetValueFromConfigFile("strMsjErrorAlCargar", KEY.AppSettings("strConstArchivoSIACUTLTEConfigMsg"));
            objMigrationPlanMessageModel.strErrorValidating = Functions.GetValueFromConfigFile("strMsjErrorAlValidar", KEY.AppSettings("strConstArchivoSIACUTLTEConfigMsg"));
            objMigrationPlanMessageModel.strThereAreNoRecords = Functions.GetValueFromConfigFile("strMsjNoExistenRegistros", KEY.AppSettings("strConstArchivoSIACUTLTEConfigMsg"));
            objMigrationPlanMessageModel.strThereAreNoRecordsIn = Functions.GetValueFromConfigFile("strMsjNoExistenRegistrosEn", KEY.AppSettings("strConstArchivoSIACUTLTEConfigMsg"));
            objMigrationPlanMessageModel.strThereAreNoPhone = Functions.GetValueFromConfigFile("strMsjNoExistenLineaTelefono", KEY.AppSettings("strConstArchivoSIACUTLTEConfigMsg"));
            objMigrationPlanMessageModel.strWantChargueCore = Functions.GetValueFromConfigFile("strMsjDeseaCargarCore", KEY.AppSettings("strConstArchivoSIACUTLTEConfigMsg"));
            objMigrationPlanMessageModel.strMessageValidationQuantityDecos = Functions.GetValueFromConfigFile("strMessageValidationQuantityDecos", KEY.AppSettings("strConstArchivoSIACUTLTEConfigMsg"));
            objMigrationPlanMessageModel.strSendEmail = GetValueConfig("hdnMessageSendMail", strIdSession, "LTE-MIGRACIÓN DE PLAN-Config");
            objMigrationPlanMessageModel.strError = GetValueConfig("strMensajeDeError", strIdSession, "LTE-MIGRACIÓN DE PLAN-Config");
            objMigrationPlanMessageModel.strServerPDF = GetValueConfig("strServidorLeerPDF", strIdSession, "LTE-MIGRACIÓN DE PLAN-Config");
            objMigrationPlanMessageModel.strDontLoadTipification = GetValueConfig("gConstNoReconoceTipifiTransaccion", strIdSession, "LTE-MIGRACIÓN DE PLAN-Config");
            objMigrationPlanMessageModel.strCodeHD = GetValueConfig("CodigoHDLTE", strIdSession, "LTE-MIGRACIÓN DE PLAN-Config");
            objMigrationPlanMessageModel.strCodeSD = GetValueConfig("CodigoSDLTE", strIdSession, "LTE-MIGRACIÓN DE PLAN-Config");
            objMigrationPlanMessageModel.strCodeDVR = GetValueConfig("CodigoDVRLTE", strIdSession, "LTE-MIGRACIÓN DE PLAN-Config");
            objMigrationPlanMessageModel.strMessageValidateVelPlan = Functions.GetValueFromConfigFile("strMessageValidateVelPlan", KEY.AppSettings("strConstArchivoSIACUTLTEConfigMsg"));
            objMigrationPlanMessageModel.strMessageValidationQuantityDecos = Functions.GetValueFromConfigFile("strMessageValidationQuantityDecos", KEY.AppSettings("strConstArchivoSIACUTLTEConfigMsg"));
            objMigrationPlanMessageModel.strDayInstallation = GetValueConfig("DiasInstalacion", strIdSession, "LTE-MIGRACIÓN DE PLAN-Config");
            objMigrationPlanMessageModel.strTittleConstancy = GetValueConfig("TituloConstancia", strIdSession, "LTE-MIGRACIÓN DE PLAN-Config");
            objMigrationPlanMessageModel.strTextConstancy = GetValueConfig("strTextoConstancia", strIdSession, "LTE-MIGRACIÓN DE PLAN-Config");
            objMigrationPlanMessageModel.strActionDefaultTopeConsumo = GetValueConfig("strActionXDefectoTopeConsumo", strIdSession, "LTE-MIGRACIÓN DE PLAN-Config");
            objMigrationPlanMessageModel.strValueTopeConsumo = GetValueConfig("strValorTopeConsumo", strIdSession, "LTE-MIGRACIÓN DE PLAN-Config");
            objMigrationPlanMessageModel.strValueTopeConsumoOCC = GetValueConfig("gConstMontoCobroTC", strIdSession, "LTE-MIGRACIÓN DE PLAN-Config");
            objMigrationPlanMessageModel.strNameFormatLte = GetValueConfig("strNombreFormatoCambiodeEquipoLTE", strIdSession, "LTE-MIGRACIÓN DE PLAN-Config");
            objMigrationPlanMessageModel.strCodeTechnicalVisit = GetValueConfig("strCodigoMotivoVisitaTecnica", strIdSession, "LTE-MIGRACIÓN DE PLAN-Config");

            return objMigrationPlanMessageModel;
        }
        #endregion

        #region LTEChoosePlan
        public ActionResult LTEChoosePlan()
        {
            return PartialView();
        }
        [HttpPost]
        public JsonResult LTEChoosePlanLoad(LteMigrationPlanRequest objModel)
        {
            Claro.Web.Logging.Info("LTEChoosePlanLoad", "LTEPlanMigration-Request", JsonConvert.SerializeObject(objModel));
            FixedTransacService.AuditRequest straudit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(objModel.strIdSession);
            var objLteChoosePlanLoadModel = new LteChoosePlanLoadModel();
            objLteChoosePlanLoadModel.bolPermition = true;
            var strIdOfficeDefault = GetValueConfig("strPuntoVentaDefault", objModel.strIdSession, "LTE-MIGRACIoN DE PLAN-Config");
            if (objModel.strOffice == null)
                objModel.strOffice = strIdOfficeDefault;

            FixedTransacService.PlansResponseHfc objPlansResponse;
            FixedTransacService.PlansRequestHfc objPlansRequest = new FixedTransacService.PlansRequestHfc
            {
                strOffice = objModel.strOffice,
                strOfficeDefault = strIdOfficeDefault,
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
                objLteChoosePlanLoadModel.lstSearchOptions = Functions.GetListValuesXML("BuscaPlanOpcionesXML", Constants.ZeroNumber, oTransacServ.Constants.SiacutDataXML);
                Claro.Web.Logging.Info("LTEChoosePlanLoad", "LTEPlanMigration- lstSearchOptions", JsonConvert.SerializeObject(objLteChoosePlanLoadModel.lstSearchOptions));
                var strPermitionNot = GetValueConfig("gConstNoVigentePlanMigracion", objModel.strIdSession, "LTE-MIGRACIÓN DE PLAN-Config");
                var intPositionAccess = objModel.strPermitions.ToUpper().IndexOf(strPermitionNot.ToUpper(), StringComparison.OrdinalIgnoreCase);
                Claro.Web.Logging.Info("LTEChoosePlanLoad", "LTEPlanMigration- strPermitionNot", "-" + strPermitionNot + "-" + intPositionAccess.ToString() + "-");
                if (intPositionAccess < 0)
                {
                    var itemToRemove = objLteChoosePlanLoadModel.lstSearchOptions.Single(w => w.Description.Equals("NO VIGENTES"));
                    objLteChoosePlanLoadModel.lstSearchOptions.Remove(itemToRemove);
                    itemToRemove = objLteChoosePlanLoadModel.lstSearchOptions.Single(w => w.Description.Equals("TODOS"));
                    objLteChoosePlanLoadModel.lstSearchOptions.Remove(itemToRemove);
                    objLteChoosePlanLoadModel.lstPlans = (from x in objPlansResponse.listPlan
                        where x.strStatus == "1"
                        select x).ToList();
                    objLteChoosePlanLoadModel.bolPermition = false;
                    Claro.Web.Logging.Info("LTEChoosePlanLoad", "LTEPlanMigration- intPositionAccess", "Eliminó elementos");

                }
                else
                {
                    objLteChoosePlanLoadModel.lstPlans = objPlansResponse.listPlan;
                    Claro.Web.Logging.Info("LTEChoosePlanLoad", "LTEPlanMigration- strPermitionNot", "-No Elim elementos-");
                    objLteChoosePlanLoadModel.bolPermition = true;

                }
                objLteChoosePlanLoadModel.lstCampaniasAndSolutions = objPlansResponse.listPlan;
                objLteChoosePlanLoadModel.lstCampaigns = (from sol in objLteChoosePlanLoadModel.lstPlans
                    select sol.strCampaignDescription).Distinct();

                objLteChoosePlanLoadModel.lstSolutions = (from sol in objLteChoosePlanLoadModel.lstPlans
                    select sol.strSolucion).Distinct();

            }
            catch (Exception ex)
            {
                objPlansResponse = null;
                Claro.Web.Logging.Error(objModel.strIdSession, straudit.transaction, Claro.SIACU.Transac.Service.Functions.GetExceptionMessage(ex));

            }
            Claro.Web.Logging.Info(objModel.strIdSession, straudit.transaction, JsonConvert.SerializeObject(objLteChoosePlanLoadModel));
            Claro.Web.Logging.Info("LTEChoosePlanLoad", "LTEPlanMigration", "fin ChoosePlanLoad");

            return Json(new { data = objLteChoosePlanLoadModel }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Servicios del core - LTEChooseCoreServicesByPlan
        public ActionResult LTEChooseCoreServicesByPlan()
        {
            try
            {
                return PartialView("~/Areas/Transactions/Views/PlanMigration/LTEChooseCoreServicesByPlan.cshtml");
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error("No existe vista", "LTE-MIGRACIÓN DE PLAN, LTEChooseCoreServicesByPlanLoad", "~/Areas/Transactions/Views/PlanMigration/LTEChooseCoreServicesByPlan.cshtml");
                return null;
            }
        }
        [HttpPost]
        public JsonResult LTEChooseCoreServicesByPlanLoad(LteMigrationPlanRequest objModel)
        {
            Claro.Web.Logging.Info("LTEChooseCoreServicesByPlan", "LTEPlanMigration", "Entro a ChooseCoreServicesByPlan");

            Models.LTE.LteMigrationModel oLteMigrationModel = new Models.LTE.LteMigrationModel();
            var objLteChooseCoreServicesByPlanLoad = new LteChooseCoreServicesByPlanLoadModel();
            var lstServicesByPlan = new List<FixedTransacService.ServiceByPlan>();

            objLteChooseCoreServicesByPlanLoad.objLteChooseCoreServicesByPlanLoadMessage = GetMessageObjCoreService(objModel.strIdSession);

            objLteChooseCoreServicesByPlanLoad.strLTEGroupCable = GetValueConfig("strLTEGroupCable", objModel.strIdSession, "LTE-MIGRACIÓN DE PLAN-Config");
            objLteChooseCoreServicesByPlanLoad.strLTEGroupInternet = GetValueConfig("strLTEGroupInternet", objModel.strIdSession, "LTE-MIGRACIÓN DE PLAN-Config");
            objLteChooseCoreServicesByPlanLoad.strLTEGroupTelephony = GetValueConfig("strLTEGroupTelephony", objModel.strIdSession, "LTE-MIGRACIÓN DE PLAN-Config");
            objLteChooseCoreServicesByPlanLoad.strLTEServicesType = GetValueConfig("TipoServicioLTE", objModel.strIdSession, "LTE-MIGRACIÓN DE PLAN-Config");
            objLteChooseCoreServicesByPlanLoad.arrLTEGroupCable = objLteChooseCoreServicesByPlanLoad.strLTEGroupCable.Split(',');
            objLteChooseCoreServicesByPlanLoad.arrLTEGroupInternet = objLteChooseCoreServicesByPlanLoad.strLTEGroupInternet.Split(',');
            objLteChooseCoreServicesByPlanLoad.arrLTEGroupTelephony = objLteChooseCoreServicesByPlanLoad.strLTEGroupTelephony.Split(',');
            objLteChooseCoreServicesByPlanLoad.arrLTEServicesType = objLteChooseCoreServicesByPlanLoad.strLTEServicesType.Split(',');

            FixedTransacService.PlanServiceResponse objServicesResponse;
            FixedTransacService.PlanServiceRequest objServicesRequest = new FixedTransacService.PlanServiceRequest
            {
                idplan = objModel.strIdPlan,
                strTipoProducto = objModel.strTypeProduct,
                audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(objModel.strIdSession)
            };
            try
            {
                objServicesResponse = Claro.Web.Logging.ExecuteMethod<FixedTransacService.PlanServiceResponse>(() => { return new FixedTransacService.FixedTransacServiceClient().LteGetServicesByPlan(objServicesRequest); });

                if (objServicesResponse.listServicio != null)
                {

                    Claro.Web.Logging.Info("Sesssion", "LTEChooseCoreServicesByPlanLoad-LTE", JsonConvert.SerializeObject(objServicesResponse.listServicio));
                    lstServicesByPlan = objServicesResponse.listServicio;
                    lstServicesByPlan = (from ele in lstServicesByPlan
                        group ele by ele.CodServSisact
                        into groups
                        select groups.OrderBy(x => x.CodServiceType).First()).ToList();
                    Claro.Web.Logging.Info("Session:" + objModel.strIdSession + ", Contrato:" + objModel.strIdContract, "LTE-MIGRACIÓN DE PLAN-LTEChooseCoreServicesByPlanLoad1", JsonConvert.SerializeObject(lstServicesByPlan));

                    objLteChooseCoreServicesByPlanLoad.lstServicesByPlanCable = (from x in lstServicesByPlan
                        where objLteChooseCoreServicesByPlanLoad.arrLTEGroupCable.Contains(x.CodGroupServ) &&
                              objLteChooseCoreServicesByPlanLoad.arrLTEServicesType.Contains(x.CodServiceType)
                        select x).ToList();

                    objLteChooseCoreServicesByPlanLoad.lstServicesByPlanInternet = (from x in lstServicesByPlan
                        where objLteChooseCoreServicesByPlanLoad.arrLTEGroupInternet.Contains(x.CodGroupServ) &&
                              objLteChooseCoreServicesByPlanLoad.arrLTEServicesType.Contains(x.CodServiceType)
                        select x).ToList();
                    objLteChooseCoreServicesByPlanLoad.lstServicesByPlanTelephone = (from x in lstServicesByPlan
                        where objLteChooseCoreServicesByPlanLoad.arrLTEGroupTelephony.Contains(x.CodGroupServ) &&
                              objLteChooseCoreServicesByPlanLoad.arrLTEServicesType.Contains(x.CodServiceType)
                        select x).ToList();


                }
                Claro.Web.Logging.Info("Session:" + objModel.strIdSession + ", Contrato:" + objModel.strIdContract, "LTE-MIGRACIÓN DE PLAN", "LTEChooseCoreServicesByPlanLoad-OK");

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error("Session:" + objModel.strIdSession + ", Contrato:" + objModel.strIdContract, "LTE-MIGRACIÓN DE PLAN, LTEChooseCoreServicesByPlanLoad", Claro.SIACU.Transac.Service.Functions.GetExceptionMessage(ex));

            }
            Claro.Web.Logging.Info(objModel.strIdSession, "LTEChooseCoreServicesByPlanLoad", JsonConvert.SerializeObject(objLteChooseCoreServicesByPlanLoad));

            return Json(new { data = objLteChooseCoreServicesByPlanLoad });
        }

        private LteChooseCoreServicesByPlanLoadMessageModel GetMessageObjCoreService(string strIdSession)
        {

            var objLteChooseCoreServicesByPlanLoadMessage = new LteChooseCoreServicesByPlanLoadMessageModel();

            objLteChooseCoreServicesByPlanLoadMessage.strMessageValidateVelPlan = Functions.GetValueFromConfigFile("strMessageValidateVelPlan", KEY.AppSettings("strConstArchivoSIACUTLTEConfigMsg"));

            return objLteChooseCoreServicesByPlanLoadMessage;
        }

        #endregion

        #region LTEChooseServicesByPlan
        public ActionResult LTEChooseServicesByPlan()
        {
            return PartialView();
        }

        [HttpPost]
        public JsonResult LTEChooseServicesByPlanLoad(LteMigrationPlanRequest objModel)
        {
            Claro.Web.Logging.Info("LTEChooseServicesByPlanLoad", "LTEPlanMigration", "Entro a chooseServicesByPlanLoad");

            Models.LTE.LteMigrationModel oLteMigrationModel = new Models.LTE.LteMigrationModel();
            var objLteChooseServicesByPlanLoadModel = new LteChooseServicesByPlanLoadModel();
            var lstServicesByPlan = new List<FixedTransacService.ServiceByPlan>();
            objLteChooseServicesByPlanLoadModel.strLTEGroupCable = GetValueConfig("strLTEGroupCable", objModel.strIdSession, "LTE-MIGRACIÓN DE PLAN-Config");
            objLteChooseServicesByPlanLoadModel.strLTEGroupInternet = GetValueConfig("strLTEGroupInternet", objModel.strIdSession, "LTE-MIGRACIÓN DE PLAN-Config");
            objLteChooseServicesByPlanLoadModel.strLTEGroupTelephony = GetValueConfig("strLTEGroupTelephony", objModel.strIdSession, "LTE-MIGRACIÓN DE PLAN-Config");
            objLteChooseServicesByPlanLoadModel.strLTEServicesType = GetValueConfig("TipoServicioAdicionalLTE", objModel.strIdSession, "LTE-MIGRACIÓN DE PLAN-Config");
            objLteChooseServicesByPlanLoadModel.arrLTEGroupCable = objLteChooseServicesByPlanLoadModel.strLTEGroupCable.Split(',');
            objLteChooseServicesByPlanLoadModel.arrLTEGroupInternet = objLteChooseServicesByPlanLoadModel.strLTEGroupInternet.Split(',');
            objLteChooseServicesByPlanLoadModel.arrLTEGroupTelephony = objLteChooseServicesByPlanLoadModel.strLTEGroupTelephony.Split(',');
            objLteChooseServicesByPlanLoadModel.arrLTEServicesType = objLteChooseServicesByPlanLoadModel.strLTEServicesType.Split(',');

            FixedTransacService.PlanServiceResponse objServicesResponse;
            FixedTransacService.PlanServiceRequest objServicesRequest = new FixedTransacService.PlanServiceRequest
            {
                idplan = objModel.strIdPlan,
                strTipoProducto = objModel.strTypeProduct,
                audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(objModel.strIdSession)
            };

            try
            {
                objServicesResponse = Claro.Web.Logging.ExecuteMethod<FixedTransacService.PlanServiceResponse>(() => { return new FixedTransacService.FixedTransacServiceClient().LteGetServicesByPlan(objServicesRequest); });
                if (objServicesResponse.listServicio != null)
                {
                    lstServicesByPlan = objServicesResponse.listServicio;
                    lstServicesByPlan = (from ele in lstServicesByPlan
                        group ele by ele.CodServSisact
                        into groups
                        select groups.OrderBy(x => x.CodServiceType).First()).ToList();

                    objLteChooseServicesByPlanLoadModel.lstServicesByPlanCable = (from x in lstServicesByPlan
                        where objLteChooseServicesByPlanLoadModel.arrLTEGroupCable.Contains(x.CodGroupServ) &&
                              objLteChooseServicesByPlanLoadModel.arrLTEServicesType.Contains(x.CodServiceType)
                        select x).ToList();

                    objLteChooseServicesByPlanLoadModel.lstServicesByPlanInternet = (from x in lstServicesByPlan
                        where objLteChooseServicesByPlanLoadModel.arrLTEGroupInternet.Contains(x.CodGroupServ) &&
                              objLteChooseServicesByPlanLoadModel.arrLTEServicesType.Contains(x.CodServiceType)
                        select x).ToList();

                    objLteChooseServicesByPlanLoadModel.lstServicesByPlanTelephone = (from x in lstServicesByPlan
                        where objLteChooseServicesByPlanLoadModel.arrLTEGroupTelephony.Contains(x.CodGroupServ) &&
                              objLteChooseServicesByPlanLoadModel.arrLTEServicesType.Contains(x.CodServiceType)
                        select x).ToList();
                }

                Claro.Web.Logging.Info("Session:" + objModel.strIdSession + ", Contrato:" + objModel.strIdContract, "LTE-MIGRACIÓN DE PLAN", "LTEChooseServicesByPlanLoad-OK");

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error("Session:" + objModel.strIdSession + ", Contrato:" + objModel.strIdContract, "LTE-MIGRACIÓN DE PLAN, LTEChooseServicesByPlanLoad", Claro.SIACU.Transac.Service.Functions.GetExceptionMessage(ex));

            }
            Claro.Web.Logging.Info("Session:" + objModel.strIdSession + ", Contrato:" + objModel.strIdContract, "LTE-MIGRACIÓN DE PLAN-LTEChooseServicesByPlanLoadF", JsonConvert.SerializeObject(objLteChooseServicesByPlanLoadModel));

            return Json(new { data = objLteChooseServicesByPlanLoadModel });
        }
        #endregion

        #region Decos Adicionales - LTEChooseEquipmentByPlan
        public ActionResult LTEChooseEquipmentByPlan()
        {
            return PartialView();
        }


        [HttpPost]
        public JsonResult LTEChooseEquipmentByPlanLoad(LteMigrationPlanRequest objModel)
        {
            var objLteChooseEquipmentByPlan = new LteChooseEquipmentByPlanLoadModel();
            objLteChooseEquipmentByPlan.objMessage = GetMessageObjEquipment(objModel.strIdSession);
            try
            {
                objLteChooseEquipmentByPlan.objDecoMatriz = GetDecoMatriz(objModel.strIdSession);
                Claro.Web.Logging.Info("Session:" + objModel.strIdSession + ", Contrato:" + objModel.strIdContract, "LTE-MIGRACIÓN DE PLAN", "LTEChooseEquipmentByPlanLoad-OK");

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error("Session:" + objModel.strIdSession + ", Contrato:" + objModel.strIdContract, "LTE-MIGRACIÓN DE PLAN, LTEChooseEquipmentByPlanLoad", Claro.SIACU.Transac.Service.Functions.GetExceptionMessage(ex));
            }
            Claro.Web.Logging.Info("LTEChooseEquipmentByPlanLoad", "LTEPlanMigration- lstPlans", JsonConvert.SerializeObject(objLteChooseEquipmentByPlan));

            return Json(new { data = objLteChooseEquipmentByPlan });
        }
        private DecoMatrizResponse GetDecoMatriz(string strIdSession)
        {
            Claro.Web.Logging.Info(strIdSession, strIdSession, "In GetDecoMatriz - LTE");
            DecoMatrizResponse objDecoMatrizResponse = null;
            var lstDecos = new List<DecoMatriz>();
            string numDecosMax = "0";

            var audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);
            var objDecoMatrizRequest = new DecoMatrizRequest
            {
                audit = audit,
                strTipoProducto = "08"
            };

            try
            {
                objDecoMatrizResponse = Claro.Web.Logging.ExecuteMethod(() => { return _oServiceFixed.GetDecoMatriz(objDecoMatrizRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objDecoMatrizRequest.audit.transaction, Functions.GetExceptionMessage(ex));
                throw new Exception(audit.transaction);
            }

            if (objDecoMatrizResponse == null)
            {
                objDecoMatrizResponse.ListaMatrizDecos = new List<DecoMatriz>();
                objDecoMatrizResponse.CantidadMaxima = "0";
            }
            return objDecoMatrizResponse;
        }
        private LteChooseEquipmentByPlanLoadMessageModel GetMessageObjEquipment(string strIdSession)
        {

            var objLteChooseEquipmentByPlanLoadMessage = new LteChooseEquipmentByPlanLoadMessageModel();

            objLteChooseEquipmentByPlanLoadMessage.intQuantityMaxPoint =
                Convert.ToInt(GetValueConfig("CantidadMaximaPuntosLTE", strIdSession, "LTE-MIGRACIÓN DE PLAN-Config"));
            objLteChooseEquipmentByPlanLoadMessage.intQuantityDefaultHD = Convert.ToInt(GetValueConfig("CantidadXDefectoHDLTE", strIdSession, "LTE-MIGRACIÓN DE PLAN-Config"));
            objLteChooseEquipmentByPlanLoadMessage.intQuantityDefaultSD = Convert.ToInt(GetValueConfig("CantidadXDefectoSDLTE", strIdSession, "LTE-MIGRACIÓN DE PLAN-Config"));
            objLteChooseEquipmentByPlanLoadMessage.intQuantityDefaultDVR = Convert.ToInt(GetValueConfig("CantidadXDefectoDVRLTE", strIdSession, "LTE-MIGRACIÓN DE PLAN-Config"));
            objLteChooseEquipmentByPlanLoadMessage.strMessageQuantityEquipment = Functions.GetValueFromConfigFile("strMessageQuantityEquipment", KEY.AppSettings("strConstArchivoSIACUTLTEConfigMsg"));
            objLteChooseEquipmentByPlanLoadMessage.strMessageErrorValidationEquipment = Functions.GetValueFromConfigFile("strMessageErrorValidationEquipment", KEY.AppSettings("strConstArchivoSIACUTLTEConfigMsg"));
            objLteChooseEquipmentByPlanLoadMessage.strMessageQuantityXEquipment = Functions.GetValueFromConfigFile("strMessageQuantityXEquipment", KEY.AppSettings("strConstArchivoSIACUTLTEConfigMsg"));

            return objLteChooseEquipmentByPlanLoadMessage;
        }
        #endregion

        #region VALIDATION
        [HttpPost]
        public JsonResult GetAdditionalAndCoreEquipment(LteMigrationPlanRequest objModel)
        {
            var objEquipmentCoreAndCoreAdditional = new EquipmentCoreAndCoreAdditional();
            var lstServicePlan = new List<ServiceByPlan>();

            objEquipmentCoreAndCoreAdditional.lstEquipmentDecoCableAdditional = new List<ServiceByPlan>();
            objEquipmentCoreAndCoreAdditional.lstServicesByPlanCableCoreAddi = new List<ServiceByPlan>();
            objEquipmentCoreAndCoreAdditional.lstServicesByPlanInternetCoreAddi = new List<ServiceByPlan>();
            objEquipmentCoreAndCoreAdditional.lstServicesByPlanTelephoneCoreAddi = new List<ServiceByPlan>();
            objEquipmentCoreAndCoreAdditional.lstEquipmentInternetAndPhone = new List<ServiceByPlan>();
            objEquipmentCoreAndCoreAdditional.lstEquipmentDecoCableCore = new List<ServiceByPlan>();
            objEquipmentCoreAndCoreAdditional.lstEquipmentTotalCore = new List<ServiceByPlan>();
            objEquipmentCoreAndCoreAdditional.lstEquipmentCableNotDecos = new List<ServiceByPlan>();
            
            try
            {
                var arrLTEGroupCable = GetValueConfig("strLTEGroupCable", objModel.strIdSession, "LTE-CP-Config").Split(',');
                var arrLTEGroupInternet = GetValueConfig("strLTEGroupInternet", objModel.strIdSession, "LTE-CP-Config").Split(',');
                var arrLTEGroupTelephony = GetValueConfig("strLTEGroupTelephony", objModel.strIdSession, "LTE-CP-Config").Split(',');
                //var arrLTEServicesType = GetValueConfig("TipoServicioLTE", objModel.strIdSession, "LTE-CP-Config").Split(',');
                var arrLTEServicesTypeAdditionalCore = GetValueConfig("TipoServicioAdicionalCoreLTE", objModel.strIdSession, "LTE-CP-Config").Split(',');
                var arrLTEServicesTypeAdditional = GetValueConfig("TipoServicioAdicionalLTE", objModel.strIdSession, "LTE-CP-Config").Split(',');
                var arrLTEEquipmentType = GetValueConfig("TipoEquipoLTE", objModel.strIdSession, "LTE-CP-Config").Split(',');
                var arrLTEGroupEquipment = GetValueConfig("strValorCodGrupoEquiposCore", objModel.strIdSession, "LTE-CP-Config");

                var strHD = GetValueConfig("CodigoHDLTE", objModel.strIdSession, "LTE-CP-Config");
                var strSD = GetValueConfig("CodigoSDLTE", objModel.strIdSession, "LTE-CP-Config");
                var strDVR = GetValueConfig("CodigoDVRLTE", objModel.strIdSession, "LTE-CP-Config");
                var arrTypeEquipmentCore = new string[3] { strHD, strSD, strDVR };
                if (objModel.strIdInternet == null) objModel.strIdInternet = "";
                if (objModel.strIdPhone == null) objModel.strIdPhone = "";
                if (objModel.strIdCable == null) objModel.strIdCable = "";
                PlanServiceResponse objServicesResponse;
                PlanServiceRequest objServicesRequest = new PlanServiceRequest
                {
                    idplan = objModel.strIdPlan,
                    strTipoProducto = objModel.strTypeProduct,
                    audit = App_Code.Common.CreateAuditRequest<AuditRequestFixed>(objModel.strIdSession)
                };
                if (objModel.strIgv == null)
                {
                        var objIgv = GetCommonConsultIgv(objModel.strIdSession);
                        if (objIgv != null)
                            objModel.strIgv = (objIgv.igvD+1).ToString();
                        else
                            objModel.strIgv = (Convert.ToDouble(GetValueConfig("valorIGV", objModel.strIdSession, "LTE-CAMBIO DE PLAN-Config"))+1).ToString();
                }
               

                try
                {
                    objServicesResponse = Claro.Web.Logging.ExecuteMethod<PlanServiceResponse>(() =>
                    {
                        return new FixedTransacServiceClient().LteGetServicesByPlan(objServicesRequest);
                    });
                    if (objServicesResponse.listServicio != null)
                    {
                        var strSolution = objServicesResponse.listServicio[0].Solution;
                        lstServicePlan = (from x in objServicesResponse.listServicio
                                          where x.Solution == strSolution 
                                          select x).ToList(); ;
                        foreach (var objItem in lstServicePlan)
                        {
                            objItem.CfWithIgv=string.Format("{0:0.00}", Math.Round(Double.Parse(objItem.CF) * Double.Parse(objModel.strIgv), Claro.Constants.NumberTwo));
                        }
                        objEquipmentCoreAndCoreAdditional.lstEquipmentTotalCore = (from x in lstServicePlan
                            where x.CodEquipmentGroup == arrLTEGroupEquipment &&
                                  !String.IsNullOrEmpty(x.Equipment) &&
                                  (arrLTEGroupInternet.Contains(x.CodGroupServ) ||
                                   arrLTEGroupTelephony.Contains(x.CodGroupServ) ||
                                   arrLTEGroupCable.Contains(x.CodGroupServ)
                                  ) &&
                                  (objModel.strIdInternet == x.CodServSisact || objModel.strIdPhone == x.CodServSisact || objModel.strIdCable == x.CodServSisact)
                            select x).ToList();

                        objEquipmentCoreAndCoreAdditional.lstEquipmentTotalCore = (from ele in objEquipmentCoreAndCoreAdditional.lstEquipmentTotalCore
                                                                                      group ele by ele.Codtipequ
                                                                                      into groups
                                                                                      select groups.OrderBy(x => x.Codtipequ).First()).ToList();

                        objEquipmentCoreAndCoreAdditional.lstEquipmentVisitCore = (from x in objEquipmentCoreAndCoreAdditional.lstEquipmentTotalCore
                            where !arrLTEGroupCable.Contains(x.CodGroupServ) ||
                                  (arrLTEGroupCable.Contains(x.CodGroupServ) && arrTypeEquipmentCore.Contains(x.Codtipequ))
                            select x).ToList();
                        
                        objEquipmentCoreAndCoreAdditional.lstEquipmentCableAllAdditional = (from x in lstServicePlan
                            where arrLTEEquipmentType.Contains(x.CodGroupServ) && arrLTEServicesTypeAdditional.Contains(x.CodServiceType)
                            select x).ToList();

                        objEquipmentCoreAndCoreAdditional.lstEquipmentDecoCableAdditional = (from x in objEquipmentCoreAndCoreAdditional.lstEquipmentCableAllAdditional
                            where arrTypeEquipmentCore.Contains(x.Codtipequ)
                            select x).ToList();
                        var lstService = (from ele in lstServicePlan
                            group ele by ele.CodServSisact
                            into groups
                            select groups.OrderBy(x => x.CodServiceType).First()).ToList();

                        objEquipmentCoreAndCoreAdditional.lstServicesByPlanCableCoreAddi = (from x in lstService
                            where arrLTEGroupCable.Contains(x.CodGroupServ) &&
                                  arrLTEServicesTypeAdditionalCore.Contains(x.CodServiceType)
                            select x).ToList();

                        objEquipmentCoreAndCoreAdditional.lstServicesByPlanInternetCoreAddi = (from x in lstService
                            where arrLTEGroupInternet.Contains(x.CodGroupServ) &&
                                  arrLTEServicesTypeAdditionalCore.Contains(x.CodServiceType)
                            select x).ToList();

                        objEquipmentCoreAndCoreAdditional.lstServicesByPlanTelephoneCoreAddi = (from x in lstService
                            where arrLTEGroupTelephony.Contains(x.CodGroupServ) &&
                                  arrLTEServicesTypeAdditionalCore.Contains(x.CodServiceType)
                            select x).ToList();

                    }


                }
                catch (Exception ex)
                {
                    objServicesResponse = null;
                    Claro.Web.Logging.Error("Session:" + objModel.strIdSession + ", Contrato:" + objModel.strIdContract, "LTE-MIGRACIÓN DE PLAN, GetAdditionalEquipment", Claro.SIACU.Transac.Service.Functions.GetExceptionMessage(ex));
                }
                Claro.Web.Logging.Info("Session:" + objModel.strIdSession + ", Contrato:" + objModel.strIdContract, "LTE-MIGRACIÓN DE PLAN", "GetAdditionalEquipment-OK");

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error("Session:" + objModel.strIdSession + ", Contrato:" + objModel.strIdContract, "LTE-MIGRACIÓN DE PLAN, GetAdditionalEquipment", Claro.SIACU.Transac.Service.Functions.GetExceptionMessage(ex));
            }
            Claro.Web.Logging.Info("Contrato:" + objModel.strIdContract, "LTE-MIGRACIÓN DE PLAN-GetAdditionalAndCoreEquipment9", JsonConvert.SerializeObject(objEquipmentCoreAndCoreAdditional));

            return Json(new { data = objEquipmentCoreAndCoreAdditional });
        }

        public JsonResult GetTechnicalVisitResult(LteMigrationPlanRequest objModel)
        {
            string strLTEGroupCable = ConfigurationManager.AppSettings("strLTEGroupCable").ToString();
            string strLTEGroupInternet = ConfigurationManager.AppSettings("strLTEGroupInternet").ToString();
            string strLTEGroupTelephony = ConfigurationManager.AppSettings("strLTEGroupTelephony").ToString();
            var lstServicesFinal = new List<ServiceByPlan>();
            if (objModel.lstAdditionalEquipmentQuantity == null) objModel.lstAdditionalEquipmentQuantity = new List<ServiceByPlan>();
            if (objModel.lstEquipmentVisitCore == null) objModel.lstEquipmentVisitCore = new List<ServiceByPlan>();
            var strCodGroupServ = String.Empty;
            foreach (ServiceByPlan sbp in objModel.lstEquipmentVisitCore)
            {
                lstServicesFinal.Add(sbp);
                if (strLTEGroupCable.Contains(sbp.CodGroupServ))
                    strCodGroupServ = sbp.CodGroupServ;
            }
            foreach (ServiceByPlan sbp in objModel.lstAdditionalEquipmentQuantity)
            {
                sbp.CodGroupServ=strCodGroupServ;
                lstServicesFinal.Add(sbp);
            }

            for (int i = 0; i < lstServicesFinal.Count; i++)
            {
                if (strLTEGroupCable.Contains(lstServicesFinal[i].CodGroupServ))
                {
                    lstServicesFinal[i].ServiceType = Constant.CAMBIO_DE_PLAN.TipoServicio_Cable;
                }
                else if (strLTEGroupInternet.Contains(lstServicesFinal[i].CodGroupServ))
                {
                    lstServicesFinal[i].ServiceType = Constant.CAMBIO_DE_PLAN.TipoServicio_Internet;
                }
                else if (strLTEGroupTelephony.Contains(lstServicesFinal[i].CodGroupServ))
                {
                    lstServicesFinal[i].ServiceType = Constant.CAMBIO_DE_PLAN.TipoServicio_Telefonia;
                }
            }

            string strFieldSeparator = oTransacServ.Constants.PresentationLayer.fieldSeparator;
            string strTrama = string.Empty;
            string strTramaLog = string.Empty;
            foreach (var item in lstServicesFinal)
            {
                if (Functions.CheckInt(item.CantEquipment) > 0)
                {
                    strTrama = strTrama + item.CodServSisact + strFieldSeparator + item.Sncode + strFieldSeparator + item.CodGroupServ + strFieldSeparator + item.Codtipequ +
                               strFieldSeparator + item.Tipequ
                               + strFieldSeparator + item.IDEquipment + strFieldSeparator + item.CantEquipment + strFieldSeparator + item.ServiceType + ";";
                    strTramaLog = strTramaLog + "CodServSisact:" + item.CodServSisact + ",Sncode:" + item.Sncode + ",CodGroupServ:" + item.CodGroupServ + ",Codtipequ:" + item.Codtipequ +
                                  ",Tipequ:" + item.Tipequ
                                  + ",IDEquipment:" + item.IDEquipment + ",CantEquipment:" + item.CantEquipment + ",ServiceType:" + item.ServiceType + ";";
                }
            }
            Claro.Web.Logging.Info("Session:" + objModel.strIdSession + ", Contrato:" + objModel.strIdContract, "LTE-MIGRACIÓN DE PLAN", "GetTechnicalVisitResult-Trama:" + strTrama);
            Claro.Web.Logging.Info("Session:" + objModel.strIdSession + ", Contrato:" + objModel.strIdContract, "LTE-MIGRACIÓN DE PLAN", "GetTechnicalVisitResult-TramaLog:" + strTramaLog);

            TechnicalVisitResultRequest oRequest = new TechnicalVisitResultRequest
            {
                strCoId = objModel.strIdContract,
                strCustomerId = objModel.strCustomerId,
                strCodPlanSisact = objModel.strCodPlanSisact,
                strTmCode = objModel.strTmCode,
                strTrama = strTrama,
                audit = App_Code.Common.CreateAuditRequest<AuditRequestFixed>(objModel.strIdSession)
            };
            var objResponse = new TechnicalVisitResultResponse();
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<TechnicalVisitResultResponse>(() =>
                {
                    return new FixedTransacServiceClient().GetTechnicalVisitResult(oRequest);
                });
                if (Convert.ToInt(objResponse.Result.Anerror) < oTransacServ.Constants.numeroCero)
                {
                    Claro.Web.Logging.Info("Session:" + objModel.strIdSession + ", Contrato:" + objModel.strIdContract, "LTE-MIGRACIÓN DE PLAN", "GetTechnicalVisitResult-Error en el servicio de respuesta-" + objResponse.Result.Averror);
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info("Session:" + objModel.strIdSession + ", Contrato:" + objModel.strIdContract, "LTE-MIGRACIÓN DE PLAN", "GetTechnicalVisitResult-Excepcion:" + ex.Message);
            }

            return Json(new { data = objResponse.Result });
        }

        #endregion

        #region Save Migrated Plan
        [HttpPost]
        public JsonResult SaveMigratedPlan(LteMigrationPlanSaveModel objLteMigrationPlanSaveModel)
        {
            Claro.Web.Logging.Info("LTESaveMigratedPlan", "Inicio", JsonConvert.SerializeObject(objLteMigrationPlanSaveModel));
            AuditRequestFixed objAuditRequest = App_Code.Common.CreateAuditRequest<AuditRequestFixed>(objLteMigrationPlanSaveModel.strIdSession);
            ExecutePlanMigrationLTEResponse objResponse = new ExecutePlanMigrationLTEResponse();
            ExecutePlanMigrationLTERequest objRequest = new ExecutePlanMigrationLTERequest();
            if (objLteMigrationPlanSaveModel.lstAdditionalEquipmentQuantity == null)
                objLteMigrationPlanSaveModel.lstAdditionalEquipmentQuantity = new List<ServiceByPlan>();
            if (objLteMigrationPlanSaveModel.lstEquipmentCableAllAdditional == null)
                objLteMigrationPlanSaveModel.lstEquipmentCableAllAdditional = new List<ServiceByPlan>();
            var lstServicesContrat = new List<RegServiciosType>();
            var lstCoser = new List<Coser>();
            GetServiceByContrat(objLteMigrationPlanSaveModel.lstServices, objLteMigrationPlanSaveModel.lstAdditionalEquipmentQuantity, objLteMigrationPlanSaveModel.strIdContract, ref lstServicesContrat,ref lstCoser);

            var lstServicesAndEquipment = new List<ServiceByPlan>();
            var lstServicesAndEquipmentTipification = new List<ServiceByPlan>();
            GetServiceAndEquipment(objLteMigrationPlanSaveModel.lstServices, objLteMigrationPlanSaveModel.lstAdditionalEquipmentQuantity, objLteMigrationPlanSaveModel.lstEquipmentTotalCore, objLteMigrationPlanSaveModel.lstEquipmentCableAllAdditional, ref lstServicesAndEquipment, ref lstServicesAndEquipmentTipification);

            objLteMigrationPlanSaveModel.strConstanceXml = CreateDictionaryConstancyXML(objLteMigrationPlanSaveModel.strIdSession, objLteMigrationPlanSaveModel.lstPDFConstancyParameters, lstServicesAndEquipmentTipification);

            if (objLteMigrationPlanSaveModel.objItemTypification == null)
            {
                Claro.Web.Logging.Info("LTESaveMigratedPlan", "LTEPlanMigration", "LTESaveMigratedPlan_tipificacion_llega_nulo");
            }

            Claro.Web.Logging.Info("LTESaveMigratedPlan", "LTEPlanMigration", "LTESaveMigratedPlan_antes_de_crear_llave_gConstKeyCustomerInteract");
            string strmsisdn = ConfigurationManager.AppSettings("gConstKeyCustomerInteract").ToString() + objLteMigrationPlanSaveModel.strIdCustomer;
            var strcontactObjId = String.Empty;
            try
            {
                strcontactObjId = new CommonServicesController().GetOBJID(objLteMigrationPlanSaveModel.strIdSession, strmsisdn);

            }
            catch (Exception e)
            {
                
            }
            try
            {
                ClientParameters objClientParameters = new ClientParameters()
                {
                    strflagReg = Claro.SIACU.Transac.Service.Constants.PresentationLayer.NumeracionUNO,
                    straccount = string.Empty,
                    strmsisdn = strmsisdn,
                    strcontactObjId = strcontactObjId
                };
                Claro.Web.Logging.Info("LTESaveMigratedPlan", "LTEPlanMigration", "LTESaveMigratedPlan_despues_de_loggear_oClientParameters");

                #region Plantilla
                MainParameters objMainParameters = new MainParameters()
                {
                    strType = objLteMigrationPlanSaveModel.objItemTypification.TIPO,
                    strClass = objLteMigrationPlanSaveModel.objItemTypification.CLASE,
                    strSubClass = objLteMigrationPlanSaveModel.objItemTypification.SUBCLASE,
                    strContactMethod = ConfigurationManager.AppSettings("MetodoContactoTelefonoDefault"),
                    strInterType = ConfigurationManager.AppSettings("AtencionDefault"),
                    strAgent = objLteMigrationPlanSaveModel.strLogin,
                    strUserProcess = ConfigurationManager.AppSettings("USRProcesoSU"),
                    strMadeInOne = Claro.SIACU.Transac.Service.Constants.PresentationLayer.NumeracionCERO,
                    strNotes = objLteMigrationPlanSaveModel.strNotes,
                    strFlagCase = Claro.SIACU.Transac.Service.Constants.PresentationLayer.NumeracionCERO,
                    strResult = ConfigurationManager.AppSettings("Ninguno"),
                    strServAfect = string.Empty,
                    strInconven = string.Empty,
                    strServAfectCode = string.Empty,
                    strInconvenCode = string.Empty,
                    strCoId = objLteMigrationPlanSaveModel.strIdContract,
                    strCodPlan = objLteMigrationPlanSaveModel.strCodPlan,
                    strValueOne = string.Empty,
                    trValueTwo = string.Empty
                };

                Claro.Web.Logging.Info("LTESaveMigratedPlan", "LTEPlanMigration", "LTESaveMigratedPlan_despues_de_loggear_oMainParameters");

                PlusParameters objPlusParameters = new PlusParameters()
                {
                    strInter1 = objLteMigrationPlanSaveModel.strBillingCycle,
                    strInter3 = objLteMigrationPlanSaveModel.strActivationDate,
                    strInter4 = objLteMigrationPlanSaveModel.strTermContract,
                    strInter5 = objLteMigrationPlanSaveModel.strStateLine,
                    strInter6 = objLteMigrationPlanSaveModel.strExpirationDate,
                    strInter7 = objLteMigrationPlanSaveModel.strOfficeAddress,
                    strInter8 = objLteMigrationPlanSaveModel.lstPDFConstancyParameters[Constant.numeroCatorce],
                    strInter15 = objLteMigrationPlanSaveModel.strCacDac,
                    strInter16 = objLteMigrationPlanSaveModel.strLegalAddress,
                    strInter17 = objLteMigrationPlanSaveModel.strLegalDistrict,
                    strInter18 = objLteMigrationPlanSaveModel.strLegalCountry,
                    strInter19 = objLteMigrationPlanSaveModel.strLegalProvince,
                    strInter20 = objLteMigrationPlanSaveModel.strPlaneCodeInstallation,
                    strInter21 = objLteMigrationPlanSaveModel.strNewPlan, // NewPlan
                    strInter29 = string.Empty,
                    strInter30 = objLteMigrationPlanSaveModel.strNotes,
                    strAmountUnit = objLteMigrationPlanSaveModel.strLegalUrbanization,
                    strBirthday = DateTime.Now.ToString(oTransacServ.Constants.PresentationLayer.dateDefaultFormat),
                    strClarifyInteraction = "",
                    strClaroLdn1 = objLteMigrationPlanSaveModel.strDocumentNumber,
                    strClaroLdn2 = (objLteMigrationPlanSaveModel.bolSendEmail==true?"1":"0") ,
                    strFirstName = objLteMigrationPlanSaveModel.strFullName,
                    strNameLegalRep = objLteMigrationPlanSaveModel.strLegalAgent,
                    strOldClaroLdn2 = objLteMigrationPlanSaveModel.strNewSolution, // Solution
                    strOldClaroLdn3 = objLteMigrationPlanSaveModel.strPresuscritoStatus,
                    strOldClaroLdn4 = objLteMigrationPlanSaveModel.strNoLetter,
                    strOldClaroLocal1 = objLteMigrationPlanSaveModel.strDdlOperator,
                    strOldClaroLocal2 = objLteMigrationPlanSaveModel.strPublishFinalStatus,
                    strOldClaroLocal3 = objLteMigrationPlanSaveModel.strRefound,
                    strOldClaroLocal4 = objLteMigrationPlanSaveModel.strLoyalityAmount,
                    strOldClaroLocal5 = objLteMigrationPlanSaveModel.strTotalPenalty,
                    strOldClaroLocal6 = objLteMigrationPlanSaveModel.strFinalLoyalityStatus,
                    strOldFirstName = objLteMigrationPlanSaveModel.strOCCFinalStatus,
                    strOtherPhone = objLteMigrationPlanSaveModel.strDocumentType,
                    strPhoneLegalRep = objLteMigrationPlanSaveModel.strValidateETAStatus,
                    strReferencePhone = objLteMigrationPlanSaveModel.strTelephone,
                    strReason = objLteMigrationPlanSaveModel.strCustomerContact,
                    strRegistrationReason = objLteMigrationPlanSaveModel.strIdContract,
                    strBasket = objLteMigrationPlanSaveModel.strPlan,
                    strExpireDate = DateTime.Now.ToString(oTransacServ.Constants.PresentationLayer.dateDefaultFormat),
                    strCity = DateTime.Now.ToShortDateString(),
                    strOccupation = objLteMigrationPlanSaveModel.strExistCorePhoneService,
                    strPosition = objLteMigrationPlanSaveModel.strFProgrammming,
                    strTypeDocument = objLteMigrationPlanSaveModel.strCustomerType,
                    strZipCode = objLteMigrationPlanSaveModel.spnNewTotalFixedChargeCIGV,
                    strEmail = objLteMigrationPlanSaveModel.strEmail,
                    strAddress = objLteMigrationPlanSaveModel.strAddressReference
                };
                Claro.Web.Logging.Info("LTESaveMigratedPlan", "LTEPlanMigration", "LTESaveMigratedPlan_despues_de_loggear_PlusParameters");
                #endregion

                #region Parametros SOT

                string fieldSeparator = Constant.PresentationLayer.gstrVariablePipeline;
                string fieldSeparatorEnd = Constant.PresentationLayer.gstrPuntoyComa;
                var strHeader = new System.Text.StringBuilder();
                strHeader.Append(objLteMigrationPlanSaveModel.strIdContract);
                strHeader.Append(fieldSeparator);
                strHeader.Append(objLteMigrationPlanSaveModel.strIdCustomer);
                strHeader.Append(fieldSeparator);
                strHeader.Append("{idInteraccion}");
                strHeader.Append(fieldSeparator);
                strHeader.Append(ConfigurationManager.AppSettings("gConstKeyTipoTranMPLTE"));
                strHeader.Append(fieldSeparator);
                strHeader.Append(ConfigurationManager.AppSettings("strTiptraLTE").ToString());
                strHeader.Append(fieldSeparator);
                strHeader.Append("{idInteraccion}");
                strHeader.Append(fieldSeparator);
                strHeader.Append(objLteMigrationPlanSaveModel.strCodMoTot);
                strHeader.Append(fieldSeparator);
                strHeader.Append(string.Empty);
                strHeader.Append(fieldSeparator);
                strHeader.Append(string.Empty);
                strHeader.Append(fieldSeparator);
                strHeader.Append(string.Empty);
                strHeader.Append(fieldSeparator);
                strHeader.Append(string.Empty);
                strHeader.Append(fieldSeparator);
                strHeader.Append(string.Empty);
                strHeader.Append(fieldSeparator);
                strHeader.Append(string.Empty);
                strHeader.Append(fieldSeparator);
                strHeader.Append(string.Empty);
                strHeader.Append(fieldSeparator);
                strHeader.Append(string.Empty);
                strHeader.Append(fieldSeparator);
                strHeader.Append(string.Empty);
                strHeader.Append(fieldSeparator);
                strHeader.Append(string.Empty);
                strHeader.Append(fieldSeparator);
                strHeader.Append(string.Empty);
                strHeader.Append(fieldSeparator);
                strHeader.Append(string.Empty);
                strHeader.Append(fieldSeparator);
                strHeader.Append(string.Empty);
                strHeader.Append(fieldSeparator);
                strHeader.Append(ConfigurationManager.AppSettings("strFranjaHorLTE").ToString());
                strHeader.Append(fieldSeparator);
                strHeader.Append(objLteMigrationPlanSaveModel.strNoLetter);
                strHeader.Append(fieldSeparator);
                strHeader.Append(Convert.ToInt(objLteMigrationPlanSaveModel.strDdlOperatorStatus).ToString());
                strHeader.Append(fieldSeparator);
                strHeader.Append(objLteMigrationPlanSaveModel.strPresuscritoStatus);
                strHeader.Append(fieldSeparator);
                strHeader.Append(objLteMigrationPlanSaveModel.strPublishFinalStatus);
                strHeader.Append(fieldSeparator);
                strHeader.Append(objLteMigrationPlanSaveModel.strHdnListFTMCode);
                strHeader.Append(fieldSeparator);
                strHeader.Append(objLteMigrationPlanSaveModel.strLogin);
                strHeader.Append(fieldSeparator);
                strHeader.Append(string.Empty);
                strHeader.Append(fieldSeparator);
                strHeader.Append(string.Empty);
                strHeader.Append(fieldSeparator);
                strHeader.Append(ConfigurationManager.AppSettings("strTipoLTEMigrationPlan").ToString());
                strHeader.Append(fieldSeparator);
                strHeader.Append(string.Empty);
                strHeader.Append(fieldSeparator);
                strHeader.Append(objLteMigrationPlanSaveModel.strProductType);
                strHeader.Append(fieldSeparator);
                strHeader.Append(Functions.GetValueFromConfigFile("strConstCodOCCTope", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig")));
                strHeader.Append(fieldSeparator);
                strHeader.Append(string.Empty);
                strHeader.Append(fieldSeparator);
                strHeader.Append(objLteMigrationPlanSaveModel.dblConsumeCapAmount);
                strHeader.Append(fieldSeparator);
                strHeader.Append("{idContrato},{idInteraccion}," + DateTime.Now.ToShortDateString());
                strHeader.Append(fieldSeparator);
                strHeader.Append(objLteMigrationPlanSaveModel.dblConsumeCap);
                strHeader.Append(fieldSeparator);
                strHeader.Append(Functions.GetValueFromConfigFile("srtCodServicioTope", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig")));
                strHeader.Append(fieldSeparator);
                strHeader.Append(objLteMigrationPlanSaveModel.dblCreditLimit);
                strHeader.Append(fieldSeparator);
                strHeader.Append(objLteMigrationPlanSaveModel.strAnotation);
                strHeader.Append(fieldSeparator);
                strHeader.Append(objLteMigrationPlanSaveModel.strNotes);
                strHeader.Append(fieldSeparatorEnd);
                strHeader.Append(fieldSeparator);
                strHeader.Append(objLteMigrationPlanSaveModel.intFlagConsumeCap);
                strHeader.Append(fieldSeparator);
                strHeader.Append(Constant.numeroCero);

                var strBody = new System.Text.StringBuilder();
                foreach (var item in lstServicesAndEquipment)
                {
                    strBody.Append(item.CodServSisact);
                    strBody.Append(fieldSeparator);
                    strBody.Append(item.CodPrincipalGroup);
                    strBody.Append(fieldSeparator);
                    strBody.Append(item.CodGroupServ);
                    strBody.Append(fieldSeparator);
                    strBody.Append(item.CantEquipment);
                    strBody.Append(fieldSeparator);
                    strBody.Append(item.DesServSisact);
                    strBody.Append(fieldSeparator);
                    strBody.Append(string.Empty);
                    strBody.Append(fieldSeparator);
                    strBody.Append(item.IdLineQuantity);
                    strBody.Append(fieldSeparator);
                    strBody.Append(item.Tipequ);
                    strBody.Append(fieldSeparator);
                    strBody.Append(item.Codtipequ);
                    strBody.Append(fieldSeparator);
                    strBody.Append(item.Quantity);
                    strBody.Append(fieldSeparator);
                    strBody.Append(item.Dscequ);
                    strBody.Append(fieldSeparator);
                    strBody.Append(string.Empty);
                    strBody.Append(fieldSeparator);
                    strBody.Append(DateTime.Now);
                    strBody.Append(fieldSeparator);
                    strBody.Append(objLteMigrationPlanSaveModel.strLogin);
                    strBody.Append(fieldSeparator);
                    strBody.Append(item.Sncode);
                    strBody.Append(fieldSeparator);
                    strBody.Append(item.Spcode);
                    strBody.Append(fieldSeparator);
                    strBody.Append(item.CF);
                    strBody.Append(fieldSeparator);
                    strBody.Append(string.Empty);
                    strBody.Append(fieldSeparator);
                    strBody.Append(string.Empty);
                    strBody.Append(fieldSeparator);
                    strBody.Append(string.Empty);
                    strBody.Append(fieldSeparator);
                    strBody.Append(string.Empty);
                    strBody.Append(fieldSeparatorEnd);
                };

                var strLstTipequ = new System.Text.StringBuilder();
                var strLstCoser = new System.Text.StringBuilder();
                var strLstSnCode = new System.Text.StringBuilder();
                var strLstSpCode = new System.Text.StringBuilder();
                int intLstServicesLength = lstServicesAndEquipment.Count;

                for (var i = 0; i < intLstServicesLength; i++)
                {
                    strLstTipequ.Append(lstServicesAndEquipment[i].Tipequ);
                    strLstCoser.Append(lstServicesAndEquipment[i].CodServSisact);
                    strLstSnCode.Append(lstServicesAndEquipment[i].Sncode);
                    strLstSpCode.Append(lstServicesAndEquipment[i].Spcode);
                    if ((i + 1) < intLstServicesLength)
                    {
                        strLstTipequ.Append(fieldSeparator);
                        strLstCoser.Append(fieldSeparator);
                        strLstSnCode.Append(fieldSeparator);
                        strLstSpCode.Append(fieldSeparator);
                    }
                }

                SotParametersLTE objSotParametersLTE = new SotParametersLTE()
                {
                    strTramaCab = strHeader.ToString(),
                    strLstTipEqu = strLstTipequ.ToString(),
                    strLstCoser = strLstCoser.ToString(),
                    strLstSnCode = strLstSnCode.ToString(),
                    strLstSpCode = strLstSpCode.ToString(),
                    strTramaBody = strBody.ToString(),
                };
                Claro.Web.Logging.Info("LTESaveMigratedPlan", "LTEPlanMigration", "LTESaveMigratedPlan_despues_de_loggear_SotParameters");
                #endregion

                #region ParametrosCONTRATO
                List<Campo> objCampos = new List<Campo>() { new Campo { strIndice = "28", strTipo = "1", strValor = ConfigurationManager.AppSettings("gPrefijoCodigoPlanLTE").ToString().Trim() + objLteMigrationPlanSaveModel.strHdnCodPlan } };
                InformacionContrato InformacionContrato = new InformacionContrato()
                {
                    coId = objLteMigrationPlanSaveModel.strIdContract,
                    Campos = objCampos
                };

                ContractElement objContractElement = new ContractElement()
                {
                    strPlanTarifario = objLteMigrationPlanSaveModel.strHdnListFTMCode,
                    strIdSubmercado = ConfigurationManager.AppSettings("strConstIDSubmercadoCrearContratoLTE"),
                    strIdMercado = ConfigurationManager.AppSettings("strConstIDMercadoCrearContratoLTE"),
                    strRed = ConfigurationManager.AppSettings("strConstRedCrearContratoLTE"),
                    strEstadoUmbral = ConfigurationManager.AppSettings("strConstEstadoUmbralCrearContratoLTE"),
                    strCantidadUmbral = ConfigurationManager.AppSettings("strConstCantidadUmbralCrearContratoLTE"),
                    strArchivoLlamadas = ConfigurationManager.AppSettings("strConstArchivoLlamadasCrearContratoLTE"),
                    ListServices = lstServicesContrat,//aqui
                    ActualizacionContrato = new ActualizacionContrato() { strRazon = ConfigurationManager.AppSettings("lngConstRazonActualizacionContratoLTE") },
                    InformacionContrato = InformacionContrato
                };
                Claro.Web.Logging.Info("LTESaveMigratedPlan", "LTEPlanMigration", "LTESaveMigratedPlan_despues_de_loggear_oContractElement");

                Contract objContract = new Contract()
                {
                    strIpAplicacion = Common.GetApplicationIp(),
                    strNombreAplicacion = ConfigurationManager.AppSettings("gConstTipoHFCLTE"),
                    strTipoPostpago = ConfigurationManager.AppSettings("strConstTipoPostpagoCrearContratoLTE"),
                    ContractList = new List<ContractElement>() { objContractElement }
                };
                Claro.Web.Logging.Info("LTESaveMigratedPlan", "LTEPlanMigration", "LTESaveMigratedPlan_despues_de_loggear_Contract");

                ActualizarTipificacion ActualizarTipificacion = new ActualizarTipificacion()
                {
                    Orden = Claro.SIACU.Transac.Service.Constants.strLetraI
                };

                bool FlagContingencia = System.Convert.ToBoolean(ConfigurationManager.AppSettings("FlagContingenciaLTE").ToString());
                bool FlagCrearPlantilla = System.Convert.ToBoolean(ConfigurationManager.AppSettings("FlagCrearPlantillaLTE").ToString());
                #endregion

                AuditRegister AuditRegister = new AuditRegister()
                {
                    strIdTransaccion = objAuditRequest.transaction,
                    strServicio = ConfigurationManager.AppSettings("gConstEvtServicio"),
                    strIpCliente = Common.GetClientIP(),
                    strNombreCliente = objLteMigrationPlanSaveModel.strFullName,
                    strIpServidor = Common.GetApplicationIp(),
                    strNombreServidor = Common.GetApplicationName(),
                    strMonto = Claro.SIACU.Transac.Service.Constants.PresentationLayer.NumeracionCERO,
                    strCuentaUsuario = objLteMigrationPlanSaveModel.strLogin,
                    strTelefono = objLteMigrationPlanSaveModel.strIdCustomer,
                    strTexto = String.Empty
                };
                Claro.Web.Logging.Info("LTESaveMigratedPlan", "LTEPlanMigration", "LTESaveMigratedPlan_despues_de_loggear_AuditRegister");

                bool FlagValidaEta = System.Convert.ToBoolean(ConfigurationManager.AppSettings("FlagValidaEtaLTE").ToString());
                string ParametrosConstancia = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + (objLteMigrationPlanSaveModel.strConstanceXml == null ? "" : objLteMigrationPlanSaveModel.strConstanceXml);
                string DestinatarioCorreo = objLteMigrationPlanSaveModel.strEmail;
                string Notes = objLteMigrationPlanSaveModel.strNotes;

                objRequest = new ExecutePlanMigrationLTERequest()
                {
                    strTransactionId = objAuditRequest.transaction,
                    lstServicesList = lstServicesAndEquipmentTipification,
                    objTipification = objLteMigrationPlanSaveModel.objItemTypification,
                    objClientParameters = objClientParameters,
                    objMainParameters = objMainParameters,
                    objPlusParameters = objPlusParameters,
                    objEtaSelection = new EtaSelection(),
                    objSotParametersLTE = objSotParametersLTE,
                    objEtaParameters = new EtaParameters(),
                    objContract = objContract,
                    objActualizarTipificacion = ActualizarTipificacion,
                    blFlagContingencia = FlagContingencia,
                    blFlagCrearPlantilla = FlagCrearPlantilla,
                    objAuditRegister = AuditRegister,
                    lstListCoser = lstCoser,
                    blFlagValidaEta = FlagValidaEta,
                    strParametrosConstancia = ParametrosConstancia,
                    strDestinatarioCorreo = DestinatarioCorreo,
                    strNotes = Notes,
                    strTipoPlan = objLteMigrationPlanSaveModel.strHdnCodPlan,
                    strCodPlan = objLteMigrationPlanSaveModel.strPlan,
                    strTmCode = objLteMigrationPlanSaveModel.strTmCode,
                    strTipoProducto = objLteMigrationPlanSaveModel.strProductType,
                    audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(objLteMigrationPlanSaveModel.strIdSession),
                    strCodServicioGeneralTope = Functions.GetValueFromConfigFile("srtCodServicioTope", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig")),
                    dblMontoTopeConsumo = objLteMigrationPlanSaveModel.dblConsumeCapAmount,
                    dblTopeConsumo = objLteMigrationPlanSaveModel.dblConsumeCap,
                    strComentTopeConsumo = objLteMigrationPlanSaveModel.strConsumeCapComment,
                    dblLimiteCredito = objLteMigrationPlanSaveModel.dblCreditLimit,
                    strAnotacionToa = objLteMigrationPlanSaveModel.strAnotation,
                    strCodOCCTope = Functions.GetValueFromConfigFile("strConstCodOCCTope", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig"))
                };
                Claro.Web.Logging.Info("LTESaveMigratedPlan", "LTEPlanMigration", "LTESaveMigratedPlan_despues_de_loggear_oMigratedPlanRequestLte:" + JsonConvert.SerializeObject(objRequest));
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objAuditRequest.Session, objAuditRequest.transaction, "TR01 - " + Claro.SIACU.Transac.Service.Functions.GetExceptionMessage(ex));
            }

            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<ExecutePlanMigrationLTEResponse>(() =>
                {
                    return new FixedTransacServiceClient().ExecutePlanMigrationLTE1(objRequest);
                });

                Claro.Web.Logging.Info("LTESaveMigratedPlan", "LTEPlanMigration", JsonConvert.SerializeObject(objResponse));
                if (objResponse.result.result == null)
                    objResponse.result.result = string.Empty;
                if (objResponse.result.Code == null)
                    objResponse.result.Code = -100;
                if (objResponse.result.result == string.Empty || objResponse.result.Code < 0)
                    objResponse.result.result = Functions.GetValueFromConfigFile("strTextoErrorSiacu001", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                if (objResponse.result.ConstancyRoute == null)
                    objResponse.result.ConstancyRoute = string.Empty;

                Claro.Web.Logging.Info("LTESaveMigratedPlan", "Result", JsonConvert.SerializeObject(objResponse.result));

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objAuditRequest.Session, objAuditRequest.transaction, "TR02 - " + Claro.SIACU.Transac.Service.Functions.GetExceptionMessage(ex));
            }
            Claro.Web.Logging.Info("LTEListServicesByPlanWithEquipment", "LTEPlanMigration", "Fin ListServicesByPlanWithEquipment");
            return Json(new { data = objResponse.result });
        }

        private string CreateDictionaryConstancyXML(string strIdSession, List<string> listParamConstancyPDF, List<ServiceByPlan> lstServices)
        {
            var xmlGenerated = new StringBuilder();
            FixedTransacService.AuditRequest straudit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);
            try
            {
                var listLabels = GetXmlToString(App_Code.Common.GetApplicationRoute() + "/DataTransac/LTEConstance.xml");
                var count = 0;
                xmlGenerated.Append("<PLANTILLA>\r\n");
                foreach (var key in listLabels)
                {
                    if (count < listParamConstancyPDF.Count)
                        xmlGenerated.Append(string.Format("<{0}>{1}</{2}>\r\n", key, listParamConstancyPDF[count], key));
                    count++;
                }
                foreach (var item in lstServices)
                {
                    xmlGenerated.Append("<NOMBRE_SERVICIO_GRILLA>" + item.DesServSisact + "</NOMBRE_SERVICIO_GRILLA>\r\n ");
                    xmlGenerated.Append("<TIPO_SERVICIO_GRILLA>" + item.ServiceType + "</TIPO_SERVICIO_GRILLA>\r\n ");
                    xmlGenerated.Append("<GRUPO_SERVICIO_GRILLA>" + item.GroupServ + "</GRUPO_SERVICIO_GRILLA>\r\n ");
                    xmlGenerated.Append("<CF_TOTAL_IGV_GRILLA>" + item.CF + "</CF_TOTAL_IGV_GRILLA>\r\n ");
                }
                xmlGenerated.Append("<CONTENIDO_COMERCIAL>" + Functions.GetValueFromConfigFile("PlanMigrationContentCommercial",
                                        Claro.ConfigurationManager.AppSettings("strConstArchivoSIACPOConfigMsg")) + "</CONTENIDO_COMERCIAL>\r\n ");
                xmlGenerated.Append("<CONTENIDO_COMERCIAL2>" + Functions.GetValueFromConfigFile("PlanMigrationContentCommercial2",
                                        Claro.ConfigurationManager.AppSettings("strConstArchivoSIACPOConfigMsg")) + "</CONTENIDO_COMERCIAL2>\r\n ");

                xmlGenerated.Append("</PLANTILLA>");
                return xmlGenerated.ToString();
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(straudit.Session, straudit.transaction, ex.Message);
            }
            return xmlGenerated.ToString();
        }

        private string ChangeServiceType(string strServiceType)
        {
            if (!strServiceType.Trim().ToUpper().Equals("ADICIONAL"))
                return "INCLUÍDO";
            else return "ADICIONAL";
        }
        private void GetServiceByContrat(List<ServiceByPlan> lstServices, List<ServiceByPlan> lstDecoAdditional, string strIdContract, ref List<RegServiciosType> lstServicesContratAllService, ref List<Coser> lstCoser)
                {
            try
            {
                var lstServiceAll = new List<ServiceByPlan>();
                lstServiceAll = lstServiceAll.Concat(lstServices).ToList();
                lstServiceAll = lstServiceAll.Concat(lstDecoAdditional).ToList();
                lstCoser = (from ele in lstServiceAll
                    select new Coser
                {
                        strCargoFijo = ele.CF,
                        strSnCode = ele.Sncode,
                        strSpCode = ele.Spcode,
                        strTipoServicio = ele.ServiceType,
                        strPeriodos = String.Empty
                    }).ToList();
                lstServicesContratAllService = (from ele in lstServiceAll
                    select new RegServiciosType
                    {
                        strCoId = strIdContract,
                        strCodGroupServ = ele.CodGroupServ,
                        strSnCode = ele.Sncode,
                        strSpCode = ele.Spcode,
                        strTipEqu = ele.Tipequ,
                        strProfileId = Claro.SIACU.Transac.Service.Constants.PresentationLayer.NumeracionCERO,
                        CamposAdicionalesDescuento = new CamposAdicionalesDcto()
                        {
                            strTipoCostoServicioAvanzado = ConfigurationManager.AppSettings("strConstTipoCostoServicioCrearContraroLTE"),
                            strCostoServicioAvanzado = ele.CF,
                            strPeriodoCostoServicioAvanzado = ConfigurationManager.AppSettings("strConstPeriodoServicioCrearContraroLTE")
                        },
                        CamposAdicionalesCargo = new CamposAdicionalesCargo()
                        {
                            strTipoCostoServicio = ConfigurationManager.AppSettings("strConstTipoCostoServicioCrearContraroLTE"),
                            strCostoServicio = ele.CF,
                            strPeriodoCostoServicio = ConfigurationManager.AppSettings("strConstPeriodoServicioCrearContraroLTE")
                        }
                    }).ToList();
                
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info("GetServiceByContrat", "LTEPlanMigration", "catch GetServiceByContrat" + ex.ToString());
                
            }
            }
        private void GetServiceAndEquipment(List<ServiceByPlan> lstServices, List<ServiceByPlan> lstDecoAdditional, List<ServiceByPlan> lstEquipmentCore, List<ServiceByPlan> lstEquipmentCableAll, ref List<ServiceByPlan> lstServicesAndEquipment, ref List<ServiceByPlan> lstServicesAndEquipmentTipification)
        {
            try
            {
           
                var lstServicesCopy = GetNewList(lstServices);
                var lstEquipmentCoreCopy = GetNewList(lstEquipmentCore);
                foreach (var objItem in lstServicesCopy)
                {
                    objItem.Tipequ = "";
                    objItem.Codtipequ = "";
                    objItem.Dscequ = "";
                }
                foreach (var objItem in lstEquipmentCoreCopy)
                {
                    objItem.CodServSisact = "";
                    objItem.DesServSisact = "";
                }
                var lstDecosAdditionalCopy = new List<ServiceByPlan>();
           
                foreach (var objItem in lstDecoAdditional)
                {
                    lstDecosAdditionalCopy = lstDecosAdditionalCopy.Concat(GetNewList((from x in lstEquipmentCableAll
                                                                                        where objItem.CodServSisact == x.CodServSisact
                                                                                        select x).ToList()
                                                                                      )
                                                                           ).ToList();
                }
                foreach (var objItem in lstDecosAdditionalCopy)
                {
                    objItem.CodServSisact = "";
                    objItem.DesServSisact = "";
                }

                lstServicesAndEquipment = lstServicesAndEquipment.Concat(lstServicesCopy).ToList();
                lstServicesAndEquipment = lstServicesAndEquipment.Concat(lstEquipmentCoreCopy).ToList();
                lstServicesAndEquipment = lstServicesAndEquipment.Concat(lstDecosAdditionalCopy).ToList();
                lstServicesAndEquipmentTipification = GetNewList(lstServicesAndEquipment);
                foreach (var objItem in lstServicesAndEquipmentTipification)
                {
                    objItem.CF = objItem.CfWithIgv;
                    objItem.DesServSisact = !string.IsNullOrEmpty(objItem.Tipequ)? objItem.Equipment: objItem.DesServSisact;
                    objItem.ServiceType = !string.IsNullOrEmpty(objItem.ServiceType)? ChangeServiceType(objItem.ServiceType): objItem.ServiceType;
        }

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info("GetServiceAndEquipment", "LTEPlanMigration", "catch GetUnionEquipmentAdditional"+ex.ToString());
            }
        }

        private List<ServiceByPlan> GetNewList(List<ServiceByPlan> lstServices)
        {
            var lstNewServices = new List<ServiceByPlan>();
            foreach (var item in lstServices)
            {
                ServiceByPlan objPivot = new ServiceByPlan()
                {
                    CantEquipment = item.CantEquipment,
                    CF = item.CF,
                    CfWithIgv = item.CfWithIgv,
                    CodeExternal = item.CodeExternal,
                    CodGroupServ = item.CodGroupServ,
                    CodPlanSisact = item.CodPlanSisact,
                    CodPrincipalGroup = item.CodPrincipalGroup,
                    CodEquipmentGroup = item.CodEquipmentGroup,
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
                    IdLineQuantity = Constant.numeroUno.ToString(),
                    Quantity = Constant.numeroUno.ToString(),
                    ServiceType = item.ServiceType,
                    ServvUserCrea = item.ServvUserCrea,
                    Sncode = item.Sncode,
                    Solution = item.Solution,
                    Spcode = item.Spcode,
                    Tipequ = item.Tipequ,
                    Tmcode = item.Tmcode
                };
                lstNewServices.Add(objPivot);
            }
            return lstNewServices;
        }
        #endregion

        public JsonResult GetWorkSubType(string strIdSession, string strCodTypeWork, string strContractID)
        {
            List<HELPERS.CommonServices.GenericItem> objListaEta = new List<HELPERS.CommonServices.GenericItem>();
            FixedTransacService.OrderSubTypesRequestHfc objResquest = null;
            FixedTransacService.OrderSubTypesResponseHfc objResponse = new FixedTransacService.OrderSubTypesResponseHfc();
            FixedTransacService.OrderSubType objResponseValidate = new FixedTransacService.OrderSubType();
            MODEL.HFC.ExternalInternalTransferModel objFixedGetSubOrderType = null;
            try
            {

                FixedTransacService.AuditRequest auditreq = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);

                objResquest = new FixedTransacService.OrderSubTypesRequestHfc()
                {
                    audit = auditreq,
                    av_cod_tipo_trabajo = strCodTypeWork
                };
                objResponse = Claro.Web.Logging.ExecuteMethod<FixedTransacService.OrderSubTypesResponseHfc>(() => { return new FixedTransacServiceClient().GetOrderSubTypeWork(objResquest); });

                List<HELPERS.CommonServices.GenericItem> ListSubOrderType = new List<HELPERS.CommonServices.GenericItem>();
                if (objResponse != null && objResponse.OrderSubTypes != null)
                {
                    objFixedGetSubOrderType = new MODEL.HFC.ExternalInternalTransferModel();

                    foreach (FixedTransacService.OrderSubType item in objResponse.OrderSubTypes)
                    {
                        ListSubOrderType.Add(new HELPERS.CommonServices.GenericItem()
                        {
                            Code = string.Format("{0}|{1}|{2}", item.COD_SUBTIPO_ORDEN, item.TIEMPO_MIN, item.ID_SUBTIPO_ORDEN),
                            Description = item.DESCRIPCION,
                            Code2 = item.TIPO_SERVICIO
                        });
                    }
                    objFixedGetSubOrderType.ListGeneric = ListSubOrderType;
                }

                objResquest = new FixedTransacService.OrderSubTypesRequestHfc()
                {
                    audit = auditreq,
                    av_cod_tipo_trabajo = strCodTypeWork,
                    av_cod_contrato = strContractID
                };
                objResponseValidate = Claro.Web.Logging.ExecuteMethod<FixedTransacService.OrderSubType>(() => { return new FixedTransacServiceClient().GetValidationSubTypeWork(objResquest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strIdSession, ex.Message);

            }
            return Json(new { data = objFixedGetSubOrderType, typeValidate = objResponseValidate });
        }

        public JsonResult GetWorkType(string strIdSession, string strTransacType)
        {
            FixedTransacService.JobTypesResponseHfc objWorkTypeResponseCommon = null;
            FixedTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);
            FixedTransacService.JobTypesRequestHfc objWorkTypeRequestCommon = new FixedTransacService.JobTypesRequestHfc()
            {
                audit = audit

            };

            try
            {
                objWorkTypeRequestCommon.p_tipo = Convert.ToInt(strTransacType);
                objWorkTypeResponseCommon = Claro.Web.Logging.ExecuteMethod<FixedTransacService.JobTypesResponseHfc>(() => { return new FixedTransacServiceClient().GetJobTypeLte(objWorkTypeRequestCommon); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objWorkTypeRequestCommon.audit.transaction, ex.Message);
            }

            MODEL.HFC.ExternalInternalTransferModel objCommonTransacServices = null;
            if (objWorkTypeResponseCommon != null && objWorkTypeResponseCommon.JobTypes != null)
            {
                objCommonTransacServices = new MODEL.HFC.ExternalInternalTransferModel();
                List<HELPERS.CommonServices.GenericItem> listWorkTypes = new List<HELPERS.CommonServices.GenericItem>();

                foreach (FixedTransacService.JobType item in objWorkTypeResponseCommon.JobTypes)
                {
                    listWorkTypes.Add(new HELPERS.CommonServices.GenericItem()
                    {
                        Code = item.tiptra,
                        Description = item.descripcion
                    });
                }
                objCommonTransacServices.ListGeneric = listWorkTypes;
            }

            return Json(new { data = objCommonTransacServices });
        }

        
    }
}