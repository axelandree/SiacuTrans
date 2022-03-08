using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed
{
    [DataContract]
    public class ContactUserBpel
    {
        [DataMember]
        public string Usuario { get; set; }
        [DataMember]
        public string Nombres { get; set; }
        [DataMember]
        public string Apellidos { get; set; }
        [DataMember]
        public string RazonSocial { get; set; }
        [DataMember]
        public string TipoDoc { get; set; }
        [DataMember]
        public string NumDoc { get; set; }
        [DataMember]
        public string Domicilio { get; set; }
        [DataMember]
        public string Distrito { get; set; }
        [DataMember]
        public string Departamento { get; set; }
        [DataMember]
        public string Provincia { get; set; }
        [DataMember]
        public string Modalidad { get; set; }
    }
}
