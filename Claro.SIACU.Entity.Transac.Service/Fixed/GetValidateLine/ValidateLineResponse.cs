using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetValidateLine
{
    [DataContract(Name = "contarLineasResponse")]
    public class ValidateLineResponse
    {
        [DataMember(Name = "codRespuesta")]
        public string codRespuesta { get; set; }
        [DataMember(Name = "msjRespuesta")]
        public string msjRespuesta { get; set; }

        [DataMember(Name = "cantidadLineasActivas")]
        public string cantidadLineasActivas { get; set; }

        [DataMember(Name = "cantidadLineasActivasPorDia")]
        public string cantidadLineasActivasPorDia { get; set; }

        [DataMember(Name = "listaLineasConsolidadasType")]
        public listaLineasConsolidadasType listaLineasConsolidadasType { get; set; }

  

    }
}
