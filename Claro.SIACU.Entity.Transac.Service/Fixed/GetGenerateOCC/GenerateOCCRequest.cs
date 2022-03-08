using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetGenerateOCC
{
    [DataContract]
    public class GenerateOCCRequest : Claro.Entity.Request
    {
        [DataMember]
        public string txId { get; set; }

        [DataMember]
        public string ipApp { get; set; }

        [DataMember]
        public string usrApp { get; set; }

        [DataMember]
        public decimal customerId { get; set; }

        [DataMember]
        public bool customerIdSpecified { get; set; }

        [DataMember]
        public decimal codigoOcc { get; set; }

        [DataMember]
        public bool codigoOccSpecified { get; set; }

        [DataMember]
        public decimal nroCuotas { get; set; }

        [DataMember]
        public bool nroCuotasSpecified { get; set; }

        [DataMember]
        public float montoOcc { get; set; }

        [DataMember]
        public bool montoOccSpecified { get; set; }

        [DataMember]
        public DateTime recDate { get; set; }

        [DataMember]
        public string remark { get; set; }

        [DataMember]
        public bool recDateSpecified { get; set; }
    }
}
