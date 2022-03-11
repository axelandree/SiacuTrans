using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo.GetHistoryDevice
{
    [DataContract]
    public class HistoryDeviceResponse
    {
        [DataMember(Name = "MessageResponse")]
        public HistoryDeviceMessageResponse MessageResponse { get; set; }
    }
}
