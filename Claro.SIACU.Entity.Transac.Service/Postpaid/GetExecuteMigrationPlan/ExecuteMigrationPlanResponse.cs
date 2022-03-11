using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetExecuteMigrationPlan
{
    [DataContract(Name = "ExecuteMigrationPlanResponseTransactions")]
    public class ExecuteMigrationPlanResponse
    {
        [DataMember]
        public string CodResult { get; set; }
        [DataMember]
        public string MenssageResult { get; set; }
    }
}
