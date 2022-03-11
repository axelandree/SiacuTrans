using Claro.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using KEY = Claro.ConfigurationManager;
using Claro.SIACU.Data.Transac.Service.Configuration;
using PREPAID = Claro.SIACU.Entity.Transac.Service.Prepaid;
using Claro.SIACU.Transac.Service;
using Claro.SIACU.ProxyService.Transac.Service.SIAC.PlanesTFI;
using Claro.SIACU.ProxyService.Transac.Service.SIAC.CambioPlanTFI;
using Claro.Transversal.ProxyService.ConsultaClaves;
using ServiceModel = System.ServiceModel;

namespace Claro.SIACU.Data.Transac.Service.Prepaid
{
    public class Prepaid
    {
        /// <summary>
        /// Método que obtiene los datos de las recargas del cliente.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strMSISDN">Nümero de Teléfono</param>
        /// <param name="strStartDate">Rango inicial de la consulta</param>
        /// <param name="strEndDate">Rango final de la consulta</param>
        /// <param name="strFlag">Flag para retornar todos o una parte de los registros</param>
        /// <param name="intNumberRegs">Cantidad de registros a retornar</param>
        /// <returns>Devuelve los datos de las recargas realizadas por el cliente.</returns>
        public static List<Claro.SIACU.Entity.Transac.Service.Prepaid.Recharge> GetRecharge(string strIdSession, string strTransaction, string strMSISDN, string strStartDate, string strEndDate, string strFlag, int intNumberRegs)
        {
            List<Claro.SIACU.Entity.Transac.Service.Prepaid.Recharge> lstRecharge;

            DbParameter[] parameters = new DbParameter[]{
                new DbParameter("P_MSISDN", DbType.String, ParameterDirection.Input, strMSISDN),
                new DbParameter("P_FECHINI", DbType.String, ParameterDirection.Input, strStartDate),
                new DbParameter("P_FECHFIN", DbType.String, ParameterDirection.Input, strEndDate),
                new DbParameter("P_CURSOR_SALIDA", DbType.Object, ParameterDirection.Output)
            };

            lstRecharge = DbFactory.ExecuteReader<List<Claro.SIACU.Entity.Transac.Service.Prepaid.Recharge>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DWO, DbCommandConfiguration.SIACU_CONSULTAR_REGIONES, parameters);
            lstRecharge = lstRecharge.OrderByDescending(x => DateTime.Parse(x.FECHA_RECARGA)).ToList();
            if (strFlag != Claro.Constants.NumberOneString)
            {
                if (lstRecharge.Count >= intNumberRegs)
                {
                    lstRecharge = lstRecharge.GetRange(Claro.Constants.NumberZero, intNumberRegs - Claro.Constants.NumberOneNegative);
                }
            }
            return lstRecharge;
        }
         
        public static string GetConsultPointOfSale(string strIdSession, string strTransaction, string vstrCodigoCAC)
        {
            DbParameter[] parameters = new DbParameter[]{
												   new DbParameter("P_OVENC_CODIGO", DbType.String,ParameterDirection.Input, vstrCodigoCAC), 
												   new DbParameter("K_SALIDA", DbType.Object,ParameterDirection.Output)};
       
           string strFlagBiometria = string.Empty;
            try
            {
                DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_PVU, DbCommandConfiguration.PVU_SP_GET_DATOS_OFICINA, parameters, (IDataReader reader) =>
                {
                    while (reader.Read())
                    {
                        strFlagBiometria = Convert.ToString(reader["OVENC_FLAG_BIOMETRIA"]);
                    }
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransaction, "ERROR Data/Prepaid GetConsultPointOfSale() -> " + ex.Message);
            }
            return strFlagBiometria;
        }

