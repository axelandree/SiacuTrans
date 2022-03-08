using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Claro.SIACU.Entity.Transac.Service.Fixed
{
    [DataContract(Name = "BEETAListaParamRequestOpcionalCapacityHfc")]
    public class BEETAListaParamRequestOpcionalCapacity
    {
        [DataMember]
        public List<BEETAParamRequestCapacity> ParamRequestCapacities { get; set; }
        public BEETAListaParamRequestOpcionalCapacity()
        {
          ParamRequestCapacities = new List<BEETAParamRequestCapacity>();
        }
    }
}
