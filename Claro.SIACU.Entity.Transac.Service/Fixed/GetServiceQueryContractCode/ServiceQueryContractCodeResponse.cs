using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetServiceQueryContractCode
{
    [DataContract]
    public class ServiceQueryContractCodeResponse
    {
        [DataMember]
        public string msisdn { get; set; }

        [DataMember]
        public string strMsgSalida { get; set; }

        [DataMember]
        public bool result { get; set; }
    }
}
