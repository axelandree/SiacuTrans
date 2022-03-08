using System.Runtime.Serialization;
using System.Collections.Generic;
using Claro.SIACU.Entity.Transac.Service.Fixed;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ExecutePlanMigrationLte
{
    [DataContract(Name = "MigratedPlanResponseLte")]
    public class MigratedPlanResponse
    {
        [DataMember]
        public OsbLteEntity result { get; set; }
    }
}
