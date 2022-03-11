using Claro.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed
{
    [DataContract(Name="OperatorsHfc")]
    public class Operator
    {
        [DbColumn("IDCARRIER")]
        [DataMember]
        public string IDCARRIER { get; set; }
        [DbColumn("OPERADOR")]
        [DataMember]
        public string OPERADOR { get; set; }

    }
}
