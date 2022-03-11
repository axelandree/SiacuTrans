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
    public class registrarProcesoContinueResponse
    {
        [DataMember]
        public responseAudit responseAudit { get; set; }

        public registrarProcesoContinueResponse()
        {
            responseAudit = new responseAudit();
        }
    }
}
