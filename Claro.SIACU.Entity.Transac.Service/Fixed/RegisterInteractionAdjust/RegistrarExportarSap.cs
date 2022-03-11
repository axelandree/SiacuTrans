using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.RegisterInteractionAdjust
{
    [DataContract]
    public class RegistrarExportarSap
    {
        [DataMember]
        public string piCuentaCab { get; set; }
        [DataMember]
        public string piTipoDocCab { get; set; }
        [DataMember]
        public string piClaseDocumentoCab { get; set; }
        [DataMember]
        public string piSociedadCab { get; set; }
        [DataMember]
        public string piMonedaCab { get; set; }
        [DataMember]
        public string piTextoCab { get; set; }
        [DataMember]
        public string piClavePosCab { get; set; }
        [DataMember]
        public string piClaveNegCab { get; set; }
        [DataMember]
        public string piTipoDocDet { get; set; }
        [DataMember]
        public string piIndivaDet { get; set; }
        [DataMember]
        public string piClavePosDet { get; set; }
        [DataMember]
        public string piClaveNegDet { get; set; }
        [DataMember]
        public string piTipoDocIgv { get; set; }
        [DataMember]
        public string piCuentaIgv { get; set; }
        [DataMember]
        public string piIndicadorSap { get; set; }
        [DataMember]
        public string piFlagEliminarAnterior { get; set; }
    }
}
