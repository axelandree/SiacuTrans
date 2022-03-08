using System;
using System.Collections.Generic;
using Claro.Data;
using System.Data;
using  System.Web;
using Claro.SIACU.Data.Transac.Service.Configuration;
using SIMCARD = Claro.SIACU.ProxyService.Transac.Service.SIACPost.SimCard;
using POSTPAID_CONSULTCLIENT = Claro.SIACU.ProxyService.Transac.Service.SIACPost.Customer;
using WSVALIDAIDENTIDAD = Claro.SIACU.ProxyService.Transac.Service.WSValidaIdentidad;
using WSREGISTRARTRAZABILIDAD = Claro.SIACU.ProxyService.Transac.Service.SIACU.WSRegisterTraceability;
using WSDIGITALSIGNATURE = Claro.SIACU.ProxyService.Transac.Service.SIACU.WSDigitalSignature;
using KEY = Claro.ConfigurationManager;
using Claro.SIACU.Entity.Transac.Service.Postpaid;
using Claro.SIACU.Entity.Transac.Service.Postpaid.GetActDesServProg;
using Claro.SIACU.Entity.Transac.Service.Postpaid.GetServiceBSCS;
using Claro.SIACU.Entity.Transac.Service.Postpaid.GetServiceByContract;
using Claro.SIACU.Entity.Transac.Service.Postpaid.GetServicesDTH;
using Claro.SIACU.Entity.Transac.Service.Postpaid.GetTypeTransactionBRMS;
using Claro.SIACU.Entity.Transac.Service.Postpaid.GetValidateActDesServProg;
using Claro.SIACU.Entity.Transac.Service.Postpaid.GetBiometricConfiguration;
using Claro.SIACU.Entity.Transac.Service.Postpaid.GetSignDocument;
using Claro.SIACU.ProxyService.Transac.Service.ActivaDesactivaServicioComercialWS;
using Claro.SIACU.ProxyService.Transac.Service.ServiciosPostpagoWS;
using Claro.SIACU.ProxyService.Transac.Service.WSBRMS;
using Claro.SIACU.Transac.Service;
using Claro.Web;
using POSTPAID = Claro.SIACU.Entity.Transac.Service.Postpaid;
using COMMON = Claro.SIACU.Entity.Transac.Service.Common;
using FUNCTIONS = Claro.SIACU.Transac.Service;
using Claro.SIACU.ProxyService.Transac.Service.WSValidaIdentidad;
using System.Linq;
using POSTPAID_CAMBIODATOS = Claro.SIACU.ProxyService.Transac.Service.CambioDatosSiacWS;

namespace Claro.SIACU.Data.Transac.Service.Postpaid
{
    public class Postpaid
    {
        
        /// <summary>
        /// ListarFacturas - PostPago
        /// </summary>
        /// <param name="sesion"></param>
        /// <param name="transaction"></param>
        /// <param name="customerCode"></param>
        /// <param name="msgError"></param>
        /// <returns></returns>
        public static List<ListItem> GetListBill(string sesion, string transaction, string customerCode, ref string msgError)
        {
            List<ListItem> list = new List<ListItem>();

            DbParameter[] parameters = new DbParameter[]
            {
                new DbParameter("K_CODIGOCLIENTE", DbType.String,255, ParameterDirection.Input,customerCode),
                new DbParameter("K_ERRMSG", DbType.String, 255,ParameterDirection.Output),
                new DbParameter("K_LISTA", DbType.Object, ParameterDirection.Output)
            };

            DbFactory.ExecuteReader(sesion,transaction,DbConnectionConfiguration.SIAC_POST_DBTO,DbCommandConfiguration.SIACU_TOLS_OBTENERDATOSDECUENTA,parameters,(IDataReader reader)=> 
            {
                while (reader.Read())
	            {
                     list.Add( new ListItem(){
                         Code = reader["Invoicenumber"] + "$" +reader["FechaInicio"] + "$" +reader["FechaFin"],
                         Code2 = Functions.CheckStr(reader["Periodo"]),
                         Description = Functions.CheckStr(reader["FechaEmision"])
                     });
                }
            }); 
            msgError = parameters[1].Value.ToString();
  
            return list;
        }
        

        public static string ValidateMail(string strIdSession, string strTransaction, string strCustomerCode)
        {
            string strServicio;

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_CUSTOMER_ID", DbType.String, ParameterDirection.Input, strCustomerCode),
                new DbParameter("P_STATUS", DbType.String,1, ParameterDirection.Output),
            };

            DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_SP_VALIDA_MAIL, parameters);
            strServicio = parameters[1].Value.ToString();

