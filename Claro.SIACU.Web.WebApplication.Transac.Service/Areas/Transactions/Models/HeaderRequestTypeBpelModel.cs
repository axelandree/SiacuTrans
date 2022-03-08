namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models
{
    public class HeaderRequestTypeBpelModel
    {
        public string Canal { get; set; }
        public string IdAplicacion { get; set; }
        public string UsuarioAplicacion { get; set; }
        public string UsuarioSesion { get; set; }
        public string IdTransaccionEsb { get; set; }
        public string IdTransaccionNegocio { get; set; }
        public System.DateTime FechaInicio { get; set; }
        public object NodoAdicional { get; set; }
    }
}