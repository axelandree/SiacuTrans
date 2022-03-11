using System;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.PostUpdateProgTaskLte
{
    [DataContract]
    public class UpdateProgTaskLteRequest : Claro.Entity.Request
    {
        [DataMember]
        public string StrIdSession { get; set; }
        [DataMember]
        public string StrTransaction { get; set; }
        [DataMember]
        public string ConId { get; set; }
        [DataMember]
        public string CustomerId { get; set; }
        [DataMember]
        public string TransTipo { get; set; }
        [DataMember]
        public string InterCasoId { get; set; }
        [DataMember]
        public string ServicioId { get; set; }
        [DataMember]
        public int MotivoId { get; set; }
        [DataMember]
        public string TrabajoId { get; set; }
        [DataMember]
        public string TipoVia { get; set; }
        [DataMember]
        public string NomVia { get; set; }
        [DataMember]
        public int NroVia { get; set; }
        [DataMember]
        public string TipMz { get; set; }
        [DataMember]
        public string NumMz { get; set; }
        [DataMember]
        public string NumLote { get; set; }
        [DataMember]
        public string TipoUrb { get; set; }
        [DataMember]
        public string NomUrb { get; set; }
        [DataMember]
        public string Ubigeo { get; set; }
        [DataMember]
        public string ZonaId { get; set; }
        [DataMember]
        public string CentroPobladoId { get; set; }
        [DataMember]
        public string EdificioId { get; set; }
        [DataMember]
        public string Referencia { get; set; }
        [DataMember]
        public string Observacion { get; set; }
        [DataMember]
        public string FranjaHora { get; set; }
        [DataMember]
        public string FranjaHoraId { get; set; }
        [DataMember]
        public DateTime FechaProgramada { get; set; }
        [DataMember]
        public string NumCarta { get; set; }
        [DataMember]
        public string Operador { get; set; }
        [DataMember]
        public string TmCode { get; set; }
        [DataMember]
        public string Usrregis { get; set; }
        [DataMember]
        public string Cargo { get; set; }
        [DataMember]
        public string CargoFijo { get; set; }
        [DataMember]
        public string CodReclamo { get; set; }
        [DataMember]
        public string FlagActDirFact { get; set; }
        [DataMember]
        public string IdTransaccion { get; set; }
        [DataMember]
        public string CodigoAplicacion { get; set; }
        [DataMember]
        public string IpAplicacion { get; set; }
        [DataMember]
        public string UsuarioSistema { get; set; }
        [DataMember]
        public string UsuarioApp { get; set; }
        [DataMember]
        public string NombreAplicacion { get; set; }
        [DataMember]
        public string FechaProg { get; set; }
        [DataMember]
        public string ServiEstado { get; set; }
        [DataMember]
        public string ServiCod { get; set; }
        [DataMember]
        public string Cantidad { get; set; }
        [DataMember]
        public string FechaDesde { get; set; }
        [DataMember]
        public string FechaHasta { get; set; }
        [DataMember]
        public string CadDac { get; set; }
        [DataMember]
        public string Cuenta { get; set; }
        [DataMember]
        public string Asesor { get; set; }
        [DataMember]
        public string TipoTransaccion { get; set; }
        [DataMember]
        public string CodInteraccion { get; set; }
    }
}
