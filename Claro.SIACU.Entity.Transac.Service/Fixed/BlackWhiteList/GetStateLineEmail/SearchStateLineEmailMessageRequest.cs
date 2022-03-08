using System.Runtime.Serialization;
using System.Collections.Generic;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.BlackWhiteList.GetStateLineEmail
{
    public class SearchStateLineEmailMessageRequest
    {
        [DataMember(Name = "Header")]
        public SearchStateLineEmailHeaderRequest Header { get; set; }
        [DataMember(Name = "Body")]
        public SearchStateLineEmailBodyRequest Body { get; set; }
    }
}
