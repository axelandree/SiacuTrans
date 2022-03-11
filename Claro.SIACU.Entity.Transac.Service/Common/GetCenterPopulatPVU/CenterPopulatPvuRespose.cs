using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetCenterPopulatPVU
{
 
    [DataContract(Name = "CenterPopulatPvuResponseCommon")]
    public class CenterPopulatPvuRespose
    {

            [DataMember]
            public string idUbigeo { get; set; }

    }
}
