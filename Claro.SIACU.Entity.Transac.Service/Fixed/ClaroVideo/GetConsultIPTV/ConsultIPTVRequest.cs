using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo.GetConsultIPTV
{
        [DataContract(Name = "ConsultIPTVRequest")]
    public class ConsultIPTVRequest : Claro.Entity.Request
    {
        [DataMember]
        public string strProducto { get; set; }
   
    }
}
