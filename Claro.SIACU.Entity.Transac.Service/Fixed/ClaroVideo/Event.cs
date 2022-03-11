using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo
{
    [DataContract(Name = "Event")]
    public class Event
    {
        [DataMember(Name = "item")]
        List<EventItem> item { get; set; }
    }
}
