﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetValidateQuantityCampaign
{
    [DataContract]
    public class GetValidateQuantityCampaignResponse
    {
        [DataMember(Name = "MessageResponse")]
        public GetValidateQuantityCampaignMessageResponse ValidateQuantityCampaignMessageResponse { get; set; }
    }
}