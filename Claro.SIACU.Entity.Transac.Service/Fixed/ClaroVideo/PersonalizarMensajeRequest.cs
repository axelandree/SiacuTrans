using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo
{
   [DataContract(Name = "PersonalizarMensajeRequest")]
    public class PersonalizarMensajeRequest
    {
          [DataMember(Name = "correlatorId")]
          public string correlatorId { get; set; }

          [DataMember(Name = "employeeId")]
          public string employeeId { get; set; }

          [DataMember(Name = "mensajeAmco")]
          public string mensajeAmco { get; set; }
    }
}
