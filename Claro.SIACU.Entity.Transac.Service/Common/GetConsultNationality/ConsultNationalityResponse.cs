﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetConsultNationality
{
    [DataContract(Name = "BrandResponsePrepaid")]
    public class ConsultNationalityResponse
    {
        [DataMember]
        public List<ListItem> ListConsultNationality { get; set; }
    }
}