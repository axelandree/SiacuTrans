using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Claro.SIACU.Data.Transac.Service.Configuration;
using Claro.Data;
using ADMCU = Claro.SIACU.ProxyService.Transac.Service.SIACFixed.CrewManagement;
using ConvertHFC = Claro.Convert;
using BE = Claro.SIACU.Entity.Transac.Service.Fixed;
using KEY = Claro.ConfigurationManager;
using System.Data;
using Claro.SIACU.Entity.Transac.Service.Fixed;
using Claro.SIACU.Transac.Service;

namespace Claro.SIACU.Data.Transac.Service.Fixed
{
    public class AdditionalPoints
    {
        public static BE.GetGenericSot.GenericSotResponse GenericSOT(Claro.SIACU.Entity.Transac.Service.Fixed.GetGenericSot.GenericSotRequest objGenericSotRequest)
        {
            Claro.SIACU.Entity.Transac.Service.Fixed.GetGenericSot.GenericSotResponse objGenericSotResponse = new Entity.Transac.Service.Fixed.GetGenericSot.GenericSotResponse();
            DbParameter[] arrParam = {
			   new DbParameter("p_customer_id",DbType.String,22,ParameterDirection.Input,objGenericSotRequest.vCuId),                                                                                                                                                 
			   new DbParameter("p_cod_id",DbType.String,22,ParameterDirection.Input,objGenericSotRequest.vCoId),
			   new DbParameter("p_tiptra",DbType.Int64,300,ParameterDirection.Input,objGenericSotRequest.vTipTra),
               new DbParameter("p_fecprog",DbType.Date,ParameterDirection.Input, ConvertHFC.ToDate(objGenericSotRequest.vFeProg)),
               new DbParameter("p_franja",DbType.String,300,ParameterDirection.Input,objGenericSotRequest.vFranja),
               new DbParameter("p_codmotot",DbType.String,22,ParameterDirection.Input,objGenericSotRequest.vCodMotivo),
               new DbParameter("p_observacion",DbType.String,300,ParameterDirection.Input,objGenericSotRequest.vObserv),
               new DbParameter("p_plano",DbType.String,300,ParameterDirection.Input,objGenericSotRequest.vPlano),
               new DbParameter("p_tiposervico",DbType.Int64,ParameterDirection.Input), 
               new DbParameter("p_usuarioreg",DbType.String,30,ParameterDirection.Input,objGenericSotRequest.vUser),
               new DbParameter("p_cargo",DbType.Double,ParameterDirection.Input),
               new DbParameter("p_cantidad",DbType.Int32,10,ParameterDirection.Input,objGenericSotRequest.vintCantidadAnexo),
               new DbParameter("p_codsolot",DbType.Int64,300,ParameterDirection.Output),
               new DbParameter("p_res_cod",DbType.Int64,300,ParameterDirection.Output),
			   new DbParameter("p_res_desc",DbType.String,1000,ParameterDirection.Output)
        };


            if (objGenericSotRequest.idTipoServ.Length > 0)
            {
                arrParam[8].Value = int.Parse(objGenericSotRequest.idTipoServ);
            }
            if (objGenericSotRequest.cargo != null)
            {
                if (objGenericSotRequest.cargo.Length > 0)
                {
                    arrParam[10].Value = double.Parse(objGenericSotRequest.cargo);
                }
            }



            try
            {
                Web.Logging.ExecuteMethod(objGenericSotRequest.Audit.Session, objGenericSotRequest.Audit.Transaction, () =>
                {
                    DbFactory.ExecuteNonQuery(objGenericSotRequest.Audit.Session, objGenericSotRequest.Audit.Transaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_SP_GENERA_SOT_SIAC, arrParam);

                });



            }
            catch (Exception ex)
            {
                Web.Logging.Error(objGenericSotRequest.Audit.Session, objGenericSotRequest.Audit.Transaction, Claro.SIACU.Transac.Service.Functions.GetExceptionMessage(ex));
            }
            finally
            {
                objGenericSotResponse.rCodSot = arrParam[12].Value.ToString();
                objGenericSotResponse.rCodRes = arrParam[13].Value.ToString();
                objGenericSotResponse.rDescRes = arrParam[14].Value.ToString();
            }


            return objGenericSotResponse;
        }

