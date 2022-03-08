using System;
using System.Collections.Generic;
using FIXED = Claro.SIACU.Entity.Transac.Service.Fixed;

namespace Claro.SIACU.Business.Transac.Service.Fixed
{
    public class ChangePhoneNumber
    {
        public static FIXED.GetPhoneNumber.PhoneNumberResponse GetExecuteChangeNumber(FIXED.GetPhoneNumber.PhoneNumberRequest objRequest)
        {
            string strResponseCode = string.Empty;
            string strResponseMessage = string.Empty;
            string strPDFRoute = string.Empty;
            FIXED.GetPhoneNumber.PhoneNumberResponse objResponse = new FIXED.GetPhoneNumber.PhoneNumberResponse()
            {
                NEW_PHONE = Claro.Web.Logging.ExecuteMethod<string>(objRequest.Audit.Session, objRequest.Audit.Transaction, () => 
                { 
                    return Data.Transac.Service.Fixed.ChangePhoneNumber.GetExecuteChangeNumber(objRequest, null, ref strPDFRoute, ref strResponseCode, ref strResponseMessage); 
                })
            };

            objResponse.ROUTE_PDF = strPDFRoute;
            objResponse.RESPONSE_CODE = strResponseCode;
            objResponse.RESPONSE_MESSAGE = strResponseMessage;

            return objResponse;
        }
    }
}
