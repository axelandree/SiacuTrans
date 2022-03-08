using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo
{
    [DataContract(Name = "rent")]
    public class Rent
    {
        [DataMember(Name = "item")]
        public List<RentItem> item { get; set; }
    }
}
