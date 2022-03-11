using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Transac.Service.Common.GetBusinessInteraction2
{
    [DataContract(Name = "BusinessInteraction2RequestCommon")]
    public class BusinessInteraction2Request: Claro.Entity.Request
    {

        [DataMember]
        public Iteraction Item { get; set; }

        

    }
}
