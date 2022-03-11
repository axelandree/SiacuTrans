using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetServicesByInteraction
{
    [DataContract(Name="InteractionServiceRequestHfc")]
    public class InteractionServiceRequest:Claro.Entity.Request
    {
        [DataMember]
        public string idInteraccion { get; set; }

    }
}
