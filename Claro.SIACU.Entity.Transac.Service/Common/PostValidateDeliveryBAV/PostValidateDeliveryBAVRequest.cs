using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.PostValidateDeliveryBAV
{
    [DataContract]
    public class PostValidateDeliveryBAVRequest : Claro.Entity.Request
    {
        [DataMember(Name = "MessageRequest")]
        public PostValidateDeliveryBAVMessageRequest validateDeliveryBAVMessageRequest { get; set; }
    }
}
