using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Prepaid.GetIncomingCallDetail
{
    [DataContract(Name = "IncomingCallDetailResponsePrepaid")]
    public class IncomingCallDetailResponse
    {
        [DataMember]
        public string Result { get; set; }
        [DataMember]
        public List<IncomingCallDetail> ListIncomingCallDetail { get; set; }

    }
}
