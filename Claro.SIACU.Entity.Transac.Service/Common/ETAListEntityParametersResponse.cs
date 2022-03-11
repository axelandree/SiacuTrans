using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common
{
    public class ETAListEntityParametersResponse
    {
        public List<ETAListEntityParametersResponse> ListEntityParametersResponse { get; set; }
        public ETAListEntityParametersResponse()
        {
            ListEntityParametersResponse = new List<ETAListEntityParametersResponse>();
        }
    }
}
