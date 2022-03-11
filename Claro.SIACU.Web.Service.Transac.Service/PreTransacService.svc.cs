using Claro.SIACU.Entity.Transac.Service;
using Claro.SIACU.Entity.Transac.Service.Prepaid.GetCall;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using PREPAID = Claro.SIACU.Entity.Transac.Service.Prepaid;
using FUNCTIONS = Claro.SIACU.Transac.Service;

namespace Claro.SIACU.Web.Service.Transac.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "PreTransacService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select PreTransacService.svc or PreTransacService.svc.cs at the Solution Explorer and start debugging.
    public class PreTransacService : IPreTransacService
    {
        public Claro.SIACU.Entity.Transac.Service.Prepaid.GetRecharge.RechargeResponse GetRecharge(Claro.SIACU.Entity.Transac.Service.Prepaid.GetRecharge.RechargeRequest objRechargeRequest)
        {
            Claro.SIACU.Entity.Transac.Service.Prepaid.GetRecharge.RechargeResponse objRechargeResponse = null;

            try
            {
                objRechargeResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Transac.Service.Prepaid.GetRecharge.RechargeResponse>(() => { return Business.Transac.Service.Prepaid.Prepaid.GetRecharge(objRechargeRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRechargeRequest.Audit.Session, objRechargeRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objRechargeResponse;
        }

        /// <summary>
        /// Método para obtener el listado de detalle de llamadas
        /// </summary>
        /// <param name="objRequest">Request de detalle de llamadas</param>
        /// <returns>Response del tipo llamadas</returns>
    
        public PREPAID.GetLineData.LineDataResponse GetLineData(PREPAID.GetLineData.LineDataRequest request)
        {
            PREPAID.GetLineData.LineDataResponse objResponse = null;

            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<PREPAID.GetLineData.LineDataResponse>(() =>
                {
                    return Business.Transac.Service.Prepaid.CallsDetail.GetLineData(request);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objResponse;
        }

        public PREPAID.GetDataBag.DataBagResponse GetDataBag(PREPAID.GetDataBag.DataBagRequest request) {
            PREPAID.GetDataBag.DataBagResponse objResponse = null;
            try{
                objResponse = Claro.Web.Logging.ExecuteMethod<PREPAID.GetDataBag.DataBagResponse>(() =>
                {
                    return Business.Transac.Service.Prepaid.CallsDetail.GetDataBag(request);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            } 
            return objResponse;
        }

        public PREPAID.GetIncomingCallDetail.IncomingCallDetailResponse GetIncomingCallDetail(PREPAID.GetIncomingCallDetail.IncomingCallDetailRequest objRequest)
        {
            PREPAID.GetIncomingCallDetail.IncomingCallDetailResponse objResponse = null;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<PREPAID.GetIncomingCallDetail.IncomingCallDetailResponse>(() =>
                {
                    return Business.Transac.Service.Prepaid.CallsDetail.GetIncomingCallDetail(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        public PREPAID.GetTipifCallOutPrep.TipifCallOutPrepResponse GetTipifCallOutPrep(PREPAID.GetTipifCallOutPrep.TipifCallOutPrepRequest objRequest)
        {
            PREPAID.GetTipifCallOutPrep.TipifCallOutPrepResponse objResponse = new PREPAID.GetTipifCallOutPrep.TipifCallOutPrepResponse();
              try
            {
            objResponse = Claro.Web.Logging.ExecuteMethod<PREPAID.GetTipifCallOutPrep.TipifCallOutPrepResponse>(
                objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Business.Transac.Service.Prepaid.CallOutDetails.GetTipifCallOutPrep(objRequest);
                });
                  }
            catch (Exception ex)
            {
                   Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            
            return objResponse;
        }

        public CallResponse GetCallOutDetailsLoad(CallRequest objRequest)
        {
            CallResponse objResponse = null;

            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<CallResponse>(() =>
                {
                    return Business.Transac.Service.Prepaid.CallOutDetails.GetCallOutDetailsLoad(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objResponse;
        }
        public PREPAID.GetConsultPointOfSale.ConsultPointOfSaleResponse GetConsultPointOfSale(PREPAID.GetConsultPointOfSale.ConsultPointOfSaleRequest objPointOfSaleRequest)
        {
            PREPAID.GetConsultPointOfSale.ConsultPointOfSaleResponse obConsultPointOfSaleResponse = null;

            try
            {
                obConsultPointOfSaleResponse = Claro.Web.Logging.ExecuteMethod<PREPAID.GetConsultPointOfSale.ConsultPointOfSaleResponse>(() =>
                {
                    return Business.Transac.Service.Prepaid.Prepaid.GetConsultPointOfSale(objPointOfSaleRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objPointOfSaleRequest.Audit.Session, objPointOfSaleRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return obConsultPointOfSaleResponse;
        }

        public PREPAID.GetPlanesTFI.responseDataObtenerTabConsultaPlanTFIPost GetPlanesTFI(PREPAID.GetPlanesTFI.PlanesTFIRequest objRequest)
        {
            PREPAID.GetPlanesTFI.responseDataObtenerTabConsultaPlanTFIPost objResponse = new PREPAID.GetPlanesTFI.responseDataObtenerTabConsultaPlanTFIPost();
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<PREPAID.GetPlanesTFI.responseDataObtenerTabConsultaPlanTFIPost>(
                    objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                    {
                        return Business.Transac.Service.Prepaid.Prepaid.GetPlanesTFI(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objResponse;
        }

        public PREPAID.GetCambioPlanTFI.CambioPlanTFIResponse GetCambioPlanTFI(PREPAID.GetCambioPlanTFI.CambioPlanTFIRequest objCambioPlanTFIRequest)
        {
            PREPAID.GetCambioPlanTFI.CambioPlanTFIResponse objCambioPlanTFIResponse = new PREPAID.GetCambioPlanTFI.CambioPlanTFIResponse();
            try
            {
                objCambioPlanTFIResponse = Claro.Web.Logging.ExecuteMethod(objCambioPlanTFIRequest.Audit.Session, objCambioPlanTFIRequest.Audit.Transaction, () =>
                { return Business.Transac.Service.Prepaid.Prepaid.GetCambioPlanTFI(objCambioPlanTFIRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objCambioPlanTFIRequest.Audit.Session, objCambioPlanTFIRequest.Audit.Transaction, ex.Message);
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objCambioPlanTFIResponse;
        }
    }
}
