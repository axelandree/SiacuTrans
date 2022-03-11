using System.Runtime.Serialization;


namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetOrderSubType
{
    [DataContract(Name = "OrderSubTypesRequestHfc")]
    public class OrderSubTypesRequest : Claro.Entity.Request
    {
        [DataMember]
        public string av_cod_tipo_orden { get; set; }
        [DataMember]
        public string av_cod_tipo_trabajo { get; set; }
        [DataMember]
        public string av_cod_contrato { get; set; }

    }
}
