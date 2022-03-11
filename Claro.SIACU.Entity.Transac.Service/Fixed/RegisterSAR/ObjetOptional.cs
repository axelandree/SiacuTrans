using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.RegisterSAR
{
    [DataContract]
    public class ObjetOptional
    {
        [DataMember]
        public string campo { get; set; }
        [DataMember]
        public string valor { get; set; }

    }
}
