using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.Fixed.DiscardHelper
{
    public class DiscardListValueHelper
    {      
        public string nombre { get; set; }       
        public string valor { get; set; }        
        public string medida { get; set; }      
        public string fechaVencimiento { get; set; }    
        public string esCabecera { get; set; }
        public string idDescarte { get; set; } //INICIATIVA-871
    }
}