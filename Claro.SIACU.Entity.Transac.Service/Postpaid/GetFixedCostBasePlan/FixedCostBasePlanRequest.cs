using System.Runtime.Serialization;


namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetFixedCostBasePlan
{
    [DataContract(Name = "FixedCostBasePlanRequestTransactions")]
    public class FixedCostBasePlanRequest : Claro.Entity.Request
    {
        [DataMember]
        public string CodigoProduct { get; set; }
        [DataMember]
        public string IdProduct { get; set; }
        [DataMember]
        public string CategoriaProducto { get; set; }
        [DataMember]
        public string DescriptionPlan { get; set; }
    }
}
