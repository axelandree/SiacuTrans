using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid
{
    [Data.DbTable("TiME")]
    [DataContract(Name = "ServicePostPaid")]
    public class Service
    {
        [DataMember]
        public string CoID { get; set; }
        [DataMember]
        public string Company { get; set; }
        [DataMember]
        public DateTime DateState { get; set; }
        [DataMember]
        [Data.DbColumn("ESTADO")]
        public string StateLine { get; set; }
        [DataMember]
        public string Reason { get; set; }
        [DataMember]
        [Data.DbColumn("PLAN_TARIFARIO")]
        public string Plan { get; set; }
        [DataMember]
        public string TermContract { get; set; }
        [DataMember]
        public string NroICCID { get; set; }
        [DataMember]
        public string NroIMSI { get; set; }
        [DataMember]
        public string Seller { get; set; }
        [DataMember]
        public string DateActivation { get; set; }
        [DataMember]
        public string DateDeactivation { get; set; }
        [DataMember]
        public string FlagPlatform { get; set; }
        [DataMember]
        public string PIN1 { get; set; }

        [DataMember]
        public string PIN2 { get; set; }

        [DataMember]
        public string PUK1 { get; set; }

        [DataMember]
        public string PUK2 { get; set; }

        [DataMember]
        public string CodPlanTariff { get; set; }
        [DataMember]
        public string PlanTariff { get; set; }

        [Data.DbColumn("MSISDN")]
        [DataMember]
        public string NroCellPhone { get; set; }

        [DataMember]
        [Data.DbColumn("CO_ID")]
        public string ContractID { get; set; }

        [DataMember]
        public string CodReturn { get; set; }

        [DataMember]
        public string ProviderID { get; set; }

        [DataMember]
        public string MSISDN { get; set; }

        [DataMember]
        public string BancaMovil { get; set; }

        [DataMember]
        public string TypeSolution { get; set; }


        [DataMember]
        [Data.DbColumn("TIPO_PROD")]
        public string TypeProduct { get; set; }

        [DataMember]
        public string StopConsumption { get; set; }


        [DataMember]
        [Data.DbColumn("TELEFONIA")]
        public string Telophony { get; set; }


        [DataMember]
        [Data.DbColumn("INTERNET")]
        public string Internet { get; set; }


        [DataMember]
        [Data.DbColumn("CABLE_TV")]
        public string CableTV { get; set; }

        [DataMember]
        public string Application { get; set; }

        [DataMember]
        public string ServicePackage { get; set; }

        [DataMember]
        public bool State_ServicePackage { get; set; }

        [DataMember]
        public string FlagTFI { get; set; }

        [DataMember]
        public string TFI { get; set; }

        [DataMember]
        public bool StateTFI { get; set; }


        [DataMember]
        public string Sale { get; set; }

        [DataMember]
        public string ServiceCombo { get; set; }

        [DataMember]
        public bool StateServiceCombo { get; set; }

        [DataMember]
        public DateTime IntroducedThe { get; set; }

        [DataMember]
        public string IntroducedBy { get; set; }

        [DataMember]
        public DateTime ValidFrom { get; set; }
        [DataMember]
        public string ChangedBy { get; set; }

        [DataMember]
        public string NoEs3Play { get; set; }

        [DataMember]
        public string Package { get; set; }

        [DataMember]
        public string PackageID { get; set; }

        [DataMember]
        public string Quota { get; set; }

        [DataMember]
        public string MainBalance { get; set; }

        [DataMember]
        public string DateBalanceExpiration { get; set; }

        [DataMember]
        public string FreeThreesomeChanges { get; set; }

        [DataMember]
        public string ChangeFreeRate { get; set; }

        [DataMember]
        public string DateDol { get; set; }

        [DataMember]
        public string DateExpirationLine { get; set; }

        [DataMember]
        public string SubscribeState { get; set; }

        [DataMember]
        public string CNTNumber { get; set; }

        [DataMember]
        public string CNTPossible { get; set; }

        [DataMember]
        public string StateIMSI { get; set; }

        [DataMember]
        public string TypeTriacion { get; set; }

        [DataMember]
        public string NumberFamilyFriends { get; set; }

        [DataMember]
        public string CodResponse { get; set; }

        //[DataMember]
        //public List<Account> ListAccount { get; set; }

        //[DataMember]
        //public List<Trio> ListTrio { get; set; }

        [DataMember]
        public bool Portability { get; set; }

        [DataMember]
        public string PortabilityState { get; set; }

        [DataMember]
        public bool StateDTH { get; set; }

        [DataMember]
        public bool Roaming { get; set; }
    }
}
