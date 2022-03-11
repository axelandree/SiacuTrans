using Claro.SIACU.Entity.Transac.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OPostBillOutCallDetail = Claro.SIACU.Entity.Transac.Service.Postpaid;

namespace Claro.SIACU.Business.Transac.Service.Postpaid
{
   public class PostBilledOutCallDetails
    {

        public static OPostBillOutCallDetail.GetBillPostDetail.BillPostDetailResponse GetBillPostDetail(OPostBillOutCallDetail.GetBillPostDetail.BillPostDetailRequest objBillPostRequest)
        {
            OPostBillOutCallDetail.GetBillPostDetail.BillPostDetailResponse ObjBillPostResponse = new OPostBillOutCallDetail.GetBillPostDetail.BillPostDetailResponse();
            List<CallDetailGeneric> lstBillPostdetail = new List<CallDetailGeneric>();
            try
            {
                lstBillPostdetail = Claro.Web.Logging.ExecuteMethod<List<CallDetailGeneric>>(objBillPostRequest.Audit.Session, objBillPostRequest.Audit.Transaction,
                    () =>
                    {
                        return Claro.SIACU.Data.Transac.Service.Postpaid.PostBilledOutCallDetails.ListarLlamadasMesPrepagoControl(objBillPostRequest.Audit.Session, objBillPostRequest.Audit.Transaction, objBillPostRequest.vFECHA_FIN, objBillPostRequest.vFECHA_INI, objBillPostRequest.vFlag, objBillPostRequest.vFLAG_777, objBillPostRequest.vFLAG_DETALLE, objBillPostRequest.vFLAG_GPRS, objBillPostRequest.vFLAG_INTERNACIONA, objBillPostRequest.vFLAG_NACIONAL,
                          objBillPostRequest.vFLAG_SMS_MMS, objBillPostRequest.vFLAG_TIPO_VISUAL, objBillPostRequest.vFLAG_VAS, objBillPostRequest.vMSISDN, objBillPostRequest.vSeguridad);
                    });

                ObjBillPostResponse.GetBillPostDetail = lstBillPostdetail;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objBillPostRequest.Audit.Session, objBillPostRequest.Audit.Transaction, ex.Message);
            }
            return ObjBillPostResponse;
        }

