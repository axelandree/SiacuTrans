using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo.GetValidateIPTV
{
     [DataContract(Name = "ValidateIPTVResponse")]
    public class ValidateIPTVResponse
    {
         [DataMember]
         public List<Entity.Transac.Service.Fixed.ClaroVideo.ValidateIPTV> lstValidateIPTV { get; set; }
    }
}
