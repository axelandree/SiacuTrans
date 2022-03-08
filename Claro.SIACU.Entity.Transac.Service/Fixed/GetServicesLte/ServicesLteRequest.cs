using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetServicesLte
{
    [DataContract(Name = "ServicesLteFixedRequest")]
    public   class ServicesLteRequest : Claro.Entity.Request
    {
        [DataMember]
        public string strCustomerId { get; set; }
        [DataMember]
        public string strCoid { get; set; }
    }
}
