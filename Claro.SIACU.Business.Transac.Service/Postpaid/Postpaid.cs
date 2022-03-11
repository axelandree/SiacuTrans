using Claro.SIACU.Entity;
using COMMON = Claro.SIACU.Entity.Transac.Service.Common;
using Claro.SIACU.Entity.Transac.Service.Postpaid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Claro.SIACU.Entity.Transac.Service.Postpaid.GetActDesServProg;
using Claro.SIACU.Entity.Transac.Service.Postpaid.GetApprovalBusinessCreditLimit;
using Claro.SIACU.Entity.Transac.Service.Postpaid.GetConsultService;
using Claro.SIACU.Entity.Transac.Service.Postpaid.GetModifyServiceQuotAmount;
using Claro.SIACU.Entity.Transac.Service.Postpaid.GetServiceBSCS;
using Claro.SIACU.Entity.Transac.Service.Postpaid.GetServiceByContract;
using Claro.SIACU.Entity.Transac.Service.Postpaid.GetTypeTransactionBRMS;
using Claro.SIACU.Entity.Transac.Service.Postpaid.GetUserExistsBSCS;
using Claro.SIACU.Entity.Transac.Service.Postpaid.GetValidateActDesServProg;
using Claro.SIACU.Entity.Transac.Service.Postpaid.GetInsertTraceability;
using Claro.SIACU.Entity.Transac.Service.Postpaid.GetBiometricConfiguration;
using Claro.SIACU.Entity.Transac.Service.Postpaid.GetSignDocument;
using KEY = Claro.ConfigurationManager;
using POSTPAID = Claro.SIACU.Entity.Transac.Service.Postpaid;
using Claro.SIACU.Entity.Transac.Service.Postpaid.PostUpdateChangeData;

namespace Claro.SIACU.Business.Transac.Service.Postpaid
{
    public class Postpaid
    {
        
        public static POSTPAID.GetBillData.BillDataResponse GetListBillSummary(POSTPAID.GetBillData.BillDataRequest request)
        {
            string msgError = "";
            POSTPAID.GetBillData.BillDataResponse objResponse =
                new POSTPAID.GetBillData.BillDataResponse()
                {
                    ListBillSummary = Claro.Web.Logging.ExecuteMethod<List<ListItem>>(request.Audit.Session, request.Audit.Transaction,
                        () =>
                        {
                            return Data.Transac.Service.Postpaid.Postpaid.GetListBill(request.Audit.Session, request.Audit.Transaction, request.CustomerCode, ref msgError);
                        })
                };
            objResponse.MsgError = msgError;
            return objResponse;
        }
        

        /// <summary>
        /// Método para obtener el estado de la línea
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strApplicationName">Nombre de la aplicación</param>
        /// <param name="strApplicationUser">Usuario de la aplicación</param>
        /// <param name="strPhoneNumber">Línea consultada</param>
        /// <param name="strResponseMessage">Respuesta del servicio</param>
        /// <returns>Devuelve objeto objNumberPhoneStatus con información de las recargas.</returns>
        public static POSTPAID.SimCardPhone GetStatusPhone(string strIdSession, string strTransactionID, string strApplicationID, string strApplicationName, string strApplicationUser, string strPhoneNumber)
        {
            string strResponseMessage = "";
            POSTPAID.SimCardPhone objNumberPhoneStatus = new POSTPAID.SimCardPhone();

            string strResponse = Claro.Web.Logging.ExecuteMethod<string>(strIdSession, strTransactionID, () =>
            {
                return Data.Transac.Service.Postpaid.Postpaid.GetStatusPhone(strIdSession, strTransactionID, strApplicationID, strApplicationName, strApplicationUser, strPhoneNumber, ref strResponseMessage).Trim();
            });
            if (!string.IsNullOrEmpty(strResponse))
            {
                Claro.Web.Logging.Info(strIdSession, strTransactionID, strResponse);
                objNumberPhoneStatus.RESPONSE_SERVICE = strResponse;
                Claro.Web.Logging.Info(strIdSession, strTransactionID, strResponseMessage);
                objNumberPhoneStatus.RESPONSE_MESSAGE = strResponseMessage;
            }
            return objNumberPhoneStatus;
        }

        /// <summary>
        /// Método para obtener información de portabilidad.
        /// </summary>
        /// <param name="objPortabilityRequest">Contiene teléfono</param>
        /// <returns>Devuelve objeto PortabilityResponse con la información de portabilidad.</returns>
        public static Entity.Transac.Service.Postpaid.GetPortability.PortabilityResponse GetPortability(Entity.Transac.Service.Postpaid.GetPortability.PortabilityRequest objPortabilityRequest)
        {
            string strRespuesta = "", portIN = "", portOUT = "";

            try
            {
                portIN = KEY.AppSettings("PortabilidadPortIN");
                portOUT = KEY.AppSettings("PortabilidadPortOUT");
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objPortabilityRequest.Audit.Session, objPortabilityRequest.Audit.Transaction, ex.Message);
            }

