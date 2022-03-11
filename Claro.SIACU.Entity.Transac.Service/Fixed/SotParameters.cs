using System.Runtime.Serialization;
using Claro.Data;
namespace Claro.SIACU.Entity.Transac.Service.Fixed
{
    [DataContract(Name = "SotParameters")]
    public class SotParameters
    {
        [DataMember]
        public string strCoId { get; set; }
        [DataMember]
        public string strPId { get; set; }
        [DataMember]
        public string strCustomerId { get; set; }
        [DataMember]
        public string strTransTipo { get; set; }
        [DataMember]
        public string strFechaProgramada { get; set; }
        [DataMember]
        public string strFranjaHoraria { get; set; }
        [DataMember]
        public string strNumCarta { get; set; }
        [DataMember]
        public string strObservacion { get; set; }
        [DataMember]
        public string strUsrRegistro { get; set; }
        [DataMember]
        public string strOperador { get; set; }
        [DataMember]
        public string strPresuscrito { get; set; }
        [DataMember]
        public string strPublicar { get; set; }
        [DataMember]
        public string strTmCode { get; set; }
        [DataMember]
        public string strTipoVia { get; set; }
        [DataMember]
        public string strNombreVia { get; set; }
        [DataMember]
        public string strNumeroVia { get; set; }
        [DataMember]
        public string strTipoUrbanizacion { get; set; }
        [DataMember]
        public string strNombreUrbanizacion { get; set; }
        [DataMember]
        public string strManzana { get; set; }
        [DataMember]
        public string strLote { get; set; }
        [DataMember]
        public string strCodUbigeo { get; set; }
        [DataMember]
        public string strCodZona { get; set; }
        [DataMember]
        public string strIdPlano { get; set; }
        [DataMember]
        public string strCodEdif { get; set; }
        [DataMember]
        public string strReferencia { get; set; }
        [DataMember]
        public string strCargo { get; set; }
        [DataMember]
        public string strCodMotot { get; set; }
        [DataMember]
        public string strTiptra { get; set; }
        [DataMember]
        public string strFranja { get; set; }
        [DataMember]
        public string strFranjaHor { get; set; }
    }
}
