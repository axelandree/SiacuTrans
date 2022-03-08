using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetBusinessRules
{
    public class BusinessRulesRequest : Claro.Entity.Request
    {
        [DataMember]
        public string SUB_CLASE { get; set; }
    }
}
