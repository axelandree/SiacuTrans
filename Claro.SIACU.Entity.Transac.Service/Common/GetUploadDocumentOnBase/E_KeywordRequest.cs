using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetUploadDocumentOnBase
{
    [DataContract]
    public class E_KeywordRequest
    {
        [DataMember]
        public string codigoCampo { get; set; }
        [DataMember]
        public string Campo { get; set; }
        [DataMember]
        public string Valor { get; set; }
        [DataMember]
        public string longitud { get; set; }
    }
}
