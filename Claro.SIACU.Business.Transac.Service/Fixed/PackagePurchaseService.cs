using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Claro.SIACU.Entity.Transac.Service.Fixed;
using Claro.SIACU.Entity.Transac.Service.Common.GetComprarPaquetes;
using Claro.SIACU.Entity.Transac.Service.Common.GetPCRFConsultation;
using Claro.SIACU.Entity.Transac.Service.Common.GetConsultarClaroPuntos;
using Claro.SIACU.Entity.Transac.Service.Common.GetConsultarPaqDisponibles;

 //INI - RF-02 Evalenzs

namespace Claro.SIACU.Business.Transac.Service.Fixed
{
    public class PackagePurchaseService
    {
        public static ConsultarClaroPuntosResponse ConsultarClaroPuntos(ConsultarClaroPuntosRequest objRequest)
        {
            ConsultarClaroPuntosResponse objResponse = new ConsultarClaroPuntosResponse();

            Claro.SIACU.Entity.Transac.Service.Common.GetConsultarClaroPuntos.RestConsultarClaroPuntosRequest oRestRequest = new Claro.SIACU.Entity.Transac.Service.Common.GetConsultarClaroPuntos.RestConsultarClaroPuntosRequest
            {
                MessageRequest = new Claro.SIACU.Entity.Transac.Service.Common.GetConsultarClaroPuntos.RestConsultarClaroPuntosMessageRequest
                {
                    Header = new Claro.SIACU.Entity.Transac.Service.Common.GetConsultarClaroPuntos.RestConsultarClaroPuntosHeader
                    {
                        HeaderRequest = new Claro.SIACU.Entity.Transac.Service.Common.GetConsultarClaroPuntos.RestConsultarClaroPuntosHeaderRequest
                        {
                            consumer = objRequest.MessageRequest.Header.HeaderRequest.consumer,
                            country = objRequest.MessageRequest.Header.HeaderRequest.country,
                            dispositivo = objRequest.MessageRequest.Header.HeaderRequest.dispositivo,
                            language = objRequest.MessageRequest.Header.HeaderRequest.language,
                            modulo = objRequest.MessageRequest.Header.HeaderRequest.modulo,
                            msgType = objRequest.MessageRequest.Header.HeaderRequest.msgType,
                            operation = objRequest.MessageRequest.Header.HeaderRequest.operation,
                            pid = objRequest.MessageRequest.Header.HeaderRequest.pid,
                            system = objRequest.MessageRequest.Header.HeaderRequest.system,
                            timestamp = objRequest.MessageRequest.Header.HeaderRequest.timestamp,
                            userId = objRequest.MessageRequest.Header.HeaderRequest.userId,
                            wsIp = objRequest.MessageRequest.Header.HeaderRequest.wsIp
                        }
                    },
                    Body = new Claro.SIACU.Entity.Transac.Service.Common.GetConsultarClaroPuntos.RestConsultarClaroPuntosBodyRequest
                    {

                        tipoPuntos = objRequest.MessageRequest.Body.tipoPuntos,
                        tipoDocumento = objRequest.MessageRequest.Body.tipoDocumento,
                        numeroDocumento = objRequest.MessageRequest.Body.numeroDocumento,
                        bolsa = objRequest.MessageRequest.Body.bolsa,
                        tipoConsulta = objRequest.MessageRequest.Body.tipoConsulta
                    }
                }
            };
            objResponse = Claro.Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
            {
                return Claro.SIACU.Data.Transac.Service.Fixed.PackagePurchaseService.ConsultarClaroPuntos(objRequest, oRestRequest);
            });

            return objResponse;
        }

