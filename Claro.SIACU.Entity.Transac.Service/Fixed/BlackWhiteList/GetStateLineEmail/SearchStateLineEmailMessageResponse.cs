using System.Runtime.Serialization;
using System.Collections.Generic;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.BlackWhiteList.GetStateLineEmail
{
   public class SearchStateLineEmailMessageResponse
    {
        [DataMember(Name = "Header")]
       public SearchStateLineEmailHeaderResponse Header { get; set; }
        [DataMember(Name = "Body")]
        public SearchStateLineEmailBodyResponse Body { get; set; }

    }
}
