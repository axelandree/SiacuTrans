using System.Runtime.Serialization;
using Claro.Data;
namespace Claro.SIACU.Entity.Transac.Service.Fixed
{
    [DataContract(Name = "Campo")]
    public class Campo
    {
        [DataMember]
        public string strIndice { get; set; }
        [DataMember]
        public string strTipo { get; set; }
        [DataMember]
        public string strValor { get; set; }

    }
}
