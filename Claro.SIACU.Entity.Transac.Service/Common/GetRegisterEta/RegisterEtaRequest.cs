using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetRegisterEta
{
    [DataContract]
    public class RegisterEtaRequest : Claro.Entity.Request
    {
        [DataMember]
        public string vnumslc { get; set; }
        [DataMember]
        public string vfecha_venta { get; set; }
        [DataMember]
        public int vcod_zona { get; set; }
        [DataMember]
        public string vcod_plano { get; set; }
        [DataMember]
        public string vtipo_orden { get; set; }
        [DataMember]
        public string vsubtipo_orden { get; set; }
        [DataMember]
        public int vtiempo_trabajo { get; set; }
        [DataMember]
        public int vidreturn { get; set; }
    }
}
