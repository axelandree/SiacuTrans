using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetValidateQuantityCampaign
{
    [DataContract]
    public class ValidateQuantityCampaignRequest
    {
        [DataMember(Name = "auditRequest")]
        public GetValidateQuantityCampaignAuditRequest ValidateQuantityCampaignAuditRequest { get; set; }
        [DataMember(Name = "tipoDocumento")]
        public string KindDocument { get; set; }
        [DataMember(Name = "numeroDocumento")]
        public string NumDocument { get; set; }
        [DataMember(Name = "casoEspecial")]
        public string CaseSpecial { get; set; }
        [DataMember(Name = "tipoOperacion")]
        public string KindOperation { get; set; }
        [DataMember(Name = "codAplicativo")]
        public string CodAplica { get; set; }
        [DataMember(Name = "codTipoProducto")]
        public string CodKindProduc { get; set; }
        [DataMember(Name = "descTipoProducto")]
        public string DescKindProduc { get; set; }
    }
}
