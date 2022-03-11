using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetInsertLogTrx
{
    [DataContract(Name = "InsertLogTrxRequestCommon")]
    public class InsertLogTrxRequest : Claro.Entity.Request
    {
        [DataMember]
        public string Aplicacion{ get; set; }
        [DataMember]
        public string Transaccion{ get; set; }
        [DataMember]
        public string Opcion{ get; set; }
        [DataMember]
        public string Accion{ get; set; }
        [DataMember]
        public string Phone{ get; set; }
        [DataMember]
        public string IdInteraction{ get; set; }
        [DataMember]
        public string IdTypification{ get; set; }
        [DataMember]
        public string User{ get; set; }
        [DataMember]
        public string IPPCClient{ get; set; }
        [DataMember]
        public string PCClient{ get; set; }
        [DataMember]
        public string IPServer{ get; set; }
        [DataMember]
        public string NameServer{ get; set; }
        [DataMember]
        public string InputParameters{ get; set; }
        [DataMember]
        public string OutpuParameters{ get; set; }  
    }
}
