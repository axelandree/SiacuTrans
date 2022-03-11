using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Claro.SIACU.Entity.Transac.Service.Fixed.ExecutePlanMigrationLte
{
    [DataContract(Name = "MigratedPlanRequestLte")]
    public class MigratedPlanRequest : Claro.Entity.Request
    {
        [DataMember]
        public string TransactionId { get; set; }
        [DataMember]
        public TypificationItem Tipification { get; set; }
        [DataMember]
        public ClientParameters ClientParameters { get; set; }
        [DataMember]
        public List<ServiceByPlan> ServicesList { get; set; }
        [DataMember]
        public MainParameters MainParameters { get; set; }
        [DataMember]
        public PlusParameters PlusParameters { get; set; }
        [DataMember]
        public EtaSelection EtaSelection { get; set; }
        [DataMember]
        public SotParameters SotParameters { get; set; }
        [DataMember]
        public EtaParameters EtaParameters { get; set; }
        [DataMember]
        public Contract Contract { get; set; }
        [DataMember]
        public ActualizarTipificacion ActualizarTipificacion { get; set; }
        [DataMember]
        public bool FlagContingencia { get; set; }
        [DataMember]
        public bool FlagCrearPlantilla { get; set; }
        [DataMember]
        public AuditRegister AuditRegister { get; set; }
        [DataMember]
        public List<Coser> ListCoser { get; set; }
        [DataMember]
        public bool FlagValidaEta { get; set; }
        [DataMember]
        public string ParametrosConstancia { get; set; }
        [DataMember]
        public string DestinatarioCorreo { get; set; }
        [DataMember]
        public string Notes { get; set; }
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
        //[DataMember]
        //public string strCodOCCTope { get; set; }
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
