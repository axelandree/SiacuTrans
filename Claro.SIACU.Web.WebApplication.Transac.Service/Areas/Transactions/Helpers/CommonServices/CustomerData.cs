using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.CommonServices
{
    public class CustomerData
    {
        public string Type { get; set; }
        public string Account { get; set; }
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Telephone { get; set; }
        public string IdContact { get; set; }
        public string LegalRepresentative { get; set; }
        public string DocumentType { get; set; }
        public string NumberDocument { get; set; }
    }
}