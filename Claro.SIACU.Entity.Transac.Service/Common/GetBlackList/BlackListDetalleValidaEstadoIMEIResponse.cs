using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetBlackList
{
    [DataContract]
    public class BlackListDetalleValidaEstadoIMEIResponse
    {
        [DataMember(Name = "po_estado")]
        public string po_estado { get; set; }
        [DataMember(Name = "po_code_result")]
        public string po_code_result { get; set; }
        [DataMember(Name = "po_message_result")]
        public string po_message_result { get; set; }
    }
}
