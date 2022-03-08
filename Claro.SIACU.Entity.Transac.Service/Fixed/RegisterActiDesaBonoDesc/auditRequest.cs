using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.RegisterActiDesaBonoDesc
{
    [DataContract]
    public class auditRequest
    {
        [DataMember(Name = "idTransaccion")]
        public string idTransaccion { get; set; }
        [DataMember(Name = "ipAplicacion")]
        public string ipAplicacion { get; set; }
        [DataMember(Name = "nombreAplicacion")]
        public string nombreAplicacion { get; set; }
        [DataMember(Name = "usuarioAplicacion")]
        public string usuarioAplicacion { get; set; }
    }
}
