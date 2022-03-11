using Claro.SIACU.Entity;
using Claro.SIACU.Entity.Transac.Service.Postpaid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KEY = Claro.ConfigurationManager;
using POSTPAID = Claro.SIACU.Entity.Transac.Service.Postpaid;

namespace Claro.SIACU.Business.Transac.Service.Postpaid
{
    public class ChangePhoneNumber
    {
        public static POSTPAID.GetHLR.HLRResponse GetHLRLocation(POSTPAID.GetHLR.HLRRequest objHLRRequest)
        {
            POSTPAID.GetHLR.HLRResponse objHLRResponse = new POSTPAID.GetHLR.HLRResponse();

            try
            {
                if (objHLRRequest.RANGE_TYPE == Claro.Constants.NumberOneString)
                {
                    objHLRResponse.LOCATION = Claro.Web.Logging.ExecuteMethod<int>(objHLRRequest.Audit.Session, objHLRRequest.Audit.Transaction, () => { return Data.Transac.Service.Postpaid.ChangePhoneNumber.GetHLRUbicationINSI(objHLRRequest.Audit.Session, objHLRRequest.Audit.Transaction, objHLRRequest.PHONE_NUMBER); });
                }
                else if (objHLRRequest.RANGE_TYPE == Claro.Constants.NumberTwoString)
                {
                    objHLRResponse.LOCATION = Claro.Web.Logging.ExecuteMethod<int>(objHLRRequest.Audit.Session, objHLRRequest.Audit.Transaction, () => { return Data.Transac.Service.Postpaid.ChangePhoneNumber.GetHLRUbicationPhone(objHLRRequest.Audit.Session, objHLRRequest.Audit.Transaction, objHLRRequest.PHONE_NUMBER); });
                }

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objHLRRequest.Audit.Session, objHLRRequest.Audit.Transaction, ex.Message);
                throw new Exception(ex.Message);
            }
           
            return objHLRResponse;
        }

        public static POSTPAID.ChangePhoneNumber ValidateChangeNumberTransaction(string strIdSession, string strTransactionID, string strContract, string strFlagFidelize)
        {
            string strCodeError = "";
            string strMessage = "";
            bool Response = false;
            POSTPAID.ChangePhoneNumber objChangePhoneNumber = null;

            Response = Claro.Web.Logging.ExecuteMethod<bool>(strIdSession, strTransactionID, () =>
            {
                return Data.Transac.Service.Postpaid.ChangePhoneNumber.ValidateChangeNumberTransaction(strIdSession, strTransactionID, strContract, strFlagFidelize, ref strCodeError, ref strMessage);
            });

            objChangePhoneNumber = new POSTPAID.ChangePhoneNumber();
            objChangePhoneNumber.RESPONSE_CODE = strCodeError;
            objChangePhoneNumber.RESPONSE_MESSAGE = strMessage;
            objChangePhoneNumber.RESPONSE = Response;
            return objChangePhoneNumber;
        }

        public static POSTPAID.GetChangePhoneNumber.ChangePhoneNumberResponse GetAvailableLines(POSTPAID.GetChangePhoneNumber.ChangePhoneNumberRequest objChangePhoneNumberRequest)
        {
            POSTPAID.GetChangePhoneNumber.ChangePhoneNumberResponse objChangePhoneNumberResponse = new POSTPAID.GetChangePhoneNumber.ChangePhoneNumberResponse();
            string strMenssageResponse = "";

            objChangePhoneNumberResponse.LstSimCardPhone = Claro.Web.Logging.ExecuteMethod<List<POSTPAID.SimCardPhone>>(objChangePhoneNumberRequest.Audit.Session, objChangePhoneNumberRequest.Audit.Transaction, () =>
            {
                return Data.Transac.Service.Postpaid.ChangePhoneNumber.GetAvailableLines(objChangePhoneNumberRequest.Audit.Session, objChangePhoneNumberRequest.Audit.Transaction, objChangePhoneNumberRequest.Audit.IPAddress, objChangePhoneNumberRequest.Audit.ApplicationName, objChangePhoneNumberRequest.Audit.UserName,objChangePhoneNumberRequest.NUMBER_PHONES,objChangePhoneNumberRequest.CLASIFICATION_RED,objChangePhoneNumberRequest.CUSTOMER_TYPE,objChangePhoneNumberRequest.NATIONAL_CODE, objChangePhoneNumberRequest.PHONE_TYPE, objChangePhoneNumberRequest.HLR, objChangePhoneNumberRequest.PHONE, ref strMenssageResponse);
            });

            objChangePhoneNumberResponse.RESPONSE_MESSAGE = strMenssageResponse;

            return objChangePhoneNumberResponse;
        }

        public static bool ValidateChangeNumberBSCS(string strIdSession, string strTransactionID, string strSerialNum, string strDnNum, int intEstado)
        {
            bool Response = false;

            Response = Claro.Web.Logging.ExecuteMethod<bool>(strIdSession, strTransactionID, () =>
            {
                return Data.Transac.Service.Postpaid.ChangePhoneNumber.ValidateChangeNumberBSCS(strIdSession, strTransactionID, strSerialNum, strDnNum, intEstado);
            });

            return Response;
        }

