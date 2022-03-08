using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetMigrationPlans
{
    [DataContract(Name = "MigrationPlanResponseTransactions")]
    public class MigrationPlanResponse
    {
        [DataMember]
        public List<NewPlan> lstNewPlan { get; set; }
    }
}
