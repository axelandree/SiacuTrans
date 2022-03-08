using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.RegisterActiDesaBonoDesc
{
    [DataContract]
    public class registrarDescLTERequest
    {
        [DataMember(Name = "auditRequest")]
        public auditRequest auditRequest { get; set; }

        [DataMember(Name = "fechaProgramacion")]
        public string fechaProgramacion { get; set; }

        [DataMember(Name = "customerId")]
        public string customerId { get; set; }

        [DataMember(Name = "coId")]
        public string coId { get; set; }

        [DataMember(Name = "msisdn")]
        public string msisdn { get; set; }

        [DataMember(Name = "coSer")]
        public string coSer { get; set; }

        [DataMember(Name = "desCoSer")]
        public string desCoSer { get; set; }

        [DataMember(Name = "nroCuenta")]
        public string nroCuenta { get; set; }

        [DataMember(Name = "cicloFac")]
        public string cicloFac { get; set; }

        [DataMember(Name = "aplicaDesc")]
        public string aplicaDesc { get; set; }

        [DataMember(Name = "objId")]
        public string objId { get; set; }

        [DataMember(Name = "siteObjId")]
        public string siteObjId { get; set; }

        [DataMember(Name = "idCampana")]
        public string idCampana { get; set; }

        [DataMember(Name = "MontoDesc")]
        public string MontoDesc { get; set; }

        [DataMember(Name = "PeriodoDesc")]
        public string PeriodoDesc { get; set; }

        [DataMember(Name = "FlagDesc")]
        public string FlagDesc { get; set; }

        [DataMember(Name = "SnCode")]
        public string SnCode { get; set; }

        [DataMember(Name = "notas")]
        public string notas { get; set; }

        [DataMember(Name = "destinatarioCorreo")]
        public string destinatarioCorreo { get; set; }

        [DataMember(Name = "codigoEmpleado")]
        public string codigoEmpleado { get; set; }

        [DataMember(Name = "codigoSistema")]
        public string codigoSistema { get; set; }

        [DataMember(Name = "NroDocumento")]
        public string NroDocumento { get; set; }

        [DataMember(Name = "PlanCliente")]
        public string PlanCliente { get; set; }

        [DataMember(Name = "NomCliente")]
        public string NomCliente { get; set; }

        [DataMember(Name = "ApeCliente")]
        public string ApeCliente { get; set; }

        [DataMember(Name = "DescDAC")]
        public string DescDAC { get; set; }

        [DataMember(Name = "DescTipoDoc")]
        public string DescTipoDoc { get; set; }

        [DataMember(Name = "TipoCliente")]
        public string TipoCliente { get; set; }

        [DataMember(Name = "NomClienteCompleto")]
        public string NomClienteCompleto { get; set; }

        [DataMember(Name = "NroCliente")]
        public string NroCliente { get; set; }

        [DataMember(Name = "NomResponsableLegal")]
        public string NomResponsableLegal { get; set; }

        [DataMember(Name = "CostoServiciosinIGV")]
        public string CostoServiciosinIGV { get; set; }

        [DataMember(Name = "CostoServicioconIGV")]
        public string CostoServicioconIGV { get; set; }

        [DataMember(Name = "listaRequestOpcional")]
        public List<listaRequestOpcional> listaRequestOpcional { get; set; }

    }
}
