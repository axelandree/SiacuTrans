using System;

namespace Claro.SIACU.Transac.Service
{
    public class Constants
    {
        // constantes usadas en iteraccion
        public const string constCeroCeroCero = "000";
        // constante para enviar email
        public const string Message_OK = "OK";
        public const string gstrVariableSI = "SI";

        //
        public static string MessageNotServicesLimitWait = "No se ha podido completar la operación debido a que el servicio web ha superado el tiempo límite de espera.";
        public static string MessageNotComunicationServerRemote = "No se ha podido establecer comunicación con el servidor remoto.";
        public static string MessageConstancyOne = "Se informó al cliente sobre los prorrateos que se realizarán en el cargo fijo del servicio que ha solicitado Activar/Desactivar. El cliente acepta esta condición para realizar la Activación/Desactivación de Servicios. ";
        public static string MessageConstancyTwo= "Por la presente doy fé que los datos personales antes consignados son verdaderos y autorizo a América Móvil Perú SAC, que en caso alguno de los datos proporcionados no sean válidos o no coincidan con los registros oficiales, se dé por inválida esta solicitud.";


        public static string strRed = "RED";
        public static string strWhite = "WHITE";
        public static string Letter_A = "A";
        public static string Letter_B = "B";
        public static string Letter_C = "C";
        public static string Letter_E = "E";
        public static string Letter_G = "G";
        public static string Letter_I = "I";
        public static string Letter_T = "T";
        public static string Letter_F = "F";
        public static string Letter_L = "L";
        public static string Letter_R = "R";
        public static string Variable_SI = "SÍ";
        public static string Variable_NO = "NO";
        public static string Variable_True = "TRUE";
        public static string Variable_False = "FALSE";
        public static string NameApplication = "SIACUNICO";
        public static string NameExcel = "EXCEL";
        public static string NameListbags = "ListaBolsa";
        public static string NameListStateLine = "ListaEstadoLinea";
        public static string DescriptionX2 = "{0} - {1}";
        public static string GstrBusiness = "BUSINESS";

        //public static string SiacuDataXML = "SiacuDat.xml";
        //public static string SiacuDataPrepaidXML = "SiacuDataPrepaid.xml";
        public static string SiacutDataXML = "SiacutData.xml";
        public static string SiacutDataPrepaidXML = "SiacutDataPrepaid.xml";
        public static string SiacutDataPrepaidWSXML  = "SiacutDataPrepaidWS.xml";
        public static string FileSiacutData = "DataTransac";

        //Key
        public static string Key_RestrictedPlansConsultation = "CadenaPlanesRestringidosParaConsulta";
        public static string Key_LastMonthDBTO= "UltimosMesesDBTO";
        public static string Key_ConfCallDetail = "ConfDetalleLlamada";
        public static string Key_LastMonthDBTOTOPE = "UltimosMesesDBTOTOPE";

        //DataTransac
        public static string Message_ErrorGeneral = "Error : {0}";
        public static string Message_ErrorGetIpClient_LogTrx = "Llamada_Telefono {0} \n Error en el RegistroLogTrx. IpCliente: {1}";
        public static string Message_TypificationNotRecognized = "No se reconoce la tipificación de esta transacción.";
        public static string Message_TypificationNotCharged = "No se cargaron los datos de la tipificación.";
        public static string Message_DataNotCharged = "No se cargaron los datos correctamente.";
        public static string Message_FileNotExist = "La ruta del archivo no existe: \n{0}";
        public static string Message_ErrorGetDeadlines = "Error al obtener las fechas limites.";

        public static string Action_Search = "Buscar";
        public static string Action_Export = "Exportar";
        public static string Action_Print = "Imprimir";
        public static string Action_Save = "Grabar";
        public static string StatusCode_OK = "OK";
        public static string StatusCode_Error = "Error";

        public static string GstrTransactionCallDetail = "TRANSACCION_DETALLE_LLAMADAS";

        public static string DateFormat_DateESP = "dd/MM/yyyy";
        public static string DateFormat_DatetimeESP = "dd/MM/yyyy hh:mm:ss";
        public static string DateFormat_DateENU = "MM/dd/yyyy";
        public static string DateFormat_DateENU2 = "yyyy-MM-dd";

        public static string Option_CAC = "LISTACAC";
        public static string Option_TypeDocId = "LISTATIPODOCUIDENTIDAD";
        public static string Option_CivilStatus = "LISTAESTADOCIVIL";
        public static string Option_CAS16 = "LISTACAS16";
        public static string Option_Occupation = "LISTAOCUPACION";
        public static string Option_RegistrationReason = "LISTAMOTIVOREGISTRO";
        public static string Option_Brand = "LISTAMARCA";
        public static string Option_Model = "LISTAMODELO";
        public static string Option_Departamento = "LISTADEPARTAMENTOS";
        public static string Option_Provincia = "LISTAPROVINCIA";
        public static string Option_Distrito = "LISTADISTRITO";
        public static string Option_BirthPlace = "LISTALUGARNACIMIENTO";
        public static string Option_ServiceVAS = "LISTASERVICIOVAS";

        public static string Bag_Multicast = "Bolsa Multidestino";
        public static string Bag_NumbersFrequentTFI = "Bolsa Nros. Frec. TFI";
        public static string Bag_FreeMinutes = "Bolsa Min Gratis";
        
        public static string Balance_SMS = "Saldo SMS";
        public static string Balance_MMS = "Saldo MMS";
        public static string Balance_PromoSoles = "Saldo S/. Promo";
        public static string Balance_LoyaltySMS = "Saldo SMS Fidelizacion";
        public static string Balance_LoyaltyVoice = "Saldo Voz Fidelizacion";

        public static string CodeNumberTriado = "Nro Triado {0}";

        public static string FrequentlyNumbers_SMS = "SMS Num. Frec.";
        public static string FrequentlyNumbers_SEG = "SEG Num. Frec.";

        public static string GPRS_KB = "GPRS(KB)";

        public static string Promo1_Soles = "Promo 1 (Soles)";
        public static string Promo2_Soles = "Promo 2 (Soles)"; 

        public static string Voice1_PromoAccount = "Voice1 Promo. account";
        public static string Voice2_PromoAccount = "Voice2 Promo. account";

