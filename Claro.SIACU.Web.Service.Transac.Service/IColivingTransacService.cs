using Claro.SIACU.Entity.Transac.Service.Coliving;
using Claro.SIACU.Entity.Transac.Service.Coliving.GetDataHistoryClient;
using Claro.SIACU.Entity.Transac.Service.Coliving.GetParameter;
using Claro.SIACU.Entity.Transac.Service.Coliving.PostHistoryClient;
using Claro.SIACU.Entity.Transac.Service.Coliving.PutBillingAddress;
using Claro.SIACU.Entity.Transac.Service.Coliving.PutDataClient;
using System.Collections.Generic;
using System.ServiceModel;
using COLIVING = Claro.SIACU.Entity.Transac.Service.Coliving;
using Claro.SIACU.Entity.Transac.Service.Postpaid.GetDataCustomerCBIO.Request;
using Claro.SIACU.Entity.Transac.Service.Postpaid.GetDataCustomerCBIO.Response;

namespace Claro.SIACU.Web.Service.Transac.Service
{
    [ServiceContract]
    public interface IColivingTransacService
    {
        
        [OperationContract]
        COLIVING.GetDataBilling.GetDataBillingResponse GetDataBilling(COLIVING.GetDataBilling.DataBillingRequest objRequest, string strIdSession, string strIdTransaccion);
       

        [OperationContract]
        string UpdateDatosClienteMigrado(DataClientRequest objRequest, string strIdSession, string strIdTransaccion);

        [OperationContract]
        GetParameterResponse GetParameter(string name, string strIdSession, string strTransaction);

        [OperationContract]
        List<ListItems> GetDocumentTypeTOBE(string strIdSession, string strTransaction, string strCodCargaDdl);

        //vtorremo
        [OperationContract]
        List<DataHistorical> HistoryDataClientTobe(Claro.Entity.AuditRequest audit, string strIdSession, string strTransaction, string strIpAplicacion, string strAplicacion, string strUsrApp, string strCustomerID, string flagconvivencia);
        

        [OperationContract]
        HistoryClientResponse PostDataHistoryClientResponse(DataClientRequest objRequest, HistoryClient objRequestType, string strIdSession, string strIdTransaccion);
    
        [OperationContract]
        COLIVING.GetUpdateDataClient.ResponseCUParticipante GetCuParticipante(COLIVING.GetUpdateDataClient.RequestCUParticipante objRequest, HeaderToBe objAudit);

        [OperationContract]
        ResponseCBIO getDataCustomerCBIO(Claro.Entity.AuditRequest audit, request objRequest);
            
    }
}
