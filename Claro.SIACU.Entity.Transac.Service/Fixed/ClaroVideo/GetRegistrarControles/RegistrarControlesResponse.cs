using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo.GetRegistrarControles
{
      [DataContract]
    public class RegistrarControlesResponse : Tools.Entity.Response
    {
          [DataMember(Name = "MessageResponse")]
          public RegistrarControlesMessageResponse MessageResponse { get; set; }
    }
}