        public static string StrTipoLinea_PREPAGO = "PREPAGO";
        

        #region Retencion/Cancelación

        public const string gstrTransaccionHFCRetCanServicio = "TRANSACCION_CANCEL_LINEA_HFC";
        public const string gstrNoRetenido = "NR";
        public const string gstrSaveTransaccionNoRetenido = "No se pudo Programar la Cancelación de la Línea";
        public const string gConstMsgNoSePProCanLi = "No se pudo Programar la Cancelación de la Línea";

        #endregion


        #region detalle llamadas
        public const string gstrTransaccionLTEDetLlamFac = "TRANSACCION_DETALLE_LLAMADAS_LTE";
        #endregion

        public static String RECIBO8 = "20070628";
        public static String RECIBO9 = "20130111";
        public static string gstrVariableS = "S";
        public static string gstrVariableN = "N";

        public static string mensajeError = "Error";
        public static string DETALLE_LLAMADAS = "DETALLE_LLAMADAS";
        public static string DETALLE_SMS = "DETALLE_SMS";
        public static string DETALLE_LLAMADAS_CONSUMO = "DETALLE_LLAMADAS_CONSUMO";

        public static string strMenosCuatro = "-4";
        public static string strMenosCinco = "-5";
        public static string strMenosTres = "-3";
        public static string strMenosDos = "-2";
        public static string strMenosUno = "-1";
        public static string strCero = "0";
        public static string strDobleCero = "00";
        public static string strUno = "1";
        public static string strDos = "2";
        public static string strTres = "3";
        public static string strCuatro = "4";
        public static string strVeinte = "20";
        public static string strCincuentaYUno = "51";
        public static string strTresNegativo = "-3";
        public static int numeroCero = 0;
        public static int numeroUno = 1;
        public static int numeroDos = 2;
        public static int numeroTres = 3;
        public static int numeroCuatro = 4;
        public static int numeroCinco = 5;
        public static int numeroSeis = 6;
        public static int numeroDiez = 10;
        public static int numeroCatorce = 14;
        public static int numeroQuince = 15;
        public static int numeroDieciocho = 18;
        public static int numeroDiecinueve = 19;
        public static int numeroVeinte = 20;
        public static string numeroCeroConMichi = "#0.00";
        public static string strLetraN = "N";

        #region  PROY-140245-IDEA140240
        public static string strConstVacio = "";
        public static string strConstP = "P";
        #endregion
        
        public static string blcasosVariableSI = "SI";
        public static string blcasosVariableSImin = "Si";
        public static string blcasosVariableNO = "NO";
        public const string grstFalse = "false";
        public const string grstTrue = "true";
        public const string GstrTransaccionCambioTipoCliente = "TRANSACCION_CAMBIO_TIPO_CLIENTE";
        public const string ConstExportar = "Exportar";
        public const string ConstImprimir = "Imprimir";
        //Tope Consumo
        public static string strVariableNOmin = "No";
        public static string strVariableActivacion = "Activación";
        public static string strVariableDesactivacion = "Desactivación";

        public static string DAReclamDatosVariableNO_OK = "NO OK";
        public static string DAReclamDatosVariableNoOk = "NO_OK";

        public static string DAReclamDatosVariable_OK = "OK";
        public static int zero = 0;
        public static string Value = "True";
        public static string NoteReservationToa = " ~";
        public static string IdentifiacdorToa = "~";

        public struct ADDITIONALSERVICESHFC
        {
            public static string gstrTransaccionDTHTACTDESSER = "TRANSACCION_ACT_DES_SERVICIOS_HFC";
            public static string gstrVariableSI = "SI";
            public static string gstrVariableNO = "NO";
            public static string gstrServCANAL  = "CANAL";
            public static string gstrServVOD = "VOD";
            public static string gstrTransaccionSusReactTemp= "TRANSACCION_SUSP_REACT_TEMP_HFC";
        }
        public struct ADDITIONALSERVICESPOSTPAID
        {
            public static string strSystemSIACPO = "SIACPO";
            public static string strTransactionACTDESSER = "TRANSACCION_ACT_DES_SER";
            public static string gstrTransactionDTHTACTDESSER = "TRANSACCION_DTH_ACT_DES_SER";
            public static string strNotTypification = "No se reconoce la tipificación de esta transacción.";
            public static string gstrActiveDetermined = "AD";
            public static string gstrActiveService = "A";
            public static string gstrDeactivationService = "D";
            public static string gstrMaintainService = "M";
            public static string gstrDesactiveDetermined = "DD";
            public static string gstrActiveIndetermined = "AI";
            public static string gstrActiveServiceConst = "Activación";
            public static string gstrDesactivactionServiceConst = "Desactivación";
            public static string gstrTerm = "Plazo";
            public static string gstrMantActivationServiceConst = "Mantener Activación";
            public static string NameTransactionActDesactServAdicionalesPostpago = "Activación y Desactivación de Servicios Adicionales Postpago";
            public static string NameTransactionIncomingCallDetailPostpaid = "Detalle de Llamadas Entrantes Postpago";
            public static string gstrExistProg = "Existe Programación";

        }
        public struct DETALLE_CARGO_FIJO
        {
            public static int consulta_descuento_cargo_fijo = 0;
            public static int consulta_cargo_fijo_timpro = 1;
            public static int consulta_cargo_fijo_timpro_1 = 3;
            public static int consulta_cargo_fijo_timpro_2 = 4;
            public static int consulta_cargo_fijo_timmax = 2;
            public static int consulta_cargo_fijo_timmax_2 = 5;
            public static int consulta_cargo_fijo_timpro_bolsa = 1;
            public static int consulta_cargo_fijo_timpro_bolsa_1 = 2;
            public static int consulta_cargo_fijo_timpro_bolsa_2 = 3;
            public static int consulta_cargo_fijo_timpro_bolsa_3 = 4;

        }

