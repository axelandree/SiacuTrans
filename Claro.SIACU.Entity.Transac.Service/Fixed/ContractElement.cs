using System.Runtime.Serialization;
using Claro.Data;
using System.Collections.Generic;
namespace Claro.SIACU.Entity.Transac.Service.Fixed
{
    [DataContract(Name = "ContractElement")]
    public class ContractElement
    {
        [DataMember]
        public string strPlanTarifario { get; set; }
        [DataMember]
        public string strIdSubmercado { get; set; }
        [DataMember]
        public string strIdMercado { get; set; }
        [DataMember]
        public string strRed { get; set; }
        [DataMember]
        public string strEstadoUmbral { get; set; }
        [DataMember]
        public string strCantidadUmbral { get; set; }
        [DataMember]
        public string strArchivoLlamadas { get; set; }
        [DataMember]
        public List<RegServiciosType> ListServices { get; set; }
        [DataMember]
        public ActualizacionContrato ActualizacionContrato { get; set; }
        [DataMember]
        public InformacionContrato InformacionContrato { get; set; }

    }
}
