using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed
{
    [DataContract]
    public class AddServiceAditional
    {
        [DataMember]
        public string StrTransaccionId { get; set; }
        [DataMember]
        public string StrNameAplication { get; set; }
        [DataMember]
        public string StrIpAplication { get; set; }
        [DataMember]
        public string StrMsisdn { get; set; }
        [DataMember]
        public string StrCoId { get; set; }
        [DataMember]
        public string StrCustomerId { get; set; }
        [DataMember]
        public string StrCoSer { get; set; }
        [DataMember]
        public int IntFlagOccPenalty { get; set; }
        [DataMember]
        public double DblPenaltyAmount { get; set; }
        [DataMember]
        public double DblNewCf { get; set; }
        [DataMember]
        public string StrTypeRegistry { get; set; }
        [DataMember]
        public string StrCycleFacturation { get; set; }
        [DataMember]
        public string StrCodeSer { get; set; }
        [DataMember]
        public string StrDescriptioCoSer { get; set; }
        [DataMember]
        public string StrNroAccoutnt { get; set; }
        [DataMember]
        public string StrDateProgramation { get; set; }
        [DataMember]
        public string StrInteractionId { get; set; }
        [DataMember]
        public string StrTypeSerivice { get; set; }
        [DataMember]
        public string StrUserSystem { get; set; }
    }
}
