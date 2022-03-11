using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Redirect
{
    [DataContract]
    public class BESessionParams
    {
        private BECliente _DatosCliente;
        private string _NombreApellido;
        private string _TFIJA;
        string _ctaCliente;
        string _contratoValor;
        string _codUsuario;
        string _customerID;
        string _NombreUsuario;
        private string _AccesoPagina;
        private string _AreaDes;
        private string _codPerfil;
        private string _UsuarioConsulta;
        private string _NoEs3Play;
        private bool _EsServicioLTE;
        private string _TipoServicio;

        public string codUsuario
        {
            get
            {
                if (K_USUARIO_ACCESO != null)
                {
                    _codUsuario = K_USUARIO_ACCESO.CodigoUsuario;
                }
                return _codUsuario;
            }
            set { _codUsuario = value; }
        }

        public string lgn { get; set; }

        public string UsuarioConsulta
        {
            get
            {
                if (K_USUARIO_ACCESO != null)
                    _UsuarioConsulta = K_USUARIO_ACCESO.UsuarioConsulta;
                return _UsuarioConsulta;
            }
            set { _UsuarioConsulta = value; }
        }

        public string codPerfil
        {
            get
            {
                if (K_USUARIO_ACCESO != null)
                    _codPerfil = K_USUARIO_ACCESO.CodigoPerfil;
                return _codPerfil;
            }
            set { _codPerfil = value; }
        }

        public string NombreApellido
        {
            get
            {
                if (_DatosCliente != null)
                {
                    _NombreApellido = _DatosCliente.NOMBRE_COMPLETO;
                }
                return _NombreApellido;
            }
            set { _NombreApellido = value; }
        }

        public string NombreUsuario
        {
            get
            {
                if (K_USUARIO_ACCESO != null)
                    _NombreUsuario = K_USUARIO_ACCESO.NombreCompleto;
                return _NombreUsuario;
            }
            set { _NombreUsuario = value; }
        }

        //[DataMember(Name = "NAMEAREA")]
        public string NombreArea
        {
            get
            {
                if (K_USUARIO_ACCESO != null)
                    _AreaDes = K_USUARIO_ACCESO.AreaDes;
                return _AreaDes;
            }
            set { _AreaDes = value; }
        }

        public string loginM { get; set; }
        public string BusqInstantaneas { get; set; }

        //[DataMember(Name = "PAGEACCESS")]
        public string AccesoPagina
        {
            get
            {
                if (K_USUARIO_ACCESO != null)
                    _AccesoPagina = K_USUARIO_ACCESO.CadenaOpciones;
                return _AccesoPagina;
            }
            set { _AccesoPagina = value; }
        }

        public string TipoServicio
        {
            get
            {
                if (K_CLAVE_DATOS_LINEA != null)
                    _TipoServicio = K_CLAVE_DATOS_LINEA.tipoServicio;
                return _TipoServicio;
            }
            set { _TipoServicio = value; }
        }

        [DataMember(Name = "DATACUSTOMER")]
        public BECliente K_CLAVE_DATOS_CLIENTE
        {
            get
            {
                return _DatosCliente;
            }
            set
            {
                _DatosCliente = value;
                if (_DatosCliente != null && _DatosCliente.objPostDataAccount != null)
                {
                    _DatosCliente.LIMITE_CREDITO = _DatosCliente.objPostDataAccount.CreditLimit;
                    _DatosCliente.ESTADO_CUENTA = _DatosCliente.objPostDataAccount.AccountStatus;
                    _DatosCliente.RESPONSABLE_PAGO = _DatosCliente.objPostDataAccount.ResponsiblePayment;
                    _DatosCliente.TIPO_CLIENTE = _DatosCliente.objPostDataAccount.CustomerType;
                    _DatosCliente.TOTAL_LINEAS = _DatosCliente.objPostDataAccount.TotalLines;
                    _DatosCliente.LIMITE_CREDITO = _DatosCliente.objPostDataAccount.CreditLimit;
                    _DatosCliente.MODALIDAD = _DatosCliente.objPostDataAccount.Modality;
                    _DatosCliente.SEGMENTO = _DatosCliente.objPostDataAccount.Segment;
                    _DatosCliente.CICLO_FACTURACION = _DatosCliente.objPostDataAccount.BillingCycle;
                    _DatosCliente.NICHO = _DatosCliente.objPostDataAccount.Niche;
                }
            }
        }

        [DataMember(Name = "DATASERVICE")]
        public BELinea K_CLAVE_DATOS_LINEA { get; set; }

        [DataMember(Name = "USERACCESS")]
        public BEUsuarioSession K_USUARIO_ACCESO { get; set; }

        public string OBJIDCONTACT { get; set; }

        [DataMember(Name = "PORTABILITY")]
        public List<BEPortabilidad> Portabilidad { get; set; }


        public bool EsServicioLTE
        {
            get
            {
                if (K_CLAVE_DATOS_LINEA != null)
                    _EsServicioLTE = K_CLAVE_DATOS_LINEA.EsLTE;
                return _EsServicioLTE;
            }
            set { _EsServicioLTE = value; }
        }

        public string NoEs3Play
        {
            get
            {
                if (K_CLAVE_DATOS_LINEA != null)
                    _NoEs3Play = K_CLAVE_DATOS_LINEA.NoEs3Play;
                return _NoEs3Play;
            }
            set { _NoEs3Play = value; }
        }

        [DataMember(Name = "OPTIONREDIRECT")]
        public string OPTIONREDIRECT { get; set; }
        public string ctaCliente
        {
            get
            {
                if (K_CLAVE_DATOS_CLIENTE != null)
                {
                    _ctaCliente = K_CLAVE_DATOS_CLIENTE.CUENTA;
                }
                return _ctaCliente;
            }
            set
            {
                _ctaCliente = value;
            }
        }

        public string contratoValor
        {
            get
            {
                if (K_CLAVE_DATOS_CLIENTE != null)
                {
                    _contratoValor = K_CLAVE_DATOS_CLIENTE.CONTRATO_ID;
                }
                return _contratoValor;
            }
            set { _contratoValor = value; }
        }
        public string nroTelefono { get; set; }
        public string customerID
        {
            get
            {
                if (K_CLAVE_DATOS_CLIENTE != null)
                {
                    _customerID = K_CLAVE_DATOS_CLIENTE.CUSTOMER_ID;
                }
                return _customerID;
            }
            set { _customerID = value; }
        }
        public double SaldoEstadoCuenta { get; set; }
        public string FechaActivacionPrepago { get; set; }

        public string TFIJA
        {
            get
            {
                if (K_CLAVE_DATOS_LINEA != null)
                {
                    if (K_CLAVE_DATOS_LINEA.telefonia == "T")
                    {
                        _TFIJA = "1";
                    }
                    else { _TFIJA = "0"; }
                }
                return _TFIJA;
            }
            set { _TFIJA = value; }
        }

    }
}
