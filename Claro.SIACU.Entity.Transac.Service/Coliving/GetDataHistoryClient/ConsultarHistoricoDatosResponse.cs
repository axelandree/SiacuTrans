using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Coliving.GetDataHistoryClient
{
    public class ConsultarHistoricoDatosResponse
    {
        public AuditResponse responseAudit { get; set; }
        public DataResponse responseData { get; set; }
    }
}
