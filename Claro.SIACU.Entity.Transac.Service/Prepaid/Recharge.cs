using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Prepaid
{
    public class Recharge
    {
        [DataMember]
        [Data.DbColumn("CREDDEBI")]
        public string CREDITO { get; set; }

        [DataMember]
        [Data.DbColumn("FECHARECARGA")]
        public string FECHA_RECARGA { get; set; }

        [DataMember]
        [Data.DbColumn("MONTOEFECTIVO")]
        public string MONTO_EFECTIVO { get; set; }

        [DataMember]
        [Data.DbColumn("MONTONOMINAL")]
        public string MONTO_NOMINAL { get; set; }

        [DataMember]
        [Data.DbColumn("BOLSARECARGA")]
        public string BOLSA_RECARGA { get; set; }

        [DataMember]
        [Data.DbColumn("TIPORECARGA")]
        public string TIPO_RECARGA { get; set; }

        [DataMember]
        [Data.DbColumn("DETRECARGA")]
        public string DETALLE_RECARGA { get; set; }

        [DataMember]
        [Data.DbColumn("SALDO")]
        public string SALDO { get; set; }

        [DataMember]
        [Data.DbColumn("DESTIPORECARGA")]
        public string DES_TIPO_RECARGA { get; set; }
    }
}
