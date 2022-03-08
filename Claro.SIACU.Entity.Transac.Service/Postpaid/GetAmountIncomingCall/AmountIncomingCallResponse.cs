using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetAmountIncomingCall
{
    [DataContract(Name = "AmountIncomingCallResponse")]
    public class AmountIncomingCallResponse
    {
        [DataMember]
        public string Message { get; set; }
        [DataMember]
        public List<AmountIncomingCall> ListAmountIncomingCall { get; set; }
    }
}
