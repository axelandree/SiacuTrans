using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo
{
   public class RegistrarControlesCvRequest
    {
       [DataMember(Name = "transaccionId")]
       public string transaccionId { get; set; }

       [DataMember(Name = "flagTransaccion")]
       public string flagTransaccion { get; set; }

       [DataMember(Name = "tipoTransaccion")]
       public string tipoTransaccion { get; set; }

       [DataMember(Name = "documentoVenta")]
       public string documentoVenta { get; set; }

       [DataMember(Name = "nombreAplicacion")]
       public string nombreAplicacion { get; set; }

       [DataMember(Name = "operacionSuscripcion")]
       public string operacionSuscripcion { get; set; }

       [DataMember(Name = "nombreServicio")]
       public string nombreServicio { get; set; }

       [DataMember(Name = "nombrePdv")]
       public string nombrePdv { get; set; }

       [DataMember(Name = "custormerId")]
       public string custormerId { get; set; }

       [DataMember(Name = "linea")]
       public string linea { get; set; }

       [DataMember(Name = "estadoTransaccion")]
       public string estadoTransaccion { get; set; }

       [DataMember(Name = "mensajeTransaccion")]
       public string mensajeTransaccion { get; set; }

    }
}
