using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetETAAuditRequestCapacity
{
    [DataContract(Name = "ETAAuditRequestCapacity")]
    public class ETAAuditRequestCapacity : Claro.Entity.Request
    {
        [DataMember]
        public string pIdTrasaccion { get; set; }
        [DataMember]
        public string pIP_APP { get; set; }
        [DataMember]
        public string pAPP { get; set; }
        [DataMember]
        public string pUsuario { get; set; }
        [DataMember]
        public DateTime[] vFechas { get; set; }
        [DataMember]
        public string[] vUbicacion { get; set; }
        [DataMember]
        public bool vCalcDur { get; set; }
        [DataMember]
        public bool vCalcDurEspec { get; set; }
        [DataMember]
        public bool vCalcTiempoViaje { get; set; }
        [DataMember]
        public bool vCalcTiempoViajeEspec { get; set; }
        [DataMember]
        public bool vCalcHabTrabajo { get; set; }
        [DataMember]
        public bool vCalcHabTrabajoEspec { get; set; }
        [DataMember]
        public bool vObtenerUbiZona { get; set; }
        [DataMember]
        public bool vObtenerUbiZonaEspec { get; set; }
        [DataMember]
        public string[] vEspacioTiempo { get; set; }
        [DataMember]
        public string[] vHabilidadTrabajo { get; set; }
        [DataMember]
        public ETAFieldActivity[] vCampoActividad { get; set; }
        [DataMember]
        public ETAListParametersRequestOptionalCapacity[] vListaCapReqOpc { get; set; }
    }
}
