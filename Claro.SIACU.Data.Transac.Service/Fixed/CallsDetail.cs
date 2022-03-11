using System;
using System.Collections.Generic;
using System.Data;
using Claro.Data;
using Claro.SIACU.Data.Transac.Service.Configuration;
using Claro.SIACU.Transac.Service;
using EntitiesFixed = Claro.SIACU.Entity.Transac.Service.Fixed;
using ConstantsHFC = Claro.SIACU.Transac.Service.Constants;
using BpelCallDetail = Claro.SIACU.ProxyService.Transac.Service.SIACFixedBpelCallDetail;
using Claro.SIACU.Entity.Transac.Service.Postpaid;
using System.Reflection;
using System.ServiceModel;
using Claro.SIACU.Entity.Transac.Service.Fixed.GetCallDetailInputFixed;
using System.Net;
using Claro.Web;
using Claro.SIACU.Entity.Transac.Service.Fixed.GetBpelCallDetail;

namespace Claro.SIACU.Data.Transac.Service.Fixed
{
    public class CallsDetail : GenericDataMethods
    {
        //Name: ObtenerTelefonosCliente DAConsultas
        public static List<EntitiesFixed.GenericItem> GetCustomerPhone(string strIdSession, string strTransaction, int idContract)
        {
            List<EntitiesFixed.GenericItem> salida = new List<EntitiesFixed.GenericItem>();
            DbParameter[] parameters =
            {   new DbParameter("P_CO_ID", DbType.Int64, 255,ParameterDirection.Input, idContract),
                new DbParameter("P_CURSOR", DbType.Object, ParameterDirection.Output),
                new DbParameter("P_RESULTADO", DbType.Int64, 255,ParameterDirection.Output),
                new DbParameter("P_MSGERR", DbType.String, 255,ParameterDirection.Output)             
            };

            try
            {
                Logging.ExecuteMethod(strIdSession, strTransaction, () =>
                {
                    DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS,
                        DbCommandConfiguration.SIACU_POST_BSCS_SP_LISTA_TELEFONO, parameters, reader =>
                        {
                            while (reader.Read())
                            {
                                var item = new EntitiesFixed.GenericItem
                                {
                                    Descripcion = reader[0] != null ? reader[0].ToString() : string.Empty
                                };

                                salida.Add(item);
                            }
                        });
                });

            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, strTransaction, ex.Message);
            }