        public static PREPAID.GetPlanesTFI.responseDataObtenerTabConsultaPlanTFIPost GetPlanesTFI(PREPAID.GetPlanesTFI.PlanesTFIRequest objPlanesTFIRequest)
        {
            PREPAID.GetPlanesTFI.responseDataObtenerTabConsultaPlanTFIPost listResponse =  new PREPAID.GetPlanesTFI.responseDataObtenerTabConsultaPlanTFIPost();
            PREPAID.GetPlanesTFI.PlanesTFIResponse objPlanesTFIResponse = null;
         
            try
            {
                const string TIMRootRegistry = @"SOFTWARE\TIM";
                string strKey = KEY.AppSettings("strRestricTetheVelocWS");
                string cryptoUser = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(TIMRootRegistry + @"\" + strKey).GetValue("User", "").ToString();
                string cryptoPass = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(TIMRootRegistry + @"\" + strKey).GetValue("Password", "").ToString();
                desencriptarResponseBody objdesencriptarResponse = new desencriptarResponseBody();
                desencriptarRequestBody objdesencriptarRequest = new desencriptarRequestBody()
                    {
                        idTransaccion = objPlanesTFIRequest.Audit.Transaction,
                        ipAplicacion = objPlanesTFIRequest.Audit.IPAddress,
                        ipTransicion = objPlanesTFIRequest.Audit.IPAddress,
                        usrAplicacion = objPlanesTFIRequest.Audit.UserName,
                        idAplicacion = objPlanesTFIRequest.Audit.ApplicationName,
                        codigoAplicacion = KEY.AppSettings("strCodigoAplicacion"),
                        usuarioAplicacionEncriptado = cryptoUser,
                        claveEncriptado = cryptoPass,
                    };
                objdesencriptarResponse.codigoResultado = Configuration.ServiceConfiguration.CONSULTA_CLAVES.desencriptar
                (
                            ref objdesencriptarRequest.idTransaccion,
                            objdesencriptarRequest.ipAplicacion,
                            objdesencriptarRequest.ipTransicion,
                            objdesencriptarRequest.usrAplicacion,
                            objdesencriptarRequest.idAplicacion,
                            objdesencriptarRequest.codigoAplicacion,
                            objdesencriptarRequest.usuarioAplicacionEncriptado,
                            objdesencriptarRequest.claveEncriptado,
                            out objdesencriptarResponse.mensajeResultado,
                            out objdesencriptarResponse.usuarioAplicacion,
                            out objdesencriptarResponse.clave
                );
                Claro.Web.Logging.Info(objPlanesTFIRequest.Audit.Session, objPlanesTFIRequest.Audit.Transaction, "CONSULTA_CLAVES.desencriptar codigoResultado: " + objdesencriptarResponse.codigoResultado);
                if (objdesencriptarResponse.codigoResultado == "0")
                    {
                        HeaderRequestType objHeaderRequest = new HeaderRequestType()
                        {
                            country = objPlanesTFIRequest.Header.country,
                            language = objPlanesTFIRequest.Header.language,
                            consumer = objPlanesTFIRequest.Header.consumer,
                            system = objPlanesTFIRequest.Header.system,
                            modulo = KEY.AppSettings("planesTFI"),
                            pid = DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                            userId = objPlanesTFIRequest.Audit.UserName,
                            dispositivo = objPlanesTFIRequest.Audit.IPAddress,
                            wsIp = objPlanesTFIRequest.Audit.IPAddress,
                            operation = KEY.AppSettings("obtenerTabCambioPlanPost"),
                            timestamp = DateTime.Now,
                            msgType = objPlanesTFIRequest.Header.msgType,
                            VarArg = new ProxyService.Transac.Service.SIAC.PlanesTFI.ArgType[1]
                            {
                                new ProxyService.Transac.Service.SIAC.PlanesTFI.ArgType()
                                {
                                    key = String.Empty,
                                    value = String.Empty,
                                }
                            },

                        };
                        HeaderRequestType1 objHeaderRequest1 = new HeaderRequestType1()
                        {
                            canal = objPlanesTFIRequest.Audit.ApplicationName,
                            idAplicacion = KEY.AppSettings("strCodigoAplicacion"),
                            usuarioAplicacion = objPlanesTFIRequest.Audit.UserName,
                            usuarioSesion = objPlanesTFIRequest.Audit.UserName,
                            idTransaccionESB = DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                            idTransaccionNegocio = objPlanesTFIRequest.Audit.Transaction,
                            fechaInicio = DateTime.Now,
                            nodoAdicional = new object()
                        };

                        obtenerTabConsultaPlanTFIPostRequestType objCambioPlanTFI = new obtenerTabConsultaPlanTFIPostRequestType()
                        {
                            proveedor = objPlanesTFIRequest.obtenerTabCambioPlanTFIPostRequest.proveedor,
                            suscriptor = objPlanesTFIRequest.obtenerTabCambioPlanTFIPostRequest.suscriptor,
                            tarifa = objPlanesTFIRequest.obtenerTabCambioPlanTFIPostRequest.tarifa,
                        };

                        obtenerTabConsultaPlanTFIPostResponseType objObtenerTabConsultaPlanTFIPostResponseType = new obtenerTabConsultaPlanTFIPostResponseType();
                        HeaderResponseType1 objHeaderResponseType1 = new HeaderResponseType1();
                        HeaderResponseType objHeaderResponseType = null;
                        objPlanesTFIResponse = new PREPAID.GetPlanesTFI.PlanesTFIResponse();

                        using (new ServiceModel.OperationContextScope(Configuration.ServiceConfiguration.PREPAID_PLANESTFI.InnerChannel))
                        {
                            ServiceModel.OperationContext.Current.OutgoingMessageHeaders.Add
                          (
                              new Claro.Entity.SecurityHeader(objPlanesTFIRequest.Audit.Transaction, objdesencriptarResponse.usuarioAplicacion, objdesencriptarResponse.clave)
                          );
                            objHeaderResponseType = Web.Logging.ExecuteMethod<HeaderResponseType>(objPlanesTFIRequest.Audit.Session, objPlanesTFIRequest.Audit.Transaction, ServiceConfiguration.PREPAID_PLANESTFI, () =>
                            {
                                return ServiceConfiguration.PREPAID_PLANESTFI.obtenerTabConsultaPlanTFIPost(objHeaderRequest, objHeaderRequest1, objCambioPlanTFI,
                                    out objHeaderResponseType1, out objObtenerTabConsultaPlanTFIPostResponseType);
                            });
                        }
                        if (objHeaderResponseType != null && objObtenerTabConsultaPlanTFIPostResponseType != null)
                        {
                            List<PREPAID.GetPlanesTFI.tabConsultaPlanTFIPost> listaTabConsultaPlanTFIPost = new List<PREPAID.GetPlanesTFI.tabConsultaPlanTFIPost>();
                           
                            if (objObtenerTabConsultaPlanTFIPostResponseType.responseStatus.codigoRespuesta == Constants.NumberZeroString)
                            {
                                foreach (var item in objObtenerTabConsultaPlanTFIPostResponseType.responseDataObtenerTabConsultaPlanTFIPost.listaTabConsultaPlanTFIPost)
                                {
                                    Entity.Transac.Service.Prepaid.GetPlanesTFI.tabConsultaPlanTFIPost obj = new PREPAID.GetPlanesTFI.tabConsultaPlanTFIPost();
                                    obj.provider = item.provider;
                                    obj.tariff = item.tariff;
                                    obj.suscriber = item.suscriber;
                                    obj.desc_plan = item.desc_plan;
                                    listaTabConsultaPlanTFIPost.Add(obj);
                                }
                                listResponse.listaTabConsultaPlanTFIPost = listaTabConsultaPlanTFIPost;
                            }
                        }
                    }
                }

            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objPlanesTFIRequest.Audit.Session, objPlanesTFIRequest.Audit.Transaction, "ERROR Data/Prepaid GetPlanesTFI() -> " + ex.Message);
            }

            return listResponse;

        }

