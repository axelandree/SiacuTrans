using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models.Postpaid;
using Claro.SIACU.Web.WebApplication.Transac.Service.PostTransacService;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Controllers.Postpaid
{
    public class AdditionalServicesDetailCommercialController : Controller
    {
        private readonly PostTransacService.PostTransacServiceClient oServicePostpaid = new PostTransacService.PostTransacServiceClient();

        //
        // GET: /Transactions/AdditionalServicesDetailCommercial/
        public ActionResult AdditionalServicesDetailCommercial()
        {
            return View();
        }

        public JsonResult Page_Load(AdditionalServicesModel model)
        {
            //AdditionalServicesModel model = new AdditionalServicesModel();
            try
            {
                Start(model);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(model.IdSession, "Transaction: Start ", string.Format("Error: {0}", ex.Message));
                model.MessageCode = "E";
                model.MessageLabel = ex.Message;
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        private void Start(AdditionalServicesModel model)
        {
            string strSystem = "SIACPO";
            ServiceBSCSResponse objArray = new ServiceBSCSResponse();
            try
            {
                objArray = ServiceBSCS(model, strSystem);
                if (objArray.ListServiceBSCS.Count == Claro.Constants.NumberZero)
                {
                    model.MessageCode = "A";
                    model.Message = "La linea no cuenta con ningun contrato.";
                    return;
                }
                else
                {
                    model.StrlblServiceCommercial = objArray.StrDesServ;
                    model.ListServiceBSCS = objArray.ListServiceBSCS;
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(model.IdSession, "Transaction: Start ", string.Format("Error: {0}", ex.Message));
            }
        }

        private ServiceBSCSResponse ServiceBSCS(AdditionalServicesModel model, string strSystem)
        {
            PostTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostTransacService.AuditRequest>(model.IdSession);
            ServiceBSCSResponse objResponse = new ServiceBSCSResponse();
            try
            {
                ServiceBSCSRequest objRequest = new ServiceBSCSRequest();
                objRequest.StrUser = model.UserLogin;
                objRequest.StrSystem = strSystem;
                objRequest.StrCodServ = model.StrCodSer;
                objRequest.audit = audit;

                objResponse = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return oServicePostpaid.GetServiceBSCS(objRequest);
                });


            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(model.IdSession, audit.transaction, ex.Message);
                throw new Exception(audit.transaction);
            }

            return objResponse;
        }

	}
}