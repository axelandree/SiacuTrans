using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetDecoType
{
    [DataContract]
    public class DecoTypeRequest : Claro.Entity.Request
    {
        [DataMember]
        public string strTipoEquipo { get; set; }
    }
}
