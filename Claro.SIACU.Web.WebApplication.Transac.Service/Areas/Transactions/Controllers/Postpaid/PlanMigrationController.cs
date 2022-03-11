using Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ConsTRans=Claro.SIACU.Transac.Service;
using Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.CommonServices;
using model= Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models;
using Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.Postpaid.PostpaidIncomingCall;
using KEY = Claro.ConfigurationManager;
using TransCommon = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Controllers.CommonServicesController;
using Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models.PlanMigration;
using Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models.Postpaid;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Controllers.Postpaid
{
    public class PlanMigrationController : Controller
    {
        //
        // GET: /Transactions/PostPlanMigration/

        private readonly CommonTransacServiceClient _oServiceCommon = new CommonTransacServiceClient();
        private readonly PostTransacService.PostTransacServiceClient oPostServiceT = new PostTransacService.PostTransacServiceClient();
        
        public ActionResult PostpaidPlanMigration()
        {
            return PartialView();
        }
        public ActionResult PostpaidPlansMigrations()
        {
            return PartialView();
        }
        public JsonResult ChargeMain(string strIdSession, string ConsumerControl, string codTipoCliente, string telephone, string contratoId, string HidCorporativo, string FlagPlataforma, string TipoCliente)
        {
            model.PlanMigration.PostPaidPlanMigration objPostPaidPlanMigration = new model.PlanMigration.PostPaidPlanMigration();

            // Get Consumption cap
            string hidOpTopeConsOrd = string.Empty,
                hidOpTopeConsCod = string.Empty,
                hidOpTopeConsDesc = string.Empty,
                hidOpTopeCons5Soles = string.Empty,
                hidValidaTipoCliente = string.Empty,
                hidValidaCargoFijoSAP;

            double douPenalidadAPADECE;

            List<Helpers.CommonServices.GenericItem> ListTopConsum = GetTopConsum(strIdSession, KEY.AppSettings("ListOpcTope").ToString(), ConsumerControl,
                codTipoCliente, out hidOpTopeConsOrd, out hidOpTopeConsCod, out hidOpTopeConsDesc, out hidOpTopeCons5Soles, out hidValidaTipoCliente);

            objPostPaidPlanMigration.listItemOpcTope = ListTopConsum;
            objPostPaidPlanMigration.hidOpTopeConsOrd = hidOpTopeConsOrd;
            objPostPaidPlanMigration.hidOpTopeConsCod = hidOpTopeConsCod;
            objPostPaidPlanMigration.hidOpTopeConsDesc = hidOpTopeConsDesc;
            objPostPaidPlanMigration.hidOpTopeCons5Soles = hidOpTopeCons5Soles;
            objPostPaidPlanMigration.hidValidaTipoCliente = hidValidaTipoCliente;

            //Load APADECE
            douPenalidadAPADECE = Convert.ToDouble(GetApadece(strIdSession, ConsTRans.Constants.strCero, contratoId, DateTime.Now.ToShortDateString(),
                                KEY.AppSettings("strFlagEquipo"), KEY.AppSettings("strMotivoApadece"), telephone, out hidValidaCargoFijoSAP));

            if (HidCorporativo != "1")
            {
                objPostPaidPlanMigration.txtMontoSIGA = Math.Round(douPenalidadAPADECE, 2).ToString();
                objPostPaidPlanMigration.txtCobroApadece = ConsTRans.Constants.strCero;
                objPostPaidPlanMigration.hidCobroApadece = objPostPaidPlanMigration.txtCobroApadece;
                objPostPaidPlanMigration.txtCobroApadeceEnable = "False";

                if (objPostPaidPlanMigration.txtCobroApadece != ((int)ConsTRans.Modo.Valor_Minimo).ToString())
                {
                    objPostPaidPlanMigration.chkFideliza = "true";//enable
                    objPostPaidPlanMigration.chkOcc = "true";//check
                }
                else
                {
                    objPostPaidPlanMigration.chkFideliza = "false";//enable
                    objPostPaidPlanMigration.chkOcc = "false";//check
                }

            }
            else
            {
                objPostPaidPlanMigration.txtCobroApadece = ((int)ConsTRans.Modo.Valor_Minimo).ToString();
                objPostPaidPlanMigration.hidCobroApadece = ((int)ConsTRans.Modo.Valor_Minimo).ToString();
                objPostPaidPlanMigration.txtCobroApadeceEnable = "True";
            }

            objPostPaidPlanMigration.txtMontoFidelizaApadece = ConsTRans.Modo.Valor_Minimo.ToString();
            objPostPaidPlanMigration.txtTotalApadeceCobrar = Math.Round(Convert.ToDecimal(objPostPaidPlanMigration.hidCobroApadece) - Convert.ToDecimal(objPostPaidPlanMigration.txtMontoFidelizaApadece), 2).ToString();
            objPostPaidPlanMigration.txtCobroPenalidadPCS = ((int)ConsTRans.Modo.Valor_Minimo).ToString();
            objPostPaidPlanMigration.txtMontoPenalidadPCS = ((int)ConsTRans.Modo.Valor_Minimo).ToString();
            objPostPaidPlanMigration.txtTotalPenalidadPCS = ((int)ConsTRans.Modo.Valor_Minimo).ToString();

            if (KEY.AppSettings("strFlagPlataformaControl").Equals(FlagPlataforma))
            {
                objPostPaidPlanMigration.hidConsumerControl = ConsTRans.Constants.blcasosVariableSI;
            }
            else
            {
                objPostPaidPlanMigration.hidConsumerControl = ConsTRans.Constants.blcasosVariableNO;
            }

            if (objPostPaidPlanMigration.hidConsumerControl.Equals(ConsTRans.Constants.blcasosVariableSI))
            {
                objPostPaidPlanMigration.hidCorporativo = ConsTRans.Constants.strCero;
            }
            else if (TipoCliente.Equals(KEY.AppSettings("strCodTipoCli").Split('|')[1]))
            {
                objPostPaidPlanMigration.hidCorporativo = ConsTRans.Constants.strCero;
            }
            else
            {
                objPostPaidPlanMigration.hidCorporativo = ConsTRans.Constants.strUno;
            }

            objPostPaidPlanMigration.CodPlanClaroConexionChip = KEY.AppSettings("gConstCodPlanClaroConexionChip");
            objPostPaidPlanMigration.gConstMsjPlanClaroConexionChip = KEY.AppSettings("gConstMsjPlanClaroConexionChip");
            objPostPaidPlanMigration.TipoClienteAplica = KEY.AppSettings("TipoClienteAplica");
            objPostPaidPlanMigration.gCodCostoCero = KEY.AppSettings("gCodCostoCero");
            objPostPaidPlanMigration.CodPlanSinTopeConsAutorizacion = KEY.AppSettings("CodPlanSinTopeConsAutorizacion");
            objPostPaidPlanMigration.hidOpTopeCodAutomatico = KEY.AppSettings("OpcTopeConsumoAutomatico");
            objPostPaidPlanMigration.hidFechaActual = DateTime.Now.ToShortDateString();
            objPostPaidPlanMigration.hidFechaLimite = DateTime.Now.AddMonths(1).ToShortDateString();
            objPostPaidPlanMigration.strConfigPlantaforma = KEY.AppSettings("strFlagPlataformaControl");

            CommonServicesController objCommonService = new CommonServicesController();
            string[] resp = new string[2];
            resp[0] = objCommonService.ValidatePermissionPost(strIdSession, contratoId);
            resp[1] = KEY.AppSettings("gConstMsjNoEsPostNiFijoPost");
            return Json(new { data = objPostPaidPlanMigration, result = resp });
        }
        public List<Helpers.CommonServices.GenericItem> GetTopConsum(string strIdSession, string strClave, string ConsumerControl, string codTipoCliente, out string hidOpTopeConsOrd, out string hidOpTopeConsCod, out string hidOpTopeConsDesc, out string hidOpTopeCons5Soles, out string hidValidaTipoCliente)
        {
            PostTransacService.ParameterBusinnesResponse objResponse = null;
            PostTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostTransacService.AuditRequest>(strIdSession);
            PostTransacService.ParameterBusinnesRequest objRequest = new PostTransacService.ParameterBusinnesRequest()
            {
                audit = audit,
                strIdList = strClave
            };

            hidOpTopeConsOrd = string.Empty;
            hidOpTopeConsCod = string.Empty;
            hidOpTopeConsDesc = string.Empty;
            hidOpTopeCons5Soles = string.Empty;
            hidValidaTipoCliente = string.Empty;

            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<PostTransacService.ParameterBusinnesResponse>(() =>
                {
                    return oPostServiceT.GetPlanModel(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }

            Helpers.CommonServices.GenericItem objList;
            List<Helpers.CommonServices.GenericItem> objListReturn = new List<Helpers.CommonServices.GenericItem>();

            foreach (var item in objResponse.lstParameterBusinnes)
            {
                objList = objList = new Helpers.CommonServices.GenericItem();
                objList.Number = item.strNumber;
                objList.Description = item.strDescription;
                objList.Code = item.strCode;

                if (objList.Number == KEY.AppSettings("OpcTopeOrden"))
                {
                    hidOpTopeConsOrd = objList.Number;
                    hidOpTopeConsCod = objList.Code;
                    hidOpTopeConsDesc = objList.Description;
                }

                if (objList.Code == KEY.AppSettings("OpcTopeConsumo5soles"))
                {
                    hidOpTopeCons5Soles = objList.Code;
                }

                if (objList.Number != KEY.AppSettings("OpcTopeOrden"))
                {
                    objListReturn.Add(objList);
                }

                if (objList.Code == KEY.AppSettings("strCodigoTipoTopeConFidelizacion"))
                {
                    hidOpTopeConsOrd = objList.Number;
                    hidOpTopeConsCod = objList.Code;
                    hidOpTopeConsDesc = objList.Description;
                }

                if (ConsumerControl.Equals(ConsTRans.Constants.blcasosVariableSI))
                {
                    hidValidaTipoCliente = "1";
                }
                else
                {
                    if (codTipoCliente.Equals(KEY.AppSettings("strCodTipoCli").Split('|')[1]))
                    {
                        hidValidaTipoCliente = "1";
                    }
                }

                if (codTipoCliente.Equals(KEY.AppSettings("strCodTipoCli").Split('|')[4]))
                {
                    if (objList.Code == KEY.AppSettings("OpcTopeConsumoAdicional"))
                    {
                        hidValidaTipoCliente = "2";
                        break;
                    }
                }
                else if (codTipoCliente.Equals(KEY.AppSettings("strCodTipoCli").Split('|')[0]))
                {
                    if (objList.Code == KEY.AppSettings("OpcTopeConsumoAdicional"))
                    {
                        hidValidaTipoCliente = "2";
                        break;
                    }
                }

            }
            return objListReturn;
        }
        public string GetApadece(string strIdSession, string CargoFijoNuevo, string CoId, string FechaTransaccion, string FlagEquipo
                                , string MontoApadece, string msisdn, out string hidValidaCargoFijoSAP)
        {
            string strMensajeRespuesta = string.Empty;
            hidValidaCargoFijoSAP = ConsTRans.Constants.numeroUno.ToString();
            PostTransacService.AgreementResponse objResponse = null;
            PostTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostTransacService.AuditRequest>(strIdSession);
            PostTransacService.AgreementResquest objRequest = new PostTransacService.AgreementResquest()
            {
                audit = audit,
                CargoFijoNuevo = CargoFijoNuevo,
                CoId = CoId,
                FechaTransaccion = FechaTransaccion,
                FlagEquipo = FlagEquipo,
                MontoApadece = MontoApadece,
                msisdn = msisdn
            };

            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<PostTransacService.AgreementResponse>(() =>
                { return oPostServiceT.GetReinstatementEquipment(objRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }

            if (objResponse.CodRespuesta == ConsTRans.Constants.strCero)
            {
                strMensajeRespuesta = objResponse.ACUERDO_MONTO_APADECE_TOTAL;
            }
            else
            {
                strMensajeRespuesta = ConsTRans.Constants.strCero;
            }

            return strMensajeRespuesta;
        }
        public string ConsultFixedCharge(string strIdSession, string strPhone)
        {
            string strResponse;
            PostTransacService.FixedChargeResponseTransactions oResponse = null;
            PostTransacService.FixedChargeRequestTransactions oRequest = new PostTransacService.FixedChargeRequestTransactions();
            PostTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostTransacService.AuditRequest>(strIdSession);
            oRequest.audit = audit;
            oRequest.Flag = Claro.Constants.NumberOneString;
            oRequest.Valor = strPhone;

            try
            {
                oResponse = Claro.Web.Logging.ExecuteMethod<PostTransacService.FixedChargeResponseTransactions>(() => { return oPostServiceT.ConsultFixedCharge(oRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }

            if (oResponse.Error == Claro.Constants.NumberZeroString)
            {
                strResponse = oResponse.CargoFijo;
            }
            else
            {
                strResponse = string.Empty;
            }

            return strResponse;
        }
        public string GetReceiptDetail(string strIdSession, string strCustomerID)
        {
            string strResponse;
            PostTransacService.ReceiptResponseTransactions oResponse = null;
            PostTransacService.ReceiptRequestTransactions oRequest = new PostTransacService.ReceiptRequestTransactions();
            PostTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostTransacService.AuditRequest>(strIdSession);
            oRequest.audit = audit;
            oRequest.CustomerCode = strCustomerID;
            try
            {
                oResponse = Claro.Web.Logging.ExecuteMethod<PostTransacService.ReceiptResponseTransactions>(() => { return oPostServiceT.GetDataInvoice(oRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }

            if (oResponse != null && oResponse.ObjReceipt != null)
            {
                strResponse = oResponse.ObjReceipt.FECHA_VENCIMIENTO;
            }
            else
            {
                strResponse = string.Empty;
            }

            return strResponse;
        }
        public void GetConsumeLimit(string strIdSession, string strPhone, string strContractID, ref string strCodServ, ref string strTope, ref string strMountTope)
        {
            PostTransacService.ConsumeLimitResponseTransactions oResponse = null;
            PostTransacService.ConsumeLimitRequestTransactions oRequest = new PostTransacService.ConsumeLimitRequestTransactions();
            PostTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostTransacService.AuditRequest>(strIdSession);
            oRequest.audit = audit;
            oRequest.Telefono = strPhone;
            oRequest.IdContrato = strContractID;

            try
            {
                oResponse = Claro.Web.Logging.ExecuteMethod<PostTransacService.ConsumeLimitResponseTransactions>(() => { return oPostServiceT.GETConsumeLimit(oRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }

            if (oResponse != null && oResponse.LstConsumeLimit != null)
            {
                if (oResponse.LstConsumeLimit.Count > 0)
                {
                    foreach (PostTransacService.ComsumeLimitTransactions item in oResponse.LstConsumeLimit)
                    {
                        strCodServ = item.CO_SER;
                        strTope = item.DESC_SERV;
                        strMountTope = item.TOPE;
                    }
                }
                else
                {
                    strTope = KEY.AppSettings("gConstkeyILIMITADO");
                    strCodServ = string.Empty;
                    strMountTope = string.Empty;
                }
            }
            else
            {
                strTope = KEY.AppSettings("gConstkeyILIMITADO");
                strCodServ = string.Empty;
                strMountTope = string.Empty;
            }

        }
        public JsonResult GetValidationProgDeudaBloqSuspResponse(string strIdSession, string phoneNumber, string contract)
        {
            PostTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostTransacService.AuditRequest>(strIdSession);

            PostTransacService.ValidationProgDeudaBloqSuspRequest objRequest = new PostTransacService.ValidationProgDeudaBloqSuspRequest()
            {
                audit = audit,
                Telefono = phoneNumber,
                Contrato = contract
            };


            PostTransacService.ValidationProgDeudaBloqSuspResponse objResponse = Claro.Web.Logging.ExecuteMethod<PostTransacService.ValidationProgDeudaBloqSuspResponse>(
                () =>
                {
                    return oPostServiceT.GetValidationProgDeudaBloqSuspResponse(objRequest);
                });

            return Json(new { data = objResponse });
        }
        public JsonResult GetIsBloqAllowConfiguration(string strIdSession, string Bloq)
        {
            List<ListItemVM> lstListItemVM = new List<ListItemVM>();
            string[] result = new string[2];
            bool blRespuesta = false;
            CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
            ListItemRequest objListItemRequest = new ListItemRequest
            {
                audit = audit,
                strFlagCode = "1",
                strNameFunction = "ValidarBloqueosDes"
            };
            ListItemResponse objListItemResponse = Claro.Web.Logging.ExecuteMethod<ListItemResponse>(() => { return _oServiceCommon.GetListValueXML(objListItemRequest); });

            objListItemResponse.lstListItem.ToList().ForEach(item =>
            {
                if (Bloq == item.Code && blRespuesta != true)
                {
                    result[1] = item.Description;
                    blRespuesta = true;
                }
            });

            result[0] = false.ToString();
            return Json(new { data = result });

        }
        public JsonResult GetDateActual(string dateBill)
        {
            DateTime dtmFechaHoy;
            string fechaCicloFactMenorUno;
            string fechaCicloFactMenorUnoTemp;
            DateTime fechaAgregaMes;
            dtmFechaHoy = DateTime.Now;
            if (Convert.ToInt(dtmFechaHoy.Day) >= Convert.ToInt(dateBill))
            {
                fechaAgregaMes = dtmFechaHoy.AddMonths(1);
                fechaCicloFactMenorUnoTemp = dateBill + "/" + fechaAgregaMes.Month.ToString() + "/" + fechaAgregaMes.Year.ToString();
                fechaAgregaMes = DateTime.Parse(fechaCicloFactMenorUnoTemp);
                fechaCicloFactMenorUno = fechaAgregaMes.AddDays(-1).ToShortDateString();
            }
            else if (Convert.ToInt(dtmFechaHoy.Day) < Convert.ToInt(dateBill))
            {
                fechaCicloFactMenorUnoTemp = dateBill + "/" + dtmFechaHoy.Month.ToString() + "/" + dtmFechaHoy.Year.ToString();
                fechaAgregaMes = DateTime.Parse(fechaCicloFactMenorUnoTemp);
                fechaAgregaMes = fechaAgregaMes.AddMonths(1);
                fechaCicloFactMenorUno = fechaAgregaMes.AddDays(-1).ToShortDateString();
            }
            else
            {
                fechaCicloFactMenorUnoTemp = dateBill + "/" + dtmFechaHoy.Month.ToString() + "/" + dtmFechaHoy.Year.ToString();
                fechaAgregaMes = DateTime.Parse(fechaCicloFactMenorUnoTemp);
                fechaCicloFactMenorUno = fechaAgregaMes.AddDays(-1).ToShortDateString();
            }

            return Json(new { data = fechaCicloFactMenorUno });
        }
        public JsonResult GetNewPlans(string strIdSession, string CategoriaProducto, string MigracionPlan, string PlanActual, string CodPlanTarifario, string ValorTipoProducto)
        {
            PostTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostTransacService.AuditRequest>(strIdSession);
            
            PostTransacService.NewPlanRequestTransactions objRequest= new PostTransacService.NewPlanRequestTransactions()
            {
                audit = audit,
                CategoriaProducto=CategoriaProducto,
                CodPlanTarifario=Convert.ToInt(CodPlanTarifario),
                MigracionPlan= MigracionPlan,
                PlanActual = PlanActual,
                ValorTipoProducto= ValorTipoProducto
            };

            PostTransacService.NewPlanResponseTransactions objResponse = Claro.Web.Logging.ExecuteMethod<PostTransacService.NewPlanResponseTransactions>(
                () =>
                {
                    return oPostServiceT.GetNewPlan(objRequest);
                });
            return Json(new { data = objResponse });
        }
        public JsonResult GetFixedCostBasePlan(string strIdSession, string CodigoProduct, string IdProduct, string CategoriaProducto, string DescriptionPlan)
        {
            PostTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostTransacService.AuditRequest>(strIdSession);

            PostTransacService.FixedCostBasePlanRequestTransactions objRequest = new PostTransacService.FixedCostBasePlanRequestTransactions()
            {
                audit = audit,
                CodigoProduct=CodigoProduct,
                IdProduct=IdProduct,
                CategoriaProducto = CategoriaProducto,
                DescriptionPlan=DescriptionPlan
            };

            PostTransacService.FixedCostBasePlanResponseTransactions objResponse = Claro.Web.Logging.ExecuteMethod<PostTransacService.FixedCostBasePlanResponseTransactions>(
                () =>
                {
                    return oPostServiceT.GetFixedCostBasePlan(objRequest);
                });
            return Json(new { data = objResponse });
        }
        public JsonResult GetConfigBamTFI(string strIdSession)
        {
            string[] llaves = new string[3];
            llaves[0] = KEY.AppSettings("TipoClienteAplica");
            llaves[1] = KEY.AppSettings("CodPlanesTarifarioBAM");
            llaves[2] = KEY.AppSettings("CodPlanesTarifarioTFI");
            return Json(new { data = llaves });
        }
        public JsonResult GetPlansMigrations(string strIdSession, string MigracionPlan, string PlanActual, string ValorTipoProducto, string CategoriaProducto, string Modalidad, string Familia)
        {
            PostTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostTransacService.AuditRequest>(strIdSession);

            PostTransacService.MigrationPlanRequestTransactions objRequest = new PostTransacService.MigrationPlanRequestTransactions()
            {
                audit = audit,
              ValorTipoProducto=ValorTipoProducto,
              CategoriaProducto=CategoriaProducto,
              Modalidad=Modalidad,
              Familia=Familia,
              MigracionPlan=MigracionPlan,
              PlanActual=PlanActual
            };


            PostTransacService.MigrationPlanResponseTransactions objResponse = Claro.Web.Logging.ExecuteMethod<PostTransacService.MigrationPlanResponseTransactions>(
                () =>
                {
                    return oPostServiceT.GetPlansMigrations(objRequest);
                });



            return Json(new { data = objResponse });
        }
        public JsonResult GetValidationMigration(string strIdSession, string phoneNumber, string contract, string pry)
        {
            PostTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostTransacService.AuditRequest>(strIdSession);

            PostTransacService.ValidationProgDeudaBloqSuspRequest objRequest = new PostTransacService.ValidationProgDeudaBloqSuspRequest()
            {
                audit = audit,
                Telefono = phoneNumber,
                Contrato = contract,
                PRY=pry
            };

            PostTransacService.ValidationProgDeudaBloqSuspResponse objResponse = Claro.Web.Logging.ExecuteMethod<PostTransacService.ValidationProgDeudaBloqSuspResponse>(
                () =>
                {
                    return oPostServiceT.GetValidationMigration(objRequest);
                });

            return Json(new { data = objResponse });
        }
        public JsonResult GetServByTransCodeProductResponse(string strIdSession, string CodProducto)
        {
            PostTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostTransacService.AuditRequest>(strIdSession);
            PostTransacService.ServByTransCodeProductResponse objResponse = null;
            PostTransacService.ServByTransCodeProductRequest objRequest = new PostTransacService.ServByTransCodeProductRequest()
            {
                audit=audit,
                CodProducto =CodProducto
            };
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<PostTransacService.ServByTransCodeProductResponse>(() =>
                    { return oPostServiceT.GetServByTransCodeProductResponse(objRequest); });
            }
            catch (Exception ex)
            { Claro.Web.Logging.Error(strIdSession, audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }

            return Json(new { data = objResponse });
        }
        public JsonResult GetPlansServices(string strIdSession, string TMCODE)
        {
            PostTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostTransacService.AuditRequest>(strIdSession);
            PostTransacService.MaintenancePlanResponseTransactions objResponse = null;
            PostTransacService.MaintenancePlanRequestTransactions objRequest = new PostTransacService.MaintenancePlanRequestTransactions()
            {
                audit = audit,
                Tmcode=Convert.ToInt(TMCODE),
            };
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<PostTransacService.MaintenancePlanResponseTransactions>(() =>
                { return oPostServiceT.GetPlansServices(objRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }

            return Json(new { data = objResponse });
        }
        public JsonResult SavePlanMigration(string strIdSession, PlanMigrationTransac objHiddenControl, DataCustomerModel objDatacustomer)
        {
            bool blnPlanPlus = false;
            string strArrayCodProd = string.Empty;
            string NroCuenta = string.Empty;
            string strMensaje = string.Empty;
            for (int i = 0; i < KEY.AppSettings("CodConexionPlus").Split(';').Length-1; i++)
            {
                if (Convert.ToString(KEY.AppSettings("CodConexionPlus").Split(';')[i]) == Convert.ToString(objHiddenControl.hidCodProd.Split(';')[0])
                    || Convert.ToString(KEY.AppSettings("CodConexionPlus").Split(';')[i]) == Convert.ToString(objHiddenControl.hidCodProd.Split(';')[1]))
                {
                    blnPlanPlus = true;
                    break;
                }
            }

            if (blnPlanPlus)
            {
                bool result = ValidateBagShare(strIdSession, objDatacustomer.ContractID, out strMensaje, out NroCuenta, out strArrayCodProd);
                if (result)
                {
                    if (Convert.ToInt(NroCuenta) > Convert.ToInt(ConsTRans.Constants.numeroUno))
                    {
                        bool result2 = ValidateBagShare2(strIdSession, objDatacustomer.Account, strArrayCodProd, out strMensaje);
                        if (result2)
                        {
                            ExecuteProgramation(strIdSession, objHiddenControl, objDatacustomer);
                        }
                        else
                        {
                            //logica para mostrar este alert y terminar proceso
                            return Json(new { data = strMensaje });
                        }
                    }
                    else if (Convert.ToInt(NroCuenta) == Convert.ToInt(ConsTRans.Constants.numeroUno))
                    {
                        ExecuteProgramation(strIdSession, objHiddenControl, objDatacustomer);
                    }
                }
                else
                {
                    //logica para mostrar este alert y terminar proceso
                    return Json(new { data = strMensaje });  
                }
            }
            else
            {
                ExecuteProgramation(strIdSession, objHiddenControl, objDatacustomer);
            }
            return Json(new { data = "" });
        }   
        public bool ValidateBagShare(string strIdSession, string contrato, out string strMensaje,out string strNroCuenta, out string strArrayCodProd)
        {
            string strResultValid = KEY.AppSettings("strMsjErrorBolsas");
            bool ResultReturn = false;
            strNroCuenta = string.Empty;
            strMensaje = string.Empty;
            strArrayCodProd = string.Empty;
             PostTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostTransacService.AuditRequest>(strIdSession);
             PostTransacService.ValidateBagShareResponseTransactions objResponse = null;
             PostTransacService.ValidateBagShareRequestTransactions objRequest = new PostTransacService.ValidateBagShareRequestTransactions()
            {
                audit = audit,
                Contrato = contrato
            };
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<PostTransacService.ValidateBagShareResponseTransactions>(() =>
                { return oPostServiceT.GetValidateBagShare(objRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }

            if (objResponse!=null)
            {
                strNroCuenta = objResponse.NroCuenta;
                if (objResponse.RPT.Trim().ToUpper() == ConsTRans.Constants.gstrVariableS)
                {
                    strMensaje=strResultValid;
                    ResultReturn = false;
                }
                else if (objResponse.RPT.Trim().ToUpper() == ConsTRans.Constants.gstrVariableN)
	                {
                        ResultReturn = true;
                        foreach (var item in objResponse.lstListItem)
	                    {
		                    strArrayCodProd += item.Code+"|";
	                    }
	                }
            }

            return ResultReturn;
        }
        public bool ValidateBagShare2(string strIdSession,string Cuenta, string strArrayCodProd,out string Mensaje)
        {
            string strResultValid = KEY.AppSettings("strMsjErrorBolsas");
            string strESTADO = ConsTRans.Constants.strUno;
            string strServicio= KEY.AppSettings("ParamPROGRAMACIONSERV");
            string strERRORCODE = string.Empty;
            string strERRORMSG= string.Empty;
            Mensaje = string.Empty;
            bool blnReturnBolsa = false;
            PostTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostTransacService.AuditRequest>(strIdSession);
            PostTransacService.ValidateProgByProductResponseTransactions objResponse = null;
            PostTransacService.ValidateProgByProductRequestTransactions objRequest = new PostTransacService.ValidateProgByProductRequestTransactions()
            {
                audit = audit,
                Cuenta = Cuenta,
                ArrayCodProd = strArrayCodProd
            };
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<PostTransacService.ValidateProgByProductResponseTransactions>(() =>
                { return oPostServiceT.GetValidateProgByProduct(objRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }
            if (objResponse!=null)
            {
                if (objResponse.Result)
                {
                    if (objResponse.ErrorCode.Trim()== ConsTRans.Constants.strCero)
                    {
                        Mensaje = strResultValid;
                        blnReturnBolsa = false;   
                    }
                    else if (objResponse.ErrorCode.Trim()== ConsTRans.Constants.strUno)
                    {
                        blnReturnBolsa = true;   
                    }
                }
            }
            else
            {
                blnReturnBolsa = false;
            }
            return blnReturnBolsa;
        }
        public bool ExecuteProgramation(string strIdSession, PlanMigrationTransac objHiddenControl, DataCustomerModel objDatacustomer)
        {
            string strInteractionId = string.Empty, strMessage = string.Empty; ;
            string strFlagCamp = KEY.AppSettings("strFlagCampMigraPlan");
            string strMensajeNotas;
            InsertTemplateInteraction objTemplateInteractionModel = GetInfoInteractionTemplate(objHiddenControl, objDatacustomer);
            
            SaveInteraction(objTemplateInteractionModel, strIdSession, objHiddenControl, objDatacustomer, out strInteractionId, out   strMessage);

            PostTransacService.ExecuteMigrationPlanRequestTransactions objExeMigrationPlan = new PostTransacService.ExecuteMigrationPlanRequestTransactions();

            PostTransacService.AuditRequest audit;
            CommonTransacService.AuditRequest auditUXI29;
            CommonTransacService.UpdateXInter29ResponseCommon ObjUpdateXInter29Response;
            CommonTransacService.UpdateXInter29RequestCommon ObjUpdateXInter29Request;
            PostTransacService.RegisterPlanResponseTransactions objRegisterPlanResponse;
            PostTransacService.RegisterPlanRequestTransactions objRegisterPlanRequest;
            if (strMessage.Equals(string.Empty))
            {
                if (objHiddenControl.hidTipoPlan!=string.Empty)
                {
                    if (Convert.ToInt(objHiddenControl.hidTipoPlan)== Convert.ToInt(KEY.AppSettings("strIdTipoProd3")))
                    {
                        if (objHiddenControl.hidObtenerDatosGrilla != string.Empty)
                        {
                            audit= App_Code.Common.CreateAuditRequest<PostTransacService.AuditRequest>(strIdSession);
                            for (int i = 0; i < objHiddenControl.hidObtenerDatosGrilla.Split('|').Length-2; i++)
                            {
                                objRegisterPlanRequest = new PostTransacService.RegisterPlanRequestTransactions()
                                            {
                                                audit = audit,
                                                ID_INTERACCION= strInteractionId,
                                                COD_SERVICIO = objHiddenControl.hidObtenerDatosGrilla.Split('|')[i].Split(';')[0],
                                                DES_SERVICIO = objHiddenControl.hidObtenerDatosGrilla.Split('|')[i].Split(';')[0],
                                                MOTIVO_EXCLUYE = objHiddenControl.hidObtenerDatosGrilla.Split('|')[i].Split(';')[0],
                                                CARGO_FIJO = objHiddenControl.hidObtenerDatosGrilla.Split('|')[i].Split(';')[0],
                                                PERIODO = objHiddenControl.hidObtenerDatosGrilla.Split('|')[i].Split(';')[0],
                                                USUARIO = objHiddenControl.User
                                            };
                                objRegisterPlanResponse = Claro.Web.Logging.ExecuteMethod<PostTransacService.RegisterPlanResponseTransactions>(() =>
                                { return oPostServiceT.RegisterPlanService(objRegisterPlanRequest); });
                                if (objRegisterPlanResponse.CodRegServicioPlan.Trim().ToUpper()!=KEY.AppSettings("strMsgOK").Trim().ToUpper())
                                {
                                    try
                                    {
                                        strMensajeNotas = KEY.AppSettings("strMensajeErrorparaNotasClfy");
                                        auditUXI29 = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
                                        ObjUpdateXInter29Request = new UpdateXInter29RequestCommon() {
                                            IdInteract = strInteractionId,
                                            Text= objHiddenControl.txtNota,
                                            Order = ConsTRans.Constants.strLetraI
                                        };
                                        ObjUpdateXInter29Response = Claro.Web.Logging.ExecuteMethod<CommonTransacService.UpdateXInter29ResponseCommon>(() =>
                                        { return _oServiceCommon.UpdateXInter29(ObjUpdateXInter29Request); });
                                    }
                                    catch (Exception ex)
                                    {
                                        Claro.Web.Logging.Error(strIdSession, audit.transaction, ex.Message);
                                        throw new Exception(ex.Message);
                                    }
                                }
                            }
                        }   
                    }
                }

                objExeMigrationPlan.Msisdn = objHiddenControl.hidTelefono;
                objExeMigrationPlan.CoId = objDatacustomer.ContractID;
                objExeMigrationPlan.CustomerId = objDatacustomer.CustomerID;
                objExeMigrationPlan.Cuenta = objDatacustomer.Account;
                if (objHiddenControl.hidConsumerControl.Equals(ConsTRans.Constants.gstrVariableSI))
                {
                    objExeMigrationPlan.Escenario = KEY.AppSettings("strEscenarioPlanesCombos").Split('|')[0];
                }
                else
                {
                    if (objDatacustomer.CodCustomerType.Equals(KEY.AppSettings("strCodTipoCli").Split('|')[1]))
                    {
                        objExeMigrationPlan.Escenario = KEY.AppSettings("strEscenarioPlanesCombos").Split('|')[1];
                    }
                }
                if (objDatacustomer.CodCustomerType.Equals(KEY.AppSettings("strCodTipoCli").Split('|')[4]))
                {
                    objExeMigrationPlan.Escenario = KEY.AppSettings("strEscenarioPlanesCombos").Split('|')[2];
                }
                else
                {
                    if (objDatacustomer.CodCustomerType.Equals(KEY.AppSettings("strCodTipoCli").Split('|')[0]))
                    {
                        objExeMigrationPlan.Escenario = KEY.AppSettings("strEscenarioPlanesCombos").Split('|')[3];
                    }
                }

            }



            return true;

        }
        public bool SaveInteraction(InsertTemplateInteraction objTemplateInteractionModel, string strIdSession, PlanMigrationTransac objHiddenControl, DataCustomerModel objDatacustomer, out string idInteract, out string strMessage) 
        {
            Iteraction objInteractionModel = DatInteraction(objHiddenControl, objDatacustomer);
            string strUSUARIO_APLICACION = KEY.AppSettings("strUsuarioAplicacionWSConsultaPrepago");
            string strPASSWORD_USUARIO = KEY.AppSettings("strPasswordAplicacionWSConsultaPrepago");

            CommonTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
            InsertGeneralRequest objRequest = new InsertGeneralRequest()
            {
                Interaction = objInteractionModel,
                InteractionTemplate = objTemplateInteractionModel,
                vNroTelefono = objDatacustomer.Telephone,
                vUSUARIO_SISTEMA = objHiddenControl.User,
                vUSUARIO_APLICACION = strUSUARIO_APLICACION,
                vPASSWORD_USUARIO = strPASSWORD_USUARIO,
                vEjecutarTransaccion = true,
                audit = audit
            };

            InsertGeneralResponse resultInteraction = GetInsertInteractionBusiness(objRequest);
            idInteract = resultInteraction.rInteraccionId;
            strMessage = string.Empty;
            
            if (resultInteraction.rFlagInsercion.ToUpper()!=ConsTRans.Constants.CriterioMensajeOK && resultInteraction.rFlagInsercion!=string.Empty)
            {
                strMessage = "Error al crear Interacción: " + resultInteraction.rMsgText;
                return false;
            }
            if (resultInteraction.rFlagInsercionInteraccion.ToUpper() != ConsTRans.Constants.CriterioMensajeOK && resultInteraction.rFlagInsercionInteraccion != string.Empty)
            {
                strMessage = "Se creó la interaccion pero existe error en la transacción, el numero insertado es: " + resultInteraction.rInteraccionId + " por el siguiente error: " + resultInteraction.rMsgTextInteraccion + resultInteraction.rMsgText;
                return false;
            }

            return true;
        }
        private InsertGeneralResponse GetInsertInteractionBusiness(InsertGeneralRequest objRequest)
        {
            InsertGeneralResponse objResponse = Claro.Web.Logging.ExecuteMethod<CommonTransacService.InsertGeneralResponse>(() =>
            {
                return _oServiceCommon.GetinsertInteractionGeneral(objRequest);
            });
            return objResponse;
        }
        public InsertTemplateInteraction GetInfoInteractionTemplate(PlanMigrationTransac objHiddenControl, DataCustomerModel objDatacustomer)
        {
            InsertTemplateInteraction objTemplateInteractionModel = new InsertTemplateInteraction();
            string strFormaPago = string.Empty;

            if (objHiddenControl.hidFormaPago.Equals(ConsTRans.Constants.strLetraD))
            {
                strFormaPago = KEY.AppSettings("gConstCargaDesNotaDebito");
            }
            else
            {
                strFormaPago = string.Empty;
            }
            if (objHiddenControl.hidConsumerControl.Equals(ConsTRans.Constants.blcasosVariableSI))
            {
                objTemplateInteractionModel._X_MODEL = KEY.AppSettings("strDescripEscenarios").Split('|')[0];
            }
            else
            {
                if (objDatacustomer.CustomerTypeCode.Equals(KEY.AppSettings("strCodTipoCli").Split('|')[1]))
                {
                    objTemplateInteractionModel._X_MODEL = KEY.AppSettings("strDescripEscenarios").Split('|')[1];
                }
            }
            if (objDatacustomer.CustomerTypeCode.Equals(KEY.AppSettings("strCodTipoCli").Split('|')[4]))
            {
                    objTemplateInteractionModel._X_MODEL = KEY.AppSettings("strDescripEscenarios").Split('|')[2];
            }
            else if (objDatacustomer.CustomerTypeCode.Equals(KEY.AppSettings("strCodTipoCli").Split('|')[0]))
            {
                    objTemplateInteractionModel._X_MODEL = KEY.AppSettings("strDescripEscenarios").Split('|')[3];
            }
            objTemplateInteractionModel._X_EMAIL = objHiddenControl.hidDescripProducto;

            if (objHiddenControl.hidTipoPlan== ConsTRans.Constants.strUno)
            {
                objTemplateInteractionModel._X_INTER_16 = objHiddenControl.hidValorTotalNuevoPlan;
            }
            else
            {
                objTemplateInteractionModel._X_INTER_16 = objHiddenControl.hidValorTotalNuevoPlan2;
            }

            objTemplateInteractionModel._X_EXPIRE_DATE= DateTime.Parse(objHiddenControl.Post_txtFechaAplicacion);
            objTemplateInteractionModel._X_INTER_19 = objHiddenControl.txt_CobroApadece_Post;
            objTemplateInteractionModel._X_INTER_20 =objHiddenControl.chkFidelizaPenalidad;
            objTemplateInteractionModel._X_INTER_21 =objHiddenControl.hidTotalApadece==string.Empty?((int)ConsTRans.Modo.Valor_Minimo).ToString():objHiddenControl.hidTotalApadece;
            objTemplateInteractionModel._X_INTER_1 = objHiddenControl.hidFormaPagoApadece;
            objTemplateInteractionModel._X_INTER_2 = objHiddenControl.txt_Post_PenalidadPCS;
            objTemplateInteractionModel._X_INTER_3 = objHiddenControl.chkFidelizaPenalidadPCS;
            objTemplateInteractionModel._X_INTER_4 = objHiddenControl.hidTotalPCS==""?((int)ConsTRans.Modo.Valor_Minimo).ToString():objHiddenControl.hidTotalPCS;
            objTemplateInteractionModel._X_INTER_5 = strFormaPago;
            objTemplateInteractionModel._X_INTER_15 =objHiddenControl.cbo_PostMigrationCacDac;
            objTemplateInteractionModel._X_OLD_CLAROLOCAL1=objHiddenControl.txt_Post_TotalPenalidadCobrar==string.Empty?((int)ConsTRans.Modo.Valor_Minimo).ToString():objHiddenControl.txt_Post_TotalPenalidadCobrar;
            objTemplateInteractionModel._X_OLD_CLAROLOCAL2= objHiddenControl.txt_Post_TotalFidelizacionPenalidadPCS==string.Empty?((int)ConsTRans.Modo.Valor_Minimo).ToString():objHiddenControl.txt_Post_TotalFidelizacionPenalidadPCS;
            objTemplateInteractionModel._X_OLD_CLAROLOCAL3=objHiddenControl.txt_Post_NumeroBoleta;
            objTemplateInteractionModel._NRO_TELEFONO =objDatacustomer.Telephone;
            objTemplateInteractionModel._X_CLARO_NUMBER = objDatacustomer.Telephone;
            if (objHiddenControl.hidTipoPlan!=string.Empty)
            {
                objTemplateInteractionModel._X_OCCUPATION = objHiddenControl.hidTipoPlan;
            }
            if (objHiddenControl.chkMantenerTopeConsumo== ConsTRans.Constants.strUno)
            {
                objTemplateInteractionModel._X_OPERATION_TYPE = objHiddenControl.lblPost_TopeConsumoActual;
            }
            else
            {
                if (objHiddenControl.rdSinTopeConsumo==ConsTRans.Constants.strUno)
                {
                        if (objHiddenControl.hidFlgAutomatico==ConsTRans.Constants.strCero && objHiddenControl.hidFlgCincoSoles==ConsTRans.Constants.strCero && objHiddenControl.hiFlgAdicionalTope==ConsTRans.Constants.strCero)
                        {
                            objTemplateInteractionModel._X_OPERATION_TYPE = KEY.AppSettings("gStrMsjSinTopesNuevoPlan");
                        }
                        else
                        {
                            objTemplateInteractionModel._X_OPERATION_TYPE = ConsTRans.Constants.Plans_Migrations.gConstIlimitado;
                        }

                }
                else
                {
                    if (objHiddenControl.hidCheckSinCostoFinal == ConsTRans.Constants.strUno)
                    {
                        objTemplateInteractionModel._X_OPERATION_TYPE = objHiddenControl.hidOpTopeConsDesc;
                    }
                    else
                    {
                        if (objHiddenControl.rdConTopeConsumo==ConsTRans.Constants.strUno)
                        {
                            objTemplateInteractionModel._X_OPERATION_TYPE = objHiddenControl.ddlTopesConsumoText;
                        }
                    }
                }
            }
            if (KEY.AppSettings("strValidacionTope")==ConsTRans.Constants.strUno)
            {
                if (objHiddenControl.hidCheckSinCostoFinal == ConsTRans.Constants.strUno && objHiddenControl.ddlTopesConsumoVal == KEY.AppSettings("strCodigoTipoTope"))
                {
                    objTemplateInteractionModel._X_OPERATION_TYPE = objHiddenControl.ddlTopesConsumoText;
                }
            }

            if (objHiddenControl.hidFlgAutomatico == ConsTRans.Constants.strCero && objHiddenControl.hidFlgCincoSoles == ConsTRans.Constants.strCero && objHiddenControl.hiFlgAdicionalTope == ConsTRans.Constants.strCero)
            {
                objTemplateInteractionModel._X_BIRTHDAY = Convert.ToDate(null);
            }
            else
            {
                objTemplateInteractionModel._X_BIRTHDAY = Convert.ToDate(objHiddenControl.Post_txtFechaAplicacion);
            }
            objTemplateInteractionModel._X_DOCUMENT_NUMBER = objHiddenControl.Post_txtFechaAplicacion;
            objTemplateInteractionModel._X_ADDRESS = objHiddenControl.hidCartaInfor;
            if (objHiddenControl.ddlTopesConsumoVal!= string.Empty)
            {
                if (Convert.ToInt(objHiddenControl.ddlTopesConsumoVal) == Convert.ToInt(KEY.AppSettings("OpcTopeConsumo5soles")))
                {
                    objTemplateInteractionModel._X_REFERENCE_PHONE = KEY.AppSettings("MontoConsumo5soles");
                }
                else
                {

                    objTemplateInteractionModel._X_REFERENCE_PHONE =((int)ConsTRans.Modo.Valor_Minimo).ToString();
                }
            }
            if (objHiddenControl.chkMantenerTopeConsumo == ConsTRans.Constants.strUno)
            {
                if (objHiddenControl.hidCodSerActuals.Equals(KEY.AppSettings("OpcTopeConsumo5soles")))
                {
                    objTemplateInteractionModel._X_REFERENCE_PHONE = KEY.AppSettings("MontoConsumo5soles");
                }
            }
            if (objHiddenControl.hidCheckSinCostoFinal == ConsTRans.Constants.strUno)
            {
                if (objHiddenControl.ddlTopesConsumoVal != string.Empty)
                {
                    if (Convert.ToInt(objHiddenControl.ddlTopesConsumoVal) == Convert.ToInt(KEY.AppSettings("OpcTopeConsumo5soles")))
                    {
                      objTemplateInteractionModel._X_REFERENCE_PHONE =  ((int)ConsTRans.Modo.Valor_Minimo).ToString();
                }
                }
            }

            return objTemplateInteractionModel;
        }
        public Iteraction DatInteraction(PlanMigrationTransac objHiddenControl, DataCustomerModel objDatacustomer)
        {
            var objInteractionModel = new Iteraction();

            objInteractionModel.OBJID_CONTACTO = objDatacustomer.ContactCode;
            objInteractionModel.FECHA_CREACION = DateTime.Now.ToString("MM/dd/yyyy");
            objInteractionModel.TELEFONO = ConfigurationManager.AppSettings("gConstKeyCustomerInteract") + "" + objDatacustomer.CustomerID;
            objInteractionModel.TIPO = objHiddenControl.strTipo;
            objInteractionModel.CLASE = objHiddenControl.strClase;
            objInteractionModel.SUBCLASE = objHiddenControl.strSubClase;
            objInteractionModel.TIPO_INTER = ConfigurationManager.AppSettings("AtencionDefault");
            objInteractionModel.METODO = ConfigurationManager.AppSettings("MetodoContactoTelefonoDefault");
            objInteractionModel.RESULTADO = ConfigurationManager.AppSettings("Ninguno");
            objInteractionModel.HECHO_EN_UNO = ConsTRans.Constants.strCero;
            objInteractionModel.NOTAS = objHiddenControl.txtNota;
            objInteractionModel.FLAG_CASO = ConsTRans.Constants.strCero;
            objInteractionModel.USUARIO_PROCESO = ConfigurationManager.AppSettings("USRProcesoSU");
            objInteractionModel.AGENTE = objHiddenControl.User;

            return objInteractionModel;
        }
        public JsonResult GetMessage(string strIdSession)
        {
            ArrayList lstMessage = new ArrayList();
            lstMessage.Add(KEY.AppSettings("gConstTipoLineaActual"));
            lstMessage.Add(KEY.AppSettings("gConstMsjNoEsPostNiFijoPost"));
            lstMessage.Add(KEY.AppSettings("gConstTipoLineaAbrev"));
            lstMessage.Add(KEY.AppSettings("strFlagPlataformaControl"));
            lstMessage.Add(KEY.AppSettings("gStrTransAuditFechProg"));
            lstMessage.Add(KEY.AppSettings("gStrTransAuditChckFideliza"));
            lstMessage.Add(KEY.AppSettings("gStrTransAuditChckSinCosto"));
            lstMessage.Add(KEY.AppSettings("gConstFidelizaPenalidadMigraPlan"));
            lstMessage.Add(KEY.AppSettings("gConstModFechaProgMigraPlan"));
            lstMessage.Add(KEY.AppSettings("gConstDiferenciaMontoCF"));
            lstMessage.Add(KEY.AppSettings("gConstTopeConsSinCostoMigraPlan"));
            lstMessage.Add(KEY.AppSettings("OpcTopeConsumoAdicional"));
            lstMessage.Add(KEY.AppSettings("strCodTipoCli"));
            return Json(lstMessage, JsonRequestBehavior.AllowGet);
        }
    }
}