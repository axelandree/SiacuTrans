using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetServiceDTH
{
    public class ServiceDTHResponse
    {
        [DataMember]
        public List<Entity.Transac.Service.Fixed.BEDeco> ListServicesDTH { get; set; }
    }
}
