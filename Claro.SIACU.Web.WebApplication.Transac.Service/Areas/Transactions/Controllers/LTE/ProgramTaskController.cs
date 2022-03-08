using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Controllers.LTE
{
    public class ProgramTaskController : CommonServicesController
    {
        private readonly FixedTransacServiceClient _oServiceFixed = new FixedTransacServiceClient();

        #region Variables Globales y Constantes
        public string BlnSeguridad;
        public string StrIpServ;
        public string GstrUserHostName;
        public string GstrLocalAddr;
        public string GstrServerName;
        public string StrNomServ;

        public Model.ConsumptionTop ConsumptionTop;

        #endregion

        public ActionResult LteProgramTask(string coid = "")
        {
            ViewBag.ContractId = coid;
            return View();
        }

        [HttpPost]
        public JsonResult LteProgramTask_PageLoad(string strIdSession, string strTransaction, string strPermisos)
        {
            try
            {
                var dictionaryPageLoad = new Dictionary<string, object>
                {
                    { "hdnTituloPagina", Functions.GetValueFromConfigFile("strMsgTituloTransProg", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"))},
                    { "hdnMensaje1", Functions.GetValueFromConfigFile("strMsgErroOtbTraPro", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"))},
                    { "hdnMensaje2", Functions.GetValueFromConfigFile("strMsgNoHayTraPro", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"))},
                    { "hdnMensaje3", Functions.GetValueFromConfigFile("strMsgSeguroDesElimTra", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"))},
                    { "hdnMensajeNoTienePermisoEliminar",Functions.GetValueFromConfigFile("strMsgSeguroDesElimTra", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"))},
                    { "hdnMensajeNoTienePermisoEditar", Functions.GetValueFromConfigFile("strTextoNoTienePermisoEditar", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"))},

                    { "hdnLocalAdd", Request.ServerVariables["LOCAL_ADDR"]},
                    { "hdnServName",  Request.ServerVariables["SERVER_NAME"]},
                    { "lblMensajeText", Functions.GetValueFromConfigFile("strMensajeProblemaLoad",ConfigurationManager.AppSettings( "strConstArchivoSIACUTHFCConfigMsg") )},
                    { "lblMensajeVisible", false }
                };

                var strKeyAccesoOpcionEliminarProgramacion = ConfigurationManager.AppSettings("strKeyAccesoOpcionEliminarProgramacionLTE");
                var strKeyAccesoOpcionEditarProgramacion = ConfigurationManager.AppSettings("strKeyAccesoOpcionEditarProgramacionLTE");
                var hdnPermisoEliminar = strPermisos.IndexOf(strKeyAccesoOpcionEliminarProgramacion, StringComparison.OrdinalIgnoreCase) + 1 > 0 ? ConstantsHFC.PresentationLayer.NumeracionUNO : ConstantsHFC.PresentationLayer.NumeracionCERO;
                var hdnPermisoEditar = strPermisos.IndexOf(strKeyAccesoOpcionEditarProgramacion, StringComparison.OrdinalIgnoreCase) + 1 > 0 ? ConstantsHFC.PresentationLayer.NumeracionUNO : ConstantsHFC.PresentationLayer.NumeracionCERO;

                dictionaryPageLoad.Add("hdnPermisoEliminar", hdnPermisoEliminar);
                dictionaryPageLoad.Add("hdnPermisoEditar", hdnPermisoEditar);

                StrIpServ = Request.ServerVariables["LOCAL_ADDR"];
                GstrUserHostName = Request.UserHostName;
                GstrLocalAddr = Request.ServerVariables["LOCAL_ADDR"];
                GstrServerName = Request.ServerVariables["SERVER_NAME"];
                StrNomServ = Request.ServerVariables["SERVER_NAME"];

                Logging.Info(strIdSession, strTransaction, "hdnTituloPagina " + dictionaryPageLoad["hdnTituloPagina"]);
                Logging.Info(strIdSession, strTransaction, "hdnMensaje1 " + dictionaryPageLoad["hdnMensaje1"]);
                Logging.Info(strIdSession, strTransaction, "hdnMensaje2 " + dictionaryPageLoad["hdnMensaje2"]);
                Logging.Info(strIdSession, strTransaction, "hdnMensaje3 " + dictionaryPageLoad["hdnMensaje3"]);
                Logging.Info(strIdSession, strTransaction, "hdnMensajeNoTienePermisoEliminar " + dictionaryPageLoad["hdnMensajeNoTienePermisoEliminar"]);
                Logging.Info(strIdSession, strTransaction, "hdnMensajeNoTienePermisoEditar " + dictionaryPageLoad["hdnMensajeNoTienePermisoEditar"]);
                Logging.Info(strIdSession, strTransaction, "hdnMensajeNoTienePermisoEditar " + dictionaryPageLoad["hdnMensajeNoTienePermisoEditar"]);
                Logging.Info(strIdSession, strTransaction, "lblMensajeText " + dictionaryPageLoad["lblMensajeText"]);
                Logging.Info(strIdSession, strTransaction, "lblMensajeVisible " + dictionaryPageLoad["lblMensajeVisible"]);

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

                Logging.Info(strIdSession, strTransaction, "lblMensajeText " + dictionaryPageLoad["lblMensajeText"]);
                Logging.Info(strIdSession, strTransaction, "lblMensajeVisible " + dictionaryPageLoad["lblMensajeVisible"]);

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
        public JsonResult LteProgramTask_ComboBoxLoad(string strIdSession, string strTransaction)
        {
            try
            {
                var lstEstado = GetProgramTask(strIdSession, strTransaction, "SERVC_ESTADO_HFC");
                var lstTipoTransacciones = GetTypeTransaction(strIdSession, strTransaction);

                var dictionaryComboBoxLoad = new Dictionary<string, object>
                {
                    { "LstEstado", lstEstado },
                    { "LstTipoTransacciones", lstTipoTransacciones }
                };

                return new JsonResult
                {
                    Data = dictionaryComboBoxLoad,
                    ContentType = "application/json",
                    ContentEncoding = Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            catch (Exception e)
            {
                Logging.Info(strIdSession, strTransaction, "Exception " + e.Message);
                
                return new JsonResult
                {
                    Data = string.Empty,
                    ContentType = "application/json",
                    ContentEncoding = Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }

        public List<Model.ScheduledTransactionModel> LteProgramTask_ListTable(string srtIdSession, string strTransaction, string vstrCoId, string vstrCuenta, string vstrFDesde, string vstrFHasta, string vstrEstado, string vstrAsesor, string vstrTipoTran, string vstrCodInter, string vstrCacDac)
        {
            var viewModel = new List<Model.ScheduledTransactionModel>();
            var audit = App_Code.Common.CreateAuditRequest<AuditRequestFixed>(srtIdSession);

            var objRequest = new ListScheduledTransactionsRequest
            {
                audit = audit,
                Account = vstrCuenta,
                Advisor = vstrAsesor,
                CodeInteraction = vstrCodInter,
                ApplicationCode = ConfigurationManager.AppSettings("strWebIpCod"),
                ApplicationName = ConfigurationManager.AppSettings("gConstTipoLTE"),
                NameCACDAC = vstrCacDac,
                ServiCoId = vstrCoId,
                ServiceState = vstrEstado,
                StrEndDate = vstrFDesde,
                StrStartDate = vstrFHasta,
                TransactionType = vstrTipoTran,
                UserApp = ConfigurationManager.AppSettings("gConstTipoLTE")
            };

            ListScheduledTransactionsResponse objResponse = Logging.ExecuteMethod(() => _oServiceFixed.GetListScheduledTransactions(objRequest));

            if (objResponse != null)
            {
                if (objResponse.LstTransactions != null)
                {
                    if (objResponse.LstTransactions.Count > 0)
                    {
                        var modelLst = objResponse.LstTransactions;
                        viewModel = Mapper.Map<List<Model.ScheduledTransactionModel>>(modelLst);
                    }                   
                }   
            }
          
            //InsertarAuditoria(ConfigurationManager.AppSettings("gConstEvtConsTranProgLTE"), GeneradorLog.ObtenerValorConfig("strMsgAuditTranProgC", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")))
            return viewModel;
        }

        [HttpPost]
        public JsonResult LteProgramTask_ListTask(string srtIdSession, string strTransaction, string vstrCoId, string vstrCuenta, string vstrFDesde, string vstrFHasta, string vstrEstado, string vstrAsesor, string vstrTipoTran, string vstrCodInter, string vstrCacDac, string fullName, string currentUser, string customerId)
        {
            try
            {
                var listViewModel = LteProgramTask_ListTable(srtIdSession, strTransaction, vstrCoId, vstrCuenta, vstrFDesde, vstrFHasta, vstrEstado, vstrAsesor, vstrTipoTran, vstrCodInter, vstrCacDac);

                var dictionaryListTask = new Dictionary<string, object>
                {
                    { "LstTask", listViewModel }
                };

                InsertAuditProgTask(srtIdSession, strTransaction, ConfigurationManager.AppSettings("gConstEvtConsTranProgLTE"), FunctionsSIACU.GetValueFromConfigFile("strMsgAuditTranProgC", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")), fullName, currentUser, customerId);

                return new JsonResult
                {
                    Data = dictionaryListTask,
                    ContentType = "application/json",
                    ContentEncoding = Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            catch (Exception e)
            {
                Logging.Info(srtIdSession, strTransaction, "Exception " + e.Message);               
                return new JsonResult
                {
                    Data = string.Empty,
                    ContentType = "application/json",
                    ContentEncoding = Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }

        [HttpPost]
        public JsonResult LteProgramTask_Delete(string srtIdSession, string strTransaction, string vstrCodServ, string vstrCoId, string vstrServCEstado, string vstrFecDesde, string vstrFecHasta, string currentUser, string customerId, string fullName, string flagTfi, string codePlanInst, string nroCelular, string socialReason, string representanteLegal, string tipoDoc, string nroDoc, string plan, string cicloFacturacion)
        {
            var audit = App_Code.Common.CreateAuditRequest<AuditRequestFixed>(srtIdSession);

            var objRequest = new ListScheduledTransactionsRequest
            {
                audit = audit,
                ApplicationCode = ConfigurationManager.AppSettings("strWebIpCod"),
                ApplicationName = ConfigurationManager.AppSettings("gConstTipoLTE"),
                UserApp = ConfigurationManager.AppSettings("gConstTipoLTE"),
                StrEndDate = vstrFecDesde,
                StrStartDate = vstrFecHasta,
                ServiceState = vstrServCEstado,
                ServiCoId = vstrCoId
            };

            var objRequestDelete = new DeleteProgramTaskRequest();

            var objResponse = Logging.ExecuteMethod(() => _oServiceFixed.GetListScheduledTransactions(objRequest));
            if (objResponse != null)
            {
                if (objResponse.LstTransactions != null)
                {
                    var listaTra = objResponse.LstTransactions;
                    if (listaTra.Count > 1)
                    {
                        return new JsonResult
                        {
                            Data = FunctionsSIACU.GetValueFromConfigFile("strMsgErrSEMD1TAE", "HFCConfigMsg.config"),
                            ContentType = "application/json",
                            ContentEncoding = Encoding.UTF8,
                            JsonRequestBehavior = JsonRequestBehavior.AllowGet
                        };
                    }

                    for (var i = 0; i < listaTra.Count; i++)
                    {
                        if (listaTra[i].DESC_SERVICE.Equals(FunctionsSIACU.GetValueFromConfigFile("strConstTipoReact", "HFCConfigMsg.config")))
                        {
                            return new JsonResult
                            {
                                Data = FunctionsSIACU.GetValueFromConfigFile("strMsgNoPueEliReact", "HFCConfigMsg.config"),
                                ContentType = "application/json",
                                ContentEncoding = Encoding.UTF8,
                                JsonRequestBehavior = JsonRequestBehavior.AllowGet
                            };
                        }

                        if (listaTra[i].SERVC_STATE.Equals(ConstantsHFC.PresentationLayer.NumeracionTRES) || listaTra[i].SERVC_STATE.Equals(ConstantsHFC.PresentationLayer.NumeracionCUATRO))
                        {
                            return new JsonResult
                            {
                                Data = FunctionsSIACU.GetValueFromConfigFile("strMsgNoPueEliReact", "HFCConfigMsg.config"),
                                ContentType = "application/json",
                                ContentEncoding = Encoding.UTF8,
                                JsonRequestBehavior = JsonRequestBehavior.AllowGet
                            };
                        }
                    }

                    ConsumptionTop = new Model.ConsumptionTop
                    {
                        TopeConsumoDesc = listaTra[0].SERVC_DES_CO_SER,
                        Telefono = listaTra[0].SERVV_MSISDN,
                        TipoRegistro = vstrCodServ
                    };

                    objRequestDelete.CodigoAplicacion = ConfigurationManager.AppSettings("CodAplicacion");
                    objRequestDelete.NombreAplicacion = ConfigurationManager.AppSettings("gConstTipoLTE");
                    objRequestDelete.UsuarioApp = currentUser;

                    objRequestDelete.ServiCod = vstrCodServ;
                    objRequestDelete.ConId = vstrCoId;
                    objRequestDelete.ServiEstado = vstrServCEstado;

                    var resultDelete = Logging.ExecuteMethod(() => _oServiceFixed.DeleteProgramTask(objRequestDelete));

                    if (resultDelete != null)
                    {
                        if (vstrCodServ.Equals(ConstantsHFC.PresentationLayer.NumeracionTRES) && resultDelete.ResponseStatus) { }
                        {
                            objRequestDelete.ServiCod = ConstantsHFC.PresentationLayer.NumeracionCUATRO;
                            resultDelete = Logging.ExecuteMethod(() => _oServiceFixed.DeleteProgramTask(objRequestDelete));
                        }

                        if (resultDelete.ResponseStatus)
                        {
                            SaveInteraction(srtIdSession, strTransaction, vstrCodServ, flagTfi, customerId, currentUser, vstrCoId, codePlanInst, nroCelular, fullName, socialReason, representanteLegal, tipoDoc, nroDoc, plan, cicloFacturacion);
                            InsertAuditProgTask(srtIdSession, strTransaction, ConfigurationManager.AppSettings("gConstEvtConsTranProgLTE"), FunctionsSIACU.GetValueFromConfigFile("strMsgAuditTranProgC", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")), fullName, currentUser, customerId);
                            objRequestDelete.ServiCod = ConstantsHFC.PresentationLayer.NumeracionCUATRO;
                            return new JsonResult
                            {
                                Data = FunctionsSIACU.GetValueFromConfigFile("strMsgExitoElimProgra", "HFCConfigMsg.config"),
                                ContentType = "application/json",
                                ContentEncoding = Encoding.UTF8,
                                JsonRequestBehavior = JsonRequestBehavior.AllowGet
                            };
                        }

                        return new JsonResult
                        {
                            Data = FunctionsSIACU.GetValueFromConfigFile("strMsgErrorElimProgra", "HFCConfigMsg.config"),
                            ContentType = "application/json",
                            ContentEncoding = Encoding.UTF8,
                            JsonRequestBehavior = JsonRequestBehavior.AllowGet
                        };
                    }
                }
            }

            return new JsonResult
            {
                Data = FunctionsSIACU.GetValueFromConfigFile("strMsgErrorElimProgra", "HFCConfigMsg.config"),
                ContentType = "application/json",
                ContentEncoding = Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        [HttpPost]
        public JsonResult GetExportExcel(string srtIdSession, string strTransaction, string vstrCoId, string vstrCuenta, string vstrFDesde, string vstrFHasta, string vstrEstado, string vstrAsesor, string vstrTipoTran, string vstrCodInter, string vstrCacDac, string strCuentUser, string strTelephone, string strNameComplet)
        {
            string path;
            ExcelHelper oExcelHelper = new ExcelHelper();
            List<int> lstHelperPlan = new List<int>();
            lstHelperPlan.Add(0);

            Model.ProgramTaskModel objExportExcel = new Model.ProgramTaskModel();
            objExportExcel.ListExportExcel = new List<Model.ProgramTaskModelExcel>();
            try
            {
                var listViewModel = LteProgramTask_ListTable(srtIdSession, strTransaction, vstrCoId, vstrCuenta, vstrFDesde, vstrFHasta, vstrEstado, vstrAsesor, vstrTipoTran, vstrCodInter, vstrCacDac);
                listViewModel.ForEach(x =>
                {
                    objExportExcel.ListExportExcel.Add(new Model.ProgramTaskModelExcel
                    {
                        Contract = x.CO_ID,
                        CustomerId = x.CUSTOMER_ID,
                        ProgramationDate = x.SERVD_DATEPROG,
                        RegisterDate = x.SERVD_DATE_REG,
                        EjectDate = x.SERVD_DATE_EJEC,
                        State = x.DESC_STATE,
                        ServiceDescription = x.DESC_SERVICE,
                        Account = x.SERVC_NUMBERACCOUNT,
                        ServiceType = x.SERVC_TYPE_REG,
                        Users = x.SERVV_USER_APLICATION
                    });
                });
                InsertAuditory(strTelephone, strNameComplet, srtIdSession, strCuentUser);
                var url = ConfigurationManager.AppSettings("CONSTEXPORT_LTEGetScheduledTask");
                path = oExcelHelper.ExportExcel(objExportExcel, url, lstHelperPlan);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return Json(path);
        }

        public void InsertAuditory(string strTelephone, string strNameComplet, string strIdSession, string strCuentUser)
        {
            var strTransaction = ConfigurationManager.AppSettings("gConstEvtExpTranProg");
            var strDesTranssation = FunctionsSIACU.GetValueFromConfigFile("strMsgAuditTranProgEx", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
            var strService = ConfigurationManager.AppSettings("gConstEvtServicio");
            var strDate = DateTime.Now;

            string strIpCustomer = Request.UserHostName;
            string strIpServidor = Request.ServerVariables["LOCAL_ADDR"];
            string strNameServidor = Request.ServerVariables["SERVER_NAME"];

            string sbText = string.Format("Ip Cliente: {0},/Usuario: {1}, /Opcion: {2}, /Fecha y Hora: {3}", strIpCustomer, strCuentUser, strDesTranssation, strDate);

            //Registrar Auditoria
            SaveAuditM(strTransaction, strService, sbText, strTelephone, strNameComplet, strIdSession, strNameServidor, strIpServidor, strIpCustomer, strCuentUser);

        }

        public void InsertAuditProgTask(string strIdSession, string strTransaction, string strTransac, string strDesTransaccion, string nombreCompleto, string currentUser, string customerId)
        {
            try
            {
                var strServicio = ConfigurationManager.AppSettings("gConstEvtServicio");
                var strIpCliente = GstrUserHostName;
                var strNombreCliente = nombreCompleto;
                var strIpServidor = StrIpServ;
                var strNombreServidor = StrNomServ;
                var strCuentaUsuario = currentUser;
                var strTelefono = customerId;
                var strTexto = string.Empty;
                strTexto = strTexto + "/Ip Cliente: " + strIpCliente + "/Usuario: " + strCuentaUsuario + "/Opcion: " + strDesTransaccion + "/Fecha y Hora: " + DateTime.UtcNow;
                SaveAuditM(strTransaction, strServicio, strTexto, strTelefono, strNombreCliente, strIdSession, strNombreServidor, strIpServidor, strIpCliente, strCuentaUsuario);
            }
            catch (Exception e)
            {
                Logging.Error(strIdSession, strTransaction, e.Message);
            }
        }

        public Model.InteractionModel DataInteraction(string strIdSession, string strIdTransaction, string flagTfi, string customerId, string currentUser, string contractId, string codePlanInst)
        {
            var responseModel = new Model.InteractionModel();
            try
            {
                var objServiceTypification = GetTypificationHFC(strIdSession, "TRANSACCION_DTH_ELIMIN_TRAN_PROG_HFC");
                var tipo = ConfigurationManager.AppSettings("gConstTipoHFC");
                var objTypificationModel = objServiceTypification.Where(x => x.Type.Equals(tipo)).ToList().FirstOrDefault();
                if (objTypificationModel != null)
                {
                    objTypificationModel.Type = ValidatePlanTFI(objTypificationModel.Type, flagTfi);

                    responseModel.ObjidContacto = GetOBJID(strIdSession, ConfigurationManager.AppSettings("gConstKeyCustomerInteract") + customerId);
                    responseModel.DateCreaction = DateTime.UtcNow.ToString("dd/MM/yyyy");
                    responseModel.Telephone = ConfigurationManager.AppSettings("gConstKeyCustomerInteract") + customerId;
                    responseModel.Type = objTypificationModel.Type;
                    responseModel.Class = objTypificationModel.Class;
                    responseModel.SubClass = objTypificationModel.SubClass;
                    responseModel.TypeInter = ConfigurationManager.AppSettings("AtencionDefault");
                    responseModel.Method = ConfigurationManager.AppSettings("MetodoContactoTelefonoDefault");
                    responseModel.Result = ConfigurationManager.AppSettings("Ninguno");
                    responseModel.MadeOne = FunctionsSIACU.GetValueFromConfigFile("strTipSerRegAjusteO", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig"));

                    if (ConsumptionTop.TipoRegistro.Equals(ConfigurationManager.AppSettings("gConstHFCkeyActivaProg")) ||
                        ConsumptionTop.TipoRegistro.Equals(ConfigurationManager.AppSettings("gConstHFCkeyDesctivaProg")))
                    {
                        responseModel.Note =
                            FunctionsSIACU.GetValueFromConfigFile("strMsgCliDesistioTope",
                                ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")) + " " +
                            ConsumptionTop.TopeConsumoDesc;
                    }
                    else
                    {
                        responseModel.Note = FunctionsSIACU.GetValueFromConfigFile("strMsgElCliDesDL",
                            ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                    }

                    responseModel.FlagCase = FunctionsSIACU.GetValueFromConfigFile("strTipSerRegAjusteO", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig"));
                    responseModel.UserProces = ConfigurationManager.AppSettings("USRProcesoSU");
                    responseModel.Agenth = currentUser;
                    responseModel.Contract = contractId;
                    responseModel.Plan = codePlanInst;
                }
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, strIdTransaction, ex.Message);
            }


            return responseModel;
        }

        public Model.TemplateInteractionModel DataTemplateInteraction(string strIdSession, string strIdTransaction, string vstrTipoTran, string nroCelular, string customerId, string fullName, string socialReason, string representanteLegal, string tipoDoc, string nroDoc, string plan, string cicloFacturacion)
        {
            var responseModel = new Model.TemplateInteractionModel();
            try
            {
                responseModel.X_CLARO_NUMBER = nroCelular;
                var lstTipoTransacciones = GetTypeTransaction(strIdSession, strIdTransaction);
                responseModel.X_OPERATION_TYPE = string.Empty;
                if (lstTipoTransacciones != null)
                {
                    foreach (var t in lstTipoTransacciones)
                    {
                        if (vstrTipoTran.Equals(t.Code))
                        {
                            responseModel.X_OPERATION_TYPE = t.Description;
                        }
                    }
                }

                responseModel.X_INTER_1 = customerId;

                responseModel.X_INTER_2 = fullName; //HFCPOST_Session.DatosCliente.NOMBRE_COMPLETO
                responseModel.X_INTER_3 = socialReason; //HFCPOST_Session.DatosCliente.RAZON_SOCIAL

                if (vstrTipoTran.Equals(ConfigurationManager.AppSettings("gConstHFCkeyActivaProg")) ||
                    vstrTipoTran.Equals(ConfigurationManager.AppSettings("gConstHFCkeyDesctivaProg")))
                {
                    responseModel.X_INTER_30 =
                        FunctionsSIACU.GetValueFromConfigFile("strMsgCliDesistioTope",
                            ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")) + " " +
                        ConsumptionTop.TopeConsumoDesc;
                }
                else
                {
                    responseModel.X_INTER_30 = FunctionsSIACU.GetValueFromConfigFile("strMsgElCliDesDL",
                        ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                }

                responseModel.X_INTER_6 = DateTime.UtcNow.ToString("dd/MM/yyyy");
                responseModel.X_INTER_4 = representanteLegal;
                responseModel.X_INTER_5 = tipoDoc;
                responseModel.X_INTER_7 = nroDoc;
                responseModel.X_INTER_16 = plan;
                responseModel.X_INTER_17 = cicloFacturacion;


                responseModel.X_INTER_18 = ConsumptionTop.Telefono;
                responseModel.X_INTER_19 = vstrTipoTran;

                if (vstrTipoTran.Equals(ConfigurationManager.AppSettings("gConstHFCkeyActivaProg")))
                {
                    responseModel.X_INTER_20 = ConfigurationManager.AppSettings("gConstHFCActivarTopeImp");
                }
                else if (vstrTipoTran.Equals(ConfigurationManager.AppSettings("gConstHFCkeyDesctivaProg")))
                {
                    responseModel.X_INTER_20 = ConfigurationManager.AppSettings("gConstHFCDesactivarTopeImp");
                }
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, strIdTransaction, ex.Message);
            }

            return responseModel;
        }

        public string SaveInteraction(string strIdSession, string strIdTransaction, string tipoTransaccion, string flagTfi, string customerId, string currentUser, string contractId, string codePlanInst, string nroCelular, string fullName, string socialReason, string representanteLegal, string tipoDoc, string nroDoc, string plan, string cicloFacturacion)
        {
            var strInteraccionId = string.Empty;
            try
            {
                var oInteraccion = DataInteraction(strIdSession, strIdTransaction, flagTfi, customerId, currentUser, contractId, codePlanInst);
                var strUsuarioSistema = ConfigurationManager.AppSettings("strUsuarioSistemaWSConsultaPrepago");
                var strUsuarioAplicacion = ConfigurationManager.AppSettings("strUsuarioAplicacionWSConsultaPrepago");
                var strPasswordUsuario = ConfigurationManager.AppSettings("strPasswordAplicacionWSConsultaPrepago");

                var oPlantillaDatos = DataTemplateInteraction(strIdSession, strIdTransaction, tipoTransaccion, nroCelular, customerId, fullName, socialReason, representanteLegal, tipoDoc, nroDoc, plan, cicloFacturacion);

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

                if (resultInteraction != null)
                {
                    strInteraccionId = resultInteraction.ContainsKey("rInteraccionId") ? resultInteraction.FirstOrDefault(x => x.Key == "rInteraccionId").Value.ToString() : string.Empty;
                }
            }
            catch (Exception ex)
            {
                if (strInteraccionId.Length > 1)
                {
                    ExecuteUpdateInter30(strIdSession, strIdTransaction, strInteraccionId, FunctionsSIACU.GetValueFromConfigFile("strMensajeErrorparaNotasClfy", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")));
                }
                
                Logging.Error(strIdSession, strIdTransaction, ex.Message);
            }
            
            return strInteraccionId;
        }

	}
}