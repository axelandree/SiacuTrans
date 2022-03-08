using System.Runtime.Serialization;
using Claro.Data;
using System.Collections.Generic;
namespace Claro.SIACU.Entity.Transac.Service.Fixed
{
    [DataContract(Name = "Contract")]
    public class Contract
    {
        [DataMember]
        public string strIpAplicacion { get; set; }
        [DataMember]
        public string strNombreAplicacion { get; set; }
        [DataMember]
        public string strTipoPostpago { get; set; }
        [DataMember]
        public List<ContractElement> ContractList { get; set; }

    }
}
