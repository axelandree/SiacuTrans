using System.Runtime.Serialization;
using Claro.Data;

namespace Claro.SIACU.Entity.Transac.Service.Common
{
    [DataContract(Name = "TimeZone")]
    public class TimeZone
    {
        [DbColumn("TIPTRA")]
        [DataMember]
        public string TIPTRA { get; set; }
        [DbColumn("CODCON")]
        [DataMember]
        public string CODCON { get; set; }
        [DbColumn("CODCUADRILLA")]
        [DataMember]
        public string CODCUADRILLA { get; set; }
        [DbColumn("HORA")]
        [DataMember]
        public string HORA { get; set; }
        [DbColumn("COLOR")]
        [DataMember]
        public string COLOR { get; set; }
    }
}
