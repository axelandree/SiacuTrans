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
    public class MessageRequestRegistrarProcesoContinue
    {
        [DataMember]
        public registrarProcesoContinueRequest registrarProcesoContinueRequest { get; set; }

        public MessageRequestRegistrarProcesoContinue()
        {
            registrarProcesoContinueRequest = new registrarProcesoContinueRequest();
        }
    }
}
