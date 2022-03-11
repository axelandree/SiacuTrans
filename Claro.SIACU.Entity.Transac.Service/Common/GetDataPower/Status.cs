using System;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetDataPower
{
    [DataContract]
    public class Status
    {
        [DataMember(Name = "code")]
        public string Code { get; set; }
        [DataMember(Name = "message")]
        public string Message { get; set; }
        [DataMember(Name = "msgid")]
        public string Msgid { get; set; }
        [DataMember(Name = "type")]
        public string Type { get; set; }
    }
}
