using System.Runtime.Serialization;
using System.Collections.Generic;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.BlackWhiteList.GetStateLineEmail
{
    [DataContract]
    public class SearchStateLineEmailResponse
    {
        [DataMember(Name = "MessageResponse")]
        public SearchStateLineEmailMessageResponse MessageResponse { get; set; }
    }
}