            return strServicio;
        }

        /// <summary>
        /// Método que obitene el estado de la línea.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strApplicationName">Nombre de la aplicación</param>
        /// <param name="strApplicationUser">Usuario de la aplicación</param>
        /// <param name="strPhoneNumber">Línea consultada</param>
        /// <param name="strResponseMessage">Respuesta del servicio</param>
        /// <returns>Devuelve el estado de la línea.</returns>
        public static string GetStatusPhone(string strIdSession, string strTransactionID, string strApplicationID, string strApplicationName, string strApplicationUser, string strPhoneNumber, ref string strResponseMessage)
        {
            string strResponse = "";
            string strMessage = "";
            SIMCARD.itNumTelefType[] oNumTelefType = null;
            SIMCARD.itReturnType[] oReturnType = null;

            try
            {
                strResponse = Claro.Web.Logging.ExecuteMethod<string>(strIdSession, strTransactionID, Configuration.WebServiceConfiguration.PREPAID_SIMCARD, () =>
                {
                    return Configuration.WebServiceConfiguration.PREPAID_SIMCARD.consultarTelefonoTodos(ref strTransactionID, strApplicationID, strApplicationName, strApplicationUser, strPhoneNumber, out strMessage, out oNumTelefType, out oReturnType);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransactionID, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new Exception(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            if (string.Equals(strResponse, Claro.Constants.NumberZeroString, StringComparison.OrdinalIgnoreCase))
            {
                if (oNumTelefType != null)
                {
                    if (oNumTelefType.Length > 0)
                    {
                        strMessage = oNumTelefType[0].descrStatus;
                    }
                    else
                    {
                        strResponse = Claro.Constants.NumberOneString;
                    }
                }
                else
                {
                    strResponse = Claro.Constants.NumberTwoString;
                }
            }

            strResponseMessage = strMessage;
            return strResponse;
        }

        /// <summary>
        /// Método que retorna lista con los datos de portabilidad.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="telefono">Teléfono</param>
        /// <param name="respuesta">Respuesta</param>
        /// <returns>Devuelve listado de objetos Portability con información de portabilidad.</returns>
        public static List<Entity.Transac.Service.Postpaid.Portability> GetPortability(string strIdSession, string strTransaction, string telefono, out string respuesta)
        {
            List<Entity.Transac.Service.Postpaid.Portability> objPortability = new List<Entity.Transac.Service.Postpaid.Portability>();
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("V_TELEFONO", DbType.String, 20, ParameterDirection.Input, telefono),
                new DbParameter("RESPUESTA", DbType.String, 50 ,ParameterDirection.Output ),
                new DbParameter("K_RESULTADO", DbType.Object, ParameterDirection.Output)
            };

            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_PVU, DbCommandConfiguration.SIACU_SP_CONSULTA_PORTABILIDAD, parameters, (IDataReader reader) =>
            {
                Entity.Transac.Service.Postpaid.Portability item;

                while (reader.Read())
                {
                    item = new Entity.Transac.Service.Postpaid.Portability();

                    item.NUMERO_SOLICITUD = Convert.ToString(reader["SOPOC_ID_SOLICITUD"]);
                    item.ESTADO_PROCESO = Convert.ToString(reader["SOPOC_ESTA_PROCESO"]);
                    item.DESCRPCION_ESTADO_PROCESO = Convert.ToString(reader["PARAV_DESCRIPCION"]);
                    item.TIPO_PORTABILIDAD = Convert.ToString(reader["SOPOC_TIPO_PORTA"]);
                    item.FECHA_REGISTRO = Convert.ToDate(reader["SOPOT_FECHA_REGISTRO"]);
                    if (item.ESTADO_PROCESO == KEY.AppSettings("PortabilidadEstadoFinOk"))
                    {
                        item.FECHA_EJECUCION = Convert.ToDate(reader["SOPOT_FECHA_EJECUCION"]);

                        if (item.TIPO_PORTABILIDAD == KEY.AppSettings("PortabilidadPortIN"))
                        {
                            item.CODIGO_OPERADOR_CEDENTE = Convert.ToString(reader["SOPOC_CODIGO_CEDENTE"]);
                        }
                        if (item.TIPO_PORTABILIDAD == KEY.AppSettings("PortabilidadPortOUT"))
                        {
                            item.CODIGO_OPERADOR_RECEPTOR = Convert.ToString(reader["SOPOC_CODIGO_RECEPTOR"]);
                        }
                    }
                    objPortability.Add(item);
                }
            });
            respuesta = Convert.ToString(parameters[1].Value);

            return objPortability;
        }

        /// <summary>
        /// Método que devuelve una lista con los datos de los trios por número de contrato.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="intContractId">Id de contrato</param>
        /// <returns>Devuelve listado de objetos Striations con información de triación.</returns>
        public static List<Entity.Transac.Service.Postpaid.Striations> GetTriaciones(string strIdSession, string strTransaction, int intContractId)
        {
            List<Entity.Transac.Service.Postpaid.Striations> listStriations = null;
            POSTPAID_CONSULTCLIENT.ConsultaTriados[] objTriados = Claro.Web.Logging.ExecuteMethod<POSTPAID_CONSULTCLIENT.ConsultaTriados[]>(strIdSession, strTransaction, Configuration.ServiceConfiguration.POSTPAID_CONSULTCLIENT, () => { return Configuration.ServiceConfiguration.POSTPAID_CONSULTCLIENT.consultaTriados(intContractId); });

            if (objTriados.Length >= 1)
            {
                listStriations = new List<Entity.Transac.Service.Postpaid.Striations>();

                foreach (var item in objTriados)
                {
                    listStriations.Add(new Entity.Transac.Service.Postpaid.Striations()
                    {
                        NUM_TRIO = item.num_trio,
                        TIPO_DESTINO = item.Tipo_Destino,
                        FACTOR = item.factor,
                        TELEFONO = item.telefono,
                        TIPO_TRIADO = item.tipo_triado,
                        DESTINO_TRIO = item.dest_trio,
                        CODIGO_TIPO_DESTINO = item.Cod_Tipo_Destino
                    });
                }
            }

            return listStriations;
        }

        public static List<ListItem> GetDocumentType(string strIdSession, string strTransaction, string strCodCargaDdl)
        {
            var msg = string.Format("Metodo: {0}, Request: {1}", "GetDocumentType", strCodCargaDdl);
            Claro.Web.Logging.Info("Session: " + strIdSession, "Transaction: 270492 ", "Message" + msg);
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("p_id_lista", DbType.String,100, ParameterDirection.Input, strCodCargaDdl),
                new DbParameter("p_cursor", DbType.Object, ParameterDirection.Output)
                
            };

            List<ListItem> listItem = null;
            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_SP_OBTIENE_LISTAS, parameters, (IDataReader reader) =>
            {
                listItem = new List<ListItem>();

                while (reader.Read())
                {
                    listItem.Add(new ListItem()
                    {
                        Description = Convert.ToString(reader["DESCRIPCION"]),
                        Code = Convert.ToString(reader["VALOR"])
                    });
                }
            });

            return listItem;
        }

        //Método que obtiene una lista de las lineas asociadas
        public static List<LineDetail> GetQueryAssociatedLines(string strIdSession, string strTransaction, string PhoneNumber, DateTime formatDateIni, DateTime formatDateEnd, Int32 TypeQuery, ref string Result)
        {
            var msg = string.Format("Metodo: {0}, Request: {1}, {2}, {3}, {4}", "GetQueryAssociatedLines", PhoneNumber, formatDateIni, formatDateEnd, TypeQuery);
            Claro.Web.Logging.Info("Session: " + strIdSession, "Transaction: 270492 ", "Message" + msg);
            List<LineDetail> listLineDetail = new List<LineDetail>();
            DbParameter[] parameters = new DbParameter[]{
                new DbParameter("V_MSISDN",DbType.String, ParameterDirection.Input, PhoneNumber),
                new DbParameter("V_FEC_INI",DbType.Date, ParameterDirection.Input, formatDateIni),
                new DbParameter("V_FEC_FIN",DbType.Date,ParameterDirection.Input, formatDateEnd),
                new DbParameter("V_CUR_SAL", DbType.Object,ParameterDirection.Output),
                new DbParameter("V_ERRNUM", DbType.String,255,ParameterDirection.Output),
                new DbParameter("V_ERRMSJ", DbType.String,255,ParameterDirection.Output),
                new DbParameter("V_IND", DbType.Int32,ParameterDirection.Input,TypeQuery)

            };

            if (PhoneNumber.Substring(0, 2) == "51")
            {
                parameters[0].Value = PhoneNumber.Substring(2);
            }
            else
            {
                parameters[0].Value = PhoneNumber;
            }

            parameters[1].Value = formatDateIni;
            parameters[2].Value = formatDateEnd;
            parameters[6].Value = TypeQuery;
            msg = string.Format("Metodo: {0}", "GetQueryAssociatedLines"); 
            Claro.Web.Logging.Info("Session: " + strIdSession, "Transaction: 270492 ", "Message" + msg + '-' + formatDateIni + '-' + formatDateEnd + '-' + TypeQuery);

            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_SP_TIM113_CONS_LINEAS_ASOC, parameters, (IDataReader reader) => {
                LineDetail entity = null;
                while (reader.Read())
                {   entity = new LineDetail();


                        entity.CodId = Functions.CheckStr(reader["CO_ID"]);
                        entity.DnNum = Functions.CheckStr(reader["DN_NUM"]);
                        
                        entity.DtIni = Functions.CheckDate(reader["CS_ACTIV_DATE"].ToString());
                        entity.DtFin = Functions.CheckDate(reader["CS_DEACTIV_DATE"].ToString());

                        entity.StrDtIni = entity.DtIni.ToString("dd/MM/yyyy");
                        entity.StrDtFin = entity.DtFin.ToString("dd/MM/yyyy");

                    if (!(String.IsNullOrEmpty(entity.StrDtIni)))
                    {
                        entity.StrDtIni = entity.StrDtIni.Substring(6, 4) + "-" + entity.StrDtIni.Substring(3, 2) + "-" + entity.StrDtIni.Substring(0, 2);
                    }
                    if (!(entity.StrDtFin == ""))
                    {
                        entity.StrDtFin = entity.StrDtFin.Substring(6, 4) + "-" + entity.StrDtFin.Substring(3, 2) + "-" + entity.StrDtFin.Substring(0, 2);
                    }
                    else
                    {
                        entity.StrDtFin = formatDateEnd.ToString();
                    }

                    listLineDetail.Add(entity);
                }

            });

                Result = parameters[5].Value.ToString();
                msg = string.Format("Metodo: {0}, Request: {1}, {2}", "GetQueryAssociatedLines", listLineDetail.Count.ToString(), Result);
            Claro.Web.Logging.Info("Session: " + strIdSession, "Transaction: 270492 ", "Message" + msg);
            return listLineDetail;
        }
        
        
        //Método que obtiene una lista de llamadas entrantes con las lineas asociadas
        public static List<CallDetail> GetIncomingCallDetail(string strIdSession, string strTransaction, string PhoneNumber, string Date_Ini, string Date_Fin)
        {
            var msg = string.Format("Metodo: {0}, Request: {1}, {2}, {3}", "GetIncomingCallDetail", PhoneNumber, Date_Ini, Date_Fin);
            Claro.Web.Logging.Info("Session: " + strIdSession, "Transaction: 270492 ", "Message" + msg);
            List<CallDetail> list = new List<CallDetail>();
            DbParameter[] parameters = new DbParameter[]{
                new DbParameter("V_MSISDN", DbType.String,255,ParameterDirection.Input, PhoneNumber),
                new DbParameter("V_INI", DbType.String,255,ParameterDirection.Input, Date_Ini),
                new DbParameter("V_FIN", DbType.String,255,ParameterDirection.Input, Date_Fin),
                new DbParameter("P_CURSOR_SALIDA", DbType.Object,ParameterDirection.Output),
                new DbParameter("V_FLAG", DbType.String,255,ParameterDirection.Output),
             };
            //TODO
            

            try
            {
                DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIACU_DM, DbCommandConfiguration.SIACU_SP_DET_LLAM_ENTRANTES, parameters, (IDataReader reader) =>
                {
                    CallDetail entity = null;
                    int count = 0;
                    while (reader.Read())
                    {
                        count++;
                        entity = new CallDetail();
                        entity.NROORD = count.ToString();
                        entity.MSISDN = Functions.CheckStr(reader["NUMEROA"]);
                        entity.CALLDATE = Functions.CheckStr(reader["FECHA"]);
                        entity.CALLTIME = Functions.CheckStr(reader["HORA_INICIO"]);
                        entity.CALLNUMBER = Functions.CheckStr(reader["NUMEROB"]);
                        entity.CALLDURATION = Functions.CheckStr(reader["DURACION"]);

                        list.Add(entity);
                    }
                });
            }
            catch (Exception e)
            {
                Claro.Web.Logging.Info("Session: " + strIdSession, "Transaction: 270492 ", "Message" + e.InnerException + "---" + e.Message);
                throw;
            }

            return list;
        }
        public static string GetDataline(string strIdSession, string strTransaction, int intContractID, ref COMMON.Line Dataline, ref string Message)
        {
            var msg = string.Format("Metodo: {0}, Request: {1}", "GetDataline", intContractID);
            Claro.Web.Logging.Info("Session: " + strIdSession, "Transaction: 270492 ", "Message" + msg);
            string strCodReturn = "";

            POSTPAID_CONSULTCLIENT.contrato[] ObjContract = Claro.Web.Logging.ExecuteMethod<POSTPAID_CONSULTCLIENT.contrato[]>
                (strIdSession, strTransaction,
                Configuration.ServiceConfiguration.POSTPAID_CONSULTCLIENT,
                () =>
                {
                    return Configuration.ServiceConfiguration.POSTPAID_CONSULTCLIENT.datosContrato(intContractID);
                });
            
            if (ObjContract.Length >= 1)
            {
                Dataline = new COMMON.Line();
                Dataline.DateStatus = ObjContract[0].fec_estado;
                Dataline.LineStatus = ObjContract[0].estado;
                Dataline.Reason = ObjContract[0].motivo;
                Dataline.Plan = ObjContract[0].plan;
                Dataline.TermContract = ObjContract[0].plazo_contrato;
                Dataline.NumICCID = ObjContract[0].iccid;
                Dataline.NumIMSI = ObjContract[0].imsi;
                Dataline.Sale = ObjContract[0].vendedor;
                Dataline.Bell = ObjContract[0].campania;
                Dataline.ActivationDate = Functions.CheckStr(ObjContract[0].fecha_act);
                Dataline.FlagPlatform = ObjContract[0].flag_plataforma;
                Dataline.PIN1 = ObjContract[0].pin1;
                Dataline.PIN2 = ObjContract[0].pin2;
                Dataline.PUK1 = ObjContract[0].puk1;
                Dataline.PUK2 = ObjContract[0].puk2;
                Dataline.CodPlanTariff = Functions.CheckStr(ObjContract[0].codigo_plan_tarifario);
                Dataline.PhoneNumber = ObjContract[0].telefono;
                Dataline.ContractID = Functions.CheckStr(ObjContract[0].co_id);
                strCodReturn = "1";

            }else
            {
                Dataline.LineStatus = "";
                Dataline.Reason = "";
                Dataline.Plan = "";
                Dataline.TermContract = "";
                Dataline.NumICCID = "";
                Dataline.NumIMSI = "";
                Dataline.Sale = "";
                Dataline.Bell = "";
                Dataline.FlagPlatform = "";
                Dataline.PIN1 = "";
                Dataline.PIN2 = "";
                Dataline.PUK1 = "";
                Dataline.PUK2 = "";
                Dataline.PhoneNumber = "";
                Dataline.ContractID = "";
                strCodReturn = "Error: No existe datos";
            }
            Logging.Info("Session: " + strIdSession, "Transaction: 270492 ", "Message de saida" + strCodReturn);
            return strCodReturn;
        }

        public static List<AmountIncomingCall> GetAmountIncomingCall(string strIdSession, string strTransaction, string Name, ref string Message)
        {
            var msg = string.Format("Metodo: {0}, Request: {1}", "GetAmountIncomingCall", Name);
            Claro.Web.Logging.Info("Session: " + strIdSession, "Transaction: 270492 ", "Message" + msg);
            List<AmountIncomingCall> listAmountIncomingCall = new List<AmountIncomingCall>();
            DbParameter[] parameters = {
                                           new DbParameter("P_NOMBRE", DbType.String, ParameterDirection.Input, Name),
                                           new DbParameter("P_MENSAJE", DbType.String, ParameterDirection.Output),
                                           new DbParameter("P_CURSOR", DbType.Object, ParameterDirection.Output)
                                       };
            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_SP_OBTENER_DATO, parameters, (IDataReader reader) =>
            {
                AmountIncomingCall entity = null;

                while (reader.Read())
                {
                    entity = new AmountIncomingCall();
                    entity.Description = Functions.CheckStr(reader["DESCRIPCION"]);
                    entity.ValorN = Functions.CheckDbl(reader["VALOR_N"]);
                    listAmountIncomingCall.Add(entity);
                }
            });

            Message = parameters[1].Value.ToString();
            return listAmountIncomingCall;
        }

        public static bool AlignTransactionService(string strIdSession, string strTransaction, string strContractID)
        {
            var msg = string.Format("Metodo: {0}, Request: {1}", "AlignTransactionService", strContractID);
            Claro.Web.Logging.Info("Session: " + strIdSession, "Transaction: 270492 ", "Message" + msg);
            int intResult;
            bool blnResponse;
            DbParameter[] parameters = {
                new DbParameter("P_CO_ID", DbType.String, 25, ParameterDirection.Input, strContractID)
            };

            intResult = DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_ALINEA_SERV_DESACT, parameters);

            if (intResult > 0)
                blnResponse = true;
            else
                blnResponse = false;

            return blnResponse;
        }

        public static bool AlignCodID(string strIdSession, string strTransaction, string strContractID)
        {
            var msg = string.Format("Metodo: {0}, Request: {1}", "AlignCodID", strContractID);
            Claro.Web.Logging.Info("Session: " + strIdSession, "Transaction: 270492 ", "Message" + msg);
            int intResult;
            bool blnResponse;
            DbParameter[] parameters = {
                new DbParameter("P_CO_ID", DbType.String, 25, ParameterDirection.Input, strContractID),
                new DbParameter("P_RESULTADO", DbType.String, 255, ParameterDirection.Output),
            };

            intResult = DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_ALINEA_CO_ID, parameters);

            if (intResult > 0)
                blnResponse = true;
            else
                blnResponse = false;

            return blnResponse;
        }

        public static bool UpdateInteraction(string strIdSession, string strTransaction, string p_objid, string p_texto, string p_orden, out string rFlagInsercion, out string rMsgText)
        {

            var msg = string.Format("Metodo: {0}, Request: {1}, {2}, {3}", "UpdateInteraction", p_objid, p_texto, p_orden);
            Claro.Web.Logging.Info("Session: " + strIdSession, "Transaction: 270492 ", "Message" + msg);
            bool blnResult = false;
            rFlagInsercion = "";
            rMsgText = "";
            DbParameter[] parameters = {
                                           new  DbParameter("P_INTERACT_ID", DbType.Int64, ParameterDirection.Input),
                                           new  DbParameter("P_TEXTO", DbType.String,1000,ParameterDirection.Input),
                                           new  DbParameter("P_ORDEN", DbType.String,1,ParameterDirection.Input),
                                           new  DbParameter("P_FLAG_INSERCION", DbType.String, 255, ParameterDirection.Output),
                                           new  DbParameter("P_MSG_TEXT", DbType.String, 255, ParameterDirection.Output)

                                       };

            for (int i = 0; i < parameters.Length; i++)
            {
                parameters[i].Value = System.DBNull.Value;
            }

            int j = 0;
            if (p_objid != "")
            {
                parameters[j].Value =   Functions.CheckInt64(p_objid);
            }


            if (p_texto != "")
            {
                parameters[1].Value = Functions.CheckStr(p_texto);
            }

            if (p_orden != "")
            {
                parameters[2].Value = Functions.CheckStr(p_orden);
            }

            DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_CLARIFY, DbCommandConfiguration.SIACU_SP_UPDATE_INTERACT_X_AUDIT, parameters);

            rFlagInsercion = Functions.CheckStr(parameters[3].Value);
            rMsgText = Functions.CheckStr(parameters[4].Value);

            blnResult = true;

            return blnResult;
        }

        public static Int32 InsertAdjustForClaim(string strIdSession, string strTransaction, Int64 iCustomerId, string strCodOCC, string strDate, string strPeriod, double dAmount, string strComment)
        {
            var msg = string.Format("Metodo: {0}, Request: {1}, {2}, {3}, {4}, {5}, {6}", "InsertAdjustForClaim", iCustomerId, strCodOCC, strDate, strPeriod, dAmount, strComment);
            Claro.Web.Logging.Info("Session: " + strIdSession, "Transaction: 270492 ", "Message" + msg);
            Int32 iReturn = 0;

            DbParameter[] parameters = {
                                           new DbParameter("p_CodCli", DbType.Int64, ParameterDirection.Input, iCustomerId),
                                           new DbParameter("p_CodOCC", DbType.String,255, ParameterDirection.Input, strCodOCC),
                                           new DbParameter("p_FecVig", DbType.String,255, ParameterDirection.Input, strDate),
                                           new DbParameter("p_NumCuo", DbType.String,255, ParameterDirection.Input, strPeriod),
                                           new DbParameter("p_Monto", DbType.Double, ParameterDirection.Input, dAmount),
                                           new DbParameter("p_Coment", DbType.String,255, ParameterDirection.Input, strComment),
                                           new DbParameter("p_Result", DbType.Int32, ParameterDirection.Output)

                                       };

            DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_SP_AJUSTE_POR_RECLAMOS, parameters);

            iReturn = Convert.ToInt(parameters[6].Value.ToString());
            return iReturn;
        }

        public static bool GetApprovalBusinessCreditLimitBusinessAccount(string strIdSession, string strTransaction, string strAccount, Int64 intContract, Int64 intService,
            out decimal dNewCharge, out decimal dMaxCharge, out Int64 dError, out string strErrorMessage)
        {

            var msg = string.Format("Metodo: {0}, Request: {1}, {2}, {3}", "GetApprovalBusinessCreditLimitBusinessAccount", strAccount, intContract, intService);
            Claro.Web.Logging.Info("Session: " + strIdSession, "Transaction: 270492 ", "Message" + msg);

            bool result = false;
            dNewCharge = 0;
            dMaxCharge = 0;
            dError = 0;
            strErrorMessage = "";


            DbParameter[] parameters =
            {
                new DbParameter("p_cuenta", DbType.String, 30, ParameterDirection.Input,strAccount),
                new DbParameter("p_co_id", DbType.Int64, ParameterDirection.Input, intContract),
                new DbParameter("p_co_ser", DbType.Int64, ParameterDirection.Input, intService),
                new DbParameter("p_total_cf", DbType.Double, ParameterDirection.Output),
                new DbParameter("p_limite_c", DbType.Double, ParameterDirection.Output),
                new DbParameter("p_error", DbType.Int64, ParameterDirection.Output),
                new DbParameter("p_msg_err", DbType.String, 1000, ParameterDirection.Output)
            };

            DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS,
                DbCommandConfiguration.SIACU_SP_TOTAL_CF_LC_X_CUENTA, parameters);

            dNewCharge = Functions.CheckDecimal(parameters[3].Value.ToString());
            dMaxCharge = Functions.CheckDecimal(parameters[4].Value.ToString());
            dError = Functions.CheckInt(parameters[5].Value.ToString());
            strErrorMessage = Functions.CheckStr(parameters[6].Value);
            result = true;
            return result;
        }


        public static int UserExistsBSCS(string strIdSession, string strTransaction, string strUser)
        {
            var msg = string.Format("Metodo: {0}, Request: {1}", "UserExistsBSCS", strUser);
            Claro.Web.Logging.Info("Session: " + strIdSession, "Transaction: 270492 ", "Message" + msg);
            int resultado;

            DbParameter[] parameters =
            {
                new DbParameter("result", DbType.Int32, ParameterDirection.ReturnValue),
                new DbParameter("p_usuario", DbType.String, 16, ParameterDirection.Input, strUser)
            };

            DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS,
                DbCommandConfiguration.SIACU_SP_VALIDAR_USUARIO, parameters);

            resultado = Functions.CheckInt(parameters[0].Value.ToString());

            return resultado;
        }

        public static string ConsultServices(string strIdSession, string strTransaction, int intCodId, int intCodServ,
            out int intSnCode, out int intSpCode, out string strErrorMsg, out string strErrorNum, out string strServ)
        {
            var msg = string.Format("Metodo: {0}, Request: {1}, {2}", "ConsultServices", intCodId, intCodServ);
            Claro.Web.Logging.Info("Session: " + strIdSession, "Transaction: 270492 ", "Message" + msg);

            strErrorNum = string.Empty;
            strServ = string.Empty;

            intSnCode = 0;
            intSpCode = 0;
            strErrorMsg = string.Empty;


            DbParameter[] parameters =
            {
                new DbParameter("p_coid", DbType.Int32, 30, ParameterDirection.Input, intCodId),
                new DbParameter("p_co_ser", DbType.Int32, 30 , ParameterDirection.Input, intCodServ),
                new DbParameter("p_co_des", DbType.String, 30 , ParameterDirection.Output),
                new DbParameter("p_sncode", DbType.Int32, 30 , ParameterDirection.Output),
                new DbParameter("p_spcode", DbType.Int32, 30 , ParameterDirection.Output),
                new DbParameter("p_errnum", DbType.Int32, 30 , ParameterDirection.Output),
                new DbParameter("p_errmsj", DbType.String, 30 , ParameterDirection.Output),
 
            };


            DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS,
                DbCommandConfiguration.SIACU_SP_SNCODE_X_CO_SER, parameters);

            strServ = parameters[2].Value.ToString();
            intSnCode = Functions.CheckInt(parameters[3].Value.ToString());
            intSpCode = Functions.CheckInt(parameters[4].Value.ToString());
            strErrorNum = parameters[5].Value.ToString();
            strErrorMsg = parameters[6].Value.ToString();

            return strErrorNum;
        }

        public static string ModifyServiceQuotAmountresponse(string strIdSession, string strTransaction, int intCodId,
            int intSnCode, int intSpCode, double dCost, int intPeriod, out string strResult)
        {
            var msg = string.Format("Metodo: {0}, Request: {1}, {2}, {3}, {4}, {5}", "ModifyServiceQuotAmountresponse", intCodId, intSnCode, intSpCode, dCost, intPeriod);
            Claro.Web.Logging.Info("Session: " + strIdSession, "Transaction: 270492 ", "Message" + msg);
            strResult = string.Empty;

            DbParameter[] parameters =
            {
                new DbParameter("p_co_id", DbType.Int32, 30, ParameterDirection.Input, intCodId),
                new DbParameter("p_sncode", DbType.Int32, 30, ParameterDirection.Input, intSnCode),
                new DbParameter("p_spcode", DbType.Int32, 30, ParameterDirection.Input, intSpCode),
                new DbParameter("p_costo", DbType.Decimal, ParameterDirection.Input, dCost),
                new DbParameter("p_periodos", DbType.Int32, 30, ParameterDirection.Input, intPeriod)
            };

            DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS,
                DbCommandConfiguration.SIACU_SP_MOD_CF_SERVICIO, parameters);

            strResult = SIACU.Transac.Service.Constants.Message_OK;
            return strResult;

        }

        public static TypeTransactionBRMSResponse GetTypeTransactionBRMS(TypeTransactionBRMSRequest objRequest)
        {
            REGLASAUTOMATIZACIONDEDOCUMENTOSResponse objServiceResponse = new REGLASAUTOMATIZACIONDEDOCUMENTOSResponse();
            REGLASAUTOMATIZACIONDEDOCUMENTOSRequest objServiceRequest = new REGLASAUTOMATIZACIONDEDOCUMENTOSRequest();

            objServiceRequest.pedidoEspecifico = new pedidoEspecifico();
            pedidoEspecifico objSpecificOrder = new pedidoEspecifico();
            objSpecificOrder.pedidoEspecifico1 = new consulta[1];

            consulta objConsult = new consulta();
            producto objProduct = new producto();

            objServiceRequest.pedidoGeneral = new pedidoGeneral();
            pedidoGeneral objGeneralOrder = new pedidoGeneral();

            proceso objProcess = new proceso();
            pedidoGeneral1 objGeneralOrder1 = new pedidoGeneral1();

            TypeTransactionBRMSResponse objResponse = new TypeTransactionBRMSResponse();

            try
            {
                objConsult.identificador = objRequest.StrIdentifier;
                objProduct.retencion = objRequest.StrRetention;
                objProduct.campana = string.Empty;
                objConsult.producto = objProduct;
                objSpecificOrder.pedidoEspecifico1[0] = objConsult;

                objProcess.operacion = objRequest.StrOperationCodSubClass;
                objProcess.transaccion = objRequest.StrTransactionM;

                objGeneralOrder1.proceso = objProcess;
                objGeneralOrder.pedidoGeneral1 = objGeneralOrder1;

                objServiceRequest.pedidoEspecifico = objSpecificOrder;
                objServiceRequest.pedidoGeneral = objGeneralOrder;

                objServiceResponse = Claro.Web.Logging.ExecuteMethod<REGLASAUTOMATIZACIONDEDOCUMENTOSResponse>(objRequest.Audit.Session, objRequest.Audit.Transaction,
                () =>
                {
                    return Configuration.WebServiceConfiguration.objWSBRMSService
                        .REGLASAUTOMATIZACIONDEDOCUMENTOS(objServiceRequest);
                });


                if (objServiceResponse.listaGeneral.listaGeneral1.Length > 0)
                {
                    objResponse.StrResult = objServiceResponse.listaGeneral.listaGeneral1[0];
                }
                else if (objServiceResponse.listaEspecifica.listaEspecifica1.Length > 0)
                {
                    objResponse.StrResult = objServiceResponse.listaEspecifica.listaEspecifica1[0].archivo;
                }

            }
            catch (Exception ex)
            {
                objResponse.StrError = ex.Message;
                return objResponse;
            }


            return objResponse;

        }

        public static ActDesServProgResponse ActDesServProg(ActDesServProgRequest objRequest)
        {
            ActivaDesactivaServiciosProgResponse objServiceResponse = new ActivaDesactivaServiciosProgResponse();
            ActivaDesactivaServiciosProgRequest objServiceRequest = new ActivaDesactivaServiciosProgRequest();


            ActDesServProgResponse objResponse = new ActDesServProgResponse();


            string[] strDateProg = System.Text.RegularExpressions.Regex.Split(objRequest.StrDateProg, "/");
            string strMonth = string.Empty;
            string strDay = string.Empty;

            try
            {
                objServiceRequest.idTransaccion = objRequest.StrTransactionId;
                objServiceRequest.ipAplicacion = objRequest.StrIpApplication;
                objServiceRequest.aplicacion = objRequest.StrApplication;
                objServiceRequest.msisdn = objRequest.StrMsisDn;
                objServiceRequest.co_id = objRequest.StrCoId;
                objServiceRequest.customer_id = objRequest.StrCustomerId;
                objServiceRequest.co_ser = objRequest.StrCoSer;
                objServiceRequest.flag_occ_apadece = objRequest.StrFlagOccApadece;
                objServiceRequest.monto_fid_apadece = objRequest.DAmountFidApadece;
                objServiceRequest.nuevo_CF = objRequest.DNewCf;
                objServiceRequest.tipo_reg = objRequest.StrTypeReg;
                objServiceRequest.ciclo_fact = objRequest.ICycleFact;
                objServiceRequest.cod_serv = objRequest.StrCodSer;
                objServiceRequest.desc_serv = objRequest.StrDesSer;
                objServiceRequest.nro_cuenta = objRequest.StrNumberAccount;
                objServiceRequest.usuario_aplicacion = objRequest.StrUserApplication;
                objServiceRequest.usuario_sistema = objRequest.StrUserSystem;
                objServiceRequest.fecha_prog =
                    Functions.CheckDate(strDateProg[2] + "-" + strDateProg[1] + "-" + strDateProg[0]);

                if (DateTime.Now.Month < 10)
                {
                    strMonth = SIACU.Transac.Service.Constants.strCero + DateTime.Now.Month;
                }
                else
                {
                    strMonth = Functions.CheckStr(DateTime.Now.Month);
                }
                if (DateTime.Now.Day < 10)
                {
                    strDay = SIACU.Transac.Service.Constants.strCero + DateTime.Now.Day;
                }
                else
                {
                    strDay = Functions.CheckStr(DateTime.Now.Day);
                }
                objServiceRequest.fecha_reg = Functions.CheckDate(DateTime.Now.Year + "-" + strMonth + "-" + strDay);
                objServiceRequest.id_interaccion = objRequest.StrIdInteract;
                objServiceRequest.tipo_servicio = objRequest.StrTypeSer;

                objServiceResponse = Web.Logging.ExecuteMethod<ActivaDesactivaServiciosProgResponse>(
                    objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return WebServiceConfiguration.objEbsActivaDesactivaServicio
                            .activaDesactivaServiciosProg(objServiceRequest);
                    });
                objResponse.StrCodError = objServiceResponse.codigoRespuesta;
                objResponse.StrDesResponse = objServiceResponse.mensajeRespuesta;
                objResponse.BlnResposne = true;
            }
            catch (Exception ex)
            {
                Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    string.Format("Error: {0}", ex.Message));
                objResponse.BlnResposne = false;
            }
            finally
            {
                Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    string.Format("strCodError: {0};strDesRespuesta: {1}", objResponse.StrCodError, objResponse.StrDesResponse));
            }
            return objResponse;
        }

        public static ServiceByContractResponse GetServiceByContract(ServiceByContractRequest objRequest)
        {
            ServiceByContractResponse objResponse = new ServiceByContractResponse();

            List<ServiceByContract> ListServiceByContract = new List<ServiceByContract>();
            serviciosContratoResponse objServiceResponse = new serviciosContratoResponse();
            serviciosContratoRequest objServiceRequest = new serviciosContratoRequest();


            objServiceRequest.login = objRequest.StrUser;
            objServiceRequest.sistema = objRequest.StrSystem;
            objServiceRequest.coId = objRequest.StrCoid;

            try
            {

                objServiceResponse = Claro.Web.Logging.ExecuteMethod<serviciosContratoResponse>(objRequest.Audit.Session,
                    objRequest.Audit.Transaction,
                    () =>
                    {
                        return WebServiceConfiguration.ActDesactServiciosComerciales.serviciosContrato(objServiceRequest);
                    });

                objResponse.StrMessage = objServiceResponse.errMsj;
                objResponse.StrDesPlan = objServiceResponse.timDes;
                objResponse.StrResult = objServiceResponse.errNum;

                if (Functions.CheckInt(objResponse.StrResult) == Constants.NumberZero)
                {
                    if (objServiceResponse.servicio == null)
                    {
                        return objResponse;
                    }

                    int intQuantity = objServiceResponse.servicio.Length;
                    for (int i = 0; i < intQuantity; i++)
                    {
                        ServiceByContract obj = new ServiceByContract();
                        obj._cod_grupo = objServiceResponse.servicio[i].coGrp;
                        obj._des_grupo = objServiceResponse.servicio[i].deGrp;
                        obj._pos_grupo = objServiceResponse.servicio[i].noGrp;
                        obj._cod_serv = objServiceResponse.servicio[i].coSer;
                        obj._des_serv = objServiceResponse.servicio[i].deSer;
                        obj._pos_serv = objServiceResponse.servicio[i].noSer;
                        obj._cod_excluyente = objServiceResponse.servicio[i].coExcl;
                        obj._des_excluyente = objServiceResponse.servicio[i].deExcl;
                        obj._estado = "";

                        if (objServiceResponse.servicio[i].estado == "A")
                        {
                            obj._estado = "Activo";
                        }

                        if (objServiceResponse.servicio[i].estado == "D")
                        {
                            obj._estado = "Desactivo";
                        }

                        obj._fecha_validez = objServiceResponse.servicio[i].valido;
                        obj._monto_cargo_sus = objServiceResponse.servicio[i].suscrip;
                        obj._monto_cargo_fijo = objServiceResponse.servicio[i].cargoFijo;
                        obj._cuota_modif = objServiceResponse.servicio[i].cuota;
                        obj._monto_final = objServiceResponse.servicio[i].final;
                        obj._periodos_validos = objServiceResponse.servicio[i].period;
                        obj._bloqueo_desact = objServiceResponse.servicio[i].BloqDes;
                        obj._bloqueo_act = objServiceResponse.servicio[i].BloqAct;

                        ListServiceByContract.Add(obj);
                        

                    }

                }

                objResponse.ListServiceByContract = ListServiceByContract;

            }
            catch (Exception ex)
            {
                Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, string.Format("Error: {0}", ex.Message));
            }
            return objResponse;
        }

        public static ServicesDTHResponse GetServicesDTH(string strIdSession, string strTransaction, int intCoId,
            string strMsisdn)
        {
            ServicesDTHResponse objRespnse = new ServicesDTHResponse();
            List<ServicesDTH> listServiceDTH = null;
            int P_RESULTADO = 0;
            string P_MSGERR = string.Empty;
            DbParameter[] parameters =
            {
                new DbParameter("P_CO_ID", DbType.Int64, 30, ParameterDirection.Input, intCoId),
                new DbParameter("P_MSISDN", DbType.String, 20, ParameterDirection.Input, strMsisdn),
                new DbParameter("P_RESULTADO", DbType.Int64, ParameterDirection.Output),
                new DbParameter("P_MSGERR", DbType.String, 300, ParameterDirection.Output),
                new DbParameter("P_CURSOR", DbType.Object, ParameterDirection.Output)
            };

            try
            {
                DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS,
                    DbCommandConfiguration.SIACU_SP_CONSULTA_DECO, parameters, (IDataReader reader) =>
                    {
                        listServiceDTH = new List<ServicesDTH>();
                        while (reader.Read())
                        {
                            listServiceDTH.Add(new ServicesDTH()
                            {
                                COD_DECO = Functions.CheckStr(reader["NRO_DECO"]),
                                TIP_DECO = Functions.CheckStr(reader["TIPO_DECO"]),
                                COD_TARJETA = Functions.CheckStr(reader["NRO_TARJETA"]),
                                TIP_EQUIPO = Functions.CheckStr(reader["TIPO_EQUIPO"]),
                                ESTADO = Functions.CheckStr(reader["ESTADO"]),
                                FEC_ESTADO = Functions.CheckStr(reader["FEC_ESTADO"])
                            });
                        }


                    });

            }
            catch (Exception ex)
            {
                Logging.Info(strIdSession, strTransaction, string.Format("Error: {0}", ex.Message));
            }
            finally
            {
                P_RESULTADO = Functions.CheckInt(parameters[2].Value.ToString());
                P_MSGERR = Functions.CheckStr(parameters[3].Value);

                Logging.Info(strIdSession, strTransaction, string.Format("Parametros de salida ----> P_RESULTADO: {0},P_MSGERR: {1}", P_RESULTADO, P_MSGERR));

            }

            objRespnse.IntResult = P_RESULTADO;
            objRespnse.StrMessageError = P_MSGERR;
            objRespnse.ListServicesDTH = listServiceDTH;

            return objRespnse;
        }


        public static ValidateActDesServProgResponse ValidateActDesServProg(ValidateActDesServProgRequest objRequest)
        {
            ValidateActDesServProgResponse objResponse = new ValidateActDesServProgResponse();


            ValidarActivaDesactivaServiciosResponse objServiceResponse = new ValidarActivaDesactivaServiciosResponse();
            ValidarActivaDesactivaServiciosRequest objServiceRequest = new ValidarActivaDesactivaServiciosRequest();
            objServiceRequest.idTransaccion = objRequest.StrIdTransaction;
            objServiceRequest.ipAplicacion = objRequest.StrIpAplication;
            objServiceRequest.aplicacion = objRequest.StrAplication;
            objServiceRequest.msisdn = objRequest.StrMsisdn;
            objServiceRequest.co_id = objRequest.StrCoId;
            objServiceRequest.co_ser = objRequest.StrCoSer;
            objServiceRequest.tip_reg = objRequest.StrTypeReg;
            objServiceRequest.cod_serv = objRequest.StrCodServ;
            objServiceRequest.servc_estado = Constants.NumberOneString;


            try
            {
                objServiceResponse =
                    Claro.Web.Logging.ExecuteMethod<ValidarActivaDesactivaServiciosResponse>(objRequest.Audit.Session, objRequest.Audit.Transaction,
                        () =>
                        {
                            return WebServiceConfiguration.objEbsActivaDesactivaServicio
                                .validarActivaDesactivaServicios(objServiceRequest);
                        });

                objResponse.StrCodError = objServiceResponse.codigoRespuesta;
                objResponse.StrDesResponse = objServiceResponse.mensajeRespuesta;

            }
            catch (Exception ex)
            {
                Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, string.Format("Error: {0}", ex.Message));

            }

            return objResponse;
        }

        public static ServiceBSCSResponse ServiceBSCS(ServiceBSCSRequest objRequest)
        {
            ServiceBSCSResponse objResponse = new ServiceBSCSResponse();
            List<ServiceBSCS> listServiceBSCS = new List<ServiceBSCS>();
            serviciosBSCSServComercialResponse objServiceResponse = new serviciosBSCSServComercialResponse();
            serviciosBSCSServComercialRequest objServiceRequest = new serviciosBSCSServComercialRequest();

            objServiceRequest.login = objRequest.StrUser;
            objServiceRequest.sistema = objRequest.StrSystem;
            objServiceRequest.coSer = objRequest.StrCodServ;

            try
            {
                objServiceResponse = Claro.Web.Logging.ExecuteMethod<serviciosBSCSServComercialResponse>(
                    objRequest.Audit.Session,
                    objRequest.Audit.Transaction,
                    () =>
                    {
                        return WebServiceConfiguration.ActDesactServiciosComerciales
                            .serviciosBSCSServComercial(objServiceRequest);
                    });

                objResponse.StrMsg = objServiceResponse.errMsj;
                objResponse.StrDesServ = objServiceResponse.deSer;
                objResponse.StrResult = objServiceResponse.errNum;
                if (Functions.CheckInt(objResponse.StrResult) == Constants.NumberZero)
                {
                    int intQuantity = objServiceResponse.servicioBscs.Length;
                    for (int i = 0; i < intQuantity; i++)
                    {
                        POSTPAID.ServiceBSCS obj = new ServiceBSCS();
                        obj.StrService = HttpContext.Current.Server.HtmlEncode(objServiceResponse.servicioBscs[i].servicio);
                        obj.StrPackage = HttpContext.Current.Server.HtmlEncode(objServiceResponse.servicioBscs[i].paquete);
                        obj.StrStatus = HttpContext.Current.Server.HtmlEncode(objServiceResponse.servicioBscs[i].estado);
                        listServiceBSCS.Add(obj);
                    }
                }

                objResponse.ListServiceBSCS = listServiceBSCS;
            }
            catch (Exception ex)
            {
                Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, string.Format("Error: {0}", ex.Message));
            }

            return objResponse;
        }

        public static SIACU.Entity.Transac.Service.Postpaid.GetInsertTraceability.InsertTraceabilityResponse GetInsertTraceability(SIACU.Entity.Transac.Service.Postpaid.GetInsertTraceability.InsertTraceabilityRequest objInsertTraceabilityRequest)
        {
                WSREGISTRARTRAZABILIDAD.parametrosAuditRequest objparametrosAuditRequest = new WSREGISTRARTRAZABILIDAD.parametrosAuditRequest()
                {
                    idTransaccion = objInsertTraceabilityRequest.Audit.Transaction,
                    ipAplicacion = objInsertTraceabilityRequest.Audit.IPAddress,
                    nombreAplicacion = KEY.AppSettings("strNombreAplicacionPostpago"),
                    usuarioAplicacion = objInsertTraceabilityRequest.Audit.UserName
                };
                SIACU.Entity.Transac.Service.Postpaid.GetInsertTraceability.InsertTraceabilityResponse objInsertTraceabilityResponse = new SIACU.Entity.Transac.Service.Postpaid.GetInsertTraceability.InsertTraceabilityResponse();

                try
                {
                    WSREGISTRARTRAZABILIDAD.registrarRequest objregistrarRequest = new WSREGISTRARTRAZABILIDAD.registrarRequest()
                    {
                        auditRequest = objparametrosAuditRequest,
                        biometria = new WSREGISTRARTRAZABILIDAD.parametrosBiometria()
                        {
                            idPadre = objInsertTraceabilityRequest.item.IdPadre,
                            codOperacion = objInsertTraceabilityRequest.item.CodOperacion,
                            sistema = objInsertTraceabilityRequest.item.Sistema,
                            codCanal = objInsertTraceabilityRequest.item.CodCanal,
                            codPdv = objInsertTraceabilityRequest.item.CodPdv,
                            codModalVenta = objInsertTraceabilityRequest.item.CodModalVenta,
                            tipoDocumento = objInsertTraceabilityRequest.item.TipoDocumento,
                            numeroDocumento = objInsertTraceabilityRequest.item.NumeroDocumento,
                            lineas = objInsertTraceabilityRequest.item.Lineas,
                            veprnId = objInsertTraceabilityRequest.item.VeprnId,
                            dniAutorizado = objInsertTraceabilityRequest.item.DNIAutorizado,
                            usuarioCtaRed = objInsertTraceabilityRequest.item.UsuarioCtaRed,
                            idHijo = objInsertTraceabilityRequest.item.IdHijo,
                            padreAnt = objInsertTraceabilityRequest.item.PadreAnt,
                            dniConsultado = objInsertTraceabilityRequest.item.DNIConsultado,
                            wsOrigen = objInsertTraceabilityRequest.item.WSOrigen,
                            tipoValidacion = objInsertTraceabilityRequest.item.TipoValidacion,
                            origenTipo = objInsertTraceabilityRequest.item.OrigenTipo,
                            codigoError = objInsertTraceabilityRequest.item.CodigoRptaExitoError,
                            mensajeProceso = objInsertTraceabilityRequest.item.MensajeProceso,
                            estado = objInsertTraceabilityRequest.item.Estado,
                            flag = objInsertTraceabilityRequest.item.Flag
                        }
                    };
                    if (objInsertTraceabilityRequest.lstRegisterTraceabilityOptionalList == null)
                    {
                        objregistrarRequest.listaCamposAdicionales = new WSREGISTRARTRAZABILIDAD.ListaCamposAdicionalesTypeCampoAdicional[1];

                    }
                    else
                    {
                        objregistrarRequest.listaCamposAdicionales = new WSREGISTRARTRAZABILIDAD.ListaCamposAdicionalesTypeCampoAdicional[objInsertTraceabilityRequest.lstRegisterTraceabilityOptionalList.Count];
                        for (int i = 0; i < objInsertTraceabilityRequest.lstRegisterTraceabilityOptionalList.Count; i++)
                        {
                            WSREGISTRARTRAZABILIDAD.ListaCamposAdicionalesTypeCampoAdicional l = new WSREGISTRARTRAZABILIDAD.ListaCamposAdicionalesTypeCampoAdicional();
                            l.nombreCampo = objInsertTraceabilityRequest.lstRegisterTraceabilityOptionalList[i].Campo;
                            l.valor = objInsertTraceabilityRequest.lstRegisterTraceabilityOptionalList[i].Valor;
                            objregistrarRequest.listaCamposAdicionales[i] = l;
                        }
                    }

                    WSREGISTRARTRAZABILIDAD.registrarResponse response = Claro.Web.Logging.ExecuteMethod<WSREGISTRARTRAZABILIDAD.registrarResponse>(
                    objInsertTraceabilityRequest.Audit.Session, objInsertTraceabilityRequest.Audit.Transaction,
                    Configuration.ServiceConfiguration.SIACURegistrarTrazabilidad,
                    () =>
                    {
                        return Configuration.ServiceConfiguration.SIACURegistrarTrazabilidad.registrarTrazabilidad(objregistrarRequest);
                    });

                objInsertTraceabilityResponse = new POSTPAID.GetInsertTraceability.InsertTraceabilityResponse()
                {
                    IdTransaccion = response.auditResponse.idTransaccion,
                    CodRespuesta = response.auditResponse.codRespuesta,
                    MsjRespuesta = response.auditResponse.msjRespuesta
                };
            }
            catch (Exception ex)
            {
                Logging.Info(objInsertTraceabilityRequest.Audit.Session, objInsertTraceabilityRequest.Audit.Transaction, string.Format("Error: {0}", ex.Message));
            }
            return objInsertTraceabilityResponse;
        }

        public static BiometricConfigurationResponse GetBiometricConfiguration(BiometricConfigurationRequest objRequest)
        {   
                WSVALIDAIDENTIDAD.PRS_ValidaIdentidad_WS objValidaIdentidad = new WSVALIDAIDENTIDAD.PRS_ValidaIdentidad_WS();

                BiometricConfigurationResponse objResponse = new BiometricConfigurationResponse();
                WSVALIDAIDENTIDAD.HeaderRequestType head = new WSVALIDAIDENTIDAD.HeaderRequestType();

                head.canal = objRequest.head.canal;
                head.fechaInicio = objRequest.head.fechaInicio;
                head.idAplicacion = objRequest.head.idAplicacion;
                head.idTransaccionESB = objRequest.head.idTransaccionESB;
                head.idTransaccionNegocio = objRequest.head.idTransaccionNegocio;
                head.nodoAdicional = objRequest.head.nodoAdicional;
                head.usuarioAplicacion = objRequest.head.usuarioAplicacion;
                head.usuarioSesion = objRequest.head.usuarioSesion;
                objValidaIdentidad.headerRequest = head;

                WSVALIDAIDENTIDAD.obtenerConfiguracionRequest request = new WSVALIDAIDENTIDAD.obtenerConfiguracionRequest();
                request.codCanal = objRequest.codCanal;
                request.codigoPDV = objRequest.codigoPDV;
                request.codModalVenta = objRequest.codModalVenta;
                request.codOperacion = objRequest.codOperacion;
                request.estado = objRequest.estado;
                request.idHijo = objRequest.idHijo;
                request.idPadre = objRequest.idPadre;
                request.numeroDocumento = objRequest.numeroDocumento;
                request.sistema = objRequest.sistema;
                request.tipoDocumento = objRequest.tipoDocumento;
                request.usuarioCtaRed = objRequest.usuarioCtaRed;

                if (objRequest.listaOpcional != null && objRequest.listaOpcional.Count > 0)
                {
                    request.listaRequestOpcional = new WSVALIDAIDENTIDAD.RequestOpcionalTypeRequestOpcional[objRequest.listaOpcional.Count];
                    for (int i = 0; i < objRequest.listaOpcional.Count; i++)
                    {
                        request.listaRequestOpcional[i].campo = objRequest.listaOpcional[i].Campo;
                        request.listaRequestOpcional[i].valor = objRequest.listaOpcional[i].Valor;
                    }
                }

                WSVALIDAIDENTIDAD.obtenerConfiguracionResponse response = new WSVALIDAIDENTIDAD.obtenerConfiguracionResponse();
                try
                {
                    response = objValidaIdentidad.obtenerConfiguracion(request);

                    objResponse.status = new ValidateIdentityStatusResponse();
                    objResponse.status.codigoRespuesta = response.responseStatus.codigoRespuesta;
                    objResponse.status.descripcionRespuesta = response.responseStatus.descripcionRespuesta;
                    objResponse.status.estado = response.responseStatus.estado;
                    objResponse.status.fecha = response.responseStatus.fecha;
                    objResponse.status.origen = response.responseStatus.origen;
                    objResponse.status.ubicacionError = response.responseStatus.ubicacionError;

                    if (response.responseData.listaObtenerConfigResponse.Count() > 0)
                    {
                        objResponse.data = new List<BiometricDataConfiguration>();
                        for (int i = 0; i < response.responseData.listaObtenerConfigResponse.Count(); i++)
                        {
                            BiometricDataConfiguration d = new BiometricDataConfiguration();
                            d.canacCodigo = response.responseData.listaObtenerConfigResponse[i].canacCodigo;
                            d.canavDescripcion = response.responseData.listaObtenerConfigResponse[i].canavDescripcion;
                            d.gpdvvCodigo = response.responseData.listaObtenerConfigResponse[i].gpdvvCodigo;
                            d.ovencCodigo = response.responseData.listaObtenerConfigResponse[i].ovencCodigo;
                            d.ovencEstado = response.responseData.listaObtenerConfigResponse[i].ovencEstado;
                            d.ovencSupervisorHuellaBc = response.responseData.listaObtenerConfigResponse[i].ovencSupervisorHuellaBc;
                            d.ovenvDescripcion = response.responseData.listaObtenerConfigResponse[i].ovenvDescripcion;
                            d.soxpnFlagBiometria = response.responseData.listaObtenerConfigResponse[i].soxpnFlagBiometria;
                            d.soxpnFlagEmail = response.responseData.listaObtenerConfigResponse[i].soxpnFlagEmail;
                            d.soxpnFlagFinVenta = response.responseData.listaObtenerConfigResponse[i].soxpnFlagFinVenta;
                            d.soxpnFlagFirmaDigital = response.responseData.listaObtenerConfigResponse[i].soxpnFlagFirmaDigital;
                            d.soxpnFlagHuellero = response.responseData.listaObtenerConfigResponse[i].soxpnFlagHuellero;
                            d.soxpnFlagIdValidator = response.responseData.listaObtenerConfigResponse[i].soxpnFlagIdValidator;
                            d.soxpnFlagIncapacidad = response.responseData.listaObtenerConfigResponse[i].soxpnFlagIncapacidad;
                            d.soxpnFlagNoBiometriaDc = response.responseData.listaObtenerConfigResponse[i].soxpnFlagNoBiometriaDc;
                            d.soxpnFlagNoBiometriaReniec = response.responseData.listaObtenerConfigResponse[i].soxpnFlagNoBiometriaReniec;
                            d.soxpnTipoError = response.responseData.listaObtenerConfigResponse[i].soxpnTipoError;
                            d.soxpnTipoVerificacionBio = response.responseData.listaObtenerConfigResponse[i].soxpnTipoVerificacionBio;
                            d.soxpnTipoVerificacionFirma = response.responseData.listaObtenerConfigResponse[i].soxpnTipoVerificacionFirma;
                            d.soxpvMensaje = response.responseData.listaObtenerConfigResponse[i].soxpvMensaje;
                            d.soxsvCodOperacion = response.responseData.listaObtenerConfigResponse[i].soxsvCodOperacion;
                            d.soxsvDescOperacion = response.responseData.listaObtenerConfigResponse[i].soxsvDescOperacion;
                            d.soxsvSistema = response.responseData.listaObtenerConfigResponse[i].soxsvSistema;
                            d.toficCodigo = response.responseData.listaObtenerConfigResponse[i].toficCodigo;
                            d.tprocCodigo = response.responseData.listaObtenerConfigResponse[i].tprocCodigo;
                            objResponse.data.Add(d);
                        }
                    }

                    if (response.responseData.listaResponseOpcional.Count() > 0)
                    {
                        objResponse.listaOpcional = new List<ValidateIdentityOptionalList>();
                        for (int i = 0; i < response.responseData.listaResponseOpcional.Count(); i++)
                        {
                            ValidateIdentityOptionalList l = new ValidateIdentityOptionalList();
                            l.Campo = response.responseData.listaResponseOpcional[i].campo;
                            l.Valor = response.responseData.listaResponseOpcional[i].valor;
                            objResponse.listaOpcional.Add(l);
                        }
                    }
            }
            catch (Exception ex)
            {
                Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, string.Format("Error: {0}", ex.Message));
            }
            return objResponse;
        }

        public static SignDocumentResponse GetSignDocument(SignDocumentRequest objSignDocumentRequest)
        {
            WSDIGITALSIGNATURE.HeaderRequestType objHeaderRequestType = new WSDIGITALSIGNATURE.HeaderRequestType()
            {
                canal = objSignDocumentRequest.ObjSignDocumentHeaderRequest.Canal,
                idAplicacion = objSignDocumentRequest.ObjSignDocumentHeaderRequest.IdAplicacion,
                usuarioAplicacion = objSignDocumentRequest.ObjSignDocumentHeaderRequest.UsuarioAplicacion,
                usuarioSesion = objSignDocumentRequest.ObjSignDocumentHeaderRequest.UsuarioSesion,
                idTransaccionESB = objSignDocumentRequest.ObjSignDocumentHeaderRequest.IdTransaccionESB,
                idTransaccionNegocio = objSignDocumentRequest.ObjSignDocumentHeaderRequest.IdTransaccionNegocio,
                fechaInicio = objSignDocumentRequest.ObjSignDocumentHeaderRequest.FechaInicio,
                nodoAdicional = objSignDocumentRequest.ObjSignDocumentHeaderRequest.NodoAdicional
            };
            WSDIGITALSIGNATURE.PRS_FirmaDigital_WS objPRS_FirmaDigital_WS = new WSDIGITALSIGNATURE.PRS_FirmaDigital_WS()
            {
                headerRequest = objHeaderRequestType
            };
            WSDIGITALSIGNATURE.DatosFirmarDocumentoRequest objDatosFirmarDocumentoRequest = new WSDIGITALSIGNATURE.DatosFirmarDocumentoRequest()
            {
                codigoPDV = objSignDocumentRequest.objSignDocument.CodigoPDV,
                nombrePDV = objSignDocumentRequest.objSignDocument.NombrePDV,
                tipoFirma = objSignDocumentRequest.objSignDocument.TipoFirma,
                tipoArchivo = objSignDocumentRequest.objSignDocument.TipoArchivo,
                negocio = objSignDocumentRequest.objSignDocument.Negocio,
                tipoContrato = objSignDocumentRequest.objSignDocument.TipoContrato,
                datFirma = objSignDocumentRequest.objSignDocument.DatFirma,
                origenArchivo = objSignDocumentRequest.objSignDocument.OrigenArchivo,
                codigoAplicacion = objSignDocumentRequest.objSignDocument.CodigoAplicacion,
                posFirma = objSignDocumentRequest.objSignDocument.PosFirma,
                nombreArchivo = objSignDocumentRequest.objSignDocument.NombreArchivo,
                ipAplicacion = objSignDocumentRequest.objSignDocument.IPAplicacion,
                numeroArchivo = objSignDocumentRequest.objSignDocument.NumeroArchivo,
                segmentoOferta = objSignDocumentRequest.objSignDocument.SegmentoOferta,
                plantillaBRMS = objSignDocumentRequest.objSignDocument.PlantillaBRMS,
                tipoOperacion = objSignDocumentRequest.objSignDocument.TipoOperacion,
                tipoDocumento = objSignDocumentRequest.objSignDocument.TipoDocumento,
                numeroDocumento = objSignDocumentRequest.objSignDocument.NumeroDocumento,
                contenidoArchivo = objSignDocumentRequest.objSignDocument.ContenidoArchivo,
                rutaArchivoDestino = objSignDocumentRequest.objSignDocument.RutaArchivoDestino,
                rutaArchivoOrigen = objSignDocumentRequest.objSignDocument.RutaArchivoOrigen,
                canalAtencion = objSignDocumentRequest.objSignDocument.CanalAtencion
            };

            WSDIGITALSIGNATURE.firmarDocumentoRequest objfirmarDocumentoRequest = new WSDIGITALSIGNATURE.firmarDocumentoRequest() {
                datosFirmarDocumentoRequest = objDatosFirmarDocumentoRequest
            };
            
            if (objSignDocumentRequest.lstSignDocumenOptionalList == null)
            {
                objfirmarDocumentoRequest.listaRequestOpcional = new WSDIGITALSIGNATURE.RequestOpcionalTypeRequestOpcional[1];
            }
            else
            {
                objfirmarDocumentoRequest.listaRequestOpcional = new WSDIGITALSIGNATURE.RequestOpcionalTypeRequestOpcional[objSignDocumentRequest.lstSignDocumenOptionalList.Count];
                for (int i = 0; i < objSignDocumentRequest.lstSignDocumenOptionalList.Count; i++)
                {

                    WSDIGITALSIGNATURE.RequestOpcionalTypeRequestOpcional l = new WSDIGITALSIGNATURE.RequestOpcionalTypeRequestOpcional();
                    l.campo = objSignDocumentRequest.lstSignDocumenOptionalList[i].Campo;
                    l.valor = objSignDocumentRequest.lstSignDocumenOptionalList[i].Valor;
                    objfirmarDocumentoRequest.listaRequestOpcional[i] = l;
                }
            }

            WSDIGITALSIGNATURE.firmarDocumentoResponse response = Claro.Web.Logging.ExecuteMethod<WSDIGITALSIGNATURE.firmarDocumentoResponse>(
                objSignDocumentRequest.Audit.Session, objSignDocumentRequest.Audit.Transaction,
                Configuration.WebServiceConfiguration.SIACUDigitalSignature,
                () =>
                {
                    return Configuration.WebServiceConfiguration.SIACUDigitalSignature.firmarDocumento(objfirmarDocumentoRequest);
                });


            SIACU.Entity.Transac.Service.Postpaid.GetSignDocument.SignDocumentResponse objSignDocumentResponse = new SIACU.Entity.Transac.Service.Postpaid.GetSignDocument.SignDocumentResponse()
            {
                ObjSignDocumentResponseStatus = new POSTPAID.GetSignDocument.SignDocumentResponseStatus()
                {
                    Estado = response.responseStatus.estado,
                    CodigoRespuesta = response.responseStatus.codigoRespuesta,
                    DescripcionRespuesta = response.responseStatus.descripcionRespuesta,
                    UbicacionError = response.responseStatus.ubicacionError,
                    Fecha = response.responseStatus.fecha,
                    Origen = response.responseStatus.origen
                },
                ObjSignDocumentResponseData = new POSTPAID.GetSignDocument.SignDocumentResponseData()
                {
                    ObjSignDocumentDatosFirmarDocumentoResponse = new POSTPAID.GetSignDocument.SignDocumentDatosFirmarDocumentoResponse() { 
                        RutaArchivo = response.responseData.datosFirmarDocumentoResponse.rutaArchivo,
                        IdTransaccion = response.responseData.datosFirmarDocumentoResponse.idTransaccion,
                        FechaInicio = response.responseData.datosFirmarDocumentoResponse.fechaInicio,
                        FechaFin = response.responseData.datosFirmarDocumentoResponse.fechaFin,
                        CodigoRespuesta = response.responseData.datosFirmarDocumentoResponse.codigoRespuesta,
                        MensajeRespuesta = response.responseData.datosFirmarDocumentoResponse.mensajeRespuesta,
                        DescripcionRespuesta = response.responseData.datosFirmarDocumentoResponse.descripcionRespuesta,
                    }
                }
            };

            if (response.responseData.listaResponseOpcional.Length > 0)
            {
                objSignDocumentResponse.ObjSignDocumentResponseData.LstSignDocumenOptionalList = new List<SignDocumenOptionalList>();
                for (int i = 0; i < response.responseData.listaResponseOpcional.Length; i++)
                {
                    SignDocumenOptionalList lstBEFirmaDigitalListaOpcional = new SignDocumenOptionalList();
                    lstBEFirmaDigitalListaOpcional.Campo = response.responseData.listaResponseOpcional[i].campo;
                    lstBEFirmaDigitalListaOpcional.Valor = response.responseData.listaResponseOpcional[i].valor;
                    objSignDocumentResponse.ObjSignDocumentResponseData.LstSignDocumenOptionalList.Add(lstBEFirmaDigitalListaOpcional);
                }
            }

            return objSignDocumentResponse;
        }

        public static string GetDataCustomer(string strIdSession, string strTransaction, string strcustomerid, string strtelefono, ref COMMON.Client Dataline, ref string Message)
        {
            string strCodRetorno = "";
            string strError = "";

            try
            {

                POSTPAID_CONSULTCLIENT.cliente[] objTempCliente = new POSTPAID_CONSULTCLIENT.cliente[1];
                POSTPAID_CONSULTCLIENT.datosClienteResponse objdatosClienteResponse = Claro.Web.Logging.ExecuteMethod<POSTPAID_CONSULTCLIENT.datosClienteResponse>
                    (strIdSession, strTransaction,
                    Configuration.ServiceConfiguration.POSTPAID_CONSULTCLIENT,
                    () =>
                    {
                        return Configuration.ServiceConfiguration.POSTPAID_CONSULTCLIENT.datosCliente(strcustomerid, strtelefono);
                    });

                objTempCliente = objdatosClienteResponse.cliente;
                strError = objdatosClienteResponse.errorsql;
                if (strError == "")
                {
                    if (objTempCliente.Length >= 1)
                    {
                        Dataline.NOMBRES = Claro.Utils.CheckStr(objTempCliente[0].nombre);
                        Dataline.APELLIDOS = Claro.Utils.CheckStr(objTempCliente[0].apellidos);
                        Dataline.CUENTA = Claro.Utils.CheckStr(objTempCliente[0].cuenta);
                        Dataline.SEXO = Claro.Utils.CheckStr(objTempCliente[0].sexo);
                        Dataline.NRO_DOC = Claro.Utils.CheckStr(objTempCliente[0].num_doc);
                        Dataline.DOMICILIO = objTempCliente[0].direccion_fac;
                        Dataline.ZIPCODE = objTempCliente[0].cod_postal_fac;
                        Dataline.DEPARTAMENTO = objTempCliente[0].departamento_fac;
                        Dataline.DISTRITO = objTempCliente[0].distrito_fac;
                        Dataline.RAZON_SOCIAL = objTempCliente[0].razonSocial;
                        Dataline.PROVINCIA = objTempCliente[0].provincia_fac;
                        Dataline.DNI_RUC = objTempCliente[0].ruc_dni;
                        Dataline.ASESOR = objTempCliente[0].asesor;
                        Dataline.CICLO_FACTURACION = objTempCliente[0].ciclo_fac;
                        Dataline.CONSULTOR = objTempCliente[0].consultor;
                        Dataline.MODALIDAD = objTempCliente[0].modalidad;
                        Dataline.SEGMENTO = objTempCliente[0].segmento;
                        Dataline.CREDIT_SCORE = objTempCliente[0].credit_score;
                        Dataline.ESTADO_CUENTA = objTempCliente[0].status_cuenta;
                        Dataline.FECHA_ACT = objTempCliente[0].fecha_act;
                        Dataline.LIMITE_CREDITO = Claro.Utils.CheckStr(objTempCliente[0].limite_credito);
                        Dataline.TOTAL_CUENTAS = Claro.Utils.CheckStr(objTempCliente[0].num_cuentas);
                        Dataline.TOTAL_LINEAS = Claro.Utils.CheckStr(objTempCliente[0].num_lineas);
                        Dataline.RESPONSABLE_PAGO = objTempCliente[0].respon_pago;
                        Dataline.TIPO_CLIENTE = objTempCliente[0].tipo_cliente;
                        Dataline.REPRESENTANTE_LEGAL = objTempCliente[0].rep_legal;
                        Dataline.EMAIL = objTempCliente[0].email;
                        Dataline.TELEF_REFERENCIA = objTempCliente[0].telef_principal;
                        Dataline.CONTACTO_CLIENTE = objTempCliente[0].contacto_cliente;
                        Dataline.TIPO_DOC = objTempCliente[0].tip_doc;
                        Dataline.NRO_DOC = objTempCliente[0].num_doc;
                        Dataline.NOMBRE_COMERCIAL = objTempCliente[0].nomb_comercial;
                        Dataline.FAX = objTempCliente[0].fax;
                        Dataline.CARGO = objTempCliente[0].cargo;

                        Dataline.CUSTOMER_ID = Claro.Utils.CheckStr(objTempCliente[0].customerId);

                        Dataline.CONTACTO_CLIENTE = objTempCliente[0].contacto_cliente;
                        Dataline.TELEFONO_CONTACTO = objTempCliente[0].telef_contacto;
                        Dataline.CALLE_FAC = objTempCliente[0].direccion_fac;
                        Dataline.POSTAL_FAC = objTempCliente[0].cod_postal_fac;
                        Dataline.URBANIZACION_FAC = objTempCliente[0].urbanizacion_fac;
                        Dataline.DEPARTEMENTO_FAC = objTempCliente[0].departamento_fac;
                        Dataline.PROVINCIA_FAC = objTempCliente[0].provincia_fac;
                        Dataline.DISTRITO_FAC = objTempCliente[0].distrito_fac;
                        Dataline.CALLE_LEGAL = objTempCliente[0].direccion_fac;
                        Dataline.POSTAL_LEGAL = objTempCliente[0].cod_postal_fac;
                        Dataline.URBANIZACION_LEGAL = objTempCliente[0].urbanizacion_leg;
                        Dataline.DEPARTEMENTO_LEGAL = objTempCliente[0].departamento_leg;
                        Dataline.PROVINCIA_LEGAL = objTempCliente[0].provincia_leg;
                        Dataline.DISTRITO_LEGAL = objTempCliente[0].distrito_leg;
                        Dataline.PAIS_FAC = objTempCliente[0].pais_fac;
                        Dataline.PAIS_LEGAL = objTempCliente[0].pais_leg;
                        Dataline.REFERENCIA = objTempCliente[0].urbanizacion_fac;
                        Dataline.CONTRATO_ID = Claro.Utils.CheckStr(objTempCliente[0].co_id);
                        Dataline.LUGAR_NACIMIENTO_DES = Claro.Utils.CheckStr(objTempCliente[0].lug_nac);

                        Dataline.LUGAR_NACIMIENTO_ID = Claro.Utils.CheckStr(objTempCliente[0].nacionalidad);

                        if (objTempCliente[0].fecha_nac.ToString() != "")
                        { Dataline.FECHA_NAC = (Claro.Utils.CheckDate(objTempCliente[0].fecha_nac)).ToString("dd/MM/yyyy"); } // Convert.ToDate convertir // Claro.Utils.CheckDate
                        else
                        { Dataline.FECHA_NAC = (Claro.Utils.CheckDate("1/1/1")).ToString("dd/MM/yyyy"); }
                        Dataline.ESTADO_CIVIL = Claro.Utils.CheckStr(objTempCliente[0].estado_civil);

                        Dataline.ESTADO_CIVIL_ID = Claro.Utils.CheckStr(objTempCliente[0].estado_civil_id);
                        Dataline.NICHO = Claro.Utils.CheckStr(objTempCliente[0].nicho_id);
                        Dataline.FORMA_PAGO = Claro.Utils.CheckStr(objTempCliente[0].forma_pago);
                        Dataline.COD_TIPO_CLIENTE = Claro.Utils.CheckStr(objTempCliente[0].codigo_tipo_cliente);

                        strCodRetorno = "1";
                    }
                    else
                    {
                        Dataline.NOMBRES = "";
                        Dataline.APELLIDOS = "";
                        Dataline.CUENTA = "";
                        Dataline.NRO_DOC = "";
                        Dataline.DOMICILIO = "";
                        Dataline.ZIPCODE = "";
                        Dataline.DEPARTAMENTO = "";
                        Dataline.DISTRITO = "";
                        Dataline.RAZON_SOCIAL = "";
                        Dataline.PROVINCIA = "";
                        Dataline.DNI_RUC = "";
                        Dataline.ASESOR = "";
                        Dataline.CICLO_FACTURACION = "";
                        Dataline.CONSULTOR = "";
                        Dataline.MODALIDAD = "";
                        Dataline.SEGMENTO = "";
                        Dataline.CREDIT_SCORE = "";
                        Dataline.ESTADO_CUENTA = "";
                        Dataline.FECHA_ACT = Claro.Utils.CheckDate("1/1/1");
                        Dataline.LIMITE_CREDITO = "";
                        Dataline.TOTAL_CUENTAS = "";
                        Dataline.TOTAL_LINEAS = "";
                        Dataline.RESPONSABLE_PAGO = "";
                        Dataline.TIPO_CLIENTE = "";
                        Dataline.REPRESENTANTE_LEGAL = "";
                        Dataline.EMAIL = "";
                        Dataline.TELEF_REFERENCIA = "";
                        Dataline.CONTACTO_CLIENTE = "";
                        Dataline.TIPO_DOC = "";
                        Dataline.NRO_DOC = "";
                        Dataline.NOMBRE_COMERCIAL = "";
                        Dataline.FAX = "";
                        Dataline.CARGO = "";
                        Dataline.CUSTOMER_ID = "";
                        Dataline.CONTACTO_CLIENTE = "";
                        Dataline.TELEFONO_CONTACTO = "";
                        Dataline.CALLE_FAC = "";
                        Dataline.POSTAL_FAC = "";
                        Dataline.URBANIZACION_FAC = "";
                        Dataline.DEPARTEMENTO_FAC = "";
                        Dataline.PROVINCIA_FAC = "";
                        Dataline.DISTRITO_FAC = "";
                        Dataline.CALLE_LEGAL = "";
                        Dataline.POSTAL_LEGAL = "";
                        Dataline.URBANIZACION_LEGAL = "";
                        Dataline.DEPARTEMENTO_LEGAL = "";
                        Dataline.PROVINCIA_LEGAL = "";
                        Dataline.DISTRITO_LEGAL = "";
                        Dataline.PAIS_FAC = "";
                        Dataline.PAIS_LEGAL = "";
                        Dataline.REFERENCIA = "";
                        Dataline.CONTRATO_ID = "";
                        Dataline.LUGAR_NACIMIENTO_DES = "";
                        Dataline.LUGAR_NACIMIENTO_ID = "";
                        Dataline.FECHA_NAC = (Claro.Utils.CheckDate("1/1/1")).ToString();
                        Dataline.ESTADO_CIVIL = "";
                        Dataline.ESTADO_CIVIL_ID = "";
                        Dataline.NICHO = "";
                        strCodRetorno = "Error: No existe datos";
                    }
                }
                else
                {
                    strCodRetorno = "Error: Al ejecutar SQL - Oracle";
                }

            }
            catch (Exception ex)
            {

            }
            return strCodRetorno;
        }

        public static List<ListItem> GetMotivoCambio(string sesion, string transaction, string pid_parametro,  string rMsgText)
        {
            List<ListItem> list = new List<ListItem>();

            DbParameter[] parameters = new DbParameter[]
            {
                new DbParameter("PI_CAMPO", DbType.String,250, ParameterDirection.Input,pid_parametro),
                new DbParameter("PO_CODE_RESULT", DbType.Int32, 22,ParameterDirection.Output),
                new DbParameter("PO_MESSAGE_RESULT", DbType.String, 250,ParameterDirection.Output),
                new DbParameter("PO_CURSOR", DbType.Object, ParameterDirection.Output)
            };

            try
            {
                DbFactory.ExecuteReader(sesion, transaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_SP_PARAMETRICO, parameters, (IDataReader reader) =>
                {
                    while (reader.Read())
                    {
                        list.Add(new ListItem()
                        {
                            Code = Functions.CheckStr(reader["VALOR1"]),
                            Code2 = Functions.CheckStr(reader["VALOR2"]),
                            Description = Functions.CheckStr(reader["DESCRIPCION"])
                        });
                    }
                });
                rMsgText = parameters[2].Value.ToString();
            }
            catch (Exception ex)
            {
                Logging.Info(sesion, transaction, string.Format("Error: {0}", ex.Message));
            }
            

            return list;
        }
        
