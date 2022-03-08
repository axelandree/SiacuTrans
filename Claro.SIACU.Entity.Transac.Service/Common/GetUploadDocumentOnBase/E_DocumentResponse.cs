using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetUploadDocumentOnBase
{
    [DataContract]
    public class E_DocumentResponse
    {
        [DataMember]
        public string Seq { get; set; }
        [DataMember]
        public string CodigoTCRM { get; set; }
        [DataMember]
        public string Estado { get; set; }
        [DataMember]
        public string Fecha { get; set; }
        [DataMember]
        public string Hora { get; set; }
        [DataMember]
        public string CodigoOnBase { get; set; }
    }
}
