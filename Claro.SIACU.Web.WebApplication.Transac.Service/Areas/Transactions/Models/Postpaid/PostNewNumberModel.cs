using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models.Postpaid
{
    public class PostNewNumberModel
    {
        public string SESSION { get; set; }
        public string CONTRACT { get; set; }
        public string ACCOUNT { get; set; }
        public string CONTACTOBJID { get; set; }
        public string TRANSACTION { get; set; }
        public string CUSTOMER_TYPE { get; set; }
        public string TYPE { get; set; }
        public string CLASS { get; set; }
        public string SUB_CLASS { get; set; }
        public string CURRENT_PHONE { get; set; }
        public string NEW_PHONE { get; set; }
        public string TRANSACTION_TYPE { get; set; }
        public string LOCATION { get; set; }
        public string START_HLR { get; set; }
        public string END_HLR { get; set; }
        public string CHIP { get; set; }
        public string FIDELIZE { get; set; }
        public string COST { get; set; }
        public string SALE_POINT { get; set; }
        public string NOTES { get; set; }
        public string IGV { get; set; }
        public string USER { get; set; }

        public string TITRE { get; set; }
        public string LEGALREP { get; set; }
        public string DOCUMENT_TYPE { get; set; }
        public string DOCUMENT { get; set; }
        public string PROGRAM_DATE { get; set; }
        public string ACTION { get; set; }

    }
}