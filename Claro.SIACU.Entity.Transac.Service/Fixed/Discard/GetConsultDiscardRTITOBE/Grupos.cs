using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.Discard.GetConsultDiscardRTITOBE
{
    [DataContract]
    public class Grupos
    {
        [DataMember]
        public string idGrupo { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public string tipo { get; set; }
        [DataMember]
        public string flagVisual { get; set; }
        [DataMember]
        public string orden { get; set; }
        [DataMember]
        public string usuarioRegistro { get; set; }
        [DataMember]
        public string fechaRegistro { get; set; }
        [DataMember]
        public string usuarioModificacion { get; set; }
        [DataMember]
        public string fechaModificacion { get; set; }
        [DataMember]
        public string estadoRegistro { get; set; }
    }
}
