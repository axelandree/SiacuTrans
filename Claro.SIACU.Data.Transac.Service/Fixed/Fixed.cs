using System;
using System.Data;
using System.Globalization;
using System.Collections.Generic;
using Claro.Data;
using Claro.SIACU.Data.Transac.Service.Configuration;
using Claro.SIACU.Transac.Service;
using CSTS = Claro.SIACU.Transac.Service;
using EntitiesFixed = Claro.SIACU.Entity.Transac.Service.Fixed;
using CUSTOMER_HFC = Claro.SIACU.ProxyService.Transac.Service.SIACFixed.CustomerHFC;
using CUSTOMER_LTE = Claro.SIACU.ProxyService.Transac.Service.SIACFixed.CustomerLTE;
using Claro.SIACU.Entity.Transac.Service.Fixed.GetGenerateOCC;
using Claro.SIACU.ProxyService.Transac.Service.TransaccionOCC;
using Claro.SIACU.Entity.Transac.Service.Fixed.GetCaseInsert;
using Claro.SIACU.Entity.Transac.Service.Fixed;
using Claro.SIACU.Entity.Transac.Service.Fixed.GetConsultationServiceByContract;
using Claro.SIACU.Entity.Transac.Service.Fixed.GetReconeService;
using ADMCU = Claro.SIACU.ProxyService.Transac.Service.SIACFixed.CrewManagement;
using INTOA = Claro.SIACU.ProxyService.Transac.Service.SIACPost.InboundToaWS;
using Claro.SIACU.Entity.Transac.Service.Common;
using Claro.SIACU.Entity.Transac.Service.Fixed.BlackWhiteList; 
using KEY = Claro.ConfigurationManager;
using Claro.SIACU.Entity.Transac.Service.Fixed.GetValidateLine;
using ENTITYS = Claro.SIACU.Entity.Transac.Service.Fixed;
using ENTITIES_CONSULTALINEA = Claro.SIACU.Entity.Transac.Service.Fixed.getConsultaLineaCuenta;
using CONSULTALINEACUENTA = Claro.SIACU.ProxyService.Transac.Service.SIAC.ConsultaLineaCuenta;
using POSTPREDATA = Claro.SIACU.ProxyService.Transac.Service.SIAC.Post.DatosPrePost_V2;
using System.Collections;

//INI: INICIATIVA-871
using CONSULTCLAVE = Claro.Transversal.ProxyService.ConsultaClaves;
using CONSULTAR_DATOS_SIM = Claro.SIACU.ProxyService.SIACPre.ConsultarDatosSIM;
using Claro.SIACU.Entity.Transac.Service.GetDataPower;
using Claro.SIACU.Entity.Transac.Service.Fixed.Discard;
using Claro.SIACU.ProxyService.Transac.Service.InstantLinkSOA;
using Claro.SIACU.Entity.Transac.Service.Fixed.Discard.ProcesarContinue;
using System.Text;
using System.IO;
//FIN: INICIATIVA-871

namespace Claro.SIACU.Data.Transac.Service.Fixed
{
    public class Fixed
    {
        //NAME: ObtenerCliente DACliente
        public static EntitiesFixed.Customer GetCustomer(string strIdSession, string strTransaction, string vPhone, string vAccount, string vContactobjid1, string vFlagReg, ref  string vFlagConsulta, ref string vMsgText)
        {
            if (vContactobjid1 == "")
                vContactobjid1 = null;

            DbParameter[] parameters = {new DbParameter("P_PHONE", DbType.String,20,ParameterDirection.Input, vPhone),
                new DbParameter("P_ACCOUNT", DbType.String,80,ParameterDirection.Input, vAccount),
                new DbParameter("P_CONTACTOBJID_1", DbType.Int64,ParameterDirection.Input, vContactobjid1),
                new DbParameter("P_FLAG_REG", DbType.String,20,ParameterDirection.Input, vFlagReg),												
                new DbParameter("P_FLAG_CONSULTA", DbType.String,255,ParameterDirection.Output, vFlagConsulta),
                new DbParameter("P_MSG_TEXT", DbType.String,255,ParameterDirection.Output, vMsgText),
                new DbParameter("CUSTOMER", DbType.Object, ParameterDirection.Output)
            };

            var objEntity = new EntitiesFixed.Customer();
            try
            {
                Web.Logging.ExecuteMethod(strIdSession, strTransaction, () =>
                {
                    DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_CLARIFY,
                        DbCommandConfiguration.SIACU_POST_CLARIFY_SP_CUSTOMER_CLFY_HFC, parameters, reader =>
                        {
                            while (reader.Read())
                            {
                                objEntity = new EntitiesFixed.Customer
                                {
                                    OBJID_CONTACTO =
                                        (String)(DataUtil
                                            .DbValueToDefault<decimal>(reader[reader.GetOrdinal("OBJID_CONTACTO")])
                                            .ToString()),
                                    OBJID_SITE = (String)DataUtil.DbValueToDefault<decimal>(reader["OBJID_SITE"])
                                        .ToString(CultureInfo.InvariantCulture),
                                    TELEFONO = DataUtil.DbValueToDefault<string>(reader["TELEFONO"]),
                                    CUENTA = DataUtil.DbValueToDefault<string>(reader["CUENTA"]),
                                    MODALIDAD = DataUtil.DbValueToDefault<string>(reader["MODALIDAD"]),
                                    SEGMENTO = DataUtil.DbValueToDefault<string>(reader["SEGMENTO"]),
                                    ROL_CONTACTO = DataUtil.DbValueToDefault<string>(reader["ROL_CONTACTO"]),
                                    ESTADO_CONTACTO = DataUtil.DbValueToDefault<string>(reader["ESTADO_CONTACTO"]),
                                    ESTADO_CONTRATO = DataUtil.DbValueToDefault<string>(reader["ESTADO_CONTRATO"]),
                                    ESTADO_SITE = DataUtil.DbValueToDefault<string>(reader["ESTADO_SITE"]),
                                    S_NOMBRES = DataUtil.DbValueToDefault<string>(reader["S_NOMBRES"]),
                                    S_APELLIDOS = DataUtil.DbValueToDefault<string>(reader["S_APELLIDOS"]),
                                    NOMBRES = DataUtil.DbValueToDefault<string>(reader["NOMBRES"]),
                                    APELLIDOS = DataUtil.DbValueToDefault<string>(reader["APELLIDOS"]),
                                    DOMICILIO = DataUtil.DbValueToDefault<string>(reader["DOMICILIO"]),
                                    URBANIZACION = DataUtil.DbValueToDefault<string>(reader["URBANIZACION"]),
                                    REFERENCIA = DataUtil.DbValueToDefault<string>(reader["REFERENCIA"]),
                                    CIUDAD = DataUtil.DbValueToDefault<string>(reader["CIUDAD"]),
                                    DISTRITO = DataUtil.DbValueToDefault<string>(reader["DISTRITO"]),
                                    DEPARTAMENTO = DataUtil.DbValueToDefault<string>(reader["DEPARTAMENTO"]),
                                    ZIPCODE = DataUtil.DbValueToDefault<string>(reader["ZIPCODE"]),
                                    EMAIL = DataUtil.DbValueToDefault<string>(reader["EMAIL"]),
                                    TELEF_REFERENCIA = DataUtil.DbValueToDefault<string>(reader["TELEF_REFERENCIA"]),
                                    FAX = DataUtil.DbValueToDefault<string>(reader["FAX"]),
                                    FECHA_NAC =
                                        DataUtil.DbValueToDefault<DateTime>(reader["FECHA_NAC"]).ToShortDateString(),
                                    SEXO = DataUtil.DbValueToDefault<string>(reader["SEXO"]),
                                    ESTADO_CIVIL = DataUtil.DbValueToDefault<string>(reader["ESTADO_CIVIL"]),
                                    TIPO_DOC = DataUtil.DbValueToDefault<string>(reader["TIPO_DOC"]),
                                    NRO_DOC = DataUtil.DbValueToDefault<string>(reader["NRO_DOC"]),
                                    FECHA_ACT = DataUtil.DbValueToDefault<DateTime>(reader["FECHA_ACT"]),
                                    PUNTO_VENTA = DataUtil.DbValueToDefault<string>(reader["PUNTO_VENTA"]),
                                    FLAG_REGISTRADO =
                                        (int)DataUtil.DbValueToDefault<decimal>(reader["FLAG_REGISTRADO"]),
                                    OCUPACION = DataUtil.DbValueToDefault<string>(reader["OCUPACION"]),
                                    CANT_REG = (string)DataUtil.DbValueToDefault<decimal>(reader["CANT_REG"])
                                        .ToString(),
                                    FLAG_EMAIL = DataUtil.DbValueToDefault<string>(reader["FLAG_EMAIL"]),
                                    MOTIVO_REGISTRO = DataUtil.DbValueToDefault<string>(reader["MOTIVO_REGISTRO"]),
                                    FUNCION = DataUtil.DbValueToDefault<string>(reader["FUNCION"]),
                                    CARGO = DataUtil.DbValueToDefault<string>(reader["CARGO"]),
                                    LUGAR_NACIMIENTO_DES = DataUtil.DbValueToDefault<string>(reader["LUGAR_NAC"])
                                };
                            }
                        });
                });

            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
                throw;
            }
            finally
            {
                vFlagConsulta = parameters[parameters.Length - 3].Value.ToString();
                vMsgText = parameters[parameters.Length - 2].Value.ToString();
            }

            return objEntity;
        }

        /// <summary>
        /// Método que obtiene una lista con los datos del teléfono  por código de contrato HFC.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strIdTransaction">Id de transacción</param>
        /// <param name="strIpApplication">Ip de aplicación</param>
        /// <param name="strApplicationName">Nombre de aplicación</param>
        /// <param name="strUserApplication">Usuario de aplicación</param>
        /// <param name="strContractId">Id de contrato</param>
        /// <returns>Devuelve listado de objetos Service con información del servicio.</returns>
        public static List<Claro.SIACU.Entity.Transac.Service.Fixed.Service> GetTelephoneByContractCodeHFC(string strIdSession, string strIdTransaction, string strIpApplication, string strApplicationName, string strUserApplication, string strContractId)
        {
            List<Claro.SIACU.Entity.Transac.Service.Fixed.Service> lstService = new List<Claro.SIACU.Entity.Transac.Service.Fixed.Service>();

            CUSTOMER_HFC.consultarListaTelefonoPorCodigoContratoEAIResponse oConsultationListOut =
                 Claro.Web.Logging.ExecuteMethod<CUSTOMER_HFC.consultarListaTelefonoPorCodigoContratoEAIResponse>
            (strIdSession, strIdTransaction, Configuration.ServiceConfiguration.FIXED_CUSTOMER_HFC, () =>
            {
                return Configuration.ServiceConfiguration.FIXED_CUSTOMER_HFC.consultarListaTelefonoPorCodigoContrato(new CUSTOMER_HFC.consultarListaTelefonoPorCodigoContratoEAIRequest()
                {
                    consultarListaTelefonoPorCodigoContratoEaiRequest = new CUSTOMER_HFC.ConsultarListaTelefonoPorCodigoContratoEAIInput()
                    {
                        cabeceraRequest = new CUSTOMER_HFC.CabeceraRequest()
                        {
                            idTransaccion = strIdTransaction,
                            ipAplicacion = strIpApplication,
                            nombreAplicacion = strApplicationName,
                            usuarioAplicacion = strUserApplication
                        },
                        cuerpoRequest = new CUSTOMER_HFC.CuerpoCLTCRequest()
                        {
                            codigoContrato = strContractId
                        }
                    }
                });

            });

            CUSTOMER_HFC.CabeceraResponse oHeaderOutput = oConsultationListOut.consultarListaTelefonoPorCodigoContratoEaiResponse.cabeceraResponse;
            CUSTOMER_HFC.TelefonoPorCodigoContratoType[] oTempCustomer = oConsultationListOut.consultarListaTelefonoPorCodigoContratoEaiResponse.cuerpoResponse.listaTelefonoPorCodigoContrato;

            if (oHeaderOutput.codigoRespuesta.Equals(Claro.Constants.NumberZeroString))
            {
                for (int i = 0; i < oTempCustomer.Length; i++)
                {
                    Claro.SIACU.Entity.Transac.Service.Fixed.Service objService = new Claro.SIACU.Entity.Transac.Service.Fixed.Service();
                    objService.NRO_CELULAR = Convert.ToString(oTempCustomer[i].dnNum);
                    objService.ESTADO_LINEA = Convert.ToString(oTempCustomer[i].estadoLinea);
                    lstService.Add(objService);
                }
            }

            return lstService;
        }

        /// <summary>
        /// Método que obtiene una lista con la consulta de teléfono por código de contrato LTE.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strIdTransaction">Id de transacción</param>
        /// <param name="strIpApplication">Ip de aplicación</param>
        /// <param name="strApplicationName">Nombre de aplicación</param>
        /// <param name="strUserApplication">Usuario de aplicación</param>
        /// <param name="strContractId">Id de contrato</param>
        /// <returns>Devuelve listado de objetos Service con información de la misma.</returns>
        public static List<Claro.SIACU.Entity.Transac.Service.Fixed.Service> GetTelephoneByContractCodeLTE(string strIdSession, string strIdTransaction, string strIpApplication, string strApplicationName, string strUserApplication, string strContractId)
        {
            List<Claro.SIACU.Entity.Transac.Service.Fixed.Service> lstService = new List<Claro.SIACU.Entity.Transac.Service.Fixed.Service>();
            CUSTOMER_LTE.consultarListaTelefonoPorCodigoContratoEAIResponse oConsultationListOut =
               Claro.Web.Logging.ExecuteMethod<CUSTOMER_LTE.consultarListaTelefonoPorCodigoContratoEAIResponse>
           (strIdSession, strIdTransaction, Configuration.ServiceConfiguration.FIXED_CUSTOMER_LTE, () =>
           {
               return Configuration.ServiceConfiguration.FIXED_CUSTOMER_LTE.consultarListaTelefonoPorCodigoContrato(new CUSTOMER_LTE.consultarListaTelefonoPorCodigoContratoEAIRequest()
               {
                   codigoContrato = strContractId,
                   auditRequest = new CUSTOMER_LTE.AuditRequestType()
                   {
                       idTransaccion = strIdTransaction,
                       ipAplicacion = strIpApplication,
                       nombreAplicacion = strApplicationName,
                       usuarioAplicacion = strUserApplication
                   }
               });
           });

            CUSTOMER_LTE.AuditResponseType objAuditResponse = oConsultationListOut.auditResponse;
            CUSTOMER_LTE.TelefonoPorCodigoContratoType[] oTempCustomer = oConsultationListOut.listaTelefonoPorCodigoContrato;

            if (objAuditResponse.codigoRespuesta.Equals(Claro.Constants.NumberZeroString))
            {
                for (int i = 0; i < oTempCustomer.Length; i++)
                {
                    Claro.SIACU.Entity.Transac.Service.Fixed.Service objService = new Claro.SIACU.Entity.Transac.Service.Fixed.Service();
                    objService.NRO_CELULAR = Convert.ToString(oTempCustomer[i].dnNum);
                    objService.ESTADO_LINEA = Convert.ToString(oTempCustomer[i].estadoLinea);
                    lstService.Add(objService);
                }
            }
            return lstService;
        }

        public static GenerateOCCResponse GenerateOCC(GenerateOCCRequest objRequest)
        {

            decimal registraOCC = 0;
            bool registraOccSpecified = true;

            GenerateOCCResponse oGenerateOCCResponse = new GenerateOCCResponse();

            AuditType objResponse = Claro.Web.Logging.ExecuteMethod<AuditType>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
            {
                return Configuration.WebServiceConfiguration.TransaccionOCC.GeneraOCC(objRequest.txId,
                                                                            objRequest.ipApp,
                                                                            objRequest.usrApp,
                                                                            objRequest.customerId,
                                                                            objRequest.customerIdSpecified,
                                                                            objRequest.codigoOcc,
                                                                            objRequest.codigoOccSpecified,
                                                                            objRequest.nroCuotas,
                                                                            objRequest.nroCuotasSpecified,
                                                                            objRequest.montoOcc,
                                                                            objRequest.montoOccSpecified,
                                                                            objRequest.recDate,
                                                                            objRequest.recDateSpecified,
                                                                            objRequest.remark,
                                                                            out registraOCC,
                                                                            out registraOccSpecified);
            });


            oGenerateOCCResponse.txId = objResponse.txId;
            oGenerateOCCResponse.errorCode = objResponse.errorCode;
            oGenerateOCCResponse.errorMsg = objResponse.errorMsg;
            oGenerateOCCResponse.registraOcc = registraOCC;
            oGenerateOCCResponse.registraOccSpecified = registraOccSpecified;

