using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetTypeConfip
{
    [DataContract]
    public class TypeConfigIpRequest: Claro.Entity.Request
    {
        [DataMember]
        public int an_tiptrabajo { get; set; }
    }
}
