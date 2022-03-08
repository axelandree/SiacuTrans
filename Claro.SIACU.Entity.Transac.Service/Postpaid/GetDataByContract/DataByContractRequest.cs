
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetDataByContract
{
    [DataContract(Name = "DataByContractRequestPostPaid")]
    public class DataByContractRequest : Claro.Entity.Request
    {
        [DataMember]
        public string CustomerId { get; set; }
        [DataMember]
        public string CoId { get; set; }
    }
}
