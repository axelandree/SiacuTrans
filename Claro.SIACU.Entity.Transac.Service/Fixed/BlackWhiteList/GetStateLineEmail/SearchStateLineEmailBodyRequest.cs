using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.BlackWhiteList.GetStateLineEmail
{
    public class SearchStateLineEmailBodyRequest
    {
        [DataMember(Name = "nroTelefono")]
        public string nroTelefono { get; set; }

        [DataMember(Name = "correo")]
        public string correo { get; set; }
    }
}
