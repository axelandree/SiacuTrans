using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetConsultationServiceByContract
{
    [DataContract]
    public class ConsultationServiceByContractResponse
    {
        [DataMember]
        public string strMsgSalida { get; set; }

        [DataMember]
        public bool bResultado { get; set; }

        [DataMember]
        public string msisdn { get; set; }
    }
}
