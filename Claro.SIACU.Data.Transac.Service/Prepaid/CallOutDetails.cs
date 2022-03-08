using Claro.Data;
using Claro.SIACU.Data.Transac.Service.Configuration;
using Claro.SIACU.Entity.Transac.Service.Prepaid;
using Claro.SIACU.ProxyService.Transac.Service.SIACPre.ConsultPrePostData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTITY = Claro.SIACU.Entity.Transac.Service.Prepaid;

namespace Claro.SIACU.Data.Transac.Service.Prepaid
{
    public class CallOutDetails
    {


        
        public static ENTITY.GetTipifCallOutPrep.TipifCallOutPrepResponse GetTipifCallOutPrep(string strIdSession, string strTransaction, string vTransaccion)
        {
            string vNumero = string.Empty;
            ENTITY.GetTipifCallOutPrep.TipifCallOutPrepResponse oTipifCallOutPrepResponse = new ENTITY.GetTipifCallOutPrep.TipifCallOutPrepResponse();

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_TRANSACCION", DbType.String,100, ParameterDirection.Input,vTransaccion),
                new DbParameter("P_CURSOR", DbType.Object, ParameterDirection.Output)
            };
           
            try
            {
                //usrsiac.PKG_SIAC_GENERICO.OBTENER_TIPIFICACION
                DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_OBTENER_TIPIFICACIONES, parameters, (IDataReader reader) =>
                {
                    while (reader.Read())
                    { 
                        oTipifCallOutPrepResponse.InteraccionId = Convert.ToString(reader["INTERACCION_CODE"]);
                        oTipifCallOutPrepResponse.ClaseId = Convert.ToString(reader["CLASE_CODE"]);
                        oTipifCallOutPrepResponse.ClaseDes = Convert.ToString(reader["CLASE"]);
                        oTipifCallOutPrepResponse.SubClaseId = Convert.ToString(reader["SUBCLASE_CODE"]);
                        oTipifCallOutPrepResponse.SubClaseDes = Convert.ToString(reader["SUBCLASE"]);
                        oTipifCallOutPrepResponse.TipoDes = Convert.ToString(reader["TIPO"]);
                        oTipifCallOutPrepResponse.TipoId = Convert.ToString(reader["TIPO_CODE"]);                        
                    };


                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransaction, ex.Message);
                oTipifCallOutPrepResponse = null;
            }

            return oTipifCallOutPrepResponse;
        }



