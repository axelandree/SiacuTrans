using Claro.SIACU.Entity.Transac.Service.Prepaid; 
using Claro.SIACU.Entity.Transac.Service.Prepaid.GetRecharge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using PREPAID = Claro.SIACU.Entity.Transac.Service.Prepaid;

namespace Claro.SIACU.Web.Service.Transac.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IPreTransacService" in both code and config file together.
    [ServiceContract]
    public interface IPreTransacService
    {

        [OperationContract]
        RechargeResponse GetRecharge(RechargeRequest objRechargeRequest);

        [OperationContract]
        PREPAID.GetLineData.LineDataResponse GetLineData(PREPAID.GetLineData.LineDataRequest request);

        [OperationContract]
        PREPAID.GetDataBag.DataBagResponse GetDataBag(PREPAID.GetDataBag.DataBagRequest request);

        [OperationContract]
        PREPAID.GetIncomingCallDetail.IncomingCallDetailResponse GetIncomingCallDetail(PREPAID.GetIncomingCallDetail.IncomingCallDetailRequest objRequest);
        
        [OperationContract]
        PREPAID.GetTipifCallOutPrep.TipifCallOutPrepResponse GetTipifCallOutPrep(PREPAID.GetTipifCallOutPrep.TipifCallOutPrepRequest objRequest);
      
        [OperationContract]
        PREPAID.GetCall.CallResponse GetCallOutDetailsLoad(PREPAID.GetCall.CallRequest objRequest);

        [OperationContract]
        PREPAID.GetConsultPointOfSale.ConsultPointOfSaleResponse GetConsultPointOfSale(PREPAID.GetConsultPointOfSale.ConsultPointOfSaleRequest objPointOfSaleRequest);

        [OperationContract]
        PREPAID.GetPlanesTFI.responseDataObtenerTabConsultaPlanTFIPost GetPlanesTFI(PREPAID.GetPlanesTFI.PlanesTFIRequest objRequest);

        [OperationContract]
        PREPAID.GetCambioPlanTFI.CambioPlanTFIResponse GetCambioPlanTFI(PREPAID.GetCambioPlanTFI.CambioPlanTFIRequest objCambioPlanTFIRequest);
    }
}
