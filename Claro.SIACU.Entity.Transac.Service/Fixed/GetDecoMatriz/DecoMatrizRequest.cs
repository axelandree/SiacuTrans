using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetDecoMatriz
{
    [DataContract]
    public class DecoMatrizRequest : Claro.Entity.Request
    {
        [DataMember]
        public string strTipoProducto { get; set; }
    }
}
