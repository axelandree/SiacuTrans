using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.Prepaid.IncomingCallsDetail
{
    public class Line
    { 
        //public string PhoneNumber { get; set; } 
        //public string LineStatus { get; set; } 
        //public string MainBalance { get; set; }
        public string FlagLoadDataLine { get; set; }

        public string TriosChanguesFree { get; set; }
        public int NumberFailedRecharges { get; set; }
        public int NumberTariffChangesMade { get; set; }
        public int IdTriacionType { get; set; }
        public string StrActivationDate { get; set; }

        public string StrNumIMSI { get; set; }
        public string StrNumICCID { get; set; }
        public string StatusIMSI { get; set; }

        public string LineStatus { get; set; }
        public string ProviderIdPlan { get; set; }
        public string TariffPlan { get; set; }
        public string MainBalance { get; set; }
        public string MinutesBalance { get; set; }
        public string ExpDate_Select { get; set; }

        public string IsSelect { get; set; }
        public string SubscriberStatus { get; set; }

        public string StrMainBalance { get; set; }
        public string StrMainDate { get; set; }

        public string IsTFI { get; set; }

        public Item BalanceVoice1Promo { get; set; }
        public Item BalanceVoice2Promo { get; set; }
        public Item BalanceBonusPromo { get; set; }
        public Item BalanceVoiceLoyalty { get; set; }
        public Item BalanceBonus1Promo { get; set; }
        public Item BalanceBonus2Promo { get; set; }
   

        public string StandartGroupsFrequentNumbers { get; set; }
        public string StandartGroupsSMSNumbers { get; set; }
        public string StandartGroupsTrio { get; set; }
        public string StandartGroupsAccounts { get; set; }

        //public List<Item> ListFrequentNumbers { get; set; }
        public List<Item> ListTrio { get; set; }
        public List<Item> ListTriadoAccounts { get; set; }
        //public List<Item> ListNumberSMS { get; set; }
        //public List<Item> ListAccounts { get; set; }
    }
}