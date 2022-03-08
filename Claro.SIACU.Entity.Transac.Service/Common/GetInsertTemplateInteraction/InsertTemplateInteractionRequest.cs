using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Transac.Service.Common.GetInsertTemplateInteraction
{

    [DataContract(Name = "InsertTemplateInteractionRequestCommon")]
    public class InsertTemplateInteractionRequest:Claro.Entity.Request
    {
        [DataMember]
        public InsertTemplateInteraction item { get; set; }
        [DataMember]
        public string IdInteraction { get; set; }
    }
}
