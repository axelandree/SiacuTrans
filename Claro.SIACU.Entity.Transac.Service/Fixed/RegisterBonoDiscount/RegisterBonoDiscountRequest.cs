using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Transac.Service.Fixed.RegisterBonoDiscount
{
    [DataContract]
    public class RegisterBonoDiscountRequest : Claro.Entity.Request
    {
        [DataMember]
        public int pi_co_id { get; set; }
        [DataMember]
        public int pi_id_campana { get; set; }
        [DataMember]
        public double pi_porcentaje { get; set; }
        [DataMember]
        public double pi_monto { get; set; }
        [DataMember]
        public int pi_periodo { get; set; }
        [DataMember]
        public int pi_sncode { get; set; }
        [DataMember]
        public double pi_costo_inst { get; set; }
        [DataMember]
        public int pi_flag { get; set; }
    }
}
