using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetExecuteMigrationPlan
{
    [DataContract(Name = "ExecuteMigrationPlanRequestTransactions")]
    public class ExecuteMigrationPlanRequest : Claro.Entity.Request
    {
        [DataMember]
        public string Msisdn { get; set; }
        [DataMember]
        public string CoId { get; set; }
        [DataMember]
        public string CustomerId { get; set; }
        [DataMember]
        public string Cuenta { get; set; }
        [DataMember]
        public string Escenario { get; set; }
        [DataMember]
        public string TipoProducto { get; set; }
        [DataMember]
        public string ServiciosAdicionales { get; set; }
        [DataMember]
        public string CodigoProducto { get; set; }
        [DataMember]
        public string CodPlanBase { get; set; }
        [DataMember]
        public decimal MontoApadece { get; set; }
        [DataMember]
        public decimal MontoFidelizar { get; set; }
        [DataMember]
        public string FlagValidaApadece { get; set; }
        [DataMember]
        public string FlagAplicaApadece { get; set; }
        [DataMember]
        public string TopeConsumo { get; set; }
        [DataMember]
        public string TipoTope { get; set; }
        [DataMember]
        public string DescripcionTipoTpe { get; set; }
        [DataMember]
        public string TipoRegistroTope { get; set; }
        [DataMember]
        public int TopeControlConsumo { get; set; }
        [DataMember]
        public string FechaProgramacionTope { get; set; }
        [DataMember]
        public string CAC { get; set; }
        [DataMember]
        public string Asesor { get; set; }
        [DataMember]
        public string CodigoInteraccion { get; set; }
        [DataMember]
        public decimal MontoPCS { get; set; }
        [DataMember]
        public string AreaPCS { get; set; }
        [DataMember]
        public string MotivoPCS { get; set; }
        [DataMember]
        public string SubMotivoPCS { get; set; }
        [DataMember]
        public int CicloFacturacion { get; set; }
        [DataMember]
        public string IdTipoCliente { get; set; }
        [DataMember]
        public string NumeroDocumento { get; set; }
        [DataMember]
        public string FlagServicioOnTop { get; set; }
        [DataMember]
        public string FechaProgramacion { get; set; }
        [DataMember]
        public string FlagLimiteCredito { get; set; }
        [DataMember]
        public string TipoClarify { get; set; }
        [DataMember]
        public string NumeroCuentaPadre { get; set; }
        [DataMember]
        public string UsuarioAplicacion { get; set; }
        [DataMember]
        public string UsuarioSistema {get;set;}
    }
}
