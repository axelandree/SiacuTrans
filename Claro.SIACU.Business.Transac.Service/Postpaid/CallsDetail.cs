using Claro.SIACU.Entity.Transac.Service.Common;
using Claro.SIACU.Transac.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POSTPAID = Claro.SIACU.Entity.Transac.Service.Postpaid;

namespace Claro.SIACU.Business.Transac.Service.Postpaid
{
    public class CallsDetail
    {
        #region No Facturado
        public static POSTPAID.GetCallDetailNBDB1.CallDetailNBDB1Response GetCallDetailNB_DB1(POSTPAID.GetCallDetailNBDB1.CallDetailNBDB1Request request)
        {
            string summaryTotal = "";
            POSTPAID.GetCallDetailNBDB1.CallDetailNBDB1Response objResponse =
            new POSTPAID.GetCallDetailNBDB1.CallDetailNBDB1Response()
            {
                ListCallDetail = Claro.Web.Logging.ExecuteMethod<List<CallDetailGeneric>>(request.Audit.Session, request.Audit.Transaction,
                () =>
                {
                    return Data.Transac.Service.Postpaid.CallsDetail.GetListCallDetailNB_DB1(request.ContractID, request.StrStartDate, request.StrEndDate, request.Security, ref summaryTotal);
                })
            };
            objResponse.SummaryTotal = summaryTotal;

            return objResponse;
        }

        public static POSTPAID.GetCallDetailNBDB1BSCS.CallDetailNBDB1BSCSResponse GetCallDetailNB_DB1_BSCS(POSTPAID.GetCallDetailNBDB1BSCS.CallDetailNBDB1BSCSRequest request)
        {
            string summaryTotal = "";
            string[] strTotales; 
            double[] totales1 = new double[5], totales2 = new double[5]; 
            POSTPAID.GetCallDetailNBDB1BSCS.CallDetailNBDB1BSCSResponse objResponse = new POSTPAID.GetCallDetailNBDB1BSCS.CallDetailNBDB1BSCSResponse();

            List<CallDetailGeneric> ListCallDetail = Claro.Web.Logging.ExecuteMethod<List<CallDetailGeneric>>(request.Audit.Session, request.Audit.Transaction,
                () =>
                {
                    return Data.Transac.Service.Postpaid.CallsDetail.GetListCallDetailNB_DB1(request.ContractID, request.StrStartDate, request.StrYesterday, request.Security, ref summaryTotal);
                });
            strTotales = summaryTotal.Split(';');
            for (int i = 0; i < 5; i++) {
                totales1[i] = Functions.CheckDbl(strTotales[i]);
            }

            List<CallDetailGeneric> ListCallDetail2 = Claro.Web.Logging.ExecuteMethod<List<CallDetailGeneric>>(request.Audit.Session, request.Audit.Transaction,
                () =>
                {
                    return Data.Transac.Service.Postpaid.CallsDetail.GetListCallDetailNB_DB1(request.ContractID, request.StrToday, request.StrEndDate, request.Security, ref summaryTotal);
                });
            strTotales = summaryTotal.Split(';');
            for (int i = 0; i < 5; i++)
            {
                totales2[i] = Functions.CheckDbl(strTotales[i]);
            }

            if (ListCallDetail2.Count > 0) {
                foreach (CallDetailGeneric item in ListCallDetail2) {
                    ListCallDetail.Add(item); 
                }  
            }

            objResponse.ListCallDetail = ListCallDetail;
            if (ListCallDetail.Count > 0)
            {
                objResponse.SummaryTotal = (totales1[0] + totales2[0]).ToString() + ";" + (totales1[1] + totales2[1]).ToString() + ";" + (totales1[2] + totales2[2]).ToString() + ";" +
                   (totales1[3] + totales2[3]).ToString() + ";" + (totales1[4] + totales2[4]).ToString();
            }
            else 
                objResponse.SummaryTotal = "0;0;0;0;0";

            return objResponse;
        }
        #endregion
    }
}
