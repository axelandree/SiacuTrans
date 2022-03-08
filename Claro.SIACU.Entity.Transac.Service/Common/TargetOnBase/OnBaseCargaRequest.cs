using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.TargetOnBase
{
    [DataContract(Name = "OnBaseCargaRequest")]
    public class OnBaseCargaRequest : Tools.Entity.Request
    {

        [DataMember(Name = "SpecificationAttachmentOnBase")]
        public SpecificationAttachmentOnBase SpecificationAttachmentOnBase { get; set; }

        [DataMember(Name = "metadatosOnBase")]
        public List<metadatosOnBase> metadatosOnBase { get; set; }

        [DataMember(Name = "user")]
        public string user { get; set; }
    }
}
