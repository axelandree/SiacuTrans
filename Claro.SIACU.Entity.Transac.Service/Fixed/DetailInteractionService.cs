using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed
{
    [DataContract]
    public class DetailInteractionService
    {
        [DataMember]
        public string IdServicio { get; set; }
        [DataMember]
        public string GsrvcPrincipal { get; set; }
        [DataMember]
        public string GsrvcCodigo { get; set; }
        [DataMember]
        public string Cantidad { get; set; }
        [DataMember]
        public string Servicio { get; set; }
        [DataMember]
        public string Bandwid { get; set; }
        [DataMember]
        public string FlagLc { get; set; }
        [DataMember]
        public string CantidadIdLinea { get; set; }
        [DataMember]
        public string IdEquipo { get; set; }
        [DataMember]
        public string CodTipEqu { get; set; }
        [DataMember]
        public string CantEquipo { get; set; }
        [DataMember]
        public string Equipo { get; set; }
        [DataMember]
        public string CodigoExt { get; set; }
    }
}
