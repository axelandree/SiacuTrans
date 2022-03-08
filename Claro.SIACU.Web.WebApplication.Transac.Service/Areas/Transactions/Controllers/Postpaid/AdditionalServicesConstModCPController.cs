using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Claro.SIACU.Transac.Service;
using Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models;
using Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models.Postpaid;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Controllers.Postpaid
{
    public class AdditionalServicesConstModCPController : Controller
    {
        //
        // GET: /Transactions/AdditionalServicesConstModCP/
        public ActionResult AdditionalServicesConstModCP()
        {
            return View();
        }

        public JsonResult Page_Load(AdditionalServicesModel model)
        {
            LoadInteraction(model);
            //return Json(new { });
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        private void LoadInteraction(AdditionalServicesModel model)
        {
            TemplateInteractionModel objDataTemplateInteraction = new TemplateInteractionModel();
            CommonServicesController objProcess = new CommonServicesController();

            string strCasoId = model.HidCaseId;
            //string strFlagConsult = string.Empty;
            //string strMsgTxt = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(strCasoId))
                {
                    model.MessageCode = "A";
                    model.Message = "El número de interacción no fue cargada correctamente.";
                    return;
                }

                if (!string.IsNullOrEmpty(strCasoId))
                {
                    

                    objDataTemplateInteraction = objProcess.GetInfoInteractionTemplate(model.IdSession, strCasoId);
                }

                LoadData(objDataTemplateInteraction, model);
                model.StrHidCodInter = strCasoId;
                


            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(model.IdSession, "Transaction: LoadInteraction", string.Format("Error al cargar plantilla Interacción: {0}", ex.Message));
            }
        }

        private void LoadData(TemplateInteractionModel objDataTemplateInteraction, AdditionalServicesModel model)
        {
            try
            {

                objDataTemplateInteraction = new TemplateInteractionModel();
                model.StrlblNumberPhone = objDataTemplateInteraction.X_CLARO_NUMBER;
                model.StrlblDNIRUC = objDataTemplateInteraction.X_DOCUMENT_NUMBER;
                model.StrlblTypeClient = objDataTemplateInteraction.X_LASTNAME_REP;
                model.StrlblClient = objDataTemplateInteraction.X_LAST_NAME + " " + objDataTemplateInteraction.X_FIRST_NAME;
                model.StrlblContactClient = objDataTemplateInteraction.X_NAME_LEGAL_REP;
                model.StrlblContact = objDataTemplateInteraction.X_NAME_LEGAL_REP;
                model.StrlblServiceBusiness = objDataTemplateInteraction.X_ADDRESS5;
                model.StrlblNewNumPerVal = Functions.CheckStr(objDataTemplateInteraction.X_CHARGE_AMOUNT);
                model.StrlblQuotModify = Functions.CheckStr(objDataTemplateInteraction.X_CHARGE_AMOUNT);
                if (objDataTemplateInteraction.X_EMAIL_CONFIRMATION == Claro.Constants.NumberOneString)
                {
                    model.StrlblSendEmail = SIACU.Transac.Service.Constants.gstrVariableSI;
                    model.StrlblEmail = objDataTemplateInteraction.X_EMAIL;
                }
                else
                {
                    model.StrlblSendEmail = SIACU.Transac.Service.Constants.PresentationLayer.gstrVariableNO;
                }

                model.StrlblCacDac = objDataTemplateInteraction.X_INTER_15;
                model.StrlblChargeFixed = Functions.CheckStr(objDataTemplateInteraction.X_INTER_10);
                model.StrlblDateExec = objDataTemplateInteraction.X_CLAROLOCAL6;
                model.StrlblNumPerVal = objDataTemplateInteraction.X_INTER_2;
                if (!string.IsNullOrEmpty(objDataTemplateInteraction.X_INTER_29))
                {
                    model.StrtaskNoteModCP = objDataTemplateInteraction.X_INTER_29 + " " + objDataTemplateInteraction.X_INTER_30;
                }
                else
                {
                    model.StrtaskNoteModCP = objDataTemplateInteraction.X_INTER_30;
                }
                if (model.StrlblNewNumPerVal == ConstantsSiacpo.ConstMenosUno)
                {
                    model.StrlblLegend = ConfigurationManager.AppSettings("gConstLeyenda");
                    model.StrStrlblLegendVisible = "T";
                }
                else
                {
                    model.StrStrlblLegendVisible = "F";
                }

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(model.IdSession, "Transaction: LoadInteraction", string.Format("Carga Datos de plantilla Interacción: {0}", ex.Message));
            }
        }
    }
}