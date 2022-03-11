using System;
using System.Collections.Generic;
using System.Data;
using Claro.Data;
using System.Configuration;
using Claro.SIACU.Data.Transac.Service.Configuration;
using Claro.SIACU.Transac.Service;
using EntitiesFixed = Claro.SIACU.Entity.Transac.Service.Fixed;
using ConstantsHFC = Claro.SIACU.Transac.Service.Constants;
using Claro.SIACU.Entity.Transac.Service.Fixed;
using KEY = Claro.ConfigurationManager;
using Claro.SIACU.Entity.Transac.Service.Fixed.GetRegisterEtaSelection;
using Claro.SIACU.Entity.Transac.Service.Fixed.GetCaseInsert;
using CUSTOMER_HFC = Claro.SIACU.ProxyService.Transac.Service.SIACFixed.CustomerHFC;
using CUSTOMER_LTE = Claro.SIACU.ProxyService.Transac.Service.SIACFixed.CustomerLTE;
#region Proy-32650
using Claro.SIACU.Entity.Transac.Service.Fixed.RegisterBonoDiscount;
using Claro.SIACU.Entity.Transac.Service.Fixed.RegisterActiDesaBonoDesc;
using Claro.SIACU.ProxyService.Transac.Service.SIAC.ServAdiDescuentoWS;
using Claro.SIACU.Entity.Transac.Service.Fixed.RegisterNuevaInteraccion;
using Claro.SIACU.ProxyService.Transac.Service.TransaccionInteracciones;
using Claro.SIACU.Entity.Transac.Service.Fixed.GetStatusAccountAOC;
using FIXED_STATE = Claro.SIACU.ProxyService.Transac.Service.SIACU.StateAccount;
using QUERY_PAY = Claro.SIACU.ProxyService.Transac.Service.SIACU.ConsultationPayments;
using REGISTRAR_SAR = Claro.SIACU.ProxyService.Transac.Service.EBSRegistroAjusteSAR;
using Claro.SIACU.Entity.Transac.Service.Fixed.RegisterSAR;
using ENTITIES_QUERYDEBT = Claro.SIACU.Entity.Transac.Service.Fixed.GetQueryDebt;
using QUERYDEBT = Claro.SIACU.ProxyService.Transac.Service.SIACU.ConsultationPayments;
using Claro.SIACU.Entity.Transac.Service.Fixed.RegisterInteractionAdjust;
using Claro.SIACU.Entity.Transac.Service.Fixed.GetAccountStatus; 
using Claro.SIACU.Entity.Transac.Service.Fixed.GetRegistarInstaDecoAdiHFC;
using UNINSTALLDECOSLTE = Claro.SIACU.ProxyService.Transac.Service.Fixed.UninstallInstallDecosLTE;
using LDEDP = Claro.SIACU.ProxyService.Transac.Service.SIACSecurity.Permissions.LDE;
using Claro.SIACU.ProxyService.Transac.Service.SIACSecurity.Permissions.LDE;
#endregion

namespace Claro.SIACU.Data.Transac.Service.Fixed
{
    public class RetentionCancelServ : GenericDataMethods
    {
        //Name: Obtener las Lista de Acciones de Retención/Cancelación
        public static List<GenericItem> GetAcciones(string strIdSession, string strTransaction, Int32 vNivel)
        {
            List<GenericItem> oLstAcciones = new List<GenericItem>();
            DbParameter[] parameters =
            {   new DbParameter("P_NIVEL", DbType.Int64, 255,ParameterDirection.Input, vNivel), 
                new DbParameter("P_CURSOR", DbType.Object, ParameterDirection.Output)
            };

            try
            {
                Web.Logging.ExecuteMethod(strIdSession, strTransaction, () =>
                {
                    DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB,  //SIAC_POST_PVU
                        DbCommandConfiguration.SIACU_POST_PVU_LISTA_ACCIONES_RETENCION, parameters, reader =>
                        {
                            while (reader.Read())
                            {
                                //if (reader["COD_TIPO_SERV"].ToString() == "3") //FTTH
                                //{

                                    var item = new GenericItem
                                    {

                                        Codigo = reader["CODIGO"].ToString(),
                                        Descripcion = reader["DESCRIPCION"].ToString(),
                                        Cod_tipo_servicio = Convert.ToInt(reader["COD_TIPO_SERV"].ToString())

                                    };
                                    oLstAcciones.Add(item);
                                //}

                            }
                        });
                });

            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
            }

            return oLstAcciones;
        }

        //Name: Obtener las Lista Motivos de Retención/Cancelación
        public static List<GenericItem> GetMotCancelacion(RetentionCancelServicesRequest oRequest) 
        {
        
            List<GenericItem> oLstMotCancelacion = new List<GenericItem>();
            Web.Logging.Info("GetMotCancelacion: ",  "Inicio Método : ","GetMotCancelacion ");
            Web.Logging.Info("GetMotCancelacion: ", "vEstado : ", oRequest.vEstado.ToString());
            Web.Logging.Info("GetMotCancelacion: ", "vTipoLista : ", oRequest.vTipoLista.ToString());
            DbParameter[] parameters =
            {   new DbParameter("P_ESTADO", DbType.Int16, 10,ParameterDirection.Input, oRequest.vEstado), 
                new DbParameter("P_TIPO", DbType.Int16, 10,ParameterDirection.Input,oRequest.vTipoLista ),  
                new DbParameter("P_CURSOR", DbType.Object, ParameterDirection.Output)
            };
        
            try
            {
                Web.Logging.ExecuteMethod(oRequest.Audit.Session, oRequest.Audit.Transaction, () =>
                {
                    DbFactory.ExecuteReader(oRequest.Audit.Session, oRequest.Audit.Transaction, DbConnectionConfiguration.SIAC_POST_DB,   //SIAC_POST_PVU
                        DbCommandConfiguration.SIACU_POST_PVU_LISTAR_MOTIVOS_RETENCION, parameters, reader =>
                        {
                            while (reader.Read())
                            {

                                var item = new GenericItem
                                {

                                    Codigo = reader["CODIGO"].ToString(),
                                    Descripcion = reader["DESCRIPCION"].ToString(),
                                    Cod_tipo_servicio = Convert.ToInt(reader["COD_TIPO_SERV"].ToString())

                                };
                                oLstMotCancelacion.Add(item);
                            }
                        });
                });

            }
            catch (Exception ex)
            {
                Web.Logging.Error(oRequest.Audit.Session.ToString(), oRequest.Audit.Transaction, "Error GetMotCancelacion : " + ex.Message);
            }

            return oLstMotCancelacion;
        }

        //Name: Obtener las Lista Sub Motivos de Retención/Cancelación  
        public static List<GenericItem> GetSubMotiveCancel(string  strIdSession, string strTransaction, int pIdMotivo)
        {
            List<EntitiesFixed.GenericItem> oLstSubMotive = new List<EntitiesFixed.GenericItem>();

            Claro.Web.Logging.Info(strIdSession, strTransaction, string.Format("Parámetros de Entrada: pIdMotivo = {0}", pIdMotivo.ToString()));

            try
            {
                DbParameter[] parameters = new DbParameter[]{
                    new DbParameter("PI_IDMOTIVO", DbType.Int32,ParameterDirection.Input, pIdMotivo), 
                new DbParameter("PO_CURSOR", DbType.Object, ParameterDirection.Output)
            };

                DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_POST_PVU_LISTAR_SUBMOTIVOS_RETENCION, parameters, (IDataReader reader) =>
            {
                    Entity.Transac.Service.Fixed.GenericItem item;
                    while (reader.Read())
                    {
                        item = new Entity.Transac.Service.Fixed.GenericItem();
                        item.Codigo = Claro.Convert.ToString(reader[0]);
                        item.Descripcion = Claro.Convert.ToString(reader[1]);
                        item.Estado = Claro.Convert.ToString(reader[3]);
                        oLstSubMotive.Add(item);
                        //oLstSubMotive.Add(new EntitiesFixed.GenericItem() {
                        //    Codigo = Claro.SIACU.Transac.Service.Functions.CheckStr(reader[0]), //CODIGO
                        //    Descripcion = Claro.SIACU.Transac.Service.Functions.CheckStr(reader[1]) //DESCRIPCION
                        //});
                    }
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, Claro.SIACU.Transac.Service.Functions.GetExceptionMessage(ex));
                throw new Exception(Claro.SIACU.Transac.Service.Functions.GetExceptionMessage(ex));
            }

            return oLstSubMotive;
        }

        //Name: Obtener las Lista de Tipos de Trabajo de Retención/Cancelación  
        public static List<GenericItem> GetTypeWork(string strIdSession, Int64 IntIdTypeWork, string strTransaction)
        {

            List<GenericItem> oLstTypeWork = new List<GenericItem>();

            DbParameter[] parameters =
            {   new DbParameter("p_tipo", DbType.Int64, 255,ParameterDirection.Input, IntIdTypeWork), 
                new DbParameter("srv_tipra", DbType.Object, ParameterDirection.Output)
            };

            try
            {
                Web.Logging.ExecuteMethod(strIdSession, strTransaction, () =>
                {
                    DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SGA,
                        DbCommandConfiguration.SIACU_SP_CONSULTA_TIPTRA, parameters, reader =>
                        {
                            while (reader.Read())
                            {

                                var item = new GenericItem
                                {

                                    Codigo = reader["tiptra"].ToString(),
                                    Descripcion = reader["descripcion"].ToString(),
                                    Codigo2 = reader["FLAG_FRANJA"].ToString()

                                };
                                oLstTypeWork.Add(item);
                            }
                        });
                });

            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
            }

            return oLstTypeWork;
        }

        //Name: Obtener las Lista de Sub Tipos de Trabajo de Retención/Cancelación  
        public static List<GenericItem> GetSubTypeWork(string strIdSession, Int64 vIdTypeWork, string strTransaction)
        {

            List<GenericItem> oLstTypeWork = new List<GenericItem>();

            DbParameter[] parameters =
            {   new DbParameter("vIdtiptra", DbType.String, 22,ParameterDirection.Input, vIdTypeWork), 
                new DbParameter("cur_tipo", DbType.Object, ParameterDirection.Output)
            };

            try
            {
                Web.Logging.ExecuteMethod(strIdSession, strTransaction, () =>
                {
                    DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SGA,
                        DbCommandConfiguration.SIACU_P_CONSULTA_SUBTIPORD, parameters, reader =>
                        {
                            while (reader.Read())
                            {

                                var item = new GenericItem
                                {

                                    Codigo = reader["VALOR"].ToString(),
                                    Descripcion = reader["DESCRIPCION"].ToString(),

                                };
                                oLstTypeWork.Add(item);
                            }
                        });
                });

            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
            }

            //EntitiesFixed.GenericItem otemp = new EntitiesFixed.GenericItem();
            //otemp.Codigo = "501";
            //otemp.Descripcion = "HFC - MANTENIMIENTO BABY SITTING";
            //oLstTypeWork.Add(otemp);


            return oLstTypeWork;
        }

        //Name: Obtener las Lista de Motivos SOT - de Retención/Cancelación   - Temporal
        public static List<GenericItem> GetMotiveSOT(string strIdSession, string strTransaction)
        {
            List<GenericItem> oLstMotiveSOT = new List<GenericItem>();
            DbParameter[] parameters =
            {    
                new DbParameter("srv_cur", DbType.Object, ParameterDirection.Output)
            };

            try
            {

                Web.Logging.ExecuteMethod(strIdSession.ToString(), strTransaction, () =>
                {
                    DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SGA,
                        DbCommandConfiguration.SIACU_POST_SGA_P_CONSULTA_MOTIVO, parameters, reader =>
                        {
                            while (reader.Read())
                            {

                                var item = new GenericItem
                                {

                                    Codigo = reader["CODMOTOT"].ToString(),
                                    Descripcion = reader["MOTIVO"].ToString(),

                                };
                                oLstMotiveSOT.Add(item);

                            }
                        });
                });

            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
            }

            return oLstMotiveSOT;
        }

        //Name: Agregar días laborables
        public static AddDayWorkResponse GetAddDayWork(string strIdSession, string strTransaction, string strFechaIni, int intNroDias, string strFechResult, int intCodError, string strDesError)
        {
            AddDayWorkResponse oAddDayWork = new AddDayWorkResponse();
            Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + strTransaction, "Inicio Método : GetAddDayWork");

            try
            {
                DbParameter[] parameters =
                {    
                   new DbParameter("P_FECHA_INI", DbType.String, 255,ParameterDirection.Input, strFechaIni), 
                   new DbParameter("P_NRO_DIAS", DbType.Int64, 255,ParameterDirection.Input, intNroDias), 
                    new DbParameter("P_FECHA_RESULT", DbType.String,20, ParameterDirection.Output),
                    new DbParameter("P_COD_ERROR", DbType.Int16,22, ParameterDirection.Output),
                    new DbParameter("P_DES_ERROR", DbType.String,1000, ParameterDirection.Output),
                };


                DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_POST_SGA_P_AGREGAR_DIAS_LABORABLES, parameters);
