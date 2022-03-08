using Claro.SIACU.Entity;
using Claro.SIACU.Entity.Transac.Service.Prepaid;
using System;
using System.Collections.Generic;
using KEY = Claro.ConfigurationManager;
using PREPAID = Claro.SIACU.Entity.Transac.Service.Prepaid;

namespace Claro.SIACU.Business.Transac.Service.Prepaid
{
    public class Prepaid
    {
        /// <summary>
        /// Método para obtener datos de las recargas
        /// </summary>
        /// <param name="objRechargeRequest">Contiene los datos de auditoria.</param>
        /// <returns>Devuelve objeto objRechargeResponse con información de las recargas.</returns>
        public static PREPAID.GetRecharge.RechargeResponse GetRecharge(PREPAID.GetRecharge.RechargeRequest objRechargeRequest)
        {
            PREPAID.GetRecharge.RechargeResponse objRechargeResponse = new PREPAID.GetRecharge.RechargeResponse()
            {
                ListRecharge = Claro.Web.Logging.ExecuteMethod<List<PREPAID.Recharge>>(objRechargeRequest.Audit.Session, objRechargeRequest.Audit.Transaction, () => { return Data.Transac.Service.Prepaid.Prepaid.GetRecharge(objRechargeRequest.Audit.Session, objRechargeRequest.Audit.Transaction, objRechargeRequest.MSISDN, objRechargeRequest.START_DATE, objRechargeRequest.END_DATE, objRechargeRequest.FLAG, objRechargeRequest.NUMBER_REG); })
            };
            return objRechargeResponse;
        }

        public static PREPAID.GetConsultPointOfSale.ConsultPointOfSaleResponse GetConsultPointOfSale(PREPAID.GetConsultPointOfSale.ConsultPointOfSaleRequest objPointOfSaleRequest)
        {   
            PREPAID.GetConsultPointOfSale.ConsultPointOfSaleResponse objPointOfSaleResponse = new PREPAID.GetConsultPointOfSale.ConsultPointOfSaleResponse();
            string strResponse = Claro.Web.Logging.ExecuteMethod<string>(objPointOfSaleRequest.SessionId, objPointOfSaleRequest.TransactionId, () =>
            {
                return Data.Transac.Service.Prepaid.Prepaid.GetConsultPointOfSale(objPointOfSaleRequest.SessionId, objPointOfSaleRequest.TransactionId, objPointOfSaleRequest.CodigoCAC).Trim();
            });
            if (!string.IsNullOrEmpty(strResponse))
            {
                Claro.Web.Logging.Info(objPointOfSaleRequest.SessionId, objPointOfSaleRequest.TransactionId, strResponse);
                objPointOfSaleResponse.flag_biometria = strResponse;

            }
            return objPointOfSaleResponse;
        }
        //public static PREPAID.GetIncomingCallDetail.IncomingCallDetailResponse GetIncomingCallDetail(PREPAID.GetIncomingCallDetail.IncomingCallDetailRequest objRequest)
        //{

        //    string vInteraccion = "";
        //    string vResultado = "";


        //    string rFlagInsercion = "";
        //    string rMensaje = "";


        //    PREPAID.GetIncomingCallDetail.IncomingCallDetailResponse objResponse = new PREPAID.GetIncomingCallDetail.IncomingCallDetailResponse()
        //    {
        //        ListIncomingCallDetail = Claro.Web.Logging.ExecuteMethod<List<PREPAID.IncomingCallDetail>>(objRequest.Audit.Session, objRequest.Audit.Transaction, () => { return Data.Transac.Service.Prepaid.Prepaid.GetIncomingCallDetail(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.V_MSISDN, objRequest.Start_date, objRequest.End_DAte, ref vInteraccion, ref vResultado); })
        //    };


        //    PREPAID.GetUpdateNotes.UpdateNotesResponse obj = new PREPAID.GetUpdateNotes.UpdateNotesResponse();



        //    if (vResultado != "0")
        //    {
        //        obj.Salida = Claro.Web.Logging.ExecuteMethod<bool>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
        //        {
        //            return Data.Transac.Service.Prepaid.Prepaid.UpdateNotes(objRequest.Audit.Session, objRequest.Audit.Transaction, vInteraccion, "Error al Consultar Detalle de Llamadas Entrantes", "F", ref rFlagInsercion, ref rMensaje);
        //        });

        //    }

        //    objResponse.Result = obj.Salida.ToString();

        //    return objResponse;
        //}
        
        public static PREPAID.GetPlanesTFI.responseDataObtenerTabConsultaPlanTFIPost GetPlanesTFI(PREPAID.GetPlanesTFI.PlanesTFIRequest objPlanesTFIRequest)
        {
            PREPAID.GetPlanesTFI.responseDataObtenerTabConsultaPlanTFIPost objPlanesTFIResponse = new PREPAID.GetPlanesTFI.responseDataObtenerTabConsultaPlanTFIPost();
            try
            {
                objPlanesTFIResponse = Claro.Web.Logging.ExecuteMethod(objPlanesTFIRequest.Audit.Session, objPlanesTFIRequest.Audit.Transaction, () =>
                { return Data.Transac.Service.Prepaid.Prepaid.GetPlanesTFI(objPlanesTFIRequest); });
            }
            catch(Exception ex)
            {
                  Web.Logging.Error(objPlanesTFIRequest.Audit.Session, objPlanesTFIRequest.Audit.Transaction, ex.Message);
            }
            return objPlanesTFIResponse;
        }
        
        public static PREPAID.GetCambioPlanTFI.CambioPlanTFIResponse GetCambioPlanTFI (PREPAID.GetCambioPlanTFI.CambioPlanTFIRequest objCambioPlanTFIRequest)
        {
            PREPAID.GetCambioPlanTFI.CambioPlanTFIResponse objCambioPlanTFIResponse = new PREPAID.GetCambioPlanTFI.CambioPlanTFIResponse();
            try
            {
                objCambioPlanTFIResponse = Claro.Web.Logging.ExecuteMethod(objCambioPlanTFIRequest.Audit.Session, objCambioPlanTFIRequest.Audit.Transaction, () =>
                { return Data.Transac.Service.Prepaid.Prepaid.GetCambioPlanTFI(objCambioPlanTFIRequest); });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objCambioPlanTFIRequest.Audit.Session, objCambioPlanTFIRequest.Audit.Transaction, ex.Message);
            }
            return objCambioPlanTFIResponse;
        }
   }
}
