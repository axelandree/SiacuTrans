using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetBpelCallDetail
{
    [DataContract]
    public class BpelCallDetailRequest : Claro.Entity.Request
    {
        [DataMember]
        public HeaderRequestTypeBpel HeaderRequestTypeBpel { get; set; }
        [DataMember]
        public DetalleLlamadasRequestBpel DetalleLlamadasRequestBpel { get; set; }
        [DataMember]
        public string StrSecurity { get; set; }
        [DataMember]
        public string StrIdSession { get; set; }
        [DataMember]
        public string StrTransaction { get; set; }
    }
}
