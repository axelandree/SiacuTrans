using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.PostValidateDeliveryBAV
{
    [DataContract]
    public class PostValidateDeliveryBAVResponse
    {
        [DataMember(Name = "MessageResponse")]
        public PostValidateDeliveryBAVMessageResponse validateDeliveryBAVMessageResponse { get; set; }
    }
}
