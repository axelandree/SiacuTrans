﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo.GetDeleteClientSN
{
    [DataContract]
    public class DeleteClientSNRequest: Tools.Entity.Request
    {
        [DataMember(Name = "MessageRequest")]
        public DeleteClientSNMessageRequest MessageRequest { get; set; }
    }
}
