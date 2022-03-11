using System.Runtime.Serialization;
using System.Collections.Generic;
namespace Claro.SIACU.Entity.Transac.Service.Fixed.BlackWhiteList.GetStateLineEmail
{
    public class SearchStateLineEmailHeaderResponse
    {
        [DataMember(Name = "HeaderResponse")]
        public GetDataPower.HeaderResponse HeaderResponse { get; set; }
    }
}
