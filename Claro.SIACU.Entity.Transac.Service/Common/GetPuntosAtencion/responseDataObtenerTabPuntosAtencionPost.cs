using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetPuntosAtencion
{
    [DataContract]
    public  class responseDataObtenerTabPuntosAtencionPost
    {
        [DataMember]
        public List<tabPuntosAtencionPost> listaTabPuntosAtencionPost { get; set; }

    }
}
