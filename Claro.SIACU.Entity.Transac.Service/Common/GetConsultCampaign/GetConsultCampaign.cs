using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetConsultCampaign
{
    [DataContract]
    public class GetConsultCampaign
    {
        [DataMember(Name = "numLinea")]
        public string NumLine { get; set; }
        [DataMember(Name = "tipoDoc")]
        public string KindDoc { get; set; }
        [DataMember(Name = "nroDoc")]
        public string NroDoc { get; set; }
        [DataMember(Name = "coId")]
        public string CoId { get; set; }
        [DataMember(Name = "nroPed")]
        public string NroPed { get; set; }
        [DataMember(Name = "nroPedDet")]
        public string NroPedDet { get; set; }
        [DataMember(Name = "nroCont")]
        public string NroCont { get; set; }
        [DataMember(Name = "nroContDet")]
        public string NroContDet { get; set; }
        [DataMember(Name = "tipoPrdCod")]
        public string KindPrdCod { get; set; }
    }
}
