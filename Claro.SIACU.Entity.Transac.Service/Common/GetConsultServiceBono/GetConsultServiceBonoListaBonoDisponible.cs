using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetConsultServiceBono
{
    [DataContract]
    public class GetConsultServiceBonoListaBonoDisponible
    {
        [DataMember(Name = "idBonoDisp")]
        public string IdBonoDisp { get; set; }
        [DataMember(Name = "tipoIdBonoDisp")]
        public string TipoIdBonoDisp { get; set; }
        [DataMember(Name = "descBonoDisp")]
        public string DescBonoDisp { get; set; }
        [DataMember(Name = "snCodeBonoDisp")]
        public string SnCodeBonoDisp { get; set; }
        [DataMember(Name = "estadoBonoDisp")]
        public string EstadoBonoDisp { get; set; }
        [DataMember(Name = "tipoBonoDisp")]
        public string TipoBonoDisp { get; set; }
        [DataMember(Name = "tipoDescBonoDisp")]
        public string TipoDescBonoDisp { get; set; }

        [DataMember(Name = "periodosBonoDisp")]
        public List<GetConsultServiceBonoPeriodosBonoDisp> ConsultServiceBonoPeriodosBonoDisp { get; set; }

    }
}
