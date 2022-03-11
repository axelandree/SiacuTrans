using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.Redirect
{
    [DataContract]
    public class BESessionParams
    {

        Cliente _DatosCliente;
        Linea _DatosLinea;
        string _fEstadoLinea, _OBJIDCONTACT, _Accion, _Plan;
        string _codUsuario, _UsuarioConsulta, _codPerfil, _NombreApellido, _NombreUsuario, _NombreArea, _AccesoPagina, _Login, _loginM;
        BEUsuarioSession _DatosUsuario;

        public string codUsuario
        {
            get
            {
                if (_DatosUsuario != null)
                {
                    _codUsuario = _DatosUsuario.CodigoUsuario;
                }
                return _codUsuario;
            }
            set
            {
                _codUsuario = value;
            }
        }
        public string UsuarioConsulta
        {
            get
            {
                if (_DatosUsuario != null)
                {
                    _UsuarioConsulta = _DatosUsuario.UsuarioConsulta;
                }
                return _UsuarioConsulta;
            }
            set
            {
                _UsuarioConsulta = value;
            }
        }
        public string codPerfil
        {
            get
            {
                if (_DatosUsuario != null)
                {
                    _codPerfil = _DatosUsuario.CodigoPerfil;
                }
                return _codPerfil;
            }
            set
            {
                _codPerfil = value;
            }
        }
        public string NombreApellido
        {
            get
            {
                if (_DatosUsuario != null)
                {
                    _NombreApellido = _DatosUsuario.NombreCompleto;
                }
                return _NombreApellido;
            }
            set
            {
                _NombreApellido = value;
            }
        }
        public string NombreUsuario
        {
            get
            {
                if (_DatosUsuario != null)
                {
                    _NombreUsuario = _DatosUsuario.NombreCompleto;
                }
                return _NombreUsuario;
            }
            set
            {
                _NombreUsuario = value;
            }
        }

        public string NombreArea
        {
            get
            {
                if (_DatosUsuario != null)
                {
                    _NombreArea = _DatosUsuario.AreaDes;
                }
                return _NombreArea;
            }
            set
            {
                _NombreArea = value;
            }
        }
        public string loginM
        {
            get
            {
                if (_DatosUsuario != null)
                {
                    _loginM = _DatosUsuario.CodigoUsuarioRed;
                }
                return _loginM;
            }
            set
            {
                _loginM = value;
            }
        }
        [DataMember(Name = "BUSQINSTANT")]
        public string BusqInstantaneas { get; set; }
        public string AccesoPagina
        {
            get
            {
                if (_DatosUsuario != null)
                {
                    _AccesoPagina = _DatosUsuario.CadenaOpciones;
                }
                return _AccesoPagina;
            }
            set
            {
                _AccesoPagina = value;
            }
        }
        [DataMember(Name = "TYPESERVICE")]
        public string TipoServicio { get; set; }
        [DataMember(Name = "DATACUSTOMER")]
        public Cliente K_CLAVE_DATOS_CLIENTE//DatosCliente
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
                    _DatosCliente.CICLO_FACTURACION = _DatosCliente.objPostDataAccount.BillingCycle;

                }
            }
        }

        [DataMember(Name = "DATASERVICE")]//DatosLinea
        public Linea K_CLAVE_DATOS_LINEA
        {
            get
            {
                if (_DatosLinea != null)
                {
                    if (K_CLAVE_DATOS_CLIENTE != null)
                    {
                        _DatosLinea.ContratoID = K_CLAVE_DATOS_CLIENTE.CONTRATO_ID;
                    }

                }
                return _DatosLinea;
            }
            set
            {
                _DatosLinea = value;
            }
        }

        public string OBJIDCONTACT
        {
            get
            {
                if (_DatosCliente != null)
                {
                    _OBJIDCONTACT = _DatosCliente.OBJID_CONTACTO;
                }
                return _OBJIDCONTACT;
            }
            set
            {
                _OBJIDCONTACT = value;
            }
        }
        [DataMember(Name = "SERVISBAMBAF")]
        public string ServEsBAM_BAF { get; set; }
        [DataMember(Name = "SERVDTH_MOVIL")]
        public string ServDTH_Movil { get; set; }
        [DataMember(Name = "PORTABILITY")]
        public ArrayList Portabilidad { get; set; }

        public string fEstadoLinea
        {
            get
            {
                if (K_CLAVE_DATOS_LINEA != null)
                {
                    _fEstadoLinea = K_CLAVE_DATOS_LINEA.StatusLinea;
                }
                return _fEstadoLinea;
            }
            set
            {
                _fEstadoLinea = value;
            }
        }

        public string Plan
        {
            get
            {
                if (K_CLAVE_DATOS_LINEA != null)
                {
                    _Plan = K_CLAVE_DATOS_LINEA.Cod_Plan_Tarifario;
                }
                return _Plan;
            }
            set
            {
                _Plan = value;
            }
        }

        public string Accion
        {
            get
            {
                if (K_CLAVE_DATOS_LINEA != null)
                {
                    _Accion = K_CLAVE_DATOS_LINEA.Motivo;
                }
                return _Accion;
            }
            set
            {
                _Accion = value;
            }
        }
        [DataMember(Name = "FLAG_TFI")]
        public string Flag_TFI { get; set; }
        public string Login
        {
            get
            {
                if (_DatosUsuario != null)
                {
                    _Login = _DatosUsuario.CodigoUsuarioRed;
                }
                return _Login;
            }
            set
            {
                _Login = value;
            }
        }
        [DataMember(Name = "USERACCESS")]
        public BEUsuarioSession K_USUARIO_ACCESO
        {
            get { return _DatosUsuario; }
            set { _DatosUsuario = value; }
        }

    }
}