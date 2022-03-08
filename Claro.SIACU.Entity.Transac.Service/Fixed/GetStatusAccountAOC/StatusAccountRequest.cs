using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetStatusAccountAOC
{
    [DataContract]
    public class StatusAccountRequest : Claro.Entity.Request
    {
        [DataMember]
        public string CustomerId { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string OriginType { get; set; }
        [DataMember]
        public bool isHR { get; set; }
    }
}
