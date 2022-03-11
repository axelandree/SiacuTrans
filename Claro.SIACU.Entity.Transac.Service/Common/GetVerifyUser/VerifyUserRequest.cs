using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetVerifyUser
{
    [DataContract]
    public class VerifyUserRequest : Claro.Entity.Request
    {
        [DataMember]
        public string SessionId { get; set; }
        [DataMember]
        public string TransactionId { get; set; }
        [DataMember]
        public string AppId { get; set; }
        [DataMember]
        public string AppName { get; set; }
        [DataMember]
        public string Username { get; set; }
        [DataMember]
        public string AppCode { get; set; }
    }
}
