using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetServicesLte
{
    [DataContract(Name = "ServicesLteFixedResponse")]
  public   class ServicesLteResponse
    {
        [DataMember]
        public List<Entity.Transac.Service.Fixed.BEDeco> ListServicesLte { get; set; }

    }
}
