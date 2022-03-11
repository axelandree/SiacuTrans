using System;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.RegisterActiDesaBonoDesc
{
    [DataContract]
    public class HeadersRequest
    {
        [DataMember(Name = "HeaderRequest")]
        public HeaderRequestADB HeaderRequest { get; set; }
    }
}
