using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetQueryAssociatedLines
{
    [DataContract(Name = "QueryAssociatedLinesResponsePostPaid")]
    public class QueryAssociatedLinesResponse
    {
        [DataMember]
        public string Result { get; set; }

        [DataMember]

        public List<CallDetailSummary> Total { get; set; }
    }
}
