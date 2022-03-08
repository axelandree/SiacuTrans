using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo.GetHistoryDevice
{
    [DataContract]
    public class HistoryDeviceRequest : Tools.Entity.Request
    {
        [DataMember(Name = "MessageRequest")]
        public HistoryDeviceMessageRequest MessageRequest { get; set; } 
    }
}
