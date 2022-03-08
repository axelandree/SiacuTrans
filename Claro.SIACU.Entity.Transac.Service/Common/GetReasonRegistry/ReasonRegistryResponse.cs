﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetReasonRegistry
{
    [DataContract(Name = "ReasonRegistryResponsePrepaid")]
    public class ReasonRegistryResponse
    {
        [DataMember]
        public List<ListItem> ListReasonRegistry { get; set; }
    }
}
