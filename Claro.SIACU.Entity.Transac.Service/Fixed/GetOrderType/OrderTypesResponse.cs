using System.Runtime.Serialization;
using System.Collections.Generic;
using Claro.SIACU.Entity.Transac.Service.Fixed;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetOrderType
{
    [DataContract(Name = "OrderTypesResponseHfc")]
    public class OrderTypesResponse
    {
        [DataMember]
        public List<OrderType> ordertypes { get; set; }
    }
}