        public static POSTPAID.GetChangePhoneNumber.ChangePhoneNumberResponse ExecuteChangeNumber(POSTPAID.GetChangePhoneNumber.ChangePhoneNumberRequest objChangePhoneNumberRequest)
        {
            POSTPAID.GetChangePhoneNumber.ChangePhoneNumberResponse objChangePhoneNumberResponse = new POSTPAID.GetChangePhoneNumber.ChangePhoneNumberResponse();
            string strCodeResponse = "";
            string strMessage = "";

            objChangePhoneNumberResponse.RESPONSE = Claro.Web.Logging.ExecuteMethod<bool>(objChangePhoneNumberRequest.Audit.Session, objChangePhoneNumberRequest.Audit.Transaction, () =>
            {
                return Data.Transac.Service.Postpaid.ChangePhoneNumber.ExecuteChangeNumber(objChangePhoneNumberRequest.Audit.Session, objChangePhoneNumberRequest.Audit.Transaction, objChangePhoneNumberRequest.CONTRACT, objChangePhoneNumberRequest.NEW_PHONE, objChangePhoneNumberRequest.EST_TRASLADO, objChangePhoneNumberRequest.COST, objChangePhoneNumberRequest.FLAG_FIDELIZE, objChangePhoneNumberRequest.CURRENT_PHONE, objChangePhoneNumberRequest.APPLICATION_ID, objChangePhoneNumberRequest.APPLICATION_PWD, objChangePhoneNumberRequest.Audit.UserName, objChangePhoneNumberRequest.COSTMEDNO, objChangePhoneNumberRequest.FLAG_CHANGECHIP,objChangePhoneNumberRequest.FLAG_LOCUTION, ref strCodeResponse,ref  strMessage);
            });

            objChangePhoneNumberResponse.RESPONSE_CODE = strCodeResponse;
            objChangePhoneNumberResponse.RESPONSE_MESSAGE = strMessage;

            return objChangePhoneNumberResponse;
        }

        public static POSTPAID.GetChangePhoneNumber.ChangePhoneNumberResponse RollbackChangeNumber(POSTPAID.GetChangePhoneNumber.ChangePhoneNumberRequest objChangePhoneNumberRequest)
        {
            POSTPAID.GetChangePhoneNumber.ChangePhoneNumberResponse objChangePhoneNumberResponse = new POSTPAID.GetChangePhoneNumber.ChangePhoneNumberResponse();
            string strMessage = "";
            objChangePhoneNumberResponse.RESPONSE = Claro.Web.Logging.ExecuteMethod<bool>(objChangePhoneNumberRequest.Audit.Session, objChangePhoneNumberRequest.Audit.Transaction, () =>
            {
                return Data.Transac.Service.Postpaid.ChangePhoneNumber.RollbackChangeNumber(objChangePhoneNumberRequest.Audit.Session, objChangePhoneNumberRequest.Audit.Transaction, objChangePhoneNumberRequest.Audit.IPAddress, objChangePhoneNumberRequest.Audit.ApplicationName, objChangePhoneNumberRequest.Audit.UserName,objChangePhoneNumberRequest.CURRENT_PHONE, objChangePhoneNumberRequest.NEW_PHONE,objChangePhoneNumberRequest.USER, ref strMessage);
            });

            objChangePhoneNumberResponse.RESPONSE_MESSAGE = strMessage;

            return objChangePhoneNumberResponse;
        }

        public static string DeleteUserHistory(string strIdSession, string strTransactionID, string strPhone, string strMotive)
        {
            string strResponse = Claro.Web.Logging.ExecuteMethod<string>(strIdSession, strTransactionID, () =>
            {
                return Data.Transac.Service.Postpaid.ChangePhoneNumber.DeleteUserHistory(strIdSession, strTransactionID, strPhone, strMotive);
            });

            return strResponse;
        }

        public static POSTPAID.GetChangePhoneNumber.ChangePhoneNumberResponse UpdatePhoneNumber(POSTPAID.GetChangePhoneNumber.ChangePhoneNumberRequest objChangePhoneNumberRequest)
        {
            POSTPAID.GetChangePhoneNumber.ChangePhoneNumberResponse objChangePhoneNumberResponse = new POSTPAID.GetChangePhoneNumber.ChangePhoneNumberResponse();
            string strMessageResponse = "";

            objChangePhoneNumberResponse.RESPONSE_CODE = Claro.Web.Logging.ExecuteMethod<string>(objChangePhoneNumberRequest.Audit.Session, objChangePhoneNumberRequest.Audit.Transaction, () =>
            {
                return Data.Transac.Service.Postpaid.ChangePhoneNumber.UpdatePhoneNumber(objChangePhoneNumberRequest.Audit.Session, objChangePhoneNumberRequest.Audit.Transaction, objChangePhoneNumberRequest.Audit.IPAddress, objChangePhoneNumberRequest.Audit.ApplicationName, objChangePhoneNumberRequest.Audit.UserName, objChangePhoneNumberRequest.CURRENT_PHONE, objChangePhoneNumberRequest.NEW_PHONE, objChangePhoneNumberRequest.USER, ref strMessageResponse);
            });

            objChangePhoneNumberResponse.RESPONSE_MESSAGE = strMessageResponse;

            return objChangePhoneNumberResponse;
        }
    }
}
