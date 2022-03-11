using Claro.Data;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Transac.Service.Fixed
{
    public class Carrier
    {
        [DbColumn("IDCARRIER")]
        [DataMember]
        public string IDCARRIER { get; set; }
        [DbColumn("OPERADOR")]
        [DataMember]
        public string OPERADOR { get; set; }

    }
}
