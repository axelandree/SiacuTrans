using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetConsultServiceBono
{
    [DataContract]
    public class GetConsultServiceBonoBodyResponse
    {
        [DataMember(Name = "mensajeError")]
        public string MensajeError { get; set; }
        [DataMember(Name = "codigoRespuesta")]
        public string CodigoRespuesta { get; set; }
        [DataMember(Name = "mensajeRespuesta")]
        public string MensajeRespuesta { get; set; }
        [DataMember(Name = "servicioInternet")]
        public string ServicioInternet { get; set; }
        [DataMember(Name = "flagFullClaro")]
        public int FlagFullClaro { get; set; }
        [DataMember(Name = "flagAumentoVelocidad")]
        public int FlagAumentoVelocidad { get; set; }
        [DataMember(Name = "flagBonoActual")]
        public int FlagBonoActual { get; set; }

        [DataMember(Name = "bonoActual")]
        public List<GetConsultServiceBonoListaBonoActual> ConsultServiceBonoListaBonoActual { get; set; }
        [DataMember(Name = "listaBonoDisponible")]
        public List<GetConsultServiceBonoListaBonoDisponible> ConsultServiceBonoListaBonoDisponible { get; set; }

    }
}
