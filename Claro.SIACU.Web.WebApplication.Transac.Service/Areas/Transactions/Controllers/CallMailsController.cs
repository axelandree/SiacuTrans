#region "RHJ: Transversal - Send Mail"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KEY = Claro.ConfigurationManager;
//using RELATIONPLAN_MODEL = Claro.SIACU.Web.WebApplication.Areas.Transaction.Models.Postpaid.PostpaidAccountPenals;
//using HELPER = Claro.SIACU.Web.WebApplication.Areas.Transaction.Helpers.Postpaid;


namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Controllers
{
    public class CallMailsController : Controller
    {
        //
        // GET: /Transactions/CallMails/
        //private readonly Claro.Helpers.ExcelHelper oExcelHelper = new Claro.Helpers.ExcelHelper();
        private readonly PostTransacService.PostTransacServiceClient oPostServiceT = new PostTransacService.PostTransacServiceClient();

        public ActionResult Index()
        {
            return View();
        }

        //public JsonResult CallMails(string strIdSession, string strTransaction, string strRemitente, string strPara, string strCC, string strBCC, string strAsunto, string strMensaje, List<string> lstArchivos_)
       // {
        
       // }


	}
}
#endregion