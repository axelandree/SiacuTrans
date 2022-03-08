using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.PostValidateDeliveryBAV
{
    [DataContract]
    public class PostValidateDeliveryBAVMessageRequest
    {
        [DataMember(Name = "Header")]
        public Common.GetDataPower.HeadersRequest header { get; set; }

        [DataMember(Name = "Body")]
        public PostValidateDeliveryBAVBodyRequest validateDeliveryBAVBodyRequest { get; set; }
    }
}
