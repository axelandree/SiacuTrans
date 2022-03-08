using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetServiceDTH
{
    public class ServiceDTHRequest : Claro.Entity.Request
    {
        [DataMember]
        public string strCustomerId { get; set; }
        [DataMember]
        public string strCoid { get; set; }
    }
}
