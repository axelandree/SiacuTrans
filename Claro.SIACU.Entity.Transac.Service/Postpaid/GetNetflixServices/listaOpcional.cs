using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetNetflixServices
{
    [DataContract(Name = "listaOpcional")]
    public class listaOpcional
    {
        [DataMember(Name = "clave")]
        public string clave { get; set; }

        [DataMember(Name = "valor")]
        public string valor { get; set; }
    }
}
