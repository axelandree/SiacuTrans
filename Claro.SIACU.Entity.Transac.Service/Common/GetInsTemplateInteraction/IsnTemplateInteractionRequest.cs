using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetInsTemplateInteraction
{
    [DataContract(Name = "InsTemplateInteractionRequestCommon")]
    public class InsTemplateInteractionRequest:Claro.Entity.Request
    {
        [DataMember]
        public InsertTemplateInteraction item { get; set; }
        [DataMember]
        public string IdInteraction { get; set; }
    }
}
