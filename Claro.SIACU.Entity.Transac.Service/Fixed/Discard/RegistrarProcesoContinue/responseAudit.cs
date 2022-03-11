using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.Discard.ProcesarContinue
{
    //INICIATIVA-986
    [DataContract]
    public class responseAudit
    {
        [DataMember]
        public string idTransaccion { get; set; }

        [DataMember]
        public string codigoRespuesta { get; set; }

        [DataMember]
        public string mensajeRespuesta { get; set; }
    }
}
