using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.RegisterInteractionAdjust
{
    [DataContract]
    public class DetalleDocs
    {
        [DataMember]
        public string piTipoDocCxc {get;set;}
        [DataMember]
        public string piNroDocumentoCxc { get; set; }
    }
}
