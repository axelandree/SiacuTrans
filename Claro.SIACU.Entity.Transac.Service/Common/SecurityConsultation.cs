using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common
{
    class SecurityConsultation
    {
        [DataMember]
        public string USUACCOD { get; set; }
        [DataMember]
        public string PERFCCOD { get; set; }
        [DataMember]
        public string USUACCODVENSAP { get; set; }
        [DataMember]
        public string APLICCOD { get; set; }
        [DataMember]
        public string OPCCODPAD { get; set; }
        [DataMember]
        public string OPCICCOD { get; set; }
        [DataMember]
        public string OPCICNIVPAD { get; set; }
        [DataMember]
        public string OPCICNIV { get; set; }
        [DataMember]
        public string OPCICDES { get; set; }
        [DataMember]
        public string OPCICABREV { get; set; }
        [DataMember]
        public string OPCICNOMPAG { get; set; }
        [DataMember]
        public string OPCICNUMORD { get; set; }
        [DataMember]
        public string OPCICD1 { get; set; } 
    }
}
