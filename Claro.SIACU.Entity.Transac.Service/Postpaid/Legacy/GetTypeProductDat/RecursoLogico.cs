using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.Legacy.GetTypeProductDat
{
    public class RecursoLogico
    {
        [DataMember(Name = "numeroLinea")]
        public string numeroLinea { get; set; }
    }
}
