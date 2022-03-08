using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetServicesByPlan
{
    public class PlanServiceRequest:Claro.Entity.Request
    {
        [DataMember]
        public string idplan { get; set; }
        [DataMember]
        public string strTipoProducto { get; set; }
    }
}
