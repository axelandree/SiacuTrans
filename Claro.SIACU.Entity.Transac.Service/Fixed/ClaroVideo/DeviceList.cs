using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo
{
    [DataContract(Name = "deviceList")]
    public class DeviceList
    {
         [DataMember(Name = "device")]
        public List<Device> device { get; set; }
    }
}
