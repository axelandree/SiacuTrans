using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Coliving
{
    [DataContract]
    public class DataHistorical
    {
        [DataMember]
        public string AddressFact { get; set; }
        [DataMember]
        public string AddressLegal { get; set; }
        [DataMember]
        public string AddressNoteFact { get; set; }
        [DataMember]
        public string AddressNoteLegal { get; set; }
        [DataMember]
        public string BirthDate { get; set; }
        [DataMember]
        public string BusinessName { get; set; }
        [DataMember]
        public string ChangeMot { get; set; }
        [DataMember]
        public string Contact { get; set; }
        [DataMember]
        public string CountryFact { get; set; }
        [DataMember]
        public string CountryLegal { get; set; }
        [DataMember]
        public string CustomerId { get; set; }
        [DataMember]
        public string DepartmentFact { get; set; }
        [DataMember]
        public string DepartmentLegal { get; set; }
        [DataMember]
        public string DistrictFact { get; set; }
        [DataMember]
        public string DistrictLegal { get; set; }
        [DataMember]
        public string DocRep { get; set; }
        [DataMember]
        public string DocType { get; set; }
        [DataMember]
        public string DocTypeDesc { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Fax { get; set; }
        [DataMember]
        public string fechaRegistroCli { get; set; }
        [DataMember]
        public string FecMod1 { get; set; }
        [DataMember]
        public string FecMod2 { get; set; }
        [DataMember]
        public string FecMod3 { get; set; }
        [DataMember]
        public string FecMod4 { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string JobDesc { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string LegalRep { get; set; }
        [DataMember]
        public string MaritalStatus { get; set; }
        [DataMember]
        public string Movil { get; set; }
        [DataMember]
        public string Nationality { get; set; }
        [DataMember]
        public string NationalityDesc { get; set; }
        [DataMember]
        public string NroDoc { get; set; }
        [DataMember]
        public string nSequense { get; set; }
        [DataMember]
        public string ProvinceFact { get; set; }
        [DataMember]
        public string ProvinceLegal { get; set; }
        [DataMember]
        public string Sex { get; set; }
        [DataMember]
        public string Telephone { get; set; }
        [DataMember]
        public string TradeName { get; set; }
        [DataMember]
        public string updCliente { get; set; }
        [DataMember]
        public string updContact { get; set; }
        [DataMember]
        public string updDataMinor { get; set; }
        [DataMember]
        public string updDirFac { get; set; }
        [DataMember]
        public string updDirLegal { get; set; }
        [DataMember]
        public string updRepLegal { get; set; }
        [DataMember]
        public string ZipFact { get; set; }
        [DataMember]
        public string ZipLegal { get; set; }
    }
}
