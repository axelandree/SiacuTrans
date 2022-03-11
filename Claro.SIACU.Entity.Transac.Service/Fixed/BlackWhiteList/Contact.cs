using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.BlackWhiteList
{
    [DataContract(Name = "contactos")]
    public class Contact
    {
        [DataMember(Name = "contId")]
        public string contId { get; set; }

        [DataMember(Name = "cliId")]
        public string cliId { get; set; }

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

    }
}
