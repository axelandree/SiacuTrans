using System.Runtime.Serialization;


namespace Claro.SIACU.Entity.Transac.Service.Postpaid
{
    [Data.DbTable("TEMPO")]
    public class TopConsumption
    {
        [DataMember]
       [Data.DbColumn("COD_PROD")]
       public string COD_PROD { get; set; }
      
        [DataMember]
        [Data.DbColumn("VERSION")]
         public string VERSION { get; set; }
         
        [DataMember]
        [Data.DbColumn("DE_EXCL")]
        public string  DE_EXCL { get; set; }
        [DataMember]
        [Data.DbColumn("CO_EXCL")]
        public string  CO_EXCL { get; set; }
        [DataMember]
        [Data.DbColumn("DE_SER")]
        public string  DE_SER { get; set; }
        [DataMember]
        [Data.DbColumn("PERIODOS")]
        public string  PERIODOS { get; set; }
        [DataMember]
        [Data.DbColumn("FECHA_MOD")]
        public string  FECHA_MOD { get; set; }
        [DataMember]
        [Data.DbColumn("FECHA_REG")]
        public string  FECHA_REG { get; set; }
        [DataMember]
        [Data.DbColumn("SPCODE")]
        public string  SPCODE { get; set; }
        [DataMember]
        [Data.DbColumn("SNCODE")]
        public string  SNCODE { get; set; }
        [DataMember]
        [Data.DbColumn("CO_SER")]
        public string  CO_SER { get; set; }
        [DataMember]
        [Data.DbColumn("USUARIO")]
        public string USUARIO { get; set; }
        [DataMember]
        [Data.DbColumn("ESTADO")]
        public string ESTADO { get; set; }
        [DataMember]
        [Data.DbColumn("CARGO_FIJO")]
        public string CARGO_FIJO { get; set; }
        [DataMember]
        public string TMCODE { get; set; }
    }
}
