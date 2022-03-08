using Claro.Data;


namespace Claro.SIACU.Data.Transac.Service.Configuration
{
    internal struct DbCommandConfiguration : IDbCommandConfig
    {

        public static readonly IDbCommandConfig SIACU_SP_SP_CREATE_CONTACT_NO_USER_PORT = DbCommandConfiguration.Create("SIACU_SP_SP_CREATE_CONTACT_NO_USER_PORT");
        public static readonly IDbCommandConfig SIACU_POST_BSCS_LISTA_TELEFONO_LTE = DbCommandConfiguration.Create("SIACU_POST_BSCS_SP_LISTA_TELEFONO_LTE");

        #region  Fase 03

            #region AdditionalPoints       
            public static readonly IDbCommandConfig SIACU_SP_P_CONSULTA_EQU = DbCommandConfiguration.Create("SIACU_SP_P_CONSULTA_EQU");
            public static readonly IDbCommandConfig SIACU_SP_COD_UBIGEO = DbCommandConfiguration.Create("SIACU_SP_COD_UBIGEO");
            public static readonly IDbCommandConfig SIACU_SP_GENERA_SOT_SIAC = DbCommandConfiguration.Create("SIACU_SP_GENERA_SOT_SIAC");
            public static readonly IDbCommandConfig SIACU_SP_REGISTRA_ETA_REQ = DbCommandConfiguration.Create("SIACU_SP_REGISTRA_ETA_REQ");
            public static readonly IDbCommandConfig SIACU_SP_REGISTRA_ETA_RESP = DbCommandConfiguration.Create("SIACU_SP_REGISTRA_ETA_RESP");
            public static readonly IDbCommandConfig SIACU_SP_REGISTRA_COSTO_PA = DbCommandConfiguration.Create("SIACU_SP_REGISTRA_COSTO_PA");
            public static readonly IDbCommandConfig SIACU_SP_ACTUALIZAR_COSTO_PA = DbCommandConfiguration.Create("SIACU_SP_ACTUALIZAR_COSTO_PA");
            #endregion

            #region ConfigurationIP
            public static readonly IDbCommandConfig SIACU_SGASS_TIP_TRAB_CONFIP = DbCommandConfiguration.Create("SIACU_SGASS_TIP_TRAB_CONFIP");
            public static readonly IDbCommandConfig SIACU_SGASS_TIPO_CONFIP = DbCommandConfiguration.Create("SIACU_SGASS_TIPO_CONFIP");
            public static readonly IDbCommandConfig SIACU_SGASS_SUCURSALES_CLIENTE = DbCommandConfiguration.Create("SIACU_SGASS_SUCURSALES_CLIENTE");
            public static readonly IDbCommandConfig SIACU_POST_BSCS_SP_DATOS_LINEA = DbCommandConfiguration.Create("SIACU_POST_BSCS_SP_DATOS_LINEA");
        
            #endregion
       #endregion

