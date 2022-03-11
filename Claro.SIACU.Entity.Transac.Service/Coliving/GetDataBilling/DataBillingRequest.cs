using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Coliving.GetDataBilling
{
    [DataContract]
    public class DataBillingRequest : HeaderToBe
    {
        [DataMember]
        public GetDataBillingRequest oDataBillingRequest { get; set; }
    }
}
