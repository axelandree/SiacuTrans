using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models;
using CommonTransacService = Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService;
using PostTransacService = Claro.SIACU.Web.WebApplication.Transac.Service.PostTransacService;
using Claro.SIACU.Transac.Service;
using Claro.SIACU.Entity.Transac.Service.Postpaid;
using KEY = Claro.ConfigurationManager;
using Newtonsoft.Json;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Controllers
{
    public class RecordEquipmentForeignController : Controller
    {
        CommonTransacService.CommonTransacServiceClient objCommonTransacService = new CommonTransacService.CommonTransacServiceClient();
        PostTransacService.PostTransacServiceClient objPostTransacService = new PostTransacService.PostTransacServiceClient();
   
        public ActionResult RecordEquipment()
        {
            ViewData["keyTranEquipoExtranjeroPre"] = KEY.AppSettings("strTranEquipoExtranjeroPre");
            ViewData["keyTranEquipoExtranjeroPost"] = KEY.AppSettings("strTranEquipoExtranjeroPost");

            Models.BiometryKeys BioKeys = new Model.BiometryKeys();
            try
            { 
                BioKeys = new Model.BiometryKeys()
                {
                    strTipoDocumentos = KEY.AppSettings("strTipoDocumentos"),
                    strCodigoTipoDocumentoDNIValidacionBiometrica = KEY.AppSettings("strCodigoTipoDocumentoDNIValidacionBiometrica"),
                    strMensajeValidacionBiometrica1 = KEY.AppSettings("strMensajeValidacionBiometrica1"),
                    strMensajeValidacionBiometrica2 = KEY.AppSettings("strMensajeValidacionBiometrica2"),
                    strMensajeValidacionBiometrica3 = KEY.AppSettings("strMensajeValidacionBiometrica3"),
                    strMensajeValidacionBiometrica4 = KEY.AppSettings("strMensajeValidacionBiometrica4"),
                    strMensajeValidacionBiometricaMenos2 = KEY.AppSettings("strMensajeValidacionBiometricaMenos2"),
                    strMensajeValidacionBiometricaMenos4 = KEY.AppSettings("strMensajeValidacionBiometricaMenos4"),
                    strMensajeValidacionBiometricaMenos5 = KEY.AppSettings("strMensajeValidacionBiometricaMenos5"),
                    strMensajeValidacionBiometrica0 = KEY.AppSettings("strMensajeValidacionBiometrica0"),
                    strMensajeValidacionBiometricaOtros = KEY.AppSettings("strMensajeValidacionBiometricaOtros"),
                    strKeyTransaccionDesbloqueoLinea = KEY.AppSettings("strKeyTransaccionDesbloqueoLinea"),
                    strMensajeBlackList = KEY.AppSettings("strMensajeBlackList"),
                    strFlagValidaBlackList = KEY.AppSettings("strFlagValidaBlackList"),
                    strFlagPermisoBiometria = KEY.AppSettings("strConstFlagSiTienePermisosBiometria")
                };
            }
            catch (Exception ex)
            { 
                throw new Exception(ex.Message);
            }
            
            return View(BioKeys);
        }

        public JsonResult LoadListEquipment(string idSession, Int64 CustomerId)
        {

            CommonTransacService.EquipmentForeignResponse objEquipmentForeignResponse = new CommonTransacService.EquipmentForeignResponse();

            CommonTransacService.AuditRequest objAuditRequest = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(idSession);
            CommonTransacService.EquipmentForeignRequest objEquipmentForeignRequest = new CommonTransacService.EquipmentForeignRequest
            {
                audit = objAuditRequest,
                CustomerId = CustomerId,
                Estado = KEY.AppSettings("redeEstadoRegistrado"),
                SessionId = objAuditRequest.Session,
                TransactionId = objAuditRequest.transaction
            };
            try
            {               
                objEquipmentForeignResponse = objCommonTransacService.GetEquipmentForeign(objEquipmentForeignRequest);

                var ObjListEquipmentForeign = objEquipmentForeignResponse.ListEquipmentForeign;
                var oCodeErrorList = objEquipmentForeignResponse.CodeError;
                var oMsgErrorList = objEquipmentForeignResponse.MsgError;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objAuditRequest.Session, objAuditRequest.transaction, ex.Message);
            }
            return Json(objEquipmentForeignResponse, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RegisterEquipmentJson(string nameTransaction, string nameCac, Model.SiactRegistroDesvModel model)
        {
            CommonTransacService.AuditRequest objAuditRequest = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(model.idSession);

            CommonTransacService.InsertEquipmentForeignRequest objRequest = new CommonTransacService.InsertEquipmentForeignRequest
            {                
                audit = objAuditRequest,
                item = new CommonTransacService.EquipmentForeignInsert()
                {
                    REDEN_CUSTOMERID = model.customerId,
                    REDEV_NUMERO_IMEI = model.imei,
                    REDEV_NUMERO_IMEI_FISICO = model.imeiFisico,
                    REDEV_NUMEROLINEA = model.customerTelephone,
                    REDEV_ESTADO = KEY.AppSettings("redeEstadoRegistrado"),
                    REDEV_MARCA_MODELO = model.markModel,
                    REDEV_USUARIOCREA = model.userAccesslogin,
                    MAXIMO = Convert.ToInt(KEY.AppSettings("redeMaximo")) 
                },
                nameTransaction = nameTransaction,
                nameCac = nameCac,
                typeCac=model.typeCac,
                userAccesslogin = model.userAccesslogin,                
                firstName=model.firstName,
                lastName=model.lastName,
                customerTelephone = model.customerTelephone,
                customerFullName = model.customerFullName,
                customerName = model.customerName,
                customerLastName= model.customerLastName,
                customerNumberDocument = model.customerNumberDocument,
                documentTypeText=model.documentTypeText,
                documentNumber = model.documentNumber,
                imei=model.imei,
                imeiFisico=model.imeiFisico,
                markModel=model.markModel,
                codeadviser = model.codeadviser,
                adviser = model.adviser,
                flagFirmaDigital = model.flagFirmaDigital,
                notes= model.notes,
                area=model.area,
                referencePhone=model.referencePhone,
                tipoPersona=model.tipoPersona,
                parient=model.parient,
                customerLegalAgent=model.customerLegalAgent,
                customerNumberDocumentRRLL=model.customerNumberDocumentRRLL,
                listLegalAgent = getListLegalAgent(model.idSession,model.listLegalAgent),
                strHuellaMinucia = model.strHuellaMinucia,
                strHuellaEncode = model.strHuellaEncode,
                customerId=model.customerId,
                strStatusLinea=model.strStatusLinea
            };

            CommonTransacService.InsertEquipmentForeignResponse ObjResponse = new CommonTransacService.InsertEquipmentForeignResponse();
            try
            {
                ObjResponse = objCommonTransacService.GetInsertEquipmentForeign(objRequest);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objAuditRequest.Session, objAuditRequest.transaction, ex.Message);
            }

            return Json(new { data = ObjResponse });
        }

        public JsonResult RegisterTraceabilityJson(string idSession, PostTransacService.InsertTraceabilityRequest objInsertTraceabilityRequest)
        {
            PostTransacService.AuditRequest objAuditRequest = App_Code.Common.CreateAuditRequest<PostTransacService.AuditRequest>(idSession);
            objInsertTraceabilityRequest.audit = objAuditRequest;

            PostTransacService.PostTransacServiceClient objPostTransacServiceClient = new PostTransacService.PostTransacServiceClient();
            PostTransacService.InsertTraceabilityResponse ObjInsertTraceabilityResponse = objPostTransacServiceClient.GetInsertTraceability(objInsertTraceabilityRequest);

            return Json(new { data = ObjInsertTraceabilityResponse });
        }

        public JsonResult GetListEquipmentRegistered(string idSession, Int64 CustomerId)
        {

            CommonTransacService.ListEquipmentRegisteredResponse objEquipmentRegisteredResponse = new CommonTransacService.ListEquipmentRegisteredResponse();

          
            CommonTransacService.AuditRequest objAuditRequest = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(idSession);
            CommonTransacService.ListEquipmentRegisteredRequest objEquipmentRegisteredRequest = new CommonTransacService.ListEquipmentRegisteredRequest
            {
                audit = objAuditRequest,
                CustomerId = CustomerId,
                EstadoId = KEY.AppSettings("redeEstadoRegistrado"),
                NumMaximo = Convert.ToInt(KEY.AppSettings("redeMaximo")),
                SessionId = objAuditRequest.Session,
                TransactionId = objAuditRequest.transaction
            };
            try
            {
                objEquipmentRegisteredResponse = objCommonTransacService.GetListEquipmentRegistered(objEquipmentRegisteredRequest);

                var ObjtEquipmentRegistered = objEquipmentRegisteredResponse.ListEquipmentRegistered;
                var oCodeErrorList = objEquipmentRegisteredResponse.CodeError;
                var oMsgErrorList = objEquipmentRegisteredResponse.MsgError;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objAuditRequest.Session, objAuditRequest.transaction, ex.Message);
            }
            return Json(objEquipmentRegisteredResponse, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SetByParamGroup(string idSession, string CustomerId, string strCadenaOpciones, int CodigoRol)
        {

            CommonTransacService.ConsultByGroupParameterResponse objGroupParameterResponse = new CommonTransacService.ConsultByGroupParameterResponse();
            CommonTransacService.AuditRequest objAuditRequest = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(idSession);
            CommonTransacService.ConsultByGroupParameterRequest objGroupParameterRequest = new CommonTransacService.ConsultByGroupParameterRequest
            {
                audit = objAuditRequest,
                intCodGrupo = Convert.ToInt64(KEY.AppSettings("strConfigBioBlackListCAC")),
                SessionId = idSession,
                TransactionId = objAuditRequest.transaction

            };

            Boolean FlagBlackList = true;            
            try
            {
                objGroupParameterResponse = objCommonTransacService.GetConsultByGroupParameter(objGroupParameterRequest);
                List<ParameterBiometrics> lstAllParamByGroup = new List<ParameterBiometrics>();
                char ConfigSplit = char.Parse(KEY.AppSettings("strConfigSplitDocumentosFD"));
                string strVentanaBiometria = KEY.AppSettings("gConstAccesoBiometriaDesbloqueoEquipo");

                foreach (var obj in objGroupParameterResponse.ListConsultByGroupParameter)
                {
                    lstAllParamByGroup.Add(new ParameterBiometrics()
                    {
                        ID_PARAMETRO = obj.ID_PARAMETRO,
                        DESCRIPCION = obj.DESCRIPCION,
                        VALOR = obj.VALOR,
                        VALOR1 = obj.VALOR1
                    });
                }

                foreach (ParameterBiometrics item in lstAllParamByGroup)
                {
                    string[] obj = item.VALOR.Split(ConfigSplit);
                    if (string.Equals(item.VALOR1, KEY.AppSettings("key_strBlackList")))
                    {
                        if ((strCadenaOpciones.IndexOf(strVentanaBiometria) != -1))
                        {
                            for (int i = 0; (i <= (obj.Length - 1)); i++)
                            {
                                if (obj[i] == CodigoRol.ToString())
                                {
                                    FlagBlackList = false;
                                    break;
                                }
                            }
                        }
                    }
                }               
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objAuditRequest.Session, objAuditRequest.transaction, "Error: " + ex);
                throw new Exception(ex.Message);
            }
            return Json(FlagBlackList, JsonRequestBehavior.AllowGet);
        }
      
        public JsonResult BlackListJson(string idSession, string imei)
        {

            CommonTransacService.AuditRequest objAuditRequest = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(idSession);

            CommonTransacService.BlackListRequest objRequest = new CommonTransacService.BlackListRequest();

            try
            {            
                objRequest = new CommonTransacService.BlackListRequest
                {
                    audit = objAuditRequest,
                    MessageRequest = new CommonTransacService.BlackListMessageRequest()
                    {
                        Header= new CommonTransacService.HeadersRequest()
                        {
                            HeaderRequest = new CommonTransacService.HeaderRequest()
                            {
                                country = KEY.AppSettings("country"),
                                language = KEY.AppSettings("country"),
                                consumer = KEY.AppSettings("consumer"),
                                system = KEY.AppSettings("system"),
                                modulo = KEY.AppSettings("modulo"),
                                pid = DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                                userId = App_Code.Common.CurrentUser,
                                dispositivo = App_Code.Common.GetClientIP(),
                                wsIp = App_Code.Common.GetApplicationIp(),
                                operation = KEY.AppSettings("ValidaEstadoIMEI"),
                                timestamp = DateTime.Now.ToString(),
                                msgType = KEY.AppSettings("msgType"),
                                VarArg = KEY.AppSettings("VarArg")                   
                            }
                        },
                        Body = new CommonTransacService.BlackListBodyRequest()
                        {
                            pi_imei = imei
                        }
                    }
                };
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(objAuditRequest.Session, objAuditRequest.transaction, "IN BlackListJson() => BlackListMessageRequest" + ex.Message);
            }

            CommonTransacService.BlackListResponse ObjResponse = new CommonTransacService.BlackListResponse();
            try
            {
                ObjResponse = objCommonTransacService.GetBlackListOsiptel(objRequest);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objAuditRequest.Session, objAuditRequest.transaction, ex.Message);
            }


            return Json(ObjResponse, JsonRequestBehavior.AllowGet);
        }

        public string getListLegalAgent(string idSession,string listLegalAgent)
        {
            if (string.IsNullOrEmpty(listLegalAgent))
            {
                return String.Empty;
            }
            else
            {
                string strCadena = String.Empty;
                try
                {
                    List<TypeApplicant> lstCant = JsonConvert.DeserializeObject<List<TypeApplicant>>(listLegalAgent);
                    if (lstCant.Count == 0)
                    {
                        return String.Empty;
                    }

                    for (int i = 0; (i<= (lstCant.Count - 1)); i++)
                    {
                        strCadena = (strCadena + (lstCant[i].Nombres + ","));
                        strCadena = (strCadena + (lstCant[i].Apellidos + ","));
                        strCadena = (strCadena + (lstCant[i].TipoDocumento + ","));
                        strCadena = (strCadena + (lstCant[i].NroDocumento + ","));
                        strCadena = (strCadena + (lstCant[i].TipoSolicitante + "|"));
                    }

                    strCadena = strCadena.Substring(0, (strCadena.Length - 1));
                }
                catch (Exception ex)
                {
                    strCadena = String.Empty;
                    Claro.Web.Logging.Info("Session: " + idSession, "RegisterEquipmentJson=> getListLegalAgent  :", string.Format("Se encontró un error al dar formato al registro de Carta Poder/Rep Legal: {0}" , ex.Message));
                }

                return strCadena;
            }
        }
    }
}
