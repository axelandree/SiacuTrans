using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed
{
    [DataContract]
    public class InboundEtaComandos
    {
        [DataMember]
        public DateTime fechaAsignacion { get; set; }
        [DataMember]
        public string tipoComando { get; set; }
        [DataMember]
        public string idContrata { get; set; }
        [DataMember]
        public string idContrataError { get; set; }
    }
}
