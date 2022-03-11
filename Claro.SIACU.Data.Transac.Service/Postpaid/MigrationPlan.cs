using Claro.Data;
using Sap = Claro.SAP.Transac.Service;
using System.Data;
using System.Collections.Generic;
using Claro.SIACU.Data.Transac.Service.Configuration;
using OPlanMigration = Claro.SIACU.Entity.Transac.Service.Postpaid;
using ValidationQueryBscsEai = Claro.SIACU.ProxyService.Transac.Service.SIACPost.ValidationQueryBscsEai;
using GestionAcuerdoWS = Claro.SIACU.ProxyService.Transac.Service.SIACPost.GestionAcuerdoWS;
using MigracionControlPostpagoWS = Claro.SIACU.ProxyService.Transac.Service.SIACPost.MigracionControlPostpagoWS;
using Constantes = Claro.SIACU.Transac.Service;
using System.Xml;
using System;
using Claro.SIACU.Entity.Transac.Service.Postpaid.GetProgramerMigration;

namespace Claro.SIACU.Data.Transac.Service.Postpaid
{
    public class MigrationPlan
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="strCustomerCode"></param>
        /// <returns></returns>
        public static OPlanMigration.Receipt GetDataInvoice(string strIdSession, string strTransaction, string strCustomerCode)
        {
            OPlanMigration.Receipt objReceipt;

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("K_CODIGOCLIENTE", DbType.String,24, ParameterDirection.Input, strCustomerCode),
                new DbParameter("K_ERRMSG", DbType.String, ParameterDirection.Output),
                new DbParameter("K_LISTA", DbType.Object, ParameterDirection.Output)
            };

