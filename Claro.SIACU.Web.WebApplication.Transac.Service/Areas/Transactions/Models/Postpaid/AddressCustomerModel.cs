using System;
using Claro.SIACU.Web.WebApplication.Transac.Service;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models.Postpaid
{
    public class AddressCustomerModel
    {
        public string strSessionId { get; set; }   
        public string strCustomerId { get; set; }        
        public string strDireccion { get; set; }
        public string strReferencia { get; set; }
        public string strPais { get; set; }
        public string strDepartamento { get; set; }
        public string strProvincia { get; set; }
        public string strDistrito { get; set; }
        public string strCodPostal { get; set; }
        public string strMotivo { get; set; }
        public string strTipo { get; set; }
        public int intSeqIn { get; set; }
        public string FLAG_LEGAL { get; set; }
        public string FLAG_FACTURACION { get; set; }


        public string strStreet { get; set; }
        public string csIdPub { get; set; }
        public string userId { get; set; }

        public string adrStreetNo { get; set; }
        public string adrSeq { get; set; }
        public string adrStreet { get; set; }
        public string adrJbdes { get; set; }
        public string  adrNote1 { get; set; }
        public string adrNote2 { get; set; }
        public string adrNote3 { get; set; }
    }
}