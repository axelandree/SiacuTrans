using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetUploadDocumentOnBase
{
    [DataContract]
    public class E_DocumentRequest
    {
        [DataMember]
        public string Seq { get; set; }
        [DataMember]
        public string CodigoTCRM { get; set; }
        [DataMember]
        public string TipoDocumentoOnBase { get; set; }
        [DataMember]
        public List<E_KeywordRequest> ListaMetadatos { get; set; }
        [DataMember]
        public string abytArchivo { get; set; }
        [DataMember]
        public string CodigoOnBase { get; set; }
        [DataMember]
        public string strExtension { get; set; }
        [DataMember]
        public List<E_KeywordRequest> ListOnBase { get; set; }

    }
}
