using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.RegisterActiDesaBonoDesc
{
    [DataContract]
    public class BodyRequest
    {
        [DataMember(Name="registrarDescHFCRequest")]
        public registrarDescHFCRequest registrarDescHFCRequest { get; set; }

        [DataMember(Name = "registrarDescLTERequest")]
        public registrarDescLTERequest registrarDescLTERequest { get; set; }
    }
}
