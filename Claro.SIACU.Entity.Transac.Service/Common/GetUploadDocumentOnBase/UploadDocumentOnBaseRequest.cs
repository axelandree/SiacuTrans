using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetUploadDocumentOnBase
{
    [DataContract]
    public class UploadDocumentOnBaseRequest : Claro.Entity.Request
    {
        [DataMember]
        public string idTransaccion { get; set; }
        [DataMember]
        public string ipAplicacion { get; set; }
        [DataMember]
        public string nombreAplicacion { get; set; }
        [DataMember]
        public string usuarioAplicacion { get; set; }
        [DataMember]
        public string idInterfazTCRM { get; set; }
        [DataMember]
        public string usuario { get; set; }
        [DataMember]
        public string fecha { get; set; }
        [DataMember]
        public string hora { get; set; }
        [DataMember]
        public E_DocumentRequest listaDocumentos { get; set; }
        [DataMember]
        public ParametersRequest parametrosRequest { get; set; }
    }
}