        // DETALLE TRAFICO LOCAL ADICIONAL y CONSUMO
        public struct DETALLE_TRAFICO_LOCAL_GENERAL
        {
            public static int CONSULTA_TRAFICO_LOCAL_ADICIONAL_TIMpro = 1;
            public static int CONSULTA_TRAFICO_LOCAL_ADICIONAL_TIMmax = 2;
            public static int CONSULTA_TRAFICO_LOCAL_A_CONSUMO_TIMpro = 3;
            public static int CONSULTA_TRAFICO_LOCAL_A_CONSUMO_TIMmax = 4;
        }


        // CAMBIO DE PLAN

        public struct CAMBIO_DE_PLAN {
            public static string TipoServicio_Internet = "INT";
            public static string TipoServicio_Telefonia = "TLF";
            public static string TipoServicio_Cable = "CTV";
        }


        // DETALLE LARGA DISTANCIA NACIONAL
        public struct DETALLE_LARGA_DISTANCIA
        {
            public static int CONSULTA_LARGA_DISTANCIA_NACIONAL = 20;
            public static int CONSULTA_LARGA_DISTANCIA_INTERNACIONAL = 30;
        }

        // DETALLE OTROS CARGOS Y ABONOS
        public struct DETALLE_OTROS_CARGOS
        {
            public static int CONSULTA_OTROS_CARGOS_Y_ABONOS = 1;
            public static int CONSULTA_OTROS_CARGOS_NO_IGV = 2;
            public static int CONSULTA_COBRANZAS_DIFERIDAS = 3;
        }

        // DETALLE LLAMADAS INTERNET
        public struct DETALLE_LLAMADAS_INTERNET
        {
            public static int CONSULTA_LLAMADAS_INTERNET = 11;
        }
        public struct DETALLE_LLAMADAS_SALIENTE
        {
            public static string LteBuscar = "TRANSACCION_DETALLE_SALIENTES_LTE"; //Buscar
            public static string LteSave = "TRANSACCION_DETALLE_LLAMADAS_LTE"; //Guardar

            public static string HfcBuscar = "TRANSACCION_DETALLE_LLAMADA_SALIENTE_HFC_BUSCAR";//"TRANSACCION_DETALLE_LLAMADAS_HFC_BUSCAR"; //Buscar
            public static string HfcSave = "TRANSACCION_DETALLE_LLAMADAS_HFC_GUARDAR"; //Guardar

















            //public static string HfcBuscar = "TRANSACCION_DETALLE_SALIENTES_LTE"; //Buscar
            //public static string HfcSave = "TRANSACCION_DETALLE_SALIENTES_LTE"; //Buscar
        }

        public struct DETALLE_LLAMADAS_NO_SALIENTE
        {
            public static string HfcBuscar = "TRANSACCION_DETALLE_LLAMADASNF_HFC"; //Buscar
            public static string LteBuscar = "TRANSACCION_DETALLE_LLAMADASNF_LTE"; //Buscar

        }
        // DETALLE LLAMADA SALIENTE FACTURADA
        public static int AgStrTransAuditFechProg = 12;
        public static int ConfDetailCall = 1;
        public static int LastMonthsDBTOTOPE = 200703;
        public struct DETALLE_TRAFICO_LOCAL
        {
            public static String RTP_OnNet = "RPTOnNet".ToUpper();
            public static String RTP = "RTP".ToUpper();
            public static String OnNet = "OnNet".ToUpper();
            public static String OffNetFijo = "OffNetFijo".ToUpper();
            public static String OffNetCelular = "OffNetCelular".ToUpper();
            public static String OffNet = "OffNet".ToUpper();
            public static String RTP_OnNet_OffNet = "RTP_OnNet_OffNet".ToUpper();
            public static String OnNet_OffNet = "OnNet_OffNet".ToUpper();
            public static String COMPLETO = "TODO".ToUpper();
        }


        public struct BilledOutCallPostpaid
        {
            public static string NameModule = "Detalle LLamadas Saliente Facturada";
            public static string Message_RestrictedInformation = "No es posible consultar el Detalle de Llamadas: Información restringida.";
            public static string Message_TransactionNotEnabledForPlan = "Esta Transacción no está habilitada para este tipo de Plan!.";
            public static string Message_GeneralDataNotChargeForLine = "No se cargó datos generales de la linea, no se podra continuar con la transacción.";
            public static string Message_ErrorCreatingInteraction = "Error al crear interacción en clarify: {0}";
            public static string Message_InfoCallsDetailOfTheMonth = "Información - Consulta Detalle de Llamadas del Mes \"{0}\" , Año \"{1}\"";
            public static string Message_SendMailSuccessful = "En unos minutos se estará enviando al correo indicado el detalle de las llamadas del mes elegido.";
            public static string Message_SendMailUnsuccessful = "Error al enviar correo.";
            public static string Message_DateRange = "Del {0} al {1}.";
            public static string Message_IncorrectParameters = "Se han enviado parámetros incorrectos a la página.";
            public static string Message_NullParameters = "Se han enviado parámetros nulos a la página.";
            
        }

        public struct Notes_OutgoingCallsNBP
        {
            public static string SuccessfulConsultation_CallsDetailNB = "Consulta de Detalle de LLamadas No Facturadas retornó registros";
            public static string UnsuccessfulConsultation_CallsDetailNB = "Consulta de Detalle de LLamadas No Facturadas NO retorno registros";
            public static string NameTransaction = "DETALLE_LLAMADA_NO_FACTURADA";

            public static string NameColumnTelephone = "Telefono";
            public static string NameColumnStartDate = "FechaInicio";
            public static string NameColumnEndDate = "FechaFin";
            public static string NameColumnInvoiceNumber = "InvoiceNumber";
            public static string NameColumnTypeTransaction = "Tipo Transaccion";

            public static string Message_RestrictedInformation = "No es posible consultar el Detalle de Llamadas: Información restringida."; 
            public static string Message_TotalRegistration = "Total Registro : {0}";
            public static string Message_ErrorWCF = "Error WS:  {0}";
            public static string ParametersIN_LogTrx = "TelfConsulta={0}; CoId={1}; FechaINI={2};FechaFin={3}";
            public static string Description_SaveInteractionClarify = "(Grabar Interacción Clarify)";
            public static string NotesInteractionData = "Información - Consulta Detalle de Llamadas Interno desde \"{0}\" hasta \"{1}\"";
        }