        public static bool UpdateInter29(string strIdSession, string strTransaction, string p_objid, string p_texto, string p_orden, ref string rFlagInsercion, ref string rMsgText)
        {

            var salida = false;


            DbParameter[] parameters = {
                new DbParameter("P_INTERACT_ID", DbType.Int64,ParameterDirection.Input),
				new DbParameter("P_TEXTO", DbType.String,1000,ParameterDirection.Input),
				new DbParameter("P_ORDEN", DbType.String,1,ParameterDirection.Input),
				new DbParameter("P_FLAG_INSERCION", DbType.String,255,ParameterDirection.Output),
				new DbParameter("P_MSG_TEXT", DbType.String,255,ParameterDirection.Output)
		};


            foreach (var t in parameters)
                t.Value = DBNull.Value;

            var i = 0;
            if (!string.IsNullOrEmpty(p_objid))
                parameters[i].Value = Convert.ToInt64(p_objid);

            if (!string.IsNullOrEmpty(p_texto))
                parameters[1].Value = p_texto;

            if (!string.IsNullOrEmpty(p_orden))
                parameters[2].Value = p_orden;

            try
            {
                Web.Logging.ExecuteMethod(strIdSession, strTransaction, () =>
                {
                    DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_CLARIFY, DbCommandConfiguration.SIACU_SP_UPDATE_INTERACT_X_INTER29, parameters);
                    salida = true;
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
            }
            finally
            {
                rFlagInsercion = parameters[parameters.Length - 2].Value.ToString();
                rMsgText = parameters[parameters.Length - 1].Value.ToString();
            }
            return salida;
        }


        public static BE.GetETAAuditoriaRequestCapacity.BEETAAuditoriaResponseCapacity ConsultarCapacidadCuadrillas(BE.GetETAAuditoriaRequestCapacity.BEETAAuditoriaRequestCapacity objBEETAAuditoriaRequestCapacity)
        {

            string vDurActivity = string.Empty;
            string vTiempoViajeActivity = string.Empty;
            BE.GetETAAuditoriaRequestCapacity.BEETAAuditoriaResponseCapacity Resp = new BE.GetETAAuditoriaRequestCapacity.BEETAAuditoriaResponseCapacity();

            ADMCU.AuditResponse objResponseCuadrillas = new ADMCU.AuditResponse();

            try
            {

                //ADMCU.ebsADMCUAD_CapacityService objServicioCuadrillas = new ADMCU.ebsADMCUAD_CapacityService();
                //objServicioCuadrillas.Url =  KEY.AppSettings("strWebServEtaDirectWebService");

                //objServicioCuadrillas.Credentials = System.Net.CredentialCache.DefaultCredentials;

                //Configuration.WebServiceConfiguration.ADMCUAD_CapacityService.Url = KEY.AppSettings("strWebServEtaDirectWebService");
                //Configuration.WebServiceConfiguration.ADMCUAD_CapacityService.Credentials = System.Net.CredentialCache.DefaultCredentials;


                ADMCU.AuditRequest AuditRequestCuadrillas = new ADMCU.AuditRequest();
                AuditRequestCuadrillas.idTransaccion = objBEETAAuditoriaRequestCapacity.pIdTrasaccion;
                AuditRequestCuadrillas.ipAplicacion = objBEETAAuditoriaRequestCapacity.pIP_APP;
                AuditRequestCuadrillas.nombreAplicacion = objBEETAAuditoriaRequestCapacity.pAPP;
                AuditRequestCuadrillas.usuarioAplicacion = objBEETAAuditoriaRequestCapacity.pUsuario;


                ADMCU.campoActividadType[] ListaCapActiRequestCuadrillas = new ADMCU.campoActividadType[objBEETAAuditoriaRequestCapacity.vCampoActividad.Length];


                String CantidadFechas = String.Empty;
                foreach (DateTime vF in objBEETAAuditoriaRequestCapacity.vFechas)
                {
                    CantidadFechas = CantidadFechas + ";" + vF.ToString();
                }
                //log.Info(string.Format(" Fecha: {0}, Ubicacion: {1}, calcularDuracion: {2}, ", CantidadFechas, vUbicacion[0], vCalcDur ? "true" : "false"));
                //log.Info(string.Format(" calcularTiempoViaje: {0}, calcularHabilidadTrabajo: {1}, determinarUbicacionPorZona: {2}, habilidadTrabajo: {3}", vCalcTiempoViaje ? "true" : "false", vCalcHabTrabajo ? "true" : "false", vObtenerUbiZona ? "true" : "false", vHabilidadTrabajo[0]));
                if (objBEETAAuditoriaRequestCapacity.vCampoActividad.Length > 0)
                {
                    //log.Info(string.Format("ListaCampoActivity(), Elementos: {0}", vCampoActividad.Length));
                    int i = 0;
                    ADMCU.campoActividadType CampoActividadRequestCuadrillas = null;
                    foreach (BE.BEETACampoActivity oCampAct in objBEETAAuditoriaRequestCapacity.vCampoActividad)
                    {
                        CampoActividadRequestCuadrillas = new ADMCU.campoActividadType();
                        CampoActividadRequestCuadrillas.nombre = oCampAct.Nombre;
                        CampoActividadRequestCuadrillas.valor = oCampAct.Valor;
                        ListaCapActiRequestCuadrillas[i] = CampoActividadRequestCuadrillas;
                        //log.Info(string.Format("  objetoCampoActivity -->Indice: {0}, Nombre: {1}, Valor: {2}", i, CampoActividadRequestCuadrillas.nombre, CampoActividadRequestCuadrillas.valor));
                        i++;
                    }
                }
                else
                {
                    //log.Info(string.Format("ListaCampoActivity(), Elementos: 0"));
                    ListaCapActiRequestCuadrillas[0].nombre = "";
                    ListaCapActiRequestCuadrillas[0].valor = "";
                }

                //Listado de Listado Parametro ListaParamRequest
                ADMCU.parametrosRequest oIniParamRequestCuadrillas = new ADMCU.parametrosRequest();
                ADMCU.parametrosRequest[] oParamRequestCuadrillas = new ADMCU.parametrosRequest[] { oIniParamRequestCuadrillas };

                ADMCU.parametrosRequestObjetoRequestOpcional[] ListaParamReqOpcionalCuadrillas = new ADMCU.parametrosRequestObjetoRequestOpcional[objBEETAAuditoriaRequestCapacity.vListaCapReqOpc.Length];

                if (objBEETAAuditoriaRequestCapacity.vListaCapReqOpc.Length > 0)
                {

                    //log.Info(string.Format("ListaRequestOpcionalCapacity(), Elementos: {0}", vListaCapReqOpc.Length));

                    int j = 0, k = 0;
                    foreach (BE.BEETAListaParamRequestOpcionalCapacity oListaParReq in objBEETAAuditoriaRequestCapacity.vListaCapReqOpc)
                    {

                        foreach (BE.BEETAParamRequestCapacity oParamReqCapacity in oListaParReq.ParamRequestCapacities)
                        {
                            ADMCU.parametrosRequestObjetoRequestOpcional oParamRequestOpcionalCuadrillas = new ADMCU.parametrosRequestObjetoRequestOpcional();
                            oParamRequestOpcionalCuadrillas.campo = oParamReqCapacity.Campo;
                            oParamRequestOpcionalCuadrillas.valor = oParamReqCapacity.Valor;
                            ListaParamReqOpcionalCuadrillas[j] = oParamRequestOpcionalCuadrillas;
                            //log.Info(string.Format("  objetoRequestOpcional -->Indice: {0}, Campo: {1}, Valor: {2}", j, oParamRequestOpcionalCuadrillas.campo, oParamRequestOpcionalCuadrillas.valor));
                            j++;
                        }
                        oParamRequestCuadrillas[k].objetoRequestOpcional = ListaParamReqOpcionalCuadrillas;
                        k++;
                    }
                }
                else
                {
                    //log.Info(string.Format("ListaRequestOpcionalCapacity(), Elementos: 0"));
                    ListaParamReqOpcionalCuadrillas[0].campo = "";
                    ListaParamReqOpcionalCuadrillas[0].valor = "";
                    oParamRequestCuadrillas[0].objetoRequestOpcional = ListaParamReqOpcionalCuadrillas;
                }

                ADMCU.capacidadType[] ListaCapacidadTypeCuadrillas = new ADMCU.capacidadType[0];

                ADMCU.parametrosResponse[] ListaParamResponseCuadrillas = new ADMCU.parametrosResponse[0];


                objResponseCuadrillas = Claro.Web.Logging.ExecuteMethod<ADMCU.AuditResponse>(objBEETAAuditoriaRequestCapacity.Audit.Session, objBEETAAuditoriaRequestCapacity.Audit.Transaction, () =>
                {
                    //Configuration.WebServiceConfiguration.ADMCUAD_CapacityService.Url = KEY.AppSettings("strWebServEtaDirectWebService");
                    //Configuration.WebServiceConfiguration.ADMCUAD_CapacityService.Credentials = System.Net.CredentialCache.DefaultCredentials;


                    return Configuration.WebServiceConfiguration.ADMCUAD_CapacityService.consultarCapacidad(AuditRequestCuadrillas,
                                                                                                            objBEETAAuditoriaRequestCapacity.vFechas,
                                                                                                            null,
                                                                                                            objBEETAAuditoriaRequestCapacity.vCalcDur,
                                                                                                            objBEETAAuditoriaRequestCapacity.vCalcDurEspec,
                                                                                                            objBEETAAuditoriaRequestCapacity.vCalcTiempoViaje,
                                                                                                            objBEETAAuditoriaRequestCapacity.vCalcTiempoViajeEspec,
                                                                                                            objBEETAAuditoriaRequestCapacity.vCalcHabTrabajo,
                                                                                                            objBEETAAuditoriaRequestCapacity.vCalcHabTrabajoEspec,
                                                                                                            objBEETAAuditoriaRequestCapacity.vObtenerUbiZona,
                                                                                                            objBEETAAuditoriaRequestCapacity.vObtenerUbiZonaEspec,
                                                                                                            objBEETAAuditoriaRequestCapacity.vEspacioTiempo,
                                                                                                            objBEETAAuditoriaRequestCapacity.vHabilidadTrabajo,
                                                                                                            ListaCapActiRequestCuadrillas,
                                                                                                            oParamRequestCuadrillas,
                                                                                                            out vDurActivity,
                                                                                                            out vTiempoViajeActivity,
                                                                                                            out ListaCapacidadTypeCuadrillas,
                                                                                                            out ListaParamResponseCuadrillas);
                });


                Resp.CodigoRespuesta = objResponseCuadrillas.codigoRespuesta;
                Resp.IdTransaccion = objResponseCuadrillas.idTransaccion;
                Resp.MensajeRespuesta = objResponseCuadrillas.mensajeRespuesta;

                //log.Info(string.Format("Resultado Capacity --> codigoRespuesta: {0},idTransaccion: {1},mensajeRespuesta: {2}", objResponseCuadrillas.codigoRespuesta, objResponseCuadrillas.idTransaccion, objResponseCuadrillas.mensajeRespuesta));
                //log.Info(string.Format("Cantidad de Elementos : {0}", ListaCapacidadTypeCuadrillas.Length));

                string OutDurActivity = vDurActivity;
                string OutTiempoViajeActivity = vTiempoViajeActivity;

                Resp.DuraActivity = vDurActivity;
                Resp.TiempoViajeActivity = vTiempoViajeActivity;

                BE.BEETAEntidadcapacidadType[] oCapacidadTypeM = new BE.BEETAEntidadcapacidadType[ListaCapacidadTypeCuadrillas.Length];
                int l = 0;
                foreach (ADMCU.capacidadType oEntCapacidadType in ListaCapacidadTypeCuadrillas)
                {
                    BE.BEETAEntidadcapacidadType oCapacidadType = new BE.BEETAEntidadcapacidadType();
                    oCapacidadType.Ubicacion = oEntCapacidadType.ubicacion;
                    oCapacidadType.Fecha = oEntCapacidadType.fecha;
                    oCapacidadType.EspacioTiempo = oEntCapacidadType.espacioTiempo;
                    oCapacidadType.HabilidadTrabajo = oEntCapacidadType.habilidadTrabajo;
                    oCapacidadType.Cuota = oEntCapacidadType.cuota;
                    oCapacidadType.Disponible = oEntCapacidadType.disponible;
                    oCapacidadTypeM[l] = oCapacidadType;
                    l++;
                }
                Resp.ObjetoCapacity = oCapacidadTypeM;



            }
            catch (Exception ex)
            {
                //log.Info(string.Format("Error(): {0}", ex.Message));
             //   throw ex;

                Web.Logging.Error(objBEETAAuditoriaRequestCapacity.Audit.Session, objBEETAAuditoriaRequestCapacity.Audit.Transaction, ex.Message);
            }
            return Resp;
        }

        public static int registraEtaRequest(BE.GetRegisterEta.RegisterEtaRequest objRegisterEtaRequest)
        {

            DbParameter[] arrParam = {
		    new DbParameter("vnumslc", DbType.String,20,ParameterDirection.Input,objRegisterEtaRequest.vnumslc),
            new DbParameter("vfecha_venta", DbType.Date,20,ParameterDirection.Input,ConvertHFC.ToDate( objRegisterEtaRequest.vfecha_venta) ),
            new DbParameter("vcod_zona", DbType.Int32,20,ParameterDirection.Input,objRegisterEtaRequest.vcod_zona),
            new DbParameter("vcod_plano", DbType.String,20,ParameterDirection.Input,objRegisterEtaRequest.vcod_plano),
            new DbParameter("vtipo_orden", DbType.String,20,ParameterDirection.Input,objRegisterEtaRequest.vtipo_orden),
            new DbParameter("vsubtipo_orden", DbType.String,20,ParameterDirection.Input,objRegisterEtaRequest.vsubtipo_orden),
            new DbParameter("vidPoblado", DbType.String,20 ,ParameterDirection.Input,objRegisterEtaRequest.vcod_plano),
            new DbParameter("vtiempo_trabajo", DbType.Int32,20,ParameterDirection.Input,objRegisterEtaRequest.vtiempo_trabajo),
			new DbParameter("vidreturn", DbType.Int32,20,ParameterDirection.Output,objRegisterEtaRequest.vidreturn)
        };

            try
            {
                Web.Logging.ExecuteMethod(objRegisterEtaRequest.Audit.Session, objRegisterEtaRequest.Audit.Transaction, () =>
                {
                    DbFactory.ExecuteNonQuery(objRegisterEtaRequest.Audit.Session, objRegisterEtaRequest.Audit.Transaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_SP_REGISTRA_ETA_REQ, arrParam);
                });

            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRegisterEtaRequest.Audit.Session, objRegisterEtaRequest.Audit.Transaction, ex.Message);
            }
            finally
            {
                objRegisterEtaRequest.vidreturn = Convert.ToInt(arrParam[arrParam.Length - 1].Value.ToString());
            }

            return objRegisterEtaRequest.vidreturn;
        }

