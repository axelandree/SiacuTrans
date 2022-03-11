using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.BlackWhiteList
{
    [DataContract(Name = "datosCliente")]
    public class DataClient
    {

        [DataMember(Name = "cliId")]
        public string cliId { get; set; }

        [DataMember(Name = "tipoDoc")]
        public string tipoDoc { get; set; }

        [DataMember(Name = "tipoDocDesc")]
        public string tipoDocDesc { get; set; }

        [DataMember(Name = "nroDocumento")]
        public string nroDocumento { get; set; }

        [DataMember(Name = "nombresYApellidos")]
        public string nombresYApellidos { get; set; }

        [DataMember(Name = "email")]
        public string email { get; set; }

        [DataMember(Name = "tipoCliente")]
        public string tipoCliente { get; set; }

        [DataMember(Name = "origen")]
        public string origen { get; set; }

        [DataMember(Name = "usuarioCrea")]
        public string usuarioCrea { get; set; }

        [DataMember(Name = "fechaCrea")]
        public string fechaCrea { get; set; }

        [DataMember(Name = "usuarioModi")]
        public string usuarioModi { get; set; }

        [DataMember(Name = "fechaModi")]
        public string fechaModi { get; set; }

        [DataMember(Name = "contactos")]
        public List<Contact> contactos { get; set; }

        [DataMember(Name = "detalle")]
        public List<Details> detalle { get; set; }

    }
    
}
