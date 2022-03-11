using Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.CommonServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models.UnistallInstallationOfDecoder
{
    public class UnistallInstallationOfDecoderModel
    {
        public List<Decoder> ListDecoder { get; set; }
        public List<GenericItem> ListJobTypes { get; set; }
        public List<PlanService> ListPlanService { get; set; }
        public List<DetailInteractionService> ListDetailInteractionService { get; set; }
        public DatosCliente oDatosCliente { get; set; }
        public DatosLinea oDatosLinea { get; set; }
        public UsuarioAcceso oUsuarioAcceso { get; set; }

        public string rutaArchivo { get; set; }
        public string nombreArchivo { get; set; }
        public string SotDeBaja { get; set; }
        public string IdSession { get; set; }
        public string IpServidor { get; set; }
        public string Sn { get; set; }

        public string FlajCorreo { get; set; }
        public string Correo { get; set; }
        public string FlajFidelizacion { get; set; }
        public string MontoFidelizacion { get; set; }
        public string FlajInstDesins { get; set; }
        public double IGV { get; set; }

        public string PuntoAtencion { get; set; }
        public string Cantidad { get; set; }
        public string CargoFijoTotalPlanSIGV { get; set; }
        public string CargoFijoTotalPlanCIGV { get; set; }
        public string CargoFijoTotal { get; set; }
        public string FechaProgramacion { get; set; }
        public string TipoTrabajo { get; set; }
        public string Nota { get; set; }
        public string ValidaEta { get; set; }
        public string FranjaHorariaETA { get; set; }
        public string FranjaHora { get; set; }
        public string FranjaHorariaFinal { get; set; }
        public string CodigoRequestAct { get; set; }
        public string ContenidoEquipo { get; set; } //Trama de Equipos Adicionales
        public string CodSot { get; set; }
        public string strSubTypeWork { get; set; }
    }
    public class PlanService
    {
        public string CodigoPlan { get; set; }
        public string DescPlan { get; set; }
        public string TmCode { get; set; }
        public string Solucion { get; set; }
        public string CodServSisact { get; set; }
        public string SNCode { get; set; }
        public string SPCode { get; set; }
        public string CodTipoServ { get; set; }
        public string TipoServ { get; set; }
        public string DesServSisact { get; set; }
        public string CodGrupoServ { get; set; }
        public string GrupoServ { get; set; }
        public string CF { get; set; }
        public string IdEquipo { get; set; }
        public string Equipo { get; set; }
        public string CantidadEquipo { get; set; }
        public string MatvIdSap { get; set; }
        public string MatvDesSap { get; set; }
        public string TipoEquipo { get; set; }
        public string CodigoExterno { get; set; }
        public string DesCodigoExterno { get; set; }
        public string ServvUsuarioCrea { get; set; }
    }
    public class DatosCliente
    {
        public string CUSTOMER_ID { get; set; }
        public string CONTRATO_ID { get; set; }
        public string NOMBRE_COMPLETO { get; set; }
        public string REPRESENTANTE_LEGAL { get; set; }
        public string CICLO_FACTURACION { get; set; }
        public string NRO_DOC { get; set; }
        public string DIRECCION_DESPACHO { get; set; }
        public string DEPARTEMENTO_LEGAL { get; set; }
        public string DISTRITO_LEGAL { get; set; }
        public string PAIS_LEGAL { get; set; }
        public string PROVINCIA_LEGAL { get; set; }
        public string CODIGO_PLANO_INST { get; set; }
        public string URBANIZACION_LEGAL { get; set; }
        public string NOMBRES { get; set; }
        public string APELLIDOS { get; set; }
        public string TIPO_CLIENTE { get; set; }
        public string CONTACTO_CLIENTE { get; set; }
        public string UBIGEO_INST { get; set; }
        public string RAZON_SOCIAL { get; set; }
    }
    public class DatosLinea
    {
        public string Plan { get; set; }
        public string FecActivacion { get; set; }
        public string StatusLinea { get; set; }
    }
    public class UsuarioAcceso
    {
        public string USRREGIS { get; set; }
    }
    public class DetailInteractionService
    {
        public string IdServicio { get; set; }
        public string GsrvcPrincipal { get; set; }
        public string GsrvcCodigo { get; set; }
        public string Cantidad { get; set; }
        public string Servicio { get; set; }
        public string Bandwid { get; set; }
        public string FlagLc { get; set; }
        public string CantidadIdLinea { get; set; }
        public string IdEquipo { get; set; }
        public string CodTipEqu { get; set; }
        public string CantEquipo { get; set; }
        public string Equipo { get; set; }
        public string CodigoExt { get; set; }
    }
    public class Decoder {
        public string codigo_material { get; set; }
        public string codigo_sap { get; set; }
        public string numero_serie { get; set; }
        public string macadress { get; set; }
        public string descripcion_material { get; set; }
        public string abrev_material { get; set; }
        public string estado_material { get; set; }
        public string precio_almacen { get; set; }
        public string codigo_cuenta { get; set; }
        public string componente { get; set; }
        public string centro { get; set; }
        public string idalm { get; set; }
        public string almacen { get; set; }
        public string tipo_equipo { get; set; }
        public string id_producto { get; set; }
        public string id_cliente { get; set; }
        public string modelo { get; set; }
        public string convertertype { get; set; }
        public string servicio_principal { get; set; }
        public string headend { get; set; }
        public string ephomeexchange { get; set; }
        public string numero { get; set; }
        public string tipoServicio { get; set; }
        public string TIPODECO { get; set; }
        public string CARGO_FIJO { get; set; }
        public string CARGO_FIJO_IGV { get; set; }
        public string DesTipoServ { get; set; }
    }
}