        public static string registraEtaResponse(BE.GetRegisterEta.RegisterEtaResponse objRegisterEtaResponse)
        {
            DbParameter[] arrParam = {
            new DbParameter("vidconsulta", DbType.Int32,20,ParameterDirection.Input,objRegisterEtaResponse.vidconsulta),
            new DbParameter("vdia", DbType.Date,20,ParameterDirection.Input,objRegisterEtaResponse.vdia),
            new DbParameter("vfranja", DbType.String,20,ParameterDirection.Input,objRegisterEtaResponse.vfranja),
            new DbParameter("vestado", DbType.Int32,20,ParameterDirection.Input,objRegisterEtaResponse.vestado),
            new DbParameter("vquota", DbType.Int32,20,ParameterDirection.Input,objRegisterEtaResponse.vquota),
            new DbParameter("vid_bucket", DbType.String,50,ParameterDirection.Input,objRegisterEtaResponse.vid_bucket),
            new DbParameter("vresp", DbType.String,20,ParameterDirection.Output,objRegisterEtaResponse.vresp)
         };

            try
            {
                Web.Logging.ExecuteMethod(objRegisterEtaResponse.Audit.Session, objRegisterEtaResponse.Audit.Transaction, () =>
                {
                    DbFactory.ExecuteNonQuery(objRegisterEtaResponse.Audit.Session, objRegisterEtaResponse.Audit.Transaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_SP_REGISTRA_ETA_RESP, arrParam);
                });

            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRegisterEtaResponse.Audit.Session, objRegisterEtaResponse.Audit.Transaction, ex.Message);
            }
            finally
            {
                objRegisterEtaResponse.vresp = arrParam[arrParam.Length - 1].Value.ToString();
            }
            return objRegisterEtaResponse.vresp;

        }

