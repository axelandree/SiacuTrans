using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Claro.SIACU.Entity.Transac.Service.Fixed.BlackWhiteList.GetUpdateStateLineEmail
{
     [DataContract]
    public class UpdateStateLineEmailRequest : Tools.Entity.Request
    {
        [DataMember(Name = "MessageRequest")]
        public UpdateStateLineEmailMessageRequest MessageRequest { get; set; } 
    }
}
