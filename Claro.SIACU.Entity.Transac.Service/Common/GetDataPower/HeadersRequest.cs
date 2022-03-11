using System;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetDataPower
{
    [DataContract]
    public class HeadersRequest
    {
        [DataMember(Name = "HeaderRequest")]
        public HeaderRequest HeaderRequest { get; set; }
    }
}
