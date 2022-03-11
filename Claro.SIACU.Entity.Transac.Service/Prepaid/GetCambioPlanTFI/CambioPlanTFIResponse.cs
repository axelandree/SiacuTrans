using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Prepaid.GetCambioPlanTFI
{
    [DataContract]
    public class CambioPlanTFIResponse
    {
        [DataMember]
        public string offerAntiguo { get; set; }
        [DataMember]
        public string offerNuevo { get; set; }
        [DataMember]
        public string idRespuesta { get; set; }
        [DataMember]
        public string mensajeRespuesta { get; set; }

    }
}
