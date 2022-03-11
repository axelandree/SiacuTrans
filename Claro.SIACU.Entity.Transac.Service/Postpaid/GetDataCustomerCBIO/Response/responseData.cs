using Claro.SIACU.Entity.Transac.Service.Postpaid.GetDataCustomerCBIO.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetDataCustomerCBIO.Response
{
    public class responseData
    {
        public clienteData clienteData { get; set; }
        public cuentaData cuentaData { get; set; }
        public List<listaOpcional> listaOpcional { get; set; }

    }
}
