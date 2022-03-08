using System.Runtime.Serialization;
using Claro.Data;
namespace Claro.SIACU.Entity.Transac.Service.Fixed
{
    [DataContract(Name = "ActualizarTipificacion")]
    public class ActualizarTipificacion
    {
        [DataMember]
        public string Orden { get; set; }
    }
}
