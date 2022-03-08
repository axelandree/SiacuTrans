using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.RegisterInteractionAdjust
{
    [DataContract]
    public class AprobarDocumento
    {
        [DataMember]
        public string piIdTipoDoc {get; set;}
        [DataMember]
        public string piUsuAprob { get; set; }
    }
}
