using System;
using System.Collections.Generic;
using EntitiesFixed = Claro.SIACU.Entity.Transac.Service.Fixed;
using KEY = Claro.ConfigurationManager;
using ConstantsHFC = Claro.SIACU.Transac.Service.Constants;

namespace Claro.SIACU.Business.Transac.Service.Fixed
{
    public class CallsDetail
    {
        public static EntitiesFixed.GetCustomerPhone.CustomerPhoneResponse GetCustomerPhone(EntitiesFixed.GetCustomerPhone.CustomerPhoneRequest objRequest)
        {
            var objResponse = new EntitiesFixed.GetCustomerPhone.CustomerPhoneResponse();
            try
            {
                var lstCustomerPhone = Claro.Web.Logging.ExecuteMethod<List<EntitiesFixed.GenericItem>>(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Data.Transac.Service.Fixed.CallsDetail.GetCustomerPhone(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.IdContract);
                    });

                objResponse.LstCustomerPhone = lstCustomerPhone;
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }

        public static EntitiesFixed.GetCallDetailDB1.CallDetailDB1Response GetCallDetailDB1(EntitiesFixed.GetCallDetailDB1.CallDetailDB1Request objRequest)
        {
            var objResponse = new EntitiesFixed.GetCallDetailDB1.CallDetailDB1Response();
            var vTOTAL = string.Empty;
            var MSGERROR = string.Empty;

            try
            {
                var lstPhoneCall = Claro.Web.Logging.ExecuteMethod<List<EntitiesFixed.PhoneCall>>(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Data.Transac.Service.Fixed.CallsDetail.GetCallDetailDB1(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.vCONTRATOID, objRequest.vFECHA_INI, objRequest.vFECHA_FIN, objRequest.vSeguridad, ref vTOTAL, ref MSGERROR);
                    });

                objResponse.LstPhoneCall = lstPhoneCall;
                objResponse.vTOTAL = vTOTAL;
                objResponse.MSGERROR = MSGERROR;

            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }

        public static EntitiesFixed.GetCallDetail.CallDetailResponse GetCallDetail(EntitiesFixed.GetBpelCallDetail.BpelCallDetailRequest objRequest)
        {
            var objResponse = new EntitiesFixed.GetCallDetail.CallDetailResponse();
            var vTOTAL = string.Empty;

            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<EntitiesFixed.GetCallDetail.CallDetailResponse>(objRequest.Audit.Session, objRequest.Audit.Transaction,
                () =>
                {
                    return Data.Transac.Service.Fixed.CallsDetail.GetCallDetail(objRequest);
                });

                objResponse.VTotal = vTOTAL;
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }

        public static EntitiesFixed.GetFacturePDI.FacturePDIResponse GetFacturePDI(EntitiesFixed.GetFacturePDI.FacturePDIRequest objRequest)
        {
            var objResponse = new EntitiesFixed.GetFacturePDI.FacturePDIResponse();

            try
            {
                var lstBilledCallsDetailPDI = Claro.Web.Logging.ExecuteMethod<List<EntitiesFixed.GenericItem>>(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Data.Transac.Service.Fixed.CallsDetail.GetFacturePDI(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.strCodeCustomer);
                    });

                objResponse.LstGenericItem = lstBilledCallsDetailPDI;
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }

        public static EntitiesFixed.GetFactureDBTO.FactureDBTOResponse GetFactureDBTO(EntitiesFixed.GetFactureDBTO.FactureDBTORequest objRequest)
        {
            var objResponse = new EntitiesFixed.GetFactureDBTO.FactureDBTOResponse();

            try
            {
                var lstFactureDbto = Claro.Web.Logging.ExecuteMethod<List<EntitiesFixed.GenericItem>>(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Data.Transac.Service.Fixed.CallsDetail.GetFactureDBTO(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.strCodeCustomer);
                    });

                objResponse.LstGenericItem = lstFactureDbto;
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }

        public static EntitiesFixed.GetCallDetail.CallDetailResponse GetBilledCallsDetailDB1_BSCS(EntitiesFixed.GetCallDetail.CallDetailRequest objRequest)
        {
            var objResponse = new EntitiesFixed.GetCallDetail.CallDetailResponse();
            var vTOTAL = string.Empty;

            try
            {
                var lstPhoneCall = Claro.Web.Logging.ExecuteMethod<List<EntitiesFixed.PhoneCall>>(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Data.Transac.Service.Fixed.CallsDetail.GetBilledCallsDetailDB1_BSCS(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.vCONTRATOID, objRequest.vFECHA_INI, objRequest.vFECHA_FIN, objRequest.vFECHA_AYER, objRequest.vFECHA_AHORA, objRequest.vSeguridad, ref vTOTAL);
                    });

                objResponse.LstPhoneCall = lstPhoneCall;
                objResponse.VTotal = vTOTAL;

            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }

        public static EntitiesFixed.GetBpelCallDetail.BpelCallDetailResponse GetBilledCallsDetailHfC(EntitiesFixed.GetBpelCallDetail.BpelCallDetailRequest objRequest)
        {
            var objResponse = new  EntitiesFixed.GetBpelCallDetail.BpelCallDetailResponse();
            var vTOTAL = string.Empty;

            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<EntitiesFixed.GetBpelCallDetail.BpelCallDetailResponse>(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Data.Transac.Service.Fixed.CallsDetail.GetBilledCallsDetailHfC(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest);
                    });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }
    }
}
