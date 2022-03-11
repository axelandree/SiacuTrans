using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetConsultCampaign
{
    [DataContract]
    public class GetConsultCampaignCursor
    {
        [DataMember(Name = "tipoDocumento")]
        public string KindDocument { get; set; }
        [DataMember(Name = "nroDocumento")]
        public string NroDocument { get; set; }
        [DataMember(Name = "nroLinea")]
        public string NroLine { get; set; }
        [DataMember(Name = "nroSec")]
        public string NroSec { get; set; }
        [DataMember(Name = "nroPed")]
        public string NroPed { get; set; }
        [DataMember(Name = "planCodigo")]
        public string PlanCode { get; set; }
        [DataMember(Name = "planDescripcion")]
        public string PlanDescription { get; set; }
        [DataMember(Name = "tipoPrdCodigo")]
        public string KindPrdCode { get; set; }
        [DataMember(Name = "tipoPrdDescripcion")]
        public string KindPrdDescription { get; set; }
        [DataMember(Name = "campaniaCodigo")]
        public string CampaignCode { get; set; }
        [DataMember(Name = "campaniaDescripcion")]
        public string CampaignDescription { get; set; }
        [DataMember(Name = "coId")]
        public string CoId { get; set; }
        [DataMember(Name = "tipoOpeCodigo")]
        public string KindOpeCode { get; set; }
        [DataMember(Name = "tipoOpeDescripcion")]
        public string KindOpeDescription { get; set; }
        [DataMember(Name = "estado")]
        public string State { get; set; }
        [DataMember(Name = "usuarioCrea")]
        public string UserCreates { get; set; }
        [DataMember(Name = "fechaCrea")]
        public string DateCreates { get; set; }
        [DataMember(Name = "usuarioModifica")]
        public string UserModify { get; set; }
        [DataMember(Name = "fechaModifica")]
        public string DateModify { get; set; }
    }
}
