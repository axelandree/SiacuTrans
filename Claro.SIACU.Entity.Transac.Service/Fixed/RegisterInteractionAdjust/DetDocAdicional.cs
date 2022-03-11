using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.RegisterInteractionAdjust
{
    [DataContract]
    public class DetDocAdicional
    {
        [DataMember]
        public string pIdCategoria { get; set; }
        [DataMember]
        public string pNombreCategoria { get; set; }
        [DataMember]
        public string pImporte { get; set; }
        [DataMember]
        public string pImporteAjustar { get; set; }
        [DataMember]
        public string pIgvImporteAjustarIgv { get; set; }
        [DataMember]
        public string pImporteAjustarIgv { get; set; }
        [DataMember]
        public string pIdAreaImputar { get; set; }
        [DataMember]
        public string pNombreAreaImputar { get; set; }
        [DataMember]
        public string pIdMotivo { get; set; }
        [DataMember]
        public string pNombreMotivo { get; set; }
        [DataMember]
        public string pIdResponsable { get; set; }
        [DataMember]
        public string pNombreResponsable { get; set; }
    }
}
