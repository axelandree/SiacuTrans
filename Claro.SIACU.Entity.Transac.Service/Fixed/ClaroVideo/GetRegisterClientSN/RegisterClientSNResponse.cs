using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo.GetRegisterClientSN
{
    [DataContract]
    public class RegisterClientSNResponse: Tools.Entity.Response
    {
        [DataMember(Name="MessageResponse")]
        public RegisterClientSNMessageResponse MessageResponse { get; set; }
    }
}
