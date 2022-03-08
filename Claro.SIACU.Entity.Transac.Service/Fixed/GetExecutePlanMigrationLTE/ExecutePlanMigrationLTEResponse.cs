using System.Runtime.Serialization;
using System.Collections.Generic;
using Claro.SIACU.Entity.Transac.Service.Fixed;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetExecutePlanMigrationLTE
{
    [DataContract(Name = "ExecutePlanMigrationLTEResponse")]
    public class ExecutePlanMigrationLTEResponse
    {
        [DataMember]
        public OsbLteEntity result { get; set; }
    }
}
