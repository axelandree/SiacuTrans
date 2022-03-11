using System;
using Claro.SIACU.Data.Transac.Service.Configuration;
using Claro.SIACU.Entity.Transac.Service;
using Claro.SIACU.Entity.Transac.Service.Fixed.GetReconeService;
using Claro.Web;
using EntitiesFixed = Claro.SIACU.Entity.Transac.Service.Fixed;
using ConstantsHFC = Claro.SIACU.Transac.Service.Constants;
using ActDesactLTE = Claro.SIACU.ProxyService.Transac.Service.SIACFixed.ServAddLTE;
using ActDesactHFC = Claro.SIACU.ProxyService.Transac.Service.SIACFixed.ServAddHFC;
using KEY = Claro.ConfigurationManager;

namespace Claro.SIACU.Data.Transac.Service.Fixed
{
    public class SuspensionService
    {
        public static EntitiesFixed.PostExecuteSuspension.ExecuteSuspensionResponse EjecutaSuspensionDeServicioCodRes(
            EntitiesFixed.PostExecuteSuspension.ExecuteSuspensionRequest objRequest)
        {
            string mensajeRespuesta = string.Empty;

            var objResponse = new EntitiesFixed.PostExecuteSuspension.ExecuteSuspensionResponse();
            try
            {
                ActDesactHFC.ParametroType[] listaOpcionalRequest = new ActDesactHFC.ParametroType[0];
                ActDesactHFC.ParametroType[] listaOpcionalResponse = new ActDesactHFC.ParametroType[0];
                var idTransaccion = string.Empty;

                var result = Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return ServiceConfiguration.SiacFixedActivationDesactivacionHfc.ejecutarSuspencionServicio(
                        ref idTransaccion, objRequest.Suspension.codigoAplicacion, objRequest.Suspension.ipAplicacion,
                        objRequest.Suspension.flagAccion, objRequest.Suspension.fechaProgramacion,
                        objRequest.Suspension.coId, objRequest.Suspension.nroDias, objRequest.Suspension.fideliza,
                        objRequest.Suspension.fechaSuspension, objRequest.Suspension.ticklerCode,
                        objRequest.Suspension.desTickler, objRequest.Suspension.usuario,
                        objRequest.Suspension.codCliente,
                        objRequest.Suspension.coState, objRequest.Suspension.reason,
                        objRequest.Suspension.telefono, objRequest.Suspension.tipoServicio, objRequest.Suspension.coSer,
                        objRequest.Suspension.tipoRegistro, objRequest.Suspension.usuarioSistema,
                        objRequest.Suspension.usuarioApp, objRequest.Suspension.emailUsuarioApp,
                        objRequest.Suspension.desCoSer,
                        objRequest.Suspension.codigoInteraccion, objRequest.Suspension.nroCuenta, listaOpcionalRequest,
                        out mensajeRespuesta, out listaOpcionalResponse);
                });

