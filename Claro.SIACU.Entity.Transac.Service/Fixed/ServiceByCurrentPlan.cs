using System.Runtime.Serialization;
using Claro.Data;
namespace Claro.SIACU.Entity.Transac.Service.Fixed
{
    [DbTable("TEMPO")]
    [DataContract(Name = "ServiceByCurrentPlan")]
    public class ServiceByCurrentPlan
    {
        [DbColumn("DE_GRP")]
        [DataMember]
        public string DeGrp { get; set; }
        [DbColumn("NO_GRP")]
        [DataMember]
        public string NoGrp { get; set; }
        [DbColumn("DE_SER")]
        [DataMember]
        public string DeSer { get; set; }
        [DbColumn("CARGOFIJO")]
        [DataMember]
        public string CargoFijo { get; set; }
        [DbColumn("TIPO_SERVICIO")]
        [DataMember]
        public string ServiceType { get; set; }
        [DbColumn("SNCODE")]
        [DataMember]
        public string SnCode { get; set; }
        [DbColumn("STATUS")]
        [DataMember]
        public string Status { get; set; }

        //ADD COLUMN CF WITH IGV 02-02-2018
        [DataMember]
        public string CargoFijoConIgv { get; set; }
    }
}
