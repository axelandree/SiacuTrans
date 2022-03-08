using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.Redirect
{
    [DataContract]
    public class Linea
    {
        private string _NroCelular;
        private string _StatusLinea;
        private string _SaldoPrincipal;
        private string _FechaExpiracionSaldo;
        private string _CambiosTriosGratis;
        private string _CambiosTarifaGratis;
        private string _PlanTarifario;
        private string _FecActivacion;
        private string _FecDol;
        private string _FecExpLinea;
        private string _NroIMSI;
        private string _StatusIMSI;
        private string _NroICCID;
        private string _NroFamAmigos;
        private string _TipoTriacion;
        private string _ProviderID;

        private DateTime _Fecha_Estado;
        private string _Motivo;
        private string _Plan;
        private string _Plazo_Contrato;
        private string _Vendedor;
        private string _Campana;
        private DateTime _Introducido_El;
        private string _Introducido_Por;
        private DateTime _Valido_Desde;
        private string _Cambiado_Por;
        private string _Flag_Plataforma;
        private string _PIN1;
        private string _PIN2;
        private string _PUK1;
        private string _PUK2;
        private string _ContratoID;
        private string _Cod_Plan_Tarifario;

        private string _CNTNumber;
        private string _IsCNTPossible;
        private string _SubscriberStatus;
        private string _PlanRate;

        private string _Estado_Acuerdo;
        private string _Fecha_Fin_Acuerdo;
        private string _Flag_TFI;
        private string _EsTFI;

        private ArrayList _ServicioPlanCombo;

        private string _Cuenta;
        private string _FechaDesactivacion;
        private string _Tickler_Code;
        private string _Fecha_EstadoString;

        public Linea()
        {
        }

        [DataMember(Name = "CodePlanTariff")]
        public string Cod_Plan_Tarifario
        {
            set { _Cod_Plan_Tarifario = value; }
            get { return _Cod_Plan_Tarifario; }
        }

        [DataMember(Name = "ContractID")]
        public string ContratoID
        {
            set { _ContratoID = value; }
            get { return _ContratoID; }
        }

        [DataMember(Name = "PUK1")]
        public string PUK1
        {
            set { _PUK1 = value; }
            get { return _PUK1; }
        }

        [DataMember(Name = "PUK2")]
        public string PUK2
        {
            set { _PUK2 = value; }
            get { return _PUK2; }
        }

        [DataMember(Name = "PIN1")]
        public string PIN1
        {
            set { _PIN1 = value; }
            get { return _PIN1; }
        }

        [DataMember(Name = "PIN2")]
        public string PIN2
        {
            set { _PIN2 = value; }
            get { return _PIN2; }
        }

        [DataMember(Name = "FlagPlatform")]
        public string Flag_Plataforma
        {
            set { _Flag_Plataforma = value; }
            get { return _Flag_Plataforma; }
        }
        public string Cambiado_Por
        {
            set { _Cambiado_Por = value; }
            get { return _Cambiado_Por; }
        }
        public DateTime Valido_Desde
        {
            set { _Valido_Desde = value; }
            get { return _Valido_Desde; }
        }
        public DateTime Introducido_El
        {
            set { _Introducido_El = value; }
            get { return _Introducido_El; }
        }
        public string Introducido_Por
        {
            set { _Introducido_Por = value; }
            get { return _Introducido_Por; }
        }

        public DateTime Fecha_Estado
        {
            set { _Fecha_Estado = value; }
            get { return _Fecha_Estado; }
        }

        [DataMember(Name = "Reason")]
        public string Motivo
        {
            set { _Motivo = value; }
            get { return _Motivo; }
        }

        [DataMember(Name = "Plan")]
        public string Plan
        {
            set { _Plan = value; }
            get { return _Plan; }
        }

        [DataMember(Name = "TermContract")]
        public string Plazo_Contrato
        {
            set { _Plazo_Contrato = value; }
            get { return _Plazo_Contrato; }
        }

        [DataMember(Name = "Seller")]
        public string Vendedor
        {
            set { _Vendedor = value; }
            get { return _Vendedor; }
        }


        [DataMember(Name = "Campaign")]
        public string Campana
        {
            set { _Campana = value; }
            get { return _Campana; }
        }

        [DataMember(Name = "CellPhone")]
        public string NroCelular
        {
            set { _NroCelular = value; }
            get { return _NroCelular; }
        }

        [DataMember(Name = "StateLine")]
        public string StatusLinea
        {
            set { _StatusLinea = value; }
            get { return _StatusLinea; }
        }
        public string SaldoPrincipal
        {
            set { _SaldoPrincipal = value; }
            get { return _SaldoPrincipal; }
        }
        public string FechaExpiracionSaldo
        {
            set { _FechaExpiracionSaldo = value; }
            get { return _FechaExpiracionSaldo; }
        }
        public string CambiosTriosGratis
        {
            set { _CambiosTriosGratis = value; }
            get { return _CambiosTriosGratis; }
        }
        public string CambiosTarifaGratis
        {
            set { _CambiosTarifaGratis = value; }
            get { return _CambiosTarifaGratis; }
        }
        public string PlanTarifario
        {
            set { _PlanTarifario = value; }
            get { return _PlanTarifario; }
        }
        public string ProviderID
        {
            set { _ProviderID = value; }
            get { return _ProviderID; }
        }

        [DataMember(Name = "ActivationDate")]
        public string FecActivacion
        {
            set { _FecActivacion = value; }
            get { return _FecActivacion; }
        }
        public string FecDol
        {
            set { _FecDol = value; }
            get { return _FecDol; }
        }
        public string FecExpLinea
        {
            set { _FecExpLinea = value; }
            get { return _FecExpLinea; }
        }

        [DataMember(Name = "NumberIMSI")]
        public string NroIMSI
        {
            set { _NroIMSI = value; }
            get { return _NroIMSI; }
        }
        public string StatusIMSI
        {
            set { _StatusIMSI = value; }
            get { return _StatusIMSI; }
        }

        [DataMember(Name = "NumberICCID")]
        public string NroICCID
        {
            set { _NroICCID = value; }
            get { return _NroICCID; }
        }
        public string NroFamAmigos
        {
            set { _NroFamAmigos = value; }
            get { return _NroFamAmigos; }
        }
        public string TipoTriacion
        {
            set { _TipoTriacion = value; }
            get { return _TipoTriacion; }
        }

        public string CNTNumber
        {
            set { _CNTNumber = value; }
            get { return _CNTNumber; }
        }
        public string IsCNTPossible
        {
            set { _IsCNTPossible = value; }
            get { return _IsCNTPossible; }
        }
        public string SubscriberStatus
        {
            set { _SubscriberStatus = value; }
            get { return _SubscriberStatus; }
        }
        public string PlanRate
        {
            set { _PlanRate = value; }
            get { return _PlanRate; }
        }
        [DataMember(Name = "StateAgreement")]
        public string Estado_Acuerdo
        {
            set { _Estado_Acuerdo = value; }
            get { return _Estado_Acuerdo; }
        }
        public string Fecha_Fin_Acuerdo
        {
            set { _Fecha_Fin_Acuerdo = value; }
            get { return _Fecha_Fin_Acuerdo; }
        }

        [DataMember(Name = "FlagTFI")]
        public string Flag_TFI
        {
            set { _Flag_TFI = value; }
            get { return _Flag_TFI; }
        }
        public ArrayList ServicioPlanCombo
        {
            set { _ServicioPlanCombo = value; }
            get { return _ServicioPlanCombo; }
        }

        [DataMember(Name = "ISTFI")]
        public string EsTFI
        {
            set { _EsTFI = value; }
            get { return _EsTFI; }
        }

        [DataMember(Name = "Account")]
        public string Cuenta
        {
            set { _Cuenta = value; }
            get { return _Cuenta; }
        }
        [DataMember(Name = "DeactivationDate")]
        public string FechaDesactivacion
        {
            set { _FechaDesactivacion = value; }
            get { return _FechaDesactivacion; }
        }
        public string Tickler_Code
        {
            set { _Tickler_Code = value; }
            get { return _Tickler_Code; }
        }

        #region Redireccionamiento SU

        [DataMember(Name = "StateDate")]
        protected string Fecha_EstadoString
        {
            set
            {
                _Fecha_EstadoString = value;
                DateTime.TryParse(_Fecha_EstadoString, out _Fecha_Estado);
            }
            get { return _Fecha_Estado.ToString(); }
        }

        #endregion


    }
}