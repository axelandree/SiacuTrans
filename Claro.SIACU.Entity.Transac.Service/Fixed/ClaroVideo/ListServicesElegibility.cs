using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo
{
    [DataContract(Name = "ListServicesElegibility")]
    public class ListServicesElegibility 
    {
        [DataMember(Name = "productID")]
        public string productID { get; set; }

        [DataMember(Name = "nombre")]
        public string nombre { get; set; }
    }
}