        public struct Notes_IncomingCallsPrepaid
        {
            public static string NameModule = "DETALLE_LLAMADA_ENTRANTES";
            public static string NotesInteractionData = "Detalle de Llamadas Entrantes desde  \"{0}\" hasta \"{1}\" -- {2} - {3}";
            public static string NotesNumberInteraction = "El Nro de interacción es : {0}";
            public static string ParametersIN_LogTrx = "TelfConsulta={0}; FechaINI={1};FechaFin={2}";
            public static string SubscriberStatusDefault = "0000000000";
            public static string Code_SMSIlimitado = "SMS Ilimitado";
            public static string KeyXML_MessageErrorTypifTransact = "MensajeErrorTipificacionTransaccion";
            public static string Key_AmountOCC = "MontoDetLlamEntHFC";//"gAmountIncomingCallDetail";

            public static string Message_NotLoadedDataLine = "No se cargó datos generales de la linea, no se podra continuar con la transacción.";
            public static string Message_ErrorCreatingInteraction = "Error al crear interacción en clarify: {0}";
            public static string Message_ErrorGenerateRecharge = "Ocurrió Error al Generar Recarga: {0}";
            public static string Message_ErrorInsercionInteraccion = "{0}\n Por el siguiente error : {1}\n{2}";
            public static string Message_InsufficientBalance = "No se puede realizar la recarga, debido a que no tiene suficiente saldo.";
            public static string Message_UnableToRecharge = "No se puede realizar la recarga.";
            public static string Message_TheChargeIsCompleted = "Se realizo el cobro en : {0}.";
            public static string Message_DescriptionCharge = "Se realizó el cobro de S/. {0} (Incl. IGV).";
        }

        public struct Notes_AdditionalServicesLte
        {
            public static string StrTranstionActDesacServLte = "TRANSACCION_ACT_DES_SERVICIOS_LTE";
        }
        //Migracion Plan PostPago
        public struct Plans_Migrations
        {
            public static string gConstIlimitado = "ILIMITADO";
        }
        // Constantes consulta de servicios
        public static string ConsanteActivo = "Activo";
        public static string ConsanteDesActivo = "Desactivo";
        public static string strLetraA = "A";
        public static string strLetraB = "B";
        public static string strLetraD = "D";
        public static string strLetraF = "F";
        public static string strLetraP = "P";
        public static string strLetraC = "C";
        public static string strLetraR = "R";
        public static string strLetraH = "H";
        public static string strLetraS = "S";
        public static string strLetraI = "I";

        // Activación / Desactivación 
        public static string strDatosReg = "seachange|210|bsghandle|vodprofile";
        //cambio numero
        public static string strTipo_Serv = "TELEFONIA";
        public static string strNombServ = "Servicio VoD";
        public static string strServVOD = "VOD";


        //Afiliacion a comprobantes electronicos
        public static string strNotieneEmail = "El cliente no tiene registrado un correo electrónico o e-mail";
        public static string strTipoLinea_DTH_POST = "DTH POST";
        public static string strTipoLinea_POSTPAGO = "POSTPAGO";
        public static string strTipoLinea_FIJO_POST = "FIJO POST";
        public static string strTipoLinea_TPI = "TPI";
        public static string CriterioMensajeOK = "OK";

        public static string strCeroUno = "01";
        public static string strCeroSeis = "06";

        public static string strLetraT = "T";

        /* Constantes para nuevo HLR - UDB */
        public const string lista_ZMIO = "ZMIO";
        public const string lista_ZMNO = "ZMNO";
        public const string lista_ZMGO = "ZMGO";
        public const string lista_ZMSO = "ZMSO";
        public const string lista_ZMQO = "ZMQO";
        public const string lista_ZMQO_IN = "IN/ZMQO";
        public const string lista_ZMQO_CA = "CA/ZMQO";
        public const string lista_ZMAO = "ZMAO";
        public const string lista_ZMNF = "ZMNF";
        public const string lista_ZMNI = "ZMNI";

        public const string par_HLR = "HLR";
        public const string par_MSIDN = "MSIDN";
        public const string par_IMSI = "IMSI";
        public const string par_ARC = "ARC";
        public const string par_RC = "RC";
        public const string par_CC = "CC";
        public const string par_MSC = "MSC2";
        public const string par_VLR = "VLR";
        public const string par_SAOM = "SAOM";
        public const string par_ACT_STATUS = "AS";

        public const string par_PSW = "PSW";
        public const string par_BSC = "BSC";
        public const string par_CW = "CW";
        public const string par_HOLD = "HOLD";
        public const string par_CLIP = "CLIP";
        public const string par_CLIR = "CLIR";
        public const string par_MPTY = "MPTY";
        public const string par_BAPRE = "BAPRE";
        public const string par_BOS3 = "BOS3";
        public const string par_BOS4 = "BOS4";
        public const string par_USSD = "USSD";
        public const string par_EPS_STATUS = "ES";
        public const string par_NETWORK_ACCESS = "NA";
        public const string par_CA = "CA";
        public const string par_MSIN = "MSIN";
        public const string par_ACU_IDENTITY = "AI";
        public const string par_BAOC = "BAOC";
        public const string par_BOIC = "BOIC";
        public const string par_BAIC = "BAIC";
        public const string par_BIRO = "BIRO";
        public const string par_BOIH = "BOIH";
        public const string par_BORO = "BORO";
        public const string par_CFU = "CFU";
        public const string par_CFB = "CFB";
        public const string par_CFNR = "CFNR";
        public const string par_CFNA = "CFNA";
        public const string par_OCCF = "OCCF";
        public const string par_TIME = "TIME";

        public const string sublista_T11 = "T11";
        public const string sublista_B17 = "B17";
        public const string sublista_T21 = "T21";
        public const string sublista_T22 = "T22";

        public const string par_DP = "DP";
        public const string par_SCP = "SCP";
        public const string par_DEFCH = "DEFCH";
        public const string par_SSET = "SSET";
        public const string par_DEFSMS = "DEFSMS";
        public const string par_DEFSH = "DEFSH";
        public const string par_DNT = "DNT";


        public const string par_QOSP = "QOSP";
        public const string par_APN = "APN";
        public const string par_PDP_TYPE = "PT";
        public const string par_PDP_ADDRESS = "PA";
        public const string par_SA = "SA";

