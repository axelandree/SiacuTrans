using Claro.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed
{
    [DbTable("TEMPO")]
    [DataContract(Name = "ProductType")]
   public class ProductType
    {

        [DbColumn("CODIGO_PRODUCTO")]
        [DataMember]
        public string CODIGO_PRODUCTO { get; set; }

        [DbColumn("TIPO_PRODUCTO")]
        [DataMember]
        public string TIPO_PRODUCTO { get; set; }

    }
}
