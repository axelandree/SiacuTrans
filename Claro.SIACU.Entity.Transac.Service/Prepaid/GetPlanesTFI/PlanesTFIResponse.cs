using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Prepaid.GetPlanesTFI
{
    [DataContract(Name = "PlanesTFIResponse")]
    public class PlanesTFIResponse
    {
        [DataMember(Name = "Headers")]
        public Common.GetDataPower.HeadersResponse HeadersResponse { get; set; }
        [DataMember]
        public obtenerTabConsultaPlanTFIPostResponse obtenerTabConsultaPlanTFIPostResponse { get; set; }
    }
}
