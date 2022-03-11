using Claro.SIACU.Entity.Transac.Service.Coliving;
using Claro.SIACU.Entity.Transac.Service.Coliving.GetDataBilling;
using Claro.SIACU.Entity.Transac.Service.Coliving.GetDataHistoryClient;
using Claro.SIACU.Entity.Transac.Service.Coliving.GetUpdateDataClient;
using Claro.SIACU.Entity.Transac.Service.Coliving.PostHistoryClient;
using Claro.SIACU.Entity.Transac.Service.Coliving.PutBillingAddress;
using Claro.SIACU.Entity.Transac.Service.Coliving.PutDataClient;
using System;
using System.Collections.Generic;
using DATAHIST = Claro.SIACU.Entity.Transac.Service.Coliving;
using Claro.SIACU.Entity.Transac.Service.Postpaid.GetDataCustomerCBIO.Request;
using Claro.SIACU.Entity.Transac.Service.Postpaid.GetDataCustomerCBIO.Response;

namespace Claro.SIACU.Business.Transac.Service.Coliving
{
    public class ChangeData
    {
        /// <summary>
        /// Actualizacion de datos del cliente lineas migradas
        /// </summary>
        /// <param name="objRequest"></param>
        /// <param name="strIdSession"></param>
        /// <returns></returns>
        public static GetUpdateDataClientResponse GetUpdateDataClient(DataClientRequest objRequest, string strIdSession, string strIdTransaccion)
        {
            GetUpdateDataClientResponse objResponse = null;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(strIdSession, strIdTransaccion, () => Data.Transac.Service.Coliving.ChangeData.GetUpdateDataClient(objRequest, strIdSession, strIdTransaccion));
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strIdTransaccion, Claro.MessageException.GetOriginalExceptionMessage(ex));
                objResponse = null;
            }
            return objResponse;
        }


        /// <summary>
        /// Actualiza los datos de facturación para lineas migradas
        /// </summary>
        /// <param name="objRequest"></param>
        /// <param name="strIdSession"></param>
        /// <returns></returns>
        public static BillingAddressResponse UpdateDataBillingResponse(DataClientRequest objRequest, string strIdSession, string strIdTransaccion)
        {
            BillingAddressResponse objResponse = null; 
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(strIdTransaccion, strIdTransaccion,() => Data.Transac.Service.Coliving.ChangeData.UpdateDataBillingResponse(objRequest, strIdSession));
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strIdTransaccion, Claro.MessageException.GetOriginalExceptionMessage(ex));
                objResponse = null;
            }
            return objResponse;
        }

        public static GetDataBillingResponse GetDataBilling(DataBillingRequest objRequest, string strIdSession, string strIdTransaccion)
        {
            GetDataBillingResponse objResponse = null;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(strIdTransaccion, strIdTransaccion, () => Data.Transac.Service.Coliving.ChangeData.GetDataBilling(objRequest, strIdSession));
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strIdTransaccion, Claro.MessageException.GetOriginalExceptionMessage(ex));
                objResponse = null;
            }
            return objResponse;
        }



        public static HistoryClientResponse PostHistoryClientResponse(DataClientRequest objRequest, HistoryClient request, string strIdSession, string strIdTransaccion)
        {
            HistoryClientResponse objResponse = null;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(strIdTransaccion, strIdTransaccion, () => Data.Transac.Service.Coliving.ChangeData.PostHistoryClientResponse(objRequest, request, strIdSession));
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strIdTransaccion, Claro.MessageException.GetOriginalExceptionMessage(ex));
                objResponse = null;
            }
            return objResponse;
        }

        //vtorremo

        public static List<DataHistorical> HistoryDataClientTobe(Claro.Entity.AuditRequest audit, string strIdSession, string strIdTransaccion, string strIpAplicacion, string strAplicacion, string strUsrApp, string strCustomerID, string flagconvivencia)
        {
            List<DataHistorical> listaHistoryDataClient = null;
            try
            {
                listaHistoryDataClient = Claro.Web.Logging.ExecuteMethod<List<DataHistorical>>(strIdSession, strIdTransaccion, () =>
                {
                    return Data.Transac.Service.Coliving.ChangeData.HistoryDataClientTobe(audit, strCustomerID, flagconvivencia);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strIdTransaccion, Claro.MessageException.GetOriginalExceptionMessage(ex));
                listaHistoryDataClient = null; 
            }
            return listaHistoryDataClient;
        }
        public static ResponseCUParticipante GetCuParticipante(RequestCUParticipante objRequest, HeaderToBe objAudit)
        {
            ResponseCUParticipante objResponse = null;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<ResponseCUParticipante>(objAudit.IdTransaccion, objAudit.IdTransaccion, () =>
                {
                    return Data.Transac.Service.Coliving.ChangeData.GetCuParticipante(objRequest, objAudit);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objAudit.IdTransaccion, objAudit.IdTransaccion, Claro.MessageException.GetOriginalExceptionMessage(ex));
                objResponse = null;
            }
            return objResponse;
        }
        
        public static ResponseCBIO getDataCustomerCBIO(Claro.Entity.AuditRequest audit, request objRequest)
        {
            ResponseCBIO objResponse = null;

            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<ResponseCBIO>(audit.Session, audit.Transaction, () =>
                {
                    return Data.Transac.Service.Coliving.ChangeData.getDataCustomerCBIO(audit, objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(audit.Session, audit.Transaction, ex.Message);
            }
            return objResponse;

        }
    }
}
