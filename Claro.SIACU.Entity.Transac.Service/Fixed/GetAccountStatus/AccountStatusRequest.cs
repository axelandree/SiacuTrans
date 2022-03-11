using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetAccountStatus
{
    [DataContract]
    public class AccountStatusRequest : Claro.Entity.Request
    {
        [DataMember]
        public string txId { get; set; }
        [DataMember]
        public string pCodAplicacion { get; set; }
        [DataMember]
        public string pUsuarioAplic { get; set; }

        //[DataMember]
        //public string pCliTipoDocIdent { get;  set;}
        //[DataMember]
        // public string pCliNroDocIdent { get;  set;}

        [DataMember]
        public string pTipoServicio { get; set; }
        [DataMember]
        public string pCliNroCuenta { get; set; }
        [DataMember]
        public string pNroTelefono { get; set; }

        [DataMember]
        public string pTipoConsulta { get; set; }
        [DataMember]
        public string pFlagSoloSaldo { get; set; }
        [DataMember]
        public string pFlagSoloDisputa { get; set; }
        [DataMember]
        public string pFechaDesde { get; set; }
        [DataMember]
        public string pFechaHasta { get; set; }
        [DataMember]
        public string pTamanoPagina { get; set; }
        [DataMember]
        public string pNroPagina { get; set; }

    }
}
