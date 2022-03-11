using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetAdjustForClaims
{
    [DataContract(Name = "AdjustForClaimsResponse")]
    public class AdjustForClaimsResponse
    {   
        [DataMember]
        public Double Result { get; set; }
    }
}
