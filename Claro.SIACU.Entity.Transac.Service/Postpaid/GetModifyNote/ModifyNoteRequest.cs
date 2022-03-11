using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetModifyNote
{
    [DataContract(Name = "ModifyNoteRequest")]
    public class ModifyNoteRequest : Claro.Entity.Request
    {
        [DataMember]
        public string StrInteractionId { get; set; }
        [DataMember]
        public string StrText { get; set; }
    }
}
