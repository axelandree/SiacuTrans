using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.Legacy.GetAdditionalServices
{
    [DataContract]
    public class serviciosAsociados
    {
        [DataMember(Name = "descripcionGrupo")]
        public string descripcionGrupo { get; set; }
        [DataMember(Name = "numeroGrupo")]
        public string numeroGrupo { get; set; }
        [DataMember(Name = "codigoServicio")]
        public string codigoServicio { get; set; }
        [DataMember(Name = "descripcionServicio")]
        public string descripcionServicio { get; set; }
        [DataMember(Name = "tipoServicio")]
        public string tipoServicio { get; set; }
        [DataMember(Name = "categoriaServicio")]
        public string categoriaServicio { get; set; }
        [DataMember(Name = "valorServicio")]
        public string valorServicio { get; set; }
        [DataMember(Name = "codigoExcluyente")]
        public string codigoExcluyente { get; set; }
        [DataMember(Name = "descripcionExcluyente")]
        public string descripcionExcluyente { get; set; }
        [DataMember(Name = "daPo")]
        public string daPo { get; set; }
        [DataMember(Name = "estado")]
        public string estado { get; set; }
        [DataMember(Name = "validoDesde")]
        public string validoDesde { get; set; }
        [DataMember(Name = "cargoFijo")]
        public string cargoFijo { get; set; }
        [DataMember(Name = "cuota")]
        public string cuota { get; set; }
        [DataMember(Name = "periodo")]
        public string periodo { get; set; }
        [DataMember(Name = "bloqueoActivacion")]
        public string bloqueoActivacion { get; set; }
        [DataMember(Name = "bloqueoDesactivacion")]
        public string bloqueoDesactivacion { get; set; }
        [DataMember(Name = "suscripcion")]
        public string suscripcion { get; set; }
        [DataMember(Name = "itemCargo")]
        public string itemCargo { get; set; }
        [DataMember(Name = "productId")]
        public string productId { get; set; }
        [DataMember(Name = "unidadTipo")]
        public string unidadTipo { get; set; }
        [DataMember(Name = "unidad")]
        public string unidad { get; set; }
        [DataMember(Name = "periodoTipo")]
        public string periodoTipo { get; set; }
        [DataMember(Name = "tipoCompra")]
        public string tipoCompra { get; set; }
        [DataMember(Name = "codigoConvergente")]
        public string codigoConvergente { get; set; }
    }
}
