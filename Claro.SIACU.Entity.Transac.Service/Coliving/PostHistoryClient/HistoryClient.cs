using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Coliving.PostHistoryClient
{
   [DataContract]
    public class HistoryClient
    {

       [DataMember(Name = "auditRequest")]
       public AuditRequest AuditRequest { get; set; }
       [DataMember]
        public string customerId { get; set; }
       [DataMember]
        public string ccNameNew { get; set; }
       [DataMember]
        public string firstName { get; set; }
       [DataMember]
        public string lastName { get; set; }
       [DataMember]
        public string businessName { get; set; }
       [DataMember]
        public string doctype { get; set; }
       [DataMember]
        public string doctypeDesc { get; set; }
       [DataMember]
        public string nroDoc { get; set; }
       [DataMember]
        public string csCompRegNoNew { get; set; }
       [DataMember]
        public string passPortNoNewc { get; set; }
       [DataMember]
        public string birthdate { get; set; }
       [DataMember]
        public string jobDesc { get; set; }
       [DataMember]
        public string telf { get; set; }
       [DataMember]
        public string movil { get; set; }
       [DataMember]
        public string fax { get; set; }
       [DataMember]
        public string email { get; set; }
       [DataMember]
        public string csEmployerNew { get; set; }
       [DataMember]
        public string tradeName { get; set; }
       [DataMember]
        public string contact { get; set; }
       [DataMember]
        public string nationality { get; set; }
       [DataMember]
        public string nationalityDesc { get; set; }
       [DataMember]
        public string sex { get; set; }
       [DataMember]
        public string maritalStatus { get; set; }
       [DataMember]
        public string legalRep { get; set; }
       [DataMember]
        public string docRep { get; set; }
       [DataMember]
       public string docTypePerep { get; set; }
       [DataMember]
        public string addressLegal { get; set; }
       [DataMember]
        public string addressNoteLegal { get; set; }
       [DataMember]
        public string ccAddrL2New { get; set; }
       [DataMember]
        public string districtLegal { get; set; }
       [DataMember]
        public string provinceLegal { get; set; }
       [DataMember]
        public string ccStreetLNew { get; set; }
       [DataMember]
        public string departmentLegal { get; set; }
       [DataMember]
        public string countryLegal { get; set; }
       [DataMember]
        public string countryLNew { get; set; }
       [DataMember]
        public string zipLegal { get; set; }
       [DataMember]
        public string addressFact { get; set; }
       [DataMember]
        public string addressNoteFact { get; set; }
       [DataMember]
        public string districtFact { get; set; }
       [DataMember]
        public string provinceFact { get; set; }
       [DataMember]
        public string departmentFact { get; set; }
       [DataMember]
        public string ccStreetNew { get; set; }
       [DataMember]
        public string countryFact { get; set; }
       [DataMember]
        public string zipFact { get; set; }
       [DataMember]
        public string changeMot { get; set; }
       [DataMember]
        public string updateGrupo { get; set; }
       [DataMember]
        public string fecReg { get; set; }
       [DataMember]
        public string usuario { get; set; }
    }
}
