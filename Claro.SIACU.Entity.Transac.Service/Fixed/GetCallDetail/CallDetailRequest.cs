using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetCallDetail
{
    [DataContract]
    public class CallDetailRequest : Claro.Entity.Request
    {
        [DataMember]
        public string vCONTRATOID { get; set; }
        [DataMember]
        public string vFECHA_INI { get; set; }
        [DataMember]
        public string vFECHA_FIN { get; set; }
        [DataMember]
        public string vSeguridad { get; set; }
        [DataMember]
        public string vFECHA_AYER { get; set; }
        [DataMember]
        public string vFECHA_AHORA { get; set; }
    }
}
