using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetTransactionScheduled
{
    [DataContract]
    public class TransactionScheduledResponse
    {
        [DataMember]
        public List<TransactionScheduled> ListTransactionScheduled { get; set; }
    }
}
