using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetUploadDocumentOnBase
{
    [DataContract]
    public class ListOptionalRequest
    {
        [DataMember]
        public List<ObjetOptionalRequest> objetoRequestOpcional { get; set; }
    }
}
