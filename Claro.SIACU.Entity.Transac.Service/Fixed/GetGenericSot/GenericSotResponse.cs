using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetGenericSot
{
    [DataContract]
    public class GenericSotResponse
    {
        [DataMember] 
        public string rCodSot { get; set;}
        [DataMember]
        public string rCodRes { get; set;}
        [DataMember]
        public string rDescRes { get; set; }
    }
}
