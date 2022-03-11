using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetTimeZones
{
    [DataContract(Name = "TimeZonesRequest")]
    public class TimeZonesRequest : Claro.Entity.Request
    {
        [DataMember]
        public string strAnUbigeo { get; set; }
        [DataMember]
        public string strAnTiptra { get; set; }
        [DataMember]
        public string strAdFecagenda { get; set; }

    }
}
