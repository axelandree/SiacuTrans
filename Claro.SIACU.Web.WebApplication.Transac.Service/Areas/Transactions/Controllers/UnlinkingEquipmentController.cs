using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models;
using CommonTransacService = Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService;
using PostTransacService = Claro.SIACU.Web.WebApplication.Transac.Service.PostTransacService;
using Claro.SIACU.Transac.Service;
using Claro.SIACU.Entity.Transac.Service.Postpaid;
using KEY = Claro.ConfigurationManager;
using COMMON = Claro.SIACU.Entity.Transac.Service.Common;
using Newtonsoft.Json;





namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Controllers
{
    public class UnlinkingEquipmentController : Controller
    {
        CommonTransacService.CommonTransacServiceClient objCommonTransacService = new CommonTransacService.CommonTransacServiceClient();
        PostTransacService.PostTransacServiceClient objPostTransacService = new PostTransacService.PostTransacServiceClient();

        public ActionResult UnlinkingEquipmentMovil()
        {
            ViewData["keyTranEquipoDesvinculacionPre"] = KEY.AppSettings("strTranEquipoDesvinculacionPre");
            ViewData["keyTransTipoCACCallcenter"] = KEY.AppSettings("tipoCACCallCenter");
            ViewData["keyTranEquipoDesvinculacionPost"] = KEY.AppSettings("strTranEquipoDesvinculacionPost");
             Models.BiometryKeys BioKeys = new Model.BiometryKeys();
             try
             {
                 BioKeys = new Model.BiometryKeys()
                 {
                     strTipoDocumentos = KEY.AppSettings("strTipoDocumentos"),
                     strFlagPermisoBiometria = KEY.AppSettings("strConstFlagSiTienePermisosBiometria")
                 };
             }
             catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
             return View(BioKeys);
        }
        public JsonResult UnlinkingEquipmentJson(string nameTransaction, string nameCac, Model.SiactRegistroDesvModel model)
        {
            CommonTransacService.AuditRequest objAuditRequest = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(model.idSession);
            CommonTransacService.InsertEquipmentForeignRequest objRequest = new CommonTransacService.InsertEquipmentForeignRequest
            {
                audit = objAuditRequest,
                item = new CommonTransacService.EquipmentForeignInsert()
                {
                    REDEN_CUSTOMERID = model.customerId,
                    REDEV_NUMERO_IMEI = model.imei,
                    REDEV_NUMERO_IMEI_FISICO = model.imeiFisico,
                    REDEV_NUMEROLINEA = model.customerTelephone,
                    REDEV_ESTADO = KEY.AppSettings("redeEstadoDesvinculado"),
                    REDEV_MARCA_MODELO = model.markModel,
                    REDEV_USUARIOCREA = model.userAccesslogin,
                    MAXIMO = Convert.ToInt(KEY.AppSettings("redeMaximoDesvinculado"))
                },
                nameTransaction = nameTransaction,
                nameCac = nameCac,
                typeCac = model.typeCac,
                userAccesslogin = model.userAccesslogin,
                firstName = model.firstName,
                lastName = model.lastName,
                customerTelephone = model.customerTelephone,
                customerFullName = model.customerFullName,
                customerName = model.customerName,
                customerLastName = model.customerLastName,
                customerNumberDocument = model.customerNumberDocument,
                documentTypeText = model.documentTypeText,
                documentNumber = model.documentNumber,
                imei = model.imei,
                imeiFisico = model.imeiFisico,
                markModel = model.markModel,
                codeadviser = model.codeadviser,
                adviser = model.adviser,
                flagFirmaDigital = model.flagFirmaDigital,
                notes= model.notes,
                area = model.area,
                referencePhone=model.referencePhone,
                tipoPersona = model.tipoPersona,
                parient = model.parient,
                customerLegalAgent = model.customerLegalAgent,
                customerNumberDocumentRRLL = model.customerNumberDocumentRRLL,
                listLegalAgent = getListLegalAgent(model.idSession, model.listLegalAgent),
                customerId = model.customerId,
                strStatusLinea = model.strStatusLinea,
                strHuellaMinucia = model.strHuellaMinucia,
                strHuellaEncode = model.strHuellaEncode,
            };

            CommonTransacService.InsertEquipmentForeignResponse ObjResponse = new CommonTransacService.InsertEquipmentForeignResponse();
            try
            {
                ObjResponse = objCommonTransacService.GetInsertEquipmentForeign(objRequest);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objAuditRequest.Session, objAuditRequest.transaction, ex.Message);
            }

            return Json(new { data = ObjResponse });
        }
        public string getListLegalAgent(string idSession, string listLegalAgent)
        {
            if (string.IsNullOrEmpty(listLegalAgent))
            {
                return String.Empty;
            }
            else
            {
                string strCadena = String.Empty;
                try
                {
                    List<TypeApplicant> lstCant = JsonConvert.DeserializeObject<List<TypeApplicant>>(listLegalAgent);
                    if (lstCant.Count == 0)
                    {
                        return String.Empty;
                    }

                    for (int i = 0; (i <= (lstCant.Count - 1)); i++)
                    {
                        strCadena = (strCadena + (lstCant[i].Nombres + ","));
                        strCadena = (strCadena + (lstCant[i].Apellidos + ","));
                        strCadena = (strCadena + (lstCant[i].TipoDocumento + ","));
                        strCadena = (strCadena + (lstCant[i].NroDocumento + ","));
                        strCadena = (strCadena + (lstCant[i].TipoSolicitante + "|"));
                    }

                    strCadena = strCadena.Substring(0, (strCadena.Length - 1));
                }
                catch (Exception ex)
                {
                    strCadena = String.Empty;
                    Claro.Web.Logging.Info("Session: " + idSession, "RegisterEquipmentJson=> getListLegalAgent  :", string.Format("Se encontró un error al dar formato al registro de Carta Poder/Rep Legal: {0}", ex.Message));
                }

                return strCadena;
            }
        }
    }
}
