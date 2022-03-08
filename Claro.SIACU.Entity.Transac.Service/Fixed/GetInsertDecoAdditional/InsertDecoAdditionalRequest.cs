using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetInsertDecoAdditional
{
    [DataContract]
    public class InsertDecoAdditionalRequest : Claro.Entity.Request
    {
        [DataMember]
        public string vInter { get; set; }
        [DataMember]
        public string vServ { get; set; }
        [DataMember]
        public string vGrupoPrincipal { get; set; }
        [DataMember]
        public string vGrupo { get; set; }
        [DataMember]
        public string vCantidadInst { get; set; }
        [DataMember]
        public string vDscServ { get; set; }
        [DataMember]
        public string vBandwid { get; set; }
        [DataMember]
        public string vFlagLc { get; set; }
        [DataMember]
        public string vCantIdLinea { get; set; }
        [DataMember]
        public string vTipoEquipo { get; set; }
        [DataMember]
        public string vCodTipoEquipo { get; set; }
        [DataMember]
        public string vCantidad { get; set; }
        [DataMember]
        public string vDscEquipo { get; set; }
        [DataMember]
        public string vCodigoExt { get; set; }
        [DataMember]
        public string vSnCode { get; set; }
        [DataMember]
        public string vSpCode { get; set; }
        [DataMember]
        public string vCargoFijo { get; set; }
    }
}