            Entity.Transac.Service.Postpaid.GetPortability.PortabilityResponse objPortabilityResponse = new Entity.Transac.Service.Postpaid.GetPortability.PortabilityResponse
            {
                ListPortability = Claro.Web.Logging.ExecuteMethod<List<Claro.SIACU.Entity.Transac.Service.Postpaid.Portability>>(objPortabilityRequest.Audit.Session, objPortabilityRequest.Audit.Transaction, () => { return Data.Transac.Service.Postpaid.Postpaid.GetPortability(objPortabilityRequest.Audit.Session, objPortabilityRequest.Audit.Transaction, objPortabilityRequest.Telephone, out strRespuesta); })
            };

            if (objPortabilityResponse != null && objPortabilityResponse.ListPortability != null && objPortabilityResponse.ListPortability.Count > 0)
            {
                Claro.SIACU.Entity.Transac.Service.Postpaid.Portability objPortabilitys = objPortabilityResponse.ListPortability[0];
                objPortabilityResponse.Respuesta = strRespuesta;
                objPortabilityResponse.ApplicationNumber = objPortabilitys.NUMERO_SOLICITUD;
                objPortabilityResponse.State = objPortabilitys.DESCRPCION_ESTADO_PROCESO;
                objPortabilityResponse.RegistrationDate = objPortabilitys.FECHA_REGISTRO;
                objPortabilityResponse.TrTypeProcessDate = false;
                objPortabilityResponse.TrTypeProcessOperator = false;

                if (strRespuesta != null && strRespuesta != Claro.SIACU.Constants.InProcessPortability)
                {
                    objPortabilityResponse.TrTypeProcessDate = true;
                    objPortabilityResponse.TrTypeProcessOperator = true;

                    if (objPortabilitys.TIPO_PORTABILIDAD == portOUT)
                    {
                        objPortabilityResponse.TypeProcessDate = Claro.SIACU.Constants.DateDeactivation;
                        objPortabilityResponse.TypeProcessOperator = Claro.SIACU.Constants.OperatorToWhichPort;
                        objPortabilityResponse.ExecutionDate = objPortabilitys.FECHA_EJECUCION;
                        objPortabilityResponse.Operator = ValidateDescriptionPortability(objPortabilitys.CODIGO_OPERADOR_RECEPTOR);
                    }
                    else if (objPortabilitys.TIPO_PORTABILIDAD == portIN)
                    {
                        objPortabilityResponse.TypeProcessDate = Claro.SIACU.Constants.DateActivation;
                        objPortabilityResponse.TypeProcessOperator = Claro.SIACU.Constants.OperatorOfThePort;
                        objPortabilityResponse.ExecutionDate = objPortabilitys.FECHA_EJECUCION;
                        objPortabilityResponse.Operator = ValidateDescriptionPortability(objPortabilitys.CODIGO_OPERADOR_CEDENTE);
                    }
                }
            }

            return objPortabilityResponse;
        }

        /// <summary>
        /// Método para validar la descripción de portabilidad.
        /// </summary>
        /// <param name="strReceivingOperatorCode">Código de operador receptor.</param>
        /// <returns>Devuelve cadena con la descripción de portabilidad.</returns>
        private static string ValidateDescriptionPortability(string strReceivingOperatorCode)
        {
            string strAnswer = "";
            switch (strReceivingOperatorCode)
            {
                case Claro.SIACU.Constants.OperatorCode00A01:
                    strAnswer = Claro.SIACU.Constants.Answer00A01;
                    break;
                case Claro.SIACU.Constants.OperatorCode00A02:
                    strAnswer = Claro.SIACU.Constants.Answer00A02;
                    break;
                case Claro.SIACU.Constants.OperatorCode01R01:
                    strAnswer = Claro.SIACU.Constants.Answer01R01;
                    break;
                case Claro.SIACU.Constants.OperatorCode01R02:
                    strAnswer = Claro.SIACU.Constants.Answer01R02;
                    break;
                case Claro.SIACU.Constants.OperatorCode01R03:
                    strAnswer = Claro.SIACU.Constants.Answer01R03;
                    break;
                case Claro.SIACU.Constants.OperatorCode01R04:
                    strAnswer = Claro.SIACU.Constants.Answer01R04;
                    break;
                case Claro.SIACU.Constants.OperatorCode01R05:
                    strAnswer = Claro.SIACU.Constants.Answer01R05;
                    break;
                case Claro.SIACU.Constants.OperatorCode01D01:
                    strAnswer = Claro.SIACU.Constants.Answer01D01;
                    break;
                case Claro.SIACU.Constants.OperatorCode01D02:
                    strAnswer = Claro.SIACU.Constants.Answer01D02;
                    break;
                case Claro.SIACU.Constants.OperatorCode01D03:
                    strAnswer = Claro.SIACU.Constants.Answer01D03;
                    break;
                case Claro.SIACU.Constants.OperatorCode01A03:
                    strAnswer = Claro.SIACU.Constants.Answer01A03;
                    break;
                case Claro.SIACU.Constants.OperatorCode01A04:
                    strAnswer = Claro.SIACU.Constants.Answer01A04;
                    break;
                case Claro.SIACU.Constants.OperatorCode01A05:
                    strAnswer = Claro.SIACU.Constants.Answer01A05;
                    break;
                case Claro.SIACU.Constants.OperatorCode01R06:
                    strAnswer = Claro.SIACU.Constants.Answer01R06;
                    break;
                case Claro.SIACU.Constants.OperatorCode01A06:
                    strAnswer = Claro.SIACU.Constants.Answer01A06;
                    break;
                case Claro.SIACU.Constants.OperatorCode01R07:
                    strAnswer = Claro.SIACU.Constants.Answer01R07;
                    break;
                case Claro.Constants.NumberTwentyString:
                    strAnswer = Claro.SIACU.Constants.Answer20;
                    break;
                case Claro.Constants.NumberTwentyOneString:
                    strAnswer = Claro.SIACU.Constants.Answer21;
                    break;
                case Claro.Constants.NumberTwentyTwoString:
                    strAnswer = Claro.SIACU.Constants.Answer22;
                    break;
            }
            return strAnswer;
        }

