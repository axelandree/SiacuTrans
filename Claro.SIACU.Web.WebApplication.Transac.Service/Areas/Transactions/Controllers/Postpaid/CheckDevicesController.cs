using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Claro.SIACU.Transac.Service;
using Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models.Postpaid;
using Claro.SIACU.Web.WebApplication.Transac.Service.PostTransacService;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Controllers.Postpaid
{
    public class CheckDevicesController : Controller
    {
        private readonly PostTransacService.PostTransacServiceClient oServicePostpaid = new PostTransacService.PostTransacServiceClient();

        //
        // GET: /Transactions/CheckDevices/
        public ActionResult PostpaidCheckDevices()
        {
            return PartialView();
        }

        public JsonResult Page_Load()
        {
            
            return Json(new {});
        }

        public JsonResult GetProductDetails(CheckDevicesModel model)
        {
            PostTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostTransacService.AuditRequest>(model.StrIdSession);

            try
            {
                ServicesDTHRequest objRequest = new ServicesDTHRequest()
                {
                    intCoId = Functions.CheckInt(model.StrCoId),
                    strMsisdn = model.StrMsisdn,
                    audit = audit
                };

                ServicesDTHResponse objtServicesDTH = GetServicesDTH(objRequest);
                model.StrCountItems = Functions.CheckStr(objtServicesDTH.ListServicesDTH.Count);
                model.ListServicesDTH = objtServicesDTH.ListServicesDTH;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info("Session: " + model.StrIdSession, "Transaction: GetProductDetails ", string.Format("Error : {0}", ex.Message));
            }

            return Json(model);
        }



        private ServicesDTHResponse GetServicesDTH(ServicesDTHRequest objRequest)
        {
            ServicesDTHResponse objResponse = new ServicesDTHResponse();
            objResponse = Claro.Web.Logging.ExecuteMethod<ServicesDTHResponse>(() =>
            {
                return oServicePostpaid.GetServicesDTH(objRequest);
            });
            return objResponse;
        }
	}
}