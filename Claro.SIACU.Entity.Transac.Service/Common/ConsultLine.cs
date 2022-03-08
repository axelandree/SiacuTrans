using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common
{
    
    [DataContract]
    public class ConsultLine
    {

        [DataMember]
        public string Msisdn { get; set; }
        [DataMember]
        public string Traces { get; set; }
        [DataMember]
        public string User { get; set; }
        [DataMember]
        public string Application { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string DateTransaction { get; set; }
        [DataMember]
        public string IdTransaction { get; set; }
        [DataMember]
        public string IPApplication { get; set; }
    }
}
