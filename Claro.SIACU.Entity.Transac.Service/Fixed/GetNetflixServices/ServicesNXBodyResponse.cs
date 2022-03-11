using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetNetflixServices
{
    public class ServicesNXBodyResponse
    {
        [DataMember(Name = "responseAudit")]
        public responseAudit responseAudit { get; set; }

        [DataMember(Name = "listaOpcional")]
        public List<listaOpcional> listaOpcional { get; set; }
    }
}
