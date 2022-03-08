using System.Runtime.Serialization;
using Claro.Data;
namespace Claro.SIACU.Entity.Transac.Service.Common
{
    [DataContract(Name = "BEHub")]
    public class BEHub
    {
        [DataMember]
        [Data.DbColumn("CINTILLO")]
        public string strCintillo { get; set; }
        [DataMember]
        [Data.DbColumn("CMTS")]
        public string strCmts { get; set; }
        [DataMember]
        [Data.DbColumn("CODSUC")]
        public string strCodSuc { get; set; }
        [DataMember]
        [Data.DbColumn("DIRSUC")]
        public string strDirSuc { get; set; }
        [DataMember]
        [Data.DbColumn("HUB")]
        public string strHub { get; set; }
        [DataMember]
        [Data.DbColumn("HUB_DESC")]
        public string strHubDesc { get; set; }
        [DataMember]
        [Data.DbColumn("IDPLANO")]
        public string strIdPlano { get; set; }
        [DataMember]
        [Data.DbColumn("NOMSUC")]
        public string strNomSuc { get; set; }
        [DataMember]
        [Data.DbColumn("NOMURB")]
        public string strNomUrb { get; set; }
        [DataMember]
        [Data.DbColumn("NUMVIA")]
        public string strNumVia { get; set; }
        [DataMember]
        [Data.DbColumn("REFERENCIA")]
        public string strReference { get; set; }
    }
}
