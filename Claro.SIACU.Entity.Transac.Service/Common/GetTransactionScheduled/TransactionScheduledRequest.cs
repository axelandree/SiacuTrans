using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetTransactionScheduled
{
    [DataContract]
    public class TransactionScheduledRequest : Claro.Entity.Request
    {
        [DataMember]
        public string vstrCoId { get; set; }
        [DataMember]
        public string vstrCuenta { get; set; }
        [DataMember]
        public string vstrFDesde { get; set; }
        [DataMember]
        public string vstrFHasta { get; set; }
        [DataMember]
        public string vstrEstado { get; set; }
        [DataMember]
        public string vstrAsesor { get; set; }
        [DataMember]
        public string vstrTipoTran { get; set; }
        [DataMember]
        public string vstrCodInter { get; set; }
        [DataMember]
        public string vstrCacDac { get; set; }
        [DataMember]
        public string vstrIdParametro { get; set; }
    }
}
