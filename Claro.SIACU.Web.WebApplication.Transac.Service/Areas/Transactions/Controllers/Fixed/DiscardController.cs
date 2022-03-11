using Claro.SIACU.Transac.Service;
using Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService;
using Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService;
using CommonService = Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CONSTANT = Claro.SIACU.Transac.Service;
using MODELS = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models;
using AuditRequestFixed = Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService.AuditRequest;
using AuditRequestCommon = Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService.AuditRequest;
using KEY = Claro.ConfigurationManager;
using ConstantsFixed = Claro.SIACU.Transac.Service.Constants;
using HELPERS = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers;
using FIXED = Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService;
using FunctionsSIACU = Claro.SIACU.Transac.Service.Functions;
using Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models.Fixed;
using Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models;
using Claro.SIACU.Web.WebApplication.Transac.Service.App_Code;
using COMMON = Claro.SIACU.Entity.Transac.Service.Common;
using HELPER_ITEM = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.Fixed.DiscardHelper;
using AutoMapper;
using System.Text;
using System.Web;
using System.IO;
using System.Data;
using System.Net;
using System.Net.Security;
using System.Linq;
using System.Security.Principal;
using System.Security.Cryptography.X509Certificates;
using System.Reflection;
//Luis D
using Claro.Web;
using ConstantsHFC = Claro.SIACU.Transac.Service.Constants;
using Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.Fixed.DiscardHelper; //INICIATIVA-871
using Claro.SIACU.ProxyService.Transac.Service.InstantLinkSOA;//INICIATIVA-871
using Claro.SIACU.Entity.Transac.Service.Common.GetInsertInt;
using Claro.SIACU.Entity.Transac.Service.Fixed.Discard;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Controllers.Fixed
{

    public class DiscardController : Controller
    {
        //
        // GET: /Transactions/Discard/
        private readonly FixedTransacServiceClient _oServiceFixed = new FixedTransacServiceClient();
        private readonly CommonTransacServiceClient _oServiceCommon = new CommonTransacServiceClient();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DiscardListPost()
        {           
            return PartialView();
        }

        public ActionResult DiscardListPre()
        {
            return PartialView();
        }

        public WebApplication.Transac.Service.FixedTransacService.HeaderRequest getHeaderRequest(string operation)
        {
            return new WebApplication.Transac.Service.FixedTransacService.HeaderRequest()
            {
                consumer = KEY.AppSettings("consumer"),
                country = KEY.AppSettings("country"),
                dispositivo = KEY.AppSettings("dispositivo"),
                language = KEY.AppSettings("language"),
                modulo = KEY.AppSettings("moduloDiscard"),
                msgType = KEY.AppSettings("msgType"),
                operation = operation,
                pid = DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                system = KEY.AppSettings("system"),
                timestamp = DateTime.Now.ToString("o"),
                userId = App_Code.Common.CurrentUser,
                wsIp = App_Code.Common.GetApplicationIp()
            };
        }

        public JsonResult ConsultDiscardRTI(string strIdSession, WebApplication.Transac.Service.FixedTransacService.ConsultDiscardRTIRequest objReqConsultDiscardRTIRequest, ConsultDiscartRTITOBERequest requestToBe, string plataforma, GenericDiscard objParametrosSesion, string listIdDescarte)
        {
            Claro.Web.Logging.Info(strIdSession, strIdSession, "ConsultDiscardRTI INI");
            Claro.Web.Logging.Info(strIdSession, strIdSession, string.Format("ConsultDiscardRTI requestToBe: {0}", new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(requestToBe)));
            Areas.Transactions.Models.Fixed.DiscardModel objDiscardModel = new Models.Fixed.DiscardModel();
            ConsultDiscardRTIResponse objConsultDiscardRTIResponse = new ConsultDiscardRTIResponse();

            try
            {
                //INI: INICIATIVA-871
                string strTipoLinea = objReqConsultDiscardRTIRequest.MessageRequest.Body.consultarDescartesRtiPrePostRequest.tipoLinea;
                Claro.Web.Logging.Info(strIdSession, strIdSession, string.Format("{0} --> {1}", "[INICIATIVA-871 - ConsultDiscardRTI][strTipoLinea]", strTipoLinea));

                List<string> CodigosDescartesSesion = strTipoLinea == "POSTPAGO" ? KEY.AppSettings("CodDescartesSesionPostpago").Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList() : KEY.AppSettings("CodDescartesSesionPrepago").Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                Claro.Web.Logging.Info(strIdSession, strIdSession, string.Format("{0} --> {1}", "[INICIATIVA-871 - ConsultDiscardRTI][CodigosDescartesSesionPostpago]", KEY.AppSettings("CodDescartesSesionPostpago")));
                Claro.Web.Logging.Info(strIdSession, strIdSession, string.Format("{0} --> {1}", "[INICIATIVA-871 - ConsultDiscardRTI][CodigosDescartesSesionPrepago]", KEY.AppSettings("CodDescartesSesionPrepago")));

                List<string> ListaDescartesSesionPrepago = strTipoLinea == "PREPAGO" ? KEY.AppSettings("ListaDescartesSesionPrepago").Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList() : null;
                Claro.Web.Logging.Info(strIdSession, strIdSession, string.Format("{0} --> {1}", "[INICIATIVA-871 - ConsultDiscardRTI][ListaDescartesSesionPrepago]", KEY.AppSettings("ListaDescartesSesionPrepago")));
                //FIN: INICIATIVA-871

                if (plataforma == Constants.TOBE)
                {
                    objDiscardModel = ConsultDiscardRTITOBE(requestToBe, strIdSession, objParametrosSesion, listIdDescarte);
                }
                else
        {
            
            ConsultDiscardRTIRequest objConsultDiscardRTIRequest = new ConsultDiscardRTIRequest()
            {
                audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest1>(strIdSession),
                MessageRequest = new WebApplication.Transac.Service.FixedTransacService.ConsultDiscardRTIMessageRequest()
                {
                    Header = new WebApplication.Transac.Service.FixedTransacService.ConsultDiscardRTIHeaderRequest()
                    {
                        HeaderRequest = getHeaderRequest("ConsultDiscardRTI")
                    }
                }
            };

            WebApplication.Transac.Service.FixedTransacService.ConsultDiscardRTIBodyRequest objBodyRequest = new WebApplication.Transac.Service.FixedTransacService.ConsultDiscardRTIBodyRequest()
            {
                        consultarDescartesRtiPrePostRequest = new consultarDescartesRtiPrePostRequest()
                        {
                            coId = objReqConsultDiscardRTIRequest.MessageRequest.Body.consultarDescartesRtiPrePostRequest.coId,
                            msisdn = objReqConsultDiscardRTIRequest.MessageRequest.Body.consultarDescartesRtiPrePostRequest.msisdn,
                            tipoLinea = objReqConsultDiscardRTIRequest.MessageRequest.Body.consultarDescartesRtiPrePostRequest.tipoLinea
                        }
            };

            objConsultDiscardRTIRequest.MessageRequest.Body = objBodyRequest;
            objConsultDiscardRTIRequest.audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest1>(strIdSession);

                    if (listIdDescarte.Length > 0)
                    {
                        Claro.Web.Logging.Info(strIdSession, objConsultDiscardRTIRequest.audit.transaction, string.Format("_oServiceFixed.ConsultDiscardRTIActuali Request: {0}", new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(objConsultDiscardRTIRequest)));

                        ConsultDiscardRTIRequestGrupo objConsultDiscardRTIRequestGrupo = new ConsultDiscardRTIRequestGrupo()
                        {
                            audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest1>(strIdSession),
                            MessageRequest = new WebApplication.Transac.Service.FixedTransacService.ConsultDiscardRTIMessageRequestGrupo()
                            {
                                Header = new WebApplication.Transac.Service.FixedTransacService.ConsultDiscardRTIHeaderRequest()
                                {
                                    HeaderRequest = getHeaderRequest("ConsultDiscardRTI")
                                },
                                Body = new ConsultDiscardRTIBodyRequestGrupo()
                                {
                                    consultarDescartesRtiPrePostRequest = new consultarDescartesRtiPrePostRequestGrupo()
                                    {
                                        coId = objReqConsultDiscardRTIRequest.MessageRequest.Body.consultarDescartesRtiPrePostRequest.coId,
                                        msisdn = objReqConsultDiscardRTIRequest.MessageRequest.Body.consultarDescartesRtiPrePostRequest.msisdn,
                                        tipoLinea = objReqConsultDiscardRTIRequest.MessageRequest.Body.consultarDescartesRtiPrePostRequest.tipoLinea,
                                        grupoDescarte = listIdDescarte
                                    }
                                }
                            }
                        };

                        objConsultDiscardRTIResponse = Claro.Web.Logging.ExecuteMethod<WebApplication.Transac.Service.FixedTransacService.ConsultDiscardRTIResponse>(() =>
                        {
                            return _oServiceFixed.ConsultDiscardGrupoRTI(objConsultDiscardRTIRequestGrupo);
                        });
                        Claro.Web.Logging.Info(strIdSession, objConsultDiscardRTIRequest.audit.transaction, string.Format("_oServiceFixed.ConsultDiscardRTIActuali Response: {0}", new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(objConsultDiscardRTIResponse)));
                    }
                    else
                    {
                        Claro.Web.Logging.Info(strIdSession, objConsultDiscardRTIRequest.audit.transaction, string.Format("_oServiceFixed.ConsultDiscardRTI Request: {0}", new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(objConsultDiscardRTIRequest)));
                objConsultDiscardRTIResponse = Claro.Web.Logging.ExecuteMethod<WebApplication.Transac.Service.FixedTransacService.ConsultDiscardRTIResponse>(() =>
                {
                    return _oServiceFixed.ConsultDiscardRTI(objConsultDiscardRTIRequest);
                });
                        Claro.Web.Logging.Info(strIdSession, objConsultDiscardRTIRequest.audit.transaction, string.Format("_oServiceFixed.ConsultDiscardRTI Response: {0}", new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(objConsultDiscardRTIResponse)));
                    }



                if (objConsultDiscardRTIResponse.MessageResponse != null)
                {
                    if (objConsultDiscardRTIResponse.MessageResponse.Body != null)
                    {
                            if (objConsultDiscardRTIResponse.MessageResponse.Body.consultarDescartesRtiPrePostResponse.responseData.grupos != null)
                        {
                            List<HELPER_ITEM.GroupHelper> lstGroupHelper = new List<HELPER_ITEM.GroupHelper>();
                                foreach (WebApplication.Transac.Service.FixedTransacService.Grupos ItemGroup in objConsultDiscardRTIResponse.MessageResponse.Body.consultarDescartesRtiPrePostResponse.responseData.grupos)
                            {

                                lstGroupHelper.Add(new HELPER_ITEM.GroupHelper()
                                   {
                                           degriIdGrupo = ItemGroup.idGrupo,
                                           degrvDescripcion = ItemGroup.descripcion,
                                           degrvTipo = ItemGroup.tipo,
                                           degriFlagVisual = ItemGroup.flagVisual,
                                           degriOrden = ItemGroup.orden,
                                           degrvUsuReg = ItemGroup.usuarioRegistro,
                                           degrdFecReg = ItemGroup.fechaRegistro,
                                           degrvUsuMod = ItemGroup.usuarioModificacion,
                                           degrdFecMod = ItemGroup.fechaModificacion,
                                           degriEstadoReg = ItemGroup.estadoRegistro

                                   });

                            }
                            objDiscardModel.listaGrupos = new List<HELPER_ITEM.GroupHelper>();
                            objDiscardModel.listaGrupos = lstGroupHelper;
                        }

                            if (objConsultDiscardRTIResponse.MessageResponse.Body.consultarDescartesRtiPrePostResponse.responseData.descartes != null)
                        {

                            List<HELPER_ITEM.DiscardHelper> lstDiscardHelper = new List<HELPER_ITEM.DiscardHelper>();
                                foreach (WebApplication.Transac.Service.FixedTransacService.Descartes ItemDiscard1 in objConsultDiscardRTIResponse.MessageResponse.Body.consultarDescartesRtiPrePostResponse.responseData.descartes)
                            {
                                    WebApplication.Transac.Service.FixedTransacService.Descartes ItemDiscard = new WebApplication.Transac.Service.FixedTransacService.Descartes();
                                    ItemDiscard = ItemDiscard1;
                                    Claro.Web.Logging.Info(strIdSession, objConsultDiscardRTIRequest.audit.transaction, string.Format("ConsultDiscardRTI - idDescarte: {0}", ItemDiscard.idDescarte));
                                List<HELPER_ITEM.DiscardListValueHelper> lstDiscardListValueHelper = new List<HELPER_ITEM.DiscardListValueHelper>();

                                if (ItemDiscard.descarteListaValor != null)
                                {
                                        foreach (WebApplication.Transac.Service.FixedTransacService.DescarteListaValor itemE in ItemDiscard.descarteListaValor)
                                    {
                                        lstDiscardListValueHelper.Add(new HELPER_ITEM.DiscardListValueHelper()
                                        {
                                            nombre = itemE.nombre,
                                            valor = itemE.valor,
                                            medida = itemE.medida,
                                            fechaVencimiento = itemE.fechaVencimiento,
                                            esCabecera = itemE.esCabecera
                                        });
                                    }
                                }

                                    //INI: INICIATIVA-871

                                    if (ListaDescartesSesionPrepago != null && ListaDescartesSesionPrepago.Count > 0)
                                    {
                                        if (ListaDescartesSesionPrepago.Contains(ItemDiscard.idDescarte))
                                        {
                                            Claro.Web.Logging.Info(strIdSession, objConsultDiscardRTIRequest.audit.transaction, string.Format("ConsultDiscardRTI - idDescarte {0} se encuentra dentro de ListaDescartesSesionPrepago", ItemDiscard.idDescarte));
                                            lstDiscardListValueHelper = ObtenerListaValorSesionDescarte(strIdSession, ItemDiscard.idDescarte, lstDiscardListValueHelper, objParametrosSesion);
                                        }
                                    }

                                    if (CodigosDescartesSesion != null && CodigosDescartesSesion.Count > 0)
                                    {
                                        if (CodigosDescartesSesion.Contains(ItemDiscard.idDescarte))
                                        {
                                            Claro.Web.Logging.Info(strIdSession, objConsultDiscardRTIRequest.audit.transaction, string.Format("ConsultDiscardRTI - idDescarte {0} se encuentra dentro de CodigosDescartesSesion", ItemDiscard.idDescarte));
                                            ItemDiscard = ObtenerValorSesionDescarteASIS(strIdSession, ItemDiscard.idDescarte, objBodyRequest.consultarDescartesRtiPrePostRequest.msisdn, ItemDiscard, objParametrosSesion);
                                        }
                                    }
                                    //FIN: INICIATIVA-871

                                lstDiscardHelper.Add(new HELPER_ITEM.DiscardHelper()
                                {
                                        id_descarte = ItemDiscard.idDescarte,
                                        nombre_variable = ItemDiscard.nombreVariable,
                                        desc_descarte = ItemDiscard.descripcionDescarte,
                                        tipo_descarte = ItemDiscard.tipoDescarte,
                                        flag_descarte = ItemDiscard.flagDescarte,
                                        orden_descarte = ItemDiscard.ordenDescarte,
                                        fecha_reg = ItemDiscard.fechaRegistro,
                                        id_grupo = ItemDiscard.idGrupo,
                                        flag_OK = ItemDiscard.flagOk,
                                        flag_Error = ItemDiscard.flagError,
                                    descarteValor = ItemDiscard.descarteValor,
                                    descarteListaValor = lstDiscardListValueHelper
                                });

                            }

                            objDiscardModel.listaDescartes = new List<HELPER_ITEM.DiscardHelper>();
                            objDiscardModel.listaDescartes = lstDiscardHelper;
                        }
                    }

                }
            }
            }
            catch (Exception ex)
            {
                objConsultDiscardRTIResponse = null;
                Claro.Web.Logging.Error(strIdSession, objReqConsultDiscardRTIRequest.audit.transaction, ex.Message);
                throw new Claro.MessageException(objReqConsultDiscardRTIRequest.audit.transaction);
            }

            JsonResult json = Json(new { data = objDiscardModel });

            return json;
        }

        private DiscardModel ConsultDiscardRTITOBE(ConsultDiscartRTITOBERequest request, string strIdSession, GenericDiscard objParametrosSesion, string listIdDescarte)
        {
            Claro.Web.Logging.Info(strIdSession, strIdSession, "ConsultDiscardRTITOBE INI");
            Areas.Transactions.Models.Fixed.DiscardModel objDiscardModel;
            AuditRequest1 auditRequest = new AuditRequest1(); 
            auditRequest = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest1>(strIdSession);
            try
            {
                //INI: INICIATIVA-871
                string strTipoLinea = request.consultarDescartesRtiRequest.tipoLinea;
                Claro.Web.Logging.Info(strIdSession, auditRequest.transaction, string.Format("{0} --> {1}", "[INICIATIVA-871 - ConsultDiscardRTI][strTipoLinea]", strTipoLinea));

                List<string> CodigosDescartesSesion = strTipoLinea == "POSTPAGO" ? KEY.AppSettings("CodDescartesSesionPostpago").Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList() : KEY.AppSettings("CodDescartesSesionPrepago").Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                Claro.Web.Logging.Info(strIdSession, auditRequest.transaction, string.Format("{0} --> {1}", "[INICIATIVA-871 - ConsultDiscardRTI][CodigosDescartesSesion]", KEY.AppSettings("CodDescartesSesionPostpago")));
                //FIN: INICIATIVA-871

                objDiscardModel = new Models.Fixed.DiscardModel();
                ConsultDiscartRTITOBEResponse response = new ConsultDiscartRTITOBEResponse();

                if (listIdDescarte.Length > 0)
                {
                    ConsultDiscartRTITOBERequestGrupo request2 = new ConsultDiscartRTITOBERequestGrupo()
                    {
                        consultarDescartesRtiRequest = new ConsultarDescartesRtiRequestGrupo()
                        {
                            coId = request.consultarDescartesRtiRequest.coId,
                            coIdPub = request.consultarDescartesRtiRequest.coIdPub,
                            msisdn = request.consultarDescartesRtiRequest.msisdn,
                            tipoLinea = request.consultarDescartesRtiRequest.tipoLinea,
                            customerId = request.consultarDescartesRtiRequest.customerId,
                            flagConvivencia = request.consultarDescartesRtiRequest.flagConvivencia,
                            grupoDescarte = listIdDescarte
                        }
                    };

                    Claro.Web.Logging.Info(strIdSession, auditRequest.transaction, string.Format("_oServiceFixed.ConsultDiscardRTIToBeActu Request: {0}", new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(request2)));
                    response = _oServiceFixed.ConsultDiscardRTIToBeGrupo(request2, auditRequest);
                    Claro.Web.Logging.Info(strIdSession, auditRequest.transaction, string.Format("_oServiceFixed.ConsultDiscardRTIToBeActu Response: {0}", new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(response)));
                }
                else
                {
                Claro.Web.Logging.Info(strIdSession, auditRequest.transaction, string.Format("_oServiceFixed.ConsultDiscardRTIToBe Request: {0}", new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(request)));
                response = _oServiceFixed.ConsultDiscardRTIToBe(request, auditRequest);
                    Claro.Web.Logging.Info(strIdSession, auditRequest.transaction, string.Format("_oServiceFixed.ConsultDiscardRTIToBe Response: {0}", new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(response)));
                }

                if (response.consultarDescartesRtiResponse.responseAudit.codigoRespuesta == "0" && response.consultarDescartesRtiResponse.responseData != null)
                {
                    if (response.consultarDescartesRtiResponse.responseData.grupos != null)
                    {
                        List<HELPER_ITEM.GroupHelper>  lstGroupHelper = new List<HELPER_ITEM.GroupHelper>();
                        foreach (FixedTransacService.Grupos ItemGroup in response.consultarDescartesRtiResponse.responseData.grupos)
                        {
                            lstGroupHelper.Add(new HELPER_ITEM.GroupHelper()
                           {
                               degriIdGrupo = ItemGroup.idGrupo,
                               degrvDescripcion = ItemGroup.descripcion,
                               degrvTipo = ItemGroup.tipo,
                               degriFlagVisual = ItemGroup.flagVisual,
                               degriOrden = ItemGroup.orden,
                               degrvUsuReg = ItemGroup.usuarioRegistro,
                               degrdFecReg = ItemGroup.fechaRegistro,
                               degrvUsuMod = ItemGroup.usuarioModificacion,
                               degrdFecMod = ItemGroup.fechaModificacion,
                               degriEstadoReg = ItemGroup.estadoRegistro

                           });
                        }
                        objDiscardModel.listaGrupos = new List<HELPER_ITEM.GroupHelper>();
                        objDiscardModel.listaGrupos = lstGroupHelper;
                    }

                    if (response.consultarDescartesRtiResponse.responseData.descartes != null)
                    {
                        List<HELPER_ITEM.DiscardHelper> lstDiscardHelper = new List<HELPER_ITEM.DiscardHelper>();
                        foreach (FixedTransacService.Descartes ItemDiscard1 in response.consultarDescartesRtiResponse.responseData.descartes)
                        {
                            FixedTransacService.Descartes ItemDiscard = new FixedTransacService.Descartes(); //INICIATIVA-871
                            ItemDiscard = ItemDiscard1;
                            List<HELPER_ITEM.DiscardListValueHelper> lstDiscardListValueHelper = new List<HELPER_ITEM.DiscardListValueHelper>();

                            if (ItemDiscard.descarteListaValor != null)
                            {
                                foreach (FixedTransacService.DescarteListaValor itemE in ItemDiscard.descarteListaValor)
                                {
                                    lstDiscardListValueHelper.Add(new HELPER_ITEM.DiscardListValueHelper()
                                    {
                                        nombre = itemE.nombre,
                                        valor = itemE.valor,
                                        medida = itemE.medida,
                                        fechaVencimiento = itemE.fechaVencimiento,
                                        esCabecera = itemE.esCabecera
                                    });
                                }
                            }

                            //INI: INICIATIVA-871
                            if (CodigosDescartesSesion != null && CodigosDescartesSesion.Count > 0)
                            {
                                if (CodigosDescartesSesion.Contains(ItemDiscard.idDescarte))
                                {
                                    Claro.Web.Logging.Info(strIdSession, auditRequest.transaction, string.Format("ConsultDiscardRTI - idDescarte {0} se encuentra dentro de CodigosDescartesSesion", ItemDiscard.idDescarte));
                                    ItemDiscard = ObtenerValorSesionDescarteTOBE(strIdSession, ItemDiscard.idDescarte, request.consultarDescartesRtiRequest.msisdn, ItemDiscard, objParametrosSesion);
                                }
                            }
                            //FIN: INICIATIVA-871

                            lstDiscardHelper.Add(new HELPER_ITEM.DiscardHelper()
                            {
                                id_descarte = ItemDiscard.idDescarte,
                                nombre_variable = ItemDiscard.nombreVariable,
                                desc_descarte = ItemDiscard.descripcionDescarte,
                                tipo_descarte = ItemDiscard.tipoDescarte,
                                flag_descarte = ItemDiscard.flagDescarte,
                                orden_descarte = ItemDiscard.ordenDescarte,
                                fecha_reg = ItemDiscard.fechaRegistro,
                                id_grupo = ItemDiscard.idGrupo,
                                flag_Error = ItemDiscard.flagError,
                                flag_OK = ItemDiscard.flagOk,
                                descarteValor = ItemDiscard.descarteValor,
                                descarteListaValor = lstDiscardListValueHelper
                            });
                        }
                        objDiscardModel.listaDescartes = new List<HELPER_ITEM.DiscardHelper>();
                        objDiscardModel.listaDescartes = lstDiscardHelper;
                    }


                }

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, auditRequest.transaction, ex.Message);
                throw new Claro.MessageException(auditRequest.transaction);
            }

            Claro.Web.Logging.Info(strIdSession, auditRequest.transaction, string.Format("_oServiceFixed.objDiscardModel Response: {0}", new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(objDiscardModel)));
            Claro.Web.Logging.Info(strIdSession, auditRequest.transaction, "ConsultDiscardRTITOBE FIN");

            return objDiscardModel;
        }

        public JsonResult GetKeyConfig(string strIdSession, string strfilterKeyName)
        {
            string value = "";
            value = KEY.AppSettings(strfilterKeyName);
            JsonResult json = Json(new { data = value });
            return json;
        }

        //INI: INICIATIVA-871 : Obtener valores de descartes mediante la Sesion del Aplicativo
        public List<DiscardListValueHelper> ObtenerListaValorSesionDescarte(string strIdSession, string idDescarte, List<DiscardListValueHelper> listaDescarteValorSesion, GenericDiscard objParametrosSesion)
        {
            Claro.Web.Logging.Info(strIdSession, strIdSession, "INICIO INICIATIVA-871 - ObtenerListaValorSesionDescarte");

            try
            {
                if (objParametrosSesion != null)
                {
                    if (objParametrosSesion.ListaDescartesValor != null && objParametrosSesion.ListaDescartesValor.Count > 0)
                    {
                        foreach (var item in objParametrosSesion.ListaDescartesValor)
                        {
                            if (item.idDescarte == idDescarte)
                            {
                                DiscardListValueHelper objListaDescarte = new DiscardListValueHelper();
                                Claro.Web.Logging.Info(strIdSession, strIdSession, string.Format("{0} --> {1}", "[INICIATIVA-871 - ObtenerListaValorSesionDescarte][item.idDescarte]", item.idDescarte));

                                objListaDescarte.nombre = item.nombre;
                                Claro.Web.Logging.Info(strIdSession, strIdSession, string.Format("{0} --> {1}", "[INICIATIVA-871 - ObtenerListaValorSesionDescarte][objListaDescarte.nombre]", objListaDescarte.nombre));

                                objListaDescarte.valor = item.valor;
                                Claro.Web.Logging.Info(strIdSession, strIdSession, string.Format("{0} --> {1}", "[INICIATIVA-871 - ObtenerListaValorSesionDescarte][objListaDescarte.valor]", objListaDescarte.valor));

                                objListaDescarte.medida = item.medida;
                                Claro.Web.Logging.Info(strIdSession, strIdSession, string.Format("{0} --> {1}", "[INICIATIVA-871 - ObtenerListaValorSesionDescarte][objListaDescarte.medida]", objListaDescarte.medida));

                                objListaDescarte.fechaVencimiento = item.fechaVencimiento;
                                Claro.Web.Logging.Info(strIdSession, strIdSession, string.Format("{0} --> {1}", "[INICIATIVA-871 - ObtenerListaValorSesionDescarte][objListaDescarte.fechaVencimiento]", objListaDescarte.fechaVencimiento));

                                objListaDescarte.esCabecera = listaDescarteValorSesion.Count > 0 ? "true" : "false";
                                Claro.Web.Logging.Info(strIdSession, strIdSession, string.Format("{0} --> {1}", "[INICIATIVA-871 - ObtenerListaValorSesionDescarte][objListaDescarte.esCabecera]", objListaDescarte.esCabecera));

                                listaDescarteValorSesion.Add(objListaDescarte);
                            }
                        }
                    }
                    else
                    {
                        Claro.Web.Logging.Info(strIdSession, strIdSession, "[INICIATIVA-871 - ObtenerListaValorSesionDescarte][objParametrosSesion.ListaDescartesValor es nulo]");
                    }
                }
                else
                {
                    Claro.Web.Logging.Info(strIdSession, strIdSession, "[INICIATIVA-871 - ObtenerListaValorSesionDescarte][objParametrosSesion es nulo]");
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strIdSession, string.Format("{0} => [{1}|{2}]", "[INICIATIVA-871 - ObtenerListaValorSesionDescarte][Error]", ex.Message, ex.StackTrace));
            }

            Claro.Web.Logging.Info(strIdSession, strIdSession, "FIN INICIATIVA-871 - ObtenerListaValorSesionDescarte");

            return listaDescarteValorSesion;
        }

        public Descartes ObtenerValorSesionDescarteASIS(string strIdSession, string idDescarte, string strLinea, Descartes objDescarte, GenericDiscard objParametrosSesion)
        {
            Claro.Web.Logging.Info(strIdSession, strIdSession, "INICIO INICIATIVA-871 - ObtenerValorSesionDescarteASIS");

            try
            {
                Claro.Web.Logging.Info(strIdSession, strIdSession, string.Format("{0} --> {1}", "[INICIATIVA-871 - ObtenerValorSesionDescarteASIS][idDescarte]", idDescarte));
                if (idDescarte == KEY.AppSettings("CodDescartePortabilidadPostpago") || idDescarte == KEY.AppSettings("CodDescartePortabilidadPrepago")) // PORTABILIDAD PREPAGO - POSTPAGO
                {
                    objDescarte.descarteValor = !string.IsNullOrEmpty(objParametrosSesion.EstadoPortabilidad) ? objParametrosSesion.EstadoPortabilidad : "NO";
                    objDescarte.flagOk = "1";
                }
                else if (idDescarte == KEY.AppSettings("CodDescarteDesalineacionIMSI")) // Desalineación de IMSI SIAC - UDB - Prepago
                {
                    objDescarte = ValidacionImsiSIAC_HLR(strIdSession, strLinea, objDescarte);
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strIdSession, string.Format("{0} => [{1}|{2}]", "[INICIATIVA-871 - ObtenerValorSesionDescarteASIS][Error]", ex.Message, ex.StackTrace));
                objDescarte.descarteValor = string.Empty;
            }

            Claro.Web.Logging.Info(strIdSession, strIdSession, string.Format("{0} --> {1}", "[INICIATIVA-871 - ObtenerValorSesionDescarteASIS][objDescarte.descarteValor]", objDescarte.descarteValor));
            Claro.Web.Logging.Info(strIdSession, strIdSession, string.Format("{0} --> {1}", "[INICIATIVA-871 - ObtenerValorSesionDescarteASIS][objDescarte.flag_OK]", objDescarte.flagOk));
            Claro.Web.Logging.Info(strIdSession, strIdSession, "FIN INICIATIVA-871 - ObtenerValorSesionDescarteASIS");

            return objDescarte;
        }

        public FixedTransacService.Descartes ObtenerValorSesionDescarteTOBE(string strIdSession, string idDescarte, string strLinea, FixedTransacService.Descartes objDescarte, GenericDiscard objParametrosSesion)
        {
            Claro.Web.Logging.Info(strIdSession, strIdSession, "INICIO INICIATIVA-871 - ObtenerValorSesionDescarteTOBE");

            try
            {
                Claro.Web.Logging.Info(strIdSession, strIdSession, string.Format("{0} --> {1}", "[INICIATIVA-871 - ObtenerValorSesionDescarteTOBE][idDescarte]", idDescarte));

                if (idDescarte == KEY.AppSettings("CodDescartePortabilidadPostpago") || idDescarte == KEY.AppSettings("CodDescartePortabilidadPrepago")) // PORTABILIDAD PREPAGO - POSTPAGO
                {
                    objDescarte.descarteValor = !string.IsNullOrEmpty(objParametrosSesion.EstadoPortabilidad) ? objParametrosSesion.EstadoPortabilidad : "NO";
                    objDescarte.flagOk = "1";
                }

                Claro.Web.Logging.Info(strIdSession, strIdSession, string.Format("{0} --> {1}", "[INICIATIVA-871 - ObtenerValorSesionDescarteTOBE][objDescarte.descarteValor]", objDescarte.descarteValor));
                Claro.Web.Logging.Info(strIdSession, strIdSession, string.Format("{0} --> {1}", "[INICIATIVA-871 - ObtenerValorSesionDescarteTOBE][objDescarte.flagOk]", objDescarte.flagOk));
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strIdSession, string.Format("{0} => [{1}|{2}]", "[INICIATIVA-871 - ObtenerValorSesionDescarteTOBE][Error]", ex.Message, ex.StackTrace));
                objDescarte.descarteValor = string.Empty;
            }

            Claro.Web.Logging.Info(strIdSession, strIdSession, "FIN INICIATIVA-871 - ObtenerValorSesionDescarteTOBE");

            return objDescarte;
        }

        public Descartes ValidacionImsiSIAC_HLR(string strIdSession, string strPhoneNumber, Descartes objDescarte)
        {
            Claro.Web.Logging.Info(strIdSession, strIdSession, "INICIO INICIATIVA-871 - ValidacionImsiSIAC_HLR");
            FIXED.DatosSIMPrepago objResponse = new FIXED.DatosSIMPrepago();
            AuditRequest1 auditRequest = new AuditRequest1();
            auditRequest = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest1>(strIdSession);
            string strIccid = string.Empty;
            string strIccidRecorte1 = string.Empty;
            string strIccidRecorte2 = string.Empty;
            string strImsiSIAC = string.Empty;
            string strImsiHRL = string.Empty;
            string strImsiConcatenado = string.Empty;
            string strValidacionImsi = string.Empty;

            strImsiHRL = objDescarte.descarteValor; //IMSI HLR
            Claro.Web.Logging.Info(strIdSession, auditRequest.transaction, string.Format("{0} --> {1}", "[INICIATIVA-871 - ValidacionImsiSIAC_HLR][strImsiHRL]", strImsiHRL));
            objResponse = _oServiceFixed.ObtenerDatosSIMPrepago(strIdSession, auditRequest.transaction, auditRequest.userName, strPhoneNumber);

            if (objResponse != null)
            {
                strIccid = objResponse.iccid;
                strImsiSIAC = objResponse.imsi;
            }
            else
            {
                Claro.Web.Logging.Info(strIdSession, auditRequest.transaction, "[INICIATIVA-871 - ValidacionImsiSIAC_HLR][SIAC objResponse es nulo]");
            }

            Claro.Web.Logging.Info(strIdSession, auditRequest.transaction, string.Format("{0} --> {1}", "[INICIATIVA-871 - ValidacionImsiSIAC_HLR][SIAC objResponse.iccid]", strIccid));
            Claro.Web.Logging.Info(strIdSession, auditRequest.transaction, string.Format("{0} --> {1}", "[INICIATIVA-871 - ValidacionImsiSIAC_HLR][SIAC objResponse.imsi]", strImsiSIAC));

            strIccidRecorte1 = strIccid;
            strIccidRecorte2 = strIccid;
            strImsiSIAC = strImsiSIAC.Substring(0, 5);
            Claro.Web.Logging.Info(strIdSession, auditRequest.transaction, string.Format("{0} --> {1}", "[INICIATIVA-871 - ValidacionImsiSIAC_HLR][SIAC strImsiSIAC]", strImsiSIAC));

            strIccidRecorte1 = strIccidRecorte1.Substring(6, 2);
            Claro.Web.Logging.Info(strIdSession, auditRequest.transaction, string.Format("{0} --> {1}", "[INICIATIVA-871 - ValidacionImsiSIAC_HLR][strIccidRecorte1]", strIccidRecorte1));

            strIccidRecorte2 = strIccidRecorte2.Substring(10, 8);
            Claro.Web.Logging.Info(strIdSession, auditRequest.transaction, string.Format("{0} --> {1}", "[INICIATIVA-871 - ValidacionImsiSIAC_HLR][strIccidRecorte2]", strIccidRecorte2));

            strImsiConcatenado = string.Format("{0}{1}{2}", strImsiSIAC, strIccidRecorte1, strIccidRecorte2);
            Claro.Web.Logging.Info(strIdSession, auditRequest.transaction, string.Format("{0} --> {1}", "[INICIATIVA-871 - ValidacionImsiSIAC_HLR][strImsiConcatenado]", strImsiConcatenado));

            if (strImsiConcatenado == strImsiHRL)
            {
                objDescarte.descarteValor = KEY.AppSettings("CoincidenciaIMSI");
                objDescarte.flagOk = "1";
            }
            else
            {
                objDescarte.descarteValor = KEY.AppSettings("DesalineacionIMSI");
                objDescarte.flagOk = "0";
            }

            Claro.Web.Logging.Info(strIdSession, auditRequest.transaction, string.Format("{0} --> {1}", "[INICIATIVA-871 - ValidacionImsiSIAC_HLR][Resultado strValidacionImsi]", objDescarte.descarteValor));
            Claro.Web.Logging.Info(strIdSession, auditRequest.transaction, "FIN INICIATIVA-871 - ValidacionImsiSIAC_HLR");

            return objDescarte;
        }
        //FIN: INICIATIVA-871

        public ConsultByGroupParameterResponse ListarParametrosGrupoDescartes(string strIdSession, int strParanGrupo)
        {
            CommonTransacService.AuditRequest auditRequest = new CommonTransacService.AuditRequest();
            auditRequest = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);

            ConsultByGroupParameterResponse objResponseParametros = new ConsultByGroupParameterResponse();
            ConsultByGroupParameterRequest objRequestParametros = new ConsultByGroupParameterRequest();

            objRequestParametros.audit = auditRequest;
            objRequestParametros.SessionId = strIdSession;
            objRequestParametros.TransactionId = auditRequest.transaction;
            objRequestParametros.intCodGrupo = strParanGrupo;

            objResponseParametros = _oServiceCommon.GetConsultByGroupParameter(objRequestParametros);

            return objResponseParametros;
        }

        #region INICIATIVA-986 | MEJORAS DESCARTES AT | Andre Chumbes Lizarraga
        public JsonResult ProcesarContinue(string strIdSession, FIXED.AplicarRetirarContingencia objRequestContinue)
        {
            CommonTransacService.AuditRequest auditRequest = new CommonTransacService.AuditRequest();
            auditRequest = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);

            Claro.Web.Logging.Info(strIdSession, auditRequest.transaction, "INICIO INICIATIVA-986 - ProcesarContinue");

            ItemGeneric objItem = new ItemGeneric();
            FIXED.Response1 objResponseContinue = new FIXED.Response1();
            FIXED.MessageResponseRegistrarProcesoContinue objResponseRegistroTransaccion = new MessageResponseRegistrarProcesoContinue();
            string strTransaccion = auditRequest.transaction;
            string strCodigoRespuesta = string.Empty;
            bool blActivarDesactivarContinueWS = false;
            bool blRegistroTransaccion = false;
            bool blRegistroTipificacion = false;
            bool blRegistroPlantillaInteraccion = false;
            string strInteractionId = string.Empty;
            int reintentos = 2;

            try
            {
                //Realizar continue
                objResponseContinue = ActivarDesactivarContinueWS(strIdSession, strTransaccion, objRequestContinue, ref blActivarDesactivarContinueWS);

                if (blActivarDesactivarContinueWS)
                {
                    //Registrar transaccion continue
                    if (objRequestContinue.Accion == "A") //Aplicar Continue
                    {
                        for (int i = 1; i <= reintentos; i++)
                        {
                            objResponseRegistroTransaccion = RegistrarTransaccionContinue(strIdSession, objRequestContinue, ref strCodigoRespuesta);

                            if (strCodigoRespuesta == "0") //Operacion exitosa
                            {
                                blRegistroTransaccion = true;
                                break;
                            }
                        }
                    }
                    else //Retirar Continue
                    {
                        for (int i = 1; i <= reintentos; i++)
                        {
                            objResponseRegistroTransaccion = ActualizarTransaccionContinue(strIdSession, objRequestContinue, ref strCodigoRespuesta);

                            if (strCodigoRespuesta == "0") //Operacion exitosa
                            {
                                blRegistroTransaccion = true;
                                break;
                            }
                        }
                    }

                    //Registrar Tipificaciones
                    for (int i = 1; i <= reintentos; i++)
                    {
                        blRegistroTipificacion = RegistrarTipificacionContinue(auditRequest, objRequestContinue, ref strInteractionId);

                        if (blRegistroTipificacion)
                        {
                            break;
                        }
                    }

                    if (blRegistroTipificacion)
                    {
                        for (int i = 1; i <= reintentos; i++)
                        {
                            blRegistroPlantillaInteraccion = RegistrarPlantillaTipificacionContinue(auditRequest, objRequestContinue, strInteractionId);

                            if (blRegistroPlantillaInteraccion)
                            {
                                break;
                            }
                        }
                    }

                    //Validar mensaje de respuesta
                    objItem.Code = "0";

                    if (!blRegistroTransaccion && (!blRegistroTipificacion || !blRegistroPlantillaInteraccion))
                    {
                        objItem.Description = "Se aplicó contingencia de manera exitosa, error al registrar transacción y tipificación";
                    }
                    else if (!blRegistroTransaccion)
                    {
                        objItem.Description = "Se aplicó contingencia de manera exitosa, error al registrar transacción";
                    }
                    else if (!blRegistroTipificacion || !blRegistroPlantillaInteraccion)
                    {
                        objItem.Description = "Se aplicó contingencia de manera exitosa, error al registrar tipificación";
                    }
                    else
                    {
                        if (objRequestContinue.Accion == "A")
                        {
                            objItem.Description = "Se aplicó contingencia de manera exitosa";
                        }
                        else
                        {
                            objItem.Description = "Se retiró contingencia de manera exitosa";
                        }
                    }

                    Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} --> {1}", "[INICIATIVA-986 - ProcesarContinue][blActivarDesactivarContinueWS]", Functions.CheckStr(blActivarDesactivarContinueWS)));
                    Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} --> {1}", "[INICIATIVA-986 - ProcesarContinue][blRegistroTransaccion]", Functions.CheckStr(blRegistroTransaccion)));
                    Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} --> {1}", "[INICIATIVA-986 - ProcesarContinue][blRegistroTipificacion]", Functions.CheckStr(blRegistroTipificacion)));
                    Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} --> {1}", "[INICIATIVA-986 - ProcesarContinue][blRegistroPlantillaInteraccion]", Functions.CheckStr(blRegistroPlantillaInteraccion)));
                    Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} --> {1}", "[INICIATIVA-986 - ProcesarContinue][objResponse.Code]", objItem.Code));
                    Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} --> {1}", "[INICIATIVA-986 - ProcesarContinue][objResponse.Description]", objItem.Description));
                }
                else
                {
                    if (objRequestContinue.Accion == "A")
                    {
                        objItem.Code = "1";
                        objItem.Description = "Ocurrió un error al aplicar contingencia, intentar nuevamente";
                    }
                    else
                    {
                        objItem.Code = "1";
                        objItem.Description = "Ocurrió un error al retirar contingencia, intentar nuevamente";
                    }

                    Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} --> {1}", "[INICIATIVA-986 - ProcesarContinue][blActivarDesactivarContinueWS]", Functions.CheckStr(blActivarDesactivarContinueWS)));
                    Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} --> {1}", "[INICIATIVA-986 - ProcesarContinue][objResponse.Code]", objItem.Code));
                    Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} --> {1}", "[INICIATIVA-986 - ProcesarContinue][objResponse.Description]", objItem.Description));
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransaccion, string.Format("{0} => [{1}|{2}]", "[INICIATIVA-986 - ActivarDesactivarContinue][Error]", ex.Message, ex.StackTrace));
            }

            JsonResult objJsonResponse = Json(new { data = objItem });

            Claro.Web.Logging.Info(strIdSession, strTransaccion, "FIN INICIATIVA-986 - ProcesarContinue");

            return objJsonResponse;
        }

        public FIXED.Response1 ActivarDesactivarContinueWS(string strIdSession, string strTransaccion, FIXED.AplicarRetirarContingencia objRequestContinue, ref bool blRespuesta)
        {
            Claro.Web.Logging.Info(strIdSession, strTransaccion, "INICIO INICIATIVA-986 - ActivarDesactivarContinueWS");
            Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} --> {1}", "[INICIATIVA-986 - ActivarDesactivarContinueWS][strTipoCliente]", Functions.CheckStr(objRequestContinue.TipoCliente)));

            FIXED.Response1 objResponse = new FIXED.Response1();
            blRespuesta = false;

            objRequestContinue.ServicioVolte = Functions.CheckStr(objRequestContinue.ServicioVolte.ToUpper()) == "ACTIVO" ? "VOLTE" : string.Empty;

            objRequestContinue.TipoCliente = Functions.CheckStr(objRequestContinue.TipoCliente) == "Consumer" ? "0SY" : "0SX";
            Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} --> {1}", "[INICIATIVA-986 - ActivarDesactivarContinueWS][TipoCliente continue]", Functions.CheckStr(objRequestContinue.TipoCliente)));

            Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} --> {1}", "[INICIATIVA-986 - ActivarDesactivarContinueWS][objRequestContinue.Imsi]", Functions.CheckStr(objRequestContinue.Imsi)));
            Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} --> {1}", "[INICIATIVA-986 - ActivarDesactivarContinueWS][objRequestContinue.Linea]", "51" + objRequestContinue.Linea));
            Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} --> {1}", "[INICIATIVA-986 - ActivarDesactivarContinueWS][objRequestContinue.ServicioVolte]", Functions.CheckStr(objRequestContinue.ServicioVolte)));

            objRequestContinue.NetworkService = objRequestContinue.Accion == "A" ? "0CC" : objRequestContinue.TipoCliente;
            Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} --> {1}", "[INICIATIVA-986 - ActivarDesactivarContinueWS][objRequestContinue.NetworkService]", Functions.CheckStr(objRequestContinue.NetworkService)));

            objRequestContinue.NeType = ConfigurationManager.AppSettings("NeTypeContinue");
            objRequestContinue.Priority = ConfigurationManager.AppSettings("PriorityContinue");
            objRequestContinue.ReqUser = ConfigurationManager.AppSettings("ReqUserContinue");
            objRequestContinue.ActionId = ConfigurationManager.AppSettings("ActionIdContinue");
            objRequestContinue.TipoPlan = ConfigurationManager.AppSettings("TipoPlanContinue");
            objRequestContinue.ClienteCbio = ConfigurationManager.AppSettings("ClienteCbioContinue");

            Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} --> {1}", "[INICIATIVA-986 - ActivarDesactivarContinueWS][objRequestContinue.NeType]", Functions.CheckStr(objRequestContinue.NeType)));
            Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} --> {1}", "[INICIATIVA-986 - ActivarDesactivarContinueWS][objRequestContinue.Priority]", Functions.CheckStr(objRequestContinue.Priority)));
            Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} --> {1}", "[INICIATIVA-986 - ActivarDesactivarContinueWS][objRequestContinue.ReqUser]", Functions.CheckStr(objRequestContinue.ReqUser)));
            Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} --> {1}", "[INICIATIVA-986 - ActivarDesactivarContinueWS][objRequestContinue.ActionId]", Functions.CheckStr(objRequestContinue.ActionId)));
            Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} --> {1}", "[INICIATIVA-986 - ActivarDesactivarContinueWS][objRequestContinue.TipoPlan]", Functions.CheckStr(objRequestContinue.TipoPlan)));
            Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} --> {1}", "[INICIATIVA-986 - ActivarDesactivarContinueWS][objRequestContinue.ClienteCbio]", Functions.CheckStr(objRequestContinue.ClienteCbio)));

            objResponse = _oServiceFixed.ActivarDesactivarContinueWS(strIdSession, strTransaccion, objRequestContinue);

            if (objResponse != null)
            {
                if (objResponse.responseHeaderField != null)
                {
                    if (objResponse.responseHeaderField.statusField == 9) //EXITO
                    {
                        blRespuesta = true;
                    }
                    Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} --> {1}", "[INICIATIVA-986 - ActivarDesactivarContinueWS][objResponse.responseHeaderField.status]", Functions.CheckStr(objResponse.responseHeaderField.statusField)));
                }
            }

            Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} --> {1}", "[INICIATIVA-986 - ActivarDesactivarContinueWS][blRespuesta]", Functions.CheckStr(blRespuesta)));
            Claro.Web.Logging.Info(strIdSession, strTransaccion, "FIN INICIATIVA-986 - ActivarDesactivarContinueWS");

            return objResponse;
        }

        public MessageResponseRegistrarProcesoContinue RegistrarTransaccionContinue(string strIdSession, FIXED.AplicarRetirarContingencia objRequestContinue, ref string strCodigoRespuesta)
        {
            AuditRequest1 auditRequest = new AuditRequest1();
            auditRequest = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest1>(strIdSession);

            MessageResponseRegistrarProcesoContinue objResponse = new MessageResponseRegistrarProcesoContinue();
            MessageRequestRegistrarProcesoContinue objRequest = new MessageRequestRegistrarProcesoContinue();
            objRequest.registrarProcesoContinueRequest = new registrarProcesoContinueRequest();

            string strTransaccion = auditRequest.transaction;
            string strMensajeRespuesta = string.Empty;

            #region request
            Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} --> {1}", "[INICIATIVA-986 - RegistrarTransaccionContinue][PlataformaActivacion]", objRequestContinue.PlataformaActivacion));
            Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} --> {1}", "[INICIATIVA-986 - RegistrarTransaccionContinue][Accion]", objRequestContinue.Accion));

            if (objRequestContinue.PlataformaActivacion == Constants.ASIS) //ASIS - DROP1
            {
                objRequest.registrarProcesoContinueRequest.plataforma = "1"; //ASIS
                objRequest.registrarProcesoContinueRequest.msisdn = objRequestContinue.Linea;
                objRequest.registrarProcesoContinueRequest.accion = "I"; //INSERTAR
                objRequest.registrarProcesoContinueRequest.motivo = Functions.CheckStr(ConfigurationManager.AppSettings("MotivoAplicarContinue"));

                Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} --> {1}", "[INICIATIVA-986 - RegistrarTransaccionContinue][objRequest.registrarProcesoContinueRequest.plataforma]", objRequest.registrarProcesoContinueRequest.plataforma));
                Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} --> {1}", "[INICIATIVA-986 - RegistrarTransaccionContinue][objRequest.registrarProcesoContinueRequest.msisdn]", objRequest.registrarProcesoContinueRequest.msisdn));
                Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} --> {1}", "[INICIATIVA-986 - RegistrarTransaccionContinue][objRequest.registrarProcesoContinueRequest.accion]", objRequest.registrarProcesoContinueRequest.accion));
                Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} --> {1}", "[INICIATIVA-986 - RegistrarTransaccionContinue][objRequest.registrarProcesoContinueRequest.motivo]", objRequest.registrarProcesoContinueRequest.motivo));
            }
            else // TOBE - DROP2
            {
                objRequest.registrarProcesoContinueRequest.plataforma = "2"; //TOBE
                objRequest.registrarProcesoContinueRequest.msisdn = objRequestContinue.Linea;
                objRequest.registrarProcesoContinueRequest.imsi = objRequestContinue.Imsi;
                objRequest.registrarProcesoContinueRequest.accion = "I"; //INSERTAR
                objRequest.registrarProcesoContinueRequest.ip = auditRequest.ipAddress;
                objRequest.registrarProcesoContinueRequest.codigoILink = "0";

                Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} --> {1}", "[INICIATIVA-986 - RegistrarTransaccionContinue][objRequest.registrarProcesoContinueRequest.plataforma]", objRequest.registrarProcesoContinueRequest.plataforma));
                Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} --> {1}", "[INICIATIVA-986 - RegistrarTransaccionContinue][objRequest.registrarProcesoContinueRequest.msisdn]", objRequest.registrarProcesoContinueRequest.msisdn));
                Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} --> {1}", "[INICIATIVA-986 - RegistrarTransaccionContinue][objRequest.registrarProcesoContinueRequest.imsi]", objRequest.registrarProcesoContinueRequest.imsi));
                Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} --> {1}", "[INICIATIVA-986 - RegistrarTransaccionContinue][objRequest.registrarProcesoContinueRequest.accion]", objRequest.registrarProcesoContinueRequest.accion));
                Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} --> {1}", "[INICIATIVA-986 - RegistrarTransaccionContinue][objRequest.registrarProcesoContinueRequest.ip]", objRequest.registrarProcesoContinueRequest.ip));
                Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} --> {1}", "[INICIATIVA-986 - RegistrarTransaccionContinue][objRequest.registrarProcesoContinueRequest.codigoILink]", objRequest.registrarProcesoContinueRequest.codigoILink));
            }
            #endregion

            #region Response
            objResponse = _oServiceFixed.RegistrarActualizarContingencia(objRequest, auditRequest);

            if (objResponse != null)
            {
                if (objResponse.registrarProcesoContinueResponse != null && objResponse.registrarProcesoContinueResponse.responseAudit != null)
                {
                    strCodigoRespuesta = Functions.CheckStr(objResponse.registrarProcesoContinueResponse.responseAudit.codigoRespuesta);
                    strMensajeRespuesta = Functions.CheckStr(objResponse.registrarProcesoContinueResponse.responseAudit.mensajeRespuesta);
                }
                else
                {
                    Claro.Web.Logging.Info(auditRequest.Session, auditRequest.transaction, "[INICIATIVA-986 - RegistrarTransaccionContinue - Ocurrio un error al registrar la transaccion de continue]");
                }
            }
            else
            {
                Claro.Web.Logging.Info(auditRequest.Session, auditRequest.transaction, "[INICIATIVA-986 - RegistrarTransaccionContinue - Ocurrio un error al registrar la transaccion de continue]");
            }

            Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} --> {1}", "[INICIATIVA-986 - RegistrarTransaccionContinue][objResponse.strCodigoRespuesta]", strCodigoRespuesta));
            Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} --> {1}", "[INICIATIVA-986 - RegistrarTransaccionContinue][objResponse.strMensajeRespuesta]", strMensajeRespuesta));

            return objResponse;
            #endregion
        }

        public MessageResponseRegistrarProcesoContinue ActualizarTransaccionContinue(string strIdSession, FIXED.AplicarRetirarContingencia objRequestContinue, ref string strCodigoRespuesta)
        {
            AuditRequest1 auditRequest = new AuditRequest1();
            auditRequest = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest1>(strIdSession);

            MessageResponseRegistrarProcesoContinue objResponse = new MessageResponseRegistrarProcesoContinue();
            MessageRequestRegistrarProcesoContinue objRequest = new MessageRequestRegistrarProcesoContinue();
            objRequest.registrarProcesoContinueRequest = new registrarProcesoContinueRequest();

            string strTransaccion = auditRequest.transaction;
            string strMensajeRespuesta = string.Empty;

            #region request
            Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} --> {1}", "[INICIATIVA-986 - ActualizarTransaccionContinue][PlataformaActivacion]", objRequestContinue.PlataformaActivacion));

            if (objRequestContinue.PlataformaActivacion == Constants.ASIS) //ASIS - DROP1
            {
                objRequest.registrarProcesoContinueRequest.plataforma = "1"; //ASIS
                objRequest.registrarProcesoContinueRequest.msisdn = objRequestContinue.Linea;
                objRequest.registrarProcesoContinueRequest.accion = "U"; //ACTUALIZAR
                objRequest.registrarProcesoContinueRequest.motivo = Functions.CheckStr(ConfigurationManager.AppSettings("MotivoRetirarContinue")); ;

                Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} --> {1}", "[INICIATIVA-986 - ActualizarTransaccionContinue][objRequest.registrarProcesoContinueRequest.plataforma]", objRequest.registrarProcesoContinueRequest.plataforma));
                Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} --> {1}", "[INICIATIVA-986 - ActualizarTransaccionContinue][objRequest.registrarProcesoContinueRequest.msisdn]", objRequest.registrarProcesoContinueRequest.msisdn));
                Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} --> {1}", "[INICIATIVA-986 - ActualizarTransaccionContinue][objRequest.registrarProcesoContinueRequest.accion]", objRequest.registrarProcesoContinueRequest.accion));
                Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} --> {1}", "[INICIATIVA-986 - ActualizarTransaccionContinue][objRequest.registrarProcesoContinueRequest.motivo]", objRequest.registrarProcesoContinueRequest.motivo));
            }
            else // TOBE - DROP2
            {
                objRequest.registrarProcesoContinueRequest.plataforma = "2"; //TOBE
                objRequest.registrarProcesoContinueRequest.msisdn = objRequestContinue.Linea;
                objRequest.registrarProcesoContinueRequest.imsi = objRequestContinue.Imsi;
                objRequest.registrarProcesoContinueRequest.accion = "U"; //ACTUALIZAR

                Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} --> {1}", "[INICIATIVA-986 - ActualizarTransaccionContinue][objRequest.registrarProcesoContinueRequest.plataforma]", objRequest.registrarProcesoContinueRequest.plataforma));
                Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} --> {1}", "[INICIATIVA-986 - ActualizarTransaccionContinue][objRequest.registrarProcesoContinueRequest.msisdn]", objRequest.registrarProcesoContinueRequest.msisdn));
                Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} --> {1}", "[INICIATIVA-986 - ActualizarTransaccionContinue][objRequest.registrarProcesoContinueRequest.imsi]", objRequest.registrarProcesoContinueRequest.imsi));
                Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} --> {1}", "[INICIATIVA-986 - ActualizarTransaccionContinue][objRequest.registrarProcesoContinueRequest.accion]", objRequest.registrarProcesoContinueRequest.accion));
            }
            #endregion

            #region Response
            objResponse = _oServiceFixed.RegistrarActualizarContingencia(objRequest, auditRequest);

            if (objResponse != null)
            {
                if (objResponse.registrarProcesoContinueResponse != null && objResponse.registrarProcesoContinueResponse.responseAudit != null)
                {
                    strCodigoRespuesta = Functions.CheckStr(objResponse.registrarProcesoContinueResponse.responseAudit.codigoRespuesta);
                    strMensajeRespuesta = Functions.CheckStr(objResponse.registrarProcesoContinueResponse.responseAudit.mensajeRespuesta);
                }
                else
                {
                    Claro.Web.Logging.Info(auditRequest.Session, auditRequest.transaction, "[INICIATIVA-986 - ActualizarTransaccionContinue - Ocurrio un error al registrar la transaccion de continue]");
                }
            }
            else
            {
                Claro.Web.Logging.Info(auditRequest.Session, auditRequest.transaction, "[INICIATIVA-986 - ActualizarTransaccionContinue - Ocurrio un error al registrar la transaccion de continue]");
            }

            Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} --> {1}", "[INICIATIVA-986 - ActualizarTransaccionContinue][objResponse.strCodigoRespuesta]", strCodigoRespuesta));
            Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} --> {1}", "[INICIATIVA-986 - ActualizarTransaccionContinue][objResponse.strMensajeRespuesta]", strMensajeRespuesta));

            return objResponse;
            #endregion
        }

        public bool RegistrarTipificacionContinue(CommonTransacService.AuditRequest auditRequest, FIXED.AplicarRetirarContingencia objRequestContinue, ref string strInteractionId)
        {
            string strIdSession = Functions.CheckStr(auditRequest.Session);
            string strTransaccion = Functions.CheckStr(auditRequest.transaction);
            Claro.Web.Logging.Info(strIdSession, strTransaccion, "INICIO INICIATIVA-986 - RegistrarInteraccionContinue");

            InsertIntRequestCommon objRequestInteraccion = new InsertIntRequestCommon();
            InsertIntResponseCommon objResponseInteraccion = new InsertIntResponseCommon();
            bool blRespuesta = false;

            objRequestInteraccion.item = new CommonService.Iteraction();
            objRequestInteraccion.item.OBJID_CONTACTO = objRequestContinue.ContactCode;
            objRequestInteraccion.item.TELEFONO = objRequestContinue.Linea;
            objRequestInteraccion.item.TIPO = Functions.CheckStr(ConfigurationManager.AppSettings("TipoTipificacionPostpago"));
            objRequestInteraccion.item.CLASE = Functions.CheckStr(ConfigurationManager.AppSettings("ClaseTipificacionVariacionFallasTecnicas"));
            objRequestInteraccion.item.SUBCLASE = objRequestContinue.Accion == "A" ? Functions.CheckStr(ConfigurationManager.AppSettings("SubClaseTipificacionAplicarContingencia"))
                                                                        : Functions.CheckStr(ConfigurationManager.AppSettings("SubClaseTipificacionRetirarContingencia"));

            Claro.Web.Logging.Info(strIdSession, strTransaccion, "[INICIATIVA-986 - RegistrarInteraccionContinue][Inicio invocacion a metodo generico RegistrarInteraccion]");

            objResponseInteraccion = RegistrarInteraccion(auditRequest.Session, objRequestInteraccion);

            Claro.Web.Logging.Info(strIdSession, strTransaccion, "[INICIATIVA-986 - RegistrarInteraccionContinue][Fin invocacion a metodo generico RegistrarInteraccion]");

            if (objResponseInteraccion != null)
            {
                if (objResponseInteraccion.FlagInsercion == "OK" && !string.IsNullOrEmpty(objResponseInteraccion.Interactionid))
                {
                    strInteractionId = objResponseInteraccion.Interactionid;
                    blRespuesta = true;
                }
            }
            else
            {
                Claro.Web.Logging.Info(auditRequest.Session, auditRequest.transaction, "[INICIATIVA-986 - RegistrarInteraccionContinue - Ocurrio un error al registrar la plantilla de interaccion]");
            }

            Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} --> {1}", "[INICIATIVA-986 - RegistrarInteraccionContinue][blRespuesta]", Functions.CheckStr(blRespuesta)));
            Claro.Web.Logging.Info(strIdSession, strTransaccion, "FIN INICIATIVA-986 - RegistrarInteraccionContinue");

            return blRespuesta;
        }

        public bool RegistrarPlantillaTipificacionContinue(CommonTransacService.AuditRequest auditRequest, FIXED.AplicarRetirarContingencia objRequestContinue, string strInteractionid)
        {
            string strIdSession = Functions.CheckStr(auditRequest.Session);
            string strTransaccion = Functions.CheckStr(auditRequest.transaction);
            Claro.Web.Logging.Info(strIdSession, strTransaccion, "INICIO INICIATIVA-986 - RegistrarPlantillaInteraccionContinue");

            InsertTemplateInteractionRequestCommon objRequestPlantillaInteraccion = new InsertTemplateInteractionRequestCommon();
            InsertTemplateInteractionResponseCommon objResponsePlantillaInteraccion = new InsertTemplateInteractionResponseCommon();
            string strEscenario = string.Empty;
            string strAccion = string.Empty;
            string strNotas = string.Empty;
            bool blRespuesta = false;

            strAccion = objRequestContinue.Accion == "A" ? "Aplicar contingencia" : "Retirar contingencia";

            if (objRequestContinue.Accion == "R")
            {
                strNotas = ConfigurationManager.AppSettings("NotasContinueRetirarContingencia");
            }
            else if (objRequestContinue.Escenario == "Línea no provisionada")
            {
                strNotas = ConfigurationManager.AppSettings("NotasContinueLineaNoProvisionada");
            }
            else if (objRequestContinue.Escenario == "Plan no provisionado")
            {
                strNotas = ConfigurationManager.AppSettings("NotasContinuePlanNoProvisionado");
            }
            else if (objRequestContinue.Escenario == "Desalineación de plan")
            {
                strNotas = ConfigurationManager.AppSettings("NotasContinueDesalineacionDePlan");
            }

            objRequestPlantillaInteraccion.audit = auditRequest;
            objRequestPlantillaInteraccion.IdInteraction = strInteractionid;
            objRequestPlantillaInteraccion.item = new CommonService.InsertTemplateInteraction();
            objRequestPlantillaInteraccion.item._X_INTER_1 = strAccion;
            objRequestPlantillaInteraccion.item._X_INTER_2 = objRequestContinue.Escenario;
            objRequestPlantillaInteraccion.item._X_INTER_3 = strNotas;
            objRequestPlantillaInteraccion.item._X_PLUS_INTER2INTERACT = Functions.CheckDbl(strInteractionid);
            objRequestPlantillaInteraccion.item._X_DOCUMENT_NUMBER = objRequestContinue.NumeroDocumento;
            objRequestPlantillaInteraccion.item._X_FIRST_NAME = objRequestContinue.NombresCliente;
            objRequestPlantillaInteraccion.item._X_LAST_NAME = objRequestContinue.ApellidosCliente;
            objRequestPlantillaInteraccion.item._X_CLARO_NUMBER = objRequestContinue.Linea;

            Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} --> {1}", "[INICIATIVA-986 - RegistrarPlantillaInteraccionContinue][objRequestPlantillaInteraccion.IdInteraction]", objRequestPlantillaInteraccion.IdInteraction));
            Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} --> {1}", "[INICIATIVA-986 - RegistrarPlantillaInteraccionContinue][objRequestPlantillaInteraccion.item._X_INTER_1]", objRequestPlantillaInteraccion.item._X_INTER_1));
            Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} --> {1}", "[INICIATIVA-986 - RegistrarPlantillaInteraccionContinue][objRequestPlantillaInteraccion.item._X_INTER_2]", objRequestPlantillaInteraccion.item._X_INTER_2));
            Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} --> {1}", "[INICIATIVA-986 - RegistrarPlantillaInteraccionContinue][objRequestPlantillaInteraccion.item._X_INTER_3]", strNotas));
            Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} --> {1}", "[INICIATIVA-986 - RegistrarPlantillaInteraccionContinue][objRequestPlantillaInteraccion.item._X_PLUS_INTER2INTERACT]", objRequestPlantillaInteraccion.item._X_PLUS_INTER2INTERACT));
            Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} --> {1}", "[INICIATIVA-986 - RegistrarPlantillaInteraccionContinue][objRequestPlantillaInteraccion.item._X_DOCUMENT_NUMBER]", objRequestPlantillaInteraccion.item._X_DOCUMENT_NUMBER));
            Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} --> {1}", "[INICIATIVA-986 - RegistrarPlantillaInteraccionContinue][objRequestPlantillaInteraccion.item._X_FIRST_NAME]", objRequestPlantillaInteraccion.item._X_FIRST_NAME));
            Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} --> {1}", "[INICIATIVA-986 - RegistrarPlantillaInteraccionContinue][objRequestPlantillaInteraccion.item._X_LAST_NAME]", objRequestPlantillaInteraccion.item._X_LAST_NAME));
            Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} --> {1}", "[INICIATIVA-986 - RegistrarPlantillaInteraccionContinue][objRequestPlantillaInteraccion.item._X_CLARO_NUMBER]", objRequestPlantillaInteraccion.item._X_CLARO_NUMBER));

            objResponsePlantillaInteraccion = _oServiceCommon.GetInsertInteractionTemplate(objRequestPlantillaInteraccion);

            if (objResponsePlantillaInteraccion != null)
            {
                if (objResponsePlantillaInteraccion.FlagInsercion == "OK")
                {
                    blRespuesta = true;
                    Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} --> {1}", "[INICIATIVA-986 - RegistrarPlantillaInteraccionContinue][objResponsePlantillaInteraccion.FlagInsercion]", objResponsePlantillaInteraccion.FlagInsercion));
                    Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} --> {1}", "[INICIATIVA-986 - RegistrarPlantillaInteraccionContinue][objResponsePlantillaInteraccion.MsgText]", Functions.CheckStr(objResponsePlantillaInteraccion.MsgText)));
                }
            }
            else
            {
                Claro.Web.Logging.Info(auditRequest.Session, auditRequest.transaction, "[INICIATIVA-986 - RegistrarPlantillaInteraccionContinue - Ocurrio un error al registrar la plantilla de interaccion]");
            }

            Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} --> {1}", "[INICIATIVA-986 - RegistrarPlantillaInteraccionContinue][blRespuesta]", Functions.CheckStr(blRespuesta)));
            Claro.Web.Logging.Info(auditRequest.Session, auditRequest.transaction, "FIN INICIATIVA-986 - RegistrarPlantillaInteraccionContinue");

            return blRespuesta;
        }

        #region Tipificaciones - Descartes AT
        //SP: SA.PCK_INTERACT_CLFY.SP_CREATE_INTERACT
        public InsertIntResponseCommon RegistrarInteraccion(string strIdSession, InsertIntRequestCommon objRequestInteraccion)
        {
            CommonTransacService.AuditRequest auditRequest = new CommonTransacService.AuditRequest();
            auditRequest = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);

            Claro.Web.Logging.Info(strIdSession, auditRequest.transaction, "INICIO INICIATIVA-986 - RegistrarInteraccion");

            InsertIntResponseCommon objResponse = new InsertIntResponseCommon();
            InsertIntRequestCommon objRequest = new InsertIntRequestCommon();
            string strTransaccion = auditRequest.transaction;

            try
            {
                #region Request
                objRequest.audit = auditRequest;
                objRequest.item = new CommonService.Iteraction();
                objRequest.item.OBJID_CONTACTO = Functions.CheckStr(objRequestInteraccion.item.OBJID_CONTACTO);
                objRequest.item.FECHA_CREACION = DateTime.Now.ToString();
                objRequest.item.TELEFONO = Functions.CheckStr(objRequestInteraccion.item.TELEFONO);
                objRequest.item.TIPO = Functions.CheckStr(objRequestInteraccion.item.TIPO);
                objRequest.item.CLASE = Functions.CheckStr(objRequestInteraccion.item.CLASE);
                objRequest.item.SUBCLASE = Functions.CheckStr(objRequestInteraccion.item.SUBCLASE);
                objRequest.item.TIPO_INTER = ConfigurationManager.AppSettings("AtencionDefault");
                objRequest.item.METODO = ConfigurationManager.AppSettings("MetodoContactoTelefonoDefault");
                objRequest.item.RESULTADO = ConfigurationManager.AppSettings("Ninguno");
                objRequest.item.HECHO_EN_UNO = "0";
                objRequest.item.NOTAS = Functions.CheckStr(objRequestInteraccion.item.NOTAS);
                objRequest.item.FLAG_CASO = "0";
                objRequest.item.USUARIO_PROCESO = ConfigurationManager.AppSettings("USRProceso");
                objRequest.item.AGENTE = Functions.CheckStr(App_Code.Common.CurrentUser);

                Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} --> {1}", "[INICIATIVA-986 - RegistrarInteraccion][objRequest.item.OBJID_CONTACTO]", objRequest.item.OBJID_CONTACTO));
                Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} --> {1}", "[INICIATIVA-986 - RegistrarInteraccion][objRequest.item.FECHA_CREACION]", Functions.CheckStr(objRequest.item.FECHA_CREACION)));
                Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} --> {1}", "[INICIATIVA-986 - RegistrarInteraccion][objRequest.item.TELEFONO]", objRequest.item.TELEFONO));
                Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} --> {1}", "[INICIATIVA-986 - RegistrarInteraccion][objRequest.item.TIPO]", objRequest.item.TIPO));
                Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} --> {1}", "[INICIATIVA-986 - RegistrarInteraccion][objRequest.item.CLASE]", objRequest.item.CLASE));
                Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} --> {1}", "[INICIATIVA-986 - RegistrarInteraccion][objRequest.item.SUBCLASE]", objRequest.item.SUBCLASE));
                Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} --> {1}", "[INICIATIVA-986 - RegistrarInteraccion][objRequest.item.TIPO_INTER]", objRequest.item.TIPO_INTER));
                Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} --> {1}", "[INICIATIVA-986 - RegistrarInteraccion][objRequest.item.METODO]", objRequest.item.METODO));
                Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} --> {1}", "[INICIATIVA-986 - RegistrarInteraccion][objRequest.item.RESULTADO]", objRequest.item.RESULTADO));
                Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} --> {1}", "[INICIATIVA-986 - RegistrarInteraccion][objRequest.item.HECHO_EN_UNO]", objRequest.item.HECHO_EN_UNO));
                Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} --> {1}", "[INICIATIVA-986 - RegistrarInteraccion][objRequest.item.NOTAS]", objRequest.item.NOTAS));
                Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} --> {1}", "[INICIATIVA-986 - RegistrarInteraccion][objRequest.item.FLAG_CASO]", objRequest.item.FLAG_CASO));
                Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} --> {1}", "[INICIATIVA-986 - RegistrarInteraccion][objRequest.item.USUARIO_PROCESO]", objRequest.item.USUARIO_PROCESO));
                Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} --> {1}", "[INICIATIVA-986 - RegistrarInteraccion][objRequest.item.AGENTE]", objRequest.item.AGENTE));
                #endregion

                #region Response
                objResponse = _oServiceCommon.InsertInt(objRequest);

                Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} --> {1}", "[INICIATIVA-986 - RegistrarInteraccion][objResponse.Interactionid]", Functions.CheckStr(objResponse.Interactionid)));
                Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} --> {1}", "[INICIATIVA-986 - RegistrarInteraccion][objResponse.FlagInsercion]", Functions.CheckStr(objResponse.FlagInsercion)));
                Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} --> {1}", "[INICIATIVA-986 - RegistrarInteraccion][objResponse.MsgText]", Functions.CheckStr(objResponse.MsgText)));
                #endregion
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransaccion, string.Format("{0} => [{1}|{2}]", "[INICIATIVA-986 - RegistrarInteraccion][Error]", ex.Message, ex.StackTrace));
            }

            Claro.Web.Logging.Info(strIdSession, strTransaccion, "FIN INICIATIVA-986 - RegistrarInteraccion");

            return objResponse;
        }

        public InteractionSubClasDetailRequestCommon ObtenerDetalleInteraccionAutomaticoDescarte(string strTipo, string strTipoInconveniente)
        {
            InteractionSubClasDetailRequestCommon objRequestDetalleAutomatico = new InteractionSubClasDetailRequestCommon();

            if (strTipo == "POSTPAGO")
            {
                objRequestDetalleAutomatico.item = new InteractionSubClasDetail();
                objRequestDetalleAutomatico.item.TIPO_CODIGO = Functions.CheckStr(ConfigurationManager.AppSettings("codTipoTipificacionPostpago"));
                objRequestDetalleAutomatico.item.CLASE_CODIGO = Functions.CheckStr(ConfigurationManager.AppSettings("codClaseVariacionFallasTecnicasPostpago"));
                objRequestDetalleAutomatico.item.SUBCLASE_CODIGO = Functions.CheckStr(ConfigurationManager.AppSettings("codSubClaseFallaTecnicaPostpago"));
                objRequestDetalleAutomatico.item.SERVAFECT_CODE = Functions.CheckStr(ConfigurationManager.AppSettings("codServicioAfectadoInternetPostpago"));
                objRequestDetalleAutomatico.item.TIPO = Functions.CheckStr(ConfigurationManager.AppSettings("TipoTipificacionPostpago"));
                objRequestDetalleAutomatico.item.CLASE = Functions.CheckStr(ConfigurationManager.AppSettings("ClaseTipificacionVariacionFallasTecnicas"));
                objRequestDetalleAutomatico.item.SUBCLASE = Functions.CheckStr(ConfigurationManager.AppSettings("SubClaseTipificacionFallaTecnica"));
                objRequestDetalleAutomatico.item.SERVAFECT = Functions.CheckStr(ConfigurationManager.AppSettings("ServicioAfectadoInternet"));

                if (strTipoInconveniente == "12750220") //Reinicio de Datos Moviles
                {
                    objRequestDetalleAutomatico.item.INCONVEN_CODE = Functions.CheckStr(ConfigurationManager.AppSettings("codInconvenienteReinicioDatosMovilesPostpago"));
                    objRequestDetalleAutomatico.item.INCONVEN = Functions.CheckStr(ConfigurationManager.AppSettings("InconvenienteReinicioDatosMoviles"));
                }
                else if (strTipoInconveniente == "12750225") //Reinicio de Red
                {
                    objRequestDetalleAutomatico.item.INCONVEN_CODE = Functions.CheckStr(ConfigurationManager.AppSettings("codInconvenienteReinicioDeRedPostpago"));
                    objRequestDetalleAutomatico.item.INCONVEN = Functions.CheckStr(ConfigurationManager.AppSettings("InconvenienteReinicioDeRed"));
                }
            }
            else if (strTipo == "PREPAGO")
            {
                objRequestDetalleAutomatico.item = new InteractionSubClasDetail();
                objRequestDetalleAutomatico.item.TIPO_CODIGO = Functions.CheckStr(ConfigurationManager.AppSettings("codTipoTipificacionPrepago"));
                objRequestDetalleAutomatico.item.CLASE_CODIGO = Functions.CheckStr(ConfigurationManager.AppSettings("codClaseTipificacionVariacionFallasTecnicas"));
                objRequestDetalleAutomatico.item.SUBCLASE_CODIGO = Functions.CheckStr(ConfigurationManager.AppSettings("codSubClaseTipificacionFallaTecnica"));
                objRequestDetalleAutomatico.item.SERVAFECT_CODE = Functions.CheckStr(ConfigurationManager.AppSettings("codServicioAfectadoInternet"));
                objRequestDetalleAutomatico.item.TIPO = Functions.CheckStr(ConfigurationManager.AppSettings("TipoTipificacionPrepago"));
                objRequestDetalleAutomatico.item.CLASE = Functions.CheckStr(ConfigurationManager.AppSettings("ClaseTipificacionVariacionFallasTecnicas"));
                objRequestDetalleAutomatico.item.SUBCLASE = Functions.CheckStr(ConfigurationManager.AppSettings("SubClaseTipificacionFallaTecnica"));
                objRequestDetalleAutomatico.item.SERVAFECT = Functions.CheckStr(ConfigurationManager.AppSettings("ServicioAfectadoInternet"));

                if (strTipoInconveniente == "101300220") //Reinicio de Datos Moviles
                {
                    objRequestDetalleAutomatico.item.INCONVEN_CODE = Functions.CheckStr(ConfigurationManager.AppSettings("codInconvenienteReinicioDatosMovilesPrepago"));
                    objRequestDetalleAutomatico.item.INCONVEN = Functions.CheckStr(ConfigurationManager.AppSettings("InconvenienteReinicioDatosMoviles"));
                }
                else if (strTipoInconveniente == "101300221") //Reinicio de Red
                {
                    objRequestDetalleAutomatico.item.INCONVEN_CODE = Functions.CheckStr(ConfigurationManager.AppSettings("codInconvenienteReinicioDeRedPrepago"));
                    objRequestDetalleAutomatico.item.INCONVEN = Functions.CheckStr(ConfigurationManager.AppSettings("InconvenienteReinicioDeRed"));
                }
            }

            return objRequestDetalleAutomatico;
        }

        //SP: SA.PCK_CASE_CLFY.SP_INS_DET_INTERACCION
        public InteractionSubClasDetailResponseCommon RegistrarDetalleInteraccion(string strIdSession, InteractionSubClasDetailRequestCommon objRequestDetalleInteraccion)
        {
            CommonTransacService.AuditRequest auditRequest = new CommonTransacService.AuditRequest();
            auditRequest = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);

            Claro.Web.Logging.Info(strIdSession, auditRequest.transaction, "INICIO INICIATIVA-986 - RegistrarDetalleInteraccion");

            InteractionSubClasDetailRequestCommon objRequest = new InteractionSubClasDetailRequestCommon();
            InteractionSubClasDetailResponseCommon objResponse = new InteractionSubClasDetailResponseCommon();

            try
            {
                #region Request
                objRequest.audit = auditRequest;
                objRequest.item = new InteractionSubClasDetail();
                objRequest.item.INTERACT_ID = Functions.CheckStr(objRequestDetalleInteraccion.item.INTERACT_ID);
                objRequest.item.CASOID = Functions.CheckStr(objRequestDetalleInteraccion.item.CASOID);
                objRequest.item.TIPO = Functions.CheckStr(objRequestDetalleInteraccion.item.TIPO);
                objRequest.item.CLASE = Functions.CheckStr(objRequestDetalleInteraccion.item.CLASE);
                objRequest.item.SUBCLASE = Functions.CheckStr(objRequestDetalleInteraccion.item.SUBCLASE);
                objRequest.item.SERVAFECT = Functions.CheckStr(objRequestDetalleInteraccion.item.SERVAFECT);
                objRequest.item.INCONVEN = Functions.CheckStr(objRequestDetalleInteraccion.item.INCONVEN);
                objRequest.item.TIPO_CODIGO = Functions.CheckStr(objRequestDetalleInteraccion.item.TIPO_CODIGO);
                objRequest.item.CLASE_CODIGO = Functions.CheckStr(objRequestDetalleInteraccion.item.CLASE_CODIGO);
                objRequest.item.SUBCLASE_CODIGO = Functions.CheckStr(objRequestDetalleInteraccion.item.SUBCLASE_CODIGO);
                objRequest.item.SERVAFECT_CODE = Functions.CheckStr(objRequestDetalleInteraccion.item.SERVAFECT_CODE);
                objRequest.item.INCONVEN_CODE = Functions.CheckStr(objRequestDetalleInteraccion.item.INCONVEN_CODE);
                objRequest.item.USUARIO_PROCESO = Functions.CheckStr(App_Code.Common.CurrentUser);

                Claro.Web.Logging.Info(strIdSession, auditRequest.transaction, string.Format("{0} --> {1}", "[INICIATIVA-986 - RegistrarDetalleInteraccion][objRequest.item.INTERACT_ID]", objRequest.item.INTERACT_ID));
                Claro.Web.Logging.Info(strIdSession, auditRequest.transaction, string.Format("{0} --> {1}", "[INICIATIVA-986 - RegistrarDetalleInteraccion][objRequest.item.CASOID]", objRequest.item.CASOID));
                Claro.Web.Logging.Info(strIdSession, auditRequest.transaction, string.Format("{0} --> {1}", "[INICIATIVA-986 - RegistrarDetalleInteraccion][objRequest.item.TIPO]", objRequest.item.TIPO));
                Claro.Web.Logging.Info(strIdSession, auditRequest.transaction, string.Format("{0} --> {1}", "[INICIATIVA-986 - RegistrarDetalleInteraccion][objRequest.item.CLASE]", objRequest.item.CLASE));
                Claro.Web.Logging.Info(strIdSession, auditRequest.transaction, string.Format("{0} --> {1}", "[INICIATIVA-986 - RegistrarDetalleInteraccion][objRequest.item.SUBCLASE]", objRequest.item.SUBCLASE));
                Claro.Web.Logging.Info(strIdSession, auditRequest.transaction, string.Format("{0} --> {1}", "[INICIATIVA-986 - RegistrarDetalleInteraccion][objRequest.item.SERVAFECT]", objRequest.item.SERVAFECT));
                Claro.Web.Logging.Info(strIdSession, auditRequest.transaction, string.Format("{0} --> {1}", "[INICIATIVA-986 - RegistrarDetalleInteraccion][objRequest.item.INCONVEN]", objRequest.item.INCONVEN));
                Claro.Web.Logging.Info(strIdSession, auditRequest.transaction, string.Format("{0} --> {1}", "[INICIATIVA-986 - RegistrarDetalleInteraccion][objRequest.item.TIPO_CODIGO]", objRequest.item.TIPO_CODIGO));
                Claro.Web.Logging.Info(strIdSession, auditRequest.transaction, string.Format("{0} --> {1}", "[INICIATIVA-986 - RegistrarDetalleInteraccion][objRequest.item.CLASE_CODIGO]", objRequest.item.CLASE_CODIGO));
                Claro.Web.Logging.Info(strIdSession, auditRequest.transaction, string.Format("{0} --> {1}", "[INICIATIVA-986 - RegistrarDetalleInteraccion][objRequest.item.SUBCLASE_CODIGO]", objRequest.item.SUBCLASE_CODIGO));
                Claro.Web.Logging.Info(strIdSession, auditRequest.transaction, string.Format("{0} --> {1}", "[INICIATIVA-986 - RegistrarDetalleInteraccion][objRequest.item.SERVAFECT_CODE]", objRequest.item.SERVAFECT_CODE));
                Claro.Web.Logging.Info(strIdSession, auditRequest.transaction, string.Format("{0} --> {1}", "[INICIATIVA-986 - RegistrarDetalleInteraccion][objRequest.item.INCONVEN_CODE]", objRequest.item.INCONVEN_CODE));
                Claro.Web.Logging.Info(strIdSession, auditRequest.transaction, string.Format("{0} --> {1}", "[INICIATIVA-986 - RegistrarDetalleInteraccion][objRequest.item.USUARIO_PROCESO]", objRequest.item.USUARIO_PROCESO));
                #endregion

                #region Response
                objResponse = _oServiceCommon.InsertRecordSubClaseDetail(objRequest);

                Claro.Web.Logging.Info(strIdSession, auditRequest.transaction, string.Format("{0} --> {1}", "[INICIATIVA-986 - RegistrarDetalleInteraccion][objResponse.codigoRespuesta]", Functions.CheckStr(objResponse.CodeError)));
                Claro.Web.Logging.Info(strIdSession, auditRequest.transaction, string.Format("{0} --> {1}", "[INICIATIVA-986 - RegistrarDetalleInteraccion][objResponse.]", Functions.CheckStr(objResponse.MsgError)));
                #endregion
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, auditRequest.transaction, string.Format("{0} => [{1}|{2}]", "[INICIATIVA-986 - RegistrarDetalleInteraccion][Error]", ex.Message, ex.StackTrace));
            }

            return objResponse;
        }
        #endregion
        #endregion
        //FIN: INICIATIVA-871
    }
}