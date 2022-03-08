using Claro.SIACU.Data.Transac.Service.Configuration;
using Claro.SIACU.Entity.Transac.Service.Fixed;
using Claro.SIACU.Transac.Service;
using System;
using System.Data;
using Claro.Data;
using System.Collections.Generic;
using Claro.SIACU.Entity.Transac.Service.Fixed.DeleteProgramTask;
using Claro.Web;
using WSServicioLTE = Claro.SIACU.ProxyService.Transac.Service.SIACFixed.ServAddLTE;
using ConstantsHFC = Claro.SIACU.Transac.Service.Constants;
using EntitiesFixed = Claro.SIACU.Entity.Transac.Service.Fixed;

namespace Claro.SIACU.Data.Transac.Service.Fixed
{
    public class ProgramTask
    {
        public static List<ScheduledTransaction> GetListScheduledTransactions(string strIdSession, string strTransaction,
            string strIdTransactionTask ,string strApplicationCode,string strApplicationName,string strUserApp,string strServiCoId,
            string strStartDate,string strEndDate,string strServiceState,string strAdvisor,string strAccount,
            string strTransactionType,string strCodeInteraction,string strNameCacdac, out bool correctProcess)
        {
            List<ScheduledTransaction> list = new List<ScheduledTransaction>();

            WSServicioLTE.parametrosAuditRequest objAuditReq = new WSServicioLTE.parametrosAuditRequest();
            WSServicioLTE.parametrosAuditResponse objAuditRes = new WSServicioLTE.parametrosAuditResponse();

            WSServicioLTE.parametrosRequestObjetoRequestOpcional[] listaOpcionalRequest = new WSServicioLTE.parametrosRequestObjetoRequestOpcional[0];
            WSServicioLTE.parametrosResponseObjetoResponseOpcional[] listaOpcionalResponse = new WSServicioLTE.parametrosResponseObjetoResponseOpcional[0];

        
            WSServicioLTE.tareasProgramadasConsultarRequest objReq = new WSServicioLTE.tareasProgramadasConsultarRequest();
            WSServicioLTE.tareasProgramadasConsultarResponse objRes = new WSServicioLTE.tareasProgramadasConsultarResponse();

            objAuditReq.idTransaccion = strIdTransactionTask;
            objAuditReq.ipAplicacion = strApplicationCode;
            objAuditReq.nombreAplicacion = strApplicationName;
            objAuditReq.usuarioAplicacion = strUserApp;
             
            objReq.auditRequest = objAuditReq;
            objReq.servicoid = strServiCoId;
            if (strStartDate != string.Empty && strEndDate != string.Empty)
            {
                objReq.fechadesde = string.Format("{0:yyyy-MM-dd}", Convert.ToDate(strStartDate));
                objReq.fechahasta = string.Format("{0:yyyy-MM-dd}", Convert.ToDate(strEndDate));
            }
            else
            {
                objReq.fechadesde = string.Empty;
                objReq.fechahasta = string.Empty;
            }
            objReq.estado = strServiceState;
            objReq.asesor = strAdvisor;
            objReq.cuenta = strAccount;
            objReq.tipoTransaccion = strTransactionType;
            objReq.codInteraccion = strCodeInteraction;
            objReq.cadDac = strNameCacdac;

            objReq.listaRequestOpcional = listaOpcionalRequest;

            objRes = Web.Logging.ExecuteMethod(strIdSession, strTransaction, () =>
            {
                return ServiceConfiguration.SiacFixedActivationDesactivacionLte.tareasProgramadasConsultar(objReq);
            });

            if (objRes != null)
            {
                if (objRes.pcursor.Length > 0)
                {
                    for (int i = 0; i < objRes.pcursor.Length; i++)
                    {
                        ScheduledTransaction objServicioLinea = new ScheduledTransaction();
                        objServicioLinea.CO_ID = Functions.CheckStr(objRes.pcursor[i].co_id);
                        objServicioLinea.DESC_STATE = Functions.CheckStr(objRes.pcursor[i].desc_estado);
                        objServicioLinea.DESC_SERVICE = Functions.CheckStr(objRes.pcursor[i].desc_servi);
                        objServicioLinea.CUSTOMER_ID = Functions.CheckStr(objRes.pcursor[i].customer_id); //SD-794552 - RPB - PROY-20152.INC000000726842
                        objServicioLinea.SERVC_CO_SER = Functions.CheckStr(objRes.pcursor[i].servc_co_ser);
                        objServicioLinea.SERVC_CODE_INTERACTION = Functions.CheckStr(objRes.pcursor[i].servc_codigo_interaccion);
                        objServicioLinea.SERVC_DES_CO_SER = Functions.CheckStr(objRes.pcursor[i].servc_des_co_ser);
                        objServicioLinea.SERVC_ISBATCH = Functions.CheckStr(objRes.pcursor[i].servc_esbatch);
                        objServicioLinea.SERVC_STATE = Functions.CheckStr(objRes.pcursor[i].servc_estado);
                        objServicioLinea.SERVC_NUMBERACCOUNT = Functions.CheckStr(objRes.pcursor[i].servc_nrocuenta);
                        objServicioLinea.SERVC_POINTSALE = Functions.CheckStr(objRes.pcursor[i].servc_puntoventa);
                        objServicioLinea.SERVC_TYPE_REG = Functions.CheckStr(objRes.pcursor[i].servc_tipo_reg);
                        objServicioLinea.SERVC_TYPE_SERV = Functions.CheckStr(objRes.pcursor[i].servc_tipo_serv);
                        objServicioLinea.SERVD_DATE_EJEC = Functions.CheckStr(objRes.pcursor[i].servd_fecha_ejec);
                        objServicioLinea.SERVD_DATE_REG = Functions.CheckStr(objRes.pcursor[i].servd_fecha_reg);
                        objServicioLinea.SERVD_DATEPROG = Functions.CheckStr(objRes.pcursor[i].servd_fechaprog);
                        objServicioLinea.SERVI_COD = Functions.CheckStr(objRes.pcursor[i].servi_cod);
                        objServicioLinea.SERVV_COD_ERROR = Functions.CheckStr(objRes.pcursor[i].servv_cod_error);
                        objServicioLinea.SERVV_EMAIL_USER_APP = Functions.CheckStr(objRes.pcursor[i].servv_email_usuario_app);
                        objServicioLinea.SERVV_ID_BATCH = Functions.CheckStr(objRes.pcursor[i].servv_id_batch);
                        objServicioLinea.SERVV_ID_EAI_SW = Functions.CheckStr(objRes.pcursor[i].servv_id_eai_sw);
                        objServicioLinea.SERVV_MEN_ERROR = Functions.CheckStr(objRes.pcursor[i].servv_men_error);
                        objServicioLinea.SERVV_MSISDN = Functions.CheckStr(objRes.pcursor[i].servv_msisdn);
                        objServicioLinea.SERVV_USER_APLICATION = Functions.CheckStr(objRes.pcursor[i].servv_usuario_aplicacion);
                        objServicioLinea.SERVV_USER_SYSTEM = Functions.CheckStr(objRes.pcursor[i].servv_usuario_sistema);
                        objServicioLinea.SERVV_XMLENTRY = Functions.CheckStr(objRes.pcursor[i].servv_xmlentrada);
                        list.Add(objServicioLinea);
                    }
                }
            }
            objAuditRes = objRes.auditResponse;
               
            if (objAuditRes.codigoRespuesta.Equals(ConstantsHFC.strCero))
                correctProcess =true; 
            else
                correctProcess = false;

            return list;
        }

