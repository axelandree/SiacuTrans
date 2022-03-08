using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Prepaid.GetPlanesTFI
{
    [DataContract]
    public class obtenerTabConsultaPlanTFIPostResponse
    {
        [DataMember(Name = "ResponseStatus")]
        public Common.GetDataPower.ResponseStatus ResponseStatus { get; set; }
        [DataMember(Name = "responseDataObtenerTabConsultaPlanTFI")]
        public responseDataObtenerTabConsultaPlanTFIPost responseDataObtenerTabConsultaPlanTFIPost { get; set; }
    }
}
