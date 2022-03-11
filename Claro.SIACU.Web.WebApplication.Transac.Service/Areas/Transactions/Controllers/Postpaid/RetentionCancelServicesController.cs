using Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ConsTRans = Claro.SIACU.Transac.Service;
using Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.CommonServices;
using model = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models;
using KEY = Claro.ConfigurationManager;
using TransCommon = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Controllers.CommonServicesController;
using Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models.Postpaid;
using Claro.SIACU.Web.WebApplication.Transac.Service.PostTransacService;
using CONSTANT = Claro.SIACU.Transac.Service;
using Claro.SIACU.Entity.Transac.Service.Postpaid;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Controllers.Postpaid
{
    public class RetentionCancelServicesController : Controller
    {
        private readonly PostTransacService.PostTransacServiceClient _oServicePostpaid = new PostTransacService.PostTransacServiceClient();
        private readonly CommonTransacService.CommonTransacServiceClient oServiceCommon = new CommonTransacService.CommonTransacServiceClient();
        private readonly FixedTransacService.FixedTransacServiceClient _oServiceFixed = new FixedTransacService.FixedTransacServiceClient();


        private static string cstrTempTrans = ConfigurationManager.AppSettings("strTransRetCanServPost");
        private static string cstrClaseDes = String.Empty;
        private static string cstrClaseId = String.Empty;
        private static string cstrSubClaseDes = String.Empty;
        private static string cstrSubClaseId = String.Empty;
        private static string cstrTipoDes = String.Empty;
        private static string cstrTipoId = String.Empty;
        private static string cstrInteractionCode = String.Empty;

        private static string cstrCoIDMasivo = string.Empty;


        //
        // GET: /Transactions/RetentionCancelServices/
        public ActionResult PostpaidRetentionCancelServices()
        {
            return View();
        }
        public JsonResult LoadPage(Transactions.Models.Postpaid.RetentionCancelServicesModel oModel) // Model.HFC.RetentionCancelServicesModel oModel
        {
            string strFechaFinAcuerdo = string.Empty;
            string strEstadoAcuerdo = string.Empty;
            string strCodeTypification = string.Empty;


            cstrCoIDMasivo = string.Empty;

            PostTransacService.RetentionCancel oResponse = new PostTransacService.RetentionCancel();
            PostTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostTransacService.AuditRequest>(oModel.IdSession);
            try
            {
                if (oModel.ListNumImportar == null || oModel.ListNumImportar == string.Empty)
                {
                    strCodeTypification ="TRANSACCION_GEN_ORD_VISITA_MANT_HFC";  //ConfigurationManager.AppSettings("strTransRetCanServPost");
                    GetTypification(oModel.IdSession, strCodeTypification);
                }





            }
            catch (Exception ex)
            {
                //Claro.Web.Logging.Error(oModel.IdSession, oModel.audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }



            return Json(new { data = oResponse });
        }

        public void GetTypification(string IdSession, string CodeTypification)
        {
            string FlagTipificacion = string.Empty;
            string strMessage = string.Empty;
            string strTempTipoTelef = string.Empty;
            try
            {
                if (cstrTempTrans != "DTH")
                {
                //strTempTipoTelef = 

                }else{
                
                }


                Claro.Web.Logging.Info("IdSession: " + IdSession, "Método :  GetTypification  ", "Inicio" );
                CommonServicesController oCommonControler = new CommonServicesController();

                var tipification = oCommonControler.GetTypificationHFC(IdSession, CodeTypification);
                if (tipification != null)
                {
                    tipification.ToList().ForEach(x =>
                    {
                        cstrTipoDes = x.Type;
                        cstrClaseDes = x.Class;
                        cstrSubClaseDes = x.SubClass;
                        cstrInteractionCode = x.InteractionCode;
                        cstrTipoId = x.TypeCode;
                        cstrClaseId = x.ClassCode;
                        cstrSubClaseId = x.SubClassCode;
                        FlagTipificacion = CONSTANT.Constants.Message_OK;
                    });
                }
                else {
                    FlagTipificacion = CONSTANT.Constants.DAReclamDatosVariableNO_OK;
                    strMessage = CONSTANT.Constants.ADDITIONALSERVICESPOSTPAID.strNotTypification;
                }


            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(IdSession, "Método : GetTypification ", ex.Message);
 
            }               
        
        }

        public PostTransacService.RetentionCancel GetDataAccord(PostTransacService.RetentionCancel oRequest)
        {
            PostTransacService.RetentionCancel oResponse= new PostTransacService.RetentionCancel();
            try 
	        {

                oResponse = Claro.Web.Logging.ExecuteMethod<PostTransacService.RetentionCancel>(() =>
                {
                    return _oServicePostpaid.GetDataAccord(oRequest);
                });


	        }
	        catch (Exception ex)
	        {
                Claro.Web.Logging.Error("IdSession" + oRequest.audit.Session, "Message Error : ", ex.Message);
                
	        }

            return oResponse;
        }

        public PostTransacService.RetentionCancel GetLoadStaidTotal(PostTransacService.RetentionCancel oRequest)
        {
            PostTransacService.RetentionCancel oResponse = new PostTransacService.RetentionCancel();
            try
            {

                oResponse = Claro.Web.Logging.ExecuteMethod<PostTransacService.RetentionCancel>(() =>
                {
                    return _oServicePostpaid.GetLoadStaidTotal(oRequest);
                });


            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error("IdSession" + oRequest.audit.Session, "Message Error : ", ex.Message);

            }

            return oResponse;
        }

        public JsonResult GetMotCancelacion(string strIdSession)
        {
            string strTipoLista = string.Empty;
            int intCodSer=0;

            FixedTransacService.RetentionCancelServicesResponse objMotCancelacionesResponse = null;
            FixedTransacService.AuditRequest audit =
                App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);

            cstrTempTrans = cstrTempTrans.Substring(cstrTempTrans.Length-3,3);
            if(cstrTempTrans=="DTH")
            {
                intCodSer =2;
            }else{
                intCodSer =1;
            }
            

            FixedTransacService.RetentionCancelServicesRequest objRequest = new FixedTransacService.RetentionCancelServicesRequest();
            objRequest.vEstado = 1;
            objRequest.vTipoLista = intCodSer;
            objRequest.audit = audit;

            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, "Inicio Método : GetMotCancelacion");
            try
            {
                objMotCancelacionesResponse =
                    Claro.Web.Logging.ExecuteMethod<FixedTransacService.RetentionCancelServicesResponse>(() =>
                    {
                        return _oServiceFixed.GetMotCancelacion(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }


            Models.CommonServices objCommonServices = null;

            if (objMotCancelacionesResponse != null && objMotCancelacionesResponse.AccionTypes != null)
            {
                objCommonServices = new Models.CommonServices();
                List<Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.CommonServices.CacDacTypeVM> listCacDacTypes =
                    new List<Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.CommonServices.CacDacTypeVM>();

                foreach (Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService.GenericItem item in objMotCancelacionesResponse.AccionTypes)
                {
                    listCacDacTypes.Add(new Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.CommonServices.CacDacTypeVM()
                    {
                        Code = item.Codigo,
                        Description = item.Descripcion,
                    });
                }
                objCommonServices.CacDacTypes = listCacDacTypes;
            }

            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, "Fín Método : GetMotCancelacion listCacDacTypes = " + objCommonServices.CacDacTypes.Count);
            return Json(new { data = objCommonServices.CacDacTypes });
        }

        public JsonResult GetListarAcciones(string strIdSession, string ServDTH_Movil)
        {
            FixedTransacService.RetentionCancelServicesResponse objlistaAccionesResponse;
            FixedTransacService.AuditRequest audit =
                App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);
            FixedTransacService.RetentionCancelServicesRequest objlistaAccionesRequest =
                new FixedTransacService.RetentionCancelServicesRequest()
                {
                    audit = audit,
                    vNivel = Convert.ToInt(ConfigurationManager.AppSettings("gConstPerfil_AsesorCAC"))
                };

            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, "Inicio Método : GetListarAcciones");
            try
            {
                objlistaAccionesResponse =
                    Claro.Web.Logging.ExecuteMethod<FixedTransacService.RetentionCancelServicesResponse>(() =>
                    {
                        return _oServiceFixed.GetListarAccionesRC(objlistaAccionesRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objlistaAccionesRequest.audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }


            Models.CommonServices objCommonServices = null;

            if (objlistaAccionesResponse != null && objlistaAccionesResponse.AccionTypes != null)
            {
                objCommonServices = new Models.CommonServices();
                List<Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.CommonServices.CacDacTypeVM> listCacDacTypes =
                    new List<Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.CommonServices.CacDacTypeVM>();

                Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.CommonServices.CacDacTypeVM oCacDacTypeVM =
                    new Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.CommonServices.CacDacTypeVM();

                if (ServDTH_Movil.Equals("0"))
                {
                     
                    foreach (Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService.GenericItem item in objlistaAccionesResponse.AccionTypes)
                    {

                       oCacDacTypeVM = new Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.CommonServices.CacDacTypeVM();

                            oCacDacTypeVM.Code = item.Codigo;
                            oCacDacTypeVM.Description = item.Descripcion;
                            listCacDacTypes.Add(oCacDacTypeVM);

                    }
                
                }else{

                    foreach (Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService.GenericItem item in objlistaAccionesResponse.AccionTypes)
                    {

                        oCacDacTypeVM = new Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.CommonServices.CacDacTypeVM();

                            oCacDacTypeVM.Code = item.Codigo;
                            oCacDacTypeVM.Description = item.Descripcion;
                            listCacDacTypes.Add(oCacDacTypeVM);

                    }
                }




                objCommonServices.CacDacTypes = listCacDacTypes;
            }

            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, "Fín Método : GetListarAcciones - Total Registros : " + objCommonServices.CacDacTypes.Count);
            return Json(new { data = objCommonServices.CacDacTypes });
        }


	}
}