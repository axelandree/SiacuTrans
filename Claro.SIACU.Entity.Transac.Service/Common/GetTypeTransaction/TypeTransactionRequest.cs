using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetTypeTransaction
{
    [DataContract]
    public class TypeTransactionRequest : Claro.Entity.Request
    {
        [DataMember]
        public string SessionId { get; set; }
        [DataMember]
        public string TransactionId { get; set; }
    }
}
