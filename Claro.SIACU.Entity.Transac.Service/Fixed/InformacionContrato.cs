using System.Runtime.Serialization;
using Claro.Data;
using System.Collections.Generic;
namespace Claro.SIACU.Entity.Transac.Service.Fixed
{
    [DataContract(Name = "InformacionContrato")]
    public class InformacionContrato
    {
        [DataMember]
        public string coId { get; set; }
        [DataMember]
        public List<Campo> Campos { get; set; }
    }
}
