using System.Runtime.Serialization;
using System.Collections.Generic;

namespace Claro.SIACU.Entity.Transac.Service.Common.ETAFlowValidate
{
    [DataContract(Name = "ETAFlowResponseHfc")]
    public class ETAFlowResponse
    {
        [DataMember]
        public ETAFlow ETAFlow { get; set; }
    }
}
