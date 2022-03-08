using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KEY = Claro.ConfigurationManager;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Controllers.HFC
{
    public class ChangePhoneNumberController : Controller
    {
        private readonly FixedTransacService.FixedTransacServiceClient oServiceFixed = new FixedTransacService.FixedTransacServiceClient();
        public ActionResult HFCChangePhoneNumber()
        {
            return PartialView();
        }

        public JsonResult GetCurrentPhone(string strIdSession, string strContractID, string strProductType)
        {
            FixedTransacService.ServiceResponse objServiceResponse = null;
            FixedTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);
            FixedTransacService.ServiceRequest objServiceRequest = new FixedTransacService.ServiceRequest();
            FixedTransacService.Service oService = new FixedTransacService.Service();
            objServiceRequest.audit = audit;
            objServiceRequest.ProductType = strProductType;
            objServiceRequest.ContractID = strContractID;

            try
            {
                objServiceResponse = Claro.Web.Logging.ExecuteMethod<FixedTransacService.ServiceResponse>(() => { return oServiceFixed.GetTelephoneByContractCode(objServiceRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }

            if (objServiceResponse != null && objServiceResponse.ListService.Count > Claro.Constants.NumberZero)
            {
                oService = objServiceResponse.ListService.First();
            }

            return Json(oService, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ExecuteChangePhoneNumber(Models.HFC.PostExecuteChangeNumber oNewNumber)
        {
            FixedTransacService.PhoneNumberResponse objResponse = null;
            FixedTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(oNewNumber.IDSESSION);
            FixedTransacService.PhoneNumberRequest objRequest = new FixedTransacService.PhoneNumberRequest();
            objRequest.audit = audit;
            objRequest.CLASIF_RED = oNewNumber.CLASIF_RED;
            objRequest.CONTRACT_ID = oNewNumber.CONTRACT_ID;
            objRequest.COST_LOCU = oNewNumber.COST_LOCU;
            objRequest.COST_LOCU_IGV = oNewNumber.COST_LOCU_IGV;
            //objRequest.COST_TRANS = oNewNumber.COST_TRANS;
            objRequest.COST_TRANS = oNewNumber.COST_TRANS;
            objRequest.COST_TRANS_IGV = oNewNumber.COST_TRANS_IGV;

            objRequest.CUSTOMER_ID = oNewNumber.CUSTOMER_ID;
            objRequest.CUSTOMER_TYPE = oNewNumber.CUSTOMER_TYPE;
            objRequest.FLAG_FIDEL_LOCU = oNewNumber.FLAG_FIDEL_LOCU;
            objRequest.FLAG_FIDEL_TRANS = oNewNumber.FLAG_FIDEL_TRANS;
            objRequest.FLAG_LOCU = oNewNumber.FLAG_LOCU;
            objRequest.FLAG_SEND_EMAIL = oNewNumber.FLAG_SEND_EMAIL;
            if (objRequest.FLAG_SEND_EMAIL == Claro.Constants.NumberZeroString) objRequest.EMAIL = string.Empty; else objRequest.EMAIL = oNewNumber.EMAIL;
            objRequest.NATIONAL_CODE = oNewNumber.NATIONAL_CODE;
            objRequest.NUMBER_PHONES = oNewNumber.NUMBER_PHONES;
            objRequest.TYPE_NUMBER = oNewNumber.TYPE_NUMBER;
            objRequest.TYPE_PHONE = oNewNumber.TYPE_PHONE;
            objRequest.NRO_TELEF = oNewNumber.NRO_TELEF;
            objRequest.HLR_CODE = oNewNumber.HLR_CODE;
            objRequest.FLAG_PLAN_TIPI = oNewNumber.FLAG_PLAN_TIPI;
            objRequest.POINT_ATTENTION = oNewNumber.POINT_ATTENTION;
            objRequest.NOTES = oNewNumber.NOTES;
            objRequest.CONTACT = oNewNumber.CONTACT;
            objRequest.FULL_NAME = oNewNumber.FULL_NAME;
            objRequest.DOCUMENT = oNewNumber.DOCUMENT;
            objRequest.METHOD = KEY.AppSettings("MetodoContactoTelefonoDefault");
            objRequest.RESULT = KEY.AppSettings("Ninguno");
            objRequest.TIPO_INTER = KEY.AppSettings("AtencionDefault");
            objRequest.USER_PROCESS = KEY.AppSettings("USRProcesoSU");
            objRequest.CUSTIDOBJID = KEY.AppSettings("gConstKeyCustomerInteract") + oNewNumber.CUSTOMER_ID;

            if (oNewNumber.TYPE_NUMBER == KEY.AppSettings("gConstTipoHFC"))
            {
                objRequest.TYPE = KEY.AppSettings("strCNTypeHFCDesc");
                objRequest.CLASS = KEY.AppSettings("strCNClassHFCDesc");
                objRequest.SUBCLASS = KEY.AppSettings("strCNSubClassHFCDesc");
                objRequest.CLASIF_RED = KEY.AppSettings("strClasifRefCNHFC");
            }else{
                objRequest.TYPE = KEY.AppSettings("strCNTypeLTEDesc");
                objRequest.CLASS = KEY.AppSettings("strCNClassLTEDesc");
                objRequest.SUBCLASS = KEY.AppSettings("strCNSubClassLTEDesc");
                objRequest.CLASIF_RED = KEY.AppSettings("strClasifRefCNLTE");
            }

            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<FixedTransacService.PhoneNumberResponse>(() => { return oServiceFixed.GetExecuteChangeNumber(objRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }

            return Json(objResponse, JsonRequestBehavior.AllowGet); 
        }

        public JsonResult GetMessage(string strIdSession)
        {
            ArrayList lstMessage = new ArrayList();
            lstMessage.Add(KEY.AppSettings("strHFCCambioNumeroCostoTrans"));
            lstMessage.Add(KEY.AppSettings("strHFCCambioNumeroCostoLocucion"));
            lstMessage.Add(KEY.AppSettings("strEstadoContratoInactivo"));
            lstMessage.Add(KEY.AppSettings("strEstadoContratoSuspendido"));
            lstMessage.Add(KEY.AppSettings("strEstadoContratoReservado"));
            lstMessage.Add(KEY.AppSettings("strMsjEstadoContratoInactivo"));
            lstMessage.Add(KEY.AppSettings("strMsjEstadoContratoSuspendido"));
            lstMessage.Add(KEY.AppSettings("strMsjEstadoContratoReservado"));
            lstMessage.Add(KEY.AppSettings("strMsjSinTelefoniaFija"));
            lstMessage.Add(KEY.AppSettings("strServidorLeerPDF"));
            lstMessage.Add(KEY.AppSettings("strCNSubClassHFC"));
            lstMessage.Add(KEY.AppSettings("strMensajeErrorConsultaIGV"));
            lstMessage.Add(KEY.AppSettings("strMensajeBackOfficeFTTH")); //EVALENZS - INICIO
            lstMessage.Add(KEY.AppSettings("strPlanoFTTH")); //EVALENZS - FIN
            return Json(lstMessage, JsonRequestBehavior.AllowGet);
        }
	}
}