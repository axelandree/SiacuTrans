using System.Runtime.Serialization;
using Claro.Data;
namespace Claro.SIACU.Entity.Transac.Service.Fixed
{
    [DataContract(Name = "ActualizacionContrato")]
    public class ActualizacionContrato
    {
        [DataMember]
        public string strCoId { get; set; }
        [DataMember]
        public string strEstado { get; set; }
        [DataMember]
        public string strRazon { get; set; }

    }
}
