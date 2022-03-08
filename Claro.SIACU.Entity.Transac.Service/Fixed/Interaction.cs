using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed
{
    [DataContract]
    public class Interaction: Claro.Entity.Request
    {
        public Interaction()
        {
            
        }

        [DataMember]
        public string OBJID_CONTACTO { get; set; }
        [DataMember]
        public string OBJID_SITE { get; set; }
        [DataMember]
        public string CUENTA { get; set; }
        [DataMember]
        public string ID_INTERACCION { get; set; }
        [DataMember]
        public string FECHA_CREACION { get; set; }
        [DataMember]
        public string START_DATE { get; set; }
        [DataMember]
        public string TELEFONO { get; set; }
        [DataMember]
        public string TIPO { get; set; }
        [DataMember]
        public string CLASE { get; set; }
        [DataMember]
        public string SUBCLASE { get; set; }
        [DataMember]
        public string TIPIFICACION { get; set; }
        [DataMember]
        public string TIPO_CODIGO { get; set; }
        [DataMember]
        public string CLASE_CODIGO { get; set; }
        [DataMember]
        public string SUBCLASE_CODIGO { get; set; }
        [DataMember]
        public string INSERTADO_POR { get; set; }
        [DataMember]
        public string TIPO_INTER { get; set; }
        [DataMember]
        public string METODO { get; set; }
        [DataMember]
        public string RESULTADO { get; set; }
        [DataMember]
        public string HECHO_EN_UNO { get; set; }
        [DataMember]
        public string AGENTE { get; set; }
        [DataMember]
        public string NOMBRE_AGENTE { get; set; }
        [DataMember]
        public string APELLIDO_AGENTE { get; set; }
        [DataMember]
        public string ID_CASO { get; set; }
        [DataMember]
        public string NOTAS { get; set; }
        [DataMember]
        public string FLAG_CASO { get; set; }
        [DataMember]
        public string USUARIO_PROCESO { get; set; }
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
        public string PRIORIDAD { get; set; }
        [DataMember]
        public string SEVERIDAD { get; set; }
        [DataMember]
        public string COLA { get; set; }
        [DataMember]
        public string FLAG_INTERACCION { get; set; }
        [DataMember]
        public string USUARIO_ID { get; set; }
        [DataMember]
        public string TIPO_INTERACCION { get; set; }
        [DataMember]
        public string DUMMY_ID { get; set; }
        [DataMember]
        public string CASO_PADRE_ID { get; set; }
        [DataMember]
        public string CASO_ID { get; set; }
        [DataMember]
        public string FLAG_INSERCION { get; set; }
        [DataMember]
        public string FLAG_INSERCION_CASO { get; set; }
        [DataMember]
        public string MESSAGE_CASO { get; set; }  
   

    }
}