            //Inicio roberto
        public static readonly IDbCommandConfig SIACU_CONSULTA_POSTT_SERVICIOPROG = DbCommandConfiguration.Create("SIACU_CONSULTA_POSTT_SERVICIOPROG");
        public static readonly IDbCommandConfig SIACU_TIM081_SP_CF_DN_NUM_OR_CO_ID = DbCommandConfiguration.Create("SIACU_TIM081_SP_CF_DN_NUM_OR_CO_ID");
        public static readonly IDbCommandConfig SIACU_TFUN014_PLAN_TARIFARIO = DbCommandConfiguration.Create("SIACU_TFUN014_PLAN_TARIFARIO");
        public static readonly IDbCommandConfig SIACU_SP_SERVICIOS_X_PLAN = DbCommandConfiguration.Create("SIACU_SP_SERVICIOS_X_PLAN");
        public static readonly IDbCommandConfig SIACU_SP_VALIDA_BOLSA_COMP = DbCommandConfiguration.Create("SIACU_SP_VALIDA_BOLSA_COMP");
        public static readonly IDbCommandConfig SIACU_P_VAL_PROGXPRODUCTO = DbCommandConfiguration.Create("SIACU_P_VAL_PROGXPRODUCTO");
        public static readonly IDbCommandConfig SIACU_sp_datos_x_cta = DbCommandConfiguration.Create("SIACU_sp_datos_x_cta");
        public static readonly IDbCommandConfig SIACU_sp_datos_x_contr = DbCommandConfiguration.Create("SIACU_sp_datos_x_contr");
        public static readonly IDbCommandConfig SIACU_SP_INSERTAR_SERVICIOSPLAN = DbCommandConfiguration.Create("SIACU_SP_INSERTAR_SERVICIOSPLAN");
        public static readonly IDbCommandConfig SIACU_SP_UPDATE_INTERACT_X_INTER29 = DbCommandConfiguration.Create("SIACU_SP_UPDATE_INTERACT_X_INTER29");
        public static readonly IDbCommandConfig SIACU_TIM100_CONSULTA_TOPE_CONSUMO = DbCommandConfiguration.Create("SIACU_TIM100_CONSULTA_TOPE_CONSUMO");
        public static readonly IDbCommandConfig SIACU_SP_BUSCA_SERV_PLAN_MANT = DbCommandConfiguration.Create("SIACU_SP_BUSCA_SERV_PLAN_MANT");
        public static readonly IDbCommandConfig SIACU_TOLS_OBTENERDATOSDECUENTA = DbCommandConfiguration.Create("SIACU_TOLS_OBTENERDATOSDECUENTA");
        public static readonly IDbCommandConfig SIACU_TOLS_CONSULTARTEMPTAG1220 = DbCommandConfiguration.Create("SIACU_TOLS_CONSULTARTEMPTAG1220");
        public static readonly IDbCommandConfig SIACU_SP_VALIDA_MAIL = DbCommandConfiguration.Create("SIACU_SP_VALIDA_MAIL");
        public static readonly IDbCommandConfig SIACU_SP_OBTIENE_LISTAS = DbCommandConfiguration.Create("SIACU_SP_OBTIENE_LISTAS");
        public static readonly IDbCommandConfig SIACU_SP_CON_CONTRATO = DbCommandConfiguration.Create("SIACU_SP_CON_CONTRATO");
        public static readonly IDbCommandConfig SIACU_SP_BUSCA_PLAN_X_CODPROD = DbCommandConfiguration.Create("SIACU_SP_BUSCA_PLAN_X_CODPROD");
        public static readonly IDbCommandConfig SIACU_SP_SIACS_PLAN_TARIFARIO = DbCommandConfiguration.Create("SIACU_SP_SIACS_PLAN_TARIFARIO");
        public static readonly IDbCommandConfig SIACU_SP_CONSULTA_PORTABILIDAD = DbCommandConfiguration.Create("SIACU_SP_CONSULTA_PORTABILIDAD");
        public static readonly IDbCommandConfig SIACU_SP_OBTENER_DATO = DbCommandConfiguration.Create("SIACU_SP_OBTENER_DATO");
        public static readonly IDbCommandConfig SIACU_POST_DB_SP_OBTENER_PARAMETRO = DbCommandConfiguration.Create("SIACU_OBTENER_PARAMETRO");
        public static readonly IDbCommandConfig SIACU_DBTO_SP_DETALLE_LLAMADAS = DbCommandConfiguration.Create("SIACU_DBTO_SP_DETALLE_LLAMADAS");
        public static readonly IDbCommandConfig SIACU_SP_OBTENER_NUMERO = DbCommandConfiguration.Create("SIACU_SP_OBTENER_NUMERO");
        public static readonly IDbCommandConfig SIACU_SP_OBTENER_NUMERO_PORT = DbCommandConfiguration.Create("SIACU_SP_OBTENER_NUMERO_PORT");
        public static readonly IDbCommandConfig SIACU_SP_CUSTOMER_MARITAL_STATUS = Create("SIACU_SP_CUSTOMER_MARITAL_STATUS");
        public static readonly IDbCommandConfig SIACU_SP_CUSTOMER_OCCUPATION = Create("SIACU_SP_CUSTOMER_OCCUPATION");
        public static readonly IDbCommandConfig SIACU_SP_PREPAID_REGISTRATION_REASON = Create("SIACU_SP_PREPAID_REGISTRATION_REASON");
        public static readonly IDbCommandConfig SIACU_SP_EVALUAR_MONTO_AUTORIZAR_DCM = Create("SIACU_SP_EVALUAR_MONTO_AUTORIZAR_DCM");
        public static readonly IDbCommandConfig SIACU_SP_EVALUAR_MONTO_AUTORIZAR = Create("SIACU_SP_EVALUAR_MONTO_AUTORIZAR");
        public static readonly IDbCommandConfig SIACU_SP_SHOW_LIST_ELEMENT = Create("SIACU_SP_SHOW_LIST_ELEMENT");
        public static readonly IDbCommandConfig SIACU_SP_OBTENER_CODIGO = DbCommandConfiguration.Create("SIACU_SP_OBTENER_CODIGO");
        public static readonly IDbCommandConfig SIACU_POST_BSCS_SP_OBTENER_DATOS_NF_HFC = Create("SIACU_POST_BSCS_SP_OBTENER_DATOS_NF_HFC");
        public static readonly IDbCommandConfig SIACU_POST_BSCS_SP_LISTAR_SERVICIOS_TELEFONO = Create("SIACU_POST_BSCS_SP_LISTAR_SERVICIOS_TELEFONO");
        public static readonly IDbCommandConfig SIACU_POST_BSCS_SP_LISTAR_PROMOCIONES = Create("SIACU_POST_BSCS_SP_LISTAR_PROMOCIONES");
        public static readonly IDbCommandConfig SIACU_POST_BSCS_SP_GET_PLAN_COMERCIAL = Create("SIACU_POST_BSCS_SP_GET_PLAN_COMERCIAL");
        public static readonly IDbCommandConfig SIACU_POST_BSCS_SP_LISTA_TELEFONO = Create("SIACU_POST_BSCS_SP_LISTA_TELEFONO");
        public static readonly IDbCommandConfig SIACU_POST_BSCS_SP_DET_LLAMADA = Create("SIACU_POST_BSCS_SP_DET_LLAMADA");
        public static readonly IDbCommandConfig SIACU_TFUN051_GET_DNNUM_FROM_COID = Create("SIACU_TFUN051_GET_DNNUM_FROM_COID");
        #region Proy-32650
        public static readonly IDbCommandConfig SIACU_POST_BSCS_SP_LST_PERIOD_X_CF_SA = Create("SIACU_POST_BSCS_SP_LST_PERIOD_X_CF_SA");
        public static readonly IDbCommandConfig SIACU_POST_PVU_SP_CON_PLAN_SERVICIO = Create("SIACU_POST_PVU_SP_CON_PLAN_SERVICIO");
        public static readonly IDbCommandConfig SIACU_POST_BSCS_BSCSSS_CONSUL_PORCENT = DbCommandConfiguration.Create("SIACU_POST_BSCS_BSCSSS_CONSUL_PORCENT");
        public static readonly IDbCommandConfig BSCSSS_TOTAL_INVERSION = DbCommandConfiguration.Create("BSCSSS_TOTAL_INVERSION");
        public static readonly IDbCommandConfig SIACU_BSCSSS_DESC_CARGOFIJO = DbCommandConfiguration.Create("SIACU_BSCSSS_DESC_CARGOFIJO");
        public static readonly IDbCommandConfig SIACU_BSCSSI_REG_DESCCP = DbCommandConfiguration.Create("SIACU_BSCSSI_REG_DESCCP");
        public static readonly IDbCommandConfig SIACU_POST_BSCS_BSCSSI_REG_BONOS_DESC = DbCommandConfiguration.Create("SIACU_POST_BSCS_BSCSSI_REG_BONOS_DESC");
        public static readonly IDbCommandConfig SIACU_POST_BSCS_SP_ACTUALIZA_DATOS_MENORES = DbCommandConfiguration.Create("SIACU_POST_BSCS_SP_ACTUALIZA_DATOS_MENORES");
        public static readonly IDbCommandConfig SIAC_POST_SP_UPDATE_CUSTOMER_CLF = DbCommandConfiguration.Create("SIAC_POST_SP_UPDATE_CUSTOMER_CLF");
        public static readonly IDbCommandConfig SIACU_POST_BSCSSS_CONSUL_SERVICIOS = DbCommandConfiguration.Create("SIACU_POST_BSCSSS_CONSUL_SERVICIOS");
        //Fase 2
        public static readonly IDbCommandConfig SIACU_SIACSS_PARAMETROSBSCS = DbCommandConfiguration.Create("SIACU_SIACSS_PARAMETROSBSCS");
        #endregion
        public static readonly IDbCommandConfig SIACU_POST_CLARIFY_SP_UPDATE_X_INTER_30 = Create("SIACU_POST_CLARIFY_SP_UPDATE_X_INTER_30");
        public static readonly IDbCommandConfig SIACU_POST_CLARIFY_SP_QUERY_PLUS_INTER = Create("SIACU_POST_CLARIFY_SP_QUERY_PLUS_INTER");
        public static readonly IDbCommandConfig SIACU_POST_CLARIFY_SP_CREATE_INTERACT_HFC = Create("SIACU_POST_CLARIFY_SP_CREATE_INTERACT_HFC");
        public static readonly IDbCommandConfig SIACU_POST_CLARIFY_SP_CUSTOMER_CLFY_HFC = Create("SIACU_POST_CLARIFY_SP_CUSTOMER_CLFY_HFC");
        public static readonly IDbCommandConfig SIACU_POST_CLARIFY_SP_CREATE_PLUS_INTER = Create("SIACU_POST_CLARIFY_SP_CREATE_PLUS_INTER");
        
