using Claro.SIACU.Entity.Transac.Service.Common; 
using PREPAID = Claro.SIACU.Entity.Transac.Service.Prepaid;
using System.Collections.Generic;

namespace Claro.SIACU.Business.Transac.Service.Prepaid
{
    public class CallsDetail
    {
        #region Llamadas Entrantes
        public static PREPAID.GetLineData.LineDataResponse GetLineData(PREPAID.GetLineData.LineDataRequest request)
        {
            List<Account> listAccount = new List<Account>();
            List<ListItem> listTrio = new List<ListItem>();
            Line lineItem = new Line();
            string errorMessage = ""; 

            PREPAID.GetLineData.LineDataResponse objResponse = new PREPAID.GetLineData.LineDataResponse()
            {
                Code = Claro.Web.Logging.ExecuteMethod<string>(request.Audit.Session, request.Audit.Transaction,
                () =>
                {
                    return Data.Transac.Service.Prepaid.CallsDetail.GetLineData(request.Audit.Session,request.Audit.Transaction, request.Info, ref listAccount, ref listTrio
                        , ref lineItem, ref errorMessage);
                })
            };
            objResponse.LineItem = lineItem; 
            objResponse.ListAccount = listAccount;
            objResponse.ListTrio = listTrio;
            objResponse.ErrorMessage = errorMessage;
            return objResponse;
        }

        public static PREPAID.GetDataBag.DataBagResponse GetDataBag(PREPAID.GetDataBag.DataBagRequest request)
        {
            string strDate = "", strBalance = "";
            PREPAID.GetDataBag.DataBagResponse objResponse = new PREPAID.GetDataBag.DataBagResponse()
            {
                ListDataBag = Claro.Web.Logging.ExecuteMethod<List<Account>>(request.Audit.Session, request.Audit.Transaction,
                () =>
                {
                    return Data.Transac.Service.Prepaid.CallsDetail.GetListDataBag(request.Telephone, request.IdTransaction,
                        request.Audit.IPAddress, request.NameApplication, request.UserApplication, ref strDate, ref strBalance);
                })
            };
            objResponse.StrBalance = strBalance;
            objResponse.StrDate = strDate;
            return objResponse;
        }

        public static PREPAID.GetIncomingCallDetail.IncomingCallDetailResponse GetIncomingCallDetail(PREPAID.GetIncomingCallDetail.IncomingCallDetailRequest objRequest)
        {
            string vInteraccion = "";
            string vResultado = "";

            string rFlagInsercion = "";
            string rMensaje = "";

            PREPAID.GetIncomingCallDetail.IncomingCallDetailResponse objResponse = new PREPAID.GetIncomingCallDetail.IncomingCallDetailResponse()
            {
                ListIncomingCallDetail = Claro.Web.Logging.ExecuteMethod<List<PREPAID.IncomingCallDetail>>(objRequest.Audit.Session, objRequest.Audit.Transaction, () => { return Data.Transac.Service.Prepaid.CallsDetail.GetIncomingCallDetail(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.MSISDN, objRequest.StrStartDate, objRequest.StrEndDate, ref vInteraccion, ref vResultado); })
            };

            PREPAID.GetUpdateNotes.UpdateNotesResponse obj = new PREPAID.GetUpdateNotes.UpdateNotesResponse();

            if (vResultado != "0")
            {
                obj.Salida = Claro.Web.Logging.ExecuteMethod<bool>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Common.UpdateNotes(objRequest.Audit.Session, objRequest.Audit.Transaction, vInteraccion, "Error al Consultar Detalle de Llamadas Entrantes", "F", out rFlagInsercion, out rMensaje);
                });

            }

            objResponse.Result = obj.Salida.ToString();

            return objResponse;
        }
        #endregion

        
    }
}
