using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetUploadDocumentOnBase
{
    [DataContract]
    public class UploadDocumentOnBaseResponse
    {
        [DataMember]
        public string idTransaccion { get; set; }
        [DataMember]
        public string idInterfazTCRM { get; set; }
        [DataMember]
        public string ipServerResponse { get; set; }
        [DataMember]
        public string fecha { get; set; }
        [DataMember]
        public string hora { get; set; }
        [DataMember]
        public E_DocumentResponse E_Document { get; set; }
    }
}
