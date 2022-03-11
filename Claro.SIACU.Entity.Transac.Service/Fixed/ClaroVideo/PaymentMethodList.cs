using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo
{
    [DataContract(Name = "paymentMethodList")]
    public class PaymentMethodList
    {
        [DataMember(Name = "paymentMethod")]
       public  List<PaymentMethod> paymentMethod { get; set; }
    }
}
