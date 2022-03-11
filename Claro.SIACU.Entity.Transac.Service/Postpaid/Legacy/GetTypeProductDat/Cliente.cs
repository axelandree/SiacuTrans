using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.Legacy.GetTypeProductDat
{
    [DataContract]
    public class Cliente
    {
        [DataMember(Name = "identificacionPersona")]
        public IdentificacionPersona identificacionPersona { get; set; }
        [DataMember(Name = "direccionUrbana")]
        public List<DireccionUrbana> direccionUrbana { get; set; }
    }
}
