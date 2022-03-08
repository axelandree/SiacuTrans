using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Prepaid.GetPlanesTFI
{
    [DataContract(Name = "PlanesTFIRequest")]
    public class PlanesTFIRequest : Claro.Entity.Request
    {
        [DataMember(Name = "Header")]
        public Common.GetDataPower.HeaderRequest Header { get; set; }
        [DataMember(Name = "obtenerTabCambioPlanTFI")]
        public obtenerTabCambioPlanTFIPostRequest obtenerTabCambioPlanTFIPostRequest { get; set; }
    }
}
