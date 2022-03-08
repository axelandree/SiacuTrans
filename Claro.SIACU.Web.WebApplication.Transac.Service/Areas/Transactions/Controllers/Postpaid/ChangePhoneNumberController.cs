using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KEY = Claro.ConfigurationManager;
using Model = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Controllers.Postpaid
{
    public class ChangePhoneNumberController : Controller
    {

        private readonly PostTransacService.PostTransacServiceClient oServicePostpaid = new PostTransacService.PostTransacServiceClient();
        private readonly CommonTransacService.CommonTransacServiceClient oServiceCommon = new CommonTransacService.CommonTransacServiceClient();
        
        public ActionResult PostpaidChangePhoneNumber()
        {
            //return View();
            return PartialView(); // Redirect ini 4.0 
        }

        public JsonResult GetPortability(string strIdSession, string strPhone)
        {
            Claro.SIACU.Web.WebApplication.Transac.Service.PostTransacService.PortabilityResponse objPortability = null;
            PostTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostTransacService.AuditRequest>(strIdSession);
            PostTransacService.PortabilityRequest objRequest = new PostTransacService.PortabilityRequest();
            objRequest.Telephone = strPhone;
            objRequest.audit = audit;

            if (!strPhone.Equals(""))
            {
                try
                {
                    objPortability = Claro.Web.Logging.ExecuteMethod<PostTransacService.PortabilityResponse>(() => { return oServicePostpaid.GetPortability(objRequest); });
                }
                catch (Exception ex)
                {
                    Claro.Web.Logging.Error(strIdSession, audit.transaction, ex.Message);
                    throw new Exception(audit.transaction);
                }
            }


            return Json(new { data = objPortability });
        }

        public JsonResult GetStriations(string strIdSession, PostTransacService.StriationsRequest objStriationsRequest)
        {
            PostTransacService.StriationsResponse objStriationsResponse = null;

            try
            {
                objStriationsRequest.audit = App_Code.Common.CreateAuditRequest<PostTransacService.AuditRequest>(strIdSession);
                objStriationsResponse = Claro.Web.Logging.ExecuteMethod<PostTransacService.StriationsResponse>(() => { return oServicePostpaid.GetTriaciones(objStriationsRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objStriationsRequest.audit.transaction, ex.Message);
                throw new Exception(objStriationsRequest.audit.transaction);
            }

            Areas.Transactions.Models.StriationsModel objStriationsModel = new Areas.Transactions.Models.StriationsModel();

            if (objStriationsResponse.Striations != null)
            {
                List<Helpers.Postpaid.StriationHelper> listStriations = new List<Helpers.Postpaid.StriationHelper>();

                foreach (PostTransacService.Striations item in objStriationsResponse.Striations)
                {
                    listStriations.Add(new Helpers.Postpaid.StriationHelper()
                    {
                        NumberThreesome = item.NUM_TRIO,
                        TypeDestination = item.TIPO_DESTINO,
                        Factor = item.FACTOR,
                        Telephone = item.TELEFONO,
                        TypeTriado = item.TIPO_TRIADO,
                        TrioDestination = item.DESTINO_TRIO,
                        CodeTypeDestination = item.CODIGO_TIPO_DESTINO == "1" ? "SI" : "NO"
                    });
                }
                objStriationsModel.Striations = listStriations;
            }

            return Json(new { data = objStriationsModel });
        }

        public JsonResult GetHLR(string strIdSession, string strNumberPhone, string strRangeType)
        {
            PostTransacService.HLRResponse objHLRResponse = new PostTransacService.HLRResponse();
            PostTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostTransacService.AuditRequest>(strIdSession);
            PostTransacService.HLRRequest objHLRRequest = new PostTransacService.HLRRequest();
            objHLRRequest.audit = audit;
            objHLRRequest.PHONE_NUMBER = strNumberPhone;
            objHLRRequest.RANGE_TYPE = strRangeType;

            try
            {
                objHLRResponse = Claro.Web.Logging.ExecuteMethod<PostTransacService.HLRResponse>(() => { return oServicePostpaid.GetHLRLocation(objHLRRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }

            return Json(new { data = objHLRResponse });
        }

        public JsonResult GetStatusPhone(string strIdSession, string strNumberPhone)
        {
            PostTransacService.SimCardPhone objStatus = new PostTransacService.SimCardPhone();
            PostTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostTransacService.AuditRequest>(strIdSession);
            Claro.Web.Logging.Info(audit.Session, audit.transaction, strNumberPhone);

            try
            {
                objStatus = Claro.Web.Logging.ExecuteMethod<PostTransacService.SimCardPhone>(() => { return oServicePostpaid.GetStatusPhone(strIdSession, audit.transaction, audit.ipAddress, audit.applicationName, audit.userName, strNumberPhone); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }

            return Json(new { data = objStatus });
        }

        public JsonResult ValidateChangeNumber(string strIdSession, string strContract, string strFlagFidelize)
        {
            PostTransacService.ChangePhoneNumber objChangeNumber = null;
            PostTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostTransacService.AuditRequest>(strIdSession);

            try
            {
                objChangeNumber = Claro.Web.Logging.ExecuteMethod<PostTransacService.ChangePhoneNumber>(() => { return oServicePostpaid.ValidateChangeNumberTransaction(strIdSession, audit.transaction, strContract, strFlagFidelize); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }

            return Json(new { data = objChangeNumber });
        }

        public JsonResult GetNewPhoneNumber(string strIdSession,string strPhone, string strICCID, string strFlagTFI, string strModality, string strLocation, string strHLR)
        {
            string strNewPhone = string.Empty;
            string strEndHLR = string.Empty;
            string strClaseRed = string.Empty;
            string strUpdatePhoneState = string.Empty;
            string strUpdatePhoneMessage = string.Empty;
            string strUser = KEY.AppSettings("strConstUsrAplicacion");
            
            if (strFlagTFI == Claro.SIACU.Transac.Service.Constants.gstrVariableSI)
            {
                strClaseRed = KEY.AppSettings("strCambioNumClaseRedTFI");
            }
            else
            {
                if (strModality.ToUpper() == KEY.AppSettings("strConstModalidadCorporativo"))
                    strClaseRed = KEY.AppSettings("strCambioNumClaseRedCORP");
                else
                    strClaseRed = KEY.AppSettings("strCambioNumClaseRedNOCORP");
            }

            Models.Postpaid.GetNewNumberModel objModel = new Models.Postpaid.GetNewNumberModel();
            objModel.ERROR = Claro.Constants.NumberZeroString;

            PostTransacService.ChangePhoneNumberResponse objChangeNumberResponse = null;
            PostTransacService.ChangePhoneNumberRequest objChangeNumberRequest = new PostTransacService.ChangePhoneNumberRequest();
            PostTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostTransacService.AuditRequest>(strIdSession);
            objChangeNumberRequest.audit = audit;
            objChangeNumberRequest.NUMBER_PHONES = KEY.AppSettings("strCantListaNumDispParaCambio");
            objChangeNumberRequest.CLASIFICATION_RED = strClaseRed;
            objChangeNumberRequest.CUSTOMER_TYPE = KEY.AppSettings("strTipoClienteZSanz");
            objChangeNumberRequest.NATIONAL_CODE = strLocation;
            objChangeNumberRequest.PHONE_TYPE = KEY.AppSettings("strTipoNroTelefonicoZSanz");
            objChangeNumberRequest.HLR = strHLR;
            objChangeNumberRequest.PHONE = strPhone;

            try
            {
                objChangeNumberResponse = Claro.Web.Logging.ExecuteMethod<PostTransacService.ChangePhoneNumberResponse>(() => { return oServicePostpaid.GetAvailableLines(objChangeNumberRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }

            if (objChangeNumberResponse.RESPONSE_MESSAGE != Claro.Constants.NumberZeroString || objChangeNumberResponse.LstSimCardPhone == null)
            {
                objModel.MESSAGE = objChangeNumberResponse.RESPONSE_MESSAGE;
                objModel.ERROR = Claro.Constants.NumberOneString;
            }
            else
            {
                if (objChangeNumberResponse.LstSimCardPhone.Count() > Claro.Constants.NumberZero)
                {
                    foreach (PostTransacService.SimCardPhone item in objChangeNumberResponse.LstSimCardPhone)
                    {
                        strNewPhone = Claro.Convert.ToString(item.NRO_TELEF);
                        strEndHLR = Claro.Convert.ToString(item.CODIG_HLR);
                        strNewPhone = strNewPhone.Substring(Claro.Constants.NumberFive);
                        if (strNewPhone.Substring(Claro.Constants.NumberZero, Claro.Constants.NumberOne) == Claro.Constants.NumberZeroString)
                        {
                            strNewPhone = strNewPhone.Substring(Claro.Constants.NumberOne);
                        }

                        if (ValidateBSCS(strIdSession, strICCID, strNewPhone))
                        {
                            objModel.NEW_PHONE = strNewPhone;
                            objModel.END_HLR = strEndHLR;

                            strUpdatePhoneState = UpdateNumberPhone(strIdSession, strPhone, strNewPhone,strUser, ref strUpdatePhoneMessage);
                            if(strUpdatePhoneState != Claro.Constants.NumberZeroString){
                                objModel.MESSAGE = strUpdatePhoneMessage;
                                objModel.ERROR = Claro.Constants.NumberTwoString;
                                objModel.END_HLR = string.Empty;
                                objModel.NEW_PHONE = string.Empty;
                                objModel.ROLLBACK = Claro.Constants.LetterN;
                            }
                            else
                            {
                                objModel.ROLLBACK = Claro.Constants.LetterS;
                                objModel.MESSAGE = string.Empty;
                                objModel.ERROR = string.Empty;
                            }

                            break;
                        }
                        else
                        {
                            objModel.NEW_PHONE = string.Empty;
                            objModel.END_HLR = string.Empty;
                            objModel.ROLLBACK = Claro.Constants.LetterN;
                            objModel.MESSAGE = KEY.AppSettings("strMsjNotLinesInBSCS");
                            objModel.ERROR = string.Empty;
                        }
                    }
                }
                else
                {
                    objModel.MESSAGE = KEY.AppSettings("strMsjNotLinesInZans");
                    objModel.ERROR = Claro.Constants.NumberZeroString;
                }
            }

            return Json(new { data = objModel });

        }

        public JsonResult ExecuteChangePhoneNumber(Areas.Transactions.Models.Postpaid.PostNewNumberModel oNewNumber)
        {
            Models.Postpaid.DataNewNumber oResponse = new Models.Postpaid.DataNewNumber();
            Models.Postpaid.DataNewNumber oResponseInteraction = new Models.Postpaid.DataNewNumber();
            string strFlagFidelize = string.Empty;
            string strApplicationID = KEY.AppSettings("strUsuarioAplicacionWSConsultaPrepago");
            string strApplicationPWD = KEY.AppSettings("strPasswordAplicacionWSConsultaPrepago");
            if (oNewNumber.CUSTOMER_TYPE.ToUpper() == Claro.SIACU.Transac.Service.Constants.PresentationLayer.gstrBusinessUpper)
                strFlagFidelize = Claro.Constants.NumberOneString;
            else
                strFlagFidelize = Claro.Constants.NumberOneString;

            double dblCost = Claro.Convert.ToDouble(oNewNumber.COST);
            if (dblCost > 0)
            {
                dblCost = Math.Round(dblCost / Claro.Convert.ToDouble(oNewNumber.IGV), Claro.Constants.NumberTwo);
            }

            PostTransacService.ChangePhoneNumberResponse objChangePhoneNumberResponse = new PostTransacService.ChangePhoneNumberResponse();
            PostTransacService.ChangePhoneNumberRequest objChangePhoneNumberRequest = new PostTransacService.ChangePhoneNumberRequest();
            PostTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostTransacService.AuditRequest>(oNewNumber.SESSION);
            objChangePhoneNumberRequest.audit = audit;
            objChangePhoneNumberRequest.CONTRACT = oNewNumber.CONTRACT;
            objChangePhoneNumberRequest.NEW_PHONE = oNewNumber.NEW_PHONE;
            objChangePhoneNumberRequest.EST_TRASLADO = oNewNumber.TRANSACTION_TYPE;
            objChangePhoneNumberRequest.COST = dblCost;
            objChangePhoneNumberRequest.FLAG_FIDELIZE = strFlagFidelize;
            objChangePhoneNumberRequest.CURRENT_PHONE = oNewNumber.CURRENT_PHONE;
            objChangePhoneNumberRequest.APPLICATION_ID = strApplicationID;
            objChangePhoneNumberRequest.APPLICATION_PWD = strApplicationPWD;
            objChangePhoneNumberRequest.COSTMEDNO = Claro.Constants.NumberZeroString;
            objChangePhoneNumberRequest.FLAG_CHANGECHIP = Claro.Constants.NumberZeroString;
            objChangePhoneNumberRequest.FLAG_LOCUTION = Claro.Constants.NumberZeroString;

            try
            {
                objChangePhoneNumberResponse = Claro.Web.Logging.ExecuteMethod<PostTransacService.ChangePhoneNumberResponse>(() => { return oServicePostpaid.ExecuteChangeNumber(objChangePhoneNumberRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message);
            }

            if (!objChangePhoneNumberResponse.RESPONSE_CODE.Equals(KEY.AppSettings("strRespuestaProcesoZsans")))
            {
                oResponse.MESSAGE = KEY.AppSettings("strMsjErrorExecTransCambioNro") + objChangePhoneNumberResponse.RESPONSE_MESSAGE;
                oResponse.ROLLBACK = Claro.Constants.LetterN;
                PostTransacService.ChangePhoneNumberResponse objRollbakcResponse = ExecuteRollbackChangePhoneNumber(audit.Session, oNewNumber.CURRENT_PHONE, oNewNumber.NEW_PHONE);
                if (!objRollbakcResponse.RESPONSE)
                {
                    oResponse.MESSAGE = oResponse.MESSAGE + KEY.AppSettings("strMsjErrorRollbackCambioNro");
                }
            }
            else
            {
                oResponse.ROLLBACK = Claro.Constants.LetterN;
                oResponse.MESSAGE = KEY.AppSettings("strMsjExistoCambioNro");

                oResponseInteraction = InsertInteraction(oNewNumber);
                oResponse.MESSAGE = oResponse.MESSAGE + oResponseInteraction.MESSAGE;
                oResponse.INTERACTION_ID = oResponseInteraction.INTERACTION_ID;

                oResponse.NAME_PDF = GetConstancyPDF(oNewNumber, oResponse.INTERACTION_ID).NAME_PDF;

                insertAudit(oNewNumber);
                DeleteUserHistory(oNewNumber.SESSION, oNewNumber.CURRENT_PHONE);
            }

            return Json(new { data = oResponse });
        }

        public JsonResult RollbackChangePhoneNumber(string strIdSession, string strPhone, string strNewPhone)
        {
            PostTransacService.ChangePhoneNumberResponse objChangePhoneNumberResponse = null;

            objChangePhoneNumberResponse = ExecuteRollbackChangePhoneNumber(strIdSession, strPhone, strNewPhone);

            return Json(new { data = objChangePhoneNumberResponse });
        }

        public JsonResult AlignService(string strIdSession, string strPhone, string strContractID)
        {
            Transactions.Controllers.CommonServicesController octlPostpaid = new Transactions.Controllers.CommonServicesController();
            AlignServiceTransaction(strIdSession, strContractID);
            AlignContractID(strIdSession, strContractID);
            strPhone = octlPostpaid.GetNumber(strIdSession, false, strPhone);
            return Json(new { data = strPhone });
        }

        public PostTransacService.ChangePhoneNumberResponse ExecuteRollbackChangePhoneNumber(string strIdSession, string strPhone, string strNewPhone)
        {
            PostTransacService.ChangePhoneNumberResponse objChangePhoneNumberResponse = null;
            PostTransacService.ChangePhoneNumberRequest objChangePhoneNumberRequest = new PostTransacService.ChangePhoneNumberRequest();
            PostTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostTransacService.AuditRequest>(strIdSession);
            objChangePhoneNumberRequest.audit = audit;
            objChangePhoneNumberRequest.CURRENT_PHONE = strPhone;
            objChangePhoneNumberRequest.NEW_PHONE = strNewPhone;
            objChangePhoneNumberRequest.USER = string.Empty;

            try
            {
                objChangePhoneNumberResponse = Claro.Web.Logging.ExecuteMethod<PostTransacService.ChangePhoneNumberResponse>(() => { return oServicePostpaid.RollbackChangeNumber(objChangePhoneNumberRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }

            return objChangePhoneNumberResponse;
        }
    
        public bool ValidateBSCS(string strIdSession, string strICCID, string strNewPhone)
        {
            bool strResul = false;
            PostTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostTransacService.AuditRequest>(strIdSession);

            try
            {
                strResul = Claro.Web.Logging.ExecuteMethod<bool>(() => { return oServicePostpaid.ValidateChangeNumberBSCS(audit.Session, audit.transaction, strICCID, strNewPhone, Claro.Constants.NumberZero); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }

            return strResul;
        }

        public string UpdateNumberPhone(string strIdSession, string strPhone, string strNewPhone, string strUser, ref string strMessage)
        {
            PostTransacService.ChangePhoneNumberResponse objChangeNumberResponse = new PostTransacService.ChangePhoneNumberResponse();
            PostTransacService.ChangePhoneNumberRequest objChangeNumberRequest = new PostTransacService.ChangePhoneNumberRequest();
            PostTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostTransacService.AuditRequest>(strIdSession);
            objChangeNumberRequest.audit = audit;
            objChangeNumberRequest.CURRENT_PHONE = strPhone;
            objChangeNumberRequest.NEW_PHONE = strNewPhone;
            objChangeNumberRequest.USER = strUser;

            try
            {
                objChangeNumberResponse = Claro.Web.Logging.ExecuteMethod<PostTransacService.ChangePhoneNumberResponse>(() => { return oServicePostpaid.UpdatePhoneNumber(objChangeNumberRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }

            strMessage = objChangeNumberResponse.RESPONSE_MESSAGE;
            return objChangeNumberResponse.RESPONSE_CODE;
        }

        public bool AlignServiceTransaction(string strIdSession, string strContractID)
        {
            bool blnResponse;
            PostTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostTransacService.AuditRequest>(strIdSession);
            try
            {
                blnResponse = Claro.Web.Logging.ExecuteMethod<bool>(() => { return oServicePostpaid.AlignTransactionService(audit.Session, audit.transaction, strContractID); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }

            return blnResponse;
        }

        public bool AlignContractID(string strIdSession, string strContractID)
        {
            bool blnResponse;
            PostTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostTransacService.AuditRequest>(strIdSession);
            try
            {
                blnResponse = Claro.Web.Logging.ExecuteMethod<bool>(() => { return oServicePostpaid.AlignCodID(audit.Session, audit.transaction, strContractID); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }

            return blnResponse;
        }

        public Models.Postpaid.DataNewNumber InsertInteraction(Areas.Transactions.Models.Postpaid.PostNewNumberModel oNewNumber)
        {
            double dblNumber;
            Models.Postpaid.DataNewNumber oResponseMethod = new Models.Postpaid.DataNewNumber();
            CommonTransacService.InsertGeneralResponse oResponse = null;
            CommonTransacService.InsertGeneralRequest oRequest = new CommonTransacService.InsertGeneralRequest();
            CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(oNewNumber.SESSION);
            CommonTransacService.Iteraction oInteraction = new CommonTransacService.Iteraction();
            oInteraction.OBJID_CONTACTO = oNewNumber.CONTACTOBJID;
            oInteraction.FECHA_CREACION = DateTime.Now.ToString();
            oInteraction.TELEFONO = oNewNumber.CURRENT_PHONE;
            oInteraction.TIPO = oNewNumber.TYPE;
            oInteraction.CLASE = oNewNumber.CLASS;
            oInteraction.SUBCLASE = oNewNumber.SUB_CLASS;
            oInteraction.TIPO_INTER = KEY.AppSettings("AtencionDefault");
            oInteraction.METODO = KEY.AppSettings("MetodoContactoTelefonoDefault");
            oInteraction.RESULTADO = KEY.AppSettings("Ninguno");
            oInteraction.HECHO_EN_UNO = Claro.Constants.NumberZeroString;
            oInteraction.NOTAS = oNewNumber.NOTES;
            oInteraction.FLAG_CASO = Claro.Constants.NumberZeroString;
            oInteraction.USUARIO_PROCESO = KEY.AppSettings("USRProcesoSU");
            oInteraction.AGENTE = audit.userName;

            CommonTransacService.InsertTemplateInteraction oTemplate = new CommonTransacService.InsertTemplateInteraction();
            oTemplate._NOMBRE_TRANSACCION = oNewNumber.TRANSACTION;
            oTemplate._X_CLARO_NUMBER = oNewNumber.CURRENT_PHONE;
            if (double.TryParse(oNewNumber.CURRENT_PHONE,out dblNumber)){
                oTemplate._X_INTER_22 = dblNumber;
            }else{
                oTemplate._X_INTER_22 = 0;
            }
            oTemplate._X_REASON = oNewNumber.TRANSACTION_TYPE;
            oTemplate._X_DEPARTMENT = oNewNumber.LOCATION;
            oTemplate._X_INTER_17 = oNewNumber.START_HLR;
            oTemplate._X_INTER_18 = oNewNumber.END_HLR;
            oTemplate._X_OTHER_PHONE = oNewNumber.NEW_PHONE;
            oTemplate._X_INTER_21 = oNewNumber.CHIP;
            oTemplate._X_INTER_2 = Claro.Constants.NumberOneString;
            oTemplate._X_FLAG_OTHER = oNewNumber.FIDELIZE;
            oTemplate._X_FLAG_REGISTERED = Claro.Constants.LetterN;
            oTemplate._X_INTER_19 = oNewNumber.COST;
            oTemplate._X_INTER_15 = oNewNumber.SALE_POINT;

            oRequest.audit = audit;
            oRequest.Interaction = oInteraction;
            oRequest.InteractionTemplate = oTemplate;
            oRequest.vNroTelefono = oNewNumber.CURRENT_PHONE;
            oRequest.vUSUARIO_SISTEMA = oNewNumber.USER;
            oRequest.vUSUARIO_APLICACION = KEY.AppSettings("strUsuarioAplicacionWSConsultaPrepago");
            oRequest.vPASSWORD_USUARIO = KEY.AppSettings("strPasswordAplicacionWSConsultaPrepago");
            oRequest.vEjecutarTransaccion = true;

            try
            {
                oResponse = Claro.Web.Logging.ExecuteMethod<CommonTransacService.InsertGeneralResponse>(() => { return oServiceCommon.GetinsertInteractionGeneral(oRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }

            oResponseMethod.INTERACTION_ID = oResponse.rInteraccionId;

            if (!oResponse.rFlagInsercion.Equals(Claro.SIACU.Transac.Service.Constants.Message_OK) && !oResponse.rFlagInsercion.Equals(string.Empty))
            {
                oResponseMethod.MESSAGE = " Pero no se registró correctamente la interacción " + oResponse.rMsgText;
            }

            if (!oResponse.rFlagInsercionInteraccion.Equals(Claro.SIACU.Transac.Service.Constants.Message_OK) && !oResponse.rFlagInsercionInteraccion.Equals(string.Empty))
            {
                string strMsgError = " El número de interacción es: " + oResponse.rInteraccionId;
                oResponseMethod.MESSAGE = KEY.AppSettings("strMsjErrorInteraction") + " por el siguiente error: " + oResponse.rMsgTextInteraccion + strMsgError;
            }

            return oResponseMethod;
        }

        public void insertAudit(Areas.Transactions.Models.Postpaid.PostNewNumberModel oNewNumber)
        {
            string strServicio = ConfigurationManager.AppSettings("gConstEvtServicioChangeNumber");
            string strMonto = Claro.Constants.NumberZeroString;
            string strTexto = string.Format("Teléfono: {0}/Contrato: {1}/Cuenta: {2}", oNewNumber.CURRENT_PHONE, oNewNumber.CONTRACT, oNewNumber.ACCOUNT);

            CommonTransacService.AuditRequest AuditRequest = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(oNewNumber.SESSION);
            CommonTransacService.SaveAuditRequestCommon objRegAuditRequest = new CommonTransacService.SaveAuditRequestCommon()
                {
                    audit = AuditRequest,
                    vCuentaUsuario = oNewNumber.USER,
                    vIpCliente = AuditRequest.ipAddress,
                    vIpServidor = AuditRequest.ipAddress,
                    vMonto = strMonto,
                    vNombreCliente = AuditRequest.userName,
                    vNombreServidor = AuditRequest.applicationName,
                    vServicio = strServicio,
                    vTelefono = oNewNumber.CURRENT_PHONE,
                    vTexto = strTexto,
                    vTransaccion = AuditRequest.transaction,
                };
            CommonTransacService.SaveAuditResponseCommon objRegAuditResponse = new CommonTransacService.SaveAuditResponseCommon();

            try
            {
                objRegAuditResponse = Claro.Web.Logging.ExecuteMethod<CommonTransacService.SaveAuditResponseCommon>(() => { return oServiceCommon.SaveAudit(objRegAuditRequest); });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(AuditRequest.Session, AuditRequest.transaction, ex.Message);

            }
        }

        public void DeleteUserHistory(string strIdSession, string strPhone)
        {
            string strResponse = string.Empty;
            string strMotive = KEY.AppSettings("strMotiveCambioNro");
            PostTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostTransacService.AuditRequest>(strIdSession);

            try
            {
                strResponse = Claro.Web.Logging.ExecuteMethod<string>(() => { return oServicePostpaid.DeleteUserHistory(audit.Session, audit.transaction, strResponse, strMotive); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public Models.Postpaid.DataNewNumber GetConstancyPDF(Areas.Transactions.Models.Postpaid.PostNewNumberModel oNewNumber, string strInteraction)
        {
            Models.Postpaid.DataNewNumber oPDF = new Model.Postpaid.DataNewNumber();
            oNewNumber.PROGRAM_DATE = DateTime.Today.ToShortDateString();
            oNewNumber.ACTION = KEY.AppSettings("strCambioNumeroAccionEjecutar");

            FixedTransacService.AuditRequest objAuditRequest = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(oNewNumber.SESSION);

            try
            {

                CommonTransacService.ParametersGeneratePDF oParameter = new CommonTransacService.ParametersGeneratePDF()
                {
                    StrNombreArchivoTransaccion = KEY.AppSettings("strCambioNumeroFormatoTransac"),
                    StrCentroAtencionArea = oNewNumber.SALE_POINT,
                    StrTitularCliente = oNewNumber.TITRE,
                    StrRepresLegal = oNewNumber.LEGALREP,
                    StrTipoDocIdentidad = oNewNumber.DOCUMENT_TYPE,
                    StrNroDocIdentidad = oNewNumber.DOCUMENT,
                    StrFechaTransaccionProgram = oNewNumber.PROGRAM_DATE,
                    StrCasoInter = strInteraction,
                    strAccionEjecutar = oNewNumber.ACTION,
                    strContrato = oNewNumber.CONTRACT,
                    StrNroServicio = oNewNumber.CURRENT_PHONE,
                    strNroAnterior = oNewNumber.CURRENT_PHONE,
                    strNroNuevo = oNewNumber.NEW_PHONE,
                    StrCostoTransaccion = oNewNumber.COST,
                    strLocucion = Claro.SIACU.Transac.Service.Constants.Variable_NO,
                    strCostoLocucion = Claro.Constants.NumberZeroString,
                    strDuracionLocucion = string.Empty,
                    strEnvioCorreo = Claro.SIACU.Transac.Service.Constants.Variable_NO,
                    StrEmail = string.Empty,
                    StrCarpetaTransaccion = KEY.AppSettings("strCarpetaCambioNumeroPost"),
                    StrContenidoComercial = Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("ChangeNumberContentCommercial", KEY.AppSettings("strConstArchivoSIACPOConfigMsg")),
                    StrContenidoComercial2 = Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("ChangeNumberContentCommercial2", KEY.AppSettings("strConstArchivoSIACPOConfigMsg"))
                };
                Areas.Transactions.Controllers.CommonServicesController oCommonHandler = new Areas.Transactions.Controllers.CommonServicesController();
                CommonTransacService.GenerateConstancyResponseCommon response = oCommonHandler.GenerateContancyPDF(objAuditRequest.Session, oParameter);
                if (response.Generated)
                {
                    oPDF.NAME_PDF = response.FullPathPDF;
                    oCommonHandler.InsertEvidence(oNewNumber.SESSION, oNewNumber.CURRENT_PHONE, oNewNumber.CURRENT_PHONE, oNewNumber.SUB_CLASS, oNewNumber.SUB_CLASS,
                        strInteraction, KEY.AppSettings("strCambioNumeroFormatoTransac"), response.Path,response.Document,oNewNumber.USER);
                }
                else
                {
                    oPDF.NAME_PDF = string.Empty;
                }
                
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objAuditRequest.Session, objAuditRequest.transaction, ex.Message);
                throw new Exception(ex.Message);
            }

            return oPDF;
        }

        public JsonResult GetMessage(string strIdSession)
        {
            ArrayList lstMessage = new ArrayList();
            lstMessage.Add(KEY.AppSettings("strConsumerChangeNumberCost"));
            lstMessage.Add(KEY.AppSettings("strConsumerTranslateNumberCost"));
            lstMessage.Add(KEY.AppSettings("strIGVPercentageType2"));
            lstMessage.Add(KEY.AppSettings("strWSZsansEstadoLineTelf"));
            return Json(lstMessage, JsonRequestBehavior.AllowGet);
        }
	}
}