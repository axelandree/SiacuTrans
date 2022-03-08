using System.Collections.Generic;
using System.Runtime.Serialization;
using HELPER_ITEM = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.Fixed.InfoPromotionPrePostHelper;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.Fixed.InfoPromotionPrePostHelper
{
    public class DataClientHelper
    {
        public string cliId { get; set; }
        public string tipoDoc { get; set; }
        public string tipoDocDesc { get; set; }
        public string nroDocumento { get; set; }
        public string nombresYApellidos { get; set; }
        public string email { get; set; }
        public string tipoCliente { get; set; }
        public string origen { get; set; }
        public string usuarioCrea { get; set; }
        public string fechaCrea { get; set; }
        public string usuarioModi { get; set; }
        public string fechaModi { get; set; }
        public List<HELPER_ITEM.ContactHelper> contactos { get; set; }
        public List<HELPER_ITEM.DetailsHelper> detalle { get; set; }
    }
}