using System.Runtime.Serialization;


namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetComServiceActivation
{
    [DataContract]
    public class ComServiceActivationResponse
    {

        [DataMember]
        public string StrResult { get; set; }
        [DataMember]
        public string StrMessage { get; set; }
        [DataMember]
        public bool BlValues { get; set; }
    }
}