        public static ConsultarPaqDisponiblesResponse ConsultarPaqDisponibles(ConsultarPaqDisponiblesRequest objRequest)
        {
            ConsultarPaqDisponiblesResponse objResponse = new ConsultarPaqDisponiblesResponse();

            Claro.SIACU.Entity.Transac.Service.Common.GetConsultarPaqDisponibles.RestConsultarPaqDisponiblesRequest oRestRequest = new Claro.SIACU.Entity.Transac.Service.Common.GetConsultarPaqDisponibles.RestConsultarPaqDisponiblesRequest
            {
                MessageRequest = new Claro.SIACU.Entity.Transac.Service.Common.GetConsultarPaqDisponibles.RestConsultarPaqDisponiblesMessageRequest
                {
                    Header = new Claro.SIACU.Entity.Transac.Service.Common.GetConsultarPaqDisponibles.RestConsultarPaqDisponiblesHeader
                    {
                        HeaderRequest = new Claro.SIACU.Entity.Transac.Service.Common.GetConsultarPaqDisponibles.RestConsultarPaqDisponiblesHeaderRequest
                        {
                            consumer = objRequest.MessageRequest.Header.HeaderRequest.consumer,
                            country = objRequest.MessageRequest.Header.HeaderRequest.country,
                            dispositivo = objRequest.MessageRequest.Header.HeaderRequest.dispositivo,
                            language = objRequest.MessageRequest.Header.HeaderRequest.language,
                            modulo = objRequest.MessageRequest.Header.HeaderRequest.modulo,
                            msgType = objRequest.MessageRequest.Header.HeaderRequest.msgType,
                            operation = objRequest.MessageRequest.Header.HeaderRequest.operation,
                            pid = objRequest.MessageRequest.Header.HeaderRequest.pid,
                            system = objRequest.MessageRequest.Header.HeaderRequest.system,
                            timestamp = objRequest.MessageRequest.Header.HeaderRequest.timestamp,
                            userId = objRequest.MessageRequest.Header.HeaderRequest.userId,
                            wsIp = objRequest.MessageRequest.Header.HeaderRequest.wsIp
                        }
                    },
                    Body = new Claro.SIACU.Entity.Transac.Service.Common.GetConsultarPaqDisponibles.RestConsultarPaqDisponiblesBodyRequest
                    {

                        idCategoria = objRequest.MessageRequest.Body.idCategoria,
                        idContrato = objRequest.MessageRequest.Body.idContrato,
                        codigoCategoria = objRequest.MessageRequest.Body.codigoCategoria,
                        prepagoCode = objRequest.MessageRequest.Body.prepagoCode,
                        tmCode = objRequest.MessageRequest.Body.tmCode
                    }
                }
            };
            objResponse = Claro.Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
            {
                return Claro.SIACU.Data.Transac.Service.Fixed.PackagePurchaseService.ConsultarPaqDisponibles(objRequest, oRestRequest);
            });

