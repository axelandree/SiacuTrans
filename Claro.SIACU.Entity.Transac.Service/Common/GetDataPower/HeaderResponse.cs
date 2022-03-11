using System;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetDataPower
{
    [DataContract]
    public class HeaderResponse
    {
        [DataMember(Name = "consumer")]
        public string Consumer { get; set; }
        [DataMember(Name = "pid")]
        public string Pid { get; set; }
        [DataMember(Name = "status")]
        public Status Status { get; set; }
        [DataMember(Name = "timestamp")]
        public string Timestamp { get; set; }
        [DataMember(Name = "varArg")]
        public string VarArg { get; set; }
    }
}
