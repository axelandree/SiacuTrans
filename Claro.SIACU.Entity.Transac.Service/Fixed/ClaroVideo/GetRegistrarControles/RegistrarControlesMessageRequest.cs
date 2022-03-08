using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo.GetRegistrarControles
{
    public class RegistrarControlesMessageRequest
    {
        [DataMember(Name = "Header")]
        public RegistrarControlesHeaderRequest Header { get; set; }

        [DataMember(Name = "Body")]
        public RegistrarControlesBodyRequest Body { get; set; }
    }

}
