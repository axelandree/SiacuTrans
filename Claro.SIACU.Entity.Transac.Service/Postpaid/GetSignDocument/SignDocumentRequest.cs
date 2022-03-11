using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetSignDocument
{
    [DataContract]
    public class SignDocumentRequest : Claro.Entity.Request
    {
        [DataMember]
        public SignDocumentHeaderRequest ObjSignDocumentHeaderRequest { get; set; }

        [DataMember]
        public SignDocument objSignDocument { get; set; }

        [DataMember]
        public List<SignDocumenOptionalList> lstSignDocumenOptionalList { get; set; }


    }
}
