using Claro.Data;
using Claro.SIACU.Data.Transac.Service.Configuration;
using BE = Claro.SIACU.Entity.Transac.Service.Fixed;
using CSTS = Claro.SIACU.Transac.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Claro.SIACU.Entity.Transac.Service.Fixed.GetDecoMatriz;
using Claro.SIACU.Entity.Transac.Service.Fixed.GetDecoType;
using Claro.SIACU.ProxyService.Transac.Service.Fixed.UninstallInstallDecosLTE;
using Newtonsoft.Json;
using UninstallInstallDecos = Claro.SIACU.ProxyService.Transac.Service.Fixed.UninstallInstallDecosLTE;
using Claro.Web;


namespace Claro.SIACU.Data.Transac.Service.Fixed
{
    public class UnistallInstallationOfDecoder
    {
        public static List<BE.Decoder> GetProductDetail(string strIdSession, string strTransaction, string vCustomerId, string vCoId, int tipoBusqueda, ref int rResultado, ref string rMensaje)
        {
            DbParameter[] parameters = 
            {
                new DbParameter("av_customer_id", DbType.String,255, ParameterDirection.Input, vCustomerId),
                new DbParameter("ac_equ_cur", DbType.Object, ParameterDirection.Output),
                new DbParameter("an_resultado", DbType.Int64,255, ParameterDirection.Output),
                new DbParameter("tipoBusqueda", DbType.Int64,255, ParameterDirection.Input, tipoBusqueda),
                new DbParameter("av_mensaje", DbType.String,255, ParameterDirection.Output)
            };

            List<BE.Decoder> listItem = null;
            BE.Decoder item = null;

            try
            {
                DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_P_CONSULTA_EQU_IW, parameters, (IDataReader reader) =>
                {
                    listItem = new List<BE.Decoder>();

                    while (reader.Read())
                    {
                        item = new BE.Decoder();
                        item.codigo_material = Convert.ToString(reader["codigo_material"]);
                        item.codigo_sap = Convert.ToString(reader["codigo_sap"]);
                        item.numero_serie = Convert.ToString(reader["numero_serie"]);
                        item.macadress = Convert.ToString(reader["macaddress"]);
                        item.descripcion_material = Convert.ToString(reader["descripcion_material"]);
                        item.abrev_material = Convert.ToString(reader["abrev_material"]);
                        item.estado_material = Convert.ToString(reader["estado_material"]);
                        item.precio_almacen = Convert.ToString(reader["precio_almacen"]);
                        item.codigo_cuenta = Convert.ToString(reader["codigo_cuenta"]);
                        item.componente = Convert.ToString(reader["componente"]);
                        item.centro = Convert.ToString(reader["centro"]);
                        item.idalm = Convert.ToString(reader["idalm"]);
                        item.almacen = Convert.ToString(reader["almacen"]);
                        item.tipo_equipo = Convert.ToString(reader["tipo_equipo"]);
                        item.id_producto = Convert.ToString(reader["idproducto"]);
                        item.id_cliente = Convert.ToString(reader["id_cliente"]);
                        item.modelo = Convert.ToString(reader["modelo"]);
                        item.convertertype = Convert.ToString(reader["convertertype"]);
                        item.servicio_principal = Convert.ToString(reader["servicio_principal"]);
                        item.headend = Convert.ToString(reader["headend"]);
                        item.ephomeexchange = Convert.ToString(reader["ephomeexchange"]);
                        item.numero = Convert.ToString(reader["numero"]);
                        item.tipoServicio = Convert.ToString(reader["tiposerv"]);

                        listItem.Add(item);
                    }
                });
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, strTransaction, ex.Message);
            }
            finally
            {
                rResultado = CSTS.Functions.CheckInt(parameters[parameters.Length - 3].Value.ToString());
                rMensaje = CSTS.Functions.CheckStr(parameters[parameters.Length - 2].Value.ToString());
            }

