using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo.GetValidateIPTV
{
        [DataContract(Name = "ValidateIPTVRequest")]
    public class ValidateIPTVRequest : Claro.Entity.Request
    {
        [DataMember]
        public string strCodNum { get; set; }
        [DataMember]
        public string strOpc { get; set; }
    }
}