        public const string par_OCSI = "OCSI";
        public const string par_MOSMSCSI = "MOSMSCSI";
        public const string par_GGCSI = "GGCSI";
        public const string par_VO = "VO";

        public static string strLetraY = "Y";
        public const string val_PCBN = "PCBN";
        public const string val_PREP = "PREP";
        public const string val_BOTH = "BOTH";
        public const string val_reg1 = "@REG01";
        public const string val_reg2 = "@REG02";
        public const string val_reg3 = "@REG03";

        public struct PresentationLayer
        {
            //Registros de Auditoría en la Consulta de Clientes
            public const string gstrTransaccionBusquedaTelefono = "BTELEFONO";
            public const string gstrTransaccionBusquedaCuenta = "BCUENTA";
            public const string gstrTransaccionBusquedaNombre = "BNOMBRE";
            public const string gstrTransaccionBusquedaRazonSocial = "BRAZONSOCIAL";
            public const string gstrTransaccionBusquedaNroDctoIdentidad = "BNRODCTOIDENTIDAD";

            public const string CriterioOrdenamientoASC = "ASC";
            public const string CriterioOrdenamientoDESC = "DESC";
            public const string NumeracionMENOSUNO = "-1";
            public const string NumeracionCERO = "0";
            public const string NumeracionUNO = "1";
            public const string NumeracionDOS = "2";
            public const string NumeracionTRES = "3";
            public const string NumeracionCUATRO = "4";
            public const string NumeracionCINCO = "5";
            public const string NumeracionSEIS = "6";
            public const string NumeracionMenosNueve = "-9";
            public const string NumeracionDIEZ = "10";
            public const string NumeracionDOCE = "12";
            public const string ControladoDesviadoOpc = "CFB&CFNA&CFNR";


            public const string Numeracion99 = "99";
            public const string CriterioMensajeOK = "OK";
            public const string CriterioMensajeNOOK = "NO_OK";
            public const string gstrVariableSI = "SI";
            public const string gstrVariableSIminus = "Si";
            public const string gstrVariableNOminus = "No";
            public const string gstrVariableNO = "NO";
            public const string gstrOkClose = "OK_CLOSE";
            public const string gstrEspacio = " ";

            public const string gstrVariableSIAbreviado = "S";
            public const string gstrVariableSNAbreviado = "S/N";
            public const string gstrVariableNOAbreviado = "N";
            public const string gstrValorPostpago = "POSTPAGO";
            public const string HCTNUMEROCPLANSOLOTFI = "HCTNUMEROCPLANSOLOTFI";
            public const string gstrValorFijoPost = "Fijo Post";
            public const string gstrTipoServDTH = "DTH POST";
            public const string gstrUndefined = "Indefinido";

            public const string gstrVariableT = "T";
            public const string gstrVariableG = "G";
            public const string gstrVariableA = "A";
            public const string gstrVariableB = "B";
            public const string gstrVariableC = "C";
            public const string gstrVariableMO = "MO";
            public const string gstrVariableD = "D";
            public const string gstrVariableP = "P";
            public const string gstrVariableX = "X";
            public const string gstrVariableL = "L";
            public const string gstrVariableE = "E";
            public const string gstrVariableM = "M";
            public const string gstrVariableV = "V";
            public const string gstrVariableN = "N";
            public const string gstrVariableF = "F";
            public const string gstrVariableH = "H";
            public const string gstrVariableI = "I";
            public const string gstrVariableR = "R";
            public const string gstrVariableRA = "RA";
            public const string gstrVariableS = "S";


            //INICIO TITULARIDAD
            public const string gstrVariableZZ = "ZZ";
            public const string gstrVariableLT = "LT";
            public const string gstrVariableFechaEmpty = "1/1/1";
            public const string NumeracionOCHO = "8";

            //FIN TITULARIDAD
            public const string gstrExtensionPDF = ".pdf";
            public const string gstrACTIVO = "ACTIVO";
            public const string gstrRecargas = "RECARGA";
            public const string gstrB2E = "B2E";
            public const string gstrConsumer = "Consumer";
            public const string gstrBusiness = "Business";
            public const string gstrBusinessUpper = "BUSINESS";

            public const string gstrDatosNoEncontrados = "NO_DATA_FOUND";
            public const string gstrNoExistenDatos = "No existen datos";
            public const string gstrAll = "All";
            public const string gstrTodos = "Todos";

            public const string kitracReclamo = "RECLAMO EN PRIMERA INSTANCIA";
            public const string kitracReporte = "REPORTE";
            public const int kitracDigitosCasoId = 10;
            public const int kitracVariableTreinta = 30;
            public const int kitracVariableCero = 0;
            public const int kitracVariableMenosUno = -1;
            public const int kitracVariableUno = 1;
            public const int kitracVariableDos = 2;
            public const int kitracVariableTres = 3;
            public const int kitracVariable21 = 21;
            public const int kitracVariableCuatro = 4;
            public const int kitracVariableCinco = 5;
            public const int kitracVariableVeinte = 20;
            public const string kitracNroReclamo = "NUMERO DE RECLAMO";
            public const string kitracNroReporte = "NUMERO DE REPORTE";
            public const string kitracCodCaso = "CODIGO CASO";
            public const string kitracDNI = "DNI";
            public const string kitracCarnetEx = "Carnet Extranjería";
            public const string kitracRUC = "R.U.C";
            public const string kitracKB = "KB";
            public const string kitracSoles = "S/.";
            public const string kitracUTF8 = "UTF-8";
            public const string kitracISO8859 = "ISO-8859-1";

            //SUBCASO
            public const string gstrPendiente = "PENDIENTE";
            public const string gstrCerrado = "CERRADO";
            public const string gstrTpi = "TPI";
            public const string gstrFijoPost = "FIJO POST";
            public const string gstrRecursoQueja = "RECURSO DE QUEJA";
            public const string gstrCeros = "000";
            public const string gstrVariableACT = "ACT";
            public const string gstrVariableOK = "OK";
            public const string gstrVariableEmpty = "";
            //FIN SUBCASO

            public const string CODIGO_SIN_NOMBRE = "zz";
            public const string APOCOPE_LOTE = "LT";

