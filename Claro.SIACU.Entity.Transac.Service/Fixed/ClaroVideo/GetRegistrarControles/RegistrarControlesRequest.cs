using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo.GetRegistrarControles
{
      [DataContract]
    public class RegistrarControlesRequest : Tools.Entity.Request
    {
          [DataMember(Name = "MessageRequest")]
          public RegistrarControlesMessageRequest MessageRequest { get; set; }
    }
}
