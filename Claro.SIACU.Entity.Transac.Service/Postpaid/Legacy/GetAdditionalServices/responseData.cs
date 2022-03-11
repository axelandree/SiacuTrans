using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.Legacy.GetAdditionalServices
{
    [DataContract]
    public class responseData
    {
        [DataMember(Name = "serviciosAsociados")]
        public List<serviciosAsociados> serviciosAsociados { get; set; }
        [DataMember(Name = "listaOpcional")]
        public List<listaOpcional> listaOpcional { get; set; }
    }
}
