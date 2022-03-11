using Claro.Data;
using Claro.SIACU.Data.Transac.Service.Configuration;
using Claro.SIACU.Entity.Transac.Service.Fixed;
using Claro.SIACU.ProxyService.Transac.Service.CambioPlanFija;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Constant = Claro.SIACU.Transac.Service.Constants;

namespace Claro.SIACU.Data.Transac.Service.Fixed
{
    public class PlanMigrationHfc
    {
        /// <summary>
        /// Método que obtiene una lista de planes
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">id de transacción</param>
        /// <param name="strPlano"></param>
        /// <param name="strOferta"></param>
        /// <returns>retorna una lista de planes</returns>
        public static List<Entity.Transac.Service.Fixed.ProductPlan> GetNewPlans(string strIdSession, string strTransaction, string strPlano, string strOferta, string strTipoProducto)
        {
            Claro.Web.Logging.Info("HFCGetNewPlans", "HFCPlanMigration", "Entro GetNewPlans");
            Claro.Web.Logging.Info("HFCGetNewPlans", "HFCPlanMigration", "los parametros que recibe el metodo son: strIdSession:" + strIdSession + ";strTransaction:" + strTransaction + ";strPlano:" + strPlano + ";strOferta:" + strOferta + ";strTipoProducto:" + strTipoProducto); 
            List<Entity.Transac.Service.Fixed.ProductPlan> list = null;
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_CONSULTA", DbType.Object, ParameterDirection.Output),
                new DbParameter("P_PLANO", DbType.String,255, ParameterDirection.Input, strPlano),
                new DbParameter("P_OFERTA", DbType.String,255, ParameterDirection.Input,strOferta),
                new DbParameter("p_tipo_producto",DbType.String,255,ParameterDirection.Input,strTipoProducto)
            };

            try
            {
                list = DbFactory.ExecuteReader<List<Entity.Transac.Service.Fixed.ProductPlan>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_PVU, DbCommandConfiguration.SIACU_SP_CON_PLAN, parameters);
            }
            catch (Exception ex)
            {
                list = null;
                Claro.Web.Logging.Error(strIdSession, strTransaction, Claro.SIACU.Transac.Service.Functions.GetExceptionMessage(ex));              
            }
            Claro.Web.Logging.Info(strIdSession, strTransaction, JsonConvert.SerializeObject(list));
            Claro.Web.Logging.Info("HFCGetNewPlans", "HFCPlanMigration", "Finalizó GetNewPlans");
               

