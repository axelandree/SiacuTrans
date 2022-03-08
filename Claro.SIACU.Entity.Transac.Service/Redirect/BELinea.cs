using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Redirect
{
    [DataContract]
    public class BELinea
    {
        public BELinea() { }

        private DateTime _Fecha_Estado;


        [DataMember(Name = "CellPhone")]
        public string NroCelular { get; set; }

        [DataMember(Name = "StateLine")]
        public string StatusLinea { get; set; }

        public string SaldoPrincipal { get; set; }
        public string FechaExpiracionSaldo { get; set; }
        public string CambiosTriosGratis { get; set; }
        public string CambiosTarifaGratis { get; set; }

        [DataMember(Name = "PlanTariff")]
        public string PlanTarifario { get; set; }

        [DataMember(Name = "ActivationDate")]
        public string FecActivacion { get; set; }

        public string FecDol { get; set; }
        public string FecExpLinea { get; set; }

        [DataMember(Name = "NumberIMSI")]
        public string NroIMSI { get; set; }

        public string StatusIMSI { get; set; }

        [DataMember(Name = "NumberICCID")]
        public string NroICCID { get; set; }

        public string NroFamAmigos { get; set; }
        public string TipoTriacion { get; set; }

        [DataMember(Name = "ProviderID")]
        public string ProviderID { get; set; }

        public DateTime Fecha_Estado { get { return _Fecha_Estado; } set { _Fecha_Estado = value; } }

        [DataMember(Name = "Reason")]
        public string Motivo { get; set; }

        [DataMember(Name = "Plan")]
        public string Plan { get; set; }

        [DataMember(Name = "TermContract")]
        public string Plazo_Contrato { get; set; }

        [DataMember(Name = "Seller")]
        public string Vendedor { get; set; }

        [DataMember(Name = "Campaign")]
        public string Campana { get; set; }

        [DataMember(Name = "Introduced")]
        public DateTime Introducido_El { get; set; }

        [DataMember(Name = "IntroducedBy")]
        public string Introducido_Por { get; set; }

        [DataMember(Name = "ValidFrom")]
        public DateTime Valido_Desde { get; set; }

        [DataMember(Name = "ChangedBy")]
        public string Cambiado_Por { get; set; }

        [DataMember(Name = "FlagPlatform")]
        public string Flag_Plataforma { get; set; }

        [DataMember(Name = "PIN1")]
        public string PIN1 { get; set; }

        [DataMember(Name = "PIN2")]
        public string PIN2 { get; set; }

        [DataMember(Name = "PUK1")]
        public string PUK1 { get; set; }

        [DataMember(Name = "PUK2")]
        public string PUK2 { get; set; }

        [DataMember(Name = "ContractID")]
        public string ContratoID { get; set; }

        [DataMember(Name = "CodePlanTariff")]
        public string Cod_Plan_Tarifario { get; set; }
        public string coId { get; set; }
        public string CNTNumber { get; set; }
        public string IsCNTPossible { get; set; }
        public string SubscriberStatus { get; set; }
        public string fechaEstado { get; set; }

        [DataMember(Name = "StateAgreement")]
        public string Estado_Acuerdo { get; set; }
        public string Fecha_Fin_Acuerdo { get; set; }

        [DataMember(Name = "FlagTFI")]
        public string Flag_TFI { get; set; }
        public string ptoVenta { get; set; }

        [DataMember(Name = "MSISDN")]
        public string msisdn { get; set; }

        [DataMember(Name = "InternetValue")]
        public string internet { get; set; }

        [DataMember(Name = "TelephonyValue")]
        public string telefonia { get; set; }

        [DataMember(Name = "CableValue")]
        public string cableTv { get; set; }

        [DataMember(Name = "IsLTE")]
        public bool EsLTE { get; set; }

        [DataMember(Name = "TypeProduct")]
        public string tipoProducto { get; set; }
        public string tipoServicio { get; set; }

        [DataMember(Name = "StateDate")]
        public string Fecha_Estado_Str
        {
            set
            {
                DateTime.TryParse(value, out _Fecha_Estado);
            }
            get { return _Fecha_Estado.ToString(); }
        }

        [DataMember(Name = "IsNot3Play")]
        public string NoEs3Play { get; set; }
    }
}
