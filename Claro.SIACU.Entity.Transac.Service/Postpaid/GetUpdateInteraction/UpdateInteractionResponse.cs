using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetUpdateInteraction
{
    [DataContract(Name = "UpdateInteractionResponse")]
    public class UpdateInteractionResponse
    {
        [DataMember]
        public bool ProcessSuccess { get; set; }
        [DataMember]
        public string FlagInsertion { get; set; }
        [DataMember]
        public string MessageText { get; set; }
    }
}
