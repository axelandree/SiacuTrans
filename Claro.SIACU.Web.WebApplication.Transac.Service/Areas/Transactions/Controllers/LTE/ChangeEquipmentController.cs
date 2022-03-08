using Claro.SIACU.Transac.Service;
using Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MODELS = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models;
using AuditRequestFixed = Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService.AuditRequest;
using Model = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models;
using AuditRequestCommon = Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService.AuditRequest;
using KEY = Claro.ConfigurationManager;
using ConstantsLTE = Claro.SIACU.Transac.Service.Constants;
using HELPERS = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers;

using COMMON = Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService;
using FIXED = Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService;
using FunctionsSIACU = Claro.SIACU.Transac.Service.Functions;
using Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models.LTE;
using Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models;
using Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.LTE.ChangeEquipment;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Controllers.LTE
{
    public class ChangeEquipmentController : CommonServicesController
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
        public ActionResult LTEChangeEquipment()
        {
            return PartialView();
        }
        public ActionResult LTEChangeEquipmentAssociate(string currentEquipmentTypeSelected, string limitEquipmentsPerCustomer, string associateEquipmentsPerCustomer, string strEquipmentAssociateUsed)
        {
            _strCurrentEquipmentTypeSelected = currentEquipmentTypeSelected;
            _strEquipmentslimit = limitEquipmentsPerCustomer;
            _strEquipmentsAssociate = associateEquipmentsPerCustomer;
            _strEquipmentAssociateUsed = strEquipmentAssociateUsed;
            return PartialView("~/Areas/Transactions/Views/ChangeEquipment/LTEChangeEquipmentAssociate.cshtml");
        }
        #endregion

        #region LoadMethods
        [HttpPost]
        public JsonResult LTEChangeEquipmentLoad(RequestDataModel objModel)
        {
            var objChangeEquipmentLoadModel = new ChangeEquipmentLoadModel();
            try
            {
                Claro.Web.Logging.Info(objModel.strIdSession, "ChangeEquipment: ", string.Format("ExecuteLTEChangeEquipmentLoad() - CustomerContractID: {0}", objModel.strIdContract));

                objChangeEquipmentLoadModel.objChangeEquipmentMessageModel = GetMessageObj(objModel.strIdSession);

                objChangeEquipmentLoadModel.strUserCac = GetUsersStr(objModel.strIdSession, objModel.strCodeUser);
                objChangeEquipmentLoadModel.lstCacDacTypes = GetListCacDac(objModel.strIdSession);
                objChangeEquipmentLoadModel.lstSOTListypes = GetListSot(objModel.strIdSession, objModel.strCustomerId, objModel.strIdContract);
               
                #region ChargeTypification
                var objTypificationModel = new TypificationModel();
                var strMessageResult = string.Empty;
                var objtipification = LoadTypifications(objModel.strIdSession, GetValueConfig("strCodigosCambioDeEquipoLTE", objModel.strIdSession, ConfigurationManager.AppSettings("strNombreTransaccionCambioEquipo")), ref strMessageResult);
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

                objChangeEquipmentLoadModel.strAmountToPay = GetValueConfig("strMontoAPagarCambioEquipoLTE", objModel.strIdSession, KEY.AppSettings("strNombreTransaccionCambioEquipo"));
                objChangeEquipmentLoadModel.objTypification = objTypificationModel;
                objChangeEquipmentLoadModel.lstBusinessRules = GetBusinessRulesLst(objModel.strIdSession, objTypificationModel.SubClassCode);
                objChangeEquipmentLoadModel.lstServiceTypes = GetServiceTypesList(objModel.strIdSession,
                    KEY.AppSettings("strCodigoTiposDeServicio"));
                objChangeEquipmentLoadModel.lstTypeWork = GetTypeWorkLteList(objModel.strIdSession, Convert.ToInt(KEY.AppSettings("strTipoTrabajoCambioEquipoLTE")));
                objChangeEquipmentLoadModel.lstMotiveSOTByTypeJob = GetMotiveSotByTypeJob(objModel.strIdSession, Convert.ToInt(objChangeEquipmentLoadModel.lstTypeWork.First().Code));

                _lstServiceTypes = objChangeEquipmentLoadModel.lstServiceTypes;
                objChangeEquipmentLoadModel.strActiveFullChangeEquipmentForLTE = KEY.AppSettings("strActiveFullChangeEquipmentForLTE");

                objChangeEquipmentLoadModel.lstEquimentAssociate = GetListDataProducts(objModel.strIdSession,objModel.strCustomerId, objModel.strIdContract);
                //objChangeEquipmentLoadModel.lstEquimentAssociate.Sort((x, y) => string.Compare(x.EquipmentAssociate, y.EquipmentAssociate, StringComparison.Ordinal));
                objChangeEquipmentLoadModel.lstEquimentAssociate = objChangeEquipmentLoadModel.lstEquimentAssociate.OrderBy(p => p.EquipmentAssociate).ThenBy(p => p.EquipmentType).ToList();

                objChangeEquipmentLoadModel.strServidorLeerPDF = KEY.AppSettings("strServidorLeerPDF");
                var lstDecos = new List<DecoMatriz>();
                var strDecosMax = String.Empty;
                GetDecoMatriz(objModel.strIdSession, objModel.strTypeProduct, ref lstDecos, ref strDecosMax);
                objChangeEquipmentLoadModel.lstWeight = lstDecos;
                objChangeEquipmentLoadModel.strDecosMax = strDecosMax;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objModel.strIdSession, "ChangeEquipment", string.Format("ExecuteLTEChangeEquipmentLoad() - Error: {0}", ex.Message));
            }

            return Json(new { data = objChangeEquipmentLoadModel }, JsonRequestBehavior.AllowGet);
            }

        public JsonResult LTEChangeEquipmentLoadModalAssociate()
        {
            var objChangeEquipmentLoadModel = new ChangeEquipmentLoadModel();
            var objChangeEquipmentMessageModel = new ChangeEquipmentMessageModel();
            objChangeEquipmentLoadModel.lstServiceTypes = _lstServiceTypes;
            
            objChangeEquipmentLoadModel.strCurrentEquipmentTypeSelected = _strCurrentEquipmentTypeSelected;
            objChangeEquipmentLoadModel.strActiveFullChangeEquipmentForLTE = KEY.AppSettings("strActiveFullChangeEquipmentForLTE");

            objChangeEquipmentMessageModel.strMsgCheckFullChange = Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strMessageCheckFullChange", ConfigurationManager.AppSettings("strConstArchivoSIACUTLTEConfigMsg"));
            objChangeEquipmentMessageModel.strMsjValidacionCargadoListado = Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strMsjValidacionCargadoListado", ConfigurationManager.AppSettings("strConstArchivoSIACUTLTEConfigMsg"));

            objChangeEquipmentLoadModel.objChangeEquipmentMessageModel = objChangeEquipmentMessageModel;
            objChangeEquipmentLoadModel.strEquipmentLimits = _strEquipmentslimit;
            objChangeEquipmentLoadModel.strEquipmentAssociate = _strEquipmentsAssociate;
            objChangeEquipmentLoadModel.strEquipmentAssociateUsed = _strEquipmentAssociateUsed;
            return Json(new { data = objChangeEquipmentLoadModel }, JsonRequestBehavior.AllowGet);
        }

        private void GetDecoMatriz(string strIdSession, string strTypeProduct, ref List<DecoMatriz> lstDecos, ref string strDecosMax)
        {
            Claro.Web.Logging.Info(strIdSession, strIdSession, "In GetDecoMatriz - LTE");
            DecoMatrizResponse objDecoMatrizResponse = null;
            //string numDecosMax = "0";

            var audit = App_Code.Common.CreateAuditRequest<AuditRequest>(strIdSession);
            var objDecoMatrizRequest = new DecoMatrizRequest
            {
                audit = audit,
                strTipoProducto = strTypeProduct
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

            if (objDecoMatrizResponse != null)
            {
                lstDecos = objDecoMatrizResponse.ListaMatrizDecos;
                strDecosMax = objDecoMatrizResponse.CantidadMaxima;
            }

            Claro.Web.Logging.Info(strIdSession, strIdSession, "Out GetDecoMatriz - LTE");

        }
        
        #region GetMessageObj
        public ChangeEquipmentMessageModel GetMessageObj(string strIdSession)
        {
            Claro.Web.Logging.Info(strIdSession, "ChangeEquipmentLTEChange",  "GetMessageObj");
            var objChangeEquipmentMessageModel = new ChangeEquipmentMessageModel
            {
                strStateLine = Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strStateLine",ConfigurationManager.AppSettings("strConstArchivoSIACUTLTEConfigMsg")),
                strMsgStateLine = Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strMsgStateLine",ConfigurationManager.AppSettings("strConstArchivoSIACUTLTEConfigMsg")),
                strVariableEmpty = Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strVariableEmpty",ConfigurationManager.AppSettings("strConstArchivoSIACUTLTEConfigMsg")),
                strMsgVariableEmpty = Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strMsgVariableEmpty", ConfigurationManager.AppSettings("strConstArchivoSIACUTLTEConfigMsg")),
                strMsjValidacionCampoSlt = Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strMsjValidacionCampoSlt", ConfigurationManager.AppSettings("strConstArchivoSIACUTLTEConfigMsg")),
                strMsjValidacionCampo = Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strMsjValidacionCampo", ConfigurationManager.AppSettings("strConstArchivoSIACUTLTEConfigMsg")),
                strMsjValidacionCampoFormato = Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strMsjValidacionCampoFormato",ConfigurationManager.AppSettings("strConstArchivoSIACUTLTEConfigMsg")),
                strMsjValidacionCargadoListado = Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strMsjValidacionCargadoListado",ConfigurationManager.AppSettings("strConstArchivoSIACUTLTEConfigMsg")),
                strMessageSummary = Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strMessageSummary",ConfigurationManager.AppSettings("strConstArchivoSIACUTLTEConfigMsg")),
                strValidateEquipmentActive = Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strValidateEquipmentActive",ConfigurationManager.AppSettings("strConstArchivoSIACUTLTEConfigMsg")),
                strValidateEquipmentNotFound = Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strValidateEquipmentNotFound",ConfigurationManager.AppSettings("strConstArchivoSIACUTLTEConfigMsg")),
                strValidateEquipmentTechnicalErrors = Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strValidateEquipmentTechnicalErrors",ConfigurationManager.AppSettings("strConstArchivoSIACUTLTEConfigMsg")),
                strValidateEquipmentContractInvalid = Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strValidateEquipmentContractInvalid",ConfigurationManager.AppSettings("strConstArchivoSIACUTLTEConfigMsg")),
                strValidateEquipmentIMSI = Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strValidateEquipmentIMSI", ConfigurationManager.AppSettings("strConstArchivoSIACUTLTEConfigMsg")),
                strValidateEquipmentICCID = Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strValidateEquipmentICCID", ConfigurationManager.AppSettings("strConstArchivoSIACUTLTEConfigMsg")),
                strValidateEquipmentICCIDNotExist = Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strValidateEquipmentICCIDNotExist",ConfigurationManager.AppSettings("strConstArchivoSIACUTLTEConfigMsg")),
                strValidateEquipmentMoreThanOneEquipment =Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strValidateEquipmentMoreThanOneEquipment",ConfigurationManager.AppSettings("strConstArchivoSIACUTLTEConfigMsg")),           
                strValidateEquipmentContractNotExist = Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strValidateEquipmentContractNotExist", ConfigurationManager.AppSettings("strConstArchivoSIACUTLTEConfigMsg")),
                strValidateEquipmentNotReceivedNewData = Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strValidateEquipmentNotReceivedNewData", ConfigurationManager.AppSettings("strConstArchivoSIACUTLTEConfigMsg")),
                strValidateEquipmentResponseCodeInvalid = Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strValidateEquipmentResponseCodeInvalid", ConfigurationManager.AppSettings("strConstArchivoSIACUTLTEConfigMsg")),
                strValidateIncompleteFields = Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strValidateIncompleteFields", ConfigurationManager.AppSettings("strConstArchivoSIACUTLTEConfigMsg")),
                strConstancyError = Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strConstancyError", ConfigurationManager.AppSettings("strConstArchivoSIACUTLTEConfigMsg")),
                strEquipmentNotSelected = Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strEquipmentNotSelected", ConfigurationManager.AppSettings("strConstArchivoSIACUTLTEConfigMsg")),
                strEquipmentBlockToSelected = Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strEquipmentBlockToSelected", ConfigurationManager.AppSettings("strConstArchivoSIACUTLTEConfigMsg")),
                strValidateSameDecoType = Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strValidateSameDecoType", ConfigurationManager.AppSettings("strConstArchivoSIACUTLTEConfigMsg")),
                strSeriesNumberNotEqual = Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strSeriesNumberNotEqual", ConfigurationManager.AppSettings("strConstArchivoSIACUTLTEConfigMsg")),
                strNotSeriesInput = Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strNotSeriesInput", ConfigurationManager.AppSettings("strConstArchivoSIACUTLTEConfigMsg")),
                strLimitEquipments = Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strLimitEquipments", ConfigurationManager.AppSettings("strConstArchivoSIACUTLTEConfigMsg")),
                strEqualEquipmentExistInOperation = Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strEqualEquipmentExistInOperation", ConfigurationManager.AppSettings("strConstArchivoSIACUTLTEConfigMsg")),
                strSuccessModification = Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strSuccessModification", ConfigurationManager.AppSettings("strConstArchivoSIACUTLTEConfigMsg")),
                strMsjErrorAlCargar = Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strMsjErrorAlCargar", ConfigurationManager.AppSettings("strConstArchivoSIACUTLTEConfigMsg")),
                strValidateCombinationDecoType = Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strValidateCombinationDecoType", ConfigurationManager.AppSettings("strConstArchivoSIACUTLTEConfigMsg")),
                strValidateWeightDecoType = Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strValidateWeightDecoType", ConfigurationManager.AppSettings("strConstArchivoSIACUTLTEConfigMsg")),

            };
            return objChangeEquipmentMessageModel;
        }
        #endregion

        #endregion

        #region GetProductsList

        public List<HELPERS.LTE.ChangeEquipment.CustomerEquipment> GetListDataProducts(string strIdSession, string CUSTOMER_ID, string CONTRATO_ID)
        {
            Claro.Web.Logging.Info(strIdSession, "ChangeEquipment", string.Format("ExecuteGetListDataProducts() - CustomerID: {0} - ContractID: {1}", CUSTOMER_ID, CONTRATO_ID));
            List<HELPERS.LTE.ChangeEquipment.CustomerEquipment> listDataProducts = new List<HELPERS.LTE.ChangeEquipment.CustomerEquipment>();

            var audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);
            ServicesLteFixedRequest objServicesLteFixedRequest = new FixedTransacService.ServicesLteFixedRequest() 
            {
                audit = audit,
                strCoid = CONTRATO_ID,
                strCustomerId = CUSTOMER_ID 
            };

            try
            {
                var objServicesLteFixedResponse = Claro.Web.Logging.ExecuteMethod<ServicesLteFixedResponse>(
                    () => _oServiceFixed.GetCustomerEquipments(objServicesLteFixedRequest));


                if (objServicesLteFixedResponse.ListServicesLte.Count > 0)
                {

                    foreach (BEDeco item in objServicesLteFixedResponse.ListServicesLte)
                    {
                        listDataProducts.Add(new HELPERS.LTE.ChangeEquipment.CustomerEquipment()
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
                            OperationCode = string.Empty,
                            EquipmentTypeBD = item.tipequ,
                            EquipmentTypeCodeBD = item.codtipequ,
                            EquipmentCodINSSRV = item.codinssrv
                        });
                    }
                    Claro.Web.Logging.Info(strIdSession, strIdSession, string.Format("Cambio de Equipo - ExecutingGetListDataProducts() - ListDataProducts LTE: {0}", listDataProducts.Count.ToString()));
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objServicesLteFixedRequest.audit.transaction, ex.Message);
            }
            Claro.Web.Logging.Info(strIdSession, strIdSession, string.Format("Cambio de Equipo - EndGetListDataProducts() - CustomerID: {0} - ContractID: {1}", CUSTOMER_ID, CONTRATO_ID));
            return listDataProducts;
        }

        #endregion

        #region GetListSOT
        public List<COMMON.ListItem> GetListSot(string strIdSession, string CUSTOMER_ID, string CONTRATO_ID)
        {
            Claro.Web.Logging.Info(strIdSession, "ChangeEquipment", string.Format("GetListSot() - CustomerID: {0} - ContractID: {1}", CUSTOMER_ID, CONTRATO_ID));
            List<COMMON.ListItem> listDataSots = new List<COMMON.ListItem>();
            var audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
            COMMON.GetSotRequest objSotRequestCommon = new COMMON.GetSotRequest()
            {
                audit = audit,
                COD_ID = CONTRATO_ID,
                CUSTOMER_ID = CUSTOMER_ID
            };
            try
            {
                var objSotResponseCommon = Claro.Web.Logging.ExecuteMethod<COMMON.GetSotResponseCommon>(
                    () => _oServiceCommon.GetSot(objSotRequestCommon));
                if (objSotResponseCommon.ListSot.Count > 0)
                {
                    listDataSots.AddRange(objSotResponseCommon.ListSot.Select(item => new COMMON.ListItem()
                    {
                            Code = item.Code
                    }));
                    Claro.Web.Logging.Info(strIdSession, strIdSession, string.Format("Cambio de Equipo - GetListSot() - ListSot LTE: {0}", listDataSots.Count.ToString()));
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    Claro.Web.Logging.Error(strIdSession, objSotRequestCommon.audit.transaction,
                        ex.InnerException.Message);
            }
            Claro.Web.Logging.Info(strIdSession, strIdSession, string.Format("Cambio de Equipo - GetListSot() - CustomerID: {0} - ContractID: {1}", CUSTOMER_ID, CONTRATO_ID));
            return listDataSots;
        }
        #endregion

        #region GetValidateEquipment

        public JsonResult GetValidateEquipment(ChangeEquipmentValidateModel objModel)
        {
            string[] result = new string[2];
            objToAssociate = new HELPERS.LTE.ChangeEquipment.CustomerEquipment();
            var audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(objModel.strSessionId);
            if (KEY.AppSettings("strValidacionCambioEquipoSANSDB")=="1")
            {
                Claro.Web.Logging.Info(objModel.strSessionId, "ChangeEquipment","FLAG = 1");
                if (objModel.strEquipmentTypeCode == ConstantsLTE.strTres)
                {
                    Claro.Web.Logging.Info(objModel.strSessionId, "ChangeEquipment", "SIMCARD LTE");

                    result = GetAvailabilitySimcardSANS(objModel, audit);
                    Claro.Web.Logging.Info(objModel.strSessionId, "ChangeEquipment", "GetAvailabilitySimcardSANS: " + result[0] + " | DESCRIPCION: " + result[1]);
                    if (result[0] == ConstantsLTE.strCero)
                    {
                        result = GetAvailabilitySimcardBSCS(objModel, audit);
                        Claro.Web.Logging.Info(objModel.strSessionId, "ChangeEquipment", "GetAvailabilitySimcardBSCS: " + result[0] + " | DESCRIPCION: " + result[1]);
                        if (result[0] == ConstantsLTE.strCero)
                        {
                            result = GetValidateSimcardBSCS_HLCODE(objModel, audit);
                            Claro.Web.Logging.Info(objModel.strSessionId, "ChangeEquipment", "GetValidateSimcardBSCS_HLCODE: " + result[0] + " | DESCRIPCION: " + result[1]);
                            if (result[0] == ConstantsLTE.strCero)
                            {
                                Claro.Web.Logging.Info(objModel.strSessionId, "ChangeEquipment", "EQUIPO LTE");
                                result = GetValidateEquipmentSGA(objModel, audit);
                                Claro.Web.Logging.Info(objModel.strSessionId, "ChangeEquipment", "GetValidateEquipmentSGA: " + result[0] + " | DESCRIPCION: " + result[1]);
                            }
                        }
                    }
                }
                else
                {
                    Claro.Web.Logging.Info(objModel.strSessionId, "ChangeEquipment", "EQUIPO LTE");
                    result = GetValidateEquipmentSGA(objModel, audit);
                }

            }
            else
            {
                Claro.Web.Logging.Info(objModel.strSessionId, "ChangeEquipment", "FLAG != 1");
                if (objModel.strEquipmentTypeCode == ConstantsLTE.strTres)
                {
                    Claro.Web.Logging.Info(objModel.strSessionId, "ChangeEquipment", "SIMCARD LTE");
                        result = GetAvailabilitySimcardBSCS(objModel, audit);
                        Claro.Web.Logging.Info(objModel.strSessionId, "ChangeEquipment", "GetAvailabilitySimcardBSCS: " + result[0] + "DESCRIPCION: " + result[1]);
                        if (result[0] == ConstantsLTE.strCero)
                        {
                            result = GetValidateSimcardBSCS_HLCODE(objModel, audit);
                            Claro.Web.Logging.Info(objModel.strSessionId, "ChangeEquipment", "GetValidateSimcardBSCS_HLCODE: " + result[0] + " | DESCRIPCION: " + result[1]);
                            if (result[0] == ConstantsLTE.strCero)
                            {
                                result = GetValidateEquipmentSGA(objModel, audit);
                                Claro.Web.Logging.Info(objModel.strSessionId, "ChangeEquipment", "GetValidateEquipmentSGA: " + result[0] + " | DESCRIPCION: " + result[1]);
                            }
                        }
                    
                }
                else
                {
                    Claro.Web.Logging.Info(objModel.strSessionId, "ChangeEquipment", "EQUIPO LTE");
                    result = GetValidateEquipmentSGA(objModel, audit);
                    Claro.Web.Logging.Info(objModel.strSessionId, "ChangeEquipment", "GetValidateEquipmentSGA: " + result[0]+" | DESCRIPCION: " + result[1]);
                }


            }
            if (result[0] == null)
                result[0] = "00";
            objModel.strResultCode = result[0];
            objModel.strResultMessage = result[1];
            objModel.objEquipmentToAssociate = objToAssociate;
            return Json(new { data = objModel }, JsonRequestBehavior.AllowGet);
        }

        #region GetBValidateEquipmentSGA
        public string[] GetValidateEquipmentSGA(ChangeEquipmentValidateModel objRequest, FixedTransacService.AuditRequest audit)
        {
            Claro.Web.Logging.Info(objRequest.strSessionId, "ChangeEquipment", string.Format("GetValidateEquipmentSGA() - EquipmentCodeType: {0} - EquipmentSeriesnumber: {1}", objRequest.strEquipmentTypeCode, objRequest.strEquipmentSeries));

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

                if (objDispEquipmentResponse.ResultCode==ConstantsLTE.strCero)
                {
                    List<Helpers.LTE.ChangeEquipment.CustomerEquipment> LstEquipmentToAssociate = new List<HELPERS.LTE.ChangeEquipment.CustomerEquipment>();
                    if (objDispEquipmentResponse.lstEquipments.Count > 0)
                    {

                        foreach (BEDeco item in objDispEquipmentResponse.lstEquipments)
                        {
                            LstEquipmentToAssociate.Add(new HELPERS.LTE.ChangeEquipment.CustomerEquipment()
                            {
                                EquipmentServiceType = objRequest.strEquipmentTypeCode == ConstantsLTE.PresentationLayer.NumeracionCUATRO || objRequest.strEquipmentTypeCode == ConstantsLTE.PresentationLayer.NumeracionTRES ? ConstantsLTE.PresentationLayer.TipoProduco.LTE : ConstantsLTE.PresentationLayer.TipoProduco.TVSatelital,

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
                        if (objRequest.strEquipmentTypeCode==ConstantsLTE.strDos)
                        {
                            result[0]=GetValidateEquipmentTypeAndWeight(objRequest, LstEquipmentToAssociate[0]);
                        }
                        else
                        {
                            result[0] = objDispEquipmentResponse.ResultCode;
                        }
                      
                    }
                    else
                    {
                        Claro.Web.Logging.Info(objRequest.strSessionId, "ChangeEquipment", string.Format("GetValidateEquipmentSGA() - Result: {0}  - ResultMessage: {1}", objDispEquipmentResponse.ResultCode, objDispEquipmentResponse.ResultMessage));
                        result[0] = ConstantsLTE.strCeroUno;
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
                result[0] = ConstantsLTE.strDobleCero;
                return result;
            }

            result[1] = objDispEquipmentResponse.ResultMessage;
            Claro.Web.Logging.Info(objRequest.strSessionId, "ChangeEquipment", string.Format("GetValidateEquipmentSGA() - Result: {0}  - ResultMessage: {1}", objDispEquipmentResponse.ResultCode,objDispEquipmentResponse.ResultMessage));
            return result;
        }
        #endregion
        #region GetValidateEquipmentTypeAndWeight
        private string GetValidateEquipmentTypeAndWeight(ChangeEquipmentValidateModel objRequest, HELPERS.LTE.ChangeEquipment.CustomerEquipment objToAssociate)
        {
            Claro.Web.Logging.Info(objRequest.strSessionId, "ChangeEquipment", string.Format("GetValidateEquipmentTypeAndWeight() - EquipmentCodeType: {0} - EquipmentSeriesnumber: {1}", objRequest.strEquipmentTypeCode, objRequest.strEquipmentSeries));
            DispEquipmentResponse objDispEquipmentResponse = new DispEquipmentResponse();
            string strResult = ConstantsLTE.strCeroSeis;
            string strAutoritation = ConstantsLTE.strCero;
            var lstDecoWeight = new List<Helpers.LTE.ChangeEquipment.CustomerEquipment>();
            try
            {
                var arrCombinationDecoByType = GetValueConfig("strCombinacionEquiposPorTipo", objRequest.strSessionId, "LTE-CAMBIO DE EQUIPO-Config").Split(';');
                foreach (var objItem in arrCombinationDecoByType)
                {
                    var arrCombinationItem = objItem.Split('|');
                    if (arrCombinationItem[0] == objRequest.strDecoType)
                    {
                        if (arrCombinationItem[1] == objToAssociate.DecoType)
                        {
                            strResult = ConstantsLTE.strCero;
                            strAutoritation = arrCombinationItem[2];
                        }
                    }
                    if (strResult == ConstantsLTE.strCero)
                    {
                        var intQuantityPoint = 0;
                        var lstOnlyDeco = (from x in objRequest.lstEquimentAssociate
                            where x.EquipmentCodeType == "2"
                            select x).ToList();
                        foreach (var objDeco in lstOnlyDeco)
                        {
                            //verificar el numero de serie
                            if (objRequest.lstEquimentsAssociateSummary != null)
                            {
                                var lstDecoSummary = (from x in objRequest.lstEquimentsAssociateSummary
                                    where x.EquipmentCodeType == "2" && x.EquipmentSeriesNumber == objDeco.EquipmentSeriesNumber && x.EquipmentSeriesNumber != objRequest.strOldEquipmentSeriesDeco// && x.EquipmentCodINSSRV== objDeco.EquipmentCodINSSRV
                                    select x).ToList();
                                if (lstDecoSummary.Count > 0)
                                {
                                    var objDecoSummary = lstDecoSummary[0];
                                    var lstDecoSummaryAsociar = (from x in objRequest.lstEquimentsAssociateSummary
                                        where x.EquipmentCodeType == "2" && x.EquipmentSeriesNumber != objDeco.EquipmentSeriesNumber && x.OperationCode == objDecoSummary.OperationCode
                                        select x).ToList();
                                    if (lstDecoSummaryAsociar.Count > 0)
                                        lstDecoWeight.Add(lstDecoSummaryAsociar[0]);
                                }
                                else
                                {
                                    lstDecoWeight.Add(objDeco);
                                }
                            }
                            else
                            {
                                lstDecoWeight.Add(objDeco);
                            }
                            
                               
                        }
                        foreach (var objDeco in lstDecoWeight)//lstOnlyDeco)
                        {
                            foreach (var objWeight in objRequest.lstWeight)
                            {
                                if (objDeco.DecoType == objWeight.Descripcion)
                                {
                                    intQuantityPoint = intQuantityPoint + Convert.ToInt(objWeight.Valor);
                                    break;
                                }
                            }
                        }
                        foreach (var objWeight in objRequest.lstWeight)
                        {
                            if (objToAssociate.DecoType == objWeight.Descripcion)//añadir el nuevo
                                intQuantityPoint = intQuantityPoint + Convert.ToInt(objWeight.Valor);
                            if (objRequest.strDecoType == objWeight.Descripcion)//quitar el anterior
                                intQuantityPoint = intQuantityPoint - Convert.ToInt(objWeight.Valor);

                        }
                        if (intQuantityPoint > Convert.ToInt(objRequest.strDecosMax))
                            strResult = "07";
                        else if (strAutoritation != ConstantsLTE.strCero)
                        {
                            var strPermiso = GetValueConfig("strLlavePermisoCambioEquipoDecoTipo", objRequest.strSessionId, "LTE-CAMBIO DE EQUIPO-Config");
                            var intPositionAccess = objRequest.strPermitions.ToUpper().IndexOf(strPermiso.ToUpper(), StringComparison.OrdinalIgnoreCase);
                            strResult = (intPositionAccess < 0) ? "08" : "0";
                        }
                        break;//salir del foreach
                    }


                }

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.strSessionId, "Cambio de equipo-GetValidateEquipmentTypeAndWeight", ex.Message);
                strResult = ConstantsLTE.strDobleCero;
                return strResult;
            }

            Claro.Web.Logging.Info(objRequest.strSessionId, "ChangeEquipment", string.Format("GetValidateEquipmentTypeAndWeight() - Result: {0}  - ResultMessage: {1}", objDispEquipmentResponse.ResultCode, objDispEquipmentResponse.ResultMessage));
            return strResult;
        }
        #endregion
        #region GetValidateSimcardSANS
        public string[] GetAvailabilitySimcardSANS(ChangeEquipmentValidateModel objRequest, FixedTransacService.AuditRequest audit)
        {
            Claro.Web.Logging.Info(objRequest.strSessionId, "ChangeEquipment", string.Format("GetValidateSimcardSANS() - ContractID: {0} - EquipmentSerie: {1} - EquipmentTypeCode: {2}", objRequest.strContractId, objRequest.strEquipmentSeries, objRequest.strEquipmentTypeCode));

            AvailabilitySimcardResponse objAvailabilitySimcardResponse = new AvailabilitySimcardResponse();
            string[] result = new string[2];

            AvailabilitySimcardRequest objAvailabilitySimcardRequest = new AvailabilitySimcardRequest()
            {
                SimcardSeries = objRequest.strEquipmentSeries.Substring(ConstantsLTE.numeroCero, objRequest.strEquipmentSeries.Length-2),
                ContractId = objRequest.strContractId,
                audit = audit
            };
            try
            {
                objAvailabilitySimcardResponse = Claro.Web.Logging.ExecuteMethod<AvailabilitySimcardResponse>(
                    () =>_oServiceFixed.GetAvailabilitySimcardSANS(objAvailabilitySimcardRequest)
                    );
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.strSessionId, audit.transaction, ex.InnerException.Message);
            }
            result[0] = objAvailabilitySimcardResponse.ResultCode;
            result[1] = objAvailabilitySimcardResponse.ResultMessage;
            Claro.Web.Logging.Info(objRequest.strSessionId, "ChangeEquipment", string.Format("GetValidateSimcardSANS(): Result: {0} - ResultMessage: {1}", objAvailabilitySimcardResponse.ResultCode, objAvailabilitySimcardResponse.ResultMessage));
            return result;
        }
        #endregion
        #region GetValidateSimcardBSCS
        public string[] GetAvailabilitySimcardBSCS(ChangeEquipmentValidateModel objRequest, FixedTransacService.AuditRequest audit)
        {
            Claro.Web.Logging.Info(objRequest.strSessionId, "ChangeEquipment", string.Format("GetValidateSimcardBSCS() - ContractID: {0} - EquipmentSerie: {1} - EquipmentTypeCode: {2}", objRequest.strContractId, objRequest.strEquipmentSeries, objRequest.strEquipmentTypeCode));

            AvailabilitySimcardResponse objAvailabilitySimcardResponse = new AvailabilitySimcardResponse();
            string[] result =  new string[2];
            AvailabilitySimcardRequest objAvailabilitySimcardRequest = new AvailabilitySimcardRequest()
            {
                SimcardSeries = objRequest.strEquipmentSeries.Substring(ConstantsLTE.numeroCero, objRequest.strEquipmentSeries.Length - 1),
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
            Claro.Web.Logging.Info(objRequest.strSessionId, "ChangeEquipment", string.Format("GetValidateSimcardBSCS(): Result: {0} - ResultMessage: {1}", objAvailabilitySimcardResponse.ResultCode, objAvailabilitySimcardResponse.ResultMessage));
            return result;
        }
        #endregion
        #region GetValidateSimcardBSCS_HLCODE
        public string[] GetValidateSimcardBSCS_HLCODE(ChangeEquipmentValidateModel objRequest, FixedTransacService.AuditRequest audit)
        {
            Claro.Web.Logging.Info(objRequest.strSessionId, "ChangeEquipment", string.Format("GetValidateSimcardBSCS_HLCODE() - ContractID: {0} - EquipmentSerie: {1} - EquipmentTypeCode: {2}", objRequest.strContractId, objRequest.strEquipmentSeries, objRequest.strEquipmentTypeCode));

            AvailabilitySimcardResponse objAvailabilitySimcardResponse = new AvailabilitySimcardResponse();
            string[] result = new string[2];
            AvailabilitySimcardRequest objAvailabilitySimcardRequest = new AvailabilitySimcardRequest()
            {
                SimcardSeries = objRequest.strEquipmentSeries.Substring(ConstantsLTE.numeroCero, objRequest.strEquipmentSeries.Length - 1),
                SimcardSeriesOld = objRequest.strOldEquipmentSeriesNumber.Substring(ConstantsLTE.numeroCero, objRequest.strOldEquipmentSeriesNumber.Length - 1),
                ContractId = objRequest.strContractId,
                audit = audit
            };
            try
            {
                objAvailabilitySimcardResponse = Claro.Web.Logging.ExecuteMethod<AvailabilitySimcardResponse>(
                    () =>_oServiceFixed.GetValidateSimcardBSCS_HLCODE(objAvailabilitySimcardRequest));
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.strSessionId, audit.transaction, ex.Message);
            }
            result[0] = objAvailabilitySimcardResponse.ResultCode;
            result[1] = objAvailabilitySimcardResponse.ResultMessage;
            Claro.Web.Logging.Info(objRequest.strSessionId, "ChangeEquipment", string.Format("GetValidateSimcardBSCS_HLCODE(): Result: {0} - ResultMessage: {1}", objAvailabilitySimcardResponse.ResultCode, objAvailabilitySimcardResponse.ResultMessage));
            return result;
        }
        #endregion
        #endregion

        #region Save

        [HttpPost]
        public JsonResult Save(ChangeEquipmentSaveModel objRequestDataModel)
        {
            var objChangeEquipmentSaveModel = new ChangeEquipmentSaveModel();

            string strResult = string.Empty;
            string fieldSeparator = ConstantsLTE.PresentationLayer.gstrVariablePipeline;
            string fieldSeparatorEnd = ConstantsLTE.PresentationLayer.gstrPuntoyComa;
            objChangeEquipmentSaveModel.SaveSuccessfully = Functions.GetValueFromConfigFile("strMsgSuccessTransaction",
                KEY.AppSettings("strConstArchivoSIACUTLTEConfigMsg"));

            #region Trama

            var strTrama = new System.Text.StringBuilder();

            List<Decoder> ListaDecos = new List<Decoder>();

            foreach (CustomerEquipment equipment in objRequestDataModel.lstEquimentsAssociate)
            {
                Decoder objDeco;
                if (equipment.EquipmentServiceType == ConstantsLTE.PresentationLayer.TipoProduco.LTE &&
                    equipment.Action == ConstantsLTE.Letter_C)
                {
                    objDeco = new Decoder();
                    objDeco.descripcion_material = equipment.EquipmentDescription;
                    objDeco.tipoServicio =
                        equipment.EquipmentServiceType == ConstantsLTE.PresentationLayer.TipoProduco.TVSatelital
                            ? ConstantsLTE.PresentationLayer.TipoProduco.DTH
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
                    strTrama.Append(ConstantsLTE.PresentationLayer.NumeracionUNO);
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
                    if (equipment.Action != ConstantsLTE.Letter_C)
                    {
                        strTrama.Append(equipment.Action == KEY.AppSettings("strActionDisassociate")
                            ? ConstantsLTE.Letter_B
                            : ConstantsLTE.Letter_A);

                        objDeco = new Decoder
                        {
                            descripcion_material = equipment.EquipmentDescription,
                            tipoServicio =
                            equipment.EquipmentServiceType == ConstantsLTE.PresentationLayer.TipoProduco.TVSatelital
                                ? ConstantsLTE.PresentationLayer.TipoProduco.DTH
                                    : equipment.EquipmentServiceType,
                            servicio_principal = equipment.EquipmentType,
                            numero_serie = equipment.EquipmentSeriesNumber,
                            numero = equipment.Action == KEY.AppSettings("strActionDisassociate")
                            ? ConstantsLTE.strLetraB
                                : ConstantsLTE.strLetraA,
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
                            equipment.EquipmentServiceType == ConstantsLTE.PresentationLayer.TipoProduco.TVSatelital
                                ? ConstantsLTE.PresentationLayer.TipoProduco.DTH
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

            Claro.Web.Logging.Info(objRequestDataModel.strSessionId, "ChangeEquipment",
                string.Format("Save() - Trama: {0}", strTrama.ToString()));

            #endregion

   
            #region ServiceCall

            #region ServiceRequest
            var objChangeEquipmentRequest = new ChangeEquipmentRequest() 
            {
                audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(objRequestDataModel.strSessionId),
                idApplication = KEY.AppSettings("ApplicationCode"),
                userSession = objRequestDataModel.strCodeUser,
                customerId = objRequestDataModel.strCustomerID,
                RegistrationReason = objRequestDataModel.strContractId,
                apellidos=objRequestDataModel.strFullName,
                departamento= objRequestDataModel.strDepartament,
                provincia=objRequestDataModel.strProvince,
                distrito = objRequestDataModel.strDistrict,
                domicilio = objRequestDataModel.strAddress,
                modalidad=KEY.AppSettings("gConstKeyStrModalidad"),
                nombres = objRequestDataModel.strFullName,
                documentNumber= objRequestDataModel.strDocumentNumber,
                tipoDoc=objRequestDataModel.strDocumentType,
                usuario = objRequestDataModel.strCodeUser,
                razonSocial=string.Empty,
                flagReg = ConstantsLTE.strUno,
                formatoConstancia = GetGenerateConstancyXml(objRequestDataModel),
                directory = KEY.AppSettings("strCarpetaCambiodeEquipoLTE"),
                fileName =  KEY.AppSettings("strNombreArchCambiodeEquipoLTE"),
                flagContingencia = KEY.AppSettings("gConstContingenciaClarify")!= ConstantsLTE.blcasosVariableSI?ConstantsLTE.strCero: ConstantsLTE.strUno,

                account = string.Empty,
                agente = objRequestDataModel.strCodeUser,
                phone = KEY.AppSettings("gConstKeyCustomerInteract") + objRequestDataModel.strCustomerID,
                siteobjid=string.Empty,
                usrProceso = KEY.AppSettings("USRProcesoSU"),
                tipo = objRequestDataModel.objTypification.Type,
                clase = objRequestDataModel.objTypification.Class,
                subClase = objRequestDataModel.objTypification.SubClass,
                tipoInter = KEY.AppSettings("AtencionDefault"),
                flagCaso = ConstantsLTE.strCero,
                metodoContacto = KEY.AppSettings("MetodoContactoCartaDefault"),
                resultado = KEY.AppSettings("Ninguno"),
                hechoEnUno = ConstantsLTE.strCero,
                notas = string.IsNullOrEmpty(objRequestDataModel.strNote)?string.Empty :objRequestDataModel.strNote ,
                codPlano = string.Empty,
                inconven= string.Empty,
                inconvenCode= string.Empty,
                valor1= string.Empty,
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
                inter30 =  string.IsNullOrEmpty(objRequestDataModel.strNote)?string.Empty :objRequestDataModel.strNote ,
                observacion= string.IsNullOrEmpty(objRequestDataModel.strNote)?string.Empty :objRequestDataModel.strNote,
                cargo = objRequestDataModel.strAmmountToPay,
                codCaso=string.Empty,
                codeDif=ConstantsLTE.strCero,
                codIntercaso = string.Empty,
                codMotot=objRequestDataModel.strMotiveSOTCode,
                idInteraccion =string.Empty,
                tipoTrans = KEY.AppSettings("strTipTransCambioEquipoLTE"),
                tipTra = objRequestDataModel.strTypeWorkCode,
                idProcess=string.Empty,
                flagCharge=ConstantsLTE.strCero,
                trama = strTrama.ToString(),
                coId = objRequestDataModel.strContractId,
                numDoc = objRequestDataModel.strDocumentNumber,
                fecProg = DateTime.Now.ToString("dd/MM/yyyy"),
                tipoVia = ConstantsLTE.strCero,
                nomVia = string.Empty,
                numVia = string.Empty,
                tipUrb = ConstantsLTE.strCero,
                nomUrb = string.Empty,
                manzana = string.Empty,
                lote = string.Empty,
                ubigeo = string.Empty,
                codZona = ConstantsLTE.strCero,
                referencia = string.Empty,
                franjaHor = string.Empty,
                numCarta = ConstantsLTE.strCero,
                operador = ConstantsLTE.strCero,
                preSuscrito = ConstantsLTE.strCero,
                publicar = ConstantsLTE.strCero,
                tmCode = ConstantsLTE.strCero,
                lstTipEqu = string.Empty,
                lstCoser = string.Empty,
                lstSncode = string.Empty,
                lstSpcode = string.Empty,
                usuReg = string.Empty,    
                tipoServicio = string.Empty,
                flagActDirFact = string.Empty,
                tipoProducto = string.Empty,
                inter7 = objRequestDataModel.strMotiveSOT,
                asunto = Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strCorreoAsunto", ConfigurationManager.AppSettings("strConstArchivoSIACUTLTEConfigMsg")),
                mensaje = Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("strCorreoMensaje", ConfigurationManager.AppSettings("strConstArchivoSIACUTLTEConfigMsg")),
                ListDetService = ListaDecos,
                strTransactionAudit = ConfigurationManager.AppSettings("gConstKeyCodTransChangeEquipment"),
                strService = ConfigurationManager.AppSettings("gConstEvtServicio"),
                strIpClient = Functions.CheckStr(HttpContext.Request.UserHostAddress),
                strIpServer = App_Code.Common.GetApplicationIp(),
                strNameServer = App_Code.Common.GetApplicationName(),
                strAmount = Claro.Constants.NumberZeroString,
                strText = string.Format("{0}/Ip Cliente: {1}/Usuario: {2}/Id Opcion: {3}/Fecha y Hora: {4}", string.Format("Codigo Contrato: {0}/MSISDN: {1}", objRequestDataModel.strContractId, objRequestDataModel.strNumberTelephone), Functions.CheckStr(HttpContext.Request.UserHostAddress), objRequestDataModel.strAccount, ConfigurationManager.AppSettings("strIdOpcionClaroProteccion"), DateTime.Now.ToShortDateString()),
                strNameClient=objRequestDataModel.strFullName,
                claroLocal1 = objRequestDataModel.strReferencia,
                //strTransaccion = KEY.AppSettings("strNombreTransaccionCambioEquipo")
            };
            Claro.Web.Logging.Info(objRequestDataModel.strSessionId, "ChangeEquipment: ", string.Format("Save(): RequestForService - CustomerContractID: {0} - CustomerID: {1} - CodigoTipTrabajo: {2} - SOTNumber: {3}", objRequestDataModel.strContractId, objRequestDataModel.strCustomerID, objRequestDataModel.strTypeWorkCode, objRequestDataModel.strSOTNumber));

             #endregion
           
            #region ServiceResponse

             try
             {
                 var objChangeEquipmentResponse = Claro.Web.Logging.ExecuteMethod<ChangeEquipmentResponse>(
                     () => new FixedTransacServiceClient().GetExecuteChangeEquipment(objChangeEquipmentRequest)
                     );

                if (!string.IsNullOrEmpty(objChangeEquipmentResponse.numeroSOT))
                {
                    objChangeEquipmentSaveModel.strFilePathConstancy = objChangeEquipmentResponse.rutaConstancia;
                    objChangeEquipmentSaveModel.bErrorTransac = false;
                    objChangeEquipmentSaveModel.strSOTNumber = objChangeEquipmentResponse.numeroSOT;
                    objChangeEquipmentSaveModel.strResultCode = objChangeEquipmentResponse.codeResponse;
                    objChangeEquipmentSaveModel.strResultMessage = objChangeEquipmentResponse.descriptionResponse;
                    objChangeEquipmentSaveModel.strMessageErrorTransac = Functions.GetValueFromConfigFile("strMsjTranGrabSatis", KEY.AppSettings("strConstArchivoSIACUTLTEConfigMsg"));
                    Claro.Web.Logging.Info(objRequestDataModel.strSessionId, "ChangeEquipment: ", string.Format("Save():ResponseFromService - NumSOT: {0} - IdInteracción: {1} - Ruta de Contancia: {2}", objChangeEquipmentResponse.numeroSOT, objChangeEquipmentResponse.idInteraccion, objChangeEquipmentResponse.rutaConstancia));
                    Claro.Web.Logging.Info(objRequestDataModel.strSessionId, "ChangeEquipment: ", string.Format("Save():ResponseFromService - strResultMessage: {0} ", !string.IsNullOrEmpty(objChangeEquipmentSaveModel.strResultMessage) ? objChangeEquipmentSaveModel.strResultMessage : ConstantsLTE.CriterioMensajeOK));
                }
                else
                {
                    if (string.IsNullOrEmpty(objChangeEquipmentResponse.codeResponse) && string.IsNullOrEmpty(objChangeEquipmentResponse.descriptionResponse))
                    {
                        objChangeEquipmentSaveModel.bErrorTransac = true;
                        objChangeEquipmentSaveModel.strResultMessage = Functions.GetValueFromConfigFile("strMsjServiceNotDis", KEY.AppSettings("strConstArchivoSIACUTLTEConfigMsg"));
                        Claro.Web.Logging.Info(objRequestDataModel.strSessionId, "ChangeEquipment: ", string.Format("Save():ErrorGeneracionSOT - Codigo de Respuesta: {0} - Mensaje: {1} - ", objChangeEquipmentResponse.codeResponse, objChangeEquipmentSaveModel.strResultMessage));
                    }else{
                        objChangeEquipmentSaveModel.bErrorTransac = true;
                        objChangeEquipmentSaveModel.strResultCode = objChangeEquipmentResponse.codeResponse;
                        objChangeEquipmentSaveModel.strResultMessage = objChangeEquipmentResponse.descriptionResponse;
                        objChangeEquipmentSaveModel.strMessageErrorTransac = Functions.GetValueFromConfigFile("strMensajeDeError", KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                        Claro.Web.Logging.Info(objRequestDataModel.strSessionId, "ChangeEquipment: ", string.Format("Save():ErrorGeneracionSOT - Codigo de Respuesta: {0} - Mensaje: {1} - IdInteración: {2} - ", objChangeEquipmentResponse.codeResponse, objChangeEquipmentResponse.descriptionResponse, objChangeEquipmentResponse.idInteraccion));
                    }
                }
             }
             catch (Exception ex)
             {
                 objChangeEquipmentSaveModel.strMessageErrorTransac = Functions.GetValueFromConfigFile("strMensajeDeError", KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                 Claro.Web.Logging.Error(objRequestDataModel.strSessionId, "ChangeEquipment", string.Format("Save() - Error: {0}", ex.Message));
             }
                
             #endregion

            #endregion

                return Json(new { data = objChangeEquipmentSaveModel }, JsonRequestBehavior.AllowGet);
            }
    
        #endregion

        #region Contancia
        public string GetGenerateConstancyXml(ChangeEquipmentSaveModel objRequestDataModel)
        {
            string xmlConstancyPDF = string.Empty;
            try
            {
                string pathFileXml = KEY.AppSettings("strRutaXmlConstanciaCambioEquipo");
                var listParamConstancyPdf = new List<string>
                {
                    KEY.AppSettings("strNombreFormatoCambiodeEquipoLTE"),
                    KEY.AppSettings("strNombreTransaccionCambioEquipo"),
                    objRequestDataModel.strCacDacDescription,
                    objRequestDataModel.strFullName,
                    objRequestDataModel.strFullName,
                    objRequestDataModel.strDocumentType,
                    Functions.CheckStr(DateTime.Now),
                    "$CodigoInteraccion",
                    objRequestDataModel.strContractId,
                    objRequestDataModel.strDocumentNumber,
                    "Cambio de Equipo",//KEY.AppSettings("strNombreTransaccionCambioEquipo"),
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
                            xmlGeneratedGrilla.Append(string.Format("<CAMBIO_EQUIPOS>Cambio: {0}</CAMBIO_EQUIPOS>\r\n", strCadenaSplit[1]));
                        }
                        else
                        {
                            xmlGeneratedGrilla.Append(string.Format("<CAMBIO_EQUIPOS>Cambio: {0}</CAMBIO_EQUIPOS>\r\n", strCadenaSplit[1] + " + " + strCadenaSplit[0]));
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