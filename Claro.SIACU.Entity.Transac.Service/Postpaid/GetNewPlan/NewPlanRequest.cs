using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetNewPlan
{
    [DataContract(Name = "NewPlanRequestTransactions")]
    public class NewPlanRequest: Claro.Entity.Request
    {     
        [DataMember]
        public string ValorTipoProducto { get; set; }
        [DataMember]
        public string CategoriaProducto { get; set; }
        [DataMember]
        public string MigracionPlan { get; set; }
        [DataMember]
        public string PlanActual { get; set; }
        [DataMember]
        public int CodPlanTarifario { get; set; }
    }
 }
