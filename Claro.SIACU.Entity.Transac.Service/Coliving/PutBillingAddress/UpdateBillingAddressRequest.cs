using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Coliving.PutBillingAddress
{
    [DataContract]
    public class UpdateBillingAddressRequest 
    {
        
        [DataMember]
        public string bmId { get; set; }
        [DataMember]
        public string adrBirthdt { get; set; }
        [DataMember]
        public string adrCheck { get; set; }
        [DataMember]
        public string adrCity { get; set; }
        [DataMember]
        public string adrCode { get; set; }
        [DataMember]
        public string adrCompno { get; set; }
        [DataMember]
        public string adrCounty { get; set; }
        [DataMember]
        public string adrCusttype { get; set; }
        [DataMember]
        public string adrDeleted { get; set; }
        [DataMember]
        public string adrDrivelicence { get; set; }
        [DataMember]
        public string adrEmail { get; set; }
        [DataMember]
        public string adrEmployee { get; set; }
        [DataMember]
        public string adrEmployer { get; set; }
        [DataMember]
        public string adrFax { get; set; }
        [DataMember]
        public string adrFaxarea { get; set; }
        [DataMember]
        public string adrFname { get; set; }
        [DataMember]
        public string adrForward { get; set; }
        [DataMember]
        public string adrIdno { get; set; }
        [DataMember]
        public string adrInccode { get; set; }
        [DataMember]
        public string adrJbdes { get; set; }
        [DataMember]
        public string adrLname { get; set; }
        [DataMember]
        public string adrLocation1 { get; set; }
        [DataMember]
        public string adrLocation2 { get; set; }
        [DataMember]
        public string adrMname { get; set; }
        [DataMember]
        public string adrName { get; set; }
        [DataMember]
        public string adrNationality { get; set; }
        [DataMember]
        public string adrNationalitypub { get; set; }
        [DataMember]
        public string adrNote1 { get; set; }
        [DataMember]
        public string adrNote2 { get; set; }
        [DataMember]
        public string adrNote3 { get; set; }
        [DataMember]
        public string adrPhn1 { get; set; }
        [DataMember]
        public string adrPhn1Area { get; set; }
        [DataMember]
        public string adrPhn2 { get; set; }
        [DataMember]
        public string adrPhn2Area { get; set; }
        [DataMember]
        public string adrRemark { get; set; }
        [DataMember]
        public string adrRoles { get; set; }
        [DataMember]
        public string adrSeq { get; set; }
        [DataMember]
        public string adrSex { get; set; }
        [DataMember]
        public string adrSmsno { get; set; }
        [DataMember]
        public string adrSocialSeno { get; set; }
        [DataMember]
        public string adrState { get; set; }
        [DataMember]
        public string adrStreet { get; set; }
        [DataMember]
        public string adrStreetNo { get; set; }
        [DataMember]
        public string adrTaxno { get; set; }
        [DataMember]
        public string adrUrgent { get; set; }
        [DataMember]
        public string adrValidDate { get; set; }
        [DataMember]
        public string adrWriteOnReq { get; set; }
        [DataMember]
        public string adrYears { get; set; }
        [DataMember]
        public string adrzIp { get; set; }
        [DataMember]
        public string countryId { get; set; }
        [DataMember]
        public string countryIdPub { get; set; }
        [DataMember]
        public string csId { get; set; }
        [DataMember]
        public string csIdPub { get; set; }
        [DataMember]
        public string idTypeCode { get; set; }
        [DataMember]
        public string lngCode { get; set; }
        [DataMember]
        public string lngCodePub { get; set; }
        [DataMember]
        public string masCode { get; set; }
        [DataMember]
        public string masCodePub { get; set; }
        [DataMember]
        public string ttlId { get; set; }
        [DataMember]
        public string ttlidpub { get; set; }
        [DataMember]
        public string billingAccountId { get; set; }
        [DataMember]
        public List<Opcional> listaOpcional { get; set; }


    }
}
