﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetContractByPhoneNumber
{
    [DataContract(Name = "ContractByPhoneNumberRequestCommon")]
    public class ContractByPhoneNumberRequest : Claro.Entity.Request
    {
        [DataMember]
        public string User { get; set; }
        [DataMember]
        public string System { get; set; }
        [DataMember]
        public string PhoneNumber { get; set; }
    }
}