        // INTERACTION
        public static readonly IDbCommandConfig SIACU_POST_CLARIFY_SP_QUERY_INTERACT = Create("SIACU_POST_CLARIFY_SP_QUERY_INTERACT");
        public static readonly IDbCommandConfig SIACU_POST_CLARIFY_SP_INS_DET_INTERACCION = Create("SIACU_POST_CLARIFY_SP_INS_DET_INTERACCION");
        public static readonly IDbCommandConfig SIACU_POST_CLARIFY_SP_CREATE_INTERACT = Create("SIACU_POST_CLARIFY_SP_CREATE_INTERACT");
        public static readonly IDbCommandConfig SIACU_POST_CLARIFY_SP_CUSTOMER_CLFY = Create("SIACU_POST_CLARIFY_SP_CUSTOMER_CLFY");
        public static readonly IDbCommandConfig SIACU_POST_CLARIFY_SP_INS_DET_INTERACCION_DETALLE = Create("SIACU_POST_CLARIFY_SP_INS_DET_INTERACCION_DETALLE");

        //Rodolfo
        public static readonly IDbCommandConfig SIACU_CONSULTAR_REGLAS_ATENCION = DbCommandConfiguration.Create("SIACU_SP_CONSULTAR_REGLAS_ATENCION");
        public static readonly IDbCommandConfig SIACU_CONSULTAR_REGIONES = DbCommandConfiguration.Create("SIACU_SP_CONSULTAR_REGIONES");
        public static readonly IDbCommandConfig SIACU_TABLA_NROHLR_TELEFONO = DbCommandConfiguration.Create("SIACU_SP_TABLA_NROHLR_TELEFONO");
        public static readonly IDbCommandConfig SIACU_TABLA_HLR2_INSI = DbCommandConfiguration.Create("SIACU_SP_TABLA_HLR2_INSI");
        public static readonly IDbCommandConfig SIACU_VALIDA_CAMBIO_NUMERO_BSCS = DbCommandConfiguration.Create("SIACU_SP_VALIDAR_CAMBIO_NUMERO_BSCS");
        public static readonly IDbCommandConfig SIACU_HISTORICO_BAJA_USUARIOS = DbCommandConfiguration.Create("SIACU_SP_HISTORICO_BAJA_USUARIOS");
        public static readonly IDbCommandConfig SIACU_OBTENER_TIPIFICACIONES = DbCommandConfiguration.Create("SIACU_SP_OBTENER_TIPIFICACION");

        public static readonly IDbCommandConfig SIACU_ALINEA_SERV_DESACT = DbCommandConfiguration.Create("SIACU_SP_ALINEA_SERV_DESACT");
        public static readonly IDbCommandConfig SIACU_ALINEA_CO_ID = DbCommandConfiguration.Create("SIACU_SP_ALINEA_CO_ID");

        //SIAC POST DB
        public static readonly IDbCommandConfig SIACU_POST_DB_SP_INSERTAR_INTERACT = Create("SIACU_POST_DB_SP_INSERTAR_INTERACT");
        public static readonly IDbCommandConfig SIACU_POST_DB_SP_INSERTAR_X_PLUS_INTER = Create("SIACU_POST_DB_SP_INSERTAR_X_PLUS_INTER");

        //SIAC BSCS Change type customer
        public static readonly IDbCommandConfig SIACU_MGRSS_TIM_SP_VALIDA_CICLO_FACT = Create("SIACU_MGRSS_TIM_SP_VALIDA_CICLO_FACT");
        public static readonly IDbCommandConfig SIACU_MGRSS_USRSIAC_GR_LISTAMOTIVO = Create("SIACU_MGRSS_USRSIAC_GR_LISTAMOTIVO");
        public static readonly IDbCommandConfig SIACU_MGRSS_USRSIAC_GR_LISTASUBMOTIVO = Create("SIACU_MGRSS_USRSIAC_GR_LISTASUBMOTIVO");
        public static readonly IDbCommandConfig SIACU_MGRSS_USRSIAC_GR_LISTAAREA = Create("SIACU_MGRSS_USRSIAC_GR_LISTAAREA");
        public static readonly IDbCommandConfig SIACU_SP_ST_CONSULTAS_USUARIO = Create("SIACU_SP_ST_CONSULTAS_USUARIO");
        public static readonly IDbCommandConfig SIACU_SP_REG_PLAN_COMERCIAL = Create("SIACU_SP_REG_PLAN_COMERCIAL");