            objReceipt = DbFactory.ExecuteReader<OPlanMigration.Receipt>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DBTO, DbCommandConfiguration.SIACU_TOLS_OBTENERDATOSDECUENTA, parameters);

            return objReceipt;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="strInvoiceNumber"></param>
        /// <returns></returns>
        public static OPlanMigration.DetailReceipt GetDetailInvoice(string strIdSession, string strTransaction, string strInvoiceNumber)
        {
            OPlanMigration.DetailReceipt objDetaileReceipt;

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("K_INVOICENUMBER", DbType.String,16, ParameterDirection.Input, strInvoiceNumber),
                new DbParameter("K_ERRMSG", DbType.String, ParameterDirection.Output),
                new DbParameter("K_LISTA", DbType.Object, ParameterDirection.Output),
            };

            objDetaileReceipt = DbFactory.ExecuteReader<OPlanMigration.DetailReceipt>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DBTO, DbCommandConfiguration.SIACU_TOLS_CONSULTARTEMPTAG1220, parameters);

            return objDetaileReceipt;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="strTelefono"></param>
        /// <param name="strContratoId"></param>
        /// <returns></returns>
        public static List<OPlanMigration.ConsumeLimit> GetConsumeLimit(string strIdSession, string strTransaction, string strTelefono, int strContratoId)
        {
            List<OPlanMigration.ConsumeLimit> lstConsumeLimit;

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("p_msisdn", DbType.String, ParameterDirection.Input, strTelefono),
                new DbParameter("p_co_id", DbType.String, ParameterDirection.Input,strContratoId),
                new DbParameter("p_cursor", DbType.Object, ParameterDirection.Output),
                new DbParameter("p_resultado", DbType.String, 255, ParameterDirection.Output),
                new DbParameter("p_msgerr", DbType.String, 255, ParameterDirection.Output)
            };

            lstConsumeLimit = DbFactory.ExecuteReader<List<OPlanMigration.ConsumeLimit>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_TIM100_CONSULTA_TOPE_CONSUMO, parameters);

            return lstConsumeLimit;
        }

        /// <summary>
        /// Consultar Cargo Fijo
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="iFlag"></param>
        /// <param name="strValor"></param>
        /// <param name="strError"></param>
        /// <param name="strDescError"></param>
        /// <returns></returns>
        public static string ConsultFixedCharge(string strIdSession, string strTransaction, int iFlag, string strValor, out string strError, out string strDescError)
        {
            string strCargoFijo;

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_FLAG", DbType.Int16, ParameterDirection.Input, iFlag),
                new DbParameter("P_VALOR", DbType.String, ParameterDirection.Input,strValor),
                new DbParameter("V_CF", DbType.String,255, ParameterDirection.Output),
                new DbParameter("V_ERROR", DbType.String, 255, ParameterDirection.Output),
                new DbParameter("V_DES_ERROR", DbType.String, 255, ParameterDirection.Output)
            };

            DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_TIM081_SP_CF_DN_NUM_OR_CO_ID, parameters);
            strCargoFijo = Convert.ToString(parameters[2].Value);
            strError = Convert.ToString(parameters[3].Value);
            strDescError = Convert.ToString(parameters[4].Value);
            return strCargoFijo;
        }
        /// <summary>
        /// ObtenerDetalleTransacciones
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="strMsisdn"></param>
        /// <param name="strFecDesde"></param>
        /// <param name="strFecHasta"></param>
        /// <param name="strEstado"></param>
        /// <param name="strAsesor"></param>
        /// <returns></returns>
        public static List<OPlanMigration.TransactionsDetail> GetTransactionDetail(string strIdSession, string strTransaction, string strApplicationName, string strIpApplication, string strUserName, string strMsisdn, string strFecDesde, string strFecHasta, string strEstado, string strAsesor, string strPuntoVenta, string strInteracion, string nroCuenta, out string strCodeError)
        {
            string idTransaccion;
            List<OPlanMigration.TransactionsDetail> objTransactionsDetail = new List<OPlanMigration.TransactionsDetail>();
            ValidationQueryBscsEai.ConsultaPosttServicioProgRequest objConsultaPosttServicioProgRequest = new ValidationQueryBscsEai.ConsultaPosttServicioProgRequest()
            {
                Msisdn = strMsisdn,
                Cuenta = nroCuenta,
                Asesor = strAsesor,
                Estado = strEstado,
                CodInteraccion = strInteracion,
                TipoTransaccion = string.Empty,//fuentes en siacpost siempre envia -1
                CadDac = strPuntoVenta,
                FechaDesde = strFecDesde,
                FechaHasta = strFecHasta,
                IdTransaccion = strTransaction
            };
            idTransaccion = objConsultaPosttServicioProgRequest.IdTransaccion;

            try
            {

            ValidationQueryBscsEai.ConsultaPosttServicioProgResponse objConsultaPosttServicioProgResponse = Claro.Web.Logging.ExecuteMethod<ValidationQueryBscsEai.ConsultaPosttServicioProgResponse>
                (strIdSession, strTransaction, Configuration.WebServiceConfiguration.ValidateQueryBscsEAi, () =>
                { return Configuration.WebServiceConfiguration.ValidateQueryBscsEAi.consultarPosttServiciosProg(objConsultaPosttServicioProgRequest); });
           

                if (objConsultaPosttServicioProgResponse.Resultado.ToString().Equals("0"))
                {
                    for (int i = 0; i < objConsultaPosttServicioProgResponse.ListaConsultaPosttServicioProg.Length; i++)
                    {
                        OPlanMigration.TransactionsDetail obj = new OPlanMigration.TransactionsDetail();
                        obj.SERVV_MSISDN = objConsultaPosttServicioProgResponse.ListaConsultaPosttServicioProg[i].MSISDN;
                        obj.SERVD_FECHA_REG = objConsultaPosttServicioProgResponse.ListaConsultaPosttServicioProg[i].FechaReg;
                        obj.SERV_FECHAPROG = objConsultaPosttServicioProgResponse.ListaConsultaPosttServicioProg[i].FechaProg;
                        obj.SERVI_COD = Convert.ToInt(objConsultaPosttServicioProgResponse.ListaConsultaPosttServicioProg[i].ServiCod);
                        obj.CO_ID = Convert.ToInt(objConsultaPosttServicioProgResponse.ListaConsultaPosttServicioProg[i].CoId);
                        obj.TIPO_TRANSACCION = objConsultaPosttServicioProgResponse.ListaConsultaPosttServicioProg[i].DescServi;
                        obj.SERVC_DESCOSER = objConsultaPosttServicioProgResponse.ListaConsultaPosttServicioProg[i].descCoSer;
                        obj.SERVC_TIPOREG = objConsultaPosttServicioProgResponse.ListaConsultaPosttServicioProg[i].tipoReg;
                        obj.SERVC_TIPOSER = objConsultaPosttServicioProgResponse.ListaConsultaPosttServicioProg[i].tipoServ;


                        obj.SERVV_USUARIO_SISTEMA = objConsultaPosttServicioProgResponse.ListaConsultaPosttServicioProg[i].UsuarioSistema;
                        obj.SERVC_ESTADO = objConsultaPosttServicioProgResponse.ListaConsultaPosttServicioProg[i].Estado;
                        obj.TIPO_ESTADO = objConsultaPosttServicioProgResponse.ListaConsultaPosttServicioProg[i].DescEstado;
                        obj.SERVC_NROCUENTA = objConsultaPosttServicioProgResponse.ListaConsultaPosttServicioProg[i].NroCuenta;
                        obj.SERVC_CODIGO_INTERACCION = objConsultaPosttServicioProgResponse.ListaConsultaPosttServicioProg[i].CodigoInteraccion;

                        obj.SERVC_PUNTOVENTA = objConsultaPosttServicioProgResponse.ListaConsultaPosttServicioProg[i].PuntoVenta;
                        obj.SERVV_MEN_ERROR = objConsultaPosttServicioProgResponse.ListaConsultaPosttServicioProg[i].MenError;
                        obj.SERVC_CODIGO_PROGRAMACION = objConsultaPosttServicioProgResponse.ListaConsultaPosttServicioProg[i].IdBatch;


                        if (!string.IsNullOrEmpty(objConsultaPosttServicioProgResponse.ListaConsultaPosttServicioProg[i].XmlEntrada))
                        {
                            try
                            {
                                System.Xml.XmlDocument docRespuesta = new XmlDocument();
                                string strXMLResponse = objConsultaPosttServicioProgResponse.ListaConsultaPosttServicioProg[i].XmlEntrada;
                                int primero;
                                int ultimo;
                                primero = strXMLResponse.IndexOf("<codigoProducto>");
                                ultimo = strXMLResponse.LastIndexOf("</codigoProducto>");
                                strXMLResponse = strXMLResponse.Substring(primero, ultimo - primero);
                                strXMLResponse = strXMLResponse + "</codigoProducto>";

                                docRespuesta.LoadXml(strXMLResponse);
                                XmlElement element;
                                element = docRespuesta.DocumentElement;
                                if (!string.IsNullOrEmpty(element.InnerText))
                                { obj.CODIGO_PRODUCTO = Convert.ToInt(element.InnerText); }
                                else
                                { obj.CODIGO_PRODUCTO = 0; }
                            }
                            catch (Exception ex)
                            {
                                obj.COD_PRODUCTO = 0;
                            }
                        }
                        objTransactionsDetail.Add(obj);
                    }
                    strCodeError = objConsultaPosttServicioProgResponse.Descripcion.ToString();
                }
                else
                {
                    strCodeError = objConsultaPosttServicioProgResponse.Descripcion.ToString();
                }

                return objTransactionsDetail;
            }
            catch (Exception ex)
            {
                strCodeError = "Error del Servicio : " + ex.Message;
                return objTransactionsDetail;
            }
        }
        /// <summary>
        /// PagoDeuda
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="strFlag"></param>
        /// <param name="strValor"></param>
        /// <returns></returns>
        public static OPlanMigration.DebtPayment GetDebtPayment(string strIdSession, string strTransaction, string strFlag, string strValor)
        {
            OPlanMigration.DebtPayment lstDebtPayment;
            ValidationQueryBscsEai.ValidacionPagoRequest objValidacionPagoRequest = new ValidationQueryBscsEai.ValidacionPagoRequest()
            {
                IdTransaccion = strFlag,
                Flag = strFlag,
                Valor = strValor
            };
            ValidationQueryBscsEai.ValidacionPagoResponse objValidacionPagoResponse = Claro.Web.Logging.ExecuteMethod<ValidationQueryBscsEai.ValidacionPagoResponse>
            (strIdSession, strTransaction, Configuration.WebServiceConfiguration.ValidateQueryBscsEAi, () =>
            { return Configuration.WebServiceConfiguration.ValidateQueryBscsEAi.validaPago(objValidacionPagoRequest); });

            return lstDebtPayment = new OPlanMigration.DebtPayment()
            {
                CODERROR = objValidacionPagoResponse.DatosValidacionPago.CodigoError,
                DESCRIPCIONERROR = objValidacionPagoResponse.DatosValidacionPago.DescripcionError,
                DEUDA = objValidacionPagoResponse.DatosValidacionPago.Deuda,
                RESULTADO = objValidacionPagoResponse.DatosValidacionPago.Resultado
            };
        }
        /// <summary>
        /// ValidaBloqueo
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="strCoId"></param>
        /// <returns></returns>
        public static OPlanMigration.Suspension ValidateLockSuspension(string strIdSession, string strTransaction, string strCoId)
        {
            OPlanMigration.Suspension objSuspension;
            ValidationQueryBscsEai.ValidacionBloqueoSuspCoIdRequest objValidacionBloqueoSuspCoIdRequest = new ValidationQueryBscsEai.ValidacionBloqueoSuspCoIdRequest()
            {
                IdTransaccion = strTransaction,
                CoID = strCoId
            };

            ValidationQueryBscsEai.ValidacionBloqueoSuspCoIdResponse objValidacionBloqueoSuspCoIdResponse = Claro.Web.Logging.ExecuteMethod<ValidationQueryBscsEai.ValidacionBloqueoSuspCoIdResponse>
                (strIdSession, strTransaction, Configuration.WebServiceConfiguration.ValidateQueryBscsEAi, () =>
                { return Configuration.WebServiceConfiguration.ValidateQueryBscsEAi.validaBloqueadoSuspCoid(objValidacionBloqueoSuspCoIdRequest); });

            return objSuspension = new OPlanMigration.Suspension()
            {
                DESCRIPCION = objValidacionBloqueoSuspCoIdResponse.DatosValidacionBloqueoSuspCoId.MensajeValidacion,
                CODIGO = objValidacionBloqueoSuspCoIdResponse.DatosValidacionBloqueoSuspCoId.ResultadoValidacion
            };
        }
        /// <summary>
        /// ObtieneBloqueo
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="strCoId"></param>
        /// <returns></returns>
        public static List<OPlanMigration.Block> GetBlock(string strIdSession, string strTransaction, string strCoId)
        {
            List<OPlanMigration.Block> lstBlock = new List<OPlanMigration.Block>();

            ValidationQueryBscsEai.ObtieneBloqueadoSuspCoIdRequest objObtieneBloqueadoSuspCoIdRequestt = new ValidationQueryBscsEai.ObtieneBloqueadoSuspCoIdRequest()
            {
                IdTransaccion = strTransaction,
                CoID = strCoId
            };

            ValidationQueryBscsEai.ObtieneBloqueadoSuspCoIdResponse objObtieneBloqueadoSuspCoIdResponse = Claro.Web.Logging.ExecuteMethod<ValidationQueryBscsEai.ObtieneBloqueadoSuspCoIdResponse>
                (strIdSession, strTransaction, Configuration.WebServiceConfiguration.ValidateQueryBscsEAi, () =>
                { return Configuration.WebServiceConfiguration.ValidateQueryBscsEAi.obtieneBloqueadoSuspCoId(objObtieneBloqueadoSuspCoIdRequestt); });

            for (int i = 0; i < objObtieneBloqueadoSuspCoIdResponse.DatosObtencionBloqueoSuspCoId.ListaObtencionBloqueoSusp.Length; i++)
            {
                OPlanMigration.Block obj = new OPlanMigration.Block();
                obj.DESCRIPCION = objObtieneBloqueadoSuspCoIdResponse.DatosObtencionBloqueoSuspCoId.ListaObtencionBloqueoSusp[i].TicklerCode;
                obj.FECHA = objObtieneBloqueadoSuspCoIdResponse.DatosObtencionBloqueoSuspCoId.ListaObtencionBloqueoSusp[i].Fecha;
                obj.DESCRIPCION_USUARIO = objObtieneBloqueadoSuspCoIdResponse.DatosObtencionBloqueoSuspCoId.ListaObtencionBloqueoSusp[i].Usuario;

                lstBlock.Add(obj);
            };
            return lstBlock;
        }
        /// <summary>
        /// Reintegro de Equipo
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="strIpAplicacion"></param>
        /// <param name="strAplicacion"></param>
        /// <param name="strUserAplica"></param>
        /// <param name="strMsisdn"></param>
        /// <param name="strCoId"></param>
        /// <param name="strFechaTransaccion"></param>
        /// <param name="strCargoFijoNuevo"></param>
        /// <param name="strMotivoApadece"></param>
        /// <param name="strFlagEquipo"></param>
        /// <param name="strOpcional1"></param>
        /// <param name="strOpcional2"></param>
        /// <param name="strValor1"></param>
        /// <param name="strValor2"></param>
        /// <param name="strCodigoRespuesta"></param>
        /// <param name="strMensajeRespuesta"></param>
        /// <param name="blRespuesta"></param>
        /// <returns></returns>
        public static OPlanMigration.Agreement ReinstatementEquipment(string strIdSession, string strTransaction, string strIpAplicacion, string strAplicacion, string strUserAplica, string strMsisdn, string strCoId, string strFechaTransaccion, string strCargoFijoNuevo, string strMotivoApadece, string strFlagEquipo,
                                                                       out string strCodigoRespuesta, out string strMensajeRespuesta, out bool blRespuesta)
        {
            OPlanMigration.Agreement objAgreement = null;
            blRespuesta = true;
            strCodigoRespuesta = "";
            strMensajeRespuesta = "";
            try
            {

                GestionAcuerdoWS.obtenerReintegroEquipoRequest objobtenerReintegroEquipoRequest = new GestionAcuerdoWS.obtenerReintegroEquipoRequest()
                {
                    auditRequest = new GestionAcuerdoWS.auditRequestType()
                    {
                        idTransaccion = strTransaction,
                        ipAplicacion = strIpAplicacion,
                        nombreAplicacion = strAplicacion,
                        usuarioAplicacion = strUserAplica
                    },
                    msisdn = strMsisdn,
                    coId = strCoId,
                    fechaTransaccion = strFechaTransaccion,
                    cargoFijoNuevo = strCargoFijoNuevo,
                    motivoApadece = strMotivoApadece,
                    flagEquipo = strFlagEquipo,
                    listaRequestOpcional = new GestionAcuerdoWS.parametrosTypeObjetoOpcional[3]{
                 new GestionAcuerdoWS.parametrosTypeObjetoOpcional() { 
                        campo = string.Empty,
                        valor =string.Empty
                    },
                 new GestionAcuerdoWS.parametrosTypeObjetoOpcional()
                    {
                        campo = string.Empty,
                        valor = string.Empty
                    },
                new GestionAcuerdoWS.parametrosTypeObjetoOpcional()
               }
                };

                GestionAcuerdoWS.obtenerReintegroEquipoResponse objobtenerReintegroEquipoResponse = Claro.Web.Logging.ExecuteMethod<GestionAcuerdoWS.obtenerReintegroEquipoResponse>
                     (strIdSession, strTransaction, Configuration.WebServiceConfiguration.GestionAcuerdoWS, () =>
                     { return Configuration.WebServiceConfiguration.GestionAcuerdoWS.obtenerReintegroEquipo(objobtenerReintegroEquipoRequest); });
                strCodigoRespuesta = objobtenerReintegroEquipoResponse.auditResponse.codigoRespuesta;
                strMensajeRespuesta = objobtenerReintegroEquipoResponse.auditResponse.mensajeRespuesta;

                if (strCodigoRespuesta == Constantes.Constants.strCero)
                {
                    objAgreement = new OPlanMigration.Agreement()
                    {
                        ACUERDO_CADUCADO = objobtenerReintegroEquipoResponse.acuerdoCaducado,
                        ACUERDO_ESTADO = objobtenerReintegroEquipoResponse.acuerdoEstado,
                        ACUERDO_FECHA_FIN = objobtenerReintegroEquipoResponse.acuerdoFechaFin,
                        ACUERDO_FECHA_INICIO = objobtenerReintegroEquipoResponse.acuerdoFechaInicio,
                        ACUERDO_ID = objobtenerReintegroEquipoResponse.acuerdoId,
                        ACUERDO_MONTO_APADECE_TOTAL = objobtenerReintegroEquipoResponse.acuerdoMontoApacedeTotal,
                        ACUERDO_ORIGEN = objobtenerReintegroEquipoResponse.acuerdoOrigen,
                        ACUERDO_VIGENCIA_MES = objobtenerReintegroEquipoResponse.acuerdoVigenciaMeses,
                        CARGO_FIJO_DIARIO = objobtenerReintegroEquipoResponse.cargoFijoDiario,
                        CODIGO_PLAZO_ACUERDO = objobtenerReintegroEquipoResponse.codPlazoAcuerdo,
                        CO_ID = objobtenerReintegroEquipoResponse.coId,
                        CUSTOMER_ID = objobtenerReintegroEquipoResponse.customerId,
                        DESCRIPCION_PLAZO_ACUERDO = objobtenerReintegroEquipoResponse.descPlazoAcuerdo,
                        DESCRIPCION_ESTADO_ACUERDO = objobtenerReintegroEquipoResponse.descripcionEstadoAcuerdo,
                        DIAS_BLOQUEO = objobtenerReintegroEquipoResponse.diasBloqueo,
                        DIAS_PENDIENTES = objobtenerReintegroEquipoResponse.diasPendientes,
                        DIAS_VIGENCIA = objobtenerReintegroEquipoResponse.diasVigencia,
                        FIN_VIGENCIA_REAL = objobtenerReintegroEquipoResponse.finVigenciaReal,
                        MESES_ANTIGUEDAD = objobtenerReintegroEquipoResponse.mesesAntiguedad,
                        MESES_PENDIENTES = objobtenerReintegroEquipoResponse.mesesPendientes,
                        MONTO_APADECE = objobtenerReintegroEquipoResponse.montoApadece,
                        PRECIO_LISTA = objobtenerReintegroEquipoResponse.precioLista,
                        PRECIO_VENTA = objobtenerReintegroEquipoResponse.precioVenta,
                        PENALIDAD = objobtenerReintegroEquipoResponse.listaResponseOpcional[0].valor
                    };
                }
            }
            catch (Exception)
            {
                blRespuesta = false;
                objAgreement=null;
            }
            return objAgreement;

        }
        /// <summary>
        /// Consultar cargo Fijo PVU
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="boolRespuesta"></param>
        /// <param name="strCodigoError"></param>
        /// <param name="strMensajeErro"></param>
        /// <returns></returns>
        public static List<OPlanMigration.FixedCharge> ConsultFixedChargePVU(string strIdSession, string strTransaction,string strTelefono, out bool boolRespuesta, out string strCodigoError, out string strMensajeErro)
        {
            List<OPlanMigration.FixedCharge> lstFixedCharge = null;
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_TELEFONO", DbType.String,16, ParameterDirection.Input, strTelefono),
                new DbParameter("C_CONTRATO", DbType.Object, ParameterDirection.Output),
                new DbParameter("ouCOD_ERR", DbType.String, ParameterDirection.Output),
                new DbParameter("ouMSG_ERR", DbType.String, ParameterDirection.Output),
            };

            lstFixedCharge = DbFactory.ExecuteReader<List<OPlanMigration.FixedCharge>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_PVU, DbCommandConfiguration.SIACU_SP_CON_CONTRATO, parameters);

            strCodigoError = Convert.ToString(parameters[2].Value);
            strMensajeErro = Convert.ToString(parameters[3].Value);
            boolRespuesta = strMensajeErro==Constantes.Constants.CriterioMensajeOK ? true : false;
            return lstFixedCharge;
        }
       /// <summary>
       /// obtener cargo fijo SAP
       /// </summary>
       /// <param name="strIdSession"></param>
       /// <param name="strTransaction"></param>
       /// <param name="strTelefono"></param>
       /// <param name="boolRespuesta"></param>
       /// <param name="strCodigoError"></param>
       /// <param name="strMensajeErro"></param>
       /// <returns></returns>
        public static OPlanMigration.FixedChargeSAP ConsultFixedChargeSAP(string strIdSession, string strTransaction, string strTelefono,string strContrato)
        {
            OPlanMigration.FixedChargeSAP objFixedCharge = null;

            DataSet dsData = Sap.SAPMethods.GetFixedCharge(strTelefono, strContrato);

            if (dsData != null)
            {
                objFixedCharge = new OPlanMigration.FixedChargeSAP()
                {
                    CARGO_FIJO = Convert.ToString(dsData.Tables[0].Rows[0]["CARGO_FIJO"]),
                    NUMERO_CONTRATO = Convert.ToString(dsData.Tables[0].Rows[0]["NUMERO_CONTRATO"]),
                    PLAN_TARIFARIO = Convert.ToString(dsData.Tables[0].Rows[0]["PLAN_TARIFARIO"]),
                    DES_PLAN_TARIF = Convert.ToString(dsData.Tables[0].Rows[0]["DES_PLAN_TARIF"]),
                    FECHA_CONTATO = Convert.ToString(dsData.Tables[0].Rows[0]["FECHA_CONTATO"])
                };
            }
            return objFixedCharge;
        }
        /// <summary>
        /// obtener nuevos planes
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="strCodTipoProducto"></param>
        /// <param name="strCategoriaProducto"></param>
        /// <param name="strNombrePlan"></param>
        /// <returns></returns>
        public static List<OPlanMigration.NewPlan> GetNewPlans(string strIdSession, string strTransaction, string strCodTipoProducto, string strCategoriaProducto, string strNombrePlan)
        {
            List<OPlanMigration.NewPlan> lstNewPlans = new  List<OPlanMigration.NewPlan>();
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_TIPO_PRODUCTO", DbType.String,255, ParameterDirection.Input, strCodTipoProducto),
                new DbParameter("P_CAT_PROD", DbType.String,255, ParameterDirection.Input, strCategoriaProducto),
                new DbParameter("P_NOMBRE_PLAN", DbType.String,255, ParameterDirection.Input, strNombrePlan),
                new DbParameter("P_CURSOR", DbType.Object, ParameterDirection.Output),
            };

         return   lstNewPlans = DbFactory.ExecuteReader<List<OPlanMigration.NewPlan>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_SP_BUSCA_PLAN_X_CODPROD, parameters);

        }
        /// <summary>
        /// Planes tarifarios
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="strProcCodigo"></param>
        /// <param name="strPrdcCodigo"></param>
        /// <param name="strModalidad_venta"></param>
        /// <param name="strPlncFamilia"></param>
        /// <returns></returns>
        public static List<OPlanMigration.RatePlan> GetRatePlan(string strIdSession,string strTransaction, string strProcCodigo, string strPrdcCodigo, string strModalidad_venta, string strPlncFamilia)
        {
            List<OPlanMigration.RatePlan> lstRatePlan = null;
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_CURSOR", DbType.Object, ParameterDirection.Output),
                new DbParameter("P_TPROC_CODIGO", DbType.String,255, ParameterDirection.Input, strProcCodigo),
                new DbParameter("P_PRDC_CODIGO", DbType.String,255, ParameterDirection.Input, strPrdcCodigo),
                new DbParameter("P_MODALIDAD_VENTA", DbType.String,255, ParameterDirection.Input, strModalidad_venta),
                new DbParameter("P_PLNC_FAMILIA", DbType.String,255, ParameterDirection.Input, strPlncFamilia)
            };

            return lstRatePlan = DbFactory.ExecuteReader<List<OPlanMigration.RatePlan>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_PVU, DbCommandConfiguration.SIACU_SP_SIACS_PLAN_TARIFARIO, parameters);

        }
        /// <summary>
        /// Obtienen plan tarifario de BSCS
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="iFlag"></param>
        /// <param name="strValor"></param>
        /// <param name="strError"></param>
        /// <param name="strDescError"></param>
        /// <returns></returns>
        public static string GetRatePlanBSCS(string strIdSession,string strTransaction,int iTMCODE)
        {
            string strDesPlanOrigen;

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("v_plan_tarifario", DbType.String,30, ParameterDirection.ReturnValue),
                new DbParameter("p_tmcode", DbType.Int64, ParameterDirection.Input, iTMCODE)
            };

            DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_TFUN014_PLAN_TARIFARIO, parameters);
            strDesPlanOrigen = Convert.ToString(parameters[0].Value);
            return strDesPlanOrigen;
        }
        public static List<OPlanMigration.TopConsumption> SearchMaintenancePlan(string strIdSession, string strTransaction, string strCodigoProduct)
        {
            List<OPlanMigration.TopConsumption> lstConsumeLimit;

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_cod_prod", DbType.String,10, ParameterDirection.Input, strCodigoProduct),
                new DbParameter("P_CURSOR", DbType.Object, ParameterDirection.Output)
            };

            lstConsumeLimit = DbFactory.ExecuteReader<List<OPlanMigration.TopConsumption>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_SP_BUSCA_SERV_PLAN_MANT, parameters);

            return lstConsumeLimit;
        }

        public static List<OPlanMigration.MaintenancePlan> GetPlansServices(string strIdSession, string strTransaction, int iTMCODE)
        {

            List<OPlanMigration.MaintenancePlan> lstMaintenancePlan;

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_TMCODE", DbType.Int32,30, ParameterDirection.Input,iTMCODE),
                new DbParameter("P_CURSOR", DbType.Object, ParameterDirection.Output)
            };

            lstMaintenancePlan = DbFactory.ExecuteReader<List<OPlanMigration.MaintenancePlan>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_SP_SERVICIOS_X_PLAN, parameters);
            return lstMaintenancePlan;
        }
        public static List<OPlanMigration.ListItem> GetValidateBagShare(string strIdSession, string strTransaction, 
            string strCO_ID, out string strCustCode,out string strRPT, out int intResultado, out string strMSGERR, out string strNroCuenta)
         {

             List<OPlanMigration.ListItem> lstListItem=new List<OPlanMigration.ListItem>();
             OPlanMigration.ListItem objListItem;
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_CO_ID", DbType.String,256, ParameterDirection.Input,strCO_ID),
                new DbParameter("P_RPT", DbType.String,256, ParameterDirection.Output),
                new DbParameter("P_NRO_CTA", DbType.String,256, ParameterDirection.Output),
                new DbParameter("P_CURSOR_PLAN", DbType.Object, ParameterDirection.Output),
                new DbParameter("P_CUSTCODE", DbType.String,256, ParameterDirection.Output),
                new DbParameter("P_RESULTADO", DbType.Int32, ParameterDirection.Output),
                new DbParameter("P_MSGERR", DbType.String,256, ParameterDirection.Output)
            };
            
            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_SP_VALIDA_BOLSA_COMP, parameters, (IDataReader reader) =>
            {
                while (reader.Read())
                {
                    objListItem = new OPlanMigration.ListItem()
                    { 
                    Code =Convert.ToString(reader["COD_PROD"])
                    };
                    lstListItem.Add(objListItem);
                }
            });
            strRPT = Convert.ToString(parameters[1].Value);
            strNroCuenta = Convert.ToString(parameters[2].Value);
            strCustCode = Convert.ToString(parameters[4].Value);
            intResultado = Convert.ToInt(Convert.ToString(parameters[5].Value));
            strMSGERR = Convert.ToString(parameters[6].Value);

            return lstListItem;
            
        }

        public static bool GetValidateProgByProduct(string strIdSession, string strTransaction,string strNROCUENTA, string strMSISDN, string strP_array_producto, 
			string strSERVICIO,string strESTADO, out string strERRORCODE, out string strERRORMSG)
        {

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("NROCUENTA", DbType.String,256, ParameterDirection.Input,strNROCUENTA),
                new DbParameter("MSISDN", DbType.String,256, ParameterDirection.Input,strMSISDN),
                new DbParameter("p_array_producto", DbType.String,256, ParameterDirection.Input,strP_array_producto),
                new DbParameter("SERVICIO", DbType.String,256, ParameterDirection.Input,strSERVICIO),
                new DbParameter("ESTADO", DbType.String,256, ParameterDirection.Input,strESTADO),
                new DbParameter("P_ERRORCODE", DbType.Int32, ParameterDirection.Output),
                new DbParameter("P_ERRORMSG", DbType.String,256, ParameterDirection.Output)
            };

            DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_P_VAL_PROGXPRODUCTO, parameters);
            strERRORCODE = Convert.ToString(parameters[5].Value);
            strERRORMSG = Convert.ToString(parameters[6].Value);
            return true;
        }

        public static string RegisterPlanService(string strIdSession, string strTransaction, string ID_INTERACCION, string COD_SERVICIO, string DES_SERVICIO,
           string MOTIVO_EXCLUYE, string CARGO_FIJO, string PERIODO, string USUARIO,out string message)
        {

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_ID_INTERACCION", DbType.String,256, ParameterDirection.Input,ID_INTERACCION),
                new DbParameter("P_COD_SERVICIO", DbType.String,256, ParameterDirection.Input,COD_SERVICIO),
                new DbParameter("P_DES_SERVICIO", DbType.String,256, ParameterDirection.Input,DES_SERVICIO),
                new DbParameter("P_MOTIVO_EXCLUYE", DbType.String,256, ParameterDirection.Input,MOTIVO_EXCLUYE),
                new DbParameter("P_CARGO_FIJO", DbType.String,256, ParameterDirection.Input,CARGO_FIJO),
                new DbParameter("P_PERIODO", DbType.String,256, ParameterDirection.Input,PERIODO),
                new DbParameter("P_USUARIO",DbType.String,256, ParameterDirection.Input,USUARIO),
                new DbParameter("P_FLAG_INSERT", DbType.String,256, ParameterDirection.Output),
                new DbParameter("P_MSG_TEXT", DbType.String,256, ParameterDirection.Output)
            };

            DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_CLARIFY, DbCommandConfiguration.SIACU_SP_INSERTAR_SERVICIOSPLAN, parameters);

            string Flag = Convert.ToString(parameters[7].Value);
            message = Convert.ToString(parameters[8].Value);
            return Flag;
        }

        public static bool ProgramerMigrationControlPostPago(ProgramerMigrationRequest objProgramerMigration, out string p_codigoRespuesta, out string p_mensajeRespuesta, out int intResultado)
        {
            bool result=false;
            string codigoRespuesta = "";
            intResultado = 0;
            p_codigoRespuesta = string.Empty;
            p_mensajeRespuesta = string.Empty;
            string p_codigoRespuesta2 = string.Empty;
            string p_mensajeRespuesta2 = string.Empty;
            try
            {
                MigracionControlPostpagoWS.audiTypeRequest oATR = new MigracionControlPostpagoWS.audiTypeRequest()
                {
                    idTransaccion = objProgramerMigration.Audit.Transaction,
                    ipAplicacion = objProgramerMigration.Audit.IPAddress,
                    nombreAplicacion = objProgramerMigration.Audit.ApplicationName,
                    usuarioAplicacion = objProgramerMigration.Audit.UserName
                };

                int NumRegContra = objProgramerMigration.listaContratos.Count;

                MigracionControlPostpagoWS.contratoBeanType[] olistaContratos = new MigracionControlPostpagoWS.contratoBeanType[NumRegContra];
                for (int i = 0; i < NumRegContra; i++)
                {
                    olistaContratos[i] = new MigracionControlPostpagoWS.contratoBeanType() ;
                    olistaContratos[i].planTarifario = objProgramerMigration.listaContratos[i].planTarifario;
                    olistaContratos[i].estadoUmbral = objProgramerMigration.listaContratos[i].estadoUmbral;
                    MigracionControlPostpagoWS.actualizacionContratoBeanType oActualizacionContrato = new MigracionControlPostpagoWS.actualizacionContratoBeanType();
                    oActualizacionContrato.razon = objProgramerMigration.listaContratos[i].actualizacionContrato.razon;
                    olistaContratos[i].actualizacionContrato = oActualizacionContrato;

                    int NumRegInfoContra = objProgramerMigration.listaContratos[i].informacionContrato.Count;
                    MigracionControlPostpagoWS.campoBean[] oInformacionContrato = new MigracionControlPostpagoWS.campoBean[NumRegInfoContra];
                    for (int j = 0; j < NumRegInfoContra; j++)
                    {
                        oInformacionContrato[j] = new MigracionControlPostpagoWS.campoBean();
                        oInformacionContrato[j].indice = objProgramerMigration.listaContratos[i].informacionContrato[j].indice;
                        oInformacionContrato[j].tipo = objProgramerMigration.listaContratos[i].informacionContrato[j].tipo;
                        oInformacionContrato[j].valor = objProgramerMigration.listaContratos[i].informacionContrato[j].valor;
                    }
                    olistaContratos[i].informacionContrato = oInformacionContrato;

                    int NumRegDispos = objProgramerMigration.listaContratos[i].listaDispositivos.Count;
                    MigracionControlPostpagoWS.dispositivoBeanType[] oListaDispositivos = new MigracionControlPostpagoWS.dispositivoBeanType[NumRegDispos];
                    for (int j = 0; j < NumRegDispos; j++)
                    {
                        oListaDispositivos[j] = new MigracionControlPostpagoWS.dispositivoBeanType();
                        oListaDispositivos[j].idDispositivo = objProgramerMigration.listaContratos[i].listaDispositivos[j].idDispositivo;
                        oListaDispositivos[j].tipoDispositivo = objProgramerMigration.listaContratos[i].listaDispositivos[j].tipoDispositivo;
                    }
                    olistaContratos[i].listaDispositivos = oListaDispositivos;

                }
                DateTime fechaProgramacion = Convert.ToDate(objProgramerMigration.fechaProgramacion.ToString("yyyy-MM-dd"));
                DateTime fechaProgramacionTope = Convert.ToDate(objProgramerMigration.fechaProgramacionTope.ToString("yyyy-MM-dd"));


                codigoRespuesta = Claro.Web.Logging.ExecuteMethod(objProgramerMigration.Audit.Session,objProgramerMigration.Audit.Transaction,
                    Configuration.WebServiceConfiguration.MigracionControlPostpagoService,()=>
                    {return Configuration.WebServiceConfiguration.MigracionControlPostpagoService.programarMigracion(oATR, objProgramerMigration.coId, objProgramerMigration.msisdn, fechaProgramacion, objProgramerMigration.customerId, objProgramerMigration.codigoProducto,
                    olistaContratos, objProgramerMigration.tipoServicio, objProgramerMigration.flagOccApadece, objProgramerMigration.flagNdPcs, objProgramerMigration.ndArea, objProgramerMigration.ndMotivo, objProgramerMigration.ndSubmotivo,
                    objProgramerMigration.cacDac, objProgramerMigration.cicloFacturacion, objProgramerMigration.idTipoCliente, objProgramerMigration.numeroDocumento, objProgramerMigration.clienteCuenta, objProgramerMigration.montoPCS,
                    objProgramerMigration.montoFidelizacion, objProgramerMigration.idInteraccion, objProgramerMigration.tipoPostpago, objProgramerMigration.montoApadece, objProgramerMigration.flagValidaApadece,
                    objProgramerMigration.flagAplicaApadece, objProgramerMigration.flagLimiteCredito, objProgramerMigration.topeConsumo, objProgramerMigration.nroCuenta, objProgramerMigration.asesor, fechaProgramacionTope,
                    objProgramerMigration.tipoTope, objProgramerMigration.descripcionTipoTope, objProgramerMigration.tipoRegistroTope, objProgramerMigration.topeControlConsumo, objProgramerMigration.tipoClarify, objProgramerMigration.cuentaPadre,
                    objProgramerMigration.tipoMigracion, objProgramerMigration.nivelCuenta, objProgramerMigration.tipoCuenta, objProgramerMigration.imsi, out p_codigoRespuesta2, out p_mensajeRespuesta2);
                    });

                p_codigoRespuesta = p_codigoRespuesta2;
                p_mensajeRespuesta = p_mensajeRespuesta2;
                intResultado = 1;
                result= true;
            }
            catch (Exception ex)
            {
                result= false;
            }
            return result;
        }

        public static bool GetDataByContract(string strIdSession, string strTransaction, int pCustomerId, int pCoId,out List<OPlanMigration.DataByContract> lstDataContract,
           out List<OPlanMigration.DataByContractInfo> lstDataContractInfo, out string ErrorCod, out string ErrorDes)
        {
            List<OPlanMigration.DataByContract> lstDataByContract = new List<OPlanMigration.DataByContract>();
            OPlanMigration.DataByContract objDataByContract;
            List<OPlanMigration.DataByContractInfo> lstDataByContractInfo = new List<OPlanMigration.DataByContractInfo>();
            OPlanMigration.DataByContractInfo objDataByContractInfo;
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_CUSTOMER_ID", DbType.Int64, ParameterDirection.Input,pCustomerId),
                new DbParameter("P_CO_ID", DbType.Int64, ParameterDirection.Input,pCoId),
                new DbParameter("P_DATOS_CONTR", DbType.Object, ParameterDirection.Output),
                new DbParameter("P_DATOS_CONTR_INFOS", DbType.Object, ParameterDirection.Output),
                new DbParameter("P_ERROR_COD", DbType.Int32, ParameterDirection.Output),
                new DbParameter("P_ERROR_DES", DbType.String,256, ParameterDirection.Output)
            };

            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_sp_datos_x_contr, parameters, (IDataReader reader) =>
            {
                while (reader.Read())
                {
                    objDataByContract = new OPlanMigration.DataByContract()
                    {
                        customer_id = Convert.ToString(reader["customer_id"]),
                        tmcode = Convert.ToString(reader["tmcode"]),
                        sub_mercado = Convert.ToString(reader["sub_mercado"]),
                        mercado = Convert.ToString(reader["mercado"]),
                        red = Convert.ToString(reader["red"]),
                        estado_umbral = Convert.ToString(reader["estado_umbral"]),
                        cantidad_umbral = Convert.ToString(reader["cantidad_umbral"]),
                        ARCH_LLAMADAS = Convert.ToString(reader["ARCH_LLAMADAS"]),
                        co_id = Convert.ToString(reader["co_id"]),
                        estado = Convert.ToString(reader["estado"]),
                        razon = Convert.ToString(reader["razon"]),
                        rs_desc = Convert.ToString(reader["rs_desc"]),
                        sm_id = Convert.ToString(reader["sm_id"]),
                    };
                    lstDataByContract.Add(objDataByContract);
                }
                if (reader.NextResult())
                {
                    while (reader.Read())
                    {
                        objDataByContractInfo = new OPlanMigration.DataByContractInfo()
                        {
                            customer_id = Convert.ToString(reader["customer_id"]),
                            check01 = Convert.ToString(reader["check01"]),
                            check08 = Convert.ToString(reader["check08"]),
                            check15 = Convert.ToString(reader["check15"]),
                            combo01 = Convert.ToString(reader["combo01"]),
                            combo08 = Convert.ToString(reader["combo08"]),
                            combo15 = Convert.ToString(reader["combo15"]),
                            text01 = Convert.ToString(reader["text01"]),
                            text09 = Convert.ToString(reader["text09"]),
                            text17 = Convert.ToString(reader["text17"]),
                            text25 = Convert.ToString(reader["text25"]),
                            check02 = Convert.ToString(reader["check02"]),
                            check09 = Convert.ToString(reader["check09"]),
                            check16 = Convert.ToString(reader["check16"]),
                            combo02 = Convert.ToString(reader["combo02"]),
                            combo09 = Convert.ToString(reader["combo09"]),
                            combo16 = Convert.ToString(reader["combo16"]),
                            text02 = Convert.ToString(reader["text02"]),
                            text10 = Convert.ToString(reader["text10"]),
                            text18 = Convert.ToString(reader["text18"]),
                            text26 = Convert.ToString(reader["text26"]),
                            check03 = Convert.ToString(reader["check03"]),
                            check10 = Convert.ToString(reader["check10"]),
                            check17 = Convert.ToString(reader["check17"]),
                            combo03 = Convert.ToString(reader["combo03"]),
                            combo10 = Convert.ToString(reader["combo10"]),
                            combo17 = Convert.ToString(reader["combo17"]),
                            text03 = Convert.ToString(reader["text03"]),
                            text11 = Convert.ToString(reader["text11"]),
                            text19 = Convert.ToString(reader["text19"]),
                            text27 = Convert.ToString(reader["text27"]),
                            check04 = Convert.ToString(reader["check04"]),
                            check11 = Convert.ToString(reader["check11"]),
                            check18 = Convert.ToString(reader["check18"]),
                            combo04 = Convert.ToString(reader["combo04"]),
                            combo11 = Convert.ToString(reader["combo11"]),
                            combo18 = Convert.ToString(reader["combo18"]),
                            text04 = Convert.ToString(reader["text04"]),
                            text12 = Convert.ToString(reader["text12"]),
                            text20 = Convert.ToString(reader["text20"]),
                            text28 = Convert.ToString(reader["text28"]),
                            check05 = Convert.ToString(reader["check05"]),
                            check12 = Convert.ToString(reader["check12"]),
                            check19 = Convert.ToString(reader["check19"]),
                            combo05 = Convert.ToString(reader["combo05"]),
                            combo12 = Convert.ToString(reader["combo12"]),
                            combo19 = Convert.ToString(reader["combo19"]),
                            text05 = Convert.ToString(reader["text05"]),
                            text13 = Convert.ToString(reader["text13"]),
                            text21 = Convert.ToString(reader["text21"]),
                            text29 = Convert.ToString(reader["text29"]),
                            check06 = Convert.ToString(reader["check06"]),
                            check13 = Convert.ToString(reader["check13"]),
                            check20 = Convert.ToString(reader["check20"]),
                            combo06 = Convert.ToString(reader["combo06"]),
                            combo13 = Convert.ToString(reader["combo13"]),
                            combo20 = Convert.ToString(reader["combo20"]),
                            text06 = Convert.ToString(reader["text06"]),
                            text14 = Convert.ToString(reader["text14"]),
                            text22 = Convert.ToString(reader["text22"]),
                            text30 = Convert.ToString(reader["text30"]),
                            check07 = Convert.ToString(reader["check07"]),
                            check14 = Convert.ToString(reader["check14"]),
                            combo07 = Convert.ToString(reader["combo07"]),
                            combo14 = Convert.ToString(reader["combo14"]),
                            text07 = Convert.ToString(reader["text07"]),
                            text15 = Convert.ToString(reader["text15"]),
                            text23 = Convert.ToString(reader["text23"]),
                            text08 = Convert.ToString(reader["text08"]),
                            text16 = Convert.ToString(reader["text16"]),
                            text24 = Convert.ToString(reader["text24"]),
                        };

                        lstDataByContractInfo.Add(objDataByContractInfo);
                    }
                }
            });
            lstDataContract = lstDataByContract;
            lstDataContractInfo = lstDataByContractInfo;
            ErrorCod = Convert.ToString(parameters[4].Value);
            ErrorDes = Convert.ToString(parameters[5].Value);
            return true;
        }
        public static int GetDataByCount(string strIdSession, string strTransaction, int pCustomerId,
            out List<OPlanMigration.DataByCount> lstDataCount, out List<OPlanMigration.DataByContractInfo> lstDataContractInfo)
        {

            List<OPlanMigration.DataByCount> lstDataByCount = new List<OPlanMigration.DataByCount>();
            OPlanMigration.DataByCount objDataByCount;
            List<OPlanMigration.DataByContractInfo> lstDataByContractInfo = new List<OPlanMigration.DataByContractInfo>();
            OPlanMigration.DataByContractInfo objDataByContractInfo;
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_CUSTOMER_ID", DbType.Int64, ParameterDirection.Input,pCustomerId),
                new DbParameter("P_DATOS_CUENTA", DbType.Object, ParameterDirection.Output),
                new DbParameter("P_DATOS_INFOS", DbType.Object, ParameterDirection.Output)
            };

            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_sp_datos_x_cta, parameters, (IDataReader reader) =>
            {
                while (reader.Read())
                {
                    objDataByCount = new OPlanMigration.DataByCount() {
                        CODIGO_AREA = Convert.ToString(reader["CODIGO_AREA"]),
                        CENTRO_COSTO = Convert.ToString(reader["CENTRO_COSTO"]),
                        CICLO = Convert.ToString(reader["CICLO"]),
                        LIMITE_CREDITO = Convert.ToString(reader["LIMITE_CREDITO"]),
                        CUENTA = Convert.ToString(reader["CUENTA"]),
                        CUENTA_PADRE = Convert.ToString(reader["CUENTA_PADRE"]),
                        CUSTOMER_PADRE = Convert.ToString(reader["CUSTOMER_PADRE"]),
                        NIVEL_CUENTA = Convert.ToString(reader["NIVEL_CUENTA"]),
                        REP_LEGAL = Convert.ToString(reader["REP_LEGAL"]),
                        RESP_PAGO = Convert.ToString(reader["RESP_PAGO"]),
                        ESTADO_CUENTA = Convert.ToString(reader["ESTADO_CUENTA"]),
                        TIPO_CLIENTE = Convert.ToString(reader["TIPO_CLIENTE"]),
                        PLAN_CUENTA = Convert.ToString(reader["PLAN_CUENTA"]),
                        COD_RAZON = Convert.ToString(reader["COD_RAZON"]),
                        FREC_FACT = Convert.ToString(reader["FREC_FACT"]),
                        COD_PROF = Convert.ToString(reader["COD_PROF"]),
                        TITULO = Convert.ToString(reader["TITULO"]),
                        APELLIDOS = Convert.ToString(reader["APELLIDOS"]),
                        NOMBRES = Convert.ToString(reader["NOMBRES"]),
                        ABREVIACION = Convert.ToString(reader["ABREVIACION"]),
                        RAZON_SOCIAL = Convert.ToString(reader["RAZON_SOCIAL"]),
                        FEC_NACIMIENTO = Convert.ToString(reader["FEC_NACIMIENTO"]),
                        DEPARTAMENTO = Convert.ToString(reader["DEPARTAMENTO"]),
                        PROVINCIA = Convert.ToString(reader["PROVINCIA"]),
                        COD_POSTAL = Convert.ToString(reader["COD_POSTAL"]),
                        DIRECCION = Convert.ToString(reader["DIRECCION"]),
                        REFERENCIA = Convert.ToString(reader["REFERENCIA"]),
                        DISTRITO = Convert.ToString(reader["DISTRITO"]),
                        TELF_CONTRACTO = Convert.ToString(reader["TELF_CONTRACTO"]),
                        TELF_CONT_2 = Convert.ToString(reader["TELF_CONT_2"]),
                        MAIL = Convert.ToString(reader["MAIL"]),
                        TIPO_DOC = Convert.ToString(reader["TIPO_DOC"]),
                        NRO_DOC = Convert.ToString(reader["NRO_DOC"]),
                        RUC = Convert.ToString(reader["RUC"]),
                        SEXO = Convert.ToString(reader["SEXO"]),
                        VAL_DIRECION = Convert.ToString(reader["VAL_DIRECION"]),
                        PAIS = Convert.ToString(reader["PAIS"]),
                        SEGMENTO = Convert.ToString(reader["SEGMENTO"]),
                        NACIONALIDAD = Convert.ToString(reader["NACIONALIDAD"]),
                        ROL = Convert.ToString(reader["ROL"]),
                        NICHO = Convert.ToString(reader["NICHO"]),
                        COD_UBIGEO = Convert.ToString(reader["COD_UBIGEO"]),
                        COD_PLANO = Convert.ToString(reader["COD_PLANO"]),
                        TIPO_INFO = Convert.ToString(reader["TIPO_INFO"])
                    };
                    lstDataByCount.Add(objDataByCount);
                }

                if (reader.NextResult())
                {
                    while (reader.Read())
                    {
                        objDataByContractInfo = new OPlanMigration.DataByContractInfo()
                        {
                            customer_id = Convert.ToString(reader["customer_id"]),
                            check01 = Convert.ToString(reader["check01"]),
                            check08 = Convert.ToString(reader["check08"]),
                            check15 = Convert.ToString(reader["check15"]),
                            combo01 = Convert.ToString(reader["combo01"]),
                            combo08 = Convert.ToString(reader["combo08"]),
                            combo15 = Convert.ToString(reader["combo15"]),
                            text01 = Convert.ToString(reader["text01"]),
                            text09 = Convert.ToString(reader["text09"]),
                            text17 = Convert.ToString(reader["text17"]),
                            text25 = Convert.ToString(reader["text25"]),
                            check02 = Convert.ToString(reader["check02"]),
                            check09 = Convert.ToString(reader["check09"]),
                            check16 = Convert.ToString(reader["check16"]),
                            combo02 = Convert.ToString(reader["combo02"]),
                            combo09 = Convert.ToString(reader["combo09"]),
                            combo16 = Convert.ToString(reader["combo16"]),
                            text02 = Convert.ToString(reader["text02"]),
                            text10 = Convert.ToString(reader["text10"]),
                            text18 = Convert.ToString(reader["text18"]),
                            text26 = Convert.ToString(reader["text26"]),
                            check03 = Convert.ToString(reader["check03"]),
                            check10 = Convert.ToString(reader["check10"]),
                            check17 = Convert.ToString(reader["check17"]),
                            combo03 = Convert.ToString(reader["combo03"]),
                            combo10 = Convert.ToString(reader["combo10"]),
                            combo17 = Convert.ToString(reader["combo17"]),
                            text03 = Convert.ToString(reader["text03"]),
                            text11 = Convert.ToString(reader["text11"]),
                            text19 = Convert.ToString(reader["text19"]),
                            text27 = Convert.ToString(reader["text27"]),
                            check04 = Convert.ToString(reader["check04"]),
                            check11 = Convert.ToString(reader["check11"]),
                            check18 = Convert.ToString(reader["check18"]),
                            combo04 = Convert.ToString(reader["combo04"]),
                            combo11 = Convert.ToString(reader["combo11"]),
                            combo18 = Convert.ToString(reader["combo18"]),
                            text04 = Convert.ToString(reader["text04"]),
                            text12 = Convert.ToString(reader["text12"]),
                            text20 = Convert.ToString(reader["text20"]),
                            text28 = Convert.ToString(reader["text28"]),
                            check05 = Convert.ToString(reader["check05"]),
                            check12 = Convert.ToString(reader["check12"]),
                            check19 = Convert.ToString(reader["check19"]),
                            combo05 = Convert.ToString(reader["combo05"]),
                            combo12 = Convert.ToString(reader["combo12"]),
                            combo19 = Convert.ToString(reader["combo19"]),
                            text05 = Convert.ToString(reader["text05"]),
                            text13 = Convert.ToString(reader["text13"]),
                            text21 = Convert.ToString(reader["text21"]),
                            text29 = Convert.ToString(reader["text29"]),
                            check06 = Convert.ToString(reader["check06"]),
                            check13 = Convert.ToString(reader["check13"]),
                            check20 = Convert.ToString(reader["check20"]),
                            combo06 = Convert.ToString(reader["combo06"]),
                            combo13 = Convert.ToString(reader["combo13"]),
                            combo20 = Convert.ToString(reader["combo20"]),
                            text06 = Convert.ToString(reader["text06"]),
                            text14 = Convert.ToString(reader["text14"]),
                            text22 = Convert.ToString(reader["text22"]),
                            text30 = Convert.ToString(reader["text30"]),
                            check07 = Convert.ToString(reader["check07"]),
                            check14 = Convert.ToString(reader["check14"]),
                            combo07 = Convert.ToString(reader["combo07"]),
                            combo14 = Convert.ToString(reader["combo14"]),
                            text07 = Convert.ToString(reader["text07"]),
                            text15 = Convert.ToString(reader["text15"]),
                            text23 = Convert.ToString(reader["text23"]),
                            text08 = Convert.ToString(reader["text08"]),
                            text16 = Convert.ToString(reader["text16"]),
                            text24 = Convert.ToString(reader["text24"]),
                        };

                        lstDataByContractInfo.Add(objDataByContractInfo);
                    }
                }
            });

            lstDataContractInfo = lstDataByContractInfo;
            lstDataCount = lstDataByCount;
            return 0;
        }

        public static bool MigrationPlans(string strIdSession,string strIdTransaccion, string strIPAplicacion, string strAplicacion, string strMsisdn, string strCoId,
                                        string strCustomerId, string strCuenta, string strEscenario, string strTipoProducto, string strServiciosAdicionales, string strCodigoProducto,
                                        string strCodPlanBase, decimal dblMontoApadece, decimal dblMontoFidelizar, string strFlagValidaApadece, string strFlagAplicaApadece,
                                        string strTopeConsumo, string strTipoTope, string strDescripcionTipoTpe, string strTipoRegistroTope, int intTopeControlConsumo,
                                        string strFechaProgramacionTope, string strCAC, string strAsesor, string strCodigoInteraccion, decimal dblMontoPCS, string strAreaPCS, string strMotivoPCS,
                                        string strSubMotivoPCS, int intCicloFacturacion, string strIdTipoCliente, string strNumeroDocumento, string strFlagServicioOnTop,
                                        string strFechaProgramacion, string strFlagLimiteCredito, string strTipoClarify, string strNumeroCuentaPadre, string strUsuarioAplicacion,
                                        string strUsuarioSistema, out string strCodRespuestaReturn, out string p_strMensaje)
        {
            bool result = false;
            strCodRespuestaReturn = string.Empty;
            p_strMensaje = string.Empty;
            try
            {
            string mensaje = "";
            string strCodRespuesta = "";
            string[] partsFechProgTop = strFechaProgramacionTope.Split('/');
            string[] partsFechProg = strFechaProgramacion.Split('/');
            string strFechaProgTop = partsFechProgTop[2] + "-" + partsFechProgTop[1] + "-" + partsFechProgTop[0];
            string strFechaProg = partsFechProg[2] + "-" + partsFechProg[1] + "-" + partsFechProg[0];
            strCodRespuesta = Claro.Web.Logging.ExecuteMethod(strIdSession, strIdTransaccion,
                    Configuration.WebServiceConfiguration.MigracionPlanPostpago, () =>
                    {
                        return Configuration.WebServiceConfiguration.MigracionPlanPostpago.programarMigracion(ref strIdTransaccion, strIPAplicacion, strAplicacion, strMsisdn, strCoId,
                                     strCustomerId, strCuenta, strEscenario, strTipoProducto, strServiciosAdicionales, strCodigoProducto,
                                     strCodPlanBase,dblMontoApadece, dblMontoFidelizar, strFlagValidaApadece, strFlagAplicaApadece,
                                     strTopeConsumo, strTipoTope, strDescripcionTipoTpe, strTipoRegistroTope, intTopeControlConsumo,
                                     Convert.ToDate(strFechaProgTop), strCAC, strAsesor, strCodigoInteraccion, dblMontoPCS, strAreaPCS, strMotivoPCS,
                                     strSubMotivoPCS, intCicloFacturacion, strIdTipoCliente, strNumeroDocumento, strFlagServicioOnTop,
                                     Convert.ToDate(strFechaProg), strFlagLimiteCredito, strTipoClarify, strNumeroCuentaPadre, strUsuarioAplicacion,
                                     strUsuarioSistema, out mensaje);
                    });
            p_strMensaje = mensaje;
            strCodRespuestaReturn = strCodRespuesta;
            result= true;
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }

    }
}
