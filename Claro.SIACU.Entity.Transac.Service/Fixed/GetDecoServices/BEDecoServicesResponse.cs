using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using EntitiesFixed = Claro.SIACU.Entity.Transac.Service.Fixed;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetDecoServices
{
    [DataContract(Name = "BEDecoServicesResponseFixed")]
    public class BEDecoServicesResponse
    {
        [DataMember]
        public List<EntitiesFixed.BEDeco>  ListDecoServices {get;set;}
    }
}
