using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo
{
    [DataContract(Name = "CancelarSuscripcionSNRequest")]
    public class CancelarSuscripcionSNRequest
    {
        [DataMember(Name = "cancelAccountRequest")]
        public CancelAccountRequest cancelAccountRequest { get; set; }
    }
}
