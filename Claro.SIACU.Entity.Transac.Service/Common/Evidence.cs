using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Transac.Service.Common
{
    [DataContract]
    public class Evidence
    {
        [DataMember]
        public string StrTransactionType { get; set; }
        [DataMember]
        public string StrTransactionCode { get; set; }
        [DataMember]
        public string StrCustomerCode { get; set; }
        [DataMember]
        public string StrPhoneNumber { get; set; }
        [DataMember]
        public string StrTypificationCode { get; set; }
        [DataMember]
        public string StrTypificationDesc { get; set; }
        [DataMember]
        public string StrCommercialDesc { get; set; }
        [DataMember]
        public string StrProductType { get; set; }
        [DataMember]
        public string StrServiceChannel { get; set; }
        [DataMember]
        public string StrActivationDate { get; set; }
        [DataMember]
        public string StrSuspensionDate { get; set; }
        [DataMember]
        public string StrServiceStatus { get; set; }
        [DataMember]
        public string StrLoadDate { get; set; }
        [DataMember]
        public string StrUserName { get; set; }
        [DataMember]
        public string StrFlagEvidence { get; set; }
        [DataMember]
        public string StrFlagExport { get; set; }
        [DataMember]
        public string StrFlagMa { get; set; }
        [DataMember]
        public string StrTransactionId { get; set; }
        [DataMember]
        public string StrTransactionDate { get; set; }
        [DataMember]
        public string StrCreationDate { get; set; }
        [DataMember]
        public string StrEvidenceId { get; set; }
        [DataMember]
        public string StrDocumentName { get; set; }
        [DataMember]
        public string StrDocumentType { get; set; }
        [DataMember]
        public string StrDocumentExtension { get; set; }
        [DataMember]
        public string StrDocumentPath { get; set; }
        [DataMember]
        public string StrUserFirstName { get; set; }
        [DataMember]
        public string StrUserLastName { get; set; }
    }
}
