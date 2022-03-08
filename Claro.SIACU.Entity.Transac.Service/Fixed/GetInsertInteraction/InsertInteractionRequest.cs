using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetInsertInteraction
{
    [DataContract]
    public class GetInsertInteractionRequest : Claro.Entity.Request
    {
        [DataMember]
        public Interaction Interaction { get; set; }
    }
}
