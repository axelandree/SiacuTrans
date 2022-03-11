using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo
{
    [DataContract(Name = "PersonalizarMensajeResponse")]
    public class PersonalizarMensajeResponse
    {
        [DataMember(Name = "codRpta")]
          public string codRpta { get; set; }

        [DataMember(Name = "msjRpta")]
        public string msjRpta { get; set; }

        [DataMember(Name = "mensajePersonalizado")]
        public string mensajePersonalizado { get; set; }
    }
}
