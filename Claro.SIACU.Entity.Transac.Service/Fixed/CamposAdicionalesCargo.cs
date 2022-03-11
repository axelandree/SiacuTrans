using System.Runtime.Serialization;
using Claro.Data;
namespace Claro.SIACU.Entity.Transac.Service.Fixed
{
    [DataContract(Name = "CamposAdicionalesCargo")]
    public class CamposAdicionalesCargo
    {
        [DataMember]
        public string strTipoCostoServicio { get; set; }
        [DataMember]
        public string strCostoServicio { get; set; }
        [DataMember]
        public string strPeriodoCostoServicio { get; set; }

    }
}
