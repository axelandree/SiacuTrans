using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.RegisterBonoDiscount
{
    [DataContract]
    public class RegisterBonoDiscountResponse
    {
        [DataMember]
        public int po_cod_err { get; set; }
        [DataMember]
        public string po_des_err { get; set; }
        [DataMember]
        public bool rResult { get; set; }
    }
}
