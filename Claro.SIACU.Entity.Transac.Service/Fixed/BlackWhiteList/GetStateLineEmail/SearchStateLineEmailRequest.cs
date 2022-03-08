using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Claro.SIACU.Entity.Transac.Service.Fixed.BlackWhiteList.GetStateLineEmail
{
    [DataContract]
    public class SearchStateLineEmailRequest : Tools.Entity.Request
    {
        [DataMember(Name = "MessageRequest")]
        public SearchStateLineEmailMessageRequest MessageRequest { get; set; }      


    }
}
