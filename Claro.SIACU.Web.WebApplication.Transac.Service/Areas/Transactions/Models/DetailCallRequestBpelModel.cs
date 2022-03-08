namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models
{
    public class DetailCallRequestBpelModel
    {
        public string TipoConsulta { get; set; }
        public string CodigoCliente { get; set; }
        public string Msisdn { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
        public ContactUserBpelModel ContactUserBpelModel { get; set; }
        public CustomerClfyBpelModel CustomerClfyBpelModel { get; set; }
        public InteractionBpelModel InteractionBpelModel { get; set; }
        public InteractionPlusBpelModel InteractionPlusBpelModel { get; set; }
        public string FlagContingencia { get; set; }
        public string TipoProducto { get; set; }
        public string Periodo { get; set; }
        public string IpCliente { get; set; }
        public string FlagEnvioCorreo { get; set; }
        public string FlagConstancia { get; set; }
        public string FlagGenerarOcc { get; set; }
        public string InvoiceNumber { get; set; }
        public string TipoConsultaContrato { get; set; }
        public string ValorContrato { get; set; }
    }
}