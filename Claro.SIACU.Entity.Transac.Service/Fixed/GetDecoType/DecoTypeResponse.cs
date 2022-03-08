using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetDecoType
{
    [DataContract]
    public class DecoTypeResponse
    {
        [DataMember]
        public string TipoDeco { get; set; }

        [DataMember]
        public int CodResp { get; set; }

        [DataMember]
        public string Mensaje { get; set; }
        
    }
}
