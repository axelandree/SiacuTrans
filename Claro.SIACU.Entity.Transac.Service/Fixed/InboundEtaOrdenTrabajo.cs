using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed
{
    [DataContract]
    public class InboundEtaOrdenTrabajo
    {
        [DataMember]
        public string nroOrden { get; set; }
        [DataMember]
        public string nroUsuario { get; set; }
        [DataMember]
        public string tipoTrabajo { get; set; }
        [DataMember]
        public string franjasHorariasOrdenTrabajo { get; set; }
        [DataMember]
        public string tiempoRecordatorioMinutos { get; set; }
        [DataMember]
        public string duracion { get; set; }
        [DataMember]
        public List<InboundEtaDetalleOrdenTrabajo> propiedades { get; set; }
    }
}
