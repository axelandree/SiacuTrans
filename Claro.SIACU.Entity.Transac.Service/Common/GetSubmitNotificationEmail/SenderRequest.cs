using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetSubmitNotificationEmail
{
    [DataContract]
    public class SenderRequest
    {
        [DataMember(Name = "Username")]
        public string Username {get;set;}
        [DataMember(Name = "Password")]
        public string Password {get;set;}
        [DataMember(Name = "FromAddress")]
        public string FromAddress {get;set;}
        [DataMember(Name = "DisplayName")]
        public string DisplayName { get; set; }
    }
}
