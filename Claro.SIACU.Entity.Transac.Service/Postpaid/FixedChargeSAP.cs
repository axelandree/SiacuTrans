using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid
{
    [Data.DbTable("TEMPO")]
    [DataContract(Name = "FixedChargeSAPPostPaid")]
    public class FixedChargeSAP
    {
         [DataMember]
        [Data.DbColumn("CARGO_FIJO")]
        public string CARGO_FIJO { get; set; }

         [DataMember]
         [Data.DbColumn("NUMERO_CONTRATO")]
         public string NUMERO_CONTRATO { get; set; }

         [DataMember]
         [Data.DbColumn("PLAN_TARIFARIO")]
         public string PLAN_TARIFARIO { get; set; }

         [DataMember]
         [Data.DbColumn("DES_PLAN_TARIF")]
         public string DES_PLAN_TARIF { get; set; }

         [DataMember]
         [Data.DbColumn("FECHA_CONTATO")]
         public string FECHA_CONTATO { get; set; }


    }
}
