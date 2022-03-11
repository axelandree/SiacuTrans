using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Coliving.GetDataHistoryClient
{
    public class ConsultarHistoricoDatosRequest
    {
        public string customerId { get; set; }
        public List<ListOptional> listaOpcional { get; set; }
    }
}
