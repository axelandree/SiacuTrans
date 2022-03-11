using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.PostSuspensionLte
{
    [DataContract]
    public class PostSuspensionLteRequest : Claro.Entity.Request
    {
        [DataMember]
        public SuspensionLte Suspension { get; set; }
    }
}
