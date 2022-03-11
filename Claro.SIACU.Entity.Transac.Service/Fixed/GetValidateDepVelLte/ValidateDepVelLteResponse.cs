using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetValidateDepVelLte
{
    [DataContract]
    public class ValidateDepVelLteResponse
    {
        [DataMember]
        public int intResult { get; set; }
    }
}
