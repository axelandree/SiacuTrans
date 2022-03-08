using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetConsultationServiceByContract
{
    [DataContract]
    public class ConsultationServiceByContractRequest : Claro.Entity.Request
    {
        [DataMember]
        public string strCodContrato { get; set; }

        [DataMember]
        public string typeProduct { get; set; }
    }
}
