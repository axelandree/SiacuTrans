using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web.Mvc;
using AutoMapper;
using Claro.Helpers.Transac.Service;
using ConstantsHFC = Claro.SIACU.Transac.Service.Constants;
using HELPERS = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers;
using Model = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models;
using Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService;
using AuditRequestFixed = Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService.AuditRequest;
using AuditRequestCommon = Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService.AuditRequest;
using COMMON = Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService;
using Claro.SIACU.Transac.Service;
using Claro.Web;
using FunctionsSIACU = Claro.SIACU.Transac.Service.Functions;
using Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models;
using Microsoft.VisualBasic;
using Claro.SIACU.Web.WebApplication.Transac.Service.PostTransacService;
using Claro.SIACU.Web.WebApplication.Transac.Service.App_Code;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Controllers.HFC
{
    public class SuspensionServiceController : CommonServicesController
    {
        private readonly FixedTransacServiceClient _oServiceFixed = new FixedTransacServiceClient();
        private readonly PostTransacServiceClient oServicePostpaid = new PostTransacServiceClient();
        private readonly COMMON.CommonTransacServiceClient _oServiceCommon = new COMMON.CommonTransacServiceClient();
        public string StrFlagContingenciaHp = ConfigurationManager.AppSettings("strFlagContingenciaHP");
        public string Mode;
        public string TipoServi;
        public string EstadoServi;
        public bool ModoEdicion;
        public Model.ScheduledTransactionHfcModel ProgramTransaction = new Model.ScheduledTransactionHfcModel();

        public ActionResult HfcSuspensionService(string mode = "", string tipoServi = "", string estadoServi = "")
        {
            Mode = mode;
            ModoEdicion = !string.IsNullOrEmpty(Mode);
            TipoServi = tipoServi;
            EstadoServi = estadoServi;
            Session["Mode"] = Mode;
            Session["ModoEdicion"] = ModoEdicion;
            Session["TipoServi"] = TipoServi;
            Session["EstadoServi"] = EstadoServi;

            return View();
        }

        [HttpPost]
        public JsonResult HfcSuspensionService_PageLoad(string strIdSession, string strTransaction, string estadoLinea, string strPermisos, string contractId)
        {
            try
            {
                var strMinDias = FunctionsSIACU.GetValueFromConfigFile("gConstMsgLineaStatSuspe", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                var dictionaryPageLoad = new Dictionary<string, object>
                {
                    { "hidFlagContingenciaHP", ConfigurationManager.AppSettings("strFlagContingenciaHP")},
                    { "hdnSiteUrl", ConfigurationManager.AppSettings("strRutaSiteInicio")},
                    { "EstadoLinea", StatusLineValidate(strIdSession, 4, estadoLinea)},
                    { "flagRestringirAccesoTemporalSrasc", FunctionsSIACU.GetValueFromConfigFile("flagRestringirAccesoTemporalSRASC", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig"))},
                    { "msgRestringirAccesoTemporalSrasc", FunctionsSIACU.GetValueFromConfigFile("gConstMsgOpcionTemporalmenteInhabilitada", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"))},
                    { "strConsLineaDesaActiva", ConfigurationManager.AppSettings("strConsLineaDesaActiva")},
                    { "gConstMsgLineaStatSuspe", FunctionsSIACU.GetValueFromConfigFile("gConstMsgLineaStatSuspe", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"))},
                    
                    { "hidMinDiasSuspencion", strMinDias == string.Empty ? ConstantsHFC.strCero : strMinDias},
                    { "hidDiasMinSuspension", FunctionsSIACU.GetValueFromConfigFile("intDiasMinSuspension", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig"))},
                    { "hidMaxDiasSuspension", FunctionsSIACU.GetValueFromConfigFile("intDiasMaxSuspension", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig"))},
                    
                    { "hidMinDiasRetSuspension", FunctionsSIACU.GetValueFromConfigFile("intDiasMinRetencionSuspension", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig"))},                    
                    { "hidMaxDiasRetSuspension", FunctionsSIACU.GetValueFromConfigFile("intDiasMaxRetencionSuspension", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig"))},
                    { "hidTipoTranSuspension", FunctionsSIACU.GetValueFromConfigFile("TipoTranSuspension", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig"))},
                    { "hidEstadoTranPendiente", FunctionsSIACU.GetValueFromConfigFile("EstadoTranPendiente", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig"))},
                    
                    { "chkRetencionVisible", strPermisos.IndexOf(ConfigurationManager.AppSettings("strAccesoChkSuspReactServ"), StringComparison.InvariantCultureIgnoreCase) + 1 > 0},
                    //{ "strModoEdicion", string.IsNullOrEmpty(mode)},

                    //IF NOT ISPOSTBACK
                    { "txtMontoRet", ConstantsHFC.PresentationLayer.NumeracionCERO},
                    { "txtTotalImportePagar", FunctionsSIACU.GetValueFromConfigFile("MontoCobroReactivacionServicio", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig"))},

                    { "lblMensajeVisible", false },
                    { "ModoEdicion", (bool)Session["ModoEdicion"] },
                    { "TipoServi", Session["TipoServi"] },
                    { "EstadoServi", Session["EstadoServi"] }                              
                };

                return new JsonResult
                {
                    Data = dictionaryPageLoad,
                    ContentType = "application/json",
                    ContentEncoding = Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            catch (Exception e)
            {
                var dictionaryPageLoad = new Dictionary<string, object>
                {
                    { "lblMensajeText",  Functions.GetValueFromConfigFile("strMensajeProblemaLoad",ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"))},
                    { "lblMensajeVisible", true }
                };

                return new JsonResult
                {
                    Data = dictionaryPageLoad,
                    ContentType = "application/json",
                    ContentEncoding = Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }

        [HttpPost]
        public JsonResult HfcSuspensionService_LoadHiddenMessages(string strIdSession, string strTransaction)
        {
            var dictionaryHiddenMessages = new Dictionary<string, object>();
            try
            {
                dictionaryHiddenMessages = new Dictionary<string, object>
                {
                    { "hdnTituloPagina", FunctionsSIACU.GetValueFromConfigFile("gConstMsgTituloSusTemp", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"))},
                    { "hdnMensaje1", FunctionsSIACU.GetValueFromConfigFile("gConstMsgEstaSegGT", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"))},
                    { "hdnMensaje2", FunctionsSIACU.GetValueFromConfigFile("gConstMsgPIFechSus", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"))},
                    { "hdnMensaje3", FunctionsSIACU.GetValueFromConfigFile("gConstMsgPIFechRea", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"))},
                    { "hdnMensaje4", FunctionsSIACU.GetValueFromConfigFile("strMsgCampNotasMaxC", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"))},
                };
            }
            catch (Exception e)
            {
                Logging.Error(strIdSession, strTransaction, e.Message);
            }

            return new JsonResult
            {
                Data = dictionaryHiddenMessages,
                ContentType = "application/json",
                ContentEncoding = Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        [HttpPost]
        public JsonResult HfcSuspensionService_LoadInfoCustomer(string strIdSession, string strTransaction)
        {
            Dictionary<string, object> dictionaryLoadInfoCustomer;
            try
            {
                dictionaryLoadInfoCustomer = new Dictionary<string, object>
                {
                    { "txtImpPagar", FunctionsSIACU.GetValueFromConfigFile("MontoCobroReactivacionServicio", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig"))},
                    { "hidImportePagar", FunctionsSIACU.GetValueFromConfigFile("MontoCobroReactivacionServicio", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig"))},
                    { "hidMontoCobrarUnitario", FunctionsSIACU.GetValueFromConfigFile("MontoCobroReactivacionServicio", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig"))},
                    { "btnImprimirDisabled", true},
                    { "btnSuspenderDisabled", false},
                    { "lblMensajeVisible", false}
                };
            }
            catch (Exception e)
            {
                dictionaryLoadInfoCustomer = new Dictionary<string, object>
                {
                    { "lblMensajeVisible", true},
                    { "lblMensajeText",  FunctionsSIACU.GetValueFromConfigFile("strMensajeDeError", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"))}
                };

                Logging.Error(strIdSession, strTransaction, e.Message);
            }

            return new JsonResult
            {
                Data = dictionaryLoadInfoCustomer,
                ContentType = "application/json",
                ContentEncoding = Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        [HttpPost]
        public JsonResult HfcSuspensionService_LoadTypification(string strIdSession, string strTransaction)
        {
            var loadTypification = new Dictionary<string, object>();
            try
            {
                var objServiceTypification = GetTypificationHFC(strIdSession, "TRANSACCION_SUSP_REACT_TEMP_HFC");
                var tipo = ConfigurationManager.AppSettings("gConstTipoHFC");
                var objTypificationModel = objServiceTypification.Where(x => x.Type.Equals(tipo)).ToList().FirstOrDefault();

                if (objTypificationModel != null && !string.IsNullOrEmpty(objTypificationModel.Class))
                {
                    loadTypification.Add("hidClaseId", objTypificationModel.ClassCode);
                    loadTypification.Add("hidSubClaseId", objTypificationModel.SubClassCode);
                    loadTypification.Add("hidTipo", objTypificationModel.Type);
                    loadTypification.Add("hidClaseDes", objTypificationModel.Class);
                    loadTypification.Add("hidSubClaseDes", objTypificationModel.SubClass);
                    loadTypification.Add("lblMensajeVis", false);
                }
                else
                {
                    loadTypification.Add("lblMensajeTxt", FunctionsSIACU.GetValueFromConfigFile("strAjusteNoRecon", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")));
                    loadTypification.Add("lblMensajeVis", true);
                    loadTypification.Add("btnGuardarDisabled", true);
                    loadTypification.Add("btnConstanciaDisabled", true);
                }
            }
            catch (Exception e)
            {
                loadTypification = new Dictionary<string, object>
                {
                    { "lblMensajeVisible", true},
                    { "lblMensajeText",  FunctionsSIACU.GetValueFromConfigFile("strMensajeDeError", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"))}
                };

                Logging.Error(strIdSession, strTransaction, e.Message);

            }

            return new JsonResult
            {
                Data = loadTypification,
                ContentType = "application/json",
                ContentEncoding = Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public bool HfcSuspensionService_VerifyProgramTask(string srtIdSession, string strTransaction, string vstrCoId)
        {
            try
            {
                var audit = App_Code.Common.CreateAuditRequest<AuditRequestFixed>(srtIdSession);

                var objRequest = new TransactionScheduledRequest
                {
                    audit = audit,
                    vstrCuenta = string.Empty,
                    vstrAsesor = string.Empty,
                    vstrCodInter = string.Empty,
                    vstrCacDac = string.Empty,
                    vstrCoId = vstrCoId,
                    vstrEstado = string.Empty,
                    vstrFDesde = string.Empty,
                    vstrFHasta = string.Empty,
                    vstrTipoTran = string.Empty
                };

                var objResponse = Logging.ExecuteMethod(() => _oServiceFixed.GetTransactionScheduled(objRequest));

                if (objResponse != null)
                {
                    if (objResponse.ListTransactionScheduled != null)
                    {
                        if (objResponse.ListTransactionScheduled.Count > 0)
                        {
                            var modelLst = objResponse.ListTransactionScheduled;
                            var viewModelLst = Mapper.Map<List<Model.ScheduledTransactionHfcModel>>(modelLst);

                            foreach (var item in viewModelLst)
                            {
                                if (item.SERVC_ESTADO.Equals(ConstantsHFC.PresentationLayer.NumeracionCERO))
                                {
                                    return false;
                                }
                                if (item.SERVC_ESTADO.Equals(ConstantsHFC.PresentationLayer.NumeracionUNO))
                                {
                                    return false;
                                }
                                if (item.SERVC_ESTADO.Equals(ConstantsHFC.PresentationLayer.NumeracionDOS))
                                {
                                    return false;
                                }
                            }
                        }
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                Logging.Error(srtIdSession, strTransaction, e.Message);
                return false;
            }
        }

        [HttpPost]
        public JsonResult HfcSuspensionService_LoadDataProgramTask(string strIdSession, string strTransaction, string contractId, string accountNumber)
        {
            var loadDataProgramTask = new Dictionary<string, object>();
            var idInterac = string.Empty;

            try
            {
                if ((bool)Session["ModoEdicion"])
                {
                    loadDataProgramTask.Add("gConstMsgLineaPOTPVisble", false);
                    var lstProgramTask = HfcProgramTask_GetProgramTask(strIdSession, strTransaction, contractId, accountNumber);

                    foreach (var item in lstProgramTask)
                    {
                        if (item.SERVI_COD.Equals(ConstantsHFC.PresentationLayer.NumeracionCUATRO))
                        {
                            var objTemp = item;
                            Session["objTranAct"] = item;
                            ProgramTransaction = item;
                            idInterac = item.SERVC_CODIGO_INTERACCION;
                            loadDataProgramTask.Add("CODIGO_INTERACCION", idInterac);
                            loadDataProgramTask.Add("SERVI_COD", ConstantsHFC.PresentationLayer.NumeracionCUATRO);
                            loadDataProgramTask.Add("ProgramTransaction", objTemp);
                            break;
                        }

                        if (item.SERVI_COD.Equals(ConstantsHFC.PresentationLayer.NumeracionTRES))
                        {
                            var objTemp = item;
                            Session["objTranAct"] = item;
                            ProgramTransaction = item;
                            idInterac = item.SERVC_CODIGO_INTERACCION;
                            loadDataProgramTask.Add("CODIGO_INTERACCION", idInterac);
                            loadDataProgramTask.Add("SERVI_COD", ConstantsHFC.PresentationLayer.NumeracionTRES);
                            loadDataProgramTask.Add("ProgramTransaction", objTemp);
                            break;
                        }
                    }

                    if (lstProgramTask.Count > 0)
                    {
                        if (Session["TipoServi"].Equals(ConstantsHFC.PresentationLayer.NumeracionCUATRO))
                        {
                            loadDataProgramTask.Add("FechaSuspencion", ProgramTransaction.SERVD_FECHAPROG);
                        }
                        else
                        {
                            loadDataProgramTask.Add("FechaReactivacion", ProgramTransaction.SERVD_FECHAPROG);
                        }

                        if (Session["TipoServi"].Equals(ConstantsHFC.PresentationLayer.NumeracionCUATRO))
                        {
                            loadDataProgramTask.Add("FechaSuspencionDisabled", true);
                        }

                        if (Session["TipoServi"].Equals(ConstantsHFC.PresentationLayer.NumeracionTRES))
                        {
                            loadDataProgramTask.Add("FechaReactivacionDisabled", true);
                        }

                        if (!idInterac.Equals(string.Empty))
                        {
                            var dataInteractionTemplate = GetInfoInteractionTemplate(strIdSession, idInterac);
                            loadDataProgramTask.Add("dataInteractionTemplate", dataInteractionTemplate);
                        }
                    }
                }
                else
                {
                    if (!HfcSuspensionService_VerifyProgramTask(strIdSession, strTransaction, contractId))
                    {
                        loadDataProgramTask.Add("gConstMsgLineaPOTPVisble", true);
                        loadDataProgramTask.Add("gConstMsgLineaPOTP",
                            FunctionsSIACU.GetValueFromConfigFile("gConstMsgLineaPOTP",
                                ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")));
                    }
                    else
                    {
                        loadDataProgramTask.Add("gConstMsgLineaPOTPVisble", false);
                    }
                }
            }
            catch (Exception e)
            {
                Logging.Error(strIdSession, strTransaction, e.Message);
            }

            return new JsonResult
            {
                Data = loadDataProgramTask,
                ContentType = "application/json",
                ContentEncoding = Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public List<Model.ScheduledTransactionHfcModel> HfcProgramTask_GetProgramTask(string srtIdSession, string strTransaction, string vstrCoId, string vstrCuenta)
        {
            var viewModelLst = new List<Model.ScheduledTransactionHfcModel>();
            try
            {
                var audit = App_Code.Common.CreateAuditRequest<AuditRequestFixed>(srtIdSession);

                var objRequest = new TransactionScheduledRequest
                {
                    audit = audit,
                    vstrCuenta = vstrCuenta,
                    vstrAsesor = string.Empty,
                    vstrCodInter = string.Empty,
                    vstrCacDac = string.Empty,
                    vstrCoId = vstrCoId,
                    vstrEstado = string.Empty,
                    vstrFDesde = string.Empty,
                    vstrFHasta = string.Empty,
                    vstrTipoTran = string.Empty
                };

                var objResponse = Logging.ExecuteMethod(() => _oServiceFixed.GetTransactionScheduled(objRequest));

                if (objResponse != null)
                {
                    if (objResponse.ListTransactionScheduled != null)
                    {
                        if (objResponse.ListTransactionScheduled.Count > 0)
                        {
                            var modelLst = objResponse.ListTransactionScheduled;
                            viewModelLst = Mapper.Map<List<Model.ScheduledTransactionHfcModel>>(modelLst);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logging.Error(srtIdSession, strTransaction, e.Message);
            }

            return viewModelLst;
        }

        [HttpPost]
        public JsonResult HfcSuspensionService_PostSuspendProgramTask(string strIdSession, string strTransaction, string hidAccion, string contractId, string txtImpPagar, string fullName, string currentUser, string notes, string customerId, string codePlanInst, string nroCelular, string socialReason, string representanteLegal, string tipoDoc, string nroDoc, string plan, string cicloFacturacion, string fechaSuspension, string fechaReactivacion, string checkRetencion, string cuenta, string txtMontoRet, string hidImportePagar, string cacDac, string hidTotalImportePagar, string hidMontoCobrarUnitario, string strTypeProduct, string strDateActivation, string strDateEndAcuerdo, string strStatusLine)
        {
            var postSuspendProgramTask = new Dictionary<string, object>();
            try
            {
                postSuspendProgramTask.Add("lblMensajeVisible", false);

                if ((bool)Session["ModoEdicion"])
                {
                    InsertAuditProgTask(strIdSession, strTransaction, contractId, txtImpPagar, ConstantsHFC.PresentationLayer.gstrContrato + contractId + ConstantsHFC.PresentationLayer.gstrFechaHora + DateTime.UtcNow, fullName, currentUser);

                    postSuspendProgramTask.Add("btnImprimirDisabled", true);

                    var dictionaryActualizar = ActualizaTransaccionProgramada(strIdSession, strTransaction, notes, customerId, currentUser, contractId, codePlanInst, nroCelular, fullName, socialReason, representanteLegal, tipoDoc, nroDoc, plan, cicloFacturacion, fechaSuspension, fechaReactivacion, checkRetencion, cuenta, txtMontoRet, hidImportePagar, cacDac, hidTotalImportePagar, hidMontoCobrarUnitario, strTypeProduct, strDateActivation, strDateEndAcuerdo, strStatusLine);

                    //Obteniendo ruta pdf
                    postSuspendProgramTask["hidRutaPDF"] = dictionaryActualizar.SingleOrDefault(y => y.Key.Equals("hidRutaPDF")).Value;
                    postSuspendProgramTask["hidBoolReturn"] = dictionaryActualizar.SingleOrDefault(y => y.Key.Equals("hidBoolReturn")).Value;

                    var lblMensajeText = dictionaryActualizar.FirstOrDefault(x => x.Key == "lblMensajeText").Value.ToString();
                    var lblMensajeVisible = System.Convert.ToBoolean(dictionaryActualizar.FirstOrDefault(x => x.Key == "lblMensajeVisible").Value.ToString());
                    var responseFinal = System.Convert.ToBoolean(dictionaryActualizar.FirstOrDefault(x => x.Key == "responseFinal").Value.ToString());

                    postSuspendProgramTask.Add("lblMensajeText", lblMensajeText);
                    postSuspendProgramTask.Add("lblMensajeVisible", lblMensajeVisible);
                    postSuspendProgramTask.Add("responseFinal", responseFinal);

                    postSuspendProgramTask.Add("btnSuspenderDisabled", true);
                    postSuspendProgramTask.Add("chkRetencionDisabled", true);
                }
                else
                {
                    var dictionarysSaveTransaction = SetSaveTransaction(strIdSession, strTransaction, notes, customerId, currentUser, contractId, codePlanInst, nroCelular, fullName, socialReason, representanteLegal, tipoDoc, nroDoc, plan, cicloFacturacion, fechaSuspension, fechaReactivacion, checkRetencion, cuenta, txtMontoRet, hidImportePagar, cacDac, hidTotalImportePagar, hidMontoCobrarUnitario, "", strTransaction, strTypeProduct, strDateActivation, strDateEndAcuerdo, strStatusLine);
                   
                    //Obteniendo ruta pdf
                    postSuspendProgramTask["hidRutaPDF"] = dictionarysSaveTransaction.SingleOrDefault(y => y.Key.Equals("hidRutaPDF")).Value;
                    postSuspendProgramTask["hidBoolReturn"] = dictionarysSaveTransaction.SingleOrDefault(y => y.Key.Equals("hidBoolReturn")).Value;

                    var lblMensajeText = dictionarysSaveTransaction.FirstOrDefault(x => x.Key == "lblMsg").Value.ToString();
                    var lblMensajeVisible = System.Convert.ToBoolean(dictionarysSaveTransaction.FirstOrDefault(x => x.Key == "lblMsgVisible").Value.ToString());
                    var responseFinal = System.Convert.ToBoolean(dictionarysSaveTransaction.FirstOrDefault(x => x.Key == "boolReturn").Value.ToString());

                    postSuspendProgramTask.Add("lblMensajeText", lblMensajeText);
                    postSuspendProgramTask.Add("lblMensajeVisible", lblMensajeVisible);
                    postSuspendProgramTask.Add("responseFinal", responseFinal);

                    if (responseFinal)
                    {
                        postSuspendProgramTask.Add("btnImprimirDisabled", false);
                        postSuspendProgramTask.Add("btnSuspenderDisabled", true);
                        postSuspendProgramTask.Add("chkRetencionDisabled", true);
                    }
                    else
                    {
                        postSuspendProgramTask.Add("btnImprimirDisabled", true);
                        postSuspendProgramTask.Add("btnSuspenderDisabled", true);
                        postSuspendProgramTask.Add("chkRetencionDisabled", true);
                    }
                }
            }
            catch (Exception e)
            {
                postSuspendProgramTask = new Dictionary<string, object>
                {
                    { "lblMensajeVisible", true},
                    { "lblMensajeText",  FunctionsSIACU.GetValueFromConfigFile("strMensajeDeError", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"))}
                };

                Logging.Error(strIdSession, strTransaction, e.Message);
            }

            return new JsonResult
            {
                Data = postSuspendProgramTask,
                ContentType = "application/json",
                ContentEncoding = Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public void InsertAuditProgTask(string strIdSession, string strTransactionId, string contractId, string monto, string strTexto, string nombreCompleto, string currentUser)
        {
            try
            {
                var strTransaccion = (bool)Session["ModoEdicion"] ? ConfigurationManager.AppSettings("gConstCodigoAuditoriaEditarProgramacion") : ConfigurationManager.AppSettings("strCodTranSuspReactTemp");
                var strServicio = ConfigurationManager.AppSettings("gConstEvtServicio");
                var strIpCliente = Request.UserHostName;
                var strNombreCliente = nombreCompleto;
                var strIpServidor = Request.ServerVariables["LOCAL_ADDR"];
                var strNombreServidor = Request.ServerVariables["SERVER_NAME"];
                var strCuentaUsuario = currentUser;
                var strTelefono = contractId;
                SaveAuditM(strTransaccion, strServicio, strTexto, strTelefono, strNombreCliente, strIdSession, strNombreServidor, strIpServidor, strIpCliente, strCuentaUsuario, monto);
            }
            catch (Exception e)
            {
                Logging.Error(strIdSession, strTransactionId, e.Message);
            }
        }

        public Model.InteractionModel DataInteraction(string strIdSession, string strIdTransaction, string customerId, string currentUser, string contractId, string codePlanInst, string notes)
        {
            var responseModel = new Model.InteractionModel();
            try
            {
                var objServiceTypification = GetTypificationHFC(strIdSession, "TRANSACCION_DTH_ELIMIN_TRAN_PROG_HFC");
                var tipo = ConfigurationManager.AppSettings("gConstTipoHFC");
                var objTypificationModel = objServiceTypification.Where(x => x.Type.Equals(tipo)).ToList().FirstOrDefault();
                if (objTypificationModel != null)
                {
                    responseModel.ObjidContacto = GetOBJID(strIdSession, ConfigurationManager.AppSettings("gConstKeyCustomerInteract") + customerId);
                    responseModel.DateCreaction = DateTime.UtcNow.ToString("dd/MM/yyyy");
                    responseModel.Telephone = ConfigurationManager.AppSettings("gConstKeyCustomerInteract") + customerId;
                    responseModel.Type = objTypificationModel.Type;
                    responseModel.Class = objTypificationModel.Class;
                    responseModel.SubClass = objTypificationModel.SubClass;
                    responseModel.TypeInter = ConfigurationManager.AppSettings("AtencionDefault");
                    responseModel.Method = ConfigurationManager.AppSettings("MetodoContactoTelefonoDefault");
                    responseModel.Result = ConfigurationManager.AppSettings("Ninguno");
                    responseModel.MadeOne = ConstantsHFC.PresentationLayer.NumeracionCERO;
                    responseModel.Note = notes;
                    responseModel.FlagCase = ConstantsHFC.PresentationLayer.NumeracionCERO;
                    responseModel.UserProces = ConfigurationManager.AppSettings("USRProcesoSU");
                    responseModel.Agenth = currentUser;
                    responseModel.Contract = contractId;
                    responseModel.Plan = codePlanInst;
                    //usuarioBL.ConsultarUsuario
                }
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, strIdTransaction, ex.Message);
            }

            return responseModel;
        }

        public Model.TemplateInteractionModel DataTemplateInteraction(string strIdSession, string strIdTransaction, string nroCelular, string fechaSuspension, string fechaReactivacion, string checkRetencion, string contractId, string fullName, string representanteLegal, string tipoDoc, string cuenta, string txtNotas, string txtMontoRet, string nroDoc, string hidImportePagar, string CacDac, string hidTotalImportePagar, string hidMontoCobrarUnitario)
        {
            var responseModel = new Model.TemplateInteractionModel();
            var strImporte = string.Empty;
            try
            {
                responseModel.X_CLARO_NUMBER = nroCelular;
                responseModel.X_INTER_1 = fechaSuspension;
                responseModel.X_INTER_2 = fechaReactivacion;
                responseModel.X_INTER_3 = checkRetencion;
                responseModel.X_BASKET = contractId;
                responseModel.X_ADDRESS5 = fullName;
                responseModel.X_BIRTHDAY = DateTime.UtcNow;
                responseModel.X_INTER_17 = fullName;
                responseModel.X_NAME_LEGAL_REP = representanteLegal;

                responseModel.X_CLARO_LDN1 = tipoDoc;
                responseModel.X_CLARO_LDN2 = nroDoc;
                responseModel.X_INTER_15 = cuenta;
                responseModel.X_INTER_16 = contractId;

                if (Convert.ToInt(hidImportePagar) != ConstantsHFC.PresentationLayer.kitracVariableCero)
                {
                    if (checkRetencion.Equals("1"))
                    {
                        strImporte = Math.Round((double)FunctionsSIACU.CheckDblDB(hidTotalImportePagar), 2)
                            .ToString(CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        strImporte = hidMontoCobrarUnitario;
                    }
                }

                responseModel.X_INTER_5 = strImporte;
                responseModel.X_INTER_6 = txtNotas;
                responseModel.X_INTER_7 = txtMontoRet;
                responseModel.X_INTER_15 = CacDac;
                responseModel.X_INTER_30 = txtNotas;
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, strIdTransaction, ex.Message);
            }

            return responseModel;
        }

        public Dictionary<string, object> SaveInteraction(string strIdSession, string strIdTransaction, string notes, string customerId, string currentUser, string contractId, string codePlanInst, string nroCelular, string fullName, string socialReason, string representanteLegal, string tipoDoc, string nroDoc, string plan, string cicloFacturacion, string fechaSuspension, string fechaReactivacion, string checkRetencion, string cuenta, string txtMontoRet, string hidImportePagar, string cacDac, string hidTotalImportePagar, string hidMontoCobrarUnitario, string strTypeProduct, string strDateActivation, string strDateEndAcuerdo, string strStatusLine)
        {
            var responseDictionary = new Dictionary<string, object>();
            try
            {
                responseDictionary["boolReturn"] = true;
                var strInteraccionId = string.Empty;
                var strFlagInsercion = string.Empty;
                var strFlagInsercionInteraccion = string.Empty;

                //Datos de Interaccion
                var oInteraccion = DataInteraction(strIdSession, strIdTransaction, customerId, currentUser, contractId, codePlanInst, notes);

                var strUsuarioSistema = ConfigurationManager.AppSettings("strUsuarioSistemaWSConsultaPrepago");
                var strUsuarioAplicacion = ConfigurationManager.AppSettings("strUsuarioAplicacionWSConsultaPrepago");
                var strPasswordUsuario = ConfigurationManager.AppSettings("strPasswordAplicacionWSConsultaPrepago");

                //DatosPlantillaInteracion
                var oPlantillaDatos = DataTemplateInteraction(strIdSession, strIdTransaction, nroCelular, fechaSuspension, fechaReactivacion, checkRetencion, contractId, fullName, representanteLegal, tipoDoc, cuenta, notes, txtMontoRet, nroDoc, hidImportePagar, cacDac, hidTotalImportePagar, hidMontoCobrarUnitario);

                //Insertar la interaccion
                var resultInteraction = InsertInteraction(
                    oInteraccion,
                    oPlantillaDatos,
                    nroCelular,
                    strUsuarioSistema,
                    strUsuarioAplicacion,
                    strPasswordUsuario,
                    true,
                    strIdSession,
                    customerId);

                var hdnInteracId = resultInteraction.SingleOrDefault(y => y.Key.Equals("rInteraccionId")).Value.ToString();
                var strFlagInsertion = resultInteraction.SingleOrDefault(y => y.Key.Equals("strFlagInsertion")).Value;
                var strFlagInsertionInteraction = resultInteraction.SingleOrDefault(y => y.Key.Equals("strFlagInsertionInteraction")).Value;

                if (strFlagInsertion != null)
                {
                    if (StrFlagContingenciaHp == "1")
                    {
                        //GENERARPDF
                        var dictionary = GeneratePDF(hdnInteracId, customerId, strIdSession, fullName, representanteLegal, tipoDoc, nroDoc, "", strTypeProduct, strDateActivation, strDateEndAcuerdo, strStatusLine, oInteraccion.SubClassCode, oInteraccion.SubClass);
                        responseDictionary["hidRutaPDF"] = dictionary.SingleOrDefault(y => y.Key.Equals("hidRutaPDF")).Value;
                        responseDictionary["hidBoolReturn"] = dictionary.SingleOrDefault(y => y.Key.Equals("hidBoolReturn")).Value;

                        var exito = dictionary.SingleOrDefault(y => y.Key.Equals("hidBoolReturn")).Value;
                        if (exito == "false")
                        {
                            responseDictionary["lblMensajeVisible"] = true;
                            responseDictionary["lblMensaje"] = "Ocurrió un error al tratar de generar la constancia en formato PDF";
                            responseDictionary["boolReturn"] = false;
                        }
                    }
                    responseDictionary["lblMensajeVisible"] = false;
                    strInteraccionId = resultInteraction.FirstOrDefault(x => x.Key == "rInteraccionId").Value.ToString();
                    strFlagInsercion = resultInteraction.FirstOrDefault(x => x.Key == "strFlagInsertion").Value.ToString();
                    strFlagInsercionInteraccion = resultInteraction.FirstOrDefault(x => x.Key == "strFlagInsertionInteraction").Value.ToString();

                    if (strFlagInsercion != ConstantsHFC.CriterioMensajeOK && strFlagInsercion != ConstantsHFC.PresentationLayer.gstrVariableEmpty)
                    {
                        responseDictionary["lblMensajeVisible"] = true;
                        responseDictionary["lblMensaje"] = FunctionsSIACU.GetValueFromConfigFile("gConstKeyErrorEnTransaccion", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                        responseDictionary["boolReturn"] = false;
                    }

                    if (strFlagInsercionInteraccion != ConstantsHFC.CriterioMensajeOK && strFlagInsercionInteraccion != ConstantsHFC.PresentationLayer.gstrVariableEmpty)
                    {
                        responseDictionary["lblMensajeVisible"] = true;
                        responseDictionary["lblMensaje"] = FunctionsSIACU.GetValueFromConfigFile("gConstKeyErrorEnTransaccion", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                        responseDictionary["boolReturn"] = false;
                    }
                }

                responseDictionary["strInteraccionId"] = strInteraccionId;
            }
            catch (Exception e)
            {
                Logging.Error(strIdSession, strIdTransaction, e.Message);
            }


            return responseDictionary;
        }

        public Dictionary<string, object> ActualizaTransaccionProgramada(string strIdSession, string strIdTransaction, string notes, string customerId, string currentUser, string contractId, string codePlanInst, string nroCelular, string fullName, string socialReason, string representanteLegal, string tipoDoc, string nroDoc, string plan, string cicloFacturacion, string fechaSuspension, string fechaReactivacion, string checkRetencion, string cuenta, string txtMontoRet, string hidImportePagar, string cacDac, string hidTotalImportePagar, string hidMontoCobrarUnitario, string strTypeProduct, string strDateActivation, string strDateEndAcuerdo, string strStatusLine)
        {
            var dictionaryResponse = new Dictionary<string, object>();
            var dictionaryInteraction = SaveInteraction(strIdSession, strIdTransaction, notes, customerId, currentUser, contractId, codePlanInst, nroCelular, fullName, socialReason, representanteLegal, tipoDoc, nroDoc, plan, cicloFacturacion, fechaSuspension, fechaReactivacion, checkRetencion, cuenta, txtMontoRet, hidImportePagar, cacDac, hidTotalImportePagar, hidMontoCobrarUnitario, strTypeProduct, strDateActivation, strDateEndAcuerdo, strStatusLine);
            
            //Obteniendo ruta pdf
            dictionaryResponse["hidRutaPDF"] = dictionaryInteraction.SingleOrDefault(y => y.Key.Equals("hidRutaPDF")).Value;
            dictionaryResponse["hidBoolReturn"] = dictionaryInteraction.SingleOrDefault(y => y.Key.Equals("hidBoolReturn")).Value;

            var idInteract = dictionaryInteraction.FirstOrDefault(x => x.Key == "strInteraccionId").Value.ToString();
            try
            {
                if (idInteract.Length > 0)
                {
                    var objProgramadoActualizar = (ScheduledTransactionHfcModel)Session["objTranAct"];
                    int intDias;
                    int intResReactivacion = 0;
                    int intResSuspender = 0;
                    var strCodigoAplicativo = ConfigurationManager.AppSettings("strConsCodAplicSuReTemp");
                    var strPasswordAplicativo = ConfigurationManager.AppSettings("strPasswordAplicacionWSConsultaPrepago");

                    var dtFechaSuspension = FunctionsSIACU.GetDDMMYYYYAsDateTime(fechaSuspension);
                    var dtFechaReactivacion = FunctionsSIACU.GetDDMMYYYYAsDateTime(fechaReactivacion);

                    intDias = Convert.ToInt((dtFechaSuspension - dtFechaReactivacion).TotalDays);

                    var resfinal = false;

                    if (objProgramadoActualizar.SERVI_COD.Equals(ConstantsHFC.PresentationLayer.NumeracionTRES))
                    {
                        var result1 = false;
                        var responseSuspenderServicio = SuspenderServicio(strIdSession, strIdTransaction, contractId, dtFechaSuspension, intDias, strCodigoAplicativo, strPasswordAplicativo, idInteract, customerId, contractId, checkRetencion, cuenta, currentUser);
                        result1 = responseSuspenderServicio.ResultMethod;
                        intResSuspender = Convert.ToInt(responseSuspenderServicio.Result);

                        var result2 = false;
                        var responseReactivarServicio = ReactivarService(contractId, dtFechaReactivacion, intDias, strCodigoAplicativo, strPasswordAplicativo, currentUser, checkRetencion, Convert.ToDouble(hidTotalImportePagar), hidImportePagar, hidImportePagar, idInteract, strCodigoAplicativo, customerId, currentUser, cuenta, strIdSession);
                        result2 = System.Convert.ToBoolean(responseReactivarServicio.SingleOrDefault(x => x.Key.Equals("BoolResult")).Value.ToString());
                        intResReactivacion = System.Convert.ToInt32(responseReactivarServicio.SingleOrDefault(x => x.Key.Equals("Result")).Value.ToString());

                        if (result1 && result2)
                        {
                            resfinal = true;
                        }
                    }
                    else
                    {
                        var responseReactivarServicio = ReactivarService(contractId, dtFechaReactivacion, intDias, strCodigoAplicativo, strPasswordAplicativo, currentUser, checkRetencion, Convert.ToDouble(hidTotalImportePagar), hidImportePagar, hidImportePagar, idInteract, strCodigoAplicativo, customerId, currentUser, cuenta, strIdSession);
                        resfinal = System.Convert.ToBoolean(responseReactivarServicio.SingleOrDefault(x => x.Key.Equals("BoolResult")).Value.ToString());
                    }

                    if (resfinal)
                    {
                        dictionaryResponse["lblMensajeText"] = FunctionsSIACU.GetValueFromConfigFile("strMensajeFinal", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                        dictionaryResponse["lblMensajeVisible"] = true;
                        dictionaryResponse["btnImprimirDisabled"] = false;
                        dictionaryResponse["responseFinal"] = true;
                    }
                    else
                    {
                        if (intResSuspender.Equals(Convert.ToInt(ConstantsHFC.PresentationLayer.NumeracionMENOSUNO)))
                        {
                            dictionaryResponse["lblMensajeText"] = FunctionsSIACU.GetValueFromConfigFile("strMensajeFinal",
                                ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                        }
                        else if (intResReactivacion.Equals(Convert.ToInt(ConstantsHFC.PresentationLayer.NumeracionMENOSUNO))
                        )
                        {
                            dictionaryResponse["lblMensajeText"] = ConfigurationManager.AppSettings("strErrorTareaEnProcesoReconexion");
                        }
                        else
                        {
                            dictionaryResponse["lblMensajeText"] =
                                FunctionsSIACU.GetValueFromConfigFile("strMensajeDeError",
                                    ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                        }

                        dictionaryResponse["lblMensajeVisible"] = true;
                        dictionaryResponse["responseFinal"] = false;
                    }
                }
                else
                {
                    dictionaryResponse["lblMensajeText"] =
                        FunctionsSIACU.GetValueFromConfigFile("strMensajeDeError",
                            ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                    dictionaryResponse["lblMensajeVisible"] = true;
                    dictionaryResponse["responseFinal"] = false;
                }
            }
            catch (Exception e)
            {
                dictionaryResponse["lblMensajeText"] =
                    FunctionsSIACU.GetValueFromConfigFile("strMensajeDeError",
                        ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                dictionaryResponse["lblMensajeVisible"] = true;
                dictionaryResponse["responseFinal"] = false;

                Logging.Error(strIdSession, strIdTransaction, e.Message);
            }

            return dictionaryResponse;
        }

        public Model.HFC.SuspensionModel SuspenderServicio(string strIdSession, string strIdTransaction, string strCodId, DateTime dtFechaSuspension, int intDias, string strCodAplic, string strPasswordAplic, string interaccion, string customerId, string contractId, string chkRetencion, string accountNumber, string currentUser)
        {
            var objResponse = new Model.HFC.SuspensionModel();

            AuditRequestFixed objAuditRequest = App_Code.Common.CreateAuditRequest<AuditRequestFixed>(strIdSession);

            var objRequest = new ExecuteSuspensionRequest
            {
                audit = objAuditRequest
            };

            var objSuspension = new Suspension
            {
                codCliente = customerId,
                codigoAplicacion = strCodAplic,
                codigoInteraccion = interaccion,
                coId = contractId,
                coSer = string.Empty,
                desCoSer = string.Empty,
                desTickler = FunctionsSIACU.GetValueFromConfigFile("gConstDesTicklerNormal", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")),
                usuarioApp = string.Empty,
                fechaProgramacion = dtFechaSuspension,
                fechaSuspension = dtFechaSuspension
            };

            if (chkRetencion.Equals("1"))
            {
                objSuspension.fideliza = Convert.ToInt(ConstantsHFC.PresentationLayer.NumeracionUNO);
            }
            else
            {
                objSuspension.fideliza = Convert.ToInt(ConstantsHFC.PresentationLayer.NumeracionCERO);
            }

            if (string.IsNullOrEmpty((string)Session["Mode"]))
            {
                objSuspension.flagAccion = Convert.ToInt(ConstantsHFC.PresentationLayer.NumeracionUNO);
            }
            else
            {
                objSuspension.flagAccion = Convert.ToInt(ConfigurationManager.AppSettings("strSiacHFCFlag"));
            }

            objSuspension.ipAplicacion = HttpContext.Request.UserHostAddress;
            objSuspension.nroCuenta = accountNumber;
            objSuspension.nroDias = intDias;
            objSuspension.reason = FunctionsSIACU.GetValueFromConfigFile("gConstReasonSuspencion", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
            objSuspension.coState = FunctionsSIACU.GetValueFromConfigFile("gConstCoStateSuspension", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
            objSuspension.telefono = string.Empty;
            objSuspension.ticklerCode = ConfigurationManager.AppSettings("gConstTicklerNormalSS");
            objSuspension.tipoRegistro = string.Empty;
            objSuspension.tipoServicio = string.Empty;
            objSuspension.usuario = currentUser;
            objSuspension.usuarioApp = currentUser;
            objSuspension.usuarioSistema = ConfigurationManager.AppSettings("USRProcesoSU");

            objRequest.Suspension = objSuspension;

            var objSuspensionResponse = Logging.ExecuteMethod(() => _oServiceFixed.EjecutaSuspensionDeServicioCodRes(objRequest));

            objResponse.IdTrans = objSuspensionResponse.idtrans;
            objResponse.Result = objSuspensionResponse.result;
            objResponse.ResultMethod = objSuspensionResponse.ResultMethod;

            return objResponse;
        }

        public Dictionary<string, object> ReactivarService(
            string strCodId,
            DateTime dtDatReactivation,
            int intDias,
            string strCodAplic,
            string strPasswordAplic,
            string strCodUsuario,
            string retencion,
            double totalPagar,
            string txtImporteAPagar,
            string hdnImpAPagar,
            string interaccion,
            string strIpAplication,
            string strCustomerId,
            string strUser,
            string strAcount,
            string strIdSession)
        {
            var dictionary = new Dictionary<string, object>();
            try
            {
                var objRec = new ReconeServiceRequest();
                objRec.GetReconection.CodeAplication = strCodAplic;
                objRec.GetReconection.IpAplication = strIpAplication;
                if (string.IsNullOrEmpty(Session["Mode"].ToString()))
                {
                    objRec.GetReconection.FlagAccion = Convert.ToInt(ConstantsHFC.PresentationLayer.NumeracionUNO);
                }
                else
                {
                    objRec.GetReconection.FlagAccion = Convert.ToInt(ConfigurationManager.AppSettings("strSiacHFCFlag"));
                }
                objRec.GetReconection.ProgramationDate = dtDatReactivation;
                objRec.GetReconection.CoId = strCodId;
                objRec.GetReconection.Reason = FunctionsSIACU.GetValueFromConfigFile("gConstReasonReconexion", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                objRec.GetReconection.CoState = FunctionsSIACU.GetValueFromConfigFile("gConstCoStateReconexion", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                objRec.GetReconection.CodCustomer = strCustomerId;
                if (retencion.Equals("1"))
                {
                    objRec.GetReconection.MontoOcc = Math.Round(totalPagar, 2);
                }
                else
                {
                    if (!string.IsNullOrEmpty(txtImporteAPagar))
                    {
                        objRec.GetReconection.MontoOcc = Single.Parse(txtImporteAPagar);
                    }
                    else
                    {
                        objRec.GetReconection.MontoOcc = Single.Parse(hdnImpAPagar);
                    }
                }

                objRec.GetReconection.Interaction = interaccion;
                objRec.GetReconection.Telephone = string.Empty;
                objRec.GetReconection.TypeService = string.Empty;
                objRec.GetReconection.CoSer = string.Empty;
                objRec.GetReconection.TypeRegister = string.Empty;
                objRec.GetReconection.UserSystem = ConfigurationManager.AppSettings("USRProcesoSU");
                objRec.GetReconection.UserApp = strUser;
                objRec.GetReconection.EmailUserApp = string.Empty;
                objRec.GetReconection.DesCoser = string.Empty;
                objRec.GetReconection.CodeInteraction = interaccion;
                objRec.GetReconection.NroAcount = strAcount;
                objRec.GetReconection.CodeTicker = ConfigurationManager.AppSettings("gConstTicklerNormalSS");

                ReconeServiceResponse objResponse = null;
                //AuditRequest audit = App_Code.Common.CreateAuditRequest<AuditRequest>(strIdSession);
                //objRec.audit = audit;

                try
                {
                    objResponse = Logging.ExecuteMethod<ReconeServiceResponse>(() =>
                    {
                        return _oServiceFixed.GetReconectionService(objRec);
                    });
                }
                catch (Exception ex)
                {
                    //Logging.Error(strIdSession, audit.transaction, ex.Message);
                    throw new Exception(ex.Message);
                }
                dictionary["BoolResult"] = objResponse.BoolResult;
                dictionary["IdTransaction"] = objResponse.IdTransaction;
                dictionary["Result"] = objResponse.Result;
            }
            catch (Exception e)
            {
                Logging.Error(strIdSession, strIdSession, e.Message);
            }

            return dictionary;
        }

        public Dictionary<string, object> SetSaveTransaction(
            string strIdSession,
            string strIdTransaction,
            string notes,
            string customerId,
            string currentUser,
            string contractId,
            string codePlanInst,
            string nroCelular,
            string fullName,
            string socialReason,
            string representanteLegal,
            string tipoDoc,
            string nroDoc,
            string plan,
            string cicloFacturacion,
            string fechaSuspension,
            string fechaReactivacion,
            string checkRetencion,
            string cuenta,
            string txtMontoRet,
            string hidImportePagar,
            string cacDac,
            string hidTotalImportePagar,
            string hidMontoCobrarUnitario,
            string interaccion,
            string strTransactionId,
            string strTypeProduct,
            string strDateActivation,
            string strDateEndAcuerdo,
            string strStatusLine)
        {
            var dictionary = new Dictionary<string, object>();

            var dtFechaSuspension = Functions.GetDDMMYYYYAsDateTime(fechaSuspension);
            var dtFechaReactivacion = Functions.GetDDMMYYYYAsDateTime(fechaReactivacion);

            var strCodigoAplicativo = ConfigurationManager.AppSettings("strConsCodAplicSuReTemp");
            var strPasswordAplicativo = ConfigurationManager.AppSettings("strPasswordAplicacionWSConsultaPrepago");

            var strSusxReten = FunctionsSIACU.GetValueFromConfigFile("strMensajeSusxReten", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
            var strMinSus = FunctionsSIACU.GetValueFromConfigFile("strMensajeMinSus", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
            var strMaxSus = FunctionsSIACU.GetValueFromConfigFile("strMensajeMaxSus", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
            var strMenFin = FunctionsSIACU.GetValueFromConfigFile("strMensajeFinal", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));

            if (checkRetencion.Equals("1"))
            {
                if (DateAndTime.DateDiff(DateInterval.Day, dtFechaSuspension, dtFechaReactivacion) > ConstantsHFC.PresentationLayer.NUMERO155)
                {
                    dictionary["lblMsg"] = strSusxReten;
                    dictionary["boolReturn"] = false;
                }
            }
            if (!checkRetencion.Equals("1"))
            {
                if (DateAndTime.DateDiff(DateInterval.Day, dtFechaSuspension, dtFechaReactivacion) < ConstantsHFC.PresentationLayer.NUMERO15)
                {
                    dictionary["lblMsg"] = strMinSus;
                    dictionary["boolReturn"] = false;
                }
            }
            if (!checkRetencion.Equals("1"))
            {
                if (DateAndTime.DateDiff(DateInterval.Day, dtFechaSuspension, dtFechaReactivacion) > ConstantsHFC.PresentationLayer.NUMERO62)
                {
                    dictionary["lblMsg"] = strMaxSus;
                    dictionary["boolReturn"] = false;
                }
            }

            var intDias = Convert.ToInt(DateAndTime.DateDiff(DateInterval.Day, dtFechaSuspension, dtFechaReactivacion));

            var model = new CustomersDataModel();
            var validateCustomerId = GetValidateCustomerId(model, string.Format("{0}{1}", ConfigurationManager.AppSettings("gConstKeyCustomerInteract"), customerId), strIdSession);
            if (!validateCustomerId)
            {
                dictionary["lblMsg"] = FunctionsSIACU.GetValueFromConfigFile("gConstKeyNoValidaCustomerID", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                dictionary["lblMsgVisible"] = true;
                dictionary["boolReturn"] = false;
            }
            else
            {
                //Datos de Interaccion
                var objInteraction = SaveInteraction(strIdSession, strIdTransaction, notes, customerId, currentUser, contractId, codePlanInst, nroCelular, fullName, socialReason, representanteLegal, tipoDoc, nroDoc, plan, cicloFacturacion, fechaSuspension, fechaReactivacion, checkRetencion, cuenta, txtMontoRet, hidImportePagar, cacDac, hidTotalImportePagar, hidMontoCobrarUnitario, strTypeProduct, strDateActivation, strDateEndAcuerdo, strStatusLine);
                var strInteraccionId = objInteraction.SingleOrDefault(y => y.Key.Equals("strInteraccionId")).Value.ToString();
                var blnRetorno = objInteraction.SingleOrDefault(y => y.Key.Equals("boolReturn")).Value;

                //Obteniendo ruta pdf
                dictionary["hidRutaPDF"] = objInteraction.SingleOrDefault(y => y.Key.Equals("hidRutaPDF")).Value;
                dictionary["hidBoolReturn"] = objInteraction.SingleOrDefault(y => y.Key.Equals("hidBoolReturn")).Value;

                if (blnRetorno != "true")
                {
                    dictionary["lblMsg"] = FunctionsSIACU.GetValueFromConfigFile("strMensajeGrabarInteraccionError", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                    dictionary["lblMsgVisible"] = true;
                    dictionary["boolReturn"] = false;
                }

                var resfinal = false;
                if (dtFechaSuspension > Functions.TruncDateTime(DateTime.Now))
                {
                    var strSuspenderService = SuspenderServicio(strIdSession, strIdTransaction, contractId, dtFechaSuspension, intDias, strCodigoAplicativo, strPasswordAplicativo, interaccion, customerId, contractId, checkRetencion, cuenta, currentUser);
                    if (strSuspenderService.ResultMethod)
                    {
                        var strReactiveService = ReactivarService(contractId, dtFechaReactivacion, intDias, strCodigoAplicativo, strPasswordAplicativo, currentUser, checkRetencion, Convert.ToDouble(hidTotalImportePagar), hidImportePagar, hidImportePagar, strInteraccionId, strCodigoAplicativo, customerId, currentUser, cuenta, strIdSession);
                        var boolResult = strReactiveService.SingleOrDefault(y => y.Key.Equals("BoolResult")).Value;
                        if (boolResult == "true")
                        {
                            resfinal = true;
                        }
                        else
                        {
                            var msg = string.Format("Controlador: {0}, Metodo: {1}, Accion: {2}", "SuspensionServiceController", "SetSaveTransaction", "Error al Reactivar Servicio");
                            Logging.Info("IdSession: " + strIdSession, "Transaccion: " + "Suspension de Servicios", msg);
                        }
                    }
                    else
                    {
                        var msg = string.Format("Controlador: {0}, Metodo: {1}, Accion: {2}", "SuspensionServiceController", "SetSaveTransaction", "Error al SuspenderServicio");
                        Logging.Info("IdSession: " + strIdSession, "Transaccion: " + "Suspension de Servicios", msg);
                    }
                }
                else
                {
                    var strReactiveService = ReactivarService(contractId, dtFechaReactivacion, intDias, strCodigoAplicativo, strPasswordAplicativo, currentUser, checkRetencion, Convert.ToDouble(hidTotalImportePagar), hidImportePagar, hidImportePagar, strInteraccionId, strCodigoAplicativo, customerId, currentUser, cuenta, strIdSession);
                    var boolResult = strReactiveService.SingleOrDefault(y => y.Key.Equals("BoolResult")).Value;
                    resfinal = (boolResult == "true") ? true : false;
                }

                if (resfinal)
                {
                    dictionary["result"] = ConstantsHFC.numeroCero;
                    dictionary["btnImprimir"] = false;
                    var strTexto = ConstantsHFC.PresentationLayer.gstrContrato + " " + contractId + " " +
                                   ConstantsHFC.PresentationLayer.gstrFechaHora + " " + DateTime.Now;
                    //Insertar Auditoria
                    InsertAuditProgTask(strIdSession, strTransactionId, contractId, "0", strTexto, fullName, currentUser);
                    dictionary["lblMsg"] = strMenFin;
                    dictionary["lblMsgVisible"] = true;
                }
                else
                {
                    if (strInteraccionId.Length > 1)
                    {
                        //Insertar UpdateXInter29
                        var strTexto = FunctionsSIACU.GetValueFromConfigFile("strMensajeErrorEnTranActNota", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                        UpdateXInter29(strIdSession, strInteraccionId, strTexto, string.Empty);
                    }
                }
            }
            return dictionary;
        }

        public Dictionary<string, object> GeneratePDF(string hdnInteracId, string strCustomerId, string strIdSession, string strFullName, string strRepresLegal, string strTypeDocument, string strNroDocument, string strUser, string strTypeProduct, string strDateActivation, string strDateEndAcuerdo, string strStatusLine, string strSubClaseId, string strSubClaseDescrip)
        {
            var strFechaTransaccion = DateTime.Today.ToShortDateString();
            var dictionary = new Dictionary<string, object>();
            try
            {
                COMMON.ParametersGeneratePDF parameters = new COMMON.ParametersGeneratePDF();
                COMMON.GenerateConstancyResponseCommon response = null;
                var strTelephone = string.Format("{0}{1}", ConfigurationManager.AppSettings("gConstKeyCustomerInteract"), strCustomerId);

                //ObtenerDatosPlantillaInteraccion
                var getTemplateInteraction = GetInfoInteractionTemplate(strIdSession, hdnInteracId);
                if (getTemplateInteraction.ID_INTERACCION == "" || getTemplateInteraction.TIENE_DATOS == "")
                {
                    //ObtenerDatosPlantillaCaso
                    var getCaseTemplateInteraction = GetDynamicCaseTemplateData(strIdSession, hdnInteracId);
                    parameters.StrCentroAtencionArea = (getCaseTemplateInteraction.X_CAS_15 == string.Empty) ? "No Precisado" : getCaseTemplateInteraction.X_CAS_15;
                }
                else
                {
                    parameters.StrCentroAtencionArea = (getTemplateInteraction.X_INTER_15 == string.Empty) ? "No Precisado" : getTemplateInteraction.X_INTER_15;
                }
                parameters.StrCasoInter = hdnInteracId;
                parameters.StrTitularCliente = strFullName;
                parameters.StrRepresLegal = strRepresLegal;
                parameters.StrTipoDocIdentidad = strTypeDocument;
                parameters.StrNroDocIdentidad = strNroDocument;
                parameters.StrFechaTransaccionProgram = DateTime.Now.ToLongDateString();
                parameters.StrAccion = getTemplateInteraction.X_INTER_1;
                parameters.StrNroServicio = strTelephone;

                parameters.StrFechaSuspension = string.IsNullOrEmpty(getTemplateInteraction.X_INTER_1) ? "" : getTemplateInteraction.X_INTER_1;
                parameters.StrFechaActivacion = string.IsNullOrEmpty(getTemplateInteraction.X_INTER_2) ? "" : getTemplateInteraction.X_INTER_2;
                parameters.StrCostoReactivacion = string.IsNullOrEmpty(getTemplateInteraction.X_INTER_5) ? "" : (getTemplateInteraction.X_INTER_5 + "" + "SIN IGV");

                //obtener tipo de transaccion
                parameters.StrNombreArchivoTransaccion = GetTypeTransaction(strIdSession, strTelephone, string.Empty);

                // Obtengo parametros configurados en el webconfig
                parameters.StrCarpetaTransaccion = ConfigurationManager.AppSettings("strCarpetaSuspensionHFC");
                var strTerminacionPdf = ConfigurationManager.AppSettings("strTerminacionPDF");
                response = GenerateContancyPDF(strIdSession, parameters);

                if (response.Generated)
                {
                    strFechaTransaccion = strFechaTransaccion.Replace('/', '_');

                    var nombrePdf = string.Format("{0}{1}{2}{3}_{4}_{5}_{6}.pdf", parameters.StrServidorLeerPDF, parameters.StrCarpetaPDFs, parameters.StrCarpetaTransaccion, hdnInteracId, strFechaTransaccion, parameters.StrNombreArchivoTransaccion, strTerminacionPdf);
                    var nombrepath = string.Format("{0}{1}{2}", parameters.StrServidorLeerPDF, parameters.StrCarpetaPDFs, parameters.StrCarpetaTransaccion);
                    var documentName = string.Format("{0}_{1}_{2}_{3}", hdnInteracId, strFechaTransaccion, parameters.StrNombreArchivoTransaccion, strTerminacionPdf);

                    dictionary["hidRutaPDF"] = nombrePdf;
                    dictionary["hidRutaPath"] = nombrepath;
                    dictionary["hidDocumentName"] = documentName;
                    dictionary["hidBoolReturn"] = response.Generated;
                    var result = InsertIvidence(parameters.StrNombreArchivoTransaccion, hdnInteracId, parameters.StrCustomerId, strTypeProduct, strDateActivation, strDateEndAcuerdo, strStatusLine, documentName, nombrepath, parameters.StrNombreArchivoTransaccion, strUser, strSubClaseId, strSubClaseDescrip, strIdSession);
                    if (result.BoolResult)
                    {
                        Logging.Info(strIdSession, "GeneratePDF", ConfigurationManager.AppSettings("strRegistroOK"));
                    }
                }
            }
            catch (Exception e)
            {
                Logging.Error("IdSession: " + strIdSession, "GeneratePDF: " + "Error", e.Message);
            }   

            return dictionary;
        }

        public string GetTypeTransaction(string strIdSession, string strRetention, string strIdentificador)
        {
            TypeTransactionBRMSResponse objResponse = new TypeTransactionBRMSResponse();
            try
            {
                var strTypeTransaction = ConfigurationManager.AppSettings("gConstTipoHFC");
                var strTransactionName = ConstantsHFC.ADDITIONALSERVICESHFC.gstrTransaccionSusReactTemp;

                //Tipo de Transaccion
                var oItemTipificacion = GetTypificationHFC(strIdSession, strTransactionName);
                strIdentificador = strIdentificador == "" ? "?" : strIdentificador;
                strRetention = strRetention == "" ? "?" : strRetention;

                var strTransaccion = ConfigurationManager.AppSettings("strTransaccion");
                PostTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostTransacService.AuditRequest>(strIdSession);

                TypeTransactionBRMSRequest objRequest = new TypeTransactionBRMSRequest()
                {
                    audit = audit,
                    StrIdentifier = strIdentificador,
                    StrOperationCodSubClass = oItemTipificacion.First().SubClassCode,
                    StrRetention = strRetention,
                    StrTransactionM = strTransaccion,

                };

                objResponse = Claro.Web.Logging.ExecuteMethod<TypeTransactionBRMSResponse>(() =>
                {
                    return oServicePostpaid.GetTypeTransactionBRMS(objRequest);
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return objResponse.StrResult;
        }

        public COMMON.InsertEvidenceResponse InsertIvidence(string strNameArchive, string strInteractioId, string strCustomerId, string strTypeProduct, string strDateActivation, string strDatEndAcuerdo, string strStatusLine, string hdnDocument, string strNombrepath, string strTypeDocument, string strUser, string strSubClaseId, string strSubClaseDesc, string strIdSession)
        {
            COMMON.Evidence objEvidence = new COMMON.Evidence();
            COMMON.InsertEvidenceResponse ObjEvidenceResponse = null;
            try
            {
                var hdnConsultUser = string.Empty;
                var userQuery = GetUsers(strIdSession, strUser);
                if (userQuery != null)
                {
                    if (userQuery.UserModel != null)
                    {
                        hdnConsultUser = userQuery.UserModel.TypeCac;
                    }
                }
                
                var stDateTransaction = DateTime.Now.ToShortDateString();

                objEvidence.StrTransactionType = ConfigurationManager.AppSettings("strTransactionType");
                objEvidence.StrTransactionCode = strInteractioId;
                objEvidence.StrCustomerCode = strCustomerId;
                objEvidence.StrPhoneNumber = string.Format("{0}{1}", ConfigurationManager.AppSettings("gConstKeyCustomerInteract"), strCustomerId);
                objEvidence.StrTypificationCode = strSubClaseId;
                objEvidence.StrTypificationDesc = strSubClaseDesc;
                objEvidence.StrCommercialDesc = strSubClaseDesc;
                objEvidence.StrProductType = strTypeProduct;
                objEvidence.StrServiceChannel = hdnConsultUser;
                objEvidence.StrTransactionDate = stDateTransaction;
                objEvidence.StrActivationDate = strDateActivation;
                objEvidence.StrSuspensionDate = strDatEndAcuerdo;
                objEvidence.StrServiceStatus = strStatusLine;
                objEvidence.StrDocumentName = strNameArchive;
                objEvidence.StrDocumentType = strTypeDocument;
                objEvidence.StrDocumentExtension = ConfigurationManager.AppSettings("strDocumentoExtension");
                objEvidence.StrDocumentPath = strNombrepath;
                objEvidence.StrUserName = strUser;

                COMMON.AuditRequest audit = App_Code.Common.CreateAuditRequest<COMMON.AuditRequest>(strIdSession);
                COMMON.InsertEvidenceRequest objRequest = new COMMON.InsertEvidenceRequest()
                {
                    audit = audit,
                    Evidence = objEvidence
                };
                ObjEvidenceResponse = Claro.Web.Logging.ExecuteMethod<COMMON.InsertEvidenceResponse>(() =>
                {
                    return _oServiceCommon.GetInsertEvidence(objRequest);
                });

                if (ObjEvidenceResponse.BoolResult)
                {
                    Logging.Info(objRequest.audit.Session, objRequest.audit.transaction, ConfigurationManager.AppSettings("strRegistroOK"));
                }
                else
                {
                    Logging.Info(objRequest.audit.Session, objRequest.audit.transaction, ConfigurationManager.AppSettings("strRegistroNOOK"));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return ObjEvidenceResponse;
        }

        public COMMON.UserResponse GetUsers(string strIdSession, string strCodeUser)
        {
            COMMON.UserResponse objRegionResponse = null;
            CommonTransacService.AuditRequest audit = Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
            COMMON.UserRequest objRegionRequest = new COMMON.UserRequest();
            objRegionRequest.audit = audit;

            objRegionRequest.CodeUser = strCodeUser;
            objRegionRequest.CodeRol = ConstantsHFC.strMenosUno;
            objRegionRequest.CodeCac = ConstantsHFC.strMenosUno;
            objRegionRequest.State = ConstantsHFC.strMenosUno;

            try
            {
                objRegionResponse = Logging.ExecuteMethod<COMMON.UserResponse>(() =>
                {
                    return _oServiceCommon.GetUser(objRegionRequest);
                });
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }

            return objRegionResponse;
        }
    }
}