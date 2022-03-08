using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed
{
    [DataContract]
    public class PlanService
    {
        public PlanService()
        {
        }

        [DataMember]
        public string CodigoPlan { get; set; }
        [DataMember]
        public string DescPlan { get; set; }
        [DataMember]
        public string TmCode { get; set; }
        [DataMember]
        public string Solucion { get; set; }
        [DataMember]
        public string CodServSisact { get; set; }
        [DataMember]
        public string SNCode { get; set; }
        [DataMember]
        public string SPCode { get; set; }
        [DataMember]
        public string CodTipoServ { get; set; }
        [DataMember]
        public string TipoServ { get; set; }
        [DataMember]
        public string DesServSisact { get; set; }
        [DataMember]
        public string CodGrupoServ { get; set; }
        [DataMember]
        public string GrupoServ { get; set; }
        [DataMember]
        public string CF { get; set; }
        [DataMember]
        public string IdEquipo { get; set; }
        [DataMember]
        public string Equipo { get; set; }
        [DataMember]
        public string CantidadEquipo { get; set; }
        [DataMember]
        public string MatvIdSap { get; set; }
        [DataMember]
        public string MatvDesSap { get; set; }
        [DataMember]
        public string TipoEquipo { get; set; }
        [DataMember]
        public string CodigoExterno { get; set; }
        [DataMember]
        public string DesCodigoExterno { get; set; }
        [DataMember]
        public string ServvUsuarioCrea { get; set; }
        [DataMember]
        public string TarifaRetencion { get; set; }
        [DataMember]
        public string ArrServicios { get; set; }
    }

}
