using System.Runtime.Serialization;
using Claro.Data;
namespace Claro.SIACU.Entity.Transac.Service.Fixed
{
    [DbTable("TEMPO")]
    [DataContract(Name = "EquipmentByCurrentPlan")]
    public class EquipmentByCurrentPlan
    {
        [DbColumn("DSCEQU")]
        [DataMember]
        public string Description { get; set; }
        [DbColumn("TIPSRV")]
        [DataMember]
        public string ServiceType { get; set; }
        [DbColumn("CARGO_FIJO")]
        [DataMember]
        public string CF { get; set; }
        [DbColumn("CANTIDAD")]
        [DataMember]
        public string CANTIDAD { get; set; }
    }
}
