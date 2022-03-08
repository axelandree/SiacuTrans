using System.Runtime.Serialization;
using Claro.Data;
using System.Collections.Generic;
namespace Claro.SIACU.Entity.Transac.Service.Fixed
{
    [DataContract(Name = "CurrentPlan")]
    public class CurrentPlan
    {
        [DbColumn("PLAN_ID")]
        [DataMember]
        public string PLAN_ID { get; set; }
        [DbColumn("PLAN_DESCRIPCION")]
        [DataMember]
        public string PLAN_DESCRIPCION { get; set; }
        [DbTable("SERVICIOS")]
        [DataMember]
        public List<ServiceByPlan> Services { get; set; }
    }
}
