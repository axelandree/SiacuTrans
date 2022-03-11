using System;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed
{
    [DataContract]
    public class TransactionSiga
    {
        [DataMember]
        public string MSISDN { get; set; }
        [DataMember]
        public int MOTIVO_APADECE { get; set; }
        [DataMember]
        public int FLAG_EQUIPO { get; set; }
        [DataMember]
        public int ESTADO_ACUERDO { get; set; }
        [DataMember]
        public int ESTADO_APADECE { get; set; }
        [DataMember]
        public double MONTO_FIDELIZA { get; set; }
        [DataMember]
        public string COD_TIPO_OPERACION { get; set; }
        [DataMember]
        public string DIRECCION_CLIENTE { get; set; }
        [DataMember]
        public string FUENTE_ACTUALIZACI { get; set; }
        [DataMember]
        public string NOMBRE_CLIENTE { get; set; }
        [DataMember]
        public string NRO_DOC_CLIENTE { get; set; }
        [DataMember]
        public string RAZON_SOCIAL { get; set; }
        [DataMember]
        public string NRO_DOC_PAGO { get; set; }
    }
}
