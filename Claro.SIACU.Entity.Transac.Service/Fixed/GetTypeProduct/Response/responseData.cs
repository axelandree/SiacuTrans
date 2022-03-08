using Claro.SIACU.Entity.Transac.Service.Fixed.GetTypeProduct.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetTypeProduct.Response
{
   public class responseData
    {
        public string lineaContrato { get; set; }
        public List<listaTipoProducto> listaTipoProducto { get; set; }
        public List<listaOpcional> listaOpcional { get; set; }
    }
}
