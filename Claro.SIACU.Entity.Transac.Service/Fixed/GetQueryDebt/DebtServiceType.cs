using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetQueryDebt
{
    [DataContract]
    public class DebtServiceType
    {
        [DataMember]
        public string xEstadoDeudor { get; set; }
        [DataMember]
        public string xCodTipoServicio { get; set; }
        [DataMember]
        public string xFlagCronologPago { get; set; }
        [DataMember]
        public string xFlagPagoParcial { get; set; }
        [DataMember]
        public string xFlagPagoVencido { get; set; }
        [DataMember]
        public string xFlagRestricPago { get; set; }
        [DataMember]
        public string xOpcionRecaudacion { get; set; }
        [DataMember]
        public string xCodMoneda { get; set; }
        [DataMember]
        public decimal xNroDocs { get; set; }
        [DataMember]
        public decimal xMontoDeudaTotal { get; set; }
        [DataMember]
        public string xMensaje1 { get; set; }
        [DataMember]
        public string xMensaje2 { get; set; }
        [DataMember]
        public string xDatoServicio { get; set; }
        [DataMember]
        public List<DebtDocumentType> xDeudaDocs { get; set; }
       
    }
}