public static string UpdateNameCustomer(string strIdTransaccion, string strIpAplicacion, string strAplicacion, string strUsrApp, Entity.Transac.Service.Common.Client oCliente, ref int intSeq_out)
        {
            string strResponse = string.Empty;
            try
            {
                POSTPAID_CAMBIODATOS.CambioDatosSIACWSService oService = new POSTPAID_CAMBIODATOS.CambioDatosSIACWSService();
                POSTPAID_CAMBIODATOS.actualizarNombreClienteResponse oResponse = new POSTPAID_CAMBIODATOS.actualizarNombreClienteResponse();
                oService.Url = WebServiceConfiguration.CambioDatosWS.Url;
                oService.Credentials = System.Net.CredentialCache.DefaultCredentials;
                oService.Timeout = Int32.Parse(KEY.AppSettings("intTimeoutDataPrePostWS"));

                POSTPAID_CAMBIODATOS.actualizarNombreClienteRequest oRequest = new POSTPAID_CAMBIODATOS.actualizarNombreClienteRequest()
                {
                    auditRequest = new POSTPAID_CAMBIODATOS.auditRequestType() { idTransaccion = strIdTransaccion, ipAplicacion = strIpAplicacion, nombreAplicacion = strAplicacion, usuarioAplicacion = strUsrApp },
                    apellido = oCliente.APELLIDOS,
                    nombres = oCliente.NOMBRES,
                    razonSocial = oCliente.RAZON_SOCIAL,
                    tipoDocumento = Int32.Parse(oCliente.TIPO_DOC_RL),  //Old
                    numeroDocumento = oCliente.NRO_DOC,                 //Old,
                    motivo = oCliente.MOTIVO_REGISTRO,
                    tipoDocumentoNuevo = Int32.Parse(oCliente.TIPO_DOC),//New
                    dniRucNuevo = oCliente.DNI_RUC,                     //New
                    listaOpcionalRequest = new POSTPAID_CAMBIODATOS.RequestOpcionalComplexType[0]
                };

                var msg = string.Format(" Metodo WS: UpdateNameCustomer, URL:{0}", oService.Url);
                Claro.Web.Logging.Info("Session: " + strIdTransaccion, "Transaction:  " + strIdTransaccion, "Message" + msg);

                var msg2 = string.Format(" Parámetros Entrada -> idTransaccion:{0}, ipAplicacion:{1}, nombreAplicacion:{2}, usuarioAplicacion:{3}, customerId:{4}, apellido: {5},nombres: {6}, razonSocial: {7}, tipoDoc: {8}, dniRuc: {9}, motivo: {10}", strIdTransaccion, strIpAplicacion, strAplicacion, strUsrApp, oCliente.CUSTOMER_ID, oCliente.APELLIDOS, oCliente.NOMBRES, oCliente.RAZON_SOCIAL, oCliente.TIPO_DOC, oCliente.DNI_RUC, oCliente.MOTIVO_REGISTRO);
                Claro.Web.Logging.Info("Session: " + strIdTransaccion, "Transaction:  " + strIdTransaccion, "Message" + msg2);

                oResponse = oService.actualizarNombreCliente(oRequest);

                if (oResponse.auditResponse.codigoRespuesta == "0")
                {
                    strResponse = "0";                    
                }
                else
                {
                    strResponse = oResponse.auditResponse.codigoRespuesta;
                }
            }
            catch (Exception ex)
            {
                Logging.Info(strUsrApp, strIdTransaccion,string.Format("Error: {0}", ex.Message));
                strResponse = "-1";
            }

            return strResponse;
        }

