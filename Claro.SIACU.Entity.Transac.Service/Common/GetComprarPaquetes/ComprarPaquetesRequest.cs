﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetComprarPaquetes 
{
//INI - RF-04 - RF-05 Evalenzs
    [DataContract(Name = "ComprarPaquetes Request")]
    public class ComprarPaquetesRequest : Claro.Entity.Request
    {
        [DataMember(Name = "MessageRequest")]
        public ComprarPaquetesMessageRequest MessageRequest { get; set; }      
    }
}