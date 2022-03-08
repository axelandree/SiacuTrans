using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetConsultServiceBono
{
    [DataContract]
    public class GetConsultServiceBonoPeriodosBonoDisp
    {
        [DataMember(Name = "idPeriodo")]
        public string IdPeriod { get; set; }
        [DataMember(Name = "valorPeriodo")]
        public string ValorPeriodo { get; set; }
        [DataMember(Name = "desPeriodo")]
        public string DesPeriodo { get; set; }
        [DataMember(Name = "estadoPeriodo")]
        public string EstadoPeriodo { get; set; }
    }
}
