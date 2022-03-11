using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed
{
    [DataContract]
    public class HeaderRequestTypeBpel
    {
        [DataMember]
        public string Canal { get; set; }
        [DataMember]
        public string IdAplicacion { get; set; }
        [DataMember]
        public string UsuarioAplicacion { get; set; }
        [DataMember]
        public string UsuarioSesion { get; set; }
        [DataMember]
        public string IdTransaccionEsb { get; set; }
        [DataMember]
        public string IdTransaccionNegocio { get; set; }
        [DataMember]
        public System.DateTime FechaInicio { get; set; }
        [DataMember]
        public object NodoAdicional { get; set; }
    }
}
