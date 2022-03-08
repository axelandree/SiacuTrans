using System.Runtime.Serialization;
using Claro.Data;
namespace Claro.SIACU.Entity.Transac.Service.Fixed
{
    [DataContract(Name = "CamposAdicionalesDcto")]
    public class CamposAdicionalesDcto
    {
        [DataMember]
        public string strTipoCostoServicioAvanzado { get; set; }
        [DataMember]
        public string strCostoServicioAvanzado { get; set; }
        [DataMember]
        public string strPeriodoCostoServicioAvanzado { get; set; }

    }
}
