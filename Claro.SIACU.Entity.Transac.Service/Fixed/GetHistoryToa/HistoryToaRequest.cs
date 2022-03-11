using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetHistoryToa
{
    [DataContract(Name = "HistoryToaRequest")]
    public class HistoryToaRequest : Claro.Entity.Request
    {
        [DataMember]
        public string strNroOrden { get; set; }
        [DataMember]
        public int strIdConsulta { get; set; }
        [DataMember]
        public string strFranja { get; set; }
        [DataMember]
        public DateTime strDiaReserva { get; set; }
        [DataMember]
        public string strIdBucket { get; set; }
        [DataMember]
        public string strCodZona { get; set; }
        [DataMember]
        public string strCodPlano { get; set; }
        [DataMember]
        public string strTipoOrden { get; set; }
        [DataMember]
        public string strSubTipoOrden { get; set; }
        [DataMember]
        public string strValor { get; set; }
        [DataMember]
        public int strTipoTransaccion { get; set; }
    }
}