                objResponse.idtrans = idTransaccion;
                objResponse.result = result;
                objResponse.ResultMethod = result.Equals(ConstantsHFC.strCero);
            }
            catch (Exception e)
            {
                Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, e.Message);
            }

            return objResponse;

        }

        public static ReconeServiceResponse GetReconectionService(ReconeServiceRequest objContent)
        {
            var model = new ReconeServiceResponse();
            string msgRespta = string.Empty;
            ActDesactHFC.ParametroType[] listaOpcionalRequest = new ActDesactHFC.ParametroType[0];
            ActDesactHFC.ParametroType[] listaOpcionalResponse = new ActDesactHFC.ParametroType[0];

            var idTransaction = string.Empty;
            var result = Claro.Web.Logging.ExecuteMethod(objContent.Audit.Session, objContent.Audit.Transaction,
                () =>
                {
                    return ServiceConfiguration.SiacFixedActivationDesactivacionHfc.ejecutarSuspencionServicio(
                        ref idTransaction,
                        objContent.GetReconection.CodeAplication,
                        objContent.GetReconection.IpAplication,
                        objContent.GetReconection.FlagAccion,
                        objContent.GetReconection.ProgramationDate,
                        objContent.GetReconection.CoId,
                        objContent.GetReconection.NroDay,
                        objContent.GetReconection.Fideliza,
                        objContent.GetReconection.SuspentionDate,
                        objContent.GetReconection.CodeTicker,
                        objContent.GetReconection.DesTickler,
                        objContent.GetReconection.Users,
                        objContent.GetReconection.CodCustomer,
                        objContent.GetReconection.CoState,
                        objContent.GetReconection.Reason,
                        objContent.GetReconection.Telephone,
                        objContent.GetReconection.TypeService,
                        objContent.GetReconection.CoSer,
                        objContent.GetReconection.TypeRegister,
                        objContent.GetReconection.UserSystem,
                        objContent.GetReconection.UserApp,
                        objContent.GetReconection.EmailUserApp,
                        objContent.GetReconection.DesCoser,
                        objContent.GetReconection.CodeInteraction,
                        objContent.GetReconection.NroAcount,
                        listaOpcionalRequest,
                        out msgRespta,
                        out listaOpcionalResponse
                    );

                });

            if (result.Equals(ConstantsHFC.strCero))
            {
                model.BoolResult = true;
            }
            else
            {
                model.BoolResult = false;
            }
            model.IdTransaction = idTransaction;
            model.Result = msgRespta;
            return model;
        }

        public static EntitiesFixed.PostSuspensionLte.PostSuspensionLteResponse EjecutaSuspensionDeServicioLte(EntitiesFixed.PostSuspensionLte.PostSuspensionLteRequest objRequest)
        {
            var objResponse = new EntitiesFixed.PostSuspensionLte.PostSuspensionLteResponse();
            try
            {
                string mensajeRespuesta = string.Empty;

                ActDesactLTE.parametrosAuditRequest objAuditReq = new ActDesactLTE.parametrosAuditRequest();
                ActDesactLTE.parametrosAuditResponse objAuditRes = new ActDesactLTE.parametrosAuditResponse();

                ActDesactLTE.parametrosRequestObjetoRequestOpcional[] listaOpcionalRequest = new ActDesactLTE.parametrosRequestObjetoRequestOpcional[0];
                ActDesactLTE.parametrosResponseObjetoResponseOpcional[] listaOpcionalResponse = new ActDesactLTE.parametrosResponseObjetoResponseOpcional[0];

                //WSServicioLTE.ServiciosLTEService objServiceLTE = new WSServicioLTE.ServiciosLTEService();
                ActDesactLTE.ejecutarSuspencionServicioRequest objReq = new ActDesactLTE.ejecutarSuspencionServicioRequest();
                ActDesactLTE.ejecutarSuspencionServicioResponse objRes = new ActDesactLTE.ejecutarSuspencionServicioResponse();

                //objServiceLTE.Url = ConfigurationManager.AppSettings["strWebserviceServiciosLTE"];
                //objServiceLTE.Credentials = System.Net.CredentialCache.DefaultCredentials;

                var objContent = objRequest.Suspension;

                objAuditReq.idTransaccion = string.Empty;
                objAuditReq.ipAplicacion = objContent.ipAplicacion;
                objAuditReq.nombreAplicacion = objContent.nombreAplicacion;
                objAuditReq.usuarioAplicacion = objContent.usuarioApp;

                objReq.auditRequest = objAuditReq;
                objReq.codCliente = objContent.codCliente;
                objReq.codigoInteraccion = objContent.codigoInteraccion;
                objReq.coId = objContent.coId;
                objReq.coSer = objContent.coSer;
                objReq.coState = objContent.coState;
                objReq.fideliza = objContent.fideliza;
                objReq.desCoSer = objContent.desCoSer;
                objReq.nroDias = objContent.nroDias;
                objReq.emailUsuarioApp = objContent.emailUsuarioApp;
                objReq.fechaProgramacion = objContent.fechaProgramacion.ToString("yyyy-MM-dd");
                objReq.fechaSuspension = objContent.fechaSuspension.ToString("yyyy-MM-dd");
                objReq.flagAccion = objContent.flagAccion;
                objReq.codigoInteraccion = objContent.codigoInteraccion;
                objReq.listaRequestOpcional = listaOpcionalRequest;
                objReq.nroCuenta = objContent.nroCuenta;
                objReq.reason = objContent.reason;
                objReq.telefono = objContent.telefono;
                objReq.ticklerCode = objContent.ticklerCode;
                objReq.tipoRegistro = objContent.tipoRegistro;
                objReq.tipoServicio = objContent.tipoServicio;
                objReq.usuario = objContent.usuario;
                objReq.usuarioApp = objContent.usuarioApp;
                objReq.usuarioSistema = objContent.usuarioSistema;


                var result = Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return ServiceConfiguration.SiacFixedActivationDesactivacionLte.ejecutarSuspencionServicio(objReq);
                });

                objAuditRes = objRes.auditResponse;

                if (objAuditRes.codigoRespuesta.Equals("0"))
                {
                    objResponse.ResponseStatus = true;
                }
                else
                {
                    objResponse.ResponseStatus = false;
                }

            }
            catch (Exception ex)
            {
                //log.Info(String.Format("Error: {0}", ex.Message));
                objResponse.ResponseStatus = false;
            }

            return objResponse;
        }

        public static EntitiesFixed.PostReconexionLte.ReconexionLteResponse EjecutaReconexionDeServicioLte(EntitiesFixed.PostReconexionLte.ReconexionLteRequest objRequest)
        {
            var objResponse = new EntitiesFixed.PostReconexionLte.ReconexionLteResponse();
            try
            {

                string mensajeRespuesta = string.Empty;

                ActDesactLTE.parametrosAuditRequest objAuditReq = new ActDesactLTE.parametrosAuditRequest();
                ActDesactLTE.parametrosAuditResponse objAuditRes = new ActDesactLTE.parametrosAuditResponse();

                ActDesactLTE.parametrosRequestObjetoRequestOpcional[] listaOpcionalRequest = new ActDesactLTE.parametrosRequestObjetoRequestOpcional[0];
                ActDesactLTE.parametrosResponseObjetoResponseOpcional[] listaOpcionalResponse = new ActDesactLTE.parametrosResponseObjetoResponseOpcional[0];

                //WSServicioLTE.ServiciosLTEService objServiceLTE = new WSServicioLTE.ServiciosLTEService();
                ActDesactLTE.ejecutarReconexionServicioRequest objReq = new ActDesactLTE.ejecutarReconexionServicioRequest();
                ActDesactLTE.ejecutarReconexionServicioResponse objRes = new ActDesactLTE.ejecutarReconexionServicioResponse();

                //objServiceLTE.Url = ConfigurationManager.AppSettings["strWebserviceServiciosLTE"];
                //objServiceLTE.Credentials = System.Net.CredentialCache.DefaultCredentials;

                var objContent = objRequest.ReconexionLte;

                objAuditReq.idTransaccion = string.Empty;
                objAuditReq.ipAplicacion = objContent.codigoAplicacion;
                objAuditReq.nombreAplicacion = objContent.nombreAplicacion;
                objAuditReq.usuarioAplicacion = objContent.usuarioApp;

                objReq.auditRequest = objAuditReq;
                objReq.codCliente = objContent.codCliente;
                objReq.codigoInteraccion = objContent.codigoInteraccion;
                objReq.coId = objContent.coId;
                objReq.coSer = objContent.coSer;
                objReq.coState = objContent.coState;
                objReq.desCoSer = objContent.desCoSer;
                objReq.emailUsuarioApp = objContent.emailUsuarioApp;
                objReq.fechaProgramacion = objContent.fechaProgramacion.ToString("yyyy-MM-dd");
                objReq.flagAccion = objContent.flagAccion;
                objReq.interaccion = objContent.codigoInteraccion;
                objReq.listaRequestOpcional = listaOpcionalRequest;
                objReq.montoOcc = objContent.montoOCC;
                objReq.nroCuenta = objContent.nroCuenta;
                objReq.reason = objContent.reason;
                objReq.telefono = objContent.telefono;
                objReq.ticklerCode = objContent.ticklerCode;
                objReq.tipoRegistro = objContent.tipoRegistro;
                objReq.tipoServicio = objContent.tipoServicio;
                objReq.usuario = objContent.usuario;
                objReq.usuarioApp = objContent.usuarioApp;
                objReq.usuarioSistema = objContent.usuarioSistema;


                objRes = Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return ServiceConfiguration.SiacFixedActivationDesactivacionLte.ejecutarReconexionServicio(objReq);
                });

                objAuditRes = objRes.auditResponse;

                if (objAuditRes.codigoRespuesta.Equals("0"))
                {
                    objResponse.ResponseStatus = true;
                }
                else
                {
                    objResponse.ResponseStatus = false;
                }
            }
            catch (Exception ex)
            {
                //log.Info(String.Format("Error: {0}", ex.Message));
                objResponse.ResponseStatus = false;
            }

            return objResponse;
        }

    }
}