            return listItem;
        }
        public static bool GetProcessingServices(string strIdSession, string strTransaction, string vCoId, string vCustomerId, string vCadena, ref string rResultado, ref string rMensaje)
        {
            bool strRet = false;

            DbParameter[] parameters = 
            {
                new DbParameter("n_cod_id", DbType.Int32, ParameterDirection.Input, vCoId),
                new DbParameter("n_customer_id", DbType.Int32, ParameterDirection.Input, vCustomerId),
                new DbParameter("v_cadena", DbType.String, ParameterDirection.Input, vCadena),
                new DbParameter("n_error", DbType.Int32, ParameterDirection.Output),
                new DbParameter("v_mensaje", DbType.String, 255, ParameterDirection.Output)
            };

            try
            {
                Logging.ExecuteMethod(strIdSession, strTransaction, () =>
                {
                    DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_P_BAJA_DECO_ADICIONAL, parameters);
                    strRet = true;
                });
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, strTransaction, ex.Message);
            }
            finally
            {
                rResultado = CSTS.Functions.CheckStr(parameters[parameters.Length - 2].Value);
                rMensaje = CSTS.Functions.CheckStr(parameters[parameters.Length - 1].Value);
            }

            return strRet;
        }
        public static List<BE.DetailInteractionService> GetDecoDetailByIdService(string strIdSession, string strTransaction, string strIdServicio)
        {
            DbParameter[] parameters = 
            {
                new DbParameter("P_CONSULTA", DbType.Object, ParameterDirection.Output),
                new DbParameter("P_IDSERV", DbType.String, ParameterDirection.Input, strIdServicio)
            };

            List<BE.DetailInteractionService> listItem = null;
            BE.DetailInteractionService item = null;

            try
            {
                DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_PVU, DbCommandConfiguration.SIACU_SP_CONS_DATA_DECO_BY_ID, parameters, (IDataReader reader) =>
                {
                    listItem = new List<BE.DetailInteractionService>();

                    while (reader.Read())
                    {
                        item = new BE.DetailInteractionService();
                        item.IdServicio = CSTS.Functions.CheckStr(reader["IDSERVICIO"]);
                        item.GsrvcPrincipal = CSTS.Functions.CheckStr(reader["GSRVC_PRINCIPAL"]);
                        item.GsrvcCodigo = CSTS.Functions.CheckStr(reader["GSRVC_CODIGO"]);
                        item.Cantidad = CSTS.Functions.CheckStr(reader["CANTIDAD"]);
                        item.Servicio = CSTS.Functions.CheckStr(reader["SERVICIO"]);
                        item.Bandwid = CSTS.Functions.CheckStr(reader["BANDWID"]);
                        item.FlagLc = CSTS.Functions.CheckStr(reader["FLAG_LC"]);
                        item.CantidadIdLinea = CSTS.Functions.CheckStr(reader["CANTIDAD_IDLINEA"]);
                        item.IdEquipo = CSTS.Functions.CheckStr(reader["IDEQUIPO"]);
                        item.CodTipEqu = CSTS.Functions.CheckStr(reader["CODTIPEQU"]);
                        item.CantEquipo = CSTS.Functions.CheckStr(reader["CANT_EQUIPO"]);
                        item.Equipo = CSTS.Functions.CheckStr(reader["EQUIPO"]);
                        item.CodigoExt = CSTS.Functions.CheckStr(reader["CODIGO_EXT"]);

                        listItem.Add(item);
                    }
                });
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, strTransaction, ex.Message);
            }

            return listItem;
        }
        public static List<BE.Decoder> GetProductDown(string strIdSession, string strTransaction, string vCustomerId, string vCoId, int tipoBusqueda, ref int rResultado, ref string rMensaje)
        {
            DbParameter[] parameters = 
            {
                new DbParameter("av_customer_id", DbType.String,255, ParameterDirection.Input, vCustomerId),
                new DbParameter("an_cod_id", DbType.Int32, ParameterDirection.Input, CSTS.Functions.CheckInt(vCoId)),
                new DbParameter("ac_equ_cur", DbType.Object, ParameterDirection.Output),
                new DbParameter("an_resultado", DbType.Int64,255, ParameterDirection.Output),
                new DbParameter("tipoBusqueda", DbType.Int64,255, ParameterDirection.Input, tipoBusqueda),
                new DbParameter("av_mensaje", DbType.String,255, ParameterDirection.Output)
            };

            List<BE.Decoder> listItem = null;
            BE.Decoder item = null;

            try
            {
                DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_SIACSS_EQU_IW_TIP, parameters, (IDataReader reader) =>
                {
                    listItem = new List<BE.Decoder>();

                    while (reader.Read())
                    {
                        item = new BE.Decoder();
                        item.codigo_material = Convert.ToString(reader["codigo_material"]);
                        item.codigo_sap = Convert.ToString(reader["codigo_sap"]);
                        item.numero_serie = Convert.ToString(reader["numero_serie"]);
                        item.macadress = Convert.ToString(reader["macaddress"]);
                        item.descripcion_material = Convert.ToString(reader["descripcion_material"]);
                        item.abrev_material = Convert.ToString(reader["abrev_material"]);
                        item.estado_material = Convert.ToString(reader["estado_material"]);
                        item.precio_almacen = Convert.ToString(reader["precio_almacen"]);
                        item.codigo_cuenta = Convert.ToString(reader["codigo_cuenta"]);
                        item.componente = Convert.ToString(reader["componente"]);
                        item.centro = Convert.ToString(reader["centro"]);
                        item.idalm = Convert.ToString(reader["idalm"]);
                        item.almacen = Convert.ToString(reader["almacen"]);
                        item.tipo_equipo = Convert.ToString(reader["tipo_equipo"]);
                        item.id_producto = Convert.ToString(reader["idproducto"]);
                        item.id_cliente = Convert.ToString(reader["id_cliente"]);
                        item.modelo = Convert.ToString(reader["modelo"]);
                        item.convertertype = Convert.ToString(reader["convertertype"]);
                        item.servicio_principal = Convert.ToString(reader["servicio_principal"]);
                        item.headend = Convert.ToString(reader["headend"]);
                        item.ephomeexchange = Convert.ToString(reader["ephomeexchange"]);
                        item.numero = Convert.ToString(reader["numero"]);
                        item.tipoServicio = Convert.ToString(reader["tiposerv"]);

                        item.TIPODECO = Convert.ToString(reader["TIPODECO"]);
                        item.CARGO_FIJO = Convert.ToString(reader["CARGO_FIJO"]);
                        item.PORCENTAJE_IGV = Convert.ToString(reader["PORCENTAJE_IGV"]);

                        listItem.Add(item);
                    }
                });
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, strTransaction, ex.Message);
            }
            finally
            {
                rResultado = CSTS.Functions.CheckInt(parameters[parameters.Length - 3].Value.ToString());
                rMensaje = CSTS.Functions.CheckStr(parameters[parameters.Length - 2].Value.ToString());
            }

            return listItem;
        }
        public static string GetLoyaltyAmount(string strIdSession, string strTransaction, int iTipo)
        {
            string strMonto = string.Empty;
            Logging.Info("IdSession: " + strIdSession, "Transaccion: " + strTransaction, "Begin a GetLoyaltyAmount Capa Data --> Parametros de entrada iTipo: " + CSTS.Functions.CheckStr(iTipo)); // Temporal
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("v_monto_occ", DbType.String, 20, ParameterDirection.ReturnValue),
                new DbParameter("K_TIPO", DbType.Int32, ParameterDirection.Input, iTipo)
            };

            try
            {
                DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_SGAFUN_OBT_MONTO_OCC, parameters);
                strMonto = Convert.ToString(parameters[0].Value);
                Logging.Info("IdSession: " + strIdSession, "Transaccion: " + strTransaction, "Fin a GetLoyaltyAmount Capa Data --> Parametros de salida strMonto: " + strMonto); // Temporal
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, strTransaction, ex.Message);
            }
            
            return strMonto;
        }
        public static List<Entity.Transac.Service.Fixed.ServiceByPlan> GetServicesByPlan(string strIdSession, string strTransaction, string idplan, string strTipoProducto)
        {
            Logging.Info("LTEGetServicesByPlan", "LTEUnistallInstallationOfDecoder", "Entro GetServicesByPlan");
            Logging.Info("LTEGetServicesByPlan", "LTEUnistallInstallationOfDecoder", "los parametros que recibe el metodo son: strIdSession:" + strIdSession + ";strTransaction:" + strTransaction + ";strTipoProducto:" + strTipoProducto);
            List<Entity.Transac.Service.Fixed.ServiceByPlan> list = null;

            DbParameter[] parameters = new DbParameter[] {                
                new DbParameter("P_PLAN", DbType.String,255, ParameterDirection.Input,idplan),
                new DbParameter("p_prdc",DbType.String,255,ParameterDirection.Input,strTipoProducto),
                new DbParameter("P_CURSOR", DbType.Object, ParameterDirection.Output)                
            };
            try
            {

                list = DbFactory.ExecuteReader<List<Entity.Transac.Service.Fixed.ServiceByPlan>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_PVU, DbCommandConfiguration.SIACU_POST_PVU_SP_CON_PLAN_SERVICIOLTE, parameters);
            }
            catch (Exception ex)
            {
                list = null;
                Logging.Error(strIdSession, strTransaction, SIACU.Transac.Service.Functions.GetExceptionMessage(ex));
            }
            Logging.Info(strIdSession, strTransaction, JsonConvert.SerializeObject(list));
            Logging.Info("LTEGetServicesByPlan", "LTEUnistallInstallationOfDecoder", "Finalizó GetServicesByPlan");
            return list;
        }

        public static DecoMatrizResponse GetDecoMatriz(DecoMatrizRequest objDecoMatrizRequest)
        {
            Logging.Info("LTEGetServicesByPlan", "LTEUnistallInstallationOfDecoder", "Entro GetDecoMatriz");
            Logging.Info("LTEGetServicesByPlan", "LTEUnistallInstallationOfDecoder", "los parametros que recibe el metodo son: strIdSession:");

            DecoMatrizResponse objDecoMatrizResponse = new DecoMatrizResponse();

            DbParameter[] parameters =
            {
                new DbParameter("PO_CANTIDAD",DbType.Decimal,ParameterDirection.Output),
                new DbParameter("PO_MATRIZ",DbType.Object,ParameterDirection.Output),
                new DbParameter("PO_MENSAJE",DbType.String,50,ParameterDirection.Output),
                new DbParameter("PO_COD_ERROR",DbType.Decimal,ParameterDirection.Output),
                new DbParameter("PO_MSG_ERROR",DbType.String,50,ParameterDirection.Output)
                
            };

            try
            {
                List<Entity.Transac.Service.Fixed.DecoMatriz> List = new List<Entity.Transac.Service.Fixed.DecoMatriz>();
                   
                DbFactory.ExecuteReader(objDecoMatrizRequest.Audit.Session, objDecoMatrizRequest.Audit.Transaction,
                    DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SGASS_MATRIZ_DECOS, parameters,
                    (IDataReader reader) =>
                    {
                        

                        while (reader.Read())
                        {
                            Entity.Transac.Service.Fixed.DecoMatriz entity = new Entity.Transac.Service.Fixed.DecoMatriz();
                            entity.Descripcion = Convert.ToString(reader["MODELO"]);
                            entity.Valor = Convert.ToString(reader["PESO"]);
                            List.Add(entity);
                        }
                    });

                objDecoMatrizResponse.CantidadMaxima = parameters[0].Value.ToString();
                objDecoMatrizResponse.ListaMatrizDecos = List;

                
                

            }
            catch (Exception ex)
            {
                Logging.Error(objDecoMatrizRequest.Audit.Session, objDecoMatrizRequest.Audit.Transaction, ex.Message);
            }

            Logging.Info(objDecoMatrizRequest.Audit.Session, objDecoMatrizRequest.Audit.Transaction, "GetInteractIDforCaseID OUT ");

            return objDecoMatrizResponse;




        }

        public static DecoTypeResponse GetDecoType(DecoTypeRequest objDecoTypeRequest)
        {
            bool salida = false;
            Logging.Info("GetDecoType", "LTEUnistallInstallationOfDecoder", "Entro GetTypeDeco");
            Logging.Info("GetDecoType", "LTEUnistallInstallationOfDecoder", "los parametros que recibe el metodo son: strIdSession:" + objDecoTypeRequest.strTipoEquipo);
            
            DbParameter[] parameters =
            {
                new DbParameter("p_tipequ",DbType.Int32,ParameterDirection.Input, Convert.ToInt(objDecoTypeRequest.strTipoEquipo)),
                new DbParameter("p_tipo_deco",DbType.String,50,ParameterDirection.Output),
                new DbParameter("p_cod",DbType.Int32,ParameterDirection.Output),
                new DbParameter("p_mensaje",DbType.String,50,ParameterDirection.Output)
            };

            string mensaje = string.Empty;
            int codresp = -1;
            string tipo = string.Empty;

            DecoTypeResponse objDecoTypeResponse = new DecoTypeResponse();

            try
            {
                Logging.ExecuteMethod(objDecoTypeRequest.Audit.Session, objDecoTypeRequest.Audit.Transaction, () =>
                  {
                      DbFactory.ExecuteNonQuery(objDecoTypeRequest.Audit.Session, objDecoTypeRequest.Audit.Transaction, DbConnectionConfiguration.SIAC_POST_SGA,
                          DbCommandConfiguration.SIACU_P_TIPO_DECO_LTE, parameters);

                  });
                

                mensaje = parameters[3].Value.ToString();
                codresp = Convert.ToInt(parameters[2].Value.ToString());
                tipo = parameters[1].Value.ToString();


                if (codresp == 0)
                {
                    objDecoTypeResponse.TipoDeco = tipo;
                }
                
                objDecoTypeResponse.Mensaje = mensaje;
                

            }
            catch (Exception ex)
            {
                Logging.Error(objDecoTypeRequest.Audit.Session, objDecoTypeRequest.Audit.Transaction, ex.Message);
            }

            Logging.Info(objDecoTypeRequest.Audit.Session, objDecoTypeRequest.Audit.Transaction, "GetDecoType OUT ");

            return objDecoTypeResponse;
            

        }

        public static Entity.Transac.Service.Fixed.PostExecuteDecosLte.DecosLteResponse ExecuteUninstallInstallDecosLte(Entity.Transac.Service.Fixed.PostExecuteDecosLte.DecosLteRequest objRequest)
        {

            var responseFinal = new Entity.Transac.Service.Fixed.PostExecuteDecosLte.DecosLteResponse();

            #region Body Request of Service

            var validarSotPendiente = new validarSotPendienteType
            {
                codId = objRequest.SotPending.StrCoId ?? string.Empty,
                tipTra = objRequest.SotPending.StrTipTra
            };

            var obtenerCliente = new obtenerClienteType
            {
                phone = objRequest.DecoCustomer.Phone ?? string.Empty,
                account = objRequest.DecoCustomer.Account ?? string.Empty,
                contactobjid1 = objRequest.DecoCustomer.ContactObjId ?? string.Empty,
                flagReg = objRequest.DecoCustomer.FlagReg ?? string.Empty

            };

           /* var realizarFidelizacion = new realizarFidelizacionType
            {
                pcodigoPostal = objRequest.RealizarFidelizacion.CodigoPostal ?? string.Empty,
                pcustomer_id = objRequest.RealizarFidelizacion.CustomerId ?? string.Empty,
                pdepartamento = objRequest.RealizarFidelizacion.Departamento ?? string.Empty,
                pdireccionFacturacion = objRequest.RealizarFidelizacion.DireccionFacturacion ?? string.Empty,
                pdistrito = objRequest.RealizarFidelizacion.Distrito ?? string.Empty,
                pfechaReg = objRequest.RealizarFidelizacion.FechaReg ?? string.Empty,
                pflagDirecc_fact = objRequest.RealizarFidelizacion.FlagDireccFact ?? string.Empty,
                pnotasDireccion = objRequest.RealizarFidelizacion.NotasDireccion ?? string.Empty,
                ppais = objRequest.RealizarFidelizacion.Pais ?? string.Empty,
                pprovincia = objRequest.RealizarFidelizacion.Provincia ?? string.Empty,
                pusuarioReg = objRequest.RealizarFidelizacion.UsuarioReg

            };

            var registrarOcc = new registrarOCCType
            {
                paplicacion = objRequest.RealizarOcc.Aplicacion ?? string.Empty ?? string.Empty,
                pcustomerId = objRequest.RealizarOcc.CustomerId ?? string.Empty,
                pfechaAct = objRequest.RealizarOcc.FechaAct ?? string.Empty,
                pfecvig = objRequest.RealizarOcc.Fecvig ?? string.Empty,
                pflagCobroOcc = objRequest.RealizarOcc.FlagCobroOcc ?? string.Empty,
                pmonto = objRequest.RealizarOcc.Monto ?? string.Empty,
                pobservacion = objRequest.RealizarOcc.Observacion,
                pusuarioAct = objRequest.RealizarOcc.UsuarioAct ?? string.Empty
            };*/

            var parametroPrincipal = new parametroPrincipalType
            {
                pnContactobjid1 = objRequest.Interaction.Contactobjid ?? string.Empty,
                pnSiteobjid1 = objRequest.Interaction.Siteobjid ?? string.Empty,
                pvAccount = objRequest.Interaction.Account ?? string.Empty,
                pvPhone = objRequest.Interaction.Phone ?? string.Empty,
                pvTipo = objRequest.Interaction.Tipo ?? string.Empty,
                pvClase = objRequest.Interaction.Clase ?? string.Empty,
                pvSubclase = objRequest.Interaction.Subclase ?? string.Empty,
                pvMetodoContacto = objRequest.Interaction.MetodoContacto ?? string.Empty,
                pvTipoInter = objRequest.Interaction.TipoInter ?? string.Empty,
                pvAgente = objRequest.Interaction.Agente ?? string.Empty,
                pvUsrProceso = objRequest.Interaction.UsrProceso ?? string.Empty,
                pnHechoEnUno = objRequest.Interaction.HechoEnUno ?? string.Empty,
                pvNotas = objRequest.Interaction.Notas ?? string.Empty,
                pvFlagCaso = objRequest.Interaction.FlagCaso ?? string.Empty,
                pvResultado = objRequest.Interaction.Resultado ?? string.Empty

            };

            var parametroPlus = new parametroPlusType
            {
                pnInter1 = objRequest.InsInteractionPlus.Inter1 ?? string.Empty,
                pnInter2 = objRequest.InsInteractionPlus.Inter2 ?? string.Empty,
                pnInter3 = Convert.ToDate(objRequest.InsInteractionPlus.Inter3).ToString("dd/MM/yyyy") ?? string.Empty,
                pnInter4 = objRequest.InsInteractionPlus.Inter4 ?? string.Empty,
                pnInter5 = objRequest.InsInteractionPlus.Inter5 ?? string.Empty,
                pnInter6 = objRequest.InsInteractionPlus.Inter6 ?? string.Empty,
                pnInter7 = objRequest.InsInteractionPlus.Inter7 ?? string.Empty,
                pnInter8 = objRequest.InsInteractionPlus.Inter8 ?? string.Empty,
                pnInter9 = objRequest.InsInteractionPlus.Inter9 ?? string.Empty,
                pnInter10 = objRequest.InsInteractionPlus.Inter10 ?? string.Empty,
                pnInter11 = objRequest.InsInteractionPlus.Inter11 ?? string.Empty,
                pnInter12 = objRequest.InsInteractionPlus.Inter12 ?? string.Empty,
                pnInter13 = objRequest.InsInteractionPlus.Inter13 ?? string.Empty,
                pnInter14 = objRequest.InsInteractionPlus.Inter14 ?? string.Empty,
                pnInter15 = objRequest.InsInteractionPlus.Inter15 ?? string.Empty,
                pnInter16 = objRequest.InsInteractionPlus.Inter16 ?? string.Empty,
                pnInter17 = objRequest.InsInteractionPlus.Inter17 ?? string.Empty,
                pnInter18 = objRequest.InsInteractionPlus.Inter18 ?? string.Empty,
                pnInter19 = objRequest.InsInteractionPlus.Inter19 ?? string.Empty,
                pnInter20 = objRequest.InsInteractionPlus.Inter20 ?? string.Empty,
                pnInter21 = objRequest.InsInteractionPlus.Inter21 ?? string.Empty,
                pnInter22 = objRequest.InsInteractionPlus.Inter22 ?? string.Empty,
                pnInter23 = objRequest.InsInteractionPlus.Inter23 ?? string.Empty,
                pnInter24 = objRequest.InsInteractionPlus.Inter24 ?? string.Empty,
                pnInter25 = objRequest.InsInteractionPlus.Inter25 ?? string.Empty,
                pnInter26 = objRequest.InsInteractionPlus.Inter26 ?? string.Empty,
                pnInter27 = objRequest.InsInteractionPlus.Inter27 ?? string.Empty,
                pnInter28 = objRequest.InsInteractionPlus.Inter28 ?? string.Empty,
                pnInter29 = objRequest.InsInteractionPlus.Inter29 ?? string.Empty,
                pnInter30 = objRequest.InsInteractionPlus.Inter30 ?? string.Empty,
                pnPlusInter2interact = objRequest.InsInteractionPlus.PlusInter2Interact ?? string.Empty,
                pnAdjustmentAmount = objRequest.InsInteractionPlus.AdjustmentAmount ?? string.Empty,
                pvAdjustmentReason = objRequest.InsInteractionPlus.AdjustmentReason ?? string.Empty,
                pvAddress = objRequest.InsInteractionPlus.Address ?? string.Empty,
                pvAmountUnit = objRequest.InsInteractionPlus.AmountUnit ?? string.Empty,
                pdBirthday = DateTime.UtcNow.ToString("dd/MM/yyyy"),
                pvClarifyInteraction = objRequest.InsInteractionPlus.ClarifyInteraction ?? string.Empty,
                pvClaroLdn1 = objRequest.InsInteractionPlus.ClaroLdn1 ?? string.Empty,
                pvClaroLdn2 = objRequest.InsInteractionPlus.ClaroLdn2 ?? string.Empty,
                pvClaroLdn3 = objRequest.InsInteractionPlus.ClaroLdn3 ?? string.Empty,
                pvClaroLdn4 = objRequest.InsInteractionPlus.ClaroLdn4 ?? string.Empty,
                pvClarolocal1 = objRequest.InsInteractionPlus.ClaroLocal1 ?? string.Empty,
                pvClarolocal2 = objRequest.InsInteractionPlus.ClaroLocal2 ?? string.Empty,
                pvClarolocal3 = objRequest.InsInteractionPlus.ClaroLocal3 ?? string.Empty,
                pvClarolocal4 = objRequest.InsInteractionPlus.ClaroLocal4 ?? string.Empty,
                pvClarolocal5 = objRequest.InsInteractionPlus.ClaroLocal5 ?? string.Empty,
                pvClarolocal6 = objRequest.InsInteractionPlus.ClaroLocal6 ?? string.Empty,
                pvContactPhone = objRequest.InsInteractionPlus.ContactPhone ?? string.Empty,
                pvDniLegalRep = objRequest.InsInteractionPlus.DniLegalRep ?? string.Empty,
                pvDocumentNumber = objRequest.InsInteractionPlus.DocumentNumber ?? string.Empty,
                pvEmail = objRequest.InsInteractionPlus.Email ?? string.Empty,
                pvFirstName = objRequest.InsInteractionPlus.FirstName ?? string.Empty,
                pvFixedNumber = objRequest.InsInteractionPlus.FixedNumber ?? string.Empty,
                pvFlagChangeUser = objRequest.InsInteractionPlus.FlagChangeUser ?? string.Empty,
                pvFlagLegalRep = objRequest.InsInteractionPlus.FlagLegalRep ?? string.Empty,
                pvFlagOther = objRequest.InsInteractionPlus.FlagOther ?? string.Empty,
                pvFlagTitular = objRequest.InsInteractionPlus.FlagTitular ?? string.Empty,
                pvImei = objRequest.InsInteractionPlus.Imei ?? string.Empty,
                pvLastName = objRequest.InsInteractionPlus.LastName ?? string.Empty,
                pvLastnameRep = objRequest.InsInteractionPlus.LastNameRep ?? string.Empty,
                pvOldClaroLdn1 = objRequest.InsInteractionPlus.OldClaroLdn1 ?? string.Empty,
                pvOldClaroLdn2 = objRequest.InsInteractionPlus.OldClaroLdn2 ?? string.Empty,
                pvOldClaroLdn3 = objRequest.InsInteractionPlus.OldClaroLdn3 ?? string.Empty,
                pvOldClaroLdn4 = objRequest.InsInteractionPlus.OldClaroLdn4 ?? string.Empty,
                pvOldClarolocal1 = objRequest.InsInteractionPlus.OldClaroLocal1 ?? string.Empty,
                pvOldClarolocal2 = objRequest.InsInteractionPlus.OldClaroLocal2 ?? string.Empty,
                pvOldClarolocal3 = objRequest.InsInteractionPlus.OldClaroLocal3 ?? string.Empty,
                pvOldClarolocal4 = objRequest.InsInteractionPlus.OldClaroLocal4 ?? string.Empty,
                pvOldClarolocal5 = objRequest.InsInteractionPlus.OldClaroLocal5 ?? string.Empty,
                pvOldClarolocal6 = objRequest.InsInteractionPlus.OldClaroLocal6 ?? string.Empty,
                pvOldDocNumber = objRequest.InsInteractionPlus.OldDocNumber ?? string.Empty,
                pvOldFirstName = objRequest.InsInteractionPlus.OldFirstName ?? string.Empty,
                pvOldFixedPhone = objRequest.InsInteractionPlus.OldFixedPhone ?? string.Empty,
                pvOldLastName = objRequest.InsInteractionPlus.OldLastName ?? string.Empty,
                pvOldLdiNumber = objRequest.InsInteractionPlus.OldLdiNumber ?? string.Empty,
                pvOldFixedNumber = objRequest.InsInteractionPlus.OldFixedNumber ?? string.Empty,
                pvOperationType = objRequest.InsInteractionPlus.OperationType ?? string.Empty,
                pvOtherDocNumber = objRequest.InsInteractionPlus.OtherDocNumber ?? string.Empty,
                pvOtherFirstName = objRequest.InsInteractionPlus.OtherFirstName ?? string.Empty,
                pvOtherLastName = objRequest.InsInteractionPlus.OtherLastName ?? string.Empty,
                pvOtherPhone = objRequest.InsInteractionPlus.OtherPhone ?? string.Empty,
                pvPhoneLegalRep = objRequest.InsInteractionPlus.PhoneLegalRep ?? string.Empty,
                pvReferencePhone = objRequest.InsInteractionPlus.ReferencePhone ?? string.Empty,
                pvReason = objRequest.InsInteractionPlus.Reason ?? string.Empty,
                pvModel = objRequest.InsInteractionPlus.Model ?? string.Empty,
                pvLotCode = objRequest.InsInteractionPlus.LotCode ?? string.Empty,
                pvFlagRegistered = objRequest.InsInteractionPlus.FlagRegistered ?? string.Empty,
                pvRegistrationReason = objRequest.InsInteractionPlus.RegistrationReason ?? string.Empty,
                pvClaroNumber = objRequest.InsInteractionPlus.ClaroNumber ?? string.Empty,
                pvMonth = objRequest.InsInteractionPlus.Month ?? string.Empty,
                pvOstNumber = objRequest.InsInteractionPlus.OstNumber ?? string.Empty,
                pvBasket = objRequest.InsInteractionPlus.Basket ?? string.Empty,
                pdExpireDate = DateTime.UtcNow.ToString("dd/MM/yyyy"),
                pvAddress5 = objRequest.InsInteractionPlus.Address5 ?? string.Empty,
                pnChargeAmount = objRequest.InsInteractionPlus.ChargeAmount ?? string.Empty,
                pvCity = objRequest.InsInteractionPlus.City ?? string.Empty,
                pvContactSex = objRequest.InsInteractionPlus.ContactSex ?? string.Empty,
                pvDepartment = objRequest.InsInteractionPlus.Department ?? string.Empty,
                pvDistrict = objRequest.InsInteractionPlus.District ?? string.Empty,
                pvEmailConfirmation = objRequest.InsInteractionPlus.EmailConfirmation ?? string.Empty,
                pvFax = objRequest.InsInteractionPlus.Fax ?? string.Empty,
                pvFlagCharge = objRequest.InsInteractionPlus.FlagCharge ?? string.Empty,
                pvMaritalStatus = objRequest.InsInteractionPlus.MaritalStatus ?? string.Empty,
                pvOccupation = objRequest.InsInteractionPlus.Occupation ?? string.Empty,
                pvPosition = objRequest.InsInteractionPlus.Position ?? string.Empty,
                pvReferenceAddress = objRequest.InsInteractionPlus.ReferenceAddress ?? string.Empty,
                pvTypeDocument = objRequest.InsInteractionPlus.TypeDocument ?? string.Empty,
                pvZipcode = objRequest.InsInteractionPlus.ZipCode ?? string.Empty,
                pvIccid = objRequest.InsInteractionPlus.Iccid ?? string.Empty,
                pvLdiNumber = objRequest.InsInteractionPlus.LdiNumber ?? string.Empty,
                pvNameLegalRep = objRequest.InsInteractionPlus.NameLegalRep ?? string.Empty

            };

            var generarConstancia = new generarConstanciaType
            {
                directory = objRequest.GenerateConstancy.Directory,
                driver = objRequest.GenerateConstancy.Driver,
                fileName = objRequest.GenerateConstancy.FileName
            };

            var registroAuditoria = new registroAuditoriaType
            {
                cuentaUsuario = objRequest.AuditRegister.strCuentaUsuario ?? string.Empty,
                ipCliente = objRequest.AuditRegister.strIpCliente ?? string.Empty,
                ipServidor = objRequest.AuditRegister.strIpServidor ?? string.Empty,
                nombreCliente = objRequest.AuditRegister.strNombreCliente ?? string.Empty,
                nombreServidor = objRequest.AuditRegister.strNombreServidor ?? string.Empty,
                telefono = objRequest.AuditRegister.strTelefono ?? string.Empty,
                texto = objRequest.AuditRegister.strTexto ?? string.Empty
            };

            var registrarProcesoPostventa = new registrarProcesoPostventaType
            {
                piTramaCab = objRequest.RegistrarProcesoPostVenta.PiTramaCab ?? string.Empty,
                //piCargo = objRequest.RegistrarProcesoPostVenta.PiCargo ?? string.Empty,
                //piCodCaso = objRequest.RegistrarProcesoPostVenta.PiCodCaso ?? string.Empty,
                piCodId = objRequest.RegistrarProcesoPostVenta.PiCodId ?? string.Empty,
                //piCodIntercaso = objRequest.RegistrarProcesoPostVenta.PiCodIntercaso ?? string.Empty,
                //piCodedif = objRequest.RegistrarProcesoPostVenta.PiCodedif ?? string.Empty,
                //piCodmotot = objRequest.RegistrarProcesoPostVenta.PiCodmotot ?? string.Empty,
                piCodplano = objRequest.RegistrarProcesoPostVenta.PiCodplano ?? string.Empty,
                //piCodzona = objRequest.RegistrarProcesoPostVenta.PiCodzona ?? string.Empty,
                piCustomerId = objRequest.RegistrarProcesoPostVenta.PiCustomerId ?? string.Empty,
                piFecProg = objRequest.RegistrarProcesoPostVenta.PiFecProg ?? string.Empty,
                piFecreg = objRequest.RegistrarProcesoPostVenta.PiFecreg ?? string.Empty,
                //piFlagActDirFact = objRequest.RegistrarProcesoPostVenta.PiFlagActDirFact ?? string.Empty,
                //piFranjaHor = objRequest.RegistrarProcesoPostVenta.PiFranjaHor ?? string.Empty,
                //piLote = objRequest.RegistrarProcesoPostVenta.PiLote ?? string.Empty,
                piLstCoser = objRequest.RegistrarProcesoPostVenta.PiLstCoser ?? string.Empty,
                piLstSncode = objRequest.RegistrarProcesoPostVenta.PiLstSncode ?? string.Empty,
                piLstSpcode = objRequest.RegistrarProcesoPostVenta.PiLstSpcode ?? string.Empty,
                piLstTipequ = objRequest.RegistrarProcesoPostVenta.PiLstTipequ ?? string.Empty,
                //piManzana = objRequest.RegistrarProcesoPostVenta.PiManzana ?? string.Empty,
                //piNomVia = objRequest.RegistrarProcesoPostVenta.PiNomVia ?? string.Empty,
                //piNomurb = objRequest.RegistrarProcesoPostVenta.PiNomurb ?? string.Empty,
                //piNumCarta = objRequest.RegistrarProcesoPostVenta.PiNumCarta ?? string.Empty,
                //piNumVia = objRequest.RegistrarProcesoPostVenta.PiNumVia ?? string.Empty,
                //piObservacion = objRequest.RegistrarProcesoPostVenta.PiObservacion ?? string.Empty,
                //piOperador = objRequest.RegistrarProcesoPostVenta.PiOperador ?? string.Empty,
                //piPresuscrito = objRequest.RegistrarProcesoPostVenta.PiPresuscrito ?? string.Empty,
                //piPublicar = objRequest.RegistrarProcesoPostVenta.PiPublicar ?? string.Empty,
                //piReferencia = objRequest.RegistrarProcesoPostVenta.PiReferencia ?? string.Empty,
                //piTipUrb = objRequest.RegistrarProcesoPostVenta.PiTipUrb ?? string.Empty,
                //piTipoProducto = objRequest.RegistrarProcesoPostVenta.PiTipoProducto ?? string.Empty,
                //piTipoTrans = objRequest.RegistrarProcesoPostVenta.PiTipoTrans ?? string.Empty,
                //piTipoVia = objRequest.RegistrarProcesoPostVenta.PiTipoVia ?? string.Empty,
                //piTiposervicio = objRequest.RegistrarProcesoPostVenta.PiTiposervicio ?? string.Empty,
                //piTiptra = objRequest.RegistrarProcesoPostVenta.PiTiptra ?? string.Empty,
                //piTmcode = objRequest.RegistrarProcesoPostVenta.PiTmcode ?? string.Empty,
                //piUbigeo = objRequest.RegistrarProcesoPostVenta.PiUbigeo ?? string.Empty,
                //piUsureg = objRequest.RegistrarProcesoPostVenta.PiUsureg ?? string.Empty
            };

            var listaDetalleDecoRequest = new List<ListaDetalleDecoRequestType>();

            foreach (var item in objRequest.LstDecoders)
            {
                var objLst = new ListaDetalleDecoRequestType
                {
                    associated = item.Associated ?? string.Empty,
                    cf = item.Cf ?? string.Empty,
                    codInssrv = item.CodeInsSrv ?? string.Empty,
                    codUser = item.CodeUser ?? string.Empty,
                    codeService = item.CodeService ?? string.Empty,
                    dateUser = item.DateUser ?? string.Empty,
                    equipment = item.Equipment ?? string.Empty,
                    flag = item.Flag ?? string.Empty,
                    serialNumber = item.SerialNumber ?? string.Empty,
                    quantity = item.Quantity ?? string.Empty,
                    serviceGroup = item.ServiceGroup ?? string.Empty,
                    serviceName = item.ServiceName ?? string.Empty,
                    serviceType = item.ServiceType ?? string.Empty,
                    snCode = item.SnCode ?? string.Empty,
                    spCode = item.SpCode ?? string.Empty,
                    typeEquipmentCode = item.TypeEquipmentCode ?? string.Empty
                };

                listaDetalleDecoRequest.Add(objLst);

            }

            listaDetalleDecoRequest = listaDetalleDecoRequest.OrderByDescending(x => x.flag).ToList();

            var objEtaSelection = new registrarEtaSeleccionType
            {
                idBucket = objRequest.RegistrarEtaSeleccion.Idbucket,
                franja = objRequest.RegistrarEtaSeleccion.Franja,
                fechaCompromiso = objRequest.RegistrarEtaSeleccion.FechaCompromiso,
                idconsulta = objRequest.RegistrarEtaSeleccion.IdConsulta,
                idInteraccion = objRequest.RegistrarEtaSeleccion.IdInteraccion
            };

            var objRegistrarEta = new registrarEtaType()
            {
                inIdbucket = objRequest.RegistrarEta.Idbucket,
               // inIpcreacion = objRequest.RegistrarEta.IpCreacion,
                inIdPoblado = objRequest.RegistrarEta.IdPoblado,
               // inDniTecnico = objRequest.RegistrarEta.DniTecnico,
                inFranja = objRequest.RegistrarEta.Franja,
                inSubTipoOrden = objRequest.RegistrarEta.SubTipoOrden,
                inUsrcrea = objRequest.RegistrarEta.UsrCrea
            };


            #endregion

            var objRequestHeader = new HeaderRequest
            {
                channel = string.Empty,
                startDate = DateTime.UtcNow,
                userSession = objRequest.Interaction.Agente,
                idApplication = "915",
                idBusinessTransaction = objRequest.StrIdSession,
                idESBTransaction = objRequest.Audit.Transaction,
                additionalNode = string.Empty,
                userApplication = objRequest.AuditRegister.strCuentaUsuario,
                startDateSpecified = true
            };


            var objRequestBody = new instalarDesinstalarDecoAdicionalRequest
            {
                obtenerCliente = obtenerCliente,
                parametroPlus = parametroPlus,
                validarSotPendiente = validarSotPendiente,
                parametroPrincipal = parametroPrincipal,
                flagContingInteracion = objRequest.FlagConting,
                listaDetalleDeco = listaDetalleDecoRequest.ToArray(),
                registrarProcesoPostventa = registrarProcesoPostventa,
                registroAuditoria = registroAuditoria,
                generarConstancia = generarConstancia,
                hdnValidaEta = objRequest.EtaValidation ?? string.Empty,
                registrarEtaSeleccion = objEtaSelection,
                registrarEta = objRegistrarEta
            };

            var objResponseBody = new instalarDesinstalarDecoAdicionalResponse();

            try
            {

                var objResponseHeader = Logging.ExecuteMethod(string.Empty, string.Empty,
                    ServiceConfiguration.FixedUninstallinstallDecosLte, () =>
                    {
                        return ServiceConfiguration.FixedUninstallinstallDecosLte.instalarDesinstalarDecoAdicional(objRequestHeader, objRequestBody, out objResponseBody);
                    });

                Logging.Info(string.Empty, string.Empty, "DECOSLTE SUCCESS RESPONSE HEADER: " + objResponseBody.responseStatus + "-" + objResponseBody.responseStatus.codeResponse + "-" + objResponseBody.responseStatus.descriptionResponse);
            }
            catch (Exception ex)
            {
                Logging.Error(string.Empty, string.Empty, CSTS.Functions.GetExceptionMessage(ex));
            }

            if (objResponseBody != null)
            {
                responseFinal.ResponseCode = objResponseBody.responseStatus.codeResponse;
                responseFinal.SotNumber = string.Empty;
                responseFinal.CodeInteraction = string.Empty;
                responseFinal.UrlConstancy = string.Empty;
                responseFinal.ResponseMessage = string.Empty;

                if (objResponseBody.responseData != null)
                {
                    if (responseFinal.ResponseCode.Equals(CSTS.Constants.strUno))
                    {
                        responseFinal.SotNumber = objResponseBody.responseData.sotPendiente;
                        responseFinal.ResponseMessage = "Existen SOT pendientes que tiene el cliente, para continuar primero estas deben ser atendidas.";
                    }
                    else
                    {
                        responseFinal.SotNumber = objResponseBody.responseData.sotGenerada;
                        responseFinal.CodeInteraction = objResponseBody.responseData.idTransaccion;
                        responseFinal.UrlConstancy = CSTS.Functions.GetConstancyUrlFromServerToApp(objResponseBody.responseData.constancia,
                            objRequest.GenerateConstancy.ServerReadPdf,
                            objRequest.GenerateConstancy.Directory);
                        responseFinal.ResponseMessage = objResponseBody.responseStatus.descriptionResponse;
                    }

                    Logging.Info(string.Empty, string.Empty, "DECOSLTE SUCCESS RESPONSE BODY - " + "INTERACCION: " + objResponseBody.responseData.idTransaccion + " - SOT: " + objResponseBody.responseData.sotGenerada + "URLCONSTANCIA: " + objResponseBody.responseData.constancia);

            
                }
                else if (objResponseBody.responseStatus != null)
                {
                    var objStatus = objResponseBody.responseStatus;
                    if (objStatus.codeResponse.Equals(CSTS.Constants.strUno))
                    {
                        responseFinal.ResponseMessage = "No se están enviando correctamente los datos para la correcta ejecución de la transacción.(DB-" + objRequestHeader.idBusinessTransaction + ")";
                    }
                    else if (objStatus.codeResponse.Equals(CSTS.Constants.strDos))
                    {
                        responseFinal.ResponseMessage = "No se están enviando correctamente los datos para la correcta ejecución de la transacción.(WS-" + objRequestHeader.idBusinessTransaction + ")";
                    }
                    else if (objStatus.codeResponse.Equals(CSTS.Constants.strMenosUno))
                    {
                        responseFinal.ResponseMessage = "Se supero el tiempo de espera la ejecución de la transacción.";
                    }
                    else if (objStatus.codeResponse.Equals("-2"))
                    {
                        responseFinal.ResponseMessage = "Existen problemas de disponibilidad para la ejecución de la transacción.";
                    }
                    else if (objStatus.codeResponse.Equals("-3"))
                    {
                        responseFinal.ResponseMessage = "Ocurrió un problema interno en la aplicación, vuelva a intentarlo.";
                    }
                    else
                    {
                        responseFinal.ResponseMessage = "Ocurrió un problema interno no controlado, consulte con el administrador.";
                    }
                }
            }        

            return responseFinal;
        }

        public static string GetLoyaltyAmountLte(string strIdSession, string strTransaction, int intTipo)
        {
            var strMonto = string.Empty;
            Logging.Info("IdSession: " + strIdSession, "Transaccion: " + strTransaction, "Begin a GetLoyaltyAmountLte Capa Data --> Parametros de entrada intTipo: " + CSTS.Functions.CheckStr(intTipo));
            DbParameter[] parameters = {
                new DbParameter("v_monto_occ", DbType.String, 20, ParameterDirection.ReturnValue),
                new DbParameter("K_TIPO", DbType.Int32, ParameterDirection.Input, intTipo)
            };

            try
            {
                DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_IDD_SGA_GET_MONTO_OCC_LTE, parameters);
                strMonto = Convert.ToString(parameters[0].Value);
                Logging.Info("IdSession: " + strIdSession, "Transaccion: " + strTransaction, "Fin a GetLoyaltyAmountLte Capa Data --> Parametros de salida strMonto: " + strMonto);
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, strTransaction, CSTS.Functions.GetExceptionMessage(ex));
            }

            return strMonto;
        }
    }
}