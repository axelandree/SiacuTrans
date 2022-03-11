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
    [Serializable]
    public class registrarProcesoContinueRequest
    {
        [DataMember]
        public string plataforma { get; set; }

        [DataMember]
        public string accion { get; set; }

        [DataMember]
        public string msisdn { get; set; }

        [DataMember]
        public string motivo { get; set; }

        [DataMember]
        public string imsi { get; set; }

        [DataMember]
        public string ip { get; set; }

        [DataMember]
        public string codigoILink { get; set; }
    }
}
