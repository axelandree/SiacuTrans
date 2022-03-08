using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.CommonServices
{
    [Serializable]
    public class TimeZoneVM
    {
        public string vUbigeo { get; set; }
        public string vJobTypes { get; set; }
        public string vCommitmentDate { get; set; }
        public string vSubJobTypes { get; set; }
        public string vValidateETA { get; set; }
        public string vHistoryETA { get; set; }

        public string vTimeZone { get; set; }
        public string vMotiveSot { get; set; }
        public string vIdTipoServicio { get; set; }
        public string vCantidad { get; set; }
        public string vValidationAbbreviation { get; set; }
        public string vContractID { get; set; }
    }
}