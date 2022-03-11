using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Coliving.GetDataBilling
{
    [DataContract]
    public class GetAddressBillingResponse
    {
        [DataMember(Name = "adrBirthdt")]
        public string adr_birthdt { get; set; }
        [DataMember(Name = "adrCity")]
        public string adr_city { get; set; }
        [DataMember(Name = "adrCompno")]
        public string adr_compno { get; set; }
        [DataMember(Name = "adrCounty")]
        public string adr_county { get; set; }
        [DataMember(Name = "adrCusttype")]
        public string adr_custtype { get; set; }
        [DataMember(Name = "adrDeleted")]
        public string adr_deleted { get; set; }
        [DataMember(Name = "adrDrivelicence")]
        public string adr_drivelicence { get; set; }
        [DataMember(Name = "adrEmail")]
        public string adr_email { get; set; }
        [DataMember(Name = "adrEmployee")]
        public string adr_employee { get; set; }
        [DataMember(Name = "adrEmployer")]
        public string adr_employer { get; set; }
        [DataMember(Name = "adrFax")]
        public string adr_fax { get; set; }
        [DataMember(Name = "adrFaxArea")]
        public string adr_fax_area { get; set; }
        [DataMember(Name = "adrFname")]
        public string adr_fname { get; set; }
        [DataMember(Name = "adrForward")]
        public string adr_forward { get; set; }
        [DataMember(Name = "adrIdno")]
        public string adr_idno { get; set; }
        [DataMember(Name = "adrInccode")]
        public string adr_inccode { get; set; }
        [DataMember(Name = "adrJbdes")]
        public string adr_jbdes { get; set; }
        [DataMember(Name = "adrJurTaxOverridden")]
        public string adr_jur_tax_overridden { get; set; }
        [DataMember(Name = "adrLname")]
        public string adr_lname { get; set; }
        [DataMember(Name = "adrLocation1")]
        public string adr_location_1 { get; set; }
        [DataMember(Name = "adrLocation2")]
        public string adr_location_2 { get; set; }
        [DataMember(Name = "adrMname")]
        public string adr_mname { get; set; }
        [DataMember(Name = "adrName")]
        public string adr_name { get; set; }
        [DataMember(Name = "adrNationality")]
        public string adr_nationality { get; set; }
        [DataMember(Name = "adrNationalityPub")]
        public string adr_nationality_pub { get; set; }
        [DataMember(Name = "adrNote1")]
        public string adr_note1 { get; set; }
        [DataMember(Name = "adrNote2")]
        public string adr_note2 { get; set; }
        [DataMember(Name = "adrNote3")]
        public string adr_note3 { get; set; }
        [DataMember(Name = "adrPhn1")]
        public string adr_phn1 { get; set; }
        [DataMember(Name = "adrPhn1Area")]
        public string adr_phn1_area { get; set; }
        [DataMember(Name = "adrPhn2")]
        public string adr_phn2 { get; set; }
        [DataMember(Name = "adrPhn2Area")]
        public string adr_phn2_area { get; set; }
        [DataMember(Name = "adrRemark")]
        public string adr_remark { get; set; }
        [DataMember(Name = "adrRoles")]
        public string adr_roles { get; set; }
        [DataMember(Name = "adrSeq")]
        public string adr_seq { get; set; }
        [DataMember(Name = "adrSex")]
        public string adr_sex { get; set; }
        [DataMember(Name = "adrSmsno")]
        public string adr_smsno { get; set; }
        [DataMember(Name = "adrSocialseno")]
        public string adr_socialseno { get; set; }
        [DataMember(Name = "adrState")]
        public string adr_state { get; set; }
        [DataMember(Name = "adrStreet")]
        public string adr_street { get; set; }
        [DataMember(Name = "adrStreetno")]
        public string adr_streetno { get; set; }
        [DataMember(Name = "adrTaxno")]
        public string adr_taxno { get; set; }
        [DataMember(Name = "adrTempbillOverridden")]
        public string adr_tempbill_overridden { get; set; }
        [DataMember(Name = "adrUrgent")]
        public string adr_urgent { get; set; }
        [DataMember(Name = "adrValiddate")]
        public string adr_validdate { get; set; }
        [DataMember(Name = "adrYears")]
        public string adr_years { get; set; }
        [DataMember(Name = "adrZip")]
        public string adr_zip { get; set; }
        [DataMember(Name = "countryId")]
        public string country_id { get; set; }
        [DataMember(Name = "countryIdPub")]
        public string country_id_pub { get; set; }
        [DataMember(Name = "idtypeCode")]
        public string idtype_code { get; set; }
        [DataMember(Name = "lngCode")]
        public string lng_code { get; set; }
        [DataMember(Name = "lngCodePub")]
        public string lng_code_pub { get; set; }
        [DataMember(Name = "masCode")]
        public string mas_code { get; set; }
        [DataMember(Name = "masCodePub")]
        public string mas_code_pub { get; set; }
        [DataMember(Name = "ttlId")]
        public string ttl_id { get; set; }
        [DataMember(Name = "ttlIdPub")]
        public string ttl_id_pub { get; set; }
    }
}
