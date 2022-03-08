using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Prepaid.GetPlanesTFI
{
    [DataContract]
    public class obtenerTabCambioPlanTFIPostRequest : Claro.Entity.Request
    {
        [DataMember(Name = "suscriptor")]
        public string suscriptor { get; set; }
        [DataMember(Name = "proveedor")]
        public string proveedor { get; set; }
        [DataMember(Name = "tarifa")]
        public string tarifa { get; set; }
    }
}
