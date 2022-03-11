using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed
{
    [DataContract]
    public class Decoder
    {
        [DataMember]
        public string codigo_material { get; set; }
        [DataMember]
        public string codigo_sap { get; set; }
        [DataMember]
        public string numero_serie { get; set; }
        [DataMember]
        public string macadress { get; set; }
        [DataMember]
        public string descripcion_material { get; set; }
        [DataMember]
        public string abrev_material { get; set; }
        [DataMember]
        public string estado_material { get; set; }
        [DataMember]
        public string precio_almacen { get; set; }
        [DataMember]
        public string codigo_cuenta { get; set; }
        [DataMember]
        public string componente { get; set; }
        [DataMember]
        public string centro { get; set; }
        [DataMember]
        public string idalm { get; set; }
        [DataMember]
        public string almacen { get; set; }
        [DataMember]
        public string tipo_equipo { get; set; }
        [DataMember]
        public string id_producto { get; set; }
        [DataMember]
        public string id_cliente { get; set; }
        [DataMember]
        public string modelo { get; set; }
        [DataMember]
        public string convertertype { get; set; }
        [DataMember]
        public string servicio_principal { get; set; }
        [DataMember]
        public string headend { get; set; }
        [DataMember]
        public string ephomeexchange { get; set; }
        [DataMember]
        public string numero { get; set; }
        [DataMember]
        public string tipoServicio { get; set; }
        [DataMember]
        public string TIPODECO { get; set; }
        [DataMember]
        public string CARGO_FIJO { get; set; }
        [DataMember]
        public string PORCENTAJE_IGV { get; set; }
    }
}