        #region RetenciónCancelación

        public static readonly IDbCommandConfig SIACU_POST_PVU_LISTA_ACCIONES_RETENCION = DbCommandConfiguration.Create("SIACU_POST_PVU_LISTA_ACCIONES_RETENCION");
        public static readonly IDbCommandConfig SIACU_POST_PVU_LISTAR_MOTIVOS_RETENCION = DbCommandConfiguration.Create("SIACU_POST_PVU_LISTAR_MOTIVOS_RETENCION");
        public static readonly IDbCommandConfig SIACU_POST_PVU_LISTAR_SUBMOTIVOS_RETENCION = DbCommandConfiguration.Create("SIACU_POST_PVU_LISTAR_SUBMOTIVOS_RETENCION");
        public static readonly IDbCommandConfig SIACU_POST_SGA_P_CONSULTA_MOTIVO = DbCommandConfiguration.Create("SIACU_POST_SGA_P_CONSULTA_MOTIVO");
        public static readonly IDbCommandConfig SIACU_POST_SGA_P_AGREGAR_DIAS_LABORABLES = DbCommandConfiguration.Create("SIACU_POST_SGA_P_AGREGAR_DIAS_LABORABLES");
        public static readonly IDbCommandConfig SIACU_POST_COBS_SSSIGA_OBTENER_DATOS_BSCS_EXT = DbCommandConfiguration.Create("SIACU_POST_COBS_SSSIGA_OBTENER_DATOS_BSCS_EXT");
        public static readonly IDbCommandConfig SIACU_POST_SIGA_SSSIGA_OBTENER_PENALIDAD_EXT = DbCommandConfiguration.Create("SIACU_POST_SIGA_SSSIGA_OBTENER_PENALIDAD_EXT");
        public static readonly IDbCommandConfig SIACU_POST_CLARIFY_SP_CREATE_CONTACT_USERLDI = DbCommandConfiguration.Create("SIACU_POST_CLARIFY_SP_CREATE_CONTACT_USERLDI");
        public static readonly IDbCommandConfig SIACU_POST_SGA_P_REGISTRA_ETA_SEL = DbCommandConfiguration.Create("SIACU_POST_SGA_P_REGISTRA_ETA_SEL");
        public static readonly IDbCommandConfig SIACU_POST_SIGA_SP_OBTENER_APADECE = DbCommandConfiguration.Create("SIACU_POST_SIGA_SP_OBTENER_APADECE");
        public static readonly IDbCommandConfig SIACU_POST_CLARIFY_CREATE_CASE_HFC = DbCommandConfiguration.Create("SIACU_POST_CLARIFY_CREATE_CASE_HFC");
        public static readonly IDbCommandConfig SIACU_POST_SGA_P_CONSULTA_TIPTRA = DbCommandConfiguration.Create("SIACU_POST_SGA_P_CONSULTA_TIPTRA");
        public static readonly IDbCommandConfig SIACU_SP_NOTES_INTERACT = DbCommandConfiguration.Create("SIACU_SP_NOTES_INTERACT");
        public static readonly IDbCommandConfig SIACU_SP_QUERY_PLUS_CASE = DbCommandConfiguration.Create("SIACU_SP_QUERY_PLUS_CASE");
        public static readonly IDbCommandConfig SIACU_SP_UPDATE_PLUS_CASE = DbCommandConfiguration.Create("SIACU_SP_UPDATE_PLUS_CASE");
        public static readonly IDbCommandConfig SIACU_SP_INTERACT_ID_HFC = DbCommandConfiguration.Create("SIACU_SP_INTERACT_ID_HFC");
        public static readonly IDbCommandConfig SIACU_POST_COBS_INSERTAR_CASE = DbCommandConfiguration.Create("SIACU_POST_COBS_INSERTAR_CASE");
        public static readonly IDbCommandConfig SIACU_PS_CREATE_PLUS_CASE = DbCommandConfiguration.Create("SIACU_PS_CREATE_PLUS_CASE");

        public static readonly IDbCommandConfig SIACU_POST_INSERTAR_X_PLUS_CASE  = DbCommandConfiguration.Create("SIACU_POST_INSERTAR_X_PLUS_CASE");
        public static readonly IDbCommandConfig SIACU_P_CONSULTA_MOTIVOXTIPTRA = DbCommandConfiguration.Create("SIACU_P_CONSULTA_MOTIVOXTIPTRA");
        public static readonly IDbCommandConfig SIACU_CONSULTA_POSTT_SERVICIOPROG_HFC = DbCommandConfiguration.Create("SIACU_CONSULTA_POSTT_SERVICIOPROG_HFC");
        public static readonly IDbCommandConfig SIACU_POST_SGA_SGASS_MOTIVOS_TIPTRA = DbCommandConfiguration.Create("SIACU_POST_SGA_SGASS_MOTIVOS_TIPTRA");
        public static readonly IDbCommandConfig SIACU_POST_PVU_SP_INSERTAR_EVIDENCIA_A = DbCommandConfiguration.Create("SIACU_POST_PVU_SP_INSERTAR_EVIDENCIA_A");

        //PostPago
        public static readonly IDbCommandConfig SIACU_POST_SIGA_CONSULTA_ACUERDO = DbCommandConfiguration.Create("SIACU_POST_SIGA_CONSULTA_ACUERDO");
        public static readonly IDbCommandConfig SIACU_POST_BSCS_CARGOFIJO_SERV_X_CLIENTE = DbCommandConfiguration.Create("SIACU_POST_BSCS_CARGOFIJO_SERV_X_CLIENTE");


