using System;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.BlackWhiteList.GetUpdateStateLineEmail
{
    public class UpdateStateLineEmailMessageResponse
    {
        [DataMember(Name = "Header")]
        public UpdateStateLineEmailHeaderResponse Header { get; set; }
        [DataMember(Name = "Body")]
        public UpdateStateLineEmailBodyResponse Body { get; set; }
    }
}
