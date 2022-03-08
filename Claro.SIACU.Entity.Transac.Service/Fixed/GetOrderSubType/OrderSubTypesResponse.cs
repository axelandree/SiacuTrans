using System.Runtime.Serialization;
using System.Collections.Generic;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetOrderSubType
{
    [DataContract(Name = "OrderSubTypesResponseHfc")]
    public class OrderSubTypesResponse
    {
        [DataMember]
        public List<OrderSubType> OrderSubTypes { get; set; }
    }
}
