using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.PostValidateDeliveryBAV
{
    [DataContract]
    public class PostValidateDeliveryBAVMessageResponse
    {
        [DataMember(Name = "Header")]
        public Common.GetDataPower.HeadersResponse Header { get; set; }

        [DataMember(Name = "Body")]
        public PostValidateDeliveryBAVBodyResponse ValidateDeliveryBAVBodyResponse { get; set; }
    }
}
