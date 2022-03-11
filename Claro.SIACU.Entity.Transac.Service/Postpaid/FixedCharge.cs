using System.Runtime.Serialization;
    
namespace Claro.SIACU.Entity.Transac.Service.Postpaid
{
    [Data.DbTable("TEMPO")]
    [DataContract(Name = "FixedChargePostPaid")]
    public class FixedCharge
    {
        [DataMember]
        [Data.DbColumn("CARGO_FIJO")]
        public string CARGO_FIJO { get; set; }
        [DataMember]
        [Data.DbColumn("ID_CONTRATO")]
        public string ID_CONTRATO { get; set; }
        [DataMember]
        [Data.DbColumn("COD_PLAN")]
        public string COD_PLAN { get; set; }
        [DataMember]
        [Data.DbColumn("DES_PLAN")]
        public string DES_PLAN { get; set; }
        [DataMember]
        [Data.DbColumn("FECHA_CONTRATO")]
        public string FECHA_CONTRATO { get; set; }

    }
}