        public static BE.GetDetailTransExtra.DetailTransExtraResponse REGISTRA_COSTO_PA(BE.GetDetailTransExtra.DetailTransExtraRequest objDetailTransExtraRequest)
        {

            int PRESULTADO = 0;
            string PMSGERR = string.Empty;
            BE.GetDetailTransExtra.DetailTransExtraResponse objDetailTransExtraResponse = new BE.GetDetailTransExtra.DetailTransExtraResponse();
            DbParameter[] arrParam = {
            new DbParameter("PCODSOLOT", DbType.Int64,255,ParameterDirection.Input,objDetailTransExtraRequest.CodSolOt),
            new DbParameter("PCUSTOMER_ID", DbType.Int64,255,ParameterDirection.Input,objDetailTransExtraRequest.iCustomerId),
            new DbParameter("PDIRECCION_FACTURACION",DbType.String,255,ParameterDirection.Input),
            new DbParameter("PNOTAS_DIRECCION", DbType.String,255,ParameterDirection.Input),
            new DbParameter("PDISTRITO", DbType.String,255,ParameterDirection.Input),
            new DbParameter("PPROVINCIA", DbType.String,255,ParameterDirection.Input),
            new DbParameter("PCODIGO_POSTAL", DbType.String,255,ParameterDirection.Input),
            new DbParameter("PDEPARTAMENTO", DbType.String,255,ParameterDirection.Input),
            new DbParameter("PPAIS", DbType.String,255,ParameterDirection.Input),
            new DbParameter("PFLAG_DIRECC_FACT", DbType.Int32,255,ParameterDirection.Input),
            new DbParameter("PUSUARIO_REG", DbType.String,255,ParameterDirection.Input,objDetailTransExtraRequest.vUsuarioReg),
            new DbParameter("PFECHA_REG", DbType.Date,ParameterDirection.Input),
            new DbParameter("PRESULTADO", DbType.Int32,255,ParameterDirection.Output),
            new DbParameter("PMSGERR", DbType.String,255,ParameterDirection.Output)
         };


            //if (objDetailTransExtraRequest.iFlagDireccFact == 0)
            //{
            //    arrParam[9].Value = false;
            //}
            //else
            //{
            //    arrParam[9].Value = true;
            //}

            try
            {
                Web.Logging.ExecuteMethod(objDetailTransExtraRequest.Audit.Session, objDetailTransExtraRequest.Audit.Transaction, () =>
                {
                    DbFactory.ExecuteNonQuery(objDetailTransExtraRequest.Audit.Session, objDetailTransExtraRequest.Audit.Transaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_SP_REGISTRA_COSTO_PA, arrParam);
                });

            }
            catch (Exception ex)
            {
                Web.Logging.Error(objDetailTransExtraRequest.Audit.Session, objDetailTransExtraRequest.Audit.Transaction, ex.Message);
            }
            finally
            {
                objDetailTransExtraResponse.vMensaje = arrParam[arrParam.Length - 1].Value.ToString();
                objDetailTransExtraResponse.iResultado = ConvertHFC.ToInt(arrParam[arrParam.Length - 2].Value.ToString());
            }
            return objDetailTransExtraResponse;

        }

