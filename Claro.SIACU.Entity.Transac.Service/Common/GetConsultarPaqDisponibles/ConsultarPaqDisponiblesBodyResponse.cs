using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetConsultarPaqDisponibles 
{
    //INI - RF-02 Evalenzs
    [DataContract(Name = "ConsultarPaqDisponiblesBodyResponse")]
    public class ConsultarPaqDisponiblesBodyResponse
    {
        [DataMember(Name = "defaultServiceResponse")]
        public defaultServiceResponse defaultServiceResponse { get; set; }
        [DataMember(Name = "listaProductosDeCompra")]
        public List<listaProductosDeCompra> listaProductosDeCompra { get; set; }
    }
    public class defaultServiceResponse
    {

        [DataMember(Name = "idRespuesta")]
        public string idRespuesta { get; set; }

        [DataMember(Name = "mensaje")]
        public string mensaje { get; set; }

        [DataMember(Name = "idSesion")]
        public string idSesion { get; set; }
        [DataMember(Name = "idTransaccional")]
        public string idTransaccional { get; set; }

    }
    public class listaProductosDeCompra
    {
        [DataMember(Name = "cantidadFavoritos")]
        public string cantidadFavoritos { get; set; }

        [DataMember(Name = "catnId")]
        public string catnId { get; set; }
        [DataMember(Name = "catvCodCategoria")]
        public string catvCodCategoria { get; set; }
        [DataMember(Name = "catvTitulo")]
        public string catvTitulo { get; set; }

        [DataMember(Name = "codTipoLinea")]
        public string codTipoLinea { get; set; }
        [DataMember(Name = "codigoPaquete")]
        public string codigoPaquete { get; set; }
        [DataMember(Name = "codigoProducto")]
        public string codigoProducto { get; set; }
        [DataMember(Name = "estadoMetodoPago")]
        public string estadoMetodoPago { get; set; }
        [DataMember(Name = "finVigencia")]
        public string finVigencia { get; set; }
        [DataMember(Name = "flagValidarSaldoPontis")]
        public string flagValidarSaldoPontis { get; set; }
        [DataMember(Name = "idMetodoPago")]
        public string idMetodoPago { get; set; }
        [DataMember(Name = "idProductoDeCompra")]
        public string idProductoDeCompra { get; set; }
        [DataMember(Name = "idProductoDeCompraAsociado")]
        public string idProductoDeCompraAsociado { get; set; }
        [DataMember(Name = "idTipoLinea")]
        public string idTipoLinea { get; set; }
        [DataMember(Name = "iniVigencia")]
        public string iniVigencia { get; set; }
        [DataMember(Name = "listaCaracteristicasProducto")]
        public List<listaCaracteristicasProducto> listaCaracteristicasProducto { get; set; }
        [DataMember(Name = "moneda2")]
        public string moneda2 { get; set; }
        [DataMember(Name = "nombreMetodoPago")]
        public string nombreMetodoPago { get; set; }
        [DataMember(Name = "nombreProducto")]
        public string nombreProducto { get; set; }
        [DataMember(Name = "precio")]
        public string precio { get; set; }
        [DataMember(Name = "recomendadoCategoriaOrden")]
        public string recomendadoCategoriaOrden { get; set; }
        [DataMember(Name = "tipoVigencia")]
        public string tipoVigencia { get; set; }
        [DataMember(Name = "tituloProducto")]
        public string tituloProducto { get; set; }
        [DataMember(Name = "vigencia")]
        public string vigencia { get; set; }


    }

    public class listaCaracteristicasProducto
    {

        [DataMember(Name = "estilo")]
        public string estilo { get; set; }
        [DataMember(Name = "idCaracteristica")]
        public string idCaracteristica { get; set; }
        [DataMember(Name = "idProducto")]
        public string idProducto { get; set; }
        [DataMember(Name = "nombre")]
        public string nombre { get; set; }
        [DataMember(Name = "orden")]
        public string orden { get; set; }
    }

}
