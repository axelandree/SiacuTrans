using System.Runtime.Serialization;
using System.Collections.Generic;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetServByTransCodeProduct
{
    [DataContract(Name = "ServByTransCodeProductResponse")]
    public class ServByTransCodeProductResponse
    {
        
        [DataMember]
        public double TotalCargoFijo { get; set; }
        [DataMember]
        public double CargoFijoPorPlan { get; set; }
        [DataMember]
        public int NroRegistro { get; set; }
        [DataMember]
        public List<TopConsumption> lstTopConsumption { get; set; }
    }
}
