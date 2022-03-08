using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.Redirect
{
    [DataContract]
    public class Cliente
    {
        private string _OBJID_CONTACTO;
        private string _OBJID_SITE;
        private string _TELEFONO;
        private string _CUENTA;
        private string _MODALIDAD;
        private string _SEGMENTO;
        private string _ROL_CONTACTO;
        private string _ESTADO_CONTACTO;
        private string _ESTADO_CONTRATO;
        private string _ESTADO_SITE;
        private string _S_NOMBRES;
        private string _S_APELLIDOS;
        private string _NOMBRES = "";
        private string _APELLIDOS = "";
        private string _NOMBRE_COMPLETO = "";
        private string _DOMICILIO;
        private string _URBANIZACION;
        private string _REFERENCIA;
        private string _CIUDAD;
        private string _DISTRITO;
        private string _DEPARTAMENTO;
        private string _ZIPCODE;
        private string _EMAIL;
        private string _TELEF_REFERENCIA;
        private string _FAX;
        private string _FECHA_NAC;
        private string _SEXO;
        private string _ESTADO_CIVIL;
        private string _ESTADO_CIVIL_ID;
        private string _TIPO_DOC;
        private string _NRO_DOC;
        private DateTime _FECHA_ACT;
        private string _PUNTO_VENTA;
        private int _FLAG_REGISTRADO;
        private string _OCUPACION;
        private string _CANT_REG;
        private string _FLAG_EMAIL;
        private string _MOTIVO_REGISTRO;
        private string _FUNCION;
        private string _CARGO;
        private string _LUGAR_NACIMIENTO_ID;
        private string _LUGAR_NACIMIENTO_DES;

        private string _P_FLAG_CONSULTA;
        private string _P_MSG_TEXT;
        private string _USUARIO;
        private string _RAZON_SOCIAL;
        private string _DNI_RUC;
        private string _PROVINCIA;

        private string _ASESOR;
        private string _CONSULTOR;
        private string _CICLO_FACTURACION;
        private string _CREDIT_SCORE;
        private string _TOTAL_CUENTAS;
        private string _DEPOSITO;
        private string _ESTADO_CUENTA;
        private string _RENTA;
        private string _TIPO_CLIENTE;
        private string _TOTAL_LINEAS;
        private string _LIMITE_CREDITO;
        private string _RESPONSABLE_PAGO;
        private string _REPRESENTANTE_LEGAL;

        private string _CONTACTO_CLIENTE;
        private string _TELEFONO_CONTACTO;
        private string _CALLE_FAC;
        private string _POSTAL_FAC;
        private string _URBANIZACION_FAC;
        private string _DEPARTEMENTO_FAC;
        private string _PROVINCIA_FAC;
        private string _DISTRITO_FAC;
        private string _NOMBRE_COMERCIAL;
        private string _CALLE_LEGAL;
        private string _POSTAL_LEGAL;
        private string _URBANIZACION_LEGAL;
        private string _DEPARTEMENTO_LEGAL;
        private string _PROVINCIA_LEGAL;
        private string _DISTRITO_LEGAL;
        private string _PAIS_LEGAL;
        private string _PAIS_FAC;
        private string _CUSTOMER_ID;
        private string _CONTRATO_ID;
        private string _NICHO;
        private string _DIRECCION_DESPACHO;
        private string _FORMA_PAGO;
        private string _COD_TIPO_CLIENTE;


        private string _TEXTO_NOTAS;
        private string _SEGMENTO2;
        private string _LONG_NRO_DOC;
        private string _USU_ASEG;
        private long _NACIONALIDAD;

        public Cliente() { }


        [DataMember(Name = "PaymentMethod")]
        public string FORMA_PAGO
        {
            set { _FORMA_PAGO = value; }
            get { return _FORMA_PAGO; }
        }
        [DataMember(Name = "CodCustomerType")]
        public string COD_TIPO_CLIENTE
        {
            set { _COD_TIPO_CLIENTE = value; }
            get { return _COD_TIPO_CLIENTE; }
        }

        public string TEXTO_NOTAS
        {
            set { _TEXTO_NOTAS = value; }
            get { return _TEXTO_NOTAS; }
        }

        [DataMember(Name = "OfficeAddress")]
        public string DIRECCION_DESPACHO
        {
            set { _DIRECCION_DESPACHO = value; }
            get { return _DIRECCION_DESPACHO; }
        }
        public string NICHO
        {
            set { _NICHO = value; }
            get { return _NICHO; }
        }

        [DataMember(Name = "ContractID")]
        public string CONTRATO_ID
        {
            set { _CONTRATO_ID = value; }
            get { return _CONTRATO_ID; }
        }

        [DataMember(Name = "CustomerID")]
        public string CUSTOMER_ID
        {
            set { _CUSTOMER_ID = value; }
            get { return _CUSTOMER_ID; }
        }

        [DataMember(Name = "LegalCountry")]
        public string PAIS_LEGAL
        {
            set { _PAIS_LEGAL = value; }
            get { return _PAIS_LEGAL; }
        }

        [DataMember(Name = "InvoiceCountry")]
        public string PAIS_FAC
        {
            set { _PAIS_FAC = value; }
            get { return _PAIS_FAC; }
        }

        [DataMember(Name = "LegalDepartament")]
        public string DEPARTEMENTO_LEGAL
        {
            set { _DEPARTEMENTO_LEGAL = value; }
            get { return _DEPARTEMENTO_LEGAL; }
        }

        [DataMember(Name = "LegalProvince")]
        public string PROVINCIA_LEGAL
        {
            set { _PROVINCIA_LEGAL = value; }
            get { return _PROVINCIA_LEGAL; }
        }

        [DataMember(Name = "LegalDistrict")]
        public string DISTRITO_LEGAL
        {
            set { _DISTRITO_LEGAL = value; }
            get { return _DISTRITO_LEGAL; }
        }

        [DataMember(Name = "LegalAddress")]
        public string CALLE_LEGAL
        {
            set { _CALLE_LEGAL = value; }
            get { return _CALLE_LEGAL; }
        }
        [DataMember(Name = "LegalPostal")]
        public string POSTAL_LEGAL
        {
            set { _POSTAL_LEGAL = value; }
            get { return _POSTAL_LEGAL; }
        }

        [DataMember(Name = "LegalUrbanization")]
        public string URBANIZACION_LEGAL
        {
            set { _URBANIZACION_LEGAL = value; }
            get { return _URBANIZACION_LEGAL; }
        }

        [DataMember(Name = "Tradename")]
        public string NOMBRE_COMERCIAL
        {
            set { _NOMBRE_COMERCIAL = value; }
            get { return _NOMBRE_COMERCIAL; }
        }

        [DataMember(Name = "InvoiceDepartament")]
        public string DEPARTEMENTO_FAC
        {
            set { _DEPARTEMENTO_FAC = value; }
            get { return _DEPARTEMENTO_FAC; }
        }

        [DataMember(Name = "InvoiceProvince")]
        public string PROVINCIA_FAC
        {
            set { _PROVINCIA_FAC = value; }
            get { return _PROVINCIA_FAC; }
        }

        [DataMember(Name = "InvoiceDistrict")]
        public string DISTRITO_FAC
        {
            set { _DISTRITO_FAC = value; }
            get { return _DISTRITO_FAC; }
        }

        [DataMember(Name = "InvoiceAddress")]
        public string CALLE_FAC
        {
            set { _CALLE_FAC = value; }
            get { return _CALLE_FAC; }
        }
        [DataMember(Name = "InvoicePostal")]
        public string POSTAL_FAC
        {
            set { _POSTAL_FAC = value; }
            get { return _POSTAL_FAC; }
        }

        [DataMember(Name = "InvoiceUrbanization")]
        public string URBANIZACION_FAC
        {
            set { _URBANIZACION_FAC = value; }
            get { return _URBANIZACION_FAC; }
        }

        [DataMember(Name = "CustomerContact")]
        public string CONTACTO_CLIENTE
        {
            set { _CONTACTO_CLIENTE = value; }
            get { return _CONTACTO_CLIENTE; }
        }

        [DataMember(Name = "PhoneContact")]
        public string TELEFONO_CONTACTO
        {
            set { _TELEFONO_CONTACTO = value; }
            get { return _TELEFONO_CONTACTO; }
        }

        [DataMember(Name = "LegalAgent")]
        public string REPRESENTANTE_LEGAL
        {
            set { _REPRESENTANTE_LEGAL = value; }
            get { return _REPRESENTANTE_LEGAL; }
        }
        [DataMember(Name = "ResponsiblePayment")]
        public string RESPONSABLE_PAGO
        {
            set { _RESPONSABLE_PAGO = value; }
            get { return _RESPONSABLE_PAGO; }
        }
        [DataMember(Name = "CreditLimit")]
        public string LIMITE_CREDITO
        {
            set { _LIMITE_CREDITO = value; }
            get { return _LIMITE_CREDITO; }
        }

        [DataMember(Name = "Assessor")]
        public string ASESOR
        {
            set { _ASESOR = value; }
            get { return _ASESOR; }
        }
        public string CONSULTOR
        {
            set { _CONSULTOR = value; }
            get { return _CONSULTOR; }
        }

        [DataMember(Name = "BillingCycle")]
        public string CICLO_FACTURACION
        {
            set { _CICLO_FACTURACION = value; }
            get { return _CICLO_FACTURACION; }
        }
        public string CREDIT_SCORE
        {
            set { _CREDIT_SCORE = value; }
            get { return _CREDIT_SCORE; }
        }
        public string TOTAL_CUENTAS
        {
            set { _TOTAL_CUENTAS = value; }
            get { return _TOTAL_CUENTAS; }
        }
        public string DEPOSITO
        {
            set { _DEPOSITO = value; }
            get { return _DEPOSITO; }
        }
        [DataMember(Name = "AccountStatus")]
        public string ESTADO_CUENTA
        {
            set { _ESTADO_CUENTA = value; }
            get { return _ESTADO_CUENTA; }
        }
        public string RENTA
        {
            set { _RENTA = value; }
            get { return _RENTA; }
        }
        [DataMember(Name = "CustomerType")]
        public string TIPO_CLIENTE
        {
            set { _TIPO_CLIENTE = value; }
            get { return _TIPO_CLIENTE; }
        }
        public string TOTAL_LINEAS
        {
            set { _TOTAL_LINEAS = value; }
            get { return _TOTAL_LINEAS; }
        }

        [DataMember(Name = "ContactCode")]
        public string OBJID_CONTACTO
        {
            set { _OBJID_CONTACTO = value; }
            get { return _OBJID_CONTACTO; }
        }
        [DataMember(Name = "SiteCode")]
        public string OBJID_SITE
        {
            set { _OBJID_SITE = value; }
            get { return _OBJID_SITE; }
        }

        [DataMember(Name = "Telephone")]
        public string TELEFONO
        {
            set { _TELEFONO = value; }
            get { return _TELEFONO; }
        }

        [DataMember(Name = "Account")]
        public string CUENTA
        {
            set { _CUENTA = value; }
            get { return _CUENTA; }
        }
        [DataMember(Name = "Modality")]
        public string MODALIDAD
        {
            set { _MODALIDAD = value; }
            get { return _MODALIDAD; }
        }
        public string SEGMENTO
        {
            set { _SEGMENTO = value; }
            get { return _SEGMENTO; }
        }
        public string ROL_CONTACTO
        {
            set { _ROL_CONTACTO = value; }
            get { return _ROL_CONTACTO; }
        }
        public string ESTADO_CONTACTO
        {
            set { _ESTADO_CONTACTO = value; }
            get { return _ESTADO_CONTACTO; }
        }
        public string ESTADO_CONTRATO
        {
            set { _ESTADO_CONTRATO = value; }
            get { return _ESTADO_CONTRATO; }
        }
        public string ESTADO_SITE
        {
            set { _ESTADO_SITE = value; }
            get { return _ESTADO_SITE; }
        }
        public string S_NOMBRES
        {
            set { _S_NOMBRES = value; }
            get { return _S_NOMBRES; }
        }
        public string S_APELLIDOS
        {
            set { _S_APELLIDOS = value; }
            get { return _S_APELLIDOS; }
        }

        [DataMember(Name = "Name")]
        public string NOMBRES
        {
            set { _NOMBRES = value; }
            get { return _NOMBRES; }
        }

        [DataMember(Name = "LastName")]
        public string APELLIDOS
        {
            set { _APELLIDOS = value; }
            get { return _APELLIDOS; }
        }

        [DataMember(Name = "Address")]
        public string DOMICILIO
        {
            set { _DOMICILIO = value; }
            get { return _DOMICILIO; }
        }
        [DataMember(Name = "Urbanization")]
        public string URBANIZACION
        {
            set { _URBANIZACION = value; }
            get { return _URBANIZACION; }
        }

        [DataMember(Name = "Reference")]
        public string REFERENCIA
        {
            set { _REFERENCIA = value; }
            get { return _REFERENCIA; }
        }
        public string CIUDAD
        {
            set { _CIUDAD = value; }
            get { return _CIUDAD; }
        }

        [DataMember(Name = "District")]
        public string DISTRITO
        {
            set { _DISTRITO = value; }
            get { return _DISTRITO; }
        }

        [DataMember(Name = "Departament")]
        public string DEPARTAMENTO
        {
            set { _DEPARTAMENTO = value; }
            get { return _DEPARTAMENTO; }
        }
        public string ZIPCODE
        {
            set { _ZIPCODE = value; }
            get { return _ZIPCODE; }
        }

        [DataMember(Name = "Email")]
        public string EMAIL
        {
            set { _EMAIL = value; }
            get { return _EMAIL; }
        }

        [DataMember(Name = "PhoneReference")]
        public string TELEF_REFERENCIA
        {
            set { _TELEF_REFERENCIA = value; }
            get { return _TELEF_REFERENCIA; }
        }

        [DataMember(Name = "Fax")]
        public string FAX
        {
            set { _FAX = value; }
            get { return _FAX; }
        }

        [DataMember(Name = "BirthDate")]
        public string FECHA_NAC
        {
            set { _FECHA_NAC = value; }
            get { return _FECHA_NAC; }
        }
        [DataMember(Name = "Sex")]
        public string SEXO
        {
            set { _SEXO = value; }
            get { return _SEXO; }
        }

        [DataMember(Name = "CivilStatus")]
        public string ESTADO_CIVIL
        {
            set { _ESTADO_CIVIL = value; }
            get { return _ESTADO_CIVIL; }
        }
        [DataMember(Name = "CivilStatusID")]
        public string ESTADO_CIVIL_ID
        {
            set { _ESTADO_CIVIL_ID = value; }
            get { return _ESTADO_CIVIL_ID; }
        }

        [DataMember(Name = "DocumentType")]
        public string TIPO_DOC
        {
            set { _TIPO_DOC = value; }
            get { return _TIPO_DOC; }
        }

        [DataMember(Name = "DocumentNumber")]
        public string NRO_DOC
        {
            set { _NRO_DOC = value; }
            get { return _NRO_DOC; }
        }
        public DateTime FECHA_ACT
        {
            set { _FECHA_ACT = value; }
            get { return _FECHA_ACT; }
        }
        public string PUNTO_VENTA
        {
            set { _PUNTO_VENTA = value; }
            get { return _PUNTO_VENTA; }
        }
        public int FLAG_REGISTRADO
        {
            set { _FLAG_REGISTRADO = value; }
            get { return _FLAG_REGISTRADO; }
        }
        public string OCUPACION
        {
            set { _OCUPACION = value; }
            get { return _OCUPACION; }
        }
        public string CANT_REG
        {
            set { _CANT_REG = value; }
            get { return _CANT_REG; }
        }
        public string FLAG_EMAIL
        {
            set { _FLAG_EMAIL = value; }
            get { return _FLAG_EMAIL; }
        }
        public string P_FLAG_CONSULTA
        {
            set { _P_FLAG_CONSULTA = value; }
            get { return _P_FLAG_CONSULTA; }
        }
        public string P_MSG_TEXT
        {
            set { _P_MSG_TEXT = value; }
            get { return _P_MSG_TEXT; }
        }
        public string USUARIO
        {
            set { _USUARIO = value; }
            get { return _USUARIO; }
        }

        public string MOTIVO_REGISTRO
        {
            set { _MOTIVO_REGISTRO = value; }
            get { return _MOTIVO_REGISTRO; }
        }
        public string FUNCION
        {
            set { _FUNCION = value; }
            get { return _FUNCION; }
        }

        [DataMember(Name = "Position")]
        public string CARGO
        {
            set { _CARGO = value; }
            get { return _CARGO; }
        }

        [DataMember(Name = "BirthPlaceID")]
        public string LUGAR_NACIMIENTO_ID
        {
            set { _LUGAR_NACIMIENTO_ID = value; }
            get { return _LUGAR_NACIMIENTO_ID; }
        }
        [DataMember(Name = "BirthPlace")]
        public string LUGAR_NACIMIENTO_DES
        {
            set { _LUGAR_NACIMIENTO_DES = value; }
            get { return _LUGAR_NACIMIENTO_DES; }
        }

        [DataMember(Name = "FullName")]
        public string NOMBRE_COMPLETO
        {
            set { _NOMBRE_COMPLETO = value; }
            get
            {
                if (string.IsNullOrEmpty(_NOMBRE_COMPLETO))
                    _NOMBRE_COMPLETO = _NOMBRES + " " + _APELLIDOS;
                return _NOMBRE_COMPLETO.Trim();
            }
        }

        [DataMember(Name = "BusinessName")]
        public string RAZON_SOCIAL
        {
            set { _RAZON_SOCIAL = value; }
            get { return _RAZON_SOCIAL; }
        }
        [DataMember(Name = "DNIRUC")]
        public string DNI_RUC
        {
            set { _DNI_RUC = value; }
            get { return _DNI_RUC; }
        }

        [DataMember(Name = "Province")]
        public string PROVINCIA
        {
            set { _PROVINCIA = value; }
            get { return _PROVINCIA; }
        }

        [DataMember(Name = "Segment2")]
        public string SEGMENTO2
        {
            set { _SEGMENTO2 = value; }
            get { return _SEGMENTO2; }
        }

        public string LONG_NRO_DOC
        {
            set { _LONG_NRO_DOC = value; }
            get { return _LONG_NRO_DOC; }
        }

        public string USU_ASEG
        {
            set { _USU_ASEG = value; }
            get { return _USU_ASEG; }
        }

        public long NACIONALIDAD
        {
            set { _NACIONALIDAD = value; }
            get { return _NACIONALIDAD; }
        }

        #region Redireccionamiento SU
        DataAccountModel _objPostDataAccount;

        [DataMember(Name = "objPostDataAccount")]
        public DataAccountModel objPostDataAccount
        {
            set { _objPostDataAccount = value; }
            get { return _objPostDataAccount; }
        }

        [DataMember(Name = "ActivationDate")]
        protected string FECHA_ACTSTRING
        {
            set
            {
                DateTime.TryParse(value, out _FECHA_ACT);
            }
            get { return _FECHA_ACT.ToString(); }
        }

        #endregion
    }
}