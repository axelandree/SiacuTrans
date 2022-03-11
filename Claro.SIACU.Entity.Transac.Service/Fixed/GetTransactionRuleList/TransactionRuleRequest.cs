using System.Runtime.Serialization;


namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetTransactionRuleList
{
    [DataContract(Name = "TransactionRulesRequestHfc")]
    public class TransactionRulesRequest : Claro.Entity.Request
    {
        [DataMember]
        public string SubClase { get; set; }

    }
}
