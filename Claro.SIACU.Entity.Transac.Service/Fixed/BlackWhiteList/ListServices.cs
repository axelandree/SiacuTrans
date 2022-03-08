using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.BlackWhiteList
{
    public class ListServices
    {
        [DataMember(Name = "errorId")]
        public string errorId { get; set; }

        [DataMember(Name = "msisdn")]
        public string msisdn { get; set; }

        [DataMember(Name = "tipoDoc")]
        public string tipoDoc { get; set; }

        [DataMember(Name = "tipoDocDes")]
        public string tipoDocDes { get; set; }

        [DataMember(Name = "nroDoc")]
        public string nroDoc { get; set; }

        [DataMember(Name = "nombApell")]
        public string nombApell { get; set; }

        [DataMember(Name = "email")]
        public string email { get; set; }

        [DataMember(Name = "tipoCliente")]
        public string tipoCliente { get; set; }

        [DataMember(Name = "origen")]
        public string origen { get; set; }

        [DataMember(Name = "tipoLinea")]
        public string tipoLinea { get; set; }

        [DataMember(Name = "tipoDocContact")]
        public string tipoDocContact { get; set; }

        [DataMember(Name = "tipoDocDescContact")]
        public string tipoDocDescContact { get; set; }

        [DataMember(Name = "nroDocContact")]
        public string nroDocContact { get; set; }

        [DataMember(Name = "nombresContact")]
        public string nombresContact { get; set; }

        [DataMember(Name = "tipoContact")]
        public string tipoContact { get; set; }

        [DataMember(Name = "servId")]
        public string servId { get; set; }

        [DataMember(Name = "contact")]
        public string contact { get; set; }

        [DataMember(Name = "desContactCanal")]
        public string desContactCanal { get; set; }

        [DataMember(Name = "contactAplic")]
        public string contactAplic { get; set; }

        [DataMember(Name = "fechaRespuesta")]
        public string fechaRespuesta { get; set; }

        [DataMember(Name = "estadoInfo")]
        public string estadoInfo { get; set; }

        [DataMember(Name = "motivoHist")]
        public string motivoHist { get; set; }

        [DataMember(Name = "tipoOper")]
        public string tipoOper { get; set; }

        [DataMember(Name = "motivoError")]
        public string motivoError { get; set; }

        [DataMember(Name = "estado")]
        public string estado { get; set; }

        [DataMember(Name = "estadoProceso")]
        public string estadoProceso { get; set; }

        [DataMember(Name = "cliId")]
        public string cliId { get; set; }

        [DataMember(Name = "contId")]
        public string contId { get; set; }

        [DataMember(Name = "tipoMedioAprob")]
        public string tipoMedioAprob { get; set; }

        [DataMember(Name = "interactId")]
        public int interactId { get; set; }

    }
}
