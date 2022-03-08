using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetRegistarInstaDecoAdiHFC
{
    [DataContract]
    public class RegistarInstaDecoAdiHFCResponse
    {
        [DataMember]
        public string ResponseCode { get; set; }
        [DataMember]
        public string ResponseMessage { get; set; }
        [DataMember]
        public string CodSolot { get; set; }
    }
}