            //INTERACCIONES
            public const string gstrVariableINI = "INI";
            public const string gstrVariableDD = "DD";
            public const string gstrVariableBP = "BP";
            public const string gstrCrearMD = "CrearMD";

            public const string NumeracionCEROEDECIMAL = "0.0";
            public const string NumeracionCERODECIMAL2 = "0.00";
            public const string NumeracionMICHICERODECIMAL2 = "#0.00";
            public const string gstrVariableArroba = "@";
            public const string gstrVariablePipeline = "|";



            //FIN INTERACCIONES
            //Debito/Credito Manual y Recargas
            public const string gstrVariableSS = "SS";
            public const string gstrRellenoMes = "00";
            public const string gstrMesUNO = "01";
            //Fin Debito/Credito Manual y Recargas


            //DETALLE LLAMADAS FACTURADAS
            public const double kitracVariableCeroDouble = 0.0;
            public const string kitracVariableCeroDecimalString = "0.00";
            public const string gstrVariableParticular = "PARTICULAR";
            public const string gstrHoraCero = "00:00";
            public const string gstrDobleCeros = "00";
            public const string gstrLogVacio = "LOG_VACIO";
            public const string gstrDosPuntos = ":";
            public const string gstrComa = ",";
            public const string gstrGuion = "-";
            public const string gstrPuntoyComa = ";";
            public const string gstrNoPrecisado = "No Precisado";
            public const string gstrTelefono = "Telefono";
            public const string gstrCodContrato = "Código de Contrato";
            public const string gstrCuenta = "Cuenta";
            public const string gstrNroCuenta = "Nro de Cuenta";
            public const string gstrMonto = "Monto";
            public const string gstrConMonto = "Con Monto";
            public const string gstrSinMonto = "Sin Monto";
            public const string gstrCenter = "center";
            public const string gstrRight = "right";

            //FIN DETALLE LLAMADAS FACTURADAS

            //Constancia Reclamo
            public const string gstrVariableCRep = "Constancia de Reporte";
            public const string gstrVariableCRec = "Constancia de Reclamo";
            public const string gstrVariableCReconsi = "Constancia de Reconsideración";
            public const string gstrVariableCApe = "Constancia de Apelación";
            public const string gstrVariableCQue = "Constancia de Queja";
            public const string gstrNoInfo = "No se encontro Información";
            public const string gstrNroRep = "NUMERO DE REPORTE";
            public const string gstrNroRec = "NUMERO DE RECLAMO";
            public const string gstrArroba = "@";
            //FIN Constancia Reclamo

            //SUSPENSION RECONEXION SERVICIO
            public const int NUMERO155 = 155;
            public const int NUMERO15 = 15;
            public const int NUMERO62 = 62;
            //FIN SUSPENSION RECONEXION SERVICIO

            //CAMBIO TRASLADO NUMERO
            public const string gstrTraslado = "Traslado";
            public const string gstrCambio = "Cambio";
            public const string gstrContrato = "N° Contrato:";
            public const string gstrFechaHora = " Fecha y Hora:";
            //FIN CAMBIO TRASLADO NUMERO

            public const string gstrCallFacture = "LlamadasFacturadas";
            public struct DETALLE_TRAFICO_LOCAL
            {
                public const string RTP_OnNet = "RPTOnNet";
                public const string RTP = "RTP";
                public const string OnNet = "OnNet";
                public const string OffNetFijo = "OffNetFijo";
                public const string OffNetCelular = "OffNetCelular";
                public const string OffNet = "OffNet";
                public const string RTP_OnNet_OffNet = "RTP_OnNet_OffNet";
                public const string OnNet_OffNet = "OnNet_OffNet";
                public const string COMPLETO = "TODO";
            }

            public const string grstError = "Error";

            public struct TipoProduco
            {
                public const string LTE = "LTE";
                public const string HFC = "HFC";
                public const string Fixed = "FIJA";
                public const string TVSatelital = "TV SATELITAL";
                public const string DTH = "DTH";
            }
            public const string SelectItem = "--Seleccionar--";
            public const string ALL = "TODOS";
            public const string VIGENTS = "VIGENTES";
            public const string CORE = "CORE";
            public const string ADICIONAL = "ADICIONAL";
            public const string rowSeparator = ";";
            public const string fieldSeparator = " | ";
            public const string blankSpace = " ";
            public const string dateDefaultFormat = "ddMMyyyy";
            public const string dateDefaultFormat2 = "dd/MM/yyyy";

            public const string strTwoZeroEight = "208";
        }
    }

    public sealed class ConstantsSiacpo
    {

        public const string TextoContado = "Al contado";
        public const string TextoCargadoRecibo = "Cargado al recibo";


        static public readonly string TextoSeleccionar = "-Seleccionar-";
        static public readonly string TextoLibre = "<- LIBRES ->";
        static public readonly string TextoTodos = "-Todos-";

        static public readonly string ValorSeleccionar = "%%";
        static public readonly string ValorLibre = "$%";
        static public readonly string ValorTodos = "0";
        static public readonly string ValorNinguno = "0%";

        static public readonly string FormatoNumerico1 = "###,###,###,##0";
        static public readonly string FormatoNumerico2 = "###,###,###,##0.0";
        static public readonly string FormatoNumerico3 = "###,###,###,##0.00";

        static public readonly string FormatoFecha1 = "dd/MM/yyyy";

        static public readonly string FORMATOHORA1 = "HH:mm:ss";

        public const string AltaDatoValorTexto = "ALTA";
        public const string BajaDatoValorTexto = "BAJA";
        public const string AltaDatoValorValue = "A";
        public const string BajaDatoValorValue = "B";

        public const string SiDatoValorTexto = "SI";
        public const string NoDatoValorTexto = "NO";
        public const string SiDatoValorValue = "S";
        public const string NoDatoValorValue = "N";
        public const string ConstVacio = "";

        public const string OrderCriterioASC = "ASC";
        public const string OrderCriterioDESC = "DESC";