        #endregion

        
        //SIAC DB SIAC
        public static readonly IDbCommandConfig SIACU_COMMON_SP_OBTENER_DATO = Create("SIACU_SP_OBTENER_DATO");
        public static readonly IDbCommandConfig SIACU_CLARIFY_SP_CUSTOMER_DOC_TYPE = Create("SIACU_SP_CUSTOMER_DOC_TYPE");


        //SIAC DB 
        public static readonly IDbCommandConfig SIACU_COMMON_SP_INSERTAR_LOG_TRX = Create("SIACU_SP_INSERTAR_LOG_TRX");
        

        public static readonly IDbCommandConfig SIACU_SP_TIM113_CONS_LINEAS_ASOC = DbCommandConfiguration.Create("SIACU_SP_TIM113_CONS_LINEAS_ASOC");
        public static readonly IDbCommandConfig SIACU_SP_DET_LLAM_ENTRANTES = DbCommandConfiguration.Create("SIACU_SP_DET_LLAM_ENTRANTES");
        public static readonly IDbCommandConfig SIACU_SP_LISTAR_MARCA = DbCommandConfiguration.Create("SIACU_SP_LISTAR_MARCA");
        public static readonly IDbCommandConfig SIACU_SP_LISTAR_DEPARTAMENTO = DbCommandConfiguration.Create("SIACU_SP_LISTAR_DEPARTAMENTO");
        public static readonly IDbCommandConfig SIACU_SP_LISTAR_MARCA_MODELO = DbCommandConfiguration.Create("SIACU_SP_LISTAR_MARCA_MODELO");
        public static readonly IDbCommandConfig SIACU_SP_LISTAR_PROVINCIA = DbCommandConfiguration.Create("SIACU_SP_LISTAR_PROVINCIA");
        public static readonly IDbCommandConfig SIACU_SP_LISTAR_DISTRITO = DbCommandConfiguration.Create("SIACU_SP_LISTAR_DISTRITO");
        public static readonly IDbCommandConfig SIACU_SP_BIRTHPLACE = DbCommandConfiguration.Create("SIACU_SP_BIRTHPLACE");
        public static readonly IDbCommandConfig SIACU_SP_SERVICIOS_VAS = DbCommandConfiguration.Create("SIACU_SP_SERVICIOS_VAS");
        public static readonly IDbCommandConfig SIACU_SP_UPDATE_INTERACT_X_AUDIT = DbCommandConfiguration.Create("SIACU_SP_UPDATE_INTERACT_X_AUDIT");
        public static readonly IDbCommandConfig SIACU_SP_TOLS_OBTENERDATOSDECUENTA = DbCommandConfiguration.Create("SIACU_SP_TOLS_OBTENERDATOSDECUENTA");
        public static readonly IDbCommandConfig SA_SP_SEARCH_CONTACT_USERLDI = DbCommandConfiguration.Create("SA_SP_SEARCH_CONTACT_USERLDI");
        public static readonly IDbCommandConfig SIACU_SP_AJUSTE_POR_RECLAMOS = DbCommandConfiguration.Create("SIACU_SP_AJUSTE_POR_RECLAMOS");
        //Additional Services
        public static readonly IDbCommandConfig SIACU_SP_TOTAL_CF_LC_X_CUENTA = DbCommandConfiguration.Create("SIACU_SP_TOTAL_CF_LC_X_CUENTA");
        public static readonly IDbCommandConfig SIACU_SP_VALIDAR_USUARIO = DbCommandConfiguration.Create("SIACU_SP_VALIDAR_USUARIO");
        public static readonly IDbCommandConfig SIACU_SP_SNCODE_X_CO_SER = DbCommandConfiguration.Create("SIACU_SP_SNCODE_X_CO_SER");
        public static readonly IDbCommandConfig SIACU_SP_MOD_CF_SERVICIO = DbCommandConfiguration.Create("SIACU_SP_MOD_CF_SERVICIO");

        public static readonly IDbCommandConfig SIACU_SP_PROD_X_SERV_CO_ID = DbCommandConfiguration.Create("SIACU_SP_PROD_X_SERV_CO_ID");
        public static readonly IDbCommandConfig SIACU_SP_CONSULTA_DECO = DbCommandConfiguration.Create("SIACU_SP_CONSULTA_DECO");

        public static readonly IDbCommandConfig SIAC_POST_SP_OBTENER_NUMERO_GWP = DbCommandConfiguration.Create("SIAC_POST_SP_OBTENER_NUMERO_GWP");
        public static readonly IDbCommandConfig SIAC_POST_SP_OBTENER_NUMERO_EAI = DbCommandConfiguration.Create("SIAC_POST_SP_OBTENER_NUMERO_EAI");

        public static readonly IDbCommandConfig SIAC_PREP_OBTENER_TIPIFICACION = DbCommandConfiguration.Create("SIAC_PREP_OBTENER_TIPIFICACION");
        public static readonly IDbCommandConfig PVU_MANTSS_LISTA_EDIFICIOHFC = DbCommandConfiguration.Create("PVU_MANTSS_LISTA_EDIFICIOHFC");
        public static readonly IDbCommandConfig PVU_SECSS_CON_PROVINCIA = DbCommandConfiguration.Create("PVU_SECSS_CON_PROVINCIA");
        public static readonly IDbCommandConfig PVU_SECSS_CON_DEPARTAMENTO = DbCommandConfiguration.Create("PVU_SECSS_CON_DEPARTAMENTO");
        public static readonly IDbCommandConfig PVU_SECSS_CON_DISTRITO = DbCommandConfiguration.Create("PVU_SECSS_CON_DISTRITO");

