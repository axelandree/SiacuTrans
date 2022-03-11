using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetTriations
{
    public class StriationsRequest : Claro.Entity.Request
    {
        [DataMember]
        public int ContractId { get; set; }
    }
}
