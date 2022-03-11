using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.RegisterActiDesaBonoDesc
{
    [DataContract]
    public class registrarDescHFCResponse
    {
        [DataMember(Name = "auditResponse")]
        public auditResponse auditResponse { get; set; }
        [DataMember(Name = "listaResponseOpcional")]
        public listaResponseOpcional listaResponseOpcional { get; set; }
    }
}
