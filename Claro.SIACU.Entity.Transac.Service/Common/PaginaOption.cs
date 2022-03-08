using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common
{
    [DataContract(Name = "PaginaOption")]
    public class PaginaOption
    {
        [DataMember]
        public string OptionCode { get; set; }
        [DataMember]
        public string Clave { get; set; }
        [DataMember]
        public string OptionDescription { get; set; }
    }
}
