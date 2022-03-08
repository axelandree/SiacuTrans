using System.Runtime.Serialization;
using System.Collections.Generic;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetTimeZones
{
    [DataContract(Name = "TimeZonesResponseHfc")]
    public class FranjasHorariasResponse
    {
        [DataMember]
        public List<TimeZone> TimeZones { get; set; }
    }
}
