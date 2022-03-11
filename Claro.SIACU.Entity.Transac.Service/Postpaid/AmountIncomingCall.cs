using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid
{
    [DataContract(Name = "AmountIncomingCall")]
    public class AmountIncomingCall
    {
        [DataMember]
        [Data.DbColumn("DESCRIPCION")]
        public string Description { get; set; }
        [DataMember]
        [Data.DbColumn("VALOR_C")]
        public string ValorC { get; set; }
        [DataMember]
        [Data.DbColumn("VALOR_N")]
        public double ValorN { get; set; }
        [DataMember]
        [Data.DbColumn("VALOR_L")]
        public string ValorL { get; set; }
    }
}
