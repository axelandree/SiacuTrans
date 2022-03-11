using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetValidateDepVelLte
{
    [DataContract]
    public class ValidateDepVelLteRequest : Claro.Entity.Request
    {
        [DataMember]
        public int intContract { get; set; }
    }
}
