using Claro.Data;
using Claro.SIACU.Data.Transac.Service.Configuration;
using EntityCommon = Claro.SIACU.Entity.Transac.Service.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Claro.SIACU.Transac.Service;
using EntityFixed = Claro.SIACU.Entity.Transac.Service.Fixed;
using ConfigServices = Claro.SIACU.ProxyService.Transac.Service.ConfigurationServices;
using ConvertHFC = Claro.Convert;

namespace Claro.SIACU.Data.Transac.Service.Fixed
{
    public class ConfigurationIP
    {
        public static List<EntityFixed.GetJobTypeConfigIP.JobTypesConfigIPResponse> GetJobTypesConfIP(EntityFixed.GetJobTypeConfigIP.JobTypesConfigIPRequest objJobTypesRequest)
        {
            List<EntityFixed.GetJobTypeConfigIP.JobTypesConfigIPResponse> LstJobType = new List<EntityFixed.GetJobTypeConfigIP.JobTypesConfigIPResponse>();
            try
            {
                DbParameter[] parameters = new DbParameter[] {
                   new DbParameter("a_cursor", DbType.Object, ParameterDirection.Output)
                };

                LstJobType = Web.Logging.ExecuteMethod<List<EntityFixed.GetJobTypeConfigIP.JobTypesConfigIPResponse>>(objJobTypesRequest.Audit.Session, objJobTypesRequest.Audit.Transaction, () =>
                {
                    return DbFactory.ExecuteReader<List<EntityFixed.GetJobTypeConfigIP.JobTypesConfigIPResponse>>(objJobTypesRequest.Audit.Session, objJobTypesRequest.Audit.Transaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_SGASS_TIP_TRAB_CONFIP, parameters);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objJobTypesRequest.Audit.Session, objJobTypesRequest.Audit.Transaction, ex.Message);
            //    throw;
            }
            return LstJobType;
        }

        public static List<EntityFixed.GetDataLine.DataLineResponse> GetDataLine(EntityFixed.GetDataLine.DataLineRequest oBE) {

            List<EntityFixed.GetDataLine.DataLineResponse> LstDataLine = new List<EntityFixed.GetDataLine.DataLineResponse>();

            try
            {
                DbParameter[] parameters = new DbParameter[] {
                   new DbParameter("p_co_id", DbType.Int64, ParameterDirection.Input,oBE.ContractID),
                   new DbParameter("p_cursor", DbType.Object, ParameterDirection.Output)
                };

                LstDataLine = Web.Logging.ExecuteMethod<List<EntityFixed.GetDataLine.DataLineResponse>>(oBE.Audit.Session, oBE.Audit.Transaction, () =>
                {
                    return DbFactory.ExecuteReader<List<EntityFixed.GetDataLine.DataLineResponse>>(oBE.Audit.Session, oBE.Audit.Transaction, DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_POST_BSCS_SP_DATOS_LINEA, parameters);
                });

            }
            catch (Exception ex)
            {
                Web.Logging.Error(oBE.Audit.Session, oBE.Audit.Transaction, ex.Message);
            }
            return LstDataLine;
        }

        public static List<EntityFixed.GetTypeConfip.TypeConfigIpResponse> GetTypeConfig(EntityFixed.GetTypeConfip.TypeConfigIpRequest objTypeConfigIpRequest)
        {
            List<EntityFixed.GetTypeConfip.TypeConfigIpResponse> LstTypeConfigIpResponse = new List<EntityFixed.GetTypeConfip.TypeConfigIpResponse>();
            try
            {
                DbParameter[] parameters = new DbParameter[] {
                   new DbParameter("an_tiptrabajo", DbType.Int32, ParameterDirection.Input,objTypeConfigIpRequest.an_tiptrabajo),
                   new DbParameter("a_cursor", DbType.Object, ParameterDirection.Output)
                 };

                LstTypeConfigIpResponse = Web.Logging.ExecuteMethod<List<EntityFixed.GetTypeConfip.TypeConfigIpResponse>>(objTypeConfigIpRequest.Audit.Session, objTypeConfigIpRequest.Audit.Transaction, () =>
                {
                    return DbFactory.ExecuteReader<List<EntityFixed.GetTypeConfip.TypeConfigIpResponse>>(objTypeConfigIpRequest.Audit.Session, objTypeConfigIpRequest.Audit.Transaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_SGASS_TIPO_CONFIP, parameters);
                });

            }
            catch (Exception ex)
            {
                Web.Logging.Error(objTypeConfigIpRequest.Audit.Session, objTypeConfigIpRequest.Audit.Transaction, ex.Message);
              //  throw;
            }
            return LstTypeConfigIpResponse;
        }

        public static List<EntityFixed.GetBranchCustomer.BranchCustomerResponse> GetBranchCustomer(EntityFixed.GetBranchCustomer.BranchCustomerResquest objBranchCustomerResquest)
        {
            List<EntityFixed.GetBranchCustomer.BranchCustomerResponse> LstBranchCustomerResponse = new List<EntityFixed.GetBranchCustomer.BranchCustomerResponse>();
            try
            {
                DbParameter[] parameters = new DbParameter[] {
                   new DbParameter("an_customer_id", DbType.Int32, ParameterDirection.Input,objBranchCustomerResquest.an_customer_id),
                   new DbParameter("a_cursor", DbType.Object, ParameterDirection.Output)
                };

                LstBranchCustomerResponse = Claro.Web.Logging.ExecuteMethod<List<EntityFixed.GetBranchCustomer.BranchCustomerResponse>>(objBranchCustomerResquest.Audit.Session, objBranchCustomerResquest.Audit.Transaction, () =>
                {
                    return DbFactory.ExecuteReader<List<EntityFixed.GetBranchCustomer.BranchCustomerResponse>>(objBranchCustomerResquest.Audit.Session, objBranchCustomerResquest.Audit.Transaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_SGASS_SUCURSALES_CLIENTE, parameters);
                });

            }
            catch (Exception ex)
            {

                Web.Logging.Error(objBranchCustomerResquest.Audit.Session, objBranchCustomerResquest.Audit.Transaction, ex.Message);
           //     throw;
            }
            return LstBranchCustomerResponse;
        }

        public static EntityFixed.GetConfigurationIP.ConfigurationIPResponse ConfigurationServicesSave(EntityFixed.GetConfigurationIP.ConfigurationIPRequest oConfigurationIPRequest)
        {
            Web.Logging.Info("Session: 123456789", "Transaction: ConfigurationServicesSave ", "Entra a ConfigurationServicesSave");

            EntityFixed.GetConfigurationIP.ConfigurationIPResponse oConfigurationIPResponse = new EntityFixed.GetConfigurationIP.ConfigurationIPResponse();
            try
            {
                ConfigServices.HeaderRequestType oHeaderRequestType = new ConfigServices.HeaderRequestType();
                ConfigServices.configuracionIPRequest oconfiguracionIPRequest = new ConfigServices.configuracionIPRequest();
                
                ConfigServices.CustomerClfy oCustomerClfy = new ConfigServices.CustomerClfy();
                ConfigServices.ContactUser oContactUser = new ConfigServices.ContactUser();
                ConfigServices.Interact oInteract = new ConfigServices.Interact();
                ConfigServices.InteractPlus oInteractPlus = new ConfigServices.InteractPlus();
                ConfigServices.ParamSOTHFC oParamSOTHFC = new ConfigServices.ParamSOTHFC();

                //oHeaderRequestType.canal = "WEB";
                //oHeaderRequestType.idAplicacion = "SIACUNICO";
                //oHeaderRequestType.usuarioAplicacion = oConfigurationIPRequest.oCustomer.USUARIO;
                //oHeaderRequestType.usuarioSesion = oConfigurationIPRequest.oCustomer.USUARIO;
                //oHeaderRequestType.idTransaccionESB = oConfigurationIPRequest.Audit.Transaction;
                //oHeaderRequestType.idTransaccionNegocio = "333";
                //oHeaderRequestType.fechaInicio = System.Convert.ToDateTime("2017-06-04T17:37:19-05:00");

  

                oHeaderRequestType.canal = oConfigurationIPRequest.strWeb;
                oHeaderRequestType.idAplicacion = oConfigurationIPRequest.strApplicationName;
                oHeaderRequestType.usuarioAplicacion = oConfigurationIPRequest.oCustomer.USUARIO;
                oHeaderRequestType.usuarioSesion = oConfigurationIPRequest.oCustomer.USUARIO;
                oHeaderRequestType.idTransaccionESB = oConfigurationIPRequest.Audit.Transaction;
                oHeaderRequestType.idTransaccionNegocio = oConfigurationIPRequest.Audit.Transaction;
                oHeaderRequestType.fechaInicio = DateTime.Now;
                 
                oconfiguracionIPRequest.msisdn = (oConfigurationIPRequest.strMsisdn==null)?"":oConfigurationIPRequest.strMsisdn; //"84606060";
                oconfiguracionIPRequest.flagContingencia = (oConfigurationIPRequest.strFlagContingencia==null)?"":oConfigurationIPRequest.strFlagContingencia; //"1"; 
                oconfiguracionIPRequest.tipoTransaccion = (oConfigurationIPRequest.strTipoTransaccion==null)?"":oConfigurationIPRequest.strTipoTransaccion; // "ConfiguracionIP"; 
                oconfiguracionIPRequest.correoDestinatario = (oConfigurationIPRequest.strCorreoDestinatario==null)?"":oConfigurationIPRequest.strCorreoDestinatario; //"valdezkj@globalhitss.com";
                oconfiguracionIPRequest.formatoConstancia = oConfigurationIPRequest.strFormatoConstancia; //(oConfigurationIPRequest.strFormatoConstancia == null) ? "" : oConfigurationIPRequest.strFormatoConstancia; // string.Empty;
                oconfiguracionIPRequest.ipCliente = oConfigurationIPRequest.strIpCliente;
                
                oCustomerClfy.account = (oConfigurationIPRequest.oCustomer.CUENTA == null) ? "" : oConfigurationIPRequest.oCustomer.CUENTA;
                oCustomerClfy.contactObjId = (oConfigurationIPRequest.oCustomer.OBJID_CONTACTO==null)?"":oConfigurationIPRequest.oCustomer.OBJID_CONTACTO;// "268609749"; 
                oCustomerClfy.flagReg = ConvertHFC.ToString(oConfigurationIPRequest.oCustomer.FLAG_REGISTRADO);// "1";

                oContactUser.usuario = (oConfigurationIPRequest.oCustomer.USUARIO == null) ? "" :oConfigurationIPRequest.oCustomer.USUARIO;
                oContactUser.nombres = (oConfigurationIPRequest.oCustomer.NOMBRES == null) ? "" :oConfigurationIPRequest.oCustomer.NOMBRES;
                oContactUser.apellidos = (oConfigurationIPRequest.oCustomer.APELLIDOS == null) ? "" :oConfigurationIPRequest.oCustomer.APELLIDOS;
                oContactUser.razonSocial = (oConfigurationIPRequest.oCustomer.RAZON_SOCIAL == null) ? "" :oConfigurationIPRequest.oCustomer.RAZON_SOCIAL;
                oContactUser.tipoDoc = (oConfigurationIPRequest.oCustomer.TIPO_DOC == null) ? "" :oConfigurationIPRequest.oCustomer.TIPO_DOC;
                oContactUser.numDoc = (oConfigurationIPRequest.oCustomer.NRO_DOC == null) ? "" :oConfigurationIPRequest.oCustomer.NRO_DOC;
                oContactUser.domicilio = (oConfigurationIPRequest.oCustomer.DOMICILIO == null) ? "" :oConfigurationIPRequest.oCustomer.DOMICILIO;
                oContactUser.distrito = (oConfigurationIPRequest.oCustomer.DISTRITO == null) ? "" :oConfigurationIPRequest.oCustomer.DISTRITO;
                oContactUser.departamento = (oConfigurationIPRequest.oCustomer.DEPARTAMENTO == null) ? "" :oConfigurationIPRequest.oCustomer.DEPARTAMENTO;
                oContactUser.provincia = (oConfigurationIPRequest.oCustomer.PROVINCIA == null) ? "" :oConfigurationIPRequest.oCustomer.PROVINCIA;
                oContactUser.modalidad = (oConfigurationIPRequest.oCustomer.MODALIDAD == null) ? "" : oConfigurationIPRequest.oCustomer.MODALIDAD;
                 
                oInteract.contactobjid = (oConfigurationIPRequest.oInsertInteractHFCRequest.Interaction.OBJID_CONTACTO == null) ? "" : oConfigurationIPRequest.oInsertInteractHFCRequest.Interaction.OBJID_CONTACTO;
                oInteract.siteobjid = (oConfigurationIPRequest.oInsertInteractHFCRequest.Interaction.OBJID_SITE== null) ? "" :oConfigurationIPRequest.oInsertInteractHFCRequest.Interaction.OBJID_SITE;
                oInteract.account = (oConfigurationIPRequest.oInsertInteractHFCRequest.Interaction.CUENTA== null) ? "" :oConfigurationIPRequest.oInsertInteractHFCRequest.Interaction.CUENTA;
                oInteract.phone = (oConfigurationIPRequest.oInsertInteractHFCRequest.Interaction.TELEFONO== null) ? "" :oConfigurationIPRequest.oInsertInteractHFCRequest.Interaction.TELEFONO;
                oInteract.tipo = (oConfigurationIPRequest.oInsertInteractHFCRequest.Interaction.TIPO== null) ? "" :oConfigurationIPRequest.oInsertInteractHFCRequest.Interaction.TIPO;
                oInteract.clase = (oConfigurationIPRequest.oInsertInteractHFCRequest.Interaction.CLASE== null) ? "" :oConfigurationIPRequest.oInsertInteractHFCRequest.Interaction.CLASE;
                oInteract.subclase = (oConfigurationIPRequest.oInsertInteractHFCRequest.Interaction.SUBCLASE== null) ? "" :oConfigurationIPRequest.oInsertInteractHFCRequest.Interaction.SUBCLASE;
                oInteract.metodoContacto = (oConfigurationIPRequest.oInsertInteractHFCRequest.Interaction.METODO== null) ? "" :oConfigurationIPRequest.oInsertInteractHFCRequest.Interaction.METODO;
                oInteract.tipoInter = (oConfigurationIPRequest.oInsertInteractHFCRequest.Interaction.TIPO_INTER== null) ? "" :oConfigurationIPRequest.oInsertInteractHFCRequest.Interaction.TIPO_INTER;
                oInteract.agente = (oConfigurationIPRequest.oInsertInteractHFCRequest.Interaction.AGENTE== null) ? "" :oConfigurationIPRequest.oInsertInteractHFCRequest.Interaction.AGENTE;
                oInteract.usrProceso = (oConfigurationIPRequest.oInsertInteractHFCRequest.Interaction.USUARIO_PROCESO== null) ? "" :oConfigurationIPRequest.oInsertInteractHFCRequest.Interaction.USUARIO_PROCESO;
                oInteract.hechoEnUno = (oConfigurationIPRequest.oInsertInteractHFCRequest.Interaction.HECHO_EN_UNO== null) ? "" :oConfigurationIPRequest.oInsertInteractHFCRequest.Interaction.HECHO_EN_UNO;
                oInteract.notas = (oConfigurationIPRequest.oInsertInteractHFCRequest.Interaction.NOTAS== null) ? "" :oConfigurationIPRequest.oInsertInteractHFCRequest.Interaction.NOTAS;
                oInteract.flagCaso = (oConfigurationIPRequest.oInsertInteractHFCRequest.Interaction.FLAG_CASO== null) ? "" :oConfigurationIPRequest.oInsertInteractHFCRequest.Interaction.FLAG_CASO;
                oInteract.resultado = (oConfigurationIPRequest.oInsertInteractHFCRequest.Interaction.RESULTADO== null) ? "" :oConfigurationIPRequest.oInsertInteractHFCRequest.Interaction.RESULTADO;
                oInteract.servafect = (oConfigurationIPRequest.oInsertInteractHFCRequest.Interaction.SERVICIO== null) ? "" :oConfigurationIPRequest.oInsertInteractHFCRequest.Interaction.SERVICIO; // Consultar
                oInteract.inconven = (oConfigurationIPRequest.oInsertInteractHFCRequest.Interaction.INCONVENIENTE== null) ? "" :oConfigurationIPRequest.oInsertInteractHFCRequest.Interaction.INCONVENIENTE;
                oInteract.servafectCode = (oConfigurationIPRequest.oInsertInteractHFCRequest.Interaction.SERVICIO_CODE== null) ? "" :oConfigurationIPRequest.oInsertInteractHFCRequest.Interaction.SERVICIO_CODE; //Consultar
                oInteract.inconvenCode = (oConfigurationIPRequest.oInsertInteractHFCRequest.Interaction.INCONVENIENTE_CODE == null) ? "" : oConfigurationIPRequest.oInsertInteractHFCRequest.Interaction.INCONVENIENTE_CODE;
                oInteract.coId = (oConfigurationIPRequest.oInsertInteractHFCRequest.Interaction.CONTRATO== null) ? "" :oConfigurationIPRequest.oInsertInteractHFCRequest.Interaction.CONTRATO;
                oInteract.codPlano = (oConfigurationIPRequest.oInsertInteractHFCRequest.Interaction.PLANO== null) ? "" :oConfigurationIPRequest.oInsertInteractHFCRequest.Interaction.PLANO;
                oInteract.valor1 = (oConfigurationIPRequest.oInsertInteractHFCRequest.Interaction.VALOR_1== null) ? "" :oConfigurationIPRequest.oInsertInteractHFCRequest.Interaction.VALOR_1;
                oInteract.valor2 = (oConfigurationIPRequest.oInsertInteractHFCRequest.Interaction.VALOR_2== null) ? "" :oConfigurationIPRequest.oInsertInteractHFCRequest.Interaction.VALOR_2;


                oInteractPlus.inter1 = (oConfigurationIPRequest.oInsertTemplateInteraction._X_INTER_1 == null) ? "" :   oConfigurationIPRequest.oInsertTemplateInteraction._X_INTER_1;
                oInteractPlus.inter2 = (oConfigurationIPRequest.oInsertTemplateInteraction._X_INTER_2== null) ? ""  :   oConfigurationIPRequest.oInsertTemplateInteraction._X_INTER_2;
                oInteractPlus.inter3 = (oConfigurationIPRequest.oInsertTemplateInteraction._X_INTER_3== null) ? ""  :   oConfigurationIPRequest.oInsertTemplateInteraction._X_INTER_3;
                oInteractPlus.inter4 = (oConfigurationIPRequest.oInsertTemplateInteraction._X_INTER_4== null) ? ""  :   oConfigurationIPRequest.oInsertTemplateInteraction._X_INTER_4;
                oInteractPlus.inter5 = (oConfigurationIPRequest.oInsertTemplateInteraction._X_INTER_5== null) ? ""  :   oConfigurationIPRequest.oInsertTemplateInteraction._X_INTER_5;
                oInteractPlus.inter6 = (oConfigurationIPRequest.oInsertTemplateInteraction._X_INTER_6== null) ? ""  :   oConfigurationIPRequest.oInsertTemplateInteraction._X_INTER_6;
                oInteractPlus.inter7 = (oConfigurationIPRequest.oInsertTemplateInteraction._X_INTER_7== null) ? ""  :   oConfigurationIPRequest.oInsertTemplateInteraction._X_INTER_7;
                oInteractPlus.inter8 = ConvertHFC.ToString(oConfigurationIPRequest.oInsertTemplateInteraction._X_INTER_8);
                oInteractPlus.inter9 = ConvertHFC.ToString(oConfigurationIPRequest.oInsertTemplateInteraction._X_INTER_9);
                oInteractPlus.inter10 = ConvertHFC.ToString(oConfigurationIPRequest.oInsertTemplateInteraction._X_INTER_10);
                oInteractPlus.inter11 = Convert.ToString(oConfigurationIPRequest.oInsertTemplateInteraction._X_INTER_11);
                oInteractPlus.inter12 = Convert.ToString(oConfigurationIPRequest.oInsertTemplateInteraction._X_INTER_12);
                oInteractPlus.inter13 = Convert.ToString(oConfigurationIPRequest.oInsertTemplateInteraction._X_INTER_13);
                oInteractPlus.inter14 = Convert.ToString(oConfigurationIPRequest.oInsertTemplateInteraction._X_INTER_14);
                oInteractPlus.inter15 = (oConfigurationIPRequest.oInsertTemplateInteraction._X_INTER_15== null) ? ""  : oConfigurationIPRequest.oInsertTemplateInteraction._X_INTER_15;
                oInteractPlus.inter16 = (oConfigurationIPRequest.oInsertTemplateInteraction._X_INTER_16== null) ? ""  : oConfigurationIPRequest.oInsertTemplateInteraction._X_INTER_16;
                oInteractPlus.inter17 = (oConfigurationIPRequest.oInsertTemplateInteraction._X_INTER_17== null) ? ""  : oConfigurationIPRequest.oInsertTemplateInteraction._X_INTER_17;
                oInteractPlus.inter18 = (oConfigurationIPRequest.oInsertTemplateInteraction._X_INTER_18== null) ? ""  : oConfigurationIPRequest.oInsertTemplateInteraction._X_INTER_18;
                oInteractPlus.inter19 = (oConfigurationIPRequest.oInsertTemplateInteraction._X_INTER_19== null) ? ""  : oConfigurationIPRequest.oInsertTemplateInteraction._X_INTER_19;
                oInteractPlus.inter20 = (oConfigurationIPRequest.oInsertTemplateInteraction._X_INTER_20== null) ? ""  : oConfigurationIPRequest.oInsertTemplateInteraction._X_INTER_20;
                oInteractPlus.inter21 = (oConfigurationIPRequest.oInsertTemplateInteraction._X_INTER_21== null) ? ""  : oConfigurationIPRequest.oInsertTemplateInteraction._X_INTER_21;
                oInteractPlus.inter22 = ConvertHFC.ToString(oConfigurationIPRequest.oInsertTemplateInteraction._X_INTER_22);
                oInteractPlus.inter23 = ConvertHFC.ToString(oConfigurationIPRequest.oInsertTemplateInteraction._X_INTER_23);
                oInteractPlus.inter24 = ConvertHFC.ToString(oConfigurationIPRequest.oInsertTemplateInteraction._X_INTER_24);
                oInteractPlus.inter25 = ConvertHFC.ToString(oConfigurationIPRequest.oInsertTemplateInteraction._X_INTER_25);
                oInteractPlus.inter26 = ConvertHFC.ToString(oConfigurationIPRequest.oInsertTemplateInteraction._X_INTER_26);
                oInteractPlus.inter27 = ConvertHFC.ToString(oConfigurationIPRequest.oInsertTemplateInteraction._X_INTER_27);
                oInteractPlus.inter28 = ConvertHFC.ToString(oConfigurationIPRequest.oInsertTemplateInteraction._X_INTER_28);
                oInteractPlus.inter29 = (oConfigurationIPRequest.oInsertTemplateInteraction._X_INTER_29== null) ? ""  : oConfigurationIPRequest.oInsertTemplateInteraction._X_INTER_29 ;
                oInteractPlus.inter30 = (oConfigurationIPRequest.oInsertTemplateInteraction._X_INTER_30 == null) ? "" : oConfigurationIPRequest.oInsertTemplateInteraction._X_INTER_30;
                oInteractPlus.plusInter2Interact = ConvertHFC.ToString(oConfigurationIPRequest.oInsertTemplateInteraction._X_PLUS_INTER2INTERACT);
                oInteractPlus.adjustmentAmount = ConvertHFC.ToString(oConfigurationIPRequest.oInsertTemplateInteraction._X_ADJUSTMENT_AMOUNT);
                oInteractPlus.adjustmentReason = (oConfigurationIPRequest.oInsertTemplateInteraction._X_ADJUSTMENT_REASON == null) ? "" : oConfigurationIPRequest.oInsertTemplateInteraction._X_ADJUSTMENT_REASON;
                oInteractPlus.address = (oConfigurationIPRequest.oInsertTemplateInteraction._X_ADDRESS == null) ? "" : oConfigurationIPRequest.oInsertTemplateInteraction._X_ADDRESS;
                oInteractPlus.amountUnit = (oConfigurationIPRequest.oInsertTemplateInteraction._X_AMOUNT_UNIT == null) ? "" : oConfigurationIPRequest.oInsertTemplateInteraction._X_AMOUNT_UNIT;
                //<!--FORMATO DIAMESAÑO - CONVERTIDO EN BPEL A AÑO-MES-DIA -- = "
                oInteractPlus.birthday = oConfigurationIPRequest.oInsertTemplateInteraction._X_BIRTHDAY.ToString("ddMMyyyy"); //"03032002"; //
                oInteractPlus.clarifyInteraction = (oConfigurationIPRequest.oInsertTemplateInteraction._X_CLARIFY_INTERACTION== null) ? "" :oConfigurationIPRequest.oInsertTemplateInteraction._X_CLARIFY_INTERACTION;
                oInteractPlus.claroLdn1 = (oConfigurationIPRequest.oInsertTemplateInteraction._X_CLARO_LDN1== null) ? "" :oConfigurationIPRequest.oInsertTemplateInteraction._X_CLARO_LDN1;
                oInteractPlus.claroLdn2 = (oConfigurationIPRequest.oInsertTemplateInteraction._X_CLARO_LDN2== null) ? "" :oConfigurationIPRequest.oInsertTemplateInteraction._X_CLARO_LDN2;
                oInteractPlus.claroLdn3 = (oConfigurationIPRequest.oInsertTemplateInteraction._X_CLARO_LDN3== null) ? "" :oConfigurationIPRequest.oInsertTemplateInteraction._X_CLARO_LDN3;
                oInteractPlus.claroLdn4 = (oConfigurationIPRequest.oInsertTemplateInteraction._X_CLARO_LDN4== null) ? "" :oConfigurationIPRequest.oInsertTemplateInteraction._X_CLARO_LDN4;
                oInteractPlus.claroLocal1 = (oConfigurationIPRequest.oInsertTemplateInteraction._X_CLAROLOCAL1== null) ? "" :oConfigurationIPRequest.oInsertTemplateInteraction._X_CLAROLOCAL1;
                oInteractPlus.claroLocal2 = (oConfigurationIPRequest.oInsertTemplateInteraction._X_CLAROLOCAL2== null) ? "" :oConfigurationIPRequest.oInsertTemplateInteraction._X_CLAROLOCAL2;
                oInteractPlus.claroLocal3 = (oConfigurationIPRequest.oInsertTemplateInteraction._X_CLAROLOCAL3== null) ? "" :oConfigurationIPRequest.oInsertTemplateInteraction._X_CLAROLOCAL3;
                oInteractPlus.claroLocal4 = (oConfigurationIPRequest.oInsertTemplateInteraction._X_CLAROLOCAL4== null) ? "" :oConfigurationIPRequest.oInsertTemplateInteraction._X_CLAROLOCAL4;
                oInteractPlus.claroLocal5 = (oConfigurationIPRequest.oInsertTemplateInteraction._X_CLAROLOCAL5== null) ? "" :oConfigurationIPRequest.oInsertTemplateInteraction._X_CLAROLOCAL5;
                oInteractPlus.claroLocal6 = (oConfigurationIPRequest.oInsertTemplateInteraction._X_CLAROLOCAL6== null) ? "" :oConfigurationIPRequest.oInsertTemplateInteraction._X_CLAROLOCAL6;
                oInteractPlus.contactPhone = (oConfigurationIPRequest.oInsertTemplateInteraction._X_CONTACT_PHONE== null) ? "" :oConfigurationIPRequest.oInsertTemplateInteraction._X_CONTACT_PHONE;
                oInteractPlus.dniLegalRep = (oConfigurationIPRequest.oInsertTemplateInteraction._X_DNI_LEGAL_REP== null) ? "" :oConfigurationIPRequest.oInsertTemplateInteraction._X_DNI_LEGAL_REP;
                oInteractPlus.documentNumber = (oConfigurationIPRequest.oInsertTemplateInteraction._X_DOCUMENT_NUMBER== null) ? "" :oConfigurationIPRequest.oInsertTemplateInteraction._X_DOCUMENT_NUMBER;
                oInteractPlus.email = (oConfigurationIPRequest.oInsertTemplateInteraction._X_EMAIL== null) ? "" :oConfigurationIPRequest.oInsertTemplateInteraction._X_EMAIL;
                oInteractPlus.firstName = (oConfigurationIPRequest.oInsertTemplateInteraction._X_FIRST_NAME== null) ? "" :oConfigurationIPRequest.oInsertTemplateInteraction._X_FIRST_NAME;
                oInteractPlus.fixedNumber = (oConfigurationIPRequest.oInsertTemplateInteraction._X_FIXED_NUMBER== null) ? "" :oConfigurationIPRequest.oInsertTemplateInteraction._X_FIXED_NUMBER;
                oInteractPlus.flagChangeUser = (oConfigurationIPRequest.oInsertTemplateInteraction._X_FLAG_CHANGE_USER== null) ? "" :oConfigurationIPRequest.oInsertTemplateInteraction._X_FLAG_CHANGE_USER;
                oInteractPlus.flagLegalRep = (oConfigurationIPRequest.oInsertTemplateInteraction._X_FLAG_LEGAL_REP== null) ? "" :oConfigurationIPRequest.oInsertTemplateInteraction._X_FLAG_LEGAL_REP;
                oInteractPlus.flagOther = (oConfigurationIPRequest.oInsertTemplateInteraction._X_FLAG_OTHER== null) ? "" :oConfigurationIPRequest.oInsertTemplateInteraction._X_FLAG_OTHER;
                oInteractPlus.flagTitular = (oConfigurationIPRequest.oInsertTemplateInteraction._X_FLAG_TITULAR== null) ? "" :oConfigurationIPRequest.oInsertTemplateInteraction._X_FLAG_TITULAR;
                oInteractPlus.imei = (oConfigurationIPRequest.oInsertTemplateInteraction._X_IMEI== null) ? "" :oConfigurationIPRequest.oInsertTemplateInteraction._X_IMEI;
                oInteractPlus.lastName = (oConfigurationIPRequest.oInsertTemplateInteraction._X_LAST_NAME== null) ? "" :oConfigurationIPRequest.oInsertTemplateInteraction._X_LAST_NAME;
                oInteractPlus.lastNameRep = (oConfigurationIPRequest.oInsertTemplateInteraction._X_LASTNAME_REP== null) ? "" :oConfigurationIPRequest.oInsertTemplateInteraction._X_LASTNAME_REP;
                oInteractPlus.ldiNumber = (oConfigurationIPRequest.oInsertTemplateInteraction._X_LDI_NUMBER== null) ? "" :oConfigurationIPRequest.oInsertTemplateInteraction._X_LDI_NUMBER;
                oInteractPlus.nameLegalRep = (oConfigurationIPRequest.oInsertTemplateInteraction._X_NAME_LEGAL_REP== null) ? "" :oConfigurationIPRequest.oInsertTemplateInteraction._X_NAME_LEGAL_REP;
                oInteractPlus.oldClaroLdn1 = (oConfigurationIPRequest.oInsertTemplateInteraction._X_OLD_CLARO_LDN1== null) ? "" :oConfigurationIPRequest.oInsertTemplateInteraction._X_OLD_CLARO_LDN1;
                oInteractPlus.oldClaroLdn2 = (oConfigurationIPRequest.oInsertTemplateInteraction._X_OLD_CLARO_LDN2== null) ? "" :oConfigurationIPRequest.oInsertTemplateInteraction._X_OLD_CLARO_LDN2;
                oInteractPlus.oldClaroLdn3 = (oConfigurationIPRequest.oInsertTemplateInteraction._X_OLD_CLARO_LDN3== null) ? "" :oConfigurationIPRequest.oInsertTemplateInteraction._X_OLD_CLARO_LDN3;
                oInteractPlus.oldClaroLdn4 = (oConfigurationIPRequest.oInsertTemplateInteraction._X_OLD_CLARO_LDN4== null) ? "" :oConfigurationIPRequest.oInsertTemplateInteraction._X_OLD_CLARO_LDN4;
                oInteractPlus.oldClaroLocal1 = (oConfigurationIPRequest.oInsertTemplateInteraction._X_OLD_CLAROLOCAL1== null) ? "" :oConfigurationIPRequest.oInsertTemplateInteraction._X_OLD_CLAROLOCAL1;
                oInteractPlus.oldClaroLocal2 = (oConfigurationIPRequest.oInsertTemplateInteraction._X_OLD_CLAROLOCAL2== null) ? "" :oConfigurationIPRequest.oInsertTemplateInteraction._X_OLD_CLAROLOCAL2;
                oInteractPlus.oldClaroLocal3 = (oConfigurationIPRequest.oInsertTemplateInteraction._X_OLD_CLAROLOCAL3== null) ? "" :oConfigurationIPRequest.oInsertTemplateInteraction._X_OLD_CLAROLOCAL3;
                oInteractPlus.oldClaroLocal4 = (oConfigurationIPRequest.oInsertTemplateInteraction._X_OLD_CLAROLOCAL4== null) ? "" :oConfigurationIPRequest.oInsertTemplateInteraction._X_OLD_CLAROLOCAL4;
                oInteractPlus.oldClaroLocal5 = (oConfigurationIPRequest.oInsertTemplateInteraction._X_OLD_CLAROLOCAL5== null) ? "" :oConfigurationIPRequest.oInsertTemplateInteraction._X_OLD_CLAROLOCAL5;
                oInteractPlus.oldClaroLocal6 = (oConfigurationIPRequest.oInsertTemplateInteraction._X_OLD_CLAROLOCAL6== null) ? "" :oConfigurationIPRequest.oInsertTemplateInteraction._X_OLD_CLAROLOCAL6;
                oInteractPlus.oldDocNumber = (oConfigurationIPRequest.oInsertTemplateInteraction._X_OLD_DOC_NUMBER== null) ? "" :oConfigurationIPRequest.oInsertTemplateInteraction._X_OLD_DOC_NUMBER;
                oInteractPlus.oldFirstName = (oConfigurationIPRequest.oInsertTemplateInteraction._X_OLD_FIRST_NAME== null) ? "" :oConfigurationIPRequest.oInsertTemplateInteraction._X_OLD_FIRST_NAME;
                oInteractPlus.oldFixedPhone = (oConfigurationIPRequest.oInsertTemplateInteraction._X_OLD_FIXED_PHONE== null) ? "" :oConfigurationIPRequest.oInsertTemplateInteraction._X_OLD_FIXED_PHONE;
                oInteractPlus.oldLastName = (oConfigurationIPRequest.oInsertTemplateInteraction._X_OLD_LAST_NAME== null) ? "" :oConfigurationIPRequest.oInsertTemplateInteraction._X_OLD_LAST_NAME;
                oInteractPlus.oldLdiNumber = (oConfigurationIPRequest.oInsertTemplateInteraction._X_OLD_LDI_NUMBER== null) ? "" :oConfigurationIPRequest.oInsertTemplateInteraction._X_OLD_LDI_NUMBER;
                oInteractPlus.oldFixedNumber = (oConfigurationIPRequest.oInsertTemplateInteraction._X_OLD_FIXED_NUMBER== null) ? "" :oConfigurationIPRequest.oInsertTemplateInteraction._X_OLD_FIXED_NUMBER;
                oInteractPlus.operationType = (oConfigurationIPRequest.oInsertTemplateInteraction._X_OPERATION_TYPE== null) ? "" :oConfigurationIPRequest.oInsertTemplateInteraction._X_OPERATION_TYPE;
                oInteractPlus.otherDocNumber = (oConfigurationIPRequest.oInsertTemplateInteraction._X_OTHER_DOC_NUMBER== null) ? "" :oConfigurationIPRequest.oInsertTemplateInteraction._X_OTHER_DOC_NUMBER;
                oInteractPlus.otherFirstName = (oConfigurationIPRequest.oInsertTemplateInteraction._X_OTHER_FIRST_NAME== null) ? "" :oConfigurationIPRequest.oInsertTemplateInteraction._X_OTHER_FIRST_NAME;
                oInteractPlus.otherLastName = (oConfigurationIPRequest.oInsertTemplateInteraction._X_OTHER_LAST_NAME== null) ? "" :oConfigurationIPRequest.oInsertTemplateInteraction._X_OTHER_LAST_NAME;
                oInteractPlus.otherPhone = (oConfigurationIPRequest.oInsertTemplateInteraction._X_OTHER_PHONE== null) ? "" :oConfigurationIPRequest.oInsertTemplateInteraction._X_OTHER_PHONE;
                oInteractPlus.phoneLegalRep = (oConfigurationIPRequest.oInsertTemplateInteraction._X_PHONE_LEGAL_REP== null) ? "" :oConfigurationIPRequest.oInsertTemplateInteraction._X_PHONE_LEGAL_REP;
                oInteractPlus.referencePhone = (oConfigurationIPRequest.oInsertTemplateInteraction._X_REFERENCE_PHONE== null) ? "" :oConfigurationIPRequest.oInsertTemplateInteraction._X_REFERENCE_PHONE;
                oInteractPlus.reason = (oConfigurationIPRequest.oInsertTemplateInteraction._X_REASON== null) ? "" :oConfigurationIPRequest.oInsertTemplateInteraction._X_REASON;
                oInteractPlus.model = (oConfigurationIPRequest.oInsertTemplateInteraction._X_MODEL== null) ? "" :oConfigurationIPRequest.oInsertTemplateInteraction._X_MODEL;
                oInteractPlus.lotCode = (oConfigurationIPRequest.oInsertTemplateInteraction._X_LOT_CODE== null) ? "" :oConfigurationIPRequest.oInsertTemplateInteraction._X_LOT_CODE;
                oInteractPlus.flagRegistered = (oConfigurationIPRequest.oInsertTemplateInteraction._X_FLAG_REGISTERED== null) ? "" :oConfigurationIPRequest.oInsertTemplateInteraction._X_FLAG_REGISTERED;
                oInteractPlus.registrationReason = (oConfigurationIPRequest.oInsertTemplateInteraction._X_REGISTRATION_REASON== null) ? "" :oConfigurationIPRequest.oInsertTemplateInteraction._X_REGISTRATION_REASON;
                oInteractPlus.claroNumber = (oConfigurationIPRequest.oInsertTemplateInteraction._X_CLARO_NUMBER== null) ? "" :oConfigurationIPRequest.oInsertTemplateInteraction._X_CLARO_NUMBER;
                oInteractPlus.month = (oConfigurationIPRequest.oInsertTemplateInteraction._X_MONTH== null) ? "" :oConfigurationIPRequest.oInsertTemplateInteraction._X_MONTH;
                oInteractPlus.ostNumber = (oConfigurationIPRequest.oInsertTemplateInteraction._X_OST_NUMBER== null) ? "" :oConfigurationIPRequest.oInsertTemplateInteraction._X_OST_NUMBER;
                oInteractPlus.basket = (oConfigurationIPRequest.oInsertTemplateInteraction._X_BASKET == null) ? "" : oConfigurationIPRequest.oInsertTemplateInteraction._X_BASKET;
                //<!--FORMATO DIAMESAÑO - CONVERTIDO EN BPEL A AÑO-MES-DIA -->
                oInteractPlus.expireDate = "01012000"; // oConfigurationIPRequest.oInsertTemplateInteraction._X_EXPIRE_DATE.ToString();
                oInteractPlus.address5 = (oConfigurationIPRequest.oInsertTemplateInteraction._X_ADDRESS5 == null) ? "" :oConfigurationIPRequest.oInsertTemplateInteraction._X_ADDRESS5 ;
                oInteractPlus.chargeAmount = ConvertHFC.ToString(oConfigurationIPRequest.oInsertTemplateInteraction._X_CHARGE_AMOUNT);
                oInteractPlus.city = (oConfigurationIPRequest.oInsertTemplateInteraction._X_CITY == null) ? "" :oConfigurationIPRequest.oInsertTemplateInteraction._X_CITY;
                oInteractPlus.contactSex = (oConfigurationIPRequest.oInsertTemplateInteraction._X_CONTACT_SEX == null) ? "" :oConfigurationIPRequest.oInsertTemplateInteraction._X_CONTACT_SEX;
                oInteractPlus.department = (oConfigurationIPRequest.oInsertTemplateInteraction._X_DEPARTMENT == null) ? "" :oConfigurationIPRequest.oInsertTemplateInteraction._X_DEPARTMENT;
                oInteractPlus.district = (oConfigurationIPRequest.oInsertTemplateInteraction._X_DISTRICT == null) ? "" :oConfigurationIPRequest.oInsertTemplateInteraction._X_DISTRICT;
                oInteractPlus.emailConfirmation = (oConfigurationIPRequest.oInsertTemplateInteraction._X_EMAIL_CONFIRMATION == null) ? "" :oConfigurationIPRequest.oInsertTemplateInteraction._X_EMAIL_CONFIRMATION;
                oInteractPlus.fax = (oConfigurationIPRequest.oInsertTemplateInteraction._X_FAX == null) ? "" :oConfigurationIPRequest.oInsertTemplateInteraction._X_FAX;
                oInteractPlus.flagCharge = (oConfigurationIPRequest.oInsertTemplateInteraction._X_FLAG_CHARGE == null) ? "" :oConfigurationIPRequest.oInsertTemplateInteraction._X_FLAG_CHARGE;
                oInteractPlus.maritalStatus = (oConfigurationIPRequest.oInsertTemplateInteraction._X_MARITAL_STATUS == null) ? "" :oConfigurationIPRequest.oInsertTemplateInteraction._X_MARITAL_STATUS;
                oInteractPlus.occupation = (oConfigurationIPRequest.oInsertTemplateInteraction._X_OCCUPATION == null) ? "" :oConfigurationIPRequest.oInsertTemplateInteraction._X_OCCUPATION;
                oInteractPlus.position = (oConfigurationIPRequest.oInsertTemplateInteraction._X_POSITION == null) ? "" :oConfigurationIPRequest.oInsertTemplateInteraction._X_POSITION;
                oInteractPlus.referenceAddress = (oConfigurationIPRequest.oInsertTemplateInteraction._X_REFERENCE_ADDRESS == null) ? "" :oConfigurationIPRequest.oInsertTemplateInteraction._X_REFERENCE_ADDRESS;
                oInteractPlus.typeDocument = (oConfigurationIPRequest.oInsertTemplateInteraction._X_TYPE_DOCUMENT == null) ? "" :oConfigurationIPRequest.oInsertTemplateInteraction._X_TYPE_DOCUMENT;
                oInteractPlus.zipCode = (oConfigurationIPRequest.oInsertTemplateInteraction._X_ZIPCODE == null) ? "" :oConfigurationIPRequest.oInsertTemplateInteraction._X_ZIPCODE;
                oInteractPlus.iccid = (oConfigurationIPRequest.oInsertTemplateInteraction._X_ICCID == null) ? "" : oConfigurationIPRequest.oInsertTemplateInteraction._X_ICCID;



                oParamSOTHFC.tipoTrans = oConfigurationIPRequest.strJobType; //Tipo Trabajo
                oParamSOTHFC.codSolot = oConfigurationIPRequest.strCodSolot; // "19553545"; //?
                oParamSOTHFC.tipo = oConfigurationIPRequest.strTypeServices; //Tipo Servicio
                oParamSOTHFC.codinssrv = null;// "2777835"; // Codigo de Instancia Servicio

                oconfiguracionIPRequest.contactUser = oContactUser;
                oconfiguracionIPRequest.customerClfy = oCustomerClfy;
                oconfiguracionIPRequest.interact = oInteract;
                oconfiguracionIPRequest.interactPlus = oInteractPlus;
                oconfiguracionIPRequest.paramSOTHFC = oParamSOTHFC;

                //ConfigServices.configuracionIPResponse oConfiguracionIPResponse_ = new ConfigServices.configuracionIPResponse();
                var objResponseBody = new ConfigServices.configuracionIPResponse();
                var objResponseHeader = Claro.Web.Logging.ExecuteMethod(oConfigurationIPRequest.Audit.Session, oConfigurationIPRequest.Audit.Transaction,
                    () =>
                    {
                        return Configuration.ServiceConfiguration.FIXED_CONFIGURATION_SERVICES.ejecutarConfiguracion(oHeaderRequestType, oconfiguracionIPRequest, out objResponseBody);
                    });
 
                
                if (objResponseBody != null)
                {
                    if (objResponseBody.responseStatus != null)
                    {
                        oConfigurationIPResponse.oResponseStatus.intEstado = objResponseBody.responseStatus.estado;
                        oConfigurationIPResponse.oResponseStatus.strCodigoRespuesta = objResponseBody.responseStatus.codigoRespuesta;
                        oConfigurationIPResponse.oResponseStatus.strDescripcionRespuesta = objResponseBody.responseStatus.descripcionRespuesta;
                        oConfigurationIPResponse.oResponseStatus.strOrigen = objResponseBody.responseStatus.origen;
                        oConfigurationIPResponse.oResponseStatus.strUbicacionError = objResponseBody.responseStatus.ubicacionError;
                        oConfigurationIPResponse.oResponseStatus.dFecha = objResponseBody.responseStatus.fecha;
                        
                        oConfigurationIPResponse.strRutaConstacy =  Convert.ToString(objResponseBody.responseData.rutaConsntacia);
                        oConfigurationIPResponse.strNroSOT = Convert.ToString(objResponseBody.responseData.numeroSOT);
                        oConfigurationIPResponse.strIdInteraccion = Convert.ToString(objResponseBody.responseData.idInteraccion);
                        
                    }

                    //oConfigurationIPResponse.strCodigoRespuesta = objResponseBody.codigoRespuesta;
                    //oConfigurationIPResponse.strDescripcionRespuesta = objResponseBody.descripcionRespuesta;
                }
            }
            catch (Exception ex)
            {
                Web.Logging.Error(oConfigurationIPRequest.Audit.Session, oConfigurationIPRequest.Audit.Transaction, ex.Message);
                Web.Logging.Info("Session: 123456789", "Transaction: ConfigurationServicesSave ", "Error en ConfigurationServicesSave: " + ex.Message);
               
                // throw;
            }
            Web.Logging.Info("Session: 123456789", "Transaction: ConfigurationServicesSave ", "sale strNroSOT: " + Functions.CheckStr(oConfigurationIPResponse.strNroSOT));

            return oConfigurationIPResponse;
        }

        //

        public static EntityFixed.GetConfigurationIP.ConfigurationIPResponse GetConfigurationIPMegas(string strIdSession, string strTransaction, string strCoId)
        {
            Claro.Web.Logging.Info(strIdSession,strTransaction,"GetConfigurationIPMegas HFC - IN");

            EntityFixed.GetConfigurationIP.ConfigurationIPResponse oConfigurationIPMegas = new EntityFixed.GetConfigurationIP.ConfigurationIPResponse();

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("an_cod_id", DbType.Int64, ParameterDirection.Input,strCoId),
                new DbParameter("an_flag",DbType.Int16,ParameterDirection.Output),
                new DbParameter("an_error",DbType.Int16,ParameterDirection.Output),
                new DbParameter("av_error", DbType.String,500, ParameterDirection.Output)
            };
            string anflag = string.Empty;
            string anerror = string.Empty;
            string averror = string.Empty;
            try
            {

                Claro.Web.Logging.Info(strIdSession, strTransaction, "GetConfigurationIPMegas HFC CONTRADOID" + ((strCoId == null) ? "" : strCoId));
              DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_SGASS_VAL_VEL_HFC, parameters);

              anflag = Convert.ToString(parameters[1].Value.ToString());
              anerror = Convert.ToString(parameters[2].Value.ToString());
              averror = Convert.ToString(parameters[3].Value.ToString());
            }
            catch (Exception ex)
            {
                
                Claro.Web.Logging.Error(strIdSession, strTransaction, Claro.SIACU.Transac.Service.Functions.GetExceptionMessage(ex));
            }
            oConfigurationIPMegas.strflag = anflag;
            oConfigurationIPMegas.strCodigoRespuesta = anerror;
            oConfigurationIPMegas.strDescripcionRespuesta = averror;

            Claro.Web.Logging.Info(strIdSession, strTransaction, "GetConfigurationIPMegas HFC - anflag: " + anflag + "| anerror: " + anerror + "| averror: " + averror);

            Claro.Web.Logging.Info(strIdSession, strTransaction, "GetConfigurationIPMegas HFC - OUT");
            return oConfigurationIPMegas;
        }

    }
}
