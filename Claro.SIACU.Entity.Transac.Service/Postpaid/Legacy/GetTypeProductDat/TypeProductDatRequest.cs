using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.Legacy.GetTypeProductDat
{
    public class TypeProductDatRequest : Claro.Entity.Request 
    {
        [DataMember(Name = "contrato")]
        public ContratoRequest contrato { get; set; }
    }
}
