using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetTypeProductDat
{
    [DataContract]
    public class GetTypeProductDatResponse
    {
        public class DetalleError
        {
            [DataMember]
            public object errorCode { get; set; }
            [DataMember]
            public object errorDescription { get; set; }

        }
        public class ResponseStatus
        {
            [DataMember]
            public int status { get; set; }
            [DataMember]
            public string codigoRespuesta { get; set; }
            [DataMember]
            public string descripcionRespuesta { get; set; }
            [DataMember]
            public object ubicacionError { get; set; }
            [DataMember]
            public DateTime fecha { get; set; }
            [DataMember]
            public object origen { get; set; }
            [DataMember]
            public List<DetalleError> detalleError { get; set; }

        }
        public class RecursoFisico
        {
            [DataMember]
            public object iccid { get; set; }

        }
        public class RecursoLogico
        {
            public string numeroLinea { get; set; }

        }
        public class CaracteristicaProducto
        {
            [DataMember]
            public string tecnologia { get; set; }
            [DataMember]
            public string tipoServicio { get; set; }

        }
        public class Producto
        {
            [DataMember]
            public List<RecursoFisico> recursoFisico { get; set; }
            [DataMember]
            public List<RecursoLogico> recursoLogico { get; set; }
            [DataMember]
            public CaracteristicaProducto caracteristicaProducto { get; set; }

        }
        public class OfertaProducto
        {
            [DataMember]
            public Producto producto { get; set; }

        }
        public class IdentificacionPersona
        {
            [DataMember]
            public object numeroDocumento { get; set; }
            [DataMember]
            public object tipoDocumento { get; set; }
            [DataMember]
            public object genero { get; set; }
            [DataMember]
            public object fechaNacimiento { get; set; }
            [DataMember]
            public object telefonoContacto { get; set; }
            [DataMember]
            public object email { get; set; }
            [DataMember]
            public string nombreCompleto { get; set; }
            [DataMember]
            public string nombre { get; set; }
            [DataMember]
            public string apellidoCompleto { get; set; }

        }
        public class Propiedad
        {
            [DataMember]
            public object tipoPredio { get; set; }
            [DataMember]
            public object nroDepartamento { get; set; }

        }
        public class DireccionUrbana
        {
            [DataMember]
            public object referenciaDireccion { get; set; }
            [DataMember]
            public object tipoVia { get; set; }
            [DataMember]
            public object distrito { get; set; }
            [DataMember]
            public object departamento { get; set; }
            [DataMember]
            public string provincia { get; set; }
            [DataMember]
            public string codigoPostal { get; set; }
            [DataMember]
            public object nombreCalle { get; set; }
            [DataMember]
            public string nroCuadra { get; set; }
            [DataMember]
            public List<Propiedad> _propiedad { get; set; }

        }
        public class Cliente
        {
            [DataMember]
            public IdentificacionPersona identificacionPersona { get; set; }
            [DataMember]
            public List<DireccionUrbana> direccionUrbana { get; set; }

        }
        public class CuentaCliente
        {
            [DataMember]
            public Cliente cliente { get; set; }
            [DataMember]
            public string idCliente { get; set; }
            [DataMember]
            public string idPublicoCliente { get; set; }
            [DataMember]
            public string nroCuenta { get; set; }

        }
        public class CuentaFacturacion
        {
            [DataMember]
            public CuentaCliente cuentaCliente { get; set; }



        }
        public class CaracteristicaAdicional
        {
            [DataMember]
            public string descripcion { get; set; } //indCambioNum
            [DataMember]
            public string valor { get; set; }//0

        }
        public class Contrato
        {
            [DataMember]
            public string idContrato { get; set; }
            [DataMember]
            public string fechaActivacion { get; set; }
            [DataMember]
            public string idPublicoContrato { get; set; }
            [DataMember]
            public string estadoContrato { get; set; }
            [DataMember]
            public List<OfertaProducto> ofertaProducto { get; set; }
            [DataMember]
            public CuentaFacturacion cuentaFacturacion { get; set; }
            [DataMember]
            public List<CaracteristicaAdicional> caracteristicaAdicional { get; set; }
        }
        public class ResponseData
        {
            [DataMember]
            public List<Contrato> contrato { get; set; }

        }

        [DataMember]
        public ResponseStatus responseStatus { get; set; }
        [DataMember]
        public ResponseData responseData { get; set; }

    }
}
