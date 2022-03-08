
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetMaintenancePlan
{
    [DataContract(Name = "MaintenancePlanResponseTransactions")]
    public class MaintenancePlanResponse
    {
        [DataMember]
        public int FlgTopeAutomatico { get; set; }
        [DataMember]
        public int FlgCincoSoles { get; set; }
        [DataMember]
        public int FlgAdicionales { get; set; }
    }
}
