﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.Legacy.GetAdditionalServices
{
    [DataContract]
    public class listaOpcional
    {
        [DataMember(Name = "valor")]
        public string valor { get; set; }
        [DataMember(Name = "campo")]
        public string campo { get; set; }
    }
}