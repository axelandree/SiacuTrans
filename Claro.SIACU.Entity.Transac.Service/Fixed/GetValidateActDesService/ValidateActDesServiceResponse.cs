using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetValidateActDesService
{
    [DataContract]
    public class ValidateActDesServiceResponse
    {
        [DataMember]
        public string StrResult { get; set; }
        [DataMember]
        public string StrMsg { get; set; }
        [DataMember]
        public bool BlValues { get; set; }
    }
}
