using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Transac.Service.Fixed
{
    [DataContract]
    public class RegisterTransaction  
    {
        [DataMember]
        public string ConID { get; set; }
        [DataMember]
        public string CustomerID { get; set; }
        [DataMember]
        public string TransTipo { get; set; }
        [DataMember]
        public string InterCasoID { get; set; }
        [DataMember]
        public string ServicioID { get; set; }
        [DataMember]
        public int MotivoID { get; set; }
        [DataMember]
        public string TrabajoID { get; set; }
        [DataMember]
        public string TipoVia { get; set; }
        [DataMember]
        public string NomVia { get; set; }
        [DataMember]
        public int NroVia { get; set; }
        [DataMember]
        public string TipMz { get; set; }
        [DataMember]
        public string NumMZ { get; set; }
        [DataMember]
        public string NumLote { get; set; }
        [DataMember]
        public string TipoUrb { get; set; }
        [DataMember]
        public string NomUrb { get; set; }
        [DataMember]
        public string Ubigeo { get; set; }
        [DataMember]
        public string ZonaID { get; set; }
        [DataMember]
        public string CentroPobladoID { get; set; }
        [DataMember]
        public string EdificioID { get; set; }
        [DataMember]
        public string Referencia { get; set; }
        [DataMember]
        public string Observacion { get; set; }
        [DataMember]
        public string FranjaHora { get; set; }
        [DataMember]
        public string FranjaHoraID { get; set; }
        [DataMember]
        public string FechaProgramada { get; set; }
        [DataMember]
        public string NumCarta { get; set; }
        [DataMember]
        public string Operador { get; set; }
        [DataMember]
        public string TmCode { get; set; }
        [DataMember]
        public string USRREGIS { get; set; }
        [DataMember]
        public string Cargo { get; set; }
        [DataMember]
        public string CargoFijo { get; set; }
        [DataMember]
        public string CodReclamo { get; set; }
        [DataMember]
        public string FlagActDirFact { get; set; }
        [DataMember]
        public string codigoAplicacion { get; set; }
        [DataMember]
        public string ipAplicacion { get; set; }
        [DataMember]
        public string usuarioSistema { get; set; }
        [DataMember]
        public string usuarioApp { get; set; }
        [DataMember]
        public string nombreAplicacion { get; set; }
        [DataMember]
        public string FechaProg { get; set; }
        [DataMember]
        public string ServiEstado { get; set; }
        [DataMember]
        public string ServiCod { get; set; }
        [DataMember]
        public string Cantidad { get; set; }
        [DataMember]
        public string fechaDesde { get; set; }
        [DataMember]
        public string fechaHasta { get; set; }
        [DataMember]
        public string cadDac { get; set; }
        [DataMember]
        public string cuenta { get; set; }
        [DataMember]
        public string asesor { get; set; }
        [DataMember]
        public string tipoTransaccion { get; set; }
        [DataMember]
        public string codInteraccion { get; set; }
        [DataMember]
        public string Presuscrito { get; set; }
        [DataMember]
        public string Publicar { get; set; }
        [DataMember]
        public string IdInteraccion { get; set; }
        [DataMember]
        public string ListaCoser { get; set; }
        [DataMember]
        public string ListaSpCode { get; set; }
        [DataMember]
        public string CodCaso { get; set; }
        [DataMember]
        public string CodOCC { get; set; }
    }
}
