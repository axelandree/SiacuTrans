using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetAdjustForClaims
{
    [DataContract(Name = "AdjustForClaimsRequest")]
    public class AdjustForClaimsRequest : Claro.Entity.Request
    {   
        [DataMember]
        public Int64 CodClient { get; set; }
        [DataMember]
        public string CodOCC { get; set; }
        [DataMember]
        public string DateVig { get; set; }
        [DataMember]
        public string NumQuota { get; set; }
        [DataMember]
        public double Amount { get; set; }
        [DataMember]
        public string Comment { get; set; }
    }
}
