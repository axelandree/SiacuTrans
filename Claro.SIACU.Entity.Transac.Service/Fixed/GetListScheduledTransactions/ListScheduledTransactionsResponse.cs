
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetListScheduledTransactions
{
    [DataContract(Name = "ListScheduledTransactionsResponse")]
    public class ListScheduledTransactionsResponse
    {
        [DataMember]
        public List<ScheduledTransaction> LstTransactions { get; set; }

        [DataMember]
        public bool CorrectProcess { get; set; }
    }
}
