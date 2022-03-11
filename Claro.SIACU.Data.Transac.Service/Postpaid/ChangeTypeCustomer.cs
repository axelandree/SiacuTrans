using System;
using Claro.Data;
using System.Data;
using System.Collections.Generic;
using Claro.SIACU.Data.Transac.Service.Configuration;
using Claro.SIACU.Entity.Transac.Service.Postpaid;


namespace Claro.SIACU.Data.Transac.Service.Postpaid
{
    public class ChangeTypeCustomer
    {
        /// <summary>
        /// Ciclo de facturacion
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="strTypeCustomer"></param>
        /// <returns></returns>
        public static List<BillingCycle> GetBillingCycle(string strIdSession, string strTransaction, string strTypeCustomer)
        {
            List<BillingCycle> lstBillingCycle = new List<BillingCycle>();
            DbParameter[] parameters = new DbParameter[]{
                    new DbParameter("P_TIPO_CLIENTE", DbType.String, ParameterDirection.Input, strTypeCustomer),
                    new DbParameter("P_CURSOR", DbType.Object, ParameterDirection.Output),
                    new DbParameter("P_CODE_ERR", DbType.String,255, ParameterDirection.Output),
                    new DbParameter("P_MSG_ERR", DbType.String,255, ParameterDirection.Output)

                };

            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_MGRSS_TIM_SP_VALIDA_CICLO_FACT, parameters,
                (IDataReader dr) =>
                {
                    while (dr.Read())
                    {
                        lstBillingCycle.Add(new BillingCycle
                        {
                            strBicicle = dr["BILLCYCLE"].ToString(),
                            strDescription = dr["DESCRIPTION"].ToString(),
                            strValidForm = dr["VALID_FROM"].ToString(),
                            strTypeCustomer = dr["TIPO_CLIENTE"].ToString()
                        });
                    }
                });

            return lstBillingCycle;
        }

