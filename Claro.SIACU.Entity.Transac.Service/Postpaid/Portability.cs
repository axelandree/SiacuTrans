using Claro.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid
{
    public class Portability
    {
        [DataMember]
        public string INICIO_RANGO { get; set; }
        [DbColumn("SOPOC_ESTA_PROCESO")]
        [DataMember]
        public string CODIGO_ESTADO_PROCESO { get; set; }
        [DataMember]
        public string NOMBRE_ANEXO { get; set; }
        [DataMember]
        public string ID_CORRELATIVO { get; set; }
        [DbColumn("SOPOC_ID_SOLICITUD")]
        [DataMember]
        public string NUMERO_SOLICITUD { get; set; }
        [DbColumn("SOPOC_TIPO_PORTA")]
        [DataMember]
        public string TIPO_PORTABILIDAD { get; set; }
        [DataMember]
        public string ESTADO_PROCESO { get; set; }
        [DbColumn("PARAV_DESCRIPCION")]
        [DataMember]
        public string DESCRPCION_ESTADO_PROCESO { get; set; }
        [DbColumn("SOPOT_FECHA_REGISTRO")]
        [DataMember]
        public DateTime FECHA_REGISTRO { get; set; }
        [DbColumn("SOPOT_FECHA_EJECUCION")]
        [DataMember]
        public DateTime FECHA_EJECUCION { get; set; }
        [DbColumn("SOPOC_CODIGO_CEDENTE")]
        [DataMember]
        public string CODIGO_OPERADOR_CEDENTE { get; set; }
        [DbColumn("SOPOC_CODIGO_RECEPTOR")]
        [DataMember]
        public string CODIGO_OPERADOR_RECEPTOR { get; set; }
        [DataMember]
        public string USUARIO_CAMBIO_ESTADO { get; set; }
        [DataMember]
        public string USUARIO_CREA { get; set; }
        [DataMember]
        public string OBSERVACIONES { get; set; }
        [DataMember]
        public string CAC { get; set; }
        [DataMember]
        public string RESPUESTA { get; set; }
        [DataMember]
        public string TIPO_PROCESO_FECHA { get; set; }
        [DataMember]
        public string TIPO_PROCESO_OPERADOR { get; set; }
        [DataMember]
        public string OPERADOR { get; set; }
    }
}
