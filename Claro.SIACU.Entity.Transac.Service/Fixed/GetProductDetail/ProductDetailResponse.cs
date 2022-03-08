using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetProductDetail
{
    [DataContract]
    public class ProductDetailResponse
    {
        [DataMember]
        public List<Decoder> listDecoder { get; set; }
        [DataMember]
        public int rResultado { get; set; }
        [DataMember]
        public string rMensaje { get; set; }
    }
}
