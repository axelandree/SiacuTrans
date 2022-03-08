using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid
{
    [Data.DbTable("TEMPO")]
    [DataContract(Name = "NewPlanTransactions")]
    public class NewPlan
    {
        [DataMember]
        [Data.DbColumn("COD_PROD")]
        public string COD_PROD { get; set; }
        [DataMember]
        [Data.DbColumn("TMCODE")]
        public string TMCODE { get; set; }
        [DataMember]
        [Data.DbColumn("DESC_PLAN")]
        public string DESC_PLAN { get; set; }
        [DataMember]
        [Data.DbColumn("VERSION")]
        public string VERSION { get; set; }
        [DataMember]
        [Data.DbColumn("CAT_PROD")]
        public string CAT_PROD { get; set; }
        [DataMember]
        [Data.DbColumn("COD_CARTA_INFO")]
        public string COD_CARTA_INFO { get; set; }
        [DataMember]
        [Data.DbColumn("FECHA_INI_VIG")]
        public string FECHA_INI_VIG { get; set; }
        [DataMember]
        [Data.DbColumn("FECHA_FIN_VIG")]
        public string FECHA_FIN_VIG { get; set; }
        [DataMember]
        [Data.DbColumn("ID_TIPO_PROD")]
        public string ID_TIPO_PROD { get; set; }
        [DataMember]
        [Data.DbColumn("USUARIO")]
        public string USUARIO { get; set; }
    }
}
