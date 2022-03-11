using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo.GetConsultIPTV
{
     [DataContract(Name = "ConsultIPTVResponse")]
    public class ConsultIPTVResponse
    {
         [DataMember]
         public List<Entity.Transac.Service.Fixed.ClaroVideo.ConsultIPTV> lstConsultIPTV { get; set; }
    }
}
