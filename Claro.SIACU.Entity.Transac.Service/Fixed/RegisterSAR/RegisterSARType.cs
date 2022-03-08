using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.RegisterSAR
{
    [DataContract]
    public class RegisterSARType
    {
        [DataMember]
        public string piCorrelativoSar { get; set; }
        [DataMember]
        public string piCorrelativoSaraReclamo { get; set; }
        [DataMember]
        public string piNroInteraccionRelacion { get; set; }
        [DataMember]
        public string piNroCasoRelacion { get; set; }
        [DataMember]
        public string piEscenarioSarSara { get; set; }
        [DataMember]
        public string piSolAnticipadaSeleccion { get; set; }
        [DataMember]
        public string piFechaCreacion { get; set; }
        [DataMember]
        public string piHoraCreacion { get; set; }
    }
}
