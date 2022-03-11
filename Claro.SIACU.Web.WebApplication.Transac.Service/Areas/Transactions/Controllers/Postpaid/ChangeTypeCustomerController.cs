using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AuditRequestPostpaid = Claro.SIACU.Web.WebApplication.Transac.Service.PostTransacService.AuditRequest;
using Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models.Postpaid;
using Claro.SIACU.Web.WebApplication.Transac.Service.PostTransacService;
using System.Text;
using ConstantsHFC = Claro.SIACU.Transac.Service.Constants;
using Claro.Web;


namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Controllers.Postpaid
{
    public class ChangeTypeCustomerController : CommonServicesController
    {
        private readonly PostTransacServiceClient _oServicePostpaid = new PostTransacServiceClient();


        public ActionResult PostpaidDetail()
        {

            return View();
        }

        public JsonResult LoadPostpaidDetail()
        {
            try
            {
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult PostpaidPlanDetail()
        {
            try
            {
                string idSession = "1";
                string contractID = "1";
                var result = ValidatePermissionPost(idSession, contractID);
                PlanViewModel model = new PlanViewModel();
                model.ListPlan = new List<ListPlan>();
                model.ListPlan = ConsultPlan();
                return PartialView(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult PostpaidPartialConsultPlan()
        {
            PlanViewModel model = new PlanViewModel();
            model.ListPlan = new List<ListPlan>();
            model.ListPlan = ConsultPlan();
            return PartialView(model.ListPlan);
        }
        public List<ListPlan> ConsultPlan()
        {
            var objListPlan = new List<ListPlan>();
            var objAuditRequest = App_Code.Common.CreateAuditRequest<AuditRequestPostpaid>("SESSION");
            var objNewPlanRequest = new NewPlanRequestTransactions
            {
                audit = objAuditRequest,
                CategoriaProducto = "BU5",
                ValorTipoProducto = "01",
                PlanActual = "",
            };

            try
            {
                var objPlanResponse = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return _oServicePostpaid.GetNewPlan(objNewPlanRequest);
                });
                if (objPlanResponse.lstNewPlan.Count > 0)
                {
                    var lstPlanViewModel = objPlanResponse.lstNewPlan;
                    foreach (var item in lstPlanViewModel)
                    {
                        var objPlanViewModel = new ListPlan
                        {
                            strCodeProduct = item.COD_PROD ?? "",
                            strTMCode = item.TMCODE ?? "",
                            strDescriptPlan = item.DESC_PLAN ?? "",
                            strVersion = item.VERSION ?? "",
                            strCategProduct = item.CAT_PROD ?? "",
                            strCodeCartInf = item.COD_CARTA_INFO ?? "",
                            strDateIniVige = item.FECHA_INI_VIG ?? "",
                            strDateFinVige = item.FECHA_FIN_VIG ?? "",
                            strIdTypeProduct = item.ID_TIPO_PROD ?? "",
                            strUser = item.USUARIO ?? ""
                        };
                        objListPlan.Add(objPlanViewModel);
                    }
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error("SESSION", objNewPlanRequest.audit.transaction, ex.Message);
                throw new Exception(objAuditRequest.transaction);
            }

            return objListPlan;
        }

        public JsonResult ValidatePermission(string strIdSession, string strContractId)
        {
            var result = ValidatePermissionPost(strIdSession, strContractId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #region Value AppSettings
        public JsonResult ValueAppSettings(string strIdSession)
        {
            var result = new Dictionary<string, string>
            {
                {"strRutaSiteInicio", ConfigurationManager.AppSettings("strRutaSiteInicio")},
                {"gConstTipoLineaActual", ConfigurationManager.AppSettings("gConstTipoLineaActual")},
                {"gConstMsjNoEsPostNiFijoPost",ConfigurationManager.AppSettings("gConstMsjNoEsPostNiFijoPost")},
                {"gConstTipoLineaAbrev",ConfigurationManager.AppSettings("gConstTipoLineaAbrev")},
                {"gConstMsjNoEsCOB2BU",ConfigurationManager.AppSettings("gConstMsjNoEsPostNiFijoPost")},
                {"strFlagPlataformaControl",ConfigurationManager.AppSettings("strFlagPlataformaControl")},
                {"gConstMsjFlgPlataformaC",ConfigurationManager.AppSettings("gConstMsjFlgPlataformaC")},
                {"gConstCodPlanControlNoAplica",ConfigurationManager.AppSettings("gConstCodPlanControlNoAplica")},
                {"gStrTransAuditFechProg_CamTipClient",ConfigurationManager.AppSettings("gStrTransAuditFechProg_CamTipClient")},
                {"gStrTransAuditChckFideliza_CamTipClient",ConfigurationManager.AppSettings("gStrTransAuditChckFideliza_CamTipClient")},
                {"gStrTransAuditChckSinCosto_CamTipClient",ConfigurationManager.AppSettings("gStrTransAuditChckSinCosto_CamTipClient")},
                {"IGVConsumosoles",ConfigurationManager.AppSettings("IGVConsumosoles")},
                {"gConstFidelizaPenalidadCamTipClient",ConfigurationManager.AppSettings("gConstFidelizaPenalidadCamTipClient")},
                {"gConstModFechaProgCamTipClient",ConfigurationManager.AppSettings("gConstModFechaProgCamTipClient")},
                {"gConstTopeConsSinCostoCamTipClient",ConfigurationManager.AppSettings("gConstTopeConsSinCostoCamTipClient")},
                {"gConstDiferenciaMontoCF",ConfigurationManager.AppSettings("gConstDiferenciaMontoCF")},
                {"OpcTopeConsumoAdicional",ConfigurationManager.AppSettings("OpcTopeConsumoAdicional")},
                {"gConstCambioTitularidad",ConfigurationManager.AppSettings("gConstCambioTitularidad")},
                {"gValidaClienteJanus",ConfigurationManager.AppSettings("gValidaClienteJanus")},
                {"gMensajeValidaJanus",ConfigurationManager.AppSettings("gMensajeValidaJanus")},
                {"OpcTopeConsumoAutomatico",ConfigurationManager.AppSettings("OpcTopeConsumoAutomatico")},
                {"ListOpcTope",ConfigurationManager.AppSettings("ListOpcTope")},
                {"OpcTopeOrden",ConfigurationManager.AppSettings("OpcTopeOrden")},
                {"OpcTopeConsumo5soles",ConfigurationManager.AppSettings("OpcTopeConsumo5soles")},
                {"GstrTransaccionCambioTipoCliente",ConstantsHFC.GstrTransaccionCambioTipoCliente},
                {"strCodTipoCli",ConfigurationManager.AppSettings("strCodTipoCli")}
                //{"NuevoTipoCuenta",GetValueXmlMethod(strIdSession, "SIACPOConfig.Config", "NuevoTipoCuenta")}
            };

            return new JsonResult
            {
                Data = result,
                ContentType = "application/json",
                ContentEncoding = Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        #endregion

        public JsonResult GetDateApplication(string strCicleFacturation)
        {
            var dateCicleFactMenorUno = string.Empty;
            string dateCicloFactMenorUnoTemp;
            var dateAddMonth = new DateTime();

            var dtmDateHoy = DateTime.Now;
            var hidCicleFacturationCustomer = strCicleFacturation;

            var strDay = DateTime.Parse(Convert.ToInt(hidCicleFacturationCustomer) + "/" + dateAddMonth.Month + "/" + dateAddMonth.Year).AddDays(-1).Day;

            if (dtmDateHoy.Day >= Convert.ToInt(hidCicleFacturationCustomer))
            {
                dateAddMonth = dtmDateHoy.AddMonths(1);
                dateCicloFactMenorUnoTemp = string.Format("{0}/{1}/{2}", Convert.ToInt(hidCicleFacturationCustomer), dateAddMonth.Month, dateAddMonth.Year);
                dateAddMonth = DateTime.Parse(dateCicloFactMenorUnoTemp);
                dateCicleFactMenorUno = dateAddMonth.AddDays(-1).ToLongDateString();
            }
            else if (dtmDateHoy.Day < Convert.ToInt(hidCicleFacturationCustomer))
            {
                if (dtmDateHoy.Day == strDay)
                {
                    dateCicloFactMenorUnoTemp = Convert.ToInt(hidCicleFacturationCustomer) + "/" + dateAddMonth.Month + "/" + dateAddMonth.Year;
                    dateAddMonth = DateTime.Parse(dateCicloFactMenorUnoTemp);
                    dateAddMonth = dateAddMonth.AddMonths(1);
                    dateCicleFactMenorUno = dateAddMonth.AddDays(-1).ToShortDateString();
                }
                else
                {
                    dateCicloFactMenorUnoTemp = Convert.ToInt(hidCicleFacturationCustomer) + "/" + dateAddMonth.Month + "/" + dateAddMonth.Year;
                    dateAddMonth = DateTime.Parse(dateCicloFactMenorUnoTemp);
                    dateCicleFactMenorUno = dateAddMonth.AddDays(-1).ToShortDateString();
                }
            }

            var txtDateAplication = dateCicleFactMenorUno;
            return Json(txtDateAplication);
        }

        public JsonResult GetConsumptionStop(string strIdSession, string strListTope)
        {
            ConsumptionStopResponse objConsumptionStopResponse = null;
            PostTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostTransacService.AuditRequest>(strIdSession);
            ConsumptionStopRequest objRequest = new ConsumptionStopRequest()
            {
                audit = audit,
                strCode = strListTope
            };

            try
            {
                objConsumptionStopResponse = Logging.ExecuteMethod<ConsumptionStopResponse>(() =>
                {
                    return _oServicePostpaid.GetConsumptionStop(objRequest);
                });
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }

            return Json(objConsumptionStopResponse.lstConsumptionStop, JsonRequestBehavior.AllowGet);
        }
    }
}