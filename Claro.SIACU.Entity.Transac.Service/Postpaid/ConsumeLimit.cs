using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Transac.Service.Postpaid
{
    [Data.DbTable("TEMPO")]
    [DataContract(Name = "ComsumeLimitTransactions")]
    public class ConsumeLimit
    {

        [DataMember]
        [Data.DbColumn("Desc_Serv")]
        public string DESC_SERV { get; set; }

        [DataMember]
        [Data.DbColumn("Co_Serv")]
        public string CO_SER { get; set; }

        [DataMember]
        [Data.DbColumn("Tope")]
        public string TOPE { get; set; }

        [DataMember]
        [Data.DbColumn("Ciclo")]
        public string CICLO { get; set; }

        [DataMember]
        [Data.DbColumn("Des")]
        public string DES { get; set; }
    }
}
