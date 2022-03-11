using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetUpdateNotes
{
    [DataContract(Name = "UpdateNotesRequestCommon")]
    public class UpdateNotesRequest : Claro.Entity.Request
    {
        [DataMember]
        public string StrObjId { get; set; }

        [DataMember]
        public string StrText { get; set; }
        [DataMember]
        public string StrOrder { get; set; } 
    }
}
