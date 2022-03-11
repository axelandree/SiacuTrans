using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Controllers.LTE
{
    public class CallDetailsController : Controller
    {
        //
        // GET: /Transactions/BilledCallsDetail/
        public ActionResult LTEBilledCallsDetail()
        {
            return View();
        }
        public ActionResult LTEUnbilledCallDetail()
        {
            return View();
        }
	}
}