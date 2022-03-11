using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common
{
    [DataContract(Name = "auditoria")]
    public class Auditoria
    {
        [DataMember]
        public string codigo { get; set; }

        [DataMember]
        public string perfil { get; set; }
    }
}
