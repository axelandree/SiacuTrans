using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed
{
    public class Service
    {
        [DataMember]
        public string NRO_CELULAR { get; set; }

        [DataMember]
        public string ESTADO_LINEA { get; set; }
    }
}
