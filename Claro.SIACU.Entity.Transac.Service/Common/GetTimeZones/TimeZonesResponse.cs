using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetTimeZones
{
    [DataContract(Name = "TimeZonesResponse")]
    public class TimeZonesResponse
    {
        [DataMember]
        public List<TimeZone> TimeZones { get; set; }
    }
}
