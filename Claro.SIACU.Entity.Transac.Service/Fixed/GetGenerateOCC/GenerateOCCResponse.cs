using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetGenerateOCC
{
    [DataContract]
    public class GenerateOCCResponse
    {
        [DataMember]
        public string txId { get; set; }

        [DataMember]
        public string errorCode { get; set; }

        [DataMember]
        public string errorMsg { get; set; }

        [DataMember]
        public decimal registraOcc { get; set; }

        [DataMember]
        public bool registraOccSpecified { get; set; }
    }
}
