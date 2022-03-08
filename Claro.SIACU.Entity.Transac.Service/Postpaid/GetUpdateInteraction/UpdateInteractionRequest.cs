using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetUpdateInteraction
{
    [DataContract(Name = "UpdateInteractionRequest")]
    public class UpdateInteractionRequest : Claro.Entity.Request
    {
        [DataMember]
        public string InteractId { get; set; }
        [DataMember]
        public string Text { get; set; }
        [DataMember]
        public string Order { get; set; }
    }
}
