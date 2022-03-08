using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Claro.SIACU.Entity.Transac.Service.Coliving;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetTypeProductDat
{
    [DataContract]
    public class GetTypeProductDatRequest : HeaderToBe
    {
        public class RecursoLogico
        {
            [DataMember]
            public string numeroLinea { get; set; }

        }
        public class Producto
        {
            [DataMember]
            public RecursoLogico recursoLogico { get; set; }

        }
        public class OfertaProducto
        {
            [DataMember]
            public Producto producto { get; set; }

        }
        public class CaracteristicaAdicional
        {
            [DataMember]
            public string descripcion { get; set; }
            [DataMember]
            public string valor { get; set; }

        }
        public class Contrato
        {
            [DataMember]
            public string idContrato { get; set; }
            [DataMember]
            public OfertaProducto ofertaProducto { get; set; }
            [DataMember]
            public CaracteristicaAdicional caracteristicaAdicional { get; set; }

        }

        [DataMember]
        public Contrato contrato { get; set; }
    }
}
