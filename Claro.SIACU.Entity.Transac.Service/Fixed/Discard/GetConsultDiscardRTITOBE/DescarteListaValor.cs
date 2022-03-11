using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.Discard.GetConsultDiscardRTITOBE
{
    [DataContract]
    public class DescarteListaValor
    {
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public string valor { get; set; }
        [DataMember]
        public string medida { get; set; }
        [DataMember]
        public string fechaVencimiento { get; set; }
        [DataMember]
        public string movNombre { get; set; }
        [DataMember]
        public string esCabecera { get; set; }
    }
}
