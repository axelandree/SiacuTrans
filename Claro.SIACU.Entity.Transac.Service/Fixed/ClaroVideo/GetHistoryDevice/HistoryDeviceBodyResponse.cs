using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo.GetHistoryDevice
{
    [DataContract(Name="HistoryDeviceBodyResponse")]
    public class HistoryDeviceBodyResponse
    {
        [DataMember(Name = "historialServDispCVResponse")]
        public HistorialServDispCVResponse historialServDispCVRequest { get; set; }
    }
}
