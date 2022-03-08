using System;
using Claro.Data;
using System.Data;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;
using KEY = Claro.ConfigurationManager;
using Claro.SIACU.Entity.Transac.Service.Common;
using Claro.SIACU.Data.Transac.Service.Configuration;
using Claro.SIACU.Entity.Transac.Service.Common.GetComprarPaquetes;
using Claro.SIACU.Entity.Transac.Service.Common.GetPCRFConsultation;
using Claro.SIACU.Entity.Transac.Service.Common.GetConsultarClaroPuntos;
using Claro.SIACU.Entity.Transac.Service.Common.GetConsultarPaqDisponibles;
using PCRFConnectorLTE = Claro.SIACU.ProxyService.Transac.Service.SIACFixed.PCRFConnectorLTE;
using Claro.SIACU.Entity.Transac.Service.Fixed;

namespace Claro.SIACU.Data.Transac.Service.Fixed
{
    public class PackagePurchaseService
    {

        public static ConsultarClaroPuntosResponse ConsultarClaroPuntos(ConsultarClaroPuntosRequest objRequest, RestConsultarClaroPuntosRequest oRestRequest)
        {

            ConsultarClaroPuntosResponse objResponse = null;

            Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "Request DP ConsultarClaroPuntos: " + JsonConvert.SerializeObject(objRequest));
            try
            {
                Hashtable objHashtable = new Hashtable();
                objHashtable.Add("tipoConsulta",oRestRequest.MessageRequest.Body.tipoConsulta);
                objHashtable.Add("tipoDocumento", oRestRequest.MessageRequest.Body.tipoDocumento);
                objHashtable.Add("numeroDocumento", oRestRequest.MessageRequest.Body.numeroDocumento);
                objHashtable.Add("bolsa",  oRestRequest.MessageRequest.Body.bolsa);
                objHashtable.Add("tipoPuntos", oRestRequest.MessageRequest.Body.tipoPuntos);
                objResponse = RestService.GetInvoque<ConsultarClaroPuntosResponse>(Configuration.RestServiceConfiguration.ConsultarClaroPuntos_DP, objRequest.Audit, objHashtable, null);

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
                string sep = " - ";
                int posResponse = ex.Message.IndexOf(sep);
                string result = ex.Message.Substring(posResponse + sep.Length);
                objResponse = JsonConvert.DeserializeObject<ConsultarClaroPuntosResponse>(result);

            }
            Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "Response DP ConsultarClaroPuntos: " + JsonConvert.SerializeObject(objResponse));

