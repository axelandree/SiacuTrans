using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetNetflixServices
{
    [DataContract]
    public class ServicesNXRequest
    {
        [DataMember(Name = "MessageRequest")]
        public ServicesNXMessageRequest MessageRequest { get; set; } 
    }
}
