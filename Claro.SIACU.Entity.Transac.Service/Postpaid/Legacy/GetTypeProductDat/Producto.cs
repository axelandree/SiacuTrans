using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.Legacy.GetTypeProductDat
{
    [DataContract]
    public class Producto
    {
        [DataMember(Name = "recursoFisico")]
        public List<RecursoFisico> recursoFisico { get; set; }
        [DataMember(Name = "recursoLogico")]
        public List<RecursoLogico> recursoLogico { get; set; }
        [DataMember(Name = "caracteristicaProducto")]
        public CaracteristicaProducto caracteristicaProducto { get; set; }
    }
}
