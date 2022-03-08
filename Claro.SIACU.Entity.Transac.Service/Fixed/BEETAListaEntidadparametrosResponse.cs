using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Claro.SIACU.Entity.Transac.Service.Fixed
{
    public class BEETAListaEntidadparametrosResponse
    {
        public List<BEETAEntidadparametrosResponse> ListEntidadparametrosResponse { get; set; }
        public BEETAListaEntidadparametrosResponse()
        {
            ListEntidadparametrosResponse = new List<BEETAEntidadparametrosResponse>();
        }
    }
}
