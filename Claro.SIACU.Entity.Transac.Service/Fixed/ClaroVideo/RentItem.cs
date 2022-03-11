using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo
{
    [DataContract(Name = "RentItem")]
    public class RentItem
    {
        [DataMember(Name = "key")]
        public string key { get; set; }

        [DataMember(Name = "value")]
        public string value { get; set; }
    }
}
