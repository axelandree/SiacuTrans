using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetProductDetail
{
    [DataContract]
    public class ProductDetailRequest : Claro.Entity.Request
    {
        [DataMember]
        public string vCustomerId { get; set; }
        [DataMember]
        public string vCoId { get; set; }
        [DataMember]
        public int tipoBusqueda { get; set; }
    }
}
