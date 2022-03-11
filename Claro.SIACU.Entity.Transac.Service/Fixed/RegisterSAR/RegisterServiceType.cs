using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.RegisterSAR
{
    [DataContract]
    public class RegisterServiceType
    {
        [DataMember]
        public string piNroServicio { get; set; }
        [DataMember]
        public string piCoId { get; set; }
        [DataMember]
        public string piCuenta { get; set; }
        [DataMember]
        public string piCustomerId { get; set; }
        [DataMember]
        public string piFlagAsociacion { get; set; }
        [DataMember]
        public string piCantidadServicios { get; set; }
    }
}
