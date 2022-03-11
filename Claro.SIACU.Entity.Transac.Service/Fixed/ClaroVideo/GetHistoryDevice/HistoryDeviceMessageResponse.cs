using System.Collections.Generic;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo.GetHistoryDevice
{
    public class HistoryDeviceMessageResponse
    {
        [DataMember(Name = "Header")]
        public HistoryDeviceHeaderResponse Header { get; set; }
        [DataMember(Name = "Body")]
        public HistoryDeviceBodyResponse Body { get; set; }
    }
}
