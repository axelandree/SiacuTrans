using Claro.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Claro.SIACU.Data.Transac.Service.Configuration;
using POSTPAID_ACTIONS = Claro.SIACU.ProxyService.Transac.Service.SIACPost.Actions;
using POSTPAID_SIMCARD = Claro.SIACU.ProxyService.Transac.Service.SIACPost.SimCard;
using FUNCTIONS = Claro.SIACU.Transac.Service;

namespace Claro.SIACU.Data.Transac.Service.Postpaid
{
    public class ChangePhoneNumber
    {
        /// <summary>
        /// Método para obtener el estado de la línea
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strNumberPhone">Línea consultada</param>
        /// <returns>Devuelve la ubicación del HLR.</returns>
        public static int GetHLRUbicationPhone(string strIdSession, string strTransactionID, string strNumberPhone)
        {
            int Location = 0;
            List<Claro.SIACU.Entity.Transac.Service.Postpaid.HLR> lstHLR;

            DbParameter[] parameters = new DbParameter[]{
                new DbParameter("K_TELEFONO", DbType.String, ParameterDirection.Input, strNumberPhone),
                new DbParameter("k_cur_tabla_hlr2_tlf", DbType.Object, ParameterDirection.Output)
            };

            try
            {
                lstHLR = DbFactory.ExecuteReader<List<Claro.SIACU.Entity.Transac.Service.Postpaid.HLR>>(strIdSession, strTransactionID, DbConnectionConfiguration.SIAC_AUDIT, DbCommandConfiguration.SIACU_TABLA_NROHLR_TELEFONO, parameters);

                if (lstHLR != null && lstHLR.Count > Claro.Constants.NumberZero)
                {
                    Location = lstHLR.First().HLR2C_NROHLR;
                }
                else
                {
                    Location = Claro.Constants.NumberOne;
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransactionID, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new Exception(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return Location;
        }

        /// <summary>
        /// Método para obtener el estado de la línea x INSI
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strNumberPhone">Línea consultada</param>
        /// <returns>Devuelve la ubicación del HLR.</returns>
        public static int GetHLRUbicationINSI(string strIdSession, string strTransactionID, string strNumberPhone)
        {
            int Location = 0;
            List<Claro.SIACU.Entity.Transac.Service.Postpaid.HLR> lstHLR = new List<Entity.Transac.Service.Postpaid.HLR>();

            DbParameter[] parameters = new DbParameter[]{
                new DbParameter("k_cur_tabla_hlr2insi", DbType.Object, ParameterDirection.Output)
            };

            try
            {
                DbFactory.ExecuteReader(strIdSession, strTransactionID, DbConnectionConfiguration.SIAC_AUDIT, DbCommandConfiguration.SIACU_TABLA_HLR2_INSI, parameters, (IDataReader reader) =>
                {
                    Entity.Transac.Service.Postpaid.HLR item;

                    while (reader.Read())
                    {
                        item = new Entity.Transac.Service.Postpaid.HLR();

                        item.HLR2C_INICIO = Convert.ToInt64(reader["HLR2C_INICIO"]);
                        item.HLR2C_FIN = Convert.ToInt64(reader["HLR2C_FIN"]);
                        lstHLR.Add(item);
                    }
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransactionID, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new Exception(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            if (lstHLR != null && lstHLR.Count > 0)
            {
                foreach (Claro.SIACU.Entity.Transac.Service.Postpaid.HLR oHLR in lstHLR)
                {
                    if (oHLR.HLR2C_INICIO <= Claro.Convert.ToInt64(strNumberPhone) && Claro.Convert.ToInt64(strNumberPhone) <= oHLR.HLR2C_FIN)
                    {
                        Location = Claro.Constants.NumberTwo;
                        break;
                    }
                }
            }
            else
            {
                Location = Claro.Constants.NumberOne;
            }

            return Location;
        }

        /// <summary>
        /// Método para validar si procede el cambio de número
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransactionID">Id de transacción</param>
        /// <param name="strContract">Contrato de la línea</param>
        /// <param name="strFlagFidelize">Flag de fidelización</param>
        /// <param name="strErrorCode">Código de error del servicio</param>
        /// <param name="strMessage">Mensaje del servicio</param>
        /// <returns>Devuelve la ubicación del HLR.</returns>
        public static bool ValidateChangeNumberTransaction(string strIdSession, string strTransactionID, string strContract, string strFlagFidelize, ref string strErrorCode, ref string strMessage)
        {
            bool Response = false;
            POSTPAID_ACTIONS.ValidaCambioNumeroResponseOutput objValidateResponse =
                Claro.Web.Logging.ExecuteMethod<POSTPAID_ACTIONS.ValidaCambioNumeroResponseOutput>
                (strIdSession, strTransactionID, Configuration.ServiceConfiguration.CHANGE_NUMBER, () =>
                {
                    return Configuration.ServiceConfiguration.CHANGE_NUMBER.validaCambioNumero(new POSTPAID_ACTIONS.ValidaCambioNumeroRequestInput()
                        {
                            Request = new POSTPAID_ACTIONS.DETALLEINPUTType()
                            {
                                CODID = strContract,
                                FECHAEJECUCION = DateTime.Today.ToShortDateString(),
                                FLAGID = strFlagFidelize
                            }
                        });
                });

            if (objValidateResponse.ERROR.ERRORCODE == Claro.Constants.NumberZero)
            {
                strErrorCode = Claro.Convert.ToString(objValidateResponse.ERROR.ERRORCODE);
                strMessage = Claro.Convert.ToString(objValidateResponse.ERROR.ERRORDESC);
                Response = true;
            }
            else
            {
                strErrorCode = Claro.Convert.ToString(objValidateResponse.ERROR.ERRORCODE);
                strMessage = ConfigurationManager.AppSettings("strTextoErrorTransacCambioNumero") + Claro.Convert.ToString(objValidateResponse.ERROR.ERRORDESC);
                Response = false;
            }

            return Response;
        }

        /// <summary>
        /// Método para validar si procede el cambio de número
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransactionID">Id de transacción</param>
        /// <param name="strApplicationID">Id de Aplicación</param>
        /// <param name="strApplicationName">Nombre de Aplicación</param>
        /// <param name="strApplicationUser">Usuario de Aplicación</param>
        /// <param name="strNumberOfPhones">Cantidad de líneas</param>
        /// <param name="strRedClasification">Clasificación de la RED</param>
        /// <param name="strCustomerType">Tipo de Cliente</param>
        /// <param name="strNationalCode">Código Nacional</param>
        /// <param name="strPhoneType">Tipo de Línea</param>
        /// <param name="strHLR">HLR</param>
        /// <param name="strNumberPhone">Número de Línea</param>
        /// <param name="strResponseMessage">Respuesta</param>
        /// <returns>Devuelve la cantidad de líneas disponibles</returns>
        public static List<Claro.SIACU.Entity.Transac.Service.Postpaid.SimCardPhone> GetAvailableLines(string strIdSession, string strTransactionID, 
            string strApplicationID, string strApplicationName, string strApplicationUser, string strNumberOfPhones, string strRedClasification,
            string strCustomerType, string strNationalCode, string strPhoneType, string strHLR, string strNumberPhone, ref string strResponseMessage)
        {
            List<Claro.SIACU.Entity.Transac.Service.Postpaid.SimCardPhone> lstLines = new List<Entity.Transac.Service.Postpaid.SimCardPhone>();
            Claro.SIACU.Entity.Transac.Service.Postpaid.SimCardPhone Lines = null;
            string strResponse = "";
            string strMessage = "";
            POSTPAID_SIMCARD.itDataType[] itData = new POSTPAID_SIMCARD.itDataType[Int16.MaxValue];
            POSTPAID_SIMCARD.itReturnType[] oReturnType;

            strResponse = Claro.Web.Logging.ExecuteMethod<string>(strIdSession, strTransactionID, () =>
            {
                return Configuration.WebServiceConfiguration.PREPAID_SIMCARD.consultarTelefonoDisp(ref strTransactionID, strApplicationID, strApplicationName,
                    strApplicationUser, strNumberOfPhones, strRedClasification, strCustomerType, strNationalCode, strPhoneType, strHLR, strNumberPhone, out strMessage, out itData, out oReturnType);
            });

            if (strResponse == Claro.Constants.NumberZeroString)
            {
                if (itData != null)
                {
                    if (itData.Length > 0)
                    {
                        foreach (POSTPAID_SIMCARD.itDataType item in itData)
                        {
                            Lines = new Claro.SIACU.Entity.Transac.Service.Postpaid.SimCardPhone();
                            Lines.NRO_TELEF = Claro.Convert.ToString(item.nroTelef);
                            Lines.CODIG_HLR = Claro.Convert.ToString(item.codigoHlr);
                            Lines.DESC_CLRED = Claro.Convert.ToString(item.descClred);
                            Lines.DESC_REGIO = Claro.Convert.ToString(item.descRegio);
                            Lines.DESC_TPCLI = Claro.Convert.ToString(item.descTpcli);
                            Lines.DESC_TPTLF = Claro.Convert.ToString(item.descTptlf);
                            lstLines.Add(Lines);
                        }

                        strResponseMessage = Claro.Constants.NumberZeroString;
                    }
                    else
                        strResponseMessage = "No retorna líneas disponibles. " + strMessage;
                }
                else
                    strResponseMessage = "No retorna líneas disponibles. " + strMessage;
            }
            else
                strResponseMessage = strMessage;

            return lstLines;
        }

        public static bool ValidateChangeNumberBSCS(string strIdSession, string strTransactionID, string strSerialNum, string strDnNum, int intEstado)
        {
            bool response = false;
            string strcodret = "";
            DbParameter[] parameters = new DbParameter[]{
                new DbParameter("p_sm_serialnum", DbType.String, ParameterDirection.Input, strSerialNum),
                new DbParameter("p_dn_num", DbType.String, ParameterDirection.Input,strDnNum),
                new DbParameter("p_estado", DbType.Int32, ParameterDirection.Input, intEstado),
                new DbParameter("p_cod_ret", DbType.Int64, 255, ParameterDirection.Output),
                new DbParameter("p_desc_ret", DbType.String,255, ParameterDirection.Output)
            };

            DbFactory.ExecuteNonQuery(strIdSession, strTransactionID, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_VALIDA_CAMBIO_NUMERO_BSCS, parameters);
            strcodret = Claro.Convert.ToString(parameters[3].Value);

            if (strcodret == Claro.Constants.NumberOneString)
            {
                response = true;
            }

            return response;
        }

        public static bool ExecuteChangeNumber(string strIdSession, string strTransactionID, string strContract, string strNewPhone , string strEstTraslado, double dblCost, string strFlagFidelize, 
            string strCurrentPhone, string strApplicationID, string strApplicationPwd, string strUser, string strCostMedNO, string strFlagChangeChip,
            string strFlagLocution, ref string strCode, ref string strMessage)
        {
            bool Response = false;
            POSTPAID_ACTIONS.CambioNumeroResponseOutput objResponseOut =
                Claro.Web.Logging.ExecuteMethod<POSTPAID_ACTIONS.CambioNumeroResponseOutput>(strIdSession, strTransactionID, () =>
                {
                    return Configuration.ServiceConfiguration.CHANGE_NUMBER.cambiaNumeroPostpago(new POSTPAID_ACTIONS.CambioNumeroRequestInput
                    {
                        Request = new POSTPAID_ACTIONS.DETALLEINPUTType1
                        {
                            CODID = strContract,
                            NUMERONUEVO = strNewPhone,
                            FECHAEJECUCION = DateTime.Today.ToShortDateString(),
                            ESTRASLADO = strEstTraslado,
                            COSTO = dblCost,
                            FLAGID = strFlagFidelize,
                            NUMEROACTUAL = strCurrentPhone,
                            CODIGOAPLICATIVO = strApplicationID,
                            PASSWORDAPLICATIVO = strApplicationPwd,
                            CODIGOUSUARIO = strUser,
                            COSTMEDNO = strCostMedNO,
                            FLAGCAMBIOCHIPNUME = strFlagChangeChip,
                            FLAGLOCUCION = strFlagLocution
                        }
                    });
                });

            if (objResponseOut.ERROR.ERRORCODE == Claro.Constants.NumberZero)
            {
                Response = true;
            }

            strCode = Claro.Convert.ToString(objResponseOut.ERROR.ERRORCODE);
            strMessage = Claro.Convert.ToString(objResponseOut.ERROR.ERRORDESC);
            return Response;
        }

        public static bool RollbackChangeNumber(string strIdSession, string strTransactionID, string strApplicationID, string strApplicationName,
            string strApplicationUser, string strCurrentPhone, string strNewPhone, string strPhoneUser, ref string strMessage)
        {
            POSTPAID_SIMCARD.itReturnType[] itType = null;
            string strResponse = "";
            string strMessageResponse = "";
            bool ResponseFunction = false;
            strResponse = Claro.Web.Logging.ExecuteMethod<string>(strIdSession, strTransactionID, () =>
            {
                return Configuration.WebServiceConfiguration.PREPAID_SIMCARD.reversionCambioNumero(ref strTransactionID, strApplicationID, strApplicationName, strApplicationUser, strCurrentPhone, strNewPhone, strPhoneUser, out strMessageResponse, out itType);
            });

            strMessage = strMessageResponse;

            if (strResponse == Claro.Constants.NumberZeroString)
            {
                if (itType != null)
                {
                    ResponseFunction = true;
                    strMessage = itType[0].mensaje;
                }
            }

            return ResponseFunction;
        }

        public static string DeleteUserHistory(string strIdSession, string strTransactionID, string strPhone, string strMotive)
        {
            string strResponse = "";

            DbParameter[] parameters = new DbParameter[]{
                new DbParameter("P_CODIGOUSUARIO", DbType.String, ParameterDirection.Input, strPhone),
                new DbParameter("s_Motivo", DbType.String, ParameterDirection.Input,strMotive),
                new DbParameter("o_resultado", DbType.String, 255, ParameterDirection.Output)
            };

            DbFactory.ExecuteNonQuery(strIdSession, strTransactionID, DbConnectionConfiguration.SIAC_CAE, DbCommandConfiguration.SIACU_HISTORICO_BAJA_USUARIOS, parameters);
            strResponse = Claro.Convert.ToString(parameters[2].Value);

            return strResponse;
        }

        public static string UpdatePhoneNumber(string strIdSession, string strTransactionID, string strApplicationID, string strApplicationName, string strApplicationUser,
            string strCurrentPhone, string strNewPhone, string strUserPhone, ref string strMessage)
        {
            string strResponse = "";
            string strMessageResponse = "";
            POSTPAID_SIMCARD.itReturnType[] oReturnType = null;

            strResponse = Claro.Web.Logging.ExecuteMethod<string>(strIdSession, strTransactionID, () =>
            {
                return Configuration.WebServiceConfiguration.PREPAID_SIMCARD.actualizarNroTelef(ref strTransactionID, strApplicationID, strApplicationName, strApplicationUser, strCurrentPhone, strNewPhone, strUserPhone, out strMessageResponse, out oReturnType);
            });

            if (strResponse == "0")
            {
                if (oReturnType != null)
                {
                    strMessageResponse = oReturnType[0].mensaje;
                }
                else
                {
                    strMessageResponse = "Error ActualizarNroTelef: No hay retorno de información";
                    strResponse = "2";
                }
            }
            else
            {
                strResponse = "3";
            }

            strMessage = strMessageResponse;
            return strResponse;
        }
    }
}