public static string UpdateDataMinorCustomer(string strIdTransaccion, string strIpAplicacion, string strAplicacion, string strUsrApp, Entity.Transac.Service.Common.Client oCliente, int intSeq_in, ref int intSeq_out)
        {
            string strResponse = string.Empty;
            try
            {
                POSTPAID_CAMBIODATOS.CambioDatosSIACWSService oService = new POSTPAID_CAMBIODATOS.CambioDatosSIACWSService();
                POSTPAID_CAMBIODATOS.actualizarDatosMenoresResponse oResponse = new POSTPAID_CAMBIODATOS.actualizarDatosMenoresResponse();
                oService.Url = WebServiceConfiguration.CambioDatosWS.Url;
                oService.Credentials = System.Net.CredentialCache.DefaultCredentials;
                oService.Timeout = Int32.Parse(KEY.AppSettings("intTimeoutDataPrePostWS"));

                POSTPAID_CAMBIODATOS.actualizarDatosMenoresRequest oRequest = new POSTPAID_CAMBIODATOS.actualizarDatosMenoresRequest()
                {
                    auditRequest = new POSTPAID_CAMBIODATOS.auditRequestType() { idTransaccion = strIdTransaccion, ipAplicacion = strIpAplicacion, nombreAplicacion = strAplicacion, usuarioAplicacion = strUsrApp },
                    cargo = oCliente.CARGO,
                    celular = oCliente.TELEFONO_CONTACTO,
                    contactoCliente = oCliente.CONTACTO_CLIENTE,
                    dniRepre = oCliente.NRO_DOC,
                    email = oCliente.EMAIL,
                    estadoCivil = oCliente.ESTADO_CIVIL_ID,
                    fax = oCliente.FAX,
                    fechaNacimiento = Convert.ToDate(oCliente.FECHA_NAC),
                    nacionalidad = Convert.ToString(oCliente.NACIONALIDAD),
                    nombreComercial = oCliente.NOMBRE_COMERCIAL,
                    repreLegal = oCliente.REPRESENTANTE_LEGAL,
                    sexo = oCliente.SEXO,
                    telefono = oCliente.TELEFONO,
                    motivo = oCliente.MOTIVO_REGISTRO,
                    //Nuevos Requests
                    tipoDocumento = Int32.Parse(oCliente.TIPO_DOC),//Old
                    numeroDocumento = oCliente.DNI_RUC,            //Old
                    updDataMinor = oCliente.FLAG_EMAIL,
                    updRegLegal = Convert.ToString(oCliente.FLAG_REGISTRADO),
                    updContact = oCliente.P_FLAG_CONSULTA,
                    tipoDocRepLegal = Convert.ToInt(oCliente.TIPO_CLIENTE)
                };
                
                var msg = string.Format(" Metodo WS: UpdateDataMinorCustomer, URL:{0}", oService.Url);
                Claro.Web.Logging.Info("Session: " + strIdTransaccion, "Transaction:  " + strIdTransaccion, "Message" + msg);
                
                oResponse = oService.actualizarDatosMenores(oRequest);

                if (oResponse.auditResponse.codigoRespuesta == "0")
	            {
		            strResponse = "0";
	            }else{
                    strResponse = "-1";
                }
            }
            catch (Exception ex)
            {
                Logging.Info(strUsrApp, strIdTransaccion,string.Format("Error: {0}", ex.Message));
                strResponse = "-1";
            }

            return strResponse;
        }

        public static string UpdateDataCustomerCLF(string strIdTransaccion, string strIpAplicacion, string strAplicacion, string strUsrApp, Entity.Transac.Service.Common.Client oCliente)
        {
            string strResponse = "0";
            try
            {
                POSTPAID_CAMBIODATOS.CambioDatosSIACWSService oService = new POSTPAID_CAMBIODATOS.CambioDatosSIACWSService();
                oService.Url = WebServiceConfiguration.CambioDatosWS.Url;
                oService.Credentials = System.Net.CredentialCache.DefaultCredentials;
                oService.Timeout = Int32.Parse(KEY.AppSettings("intTimeoutDataPrePostWS"));


                POSTPAID_CAMBIODATOS.actualizarClientesCLFRequest oRequest = new POSTPAID_CAMBIODATOS.actualizarClientesCLFRequest()
                {

                    auditRequest = new POSTPAID_CAMBIODATOS.auditRequestType() { idTransaccion = strIdTransaccion, ipAplicacion = strIpAplicacion, nombreAplicacion = strAplicacion, usuarioAplicacion = strUsrApp },
                    
                    contactoCliente = oCliente.CONTACTO_CLIENTE,
                    email = oCliente.EMAIL,
                    estCivil = oCliente.ESTADO_CIVIL_ID,
                    fax = oCliente.FAX,
                    fecNac = Convert.ToDate(oCliente.FECHA_NAC),
                    nomComercial = oCliente.NOMBRE_COMERCIAL,
                    objId = Int32.Parse(oCliente.OBJID_CONTACTO),
                    ocupacion = oCliente.CARGO,
                    pais = oCliente.PAIS_LEGAL,
                    sexo = oCliente.SEXO,
                    telReferencial = oCliente.TELEFONO_REFERENCIA_1
                };

                POSTPAID_CAMBIODATOS.actualizarClientesCLFResponse oResponse = new POSTPAID_CAMBIODATOS.actualizarClientesCLFResponse();

                //  log.Info(string.Format("Metodo WS: consultarNumeros, URL:{0}", oService.Url));
                //  log.Info(string.Format("Parámetros Entrada -> idTransaccion:{0}, ipApp:{1}, usrApp:{2}, aplicacion:{3}, codCli:{4}, codCnt: {5},telefonoRef1: {6}, telefonoRef2: {7}, usuario: {8}", strIdTransaccion, strIpAplicacion, strUsrApp, strAplicacion, oCliente.CODIGO_CLIENTE.Trim(), oCliente.COD_CNT.Trim(), oCliente.TELEFONO_REFERENCIA_1, oCliente.TELEFONO_REFERENCIA_2, strUsrApp));

                oResponse = oService.actualizarClientesCLF(oRequest);

                if (oResponse.auditResponse.codigoRespuesta != "0")
                {
                    strResponse = "-1";
                }

            }
            catch (Exception ex)
            {
                Logging.Info(strUsrApp, strIdTransaccion,
                    string.Format("Error: {0}", ex.Message));
            }

            return strResponse;
        }

        public static int registrarTransaccionSiga(string strIdTransaccion, string strIpAplicacion, string strAplicacion, string strUsrApp, Entity.Transac.Service.Fixed.TransactionSiga oTransaction)
        {
            int strResponse = 0;
            try
            {
                POSTPAID_CAMBIODATOS.CambioDatosSIACWSService oService = new POSTPAID_CAMBIODATOS.CambioDatosSIACWSService();
                oService.Url = WebServiceConfiguration.CambioDatosWS.Url;
                oService.Credentials = System.Net.CredentialCache.DefaultCredentials;
                oService.Timeout = Int32.Parse(KEY.AppSettings("intTimeoutDataPrePostWS"));


                POSTPAID_CAMBIODATOS.registrarTransaccionSiacPoRequest oRequest = new POSTPAID_CAMBIODATOS.registrarTransaccionSiacPoRequest()
                {

                    auditRequest = new POSTPAID_CAMBIODATOS.auditRequestType() { idTransaccion = strIdTransaccion, ipAplicacion = strIpAplicacion, nombreAplicacion = strAplicacion, usuarioAplicacion = strUsrApp },
                    codTipoOperacion = oTransaction.COD_TIPO_OPERACION,
                    direccionCliente = oTransaction.DIRECCION_CLIENTE.Trim(),
                    estadoAcuerdo = oTransaction.ESTADO_ACUERDO,
                    estadoApadece = oTransaction.ESTADO_APADECE,
                    flagEquipo = oTransaction.FLAG_EQUIPO,
                    fuenteActualizaci = oTransaction.FUENTE_ACTUALIZACI,
                    montoFideliza = Convert.ToInt(oTransaction.MONTO_FIDELIZA),
                    motivoApadece = oTransaction.MOTIVO_APADECE,
                    msisdn = oTransaction.MSISDN,
                    nombreCliente = oTransaction.NOMBRE_CLIENTE,
                    nroDocCliente = oTransaction.NRO_DOC_CLIENTE,
                    nroDocPago = oTransaction.NRO_DOC_PAGO,
                    razonSocial = oTransaction.RAZON_SOCIAL.Trim(),
                };

                POSTPAID_CAMBIODATOS.registrarTransaccionSiacPoResponse oResponse = new POSTPAID_CAMBIODATOS.registrarTransaccionSiacPoResponse();

                //  log.Info(string.Format("Metodo WS: consultarNumeros, URL:{0}", oService.Url));
                //  log.Info(string.Format("Parámetros Entrada -> idTransaccion:{0}, ipApp:{1}, usrApp:{2}, aplicacion:{3}, codCli:{4}, codCnt: {5},telefonoRef1: {6}, telefonoRef2: {7}, usuario: {8}", strIdTransaccion, strIpAplicacion, strUsrApp, strAplicacion, oCliente.CODIGO_CLIENTE.Trim(), oCliente.COD_CNT.Trim(), oCliente.TELEFONO_REFERENCIA_1, oCliente.TELEFONO_REFERENCIA_2, strUsrApp));

                oResponse = oService.registrarTransaccionSiacPo(oRequest);

                strResponse = oResponse.codError;

                //if (oResponse.auditResponse.codigoRespuesta != "0")
                //{
                //    strResponse = "-1";
                //}

            }
            catch (Exception ex)
            {
                Logging.Info(strUsrApp, strIdTransaccion,
                    string.Format("Error: {0}", ex.Message));
                strResponse = -1;
            }

            return strResponse;
        }

