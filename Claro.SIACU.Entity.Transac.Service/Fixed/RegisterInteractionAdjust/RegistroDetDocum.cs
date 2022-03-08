using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.RegisterInteractionAdjust
{
    [DataContract]
    public class RegistroDetDocum
    {
        [DataMember]
        public string pImporte { get; set; }
        [DataMember]
        public string pMontoSinIgv { get; set; }
        [DataMember]
        public string pIgv { get; set; }
        [DataMember]
        public string pTotal { get; set; }
        [DataMember]
        public string pTelefono { get; set; }
        [DataMember]
        public string pFechaDesde { get; set; }
        [DataMember]
        public string pFechaHasta { get; set; }
        [DataMember]
        public string pIdCategoria { get; set; }
        [DataMember]
        public string pSubCategoria { get; set; }
        [DataMember]
        public string pTipoTran { get; set; }
    }
}