        public static readonly IDbCommandConfig SGA_P_GENERA_TRANSACCION = DbCommandConfiguration.Create("SGA_P_GENERA_TRANSACCION");
        public static readonly IDbCommandConfig CLFY_SP_UPDATE_X_INTER_30 = DbCommandConfiguration.Create("CLFY_SP_UPDATE_X_INTER_30");
        public static readonly IDbCommandConfig SGA_P_GENERA_SOT = DbCommandConfiguration.Create("SGA_P_GENERA_SOT");


        public static readonly IDbCommandConfig SIACU_OBTIENE_TIPOS_ZONA = DbCommandConfiguration.Create("SIACU_OBTIENE_TIPOS_ZONA");
        public static readonly IDbCommandConfig SIACU_OBTIENE_TIPO_MANZANA = DbCommandConfiguration.Create("SIACU_OBTIENE_TIPO_MANZANA");
        public static readonly IDbCommandConfig SIACU_OBTIENE_TIPOS_INTERIOR = DbCommandConfiguration.Create("SIACU_OBTIENE_TIPOS_INTERIOR");
        public static readonly IDbCommandConfig SIACU_POST_PVU_SP_CON_PLAN_SERVICIOLTE = DbCommandConfiguration.Create("SIACU_POST_PVU_SP_CON_PLAN_SERVICIOLTE");

        public static readonly IDbCommandConfig SIACU_SP_P_TOWN_CENTER = DbCommandConfiguration.Create("SIACU_SP_P_TOWN_CENTER");
        public static readonly IDbCommandConfig SIACU_SP_P_CONSULT_CENTER = DbCommandConfiguration.Create("SIACU_SP_P_CONSULT_CENTER");
        public static readonly IDbCommandConfig SIACU_SP_P_COBERTURA = DbCommandConfiguration.Create("SIACU_SP_P_COBERTURA");

        #region External/Internal -LTE
        public static readonly IDbCommandConfig SIACU_SP_LTE_P_CONSULT_MOTIVE = DbCommandConfiguration.Create("SIACU_SP_LTE_P_CONSULT_MOTIVE");
        public static readonly IDbCommandConfig SIACU_SP_LTE_P_CONSULT_TIPTRA = DbCommandConfiguration.Create("SIACU_SP_LTE_P_CONSULT_TIPTRA");
        public static readonly IDbCommandConfig SIACU_SP_LTE_P_CONSULT_EQU = DbCommandConfiguration.Create("SIACU_SP_LTE_P_CONSULT_EQU");
        public static readonly IDbCommandConfig SIACU_SP_LTE_P_GENERA_TRANSACCION = DbCommandConfiguration.Create("SIACU_SP_LTE_P_GENERA_TRANSACCION");
        #endregion

        #region ChangeEquipment
        public static readonly IDbCommandConfig SIACU_SP_LISTA_EQUIPOS_LTE = DbCommandConfiguration.Create("SIACU_SP_LISTA_EQUIPOS_LTE");
        #endregion
        #region "Inst/Desinst Decodificadores"
        public static readonly IDbCommandConfig SIACU_P_CONSULTA_EQU_IW = DbCommandConfiguration.Create("SIACU_P_CONSULTA_EQU_IW");
        public static readonly IDbCommandConfig SIACU_P_BAJA_DECO_ADICIONAL = DbCommandConfiguration.Create("SIACU_P_BAJA_DECO_ADICIONAL");
        public static readonly IDbCommandConfig SIACU_SP_CONS_DATA_DECO_BY_ID = DbCommandConfiguration.Create("SIACU_SP_CONS_DATA_DECO_BY_ID");
        public static readonly IDbCommandConfig SIACU_SP_INS_INTER_SERV_MP = DbCommandConfiguration.Create("SIACU_SP_INS_INTER_SERV_MP");
        public static readonly IDbCommandConfig SIACU_P_POSVENTA_DET_DECO_INSERTAR = DbCommandConfiguration.Create("SIACU_P_POSVENTA_DET_DECO_INSERTAR");
        public static readonly IDbCommandConfig SIACU_SIACSS_EQU_IW_TIP = DbCommandConfiguration.Create("SIACU_SIACSS_EQU_IW_TIP");
        public static readonly IDbCommandConfig SIACU_SGAFUN_OBT_MONTO_OCC = DbCommandConfiguration.Create("SIACU_SGAFUN_OBT_MONTO_OCC");
        public static readonly IDbCommandConfig SIACU_P_TIPO_DECO_LTE = DbCommandConfiguration.Create("SIACU_P_TIPO_DECO_LTE");
        public static readonly IDbCommandConfig SGASS_MATRIZ_DECOS = DbCommandConfiguration.Create("SGASS_MATRIZ_DECOS");
        #endregion

        //fdq
        public static readonly IDbCommandConfig SIACU_POST_SP_DETALLE_LLAMADAS = DbCommandConfiguration.Create("SIACU_POST_SP_DETALLE_LLAMADAS");
        public static readonly IDbCommandConfig SIACU_POST_SP_DETALLE_LLAMADAS_PDI = DbCommandConfiguration.Create("SIACU_POST_SP_DETALLE_LLAMADAS_PDI");
        public static readonly IDbCommandConfig SIACU_POST_SP_Recharge_List_DWO = DbCommandConfiguration.Create("SIACU_POST_SP_Recharge_List_DWO");

        public static readonly IDbCommandConfig SIACU_SP_OBTIENE_LISTAS_COBSDB = Create("SIACU_SP_OBTIENELISTAS_COBSDB");
        public static readonly IDbCommandConfig SIACU_SP_CONSULTA_OBT_SERVICIO_FIJA = Create("SIACU_SP_CONSULTAOBTSERVICIOFIJA");

