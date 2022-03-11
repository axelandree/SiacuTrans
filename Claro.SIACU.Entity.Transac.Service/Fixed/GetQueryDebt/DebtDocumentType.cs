using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetQueryDebt
{
    [DataContract]
    public class DebtDocumentType
    {
        [DataMember]
        public string xTipoServicio { get; set; }
        [DataMember]
        public string xDescripServ { get; set; }
        [DataMember]
        public string xNumeroDoc { get; set; }
        [DataMember]
        public string xFechaEmision { get; set; }
        [DataMember]
        public string xFechaVenc { get; set; }
        [DataMember]
        public string xMontoDebe { get; set; }
        [DataMember]
        public string xMontoFact { get; set; }
        [DataMember]
        public string xImportePagoMin { get; set; }
        [DataMember]
        public string xCodConcepto1 { get; set; }
        [DataMember]
        public string xCodConcepto2 { get; set; }
        [DataMember]
        public string xCodConcepto3 { get; set; }
        [DataMember]
        public string xCodConcepto4 { get; set; }
        [DataMember]
        public string xCodConcepto5 { get; set; }
        [DataMember]
        public string xImporteConcepto1 { get; set; }
        [DataMember]
        public string xImporteConcepto2 { get; set; }
        [DataMember]
        public string xImporteConcepto3 { get; set; }
        [DataMember]
        public string xImporteConcepto4 { get; set; }
        [DataMember]
        public string xImporteConcepto5 { get; set; }
        [DataMember]
        public string xReferenciaDeuda { get; set; }
        [DataMember]
        public string xDatoDocumento { get; set; }

    }
}
