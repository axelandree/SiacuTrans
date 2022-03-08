using System.Runtime.Serialization;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.Postpaid
{
    public class SiacParametroVM
    {
        public int Parametro_ID { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Tipo { get; set; }
        public string Valor_C { get; set; }
        public decimal Valor_N { get; set; }
        public string Valor_L { get; set; }
    }
}