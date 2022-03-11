﻿using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo
{
    [DataContract(Name = "VisualizationItem")]
    public class VisualizationItem
    {
        [DataMember(Name = "key")]
        public string key { get; set; }

        [DataMember(Name = "value")]
        public string value { get; set; }

    }
}