        /// <summary>
        /// Método para obtener triaciones.
        /// </summary>
        /// <param name="objTriacionRequest">Contiene id de contrato.</param>
        /// <returns>Devuelve objeto TriacionResponse con información de triaciones.</returns>
        public static Entity.Transac.Service.Postpaid.GetTriations.StriationsResponse GetTriaciones(Claro.SIACU.Entity.Transac.Service.Postpaid.GetTriations.StriationsRequest objTriacionRequest)
        {
            Entity.Transac.Service.Postpaid.GetTriations.StriationsResponse objTriacionResponse = new Entity.Transac.Service.Postpaid.GetTriations.StriationsResponse()
            {
                Striations = Claro.Web.Logging.ExecuteMethod<List<Striations>>(objTriacionRequest.Audit.Session, objTriacionRequest.Audit.Transaction, () => { return Data.Transac.Service.Postpaid.Postpaid.GetTriaciones(objTriacionRequest.Audit.Session, objTriacionRequest.Audit.Transaction, objTriacionRequest.ContractId); }),
            };

            string strPortadoPORTOUT = "";
            try
            {
                strPortadoPORTOUT = KEY.AppSettings("PortadoPORTOUT");
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objTriacionRequest.Audit.Session, objTriacionRequest.Audit.Transaction, ex.Message);
            }

