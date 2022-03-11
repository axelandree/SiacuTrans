using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed
{
    [DataContract]
    public class CommercialService
    {
        [DataMember]
        public string DE_GRP { get; set; }
        [DataMember]
        public string NO_GRP { get; set; }
        [DataMember]
        public string CO_SER { get; set; }
        [DataMember]
        public string DE_SER { get; set; }
        [DataMember]
        public string NO_SER { get; set; }
        [DataMember]
        public string CO_EXCL { get; set; }
        [DataMember]
        public string DE_EXCL { get; set; }
        [DataMember]
        public string ESTADO { get; set; }
        [DataMember]
        public string VALIDO_DESDE { get; set; }
        [DataMember]
        public string SUSCRIP { get; set; }
        [DataMember]
        public string CARGOFIJO { get; set; }
        [DataMember]
        public string CUOTA { get; set; }
        [DataMember]
        public string PERIODOS { get; set; }
        [DataMember]
        public string BLOQ_ACT { get; set; }
        [DataMember]
        public string BLOQ_DES { get; set; }
        [DataMember]
        public string SNCODE { get; set; }
        [DataMember]
        public string SPCODE { get; set; }
        [DataMember]
        public string VALORPVU { get; set; }
        [DataMember]
        public string COSTOPVU { get; set; }
        [DataMember]
        public string DESCOSER { get; set; }
        [DataMember]
        public string TIPOSERVICIO { get; set; }
        [DataMember]
        public string TIPO_SERVICIO { get; set; }
        [DataMember]
        public decimal DESCUENTO { get; set; }
        [DataMember]
        public string CODSERPVU { get; set; }
        
        public CommercialService()
        {
            CUOTA = string.Empty;
            VALORPVU = string.Empty;
            DESCOSER = string.Empty;
            TIPOSERVICIO = string.Empty;
            CODSERPVU = string.Empty;
        }


    }

}
