using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetConsultService
{
    [DataContract(Name = "ConsultServiceResponse")]
    public class ConsultServiceResponse
    {
        [DataMember]
        public int SnCode { get; set; }
        [DataMember]
        public int SpCode { get; set; }
        [DataMember]
        public string ErrorMessage { get; set; }
        [DataMember]
        public string ErrorNum { get; set; }
        [DataMember]
        public string Serv { get; set; }
        [DataMember]
        public string Result { get; set; }
    }
}
