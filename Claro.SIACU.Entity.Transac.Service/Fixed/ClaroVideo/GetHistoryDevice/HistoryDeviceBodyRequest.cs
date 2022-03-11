using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo.GetHistoryDevice
{
    [DataContract(Name = "HistoryDeviceBodyRequest")]
    public class HistoryDeviceBodyRequest
    {
        [DataMember(Name = "historialServDispCVRequest")]
        public HistorialServDispCVRequest historialServDispCVRequest { get; set; }

    }
}
