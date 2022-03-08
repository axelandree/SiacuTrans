using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Prepaid.GetTipifCallOutPrep
{
    [DataContract(Name = "TipifCallOutPrepRequest")]
    public class TipifCallOutPrepRequest : Claro.Entity.Request
    {
        [DataMember]
        public string vTransaccion { get; set; }

    }
}