            return list;
        }
        public static List<Entity.Transac.Service.Fixed.ServiceByPlan> GetServicesByPlan(string strIdSession, string strTransaction, string idplan, string strTipoProducto)
        {
            Claro.Web.Logging.Info("HFCGetServicesByPlan", "HFCPlanMigration", "Entro GetServicesByPlan");
            Claro.Web.Logging.Info("HFCGetServicesByPlan", "HFCPlanMigration", "los parametros que recibe el metodo son: strIdSession:" + strIdSession + ";strTransaction:" + strTransaction + ";strTipoProducto:" + strTipoProducto);
            List<Entity.Transac.Service.Fixed.ServiceByPlan> list = null;

            DbParameter[] parameters = new DbParameter[] {                
                new DbParameter("P_PLAN", DbType.String,255, ParameterDirection.Input,idplan),
                new DbParameter("p_tipo_producto",DbType.String,255,ParameterDirection.Input,strTipoProducto),
                new DbParameter("P_CURSOR", DbType.Object, ParameterDirection.Output)                
            };

            try
            {
                list = DbFactory.ExecuteReader<List<Entity.Transac.Service.Fixed.ServiceByPlan>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_PVU, DbCommandConfiguration.SIACU_SP_CON_PLAN_SERVICIO, parameters);
            }
            catch (Exception ex )
            {
                list = null;
                Claro.Web.Logging.Error(strIdSession, strTransaction, Claro.SIACU.Transac.Service.Functions.GetExceptionMessage(ex));
            }
            Claro.Web.Logging.Info(strIdSession, strTransaction, JsonConvert.SerializeObject(list));
            Claro.Web.Logging.Info("HFCGetServicesByPlan", "HFCPlanMigration", "Finalizó GetServicesByPlan");

            return list;
        }
        public static List<Entity.Transac.Service.Fixed.ServiceByPlan> GetInformationLetter2(string strIdSession, string strTransaction, string idplan, string coid)
        {
            Claro.Web.Logging.Info("HFCGetInformationLetter2", "HFCPlanMigration", "Entro GetInformationLetter2");
            Claro.Web.Logging.Info("HFCGetInformationLetter2", "HFCPlanMigration", "los parametros que recibe el metodo son: strIdSession:" + strIdSession + ";strTransaction:" + strTransaction + ";idplan:" + idplan + ";coid:" + coid); 
            List<Entity.Transac.Service.Fixed.ServiceByPlan> list = null;

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_CONSULTA", DbType.Object, ParameterDirection.Output),
                new DbParameter("P_PLANO", DbType.String,255, ParameterDirection.Input, idplan)
                
            };

            try
            {
                string status = "";
                status = GetDecoderServiceStatus(strIdSession, strTransaction, int.Parse(coid), Claro.Constants.NumberSeventyThree);
                if (status == "O" || status == "A" || status == "S")
                { }
                else
                {

                    list = DbFactory.ExecuteReader<List<Entity.Transac.Service.Fixed.ServiceByPlan>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_PVU, DbCommandConfiguration.SIACU_SP_CON_PLAN, parameters);
                    
                }
            }
            catch (Exception ex)
            {
                list = null;
                Claro.Web.Logging.Error(strIdSession, strTransaction, Claro.SIACU.Transac.Service.Functions.GetExceptionMessage(ex));  
               
            }
            Claro.Web.Logging.Info(strIdSession, strTransaction, JsonConvert.SerializeObject(list));
            Claro.Web.Logging.Info("HFCGetInformationLetter2", "HFCPlanMigration", "Finalizó GetInformationLetter2");
            return list;
            
        }


        public static string GetDecoderServiceStatus(string strIdSession, string strTransaction, int intContract, int intSnCode)
        {

            Claro.Web.Logging.Info("HFCGetDecoderServiceStatus", "HFCPlanMigration", "Entro GetDecoderServiceStatus");
            Claro.Web.Logging.Info("HFCGetDecoderServiceStatus", "HFCPlanMigration", "los parametros que recibe el metodo son: strIdSession:" + strIdSession + ";strTransaction:" + strTransaction + ";intContract:" + intContract + ";intSnCode" + intSnCode);

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_CO_ID", DbType.Int64, ParameterDirection.Input, intContract),
                new DbParameter("P_SNCODE", DbType.Int64, ParameterDirection.Input, intSnCode),
                new DbParameter("V_ESTADO", DbType.String, ParameterDirection.ReturnValue)
            };

            try
            {
                DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_TFUN015_ESTADO_SERVICIO, parameters);

            }
            catch (Exception ex)
            {

                Claro.Web.Logging.Error(strIdSession, strTransaction, Claro.SIACU.Transac.Service.Functions.GetExceptionMessage(ex));
            }
            //Claro.Web.Logging.Info(strIdSession, strTransaction, JsonConvert.SerializeObject(parameters).ToString());
            Claro.Web.Logging.Info("HFCGetDecoderServiceStatus", "HFCPlanMigration", "Finalizó GetDecoderServiceStatus");
            return parameters[2].Value.ToString();


        }

        public static List<Entity.Transac.Service.Fixed.Carrier> GetCarrierList(string strIdSession, string strTransaction)
        {
            Claro.Web.Logging.Info("HFCGetCarrierList", "HFCPlanMigration", "Entro GetCarrierList");
            Claro.Web.Logging.Info("HFCGetCarrierList", "HFCPlanMigration", "los parametros que recibe el metodo son: strIdSession:" + strIdSession + ";strTransaction:" + strTransaction);
            List<Entity.Transac.Service.Fixed.Carrier> list = null;

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("AC_PROB_CUR", DbType.Object, ParameterDirection.Output)
                
            };
            try
            {
                list = DbFactory.ExecuteReader<List<Entity.Transac.Service.Fixed.Carrier>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_P_LISTA_OPERADOR, parameters);
            }
            catch (Exception ex)
            {
                list = null;
                Claro.Web.Logging.Error(strIdSession, strTransaction, Claro.SIACU.Transac.Service.Functions.GetExceptionMessage(ex));  
            }
            Claro.Web.Logging.Info(strIdSession, strTransaction, JsonConvert.SerializeObject(list));
            Claro.Web.Logging.Info("HFCGetCarrierList", "HFCPlanMigration", "Finalizó GetCarrierList");

            return list;
        }

        public static List<Entity.Transac.Service.Fixed.ServiceByInteraction> GetServicesByInteraction(string strIdSession, string strTransaction, string idInteraccion)
        {
            Claro.Web.Logging.Info("HFCGetServicesByInteraction", "HFCPlanMigration", "Entro GetServicesByInteraction");
            Claro.Web.Logging.Info("HFCGetServicesByInteraction", "HFCPlanMigration", "los parametros que recibe el metodo son: strIdSession:" + strIdSession + ";strTransaction:" + strTransaction + ";idInteraccion:" + idInteraccion);
            List<Entity.Transac.Service.Fixed.ServiceByInteraction> list = null;

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("CURSOR_SALIDA", DbType.Object, ParameterDirection.Output),
                new DbParameter("P_CODINTERAC", DbType.String,255, ParameterDirection.Input, idInteraccion)
                
            };
            try
            {
                list = DbFactory.ExecuteReader<List<Entity.Transac.Service.Fixed.ServiceByInteraction>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_CLARIFY, DbCommandConfiguration.SIACU_SP_QUERY_INTER_SERV_MP, parameters);
            }
            catch (Exception ex)
            {
                list = null;
                Claro.Web.Logging.Error(strIdSession, strTransaction, Claro.SIACU.Transac.Service.Functions.GetExceptionMessage(ex));
              
            }
            Claro.Web.Logging.Info(strIdSession, strTransaction, JsonConvert.SerializeObject(list));
            Claro.Web.Logging.Info("HFCGetServicesByInteraction", "HFCPlanMigration", "Finalizó GetServicesByInteraction");
            

            return list;
        }
        public static List<Entity.Transac.Service.Fixed.TransactionRule> GetTransactionRuleList(string strIdSession, string strTransaction, string SubClase)
        {
            Claro.Web.Logging.Info("HFCGetTransactionRuleList", "HFCPlanMigration", "Entro GetTransactionRuleList");
            Claro.Web.Logging.Info("HFCGetTransactionRuleList", "HFCPlanMigration", "los parametros que recibe el metodo son: strIdSession:" + strIdSession + ";strTransaction:" + strTransaction + ";SubClase:" + SubClase);
            List<Entity.Transac.Service.Fixed.TransactionRule> list = null;

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_SUSCLASE", DbType.String,255, ParameterDirection.Input,SubClase),
                new DbParameter("P_CURSOR", DbType.Object, ParameterDirection.Output)                
            };
            try
            {
                list = DbFactory.ExecuteReader<List<Entity.Transac.Service.Fixed.TransactionRule>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_CONSULTAR_REGLAS_ATENCION, parameters);
            }
            catch (Exception ex)
            {
                list = null;
                Claro.Web.Logging.Error(strIdSession, strTransaction, Claro.SIACU.Transac.Service.Functions.GetExceptionMessage(ex));
            }
            Claro.Web.Logging.Info(strIdSession, strTransaction, JsonConvert.SerializeObject(list));
            Claro.Web.Logging.Info("HFCGetTransactionRuleList", "HFCPlanMigration", "Finalizó GetTransactionRuleList");
            return list;
        }

        public static List<Entity.Transac.Service.Fixed.ServiceByPlan> GetListServicesByPlan(string strIdSession, string strTransaction, string idplan)
        {
            Claro.Web.Logging.Info("HFCGetListServicesByPlan", "HFCPlanMigration", "Entro GetListServicesByPlan");
            Claro.Web.Logging.Info("HFCGetListServicesByPlan", "HFCPlanMigration", "los parametros que recibe el metodo son: strIdSession:" + strIdSession + ";strTransaction:" + strTransaction + ";idplan:" + idplan);
            
            List<Entity.Transac.Service.Fixed.ServiceByPlan> list = null;

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_CURSOR", DbType.Object, ParameterDirection.Output),
                new DbParameter("P_PLAN", DbType.String,255, ParameterDirection.Input, idplan)                
            };
            try
            {
                 list = DbFactory.ExecuteReader<List<Entity.Transac.Service.Fixed.ServiceByPlan>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_PVU, DbCommandConfiguration.SIACU_SP_CON_PLAN_SERVICIO, parameters);
            }
            catch (Exception ex)
            {
                list = null;
                Claro.Web.Logging.Error(strIdSession, strTransaction, Claro.SIACU.Transac.Service.Functions.GetExceptionMessage(ex));
              
            }
            Claro.Web.Logging.Info(strIdSession, strTransaction, JsonConvert.SerializeObject(list));
            Claro.Web.Logging.Info("HFCGetListServicesByPlan", "HFCPlanMigration", "Finalizó GetListServicesByPlan");

            return list;
        }

        public static List<Entity.Transac.Service.Fixed.JobType> GetJobTypes(string strIdSession, string strTransaction, int vintTipoTransaccion)
        {
            Claro.Web.Logging.Info("HFCGetJobTypes", "HFCPlanMigration", "Entro GetJobTypes");
            Claro.Web.Logging.Info("HFCGetJobTypes", "HFCPlanMigration", "los parametros que recibe el metodo son: strIdSession:" + strIdSession + ";strTransaction:" + strTransaction + ";vintTipoTransaccion:" + vintTipoTransaccion);
            List<Entity.Transac.Service.Fixed.JobType> list = null;

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("p_tipo", DbType.Int32,22, ParameterDirection.Input, vintTipoTransaccion),
            new DbParameter("srv_tipra", DbType.Object, ParameterDirection.Output)
            };
            try
            {
                list = DbFactory.ExecuteReader<List<Entity.Transac.Service.Fixed.JobType>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_SP_CONSULTA_TIPTRA, parameters);
            }
            catch (Exception ex)
            {
                list = null;
                Claro.Web.Logging.Error(strIdSession, strTransaction, Claro.SIACU.Transac.Service.Functions.GetExceptionMessage(ex));
            }
            Claro.Web.Logging.Info(strIdSession, strTransaction, JsonConvert.SerializeObject(list));
            Claro.Web.Logging.Info("HFCGetJobTypes", "HFCPlanMigration", "Finalizó GetJobTypes");
            return list;
        }


        public static ETAFlow ETAFlowValidate(string strIdSession, string strTransaction, string as_origen, string av_idplano, string av_ubigeo, int an_tiptra,
                                     string an_tipsrv)
        {
            Claro.Web.Logging.Info("HFCETAFlowValidate", "HFCPlanMigration", "Entro ETAFlowValidate");
            Claro.Web.Logging.Info("HFCETAFlowValidate", "HFCPlanMigration", "los parametros que recibe el metodo son: strIdSession:" + strIdSession + ";strTransaction:" + strTransaction + ";as_origen:" + as_origen + ";av_idplano:" + av_idplano + ";av_ubigeo:" + av_ubigeo + ";an_tiptra:" + an_tiptra);

            ETAFlow oEtaFlow;
            string as_codzona = "";
            int an_indica = 1;
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("as_origen", DbType.String, ParameterDirection.Input,as_origen),
                new DbParameter("av_idplano", DbType.String,255, ParameterDirection.Input, av_idplano),
                new DbParameter("av_ubigeo", DbType.String,255, ParameterDirection.Input, av_ubigeo),
                new DbParameter("an_tiptra", DbType.Int32,20, ParameterDirection.Input, an_tiptra),
                new DbParameter("an_tipsrv", DbType.String,255, ParameterDirection.Input, an_tipsrv),
                new DbParameter("as_codzona", DbType.String,255, ParameterDirection.Output,as_codzona),
                new DbParameter("an_indica", DbType.Int32,20, ParameterDirection.Output,an_indica)
            };
            try
            {
                DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_SP_VALIDA_FLUJO_ZONA_ADC, parameters);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransaction, Claro.SIACU.Transac.Service.Functions.GetExceptionMessage(ex));  
                return new  ETAFlow{
                an_indica=0,
                as_codzona=string.Empty
                };
             
            }
            Claro.Web.Logging.Info(strIdSession, strTransaction, JsonConvert.SerializeObject(parameters));
            Claro.Web.Logging.Info("HFCETAFlowValidate", "HFCPlanMigration", "Finalizó ETAFlowValidate");
            return new ETAFlow
            {
                an_indica = Convert.ToInt(parameters[6].Value.ToString()),
                as_codzona = Convert.ToString(parameters[5].Value)
            };
        }

        public static List<Entity.Transac.Service.Fixed.OrderType> GetOrderType(string strIdSession, string strTransaction, string vintTipoTra)
        {
            Claro.Web.Logging.Info("HFCGetOrderType", "HFCPlanMigration", "Entro GetOrderType");
            Claro.Web.Logging.Info("HFCGetOrderType", "HFCPlanMigration", "los parametros que recibe el metodo son: strIdSession:" + strIdSession + ";strTransaction:" + strTransaction + ";vintTipoTra:" + vintTipoTra); 
            List<Entity.Transac.Service.Fixed.OrderType> list = null;

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("vIdtiptra", DbType.String,255, ParameterDirection.Input, vintTipoTra),
                                new DbParameter("cur_tipo", DbType.Object, ParameterDirection.Output)
            };
            try
            {
                list = DbFactory.ExecuteReader<List<Entity.Transac.Service.Fixed.OrderType>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_SP_OBTIENE_TIPO_ORDEN_TIPTRA, parameters);
            }
            catch (Exception ex)
            {
                list = null;
                Claro.Web.Logging.Error(strIdSession, strTransaction, Claro.SIACU.Transac.Service.Functions.GetExceptionMessage(ex));  
               
            }
            Claro.Web.Logging.Info(strIdSession, strTransaction, JsonConvert.SerializeObject(list));
            Claro.Web.Logging.Info("HFCGetOrderType", "HFCPlanMigration", "Finalizó GetOrderType");
            return list;
        }


        public static List<Entity.Transac.Service.Fixed.OrderSubType> GetOrderSubType(string strIdSession, string strTransaction, string vintTipoOrden)
        {
            Claro.Web.Logging.Info("HFCGetOrderSubType", "HFCPlanMigration", "Entro GetOrderSubType");
            Claro.Web.Logging.Info("HFCGetNewPlans", "HFCPlanMigration", "los parametros que recibe el metodo son: strIdSession:" + strIdSession + ";strTransaction:" + strTransaction + ";vintTipoOrden:" + vintTipoOrden ); 
            List<Entity.Transac.Service.Fixed.OrderSubType> list = null;
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("av_cod_tipo_orden", DbType.String,255, ParameterDirection.Input, vintTipoOrden),
                new DbParameter("p_cursor", DbType.Object, ParameterDirection.Output)
              
            };
            try
            {
                list = DbFactory.ExecuteReader<List<Entity.Transac.Service.Fixed.OrderSubType>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_SGASS_CONSULTA_SUBTIPORD, parameters);
            }
            catch (Exception ex)
            {
                list = null;
                Claro.Web.Logging.Error(strIdSession, strTransaction, Claro.SIACU.Transac.Service.Functions.GetExceptionMessage(ex));
            }
            Claro.Web.Logging.Info(strIdSession, strTransaction, JsonConvert.SerializeObject(list));
            Claro.Web.Logging.Info("HFCGetOrderSubType", "HFCPlanMigration", "Finalizó GetOrderSubType");

            return list;
        }
        public static Entity.Transac.Service.Fixed.ETAAuditoriaCapacity GetGroupCapacity(string strIdSession, string pIdTrasaccion, string pIP_APP, string pAPP, string pUsuario,
            DateTime[] vFechas, string[] vUbicacion, bool vCalcDur, bool vCalcDurEspec,
           bool vCalcTiempoViaje, bool vCalcTiempoViajeEspec, bool vCalcHabTrabajo, bool vCalcHabTrabajoEspec,
            bool vObtenerUbiZona, bool vObtenerUbiZonaEspec, string[] vEspacioTiempo, string[] vHabilidadTrabajo,
            BEETACampoActivity[] vCampoActividad, BEETAListaParamRequestOpcionalCapacity[] vListaCapReqOpc)
        {
            Claro.Web.Logging.Info("HFCGetGroupCapacity", "HFCPlanMigration", "Entro GetGroupCapacity");
            Claro.Web.Logging.Info("HFCGetGroupCapacity", "HFCPlanMigration", "los parametros que reciben el metodo son: strIdSession:" + strIdSession + ";pIdTrasaccion:" + pIdTrasaccion 
                + ";pIP_APP:" + pIP_APP + ";pAPP:" + pAPP + ";pUsuario:" + pUsuario+
                ";vFechas:" + vFechas.ToString() + ";vUbicacion:" + vUbicacion.ToString() +
                ";vCalcDur:" + vCalcDur.ToString() + ";vCalcDurEspec:" + vCalcDurEspec.ToString() + ";vCalcTiempoViaje:"
                + vCalcTiempoViaje.ToString() + ";vCalcTiempoViajeEspec:" + vCalcTiempoViajeEspec.ToString() + ";vCalcHabTrabajo:"+
                vCalcHabTrabajo.ToString()+";vHabilidadTrabajo:"+
                ";vCalcHabTrabajoEspec:" + vCalcHabTrabajoEspec.ToString() + ";vObtenerUbiZona:" +
                vObtenerUbiZona.ToString() + ";vObtenerUbiZonaEspec:" + vObtenerUbiZonaEspec.ToString() + ";vEspacioTiempo:" + vEspacioTiempo.ToString() + ";vHabilidadTrabajo:" + vHabilidadTrabajo.ToString()+
                ";vCampoActividad:" + vCampoActividad.ToString() + ";vListaCapReqOpc:" + vListaCapReqOpc.ToString()); 

            string vDurActivity;
            string vTiempoViajeActivity;
            ETAAuditoriaCapacity Resp = new ETAAuditoriaCapacity();

            //Claro.SIACU.ProxyService.Transac.Service.SIACHFC.GroupManagement.AuditResponse objResponseCuadrillas = new Claro.SIACU.ProxyService.Transac.Service.SIACHFC.GroupManagement.AuditResponse();

            //try
            //{
            //    Claro.SIACU.ProxyService.Transac.Service.SIACHFC.GroupManagement.ebsADMCUAD_CapacityService
            //     objServicioCuadrillas = new Claro.SIACU.ProxyService.Transac.Service.SIACHFC.GroupManagement.ebsADMCUAD_CapacityService();
            //    objServicioCuadrillas.Url = ConfigurationManager.AppSettings("strWebServEtaDirectWebService").ToString();
            //    objServicioCuadrillas.Credentials = System.Net.CredentialCache.DefaultCredentials;
            //    Claro.SIACU.ProxyService.Transac.Service.SIACHFC.GroupManagement.AuditRequest AuditRequestCuadrillas = new Claro.SIACU.ProxyService.Transac.Service.SIACHFC.GroupManagement.AuditRequest();
            //    AuditRequestCuadrillas.idTransaccion = pIdTrasaccion;
            //    AuditRequestCuadrillas.ipAplicacion = pIP_APP;
            //    AuditRequestCuadrillas.nombreAplicacion = pAPP;
            //    AuditRequestCuadrillas.usuarioAplicacion = pUsuario;

            //    //log.Info(string.Format("URL: {0}", objServicioCuadrillas.Url));
            //    Claro.SIACU.ProxyService.Transac.Service.SIACHFC.GroupManagement.campoActividadType[] ListaCapActiRequestCuadrillas = new Claro.SIACU.ProxyService.Transac.Service.SIACHFC.GroupManagement.campoActividadType[vCampoActividad.Length];


            //    //log.Info(string.Format("Parametros -->  idTransaccion: {0}, ipAplicacion: {1}, nombreAplicacion: {2}, usuarioAplicacion: {3},", pIdTrasaccion, pIP_APP, pAPP, pUsuario));
            //    String CantidadFechas = String.Empty;
            //    foreach (DateTime vF in vFechas)
            //    {
            //        CantidadFechas = CantidadFechas + ";" + vF.ToString();
            //    }
            //    //log.Info(string.Format(" Fecha: {0}, Ubicacion: {1}, calcularDuracion: {2}, ", CantidadFechas, vUbicacion[0], vCalcDur ? "true" : "false"));
            //    //log.Info(string.Format(" calcularTiempoViaje: {0}, calcularHabilidadTrabajo: {1}, determinarUbicacionPorZona: {2}, habilidadTrabajo: {3}", vCalcTiempoViaje ? "true" : "false", vCalcHabTrabajo ? "true" : "false", vObtenerUbiZona ? "true" : "false", vHabilidadTrabajo[0]));
            //    if (vCampoActividad.Length > 0)
            //    {
            //        //log.Info(string.Format("ListaCampoActivity(), Elementos: {0}", vCampoActividad.Length));
            //        int i = 0;
            //        foreach (BEETACampoActivity oCampAct in vCampoActividad)
            //        {
            //            Claro.SIACU.ProxyService.Transac.Service.SIACHFC.GroupManagement.campoActividadType CampoActividadRequestCuadrillas = new Claro.SIACU.ProxyService.Transac.Service.SIACHFC.GroupManagement.campoActividadType();
            //            CampoActividadRequestCuadrillas.nombre = oCampAct.Nombre;
            //            CampoActividadRequestCuadrillas.valor = oCampAct.Valor;
            //            ListaCapActiRequestCuadrillas[i] = CampoActividadRequestCuadrillas;
            //            //log.Info(string.Format("  objetoCampoActivity -->Indice: {0}, Nombre: {1}, Valor: {2}", i, CampoActividadRequestCuadrillas.nombre, CampoActividadRequestCuadrillas.valor));
            //            i++;
            //        }
            //    }
            //    else
            //    {
            //        //log.Info(string.Format("ListaCampoActivity(), Elementos: 0"));
            //        ListaCapActiRequestCuadrillas[0].nombre = "";
            //        ListaCapActiRequestCuadrillas[0].valor = "";
            //    }

            //    //Listado de Listado Parametro ListaParamRequest
            //    Claro.SIACU.ProxyService.Transac.Service.SIACHFC.GroupManagement.parametrosRequest oIniParamRequestCuadrillas = new Claro.SIACU.ProxyService.Transac.Service.SIACHFC.GroupManagement.parametrosRequest();
            //    Claro.SIACU.ProxyService.Transac.Service.SIACHFC.GroupManagement.parametrosRequest[] oParamRequestCuadrillas = new Claro.SIACU.ProxyService.Transac.Service.SIACHFC.GroupManagement.parametrosRequest[] { oIniParamRequestCuadrillas };

            //    Claro.SIACU.ProxyService.Transac.Service.SIACHFC.GroupManagement.parametrosRequestObjetoRequestOpcional[] ListaParamReqOpcionalCuadrillas = new Claro.SIACU.ProxyService.Transac.Service.SIACHFC.GroupManagement.parametrosRequestObjetoRequestOpcional[vListaCapReqOpc.Length];

            //    if (vListaCapReqOpc.Length > 0)
            //    {

            //        //log.Info(string.Format("ListaRequestOpcionalCapacity(), Elementos: {0}", vListaCapReqOpc.Length));

            //        int j = 0, k = 0;
            //        foreach (BEETAListaParamRequestOpcionalCapacity oListaParReq in vListaCapReqOpc)
            //        {

            //            foreach (BEETAParamRequestCapacity oParamReqCapacity in oListaParReq.ParamRequestCapacities)
            //            {
            //                Claro.SIACU.ProxyService.Transac.Service.SIACHFC.GroupManagement.parametrosRequestObjetoRequestOpcional oParamRequestOpcionalCuadrillas = new Claro.SIACU.ProxyService.Transac.Service.SIACHFC.GroupManagement.parametrosRequestObjetoRequestOpcional();
            //                oParamRequestOpcionalCuadrillas.campo = oParamReqCapacity.Campo;
            //                oParamRequestOpcionalCuadrillas.valor = oParamReqCapacity.Valor;
            //                ListaParamReqOpcionalCuadrillas[j] = oParamRequestOpcionalCuadrillas;
            //                //log.Info(string.Format("  objetoRequestOpcional -->Indice: {0}, Campo: {1}, Valor: {2}", j, oParamRequestOpcionalCuadrillas.campo, oParamRequestOpcionalCuadrillas.valor));
            //                j++;
            //            }
            //            oParamRequestCuadrillas[k].objetoRequestOpcional = ListaParamReqOpcionalCuadrillas;
            //            k++;
            //        }
            //    }
            //    else
            //    {
            //        //log.Info(string.Format("ListaRequestOpcionalCapacity(), Elementos: 0"));
            //        ListaParamReqOpcionalCuadrillas[0].campo = "";
            //        ListaParamReqOpcionalCuadrillas[0].valor = "";
            //        oParamRequestCuadrillas[0].objetoRequestOpcional = ListaParamReqOpcionalCuadrillas;
            //    }

            //    Claro.SIACU.ProxyService.Transac.Service.SIACHFC.GroupManagement.capacidadType[] ListaCapacidadTypeCuadrillas = new Claro.SIACU.ProxyService.Transac.Service.SIACHFC.GroupManagement.capacidadType[0];
            //    Claro.SIACU.ProxyService.Transac.Service.SIACHFC.GroupManagement.parametrosResponse[] ListaParamResponseCuadrillas = new Claro.SIACU.ProxyService.Transac.Service.SIACHFC.GroupManagement.parametrosResponse[0];

            //    objResponseCuadrillas = objServicioCuadrillas.consultarCapacidad(AuditRequestCuadrillas,
            //                                             vFechas, null, vCalcDur,
            //                                             vCalcDurEspec, vCalcTiempoViaje,
            //                                             vCalcTiempoViajeEspec, vCalcHabTrabajo,
            //                                             vCalcHabTrabajoEspec, vObtenerUbiZona,
            //                 vObtenerUbiZonaEspec, vEspacioTiempo, vHabilidadTrabajo,
            //                 ListaCapActiRequestCuadrillas, oParamRequestCuadrillas, out vDurActivity, out vTiempoViajeActivity, out ListaCapacidadTypeCuadrillas, out ListaParamResponseCuadrillas);

            //    Resp.CodigoRespuesta = objResponseCuadrillas.codigoRespuesta;
            //    Resp.IdTransaccion = objResponseCuadrillas.idTransaccion;
            //    Resp.MensajeRespuesta = objResponseCuadrillas.mensajeRespuesta;

            //    //log.Info(string.Format("Resultado Capacity --> codigoRespuesta: {0},idTransaccion: {1},mensajeRespuesta: {2}", objResponseCuadrillas.codigoRespuesta, objResponseCuadrillas.idTransaccion, objResponseCuadrillas.mensajeRespuesta));
            //    //log.Info(string.Format("Cantidad de Elementos : {0}", ListaCapacidadTypeCuadrillas.Length));

            //    string OutDurActivity = vDurActivity;
            //    string OutTiempoViajeActivity = vTiempoViajeActivity;

            //    Resp.DuraActivity = vDurActivity;
            //    Resp.TiempoViajeActivity = vTiempoViajeActivity;

            //    BEETAEntidadcapacidadType[] oCapacidadTypeM = new BEETAEntidadcapacidadType[ListaCapacidadTypeCuadrillas.Length];
            //    int l = 0;
            //    foreach (Claro.SIACU.ProxyService.Transac.Service.SIACHFC.GroupManagement.capacidadType oEntCapacidadType in ListaCapacidadTypeCuadrillas)
            //    {
            //        BEETAEntidadcapacidadType oCapacidadType = new BEETAEntidadcapacidadType();
            //        oCapacidadType.Ubicacion = oEntCapacidadType.ubicacion;
            //        oCapacidadType.Fecha = oEntCapacidadType.fecha;
            //        oCapacidadType.EspacioTiempo = oEntCapacidadType.espacioTiempo;
            //        oCapacidadType.HabilidadTrabajo = oEntCapacidadType.habilidadTrabajo;
            //        oCapacidadType.Cuota = oEntCapacidadType.cuota;
            //        oCapacidadType.Disponible = oEntCapacidadType.disponible;
            //        oCapacidadTypeM[l] = oCapacidadType;
            //        l++;
            //    }
            //    Resp.ObjetoCapacity = oCapacidadTypeM;



            //}
            //catch (Exception ex)
            //{
            //    //log.Info(string.Format("Error(): {0}", ex.Message));
            //    throw ex;
            //}
            Claro.Web.Logging.Info(strIdSession, pIdTrasaccion, JsonConvert.SerializeObject(Resp));
            Claro.Web.Logging.Info("HFCGetGroupCapacity", "HFCPlanMigration", "Finalizó GetGroupCapacity");
            return Resp;


        }

        public static List<Entity.Transac.Service.Fixed.TimeZone> GetTimeZones(string strIdSession, string strTransaction, string vstrCoUbi, string vstrTipTra, string vstrFecha)
        {
            Claro.Web.Logging.Info("HFCGetTimeZones", "HFCPlanMigration", "Entro GetTimeZones");
            Claro.Web.Logging.Info("HFCGetTimeZones", "HFCPlanMigration", "los parametros que recibe el metodo son: strIdSession:" + strIdSession + ";strTransaction:" + strTransaction + ";vstrCoUbi:" + vstrCoUbi + ";vstrTipTra:" + vstrTipTra + ";vstrFecha:" + vstrFecha); 
            List<Entity.Transac.Service.Fixed.TimeZone> list = null;

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("programaciones", DbType.Object, ParameterDirection.Output),
                new DbParameter("an_ubigeo", DbType.String,255, ParameterDirection.Input, vstrCoUbi),
                new DbParameter("an_tiptra", DbType.String,255, ParameterDirection.Input,vstrTipTra),
                new DbParameter("ad_fecagenda",DbType.Date,30,ParameterDirection.Input,vstrFecha)
            };
            try
            {
                list = DbFactory.ExecuteReader<List<Entity.Transac.Service.Fixed.TimeZone>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_P_GENERA_HORARIO_SIAC, parameters);
            }
            catch (Exception ex)
            {
                list = null;
                Claro.Web.Logging.Error(strIdSession, strTransaction, Claro.SIACU.Transac.Service.Functions.GetExceptionMessage(ex));
                
            }
            Claro.Web.Logging.Info(strIdSession, strTransaction, JsonConvert.SerializeObject(list));
            Claro.Web.Logging.Info("HFCGetTimeZones", "HFCGetTimeZones", "Finalizó GetTimeZones");
            return list;
        }
        public static OsbLteEntity ExecutePlanMigrationHfc(string strIdSession, string strTransaction, string TransactionId, List<ServiceByPlan> ServicesList, TypificationItem tipification, ClientParameters clientParameters, MainParameters MainParameters, PlusParameters PlusParameters, EtaSelection EtaSelection, SotParameters SotParameters, EtaParameters EtaParameters, Contract Contract, ActualizarTipificacion ActualizarTipificacion, bool FlagContingencia, bool FlagCrearPlantilla, AuditRegister AuditRegister, List<Coser> ListCoser, bool FlagValidaEta, string ParametrosConstancia, string DestinatarioCorreo, string Notes, string strTipoProducto)
        {
            Web.Logging.Info(strIdSession, strTransaction, "Entrando a ExecutePlanMigrationHfc");
            string result = string.Empty;
            HeaderRequestType oHeaderRequest = new HeaderRequestType
            {
                canal = "pruebaHFC",
                idAplicacion = "1",
                usuarioAplicacion = "",//owner el asesor
                usuarioSesion = "",//owner el asesor
                idTransaccionESB = "123",
                idTransaccionNegocio = TransactionId
            };
            OsbLteEntity OsbLteEntity = new OsbLteEntity();
            OsbLteEntity.result = String.Empty;
            Web.Logging.Info(strIdSession, strTransaction, "ExecutePlanMigrationHfc: LUEGO DE HABER LLENADO oHeaderRequest");

            serviciosTypeHFCServiciosObject[] listServices = (from ele in ServicesList
                                                              select new serviciosTypeHFCServiciosObject
                                                           {
                                                               cantidad = ele.CantEquipment,
                                                               cf = ele.CF,
                                                               codigoServicio = ele.CodServSisact,
                                                               equipo = ele.Equipment,
                                                               grupoServ = ele.CodGroupServ,
                                                               nombreServ = ele.DesServSisact,
                                                               tipoServ = ele.ServiceType

                                                           }).ToArray();
            Web.Logging.Info(strIdSession, strTransaction, "ExecutePlanMigrationHfc: LUEGO DE HABER LLENADO listServices");

            obtenerClientesType ParametrosCliente = new obtenerClientesType()
            {

                account = clientParameters.straccount,
                contactObjId = clientParameters.strcontactObjId,
                flagReg = clientParameters.strflagReg,
                msisdn = clientParameters.strmsisdn
            };
            Web.Logging.Info(strIdSession, strTransaction, "ExecutePlanMigrationHfc: LUEGO DE HABER LLENADO ParametrosCliente");

            parametrosPrincipalType parametrosPrincipal = new parametrosPrincipalType()
            {
                agente = MainParameters.strAgent,
                clase = MainParameters.strClass,
                codPlano = MainParameters.strCodPlan,
                coId = MainParameters.strCoId,
                flagCaso = MainParameters.strFlagCase,
                hechoEnUno = MainParameters.strMadeInOne,
                inconven = MainParameters.strInconven,
                inconvenCode = MainParameters.strInconvenCode,
                metodoContacto = MainParameters.strContactMethod,
                notas = MainParameters.strNotes,
                resultado = MainParameters.strResult,
                servaFect = MainParameters.strServAfect,
                servaFectCode = MainParameters.strServAfectCode,
                subClase = MainParameters.strSubClass,
                tipo = MainParameters.strType,
                tipoInter = MainParameters.strInterType,
                usrProceso = MainParameters.strUserProcess,
                valor1 = MainParameters.strValueOne,
                valor2 = MainParameters.trValueTwo
            };
            Web.Logging.Info(strIdSession, strTransaction, "ExecutePlanMigrationHfc: LUEGO DE HABER LLENADO parametrosPrincipal");

            parametrosPlusType parametrosPlus = new parametrosPlusType()
            {
                address = PlusParameters.strAddress,
                address5 = PlusParameters.strAddress5,
                adjustmentAmount = PlusParameters.strAdjustmentAmount,
                adjustmentReason = PlusParameters.strAdjustmentReason,
                amountUnit = PlusParameters.strAmountUnit == null ? string.Empty : (PlusParameters.strAmountUnit.Length <= 20 ? PlusParameters.strAmountUnit : PlusParameters.strAmountUnit.Substring(0, 20)),
                basket = PlusParameters.strBasket,
                birthday = PlusParameters.strBirthday,//to do: verificar ddMMyyyy
                chargeAmount = PlusParameters.strChargeAmount,
                city = PlusParameters.strCity,
                clarifyInteraction = PlusParameters.strClarifyInteraction,
                claroLdn1 = PlusParameters.strClaroLdn1,
                claroLdn2 = PlusParameters.strClaroLdn2,
                claroLdn3 = PlusParameters.strClaroLdn3,
                claroLdn4 = PlusParameters.strClaroLdn4,
                claroLocal1 = PlusParameters.strClaroLocal1,
                claroLocal2 = PlusParameters.strClaroLocal2,
                claroLocal3 = PlusParameters.strClaroLocal3,
                claroLocal4 = PlusParameters.strClaroLocal4,
                claroLocal5 = PlusParameters.strClaroLocal5,
                claroLocal6 = PlusParameters.strClaroLocal6,
                claroNumber = PlusParameters.strClaroNumber,
                contactPhone = PlusParameters.strContactPhone,
                contactSex = PlusParameters.strContactSex,
                department = PlusParameters.strDepartment,
                district = PlusParameters.strDistrict,
                dniLegalRep = PlusParameters.strDniLegalRep,
                documentNumber = PlusParameters.strDocumentNumber,
                email = PlusParameters.strEmail,
                emailConfirmation = PlusParameters.strEmailConfirmation,
                expireDate = PlusParameters.strExpireDate,//to do: verificar ddMMyyyy
                fax = PlusParameters.strFax,
                firstName = PlusParameters.strFirstName,
                fixedNumber = PlusParameters.strFixedNumber,
                flagChangeUser = PlusParameters.strFlagChangeUser,
                flagCharge = PlusParameters.strFlagCharge,
                flagLegalRep = PlusParameters.strFlagLegalRep,
                flagOther = PlusParameters.strFlagOther,
                flagRegistered = PlusParameters.strFlagRegistered,
                flagTitular = PlusParameters.strFlagTitular,
                iccid = PlusParameters.strIccid,
                imei = PlusParameters.strImei,
                inter1 = PlusParameters.strInter1,
                inter2 = PlusParameters.strInter2,
                inter3 = PlusParameters.strInter3,
                inter4 = PlusParameters.strInter4,
                inter5 = PlusParameters.strInter5,
                inter6 = PlusParameters.strInter6,
                inter7 = PlusParameters.strInter7,
                inter8 = PlusParameters.strInter8,
                inter9 = PlusParameters.strInter9,
                inter10 = PlusParameters.strInter10,
                inter11 = PlusParameters.strInter11,
                inter12 = PlusParameters.strInter12,
                inter13 = PlusParameters.strInter13,
                inter14 = PlusParameters.strInter14,
                inter15 = PlusParameters.strInter15,
                inter16 = PlusParameters.strInter16,
                inter17 = PlusParameters.strInter17,
                inter18 = PlusParameters.strInter18,
                inter19 = PlusParameters.strInter19,
                inter20 = PlusParameters.strInter20,
                inter21 = PlusParameters.strInter21,
                inter22 = PlusParameters.strInter22,
                inter23 = PlusParameters.strInter23,
                inter24 = PlusParameters.strInter24,
                inter25 = PlusParameters.strInter25,
                inter26 = PlusParameters.strInter26,
                inter27 = PlusParameters.strInter27,
                inter28 = PlusParameters.strInter28,
                inter29 = PlusParameters.strInter29,
                inter30 = PlusParameters.strInter30,
                lastName = PlusParameters.strLastName,
                lastNameRep = PlusParameters.strLastNameRep,
                ldiNumber = PlusParameters.strLdiNumber,
                lotCode = PlusParameters.strLotCode,
                maritalStatus = PlusParameters.strMaritalStatus,
                model = PlusParameters.strModel,
                month = PlusParameters.strMonth,
                nameLegalRep = PlusParameters.strNameLegalRep,
                occupation = PlusParameters.strOccupation,
                oldClaroLdn1 = PlusParameters.strOldClaroLdn1,
                oldClaroLdn2 = PlusParameters.strOldClaroLdn2 == null ? string.Empty : (PlusParameters.strOldClaroLdn2.Length <= 20 ? PlusParameters.strOldClaroLdn2 : PlusParameters.strOldClaroLdn2.Substring(0, 20)),                
                oldClaroLdn3 = PlusParameters.strOldClaroLdn3,
                oldClaroLdn4 = PlusParameters.strOldClaroLdn4,
                oldClaroLocal1 = PlusParameters.strOldClaroLocal1 == null ? string.Empty : (PlusParameters.strOldClaroLocal1.Length <= 20 ? PlusParameters.strOldClaroLocal1 : PlusParameters.strOldClaroLocal1.Substring(0, 20)),
                oldClaroLocal2 = PlusParameters.strOldClaroLocal2 == null ? Constant.strCero : PlusParameters.strOldClaroLocal2,
                oldClaroLocal3 = PlusParameters.strOldClaroLocal3 == null ? Constant.strCero : PlusParameters.strOldClaroLocal3,
                oldClaroLocal4 = PlusParameters.strOldClaroLocal4 == null ? Constant.strCero : PlusParameters.strOldClaroLocal4,
                oldClaroLocal5 = PlusParameters.strOldClaroLocal5,
                oldClaroLocal6 = PlusParameters.strOldClaroLocal6 == null ? Constant.strCero : PlusParameters.strOldClaroLocal6,
                oldDocNumber = PlusParameters.strOldDocNumber,
                oldFirstName = PlusParameters.strOldFirstName,
                oldFixedNumber = PlusParameters.strOldFixedNumber,
                oldFixedPhone = PlusParameters.strOldFixedPhone,
                oldLastName = PlusParameters.strOldLastName,
                oldLdiNumber = PlusParameters.strOldLdiNumber,
                operationType = PlusParameters.strOperationType,
                ostNumber = PlusParameters.strOstNumber,
                otherDocNumber = PlusParameters.strOtherDocNumber,
                otherFirstName = PlusParameters.strOtherFirstName,
                otherLastName = PlusParameters.strOtherLastName,
                otherPhone = PlusParameters.strOtherPhone,
                phoneLegalRep = PlusParameters.strPhoneLegalRep,
                plusInter2Interact = PlusParameters.strPlusInter2Interact,
                position = PlusParameters.strPosition,
                reason = PlusParameters.strReason,
                referenceAddress = PlusParameters.strReferenceAddress,
                referencePhone = PlusParameters.strReferencePhone,
                registrationReason = PlusParameters.strRegistrationReason,
                typeDocument = PlusParameters.strTypeDocument,
                zipCode = PlusParameters.strZipCode
            }; 
            Web.Logging.Info(strIdSession, strTransaction, "ExecutePlanMigrationHfc: LUEGO DE HABER LLENADO parametrosPlus");
            Web.Logging.Info(strIdSession, strTransaction, String.Format("parametrosPlus.inter15: {0}, parametrosPlus.inter29: {1}", parametrosPlus.inter15, parametrosPlus.inter29));
            parametrosEtaType ParametrosEta = new parametrosEtaType()
            {
                dniTecnico = EtaParameters.strDniTecnico,
                fechaCreacion = EtaParameters.strFechaCreacion,
                idBucket = EtaParameters.strIdBucket,
                idFranja = EtaParameters.strIdFranja,
                idPoblado = EtaParameters.strIdPoblado,
                ipCreacion = EtaParameters.strIpCreacion,
                plano = EtaParameters.strPlano,
                subtipo = EtaParameters.strSubtipo,
                usrCreacion = EtaParameters.strUsrCreacion
            };
            etaSeleccionType SelectionEta = new etaSeleccionType()
            {
                fechaCompromiso = EtaSelection.strFechaCompromiso,
                franja = EtaSelection.strFranja,
                idBucket = EtaSelection.strIdBucket,
                idConsulta = EtaSelection.strIdConsulta
            };
            parametrosSotTypeHFC ParametrosSot = new parametrosSotTypeHFC()
            {
                codId = SotParameters.strCoId,//si
                customerId = SotParameters.strCustomerId,//si
                tipoTrans = SotParameters.strTransTipo, //si                 
                tipoVia = String.IsNullOrEmpty(SotParameters.strTipoVia) ? string.Empty : SotParameters.strTipoVia,//no
                nombreVia = String.IsNullOrEmpty(SotParameters.strNombreVia) ? String.Empty : SotParameters.strNombreVia,//no
                numeroVia = String.IsNullOrEmpty(SotParameters.strNumeroVia) ? string.Empty : SotParameters.strNumeroVia,//no
                tipoUrbanizacion = String.IsNullOrEmpty(SotParameters.strTipoUrbanizacion) ? string.Empty : SotParameters.strTipoUrbanizacion,//no
                nombreUrbanizacion = String.IsNullOrEmpty(SotParameters.strNombreUrbanizacion) ? string.Empty : SotParameters.strNombreUrbanizacion,//no
                manzana = String.IsNullOrEmpty(SotParameters.strManzana) ? string.Empty : SotParameters.strManzana,//no
                lote = String.IsNullOrEmpty(SotParameters.strLote) ? string.Empty : SotParameters.strLote,//no
                codUbigeo = String.IsNullOrEmpty(SotParameters.strCodUbigeo) ? string.Empty : SotParameters.strCodUbigeo,//no
                codZona = String.IsNullOrEmpty(SotParameters.strCodZona) ? string.Empty : SotParameters.strCodZona,//no
                idPlano = String.IsNullOrEmpty(SotParameters.strIdPlano) ? string.Empty : SotParameters.strIdPlano,//no
                codeDif = SotParameters.strCodEdif,
                referencia = String.IsNullOrEmpty(SotParameters.strReferencia) ? string.Empty : SotParameters.strReferencia,//no
                observacion = String.IsNullOrEmpty(SotParameters.strObservacion) ? string.Empty : SotParameters.strObservacion,//no
                fecProg = SotParameters.strFechaProgramada,//si
                franjaHoraria = String.IsNullOrEmpty(SotParameters.strFranjaHoraria) ? string.Empty : SotParameters.strFranjaHoraria,//no
                numCarta = SotParameters.strNumCarta,//no
                operador = SotParameters.strOperador,//no                
                presuscrito = String.IsNullOrEmpty(SotParameters.strPresuscrito) ? string.Empty : SotParameters.strPresuscrito,//no
                publicar = String.IsNullOrEmpty(SotParameters.strPublicar) ? string.Empty : SotParameters.strPublicar,//no
                adTmcode = String.IsNullOrEmpty(SotParameters.strTmCode) ? string.Empty : SotParameters.strTmCode,//no
                usuarioReg = SotParameters.strUsrRegistro,//si
                cargo = String.IsNullOrEmpty(SotParameters.strCargo) ? string.Empty : SotParameters.strCargo//no

            };
            Web.Logging.Info(strIdSession, strTransaction, string.Format("BELogStrCodMoTot - {0}", ParametrosSot.codeDif));

            listaContratosObjectType olistaContratosObjectType = new listaContratosObjectType()
            {
                planTarifario = Contract.ContractList[0].strPlanTarifario,
                idSubmercado = Contract.ContractList[0].strIdSubmercado,
                idMercado = Contract.ContractList[0].strIdMercado,
                red = Contract.ContractList[0].strRed,
                estadoUmbral = Contract.ContractList[0].strEstadoUmbral,
                cantidadUmbral = Contract.ContractList[0].strCantidadUmbral,
                archivoLlamadas = System.Convert.ToBoolean(Contract.ContractList[0].strArchivoLlamadas),
                ltaServicios = (from ele in Contract.ContractList[0].ListServices
                                select new ltaRegServiciosTypeRegServiciosType
                                {
                                    camposAdicionalesCargo = new camposAdicionalesCargoType
                                    {
                                        CostoServicio = ele.CamposAdicionalesCargo.strCostoServicio,
                                        periodoCostoServicio = ele.CamposAdicionalesCargo.strPeriodoCostoServicio,
                                        tipoCostoServicio = ele.CamposAdicionalesCargo.strTipoCostoServicio
                                    },
                                    camposAdicionalesDcto = new camposAdicionalesDctoType
                                    {
                                        CostoServicioAvanzado = ele.CamposAdicionalesDescuento.strCostoServicioAvanzado,
                                        periodoCostoServicioAvanzado = ele.CamposAdicionalesDescuento.strPeriodoCostoServicioAvanzado,
                                        tipoCostoServicioAvanzado = ele.CamposAdicionalesDescuento.strTipoCostoServicioAvanzado
                                    },
                                    coId = ele.strCoId,
                                    snCode = ele.strSnCode,
                                    spCode = ele.strSpCode,
                                    profileId = ele.strProfileId
                                }).ToArray(),
                actualizacionContrato = new actualizacionContratoType
                {
                    coId = Contract.ContractList[0].ActualizacionContrato.strCoId,
                    estado = Contract.ContractList[0].ActualizacionContrato.strEstado,
                    razon = Contract.ContractList[0].ActualizacionContrato.strRazon
                },
                informacionContrato = new informacionContratoType
                {
                    coId = Contract.ContractList[0].InformacionContrato.coId,
                    listaCampos = (from ele in Contract.ContractList[0].InformacionContrato.Campos
                                   select new campoType
                                   {
                                       indice = ele.strIndice,
                                       tipo = ele.strTipo,
                                       valor = ele.strValor
                                   }).ToArray()
                }

            };
            Web.Logging.Info(strIdSession, strTransaction, "ExecutePlanMigrationHfc: LUEGO DE HABER LLENADO olistaContratosObjectType");

            List<listaContratosObjectType> lista = new List<listaContratosObjectType>();
            lista.Add(olistaContratosObjectType);
            listaContratosType oListaContratosType = new listaContratosType()
            {
                listaContratosObject = lista.ToArray(),
            };
            contratosType ContratosType = new contratosType()
            {
                ipAplicacion = Contract.strIpAplicacion,
                nombreAplicacion = Contract.strNombreAplicacion,
                tipoPostpago = Contract.strTipoPostpago,
                listaContratos = oListaContratosType,
            };
            Web.Logging.Info(strIdSession, strTransaction, "ExecutePlanMigrationHfc: LUEGO DE HABER LLENADO ContratosType");

            actualizarTipificacionType oActualizarTipificacion = new actualizarTipificacionType()
            {
                orden = ActualizarTipificacion.Orden
            };
            registroAuditoriaType oRegistroAuditoria = new registroAuditoriaType()
            {
                idTransaccion = AuditRegister.strIdTransaccion,
                servicio = AuditRegister.strServicio,
                ipCliente = AuditRegister.strIpCliente,
                nombreCliente = AuditRegister.strNombreCliente,
                ipServidor = AuditRegister.strIpServidor,
                nombreServidor = AuditRegister.strNombreServidor,
                monto = AuditRegister.strMonto,
                cuentaUsuario = AuditRegister.strCuentaUsuario,
                telefono = AuditRegister.strTelefono
                //,
               // texto = AuditRegister.strTexto
            };
            listaCoserTypeListaCoserObject[] oListaCoser = (from ele in ListCoser
                                                            select new listaCoserTypeListaCoserObject
                                                            {
                                                                cargoFijo = ele.strCargoFijo,
                                                                periodos = ele.strPeriodos,
                                                                snCode = ele.strSnCode,
                                                                spCode = ele.strSpCode,
                                                                tipoServicio = ele.strTipoServicio
                                                            }).ToArray();
            serviciosTypeHFCServiciosObject[] servicios = (from ele in ServicesList
                                                           select new serviciosTypeHFCServiciosObject
                                                        {
                                                            cantidad = ele.CantEquipment,
                                                            cf = ele.CF,
                                                            codigoServicio = ele.CodServiceType,
                                                            equipo = ele.Equipment,
                                                            grupoServ = ele.GroupServ,
                                                            nombreServ = ele.DesServSisact,
                                                            tipoServ = ele.ServiceType
                                                        }).ToArray();
            actualizacionContratoType oActualizacionContrato = new actualizacionContratoType
            {

            };

            Web.Logging.Info(strIdSession, strTransaction, "ExecutePlanMigrationHfc: LUEGO DE HABER LLENADO oActualizacionContrato");

            ejecutarCambioPlanRequest oRequest = new ejecutarCambioPlanRequest();

            oRequest.ejecutarCambioPlanFijaRequest = new ejecutarCambioPlanFijaRequest();
            oRequest.headerRequest = oHeaderRequest;
            oRequest.ejecutarCambioPlanFijaRequest.tipoProducto = strTipoProducto;
            oRequest.ejecutarCambioPlanFijaRequest.datosCambioPlanHFCRequest = new DatosCambioPlanHFCRequestType();
            oRequest.ejecutarCambioPlanFijaRequest.datosCambioPlanHFCRequest.listaServicios = listServices;
            oRequest.ejecutarCambioPlanFijaRequest.datosCambioPlanHFCRequest.parametrosCliente = ParametrosCliente;
            oRequest.ejecutarCambioPlanFijaRequest.datosCambioPlanHFCRequest.parametrosPrincipal = parametrosPrincipal;
            oRequest.ejecutarCambioPlanFijaRequest.datosCambioPlanHFCRequest.parametrosPlus = parametrosPlus;
            oRequest.ejecutarCambioPlanFijaRequest.datosCambioPlanHFCRequest.parametrosSot = ParametrosSot;
            oRequest.ejecutarCambioPlanFijaRequest.datosCambioPlanHFCRequest.contratos = ContratosType;
            oRequest.ejecutarCambioPlanFijaRequest.datosCambioPlanHFCRequest.actualizarTipificacion = oActualizarTipificacion;
            oRequest.ejecutarCambioPlanFijaRequest.datosCambioPlanHFCRequest.flagContingencia = (FlagContingencia ? 1 : 0).ToString();
            oRequest.ejecutarCambioPlanFijaRequest.datosCambioPlanHFCRequest.flagCrearPlantilla = (FlagCrearPlantilla ? 1 : 0).ToString();// (FlagCrearPlantilla ? 1 : 0).ToString();
            oRequest.ejecutarCambioPlanFijaRequest.datosCambioPlanHFCRequest.flagValidaEta = (FlagValidaEta ? 1 : 0).ToString();//to do: definir con gustavo
            oRequest.ejecutarCambioPlanFijaRequest.datosCambioPlanHFCRequest.registroAuditoria = oRegistroAuditoria;
            oRequest.ejecutarCambioPlanFijaRequest.datosCambioPlanHFCRequest.parametrosConstancia = ParametrosConstancia;
            oRequest.ejecutarCambioPlanFijaRequest.datosCambioPlanHFCRequest.destinatarioCorreo = DestinatarioCorreo;
            oRequest.ejecutarCambioPlanFijaRequest.datosCambioPlanHFCRequest.notas = Notes;
            oRequest.ejecutarCambioPlanFijaRequest.datosCambioPlanHFCRequest.listaCoser = oListaCoser;
            oRequest.ejecutarCambioPlanFijaRequest.datosCambioPlanHFCRequest.etaSeleccion = SelectionEta;
            oRequest.ejecutarCambioPlanFijaRequest.datosCambioPlanHFCRequest.parametrosEta = ParametrosEta;
            HeaderResponseType oHeaderResponse = new HeaderResponseType();
            ejecutarCambioPlanResponse oResponse = new ejecutarCambioPlanResponse();
            Web.Logging.Info(strIdSession, strTransaction, "LOG_PRUEBA_OSB_REQUEST_HFC: " + JsonConvert.SerializeObject(oRequest));
            string strFaultMsg = string.Empty;

            oResponse = Claro.Web.Logging.ExecuteMethod<ejecutarCambioPlanResponse>(strIdSession, strTransaction, () =>
            {
                try
                {
                    return Configuration.ServiceConfiguration.SiacFixedPlanMigration.ejecutarCambioPlan(oRequest);
                }
                catch (FaultException<ClaroFault> ex)
                {
                    Web.Logging.Info(strIdSession, strTransaction, "LOG_PRUEBA_OSB_REQUEST_HFC_CLAROFAULT EX: " + JsonConvert.SerializeObject(ex));
                    strFaultMsg = ex.Detail.descripcionError;
                    Claro.Web.Logging.Error(strIdSession, strTransaction, strFaultMsg);
                    OsbLteEntity.result = ex.Detail.descripcionError;
                    return null;
                    throw new Exception(strFaultMsg);
                }
                catch (FaultException<ExceptionDetail> ex)
                {
                    strFaultMsg = ex.Message;
                    Claro.Web.Logging.Error(strIdSession, strTransaction, strFaultMsg);
                    OsbLteEntity.result = ex.Message;
                    return null;
                    throw new Exception(strFaultMsg);
                }
                catch (FaultException ex)
                {
                    strFaultMsg = ex.Message;
                    Claro.Web.Logging.Error(strIdSession, strTransaction, strFaultMsg);
                    OsbLteEntity.result = ex.Message;
                    return null;
                    throw new Exception(strFaultMsg);
                }
                catch (Exception ex)
                {
                    strFaultMsg = ex.Message;
                    Claro.Web.Logging.Error(strIdSession, strTransaction, strFaultMsg);
                    Web.Logging.Info(strIdSession, strTransaction, "LOG_PRUEBA_OSB_REQUEST_HFC_GENERAL_EXCEPTION: " + JsonConvert.SerializeObject(ex));
                    OsbLteEntity.result = ex.Message;
                    return null;
                    throw new Exception(strFaultMsg);

                }


            });
            Web.Logging.Info(strIdSession, strTransaction, "LOG_PRUEBA_OSB_REQUEST_HFC_RESPONSE: " + JsonConvert.SerializeObject(oResponse));

            if (oResponse != null && oResponse.ejecutarCambioPlanFijaResponse != null)
            {
                OsbLteEntity.result = oResponse.ejecutarCambioPlanFijaResponse.responseStatus.descripcionRespuesta;
                if (oResponse.ejecutarCambioPlanFijaResponse.responseData.datosCambioPlanHFCResponse != null)
                {
                    OsbLteEntity.InteractionCode = oResponse.ejecutarCambioPlanFijaResponse.responseData.datosCambioPlanHFCResponse.codigoInteraccion;
                    OsbLteEntity.SotNumber = oResponse.ejecutarCambioPlanFijaResponse.responseData.datosCambioPlanHFCResponse.nroSot;
                    OsbLteEntity.ConstancyRoute = oResponse.ejecutarCambioPlanFijaResponse.responseData.datosCambioPlanHFCResponse.rutaConstancia;
                    Web.Logging.Info(strIdSession, strTransaction, "rutaConstancia: " + oResponse.ejecutarCambioPlanFijaResponse.responseData.datosCambioPlanHFCResponse.rutaConstancia);
                }
                else {
                    Web.Logging.Info(strIdSession, strTransaction, "datosCambioPlanHFCResponse == null; ");
                }

            }
            else if (OsbLteEntity.result == string.Empty && oResponse.ejecutarCambioPlanFijaResponse == null)
            {
                OsbLteEntity.result = String.Empty;
            }
            Web.Logging.Info(strIdSession, strTransaction, "OsbLteEntity.result: " + OsbLteEntity.result);
            if (!String.IsNullOrEmpty (OsbLteEntity.ConstancyRoute))
                Web.Logging.Info(strIdSession, strTransaction, "OsbLteEntity.ConstancyRoute: " + OsbLteEntity.ConstancyRoute);
            else
                Web.Logging.Info(strIdSession, strTransaction, "OsbLteEntity.ConstancyRoute: Vacio");
            Claro.Web.Logging.Info(strIdSession, strTransaction, JsonConvert.SerializeObject(OsbLteEntity));
            return OsbLteEntity;
        }

        public static CurrentPlan GetCurrentPlan(string strIdSession, string strTransaction, string strCoId)
        {
            Claro.Web.Logging.Info("HFCGetCurrentPlan", "HFCPlanMigration", "Entro GetCurrentPlan");
            Claro.Web.Logging.Info("HFCGetCurrentPlan", "HFCPlanMigration", "los parametros que recibe el metodo son: strIdSession:" + strIdSession + ";strTransaction:" + strTransaction + ";strCoId:" + strCoId); 
            CurrentPlan oAddress;

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("CO_ID", DbType.String, ParameterDirection.Input,strCoId),                
                new DbParameter("PLAN_ACTUAL", DbType.Object, ParameterDirection.Output)
            };

            oAddress = new CurrentPlan();
            Claro.Web.Logging.Info(strIdSession, strTransaction, JsonConvert.SerializeObject(oAddress));
            Claro.Web.Logging.Info("HFCGetCurrentPlan", "HFCPlanMigration", "Finalizó GetCurrentPlan");
            return oAddress;
       
        }

        public static bool SendNewPlanServices(string strIdSession, string strTransaction, List<ServiceByPlan> services)
        {
            Claro.Web.Logging.Info("HFCSendNewPlanServices", "HFCPlanMigration", "Entro SendNewPlanServices");
            Claro.Web.Logging.Info("HFCSendNewPlanServices", "HFCPlanMigration", "los parametros que recibe el metodo son: strIdSession:" + strIdSession + ";strTransaction:" + strTransaction + ";services:" + services.ToString()); 
            bool oAddress = false;
            Claro.Web.Logging.Info(strIdSession, strTransaction, JsonConvert.SerializeObject(oAddress));
            Claro.Web.Logging.Info("HFCSendNewPlanServices", "HFCPlanMigration", "Finalizó SendNewPlanServices");

            return oAddress;
        }

        public static List<ServiceByCurrentPlan> GetServicesByCurrentPlan(string strIdSession, string strTransaction, string strContractId)
        {
            Claro.Web.Logging.Info("HFCGetServicesByCurrentPlan", "HFCPlanMigration", "Entro GetServicesByCurrentPlan");
            List<ServiceByCurrentPlan> ServicesByCurrentPlan;
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("p_co_id", DbType.String, ParameterDirection.Input,strContractId),
                new DbParameter("p_tmdes", DbType.String, 255, ParameterDirection.Output),
                new DbParameter("p_cursor", DbType.Object, ParameterDirection.Output),
                new DbParameter("v_errnum", DbType.String, 255, ParameterDirection.Output),
                new DbParameter("v_errmsj", DbType.String, 255, ParameterDirection.Output)

            };
            Web.Logging.Info(strIdSession, strTransaction, "los parámetros para TIM.PP004_SIAC_LTE.SIACSU_LISTA_SERV_TELEFONO: p_co_id=" + strContractId);
            try
            {
              ServicesByCurrentPlan = DbFactory.ExecuteReader<List<ServiceByCurrentPlan>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_LISTA_SERVICIOS_TELEFONO, parameters);
            }
            catch (Exception ex)
            {
                ServicesByCurrentPlan = null;
                Claro.Web.Logging.Error(strIdSession, strTransaction, Claro.SIACU.Transac.Service.Functions.GetExceptionMessage(ex));
                
            }

            Web.Logging.Info(strIdSession, strTransaction, JsonConvert.SerializeObject(ServicesByCurrentPlan));
            Claro.Web.Logging.Info("HFCGetServicesByCurrentPlan", "HFCPlanMigration", "Finalizó GetServicesByCurrentPlan");
            return ServicesByCurrentPlan;
        }
        public static List<EquipmentByCurrentPlan> GetEquipmentByCurrentPlan(string strIdSession, string strTransaction, string strIdContract)
        {
            Claro.Web.Logging.Info("HFCGetEquipmentByCurrentPlan", "HFCPlanMigration", "Entro GetEquipmentByCurrentPlan");
            Claro.Web.Logging.Info("HFCGetEquipmentByCurrentPlan", "HFCPlanMigration", "los parametros que recibe el metodo son: strIdSession:" + strIdSession + ";strTransaction:" + strTransaction + ";strIdContract:" + strIdContract);
            List<EquipmentByCurrentPlan> EquipmentsByCurrentPlan;
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("k_cod_id", DbType.String,ParameterDirection.Input, strIdContract),
                new DbParameter("k_cursor", DbType.Object, ParameterDirection.Output),
                new DbParameter("k_error", DbType.String,255,ParameterDirection.Output),
                new DbParameter("k_mensaje", DbType.String,255, ParameterDirection.Output)
            };
             

             try
             {
                 EquipmentsByCurrentPlan = DbFactory.ExecuteReader<List<EquipmentByCurrentPlan>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_SGASS_DET_EQUIPO, parameters);
             }
             catch (Exception ex)
             {
                 EquipmentsByCurrentPlan = null;
              Claro.Web.Logging.Error(strIdSession, strTransaction, Claro.SIACU.Transac.Service.Functions.GetExceptionMessage(ex));
             }
             Claro.Web.Logging.Info(strIdSession, strTransaction, JsonConvert.SerializeObject(EquipmentsByCurrentPlan));
             Claro.Web.Logging.Info("HFCGetEquipmentByCurrentPlan", "HFCPlanMigration", "Finalizó GetEquipmentByCurrentPlan");
            return EquipmentsByCurrentPlan;
        }
        public static TechnicalVisit GetTechnicalVisitResult(string strIdSession, string strTransaction, string strCoId, string strCustomerId, string strTmCode, string strCodPlanSisact, string strTrama)
        {
            Claro.Web.Logging.Info(strIdSession, strTransaction, string.Format("GetTechnicalVisitResult - {0}", strCoId));
            Claro.Web.Logging.Info(strIdSession, strTransaction, string.Format("Input params strCustomerId:{0} strTmCode:{1} strCodPlanSisact:{2} strTrama:{3}", strCustomerId, strTmCode, strCodPlanSisact, strTrama));
   
            TechnicalVisit technicalVisit = new TechnicalVisit();
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("AN_CO_ID", DbType.String,255, ParameterDirection.Input,strCoId),
                new DbParameter("AN_CUSTOMER_ID",DbType.String,255,ParameterDirection.Input,strCustomerId),
                new DbParameter("AN_TMCODE",DbType.String,255,ParameterDirection.Input,strTmCode),
                new DbParameter("AN_COD_PLAN_SISACT",DbType.String,255,ParameterDirection.Input,strCodPlanSisact),
                new DbParameter("AV_TRAMA",DbType.String,4000,ParameterDirection.Input,strTrama),
                new DbParameter("AN_FLG_VISITA",DbType.String,255,ParameterDirection.Output),
                new DbParameter("AN_CODMOTOT",DbType.String,255,ParameterDirection.Output),
                new DbParameter("AN_ERROR",DbType.String,255,ParameterDirection.Output),
                new DbParameter("AV_ERROR", DbType.String,255, ParameterDirection.Output),
                new DbParameter("AV_ANOTACION", DbType.String, 4000, ParameterDirection.Output),
                new DbParameter("AV_SUBTIPO", DbType.String, 4000, ParameterDirection.Output)
            };
            try
            {
                DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_SGASS_VAL_ORDEN_VISIT_CP, parameters, reader =>
                {
                    while (reader.Read())
                    {
                    }
                }
               );
                technicalVisit.Flag = parameters[5].Value.ToString();
                technicalVisit.Codmot = parameters[6].Value.ToString();
                technicalVisit.Anerror = parameters[7].Value.ToString();
                technicalVisit.Averror = parameters[8].Value.ToString();
                technicalVisit.Anotaciones = parameters[9].Value.ToString();
                technicalVisit.Subtipo = parameters[10].Value.ToString();
            }
            catch (Exception ex)
            {
                technicalVisit = null;
                Claro.Web.Logging.Error(strIdSession, strTransaction, Claro.SIACU.Transac.Service.Functions.GetExceptionMessage(ex));
              
            }
            Claro.Web.Logging.Info(strIdSession, strTransaction, string.Format("Output params {0}", JsonConvert.SerializeObject(technicalVisit)));
            return technicalVisit;
        }

        #region Proy-32650
        /// <summary>
        /// Consulta el descuento vigente de cargo fijo que tiene el cliente.
        /// </summary>
        /// <param name="sesion"></param>
        /// <param name="transaction"></param>
        /// <param name="msgError"></param>
        /// <returns></returns>
        public static List<CurrentDiscountFixedCharge> GetCurrentDiscountFixedCharge(string strIdSession, string strTransaction, Int64 intContractId, ref string msgError, ref string msgDescError)
        {
            string sError = string.Empty, sDescError = string.Empty;
            List<CurrentDiscountFixedCharge> ListCurrentDiscount = new List<CurrentDiscountFixedCharge>();

            DbParameter[] parameters =
            {   new DbParameter("PI_CO_ID", DbType.Int64,ParameterDirection.Input, intContractId),
                new DbParameter("PO_CURSOR", DbType.Object, ParameterDirection.Output),
                new DbParameter("PO_COD_ERR", DbType.Int64, 255,ParameterDirection.Output), 
                new DbParameter("PO_DES_ERR", DbType.String, 255,ParameterDirection.Output)
                
            };

            try
            {
                Web.Logging.ExecuteMethod(strIdSession, strTransaction, () =>
                {
                    DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS,
                        DbCommandConfiguration.SIACU_BSCSSS_DESC_CARGOFIJO, parameters, reader =>
                        {
                            while (reader.Read())
                            {


                                var item = new CurrentDiscountFixedCharge
                                {
                                    TOTAL_DESCUENTO = reader["TOTAL_DESCUENTO"].ToString(),
                                    FEC_INICIO = reader["FEC_INICIO"].ToString(),
                                    FEC_FIN = reader["FEC_FIN"].ToString(),
                                    PERIODO = reader["PERIODO"].ToString(),
                                    FLAG = reader["FLAG"].ToString(),
                                    PORCENTAJE= reader["PORCENTAJE"].ToString()

                                };
                                ListCurrentDiscount.Add(item);


                            }
                        });
                    sError = parameters[2].Value.ToString();
                    sDescError = parameters[3].Value.ToString();
                });                
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
            }
            msgError = sError;
            msgDescError = sDescError;
            return ListCurrentDiscount;

        }

        /// <summary>
        /// Se registran el bono de descuento para cambio de plan.
        /// </summary>
        /// <param name="sesion"></param>
        /// <param name="transaction"></param>
        /// <param name="msgError"></param>
        /// <returns></returns>
        public static bool InsertDiscountBondExchangePlan(string strIdSession, string strTransaction, Int64 intIdCustomer, Int64 intContractIdAnt, string strSnCode, Int64 intTmCodeCom, ref string msgError, ref string msgDescError)
        {
            bool resultado = true;
            string sError = string.Empty, sDescError = string.Empty;
            DbParameter[] parameters =
            {   new DbParameter("PI_CUSTOMER_ID", DbType.Int64,ParameterDirection.Input, intIdCustomer),
                new DbParameter("PI_CO_ID_ANT", DbType.Int64, ParameterDirection.Input, intContractIdAnt),
                new DbParameter("PI_SNCODE", DbType.String, ParameterDirection.Input, strSnCode),
                new DbParameter("PI_TMCODE_COM", DbType.Int64, ParameterDirection.Input, intTmCodeCom),
                new DbParameter("PO_COD_ERR", DbType.Int64, 255,ParameterDirection.Output), 
                new DbParameter("PO_DES_ERR", DbType.String, 255,ParameterDirection.Output)
                
            };

            try
            {
                Web.Logging.ExecuteMethod(strIdSession, strTransaction, () =>
                {
                    DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS,
                        DbCommandConfiguration.SIACU_BSCSSI_REG_DESCCP, parameters);
                });
                sError = parameters[4].Value.ToString();
                sDescError = parameters[5].Value.ToString();
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
                resultado = false;
            }
            msgError = sError;
            msgDescError = sDescError;
            return resultado;

        }
        #endregion

    }
}
