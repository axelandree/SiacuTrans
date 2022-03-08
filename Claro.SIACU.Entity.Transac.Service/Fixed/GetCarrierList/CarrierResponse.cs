using System.Runtime.Serialization;
using System.Collections.Generic;
namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetCarrierList
{
    [DataContract(Name="CarrierResponseHfc")]
    public class CarrierResponse
    {
        [DataMember]
        public List<Carrier> carriers { get; set; }
    }
}
