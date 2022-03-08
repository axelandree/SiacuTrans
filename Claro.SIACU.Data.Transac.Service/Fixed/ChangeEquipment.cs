using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Claro.Data;
using Claro.SIACU.Data.Transac.Service.Configuration;
using Claro.SIACU.Entity.Transac.Service.Fixed;
using Claro.SIACU.Entity.Transac.Service.Fixed.GetAvailabilitySimcard;
using Claro.SIACU.Entity.Transac.Service.Fixed.GetDispEquipment;
using Claro.SIACU.Transac.Service;
using FIXED = Claro.SIACU.Entity.Transac.Service.Fixed;
using CHANGE_EQUIPMENT = Claro.SIACU.ProxyService.Transac.Service.CambioEquipoWS;
using Claro.SIACU.ProxyService.Transac.Service.CambioEquipoWS;
using ConstantsLTE = Claro.SIACU.Transac.Service.Constants;

namespace Claro.SIACU.Data.Transac.Service.Fixed
{
    public class ChangeEquipment
    {
        public static List<BEDeco> GetEquipments(string strIdSession, string strTransaction, string strCustomerId, string strCoid)
        {
            List<BEDeco> listCustomerEquipments = new List<BEDeco>();
            DbParameter[] parameters = 
            { 
              new  DbParameter("pi_customer_id", DbType.String, ParameterDirection.Input,strCustomerId), 
			  new  DbParameter("pi_cod_id", DbType.String, ParameterDirection.Input,strCoid),
              new  DbParameter("po_lista", DbType.Object,ParameterDirection.Output),
			  new  DbParameter("po_resultado", DbType.Int32,ParameterDirection.Output),
			  new  DbParameter("po_mensaje", DbType.String,250,ParameterDirection.Output)             
            };
            try
            {
                DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_SP_LISTA_EQUIPOS_LTE, parameters, reader =>
                {
                    listCustomerEquipments = new List<BEDeco>();
                    while (reader.Read())
                    {
                        if (!string.IsNullOrEmpty(Functions.CheckStr(reader["ASOCIADO"])))
                        {

                            var objTemp = new BEDeco
                            {
                                tipoServicio = Functions.CheckStr(reader["TECNOLOGIA"]),
                                numero_serie = Functions.CheckStr(reader["NUMERO_SERIE"]),
                                descripcion_material = Functions.CheckStr(reader["DESCRIPCION_EQUIPO"]),
                                tipo_equipo = Functions.CheckStr(reader["TIPO_EQUIPO"]),
                                tipo_deco = Functions.CheckStr(reader["TIPO_DECO"]),
                                macadress = Functions.CheckStr(reader["MACADDRESS"]),
                                numero = Functions.CheckStr(reader["NUMERO"]),
                                oc_equipo = Functions.CheckStr(reader["OCEQUIPO"]),
                                asociado = Functions.CheckStr(reader["ASOCIADO"]),
                                codigo_tipo_equipo = Functions.CheckStr(reader["TIPO"]),
                                codtipequ = Functions.CheckStr(reader["CODTIPEQU"]),
                                tipequ = Functions.CheckStr(reader["TIPEQU"]),
                                codinssrv = Functions.CheckStr(reader["codinssrv"]),
                                precio_almacen = Functions.CheckStr(reader["CF"]),
                                penalidad = 0//Functions.CheckDecimal(reader["PENALIDAD"])
                            };



                            listCustomerEquipments.Add(objTemp);
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);

            }
            return listCustomerEquipments;
        }
        public static FIXED.GetChangeEquipment.ChangeEquipmentResponse GetExecuteChangeEquipment(FIXED.GetChangeEquipment.ChangeEquipmentRequest objRequest)
        {
            FIXED.GetChangeEquipment.ChangeEquipmentResponse objResponse = new FIXED.GetChangeEquipment.ChangeEquipmentResponse();
            try
            {
                #region Body Request of Service
                    var contactoCliente = new ContactoClienteType()
                    {
                        apellidos = objRequest.apellidos,
                        departamento = objRequest.departamento,
                        provincia = objRequest.provincia,
                        distrito = objRequest.distrito,
                        domicilio = objRequest.domicilio,
                        modalidad = objRequest.modalidad,
                        nombres = objRequest.nombres,
                        razonSocial = objRequest.razonSocial,
                        tipoDoc = objRequest.tipoDoc,
                        numDoc = objRequest.numDoc,
                        usuario = objRequest.usuario
                    };

                    var ParametrosCliente = new ParametrosClienteType()
                    {
                        contactObjId = string.Empty,
                        flagReg = objRequest.flagReg,
                        msisdn = objRequest.phone
                    };

                    var GenerarConstancia = new GenerarConstanciaType()
                    {
                        driver =objRequest.formatoConstancia,
                        directory = objRequest.directory,
                        fileName = objRequest.fileName
                    };


                    var ParametrosEnvioCorreo = new ParametrosEnvioCorreoType()
                    {
                        correoDestinatario = objRequest.email,
                        asunto = objRequest.asunto,
                        mensaje = objRequest.mensaje
                    };

                    var ParametrosPrincipal = new ParametrosPrincipalType()
                    {
                        account = objRequest.account,
                        agente = objRequest.agente,
                        servafect = string.Empty,
                        servafectCode = string.Empty,
                        phone = objRequest.phone,
                        coId = objRequest.coId,
                        contactobjid = string.Empty,
                        siteobjid = objRequest.siteobjid,
                        usrProceso = objRequest.usrProceso,
                        tipo = objRequest.tipo,
                        clase = objRequest.clase,
                        subclase = objRequest.subClase,
                        tipoInter = objRequest.tipoInter,
                        flagCaso = objRequest.flagCaso,
                        metodoContacto = objRequest.metodoContacto,
                        resultado = objRequest.resultado,
                        hechoEnUno = objRequest.hechoEnUno,
                        notas = objRequest.notas,
                        codPlano = objRequest.codPlano,
                        inconven = objRequest.inconven,
                        inconvenCode = objRequest.inconvenCode,
                        valor1 = objRequest.valor1,
                        valor2 = objRequest.valor2
                    };

                    var ParametrosPlus = new ParametrosPlusType()
                    {
                        secuencial = string.Empty,
                        nroInteraccion = string.Empty,
                        inter1 = string.Empty,
                        inter2 = string.Empty,
                        inter3 = objRequest.inter3,
                        inter4 = string.Empty,
                        inter5 = string.Empty,
                        inter6 = string.Empty,
                        inter7 = objRequest.inter7,
                        inter8 = string.Empty,
                        inter9 = string.Empty,
                        inter10 = string.Empty,
                        inter11 = string.Empty,
                        inter12 = string.Empty,
                        inter13 = string.Empty,
                        inter14 = string.Empty,
                        inter15 = objRequest.inter15,
                        inter16 = objRequest.inter16,
                        inter17 = objRequest.inter17,
                        inter18 = objRequest.inter18,
                        inter19 = objRequest.inter19,
                        inter20 = objRequest.inter20,
                        inter21 = objRequest.inter21,
                        inter22 = string.Empty,
                        inter23 = string.Empty,
                        inter24 = string.Empty,
                        inter25 = string.Empty,
                        inter26 = string.Empty,
                        inter27 = string.Empty,
                        inter28 = string.Empty,
                        inter29 = objRequest.inter29,
                        inter30 = objRequest.inter30,
                        plusInter2Interact = string.Empty,
                        adjustmentAmount = string.Empty,
                        adjustmentReason = string.Empty,
                        address = objRequest.address,
                        amountUnit = string.Empty,
                        birthday = string.Empty,
                        clarifyInteraction = string.Empty,
                        claroLdn1 = string.Empty,
                        claroLdn2 = string.Empty,
                        claroLdn3 = string.Empty,
                        claroLdn4 = string.Empty,
                        claroLocal1 = objRequest.claroLocal1,
                        claroLocal2 = string.Empty,
                        claroLocal3 = string.Empty,
                        claroLocal4 = string.Empty,
                        claroLocal5 = string.Empty,
                        claroLocal6 = string.Empty,
                        contactPhone = string.Empty,
                        dniLegalRep = string.Empty,
                        documentNumber = objRequest.documentNumber,
                        email = objRequest.email,
                        emailConfirmation = objRequest.emailConfirmation,
                        expireDate = string.Empty,
                        fax = string.Empty,
                        firstName = objRequest.firstName,
                        fixedNumber = string.Empty,
                        flagChangeUser = string.Empty,
                        flagCharge = objRequest.flagCharge,
                        flagLegalRep = string.Empty,
                        flagOther = string.Empty,
                        flagTitular = string.Empty,
                        address5 = string.Empty,
                        chargeAmount = objRequest.chargeAmount,
                        city = string.Empty,
                        claroNumber = string.Empty,
                        contactSex = string.Empty,
                        department = string.Empty,
                        district = string.Empty,
                        flagRegistered = string.Empty,
                        iccid = string.Empty,
                        imei = string.Empty,
                        lastName = string.Empty,
                        lastNameRep = string.Empty,
                        ldiNumber = string.Empty,
                        lotCode = string.Empty,
                        maritalStatus = string.Empty,
                        model = string.Empty,
                        month = string.Empty,
                        nameLegalRep = string.Empty,
                        occupation = string.Empty,
                        oldClaroLdn1 = string.Empty,
                        oldClaroLdn2 = string.Empty,
                        oldClaroLdn3 = string.Empty,
                        oldClaroLdn4 = string.Empty,
                        oldClaroLocal1 = string.Empty,
                        oldClaroLocal2 = string.Empty,
                        oldClaroLocal3 = string.Empty,
                        oldClaroLocal4 = string.Empty,
                        oldClaroLocal5 = string.Empty,
                        oldClaroLocal6 = string.Empty,
                        oldDocNumber = string.Empty,
                        oldFirstName = string.Empty,
                        oldFixedNumber = string.Empty,
                        oldFixedPhone = string.Empty,
                        oldLastName = string.Empty,
                        oldLdiNumber = string.Empty,
                        operationType = string.Empty,
                        ostNumber = string.Empty,
                        otherDocNumber = string.Empty,
                        otherFirstName = string.Empty,
                        otherLastName = string.Empty,
                        otherPhone = string.Empty,
                        phoneLegalRep = string.Empty,
                        basket = string.Empty,
                        position = string.Empty,
                        reason = string.Empty,
                        referenceAddress = string.Empty,
                        referencePhone = string.Empty,
                        registrationReason = objRequest.RegistrationReason,
                        typeDocument = objRequest.TypeDocument,
                        zipCode = string.Empty
                    };
                    int index = 0;
                    DetalleServicioType[] ListaDetalleServicio = new DetalleServicioType[objRequest.ListDetService.Count];
                    foreach (var item in objRequest.ListDetService)
                    {
                        DetalleServicioType DetalleServicio = new DetalleServicioType()
                        {
                            serv = item.descripcion_material ?? string.Empty,
                            tipServ = item.tipoServicio ?? string.Empty,
                            grupServ = item.servicio_principal ?? string.Empty,
                            cf = item.componente,
                            equipo = item.numero_serie ?? string.Empty,
                            cantidad = item.numero ?? string.Empty,
                        };
                        ListaDetalleServicio.SetValue(DetalleServicio, index);
                        index++;
                    }

                    var RegistroAuditoria = new RegistroAuditoriaType()
                    {
                        cuentaUsuario = objRequest.usuario,
                        ipCliente = objRequest.strIpClient,
                        ipServidor = objRequest.strIpServer,
                        monto = objRequest.strAmount,
                        nombreCliente = objRequest.strNameClient,
                        nombreServidor = objRequest.strNameServer,
                        servicio = objRequest.strService,
                        telefono = objRequest.strTransactionAudit,
                        texto = objRequest.strText
                    };
                    
                string fieldSeparator = ConstantsLTE.PresentationLayer.gstrVariablePipeline;
                var tramaCabeceraSOT = new System.Text.StringBuilder();
                    tramaCabeceraSOT.Append(string.Format("{0}{1}", objRequest.coId, fieldSeparator));
                    tramaCabeceraSOT.Append(string.Format("{0}{1}", objRequest.customerId, fieldSeparator));
                    tramaCabeceraSOT.Append(string.Format("{0}{1}", "$idinteraccion", fieldSeparator)); 
                    tramaCabeceraSOT.Append(string.Format("{0}{1}", objRequest.tipoTrans, fieldSeparator));
                    tramaCabeceraSOT.Append(string.Format("{0}{1}", objRequest.tipTra, fieldSeparator));
                    tramaCabeceraSOT.Append(string.Format("{0}{1}", "$idinteraccion", fieldSeparator)); 
                    tramaCabeceraSOT.Append(string.Format("{0}{1}", objRequest.codMotot, fieldSeparator));
                    tramaCabeceraSOT.Append(string.Format("{0}{1}", objRequest.tipoVia, fieldSeparator));
                    tramaCabeceraSOT.Append(string.Format("{0}{1}", objRequest.nomVia, fieldSeparator));
                    tramaCabeceraSOT.Append(string.Format("{0}{1}", objRequest.numVia, fieldSeparator));
                    tramaCabeceraSOT.Append(string.Format("{0}{1}", objRequest.tipUrb, fieldSeparator));
                    tramaCabeceraSOT.Append(string.Format("{0}{1}", objRequest.nomUrb, fieldSeparator));
                    tramaCabeceraSOT.Append(string.Format("{0}{1}", objRequest.manzana, fieldSeparator));
                    tramaCabeceraSOT.Append(string.Format("{0}{1}", objRequest.lote, fieldSeparator));
                    tramaCabeceraSOT.Append(string.Format("{0}{1}", objRequest.ubigeo, fieldSeparator));
                    tramaCabeceraSOT.Append(string.Format("{0}{1}", objRequest.codZona, fieldSeparator));
                    tramaCabeceraSOT.Append(string.Format("{0}{1}", objRequest.codPlano, fieldSeparator));
                    tramaCabeceraSOT.Append(string.Format("{0}{1}", objRequest.codeDif, fieldSeparator));
                    tramaCabeceraSOT.Append(string.Format("{0}{1}", objRequest.referencia, fieldSeparator));
                    tramaCabeceraSOT.Append(string.Format("{0}{1}", objRequest.fecProg, fieldSeparator));
                    tramaCabeceraSOT.Append(string.Format("{0}{1}", objRequest.franjaHor, fieldSeparator));
                    tramaCabeceraSOT.Append(string.Format("{0}{1}", objRequest.numCarta, fieldSeparator));
                    tramaCabeceraSOT.Append(string.Format("{0}{1}", objRequest.operador, fieldSeparator));
                    tramaCabeceraSOT.Append(string.Format("{0}{1}", objRequest.preSuscrito, fieldSeparator));
                    tramaCabeceraSOT.Append(string.Format("{0}{1}", objRequest.publicar, fieldSeparator));
                    tramaCabeceraSOT.Append(string.Format("{0}{1}", objRequest.tmCode, fieldSeparator));
                    tramaCabeceraSOT.Append(string.Format("{0}{1}", objRequest.usuReg, fieldSeparator));
                    tramaCabeceraSOT.Append(string.Format("{0}{1}", objRequest.cargo, fieldSeparator));
                    tramaCabeceraSOT.Append(string.Format("{0}{1}", objRequest.codCaso, fieldSeparator));
                    tramaCabeceraSOT.Append(string.Format("{0}{1}", objRequest.tipoServicio, fieldSeparator));
                    tramaCabeceraSOT.Append(string.Format("{0}{1}", objRequest.flagActDirFact, fieldSeparator));
                    tramaCabeceraSOT.Append(string.Format("{0}{1}", objRequest.tipoProducto, fieldSeparator));
                    tramaCabeceraSOT.Append(string.Format("{0}{1}", string.Empty , fieldSeparator));//"PI_CODOCC"
                    tramaCabeceraSOT.Append(string.Format("{0}{1}", string.Empty , fieldSeparator));// "PI_NUM_CUOTA"
                    tramaCabeceraSOT.Append(string.Format("{0}{1}", string.Empty , fieldSeparator));// "PI_MONTO"
                    tramaCabeceraSOT.Append(string.Format("{0}{1}", string.Empty , fieldSeparator));//"PI_COMENT"
                    tramaCabeceraSOT.Append(string.Format("{0}{1}", string.Empty , fieldSeparator));//"PI_TOPE"
                    tramaCabeceraSOT.Append(string.Format("{0}{1}", string.Empty , fieldSeparator));//"PI_CO_SER"
                    tramaCabeceraSOT.Append(string.Format("{0}{1}", string.Empty , fieldSeparator));//"PI_FLAG_LC"
                    tramaCabeceraSOT.Append(string.Format("{0}{1}", string.Empty , fieldSeparator));//"PI_ANOTACION_TOA"
                    tramaCabeceraSOT.Append(string.Format("{0}{1}", string.Empty ,objRequest.observacion, fieldSeparator));//
                    tramaCabeceraSOT.Append(string.Format("{0}{1}", string.Empty , fieldSeparator));//"PI_TOPEMENOR"
                    tramaCabeceraSOT.Append(string.Format("{0}{1}", string.Empty , fieldSeparator));//"PI_RECLAMOCASO"


                    var RegistroCabecera = new RegistroCabeceraType()
                    {
                        codId = objRequest.coId,
                        customerId = objRequest.customerId,
                        tipoTrans = objRequest.tipoTrans,
                        trama = tramaCabeceraSOT.ToString(),
                        lstTipEqu = objRequest.lstTipEqu,
                        lstCoser = objRequest.lstCoser,
                        lstSncode = objRequest.lstSncode,
                        lstSpcode = objRequest.lstSpcode,
                        tipTra = objRequest.tipTra,
                        codMotot = objRequest.codMotot        
                    };

                    var RegistroDetalle = new RegistroDetalleType()
                    {
                        trama = objRequest.trama
                    };

                    var RegistroSot = new RegistroSotType
                    {
                        idProcess = string.Empty,
                        coId = objRequest.coId,
                        customerId = objRequest.customerId

                    };
                #endregion
                var oRequestHeader = new HeaderRequest()
                {
                    channel = string.Empty,
                    startDate = DateTime.UtcNow,
                    userSession =objRequest.userSession,
                    idApplication = objRequest.idApplication,
                    idBusinessTransaction = objRequest.Audit.Session,
                    idESBTransaction = objRequest.Audit.Transaction,
                    additionalNode = ConstantsLTE.strUno,
                    userApplication = objRequest.usuario,
                    startDateSpecified = true,
                };

                var CambioEquipoResponse = new ejecutarCambioEquipoResponse();

                var  parametrosSot = new ParametrosSotType();
                parametrosSot.registroCabecera = RegistroCabecera;
                parametrosSot.registroSot = RegistroSot;
                parametrosSot.registroDetalle = RegistroDetalle;

                var oRequestBody = new ejecutarCambioEquipoRequest()
                {
                    parametrosCliente = ParametrosCliente,
                    contactoCliente = contactoCliente,
                    generarConstancia = GenerarConstancia,
                    parametrosEnvioCorreo = ParametrosEnvioCorreo,
                    parametrosPlus = ParametrosPlus,
                    parametrosPrincipal = ParametrosPrincipal,
                    parametrosSot = parametrosSot,
                    flagContingencia = objRequest.flagContingencia,
                    listaDetalleServicio = ListaDetalleServicio,
                    registroAuditoria = RegistroAuditoria
                };
               

                string strInputTramaHeader = Functions.CreateXML(oRequestHeader);
                string strInputTramaBody = Functions.CreateXML(oRequestBody);

                var oHeaderResponse = Claro.Web.Logging.ExecuteMethod(string.Empty, string.Empty, ServiceConfiguration.FixedChangeEquipmentWS, () =>
                {
                    return ServiceConfiguration.FixedChangeEquipmentWS.ejecutarCambioEquipo(oRequestHeader, oRequestBody, out CambioEquipoResponse);
                });

                Claro.Web.Logging.Info(objRequest.Audit.Session, "ChangeEquipment: ", string.Format("GetExecuteChangeEquipment(): - codId: {0} - customerId: {1}   - tipTra: {2}  ", objRequest.coId, objRequest.customerId, objRequest.tipoTrans));
                if (CambioEquipoResponse.responseData != null)
                {
                    objResponse.idInteraccion = CambioEquipoResponse.responseData.idInteraccion;
                    objResponse.numeroSOT = CambioEquipoResponse.responseData.numeroSOT;
                    objResponse.rutaConstancia = CambioEquipoResponse.responseData.rutaConsntacia;
                    Claro.Web.Logging.Info(objRequest.Audit.Session, "ChangeEquipment: ", string.Format("GetExecuteChangeEquipment(): - idInteraccion: {0} - numeroSOT: {1}   - rutaConstancia: {2}  ", objResponse.idInteraccion, objResponse.numeroSOT, objResponse.rutaConstancia));
                }

                if (CambioEquipoResponse.responseStatus != null)
                {
                    objResponse.codeResponse = CambioEquipoResponse.responseStatus.codeResponse;
                    objResponse.date = CambioEquipoResponse.responseStatus.date;
                    objResponse.descriptionResponse = CambioEquipoResponse.responseStatus.descriptionResponse;
                    objResponse.errorLocation = CambioEquipoResponse.responseStatus.errorLocation;
                    objResponse.origin = CambioEquipoResponse.responseStatus.origin;
                    objResponse.status = CambioEquipoResponse.responseStatus.status;
                    Claro.Web.Logging.Info(objRequest.Audit.Session, "ChangeEquipment: ", string.Format("GetExecuteChangeEquipment(): - Codigo de Respuesta: {0} - Mensaje: {1}  ", objResponse.codeResponse, objResponse.descriptionResponse));

                }

            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, "Error WS - Cambio de Equipo :" + ex.Message);
            }

            return objResponse;

        }
        public static DispEquipmentResponse GetValidateEquipment(DispEquipmentRequest objRequest)
        {

            DispEquipmentResponse objResponse = new DispEquipmentResponse();
            DbParameter[] parameters = 
            {
                new DbParameter("pi_nroserie", DbType.String, ParameterDirection.Input, objRequest.strNroserie),
                new DbParameter("pi_tipo", DbType.Int32, ParameterDirection.Input, objRequest.intTipo),
                new DbParameter("po_resultado", DbType.Int32, ParameterDirection.Output),
                new DbParameter("po_mensaje", DbType.String,255,ParameterDirection.Output),
                new DbParameter("po_lista", DbType.Object, ParameterDirection.Output)
            };
            try
            {
                List<BEDeco> listItem = null;
                DbFactory.ExecuteReader(objRequest.Audit.Session, objRequest.Audit.Transaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_SP_VAL_EQUIPO, parameters, (IDataReader reader) =>
                {
                    listItem = new List<BEDeco>();

                    while (reader.Read())
                    {
                        listItem.Add(new BEDeco
                        {
                            numero_serie = Functions.CheckStr(reader["NUMERO_SERIE"]),
                            descripcion_material = Functions.CheckStr(reader["DESCRIPCION"]),
                            tipo_equipo = Functions.CheckStr(reader["TIPO"]),
                            codigo_tipo_equipo = Functions.CheckStr(reader["TIP"]),
                            tipo_deco = Functions.CheckStr(reader["TIPO_DECO"]),
                            macadress = Functions.CheckStr(reader["MAC_ADDRESS"]),
                            codtipequ = Functions.CheckStr(reader["CODTIPEQU"]),
                            tipequ = Functions.CheckStr(reader["TIPEQU"])
                        });
                    }
                });
                objResponse.lstEquipments = listItem;
                objResponse.ResultCode = parameters[parameters.Length - 3].Value.ToString();
                objResponse.ResultMessage = parameters[parameters.Length - 2].Value.ToString();
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }
            return objResponse;
        }
        public static AvailabilitySimcardResponse GetAvailabilitySimcardSANS(AvailabilitySimcardRequest objRequest)
        {
            AvailabilitySimcardResponse objResponse = new AvailabilitySimcardResponse();
            DbParameter[] parameters = 
            {
                new DbParameter("NRO_SERIE", DbType.String, ParameterDirection.Input, objRequest.SimcardSeries),
                new DbParameter("P_RESULTADO", DbType.Int32, ParameterDirection.Output),
                new DbParameter("MENSAJE", DbType.String,255,ParameterDirection.Output)
            };
            try
            {
                Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    DbFactory.ExecuteNonQuery(objRequest.Audit.Session, objRequest.Audit.Transaction, DbConnectionConfiguration.SIAC_POST_SANSDB, DbCommandConfiguration.SIACU_USP_VALIDA_ICCID_DISP, parameters);
                });

                objResponse.ResultCode = parameters[parameters.Length - 2].Value.ToString();
                objResponse.ResultMessage = parameters[parameters.Length - 1].Value.ToString();

            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }
            return objResponse;
        }
        public static AvailabilitySimcardResponse GetAvailabilitySimcardBSCS(AvailabilitySimcardRequest objRequest)
        {
            AvailabilitySimcardResponse objResponse = new AvailabilitySimcardResponse();
            DbParameter[] parameters = 
            {
                new DbParameter("k_co_id", DbType.Int32, ParameterDirection.Input, Int32.Parse(objRequest.ContractId)),
                new DbParameter("k_iccid", DbType.String, ParameterDirection.Input, objRequest.SimcardSeries),
                new DbParameter("k_resultado", DbType.Int32, ParameterDirection.Output),
                new DbParameter("k_msgerr", DbType.String,255,ParameterDirection.Output)
            };
            try
            {
                Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    DbFactory.ExecuteNonQuery(objRequest.Audit.Session, objRequest.Audit.Transaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_SP_VAL_ICCID_CONTR, parameters);
                });

