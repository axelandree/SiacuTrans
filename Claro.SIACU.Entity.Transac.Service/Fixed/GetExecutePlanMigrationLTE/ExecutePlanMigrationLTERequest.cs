using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetExecutePlanMigrationLTE
{
    [DataContract(Name = "ExecutePlanMigrationLTERequest")]
    public class ExecutePlanMigrationLTERequest : Claro.Entity.Request
    {
        [DataMember]
        public string strTransactionId { get; set; }
        [DataMember]
        public TypificationItem objTipification { get; set; }
        [DataMember]
        public ClientParameters objClientParameters { get; set; }
        [DataMember]
        public List<ServiceByPlan> lstServicesList { get; set; }
        [DataMember]
        public MainParameters objMainParameters { get; set; }
        [DataMember]
        public PlusParameters objPlusParameters { get; set; }
        [DataMember]
        public EtaSelection objEtaSelection { get; set; }
        [DataMember]
        public SotParametersLTE objSotParametersLTE { get; set; }
        [DataMember]
        public EtaParameters objEtaParameters { get; set; }
        [DataMember]
        public Contract objContract { get; set; }
        [DataMember]
        public ActualizarTipificacion objActualizarTipificacion { get; set; }
        [DataMember]
        public bool blFlagContingencia { get; set; }
        [DataMember]
        public bool blFlagCrearPlantilla { get; set; }
        [DataMember]
        public AuditRegister objAuditRegister { get; set; }
        [DataMember]
        public List<Coser> lstListCoser { get; set; }
        [DataMember]
        public bool blFlagValidaEta { get; set; }
        [DataMember]
        public string strParametrosConstancia { get; set; }
        [DataMember]
        public string strDestinatarioCorreo { get; set; }
        [DataMember]
        public string strNotes { get; set; }
        [DataMember]
        public string strTipoPlan { get; set; }
        [DataMember]
        public string strCodPlan { get; set; }
        [DataMember]
        public string strTmCode { get; set; }
        [DataMember]
        public string strTipoProducto { get; set; }
        [DataMember]
        public string strCodServicioGeneralTope { get; set; }
        [DataMember]
        public string strCodOCCTope { get; set; }
        [DataMember]
        public double dblMontoTopeConsumo { get; set; }
        [DataMember]
        public double dblTopeConsumo { get; set; }
        [DataMember]
        public string strComentTopeConsumo { get; set; }
        [DataMember]
        public double dblLimiteCredito { get; set; }
        [DataMember]
        public string strAnotacionToa { get; set; }
    }
}
