using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo
{
    [DataContract(Name = "ContractServicesPostPaid")]
    public class ContractServices
    {
        [DataMember]
        public string COD_GRUPO { get; set; }

        [DataMember]
        public string DES_GRUPO { get; set; }

        [DataMember]
        public string POS_GRUPO { get; set; }

        [DataMember]
        public string COD_SERV { get; set; }

        [DataMember]
        public string DES_SERV { get; set; }

        [DataMember]
        public string POS_SERV { get; set; }

        [DataMember]
        public string COD_EXCLUYENTE { get; set; }

        [DataMember]
        public string DES_EXCLUYENTE { get; set; }

        [DataMember]
        public string ESTADO { get; set; }

        [DataMember]
        public string FECHA_VALIDEZ { get; set; }

        [DataMember]
        public string MONTO_CARGO_SUS { get; set; }

        [DataMember]
        public string MONTO_CARGO_FIJO { get; set; }

        [DataMember]
        public string CUOTA_MODIF { get; set; }
        [DataMember]
        public string MONTO_FINAL { get; set; }

        [DataMember]
        public string PERIODOS_VALIDOS { get; set; }

        [DataMember]
        public string BLOQUEO_ACT { get; set; }

        [DataMember]
        public string BLOQUEO_DESACT { get; set; }

        [DataMember]
        public string SPCODE { get; set; }

        [DataMember]
        public string SNCODE { get; set; }
    }
}
