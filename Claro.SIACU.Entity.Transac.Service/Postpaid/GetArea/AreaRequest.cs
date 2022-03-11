using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetArea
{
    [DataContract(Name = "AreaRequest")]
    public class AreaRequest : Claro.Entity.Request
    {
        public int MyProperty { get; set; }
    }
}
