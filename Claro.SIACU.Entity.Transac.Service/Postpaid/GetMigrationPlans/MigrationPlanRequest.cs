using System.Runtime.Serialization;


namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetMigrationPlans
{ 
    [DataContract(Name = "MigrationPlanRequestTransactions")]
    public class MigrationPlanRequest: Claro.Entity.Request
    {       
        [DataMember]
        public string ValorTipoProducto { get; set; }
        [DataMember]
        public string CategoriaProducto { get; set; }
        [DataMember]
        public string Modalidad { get; set; }

        [DataMember]
        public string Familia { get; set; }
        [DataMember]
        public string MigracionPlan { get; set; }
        [DataMember]
        public string PlanActual { get; set; }
    }
}
