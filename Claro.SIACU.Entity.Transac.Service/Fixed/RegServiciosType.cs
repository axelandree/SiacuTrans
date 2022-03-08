using System.Runtime.Serialization;
using Claro.Data;
using System.Collections.Generic;

namespace Claro.SIACU.Entity.Transac.Service.Fixed
{
    [DataContract(Name = "RegServiciosType")]
    public class RegServiciosType
    {
        [DataMember]
        public string strTipEqu { get; set; }
        [DataMember]
        public string strCoId { get; set; }
        [DataMember]
        public string strSnCode { get; set; }
        [DataMember]
        public string strSpCode { get; set; }
        [DataMember]
        public string strProfileId { get; set; }
        [DataMember]
        public CamposAdicionalesDcto CamposAdicionalesDescuento { get; set; }
        [DataMember]
        public CamposAdicionalesCargo CamposAdicionalesCargo { get; set; }
        [DataMember]
        public string strCodGroupServ { get; set; }

        [DataMember]
        public List<NumberDirectory> listaNumerosDirectorio { get; set; }

    }
}