        public static List<ParameterBusinnes> GetParameterBusinnes(string strIdSession, string strTransaction, string strIdList)
        {
            List<ParameterBusinnes> lstItemGenerico = new List<ParameterBusinnes>();
            DbParameter[] parameters = new DbParameter[]{
                    new DbParameter("P_ID_LISTA", DbType.String, ParameterDirection.Input, strIdList),
				    new DbParameter("P_CURSOR", DbType.Object,ParameterDirection.Output),
	
                };

            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_SP_OBTIENE_LISTAS, parameters,
            (IDataReader dr) =>
            {
                while (dr.Read())
                {
                    lstItemGenerico.Add(new ParameterBusinnes
                    {
                        strCode = Convert.ToString(dr["VALOR"]),
                        strDescription = Convert.ToString(dr["DESCRIPCION"]),
                    });
                }
            });
            return lstItemGenerico;
        }

        /// <summary>
        /// Listado los nuevo planes
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="strCodeTypeProd"></param>
        /// <param name="strCategoryProdu"></param>
        /// <param name="NamePlan"></param>
        /// <returns></returns>
        public static List<NewPlan> GetNewPlan(string strIdSession, string strTransaction, string strCodeTypeProd, string strCategoryProdu, string NamePlan)
        {
            List<NewPlan> lstNewPlan = new List<NewPlan>();

            DbParameter[] parameters = new DbParameter[]{
                    new DbParameter("P_TIPO_PRODUCTO", DbType.String,20, ParameterDirection.Input, strCodeTypeProd),
                    new DbParameter("P_CAT_PROD", DbType.String,50, ParameterDirection.Input, strCategoryProdu),
                    new DbParameter("P_NOMBRE_PLAN", DbType.String,100, ParameterDirection.Input, NamePlan),
				    new DbParameter("P_CURSOR", DbType.Object,ParameterDirection.Output),
	
                };
            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_SP_BUSCA_PLAN_X_CODPROD, parameters,
            (IDataReader dr) =>
            {
                while (dr.Read())
                {
                    lstNewPlan.Add(new NewPlan
                    {
                        COD_PROD = Convert.ToString(dr["COD_PROD"]),
                        TMCODE = Convert.ToString(dr["TMCODE"]),
                        DESC_PLAN = Convert.ToString(dr["DESC_PLAN"]),
                        VERSION = Convert.ToString(dr["VERSION"]),
                        CAT_PROD = Convert.ToString(dr["CAT_PROD"]),
                        COD_CARTA_INFO = Convert.ToString(dr["COD_CARTA_INFO"]),
                        FECHA_INI_VIG = Convert.ToString(dr["FECHA_INI_VIG"]),
                        FECHA_FIN_VIG = Convert.ToString(dr["FECHA_FIN_VIG"]),
                        ID_TIPO_PROD = Convert.ToString(dr["ID_TIPO_PROD"])
                    });
                }
            });

            return lstNewPlan;
        }

        /// <summary>
        /// Listado Plan tarifario
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="strTPROCCode"></param>
        /// <param name="strPRDCCode"></param>
        /// <param name="strModalVent"></param>
        /// <param name="strPLNCFamilly"></param>
        /// <returns></returns>
        public static List<TarifePlan> GetTarifePlan(string strIdSession, string strTransaction, string strTPROCCode, string strPRDCCode, string strModalVent, string strPLNCFamilly)
        {
            List<TarifePlan> lstTarifePlan = new List<TarifePlan>();

            DbParameter[] parameters = new DbParameter[]{
                    new DbParameter("P_TPROC_CODIGO", DbType.String, ParameterDirection.Input, strTPROCCode),
                    new DbParameter("P_PRDC_CODIGO", DbType.String, ParameterDirection.Input, strPRDCCode),
                    new DbParameter("P_MODALIDAD_VENTA", DbType.String, ParameterDirection.Input, strModalVent),
                    new DbParameter("P_PLNC_FAMILIA", DbType.String, ParameterDirection.Input, strPLNCFamilly),
                    new DbParameter("P_CURSOR", DbType.String, ParameterDirection.Output)
                };

            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_PVU, DbCommandConfiguration.SIACU_SP_SIACS_PLAN_TARIFARIO, parameters,
                (IDataReader dr) =>
                {
                    while (dr.Read())
                    {
                        lstTarifePlan.Add(new TarifePlan
                        {
                            strPLNVCode = Convert.ToString(dr["PLNV_CODIGO"]),
                            strPLNVDescription = Convert.ToString(dr["PLNV_DESCRIPCION"]),
                            dcmPLNNCargoFijo = Convert.ToDecimal(dr["PLNN_CARGO_FIJO"]),
                            strPLNCodeBSCS = Convert.ToString(dr["PLNV_CODIGO_BSCS"]),
                            strPRDCCode = Convert.ToString(dr["PRDC_CODIGO"]),
                            strPRDVDescription = Convert.ToString(dr["PRDV_DESCRIPCION"]),
                            strPlNCFamally = Convert.ToString(dr["PLNC_FAMILIA"]),
                            strTPROCCode = Convert.ToString(dr["TPROC_CODIGO"]),
                            strTPROVDescription = Convert.ToString(dr["TPROV_DESCRIPCION"]),
                        });
                    }
                });

            return lstTarifePlan;
        }

        /// <summary>
        /// elemento de migraciones CAC/DAC
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="intCode"></param>
        /// <returns></returns>
        public static List<ParameterBusinnes> GetElementMigration(string strIdSession, string strTransaction, Int64 intCode)
        {
            List<ParameterBusinnes> lstElementMigration = new List<ParameterBusinnes>();
            DbParameter[] parameters = new DbParameter[]{
                    new DbParameter("P_OBJID", DbType.Int64 ,ParameterDirection.Input, intCode * -1),
                    new DbParameter("P_FLAG_CONSULTA", DbType.String, ParameterDirection.Output),
                    new DbParameter("P_MSG_TEXT", DbType.String , ParameterDirection.Output),
                    new DbParameter("P_LIST", DbType.Object, ParameterDirection.Output)
                };

            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_SP_SHOW_LIST_ELEMENT, parameters,
                (IDataReader dr) =>
                {
                    while (dr.Read())
                    {
                        lstElementMigration.Add(new ParameterBusinnes
                        {
                            strCode = Convert.ToString(dr["CODIGO"]),
                            strDescription = Convert.ToString(dr["NOMBRE"]),
                        });
                    }
                });

            return lstElementMigration;
        }

        /// <summary>
        /// Traer el Codigo de Cliente
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="strName"></param>
        /// <returns></returns>
        public static string GetCodeChangeCustomer(string strIdSession, string strTransaction, string strName)
        {
            string strCodeChangeCustomer;

            DbParameter[] parameters = new DbParameter[]{
                    new DbParameter("P_TITLE", DbType.String, ParameterDirection.Input, strName),
                    new DbParameter("P_OBJID", DbType.String, ParameterDirection.Output)
                };

            DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_SP_OBTENER_CODIGO, parameters);
            strCodeChangeCustomer = parameters[1].Value.ToString();

            return strCodeChangeCustomer;
        }

        /// <summary>
        /// conboBox Listar Area
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <returns></returns>
        public static List<ParameterBusinnes> GetArea(string strIdSession, string strTransaction)
        {
            List<ParameterBusinnes> lstArea = new List<ParameterBusinnes>();
            DbParameter[] parameters = new DbParameter[]{
                      new DbParameter("K_CURLISTADO", DbType.Object, ParameterDirection.Output)
                };

            Web.Logging.ExecuteMethod(strIdSession, strTransaction, () =>
                {
                    DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_MGRSS_USRSIAC_GR_LISTAAREA, parameters, reader =>
                        {
                            while (reader.Read())
                            {
                                lstArea.Add(new ParameterBusinnes
                                {
                                    strCode = Convert.ToString(reader["IDAREA"]),
                                    strDescription = Convert.ToString(reader["AREA"]),
                                });
                            }
                        });
                });

            return lstArea;
        }

        /// <summary>
        /// conboBox Listado Motivos x Areas
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="strIdArea"></param>
        /// <returns></returns>
        public static List<ParameterBusinnes> GetMotiveByArea(string strIdSession, string strTransaction, string strIdArea)
        {
            List<ParameterBusinnes> lstMotiveByArea = new List<ParameterBusinnes>();

            DbParameter[] parameters = new DbParameter[]{
                    new DbParameter("K_AREA", DbType.String ,ParameterDirection.Input, strIdArea),
                    new DbParameter("K_CURLISTADO", DbType.Object, ParameterDirection.Output)
                };

            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_MGRSS_USRSIAC_GR_LISTAMOTIVO, parameters,
                (IDataReader dr) =>
                {
                    while (dr.Read())
                    {
                        lstMotiveByArea.Add(new ParameterBusinnes
                        {
                            strCode = Convert.ToString(dr["IDMOTIVO"]),
                            strDescription = Convert.ToString(dr["MOTIVO"]),
                        });
                    }
                });

            return lstMotiveByArea;
        }

        /// <summary>
        /// conboBox Traendo sub motivo por motivo
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="strIdArea"></param>
        /// <param name="strIdMotive"></param>
        /// <returns></returns>
        public static List<ParameterBusinnes> GetSubMotive(string strIdSession, string strTransaction, string strIdArea, string strIdMotive)
        {
            List<ParameterBusinnes> lstSubMotive = new List<ParameterBusinnes>();
            DbParameter[] parameters = new DbParameter[]{
                      new DbParameter("K_AREA", DbType.String ,ParameterDirection.Input, strIdArea),
                      new DbParameter("K_MOTIVO", DbType.String ,ParameterDirection.Input, strIdMotive),
                      new DbParameter("K_CURLISTADO", DbType.Object, ParameterDirection.Output)
                };

            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_MGRSS_USRSIAC_GR_LISTASUBMOTIVO, parameters,
                (IDataReader dr) =>
                {
                    while (dr.Read())
                    {
                        lstSubMotive.Add(new ParameterBusinnes
                        {
                            strCode = Convert.ToString(dr["IDSUBMOTIVO"]),
                            strDescription = Convert.ToString(dr["SUBMOTIVO"]),
                        });
                    }
                });

            return lstSubMotive;
        }

        public static List<ParameterBusinnes> GetConsumptionStop(string strIdSession, string strTransaction, string strCode)
        {
            List<ParameterBusinnes> lstConsumptionStop = new List<ParameterBusinnes>();

            DbParameter[] parameters = new DbParameter[]{
                      new DbParameter("P_ID_LISTA", DbType.String ,ParameterDirection.Input, strCode),
                      new DbParameter("K_CURLISTADO", DbType.Object, ParameterDirection.Output)
                };

            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_SP_OBTIENE_LISTAS, parameters,
                (IDataReader dr) =>
                {
                    while (dr.Read())
                    {
                        lstConsumptionStop.Add(new ParameterBusinnes
                        {
                            strCode = Convert.ToString(dr["VALOR"]),
                            strDescription = Convert.ToString(dr["DESCRIPCION"]),
                            strNumber = Convert.ToString(dr["ORDEN"]),
                        });
                    }
                });

            return lstConsumptionStop;
        }

    }
}
