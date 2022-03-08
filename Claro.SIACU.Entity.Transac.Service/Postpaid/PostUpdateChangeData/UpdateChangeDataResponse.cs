using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.PostUpdateChangeData
{
    public class UpdateChangeDataResponse
    {
        [DataMember]
        public string ResultCode { get; set; }
        [DataMember]
        public int SequenceCode { get; set; }
        [DataMember]
        public string ResultMessage { get; set; }
    }
}
