using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.RegisterInteractionAdjust
{
    [DataContract]
    public class RegistrarCabeceraDoc
    {
        [DataMember]
        public string piSubTotalAfecto { get; set; }
        [DataMember]
        public string piSubTotalNoAfecto { get; set; }
        [DataMember]
        public string piMontoIgv { get; set; }
        [DataMember]
        public string piFechaVenc { get; set; }
        [DataMember]
        public string piUsuRegistro { get; set; }
        [DataMember]
        public string piCicloFact { get; set; }
        [DataMember]
        public string piDocRef { get; set; }
        [DataMember]
        public string piFechaDocRef { get; set; }
        [DataMember]
        public string piIdTipoDoc { get; set; }
        [DataMember]
        public string piIdResponsabCrm { get; set; }
        [DataMember]
        public string piResponsabCrm { get; set; }
        [DataMember]
        public string piIdCliente { get; set; }
        [DataMember]
        public string piOhxAct { get; set; }
        [DataMember]
        public string piIdTipCliente { get; set; }
        [DataMember]
        public string piNumDoc { get; set; }
        [DataMember]
        public string piClienteCta { get; set; }
        [DataMember]
        public string piErrorWsSap { get; set; }
        [DataMember]
        public string piReintentosWsSap { get; set; }
        [DataMember]
        public string piNombreCliente { get; set; }
        [DataMember]
        public string piCiudad { get; set; }
        [DataMember]
        public string piCodigoPais { get; set; }
        [DataMember]
        public string piDireccion { get; set; }
        [DataMember]
        public string piTipoIdentFiscal { get; set; }
        [DataMember]
        public string piNumIdentFiscal { get; set; }
        [DataMember]
        public string piSubjetoImp { get; set; }
        [DataMember]
        public string piVersionSap6 { get; set; }
    }
}