public static string UpdateAddressCustomer(string strIdTransaccion, string strIpAplicacion, string strAplicacion, string strUsrApp, Entity.Transac.Service.Common.Client oCliente, string tipoDireccion, int intSeq_in, ref int intSeq_out)
        {
            string strResponse = string.Empty;
            try
            {
                POSTPAID_CAMBIODATOS.CambioDatosSIACWSService oService = new POSTPAID_CAMBIODATOS.CambioDatosSIACWSService();
                oService.Url = WebServiceConfiguration.CambioDatosWS.Url;
                oService.Credentials = System.Net.CredentialCache.DefaultCredentials;
                oService.Timeout = Int32.Parse(KEY.AppSettings("intTimeoutDataPrePostWS"));

                POSTPAID_CAMBIODATOS.actualizarCambioDireccionRequest oRequest = new POSTPAID_CAMBIODATOS.actualizarCambioDireccionRequest()
                {
                    auditRequest = new POSTPAID_CAMBIODATOS.auditRequestType() { idTransaccion = strIdTransaccion, ipAplicacion = strIpAplicacion, nombreAplicacion = strAplicacion, usuarioAplicacion = strUsrApp },
                    codigoPostal = oCliente.POSTAL_LEGAL,
                    departamento = oCliente.DEPARTEMENTO_LEGAL,
                    direccion = oCliente.CALLE_LEGAL,
                    distrito = oCliente.DISTRITO_LEGAL,
                    notasDireccion = oCliente.REFERENCIA,
                    pais = oCliente.PAIS_LEGAL,
                    provincia = oCliente.PROVINCIA_LEGAL,
                    tipoDireccion = tipoDireccion,
                    motivo = oCliente.MOTIVO_REGISTRO,
                    //Nuevos Requests
                    tipoDocumento = Int32.Parse(oCliente.TIPO_DOC),//Old
                    numeroDocumento = oCliente.DNI_RUC,            //Old
                    customerid = Int32.Parse(oCliente.CUSTOMER_ID)
                };

                POSTPAID_CAMBIODATOS.actualizarCambioDireccionResponse oResponse = new POSTPAID_CAMBIODATOS.actualizarCambioDireccionResponse();

                oResponse = oService.actualizarCambioDireccion(oRequest);

                if (oResponse.auditResponse.codigoRespuesta == "0")
                {
                    strResponse = "0";
                }
                else
                {
                    strResponse = "-1";
                }

            }
            catch (Exception ex)
            {
                Logging.Info(strUsrApp, strIdTransaccion,string.Format("Error: {0}", ex.Message));
            }

            return strResponse;
        }

        public static string UpdateDataCustomerPClub(string strIdTransaccion, string strIpAplicacion, string strAplicacion, string strUsrApp, Entity.Transac.Service.Common.Client oCliente)
        {
            string strResponse = "0";
            try
            {
                POSTPAID_CAMBIODATOS.CambioDatosSIACWSService oService = new POSTPAID_CAMBIODATOS.CambioDatosSIACWSService();
                oService.Url = WebServiceConfiguration.CambioDatosWS.Url;
                oService.Credentials = System.Net.CredentialCache.DefaultCredentials;
                oService.Timeout = Int32.Parse(KEY.AppSettings("intTimeoutDataPrePostWS"));


                POSTPAID_CAMBIODATOS.actualizarDatosClientePCLUBRequest oRequest = new POSTPAID_CAMBIODATOS.actualizarDatosClientePCLUBRequest()
                {

                    auditRequest = new POSTPAID_CAMBIODATOS.auditRequestType() { idTransaccion = strIdTransaccion, ipAplicacion = strIpAplicacion, nombreAplicacion = strAplicacion, usuarioAplicacion = strUsrApp },
                    codCliente = oCliente.CUSTOMER_ID,
                    firstName = oCliente.NOMBRES,
                    lastName = oCliente.APELLIDOS,
                    numDoc = oCliente.DNI_RUC,
                    tipCliente = oCliente.TIPO_CLIENTE,
                    tipoDoc = oCliente.TIPO_DOC,
                    usuario = oCliente.USUARIO                    
                };

                POSTPAID_CAMBIODATOS.actualizarDatosClientePCLUBResponse oResponse = new POSTPAID_CAMBIODATOS.actualizarDatosClientePCLUBResponse();

                //  log.Info(string.Format("Metodo WS: consultarNumeros, URL:{0}", oService.Url));
                //  log.Info(string.Format("Parámetros Entrada -> idTransaccion:{0}, ipApp:{1}, usrApp:{2}, aplicacion:{3}, codCli:{4}, codCnt: {5},telefonoRef1: {6}, telefonoRef2: {7}, usuario: {8}", strIdTransaccion, strIpAplicacion, strUsrApp, strAplicacion, oCliente.CODIGO_CLIENTE.Trim(), oCliente.COD_CNT.Trim(), oCliente.TELEFONO_REFERENCIA_1, oCliente.TELEFONO_REFERENCIA_2, strUsrApp));

                oResponse = oService.actualizarDatosClientePCLUB(oRequest);

                if (oResponse.auditResponse.codigoRespuesta != "0")
                {
                    strResponse = "-1";
                }

            }
            catch (Exception ex)
            {
                Logging.Info(strUsrApp, strIdTransaccion,
                    string.Format("Error: {0}", ex.Message));
            }

            return strResponse;
        }

