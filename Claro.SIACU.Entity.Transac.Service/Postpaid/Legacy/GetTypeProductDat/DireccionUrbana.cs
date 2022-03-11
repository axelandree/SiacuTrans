using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.Legacy.GetTypeProductDat
{
    [DataContract]
    public class DireccionUrbana
    {
        [DataMember(Name = "referenciaDireccion")]
        public object referenciaDireccion { get; set; }
        [DataMember(Name = "tipoVia")]
        public object tipoVia { get; set; }
        [DataMember(Name = "distrito")]
        public object distrito { get; set; }
        [DataMember(Name = "departamento")]
        public object departamento { get; set; }
        [DataMember(Name = "provincia")]
        public string provincia { get; set; }
        [DataMember(Name = "codigoPostal")]
        public string codigoPostal { get; set; }
        [DataMember(Name = "nombreCalle")]
        public object nombreCalle { get; set; }
        [DataMember(Name = "nroCuadra")]
        public string nroCuadra { get; set; }
        [DataMember(Name = "_propiedad")]
        public List<Propiedad> _propiedad { get; set; }
    }
}
