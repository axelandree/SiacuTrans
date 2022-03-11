using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetValidateActDesService
{
    [DataContract]
    public class ValidateActDesServiceRequest : Claro.Entity.Request
    {
        [DataMember]
        public string StrMsisdn { get; set; }
        [DataMember]
        public string StrCodId { get; set; }
        [DataMember]
        public string StrCoSer { get; set; }
        [DataMember]
        public string StrTypeRegistre { get; set; }
        [DataMember]
        public string StrCodSer { get; set; }
        [DataMember]
        public int StrStateService { get; set; }

    }
}
