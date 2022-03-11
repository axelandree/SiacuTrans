using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetFixedCostBasePlan
{
    [DataContract(Name = "FixedCostBasePlanResponseTransactions")]
    public class FixedCostBasePlanResponse
    {
        [DataMember]
        public string DescriptionOrigenPlan { get; set; }

    }
}
