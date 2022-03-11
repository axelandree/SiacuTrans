using System;
using Claro.Data;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed
{
    [DataContract(Name="ProductPlanHfc")]
    public class ProductPlan
    {
        [DbColumn("COD_PLAN_SISACT")]
        [DataMember]
        public string strCodPlanSisact { get; set; }
        [DbColumn("TMCODE")]
        [DataMember]
        public string strTmcode { get; set; }
        [DbColumn("DES_PLAN_SISACT")]
        [DataMember]
        public string strDesPlanSisact { get; set; }
        [DbColumn("VERSION")]
        [DataMember]
        public string strVersion { get; set; }
        [DbColumn("CAT_PROD")]
        [DataMember]
        public string strCatProd { get; set; }
        [DbColumn("DES_TMCODE")]
        [DataMember]
        public string strDesTmcode { get; set; }
        
        [DataMember]
        public string FechaInicio { get; set; }
       
        [DataMember]
        public string FechaFin { get; set; }
        [DbColumn("TIPO_PROD")]
        [DataMember]
        public string strTipoProd { get; set; }
        [DbColumn("USER_CREA")]
        [DataMember]
        public string strUserCrea { get; set; }
        [DbColumn("SOLUCION")]
        [DataMember]
        public string strSolucion { get; set; }
        [DbColumn("COD_PLANO")]
        [DataMember]
        public string strCodPlano { get; set; }
        [DbColumn("PLNC_ESTADO")]
        [DataMember]
        public string strStatus { get; set; }
        //[DataMember]
        //public string strDescriptionCampaign { get; set; }
        [DataMember]
        public string strCampaignCode { get; set; }
        [DataMember]
        public string strCampaignDescription { get; set; }
        [DataMember]
        public DateTime strCampaignDateEnd { get; set; }
    }
}
