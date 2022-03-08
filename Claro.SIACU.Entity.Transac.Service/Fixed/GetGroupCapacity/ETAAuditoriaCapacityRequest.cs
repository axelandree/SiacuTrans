using System;
using System.Runtime.Serialization;


namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetGroupCapacity
{
    [DataContract(Name = "ETAAuditoriaCapacityRequestHfc")]
    public class ETAAuditoriaCapacityRequest : Claro.Entity.Request
    {
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
        
        public BEETACampoActivity[] vCampoActividad { get; set; }
        [DataMember]
        public BEETAListaParamRequestOpcionalCapacity[] vListaCapReqOpc { get; set; }
            
    }
}