            return objResponse;
        }

        public static ConsultarPaqDisponiblesResponse ConsultarPaqDisponibles(ConsultarPaqDisponiblesRequest objRequest, RestConsultarPaqDisponiblesRequest oRestRequest)
        {

            ConsultarPaqDisponiblesResponse objResponse = null;

            Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "Request DP ConsultarPaqDisponibles: " + JsonConvert.SerializeObject(objRequest));
            try
            {

                objResponse = RestService.PostInvoque<ConsultarPaqDisponiblesResponse>(Configuration.RestServiceConfiguration.ConsultarPaqDisponibles_DP, objRequest.Audit, oRestRequest, true);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
                string sep = " - ";
                int posResponse = ex.Message.IndexOf(sep);
                string result = ex.Message.Substring(posResponse + sep.Length);
                objResponse = JsonConvert.DeserializeObject<ConsultarPaqDisponiblesResponse>(result);

            }
            Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "Response DP ConsultarPaqDisponibles: " + JsonConvert.SerializeObject(objResponse));

            return objResponse;
        }

        public static ComprarPaquetesBodyResponse ComprarPaquetes(ComprarPaquetesRequest objRequest, RestComprarPaquetesRequest oRestRequest)
        {

            ComprarPaquetesBodyResponse objResponse = null;

            Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "Request DP ComprarPaquetes: " + JsonConvert.SerializeObject(objRequest));
            try
            {
                Hashtable objHashtable = new Hashtable();
                objHashtable.Add("idTransaccion", objRequest.Audit.Transaction);
                objHashtable.Add("msgid", objRequest.Audit.Transaction);
                objHashtable.Add("userId", objRequest.Audit.UserName);
                objHashtable.Add("timestamp", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ"));
                objHashtable.Add("idAplicacion", objRequest.Audit.ApplicationName);
                objResponse = RestService.PostInvoqueSDP<ComprarPaquetesBodyResponse>(Configuration.RestServiceConfiguration.ComprarPaquetes_DP, objHashtable, oRestRequest.MessageRequest.Body);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
                string sep = " - ";
                int posResponse = ex.Message.IndexOf(sep);
                string result = ex.Message.Substring(posResponse + sep.Length);
                objResponse = JsonConvert.DeserializeObject<ComprarPaquetesBodyResponse>(result);

            }
            Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "Response DP ComprarPaquetes: " + JsonConvert.SerializeObject(objResponse));

            return objResponse;
        }

        public static PCRFConnectorResponse ConsultarPCRFDegradacion(PCRFConnectorRequest objRequest)
        {
            Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "Inicio Método - ConsultarPCRFDegradacion");
            PCRFConnectorLTE.consultarResponse objResponse = new PCRFConnectorLTE.consultarResponse();
            PCRFConnectorResponse objPCRFSuscriberQuota = null;
            string strAccion = KEY.AppSettings("str_ConsultaSucr1");
            try
            {

                PCRFConnectorLTE.listaParametrosParametro[] lstParamParam = new PCRFConnectorLTE.listaParametrosParametro[1];
                PCRFConnectorLTE.listaParametros[] lstParam = new PCRFConnectorLTE.listaParametros[1];

                PCRFConnectorLTE.listaParametrosParametro listaParamParam = new PCRFConnectorLTE.listaParametrosParametro()
                {
                    campo = KEY.AppSettings("str_campoPCRF"),
                    valor = objRequest.strLinea,
                };

                lstParamParam[0] = listaParamParam;

                PCRFConnectorLTE.listaParametros listaParam = new PCRFConnectorLTE.listaParametros()
                {
                    nombreLista = KEY.AppSettings("str_nombreListaPCRF"),
                    parametro = lstParamParam,
                    subListaParametros = new PCRFConnectorLTE.subListaParametros[0],
                };

                lstParam[0] = listaParam;

                PCRFConnectorLTE.consultarRequest objConnectorRequest = new PCRFConnectorLTE.consultarRequest()
                {
                    auditRequest = new PCRFConnectorLTE.parametrosAuditRequest()
                    {
                        idTransaccion = objRequest.Audit.Transaction,
                        ipAplicacion = objRequest.Audit.IPAddress,
                        nombreAplicacion = objRequest.Audit.ApplicationName,
                        usuarioAplicacion = objRequest.Audit.UserName,
                    },

                    accionRequest = new PCRFConnectorLTE.accionType()
                    {
                        idAccion = strAccion,
                        listaParametros = lstParam,
                    },
                };
                objResponse = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, ServiceConfiguration.SiacFixedPCRFConnector, () =>
                {
                    return ServiceConfiguration.SiacFixedPCRFConnector.consultar(objConnectorRequest);
                });
                objPCRFSuscriberQuota = new PCRFConnectorResponse();

                if (objResponse != null && objResponse.auditResponse.codRespuesta == Constants.NumberZeroString)
                {
                    objPCRFSuscriberQuota.listSuscriberQuota = new List<SuscriberQuota>();
                    objPCRFSuscriberQuota.codRespuesta = objResponse.auditResponse.codRespuesta;
                    objPCRFSuscriberQuota.msjRespuesta = objResponse.auditResponse.msjRespuesta;
                    if (objResponse.accionResponse.idAccion == strAccion)
                    {

                        PCRFConnectorLTE.listaParametros[] lstResponse = objResponse.accionResponse.listaParametros;

                        if (lstResponse[0].parametro != null)
                        {
                            var objSuscriberQuotaParametro = new SuscriberQuota();

                            for (int j = 0; j < lstResponse[0].parametro.Length; j++)
                            {

                                if (lstResponse[0].parametro[j].campo.Equals("QTATIMESTAMP"))
                                {
                                    objSuscriberQuotaParametro.QTATIMESTAMP = lstResponse[0].parametro[j].valor;
                                }
                                if (lstResponse[0].parametro[j].campo.Equals("QTARSTDATETIME"))
                                {
                                    objSuscriberQuotaParametro.QTARSTDATETIME = lstResponse[0].parametro[j].valor;
                                }
                                if (lstResponse[0].parametro[j].campo.Equals("QTALASTRSTDATETIME"))
                                {
                                    objSuscriberQuotaParametro.QTALASTRSTDATETIME = lstResponse[0].parametro[j].valor;
                                }
                                if (lstResponse[0].parametro[j].campo.Equals("QTASTATUS"))
                                {
                                    objSuscriberQuotaParametro.QTASTATUS = lstResponse[0].parametro[j].valor;
                                }
                                if (lstResponse[0].parametro[j].campo.Equals("QTACONSUMPTION"))
                                {
                                    objSuscriberQuotaParametro.QTACONSUMPTION = lstResponse[0].parametro[j].valor;
                                }
                                if (lstResponse[0].parametro[j].campo.Equals("QTABALANCE"))
                                {
                                    objSuscriberQuotaParametro.QTABALANCE = lstResponse[0].parametro[j].valor;
                                }
                                if (lstResponse[0].parametro[j].campo.Equals("QTAVALUE"))
                                {
                                    objSuscriberQuotaParametro.QTAVALUE = lstResponse[0].parametro[j].valor;
                                }
                                if (lstResponse[0].parametro[j].campo.Equals("QTANAME"))
                                {
                                    objSuscriberQuotaParametro.QTANAME = lstResponse[0].parametro[j].valor;
                                }
                                if (lstResponse[0].parametro[j].campo.Equals("SRVNAME"))
                                {
                                    objSuscriberQuotaParametro.SRVNAME = lstResponse[0].parametro[j].valor;
                                }

                            }

                            objPCRFSuscriberQuota.listSuscriberQuota.Add(objSuscriberQuotaParametro);

                        }

                        if (lstResponse[0].subListaParametros != null) 
                        {
                            for (int j = 0; j < lstResponse[0].subListaParametros.Length; j++)
                            {
                                var objSuscriberQuotasubLista = new SuscriberQuota();
                                for (int i = 0; i < lstResponse[0].subListaParametros[j].parametro.Length; i++)
                                {
                                    if (lstResponse[0].subListaParametros[j].parametro[i].campo.Equals("QTATIMESTAMP"))
                                    {
                                        objSuscriberQuotasubLista.QTATIMESTAMP = lstResponse[0].subListaParametros[j].parametro[i].valor;
                                    }
                                    if (lstResponse[0].subListaParametros[j].parametro[i].campo.Equals("QTARSTDATETIME"))
                                    {
                                        objSuscriberQuotasubLista.QTARSTDATETIME = lstResponse[0].subListaParametros[j].parametro[i].valor;
                                    }
                                    if (lstResponse[0].subListaParametros[j].parametro[i].campo.Equals("QTALASTRSTDATETIME"))
                                    {
                                        objSuscriberQuotasubLista.QTALASTRSTDATETIME = lstResponse[0].subListaParametros[j].parametro[i].valor;
                                    }
                                    if (lstResponse[0].subListaParametros[j].parametro[i].campo.Equals("QTASTATUS"))
                                    {
                                        objSuscriberQuotasubLista.QTASTATUS = lstResponse[0].subListaParametros[j].parametro[i].valor;
                                    }
                                    if (lstResponse[0].subListaParametros[j].parametro[i].campo.Equals("QTACONSUMPTION"))
                                    {
                                        objSuscriberQuotasubLista.QTACONSUMPTION = lstResponse[0].subListaParametros[j].parametro[i].valor;
                                    }
                                    if (lstResponse[0].subListaParametros[j].parametro[i].campo.Equals("QTABALANCE"))
                                    {
                                        objSuscriberQuotasubLista.QTABALANCE = lstResponse[0].subListaParametros[j].parametro[i].valor;
                                    }
                                    if (lstResponse[0].subListaParametros[j].parametro[i].campo.Equals("QTAVALUE"))
                                    {
                                        objSuscriberQuotasubLista.QTAVALUE = lstResponse[0].subListaParametros[j].parametro[i].valor;
                                    }
                                    if (lstResponse[0].subListaParametros[j].parametro[i].campo.Equals("QTANAME"))
                                    {
                                        objSuscriberQuotasubLista.QTANAME = lstResponse[0].subListaParametros[j].parametro[i].valor;
                                    }
                                    if (lstResponse[0].subListaParametros[j].parametro[i].campo.Equals("SRVNAME"))
                                    {
                                        objSuscriberQuotasubLista.SRVNAME = lstResponse[0].subListaParametros[j].parametro[i].valor;
                                    }

                            }

                                objPCRFSuscriberQuota.listSuscriberQuota.Add(objSuscriberQuotasubLista);

                            }

                        }

                    }
                }
                else
                {
                    objPCRFSuscriberQuota.msjRespuesta = "Error al cargar PCRF";
                    Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, "Error WS - ConsultarPCRFDegradacion" + objPCRFSuscriberQuota.msjRespuesta);
                }

            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, "Error WS - ConsultarPCRFDegradacion: " + ex.Message);
            }
            return objPCRFSuscriberQuota;
        }

        public static List<Entity.Transac.Service.Common.Client> GetDatosporNroDocumentos(string strIdSession,string strTransaction, string strTipDoc, string strDocumento, string strEstado)
        {

            List<Entity.Transac.Service.Common.Client> ListDatos = new List<Entity.Transac.Service.Common.Client>();
            try
            {
                DbParameter[] parameters ={   
                                            new DbParameter("PI_TIPODOCUMENTO", DbType.Int32,ParameterDirection.Input,strTipDoc),
                                            new DbParameter("PI_DOCUMENTO", DbType.String,ParameterDirection.Input,strDocumento),
                                            new DbParameter("PI_STATUS", DbType.String,ParameterDirection.Input,strEstado),
                                            new DbParameter("PO_CURSOR", DbType.Object,ParameterDirection.Output),
                                            new DbParameter("PO_CODERROR", DbType.Int32,ParameterDirection.Output),
                                            new DbParameter("PO_MSGERROR", DbType.String,200,ParameterDirection.Output)
                };


                    DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_POST_BSCSSS_OBT_DATOS, parameters, (IDataReader reader) =>
                    {

                        while (reader.Read())
                        {
                            ListDatos.Add(new Entity.Transac.Service.Common.Client()
                            {
                                CONTRATO_ID = (reader["CONTRATO"]).ToString(),
                                RESPONSABLE_PAGO = (reader["DESCRIPCION_PLAN"]).ToString(),
                                // FECHA_ACT =  Convert.ToDateTime(reader["FECHA_ALTA"]),
                                CARGO = (reader["CARGO_TOTAL"]).ToString(),
                                ESTADO_CUENTA = (reader["STATUS"]).ToString(),
                                CICLO_FACTURACION = (reader["CICLO"]).ToString(),
                                CUSTOMER_ID = (reader["CUSTOMER"]).ToString(),
                                S_APELLIDOS = (reader["TITULAR"]).ToString(),
                                TELEFONO = (reader["NUMERO"]).ToString(),
                                TIPO_CLIENTE = (reader["TIPO_CLIENTE"]).ToString(),

                            });
                        }

                    });

            }
            catch (Exception ex)
            {
                ListDatos = null;
                Web.Logging.Error(strIdSession, strTransaction, "Error  - GetDatosporNroDocumentos: " + ex.Message);
            }

            return ListDatos;
        }



        /// <summary>
        /// ObtenerTelefonosClienteLTE
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns></returns>
        /// <remarks>ObtenerTelefonosClienteLTE</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Hitss</CreadoPor></item>
        /// <item><FecCrea>06/03/2021.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>XX/XX/XXXX.</FecActu></item>
        /// <item><Resp>Hitss</Resp></item>
        /// <item><Mot>Se obtiene el teelfono del cliente</Mot></item></list>
        public static PCRFConnectorResponse ObtenerTelefonosClienteLTE(PCRFConnectorRequest objRequest)
        {
            PCRFConnectorResponse objResponse = null;
            DbParameter[] parameters = 
            {   
                new DbParameter("p_co_id", DbType.Int64,22,ParameterDirection.Input, objRequest.strAccountId),
                new DbParameter("p_cursor", DbType.Object,ParameterDirection.Output),
                new DbParameter("p_resultado", DbType.Int64,22,ParameterDirection.Output),
                new DbParameter("p_msgerr", DbType.String,300,ParameterDirection.Output)};


            try
            {
                Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    DbFactory.ExecuteReader(objRequest.Audit.Session, objRequest.Audit.Transaction, DbConnectionConfiguration.SIAC_POST_BSCS,
                        DbCommandConfiguration.SIACU_POST_BSCS_LISTA_TELEFONO_LTE, parameters, (IDataReader reader) =>
                        {
                            objResponse = new PCRFConnectorResponse();

                            while (reader.Read())
                            {
                                var item = new GenericItem
                                {
                                    Descripcion = reader["DN_NUM"].ToString(),
                                };
                                objResponse.strTelefonoLTE = item.Descripcion;
                            }
                        });
                });

            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }
            return objResponse;
        }
    }
}
