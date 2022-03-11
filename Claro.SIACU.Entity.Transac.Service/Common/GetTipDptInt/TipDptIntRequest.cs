using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetTipDptInt
{
    [DataContract(Name = "TipDptIntRequestCommon")]
    public class TipDptIntRequest : Claro.Entity.Request
    {

        [DataMember]
        public string CodTipDptInt { get; set; }

    }
}
