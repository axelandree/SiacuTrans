using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetQueryDebt
{
    [DataContract]
    public class QueryDebtRequest : Claro.Entity.Request
    {
        [DataMember]
        public string pTxId { get; set; }
        [DataMember]
        public string pCodAplicacion { get; set; }
        [DataMember]
        public string pCodBanco { get; set; }
        [DataMember]
        public string pCodReenvia { get; set; }
        [DataMember]
        public string pCodMoneda { get; set; }
        [DataMember]
        public string pCodTipoServicio { get; set; }
        [DataMember]
        public decimal pPosUltDocumento { get; set; }
        [DataMember]
        public string pTipoIdentific { get; set; }
        [DataMember]
        public string pDatoIdentific { get; set; }
        [DataMember]
        public string pNombreComercio { get; set; }
        [DataMember]
        public string pNumeroComercio { get; set; }
        [DataMember]
        public string pCodAgencia { get; set; }
        [DataMember]
        public string pCodCanal { get; set; }
        [DataMember]
        public string pCodCiudad { get; set; }
        [DataMember]
        public string pNroTerminal { get; set; }
        [DataMember]
        public string pPlaza { get; set; }
        [DataMember]
        public string pNroReferencia { get; set; }

    }
}
