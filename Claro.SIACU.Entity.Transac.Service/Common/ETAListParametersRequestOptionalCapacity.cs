using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common
{
    [DataContract(Name = "ETAListParametersRequestOptionalCapacity")]
    public class ETAListParametersRequestOptionalCapacity
    {
        [DataMember]
        public List<ETAParametersRequestCapacity> ParamRequestCapacities { get; set; }
        public ETAListParametersRequestOptionalCapacity()
        {
            ParamRequestCapacities = new List<ETAParametersRequestCapacity>();
        }
    }
}
