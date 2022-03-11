using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common
{
    [DataContract]
    public class CallDetailGeneric
    {
        [DataMember]
        public string VlrNumber { get; set; }

        [DataMember]
        public DateTime StartDateTime { get; set; }
        [DataMember]
        public string StrStartDateTime { get; set; }
        [DataMember]
        public string StrDate { get; set; }

        [DataMember]
        public string StrHour { get; set; }

        [DataMember]
        public Double Quantity { get; set; }

        [DataMember]
        public string Quantity_FormatHHMMSS { get; set; }

        [DataMember]
        public Double FinalAmount { get; set; }

        [DataMember]
        public Double OriginalAmount { get; set; }

        [DataMember]
        public Double OriginalAmount_Flexible { get; set; }

        [DataMember]
        public string Horary { get; set; }

        [DataMember]
        public string Plan { get; set; }

        [DataMember]
        public string TariffPlan { get; set; }

        [DataMember]
        public string Operator { get; set; }
        [DataMember]
        public string Type { get; set; }
        [DataMember]
        public string DestinationPhone { get; set; }
        [DataMember]
        public string CallType { get; set; }
        [DataMember]
        public string Tariff { get; set; }
        [DataMember]
        public string Tariff_Zone { get; set; }
        [DataMember]
        public string CallDuration { get; set; }
        [DataMember]
        public string BalanceSoles { get; set; }
        [DataMember]
        public string Purpose { get; set; }
        [DataMember]
        public string CallDate { get; set; }
        [DataMember]
        public string CallDateTime { get; set; }
        [DataMember]
        public string NumberB { get; set; }
        [DataMember]
        public string ChargeSoles { get; set; }
        [DataMember]
        public string TariffPlanIdCall { get; set; }
        [DataMember]
        public string NumberBOperator { get; set; }

        //public string CallType;
        //public string CallDate;
        //public string CallDateTime;
        //public string NumberB;
        //public string NumberBOperator;
        //public string CallDuration;
        //public string ChargeSoles;
        //public string BalanceSoles;
        //public string TariffPlanIdCall;
        //public string VlrNumber;
        //public string Numbera_area;
        //public string Numberb_area;
        //public string Purpose;
        //public int _NroRegistro;

        //public string VozFidelizacionConsumo;
        //public string VozFidelizacionSaldo;
        //public string Voice1Consumo;
        //public string Voice1Saldo;
        //public string SMSFidelizacionConsumo;
        //public string SMSFidelizacionSaldo;
        //public string MMSConsumo;
        //public string MMSSaldo;

        //public string GPRSConsumo;
        //public string GPRSSaldo;

        //public string GPRSFidelizacionConsumo;
        //public string GPRSFidelizacionSaldo;

        //public string SolesConsumo;
        //public string SolesSaldo;
        //public string SMSConsumo;
        //public string SMSSaldo;
        //public string Voice2Consumo;
        //public string Voice2Saldo;
        //public string Promo1Consumo;
        //public string Promo1Saldo;
        //public string Promo2Consumo;
        //public string Promo2Saldo;
    }
}
