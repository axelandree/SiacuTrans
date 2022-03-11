using Claro.Data;
using Claro.SIACU.Data.Transac.Service.Configuration;
using Claro.SIACU.Entity.Transac.Service.Coliving;
using Claro.SIACU.Entity.Transac.Service.Coliving.GetParameter;
using Claro.SIACU.Transac.Service;
using System;
using System.Collections.Generic;
using System.Data;

namespace Claro.SIACU.Data.Transac.Service.Coliving
{
    public class Common
    {
        #region [INICIATIVA 217]
        public static GetParameterResponse GetParameter(string name, string strIdSession, string strTransaction)
        {
            GetParameterResponse objResponse = new GetParameterResponse();
            List<ListParameter> ListParameter = new List<ListParameter>();
            try
            {
                DbParameter[] parameters = {
                new DbParameter("P_NOMBRE", DbType.String, 255,ParameterDirection.Input, name),
                new DbParameter("P_MENSAJE", DbType.String, 255,ParameterDirection.Output),
                new DbParameter("P_CURSOR", DbType.Object, ParameterDirection.Output)
            };
                DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_COMMON_SP_OBTENER_DATO, parameters, (IDataReader reader) =>
                {
                    ListParameter entity = null;
                    while (reader.Read())
                    {
                        entity = new ListParameter();
                        entity.strDescripcion = Functions.CheckStr(reader["DESCRIPCION"]);
                        entity.strValorN = Functions.CheckStr(reader["VALOR_N"]);
                        entity.strValorC = Functions.CheckStr(reader["VALOR_C"]);
                        ListParameter.Add(entity);
                    }
                });

                objResponse.Mensaje = parameters[1].Value.ToString();
                objResponse.LstParameter = ListParameter;
                Claro.Web.Logging.Info("Session: " + strIdSession, "GetParameter ", "Message" + objResponse.Mensaje);
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, "Error GetParameter : " + ex.Message);
            }
            return objResponse;
        }
         
        public static List<ListItems> GetDocumentTypeTOBE(string strIdSession, string strTransaction, string strCodCargaDdl)
        {
            List<ListItems> listItem = null;
            try
            {
            var msg = string.Format("Metodo: {0}, Request: {1}", "GetDocumentTypeTOBE", strCodCargaDdl);
                Claro.Web.Logging.Info("Session: " + strIdSession, "Transaction: " + strTransaction, "Message" + msg);
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("p_id_lista", DbType.String,100, ParameterDirection.Input, strCodCargaDdl),
                new DbParameter("p_cursor", DbType.Object, ParameterDirection.Output)
            };

            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_SP_OBTIENE_LISTAS_CBIO, parameters, (IDataReader reader) =>
            {
                listItem = new List<ListItems>();

                while (reader.Read())
                {
                    listItem.Add(new ListItems()
                    {
                        Description = Convert.ToString(reader["DESCRIPCION"]),
                        Code = Convert.ToString(reader["VALOR_TOBE"])
                    });
                }
            });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, "Error : " + ex.Message);
                throw;
            }
            return listItem;
        }

        
        #endregion
    }
}
