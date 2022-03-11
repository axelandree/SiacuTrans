using System;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.BlackWhiteList.GetUpdateStateLineEmail
{
   public class UpdateStateLineEmailMessageRequest
    {
        [DataMember(Name = "Header")]
       public UpdateStateLineEmailHeaderRequest Header { get; set; }
        [DataMember(Name = "Body")]
        public UpdateStateLineEmailBodyRequest Body { get; set; }
    }
}
