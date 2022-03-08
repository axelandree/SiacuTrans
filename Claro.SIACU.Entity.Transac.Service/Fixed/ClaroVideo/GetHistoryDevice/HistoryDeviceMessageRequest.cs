using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo.GetHistoryDevice
{
   public class HistoryDeviceMessageRequest
    {
        [DataMember(Name = "Header")]
       public HistoryDeviceHeaderRequest Header { get; set; }
        [DataMember(Name = "Body")]
        public HistoryDeviceBodyRequest Body { get; set; }
    }
}
