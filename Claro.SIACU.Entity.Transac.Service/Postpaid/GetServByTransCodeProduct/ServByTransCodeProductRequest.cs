using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetServByTransCodeProduct
{
    [DataContract(Name = "ServByTransCodeProductRequest")]
    public class ServByTransCodeProductRequest : Claro.Entity.Request
    {
        [DataMember]
        public string CodProducto { get; set; }

    }
}