        #region JCAA
        public static readonly IDbCommandConfig SIACU_LISTA_SERVICIOS_TELEFONO = DbCommandConfiguration.Create("SIACU_LISTA_SERVICIOS_TELEFONO");
        public static readonly IDbCommandConfig SIACU_P_GENERA_HORARIO_SIAC = DbCommandConfiguration.Create("SIACU_P_GENERA_HORARIO_SIAC");
        public static readonly IDbCommandConfig SIACU_P_CONSULTA_SUBTIPORD = DbCommandConfiguration.Create("SIACU_P_CONSULTA_SUBTIPORD");
        public static readonly IDbCommandConfig SIACU_SP_OBTIENE_TIPO_ORDEN_TIPTRA = DbCommandConfiguration.Create("SIACU_SP_OBTIENE_TIPO_ORDEN_TIPTRA");
        public static readonly IDbCommandConfig SIACU_SP_VALIDA_FLUJO_ZONA_ADC = DbCommandConfiguration.Create("SIACU_SP_VALIDA_FLUJO_ZONA_ADC");
        public static readonly IDbCommandConfig SIACU_SP_CONSULTA_TIPTRA = DbCommandConfiguration.Create("SIACU_SP_CONSULTA_TIPTRA");

        public static readonly IDbCommandConfig SIACU_SIUNSS_DATOS_SERVICIOS = DbCommandConfiguration.Create("SIACU_SIUNSS_DATOS_SERVICIOS");
        public static readonly IDbCommandConfig SIACU_SP_QUERY_INTER_SERV_MP = DbCommandConfiguration.Create("SIACU_SP_QUERY_INTER_SERV_MP");
        public static readonly IDbCommandConfig SIACU_SP_CON_PLAN = DbCommandConfiguration.Create("SIACU_SP_CON_PLAN");
        public static readonly IDbCommandConfig SIACU_SP_CON_PLAN_SERVICIO = DbCommandConfiguration.Create("SIACU_SP_CON_PLAN_SERVICIO");
        public static readonly IDbCommandConfig SIACU_P_LISTA_OPERADOR = DbCommandConfiguration.Create("SIACU_P_LISTA_OPERADOR");
        public static readonly IDbCommandConfig SIACU_TFUN015_ESTADO_SERVICIO = DbCommandConfiguration.Create("SIACU_TFUN015_ESTADO_SERVICIO");
        public static readonly IDbCommandConfig SIACU_SGASS_DET_EQUIPO = DbCommandConfiguration.Create("SIACU_SGASS_DET_EQUIPO");
        public static readonly IDbCommandConfig SIACU_SGASS_VAL_ORDEN_VISIT_CP = DbCommandConfiguration.Create("SIACU_SGASS_VAL_ORDEN_VISIT_CP");
        public static readonly IDbCommandConfig SIACU_P_CONSULTA_HUB = DbCommandConfiguration.Create("SIACU_P_CONSULTA_HUB");
        public static readonly IDbCommandConfig SIACU_SGASS_VAL_VEL_HFC = DbCommandConfiguration.Create("SIACU_SGASS_VAL_VEL_HFC");
        public static readonly IDbCommandConfig SIACU_SIACSS_LISTAEXTRANJERO = DbCommandConfiguration.Create("SIACU_SIACSS_LISTAEXTRANJERO");
        public static readonly IDbCommandConfig SIACU_SIACSS_LISTAEQUIPOREGISTRADO = DbCommandConfiguration.Create("SIACU_SIACSS_LISTAEQUIPOREGISTRADO");
        public static readonly IDbCommandConfig SIACU_SIACSI_REG_DESVIN_EQUIPO_MOV = DbCommandConfiguration.Create("SIACU_SIACSI_REG_DESVIN_EQUIPO_MOV");
        public static readonly IDbCommandConfig SIACU_SECSS_CON_PARAMETRO_GP = DbCommandConfiguration.Create("SIACU_SECSS_CON_PARAMETRO_GP");

        public static readonly IDbCommandConfig GET_ID_TRAZABILIDAD_BIOMETRIA = DbCommandConfiguration.Create("GET_ID_TRAZABILIDAD_BIOMETRIA");
        
        #endregion

        public static readonly IDbCommandConfig SIACU_HFCPOST_SP_BORRAR_PROGRAMACION = Create("SIACU_HFCPOST_BORRAR_PROGRAMACION");
        public static readonly IDbCommandConfig SIACU_HFCPOST_SP_ACTUALIZA_PROGRAMACION = Create("SIACU_HFCPOST_ACTUALIZA_PROGRAMACION");

        public static readonly IDbCommandConfig SIACSS_SP_PREGUNTAS_SEGURIDAD = Create("SIACSS_PREG_GENERICO");
        public static readonly IDbCommandConfig SIACSS_SP_RESPUESTAS_SEGURIDAD = Create("SIACSS_RESP_GENERICO");
        public static readonly IDbCommandConfig PVU_SP_GET_DATOS_OFICINA = DbCommandConfiguration.Create("GET_DATOS_OFICINA");


        //INICIATIVA-794
        public static readonly IDbCommandConfig SIACU_SP_CONSULTA_SERVICIO_IPTV = DbCommandConfiguration.Create("SIACU_SP_CONSULTA_SERVICIO_IPTV");
        public static readonly IDbCommandConfig SIACU_SP_VALIDA_SERVICIO_IPTV = DbCommandConfiguration.Create("SIACU_SP_VALIDA_SERVICIO_IPTV");
        

#region ChangeEquipment
        public static readonly IDbCommandConfig SIACU_SP_GET_SOT_MTTO = Create("SIACU_SP_GET_SOT_MTTO");
        public static readonly IDbCommandConfig SIACU_SP_VAL_EQUIPO = Create("SIACU_SP_VAL_EQUIPO");
        public static readonly IDbCommandConfig SIACU_SP_VAL_ICCID_CONTR = Create("SIACU_SP_VAL_ICCID_CONTR");
        public static readonly IDbCommandConfig SIACU_USP_VALIDA_ICCID_DISP = Create("SIACU_USP_VALIDA_ICCID_DISP");
        public static readonly IDbCommandConfig SIACU_SP_BSCSU_UPD_HLCODE = Create("SIACU_SP_BSCSU_UPD_HLCODE");
        #endregion

