using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetModifyNote
{
    [DataContract(Name = "ModifyNoteResponse")]
    public class ModifyNoteResponse
    {
        [DataMember]
        public string StrErrNum { get; set; }
        [DataMember]
        public string StrErrMsg { get; set; }
    }
}