public static string GetDataCustomer2(string strIdSession, string strTransaction, string strIpAplicacion, string strAplicacion, string strUsrApp, string strcustomerid, string strtelefono, ref COMMON.Client Dataline, ref string Message)
        {
            string strCodRetorno = "";
            string strError = "";

            string strResponse = string.Empty;
            COMMON.Client objTemp = new COMMON.Client();

            try
            {

                POSTPAID_CAMBIODATOS.CambioDatosSIACWSService oService = new POSTPAID_CAMBIODATOS.CambioDatosSIACWSService();
                oService.Url = WebServiceConfiguration.CambioDatosWS.Url;
                oService.Credentials = System.Net.CredentialCache.DefaultCredentials;
                oService.Timeout = Int32.Parse(KEY.AppSettings("intTimeoutDataPrePostWS"));

                POSTPAID_CAMBIODATOS.obtenerDataClientRequest oRequest = new POSTPAID_CAMBIODATOS.obtenerDataClientRequest()
                {
                    auditRequest = new POSTPAID_CAMBIODATOS.auditRequestType() { idTransaccion = strTransaction, ipAplicacion = strIpAplicacion, nombreAplicacion = strAplicacion, usuarioAplicacion = strUsrApp },
                    custCode = strcustomerid,
                    numero = strtelefono
                };

                POSTPAID_CAMBIODATOS.obtenerDataClientResponse oResponse = new POSTPAID_CAMBIODATOS.obtenerDataClientResponse();

                var msg = string.Format(" Metodo WS: obtenerDataClient, URL:{0}", oService.Url);
                Claro.Web.Logging.Info("Session: " + strIdSession, "Transaction:  " + strTransaction, "Message" + msg);

                oResponse = oService.obtenerDataClient(oRequest);

                if (oResponse.auditResponse.codigoRespuesta == "0")
                {
                    objTemp.NOMBRES = oResponse.datosCliente.nombre;
                    objTemp.APELLIDOS = oResponse.datosCliente.apellidos;
                    objTemp.CUENTA = oResponse.datosCliente.cuenta;
                    objTemp.SEXO = oResponse.datosCliente.sexo;
                    objTemp.DOMICILIO = oResponse.datosCliente.direccion_fac;
                    objTemp.ZIPCODE = oResponse.datosCliente.cod_postal_fac;
                    objTemp.DEPARTAMENTO = oResponse.datosCliente.departamento_fac;
                    objTemp.DISTRITO = oResponse.datosCliente.distrito_fac;
                    objTemp.RAZON_SOCIAL = oResponse.datosCliente.razon_social;
                    objTemp.PROVINCIA = oResponse.datosCliente.provincia_fac;
                    objTemp.TIPO_DOC = oResponse.datosCliente.tip_doc_c;
                    objTemp.DNI_RUC = oResponse.datosCliente.ruc_dni;
                    objTemp.ASESOR = oResponse.datosCliente.asesor;
                    objTemp.CICLO_FACTURACION = oResponse.datosCliente.ciclo_fac;
                    objTemp.CONSULTOR = oResponse.datosCliente.consultor;
                    objTemp.MODALIDAD = oResponse.datosCliente.modalidad;
                    objTemp.SEGMENTO = oResponse.datosCliente.segmento;
                    objTemp.CREDIT_SCORE = oResponse.datosCliente.credit_score;
                    objTemp.ESTADO_CUENTA = oResponse.datosCliente.status_cuenta;
                    objTemp.FECHA_ACT = Convert.ToDate(oResponse.datosCliente.fecha_act);
                    objTemp.LIMITE_CREDITO = oResponse.datosCliente.limite_credito;
                    objTemp.TOTAL_CUENTAS = oResponse.datosCliente.num_cuentas;
                    objTemp.TOTAL_LINEAS = oResponse.datosCliente.num_lineas;
                    objTemp.RESPONSABLE_PAGO = oResponse.datosCliente.respon_pago;
                    objTemp.TIPO_CLIENTE = oResponse.datosCliente.tipo_cliente;
                    objTemp.REPRESENTANTE_LEGAL = oResponse.datosCliente.rep_legal;
                    objTemp.EMAIL = oResponse.datosCliente.email;
                    objTemp.TELEF_REFERENCIA = oResponse.datosCliente.telef_principal;
                    objTemp.CONTACTO_CLIENTE = oResponse.datosCliente.contacto_cliente;
                    objTemp.TIPO_DOC_RL = oResponse.datosCliente.tip_doc;
                    objTemp.NRO_DOC = oResponse.datosCliente.num_doc;
                    objTemp.NOMBRE_COMERCIAL = oResponse.datosCliente.nomb_comercial;
                    objTemp.FAX = oResponse.datosCliente.fax;
                    objTemp.CARGO = oResponse.datosCliente.cargo;
                    objTemp.CUSTOMER_ID = oResponse.datosCliente.customer_id;
                    objTemp.CONTACTO_CLIENTE = oResponse.datosCliente.contacto_cliente;
                    objTemp.TELEFONO_CONTACTO = oResponse.datosCliente.telef_contacto;
                    objTemp.CALLE_FAC = oResponse.datosCliente.direccion_fac;
                    objTemp.POSTAL_FAC = oResponse.datosCliente.cod_postal_fac;
                    objTemp.URBANIZACION_FAC = oResponse.datosCliente.urbanizacion_fac;
                    objTemp.DEPARTEMENTO_FAC = oResponse.datosCliente.departamento_fac;
                    objTemp.PROVINCIA_FAC = oResponse.datosCliente.provincia_fac;
                    objTemp.DISTRITO_FAC = oResponse.datosCliente.distrito_fac;
                    objTemp.CALLE_LEGAL = oResponse.datosCliente.direccion_leg;
                    objTemp.POSTAL_LEGAL = oResponse.datosCliente.cod_postal_leg;
                    objTemp.URBANIZACION_LEGAL = oResponse.datosCliente.urbanizacion_leg;
                    objTemp.DEPARTEMENTO_LEGAL = oResponse.datosCliente.departamento_leg;
                    objTemp.PROVINCIA_LEGAL = oResponse.datosCliente.provincia_leg;
                    objTemp.DISTRITO_LEGAL = oResponse.datosCliente.distrito_leg;
                    objTemp.PAIS_FAC = oResponse.datosCliente.pais_fac;
                    objTemp.PAIS_LEGAL = oResponse.datosCliente.pais_leg;
                    objTemp.REFERENCIA = oResponse.datosCliente.urbanizacion_fac;
                    objTemp.CONTRATO_ID = oResponse.datosCliente.co_id;
                    objTemp.LUGAR_NACIMIENTO_DES = oResponse.datosCliente.lug_nac;
                    objTemp.LUGAR_NACIMIENTO_ID = oResponse.datosCliente.nacionalidad;
                    objTemp.FECHA_NAC = oResponse.datosCliente.fecha_nac.Substring(8, 2) + "/" + oResponse.datosCliente.fecha_nac.Substring(5, 2) + "/" + oResponse.datosCliente.fecha_nac.Substring(0, 4);
                    objTemp.ESTADO_CIVIL = oResponse.datosCliente.estado_civil;
                    objTemp.ESTADO_CIVIL_ID = oResponse.datosCliente.estado_civil_id;
                    objTemp.NICHO = oResponse.datosCliente.nicho_id;
                    objTemp.FORMA_PAGO = oResponse.datosCliente.forma_pago;
                    objTemp.COD_TIPO_CLIENTE = oResponse.datosCliente.codigo_tipo_cliente;
                }

                Dataline = objTemp;
                Logging.Info(strIdSession, strTransaction, string.Format("strError: {0}", strError));

            }
            catch (Exception ex)
            {
                Logging.Info(strIdSession, strTransaction, string.Format("Error: {0}", ex.Message));
                Logging.Info(strIdSession, strTransaction, string.Format("Error: {0}", ex.InnerException));
            }
            return strCodRetorno;
        }
    }
}
