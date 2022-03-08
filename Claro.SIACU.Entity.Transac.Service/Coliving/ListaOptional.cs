using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Coliving
{
    [DataContract]
    public class ListOptional
    {
        [DataMember(Name = "clave")]
        public string clave { get; set; }
        [DataMember(Name = "valor")]
        public string valor { get; set; }
    }
}