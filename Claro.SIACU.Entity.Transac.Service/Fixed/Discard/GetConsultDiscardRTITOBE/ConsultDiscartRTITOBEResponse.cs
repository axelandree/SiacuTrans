using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.Discard.GetConsultDiscardRTITOBE
{
    [DataContract]
    public class ConsultDiscartRTITOBEResponse
    {
        [DataMember]
        public ConsultarDescartesRtiResponse consultarDescartesRtiResponse { get; set; }
    }
}