        public static bool DeleteProgramTask(DeleteProgramTaskRequest objContent)
        {
            try
            {
                string mensajeRespuesta = string.Empty;

                WSServicioLTE.parametrosAuditRequest objAuditReq = new WSServicioLTE.parametrosAuditRequest();
                WSServicioLTE.parametrosAuditResponse objAuditRes = new WSServicioLTE.parametrosAuditResponse();

                WSServicioLTE.parametrosRequestObjetoRequestOpcional[] listaOpcionalRequest = new WSServicioLTE.parametrosRequestObjetoRequestOpcional[0];
                WSServicioLTE.parametrosResponseObjetoResponseOpcional[] listaOpcionalResponse = new WSServicioLTE.parametrosResponseObjetoResponseOpcional[0];

                WSServicioLTE.tareasProgramadasEliminarRequest objReq = new WSServicioLTE.tareasProgramadasEliminarRequest();
                WSServicioLTE.tareasProgramadasEliminarResponse objRes = new WSServicioLTE.tareasProgramadasEliminarResponse();

                objAuditReq.idTransaccion = string.Empty;
                objAuditReq.ipAplicacion = objContent.CodigoAplicacion;
                objAuditReq.nombreAplicacion = objContent.NombreAplicacion;
                objAuditReq.usuarioAplicacion = objContent.UsuarioApp;

                objReq.auditRequest = objAuditReq;

                objReq.serviCod = objContent.ServiCod;
                objReq.codId = objContent.ConId;
                objReq.servcEstado = objContent.ServiEstado;

                objReq.listaRequestOpcional = listaOpcionalRequest;

                objRes = Web.Logging.ExecuteMethod(objContent.StrIdSession, objContent.StrTransaction, () =>
                {
                    return ServiceConfiguration.SiacFixedActivationDesactivacionLte.tareasProgramadasEliminar(objReq);
                });

                if (objRes != null)
                {
                    objAuditRes = objRes.auditResponse;
                    Logging.Info(objContent.StrIdSession, objContent.StrTransaction, "Parámetros de Salida -> Mensaje respuesta: " + objAuditRes.mensajeRespuesta);
                    if (objAuditRes.codigoRespuesta.Equals(ConstantsHFC.strCero))
                    {
                        return true;
                    }
                }           
               
                return false;
            }
            catch (Exception ex)
            {
                Logging.Info(objContent.StrIdSession, objContent.StrTransaction, "Error: " + ex.Message);
                return false;
            }
        }