        /// <summary>
        /// Método para Obtener el listado de llamadas salientes facturado
        /// </summary>
        /// <author></author>
        /// <creationdate>18/05/2017</creationdate>
        /// <param name="linea"></param>
        /// <param name="idtrans"></param>
        /// <param name="ipservidor"></param>
        /// <param name="nombreApp"></param>
        /// <param name="usuarioApp"></param>
        /// <param name="strfechaInicio"></param>
        /// <param name="strfechaFin"></param>
        /// <param name="strTipoConsulta"></param>
        /// <param name="tp"></param>
        /// <returns>Listado de llamadas salientes</returns>
        public static List<Call> GetCallDetails(
            string strIdSession, 
            string strtransaction,
            string linea,
            string idtrans,
            string ipservidor,
            string nombreApp,
            string usuarioApp,
            string strfechaInicio,
            string strfechaFin,
            string strTipoConsulta,
            string tp)
        {

            List<Call> lstReturn = new List<Call>();

            try
            {

                Claro.Web.Logging.Info(strIdSession, strtransaction, "IN PREPAGO SALIENTES - GetCallDetails()");
                ConsultaDatosPrePostWSService objService = new ConsultaDatosPrePostWSService();
                ConsultarDetalleLlamadasRequest objServiceRequest = new ConsultarDetalleLlamadasRequest();
                ConsultarDetalleLlamadasResponse objServiceResponse = new ConsultarDetalleLlamadasResponse();
                auditRequestType objAuditRequest = new auditRequestType();
                parametrosTypeObjetoOpcional[] Opcional = new parametrosTypeObjetoOpcional[0];

                objAuditRequest.idTransaccion = idtrans;
                objAuditRequest.ipAplicacion = ipservidor;
                objAuditRequest.nombreAplicacion = nombreApp;
                objAuditRequest.usuarioAplicacion = usuarioApp;

                objServiceRequest.msisdn = linea;
                objServiceRequest.listaRequestOpcional = Opcional;
                objServiceRequest.fechaInicial = strfechaInicio;
                objServiceRequest.fechaFinal = strfechaFin;
                objServiceRequest.tipoConsulta = strTipoConsulta;
                objServiceRequest.tipoTrafico = "";
                objServiceRequest.auditRequest = objAuditRequest;

                objServiceResponse = objService.consultarDetalleLlamadas(objServiceRequest);

                Claro.Web.Logging.Info(strIdSession, strtransaction, "- idTransaccion: " + objAuditRequest.idTransaccion + "- ipAplicacion: " + objAuditRequest.ipAplicacion + " - nombreAplicacion: " + objAuditRequest.nombreAplicacion + " -usuarioAplicacion: " + objAuditRequest.usuarioAplicacion);

                Claro.Web.Logging.Info(strIdSession, strtransaction, "- msisdn:  " + objServiceRequest.msisdn + "- fechaInicial: " + objServiceRequest.fechaInicial + "- fechaFinal  " + objServiceRequest.fechaFinal + " - tipoConsulta: " + objServiceRequest.tipoConsulta + " - tipoTrafico:  " + objServiceRequest.tipoTrafico);
                

                
                //Claro.Web.Logging.Info(strIdSession, strtransaction, "_objServiceResponse.listaResponseOpcional:  " + objServiceResponse.listaResponseOpcional.Count().ToString());
                if (objServiceResponse.detalleLlamadas != null)
                {
                    parametrosTypeLlamadasObjetoOpcionalLlamadas[][] detalleLlamadas;
                    if (objServiceResponse.detalleLlamadas.Length > 0)
                    {
                        detalleLlamadas = objServiceResponse.detalleLlamadas;

                        for (int i = 0; i < detalleLlamadas.Length; i++)
                        {
                            Call item = new Call();
                            for (int j = 0; j < detalleLlamadas[i].Length; j++)
                            {
                                if (detalleLlamadas[i][j].campo.Equals("FECHA_HORA"))
                                {
                                    item.FECHA_HORA = System.Convert.ToDateTime(detalleLlamadas[i][j].valor);
                                }
                                if (detalleLlamadas[i][j].campo.Equals("TELEFONO_DESTINO"))
                                {
                                    item.TELEFONO_DESTINO = detalleLlamadas[i][j].valor;
                                }
                                if (detalleLlamadas[i][j].campo.Equals("TIPO_DE_TRAFICO"))
                                {
                                    item.TIPO_DE_TRAFICO = detalleLlamadas[i][j].valor;
                                }
                                if (detalleLlamadas[i][j].campo.Equals("DURACION"))
                                {
                                    item.DURACION = detalleLlamadas[i][j].valor;
                                }
                                if (detalleLlamadas[i][j].campo.Equals("CONSUMO"))
                                {
                                    item.CONSUMO = detalleLlamadas[i][j].valor;
                                }
                                if (detalleLlamadas[i][j].campo.Equals("COMPRADO_REGALADO"))
                                {
                                    item.COMPRADO_REGALADO = detalleLlamadas[i][j].valor;
                                }
                                if (detalleLlamadas[i][j].campo.Equals("SALDO"))
                                {
                                    item.SALDO = detalleLlamadas[i][j].valor;
                                }
                                if (detalleLlamadas[i][j].campo.Equals("BOLSA_ID"))
                                {
                                    item.BOLSA_ID = detalleLlamadas[i][j].valor;
                                }
                                if (detalleLlamadas[i][j].campo.Equals("DESCRIPCION"))
                                {
                                    item.DESCRIPCION = detalleLlamadas[i][j].valor;
                                }
                                if (detalleLlamadas[i][j].campo.Equals("PLAN"))
                                {
                                    item.PLAN = detalleLlamadas[i][j].valor;
                                }
                                if (detalleLlamadas[i][j].campo.Equals("PROMOCION"))
                                {
                                    item.PROMOCION = detalleLlamadas[i][j].valor;
                                }
                                if (detalleLlamadas[i][j].campo.Equals("DESTINO"))
                                {
                                    item.DESTINO = detalleLlamadas[i][j].valor;
                                }
                                if (detalleLlamadas[i][j].campo.Equals("OPERADOR"))
                                {
                                    item.OPERADOR = detalleLlamadas[i][j].valor;
                                }
                                if (detalleLlamadas[i][j].campo.Equals("GRUPO_DE_COBRO"))
                                {
                                    item.GRUPO_DE_COBRO = detalleLlamadas[i][j].valor;
                                }
                                if (detalleLlamadas[i][j].campo.Equals("TIPO_DE_RED"))
                                {
                                    item.TIPO_DE_RED = detalleLlamadas[i][j].valor;
                                }
                                if (detalleLlamadas[i][j].campo.Equals("IMEI"))
                                {
                                    item.IMEI = detalleLlamadas[i][j].valor;
                                }
                                if (detalleLlamadas[i][j].campo.Equals("ROAMING"))
                                {
                                    item.ROAMING = detalleLlamadas[i][j].valor;
                                }
                                if (detalleLlamadas[i][j].campo.Equals("ZONA_TARIFARIA"))
                                {
                                    item.ZONA_TARIFARIA = detalleLlamadas[i][j].valor;
                                }
                            }
                            lstReturn.Add(item);
                        }

                        lstReturn = lstReturn.OrderByDescending(o => o.FECHA_HORA).ToList();

                        if (tp.ToUpper() == "VOZ")
                        {
                            lstReturn = lstReturn.Where(o => o.TIPO_DE_TRAFICO == "VOZ").ToList();
                        }
                        else if (tp.ToUpper() == "SMS")
                        {
                            lstReturn = lstReturn.Where(o => o.TIPO_DE_TRAFICO == "SMS").ToList();
                        }
                        else if (tp.ToUpper() == "MMS")
                        {
                            lstReturn = lstReturn.Where(o => o.TIPO_DE_TRAFICO == "MMS").ToList();
                        }
                        else if (tp.ToUpper() == "DATOS")
                        {
                            lstReturn = lstReturn.Where(o => o.TIPO_DE_TRAFICO == "DATOS").ToList();
                        }
                        else if (tp.ToUpper() == "VAS")
                        {
                            lstReturn = lstReturn.Where(o => o.TIPO_DE_TRAFICO == "VAS").ToList();
                        }
                        else if (tp.ToUpper() == "USSD")
                        {
                            lstReturn = lstReturn.Where(o => o.TIPO_DE_TRAFICO == "USSD").ToList();
                        }
                        else
                        {
                            lstReturn = lstReturn.OrderByDescending(o => o.FECHA_HORA).ToList();
                        }
                    }
                }

                var msg = lstReturn.Count.ToString();
                Claro.Web.Logging.Info(strIdSession, strtransaction, "OUT PREPAGO SALIENTES - lstReturn.Count:  " + msg);

                return lstReturn;
            }
            catch (Exception ex)
            {
                lstReturn = new List<Call>();
                var msg = ex.InnerException.Message;
                Claro.Web.Logging.Info(strIdSession, strtransaction, msg);
                return lstReturn;
            }


        }
    }
}
