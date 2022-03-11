using System;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo.GetRegistrarControles
{
    public class RegistrarControlesMessageResponse
    {
        [DataMember(Name = "Header")]
        public RegistrarControlesHeaderResponse Header { get; set; }

        [DataMember(Name = "Body")]
        public RegistrarControlesBodyResponse Body { get; set; }
    }
}
