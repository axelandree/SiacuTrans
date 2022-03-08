using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid
{
    [Data.DbTable("SIAC_PARAMETRO")]
    [DataContract(Name = "SiacParametro")]
    public class SiacParameter
    {
        [DataMember]
        [Data.DbColumn("PARAMETRO_ID")]
        public int Parametro_ID { get; set; }
        [DataMember]
        [Data.DbColumn("NOMBRE")]
        public string Nombre { get; set; }
        [DataMember]
        [Data.DbColumn("DESCRIPCION")]
        public string Descripcion { get; set; }
        [DataMember]
        [Data.DbColumn("TIPO")]
        public int Tipo { get; set; }
        [DataMember]
        [Data.DbColumn("VALOR_C")]
        public string Valor_C { get; set; }
        [DataMember]
        [Data.DbColumn("VALOR_N")]
        public decimal Valor_N { get; set; }
        [DataMember]
        [Data.DbColumn("VALOR_L")]
        public string Valor_L { get; set; }

    }
}
