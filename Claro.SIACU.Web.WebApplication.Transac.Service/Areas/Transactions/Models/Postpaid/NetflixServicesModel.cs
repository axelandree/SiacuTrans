using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models.Postpaid
{
    public class NetflixServicesModel
    {
        public string strIdSession { get; set; }
        public string strIdTransaccion { get; set; }
        public string linea { get; set; }
        public string notificaEmail { get; set; }
        public string notificaSMS { get; set; }
        public int notifica { get; set; }
        public string email { get; set; }
        public string correo { get; set; }
        public string departamento { get; set; }
        public string provincia { get; set; }
        public string distrito { get; set; }
        public string referencia { get; set; }

        #region "Interaccion"
        public string DNI_RUC { get; set; }
        public string strNombres { get; set; }
        public string strApellidos { get; set; }
        public string strDireccion { get; set; }
        public string strReferencia { get; set; }
        public string strContactoCliente { get; set; }
        public string strfullNameUser { get; set; }
        public string strCacDac { get; set; }
        #endregion

        #region "Tipificacion"
        public string strObjidContacto { get; set; }
        public string tipo { get; set; }
        public string claseDes { get; set; }
        public string subClaseDes { get; set; }
        public string tipoCode { get; set; }
        public string claseCode { get; set; }
        public string subClaseCode { get; set; }
        public string strNote { get; set; }
        public string currentUser { get; set; }
        public string agente { get; set; }
        #endregion

        #region "Mensajes Estados"
        public string strEstadoContratoInactivo { get; set; }
        public string strEstadoContratoSuspendido { get; set; }
        public string strEstadoContratoReservado { get; set; }
        public string strMsjEstadoContratoInactivo { get; set; }
        public string strMsjEstadoContratoSuspendido { get; set; }
        public string strMsjEstadoContratoReservado { get; set; }
        public string strMsjServicioContrato { get; set; }
        #endregion
    }
}