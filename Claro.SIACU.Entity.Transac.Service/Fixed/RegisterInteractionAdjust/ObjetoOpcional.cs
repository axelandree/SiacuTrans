﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.RegisterInteractionAdjust
{
    [DataContract]
    public class ObjetoOpcional
    {

        [DataMember]
        public string campo { get; set; }
        [DataMember]
        public string valor { get; set; }

    }
}