                objResponse.ResultCode = parameters[parameters.Length - 2].Value.ToString();
                objResponse.ResultMessage = parameters[parameters.Length - 1].Value.ToString();
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }
            return objResponse;
        }
        public static AvailabilitySimcardResponse GetValidateSimcardBSCS_HLCODE(AvailabilitySimcardRequest objRequest)
        {
            AvailabilitySimcardResponse objResponse = new AvailabilitySimcardResponse();
            DbParameter[] parameters = 
            {
                new DbParameter("PI_ICCID_OLD", DbType.String,255, ParameterDirection.Input, objRequest.SimcardSeriesOld),
                new DbParameter("PI_ICCID_NEW", DbType.String,255, ParameterDirection.Input, objRequest.SimcardSeries),
                new DbParameter("PO_RESULTADO", DbType.Int32, ParameterDirection.Output),
                new DbParameter("PO_MSGERR", DbType.String,255,ParameterDirection.Output)
            };
            try
            {
                Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    DbFactory.ExecuteNonQuery(objRequest.Audit.Session, objRequest.Audit.Transaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_SP_BSCSU_UPD_HLCODE, parameters);
                });

                objResponse.ResultCode = parameters[parameters.Length - 2].Value.ToString();
                objResponse.ResultMessage = parameters[parameters.Length - 1].Value.ToString();
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }
            return objResponse;
        }

        //PROY140315 - Inicio
        public static List<BEDeco> GetEquipmentsDTH(string strIdSession, string strTransaction, string strCustomerId, string strCoid)
        {
            List<BEDeco> listCustomerEquipments = new List<BEDeco>();
            DbParameter[] parameters = 
            { 
              new  DbParameter("pi_customer_id", DbType.String, ParameterDirection.Input,strCustomerId), 
			  new  DbParameter("pi_cod_id", DbType.String, ParameterDirection.Input,strCoid),
              new  DbParameter("po_lista", DbType.Object,ParameterDirection.Output),
			  new  DbParameter("po_resultado", DbType.Int32,ParameterDirection.Output),
			  new  DbParameter("po_mensaje", DbType.String,250,ParameterDirection.Output)             
            };
            try
            {
                DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_SP_LISTA_EQUIPOS_DTH, parameters, reader =>
                {
                    listCustomerEquipments = new List<BEDeco>();
                    while (reader.Read())
                    {
                        if (!string.IsNullOrEmpty(Functions.CheckStr(reader["ASOCIADO"])))
                        {

                            var objTemp = new BEDeco
                            {
                                tipoServicio = Functions.CheckStr(reader["TECNOLOGIA"]),
                                numero_serie = Functions.CheckStr(reader["NUMERO_SERIE"]),
                                descripcion_material = Functions.CheckStr(reader["DESCRIPCION_EQUIPO"]),
                                tipo_equipo = Functions.CheckStr(reader["TIPO_EQUIPO"]),
                                tipo_deco = Functions.CheckStr(reader["TIPO_DECO"]),
                                macadress = Functions.CheckStr(reader["MACADDRESS"]),
                                numero = Functions.CheckStr(reader["NUMERO"]),
                                oc_equipo = Functions.CheckStr(reader["OCEQUIPO"]),
                                asociado = Functions.CheckStr(reader["ASOCIADO"]),
                                codigo_tipo_equipo = Functions.CheckStr(reader["TIPO"]),
                                codtipequ = Functions.CheckStr(reader["CODTIPEQU"]),
                                tipequ = Functions.CheckStr(reader["TIPEQU"]),
                                codinssrv = Functions.CheckStr(reader["codinssrv"]),
                                precio_almacen = Functions.CheckStr(reader["CF"]),
                                penalidad = 0//Functions.CheckDecimal(reader["PENALIDAD"])
                            };



                            listCustomerEquipments.Add(objTemp);
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);

            }
            return listCustomerEquipments;
        }

        public static FIXED.GetChangeEquipment.ChangeEquipmentResponse GetExecuteChangeEquipmentDTH(FIXED.GetChangeEquipment.ChangeEquipmentRequest objRequest)
        {
            FIXED.GetChangeEquipment.ChangeEquipmentResponse objResponse = new FIXED.GetChangeEquipment.ChangeEquipmentResponse();
            try
            {
                #region Body Request of Service
                var contactoCliente = new ContactoClienteType()
                {
                    apellidos = objRequest.apellidos,
                    departamento = objRequest.departamento,
                    provincia = objRequest.provincia,
                    distrito = objRequest.distrito,
                    domicilio = objRequest.domicilio,
                    modalidad = objRequest.modalidad,
                    nombres = objRequest.nombres,
                    razonSocial = objRequest.razonSocial,
                    tipoDoc = objRequest.tipoDoc,
                    numDoc = objRequest.numDoc,
                    usuario = objRequest.usuario
                };

                var ParametrosCliente = new ParametrosClienteType()
                {
                    contactObjId = string.Empty,
                    flagReg = objRequest.flagReg,
                    msisdn = objRequest.phone
                };

                var GenerarConstancia = new GenerarConstanciaType()
                {
                    driver = objRequest.formatoConstancia,
                    directory = objRequest.directory,
                    fileName = objRequest.fileName
                };


                var ParametrosEnvioCorreo = new ParametrosEnvioCorreoType()
                {
                    correoDestinatario = objRequest.email,
                    asunto = objRequest.asunto,
                    mensaje = objRequest.mensaje
                };

                var ParametrosPrincipal = new ParametrosPrincipalType()
                {
                    account = objRequest.account,
                    agente = objRequest.agente,
                    servafect = string.Empty,
                    servafectCode = string.Empty,
                    phone = objRequest.phone,
                    coId = objRequest.coId,
                    contactobjid = objRequest.contactobjid,
                    siteobjid = objRequest.siteobjid,
                    usrProceso = objRequest.usrProceso,
                    tipo = objRequest.tipo,
                    clase = objRequest.clase,
                    subclase = objRequest.subClase,
                    tipoInter = objRequest.tipoInter,
                    flagCaso = objRequest.flagCaso,
                    metodoContacto = objRequest.metodoContacto,
                    resultado = objRequest.resultado,
                    hechoEnUno = objRequest.hechoEnUno,
                    notas = objRequest.notas,
                    codPlano = objRequest.codPlano,
                    inconven = objRequest.inconven,
                    inconvenCode = objRequest.inconvenCode,
                    valor1 = objRequest.valor1,
                    valor2 = objRequest.valor2
                };

                var ParametrosPlus = new ParametrosPlusType()
                {
                    secuencial = string.Empty,
                    nroInteraccion = string.Empty,
                    inter1 = string.Empty,
                    inter2 = string.Empty,
                    inter3 = objRequest.inter3,
                    inter4 = string.Empty,
                    inter5 = string.Empty,
                    inter6 = string.Empty,
                    inter7 = objRequest.inter7,
                    inter8 = string.Empty,
                    inter9 = string.Empty,
                    inter10 = string.Empty,
                    inter11 = string.Empty,
                    inter12 = string.Empty,
                    inter13 = string.Empty,
                    inter14 = string.Empty,
                    inter15 = objRequest.inter15,
                    inter16 = objRequest.inter16,
                    inter17 = objRequest.inter17,
                    inter18 = objRequest.inter18,
                    inter19 = objRequest.inter19,
                    inter20 = objRequest.inter20,
                    inter21 = objRequest.inter21,
                    inter22 = string.Empty,
                    inter23 = string.Empty,
                    inter24 = string.Empty,
                    inter25 = string.Empty,
                    inter26 = string.Empty,
                    inter27 = string.Empty,
                    inter28 = string.Empty,
                    inter29 = objRequest.inter29,
                    inter30 = objRequest.inter30,
                    plusInter2Interact = string.Empty,
                    adjustmentAmount = string.Empty,
                    adjustmentReason = string.Empty,
                    address = objRequest.address,
                    amountUnit = string.Empty,
                    birthday = string.Empty,
                    clarifyInteraction = string.Empty,
                    claroLdn1 = string.Empty,
                    claroLdn2 = string.Empty,
                    claroLdn3 = string.Empty,
                    claroLdn4 = string.Empty,
                    claroLocal1 = objRequest.claroLocal1,
                    claroLocal2 = string.Empty,
                    claroLocal3 = string.Empty,
                    claroLocal4 = string.Empty,
                    claroLocal5 = string.Empty,
                    claroLocal6 = string.Empty,
                    contactPhone = string.Empty,
                    dniLegalRep = string.Empty,
                    documentNumber = objRequest.documentNumber,
                    email = objRequest.email,
                    emailConfirmation = objRequest.emailConfirmation,
                    expireDate = string.Empty,
                    fax = string.Empty,
                    firstName = objRequest.firstName,
                    fixedNumber = string.Empty,
                    flagChangeUser = string.Empty,
                    flagCharge = objRequest.flagCharge,
                    flagLegalRep = string.Empty,
                    flagOther = string.Empty,
                    flagTitular = string.Empty,
                    address5 = string.Empty,
                    chargeAmount = objRequest.chargeAmount,
                    city = string.Empty,
                    claroNumber = string.Empty,
                    contactSex = string.Empty,
                    department = string.Empty,
                    district = string.Empty,
                    flagRegistered = string.Empty,
                    iccid = string.Empty,
                    imei = string.Empty,
                    lastName = string.Empty,
                    lastNameRep = string.Empty,
                    ldiNumber = string.Empty,
                    lotCode = string.Empty,
                    maritalStatus = string.Empty,
                    model = string.Empty,
                    month = string.Empty,
                    nameLegalRep = string.Empty,
                    occupation = string.Empty,
                    oldClaroLdn1 = string.Empty,
                    oldClaroLdn2 = string.Empty,
                    oldClaroLdn3 = string.Empty,
                    oldClaroLdn4 = string.Empty,
                    oldClaroLocal1 = string.Empty,
                    oldClaroLocal2 = string.Empty,
                    oldClaroLocal3 = string.Empty,
                    oldClaroLocal4 = string.Empty,
                    oldClaroLocal5 = string.Empty,
                    oldClaroLocal6 = string.Empty,
                    oldDocNumber = string.Empty,
                    oldFirstName = string.Empty,
                    oldFixedNumber = string.Empty,
                    oldFixedPhone = string.Empty,
                    oldLastName = string.Empty,
                    oldLdiNumber = string.Empty,
                    operationType = string.Empty,
                    ostNumber = string.Empty,
                    otherDocNumber = string.Empty,
                    otherFirstName = string.Empty,
                    otherLastName = string.Empty,
                    otherPhone = string.Empty,
                    phoneLegalRep = string.Empty,
                    basket = string.Empty,
                    position = string.Empty,
                    reason = string.Empty,
                    referenceAddress = string.Empty,
                    referencePhone = string.Empty,
                    registrationReason = objRequest.RegistrationReason,
                    typeDocument = objRequest.TypeDocument,
                    zipCode = string.Empty
                };
                int index = 0;
                DetalleServicioType[] ListaDetalleServicio = new DetalleServicioType[objRequest.ListDetService.Count];
                foreach (var item in objRequest.ListDetService)
                {
                    DetalleServicioType DetalleServicio = new DetalleServicioType()
                    {
                        serv = item.descripcion_material ?? string.Empty,
                        tipServ = item.tipoServicio ?? string.Empty,
                        grupServ = item.servicio_principal ?? string.Empty,
                        cf = item.componente,
                        equipo = item.numero_serie ?? string.Empty,
                        cantidad = item.numero ?? string.Empty,
                    };
                    ListaDetalleServicio.SetValue(DetalleServicio, index);
                    index++;
                }

                var RegistroAuditoria = new RegistroAuditoriaType()
                {
                    cuentaUsuario = objRequest.usuario,
                    ipCliente = objRequest.strIpClient,
                    ipServidor = objRequest.strIpServer,
                    monto = objRequest.strAmount,
                    nombreCliente = objRequest.strNameClient,
                    nombreServidor = objRequest.strNameServer,
                    servicio = objRequest.strService,
                    telefono = objRequest.strTransactionAudit,
                    texto = objRequest.strText
                };

                string fieldSeparator = ConstantsLTE.PresentationLayer.gstrVariablePipeline;
                var tramaCabeceraSOT = new System.Text.StringBuilder();
                tramaCabeceraSOT.Append(string.Format("{0}{1}", objRequest.coId, fieldSeparator));
                tramaCabeceraSOT.Append(string.Format("{0}{1}", objRequest.customerId, fieldSeparator));
                tramaCabeceraSOT.Append(string.Format("{0}{1}", "$idinteraccion", fieldSeparator));
                tramaCabeceraSOT.Append(string.Format("{0}{1}", objRequest.tipoTrans, fieldSeparator));
                tramaCabeceraSOT.Append(string.Format("{0}{1}", objRequest.tipTra, fieldSeparator));
                tramaCabeceraSOT.Append(string.Format("{0}{1}", "$idinteraccion", fieldSeparator));
                tramaCabeceraSOT.Append(string.Format("{0}{1}", objRequest.codMotot, fieldSeparator));
                tramaCabeceraSOT.Append(string.Format("{0}{1}", objRequest.tipoVia, fieldSeparator));
                tramaCabeceraSOT.Append(string.Format("{0}{1}", objRequest.nomVia, fieldSeparator));
                tramaCabeceraSOT.Append(string.Format("{0}{1}", objRequest.numVia, fieldSeparator));
                tramaCabeceraSOT.Append(string.Format("{0}{1}", objRequest.tipUrb, fieldSeparator));
                tramaCabeceraSOT.Append(string.Format("{0}{1}", objRequest.nomUrb, fieldSeparator));
                tramaCabeceraSOT.Append(string.Format("{0}{1}", objRequest.manzana, fieldSeparator));
                tramaCabeceraSOT.Append(string.Format("{0}{1}", objRequest.lote, fieldSeparator));
                tramaCabeceraSOT.Append(string.Format("{0}{1}", objRequest.ubigeo, fieldSeparator));
                tramaCabeceraSOT.Append(string.Format("{0}{1}", objRequest.codZona, fieldSeparator));
                tramaCabeceraSOT.Append(string.Format("{0}{1}", objRequest.codPlano, fieldSeparator));
                tramaCabeceraSOT.Append(string.Format("{0}{1}", objRequest.codeDif, fieldSeparator));
                tramaCabeceraSOT.Append(string.Format("{0}{1}", objRequest.referencia, fieldSeparator));
                tramaCabeceraSOT.Append(string.Format("{0}{1}", objRequest.fecProg, fieldSeparator));
                tramaCabeceraSOT.Append(string.Format("{0}{1}", objRequest.franjaHor, fieldSeparator));
                tramaCabeceraSOT.Append(string.Format("{0}{1}", objRequest.numCarta, fieldSeparator));
                tramaCabeceraSOT.Append(string.Format("{0}{1}", objRequest.operador, fieldSeparator));
                tramaCabeceraSOT.Append(string.Format("{0}{1}", objRequest.preSuscrito, fieldSeparator));
                tramaCabeceraSOT.Append(string.Format("{0}{1}", objRequest.publicar, fieldSeparator));
                tramaCabeceraSOT.Append(string.Format("{0}{1}", objRequest.tmCode, fieldSeparator));
                tramaCabeceraSOT.Append(string.Format("{0}{1}", objRequest.usuReg, fieldSeparator));
                tramaCabeceraSOT.Append(string.Format("{0}{1}", objRequest.cargo, fieldSeparator));
                tramaCabeceraSOT.Append(string.Format("{0}{1}", objRequest.codCaso, fieldSeparator));
                tramaCabeceraSOT.Append(string.Format("{0}{1}", objRequest.tipoServicio, fieldSeparator));
                tramaCabeceraSOT.Append(string.Format("{0}{1}", objRequest.flagActDirFact, fieldSeparator));
                tramaCabeceraSOT.Append(string.Format("{0}{1}", objRequest.tipoProducto, fieldSeparator));
                tramaCabeceraSOT.Append(string.Format("{0}{1}", string.Empty, fieldSeparator));
                tramaCabeceraSOT.Append(string.Format("{0}{1}", string.Empty, fieldSeparator));
                tramaCabeceraSOT.Append(string.Format("{0}{1}", string.Empty, fieldSeparator));
                tramaCabeceraSOT.Append(string.Format("{0}{1}", string.Empty, fieldSeparator));
                tramaCabeceraSOT.Append(string.Format("{0}{1}", string.Empty, fieldSeparator));
                tramaCabeceraSOT.Append(string.Format("{0}{1}", string.Empty, fieldSeparator));
                tramaCabeceraSOT.Append(string.Format("{0}{1}", string.Empty, fieldSeparator));
                tramaCabeceraSOT.Append(string.Format("{0}{1}", string.Empty, fieldSeparator));
                tramaCabeceraSOT.Append(string.Format("{0}{1}", string.Empty, objRequest.observacion, fieldSeparator));
                tramaCabeceraSOT.Append(string.Format("{0}{1}", string.Empty, fieldSeparator));
                tramaCabeceraSOT.Append(string.Format("{0}{1}", string.Empty, fieldSeparator));
                tramaCabeceraSOT.Append(string.Format("{0}{1}", objRequest.strSOTAssociate, fieldSeparator));

                var RegistroCabecera = new RegistroCabeceraType()
                {
                    codId = objRequest.coId,
                    customerId = objRequest.customerId,
                    tipoTrans = objRequest.tipoTrans,
                    trama = tramaCabeceraSOT.ToString(),
                    lstTipEqu = objRequest.lstTipEqu,
                    lstCoser = objRequest.lstCoser,
                    lstSncode = objRequest.lstSncode,
                    lstSpcode = objRequest.lstSpcode,
                    tipTra = objRequest.tipTra,
                    codMotot = objRequest.codMotot
                };

                var RegistroDetalle = new RegistroDetalleType()
                {
                    trama = objRequest.trama
                };

                var RegistroSot = new RegistroSotType
                {
                    idProcess = string.Empty,
                    coId = objRequest.coId,
                    customerId = objRequest.customerId

                };
                #endregion
                var oRequestHeader = new HeaderRequest()
                {
                    channel = string.Empty,
                    startDate = DateTime.UtcNow,
                    userSession = objRequest.userSession,
                    idApplication = objRequest.idApplication,
                    idBusinessTransaction = objRequest.Audit.Session,
                    idESBTransaction = objRequest.Audit.Transaction,
                    additionalNode = ConstantsLTE.strUno,
                    userApplication = objRequest.usuario,
                    startDateSpecified = true,
                };

                var CambioEquipoResponse = new ejecutarCambioEquipoResponse();

                var parametrosSot = new ParametrosSotType();
                parametrosSot.registroCabecera = RegistroCabecera;
                parametrosSot.registroSot = RegistroSot;
                parametrosSot.registroDetalle = RegistroDetalle;

                var oRequestBody = new ejecutarCambioEquipoRequest()
                {
                    parametrosCliente = ParametrosCliente,
                    contactoCliente = contactoCliente,
                    generarConstancia = GenerarConstancia,
                    parametrosEnvioCorreo = ParametrosEnvioCorreo,
                    parametrosPlus = ParametrosPlus,
                    parametrosPrincipal = ParametrosPrincipal,
                    parametrosSot = parametrosSot,
                    flagContingencia = objRequest.flagContingencia,
                    listaDetalleServicio = ListaDetalleServicio,
                    registroAuditoria = RegistroAuditoria
                };


                string strInputTramaHeader = Functions.CreateXML(oRequestHeader);
                string strInputTramaBody = Functions.CreateXML(oRequestBody);

                var oHeaderResponse = Claro.Web.Logging.ExecuteMethod(string.Empty, string.Empty, ServiceConfiguration.FixedChangeEquipmentWS, () =>
                {
                    return ServiceConfiguration.FixedChangeEquipmentWS.ejecutarCambioEquipo(oRequestHeader, oRequestBody, out CambioEquipoResponse);
                });

                Claro.Web.Logging.Info(objRequest.Audit.Session, "ChangeEquipment: ", string.Format("GetExecuteChangeEquipment(): - codId: {0} - customerId: {1}   - tipTra: {2}  ", objRequest.coId, objRequest.customerId, objRequest.tipoTrans));
                if (CambioEquipoResponse.responseData != null)
                {
                    objResponse.idInteraccion = CambioEquipoResponse.responseData.idInteraccion;
                    objResponse.numeroSOT = CambioEquipoResponse.responseData.numeroSOT;
                    objResponse.rutaConstancia = CambioEquipoResponse.responseData.rutaConsntacia;
                    Claro.Web.Logging.Info(objRequest.Audit.Session, "ChangeEquipment: ", string.Format("GetExecuteChangeEquipment(): - idInteraccion: {0} - numeroSOT: {1}   - rutaConstancia: {2}  ", objResponse.idInteraccion, objResponse.numeroSOT, objResponse.rutaConstancia));
                }

                if (CambioEquipoResponse.responseStatus != null)
                {
                    objResponse.codeResponse = CambioEquipoResponse.responseStatus.codeResponse;
                    objResponse.date = CambioEquipoResponse.responseStatus.date;
                    objResponse.descriptionResponse = CambioEquipoResponse.responseStatus.descriptionResponse;
                    objResponse.errorLocation = CambioEquipoResponse.responseStatus.errorLocation;
                    objResponse.origin = CambioEquipoResponse.responseStatus.origin;
                    objResponse.status = CambioEquipoResponse.responseStatus.status;
                    Claro.Web.Logging.Info(objRequest.Audit.Session, "ChangeEquipment: ", string.Format("GetExecuteChangeEquipment(): - Codigo de Respuesta: {0} - Mensaje: {1}  ", objResponse.codeResponse, objResponse.descriptionResponse));

                }

            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, "Error WS - Cambio de Equipo :" + ex.Message);
            }

            return objResponse;

        }
        //PROY140315 - Fin
    }
}
