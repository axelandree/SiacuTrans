using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetProgramerMigration
{
    [DataContract(Name = "ProgramerMigrationRequest")]
    public class ProgramerMigrationRequest : Claro.Entity.Request
    {
        [DataMember]
        public string coId { get; set; }
        [DataMember]
        public string msisdn { get; set; }
        [DataMember]
        public System.DateTime fechaProgramacion { get; set; }
        [DataMember]
        public string customerId { get; set; }
        [DataMember]
        public string codigoProducto { get; set; }
        [DataMember]
        public List<contratoBeanType> listaContratos { get; set; }
        [DataMember]
        public string tipoServicio { get; set; }
        [DataMember]
        public string flagOccApadece { get; set; }
        [DataMember]
        public string flagNdPcs { get; set; }
        [DataMember]
        public string ndArea { get; set; }
        [DataMember]
        public string ndMotivo { get; set; }
        [DataMember]
        public string ndSubmotivo { get; set; }
        [DataMember]
        public string cacDac { get; set; }
        [DataMember]
        public int cicloFacturacion { get; set; }
        [DataMember]
        public string idTipoCliente { get; set; }
        [DataMember]
        public string numeroDocumento { get; set; }
        [DataMember]
        public string clienteCuenta { get; set; }
        [DataMember]
        public System.Decimal montoPCS { get; set; }
        [DataMember]
        public System.Decimal montoFidelizacion { get; set; }
        [DataMember]
        public string idInteraccion { get; set; }
        [DataMember]
        public string tipoPostpago { get; set; }
        [DataMember]
        public System.Decimal montoApadece { get; set; }
        [DataMember]
        public string flagValidaApadece { get; set; }
        [DataMember]
        public string flagAplicaApadece { get; set; }
        [DataMember]
        public string flagLimiteCredito { get; set; }
        [DataMember]
        public string topeConsumo { get; set; }
        [DataMember]
        public string nroCuenta { get; set; }
        [DataMember]
        public string asesor { get; set; }
        [DataMember]
        public System.DateTime fechaProgramacionTope { get; set; }
        [DataMember]
        public string tipoTope { get; set; }
        [DataMember]
        public string descripcionTipoTope { get; set; }
        [DataMember]
        public string tipoRegistroTope { get; set; }
        [DataMember]
        public int topeControlConsumo { get; set; }
        [DataMember]
        public string tipoClarify { get; set; }
        [DataMember]
        public string cuentaPadre { get; set; }
        [DataMember]
        public string tipoMigracion { get; set; }
        [DataMember]
        public string nivelCuenta { get; set; }
        [DataMember]
        public string tipoCuenta { get; set; }
        [DataMember]
        public string imsi { get; set; }
    }
    [DataContract]
    public class contratoBeanType
    {
        [DataMember]
        public long planTarifario { get; set; }
        [DataMember]
        public bool estadoUmbral { get; set; }
        [DataMember]
        public actualizacionContratoBeanType actualizacionContrato { get; set; }
        [DataMember]
        public List<campoBean> informacionContrato { get; set; }
        [DataMember]
        public List<dispositivoBeanType> listaDispositivos { get; set; }
    }
    [DataContract]
    public class actualizacionContratoBeanType
    {
        [DataMember]
        public long razon { get; set; }
    }
    [DataContract]
    public class campoBean
    {
        [DataMember]
        public int indice { get; set; }
        [DataMember]
        public int tipo { get; set; }
        [DataMember]
        public string valor { get; set; }
    }
    [DataContract]
    public class dispositivoBeanType
    {
        [DataMember]
        public long idDispositivo { get; set; }
        [DataMember]
        public int tipoDispositivo { get; set; }
    }
}
