using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetDetailTransExtra
{
    [DataContract]
    public class DetailTransExtraResponse
    {
        [DataMember]
        public int iResultado { get; set; }
        [DataMember]
        public string vMensaje { get; set; }
    }
}