        #region PlanMigration
        public static readonly IDbCommandConfig SIACU_POST_SP_OBTENER_CAMPANA = DbCommandConfiguration.Create("SIACU_OBTENER_CAMPANA");
        public static readonly IDbCommandConfig SIACU_SP_CON_PLAN_CAMPANA = DbCommandConfiguration.Create("SIACU_SP_CON_PLAN_CAMPANA");
        public static readonly IDbCommandConfig SIACU_SGAFUN_GET_VEL_LTE = DbCommandConfiguration.Create("SIACU_SGAFUN_GET_VEL_LTE");
        public static readonly IDbCommandConfig SIACU_POST_SP_OBTENER_SERVICIO = DbCommandConfiguration.Create("SIACU_POST_SP_OBTENER_SERVICIO");
        public static readonly IDbCommandConfig SIACU_SGASS_SP_INSERTAR_SERVICIO = DbCommandConfiguration.Create("SIACU_SGASS_SP_INSERTAR_SERVICIO");
        public static readonly IDbCommandConfig SIACU_OPERATION_DET_EQUIPO_LTE = DbCommandConfiguration.Create("SIACU_OPERATION_DET_EQUIPO_LTE");
        public static readonly IDbCommandConfig SIACU_SP_CON_PLAN_SERVICIO_LTE = DbCommandConfiguration.Create("SIACU_SP_CON_PLAN_SERVICIO_LTE");
        public static readonly IDbCommandConfig SIACU_SP_CONSULTA_PDV_USUARIO = DbCommandConfiguration.Create("SIACU_SP_CONSULTA_PDV_USUARIO");

        #endregion
        public static readonly IDbCommandConfig SIACU_IDD_SGA_GET_MONTO_OCC_LTE = DbCommandConfiguration.Create("SIACU_SGAFUN_OBT_MONTO_OCC");

        public static readonly IDbCommandConfig SIACU_CONSULTA_REGLAS_AGENDAMIENTO = DbCommandConfiguration.Create("SIACU_CONSULTA_REGLAS_AGENDAMIENTO");

        public static readonly IDbCommandConfig SIACU_SGASS_CONSULTA_SUBTIPORD = DbCommandConfiguration.Create("SIACU_SGASS_CONSULTA_SUBTIPORD");
        public static readonly IDbCommandConfig SIACU_VALIDACION_SUBTIPO_TRABAJO = DbCommandConfiguration.Create("SIACU_VALIDACION_SUBTIPO_TRABAJO");
        public static readonly IDbCommandConfig SIACU_INSERT_PARM_VTA_PVTA_ADC = DbCommandConfiguration.Create("SIACU_INSERT_PARM_VTA_PVTA_ADC");
        public static readonly IDbCommandConfig SIACU_UPDATE_PARM_VTA_PVTA_ADC = DbCommandConfiguration.Create("SIACU_UPDATE_PARM_VTA_PVTA_ADC");
        
        public static readonly IDbCommandConfig SIACU_HISTORIAL_RESERVA_TOA = DbCommandConfiguration.Create("SIACU_HISTORIAL_RESERVA_TOA");
        public static readonly IDbCommandConfig SIACU_UPDATE_RESERVA_TOA = DbCommandConfiguration.Create("SIACU_UPDATE_RESERVA_TOA");
        public static readonly IDbCommandConfig SIACU_GET_FLAG_RESERVA_TOA = DbCommandConfiguration.Create("SIACU_GET_FLAG_RESERVA_TOA");

        public static readonly IDbCommandConfig SIACU_SP_LISTA_SERVICIOS_USADOS_HFC = DbCommandConfiguration.Create("SIACU_SP_LISTA_SERVICIOS_USADOS_HFC");
        public static readonly IDbCommandConfig SIACU_SP_LISTA_EQUIPOS_DTH = DbCommandConfiguration.Create("SIACU_SP_LISTA_EQUIPOS_DTH");
        public static readonly IDbCommandConfig SIACU_SP_DTH_P_CONSULT_TIPTRA = DbCommandConfiguration.Create("SGASS_CONSULTA_TIPTRA_DTH");
        #region CambioDatos
        public static readonly IDbCommandConfig SIACU_SP_PARAMETRICO = DbCommandConfiguration.Create("SIACU_SP_PARAMETRICO");
        public static readonly IDbCommandConfig SIACU_SP_CAMBIO_DATOS = DbCommandConfiguration.Create("SIACU_SP_CAMBIO_DATOS");
        public static readonly IDbCommandConfig SIACU_BSCS_SP_CONSULTAS_MARITAL_STATUS = DbCommandConfiguration.Create("SIACU_BSCS_SP_CONSULTAS_MARITAL_STATUS");
        public static readonly IDbCommandConfig SIACU_POST_BSCS_SP_DATOS_CLIENTE = DbCommandConfiguration.Create("SIACU_POST_BSCS_SP_DATOS_CLIENTE");
        #endregion


        public static readonly IDbCommandConfig SIACU_SP_OBTIENE_LISTAS_CBIO = DbCommandConfiguration.Create("SIACU_SP_OBTIENE_LISTAS_CBIO");

        public static readonly IDbCommandConfig SIACU_POST_BSCSSS_OBT_DATOS = DbCommandConfiguration.Create("SIACU_POST_BSCSSS_OBT_DATOS"); //PROY-14062 - REINICIO
        #region [Fields]

        private string m_name;

        #endregion

        #region [ Properties ]

        #region Name

        public string Name
        {
            get
            {
                return this.m_name;
            }
        }

        #endregion

        #endregion

        #region SetName

        private void SetName(string name)
        {
            this.m_name = name;
        }

        #endregion

        private static IDbCommandConfig Create(string name)
        {
            DbCommandConfiguration helper = new DbCommandConfiguration();

            helper.SetName(name);

            return helper;
        }

        
    }
}