        public static PREPAID.GetCambioPlanTFI.CambioPlanTFIResponse GetCambioPlanTFI (PREPAID.GetCambioPlanTFI.CambioPlanTFIRequest objRequest)
        {
            ejecutarCambioPlanPrepagoResponse objCambioPlanResponse = null;
            PREPAID.GetCambioPlanTFI.CambioPlanTFIResponse objResponse = null;
        
            try
            {
                objResponse = new PREPAID.GetCambioPlanTFI.CambioPlanTFIResponse();
                objCambioPlanResponse = new ejecutarCambioPlanPrepagoResponse();
                ejecutarCambioPlanPrepagoRequest objCambioPlanRequest = new ejecutarCambioPlanPrepagoRequest()
                {
                    auditRequest = new auditRequestType()
                    {
                        idTransaccion = objRequest.Audit.Transaction,
                        ipAplicacion = objRequest.Audit.IPAddress,
                        nombreAplicacion = objRequest.Audit.ApplicationName,
                        usuarioAplicacion = KEY.AppSettings("USRSIAC"),
                    },
                    telefono = objRequest.telefono,
                    offer = objRequest.offer,
                    subscriberStatus = objRequest.subscriberStatus,
                };

                objCambioPlanResponse = Web.Logging.ExecuteMethod<ejecutarCambioPlanPrepagoResponse>(objRequest.Audit.Session,objRequest.Audit.Transaction,ServiceConfiguration.PREPAID_CAMBIOPLANTFI, () =>
                    {
                        return ServiceConfiguration.PREPAID_CAMBIOPLANTFI.ejecutarCambioPlanPrepago(objCambioPlanRequest);
                    });

                if (objCambioPlanResponse != null && objCambioPlanResponse.auditResponse.codigoRespuesta == Constants.NumberZeroString)
                {
                    objResponse.idRespuesta = objCambioPlanResponse.defaultServiceResponse.idRespuesta;
                    objResponse.mensajeRespuesta = objCambioPlanResponse.defaultServiceResponse.mensajeRespuesta;
                    if (objResponse.idRespuesta == Constants.NumberZeroString)
                    {
                        objResponse.offerAntiguo = objCambioPlanResponse.offerAntiguo;
                        objResponse.offerNuevo = objCambioPlanResponse.offerNuevo;  
                    }   
                }
                    
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }
            return objResponse;
        }

    }
}
