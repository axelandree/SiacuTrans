using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetTipDptInt
{

    [DataContract(Name = "TipDptIntResponseCommon")]
    public class TipDptIntResponse
    {
        [DataMember]
        public List<Entity.Transac.Service.Common.ListItem> LisTipDptIntType { get; set; }
    }
}
