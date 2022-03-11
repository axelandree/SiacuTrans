using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;


namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetCaseInsert
{
     [DataContract]
    public class CaseInsertRequest : Claro.Entity.Request
    {

        [DataMember]
        public string OBJID_CONTACTO { get; set; }
        [DataMember]
         public string OBJID_SITE { get; set; }
        [DataMember]
         public string OBJID_CASO { get; set; }
        [DataMember]
        public string CUENTA { get; set; }
        [DataMember]
        public string ID_CASO { get; set; }
        [DataMember]
        public string COD_CASO { get; set; }
        [DataMember]
        public string FECHA_CREACION { get; set; }
        [DataMember]
         public string TELEFONO { get; set; }
        [DataMember]
        public string SITIO { get; set; }

        [DataMember]
        public string TIPIFICACION { get; set; }
        [DataMember]
        public string TIPO_CODE { get; set; }
        [DataMember]
        public string TIPO { get; set; }
        [DataMember]
        public string CLASE_CODE { get; set; }
        [DataMember]
        public string CLASE { get; set; }
        [DataMember]
        public string SUBCLASE { get; set; }
        [DataMember]
        public string SUBCLASE_CODE { get; set; }

        [DataMember]
        public string ORIGEN { get; set; }
        [DataMember]
        public string ESTADO { get; set; }
        [DataMember]
        public string PRIORIDAD { get; set; }
        [DataMember]
        public string SEVERIDAD { get; set; }
        [DataMember]
        public string FASE { get; set; }
        [DataMember]
        public string NRO_REPORTE { get; set; }
        [DataMember]
        public string NRO_RECLAMO { get; set; }
        [DataMember]
        public string CONDICION { get; set; }
        [DataMember]
        public string COLA { get; set; }
        [DataMember]
        public string PROPIETARIO { get; set; }
        [DataMember]
        public string NOMBRE_AGENTE { get; set; }
        [DataMember]
        public string APELLIDO_AGENTE { get; set; }
        [DataMember]
        public string RESULTADO { get; set; }
        [DataMember]
        public string RESOLUCION { get; set; }
        [DataMember]
        public string METODO_CONTACTO { get; set; }
        [DataMember]
        public string TIPO_INTERACCION { get; set; }
        [DataMember]
        public string NOTAS { get; set; }
        [DataMember]
        public string FLAG_CREACION { get; set; }
        [DataMember]
        public string FLAG_INTERACCION { get; set; }
        [DataMember]
        public string USUARIO_PROCESO { get; set; }
        [DataMember]
        public string USUARIO_ID { get; set; }
        [DataMember]
        public string INCIDENCIA_SD { get; set; }
        [DataMember]
        public string FLAG_WORKGROUP { get; set; }
        [DataMember]
        public string PAGINA_CASO { get; set; }
        [DataMember]
        public string SERVICIO { get; set; }
        [DataMember]
        public string INCONVENIENTE { get; set; }

        [DataMember]
        public string SERVICIO_CODE { get; set; }
        [DataMember]
        public string INCONVENIENTE_CODE { get; set; }
        [DataMember]
        public string CONTRATO { get; set; }
        [DataMember]
        public string PLANO { get; set; }
        [DataMember]
        public string VALOR_1 { get; set; }
        [DataMember]
        public string VALOR_2 { get; set; }
        [DataMember]
        public string DUMMY_ID { get; set; }
        [DataMember]
        public string CASO_PADRE_ID { get; set; }
        [DataMember]
        public string ID_INTERACCION { get; set; }
         
        [DataMember]
        public string ACCOUNT { get; set; }
        [DataMember]
        public string PHONE { get; set; }

        [DataMember]
        public string CASO_ID { get; set; }

        [DataMember]
        public string FLAT_INTERACCION { get; set; }
        [DataMember]
        public string MESSAGE { get; set; }
         
    }
}