        public static bool DeleteProgramTaskHfc(string strIdSession, string strTransaction, string vstrServCod, string vstrCodId, string vstrServCEstado)
        {
            DbParameter[] parameters = 
            {
                new DbParameter("p_servi_cod", DbType.String, ParameterDirection.Input, vstrServCod),	
                new DbParameter("p_cod_id", DbType.String, ParameterDirection.Input, vstrCodId),	
                new DbParameter("p_servc_estado", DbType.String, 255, ParameterDirection.Output),
                new DbParameter("p_cod_error", DbType.Int32, ParameterDirection.Output),	
                new DbParameter("p_men_error", DbType.String, 255, ParameterDirection.Output)
            };

            bool salida;

            try
            {
                Web.Logging.ExecuteMethod(strIdSession, strTransaction, () =>
                {
                    DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_EAIAVM, DbCommandConfiguration.SIACU_HFCPOST_SP_BORRAR_PROGRAMACION, parameters);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
            }
            finally
            {
                var resultInt = Functions.CheckInt(parameters[3].Value.ToString());
                salida = resultInt == 0; 
            }

            return salida;
        }

        public static bool UpdateProgramTaskHfc(string strIdSession, string strTransaction, string vstrServCod, string vstrCodId, string vstrCustomerId, string vstrServFProg, string vstrServIdBat, string vstrServFEjec, string vstrServCEstado, string vstrServMenErr, string vstrServCodErr)
        {
            DbParameter[] parameters = 
            {
                new DbParameter("p_servi_cod", DbType.String,ParameterDirection.Input, vstrServCod),
                new DbParameter("p_cod_id", DbType.String,ParameterDirection.Input, vstrCodId),
                new DbParameter("p_customer_id", DbType.String,ParameterDirection.Input, vstrCustomerId),
                new DbParameter("p_servd_fecha_prog", DbType.String, ParameterDirection.Input, vstrServFProg),
                new DbParameter("p_servv_id_batch", DbType.String, ParameterDirection.Input, vstrServIdBat),
                new DbParameter("p_servd_fecha_ejec", DbType.String, ParameterDirection.Input, vstrServFEjec),
                new DbParameter("p_servc_estado", DbType.String, ParameterDirection.Input, vstrServCEstado),
                new DbParameter("p_servv_men_error", DbType.String, ParameterDirection.Input, vstrServMenErr),
                new DbParameter("p_servv_cod_error", DbType.String, ParameterDirection.Input, vstrServCodErr),
                new DbParameter("p_cod_error", DbType.Int32, 255, ParameterDirection.Output),
                new DbParameter("p_men_error", DbType.String, 255, ParameterDirection.Output)
            };

            bool salida;

            try
            {
                Web.Logging.ExecuteMethod(strIdSession, strTransaction, () =>
                {
                    DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_EAIAVM, DbCommandConfiguration.SIACU_HFCPOST_SP_ACTUALIZA_PROGRAMACION, parameters);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
            }
            finally
            {
                var resultInt = Functions.CheckInt(parameters[9].Value.ToString());
                salida = resultInt == 0;
            }

            return salida;
        }

        public static bool UpdateProgramTaskLte(string strIdSession, string strTransaction, string codigoAplicacion, string nombreAplicacion, string usuarioApp, string serviCod, string conId, string serviEstado, string fechaProg)
        {
            try
            {
                string mensajeRespuesta = string.Empty;
                WSServicioLTE.parametrosAuditRequest objAuditReq = new WSServicioLTE.parametrosAuditRequest();
                WSServicioLTE.parametrosAuditResponse objAuditRes = new WSServicioLTE.parametrosAuditResponse();

                WSServicioLTE.parametrosRequestObjetoRequestOpcional[] listaOpcionalRequest = new WSServicioLTE.parametrosRequestObjetoRequestOpcional[0];
                WSServicioLTE.parametrosResponseObjetoResponseOpcional[] listaOpcionalResponse = new WSServicioLTE.parametrosResponseObjetoResponseOpcional[0];

                WSServicioLTE.tareasProgramadasEditarRequest objReq = new WSServicioLTE.tareasProgramadasEditarRequest();
                WSServicioLTE.tareasProgramadasEditarResponse objRes = new WSServicioLTE.tareasProgramadasEditarResponse();

                objAuditReq.idTransaccion = strTransaction;
                objAuditReq.ipAplicacion = codigoAplicacion;
                objAuditReq.nombreAplicacion = nombreAplicacion;
                objAuditReq.usuarioAplicacion = usuarioApp;

                objReq.auditRequest = objAuditReq;

                objReq.serviCod = serviCod;
                objReq.codId = conId;
                objReq.servcEstado = serviEstado;
                objReq.servdFechaProg = fechaProg;

                objReq.listaRequestOpcional = listaOpcionalRequest;

                objRes = Logging.ExecuteMethod(strIdSession, strTransaction, () =>
                {
                    return ServiceConfiguration.SiacFixedActivationDesactivacionLte.tareasProgramadasEditar(objReq);
                });

                objAuditRes = objRes.auditResponse;

                Logging.Info(strIdSession, strTransaction, "Parámetros de Salida -> Mensaje respuesta: " + objAuditRes.mensajeRespuesta);

                if (objAuditRes.codigoRespuesta.Equals(ConstantsHFC.strCero))
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Logging.Info(strIdSession, strTransaction, String.Format("Error: {0}", ex.Message));
                return false;
            }
        }

    }
}
