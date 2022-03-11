using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetPortability
{
    public class PortabilityRequest : Claro.Entity.Request
    {
        [DataMember]
        public string Telephone { get; set; }
    }
}
