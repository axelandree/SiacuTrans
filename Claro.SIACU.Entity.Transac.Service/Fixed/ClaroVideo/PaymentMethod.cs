using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo
{
    [DataContract(Name = "paymentMethod")]
    public class PaymentMethod
    {
        [DataMember(Name = "id")]
        public string id { get; set; }

        [DataMember(Name = "description")]
        public string description { get; set; }
    }
}
