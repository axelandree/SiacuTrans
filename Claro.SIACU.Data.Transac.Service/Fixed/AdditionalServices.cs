using System;
using System.Collections.Generic;
using System.Data;
using Claro.Data;
using Claro.SIACU.Data.Transac.Service.Configuration;
using Claro.SIACU.Transac.Service;
using EntitiesFixed = Claro.SIACU.Entity.Transac.Service.Fixed;
using ConstantsHFC = Claro.SIACU.Transac.Service.Constants;
using ActDesactLTE = Claro.SIACU.ProxyService.Transac.Service.SIACFixed.ServAddLTE;
using ActDesactHFC = Claro.SIACU.ProxyService.Transac.Service.SIACFixed.ServAddHFC;
using KEY = Claro.ConfigurationManager;
namespace Claro.SIACU.Data.Transac.Service.Fixed
{
    public class AdditionalServices
    {
        //Name SIAC HFC: ListarServiciosComerciales
        public static List<EntitiesFixed.CommercialService> GetCommercialServices(string strIdSession, string strTransaction, string strCoId)
        {
            DbParameter[] parameters = 
            {
                new DbParameter("p_co_id", DbType.Int64, ParameterDirection.Input, strCoId),
                new DbParameter("p_tmdes", DbType.String, 255, ParameterDirection.Output),
                new DbParameter("p_tmcode", DbType.Int64, 255, ParameterDirection.Output),
                new DbParameter("p_cursor", DbType.Object, ParameterDirection.Output),
                new DbParameter("v_errnum", DbType.Int64, 255,ParameterDirection.Output),
                new DbParameter("v_errmsj", DbType.String, 255, ParameterDirection.Output)               
            };

            var salida = new List<EntitiesFixed.CommercialService>();
            try
            {
                Web.Logging.ExecuteMethod(strIdSession, strTransaction, () =>
                {
                    DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS,
                        DbCommandConfiguration.SIACU_POST_BSCS_SP_LISTAR_SERVICIOS_TELEFONO, parameters, (IDataReader reader) =>
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
                                    SPCODE = reader["SPCODE"].ToString(),
                                    COSTOPVU = ConstantsHFC.PresentationLayer.gstrNoInfo
                                };
                                salida.Add(item);
                            }
                        });
                });

            }
            catch (Exception ex)
            {
                salida.Clear();
                var item = new EntitiesFixed.CommercialService { DE_SER = "Error" };
                salida.Add(item);
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
                throw;
            }

            return salida;
        }

        //Name SIAC HFC: ObtenerPlanComercial
        public static bool GetCommertialPlan(string strIdSession, string strTransaction, string strCoId, ref string rCodigoPlan, ref int rintCodigoError, ref string rstrDescripcionError)
        {
            DbParameter[] parameters = 
            {
                new DbParameter("P_CO_ID", DbType.String, ParameterDirection.Input, strCoId),	
                new DbParameter("P_COD_PLAN", DbType.String, 255, ParameterDirection.Output),
                new DbParameter("P_RESULTADO", DbType.String, 255, ParameterDirection.Output),	
                new DbParameter("P_MSGERR", DbType.String, 255, ParameterDirection.Output)
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
                rintCodigoError = Convert.ToInt(parameters[parameters.Length - 2].Value.ToString());
                rstrDescripcionError = parameters[parameters.Length - 1].Value.ToString();
            }

            return salida;
        }

        //Name: SIAC HFC: ObtenerIDProductoTRAcDesacServ
        public static string GetProductTracDeacServ(string strIdSession, string strTransaction, string vstrIdentificador, string vstrCoId, bool vod, bool match)
        {

            DbParameter[] parameters = 
            {   
                new DbParameter("P_CO_ID", DbType.Int64,22,ParameterDirection.Input, vstrCoId), 
                new DbParameter("P_CURSOR_CTV", DbType.Object,ParameterDirection.Output),
                new DbParameter("P_CURSOR_INT", DbType.Object,ParameterDirection.Output),
                new DbParameter("P_CURSOR_TLF", DbType.Object,ParameterDirection.Output),
                new DbParameter("P_RESULTADO", DbType.Int64, 255, ParameterDirection.Output),
                new DbParameter("P_MSGERR", DbType.String, 255,ParameterDirection.Output)
            };

            var lstObj = new List<EntitiesFixed.GenericItem>();
            var canti1 = 0;
            var idproductomayor = string.Empty;
            try
            {
                Web.Logging.ExecuteMethod(strIdSession, strTransaction, () =>
                {
                    DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_POST_BSCS_SP_OBTENER_DATOS_NF_HFC, parameters, reader =>
                    {
                        while (reader.Read())
                        {
                            var obj = new EntitiesFixed.GenericItem
                            {
                                Descripcion = reader["VALOR"].ToString(),
                                Descripcion2 = reader["ID_PRODUCTO"].ToString(),
                                Agrupador = reader["TIPO_SERV"].ToString(),
                                Estado = reader["ESTADO"].ToString(),
                                Condicion = reader["SERIAL_NUMBER"].ToString()
                            };

                            lstObj.Add(obj);
                            canti1++;
                        }
                    });
                });

                double prodm = 0;
                string macsadres = string.Empty;
                string idproductoequipo = string.Empty;
                int cantidadprod = 0;
                for (int i = 0; i < lstObj.Count; i++)
                {
                    if (match)
                    {
                        if (vod)
                        {
                            if (lstObj[i].Agrupador.Equals("VOD") && lstObj[i].Estado.Equals("A"))
                            {
                                if (idproductomayor.Equals(String.Empty))
                                {
                                    idproductomayor = lstObj[i].Descripcion2;

                                }
                                else
                                {
                                    idproductomayor = idproductomayor + "|" + lstObj[i].Descripcion2;
                                }
                            }
                        }
                        else
                        {
                            if (lstObj[i].Descripcion == vstrIdentificador && lstObj[i].Estado.Equals("A") && lstObj[i].Agrupador.Equals("CANAL"))
                            {

                                if (idproductomayor.Equals(String.Empty))
                                {
                                    idproductomayor = lstObj[i].Descripcion2;
                                }
                                else
                                {
                                    idproductomayor = idproductomayor + "|" + lstObj[i].Descripcion2;
                                }
                            }
                        }
                    }
                    else
                    {

                        if (macsadres.IndexOf(lstObj[i].Condicion) == -1 && lstObj[i].Estado.Equals("A") && lstObj[i].Agrupador.Equals("CABLE_TV"))
                        {
                            cantidadprod = cantidadprod + 1;
                            macsadres = macsadres + ";" + lstObj[i].Condicion;
                        }
                        if (lstObj[i].Agrupador.Equals("CABLE_TV"))
                        {
                            idproductoequipo = lstObj[i].Descripcion2;
                        }

                        if (prodm < double.Parse(lstObj[i].Descripcion2) && (lstObj[i].Agrupador.Equals("CANAL") || lstObj[i].Agrupador.Equals("VOD") || lstObj[i].Agrupador.Equals("VOD")))
                        {
                            idproductomayor = lstObj[i].Descripcion2;
                            prodm = double.Parse(lstObj[i].Descripcion2);
                        }

                    }
                }

                if (match.Equals(false))
                {
                    string aux1 = idproductomayor;
                    var logicaSinServicios = aux1.Equals(string.Empty) && !idproductoequipo.Equals(string.Empty);
                    idproductomayor = string.Empty;
                    for (int i = 0; i < cantidadprod; i++)
                    {
                        if (!logicaSinServicios)
                        {
                            if (i.Equals(0))
                            {
                                idproductomayor = (Int64.Parse(aux1) + 1).ToString();
                            }
                            else
                            {
                                idproductomayor = idproductomayor + "|" + (Int64.Parse(aux1) + 1);
                            }
                            aux1 = (Int64.Parse(aux1) + 1).ToString();
                        }
                        else
                        {
                            if (i.Equals(0))
                            {
                                idproductomayor = idproductoequipo + ConstantsHFC.strUno;
                            }
                            else
                            {
                                idproductomayor = idproductomayor + "|" + idproductoequipo + ConstantsHFC.strUno;
                            }
                            idproductoequipo = idproductoequipo + ConstantsHFC.strUno;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
            }
            finally
            {

            }
            return idproductomayor;
        }

        //Name: SIAC HFC: ObtenerListaCampanas
        public static List<EntitiesFixed.GenericItem> GetCamapaign(string strIdSession, string strTransaction, string coid, string sncode)
        {
            List<EntitiesFixed.GenericItem> salida = new List<EntitiesFixed.GenericItem>();
            DbParameter[] parameters = {   
                new DbParameter("P_CO_ID", DbType.String, ParameterDirection.Input, coid),
                new DbParameter("P_SNCODE", DbType.String, ParameterDirection.Input, sncode),
                new DbParameter("P_CURSOR",DbType.Object, ParameterDirection.Output),
                new DbParameter("P_RESULTADO",DbType.Int32, 255, ParameterDirection.Output),
                new DbParameter("P_MSGERR",DbType.String, 255, ParameterDirection.Output)
            };
            int i = 0;
            try
            {
                Web.Logging.ExecuteMethod(strIdSession, strTransaction, () =>
                {
                    DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS,
                        DbCommandConfiguration.SIACU_POST_BSCS_SP_LISTAR_PROMOCIONES, parameters, reader =>
                        {
                            while (reader.Read())
                            {
                                var item = new EntitiesFixed.GenericItem
                                {
                                    Codigo = reader["CPCODE"].ToString(),
                                    Descripcion = reader["PROMOCION/MOTIVO"].ToString()
                                };
                                i++;
                                salida.Add(item);
                            }
                        });
                });

            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
            }

            return salida;
        }

        //Name: SIAC HFC: Insertar DAInteracción
        public static bool GetInsertInteractHFC(string strIdSession, string strTransaction, EntitiesFixed.Interaction item,
            ref string rInteraccionId,
            ref string rFlagInsercion,
            ref string rMsgText)
        {
            DbParameter[] parameters = 
            {
                new DbParameter("P_CONTACTOBJID_1", DbType.Int64,ParameterDirection.Input),
                new DbParameter("P_SITEOBJID_1", DbType.Int64,ParameterDirection.Input),
                new DbParameter("P_ACCOUNT", DbType.String,255,ParameterDirection.Input),
                new DbParameter("P_PHONE", DbType.String,255,ParameterDirection.Input),
                new DbParameter("P_TIPO", DbType.String,255,ParameterDirection.Input),
                new DbParameter("P_CLASE", DbType.String,255,ParameterDirection.Input),
                new DbParameter("P_SUBCLASE", DbType.String,255,ParameterDirection.Input),
                new DbParameter("P_METODO_CONTACTO", DbType.String,255,ParameterDirection.Input),
                new DbParameter("P_TIPO_INTER", DbType.String,255,ParameterDirection.Input),
                new DbParameter("P_AGENTE", DbType.String,255,ParameterDirection.Input),
                new DbParameter("P_USR_PROCESO", DbType.String,255,ParameterDirection.Input),
                new DbParameter("P_HECHO_EN_UNO", DbType.String,255,ParameterDirection.Input),
                new DbParameter("P_NOTAS", DbType.String,4000,ParameterDirection.Input),
                new DbParameter("P_SERVAFECT", DbType.String,255,ParameterDirection.Input),
                new DbParameter("P_INCONVEN", DbType.String,255,ParameterDirection.Input),
                new DbParameter("P_SERVAFECT_CODE", DbType.String,255,ParameterDirection.Input),
                new DbParameter("P_INCONVEN_CODE", DbType.String,255,ParameterDirection.Input),
                new DbParameter("P_CO_ID", DbType.String,255,ParameterDirection.Input),
                new DbParameter("P_COD_PLANO", DbType.String,255,ParameterDirection.Input),
                new DbParameter("P_VALOR1", DbType.String,255,ParameterDirection.Input),
                new DbParameter("P_VALOR2", DbType.String,255,ParameterDirection.Input),
                new DbParameter("P_FLAG_CASO", DbType.String,255,ParameterDirection.Input),
                new DbParameter("P_RESULTADO", DbType.String,255,ParameterDirection.Input),
                new DbParameter("ID_INTERACCION", DbType.String,255,ParameterDirection.Output),				
                new DbParameter("FLAG_CREACION", DbType.String,255,ParameterDirection.Output),
                new DbParameter("MSG_TEXT", DbType.String,255,ParameterDirection.Output)			
            };

            for (int j = 0; j < parameters.Length; j++)
                parameters[j].Value = System.DBNull.Value;

            int i = 0;
            if (item.OBJID_CONTACTO != null)
                parameters[i].Value = Convert.ToInt64(item.OBJID_CONTACTO);

            i++;
            if (item.OBJID_SITE != null)
                parameters[i].Value = Convert.ToInt64(item.OBJID_SITE);

            i++;
            if (item.CUENTA != null)
                parameters[i].Value = item.CUENTA;

            i++;
            if (item.TELEFONO != null)
                parameters[i].Value = item.TELEFONO;
            i++;
            if (item.TIPO != null)
                parameters[i].Value = item.TIPO;

            i++;
            if (item.CLASE != null)
                parameters[i].Value = item.CLASE;

            i++;
            if (item.SUBCLASE != null)
                parameters[i].Value = item.SUBCLASE;

            i++;
            if (item.METODO != null)
                parameters[i].Value = item.METODO;

            i++;
            if (item.TIPO_INTER != null)
                parameters[i].Value = item.TIPO_INTER;

            i++;
            if (item.AGENTE != null)
                parameters[i].Value = item.AGENTE;

            i++;
            if (item.USUARIO_PROCESO != null)
                parameters[i].Value = item.USUARIO_PROCESO;

            i++;
            if (item.HECHO_EN_UNO != null)
                parameters[i].Value = item.HECHO_EN_UNO;

            i++;
            if (item.NOTAS != null)
                parameters[i].Value = item.NOTAS;

            i++;
            if (item.SERVICIO != null)
                parameters[i].Value = item.SERVICIO;

            i++;
            if (item.INCONVENIENTE != null)
                parameters[i].Value = item.INCONVENIENTE;

            i++;
            if (item.SERVICIO_CODE != null)
                parameters[i].Value = item.SERVICIO_CODE;

            i++;
            if (item.INCONVENIENTE_CODE != null)
                parameters[i].Value = item.INCONVENIENTE_CODE;

            i++;
            if (item.CONTRATO != null)
                parameters[i].Value = item.CONTRATO;

            i++;
            if (item.PLANO != null)
                parameters[i].Value = item.PLANO;

            i++;
            if (item.VALOR_1 != null)
                parameters[i].Value = item.VALOR_1;

            i++;
            if (item.VALOR_2 != null)
                parameters[i].Value = item.VALOR_2;

            i++;
            if (item.FLAG_CASO != null)
                parameters[i].Value = item.FLAG_CASO;

            i++;
            if (item.RESULTADO != null)
                parameters[i].Value = item.RESULTADO;

            try
            {
                Web.Logging.ExecuteMethod(strIdSession, strTransaction, () =>
                {
                    DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_CLARIFY, DbCommandConfiguration.SIACU_POST_CLARIFY_SP_CREATE_INTERACT_HFC, parameters);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
            }
            finally
            {
                rInteraccionId = parameters[parameters.Length - 3].Value.ToString();
                rFlagInsercion = parameters[parameters.Length - 2].Value.ToString();
                rMsgText = parameters[parameters.Length - 1].Value.ToString();
            }

            return true;
        }

        //Name: SIAC HFC ObtenerServiciosPorPlan
        public static List<EntitiesFixed.PlanService> GetPlanServices(string strIdSession, string strTransaction, string idplan, string typeProduct)
        {
            var salida = new List<EntitiesFixed.PlanService>();
            try
            {
                DbParameter[] parameters = 
                {
                    new DbParameter("P_PLAN", DbType.String, 255, ParameterDirection.Input, idplan), 
                    new DbParameter("P_TIPO_PRODUCTO", DbType.String, 255, ParameterDirection.Input, typeProduct), 
                    new DbParameter("P_CURSOR", DbType.Object, ParameterDirection.Output)
                };

                Web.Logging.ExecuteMethod(strIdSession, strTransaction, () =>
                {
                    DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_PVU,
                        DbCommandConfiguration.SIACU_SP_CON_PLAN_SERVICIO, parameters, reader =>
                        {
                            while (reader.Read())
                            {
                                var obj = new EntitiesFixed.PlanService()
                                {
                                    CodigoPlan = reader["COD_PLAN_SISACT"].ToString(),
                                    DescPlan = reader["DES_PLAN_SISACT"].ToString(),
                                    TmCode = reader["TMCODE"].ToString(),
                                    Solucion = reader["SOLUCION"].ToString(),
                                    CodServSisact = reader["COD_SERV_SISACT"].ToString(),
                                    SNCode = reader["SNCODE"].ToString(),
                                    SPCode = reader["SPCODE"].ToString(),
                                    CodTipoServ = reader["COD_TIPO_SERVICIO"].ToString(),
                                    TipoServ = reader["TIPO_SERVICIO"].ToString(),
                                    DesServSisact = reader["DES_SERV_SISACT"].ToString(),
                                    CodGrupoServ = reader["COD_GRUPO_SERV"].ToString(),
                                    GrupoServ = reader["GRUPO_SERV"].ToString(),
                                    CF = reader["CF"].ToString(),
                                    IdEquipo = reader["IDEQUIPO"].ToString(),
                                    Equipo = reader["EQUIPO"].ToString(),
                                    CantidadEquipo = reader["CANT_EQUIPO"].ToString(),
                                    //MatvIdSap = reader["MATV_ID_SAP"].ToString(),
                                    MatvIdSap = reader["CODTIPEQU"].ToString(),
                                    //MatvDesSap = reader["MATV_DES_SAP"].ToString(),
                                    MatvDesSap = reader["DSCEQU"].ToString(),
                                    TipoEquipo = reader["TIPEQU"].ToString(),
                                    CodigoExterno = reader["COD_EXTERNO"].ToString(),
                                    DesCodigoExterno = reader["DES_COD_EXTERNO"].ToString(),
                                    ServvUsuarioCrea = reader["SERVV_USUARIO_CREA"].ToString()
                                };

                                salida.Add(obj);
                            }
                        });
                });

            }
            catch (Exception ex)
            {
                salida.Clear();
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
            }

            return salida;
        }

        //Name: SIAC HFC ObtenerDatosPlantillaInteraccion
        public static EntitiesFixed.InteractionTemplate GetInfoInteractionTemplate(string strIdSession, string strTransaction, string vInteraccionID, ref string vFLAG_CONSULTA, ref string vMSG_TEXT)
        {
            DbParameter[] parameters = {
                new DbParameter("P_NRO_INTERACCION", DbType.String,255,ParameterDirection.Input, vInteraccionID),
                new DbParameter("FLAG_CONSULTA", DbType.String,255,ParameterDirection.Output, vFLAG_CONSULTA),
                new DbParameter("MSG_TEXT", DbType.String,255,ParameterDirection.Output,vMSG_TEXT),
                new DbParameter("OUT_CURSOR", DbType.Object,ParameterDirection.Output)
            };

            var item = new EntitiesFixed.InteractionTemplate();
            try
            {
                Web.Logging.ExecuteMethod(strIdSession, strTransaction, () =>
                {
                    DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_CLARIFY,
                        DbCommandConfiguration.SIACU_POST_CLARIFY_SP_QUERY_PLUS_INTER, parameters, reader =>
                        {
                            while (reader.Read())
                            {
                                item.TIENE_DATOS = "S";
                                item.ID_INTERACCION = reader["NRO_INTERACCION"].ToString();
                                item.X_INTER_1 = reader["X_INTER_1"].ToString();
                                item.X_INTER_2 = reader["X_INTER_2"].ToString();
                                item.X_INTER_3 = reader["X_INTER_3"].ToString();
                                item.X_INTER_4 = reader["X_INTER_4"].ToString();
                                item.X_INTER_5 = reader["X_INTER_5"].ToString();
                                item.X_INTER_6 = reader["X_INTER_6"].ToString();
                                item.X_INTER_7 = reader["X_INTER_7"].ToString();
                                item.X_INTER_8 = Convert.ToDouble(reader["X_INTER_8"]);
                                item.X_INTER_9 = Convert.ToDouble(reader["X_INTER_9"]);
                                item.X_INTER_10 = Convert.ToDouble(reader["X_INTER_10"]);
                                item.X_INTER_11 = Convert.ToDouble(reader["X_INTER_11"]);
                                item.X_INTER_12 = Convert.ToDouble(reader["X_INTER_12"]);
                                item.X_INTER_13 = Convert.ToDouble(reader["X_INTER_13"]);
                                item.X_INTER_14 = Convert.ToDouble(reader["X_INTER_14"]);
                                item.X_INTER_15 = reader["X_INTER_15"].ToString();
                                item.X_INTER_16 = reader["X_INTER_16"].ToString();
                                item.X_INTER_17 = reader["X_INTER_17"].ToString();
                                item.X_INTER_18 = reader["X_INTER_18"].ToString();
                                item.X_INTER_19 = reader["X_INTER_19"].ToString();
                                item.X_INTER_20 = reader["X_INTER_20"].ToString();
                                item.X_INTER_21 = reader["X_INTER_21"].ToString();
                                item.X_INTER_22 = Convert.ToDouble(reader["X_INTER_22"]);
                                item.X_INTER_23 = Convert.ToDouble(reader["X_INTER_23"]);
                                item.X_INTER_24 = Convert.ToDouble(reader["X_INTER_24"]);
                                item.X_INTER_25 = Convert.ToDouble(reader["X_INTER_25"]);
                                item.X_INTER_26 = Convert.ToDouble(reader["X_INTER_26"]);
                                item.X_INTER_27 = Convert.ToDouble(reader["X_INTER_27"]);
                                item.X_INTER_28 = Convert.ToDouble(reader["X_INTER_28"]);
                                item.X_INTER_29 = reader["X_INTER_29"].ToString();
                                item.X_INTER_30 = reader["X_INTER_30"].ToString();
                                item.X_PLUS_INTER2INTERACT = Convert.ToDouble(reader["X_PLUS_INTER2INTERACT"]);
                                item.X_ADJUSTMENT_AMOUNT = Convert.ToDouble(reader["X_ADJUSTMENT_AMOUNT"]);
                                item.X_ADJUSTMENT_REASON = reader["X_ADJUSTMENT_REASON"].ToString();
                                item.X_ADDRESS = reader["X_ADDRESS"].ToString();
                                item.X_AMOUNT_UNIT = reader["X_AMOUNT_UNIT"].ToString();

                                if (reader["X_BIRTHDAY"] != null && reader["X_BIRTHDAY"] != DBNull.Value)
                                    item.X_BIRTHDAY = Convert.ToDate(reader["X_BIRTHDAY"]);

                                item.X_CLARIFY_INTERACTION = reader["X_CLARIFY_INTERACTION"].ToString();
                                item.X_CLARO_LDN1 = reader["X_CLARO_LDN1"].ToString();
                                item.X_CLARO_LDN2 = reader["X_CLARO_LDN2"].ToString();
                                item.X_CLARO_LDN3 = reader["X_CLARO_LDN3"].ToString();
                                item.X_CLARO_LDN4 = reader["X_CLARO_LDN4"].ToString();
                                item.X_CLAROLOCAL1 = reader["X_CLAROLOCAL1"].ToString();
                                item.X_CLAROLOCAL2 = reader["X_CLAROLOCAL2"].ToString();
                                item.X_CLAROLOCAL3 = reader["X_CLAROLOCAL3"].ToString();
                                item.X_CLAROLOCAL4 = reader["X_CLAROLOCAL4"].ToString();
                                item.X_CLAROLOCAL5 = reader["X_CLAROLOCAL5"].ToString();
                                item.X_CLAROLOCAL6 = reader["X_CLAROLOCAL6"].ToString();
                                item.X_CONTACT_PHONE = reader["X_CONTACT_PHONE"].ToString();
                                item.X_DNI_LEGAL_REP = reader["X_DNI_LEGAL_REP"].ToString();
                                item.X_DOCUMENT_NUMBER = reader["X_DOCUMENT_NUMBER"].ToString();
                                item.X_EMAIL = reader["X_EMAIL"].ToString();
                                item.X_FIRST_NAME = reader["X_FIRST_NAME"].ToString();
                                item.X_FIXED_NUMBER = reader["X_FIXED_NUMBER"].ToString();
                                item.X_FLAG_CHANGE_USER = reader["X_FLAG_CHANGE_USER"].ToString();
                                item.X_FLAG_LEGAL_REP = reader["X_FLAG_LEGAL_REP"].ToString();
                                item.X_FLAG_OTHER = reader["X_FLAG_OTHER"].ToString();
                                item.X_FLAG_TITULAR = reader["X_FLAG_TITULAR"].ToString();
                                item.X_IMEI = reader["X_IMEI"].ToString();
                                item.X_LAST_NAME = reader["X_LAST_NAME"].ToString();
                                item.X_LASTNAME_REP = reader["X_LASTNAME_REP"].ToString();
                                item.X_LDI_NUMBER = reader["X_LDI_NUMBER"].ToString();
                                item.X_NAME_LEGAL_REP = reader["X_NAME_LEGAL_REP"].ToString();
                                item.X_OLD_CLARO_LDN1 = reader["X_OLD_CLARO_LDN1"].ToString();
                                item.X_OLD_CLARO_LDN2 = reader["X_OLD_CLARO_LDN2"].ToString();
                                item.X_OLD_CLARO_LDN3 = reader["X_OLD_CLARO_LDN3"].ToString();
                                item.X_OLD_CLARO_LDN4 = reader["X_OLD_CLARO_LDN4"].ToString();
                                item.X_OLD_CLAROLOCAL1 = reader["X_OLD_CLAROLOCAL1"].ToString();
                                item.X_OLD_CLAROLOCAL2 = reader["X_OLD_CLAROLOCAL2"].ToString();
                                item.X_OLD_CLAROLOCAL3 = reader["X_OLD_CLAROLOCAL3"].ToString();
                                item.X_OLD_CLAROLOCAL4 = reader["X_OLD_CLAROLOCAL4"].ToString();
                                item.X_OLD_CLAROLOCAL5 = reader["X_OLD_CLAROLOCAL5"].ToString();
                                item.X_OLD_CLAROLOCAL6 = reader["X_OLD_CLAROLOCAL6"].ToString();
                                item.X_OLD_DOC_NUMBER = reader["X_OLD_DOC_NUMBER"].ToString();
                                item.X_OLD_FIRST_NAME = reader["X_OLD_FIRST_NAME"].ToString();
                                item.X_OLD_FIXED_PHONE = reader["X_OLD_FIXED_PHONE"].ToString();
                                item.X_OLD_LAST_NAME = reader["X_OLD_LAST_NAME"].ToString();
                                item.X_OLD_LDI_NUMBER = reader["X_OLD_LDI_NUMBER"].ToString();
                                item.X_OLD_FIXED_NUMBER = reader["X_OLD_FIXED_NUMBER"].ToString();
                                item.X_OPERATION_TYPE = reader["X_OPERATION_TYPE"].ToString();
                                item.X_OTHER_DOC_NUMBER = reader["X_OTHER_DOC_NUMBER"].ToString();
                                item.X_OTHER_FIRST_NAME = reader["X_OTHER_FIRST_NAME"].ToString();
                                item.X_OTHER_LAST_NAME = reader["X_OTHER_LAST_NAME"].ToString();
                                item.X_OTHER_PHONE = reader["X_OTHER_PHONE"].ToString();
                                item.X_PHONE_LEGAL_REP = reader["X_PHONE_LEGAL_REP"].ToString();
                                item.X_REFERENCE_PHONE = reader["X_REFERENCE_PHONE"].ToString();
                                item.X_REASON = reader["X_REASON"].ToString();
                                item.X_MODEL = reader["X_MODEL"].ToString();
                                item.X_LOT_CODE = reader["X_LOT_CODE"].ToString();
                                item.X_FLAG_REGISTERED = reader["X_FLAG_REGISTERED"].ToString();
                                item.X_REGISTRATION_REASON = reader["X_REGISTRATION_REASON"].ToString();
                                item.X_CLARO_NUMBER = reader["X_CLARO_NUMBER"].ToString();
                                item.X_MONTH = reader["X_MONTH"].ToString();
                                item.X_OST_NUMBER = reader["X_OST_NUMBER"].ToString();
                                item.X_BASKET = reader["X_BASKET"].ToString();
                                if (reader["X_EXPIRE_DATE"] != null && reader["X_EXPIRE_DATE"] != DBNull.Value)
                                    item.X_EXPIRE_DATE = Convert.ToDate(reader["X_EXPIRE_DATE"]);
                                item.X_ADDRESS5 = reader["X_ADDRESS5"].ToString();
                                item.X_CHARGE_AMOUNT = Convert.ToDouble(reader["X_CHARGE_AMOUNT"]);
                                item.X_CITY = reader["X_CITY"].ToString();
                                item.X_CONTACT_SEX = reader["X_CONTACT_SEX"].ToString();
                                item.X_DEPARTMENT = reader["X_DEPARTMENT"].ToString();
                                item.X_DISTRICT = reader["X_DISTRICT"].ToString();
                                item.X_EMAIL_CONFIRMATION = reader["X_EMAIL_CONFIRMATION"].ToString();
                                item.X_FAX = reader["X_FAX"].ToString();
                                item.X_FLAG_CHARGE = reader["X_FLAG_CHARGE"].ToString();
                                item.X_MARITAL_STATUS = reader["X_MARITAL_STATUS"].ToString();
                                item.X_OCCUPATION = reader["X_OCCUPATION"].ToString();
                                item.X_POSITION = reader["X_POSITION"].ToString();
                                item.X_REFERENCE_ADDRESS = reader["X_REFERENCE_ADDRESS"].ToString();
                                item.X_TYPE_DOCUMENT = reader["X_TYPE_DOCUMENT"].ToString();
                                item.X_ZIPCODE = reader["X_ZIPCODE"].ToString();
                                item.X_ICCID = reader["X_ICCID"].ToString();
                            }
                        });
                });

            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
            }
            finally
            {
                vFLAG_CONSULTA = parameters[parameters.Length - 2].Value.ToString();
                vMSG_TEXT = parameters[parameters.Length - 1].ToString();
            }

            return item;
        }

        //Name: SIAC HFC InsertarInteraccion DAContingenciaClarifyDatos
        public static bool GetInsertInteraction(string strIdSession, string strTransaction, EntitiesFixed.Interaction item, ref string rInteraccionId, ref string rFlagInsercion, ref string rMsgText)
        {
            DbParameter[] parameters = {
                new DbParameter("PN_CONTACTOBJID_1", DbType.Int64,ParameterDirection.Input),
                new DbParameter("PN_SITEOBJID_1", DbType.Int64,ParameterDirection.Input),
                new DbParameter("PV_ACCOUNT", DbType.String,255,ParameterDirection.Input),
                new DbParameter("PV_PHONE", DbType.String,255,ParameterDirection.Input),
                new DbParameter("PV_TIPO", DbType.String,255,ParameterDirection.Input),
                new DbParameter("PV_CLASE", DbType.String,255,ParameterDirection.Input),
                new DbParameter("PV_SUBCLASE", DbType.String,255,ParameterDirection.Input),
                new DbParameter("PV_METODO_CONTACTO", DbType.String,255,ParameterDirection.Input),
                new DbParameter("PV_TIPO_INTER", DbType.String,255,ParameterDirection.Input),
                new DbParameter("PV_AGENTE", DbType.String,255,ParameterDirection.Input),
                new DbParameter("PV_USR_PROCESO", DbType.String,255,ParameterDirection.Input),
                new DbParameter("PN_HECHO_EN_UNO", DbType.String,255,ParameterDirection.Input),
                new DbParameter("PV_NOTAS", DbType.String,4000,ParameterDirection.Input),
                new DbParameter("PV_FLAG_CASO", DbType.String,255,ParameterDirection.Input),
                new DbParameter("PV_RESULTADO", DbType.String,255,ParameterDirection.Input),
                new DbParameter("ID_INTERACCION", DbType.String,255,ParameterDirection.Output),				
                new DbParameter("FLAG_CREACION", DbType.String,255,ParameterDirection.Output),
                new DbParameter("MSG_TEXT", DbType.String,255,ParameterDirection.Output)			
            };

            for (int j = 0; j < parameters.Length; j++)
                parameters[j].Value = System.DBNull.Value;

            int i = 0;
            if (item.OBJID_CONTACTO != null)
                parameters[i].Value = 0;

            i++;
            if (item.OBJID_SITE != null)
                parameters[i].Value = Convert.ToInt(item.OBJID_SITE);

            i++;
            if (item.CUENTA != null)
                parameters[i].Value = item.CUENTA;

            i++;
            if (item.TELEFONO != null)
                parameters[i].Value = item.TELEFONO;
            i++;
            if (item.TIPO != null)
                parameters[i].Value = item.TIPO;

            i++;
            if (item.CLASE != null)
                parameters[i].Value = item.CLASE;

            i++;
            if (item.SUBCLASE != null)
                parameters[i].Value = item.SUBCLASE;

            i++;
            if (item.METODO != null)
                parameters[i].Value = item.METODO;

            i++;
            if (item.TIPO_INTER != null)
                parameters[i].Value = item.TIPO_INTER;

            i++;
            if (item.AGENTE != null)
                parameters[i].Value = item.AGENTE;

            i++;
            if (item.USUARIO_PROCESO != null)
                parameters[i].Value = item.USUARIO_PROCESO;

            i++;
            if (item.HECHO_EN_UNO != null)
                parameters[i].Value = item.HECHO_EN_UNO;

            i++;
            if (item.NOTAS != null)
                parameters[i].Value = item.NOTAS;

            i++;
            if (item.FLAG_CASO != null)
                parameters[i].Value = item.FLAG_CASO;

            i++;
            if (item.RESULTADO != null)
                parameters[i].Value = item.RESULTADO;

            try
            {
                Web.Logging.ExecuteMethod(strIdSession, strTransaction, () =>
                {
                    DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_POST_DB_SP_INSERTAR_INTERACT, parameters);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
            }
            finally
            {
                rInteraccionId = parameters[parameters.Length - 3].Value.ToString();
                rFlagInsercion = parameters[parameters.Length - 2].Value.ToString();
                rMsgText = parameters[parameters.Length - 1].Value.ToString();
            }

            return true;
        }

        //Name: SIAC HFC InsertarPlantillaInteraccion DAPlantilla
        public static bool GetInsertInteractionTemplate(string strIdSession, string strTransaction, EntitiesFixed.InteractionTemplate item, string vInteraccionId, ref string rFlagInsercion, ref string rMsgText)
        {
            DbParameter[] parameters = {
                new DbParameter("P_NRO_INTERACCION",DbType.String,255,ParameterDirection.Input),
                new DbParameter("P_INTER_1",DbType.String,40,ParameterDirection.Input),
                new DbParameter("P_INTER_2",DbType.String,40,ParameterDirection.Input),
                new DbParameter("P_INTER_3",DbType.String,40,ParameterDirection.Input),
                new DbParameter("P_INTER_4",DbType.String,40,ParameterDirection.Input),
                new DbParameter("P_INTER_5",DbType.String,40,ParameterDirection.Input),
                new DbParameter("P_INTER_6",DbType.String,40,ParameterDirection.Input),
                new DbParameter("P_INTER_7",DbType.String,40,ParameterDirection.Input),
                new DbParameter("P_INTER_8",DbType.Decimal,ParameterDirection.Input),
                new DbParameter("P_INTER_9",DbType.Decimal,ParameterDirection.Input),
                new DbParameter("P_INTER_10",DbType.Decimal,ParameterDirection.Input),
                new DbParameter("P_INTER_11",DbType.Decimal,ParameterDirection.Input),
                new DbParameter("P_INTER_12",DbType.Decimal,ParameterDirection.Input),
                new DbParameter("P_INTER_13",DbType.Decimal,ParameterDirection.Input),
                new DbParameter("P_INTER_14",DbType.Decimal,ParameterDirection.Input),
                new DbParameter("P_INTER_15",DbType.String,40,ParameterDirection.Input),
                new DbParameter("P_INTER_16",DbType.String,40,ParameterDirection.Input),
                new DbParameter("P_INTER_17",DbType.String,40,ParameterDirection.Input),
                new DbParameter("P_INTER_18",DbType.String,40,ParameterDirection.Input),
                new DbParameter("P_INTER_19",DbType.String,40,ParameterDirection.Input),
                new DbParameter("P_INTER_20",DbType.String,40,ParameterDirection.Input),
                new DbParameter("P_INTER_21",DbType.String,40,ParameterDirection.Input),
                new DbParameter("P_INTER_22",DbType.Decimal,ParameterDirection.Input),
                new DbParameter("P_INTER_23",DbType.Decimal,ParameterDirection.Input),
                new DbParameter("P_INTER_24",DbType.Decimal,ParameterDirection.Input),
                new DbParameter("P_INTER_25",DbType.Decimal,ParameterDirection.Input),
                new DbParameter("P_INTER_26",DbType.Decimal,ParameterDirection.Input),
                new DbParameter("P_INTER_27",DbType.Decimal,ParameterDirection.Input),
                new DbParameter("P_INTER_28",DbType.Decimal,ParameterDirection.Input),
                new DbParameter("P_INTER_29",DbType.String,255,ParameterDirection.Input),
                new DbParameter("P_INTER_30",DbType.String,32000,ParameterDirection.Input),
                new DbParameter("P_PLUS_INTER2INTERACT",DbType.Decimal,ParameterDirection.Input),
                new DbParameter("P_ADJUSTMENT_AMOUNT",DbType.Double,ParameterDirection.Input),
                new DbParameter("P_ADJUSTMENT_REASON",DbType.String,	20,ParameterDirection.Input),
                new DbParameter("P_ADDRESS",DbType.String,100,ParameterDirection.Input),
                new DbParameter("P_AMOUNT_UNIT",DbType.String,20,ParameterDirection.Input),
                new DbParameter("P_BIRTHDAY",DbType.Date,ParameterDirection.Input),
                new DbParameter("P_CLARIFY_INTERACTION",DbType.String,15,ParameterDirection.Input),
                new DbParameter("P_CLARO_LDN1",DbType.String,20,ParameterDirection.Input),
                new DbParameter("P_CLARO_LDN2",DbType.String,20,ParameterDirection.Input),
                new DbParameter("P_CLARO_LDN3",DbType.String,20,ParameterDirection.Input),
                new DbParameter("P_CLARO_LDN4",DbType.String,20,ParameterDirection.Input),
                new DbParameter("P_CLAROLOCAL1",DbType.String,20,ParameterDirection.Input),
                new DbParameter("P_CLAROLOCAL2",DbType.String,20,ParameterDirection.Input),
                new DbParameter("P_CLAROLOCAL3",DbType.String,20,ParameterDirection.Input),
                new DbParameter("P_CLAROLOCAL4",DbType.String,20,ParameterDirection.Input),
                new DbParameter("P_CLAROLOCAL5",DbType.String,20,ParameterDirection.Input),
                new DbParameter("P_CLAROLOCAL6",DbType.String,20,ParameterDirection.Input),
                new DbParameter("P_CONTACT_PHONE",DbType.String,20,ParameterDirection.Input),
                new DbParameter("P_DNI_LEGAL_REP",DbType.String,20,ParameterDirection.Input),
                new DbParameter("P_DOCUMENT_NUMBER",DbType.String,20,ParameterDirection.Input),
                new DbParameter("P_EMAIL",DbType.String,30,ParameterDirection.Input),
                new DbParameter("P_FIRST_NAME",DbType.String,30,ParameterDirection.Input),
                new DbParameter("P_FIXED_NUMBER",DbType.String,20,ParameterDirection.Input),
                new DbParameter("P_FLAG_CHANGE_USER",DbType.String,1,ParameterDirection.Input),
                new DbParameter("P_FLAG_LEGAL_REP",DbType.String,1,ParameterDirection.Input),
                new DbParameter("P_FLAG_OTHER",DbType.String,1,ParameterDirection.Input),
                new DbParameter("P_FLAG_TITULAR",DbType.String,1,ParameterDirection.Input),
                new DbParameter("P_IMEI",DbType.String,20,ParameterDirection.Input),
                new DbParameter("P_LAST_NAME",DbType.String,30,ParameterDirection.Input),
                new DbParameter("P_LASTNAME_REP",DbType.String,30,ParameterDirection.Input),
                new DbParameter("P_LDI_NUMBER",DbType.String,20,ParameterDirection.Input),
                new DbParameter("P_NAME_LEGAL_REP",DbType.String,30,ParameterDirection.Input),
                new DbParameter("P_OLD_CLARO_LDN1",DbType.String,20,ParameterDirection.Input),
                new DbParameter("P_OLD_CLARO_LDN2",DbType.String,20,ParameterDirection.Input),
                new DbParameter("P_OLD_CLARO_LDN3",DbType.String,20,ParameterDirection.Input),
                new DbParameter("P_OLD_CLARO_LDN4",DbType.String,20,ParameterDirection.Input),
                new DbParameter("P_OLD_CLAROLOCAL1",DbType.String,20,ParameterDirection.Input),
                new DbParameter("P_OLD_CLAROLOCAL2",DbType.String,20,ParameterDirection.Input),
                new DbParameter("P_OLD_CLAROLOCAL3",DbType.String,20,ParameterDirection.Input),
                new DbParameter("P_OLD_CLAROLOCAL4",DbType.String,20,ParameterDirection.Input),
                new DbParameter("P_OLD_CLAROLOCAL5",DbType.String,20,ParameterDirection.Input),
                new DbParameter("P_OLD_CLAROLOCAL6",DbType.String,20,ParameterDirection.Input),
                new DbParameter("P_OLD_DOC_NUMBER",DbType.String,20,ParameterDirection.Input),
                new DbParameter("P_OLD_FIRST_NAME",DbType.String,30,ParameterDirection.Input),
                new DbParameter("P_OLD_FIXED_PHONE",DbType.String,20,ParameterDirection.Input),
                new DbParameter("P_OLD_LAST_NAME",DbType.String,30,ParameterDirection.Input),
                new DbParameter("P_OLD_LDI_NUMBER",DbType.String,20,ParameterDirection.Input),
                new DbParameter("P_OLD_FIXED_NUMBER",DbType.String,20,ParameterDirection.Input),
                new DbParameter("P_OPERATION_TYPE",DbType.String,50,ParameterDirection.Input),
                new DbParameter("P_OTHER_DOC_NUMBER",DbType.String,20,ParameterDirection.Input),
                new DbParameter("P_OTHER_FIRST_NAME",DbType.String,30,ParameterDirection.Input),
                new DbParameter("P_OTHER_LAST_NAME",DbType.String,30,ParameterDirection.Input),
                new DbParameter("P_OTHER_PHONE",DbType.String,20,ParameterDirection.Input),
                new DbParameter("P_PHONE_LEGAL_REP",DbType.String,20,ParameterDirection.Input),
                new DbParameter("P_REFERENCE_PHONE",DbType.String,20,ParameterDirection.Input),
                new DbParameter("P_REASON",DbType.String,20,ParameterDirection.Input),
                new DbParameter("P_MODEL",DbType.String,50,ParameterDirection.Input),
                new DbParameter("P_LOT_CODE",DbType.String,50,ParameterDirection.Input),
                new DbParameter("P_FLAG_REGISTERED",DbType.String,1,ParameterDirection.Input),
                new DbParameter("P_REGISTRATION_REASON",DbType.String,80,ParameterDirection.Input),
                new DbParameter("P_CLARO_NUMBER",DbType.String,20,ParameterDirection.Input),
                new DbParameter("P_MONTH",DbType.String,30,ParameterDirection.Input),
                new DbParameter("P_OST_NUMBER",DbType.String,30,ParameterDirection.Input),
                new DbParameter("P_BASKET",DbType.String,50,ParameterDirection.Input),
                new DbParameter("P_EXPIRE_DATE",DbType.Date,ParameterDirection.Input),
                new DbParameter("P_ADDRESS5",DbType.String,200,ParameterDirection.Input),
                new DbParameter("P_CHARGE_AMOUNT",DbType.Decimal,ParameterDirection.Input),
                new DbParameter("P_CITY",DbType.String,30,ParameterDirection.Input),
                new DbParameter("P_CONTACT_SEX",DbType.String,1,ParameterDirection.Input),
                new DbParameter("P_DEPARTMENT",DbType.String,40,ParameterDirection.Input),
                new DbParameter("P_DISTRICT",DbType.String,200,ParameterDirection.Input),
                new DbParameter("P_EMAIL_CONFIRMATION",DbType.String,1,ParameterDirection.Input),
                new DbParameter("P_FAX",DbType.String,20,ParameterDirection.Input),
                new DbParameter("P_FLAG_CHARGE",DbType.String,1,ParameterDirection.Input),
                new DbParameter("P_MARITAL_STATUS",DbType.String,20,ParameterDirection.Input),
                new DbParameter("P_OCCUPATION",DbType.String,20,ParameterDirection.Input),
                new DbParameter("P_POSITION",DbType.String,30,ParameterDirection.Input),
                new DbParameter("P_REFERENCE_ADDRESS",DbType.String,50,ParameterDirection.Input),
                new DbParameter("P_TYPE_DOCUMENT",DbType.String,20,ParameterDirection.Input),
                new DbParameter("P_ZIPCODE",DbType.String,20,ParameterDirection.Input),
                new DbParameter("P_ICCID",DbType.String,20,ParameterDirection.Input),
                new DbParameter("ID_INTERACCION",DbType.String,255,ParameterDirection.Output),
                new DbParameter("FLAG_CREACION",DbType.String,255,ParameterDirection.Output),
                new DbParameter("MSG_TEXT",DbType.String,255,ParameterDirection.Output)												  
            };
            for (int j = 0; j < parameters.Length; j++)
                parameters[j].Value = System.DBNull.Value;

            int i = 0;
            DateTime dateInicio = new DateTime(1, 1, 1);


            if (vInteraccionId != null)
            {
                parameters[i].Value = vInteraccionId;
                item.X_PLUS_INTER2INTERACT = Functions.CheckDbl(vInteraccionId);
            }

            i++;
            if (item.X_INTER_1 != null)
                parameters[i].Value = Functions.CheckStr(item.X_INTER_1);

            i++;
            if (item.X_INTER_2 != null)
                parameters[i].Value = Functions.CheckStr(item.X_INTER_2);

            i++;
            if (item.X_INTER_3 != null)
                parameters[i].Value = Functions.CheckStr(item.X_INTER_3);

            i++;
            if (item.X_INTER_4 != null)
                parameters[i].Value = Functions.CheckStr(item.X_INTER_4);

            i++;
            if (item.X_INTER_5 != null)
                parameters[i].Value = Functions.CheckStr(item.X_INTER_5);

            i++;
            if (item.X_INTER_6 != null)
                parameters[i].Value = Functions.CheckStr(item.X_INTER_6);

            i++;
            if (item.X_INTER_7 != null)
                parameters[i].Value = Functions.CheckStr(item.X_INTER_7);

            i++;
            parameters[i].Value = Functions.CheckDbl(item.X_INTER_8);

            i++;
            parameters[i].Value = Functions.CheckDbl(item.X_INTER_9);

            i++;
            parameters[i].Value = Functions.CheckDbl(item.X_INTER_10);

            i++;
            parameters[i].Value = Functions.CheckDblDB(item.X_INTER_11);

            i++;
            parameters[i].Value = Functions.CheckDbl(item.X_INTER_12);

            i++;
            parameters[i].Value = Functions.CheckDbl(item.X_INTER_13);

            i++;
            parameters[i].Value = Functions.CheckDbl(item.X_INTER_14);

            i++;
            if (item.X_INTER_15 != null)
                parameters[i].Value = Functions.CheckStr(item.X_INTER_15);

            i++;
            if (item.X_INTER_16 != null)
                parameters[i].Value = Functions.CheckStr(item.X_INTER_16);

            i++;
            if (item.X_INTER_17 != null)
                parameters[i].Value = Functions.CheckStr(item.X_INTER_17);

            i++;
            if (item.X_INTER_18 != null)
                parameters[i].Value = Functions.CheckStr(item.X_INTER_18);

            i++;
            if (item.X_INTER_19 != null)
                parameters[i].Value = Functions.CheckStr(item.X_INTER_19);

            i++;
            if (item.X_INTER_20 != null)
                parameters[i].Value = Functions.CheckStr(item.X_INTER_20);

            i++;
            if (item.X_INTER_21 != null)
                parameters[i].Value = Functions.CheckStr(item.X_INTER_21);

            i++;
            parameters[i].Value = Functions.CheckDblDB(item.X_INTER_22);

            i++;
            parameters[i].Value = Functions.CheckDblDB(item.X_INTER_23);

            i++;
            parameters[i].Value = Functions.CheckDblDB(item.X_INTER_24);

            i++;
            parameters[i].Value = Functions.CheckDbl(item.X_INTER_25);

            i++;
            parameters[i].Value = Functions.CheckDbl(item.X_INTER_26);

            i++;
            parameters[i].Value = Functions.CheckDbl(item.X_INTER_27);

            i++;
            parameters[i].Value = Functions.CheckDbl(item.X_INTER_28);

            i++;
            if (item.X_INTER_29 != null)
                parameters[i].Value = Functions.CheckStr(item.X_INTER_29);

            i++;
            if (item.X_INTER_30 != null)
            {
                if (!string.IsNullOrEmpty(item.X_INTER_30.Trim()))
                    parameters[i].Value = Functions.CheckStr(item.X_INTER_30);
            }

            i++;
            parameters[i].Value = Functions.CheckDbl(item.X_PLUS_INTER2INTERACT);

            i++;
            parameters[i].Value = Functions.CheckDblDB(item.X_ADJUSTMENT_AMOUNT);

            i++;
            if (item.X_ADJUSTMENT_REASON != null)
                parameters[i].Value = Functions.CheckStr(item.X_ADJUSTMENT_REASON);

            i++;
            if (item.X_ADDRESS != null)
                parameters[i].Value = Functions.CheckStr(item.X_ADDRESS);

            i++;
            if (item.X_AMOUNT_UNIT != null)
                parameters[i].Value = Functions.CheckStr(item.X_AMOUNT_UNIT);

            i++;
            if (item.X_BIRTHDAY != dateInicio)
                parameters[i].Value = Functions.CheckDate(item.X_BIRTHDAY);

            i++;
            if (item.X_CLARIFY_INTERACTION != null)
                parameters[i].Value = Functions.CheckStr(item.X_CLARIFY_INTERACTION);

            i++;
            if (item.X_CLARO_LDN1 != null)
                parameters[i].Value = Functions.CheckStr(item.X_CLARO_LDN1);

            i++;
            if (item.X_CLARO_LDN2 != null)
                if (item.X_CLARO_LDN2.ToString().Length >= 20)
                {
                     item.X_CLARO_LDN2 = item.X_CLARO_LDN2.ToString().Substring(0,20);
                }
                else
                {
                    parameters[i].Value = Functions.CheckStr(item.X_CLARO_LDN2);
                }
               

            i++;
            if (item.X_CLARO_LDN3 != null)
                parameters[i].Value = Functions.CheckStr(item.X_CLARO_LDN3);

            i++;
            if (item.X_CLARO_LDN4 != null)
                parameters[i].Value = Functions.CheckStr(item.X_CLARO_LDN4);

            i++;
            if (item.X_CLAROLOCAL1 != null)
                parameters[i].Value = Functions.CheckStr(item.X_CLAROLOCAL1);

            i++;
            if (item.X_CLAROLOCAL2 != null)
                parameters[i].Value = Functions.CheckStr(item.X_CLAROLOCAL2);

            i++;
            if (item.X_CLAROLOCAL3 != null)
                parameters[i].Value = Functions.CheckStr(item.X_CLAROLOCAL3);

            i++;
            if (item.X_CLAROLOCAL4 != null)
                parameters[i].Value = Functions.CheckStr(item.X_CLAROLOCAL4);

            i++;
            if (item.X_CLAROLOCAL5 != null)
                parameters[i].Value = Functions.CheckStr(item.X_CLAROLOCAL5);

            i++;
            if (item.X_CLAROLOCAL6 != null)
                parameters[i].Value = Functions.CheckStr(item.X_CLAROLOCAL6);

            i++;
            if (item.X_CONTACT_PHONE != null)
                parameters[i].Value = Functions.CheckStr(item.X_CONTACT_PHONE);

            i++;
            if (item.X_DNI_LEGAL_REP != null)
                parameters[i].Value = Functions.CheckStr(item.X_DNI_LEGAL_REP);

            i++;
            if (item.X_DOCUMENT_NUMBER != null)
                parameters[i].Value = Functions.CheckStr(item.X_DOCUMENT_NUMBER);

            i++;
            if (item.X_EMAIL != null)
                parameters[i].Value = Functions.CheckStr(item.X_EMAIL);

            i++;
            if (item.X_FIRST_NAME != null)
                parameters[i].Value = Functions.CheckStr(item.X_FIRST_NAME);

            i++;
            if (item.X_FIXED_NUMBER != null)
                parameters[i].Value = Functions.CheckStr(item.X_FIXED_NUMBER);

            i++;
            if (item.X_FLAG_CHANGE_USER != null)
                parameters[i].Value = Functions.CheckStr(item.X_FLAG_CHANGE_USER);

            i++;
            if (item.X_FLAG_LEGAL_REP != null)
                parameters[i].Value = Functions.CheckStr(item.X_FLAG_LEGAL_REP);

            i++;
            if (item.X_FLAG_OTHER != null)
                parameters[i].Value = Functions.CheckStr(item.X_FLAG_OTHER);

            i++;
            if (item.X_FLAG_TITULAR != null)
                parameters[i].Value = Functions.CheckStr(item.X_FLAG_TITULAR);

            i++;
            if (item.X_IMEI != null)
                parameters[i].Value = Functions.CheckStr(item.X_IMEI);

            i++;
            if (item.X_LAST_NAME != null)
                parameters[i].Value = Functions.CheckStr(item.X_LAST_NAME);

            i++;
            if (item.X_LASTNAME_REP != null)
                parameters[i].Value = Functions.CheckStr(item.X_LASTNAME_REP);

            i++;
            if (item.X_LDI_NUMBER != null)
                parameters[i].Value = Functions.CheckStr(item.X_LDI_NUMBER);

            i++;
            if (item.X_NAME_LEGAL_REP != null)
                parameters[i].Value = Functions.CheckStr(item.X_NAME_LEGAL_REP);

            i++;
            if (item.X_OLD_CLARO_LDN1 != null)
                parameters[i].Value = Functions.CheckStr(item.X_OLD_CLARO_LDN1);

            i++;
            if (item.X_OLD_CLARO_LDN2 != null)
                parameters[i].Value = Functions.CheckStr(item.X_OLD_CLARO_LDN2);

            i++;
            if (item.X_OLD_CLARO_LDN3 != null)
                parameters[i].Value = Functions.CheckStr(item.X_OLD_CLARO_LDN3);

            i++;
            if (item.X_OLD_CLARO_LDN4 != null)
                parameters[i].Value = Functions.CheckStr(item.X_OLD_CLARO_LDN4);

            i++;
            if (item.X_OLD_CLAROLOCAL1 != null)
                parameters[i].Value = Functions.CheckStr(item.X_OLD_CLAROLOCAL1);

            i++;
            if (item.X_OLD_CLAROLOCAL2 != null)
                parameters[i].Value = Functions.CheckStr(item.X_OLD_CLAROLOCAL2);

            i++;
            if (item.X_OLD_CLAROLOCAL3 != null)
                parameters[i].Value = Functions.CheckStr(item.X_OLD_CLAROLOCAL3);

            i++;
            if (item.X_OLD_CLAROLOCAL4 != null)
                parameters[i].Value = Functions.CheckStr(item.X_OLD_CLAROLOCAL4);

            i++;
            if (item.X_OLD_CLAROLOCAL5 != null)
                parameters[i].Value = Functions.CheckStr(item.X_OLD_CLAROLOCAL5);

            i++;
            if (item.X_OLD_CLAROLOCAL6 != null)
                parameters[i].Value = Functions.CheckStr(item.X_OLD_CLAROLOCAL6);

            i++;
            if (item.X_OLD_DOC_NUMBER != null)
                parameters[i].Value = Functions.CheckStr(item.X_OLD_DOC_NUMBER);

            i++;
            if (item.X_OLD_FIRST_NAME != null)
                parameters[i].Value = Functions.CheckStr(item.X_OLD_FIRST_NAME);

            i++;
            if (item.X_OLD_FIXED_PHONE != null)
                parameters[i].Value = Functions.CheckStr(item.X_OLD_FIXED_PHONE);

            i++;
            if (item.X_OLD_LAST_NAME != null)
                parameters[i].Value = Functions.CheckStr(item.X_OLD_LAST_NAME);

            i++;
            if (item.X_OLD_LDI_NUMBER != null)
                parameters[i].Value = Functions.CheckStr(item.X_OLD_LDI_NUMBER);

            i++;
            if (item.X_OLD_FIXED_NUMBER != null)
                parameters[i].Value = Functions.CheckStr(item.X_OLD_FIXED_NUMBER);

            i++;
            if (item.X_OPERATION_TYPE != null)
                parameters[i].Value = Functions.CheckStr(item.X_OPERATION_TYPE);

            i++;
            if (item.X_OTHER_DOC_NUMBER != null)
                parameters[i].Value = Functions.CheckStr(item.X_OTHER_DOC_NUMBER);

            i++;
            if (item.X_OTHER_FIRST_NAME != null)
                parameters[i].Value = Functions.CheckStr(item.X_OTHER_FIRST_NAME);

            i++;
            if (item.X_OTHER_LAST_NAME != null)
                parameters[i].Value = Functions.CheckStr(item.X_OTHER_LAST_NAME);

            i++;
            if (item.X_OTHER_PHONE != null)
                parameters[i].Value = Functions.CheckStr(item.X_OTHER_PHONE);

            i++;
            if (item.X_PHONE_LEGAL_REP != null)
                parameters[i].Value = Functions.CheckStr(item.X_PHONE_LEGAL_REP);

            i++;
            if (item.X_REFERENCE_PHONE != null)
                parameters[i].Value = Functions.CheckStr(item.X_REFERENCE_PHONE);

            i++;
            if (item.X_REASON != null)
                parameters[i].Value = Functions.CheckStr(item.X_REASON);

            i++;
            if (item.X_MODEL != null)
                parameters[i].Value = Functions.CheckStr(item.X_MODEL);

            i++;
            if (item.X_LOT_CODE != null)
                parameters[i].Value = Functions.CheckStr(item.X_LOT_CODE);

            i++;
            if (item.X_FLAG_REGISTERED != null)
                parameters[i].Value = Functions.CheckStr(item.X_FLAG_REGISTERED);

            i++;
            if (item.X_REGISTRATION_REASON != null)
                parameters[i].Value = Functions.CheckStr(item.X_REGISTRATION_REASON);

            i++;
            if (item.X_CLARO_NUMBER != null)
                parameters[i].Value = Functions.CheckStr(item.X_CLARO_NUMBER);

            i++;
            if (item.X_MONTH != null)
                parameters[i].Value = Functions.CheckStr(item.X_MONTH);

            i++;
            if (item.X_OST_NUMBER != null)
                parameters[i].Value = Functions.CheckStr(item.X_OST_NUMBER);

            i++;
            if (item.X_BASKET != null)
                parameters[i].Value = Functions.CheckStr(item.X_BASKET);

            i++;
            if (item.X_EXPIRE_DATE != dateInicio)
                parameters[i].Value = Functions.CheckStr(item.X_EXPIRE_DATE);

            i++;
            if (item.X_ADDRESS5 != null)
                parameters[i].Value = Functions.CheckStr(item.X_ADDRESS5);
            i++;
            parameters[i].Value = Functions.CheckDbl(item.X_CHARGE_AMOUNT);
            i++;
            if (item.X_CITY != null)
                parameters[i].Value = Functions.CheckStr(item.X_CITY);
            i++;
            if (item.X_CONTACT_SEX != null)
                parameters[i].Value = Functions.CheckStr(item.X_CONTACT_SEX);
            i++;
            if (item.X_DEPARTMENT != null)
                parameters[i].Value = Functions.CheckStr(item.X_DEPARTMENT);
            i++;
            if (item.X_DISTRICT != null)
                parameters[i].Value = Functions.CheckStr(item.X_DISTRICT);
            i++;
            if (item.X_EMAIL_CONFIRMATION != null)
                parameters[i].Value = Functions.CheckStr(item.X_EMAIL_CONFIRMATION);
            i++;
            if (item.X_FAX != null)
                parameters[i].Value = Functions.CheckStr(item.X_FAX);
            i++;
            if (item.X_FLAG_CHARGE != null)
                parameters[i].Value = Functions.CheckStr(item.X_FLAG_CHARGE);
            i++;
            if (item.X_MARITAL_STATUS != null)
                parameters[i].Value = Functions.CheckStr(item.X_MARITAL_STATUS);
            i++;
            if (item.X_OCCUPATION != null)
                parameters[i].Value = Functions.CheckStr(item.X_OCCUPATION);
            i++;
            if (item.X_POSITION != null)
                parameters[i].Value = Functions.CheckStr(item.X_POSITION);
            i++;
            if (item.X_REFERENCE_ADDRESS != null)
                parameters[i].Value = Functions.CheckStr(item.X_REFERENCE_ADDRESS);
            i++;
            if (item.X_TYPE_DOCUMENT != null)
                parameters[i].Value = Functions.CheckStr(item.X_TYPE_DOCUMENT);
            i++;
            if (item.X_ZIPCODE != null)
                parameters[i].Value = Functions.CheckStr(item.X_ZIPCODE);

            i++;
            if (item.X_ICCID != null)
                parameters[i].Value = Functions.CheckStr(item.X_ICCID);

            try
            {
                Web.Logging.ExecuteMethod(strIdSession, strTransaction, () =>
                {
                    DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_CLARIFY, DbCommandConfiguration.SIACU_POST_CLARIFY_SP_CREATE_PLUS_INTER, parameters);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
            }
            finally
            {
                rFlagInsercion = parameters[parameters.Length - 2].Value.ToString();
                rMsgText = parameters[parameters.Length - 1].Value.ToString();
            }

            return true;
        }

        // Name: SIAC HFC InsertarPlantillaInteraccion DAContingenciaClarifyDatos
        public static bool GetInsertInteractionTemplateConti(string strIdSession, string strTransaction, EntitiesFixed.InteractionTemplate item, string vInteraccionId, ref string rFlagInsercion, ref string rMsgText)
        {
            DbParameter[] parameters = {   new DbParameter("PN_SECUENCIAL",DbType.Double,ParameterDirection.Input),
                new DbParameter("PV_NRO_INTERACCION",DbType.String,255,ParameterDirection.Input),
                new DbParameter("PV_INTER_1",DbType.String,40,ParameterDirection.Input),
                new DbParameter("PV_INTER_2",DbType.String,40,ParameterDirection.Input),
                new DbParameter("PV_INTER_3",DbType.String,40,ParameterDirection.Input),
                new DbParameter("PV_INTER_4",DbType.String,40,ParameterDirection.Input),
                new DbParameter("PV_INTER_5",DbType.String,40,ParameterDirection.Input),
                new DbParameter("PV_INTER_6",DbType.String,40,ParameterDirection.Input),
                new DbParameter("PV_INTER_7",DbType.String,40,ParameterDirection.Input),
                new DbParameter("PN_INTER_8",DbType.Decimal,ParameterDirection.Input),
                new DbParameter("PN_INTER_9",DbType.Decimal,ParameterDirection.Input),
                new DbParameter("PN_INTER_10",DbType.Decimal,ParameterDirection.Input),
                new DbParameter("PN_INTER_11",DbType.Decimal,ParameterDirection.Input),
                new DbParameter("PN_INTER_12",DbType.Decimal,ParameterDirection.Input),
                new DbParameter("PN_INTER_13",DbType.Decimal,ParameterDirection.Input),
                new DbParameter("PN_INTER_14",DbType.Decimal,ParameterDirection.Input),
                new DbParameter("PV_INTER_15",DbType.String,40,ParameterDirection.Input),
                new DbParameter("PV_INTER_16",DbType.String,40,ParameterDirection.Input),
                new DbParameter("PV_INTER_17",DbType.String,40,ParameterDirection.Input),
                new DbParameter("PV_INTER_18",DbType.String,40,ParameterDirection.Input),
                new DbParameter("PV_INTER_19",DbType.String,40,ParameterDirection.Input),
                new DbParameter("PV_INTER_20",DbType.String,40,ParameterDirection.Input),
                new DbParameter("PV_INTER_21",DbType.String,40,ParameterDirection.Input),
                new DbParameter("PN_INTER_22",DbType.Decimal,ParameterDirection.Input),
                new DbParameter("PN_INTER_23",DbType.Decimal,ParameterDirection.Input),
                new DbParameter("PN_INTER_24",DbType.Decimal,ParameterDirection.Input),
                new DbParameter("PN_INTER_25",DbType.Decimal,ParameterDirection.Input),
                new DbParameter("PN_INTER_26",DbType.Decimal,ParameterDirection.Input),
                new DbParameter("PN_INTER_27",DbType.Decimal,ParameterDirection.Input),
                new DbParameter("PN_INTER_28",DbType.Decimal,ParameterDirection.Input),
                new DbParameter("PV_INTER_29",DbType.String,255,ParameterDirection.Input),
                new DbParameter("PC_INTER_30",DbType.String,ParameterDirection.Input),
                new DbParameter("PN_PLUS_INTER2INTERACT",DbType.Decimal,ParameterDirection.Input),
                new DbParameter("PN_ADJUSTMENT_AMOUNT",DbType.Double,ParameterDirection.Input),
                new DbParameter("PV_ADJUSTMENT_REASON",DbType.String,	20,ParameterDirection.Input),
                new DbParameter("PV_ADDRESS",DbType.String,100,ParameterDirection.Input),
                new DbParameter("PV_AMOUNT_UNIT",DbType.String,20,ParameterDirection.Input),
                new DbParameter("PD_BIRTHDAY",DbType.Date,ParameterDirection.Input),
                new DbParameter("PV_CLARIFY_INTERACTION",DbType.String,15,ParameterDirection.Input),
                new DbParameter("PV_CLARO_LDN1",DbType.String,20,ParameterDirection.Input),
                new DbParameter("PV_CLARO_LDN2",DbType.String,20,ParameterDirection.Input),
                new DbParameter("PV_CLARO_LDN3",DbType.String,20,ParameterDirection.Input),
                new DbParameter("PV_CLARO_LDN4",DbType.String,20,ParameterDirection.Input),
                new DbParameter("PV_CLAROLOCAL1",DbType.String,20,ParameterDirection.Input),
                new DbParameter("PV_CLAROLOCAL2",DbType.String,20,ParameterDirection.Input),
                new DbParameter("PV_CLAROLOCAL3",DbType.String,20,ParameterDirection.Input),
                new DbParameter("PV_CLAROLOCAL4",DbType.String,20,ParameterDirection.Input),
                new DbParameter("PV_CLAROLOCAL5",DbType.String,20,ParameterDirection.Input),
                new DbParameter("PV_CLAROLOCAL6",DbType.String,20,ParameterDirection.Input),
                new DbParameter("PV_CONTACT_PHONE",DbType.String,20,ParameterDirection.Input),
                new DbParameter("PV_DNI_LEGAL_REP",DbType.String,20,ParameterDirection.Input),
                new DbParameter("PV_DOCUMENT_NUMBER",DbType.String,20,ParameterDirection.Input),
                new DbParameter("PV_EMAIL",DbType.String,30,ParameterDirection.Input),
                new DbParameter("PV_FIRST_NAME",DbType.String,20,ParameterDirection.Input),
                new DbParameter("PV_FIXED_NUMBER",DbType.String,20,ParameterDirection.Input),
                new DbParameter("PV_FLAG_CHANGE_USER",DbType.String,1,ParameterDirection.Input),
                new DbParameter("PV_FLAG_LEGAL_REP",DbType.String,1,ParameterDirection.Input),
                new DbParameter("PV_FLAG_OTHER",DbType.String,1,ParameterDirection.Input),
                new DbParameter("PV_FLAG_TITULAR",DbType.String,1,ParameterDirection.Input),
                new DbParameter("PV_IMEI",DbType.String,20,ParameterDirection.Input),
                new DbParameter("PV_LAST_NAME",DbType.String,30,ParameterDirection.Input),
                new DbParameter("PV_LASTNAME_REP",DbType.String,30,ParameterDirection.Input),
                new DbParameter("PV_LDI_NUMBER",DbType.String,20,ParameterDirection.Input),
                new DbParameter("PV_NAME_LEGAL_REP",DbType.String,30,ParameterDirection.Input),
                new DbParameter("PV_OLD_CLARO_LDN1",DbType.String,20,ParameterDirection.Input),
                new DbParameter("PV_OLD_CLARO_LDN2",DbType.String,20,ParameterDirection.Input),
                new DbParameter("PV_OLD_CLARO_LDN3",DbType.String,20,ParameterDirection.Input),
                new DbParameter("PV_OLD_CLARO_LDN4",DbType.String,20,ParameterDirection.Input),
                new DbParameter("PV_OLD_CLAROLOCAL1",DbType.String,20,ParameterDirection.Input),
                new DbParameter("PV_OLD_CLAROLOCAL2",DbType.String,20,ParameterDirection.Input),
                new DbParameter("PV_OLD_CLAROLOCAL3",DbType.String,20,ParameterDirection.Input),
                new DbParameter("PV_OLD_CLAROLOCAL4",DbType.String,20,ParameterDirection.Input),
                new DbParameter("PV_OLD_CLAROLOCAL5",DbType.String,20,ParameterDirection.Input),
                new DbParameter("PV_OLD_CLAROLOCAL6",DbType.String,20,ParameterDirection.Input),
                new DbParameter("PV_OLD_DOC_NUMBER",DbType.String,20,ParameterDirection.Input),
                new DbParameter("PV_OLD_FIRST_NAME",DbType.String,30,ParameterDirection.Input),
                new DbParameter("PV_OLD_FIXED_PHONE",DbType.String,20,ParameterDirection.Input),
                new DbParameter("PV_OLD_LAST_NAME",DbType.String,30,ParameterDirection.Input),
                new DbParameter("PV_OLD_LDI_NUMBER",DbType.String,20,ParameterDirection.Input),
                new DbParameter("PV_OLD_FIXED_NUMBER",DbType.String,20,ParameterDirection.Input),
                new DbParameter("PV_OPERATION_TYPE",DbType.String,50,ParameterDirection.Input),
                new DbParameter("PV_OTHER_DOC_NUMBER",DbType.String,20,ParameterDirection.Input),
                new DbParameter("PV_OTHER_FIRST_NAME",DbType.String,30,ParameterDirection.Input),
                new DbParameter("PV_OTHER_LAST_NAME",DbType.String,30,ParameterDirection.Input),
                new DbParameter("PV_OTHER_PHONE",DbType.String,20,ParameterDirection.Input),
                new DbParameter("PV_PHONE_LEGAL_REP",DbType.String,20,ParameterDirection.Input),
                new DbParameter("PV_REFERENCE_PHONE",DbType.String,20,ParameterDirection.Input),
                new DbParameter("PV_REASON",DbType.String,20,ParameterDirection.Input),
                new DbParameter("PV_MODEL",DbType.String,50,ParameterDirection.Input),
                new DbParameter("PV_LOT_CODE",DbType.String,50,ParameterDirection.Input),
                new DbParameter("PV_FLAG_REGISTERED",DbType.String,1,ParameterDirection.Input),
                new DbParameter("PV_REGISTRATION_REASON",DbType.String,80,ParameterDirection.Input),
                new DbParameter("PV_CLARO_NUMBER",DbType.String,20,ParameterDirection.Input),
                new DbParameter("PV_MONTH",DbType.String,30,ParameterDirection.Input),
                new DbParameter("PV_OST_NUMBER",DbType.String,30,ParameterDirection.Input),
                new DbParameter("PV_BASKET",DbType.String,50,ParameterDirection.Input),
                new DbParameter("PD_EXPIRE_DATE",DbType.Date,ParameterDirection.Input),
                new DbParameter("PV_ADDRESS5",DbType.String,200,ParameterDirection.Input),
                new DbParameter("PN_CHARGE_AMOUNT",DbType.Decimal,ParameterDirection.Input),
                new DbParameter("PV_CITY",DbType.String,30,ParameterDirection.Input),
                new DbParameter("PV_CONTACT_SEX",DbType.String,1,ParameterDirection.Input),
                new DbParameter("PV_DEPARTMENT",DbType.String,40,ParameterDirection.Input),
                new DbParameter("PV_DISTRICT",DbType.String,200,ParameterDirection.Input),
                new DbParameter("PV_EMAIL_CONFIRMATION",DbType.String,1,ParameterDirection.Input),
                new DbParameter("PV_FAX",DbType.String,20,ParameterDirection.Input),
                new DbParameter("PV_FLAG_CHARGE",DbType.String,1,ParameterDirection.Input),
                new DbParameter("PV_MARITAL_STATUS",DbType.String,20,ParameterDirection.Input),
                new DbParameter("PV_OCCUPATION",DbType.String,20,ParameterDirection.Input),
                new DbParameter("PV_POSITION",DbType.String,30,ParameterDirection.Input),
                new DbParameter("PV_REFERENCE_ADDRESS",DbType.String,50,ParameterDirection.Input),
                new DbParameter("PV_TYPE_DOCUMENT",DbType.String,20,ParameterDirection.Input),
                new DbParameter("PV_ZIPCODE",DbType.String,20,ParameterDirection.Input),
                new DbParameter("PV_ICCID",DbType.String,20,ParameterDirection.Input),
                new DbParameter("ID_INTERACCION",DbType.String,255,ParameterDirection.Output),
                new DbParameter("FLAG_CREACION",DbType.String,255,ParameterDirection.Output),
                new DbParameter("MSG_TEXT",DbType.String,255,ParameterDirection.Output)												  
            };
            for (int j = 0; j < parameters.Length; j++)
                parameters[j].Value = System.DBNull.Value;

            int i = 0;
            DateTime dateInicio = new DateTime(1, 1, 1);

            if (vInteraccionId != null)
            {
                parameters[i].Value = Functions.CheckDbl(vInteraccionId);
            }

            i++;
            if (vInteraccionId != null)
            {
                parameters[i].Value = vInteraccionId;
                item.X_PLUS_INTER2INTERACT = Functions.CheckDbl(vInteraccionId);
            }

            i++;
            if (item.X_INTER_1 != null)
                parameters[i].Value = Functions.CheckStr(item.X_INTER_1);

            i++;
            if (item.X_INTER_2 != null)
                parameters[i].Value = Functions.CheckStr(item.X_INTER_2);

            i++;
            if (item.X_INTER_3 != null)
                parameters[i].Value = Functions.CheckStr(item.X_INTER_3);

            i++;
            if (item.X_INTER_4 != null)
                parameters[i].Value = Functions.CheckStr(item.X_INTER_4);

            i++;
            if (item.X_INTER_5 != null)
                parameters[i].Value = Functions.CheckStr(item.X_INTER_5);

            i++;
            if (item.X_INTER_6 != null)
                parameters[i].Value = Functions.CheckStr(item.X_INTER_6);

            i++;
            if (item.X_INTER_7 != null)
                parameters[i].Value = Functions.CheckStr(item.X_INTER_7);

            i++;
            parameters[i].Value = Functions.CheckDbl(item.X_INTER_8);

            i++;
            parameters[i].Value = Functions.CheckDbl(item.X_INTER_9);

            i++;
            parameters[i].Value = Functions.CheckDbl(item.X_INTER_10);

            i++;
            parameters[i].Value = Functions.CheckDblDB(item.X_INTER_11);

            i++;
            parameters[i].Value = Functions.CheckDbl(item.X_INTER_12);

            i++;
            parameters[i].Value = Functions.CheckDbl(item.X_INTER_13);

            i++;
            parameters[i].Value = Functions.CheckDbl(item.X_INTER_14);

            i++;
            if (item.X_INTER_15 != null)
                parameters[i].Value = Functions.CheckStr(item.X_INTER_15);

            i++;
            if (item.X_INTER_16 != null)
                parameters[i].Value = Functions.CheckStr(item.X_INTER_16);

            i++;
            if (item.X_INTER_17 != null)
                parameters[i].Value = Functions.CheckStr(item.X_INTER_17);

            i++;
            if (item.X_INTER_18 != null)
                parameters[i].Value = Functions.CheckStr(item.X_INTER_18);

            i++;
            if (item.X_INTER_19 != null)
                parameters[i].Value = Functions.CheckStr(item.X_INTER_19);

            i++;
            if (item.X_INTER_20 != null)
                parameters[i].Value = Functions.CheckStr(item.X_INTER_20);

            i++;
            if (item.X_INTER_21 != null)
                parameters[i].Value = Functions.CheckStr(item.X_INTER_21);

            i++;
            parameters[i].Value = Functions.CheckDblDB(item.X_INTER_22);

            i++;
            parameters[i].Value = Functions.CheckDblDB(item.X_INTER_23);

            i++;
            parameters[i].Value = Functions.CheckDblDB(item.X_INTER_24);

            i++;
            parameters[i].Value = Functions.CheckDbl(item.X_INTER_25);

            i++;
            parameters[i].Value = Functions.CheckDbl(item.X_INTER_26);

            i++;
            parameters[i].Value = Functions.CheckDbl(item.X_INTER_27);

            i++;
            parameters[i].Value = Functions.CheckDbl(item.X_INTER_28);

            i++;
            if (item.X_INTER_29 != null)
                parameters[i].Value = Functions.CheckStr(item.X_INTER_29);

            i++;
            if (item.X_INTER_30 != null)
                parameters[i].Value = Functions.CheckStr(item.X_INTER_30);

            i++;
            parameters[i].Value = Functions.CheckDbl(item.X_PLUS_INTER2INTERACT);

            i++;
            parameters[i].Value = Functions.CheckDblDB(item.X_ADJUSTMENT_AMOUNT);

            i++;
            if (item.X_ADJUSTMENT_REASON != null)
                parameters[i].Value = Functions.CheckStr(item.X_ADJUSTMENT_REASON);

            i++;
            if (item.X_ADDRESS != null)
                parameters[i].Value = Functions.CheckStr(item.X_ADDRESS);

            i++;
            if (item.X_AMOUNT_UNIT != null)
                parameters[i].Value = Functions.CheckStr(item.X_AMOUNT_UNIT);

            i++;
            if (item.X_BIRTHDAY != dateInicio)
                parameters[i].Value = Functions.CheckDate(item.X_BIRTHDAY);

            i++;
            if (item.X_CLARIFY_INTERACTION != null)
                parameters[i].Value = Functions.CheckStr(item.X_CLARIFY_INTERACTION);

            i++;
            if (item.X_CLARO_LDN1 != null)
                parameters[i].Value = Functions.CheckStr(item.X_CLARO_LDN1);

            i++;
            if (item.X_CLARO_LDN2 != null)
                parameters[i].Value = Functions.CheckStr(item.X_CLARO_LDN2);

            i++;
            if (item.X_CLARO_LDN3 != null)
                parameters[i].Value = Functions.CheckStr(item.X_CLARO_LDN3);

            i++;
            if (item.X_CLARO_LDN4 != null)
                parameters[i].Value = Functions.CheckStr(item.X_CLARO_LDN4);

            i++;
            if (item.X_CLAROLOCAL1 != null)
                parameters[i].Value = Functions.CheckStr(item.X_CLAROLOCAL1);

            i++;
            if (item.X_CLAROLOCAL2 != null)
                parameters[i].Value = Functions.CheckStr(item.X_CLAROLOCAL2);

            i++;
            if (item.X_CLAROLOCAL3 != null)
                parameters[i].Value = Functions.CheckStr(item.X_CLAROLOCAL3);

            i++;
            if (item.X_CLAROLOCAL4 != null)
                parameters[i].Value = Functions.CheckStr(item.X_CLAROLOCAL4);

            i++;
            if (item.X_CLAROLOCAL5 != null)
                parameters[i].Value = Functions.CheckStr(item.X_CLAROLOCAL5);

            i++;
            if (item.X_CLAROLOCAL6 != null)
                parameters[i].Value = Functions.CheckStr(item.X_CLAROLOCAL6);

            i++;
            if (item.X_CONTACT_PHONE != null)
                parameters[i].Value = Functions.CheckStr(item.X_CONTACT_PHONE);

            i++;
            if (item.X_DNI_LEGAL_REP != null)
                parameters[i].Value = Functions.CheckStr(item.X_DNI_LEGAL_REP);

            i++;
            if (item.X_DOCUMENT_NUMBER != null)
                parameters[i].Value = Functions.CheckStr(item.X_DOCUMENT_NUMBER);

            i++;
            if (item.X_EMAIL != null)
                parameters[i].Value = Functions.CheckStr(item.X_EMAIL);

            i++;
            if (item.X_FIRST_NAME != null)
                parameters[i].Value = Functions.CheckStr(item.X_FIRST_NAME);

            i++;
            if (item.X_FIXED_NUMBER != null)
                parameters[i].Value = Functions.CheckStr(item.X_FIXED_NUMBER);

            i++;
            if (item.X_FLAG_CHANGE_USER != null)
                parameters[i].Value = Functions.CheckStr(item.X_FLAG_CHANGE_USER);

            i++;
            if (item.X_FLAG_LEGAL_REP != null)
                parameters[i].Value = Functions.CheckStr(item.X_FLAG_LEGAL_REP);

            i++;
            if (item.X_FLAG_OTHER != null)
                parameters[i].Value = Functions.CheckStr(item.X_FLAG_OTHER);

            i++;
            if (item.X_FLAG_TITULAR != null)
                parameters[i].Value = Functions.CheckStr(item.X_FLAG_TITULAR);

            i++;
            if (item.X_IMEI != null)
                parameters[i].Value = Functions.CheckStr(item.X_IMEI);

            i++;
            if (item.X_LAST_NAME != null)
                parameters[i].Value = Functions.CheckStr(item.X_LAST_NAME);

            i++;
            if (item.X_LASTNAME_REP != null)
                parameters[i].Value = Functions.CheckStr(item.X_LASTNAME_REP);

            i++;
            if (item.X_LDI_NUMBER != null)
                parameters[i].Value = Functions.CheckStr(item.X_LDI_NUMBER);

            i++;
            if (item.X_NAME_LEGAL_REP != null)
                parameters[i].Value = Functions.CheckStr(item.X_NAME_LEGAL_REP);

            i++;
            if (item.X_OLD_CLARO_LDN1 != null)
                parameters[i].Value = Functions.CheckStr(item.X_OLD_CLARO_LDN1);

            i++;
            if (item.X_OLD_CLARO_LDN2 != null)
                parameters[i].Value = Functions.CheckStr(item.X_OLD_CLARO_LDN2);

            i++;
            if (item.X_OLD_CLARO_LDN3 != null)
                parameters[i].Value = Functions.CheckStr(item.X_OLD_CLARO_LDN3);

            i++;
            if (item.X_OLD_CLARO_LDN4 != null)
                parameters[i].Value = Functions.CheckStr(item.X_OLD_CLARO_LDN4);

            i++;
            if (item.X_OLD_CLAROLOCAL1 != null)
                parameters[i].Value = Functions.CheckStr(item.X_OLD_CLAROLOCAL1);

            i++;
            if (item.X_OLD_CLAROLOCAL2 != null)
                parameters[i].Value = Functions.CheckStr(item.X_OLD_CLAROLOCAL2);

            i++;
            if (item.X_OLD_CLAROLOCAL3 != null)
                parameters[i].Value = Functions.CheckStr(item.X_OLD_CLAROLOCAL3);

            i++;
            if (item.X_OLD_CLAROLOCAL4 != null)
                parameters[i].Value = Functions.CheckStr(item.X_OLD_CLAROLOCAL4);

            i++;
            if (item.X_OLD_CLAROLOCAL5 != null)
                parameters[i].Value = Functions.CheckStr(item.X_OLD_CLAROLOCAL5);

            i++;
            if (item.X_OLD_CLAROLOCAL6 != null)
                parameters[i].Value = Functions.CheckStr(item.X_OLD_CLAROLOCAL6);

            i++;
            if (item.X_OLD_DOC_NUMBER != null)
                parameters[i].Value = Functions.CheckStr(item.X_OLD_DOC_NUMBER);

            i++;
            if (item.X_OLD_FIRST_NAME != null)
                parameters[i].Value = Functions.CheckStr(item.X_OLD_FIRST_NAME);

            i++;
            if (item.X_OLD_FIXED_PHONE != null)
                parameters[i].Value = Functions.CheckStr(item.X_OLD_FIXED_PHONE);

            i++;
            if (item.X_OLD_LAST_NAME != null)
                parameters[i].Value = Functions.CheckStr(item.X_OLD_LAST_NAME);

            i++;
            if (item.X_OLD_LDI_NUMBER != null)
                parameters[i].Value = Functions.CheckStr(item.X_OLD_LDI_NUMBER);

            i++;
            if (item.X_OLD_FIXED_NUMBER != null)
                parameters[i].Value = Functions.CheckStr(item.X_OLD_FIXED_NUMBER);

            i++;
            if (item.X_OPERATION_TYPE != null)
                parameters[i].Value = Functions.CheckStr(item.X_OPERATION_TYPE);

            i++;
            if (item.X_OTHER_DOC_NUMBER != null)
                parameters[i].Value = Functions.CheckStr(item.X_OTHER_DOC_NUMBER);

            i++;
            if (item.X_OTHER_FIRST_NAME != null)
                parameters[i].Value = Functions.CheckStr(item.X_OTHER_FIRST_NAME);

            i++;
            if (item.X_OTHER_LAST_NAME != null)
                parameters[i].Value = Functions.CheckStr(item.X_OTHER_LAST_NAME);

            i++;
            if (item.X_OTHER_PHONE != null)
                parameters[i].Value = Functions.CheckStr(item.X_OTHER_PHONE);

            i++;
            if (item.X_PHONE_LEGAL_REP != null)
                parameters[i].Value = Functions.CheckStr(item.X_PHONE_LEGAL_REP);

            i++;
            if (item.X_REFERENCE_PHONE != null)
                parameters[i].Value = Functions.CheckStr(item.X_REFERENCE_PHONE);

            i++;
            if (item.X_REASON != null)
                parameters[i].Value = Functions.CheckStr(item.X_REASON);

            i++;
            if (item.X_MODEL != null)
                parameters[i].Value = Functions.CheckStr(item.X_MODEL);

            i++;
            if (item.X_LOT_CODE != null)
                parameters[i].Value = Functions.CheckStr(item.X_LOT_CODE);

            i++;
            if (item.X_FLAG_REGISTERED != null)
                parameters[i].Value = Functions.CheckStr(item.X_FLAG_REGISTERED);

            i++;
            if (item.X_REGISTRATION_REASON != null)
                parameters[i].Value = Functions.CheckStr(item.X_REGISTRATION_REASON);

            i++;
            if (item.X_CLARO_NUMBER != null)
                parameters[i].Value = Functions.CheckStr(item.X_CLARO_NUMBER);

            i++;
            if (item.X_MONTH != null)
                parameters[i].Value = Functions.CheckStr(item.X_MONTH);

            i++;
            if (item.X_OST_NUMBER != null)
                parameters[i].Value = Functions.CheckStr(item.X_OST_NUMBER);

            i++;
            if (item.X_BASKET != null)
                parameters[i].Value = Functions.CheckStr(item.X_BASKET);

            i++;
            if (item.X_EXPIRE_DATE != dateInicio)
                parameters[i].Value = Functions.CheckStr(item.X_EXPIRE_DATE);

            i++;
            if (item.X_ADDRESS5 != null)
                parameters[i].Value = Functions.CheckStr(item.X_ADDRESS5);
            i++;
            parameters[i].Value = Functions.CheckDbl(item.X_CHARGE_AMOUNT);
            i++;
            if (item.X_CITY != null)
                parameters[i].Value = Functions.CheckStr(item.X_CITY);
            i++;
            if (item.X_CONTACT_SEX != null)
                parameters[i].Value = Functions.CheckStr(item.X_CONTACT_SEX);
            i++;
            if (item.X_DEPARTMENT != null)
                parameters[i].Value = Functions.CheckStr(item.X_DEPARTMENT);
            i++;
            if (item.X_DISTRICT != null)
                parameters[i].Value = Functions.CheckStr(item.X_DISTRICT);
            i++;
            if (item.X_EMAIL_CONFIRMATION != null)
                parameters[i].Value = Functions.CheckStr(item.X_EMAIL_CONFIRMATION);
            i++;
            if (item.X_FAX != null)
                parameters[i].Value = Functions.CheckStr(item.X_FAX);
            i++;
            if (item.X_FLAG_CHARGE != null)
                parameters[i].Value = Functions.CheckStr(item.X_FLAG_CHARGE);
            i++;
            if (item.X_MARITAL_STATUS != null)
                parameters[i].Value = Functions.CheckStr(item.X_MARITAL_STATUS);
            i++;
            if (item.X_OCCUPATION != null)
                parameters[i].Value = Functions.CheckStr(item.X_OCCUPATION);
            i++;
            if (item.X_POSITION != null)
                parameters[i].Value = Functions.CheckStr(item.X_POSITION);
            i++;
            if (item.X_REFERENCE_ADDRESS != null)
                parameters[i].Value = Functions.CheckStr(item.X_REFERENCE_ADDRESS);
            i++;
            if (item.X_TYPE_DOCUMENT != null)
                parameters[i].Value = Functions.CheckStr(item.X_TYPE_DOCUMENT);
            i++;
            if (item.X_ZIPCODE != null)
                parameters[i].Value = Functions.CheckStr(item.X_ZIPCODE);

            i++;
            if (item.X_ICCID != null)
                parameters[i].Value = Functions.CheckStr(item.X_ICCID);

            try
            {
                Web.Logging.ExecuteMethod(strIdSession, strTransaction, () =>
                {
                    DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_POST_DB_SP_INSERTAR_X_PLUS_INTER, parameters);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
            }
            finally
            {
                rFlagInsercion = parameters[parameters.Length - 2].Value.ToString();
                rMsgText = parameters[parameters.Length - 1].Value.ToString();
            }

            return true;
        }

        //Name: SIAC LTE ObtenerServiciosPorPlan
        public static List<EntitiesFixed.PlanService> GetPlanServicesLte(string strIdSession, string strTransaction, string idplan, string strCodeProduct)
        {
            var salida = new List<EntitiesFixed.PlanService>();
            try
            {
                DbParameter[] parameters = 
                {
                    new DbParameter("P_PLAN", DbType.String,255, ParameterDirection.Input, idplan), 
                    new DbParameter("P_TIPO_PRODUCTO", DbType.String,255, ParameterDirection.Input, strCodeProduct),
                    new DbParameter("P_CURSOR", DbType.Object, ParameterDirection.Output)
                };

                Web.Logging.ExecuteMethod(strIdSession, strTransaction, () =>
                {
                    DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_PVU,
                        DbCommandConfiguration.SIACU_SP_CON_PLAN_SERVICIO, parameters, reader =>
                        {
                            while (reader.Read())
                            {
                                var obj = new EntitiesFixed.PlanService()
                                {
                                    CodigoPlan = reader["COD_PLAN_SISACT"].ToString(),
                                    DescPlan = reader["DES_PLAN_SISACT"].ToString(),
                                    TmCode = reader["TMCODE"].ToString(),
                                    Solucion = reader["SOLUCION"].ToString(),
                                    CodServSisact = reader["COD_SERV_SISACT"].ToString(),
                                    SNCode = reader["SNCODE"].ToString(),
                                    SPCode = reader["SPCODE"].ToString(),
                                    CodTipoServ = reader["COD_TIPO_SERVICIO"].ToString(),
                                    TipoServ = reader["TIPO_SERVICIO"].ToString(),
                                    DesServSisact = reader["DES_SERV_SISACT"].ToString(),
                                    CodGrupoServ = reader["COD_GRUPO_SERV"].ToString(),
                                    GrupoServ = reader["GRUPO_SERV"].ToString(),
                                    CF = reader["CF"].ToString(),
                                    IdEquipo = reader["IDEQUIPO"].ToString(),
                                    Equipo = reader["EQUIPO"].ToString(),
                                    CantidadEquipo = reader["CANT_EQUIPO"].ToString(),
                                    //MatvIdSap = reader["MATV_ID_SAP"].ToString(),
                                    MatvIdSap = reader["CODTIPEQU"].ToString(),
                                    //MatvDesSap = reader["MATV_DES_SAP"].ToString(),
                                    MatvDesSap = reader["DSCEQU"].ToString(),
                                    TipoEquipo = reader["TIPEQU"].ToString(),
                                    CodigoExterno = reader["COD_EXTERNO"].ToString(),
                                    DesCodigoExterno = reader["DES_COD_EXTERNO"].ToString(),
                                    ServvUsuarioCrea = reader["SERVV_USUARIO_CREA"].ToString()
                                };

                                salida.Add(obj);
                            }
                        });
                });

            }
            catch (Exception ex)
            {
                salida.Clear();
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
            }

            return salida;
        }

        public static EntitiesFixed.GetActivationDesactivation.ActivationDesactivationResponse GetActivationDesactivation(string strIdSession, string strTransaction, EntitiesFixed.GetActivationDesactivation.ActivationDesactivationRequest objrequest)
        {
            ActDesactLTE.activaDesactivaServiciosProgRequest objActDesactServesRequest = new ActDesactLTE.activaDesactivaServiciosProgRequest();
            ActDesactLTE.parametrosAuditRequest objAuditRequest = new ActDesactLTE.parametrosAuditRequest();
            ActDesactLTE.activaDesactivaServiciosProgResponse objActDesacServicessProgResponse = new ActDesactLTE.activaDesactivaServiciosProgResponse();
            ActDesactLTE.parametrosRequestObjetoRequestOpcional[] lstOptionalRequest = new ActDesactLTE.parametrosRequestObjetoRequestOpcional[1];

            //Auditoria
            objAuditRequest.idTransaccion = objrequest.Audit.Transaction;
            objAuditRequest.ipAplicacion = objrequest.Audit.IPAddress;
            objAuditRequest.nombreAplicacion = objrequest.Audit.ApplicationName;
            objAuditRequest.usuarioAplicacion = objrequest.Audit.UserName;
            objActDesactServesRequest.auditRequest = objAuditRequest;

            //Lista opcional
            lstOptionalRequest[0] = new ActDesactLTE.parametrosRequestObjetoRequestOpcional();

            if (string.IsNullOrEmpty(objrequest.AddServiceAditional.StrDateProgramation))
            {
                objActDesactServesRequest.fechaProg = System.DateTime.Now.ToString("dd/MM/yyyy");
            }
            else
            {
                objActDesactServesRequest.fechaProg = objrequest.AddServiceAditional.StrDateProgramation;
            }

            //Content
            objActDesactServesRequest.fechaProg = objrequest.AddServiceAditional.StrDateProgramation;
            objActDesactServesRequest.fechaReg = DateTime.Now.ToString("dd/MM/yyyy");
            
            objActDesactServesRequest.coId = objrequest.AddServiceAditional.StrCoId;
            objActDesactServesRequest.msisdn = objrequest.AddServiceAditional.StrMsisdn;
            objActDesactServesRequest.customerId = objrequest.AddServiceAditional.StrCustomerId;
            objActDesactServesRequest.coSer = objrequest.AddServiceAditional.StrCoSer;
            objActDesactServesRequest.flagOcc_apadece = objrequest.AddServiceAditional.IntFlagOccPenalty.ToString();
            objActDesactServesRequest.montoFid_apadece = objrequest.AddServiceAditional.DblPenaltyAmount;
            objActDesactServesRequest.nuevoCF = objrequest.AddServiceAditional.DblNewCf;
            objActDesactServesRequest.tipoReg = objrequest.AddServiceAditional.StrTypeRegistry;
            objActDesactServesRequest.cicloFact = Functions.CheckInt16(objrequest.AddServiceAditional.StrCycleFacturation);
            objActDesactServesRequest.codServ = objrequest.AddServiceAditional.StrCodeSer;
            objActDesactServesRequest.descServ = objrequest.AddServiceAditional.StrDescriptioCoSer ?? string.Empty;
            objActDesactServesRequest.nroCuenta = objrequest.AddServiceAditional.StrNroAccoutnt;
            objActDesactServesRequest.usuarioAplicacion = objrequest.Audit.UserName;
            objActDesactServesRequest.usuarioSistema = objrequest.AddServiceAditional.StrUserSystem;

            objActDesactServesRequest.idInteraccion = objrequest.AddServiceAditional.StrInteractionId;
            objActDesactServesRequest.tipoServicio = objrequest.AddServiceAditional.StrTypeSerivice;
            objActDesactServesRequest.listaRequestOpcional = lstOptionalRequest;

            objActDesacServicessProgResponse = Claro.Web.Logging.ExecuteMethod(objrequest.Audit.Session, objrequest.Audit.Transaction, () =>
            {
                return ServiceConfiguration.SiacFixedActivationDesactivacionLte.activaDesactivaServiciosProg(objActDesactServesRequest);
            });

            var objActivationDesactivation = new EntitiesFixed.GetActivationDesactivation.ActivationDesactivationResponse();
            objActivationDesactivation.StrResult = objActDesacServicessProgResponse.auditResponse.codigoRespuesta;
            objActivationDesactivation.StrMessage = objActDesacServicessProgResponse.auditResponse.mensajeRespuesta;

            objActivationDesactivation.StrMessage = string.IsNullOrEmpty(objActivationDesactivation.StrMessage) ? "No hay Errores" : objActivationDesactivation.StrMessage;
            objActivationDesactivation.BlValues = (objActivationDesactivation.StrResult == ConstantsHFC.strCero) ? true : false;

            Web.Logging.Info(strIdSession, strTransaction, String.Format("StrResult: {0},StrMessage: {1},BlValues {2}", objActivationDesactivation.StrResult, objActivationDesactivation.StrMessage,
                objActivationDesactivation.BlValues.ToString()));

            return objActivationDesactivation;
        }

        public static EntitiesFixed.GetComServiceActivation.ComServiceActivationResponse GetComServiceActivation(EntitiesFixed.GetComServiceActivation.ComServiceActivationRequest objrequest)
        {
            ActDesactHFC.ParametroType[] listOptionalRequest = new ActDesactHFC.ParametroType[1];
            listOptionalRequest[0] = new ActDesactHFC.ParametroType();
            listOptionalRequest[0].nombre = KEY.AppSettings("strNombrParametroCodCamapana","");
            listOptionalRequest[0].valor = objrequest.StrCodeBell;
           
            var idTransaccion = objrequest.StrIdTransaction;
            var Message = objrequest.StrMessage;
            
            ActDesactHFC.ParametroType[] listOptionalResponse = new ActDesactHFC.ParametroType[0];
            ActDesactHFC.ParametroType[] listOption = new ActDesactHFC.ParametroType[0];
            
            string strListOptionResponse = Claro.Web.Logging.ExecuteMethod(objrequest.Audit.Session, objrequest.Audit.Transaction, () =>
            {
                return ServiceConfiguration.SiacFixedActivationDesactivacionHfc.activarServiciosAdicionales(
                    ref idTransaccion,
                    objrequest.StrCodeAplication,
                    objrequest.StrIpAplication,
                    objrequest.StrDateProgramation,
                    objrequest.StrDateRegistre,
                    objrequest.StrFlagSearch,
                    objrequest.StrCoId,
                    objrequest.StrCodeCustomer,
                    objrequest.StrProIds,
                    objrequest.StrDatRegistry,
                    objrequest.StrUser,
                    objrequest.StrTelephone,
                    objrequest.StrTypeService,
                    objrequest.StrCoSer,
                    objrequest.StrTypeRegistry,
                    objrequest.StrUserSystem,
                    objrequest.StrUserApp,
                    objrequest.StrEmailUserApp,
                    objrequest.StrDesCoSer,
                    objrequest.StrCodeInteraction,
                    objrequest.StrNroAccount,
                    objrequest.StrCost,
                    listOptionalRequest,
                    out Message,
                    out listOption);

            });
            var model = new EntitiesFixed.GetComServiceActivation.ComServiceActivationResponse();
            model.StrMessage = (string.IsNullOrEmpty(Message)) ? "No hay Errores" : Message;
            model.BlValues = (strListOptionResponse == ConstantsHFC.strCero) ? true : false;

            Web.Logging.Info("S", "AditionalService", String.Format("StrMessage: {0},BlValues: {1}", model.StrMessage, model.BlValues.ToString()));
            return model;
        }

        //ValidarActivaDesactivaServicios
        public static EntitiesFixed.GetValidateActDesService.ValidateActDesServiceResponse GetValidateActDesService(string strIdSession, string strTransaction, EntitiesFixed.GetValidateActDesService.ValidateActDesServiceRequest objrequest)
        {
            ActDesactLTE.validarActivaDesactivaServiciosRequest objaActivaDesactivaServiciosProg = new ActDesactLTE.validarActivaDesactivaServiciosRequest();
            ActDesactLTE.parametrosAuditRequest objAuditRequest = new ActDesactLTE.parametrosAuditRequest();
            ActDesactLTE.parametrosRequestObjetoRequestOpcional[] listaOpcionalRequest = new ActDesactLTE.parametrosRequestObjetoRequestOpcional[1];

            objAuditRequest.idTransaccion = objrequest.Audit.Transaction;
            objAuditRequest.ipAplicacion = objrequest.Audit.IPAddress;
            objAuditRequest.nombreAplicacion = objrequest.Audit.ApplicationName;
            objAuditRequest.usuarioAplicacion = objrequest.Audit.UserName;

            //Lista opcional
            listaOpcionalRequest[0] = new ActDesactLTE.parametrosRequestObjetoRequestOpcional();
            objaActivaDesactivaServiciosProg.auditRequest = objAuditRequest;

            objaActivaDesactivaServiciosProg.msisdn = objrequest.StrMsisdn;
            objaActivaDesactivaServiciosProg.coId = objrequest.StrCodId;
            objaActivaDesactivaServiciosProg.coSer = objrequest.StrCoSer;
            objaActivaDesactivaServiciosProg.tipReg = objrequest.StrTypeRegistre;
            objaActivaDesactivaServiciosProg.codServ = objrequest.StrCodSer;
            objaActivaDesactivaServiciosProg.servcEstado = objrequest.StrStateService.ToString();
            objaActivaDesactivaServiciosProg.listaRequestOpcional = listaOpcionalRequest;

            ActDesactLTE.validarActivaDesactivaServiciosResponse objResServiciosAdicionales = new ActDesactLTE.validarActivaDesactivaServiciosResponse();

            objResServiciosAdicionales = Claro.Web.Logging.ExecuteMethod(objrequest.Audit.Session, objrequest.Audit.Transaction, () =>
            {
                return ServiceConfiguration.SiacFixedActivationDesactivacionLte.validarActivaDesactivaServicios(objaActivaDesactivaServiciosProg);

            });

            ActDesactLTE.parametrosAuditResponse objAuditResponse = objResServiciosAdicionales.auditResponse;

            var response = new EntitiesFixed.GetValidateActDesService.ValidateActDesServiceResponse();
            response.StrResult = objAuditResponse.codigoRespuesta;
            response.StrMsg = objAuditResponse.mensajeRespuesta;

            response.StrMsg = response.StrMsg ?? "No hay Errores";
            Web.Logging.Info("S", "AditionalService", String.Format("StrResult: {0},StrMsg: {1}", response.StrResult, response.StrMsg));
            if (response.StrResult == ConstantsHFC.strCero || response.StrResult == ConstantsHFC.strUno)
            { 
                response.BlValues = true;
                return response;
            }
            else
            {
                response.BlValues = false;
                return response;
            }

        }

        public static EntitiesFixed.GetPlanCommercial.PlanCommercialResponse GetPlanCommercial(string strIdSession, string strTransaction, EntitiesFixed.GetPlanCommercial.PlanCommercialRequest item)
        {
            var model = new EntitiesFixed.GetPlanCommercial.PlanCommercialResponse();
            DbParameter[] parameters = 
            {
                new DbParameter("PI_COID", DbType.Int32, ParameterDirection.Input, item.StrContractId), 
                new DbParameter("PI_SNCODE", DbType.Int32, ParameterDirection.Input, item.StrCodService),
                new DbParameter("PI_ESTADO", DbType.Int32, ParameterDirection.Input, item.StrState),
                new DbParameter("PI_TIPROD", DbType.String,255, ParameterDirection.Input, item.StrTypeProduct),   
                new DbParameter("PO_CODE_RESP", DbType.Int32, ParameterDirection.Output),
                new DbParameter("PO_MSG_RESP", DbType.String,255, ParameterDirection.Output)
            };
            try
            {
                Web.Logging.ExecuteMethod(strIdSession, strTransaction, () =>
                {
                    DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_SP_REG_PLAN_COMERCIAL, parameters);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
            }
            finally
            {
                model.StrResult = parameters[parameters.Length - 2].Value.ToString();
                model.StrMessage = parameters[parameters.Length - 1].Value.ToString();
            }
            Web.Logging.Info("S", "AditionalService", String.Format("StrResult: {0},StrMessage: {1}", model.StrResult, model.StrMessage));
            return model;
        }

        public static EntitiesFixed.GetComServiceActivation.ComServiceActivationResponse GetComServiceDesactivation(EntitiesFixed.GetComServiceActivation.ComServiceActivationRequest oComServiceActivationRequest)
        {
            ActDesactHFC.ParametroType[] ListOpcionalRequest = new ActDesactHFC.ParametroType[0];
            ActDesactHFC.ParametroType[] ListOpcionalResponse = new ActDesactHFC.ParametroType[0];

            var idTransaccion = oComServiceActivationRequest.StrIdTransaction;
            var Message = oComServiceActivationRequest.StrMessage;

            string strListOptionResponse = Claro.Web.Logging.ExecuteMethod(oComServiceActivationRequest.Audit.Session, oComServiceActivationRequest.Audit.Transaction, () =>
            {
                return ServiceConfiguration.SiacFixedActivationDesactivacionHfc.desactivarServiciosAdicionales( 
                    ref idTransaccion,
                    oComServiceActivationRequest.StrCodeAplication,
                    oComServiceActivationRequest.StrIpAplication,
                    oComServiceActivationRequest.StrDateProgramation,
                    oComServiceActivationRequest.StrDateRegistre,
                    oComServiceActivationRequest.StrFlagSearch,
                    oComServiceActivationRequest.StrFlagOccPenalty,
                    oComServiceActivationRequest.strPenalty,
                    oComServiceActivationRequest.strAmountFIdPenalty,
                    oComServiceActivationRequest.strNewCF,
                    oComServiceActivationRequest.strBillingCycle,
                    oComServiceActivationRequest.strTicklerCode,
                    oComServiceActivationRequest.StrCoId,
                    oComServiceActivationRequest.StrCodeCustomer,
                    oComServiceActivationRequest.StrProIds,
                    oComServiceActivationRequest.StrDatRegistry,
                    oComServiceActivationRequest.StrUser,
                    oComServiceActivationRequest.strInteraction,
                    oComServiceActivationRequest.StrTelephone,
                    oComServiceActivationRequest.StrTypeService,
                    oComServiceActivationRequest.StrCoSer,
                    oComServiceActivationRequest.StrTypeRegistry,
                    oComServiceActivationRequest.StrUserSystem,
                    oComServiceActivationRequest.StrUserApp,
                    oComServiceActivationRequest.StrEmailUserApp,
                    oComServiceActivationRequest.StrDesCoSer,
                     oComServiceActivationRequest.StrCodeInteraction,
                    oComServiceActivationRequest.StrNroAccount,
                    ListOpcionalRequest,
                    out Message,
                    out ListOpcionalResponse);

            });


            var model = new EntitiesFixed.GetComServiceActivation.ComServiceActivationResponse();
            model.StrMessage = (string.IsNullOrEmpty(Message)) ? "No hay Errores" : Message;
            model.BlValues = (strListOptionResponse == ConstantsHFC.strCero) ? true : false;

            Web.Logging.Info("S", "AditionalService", String.Format("StrMessage: {0},BlValues: {1}", model.StrMessage, model.BlValues.ToString()));
            return model;
             
        }

        public static string GetProdIdTraDesacServ(string strIdSession, string strTransaction, string vstrIdentificador, string vstrCoId, bool vod, bool match)
        {
            DbParameter[] parameters = 
                {
                    new DbParameter("P_CO_ID", DbType.Int64,255, ParameterDirection.Input, vstrCoId), 
                    new DbParameter("P_CURSOR_CTV", DbType.Object, ParameterDirection.Output),
                    new DbParameter("P_CURSOR_INT", DbType.Object, ParameterDirection.Output),
                    new DbParameter("P_CURSOR_TLF", DbType.Object, ParameterDirection.Output),
                    new DbParameter("P_RESULTADO", DbType.Int64, 255, ParameterDirection.Output),
                    new DbParameter("P_MSGERR", DbType.String, 255, ParameterDirection.Output)
                };

            int iCount = 0;
            string ProductIdMax = string.Empty;
            double dbProdm = 0;
            string strMacAddress = string.Empty;
            string strProdTeamId = string.Empty;
            int iCountProd = 0;
            List<EntitiesFixed.GenericItem> lstGenericItem = new List<EntitiesFixed.GenericItem>();
            try
            {

                Web.Logging.ExecuteMethod(strIdSession, strTransaction, () =>
                {
                    DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS,
                        DbCommandConfiguration.SIACU_SP_PROD_X_SERV_CO_ID, parameters, (IDataReader reader) =>
                        {

                            EntitiesFixed.GenericItem oGenericItem = null;
                            while (reader.Read())
                            {
                                oGenericItem = new EntitiesFixed.GenericItem();
                                oGenericItem.Descripcion = Convert.ToString(reader["VALOR"]);
                                oGenericItem.Descripcion2 = Convert.ToString(reader["ID_PRODUCTO"]);
                                oGenericItem.Agrupador = Convert.ToString(reader["TIPO_SERV"]);
                                oGenericItem.Estado = Convert.ToString(reader["ESTADO"]);
                                oGenericItem.Condicion = Convert.ToString(reader["SERIAL_NUMBER"]);
                                lstGenericItem.Add(oGenericItem);
                                iCount++;
                            }
                        });
                });

                if (lstGenericItem != null)
                {
                    Web.Logging.Info("S", "AditionalService", String.Format("lstGenericItem.Count: {0}, vstrCoId:{1}, vod:{2}, match:{3}.", lstGenericItem.Count, vstrCoId, vod.ToString(), match.ToString()));
                    if (lstGenericItem.Count > 0)
                    {

                        foreach (var item in lstGenericItem)
                        {
                            if (match)
                            {
                                if (vod)
                                {
                                    if (item.Agrupador.Equals(ConstantsHFC.ADDITIONALSERVICESHFC.gstrServVOD) && item.Estado.Equals(ConstantsHFC.strLetraA))
                                    {

                                        if (ProductIdMax.Equals(string.Empty))
                                        {
                                            ProductIdMax = item.Descripcion2;
                                        }
                                        else
                                        {
                                            ProductIdMax = ProductIdMax + "|" + item.Descripcion2;
                                        }
                                    }


                                }
                                else
                                {
                                    if (item.Descripcion ==vstrIdentificador && item.Estado.Equals(ConstantsHFC.strLetraA) && item.Agrupador.Equals(ConstantsHFC.ADDITIONALSERVICESHFC.gstrServCANAL))
                                    {
                                        if (ProductIdMax.Equals(string.Empty))
                                        {
                                            ProductIdMax = item.Descripcion2;
                                        }
                                        else
                                        {
                                            ProductIdMax = ProductIdMax + "|" + item.Descripcion2;
                                        }
                                    }
                                }
                            }
                            else
                            {

                                if (strMacAddress.IndexOf(item.Condicion) == -1 && item.Estado.Equals(ConstantsHFC.strLetraA) && item.Agrupador.Equals("CABLE_TV"))
                                {
                                    iCountProd = iCountProd + 1;
                                    strMacAddress = strMacAddress + ";" + item.Condicion;
                                }
                                if (item.Agrupador.Equals("CABLE_TV"))
                                {
                                    strProdTeamId = item.Descripcion2;
                                }

                                if (dbProdm < double.Parse(item.Descripcion2) && (item.Agrupador.Equals("CANAL") || item.Agrupador.Equals("VOD") || item.Agrupador.Equals("VOD")))
                                {
                                    ProductIdMax = item.Descripcion2;
                                    dbProdm = double.Parse(item.Descripcion2);
                                }

                            }
                        }



                    }
                }

                if (!match)
                { 
                    string strAux = ProductIdMax;
                    bool bLogicSinServices = false;
                    Web.Logging.Info("S", "AditionalService", String.Format("strAux: {0}, iCountProd: {1} ", strAux, iCountProd.ToString()));
                    if (strAux.Equals(string.Empty) && !ProductIdMax.Equals(string.Empty))
                    {
                        bLogicSinServices = true;
                    }
                    ProductIdMax = string.Empty;
                    for (int i = 0; i < iCountProd; i++)
                    {
                        if (!bLogicSinServices)
                        {
                            if (i.Equals(0))
                            {
                                ProductIdMax = (Int64.Parse(strAux) + 1).ToString();
                            }
                            else
                            {
                                ProductIdMax = ProductIdMax + "|" + (Int64.Parse(strAux) + 1).ToString();
                            }
                        }
                        else
                        {
                            if (i.Equals(0))
                            {
                                ProductIdMax = strProdTeamId + ConstantsHFC.strUno;
                            }
                            else
                            {
                                ProductIdMax = ProductIdMax + "|" + strProdTeamId + ConstantsHFC.strUno;
                            }
                        }
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
            finally {

                //rCodigoPlan = parameters[parameters.Length - 3].Value.ToString();
                //rintCodigoError = Convert.ToInt(parameters[parameters.Length - 2].Value.ToString());
                //rstrDescripcionError = parameters[parameters.Length - 1].Value.ToString();
            }
            return ProductIdMax;
        }
    }
}