        public static BE.GetDetailTransExtra.DetailTransExtraResponse ACTUALIZAR_COSTO_PA(BE.GetDetailTransExtra.DetailTransExtraRequest objDetailTransExtraRequest)
        {

            int PRESULTADO = 0;
            string PMSGERR = string.Empty;
            BE.GetDetailTransExtra.DetailTransExtraResponse objDetailTransExtraResponse = new BE.GetDetailTransExtra.DetailTransExtraResponse();
            DbParameter[] arrParam = {
            new DbParameter("PCODSOLOT", DbType.Int64,255,ParameterDirection.Input,objDetailTransExtraRequest.CodSolOt),
            new DbParameter("PCUSTOMER_ID", DbType.Int64,255,ParameterDirection.Input,objDetailTransExtraRequest.iCustomerId),
            new DbParameter("PFECVIG", DbType.String,255,ParameterDirection.Input,objDetailTransExtraRequest.vFechaVig),
            new DbParameter("PMONTO", DbType.Double,255,ParameterDirection.Input,objDetailTransExtraRequest.iMonto),
            new DbParameter("POBSERVACION", DbType.String,255,ParameterDirection.Input,objDetailTransExtraRequest.vObservacion),
            new DbParameter("PFLAG_COBRO_OCC", DbType.Int64,255,ParameterDirection.Input,objDetailTransExtraRequest.iFlag_Cobro),
            new DbParameter("PAPLICACION", DbType.String,255,ParameterDirection.Input,objDetailTransExtraRequest.vAplicacion),
            new DbParameter("PUSUARIO_ACT", DbType.String,255,ParameterDirection.Input,objDetailTransExtraRequest.vUsuarioReg),
            new DbParameter("PFECHA_ACT", DbType.String,255,ParameterDirection.Input,objDetailTransExtraRequest.vFechaReg),
            new DbParameter("PCOD_ID", DbType.Int32,255,ParameterDirection.Input,objDetailTransExtraRequest.vCodId),
            new DbParameter("PRESULTADO", DbType.String,255,ParameterDirection.Output,PRESULTADO),
            new DbParameter("PMSGERR", DbType.String,255,ParameterDirection.Output,PMSGERR),

           
         };
   
            try
            {
                Web.Logging.ExecuteMethod(objDetailTransExtraRequest.Audit.Session, objDetailTransExtraRequest.Audit.Transaction, () =>
                {
                    DbFactory.ExecuteNonQuery(objDetailTransExtraRequest.Audit.Session, objDetailTransExtraRequest.Audit.Transaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_SP_ACTUALIZAR_COSTO_PA, arrParam);
                });

            }
            catch (Exception ex)
            {
                Web.Logging.Error(objDetailTransExtraRequest.Audit.Session, objDetailTransExtraRequest.Audit.Transaction, ex.Message);
            }
            finally
            {
                objDetailTransExtraResponse.vMensaje = arrParam[arrParam.Length - 1].Value.ToString();
                objDetailTransExtraResponse.iResultado = ConvertHFC.ToInt(arrParam[arrParam.Length - 2].Value.ToString());
            }
            return objDetailTransExtraResponse;

        }



