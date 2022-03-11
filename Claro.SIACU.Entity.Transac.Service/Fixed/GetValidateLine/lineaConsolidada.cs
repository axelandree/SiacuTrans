using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetValidateLine
{

    [DataContract(Name = "lineaConsolidada")]
    public class lineaConsolidada
    {
        [DataMember(Name = "msisdn")]
        public string msisdn { get; set; }

        [DataMember(Name = "segmento")]
        public string segmento { get; set; }


    }
}
