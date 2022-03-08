using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using HELPER_ITEM = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.Fixed.InfoPromotionPrePostHelper;


namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models.Fixed
{
    public class InfoPromotionModel
    {
        public string codigoRespuesta { get; set; }
        public string mensajeRespuesta { get; set; }
        public HELPER_ITEM.DataClientHelper datosCliente { get; set; }
        public List<HELPER_ITEM.ContactHelper> contactos { get; set; }


    }

    public class UpdateStateLineEmailModel
    {
        public string codigoRespuesta { get; set; }
        public string mensajeRespuesta { get; set; }
        public string numeroDocumento { get; set; }
    }

    public class ListConsultarDatosUsuarioModel
    {
        public List<HELPER_ITEM.ConsultarDatosUsuarioClarifyHelper> DatosUsuario { get; set; }
        public string CorrectProcess { get; set; }
        public string MessageProcess { get; set; }

    }

    public class ListlineaConsolidadaModel
    {
      
        public string codRespuesta { get; set; }     
        public string msjRespuesta { get; set; }      
        public string cantidadLineasActivas { get; set; }
        public string cantidadLineasActivasPorDia { get; set; }
        public HELPER_ITEM.listaLineasConsolidadasTypeHelper listaLineasConsolidadasType { get; set; }

        public ListlineaConsolidadaModel()
        {
            listaLineasConsolidadasType = new HELPER_ITEM.listaLineasConsolidadasTypeHelper();
        }

    }


    public class ParametersInteraccion
    {
        public string strIdSession { get; set; }
        public string objIdContacto { get; set; }        
        public string Type { get; set; }
        public string Class { get; set; }
        public string SubClass { get; set; }
        public string Note { get; set; }
        public string CustomerId { get; set; }
        public string Plan { get; set; }
        public string ContractId { get; set; }
        public string CurrentUser { get; set; }
        public string objIdSite { get; set; }
        public string Cuenta { get; set; }       
        

    }

    public class ParametersEnvioEmail
    {
        public string srtIdSession { get; set; }
        public string strRemitente { get; set; }
        public string strDestinatario { get; set; }
        public string strAsunto { get; set; }
        public string strMensaje { get; set; }
        public string strHTMLFlag { get; set; }
        public string strFullPathPDF { get; set; }
    }


   public class ENMessage
    {

        public ENMessage()
        {

        }

        public ENMessage(string xxtipo, string mensaje)
        {
            this.xxtipo = xxtipo;
            this.mensaje = mensaje;
        }

        public string xxtipo { get; set; }
        public string mensaje { get; set; }

        public static ENMessage GetMessage(string tipo, string message)
        {
            ENMessage model = new ENMessage();
            model.xxtipo = tipo;
            model.mensaje = message;
            return model;
        }

    }

   public class OnBaseCargaModel
   {

       public string IdSession { get; set; }
       public string CodigoAsesor { get; set; }
       public string Path { get; set; }
       public string FormatoTransaccion { get; set; }

       public ConstanciaLeyPromoModel Constancia { get; set; }

       #region MetaDatos

       public string NombreCliente { get; set; }
       public string Canal { get; set; }
       public string NumeroLinea { get; set; }
       public string TipoDocumento { get; set; }
       public string NumeroDocumento { get; set; }
       public string FechaRegistro { get; set; }
       public string TipoCliente { get; set; }
       public string TipoOperacion { get; set; }

       #endregion

   }

    #region ConstanciaLeyPromo
       [XmlRoot(ElementName = "PLANTILLA")]
   public class ConstanciaLeyPromoModel
       {
           [XmlElement(ElementName = "FORMATO_TRANSACCION")]
           public string FORMATO_TRANSACCION { get; set; }
           [XmlElement(ElementName = "CANAL_ATENCION")]
           public string CANAL_ATENCION { get; set; }
           [XmlElement(ElementName = "TITULAR_CLIENTE")]
           public string TITULAR_CLIENTE { get; set; }
           [XmlElement(ElementName = "NRO_SERVICIO")]
           public string NRO_SERVICIO { get; set; }
           [XmlElement(ElementName = "TIPO_DOC_IDENTIDAD")]
           public string TIPO_DOC_IDENTIDAD { get; set; }
           [XmlElement(ElementName = "FECHA_SOLICITUD")]
           public string FECHA_SOLICITUD { get; set; }
           [XmlElement(ElementName = "CASO_INTER")]
           public string CASO_INTER { get; set; }
           [XmlElement(ElementName = "CONTACTO_CLIENTE")]
           public string CONTACTO_CLIENTE { get; set; }
           [XmlElement(ElementName = "NRO_DOC_IDENTIDAD")]
           public string NRO_DOC_IDENTIDAD { get; set; }
           [XmlElement(ElementName = "TRANSACCION")]
           public string TRANSACCION { get; set; }
           [XmlElement(ElementName = "TIPO_CLIENTE")]
           public string TIPO_CLIENTE { get; set; }
           [XmlElement(ElementName = "NRO_CLARO")]
           public string NRO_CLARO { get; set; }
           [XmlElement(ElementName = "HABILITAR_SERVICIO")]
           public string HABILITAR_SERVICIO { get; set; }
           [XmlElement(ElementName = "RESTRINGIR_SERVICIO")]
           public string RESTRINGIR_SERVICIO { get; set; }
           [XmlElement(ElementName = "MEDIO_PERMITIDO")]
           public string MEDIO_PERMITIDO { get; set; }
           [XmlElement(ElementName = "MEDIO_NO_PERMITIDO")]
           public string MEDIO_NO_PERMITIDO { get; set; }
           [XmlElement(ElementName = "ENVIO_CORREO")]
           public string ENVIO_CORREO { get; set; }
           [XmlElement(ElementName = "EMAIL")]
           public string EMAIL { get; set; }
           [XmlElement(ElementName = "CORREO_SOLICITUD")]
           public string CORREO_SOLICITUD { get; set; }
           [XmlElement(ElementName = "COD_AGENTE")]
           public string COD_AGENTE { get; set; }
           [XmlElement(ElementName = "NOMBRE_ASESOR")]
           public string NOMBRE_ASESOR { get; set; }
           [XmlElement(ElementName = "LINEAS_ASOCIADAS")]
           public List<string> LINEAS_ASOCIADAS { get; set; }
       }
    #endregion


       public class RegistroNoCliente {
           public string idSession { get; set; }
           public string Telefono { get; set; }

           public string Usuario { get; set; }

           public string Nombre { get; set; }

           public string Apellidos { get; set; }

           public string RazonSocial { get; set; }

           public string DocumentoTipo { get; set; }

           public string DocumentoNumero { get; set; }

           public string Direccion { get; set; }

           public string Distrito { get; set; }

           public string Departamento { get; set; }

           public string Modalidad { get; set; }
       }

}

