using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetProductTracDeacServ
{
    [DataContract]
    public class ProductTracDeacServResponse
    {
        [DataMember]
        public string IdProductoMayor { get; set; }
    }
}
