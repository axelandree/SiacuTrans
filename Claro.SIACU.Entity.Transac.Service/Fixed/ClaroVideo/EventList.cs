using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo
{
    [DataContract(Name = "eventList")]
    public class EventList
    {
        [DataMember(Name = "event")]
        public List<Event> Event { get; set; }
    }

}
