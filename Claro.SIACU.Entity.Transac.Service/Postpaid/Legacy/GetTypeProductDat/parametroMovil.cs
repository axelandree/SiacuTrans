using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.Legacy.GetTypeProductDat
{
    [DataContract]
    public class parametroMovil : Claro.Entity.Request
    {
        [DataMember(Name = "strPlataforma")]
        public string strPlataforma { get; set; }
        [DataMember(Name = "strTelefono")]
        public string strTelefono { get; set; }
        [DataMember(Name = "intCodigoContrato")]
        public int intCodigoContrato { get; set; }
        [DataMember(Name = "strCodigoServicio")]
        public string strCodigoServicio { get; set; }
        [DataMember(Name = "strCodigoServicioOne")]
        public string strCodigoServicioOne { get; set; }
    }
}
