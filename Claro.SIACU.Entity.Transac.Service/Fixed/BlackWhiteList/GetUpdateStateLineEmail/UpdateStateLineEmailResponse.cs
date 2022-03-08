using System.Runtime.Serialization;
using System.Collections.Generic;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.BlackWhiteList.GetUpdateStateLineEmail
{
        
   [DataContract]
   public class UpdateStateLineEmailResponse
    {
        [DataMember(Name = "MessageResponse")]
       public UpdateStateLineEmailMessageResponse MessageResponse { get; set; }

    }
}
