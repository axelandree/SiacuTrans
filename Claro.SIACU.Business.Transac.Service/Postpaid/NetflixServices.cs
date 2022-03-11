using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Claro.SIACU.Data.Transac.Service.Configuration;
using Claro.SIACU.Entity.Transac.Service.Postpaid.GetNetflixServices;
using Claro.SIACU.Entity.Transac.Service.Postpaid.Legacy.GetTypeProductDat;
using Claro.SIACU.Entity.Transac.Service.Postpaid.Legacy.GetAdditionalServices;

namespace Claro.SIACU.Business.Transac.Service.Postpaid
{
    public class NetflixServices
    {
        /// <summary>
        /// Permite obtener los servicios usados por contrato tecnologia móvil.
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strIdTransaccion"></param>
        /// <param name="intCoId"></param>
        /// <returns></returns>
        public static bool validarAccesoRegistroLinkMovil(string strIdSession, string strIdTransaccion, parametroMovil oParametroMovil)
        {
            bool booEnviarLink = false;
            AdditionalServiceRequest oAdditionalServiceRequest = null;
            TypeProductDatRequest oTypeProductDatRequest = null;
            string strEstado = string.Empty;
            string strIdContratoPublico = string.Empty;

            if (oParametroMovil.strPlataforma.Equals(Constants.TOBE))
            {
                List<RecursoLogico> olisRecursoLogico = new List<RecursoLogico>();
                oTypeProductDatRequest = new TypeProductDatRequest()
                {
                    contrato = new ContratoRequest()
                    {
                        idContrato = null,
                        ofertaProducto = new OfertaProducto()
                        {
                            producto = new Producto()
                            {
                                recursoLogico = new List<RecursoLogico>() { 
                                    new RecursoLogico(){
                                    numeroLinea = oParametroMovil.strTelefono
                                }
                            }
                        }
                        }
                    },
                    Audit = new Claro.Entity.AuditRequest()
                    {
                        ApplicationName = oParametroMovil.Audit.ApplicationName ,
                        IPAddress = oParametroMovil.Audit.IPAddress,
                        Session = oParametroMovil.Audit.Session,
                        Transaction = oParametroMovil.Audit.Transaction,
                        UserName = oParametroMovil.Audit.UserName
                    }
                };
                strIdContratoPublico = Claro.Web.Logging.ExecuteMethod<string>(strIdSession, strIdTransaccion,
                   () =>
                   {
                       return Data.Transac.Service.Postpaid.NetflixServices.obtenerContratoMovilOne(strIdSession, strIdTransaccion, oTypeProductDatRequest);
                   });
                if (strIdContratoPublico != "")
                {
                    oAdditionalServiceRequest = new AdditionalServiceRequest()
                    {
                        obtenerServiciosPlanPorContratoRequest = new obtenerServiciosPlanPorContratoRequest()
                        {
                            contractIdPub = strIdContratoPublico,
                            flagConsulta = "T",
                            listaOpcional = null,
                            validaExcluyente = null
                        },
                        Audit = new Claro.Entity.AuditRequest()
                        {
                            ApplicationName = oParametroMovil.Audit.ApplicationName,
                            IPAddress = oParametroMovil.Audit.IPAddress,
                            Session = oParametroMovil.Audit.Session,
                            Transaction = oParametroMovil.Audit.Transaction,
                            UserName = oParametroMovil.Audit.UserName
                        }
                    };
                    booEnviarLink = Claro.Web.Logging.ExecuteMethod<bool>(strIdSession, strIdTransaccion,
                   () =>
                   {
                       return Data.Transac.Service.Postpaid.NetflixServices.validarAccesoRegistroLinkMovilOne(strIdSession, strIdTransaccion, oParametroMovil.strCodigoServicioOne, oAdditionalServiceRequest);
                   });
                }
            }
            else
            {
                string[] arrSnCode = oParametroMovil.strCodigoServicio.Split(',');
                foreach (var objItem in arrSnCode)
                {
                    strEstado = Claro.Web.Logging.ExecuteMethod<string>(strIdSession, strIdTransaccion,
                   () =>
                   {
                       return Data.Transac.Service.Postpaid.NetflixServices.validarAccesoRegistroLinkMovil(strIdSession, strIdTransaccion, oParametroMovil.intCodigoContrato, Convert.ToInt(objItem));
                   });
                    if (strEstado.Equals(Constants.strLetraA))
                    {
                        booEnviarLink = true;
                        break;
                    }
                }
            }
            return booEnviarLink;
        }
        /// <summary>
        /// Permite realizar el envio de link para la suscripción al servicio Netflix.
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strIdTransaccion"></param>
        /// <param name="oRequest"></param>
        /// <returns></returns>
        public static ServicesNXResponse envioNotificacionRegistroNX(string strIdSession, string strIdTransaccion, ServicesNXRequest oRequest, Claro.Entity.AuditRequest oAuditRequest)
        {
            ServicesNXResponse oResponse = null;
            oResponse = Claro.Web.Logging.ExecuteMethod(strIdSession, strIdTransaccion, () =>
            {
                return Claro.SIACU.Data.Transac.Service.Postpaid.NetflixServices.envioNotificacionRegistroNX(strIdSession, strIdTransaccion, oRequest, oAuditRequest);
            });
            return oResponse;
        }
    }
}
