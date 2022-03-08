using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.Discard.GetConsultDiscardRTITOBE
{
    [DataContract]
    public class ResponseData
    {
        [DataMember]
        public List<Grupos> grupos { get; set; }
        [DataMember]
        public List<Descartes> descartes { get; set; }
        [DataMember]
        public List<ItemOpcional> listaOpcional { get; set; }
    }
}
