using Claro.SIACU.Entity.Transac.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetCallDetailNBDB1
{
    [DataContract(Name = "CallDetailNBDB1ResponsePostPaid")]
    public class CallDetailNBDB1Response
    {
        [DataMember]
        public string MsgError { get; set; }

        [DataMember]
        public List<CallDetailGeneric> ListCallDetail { get; set; }
        [DataMember]
        public string SummaryTotal { get; set; }
    } 
}
