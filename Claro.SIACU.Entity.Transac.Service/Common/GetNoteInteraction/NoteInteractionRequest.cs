using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetNoteInteraction
{
    [DataContract]
    public class NoteInteractionRequest : Claro.Entity.Request
    {
        [DataMember]
        public string vInteraccionId { get; set; }
    }
}
