﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetUserExistsBSCS
{
    [DataContract(Name = "UserExistsBSCSResponse")]
    public class UserExistsBSCSResponse
    {
        [DataMember]
        public int Result { get; set; }
    }
}
