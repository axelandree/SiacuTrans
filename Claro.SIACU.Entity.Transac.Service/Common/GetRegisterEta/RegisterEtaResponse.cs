using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetRegisterEta
{
    [DataContract]
    public class RegisterEtaResponse : Claro.Entity.Request
    {
        [DataMember]
        public int vidconsulta { get; set; }
        [DataMember]
        public DateTime vdia { get; set; }
        [DataMember]
        public string vfranja { get; set; }
        [DataMember]
        public int vestado { get; set; }
        [DataMember]
        public int vquota { get; set; }
        [DataMember]
        public string vid_bucket { get; set; }
        [DataMember]
        public string vresp { get; set; }
    }
}
