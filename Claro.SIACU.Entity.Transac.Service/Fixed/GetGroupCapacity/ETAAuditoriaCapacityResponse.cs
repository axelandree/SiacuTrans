using System.Runtime.Serialization;
using System.Collections.Generic;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetGroupCapacity
{
    [DataContract(Name = "ETAAuditoriaCapacityResponseHfc")]
    public class ETAAuditoriaCapacityResponse
    {
        [DataMember]
        public ETAAuditoriaCapacity listPlan { get; set; }
    }
}