            return salida;
        }

        //Name ListarDetalleLlamadasDB1 DALlamada
        public static List<EntitiesFixed.PhoneCall> GetCallDetailDB1(string strIdSession, string strTransaction, string vCONTRATOID, string vFECHA_INI, string vFECHA_FIN, string vSeguridad, ref string vTOTAL, ref string MSGERROR)
        {
            var salida = new List<EntitiesFixed.PhoneCall>();
            double Total = ConstantsHFC.zero;
            double TotalMIN = ConstantsHFC.zero;
            double TotalSMS = ConstantsHFC.zero;
            double TotalMMS = ConstantsHFC.zero;
            double TotalGPRS = ConstantsHFC.zero;
            int i = ConstantsHFC.zero;
            string Conex = string.Empty;

            DbParameter[] parameters = 
            {
                new DbParameter("p_tipo_consulta", DbType.String, 255, ParameterDirection.Input, "C"),
                new DbParameter("p_valor", DbType.Int64, 255, ParameterDirection.Input, vCONTRATOID),
                new DbParameter("p_fecha_ini", DbType.String, 255,ParameterDirection.Input, vFECHA_INI),
                new DbParameter("p_fecha_fin", DbType.String, 255, ParameterDirection.Input, vFECHA_FIN),
                new DbParameter("p_cursor", DbType.Object, ParameterDirection.Output)
            };

            Logging.ExecuteMethod(strIdSession, strTransaction, () =>
            {
                DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS,
                    DbCommandConfiguration.SIACU_POST_BSCS_SP_DET_LLAMADA, parameters, reader =>
                    {
                        while (reader.Read())
                        {
                            var item = new EntitiesFixed.PhoneCall();
                            i++;
                            item.NroRegistro = i;
                            item.VlrNumber = Functions.CheckStr(i);
                            string strFecha = Functions.CheckStr(reader["FECHA_HORA_INICIO"]);
                            item.Fecha_Hora_Inicio = Functions.CheckDate(reader["FECHA_HORA_INICIO"]);
                            item.Fecha = strFecha.Substring(0, 10);
                            item.Hora = strFecha.Substring(11);
                            item.Cantidad = Functions.CheckStr(reader["CANTIDAD"]);
                            item.Cargo_Final = Functions.CheckStr(reader["CARGO_FINAL"]);
                            item.Cargo_Ori_Flexible = Functions.CheckStr(reader["CARGO_ORI_FLEXIBLE"]);
                            item.Cargo_Original = Functions.CheckStr(reader["CARGO_ORIGINAL"]);
                            item.Horario = Functions.CheckStr(reader["HORARIO"]);
                            item.Plan_Tarifario = Functions.CheckStr(reader["PLAN_TARIFARIO"]);
                            item.Operador = Functions.CheckStr(reader["OPERADOR"]);
                            item.Tipo = Functions.CheckStr(reader["TIPO"]);
                            if ((item.Tipo.ToUpper() == "LLAMADA-SALIENTE") || (item.Tipo.ToUpper() == "SMS-SALIENTE"))
                            {
                                if (vSeguridad == ConstantsHFC.strUno)
                                { item.Telefono_Destino = Functions.CheckStr(reader["TELEFONO_DESTINO"]); }
                                else
                                { item.Telefono_Destino = Functions.SeguridadFormatTelf(Functions.CheckStr(reader["TELEFONO_DESTINO"])); }
                            }
                            else
                            {
                                item.Telefono_Destino = Functions.CheckStr(reader["TELEFONO_DESTINO"]);
                            }

                            item.Tipo_Llamada = Functions.CheckStr(reader["TIPO_LLAMADA"]);
                            item.Tarifa = Functions.CheckStr(reader["TARIFA"]);
                            item.Zona_Tarifaria = Functions.CheckStr(reader["ZONA_TARIFARIA"]);
                            if (item.Zona_Tarifaria.Trim() != String.Empty)
                            {
                                if (item.Tipo.ToUpper().IndexOf("LLAMADA") != -1)
                                {
                                    TotalMIN = TotalMIN + Functions.CheckDbl(item.Cantidad);
                                }
                                else
                                {
                                    if (item.Tipo.ToUpper().IndexOf("SMS") != -1)
                                    {
                                        TotalSMS = TotalSMS + Functions.CheckDbl(item.Cantidad);
                                    }
                                    else
                                    {
                                        if (item.Tipo.ToUpper().IndexOf("MMS") != -1)
                                        {
                                            TotalMMS = TotalMMS + Functions.CheckDbl(item.Cantidad);
                                        }
                                        else
                                        {
                                            if (item.Tipo.ToUpper().IndexOf("GPRS") != -1)
                                            {
                                                if (Functions.CheckDbl(item.Cantidad) % 1024 == 0)
                                                    TotalGPRS = TotalGPRS + Functions.CheckDbl(item.Cantidad) / 1024;
                                                else
                                                    TotalGPRS = TotalGPRS + (Functions.CheckDbl(item.Cantidad) / 1024 + 1);
                                            }
                                        }
                                    }
                                }
                            }
                            if (item.Tipo.ToUpper().IndexOf("LLAMADA") != -1)
                            {
                                item.Cantidad = Functions.GetFormatHHMMSS(Functions.CheckInt64(item.Cantidad));
                            }
                            Total = Total + Functions.CheckDbl(item.Cargo_Final);
                            salida.Add(item);
                        }
                    });
            });

            vTOTAL = Functions.CheckStr(Total) + ";" + Functions.CheckStr(TotalMIN) + ";" + Functions.CheckStr(TotalSMS) + ";" + Functions.CheckStr(TotalMMS) + ";" + Functions.CheckStr(TotalGPRS);

            return salida;
        }

        //Name ListarDetalleLlamadas DALlamada
        //public static List<EntitiesFixed.PhoneCall> GetCallDetail(Entity.Transac.Service.Fixed.GetBpelCallDetail.BpelCallDetailRequest objRequest)
        public static EntitiesFixed.GetCallDetail.CallDetailResponse GetCallDetail(BpelCallDetailRequest objRequest)
        {
            var responseFinal = new EntitiesFixed.GetCallDetail.CallDetailResponse
            {
                StrResponseCode = "1",
                StrResponseMessage = string.Empty
            };

            var salida = new List<EntitiesFixed.PhoneCall>();
            var vSeguridad = "1";
            double Total = ConstantsHFC.zero;
            double TotalMIN = ConstantsHFC.zero;
            double TotalSMS = ConstantsHFC.zero;
            double TotalMMS = ConstantsHFC.zero;
            double TotalGPRS = ConstantsHFC.zero;

            var objRequestHeader = new BpelCallDetail.HeaderRequestType
            {
                canal = objRequest.HeaderRequestTypeBpel.Canal,
                idAplicacion = objRequest.HeaderRequestTypeBpel.IdAplicacion,//OPCIONAL
                usuarioAplicacion = objRequest.HeaderRequestTypeBpel.UsuarioAplicacion,//OPCIONAL
                usuarioSesion = objRequest.HeaderRequestTypeBpel.UsuarioSesion,//OPCIONAL
                idTransaccionESB = objRequest.HeaderRequestTypeBpel.IdTransaccionEsb,//OPCIONAL
                idTransaccionNegocio = objRequest.HeaderRequestTypeBpel.IdTransaccionNegocio, //strTransaction,
                fechaInicio = DateTime.Now,
                nodoAdicional = ConstantsHFC.strUno
            };

            var contactUser = new BpelCallDetail.ContactUser
            {
                usuario = objRequest.DetalleLlamadasRequestBpel.ContactUserBpel.Usuario ?? string.Empty,
                nombres = objRequest.DetalleLlamadasRequestBpel.ContactUserBpel.Nombres ?? string.Empty,
                apellidos = objRequest.DetalleLlamadasRequestBpel.ContactUserBpel.Apellidos ?? string.Empty,
                razonSocial = objRequest.DetalleLlamadasRequestBpel.ContactUserBpel.RazonSocial ?? string.Empty,
                tipoDoc = objRequest.DetalleLlamadasRequestBpel.ContactUserBpel.TipoDoc ?? string.Empty,
                numDoc = objRequest.DetalleLlamadasRequestBpel.ContactUserBpel.NumDoc ?? string.Empty,
                domicilio = objRequest.DetalleLlamadasRequestBpel.ContactUserBpel.Domicilio ?? string.Empty,
                distrito = objRequest.DetalleLlamadasRequestBpel.ContactUserBpel.Distrito ?? string.Empty,
                departamento = objRequest.DetalleLlamadasRequestBpel.ContactUserBpel.Departamento ?? string.Empty,
                provincia = objRequest.DetalleLlamadasRequestBpel.ContactUserBpel.Provincia ?? string.Empty,
                modalidad = objRequest.DetalleLlamadasRequestBpel.ContactUserBpel.Modalidad ?? string.Empty,
            };

            var customerClfy = new BpelCallDetail.CustomerClfy
            {
                account = objRequest.DetalleLlamadasRequestBpel.CustomerClfyBpel.Account ?? string.Empty,
                contactObjId = objRequest.DetalleLlamadasRequestBpel.CustomerClfyBpel.ContactObjId ?? string.Empty,
                flagReg = objRequest.DetalleLlamadasRequestBpel.CustomerClfyBpel.FlagReg ?? string.Empty,
            };

            var interact = new BpelCallDetail.Interact
            {
                phone = objRequest.DetalleLlamadasRequestBpel.InteractionBpel.Phone ?? string.Empty,
                tipo = objRequest.DetalleLlamadasRequestBpel.InteractionBpel.Tipo ?? string.Empty,
                clase = objRequest.DetalleLlamadasRequestBpel.InteractionBpel.Clase ?? string.Empty,
                subclase = objRequest.DetalleLlamadasRequestBpel.InteractionBpel.Subclase ?? string.Empty,
                tipoInter = objRequest.DetalleLlamadasRequestBpel.InteractionBpel.TipoInter ?? string.Empty,
                metodoContacto = objRequest.DetalleLlamadasRequestBpel.InteractionBpel.MetodoContacto ?? string.Empty,
                resultado = objRequest.DetalleLlamadasRequestBpel.InteractionBpel.Resultado ?? string.Empty,
                hechoEnUno = objRequest.DetalleLlamadasRequestBpel.InteractionBpel.HechoEnUno ?? string.Empty,
                notas = objRequest.DetalleLlamadasRequestBpel.InteractionBpel.Notas ?? string.Empty,
                flagCaso = objRequest.DetalleLlamadasRequestBpel.InteractionBpel.FlagCaso ?? string.Empty,
                usrProceso = objRequest.DetalleLlamadasRequestBpel.InteractionBpel.UsrProceso ?? string.Empty,
                coId = objRequest.DetalleLlamadasRequestBpel.InteractionBpel.CoId ?? string.Empty,
                codPlano = objRequest.DetalleLlamadasRequestBpel.InteractionBpel.CodPlano ?? string.Empty,
                agente = objRequest.DetalleLlamadasRequestBpel.InteractionBpel.Agente
            };

            foreach (var prop in interact.GetType().GetProperties())
            {
                if (prop.GetValue(interact, null) == null)
                {
                    prop.SetValue(interact, string.Empty);
                }
            }

            var interactPlus = new BpelCallDetail.InteractPlus
            {
                claroNumber = objRequest.DetalleLlamadasRequestBpel.InteractionPlusBpel.ClaroNumber,
                documentNumber = objRequest.DetalleLlamadasRequestBpel.InteractionPlusBpel.DocumentNumber,
                firstName = objRequest.DetalleLlamadasRequestBpel.InteractionPlusBpel.FirstName,
                lastName = objRequest.DetalleLlamadasRequestBpel.InteractionPlusBpel.LastName,
                nameLegalRep = objRequest.DetalleLlamadasRequestBpel.InteractionPlusBpel.NameLegalRep,
                dniLegalRep = objRequest.DetalleLlamadasRequestBpel.InteractionPlusBpel.DniLegalRep,
                flagRegistered = objRequest.DetalleLlamadasRequestBpel.InteractionPlusBpel.FlagRegistered,
                birthday = objRequest.DetalleLlamadasRequestBpel.InteractionPlusBpel.Birthday,
                expireDate = objRequest.DetalleLlamadasRequestBpel.InteractionPlusBpel.ExpireDate,
                inter20 = objRequest.DetalleLlamadasRequestBpel.InteractionPlusBpel.Inter20,
                inter21 = objRequest.DetalleLlamadasRequestBpel.InteractionPlusBpel.Inter21,
                inter30 = objRequest.DetalleLlamadasRequestBpel.InteractionBpel.Notas ?? string.Empty
            };

            foreach (var prop in interactPlus.GetType().GetProperties())
            {
                if (prop.GetValue(interactPlus, null) == null)
                {
                    prop.SetValue(interactPlus, string.Empty);
                }
            }

            var objDetalleLlamadaRequest = new BpelCallDetail.detalleLlamadasRequest
            {
                tipoConsulta = objRequest.DetalleLlamadasRequestBpel.TipoConsulta ?? string.Empty,
                msisdn = objRequest.DetalleLlamadasRequestBpel.Msisdn ?? string.Empty,
                fechaInicio = objRequest.DetalleLlamadasRequestBpel.FechaInicio ?? string.Empty,
                fechaFin = objRequest.DetalleLlamadasRequestBpel.FechaFin ?? string.Empty,
                contactUser = contactUser,
                customerClfy = customerClfy,
                interact = interact,
                interactPlus = interactPlus,
                flagConstancia = objRequest.DetalleLlamadasRequestBpel.FlagConstancia ?? string.Empty,
                ipCliente = objRequest.DetalleLlamadasRequestBpel.IpCliente ?? string.Empty,
                tipoConsultaContrato = objRequest.DetalleLlamadasRequestBpel.TipoConsultaContrato ?? string.Empty,
                valorContrato = objRequest.DetalleLlamadasRequestBpel.ValorContrato ?? string.Empty,
                flagContingencia = objRequest.DetalleLlamadasRequestBpel.FlagContingencia ?? string.Empty,
                codigoCliente = objRequest.DetalleLlamadasRequestBpel.CodigoCliente ?? string.Empty,
                flagEnvioCorreo = objRequest.DetalleLlamadasRequestBpel.FlagEnvioCorreo ?? string.Empty,
                flagGenerarOcc = objRequest.DetalleLlamadasRequestBpel.FlagGenerarOcc ?? string.Empty,
                invoiceNumber = objRequest.DetalleLlamadasRequestBpel.InvoiceNumber ?? string.Empty,
                periodo = objRequest.DetalleLlamadasRequestBpel.Periodo ?? string.Empty,
                tipoProducto = objRequest.DetalleLlamadasRequestBpel.TipoProducto ?? string.Empty
            };

            var objResponseBody = new BpelCallDetail.detalleLlamadasResponse();

            try
            {               

                var objResponseHeader = Logging.ExecuteMethod<BpelCallDetail.HeaderResponseType>(objRequest.Audit.Session, objRequest.Audit.Transaction,
                Configuration.ServiceConfiguration.FIXED_BPEL_CALLDETAIL, () =>
                {
                    return Configuration.ServiceConfiguration.FIXED_BPEL_CALLDETAIL.consultarDetalleLlamadas(objRequestHeader, objDetalleLlamadaRequest, out objResponseBody);
                });

                Logging.Info(objRequest.StrIdSession, objRequest.StrTransaction, "BPEL SUCCESS:" + objResponseBody.responseStatus.codigoRespuesta + "-" + objResponseBody.responseStatus.descripcionRespuesta + "-" + objResponseBody.responseStatus.estado + "-" + objResponseBody.responseStatus.origen + "-" + objResponseBody.responseStatus.ubicacionError);

                if (objResponseBody != null)
                {
                    if (objResponseBody.responseData != null)
                    {
                        var objModelData = objResponseBody.responseData;
                        if (objModelData.listaDetalleLlamadasResponse != null)
                        {
                            var objModelLstLlamadas = objModelData.listaDetalleLlamadasResponse;
                            if (objModelData.listaDetalleLlamadasResponse.detLlamadaSalienteNoFact.Length > 0)
                            {
                                var objModelLstNoFac = objModelData.listaDetalleLlamadasResponse.detLlamadaSalienteNoFact;
                                var i = 0;
                                foreach (var obj in objModelLstNoFac)
                                {
                                    var item = new EntitiesFixed.PhoneCall();
                                    i++;
                                    item.NroRegistro = i;
                                    item.VlrNumber = Functions.CheckStr(i);
                                    string strFecha = Functions.CheckStr(obj.fechaHoraInicio);
                                    item.Fecha_Hora_Inicio = Functions.CheckDate(obj.fechaHoraInicio);
                                    item.Fecha = strFecha.Substring(0, 10);
                                    item.Hora = strFecha.Substring(11);
                                    item.Cantidad = Functions.CheckStr(obj.cantidad);
                                    item.Cargo_Final = Functions.CheckStr(obj.cargoFinal);
                                    item.Cargo_Ori_Flexible = Functions.CheckStr(obj.cargoOriFlexible);
                                    item.Cargo_Original = Functions.CheckStr(obj.cargoOriginal);
                                    item.Horario = Functions.CheckStr(obj.horario);
                                    item.Plan_Tarifario = Functions.CheckStr(obj.planTarifario);
                                    item.Operador = Functions.CheckStr(obj.operador);
                                    item.Tipo = Functions.CheckStr(obj.tipo);
                                    item.Telefono_Origen = Functions.CheckStr(obj.numeroOrigen);

                                    if (!string.IsNullOrEmpty(item.Telefono_Origen))
                                    {
                                    var strValor = item.Telefono_Origen.Substring(0, 2);
                                    if (strValor == "01")
                                    {
                                        item.Telefono_Origen = item.Telefono_Origen.Substring(2, item.Telefono_Origen.Length - 2);
                                    }
                                    }

                                    if ((item.Tipo.ToUpper() == "LLAMADA-SALIENTE") || (item.Tipo.ToUpper() == "SMS-SALIENTE"))
                                    {
                                        if (vSeguridad == ConstantsHFC.strUno)
                                        { item.Telefono_Destino = Functions.CheckStr(obj.telefonoDestino); }
                                        else
                                        { item.Telefono_Destino = Functions.SeguridadFormatTelf(Functions.CheckStr(obj.telefonoDestino)); }
                                    }
                                    else
                                    {
                                        item.Telefono_Destino = Functions.CheckStr(obj.telefonoDestino);
                                    }
                                    item.Tipo_Llamada = Functions.CheckStr(obj.tipoLlamada);
                                    item.Tarifa = Functions.CheckStr(obj.tarifa);
                                    item.Zona_Tarifaria = Functions.CheckStr(obj.zonaTarifaria);
                                    if (item.Zona_Tarifaria.Trim() != string.Empty)
                                    {
                                        if (item.Tipo.ToUpper().IndexOf("LLAMADA") != -1)
                                        {
                                            TotalMIN = TotalMIN + Functions.CheckDbl(item.Cantidad);
                                        }
                                        else
                                        {
                                            if (item.Tipo.ToUpper().IndexOf("SMS") != -1)
                                            {
                                                TotalSMS = TotalSMS + Functions.CheckDbl(item.Cantidad);
                                            }
                                            else
                                            {
                                                if (item.Tipo.ToUpper().IndexOf("MMS") != -1)
                                                {
                                                    TotalMMS = TotalMMS + Functions.CheckDbl(item.Cantidad);
                                                }
                                                else
                                                {
                                                    if (item.Tipo.ToUpper().IndexOf("GPRS") != -1)
                                                    {
                                                        if (Functions.CheckDbl(item.Cantidad) % 1024 == 0)
                                                            TotalGPRS = TotalGPRS + Functions.CheckDbl(item.Cantidad) / 1024;
                                                        else
                                                            TotalGPRS = TotalGPRS + (Functions.CheckDbl(item.Cantidad) / 1024 + 1);
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    if (item.Tipo.ToUpper().IndexOf("LLAMADA") != -1)
                                    {
                                        item.Cantidad = Functions.GetFormatHHMMSS(Functions.CheckInt64(item.Cantidad));
                                    }

                                    Total = Total + Functions.CheckDbl(item.Cargo_Final);
                                    salida.Add(item);
                                }
                            }
                        }
                    }
                }
            }
            catch (FaultException<BpelCallDetail.ClaroFault> ex)
            {
                responseFinal.StrResponseCode = ConstantsHFC.strMenosUno;
                responseFinal.StrResponseMessage = ConfigurationManager.AppSettings("strMsgErrorFaultBPel");
                Logging.Error(objRequest.StrIdSession, objRequest.StrTransaction, "BPEL1:" + ex.Detail.codigoError + "-" + ex.Detail.descripcionError + "-" + ex.Detail.ubicacionError);
            }
            catch (FaultException<ExceptionDetail> ex)
            {
                responseFinal.StrResponseCode = ConstantsHFC.strMenosUno;
                responseFinal.StrResponseMessage = ConfigurationManager.AppSettings("strMsgErrorFaultBPel"); ;
                Logging.Error(objRequest.StrIdSession, objRequest.StrTransaction, "BPEL2:" + ex.Detail.Message + "-" + ex.Detail.StackTrace);
            }
            catch (FaultException ex)
            {
                responseFinal.StrResponseCode = ConstantsHFC.strMenosUno;
                responseFinal.StrResponseMessage = ConfigurationManager.AppSettings("strMsgErrorFaultBPel"); ;
                Logging.Error(objRequest.StrIdSession, objRequest.StrTransaction, "BPEL3:" + ex.Message + "-" + ex.Action);
            }
            catch (Exception ex)
            {
                responseFinal.StrResponseCode = ConstantsHFC.strMenosUno;
                responseFinal.StrResponseMessage = ConfigurationManager.AppSettings("strMsgErrorFaultBPel"); ;
                LogException(objRequest.StrIdSession, objRequest.StrTransaction, "BPEL4:", ex);
            }

            responseFinal.LstPhoneCall = salida;

            return responseFinal;
        }

        public static List<EntitiesFixed.GenericItem> GetFacturePDI(string strIdSession, string strTransaction, string strCodeCustomer)
        {
            List<EntitiesFixed.GenericItem> salida = new List<EntitiesFixed.GenericItem>();
            DbParameter[] parameters = 
            {
                new DbParameter("K_CODIGOCLIENTE", DbType.String, 24, ParameterDirection.Input, strCodeCustomer),
                new DbParameter("K_ERRMSG", DbType.String, ParameterDirection.Output),
                new DbParameter("K_LISTA", DbType.Object,ParameterDirection.Output),

            };
            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_PDI,
                DbCommandConfiguration.SIACU_TOLS_OBTENERDATOSDECUENTA, parameters,
                (IDataReader reader) =>
                {
                    while (reader.Read())
                    {
                        salida.Add(new EntitiesFixed.GenericItem
                        {
                            Codigo = Functions.CheckStr(reader["InvoiceNumber"]) + "$" + Functions.CheckStr(reader["FechaInicio"]) + "$" + Functions.CheckStr(reader["FechaFin"]),
                            Descripcion = Functions.CheckStr(reader["FECHAEMISION"]),
                            Codigo2 = Functions.CheckStr(reader["Periodo"])
                        });
                    }
                });

            return salida;
        }

        public static List<EntitiesFixed.GenericItem> GetFactureDBTO(string strIdSession, string strTransaction, string strCodeCustomer)
        {
            List<EntitiesFixed.GenericItem> salida = new List<EntitiesFixed.GenericItem>();
            DbParameter[] parameters = 
            {
                new DbParameter("K_CODIGOCLIENTE", DbType.String, 24, ParameterDirection.Input, strCodeCustomer),
                new DbParameter("K_ERRMSG", DbType.String, ParameterDirection.Output),
                new DbParameter("K_LISTA", DbType.Object,ParameterDirection.Output),

            };
            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DBTO,
                DbCommandConfiguration.SIACU_TOLS_OBTENERDATOSDECUENTA, parameters,
                (IDataReader dr) =>
                {
                    while (dr.Read())
                    {
                        salida.Add(new EntitiesFixed.GenericItem
                        {
                            Codigo = Functions.CheckStr(dr["InvoiceNumber"]) + "$" + Functions.CheckStr(dr["FechaInicio"]) + "$" + Functions.CheckStr(dr["FechaFin"]),
                            Descripcion = Functions.CheckStr(dr["FECHAEMISION"]),
                            Codigo2 = Functions.CheckStr(dr["Periodo"])
                        });
                    }
                });

            return salida;
        }

        //Name ListarDetalleLlamadasDB1_BSCS DALlamada
        public static List<EntitiesFixed.PhoneCall> GetBilledCallsDetailDB1_BSCS(string strIdSession, string strTransaction, string vCONTRATOID, string vFECHA_INI, string vFECHA_FIN, string vFECHA_AYER, string vFECHA_AHORA, string vSeguridad, ref string vTOTAL)
        {
            var salida = new List<EntitiesFixed.PhoneCall>();
            double Total = ConstantsHFC.zero;
            double Total2 = ConstantsHFC.zero;
            double TotalMIN = ConstantsHFC.zero;
            double TotalSMS = ConstantsHFC.zero;
            double TotalMMS = ConstantsHFC.zero;
            double TotalGPRS = ConstantsHFC.zero;
            double TotalMIN2 = ConstantsHFC.zero;
            double TotalSMS2 = ConstantsHFC.zero;
            double TotalMMS2 = ConstantsHFC.zero;
            double TotalGPRS2 = ConstantsHFC.zero;
            int i = ConstantsHFC.zero;

            DbParameter[] parameters = 
            {
                new DbParameter("p_tipo_consulta", DbType.String, 255, ParameterDirection.Input, "C"),
                new DbParameter("p_valor", DbType.Int64, 255, ParameterDirection.Input, vCONTRATOID),
                new DbParameter("p_fecha_ini", DbType.String, 255,ParameterDirection.Input, vFECHA_INI),
                new DbParameter("p_fecha_fin", DbType.String, 255, ParameterDirection.Input, vFECHA_FIN),
                new DbParameter("p_cursor", DbType.Object, ParameterDirection.Output)
            };

            Logging.ExecuteMethod(strIdSession, strTransaction, () =>
            {
                DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS,
                    DbCommandConfiguration.SIACU_POST_BSCS_SP_DET_LLAMADA, parameters, reader =>
                    {
                        while (reader.Read())
                        {
                            var item = new EntitiesFixed.PhoneCall();
                            i++;
                            item.NroRegistro = i;
                            item.VlrNumber = Functions.CheckStr(i);
                            string strFecha = Functions.CheckStr(reader["FECHA_HORA_INICIO"]);
                            item.Fecha_Hora_Inicio = Functions.CheckDate(reader["FECHA_HORA_INICIO"]);
                            item.Fecha = strFecha.Substring(0, 10);
                            item.Hora = strFecha.Substring(11);
                            item.Cantidad = Functions.CheckStr(reader["CANTIDAD"]);
                            item.Cargo_Final = Functions.CheckStr(reader["CARGO_FINAL"]);
                            item.Cargo_Ori_Flexible = Functions.CheckStr(reader["CARGO_ORI_FLEXIBLE"]);
                            item.Cargo_Original = Functions.CheckStr(reader["CARGO_ORIGINAL"]);
                            item.Horario = Functions.CheckStr(reader["HORARIO"]);
                            item.Plan_Tarifario = Functions.CheckStr(reader["PLAN_TARIFARIO"]);
                            item.Operador = Functions.CheckStr(reader["OPERADOR"]);
                            item.Tipo = Functions.CheckStr(reader["TIPO"]);
                            if ((item.Tipo.ToUpper() == "LLAMADA-SALIENTE") || (item.Tipo.ToUpper() == "SMS-SALIENTE"))
                            {
                                if (vSeguridad == "1")
                                { item.Telefono_Destino = Functions.CheckStr(reader["TELEFONO_DESTINO"]); }
                                else
                                { item.Telefono_Destino = Functions.SeguridadFormatTelf(Functions.CheckStr(reader["TELEFONO_DESTINO"])); }
                            }
                            else
                            {
                                item.Telefono_Destino = Functions.CheckStr(reader["TELEFONO_DESTINO"]);
                            }
                            item.Tipo_Llamada = Functions.CheckStr(reader["TIPO_LLAMADA"]);
                            item.Tarifa = Functions.CheckStr(reader["TARIFA"]);
                            item.Zona_Tarifaria = Functions.CheckStr(reader["ZONA_TARIFARIA"]);
                            if (item.Zona_Tarifaria.Trim() != String.Empty)
                            {
                                if (item.Tipo.ToUpper().IndexOf("LLAMADA") != -1)
                                {
                                    TotalMIN = TotalMIN + Functions.CheckDbl(item.Cantidad);
                                }
                                else
                                {
                                    if (item.Tipo.ToUpper().IndexOf("SMS") != -1)
                                    {
                                        TotalSMS = TotalSMS + Functions.CheckDbl(item.Cantidad);
                                    }
                                    else
                                    {
                                        if (item.Tipo.ToUpper().IndexOf("MMS") != -1)
                                        {
                                            TotalMMS = TotalMMS + Functions.CheckDbl(item.Cantidad);
                                        }
                                        else
                                        {
                                            if (item.Tipo.ToUpper().IndexOf("GPRS") != -1)
                                            {
                                                if ((Functions.CheckDbl(item.Cantidad) % 1024) == 0)
                                                    TotalGPRS = TotalGPRS + (Functions.CheckDbl(item.Cantidad) / 1024);
                                                else
                                                    TotalGPRS = TotalGPRS + ((Functions.CheckDbl(item.Cantidad) / 1024) + 1);
                                            }
                                        }
                                    }
                                }
                            }
                            if (item.Tipo.ToUpper().IndexOf("LLAMADA") != -1)
                            {
                                item.Cantidad = Functions.GetFormatHHMMSS(Functions.CheckInt64(item.Cantidad));
                            }
                            Total = Total + Functions.CheckDbl(item.Cargo_Final);
                            salida.Add(item);
                        }
                    });
            });

            vTOTAL = Functions.CheckStr(Total) + ";" + Functions.CheckStr(TotalMIN) + ";" + Functions.CheckStr(TotalSMS) + ";" + Functions.CheckStr(TotalMMS) + ";" + Functions.CheckStr(TotalGPRS);
            return salida;
        }

        public static BpelCallDetailResponse GetBilledCallsDetailHfC(string strIdSession, string strTransaction, BpelCallDetailRequest objRequest)
        {
            var strFaultMsg = string.Empty;
            var objBpel = new BpelCallDetailResponse();
            objBpel.ListBilledCallsDetailHfc = new List<EntitiesFixed.BilledCallsDetailHFC>();

            List<EntitiesFixed.BilledCallsDetailHFC> listModel = new List<EntitiesFixed.BilledCallsDetailHFC>();
            try
            {
                var objRequestHeader = new BpelCallDetail.HeaderRequestType
                {
                    canal = objRequest.HeaderRequestTypeBpel.Canal,
                    idAplicacion = objRequest.HeaderRequestTypeBpel.IdAplicacion,//OPCIONAL
                    usuarioAplicacion = objRequest.HeaderRequestTypeBpel.UsuarioAplicacion,//OPCIONAL
                    usuarioSesion = objRequest.HeaderRequestTypeBpel.UsuarioSesion,//OPCIONAL
                    idTransaccionESB = objRequest.HeaderRequestTypeBpel.IdTransaccionEsb,//OPCIONAL
                    idTransaccionNegocio = objRequest.HeaderRequestTypeBpel.IdTransaccionNegocio, //strTransaction,
                    fechaInicio = DateTime.Now,//OPCIONAL
                    nodoAdicional = objRequest.HeaderRequestTypeBpel.NodoAdicional
                };

                var contactUser = new BpelCallDetail.ContactUser
                {
                    usuario = objRequest.DetalleLlamadasRequestBpel.ContactUserBpel.Usuario,// "SA",
                    nombres = objRequest.DetalleLlamadasRequestBpel.ContactUserBpel.Nombres, //"jose",
                    apellidos = objRequest.DetalleLlamadasRequestBpel.ContactUserBpel.Apellidos, //"arriola",
                    razonSocial = objRequest.DetalleLlamadasRequestBpel.ContactUserBpel.RazonSocial,//"CLARO",
                    tipoDoc = objRequest.DetalleLlamadasRequestBpel.ContactUserBpel.TipoDoc,//"DNI",
                    numDoc = objRequest.DetalleLlamadasRequestBpel.ContactUserBpel.NumDoc, //"44388042",
                    domicilio = objRequest.DetalleLlamadasRequestBpel.ContactUserBpel.Domicilio, //"JOSE BENJAMIN Z",
                    distrito = objRequest.DetalleLlamadasRequestBpel.ContactUserBpel.Distrito, //"SJL",
                    departamento = objRequest.DetalleLlamadasRequestBpel.ContactUserBpel.Departamento, //"LIMA",
                    provincia = objRequest.DetalleLlamadasRequestBpel.ContactUserBpel.Provincia, //"LIMA",
                    modalidad = objRequest.DetalleLlamadasRequestBpel.ContactUserBpel.Modalidad //"LIBRE"
                };

                var customerClfy = new BpelCallDetail.CustomerClfy
                {
                    account = objRequest.DetalleLlamadasRequestBpel.CustomerClfyBpel.Account, //"7.1828917",
                    contactObjId = objRequest.DetalleLlamadasRequestBpel.CustomerClfyBpel.ContactObjId,//"268609749",
                    flagReg = objRequest.DetalleLlamadasRequestBpel.CustomerClfyBpel.FlagReg //"1"
                };

                var interact = new BpelCallDetail.Interact
                {
                    contactobjid = objRequest.DetalleLlamadasRequestBpel.InteractionBpel.Contactobjid,
                    phone = objRequest.DetalleLlamadasRequestBpel.InteractionBpel.Phone, //"84606060",
                    tipo = objRequest.DetalleLlamadasRequestBpel.InteractionBpel.Tipo, //"NO PRECISADO", BPEL
                    clase = objRequest.DetalleLlamadasRequestBpel.InteractionBpel.Clase, //"NO PRECISADO", BPEL
                    subclase = objRequest.DetalleLlamadasRequestBpel.InteractionBpel.Subclase, //"NO PRECISADO", BPEL
                    tipoInter = objRequest.DetalleLlamadasRequestBpel.InteractionBpel.TipoInter, //"ENTRANTE",
                    metodoContacto = objRequest.DetalleLlamadasRequestBpel.InteractionBpel.MetodoContacto, //"Presencial",
                    resultado = objRequest.DetalleLlamadasRequestBpel.InteractionBpel.Resultado,//"OTRO",
                    hechoEnUno = objRequest.DetalleLlamadasRequestBpel.InteractionBpel.HechoEnUno,
                    notas = objRequest.DetalleLlamadasRequestBpel.InteractionBpel.Notas,
                    flagCaso = objRequest.DetalleLlamadasRequestBpel.InteractionBpel.FlagCaso,//"1",
                    usrProceso = objRequest.DetalleLlamadasRequestBpel.InteractionBpel.UsrProceso, //"SA",
                    coId = objRequest.DetalleLlamadasRequestBpel.InteractionBpel.CoId,
                    codPlano = objRequest.DetalleLlamadasRequestBpel.InteractionBpel.CodPlano,
                    agente = objRequest.DetalleLlamadasRequestBpel.InteractionBpel.Agente
                };

                foreach (var prop in interact.GetType().GetProperties())
                {
                    if (prop.GetValue(interact, null) == null)
                    {
                        prop.SetValue(interact, string.Empty);
                    }
                }

                var interactPlus = new BpelCallDetail.InteractPlus
                {
                    claroNumber = objRequest.DetalleLlamadasRequestBpel.InteractionPlusBpel.ClaroNumber,
                    documentNumber = objRequest.DetalleLlamadasRequestBpel.InteractionPlusBpel.DocumentNumber,
                    firstName = objRequest.DetalleLlamadasRequestBpel.InteractionPlusBpel.FirstName,
                    lastName = objRequest.DetalleLlamadasRequestBpel.InteractionPlusBpel.LastName,
                    nameLegalRep = objRequest.DetalleLlamadasRequestBpel.InteractionPlusBpel.NameLegalRep,
                    flagRegistered = objRequest.DetalleLlamadasRequestBpel.InteractionPlusBpel.FlagRegistered,
                    email = objRequest.DetalleLlamadasRequestBpel.InteractionPlusBpel.Email,
                    inter30 = objRequest.DetalleLlamadasRequestBpel.InteractionPlusBpel.Inter30, //Nota
                    inter29 = objRequest.DetalleLlamadasRequestBpel.InteractionPlusBpel.Inter29, //mes y año
                    inter15 = objRequest.DetalleLlamadasRequestBpel.InteractionPlusBpel.Inter15, //cacdac
                    inter16 = objRequest.DetalleLlamadasRequestBpel.InteractionPlusBpel.Inter16, //invoce - BPEL
                    inter18 = objRequest.DetalleLlamadasRequestBpel.InteractionPlusBpel.Inter18, //ContratoId
                    birthday = objRequest.DetalleLlamadasRequestBpel.InteractionPlusBpel.Birthday,
                    expireDate = objRequest.DetalleLlamadasRequestBpel.InteractionPlusBpel.ExpireDate
                };

                foreach (PropertyInfo prop in interactPlus.GetType().GetProperties())
                {
                    if (prop.GetValue(interactPlus, null) == null)
                    {
                        prop.SetValue(interactPlus, string.Empty);
                    }
                }

                var objDetalleLlamadaRequest = new BpelCallDetail.detalleLlamadasRequest
                {
                    tipoConsulta = objRequest.DetalleLlamadasRequestBpel.TipoConsulta,
                    msisdn = objRequest.DetalleLlamadasRequestBpel.Msisdn,
                    fechaInicio = objRequest.DetalleLlamadasRequestBpel.FechaInicio,
                    fechaFin = objRequest.DetalleLlamadasRequestBpel.FechaFin,
                    contactUser = contactUser,
                    customerClfy = customerClfy,
                    interact = interact,
                    interactPlus = interactPlus,
                    flagConstancia = objRequest.DetalleLlamadasRequestBpel.FlagConstancia,
                    ipCliente = objRequest.DetalleLlamadasRequestBpel.IpCliente,
                    tipoConsultaContrato = objRequest.DetalleLlamadasRequestBpel.TipoConsultaContrato,
                    valorContrato = objRequest.DetalleLlamadasRequestBpel.ValorContrato,
                    flagContingencia = objRequest.DetalleLlamadasRequestBpel.FlagContingencia,
                    codigoCliente = objRequest.DetalleLlamadasRequestBpel.CodigoCliente,
                    flagEnvioCorreo = objRequest.DetalleLlamadasRequestBpel.FlagEnvioCorreo,
                    flagGenerarOcc = objRequest.DetalleLlamadasRequestBpel.FlagGenerarOcc,
                    invoiceNumber = objRequest.DetalleLlamadasRequestBpel.InvoiceNumber,
                    periodo = objRequest.DetalleLlamadasRequestBpel.Periodo,
                    tipoProducto = objRequest.DetalleLlamadasRequestBpel.TipoProducto
                };

                //for (int i = 0; i < 150; i++)
                //{
                //    var count = 0;
                //    var model = new EntitiesFixed.BilledCallsDetailHFC();
                //    model.StrDate = Functions.CheckStr("27/05/2017 20:00:00.000");
                //    model.StrHour = !string.IsNullOrEmpty("20:00") ? Functions.GetFormatHHMMSS24AsHHMMSSAMPM(Functions.CheckStr("20:00")) : string.Empty;
                //    model.NroCustomer = Functions.CheckStr("940260065");
                //    if (objRequest.StrSecurity == ConstantsHFC.strUno)
                //    {
                //        model.TelephoneDest = Functions.CheckStr("948780353");
                //        model.TelephoneDestExport = Functions.CheckStr("948780353");
                //    }
                //    else
                //    {
                //        if (Functions.CheckStr("LLAMADA-SALIENTE") == "LLAMADA-SALIENTE" || Functions.CheckStr("LLAMADA-SALIENTE") == "SMS-SALIENTE")
                //        {
                //            var strNumero = Functions.CheckStr("948780353");
                //            model.TelephoneDest = Functions.SeguridadFormatTelf(strNumero);
                //            model.TelephoneDestExport = strNumero;
                //        }
                //        else
                //        {
                //            model.TelephoneDest = Functions.CheckStr("948780353");
                //            model.TelephoneDestExport = Functions.CheckStr("948780353");
                //        }
                //    }
                //    model.Consumption = !string.IsNullOrEmpty("50.00") ? Functions.GetFormatMMSSAsHHMMSS(Functions.CheckStr("50.00")) : string.Empty;
                //    model.CargOrig = Functions.CheckStr("18.75");
                //    model.TypeCalls = Functions.CheckStr("LLAMADA-SALIENTE");
                //    model.Destination = Functions.CheckStr("Trujillo");
                //    model.Operator = Functions.CheckStr("Claro");
                //    count++;
                //    model.NroRegistre = count;
                //    listModel.Add(model);
                //}

                //objBpel.ListBilledCallsDetailHfc = listModel;

                var msg1 = string.Format("METODO: {0},ACCION: {1}, RESULTADO: {2}", "GetBilledCallsDetailHfC", "WebService", "Inicio de Busqueda en el Servicio WS");
                Logging.Info("IdSession: " + strIdSession, "Transaccion: " + string.Empty, msg1);
                var objResponseBody = new BpelCallDetail.detalleLlamadasResponse();

                var objResponseHeader = Logging.ExecuteMethod<BpelCallDetail.HeaderResponseType>(objRequest.Audit.Session, objRequest.Audit.Transaction,
                Configuration.ServiceConfiguration.FIXED_BPEL_CALLDETAIL, () =>
                {
                    return Configuration.ServiceConfiguration.FIXED_BPEL_CALLDETAIL.consultarDetalleLlamadas(objRequestHeader, objDetalleLlamadaRequest, out objResponseBody);
                });

                Logging.Info(objRequest.StrIdSession, objRequest.StrTransaction, objResponseBody.responseStatus.codigoRespuesta + objResponseBody.responseStatus.descripcionRespuesta + objResponseBody.responseStatus.estado + objResponseBody.responseStatus.origen + objResponseBody.responseStatus.ubicacionError);

                var msg2 = string.Format("METODO: {0},ACCION: {1}, RESULTADO: {2}", "GetBilledCallsDetailHfC", "WebService", "Fin de Busqueda en el Servicio WS");
                Logging.Info("IdSession: " + strIdSession, "Transaccion: " + "", msg2);

                int count = 0;
                var strNumero = string.Empty;
                if (objResponseBody != null)
                {
                    if (objResponseBody.responseData != null)
                    {
                        var objModelData = objResponseBody.responseData;
                        if (objModelData.listaDetalleLlamadasResponse != null)
                        {
                            var objModelLstLlamadas = objModelData.listaDetalleLlamadasResponse;

                            objBpel.FechaCicloIni = objModelLstLlamadas.fechaCicloIni ?? string.Empty;
                            objBpel.FechaCicloFin = objModelLstLlamadas.fechaCicloFin ?? string.Empty;
                            
                            if (objModelLstLlamadas.detLlamadaSalienteFact.Length > 0)
                            {
                                var objModelLstFac = objModelData.listaDetalleLlamadasResponse.detLlamadaSalienteFact;
                                foreach (var item in objModelLstFac)
                                {
                                    var model = new EntitiesFixed.BilledCallsDetailHFC();
                                    model.StrDate = Functions.CheckStr(item.fecha);
                                    model.StrHour = !string.IsNullOrEmpty(item.hora) ? Functions.GetFormatHHMMSS24AsHHMMSSAMPM(Functions.CheckStr(item.hora)) : string.Empty;
                                    model.NroCustomer = Functions.CheckStr(item.telefono);
                                    if (objRequest.StrSecurity == ConstantsHFC.strUno)
                                    {
                                        model.TelephoneDest = Functions.CheckStr(item.telefonoDestino);
                                        model.TelephoneDestExport = Functions.CheckStr(item.telefonoDestino);
                                    }
                                    else
                                    {
                                        if (Functions.CheckStr(item.tipoLlamada) == "LLAMADA-SALIENTE" || Functions.CheckStr(item.tipoLlamada) == "SMS-SALIENTE")
                                        {
                                            strNumero = Functions.CheckStr(item.telefonoDestino);
                                            model.TelephoneDest = Functions.SeguridadFormatTelf(strNumero);
                                            model.TelephoneDestExport = strNumero;
                                        }
                                        else
                                        {
                                            model.TelephoneDest = Functions.CheckStr(item.telefonoDestino);
                                            model.TelephoneDestExport = Functions.CheckStr(item.telefonoDestino);
                                        }
                                    }
                                    model.Consumption = !string.IsNullOrEmpty(item.consumo) ? Functions.GetFormatMMSSAsHHMMSS(Functions.CheckStr(item.consumo)) : string.Empty;
                                    model.CargOrig = Functions.CheckStr(item.cargoOriginal);
                                    model.TypeCalls = Functions.CheckStr(item.tipoLlamada);
                                    model.Destination = Functions.CheckStr(item.destino);
                                    model.Operator = Functions.CheckStr(item.operador);
                                    model.NroRegistre = count + 1;
                                    count++;
                                    listModel.Add(model);
                                }

                                objBpel.ListBilledCallsDetailHfc = listModel;
                            }
                        }

                    }
                }
            }

            catch (FaultException<BpelCallDetail.ClaroFault> ex)
            {
                objBpel.StrResponseMessage = ConfigurationManager.AppSettings("strMsgErrorFaultBPel");
                objBpel.StrResponseCode = Constants.NumberOneNegativeString;
                Logging.Error(objRequest.StrIdSession, objRequest.StrTransaction, "BPEL1:" + ex.Message);
            }
            catch (FaultException<ExceptionDetail> ex)
            {
                objBpel.StrResponseMessage = ConfigurationManager.AppSettings("strMsgErrorFaultBPel"); ;
                objBpel.StrResponseCode = Constants.NumberOneNegativeString;
                Logging.Error(objRequest.StrIdSession, objRequest.StrTransaction, "BPEL2:" + ex.Message);
            }
            catch (FaultException ex)
            {
                objBpel.StrResponseMessage = ConfigurationManager.AppSettings("strMsgErrorFaultBPel"); ;
                objBpel.StrResponseCode = Constants.NumberOneNegativeString;
                Logging.Error(objRequest.StrIdSession, objRequest.StrTransaction, "BPEL3:" + ex.Message);
            }
            catch (Exception ex)
            {
                objBpel.StrResponseMessage = ConfigurationManager.AppSettings("strMsgErrorFaultBPel"); ;
                objBpel.StrResponseCode = Constants.NumberOneNegativeString;
                LogException(objRequest.StrIdSession, objRequest.StrTransaction, "BPEL4:", ex);
            }

            Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, strFaultMsg);

            return objBpel;
        }

        #region LLAMADAS ENTRANTES HFC - LTE
        public static CallDetailInputFixedResponse GetCallDetailInputFixed(BpelCallDetailRequest objRequest)
        {

            CallDetailInputFixedResponse oCallDetailInputFixedResponse = new CallDetailInputFixedResponse();

            try
            {
                var salida = new List<CallDetailSummary>();
                //var vSeguridad = "1";
                double Total = ConstantsHFC.zero;
                double TotalMIN = ConstantsHFC.zero;
                double TotalSMS = ConstantsHFC.zero;
                double TotalMMS = ConstantsHFC.zero;
                double TotalGPRS = ConstantsHFC.zero;

                var objRequestHeader = new BpelCallDetail.HeaderRequestType
                {
                    canal = objRequest.HeaderRequestTypeBpel.Canal,
                    idAplicacion = objRequest.HeaderRequestTypeBpel.IdAplicacion,//OPCIONAL
                    usuarioAplicacion = objRequest.HeaderRequestTypeBpel.UsuarioAplicacion,//OPCIONAL
                    usuarioSesion = objRequest.HeaderRequestTypeBpel.UsuarioSesion,//OPCIONAL
                    idTransaccionESB = objRequest.HeaderRequestTypeBpel.IdTransaccionEsb,//OPCIONAL
                    idTransaccionNegocio = objRequest.HeaderRequestTypeBpel.IdTransaccionNegocio, //strTransaction,
                    fechaInicio = DateTime.Now,
                    nodoAdicional = "1"
                };

                var contactUser = new BpelCallDetail.ContactUser
                {
                    usuario = objRequest.DetalleLlamadasRequestBpel.ContactUserBpel.Usuario,// "SA",
                    nombres = objRequest.DetalleLlamadasRequestBpel.ContactUserBpel.Nombres, //"jose",
                    apellidos = objRequest.DetalleLlamadasRequestBpel.ContactUserBpel.Apellidos, //"arriola",
                    razonSocial = objRequest.DetalleLlamadasRequestBpel.ContactUserBpel.RazonSocial,//"CLARO",
                    tipoDoc = objRequest.DetalleLlamadasRequestBpel.ContactUserBpel.TipoDoc,//"DNI",
                    numDoc = objRequest.DetalleLlamadasRequestBpel.ContactUserBpel.NumDoc, //"44388042",
                    domicilio = objRequest.DetalleLlamadasRequestBpel.ContactUserBpel.Domicilio, //"JOSE BENJAMIN Z",
                    distrito = objRequest.DetalleLlamadasRequestBpel.ContactUserBpel.Distrito, //"SJL",
                    departamento = objRequest.DetalleLlamadasRequestBpel.ContactUserBpel.Departamento, //"LIMA",
                    provincia = objRequest.DetalleLlamadasRequestBpel.ContactUserBpel.Provincia, //"LIMA",
                    modalidad = objRequest.DetalleLlamadasRequestBpel.ContactUserBpel.Modalidad //"LIBRE",
                };


                var customerClfy = new BpelCallDetail.CustomerClfy
                {
                    account = objRequest.DetalleLlamadasRequestBpel.CustomerClfyBpel.Account, //"7.1828917",
                    contactObjId = objRequest.DetalleLlamadasRequestBpel.CustomerClfyBpel.ContactObjId,//"268609749",
                    flagReg = objRequest.DetalleLlamadasRequestBpel.CustomerClfyBpel.FlagReg //"1"
                };

                var interact = new BpelCallDetail.Interact
                {
                    contactobjid = objRequest.DetalleLlamadasRequestBpel.InteractionBpel.Contactobjid ?? string.Empty,
                    phone = objRequest.DetalleLlamadasRequestBpel.InteractionBpel.Phone ?? string.Empty,
                    tipo = objRequest.DetalleLlamadasRequestBpel.InteractionBpel.Tipo ?? string.Empty,
                    clase = objRequest.DetalleLlamadasRequestBpel.InteractionBpel.Clase ?? string.Empty,
                    subclase = objRequest.DetalleLlamadasRequestBpel.InteractionBpel.Subclase ?? string.Empty,
                    agente = objRequest.DetalleLlamadasRequestBpel.InteractionBpel.Agente ?? string.Empty,
                    tipoInter = objRequest.DetalleLlamadasRequestBpel.InteractionBpel.TipoInter ?? string.Empty,
                    metodoContacto = objRequest.DetalleLlamadasRequestBpel.InteractionBpel.MetodoContacto ?? string.Empty,
                    resultado = objRequest.DetalleLlamadasRequestBpel.InteractionBpel.Resultado ?? string.Empty,
                    usrProceso = objRequest.DetalleLlamadasRequestBpel.InteractionBpel.UsrProceso ?? string.Empty,
                    flagCaso = objRequest.DetalleLlamadasRequestBpel.InteractionBpel.FlagCaso ?? string.Empty,
                    notas = objRequest.DetalleLlamadasRequestBpel.InteractionBpel.Notas ?? string.Empty
                };

                //Setear valor a cadena vacia
                foreach (PropertyInfo prop in interact.GetType().GetProperties())
                {
                    if (prop.GetValue(interact, null) == null)
                    {
                        prop.SetValue(interact, string.Empty);
                    }
                }

                var interactPlus = new BpelCallDetail.InteractPlus
                {
                    claroNumber = objRequest.DetalleLlamadasRequestBpel.InteractionPlusBpel.ClaroNumber ?? string.Empty,
                    inter15 = objRequest.DetalleLlamadasRequestBpel.InteractionPlusBpel.Inter15 ?? string.Empty,
                    firstName = objRequest.DetalleLlamadasRequestBpel.InteractionPlusBpel.FirstName ?? string.Empty,
                    lastName = objRequest.DetalleLlamadasRequestBpel.InteractionPlusBpel.LastName ?? string.Empty,
                    documentNumber = objRequest.DetalleLlamadasRequestBpel.InteractionPlusBpel.DocumentNumber ?? string.Empty,
                    referencePhone = objRequest.DetalleLlamadasRequestBpel.InteractionPlusBpel.ReferencePhone ?? string.Empty,
                    inter20 = objRequest.DetalleLlamadasRequestBpel.InteractionPlusBpel.Inter20 ?? string.Empty,
                    inter21 = objRequest.DetalleLlamadasRequestBpel.InteractionPlusBpel.Inter21 ?? string.Empty,
                    inter30 = objRequest.DetalleLlamadasRequestBpel.InteractionPlusBpel.Inter30 ?? string.Empty,
                    flagRegistered = objRequest.DetalleLlamadasRequestBpel.InteractionPlusBpel.FlagRegistered ?? string.Empty,
                    adjustmentAmount = objRequest.DetalleLlamadasRequestBpel.InteractionPlusBpel.AdjustmentAmount ?? string.Empty,
                    position = objRequest.DetalleLlamadasRequestBpel.InteractionPlusBpel.Position ?? string.Empty,
                    expireDate = objRequest.DetalleLlamadasRequestBpel.InteractionPlusBpel.ExpireDate ?? string.Empty,
                    birthday = objRequest.DetalleLlamadasRequestBpel.InteractionPlusBpel.Birthday ?? string.Empty,
                    email = objRequest.DetalleLlamadasRequestBpel.InteractionPlusBpel.Email ?? string.Empty,
                    inter5 = objRequest.DetalleLlamadasRequestBpel.InteractionPlusBpel.Inter5 ?? string.Empty
                };

                //Setear valor a cadena vacia
                foreach (PropertyInfo prop in interactPlus.GetType().GetProperties())
                {
                    if (prop.GetValue(interactPlus, null) == null)
                    {
                        prop.SetValue(interactPlus, string.Empty);
                    }
                }


                var objDetalleLlamadaRequest = new BpelCallDetail.detalleLlamadasRequest
                {
                    tipoConsulta = objRequest.DetalleLlamadasRequestBpel.TipoConsulta,
                    msisdn = objRequest.DetalleLlamadasRequestBpel.Msisdn,
                    fechaInicio = objRequest.DetalleLlamadasRequestBpel.FechaInicio,
                    fechaFin = objRequest.DetalleLlamadasRequestBpel.FechaFin,
                    contactUser = contactUser,
                    customerClfy = customerClfy,
                    interact = interact,
                    interactPlus = interactPlus,
                    flagConstancia = objRequest.DetalleLlamadasRequestBpel.FlagConstancia,
                    ipCliente = objRequest.DetalleLlamadasRequestBpel.IpCliente,
                    tipoConsultaContrato = objRequest.DetalleLlamadasRequestBpel.TipoConsultaContrato,
                    valorContrato = objRequest.DetalleLlamadasRequestBpel.ValorContrato,
                    flagContingencia = objRequest.DetalleLlamadasRequestBpel.FlagContingencia,
                    codigoCliente = objRequest.DetalleLlamadasRequestBpel.CodigoCliente,
                    flagEnvioCorreo = objRequest.DetalleLlamadasRequestBpel.FlagEnvioCorreo,
                    flagGenerarOcc = objRequest.DetalleLlamadasRequestBpel.FlagGenerarOcc,
                    invoiceNumber = objRequest.DetalleLlamadasRequestBpel.InvoiceNumber,
                    periodo = objRequest.DetalleLlamadasRequestBpel.Periodo,
                    tipoProducto = objRequest.DetalleLlamadasRequestBpel.TipoProducto
                };

                var objResponseBody = new BpelCallDetail.detalleLlamadasResponse();
                var objResponseHeader = Logging.ExecuteMethod<BpelCallDetail.HeaderResponseType>(objRequest.Audit.Session, objRequest.Audit.Transaction,
                Configuration.ServiceConfiguration.FIXED_BPEL_CALLDETAIL, () =>
                {
                    return Configuration.ServiceConfiguration.FIXED_BPEL_CALLDETAIL.consultarDetalleLlamadas(objRequestHeader, objDetalleLlamadaRequest, out objResponseBody);
                });

                Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "BPEL ENTRANTES STATTUS | ERROR:" + objResponseBody.responseStatus.codigoRespuesta + "-" + objResponseBody.responseStatus.descripcionRespuesta + "-" + objResponseBody.responseStatus.estado + "-" + objResponseBody.responseStatus.origen + "-" + objResponseBody.responseStatus.ubicacionError);


                if (objResponseBody != null)
                {
                    if (objResponseBody.responseData != null)
                    {
                        var objModelData = objResponseBody.responseData;
                        if (objModelData.listaDetalleLlamadasResponse != null)
                        {
                            var objModelLstLlamadas = objModelData.listaDetalleLlamadasResponse;
                            if (objModelData.listaDetalleLlamadasResponse.detLlamadaEntrante.Length > 0)
                            {
                                int contador = 1;
                                foreach (var item in objModelData.listaDetalleLlamadasResponse.detLlamadaEntrante)
                                {
                                    var oCallDetailSummary = new CallDetailSummary
                                    {
                                        NroOrd = (contador++).ToString(),
                                        MSISDN = item.numeroA,
                                        CallDate = item.fecha,
                                        CallTime = item.horaInicio,
                                        CallNumber = item.numeroB,
                                        CallDuration = item.duracion
                                    };

                                    salida.Add(oCallDetailSummary);
                                }
                            }
                        }

                    }

                }

            

                oCallDetailInputFixedResponse.ListCallDetailSummary = salida;
                oCallDetailInputFixedResponse.codigoRespuesta = objResponseBody.responseStatus.codigoRespuesta;
                oCallDetailInputFixedResponse.descripcionRespuesta = objResponseBody.responseStatus.descripcionRespuesta;
            }
            catch (TimeoutException ex)
            {
                Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
                oCallDetailInputFixedResponse.codigoRespuesta = ConstantsHFC.strMenosUno;
                oCallDetailInputFixedResponse.descripcionRespuesta = SIACU.Transac.Service.Constants.MessageNotServicesLimitWait;
                throw ex;
            }
            catch (WebException ex)
            {
                oCallDetailInputFixedResponse.codigoRespuesta = ConstantsHFC.strMenosUno;
                oCallDetailInputFixedResponse.descripcionRespuesta = SIACU.Transac.Service.Constants.MessageNotComunicationServerRemote;
                LogException(objRequest.StrIdSession, objRequest.StrTransaction, "BPEL4:", ex);
            }
            catch (FaultException<BpelCallDetail.ClaroFault> ex)
            {
                oCallDetailInputFixedResponse.codigoRespuesta = ConstantsHFC.strMenosUno;
                oCallDetailInputFixedResponse.descripcionRespuesta = ConfigurationManager.AppSettings("strMsgErrorFaultBPel");
                Logging.Error(objRequest.StrIdSession, objRequest.StrTransaction, "Error BPEL DETALLE LLAMADAS ENTRANTES HFC/LTE " + ex.Detail.codigoError + "-" + ex.Detail.descripcionError + "-" + ex.Detail.ubicacionError);
            }
            catch (FaultException ex)
            {
                oCallDetailInputFixedResponse.codigoRespuesta = ConstantsHFC.strMenosUno;
                oCallDetailInputFixedResponse.descripcionRespuesta = ConfigurationManager.AppSettings("strMsgErrorFaultBPel"); ;
                Logging.Error(objRequest.StrIdSession, objRequest.StrTransaction, "BPEL3 BPEL DETALLE LLAMADAS ENTRANTES HFC/LTE :" + ex.Message + "-" + ex.Action);
            }
            catch (Exception ex)
            {
                oCallDetailInputFixedResponse.codigoRespuesta = ConstantsHFC.strMenosUno;
                oCallDetailInputFixedResponse.descripcionRespuesta = ConfigurationManager.AppSettings("strMsgErrorFaultBPel"); ;
                Logging.Error(objRequest.StrIdSession, objRequest.StrTransaction, "BPEL3 BPEL DETALLE LLAMADAS ENTRANTES HFC/LTE:" + ex.Message);
            }

            return oCallDetailInputFixedResponse;
        }
        #endregion
    }
}
