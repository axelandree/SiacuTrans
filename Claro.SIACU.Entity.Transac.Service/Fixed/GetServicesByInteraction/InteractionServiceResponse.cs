using System.Runtime.Serialization;
using System.Collections.Generic;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetServicesByInteraction
{
    [DataContract(Name="InteractionServiceResponseHfc")]
    public class InteractionServiceResponse
    {
        [DataMember]
        public List<ServiceByInteraction> Services { get; set; }
    }
}
