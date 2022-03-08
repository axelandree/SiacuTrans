using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetAgreement
{
    [DataContract(Name = "AgreementResponse")]
    public class AgreementResponse
    {
        [DataMember]
        public string ACUERDO_CADUCADO { get; set; }
        [DataMember]
        public string ACUERDO_ESTADO { get; set; }
        [DataMember]
        public string ACUERDO_FECHA_FIN { get; set; }
        [DataMember]
        public string ACUERDO_FECHA_INICIO { get; set; }
        [DataMember]
        public string ACUERDO_ID { get; set; }
        [DataMember]
        public string ACUERDO_MONTO_APADECE_TOTAL { get; set; }
        [DataMember]
        public string ACUERDO_ORIGEN { get; set; }
        [DataMember]
        public string ACUERDO_VIGENCIA_MES { get; set; }
        [DataMember]
        public string CARGO_FIJO_DIARIO { get; set; }
        [DataMember]
        public string CODIGO_PLAZO_ACUERDO { get; set; }
        [DataMember]
        public string CO_ID { get; set; }
        [DataMember]
        public string CUSTOMER_ID { get; set; }
        [DataMember]
        public string DESCRIPCION_PLAZO_ACUERDO { get; set; }
        [DataMember]
        public string DESCRIPCION_ESTADO_ACUERDO { get; set; }
        [DataMember]
        public string DIAS_BLOQUEO { get; set; }
        [DataMember]
        public string DIAS_PENDIENTES { get; set; }
        [DataMember]
        public string DIAS_VIGENCIA { get; set; }
        [DataMember]
        public string FIN_VIGENCIA_REAL { get; set; }
        [DataMember]
        public string MESES_ANTIGUEDAD { get; set; }
        [DataMember]
        public string MESES_PENDIENTES { get; set; }
        [DataMember]
        public string MONTO_APADECE { get; set; }
        [DataMember]
        public string PRECIO_LISTA { get; set; }
        [DataMember]
        public string PRECIO_VENTA { get; set; }
        [DataMember]
        public string PENALIDAD { get; set; }
        [DataMember]
        public string CodRespuesta { get; set; }
        [DataMember]
        public string MesajeRespuesta { get; set; }
    }
}