            return objResponse;
        }

        public static ComprarPaquetesBodyResponse ComprarPaquetes(ComprarPaquetesRequest objRequest)
        {
            ComprarPaquetesBodyResponse objResponse = new ComprarPaquetesBodyResponse();

            Claro.SIACU.Entity.Transac.Service.Common.GetComprarPaquetes.RestComprarPaquetesRequest oRestRequest = new Claro.SIACU.Entity.Transac.Service.Common.GetComprarPaquetes.RestComprarPaquetesRequest
            {
                MessageRequest = new Claro.SIACU.Entity.Transac.Service.Common.GetComprarPaquetes.RestComprarPaquetesMessageRequest
                {
                    Header = new Claro.SIACU.Entity.Transac.Service.Common.GetComprarPaquetes.RestComprarPaquetesHeader
                    {
                        HeaderRequest = new Claro.SIACU.Entity.Transac.Service.Common.GetComprarPaquetes.RestComprarPaquetesHeaderRequest
                        {
                            consumer = objRequest.MessageRequest.Header.HeaderRequest.consumer,
                            country = objRequest.MessageRequest.Header.HeaderRequest.country,
                            dispositivo = objRequest.MessageRequest.Header.HeaderRequest.dispositivo,
                            language = objRequest.MessageRequest.Header.HeaderRequest.language,
                            modulo = objRequest.MessageRequest.Header.HeaderRequest.modulo,
                            msgType = objRequest.MessageRequest.Header.HeaderRequest.msgType,
                            operation = objRequest.MessageRequest.Header.HeaderRequest.operation,
                            pid = objRequest.MessageRequest.Header.HeaderRequest.pid,
                            system = objRequest.MessageRequest.Header.HeaderRequest.system,
                            timestamp = objRequest.MessageRequest.Header.HeaderRequest.timestamp,
                            userId = objRequest.MessageRequest.Header.HeaderRequest.userId,
                            wsIp = objRequest.MessageRequest.Header.HeaderRequest.wsIp
                        }
                    },
                    Body = new Claro.SIACU.Entity.Transac.Service.Common.GetComprarPaquetes.RestComprarPaquetesBodyRequest
                    {
                        comprarPaquetesRequest = new Claro.SIACU.Entity.Transac.Service.Common.GetComprarPaquetes.comprarPaquetesRequest
                        {
                            msisdn = objRequest.MessageRequest.Body.msisdn,
                            monto = objRequest.MessageRequest.Body.monto,
                            paquete = objRequest.MessageRequest.Body.paquete,
                            customerId = objRequest.MessageRequest.Body.customerId,
                            planBase = objRequest.MessageRequest.Body.planBase,
                            tipoProducto = objRequest.MessageRequest.Body.tipoProducto,
                            tipoCliente = objRequest.MessageRequest.Body.tipoCliente,
                            cicloFact = objRequest.MessageRequest.Body.cicloFact,
                            fechaAct = objRequest.MessageRequest.Body.fechaAct,
                            cargoFijo = objRequest.MessageRequest.Body.cargoFijo,
                            tipoPago = objRequest.MessageRequest.Body.tipoPago,
                            departamento = objRequest.MessageRequest.Body.departamento,
                            provincia = objRequest.MessageRequest.Body.provincia,
                            distrito = objRequest.MessageRequest.Body.distrito,
                            listaOpcionalType = new List<ComprarPaquetesListaOpcionalType>(){
                                new ComprarPaquetesListaOpcionalType()
                                {
                                    campo = objRequest.MessageRequest.Body.listaOpcionalType[0].campo,
                                    valor = objRequest.MessageRequest.Body.listaOpcionalType[0].valor
                                }
                            }
                            
                        }
                     
                            
                    }
                }
            };
            objResponse = Claro.Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
            {
                return Claro.SIACU.Data.Transac.Service.Fixed.PackagePurchaseService.ComprarPaquetes(objRequest, oRestRequest);
            });

            return objResponse;
        }
      
        public static PCRFConnectorResponse ConsultarPCRFDegradacion(PCRFConnectorRequest objRequest)    
        {
            var objResponse = new PCRFConnectorResponse();

          try
          {
                 objResponse = Claro.Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction,
                  () =>
                  {
                      return Claro.SIACU.Data.Transac.Service.Fixed.PackagePurchaseService.ConsultarPCRFDegradacion(objRequest);
                  });
         
          }
          catch (Exception ex)
          {
              Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
          }

            return objResponse;
        }

        public static List<Entity.Transac.Service.Common.Client> GetDatosporNroDocumentos(string strIdSession,string strTransaction, string strTipDoc, string strDocumento, string strEstado)
        {
            var objResponse = new List<Entity.Transac.Service.Common.Client>();

          try
          {
              objResponse = Claro.Web.Logging.ExecuteMethod(strIdSession, strTransaction,
                  () =>
                  {
                      return Claro.SIACU.Data.Transac.Service.Fixed.PackagePurchaseService.GetDatosporNroDocumentos(strIdSession, strTransaction, strTipDoc, strDocumento, strEstado);
                  });
         
          }
          catch (Exception ex)
          {
              Web.Logging.Error(strIdSession, strTransaction, ex.Message);
          }

            return objResponse;
        }

        /// <summary>Método que obtiene los número de teléfono del cliente LTE.</summary>
        /// <param name="objRequest"></param>
        /// <returns>PCRFConnectorResponse</returns>
        /// <remarks>ObtenerTelefonosClienteLTE</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Hitss</CreadoPor></item>
        /// <item><FecCrea>06/03/2021.</FecCrea></item></list>
        public static PCRFConnectorResponse ObtenerTelefonosClienteLTE(PCRFConnectorRequest objRequest)
        {
            PCRFConnectorResponse objTelefonosLTE = new PCRFConnectorResponse();
            try
            {
                objTelefonosLTE = Claro.Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Claro.SIACU.Data.Transac.Service.Fixed.PackagePurchaseService.ObtenerTelefonosClienteLTE(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
                throw ex;
            }
            return objTelefonosLTE;
        }
     
    }
}