        public const string ConstMenosUno = "-1";
        public const string ConstCero = "0";
        public const string ConstCeroCero = "00";
        public const string ConstCeroCeroCero = "000";
        public const string ConstUno = "1";
        public const string ConstDos = "2";
        public const string ConstCinco = "5";
        public const string ConstSeis = "6";
        public const string ConstCincuentaYUno = "51";
        public const string ConstNueve = "9";
        public const string ConstDiez = "10";
        public const string ConstQuince = "15";
        public const string ConstDieciseis = "16";
        public const string ConstDiecisiete = "17";
        public const string ConstActivo = "Activo";
        public const string ConstE = "E";

        public const string ConstAdiTieneBloqRobo = "Adicionalmente tiene bloqueo por robo.";
        public const string ConstI = "I";
        public const string ConstConsultar = "Consultar";
        public const string ConstDuracion = "Duracion";
        public const string ConstExportar = "Exportar";
        public const string ConstFecha = "Fecha";
        public const string ConstGuardar = "Guardar";
        public const string ConstHora = "Hora";
        public const string ConstImprimir = "Imprimir";
        public const string ConstLinea = "línea ";

        public const string ConstMsgModifacaBolsaOk = "Modificación de bolsa satisfactorio";
        public const string ConstNoProdujoCambSIM = " no se produjo cambio de SIMCARD, ";

        public const string ConstCotizacionRechazada = "Rechazada";
        public const string ConstCotizacionAceptada = "Aceptada";
        public const string ConstAjuste = "Ajuste";

        public const string ConstNoRegisAudit = "No se pudo registrar la Auditoria";
        public const string ConstNro = "Nro.";
        public const string ConstNroServicio = "Nro. Servicio:";
        public const string ConstNumero = "Número";
        public const string ConstNumeroEntrante = "Número_Entrante";
        public const string ConstOK_CLOSE = "OK_CLOSE";
        public const string ConstTotalRegistros = "Total de Registros: ";

        public const string ConstOK = "OK";
        public const string ConstOtrosCargAbon = "Otros Cargos y Abonos";

        public const string ConstTransaccionRealizoExito = "La transacción se realizó con éxito.";
        public const string ConstTransExiCambioSIM = "Transacción Exitosa : Se realizó el cambio de SIM exitosamente pero debe hacer el desbloqueo manual de la línea.";

        public const string ConstCadenaTablaDetalle = "Cadena de la tabla de detalle :";
        public const string ConstCARGA = "CARGA";
        public const string ConstPARCIAL = "PARCIAL";
        public const string ConstCargoFijo = "Cargo Fijo";
        public const string ConstCargoTotal = "Cargo Total";
        public const string ConstClaroservicios = "Claroservicios";
        public const string ConstCliente = "Cliente: ";
        public const string ConstCliente1 = "Cliente :";
        public const string ConstCobraDiferidas = "Cobranzas Diferidas";
        public const string ConstCuandoGrabaCaso = " Cuando graba Caso:";
        public const string ConstCuenta = "Cuenta: ";
        public const string ConstDESCRIPCION = "DESCRIPCION";
        public const string ConstDocumentoAfectadoAreaDetCaso = "Documento Afectado Area Detalle(Caso) :";
        public const string ConstDocumentoAfectadoCaso = "Documento Afectado(Caso):";
        public const string ConstDocumentoReferencia = "Documento de Referencia :";
        public const string ConstDocumentosAjustes = "DocumentosAjustes";
        public const string ConstError = "Error";
        public const string ConstErrTransNoGenerCaso = "Error en la Transacción : No se puede generar el caso";
        public const string ConstErrTransNoGenerInterac = "Error en la transacción : No se puede generar la interacción";
        public const string ConstErrTransNoRealizaAjuste = "Error en la transacción : No se puede realizar el Ajuste ";
        public const string ConstErrWS = "Error WS";
        public const string ConstFactura = "Factura";
        public const string ConstIncluyeInfoLinea = "Incluye información de la línea: ";
        public const string ConstInteracCorrectIdInterac = "La interacción es correcta...Id interacción";
        public const string ConstLDI = "LDI";
        public const string ConstLDN = "LDN";
        public const string Constnone = "none";
        public const string ConstNoOk = "NoOk";

        public const string ConstOtrosCargNoAfecIGV = "Otros Cargos no Afectos al IGV";
        public const string ConstRazonSocial = "Razón Social: ";
        public const string ConstResultGrabaAreaDetCuandGenCaso = "Resultado de grabar Area Detalle, cuando ha generado caso :";
        public const string ConstResultGrabaAreaDetFlgIns = "Resultado de grabar Area Detalle, Flag de inserción :";
        public const string ConstResultGrabaCabCuandGenCaso = "Resultado de grabar cabecera, cuando ha generado caso :";
        public const string ConstResultGrabaCabFlgIns = "Resultado de grabar cabecera, Flag de inserción :";
        public const string ConstResultGrabaDetCuandGenCaso = "Resultado de grabar Detalle, cuando ha generado caso :";
        public const string ConstResultGrabaDetFlgIns = "Resultado de grabar Detalle, Flag de inserción :";
        public const string ConstResultGrabaInteraccion = "Resultado de grabar la Interacción...";
        public const string ConstResultInserAreaImput = "Resultado de Inserción de Area Imputable :";
        public const string ConstResultInserAreaImputFlg = "Resultado de Inserción de Area Imputable Flag :";
        public const string ConstResultInserCabeCodDoc = "Resultado de Inserción de cabecera Codigo de Documento:";
        public const string ConstResultInserCabeFlg = "Resultado de Inserción de cabecera Flag:";
        public const string ConstResultInserCabeMsg = "Resultado de Inserción de cabecera Mensaje:";
        public const string ConstResultInserDet = "Resultado de Inserción de Detalle :";
        public const string ConstResultInserDetCadena = "Resultado de Inserción de Detalle - Cadena:";
        public const string ConstResultInserDetFlg = "Resultado de Inserción de Detalle Flag :";
        public const string ConstResultTransacCodDoc = "Resultado de de la transacción - Codigo de Documento :";
        public const string ConstResultTransacFlg = "Resultado de la transacción - Flag :";
        public const string ConstResultTransacMsg = "Resultado de la transacción - Mensaje :";
        public const string ConstRoaming = "Roaming";
        public const string ConstSeEjeEliminaExito = "Se ejecutó la eliminación con éxito.";
        public const string ConstSeGeneroNDExito = "Se generó la Nota de Débito con éxito.";
        public const string ConstSIACPO = "SIACPO";
        public const string ConstSubTotalAfecto = "Sub Total Afecto :";
        public const string ConstSubTotalNoAfecto = "Sub Total No Afecto :";
        public const string ConstTrafiLocalAdic = "Tráfico Local Adicional";
        public const string ConstTrafiLocalConsumo = "Tráfico Local Consumo";
        public const string ConstAutorizaAct4G = "Autoriza Activación Servicio 4 G ";
        public const string ConstAutorizaDesact4G = "Autoriza Desactivación Servicio 4 G ";
        public const string ConstVALOR = "VALOR";
        public const string ConstC = "C";
        public const string ConstM = "M";
        public const string ConstN = "N";
        public const string ConstS = "S";
        public const string ConstArroba = "@";
        public const string ConstF = "F";

