using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.Redirect
{
    [System.Serializable()]
    public class BEUrlParams
    {

        #region Variables
        private string _ContratoID;
        private string _Telefono;
        private string _CustomerID;
        private string _TelefonoRef;
        private string _CodCliente;
        private string _nroDocumento, _telefonoSar, _cu, _perfiles, _pstrTelefono, _pstrContratoID;
        #endregion

        public BESessionParams SessionParams { get; set; }
        public string OPTIONREDIRECT { get; set; }
        public string ContratoID
        {
            get
            {
                if (SessionParams.K_CLAVE_DATOS_CLIENTE != null)
                {
                    _ContratoID = SessionParams.K_CLAVE_DATOS_CLIENTE.CONTRATO_ID;
                    if (_ContratoID == null)
                    {
                        _ContratoID = " ";
                    }
                }
                return _ContratoID;
            }
            set { _ContratoID = value; }
        }
        public string CustomerID
        {
            get
            {
                if (SessionParams.K_CLAVE_DATOS_CLIENTE != null)
                {
                    _CustomerID = SessionParams.K_CLAVE_DATOS_CLIENTE.CUSTOMER_ID;
                }
                return _CustomerID;
            }
            set { _CustomerID = value; }
        }

        public string suRedirect { get; set; }
        public string tipoApp { get; set; }
        public string transaccion { get; set; }
        public string cu
        {
            get
            {
                if (SessionParams.K_USUARIO_ACCESO != null)
                {
                    _cu = SessionParams.K_USUARIO_ACCESO.CodigoUsuario;
                }
                return _cu;
            }
            set
            {
                _cu = value;
            }
        }
        public string co { get; set; }
        public string ca { get; set; }

        public string telefono
        {
            get
            {
                if (SessionParams.K_CLAVE_DATOS_LINEA != null)
                {
                    _Telefono = SessionParams.K_CLAVE_DATOS_LINEA.NroCelular;
                    if (String.Equals(OPTIONREDIRECT, "SU_ACP_GAB", StringComparison.OrdinalIgnoreCase))
                    {
                        _Telefono = SessionParams.K_CLAVE_DATOS_CLIENTE.TELEF_REFERENCIA;
                    }
                    else if (String.Equals(OPTIONREDIRECT, "SU_ACP_DTGEC", StringComparison.OrdinalIgnoreCase))
                    {
                        _Telefono = string.Format("M{0}", CustomerID);
                    }
                    else if (_Telefono == null)
                    {
                        _Telefono = " ";
                    }
                }
                return _Telefono;
            }
            set
            {
                _Telefono = value;
            }
        }

        public string perfiles
        {
            get
            {
                if (SessionParams.K_USUARIO_ACCESO != null)
                {
                    _perfiles = SessionParams.K_USUARIO_ACCESO.CadenaPerfil;
                }
                return _perfiles;
            }
            set
            {
                _perfiles = value;
            }
        }
        public string alturaVentana { get; set; }
        public string anchoVentana { get; set; }
        public string CodCliente
        {
            get
            {
                if (SessionParams.K_CLAVE_DATOS_CLIENTE != null)
                {
                    _CodCliente = SessionParams.K_CLAVE_DATOS_CLIENTE.CUENTA;
                }
                return _CodCliente;
            }
            set
            {
                _CodCliente = value;
            }
        }

        public string TelefonoRef
        {
            get
            {
                if (SessionParams.K_CLAVE_DATOS_CLIENTE != null)
                {
                    _TelefonoRef = SessionParams.K_CLAVE_DATOS_CLIENTE.TELEF_REFERENCIA;
                }
                return _TelefonoRef;
            }
            set { _TelefonoRef = value; }
        }
        public string pstrTipo { get; set; }



        public string op { get; set; }
        public string nroDocumento
        {
            get
            {
                if (SessionParams.K_CLAVE_DATOS_CLIENTE != null)
                {
                    _nroDocumento = SessionParams.K_CLAVE_DATOS_CLIENTE.NRO_DOC;
                }
                return _nroDocumento;
            }
            set
            {
                _nroDocumento = value;
            }
        }
        public string critBusqueda { get; set; }
        public string tipoCasoInteraccion { get; set; }
        public string casoInteraccionId { get; set; }
        public string EstadoForm { get; set; }
        public string tipocontacto { get; set; }
        public string llamadoDesdeIndex { get; set; }
        public string flagClienteAntiguo { get; set; }
        public string modalidad { get; set; }
        public string ContactoId { get; set; }
        public string ClaseId { get; set; }
        public string SubClaseId { get; set; }
        public string TipoId { get; set; }
        public string Accion { get; set; }
        public string Tipodoc { get; set; }
        public string Nrodoc { get; set; }
        public string pstrTelRefer { get; set; }
        public string pSaldo { get; set; }
        public string DatosConstCanje { get; set; }
        public string NroTelefono { get; set; }
        public string PSTRTELREFERNO { get; set; }
        public string InvoiceNumber { get; set; }
        public string tiposervicio { get; set; }
        public string to { get; set; }
        public string ActualizaDatos { get; set; }
        public string Origen { get; set; }
        public string CodUsr { get; set; }
        public string migracion { get; set; }
        public string pstrNota { get; set; }
        public string esRetencion { get; set; }
        public string dth { get; set; }
        public string descripcion { get; set; }
        public string tipobloqueo { get; set; }
        public string tipotx { get; set; }
        public string pstrCodCustomerID
        {
            get
            {
                if (SessionParams.K_CLAVE_DATOS_CLIENTE != null)
                {
                    _CustomerID = SessionParams.K_CLAVE_DATOS_CLIENTE.CUSTOMER_ID;
                }
                return _CustomerID;
            }
            set { _CustomerID = value; }
        }
        public string pstrCodCliente { get; set; }
        public string ID { get; set; }
        public string NroSOTExterno { get; set; }
        public string strCargaInmediata { get; set; }
        public string Opcion { get; set; }
        public string TipoCustomerID { get; set; }
        public string telefonoSar
        {
            get
            {
                if (SessionParams.K_CLAVE_DATOS_LINEA != null)
                {
                    _telefonoSar = SessionParams.K_CLAVE_DATOS_LINEA.NroCelular;
                }
                return _telefonoSar;
            }
            set
            {
                _telefonoSar = value;
            }
        }
        public string pstrTelefono
        {
            get
            {
                if (SessionParams.K_CLAVE_DATOS_LINEA != null)
                {
                    _pstrTelefono = SessionParams.K_CLAVE_DATOS_LINEA.NroCelular;
                }
                return _pstrTelefono;
            }
            set
            {
                _pstrTelefono = value;
            }
        }

        public string pstrContratoID
        {
            get
            {

                if (SessionParams.K_CLAVE_DATOS_CLIENTE != null)
                {
                    _pstrContratoID = SessionParams.K_CLAVE_DATOS_CLIENTE.CONTRATO_ID;
                }
                return _pstrContratoID;
            }
            set { _pstrContratoID = value; }

        }
        public string src { get; set; }
    }
}