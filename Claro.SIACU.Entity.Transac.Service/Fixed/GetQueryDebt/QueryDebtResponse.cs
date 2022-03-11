using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetQueryDebt
{
    [DataContract]
    public class QueryDebtResponse 
    {
        [DataMember]
        public AuditType audit { get; set; }
        [DataMember]
        public string xIdentificacion { get; set; }
        [DataMember]
        public string xNombreCliente { get; set; }
        [DataMember]
        public string xMasDocumentosFlag { get; set; }
        [DataMember]
        public Decimal xPosUltDocumento { get; set; }
        [DataMember]
        public string xNroReferencia { get; set; }
        [DataMember]
        public string xNroIdentifCliente { get; set; }
        [DataMember]
        public string xNroServDevueltos { get; set; }
        [DataMember]
        public Decimal xNroDocsDeuda { get; set; }
        [DataMember]
        public string xDatoTransaccion { get; set; }
        [DataMember]
        public List<DebtServiceType> xDeudaCliente { get; set; }
        [DataMember]
        public string xErrStatus { get; set; }
        [DataMember]
        public string xErrMessage { get; set; }           

    }
}
