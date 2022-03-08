using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common
{
    [DataContract]
    public class Employee
    {
        [DataMember]
        [Data.DbColumn("nombres")]
        public string nombres { get; set; }

        [DataMember]
        [Data.DbColumn("paterno")]
        public string paterno { get; set; }

        [DataMember]
        [Data.DbColumn("materno")]
        public string materno { get; set; }

        [DataMember]
        [Data.DbColumn("correo")]
        public string correo { get; set; }

        [DataMember]
        [Data.DbColumn("codigoArea")]
        public string codigoArea { get; set; }

        [DataMember]
        [Data.DbColumn("descripcionArea")]
        public string descripcionArea { get; set; }

        [DataMember]
        [Data.DbColumn("telefono")]
        public string telefono { get; set; }

        [DataMember]
        [Data.DbColumn("dni")]
        public string dni { get; set; }
        
        [DataMember(Name = "userId", Order = 0)]
        public int UserID { get; set; }

        [DataMember(Name = "sapVendorId", Order = 1)]
        public int SapVendorID { get; set; }

        [DataMember(Name = "accessStatus", Order = 2)]
        public string AccessStatus { get; set; }

        [DataMember(Name = "login", Order = 3)]
        public string Login { get; set; }

        [DataMember(Name = "fullName", Order = 4)]
        public string FullName { get; set; }

        [DataMember(Name = "firstName", Order = 5)]
        public string FirstName { get; set; }

        [DataMember(Name = "lastName1", Order = 6)]
        public string LastName1 { get; set; }

        [DataMember(Name = "lastName2", Order = 7)]
        public string LastName2 { get; set; }

        [DataMember(Name = "email", Order = 8)]
        public string Email { get; set; }

        [DataMember(Name = "profileId", Order = 9)]
        public string ProfileID { get; set; }

        [DataMember(Name = "areaId", Order = 10)]
        public int AreaID { get; set; }

        [DataMember(Name = "areaName", Order = 11)]
        public string AreaName { get; set; }

        [DataMember(Name = "searchUser", Order = 12)]
        public string SearchUser { get; set; }

        [DataMember(Name = "profiles", Order = 13)]
        public string Profiles { get; set; }

        [DataMember(Name = "optionPermissions", Order = 14)]
        public string OptionPermissions { get; set; }
    }
}
