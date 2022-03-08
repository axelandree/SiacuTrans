using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo.GetRegistrarControles
{
    [DataContract(Name = "RegistrarControlesBodyResponse")]
    public class RegistrarControlesBodyResponse
    {
        [DataMember(Name = "registrarcontrolescvresponse")]
        public RegistrarcontrolescvResponse registrarcontrolescvresponse { get; set; }
    }
}
