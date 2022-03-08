using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Coliving.GetParameter
{
    [DataContract]
    public class ListParameter
    {
        [DataMember(Name = "PARAMETRO_ID")]
        public string strparamerId { get; set; }
        [DataMember(Name = "NOMBRE")]
        public string strNombre { get; set; }
        [DataMember(Name = "DESCRIPCION")]
        public string strDescripcion { get; set; }
        [DataMember(Name = "VALOR_C")]
        public string strValorC { get; set; }
        [DataMember(Name = "VALOR_N")]
        public string strValorN { get; set; }
        [DataMember(Name = "VALOR_L")]
        public string strValorL { get; set; }
        [DataMember(Name = "PERIODO")]
        public string strPeriodo { get; set; }
        [DataMember(Name = "ESTADO")]
        public string strEstado { get; set; }   
    }
}
