﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.Legacy.GetAdditionalServices
{
    [DataContract]
    public class AdditionalServiceRequest: Claro.Entity.Request
    {
        [DataMember(Name = "obtenerServiciosPlanPorContratoRequest")]
        public obtenerServiciosPlanPorContratoRequest obtenerServiciosPlanPorContratoRequest { get; set; }
    }
}
