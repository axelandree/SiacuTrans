using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetCallDetailDB1
{
    [DataContract]
    public class CallDetailDB1Request : Claro.Entity.Request
    {
        [DataMember]
        public string vCONTRATOID { get; set; }
        [DataMember]
        public string vFECHA_INI { get; set; }
        [DataMember]
        public string vFECHA_FIN { get; set; }
        [DataMember]
        public string vSeguridad { get; set; }
     }
}
