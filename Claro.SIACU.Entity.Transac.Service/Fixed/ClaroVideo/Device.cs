using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo
{
    [DataContract(Name = "device")]
    public class Device
    {
        [DataMember(Name = "item")]
        public List<DeviceItem> item { get; set; }
    }
}
