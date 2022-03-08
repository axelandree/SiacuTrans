using Claro.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Claro.SIACU.Entity.Transac.Service.Fixed
{
    public class OrderType
    {
        [DbColumn("VALOR")]
        [DataMember]
        public string VALOR { get; set; }
        [DbColumn("DESCRIPCION")]
        [DataMember]
        public string DESCRIPCION { get; set; }
    }
}
