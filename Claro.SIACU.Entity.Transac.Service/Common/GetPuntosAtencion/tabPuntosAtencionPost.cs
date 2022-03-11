using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetPuntosAtencion
{
    [DataContract]
    public class tabPuntosAtencionPost 
    {
        [DataMember(Name="codele")]
        public string codele { get; set; }
        [DataMember(Name = "nombre")]
        public string nombre { get; set; }
        [DataMember(Name = "tipo")]
        public string tipo { get; set; }
        [DataMember(Name = "rank")]
        public string rank { get; set; }
        [DataMember(Name = "cacTypeCodele")]
        public string cacTypeCodele { get; set; }
        [DataMember(Name = "cacTypeTitle")]
        public string cacTypeTitle { get; set; }

    }
}
