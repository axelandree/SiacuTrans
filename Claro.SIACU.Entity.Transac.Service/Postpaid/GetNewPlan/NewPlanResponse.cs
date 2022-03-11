using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetNewPlan
{
    [DataContract(Name = "NewPlanResponseTransactions")]
    public class NewPlanResponse
    {
        [DataMember]
        public List<NewPlan> lstNewPlan { get; set; }
    }
}
