using System.Collections.Generic;
using System.Runtime.Serialization;
using EntitiesFixed = Claro.SIACU.Entity.Transac.Service.Fixed;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetCommercialServices
{
    [DataContract]
    public class CommercialServicesResponse
    {
        [DataMember]
        public List<CommercialService> LstCommercialServices { get; set; }
    }
}