            return oGenerateOCCResponse;


        }

        #region "Inst/Desinst Decodificadores"
        public static List<EntitiesFixed.JobType> GetJobTypes(string strIdSession, string strTransaction, int vintTipoTransaccion)
        {
            List<Entity.Transac.Service.Fixed.JobType> list = null;

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("p_tipo", DbType.Int32,22, ParameterDirection.Input, vintTipoTransaccion),
            new DbParameter("srv_tipra", DbType.Object, ParameterDirection.Output)
            };

            list = DbFactory.ExecuteReader<List<Entity.Transac.Service.Fixed.JobType>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_SP_CONSULTA_TIPTRA, parameters);

            return list;
        }
        public static EntitiesFixed.ETAFlow ETAFlowValidate(string strIdSession, string strTransaction, string as_origen, string av_idplano, string av_ubigeo, int an_tiptra,
                                     string an_tipsrv)
        {
            EntitiesFixed.ETAFlow oEtaFlow;
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

            DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_SP_VALIDA_FLUJO_ZONA_ADC, parameters);

            return new EntitiesFixed.ETAFlow
            {
                an_indica = Convert.ToInt(parameters[6].Value.ToString()),
                as_codzona = Convert.ToString(parameters[5].Value)
            };
        }
        public static List<Entity.Transac.Service.Fixed.OrderType> GetOrderType(string strIdSession, string strTransaction, string vintTipoTra)
        {
            List<Entity.Transac.Service.Fixed.OrderType> list = null;

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("vIdtiptra", DbType.String,255, ParameterDirection.Input, vintTipoTra),
                                new DbParameter("cur_tipo", DbType.Object, ParameterDirection.Output)
            };

            list = DbFactory.ExecuteReader<List<Entity.Transac.Service.Fixed.OrderType>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_SP_OBTIENE_TIPO_ORDEN_TIPTRA, parameters);

            return list;
        }
        public static List<Entity.Transac.Service.Fixed.OrderSubType> GetOrderSubType(string strIdSession, string strTransaction, string vintTipoOrden)
        {
            List<Entity.Transac.Service.Fixed.OrderSubType> list = null;
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("vIdtiptra", DbType.String,255, ParameterDirection.Input, vintTipoOrden),
                new DbParameter("cur_tipo", DbType.Object, ParameterDirection.Output)
                //new DbParameter("av_cod_tipo_orden", DbType.String,255, ParameterDirection.Input, vintTipoOrden)
            };

            list = DbFactory.ExecuteReader<List<Entity.Transac.Service.Fixed.OrderSubType>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_P_CONSULTA_SUBTIPORD, parameters);

            return list;
        }

        public static bool GetInsertDecoAdditional(string strIdSession, string strTransaction, string vInter, string vServ,
                                                   string vGrupoPrincipal, string vGrupo, string vCantidadInst, string vDscServ,
                                                   string vBandwid, string vFlagLc, string vCantIdLinea, string vTipoEquipo,
                                                   string vCodTipoEquipo, string vCantidad, string vDscEquipo, string vCodigoExt,
                                                   string vSnCode, string vSpCode, string vCargoFijo, ref string rCodRes, ref string rDescRes)
        {
            bool strRet = false;

            DbParameter[] parameters = {
				                                new DbParameter("idinteraccion_in",DbType.String,300 ,ParameterDirection.Input, vInter),
				                                new DbParameter("servicio_in",DbType.String,300,ParameterDirection.Input, vServ), 
				                                new DbParameter("idgrupo_principal_in",DbType.String,300,ParameterDirection.Input, vGrupoPrincipal), 
				                                new DbParameter("idgrupo_in",DbType.String,300,ParameterDirection.Input,vGrupo), 
				                                new DbParameter("cantidad_instancia_in",DbType.String,300,ParameterDirection.Input, vCantidadInst), 
				                                new DbParameter("dscsrv_in",DbType.String,300,ParameterDirection.Input, vDscServ), 
				                                new DbParameter("bandwid_in",DbType.String,300,ParameterDirection.Input, vBandwid), 
				                                new DbParameter("flag_lc_in",DbType.String,300,ParameterDirection.Input, vFlagLc), 
				                                new DbParameter("cantidad_idlinea_in",DbType.String,300,ParameterDirection.Input, vCantIdLinea), 
				                                new DbParameter("tipequ_in",DbType.String,300,ParameterDirection.Input, vTipoEquipo), 
				                                new DbParameter("codtipequ_in",DbType.String,300,ParameterDirection.Input, vCodTipoEquipo), 
				                                new DbParameter("cantidad_in",DbType.String,300,ParameterDirection.Input, vCantidad), 
				                                new DbParameter("dscequ_in",DbType.String,300,ParameterDirection.Input, vDscEquipo), 
				                                new DbParameter("codigo_ext_in",DbType.String,300,ParameterDirection.Input, vCodigoExt), 
				                                new DbParameter("sncode",DbType.String,300,ParameterDirection.Input, vSnCode), 
				                                new DbParameter("spcode",DbType.String,300,ParameterDirection.Input, vSpCode), 
				                                new DbParameter("cargofijo",DbType.Double,ParameterDirection.Input, vCargoFijo),  
				                                new DbParameter("errmsg_out",DbType.String,1000,ParameterDirection.Output),
                                                new DbParameter("resultado_out",DbType.Int64,300,ParameterDirection.Output)
                                            };

            try
            {
                Web.Logging.ExecuteMethod(strIdSession, strTransaction, () =>
                {
                    DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_P_POSVENTA_DET_DECO_INSERTAR, parameters);
                    strRet = true;
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
            }
            finally
            {
                rDescRes = CSTS.Functions.CheckStr(parameters[parameters.Length - 17].Value);
                rCodRes = CSTS.Functions.CheckStr(parameters[parameters.Length - 18].Value);
            }

            return strRet;
        }
        public static bool GetInsertDetailServiceInteraction(string strIdSession, string strTransaction, string codinterac,
                                                             string nombreserv, string tiposerv, string gruposerv, string cf, string equipo, string cantidad,
                                                             ref string resultado, ref string mensaje)
        {
            bool strRet = false;

            DbParameter[] parameters = {
				new DbParameter("P_COD_INTER",DbType.String ,ParameterDirection.Input, codinterac),
                new DbParameter("P_NOM_SERV",DbType.String ,ParameterDirection.Input, nombreserv),
                new DbParameter("P_TIP_SERV",DbType.String ,ParameterDirection.Input, tiposerv),
                new DbParameter("P_GRUP_SERV",DbType.String ,ParameterDirection.Input, gruposerv),
                new DbParameter("P_CF",DbType.String ,ParameterDirection.Input, cf),
                new DbParameter("P_EQUIPO",DbType.String ,ParameterDirection.Input, equipo),
                new DbParameter("P_CANTIDAD",DbType.String ,ParameterDirection.Input, cantidad),
				new DbParameter("FLAG_RESULTADO",DbType.Int64,ParameterDirection.Output),
                new DbParameter("MSG_TEXT",DbType.String,255 ,ParameterDirection.Output)
            };

            try
            {
                Web.Logging.ExecuteMethod(strIdSession, strTransaction, () =>
                {
                    DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_CLARIFY, DbCommandConfiguration.SIACU_SP_INS_INTER_SERV_MP, parameters);
                });
                strRet = true;
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
            }
            finally
            {
                resultado = CSTS.Functions.CheckStr(parameters[parameters.Length - 2].Value);
                mensaje = CSTS.Functions.CheckStr(parameters[parameters.Length - 1].Value);
            }

            return strRet;
        }
        public static string GetInsertETASelection(string strIdSession, string strTransaction, int vidconsulta,
                                                   string vidInteraccion, DateTime vfechaCompromiso, string vfranja, string vid_bucket, ref string vresp)
        {
            DbParameter[] parameters = {
				new DbParameter("vidconsulta",DbType.String, 20 ,ParameterDirection.Input, vidconsulta),
                new DbParameter("vidInteraccion",DbType.String, 20 ,ParameterDirection.Input, vidInteraccion),
                new DbParameter("vfechaCompromiso",DbType.Date, 20 ,ParameterDirection.Input, vfechaCompromiso),
                new DbParameter("vfranja",DbType.String, 20 ,ParameterDirection.Input, vfranja),
                new DbParameter("vid_bucket",DbType.String, 50 ,ParameterDirection.Input, vid_bucket),
                new DbParameter("vresp",DbType.String, 20 ,ParameterDirection.Output)
            };

            try
            {
                Web.Logging.ExecuteMethod(strIdSession, strTransaction, () =>
                {
                    DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_POST_SGA_P_REGISTRA_ETA_SEL, parameters);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
            }
            finally
            {
                vresp = CSTS.Functions.CheckStr(parameters[parameters.Length - 1].Value);
            }

            return vresp;
        }
        public static string GetInsertTransaction(string strIdSession, string strTransaction, EntitiesFixed.Transfer objTransfer,
                                                  ref string rstrResCod, ref string rstrResDes)
        {
            if (objTransfer != null)
            {
                var json_object = Newtonsoft.Json.JsonConvert.SerializeObject(objTransfer);
                Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + strTransaction, "Begin a GetInsertTransaction Capa Data --> Parametros de entrada objTransfer: " + json_object); // Temporal
            }
            else
                Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + strTransaction, "Begin a GetInsertTransaction Capa Data --> Parametros de entrada objTransfer: null"); // Temporal
            

            string intNumSot = string.Empty;
            if (objTransfer.InterCasoID == null)
                objTransfer.InterCasoID = CSTS.Constants.strCero;
            if (objTransfer.InterCasoID == string.Empty)
                objTransfer.InterCasoID = CSTS.Constants.strCero;

            if (objTransfer.NumCarta == null)
                objTransfer.NumCarta = CSTS.Constants.strCero;
            if (objTransfer.NumCarta == string.Empty)
                objTransfer.NumCarta = CSTS.Constants.strCero;

            
            string strNroVia = !string.IsNullOrEmpty(objTransfer.TipoUrb)
                ? (objTransfer.NroVia == Claro.SIACU.Transac.Service.Constants.numeroCero
                    ? Claro.SIACU.Transac.Service.Constants.PresentationLayer.gstrVariableSNAbreviado
                    : Functions.CheckStr(objTransfer.NroVia))
                : objTransfer.TipoUrb;
            

    
            DbParameter[] parameters = {
                new DbParameter("p_id", DbType.String, 255, ParameterDirection.Input, CSTS.Functions.CheckStr(objTransfer.InterCasoID)),
                new DbParameter("v_cod_id", DbType.String, 255, ParameterDirection.Input, CSTS.Functions.CheckStr(objTransfer.ConID)),
                new DbParameter("v_customer_id", DbType.String, 255, ParameterDirection.Input, CSTS.Functions.CheckStr(objTransfer.CustomerID)),
                new DbParameter("v_tipotrans", DbType.String, 255, ParameterDirection.Input, CSTS.Functions.CheckStr(objTransfer.TransTipo)),
                new DbParameter("v_codintercaso", DbType.String, 255, ParameterDirection.Input, CSTS.Functions.CheckStr(objTransfer.InterCasoID)),
                new DbParameter("v_tipovia", DbType.String, 255, ParameterDirection.Input, CSTS.Functions.CheckStr(objTransfer.TipoVia)),
                new DbParameter("v_nombrevia", DbType.String, 255, ParameterDirection.Input, CSTS.Functions.CheckStr(objTransfer.NomVia)),
                new DbParameter("v_numerovia", DbType.String, 255, ParameterDirection.Input,strNroVia ), 
                new DbParameter("v_tipourbanizacion", DbType.Int32, ParameterDirection.Input, CSTS.Functions.CheckInt(objTransfer.TipoUrb)),
                new DbParameter("v_nombreurbanizacion", DbType.String, 255, ParameterDirection.Input, CSTS.Functions.CheckStr(objTransfer.NomUrb)),
                new DbParameter("v_manzana", DbType.String, 255, ParameterDirection.Input, CSTS.Functions.CheckStr(objTransfer.NumMZ)),
                new DbParameter("v_lote", DbType.String, 255, ParameterDirection.Input, CSTS.Functions.CheckStr(objTransfer.NumLote)),
                new DbParameter("v_codubigeo", DbType.String, 255, ParameterDirection.Input, CSTS.Functions.CheckStr(objTransfer.Ubigeo)),
                new DbParameter("v_codzona", DbType.Int32, ParameterDirection.Input, CSTS.Functions.CheckInt(objTransfer.ZonaID)),
                new DbParameter("v_idplano", DbType.String, 255, ParameterDirection.Input, CSTS.Functions.CheckStr(objTransfer.PlanoID)),
                new DbParameter("v_codedif", DbType.String, 255, ParameterDirection.Input, CSTS.Functions.CheckStr(objTransfer.EdificioID)),
                new DbParameter("v_referencia", DbType.String, 255, ParameterDirection.Input, CSTS.Functions.CheckStr(objTransfer.Referencia)),
                new DbParameter("v_observacion", DbType.String, 4000, ParameterDirection.Input, CSTS.Functions.CheckStr(objTransfer.Observacion)),
                new DbParameter("v_fec_prog", DbType.Date, ParameterDirection.Input, CSTS.Functions.CheckDate( objTransfer.FechaProgramada)),
                new DbParameter("v_franja_horaria", DbType.String, 255, ParameterDirection.Input, CSTS.Functions.CheckStr(objTransfer.FranjaHora)),
                new DbParameter("v_numcarta", DbType.Double, ParameterDirection.Input, CSTS.Functions.CheckDbl(objTransfer.NumCarta)),
                new DbParameter("v_operador", DbType.String, 255, ParameterDirection.Input, CSTS.Functions.CheckStr(objTransfer.Operador)),
                new DbParameter("v_presuscrito", DbType.Int32, ParameterDirection.Input, CSTS.Functions.CheckInt(objTransfer.Presuscrito)),
                new DbParameter("v_publicar", DbType.Double, ParameterDirection.Input, CSTS.Functions.CheckDbl(objTransfer.Publicar)),
                new DbParameter("v_ad_tmcode", DbType.String, 255, ParameterDirection.Input, CSTS.Functions.CheckStr(objTransfer.TmCode)),
                new DbParameter("v_lista_coser", DbType.String, 255, ParameterDirection.Input, CSTS.Functions.CheckStr(objTransfer.ListaCoSer)),
                new DbParameter("v_lista_spcode", DbType.String, 255, ParameterDirection.Input, CSTS.Functions.CheckStr(objTransfer.ListaSPCode)),
                new DbParameter("v_usuarioreg", DbType.String, 255, ParameterDirection.Input, CSTS.Functions.CheckStr(objTransfer.USRREGIS)),
                new DbParameter("v_cargo", DbType.Double, ParameterDirection.Input, CSTS.Functions.CheckDbl(objTransfer.Cargo)),
                new DbParameter("v_codsolot", DbType.Int64, ParameterDirection.Output),
                new DbParameter("p_error_code", DbType.Int64, ParameterDirection.Output),
                new DbParameter("p_error_msg", DbType.String, 500, ParameterDirection.Output)
            };

            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + strTransaction, string.Format("BELogNroVia - {0}", objTransfer.NroVia)); 

            try
            {
                Web.Logging.ExecuteMethod(strIdSession, strTransaction, () =>
                {
                    DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SGA_P_GENERA_TRANSACCION, parameters);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
            }
            finally
            {
                intNumSot = CSTS.Functions.CheckStr(parameters[parameters.Length - 3].Value);
                rstrResCod = CSTS.Functions.CheckStr(parameters[parameters.Length - 2].Value);
                rstrResDes = CSTS.Functions.CheckStr(parameters[parameters.Length - 1].Value);
            }
            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + strTransaction, "Fin a GetInsertTransaction Capa Data --> Parametros de salida intNumSot: " + intNumSot); // Temporal
            return CSTS.Functions.CheckStr(intNumSot);
        }

        public static List<Entity.Transac.Service.Fixed.ServiceByInteraction> GetServicesByInteraction(string strIdSession, string strTransaction, string idInteraccion)
        {
            List<Entity.Transac.Service.Fixed.ServiceByInteraction> list = null;

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_CODINTERAC", DbType.String,255, ParameterDirection.Input, idInteraccion),
                new DbParameter("CURSOR_SALIDA", DbType.Object, ParameterDirection.Output)                
            };

            list = DbFactory.ExecuteReader<List<Entity.Transac.Service.Fixed.ServiceByInteraction>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_CLARIFY, DbCommandConfiguration.SIACU_SP_QUERY_INTER_SERV_MP, parameters);

            return list;
        }
        #endregion

        public static Interaction GetCreateCase(Interaction oRequest)
        {
            Interaction oResponse = new Interaction();
            //InsertTemplateInteraction asdassa=new InsertTemplateInteraction() 
            try
            {

                DbParameter[] parameters =
                {   
                   #region Parametros InsertCaso
                                        new DbParameter("P_CONTACTOBJID_1", DbType.Int64,ParameterDirection.Input,oRequest.OBJID_CONTACTO),//"5628895"
				                        new DbParameter("P_SITEOBJID_1", DbType.Int64,ParameterDirection.Input,oRequest.OBJID_SITE),// "5628895"),
				                        new DbParameter("P_ACCOUNT", DbType.String,255,ParameterDirection.Input,oRequest.CUENTA),
				                        new DbParameter("P_PHONE", DbType.String,255,ParameterDirection.Input,oRequest.TELEFONO),
				                        new DbParameter("P_TIPO", DbType.String,255,ParameterDirection.Input,oRequest.TIPIFICACION),
				                        new DbParameter("P_CLASE", DbType.String,255,ParameterDirection.Input,oRequest.CLASE),
				                        new DbParameter("P_SUBCLASE", DbType.String,255,ParameterDirection.Input,oRequest.SUBCLASE),
				                        new DbParameter("P_METODO_CONTACTO", DbType.String,255,ParameterDirection.Input,oRequest.METODO),
				                        new DbParameter("P_PRIORIDAD", DbType.String,255,ParameterDirection.Input,oRequest.PRIORIDAD),
				                        new DbParameter("P_SEVERIDAD", DbType.String,255,ParameterDirection.Input,oRequest.SEVERIDAD),
				                        new DbParameter("P_COLA", DbType.String,255,ParameterDirection.Input,oRequest.COLA),
				                        new DbParameter("P_FLAG_INTERACT", DbType.String,255,ParameterDirection.Input,oRequest.FLAG_INTERACCION),
				                        new DbParameter("P_USR_PROCESO", DbType.String,255,ParameterDirection.Input,oRequest.USUARIO_PROCESO),
				                        new DbParameter("P_USUARIO", DbType.String,255,ParameterDirection.Input,oRequest.USUARIO_ID),
				                        new DbParameter("P_TIPO_INTER", DbType.String,255,ParameterDirection.Input,oRequest.TIPO_INTERACCION),
				                        new DbParameter("P_NOTAS", DbType.String,4000,ParameterDirection.Input,oRequest.NOTAS),
 				                        new DbParameter("P_SERVAFECT", DbType.String,255,ParameterDirection.Input,oRequest.SERVICIO),
 				                        new DbParameter("P_INCONVEN", DbType.String,255,ParameterDirection.Input,oRequest.INCONVENIENTE),
 				                        new DbParameter("P_SERVAFECT_CODE", DbType.String,255,ParameterDirection.Input,oRequest.SERVICIO_CODE),
 				                        new DbParameter("P_INCONVEN_CODE", DbType.String,255,ParameterDirection.Input,oRequest.INCONVENIENTE_CODE),
 				                        new DbParameter("P_CO_ID", DbType.String,255,ParameterDirection.Input,oRequest.CONTRATO),
 				                        new DbParameter("P_COD_PLANO", DbType.String,255,ParameterDirection.Input,oRequest.PLANO),
 				                        new DbParameter("P_VALOR1", DbType.String,255,ParameterDirection.Input,oRequest.VALOR_1),
 				                        new DbParameter("P_VALOR2", DbType.String,255,ParameterDirection.Input,oRequest.VALOR_2),
 				                        new DbParameter("P_DUMMY_ID", DbType.String,255,ParameterDirection.Input,oRequest.DUMMY_ID),
 				                        new DbParameter("P_CASE_FATHER", DbType.String,255,ParameterDirection.Input,oRequest.CASO_PADRE_ID),
				                        new DbParameter("ID_CASO", DbType.String,255,ParameterDirection.Output,oResponse.CASO_ID),
				                        new DbParameter("FLAG_CREACION", DbType.String,255,ParameterDirection.Output,oResponse.FLAG_INSERCION),
				                        new DbParameter("MSG_TEXT", DbType.String,255,ParameterDirection.Output,oResponse.RESULTADO),	
    #endregion
                };
                Web.Logging.ExecuteMethod(oRequest.Audit.Session, oRequest.Audit.Transaction, () =>
                {
                    DbFactory.ExecuteNonQuery(oRequest.Audit.Session, oRequest.Audit.Transaction, DbConnectionConfiguration.SIAC_POST_CLARIFY,
                        DbCommandConfiguration.SIACU_POST_CLARIFY_CREATE_CASE_HFC, parameters);

                }
                )
                ;
                oResponse.CASO_ID = parameters[26].Value.ToString();
                oResponse.FLAG_INSERCION = parameters[27].Value.ToString();
                oResponse.RESULTADO = parameters[28].Value.ToString();

            }
            catch (Exception ex)
            {
                Web.Logging.Error(oRequest.Audit.Session, oRequest.Audit.Transaction, ex.Message);
            }


            return oResponse;
        }

        public static EntitiesFixed.Interaction GetInsertCase(EntitiesFixed.Interaction oItem)
        {
            Interaction prueba = new Interaction();
            string FLAG_CREACION = string.Empty;
            string Message = string.Empty;
            string CasoId = string.Empty;

            EntitiesFixed.Interaction oResponse = new EntitiesFixed.Interaction();
            DbParameter[] parameters =
            {
												   new DbParameter("PN_CONTACTOBJID_1", DbType.Int64,ParameterDirection.Input,oItem.OBJID_CONTACTO),//"5628895"),//
												   new DbParameter("PV_SITEOBJID_1", DbType.Int64,ParameterDirection.Input,oItem.OBJID_SITE),//"5628895"),//
												   new DbParameter("PV_ACCOUNT", DbType.String,255,ParameterDirection.Input,oItem.CUENTA),
												   new DbParameter("PV_PHONE", DbType.String,255,ParameterDirection.Input,oItem.TELEFONO),//
												   new DbParameter("PV_TIPO", DbType.String,255,ParameterDirection.Input,oItem.TIPO),
												   new DbParameter("PV_CLASE", DbType.String,255,ParameterDirection.Input,oItem.CLASE),//
												   new DbParameter("PV_SUBCLASE", DbType.String,255,ParameterDirection.Input,oItem.SUBCLASE),//
												   new DbParameter("PV_METODO_CONTACTO", DbType.String,255,ParameterDirection.Input,oItem.METODO),//
												   new DbParameter("PV_PRIORIDAD", DbType.String,255,ParameterDirection.Input,oItem.PRIORIDAD),//
												   new DbParameter("PV_SEVERIDAD", DbType.String,255,ParameterDirection.Input,oItem.SEVERIDAD),//
												   new DbParameter("PV_COLA", DbType.String,255,ParameterDirection.Input,oItem.COLA),//
												   new DbParameter("PV_FLAG_INTERACT", DbType.String,255,ParameterDirection.Input,oItem.FLAG_INTERACCION),//
												   new DbParameter("PV_USR_PROCESO", DbType.String,255,ParameterDirection.Input,oItem.USUARIO_PROCESO),//
												   new DbParameter("PV_USUARIO", DbType.String,255,ParameterDirection.Input,oItem.USUARIO_ID),//
												   new DbParameter("PV_TIPO_INTER", DbType.String,255,ParameterDirection.Input,oItem.TIPO_INTERACCION),//
												   new DbParameter("PV_NOTAS", DbType.String,4000,ParameterDirection.Input,oItem.NOTAS),//
												   new DbParameter("ID_CASO", DbType.String,255,ParameterDirection.Output,CasoId),
												   new DbParameter("FLAG_CREACION", DbType.String,255,ParameterDirection.Output,FLAG_CREACION),
												   new DbParameter("MSG_TEXT", DbType.String,255,ParameterDirection.Output,Message)	
											   };


            #region parametros
            //for (int j = 0; j < parameters.Length; j++)
            //    parameters[j].Value = System.DBNull.Value;

            //int i = 0;
            //if (item.OBJID_CONTACTO != null)
            //    parameters[i].Value = 0;
            //i++;
            //if (item.OBJID_SITE != null)
            //    parameters[i].Value = Functions.CheckInt(item.OBJID_SITE);

            //i++;
            //if (item.CUENTA != null)
            //    parameters[i].Value = item.CUENTA;
            //i++;
            //if (item.TELEFONO != null)
            //    parameters[i].Value = item.TELEFONO;
            //i++;

            //if (item.TIPIFICACION != null)
            //    parameters[i].Value = item.TIPIFICACION;
            //i++;

            //if (item.CLASE != null)
            //    parameters[i].Value = item.CLASE;
            //i++;

            //if (item.SUBCLASE != null)
            //    parameters[i].Value = item.SUBCLASE;
            //i++;

            //if (item.METODO_CONTACTO != null)
            //    parameters[i].Value = item.METODO_CONTACTO;
            //i++;

            //if (item.PRIORIDAD != null)
            //    parameters[i].Value = item.PRIORIDAD;
            //i++;

            //if (item.SEVERIDAD != null)
            //    parameters[i].Value = item.SEVERIDAD;
            //i++;

            //if (item.COLA != null)
            //    parameters[i].Value = item.COLA;
            //i++;

            //if (item.FLAG_INTERACCION != null)
            //    parameters[i].Value = item.FLAG_INTERACCION;
            //i++;

            //if (item.USUARIO_PROCESO != null)
            //    parameters[i].Value = item.USUARIO_PROCESO;
            //i++;

            //if (item.USUARIO_ID != null)
            //    parameters[i].Value = item.USUARIO_ID;
            //i++;

            //if (item.TIPO_INTERACCION != null)
            //    parameters[i].Value = item.TIPO_INTERACCION;
            //i++;

            //if (item.NOTAS != null)
            //    parameters[i].Value = item.NOTAS;
            //i++;
            #endregion
            try
            {
                Web.Logging.ExecuteMethod(oItem.Audit.Session, oItem.Audit.Transaction, () =>
                {
                    DbFactory.ExecuteNonQuery(oItem.Audit.Session, oItem.Audit.Transaction, DbConnectionConfiguration.SIAC_POST_DB,
                        DbCommandConfiguration.SIACU_POST_COBS_INSERTAR_CASE, parameters);

                });
                oResponse.CASO_ID = parameters[16].Value.ToString();
                oResponse.FLAG_INSERCION = parameters[17].Value.ToString();
                oResponse.RESULTADO = parameters[18].Value.ToString();



            }
            catch (Exception ex)
            {
                Web.Logging.Error(oItem.Audit.Session, oItem.Audit.Transaction, ex.Message);
            }

            return oResponse;

        }

        public static EntitiesFixed.CaseTemplate GetInsertTemplateCase(EntitiesFixed.CaseTemplate oItem)
        {

            EntitiesFixed.CaseTemplate oResponse = new EntitiesFixed.CaseTemplate();


            string rFlagInsercion = string.Empty;
            DbParameter[] parameters =
            
            {
                   #region Imput
                    new DbParameter("P_NRO_CASO",DbType.String,80,ParameterDirection.Input,oItem.ID_CASO),
                    new DbParameter("P_CAS_1",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_1),
                    new DbParameter("P_CAS_2",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_2),
                    new DbParameter("P_CAS_3",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_3),
                    new DbParameter("P_CAS_4",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_4),
                    new DbParameter("P_CAS_5",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_5),
                    new DbParameter("P_CAS_6",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_6),
                    new DbParameter("P_CAS_7",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_7),
                    new DbParameter("P_CAS_8",DbType.Decimal,ParameterDirection.Input,oItem.X_CAS_8),
                    new DbParameter("P_CAS_9",DbType.Decimal,ParameterDirection.Input,oItem.X_CAS_9),
                    new DbParameter("P_CAS_10",DbType.Decimal,ParameterDirection.Input,oItem.X_CAS_10),
                    new DbParameter("P_CAS_11",DbType.Decimal,ParameterDirection.Input,oItem.X_CAS_11),
                    new DbParameter("P_CAS_12",DbType.Decimal,ParameterDirection.Input,oItem.X_CAS_12),
                    new DbParameter("P_CAS_13",DbType.Decimal,ParameterDirection.Input,oItem.X_CAS_13),
                    new DbParameter("P_CAS_14",DbType.Decimal,ParameterDirection.Input,oItem.X_CAS_14),
                    new DbParameter("P_CAS_15",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_15),
                    new DbParameter("P_CAS_16",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_16),
                    new DbParameter("P_CAS_17",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_17),
                    new DbParameter("P_CAS_18",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_18),
                    new DbParameter("P_CAS_19",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_19),
                    new DbParameter("P_CAS_20",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_20),
                    new DbParameter("P_CAS_21",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_21),
                    new DbParameter("P_CAS_22",DbType.Double,ParameterDirection.Input,oItem.X_CAS_22),
                    new DbParameter("P_CAS_23",DbType.Double,ParameterDirection.Input,oItem.X_CAS_23),
                    new DbParameter("P_CAS_24",DbType.Double,ParameterDirection.Input,oItem.X_CAS_24),
                    new DbParameter("P_CAS_25",DbType.Double,ParameterDirection.Input,oItem.X_CAS_25),
                    new DbParameter("P_CAS_26",DbType.Double,ParameterDirection.Input,oItem.X_CAS_26),
                    new DbParameter("P_CAS_27",DbType.Double,ParameterDirection.Input,oItem.X_CAS_27),
                    new DbParameter("P_CAS_28",DbType.Double,ParameterDirection.Input,oItem.X_CAS_28),
                    new DbParameter("P_CAS_29",DbType.String,255,ParameterDirection.Input,oItem.X_CAS_29),
                    new DbParameter("P_CAS_30",DbType.String,255,ParameterDirection.Input,oItem.X_CAS_30),
                    new DbParameter("P_SUSPENSION_DATE",DbType.DateTime,ParameterDirection.Input,oItem.X_SUSPENSION_DATE),
                    new DbParameter("P_REACTIVATION_DATE",DbType.DateTime,ParameterDirection.Input,oItem.X_REACTIVATION_DATE),
                    new DbParameter("P_SUSPENSION_QT",DbType.Double,ParameterDirection.Input,oItem.X_SUSPENSION_QT),
                    new DbParameter("P_CONCLUSIONS",DbType.String,255,ParameterDirection.Input,oItem.X_CONCLUSIONS),
                    new DbParameter("P_TEST_MADE",DbType.String,255,ParameterDirection.Input,oItem.X_TEST_MADE),
                    new DbParameter("P_PROBLEM_DESCRIPTION",DbType.String,255,ParameterDirection.Input,oItem.X_PROBLEM_DESCRIPTION),
                    new DbParameter("P_ADDRESS",DbType.String,255,ParameterDirection.Input,oItem.X_ADDRESS),
                    new DbParameter("P_DOCUMENT_NUMBER",DbType.String,20,ParameterDirection.Input,oItem.X_DOCUMENT_NUMBER),
                    new DbParameter("P_CALL_DURATION",DbType.String,50,ParameterDirection.Input,oItem.X_CALL_DURATION),
                    new DbParameter("P_CALL_COST",DbType.Double,ParameterDirection.Input,oItem.X_CALL_COST),
                    new DbParameter("P_SYSTEM_STATUS",DbType.String,80,ParameterDirection.Input,oItem.X_SYSTEM_STATUS),
                    new DbParameter("P_FLAG_VARIATION",DbType.String,1,ParameterDirection.Input,oItem.X_FLAG_VARIATION),
                    new DbParameter("P_SEARCH_DATE",DbType.DateTime,ParameterDirection.Input,oItem.X_SEARCH_DATE),
                    new DbParameter("P_VARIATION_DATE",DbType.DateTime,ParameterDirection.Input,oItem.X_VARIATION_DATE),
                    new DbParameter("P_LAST_QUERY",DbType.DateTime,ParameterDirection.Input,oItem.X_LAST_QUERY),
                    new DbParameter("P_PROBLEM_DATE",DbType.DateTime,ParameterDirection.Input,oItem.X_PROBLEM_DATE),
                    new DbParameter("P_PURCHASE_DATE",DbType.DateTime,ParameterDirection.Input,oItem.X_PURCHASE_DATE),
                    new DbParameter("P_RECHARGE_DATE",DbType.DateTime,ParameterDirection.Input,oItem.X_RECHARGE_DATE),
                    new DbParameter("P_REQUEST_DATE",DbType.DateTime,ParameterDirection.Input,oItem.X_REQUEST_DATE),
                    new DbParameter("P_REQUEST_PLACE",DbType.String,80,ParameterDirection.Input,oItem.X_REQUEST_PLACE),
                    new DbParameter("P_FLAG_GPRS",DbType.String,1,ParameterDirection.Input,oItem.X_FLAG_GPRS),
                    new DbParameter("P_COMPLAINT_AMOUNT",DbType.Double,ParameterDirection.Input,oItem.X_COMPLAINT_AMOUNT),
                    new DbParameter("P_LINES",DbType.String,20,ParameterDirection.Input,oItem.X_LINES),
                    new DbParameter("P_ERROR_MESSAGE",DbType.String,100,ParameterDirection.Input,oItem.X_ERROR_MESSAGE),
                    new DbParameter("P_MODEL",DbType.String,50,ParameterDirection.Input,oItem.X_MODEL),
                    new DbParameter("P_MARK",DbType.String,50,ParameterDirection.Input,oItem.X_MARK),
                    new DbParameter("P_BAND",DbType.String,50,ParameterDirection.Input,oItem.X_BAND),
                    new DbParameter("P_REPOSITION_REASON",DbType.String,80,ParameterDirection.Input,oItem.X_REPOSITION_REASON),
                    new DbParameter("P_CHURN_REASON",DbType.String,80,ParameterDirection.Input,oItem.X_CHURN_REASON),
                    new DbParameter("P_CELLULAR_NUMBER",DbType.String,80,ParameterDirection.Input,oItem.X_CELLULAR_NUMBER),
                    new DbParameter("P_CLARIFY_VARIATION",DbType.String,15,ParameterDirection.Input,oItem.X_CLARIFY_VARIATION),
                    new DbParameter("P_FLAG_SEND_RECEIVE",DbType.String,1,ParameterDirection.Input,oItem.X_FLAG_SEND_RECEIVE),
                    new DbParameter("P_CUSTOMER_NAME",DbType.String,80,ParameterDirection.Input,oItem.X_CUSTOMER_NAME),
                    new DbParameter("P_PREPAID_CARD_NUMBER",DbType.String,80,ParameterDirection.Input,oItem.X_PREPAID_CARD_NUMBER),
                    new DbParameter("P_NUMBERS_COMMUNICATION",DbType.String,80,ParameterDirection.Input,oItem.X_NUMBERS_COMMUNICATION),
                    new DbParameter("P_REFERENCE_NUMBER",DbType.String,20,ParameterDirection.Input,oItem.X_REFERENCE_NUMBER),
                    new DbParameter("P_OST_NUMBER",DbType.String,30,ParameterDirection.Input,oItem.X_OST_NUMBER),
                    new DbParameter("P_FRIENDS_FAMILY",DbType.String,80,ParameterDirection.Input,oItem.X_FRIENDS_FAMILY),
                    new DbParameter("P_COUNTRY_OPERATOR",DbType.String,80,ParameterDirection.Input,oItem.X_COUNTRY_OPERATOR),
                    new DbParameter("P_OLD_PLAN",DbType.String,80,ParameterDirection.Input,oItem.X_OLD_PLAN),
                    new DbParameter("P_NEW_PLAN",DbType.String,80,ParameterDirection.Input,oItem.X_NEW_PLAN),
                    new DbParameter("P_CURRENT_PLAN",DbType.String,80,ParameterDirection.Input,oItem.X_CURRENT_PLAN),
                    new DbParameter("P_CAMPAIGN",DbType.String,80,ParameterDirection.Input,oItem.X_CAMPAIGN),
                    new DbParameter("P_BILL_NUMBER",DbType.String,80,ParameterDirection.Input,oItem.X_BILL_NUMBER_COMPLAINT),
                    new DbParameter("P_REFERENCE_ADDRESS",DbType.String,100,ParameterDirection.Input,oItem.X_REFERENCE_ADDRESS),
                    new DbParameter("P_CURRENT_BALANCE",DbType.Double,ParameterDirection.Input,oItem.X_CURRENT_BALANCE),
                    new DbParameter("P_LAST_BALANCE",DbType.Double,ParameterDirection.Input,oItem.X_LAST_BALANCE),
                    new DbParameter("P_BALANCE_REQUESTED",DbType.Double,ParameterDirection.Input,oItem.X_BALANCE_REQUESTED),
                    new DbParameter("P_CUSTOMER_SEGMENT",DbType.String,30,ParameterDirection.Input,oItem.X_CUSTOMER_SEGMENT),
                    new DbParameter("P_SERVICE_PROBLEM",DbType.String,50,ParameterDirection.Input,oItem.X_SERVICE_PROBLEM),
                    new DbParameter("P_FLAG_OTHER_PROBLEMS",DbType.String,1,ParameterDirection.Input,oItem.X_FLAG_OTHER_PROBLEMS),
                    new DbParameter("P_OPERATOR_PROBLEM",DbType.String,80,ParameterDirection.Input,oItem.X_OPERATOR_PROBLEM),
                    new DbParameter("P_CONTACT_TIME_TERM",DbType.String,80,ParameterDirection.Input,oItem.X_CONTACT_TIME_TERM),
                    new DbParameter("P_STORE",DbType.String,80,ParameterDirection.Input,oItem.X_STORE),
                    new DbParameter("P_DIAL_TYPE",DbType.String,10,ParameterDirection.Input,oItem.X_DIAL_TYPE),
                    new DbParameter("P_PROBLEM_LOCATION",DbType.String,255,ParameterDirection.Input,oItem.X_PROBLEM_LOCATION),
                    new DbParameter("P_FLAG_ADDITIONAL_SERVICES",DbType.String,1,ParameterDirection.Input,oItem.X_FLAG_ADDITIONAL_SERVICES),
                    new DbParameter("P_FLAG_WAP",DbType.String,1,ParameterDirection.Input,oItem.X_FLAG_WAP),
                    new DbParameter("P_CLARO_LDN1",DbType.String,20,ParameterDirection.Input,oItem.X_CLARO_LDN1),
                    new DbParameter("P_CLARO_LDN2",DbType.String,20,ParameterDirection.Input,oItem.X_CLARO_LDN2),
                    new DbParameter("P_CLARO_LDN3",DbType.String,20,ParameterDirection.Input,oItem.X_CLARO_LDN3),
                    new DbParameter("P_CLARO_LDN4",DbType.String,20,ParameterDirection.Input,oItem.X_CLARO_LDN4),
                    new DbParameter("P_CLAROLOCAL1",DbType.String,20,ParameterDirection.Input,oItem.X_CLAROLOCAL1),
                    new DbParameter("P_CLAROLOCAL2",DbType.String,20,ParameterDirection.Input,oItem.X_CLAROLOCAL2),
                    new DbParameter("P_CLAROLOCAL3",DbType.String,20,ParameterDirection.Input,oItem.X_CLAROLOCAL3),
                    new DbParameter("P_CLAROLOCAL4",DbType.String,20,ParameterDirection.Input,oItem.X_CLAROLOCAL4),
                    new DbParameter("P_CLAROLOCAL5",DbType.String,20,ParameterDirection.Input,oItem.X_CLAROLOCAL5),
                    new DbParameter("P_CLAROLOCAL6",DbType.String,20,ParameterDirection.Input,oItem.X_CLAROLOCAL6),
                    new DbParameter("P_FIXED_NUMBER",DbType.String,20,ParameterDirection.Input,oItem.X_FIXED_NUMBER),
                    new DbParameter("P_LDI_NUMBER",DbType.String,20,ParameterDirection.Input,oItem.X_LDI_NUMBER),
                    new DbParameter("ID_CASO",DbType.String,50,ParameterDirection.Output,oItem.ID_CASO),
					new DbParameter("P_FLAG_CREACION", DbType.String,255,ParameterDirection.Output,oItem.FLAG_CREACION),
			   		new DbParameter("P_MSG_TEXT", DbType.String,255,ParameterDirection.Output,oItem.MESSAGE),
                    #endregion
			};

            try
            {
                Web.Logging.ExecuteMethod(oItem.Audit.Session, oItem.Audit.Transaction, () =>
                {
                    DbFactory.ExecuteNonQuery(oItem.Audit.Session, oItem.Audit.Transaction, DbConnectionConfiguration.SIAC_POST_CLARIFY,
                        DbCommandConfiguration.SIACU_PS_CREATE_PLUS_CASE, parameters);

                });


                oResponse.ID_CASO = parameters[101].Value.ToString();
                oResponse.FLAG_INSERCION = parameters[102].Value.ToString();
                oResponse.MESSAGE = parameters[103].Value.ToString();

            }
            catch (Exception ex)
            {
                Web.Logging.Error(oItem.Audit.Session, oItem.Audit.Transaction, ex.Message);
            }

            return oResponse;
        }


        public static EntitiesFixed.CaseTemplate GetInsertTemplateCaseContingent(EntitiesFixed.CaseTemplate oItem)
        {
            EntitiesFixed.CaseTemplate oResponse = new EntitiesFixed.CaseTemplate();
            string rFlagInsercion = string.Empty;
            string rMsgText = string.Empty;
            DbParameter[] parameters =
            
            {
             #region Input												   
                                                   new DbParameter("PN_SECUENCIAL",DbType.Double,ParameterDirection.Input,oItem.ID_CASO), 
                                                   new DbParameter("PV_NRO_CASO",DbType.String,80,ParameterDirection.Input,oItem.ID_CASO), 
                                                    new DbParameter("P_CAS_1",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_1),
                                                   new DbParameter("P_CAS_2",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_2),
                                                   new DbParameter("P_CAS_3",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_3),
                                                   new DbParameter("P_CAS_4",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_4),
                                                   new DbParameter("P_CAS_5",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_5),
                                                   new DbParameter("P_CAS_6",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_6),
                                                   new DbParameter("P_CAS_7",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_7),
                                                   new DbParameter("P_CAS_8",DbType.Decimal,ParameterDirection.Input,oItem.X_CAS_8),
                                                   new DbParameter("P_CAS_9",DbType.Decimal,ParameterDirection.Input,oItem.X_CAS_9),
                                                   new DbParameter("P_CAS_10",DbType.Decimal,ParameterDirection.Input,oItem.X_CAS_10),
                                                   new DbParameter("P_CAS_11",DbType.Decimal,ParameterDirection.Input,oItem.X_CAS_11),
                                                   new DbParameter("P_CAS_12",DbType.Decimal,ParameterDirection.Input,oItem.X_CAS_12),
                                                   new DbParameter("P_CAS_13",DbType.Decimal,ParameterDirection.Input,oItem.X_CAS_13),
                                                   new DbParameter("P_CAS_14",DbType.Decimal,ParameterDirection.Input,oItem.X_CAS_14),
                                                   new DbParameter("P_CAS_15",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_15),
                                                   new DbParameter("P_CAS_16",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_16),
                                                   new DbParameter("P_CAS_17",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_17),
                                                   new DbParameter("P_CAS_18",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_18),
                                                   new DbParameter("P_CAS_19",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_19),
                                                   new DbParameter("P_CAS_20",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_20),
                                                   new DbParameter("P_CAS_21",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_21),
                                                   new DbParameter("P_CAS_22",DbType.Double,ParameterDirection.Input,oItem.X_CAS_22),
                                                   new DbParameter("P_CAS_23",DbType.Double,ParameterDirection.Input,oItem.X_CAS_23),
                                                   new DbParameter("P_CAS_24",DbType.Double,ParameterDirection.Input,oItem.X_CAS_24),
                                                   new DbParameter("P_CAS_25",DbType.Double,ParameterDirection.Input,oItem.X_CAS_25),
                                                   new DbParameter("P_CAS_26",DbType.Double,ParameterDirection.Input,oItem.X_CAS_26),
                                                   new DbParameter("P_CAS_27",DbType.Double,ParameterDirection.Input,oItem.X_CAS_27),
                                                   new DbParameter("P_CAS_28",DbType.Double,ParameterDirection.Input,oItem.X_CAS_28),
                                                   new DbParameter("P_CAS_29",DbType.String,255,ParameterDirection.Input,oItem.X_CAS_29),
                                                   new DbParameter("P_CAS_30",DbType.String,255,ParameterDirection.Input,oItem.X_CAS_30),
                                                   new DbParameter("P_SUSPENSION_DATE",DbType.DateTime,ParameterDirection.Input,oItem.X_SUSPENSION_DATE),
                                                   new DbParameter("P_REACTIVATION_DATE",DbType.DateTime,ParameterDirection.Input,oItem.X_REACTIVATION_DATE),
                                                   new DbParameter("P_SUSPENSION_QT",DbType.Double,ParameterDirection.Input,oItem.X_SUSPENSION_QT),
                                                   new DbParameter("P_CONCLUSIONS",DbType.String,255,ParameterDirection.Input,oItem.X_CONCLUSIONS),
                                                   new DbParameter("P_TEST_MADE",DbType.String,255,ParameterDirection.Input,oItem.X_TEST_MADE),
                                                   new DbParameter("P_PROBLEM_DESCRIPTION",DbType.String,255,ParameterDirection.Input,oItem.X_PROBLEM_DESCRIPTION),
                                                   new DbParameter("P_ADDRESS",DbType.String,255,ParameterDirection.Input,oItem.X_ADDRESS),
                                                   new DbParameter("P_DOCUMENT_NUMBER",DbType.String,20,ParameterDirection.Input,oItem.X_DOCUMENT_NUMBER),
                                                   new DbParameter("P_CALL_DURATION",DbType.String,50,ParameterDirection.Input,oItem.X_CALL_DURATION),
                                                   new DbParameter("P_CALL_COST",DbType.Double,ParameterDirection.Input,oItem.X_CALL_COST),
                                                   new DbParameter("P_SYSTEM_STATUS",DbType.String,80,ParameterDirection.Input,oItem.X_SYSTEM_STATUS),
                                                   new DbParameter("P_FLAG_VARIATION",DbType.String,1,ParameterDirection.Input,oItem.X_FLAG_VARIATION),
                                                   new DbParameter("P_SEARCH_DATE",DbType.DateTime,ParameterDirection.Input,oItem.X_SEARCH_DATE),
                                                   new DbParameter("P_VARIATION_DATE",DbType.DateTime,ParameterDirection.Input,oItem.X_VARIATION_DATE),
                                                   new DbParameter("P_LAST_QUERY",DbType.DateTime,ParameterDirection.Input,oItem.X_LAST_QUERY),
                                                   new DbParameter("P_PROBLEM_DATE",DbType.DateTime,ParameterDirection.Input,oItem.X_PROBLEM_DATE),
                                                   new DbParameter("P_PURCHASE_DATE",DbType.DateTime,ParameterDirection.Input,oItem.X_PURCHASE_DATE),
                                                   new DbParameter("P_RECHARGE_DATE",DbType.DateTime,ParameterDirection.Input,oItem.X_RECHARGE_DATE),
                                                   new DbParameter("P_REQUEST_DATE",DbType.DateTime,ParameterDirection.Input,oItem.X_REQUEST_DATE),
                                                   new DbParameter("P_REQUEST_PLACE",DbType.String,80,ParameterDirection.Input,oItem.X_REQUEST_PLACE),
                                                   new DbParameter("P_FLAG_GPRS",DbType.String,1,ParameterDirection.Input,oItem.X_FLAG_GPRS),
                                                   new DbParameter("P_COMPLAINT_AMOUNT",DbType.Double,ParameterDirection.Input,oItem.X_COMPLAINT_AMOUNT),
                                                   new DbParameter("P_LINES",DbType.String,20,ParameterDirection.Input,oItem.X_LINES),
                                                   new DbParameter("P_ERROR_MESSAGE",DbType.String,100,ParameterDirection.Input,oItem.X_ERROR_MESSAGE),
                                                   new DbParameter("P_MODEL",DbType.String,50,ParameterDirection.Input,oItem.X_MODEL),
                                                   new DbParameter("P_MARK",DbType.String,50,ParameterDirection.Input,oItem.X_MARK),
                                                   new DbParameter("P_BAND",DbType.String,50,ParameterDirection.Input,oItem.X_BAND),
                                                   new DbParameter("P_REPOSITION_REASON",DbType.String,80,ParameterDirection.Input,oItem.X_REPOSITION_REASON),
                                                   new DbParameter("P_CHURN_REASON",DbType.String,80,ParameterDirection.Input,oItem.X_CHURN_REASON),
                                                   new DbParameter("P_CELLULAR_NUMBER",DbType.String,80,ParameterDirection.Input,oItem.X_CELLULAR_NUMBER),
                                                   new DbParameter("P_CLARIFY_VARIATION",DbType.String,15,ParameterDirection.Input,oItem.X_CLARIFY_VARIATION),
                                                   new DbParameter("P_FLAG_SEND_RECEIVE",DbType.String,1,ParameterDirection.Input,oItem.X_FLAG_SEND_RECEIVE),
                                                   new DbParameter("P_CUSTOMER_NAME",DbType.String,80,ParameterDirection.Input,oItem.X_CUSTOMER_NAME),
                                                   new DbParameter("P_PREPAID_CARD_NUMBER",DbType.String,80,ParameterDirection.Input,oItem.X_PREPAID_CARD_NUMBER),
                                                   new DbParameter("P_NUMBERS_COMMUNICATION",DbType.String,80,ParameterDirection.Input,oItem.X_NUMBERS_COMMUNICATION),
                                                   new DbParameter("P_REFERENCE_NUMBER",DbType.String,20,ParameterDirection.Input,oItem.X_REFERENCE_NUMBER),
                                                   new DbParameter("P_OST_NUMBER",DbType.String,30,ParameterDirection.Input,oItem.X_OST_NUMBER),
                                                   new DbParameter("P_FRIENDS_FAMILY",DbType.String,80,ParameterDirection.Input,oItem.X_FRIENDS_FAMILY),
                                                   new DbParameter("P_COUNTRY_OPERATOR",DbType.String,80,ParameterDirection.Input,oItem.X_COUNTRY_OPERATOR),
                                                   new DbParameter("P_OLD_PLAN",DbType.String,80,ParameterDirection.Input,oItem.X_OLD_PLAN),
                                                   new DbParameter("P_NEW_PLAN",DbType.String,80,ParameterDirection.Input,oItem.X_NEW_PLAN),
                                                   new DbParameter("P_CURRENT_PLAN",DbType.String,80,ParameterDirection.Input,oItem.X_CURRENT_PLAN),
                                                   new DbParameter("P_CAMPAIGN",DbType.String,80,ParameterDirection.Input,oItem.X_CAMPAIGN),
                                                   new DbParameter("P_BILL_NUMBER",DbType.String,80,ParameterDirection.Input,oItem.X_BILL_NUMBER_COMPLAINT),
                                                   new DbParameter("P_REFERENCE_ADDRESS",DbType.String,100,ParameterDirection.Input,oItem.X_REFERENCE_ADDRESS),
                                                   new DbParameter("P_CURRENT_BALANCE",DbType.Double,ParameterDirection.Input,oItem.X_CURRENT_BALANCE),
                                                   new DbParameter("P_LAST_BALANCE",DbType.Double,ParameterDirection.Input,oItem.X_LAST_BALANCE),
                                                   new DbParameter("P_BALANCE_REQUESTED",DbType.Double,ParameterDirection.Input,oItem.X_BALANCE_REQUESTED),
                                                   new DbParameter("P_CUSTOMER_SEGMENT",DbType.String,30,ParameterDirection.Input,oItem.X_CUSTOMER_SEGMENT),
                                                   new DbParameter("P_SERVICE_PROBLEM",DbType.String,50,ParameterDirection.Input,oItem.X_SERVICE_PROBLEM),
                                                   new DbParameter("P_FLAG_OTHER_PROBLEMS",DbType.String,1,ParameterDirection.Input,oItem.X_FLAG_OTHER_PROBLEMS),
                                                   new DbParameter("P_OPERATOR_PROBLEM",DbType.String,80,ParameterDirection.Input,oItem.X_OPERATOR_PROBLEM),
                                                   new DbParameter("P_CONTACT_TIME_TERM",DbType.String,80,ParameterDirection.Input,oItem.X_CONTACT_TIME_TERM),
                                                   new DbParameter("P_STORE",DbType.String,80,ParameterDirection.Input,oItem.X_STORE),
                                                   new DbParameter("P_DIAL_TYPE",DbType.String,10,ParameterDirection.Input,oItem.X_DIAL_TYPE),
                                                   new DbParameter("P_PROBLEM_LOCATION",DbType.String,255,ParameterDirection.Input,oItem.X_PROBLEM_LOCATION),
                                                   new DbParameter("P_FLAG_ADDITIONAL_SERVICES",DbType.String,1,ParameterDirection.Input,oItem.X_FLAG_ADDITIONAL_SERVICES),
                                                   new DbParameter("P_FLAG_WAP",DbType.String,1,ParameterDirection.Input,oItem.X_FLAG_WAP),
                                                   new DbParameter("P_CLARO_LDN1",DbType.String,20,ParameterDirection.Input,oItem.X_CLARO_LDN1),
                                                   new DbParameter("P_CLARO_LDN2",DbType.String,20,ParameterDirection.Input,oItem.X_CLARO_LDN2),
                                                   new DbParameter("P_CLARO_LDN3",DbType.String,20,ParameterDirection.Input,oItem.X_CLARO_LDN3),
                                                   new DbParameter("P_CLARO_LDN4",DbType.String,20,ParameterDirection.Input,oItem.X_CLARO_LDN4),
                                                   new DbParameter("P_CLAROLOCAL1",DbType.String,20,ParameterDirection.Input,oItem.X_CLAROLOCAL1),
                                                   new DbParameter("P_CLAROLOCAL2",DbType.String,20,ParameterDirection.Input,oItem.X_CLAROLOCAL2),
                                                   new DbParameter("P_CLAROLOCAL3",DbType.String,20,ParameterDirection.Input,oItem.X_CLAROLOCAL3),
                                                   new DbParameter("P_CLAROLOCAL4",DbType.String,20,ParameterDirection.Input,oItem.X_CLAROLOCAL4),
                                                   new DbParameter("P_CLAROLOCAL5",DbType.String,20,ParameterDirection.Input,oItem.X_CLAROLOCAL5),
                                                   new DbParameter("P_CLAROLOCAL6",DbType.String,20,ParameterDirection.Input,oItem.X_CLAROLOCAL6),
                                                   new DbParameter("P_FIXED_NUMBER",DbType.String,20,ParameterDirection.Input,oItem.X_FIXED_NUMBER),
                                                   new DbParameter("P_LDI_NUMBER",DbType.String,20,ParameterDirection.Input,oItem.X_LDI_NUMBER),
                                                   new DbParameter("ID_CASO",DbType.String,50,ParameterDirection.Output,oItem.ID_CASO),
                                                   new DbParameter("P_FLAG_CREACION", DbType.String,255,ParameterDirection.Output,oItem.FLAG_CREACION),
                                                   new DbParameter("P_MSG_TEXT", DbType.String,255,ParameterDirection.Output,oItem.MESSAGE),
        #endregion
			};

            #region Parametros
            //for (int j = 0; j < arrParam.Length; j++)
            //    arrParam[j].Value = System.DBNull.Value;

            //int i = 0;
            //DateTime dateInicio = new DateTime(1, 1, 1);

            //if (vCasoId != null)
            //    arrParam[i].Value = vCasoId;

            //i++;
            //if (vCasoId != null)
            //    arrParam[i].Value = vCasoId;

            //i++;
            //if (item.X_CAS_1 != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_CAS_1);

            //i++;
            //if (item.X_CAS_2 != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_CAS_2);

            //i++;
            //if (item.X_CAS_3 != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_CAS_3);

            //i++;
            //if (item.X_CAS_4 != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_CAS_4);

            //i++;
            //if (item.X_CAS_5 != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_CAS_5);

            //i++;
            //if (item.X_CAS_6 != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_CAS_6);

            //i++;
            //if (item.X_CAS_7 != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_CAS_7);

            //i++;
            //arrParam[i].Value = Funciones.CheckDbl(item.X_CAS_8);

            //i++;
            //arrParam[i].Value = Funciones.CheckDbl(item.X_CAS_9);

            //i++;
            //arrParam[i].Value = Funciones.CheckDbl(item.X_CAS_10);

            //i++;
            //arrParam[i].Value = Funciones.CheckDbl(item.X_CAS_11);

            //i++;
            //arrParam[i].Value = Funciones.CheckDbl(item.X_CAS_12);

            //i++;
            //arrParam[i].Value = Funciones.CheckDbl(item.X_CAS_13);

            //i++;
            //arrParam[i].Value = Funciones.CheckDbl(item.X_CAS_14);

            //i++;
            //if (item.X_CAS_15 != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_CAS_15);

            //i++;
            //if (item.X_CAS_16 != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_CAS_16);

            //i++;
            //if (item.X_CAS_17 != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_CAS_17);

            //i++;
            //if (item.X_CAS_18 != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_CAS_18);

            //i++;
            //if (item.X_CAS_19 != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_CAS_19);

            //i++;
            //if (item.X_CAS_20 != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_CAS_20);

            //i++;
            //if (item.X_CAS_21 != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_CAS_21);

            //i++;
            //arrParam[i].Value = Funciones.CheckDbl(item.X_CAS_22);

            //i++;
            //arrParam[i].Value = Funciones.CheckDbl(item.X_CAS_23);

            //i++;
            //arrParam[i].Value = Funciones.CheckDbl(item.X_CAS_24);

            //i++;
            //arrParam[i].Value = Funciones.CheckDbl(item.X_CAS_25);

            //i++;
            //arrParam[i].Value = Funciones.CheckDbl(item.X_CAS_26);

            //i++;
            //arrParam[i].Value = Funciones.CheckDbl(item.X_CAS_27);

            //i++;
            //arrParam[i].Value = Funciones.CheckDbl(item.X_CAS_28);

            //i++;
            //if (item.X_CAS_29 != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_CAS_29);

            //i++;
            //if (item.X_CAS_30 != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_CAS_30);

            //i++;
            //if (item.X_SUSPENSION_DATE != dateInicio)
            //    arrParam[i].Value = Funciones.CheckDate(item.X_SUSPENSION_DATE);

            //i++;
            //if (item.X_REACTIVATION_DATE != dateInicio)
            //    arrParam[i].Value = Funciones.CheckDate(item.X_REACTIVATION_DATE);

            //i++;
            //arrParam[i].Value = Funciones.CheckDbl(item.X_SUSPENSION_QT);

            //i++;
            //if (item.X_CONCLUSIONS != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_CONCLUSIONS);
            //i++;
            //if (item.X_TEST_MADE != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_TEST_MADE);

            //i++;
            //if (item.X_PROBLEM_DESCRIPTION != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_PROBLEM_DESCRIPTION);

            //i++;
            //if (item.X_ADDRESS != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_ADDRESS);

            //i++;
            //if (item.X_DOCUMENT_NUMBER != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_DOCUMENT_NUMBER);

            //i++;
            //if (item.X_CALL_DURATION != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_CALL_DURATION);

            //i++;
            //arrParam[i].Value = Funciones.CheckDbl(item.X_CALL_COST);

            //i++;
            //if (item.X_SYSTEM_STATUS != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_SYSTEM_STATUS);

            //i++;
            //if (item.X_FLAG_VARIATION != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_FLAG_VARIATION);

            //i++;
            //if (item.X_SEARCH_DATE != dateInicio)
            //    arrParam[i].Value = Funciones.CheckDate(item.X_SEARCH_DATE);

            //i++;
            //if (item.X_VARIATION_DATE != dateInicio)
            //    arrParam[i].Value = Funciones.CheckDate(item.X_VARIATION_DATE);

            //i++;
            //if (item.X_LAST_QUERY != dateInicio)
            //    arrParam[i].Value = Funciones.CheckDate(item.X_LAST_QUERY);

            //i++;
            //if (item.X_PROBLEM_DATE != dateInicio)
            //    arrParam[i].Value = Funciones.CheckDate(item.X_PROBLEM_DATE);

            //i++;
            //if (item.X_PURCHASE_DATE != dateInicio)
            //    arrParam[i].Value = Funciones.CheckDate(item.X_PURCHASE_DATE);

            //i++;
            //if (item.X_RECHARGE_DATE != dateInicio)
            //    arrParam[i].Value = Funciones.CheckDate(item.X_RECHARGE_DATE);

            //i++;
            //if (item.X_REQUEST_DATE != dateInicio)
            //    arrParam[i].Value = Funciones.CheckDate(item.X_REQUEST_DATE);

            //i++;
            //if (item.X_REQUEST_PLACE != null)
            //    arrParam[i].Value = Funciones.CheckDate(item.X_REQUEST_PLACE);

            //i++;
            //if (item.X_FLAG_GPRS != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_FLAG_GPRS);

            //i++;
            //arrParam[i].Value = Funciones.CheckDbl(item.X_COMPLAINT_AMOUNT);

            //i++;
            //if (item.X_LINES != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_LINES);

            //i++;
            //if (item.X_ERROR_MESSAGE != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_ERROR_MESSAGE);

            //i++;
            //if (item.X_MODEL != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_MODEL);

            //i++;
            //if (item.X_MARK != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_MARK);

            //i++;
            //if (item.X_BAND != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_BAND);

            //i++;
            //if (item.X_REPOSITION_REASON != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_REPOSITION_REASON);

            //i++;
            //if (item.X_CHURN_REASON != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_CHURN_REASON);

            //i++;
            //if (item.X_CELLULAR_NUMBER != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_CELLULAR_NUMBER);

            //i++;
            //if (item.X_CLARIFY_VARIATION != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_CLARIFY_VARIATION);

            //i++;
            //if (item.X_FLAG_SEND_RECEIVE != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_FLAG_SEND_RECEIVE);

            //i++;
            //if (item.X_CUSTOMER_NAME != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_CUSTOMER_NAME);

            //i++;
            //if (item.X_PREPAID_CARD_NUMBER != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_PREPAID_CARD_NUMBER);

            //i++;
            //if (item.X_NUMBERS_COMMUNICATION != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_NUMBERS_COMMUNICATION);

            //i++;
            //if (item.X_REFERENCE_NUMBER != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_REFERENCE_NUMBER);

            //i++;
            //if (item.X_OST_NUMBER != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_OST_NUMBER);

            //i++;
            //if (item.X_FRIENDS_FAMILY != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_FRIENDS_FAMILY);

            //i++;
            //if (item.X_COUNTRY_OPERATOR != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_COUNTRY_OPERATOR);

            //i++;
            //if (item.X_OLD_PLAN != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_OLD_PLAN);

            //i++;
            //if (item.X_NEW_PLAN != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_NEW_PLAN);

            //i++;
            //if (item.X_CURRENT_PLAN != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_CURRENT_PLAN);

            //i++;
            //if (item.X_CAMPAIGN != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_CAMPAIGN);

            //i++;
            //if (item.X_BILL_NUMBER_COMPLAINT != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_BILL_NUMBER_COMPLAINT);

            //i++;
            //if (item.X_REFERENCE_ADDRESS != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_REFERENCE_ADDRESS);

            //i++;
            //arrParam[i].Value = Funciones.CheckDbl(item.X_CURRENT_BALANCE);

            //i++;
            //arrParam[i].Value = Funciones.CheckDbl(item.X_LAST_BALANCE);

            //i++;
            //arrParam[i].Value = Funciones.CheckDbl(item.X_BALANCE_REQUESTED);

            //i++;
            //if (item.X_CUSTOMER_SEGMENT != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_CUSTOMER_SEGMENT);

            //i++;
            //if (item.X_SERVICE_PROBLEM != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_SERVICE_PROBLEM);

            //i++;
            //if (item.X_FLAG_OTHER_PROBLEMS != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_FLAG_OTHER_PROBLEMS);

            //i++;
            //if (item.X_OPERATOR_PROBLEM != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_OPERATOR_PROBLEM);

            //i++;
            //if (item.X_CONTACT_TIME_TERM != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_CONTACT_TIME_TERM);

            //i++;
            //if (item.X_STORE != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_STORE);

            //i++;
            //if (item.X_DIAL_TYPE != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_DIAL_TYPE);

            //i++;
            //if (item.X_PROBLEM_LOCATION != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_PROBLEM_LOCATION);

            //i++;
            //if (item.X_FLAG_ADDITIONAL_SERVICES != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_FLAG_ADDITIONAL_SERVICES);

            //i++;
            //if (item.X_FLAG_WAP != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_FLAG_WAP);

            //i++;
            //if (item.X_CLARO_LDN1 != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_CLARO_LDN1);
            //i++;
            //if (item.X_CLARO_LDN2 != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_CLARO_LDN2);
            //i++;
            //if (item.X_CLARO_LDN3 != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_CLARO_LDN3);
            //i++;
            //if (item.X_CLARO_LDN4 != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_CLARO_LDN4);

            //i++;
            //if (item.X_CLAROLOCAL1 != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_CLAROLOCAL1);

            //i++;
            //if (item.X_CLAROLOCAL2 != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_CLAROLOCAL2);

            //i++;
            //if (item.X_CLAROLOCAL3 != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_CLAROLOCAL3);

            //i++;
            //if (item.X_CLAROLOCAL4 != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_CLAROLOCAL4);

            //i++;
            //if (item.X_CLAROLOCAL5 != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_CLAROLOCAL5);

            //i++;
            //if (item.X_CLAROLOCAL6 != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_CLAROLOCAL6);

            //i++;
            //if (item.X_FIXED_NUMBER != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_FIXED_NUMBER);

            //i++;
            //if (item.X_LDI_NUMBER != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_LDI_NUMBER);

            #endregion

            try
            {
                Web.Logging.ExecuteMethod(oItem.Audit.Session, oItem.Audit.Transaction, () =>
                {
                    DbFactory.ExecuteNonQuery(oItem.Audit.Session, oItem.Audit.Transaction, DbConnectionConfiguration.SIAC_POST_DB,
                        DbCommandConfiguration.SIACU_POST_INSERTAR_X_PLUS_CASE, parameters);

                });


                oResponse.ID_CASO = parameters[102].Value.ToString();
                oResponse.FLAG_INSERCION = parameters[103].Value.ToString();
                oResponse.MESSAGE = parameters[104].Value.ToString();

            }
            catch (Exception ex)
            {
                Web.Logging.Error(oItem.Audit.Session, oItem.Audit.Transaction, ex.Message);
            }

            return oResponse;
        }


        public static EntitiesFixed.CaseTemplate ActualizaPlantillaCaso(EntitiesFixed.CaseTemplate oItem)
        {
            EntitiesFixed.CaseTemplate oResponse = new EntitiesFixed.CaseTemplate();
            DbParameter[] parameters = 
            {
                #region Parametros
                                                   new DbParameter("PV_NRO_CASO",DbType.String,80,ParameterDirection.Input,oItem.ID_CASO), 
                                                    new DbParameter("P_CAS_1",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_1),
                                                   new DbParameter("P_CAS_2",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_2),
                                                   new DbParameter("P_CAS_3",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_3),
                                                   new DbParameter("P_CAS_4",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_4),
                                                   new DbParameter("P_CAS_5",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_5),
                                                   new DbParameter("P_CAS_6",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_6),
                                                   new DbParameter("P_CAS_7",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_7),
                                                   new DbParameter("P_CAS_8",DbType.Decimal,ParameterDirection.Input,oItem.X_CAS_8),
                                                   new DbParameter("P_CAS_9",DbType.Decimal,ParameterDirection.Input,oItem.X_CAS_9),
                                                   new DbParameter("P_CAS_10",DbType.Decimal,ParameterDirection.Input,oItem.X_CAS_10),
                                                   new DbParameter("P_CAS_11",DbType.Decimal,ParameterDirection.Input,oItem.X_CAS_11),
                                                   new DbParameter("P_CAS_12",DbType.Decimal,ParameterDirection.Input,oItem.X_CAS_12),
                                                   new DbParameter("P_CAS_13",DbType.Decimal,ParameterDirection.Input,oItem.X_CAS_13),
                                                   new DbParameter("P_CAS_14",DbType.Decimal,ParameterDirection.Input,oItem.X_CAS_14),
                                                   new DbParameter("P_CAS_15",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_15),
                                                   new DbParameter("P_CAS_16",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_16),
                                                   new DbParameter("P_CAS_17",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_17),
                                                   new DbParameter("P_CAS_18",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_18),
                                                   new DbParameter("P_CAS_19",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_19),
                                                   new DbParameter("P_CAS_20",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_20),
                                                   new DbParameter("P_CAS_21",DbType.String,80,ParameterDirection.Input,oItem.X_CAS_21),
                                                   new DbParameter("P_CAS_22",DbType.Double,ParameterDirection.Input,oItem.X_CAS_22),
                                                   new DbParameter("P_CAS_23",DbType.Double,ParameterDirection.Input,oItem.X_CAS_23),
                                                   new DbParameter("P_CAS_24",DbType.Double,ParameterDirection.Input,oItem.X_CAS_24),
                                                   new DbParameter("P_CAS_25",DbType.Double,ParameterDirection.Input,oItem.X_CAS_25),
                                                   new DbParameter("P_CAS_26",DbType.Double,ParameterDirection.Input,oItem.X_CAS_26),
                                                   new DbParameter("P_CAS_27",DbType.Double,ParameterDirection.Input,oItem.X_CAS_27),
                                                   new DbParameter("P_CAS_28",DbType.Double,ParameterDirection.Input,oItem.X_CAS_28),
                                                   new DbParameter("P_CAS_29",DbType.String,255,ParameterDirection.Input,oItem.X_CAS_29),
                                                   new DbParameter("P_CAS_30",DbType.String,255,ParameterDirection.Input,oItem.X_CAS_30),												   new DbParameter("P_SUSPENSION_DATE",DbType.DateTime,ParameterDirection.Input),
												   new DbParameter("P_REACTIVATION_DATE",DbType.DateTime,ParameterDirection.Input,oItem.X_REACTIVATION_DATE),
												   new DbParameter("P_SUSPENSION_QT",DbType.Double,ParameterDirection.Input,oItem.X_SUSPENSION_QT),
                                                   new DbParameter("P_CONCLUSIONS",DbType.String,255,ParameterDirection.Input,oItem.X_CONCLUSIONS),
                                                   new DbParameter("P_TEST_MADE",DbType.String,255,ParameterDirection.Input,oItem.X_TEST_MADE),
                                                   new DbParameter("P_PROBLEM_DESCRIPTION",DbType.String,255,ParameterDirection.Input,oItem.X_PROBLEM_DESCRIPTION),
                                                   new DbParameter("P_ADDRESS",DbType.String,255,ParameterDirection.Input,oItem.X_ADDRESS),
                                                   new DbParameter("P_DOCUMENT_NUMBER",DbType.String,20,ParameterDirection.Input,oItem.X_DOCUMENT_NUMBER),
                                                   new DbParameter("P_CALL_DURATION",DbType.String,50,ParameterDirection.Input,oItem.X_CALL_DURATION),
                                                   new DbParameter("P_CALL_COST",DbType.Double,ParameterDirection.Input,oItem.X_CALL_COST),
                                                   new DbParameter("P_SYSTEM_STATUS",DbType.String,80,ParameterDirection.Input,oItem.X_SYSTEM_STATUS),
                                                   new DbParameter("P_FLAG_VARIATION",DbType.String,1,ParameterDirection.Input,oItem.X_FLAG_VARIATION),
                                                   new DbParameter("P_SEARCH_DATE",DbType.DateTime,ParameterDirection.Input,oItem.X_SEARCH_DATE),
                                                   new DbParameter("P_VARIATION_DATE",DbType.DateTime,ParameterDirection.Input,oItem.X_VARIATION_DATE),
                                                   new DbParameter("P_LAST_QUERY",DbType.DateTime,ParameterDirection.Input,oItem.X_LAST_QUERY),
                                                   new DbParameter("P_PROBLEM_DATE",DbType.DateTime,ParameterDirection.Input,oItem.X_PROBLEM_DATE),
                                                   new DbParameter("P_PURCHASE_DATE",DbType.DateTime,ParameterDirection.Input,oItem.X_PURCHASE_DATE),
                                                   new DbParameter("P_RECHARGE_DATE",DbType.DateTime,ParameterDirection.Input,oItem.X_RECHARGE_DATE),
                                                   new DbParameter("P_REQUEST_DATE",DbType.DateTime,ParameterDirection.Input,oItem.X_REQUEST_DATE),
                                                   new DbParameter("P_REQUEST_PLACE",DbType.String,80,ParameterDirection.Input,oItem.X_REQUEST_PLACE),
                                                   new DbParameter("P_FLAG_GPRS",DbType.String,1,ParameterDirection.Input,oItem.X_FLAG_GPRS),
                                                   new DbParameter("P_COMPLAINT_AMOUNT",DbType.Double,ParameterDirection.Input,oItem.X_COMPLAINT_AMOUNT),
                                                   new DbParameter("P_LINES",DbType.String,20,ParameterDirection.Input,oItem.X_LINES),
                                                   new DbParameter("P_ERROR_MESSAGE",DbType.String,100,ParameterDirection.Input,oItem.X_ERROR_MESSAGE),
                                                   new DbParameter("P_MODEL",DbType.String,50,ParameterDirection.Input,oItem.X_MODEL),
                                                   new DbParameter("P_MARK",DbType.String,50,ParameterDirection.Input,oItem.X_MARK),
                                                   new DbParameter("P_BAND",DbType.String,50,ParameterDirection.Input,oItem.X_BAND),
                                                   new DbParameter("P_REPOSITION_REASON",DbType.String,80,ParameterDirection.Input,oItem.X_REPOSITION_REASON),
                                                   new DbParameter("P_CHURN_REASON",DbType.String,80,ParameterDirection.Input,oItem.X_CHURN_REASON),
                                                   new DbParameter("P_CELLULAR_NUMBER",DbType.String,80,ParameterDirection.Input,oItem.X_CELLULAR_NUMBER),
                                                   new DbParameter("P_CLARIFY_VARIATION",DbType.String,15,ParameterDirection.Input,oItem.X_CLARIFY_VARIATION),
                                                   new DbParameter("P_FLAG_SEND_RECEIVE",DbType.String,1,ParameterDirection.Input,oItem.X_FLAG_SEND_RECEIVE),
                                                   new DbParameter("P_CUSTOMER_NAME",DbType.String,80,ParameterDirection.Input,oItem.X_CUSTOMER_NAME),
                                                   new DbParameter("P_PREPAID_CARD_NUMBER",DbType.String,80,ParameterDirection.Input,oItem.X_PREPAID_CARD_NUMBER),
                                                   new DbParameter("P_NUMBERS_COMMUNICATION",DbType.String,80,ParameterDirection.Input,oItem.X_NUMBERS_COMMUNICATION),
                                                   new DbParameter("P_REFERENCE_NUMBER",DbType.String,20,ParameterDirection.Input,oItem.X_REFERENCE_NUMBER),												   
                                                   new DbParameter("P_OST_NUMBER",DbType.String,30,ParameterDirection.Input,oItem.X_OST_NUMBER),
                                                   new DbParameter("P_FRIENDS_FAMILY",DbType.String,80,ParameterDirection.Input,oItem.X_FRIENDS_FAMILY),
                                                   new DbParameter("P_COUNTRY_OPERATOR",DbType.String,80,ParameterDirection.Input,oItem.X_COUNTRY_OPERATOR),
                                                   new DbParameter("P_OLD_PLAN",DbType.String,80,ParameterDirection.Input,oItem.X_OLD_PLAN),
                                                   new DbParameter("P_NEW_PLAN",DbType.String,80,ParameterDirection.Input,oItem.X_NEW_PLAN),
                                                   new DbParameter("P_CURRENT_PLAN",DbType.String,80,ParameterDirection.Input,oItem.X_CURRENT_PLAN),
                                                   new DbParameter("P_CAMPAIGN",DbType.String,80,ParameterDirection.Input,oItem.X_CAMPAIGN),
                                                   new DbParameter("P_BILL_NUMBER",DbType.String,80,ParameterDirection.Input,oItem.X_BILL_NUMBER_COMPLAINT),
                                                   new DbParameter("P_REFERENCE_ADDRESS",DbType.String,100,ParameterDirection.Input,oItem.X_REFERENCE_ADDRESS),
                                                   new DbParameter("P_CURRENT_BALANCE",DbType.Double,ParameterDirection.Input,oItem.X_CURRENT_BALANCE),
                                                   new DbParameter("P_LAST_BALANCE",DbType.Double,ParameterDirection.Input,oItem.X_LAST_BALANCE),
                                                   new DbParameter("P_BALANCE_REQUESTED",DbType.Double,ParameterDirection.Input,oItem.X_BALANCE_REQUESTED),
                                                   new DbParameter("P_CUSTOMER_SEGMENT",DbType.String,30,ParameterDirection.Input,oItem.X_CUSTOMER_SEGMENT),
                                                   new DbParameter("P_SERVICE_PROBLEM",DbType.String,50,ParameterDirection.Input,oItem.X_SERVICE_PROBLEM),
                                                   new DbParameter("P_FLAG_OTHER_PROBLEMS",DbType.String,1,ParameterDirection.Input,oItem.X_FLAG_OTHER_PROBLEMS),
                                                   new DbParameter("P_OPERATOR_PROBLEM",DbType.String,80,ParameterDirection.Input,oItem.X_OPERATOR_PROBLEM),
                                                   new DbParameter("P_CONTACT_TIME_TERM",DbType.String,80,ParameterDirection.Input,oItem.X_CONTACT_TIME_TERM),
                                                   new DbParameter("P_STORE",DbType.String,80,ParameterDirection.Input,oItem.X_STORE),
                                                   new DbParameter("P_DIAL_TYPE",DbType.String,10,ParameterDirection.Input,oItem.X_DIAL_TYPE),
                                                   new DbParameter("P_PROBLEM_LOCATION",DbType.String,255,ParameterDirection.Input,oItem.X_PROBLEM_LOCATION),
                                                   new DbParameter("P_FLAG_ADDITIONAL_SERVICES",DbType.String,1,ParameterDirection.Input,oItem.X_FLAG_ADDITIONAL_SERVICES),
                                                   new DbParameter("P_FLAG_WAP",DbType.String,1,ParameterDirection.Input,oItem.X_FLAG_WAP),
                                                   new DbParameter("P_CLARO_LDN1",DbType.String,20,ParameterDirection.Input,oItem.X_CLARO_LDN1),
                                                   new DbParameter("P_CLARO_LDN2",DbType.String,20,ParameterDirection.Input,oItem.X_CLARO_LDN2),
                                                   new DbParameter("P_CLARO_LDN3",DbType.String,20,ParameterDirection.Input,oItem.X_CLARO_LDN3),
                                                   new DbParameter("P_CLARO_LDN4",DbType.String,20,ParameterDirection.Input,oItem.X_CLARO_LDN4),
                                                   new DbParameter("P_CLAROLOCAL1",DbType.String,20,ParameterDirection.Input,oItem.X_CLAROLOCAL1),
                                                   new DbParameter("P_CLAROLOCAL2",DbType.String,20,ParameterDirection.Input,oItem.X_CLAROLOCAL2),
                                                   new DbParameter("P_CLAROLOCAL3",DbType.String,20,ParameterDirection.Input,oItem.X_CLAROLOCAL3),
                                                   new DbParameter("P_CLAROLOCAL4",DbType.String,20,ParameterDirection.Input,oItem.X_CLAROLOCAL4),
                                                   new DbParameter("P_CLAROLOCAL5",DbType.String,20,ParameterDirection.Input,oItem.X_CLAROLOCAL5),
                                                   new DbParameter("P_CLAROLOCAL6",DbType.String,20,ParameterDirection.Input,oItem.X_CLAROLOCAL6),
                                                   new DbParameter("P_FIXED_NUMBER",DbType.String,20,ParameterDirection.Input,oItem.X_FIXED_NUMBER),
                                                   new DbParameter("P_LDI_NUMBER",DbType.String,20,ParameterDirection.Input,oItem.X_LDI_NUMBER),
                                                   new DbParameter("ID_CASO",DbType.String,50,ParameterDirection.Output,oItem.ID_CASO),
												   new DbParameter("P_FLAG_ACTUALIZACION", DbType.String,255,ParameterDirection.Output,oItem.FLAG_ACTUALIZACION),
												   new DbParameter("P_MSG_TEXT", DbType.String,255,ParameterDirection.Output,oItem.MESSAGE),
#endregion
            };

            #region Parametro
            //for (int j = 0; j < arrParam.Length; j++)
            //    arrParam[j].Value = System.DBNull.Value;

            //int i = 0;
            //DateTime dateInicio = new DateTime(1, 1, 1);

            //if (vCasoId != null)
            //    arrParam[i].Value = vCasoId;

            //i++;
            //if (item.X_CAS_1 != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_CAS_1);

            //i++;
            //if (item.X_CAS_2 != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_CAS_2);

            //i++;
            //if (item.X_CAS_3 != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_CAS_3);

            //i++;
            //if (item.X_CAS_4 != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_CAS_4);

            //i++;
            //if (item.X_CAS_5 != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_CAS_5);

            //i++;
            //if (item.X_CAS_6 != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_CAS_6);

            //i++;
            //if (item.X_CAS_7 != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_CAS_7);

            //i++;
            //arrParam[i].Value = Funciones.CheckDbl(item.X_CAS_8);

            //i++;
            //arrParam[i].Value = Funciones.CheckDbl(item.X_CAS_9);

            //i++;
            //arrParam[i].Value = Funciones.CheckDbl(item.X_CAS_10);

            //i++;
            //arrParam[i].Value = Funciones.CheckDbl(item.X_CAS_11);

            //i++;
            //arrParam[i].Value = Funciones.CheckDbl(item.X_CAS_12);

            //i++;
            //arrParam[i].Value = Funciones.CheckDbl(item.X_CAS_13);

            //i++;
            //arrParam[i].Value = Funciones.CheckDbl(item.X_CAS_14);

            //i++;
            //if (item.X_CAS_15 != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_CAS_15);

            //i++;
            //if (item.X_CAS_16 != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_CAS_16);

            //i++;
            //if (item.X_CAS_17 != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_CAS_17);

            //i++;
            //if (item.X_CAS_18 != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_CAS_18);

            //i++;
            //if (item.X_CAS_19 != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_CAS_19);

            //i++;
            //if (item.X_CAS_20 != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_CAS_20);

            //i++;
            //if (item.X_CAS_21 != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_CAS_21);

            //i++;
            //arrParam[i].Value = Funciones.CheckDbl(item.X_CAS_22);

            //i++;
            //arrParam[i].Value = Funciones.CheckDbl(item.X_CAS_23);

            //i++;
            //arrParam[i].Value = Funciones.CheckDbl(item.X_CAS_24);

            //i++;
            //arrParam[i].Value = Funciones.CheckDbl(item.X_CAS_25);

            //i++;
            //arrParam[i].Value = Funciones.CheckDbl(item.X_CAS_26);

            //i++;
            //arrParam[i].Value = Funciones.CheckDbl(item.X_CAS_27);

            //i++;
            //arrParam[i].Value = Funciones.CheckDbl(item.X_CAS_28);

            //i++;
            //if (item.X_CAS_29 != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_CAS_29);

            //i++;
            //if (item.X_CAS_30 != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_CAS_30);

            //i++;
            //if (item.X_SUSPENSION_DATE != dateInicio)
            //    arrParam[i].Value = Funciones.CheckDate(item.X_SUSPENSION_DATE);

            //i++;
            //if (item.X_REACTIVATION_DATE != dateInicio)
            //    arrParam[i].Value = Funciones.CheckDate(item.X_REACTIVATION_DATE);

            //i++;
            //arrParam[i].Value = Funciones.CheckDbl(item.X_SUSPENSION_QT);

            //i++;
            //if (item.X_CONCLUSIONS != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_CONCLUSIONS);

            //i++;
            //if (item.X_TEST_MADE != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_TEST_MADE);

            //i++;
            //if (item.X_PROBLEM_DESCRIPTION != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_PROBLEM_DESCRIPTION);

            //i++;
            //if (item.X_ADDRESS != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_ADDRESS);

            //i++;
            //if (item.X_DOCUMENT_NUMBER != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_DOCUMENT_NUMBER);

            //i++;
            //if (item.X_CALL_DURATION != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_CALL_DURATION);

            //i++;
            //arrParam[i].Value = Funciones.CheckDbl(item.X_CALL_COST);

            //i++;
            //if (item.X_SYSTEM_STATUS != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_SYSTEM_STATUS);

            //i++;
            //if (item.X_FLAG_VARIATION != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_FLAG_VARIATION);

            //i++;
            //if (item.X_SEARCH_DATE != dateInicio)
            //    arrParam[i].Value = Funciones.CheckDate(item.X_SEARCH_DATE);

            //i++;
            //if (item.X_VARIATION_DATE != dateInicio)
            //    arrParam[i].Value = Funciones.CheckDate(item.X_VARIATION_DATE);

            //i++;
            //if (item.X_LAST_QUERY != dateInicio)
            //    arrParam[i].Value = Funciones.CheckDate(item.X_LAST_QUERY);

            //i++;
            //if (item.X_PROBLEM_DATE != dateInicio)
            //    arrParam[i].Value = Funciones.CheckDate(item.X_PROBLEM_DATE);

            //i++;
            //if (item.X_PURCHASE_DATE != dateInicio)
            //    arrParam[i].Value = Funciones.CheckDate(item.X_PURCHASE_DATE);

            //i++;
            //if (item.X_RECHARGE_DATE != dateInicio)
            //    arrParam[i].Value = Funciones.CheckDate(item.X_RECHARGE_DATE);

            //i++;
            //if (item.X_REQUEST_DATE != dateInicio)
            //    arrParam[i].Value = Funciones.CheckDate(item.X_REQUEST_DATE);

            //i++;
            //if (item.X_REQUEST_PLACE != null)
            //    arrParam[i].Value = Funciones.CheckDate(item.X_REQUEST_PLACE);

            //i++;
            //if (item.X_FLAG_GPRS != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_FLAG_GPRS);

            //i++;
            //arrParam[i].Value = Funciones.CheckDbl(item.X_COMPLAINT_AMOUNT);

            //i++;
            //if (item.X_LINES != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_LINES);

            //i++;
            //if (item.X_ERROR_MESSAGE != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_ERROR_MESSAGE);

            //i++;
            //if (item.X_MODEL != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_MODEL);

            //i++;
            //if (item.X_MARK != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_MARK);

            //i++;
            //if (item.X_BAND != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_BAND);

            //i++;
            //if (item.X_REPOSITION_REASON != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_REPOSITION_REASON);

            //i++;
            //if (item.X_CHURN_REASON != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_CHURN_REASON);

            //i++;
            //if (item.X_CELLULAR_NUMBER != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_CELLULAR_NUMBER);

            //i++;
            //if (item.X_CLARIFY_VARIATION != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_CLARIFY_VARIATION);

            //i++;
            //if (item.X_FLAG_SEND_RECEIVE != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_FLAG_SEND_RECEIVE);

            //i++;
            //if (item.X_CUSTOMER_NAME != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_CUSTOMER_NAME);

            //i++;
            //if (item.X_PREPAID_CARD_NUMBER != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_PREPAID_CARD_NUMBER);

            //i++;
            //if (item.X_NUMBERS_COMMUNICATION != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_NUMBERS_COMMUNICATION);

            //i++;
            //if (item.X_REFERENCE_NUMBER != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_REFERENCE_NUMBER);

            //i++;
            //if (item.X_OST_NUMBER != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_OST_NUMBER);

            //i++;
            //if (item.X_FRIENDS_FAMILY != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_FRIENDS_FAMILY);

            //i++;
            //if (item.X_COUNTRY_OPERATOR != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_COUNTRY_OPERATOR);

            //i++;
            //if (item.X_OLD_PLAN != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_OLD_PLAN);

            //i++;
            //if (item.X_NEW_PLAN != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_NEW_PLAN);

            //i++;
            //if (item.X_CURRENT_PLAN != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_CURRENT_PLAN);

            //i++;
            //if (item.X_CAMPAIGN != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_CAMPAIGN);

            //i++;
            //if (item.X_BILL_NUMBER_COMPLAINT != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_BILL_NUMBER_COMPLAINT);

            //i++;
            //if (item.X_REFERENCE_ADDRESS != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_REFERENCE_ADDRESS);

            //i++;
            //arrParam[i].Value = Funciones.CheckDbl(item.X_CURRENT_BALANCE);

            //i++;
            //arrParam[i].Value = Funciones.CheckDbl(item.X_LAST_BALANCE);

            //i++;
            //arrParam[i].Value = Funciones.CheckDbl(item.X_BALANCE_REQUESTED);

            //i++;
            //if (item.X_CUSTOMER_SEGMENT != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_CUSTOMER_SEGMENT);

            //i++;
            //if (item.X_SERVICE_PROBLEM != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_SERVICE_PROBLEM);

            //i++;
            //if (item.X_FLAG_OTHER_PROBLEMS != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_FLAG_OTHER_PROBLEMS);

            //i++;
            //if (item.X_OPERATOR_PROBLEM != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_OPERATOR_PROBLEM);

            //i++;
            //if (item.X_CONTACT_TIME_TERM != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_CONTACT_TIME_TERM);

            //i++;
            //if (item.X_STORE != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_STORE);

            //i++;
            //if (item.X_DIAL_TYPE != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_DIAL_TYPE);

            //i++;
            //if (item.X_PROBLEM_LOCATION != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_PROBLEM_LOCATION);

            //i++;
            //if (item.X_FLAG_ADDITIONAL_SERVICES != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_FLAG_ADDITIONAL_SERVICES);

            //i++;
            //if (item.X_FLAG_WAP != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_FLAG_WAP);


            //i++;
            //if (item.X_CLARO_LDN1 != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_CLARO_LDN1);
            //i++;
            //if (item.X_CLARO_LDN2 != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_CLARO_LDN2);
            //i++;
            //if (item.X_CLARO_LDN3 != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_CLARO_LDN3);
            //i++;
            //if (item.X_CLARO_LDN4 != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_CLARO_LDN4);

            //i++;
            //if (item.X_CLAROLOCAL1 != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_CLAROLOCAL1);

            //i++;
            //if (item.X_CLAROLOCAL2 != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_CLAROLOCAL2);

            //i++;
            //if (item.X_CLAROLOCAL3 != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_CLAROLOCAL3);

            //i++;
            //if (item.X_CLAROLOCAL4 != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_CLAROLOCAL4);

            //i++;
            //if (item.X_CLAROLOCAL5 != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_CLAROLOCAL5);

            //i++;
            //if (item.X_CLAROLOCAL6 != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_CLAROLOCAL6);

            //i++;
            //if (item.X_FIXED_NUMBER != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_FIXED_NUMBER);

            //i++;
            //if (item.X_LDI_NUMBER != null)
            //    arrParam[i].Value = Funciones.CheckStr(item.X_LDI_NUMBER);
            #endregion

            try
            {
                Web.Logging.ExecuteMethod(oItem.Audit.Session, oItem.Audit.Transaction, () =>
                {
                    DbFactory.ExecuteNonQuery(oItem.Audit.Session, oItem.Audit.Transaction, DbConnectionConfiguration.SIAC_POST_CLARIFY,
                        DbCommandConfiguration.SIACU_SP_UPDATE_PLUS_CASE, parameters);

                });
                oResponse.ID_CASO = parameters[100].Value.ToString();
                oResponse.FLAG_INSERCION = parameters[101].Value.ToString();
                oResponse.MESSAGE = parameters[102].Value.ToString();

            }
            catch (Exception ex)
            {
                Web.Logging.Error(oItem.Audit.Session, oItem.Audit.Transaction, ex.Message);
            }


            return oResponse;
        }


        public static ConsultationServiceByContractResponse GetConsultationServiceByContract(ConsultationServiceByContractRequest oConsultationServiceByContractRequest)
        {
            string strMsgSalida = string.Empty;
            string msisdn = string.Empty;
            bool bResultado = false;

            ConsultationServiceByContractResponse oConsultationServiceByContractResponse = new ConsultationServiceByContractResponse();

            if (oConsultationServiceByContractRequest.typeProduct == Claro.SIACU.Transac.Service.Constants.PresentationLayer.TipoProduco.HFC)
            {
                CUSTOMER_HFC.consultarServicioPorCodigoContratoEAIRequest objConsultaServicioId = new CUSTOMER_HFC.consultarServicioPorCodigoContratoEAIRequest();
                CUSTOMER_HFC.consultarServicioPorCodigoContratoEAIResponse objConsultaServicioIdSalida = new CUSTOMER_HFC.consultarServicioPorCodigoContratoEAIResponse();
                CUSTOMER_HFC.ConsultarServicioPorCodigoContratoEAIInput objConsultaServicioInput = new CUSTOMER_HFC.ConsultarServicioPorCodigoContratoEAIInput();
                CUSTOMER_HFC.ConsultarServicioPorCodigoContratoEAIOutput objConsultaServicioOutput = new CUSTOMER_HFC.ConsultarServicioPorCodigoContratoEAIOutput();
                CUSTOMER_HFC.ServicioPorCodigoContratoType[] objTempServicio = new CUSTOMER_HFC.ServicioPorCodigoContratoType[0];
                CUSTOMER_HFC.CabeceraRequest objCabecera = new CUSTOMER_HFC.CabeceraRequest();
                CUSTOMER_HFC.CuerpoCSCORequest objCuerpo = new CUSTOMER_HFC.CuerpoCSCORequest();

                objCabecera.idTransaccion = oConsultationServiceByContractRequest.Audit.Transaction;
                objCabecera.ipAplicacion = oConsultationServiceByContractRequest.Audit.IPAddress;
                objCabecera.nombreAplicacion = oConsultationServiceByContractRequest.Audit.ApplicationName;
                objCabecera.usuarioAplicacion = oConsultationServiceByContractRequest.Audit.UserName;
                objCuerpo.codigoContrato = oConsultationServiceByContractRequest.strCodContrato;
                objConsultaServicioInput.cabeceraRequest = objCabecera;
                objConsultaServicioInput.cuerpoRequest = objCuerpo;
                objConsultaServicioId.consultarServicioPorCodigoContratoEaiRequest = objConsultaServicioInput;


                CUSTOMER_HFC.CabeceraResponse objCabeceraSalida = new CUSTOMER_HFC.CabeceraResponse();
                CUSTOMER_HFC.CuerpoCSCOResponse objCuerpoSalida = new CUSTOMER_HFC.CuerpoCSCOResponse();
                objConsultaServicioIdSalida = Configuration.ServiceConfiguration.FIXED_CUSTOMER_HFC.consultarServicioPorCodigoContrato(objConsultaServicioId);

                objCabeceraSalida = objConsultaServicioIdSalida.consultarServicioPorCodigoContratoEaiResponse.cabeceraResponse;
                objCuerpoSalida = objConsultaServicioIdSalida.consultarServicioPorCodigoContratoEaiResponse.cuerpoResponse;
                objTempServicio = objConsultaServicioIdSalida.consultarServicioPorCodigoContratoEaiResponse.cuerpoResponse.listaServicioPorCodigoContrato;

                if (objCabeceraSalida.codigoRespuesta == Constants.NumberZeroString)
                {

                    for (int i = 0; i < objTempServicio.Length; i++)
                    {

                        msisdn = Functions.CheckStr(objTempServicio[i].msisdn);

                    }
                    strMsgSalida = objCabeceraSalida.mensajeRespuesta;
                    bResultado = true;

                }
                else
                {
                    msisdn = String.Empty;
                    strMsgSalida = objCabeceraSalida.mensajeRespuesta;
                    bResultado = false;
                }

            }
            else
            {
                CUSTOMER_LTE.consultarServicioPorCodigoContratoEAIRequest objConsultaServicioId = new CUSTOMER_LTE.consultarServicioPorCodigoContratoEAIRequest();
                CUSTOMER_LTE.consultarServicioPorCodigoContratoEAIResponse objConsultaServicioIdSalida = new CUSTOMER_LTE.consultarServicioPorCodigoContratoEAIResponse();
                CUSTOMER_LTE.ServicioPorCodigoContratoType[] objTempServicio = new CUSTOMER_LTE.ServicioPorCodigoContratoType[0];
                CUSTOMER_LTE.AuditRequestType objAuditRequest = new CUSTOMER_LTE.AuditRequestType();
                objAuditRequest.idTransaccion = oConsultationServiceByContractRequest.Audit.Transaction;
                objAuditRequest.ipAplicacion = oConsultationServiceByContractRequest.Audit.IPAddress;
                objAuditRequest.nombreAplicacion = oConsultationServiceByContractRequest.Audit.ApplicationName;
                objAuditRequest.usuarioAplicacion = oConsultationServiceByContractRequest.Audit.UserName;
                objConsultaServicioId.auditRequest = objAuditRequest;
                objConsultaServicioId.codigoContrato = oConsultationServiceByContractRequest.strCodContrato;

                CUSTOMER_LTE.AuditResponseType objAuditResponse = new CUSTOMER_LTE.AuditResponseType();
                objConsultaServicioIdSalida = Configuration.ServiceConfiguration.FIXED_CUSTOMER_LTE.consultarServicioPorCodigoContrato(objConsultaServicioId);
                objAuditResponse = objConsultaServicioIdSalida.auditResponse;
                objTempServicio = objConsultaServicioIdSalida.listaServicioPorCodigoContrato;

                if (objAuditResponse.codigoRespuesta == Constants.NumberZeroString)
                {

                    for (int i = 0; i < objTempServicio.Length; i++)
                    {
                        msisdn = Functions.CheckStr(objTempServicio[i].msisdn);
                    }
                    strMsgSalida = objAuditResponse.mensajeRespuesta;
                    bResultado = true;

                }
                else
                {
                    msisdn = String.Empty;
                    strMsgSalida = objAuditResponse.mensajeRespuesta;
                    bResultado = false;
                }

            }

            oConsultationServiceByContractResponse.bResultado = bResultado;
            oConsultationServiceByContractResponse.msisdn = msisdn;
            oConsultationServiceByContractResponse.strMsgSalida = strMsgSalida;

            return oConsultationServiceByContractResponse;

        }

        public static List<EntitiesFixed.TransactionScheduled> GetTransactionScheduled(string strIdSession, string strTransaction, string vstrCoId, string vstrCuenta,
                                                                         string vstrFDesde, string vstrFHasta, string vstrEstado, string vstrAsesor,
                                                                         string vstrTipoTran, string vstrCodInter, string vstrCacDac)
        {
            DbParameter[] parameters = 
            {
                new DbParameter("p_servi_coid", DbType.String,255, ParameterDirection.Input, vstrCoId),

                new DbParameter("p_fecha_desde",DbType.Date,ParameterDirection.Input, DBNull.Value),
                new DbParameter("p_fecha_hasta",DbType.Date,ParameterDirection.Input, DBNull.Value),

                new DbParameter("p_estado", DbType.String,255, ParameterDirection.Input, vstrEstado),
                new DbParameter("p_asesor", DbType.String,255, ParameterDirection.Input, vstrAsesor),
                new DbParameter("p_cuenta", DbType.String,255, ParameterDirection.Input, vstrCuenta),
                new DbParameter("p_tipotransaccion", DbType.String,255, ParameterDirection.Input, vstrTipoTran),
                new DbParameter("p_codinteraccion", DbType.String,255, ParameterDirection.Input, vstrCodInter),
                new DbParameter("p_caddac", DbType.String,255, ParameterDirection.Input, vstrCacDac),
                new DbParameter("p_cursor", DbType.Object, ParameterDirection.Output)
            };

            if (!string.IsNullOrEmpty(vstrFDesde))
            {
                parameters[1].Value = Convert.ToDate(vstrFDesde);
            }

            if (!string.IsNullOrEmpty(vstrFHasta))
            {
                parameters[2].Value = Convert.ToDate(vstrFHasta);
            }

            List<EntitiesFixed.TransactionScheduled> listItem = new List<EntitiesFixed.TransactionScheduled>();
            EntitiesFixed.TransactionScheduled item = null;

            try
            {
                DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_EAIAVM, DbCommandConfiguration.SIACU_CONSULTA_POSTT_SERVICIOPROG_HFC, parameters, dr =>
                {
                    while (dr.Read())
                    {
                        item = new EntitiesFixed.TransactionScheduled();
                        item.CO_ID = CSTS.Functions.CheckStr(dr["CO_ID"]);
                        item.CUSTOMER_ID = CSTS.Functions.CheckStr(dr["CUSTOMER_ID"]);
                        item.SERVD_FECHAPROG = CSTS.Functions.CheckStr(dr["SERVD_FECHAPROG"]);
                        item.SERVD_FECHA_REG = CSTS.Functions.CheckStr(dr["SERVD_FECHA_REG"]);
                        item.SERVD_FECHA_EJEC = CSTS.Functions.CheckStr(dr["SERVD_FECHA_EJEC"]);
                        item.SERVC_ESTADO = CSTS.Functions.CheckStr(dr["SERVC_ESTADO"]);
                        item.DESC_ESTADO = CSTS.Functions.CheckStr(dr["DESC_ESTADO"]);
                        item.SERVC_ESBATCH = CSTS.Functions.CheckStr(dr["SERVC_ESBATCH"]);
                        item.SERVV_MEN_ERROR = CSTS.Functions.CheckStr(dr["SERVV_MEN_ERROR"]);
                        item.SERVV_COD_ERROR = CSTS.Functions.CheckStr(dr["SERVV_COD_ERROR"]);
                        item.SERVV_USUARIO_SISTEMA = CSTS.Functions.CheckStr(dr["SERVV_USUARIO_SISTEMA"]);
                        item.SERVV_ID_EAI_SW = CSTS.Functions.CheckStr(dr["SERVV_ID_EAI_SW"]);
                        item.SERVI_COD = CSTS.Functions.CheckStr(dr["SERVI_COD"]);
                        item.DESC_SERVI = CSTS.Functions.CheckStr(dr["DESC_SERVI"]);
                        item.SERVV_MSISDN = CSTS.Functions.CheckStr(dr["SERVV_MSISDN"]);
                        item.SERVV_ID_BATCH = CSTS.Functions.CheckStr(dr["SERVV_ID_BATCH"]);
                        item.SERVV_USUARIO_APLICACION = CSTS.Functions.CheckStr(dr["SERVV_USUARIO_APLICACION"]);
                        item.SERVV_EMAIL_USUARIO_APP = CSTS.Functions.CheckStr(dr["SERVV_EMAIL_USUARIO_APP"]);
                        item.SERVV_XMLENTRADA = CSTS.Functions.CheckStr(dr["SERVV_XMLENTRADA"]);
                        item.SERVC_NROCUENTA = CSTS.Functions.CheckStr(dr["SERVC_NROCUENTA"]);
                        item.SERVC_CODIGO_INTERACCION = CSTS.Functions.CheckStr(dr["SERVC_CODIGO_INTERACCION"]);
                        item.SERVC_PUNTOVENTA = CSTS.Functions.CheckStr(dr["SERVC_PUNTOVENTA"]);
                        item.SERVC_TIPO_SERV = CSTS.Functions.CheckStr(dr["SERVC_TIPO_SERV"]);
                        item.SERVC_CO_SER = CSTS.Functions.CheckStr(dr["SERVC_CO_SER"]);
                        item.SERVC_TIPO_REG = CSTS.Functions.CheckStr(dr["SERVC_TIPO_REG"]);
                        item.SERVC_DES_CO_SER = CSTS.Functions.CheckStr(dr["SERVC_DES_CO_SER"]);

                        listItem.Add(item);
                    }
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
            }

            Web.Logging.Info(strIdSession, strTransaction, "GetTransactionScheduled Lista Resultado: " + listItem.Count);
            
            return listItem;
        }

        public static List<BEDeco> GetServiceDTH(string strIdSession, string strTransaction,
            string strCustomerId, string strCoid)
        {
            List<BEDeco> listServicesDTH = new List<BEDeco>();
            DbParameter[] parameters = { 
            
                new  DbParameter("av_customer_id", DbType.String,22, ParameterDirection.Input,strCustomerId), 
                new  DbParameter("av_cod_id", DbType.String,22, ParameterDirection.Input,strCoid),
                new  DbParameter("ac_equ_cur", DbType.Object,ParameterDirection.Output),
                new  DbParameter("an_resultado", DbType.Int32,ParameterDirection.Output),
                new  DbParameter("av_mensaje", DbType.String,250,ParameterDirection.Output)       
            };

            try
            {

                DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SGA,
                    DbCommandConfiguration.SIACU_SP_P_CONSULTA_EQU, parameters,
                    reader =>
                    {
                        listServicesDTH = new List<BEDeco>();
                        while (reader.Read())
                        {
                            listServicesDTH.Add(new BEDeco
                            {
                                idtransaccion = Functions.CheckStr(reader["idtransaccion"]),
                                codigo_material = Functions.CheckStr(reader["codigo_material"]),
                                codigo_sap = Functions.CheckStr(reader["codigo_sap"]),
                                numero_serie = Functions.CheckStr(reader["numero_serie"]),
                                macadress = Functions.CheckStr(reader["macaddress"]),
                                descripcion_material = Functions.CheckStr(reader["descripcion_material"]),
                                abrev_material = Functions.CheckStr(reader["abrev_material"]),
                                estado_material = Functions.CheckStr(reader["estado_material"]),
                                precio_almacen = Functions.CheckStr(reader["precio_almacen"]),
                                codigo_cuenta = Functions.CheckStr(reader["codigo_cuenta"]),
                                componente = Functions.CheckStr(reader["componente"]),
                                centro = Functions.CheckStr(reader["centro"]),
                                idalm = Functions.CheckStr(reader["idalm"]),
                                almacen = Functions.CheckStr(reader["almacen"]),
                                tipo_equipo = Functions.CheckStr(reader["tipo_equipo"]),
                                id_producto = Functions.CheckStr(reader["idproducto"]),
                                id_cliente = Functions.CheckStr(reader["id_cliente"]),
                                modelo = Functions.CheckStr(reader["modelo"]),
                                fecusu = Functions.CheckStr(reader["fecusu"]),
                                codusu = Functions.CheckStr(reader["codusu"]),
                                convertertype = Functions.CheckStr(reader["convertertype"]),
                                servicio_principal = Functions.CheckStr(reader["servicio_principal"]),
                                headend = Functions.CheckStr(reader["headend"]),
                                ephomeexchange = Functions.CheckStr(reader["ephomeexchange"]),
                                numero = Functions.CheckStr(reader["numero"])
                            });
                        }
                    });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
            }

            return listServicesDTH;

        }

        public static bool GetSaveOCC(string strIdSession, string strTransaction, int vCodSot, int vCustomerId, DateTime vFechaVig, double vMonto, string vComentario, int vflag, string vAplicacion,
                                      string vUsuarioAct, DateTime vFechaAct, string vCodId, ref string vCodResult, ref string vResultado)
        {
            bool salida = false;

            DbParameter[] arrParam = {
                new DbParameter("PCODSOLOT", DbType.Int64, ParameterDirection.Input, vCodSot),
                new DbParameter("PCUSTOMER_ID", DbType.Int64, ParameterDirection.Input, vCustomerId),
                new DbParameter("PFECVIG", DbType.Date, ParameterDirection.Input, vFechaVig),
                new DbParameter("PMONTO", DbType.Decimal, ParameterDirection.Input, vMonto),
                new DbParameter("POBSERVACION", DbType.String, 255, ParameterDirection.Input, vComentario),
                new DbParameter("PFLAG_COBRO_OCC", DbType.Int32, ParameterDirection.Input, vflag),
                new DbParameter("PAPLICACION", DbType.String, 255, ParameterDirection.Input, vAplicacion),
                new DbParameter("PUSUARIO_ACT", DbType.String, 255, ParameterDirection.Input, vUsuarioAct),
                new DbParameter("PFECHA_ACT", DbType.Date, ParameterDirection.Input, vFechaAct),
                new DbParameter("PCOD_ID", DbType.String, 255, ParameterDirection.Input, vCodId),
                new DbParameter("PRESULTADO", DbType.Int32, ParameterDirection.Output),
                new DbParameter("PMSGERR", DbType.String, 255, ParameterDirection.Output)
            };

            try
            {
                Web.Logging.ExecuteMethod(strIdSession, strTransaction, () =>
                {
                    DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_SP_ACTUALIZAR_COSTO_PA, arrParam);
                });
                salida = true;
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
                salida = false;
            }
            finally
            {
                vResultado = CSTS.Functions.CheckStr(arrParam[arrParam.Length - 1].Value);
                vCodResult = CSTS.Functions.CheckStr(arrParam[arrParam.Length - 2].Value);
            }
            return salida;
        }

        public static bool GetInsertLoyalty(string strIdSession, string strTransaction, Customer oCustomer,
                                            string vCodSoLot, int vFlagDirecFact, string vUser, DateTime vFechaReg, ref string vCodResult, ref string vResultado)
        {
            bool salida = false;

            DbParameter[] arrParam = {
                new DbParameter("PCODSOLOT", DbType.Int64, ParameterDirection.Input, vCodSoLot),
                new DbParameter("PCUSTOMER_ID", DbType.Int64, ParameterDirection.Input, oCustomer.CUSTOMER_ID),
                new DbParameter("PDIRECCION_FACTURACION", DbType.String, 255, ParameterDirection.Input, oCustomer.DOMICILIO),
                new DbParameter("PNOTAS_DIRECCION", DbType.String, 255, ParameterDirection.Input, oCustomer.REFERENCIA),
                new DbParameter("PDISTRITO", DbType.String, 255, ParameterDirection.Input, oCustomer.DISTRITO),
                new DbParameter("PPROVINCIA", DbType.String,255, ParameterDirection.Input, oCustomer.PROVINCIA),
                new DbParameter("PCODIGO_POSTAL", DbType.String, 255, ParameterDirection.Input, oCustomer.ZIPCODE),
                new DbParameter("PDEPARTAMENTO", DbType.String, 255, ParameterDirection.Input, oCustomer.DEPARTAMENTO),
                new DbParameter("PPAIS", DbType.String, 255, ParameterDirection.Input, oCustomer.PAIS_LEGAL),
                new DbParameter("PFLAG_DIRECC_FACT", DbType.Int32, ParameterDirection.Input, vFlagDirecFact),
                new DbParameter("PUSUARIO_REG", DbType.String, 255, ParameterDirection.Input, vUser),
                new DbParameter("PFECHA_REG", DbType.Date, ParameterDirection.Input, Convert.ToDate(vFechaReg)),
                new DbParameter("PRESULTADO", DbType.Int32, ParameterDirection.Output),
                new DbParameter("PMSGERR", DbType.String, 255, ParameterDirection.Output)
                };
            try
            {
                Web.Logging.ExecuteMethod(strIdSession, strTransaction, () =>
                {
                    DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_SP_REGISTRA_COSTO_PA, arrParam);
                });
                salida = true;
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
                salida = false;
            }
            finally
            {
                vCodResult = CSTS.Functions.CheckStr(arrParam[arrParam.Length - 2].Value);
                vResultado = CSTS.Functions.CheckStr(arrParam[arrParam.Length - 1].Value);
            }
            return salida;
        }

        public static ConsultationServiceByContractResponse GetCustomerLineNumber(ConsultationServiceByContractRequest request)
        {
            var model = new ConsultationServiceByContractResponse();
            model.strMsgSalida = string.Empty;

            DbParameter[] parameters =
            {
                new DbParameter("result", DbType.String, 255, ParameterDirection.ReturnValue),
                new DbParameter("p_co_id", DbType.Int32, 255, ParameterDirection.Input, request.strCodContrato)
            };

            try
            {
                Web.Logging.ExecuteMethod(request.Audit.Session, request.Audit.Transaction, () =>
                {
                    DbFactory.ExecuteNonQuery(request.Audit.Session, request.Audit.Transaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_TFUN051_GET_DNNUM_FROM_COID, parameters);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, ex.Message);
            }
            finally
            {
                var resultadoFuncion = parameters[parameters.Length - 2].Value.ToString();
                model.msisdn = resultadoFuncion;
                model.bResultado = true;
            }

            return model;
        }

        public static EntitiesFixed.GetCaseInsert.CaseInsertResponse GetInteractIDforCaseID(EntitiesFixed.GetCaseInsert.CaseInsertRequest objRequest)
        {

            EntitiesFixed.GetCaseInsert.CaseInsertResponse oResponse = new EntitiesFixed.GetCaseInsert.CaseInsertResponse();

            Web.Logging.Info( objRequest.Audit.Session, objRequest.Audit.Transaction, "GetInteractIDforCaseID IN | P_ID_CASO" + objRequest.ID_CASO);

            DbParameter[] parameters =
            
            {
                   #region Imput
                    new DbParameter("P_ID_CASO",DbType.String,80,ParameterDirection.Input,objRequest.ID_CASO),
                    new DbParameter("P_FLAG_CONSULTA",DbType.String,50,ParameterDirection.Output),
					new DbParameter("P_MSG_TEXT", DbType.String,255,ParameterDirection.Output),
			   		new DbParameter("P_INTERACT_ID", DbType.String,255,ParameterDirection.Output),
                    #endregion
			};

            try
            {
                Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    DbFactory.ExecuteNonQuery(objRequest.Audit.Session, objRequest.Audit.Transaction, DbConnectionConfiguration.SIAC_POST_CLARIFY,
                        DbCommandConfiguration.SIACU_SP_INTERACT_ID_HFC, parameters);

                });


                oResponse.rFlagInsercion = parameters[1].Value.ToString();
                oResponse.rMsgText = parameters[2].Value.ToString();
                oResponse.rCasoId = parameters[3].Value.ToString();

            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "GetInteractIDforCaseID OUT ");

            return oResponse;
        }

        public static Claro.SIACU.Entity.Transac.Service.Fixed.GetHistoryToa.HistoryToaResponse GetHistoryToa(Claro.SIACU.Entity.Transac.Service.Fixed.GetHistoryToa.HistoryToaRequest request)
        {
            var model = new Claro.SIACU.Entity.Transac.Service.Fixed.GetHistoryToa.HistoryToaResponse();
            model.strMsjResult = string.Empty;

            DbParameter[] parameters =
            {
                new DbParameter("pi_nro_orden", DbType.String, ParameterDirection.Input, request.strNroOrden),
                new DbParameter("pi_id_consulta", DbType.Int64, ParameterDirection.Input, request.strIdConsulta),
                new DbParameter("pi_franja", DbType.String, 255, ParameterDirection.Input, request.strFranja),
                new DbParameter("pi_dia_reserva", DbType.Date, 255, ParameterDirection.Input, request.strDiaReserva),
                new DbParameter("pi_id_bucket", DbType.String, 255, ParameterDirection.Input, request.strIdBucket),
                new DbParameter("pi_cod_zona", DbType.String,255, ParameterDirection.Input, request.strCodZona),
                new DbParameter("pi_cod_plano", DbType.String, 255, ParameterDirection.Input, request.strCodPlano),
                new DbParameter("pi_tipo_orden", DbType.String, 255, ParameterDirection.Input, request.strTipoOrden),
                new DbParameter("pi_sub_tipo_orden", DbType.String, 255, ParameterDirection.Input, request.strSubTipoOrden),
                new DbParameter("po_cod_result", DbType.Int32, ParameterDirection.Output),
                new DbParameter("po_msj_result", DbType.String, 255, ParameterDirection.Output),
                new DbParameter("po_nro_orden", DbType.String, 255, ParameterDirection.Output)
            };

            try
            {
                Web.Logging.ExecuteMethod(request.Audit.Session, request.Audit.Transaction, () =>
                {
                    DbFactory.ExecuteNonQuery(request.Audit.Session, request.Audit.Transaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_HISTORIAL_RESERVA_TOA, parameters);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, ex.Message);
                model.strNroOrden = CSTS.Constants.strMenosUno;
            }
            finally
            {
                model.strCodResult = CSTS.Functions.CheckStr(parameters[parameters.Length - 3].Value).ToString();
                model.strMsjResult = CSTS.Functions.CheckStr(parameters[parameters.Length - 2].Value);
                model.strNroOrden = CSTS.Functions.CheckStr(parameters[parameters.Length - 1].Value);
            }

            return model;
        }

        public static Claro.SIACU.Entity.Transac.Service.Fixed.GetHistoryToa.HistoryToaResponse GetUpdateHistoryToa(Claro.SIACU.Entity.Transac.Service.Fixed.GetHistoryToa.HistoryToaRequest request)
        {
            var model = new Claro.SIACU.Entity.Transac.Service.Fixed.GetHistoryToa.HistoryToaResponse();
            model.strMsjResult = string.Empty;

            DbParameter[] parameters =
            {
                new DbParameter("pi_nro_orden", DbType.String, ParameterDirection.Input, request.strNroOrden),
                new DbParameter("pi_value", DbType.Int64, ParameterDirection.Input, request.strValor),
                new DbParameter("pi_tipo_transaccion", DbType.String, 255, ParameterDirection.Input, request.strTipoTransaccion),
                new DbParameter("po_cod_result", DbType.Int32, ParameterDirection.Output),
                new DbParameter("po_msj_result", DbType.String, 255, ParameterDirection.Output)
            };

            try
            {
                Web.Logging.ExecuteMethod(request.Audit.Session, request.Audit.Transaction, () =>
                {
                    DbFactory.ExecuteNonQuery(request.Audit.Session, request.Audit.Transaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_UPDATE_RESERVA_TOA, parameters);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, ex.Message);
            }
            finally
            {
                model.strCodResult = CSTS.Functions.CheckStr(parameters[parameters.Length - 2].Value).ToString();
                model.strMsjResult = CSTS.Functions.CheckStr(parameters[parameters.Length - 1].Value);
            }

            return model;
        }

        public static Claro.SIACU.Entity.Transac.Service.Fixed.PostEtaInboundToa.EtaInboundToaResponse PostGestionarOrdenesToa(Claro.SIACU.Entity.Transac.Service.Fixed.PostEtaInboundToa.EtaInboundToaRequest objRequestInboundEta)
        {

            string vDurActivity = string.Empty;
            string vTiempoViajeActivity = string.Empty;
            Claro.SIACU.Entity.Transac.Service.Fixed.PostEtaInboundToa.EtaInboundToaResponse Resp = new Claro.SIACU.Entity.Transac.Service.Fixed.PostEtaInboundToa.EtaInboundToaResponse();

            INTOA.gestionarOrdenResponse objRespondeInboundToa = new INTOA.gestionarOrdenResponse();

            try
            {
                INTOA.parametrosAuditRequest objRequestAuditInboundToa = new INTOA.parametrosAuditRequest();
                objRequestAuditInboundToa.idTransaccion = objRequestInboundEta.Audit.Transaction;
                objRequestAuditInboundToa.ipAplicacion = objRequestInboundEta.Audit.Session;
                objRequestAuditInboundToa.nombreAplicacion = objRequestInboundEta.Audit.ApplicationName;
                objRequestAuditInboundToa.usuarioAplicacion = objRequestInboundEta.Audit.UserName;

                INTOA.Head objRequestHeadInboundToa = new INTOA.Head();
                INTOA.InventorySettings confInventario = new INTOA.InventorySettings();
                objRequestHeadInboundToa.modoCargaPropiedades = objRequestInboundEta.propiedades.modoCargaPropiedades;
                objRequestHeadInboundToa.modoProcesamiento = objRequestInboundEta.propiedades.modoProcesamiento;
                objRequestHeadInboundToa.tipoCarga = objRequestInboundEta.propiedades.tipoCarga;
                objRequestHeadInboundToa.fechaTransaccion = objRequestInboundEta.propiedades.fechaTransaccion.ToString("yyyy-MM-dd hh:mm");
                objRequestHeadInboundToa.configuracionSOT = objRequestInboundEta.propiedades.configuracionSOT;

                confInventario.camposClave = objRequestInboundEta.propiedades.configuracionInventario;
                objRequestHeadInboundToa.configuracionInventario = confInventario;

                INTOA.Command objRequestComandInboundToa = new INTOA.Command();
                objRequestComandInboundToa.fechaAsignacion = objRequestInboundEta.comando.fechaAsignacion.ToString("yyyy-MM-dd hh:mm");
                objRequestComandInboundToa.tipoComando = objRequestInboundEta.comando.tipoComando;
                objRequestComandInboundToa.idContrata = objRequestInboundEta.comando.idContrata;
                objRequestComandInboundToa.idContrataError = objRequestInboundEta.comando.idContrataError;
                
                INTOA.Appointment objRequestAppointmentInboundToa = new INTOA.Appointment();
                objRequestAppointmentInboundToa.idAgenda = objRequestInboundEta.ordenTrabajo.nroOrden;
                //objRequestAppointmentInboundToa.nroUsuario = objRequestInboundEta.ordenTrabajo.nroUsuario;
                objRequestAppointmentInboundToa.tipoTrabajo = objRequestInboundEta.ordenTrabajo.tipoTrabajo;
                objRequestAppointmentInboundToa.franjasHorariasOrdenTrabajo = objRequestInboundEta.ordenTrabajo.franjasHorariasOrdenTrabajo;
                objRequestAppointmentInboundToa.tipoTrabajo = objRequestInboundEta.ordenTrabajo.tipoTrabajo;
                objRequestAppointmentInboundToa.tiempoRecordatorioMinutos = objRequestInboundEta.ordenTrabajo.tiempoRecordatorioMinutos;
                objRequestAppointmentInboundToa.duracion = objRequestInboundEta.ordenTrabajo.duracion;


                if (objRequestInboundEta.ordenTrabajo.propiedades != null) 
                {
                    List<INTOA.Property> objPropiedades = new List<INTOA.Property>();
                    foreach (var item in objRequestInboundEta.ordenTrabajo.propiedades)
                    {
                        INTOA.Property objData = new INTOA.Property();
                        objData.clave = item.clave;
                        objData.valor = item.valor;
                        objPropiedades.Add(objData);
                    }                    
                    objRequestAppointmentInboundToa.propiedades = objPropiedades.ToArray();
                }                            

                INTOA.gestionarOrdenRequest objRequest = new INTOA.gestionarOrdenRequest();
                objRequest.auditRequest = objRequestAuditInboundToa;
                objRequest.cabecera = objRequestHeadInboundToa;
                objRequest.comando = objRequestComandInboundToa;
                objRequest.ordenTrabajo = objRequestAppointmentInboundToa;


                objRespondeInboundToa = Claro.Web.Logging.ExecuteMethod<INTOA.gestionarOrdenResponse>(objRequestInboundEta.Audit.Session, objRequestInboundEta.Audit.Transaction, () =>
                {
                    return Configuration.WebServiceConfiguration.ADMCUAD_InboundService.gestionarOrden(objRequest);
                });
            
                Resp.idAgenda = objRespondeInboundToa.respuestaOrdenTrabajo.idAgenda;
                Resp.idETA = objRespondeInboundToa.respuestaOrdenTrabajo.idETA;
                Resp.resultadoOperacion = objRespondeInboundToa.respuestaOrdenTrabajo.mensajes[0].resultadoOperacion;
                Resp.tipoMensaje = objRespondeInboundToa.respuestaOrdenTrabajo.mensajes[0].tipoMensaje;
                Resp.codigoError = objRespondeInboundToa.respuestaOrdenTrabajo.mensajes[0].codigoError;
                Resp.descripcionError = objRespondeInboundToa.respuestaOrdenTrabajo.mensajes[0].descripcionError;
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequestInboundEta.Audit.Session, objRequestInboundEta.Audit.Transaction, ex.Message);
            }

            return Resp;
        }

        public static EntitiesFixed.ETAFlow ETAFlowValidateReservation(string strIdSession, string strTransaction, int an_tiptra, string an_tipsrv)
        {
            int an_indica = 0;
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("pi_tiptra", DbType.String, ParameterDirection.Input,an_tiptra),
                new DbParameter("pi_tipsrv", DbType.String,255, ParameterDirection.Input, an_tipsrv),
                new DbParameter("po_flag_result", DbType.String,255, ParameterDirection.Output),
                new DbParameter("po_cod_result", DbType.Int32,20, ParameterDirection.Output),
                new DbParameter("po_msj_result", DbType.String,255, ParameterDirection.Output)
            };

            DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_GET_FLAG_RESERVA_TOA, parameters);

            return new EntitiesFixed.ETAFlow
            {
                an_indica = Convert.ToInt(parameters[2].Value.ToString())
            };
        }

        public static Claro.SIACU.Entity.Transac.Service.Fixed.PostEtaInboundToa.EtaInboundToaResponse PostGestionarCancelaToa(Claro.SIACU.Entity.Transac.Service.Fixed.PostEtaInboundToa.EtaInboundToaRequest objRequestInboundEta)
        {

            string vDurActivity = string.Empty;
            string vTiempoViajeActivity = string.Empty;
            Claro.SIACU.Entity.Transac.Service.Fixed.PostEtaInboundToa.EtaInboundToaResponse Resp = new Claro.SIACU.Entity.Transac.Service.Fixed.PostEtaInboundToa.EtaInboundToaResponse();

            INTOA.cancelarOrdenResponse objRespondeInboundToa = new INTOA.cancelarOrdenResponse();

            try
            {
                INTOA.parametrosAuditRequest objRequestAuditInboundToa = new INTOA.parametrosAuditRequest();
                objRequestAuditInboundToa.idTransaccion = objRequestInboundEta.Audit.Transaction;
                objRequestAuditInboundToa.ipAplicacion = objRequestInboundEta.Audit.Session;
                objRequestAuditInboundToa.nombreAplicacion = objRequestInboundEta.Audit.ApplicationName;
                objRequestAuditInboundToa.usuarioAplicacion = objRequestInboundEta.Audit.UserName;

                INTOA.cancelarOrdenRequest objRequest = new INTOA.cancelarOrdenRequest();
                INTOA.InventorySettings confInventario = new INTOA.InventorySettings();
                confInventario.camposClave = objRequestInboundEta.propiedades.configuracionInventario;
                objRequest.auditRequest = objRequestAuditInboundToa;
                objRequest.tipoCarga = objRequestInboundEta.propiedades.tipoCarga;
                objRequest.configuracionSOT = objRequestInboundEta.propiedades.configuracionSOT;
                objRequest.configuracionInventario = confInventario;
                objRequest.tipoComando = objRequestInboundEta.comando.tipoComando;
                objRequest.idAgenda = objRequestInboundEta.ordenTrabajo.nroOrden;

                objRespondeInboundToa = Claro.Web.Logging.ExecuteMethod<INTOA.cancelarOrdenResponse>(objRequestInboundEta.Audit.Session, objRequestInboundEta.Audit.Transaction, () =>
                {
                    return Configuration.WebServiceConfiguration.ADMCUAD_InboundService.cancelarOrden(objRequest);
                });

                Resp.idAgenda = objRespondeInboundToa.respuestaOrdenTrabajo.idAgenda;
                Resp.idETA = objRespondeInboundToa.respuestaOrdenTrabajo.idETA;
                Resp.resultadoOperacion = objRespondeInboundToa.respuestaOrdenTrabajo.mensajes[0].resultadoOperacion;
                Resp.tipoMensaje = objRespondeInboundToa.respuestaOrdenTrabajo.mensajes[0].tipoMensaje;
                Resp.codigoError = objRespondeInboundToa.respuestaOrdenTrabajo.mensajes[0].codigoError;
                Resp.descripcionError = objRespondeInboundToa.respuestaOrdenTrabajo.mensajes[0].descripcionError;
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequestInboundEta.Audit.Session, objRequestInboundEta.Audit.Transaction, ex.Message);
            }

            return Resp;
        }


        public static Claro.SIACU.Entity.Transac.Service.Fixed.GetETAAuditoriaRequestCapacity.BEETAAuditoriaResponseCapacity ConsultarCapacidadCuadrillas(Claro.SIACU.Entity.Transac.Service.Fixed.GetETAAuditoriaRequestCapacity.BEETAAuditoriaRequestCapacity objBEETAAuditoriaRequestCapacity)
        {

            string vDurActivity = string.Empty;
            string vTiempoViajeActivity = string.Empty;
            Claro.SIACU.Entity.Transac.Service.Fixed.GetETAAuditoriaRequestCapacity.BEETAAuditoriaResponseCapacity Resp = new Claro.SIACU.Entity.Transac.Service.Fixed.GetETAAuditoriaRequestCapacity.BEETAAuditoriaResponseCapacity();

            ADMCU.AuditResponse objResponseCuadrillas = new ADMCU.AuditResponse();
            try
            {
                ADMCU.AuditRequest AuditRequestCuadrillas = new ADMCU.AuditRequest();
                AuditRequestCuadrillas.idTransaccion = objBEETAAuditoriaRequestCapacity.pIdTrasaccion;
                AuditRequestCuadrillas.ipAplicacion = objBEETAAuditoriaRequestCapacity.pIP_APP;
                AuditRequestCuadrillas.nombreAplicacion = objBEETAAuditoriaRequestCapacity.pAPP;
                AuditRequestCuadrillas.usuarioAplicacion = objBEETAAuditoriaRequestCapacity.pUsuario;


                ADMCU.campoActividadType[] ListaCapActiRequestCuadrillas = new ADMCU.campoActividadType[objBEETAAuditoriaRequestCapacity.vCampoActividad.Length];


                String CantidadFechas = String.Empty;
                foreach (DateTime vF in objBEETAAuditoriaRequestCapacity.vFechas)
                {
                    CantidadFechas = CantidadFechas + ";" + vF.ToString();
                }

                if (objBEETAAuditoriaRequestCapacity.vCampoActividad.Length > 0)
                {
                    int i = 0;
                    ADMCU.campoActividadType CampoActividadRequestCuadrillas = null;
                    foreach (Claro.SIACU.Entity.Transac.Service.Fixed.BEETACampoActivity oCampAct in objBEETAAuditoriaRequestCapacity.vCampoActividad)
                    {
                        CampoActividadRequestCuadrillas = new ADMCU.campoActividadType();
                        CampoActividadRequestCuadrillas.nombre = oCampAct.Nombre;
                        CampoActividadRequestCuadrillas.valor = oCampAct.Valor;
                        ListaCapActiRequestCuadrillas[i] = CampoActividadRequestCuadrillas;
                        i++;
                    }
                }
                else
                {
                    ListaCapActiRequestCuadrillas[0].nombre = String.Empty;
                    ListaCapActiRequestCuadrillas[0].valor = String.Empty;
                }

                ADMCU.parametrosRequest oIniParamRequestCuadrillas = new ADMCU.parametrosRequest();
                ADMCU.parametrosRequest[] oParamRequestCuadrillas = new ADMCU.parametrosRequest[] { oIniParamRequestCuadrillas };

                ADMCU.parametrosRequestObjetoRequestOpcional[] ListaParamReqOpcionalCuadrillas = new ADMCU.parametrosRequestObjetoRequestOpcional[objBEETAAuditoriaRequestCapacity.vListaCapReqOpc.Length];

                if (objBEETAAuditoriaRequestCapacity.vListaCapReqOpc.Length > 0)
                {
                    int j = 0, k = 0;
                    foreach (Claro.SIACU.Entity.Transac.Service.Fixed.BEETAListaParamRequestOpcionalCapacity oListaParReq in objBEETAAuditoriaRequestCapacity.vListaCapReqOpc)
                    {

                        foreach (Claro.SIACU.Entity.Transac.Service.Fixed.BEETAParamRequestCapacity oParamReqCapacity in oListaParReq.ParamRequestCapacities)
                        {
                            ADMCU.parametrosRequestObjetoRequestOpcional oParamRequestOpcionalCuadrillas = new ADMCU.parametrosRequestObjetoRequestOpcional();
                            oParamRequestOpcionalCuadrillas.campo = oParamReqCapacity.Campo;
                            oParamRequestOpcionalCuadrillas.valor = oParamReqCapacity.Valor;
                            ListaParamReqOpcionalCuadrillas[j] = oParamRequestOpcionalCuadrillas;
                            j++;
                        }
                        oParamRequestCuadrillas[k].objetoRequestOpcional = ListaParamReqOpcionalCuadrillas;
                        k++;
                    }
                }
                else
                {
                    ListaParamReqOpcionalCuadrillas[0].campo = string.Empty;
                    ListaParamReqOpcionalCuadrillas[0].valor = string.Empty;
                    oParamRequestCuadrillas[0].objetoRequestOpcional = ListaParamReqOpcionalCuadrillas;
                }

                ADMCU.capacidadType[] ListaCapacidadTypeCuadrillas = new ADMCU.capacidadType[0];

                ADMCU.parametrosResponse[] ListaParamResponseCuadrillas = new ADMCU.parametrosResponse[0];


                objResponseCuadrillas = Claro.Web.Logging.ExecuteMethod<ADMCU.AuditResponse>(objBEETAAuditoriaRequestCapacity.Audit.Session, objBEETAAuditoriaRequestCapacity.Audit.Transaction, () =>
                {

                    return Configuration.WebServiceConfiguration.ADMCUAD_CapacityService.consultarCapacidad(AuditRequestCuadrillas,
                                                                                                            objBEETAAuditoriaRequestCapacity.vFechas,
                                                                                                            null,
                                                                                                            objBEETAAuditoriaRequestCapacity.vCalcDur,
                                                                                                            objBEETAAuditoriaRequestCapacity.vCalcDurEspec,
                                                                                                            objBEETAAuditoriaRequestCapacity.vCalcTiempoViaje,
                                                                                                            objBEETAAuditoriaRequestCapacity.vCalcTiempoViajeEspec,
                                                                                                            objBEETAAuditoriaRequestCapacity.vCalcHabTrabajo,
                                                                                                            objBEETAAuditoriaRequestCapacity.vCalcHabTrabajoEspec,
                                                                                                            objBEETAAuditoriaRequestCapacity.vObtenerUbiZona,
                                                                                                            objBEETAAuditoriaRequestCapacity.vObtenerUbiZonaEspec,
                                                                                                            objBEETAAuditoriaRequestCapacity.vEspacioTiempo,
                                                                                                            objBEETAAuditoriaRequestCapacity.vHabilidadTrabajo,
                                                                                                            ListaCapActiRequestCuadrillas,
                                                                                                            oParamRequestCuadrillas,
                                                                                                            out vDurActivity,
                                                                                                            out vTiempoViajeActivity,
                                                                                                            out ListaCapacidadTypeCuadrillas,
                                                                                                            out ListaParamResponseCuadrillas);
                });

                Resp.CodigoRespuesta = objResponseCuadrillas.codigoRespuesta;
                Resp.IdTransaccion = objResponseCuadrillas.idTransaccion;
                Resp.MensajeRespuesta = objResponseCuadrillas.mensajeRespuesta;
                string OutDurActivity = vDurActivity;
                string OutTiempoViajeActivity = vTiempoViajeActivity;

                Resp.DuraActivity = vDurActivity;
                Resp.TiempoViajeActivity = vTiempoViajeActivity;

                Claro.SIACU.Entity.Transac.Service.Fixed.BEETAEntidadcapacidadType[] oCapacidadTypeM = new Claro.SIACU.Entity.Transac.Service.Fixed.BEETAEntidadcapacidadType[ListaCapacidadTypeCuadrillas.Length];
                int l = 0;
                foreach (ADMCU.capacidadType oEntCapacidadType in ListaCapacidadTypeCuadrillas)
                {
                    Claro.SIACU.Entity.Transac.Service.Fixed.BEETAEntidadcapacidadType oCapacidadType = new Claro.SIACU.Entity.Transac.Service.Fixed.BEETAEntidadcapacidadType();
                    oCapacidadType.Ubicacion = oEntCapacidadType.ubicacion;
                    oCapacidadType.Fecha = oEntCapacidadType.fecha;
                    oCapacidadType.EspacioTiempo = oEntCapacidadType.espacioTiempo;
                    oCapacidadType.HabilidadTrabajo = oEntCapacidadType.habilidadTrabajo;
                    oCapacidadType.Cuota = oEntCapacidadType.cuota;
                    oCapacidadType.Disponible = oEntCapacidadType.disponible;
                    oCapacidadTypeM[l] = oCapacidadType;
                    l++;
                }
                Resp.ObjetoCapacity = oCapacidadTypeM;



            }
            catch (Exception ex)
            {
                Web.Logging.Error(objBEETAAuditoriaRequestCapacity.Audit.Session, objBEETAAuditoriaRequestCapacity.Audit.Transaction, ex.Message);
            }
            return Resp;
        }

        public static List<EntitiesFixed.TransactionScheduled> GetSchedulingRule(string strIdSession, string strTransaction, string p_idparametro)
        {
            DbParameter[] parameters = 
            {
                new DbParameter("p_idparametro", DbType.String,255, ParameterDirection.Input, p_idparametro),
                new DbParameter("po_dato", DbType.Object, ParameterDirection.Output),
                new DbParameter("po_cod_error", DbType.String,255, ParameterDirection.Output),
                new DbParameter("po_des_error", DbType.String,255, ParameterDirection.Output)
            };

            List<EntitiesFixed.TransactionScheduled> listItem = new List<EntitiesFixed.TransactionScheduled>();
            EntitiesFixed.TransactionScheduled item = null;

            try
            {
                DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_CONSULTA_REGLAS_AGENDAMIENTO, parameters, dr =>
                {
                    while (dr.Read())
                    {
                        item = new EntitiesFixed.TransactionScheduled();
                        item.ID_PARAMETRO = CSTS.Functions.CheckStr(dr["ID_PARAMETRO"]);
                        item.CODIGOC = CSTS.Functions.CheckStr(dr["CODIGOC"]);
                        item.CODIGONPAGE = CSTS.Functions.CheckStr(dr["CODIGON"].ToString());
                        item.DESCRIPCION = CSTS.Functions.CheckStr(dr["cab_desc"]);
                        item.ABREVIATURA = CSTS.Functions.CheckStr(dr["cab_desc"]);
                        item.ESTADO = CSTS.Functions.CheckStr(dr["cab_desc"]);
                        item.ID_DETALLE = CSTS.Functions.CheckStr(dr["ID_DETALLE"]);
                        item.DESCRIPCION_DET = CSTS.Functions.CheckStr(dr["deta_desc"]);
                        item.ABREVIATURA_DET = CSTS.Functions.CheckStr(dr["deta_abrev"]);
                        item.ESTADO_DET = CSTS.Functions.CheckStr(dr["deta_estado"]);

                        listItem.Add(item);
                    }
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
            }

            Web.Logging.Info(strIdSession, strTransaction, "GetSchedulingRule Lista Resultado: " + listItem.Count);

            return listItem;
        }


        public static Claro.SIACU.Entity.Transac.Service.Fixed.GetCamapaign.CamapaignResponse GetCampaign(Claro.SIACU.Entity.Transac.Service.Fixed.GetCamapaign.CamapaignRequest objRequest)
        {

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_CONSULTA", DbType.Object, ParameterDirection.Output),
                new DbParameter("P_FLAG", DbType.Int32, ParameterDirection.Input, objRequest.Active)
            };
            var objCampaignResponse = new Claro.SIACU.Entity.Transac.Service.Fixed.GetCamapaign.CamapaignResponse();
            objCampaignResponse.lstCampaigns = new List<Campaign>();
            try
            {
                DbFactory.ExecuteReader(objRequest.Audit.Session, objRequest.Audit.Transaction, DbConnectionConfiguration.SIAC_POST_PVU, DbCommandConfiguration.SIACU_POST_SP_OBTENER_CAMPANA, parameters, (IDataReader reader) =>
                {
                    while (reader.Read())
                    {

                        var objEntity = new Campaign
                        {
                            IDCAMPAIGN = Convert.ToString(reader["CAMPV_CODIGO"]),
                            DESCRIPTION = Convert.ToString(reader["CAMPV_DESCRIPCION"]),
                            DATE_END = Convert.ToDate(reader["CAMPD_FECHA_FIN"])
                        };
                        objCampaignResponse.lstCampaigns.Add(objEntity);
                    }

                });


            }
            catch (Exception e)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, Claro.SIACU.Transac.Service.Functions.GetExceptionMessage(e));
            }
            return objCampaignResponse;
        }
        public static Entity.Transac.Service.Fixed.GetPlans.PlansResponse GetNewPlans(Entity.Transac.Service.Fixed.GetPlans.PlansRequest objRequest)
        {
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("PO_CONSULTA", DbType.Object, ParameterDirection.Output),
                new DbParameter("PI_OFERTA", DbType.String,255, ParameterDirection.Input,objRequest.strOferta),
                new DbParameter("PI_OFICINA", DbType.String,255, ParameterDirection.Input, objRequest.strOffice),
                new DbParameter("PI_OFICINADEFAULT", DbType.String,255, ParameterDirection.Input, objRequest.strOfficeDefault),
                new DbParameter("PI_TIPO_PRODUCTO",DbType.String,255,ParameterDirection.Input,objRequest.strTipoProducto),
                new DbParameter("PI_FLAG_EJECUCION",DbType.String,255,ParameterDirection.Input,objRequest.strFlagEjecution)
            };
            var objResponse = new Entity.Transac.Service.Fixed.GetPlans.PlansResponse();
            objResponse.listPlan= new List<ProductPlan>();
            try
            {
                DbFactory.ExecuteReader(objRequest.Audit.Session, objRequest.Audit.Transaction, DbConnectionConfiguration.SIAC_POST_PVU, DbCommandConfiguration.SIACU_SP_CON_PLAN_CAMPANA, parameters, (IDataReader reader) =>
                {
                    while (reader.Read())
                    {

                        var objEntity = new ProductPlan();
                        objEntity.strCodPlanSisact = Convert.ToString(reader["COD_PLAN_SISACT"]);
                        objEntity.strDesPlanSisact = Convert.ToString(reader["DES_PLAN_SISACT"]);
                        objEntity.strSolucion = Convert.ToString(reader["SOLUCION"]);
                        objEntity.strTmcode = Convert.ToString(reader["TMCODE"]);
                        objEntity.strDesTmcode = Convert.ToString(reader["DES_TMCODE"]);
                        objEntity.strVersion = Convert.ToString(reader["VERSION"]);
                        objEntity.strCatProd = Convert.ToString(reader["CAT_PROD"]);
                        objEntity.strTipoProd = Convert.ToString(reader["TIPO_PROD"]);
                        objEntity.strCodPlano = Convert.ToString(reader["COD_PLANO"]);
                        objEntity.strUserCrea = Convert.ToString(reader["USER_CREA"]);
                        objEntity.strStatus = Convert.ToString(reader["PLNC_ESTADO"]);
                        objEntity.strCampaignCode = Convert.ToString(reader["CAMPV_CODIGO"]);
                        objEntity.strCampaignDescription = Convert.ToString(reader["CAMPV_DESCRIPCION"]);
                        objEntity.strCampaignDateEnd = Convert.ToDate(reader["CAMPD_FECHA_FIN"]);
                        objResponse.listPlan.Add(objEntity);
                    }

                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, Claro.SIACU.Transac.Service.Functions.GetExceptionMessage(ex));
            }
            Claro.Web.Logging.Info("LTEGetNewPlans", "LTEPlanMigration", "Finalizó GetNewPlans");
            return objResponse;
        }

        public static Entity.Transac.Service.Fixed.GetValidateDepVelLte.ValidateDepVelLteResponse ValidateDepVelLTE(Entity.Transac.Service.Fixed.GetValidateDepVelLte.ValidateDepVelLteRequest objRequest)
        {

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("RESULT", DbType.Int64, ParameterDirection.ReturnValue),
                new DbParameter("AN_COD_ID", DbType.Int64, ParameterDirection.Input,objRequest.intContract)
            };
            var objResponse = new Entity.Transac.Service.Fixed.GetValidateDepVelLte.ValidateDepVelLteResponse();
            objResponse.intResult = 0;
            try
            {
                Claro.Web.Logging.Info("ValidateDepVelLTE", "LTEPlanMigration", "Finalizó ValidateDepVelLTE");
                DbFactory.ExecuteNonQuery(objRequest.Audit.Session, objRequest.Audit.Transaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_SGAFUN_GET_VEL_LTE, parameters);
                objResponse.intResult = Convert.ToInt(parameters[0].Value.ToString());
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, Claro.SIACU.Transac.Service.Functions.GetExceptionMessage(ex));
            }
            Claro.Web.Logging.Info("ValidateDepVelLTE", "LTEPlanMigration", "Finalizó ValidateDepVelLTE");
            return objResponse;
        }

        public static List<Entity.Transac.Service.Fixed.OrderSubType> GetOrderSubTypeWork(string strIdSession, string strTransaction, string vintTipoOrden)
        {
            List<OrderSubType> list = new List<OrderSubType>();
            OrderSubType item = null;
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("vIdtiptra", DbType.String,255, ParameterDirection.Input, vintTipoOrden),
                new DbParameter("po_dato", DbType.Object, ParameterDirection.Output),
                new DbParameter("po_cod_error", DbType.String,255, ParameterDirection.Output),
                new DbParameter("po_des_error", DbType.String,255, ParameterDirection.Output)
                //new DbParameter("av_cod_tipo_orden", DbType.String,255, ParameterDirection.Input, vintTipoOrden)
            };
            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_SGASS_CONSULTA_SUBTIPORD, parameters, dr =>
            {
                while (dr.Read())
                {
                    item = new OrderSubType();
                    item.COD_SUBTIPO_ORDEN = CSTS.Functions.CheckStr(dr["COD_SUBTIPO_ORDEN"]);
                    item.TIEMPO_MIN = CSTS.Functions.CheckStr(dr["TIEMPO_MIN"]);
                    item.DESCRIPCION = CSTS.Functions.CheckStr(dr["DET_DES"]);
                    item.TIPO_SERVICIO = CSTS.Functions.CheckStr(dr["TIPO_SERVICIO"]);
                    item.ID_SUBTIPO_ORDEN = CSTS.Functions.CheckStr(dr["ID_SUBTIPO_ORDEN"]);
                    item.DECOS = CSTS.Functions.CheckStr(dr["DECOS"]);
                    list.Add(item);
                }
            });
            return list;
        }

        public static Entity.Transac.Service.Fixed.OrderSubType GetValidationSubTypeWork(Entity.Transac.Service.Fixed.GetOrderSubType.OrderSubTypesRequest objRequest)
        {

            Entity.Transac.Service.Fixed.OrderSubType oResponse = new Entity.Transac.Service.Fixed.OrderSubType();

            Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "GetValidationSubTypeWork IN | COD_CONTRATO:" + objRequest.av_cod_contrato + " | TIPO_TRABAJO:" + objRequest.av_cod_tipo_trabajo);

            DbParameter[] parameters =
            
            {
                   #region Imput
                    new DbParameter("AV_COD_ID",DbType.String,80,ParameterDirection.Input,objRequest.av_cod_contrato),
                    new DbParameter("LN_TIPTRA",DbType.String,80,ParameterDirection.Input,objRequest.av_cod_tipo_trabajo),
					new DbParameter("po_subtipo", DbType.String,255,ParameterDirection.Output),
			   		new DbParameter("po_cod_error", DbType.String,255,ParameterDirection.Output),
                    new DbParameter("po_des_error", DbType.String,255,ParameterDirection.Output)
                    #endregion
			};

            try
            {
                Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    DbFactory.ExecuteNonQuery(objRequest.Audit.Session, objRequest.Audit.Transaction, DbConnectionConfiguration.SIAC_POST_SGA,
                        DbCommandConfiguration.SIACU_VALIDACION_SUBTIPO_TRABAJO, parameters);

                });


                oResponse.COD_SUBTIPO_ORDEN = parameters[2].Value.ToString();
                oResponse.COD_SP = parameters[3].Value.ToString();
                oResponse.MSJ_SP = parameters[4].Value.ToString();

            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "GetValidationSubTypeWork OUT ");

            return oResponse;
        }

        public static Claro.SIACU.Entity.Transac.Service.Common.ListItem registraEta(string strIdSession, string strTransaction, string vCodSolot, string vSubTipoTra, string vFeProg, string vFranja, string vBucket)
        {
            DbParameter[] parameters = new DbParameter[] {
                    new DbParameter("an_codsolot",DbType.Int64,22,ParameterDirection.Input,int.Parse(vCodSolot)),                                                                                                                                                
                    new DbParameter("an_id_subtipo_orden",DbType.Int64,22,ParameterDirection.Input,int.Parse(vSubTipoTra)),
                    new DbParameter("ad_fecha",DbType.Date,ParameterDirection.Input,DateTime.Parse(vFeProg)),
					new DbParameter("av_cod_franja",DbType.String,30,ParameterDirection.Input,vFranja),
                    new DbParameter("av_bucket",DbType.String,50,ParameterDirection.Input,vBucket),
                    new DbParameter("Pn_Codigo_Respws",DbType.Int64,300,ParameterDirection.Output),
                    new DbParameter("Pv_Mensaje_Repws",DbType.String,100,ParameterDirection.Output)};

            
            try
            {
                DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_INSERT_PARM_VTA_PVTA_ADC, parameters);
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
                if (ex.InnerException.Message != null)
                {
                    Web.Logging.Info(strIdSession, strTransaction, ex.InnerException.Message);
                }
            }
            string rCodRes;
            string rDescRes;

            rCodRes = Convert.ToString(parameters[5].Value.ToString());
            rDescRes = Convert.ToString(parameters[6].Value.ToString());

            Claro.Web.Logging.Info(strIdSession, strTransaction, string.Format("registraEta - out  Res:{0} Des{1}", rCodRes == null ? "" : rCodRes, rDescRes == null ? "" : rDescRes));
            ListItem Item = new ListItem()
            {
                Code = rCodRes,
                Description = rDescRes
            };
            return Item;

        }

        public static Claro.SIACU.Entity.Transac.Service.Common.ListItem UpdateEta(string strIdSession, string strTransaction, string vCodSolot,string vPlano,string vCentroPoblado, string vSubTipoTra, DateTime vFeProg, string vFranja, string vBucket)
        {
            DbParameter[] parameters = new DbParameter[] {
                    new DbParameter("inCODSOLOT",DbType.Int64,22,ParameterDirection.Input,int.Parse(vCodSolot)),                                                                                                                                                
                    new DbParameter("inPLANO",DbType.String,30,ParameterDirection.Input,vPlano),
                    new DbParameter("inIDPOBLADO",DbType.String,30,ParameterDirection.Input,vCentroPoblado),
					new DbParameter("inSUBTIPO_ORDEN",DbType.String,30,ParameterDirection.Input,vSubTipoTra),
                    new DbParameter("inFECHA_PROGRA",DbType.Date,50,ParameterDirection.Input,vFeProg),
                    new DbParameter("inFRANJA",DbType.String,50,ParameterDirection.Input,vFranja),
                    new DbParameter("inIDBUCKET",DbType.String,50,ParameterDirection.Input,vBucket),
                    new DbParameter("ouCOD_ERR",DbType.String,300,ParameterDirection.Output),
                    new DbParameter("ouMSG_ERR",DbType.String,100,ParameterDirection.Output)};
            
            try
            {
                DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_UPDATE_PARM_VTA_PVTA_ADC, parameters);
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
                if (ex.InnerException.Message != null)
                {
                    Web.Logging.Info(strIdSession, strTransaction, ex.InnerException.Message);
                }
            }
            string rCodRes;
            string rDescRes;

            rCodRes = Convert.ToString(parameters[7].Value.ToString());
            rDescRes = Convert.ToString(parameters[8].Value.ToString());

            Claro.Web.Logging.Info(strIdSession, strTransaction, string.Format("UpdateEta - out  Res:{0} Des{1}", rCodRes == null ? "" : rCodRes, rDescRes == null ? "" : rDescRes));
            ListItem Item = new ListItem()
            {
                Code = rCodRes,
                Description = rDescRes
            };
            return Item;

        }


        public static Entity.Transac.Service.Fixed.GetDataServ.DataServByIdResponse GetDataServById(Entity.Transac.Service.Fixed.GetDataServ.DataServByIdRequest objRequest)
        {
            Entity.Transac.Service.Fixed.GetDataServ.DataServByIdResponse oResponse = new Entity.Transac.Service.Fixed.GetDataServ.DataServByIdResponse();
            DetailInteractionService item = new DetailInteractionService();
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_CONSULTA", DbType.Object, ParameterDirection.Output),
                new DbParameter("P_IDSERV", DbType.String,255, ParameterDirection.Input, objRequest.strIdServ),
              
            };
            DbFactory.ExecuteReader(objRequest.Audit.Session, objRequest.Audit.Transaction, DbConnectionConfiguration.SIAC_POST_PVU, DbCommandConfiguration.SIACU_POST_SP_OBTENER_SERVICIO, parameters, dr =>
            {
                oResponse.DataServById = new List<DetailInteractionService>();
                while (dr.Read())
                {
                    item = new DetailInteractionService();
                    item.IdServicio = CSTS.Functions.CheckStr(dr["IDSERVICIO"]);
                    item.GsrvcPrincipal = CSTS.Functions.CheckStr(dr["GSRVC_PRINCIPAL"]);
                    item.GsrvcCodigo = CSTS.Functions.CheckStr(dr["GSRVC_CODIGO"]);
                    item.Cantidad = CSTS.Functions.CheckStr(dr["CANTIDAD"]);
                    item.Servicio = CSTS.Functions.CheckStr(dr["SERVICIO"]);
                    item.Bandwid = CSTS.Functions.CheckStr(dr["BANDWID"]);
                    item.FlagLc = CSTS.Functions.CheckStr(dr["FLAG_LC"]);
                    item.CantidadIdLinea = CSTS.Functions.CheckStr(dr["CANTIDAD_IDLINEA"]);
                    item.IdEquipo = CSTS.Functions.CheckStr(dr["IDEQUIPO"]);
                    item.CodTipEqu = CSTS.Functions.CheckStr(dr["CODTIPEQU"]);
                    item.CantEquipo = CSTS.Functions.CheckStr(dr["CANT_EQUIPO"]);
                    item.Equipo = CSTS.Functions.CheckStr(dr["EQUIPO"]);

                    oResponse.DataServById.Add(item);
                }
            });
            return oResponse;
        }

        public static Entity.Transac.Service.Fixed.GetSavePostventa.DataSavePostventaDetServResponse SavePostventaDetServ(Entity.Transac.Service.Fixed.GetSavePostventa.DataSavePostventaDetServRequest objRequest)
        {
            Entity.Transac.Service.Fixed.GetSavePostventa.DataSavePostventaDetServResponse oResponse = new Entity.Transac.Service.Fixed.GetSavePostventa.DataSavePostventaDetServResponse();

            //DbParameter[] parameters =
            //{   
            //                        new DbParameter("IDINTERACCION", DbType.String, ParameterDirection.Input,oResponse.striInteractionId),
            //                        new DbParameter("SERVICIO", DbType.String,ParameterDirection.Input,oResponse.strServiceId), 
            //                        new DbParameter("IDGRUPO_PRINCIPAL", DbType.String, ParameterDirection.Input,oResponse.strIGroupPrincipalId),
            //                        new DbParameter("IDGRUPO", DbType.String,ParameterDirection.Input,oResponse.strIdGroupId),
            //                        new DbParameter("CANTIDAD_INSTANCIA",DbType.String,ParameterDirection.Input,oResponse.strQuantity_Intance_id), 
            //                        new DbParameter("DSCSRV", DbType.String, ParameterDirection.Input,oResponse.strDscsrv_Id),
            //                        new DbParameter("BANDWIND", DbType.String,ParameterDirection.Input,oResponse.strBandwid_Id), 
            //                        new DbParameter("FLAG_LC", DbType.String, ParameterDirection.Input,oResponse.strFlag_lc_Id),
            //                        new DbParameter("CANTIDAD_IDLINEA", DbType.String,ParameterDirection.Input,oResponse.strQuantity_idline_Id),
            //                        new DbParameter("TIPEQU", DbType.String, ParameterDirection.Input,oResponse.strTipequ_Id),
            //                        new DbParameter("CODTIPEQU", DbType.String,ParameterDirection.Input,oResponse.strCodigoTipequ_id), 
            //                        new DbParameter("CANTIDAD", DbType.String, ParameterDirection.Input,oResponse.strQuantity_Id),
            //                        new DbParameter("DSCEQU", DbType.String,ParameterDirection.Input,oResponse.strDscequ_Id),
            //                        new DbParameter("CODIGO_EXT", DbType.String,ParameterDirection.Input,oResponse.strCodigo_ext_Id),
            //                        new DbParameter("SNCODE", DbType.String,ParameterDirection.Input,oResponse.strSncode_Id),
            //                        new DbParameter("SPCODE", DbType.String,ParameterDirection.Input,oResponse.strSpcode_Id),
            //                        new DbParameter("FLAG_ACCION", DbType.String,ParameterDirection.Input,oResponse.strFlag_Id),
            //};
            //try
            //{
            //    Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
            //    {
            //        DbFactory.ExecuteNonQuery(objRequest.Audit.Session, objRequest.Audit.Transaction, DbConnectionConfiguration.SIAC_POST_SGA,
            //            DbCommandConfiguration.SIACU_SGASS_SP_INSERTAR_SERVICIO, parameters);
            //    });
            //}
            //catch (Exception ex)
            //{
            //    Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            //}

            return oResponse;
        }

        // avance
        public static Entity.Transac.Service.Fixed.GetDetEquipmentLTE.DataEquipmentResponse GetDetEquipo_LTE(Entity.Transac.Service.Fixed.GetDetEquipmentLTE.DataEquipmentRequest objRequest)
        {
            Entity.Transac.Service.Fixed.GetDetEquipmentLTE.DataEquipmentResponse oResponse = new Entity.Transac.Service.Fixed.GetDetEquipmentLTE.DataEquipmentResponse();
            DetEquipmentService item = new DetEquipmentService();
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("K_COD_ID", DbType.Int32, ParameterDirection.Input, objRequest.strK_cod_id),
                new DbParameter("K_CURSOR", DbType.Object, ParameterDirection.Output),
                new DbParameter("K_ERROR", DbType.Int32, ParameterDirection.Output),
                new DbParameter("K_MENSAJE", DbType.String,255, ParameterDirection.Output),

            };
            DbFactory.ExecuteReader(objRequest.Audit.Session, objRequest.Audit.Transaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_OPERATION_DET_EQUIPO_LTE, parameters, dr =>
            {
                oResponse.Data_k_cod_id = new List<DetEquipmentService>();
                while (dr.Read())
                {
                    item = new DetEquipmentService();

                    item.strDscequ = CSTS.Functions.CheckStr(dr["DSCEQU"]);
                    item.strTipsrv = CSTS.Functions.CheckStr(dr["TIPSRV"]);
                    item.strCodsrv = CSTS.Functions.CheckStr(dr["CODSRV"]);
                    item.strTipo_srv = CSTS.Functions.CheckStr(dr["TIPO_SRV"]);
                    item.intCargo_Fijo = CSTS.Functions.CheckStr(dr["CARGO_FIJO"]);
                    item.intCantidad = CSTS.Functions.CheckStr(dr["CANTIDAD"]);
                    item.strTipo = CSTS.Functions.CheckStr(dr["TIPO"]);
                   
                    oResponse.Data_k_cod_id.Add(item);
                }
            });
            return oResponse;
        }

        

        public static EntitiesFixed.BlackWhiteList.GetStateLineEmail.SearchStateLineEmailResponse SearchStateLineEmail(EntitiesFixed.BlackWhiteList.GetStateLineEmail.SearchStateLineEmailRequest objRequest)
        {
         
            EntitiesFixed.BlackWhiteList.GetStateLineEmail.SearchStateLineEmailResponse Response = new EntitiesFixed.BlackWhiteList.GetStateLineEmail.SearchStateLineEmailResponse();
            try
            {
                Response = Tools.Connections.Rest.RestService.PostDataPowerInvoque<EntitiesFixed.BlackWhiteList.GetStateLineEmail.SearchStateLineEmailResponse>(Claro.SIACU.Data.Transac.Service.Configuration.RestServiceConfiguration.BLACK_WHITE_LIST_DP_CONSULTA, objRequest.Audit, objRequest, false, Common.GetCredentials("usrDPBlackWhite", "USRDPPassBlackWhite"));
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
                throw new Claro.MessageException(objRequest.Audit.Transaction);
            }

            return Response;
        }

        public static EntitiesFixed.BlackWhiteList.GetUpdateStateLineEmail.UpdateStateLineEmailResponse UpdateStateLineEmail(EntitiesFixed.BlackWhiteList.GetUpdateStateLineEmail.UpdateStateLineEmailRequest objRequest)
        {

            EntitiesFixed.BlackWhiteList.GetUpdateStateLineEmail.UpdateStateLineEmailResponse Response = new EntitiesFixed.BlackWhiteList.GetUpdateStateLineEmail.UpdateStateLineEmailResponse();
            try
            {
                Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "Consumiento el servicio UpdateStateLineEmail ");
                Response = Tools.Connections.Rest.RestService.PostDataPowerInvoque<EntitiesFixed.BlackWhiteList.GetUpdateStateLineEmail.UpdateStateLineEmailResponse>(Claro.SIACU.Data.Transac.Service.Configuration.RestServiceConfiguration.BLACK_WHITE_LIST_DP_ACTUALIZAR, objRequest.Audit, objRequest, false, Common.GetCredentials("usrDPBlackWhite", "USRDPPassBlackWhite"));
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
                throw new Claro.MessageException(objRequest.Audit.Transaction);
            }

            return Response;
        }
             

      
        public static string getAcceptRequest(Claro.Entity.AuditRequest Audit)
        {
            string keyAccept = "";
            try
            {
                keyAccept = Claro.ConfigurationManager.AppSettings("keyAcceptRequestRest");
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(Audit.Session, Audit.Transaction, ex.Message);
                keyAccept = "application/json";
            }
            return keyAccept;
        }
        public static bool getActivateLogRequestRest(Claro.Entity.AuditRequest Audit)
        {
            bool keyActivateLog = false;
            string keyLogRest = "";
            try
            {
                keyLogRest = Claro.ConfigurationManager.AppSettings("flagLogRequestRest");
                keyActivateLog = keyLogRest == "1" ? true : false;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(Audit.Session, Audit.Transaction, ex.Message);
                keyActivateLog = false;
            }
            return keyActivateLog;
        }


        public static List<lineaConsolidada> GetListValidateLine(string strIdSession,
                                                          string idTransaccion,                                              
                                                          string ipAplicacion,
                                                          string nombreAplicacion,
                                                          string usuarioAplicacion,
                                                          string strDNI, 
                                                          string straplicativo, 
                                                          string StrTipoDoc, 
                                                          string strnombreCampo,
                                                          ref string codigoRespuesta, 
                                                          ref string mensajeRespuesta)
        {
            bool Respuesta = false;
            int cantidad = 0;

            List <lineaConsolidada> ArrListaLineas = new List<lineaConsolidada>();
            Claro.SIACU.ProxyService.Transac.Service.SIACFixed.ValidarLineasCliente.AuditRequest objAuditoria = 
                new ProxyService.Transac.Service.SIACFixed.ValidarLineasCliente.AuditRequest();


            Claro.SIACU.ProxyService.Transac.Service.SIACFixed.ValidarLineasCliente.contarLineasRequest objRequest = 
                new ProxyService.Transac.Service.SIACFixed.ValidarLineasCliente.contarLineasRequest();

            Claro.SIACU.ProxyService.Transac.Service.SIACFixed.ValidarLineasCliente.contarLineasResponse objResponse =
                new ProxyService.Transac.Service.SIACFixed.ValidarLineasCliente.contarLineasResponse();

            Claro.SIACU.ProxyService.Transac.Service.SIACFixed.ValidarLineasCliente.ListaCamposAdicionalesTypeCampoAdicional[] objRequestListCampo =
                new ProxyService.Transac.Service.SIACFixed.ValidarLineasCliente.ListaCamposAdicionalesTypeCampoAdicional[1];

            Claro.SIACU.ProxyService.Transac.Service.SIACFixed.ValidarLineasCliente.ListaCamposAdicionalesTypeCampoAdicional objRequestCampo =
                new ProxyService.Transac.Service.SIACFixed.ValidarLineasCliente.ListaCamposAdicionalesTypeCampoAdicional();

            try
            {
                objAuditoria.idTransaccion = idTransaccion;
                objAuditoria.ipAplicacion = ipAplicacion;
                objAuditoria.nombreAplicacion = nombreAplicacion;
                objAuditoria.usuarioAplicacion = usuarioAplicacion;

                objRequest.auditRequest = objAuditoria;
                objRequest.numeroDocumento = strDNI;

                objRequestCampo.nombreCampo = strnombreCampo; //Llave
                objRequestCampo.valor = StrTipoDoc;
                objRequestListCampo[0] = objRequestCampo;

                objRequest.listaCamposAdicionales = objRequestListCampo;

                objResponse = Web.Logging.ExecuteMethod(strIdSession, idTransaccion, () =>
                {
                    return ServiceConfiguration.ValidateLine.contarLineas(objRequest);
                });


                mensajeRespuesta = objResponse.auditResponse.msjRespuesta;
                codigoRespuesta = objResponse.auditResponse.codRespuesta;

                if (Convert.ToInt64(codigoRespuesta) == 0)
                {
                    if (objResponse != null)
                    {
                        if (objResponse.listaLineasConsolidadasType.Length > 0)
                        {
                            cantidad = objResponse.listaLineasConsolidadasType.Length;
                            for (int i = 0; i < cantidad; i++)
                            {
                                lineaConsolidada obj = new lineaConsolidada();
                                obj.msisdn = objResponse.listaLineasConsolidadasType[i].msisdn;
                                obj.segmento = objResponse.listaLineasConsolidadasType[i].segmento;
                                ArrListaLineas.Add(obj);                                
                            }
                        }
                    }
                }
                Respuesta = true;
            }
            catch (Exception ex)
            {
                //log.Info(string.Format("Error(): {0}", ex.Message));
                Respuesta = false;
            }
            finally
            {
                objRequest = null;
                objResponse = null;
                objAuditoria = null;

            }
            return ArrListaLineas;
        }


        public static bool GetInsertNoCliente(Claro.SIACU.Entity.Transac.Service.Fixed.Customer objRequest)
        {
            bool salida = false;

            DbParameter[] arrParam = {
                new DbParameter("p_phone", DbType.String, ParameterDirection.Input, objRequest.TELEFONO),
                new DbParameter("p_usuario", DbType.String, ParameterDirection.Input, objRequest.USUARIO),
                new DbParameter("p_nombres", DbType.String, 255, ParameterDirection.Input, objRequest.NOMBRES),
                new DbParameter("p_apellidos", DbType.String, 255, ParameterDirection.Input, objRequest.APELLIDOS),
                new DbParameter("p_razonsocial", DbType.String, 255, ParameterDirection.Input, objRequest.RAZON_SOCIAL),
                new DbParameter("p_tipo_doc", DbType.String,255, ParameterDirection.Input, objRequest.TIPO_DOC),
                new DbParameter("p_num_doc", DbType.String, 255, ParameterDirection.Input, objRequest.NRO_DOC),
                new DbParameter("p_domicilio", DbType.String, 255, ParameterDirection.Input, objRequest.DOMICILIO),
                new DbParameter("p_distrito", DbType.String, 255, ParameterDirection.Input, objRequest.DISTRITO),
                new DbParameter("p_departamento", DbType.String, 255, ParameterDirection.Input, objRequest.DEPARTAMENTO),
                new DbParameter("p_modalidad", DbType.String, 255, ParameterDirection.Input, objRequest.MODALIDAD),
                new DbParameter("p_flag_insert",DbType.String,100,ParameterDirection.Output),
                new DbParameter("p_msg_text",DbType.String,300,ParameterDirection.Output)
                };
            try
            {

                Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    DbFactory.ExecuteNonQuery(objRequest.Audit.Session, objRequest.Audit.Transaction, DbConnectionConfiguration.SIAC_POST_CLARIFY, DbCommandConfiguration.SIACU_SP_SP_CREATE_CONTACT_NO_USER_PORT, arrParam);
                });

                string rCodRes;
                string rDescRes;

                rCodRes = Convert.ToString(arrParam[11].Value.ToString());
                rDescRes = Convert.ToString(arrParam[12].Value.ToString());

                Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Session, string.Format("GetInsertNoCliente - out  Res:{0} Des{1}", rCodRes == null ? "" : rCodRes, rDescRes == null ? "" : rDescRes));

                if (rCodRes == "OK")
                {
                    salida = true;
                }
                else { 
                    salida = false;
                } 
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.ToString());
                salida = false;
            }
            return salida;
        }

        public static EntitiesFixed.Discard.GetConsultDiscardRTI.ConsultDiscardRTIResponse ConsultDiscardRTI(EntitiesFixed.Discard.GetConsultDiscardRTI.ConsultDiscardRTIRequest objRequest)
        {
            Claro.Web.Logging.Info("", "", "Inicio Metodo ConsultDiscardRTI");
            EntitiesFixed.Discard.GetConsultDiscardRTI.ConsultDiscardRTIResponse Response = new EntitiesFixed.Discard.GetConsultDiscardRTI.ConsultDiscardRTIResponse();
            try{
                var json_object_resq = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
                Claro.Web.Logging.Info("IdSession: " + objRequest.Audit.Session, "Transaccion: " + objRequest.Audit.Transaction, "Data ConsultDiscardRTI --> Parametros de entrada: " + json_object_resq);
                Response = Tools.Connections.Rest.RestService.PostDataPowerInvoque<EntitiesFixed.Discard.GetConsultDiscardRTI.ConsultDiscardRTIResponse>(Claro.SIACU.Data.Transac.Service.Configuration.RestServiceConfiguration.DESCARTES_DP_CONSULTAR_DESCARTES_RTI, objRequest.Audit, objRequest, false, Common.GetCredentials("usrDPDescartes", "PassDPDescartes"));
                var json_object_resp = Newtonsoft.Json.JsonConvert.SerializeObject(Response);
                Claro.Web.Logging.Info("IdSession: " + objRequest.Audit.Session, "Transaccion: " + objRequest.Audit.Transaction, "Data ConsultDiscardRTI --> Parametros de salida: " + json_object_resp);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
                throw new Claro.MessageException(objRequest.Audit.Transaction);
            }
            Claro.Web.Logging.Info("", "", "Fin Metodo ConsultDiscardRTI");
            return Response;
        }      

        public static EntitiesFixed.Discard.GetConsultDiscardRTI.ConsultDiscardRTIResponse ConsultDiscardGrupoRTI(EntitiesFixed.Discard.GetConsultDiscardRTI.ConsultDiscardRTIRequestGrupo objRequest)
        {
            Claro.Web.Logging.Info("", "", "Inicio Metodo ConsultDiscardGrupoRTI");
            EntitiesFixed.Discard.GetConsultDiscardRTI.ConsultDiscardRTIResponse Response = new EntitiesFixed.Discard.GetConsultDiscardRTI.ConsultDiscardRTIResponse();
            try{
                var json_object_resq = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
                Claro.Web.Logging.Info("IdSession: " + objRequest.Audit.Session, "Transaccion: " + objRequest.Audit.Transaction, "Data ConsultDiscardGrupoRTI --> Parametros de entrada: " + json_object_resq);

                Response = Tools.Connections.Rest.RestService.PostDataPowerInvoque<EntitiesFixed.Discard.GetConsultDiscardRTI.ConsultDiscardRTIResponse>
                    (Claro.SIACU.Data.Transac.Service.Configuration.RestServiceConfiguration.DESCARTES_DP_CONSULTAR_DESCARTES_GRUPO_RTI, objRequest.Audit, objRequest, false, Common.GetCredentials("usrDPDescartes", "PassDPDescartes"));

                var json_object_resp = Newtonsoft.Json.JsonConvert.SerializeObject(Response);
                Claro.Web.Logging.Info("IdSession: " + objRequest.Audit.Session, "Transaccion: " + objRequest.Audit.Transaction, "Data ConsultDiscardGrupoRTI --> Parametros de salida: " + json_object_resp);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
                throw new Claro.MessageException(objRequest.Audit.Transaction);
            }
            Claro.Web.Logging.Info("", "", "Fin Metodo ConsultDiscardGrupoRTI");
            return Response;
        }      

        public static EntitiesFixed.Discard.GetConsultDiscardRTITOBE.ConsultDiscartRTITOBEResponse ConsultDiscardRTIToBe(EntitiesFixed.Discard.GetConsultDiscardRTITOBE.ConsultDiscartRTITOBERequest objRequest, Tools.Entity.AuditRequest audirRequest)
        {
            Claro.Web.Logging.Info("", "", "Inicio Metodo ConsultDiscardRTIToBe");
            EntitiesFixed.Discard.GetConsultDiscardRTITOBE.ConsultDiscartRTITOBEResponse Response = new EntitiesFixed.Discard.GetConsultDiscardRTITOBE.ConsultDiscartRTITOBEResponse();
            try
            {
                Hashtable audit = new Hashtable();
                audit.Add("idTransaccion", audirRequest.Transaction);
                audit.Add("msgId", objRequest.consultarDescartesRtiRequest.msisdn);
                audit.Add("timestamp", DateTime.Now.ToString("yyyy-MM-dd'T'HH:mm:ss'Z'"));
                audit.Add("userId", audirRequest.UserName);
                var json_object_resq = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
                Claro.Web.Logging.Info("IdSession: " , "Transaccion: " + audirRequest.Transaction, "Data ConsultDiscardRTI --> Parametros de entrada: " + json_object_resq);
                Response = Claro.Data.RestService.PostInvoqueSDP<EntitiesFixed.Discard.GetConsultDiscardRTITOBE.ConsultDiscartRTITOBEResponse>(Claro.SIACU.Data.Transac.Service.Configuration.RestServiceConfiguration.DESCARTES_CONSULTAR_DESCARTES_RTI_TOBE, audit, objRequest);
                var json_object_resp = Newtonsoft.Json.JsonConvert.SerializeObject(Response);
                Claro.Web.Logging.Info("IdSession: ", "Transaccion: " + audirRequest.Transaction, "Data ConsultDiscardRTI --> Parametros de salida: " + json_object_resp);            
            }
            catch (Exception ex){
                Claro.Web.Logging.Error(audirRequest.Session, audirRequest.Transaction, ex.Message);
                throw new Claro.MessageException(audirRequest.Transaction);
            }
            Claro.Web.Logging.Info("", "", "Fin Metodo ConsultDiscardRTIToBe");
            return Response;
        }     

        public static EntitiesFixed.Discard.GetConsultDiscardRTITOBE.ConsultDiscartRTITOBEResponse ConsultDiscardRTIToBeGrupo(EntitiesFixed.Discard.GetConsultDiscardRTITOBE.ConsultDiscartRTITOBERequestGrupo objRequest, Tools.Entity.AuditRequest audirRequest)
        {
            EntitiesFixed.Discard.GetConsultDiscardRTITOBE.ConsultDiscartRTITOBEResponse Response = new EntitiesFixed.Discard.GetConsultDiscardRTITOBE.ConsultDiscartRTITOBEResponse();
            try
            {
                Hashtable audit = new Hashtable();
                audit.Add("idTransaccion", audirRequest.Transaction);
                audit.Add("msgId", objRequest.consultarDescartesRtiRequest.msisdn);
                audit.Add("timestamp", DateTime.Now.ToString("yyyy-MM-dd'T'HH:mm:ss'Z'"));
                audit.Add("userId", audirRequest.UserName);
                Response = Claro.Data.RestService.PostInvoqueSDP<EntitiesFixed.Discard.GetConsultDiscardRTITOBE.ConsultDiscartRTITOBEResponse>(Claro.SIACU.Data.Transac.Service.Configuration.RestServiceConfiguration.DESCARTES_CONSULTAR_DESCARTES_RTI_TOBE_GRUPO, audit, objRequest);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audirRequest.Session, audirRequest.Transaction, ex.Message);
                throw new Claro.MessageException(audirRequest.Transaction);
            }
            return Response;
        }     

         public static EntitiesFixed.ClaroVideo.GetConsultClientSN.ConsultClientSNResponse ConsultClientSN(EntitiesFixed.ClaroVideo.GetConsultClientSN.ConsultClientSNRequest objRequest)
        {

            EntitiesFixed.ClaroVideo.GetConsultClientSN.ConsultClientSNResponse Response = new EntitiesFixed.ClaroVideo.GetConsultClientSN.ConsultClientSNResponse();
            try
            {
                Response = Tools.Connections.Rest.RestService.PostDataPowerInvoque<EntitiesFixed.ClaroVideo.GetConsultClientSN.ConsultClientSNResponse>(Claro.SIACU.Data.Transac.Service.Configuration.RestServiceConfiguration.CLARO_VIDEO_DP_CONSULTA_CLIENTE, objRequest.Audit, objRequest, false, Common.GetCredentials("usrDPBClaroVideo", "PassDPClaroVideo"));
            

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
                throw new Claro.MessageException(objRequest.Audit.Transaction);
            }

            return Response;
        }

        public static EntitiesFixed.ClaroVideo.GetConsultSN.ConsultSNResponse ConsultSN(EntitiesFixed.ClaroVideo.GetConsultSN.ConsultSNRequest objRequest)
        {

            EntitiesFixed.ClaroVideo.GetConsultSN.ConsultSNResponse Response = new EntitiesFixed.ClaroVideo.GetConsultSN.ConsultSNResponse();
            try
            {
              
                Response = Tools.Connections.Rest.RestService.PostDataPowerInvoque<EntitiesFixed.ClaroVideo.GetConsultSN.ConsultSNResponse>(Claro.SIACU.Data.Transac.Service.Configuration.RestServiceConfiguration.CLARO_VIDEO_DP_CONSULTA_SN, objRequest.Audit, objRequest, false, Common.GetCredentials("usrDPBClaroVideo", "PassDPClaroVideo"));
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
                throw new Claro.MessageException(objRequest.Audit.Transaction);
            }

            return Response;
        }

        public static EntitiesFixed.ClaroVideo.GetProvisionSubscription.ProvisionSubscriptionResponse ProvisionSubscription(EntitiesFixed.ClaroVideo.GetProvisionSubscription.ProvisionSubscriptionRequest objRequest)
        {

            EntitiesFixed.ClaroVideo.GetProvisionSubscription.ProvisionSubscriptionResponse Response = new EntitiesFixed.ClaroVideo.GetProvisionSubscription.ProvisionSubscriptionResponse();
            try
            {
                 Response = Tools.Connections.Rest.RestService.PostDataPowerInvoque<EntitiesFixed.ClaroVideo.GetProvisionSubscription.ProvisionSubscriptionResponse>(Claro.SIACU.Data.Transac.Service.Configuration.RestServiceConfiguration.CLARO_VIDEO_DP_SUBSCRIPTION_SN, objRequest.Audit, objRequest, false, Common.GetCredentials("usrDPBClaroVideo", "PassDPClaroVideo"));
               
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
                throw new Claro.MessageException(objRequest.Audit.Transaction);
            }

            return Response;
        }


        public static EntitiesFixed.ClaroVideo.GetCancelSubscriptionSN.CancelSubscriptionSNResponse CancelSubscriptionSN(EntitiesFixed.ClaroVideo.GetCancelSubscriptionSN.CancelSubscriptionSNRequest objRequest)
        {

            EntitiesFixed.ClaroVideo.GetCancelSubscriptionSN.CancelSubscriptionSNResponse Response = new EntitiesFixed.ClaroVideo.GetCancelSubscriptionSN.CancelSubscriptionSNResponse();
            try
            {
                Response = Tools.Connections.Rest.RestService.PostDataPowerInvoque<EntitiesFixed.ClaroVideo.GetCancelSubscriptionSN.CancelSubscriptionSNResponse>(Claro.SIACU.Data.Transac.Service.Configuration.RestServiceConfiguration.CLARO_VIDEO_DP_CANCEL_SUBSCRIPTION_SN, objRequest.Audit, objRequest, false, Common.GetCredentials("usrDPBClaroVideo", "PassDPClaroVideo"));
           
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
                throw new Claro.MessageException(objRequest.Audit.Transaction);
            }

            return Response;
        }

        public static EntitiesFixed.ClaroVideo.GetUpdateClientSN.UpdateClientSNResponse UpdateClientSN(EntitiesFixed.ClaroVideo.GetUpdateClientSN.UpdateClientSNRequest objRequest)
        {

            EntitiesFixed.ClaroVideo.GetUpdateClientSN.UpdateClientSNResponse Response = new EntitiesFixed.ClaroVideo.GetUpdateClientSN.UpdateClientSNResponse();
            try
            {
             
                Response = Tools.Connections.Rest.RestService.PostDataPowerInvoque<EntitiesFixed.ClaroVideo.GetUpdateClientSN.UpdateClientSNResponse>(Claro.SIACU.Data.Transac.Service.Configuration.RestServiceConfiguration.CLARO_VIDEO_DP_UPDATE_CLIENT_SN, objRequest.Audit, objRequest, false, Common.GetCredentials("usrDPBClaroVideo", "PassDPClaroVideo"));
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
                throw new Claro.MessageException(objRequest.Audit.Transaction);
            }

            return Response;
        }

        public static EntitiesFixed.ClaroVideo.GetHistoryDevice.HistoryDeviceResponse HistoryDevice(EntitiesFixed.ClaroVideo.GetHistoryDevice.HistoryDeviceRequest objRequest)
        {
            EntitiesFixed.ClaroVideo.GetHistoryDevice.HistoryDeviceResponse Response = new EntitiesFixed.ClaroVideo.GetHistoryDevice.HistoryDeviceResponse();
            try
            {

             
                Response = Tools.Connections.Rest.RestService.PostDataPowerInvoque<EntitiesFixed.ClaroVideo.GetHistoryDevice.HistoryDeviceResponse>(Claro.SIACU.Data.Transac.Service.Configuration.RestServiceConfiguration.CLARO_VIDEO_DP_HISTORY_DEVICE_SN, objRequest.Audit, objRequest, false, Common.GetCredentials("usrDPBClaroVideo", "PassDPClaroVideo"));
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
                throw new Claro.MessageException(objRequest.Audit.Transaction);
            }
            return Response;
        }
        public static EntitiesFixed.ClaroVideo.GetRegisterClientSN.RegisterClientSNResponse RegisterClientSN(EntitiesFixed.ClaroVideo.GetRegisterClientSN.RegisterClientSNRequest objRequest)
        {
            EntitiesFixed.ClaroVideo.GetRegisterClientSN.RegisterClientSNResponse Response = new EntitiesFixed.ClaroVideo.GetRegisterClientSN.RegisterClientSNResponse();
            try
            {
             
                 Response = Tools.Connections.Rest.RestService.PostDataPowerInvoque<EntitiesFixed.ClaroVideo.GetRegisterClientSN.RegisterClientSNResponse>(Claro.SIACU.Data.Transac.Service.Configuration.RestServiceConfiguration.CLARO_VIDEO_DP_REGISTER_CLIENT_SN, objRequest.Audit, objRequest, false, Common.GetCredentials("usrDPBClaroVideo", "PassDPClaroVideo"));
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
                throw new Claro.MessageException(objRequest.Audit.Transaction);
            }
            return Response;
        }

        public static EntitiesFixed.ClaroVideo.GetRegistrarControles.RegistrarControlesResponse RegistrarControles(EntitiesFixed.ClaroVideo.GetRegistrarControles.RegistrarControlesRequest objRequest)
        {
            EntitiesFixed.ClaroVideo.GetRegistrarControles.RegistrarControlesResponse Response = new EntitiesFixed.ClaroVideo.GetRegistrarControles.RegistrarControlesResponse();
            try
            {
            
                Response = Tools.Connections.Rest.RestService.PostDataPowerInvoque<EntitiesFixed.ClaroVideo.GetRegistrarControles.RegistrarControlesResponse>(Claro.SIACU.Data.Transac.Service.Configuration.RestServiceConfiguration.CLARO_VIDEO_DP_REGISTER_CONTROLES, objRequest.Audit, objRequest, false, Common.GetCredentials("usrDPBClaroVideo", "PassDPClaroVideo"));
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
                throw new Claro.MessageException(objRequest.Audit.Transaction);
            }
            return Response;
        }

        public static EntitiesFixed.ClaroVideo.GetValidateElegibility.ValidateElegibilityResponse ValidateElegibility(EntitiesFixed.ClaroVideo.GetValidateElegibility.ValidateElegibilityRequest objRequest)
        {

            EntitiesFixed.ClaroVideo.GetValidateElegibility.ValidateElegibilityResponse Response = new EntitiesFixed.ClaroVideo.GetValidateElegibility.ValidateElegibilityResponse();
            try
            {
                
                 Response = Tools.Connections.Rest.RestService.PostDataPowerInvoque<EntitiesFixed.ClaroVideo.GetValidateElegibility.ValidateElegibilityResponse>(Claro.SIACU.Data.Transac.Service.Configuration.RestServiceConfiguration.CLARO_VIDEO_DP_VALIDATEELIBILITY, objRequest.Audit, objRequest, false, Common.GetCredentials("usrDPBClaroVideo", "PassDPClaroVideo"));
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
                throw new Claro.MessageException(objRequest.Audit.Transaction);
            }

            return Response;
        }
        public static List<EntitiesFixed.ClaroVideo.ContractServices> GetContractServicesHFCLTE(string strIdSession, string strTransaction, string strContractId)
        {
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("p_co_id", DbType.Int32, ParameterDirection.Input, strContractId),
                new DbParameter("p_tmdes", DbType.String,100, ParameterDirection.Output),
                new DbParameter("p_tmcode", DbType.Int32, ParameterDirection.Output),
                new DbParameter("p_cursor", DbType.Object, ParameterDirection.Output),
                new DbParameter("v_errnum", DbType.Int32, ParameterDirection.Output),
                new DbParameter("v_errmsj", DbType.String,200, ParameterDirection.Output)                
            };

            List<EntitiesFixed.ClaroVideo.ContractServices> listContractServices = null;
            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_SP_LISTA_SERVICIOS_USADOS_HFC, parameters, (IDataReader reader) =>
            {
                listContractServices = new List<EntitiesFixed.ClaroVideo.ContractServices>();

                while (reader.Read())
                {
                    listContractServices.Add(new EntitiesFixed.ClaroVideo.ContractServices()
                    {
                        DES_GRUPO = Convert.ToString(reader["de_grp"]),
                        POS_GRUPO = Convert.ToString(reader["no_grp"]),
                        COD_SERV = Convert.ToString(reader["co_ser"]),
                        DES_SERV = Convert.ToString(reader["de_ser"]),
                        POS_SERV = Convert.ToString(reader["no_ser"]),
                        COD_EXCLUYENTE = Convert.ToString(reader["co_excl"]),
                        DES_EXCLUYENTE = Convert.ToString(reader["de_excl"]),
                        ESTADO = Convert.ToString(reader["estado"]),
                        FECHA_VALIDEZ = Convert.ToString(reader["valido_desde"]),
                        MONTO_CARGO_SUS = Convert.ToString(reader["suscrip"]),
                        MONTO_CARGO_FIJO = Convert.ToString(reader["cargofijo"]),
                        CUOTA_MODIF = Convert.ToString(reader["cuota"]),
                        PERIODOS_VALIDOS = Convert.ToString(reader["periodos"]),
                        BLOQUEO_ACT = Convert.ToString(reader["bloq_act"]),
                        BLOQUEO_DESACT = Convert.ToString(reader["bloq_des"]),
                        SPCODE = Convert.ToString(reader["spcode"]),
                        SNCODE = Convert.ToString(reader["sncode"])
                    });
                }
            });

            return listContractServices;
        }
        public static EntitiesFixed.ClaroVideo.GetPersonalizaMensajeOTT.PersonalizaMensajeOTTResponse PersonalizaMensajeOTT(EntitiesFixed.ClaroVideo.GetPersonalizaMensajeOTT.PersonalizaMensajeOTTRequest objRequest)
        {

            EntitiesFixed.ClaroVideo.GetPersonalizaMensajeOTT.PersonalizaMensajeOTTResponse Response = new EntitiesFixed.ClaroVideo.GetPersonalizaMensajeOTT.PersonalizaMensajeOTTResponse();
            try
            {
                
                Response = Tools.Connections.Rest.RestService.PostDataPowerInvoque<EntitiesFixed.ClaroVideo.GetPersonalizaMensajeOTT.PersonalizaMensajeOTTResponse>(Claro.SIACU.Data.Transac.Service.Configuration.RestServiceConfiguration.CLARO_VIDEO_DP_PERSONALIZAMENSAJEOTT, objRequest.Audit, objRequest, false, Common.GetCredentials("usrDPBClaroVideo", "PassDPClaroVideo"));
              
            
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
                throw new Claro.MessageException(objRequest.Audit.Transaction);
            }

            return Response;
        }

    #region PROY-140510 - AMCO - Modulo de consulta y eliminar cuenta de Claro Video
        public static EntitiesFixed.ClaroVideo.GetDeleteClientSN.DeleteClientSNResponse DeleteClientSN(EntitiesFixed.ClaroVideo.GetDeleteClientSN.DeleteClientSNRequest objRequest)
        {
            EntitiesFixed.ClaroVideo.GetDeleteClientSN.DeleteClientSNResponse objResponse = new EntitiesFixed.ClaroVideo.GetDeleteClientSN.DeleteClientSNResponse();
            try
            {
                objResponse = Tools.Connections.Rest.RestService.PostDataPowerInvoque<EntitiesFixed.ClaroVideo.GetDeleteClientSN.DeleteClientSNResponse>(Claro.SIACU.Data.Transac.Service.Configuration.RestServiceConfiguration.CLARO_VIDEO_DP_DELETE_CLIENT_SN, objRequest.Audit, objRequest, false, Common.GetCredentials("usrDPBClaroVideo","PassDPClaroVideo"));
                var objResponseLog = Newtonsoft.Json.JsonConvert.SerializeObject(objResponse);
                Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, string.Format("Método GetDeleteClientSN: {0}", objResponseLog));
            }
            catch (Exception ex)
            {
                string sep = "-";
                int postResponse = ex.Message.IndexOf(sep);
                string result = ex.Message.Substring(postResponse + sep.Length);
                objResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<EntitiesFixed.ClaroVideo.GetDeleteClientSN.DeleteClientSNResponse>(result);
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, string.Format("Método GetDeleteClientSN, Error: ", objResponse));

            }
            return objResponse;
        }

        public static POSTPREDATA.consultarDatosLineaResponse GetConsultaDatosLinea(Tools.Entity.AuditRequest objaudit, string strTelephone)
        {
            List<Claro.SIACU.Entity.Transac.Service.Fixed.Parameter> lstParameter = new List<Claro.SIACU.Entity.Transac.Service.Fixed.Parameter>();
            lstParameter.Add(
                    new Claro.SIACU.Entity.Transac.Service.Fixed.Parameter()
                    {
                        PARAMETRO_ID = 0,
                        CAMPO = "",
                        VALOR = ""

                    });
            POSTPREDATA.consultarDatosLineaResponse objResponse = null;
            try
            {
                POSTPREDATA.auditRequestType objAuditRequest = new POSTPREDATA.auditRequestType()
                {
                    idTransaccion = objaudit.Transaction,
                    ipAplicacion = objaudit.IPAddress,
                    nombreAplicacion = objaudit.ApplicationName,
                    usuarioAplicacion = objaudit.UserName
                };
                POSTPREDATA.modificadorType[] listmodificadorType = new POSTPREDATA.modificadorType[0];
                POSTPREDATA.operacionesType[] objlistaOperacionesConsulta = new POSTPREDATA.operacionesType[0];
                POSTPREDATA.consultaPrepagoType objconsultaPrepagoType = new POSTPREDATA.consultaPrepagoType(){

                    listaOperacionesConsulta = objlistaOperacionesConsulta
                };
                POSTPREDATA.parametroType[] objparametrosConsultaPostpagoType = new POSTPREDATA.parametroType[lstParameter.Count];
                foreach (var item in lstParameter)
                {
                    POSTPREDATA.parametroType objparametroType = new POSTPREDATA.parametroType { nombre = item.CAMPO, valor = item.VALOR };
                    objparametrosConsultaPostpagoType[Convert.ToInt(item.PARAMETRO_ID)] = objparametroType;
                }
                POSTPREDATA.consultaPostpagoType objconsultaPostpagoType = new POSTPREDATA.consultaPostpagoType(){

                    parametrosConsultaPostpago = objparametrosConsultaPostpagoType
                };
                POSTPREDATA.parametrosTypeObjetoOpcional[] objlistaRequestOpcionalType = new POSTPREDATA.parametrosTypeObjetoOpcional[lstParameter.Count];
                foreach (var item in lstParameter)
                {
                    POSTPREDATA.parametrosTypeObjetoOpcional objparametrosTypeObjetoOpcional = new POSTPREDATA.parametrosTypeObjetoOpcional { campo = item.CAMPO, valor = item.VALOR };
                    objlistaRequestOpcionalType[Convert.ToInt(item.PARAMETRO_ID)] = objparametrosTypeObjetoOpcional;
                }

                POSTPREDATA.consultarDatosLineaRequest objconsultarDatosLineaRequest = new POSTPREDATA.consultarDatosLineaRequest()
                {
                    auditRequest = objAuditRequest,
                    msisdn = strTelephone,
                    tipoConsulta= KEY.AppSettings("gTypeConsultDataObtenerLinea"),
                    consultaPrepago = objconsultaPrepagoType,
                    consultaPostpago = objconsultaPostpagoType,
                    listaRequestOpcional = objlistaRequestOpcionalType
                };

                POSTPREDATA.listaResponseOpcionalTypeObjetoResponseOpcional[] lstparametrosTypeObjetoOpcional = new POSTPREDATA.listaResponseOpcionalTypeObjetoResponseOpcional[0];

                POSTPREDATA.ConsultaDatosPrePostWSService objConsultaDatosPrePostWSService = new POSTPREDATA.ConsultaDatosPrePostWSService()
                {
                    
                    Url = KEY.AppSettings("gConstUrlConsultaSaldoWS"),
                    Credentials = System.Net.CredentialCache.DefaultCredentials,
                    Timeout = Int32.Parse(KEY.AppSettings("intTimeoutDataPrePostWS"))
                };

                objResponse = Claro.Web.Logging.ExecuteMethod<POSTPREDATA.consultarDatosLineaResponse>
                       (objAuditRequest.idTransaccion, objAuditRequest.idTransaccion, () =>
                {
                    return objConsultaDatosPrePostWSService.consultarDatosLinea(objconsultarDatosLineaRequest);
                });

            }
             catch (Exception ex)
             {
                 Claro.Web.Logging.Info(objaudit.Session, objaudit.Transaction, ex.Message);
                 throw new Claro.MessageException(objaudit.Transaction);
             }

            return objResponse;
        }

        public static ENTITIES_CONSULTALINEA.ConsultaLineaResponse ConsultarLineaCuenta(ENTITIES_CONSULTALINEA.ConsultaLineaRequest objConsultaLineaRequest)
        {
            Claro.Web.Logging.Info("IdSession: " + objConsultaLineaRequest.Audit.Session,
               "Transaccion: " + objConsultaLineaRequest.Audit.Transaction,
               string.Format("Capa Data-Metodo: {0}, Type:{1}, Value{2} ", "ConsultarLineaCuenta", objConsultaLineaRequest.Type, objConsultaLineaRequest.Value));


            CONSULTALINEACUENTA.consultarLineaCuentaResponse consultarLineaCuentaResponse = null;
            ENTITIES_CONSULTALINEA.ConsultaLineaResponse objConsultaLineaResponse = null;
            CONSULTALINEACUENTA.consultarLineaCuentaRequest consultarLineaCuentaRequest = new CONSULTALINEACUENTA.consultarLineaCuentaRequest()
            {
                tipoConsulta = objConsultaLineaRequest.Type,
                valorConsulta = objConsultaLineaRequest.Value
            };
            try
            {


                consultarLineaCuentaResponse = Configuration.ServiceConfiguration.CONSULTALINEACUENTA.consultarLineaCuenta(consultarLineaCuentaRequest);
                if (consultarLineaCuentaResponse != null)
                {
                    objConsultaLineaResponse = new ENTITIES_CONSULTALINEA.ConsultaLineaResponse();
                    if (consultarLineaCuentaResponse.rptaConsulta == "0" || consultarLineaCuentaResponse.rptaConsulta == Claro.Constants.NumberOneString)
                    {

                        objConsultaLineaResponse.ResponseValue = consultarLineaCuentaResponse.rptaConsulta;
                    }
                    if (consultarLineaCuentaResponse.cursorDatos != null)
                    {
                        objConsultaLineaResponse.itm = new ENTITIES_CONSULTALINEA.Itm()
                        {

                            origenCuenta = consultarLineaCuentaResponse.cursorDatos[0].origenCuenta,
                            codCuenta = consultarLineaCuentaResponse.cursorDatos[0].codCuenta,
                            coId = consultarLineaCuentaResponse.cursorDatos[0].coId,
                            identificacion = consultarLineaCuentaResponse.cursorDatos[0].identificacion,
                            actCuentaProd = consultarLineaCuentaResponse.cursorDatos[0].actCuentaProd,
                            migCuentaProd = consultarLineaCuentaResponse.cursorDatos[0].migCuentaProd,
                            origenRegistro = consultarLineaCuentaResponse.cursorDatos[0].origenRegistro,
                            estado = consultarLineaCuentaResponse.cursorDatos[0].estado,
                            usrCrea = consultarLineaCuentaResponse.cursorDatos[0].usrCrea,
                            usrModif = consultarLineaCuentaResponse.cursorDatos[0].usrModif,
                            fchCreacion = consultarLineaCuentaResponse.cursorDatos[0].fchCreacion,
                            fchModif = consultarLineaCuentaResponse.cursorDatos[0].fchModif,
                            custCode = consultarLineaCuentaResponse.cursorDatos[0].custCode
                        };
                    }
                }

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objConsultaLineaRequest.Audit.Session, objConsultaLineaRequest.Audit.Transaction, "(Exception) Error :" + ex.Message);
            }
            return objConsultaLineaResponse;
        }
        
        public static EntitiesFixed.GetTypeProductDat.GetTypeProductDatResponse ConsultarContrato(string strIdSession, string strIdTransaction, EntitiesFixed.GetTypeProductDat.GetTypeProductDatRequest objRequest)
        {
            EntitiesFixed.GetTypeProductDat.GetTypeProductDatResponse objResponse = null;
            try
            {
                Hashtable paramHeader = new Hashtable();
                paramHeader.Add("idTransaccion", objRequest.IdTransaccion);
                paramHeader.Add("msgid", objRequest.MsgId);
                paramHeader.Add("timestamp", objRequest.TimesTamp);
                paramHeader.Add("userId", objRequest.UserId);
                paramHeader.Add("channel", objRequest.Channel);
                paramHeader.Add("idApplication", objRequest.IpAplicacion);
                objResponse = RestService.PostInvoqueSDP<EntitiesFixed.GetTypeProductDat.GetTypeProductDatResponse>(Claro.SIACU.Data.Transac.Service.Configuration.RestServiceConfiguration.OBTENER_TIPO_PRODUCTO_DAT_TOBE, paramHeader, objRequest);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(strIdSession, strIdTransaction, ex.Message);
                throw new Claro.MessageException(strIdTransaction);
            }
            return objResponse;
        }
    #endregion

        //INICIATIVA-794
        public static List<EntitiesFixed.ClaroVideo.ConsultIPTV> ConsultarServicioIPTV(string strIdSession, string strTransaction, string strProducto)
        {
            List<Entity.Transac.Service.Fixed.ClaroVideo.ConsultIPTV> list = new List<Entity.Transac.Service.Fixed.ClaroVideo.ConsultIPTV>();
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_PRDC_CODIGO", DbType.String,255, ParameterDirection.Input, strProducto),
                new DbParameter("PO_CURSOR", DbType.Object, ParameterDirection.Output),
                new DbParameter("PO_COD_RESULTADO", DbType.String,200, ParameterDirection.Output),
                new DbParameter("PO_DES_RESULTADO", DbType.String,200, ParameterDirection.Output)
            };

            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_PVU, DbCommandConfiguration.SIACU_SP_CONSULTA_SERVICIO_IPTV, parameters,
                (IDataReader dr) =>
                {
                    while (dr.Read())
                    {
                        list.Add(new Entity.Transac.Service.Fixed.ClaroVideo.ConsultIPTV
                        {
                            SERVV_CODIGO_EXT = Convert.ToString(dr["SERVV_CODIGO_EXT"]),
                            SERVV_DES_EXT = Convert.ToString(dr["SERVV_DES_EXT"]),
                         
                        });
                    }
                });

            return list;
        }

        public static List<Entity.Transac.Service.Fixed.ClaroVideo.ValidateIPTV> ValidarServicioIPTV(string strIdSession, string strTransaction, string strCodNum, string strOpc)
        {
            List<Entity.Transac.Service.Fixed.ClaroVideo.ValidateIPTV> list = new List<Entity.Transac.Service.Fixed.ClaroVideo.ValidateIPTV>();
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_CODNUM", DbType.String,200, ParameterDirection.Input, strCodNum),
                new DbParameter("P_OPC", DbType.String,200, ParameterDirection.Input, strOpc),
                new DbParameter("PO_CURSOR", DbType.Object, ParameterDirection.Output),
                new DbParameter("P_COD_RESP", DbType.Int32, ParameterDirection.Output),
                new DbParameter("P_MSJ_RESP", DbType.String,200, ParameterDirection.Output)                
            };

            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_SP_VALIDA_SERVICIO_IPTV, parameters,
                (IDataReader dr) =>
                {
                    while (dr.Read())
                    {
                        list.Add(new Entity.Transac.Service.Fixed.ClaroVideo.ValidateIPTV
                        {
                            VALIDACION = Convert.ToString(dr["VALIDACION"]),
                          
                        });
                    }
                });

            return list;
        }

        //INI: INICIATIVA-871
        public static DatosSIMPrepago ObtenerDatosSIMPrepago(string strIdSession, string strTransactionID, string strApplicationUser, string strPhoneNumber)
        {
            DatosSIMPrepago objResponseSIM = new DatosSIMPrepago();
            string CodigoRespuesta = "";
            string MensajeRespuesta = "";

            Claro.Web.Logging.Info(strIdSession, strTransactionID, "Inicio del Método GetMobileBankingSAP");

            Claro.Web.Logging.Info(strIdSession, strTransactionID,
            string.Format("Parámetros de entrada: [strIdSession] - {0} ; [strTransactionID] - {1} ; " +
                          "[strApplicationUser] - {2} ; [strPhoneNumber] - {3}", strIdSession, strTransactionID,
                          strApplicationUser, strPhoneNumber));

            Claro.Web.Logging.Info(strApplicationUser, strTransactionID, "Inicio de Ejecución del WS ConsultaClaves - Método Desencriptar");

            CONSULTCLAVE.desencriptarRequestBody objRequest = new CONSULTCLAVE.desencriptarRequestBody();
            CONSULTCLAVE.desencriptarResponseBody objResponse = new CONSULTCLAVE.desencriptarResponseBody();

            objRequest.idTransaccion = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            objRequest.ipAplicacion = KEY.AppSettings("consIpAppConsultaClavesWS");
            objRequest.ipTransicion = KEY.AppSettings("consIpTransConsultaClavesWS");
            objRequest.usrAplicacion = strApplicationUser;
            objRequest.idAplicacion = KEY.AppSettings("CodAplicacion");
            objRequest.codigoAplicacion = KEY.AppSettings("strCodigoAplicacion");
            objRequest.usuarioAplicacionEncriptado = KEY.AppSettings("SIACU_UserEnc_ConsultaDatosSim");
            objRequest.claveEncriptado = KEY.AppSettings("SIACU_PassEnc_ConsultaDatosSim");

            Claro.Web.Logging.Info(strIdSession, strTransactionID,
            string.Format("Parámetros de entrada: [idTransaccion] - {0} ; [ipAplicacion] - {1} ; [ipTransicion] - {2} ; " +
            "[usrAplicacion] - {3} ; [idAplicacion] - {4} ; [codigoAplicacion] - {5} ; " +
            "[usuarioAplicacionEncriptado] - {6} ; [claveEncriptado] - {7}", objRequest.idTransaccion, objRequest.ipAplicacion, objRequest.ipTransicion, objRequest.usrAplicacion,
            objRequest.idAplicacion, objRequest.codigoAplicacion, "****", "****"));

            objResponse.codigoResultado = Configuration.ServiceConfiguration.CONSULTA_CLAVES.desencriptar(ref objRequest.idTransaccion, objRequest.ipAplicacion,
            objRequest.ipTransicion, objRequest.usrAplicacion, objRequest.idAplicacion, objRequest.codigoAplicacion, objRequest.usuarioAplicacionEncriptado,
            objRequest.claveEncriptado, out objResponse.mensajeResultado, out objResponse.usuarioAplicacion, out objResponse.clave);

            Claro.Web.Logging.Info(strIdSession, strTransactionID, string.Format("Parámetros de salida: [mensajeResultado] - {0} ; [usuarioDesencriptado] - {1} ; [claveDesencriptado] - {2}",
            objResponse.mensajeResultado, "****", "****"));

            Claro.Web.Logging.Info(strApplicationUser, strTransactionID, "Fin de Ejecución del WS ConsultaClaves - Método Desencriptar");

            if (objResponse.codigoResultado == "0")
            {
                Claro.Web.Logging.Info(strApplicationUser, strTransactionID, "Inicio de Ejecución del WS ConsultaDatosSIM - Método ConsultarDatosSIM");

                CONSULTAR_DATOS_SIM.HeaderRequestType objHeaderRequestType = new CONSULTAR_DATOS_SIM.HeaderRequestType()
                {
                    country = KEY.AppSettings("consCountry"),
                    language = KEY.AppSettings("consLanguage"),
                    consumer = KEY.AppSettings("consConsumer"),
                    system = KEY.AppSettings("consSystem"),
                    modulo = KEY.AppSettings("consModulo"),
                    pid = strTransactionID,
                    userId = KEY.AppSettings("consUserId"),
                    dispositivo = KEY.AppSettings("consDispositivo"),
                    wsIp = KEY.AppSettings("consIpBSS_ConsultaDatosSim"),
                    operation = KEY.AppSettings("consOperation"),
                    timestamp = DateTime.Now.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffzzz"),
                    VarArg = new CONSULTAR_DATOS_SIM.ArgType[1],
                    msgType = KEY.AppSettings("consMsgType")
                };

                objHeaderRequestType.VarArg[0] = new CONSULTAR_DATOS_SIM.ArgType() { key = string.Empty, value = string.Empty };

                CONSULTAR_DATOS_SIM.HeaderRequestType1 objHeaderRequestType1 = new CONSULTAR_DATOS_SIM.HeaderRequestType1()
                {
                    canal = KEY.AppSettings("consCanal"),
                    idAplicacion = KEY.AppSettings("consIdAplicacion"),
                    idAplicacionSpecified = true,
                    usuarioAplicacion = KEY.AppSettings("consUserApp"),
                    usuarioSesion = strApplicationUser,
                    idTransaccionNegocio = strTransactionID,
                    idTransaccionESB = strTransactionID,
                    fechaInicio = DateTime.Now.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffzzz"),
                    fechaInicioSpecified = true,
                    nodoAdicional = KEY.AppSettings("consNodoAdicional_BSS_ConsultaDatosSim")
                };

                Claro.Web.Logging.Info(strIdSession, strTransactionID,
                string.Format("Parámetros de entrada DP: [country] - {0} ; [language] - {1} ; [consumer] - {2} ; [system] - {3} ; [modulo] - {4} ; [pid] - {5} ; " +
                "[userId] - {6} ; [dispositivo] - {7} ; [wsIp] - {8} ; [operation] - {9} ; [timestamp] - {10} ; [msgType] - {11}", objHeaderRequestType.country,
                objHeaderRequestType.language, objHeaderRequestType.consumer, objHeaderRequestType.system, objHeaderRequestType.modulo, objHeaderRequestType.pid,
                objHeaderRequestType.userId, objHeaderRequestType.dispositivo, objHeaderRequestType.wsIp, objHeaderRequestType.operation, objHeaderRequestType.timestamp,
                objHeaderRequestType.msgType));

                Claro.Web.Logging.Info(strIdSession, strTransactionID,
                string.Format("Parámetros de entrada OSB: [canal] - {0} ; [idAplicacion] - {1} ; [usuarioAplicacion] - {2} ; [usuarioSesion] - {3} ; [idTransaccionESB] - {4} ; [fechaInicio] - {5} ; " +
                "[nodoAdicional] - {6}", objHeaderRequestType1.canal, objHeaderRequestType1.idAplicacion, objHeaderRequestType1.usuarioAplicacion, objHeaderRequestType1.usuarioSesion,
                objHeaderRequestType1.idTransaccionESB, objHeaderRequestType1.fechaInicio, objHeaderRequestType1.nodoAdicional));

                CONSULTAR_DATOS_SIM.consultarDatosSIMRequest objConsultarDatosSIMRequest = new CONSULTAR_DATOS_SIM.consultarDatosSIMRequest();
                CONSULTAR_DATOS_SIM.consultaDatosSIMType type = new CONSULTAR_DATOS_SIM.consultaDatosSIMType();
                type.msisdn = strPhoneNumber;
                objConsultarDatosSIMRequest.listaRequestOpcional = new CONSULTAR_DATOS_SIM.RequestOpcionalTypeRequestOpcional[1];

                objConsultarDatosSIMRequest.consultaDatosSIM = type;
                objConsultarDatosSIMRequest.listaRequestOpcional[0] = new CONSULTAR_DATOS_SIM.RequestOpcionalTypeRequestOpcional { campo = string.Empty, valor = string.Empty };

                CONSULTAR_DATOS_SIM.HeaderResponseType1 headerResponse1 = new CONSULTAR_DATOS_SIM.HeaderResponseType1();
                CONSULTAR_DATOS_SIM.consultarDatosSIMResponse objConsultarDatosSIMResponse = new CONSULTAR_DATOS_SIM.consultarDatosSIMResponse();

                Claro.Web.Logging.Info(strIdSession, strTransactionID, "OperationContextScope");

                using (new System.ServiceModel.OperationContextScope(Configuration.ServiceConfiguration.CONSULTA_DATOS_SIM.InnerChannel))
                {
                    Claro.Web.Logging.Info(strIdSession, strTransactionID, "OperationContextScope: Cabecera");
                    System.ServiceModel.OperationContext.Current.OutgoingMessageHeaders.Add(new SecurityHeader(strTransactionID, objResponse.usuarioAplicacion, objResponse.clave));
                    Claro.Web.Logging.Info("ConsultaDatosSIM: DataPower", "****", "****");

                    CONSULTAR_DATOS_SIM.HeaderResponseType objHeaderResponseType = Claro.Web.Logging.ExecuteMethod<CONSULTAR_DATOS_SIM.HeaderResponseType>
                    (strIdSession, strTransactionID, Configuration.ServiceConfiguration.CONSULTA_DATOS_SIM, () =>
                    {
                        return Configuration.ServiceConfiguration.CONSULTA_DATOS_SIM.consultarDatosSIM(objHeaderRequestType,
                        objHeaderRequestType1, objConsultarDatosSIMRequest, out headerResponse1, out objConsultarDatosSIMResponse);
                    });
                }

                CodigoRespuesta = objConsultarDatosSIMResponse.responseStatus.codigoRespuesta;
                MensajeRespuesta = objConsultarDatosSIMResponse.responseStatus.descripcionRespuesta;

                Claro.Web.Logging.Info(strIdSession, strTransactionID, string.Format("CONSULTA_DATOS_SIM Response: {0}", Newtonsoft.Json.JsonConvert.SerializeObject(objConsultarDatosSIMResponse)));
                
                objResponseSIM.idLote = objConsultarDatosSIMResponse.responseData.listaDatosSIM.idLote;
                objResponseSIM.fechaReg = objConsultarDatosSIMResponse.responseData.listaDatosSIM.fechaReg;
                objResponseSIM.iccid = objConsultarDatosSIMResponse.responseData.listaDatosSIM.iccid;
                objResponseSIM.imsi = objConsultarDatosSIMResponse.responseData.listaDatosSIM.imsi;
                objResponseSIM.pin = objConsultarDatosSIMResponse.responseData.listaDatosSIM.pin;
                objResponseSIM.puk = objConsultarDatosSIMResponse.responseData.listaDatosSIM.puk;
                objResponseSIM.pin2 = objConsultarDatosSIMResponse.responseData.listaDatosSIM.pin2;
                objResponseSIM.puk2 = objConsultarDatosSIMResponse.responseData.listaDatosSIM.puk2;
                objResponseSIM.adm = objConsultarDatosSIMResponse.responseData.listaDatosSIM.adm;
                objResponseSIM.cmdCrear = objConsultarDatosSIMResponse.responseData.listaDatosSIM.cmdCrear;
                objResponseSIM.cmdCreudb = objConsultarDatosSIMResponse.responseData.listaDatosSIM.cmdCreudb;
                objResponseSIM.estadoCod = objConsultarDatosSIMResponse.responseData.listaDatosSIM.estadoDes;
                objResponseSIM.estadoDes = objConsultarDatosSIMResponse.responseData.listaDatosSIM.estadoDes;
                objResponseSIM.r = objConsultarDatosSIMResponse.responseData.listaDatosSIM.r;

                Claro.Web.Logging.Info(strIdSession, strTransactionID, string.Format("CONSULTA_DATOS_SIM -> Respuesta: [CodigoRespuesta] - {0} ; [MensajeRespuesta] - {1}", objConsultarDatosSIMResponse.responseStatus.codigoRespuesta, objConsultarDatosSIMResponse.responseStatus.descripcionRespuesta));
            }

            Claro.Web.Logging.Info(strIdSession, strTransactionID, "Fin del Método GetMobileBankingSAP");

            return objResponseSIM;
        }
        //FIN: INICIATIVA-871

        //INI: INICIATIVA-986
        public static Response ActivarDesactivarContinueWS(string strIdSession, string strTransaccion, AplicarRetirarContingencia objRequestContinue)
        {
            Claro.Web.Logging.Info(strIdSession, strTransaccion, "INICIO INICIATIVA-986 - ActivarDesactivarContinueWS");
            Response objResponse = new Response();
            string strRespuesta = string.Empty;

            try
            {
               InstantLinkWebServices objInstantLinkWS = new InstantLinkWebServices();
                objInstantLinkWS.Url = Functions.CheckStr(ConfigurationManager.AppSettings("UrlInstantlinkWS"));
                objInstantLinkWS.Credentials = System.Net.CredentialCache.DefaultCredentials;
                objInstantLinkWS.Timeout = Functions.CheckInt(ConfigurationManager.AppSettings("TimeOutInstantlinkWS").ToString());

                Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} --> {1}", "[INICIATIVA-986 - ActivarDesactivarContinueWS][Url WS]", objInstantLinkWS.Url));
                Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} --> {1}", "[INICIATIVA-986 - ActivarDesactivarContinueWS][TimeOut WS]", Functions.CheckStr(objInstantLinkWS.Timeout)));

                CreateRequest objRequest = new CreateRequest();
                RequestHeader objRequestHeader = new RequestHeader();
                Claro.SIACU.ProxyService.Transac.Service.InstantLinkSOA.Parameter[] objRequestArrayParameters = new ProxyService.Transac.Service.InstantLinkSOA.Parameter[7];

                #region Request
                Claro.Web.Logging.Info(strIdSession, strTransaccion, "[INICIATIVA-986 - ActivarDesactivarContinueWS][Inicio obtener Request]");
                objRequestHeader.NeType = objRequestContinue.NeType;
                objRequestHeader.OrderNo = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                objRequestHeader.Priority = Functions.CheckInt(objRequestContinue.Priority);
                objRequestHeader.ReqUser = Functions.CheckStr(objRequestContinue.ReqUser);
                objRequest.RequestHeader = objRequestHeader;

                objRequestArrayParameters[0] = new ProxyService.Transac.Service.InstantLinkSOA.Parameter()
                {
                    name = "ACTION_ID",
                    value = Functions.CheckStr(objRequestContinue.ActionId)
                };

                objRequestArrayParameters[1] = new ProxyService.Transac.Service.InstantLinkSOA.Parameter()
                {
                    name = "IMSI1",
                    value = Functions.CheckStr(objRequestContinue.Imsi)
                };

                objRequestArrayParameters[2] = new ProxyService.Transac.Service.InstantLinkSOA.Parameter()
                {
                    name = "MSISDN1",
                    value = string.Format("51{0}", Functions.CheckStr(objRequestContinue.Linea))
                };

                objRequestArrayParameters[3] = new ProxyService.Transac.Service.InstantLinkSOA.Parameter()
                {
                    name = "NETWORK_SERVICE",
                    value = Functions.CheckStr(objRequestContinue.NetworkService)
                };

                objRequestArrayParameters[4] = new ProxyService.Transac.Service.InstantLinkSOA.Parameter()
                {
                    name = "SERVICE_VOLTE",
                    value = Functions.CheckStr(objRequestContinue.ServicioVolte)
                };

                objRequestArrayParameters[5] = new ProxyService.Transac.Service.InstantLinkSOA.Parameter()
                {
                    name = "TYPE_PLAN",
                    value = Functions.CheckStr(objRequestContinue.TipoPlan)
                };

                objRequestArrayParameters[6] = new ProxyService.Transac.Service.InstantLinkSOA.Parameter()
                {
                    name = "CLIENT_CBIO",
                    value = Functions.CheckStr(objRequestContinue.ClienteCbio)
                };

                objRequest.RequestParameters = objRequestArrayParameters;

                Claro.Web.Logging.Info(strIdSession, strTransaccion, "[INICIATIVA-986 - ActivarDesactivarContinueWS][xml][Request]");
                System.Xml.Serialization.XmlSerializer xRequest = new System.Xml.Serialization.XmlSerializer(typeof(CreateRequest));
                StringBuilder strRequest = new StringBuilder();
                TextWriter twRequest = new StringWriter(strRequest);
                xRequest.Serialize(twRequest, objRequest);
                twRequest.Close();
                string xmlRequest = strRequest.ToString();

                Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} : {1}", "[INICIATIVA-986 - ActivarDesactivarContinueWS][xml][Request]", xmlRequest));
                Claro.Web.Logging.Info(strIdSession, strTransaccion, "[INICIATIVA-986 - ActivarDesactivarContinueWS][Fin obtener Request]");
                #endregion

                #region Response
                Claro.Web.Logging.Info(strIdSession, strTransaccion, "[INICIATIVA-986 - ActivarDesactivarContinueWS][xml][Inicio obtener Response]");
                objResponse = objInstantLinkWS.create(objRequest);

                Claro.Web.Logging.Info(strIdSession, strTransaccion, "[INICIATIVA-986 - ActivarDesactivarContinueWS][xml][Response]");
                System.Xml.Serialization.XmlSerializer xResponse = new System.Xml.Serialization.XmlSerializer(typeof(Response));
                StringBuilder strResponse = new StringBuilder();
                TextWriter twResponse = new StringWriter(strResponse);
                xResponse.Serialize(twResponse, objResponse);
                twResponse.Close();
                string xmlResponse = strResponse.ToString();

                Claro.Web.Logging.Info(strIdSession, strTransaccion, string.Format("{0} : {1}", "[INICIATIVA-986 - ActivarDesactivarContinueWS][xml][Response]", xmlResponse));
                Claro.Web.Logging.Info(strIdSession, strTransaccion, "[INICIATIVA-986 - ActivarDesactivarContinueWS][xml][Fin obtener Response]");
                #endregion
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransaccion, ex.Message);
                throw new Claro.MessageException(strTransaccion);
            }

            return objResponse;
        }

        public static MessageResponseRegistrarProcesoContinue RegistrarActualizarContingencia(MessageRequestRegistrarProcesoContinue objRequest, Tools.Entity.AuditRequest objAuditRequest)
        {
            MessageResponseRegistrarProcesoContinue objResponse = new MessageResponseRegistrarProcesoContinue();

            try
            {
                Hashtable datosHeader = new Hashtable();
                datosHeader.Add("idTransaccion", objAuditRequest.Transaction);
                datosHeader.Add("msgId", Functions.CheckStr(objRequest.registrarProcesoContinueRequest.msisdn));
                datosHeader.Add("timestamp", DateTime.Now.ToString("yyyy-MM-dd'T'HH:mm:ss'Z'"));
                datosHeader.Add("userId", objAuditRequest.UserName);

                objResponse = Claro.Data.RestService.PostInvoqueSDP<MessageResponseRegistrarProcesoContinue>(Claro.SIACU.Data.Transac.Service.Configuration.RestServiceConfiguration.APLICAR_RETIRAR_CONTINGENCIA_DROP1_2, datosHeader, objRequest);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objAuditRequest.Session, objAuditRequest.Transaction, ex.Message);
                throw new Claro.MessageException(objAuditRequest.Transaction);
            }
            return objResponse;
        }
        //FIN: INICIATIVA-986
    }
}
