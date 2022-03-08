using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo.GetRegistrarControles
{
     [DataContract(Name = "RegistrarControlesBodyRequest")]
    public class RegistrarControlesBodyRequest
    {
         [DataMember(Name = "registrarControlesCvRequest")]
         public RegistrarControlesCvRequest registrarControlesCvRequest { get; set; }
    }
}
