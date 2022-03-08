using System.Runtime.Serialization;
using System.Collections.Generic;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.SendNewPlanServices
{
    [DataContract(Name = "NewPlanServicesResponse")]
    public class NewPlanServicesResponse
    {
        [DataMember]
        public bool result { get; set; }
    }
}
