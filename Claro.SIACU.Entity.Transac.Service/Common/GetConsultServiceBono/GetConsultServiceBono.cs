using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetConsultServiceBono
{
    [DataContract]
    public class GetConsultServiceBono
    {
        [DataMember(Name = "coId")]
        public string CoId { get; set; }
        [DataMember(Name = "campana")]
        public string Campana { get; set; }
        [DataMember(Name = "bonoId")]
        public string BonoId { get; set; }
        [DataMember(Name = "tipo")]
        public string Tipo { get; set; }

    }
}
