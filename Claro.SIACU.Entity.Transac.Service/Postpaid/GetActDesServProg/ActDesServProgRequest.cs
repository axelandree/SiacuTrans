using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetActDesServProg
{
    [DataContract(Name = "ActDesServProgRequest")]
    public class ActDesServProgRequest : Claro.Entity.Request
    {
        [DataMember]
        public string StrTransactionId { get; set; }
        [DataMember]
        public string StrIpApplication { get; set; }
        [DataMember]
        public string StrApplication { get; set; }
        [DataMember]
        public string StrMsisDn { get; set; }
        [DataMember]
        public string StrCoId { get; set; }
        [DataMember]
        public string StrCustomerId { get; set; }
        [DataMember]
        public string StrCoSer { get; set; }
        [DataMember]
        public string StrFlagOccApadece { get; set; }
        [DataMember]
        public double DAmountFidApadece { get; set; }
        [DataMember]
        public double DNewCf { get; set; }
        [DataMember]
        public string StrTypeReg { get; set; }
        [DataMember]
        public int ICycleFact { get; set; }
        [DataMember]
        public string StrCodSer { get; set; }
        [DataMember]
        public string StrDesSer { get; set; }
        [DataMember]
        public string StrNumberAccount { get; set; }
        [DataMember]
        public string StrUserApplication { get; set; }
        [DataMember]
        public string StrUserSystem { get; set; }
        [DataMember]
        public string StrDateProg { get; set; }
        [DataMember]
        public string StrIdInteract { get; set; }
        [DataMember]
        public string StrTypeSer { get; set; }

    }
}
