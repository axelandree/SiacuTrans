using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetConsultarClaroPuntos
{
//INI - RF-02 Evalenzs
    public class ConsultarClaroPuntosHeaderResponse
    {
        [DataMember(Name = "HeaderResponse")]
        public Claro.SIACU.Entity.Transac.Service.Common.GetDataPower.HeaderResponse HeaderResponse { get; set; }

    }
}
