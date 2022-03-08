using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KEY = Claro.ConfigurationManager;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Controllers.LTE
{
    public class ChangePhoneNumberController : Controller
    {
        //
        // GET: /Transactions/ChangePhoneNumber/
        public ActionResult LTEChangePhoneNumber()
        {
            return PartialView();
        }

        public JsonResult GetMessage(string strIdSession)
        {
            ArrayList lstMessage = new ArrayList();
            lstMessage.Add(KEY.AppSettings("strLTECambioNumeroCostoTrans"));
            lstMessage.Add(KEY.AppSettings("strLTECambioNumeroCostoLocucion"));
            lstMessage.Add(KEY.AppSettings("strEstadoContratoInactivo"));
            lstMessage.Add(KEY.AppSettings("strEstadoContratoSuspendido"));
            lstMessage.Add(KEY.AppSettings("strEstadoContratoReservado"));
            lstMessage.Add(KEY.AppSettings("strMsjEstadoContratoInactivo"));
            lstMessage.Add(KEY.AppSettings("strMsjEstadoContratoSuspendido"));
            lstMessage.Add(KEY.AppSettings("strMsjEstadoContratoReservado"));
            lstMessage.Add(KEY.AppSettings("strMsjSinTelefoniaFija"));
            lstMessage.Add(KEY.AppSettings("strServidorLeerPDF"));
            lstMessage.Add(KEY.AppSettings("strCNSubClassLTE"));
            lstMessage.Add(KEY.AppSettings("strMensajeErrorConsultaIGV"));
            return Json(lstMessage, JsonRequestBehavior.AllowGet);
        }
	}
}