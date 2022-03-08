using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetActivationDesactivation
{
    [DataContract]
    public class ActivationDesactivationResponse
    {
        [DataMember]
        public string StrResult { get; set; }
        [DataMember]
        public string StrMessage { get; set; }
        [DataMember]
        public bool BlValues { get; set; }
    }
}
