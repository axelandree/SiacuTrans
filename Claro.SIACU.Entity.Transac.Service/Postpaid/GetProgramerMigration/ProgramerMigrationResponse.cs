using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetProgramerMigration
{
    [DataContract(Name = "ProgramerMigrationResponse")]
    public class ProgramerMigrationResponse
    {
        [DataMember]
        public string CodResult { get; set; }
        [DataMember]
        public string MenssageResult { get; set; }
        [DataMember]
        public int IResultado { get; set; }
    }
}
