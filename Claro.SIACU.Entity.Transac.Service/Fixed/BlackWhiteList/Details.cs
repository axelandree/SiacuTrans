using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.BlackWhiteList
{
      [DataContract(Name = "detalle")]
     public class Details
    {
         [DataMember(Name = "listId")]
         public string listId { get; set; }

         [DataMember(Name = "cliId")]
         public string cliId { get; set; }

         [DataMember(Name = "msisdn")]
         public string msisdn { get; set; }

         [DataMember(Name = "servId")]
         public string servId { get; set; }

         [DataMember(Name = "contacto")]
         public string contacto { get; set; }

         [DataMember(Name = "desContactCanal")]
         public string desContactCanal { get; set; }

         [DataMember(Name = "contactAplic")]
         public string contactAplic { get; set; }

         [DataMember(Name = "tipoMedioAprob")]
         public string tipoMedioAprob { get; set; }

         [DataMember(Name = "interacId")]
         public string interacId { get; set; }

         [DataMember(Name = "fechaRespuesta")]
         public string fechaRespuesta { get; set; }

         [DataMember(Name = "estadoInfo")]
         public string estadoInfo { get; set; }

         [DataMember(Name = "tipoLinea")]
         public string tipoLinea { get; set; }
          
    }
}
