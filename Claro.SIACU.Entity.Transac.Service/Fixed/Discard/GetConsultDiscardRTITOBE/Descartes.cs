using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.Discard.GetConsultDiscardRTITOBE
{
    [DataContract]
    public class Descartes
    {
        [DataMember]
        public string idDescarte { get; set; }
        [DataMember]
        public string nombreVariable { get; set; }
        [DataMember]
        public string descripcionDescarte { get; set; }
        [DataMember]
        public string tipoDescarte { get; set; }
        [DataMember]
        public string flagDescarte { get; set; }
        [DataMember]
        public string ordenDescarte { get; set; }
        [DataMember]
        public string fechaRegistro { get; set; }
        [DataMember]
        public string idGrupo { get; set; }
        [DataMember]
        public string flagError { get; set; }
        [DataMember]
        public string flagOk { get; set; }
        [DataMember]
        public string descarteValor { get; set; }
        [DataMember]
        public List<DescarteListaValor> descarteListaValor { get; set; }

    }
}