        public const string ConstAutoLinea = "AUTORIZACIÓN LÍNEA";
        public const string ConstAutoEquipo = "AUTORIZACIÓN EQUIPO";
        public const string ConstAutorizacion = "AUTORIZACIÓN";
        public const string ConstG = "G";
        public const string ConstTN = "TN";

        public const string ConstProblemaGenerInterac = "Problemas al generar al Interacción.";
        public const string ConstLaTransRealizoExito = "La transacción se realizó con éxito.";
        public const string ConstgAutorizaAltaCliEsp = "Autoriza Registro de Alta de Cliente Especial";
        public const string ConstAutorizado = "Autorizado";
        public const string ConstNoAutorizado = "No Autorizado";
        public const string ConstSlash = "/";
        public const string ConstGuion = "-";


        public static string ConstBloqueado = "BLOQUEADO";
        public static string ConstDesbloqueado = "DESBLOQUEADO";


        public static string ConstContactoConsultar = "Se está consultando la información de los contactos. Telefono/fecha: ";
        public static string ConstContactoRegistro = "Se está registrando Datos del Contacto. Telefono/fecha: ";
        public static string ConstContactoEliminar = "Se está eliminando Datos del Contacto. Telefono/fecha: ";

        public const string ValorS = "S";
        public const string ValorN = "N";
        public const int ValorNueve = 9;

        public const string ConstMenosTres = "-3";
        public const string ConstMenosCinco = "-5";

        //INI - PROY-24729 - Biometría y Puntos Normativos - RF03 - RP
        public const string ConstValorTrue = "TRUE";
        public const string ConstValorFalse = "FALSE";
        public const string ConstValorCuatro = "4";
        //FIN - PROY-24729 - Biometría y Puntos Normativos - RF03 - RP

        public const string ConstPROBLEMA = "PROBLEMA";

        /* Constantes para nuevo HLR - UDB */
        public const string lista_ZMIO = "ZMIO";
        public const string lista_ZMNO = "ZMNO";
        public const string lista_ZMGO = "ZMGO";
        public const string lista_ZMSO = "ZMSO";
        public const string lista_ZMQO = "ZMQO";
        public const string lista_ZMQO_IN = "IN/ZMQO";
        public const string lista_ZMQO_CA = "CA/ZMQO";
        public const string lista_ZMAO = "ZMAO";
        public const string lista_ZMNF = "ZMNF";
        public const string lista_ZMNI = "ZMNI";

        public const string par_HLR = "HLR";
        public const string par_MSIDN = "MSIDN";
        public const string par_IMSI = "IMSI";
        public const string par_ARC = "ARC";
        public const string par_RC = "RC";
        public const string par_CC = "CC";
        public const string par_MSC = "MSC2";
        public const string par_VLR = "VLR";
        public const string par_SAOM = "SAOM";
        public const string par_ACT_STATUS = "AS";

        public const string par_PSW = "PSW";
        public const string par_BSC = "BSC";
        public const string par_CW = "CW";
        public const string par_HOLD = "HOLD";
        public const string par_CLIP = "CLIP";
        public const string par_CLIR = "CLIR";
        public const string par_MPTY = "MPTY";
        public const string par_BAPRE = "BAPRE";
        public const string par_BOS3 = "BOS3";
        public const string par_BOS4 = "BOS4";
        public const string par_USSD = "USSD";
        public const string par_EPS_STATUS = "ES";
        public const string par_NETWORK_ACCESS = "NA";
        public const string par_CA = "CA";
        public const string par_MSIN = "MSIN";
        public const string par_ACU_IDENTITY = "AI";
        public const string par_BAOC = "BAOC";
        public const string par_BOIC = "BOIC";
        public const string par_BAIC = "BAIC";
        public const string par_BIRO = "BIRO";
        public const string par_BOIH = "BOIH";
        public const string par_BORO = "BORO";
        public const string par_CFU = "CFU";
        public const string par_CFB = "CFB";
        public const string par_CFNR = "CFNR";
        public const string par_CFNA = "CFNA";
        public const string par_OCCF = "OCCF";
        public const string par_TIME = "TIME";

        public const string sublista_T11 = "T11";
        public const string sublista_B17 = "B17";
        public const string sublista_T21 = "T21";
        public const string sublista_T22 = "T22";

        public const string par_DP = "DP";
        public const string par_SCP = "SCP";
        public const string par_DEFCH = "DEFCH";
        public const string par_SSET = "SSET";
        public const string par_DEFSMS = "DEFSMS";
        public const string par_DEFSH = "DEFSH";
        public const string par_DNT = "DNT";


        public const string par_QOSP = "QOSP";
        public const string par_APN = "APN";
        public const string par_PDP_TYPE = "PT";
        public const string par_PDP_ADDRESS = "PA";
        public const string par_SA = "SA";

        public const string par_OCSI = "OCSI";
        public const string par_MOSMSCSI = "MOSMSCSI";
        public const string par_GGCSI = "GGCSI";
        public const string par_VO = "VO";

        public static string strLetraY = "Y";
        public const string val_PCBN = "PCBN";
        public const string val_PREP = "PREP";
        public const string val_BOTH = "BOTH";
        public const string val_reg1 = "@REG01";
        public const string val_reg2 = "@REG02";
        public const string val_reg3 = "@REG03";




    }
}
