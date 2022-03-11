using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetCommercialServices
{
    [DataContract]
    public class CommercialServicesRequest : Claro.Entity.Request
    {
        [DataMember]
        public string StrCoId { get; set; }
    }
}
