using System;
using System.Collections.Generic;
using System.Data;
using Claro.Data;
using Claro.SIACU.Data.Transac.Service.Configuration;
using EntitiesFixed = Claro.SIACU.Entity.Transac.Service.Fixed;

namespace Claro.SIACU.Data.Transac.Service.Fixed
{
    public class CommercialService
    {
        public List<EntitiesFixed.CommercialService> ListarServiciosComerciales(string strIdSession, string strTransaction, string strCoId)
        {
            DbParameter[] parameters = 
            {
                new DbParameter("p_co_id", DbType.String,ParameterDirection.Input, strCoId),
                new DbParameter("p_tmdes", DbType.String,ParameterDirection.Output),
                new DbParameter("p_tmcode", DbType.Int64,ParameterDirection.Output),
                new DbParameter("v_errnum", DbType.Int64,ParameterDirection.Output),
                new DbParameter("v_errmsj", DbType.String,ParameterDirection.Output),
                new DbParameter("p_cursor", DbType.Object,ParameterDirection.Output)
            };

            var salida = new List<EntitiesFixed.CommercialService>();
            try
            {
                Web.Logging.ExecuteMethod(strIdSession, strTransaction, () =>
                {
                    DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS,
                        DbCommandConfiguration.SIACU_POST_BSCS_SP_LISTAR_SERVICIOS_TELEFONO, parameters, reader =>
                        {
                            while (reader.Read())
                            {
                                var item = new EntitiesFixed.CommercialService
                                {
                                    DE_GRP = reader["DE_GRP"].ToString(),
                                    NO_GRP = reader["NO_GRP"].ToString(),
                                    CO_SER = reader["CO_SER"].ToString(),
                                    DE_SER = reader["DE_SER"].ToString(),
                                    NO_SER = reader["NO_SER"].ToString(),
                                    CO_EXCL = reader["CO_EXCL"].ToString(),
                                    DE_EXCL = reader["DE_EXCL"].ToString(),
                                    ESTADO = reader["ESTADO"].ToString(),
                                    VALIDO_DESDE = reader["VALIDO_DESDE"].ToString(),
                                    SUSCRIP = reader["SUSCRIP"].ToString(),
                                    CARGOFIJO = reader["CARGOFIJO"].ToString(),
                                    CUOTA = reader["CUOTA"].ToString(),
                                    PERIODOS = reader["PERIODOS"].ToString(),
                                    BLOQ_ACT = reader["BLOQ_ACT"].ToString(),
                                    BLOQ_DES = reader["BLOQ_DES"].ToString(),
                                    SNCODE = reader["SNCODE"].ToString(),
                                    SPCODE = reader["SPCODE"].ToString()
                                };
                                salida.Add(item);
                            }
                        });
                });

            }
            catch (Exception ex)
            {
                salida.Clear();
                var item = new EntitiesFixed.CommercialService {DE_SER = "Error"};
                salida.Add(item);
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
                throw;
            }
                     
            return salida;
        }

        public Boolean ObtenerPlanComercial(string strIdSession, string strTransaction, string strCoId, ref string rCodigoPlan, ref int rintCodigoError, ref string rstrDescripcionError)
        {
            DbParameter[] parameters = 
            {
                new DbParameter("P_CO_ID", DbType.String, ParameterDirection.Input, strCoId),	
                new DbParameter("P_COD_PLAN", DbType.String, ParameterDirection.Output),
                new DbParameter("P_RESULTADO", DbType.String, ParameterDirection.Output),	
                new DbParameter("P_MSGERR", DbType.String, ParameterDirection.Output)
            };


            bool salida = false;

            try
            {
                Web.Logging.ExecuteMethod(strIdSession, strTransaction, () =>
                {
                    DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_POST_BSCS_SP_GET_PLAN_COMERCIAL, parameters);
                    salida = true;
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
                salida = false;
            }
            finally
            {
                rCodigoPlan = parameters[parameters.Length - 3].Value.ToString();
                rintCodigoError = Convert.ToInt(parameters[parameters.Length - 2].Value);
                rstrDescripcionError = parameters[parameters.Length - 1].Value.ToString();
            }

            return salida;
        }

    }
}