        public static OPostBillOutCallDetail.GetListInvoice_PDI.ListInvoice_PDIResponse GetListInvoicePDI(OPostBillOutCallDetail.GetListInvoice_PDI.ListInvoice_PDIRequest objListInvoicePdiRequest)
        {
            OPostBillOutCallDetail.GetListInvoice_PDI.ListInvoice_PDIResponse ObjListInvoicePdiResponse = new OPostBillOutCallDetail.GetListInvoice_PDI.ListInvoice_PDIResponse();
            List<OPostBillOutCallDetail.Invoice_PDI> lstListInvoicePdi = new List<OPostBillOutCallDetail.Invoice_PDI>();
            try
            {
                lstListInvoicePdi = Claro.Web.Logging.ExecuteMethod<List<OPostBillOutCallDetail.Invoice_PDI>>(objListInvoicePdiRequest.Audit.Session, objListInvoicePdiRequest.Audit.Transaction,
                    () =>
                    {
                        return Claro.SIACU.Data.Transac.Service.Postpaid.PostBilledOutCallDetails.ListarFacturas_PDI(objListInvoicePdiRequest.Audit.Session, objListInvoicePdiRequest.Audit.Transaction, objListInvoicePdiRequest.vCODCLIENTE);
                    });

                ObjListInvoicePdiResponse.GetListInvoicePDI = lstListInvoicePdi;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objListInvoicePdiRequest.Audit.Session, objListInvoicePdiRequest.Audit.Transaction, ex.Message);
            }
            return ObjListInvoicePdiResponse;
        }

        public static OPostBillOutCallDetail.GetListInvoice.ListInvoiceResponse GetListInvoice(OPostBillOutCallDetail.GetListInvoice.ListInvoiceRequest objListInvoiceRequest)
        {
            OPostBillOutCallDetail.GetListInvoice.ListInvoiceResponse ObjListInvoiceResponse = new OPostBillOutCallDetail.GetListInvoice.ListInvoiceResponse();
            List<OPostBillOutCallDetail.Invoice> lstListInvoice = new List<OPostBillOutCallDetail.Invoice>();
            try
            {
                lstListInvoice = Claro.Web.Logging.ExecuteMethod<List<OPostBillOutCallDetail.Invoice>>(objListInvoiceRequest.Audit.Session, objListInvoiceRequest.Audit.Transaction,
                    () =>
                    {
                        return Claro.SIACU.Data.Transac.Service.Postpaid.PostBilledOutCallDetails.ListarFacturas(objListInvoiceRequest.Audit.Session, objListInvoiceRequest.Audit.Transaction, objListInvoiceRequest.vCODCLIENTE);
                    });

                ObjListInvoiceResponse.GetListInvoice = lstListInvoice;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objListInvoiceRequest.Audit.Session, objListInvoiceRequest.Audit.Transaction, ex.Message);
            }
            return ObjListInvoiceResponse;
        }

        public static OPostBillOutCallDetail.GetListCallDetail.ListCallDetailResponse GetListCallDetail(OPostBillOutCallDetail.GetListCallDetail.ListCallDetailRequest objListCallDetailRequest)
        {
            OPostBillOutCallDetail.GetListCallDetail.ListCallDetailResponse ObjListCallDetailResponse = new OPostBillOutCallDetail.GetListCallDetail.ListCallDetailResponse();
            List<OPostBillOutCallDetail.ListCallDetail> lstListCallDetail = new List<OPostBillOutCallDetail.ListCallDetail>();
            try
            {
                lstListCallDetail = Claro.Web.Logging.ExecuteMethod<List<OPostBillOutCallDetail.ListCallDetail>>(objListCallDetailRequest.Audit.Session, objListCallDetailRequest.Audit.Transaction,
                    () =>
                    {
                        return  Claro.SIACU.Data.Transac.Service.Postpaid.PostBilledOutCallDetails.Listar_TR_Detalle_Llamada(objListCallDetailRequest.Audit.Session, objListCallDetailRequest.Audit.Transaction, objListCallDetailRequest.vINVOICENUMBER, objListCallDetailRequest.vSeguridad, objListCallDetailRequest.vTELEFONO);
                    });

                ObjListCallDetailResponse.GetListCallDetail = lstListCallDetail;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objListCallDetailRequest.Audit.Session, objListCallDetailRequest.Audit.Transaction, ex.Message);
            }
            return ObjListCallDetailResponse;
        }

        public static OPostBillOutCallDetail.GetListCallDetailPDI.ListCallDetailPDIResponse GetListCallDetailPDI(OPostBillOutCallDetail.GetListCallDetailPDI.ListCallDetailPDIRequest objListCallDetailPDIRequest)
        {
            OPostBillOutCallDetail.GetListCallDetailPDI.ListCallDetailPDIResponse ObjListCallDetailPDIResponse = new OPostBillOutCallDetail.GetListCallDetailPDI.ListCallDetailPDIResponse();
            List<OPostBillOutCallDetail.ListCallDetail_PDI> lstListCallDetailPDI = new List<OPostBillOutCallDetail.ListCallDetail_PDI>();
            try
            {
                lstListCallDetailPDI = Claro.Web.Logging.ExecuteMethod<List<OPostBillOutCallDetail.ListCallDetail_PDI>>(objListCallDetailPDIRequest.Audit.Session, objListCallDetailPDIRequest.Audit.Transaction,
                    () =>
                    {
                        return Claro.SIACU.Data.Transac.Service.Postpaid.PostBilledOutCallDetails.Listar_TR_Detalle_Llamada_PDI(objListCallDetailPDIRequest.Audit.Session, objListCallDetailPDIRequest.Audit.Transaction, objListCallDetailPDIRequest.vINVOICENUMBER, objListCallDetailPDIRequest.vSeguridad, objListCallDetailPDIRequest.vTELEFONO);
                    });

                ObjListCallDetailPDIResponse.GetListCallDetailPDI = lstListCallDetailPDI;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objListCallDetailPDIRequest.Audit.Session, objListCallDetailPDIRequest.Audit.Transaction, ex.Message);
            }
            return ObjListCallDetailPDIResponse;
        }

        public static OPostBillOutCallDetail.GetRechargeList.RechargeListResponse GetRechargeList(OPostBillOutCallDetail.GetRechargeList.RechargeListRequest objRechargeListRequest)
        {
            OPostBillOutCallDetail.GetRechargeList.RechargeListResponse ObjRechargeListResponse = new OPostBillOutCallDetail.GetRechargeList.RechargeListResponse();
            List<OPostBillOutCallDetail.RechargeList> lstRechargeList = new List<OPostBillOutCallDetail.RechargeList>();
            try
            {
                lstRechargeList = Claro.Web.Logging.ExecuteMethod<List<OPostBillOutCallDetail.RechargeList>>(objRechargeListRequest.Audit.Session, objRechargeListRequest.Audit.Transaction,
                    () =>
                    {
                        return Claro.SIACU.Data.Transac.Service.Postpaid.PostBilledOutCallDetails.SP_RechargeList(objRechargeListRequest.Audit.Session, objRechargeListRequest.Audit.Transaction, objRechargeListRequest.vFECHFIN, objRechargeListRequest.vFECHINI, objRechargeListRequest.vFlag, objRechargeListRequest.vMSISDN, objRechargeListRequest.vNroRegistros);
                    });

                ObjRechargeListResponse.GetRechargeList = lstRechargeList;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRechargeListRequest.Audit.Session, objRechargeListRequest.Audit.Transaction, ex.Message);
            }
            return ObjRechargeListResponse;
        }

    }
}
