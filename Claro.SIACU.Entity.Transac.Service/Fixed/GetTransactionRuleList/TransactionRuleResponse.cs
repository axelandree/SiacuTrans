using System.Runtime.Serialization;
using System.Collections.Generic;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetTransactionRuleList
{
    [DataContract(Name = "TransactionRulesResponseHfc")]
    public class TransactionRulesResponse
    {
        [DataMember]
        public List<TransactionRule> rules { get; set; }
    }
}
