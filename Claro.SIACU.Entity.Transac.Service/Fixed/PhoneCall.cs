using System;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed
{
    [DataContract]
    public class PhoneCall
    {
        [DataMember]
        public DateTime Fecha_Hora_Inicio { get; set; }
        [DataMember]
        public string Telefono_Destino { get; set; }
        [DataMember]
        public string Telefono_Origen { get; set; }
        [DataMember]
        public string Cantidad { get; set; }
        [DataMember]
        public string Cargo_Original { get; set; }
        [DataMember]
        public string Cargo_Ori_Flexible { get; set; }
        [DataMember]
        public string Plan_Tarifario { get; set; }
        [DataMember]
        public string Triaciones { get; set; }
        [DataMember]
        public string Tarifa { get; set; }
        [DataMember]
        public string Tipo { get; set; }
        [DataMember]
        public string Zona_Tarifaria { get; set; }
        [DataMember]
        public string Operador { get; set; }
        [DataMember]
        public string Horario { get; set; }
        [DataMember]
        public string Tipo_Llamada { get; set; }
        [DataMember]
        public string Cargo_Final { get; set; }
        [DataMember]
        public string Fecha { get; set; }
        [DataMember]
        public string Hora { get; set; }
        [DataMember]
        public string Consumo { get; set; }
        [DataMember]
        public string Tipo_Servicio { get; set; }
        [DataMember]
        public string Destino { get; set; }
        [DataMember]
        public string CallType { get; set; }
        [DataMember]
        public string CallDate { get; set; }
        [DataMember]
        public string CallDateTime { get; set; }
        [DataMember]
        public string NumberB { get; set; }
        [DataMember]
        public string NumberBOperator { get; set; }
        [DataMember]
        public string CallDuration { get; set; }
        [DataMember]
        public string ChargeSoles { get; set; }
        [DataMember]
        public string BalanceSoles { get; set; }
        [DataMember]
        public string TariffPlanIdCall { get; set; }
        [DataMember]
        public string VlrNumber { get; set; }
        [DataMember]
        public string Numbera_area { get; set; }
        [DataMember]
        public string Numberb_area { get; set; }
        [DataMember]
        public string Purpose { get; set; }
        [DataMember]
        public int NroRegistro { get; set; }
        [DataMember]
        public string VozFidelizacionConsumo { get; set; }
        [DataMember]
        public string VozFidelizacionSaldo { get; set; }
        [DataMember]
        public string Voice1Consumo { get; set; }
        [DataMember]
        public string Voice1Saldo { get; set; }
        [DataMember]
        public string SMSFidelizacionConsumo { get; set; }
        [DataMember]
        public string SMSFidelizacionSaldo { get; set; }
        [DataMember]
        public string MMSConsumo { get; set; }
        [DataMember]
        public string MMSSaldo { get; set; }
        [DataMember]
        public string GPRSConsumo { get; set; }
        [DataMember]
        public string GPRSSaldo { get; set; }
        [DataMember]
        public string GPRSFidelizacionConsumo { get; set; }
        [DataMember]
        public string GPRSFidelizacionSaldo { get; set; }
        [DataMember]
        public string SolesConsumo { get; set; }
        [DataMember]
        public string SolesSaldo { get; set; }
        [DataMember]
        public string SMSConsumo { get; set; }
        [DataMember]
        public string SMSSaldo { get; set; }
        [DataMember]
        public string Voice2Consumo { get; set; }
        [DataMember]
        public string Voice2Saldo { get; set; }
        [DataMember]
        public string Promo1Consumo { get; set; }
        [DataMember]
        public string Promo1Saldo { get; set; }
        [DataMember]
        public string Promo2Consumo { get; set; }
        [DataMember]
        public string Promo2Saldo { get; set; }
    }
}
