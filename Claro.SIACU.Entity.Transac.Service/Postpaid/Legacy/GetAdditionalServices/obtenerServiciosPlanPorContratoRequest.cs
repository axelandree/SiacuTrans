using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.Legacy.GetAdditionalServices
{
    [DataContract]
    public class obtenerServiciosPlanPorContratoRequest
    {
        [DataMember(Name = "contractIdPub")]
        public string contractIdPub { get; set; }
        [DataMember(Name = "flagConsulta")]
        public string flagConsulta { get; set; }
        [DataMember(Name = "validaExcluyente")]
        public string validaExcluyente { get; set; }
        [DataMember(Name = "listaOpcional")]
        public List<listaOpcional> listaOpcional { get; set; }
    }
}
