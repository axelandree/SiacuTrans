namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models
{
    public class ConsumptionTop
    {
        public string Telefono { get; set; }
        public string Accion { get; set; }
        public string Ciclo { get; set; }
        public string ContratoId { get; set; }
        public string CodServ { get; set; }
        public string DesServ { get; set; }
        public string TipoRegistro { get; set; }
        public string Tope { get; set; }
        public string TopeConsumoId { get; set; }
        public string TopeConsumoDesc { get; set; }
        public string FlagTopeMenor { get; set; }
        public string FlagLc { get; set; }
        public string FlagTeleventas { get; set; }
        public string FlagTipifica { get; set; }
        public string FlagValidacion { get; set; }
        public string FechaRegistro { get; set; }
        public string FechaModificacion { get; set; }
        public string FechProgramacion { get; set; }
        public string Plataforma { get; set; }
        public string Comentario { get; set; }
        public string CostoFijo { get; set; }
        public string ConsumoActual { get; set; }
        public string BolsaAdicional { get; set; }
        public string Plan { get; set; }
        public string Usuario { get; set; }


        /*
         * CONSULTA DE PROGRAMACIONES
          Msisdn,
               FechaDesde,
               FechaHasta,
               Estado,
               Asesor,
               Cuenta,
               TipoTransaccion,
               CodInteraccion,
               CadDac,
               TipoServicio,
               SerCod,
               listaRequestOpcional}
        */
        public string FechaDesde { get; set; }
        public string FechaHasta { get; set; }
        public string Estado { get; set; }
        public string Asesor { get; set; }
        public string Cuenta { get; set; }
        public string TipoTransaccion { get; set; }
        public string CodInteraccion { get; set; }
        public string CadCac { get; set; }
        public string TipoServicio { get; set; }  
    }
}