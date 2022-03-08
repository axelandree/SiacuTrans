using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetInsertEvidence
{

    [DataContract]
    public class InsertEvidenceResponse
    {
        [DataMember]
        public string StrFlagInsertion { get; set; }
        [DataMember]
        public string StrMsgText { get; set; }
        [DataMember]
        public bool BoolResult { get; set; }
    }
}
