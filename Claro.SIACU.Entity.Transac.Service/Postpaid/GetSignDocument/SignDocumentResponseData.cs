using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetSignDocument
{
    public class SignDocumentResponseData
    {
        public SignDocumentDatosFirmarDocumentoResponse ObjSignDocumentDatosFirmarDocumentoResponse { get; set; }
        public List<SignDocumenOptionalList> LstSignDocumenOptionalList { get; set; }
    }
}
