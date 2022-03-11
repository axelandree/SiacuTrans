using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetUpdateNotes
{
    [DataContract(Name = "UpdateNotesResponseCommon")]
    public class UpdateNotesResponse
    {
        [DataMember]
        public string Flag { get; set; }

        [DataMember]
        public string Message { get; set; } 
    }
}
