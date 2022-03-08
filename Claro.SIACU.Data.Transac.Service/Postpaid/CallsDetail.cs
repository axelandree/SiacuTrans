using Claro.Data;
using Claro.SIACU.Data.Transac.Service.Configuration;
using Claro.SIACU.Entity.Transac.Service.Common;
using Claro.SIACU.Transac.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Data.Transac.Service.Postpaid
{
    public class CallsDetail
    {
        #region No Facturado
        /// <summary>
        /// ListarDetalleLlamadasDB1
        /// </summary>
        /// <param name="contractID"></param>
        /// <param name="strStartDate"></param>
        /// <param name="strEndDate"></param>
        /// <param name="security"></param>
        /// <param name="summaryTotal"></param>
        /// <returns></returns>
        public static List<CallDetailGeneric> GetListCallDetailNB_DB1(string contractID, string strStartDate , string strEndDate,
            string security, ref string summaryTotal)
        {
            List<CallDetailGeneric> list = new List<CallDetailGeneric>();

            DbParameter[] parameters = new DbParameter[]
            {
                new DbParameter("p_tipo_consulta", DbType.String,20, ParameterDirection.Input,"C"),
                new DbParameter("p_valor", DbType.Int64,20, ParameterDirection.Input,contractID),
                new DbParameter("p_fecha_ini", DbType.String,20, ParameterDirection.Input,strStartDate),
                new DbParameter("p_fecha_fin", DbType.String,20, ParameterDirection.Input,strEndDate),
                new DbParameter("p_cursor", DbType.Object, ParameterDirection.Output)
            };

            double total = 0;
            double TotalMIN = 0;
            double TotalSMS = 0;
            double TotalMMS = 0;
            double TotalGPRS = 0;

            DbFactory.ExecuteReader("S", "T", DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_POST_BSCS_SP_DET_LLAMADA, parameters, (IDataReader reader) =>
            {
                int contador = 0;
                while (reader.Read())
                {
                    contador++;
                    CallDetailGeneric entity = new CallDetailGeneric();

                    entity.VlrNumber = (contador).ToString();

                    entity.StartDateTime = Functions.CheckDate(reader["Fecha_Hora_Inicio"]);
                    entity.StrStartDateTime = entity.StartDateTime.ToString("yyyy-MM-dd hh:mm");
                    entity.StrDate = entity.StartDateTime.ToString("yyyy-MM-dd");
                    entity.StrHour = entity.StartDateTime.ToString("hh:mm");

                    entity.Quantity = Functions.CheckDbl(reader["cantidad"]);
                    entity.FinalAmount = Functions.CheckDbl(reader["cargo_final"]);
                    entity.OriginalAmount_Flexible = Functions.CheckDbl(reader["Cargo_Ori_Flexible"]);
                    entity.OriginalAmount = Functions.CheckDbl(reader["cargo_original"]);
                    entity.Horary = Functions.CheckStr(reader["horario"]);
                    entity.TariffPlan = Functions.CheckStr(reader["plan_tarifario"]);
                    entity.Operator = Functions.CheckStr(reader["operador"]);
                    entity.Type = Functions.CheckStr(reader["tipo"]);

                    if (entity.Type.ToUpper() == "LLAMADA-SALIENTE" || (entity.Type.ToUpper() == "SMS-SALIENTE"))
                    {
                        if (security.Equals("1"))
                            entity.DestinationPhone = Functions.CheckStr(reader["telefono_destino"]);
                        else
                            entity.DestinationPhone = Functions.SeguridadFormatTelf(Functions.CheckStr(reader["telefono_destino"]));
                    }
                    else
                        entity.DestinationPhone = Functions.CheckStr(reader["telefono_destino"]);

                    entity.CallType = Functions.CheckStr(reader["tipo_llamada"]);
                    entity.Tariff = Functions.CheckStr(reader["tarifa"]);
                    entity.Tariff_Zone = Functions.CheckStr(reader["zona_tarifaria"]);

                    if (!String.IsNullOrEmpty(entity.Tariff_Zone))
                    {
                        if (entity.Type.ToUpper().IndexOf("LLAMADA") >= 0)
                            TotalMIN = TotalMIN + entity.Quantity;
                        else
                        {
                            if (entity.Type.ToUpper().IndexOf("SMS") >= 0)
                                TotalSMS = TotalSMS + entity.Quantity;
                            else
                            {
                                if (entity.Type.ToUpper().IndexOf("MMS") >= 0)
                                    TotalMMS = TotalMMS + entity.Quantity;
                                else
                                {
                                    if (entity.Type.ToUpper().IndexOf("GPRS") >= 0)
                                    {
                                        if (entity.Quantity % 1024 == 0)
                                            TotalGPRS = TotalGPRS + (entity.Quantity / 1024);
                                        else
                                            TotalGPRS = TotalGPRS + ((entity.Quantity / 1024) + 1);
                                    }
                                }
                            }
                        }
                    }

                    if (entity.Type.ToUpper().IndexOf("LLAMADA") >= 0)
                    {
                        entity.Quantity_FormatHHMMSS = Functions.GetFormatHHMMSS(Functions.CheckInt64(reader["Cantidad"]));
                    }
                    total = total + entity.FinalAmount;//item.cargo_final

                    list.Add(entity);
                }

            });
            summaryTotal = total.ToString() + ";" + TotalMIN.ToString() + ";" + TotalSMS.ToString() + ";" +
                TotalMMS.ToString() + ";" + TotalGPRS.ToString();

            if (list.Count > 0) list = list.OrderBy(x => x.StartDateTime).ToList();
 
            return list;
        }
        #endregion


        #region LLAMADAS ENTRANTES POSTPAGO



        #endregion
    }
}
