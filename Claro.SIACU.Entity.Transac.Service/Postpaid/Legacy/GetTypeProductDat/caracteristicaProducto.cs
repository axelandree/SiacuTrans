using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.Legacy.GetTypeProductDat
{
    [DataContract]
    public class CaracteristicaProducto
    {
        [DataMember(Name = "tecnologia")]
        public string tecnologia { get; set; }
        [DataMember(Name = "tipoServicio")]
        public string tipoServicio { get; set; }
    }
}
