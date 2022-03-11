using System.Runtime.Serialization;


namespace Claro.SIACU.Entity.Transac.Service.Fixed
{
    [DataContract]
    public class InteractionBpel
    {
        [DataMember]
        public string Account { get; set; }
        [DataMember]
        public string Phone { get; set; }
        [DataMember]
        public string Tipo { get; set; }
        [DataMember]
        public string Clase { get; set; }
        [DataMember]
        public string Subclase { get; set; }
        [DataMember]
        public string MetodoContacto { get; set; }
        [DataMember]
        public string TipoInter { get; set; }
        [DataMember]
        public string Agente { get; set; }
        [DataMember]
        public string UsrProceso { get; set; }
        [DataMember]
        public string FlagCaso { get; set; }
        [DataMember]
        public string Resultado { get; set; }
        [DataMember]
        public string CoId { get; set; }
        [DataMember]
        public string CodPlano { get; set; }
        [DataMember]
        public string Contactobjid { get; set; }
        [DataMember]
        public string HechoEnUno { get; set; }
        [DataMember]
        public string Inconven { get; set; }
        [DataMember]
        public string InconvenCode { get; set; }
        [DataMember]
        public string Notas { get; set; }
        [DataMember]
        public string Servafect { get; set; }
        [DataMember]
        public string ServafectCode { get; set; }
        [DataMember]
        public string Siteobjid { get; set; }
        [DataMember]
        public string Valor1 { get; set; }
        [DataMember]
        public string Valor2 { get; set; }
    }
}
