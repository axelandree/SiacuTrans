using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo
{
    [DataContract(Name = "pEstadoPagoServ")]
    public class PEstadoPagoServ
    {
        [DataMember(Name = "nombreServicio")]
        public string nombreServicio { get; set; }

        [DataMember(Name = "descripcionServicio")]
        public string descripcionServicio { get; set; }

        [DataMember(Name = "fechaAct")]
        public string fechaAct { get; set; }

        [DataMember(Name = "fehaExp")]
        public string fehaExp { get; set; }

        [DataMember(Name = "diasPromo")]
        public string diasPromo { get; set; }

        [DataMember(Name = "servicioPrecio")]
        public string servicioPrecio { get; set; }

        [DataMember(Name = "estadoPago")]
        public string estadoPago { get;set;}
    }
}