         public static string LTERegisterTransaction (string IdSession,string idTransaccion, RegisterTransaction objRegisterTransaction, out int intResCod,out string strResDes)
         {
             //string strNroSot = string.Empty;
             Int64 strNroSot = 0;

             string strNroVia = !string.IsNullOrEmpty(objRegisterTransaction.TipoUrb)
                 ? (objRegisterTransaction.NroVia == Claro.SIACU.Transac.Service.Constants.numeroCero
                     ? Claro.SIACU.Transac.Service.Constants.PresentationLayer.gstrVariableSNAbreviado
                     : Functions.CheckStr(objRegisterTransaction.NroVia))
                 : objRegisterTransaction.TipoUrb;

             DbParameter[] arrParam = {
                new DbParameter("p_id", DbType.String,ParameterDirection.Input,objRegisterTransaction.InterCasoID),
                new DbParameter("a_cargo", DbType.Double,ParameterDirection.Input,objRegisterTransaction.Cargo),
                new DbParameter("a_codigo_occ", DbType.String,ParameterDirection.Input,objRegisterTransaction.CodOCC),
                new DbParameter("a_cod_caso", DbType.String,ParameterDirection.Input,objRegisterTransaction.CodCaso),
                new DbParameter("a_cod_id", DbType.String,ParameterDirection.Input,objRegisterTransaction.ConID),
                new DbParameter("a_cod_intercaso", DbType.String,ParameterDirection.Input,objRegisterTransaction.InterCasoID),
                new DbParameter("a_codedif", DbType.Int32,ParameterDirection.Input,ConvertHFC.ToInt(objRegisterTransaction.EdificioID)),
                new DbParameter("a_codmotot", DbType.Double,ParameterDirection.Input,ConvertHFC.ToDouble(objRegisterTransaction.MotivoID)),
                new DbParameter("a_codusu", DbType.String,ParameterDirection.Input,objRegisterTransaction.USRREGIS),
                new DbParameter("a_codzona", DbType.Int32,ParameterDirection.Input,objRegisterTransaction.ZonaID),
                new DbParameter("a_customer_id", DbType.String,ParameterDirection.Input,objRegisterTransaction.CustomerID),
                new DbParameter("a_fecprog", DbType.Date,ParameterDirection.Input,ConvertHFC.ToDate(objRegisterTransaction.FechaProgramada)),
                new DbParameter("a_franja", DbType.String,ParameterDirection.Input,objRegisterTransaction.FranjaHoraID),
                new DbParameter("a_franja_hor", DbType.String,ParameterDirection.Input,objRegisterTransaction.FranjaHora),
                new DbParameter("a_lote", DbType.String,ParameterDirection.Input,objRegisterTransaction.NumLote),
                new DbParameter("a_manzana", DbType.String,ParameterDirection.Input,objRegisterTransaction.NumMZ),
                new DbParameter("a_nom_via", DbType.String,ParameterDirection.Input,objRegisterTransaction.NomVia),
                new DbParameter("a_nomurb", DbType.String,ParameterDirection.Input,objRegisterTransaction.NomUrb),
                new DbParameter("a_num_via", DbType.String,ParameterDirection.Input,strNroVia),
                new DbParameter("a_observacion", DbType.String,ParameterDirection.Input,objRegisterTransaction.Observacion),
                new DbParameter("a_centro_poblado", DbType.String,ParameterDirection.Input,objRegisterTransaction.CentroPobladoID),
                new DbParameter("a_referencia", DbType.String,ParameterDirection.Input,objRegisterTransaction.Referencia),
                new DbParameter("a_tip_urb", DbType.Int32,ParameterDirection.Input,objRegisterTransaction.TipoUrb),
                new DbParameter("a_tipo_trans", DbType.String,ParameterDirection.Input,objRegisterTransaction.TransTipo),
                new DbParameter("a_tipo_via", DbType.String,ParameterDirection.Input,objRegisterTransaction.TipoVia),
                new DbParameter("a_tiposervico", DbType.String,ParameterDirection.Input,objRegisterTransaction.ServicioID),
                new DbParameter("a_tiptra", DbType.Double,ParameterDirection.Input,objRegisterTransaction.TrabajoID),
                new DbParameter("a_ubigeo", DbType.String,ParameterDirection.Input,objRegisterTransaction.Ubigeo),
                new DbParameter("a_reclamo_caso", DbType.Double,ParameterDirection.Input,ConvertHFC.ToDouble(objRegisterTransaction.CodReclamo)),
                new DbParameter("a_flag_act_dir_fact", DbType.String,ParameterDirection.Input,objRegisterTransaction.FlagActDirFact),
                new DbParameter("a_numcarta", DbType.Double,ParameterDirection.Input,ConvertHFC.ToDouble(objRegisterTransaction.NumCarta)),
                new DbParameter("a_operador", DbType.String,ParameterDirection.Input,objRegisterTransaction.Operador),
                new DbParameter("a_presuscrito", DbType.Int32,ParameterDirection.Input,ConvertHFC.ToDouble(objRegisterTransaction.Presuscrito)),
                new DbParameter("a_publicar", DbType.Double,ParameterDirection.Input, ConvertHFC.ToDouble(objRegisterTransaction.Publicar)),
                new DbParameter("a_ad_tmcode", DbType.String,ParameterDirection.Input,objRegisterTransaction.TmCode),
                new DbParameter("a_lista_coser", DbType.String,ParameterDirection.Input,objRegisterTransaction.ListaCoser),
                new DbParameter("a_lista_spcode", DbType.String,ParameterDirection.Input,objRegisterTransaction.ListaSpCode),
                new DbParameter("a_cantidad", DbType.Double,ParameterDirection.Input,objRegisterTransaction.Cantidad),
                new DbParameter("o_codsolot", DbType.Int64,ParameterDirection.Output),
                new DbParameter("errmsg_out", DbType.String,500,ParameterDirection.Output),
                new DbParameter("resultado_out", DbType.Int32,ParameterDirection.Output)

                };


             try
             {
               
                if (objRegisterTransaction.Cargo != null)
                {
                    if (objRegisterTransaction.Cargo.Length > 0)
                    {
                        arrParam[1].Value = double.Parse(objRegisterTransaction.Cargo);
                    }
                    else
                    {
                        arrParam[1].Value = DBNull.Value;
                    }
                }


                Web.Logging.ExecuteMethod(IdSession, idTransaccion, () =>
                {
                    DbFactory.ExecuteNonQuery(IdSession, IdSession, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_SP_LTE_P_GENERA_TRANSACCION, arrParam);
                });

             }
             catch (Exception ex)
             {
                 Web.Logging.Error(IdSession, IdSession, ex.Message);

             }
             finally {
                 strNroSot = ConvertHFC.ToInt64(arrParam[arrParam.Length - 3].Value.ToString() == string.Empty ? SIACU.Transac.Service.Constants.numeroCero : Functions.CheckInt64(arrParam[arrParam.Length - 3].Value.ToString()));

                

                 if (arrParam[arrParam.Length - 2].Value != null)
                 {
                     strResDes = (arrParam[arrParam.Length -2].Value.ToString() == string.Empty ? string.Empty : arrParam[arrParam.Length - 2].Value).ToString();
                 }
                 else
                 {
                     strResDes = string.Empty;
                 }

                 if (arrParam[arrParam.Length - 1].Value != null)
                 {
                     intResCod = Convert.ToInt(arrParam[arrParam.Length - 1].Value.ToString() == string.Empty ? string.Empty : arrParam[arrParam.Length - 1].Value.ToString());
                 }
                 else
                 {
                     intResCod = 0;
                 }
                
                 
             }

             return strNroSot.ToString();
              
         }

    }
}
