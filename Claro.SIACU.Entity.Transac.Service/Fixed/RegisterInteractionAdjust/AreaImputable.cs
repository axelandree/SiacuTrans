using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.RegisterInteractionAdjust
{
    [DataContract]
    public class AreaImputable
    {
        [DataMember]
        public string piArimIdCategoria {get; set;}
        [DataMember]
        public string piArimIdArea {get; set;}
        [DataMember]
        public string piArimIdMotivo {get; set;}
        [DataMember]
        public string piArimTotalImputable { get; set; }
    }
}
