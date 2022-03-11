using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetConsultServiceBono
{
    [DataContract]
    public class GetConsultServiceBonoListaBonoActual
    {
        [DataMember(Name = "descBonoActual")]
        public string DescBonoActual { get; set; }
        [DataMember(Name = "fecActBonoActual")]
        public string FecActBonoActual { get; set; }
        [DataMember(Name = "velocidadFinalActual")]
        public string VelocidadFinalActual { get; set; }
        [DataMember(Name = "periodoBonoActual")]
        public string PeriodoBonoActual { get; set; }
        [DataMember(Name = "fecVigenBonoActual")]
        public string FecVigenBonoActual { get; set; }
        [DataMember(Name = "snCodeBonoActual")]
        public string SnCodeBonoActual { get; set; }
        [DataMember(Name = "nombreBonoActual")]
        public string NombreBonoActual { get; set; }
    }
}
