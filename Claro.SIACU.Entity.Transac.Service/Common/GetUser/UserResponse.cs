using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetUser
{
    [DataContract]
    public class UserResponse
    {
        [DataMember]
        public User UserModel { get; set; }
    }
}