//SIAC_POST_PVU
                oAddDayWork = new AddDayWorkResponse();

                oAddDayWork.FechaResultado = parameters[2].Value.ToString();
                oAddDayWork.CodError = Convert.ToInt(parameters[3].Value.ToString());
                oAddDayWork.DescError = parameters[4].Value.ToString();

                Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + strTransaction, "Inicio Método : GetAddDayWork" + "oAddDayWork.FechaResultado" + oAddDayWork.FechaResultado);
                Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + strTransaction, "Inicio Método : GetAddDayWork" + "oAddDayWork.CodError" + oAddDayWork.CodError);
                Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + strTransaction, "Inicio Método : GetAddDayWork" + "oAddDayWork.DescError" + oAddDayWork.DescError);
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession.ToString(), strTransaction, ex.Message);
            }

            return oAddDayWork;
        }

        //Name: Obtener Obtener Parametro Terminal TPI  
        public static List<GenericItem> GetObtainParameterTerminalTPI(string strIdSession, string strTransaction, int parametroID, string Strmessage)
        {
            List<GenericItem> oLst = new List<GenericItem>();

            DbParameter[] parameters =
            {    
                new DbParameter("P_PARAMETRO_ID", DbType.Int16, 22,ParameterDirection.Input, parametroID), 
                new DbParameter("P_MENSAJE", DbType.String, 255,ParameterDirection.Input, Strmessage), 
                new DbParameter("P_CURSOR", DbType.Object, ParameterDirection.Output)
            };

            try
            {

                Web.Logging.ExecuteMethod(strIdSession.ToString(), strTransaction, () =>
                {
                    DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB,
                        DbCommandConfiguration.SIACU_POST_DB_SP_OBTENER_PARAMETRO, parameters, reader =>
                        {
                            while (reader.Read())
                            {

                                var item = new GenericItem
                                {

                                    ParameterID = reader["0"].ToString(),
                                    Nombre = reader["1"].ToString(),
                                    Descripcion = reader["2"].ToString(),
                                    Tipo = reader["3"].ToString(),
                                    Valor_C = reader["4"].ToString(),
                                };


                                oLst.Add(item);
                            }


                        });
                });

            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
            }

            return oLst;
        }


        //Name: Obtener Obtener Parametro Terminal Solo TFI Postpago 
        public static List<GenericItem> GetSoloTFIPostpago(string strIdSession, string strTransaction, int parametroID, string Strmessage)
        {
            List<GenericItem> oLst = new List<GenericItem>();

            DbParameter[] parameters =
            {    
                new DbParameter("P_PARAMETRO_ID", DbType.Int16, 22,ParameterDirection.Input, parametroID), 
                new DbParameter("P_MENSAJE", DbType.String, 255,ParameterDirection.Input, Strmessage), 
                new DbParameter("P_CURSOR", DbType.Object, ParameterDirection.Output)
            };

            try
            {

                Web.Logging.ExecuteMethod(strIdSession.ToString(), strTransaction, () =>
                {
                    DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB,
                        DbCommandConfiguration.SIACU_POST_COBS_SSSIGA_OBTENER_DATOS_BSCS_EXT, parameters, reader =>
                        {
                            while (reader.Read())
                            {

                                var item = new GenericItem
                                {

                                    ParameterID = reader["0"].ToString(),
                                    Nombre = reader["1"].ToString(),
                                    Descripcion = reader["2"].ToString(),
                                    Tipo = reader["3"].ToString(),
                                    Valor_C = reader["4"].ToString(),

                                };

                                oLst.Add(item);
                                break;
                            }

                        });
                });

            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
            }

            return oLst;
        }

        //Name: Obtener Datos BSCS Ext
        public static bool ObtenerDatosBSCSExt(string strIdSession, string strTransaction, string vNroTelefono, double vCodNuevoPlan,
        ref double rNroFacturas, ref double rCargoFijoActual, ref double rCargoFijoNuevoPlan)
        {
            bool resultado = true;
            Web.Logging.Info("Inicio Método", "ObtenerDatosBSCSExt : ", "Inicio ObtenerDatosBSCSExt");



            DbParameter[] parameters =
            {    
												   new DbParameter("p_nro_telefono", DbType.String,50,ParameterDirection.Input,vNroTelefono),
												   new DbParameter("p_tmcode_men", DbType.Double,10,ParameterDirection.Input,vCodNuevoPlan),
												   new DbParameter("p_num_fact", DbType.Double,10,ParameterDirection.Output,rNroFacturas),
												   new DbParameter("p_cargo_fijo_act", DbType.Double,10,ParameterDirection.Output,rCargoFijoActual),
												   new DbParameter("p_cargo_fijo_men", DbType.Double,10,ParameterDirection.Output,rCargoFijoNuevoPlan),
            };

            try
            {


                Web.Logging.ExecuteMethod(strIdSession, strTransaction, () =>
                    {
                        DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS,
                        DbCommandConfiguration.SIACU_POST_COBS_SSSIGA_OBTENER_DATOS_BSCS_EXT, parameters);

                    });

            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession.ToString(), strTransaction,"Error ObtenerDatosBSCSExt :" + ex.Message.ToString());
                resultado = false;
                return resultado;
            }

            rNroFacturas = Convert.ToDouble(parameters[2].Value.ToString());
            rCargoFijoActual = Convert.ToDouble(parameters[3].Value.ToString());
            rCargoFijoNuevoPlan = Convert.ToDouble(parameters[4].Value.ToString());

            Web.Logging.Info("Inicio Método", "ObtenerDatosBSCSExt : ", "Inicio ObtenerDatosBSCSExt - rNroFacturas : " + rNroFacturas);
            Web.Logging.Info("Inicio Método", "ObtenerDatosBSCSExt : ", "Inicio ObtenerDatosBSCSExt - rCargoFijoActual : " + rCargoFijoActual);
            Web.Logging.Info("Inicio Método", "ObtenerDatosBSCSExt : ", "Inicio ObtenerDatosBSCSExt - rCargoFijoNuevoPlan : " + rCargoFijoNuevoPlan);

            return resultado;
        }

        //Name: Obtener Obtener Penalidad Ext. 
        public static bool GetObtainPenalidadExt(string strIdSession, string strTransaction, string vNroTelefono, DateTime vFechaPenalidad,
            double vNroFacturas, double vCargoFijoActual, double vCargoFijoNuevoPlan, double vDiasxMes, double vCodNuevoPlan,
            ref double rAcuerdoIdSalida, ref double rDiasPendientes, ref double rCargoFijoDiario, ref double rPrecioLista, ref double rPrecioVenta,
            ref double rPenalidadPCS, ref double rPenalidaAPADECE)
        {
            bool resultado = true;

            DbParameter[] parameters =
            {    
													   new DbParameter("p_acuerdo_id", DbType.Double,10,ParameterDirection.Input,"0"),
													   new DbParameter("p_nro_telefono", DbType.String,50,ParameterDirection.Input,vNroTelefono),
													   new DbParameter("p_fecha_penalidad", DbType.DateTime,10,ParameterDirection.Input,vFechaPenalidad),
													   new DbParameter("p_numero_facturas", DbType.Double,10,ParameterDirection.Input,vNroFacturas),
													   new DbParameter("p_cargo_fijo_act", DbType.Double,10,ParameterDirection.Input,vCargoFijoActual),
													   new DbParameter("p_cargo_fijo_inf", DbType.Double,10,ParameterDirection.Input,vCargoFijoNuevoPlan),
													   new DbParameter("p_diasxmes", DbType.Double,10,ParameterDirection.Input,vDiasxMes),
													   new DbParameter("p_codigo_plan_nuevo", DbType.Double,10,ParameterDirection.Input,vCodNuevoPlan),
													   new DbParameter("p_acuerdo_id_salida", DbType.Double,22,ParameterDirection.Output,rAcuerdoIdSalida),
													   new DbParameter("p_dias_pendientes", DbType.Double,22,ParameterDirection.Output,rDiasPendientes),
													   new DbParameter("p_cargo_fijo_diario", DbType.Double,22,ParameterDirection.Output,rCargoFijoDiario),
													   new DbParameter("p_precio_lista", DbType.Double,22,ParameterDirection.Output,rPrecioLista),
													   new DbParameter("p_precio_venta", DbType.Double,22,ParameterDirection.Output,rPrecioVenta),
													   new DbParameter("p_monto_pcs", DbType.Double,10,ParameterDirection.Output,rPenalidadPCS),
													   new DbParameter("p_monto_apadece", DbType.Double,10,ParameterDirection.Output,rPenalidaAPADECE),

                
            };

            try
            {
                Web.Logging.ExecuteMethod(strIdSession, strTransaction, () =>
                    {
                        DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SIGA,
                            DbCommandConfiguration.SIACU_POST_SIGA_SSSIGA_OBTENER_PENALIDAD_EXT, parameters);


                    });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession.ToString(), strTransaction, ex.Message);
                resultado = false;
                return resultado;
            }
            rAcuerdoIdSalida = (parameters[8].Value.ToString()=="null") ? 0.00 : Convert.ToDouble(parameters[8].Value.ToString());
            rDiasPendientes = (parameters[9].Value.ToString() == "null") ? 0.00 : Convert.ToDouble(parameters[9].Value.ToString());
            rCargoFijoDiario = (parameters[10].Value.ToString() == "null") ? 0.00 : Convert.ToDouble(parameters[10].Value.ToString());
            rPrecioLista = (parameters[11].Value.ToString() == "null") ? 0.00 : Convert.ToDouble(parameters[11].Value.ToString());
            rPrecioVenta = (parameters[12].Value.ToString() == "null") ? 0.00 : Convert.ToDouble(parameters[12].Value.ToString());
            rPenalidadPCS = (parameters[13].Value.ToString() == "null") ? 0.00 : Convert.ToDouble(parameters[13].Value.ToString());
            rPenalidaAPADECE = (parameters[14].Value.ToString() == "null") ? 0.00 : Convert.ToDouble(parameters[14].Value.ToString());

            return resultado;

        }

        //Name: Validar el CustomerId
        public static bool GetValidateCustomerId(string strIdSession, string strTransaction,
                                              string vPHONE, ref int vCONTACTOBJID,
                                              ref string strflgResult, ref string strMSError)
        {
            bool resultado = true;

            DbParameter[] parameters =
            {    
                                               new DbParameter("p_phone", DbType.String,30,ParameterDirection.Input,vPHONE),												   
                                               new DbParameter("p_contactobjid", DbType.Int64,22,ParameterDirection.Output,vCONTACTOBJID),
                                               new DbParameter("p_flag_insert", DbType.String,255,ParameterDirection.Output,strflgResult),												   
                                               new DbParameter("p_msg_text", DbType.String,255,ParameterDirection.Output,strMSError	),											 

            };

            try
            {
                Web.Logging.ExecuteMethod(strIdSession, strTransaction, () =>
                 {
                     DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_CLARIFY, DbCommandConfiguration.SA_SP_SEARCH_CONTACT_USERLDI, parameters);

                 });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);

            }
            vCONTACTOBJID = Convert.ToInt(parameters[1].Value.ToString());
            strflgResult = parameters[2].Value.ToString();
            strMSError = parameters[3].Value.ToString();

            return resultado;
        }


        // Name: Registrar Cliente 
        public static bool GetRegisterCustomerId(string strIdSession, string strTransaction,
                                             Customer oItem, ref string strflgResult, ref string strMSError)
        {
            bool resultado = true;
            DbParameter[] parameters =
            {    
                                                   new DbParameter("P_PHONE", DbType.String,255,ParameterDirection.Input,oItem.TELEFONO),
                                                   new DbParameter("P_USUARIO", DbType.String,255,ParameterDirection.Input, oItem.USUARIO),
                                                   new DbParameter("P_NOMBRES", DbType.String,255,ParameterDirection.Input, oItem.NOMBRES),
                                                   new DbParameter("P_APELLIDOS", DbType.String,255,ParameterDirection.Input, oItem.APELLIDOS),
                                                   new DbParameter("P_RAZONSOCIAL", DbType.String,255,ParameterDirection.Input, oItem.RAZON_SOCIAL),
                                                   new DbParameter("P_TIPO_DOC", DbType.String,255,ParameterDirection.Input, oItem.TIPO_DOC),
                                                   new DbParameter("P_NUM_DOC", DbType.String,255,ParameterDirection.Input, oItem.NRO_DOC),
                                                   new DbParameter("P_DOMICILIO", DbType.String,255,ParameterDirection.Input, oItem.DOMICILIO),
                                                   new DbParameter("P_DISTRITO", DbType.String,255,ParameterDirection.Input, oItem.DISTRITO),													  
                                                   new DbParameter("P_DEPARTAMENTO", DbType.String,255,ParameterDirection.Input, oItem.DEPARTAMENTO),													  
                                                   new DbParameter("P_PROVINCIA", DbType.String,255,ParameterDirection.Input, oItem.PROVINCIA),													  												   
                                                   new DbParameter("P_MODALIDAD", DbType.String,255,ParameterDirection.Input, oItem.MODALIDAD),
                                                   new DbParameter("P_FLAG_INSERT", DbType.String,255,ParameterDirection.Output, strflgResult),
                                                   new DbParameter("P_MSG_TEXT", DbType.String,255,ParameterDirection.Output,strMSError),									 

            };

            for (int j = 0; j < parameters.Length; j++)
                parameters[j].Value = DBNull.Value;

            int i = 0;
            if (oItem.TELEFONO != null)
                parameters[i].Value = oItem.TELEFONO;

            i++;
            if (oItem.USUARIO != null)
                parameters[i].Value = oItem.USUARIO;

            i++;
            if (oItem.NOMBRES != null)
            {
                if (oItem.NOMBRES.Length > 30)
                {
                    parameters[i].Value = oItem.NOMBRES.Substring(0, 30);
                }
                else
                {
                    parameters[i].Value = oItem.NOMBRES;
                }
            }

            i++;
            if (oItem.APELLIDOS != null)
            {
                if (oItem.APELLIDOS.Length > 30)
                {
                    parameters[i].Value = oItem.APELLIDOS.Substring(0, 30);
                }
                else
                {
                    parameters[i].Value = oItem.APELLIDOS;
                }
            }

            i++;
            if (oItem.RAZON_SOCIAL != null)
            {
                if (oItem.RAZON_SOCIAL.Length > 30)
                {
                    parameters[i].Value = oItem.RAZON_SOCIAL.Substring(0, 30);
                }
                else
                {
                    parameters[i].Value = oItem.RAZON_SOCIAL;
                }
            }

            i++;
            if (oItem.TIPO_DOC != null)
                parameters[i].Value = "DNI";

            i++;
            if (oItem.NRO_DOC != null)
                parameters[i].Value = oItem.NRO_DOC;

            i++;
            if (oItem.DOMICILIO != null)
            {
                if (oItem.DOMICILIO.Length > 200)
                {
                    parameters[i].Value = oItem.DOMICILIO.Substring(0, 200);
                }
                else
                {
                    parameters[i].Value = oItem.DOMICILIO;
                }
            }

            i++;
            if (oItem.DISTRITO != null)
            {
                if (oItem.DISTRITO.Length > 200)
                {
                    parameters[i].Value = oItem.DISTRITO.Substring(0, 200);
                }
                else
                {
                    parameters[i].Value = oItem.DISTRITO;
                }
            }

            i++;
            if (oItem.DEPARTAMENTO != null)
            {
                if (oItem.DEPARTAMENTO.Length > 40)
                {
                    parameters[i].Value = oItem.DEPARTAMENTO.Substring(0, 40);
                }
                else
                {
                    parameters[i].Value = oItem.DEPARTAMENTO;
                }
            }

            i++;
            if (oItem.PROVINCIA != null)
            {
                if (oItem.PROVINCIA.Length > 200)
                {
                    parameters[i].Value = oItem.PROVINCIA.Substring(0, 200);
                }
                else
                {
                    parameters[i].Value = oItem.PROVINCIA;
                }
            }

            i++;
            if (oItem.MODALIDAD != null)
                parameters[i].Value = oItem.MODALIDAD;
            try
            {
                Web.Logging.ExecuteMethod(strIdSession, strTransaction, () =>
                {
                    DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_CLARIFY,
                        DbCommandConfiguration.SIACU_POST_CLARIFY_SP_CREATE_CONTACT_USERLDI, parameters);

                });

                strflgResult = parameters[12].Value.ToString();
                strMSError = parameters[13].Value.ToString();

                if (strflgResult == "NO OK")
                    resultado = false;


            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession.ToString(), strTransaction, ex.Message);

            }

            return resultado;
        }


        // Name: Registrar Eta Selection
        public static string GetRegisterEtaSelection(string strIdSession, string strTransaction, RegisterEtaSelectionRequest oItem, ref string strMSError)
        {

            DbParameter[] parameters =
            {   
                                    new DbParameter("vidconsulta", DbType.Int64, ParameterDirection.Input,oItem.IdConsulta),
                                    new DbParameter("vidInteraccion", DbType.String,255,ParameterDirection.Input,oItem.IdInteraccion), 
                                    new DbParameter("vfechaCompromiso", DbType.Date, ParameterDirection.Input,Convert.ToDate(oItem.FechaCompromiso)),
                                    new DbParameter("vfranja", DbType.String,50,ParameterDirection.Input,oItem.Franja),
                                    new DbParameter("vid_bucket", DbType.String,50,ParameterDirection.Input,oItem.Id_Bucket), 
                                    new DbParameter("vresp", DbType.String,255,ParameterDirection.Output)

            };
            

            try
            {
                Web.Logging.ExecuteMethod(strIdSession, strTransaction, () =>
                {
                    DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SGA,
                        DbCommandConfiguration.SIACU_POST_SGA_P_REGISTRA_ETA_SEL, parameters);

                });

                strMSError = parameters[5].Value.ToString();
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
                strMSError = ex.Message;
            }

            return strMSError;
        }

        public static string GetCaseInsert(string strIdSession, string strTransaction, CaseInsertRequest oItem, ref string rCasoId, ref string rFlagInsercion, ref string rMsgText)
        {

            DbParameter[] parameters =
            {   
						new DbParameter("PN_CONTACTOBJID_1", DbType.Int64,ParameterDirection.Input,oItem.OBJID_CONTACTO),
						new DbParameter("PV_SITEOBJID_1", DbType.Int64,ParameterDirection.Input,oItem.OBJID_SITE),
						new DbParameter("PV_ACCOUNT", DbType.String,255,ParameterDirection.Input,oItem.ACCOUNT),
						new DbParameter("PV_PHONE", DbType.String,255,ParameterDirection.Input,oItem.PHONE),
						new DbParameter("PV_TIPO", DbType.String,255,ParameterDirection.Input,oItem.TIPO),
						new DbParameter("PV_CLASE", DbType.String,255,ParameterDirection.Input,oItem.CLASE),
						new DbParameter("PV_SUBCLASE", DbType.String,255,ParameterDirection.Input,oItem.SUBCLASE),
						new DbParameter("PV_METODO_CONTACTO", DbType.String,255,ParameterDirection.Input,oItem.METODO_CONTACTO),
						new DbParameter("PV_PRIORIDAD", DbType.String,255,ParameterDirection.Input,oItem.PRIORIDAD),
						new DbParameter("PV_SEVERIDAD", DbType.String,255,ParameterDirection.Input,oItem.SEVERIDAD),
						new DbParameter("PV_COLA", DbType.String,255,ParameterDirection.Input,oItem.COLA),
						new DbParameter("PV_FLAG_INTERACT", DbType.String,255,ParameterDirection.Input,oItem.FLAG_INTERACCION),
						new DbParameter("PV_USR_PROCESO", DbType.String,255,ParameterDirection.Input,oItem.USUARIO_PROCESO),
						new DbParameter("PV_USUARIO", DbType.String,255,ParameterDirection.Input,oItem.USUARIO_ID),
						new DbParameter("PV_TIPO_INTER", DbType.String,255,ParameterDirection.Input,oItem.TIPO_INTERACCION),
						new DbParameter("PV_NOTAS", DbType.String,4000,ParameterDirection.Input,oItem.NOTAS),
						new DbParameter("ID_CASO", DbType.String,255,ParameterDirection.Output,rCasoId),
						new DbParameter("FLAG_CREACION", DbType.String,255,ParameterDirection.Output,rFlagInsercion),
						new DbParameter("MSG_TEXT", DbType.String,255,ParameterDirection.Output,rMsgText),	

            };
            for (int j = 0; j < parameters.Length; j++)
                parameters[j].Value = DBNull.Value;

            int i = 0;
            if (oItem.OBJID_CONTACTO != null)
                parameters[i].Value = 0;
            i++;
            if (oItem.OBJID_SITE != null)
                parameters[i].Value = oItem.OBJID_SITE; //Funciones.CheckInt64(oItem.OBJID_SITE); consultar

            i++;
            if (oItem.CUENTA != null)
                parameters[i].Value = oItem.CUENTA;
            i++;
            if (oItem.TELEFONO != null)
                parameters[i].Value = oItem.TELEFONO;
            i++;

            if (oItem.TIPIFICACION != null)
                parameters[i].Value = oItem.TIPIFICACION;
            i++;

            if (oItem.CLASE != null)
                parameters[i].Value = oItem.CLASE;
            i++;

            if (oItem.SUBCLASE != null)
                parameters[i].Value = oItem.SUBCLASE;
            i++;

            if (oItem.METODO_CONTACTO != null)
                parameters[i].Value = oItem.METODO_CONTACTO;
            i++;

            if (oItem.PRIORIDAD != null)
                parameters[i].Value = oItem.PRIORIDAD;
            i++;

            if (oItem.SEVERIDAD != null)
                parameters[i].Value = oItem.SEVERIDAD;
            i++;

            if (oItem.COLA != null)
                parameters[i].Value = oItem.COLA;
            i++;

            if (oItem.FLAG_INTERACCION != null)
                parameters[i].Value = oItem.FLAG_INTERACCION;
            i++;

            if (oItem.USUARIO_PROCESO != null)
                parameters[i].Value = oItem.USUARIO_PROCESO;
            i++;

            if (oItem.USUARIO_ID != null)
                parameters[i].Value = oItem.USUARIO_ID;
            i++;

            if (oItem.TIPO_INTERACCION != null)
                parameters[i].Value = oItem.TIPO_INTERACCION;
            i++;

            if (oItem.NOTAS != null)
                parameters[i].Value = oItem.NOTAS;
            i++;

            try
            {
                Web.Logging.ExecuteMethod(strIdSession, strTransaction, () =>
                {
                    DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_CLARIFY,
                        DbCommandConfiguration.SIACU_POST_SGA_P_REGISTRA_ETA_SEL, parameters);

                });

                rMsgText = parameters[19].Value.ToString();
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
                rMsgText = ex.Message;
            }

            return rMsgText;
        }

        // Name: Obtener Apadece Cancelacion Ret
        public static RetentionCancelServicesResponse GetApadeceCancelRet(string strIdSession, string strTransaction, int numTelef, int codId,
                                                ref double rdbValorApadece, ref string rintCodError, ref string rp_msg_text)
        {
            RetentionCancelServicesResponse oResponse = new RetentionCancelServicesResponse();
            

            DbParameter[] parameters =
            {   
                                    new DbParameter("PI_MSISDN", DbType.Int64,22,ParameterDirection.Input,numTelef), 
                                    new DbParameter("PI_COD_ID", DbType.Int64,22,ParameterDirection.Input,codId),
                                    new DbParameter("PI_ACUERDO_ID", DbType.Int64,22,ParameterDirection.Input,DBNull.Value),
                                    new DbParameter("PI_FECHA_TRANSACCION", DbType.String,10,ParameterDirection.Input,DBNull.Value),
                                    new DbParameter("PI_TIPO_ACUERDO", DbType.Int64,22,ParameterDirection.Input,DBNull.Value),
                                    new DbParameter("PI_MOTIVO_APADECE", DbType.Int64,22,ParameterDirection.Input,3),
                                    new DbParameter("PI_CF_NUEVO", DbType.Double,ParameterDirection.Input,0),
                                    new DbParameter("PI_FLG_EQUIPO", DbType.Int64,22,ParameterDirection.Input,DBNull.Value),												   												   
                                    new DbParameter("PI_ACUERDO_VIGENTE", DbType.Int64,22,ParameterDirection.Input,0),												   
                                    new DbParameter("PO_MONTO_APADECE", DbType.Double,22,ParameterDirection.Output,rdbValorApadece),
                                    new DbParameter("PO_TIPO_PRODUCTO", DbType.String,10,ParameterDirection.Output,DBNull.Value),
                                    new DbParameter("PO_CODERROR", DbType.String,22,ParameterDirection.Output,rintCodError),
                                    new DbParameter("PO_DESERROR", DbType.String,1000,ParameterDirection.Output,rp_msg_text),
                                    new DbParameter("CUR_SEC", DbType.Object,ParameterDirection.Output),

            };


            try
            {

                DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SIGA,
                    DbCommandConfiguration.SIACU_POST_SIGA_SP_OBTENER_APADECE, parameters, reader =>
                    {

                    });

                oResponse.ValorApadece = (parameters[10].Value.ToString()== "null" ? 0.00 : Convert.ToDouble(parameters[10].Value.ToString()));
                oResponse.CodMessage = parameters[12].Value.ToString();
                oResponse.Message = parameters[13].Value.ToString();



            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
                oResponse.CodMessage = SIACU.Transac.Service.Constants.DAReclamDatosVariableNO_OK;
            }

            return oResponse;
        }


        public static bool GetDesactivatedContract(Customer objRequest)
        {
            bool resultado = false;
            Web.Logging.Info("Inicio Método","GetDesactivatedContract : ", "GetDesactivatedContract_HFC");
            try
            {
             

                CUSTOMER_HFC.desactivarContratoEAIRequest objClienteResquest = new CUSTOMER_HFC.desactivarContratoEAIRequest();
                CUSTOMER_HFC.DesactivarContratoEAIInput oDesactivarContratoEAIInput = new CUSTOMER_HFC.DesactivarContratoEAIInput();
                CUSTOMER_HFC.desactivarContratoEAIResponse objClienteResponse = new CUSTOMER_HFC.desactivarContratoEAIResponse();
                CUSTOMER_HFC.CabeceraRequest oCabeceraRequest = new CUSTOMER_HFC.CabeceraRequest();
                CUSTOMER_HFC.CuerpoDESARequest oCuerpoDESARequest = new CUSTOMER_HFC.CuerpoDESARequest();


                oCabeceraRequest.idTransaccion =  objRequest.Audit.Transaction;
                oCabeceraRequest.ipAplicacion = objRequest.Audit.IPAddress;
                oCabeceraRequest.nombreAplicacion =objRequest.ApplicationName;  
                oCabeceraRequest.usuarioAplicacion = objRequest.UserApplication;

                oDesactivarContratoEAIInput.cabeceraRequest = oCabeceraRequest;
                
                
          


                #region Datos del Cliente
                oCuerpoDESARequest.codigoCliente = objRequest.CUSTOMER_ID;
                oCuerpoDESARequest.codigoCuenta = objRequest.CUENTA;
                oCuerpoDESARequest.codigoContrato = objRequest.CONTRATO_ID;
                Web.Logging.Info("codigoCliente: " + objRequest.CUSTOMER_ID, "codigoCuenta: " + objRequest.CUENTA, "codigoContrato : " + objRequest.CONTRATO_ID);

                oCuerpoDESARequest.cacDac = objRequest.Des_CAC;
                oCuerpoDESARequest.cicloFacturacion = objRequest.CICLO_FACTURACION;
                oCuerpoDESARequest.msisdn = objRequest.TELEFONO;
                Web.Logging.Info("cacDac: " + objRequest.Des_CAC, "cicloFacturacion: " + objRequest.CICLO_FACTURACION, "msisdn : " + objRequest.TELEFONO);

                oCuerpoDESARequest.reason = objRequest.Reason;
                oCuerpoDESARequest.codigoMotivo = objRequest.COD_MOTIVE;
                oCuerpoDESARequest.fechaActual = objRequest.FECHA_ACT.ToString("yyyy-MM-dd");  //Convert.ToString(objRequest.FECHA_ACT);
                Web.Logging.Info("reason: " + objRequest.Reason, "codigoMotivo: " + objRequest.COD_MOTIVE, "fechaActual : " + objRequest.FECHA_ACT.ToString("yyyy-MM-dd"));

                oCuerpoDESARequest.flagNdPcs = objRequest.FLAG_ND_PCS;
                oCuerpoDESARequest.flagOccApadece = objRequest.FLAG_OCC_APADECE;
                oCuerpoDESARequest.montoFidelizacion = objRequest.MONTO_FIDELIZACION;
                Web.Logging.Info("flagNdPcs: " + objRequest.FLAG_ND_PCS, "flagOccApadece: " + objRequest.FLAG_OCC_APADECE, "montoFidelizacion : " + objRequest.MONTO_FIDELIZACION);

                oCuerpoDESARequest.montoPCS = objRequest.MONTO_PCS;
                oCuerpoDESARequest.FechaProgramacion = objRequest.FECHA_PROGRAMACION;
                oCuerpoDESARequest.tipoServicio = objRequest.TIPO_SERVICIO;
                Web.Logging.Info("montoPCS: " + objRequest.MONTO_PCS, "FechaProgramacion: " + objRequest.FECHA_PROGRAMACION, "tipoServicio : " + objRequest.TIPO_SERVICIO);

                oCuerpoDESARequest.numeroDocumento = objRequest.NRO_DOC;
                oCuerpoDESARequest.codigoPlano = objRequest.CODIGO_PLANO_FACT;
                oCuerpoDESARequest.subMotivoPCS = objRequest.SUB_MOTIVO_PCS;
                Web.Logging.Info("numeroDocumento: " + objRequest.NRO_DOC, "codigoPlano: " + objRequest.CODIGO_PLANO_FACT, "subMotivoPCS : " + objRequest.SUB_MOTIVO_PCS);

                oCuerpoDESARequest.tipoCliente = objRequest.TIPO_CLIENTE;
                oCuerpoDESARequest.observaciones = objRequest.OBSERVACIONES;
                oCuerpoDESARequest.motivoPCS = objRequest.MOTIVO_PCS;
                Web.Logging.Info("tipoCliente: " + objRequest.TIPO_CLIENTE, "observaciones: " + objRequest.OBSERVACIONES, "motivoPCS : " + objRequest.MOTIVO_PCS);

                oCuerpoDESARequest.montoPenalidad = objRequest.MONTO_PENALIDAD;
                oCuerpoDESARequest.mailUsuarioAplicacion = objRequest.EMAIL;
                oCuerpoDESARequest.areaPCS = objRequest.AREA_PCS;
                Web.Logging.Info("montoPenalidad: " + objRequest.MONTO_PENALIDAD, "mailUsuarioAplicacion: " + objRequest.EMAIL, "areaPCS : " + objRequest.AREA_PCS);

                oCuerpoDESARequest.codigoInteraccion = objRequest.CODIGO_INTERACCION;
                oCuerpoDESARequest.codigoServicio = objRequest.CODIGO_SERVICIO;
                oCuerpoDESARequest.FechaProgramacionSOT = objRequest.FECHA_PROGRAMACION_SOT;
                Web.Logging.Info("codigoInteraccion: " + objRequest.CODIGO_INTERACCION, "codigoServicio: " + objRequest.CODIGO_SERVICIO, "FechaProgramacionSOT : " + objRequest.FECHA_PROGRAMACION_SOT);

                oCuerpoDESARequest.franjaHoraria = objRequest.FRANJA_HORARIO;
                oCuerpoDESARequest.trace = objRequest.TRACE;
                oCuerpoDESARequest.tipTra = objRequest.TIPO_TRABAJO;
                oCuerpoDESARequest.usuarioAsesor = objRequest.ASESOR;
                Web.Logging.Info("franjaHoraria: " + objRequest.FRANJA_HORARIO, "trace: " + objRequest.TRACE, "tipTra : " + objRequest.TIPO_TRABAJO + "usuarioAsesor : " + objRequest.ASESOR);

                #endregion

                oDesactivarContratoEAIInput.cuerpoRequest = oCuerpoDESARequest;

                objClienteResquest.desactivarContratoEaiRequest = oDesactivarContratoEAIInput;


                objClienteResponse = ServiceConfiguration.FIXED_CUSTOMER_HFC.desactivarContrato(objClienteResquest);


                Web.Logging.Info("Resultado WS : desactivarContrato_HFC", "codigoRespuesta: ", objClienteResponse.desactivarContratoEaiResponse.cabeceraResponse.codigoRespuesta);
                if (objClienteResponse.desactivarContratoEaiResponse.cabeceraResponse.codigoRespuesta == Constants.NumberZeroString)
                {
                    resultado = true;
                }
                else
                {
                    resultado = false;
                }


            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, "Error WS - desactivarContrato_HFC:" + ex.Message);
                return false;
            }

            return resultado;

        }

        public static bool GetDesactivatedContract_LTE(Customer objRequest)
        {
            bool resultado = false;
            //Web.Logging.Info("Inicio Método", "GetDesactivatedContract : ", "GetDesactivatedContract_LTE");
            Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "Inicio Método - GetDesactivatedContract : GetDesactivatedContract_LTE");
            try
            {
                CUSTOMER_LTE.desactivarContratoEAIRequest oRequest = new CUSTOMER_LTE.desactivarContratoEAIRequest();
                CUSTOMER_LTE.desactivarContratoEAIResponse oResponse = new CUSTOMER_LTE.desactivarContratoEAIResponse();
                CUSTOMER_LTE.ServicioPorCodigoClienteType[] oTempServicio = new  CUSTOMER_LTE.ServicioPorCodigoClienteType[0];
                CUSTOMER_LTE.AuditRequestType objAuditRequest = new CUSTOMER_LTE.AuditRequestType();
                CUSTOMER_LTE.AuditResponseType objAuditResponse = new CUSTOMER_LTE.AuditResponseType();
                
                objAuditRequest.idTransaccion = objRequest.Audit.Transaction;
                objAuditRequest.ipAplicacion = objRequest.Audit.IPAddress;
                objAuditRequest.nombreAplicacion = objRequest.ApplicationName;// objRequest.Audit.ApplicationName;
                objAuditRequest.usuarioAplicacion = objRequest.UserApplication;//.Audit.UserName;

                oRequest.auditRequest= objAuditRequest;

                oRequest.codigoCliente = objRequest.CUSTOMER_ID;
                oRequest.codigoCuenta = objRequest.CUENTA;
                oRequest.codigoContrato = objRequest.CONTRATO_ID;
                
                //Web.Logging.Info("codigoCliente: " + objRequest.CUSTOMER_ID, "codigoCuenta: " + objRequest.CUENTA, "codigoContrato : " + objRequest.CONTRATO_ID);
                Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "codigoCliente: " + objRequest.CUSTOMER_ID + "     |   codigoCuenta: " + objRequest.CUENTA + "     |   codigoContrato : " + objRequest.CONTRATO_ID);
                oRequest.codigoServicio = objRequest.CODIGO_SERVICIO;
                oRequest.msisdn = objRequest.Msisdn;
                oRequest.reason = objRequest.Reason;
                Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "codigoServicio: " + objRequest.CODIGO_SERVICIO + "     |   msisdn: " + objRequest.Msisdn + "     |   reason : " + objRequest.Reason);

                oRequest.FechaProgramacion = objRequest.FECHA_PROGRAMACION;
                oRequest.FechaProgramacionSOT = objRequest.FECHA_PROGRAMACION_SOT;
                oRequest.franjaHoraria = string.Empty;
                Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "FechaProgramacion: " + objRequest.FECHA_PROGRAMACION + "     |  FechaProgramacionSOT: " + objRequest.FECHA_PROGRAMACION_SOT);


                oRequest.tipTra = objRequest.TIPO_TRABAJO;
                oRequest.montoPenalidad = objRequest.MONTO_PENALIDAD;  // validar si carga esta en mimuscula en Customer
                Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "tipTra: " + objRequest.TIPO_TRABAJO + "     |  montoPenalidad: " + objRequest.MONTO_PENALIDAD);
                


                oRequest.tipoCliente = objRequest.TIPO_CLIENTE;
                oRequest.areaPCS = objRequest.AREA_PCS;
                oRequest.motivoPCS = objRequest.MOTIVO_PCS;
                Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "tipoCliente: " + objRequest.TIPO_CLIENTE + "     |  areaPCS: " + objRequest.AREA_PCS + "     |  motivoPCS : " + objRequest.MOTIVO_PCS);

                oRequest.subMotivoPCS = objRequest.SUB_MOTIVO_PCS;
                oRequest.cicloFacturacion = objRequest.CICLO_FACTURACION;
                oRequest.numeroDocumento = objRequest.NRO_DOC;
                Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "subMotivoPCS: " + objRequest.SUB_MOTIVO_PCS + "     | cicloFacturacion: " + objRequest.CICLO_FACTURACION + "     | numeroDocumento : " + objRequest.NRO_DOC);


                oRequest.usuarioAsesor = objRequest.ASESOR; // validar
                oRequest.tipoServicio = objRequest.TIPO_SERVICIO;
                oRequest.observaciones = objRequest.OBSERVACIONES;
                Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction , "usuarioAsesor: " + objRequest.USUARIO + "     | tipoServicio: " + objRequest.TIPO_SERVICIO + "     | observaciones : " + objRequest.OBSERVACIONES);

                oRequest.flagOccApadece = objRequest.FLAG_OCC_APADECE;
                oRequest.flagNdPcs = objRequest.FLAG_ND_PCS;
                oRequest.cacDac = objRequest.Des_CAC;
                Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "flagOccApadece: " + objRequest.FLAG_OCC_APADECE + "     | flagNdPcs: " + objRequest.FLAG_ND_PCS + "     | cacDac : " + objRequest.Des_CAC);


                oRequest.montoPCS = objRequest.MONTO_PCS;
                oRequest.montoFidelizacion = objRequest.MONTO_FIDELIZACION;
                oRequest.trace = objRequest.TRACE;
                Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "montoPCS: " + objRequest.MONTO_PCS + "     | montoFidelizacion: " + objRequest.MONTO_FIDELIZACION + "     | trace : " + objRequest.TRACE);

                oRequest.fechaActual = Convert.ToString(objRequest.FECHA_ACT);
                oRequest.codigoPlano = objRequest.CODIGO_PLANO_INST;
                oRequest.codigoMotivo = objRequest.COD_MOTIVE;
                Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "fechaActual: " + objRequest.FECHA_ACT + "     | codigoPlano: " + objRequest.CODIGO_PLANO_INST + "     | codigoMotivo : " + objRequest.COD_MOTIVE);

                oRequest.mailUsuarioAplicacion = objRequest.MAIL_USUARIO_APLICACION;
                oRequest.codigoInteraccion = objRequest.CODIGO_INTERACCION;
                Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "mailUsuarioAplicacion: " + objRequest.MAIL_USUARIO_APLICACION + "     | codigoInteraccion: "+objRequest.CODIGO_INTERACCION);

                
                Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "usuarioAsesor: " + objRequest.ASESOR);
                oResponse = ServiceConfiguration.FIXED_CUSTOMER_LTE.desactivarContrato(oRequest);
                Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "Resultado WS : desactivarContrato_LTE   |  ==> codigoRespuesta: "+ objAuditResponse.codigoRespuesta);

                objAuditResponse = oResponse.auditResponse;
                if (objAuditResponse.codigoRespuesta == "0")
                {
                    resultado = true;
                }
                else {
                    resultado = false;
                }

                Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "Fin Método - GetDesactivatedContract_LTE  |  ==> Resultado" + resultado);

                return resultado;

                
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, "Error WS - desactivarContrato_LTE :" + ex.Message);
                return false;
            }

        
        }


        public static CaseInsertResponse GetCreateCase(CaseInsertRequest oRequest)
        { 
            CaseInsertResponse oResponse = new CaseInsertResponse();
            try 
	        {
 
                DbParameter[] parameters =
                {   


                            #region Parametros InsertCaso
                                        new DbParameter("P_CONTACTOBJID_1", DbType.Int64,ParameterDirection.Input,oRequest.CONTRATO),
				                        new DbParameter("P_SITEOBJID_1", DbType.Int64,ParameterDirection.Input,oRequest.OBJID_SITE),
				                        new DbParameter("P_ACCOUNT", DbType.String,255,ParameterDirection.Input,oRequest.CONTRATO),
				                        new DbParameter("P_PHONE", DbType.String,255,ParameterDirection.Input,oRequest.TELEFONO),
				                        new DbParameter("P_TIPO", DbType.String,255,ParameterDirection.Input,oRequest.TIPO),
				                        new DbParameter("P_CLASE", DbType.String,255,ParameterDirection.Input,oRequest.CLASE),
				                        new DbParameter("P_SUBCLASE", DbType.String,255,ParameterDirection.Input,oRequest.SUBCLASE),
				                        new DbParameter("P_METODO_CONTACTO", DbType.String,255,ParameterDirection.Input,oRequest.METODO_CONTACTO),
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
				                        new DbParameter("ID_CASO", DbType.String,255,ParameterDirection.Output,oResponse.rCasoId),
				                        new DbParameter("FLAG_CREACION", DbType.String,255,ParameterDirection.Output,oResponse.rFlagInsercion),
				                        new DbParameter("MSG_TEXT", DbType.String,255,ParameterDirection.Output,oResponse.rMsgText),	
    #endregion
                };
                    Web.Logging.ExecuteMethod(oRequest.Audit.Session, oRequest.Audit.Transaction, () =>
                    {
                        DbFactory.ExecuteNonQuery(oRequest.Audit.Session, oRequest.Audit.Transaction, DbConnectionConfiguration.SIAC_POST_CLARIFY,
                            DbCommandConfiguration.SIACU_POST_CLARIFY_CREATE_CASE_HFC, parameters);

                    });
                    oResponse.rCasoId = parameters[26].Value.ToString();
                    oResponse.rFlagInsercion = parameters[27].Value.ToString();
                    oResponse.rMsgText = parameters[28].Value.ToString();
               
            }
            catch (Exception ex)
            {
                Web.Logging.Error(oRequest.Audit.Session, oRequest.Audit.Transaction, ex.Message);
            } 
            

           return oResponse;
        }

        public static List<GenericItem> GetMotiveSOTByTypeJob(string strIdSession, string strTransaction, int tipTra)
        {
            List<GenericItem> oLstMotive = new List<GenericItem>();
            DbParameter[] parameters =
            {   new DbParameter("pi_tiptra", DbType.Int64, 300,ParameterDirection.Input, tipTra), 
                new DbParameter("po_motivos", DbType.Object, ParameterDirection.Output)
            };

            try
            {
                Web.Logging.ExecuteMethod(strIdSession, strTransaction, () =>
                {
                    DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_POST_SGA_SGASS_MOTIVOS_TIPTRA, parameters, reader =>
                        {
                            while (reader.Read())
                            {
                                var item = new GenericItem
                                {
                                    Codigo = reader["CODMOTOT"].ToString(),
                                    Descripcion = reader["DESCRIPCION"].ToString()
                                };
                                oLstMotive.Add(item);
                            }
                        });
                });

            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
            }

            return oLstMotive;
        }  

        #region Proy-32650

        public static List<GenericItem> GetMonths(string strIdSession, string strTransaction, Int32 vBonoID, string vTipo)
        {
            List<GenericItem> oLstMonths = new List<GenericItem>();
            DbParameter[] parameters =
            {   new DbParameter("PI_TIPO", DbType.Int64, 255,ParameterDirection.Input, vTipo),
                new DbParameter("PO_COD_ERR", DbType.String,255, ParameterDirection.Output),
                new DbParameter("PO_DES_ERR", DbType.String,255, ParameterDirection.Output),
                new DbParameter("PO_CURSOR", DbType.Object, ParameterDirection.Output)
            };

            try
            {
                Web.Logging.ExecuteMethod(strIdSession, strTransaction, () =>
                {
                    DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS,  //SIAC_POST_PVU
                        DbCommandConfiguration.SIACU_POST_BSCS_SP_LST_PERIOD_X_CF_SA, parameters, reader =>
                        {
                            while (reader.Read())
                            {
                                var item = new GenericItem
                                    {

                                        Codigo = reader["ID_PERIOD"].ToString(),
                                        Codigo2 = reader["VALOR"].ToString(),
                                        Descripcion = reader["DESCRIP"].ToString()

                                    };
                                oLstMonths.Add(item);
                            }
                        });
                });

            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
            }

            return oLstMonths;
        }

        public static List<GenericItem> GetListDiscount(string strIdSession, string strTransaction, ref string msgError, ref string msgDescError)
        {
            string sError = string.Empty, sDescError = string.Empty;
            List<GenericItem> oLstDescuentosCF = new List<GenericItem>();

            DbParameter[] parameters =
            {   new DbParameter("PO_COD_ERR", DbType.Int64, 255,ParameterDirection.Output), 
                new DbParameter("PO_DES_ERR", DbType.String, 255,ParameterDirection.Output),
                new DbParameter("PO_CURSOR", DbType.Object, ParameterDirection.Output)
            };

            try
            {
                Web.Logging.ExecuteMethod(strIdSession, strTransaction, () =>
                {
                    DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS,
                        DbCommandConfiguration.SIACU_POST_BSCS_BSCSSS_CONSUL_PORCENT, parameters, reader =>
                        {
                            while (reader.Read())
                            {


                                var item = new GenericItem
                                {
                                    Codigo = reader["REPOI_IDPORCENTAJE"].ToString(),
                                    Codigo2 = reader["REPOI_VALOR"].ToString(),
                                    Descripcion = reader["REPOV_DESCRIPCION"].ToString()

                                };
                                oLstDescuentosCF.Add(item);


                            }
                        });
                    sError = parameters[0].Value.ToString();
                    sDescError = parameters[1].Value.ToString();
                });
                msgError = sError;
                msgDescError = sDescError;
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
            }

            return oLstDescuentosCF;

        }

        /// <summary>
        /// Obtiene el monto retenido de los servicios contratable.
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="strArrServicios"></param>
        /// <returns></returns>
        public static List<Entity.Transac.Service.Fixed.PlanService> GetRetentionRate(string strIdSession, string strTransaction, string strArrServicios)
        {
            Web.Logging.Info("HFCGetRetentionRate", "HFCGetRetentionRate", "Entro GetRetentionRate");
            Web.Logging.Info("HFCGetRetentionRate", "HFCGetRetentionRate", "los parametros que recibe el metodo son: strIdSession:" + strIdSession + ";strTransaction:" + strTransaction + ";strArrServicios:" + strArrServicios);
            List<Entity.Transac.Service.Fixed.PlanService> list = new List<PlanService>();

            DbParameter[] parameters = new DbParameter[] {                
                new DbParameter("PI_ARRAY", DbType.String, 5000, ParameterDirection.Input,strArrServicios),
                new DbParameter("PO_CURSOR", DbType.Object, ParameterDirection.Output),
                new DbParameter("PO_COSTO_INS", DbType.Double, ParameterDirection.Output),
                new DbParameter("PO_COD_ERR", DbType.String, 255, ParameterDirection.Output),
                new DbParameter("PO_DES_ERR", DbType.String, 255, ParameterDirection.Output)
            };
            try
            {
                Web.Logging.ExecuteMethod(strIdSession, strTransaction, () =>
                {
                    DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS,
                        DbCommandConfiguration.SIACU_POST_BSCSSS_CONSUL_SERVICIOS, parameters, reader =>
                        {
                            while (reader.Read())
                            {
                                var item = new Entity.Transac.Service.Fixed.PlanService
                                {

                                    TmCode = reader["TMCODE"].ToString(),
                                    SNCode = reader["SNCODE"].ToString(),
                                    SPCode = reader["TMCODE_COM"].ToString(),
                                    CF = reader["MONTO"].ToString()
                                };

                                list.Add(item);

                            }
                        });
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, SIACU.Transac.Service.Functions.GetExceptionMessage(ex));
            }
            return list;
        }

        /// <summary>
        /// Mostrar el Total descuento / inversion total - Retencion cancelacion.
        /// </summary>
        /// <param name="sesion"></param>
        /// <param name="transaction"></param>
        /// <param name="msgError"></param>
        /// <returns></returns>
        public static bool GetTotalInversion(string strIdSession, string strTransaction, string PI_CO_ID, ref string PO_TOTAL_DESC, ref string PO_COD_ERR, ref string PO_DES_ERR)
        {
            bool salida = false;
            DbParameter[] parameters =
            {   new DbParameter("PI_CO_ID", DbType.Int64, 255,ParameterDirection.Input, PI_CO_ID), 
                new DbParameter("PO_TOTAL_DESC", DbType.String, 255,ParameterDirection.Output), 
                new DbParameter("PO_COD_ERR", DbType.String, 255,ParameterDirection.Output),
                new DbParameter("PO_DES_ERR", DbType.String,255, ParameterDirection.Output)
            };

            try
            {
                Web.Logging.ExecuteMethod(strIdSession, strTransaction, () =>
                {
                    DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS,
                        DbCommandConfiguration.BSCSSS_TOTAL_INVERSION, parameters);
                });

                salida = true;
            }
            catch (Exception ex)
            {
                PO_TOTAL_DESC = "0";
                salida = false;
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
            }
            finally
            {
                PO_TOTAL_DESC = parameters[1].Value.ToString();
                PO_COD_ERR = parameters[2].Value.ToString();
                PO_DES_ERR = parameters[3].Value.ToString();
            }
            return salida;
        }

        //32650 falta implementar en el front //ws ConsultaEstadoCuenta
        /// <summary>
        /// Método que devuelve una lista con los datos del estado de cuenta AOC de Fijos.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransactionID">Id de transacción</param>
        /// <param name="strUser">Usuario</param>
        /// <param name="strNroCuenta">Número de cuenta</param>
        /// <param name="strErrCod">Código de error</param>
        /// <returns>Devuelve listado de objetos StatusAccountAOC con información de estado de cuenta AOC de Fijos.</returns>
        public static AccountStatusResponse GetStatusAccountFixedAOC(AccountStatusRequest objAccountStatusRequest) 
        {
            Claro.Web.Logging.Info(DateTime.Now.ToString("yyyyMMddhhmmss"),DateTime.Now.ToString("yyyyMMddhhmmss"), "entrada al metodo GetStatusAccountFixedAOC");
            AccountStatusResponse response = null;

            AccountStatusDetailH objStatusAccountAOC;
            List<AccountStatusDetailH> listStatusAccountAOC = null;
            AccountStatusDetail objStatusAccountAOCDet;
            List<AccountStatusDetail> listStatusAccountAOCDet = null;

            FIXED_STATE.AuditType auditTypeResponse = null;

            FIXED_STATE.DetalleEstadoCuentaCabType[] objDECCabType = null;
            try
            {
                auditTypeResponse = Claro.Web.Logging.ExecuteMethod<FIXED_STATE.AuditType>
                (objAccountStatusRequest.Audit.Session, objAccountStatusRequest.Audit.Transaction, Configuration.WebServiceConfiguration.FIXED_STATE, () =>
                {
                    return Configuration.WebServiceConfiguration.FIXED_STATE.consultaEstadoCuenta(
                                                                                    objAccountStatusRequest.txId,
                                                                                    objAccountStatusRequest.pCodAplicacion,
                                                                                    objAccountStatusRequest.Audit.UserName,
                                                                                    objAccountStatusRequest.pTipoConsulta,
                                                                                    objAccountStatusRequest.pTipoServicio,
                                                                                    objAccountStatusRequest.pCliNroCuenta,
                                                                                    objAccountStatusRequest.pNroTelefono,
                                                                                    objAccountStatusRequest.pFlagSoloSaldo,
                                                                                    objAccountStatusRequest.pFlagSoloDisputa,
                                                                                    objAccountStatusRequest.pFechaDesde,
                                                                                    objAccountStatusRequest.pFechaHasta,
                                                                                    Convert.ToDecimal(objAccountStatusRequest.pTamanoPagina),
                                                                                    Convert.ToDecimal(objAccountStatusRequest.pNroPagina),
                                                                                    out objDECCabType);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(DateTime.Now.ToString("yyyyMMddhhmmss"), "GetStatusAccountFixedAOC entro al catch WS de consultaEstadoCuenta ", ex.Message);
                objDECCabType = new FIXED_STATE.DetalleEstadoCuentaCabType[0];
            }

            if (objDECCabType != null)
            {
                listStatusAccountAOC = new List<AccountStatusDetailH>();

                for (int j = 0; j < objDECCabType.Length; j++)
                {

                    listStatusAccountAOCDet = new List<AccountStatusDetail>();

                    if (objDECCabType.Length >= 1)
                    {

                        for (int i = 0; i < objDECCabType[j].xDetalleTrx.Length; i++)
                        {
                            objStatusAccountAOCDet = new AccountStatusDetail()
                        {
                                xAbono = Convert.ToString(objDECCabType[j].xDetalleTrx[i].xAbono),
                                xCargo = Convert.ToString(objDECCabType[j].xDetalleTrx[i].xCargo),
                                xDescripDocumento = objDECCabType[j].xDetalleTrx[i].xDescripDocumento,
                                xDescripExtend = objDECCabType[j].xDetalleTrx[i].xDescripExtend,
                                xDocAnio = Convert.ToString(objDECCabType[j].xDetalleTrx[i].xDocAnio),
                                xDocAnioVenc = Convert.ToString(objDECCabType[j].xDetalleTrx[i].xDocAnioVenc),
                                xDocMes = Convert.ToString(objDECCabType[j].xDetalleTrx[i].xDocMes),
                                xDocMesVenc = Convert.ToString(objDECCabType[j].xDetalleTrx[i].xDocMesVenc),
                                xEstadoDocumento = objDECCabType[j].xDetalleTrx[i].xEstadoDocumento,
                                xFechaEmision = Convert.ToString(objDECCabType[j].xDetalleTrx[i].xFechaEmision),
                                xFechaPago = Convert.ToString(objDECCabType[j].xDetalleTrx[i].xFechaPago),
                                xFechaRegistro = Convert.ToString(objDECCabType[j].xDetalleTrx[i].xFechaRegistro),
                                xFechaVencimiento = Convert.ToString(objDECCabType[j].xDetalleTrx[i].xFechaVencimiento),
                                xFlagCargoCta = objDECCabType[j].xDetalleTrx[i].xFlagCargoCta,
                                xFormaPago = objDECCabType[j].xDetalleTrx[i].xFormaPago,
                                xIdDocOAC = Convert.ToString(objDECCabType[j].xDetalleTrx[i].xIdDocOAC),
                                xIdDocOrigen = objDECCabType[j].xDetalleTrx[i].xIdDocOrigen,
                                xMontoDocumento = Convert.ToString(objDECCabType[j].xDetalleTrx[i].xMontoDocumento),
                                xMontoDolares = Convert.ToString(objDECCabType[j].xDetalleTrx[i].xMontoDolares),
                                xMontoFco = Convert.ToString(objDECCabType[j].xDetalleTrx[i].xMontoFco),
                                xMontoFinan = Convert.ToString(objDECCabType[j].xDetalleTrx[i].xMontoFinan),
                                xMontoReclamado = Convert.ToString(objDECCabType[j].xDetalleTrx[i].xMontoReclamado),
                                xMontoSoles = Convert.ToString(objDECCabType[j].xDetalleTrx[i].xMontoSoles),
                                xNroDocumento = objDECCabType[j].xDetalleTrx[i].xNroDocumento,
                                xNroOperacionPago = objDECCabType[j].xDetalleTrx[i].xNroOperacionPago,
                                xNroTicket = objDECCabType[j].xDetalleTrx[i].xNroTicket,
                                xSaldoCuenta = Convert.ToString(objDECCabType[j].xDetalleTrx[i].xSaldoCuenta),
                                xSaldoDocumento = Convert.ToString(objDECCabType[j].xDetalleTrx[i].xSaldoDocumento),
                                xSaldoFco = Convert.ToString(objDECCabType[j].xDetalleTrx[i].xSaldoFco),
                                xSaldoFinan = Convert.ToString(objDECCabType[j].xDetalleTrx[i].xSaldoFinan),
                                xTelefono = objDECCabType[j].xDetalleTrx[i].xTelefono,
                                xTipoDocumento = objDECCabType[j].xDetalleTrx[i].xTipoDocumento,
                                xTipoMoneda = objDECCabType[j].xDetalleTrx[i].xTipoMoneda,
                                xUsuario = objDECCabType[j].xDetalleTrx[i].xUsuario
                            };

                            listStatusAccountAOCDet.Add(objStatusAccountAOCDet);

                            }

                        objStatusAccountAOC = new AccountStatusDetailH()
                        {
                            xCicloFacturacion = objDECCabType[j].xCicloFacturacion,
                            xCodCuenta = objDECCabType[j].xCodCuenta,
                            xCodCuentaAlterna = objDECCabType[j].xCodCuentaAlterna,
                            xCreditScore = objDECCabType[j].xCreditScore,
                            xDescUbigeo = objDECCabType[j].xDescUbigeo,
                            xDetalleTrx = new TrxDetail()
                        {
                                xDetalleEstadoCuenta = listStatusAccountAOCDet
                            },
                            xDeudaActual = Convert.ToString(objDECCabType[j].xDeudaActual),
                            xDeudaVencida = Convert.ToString(objDECCabType[j].xDeudaVencida),
                            xEstadoCuenta = objDECCabType[j].xEstadoCuenta,
                            xFechaActivacion = Convert.ToString(objDECCabType[j].xFechaActivacion),
                            xFechaUltFactura = Convert.ToString(objDECCabType[j].xFechaUltFactura),
                            xFechaUtlPago = Convert.ToString(objDECCabType[j].xFechaUtlPago),
                            xLimiteCredito = Convert.ToString(objDECCabType[j].xLimiteCredito),
                            xNombreCliente = objDECCabType[j].xNombreCliente,
                            xTipoCliente = objDECCabType[j].xTipoCliente,
                            xTipoPago = objDECCabType[j].xTipoPago,
                            xTotalMontoDisputa = Convert.ToString(objDECCabType[j].xTotalMontoDisputa)
                        };

                        listStatusAccountAOC.Add(objStatusAccountAOC);
                    }
                }

                response = new AccountStatusResponse()
                {
                    audit = new EntitiesFixed.GetAccountStatus.Audit()
                    {
                        errorCode = auditTypeResponse.errorCode,
                        errorMsg = auditTypeResponse.errorMsg,
                        txId = auditTypeResponse.txId
                    },
                    xEstadoCuenta = new AccountStatus()
                {
                        xDetalleEstadoCuentaCab = listStatusAccountAOC
                }
                };

            }
            Claro.Web.Logging.Info(DateTime.Now.ToString("yyyyMMddhhmmss"),DateTime.Now.ToString("yyyyMMddhhmmss"),
                string.Format("el resultado resumido de response es: errorCode:{0}, errorMsg {1}, txId:{2}, response.xEstadoCuenta.xDetalleEstadoCuentaCab.Count:{3}", response.audit.errorCode, response.audit.errorMsg, response.audit.txId, response.xEstadoCuenta.xDetalleEstadoCuentaCab.Count.ToString()));
            Claro.Web.Logging.Info(DateTime.Now.ToString("yyyyMMddhhmmss"), DateTime.Now.ToString("yyyyMMddhhmmss"), "salida del metodo GetStatusAccountFixedAOC");
            return response;
        }
        //32650

        public static ENTITIES_QUERYDEBT.QueryDebtResponse GetDebtQuery(ENTITIES_QUERYDEBT.QueryDebtRequest objQueryDebtRequest)
        {
            Claro.Web.Logging.Info(DateTime.Now.ToString("yyyyMMddhhmmss"), "GetDebtQuery", string.Format("entrada a metodo para llamada al ws de consultapagos."));
            ENTITIES_QUERYDEBT.QueryDebtResponse objQueryDebtResponse = null;
            List<ENTITIES_QUERYDEBT.DebtServiceType> listServiceType = null;
            ENTITIES_QUERYDEBT.DebtServiceType oServiceType = null;
            List<ENTITIES_QUERYDEBT.DebtDocumentType> listDocumentType = null;
            ENTITIES_QUERYDEBT.DebtDocumentType oDocumentType = null;

            try
            {
                //INI
                QUERYDEBT.consultaDeudaResponseBody responseConsultaDeuda = null;

                responseConsultaDeuda = Claro.Web.Logging.ExecuteMethod<QUERYDEBT.consultaDeudaResponseBody>(objQueryDebtRequest.Audit.Session, objQueryDebtRequest.Audit.Transaction,
                  Configuration.ServiceConfiguration.CONSULTA_PAGOS, () =>
                  {
                      return Configuration.ServiceConfiguration.CONSULTA_PAGOS.consultaDeuda(
                              objQueryDebtRequest.pTxId,
                              objQueryDebtRequest.pCodAplicacion,
                              objQueryDebtRequest.pCodBanco,
                              objQueryDebtRequest.pCodReenvia,
                              objQueryDebtRequest.pCodMoneda,
                              objQueryDebtRequest.pCodTipoServicio,
                              objQueryDebtRequest.pPosUltDocumento,
                              objQueryDebtRequest.pTipoIdentific,
                              objQueryDebtRequest.pDatoIdentific,
                              objQueryDebtRequest.pNombreComercio,
                              objQueryDebtRequest.pNumeroComercio,
                              objQueryDebtRequest.pCodAgencia,
                              objQueryDebtRequest.pCodCanal,
                              objQueryDebtRequest.pCodCiudad,
                              objQueryDebtRequest.pNroTerminal,
                              objQueryDebtRequest.pPlaza,
                          objQueryDebtRequest.pNroReferencia
                    );
                  });
                if (responseConsultaDeuda.xErrMessage != null)
                {

                    listServiceType = new List<ENTITIES_QUERYDEBT.DebtServiceType>();

                    for (int j = 0; j < responseConsultaDeuda.xDeudaCliente.Count; j++)
                    {

                        listDocumentType = new List<ENTITIES_QUERYDEBT.DebtDocumentType>();
                        var oCurrentDeudaCliente = responseConsultaDeuda.xDeudaCliente[j];

                        if (responseConsultaDeuda.xDeudaCliente[j].xDeudaDocs.Count >= 1)
                        {
                            for (int i = 0; i < responseConsultaDeuda.xDeudaCliente[j].xDeudaDocs.Count; i++)
                            {
                                var oCurrentDeudaDoc = oCurrentDeudaCliente.xDeudaDocs[i];

                                oDocumentType = new ENTITIES_QUERYDEBT.DebtDocumentType
                                {
                                    xCodConcepto1 = oCurrentDeudaDoc.xCodConcepto1,
                                    xCodConcepto2 = oCurrentDeudaDoc.xCodConcepto2,
                                    xCodConcepto3 = oCurrentDeudaDoc.xCodConcepto3,
                                    xCodConcepto4 = oCurrentDeudaDoc.xCodConcepto4,
                                    xCodConcepto5 = oCurrentDeudaDoc.xCodConcepto5,
                                    xDatoDocumento = oCurrentDeudaDoc.xDatoDocumento,
                                    xDescripServ = oCurrentDeudaDoc.xDescripServ,
                                    xFechaEmision = oCurrentDeudaDoc.xFechaEmision.ToString(),
                                    xFechaVenc = oCurrentDeudaDoc.xFechaVenc.ToString(),
                                    xImporteConcepto1 = oCurrentDeudaDoc.xImporteConcepto1.ToString(),
                                    xImporteConcepto2 = oCurrentDeudaDoc.xImporteConcepto2.ToString(),
                                    xImporteConcepto3 = oCurrentDeudaDoc.xImporteConcepto3.ToString(),
                                    xImporteConcepto4 = oCurrentDeudaDoc.xImporteConcepto4.ToString(),
                                    xImporteConcepto5 = oCurrentDeudaDoc.xImporteConcepto5.ToString(),
                                    xImportePagoMin = oCurrentDeudaDoc.xImportePagoMin.ToString(),
                                    xMontoDebe = oCurrentDeudaDoc.xMontoDebe.ToString(),
                                    xMontoFact = oCurrentDeudaDoc.xMontoFact.ToString(),
                                    xNumeroDoc = oCurrentDeudaDoc.xNumeroDoc,
                                    xReferenciaDeuda = oCurrentDeudaDoc.xReferenciaDeuda,
                                    xTipoServicio = oCurrentDeudaDoc.xTipoServicio
                                };
                                listDocumentType.Add(oDocumentType);
                            }

                            oServiceType = new ENTITIES_QUERYDEBT.DebtServiceType()
                            {
                                xCodMoneda = oCurrentDeudaCliente.xCodMoneda,
                                xCodTipoServicio = oCurrentDeudaCliente.xCodTipoServicio,
                                xDatoServicio = oCurrentDeudaCliente.xDatoServicio,
                                xEstadoDeudor = oCurrentDeudaCliente.xEstadoDeudor,
                                xFlagCronologPago = oCurrentDeudaCliente.xFlagCronologPago,
                                xFlagPagoParcial = oCurrentDeudaCliente.xFlagPagoParcial,
                                xFlagPagoVencido = oCurrentDeudaCliente.xFlagPagoVencido,
                                xFlagRestricPago = oCurrentDeudaCliente.xFlagRestricPago,
                                xMensaje1 = oCurrentDeudaCliente.xMensaje1,
                                xMensaje2 = oCurrentDeudaCliente.xMensaje2,
                                xMontoDeudaTotal = oCurrentDeudaCliente.xMontoDeudaTotal,
                                xNroDocs = oCurrentDeudaCliente.xNroDocs,
                                xOpcionRecaudacion = oCurrentDeudaCliente.xOpcionRecaudacion,
                                xDeudaDocs = listDocumentType
                            };
                            listServiceType.Add(oServiceType);
                        }
                    }


                    objQueryDebtResponse = new ENTITIES_QUERYDEBT.QueryDebtResponse()
                    {
                        audit = new ENTITIES_QUERYDEBT.AuditType()
                        {
                            errorCode = responseConsultaDeuda.audit.errorCode,
                            errorMsg = responseConsultaDeuda.audit.errorMsg,
                            txtId = responseConsultaDeuda.audit.txId
                        },
                        xIdentificacion = responseConsultaDeuda.xIdentificacion,
                        xNombreCliente = responseConsultaDeuda.xNombreCliente,
                        xMasDocumentosFlag = responseConsultaDeuda.xMasDocumentosFlag,
                        xPosUltDocumento = responseConsultaDeuda.xPosUltDocumento,
                        xNroReferencia = responseConsultaDeuda.xNroReferencia,
                        xNroIdentifCliente = responseConsultaDeuda.xNroIdentifCliente,
                        xNroServDevueltos = responseConsultaDeuda.xNroServDevueltos.ToString(),
                        xNroDocsDeuda = responseConsultaDeuda.xNroDocsDeuda,
                        xDatoTransaccion = responseConsultaDeuda.xDatoTransaccion,
                        xDeudaCliente = listServiceType,
                        xErrStatus = responseConsultaDeuda.xErrStatus,
                        xErrMessage = responseConsultaDeuda.xErrMessage
                    };
                }
                // FIN
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objQueryDebtRequest.Audit.UserName, objQueryDebtRequest.Audit.Transaction, "Error GetDebtQuery:" + ex.Message + ", Error Net Largo"+ ex.ToString());
            }
            Claro.Web.Logging.Info(DateTime.Now.ToString("yyyyMMddhhmmss"), "GetDebtQuery", string.Format("salida de metodo para llamada al ws de consultapagos."));
            return objQueryDebtResponse;
        }

        public static bool RegisterBonoDiscount(RegisterBonoDiscountRequest item)
        {
            var resul = false;
            var strCod = 0;
            var strDesc = "";
            try
            {
                DbParameter[] parameters =
                                     {   
                                       new DbParameter("PI_CO_ID", DbType.Int64, ParameterDirection.Input),
                                       new DbParameter("PI_ID_CAMPANA", DbType.Int64, ParameterDirection.Input),
                                       new DbParameter("PI_PORCENTAJE", DbType.Double,10, ParameterDirection.Input),
                                       new DbParameter("PI_MONTO", DbType.Double,10,ParameterDirection.Input),
                                       new DbParameter("PI_PERIODO", DbType.Int64,ParameterDirection.Input),
                                       new DbParameter("PI_SNCODE", DbType.Int64,ParameterDirection.Input),
                                       new DbParameter("PI_COSTO_INST", DbType.Double,10,ParameterDirection.Input), 
                                       new DbParameter("PI_FLAG", DbType.Int64,ParameterDirection.Input),
                                       new DbParameter("PO_COD_ERR", DbType.String,255,ParameterDirection.Output),
                                       new DbParameter("PO_DES_ERR", DbType.String,255,ParameterDirection.Output),	
                                    };
                for (int j = 0; j < parameters.Length; j++)
                {
                    parameters[j].Value = System.DBNull.Value;
                }
                parameters[0].Value = Convert.ToInt64(item.pi_co_id);
                parameters[1].Value = Convert.ToInt64(item.pi_id_campana);
                parameters[2].Value = item.pi_porcentaje;
                parameters[3].Value = item.pi_monto;
                parameters[4].Value = item.pi_periodo;
                parameters[5].Value = item.pi_sncode;
                parameters[6].Value = item.pi_costo_inst;
                parameters[7].Value = item.pi_flag;

                Web.Logging.Info(item.Audit.Session, item.Audit.Transaction, "Begin SQL statement");
                Web.Logging.ExecuteMethod(item.Audit.Session, item.Audit.Transaction, () =>
                {
                    DbFactory.ExecuteNonQuery(item.Audit.Session, item.Audit.Transaction, DbConnectionConfiguration.SIAC_POST_BSCS,
                        DbCommandConfiguration.SIACU_POST_BSCS_BSCSSI_REG_BONOS_DESC, parameters);
                });
                strCod = Convert.ToInt(parameters[8].Value.ToString());
                strDesc = parameters[9].Value.ToString();
                if (strCod == 0)
                    resul = true;
                Web.Logging.Info(item.Audit.Session, item.Audit.Transaction, "Begin SQL statement - Cod:" + strCod + ", Descrip:" + strDesc);
            }
            catch (Exception ex)
            {
                Web.Logging.Error(item.Audit.Session, item.Audit.Transaction, ex.Message);
            }
            return resul;
        }

        public static string RegisterNuevaInteraccion(RegisterNuevaInteraccionRequest objRequest)
        {
            var interaccionId = "";
            try
            {
                string stridInteraccion = string.Empty, strFlagCreacion = string.Empty, strMsgText = string.Empty; ;
                var response = new AuditType();
                Claro.SIACU.ProxyService.Transac.Service.TransaccionInteracciones.auditRequest objAudit = new ProxyService.Transac.Service.TransaccionInteracciones.auditRequest();
                Claro.SIACU.ProxyService.Transac.Service.TransaccionInteracciones.ListaResponseOpcional lstResponseOpcional = new ListaResponseOpcional();
                Claro.SIACU.ProxyService.Transac.Service.TransaccionInteracciones.ListaRequestOpcional lstReqOpcional = new Claro.SIACU.ProxyService.Transac.Service.TransaccionInteracciones.ListaRequestOpcional();
                Claro.SIACU.ProxyService.Transac.Service.TransaccionInteracciones.auditResponse objAuditResponse = new ProxyService.Transac.Service.TransaccionInteracciones.auditResponse();
                objAudit.idTransaccion = objRequest.Audit.Session;
                objAudit.ipAplicacion = objRequest.Audit.IPAddress;
                objAudit.usrAplicacion = objRequest.Audit.UserName;
                objAudit.aplicacion = "SIACUNICO";
                var interaccion = new InteraccionType()
                {
                    clase = objRequest.interaccion.clase,
                    codigoEmpleado = objRequest.interaccion.codigoEmpleado,
                    codigoSistema = objRequest.interaccion.codigoSistema,
                    cuenta = objRequest.interaccion.cuenta != null ? objRequest.interaccion.cuenta : "",
                    flagCaso = objRequest.interaccion.flagCaso,
                    hechoEnUno = objRequest.interaccion.hechoEnUno,
                    metodoContacto = objRequest.interaccion.metodoContacto,
                    notas = objRequest.interaccion.notas != null ? objRequest.interaccion.notas : "",
                    objId = objRequest.interaccion.objId != null ? objRequest.interaccion.objId : "",
                    siteObjId = objRequest.interaccion.siteObjId != null ? objRequest.interaccion.siteObjId : "",
                    subClase = objRequest.interaccion.subClase,
                    telefono = objRequest.interaccion.telefono,
                    textResultado = objRequest.interaccion.textResultado,
                    tipo = objRequest.interaccion.tipo,
                    tipoInteraccion = objRequest.interaccion.tipoInteraccion,
                };
                Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "Begin WS method");
                //response = ServiceConfiguration.TransaccionInteraccion.nuevaInteraccion(objRequest.txId, interaccion, out interaccionId);
                Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    objAuditResponse = ServiceConfiguration.TransaccionInteraccion.crearInteraccionConNivel(objAudit, int.Parse(objRequest.interaccion.objId), int.Parse(interaccion.siteObjId), 
                    objRequest.interaccion.cuenta, objRequest.interaccion.telefono, objRequest.interaccion.tipo, objRequest.interaccion.clase, 
                    objRequest.interaccion.subClase, objRequest.interaccion.metodoContacto, objRequest.interaccion.tipoInteraccion, objRequest.interaccion.codigoEmpleado, objRequest.interaccion.codigoSistema, 
                    0, objRequest.interaccion.notas, objRequest.interaccion.flagCaso, objRequest.interaccion.textResultado, objRequest.interaccion.servAfect, objRequest.interaccion.inconv, objRequest.interaccion.servAfectCode,
                    objRequest.interaccion.inconvenCode, objRequest.interaccion.coId, objRequest.interaccion.codPlano, objRequest.interaccion.valor1, objRequest.interaccion.valor2, lstReqOpcional, out  stridInteraccion, out strFlagCreacion, out strMsgText, out lstResponseOpcional);
                });

                if (objAuditResponse.codigoRespuesta == ConstantsHFC.strCero && strFlagCreacion == ConstantsHFC.CriterioMensajeOK)
                    interaccionId = stridInteraccion;
                Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "End WS method - Cod: " + response.errorCode + ", Descrip: " + response.errorMsg);
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, "Error RegisterNuevaInteraccion: " + ex.Message);
            }
            return interaccionId;
        }

        public static string RegisterNuevaInteraccionPlus(RegisterNuevaInteraccionPlusRequest objRequest)
        {
            var interaccionPlusId = "";
            try
            {
                object obj = objRequest.interaccionPlus;
                System.Reflection.PropertyInfo[] properties = obj.GetType().GetProperties();
                foreach (var p in properties)
                {
                    if (p.GetValue(obj) == null && p.PropertyType == typeof(string))
                        p.SetValue(obj, "");
                }
                var response = new AuditType();
                var interaccion = new InteraccionPlusType()
                {
                    p_nro_interaccion = objRequest.interaccionPlus.p_nro_interaccion,
                    p_inter_1 = objRequest.interaccionPlus.p_inter_1,
                    p_inter_2 = objRequest.interaccionPlus.p_inter_2,
                    p_inter_3 = objRequest.interaccionPlus.p_inter_3,
                    p_inter_4 = objRequest.interaccionPlus.p_inter_4,
                    p_inter_5 = objRequest.interaccionPlus.p_inter_5,
                    p_inter_6 = objRequest.interaccionPlus.p_inter_6,
                    p_inter_7 = objRequest.interaccionPlus.p_inter_7,
                    p_inter_8 = objRequest.interaccionPlus.p_inter_8,
                    p_inter_9 = objRequest.interaccionPlus.p_inter_9,
                    p_inter_10 = objRequest.interaccionPlus.p_inter_10,
                    p_inter_11 = objRequest.interaccionPlus.p_inter_11,
                    p_inter_12 = objRequest.interaccionPlus.p_inter_12,
                    p_inter_13 = objRequest.interaccionPlus.p_inter_13,
                    p_inter_14 = objRequest.interaccionPlus.p_inter_14,
                    p_inter_15 = objRequest.interaccionPlus.p_inter_15,
                    p_inter_16 = objRequest.interaccionPlus.p_inter_16,
                    p_inter_17 = objRequest.interaccionPlus.p_inter_17,
                    p_inter_18 = objRequest.interaccionPlus.p_inter_18,
                    p_inter_19 = objRequest.interaccionPlus.p_inter_19,
                    p_inter_20 = objRequest.interaccionPlus.p_inter_20,
                    p_inter_21 = objRequest.interaccionPlus.p_inter_21,
                    p_inter_22 = objRequest.interaccionPlus.p_inter_22,
                    p_inter_23 = objRequest.interaccionPlus.p_inter_23,
                    p_inter_24 = objRequest.interaccionPlus.p_inter_24,
                    p_inter_25 = objRequest.interaccionPlus.p_inter_25,
                    p_inter_26 = objRequest.interaccionPlus.p_inter_26,
                    p_inter_27 = objRequest.interaccionPlus.p_inter_27,
                    p_inter_28 = objRequest.interaccionPlus.p_inter_28,
                    p_inter_29 = objRequest.interaccionPlus.p_inter_29,
                    p_inter_30 = objRequest.interaccionPlus.p_inter_30,
                    p_plus_inter2interact = objRequest.interaccionPlus.p_plus_inter2interact,
                    p_adjustment_amount = objRequest.interaccionPlus.p_adjustment_amount,
                    p_adjustment_reason = objRequest.interaccionPlus.p_adjustment_reason,
                    p_address = objRequest.interaccionPlus.p_address,
                    p_amount_unit = objRequest.interaccionPlus.p_amount_unit,
                    p_birthday = objRequest.interaccionPlus.p_birthday,
                    p_clarify_interaction = objRequest.interaccionPlus.p_clarify_interaction,
                    p_claro_ldn1 = objRequest.interaccionPlus.p_claro_ldn1,
                    p_claro_ldn2 = objRequest.interaccionPlus.p_claro_ldn2,
                    p_claro_ldn3 = objRequest.interaccionPlus.p_claro_ldn3,
                    p_claro_ldn4 = objRequest.interaccionPlus.p_claro_ldn4,
                    p_clarolocal1 = objRequest.interaccionPlus.p_clarolocal1,
                    p_clarolocal2 = objRequest.interaccionPlus.p_clarolocal2,
                    p_clarolocal3 = objRequest.interaccionPlus.p_clarolocal3,
                    p_clarolocal4 = objRequest.interaccionPlus.p_clarolocal4,
                    p_clarolocal5 = objRequest.interaccionPlus.p_clarolocal5,
                    p_clarolocal6 = objRequest.interaccionPlus.p_clarolocal6,
                    p_contact_phone = objRequest.interaccionPlus.p_contact_phone,
                    p_dni_legal_rep = objRequest.interaccionPlus.p_dni_legal_rep,
                    p_document_number = objRequest.interaccionPlus.p_document_number,
                    p_email = objRequest.interaccionPlus.p_email,
                    p_first_name = objRequest.interaccionPlus.p_first_name,
                    p_fixed_number = objRequest.interaccionPlus.p_fixed_number,
                    p_flag_change_user = objRequest.interaccionPlus.p_flag_change_user,
                    p_flag_legal_rep = objRequest.interaccionPlus.p_flag_legal_rep,
                    p_flag_other = objRequest.interaccionPlus.p_flag_other,
                    p_flag_titular = objRequest.interaccionPlus.p_flag_titular,
                    p_imei = objRequest.interaccionPlus.p_imei,
                    p_last_name = objRequest.interaccionPlus.p_last_name,
                    p_lastname_rep = objRequest.interaccionPlus.p_lastname_rep,
                    p_ldi_number = objRequest.interaccionPlus.p_ldi_number,
                    p_name_legal_rep = objRequest.interaccionPlus.p_name_legal_rep,
                    p_old_claro_ldn1 = objRequest.interaccionPlus.p_old_claro_ldn1,
                    p_old_claro_ldn2 = objRequest.interaccionPlus.p_old_claro_ldn2,
                    p_old_claro_ldn3 = objRequest.interaccionPlus.p_old_claro_ldn3,
                    p_old_claro_ldn4 = objRequest.interaccionPlus.p_old_claro_ldn4,
                    p_old_clarolocal1 = objRequest.interaccionPlus.p_old_clarolocal1,
                    p_old_clarolocal2 = objRequest.interaccionPlus.p_old_clarolocal2,
                    p_old_clarolocal3 = objRequest.interaccionPlus.p_old_clarolocal3,
                    p_old_clarolocal4 = objRequest.interaccionPlus.p_old_clarolocal4,
                    p_old_clarolocal5 = objRequest.interaccionPlus.p_old_clarolocal5,
                    p_old_clarolocal6 = objRequest.interaccionPlus.p_old_clarolocal6,
                    p_old_doc_number = objRequest.interaccionPlus.p_old_doc_number,
                    p_old_first_name = objRequest.interaccionPlus.p_old_first_name,
                    p_old_fixed_phone = objRequest.interaccionPlus.p_old_fixed_phone,
                    p_old_last_name = objRequest.interaccionPlus.p_old_last_name,
                    p_old_ldi_number = objRequest.interaccionPlus.p_old_ldi_number,
                    p_old_fixed_number = objRequest.interaccionPlus.p_old_fixed_number,
                    p_operation_type = objRequest.interaccionPlus.p_operation_type,
                    p_other_doc_number = objRequest.interaccionPlus.p_other_doc_number,
                    p_other_first_name = objRequest.interaccionPlus.p_other_first_name,
                    p_other_last_name = objRequest.interaccionPlus.p_other_last_name,
                    p_other_phone = objRequest.interaccionPlus.p_other_phone,
                    p_phone_legal_rep = objRequest.interaccionPlus.p_phone_legal_rep,
                    p_reference_phone = objRequest.interaccionPlus.p_reference_phone,
                    p_reason = objRequest.interaccionPlus.p_reason,
                    p_model = objRequest.interaccionPlus.p_model,
                    p_lot_code = objRequest.interaccionPlus.p_lot_code,
                    p_flag_registered = objRequest.interaccionPlus.p_flag_registered,
                    p_registration_reason = objRequest.interaccionPlus.p_registration_reason,
                    p_claro_number = objRequest.interaccionPlus.p_claro_number,
                    p_month = objRequest.interaccionPlus.p_month,
                    p_ost_number = objRequest.interaccionPlus.p_ost_number,
                    p_basket = objRequest.interaccionPlus.p_basket,
                    p_expire_date = objRequest.interaccionPlus.p_expire_date,
                    p_ADDRESS5 = objRequest.interaccionPlus.p_ADDRESS5,
                    p_CHARGE_AMOUNT = objRequest.interaccionPlus.p_CHARGE_AMOUNT.Replace(',', '.'),
                    p_CITY = objRequest.interaccionPlus.p_CITY,
                    p_CONTACT_SEX = objRequest.interaccionPlus.p_CONTACT_SEX,
                    p_DEPARTMENT = objRequest.interaccionPlus.p_DEPARTMENT,
                    p_DISTRICT = objRequest.interaccionPlus.p_DISTRICT,
                    p_EMAIL_CONFIRMATION = objRequest.interaccionPlus.p_EMAIL_CONFIRMATION,
                    p_FAX = objRequest.interaccionPlus.p_FAX,
                    p_FLAG_CHARGE = objRequest.interaccionPlus.p_FLAG_CHARGE,
                    p_MARITAL_STATUS = objRequest.interaccionPlus.p_MARITAL_STATUS,
                    p_OCCUPATION = objRequest.interaccionPlus.p_OCCUPATION,
                    p_POSITION = objRequest.interaccionPlus.p_POSITION,
                    p_REFERENCE_ADDRESS = objRequest.interaccionPlus.p_REFERENCE_ADDRESS,
                    p_TYPE_DOCUMENT = objRequest.interaccionPlus.p_TYPE_DOCUMENT,
                    p_ZIPCODE = objRequest.interaccionPlus.p_ZIPCODE,
                    p_iccid = objRequest.interaccionPlus.p_iccid,
                };
                Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "Begin WS method");
                response = ServiceConfiguration.TransaccionInteraccion.nuevaInteraccionPlus(objRequest.txId, interaccion, out interaccionPlusId);
                if (response.errorCode != "OK")
                    interaccionPlusId = "";
                Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "End WS method - Cod: " + response.errorCode + ", Descrip: " + response.errorMsg);
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, "Error RegisterNuevaInteraccionPlus: " + ex.Message);
            }
            return interaccionPlusId;
        }

        public static bool ActualizarDatosMenores(Customer item)
        {
            var resul = false;

            var strCod = -1;

            try
            {
                DbParameter[] parameters =
                                     {   
                                     new DbParameter("p_CustomerID", DbType.Int64,ParameterDirection.Input),
												   new DbParameter("p_Cargo", DbType.String,40,ParameterDirection.Input),
												   new DbParameter("p_Telefono", DbType.String,25,ParameterDirection.Input),
												   new DbParameter("p_Celular", DbType.String,25,ParameterDirection.Input),
												   new DbParameter("p_Fax", DbType.String,25,ParameterDirection.Input),
												   new DbParameter("p_Email", DbType.String,200,ParameterDirection.Input),
												   new DbParameter("p_NombreComercial", DbType.String,20,ParameterDirection.Input),
												   new DbParameter("p_ContactoCliente", DbType.String,40,ParameterDirection.Input),
												   new DbParameter("p_FechaNacimiento", DbType.DateTime,ParameterDirection.Input),
												   new DbParameter("p_Nacionalidad", DbType.Int32,ParameterDirection.Input),
												   new DbParameter("p_Sexo", DbType.String,1,ParameterDirection.Input),
												   new DbParameter("p_EstadoCivil", DbType.Int32,ParameterDirection.Input),
												   new DbParameter("P_Result", DbType.Int32,ParameterDirection.Output)
                                     };
                for (int j = 0; j < parameters.Length; j++)
                {
                    parameters[j].Value = System.DBNull.Value;
                }
                parameters[0].Value = item.CUSTOMER_ID;
                parameters[1].Value = item.CARGO;
                parameters[2].Value = item.TELEF_REFERENCIA;
                parameters[3].Value = item.TELEFONO;
                parameters[4].Value = item.FAX;
                parameters[5].Value = item.EMAIL;
                parameters[6].Value = item.NOMBRE_COMERCIAL;
                parameters[7].Value = item.CONTACTO_CLIENTE;
                parameters[8].Value = Convert.ToDate(item.FECHA_NAC);
                parameters[9].Value = item.LUGAR_NACIMIENTO_ID;
                parameters[10].Value = item.SEXO;
                parameters[11].Value = item.ESTADO_CIVIL_ID;

                Web.Logging.Info(item.Audit.Session, item.Audit.Transaction, "Begin SQL statement");
                Web.Logging.ExecuteMethod(item.Audit.Session, item.Audit.Transaction, () =>
                {
                    DbFactory.ExecuteNonQuery(item.Audit.Session, item.Audit.Transaction, DbConnectionConfiguration.SIAC_POST_BSCS,
                        DbCommandConfiguration.SIACU_POST_BSCS_SP_ACTUALIZA_DATOS_MENORES, parameters);
                });
                strCod = Convert.ToInt(parameters[12].Value.ToString());
                if (strCod == 1)
                    resul = true;
                Web.Logging.Info(item.Audit.Session, item.Audit.Transaction, "Begin SQL statement - Cod:" + strCod);
            }
            catch (Exception ex)
            {
                Web.Logging.Error(item.Audit.Session, item.Audit.Transaction, ex.Message);
            }
            return resul;
        }

        public static bool ActualizarDatosClarify(Customer item)
        {
            var resul = false;
            string strCod = "", strDesc = "";
            try
            {
                DbParameter[] parameters = {
                                                new DbParameter("P_OBJID", DbType.Int64,ParameterDirection.Input),
											    new DbParameter("P_TEL_REFERENCIAL", DbType.String,20,ParameterDirection.Input),
											    new DbParameter("P_FAX", DbType.String,20,ParameterDirection.Input),
											    new DbParameter("P_EMAIL", DbType.String,80,ParameterDirection.Input),
											    new DbParameter("P_FEC_NAC", DbType.Date,ParameterDirection.Input),
												new DbParameter("P_SEXO", DbType.String,1,ParameterDirection.Input),
												new DbParameter("P_EST_CIVIL", DbType.String,20,ParameterDirection.Input),
												new DbParameter("P_OCUPACION", DbType.String,20,ParameterDirection.Input),
												new DbParameter("P_NOM_COMERCIAL", DbType.String,60,ParameterDirection.Input),
												new DbParameter("P_CONTACTO_CLIENTE", DbType.String,80,ParameterDirection.Input),
												new DbParameter("P_PAIS", DbType.String,40,ParameterDirection.Input),
												new DbParameter("P_MENSAJE", DbType.String,200,ParameterDirection.Output),
												new DbParameter("P_RESULTADO", DbType.String,10,ParameterDirection.Output)
											   };
                for (int j = 0; j < parameters.Length; j++)
                    parameters[j].Value = System.DBNull.Value;
                parameters[0].Value = item.OBJID_CONTACTO;
                parameters[1].Value = item.TELEF_REFERENCIA;
                parameters[2].Value = item.FAX;
                parameters[3].Value = item.EMAIL;
                parameters[4].Value = Convert.ToDate(item.FECHA_NAC); 
                parameters[5].Value = item.SEXO;
                parameters[6].Value = item.ESTADO_CIVIL;
                parameters[7].Value = item.CARGO;
                parameters[8].Value = item.NOMBRE_COMERCIAL;
                parameters[9].Value = item.CONTACTO_CLIENTE;
                parameters[10].Value = item.LUGAR_NACIMIENTO_DES;

                Web.Logging.Info(item.Audit.Session, item.Audit.Transaction, "Begin SQL statement");
                Web.Logging.ExecuteMethod(item.Audit.Session, item.Audit.Transaction, () =>
                {
                    DbFactory.ExecuteNonQuery(item.Audit.Session, item.Audit.Transaction, DbConnectionConfiguration.SIAC_POST_CLARIFY,  // BD_TIMPROD
                        DbCommandConfiguration.SIAC_POST_SP_UPDATE_CUSTOMER_CLF, parameters);
                });
                strDesc = parameters[11].Value.ToString();
                strCod = parameters[12].Value.ToString();
                if (strCod == "OK")
                    resul = true;
                Web.Logging.Info(item.Audit.Session, item.Audit.Transaction, "Begin SQL statement - Cod:" + strCod + ", Descrip:" + strDesc);
            }
            catch (Exception ex)
            {
                Web.Logging.Error(item.Audit.Session, item.Audit.Transaction, ex.Message);
            }
            return resul;
        }

        public static Claro.SIACU.Entity.Transac.Service.Fixed.RegisterInteractionAdjust.RegisterInteractionAdjustResponse RegisterInteractionAdjust(string strIdSession, string strTransaction, Claro.SIACU.Entity.Transac.Service.Fixed.RegisterInteractionAdjust.RegisterInteractionAdjustRequest pobjRegisterInteraccionAjusteRequest)
        {
            RegisterInteractionAdjustResponse objRegisterInteractionAdjustResponse = new RegisterInteractionAdjustResponse();
            REGISTRAR_SAR.registrarInteraccionAjusteRequest objrequest = new REGISTRAR_SAR.registrarInteraccionAjusteRequest()
            {
                auditRequest = new REGISTRAR_SAR.auditRequestType()
                {
                    idTransaccion = pobjRegisterInteraccionAjusteRequest.Audit.Transaction,
                     ipAplicacion = pobjRegisterInteraccionAjusteRequest.Audit.IPAddress,
                     nombreAplicacion = pobjRegisterInteraccionAjusteRequest.Audit.ApplicationName,
                    usuarioAplicacion = pobjRegisterInteraccionAjusteRequest.Audit.UserName,
                },
                contingClafifyAct = pobjRegisterInteraccionAjusteRequest.ContingClafifyAct,
                pLte = pobjRegisterInteraccionAjusteRequest.pLTE,
                registrarInteraccion = new REGISTRAR_SAR.registrarTipificacionType
                {
                     piAccount= pobjRegisterInteraccionAjusteRequest.RegistrarInteraccion.piAccount,
                     piAgente = pobjRegisterInteraccionAjusteRequest.RegistrarInteraccion.piAgente,
                     piClase = pobjRegisterInteraccionAjusteRequest.RegistrarInteraccion.piClase,
                     piCodPlano = pobjRegisterInteraccionAjusteRequest.RegistrarInteraccion.piCodPlano,
                     piCoId = pobjRegisterInteraccionAjusteRequest.RegistrarInteraccion.piCoId,
                     piContactObjId1 = pobjRegisterInteraccionAjusteRequest.RegistrarInteraccion.piContactObjId1,
                     piFlagCaso = pobjRegisterInteraccionAjusteRequest.RegistrarInteraccion.piFlagCaso,
                     piHechoEnUno = pobjRegisterInteraccionAjusteRequest.RegistrarInteraccion.piHechoEnUno,
                     piInconven = pobjRegisterInteraccionAjusteRequest.RegistrarInteraccion.piInconven,
                     piInconvenCode = pobjRegisterInteraccionAjusteRequest.RegistrarInteraccion.piInconvenCode,
                     piMetodoContacto = pobjRegisterInteraccionAjusteRequest.RegistrarInteraccion.piMetodoContacto,
                     piNotas = pobjRegisterInteraccionAjusteRequest.RegistrarInteraccion.piNotas,
                     piPhone = pobjRegisterInteraccionAjusteRequest.RegistrarInteraccion.piPhone,
                     piResultado = pobjRegisterInteraccionAjusteRequest.RegistrarInteraccion.piResultado,
                     piServAfect = pobjRegisterInteraccionAjusteRequest.RegistrarInteraccion.piServAfect,
                     piServAfectCode = pobjRegisterInteraccionAjusteRequest.RegistrarInteraccion.piServAfectCode,
                     piSiteObjId1 = pobjRegisterInteraccionAjusteRequest.RegistrarInteraccion.piSiteObjId1,
                     piSubClase = pobjRegisterInteraccionAjusteRequest.RegistrarInteraccion.piSubClase,
                     piTipo = pobjRegisterInteraccionAjusteRequest.RegistrarInteraccion.piTipo,
                     piTipoInter = pobjRegisterInteraccionAjusteRequest.RegistrarInteraccion.piTipoInter,
                     piUsrProceso = pobjRegisterInteraccionAjusteRequest.RegistrarInteraccion.piUsrProceso,
                     piValor1 = pobjRegisterInteraccionAjusteRequest.RegistrarInteraccion.piValor1,
                     piValor2 = pobjRegisterInteraccionAjusteRequest.RegistrarInteraccion.piValor2
                },
                registrarDetalleInteraccion = new REGISTRAR_SAR.registrarDetalleTipificacionType() {
                    piInter1 = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piInter1,
                    piInter2 = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piInter2,
                    piInter3 = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piInter3,
                    piInter4 = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piInter4,
                    piInter5 = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piInter5,
                    piInter6 = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piInter6,
                    piInter7 = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piInter7,
                    piInter8 = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piInter8,
                    piInter9 = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piInter9,
                    piInter10 = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piInter10,
                    piInter11 = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piInter11,
                    piInter12 = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piInter12,
                    piInter13 = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piInter13,
                    piInter14 = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piInter14,
                    piInter15 = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piInter15,
                    piInter16 = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piInter16,
                    piInter17 = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piInter17,
                    piInter18 = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piInter18,
                    piInter19 = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piInter19,
                    piInter20 = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piInter20,
                    piInter21 = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piInter21,
                    piInter22 = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piInter22,
                    piInter23 = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piInter23,
                    piInter24 = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piInter24,
                    piInter25 = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piInter25,
                    piInter26 = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piInter26,
                    piInter27 = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piInter27,
                    piInter28 = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piInter28,
                    piInter29 = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piInter29,
                    piInter30 = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piInter30,
                    piPlusInter2interact = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piPlusInter2interact,
                    piAdjustmentAmount = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piAdjustmentAmount,
                    piAdjustmentReason = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piAdjustmentReason,
                    piAddress = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piAddress,
                    piAmountUnit = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piAmountUnit,
                    piBirthday = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piBirthday,
                    piClarifyInteraction = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piClarifyInteraction,
                    piClaroLdn1 = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piClaroLdn1,
                    piClaroLdn2 = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piClaroLdn2,
                    piClaroLdn3 = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piClaroLdn3,
                    piClaroLdn4 = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piClaroLdn4,
                    piClarolocal1 = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piClarolocal1,
                    piClarolocal2 = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piClarolocal2,
                    piClarolocal3 = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piClarolocal3,
                    piClarolocal4 = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piClarolocal4,
                    piClarolocal5 = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piClarolocal5,
                    piClarolocal6 = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piClarolocal6,
                    piContactPhone = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piContactPhone,
                    piDniLegalRep = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piDniLegalRep,
                    piDocumentNumber = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piDocumentNumber,
                    piEmail = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piEmail,
                    piFirstName = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piFirstName,
                    piFixedNumber = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piFixedNumber,
                    piFlagChangeUser = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piFlagChangeUser,
                    piFlagLegalRep = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piFlagLegalRep,
                    piFlagOther = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piFlagOther,
                    piFlagTitular = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piFlagTitular,
                    piImei = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piImei,
                    piLastName = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piLastName,
                    piLastnameRep = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piLastnameRep,
                    piLdiNumber = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piLdiNumber,
                    piNameLegalRep = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piNameLegalRep,
                    piOldClaroLdn1 = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piOldClaroLdn1,
                    piOldClaroLdn2 = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piOldClaroLdn2,
                    piOldClaroLdn3 = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piOldClaroLdn3,
                    piOldClaroLdn4 = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piOldClaroLdn4,
                    piOldClarolocal1 = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piOldClarolocal1,
                    piOldClarolocal2 = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piOldClarolocal2,
                    piOldClarolocal3 = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piOldClarolocal3,
                    piOldClarolocal4 = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piOldClarolocal4,
                    piOldClarolocal5 = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piOldClarolocal5,
                    piOldClarolocal6 = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piOldClarolocal6,
                    piOldDocNumber = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piOldDocNumber,
                    piOldFirstName = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piOldFirstName,
                    piOldFixedPhone = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piOldFixedPhone,
                    piOldLastName = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piOldLastName,
                    piOldLdiNumber = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piOldLdiNumber,
                    piOldFixedNumber = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piOldFixedNumber,
                    piOperationType = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piOperationType,
                    piOtherDocNumber = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piOtherDocNumber,
                    piOtherFirstName = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piOtherFirstName,
                    piOtherLastName = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piOtherLastName,
                    piOtherPhone = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piOtherPhone,
                    piPhoneLegalRep = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piPhoneLegalRep,
                    piReferencePhone = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piReferencePhone,
                    piReason = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piReason,
                    piModel = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piModel,
                    piLotCode = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piLotCode,
                    piFlagRegistered = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piFlagRegistered,
                    piRegistrationReason = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piRegistrationReason,
                    piClaroNumber = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piClaroNumber,
                    piMonth = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piMonth,
                    piOstNumber = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piOstNumber,
                    piBasket = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piBasket,
                    piExpireDate = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piExpireDate,
                    piAddress5 = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piAddress5,
                    piChargeAmount = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piChargeAmount,
                    piCity = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piCity,
                    piContactSex = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piContactSex,
                    piDepartment = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piDepartment,
                    piDistrict = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piDistrict,
                    piEmailConfirmation = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piEmailConfirmation,
                    piFax = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piFax,
                    piFlagCharge = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piFlagCharge,
                    piMaritalStatus = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piMaritalStatus,
                    piOccupation = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piOccupation,
                    piPosition = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piPosition,
                    piReferenceAddress = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piReferenceAddress,
                    piTypeDocument = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piTypeDocument,
                    piZipCode = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piZipCode,
                    piIccid = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleInteraccion.piIccid

                   
                },
                registrarCabeceraDoc = new REGISTRAR_SAR.registrarCabeceraDocType()
                {
                    piSubTotalAfecto = pobjRegisterInteraccionAjusteRequest.RegistrarCabeceraDoc.piSubTotalAfecto,
                    piSubTotalNoAfecto = pobjRegisterInteraccionAjusteRequest.RegistrarCabeceraDoc.piSubTotalNoAfecto,
                    piMontoIgv = pobjRegisterInteraccionAjusteRequest.RegistrarCabeceraDoc.piMontoIgv,
                    piFechaVenc = pobjRegisterInteraccionAjusteRequest.RegistrarCabeceraDoc.piFechaVenc,
                    piUsuRegistro = pobjRegisterInteraccionAjusteRequest.RegistrarCabeceraDoc.piUsuRegistro,
                    piCicloFact = pobjRegisterInteraccionAjusteRequest.RegistrarCabeceraDoc.piCicloFact,
                    piDocRef = pobjRegisterInteraccionAjusteRequest.RegistrarCabeceraDoc.piDocRef,
                    piFechaDocRef = pobjRegisterInteraccionAjusteRequest.RegistrarCabeceraDoc.piFechaDocRef,
                    piIdTipoDoc = pobjRegisterInteraccionAjusteRequest.RegistrarCabeceraDoc.piIdTipoDoc,
                    piIdResponsabCrm = pobjRegisterInteraccionAjusteRequest.RegistrarCabeceraDoc.piIdResponsabCrm,
                    piResponsabCrm = pobjRegisterInteraccionAjusteRequest.RegistrarCabeceraDoc.piResponsabCrm,
                    piIdCliente = pobjRegisterInteraccionAjusteRequest.RegistrarCabeceraDoc.piIdCliente,
                    piOhxAct = pobjRegisterInteraccionAjusteRequest.RegistrarCabeceraDoc.piOhxAct,
                    piIdTipCliente = pobjRegisterInteraccionAjusteRequest.RegistrarCabeceraDoc.piIdTipCliente,
                    piNumDoc = pobjRegisterInteraccionAjusteRequest.RegistrarCabeceraDoc.piNumDoc,
                    piClienteCta = pobjRegisterInteraccionAjusteRequest.RegistrarCabeceraDoc.piClienteCta,
                    piErrorWsSap = pobjRegisterInteraccionAjusteRequest.RegistrarCabeceraDoc.piErrorWsSap,
                    piReintentosWsSap = pobjRegisterInteraccionAjusteRequest.RegistrarCabeceraDoc.piReintentosWsSap,
                    piNombreCliente = pobjRegisterInteraccionAjusteRequest.RegistrarCabeceraDoc.piNombreCliente,
                    piCiudad = pobjRegisterInteraccionAjusteRequest.RegistrarCabeceraDoc.piCiudad,
                    piCodigoPais = pobjRegisterInteraccionAjusteRequest.RegistrarCabeceraDoc.piCodigoPais,
                    piDireccion = pobjRegisterInteraccionAjusteRequest.RegistrarCabeceraDoc.piDireccion,
                    piTipoIdentFiscal = pobjRegisterInteraccionAjusteRequest.RegistrarCabeceraDoc.piTipoIdentFiscal,
                    piNumIdentFiscal = pobjRegisterInteraccionAjusteRequest.RegistrarCabeceraDoc.piNumIdentFiscal,
                    piSubjetoImp = pobjRegisterInteraccionAjusteRequest.RegistrarCabeceraDoc.piSubjetoImp,
                    piVersionSap6 = pobjRegisterInteraccionAjusteRequest.RegistrarCabeceraDoc.piVersionSap6

                     
                },
                registrarDetalleDoc = new REGISTRAR_SAR.registrarDetalleDocType()
                {
                    listaDetDocAdicional = new REGISTRAR_SAR.detDocAdicionalType[]{
                   },
                   
                   listaRegistroDetDocum= new REGISTRAR_SAR.registroDetDocumType[]{
                       
                   },
                    piNumDocAjuste= pobjRegisterInteraccionAjusteRequest.RegistrarDetalleDoc.piNumDocAjuste
                },
                registrarAreaImputable = new REGISTRAR_SAR.registrarAreaImputableType()
                {
                    listadoAreaImputable = new REGISTRAR_SAR.areaImputableType[] { 
                      
                    }
                },
                aprobarDocumento = new REGISTRAR_SAR.aprobarDocumentoType() {
                     piIdTipoDoc=pobjRegisterInteraccionAjusteRequest.AprobarDocumento.piIdTipoDoc,
                     piUsuAprob= pobjRegisterInteraccionAjusteRequest.AprobarDocumento.piUsuAprob
                },
                registrarAjusteOAC = new REGISTRAR_SAR.registrarAjusteOACType()
                {
                    piCodAplicacionOAC = pobjRegisterInteraccionAjusteRequest.RegistrarAjusteOAC.piCodAplicacionOAC,
                    piTipoServicio = pobjRegisterInteraccionAjusteRequest.RegistrarAjusteOAC.piTipoServicio,
                    piCodCuenta = pobjRegisterInteraccionAjusteRequest.RegistrarAjusteOAC.piCodCuenta,
                    piTipoOperacion = pobjRegisterInteraccionAjusteRequest.RegistrarAjusteOAC.piTipoOperacion,
                    piTipoAjuste = pobjRegisterInteraccionAjusteRequest.RegistrarAjusteOAC.piTipoAjuste,
                    piEstado = pobjRegisterInteraccionAjusteRequest.RegistrarAjusteOAC.piEstado,
                     
                    /*docsRef= new REGISTRAR_SAR.detalleDocsRefInType[]{new REGISTRAR_SAR.detalleDocsRefInType(){  }},*/  //======================================
                     docsRef=null,
                    piIdReclamoOrigen = pobjRegisterInteraccionAjusteRequest.RegistrarAjusteOAC.piIdReclamoOrigen,
                    piMonedaOrigen = pobjRegisterInteraccionAjusteRequest.RegistrarAjusteOAC.piMonedaOrigen,
                    piNSaldoAjuste = Convert.ToDecimal(pobjRegisterInteraccionAjusteRequest.RegistrarAjusteOAC.piNSaldoAjuste),
                    piCodMotivoAjuste = pobjRegisterInteraccionAjusteRequest.RegistrarAjusteOAC.piCodMotivoAjuste,
                    piFechaAjuste = Convert.ToDate(pobjRegisterInteraccionAjusteRequest.RegistrarAjusteOAC.piFechaAjuste),
                    piFechaCancelacion = Convert.ToDate(pobjRegisterInteraccionAjusteRequest.RegistrarAjusteOAC.piFechaCancelacion)
                    
                },
                registrarExportarSap = new REGISTRAR_SAR.registrarExportarSapType() {
                    piCuentaCab = pobjRegisterInteraccionAjusteRequest.RegistrarExportarSap.piCuentaCab,
                    piTipoDocCab = pobjRegisterInteraccionAjusteRequest.RegistrarExportarSap.piTipoDocCab,
                    piClaseDocumentoCab = pobjRegisterInteraccionAjusteRequest.RegistrarExportarSap.piClaseDocumentoCab,
                    piSociedadCab = pobjRegisterInteraccionAjusteRequest.RegistrarExportarSap.piSociedadCab,
                    piMonedaCab = pobjRegisterInteraccionAjusteRequest.RegistrarExportarSap.piMonedaCab,
                    piTextoCab = pobjRegisterInteraccionAjusteRequest.RegistrarExportarSap.piTextoCab,
                    piClavePosCab = pobjRegisterInteraccionAjusteRequest.RegistrarExportarSap.piClavePosCab,
                    piClaveNegCab = pobjRegisterInteraccionAjusteRequest.RegistrarExportarSap.piClaveNegCab,
                    piTipoDocDet = pobjRegisterInteraccionAjusteRequest.RegistrarExportarSap.piTipoDocDet,
                    piIndivaDet = pobjRegisterInteraccionAjusteRequest.RegistrarExportarSap.piIndivaDet,
                    piClavePosDet = pobjRegisterInteraccionAjusteRequest.RegistrarExportarSap.piClavePosDet,
                    piClaveNegDet = pobjRegisterInteraccionAjusteRequest.RegistrarExportarSap.piClaveNegDet,
                    piTipoDocIgv = pobjRegisterInteraccionAjusteRequest.RegistrarExportarSap.piTipoDocIgv,
                    piCuentaIgv = pobjRegisterInteraccionAjusteRequest.RegistrarExportarSap.piCuentaIgv,
                    piIndicadorSap = pobjRegisterInteraccionAjusteRequest.RegistrarExportarSap.piIndicadorSap,
                    piFlagEliminarAnterior = pobjRegisterInteraccionAjusteRequest.RegistrarExportarSap.piFlagEliminarAnterior
                },
                actualizarCamposSap = new REGISTRAR_SAR.actualizarCamposSapType()
                {
                     piErrorWsSap= pobjRegisterInteraccionAjusteRequest.ActualizarCamposSap.piErrorWsSap,
                     piFlagEnvioSap = pobjRegisterInteraccionAjusteRequest.ActualizarCamposSap.piFlagEnvioSap,
                     piReintentosWsSap = pobjRegisterInteraccionAjusteRequest.ActualizarCamposSap.piReintentosWsSap
                },
                listaRequestOpcional = new REGISTRAR_SAR.parametrosTypeObjetoOpcional[] {
                }


            };
            
             
            
            REGISTRAR_SAR.detDocAdicionalType[] _listaDetDocAdicional = new REGISTRAR_SAR.detDocAdicionalType[pobjRegisterInteraccionAjusteRequest.RegistrarDetalleDoc.ListaDetDocAdicional.Count];
            REGISTRAR_SAR.detDocAdicionalType _itempAdic;
            
            for (int i = 0; i <= pobjRegisterInteraccionAjusteRequest.RegistrarDetalleDoc.ListaDetDocAdicional.Count - 1; i++)
            {
                _itempAdic = new REGISTRAR_SAR.detDocAdicionalType();
                _itempAdic.pIdCategoria = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleDoc.ListaDetDocAdicional[i].pIdCategoria;
                _itempAdic.pNombreCategoria = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleDoc.ListaDetDocAdicional[i].pNombreCategoria;
                _itempAdic.pImporte = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleDoc.ListaDetDocAdicional[i].pImporte;
                _itempAdic.pImporteAjustar = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleDoc.ListaDetDocAdicional[i].pImporteAjustar;
                _itempAdic.pIgvImporteAjustarIgv = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleDoc.ListaDetDocAdicional[i].pIgvImporteAjustarIgv;
                _itempAdic.pImporteAjustarIgv = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleDoc.ListaDetDocAdicional[i].pImporteAjustarIgv;
                _itempAdic.pIdAreaImputar = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleDoc.ListaDetDocAdicional[i].pIdAreaImputar;
                _itempAdic.pNombreAreaImputar = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleDoc.ListaDetDocAdicional[i].pNombreAreaImputar;
                _itempAdic.pIdMotivo = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleDoc.ListaDetDocAdicional[i].pIdMotivo;
                _itempAdic.pNombreMotivo = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleDoc.ListaDetDocAdicional[i].pNombreMotivo;
                _itempAdic.pIdResponsable = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleDoc.ListaDetDocAdicional[i].pIdResponsable;
               // _itempAdic.pNombreResponsable = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleDoc.ListaDetDocAdicional[i].pNombreResponsable;
                _itempAdic.pNombreResponsable = " - ";// **************************==========================================================================
                _listaDetDocAdicional[i] = _itempAdic;
            }
            objrequest.registrarDetalleDoc.listaDetDocAdicional = _listaDetDocAdicional;
            ///----
            REGISTRAR_SAR.registroDetDocumType[] _listaRegistroDetDocum = new REGISTRAR_SAR.registroDetDocumType[pobjRegisterInteraccionAjusteRequest.RegistrarDetalleDoc.ListaRegistroDetDocum.Count];
            REGISTRAR_SAR.registroDetDocumType _itemDetDocum;
            for (int i = 0; i <= pobjRegisterInteraccionAjusteRequest.RegistrarDetalleDoc.ListaRegistroDetDocum.Count - 1; i++)
            {
                _itemDetDocum = new REGISTRAR_SAR.registroDetDocumType();
                _itemDetDocum.pImporte=pobjRegisterInteraccionAjusteRequest.RegistrarDetalleDoc.ListaRegistroDetDocum[i].pImporte;
                _itemDetDocum.pMontoSinIgv=pobjRegisterInteraccionAjusteRequest.RegistrarDetalleDoc.ListaRegistroDetDocum[i].pMontoSinIgv;
                _itemDetDocum.pIgv=pobjRegisterInteraccionAjusteRequest.RegistrarDetalleDoc.ListaRegistroDetDocum[i].pIgv;
                _itemDetDocum.pTotal=pobjRegisterInteraccionAjusteRequest.RegistrarDetalleDoc.ListaRegistroDetDocum[i].pTotal;
                _itemDetDocum.pTelefono=pobjRegisterInteraccionAjusteRequest.RegistrarDetalleDoc.ListaRegistroDetDocum[i].pTelefono;
                _itemDetDocum.pFechaDesde=pobjRegisterInteraccionAjusteRequest.RegistrarDetalleDoc.ListaRegistroDetDocum[i].pFechaDesde;
                _itemDetDocum.pFechaHasta=pobjRegisterInteraccionAjusteRequest.RegistrarDetalleDoc.ListaRegistroDetDocum[i].pFechaHasta;
                _itemDetDocum.pIdCategoria=pobjRegisterInteraccionAjusteRequest.RegistrarDetalleDoc.ListaRegistroDetDocum[i].pIdCategoria;
                _itemDetDocum.pSubCategoria=pobjRegisterInteraccionAjusteRequest.RegistrarDetalleDoc.ListaRegistroDetDocum[i].pSubCategoria;
                _itemDetDocum.pTipoTran = pobjRegisterInteraccionAjusteRequest.RegistrarDetalleDoc.ListaRegistroDetDocum[i].pTipoTran;
                _listaRegistroDetDocum[i] = _itemDetDocum;
            }
            objrequest.registrarDetalleDoc.listaRegistroDetDocum = _listaRegistroDetDocum;
            //---

            REGISTRAR_SAR.areaImputableType[] _listadoAreaImputable = new REGISTRAR_SAR.areaImputableType[pobjRegisterInteraccionAjusteRequest.RegistrarAreaImputable.ListadoAreaImputable.Count];
            REGISTRAR_SAR.areaImputableType _itemAreaImp;
            for (int i = 0; i <= pobjRegisterInteraccionAjusteRequest.RegistrarAreaImputable.ListadoAreaImputable.Count - 1; i++)
            {
                _itemAreaImp = new REGISTRAR_SAR.areaImputableType();
                _itemAreaImp.piArimIdArea = pobjRegisterInteraccionAjusteRequest.RegistrarAreaImputable.ListadoAreaImputable[i].piArimIdArea;
                _itemAreaImp.piArimIdCategoria = pobjRegisterInteraccionAjusteRequest.RegistrarAreaImputable.ListadoAreaImputable[i].piArimIdCategoria;
                _itemAreaImp.piArimIdMotivo = pobjRegisterInteraccionAjusteRequest.RegistrarAreaImputable.ListadoAreaImputable[i].piArimIdMotivo;
                _itemAreaImp.piArimTotalImputable = Convert.ToDecimal(pobjRegisterInteraccionAjusteRequest.RegistrarAreaImputable.ListadoAreaImputable[i].piArimTotalImputable); /* TODo: validar que venga con cero, sino saldra error*/ 
                _listadoAreaImputable[i] = _itemAreaImp;
            }
            objrequest.registrarAreaImputable.listadoAreaImputable = _listadoAreaImputable;

            if (pobjRegisterInteraccionAjusteRequest.RegistrarAjusteOAC.docsRef != null)
            { 
                    REGISTRAR_SAR.detalleDocsRefInType[] _ListadocsRef = new REGISTRAR_SAR.detalleDocsRefInType[pobjRegisterInteraccionAjusteRequest.RegistrarAjusteOAC.docsRef.Count];
                    REGISTRAR_SAR.detalleDocsRefInType _itemSocsRef;
                    for (int i = 0; i <= pobjRegisterInteraccionAjusteRequest.RegistrarAjusteOAC.docsRef.Count- 1; i++)
                    {
                        _itemSocsRef = new REGISTRAR_SAR.detalleDocsRefInType();
                        _itemSocsRef.piNroDocumentoCxc = pobjRegisterInteraccionAjusteRequest.RegistrarAjusteOAC.docsRef[i].piNroDocumentoCxc;
                        _itemSocsRef.piTipoDocCxc = pobjRegisterInteraccionAjusteRequest.RegistrarAjusteOAC.docsRef[i].piTipoDocCxc;
                        _ListadocsRef[i] = _itemSocsRef;
                    }
                    objrequest.registrarAjusteOAC.docsRef = _ListadocsRef;
            }
            //---

            if (pobjRegisterInteraccionAjusteRequest.listaRequestOpcional != null)
            {
                    REGISTRAR_SAR.parametrosTypeObjetoOpcional[] _listaRequestOpcional = new REGISTRAR_SAR.parametrosTypeObjetoOpcional[pobjRegisterInteraccionAjusteRequest.listaRequestOpcional.Count];
                    REGISTRAR_SAR.parametrosTypeObjetoOpcional _itemReqOpcional;
                    for (int i = 0; i <=pobjRegisterInteraccionAjusteRequest.listaRequestOpcional.Count - 1; i++)
                    {
                        _itemReqOpcional = new REGISTRAR_SAR.parametrosTypeObjetoOpcional();
                        _itemReqOpcional.campo = pobjRegisterInteraccionAjusteRequest.listaRequestOpcional[i].campo;
                        _itemReqOpcional.valor = pobjRegisterInteraccionAjusteRequest.listaRequestOpcional[i].valor;
                        _listaRequestOpcional[i] = _itemReqOpcional;
                    }
                    objrequest.listaRequestOpcional = _listaRequestOpcional;
            }
            objrequest.listaRequestOpcional = null;
            REGISTRAR_SAR.registrarInteraccionAjusteResponse objResponse = Web.Logging.ExecuteMethod<REGISTRAR_SAR.registrarInteraccionAjusteResponse>(pobjRegisterInteraccionAjusteRequest.Audit.Session, pobjRegisterInteraccionAjusteRequest.Audit.Transaction,
                          Configuration.ServiceConfiguration.REGISTRAR_SAR, () =>
                          {
                              return Configuration.ServiceConfiguration.REGISTRAR_SAR.registrarInteraccionAjuste(objrequest);
                          });


            parametrosType objRes = new parametrosType();
            List<parametrosType> objLista = new List<parametrosType>();
             
                 
                objRegisterInteractionAdjustResponse = new RegisterInteractionAdjustResponse()
                {
                    auditResponse = new EntitiesFixed.RegisterInteractionAdjust.auditResponseType()
                    {
                        codigoRespuesta = objResponse.auditResponse.codigoRespuesta,
                        idTransaccion = objResponse.auditResponse.idTransaccion,
                        mensajeRespuesta = objResponse.auditResponse.mensajeRespuesta,
                    },
              
                    idDocAut = objResponse.idDocAut,
                    farenNroSar = objResponse.idInteract,
                    listaResponseOpcional = objLista

                };

             
            
            return objRegisterInteractionAdjustResponse;
        }   

        public static RegistarInstaDecoAdiHFCResponse RegistarInstaDecoAdiHFC(RegistarInstaDecoAdiHFCRequest pRegistarInstaDecoAdiHFCRequest)
        {
            RegistarInstaDecoAdiHFCResponse objRegistarInstaDecoAdiHFCResponse = new RegistarInstaDecoAdiHFCResponse();

            UNINSTALLDECOSLTE.ListaRequestOpcionalObjetoRequestOpcional[] listaOpcionalRequest = null;
            if ( pRegistarInstaDecoAdiHFCRequest.listaRequestOpcional != null)
                {
                int countListOpcional = pRegistarInstaDecoAdiHFCRequest.listaRequestOpcional.Count;
                listaOpcionalRequest = new UNINSTALLDECOSLTE.ListaRequestOpcionalObjetoRequestOpcional[countListOpcional];
                for (int i = 0; i < countListOpcional; i++)
            {
                    listaOpcionalRequest[i] = new UNINSTALLDECOSLTE.ListaRequestOpcionalObjetoRequestOpcional();
                    listaOpcionalRequest[i].campo = pRegistarInstaDecoAdiHFCRequest.listaRequestOpcional[i].campo;
                    listaOpcionalRequest[i].valor = pRegistarInstaDecoAdiHFCRequest.listaRequestOpcional[i].valor;
            }
        }

            UNINSTALLDECOSLTE.registarInstaDecoAdiHFCResponse resultRegistarInstaDecoAdiHFC = null;
            var auditRequestType = new UNINSTALLDECOSLTE.auditRequestType()
            {

                idTransaccion = pRegistarInstaDecoAdiHFCRequest.Audit.Transaction,
                ipAplicacion = pRegistarInstaDecoAdiHFCRequest.Audit.IPAddress,
                nombreAplicacion = pRegistarInstaDecoAdiHFCRequest.Audit.ApplicationName,
                usuarioAplicacion = pRegistarInstaDecoAdiHFCRequest.Audit.UserName
            };
            var registarInstaDecoAdiHFCRequest = new UNINSTALLDECOSLTE.registarInstaDecoAdiHFCRequest()
            {
                auditRequest = auditRequestType,
                apellidoCliente = pRegistarInstaDecoAdiHFCRequest.Customer.APELLIDOS,
                cicloFacturacion = pRegistarInstaDecoAdiHFCRequest.Customer.CICLO_FACTURACION,
                codigoEmpleado = pRegistarInstaDecoAdiHFCRequest.Customer.ASESOR,
                contratoId = pRegistarInstaDecoAdiHFCRequest.Customer.CONTRATO_ID,
                customerId = pRegistarInstaDecoAdiHFCRequest.Customer.CUSTOMER_ID,
                departamentoCliente = pRegistarInstaDecoAdiHFCRequest.Customer.DEPARTAMENTO,
                direccionCliente = pRegistarInstaDecoAdiHFCRequest.Customer.DOMICILIO,
                distritoCliente = pRegistarInstaDecoAdiHFCRequest.Customer.DISTRITO,
                emailCliente = pRegistarInstaDecoAdiHFCRequest.Customer.EMAIL,
                nombreCliente = pRegistarInstaDecoAdiHFCRequest.Customer.NOMBRES,
                nombreCompletoCliente = pRegistarInstaDecoAdiHFCRequest.Customer.NOMBRE_COMPLETO,
                nroDocumento = pRegistarInstaDecoAdiHFCRequest.Customer.NRO_DOC,
                objId = pRegistarInstaDecoAdiHFCRequest.Customer.OBJID_CONTACTO,
                objIdSite = pRegistarInstaDecoAdiHFCRequest.Customer.OBJID_SITE,
                paisCliente = pRegistarInstaDecoAdiHFCRequest.Customer.PAIS_LEGAL,
                provinciaCliente = pRegistarInstaDecoAdiHFCRequest.Customer.PROVINCIA,
                razonSocial = pRegistarInstaDecoAdiHFCRequest.Customer.RAZON_SOCIAL,
                representanteLegal = pRegistarInstaDecoAdiHFCRequest.Customer.REPRESENTANTE_LEGAL,
                telefono = pRegistarInstaDecoAdiHFCRequest.Customer.TELEFONO,
                telefonoReferencia = pRegistarInstaDecoAdiHFCRequest.Customer.TELEF_REFERENCIA,
                tipoDocumento = pRegistarInstaDecoAdiHFCRequest.Customer.TIPO_DOC,

                cantidadEquipo = pRegistarInstaDecoAdiHFCRequest.ServiceByPlan.CantEquipment,
                descripcionGrupoServicio = pRegistarInstaDecoAdiHFCRequest.ServiceByPlan.GroupServ,
                descripcionServicio = pRegistarInstaDecoAdiHFCRequest.ServiceByPlan.DesServSisact,
                idEquipo = pRegistarInstaDecoAdiHFCRequest.ServiceByPlan.IDEquipment,
                idServicio = pRegistarInstaDecoAdiHFCRequest.ServiceByPlan.CodServSisact,
                sNCode = pRegistarInstaDecoAdiHFCRequest.ServiceByPlan.Sncode,
                sPCode = pRegistarInstaDecoAdiHFCRequest.ServiceByPlan.Spcode,
                tipoServicio = pRegistarInstaDecoAdiHFCRequest.ServiceByPlan.ServiceType,
                codTipEquipo = pRegistarInstaDecoAdiHFCRequest.ServiceByPlan.Codtipequ,

                aplicaBono = pRegistarInstaDecoAdiHFCRequest.AplicacaBono,
                campanaId = pRegistarInstaDecoAdiHFCRequest.CodigoCampana,
                canalAtencion = pRegistarInstaDecoAdiHFCRequest.CanalAtencion,
                cargoBono = pRegistarInstaDecoAdiHFCRequest.CargoBono,
                cargoFijoCIGV = pRegistarInstaDecoAdiHFCRequest.CargoFijoCIGV,
                cargoFijoSIGV = pRegistarInstaDecoAdiHFCRequest.CargoFijoSIGV,
                codigoPlano = pRegistarInstaDecoAdiHFCRequest.CodigoPlano,
                codigoSistema = pRegistarInstaDecoAdiHFCRequest.CodigoSistema,
                codigoTipoTrabajo = pRegistarInstaDecoAdiHFCRequest.CodigoTipoTrabajo,
                codigoUbigeo = pRegistarInstaDecoAdiHFCRequest.CodigoUbigeo,
                codigoZona = pRegistarInstaDecoAdiHFCRequest.CodigoZona,
                costoInstalacion = pRegistarInstaDecoAdiHFCRequest.CostoInstalacion,
                estadoLinea = pRegistarInstaDecoAdiHFCRequest.EstadoLinea,
                fechaActivacion = pRegistarInstaDecoAdiHFCRequest.FechaActivacion,
                fechaProgramacion = pRegistarInstaDecoAdiHFCRequest.FechaProgramacion,
                fidelizar = pRegistarInstaDecoAdiHFCRequest.Fidelizar,
                flagTBono = pRegistarInstaDecoAdiHFCRequest.FlagTBono,
                franjaHoraria = pRegistarInstaDecoAdiHFCRequest.FranjaHoraria,
                montoIGV = pRegistarInstaDecoAdiHFCRequest.MontoIGV,
                numeroClaro = "",
                periodoBono = pRegistarInstaDecoAdiHFCRequest.PeriodoBono,
                planActual = pRegistarInstaDecoAdiHFCRequest.CurrentPlan,
                listaRequestOpcional = listaOpcionalRequest
            };

            try
            {
                resultRegistarInstaDecoAdiHFC = Web.Logging.ExecuteMethod<UNINSTALLDECOSLTE.registarInstaDecoAdiHFCResponse>("", "",
                    () =>
                    {
                        return ServiceConfiguration.FixedUninstallinstallDecosLte.registarInstaDecoAdiHFC(registarInstaDecoAdiHFCRequest);
                    });

                objRegistarInstaDecoAdiHFCResponse.ResponseCode = resultRegistarInstaDecoAdiHFC.auditResponse.codigoRespuesta;
                objRegistarInstaDecoAdiHFCResponse.ResponseMessage = resultRegistarInstaDecoAdiHFC.auditResponse.mensajeRespuesta;
                if (resultRegistarInstaDecoAdiHFC.listaResponseOpcional != null)
                {
                    if (resultRegistarInstaDecoAdiHFC.listaResponseOpcional.Length > 0)
                        objRegistarInstaDecoAdiHFCResponse.CodSolot = resultRegistarInstaDecoAdiHFC.listaResponseOpcional[0].valor;
                }
                else
                {
                    objRegistarInstaDecoAdiHFCResponse.CodSolot = null;
                }

            }
            catch (Exception ex)
            {
                Web.Logging.Error(pRegistarInstaDecoAdiHFCRequest.Audit.Session, pRegistarInstaDecoAdiHFCRequest.Audit.Transaction, ex.Message);
            }

            return objRegistarInstaDecoAdiHFCResponse;
        }

        public static Entity.Transac.Service.Fixed.RegisterActiDesaBonoDesc.BodyResponse RegisterActiDesaBonoDescHFC(Entity.Transac.Service.Fixed.RegisterActiDesaBonoDesc.Request objRequest)
        {
            Entity.Transac.Service.Fixed.RegisterActiDesaBonoDesc.Response response = RestService.PostInvoque<Entity.Transac.Service.Fixed.RegisterActiDesaBonoDesc.Response>(Claro.SIACU.Data.Transac.Service.Configuration.RestServiceConfiguration.REGISTRARDESCHFC, objRequest.Audit, objRequest, false);
            Entity.Transac.Service.Fixed.RegisterActiDesaBonoDesc.BodyResponse Body = new Entity.Transac.Service.Fixed.RegisterActiDesaBonoDesc.BodyResponse(); ;
            if (response.MessageResponse.Header.HeaderResponse.Status.Code == "0")
            {
                Body = response.MessageResponse.Body;
            }

            return Body;
        }


        public static Entity.Transac.Service.Fixed.RegisterActiDesaBonoDesc.BodyResponse registrarDescLTE(Entity.Transac.Service.Fixed.RegisterActiDesaBonoDesc.Request objRequest)
        {
            Entity.Transac.Service.Fixed.RegisterActiDesaBonoDesc.Response response = RestService.PostInvoque<Entity.Transac.Service.Fixed.RegisterActiDesaBonoDesc.Response>(Claro.SIACU.Data.Transac.Service.Configuration.RestServiceConfiguration.REGISTRARDESCLTE, objRequest.Audit, objRequest, false);
            Entity.Transac.Service.Fixed.RegisterActiDesaBonoDesc.BodyResponse Body = new Entity.Transac.Service.Fixed.RegisterActiDesaBonoDesc.BodyResponse();
            if (response.MessageResponse.Header.HeaderResponse.Status.Code == "0")
            {
                Body = response.MessageResponse.Body;
            }

            return Body;
        }

        
        #endregion
    }
}