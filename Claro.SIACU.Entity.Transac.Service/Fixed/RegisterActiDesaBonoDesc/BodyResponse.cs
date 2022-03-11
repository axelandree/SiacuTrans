using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.RegisterActiDesaBonoDesc
{
    [DataContract]
    public class BodyResponse
    {
        [DataMember(Name = "registrarDescHFCResponse")]
        public registrarDescHFCResponse registrarDescHFCResponse { get; set; }

        [DataMember(Name = "registrarDescLTEResponse")]
        public registrarDescLTEResponse registrarDescLTEResponse { get; set; }

        
    }
}
