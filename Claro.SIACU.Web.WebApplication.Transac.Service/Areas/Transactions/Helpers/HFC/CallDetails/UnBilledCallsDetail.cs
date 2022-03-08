using Claro.Helpers.Transac.Service;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.HFC.CallDetails
{
    public class UnBilledCallsDetail : IExcel
    {
        [Header(Title = "NRO", Order = Claro.Constants.NumberZero)]
        public int NroRegistro { get; set; }
        [Header(Title = "FECHA", Order = Claro.Constants.NumberOne)]
        public string Fecha_Hora_Inicio { get; set; }
        [Header(Title = "HORA", Order = Claro.Constants.NumberTwo)]
        public string Hora { get; set; }
        [Header(Title = "T.ORIGEN", Order = Claro.Constants.NumberThree)]
        public string Telefono_Origen { get; set; }
        [Header(Title = "T.DESTINO", Order = Claro.Constants.NumberFour)]
        public string Telefono_Destino { get; set; }
        [Header(Title = "CANT.", Order = Claro.Constants.NumberFive)]
        public string Cantidad { get; set; }
        [Header(Title = "COSTO", Order = Claro.Constants.NumberSix)]
        public string Cargo_Original { get; set; }
        public string Cargo_Ori_Flexible { get; set; }

        [Header(Title = "PLAN", Order = Claro.Constants.NumberSeven)]
        public string Plan_Tarifario { get; set; }

        public string Triaciones { get; set; }

        [Header(Title = "TARIFA", Order = Claro.Constants.NumberEight)]
        public string Tarifa { get; set; }
        [Header(Title = "TIPO", Order = Claro.Constants.NumberNine)]
        public string Tipo { get; set; }
        [Header(Title = "ZONA HORARIA", Order = Claro.Constants.NumberTen)]
        public string Zona_Tarifaria { get; set; }
        [Header(Title = "OPERADOR", Order = Claro.Constants.NumberEleven)]
        public string Operador { get; set; }
        [Header(Title = "HORARIO", Order = Claro.Constants.NumberTwelve)]
        public string Horario { get; set; }
        [Header(Title = "T.LLAMADA", Order = Claro.Constants.NumberThirteen)]
        public string Tipo_Llamada { get; set; }
        [Header(Title = "CONSUMO", Order = Claro.Constants.NumberFourteen)]
        public string Cargo_Final { get; set; }
        public string Fecha { get; set; }
        public string Consumo { get; set; }
        public string Tipo_Servicio { get; set; }
        public string Destino { get; set; }

        public string CallType { get; set; }
        public string CallDate { get; set; }
        public string CallDateTime { get; set; }
        public string NumberB { get; set; }
        public string NumberBOperator { get; set; }
        public string CallDuration { get; set; }
        public string ChargeSoles { get; set; }
        public string BalanceSoles { get; set; }
        public string TariffPlanIdCall { get; set; }
        public string VlrNumber { get; set; }
        public string Numbera_area { get; set; }
        public string Numberb_area { get; set; }
        public string Purpose { get; set; }
        public string VozFidelizacionConsumo { get; set; }
        public string VozFidelizacionSaldo { get; set; }
        public string Voice1Consumo { get; set; }
        public string Voice1Saldo { get; set; }
        public string SMSFidelizacionConsumo { get; set; }
        public string SMSFidelizacionSaldo { get; set; }
        public string MMSConsumo { get; set; }
        public string MMSSaldo { get; set; }

        public string GPRSConsumo { get; set; }
        public string GPRSSaldo { get; set; }

        public string GPRSFidelizacionConsumo { get; set; }
        public string GPRSFidelizacionSaldo { get; set; }

        public string SolesConsumo { get; set; }
        public string SolesSaldo { get; set; }
        public string SMSConsumo { get; set; }
        public string SMSSaldo { get; set; }
        public string Voice2Consumo { get; set; }
        public string Voice2Saldo { get; set; }
        public string Promo1Consumo { get; set; }
        public string Promo1Saldo { get; set; }
        public string Promo2Consumo { get; set; }
        public string Promo2Saldo { get; set; }
    }
}