using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.Legacy.GetTypeProductDat
{
    [DataContract]
    public class CaracteristicaAdicional
    {
        [DataMember(Name = "descripcion")]
        public string descripcion { get; set; }
        [DataMember(Name = "valor")]
        public string valor { get; set; }
    }
}
