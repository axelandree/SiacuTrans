using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo
{
    [DataContract(Name = "rentList")]
    public class RentList
    {
        [DataMember(Name = "rent")]
        public List<Rent> rent { get; set; }
    }
}
