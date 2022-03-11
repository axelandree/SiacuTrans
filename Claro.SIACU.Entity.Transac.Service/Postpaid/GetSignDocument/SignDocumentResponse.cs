using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetSignDocument
{
   
    public class SignDocumentResponse
    {
        public SignDocumentResponseStatus ObjSignDocumentResponseStatus { get; set; }
        public SignDocumentResponseData ObjSignDocumentResponseData { get; set; }
    }
}