            if (objTriacionResponse.Striations != null)
            {
                foreach (POSTPAID.Striations item in objTriacionResponse.Striations)
                {

                    string strTelephone = item.TELEFONO, strPort = "";
                    Entity.Transac.Service.Postpaid.GetPortability.PortabilityResponse objPortabilityResponse = new Entity.Transac.Service.Postpaid.GetPortability.PortabilityResponse()
                    {
                        ListPortability = Claro.Web.Logging.ExecuteMethod<List<Claro.SIACU.Entity.Transac.Service.Postpaid.Portability>>(objTriacionRequest.Audit.Session, objTriacionRequest.Audit.Transaction, () => { return Data.Transac.Service.Postpaid.Postpaid.GetPortability(objTriacionRequest.Audit.Session, objTriacionRequest.Audit.Transaction, strTelephone.Substring(Claro.Constants.NumberTwo), out strPort); })
                    };

                    if (objPortabilityResponse.ListPortability.Count > Claro.Constants.NumberZero && strPort == strPortadoPORTOUT)
                    {
                        item.PORTABILIDAD = strPort;
                    }


                }
                objTriacionResponse.Striations = OrderedByFactor(objTriacionResponse.Striations, objTriacionRequest);
            }
            return objTriacionResponse;
        }

        /// <summary>
        /// Método para ordenar por factor.
        /// </summary>
        /// <param name="listStriations">Contiene listado de triaciones.</param>
        /// <returns>Devuelve listado de triaciones</returns>
        public static List<POSTPAID.Striations> OrderedByFactor(List<POSTPAID.Striations> listStriations, Claro.SIACU.Entity.Transac.Service.Postpaid.GetTriations.StriationsRequest objTriacionRequest)
        {
            string strFactor1 = listStriations[0].FACTOR, strFactor2;
            int intCount = Claro.Constants.NumberZero;

            for (int i = 0; i < listStriations.Count; i++)
            {
                strFactor2 = listStriations[i].FACTOR;

                if (strFactor1 == strFactor2)
                {
                    intCount = intCount + Claro.Constants.NumberOne;
                }
                else
                {
                    intCount = Claro.Constants.NumberOne;
                }

                listStriations[i].NUM_TRIO = intCount;
                strFactor1 = strFactor2;
            }
            return listStriations;
        }


        public static POSTPAID.GetQueryAssociatedLines.QueryAssociatedLinesResponse GetListQueryAssociatedLines(POSTPAID.GetQueryAssociatedLines.QueryAssociatedLinesRequest objQueryAssociatedLinesRequest)
        {
            bool flagNum = false;
            string result = "";
            List<CallDetailSummary> Total = new List<CallDetailSummary>();
            DateTime formatStartDate = DateTime.ParseExact(objQueryAssociatedLinesRequest.Date_Ini, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
            DateTime formatEndDate = DateTime.ParseExact(objQueryAssociatedLinesRequest.Date_End, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);


            List<LineDetail> list = Claro.Web.Logging.ExecuteMethod<List<LineDetail>>
            (objQueryAssociatedLinesRequest.Audit.Session, objQueryAssociatedLinesRequest.Audit.Transaction,
                () =>
                {
                    return Data.Transac.Service.Postpaid.Postpaid.GetQueryAssociatedLines(objQueryAssociatedLinesRequest.Audit.Session,
                        objQueryAssociatedLinesRequest.Audit.Transaction, objQueryAssociatedLinesRequest.PhoneNumber,
                        formatStartDate, formatEndDate,
                        objQueryAssociatedLinesRequest.TypeQuery, ref result);
                });


            List<CallDetail> listCallDet = new List<CallDetail>();
            if (list.Count > 0)
            {
                foreach (LineDetail line in list)
                {
                    List<CallDetail> listCD = CallDetail_SubProcess(objQueryAssociatedLinesRequest, line, objQueryAssociatedLinesRequest.Date_Ini, objQueryAssociatedLinesRequest.Date_End, formatStartDate, formatEndDate);
                    if (listCD.Count > 0)
                    {
                        flagNum = true;
                        foreach (CallDetail det in listCD)
                        {
                            listCallDet.Add(det);
                        }


                    }
                }
            }
            else
            {
                string Date_Ini = objQueryAssociatedLinesRequest.Date_Ini.Replace("-", "");
                string Date_End = objQueryAssociatedLinesRequest.Date_End.Replace("-", "");

                string PhoneNumber = objQueryAssociatedLinesRequest.PhoneNumber;

                if (objQueryAssociatedLinesRequest.PhoneNumber.Substring(0, 2) != "51") PhoneNumber = "51" + objQueryAssociatedLinesRequest.PhoneNumber; ;

                listCallDet = Data.Transac.Service.Postpaid.Postpaid.GetIncomingCallDetail(objQueryAssociatedLinesRequest.Audit.Session, objQueryAssociatedLinesRequest.Audit.Transaction, objQueryAssociatedLinesRequest.PhoneNumber, objQueryAssociatedLinesRequest.Date_Ini, objQueryAssociatedLinesRequest.Date_End);

                if (listCallDet.Count > 0)
                {
                    flagNum = true;
                }

            }
            if (flagNum)
            {
                for (int i = 0; i < listCallDet.Count; i++)
                {
                    CallDetail item = listCallDet[i];
                    CallDetailSummary entity = new CallDetailSummary();

                    entity.NroOrd = (i + 1).ToString();
                    entity.MSISDN = item.MSISDN;
                    entity.CallDate = item.CALLDATE;
                    entity.CallTime = item.CALLTIME;
                    entity.CallNumber = item.CALLNUMBER;
                    entity.CallDuration = item.CALLDURATION;

                    Total.Add(entity);
                }
            }

            POSTPAID.GetQueryAssociatedLines.QueryAssociatedLinesResponse objQueryAssociatedLinesResponse =
                new POSTPAID.GetQueryAssociatedLines.QueryAssociatedLinesResponse();

            objQueryAssociatedLinesResponse.Total = Total;

            objQueryAssociatedLinesResponse.Result = result;

            return objQueryAssociatedLinesResponse;



        }

        private static List<CallDetail> CallDetail_SubProcess(POSTPAID.GetQueryAssociatedLines.QueryAssociatedLinesRequest request, LineDetail item, string Date_Ini, string Date_End, DateTime formatDateIni, DateTime formatDateEnd)
        {

            //List<IncomingCallDetail> list = new List<IncomingCallDetail>();
            List<CallDetail> listCD = new List<CallDetail>();
            string lDateini = "";
            string lDateEnd = "";


            if (item != null)
            {
                if (!(String.IsNullOrEmpty(item.StrDtFin)))
                {
                    lDateEnd = Date_End;
                }



                if (item.DtIni <= formatDateIni) lDateini = Date_Ini;
                if (item.DtFin <= formatDateEnd) lDateEnd = item.StrDtFin;

                if (item.DtIni >= formatDateIni) lDateini = item.StrDtIni;
                if (item.DtFin >= formatDateEnd) lDateEnd = Date_End;

                lDateini = lDateini.Replace("-", "");
                lDateEnd = lDateEnd.Replace("-", "");

                if (!(String.IsNullOrEmpty(item.DnNum)))
                {
                    if (item.DnNum.Substring(0, 2) != "51") item.DnNum = "51" + item.DnNum;
                    listCD = Data.Transac.Service.Postpaid.Postpaid.GetIncomingCallDetail(request.Audit.Session, request.Audit.Transaction, item.DnNum, lDateini, lDateEnd);
                }
                else
                {
                    //Pintar LOG  log.Info(string.Format("Error no hay captura de numero de teléfono")); 
                    listCD = new List<CallDetail>();
                }
            }
            else
            {
                listCD = new List<CallDetail>();
            }
            return listCD;
        }

        public static POSTPAID.GetDataLine.DataLineResponse GetDataLine(POSTPAID.GetDataLine.DataLineRequest objRequest)
        {
            string message = "";
            COMMON.Line dataLine = new COMMON.Line();
            POSTPAID.GetDataLine.DataLineResponse objResponse = new POSTPAID.GetDataLine.DataLineResponse()
            {
                StrResponse = Claro.Web.Logging.ExecuteMethod<string>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Postpaid.Postpaid.GetDataline(objRequest.Audit.Session, objRequest.Audit.Transaction, int.Parse(objRequest.ContractID), ref dataLine, ref message);
                })
            };

            objResponse.Message = message;
            objResponse.DataLine = dataLine;
            return objResponse;
        }

        public static POSTPAID.GetAmountIncomingCall.AmountIncomingCallResponse GetAmountIncomingCall(POSTPAID.GetAmountIncomingCall.AmountIncomingCallRequest objRequest)
        {
            string Message = "";
            POSTPAID.GetAmountIncomingCall.AmountIncomingCallResponse objResponse = new POSTPAID.GetAmountIncomingCall.AmountIncomingCallResponse()
            {
                ListAmountIncomingCall = Claro.Web.Logging.ExecuteMethod<List<AmountIncomingCall>>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Postpaid.Postpaid.GetAmountIncomingCall(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.Name, ref Message);
                })
            };
            objResponse.Message = Message;
            return objResponse;
        }

        public static List<ListItem> GetDocumentType(string strIdSession, string strTransaction, string strCodCargaDdl) {
            List<ListItem> listItem = null;

            listItem = Claro.Web.Logging.ExecuteMethod<List<ListItem>>(strIdSession, strTransaction, () =>
                {
                    return Data.Transac.Service.Postpaid.Postpaid.GetDocumentType(strIdSession, strTransaction, strCodCargaDdl);
                });
            return listItem;
        }

        public static POSTPAID.GetUpdateInteraction.UpdateInteractionResponse GetUpdateInteraction(POSTPAID.GetUpdateInteraction.UpdateInteractionRequest objRequest){
            
            string rFlagInsercion ="";
            string rMsgText = "";
            bool blnResult;

            POSTPAID.GetUpdateInteraction.UpdateInteractionResponse objResponse = new POSTPAID.GetUpdateInteraction.UpdateInteractionResponse(){
                ProcessSuccess = Claro.Web.Logging.ExecuteMethod<bool>(objRequest.Audit.Session, objRequest.Audit.Transaction,()=>
                {
                    return Claro.SIACU.Data.Transac.Service.Postpaid.Postpaid.UpdateInteraction(objRequest.Audit.Session, objRequest.Audit.Transaction,objRequest.InteractId,objRequest.Text,objRequest.Order, out rFlagInsercion, out rMsgText);
                })
            };

            objResponse.FlagInsertion = rFlagInsercion;
            objResponse.MessageText = rMsgText;
            blnResult = objResponse.ProcessSuccess;
            return objResponse;
        }

        public static POSTPAID.GetAdjustForClaims.AdjustForClaimsResponse GetAdjustForClaims(POSTPAID.GetAdjustForClaims.AdjustForClaimsRequest objRequest)
        {
            double iReturn = 0;
            POSTPAID.GetAdjustForClaims.AdjustForClaimsResponse objResponse = new POSTPAID.GetAdjustForClaims.AdjustForClaimsResponse()
            {
                Result = Claro.Web.Logging.ExecuteMethod<double>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Claro.SIACU.Data.Transac.Service.Postpaid.Postpaid.InsertAdjustForClaim(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.CodClient, objRequest.CodOCC, objRequest.DateVig, objRequest.NumQuota, objRequest.Amount, objRequest.Comment);
                })
            };

            iReturn = objResponse.Result;
            return objResponse;
        }

        public static bool AlignTransactionService(string strIdSession, string strTransaction, string strContractID)
        {
            bool blnResponse = Claro.Web.Logging.ExecuteMethod<bool>(strIdSession, strTransaction, () =>
            {
                return Data.Transac.Service.Postpaid.Postpaid.AlignTransactionService(strTransaction, strTransaction, strContractID);
            });

            return blnResponse;
        }

        public static bool AlignCodID(string strIdSession, string strTransaction, string strContractID)
        {
            bool blnResponse = Claro.Web.Logging.ExecuteMethod<bool>(strIdSession, strTransaction, () =>
            {
                return Data.Transac.Service.Postpaid.Postpaid.AlignCodID(strIdSession, strTransaction, strContractID);
            });

            return blnResponse;
        }
        public static ApprovalBusinessCreditLimitResponse GetApprovalBusinessCreditLimitBusinessAccount(ApprovalBusinessCreditLimitRequest objRequest)
        {
            bool bResult = false;
            decimal dNewCharge = 0;
            decimal dMaxCharge = 0;
            Int64 dError = 0;
            string strErrorMessage = "";
            ApprovalBusinessCreditLimitResponse objResponse = new ApprovalBusinessCreditLimitResponse()
            {
                result = Claro.Web.Logging.ExecuteMethod<bool>(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Claro.SIACU.Data.Transac.Service.Postpaid.Postpaid
                            .GetApprovalBusinessCreditLimitBusinessAccount(objRequest.Audit.Session,
                                objRequest.Audit.Transaction,
                                objRequest.Account, objRequest.Contract, objRequest.Service, out dNewCharge, out dMaxCharge, out dError, out strErrorMessage);
                    })
            };
            bResult = true;
            objResponse.result = bResult;
            objResponse.NewCharge = dNewCharge;
            objResponse.MaxCharge = dMaxCharge;
            objResponse.Error = dError;
            objResponse.ErrorMessage = strErrorMessage;
            return objResponse;
        }

        public static UserExistsBSCSResponse GEtUserExistsBSCS(UserExistsBSCSRequest objRequest)
        {
            int result;
            UserExistsBSCSResponse objResponse = new UserExistsBSCSResponse()
            {
                Result = Claro.Web.Logging.ExecuteMethod<int>(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Claro.SIACU.Data.Transac.Service.Postpaid.Postpaid.UserExistsBSCS(
                            objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.Users);
                    })
            };

            result = objResponse.Result;
            return objResponse;
        }

        public static ConsultServiceResponse GetConsultService(ConsultServiceRequest objRequest)
        {
            string strErrorNum = string.Empty;
            string strServ = string.Empty;

            int intSnCode = 0;
            int intSpCode = 0;
            string strErrorMsg = string.Empty;
            ConsultServiceResponse objResponse = new ConsultServiceResponse()
            {
                Result = Claro.Web.Logging.ExecuteMethod<string>(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Claro.SIACU.Data.Transac.Service.Postpaid.Postpaid.ConsultServices(
                            objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.CodId,
                            objRequest.CodServ, out intSnCode, out intSpCode, out strErrorMsg, out strErrorNum, out strServ);
                    })
            };


            objResponse.Serv  = strServ;
            objResponse.SnCode = intSnCode;
            objResponse.SpCode = intSpCode;
            objResponse.ErrorNum = strErrorNum;
            objResponse.ErrorMessage = strErrorMsg;

            return objResponse;
        }

        public static ModifyServiceQuotAmountResponse GetModifyServiceQuotAmount(ModifyServiceQuotAmountRequest objRequest)
        {
           string strResult = string.Empty;
           ModifyServiceQuotAmountResponse objResponse = new ModifyServiceQuotAmountResponse()
           {
               Result = Claro.Web.Logging.ExecuteMethod<string>(objRequest.Audit.Session, objRequest.Audit.Transaction,
                   () =>
                   {
                       return Claro.SIACU.Data.Transac.Service.Postpaid.Postpaid.ModifyServiceQuotAmountresponse(
                           objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.IntCodId,
                           objRequest.IntSnCode,
                           objRequest.IntSpCode, objRequest.DCost, objRequest.IntPeriod, out strResult
                       );
                   })
           };

            objResponse.Result = strResult;
            return objResponse;
        }

        public static TypeTransactionBRMSResponse GetTypeTransactionBRMS(TypeTransactionBRMSRequest objRequest)
        {
            TypeTransactionBRMSResponse objResponse = new TypeTransactionBRMSResponse();
            objResponse = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
            {
                return Data.Transac.Service.Postpaid.Postpaid.GetTypeTransactionBRMS(objRequest);
            });
            return objResponse;
        }

        public static ActDesServProgResponse GetActDesServProg(ActDesServProgRequest objRequest)
        {
            ActDesServProgResponse objResponse = new ActDesServProgResponse();
            objResponse = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
            {
                return Data.Transac.Service.Postpaid.Postpaid.ActDesServProg(objRequest);
            });
            return objResponse;
        }

        public static ServiceByContractResponse GetServiceByContract(ServiceByContractRequest objRequest)
        {
            ServiceByContractResponse  objResponse = new ServiceByContractResponse();
            objResponse = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
            {
                return Data.Transac.Service.Postpaid.Postpaid.GetServiceByContract(objRequest);
            });
            return objResponse;
        }

        public static POSTPAID.GetServicesDTH.ServicesDTHResponse GetServicesDTH(POSTPAID.GetServicesDTH.ServicesDTHRequest objRequest)
        {
            POSTPAID.GetServicesDTH.ServicesDTHResponse objResponse = new POSTPAID.GetServicesDTH.ServicesDTHResponse();
            try
            {
                objResponse = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Data.Transac.Service.Postpaid.Postpaid.GetServicesDTH(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.intCoId, objRequest.strMsisdn);
                    });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }
            
            return objResponse;
        }

        public static POSTPAID.GetValidateActDesServProg.ValidateActDesServProgResponse GetValidateActDesServProg(
            POSTPAID.GetValidateActDesServProg.ValidateActDesServProgRequest objRequest)
        {
            POSTPAID.GetValidateActDesServProg.ValidateActDesServProgResponse objResponse = new ValidateActDesServProgResponse();
            try
            {
                objResponse = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Postpaid.Postpaid.ValidateActDesServProg(objRequest);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }
            return objResponse;
        }

        public static ServiceBSCSResponse GetServiceBSCS(ServiceBSCSRequest objRequest)
        {
            ServiceBSCSResponse objResponse = new ServiceBSCSResponse();
            try
            {
                objResponse = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Postpaid.Postpaid.ServiceBSCS(objRequest);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }
            return objResponse;
        }

        public static InsertTraceabilityResponse GetInsertTraceability(InsertTraceabilityRequest objRequest)
        {

            InsertTraceabilityResponse objResponse = new InsertTraceabilityResponse();
            try
            {
                objResponse = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Postpaid.Postpaid.GetInsertTraceability(objRequest);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }
            return objResponse;

        }

        public static BiometricConfigurationResponse GetBiometricConfiguration(BiometricConfigurationRequest objRequest)
        {
            BiometricConfigurationResponse objResponse = new BiometricConfigurationResponse();
            try
            {
                objResponse = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Postpaid.Postpaid.GetBiometricConfiguration(objRequest);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }
            return objResponse;
        }

        public static SignDocumentResponse GetSignDocument(SignDocumentRequest objRequest)
        {
            SignDocumentResponse objResponse = new SignDocumentResponse();
            try
            {
                objResponse = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Postpaid.Postpaid.GetSignDocument(objRequest);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }
            return objResponse;

        }

        public static POSTPAID.GetDataCustomer.DataCustomerResponse GetDataCustomer(POSTPAID.GetDataCustomer.DataCustomerRequest objRequest,int flag)
        {
            POSTPAID.GetDataCustomer.DataCustomerResponse objResponse = new POSTPAID.GetDataCustomer.DataCustomerResponse();
            Claro.SIACU.Entity.Transac.Service.Common.Client objcliente = new Claro.SIACU.Entity.Transac.Service.Common.Client();

            string strMessage = "";
            if (flag != 1)
            {
            objResponse.StrResponse = Web.Logging.ExecuteMethod<string>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
            {
                return Data.Transac.Service.Postpaid.Postpaid.GetDataCustomer(objRequest.strIdSession, objRequest.strTransaccion, objRequest.strcustomerid, objRequest.strtelefono, ref objcliente, ref strMessage);
            });
            }
            else {
                objResponse.StrResponse = Web.Logging.ExecuteMethod<string>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Postpaid.Postpaid.GetDataCustomer2(objRequest.strIdSession, objRequest.strTransaccion,objRequest.Audit.IPAddress,objRequest.Audit.ApplicationName,objRequest.Audit.UserName, objRequest.strcustomerid, objRequest.strtelefono, ref objcliente, ref strMessage);
                });
            }
            

            objResponse.Cliente = objcliente;
            objResponse.Message = strMessage;


            return objResponse;
        }

        public static List<ListItem> GetMotivoCambio(string sesion, string transaction,string pid_parametro, string rMsgText)
        {
            List<ListItem> listItem = null;

            listItem = Claro.Web.Logging.ExecuteMethod<List<ListItem>>(sesion, transaction, () =>
            {
                return Data.Transac.Service.Postpaid.Postpaid.GetMotivoCambio(sesion, transaction, pid_parametro, rMsgText);
            });
            return listItem;
        }

        public static string ValidateEnvioxMail(string strIdSession, string strIdTransaccion, string strCustomerID)
        {            
            string strRespuesta = "";
            strRespuesta = Claro.Web.Logging.ExecuteMethod<string>(strIdSession, strIdTransaccion, () =>
            {
                return Data.Transac.Service.Postpaid.Postpaid.ValidateMail(strIdSession, strIdTransaccion, strCustomerID);
            });
            return strRespuesta;
        }

        public static UpdateChangeDataResponse UpdateNameCustomer(string strIdTransaccion, string strIpAplicacion, string strAplicacion, string strUsrApp, Entity.Transac.Service.Common.Client oCliente)
        {
            string strRespuesta = "";
            int intSeq_out = 0;
            UpdateChangeDataResponse objResponse = new UpdateChangeDataResponse();

            strRespuesta = Claro.Web.Logging.ExecuteMethod<string>(strUsrApp, strIdTransaccion, () =>
            {
                return Data.Transac.Service.Postpaid.Postpaid.UpdateNameCustomer(strIdTransaccion, strIpAplicacion, strAplicacion, strUsrApp, oCliente, ref intSeq_out);
            });

            if (!string.IsNullOrEmpty(strRespuesta))
            {
                objResponse.ResultCode = strRespuesta;
                objResponse.SequenceCode = intSeq_out;
            }     

            return objResponse;
        }

        public static UpdateChangeDataResponse UpdateDataMinorCustomer(string strIdTransaccion, string strIpAplicacion, string strAplicacion, string strUsrApp, Entity.Transac.Service.Common.Client oCliente, int intSeq_in)
        {

            string strRespuesta = "";
            int intSeq_out = 0;
            UpdateChangeDataResponse objResponse = new UpdateChangeDataResponse();

            strRespuesta = Claro.Web.Logging.ExecuteMethod<string>(strUsrApp, strIdTransaccion, () =>
            {
                return Data.Transac.Service.Postpaid.Postpaid.UpdateDataMinorCustomer(strIdTransaccion, strIpAplicacion, strAplicacion, strUsrApp, oCliente, intSeq_in, ref intSeq_out);
            });

            if (!string.IsNullOrEmpty(strRespuesta))
            {
                objResponse.ResultCode = strRespuesta;
                objResponse.SequenceCode = intSeq_out;
            }

            return objResponse;
        }

        public static string UpdateDataCustomerCLF(string strIdTransaccion, string strIpAplicacion, string strAplicacion, string strUsrApp, Entity.Transac.Service.Common.Client oCliente)
        {

            string strRespuesta = "";
            strRespuesta = Claro.Web.Logging.ExecuteMethod<string>(strUsrApp, strIdTransaccion, () =>
            {
                return Data.Transac.Service.Postpaid.Postpaid.UpdateDataCustomerCLF(strIdTransaccion, strIpAplicacion, strAplicacion, strUsrApp, oCliente);
            });
            return strRespuesta;
        }

        public static UpdateChangeDataResponse UpdateAddressCustomer(string strIdTransaccion, string strIpAplicacion, string strAplicacion, string strUsrApp, Entity.Transac.Service.Common.Client oCliente, string tipoDireccion, int intSeq_in)
        {
            string strRespuesta = "";
            int intSeq_out = 0;
            UpdateChangeDataResponse objResponse = new UpdateChangeDataResponse();

            strRespuesta = Claro.Web.Logging.ExecuteMethod<string>(strUsrApp, strIdTransaccion, () =>
            {
                return Data.Transac.Service.Postpaid.Postpaid.UpdateAddressCustomer(strIdTransaccion, strIpAplicacion, strAplicacion, strUsrApp, oCliente, tipoDireccion, intSeq_in, ref intSeq_out);
            });
            if (!string.IsNullOrEmpty(strRespuesta))
            {
                objResponse.ResultCode = strRespuesta;
                objResponse.SequenceCode = intSeq_out;
            }

            return objResponse;
        }
        public static string UpdateDataCustomerPClub(string strIdTransaccion, string strIpAplicacion, string strAplicacion, string strUsrApp, Entity.Transac.Service.Common.Client oCliente)
        {

            string strRespuesta = "";
            strRespuesta = Claro.Web.Logging.ExecuteMethod<string>(strUsrApp, strIdTransaccion, () =>
            {
                return Data.Transac.Service.Postpaid.Postpaid.UpdateDataCustomerPClub(strIdTransaccion, strIpAplicacion, strAplicacion, strUsrApp, oCliente);
            });
            return strRespuesta;
        }
        public static int registrarTransaccionSiga(string strIdTransaccion, string strIpAplicacion, string strAplicacion, string strUsrApp, Entity.Transac.Service.Fixed.TransactionSiga oTransaction)
        {

            int strRespuesta = 0;
            strRespuesta = Claro.Web.Logging.ExecuteMethod<int>(strUsrApp, strIdTransaccion, () =>
            {
                return Data.Transac.Service.Postpaid.Postpaid.registrarTransaccionSiga(strIdTransaccion, strIpAplicacion, strAplicacion, strUsrApp, oTransaction);
            });
            return strRespuesta;
        }


      
    }
}
