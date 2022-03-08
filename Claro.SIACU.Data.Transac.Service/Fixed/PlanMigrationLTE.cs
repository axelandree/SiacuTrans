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
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Protocols;
using System.Xml;
using objTransacService = Claro.SIACU.Transac.Service;
using Constant = Claro.SIACU.Transac.Service.Constants;
using CambioPlanLTEWSService = Claro.SIACU.ProxyService.Transac.Service.CambioPlanLTEWSService;
namespace Claro.SIACU.Data.Transac.Service.Fixed
{
    public class PlanMigrationLTE
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
            Claro.Web.Logging.Info("LTEGetNewPlans", "LTEPlanMigration", "Entro GetNewPlans");
            Claro.Web.Logging.Info("LTEGetNewPlans", "LTEPlanMigration", "los parametros que recibe el metodo son: strIdSession:" + strIdSession + ";strTransaction:" + strTransaction + ";strPlano:" + strPlano + ";strOferta:" + strOferta + ";strTipoProducto:" + strTipoProducto); 
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
            catch(Exception ex) {
                list = null;
                Claro.Web.Logging.Error(strIdSession, strTransaction, Claro.SIACU.Transac.Service.Functions.GetExceptionMessage(ex));              
            }
            Claro.Web.Logging.Info(strIdSession, strTransaction, JsonConvert.SerializeObject(list));
            Claro.Web.Logging.Info("LTEGetNewPlans", "LTEPlanMigration", "Finalizó GetNewPlans");
            return list;
        }
        public static List<Entity.Transac.Service.Fixed.ServiceByPlan> GetServicesByPlan(string strIdSession, string strTransaction, string idplan, string strTipoProducto)
        {
            Claro.Web.Logging.Info("LTEGetServicesByPlan", "LTEPlanMigration", "Entro GetServicesByPlan");
            //List<Entity.Transac.Service.Fixed.ServiceByPlan> list = null;

            DbParameter[] parameters = new DbParameter[] {                
                new DbParameter("P_PLAN", DbType.String,255, ParameterDirection.Input,idplan),
                new DbParameter("p_tipo_producto",DbType.String,255,ParameterDirection.Input,strTipoProducto),
                new DbParameter("P_CURSOR", DbType.Object, ParameterDirection.Output)                
            };
            /*try
            {

                list = DbFactory.ExecuteReader<List<Entity.Transac.Service.Fixed.ServiceByPlan>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_PVU, DbCommandConfiguration.SIACU_SP_CON_PLAN_SERVICIO_LTE, parameters);
            }
            catch (Exception ex)
            {
                list = null;
                Claro.Web.Logging.Error(strIdSession, strTransaction, Claro.SIACU.Transac.Service.Functions.GetExceptionMessage(ex));
            }*/
            var lstServiceByPlan = new List<Entity.Transac.Service.Fixed.ServiceByPlan>();
            Entity.Transac.Service.Fixed.ServiceByPlan objServiceByPlan = null;

            try
            {
                DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_PVU, DbCommandConfiguration.SIACU_SP_CON_PLAN_SERVICIO_LTE, parameters, dr =>
                {
                    while (dr.Read())
                    {
                        objServiceByPlan = new Entity.Transac.Service.Fixed.ServiceByPlan();
                        objServiceByPlan.CodPlanSisact = objTransacService.Functions.CheckStr(dr["COD_PLAN_SISACT"]);
                        objServiceByPlan.DesPlanSisact = objTransacService.Functions.CheckStr(dr["DES_PLAN_SISACT"]);
                        objServiceByPlan.Tmcode = objTransacService.Functions.CheckStr(dr["TMCODE"]);
                        objServiceByPlan.Solution = objTransacService.Functions.CheckStr(dr["SOLUCION"]);
                        objServiceByPlan.CodServSisact = objTransacService.Functions.CheckStr(dr["COD_SERV_SISACT"]);
                        objServiceByPlan.Sncode = objTransacService.Functions.CheckStr(dr["SNCODE"]);
                        objServiceByPlan.Spcode = objTransacService.Functions.CheckStr(dr["SPCODE"]);
                        objServiceByPlan.CodServiceType = objTransacService.Functions.CheckStr(dr["COD_TIPO_SERVICIO"]);
                        objServiceByPlan.ServiceType = objTransacService.Functions.CheckStr(dr["TIPO_SERVICIO"]);
                        objServiceByPlan.DesServSisact = objTransacService.Functions.CheckStr(dr["DES_SERV_SISACT"]);
                        objServiceByPlan.CodPrincipalGroup = objTransacService.Functions.CheckStr(dr["GSRVC_PRINCIPAL"]);
                        objServiceByPlan.CodGroupServ = objTransacService.Functions.CheckStr(dr["COD_GRUPO_SERV"]);
                        objServiceByPlan.CodEquipmentGroup = objTransacService.Functions.CheckStr(dr["COD_GRUPO_EQU"]);
                        objServiceByPlan.GroupServ = objTransacService.Functions.CheckStr(dr["GRUPO_SERV"]);
                        objServiceByPlan.CF = objTransacService.Functions.CheckStr(dr["CF"]);
                        objServiceByPlan.IDEquipment = objTransacService.Functions.CheckStr(dr["IDEQUIPO"]);
                        objServiceByPlan.Equipment = objTransacService.Functions.CheckStr(dr["EQUIPO"]);
                        objServiceByPlan.CantEquipment = objTransacService.Functions.CheckStr(dr["CANT_EQUIPO"]);
                        objServiceByPlan.Codtipequ = objTransacService.Functions.CheckStr(dr["CODTIPEQU"]);
                        objServiceByPlan.Dscequ = objTransacService.Functions.CheckStr(dr["DSCEQU"]);
                        objServiceByPlan.Tipequ = objTransacService.Functions.CheckStr(dr["TIPEQU"]);
                        objServiceByPlan.CodeExternal = objTransacService.Functions.CheckStr(dr["COD_EXTERNO"]);
                        objServiceByPlan.DesCodeExternal = objTransacService.Functions.CheckStr(dr["DES_COD_EXTERNO"]);
                        objServiceByPlan.ServvUserCrea = objTransacService.Functions.CheckStr(dr["SERVV_USUARIO_CREA"]);
                        lstServiceByPlan.Add(objServiceByPlan);
                    }
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
            }
            Claro.Web.Logging.Info(strIdSession, strTransaction, JsonConvert.SerializeObject(lstServiceByPlan));
            Claro.Web.Logging.Info("LTEGetServicesByPlan", "LTEPlanMigration", "Finalizó GetServicesByPlan");
            return lstServiceByPlan;
        }

        public static List<Entity.Transac.Service.Fixed.ServiceByPlan> GetInformationLetter2(string strIdSession, string strTransaction, string idplan, string coid)
        {
            Claro.Web.Logging.Info("LTEGetInformationLetter2", "LTEPlanMigration", "Entro GetInformationLetter2");
            Claro.Web.Logging.Info("LTEGetInformationLetter2", "LTEPlanMigration", "los parametros que recibe el metodo son: strIdSession:" + strIdSession + ";strTransaction:" + strTransaction + ";idplan:" + idplan + ";coid:" + coid);
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
            Claro.Web.Logging.Info("LTEGetInformationLetter2", "LTEPlanMigration", "Finalizó GetInformationLetter2");
            return list;
        }


        public static string GetDecoderServiceStatus(string strIdSession, string strTransaction, int intContract, int intSnCode)
        {
            Claro.Web.Logging.Info("LTEGetDecoderServiceStatus", "LTEPlanMigration", "Entro GetDecoderServiceStatus");
            Claro.Web.Logging.Info("LTEGetInformationLetter2", "LTEPlanMigration", "los parametros que recibe el metodo son: strIdSession:" + strIdSession + ";strTransaction:" + strTransaction + ";intContract:" + intContract + ";intSnCode:" + intSnCode); 
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
           

            Claro.Web.Logging.Info(strIdSession, strTransaction, JsonConvert.SerializeObject(parameters));
            Claro.Web.Logging.Info("LTEGetDecoderServiceStatus", "LTEPlanMigration", "Finalizó GetDecoderServiceStatus");
            return parameters[2].Value.ToString();


        }

        public static List<Entity.Transac.Service.Fixed.Carrier> GetCarrierList(string strIdSession, string strTransaction)
        {
            Claro.Web.Logging.Info("LTEGetCarrierList", "LTEPlanMigration", "Entro GetCarrierList");
            Claro.Web.Logging.Info("LTEGetCarrierList", "LTEPlanMigration", "los parametros que recibe el metodo son: strIdSession:" + strIdSession + ";strTransaction:" + strTransaction);

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
            Claro.Web.Logging.Info("LTEGetCarrierList", "LTEPlanMigration", "Finalizó GetCarrierList");

            return list;
        }

        public static List<Entity.Transac.Service.Fixed.ServiceByInteraction> GetServicesByInteraction(string strIdSession, string strTransaction, string idInteraccion)
        {
            Claro.Web.Logging.Info("LTEGetServicesByInteraction", "LTEPlanMigration", "Entro GetServicesByInteraction");
            Claro.Web.Logging.Info("LTEGetServicesByInteraction", "LTEPlanMigration", "los parametros que recibe el metodo son: strIdSession:" + strIdSession + ";strTransaction:" + strTransaction + ";idInteraccion:" + idInteraccion);
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
            Claro.Web.Logging.Info("LTEGetServicesByInteraction", "LTEPlanMigration", "Finalizó GetServicesByInteraction");
            return list;
        }
        public static List<Entity.Transac.Service.Fixed.TransactionRule> GetTransactionRuleList(string strIdSession, string strTransaction, string SubClase)
        {
            Claro.Web.Logging.Info("LTEGetTransactionRuleList", "LTEPlanMigration", "Entro GetTransactionRuleList");
            Claro.Web.Logging.Info("LTEGetTransactionRuleList", "LTEPlanMigration", "los parametros que recibe el metodo son: strIdSession:" + strIdSession + ";strTransaction:" + strTransaction + ";SubClase:" + SubClase);
            List<Entity.Transac.Service.Fixed.TransactionRule> list = null;

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_SUSCLASE", DbType.String,255, ParameterDirection.Input,SubClase),
                new DbParameter("P_CURSOR", DbType.Object, ParameterDirection.Output)                
            };
            try
            {
                list = DbFactory.ExecuteReader<List<Entity.Transac.Service.Fixed.TransactionRule>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_CONSULTAR_REGLAS_ATENCION, parameters);
            }
            catch(Exception ex){
                list = null;
                Claro.Web.Logging.Error(strIdSession, strTransaction, Claro.SIACU.Transac.Service.Functions.GetExceptionMessage(ex));
            }
            
            Claro.Web.Logging.Info(strIdSession, strTransaction, JsonConvert.SerializeObject(list));
            Claro.Web.Logging.Info("LTEGetTransactionRuleList", "LTEPlanMigration", "Finalizó GetTransactionRuleList");
            return list;
        }

        public static List<Entity.Transac.Service.Fixed.ServiceByPlan> GetListServicesByPlan(string strIdSession, string strTransaction, string idplan)
        {
            Claro.Web.Logging.Info("LTEGetListServicesByPlan", "LTEPlanMigration", "Entro GetListServicesByPlan");
            Claro.Web.Logging.Info("LTEGetListServicesByPlan", "LTEPlanMigration", "los parametros que recibe el metodo son: strIdSession:" + strIdSession + ";strTransaction:" + strTransaction + ";idplan:" + idplan);
            List<Entity.Transac.Service.Fixed.ServiceByPlan> list = null;

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_CURSOR", DbType.Object, ParameterDirection.Output),
                new DbParameter("P_PLAN", DbType.String,255, ParameterDirection.Input, idplan)                
            };
            try
            {
                list = DbFactory.ExecuteReader<List<Entity.Transac.Service.Fixed.ServiceByPlan>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_PVU, DbCommandConfiguration.SIACU_SP_CON_PLAN_SERVICIO, parameters);
            }
            catch(Exception ex){
                list = null;
                Claro.Web.Logging.Error(strIdSession, strTransaction, Claro.SIACU.Transac.Service.Functions.GetExceptionMessage(ex));
            }
            Claro.Web.Logging.Info(strIdSession, strTransaction, JsonConvert.SerializeObject(list));
            Claro.Web.Logging.Info("LTEGetListServicesByPlan", "LTEPlanMigration", "Finalizó GetListServicesByPlan");
            return list;
        }

        public static List<Entity.Transac.Service.Fixed.JobType> GetJobTypes(string strIdSession, string strTransaction, int vintTipoTransaccion)
        {
            Claro.Web.Logging.Info("LTEGetJobTypes", "LTEPlanMigration", "Entro GetJobTypes");
            Claro.Web.Logging.Info("LTEGetJobTypes", "LTEPlanMigration", "los parametros que recibe el metodo son: strIdSession:" + strIdSession + ";strTransaction:" + strTransaction + ";vintTipoTransaccion:" + vintTipoTransaccion);
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
            Claro.Web.Logging.Info("LTEGetJobTypes", "LTEPlanMigration", "Finalizó GetJobTypes");
            return list;
        }


        public static ETAFlow ETAFlowValidate(string strIdSession, string strTransaction, string as_origen, string av_idplano, string av_ubigeo, int an_tiptra,
                                     string an_tipsrv)
        {
            Claro.Web.Logging.Info("LTEETAFlowValidate", "LTEPlanMigration", "Entro ETAFlowValidate");
            Claro.Web.Logging.Info("LTEETAFlowValidate", "LTEPlanMigration", "los parametros que recibe el metodo son: strIdSession:" + strIdSession + ";strTransaction:" + strTransaction + ";as_origen:" + as_origen + ";av_idplano:" + av_idplano + ";av_ubigeo:" + av_ubigeo + ";an_tiptra:" + an_tiptra);
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
                return new ETAFlow
                {
                    an_indica = 0,
                    as_codzona = string.Empty
                };
            }
           
            Claro.Web.Logging.Info(strIdSession, strTransaction, JsonConvert.SerializeObject(parameters));
            Claro.Web.Logging.Info("LTEETAFlowValidate", "LTEPlanMigration", "Finalizó ETAFlowValidate");
            return new ETAFlow
            {
                an_indica = Convert.ToInt(parameters[6].Value.ToString()),
                as_codzona = Convert.ToString(parameters[5].Value)
            };
        }

        public static List<Entity.Transac.Service.Fixed.OrderType> GetOrderType(string strIdSession, string strTransaction, string vintTipoTra)
        {
            Claro.Web.Logging.Info("LTEGetOrderType", "LTEPlanMigration", "Entro GetOrderType");
            Claro.Web.Logging.Info("LTEGetOrderType", "LTEPlanMigration", "los parametros que recibe el metodo son: strIdSession:" + strIdSession + ";strTransaction:" + strTransaction + ";vintTipoTra:" + vintTipoTra); 
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
            Claro.Web.Logging.Info("LTEGetOrderType", "LTEPlanMigration", "Finalizó GetOrderType");
            return list;
        }


        public static List<Entity.Transac.Service.Fixed.OrderSubType> GetOrderSubType(string strIdSession, string strTransaction, string vintTipoOrden)
        {
            Claro.Web.Logging.Info("LTEGetOrderSubType", "LTEPlanMigration", "Entro GetOrderSubType");
            Claro.Web.Logging.Info("LTEGetNewPlans", "LTEPlanMigration", "los parametros que recibe el metodo son: strIdSession:" + strIdSession + ";strTransaction:" + strTransaction + ";vintTipoOrden:" + vintTipoOrden); 
            List<Entity.Transac.Service.Fixed.OrderSubType> list = null;
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("p_cursor", DbType.Object, ParameterDirection.Output),
                new DbParameter("av_cod_tipo_orden", DbType.String,255, ParameterDirection.Input, vintTipoOrden)
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
            Claro.Web.Logging.Info("LTEGetOrderSubType", "LTEPlanMigration", "Finalizó GetOrderSubType");
            return list;
        }
        public static Entity.Transac.Service.Fixed.ETAAuditoriaCapacity GetGroupCapacity(string strIdSession, string pIdTrasaccion, string pIP_APP, string pAPP, string pUsuario,
            DateTime[] vFechas, string[] vUbicacion, bool vCalcDur, bool vCalcDurEspec,
           bool vCalcTiempoViaje, bool vCalcTiempoViajeEspec, bool vCalcHabTrabajo, bool vCalcHabTrabajoEspec,
            bool vObtenerUbiZona, bool vObtenerUbiZonaEspec, string[] vEspacioTiempo, string[] vHabilidadTrabajo,
            BEETACampoActivity[] vCampoActividad, BEETAListaParamRequestOpcionalCapacity[] vListaCapReqOpc)
        {
            Claro.Web.Logging.Info("LTEGetGroupCapacity", "LTEPlanMigration", "Entro GetGroupCapacity");
            Claro.Web.Logging.Info("LTEGetGroupCapacity", "LTEPlanMigration", "los parametros que reciben el metodo son: strIdSession:" + strIdSession + ";pIdTrasaccion:" + pIdTrasaccion 
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

            //Claro.SIACU.ProxyService.Transac.Service.SIACLTE.GroupManagement.AuditResponse objResponseCuadrillas = new Claro.SIACU.ProxyService.Transac.Service.SIACLTE.GroupManagement.AuditResponse();

            //try
            //{
            //    Claro.SIACU.ProxyService.Transac.Service.SIACLTE.GroupManagement.ebsADMCUAD_CapacityService
            //     objServicioCuadrillas = new Claro.SIACU.ProxyService.Transac.Service.SIACLTE.GroupManagement.ebsADMCUAD_CapacityService();
            //    objServicioCuadrillas.Url = ConfigurationManager.AppSettings("strWebServEtaDirectWebService").ToString();
            //    objServicioCuadrillas.Credentials = System.Net.CredentialCache.DefaultCredentials;
            //    Claro.SIACU.ProxyService.Transac.Service.SIACLTE.GroupManagement.AuditRequest AuditRequestCuadrillas = new Claro.SIACU.ProxyService.Transac.Service.SIACLTE.GroupManagement.AuditRequest();
            //    AuditRequestCuadrillas.idTransaccion = pIdTrasaccion;
            //    AuditRequestCuadrillas.ipAplicacion = pIP_APP;
            //    AuditRequestCuadrillas.nombreAplicacion = pAPP;
            //    AuditRequestCuadrillas.usuarioAplicacion = pUsuario;

            //    //log.Info(string.Format("URL: {0}", objServicioCuadrillas.Url));
            //    Claro.SIACU.ProxyService.Transac.Service.SIACLTE.GroupManagement.campoActividadType[] ListaCapActiRequestCuadrillas = new Claro.SIACU.ProxyService.Transac.Service.SIACLTE.GroupManagement.campoActividadType[vCampoActividad.Length];


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
            //            Claro.SIACU.ProxyService.Transac.Service.SIACLTE.GroupManagement.campoActividadType CampoActividadRequestCuadrillas = new Claro.SIACU.ProxyService.Transac.Service.SIACLTE.GroupManagement.campoActividadType();
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
            //    Claro.SIACU.ProxyService.Transac.Service.SIACLTE.GroupManagement.parametrosRequest oIniParamRequestCuadrillas = new Claro.SIACU.ProxyService.Transac.Service.SIACLTE.GroupManagement.parametrosRequest();
            //    Claro.SIACU.ProxyService.Transac.Service.SIACLTE.GroupManagement.parametrosRequest[] oParamRequestCuadrillas = new Claro.SIACU.ProxyService.Transac.Service.SIACLTE.GroupManagement.parametrosRequest[] { oIniParamRequestCuadrillas };

            //    Claro.SIACU.ProxyService.Transac.Service.SIACLTE.GroupManagement.parametrosRequestObjetoRequestOpcional[] ListaParamReqOpcionalCuadrillas = new Claro.SIACU.ProxyService.Transac.Service.SIACLTE.GroupManagement.parametrosRequestObjetoRequestOpcional[vListaCapReqOpc.Length];

            //    if (vListaCapReqOpc.Length > 0)
            //    {

            //        //log.Info(string.Format("ListaRequestOpcionalCapacity(), Elementos: {0}", vListaCapReqOpc.Length));

            //        int j = 0, k = 0;
            //        foreach (BEETAListaParamRequestOpcionalCapacity oListaParReq in vListaCapReqOpc)
            //        {

            //            foreach (BEETAParamRequestCapacity oParamReqCapacity in oListaParReq.ParamRequestCapacities)
            //            {
            //                Claro.SIACU.ProxyService.Transac.Service.SIACLTE.GroupManagement.parametrosRequestObjetoRequestOpcional oParamRequestOpcionalCuadrillas = new Claro.SIACU.ProxyService.Transac.Service.SIACLTE.GroupManagement.parametrosRequestObjetoRequestOpcional();
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

            //    Claro.SIACU.ProxyService.Transac.Service.SIACLTE.GroupManagement.capacidadType[] ListaCapacidadTypeCuadrillas = new Claro.SIACU.ProxyService.Transac.Service.SIACLTE.GroupManagement.capacidadType[0];
            //    Claro.SIACU.ProxyService.Transac.Service.SIACLTE.GroupManagement.parametrosResponse[] ListaParamResponseCuadrillas = new Claro.SIACU.ProxyService.Transac.Service.SIACLTE.GroupManagement.parametrosResponse[0];

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
            //    foreach (Claro.SIACU.ProxyService.Transac.Service.SIACLTE.GroupManagement.capacidadType oEntCapacidadType in ListaCapacidadTypeCuadrillas)
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
            Claro.Web.Logging.Info("LTEGetGroupCapacity", "LTEPlanMigration", "Finalizó GetGroupCapacity");
            return Resp;


        }

        public static List<Entity.Transac.Service.Fixed.TimeZone> GetTimeZones(string strIdSession, string strTransaction, string vstrCoUbi, string vstrTipTra, string vstrFecha)
        {
            Claro.Web.Logging.Info("LTEGetTimeZones", "LTEPlanMigration", "Entro GetTimeZones");
            Claro.Web.Logging.Info("LTEGetTimeZones", "LTEPlanMigration", "los parametros que recibe el metodo son: strIdSession:" + strIdSession + ";strTransaction:" + strTransaction + ";vstrCoUbi:" + vstrCoUbi + ";vstrTipTra:" + vstrTipTra + ";vstrFecha:" + vstrFecha); 
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
            Claro.Web.Logging.Info("LTEGetTimeZones", "LTEGetTimeZones", "Finalizó GetTimeZones");
            return list;
        }
        public static OsbLteEntity ExecutePlanMigrationLte(string strIdSession, string strTransaction, string TransactionId, List<ServiceByPlan> ServicesList, TypificationItem tipification, ClientParameters clientParameters, MainParameters MainParameters, PlusParameters PlusParameters, EtaSelection EtaSelection, SotParameters SotParameters, EtaParameters EtaParameters, Contract Contract, ActualizarTipificacion ActualizarTipificacion, bool FlagContingencia, bool FlagCrearPlantilla, AuditRegister AuditRegister, List<Coser> ListCoser, bool FlagValidaEta, string ParametrosConstancia, string DestinatarioCorreo, string Notes, string strTipoPlan, string strCodPlan, string strTmCode, string strTipoProducto, string strCodServicioGeneralTope, double dblMontoTopeConsumo, double dblTopeConsumo, string strComentTopeConsumo, double dblLimiteCredito, string strAnotationToa)
        {
            Web.Logging.Info(strIdSession, strTransaction, "ExecutePlanMigrationLTE: iniciando metodo");
            OsbLteEntity OsbLteEntity = new OsbLteEntity();
            OsbLteEntity.result = string.Empty;
            HeaderRequestType oHeaderRequest = new HeaderRequestType
            {
                canal = "pruebaLTE",
                idAplicacion = "1",
                usuarioAplicacion = "",
                usuarioSesion = "",
                idTransaccionESB = "123",
                idTransaccionNegocio = TransactionId
            };
            Web.Logging.Info(strIdSession, strTransaction, "ExecutePlanMigrationLTE: DESPUES DE SETEAR oHeaderRequest");

            serviciosTypeLTEServiciosObject[] listServices = (from ele in ServicesList
                                                              select new serviciosTypeLTEServiciosObject
                                                              {
                                                                  cantidad = ele.CantEquipment,
                                                                  cf = ele.CF,
                                                                  codigoServicio = ele.CodServSisact,
                                                                  equipo = ele.Equipment,
                                                                  grupoServ = ele.CodGroupServ,
                                                                  nombreServ = ele.DesServSisact,
                                                                  snCode = ele.Sncode,
                                                                  spCode = ele.Spcode,
                                                                  tipoServ = ele.ServiceType

                                                           }).ToArray();
            Web.Logging.Info(strIdSession, strTransaction, "ExecutePlanMigrationLTE: DESPUES DE SETEAR listServices");

            obtenerClientesType ParametrosCliente = new obtenerClientesType()
            {

                account = clientParameters.straccount,
                contactObjId = clientParameters.strcontactObjId,
                flagReg = clientParameters.strflagReg,
                msisdn = clientParameters.strmsisdn
            };
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
            parametrosPlusType parametrosPlus = new parametrosPlusType()
            {
                address = PlusParameters.strAddress,
                address5 = PlusParameters.strAddress5,
                adjustmentAmount = PlusParameters.strAdjustmentAmount,
                adjustmentReason = PlusParameters.strAdjustmentReason,
                amountUnit = PlusParameters.strAmountUnit.Length <= 20 ? PlusParameters.strAmountUnit : PlusParameters.strAmountUnit.Substring(0, 20),
                basket = PlusParameters.strBasket,
                birthday = PlusParameters.strBirthday,
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
                expireDate = PlusParameters.strExpireDate,
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
                oldClaroLdn2 = (PlusParameters.strOldClaroLdn2==null || PlusParameters.strOldClaroLdn2.Length <= 20) ? PlusParameters.strOldClaroLdn2 : PlusParameters.strOldClaroLdn2.Substring(0, 20),
                oldClaroLdn3 = PlusParameters.strOldClaroLdn3,
                oldClaroLdn4 = PlusParameters.strOldClaroLdn4,
                oldClaroLocal1 = PlusParameters.strOldClaroLocal1.Length <= 20 ? PlusParameters.strOldClaroLocal1 : PlusParameters.strOldClaroLocal1.Substring(0, 20),
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
            parametrosSotTypeLTE ParametrosSot = new parametrosSotTypeLTE()
            {
                cargo = string.Empty,
                codigoOcc = string.Empty,
                codCaso = string.Empty,
                codId = SotParameters.strCoId,
                codIntercaso = string.Empty,
                codeDif = string.Empty,
                codMotot = SotParameters.strCodMotot,
                codUsu = SotParameters.strUsrRegistro,
                codZona = string.Empty,
                customerId = SotParameters.strCustomerId,
                fecProg = SotParameters.strFechaProgramada,
                franja = SotParameters.strFranja,
                franjaHor = SotParameters.strFranjaHor,
                lote = string.Empty,
                manzana = string.Empty,
                nomVia = string.Empty,
                nomUrb = string.Empty,
                numVia = string.Empty,
                observacion = string.Empty,
                centroPoblado = string.Empty,
                referencia = string.Empty,
                tipUrb = string.Empty,
                tipoTrans = SotParameters.strTransTipo,
                tipoVia = string.Empty,
                tiposervico = string.Empty,
                tipTra = SotParameters.strTiptra,
                ubigeo = string.Empty,
                reclamoCaso = string.Empty,
                flagActDirFact = string.Empty,
                numCarta = string.Empty,
                operador = SotParameters.strOperador,
                presuscrito = string.Empty,
                publicar = string.Empty,
                adTmcode = string.Empty,
                listaCoser = string.Empty,
                listaSpCode = string.Empty,
                cantidad = string.Empty
            };
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
            serviciosTypeLTEServiciosObject[] servicios = (from ele in ServicesList
                                                        select new serviciosTypeLTEServiciosObject
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


            ejecutarCambioPlanRequest oRequest = new ejecutarCambioPlanRequest();

            oRequest.ejecutarCambioPlanFijaRequest = new ejecutarCambioPlanFijaRequest();
            oRequest.headerRequest = oHeaderRequest;
            oRequest.ejecutarCambioPlanFijaRequest.tipoProducto = strTipoProducto;
            oRequest.ejecutarCambioPlanFijaRequest.datosCambioPlanLTERequest = new DatosCambioPlanLTERequestType();
            oRequest.ejecutarCambioPlanFijaRequest.datosCambioPlanLTERequest.listaServicios = listServices;
            oRequest.ejecutarCambioPlanFijaRequest.datosCambioPlanLTERequest.parametrosCliente = ParametrosCliente;
            oRequest.ejecutarCambioPlanFijaRequest.datosCambioPlanLTERequest.parametrosPrincipal = parametrosPrincipal;
            oRequest.ejecutarCambioPlanFijaRequest.datosCambioPlanLTERequest.parametrosPlus = parametrosPlus;
            oRequest.ejecutarCambioPlanFijaRequest.datosCambioPlanLTERequest.parametrosSot = ParametrosSot;
            oRequest.ejecutarCambioPlanFijaRequest.datosCambioPlanLTERequest.contratos = ContratosType;
            oRequest.ejecutarCambioPlanFijaRequest.datosCambioPlanLTERequest.actualizarTipificacion = oActualizarTipificacion;
            oRequest.ejecutarCambioPlanFijaRequest.datosCambioPlanLTERequest.registroAuditoria = oRegistroAuditoria;
            oRequest.ejecutarCambioPlanFijaRequest.datosCambioPlanLTERequest.flagContingencia = (FlagContingencia ? 1 : 0).ToString();
            oRequest.ejecutarCambioPlanFijaRequest.datosCambioPlanLTERequest.flagCrearPlantilla = (FlagCrearPlantilla ? 1 : 0).ToString();
            oRequest.ejecutarCambioPlanFijaRequest.datosCambioPlanLTERequest.parametrosConstancia = ParametrosConstancia;
            oRequest.ejecutarCambioPlanFijaRequest.datosCambioPlanLTERequest.destinatarioCorreo = DestinatarioCorreo;
            oRequest.ejecutarCambioPlanFijaRequest.datosCambioPlanLTERequest.notas = Notes;
            oRequest.ejecutarCambioPlanFijaRequest.datosCambioPlanLTERequest.listaCoser = oListaCoser;
            oRequest.ejecutarCambioPlanFijaRequest.datosCambioPlanLTERequest.tipoPlan = strTipoPlan;
            oRequest.ejecutarCambioPlanFijaRequest.datosCambioPlanLTERequest.codPlan = strCodPlan;
            oRequest.ejecutarCambioPlanFijaRequest.datosCambioPlanLTERequest.tmCode = strTmCode;
            HeaderResponseType oHeaderResponse = new HeaderResponseType();
            ejecutarCambioPlanResponse oResponse = new ejecutarCambioPlanResponse();
            Web.Logging.Info(strIdSession, strTransaction, "LOG_PRUEBA_OSB_REQUEST_LTE: " + JsonConvert.SerializeObject(oRequest));
            string strFaultMsg = string.Empty;

            #region Trama

            string fieldSeparator = Constant.PresentationLayer.gstrVariablePipeline;
            string fieldSeparatorEnd = Constant.PresentationLayer.gstrPuntoyComa;
            var strHeader = new System.Text.StringBuilder();
            strHeader.Append(ParametrosSot.codId);
            strHeader.Append(fieldSeparator);
            strHeader.Append(ParametrosSot.customerId);
            strHeader.Append(fieldSeparator);
            strHeader.Append(string.Empty);
            strHeader.Append(fieldSeparator);
            strHeader.Append(ParametrosSot.tipoTrans);
            strHeader.Append(fieldSeparator);
            strHeader.Append(ParametrosSot.tipTra);
            strHeader.Append(fieldSeparator);
            strHeader.Append(ParametrosSot.codIntercaso);
            strHeader.Append(fieldSeparator);
            strHeader.Append(ParametrosSot.codMotot);
            strHeader.Append(fieldSeparator);
            strHeader.Append(ParametrosSot.tipoVia);
            strHeader.Append(fieldSeparator);
            strHeader.Append(ParametrosSot.nomVia);
            strHeader.Append(fieldSeparator);
            strHeader.Append(ParametrosSot.numVia);
            strHeader.Append(fieldSeparator);
            strHeader.Append(ParametrosSot.tipUrb);
            strHeader.Append(fieldSeparator);
            strHeader.Append(ParametrosSot.nomUrb);
            strHeader.Append(fieldSeparator);
            strHeader.Append(ParametrosSot.manzana);
            strHeader.Append(fieldSeparator);
            strHeader.Append(ParametrosSot.lote);
            strHeader.Append(fieldSeparator);
            strHeader.Append(ParametrosSot.ubigeo);
            strHeader.Append(fieldSeparator);
            strHeader.Append(ParametrosSot.codZona);
            strHeader.Append(fieldSeparator);
            strHeader.Append(string.Empty);
            strHeader.Append(fieldSeparator);
            strHeader.Append(string.Empty);
            strHeader.Append(fieldSeparator);
            strHeader.Append(ParametrosSot.referencia);
            strHeader.Append(fieldSeparator);
            strHeader.Append(ParametrosSot.fecProg);
            strHeader.Append(fieldSeparator);
            strHeader.Append(ParametrosSot.franjaHor);
            strHeader.Append(fieldSeparator);
            strHeader.Append(ParametrosSot.numCarta);
            strHeader.Append(fieldSeparator);
            strHeader.Append(ParametrosSot.operador);
            strHeader.Append(fieldSeparator);
            strHeader.Append(ParametrosSot.presuscrito);
            strHeader.Append(fieldSeparator);
            strHeader.Append(ParametrosSot.publicar);
            strHeader.Append(fieldSeparator);
            strHeader.Append(ParametrosSot.adTmcode);
            strHeader.Append(fieldSeparator);
            strHeader.Append(ParametrosSot.codUsu);
            strHeader.Append(fieldSeparator);
            strHeader.Append(ParametrosSot.cargo);
            strHeader.Append(fieldSeparator);
            strHeader.Append(ParametrosSot.codIntercaso);
            strHeader.Append(fieldSeparator);
            strHeader.Append(ParametrosSot.tiposervico);
            strHeader.Append(fieldSeparator);
            strHeader.Append(string.Empty);
            strHeader.Append(fieldSeparator);
            strHeader.Append(strTipoProducto);
            strHeader.Append(fieldSeparator);
            strHeader.Append(strCodServicioGeneralTope);
            strHeader.Append(fieldSeparator);
            strHeader.Append(string.Empty);
            strHeader.Append(fieldSeparator);
            strHeader.Append(dblMontoTopeConsumo);
            strHeader.Append(fieldSeparator);
            strHeader.Append(string.Empty);
            strHeader.Append(fieldSeparator);
            strHeader.Append(dblTopeConsumo);
            strHeader.Append(fieldSeparator);
            strHeader.Append(dblLimiteCredito);
            strHeader.Append(fieldSeparator);
            strHeader.Append(strAnotationToa);
            strHeader.Append(fieldSeparator);
            strHeader.Append(parametrosPrincipal.notas);
            strHeader.Append(fieldSeparatorEnd);
            string trama = strHeader.ToString();

            var strBody = new System.Text.StringBuilder();
            foreach (var item in ServicesList)
            {
                strBody.Append(item.CodServSisact);
                strBody.Append(fieldSeparator);
                strBody.Append(string.Empty);
                strBody.Append(fieldSeparator);
                strBody.Append(item.CodGroupServ);
                strBody.Append(fieldSeparator);
                strBody.Append(string.Empty);
                strBody.Append(fieldSeparator);
                strBody.Append(item.DesServSisact);
                strBody.Append(fieldSeparator);
                strBody.Append(string.Empty);
                strBody.Append(fieldSeparator);
                strBody.Append(string.Empty);
                strBody.Append(fieldSeparator);
                strBody.Append(item.Tipequ);
                strBody.Append(fieldSeparator);
                strBody.Append(item.Codtipequ);
                strBody.Append(fieldSeparator);
                strBody.Append(item.CantEquipment);
                strBody.Append(fieldSeparator);
                strBody.Append(item.Dscequ);
                strBody.Append(fieldSeparator);
                strBody.Append(string.Empty);
                strBody.Append(fieldSeparator);
                strBody.Append(DateTime.Now);
                strBody.Append(fieldSeparator);
                strBody.Append(SotParameters.strUsrRegistro);
                strBody.Append(fieldSeparator);
                strBody.Append(item.Sncode);
                strBody.Append(fieldSeparator);
                strBody.Append(item.Spcode);
                strBody.Append(fieldSeparator);
                strBody.Append(item.CF);
                strBody.Append(fieldSeparator);
                strBody.Append(string.Empty);//18
                strBody.Append(fieldSeparator);
                strBody.Append(string.Empty);//19
                strBody.Append(fieldSeparator);
                strBody.Append(string.Empty);
                strBody.Append(fieldSeparator);
                strBody.Append(string.Empty);
                strBody.Append(fieldSeparatorEnd);
            };
            string trama2 = strBody.ToString();

            var strLstTipequ = new System.Text.StringBuilder();
            foreach (var item in ServicesList)
            {
                strLstTipequ.Append(item.Tipequ);
                strLstTipequ.Append(fieldSeparator);
            };
            strLstTipequ.Append(fieldSeparatorEnd);

            var strLstCoser = new System.Text.StringBuilder();
            foreach (var item in ServicesList)
            {
                strLstCoser.Append(item.CodServSisact);
                strLstCoser.Append(fieldSeparator);
            };
            strLstCoser.Append(fieldSeparatorEnd);

            var strLstSnCode = new System.Text.StringBuilder();
            foreach (var item in ServicesList)
            {
                strLstSnCode.Append(item.Sncode);
                strLstSnCode.Append(fieldSeparator);
            };
            strLstSnCode.Append(fieldSeparatorEnd);

            var strLstSpCode = new System.Text.StringBuilder();
            foreach (var item in ServicesList)
            {
                strLstSpCode.Append(item.Spcode);
                strLstSpCode.Append(fieldSeparator);
            };
            strLstSpCode.Append(fieldSeparatorEnd);

            Web.Logging.Info(strIdSession, strTransaction, "LOG_PRUEBA_OSB_TRAMA_HEADER: " + strHeader.ToString());
            Web.Logging.Info(strIdSession, strTransaction, "LOG_PRUEBA_OSB_TRAMA_BODY: " + strBody.ToString());
            Web.Logging.Info(strIdSession, strTransaction, "LOG_PRUEBA_OSB_TRAMA_lstTIPEQU: " + strLstTipequ.ToString());
            Web.Logging.Info(strIdSession, strTransaction, "LOG_PRUEBA_OSB_TRAMA_lstCOSER: " + strLstCoser.ToString());
            Web.Logging.Info(strIdSession, strTransaction, "LOG_PRUEBA_OSB_TRAMA_lstSPCODE: " + strLstSpCode.ToString());
            Web.Logging.Info(strIdSession, strTransaction, "LOG_PRUEBA_OSB_TRAMA_LSTSNCODE: " + strLstSnCode.ToString());
            #endregion

            oResponse = Claro.Web.Logging.ExecuteMethod<ejecutarCambioPlanResponse>(strIdSession, strTransaction, () =>
            {
                try
                {
                    return Configuration.ServiceConfiguration.SiacFixedPlanMigration.ejecutarCambioPlan(oRequest);
                }
                catch (FaultException<ClaroFault> ex)
                {
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
                    OsbLteEntity.result = ex.Message;
                    return null;
                    throw new Exception(strFaultMsg);
                }

            });
            Web.Logging.Info(strIdSession, strTransaction, "LOG_PRUEBA_OSB_REQUEST_LTE_RESPONSE: " + JsonConvert.SerializeObject(oResponse));

            if (oResponse != null && oResponse.ejecutarCambioPlanFijaResponse!=null && oResponse.ejecutarCambioPlanFijaResponse.responseStatus.estado==0)
            {
                OsbLteEntity.result = oResponse.ejecutarCambioPlanFijaResponse.responseStatus.descripcionRespuesta;
                OsbLteEntity.InteractionCode = oResponse.ejecutarCambioPlanFijaResponse.responseData.datosCambioPlanLTEResponse.codigoInteraccion;
                OsbLteEntity.SotNumber = oResponse.ejecutarCambioPlanFijaResponse.responseData.datosCambioPlanLTEResponse.nroSot;
                OsbLteEntity.ConstancyRoute = oResponse.ejecutarCambioPlanFijaResponse.responseData.datosCambioPlanLTEResponse.rutaConstancia;
            }
            else if (OsbLteEntity.result == string.Empty && oResponse.ejecutarCambioPlanFijaResponse == null)
            {
                OsbLteEntity.result = String.Empty;
            }
            Claro.Web.Logging.Info(strIdSession, strTransaction, JsonConvert.SerializeObject(OsbLteEntity));
            return OsbLteEntity;
        }

        public static CurrentPlan GetCurrentPlan(string strIdSession, string strTransaction, string strCoId)
        {
            Claro.Web.Logging.Info("LTEGetCurrentPlan", "LTEPlanMigration", "Entro GetCurrentPlan");
            Claro.Web.Logging.Info("LTEGetCurrentPlan", "LTEPlanMigration", "los parametros que recieb el metodod son: strIdSession:" + strIdSession + ";strTransaction:" + strTransaction + ";strCoId:" + strCoId); 
            CurrentPlan oAddress;

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("CO_ID", DbType.String, ParameterDirection.Input,strCoId),                
                new DbParameter("PLAN_ACTUAL", DbType.Object, ParameterDirection.Output)
            };

            oAddress = new CurrentPlan();
            Claro.Web.Logging.Info(strIdSession, strTransaction, JsonConvert.SerializeObject(oAddress));
            Claro.Web.Logging.Info("LTEGetCurrentPlan", "LTEPlanMigration", "Finalizó GetCurrentPlan");
            return oAddress;
        }

        public static bool SendNewPlanServices(string strIdSession, string strTransaction, List<ServiceByPlan> services)
        {
            Claro.Web.Logging.Info("LTESendNewPlanServices", "LTEPlanMigration", "Entro SendNewPlanServices");
            Claro.Web.Logging.Info("LTESendNewPlanServices", "LTEPlanMigration", "los parametros que recibe el metodo son: strIdSession:" + strIdSession + ";strTransaction:" + strTransaction + ";services:" + services.ToString()); 
            bool oAddress = false;
            Claro.Web.Logging.Info(strIdSession, strTransaction, JsonConvert.SerializeObject(oAddress));
            Claro.Web.Logging.Info("LTESendNewPlanServices", "LTEPlanMigration", "Finalizó SendNewPlanServices");
            return oAddress;
        }

        public static List<ServiceByCurrentPlan> GetServicesByCurrentPlan(string strIdSession, string strTransaction, string strContractId)
        {
            Claro.Web.Logging.Info("LTEGetServicesByCurrentPlan", "LTEPlanMigration", "Entro GetServicesByCurrentPlan");
            List<ServiceByCurrentPlan> ServicesByCurrentPlan;
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("p_co_id", DbType.String, ParameterDirection.Input,strContractId),
                new DbParameter("p_tmdes", DbType.String, 255, ParameterDirection.Output),
                new DbParameter("p_cursor", DbType.Object, ParameterDirection.Output),
                new DbParameter("v_errnum", DbType.String, 255, ParameterDirection.Output),
                new DbParameter("v_errmsj", DbType.String, 255, ParameterDirection.Output)

            };
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
            Claro.Web.Logging.Info("LTEGetServicesByCurrentPlan", "LTEPlanMigration", "Finalizó GetServicesByCurrentPlan");
            return ServicesByCurrentPlan;
        }
        public static List<EquipmentByCurrentPlan> GetEquipmentByCurrentPlan(string strIdSession, string strTransaction, string strIdContract)
        {
            Claro.Web.Logging.Info("LTEGetEquipmentByCurrentPlan", "LTEPlanMigration", "Entro GetEquipmentByCurrentPlan");
            Claro.Web.Logging.Info("LTEGetEquipmentByCurrentPlan", "LTEPlanMigration", "los parametros que recibe el metodo son: strIdSession:" + strIdSession + ";strTransaction:" + strTransaction + ";strIdContract:" + strIdContract);
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
            catch(Exception ex){
                EquipmentsByCurrentPlan = null;
                Claro.Web.Logging.Error(strIdSession, strTransaction, Claro.SIACU.Transac.Service.Functions.GetExceptionMessage(ex));
            }
            
            Claro.Web.Logging.Info(strIdSession, strTransaction, JsonConvert.SerializeObject(EquipmentsByCurrentPlan));
            Claro.Web.Logging.Info("LTEGetEquipmentByCurrentPlan", "LTEPlanMigration", "Finalizó GetEquipmentByCurrentPlan");
            return EquipmentsByCurrentPlan;
        }
        public static TechnicalVisit GetTechnicalVisitResult(string strIdSession, string strTransaction, string strCoId, string strCustomerId, string strTmCode, string strCodPlanSisact, string strTrama)
        {
            Claro.Web.Logging.Info("LTEGetTechnicalVisitResult", "LTEPlanMigration", "Entro GetTechnicalVisitResult");
            Claro.Web.Logging.Info("LTEGetTechnicalVisitResult", "LTEPlanMigration", "los parametros que recibe el metodo son: strIdSession:" + strIdSession + ";strTransaction:" + strTransaction + ";strCoId:" + strCoId +
                ";strCustomerId:" + strCustomerId + ";strTmCode:" + strTmCode + ";strCodPlanSisact:" + strCodPlanSisact + ";strTrama:" + strTrama);
            bool result = false;
            TechnicalVisit technicalVisit = new TechnicalVisit();
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("AN_COD_ID", DbType.Int32, ParameterDirection.Input,strCoId),
                new DbParameter("AN_CUSTOMER_ID",DbType.Int32,ParameterDirection.Input,strCustomerId),
                new DbParameter("AN_TMCODE",DbType.Int32,ParameterDirection.Input,strTmCode),
                new DbParameter("AN_COD_PLAN_SISACT",DbType.Int32,ParameterDirection.Input,strCodPlanSisact),
                new DbParameter("AV_TRAMA",DbType.String,4000,ParameterDirection.Input,strTrama),
                new DbParameter("AN_FLG_VISITA",DbType.Int32,ParameterDirection.Output),
                new DbParameter("AN_CODMOTOT",DbType.Int32,ParameterDirection.Output),
                new DbParameter("AN_ERROR",DbType.Int32,ParameterDirection.Output),
                new DbParameter("AV_ERROR", DbType.String,255, ParameterDirection.Output),
                new DbParameter("AV_ANOTACION", DbType.String,255, ParameterDirection.Output),
                new DbParameter("AV_SUBTIPO", DbType.String,255, ParameterDirection.Output),
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
            Claro.Web.Logging.Info(strIdSession, strTransaction, JsonConvert.SerializeObject(technicalVisit));
            Claro.Web.Logging.Info("LTEGetTechnicalVisitResult", "LTEPlanMigration", "Finalizó GetTechnicalVisitResult");
            return technicalVisit;
        }
        public static Entity.Transac.Service.Fixed.GetExecutePlanMigrationLTE.ExecutePlanMigrationLTEResponse ExecutePlanMigrationLTE(Entity.Transac.Service.Fixed.GetExecutePlanMigrationLTE.ExecutePlanMigrationLTERequest objRequest)
        {
            Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "ExecutePlanMigrationLTE: INICIA EL CAMBIO DE PLAN");
            string strFaultMsg = string.Empty;
            CambioPlanLTEWSService.HeaderResponse objHeaderResponse = new CambioPlanLTEWSService.HeaderResponse();
            CambioPlanLTEWSService.HeaderRequest objHeaderRequest = new CambioPlanLTEWSService.HeaderRequest()
            {
                channel = "Cambio de Plan LTE",
                idApplication = Constant.strUno,
                userApplication = objRequest.Audit.UserName,
                userSession = objRequest.Audit.UserName,
                idESBTransaction = Constant.strUno + Constant.strDos + Constant.strTres,
                idBusinessTransaction = objRequest.Audit.Transaction,
                startDate = DateTime.Now
            };
            Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "ExecutePlanMigrationLTE: DESPUES DE SETEAR HeaderRequest");
            CambioPlanLTEWSService.RegistroCabeceraType objRegistroCabecera = new CambioPlanLTEWSService.RegistroCabeceraType()
            {
                tramaCab = objRequest.objSotParametersLTE.strTramaCab,
                lstTipEqu = objRequest.objSotParametersLTE.strLstTipEqu,
                lstCoser = objRequest.objSotParametersLTE.strLstCoser,
                lstSnCode = objRequest.objSotParametersLTE.strLstSnCode,
                lstSpCode = objRequest.objSotParametersLTE.strLstSpCode
            };
            Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "ExecutePlanMigrationLTE: DESPUES DE SETEAR objRegistroCabecera");
            CambioPlanLTEWSService.RegistroDetalleType objRegistroDetalle = new CambioPlanLTEWSService.RegistroDetalleType()
            {
                trama = objRequest.objSotParametersLTE.strTramaBody
            };
            Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "ExecutePlanMigrationLTE: DESPUES DE SETEAR objRegistroDetalle");
            CambioPlanLTEWSService.ParametrosSotType objSotParameters = new CambioPlanLTEWSService.ParametrosSotType()
            {
                registroCabecera = objRegistroCabecera,
                registroDetalle = objRegistroDetalle
            };
            Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "ExecutePlanMigrationLTE: DESPUES DE SETEAR objSotParameters");
            CambioPlanLTEWSService.ServicioType[] lstListServices = (from ele in objRequest.lstServicesList
                                                                     select new CambioPlanLTEWSService.ServicioType
                                                                     {
                                                                         cantidad = ele.CantEquipment,
                                                                         cf = ele.CF,
                                                                         codigoServicio = ele.CodServSisact,
                                                                         equipo = ele.Equipment,
                                                                         grupoServicio = ele.CodGroupServ,
                                                                         nombreServicio = ele.DesServSisact,
                                                                         snCode = ele.Sncode,
                                                                         spCode = ele.Spcode,
                                                                         tipoServicio = ele.ServiceType

                                                                     }).ToArray();
            Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "ExecutePlanMigrationLTE: DESPUES DE SETEAR listServices");

            CambioPlanLTEWSService.ParametrosClienteType objParametrosCliente = new CambioPlanLTEWSService.ParametrosClienteType()
            {

                account = objRequest.objClientParameters.straccount,
                contactObjId = objRequest.objClientParameters.strcontactObjId,
                flagReg = objRequest.objClientParameters.strflagReg,
                msisdn = objRequest.objClientParameters.strmsisdn
            };
            Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "ExecutePlanMigrationLTE: DESPUES DE SETEAR objParametrosCliente");
            CambioPlanLTEWSService.ParametrosPrincipalType objParametrosPrincipal = new CambioPlanLTEWSService.ParametrosPrincipalType()
            {
                agente = objRequest.objMainParameters.strAgent,
                clase = objRequest.objMainParameters.strClass,
                codPlano = objRequest.objMainParameters.strCodPlan,
                coId = objRequest.objMainParameters.strCoId,
                flagCaso = objRequest.objMainParameters.strFlagCase,
                hechoEnUno = objRequest.objMainParameters.strMadeInOne,
                inconven = objRequest.objMainParameters.strInconven,
                inconvenCode = objRequest.objMainParameters.strInconvenCode,
                metodoContacto = objRequest.objMainParameters.strContactMethod,
                notas = objRequest.objMainParameters.strNotes,
                resultado = objRequest.objMainParameters.strResult,
                servafect = objRequest.objMainParameters.strServAfect,
                servafectCode = objRequest.objMainParameters.strServAfectCode,
                subclase = objRequest.objMainParameters.strSubClass,
                tipo = objRequest.objMainParameters.strType,
                tipoInter = objRequest.objMainParameters.strInterType,
                usrProceso = objRequest.objMainParameters.strUserProcess,
                valor1 = objRequest.objMainParameters.strValueOne,
                valor2 = objRequest.objMainParameters.trValueTwo,
                phone = objRequest.objClientParameters.strmsisdn
            };
            Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "ExecutePlanMigrationLTE: DESPUES DE SETEAR objParametrosPrincipal");
            CambioPlanLTEWSService.ParametrosPlusType objParametrosPlus = new CambioPlanLTEWSService.ParametrosPlusType()
            {
                address = objRequest.objPlusParameters.strAddress,
                address5 = objRequest.objPlusParameters.strAddress5,
                adjustmentAmount = objRequest.objPlusParameters.strAdjustmentAmount,
                adjustmentReason = objRequest.objPlusParameters.strAdjustmentReason,
                amountUnit = objRequest.objPlusParameters.strAmountUnit.Length <= 20 ? objRequest.objPlusParameters.strAmountUnit : objRequest.objPlusParameters.strAmountUnit.Substring(0, 20),
                basket = objRequest.objPlusParameters.strBasket,
                birthday = objRequest.objPlusParameters.strBirthday,
                chargeAmount = objRequest.objPlusParameters.strChargeAmount,
                city = objRequest.objPlusParameters.strCity,
                clarifyInteraction = objRequest.objPlusParameters.strClarifyInteraction,
                claroLdn1 = objRequest.objPlusParameters.strClaroLdn1,
                claroLdn2 = objRequest.objPlusParameters.strClaroLdn2,
                claroLdn3 = objRequest.objPlusParameters.strClaroLdn3,
                claroLdn4 = objRequest.objPlusParameters.strClaroLdn4,
                claroLocal1 = objRequest.objPlusParameters.strClaroLocal1,
                claroLocal2 = objRequest.objPlusParameters.strClaroLocal2,
                claroLocal3 = objRequest.objPlusParameters.strClaroLocal3,
                claroLocal4 = objRequest.objPlusParameters.strClaroLocal4,
                claroLocal5 = objRequest.objPlusParameters.strClaroLocal5,
                claroLocal6 = objRequest.objPlusParameters.strClaroLocal6,
                claroNumber = objRequest.objPlusParameters.strClaroNumber,
                contactPhone = objRequest.objPlusParameters.strContactPhone,
                contactSex = objRequest.objPlusParameters.strContactSex,
                department = objRequest.objPlusParameters.strDepartment,
                district = objRequest.objPlusParameters.strDistrict,
                dniLegalRep = objRequest.objPlusParameters.strDniLegalRep,
                documentNumber = objRequest.objPlusParameters.strDocumentNumber,
                email = objRequest.objPlusParameters.strEmail,
                emailConfirmation = objRequest.objPlusParameters.strEmailConfirmation,
                expireDate = objRequest.objPlusParameters.strExpireDate,
                fax = objRequest.objPlusParameters.strFax,
                firstName = objRequest.objPlusParameters.strFirstName,
                fixedNumber = objRequest.objPlusParameters.strFixedNumber,
                flagChangeUser = objRequest.objPlusParameters.strFlagChangeUser,
                flagCharge = objRequest.objPlusParameters.strFlagCharge,
                flagLegalRep = objRequest.objPlusParameters.strFlagLegalRep,
                flagOther = objRequest.objPlusParameters.strFlagOther,
                flagRegistered = objRequest.objPlusParameters.strFlagRegistered,
                flagTitular = objRequest.objPlusParameters.strFlagTitular,
                iccid = objRequest.objPlusParameters.strIccid,
                imei = objRequest.objPlusParameters.strImei,
                inter1 = objRequest.objPlusParameters.strInter1,
                inter2 = objRequest.objPlusParameters.strInter2,
                inter3 = objRequest.objPlusParameters.strInter3,
                inter4 = objRequest.objPlusParameters.strInter4,
                inter5 = objRequest.objPlusParameters.strInter5,
                inter6 = objRequest.objPlusParameters.strInter6,
                inter7 = objRequest.objPlusParameters.strInter7,
                inter8 = objRequest.objPlusParameters.strInter8,
                inter9 = objRequest.objPlusParameters.strInter9,
                inter10 = objRequest.objPlusParameters.strInter10,
                inter11 = objRequest.objPlusParameters.strInter11,
                inter12 = objRequest.objPlusParameters.strInter12,
                inter13 = objRequest.objPlusParameters.strInter13,
                inter14 = objRequest.objPlusParameters.strInter14,
                inter15 = objRequest.objPlusParameters.strInter15,
                inter16 = objRequest.objPlusParameters.strInter16,
                inter17 = objRequest.objPlusParameters.strInter17,
                inter18 = objRequest.objPlusParameters.strInter18,
                inter19 = objRequest.objPlusParameters.strInter19,
                inter20 = objRequest.objPlusParameters.strInter20,
                inter21 = objRequest.objPlusParameters.strInter21,
                inter22 = objRequest.objPlusParameters.strInter22,
                inter23 = objRequest.objPlusParameters.strInter23,
                inter24 = objRequest.objPlusParameters.strInter24,
                inter25 = objRequest.objPlusParameters.strInter25,
                inter26 = objRequest.objPlusParameters.strInter26,
                inter27 = objRequest.objPlusParameters.strInter27,
                inter28 = objRequest.objPlusParameters.strInter28,
                inter29 = objRequest.objPlusParameters.strInter29,
                inter30 = objRequest.objPlusParameters.strInter30,
                lastName = objRequest.objPlusParameters.strLastName,
                lastNameRep = objRequest.objPlusParameters.strLastNameRep,
                ldiNumber = objRequest.objPlusParameters.strLdiNumber,
                lotCode = objRequest.objPlusParameters.strLotCode,
                maritalStatus = objRequest.objPlusParameters.strMaritalStatus,
                model = objRequest.objPlusParameters.strModel,
                month = objRequest.objPlusParameters.strMonth,
                nameLegalRep = objRequest.objPlusParameters.strNameLegalRep,
                occupation = objRequest.objPlusParameters.strOccupation,
                oldClaroLdn1 = objRequest.objPlusParameters.strOldClaroLdn1,
                oldClaroLdn2 = (objRequest.objPlusParameters.strOldClaroLdn2 == null || objRequest.objPlusParameters.strOldClaroLdn2.Length <= 20) ? objRequest.objPlusParameters.strOldClaroLdn2 : objRequest.objPlusParameters.strOldClaroLdn2.Substring(0, 20),
                oldClaroLdn3 = objRequest.objPlusParameters.strOldClaroLdn3,
                oldClaroLdn4 = objRequest.objPlusParameters.strOldClaroLdn4,
                oldClaroLocal1 = objRequest.objPlusParameters.strOldClaroLocal1.Length <= 20 ? objRequest.objPlusParameters.strOldClaroLocal1 : objRequest.objPlusParameters.strOldClaroLocal1.Substring(0, 20),
                oldClaroLocal2 = objRequest.objPlusParameters.strOldClaroLocal2 == null ? Constant.strCero : objRequest.objPlusParameters.strOldClaroLocal2,
                oldClaroLocal3 = objRequest.objPlusParameters.strOldClaroLocal3 == null ? Constant.strCero : objRequest.objPlusParameters.strOldClaroLocal3,
                oldClaroLocal4 = objRequest.objPlusParameters.strOldClaroLocal4 == null ? Constant.strCero : objRequest.objPlusParameters.strOldClaroLocal4,
                oldClaroLocal5 = objRequest.objPlusParameters.strOldClaroLocal5,
                oldClaroLocal6 = objRequest.objPlusParameters.strOldClaroLocal6 == null ? Constant.strCero : objRequest.objPlusParameters.strOldClaroLocal6,
                oldDocNumber = objRequest.objPlusParameters.strOldDocNumber,
                oldFirstName = objRequest.objPlusParameters.strOldFirstName,
                oldFixedNumber = objRequest.objPlusParameters.strOldFixedNumber,
                oldFixedPhone = objRequest.objPlusParameters.strOldFixedPhone,
                oldLastName = objRequest.objPlusParameters.strOldLastName,
                oldLdiNumber = objRequest.objPlusParameters.strOldLdiNumber,
                operationType = objRequest.objPlusParameters.strOperationType,
                ostNumber = objRequest.objPlusParameters.strOstNumber,
                otherDocNumber = objRequest.objPlusParameters.strOtherDocNumber,
                otherFirstName = objRequest.objPlusParameters.strOtherFirstName,
                otherLastName = objRequest.objPlusParameters.strOtherLastName,
                otherPhone = objRequest.objPlusParameters.strOtherPhone,
                phoneLegalRep = objRequest.objPlusParameters.strPhoneLegalRep,
                plusInter2Interact = objRequest.objPlusParameters.strPlusInter2Interact,
                position = objRequest.objPlusParameters.strPosition,
                reason = objRequest.objPlusParameters.strReason,
                referenceAddress = objRequest.objPlusParameters.strReferenceAddress,
                referencePhone = objRequest.objPlusParameters.strReferencePhone,
                registrationReason = objRequest.objPlusParameters.strRegistrationReason,
                typeDocument = objRequest.objPlusParameters.strTypeDocument,
                zipCode = objRequest.objPlusParameters.strZipCode
            };
            Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "ExecutePlanMigrationLTE: DESPUES DE SETEAR objParametrosPlus");
            CambioPlanLTEWSService.ContratoType objContrato = new CambioPlanLTEWSService.ContratoType()
            {
                planTarifario = 0,
                idSubMercado = long.Parse(objRequest.objContract.ContractList[0].strIdSubmercado),
                idMercado = long.Parse(objRequest.objContract.ContractList[0].strIdMercado),
                red = long.Parse(objRequest.objContract.ContractList[0].strRed),
                estadoUmbral = System.Convert.ToBoolean(objRequest.objContract.ContractList[0].strEstadoUmbral),
                cantidadUmbral = Double.Parse(objRequest.objContract.ContractList[0].strCantidadUmbral),
                archivoLlamadas = System.Convert.ToBoolean(objRequest.objContract.ContractList[0].strArchivoLlamadas),
                listaServicios = (from ele in objRequest.objContract.ContractList[0].ListServices
                                  select new CambioPlanLTEWSService.ServicioContratoType
                                  {
                                      costoServicio = Double.Parse(ele.CamposAdicionalesCargo.strCostoServicio),
                                      periodoCostoServicio = int.Parse(ele.CamposAdicionalesCargo.strPeriodoCostoServicio),
                                      tipoCostoServicio = ele.CamposAdicionalesCargo.strTipoCostoServicio,
                                      costoServicioAvanzado = Double.Parse(ele.CamposAdicionalesDescuento.strCostoServicioAvanzado),
                                      periodoCostoServicioAvanzado = int.Parse(ele.CamposAdicionalesDescuento.strPeriodoCostoServicioAvanzado),
                                      tipoCostoServicioAvanzado = ele.CamposAdicionalesDescuento.strTipoCostoServicioAvanzado,
                                      coId = long.Parse(ele.strCoId),
                                      snCode = long.Parse(ele.strSnCode),
                                      spCode = long.Parse(ele.strSpCode),
                                      profileId = long.Parse(ele.strProfileId)
                                  }).ToArray(),
                informacionContrato = new CambioPlanLTEWSService.InformacionContratoType
                {
                    coId = long.Parse(objRequest.objContract.ContractList[0].InformacionContrato.coId),
                    listaCampos = (from ele in objRequest.objContract.ContractList[0].InformacionContrato.Campos
                                   select new CambioPlanLTEWSService.CampoType
                                   {
                                       indice = int.Parse(ele.strIndice),
                                       tipo = int.Parse(ele.strTipo),
                                       valor = ele.strValor
                                   }).ToArray()
                }

            };
            Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "ExecutePlanMigrationLTE: DESPUES DE SETEAR objContrato");
            List<CambioPlanLTEWSService.ContratoType> lstListaContratos = new List<CambioPlanLTEWSService.ContratoType>();
            lstListaContratos.Add(objContrato);
            Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "ExecutePlanMigrationLTE: DESPUES DE SETEAR lstListaContratos");
            CambioPlanLTEWSService.ActualizarTipificacionType objActualizarTipificacion = new CambioPlanLTEWSService.ActualizarTipificacionType()
            {
                orden = objRequest.objActualizarTipificacion.Orden
            };
            Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "ExecutePlanMigrationLTE: DESPUES DE SETEAR objActualizarTipificacion");
            CambioPlanLTEWSService.ParametrosConstanciaType objParametrosConstancia = new CambioPlanLTEWSService.ParametrosConstanciaType()
            {
                formatoConstancia = objRequest.strParametrosConstancia
            };
            Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "ExecutePlanMigrationLTE: DESPUES DE SETEAR objParametrosConstancia");
            CambioPlanLTEWSService.ParametrosEnvioCorreoType objParametrosEnvioCorreo = new CambioPlanLTEWSService.ParametrosEnvioCorreoType()
            {
                correoDestinatario = objRequest.strDestinatarioCorreo
            };
            Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "ExecutePlanMigrationLTE: DESPUES DE SETEAR objParametrosEnvioCorreo");
            CambioPlanLTEWSService.ContactoClienteType objContactoCliente = new CambioPlanLTEWSService.ContactoClienteType()
            {
            };
            Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "ExecutePlanMigrationLTE: DESPUES DE SETEAR objContactoCliente");
            CambioPlanLTEWSService.ejecutarCambioPlanLTERequest objServiceRequest = new CambioPlanLTEWSService.ejecutarCambioPlanLTERequest()
            {
                listaServicios = lstListServices,
                parametrosCliente = objParametrosCliente,
                parametrosPrincipal = objParametrosPrincipal,
                parametrosPlus = objParametrosPlus,
                parametrosSot = objSotParameters,
                listaContratos = lstListaContratos.ToArray(),
                actualizarTipificacion = objActualizarTipificacion,
                flagContingencia = (objRequest.blFlagContingencia ? 1 : 0).ToString(),
                flagCrearPlantilla = (objRequest.blFlagCrearPlantilla ? 1 : 0).ToString(),
                parametrosConstancia = objParametrosConstancia,
                parametrosEnvioCorreo = objParametrosEnvioCorreo,
                notas = objRequest.strNotes,
                codPlan = objRequest.strCodPlan,
                tipoPlan = objRequest.strTipoPlan,
                tmCode = objRequest.strTmCode,
                contactoCliente = objContactoCliente,
            };
            Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "ExecutePlanMigrationLTE: DESPUES DE SETEAR objServiceRequest");
            var OsbLteEntity = new OsbLteEntity();
            OsbLteEntity.result = string.Empty;
            OsbLteEntity.Code = -100;
            OsbLteEntity.ConstancyRoute = string.Empty;
            var oResponse = new CambioPlanLTEWSService.ejecutarCambioPlantLTEResponse();
            Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "ExecutePlanMigrationLTE: INICIO EJECUCIÓN DE SERVICIO");
            Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "ExecutePlanMigrationLTE - objServiceRequest:" + objServiceRequest.ToString());
            objHeaderResponse = Claro.Web.Logging.ExecuteMethod<CambioPlanLTEWSService.HeaderResponse>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
            {
                try
                {
                    return Configuration.ServiceConfiguration.SiacFixedPlanMigrationLTE.ejecutarCambioPlanLTE(objHeaderRequest, objServiceRequest, out oResponse);
                }
                catch (FaultException<ClaroFault> ex)
                {
                    strFaultMsg = ex.Detail.descripcionError;
                    Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, "ClaroFault:"+strFaultMsg);
                    OsbLteEntity.result = ex.Detail.descripcionError;
                    OsbLteEntity.Code = -100;
                    return null;
                }
                catch (FaultException<ExceptionDetail> ex)
                {
                    strFaultMsg = ex.Message;
                    Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, "ExceptionDetail:" + strFaultMsg);
                    OsbLteEntity.result = ex.Message;
                    OsbLteEntity.Code = -100;

                    return null;
                }
                catch (FaultException ex)
                {
                    strFaultMsg = ex.Message;
                    Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, "FaultException:" + strFaultMsg);
                    OsbLteEntity.result = ex.Message;
                    OsbLteEntity.Code = -100;
                    return null;

                }
                catch (Exception ex)
                {
                    strFaultMsg = ex.Message;
                    Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, "Exception:" + strFaultMsg);
                    OsbLteEntity.result = ex.Message;
                    OsbLteEntity.Code = -100;
                    return null;

                }

            });
            Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "ExecutePlanMigrationLTE: FINALIZA EJECUCIÓN DE SERVICIO");
            Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "ExecutePlanMigrationLTE - oResponse:" + JsonConvert.SerializeObject(oResponse));
            if (oResponse != null)
            {
                OsbLteEntity.result = oResponse.responseStatus.descriptionResponse;
                OsbLteEntity.Code = oResponse.responseStatus.status;
                if (oResponse.responseStatus.status == 0)
                {
                    OsbLteEntity.InteractionCode = oResponse.responseData.idInteraccion;
                    OsbLteEntity.SotNumber = oResponse.responseData.numeroSOT;
                    OsbLteEntity.ConstancyRoute = oResponse.responseData.rutaConsntacia;
                }
            }
            else if (OsbLteEntity.result == string.Empty)
            {
                OsbLteEntity.result = String.Empty;
            }
            Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "ExecutePlanMigrationLTE - OsbLteEntity:" + JsonConvert.SerializeObject(OsbLteEntity));
            Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "ExecutePlanMigrationLTE: FINALIZA CAMBIO DE PLAN");
            return new Entity.Transac.Service.Fixed.GetExecutePlanMigrationLTE.ExecutePlanMigrationLTEResponse()
            {
                result = OsbLteEntity
            };
        }
    }
}
