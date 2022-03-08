using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetInsertEvidence
{
    [DataContract]
    public class InsertEvidenceRequest : Claro.Entity.Request
    {
        [DataMember]
        public Evidence Evidence { get; set; }
    }
}
