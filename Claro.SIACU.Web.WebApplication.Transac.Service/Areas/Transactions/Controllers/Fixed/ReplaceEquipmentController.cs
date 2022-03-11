using Claro.SIACU.Transac.Service;
using Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CONSTANT = Claro.SIACU.Transac.Service;
using MODELS = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models;
using AuditRequestFixed = Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService.AuditRequest;
using AuditRequestCommon = Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService.AuditRequest;
using KEY = Claro.ConfigurationManager;
using ConstantsFixed = Claro.SIACU.Transac.Service.Constants;
using HELPERS = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers;
using COMMON = Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService;
using FIXED = Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService;
using FunctionsSIACU = Claro.SIACU.Transac.Service.Functions;
using Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models.Fixed;
using Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models;
using Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.Fixed.ReplaceEquipment;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Controllers.Fixed
{
    public class ReplaceEquipmentController : CommonServicesController
    {
        #region Instance Service
        private readonly COMMON.CommonTransacServiceClient _oServiceCommon = new COMMON.CommonTransacServiceClient();
        private readonly FixedTransacServiceClient _oServiceFixed = new FixedTransacServiceClient();


        #endregion

        #region Globals

        public static string _strCurrentEquipmentTypeSelected = string.Empty;
        public static List<ItemGeneric> _lstServiceTypes = new List<ItemGeneric>();
        public static CustomerEquipment objToAssociate = new CustomerEquipment();
        public static string _strEquipmentslimit = string.Empty;
        public static string _strEquipmentsAssociate = string.Empty;
        public static string _strEquipmentAssociateUsed = string.Empty;
        #endregion

        #region ShowViews
        public ActionResult FixedReplaceEquipment()
        {
            return PartialView("~/Areas/Transactions/Views/ReplaceEquipment/FixedReplaceEquipment.cshtml");
        }

        public ActionResult FixedReplaceEquipmentAssociate(string currentEquipmentTypeSelected, string limitEquipmentsPerCustomer, string associateEquipmentsPerCustomer, string strEquipmentAssociateUsed)
        {
            _strCurrentEquipmentTypeSelected = currentEquipmentTypeSelected;
            _strEquipmentslimit = limitEquipmentsPerCustomer;
            _strEquipmentsAssociate = associateEquipmentsPerCustomer;
            _strEquipmentAssociateUsed = strEquipmentAssociateUsed;
            return PartialView("~/Areas/Transactions/Views/ReplaceEquipment/FixedReplaceEquipmentAssociate.cshtml");
        }
        #endregion

        #region LoadMethods
        [HttpPost]
        public JsonResult FixedReplaceEquipmentLoad(RequestDataModel objModel)
        {
            var objReplaceEquipmentLoadModel = new ReplaceEquipmentLoadModel();
            try
            {
                Claro.Web.Logging.Info(objModel.strIdSession, "ReplaceEquipment: ", string.Format("ExecuteFixedReplaceEquipmentLoad() - CustomerContractID: {0}", objModel.strIdContract));

                objReplaceEquipmentLoadModel.objReplaceEquipmentMessageModel = GetMessageObj(objModel.strIdSession);

                objReplaceEquipmentLoadModel.strUserCac = GetUsersStr(objModel.strIdSession, objModel.strCodeUser);
                objReplaceEquipmentLoadModel.lstCacDacTypes = GetListCacDac(objModel.strIdSession);
                //objReplaceEquipmentLoadModel.lstSOTListypes = GetListSot(objModel.strIdSession, objModel.strCustomerId, objModel.strIdContract);

                #region ChargeTypification
                var objTypificationModel = new TypificationModel();
                var strMessageResult = string.Empty;
                var objtipification = LoadTypifications(objModel.strIdSession, GetValueConfig("strCodigosReposicionDeEquipoLTE", objModel.strIdSession, ConfigurationManager.AppSettings("strNombreTransaccionReposicionEquipo")), ref strMessageResult);
                if (objtipification != null)
                {
                    objTypificationModel.Type = objtipification.TIPO;
                    objTypificationModel.Class = objtipification.CLASE;
                    objTypificationModel.SubClass = objtipification.SUBCLASE;
                    objTypificationModel.InteractionCode = objtipification.INTERACCION_CODE;
                    objTypificationModel.TypeCode = objtipification.TIPO_CODE;
                    objTypificationModel.ClassCode = objtipification.CLASE_CODE;
                    objTypificationModel.SubClassCode = objtipification.SUBCLASE_CODE;
                }
                else
                {
                    objTypificationModel.Type = string.Empty;
                    objTypificationModel.Class = string.Empty;
                    objTypificationModel.SubClass = string.Empty;
                    objTypificationModel.InteractionCode = string.Empty;
                    objTypificationModel.TypeCode = string.Empty;
                    objTypificationModel.ClassCode = string.Empty;
                    objTypificationModel.SubClassCode = string.Empty;

                }
                #endregion

                objReplaceEquipmentLoadModel.strAmountToPay = "";
                objReplaceEquipmentLoadModel.objTypification = objTypificationModel;
                objReplaceEquipmentLoadModel.lstBusinessRules = GetBusinessRulesLst(objModel.strIdSession, objTypificationModel.SubClassCode);
                objReplaceEquipmentLoadModel.lstServiceTypes = GetServiceTypesList(objModel.strIdSession,KEY.AppSettings("strCodigoTipoDeServicioReposicion"));
                int intTipoProducto=0;
                switch (objModel.strTypeProduct)
                {
                    case "LTE":
                        intTipoProducto = int.Parse(KEY.AppSettings("strTipoTrabajoReposicionEquipoLTE"));
                        break;
                    case "HFC":
                        intTipoProducto = int.Parse(KEY.AppSettings("strTipoTrabajoReposicionEquipoHFC"));
                        break;
                }
                objReplaceEquipmentLoadModel.lstTypeWork = GetTypeWorkLteList(objModel.strIdSession, intTipoProducto);
                objReplaceEquipmentLoadModel.lstMotiveSOTByTypeJob = GetMotiveSotByTypeJob(objModel.strIdSession, Convert.ToInt(objReplaceEquipmentLoadModel.lstTypeWork.First().Code));

                _lstServiceTypes = objReplaceEquipmentLoadModel.lstServiceTypes;
                objReplaceEquipmentLoadModel.strActiveFullReplaceEquipmentForFixed = KEY.AppSettings("strActiveFullReplaceEquipmentForFixed");

                objReplaceEquipmentLoadModel.lstEquimentAssociate = GetListDataProducts(objModel.strIdSession, objModel.strCustomerId, objModel.strIdContract);
                //objReplaceEquipmentLoadModel.lstEquimentAssociate.Sort((x, y) => x.EquipmentAssociate.CompareTo(y.EquipmentAssociate));
                objReplaceEquipmentLoadModel.lstEquimentAssociate = objReplaceEquipmentLoadModel.lstEquimentAssociate.OrderBy(p => p.EquipmentAssociate).ThenBy(p => p.EquipmentType).ToList();

                objReplaceEquipmentLoadModel.strServidorLeerPDF = KEY.AppSettings("strServidorLeerPDF");
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(objModel.strIdSession, "ReplaceEquipment", string.Format("ExecuteFixedReplaceEquipmentLoad() - Error: {0}", ex.Message));
            }

            return Json(new { data = objReplaceEquipmentLoadModel }, JsonRequestBehavior.AllowGet);
        }

        #region GetProductsDetails

        public List<HELPERS.Fixed.ReplaceEquipment.CustomerEquipment> GetListDataProducts(string strIdSession, string CUSTOMER_ID, string CONTRATO_ID)
        {
            Claro.Web.Logging.Info(strIdSession, "ReplaceEquipment", string.Format("ExecuteGetListDataProducts() - CustomerID: {0} - ContractID: {1}", CUSTOMER_ID, CONTRATO_ID));
            List<HELPERS.Fixed.ReplaceEquipment.CustomerEquipment> listDataProducts = new List<HELPERS.Fixed.ReplaceEquipment.CustomerEquipment>();

            ServicesLteFixedResponse objServicesFixedFixedResponse = null;//
            var audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);
            ServicesLteFixedRequest objServicesFixedFixedRequest = new FixedTransacService.ServicesLteFixedRequest()
            {
                audit = audit,
                strCoid = CONTRATO_ID,
                strCustomerId = CUSTOMER_ID
            };
            var oConsultIgv = new ConsultIGVModel();
            try
            {
                oConsultIgv = new ConsultIGVModel();
                oConsultIgv = GetCommonConsultIgv(strIdSession);

                objServicesFixedFixedResponse = Claro.Web.Logging.ExecuteMethod<ServicesLteFixedResponse>(
                    () =>
                    {
                        return _oServiceFixed.GetCustomerEquipments(objServicesFixedFixedRequest);
                    });


                if (objServicesFixedFixedResponse.ListServicesLte.Count > 0)
                {
                    foreach (BEDeco item in objServicesFixedFixedResponse.ListServicesLte)
                    {
                        listDataProducts.Add(new HELPERS.Fixed.ReplaceEquipment.CustomerEquipment()
                        {
                            EquipmentServiceType = item.tipoServicio,
                            EquipmentSeriesNumber = item.numero_serie,
                            EquipmentDescription = item.descripcion_material,
                            EquipmentType = item.tipo_equipo.ToUpper(),
                            DecoType = item.tipo_deco,
                            EquipmentMACAddress = item.macadress,
                            NumberPhone = item.numero,
                            EquipmentOC = item.oc_equipo,
                            EquipmentAssociate = item.asociado,
                            EquipmentCodeType = item.codigo_tipo_equipo,
                            Action = KEY.AppSettings("strActionDisassociate"),
                            OperationCode = "",
                            EquipmentTypeBD = item.tipequ,
                            EquipmentTypeCodeBD = item.codtipequ,
                            EquipmentCodINSSRV = item.codinssrv,
                            Penalidad = Math.Round((item.penalidad * decimal.Parse(oConsultIgv.igvD.ToString()) + item.penalidad),2).ToString()
                        });
                    }
                    Claro.Web.Logging.Info(strIdSession, strIdSession, string.Format("Reposición de Equipo - ExecutingGetListDataProducts() - ListDataProducts Fixed: {0}", listDataProducts.Count.ToString()));
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objServicesFixedFixedRequest.audit.transaction, ex.Message);
            }
            Claro.Web.Logging.Info(strIdSession, strIdSession, string.Format("Reposición de Equipo - EndGetListDataProducts() - CustomerID: {0} - ContractID: {1}", CUSTOMER_ID, CONTRATO_ID));
            return listDataProducts;
        }

        #endregion

        public JsonResult FixedReplaceEquipmentLoadModalAssociate()
        {
            var objReplaceEquipmentLoadModel = new ReplaceEquipmentLoadModel();
            var objReplaceEquipmentMessageModel = new ReplaceEquipmentMessageModel();
            objReplaceEquipmentLoadModel.lstServiceTypes = _lstServiceTypes;

            objReplaceEquipmentLoadModel.strCurrentEquipmentTypeSelected = _strCurrentEquipmentTypeSelected;
            objReplaceEquipmentLoadModel.strActiveFullReplaceEquipmentForFixed = KEY.AppSettings("strActiveFullReplaceEquipmentForFixed");

            objReplaceEquipmentMessageModel.strMsgCheckFullReplace = Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strMessageCheckFullChange", ConfigurationManager.AppSettings("strConstArchivoSIACUTFIXEDConfigMsg"));
            objReplaceEquipmentMessageModel.strMsjValidacionCargadoListado = Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strMsjValidacionCargadoListado", ConfigurationManager.AppSettings("strConstArchivoSIACUTFIXEDConfigMsg"));

            objReplaceEquipmentLoadModel.objReplaceEquipmentMessageModel = objReplaceEquipmentMessageModel;
            objReplaceEquipmentLoadModel.strEquipmentLimits = _strEquipmentslimit;
            objReplaceEquipmentLoadModel.strEquipmentAssociate = _strEquipmentsAssociate;
            objReplaceEquipmentLoadModel.strEquipmentAssociateUsed = _strEquipmentAssociateUsed; 
            return Json(new { data = objReplaceEquipmentLoadModel }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region GetMessageObj
        public ReplaceEquipmentMessageModel GetMessageObj(string strIdSession)
        {
            Claro.Web.Logging.Info(strIdSession, "ReplaceEquipmentFixed", "GetMessageObj");
            var objReplaceEquipmentMessageModel = new ReplaceEquipmentMessageModel
            {
            strStateLine = Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strStateLine", ConfigurationManager.AppSettings("strConstArchivoSIACUTFIXEDConfigMsg")),
            strMsgStateLine = Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strMsgStateLine", ConfigurationManager.AppSettings("strConstArchivoSIACUTFIXEDConfigMsg")),
            strVariableEmpty = Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strVariableEmpty", ConfigurationManager.AppSettings("strConstArchivoSIACUTFIXEDConfigMsg")),
            strMsgVariableEmpty = Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strMsgVariableEmpty", ConfigurationManager.AppSettings("strConstArchivoSIACUTFIXEDConfigMsg")),
            strMsjValidacionCampoSlt = Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strMsjValidacionCampoSlt", ConfigurationManager.AppSettings("strConstArchivoSIACUTFIXEDConfigMsg")),
            strMsjValidacionCampo = Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strMsjValidacionCampo", ConfigurationManager.AppSettings("strConstArchivoSIACUTFIXEDConfigMsg")),
            strMsjValidacionCampoFormato = Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strMsjValidacionCampoFormato", ConfigurationManager.AppSettings("strConstArchivoSIACUTFIXEDConfigMsg")),
            strMsjValidacionCargadoListado = Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strMsjValidacionCargadoListado", ConfigurationManager.AppSettings("strConstArchivoSIACUTFIXEDConfigMsg")),
            strMessageSummary = Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strMessageSummary", ConfigurationManager.AppSettings("strConstArchivoSIACUTFIXEDConfigMsg")),
            strValidateEquipmentActive = Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strValidateEquipmentActive", ConfigurationManager.AppSettings("strConstArchivoSIACUTFIXEDConfigMsg")),
            strValidateEquipmentNotFound = Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strValidateEquipmentNotFound", ConfigurationManager.AppSettings("strConstArchivoSIACUTFIXEDConfigMsg")),
            strValidateEquipmentTechnicalErrors = Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strValidateEquipmentTechnicalErrors", ConfigurationManager.AppSettings("strConstArchivoSIACUTFIXEDConfigMsg")),
            strValidateEquipmentContractInvalid = Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strValidateEquipmentContractInvalid", ConfigurationManager.AppSettings("strConstArchivoSIACUTFIXEDConfigMsg")),
            strValidateEquipmentICCID = Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strValidateEquipmentICCID", ConfigurationManager.AppSettings("strConstArchivoSIACUTFIXEDConfigMsg")),
            strValidateEquipmentICCIDNotExist = Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strValidateEquipmentICCIDNotExist", ConfigurationManager.AppSettings("strConstArchivoSIACUTFIXEDConfigMsg")),
            strValidateEquipmentMoreThanOneEquipment = Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strValidateEquipmentMoreThanOneEquipment", ConfigurationManager.AppSettings("strConstArchivoSIACUTFIXEDConfigMsg")),
            strValidateEquipmentContractNotExist = Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strValidateEquipmentContractNotExist", ConfigurationManager.AppSettings("strConstArchivoSIACUTFIXEDConfigMsg")),
            strValidateEquipmentNotReceivedNewData = Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strValidateEquipmentNotReceivedNewData", ConfigurationManager.AppSettings("strConstArchivoSIACUTFIXEDConfigMsg")),
            strValidateEquipmentResponseCodeInvalid = Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strValidateEquipmentResponseCodeInvalid", ConfigurationManager.AppSettings("strConstArchivoSIACUTFIXEDConfigMsg")),
            strValidateIncompleteFields = Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strValidateIncompleteFields", ConfigurationManager.AppSettings("strConstArchivoSIACUTFIXEDConfigMsg")),
            strConstancyError = Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strConstancyError", ConfigurationManager.AppSettings("strConstArchivoSIACUTFIXEDConfigMsg")),
            strEquipmentNotSelected = Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strEquipmentNotSelected", ConfigurationManager.AppSettings("strConstArchivoSIACUTFIXEDConfigMsg")),
            strEquipmentBlockToSelected = Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strEquipmentBlockToSelected", ConfigurationManager.AppSettings("strConstArchivoSIACUTFIXEDConfigMsg")),
            strValidateSameDecoType = Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strValidateSameDecoType", ConfigurationManager.AppSettings("strConstArchivoSIACUTLTEConfigMsg")),
            strSeriesNumberNotEqual = Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strSeriesNumberNotEqual", ConfigurationManager.AppSettings("strConstArchivoSIACUTLTEConfigMsg")),
            strNotSeriesInput = Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strNotSeriesInput", ConfigurationManager.AppSettings("strConstArchivoSIACUTLTEConfigMsg")),
            strLimitEquipments = Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strLimitEquipments", ConfigurationManager.AppSettings("strConstArchivoSIACUTLTEConfigMsg")),
            strEqualEquipmentExistInOperation = Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strEqualEquipmentExistInOperation", ConfigurationManager.AppSettings("strConstArchivoSIACUTLTEConfigMsg")),
            strSuccessModification = Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strSuccessModification", ConfigurationManager.AppSettings("strConstArchivoSIACUTLTEConfigMsg")),
            strMsjErrorAlCargar = Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strMsjErrorAlCargar", ConfigurationManager.AppSettings("strConstArchivoSIACUTLTEConfigMsg")),
            };
            return objReplaceEquipmentMessageModel;
        }
        #endregion

        #region GetListSOT
        public List<COMMON.ListItem> GetListSot(string strIdSession, string CUSTOMER_ID, string CONTRATO_ID)
        {
            Claro.Web.Logging.Info(strIdSession, "ReplaceEquipment", string.Format("GetListSot() - CustomerID: {0} - ContractID: {1}", CUSTOMER_ID, CONTRATO_ID));
            List<COMMON.ListItem> listDataSots = new List<COMMON.ListItem>();
            COMMON.GetSotResponseCommon objSotResponseCommon = null;
            var audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
            COMMON.GetSotRequest objSotRequestCommon = new COMMON.GetSotRequest()
            {
                audit = audit,
                COD_ID = CONTRATO_ID,
                CUSTOMER_ID = CUSTOMER_ID
            };
            try
            {
                objSotResponseCommon = Claro.Web.Logging.ExecuteMethod<COMMON.GetSotResponseCommon>(
                    () =>
                    {
                        return _oServiceCommon.GetSot(objSotRequestCommon);
                    });
                if (objSotResponseCommon.ListSot.Count > 0)
                {
                    foreach (var item in objSotResponseCommon.ListSot)
                    {
                        listDataSots.Add(new COMMON.ListItem()
                        {

                            Code = item.Code
                        });
                    }
                    Claro.Web.Logging.Info(strIdSession, strIdSession, string.Format("Reposición de Equipo - GetListSot() - ListSot Fixed: {0}", listDataSots.Count.ToString()));
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objSotRequestCommon.audit.transaction, ex.InnerException.Message);
            }
            Claro.Web.Logging.Info(strIdSession, strIdSession, string.Format("Reposición de Equipo - GetListSot() - CustomerID: {0} - ContractID: {1}", CUSTOMER_ID, CONTRATO_ID));
            return listDataSots;
        }
        #endregion

        #region GetValidateEquipment
        public JsonResult GetValidateEquipment(ReplaceEquipmentValidateModel objModel)
        {
            string[] result = new string[2];
            var audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(objModel.strSessionId);
            if (KEY.AppSettings("strValidacionCambioEquipoSANSDB")=="1")
            {
                if (objModel.strEquipmentTypeCode == ConstantsFixed.strTres)
                {
                    result = GetAvailabilitySimcardSANS(objModel, audit);
                    if (result[0] == ConstantsFixed.strCero)
                    {
                        result = GetAvailabilitySimcardBSCS(objModel, audit);
                        if (result[0] == ConstantsFixed.strCero)
                        {
                            result = GetValidateSimcardBSCS_HLCODE(objModel, audit);
                            if (result[0] == ConstantsFixed.strCero)
                            {
                            result = GetValidateEquipmentSGA(objModel, audit);
                            }
                        }
                    }
                }
                else
                {
                    result = GetValidateEquipmentSGA(objModel, audit);
                }
            }
            else
            {
                if (objModel.strEquipmentTypeCode == ConstantsFixed.strTres)
                {
                        result = GetAvailabilitySimcardBSCS(objModel, audit);
                        if (result[0] == ConstantsFixed.strCero)
                        {
                            result = GetValidateSimcardBSCS_HLCODE(objModel, audit);
                            if (result[0] == ConstantsFixed.strCero)
                            {
                                result = GetValidateEquipmentSGA(objModel, audit);
                            }
                        }                  
                }
                else
                {
                    result = GetValidateEquipmentSGA(objModel, audit);
                }
            }
            objModel.strResultCode = result[0];
            objModel.strResultMessage = result[1];
            objModel.objEquipmentToAssociate = objToAssociate;
            return Json(new { data = objModel }, JsonRequestBehavior.AllowGet);
        }

        #region GetBValidateEquipmentSGA
        public string[] GetValidateEquipmentSGA(ReplaceEquipmentValidateModel objRequest, FixedTransacService.AuditRequest audit)
        {
            Claro.Web.Logging.Info(objRequest.strSessionId, "ReplaceEquipment", string.Format("GetValidateEquipmentSGA() - EquipmentCodeType: {0} - EquipmentSeriesnumber: {1}", objRequest.strEquipmentTypeCode, objRequest.strEquipmentSeries));

            DispEquipmentResponse objDispEquipmentResponse = new DispEquipmentResponse();
            string[] result = new string[2];

            DispEquipmentRequest objDispEquipmentRequest = new DispEquipmentRequest()
            {
                audit = audit,
                intTipo = Int32.Parse(objRequest.strEquipmentTypeCode),
                strNroserie = objRequest.strEquipmentSeries
            };
            try
            {
                objDispEquipmentResponse = Claro.Web.Logging.ExecuteMethod<FIXED.DispEquipmentResponse>(
                    () => _oServiceFixed.GetValidateEquipment(objDispEquipmentRequest));

                if (objDispEquipmentResponse.ResultCode==ConstantsFixed.strCero)
                {
                    List<Helpers.Fixed.ReplaceEquipment.CustomerEquipment> LstEquipmentToAssociate = new List<HELPERS.Fixed.ReplaceEquipment.CustomerEquipment>();
                    if (objDispEquipmentResponse.lstEquipments.Count > 0)
                    {

                        foreach (BEDeco item in objDispEquipmentResponse.lstEquipments)
                        {
                            LstEquipmentToAssociate.Add(new HELPERS.Fixed.ReplaceEquipment.CustomerEquipment()
                            {
                                EquipmentServiceType = objRequest.strEquipmentTypeCode == ConstantsFixed.PresentationLayer.NumeracionCUATRO || objRequest.strEquipmentTypeCode == ConstantsFixed.PresentationLayer.NumeracionTRES ? ConstantsFixed.PresentationLayer.TipoProduco.LTE : ConstantsFixed.PresentationLayer.TipoProduco.TVSatelital,

                                EquipmentSeriesNumber = item.numero_serie,
                                EquipmentDescription = item.descripcion_material,
                                EquipmentType = item.tipo_equipo.ToUpper(),
                                EquipmentCodeType = item.codigo_tipo_equipo,
                                DecoType = item.tipo_deco,
                                EquipmentMACAddress = item.macadress,
                                EquipmentTypeBD = item.tipequ,
                                EquipmentTypeCodeBD = item.codtipequ,
                                Action = KEY.AppSettings("strActionAssociate"),
                                OperationCode = "",
                                EquipmentCodINSSRV = objRequest.strCodInssrv
                            });
                        }
                        objToAssociate = LstEquipmentToAssociate[0];
                        if (objRequest.strEquipmentTypeCode == ConstantsFixed.strDos)
                        {
                            if (objRequest.strDecoType == objToAssociate.DecoType)
                            {
                                result[0] = objDispEquipmentResponse.ResultCode;
                            }
                            else result[0] = ConstantsFixed.strCeroSeis;
                        }
                        else
                        {
                            result[0] = objDispEquipmentResponse.ResultCode;
                        }
                    
                    }
                    else
                    {
                        Claro.Web.Logging.Info(objRequest.strSessionId, "ReplaceEquipment", string.Format("GetValidateEquipmentSGA() - Result: {0}  - ResultMessage: {1}", objDispEquipmentResponse.ResultCode, objDispEquipmentResponse.ResultMessage));
                        result[0] = ConstantsFixed.strCeroUno;
                    }
                }
                else
                {
                    result[0] = objDispEquipmentResponse.ResultCode;
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.strSessionId, objDispEquipmentRequest.audit.transaction, ex.Message);
                result[0] = ConstantsFixed.strDobleCero;
                return result;
            }

            result[1] = objDispEquipmentResponse.ResultMessage;
            Claro.Web.Logging.Info(objRequest.strSessionId, "ReplaceEquipment", string.Format("GetValidateEquipmentSGA() - Result: {0}  - ResultMessage: {1}", objDispEquipmentResponse.ResultCode, objDispEquipmentResponse.ResultMessage));
            return result;
        }
        #endregion

        #region GetValidateSimcardSANS
        public string[] GetAvailabilitySimcardSANS(ReplaceEquipmentValidateModel objRequest, FixedTransacService.AuditRequest audit)
        {
            Claro.Web.Logging.Info(objRequest.strSessionId, "ReplaceEquipment", string.Format("GetValidateSimcardSANS() - ContractID: {0} - EquipmentSerie: {1} - EquipmentTypeCode: {2}", objRequest.strContractId, objRequest.strEquipmentSeries, objRequest.strEquipmentTypeCode));

            AvailabilitySimcardResponse objAvailabilitySimcardResponse = new AvailabilitySimcardResponse();
            string[] result = new string[2];

            AvailabilitySimcardRequest objAvailabilitySimcardRequest = new AvailabilitySimcardRequest()
            {
                SimcardSeries = objRequest.strEquipmentSeries.Substring(ConstantsFixed.numeroCero, objRequest.strEquipmentSeries.Length - 2),
                ContractId = objRequest.strContractId,
                audit = audit
            };
            try
            {
                objAvailabilitySimcardResponse = Claro.Web.Logging.ExecuteMethod<AvailabilitySimcardResponse>(
                    () => _oServiceFixed.GetAvailabilitySimcardSANS(objAvailabilitySimcardRequest));
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.strSessionId, audit.transaction, ex.InnerException.Message);
            }
            result[0] = objAvailabilitySimcardResponse.ResultCode;
            result[1] = objAvailabilitySimcardResponse.ResultMessage;
            Claro.Web.Logging.Info(objRequest.strSessionId, "ReplaceEquipment", string.Format("GetValidateSimcardSANS(): Result: {0} - ResultMessage: {1}", objAvailabilitySimcardResponse.ResultCode, objAvailabilitySimcardResponse.ResultMessage));
            return result;
        }
        #endregion

        #region GetValidateSimcardBSCS
        public string[] GetAvailabilitySimcardBSCS(ReplaceEquipmentValidateModel objRequest, FixedTransacService.AuditRequest audit)
        {
            Claro.Web.Logging.Info(objRequest.strSessionId, "ReplaceEquipment", string.Format("GetValidateSimcardBSCS() - ContractID: {0} - EquipmentSerie: {1} - EquipmentTypeCode: {2}", objRequest.strContractId, objRequest.strEquipmentSeries, objRequest.strEquipmentTypeCode));

            AvailabilitySimcardResponse objAvailabilitySimcardResponse = new AvailabilitySimcardResponse();
            string[] result = new string[2];
            AvailabilitySimcardRequest objAvailabilitySimcardRequest = new AvailabilitySimcardRequest()
            {
                SimcardSeries = objRequest.strEquipmentSeries.Substring(ConstantsFixed.numeroCero, objRequest.strEquipmentSeries.Length - 1),
                ContractId = objRequest.strContractId,
                audit = audit
            };
            try
            {
                objAvailabilitySimcardResponse = Claro.Web.Logging.ExecuteMethod<AvailabilitySimcardResponse>(
                    () => _oServiceFixed.GetAvailabilitySimcardBSCS(objAvailabilitySimcardRequest));
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.strSessionId, audit.transaction, ex.InnerException.Message);
            }
            result[0] = objAvailabilitySimcardResponse.ResultCode;
            result[1] = objAvailabilitySimcardResponse.ResultMessage;
            Claro.Web.Logging.Info(objRequest.strSessionId, "ReplaceEquipment", string.Format("GetValidateSimcardBSCS(): Result: {0} - ResultMessage: {1}", objAvailabilitySimcardResponse.ResultCode, objAvailabilitySimcardResponse.ResultMessage));
            return result;
        }
        #endregion

        #region GetValidateSimcardBSCS_HLCODE
        public string[] GetValidateSimcardBSCS_HLCODE(ReplaceEquipmentValidateModel objRequest, FixedTransacService.AuditRequest audit)
        {
            Claro.Web.Logging.Info(objRequest.strSessionId, "ReplaceEquipment", string.Format("GetValidateSimcardBSCS_HLCODE() - ContractID: {0} - EquipmentSerie: {1} - EquipmentTypeCode: {2}", objRequest.strContractId, objRequest.strEquipmentSeries, objRequest.strEquipmentTypeCode));

            AvailabilitySimcardResponse objAvailabilitySimcardResponse = new AvailabilitySimcardResponse();
            string[] result = new string[2];
            AvailabilitySimcardRequest objAvailabilitySimcardRequest = new AvailabilitySimcardRequest()
            {
                SimcardSeries = objRequest.strEquipmentSeries.Substring(ConstantsFixed.numeroCero, objRequest.strEquipmentSeries.Length - 1),
                SimcardSeriesOld = objRequest.strOldEquipmentSeriesNumber.Substring(ConstantsFixed.numeroCero, objRequest.strOldEquipmentSeriesNumber.Length - 1),
                ContractId = objRequest.strContractId,
                audit = audit
            };
            try
            {
                objAvailabilitySimcardResponse = Claro.Web.Logging.ExecuteMethod<AvailabilitySimcardResponse>(
                    () => _oServiceFixed.GetValidateSimcardBSCS_HLCODE(objAvailabilitySimcardRequest));
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.strSessionId, audit.transaction, ex.Message);
            }
            result[0] = objAvailabilitySimcardResponse.ResultCode;
            result[1] = objAvailabilitySimcardResponse.ResultMessage;
            Claro.Web.Logging.Info(objRequest.strSessionId, "ReplaceEquipment", string.Format("GetValidateSimcardBSCS_HLCODE(): Result: {0} - ResultMessage: {1}", objAvailabilitySimcardResponse.ResultCode, objAvailabilitySimcardResponse.ResultMessage));
            return result;
        }
         #endregion

        #endregion

        #region Save

        [HttpPost]
        public JsonResult Save(ReplaceEquipmentSaveModel objRequestDataModel)
        {
            var objReplaceEquipmentSaveModel = new ReplaceEquipmentSaveModel();

            string strResult = string.Empty;
            string fieldSeparator = ConstantsFixed.PresentationLayer.gstrVariablePipeline;
            string fieldSeparatorEnd = ConstantsFixed.PresentationLayer.gstrPuntoyComa;
            objReplaceEquipmentSaveModel.SaveSuccessfully = Functions.GetValueFromConfigFile("strMsgSuccessTransaction",
                KEY.AppSettings("strConstArchivoSIACUTLTEConfigMsg"));

            #region Trama

            var strTrama = new System.Text.StringBuilder();

            List<Decoder> ListaDecos = new List<Decoder>();

            foreach (CustomerEquipment equipment in objRequestDataModel.lstEquimentsAssociate)
            {
                Decoder objDeco;
                if (equipment.EquipmentServiceType == ConstantsFixed.PresentationLayer.TipoProduco.LTE &&
                    equipment.Action == ConstantsFixed.Letter_C)
                {
                    objDeco = new Decoder();
                    objDeco.descripcion_material = equipment.EquipmentDescription;
                    objDeco.tipoServicio =
                        equipment.EquipmentServiceType == ConstantsFixed.PresentationLayer.TipoProduco.TVSatelital
                            ? ConstantsFixed.PresentationLayer.TipoProduco.DTH
                            : equipment.EquipmentServiceType;
                    objDeco.servicio_principal = equipment.EquipmentType;
                    objDeco.numero_serie = equipment.EquipmentSeriesNumber;
                    objDeco.numero = KEY.AppSettings("strActionComplement");
                    objDeco.componente = equipment.OperationCode;
                    ListaDecos.Add(objDeco);
                }
                else
                {
                    //Trama
                    strTrama.Append(string.Empty);
                    strTrama.Append(fieldSeparator);
                    strTrama.Append(string.Empty);
                    strTrama.Append(fieldSeparator);
                    strTrama.Append(string.Empty);
                    strTrama.Append(fieldSeparator);
                    strTrama.Append(string.Empty);
                    strTrama.Append(fieldSeparator);
                    strTrama.Append(string.Empty);
                    strTrama.Append(fieldSeparator);
                    strTrama.Append(string.Empty);
                    strTrama.Append(fieldSeparator);
                    strTrama.Append(string.Empty);
                    strTrama.Append(fieldSeparator);
                    strTrama.Append(equipment.EquipmentTypeBD);
                    strTrama.Append(fieldSeparator);
                    strTrama.Append(equipment.EquipmentTypeCodeBD);
                    strTrama.Append(fieldSeparator);
                    strTrama.Append(ConstantsFixed.PresentationLayer.NumeracionUNO);
                    strTrama.Append(fieldSeparator);
                    strTrama.Append(equipment.EquipmentDescription);
                    strTrama.Append(fieldSeparator);
                    strTrama.Append(string.Empty);
                    strTrama.Append(fieldSeparator);
                    strTrama.Append(DateTime.Now);
                    strTrama.Append(fieldSeparator);
                    strTrama.Append(objRequestDataModel.strCodeUser);
                    strTrama.Append(fieldSeparator);
                    strTrama.Append(string.Empty);
                    strTrama.Append(fieldSeparator);
                    strTrama.Append(string.Empty);
                    strTrama.Append(fieldSeparator);
                    strTrama.Append(string.Empty);
                    strTrama.Append(fieldSeparator);
                    if (equipment.Action != ConstantsFixed.Letter_C)
                    {
                        strTrama.Append(equipment.Action == KEY.AppSettings("strActionDisassociate")
                            ? ConstantsFixed.Letter_B
                            : ConstantsFixed.Letter_A);

                        objDeco = new Decoder
                        {
                            descripcion_material = equipment.EquipmentDescription,
                            tipoServicio =
                                equipment.EquipmentServiceType == ConstantsFixed.PresentationLayer.TipoProduco.TVSatelital
                                    ? ConstantsFixed.PresentationLayer.TipoProduco.DTH
                                    : equipment.EquipmentServiceType,
                            servicio_principal = equipment.EquipmentType,
                            numero_serie = equipment.EquipmentSeriesNumber,
                            numero = equipment.Action == KEY.AppSettings("strActionDisassociate")
                                ? ConstantsFixed.strLetraB
                                : ConstantsFixed.strLetraA,
                            componente = equipment.OperationCode
                        };
                        ListaDecos.Add(objDeco);
                    }
                    else
                    {
                        strTrama.Append(KEY.AppSettings("strActionComplement"));

                        objDeco = new Decoder
                        {
                            descripcion_material = equipment.EquipmentDescription,
                            tipoServicio =
                                equipment.EquipmentServiceType == ConstantsFixed.PresentationLayer.TipoProduco.TVSatelital
                                    ? ConstantsFixed.PresentationLayer.TipoProduco.DTH
                                    : equipment.EquipmentServiceType,
                            servicio_principal = equipment.EquipmentType,
                            numero_serie = equipment.EquipmentSeriesNumber,
                            numero = KEY.AppSettings("strActionComplement"),
                            componente = equipment.OperationCode
                        };
                        ListaDecos.Add(objDeco);
                    }
                    strTrama.Append(fieldSeparator);
                    strTrama.Append(equipment.EquipmentCodINSSRV);
                    strTrama.Append(fieldSeparator);
                    strTrama.Append(equipment.EquipmentSeriesNumber);
                    strTrama.Append(fieldSeparator);
                    strTrama.Append(equipment.OperationCode);
                    strTrama.Append(fieldSeparatorEnd);
                }
            }

            Claro.Web.Logging.Info(objRequestDataModel.strSessionId, "ReplaceEquipment",
                string.Format("Save() - Trama: {0}", strTrama.ToString()));

            #endregion

            #region ServiceCall

            #region ServiceRequest
            var objChangeEquipmentRequest = new ChangeEquipmentRequest()
            {
                audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(objRequestDataModel.strSessionId),
                idApplication = KEY.AppSettings("ApplicationCode"),
                userSession=objRequestDataModel.strCodeUser,
                customerId = objRequestDataModel.strCustomerID,
                RegistrationReason = objRequestDataModel.strContractId,
                apellidos = objRequestDataModel.strFullName,
                departamento = objRequestDataModel.strDepartament,
                provincia = objRequestDataModel.strProvince,
                distrito = objRequestDataModel.strDistrict,
                domicilio = objRequestDataModel.strAddress,
                modalidad = KEY.AppSettings("gConstKeyStrModalidad"),
                nombres = objRequestDataModel.strFullName,
                documentNumber = objRequestDataModel.strDocumentNumber,
                tipoDoc = objRequestDataModel.strDocumentType,
                usuario = objRequestDataModel.strCodeUser,
                razonSocial = string.Empty,
                flagReg = ConstantsFixed.strUno,
                formatoConstancia = GetGenerateConstancyXml(objRequestDataModel),
                directory = KEY.AppSettings("strCarpetaReposiciondeEquipoLTE"),
                fileName = KEY.AppSettings("strNombreArchReposiciondeEquipoLTE"),
                flagContingencia = KEY.AppSettings("gConstContingenciaClarify") != ConstantsFixed.blcasosVariableSI ? ConstantsFixed.strCero : ConstantsFixed.strUno,

                account = string.Empty,
                agente = objRequestDataModel.strCodeUser,
                phone = KEY.AppSettings("gConstKeyCustomerInteract") + objRequestDataModel.strCustomerID,
                siteobjid = string.Empty,
                usrProceso = KEY.AppSettings("USRProcesoSU"),
                tipo = objRequestDataModel.objTypification.Type,
                clase = objRequestDataModel.objTypification.Class,
                subClase = objRequestDataModel.objTypification.SubClass,
                tipoInter = KEY.AppSettings("AtencionDefault"),
                flagCaso = ConstantsFixed.strCero,
                metodoContacto = KEY.AppSettings("MetodoContactoCartaDefault"),
                resultado = KEY.AppSettings("Ninguno"),
                hechoEnUno = ConstantsFixed.strCero,
                notas = string.IsNullOrEmpty(objRequestDataModel.strNote) ? string.Empty : objRequestDataModel.strNote,
                codPlano = string.Empty,
                inconven = string.Empty,
                inconvenCode = string.Empty,
                valor1 = string.Empty,
                valor2 = string.Empty,
                inter21 = objRequestDataModel.strCustomerType,
                inter1 = objRequestDataModel.strCustomerID,
                firstName = objRequestDataModel.strFullName,
                claroLdn1 = objRequestDataModel.strDocumentNumber,
                TypeDocument = objRequestDataModel.strDocumentType,
                inter3 = Functions.CheckStr(DateTime.Now),
                emailConfirmation = objRequestDataModel.strCheckEmail,
                inter15 = string.IsNullOrEmpty(objRequestDataModel.strCacDacDescription) ? string.Empty : objRequestDataModel.strCacDacDescription,
                inter29 = string.Empty,
                email = string.IsNullOrEmpty(objRequestDataModel.strSendEmail) ? string.Empty : objRequestDataModel.strSendEmail,
                chargeAmount = objRequestDataModel.strAmmountToPay,
                address = objRequestDataModel.strAddress,
                ReferenceAddress = objRequestDataModel.strAddressNotes,
                inter16 = objRequestDataModel.strDepartament,
                inter17 = objRequestDataModel.strDistrict,
                inter18 = objRequestDataModel.strCountry,
                inter19 = objRequestDataModel.strProvince,
                inter20 = objRequestDataModel.strCustomerID,
                inter30 = string.IsNullOrEmpty(objRequestDataModel.strNote) ? string.Empty : objRequestDataModel.strNote,
                observacion = string.IsNullOrEmpty(objRequestDataModel.strNote) ? string.Empty : objRequestDataModel.strNote,
                cargo = objRequestDataModel.strAmmountToPay,
                codCaso = string.Empty,
                codeDif = ConstantsFixed.strCero,
                codIntercaso = string.Empty,
                codMotot = objRequestDataModel.strMotiveSOTCode,
                idInteraccion = string.Empty,
                tipoTrans = KEY.AppSettings("strTipTransReposicionEquipoLTE"),
                tipTra = objRequestDataModel.strTypeWorkCode,
                idProcess = string.Empty,
                flagCharge = ConstantsFixed.strCero,
                trama = strTrama.ToString(),
                coId = objRequestDataModel.strContractId,
                numDoc = objRequestDataModel.strDocumentNumber,
                fecProg = DateTime.Now.ToString("dd/MM/yyyy"),
                tipoVia = ConstantsFixed.strCero,
                nomVia = string.Empty,
                numVia = string.Empty,
                tipUrb = ConstantsFixed.strCero,
                nomUrb = string.Empty,
                manzana = string.Empty,
                lote = string.Empty,
                ubigeo = string.Empty,
                codZona = ConstantsFixed.strCero,
                referencia = string.Empty,
                franjaHor = string.Empty,
                numCarta = ConstantsFixed.strCero,
                operador = ConstantsFixed.strCero,
                preSuscrito = ConstantsFixed.strCero,
                publicar = ConstantsFixed.strCero,
                tmCode = ConstantsFixed.strCero,
                lstTipEqu = string.Empty,
                lstCoser = string.Empty,
                lstSncode = string.Empty,
                lstSpcode = string.Empty,
                usuReg = string.Empty,
                tipoServicio = string.Empty,
                flagActDirFact = string.Empty,
                tipoProducto = string.Empty,
                inter7 = objRequestDataModel.strMotiveSOT,
                asunto = Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strCorreoAsunto", ConfigurationManager.AppSettings("strConstArchivoSIACUTFIXEDConfigMsg")),
                mensaje = Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strCorreoMensaje", ConfigurationManager.AppSettings("strConstArchivoSIACUTFIXEDConfigMsg")),
                ListDetService = ListaDecos,
                strTransactionAudit = ConfigurationManager.AppSettings("gConstKeyCodTransReplaceEquipment"),
                strService = ConfigurationManager.AppSettings("gConstEvtServicio"),
                strIpClient = Functions.CheckStr(HttpContext.Request.UserHostAddress),
                strIpServer = App_Code.Common.GetApplicationIp(),
                strNameServer = App_Code.Common.GetApplicationName(),
                strAmount = Claro.Constants.NumberZeroString,
                strText = string.Format("{0}/Ip Cliente: {1}/Usuario: {2}/Id Opcion: {3}/Fecha y Hora: {4}", string.Format("Codigo Contrato: {0}/MSISDN: {1}", objRequestDataModel.strContractId, objRequestDataModel.strNumberTelephone), Functions.CheckStr(HttpContext.Request.UserHostAddress), objRequestDataModel.strAccount, ConfigurationManager.AppSettings("strIdOpcionClaroProteccion"), DateTime.Now.ToShortDateString()),
                strNameClient = objRequestDataModel.strFullName,
                claroLocal1 = objRequestDataModel.strReferencia,
                //strTransaccion =  = KEY.AppSettings("strNombreTransaccionReposicionEquipo")
            };
            Claro.Web.Logging.Info(objRequestDataModel.strSessionId, "ReplaceEquipment: ", string.Format("Save(): RequestForService - CustomerContractID: {0} - CustomerID: {1} - CodigoTipTrabajo: {2} - SOTNumber: {3}", objRequestDataModel.strContractId, objRequestDataModel.strCustomerID, objRequestDataModel.strTypeWorkCode, objRequestDataModel.strSOTNumber));

            #endregion

            #region ServiceResponse

            try
            {
                var objChangeEquipmentResponse = Claro.Web.Logging.ExecuteMethod<ChangeEquipmentResponse>(
                    () => new FixedTransacServiceClient().GetExecuteChangeEquipment(objChangeEquipmentRequest)
                    );

                if (!string.IsNullOrEmpty(objChangeEquipmentResponse.numeroSOT))
                {
                    objReplaceEquipmentSaveModel.strFilePathConstancy = objChangeEquipmentResponse.rutaConstancia;
                    objReplaceEquipmentSaveModel.bErrorTransac = false;
                    objReplaceEquipmentSaveModel.strSOTNumber = objChangeEquipmentResponse.numeroSOT;
                    objReplaceEquipmentSaveModel.strResultCode = objChangeEquipmentResponse.codeResponse;
                    objReplaceEquipmentSaveModel.strResultMessage = objChangeEquipmentResponse.descriptionResponse;
                    objReplaceEquipmentSaveModel.strMessageErrorTransac = Functions.GetValueFromConfigFile("strMsjTranGrabSatis", KEY.AppSettings("strConstArchivoSIACUTLTEConfigMsg"));
                    Claro.Web.Logging.Info(objRequestDataModel.strSessionId, "ReplaceEquipment: ", string.Format("Save():ResponseFromService - NumSOT: {0} - IdInteracción: {1} - Ruta de Contancia: {2}", objChangeEquipmentResponse.numeroSOT, objChangeEquipmentResponse.idInteraccion, objChangeEquipmentResponse.rutaConstancia));
                    Claro.Web.Logging.Info(objRequestDataModel.strSessionId, "ReplaceEquipment: ", string.Format("Save():ResponseFromService - strResultMessage: {0} ", !string.IsNullOrEmpty(objReplaceEquipmentSaveModel.strResultMessage) ? objReplaceEquipmentSaveModel.strResultMessage : ConstantsFixed.CriterioMensajeOK));
                }
                else
                {
                    if (string.IsNullOrEmpty(objChangeEquipmentResponse.codeResponse) && string.IsNullOrEmpty(objChangeEquipmentResponse.descriptionResponse))
                    {
                        objReplaceEquipmentSaveModel.bErrorTransac = true;
                        objReplaceEquipmentSaveModel.strResultMessage = Functions.GetValueFromConfigFile("strMsjServiceNotDis", KEY.AppSettings("strConstArchivoSIACUTLTEConfigMsg"));
                        Claro.Web.Logging.Info(objRequestDataModel.strSessionId, "ReplaceEquipment: ", string.Format("Save():ErrorGeneracionSOT - Codigo de Respuesta: {0} - Mensaje: {1} - ", objChangeEquipmentResponse.codeResponse, objReplaceEquipmentSaveModel.strResultMessage));
                    }else{
                        objReplaceEquipmentSaveModel.bErrorTransac = true;
                        objReplaceEquipmentSaveModel.strResultCode = objChangeEquipmentResponse.codeResponse;
                        objReplaceEquipmentSaveModel.strResultMessage = objChangeEquipmentResponse.descriptionResponse;
                        objReplaceEquipmentSaveModel.strMessageErrorTransac = Functions.GetValueFromConfigFile("strMensajeDeError", KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                        Claro.Web.Logging.Info(objRequestDataModel.strSessionId, "ReplaceEquipment: ", string.Format("Save():ErrorGeneracionSOT - Codigo de Respuesta: {0} - Mensaje: {1} - IdInteración: {2} - ", objChangeEquipmentResponse.codeResponse, objChangeEquipmentResponse.descriptionResponse, objChangeEquipmentResponse.idInteraccion));
                    }
                }
            }
            catch (Exception ex)
            {
                objReplaceEquipmentSaveModel.strMessageErrorTransac = Functions.GetValueFromConfigFile("strMensajeDeError", KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                Claro.Web.Logging.Error(objRequestDataModel.strSessionId, "ReplaceEquipment", string.Format("Save() - Error: {0}", ex.Message));
            }

            #endregion

            #endregion

            return Json(new { data = objReplaceEquipmentSaveModel }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region  Constancia
        public string GetGenerateConstancyXml( ReplaceEquipmentSaveModel objRequestDataModel)
        {
            string xmlConstancyPDF = string.Empty;
            try
            {
                string pathFileXml = KEY.AppSettings("strRutaXmlConstanciaCambioEquipo");
                var listParamConstancyPdf = new List<string>
                {
                    KEY.AppSettings("strNombreFormatoCambiodeEquipoLTE"),
                    KEY.AppSettings("strNombreTransaccionReposicionEquipo"),
                    objRequestDataModel.strCacDacDescription,
                    objRequestDataModel.strFullName,
                    objRequestDataModel.strFullName,
                    objRequestDataModel.strDocumentType,
                    Functions.CheckStr(DateTime.Now),
                    "$CodigoInteraccion",
                    objRequestDataModel.strContractId,
                    objRequestDataModel.strDocumentNumber,
                    "Reposicion de Equipo",//KEY.AppSettings("strNombreTransaccionCambioEquipo"),
                    "S/. "+objRequestDataModel.strAmmountToPay,
                    "$CodigoSot",
                    objRequestDataModel.strMotiveSOT,
                    objRequestDataModel.strAddress,
                    objRequestDataModel.strAddressNotes,
                    objRequestDataModel.strCountry,
                    objRequestDataModel.strProvince,
                    objRequestDataModel.strDepartament,
                    objRequestDataModel.strDistrict,
                    objRequestDataModel.strCheckEmail,
                    objRequestDataModel.strSendEmail,
                    Functions.GetValueFromConfigFile("ChangeEquipmentCommercial2",
                        KEY.AppSettings("strConstArchivoSIACUTLTEConfigMsg")),
                    objRequestDataModel.strCodeUser,
                    objRequestDataModel.strfullNameUser
                };
                var listLabels = GetXmlToString(App_Code.Common.GetApplicationRoute() + pathFileXml);
                var count = 0;
                var xmlGenerated = new System.Text.StringBuilder();
                foreach (string key in listLabels)
                {
                    xmlGenerated.Append(string.Format("<{0}>{1}</{2}>\r\n", key, listParamConstancyPdf[count], key));
                    count++;
                }

                var xmlGeneratedGrilla = new System.Text.StringBuilder();

                string strCadena = "";
                string[] strCadenaSplit;
                var secuencia = 0;
                var cantidad = 0;

                foreach (var item in objRequestDataModel.lstEquimentsAssociate.OrderBy(x => x.OperationCode).ToList())
                {
                    if (secuencia != int.Parse(item.OperationCode))
                    {
                        cantidad = 0;
                        foreach (var where in objRequestDataModel.lstEquimentsAssociate.Where(y => y.OperationCode == item.OperationCode).ToList())
                        {
                            strCadena = strCadena + where.EquipmentType + "|";
                            cantidad = cantidad + 1;
                        }
                        strCadenaSplit = strCadena.Split(new string[] { "|" }, StringSplitOptions.None);
                        if (cantidad == 3)
                        {
                            xmlGeneratedGrilla.Append(string.Format("<CAMBIO_EQUIPOS>Reposición: {0}</CAMBIO_EQUIPOS>\r\n", strCadenaSplit[1]));
                        }
                        else
                        {
                            xmlGeneratedGrilla.Append(string.Format("<CAMBIO_EQUIPOS>Reposición: {0}</CAMBIO_EQUIPOS>\r\n", strCadenaSplit[1] + " + " + strCadenaSplit[0]));
                        }
                        strCadenaSplit = null;
                    }
                    secuencia = int.Parse(item.OperationCode);

                    xmlGeneratedGrilla.Append(string.Format("<TIPO_SERVICIO_GRILLA>{0}</TIPO_SERVICIO_GRILLA>\r\n", item.EquipmentServiceType));
                    xmlGeneratedGrilla.Append(string.Format("<TIPO_GRILLA>{0}</TIPO_GRILLA>\r\n", item.EquipmentType));
                    xmlGeneratedGrilla.Append(string.Format("<SERIE_GRILLA>{0}</SERIE_GRILLA>\r\n", item.EquipmentSeriesNumber));
                    xmlGeneratedGrilla.Append(string.Format("<EQUIPO_GRILLA>{0}</EQUIPO_GRILLA>\r\n", item.EquipmentDescription));
                    xmlGeneratedGrilla.Append(string.Format("<ACCION_GRILLA>{0}</ACCION_GRILLA>\r\n", item.Action == KEY.AppSettings("strActionDisassociate")
                            ? "Desasociado"
                            : item.Action == KEY.AppSettings("strActionAssociate")
                                ? "Asociado"
                                : "Impactado"));
                }

                xmlConstancyPDF = String.Format("<?xml version='1.0' encoding='utf-8'?><PLANTILLA>{0}{1}</PLANTILLA>", xmlGenerated, xmlGeneratedGrilla);
                Claro.Web.Logging.Info(objRequestDataModel.strSessionId, "GetGenerateConstancyXml()", "xmlConstancyPDF:    " + xmlConstancyPDF);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(objRequestDataModel.strSessionId, "GetGenerateConstancyXml()", "ERROR:    " + ex.InnerException);
            }

            return xmlConstancyPDF.ToString();
        }
        #endregion
    }
}