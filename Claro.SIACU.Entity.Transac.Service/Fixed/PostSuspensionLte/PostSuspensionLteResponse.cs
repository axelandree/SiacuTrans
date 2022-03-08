using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.PostSuspensionLte
{
    [DataContract]
    public class PostSuspensionLteResponse
    {
        [DataMember]
        public bool ResponseStatus { get; set; }
    }
}
