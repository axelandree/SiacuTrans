
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetMaintenancePlan
{
    [DataContract(Name = "MaintenancePlanRequestTransactions")]
    public class MaintenancePlanRequest : Claro.Entity.Request
    {
        [DataMember]
        public int Tmcode { get; set; }
    }
}
