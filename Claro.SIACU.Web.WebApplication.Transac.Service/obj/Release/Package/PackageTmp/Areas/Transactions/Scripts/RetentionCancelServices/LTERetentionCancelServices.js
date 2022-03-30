(function ($, undefined) {


    var Smmry = new Summary('transfer');
    var SessionTransf = function () { };

    SessionTransf.hdnFlat = "";
    SessionTransf.hidAccionTra = "";
    SessionTransf.hidCustomerID = "";
    SessionTransf.hidTelReferencia = "";
    SessionTransf.hidCuenta = "";
    SessionTransf.hidCO = "";
    SessionTransf.hidListNumImportar = "";
    SessionTransf.hayCaso = "";
    SessionTransf.hdnSubMot = "";
    SessionTransf.hdnSubMotDesc = "";
    SessionTransf.hdnMotivo = "";
    SessionTransf.InteractionId = "";
    SessionTransf.hFlatInteraccion = "";
    SessionTransf.hidCAC = "";
    SessionTransf.hidMensaje = "";
    SessionTransf.hdnGeneroCaso = "";
    SessionTransf.hidDeshabilitaIndi = "";
    SessionTransf.hidenMotivoSot = "";
    SessionTransf.hdnSubTipOrdCU = "";
    SessionTransf.hdnTipoTrabCU = "";
    SessionTransf.hidClaseId = "";
    SessionTransf.hidSubClaseId = "";
    SessionTransf.hidTipo = "";
    SessionTransf.hidClaseDes = "";
    SessionTransf.hidSubClaseDes = "";
    SessionTransf.hdnFecAgCU = "";
    SessionTransf.hidFechaDefecto = "";
    SessionTransf.hidFechaActual = "";
    SessionTransf.hidFecMinimaCancel = "";
    SessionTransf.hdnCodigoRequestAct = "";
    SessionTransf.strRutaArchivo = "";

    SessionTransf.hidFlatMsj = "";
    SessionTransf.hidMensaje = "";
    SessionTransf.FlagResultado = "";

    SessionTransf.FechaREsultado = "";
    SessionTransf.FlatReintegro = "";
    SessionTransf.PenalidadApadece = "";

    SessionTransf.hidFlatEmail = "";

    SessionTransf.vDesInteraction = "";
    SessionTransf.hidSupJef = "";
    SessionTransf.hdnSubMotDesc = "";
    SessionTransf.hidPagoAPADECE = "";
    SessionTransf.Message = "";
    SessionTransf.strCasoId = "";
    SessionTransf.RutaArchivo = "";
    SessionTransf.MensajeEmail = "";


    //Mensajes
    /*WMC*/
    SessionTransf.strMsgDebeCargLinea = "";
    SessionTransf.flagRestringirAccesoTemporalCR = "";
    SessionTransf.gConstMsgOpcionTemporalmenteInhabilitada = "";
    SessionTransf.strEstadoContratoInactivo = "";
    SessionTransf.strEstadoContratoReservado = "";
    SessionTransf.strMsgValidacionContratoInactivo = "";
    SessionTransf.strMsgValidacionContratoReservado = "";
    SessionTransf.CambNumSinCosto = "";
    SessionTransf.CostoCambioNumeroConsumer = "";
    SessionTransf.SusTempSinCostoReconexion = "";
    SessionTransf.MontoCobroReactivacionServicio = "";
    SessionTransf.gConstMsgSelTr = "";
    SessionTransf.gConstMsgSelMot = "";
    SessionTransf.gConstMsgSelSubMot = "";
    SessionTransf.gConstMsgSelAc = "";
    SessionTransf.gConstMsgSelsinoCaso = "";
    SessionTransf.gConstMsgErrRecData = "";
    SessionTransf.gConstMsgErrCampNumeri = "";
    SessionTransf.gConstMsgSelCacDac = "";
    SessionTransf.gConstMensajeEsperaLoader = "";
    SessionTransf.AccesPage = "1"; //CAMBIAR SEGUN LA VARIABLES DE SESSION ==> SessionTransac.SessionParams.USERACCESS.optionPermissions;
    SessionTransf.gConstPerfHayCaso = "";
    SessionTransf.gConstFlagRetensionCancelacionEstado = "";
    SessionTransf.strFlagInhabTipTraMotSot = "";
    SessionTransf.strValueMotivoSOTDefecto = "";
    SessionTransf.strValueTipoTrabajoDefecto = "";
    SessionTransf.gConstFlagRetensionCancelacion = "";
    SessionTransf.Email = "";
    SessionTransf.hdnBajaTOTAL = "";
    SessionTransf.hdnValidado = "";
    SessionTransf.hdnFranjaHorariaCU = "";
    SessionTransf.hdnHaySubM = "";
    SessionTransf.strConsLineaDesaActiva = "";
    SessionTransf.gConstMsgLineaPOTP = "";
    SessionTransf.gConstMsgErrRecData = "";
    /*WMC*/
    SessionTransf.strMsgConsultaCustomerContratoVacio = "Debe consultar un contrato para utilizar esta opción.";
    SessionTransf.strTextoEstadoInactivo = "Estado del servicio no esta Activo.";
    SessionTransf.gConstKeyGenTipificacionDeco = "No se pudo generar la Tipificación, por favor vuelva a intentarlo mas tarde.";
    SessionTransf.strMensajeErrValAge = "Ocurrió un error al validar Agendamiento.";
    SessionTransf.strMensajeSeleTTra = "Seleccione Tipo de Trabajo.";
    SessionTransf.strMensajeSeleSubTipOrd = "Seleccione Subtipo de Trabajo.";
    SessionTransf.strMensajeSeleFranjaDispo = "Seleccione una Franja Horaria disponible.";
    SessionTransf.strMsgTranGrabSatis = "La transacción se ha grabado satisfactoriamente";
    SessionTransf.strMsgCreoInterErrTrans = "Se creó la interacción pero existe error en la transacción, el número insertado es: ";
    SessionTransf.gConstMsgLineaStatSuspe = "";
    SessionTransf.strMensajeDeError = "No se pudo ejecutar la transacción. Informe o vuelva a intentar.";
    //#region Proy-32650
    SessionTransf.idAccion = '';
    SessionTransf.codServAdic = '0'; /*valor de servicio adicional que escoja cod_ser*/
    SessionTransf.descServAdic = ''; // 'valor que seleccionará de la grilla'; descripcion del servicio adicional que escoja*/
    SessionTransf.desServicioPVU = 'falta';
    SessionTransf.montTariRete = '0'; //  SessionTransf.costoServicioconIGV =  SessionTransf.montTariRete == monto tarifa retencion
    SessionTransf.snCode = '0';
    SessionTransf.costoServiciosinIGV = '0';   //  tarifa retencion( calculo sfuncion sin igv)
    SessionTransf.costoServicioconIGV = '0'; // tarifa retencion
    SessionTransf.RetentionBonusServAdic = '0'; /*tarifa de RETENCION de la grilla de servicios adicionales*/
    SessionTransf.RegularBonusServAdic = '0' /*tarifa regular de la grilla para servicios adicionales*/
    SessionTransf.strTarifRegular = '0'; //en el log de produccion siempre se le envia un numero.@ 'TODO
    SessionTransf.strTarifRet = '';
    SessionTransf.strMontoDescuento = '';
    SessionTransf.hdnValorIgv = '0';
    SessionTransf.gConstMsgDescuentoActivo = "";
    SessionTransf.DE_SER = '';

    SessionTransf.DecodificatorSelected = {};
    SessionTransf.CODGRUPOSERV = '';

    SessionTransf.InstallationCost = '0';
    SessionTransf.AmountInstall = '0';
    SessionTransf.strMsjCantidadLimiteDecos = '';//validacion de decos

    //PROY-32650 F2
    SessionTransf.nuevoCostoInstal = '0';//Costo de instalación que se envía al bono. strCostoDescInst
    SessionTransf.costoDescuentoInstal = '0';//Costo descontado que se envía al servicio strDescuentoInst
    SessionTransf.isDecodificator = false;
    SessionTransf.ServiceType = '';
    SessionTransf.strShowChkPromAjustFact = '';
    SessionTransf.strTipoDeco = '';

    var AdditionalPointsModel = {};
    AdditionalPointsModel.strValidateETA = "";
    AdditionalPointsModel.strHistoryETA = "";
    AdditionalPointsModel.strJobTypeComplementarySalesHFC = "";
    AdditionalPointsModel.strInternetValue = "";
    AdditionalPointsModel.strCellPhoneValue = "";
    AdditionalPointsModel.strCustomerRequestId = "";
    AdditionalPointsModel.free = "";//Validacion de decos
    AdditionalPointsModel.freeTemp = "";
    //#endregion

    var Form = function ($element, options) {
        $.extend(this, $.fn.LTERetentionCancelServices.defaults, $element.data(), typeof options === 'object' && options);
        this.setControls({
            form: $element
            // Hidden
            , hidSupJef: $("#hidSupJef", $element)

            // Combo
            , cboCACDAC: $("#cboCACDAC", $element)
            , cboAccion: $("#cboAccion", $element)
            , cboMotCancelacion: $("#cboMotCancelacion", $element)
            , cboSubMotive: $("#cboSubMotive", $element)
            , cboTypeWork: $("#cboTypeWork", $element)
            , cboMotiveSOT: $("#cboMotiveSOT", $element)

            //Text
            , txtTotInversion: $("#txtTotInversion", $element)
            , txtEmail: $("#txtEmail", $element)
            , txtPenalidad: $("#txtPenalidad", $element)
            , txtNote: $('#txtNote', $element)

            //Etiquetas
            , lblContrato: $("#lblContrato", $element)
            , lblTipoCliente: $("#lblTipoCliente", $element)
            , lblContacto: $("#lblContacto", $element)
            , lblCodUbigeo: $("#lblCodUbigeo", $element)
            , lblCliente: $("#lblCliente", $element)
            , lblPlan: $("#lblPlan", $element)
            , lblFechaActivacion: $("#lblFechaActivacion", $element)
            , lblEstLinea: $("#lblEstLinea", $element)
            , lblTipAcu: $("#lblTipAcu", $element)
            , lblActivo: $("#lblActivo", $element)
            , lblDireccion: $("#lblDireccion", $element)
            , lblNotasDirec: $("#lblNotasDirec", $element)
            , lblDepartamento: $("#lblDepartamento", $element)
            , lblDistrito: $("#lblDistrito", $element)
            , lblCodPlano: $("#lblCodPlano", $element)
            , lblPais: $("#lblPais", $element)
            , lblProvincia: $("#lblProvincia", $element)
            , lblTipAcuerdo: $("#lblTipAcuerdo", $element)
            , lblErrorMessage: $("#lblErrorMessage", $element)
            , lblTitle: $('#lblTitle', $element)

            , lblCustomerID: $('#lblCustomerID', $element)
            , lblDNI_RUC: $('#lblDNI_RUC', $element)
            , lblRepren_Legal: $('#lblRepren_Legal', $element)
            , lblCicloFact: $('#lblCicloFact', $element)
            , lblLimiteCred: $('#lblLimiteCred', $element)


            //RadioButton
            , rdbRetenido: $("#rdbRetenido", $element)
            , rdbNoRetenido: $("#rdbNoRetenido", $element)
            , rdbAplica: $("#rdbAplica", $element)
            , rdbNoAplica: $("#rdbNoAplica", $element)
            , chkEmail: $("#chkEmail", $element)

            //Botones
            , btnCerrar01: $("#btnCerrar01", $element)
            , btnCerrar02: $("#btnCerrar02", $element)
            , btnCerrar03: $("#btnCerrar03", $element)
            , btnCerrar04: $("#btnCerrar04", $element)
            , btnGuardar: $("#btnGuardar", $element)
            , btnConstancia: $("#btnConstancia", $element)
            , btnAnteriorF: $("#btnAnteriorF", $element)

            , btnValidar: $('#btnValidar', $element)
            , btnSummaryDT: $('#btnSummaryDT', $element)
            , btnSummaryRetenido: $('#btnSummaryRetenido', $element)
            , btnSummaryAccion: $('#btnSummaryAccion', $element)

            , btnSummary02: $('#btnSummary02', $element)

            , myModalLoad: $("#myModalLoad", $element)
            , divFlatRetencion: $("#divFlatRetencion", $element)
            , divCaso: $("#divCaso", $element)

            , divReglas: $("#divReglas", $element)
            , divSubMotive: $("#divSubMotive", $element)
            , divErrorAlert: $('#divErrorAlert', $element)

            , cboSchedule: $("#cboSchedule", $element)

            //Text
            , txtDateCommitment: $("#txtDateCommitment", $element)

            //#region Proy-32650
            , divDesCargFijo: $("#divDesCargFijo", $element)
            , divAditionalServices: $("#divAditionalServices", $element)
            , cboMonthSA: $("#cboMonthSA", $element)
            , cboMonthCF: $("#cboMonthCF", $element)
            , cboDiscountCF: $("#cboDiscountCF", $element)
            , cboDiscountSA: $("#cboDiscountSA", $element)
            , chkPromFact: $("#chkPromFact", $element)
            , txtCostInst: $("#txtCostInst", $element)
            , txtTotDescuento: $("#txtTotDescuento", $element)


            //#region PROY-32650  II - Retención/Fidelización
            , cboTypeWorkAccion: $("#cboTypeWorkAccion", $element)
            , cboSubTypeWorkAccion: $('#cboSubTypeWorkAccion', $element)
            , cboMotiveSOTAccion: $('#cboMotiveSOTAccion', $element)
            , txtDateCommitmentAccion: $("#txtDateCommitmentAccion", $element)
            , cboScheduleAccion: $("#cboScheduleAccion", $element)
            , divFlatAccionFranja: $('#divFlatAccionFranja', $element)
            , tblAdiServBody: $("#tblAdiServBody", $element)
            // Modal
            , ModalLoading: $('#ModalLoading', $element)
            , divchkPromFact: $('#divchkPromFact', $element)
            //#endregion PROY-32650  II - Retención/Fidelización 
            //#endregion
        });
    };

    Form.prototype = {
        constructor: Form,

        init: function () {
            var that = this,
            controls = this.getControls();

            controls.cboMotCancelacion.addEvent(that, 'change', that.cboMotCancelacion_change);
            controls.cboTypeWork.addEvent(that, 'change', that.cboTypeWork_change);
            controls.cboMotiveSOT.addEvent(that, 'change', that.cboMotiveSOT_change);
            controls.btnCerrar01.addEvent(that, 'click', that.btnCerrar_Click);
            controls.btnCerrar02.addEvent(that, 'click', that.btnCerrar_Click);
            controls.btnCerrar03.addEvent(that, 'click', that.btnCerrar_Click);
            controls.btnCerrar04.addEvent(that, 'click', that.btnCerrar_Click);
            controls.btnGuardar.addEvent(that, 'click', that.btnGuardar_click);
            controls.btnConstancia.addEvent(that, 'click', that.btnConstancia_click);
            controls.btnSummaryDT.addEvent(that, 'click', that.btnSummaryDT_click);
            controls.btnSummaryRetenido.addEvent(that, 'click', that.btnSummaryRetenido_click);
            controls.btnSummaryAccion.addEvent(that, 'click', that.btnSummaryAccion_click);

            controls.btnSummary02.addEvent(that, 'click', that.btnSummaryAccion_click);
            controls.rdbRetenido.addEvent(that, 'click', that.rdbRetenido_click);
            controls.rdbNoRetenido.addEvent(that, 'click', that.rdbNoRetenido_click);
            controls.chkEmail.addEvent(that, 'change', that.chkEmail_Change);
            //#region Proy-32650
            controls.cboAccion.addEvent(that, 'change', that.cboAccion_change);
            controls.cboMonthSA.addEvent(that, 'change', that.cboMonthSA_change);
            controls.cboMonthCF.addEvent(that, 'change', that.cboMonthCF_change);
            controls.cboDiscountCF.addEvent(that, 'change', that.cboDiscountCF_change);
            controls.cboDiscountSA.addEvent(that, 'change', that.cboDiscountSA_change);
            controls.chkPromFact.addEvent(that, 'change', that.chkPromFact_change);
            //#endregion

            //#region PROY-32650  II - Retención/Fidelización
            controls.cboTypeWorkAccion.addEvent(that, 'change', that.cboTypeWorkAccion_Change);
            controls.cboSubTypeWorkAccion.addEvent(that, 'change', that.cboSubTypeWorkAccion_Change);
            //#endregion PROY-32650  II - Retención/Fidelización 
            that.maximizarWindow();
            that.windowAutoSize();
            that.render();
        },

        render: function () {

            var that = this,
            control = that.getControls();

            control.divErrorAlert.hide();
            control.btnConstancia.prop('disabled', true);
            //Resumen
            Smmry.set('Penalidad', '0');
            Smmry.set('Retenido', 'SI');

            Smmry.set('MotivoCancelacion', '');
            Smmry.set('TotalInversion', '');
            Smmry.set('SubMotivo', '');
            Smmry.set('TipoTrabajo', '');
            Smmry.set('MotivoSot', '');
            Smmry.set('SubTipoTrabjo', '');
            Smmry.set('AplicaCaso', 'SI');

            Smmry.set('Accion', '');
            Smmry.set('Correo', '');
            Smmry.set('PuntoVenta', '');
            Smmry.set('Nota', '');

            control.divErrorAlert.hide();
            //Por Default Retenido
            SessionTransf.hidAccionTra = 'R';
            control.rdbRetenido.attr("checked", true);
            control.divFlatRetencion.hide();
            control.divCaso.hide();
            $('tr.retenido').hide();
            control.txtPenalidad.attr("readonly", true);
            control.txtPenalidad.val("0.00");
            control.txtTotInversion.val("0.00");
            //#region Proy-32650
            Smmry.set('Vigencia', '');
            Smmry.set('Descuento', '');

            control.txtTotDescuento.attr("readonly", true);

            //#region PROY-32650  II - Retención/Fidelización
            $("#divFlatAccionFranja").css("display", "none");

            that.GetTypeWorkAccion();//nueva forma
            that.GetMotiveSOTAccion();
            that.GetDateAccion();
            that.f_GetParameter();
            //#endregion PROY-32650  II - Retención/Fidelización
            control.btnValidar.prop("disabled", true);
            that.tblAdiServBodyRow_Click();
            //#endregion
            //
            that.loadCustomerData();
            that.InitGetMessage();

            control.btnValidar.prop("disabled", true);
            InitFranjasHorario();

        },

        ShowChkPromAjustFact: function () {

            if (SessionTransf.strShowChkPromAjustFact == "0") {
                $("#divchkPromFact").css("display", "none");
            }

        },
        windowAutoSize: function () {
            var hsize = Math.max(
                    document.documentElement.clientHeight,
                    document.body.scrollHeight,
                    document.documentElement.scrollHeight,
                    document.body.offsetHeight,
                    document.documentElement.offsetHeight
                );
            hsize = hsize - 72;
            $('#content').css({ 'height': hsize + 'px' });
        },

        IniBegin: function () {
            var that = this,
            controls = this.getControls();

            that.IniLoadPage();


        },

        InitGetMessage: function () {
            var that = this,
                controls = this.getControls(),
                objModel = {};

            that.loadPage();

            objModel.strIdSession = Session.IDSESSION;

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objModel),
                url: '/Transactions/LTE/RetentionCancelServices/GetMessage',
                success: function (response) {
                    if (response != null) {
                        SessionTransf.gConstMsgLineaPOTP = response[0];
                        SessionTransf.gConstMsgErrRecData = response[1];
                        SessionTransf.hdnBajaTOTAL = response[2];
                        SessionTransf.gConstFlagRetensionCancelacion = response[3];
                        SessionTransf.strValueMotivoSOTDefecto = response[4];
                        SessionTransf.strValueTipoTrabajoDefecto = response[5];
                        SessionTransf.strFlagInhabTipTraMotSot = response[6];
                        SessionTransf.gConstFlagRetensionCancelacionEstado = response[7];
                        SessionTransf.gConstPerfHayCaso = response[8];
                        SessionTransf.FechaActualServidor = response[9];
                        SessionTransf.gConstMsgSelTr = response[10];
                        SessionTransf.gConstMsgSelMot = response[11];
                        SessionTransf.gConstMsgSelSubMot = response[12];
                        SessionTransf.gConstMsgSelAc = response[13];
                        SessionTransf.gConstMsgSelsinoCaso = response[14];
                        SessionTransf.gConstMsgErrRecData = response[15];
                        SessionTransf.gConstMsgErrCampNumeri = response[16];
                        SessionTransf.gConstMsgSelCacDac = response[17];
                        SessionTransf.gConstMensajeEsperaLoader = response[18];
                        SessionTransf.CambNumSinCosto = response[19];
                        SessionTransf.CostoCambioNumeroConsumer = response[20];
                        SessionTransf.SusTempSinCostoReconexion = response[21];
                        SessionTransf.MontoCobroReactivacionServicio = response[22];

                        SessionTransf.strEstadoContratoInactivo = response[23];
                        SessionTransf.strEstadoContratoReservado = response[24];
                        SessionTransf.strMsgValidacionContratoInactivo = response[25];
                        SessionTransf.strMsgValidacionContratoReservado = response[26];

                        SessionTransf.flagRestringirAccesoTemporalCR = response[26];
                        SessionTransf.gConstMsgOpcionTemporalmenteInhabilitada = response[26];

                        SessionTransf.strMsgDebeCargLinea = response[27];
                        SessionTransf.gConstMsgLineaStatSuspe = response[28];

                        SessionTransf.strConsLineaDesaActiva = response[29];
                        SessionTransf.gConstMsgDescuentoActivo = response[33];
                        SessionTransf.strMsjCantidadLimiteDecos = response[33];
                        SessionTransf.ServiceType = response[34];//Solo para LTE
                        SessionTransf.strShowChkPromAjustFact = response[35]; //Proy-32650
                        Session.VALIDATE = "0";
                        Session.COD_RESQUEST_ACT = "";
                        Session.CASE_ID = "";

                        that.IniValidateLoadPage();
                    }
                }
            });
        },

        IniValidateLoadPage: function () {
            this.GetTransactionScheduled();
            this.ShowChkPromAjustFact();
        },

        IniLoadPage: function () {
            var that = this,
                controls = that.getControls(),

            objType = {};
            objType.strIdSession = Session.IDSESSION;
            objType.strContratoID = Session.DATACUSTOMER.ContratoID;
            objType.strListNumImportar = '';
            objType.strNroTelefono = Session.DATALINEA.NroCelular;
            objType.CadenaOpciones = Session.USERACCESS.optionPermissions;
            //#region Proy-32650
            controls.chkEmail[0].checked = true;
            that.chkEmail_Change();
            //#endregion
            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objType),
                url: '/Transactions/LTE/RetentionCancelServices/IniLoadPage',
                async: false,
                error: function (data) {
                    alert("Error JS : en llamar al IniLoadPage.", "Alerta");
                },
                success: function (response) {

                    SessionTransf.FechaREsultado = response.FechaResultado;
                    SessionTransf.FlatReintegro = response.FlatReintegro;
                    SessionTransf.hidFlatMsj = response.data;
                    SessionTransf.PenalidadApadece = ((response.PenalidaAPADECE == "" || response.PenalidaAPADECE == undefined) ? "0.00" : response.PenalidaAPADECE);
                    SessionTransf.habilitaFecha = response.habilitaFecha;
                    SessionTransf.Message = response.Message;
                    SessionTransf.hdnValorIgv = response.valorIgv;//Proy-32650
                    SessionTransf.habilitaNoRetention = response.habilitaNoRetention;//Proy-32650

                    if (SessionTransf.Message != "") {
                        controls.divErrorAlert.show();
                        controls.lblErrorMessage.text(SessionTransf.Message);
                    }

                    controls.txtDateCommitment.prop("disabled", SessionTransf.habilitaFecha);

                    //----Inicio
                    if (SessionTransf.hidFlatMsj) {

                        isPostBackFlag = NumeracionCERO;

                        Session.DeshabilitaIndi = NumeracionCERO;
                        if (Session.TIPOSERVICIO = NumeracionUNO) {
                            that.f_AsignarApadece(NumeracionUNO);
                        } else {
                            that.f_AsignarApadece(NumeracionDOS);
                        }

                        SessionTransf.hidCustomerID = Session.CUSTOMERID;
                        SessionTransf.hidTelReferencia = Session.DATALINEA.NroCelular;
                        SessionTransf.hidCuenta = Session.Cuenta;
                        SessionTransf.hidCO = gConstKeyCoCanServ;

                        that.InitMotCancel();

                        that.InitAccion();
                        //#region Proy-32650
                        that.GetTotalInversion();
                        that.GetCurrentDiscountFixedCharge();
                        that.InitDiscountAS();
                        that.InitDiscountCF();


                        that.GetDefaultDecoderVariables();

                        /*PROY-32650 F2*/

                        that.getTypificationTransaction();
                        //#endregion

                        controls.txtPenalidad.val((that.Round(SessionTransf.PenalidadApadece, 2)).toFixed(2));

                        if (SessionTransf.FlatReintegro) {
                            controls.txtPenalidad.attr("readonly", false);
                        } else {
                            controls.txtPenalidad.attr("readonly", true);
                        }

                        that.InitTypeWork();

                        if (SessionTransf.gConstFlagRetensionCancelacion == "0") {
                            that.InitMotiveSOT();
                        } else {
                            controls.cboMotiveSOT.html("");
                            controls.cboMotiveSOT.append($('<option>', { value: '-1', html: 'Seleccionar' }));
                        }

                        //Cargar Sub Tipo Trabajo

                        if (SessionTransf.gConstFlagRetensionCancelacionEstado == "0") {
                            if (SessionTransf.strFlagInhabTipTraMotSot == "1") {
                                controls.cboMotiveSOT.attr('disabled', true);
                                controls.cboTypeWork.attr('disabled', true);

                                controls.cboMotiveSOT_Action.attr('disabled', true);
                                controls.cboTypeWork_Action.attr('disabled', true);
                            }
                        }
                        that.DeterminaSiHayCaso();

                    } else {
                        alert('Error', "Alerta");
                    }

                    //----Fín
                },
            });
        },

        GetTransactionScheduled: function () {
            var that = this,
                control = that.getControls(),
                param = {};

            if (Session.DATALINEA.StatusLinea == SessionTransf.strEstadoContratoInactivo) {
                alert(SessionTransf.strMsgValidacionContratoInactivo, 'Alerta', function () { parent.window.close(); }); return;
            } else if (Session.DATALINEA.StatusLinea == SessionTransf.strEstadoContratoReservado) {
                alert(SessionTransf.strMsgValidacionContratoReservado, 'Alerta', function () { parent.window.close(); }); return;
            }

            if (SessionTransf.flagRestringirAccesoTemporalCR == "1") {
                alert(SessionTransf.gConstMsgOpcionTemporalmenteInhabilitada, 'Alerta', function () { parent.window.close(); }); return;
            }

            //Validación Linea Activa
            if (Session.DATALINEA.StatusLinea == SessionTransf.strVariableEmpty) {
                alert(SessionTransf.strMsgDebeCargLinea, 'Alerta', function () { parent.window.close(); }); return;
            } else if (Session.DATALINEA.StatusLinea == SessionTransf.strConsLineaDesaActiva) {
                alert(SessionTransf.gConstMsgLineaStatSuspe, 'Alerta', function () { parent.window.close(); }); return;
            }

            that.IniBegin();
            that.InitCacDat();
            that.getTypification();
        },

        //GetTransactionScheduled: function () {
        //    var that = this,
        //        control = that.getControls(),
        //        param = {};

        //    param.strIdSession = Session.IDSESSION;
        //    param.strContratoID = Session.DATACUSTOMER.ContratoID;

        //    $.ajax({
        //        type: 'POST',
        //        contentType: "application/json; charset=utf-8",
        //        dataType: 'json',
        //        data: JSON.stringify(param),
        //        url: '/Transactions/LTE/RetentionCancelServices/GetTransactionScheduled',
        //        error: function (response) {

        //        },
        //        success: function (response) {
        //            if (response.data != null) {
        //                if (Session.DATALINEA.StatusLinea == SessionTransf.strEstadoContratoInactivo) {
        //                    alert(SessionTransf.strMsgValidacionContratoInactivo, 'Alerta', function () { parent.window.close(); }); return;
        //                } else if (Session.DATALINEA.StatusLinea == SessionTransf.strEstadoContratoReservado) {
        //                    alert(SessionTransf.strMsgValidacionContratoReservado, 'Alerta', function () { parent.window.close(); }); return;
        //                }

        //                if (SessionTransf.flagRestringirAccesoTemporalCR == "1") {
        //                    alert(SessionTransf.gConstMsgOpcionTemporalmenteInhabilitada, 'Alerta', function () { parent.window.close(); }); return;
        //                }

        //                //Validación Linea Activa
        //                if (Session.DATALINEA.StatusLinea == SessionTransf.strVariableEmpty) {
        //                    alert(SessionTransf.strMsgDebeCargLinea, 'Alerta', function () { parent.window.close(); }); return;
        //                } else if (Session.DATALINEA.StatusLinea == SessionTransf.strConsLineaDesaActiva) {
        //                    alert(SessionTransf.gConstMsgLineaStatSuspe, 'Alerta', function () { parent.window.close(); }); return;
        //                }

        //                if (response.data == false) {
        //                    alert(SessionTransf.gConstMsgLineaPOTP, 'Alerta', function () { parent.window.close(); }); return;
        //                }

        //                that.IniBegin();
        //                that.InitCacDat();
        //                that.getTypification();
        //            }
        //        }
        //    });
        //},

        // AccessPageProfile => Cargar desde las variables de Session
        DeterminaSiHayCaso: function () {
            SessionTransf.hayCaso = "0";
        },

        CalculaDiasHabiles: function () {

            var that = this,
                controls = that.getControls(),
            objType = {};
            objType.NroDias = '8';
            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objType),
                url: '/Transactions/LTE/RetentionCancelServices/CalculaDiasHabiles',
                async: false,

                error: function (data) {
                    alert("Error JS : en llamar al CalculaDiasHabiles.", "Alerta");
                },
                success: function (response) {
                    FechaInicia = response.data;

                },

            });

        },

        loadCustomerData: function () {
            var that = this;
            var controls = this.getControls();
            controls = that.getControls();

            controls.lblTitle.text("Retención / Cancelación de Servicios");

            var SessionTransac = JSON.parse(sessionStorage.getItem("SessionTransac"));

            if (SessionTransac.UrlParams.IDSESSION == null || SessionTransac.UrlParams.IDSESSION == undefined) {
                Session.IDSESSION = '20170811110824824827183';
            } else {
                Session.IDSESSION = SessionTransac.UrlParams.IDSESSION;
            }

            Session.CLIENTE = SessionTransac.SessionParams.DATACUSTOMER;
            Session.LINEA = SessionTransac.SessionParams.DATASERVICE;
            Session.ACCESO = SessionTransac.SessionParams.USERACCESS;


            Session.DATACUSTOMER =
            {
                ContratoID: Session.CLIENTE.ContractID,
                Country: Session.CLIENTE.LegalCountry,
                CustomerContact: Session.CLIENTE.CustomerContact,
                CustomerID: Session.CLIENTE.CustomerID,
                CustomerTypeCode: Session.CLIENTE.CodCustomerType,
                refencial: Session.CLIENTE.Reference,
                Departament_Fact: Session.CLIENTE.InvoiceDepartament,
                District: Session.CLIENTE.District,
                provincia: Session.CLIENTE.Province,
                Cuenta: Session.CLIENTE.Account,
                Ciclo_Facturacion: Session.CLIENTE.BillingCycle,
                Nro_Doc: Session.CLIENTE.DocumentNumber,
                NameCompleto: Session.CLIENTE.FullName,
                TipoCliente: Session.CLIENTE.CustomerType,
                Ubigueo: Session.CLIENTE.InstallUbigeo,
                RazonSocial: Session.CLIENTE.BusinessName,
                FechaActivacion: Session.CLIENTE.ActivationDate,
                Codigo_Plano_Fact: Session.CLIENTE.PlaneCodeBilling,
                Email: Session.CLIENTE.Email,
                Direccion_Despacho: Session.CLIENTE.Address,
                Urbanizacion_Legal: Session.CLIENTE.LegalUrbanization,
                Departamento_Legal: Session.CLIENTE.LegalDepartament,
                Distrito_Legal: Session.CLIENTE.LegalDistrict,
                Codigo_Plano_Inst: Session.CLIENTE.PlaneCodeInstallation,
                Country_Legal: Session.CLIENTE.LegalCountry,
                telefono_Referencial: Session.CLIENTE.PhoneReference,
                Codigo_Tipo_Customer: Session.CLIENTE.CodCustomerType,
                Departamento_Fact: Session.CLIENTE.InvoiceDepartament,
                Distrito_Fac: Session.CLIENTE.InvoiceDistrict,
                Country_Fac: Session.CLIENTE.InvoiceCountry,
                Provincia_Fac: Session.CLIENTE.InvoiceProvince,
                Objid_Site: Session.CLIENTE.SiteCode,
                RepresentLegal: Session.CLIENTE.LegalAgent,
                DNI_RUC: Session.CLIENTE.DNIRUC,
                TipoDocumento: Session.CLIENTE.DocumentType,
                PlaneCode: Session.CLIENTE.PlaneCodeInstallation,
                District_Fac: Session.CLIENTE.InvoiceDistrict,
                LegalUrbanization: Session.CLIENTE.LegalUrbanization,
                Segmento: Session.CLIENTE.CustomerType,
                //#region Proy-32650
                Name: Session.CLIENTE.Name,
                LastName: Session.CLIENTE.LastName,
                BirthDate: Session.CLIENTE.BirthDate,
                BirthPlaceID: Session.CLIENTE.BirthPlaceID,
                Sex: Session.CLIENTE.Sex,
                CivilStatusID: Session.CLIENTE.CivilStatusID,
                Position: Session.CLIENTE.Position,
                Fax: Session.CLIENTE.Fax,
                BirthPlace: Session.CLIENTE.BirthPlace,
                CivilStatus: Session.CLIENTE.CivilStatus,
                //#endregion
            }

            Session.DATALINEA =
            {
                NroCelular: Session.LINEA.CellPhone,
                StatusLinea: Session.LINEA.StateLine,
                Plan: Session.LINEA.Plan,
                Plazo_Contrato: Session.LINEA.TermContract,
                StateAgreement: Session.LINEA.StateAgreement,
            }

            Session.USERACCESS =
            {
                optionPermissions: Session.ACCESO.optionPermissions,
                CodigoPerfil: Session.ACCESO.sapVendorId,
                CodigoUsuario: Session.ACCESO.userId,
                NombreCompleto: Session.ACCESO.fullName,
                Login: Session.ACCESO.login,
            };

            //Constantes

            SessionTransf.strVariableEmpty = "";
            //********** Datos del Cliente ***********/
            controls.lblContrato.html((Session.CLIENTE.ContractID == null) ? '' : Session.CLIENTE.ContractID);
            controls.lblCustomerID.html((Session.CLIENTE.CustomerID == null) ? '' : Session.CLIENTE.CustomerID);
            controls.lblTipoCliente.html((Session.CLIENTE.CustomerType == null) ? '' : Session.CLIENTE.CustomerType);
            controls.lblCliente.html((Session.CLIENTE.BusinessName == null) ? '' : Session.CLIENTE.BusinessName);
            controls.lblContacto.html((Session.CLIENTE.FullName == null) ? '' : Session.CLIENTE.FullName);
            controls.lblDNI_RUC.html((Session.CLIENTE.DNIRUC == null) ? '' : Session.CLIENTE.DNIRUC);
            controls.lblRepren_Legal.html((Session.CLIENTE.LegalAgent == null) ? '' : Session.CLIENTE.LegalAgent);
            controls.lblPlan.html((Session.DATALINEA.Plan == null) ? '' : Session.DATALINEA.Plan);
            controls.lblFechaActivacion.html((Session.DATACUSTOMER.FechaActivacion == null) ? '' : Session.DATACUSTOMER.FechaActivacion);
            controls.lblCicloFact.html((Session.CLIENTE.BillingCycle == null) ? '' : Session.CLIENTE.BillingCycle);
            controls.lblLimiteCred.html((Session.CLIENTE.objPostDataAccount.CreditLimit == null) ? '' : 'S/ ' + Session.CLIENTE.objPostDataAccount.CreditLimit);


            //********** Direccíón de Facturación ***********/

            controls.lblDireccion.html((Session.DATACUSTOMER.Direccion_Despacho == null) ? '' : Session.DATACUSTOMER.Direccion_Despacho);
            controls.lblNotasDirec.html((Session.DATACUSTOMER.LegalUrbanization == null) ? '' : Session.DATACUSTOMER.LegalUrbanization);
            controls.lblPais.html((Session.DATACUSTOMER.Country == null) ? '' : Session.DATACUSTOMER.Country);
            controls.lblDepartamento.html((Session.DATACUSTOMER.Departament_Fact == null) ? '' : Session.DATACUSTOMER.Departament_Fact);
            controls.lblProvincia.html((Session.DATACUSTOMER.provincia == null) ? '' : Session.DATACUSTOMER.provincia);
            controls.lblDistrito.html((Session.DATACUSTOMER.District == null) ? '' : Session.DATACUSTOMER.District);
            controls.lblCodUbigeo.html((Session.CLIENTE.InstallUbigeo == null) ? '' : Session.CLIENTE.InstallUbigeo);
            controls.txtEmail.val((Session.DATACUSTOMER.Email == null) ? '' : Session.DATACUSTOMER.Email);
        },

        LoadPag: function () {
            var that = this, controls = this.getControls();
            $.blockUI({
                message: controls.myModalLoad,
                css: {
                    border: 'none',
                    padding: '15px',
                    backgroundColor: '#000000',
                    '-webkit-border-radius': '50px',
                    '-moz-border-radius': '50px',
                    opacity: .7,
                    color: '#fff'
                }
            });

        },

        chkEmail_Change: function (sender, arg) {
            var that = this,
                control = that.getControls(),
                chkEmail = control.chkEmail;

            if (chkEmail[0].checked == true) {
                control.txtEmail.css("display", "block");
            } else {
                control.txtEmail.css("display", "none");
            }
        },

        cboTypeWork_change: function () {
            var that = this,
                control = that.getControls();

            SessionTransf.hdnTipoTrabCU = control.cboTypeWork.val();

            if (SessionTransf.hdnTipoTrabCU.indexOf(".|") == -1) {
                control.btnValidar.prop("disabled", true);
                SessionTransf.hdnValidado = "1";

                //Asignando fecha por defecto 07 días, si no tienen franja horaria

                var fechaServidor = new Date(Session.ServerDate);

                var fechaServidorMas7Dias = new Date(fechaServidor.setDate(fechaServidor.getDate() + 7));
                control.txtDateCommitment.val([that.pad(fechaServidorMas7Dias.getDate()), that.pad(fechaServidorMas7Dias.getMonth() + 1), fechaServidorMas7Dias.getFullYear()].join("/"));

                SessionTransf.hdnFecAgCU = ([that.pad(fechaServidorMas7Dias.getDate()), that.pad(fechaServidorMas7Dias.getMonth() + 1), fechaServidorMas7Dias.getFullYear()].join("/"));
                SessionTransf.FechaREsultado = control.txtDateCommitment.val();
            } else {
                control.btnValidar.prop("disabled", false);
                SessionTransf.hdnValidado = "0";
            }

            SessionTransf.hdnSubTipOrdCU = "";
            if (control.cboTypeWork.val() != "-1") {
                if (control.cboTypeWork.val() != SessionTransf.hdnBajaTOTAL) {
                    that.GetMotiveSOTByTypeJob(control.cboTypeWork.val());
                } else {
                    alert("No aplica agendamiento en línea, favor de continuar con la operación.", "Alerta");
                }
            }
        },

        maximizarWindow: function () {
            top.window.moveTo(0, 0);
            if (document.all) {
                top.window.resizeTo(screen.availWidth, screen.availHeight);
            } else if (document.layers || document.getElementById) {
                if (top.window.outerHeight < screen.availHeight || top.window.outerWidth < screen.availWidth) {
                    top.window.outerHeight = screen.availHeight;
                    top.window.outerWidth = screen.availWidth;
                }
            }
        },

        cboMotCancelacion_change: function () {
            var that = this,
           controls = that.getControls(),
                param = {};

            SessionTransf.hdnMotivo = $('#cboMotCancelacion option:selected').text();
            param.strIdSession = Session.IDSESSION;
            param.IdMotive = controls.cboMotCancelacion.val();

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(param),
                url: '/Transactions/LTE/RetentionCancelServices/GetSubMotiveCancel',
                success: function (response) {
                    controls.cboSubMotive.html("");
                    controls.cboSubMotive.append($('<option>', { value: '-1', html: 'Seleccionar' }));
                    if (response.data != null) {
                        if (response.data.length > 0) {
                            $.each(response.data, function (index, value) {
                                controls.cboSubMotive.append($('<option>', { value: value.Code, html: value.Description }));
                            });

                            controls.divSubMotive.show();
                            SessionTransf.hdnHaySubM = "1";
                        } else {
                            controls.divSubMotive.hide();
                            SessionTransf.hdnHaySubM = "-1";
                        }
                    } else {
                        controls.divSubMotive.hide();
                        SessionTransf.hdnHaySubM = "-1";
                    }
                },
                error: function (response) {
                    alert(SessionTransf.gConstMsgErrRecData, "Alerta");
                }
            });
        },

        cboSubMotive_change: function () {
            SessionTransf.hdnSubMot = ($('#cboSubMotive').val());
            SessionTransf.hdnSubMotDesc = $('#cboSubMotive option:selected').text();
        },

        cboMotiveSOT_change: function () {
            var that = this,
            controls = that.getControls();

            SessionTransf.hidenMotivoSot = controls.cboMotiveSOT.val();
        },

        btnConstancia_click: function () {


            var PDFRoute = SessionTransf.RutaArchivo;
            var IdSession = Session.IDSESSION;
            if (PDFRoute != "") {
                ReadRecordSharedFile(IdSession, PDFRoute);
            }
        },

        btnGuardar_click: function () {
            var that = this,
                controls = this.getControls(),
                isPostBackFlag = NumeracionUNO;

            confirm("¿Seguro que desea guardar la transacción?", 'Confirmar', function () {
                //#region Proy-32650
                that.loadPage();
                if (SessionTransf.hidAccionTra == 'R') { // Retenedido
                    if (SessionTransf.idAccion == strIdCargoFijo || SessionTransf.idAccion == strIdServicioAdicional) {
                        that.SaveTransactionRetention('NP');
                    }
                    else {
                        that.SaveTransactionRetention('CP');
                    }
                } else { // No Retenedido  NR
                    if (SessionTransf.habilitaNoRetention)
                    {
                        that.SaveTransactionNoRetention();
                    }
                    else {
                    that.f_VentanaAutorizacion();
                }
                }
                //#endregion
            }, function () {
                $("#hidAccion").val("");
                return false;
            });
        },

        btnCerrar_Click: function () {
            parent.window.close();
        },

        rdbRetenido_click: function () {
            var that = this,
                control = that.getControls();

            SessionTransf.hidAccionTra = 'R';
            $('tr.retenido').hide();
            control.divFlatRetencion.hide();
            control.divCaso.hide();
            control.rdbAplica.attr("checked", true);
            control.rdbNoAplica.attr("checked", false);
            control.txtPenalidad.attr("readonly", true);
            control.txtPenalidad.val("0.00");

            that.CleanTypeJobAndMotiveSOT();
        },

        rdbNoRetenido_click: function () {
            var that = this,
                control = that.getControls();

            SessionTransf.hidAccionTra = 'NR';
            $('tr.retenido').show();
            control.divFlatRetencion.show();
            control.txtPenalidad.attr("readonly", false);
            control.rdbAplica.attr("checked", false);
            control.rdbNoAplica.attr("checked", true);
            that.DetermineCase();

            that.CleanTypeJobAndMotiveSOT();
        },

        f_AsignarApadece: function (obj) {

            if (obj == "1") {
                $("#idTDApadeceDes").val("Reintegro APADECE:");
            } else {
                $("#idTDApadeceDes").val("Reintegro APALECE:");
            }

        },

        IniGetParameter: function (gConstDiasHabiles) {

            var that = this,
            controls = that.getControls(),
            objType = {};
            objType.strIdSession = Session.IDSESSION;
            objType.name = gConstDiasHabiles;
            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objType),
                url: '/Transactions/CommonServices/GetParameterData',
                async: false,

                error: function (data) {
                    alert("Error JS : en llamar al GetParameterData.", "Alerta");
                },
                success: function (response) {
                    Value_C = response.data.Parameter.Value_C;
                },

            });

        },

        setControls: function (value) {
            this.m_controls = value;
        },

        getControls: function () {
            return this.m_controls || {};
        },

        CleanTypeJobAndMotiveSOT: function () {
            var that = this,
                control = that.getControls();

            control.cboTypeWork.val("-1");
            control.cboMotiveSOT.html("");
            control.cboMotiveSOT.append($('<option>', { value: '-1', html: 'Seleccionar' }));
            SessionTransf.hidenMotivoSot = control.cboMotiveSOT.val();
        },

        DetermineCase: function () {
            var that = this,
                control = that.getControls();

            if (SessionTransf.hayCaso == "1") {
                control.divCaso.show();
            } else {
                control.divCaso.hide();
            }
        },

        GetMotiveSOTByTypeJob: function (IdTipoTrabajo) {

            var that = this,
                control = that.getControls(),
                param = {};

            param.strIdSession = Session.IDSESSION;
            param.IdTipoTrabajo = IdTipoTrabajo;

            $.ajax({
                type: "POST",
                url: "/Transactions/HFC/RetentionCancelServices/GetMotiveSOTByTypeJobs",
                data: JSON.stringify(param),
                contentType: "application/json; charset=utf-8",
                dataType: 'json',

                error: function (data) {
                    alert("Error JS : en llamar al GetMotiveSOTByTypeJob.", "Alerta");
                },

                success: function (response) {
                    control.cboMotiveSOT.html("");
                    control.cboMotiveSOT.append($('<option>', { value: '-1', html: 'Seleccionar' }));
                    if (response.data != null) {
                        $.each(response.data, function (index, value) {
                            control.cboMotiveSOT.append($('<option>', { value: value.Codigo, html: value.Descripcion }));
                        });
                    }
                }
            });
        },

        BlockControl: function () {

            $("#txtPenalidad").attr("disabled", true);
            $("#txtTotInversion").attr("disabled", true);
            $("#rdbRetenido").attr("checked", false);
            $("#rdbNoRetenido").attr("checked", false);
            $("#rdbAplica").attr("checked", false);
            $("#rdbNoAplica").attr("checked", false);
            $("#cboMotCancelacion").attr("disabled", true);

            $("#cboAccion").attr("disabled", true);

            $("#txtNota").attr("disabled", true);
            $("#txtNota").val();

        },

        SaveTransactionRetention: function (typeProc) { //Proy-32650
            var that = this,
                controls = that.getControls(),
                strUrl = '',
                model = {};
            model.CicloFacturacion = Session.DATACUSTOMER.Ciclo_Facturacion;
            model.RegularBonusServAdic = '0';//inicializo con cero para que en el WS/SP no hay error de conversion de null a Number
            model.IdSession = Session.IDSESSION;
            model.Reintegro = $("#txtPenalidad").val();
            model.DesMotivos = $('#cboMotCancelacion option:selected').text();
            model.DesAccion = $('#cboAccion option:selected').text();
            model.hidSupJef = SessionTransf.hidSupJef;
            model.NroCelular = Session.DATALINEA.NroCelular;
            model.ValAccion = SessionTransf.idAccion;
            model.Accion = SessionTransf.hidAccionTra;
            if (controls.cboSubMotive.val() != '-1')
                model.DesSubMotivo = $('#cboSubMotive option:selected').text();
            else
                model.DesSubMotivo = '';
            model.DescCacDac = $('#cboCACDAC option:selected').text();
            model.TotalInversion = $("#txtTotInversion").val();
            model.PagoAPADECE = SessionTransf.hidPagoAPADECE
            model.NameComplet = Session.DATACUSTOMER.NameCompleto;
            model.NroDoc = Session.DATACUSTOMER.Nro_Doc;
            model.RazonSocial = Session.DATACUSTOMER.BusinessName;
            model.RepresentLegal = Session.DATACUSTOMER.RepresentLegal;
            model.DNI_RUC = Session.DATACUSTOMER.DNI_RUC;
            model.TypeDoc = Session.DATACUSTOMER.TipoDocumento;
            model.TelefonoReferencia = Session.DATACUSTOMER.telefono_Referencial;
            model.CodTypeClient = Session.DATACUSTOMER.CustomerTypeCode;
            model.AdressDespatch = Session.DATACUSTOMER.Direccion_Despacho;
            model.Reference = Session.DATACUSTOMER.refencial;
            model.Departament_Fact = Session.DATACUSTOMER.Departament_Fact;
            model.District_Fac = Session.DATACUSTOMER.District_Fac;
            model.Pais_Fac = Session.DATACUSTOMER.Country_Fac;
            model.Provincia_Fac = Session.DATACUSTOMER.Provincia_Fac;
            model.ContractId = Session.DATACUSTOMER.ContratoID;
            model.TypeClient = Session.DATACUSTOMER.TipoCliente;
            model.CustomerId = Session.DATACUSTOMER.CustomerID;
            model.Cuenta = Session.DATACUSTOMER.Cuenta;
            model.Account = Session.DATACUSTOMER.Cuenta;
            model.Note = $("#txtNote").val();
            model.Code_Plane_Inst = Session.DATACUSTOMER.Codigo_Plano_Inst;
            model.CodePlanInst = Session.DATACUSTOMER.Codigo_Plano_Inst;
            model.CurrentUser = Session.USERACCESS.Login;
            model.Destinatarios = $("#txtEmail").val();
            model.fechaActual = SessionTransf.FechaActualServidor;
            model.Email = $("#txtEmail").val();

            if (controls.chkEmail[0].checked) {
                model.Flag_Email = 'true';
            } else {
                model.Flag_Email = 'false';
            }
            model.Sn = SessionTransf.hdnServName;
            model.IpServidor = SessionTransf.hdnLocalAdd;
            model.Telephone = Session.DATALINEA.NroCelular;
            model.PaqueteODeco = controls.strPaqueteDeco;

            //#region Proy-32650
            if (typeProc == 'NP') {

                //===========================PARAM===========================
                model.idContrato = Session.DATACUSTOMER.ContratoID; // codigo del contrato =>SP
                model.name = Session.DATACUSTOMER.Name; // nombre cliente
                model.LastName = Session.DATACUSTOMER.LastName; // nombre cliente
                model.BillingCycle = Session.DATACUSTOMER.Ciclo_Facturacion;
                model.emailUsuario = Session.DATACUSTOMER.Email;
                model.Msisdn = Session.NROTELEFONO;
                model.contactoCliente = Session.DATACUSTOMER.CustomerContact;
                model.fechaNac = Session.DATACUSTOMER.BirthDate;
                model.idLugarNac = Session.DATACUSTOMER.BirthPlaceID;
                model.sexo = Session.DATACUSTOMER.Sex;
                model.idEstadoCivil = Session.DATACUSTOMER.CivilStatusID;
                model.cargo = Session.DATACUSTOMER.Position;
                model.fax = Session.DATACUSTOMER.Fax;
                model.lugarNac = Session.DATACUSTOMER.BirthPlace;
                model.estadoCivil = Session.DATACUSTOMER.CivilStatus;
                model.planContract = Session.LINEA.Plan;
                model.CodTypeClient = Session.DATACUSTOMER.CustomerTypeCode;
                model.Modalidad = Session.CLIENTE.Modality;
                model.TypeDoc = Session.DATACUSTOMER.TipoDocumento;
                if (Session.DATACUSTOMER.Email == '' && (model.Flag_Email == 'true'))
                    model.updateDataMen = true;
                else
                    model.updateDataMen = false;

                if (SessionTransf.idAccion == strIdCargoFijo) {
                    model.RazonSocial = Session.CLIENTE.BusinessName;
                    model.idPorcentaje = $('#cboDiscountCF option:selected').val(); // id de porcentaje para cargo fijo =>SP
                    model.montoTotalSA = ''; // monto total calculado para servicios adicionales.(empty pa' cargo fijo) =>SP
                    model.mesVal = $('#cboMonthCF option:selected').val(); // cantidad de meses del descuento(valor del query) =>SP
                    model.mesDesc = $('#cboMonthCF option:selected').text(); // cantidad de meses del descuento(valor del query) =>SP
                    model.snCode = ''; // el snCode para cargo fijo (empty pa' cargo fijo) =>SP
                    model.costInst = '0'; // costo instalación con descuento.(empty pa' cargo fijo) =>SP
                    model.flagCargFijoServAdic = '0'; // 0 : Descuento cargo fijo
                    model.DiscountDescription = $('#cboDiscountCF option:selected').text();  // descripcion % descuento combo
                    model.flagServDeco = '0'; //NO MOVER si es Cargo fijo es 0
                    model.aplicaPromoFact = $('#chkPromFact').prop('checked'); // set Promoción a factura Vigente pendiente de pago (N/C)
                }
                else if (SessionTransf.idAccion == strIdServicioAdicional) {
                    model.RazonSocial = Session.CLIENTE.BusinessName;

                    SessionTransf.snCode = SessionTransf.DecodificatorSelected.SNCODE;
                    SessionTransf.codServAdic = SessionTransf.DecodificatorSelected.CO_SER;
                    SessionTransf.descServAdic = SessionTransf.DecodificatorSelected.DESCOSER;
                    SessionTransf.strTarifRegular = SessionTransf.DecodificatorSelected.COSTOPVU;
                    SessionTransf.strTarifRet = SessionTransf.DecodificatorSelected.CARGOFIJO;
                    SessionTransf.DE_SER = SessionTransf.DecodificatorSelected.DE_SER;
                    SessionTransf.strMontoDescuento = SessionTransf.DecodificatorSelected.COSTOPVU - SessionTransf.DecodificatorSelected.CARGOFIJO;
                    SessionTransf.strMontoDescuento = SessionTransf.strMontoDescuento - (SessionTransf.strMontoDescuento * parseFloat(SessionTransf.hdnValorIgv));

                    model.codServAdic = SessionTransf.codServAdic; // codigo del servicio seleccionado en la grilla
                    model.descServAdic = SessionTransf.descServAdic; // descripcion del servicio adicional seleccionado(grilla)
                    model.desServicioPVU = SessionTransf.DE_SER;//descServAdic; // descripcion del servicio adicional en PVU (no se muestra en la grilla(viene del select))
                    model.montTariRete = SessionTransf.strTarifRet;//  enviar la tarifa de retencion de la grilla seleccionada
                    model.mesVal = $('#cboMonthSA option:selected').val(); // 
                    model.mesDesc = $('#cboMonthSA option:selected').text(); // 
                    model.snCode = SessionTransf.snCode; // el snCode del servicio seleccionado en grilla
                    model.costoServiciosinIGV = parseFloat(SessionTransf.strTarifRet) / parseFloat(1 + parseFloat(SessionTransf.hdnValorIgv)); //SessionTransf.costoServiciosinIGV;  // falta el metodo que obtiene 
                    model.costoServicioconIGV = SessionTransf.strTarifRet;  // datos del rf3 - grilla ??  ---> ???--DUDA--???
                    model.flagCargFijoServAdic = '1'; // 1 : Descuento servicio adicional
                    model.DiscountDescription = $('#cboDiscountSA option:selected').text();  // descripcion % descuento combo
                    model.RetentionBonusServAdic = SessionTransf.strTarifRet;
                    model.RegularBonusServAdic = SessionTransf.strTarifRegular;
                    model.flagServDeco = '0'; //NO MOVER si es Serv Adic es 0
                    model.costInst = '0'; // costo instalación con descuento.(empty pa' cargo fijo) =>SP
                    model.MontoDescuento = SessionTransf.strMontoDescuento;
                }
                //--------------------------------------------------------------------------------------------
                model.CodigoAsesor = Session.ACCESO.login;
                model.NombreAsesor = Session.ACCESO.fullName;
                model.Transaction = 'Solicitud de Cancelacion'; //Transaccion:Fidelizacion o Solicitud de Cancelacion/ Ivan
                model.ReferenceOfTransaction = controls.rdbRetenido[0].checked == true ? 'Retenido' : 'Cancelado'; // RESULTADO
                model.Segmento = Session.DATACUSTOMER.Segmento; /*== 'Consumer' ? 'MASIVO' : 'CORPORATIVO';*/
                model.Reintegro = controls.txtPenalidad.val();
                model.Constancia = controls.rdbRetenido[0].checked == true ? 'Retención' : 'Cancelación';
                model.DateProgrammingSot = controls.txtDateCommitment.val();

                //Deco.
                if (SessionTransf.isDecodificator) {
                    model.flagCargFijoServAdic = '0';
                    model.flagServDeco = '1';
                    model.costInst = SessionTransf.nuevoCostoInstal;//costo para tipis y constancia
                    model.costoWSInst = SessionTransf.costoDescuentoInstal;//costo para registro de bonos y ws de hfc

                    model.vMotiveSot = $('#cboMotiveSOTAccion option:selected').text().substring(0, 40);
                    model.idPorcentaje = $('#cboDiscountSA option:selected').val();
                    var deco = {
                        id: "",
                        desc: SessionTransf.DecodificatorSelected.DESCOSER,
                        desc40: "",
                        tipodeco: SessionTransf.strTipoDeco,
                        costosing: 0,
                        costocigv: 0,
                        CodeService: SessionTransf.DecodificatorSelected.CODSERPVU,//para deco, va el codigo de servicio desde pvu
                        SnCode: SessionTransf.DecodificatorSelected.SNCODE,
                        Cf: (SessionTransf.DecodificatorSelected.CARGOFIJO == "0.00" ? '0' : SessionTransf.DecodificatorSelected.CARGOFIJO),
                        SpCode: SessionTransf.DecodificatorSelected.SPCODE,
                        ServiceName: SessionTransf.DecodificatorSelected.DE_SER,
                        ServiceType: SessionTransf.DecodificatorSelected.TIPO_SERVICIO,
                        ServiceGroup: SessionTransf.DecodificatorSelected.DE_GRP,
                        Equipment: "",
                        Quantity: "",
                        Associated: "",
                        CodeInsSrv: "",
                        SerialNumber: "",
                        Flag: "A"
                    };

                    var vModel =
                    {
                        Decos: [deco],
                        LoyaltyAmount: '0.00',
                        LoyaltyFlag: '0',
                        EtaValidation: AdditionalPointsModel.strValidateETA,
                        InsInteractionPlusModel: {
                            NameLegalRep: Session.CLIENTE.LegalAgent,
                            Basket: Session.LINEA.Plan,
                            Inter1: Session.CLIENTE.BillingCycle,
                            ClaroLdn1: Session.CLIENTE.DocumentNumber,
                            Inter3: Session.LINEA.ActivationDate,
                            Inter5: Session.LINEA.StateLine,
                            Inter7: Session.CLIENTE.Address,
                            Inter15: $("#ddlCACDAC option:selected").text(),
                            Inter16: Session.CLIENTE.LegalDepartament,
                            Inter17: Session.CLIENTE.LegalDistrict,
                            Inter18: Session.CLIENTE.LegalCountry,
                            Inter19: Session.CLIENTE.LegalProvince,
                            Inter20: Session.CLIENTE.CodeCenterPopulate,
                            Inter21: 1,//cantidad
                            Inter22: that.getRoundDecimal(that.objLteUninstallInstallDeco.CostoAdicional),
                            Inter23: that.getRoundDecimal(that.objLteUninstallInstallDeco.TotalAmountcIgv),
                            Inter24: that.getRoundDecimal(that.objLteUninstallInstallDeco.CostoAdicionalCIGV),
                            ClaroLdn2: (controls.chkEmail[0].checked ? '1' : '0'),
                            Email: (controls.chkEmail[0].checked ? $("#txtEmail").val() : ''),//agregar email que se escribe en el texto
                            ClaroLdn4: '1',
                            ClaroLocal1: model.costInst,//'0.00',
                            ClaroLocal2: '1',
                            Inter25: parseFloat((1).toFixed(2)),//parseFloat((that.objLteUninstallInstallDeco.igv).toFixed(2)),
                            Inter29: "",
                            Inter6: "0",
                            District: Session.CLIENTE.LegalDistrict,
                            Inter30: controls.txtNote.val(),
                            FirstName: Session.CLIENTE.Name,
                            LastName: Session.CLIENTE.LastName,
                            DocumentNumber: Session.CLIENTE.DocumentNumber,
                            RegistrationReason: Session.CLIENTE.ContractID,
                            ClaroNumber: Session.CLIENTE.ContractID,
                            TypeDocument: Session.CLIENTE.DocumentType,
                            Address: Session.CLIENTE.Address,
                            City: Session.CLIENTE.InstallUbigeo,
                            Reason: Session.CLIENTE.BusinessName,
                            Position: controls.txtDateCommitmentAccion.val(),
                            Address: Session.CLIENTE.Reference
                        },
                        SotPending: {
                            StrCoId: Session.CLIENTE.ContractID,
                            StrTipTra: controls.cboTypeWorkAccion.val()
                        },
                        AuditRegister: {
                            strNombreCliente: Session.CLIENTE.FullName,
                            strTelefono: Session.CLIENTE.CustomerID
                        },
                        RegistrarProcesoPostventa: {
                            PiCodId: Session.CLIENTE.ContractID,
                            PiCustomerId: Session.CLIENTE.CustomerID,
                            PiCodplano: Session.CLIENTE.CodeCenterPopulate,
                            PiObservacion: controls.txtNote.val(),
                            PiFecProg: controls.txtDateCommitmentAccion.val(),
                            PiFranjaHor: '',
                            PiTiptra: controls.cboTypeWorkAccion.val(),
                            PiTmcode: that.objLteUninstallInstallDeco.TmCode,
                            PiCodmotot: that.objLteUninstallInstallDeco.MotSotCode,
                            PiTipoProducto: JSON.parse(sessionStorage.getItem("SessionTransac")).UrlParams.SUREDIRECT
                        },
                        ImplementLoyalty: {
                            CustomerId: Session.CLIENTE.CustomerID,
                            DireccionFacturacion: Session.CLIENTE.InvoiceAddress,
                            NotasDireccion: Session.CLIENTE.InvoiceUrbanization,
                            Distrito: Session.CLIENTE.InvoiceDistrict,
                            Provincia: Session.CLIENTE.InvoiceProvince,
                            CodigoPostal: Session.CLIENTE.LegalPostal,
                            Departamento: Session.CLIENTE.InvoiceDepartament,
                            Pais: Session.CLIENTE.InvoiceCountry
                        },
                        ImplementOcc: {
                            CustomerId: Session.CLIENTE.CustomerID,
                            Monto: that.objLteUninstallInstallDeco.LoyaltyAmount,
                            FlagCobroOcc: that.objLteUninstallInstallDeco.LoyaltyFlag
                        },

                        InteractionModel: {
                            Tipo: that.objLteUninstallInstallDeco.Typification.Type,
                            Clase: that.objLteUninstallInstallDeco.Typification.Class,
                            Subclase: that.objLteUninstallInstallDeco.Typification.SubClass,
                            Agente: Session.ACCESO.login,
                            AgenteName: Session.ACCESO.fullName,
                            Notas: controls.txtNote.val(),
                        }
                    };
                    if (AdditionalPointsModel.strValidateETA != "0") {
                        vModel.RegistrarProcesoPostventa.PiFranjaHor = controls.cboScheduleAccion[0].selectedOptions[0].text;
                        vModel.RegistrarEtaSeleccion = {};
                        vModel.RegistrarEtaSeleccion.IdConsulta = Session.RequestActId;
                        vModel.RegistrarEtaSeleccion.IdInteraccion = ("0000000000" + Session.RequestActId).slice(-10);
                        vModel.RegistrarEtaSeleccion.FechaCompromiso = controls.txtDateCommitmentAccion.val();
                        vModel.RegistrarEtaSeleccion.Franja = controls.cboScheduleAccion.val().split('+')[0];
                        vModel.RegistrarEtaSeleccion.Idbucket = controls.cboScheduleAccion.val().split('+')[1];

                        vModel.RegistrarEta = {};
                        vModel.RegistrarEta.IdPoblado = Session.CLIENTE.CodeCenterPopulate;
                        vModel.RegistrarEta.DniTecnico = '';
                        vModel.RegistrarEta.Franja = controls.cboScheduleAccion.val().split('+')[0];
                        vModel.RegistrarEta.Idbucket = controls.cboScheduleAccion.val().split('+')[1];
                        vModel.RegistrarEta.IpCreacion = '';
                        vModel.RegistrarEta.SubTipoOrden = controls.cboSubTypeWorkAccion.val().split('|')[0];
                        vModel.RegistrarEta.UsrCrea = Session.ACCESO.login;
                        vModel.RegistrarEta.FechaProg = controls.txtDateCommitmentAccion.val();
                    }
                    model = { oModel: model, objViewModel: vModel };

                    strUrl = '/Transactions/LTE/RetentionCancelServices/SaveTransactionRetentionDeco';
                } else {
                    strUrl = '/Transactions/LTE/RetentionCancelServices/SaveTransactionRetentionCFSA';
                }
            }
            else
                strUrl = '/Transactions/LTE/RetentionCancelServices/SaveTransactionRetention';
            //#endregion

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(model),
                url: strUrl,
                error: function (data) {

                    alert(SessionTransf.strMensajeDeError, "Alerta");
                    $("#btnGuardar").attr('disabled', true);
                    $("#btnAnteriorF").attr('disabled', true);
                    $("#btnConstancia").attr('disabled', true);  //Desactiva
                },
                success: function (response) {
                    SessionTransf.vDesInteraction = response.vDesInteraction;
                    SessionTransf.InteractionId = response.vInteractionId;
                    SessionTransf.RutaArchivo = response.strRutaArchivo;
                    SessionTransf.MensajeEmail = response.MensajeEmail;

                    if (SessionTransf.InteractionId != '' && SessionTransf.InteractionId != "null") {

                        $("#btnConstancia").attr('disabled', false); //Activa
                        alert(SessionTransf.vDesInteraction, "Informativo");


                    } else {

                        alert(response.errorMessage != '' ? response.errorMessage : SessionTransf.strMensajeDeError, "Alerta");
                        $("#btnConstancia").attr('disabled', true);  //Desactiva
                    }

                    that.BlockControl();
                    $("#btnAnteriorF").attr('disabled', true);
                    $("#btnGuardar").attr('disabled', true);

                }
            });
        },

        SaveTransactionNoRetention: function () {
            var that = this,
                controls = that.getControls(),
                oCustomer = Session.DATACUSTOMER,
                objRegister = {},
                objSession = {};

            objRegister.CicloFacturacion = Session.DATACUSTOMER.BillingCycle;
            objRegister.IdSession = Session.IDSESSION;
            //
            objRegister.IdSession = Session.IDSESSION;
            objRegister.CustomerId = Session.DATACUSTOMER.CustomerID;
            objRegister.AreaPCs = NumeracionMENOSUNO;
            objRegister.DescCacDac = $('#cboCACDAC option:selected').text();
            objRegister.BillingCycle = Session.DATACUSTOMER.Ciclo_Facturacion;
            objRegister.ContractId = Session.DATACUSTOMER.ContratoID;
            objRegister.Account = Session.DATACUSTOMER.Cuenta;
            objRegister.Cuenta = Session.DATACUSTOMER.Cuenta;
            objRegister.CodigoInteraction = '';
            objRegister.CodMotiveSot = $('#cboMotiveSOT option:selected').val();
            objRegister.fechaActual = SessionTransf.FechaActualServidor;
            objRegister.FechaProgramacion = controls.txtDateCommitment.val();
            objRegister.DateProgrammingSot = controls.txtDateCommitment.val();
            objRegister.FlagNdPcs = NumeracionCERO;
            objRegister.TotalInversion = $("#txtTotInversion").val();
            objRegister.Email = $("#txtEmail").val();
            objRegister.MontoFidelizacion = NumeracionCERO;
            objRegister.MontoPCs = NumeracionCERO;
            objRegister.montoPenalidad = NumeracionMENOSUNO;
            objRegister.MotivePCS = NumeracionMENOSUNO;
            objRegister.Msisdn = Session.NROTELEFONO;
            objRegister.DocumentNumber = Session.DATACUSTOMER.Nro_Doc;
            objRegister.Observation = $("#txtNote").val();
            objRegister.SubMotivePCS = NumeracionMENOSUNO;
            objRegister.TypeClient = Session.DATACUSTOMER.TipoCliente;
            objRegister.TypeWork = controls.cboTypeWork.val();
            objRegister.Trace = NumeracionUNO;
            objRegister.CurrentUser = Session.USERACCESS.Login;
            objRegister.Reintegro = $("#txtPenalidad").val();
            objRegister.DesMotivos = $('#cboMotCancelacion option:selected').text();
            objRegister.DesAccion = $('#cboAccion option:selected').text();
            objRegister.hidSupJef = SessionTransf.hidSupJef;
            objRegister.NroCelular = Session.DATALINEA.NroCelular;
            objRegister.Accion = SessionTransf.hidAccionTra;
            if (controls.cboSubMotive.val() != '-1') {
                objRegister.DesSubMotivo = $('#cboSubMotive option:selected').text();
            }
            else {
                objRegister.DesSubMotivo = '';
            }
            objRegister.PagoAPADECE = SessionTransf.hidPagoAPADECE
            objRegister.NameComplet = Session.DATACUSTOMER.NameCompleto;
            objRegister.RazonSocial = Session.DATACUSTOMER.BusinessName;
            objRegister.RepresentLegal = Session.DATACUSTOMER.RepresentLegal;
            objRegister.DNI_RUC = Session.DATACUSTOMER.DNI_RUC;
            objRegister.TypeDoc = Session.DATACUSTOMER.TipoDocumento;
            objRegister.TelefonoReferencia = Session.DATACUSTOMER.telefono_Referencial;
            objRegister.CodTypeClient = Session.DATACUSTOMER.CustomerTypeCode;
            objRegister.AdressDespatch = Session.DATACUSTOMER.Direccion_Despacho;
            objRegister.Reference = Session.DATACUSTOMER.refencial;
            objRegister.Departament_Fact = Session.DATACUSTOMER.Departament_Fact;
            objRegister.District_Fac = Session.DATACUSTOMER.District_Fac;
            objRegister.Pais_Fac = Session.DATACUSTOMER.Country_Fac;
            objRegister.Provincia_Fac = Session.DATACUSTOMER.Provincia_Fac;
            objRegister.CodePlanInst = Session.DATACUSTOMER.Codigo_Plano_Inst;
            objRegister.Sn = SessionTransf.hdnServName;
            objRegister.IpServidor = SessionTransf.hdnLocalAdd;
            objRegister.Telephone = Session.DATALINEA.NroCelular;
            objRegister.CurrentUser = Session.USERACCESS.Login;
            objRegister.TotInversion = $("#txtTotInversion").val();
            objRegister.vMotiveSot = controls.cboMotiveSOT.val();
            if (controls.rdbAplica.prop('checked')) {
                
                objRegister.Aplica = 'Si'
            } else {
                objRegister.Aplica = 'No'
            }


            ////INI////

            objRegister.AdressDespatch = Session.DATACUSTOMER.Direccion_Despacho;
            objRegister.Transaction = 'Solicitud de Cancelacion'; //Transaccion:Fidelizacion o Solicitud de Cancelacion/ Ivan
            objRegister.Constancia = controls.rdbRetenido[0].checked == true ? 'Retención' : 'Cancelación';
            objRegister.ReferenceOfTransaction = controls.rdbRetenido[0].checked == true ? 'Retenido' : 'Cancelado'; // RESULTADO
            objRegister.DateProgrammingSot = controls.txtDateCommitment.val(); //fecha de cancelacion
            objRegister.Segmento = Session.DATACUSTOMER.Segmento; /*== 'Consumer' ? 'MASIVO' : 'CORPORATIVO';*/
            objRegister.Accion = SessionTransf.hidAccionTra;
            objRegister.IdAccion = SessionTransf.idAccion; //le mandamos LA ACCION SELECCIONADA.
            objRegister.costInst = '0'; //por minetras le pongo este valor. en NO RETENIDO, ESTE VALOR NO SE MUESTRA EN LA CONSTANCIA
            objRegister.RegularBonusServAdic = '0';

            //if (controls.rdbAplica.prop('checked')) {
            //    objRegister.vMotiveSot = $('#cboMotCancelacion option:selected').text();
            //}
            if (SessionTransf.idAccion == strIdCargoFijo) {
                objRegister.DiscountDescription = $('#cboDiscountCF option:selected').text();  // descripcion % descuento combo
                objRegister.mesDesc = $('#cboMonthCF option:selected').text(); // cantidad de meses del descuento(valor del query) =>SP
                objRegister.Email = controls.txtEmail.val();
            }
            else if (SessionTransf.idAccion == strIdServicioAdicional) {
                objRegister.DiscountDescription = '';
                objRegister.mesDesc = $('#cboMonthSA option:selected').text(); // 
                objRegister.descServAdic = SessionTransf.descServAdic; // descripcion del servicio adicional seleccionado(grilla)
                objRegister.RegularBonusServAdic = SessionTransf.strTarifRegular;
                objRegister.Email = controls.txtEmail.val();
            }

            if (SessionTransf.isDecodificator) {
                objRegister.costInst = SessionTransf.nuevoCostoInstal;//costo para tipis y constancia
            }

            objRegister.PaqueteODeco = controls.strPaqueteDeco;
            ////FIN////

            objRegister.Telephone = Session.CLIENTE.CustomerID;
            objRegister.CodigoAsesor = Session.ACCESO.login;
            objRegister.NombreAsesor = Session.ACCESO.fullName;

            objRegister.Destinatarios = $("#txtEmail").val();

            objRegister.Note = $("#txtNote").val();

            if (controls.chkEmail[0].checked) {
                objRegister.Flag_Email = 'true';
            } else {
                objRegister.Flag_Email = 'false';
            }

            objRegister.FechaCompromiso = controls.txtDateCommitment.val();
            objRegister.vSchedule = controls.cboSchedule.val();
            objRegister.Segmento = Session.DATACUSTOMER.Segmento === 'Consumer' ? 'MASIVO' : 'CORPORATIVO';

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objRegister),
                url: '/Transactions/LTE/RetentionCancelServices/SaveTransactionNoRetention',
                success: function (response) {

                    SessionTransf.MensajeEmail = response.data.Description;
                    SessionTransf.InteractionId = response.data.Condition;
                    SessionTransf.Message = response.data.Message;
                    SessionTransf.RutaArchivo = response.data.Description2;

                    if (response.data.Code == 'TRUE') {
                        $("#btnConstancia").attr('disabled', false); //Activa
                        alert(SessionTransf.strMsgTranGrabSatis, "Informativo");
                    }
                    else {
                        alert("No se pudo Programar la Cancelación de la Línea", "Alerta");
                        $("#btnConstancia").attr('disabled', true);  //Desactiva
                    }

                    that.BlockControl();
                    $("#btnAnteriorF").attr('disabled', true);
                    $("#btnGuardar").attr('disabled', true);

                },
                error: function () {
                    alert(SessionTransf.strMensajeDeError, "Alerta");
                    $("#btnGuardar").attr('disabled', true);
                    $("#btnAnteriorF").attr('disabled', true);
                    $("#btnConstancia").attr('disabled', true);  //Desactiva
                }
            });
        },

        InitAccion: function () {

            var that = this,
           controls = that.getControls(),
           objLstAccionType = {};

            objLstAccionType.strIdSession = Session.IDSESSION;

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objLstAccionType),
                url: '/Transactions/CommonServices/GetListarAccionesRC',  //Proy-32650
                success: function (response) {
                    controls.cboAccion.append($('<option>', { value: '', html: 'Seleccionar' }));
                    if (response.data != null) {
                        $.each(response.data, function (index, value) {
                            controls.cboAccion.append($('<option>', { value: value.Code, html: value.Description }));
                        });
                    }
                }
            });

        },

        InitCacDat: function () {

            var that = this,
                controls = that.getControls(),
                objCacDacType = {},
                parameters = {};

            objCacDacType.strIdSession = Session.IDSESSION;

            parameters.strIdSession = Session.IDSESSION;
            parameters.strCodeUser = Session.USERACCESS.Login;

            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(parameters),
                url: '/Transactions/CommonServices/GetUsers',
                success: function (results) {
                    var cacdac = results.Cac;
                    $.ajax({
                        type: 'POST',
                        contentType: "application/json; charset=utf-8",
                        dataType: 'json',
                        data: JSON.stringify(objCacDacType),
                        url: '/Transactions/CommonServices/GetCacDacType',
                        success: function (response) {
                            controls.cboCACDAC.append($('<option>', { value: '', html: 'Seleccionar' }));
                            if (response.data != null) { }
                            var itemSelect;
                            $.each(response.data.CacDacTypes, function (index, value) {

                                if (cacdac === value.Description) {
                                    controls.cboCACDAC.append($('<option>', { value: value.Code, html: value.Description }));
                                    itemSelect = value.Code;

                                } else {
                                    controls.cboCACDAC.append($('<option>', { value: value.Code, html: value.Description }));
                                }
                            });
                            if (itemSelect != null && itemSelect.toString != "undefined") {
                                $("#cboCACDAC option[value=" + itemSelect + "]").attr("selected", true);
                            }
                        }
                    });
                }
            });
        },

        InitMotCancel: function () {
            var that = this,
            controls = that.getControls(),
           objLstAccionType = {};

            objLstAccionType.strIdSession = Session.IDSESSION;

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objLstAccionType),
                url: '/Transactions/LTE/RetentionCancelServices/GetMotCancelacion',
                success: function (response) {
                    controls.cboMotCancelacion.append($('<option>', { value: '-1', html: 'Seleccionar' }));
                    if (response.data != null) {
                        $.each(response.data, function (index, value) {
                            controls.cboMotCancelacion.append($('<option>', { value: value.Code, html: value.Description }));
                        });
                    }
                }
            });
        },

        InitTypeWork: function () {
            var that = this,
           controls = that.getControls(),
           objLstTypeWork = {};

            objLstTypeWork.strIdSession = Session.IDSESSION;
            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objLstTypeWork),
                url: '/Transactions/LTE/RetentionCancelServices/GetTypeWork',
                success: function (response) {
                    controls.cboTypeWork.append($('<option>', { value: '-1', html: 'Seleccionar' }));
                    if (response.data != null) {
                        $.each(response.data, function (index, value) {
                            controls.cboTypeWork.append($('<option>', { value: value.Code, html: value.Description }));
                        });

                    }
                }
            });

        },

        InitMotiveSOT: function () {
            //
            var that = this,
           controls = that.getControls(),
           objLstMotiveSotType = {};

            objLstMotiveSotType.strIdSession = Session.IDSESSION;
            objLstMotiveSotType.vIdTypeWork = controls.cboTypeWork.val();

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objLstMotiveSotType),
                url: '/Transactions/LTE/RetentionCancelServices/GetMotive_SOT',
                success: function (response) {

                    controls.cboMotiveSOT.html("");
                    controls.cboMotiveSOT.append($('<option>', { value: '-1', html: 'Seleccionar' }));
                    if (response.data != null) {
                        $.each(response.data, function (index, value) {
                            controls.cboMotiveSOT.append($('<option>', { value: value.Code, html: value.Description }));
                        });

                        controls.cboMotiveSOT.val(SessionTransf.strValueMotivoSOTDefecto);
                        controls.cboTypeWork.val(SessionTransf.strValueTipoTrabajoDefecto);
                    }
                },
                error: function (XError) {
                },
            });
        },

        getTypification: function () {
            var that = this, controls = that.getControls();
            var transact = gConstCodigoTransRetCanServLTE;
            $.app.ajax({
                type: "POST",
                url: "/Transactions/CommonServices/GetTypification",
                data: {
                    strIdSession: Session.IDSESSION,
                    strTransactionName: transact
                },
                success: function (result) {
                    var list = result.ListTypification;
                    if (result.ListTypification.length > 0) {
                        that.getBusinessRules(list[0].SUBCLASE_CODE);
                    }
                }
            });
        },

        getBusinessRules: function (SubClaseCode) {
            var that = this, controls = that.getControls();
            controls.divReglas.empty().html('');
            $.app.ajax({
                type: "POST",
                url: "/Transactions/CommonServices/GetBusinessRules",
                data: {
                    strIdSession: Session.IDSESSION,
                    strSubClase: SubClaseCode
                },
                success: function (result) {
                    if (result.data.ListBusinessRules != null) {
                        var list = result.data.ListBusinessRules;
                        if (list.length > 0) {
                            controls.divReglas.html(list[0].REGLA);
                        }
                    }

                }
            });
        },

        ShowConstancy: function (filepath, Filename) {
            var that = this,
               controls = that.getControls();

            var url = that.strUrl + '/GenerateRecord/ExistFile';

            $.app.ajax({
                type: 'GET',
                cache: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'JSON',
                url: url,
                data: { strFilePath: filepath, strFileName: Filename, strIdSession: Session.IDSESSION },
                success: function (result) {
                    if (result.Exist == false) {
                        alert('No existe el Archivo.', "Alerta");
                    } else {
                        var url = that.strUrl + '/GenerateRecord/showInvoice';
                        window.open(url + "?strFilePath=" + Session.filepath + "&strFileName=" + Session.Filename + "&strNameForm=" + "NO" + "&strIdSession=" + Session.IDSESSION, "FACTURA ELECTRÓNICA", "");
                    }

                },
                error: function (ex) {
                    alert('No existe el Archivo.', "Alerta");
                }
            });
        },

        Round: function (cantidad, decimales) {
            var cantidad = parseFloat(cantidad);
            var decimales = parseFloat(decimales);
            decimales = (!decimales ? 2 : decimales);
            return Math.round(cantidad * Math.pow(10, decimales)) / Math.pow(10, decimales);
        },

        btnSummaryDT_click: function (e) {

            var that = this,
            control = that.getControls();

            if (control.rdbNoRetenido[0].checked || control.rdbRetenido[0].checked) {



            } else {
                alert(SessionTransf.gConstMsgSelTr, "Alerta");
                return false;
            }

            if ($.isNumeric(control.txtPenalidad.val())) {
                control.txtPenalidad.val(parseFloat(control.txtPenalidad.val()).toFixed(2));

            } else {
                alert('Ingresar Penalidad', "Alerta");
                return false;
            }
            navigateTabs(e);

        },

        btnSummaryRetenido_click: function (e) {
            var that = this,
            control = that.getControls();

            if (control.cboMotCancelacion.val() == "-1" || control.cboMotCancelacion.val() == null) {
                alert('Seleccionar motivo de cancelación.', "Alerta");
                return false;
            }


            if ($('#divSubMotive').css('display') != 'none') {

                if (control.cboSubMotive.val() == "-1" || control.cboSubMotive.val() == null) {
                    alert('Seleccionar sub motivo.', "Alerta");
                    return false;
                }
            }

            if ($.isNumeric(control.txtTotInversion.val())) {
                control.txtTotInversion.val(parseFloat(control.txtTotInversion.val()).toFixed(2));
            } else {
                alert('Ingresar total de inversión.', "Alerta");
                return false;
            }

            if (control.rdbNoRetenido[0].checked) {

                if (control.cboTypeWork.val() == "-1" || control.cboTypeWork.val() == null) {
                    alert('Seleccionar tipo de trabajo.', "Alerta");
                    return false;
                }


                if (control.cboMotiveSOT.val() == "-1" || control.cboMotiveSOT.val() == null) {
                    alert("Seleccionar motivo SOT.", "Alerta");
                    return false;

                }

            }

            navigateTabs(e);

        },

        btnSummaryAccion_click: function (e) {
            var that = this,
            control = that.getControls();

            var validaRetencion = true;
            if ($('#cboAccion option:selected').html() == "Seleccionar" || control.cboAccion.val() == null) {
                alert(SessionTransf.gConstMsgSelAc, "Alerta");
                return false;
            }


            if ($("#chkEmail").prop("checked")) {
                if ($("#txtEmail").val() == "") {
                    alert('Ingresar Email', 'Alerta', function () {
                        control.txtEmail.focus();
                    }); return false;
                }

                var regx = /^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$/;
                var blvalidar = regx.test($("#txtEmail").val());
                if (!blvalidar) {
                    alert('Ingresar Email Válido', 'Alerta', function () {
                        control.txtEmail.select();
                    }); return false;
                }
            }

            if ($('#cboCACDAC option:selected').html() == "Seleccionar" || control.cboCACDAC.val() == null) {
                alert(SessionTransf.gConstMsgSelCacDac, "Alerta");
                return false;
            }


            //#region Proy-32650
            /*validacion para que muestre el mensaje que ya cuenta con bono existente*/
            if ((SessionTransf.idAccion == strIdCargoFijo) || (SessionTransf.idAccion == strIdServicioAdicional)) {
                if ((strValidaDescuentoActivo == '') || (strValidaDescuentoActivo == undefined)) {
                } else {

                    if (SessionTransf.hidAccionTra == 'R') {
                        alert(strValidaDescuentoActivo);
                        validaRetencion = false;
                        return false;
                    } else {
                    }

                }
            }

            if (SessionTransf.idAccion == strIdCargoFijo) {
                if ($('#cboDiscountCF option:selected').html() == "Seleccionar" || control.cboDiscountCF.val() == null) {
                    alert("Seleccionar porcentaje de descuento.", "Alerta");
                    validaRetencion = false;
                    return false;
                }
                if ($('#cboMonthCF option:selected').html() == "Seleccionar" || control.cboMonthCF.val() == null) {
                    alert("Seleccionar meses a aplicar.", "Alerta");
                    validaRetencion = false;
                    return false;
                }

            }
            else if (SessionTransf.idAccion == strIdServicioAdicional) {
                var value = $('#cboAccion option:selected').val();
                var arrCod = value.split('|');
                var rowAdiServ = $('#tblAdiServBody').DataTable().rows({ selected: true }).data();

                if ($('#cboMonthSA option:selected').html() == "Seleccionar" || control.cboMonthSA.val() == null) {
                    alert("Seleccionar meses a aplicar.", "Alerta");
                    validaRetencion = false;
                    return false;
                }
                if (arrCod[0] == strIdServicioAdicional && rowAdiServ[0] == undefined) {
                    alert("Debe Seleccionar un Servicio Adicional", "Alerta");
                    validaRetencion = false;
                    return false;
                }
                var strValidarTipDeco = strValidaDecos.split('|');

                if (arrCod[0] == strIdServicioAdicional && SessionTransf.DecodificatorSelected.CODGRUPOSERV == strValidarTipDeco[2] && SessionTransf.DecodificatorSelected.TIPO_SERVICIO == strValidarTipDeco[0]) {

                    if ($('#txtCostInst').text() > 0) {
                        if ($('#cboDiscountSA option:selected').html() == "Seleccionar" || control.cboDiscountSA.val() == null) {
                            alert("Seleccionar porcentaje de descuento.", "Alerta");
                            validaRetencion = false;
                            return false;
                        }
                    }
                    if ($('#cboTypeWorkAccion option:selected').html() == "Seleccionar" || control.cboTypeWorkAccion.val() == null) {
                        alert("Seleccionar Tipo de Trabajo.", "Alerta");
                        validaRetencion = false;
                        return false;
                    }

                    if ($('#cboSubTypeWorkAccion').prop("disabled") == false) {
                        if ($('#cboSubTypeWorkAccion option:selected').html() == "Seleccionar" || control.cboSubTypeWorkAccion.val() == null) {
                            alert("Seleccionar SubTipo de Trabajo.", "Alerta");
                            validaRetencion = false;
                            return false;
                        }
                    }

                    if ($('#cboScheduleAccion option:selected').html() == "Seleccionar" || control.cboScheduleAccion.val() == null) {
                        alert("Seleccionar Horario(*).", "Alerta");
                        validaRetencion = false;
                        return false;
                    }
                }
                //Validación de cantidad de Decos
                if (SessionTransf.isDecodificator) {

                    if (AdditionalPointsModel.free == 0) {
                        alert(SessionTransf.strMsjCantidadLimiteDecos.replace('_num_', that.objLteUninstallInstallDeco.CantPuntosMax), "Alerta");
                        AdditionalPointsModel.free = AdditionalPointsModel.freeTemp;
                        return false;
                    } else {
                        var txtTipo = '';
                        if (SessionTransf.DecodificatorSelected.DESCOSER.indexOf('HD') !== -1) {
                            txtTipo = "HD";
                        } else if (SessionTransf.DecodificatorSelected.DESCOSER.indexOf('SD') !== -1) {
                            txtTipo = "SD";
                        } else {
                            txtTipo = "DVR";
                        }
                        SessionTransf.strTipoDeco = txtTipo;
                        console.log(SessionTransf.strTipoDeco);
                        var peso = that.getWeightDeco(txtTipo);

                        AdditionalPointsModel.free = AdditionalPointsModel.free - peso;

                        if (AdditionalPointsModel.free >= 0) {

                        } else {
                            alert(SessionTransf.strMsjCantidadLimiteDecos.replace('_num_', that.objLteUninstallInstallDeco.CantPuntosMax), "Alerta");
                            AdditionalPointsModel.free = AdditionalPointsModel.freeTemp;
                            return false;
                        }
                    }
                }

                //#region PROY-32650  II - Retención/Fidelización

                SessionTransf.snCode = SessionTransf.DecodificatorSelected.SNCODE;
                SessionTransf.codServAdic = SessionTransf.DecodificatorSelected.CO_SER;
                SessionTransf.descServAdic = SessionTransf.DecodificatorSelected.DESCOSER;
                SessionTransf.strTarifRegular = SessionTransf.DecodificatorSelected.COSTOPVU;
                SessionTransf.strTarifRet = SessionTransf.DecodificatorSelected.CARGOFIJO;
                SessionTransf.DE_SER = SessionTransf.DecodificatorSelected.DE_SER;
                SessionTransf.strMontoDescuento = SessionTransf.DecodificatorSelected.COSTOPVU - SessionTransf.DecodificatorSelected.CARGOFIJO;//TarifaRetencion;
            }
            //#endregion
            if (validaRetencion) {
                that.SummaryRetenido();
                that.SummaryMotivoCancelacion();
                that.SummaryTotalInversion();
                that.SummarySubMotivo();
                that.SummaryTipoTrabajo();
                that.SummaryMotivoSot();
                that.SummaryAplicaCaso();
                that.SummaryAccion();
                that.SummaryCorreo();
                that.SummaryPuntoVenta();
                that.SummaryNota();
                //#region Proy-32650
                that.SummaryDescuento();
                that.SummaryCosto();
                that.SummaryTotalDescuento();
                that.SummaryVigencia();
                //Fase 2
                if (SessionTransf.isDecodificator) {
                    $(".Deco_Accion").css("display", "table-row");
                    that.SummaryTipoTrabajoAccion();
                    that.SummaryMotivoSotAccion()
                    that.SummarySubTipoTrabajoAccion();
                } else {
                    $(".Deco_Accion").css("display", "none");
                }


                //#endregion
                if ($(e).attr('id') == 'btnSummary02') {
                    $('.btn-circle.transaction-button').removeClass('transaction-button').addClass('btn-default');
                    $(e).addClass('transaction-button').removeClass('btn-default').blur();
                    var percent = $(e).attr('percent');
                    document.getElementById('prog').style.width = percent;
                } else {
                    navigateTabs(e);
                }
            }
        },

        SummaryRetenido: function () {
            var that = this,
                controls = that.getControls();

            if (controls.rdbRetenido[0].checked == true) {
                Smmry.set('Penalidad', controls.txtPenalidad.val());
                Smmry.set('Retenido', 'Retenido');
            }
            else {
                Smmry.set('Penalidad', controls.txtPenalidad.val());
                Smmry.set('Retenido', 'No Retenido');
            }
        },

        SummaryMotivoCancelacion: function () {
            var that = this,
                controls = that.getControls();

            if (controls.cboMotCancelacion.val() != "-1") {
                Smmry.set('MotivoCancelacion', $('#cboMotCancelacion option:selected').html());
            }
            else {
                Smmry.set('MotivoCancelacion', '');
            }
        },

        SummaryTotalInversion: function () {
            var that = this,
                controls = that.getControls();

            if ($.trim(controls.txtTotInversion.val()) != "") {
                Smmry.set('TotalInversion', controls.txtTotInversion.val());
            }
            else {
                Smmry.set('TotalInversion', '');
            }
        },

        SummarySubMotivo: function () {
            var that = this,
                controls = that.getControls();

            if (controls.cboSubMotive.val() != "-1" && controls.cboSubMotive.val() != null) {
                Smmry.set('SubMotivo', $('#cboSubMotive option:selected').html());
            }
            else {
                Smmry.set('SubMotivo', '');
            }
        },

        SummaryTipoTrabajo: function () {
            var that = this,
                controls = that.getControls();

            if (controls.cboTypeWork.val() != "-1") {
                Smmry.set('TipoTrabajo', $('#cboTypeWork option:selected').html());
            }
            else {
                Smmry.set('TipoTrabajo', '');
            }
        },

        SummaryMotivoSot: function () {
            var that = this,
                controls = that.getControls();

            if (controls.cboMotiveSOT.val() != "-1") {
                Smmry.set('MotivoSot', $('#cboMotiveSOT option:selected').html());
            }
            else {
                Smmry.set('MotivoSot', '');
            }
        },

        SummaryAplicaCaso: function () {
            var that = this,
                controls = that.getControls();

            if (controls.rdbAplica[0].checked == true) {
                Smmry.set('AplicaCaso', 'SI');
            }
            else {
                Smmry.set('AplicaCaso', 'NO');
            }
        },

        SummaryAccion: function () {
            var that = this,
                controls = that.getControls();

            if (controls.cboAccion.val() != "") {
                Smmry.set('Accion', $('#cboAccion option:selected').html());
            }
            else {
                Smmry.set('Accion', '');
            }
        },
        SummaryCorreo: function () {
            var that = this,
                controls = that.getControls();

            if (controls.chkEmail[0].checked == true) {
                Smmry.set('Correo', controls.txtEmail.val());
            }
            else {
                Smmry.set('Correo', '');
            }
        },
        SummaryPuntoVenta: function () {
            var that = this,
                controls = that.getControls();

            if (controls.cboCACDAC.val() != "") {
                Smmry.set('PuntoVenta', $('#cboCACDAC option:selected').html());
            }
            else {
                Smmry.set('PuntoVenta', '');
            }
        },
        SummaryNota: function () {
            var that = this,
                controls = that.getControls();

            if ($.trim(controls.txtNote.val()) != "") {
                var Notas = controls.txtNote.val();
                var array = Notas.split(" ");

                var strFinal = "";
                array.forEach(function (item) {

                    if (item.length > 60) {
                        var cant = item.length;
                        var div = cant / 60;

                        for (var i = 1; i <= div; i++) {

                            var str = item.substring((60 * i) - 60, 60 * i);
                            strFinal = strFinal + str + " ";

                        }
                    }
                    else {
                        strFinal = strFinal + item + " ";
                    }

                });

                Smmry.set('Nota', strFinal);

            }
            else {
                Smmry.set('Nota', '');
            }
        },

        //#region Proy-32650
        SummaryVigencia: function () {
            var that = this,
                controls = that.getControls();

            if (controls.cboMonthCF.val() != '-1' || controls.cboMonthCF.val() != '-1') {
                if (SessionTransf.idAccion == strIdCargoFijo) {
                    Smmry.set('Vigencia', $('#cboMonthCF option:selected').html());
                } else if (SessionTransf.idAccion == strIdServicioAdicional) {
                    Smmry.set('Vigencia', $('#cboMonthSA option:selected').html());
                }
            }
            else {
                Smmry.set('Vigencia', '');
            }

        },
        SummaryDescuento: function () {
            var that = this,
                controls = that.getControls();

            if (SessionTransf.idAccion == strIdCargoFijo) {
                $('#lblDesc').text('Descuento');
                Smmry.set('Descuento', $('#cboDiscountCF option:selected').text());
                document.getElementById("lblCostoServAdic").style.display = 'none';
            }
            else if (SessionTransf.idAccion == strIdServicioAdicional) {
                $('#lblDesc').text('Servicio');
                Smmry.set('Descuento', SessionTransf.descServAdic);
                document.getElementById("lblCostoServAdic").style.display = 'block';
            }
            else {
                Smmry.set('Descuento', '');
                document.getElementById("lblDesc").style.display = 'none';
                document.getElementById("lblCostoServAdic").style.display = 'none';
            }
        },
        SummaryTotalDescuento: function () {
            if ((SessionTransf.idAccion == strIdCargoFijo) || (SessionTransf.idAccion == strIdServicioAdicional)) {
                document.getElementById("lblSummaryTotalDescuento").style.display = 'block';
                Smmry.set('SummaryTotDescuento', 'S/' + $('#txtTotDescuento').val());
            }
            else {
                document.getElementById("lblSummaryTotalDescuento").style.display = 'none';
                Smmry.set('SummaryTotDescuento', '');
            }
        },
        SummaryCosto: function () {
            if (SessionTransf.idAccion == strIdServicioAdicional) {
                Smmry.set('Costo', 'S/' + SessionTransf.strTarifRet);
            }
            else {
                Smmry.set('Costo', '');
            }
        },
        //#endregion

        pad: function (s) { return (s < 10) ? '0' + s : s; },
        strUrlLogo: window.location.protocol + '//' + window.location.host + '/Images/loading2.gif',
        loadPage: function () {
            $.blockUI({
                message: '<div align="center"><img src="' + this.strUrlLogo + '" width="25" height="25" /> Cargando ... </div>',
                css: {
                    border: 'none',
                    padding: '15px',
                    backgroundColor: '#000',
                    '-webkit-border-radius': '10px',
                    '-moz-border-radius': '10px',
                    opacity: .5,
                    color: '#fff',
                }
            });
        },
        intContratoID: '',
        NumeracionMENOSUNO: '-1',
        NumeracionUNO: '1',
        NumeracionDOS: '2',
        NumeracionCERO: '0',
        strFechaSum: '',
        Value_C: '',
        Value_N: '',
        Description: '',
        FlatLoad: '',

        //#region Proy-32650
        InitMonthSA: function () {
            var that = this,
                controls = that.getControls(),
                objLstMonths = {};
            objLstMonths.strIdSession = Session.IDSESSION,
            objLstMonths.strIdTipo = strIdTipoServAdic;

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objLstMonths),
                url: '/Transactions/CommonServices/GetMonths',
                success: function (response) {
                    //
                    $('#cboMonthSA').html('');
                    controls.cboMonthSA.append($('<option>', { value: '', html: 'Seleccionar' }));
                    if (response.data != null) {
                        $.each(response.data, function (index, value) {
                            controls.cboMonthSA.append($('<option>', { value: value.Code, html: value.Description }));
                        });
                    }
                }
            });
        },

        InitMonthCF: function () {
            var that = this,
                controls = that.getControls(),
                objLstMonths = {};
            objLstMonths.strIdSession = Session.IDSESSION,
            objLstMonths.strIdTipo = strIdTipoCargoFijo;

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objLstMonths),
                url: '/Transactions/CommonServices/GetMonths',
                success: function (response) {

                    $('#cboMonthCF').html('');
                    controls.cboMonthCF.append($('<option>', { value: '', html: 'Seleccionar' }));
                    if (response.data != null) {
                        $.each(response.data, function (index, value) {
                            controls.cboMonthCF.append($('<option>', { value: value.Code, html: value.Description }));
                        });
                    }
                }
            });
        },
        ObtenerValorMes: function (parametro) {
            var concatenado = parametro.split("|");
            return concatenado[1];
        },

        InitDiscountAS: function () {
            var that = this,
           controls = that.getControls(),
                objLstDiscount = {};
            objLstDiscount.strIdSession = Session.IDSESSION;

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objLstDiscount),
                url: '/Transactions/CommonServices/GetListDiscount',
                success: function (response) {
                    $('#cboDiscountSA').html('');
                    controls.cboDiscountSA.append($('<option>', { value: '', html: 'Seleccionar' }));
                    if (response.data != null) {
                        $.each(response.data, function (index, value) {

                            var discount = value.Descripcion.split("|");
                            if (discount[1] == "1") {
                                controls.cboDiscountSA.append($('<option>', { value: value.Codigo, html: discount[0] }));
                            }
                       
                        });
                    }
                }
            });
        },


        InitDiscountCF: function () {
            var that = this,
           controls = that.getControls(),
           objLstAccionType = {};

            objLstAccionType.strIdSession = Session.IDSESSION;

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objLstAccionType),
                url: '/Transactions/CommonServices/GetListDiscount',
                success: function (response) {

                    controls.cboDiscountCF.append($('<option>', { value: '', html: 'Seleccionar' }));
                    if (response.data != null) {
                        $.each(response.data, function (index, value) {

                            var discount = value.Descripcion.split("|");
                            if (discount[1] == "0") {
                                controls.cboDiscountCF.append($('<option>', { value: value.Codigo, html: discount[0] }));
                            }

                        });
                    }
                }
            });

        },

        cboAccion_change: function () {
            var that = this,
           controls = that.getControls(),
                value = $('#cboAccion option:selected').val(),
                arrCod = value.split('|');
            SessionTransf.idAccion = arrCod[0];

            document.getElementById("lblTotDescuento").style.display = 'none';
            document.getElementById("txtTotDescuento").style.display = 'none';

            if (arrCod[0] == strIdCargoFijo || arrCod[0] == strIdServicioAdicional) {
                document.getElementById("lblTotDescuento").style.display = 'block';
                document.getElementById("txtTotDescuento").style.display = 'block';

                if (arrCod[0] == strIdCargoFijo) {
                    controls.divDesCargFijo.show();
                    controls.divAditionalServices.hide();
                    that.InitMonthCF();
                } else if (arrCod[0] == strIdServicioAdicional) {
                    controls.divDesCargFijo.hide();
                    controls.divAditionalServices.show();
                    that.getDecosMatriz();
                    that.getDecosInstalados();
                    that.GetCommercialServices();
                    that.GetInstallCost();
                    that.InitMonthSA();

                }
                if ((strValidaDescuentoActivo == '') || (strValidaDescuentoActivo == undefined)) {
                } else {

                    if (SessionTransf.hidAccionTra == 'R') {
                        controls.btnSummaryAccion.attr('disabled', true);
                        alert(strValidaDescuentoActivo);
                    } else {
                        controls.btnSummaryAccion.attr('disabled', false);
                    }

                }

            }
            else {
                controls.btnSummaryAccion.attr('disabled', false);
                controls.divDesCargFijo.hide();
                controls.divAditionalServices.hide();
                $('#divCostoInstalacion').hide();
                $('#divFlatAccionFranja').hide();
            }


        },

        cboMonthSA_change: function () {
            var that = this,
           controls = that.getControls(),
                tMonth = $('#cboMonthSA option:selected').text();
        },

        cboMonthCF_change: function () {
            var that = this,
           controls = that.getControls(), objLstAccionType = {},
                tMonth = $('#cboMonthCF option:selected').text();

            objLstAccionType.strIdSession = Session.IDSESSION;

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objLstAccionType),
                url: '/Transactions/CommonServices/getCurrentVigency',
                success: function (response) {
                    //
                    if (response.data != null) {

                    }
                }
            });


        },

        cboDiscountCF_change: function () {

            var that = this,
            controls = that.getControls(), objLstAccionType = {},
            tDiscountCF = $('#cboDiscountCF option:selected').text();

            objLstAccionType.strIdSession = Session.IDSESSION;

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objLstAccionType),
                url: '/Transactions/CommonServices/getCurrentDiscount',
                success: function (response) {

                    if (response.data != null) {

                    }
                }
            });


        },

        /*proy 32650*/
        cboDiscountSA_change: function () {
            var that = this,
                controls = that.getControls(),
                tcboDiscountSA = $('#cboDiscountSA option:selected').text(),
                objDiscountSA = {
                    strIdSession: Session.IDSESSION,
                    strTantoPorciento: tcboDiscountSA,
                    strCostoInst: controls.txtCostInst.text()
                };

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objDiscountSA),
                url: '/Transactions/CommonServices/GetAmountInstall',
                success: function (response) {
                    //
                    SessionTransf.nuevoCostoInstal = '0';
                    if (response.data != null && response.data !== "") {
                        SessionTransf.nuevoCostoInstal = response.data.nuevoCostoInstal;
                        SessionTransf.costoDescuentoInstal = response.data.costoDescuentoInstal;//bono para el servicio
                    }
                }
            });

        },
        /*3265*/
        chkPromFact_change: function (send, args) {
            //VALIDAR si selecciono un porcentaje anteriormente.

            if (($('#cboDiscountCF option:selected').val()) == '') {
                alert("Seleccionar porcentaje de descuento.", "Alerta");
                $("#chkPromFact").removeAttr("checked");
                return;
            }
            if (($('#cboMonthCF option:selected').val()) == '') {
                alert("Seleccionar meses a aplicar.", "Alerta");
                $("#chkPromFact").removeAttr("checked");
                return;
            }
            if ((($('#cboMonthCF option:selected').val()) == 1) || (($('#cboMonthCF option:selected').val()) == '1')) {
                alert("Aplica para periodo de descuento mayor a 1 mes.", "Alerta");
                $("#chkPromFact").removeAttr("checked");
                return;
            }
            if (!($('#chkPromFact').is(':checked'))) {

                return;
            }


            var that = this,
            controls = that.getControls(),
            objDatos = {};
            objDatos.strCoId = Session.DATACUSTOMER.ContratoID
            objDatos.strIdSession = Session.IDSESSION;
            objDatos.strTantoPorciento = $('#cboDiscountCF option:selected').text();
            objDatos.strCliNroCuenta = Session.DATACUSTOMER.CustomerID;
            objDatos.strNroTelefono = Session.NROTELEFONO;
            objDatos.strCustomerId = Session.DATACUSTOMER.CustomerID;
            objDatos.BillingCycle = Session.DATACUSTOMER.Ciclo_Facturacion;
            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objDatos),
                url: '/Transactions/CommonServices/GetValidationOfPromotionToCurrentInvoice',
                success: function (response) {

                    if (response.validacion == false) {
                        alert(response.mensaje);
                        $("#chkPromFact").attr("disabled", true);
                        $("#chkPromFact").removeAttr("checked");
                    }
                    else {
                        $("#chkPromFact").removeAttr("disabled");

                    }
                }
            });


        },

        GetCommercialServices: function () {

            var that = this, objCacDacType = {}, controls = that.getControls();
            objCacDacType.strIdSession = Session.IDSESSION;
            objCacDacType.strCoId = Session.DATACUSTOMER.ContratoID;

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                cache: false,
                data: JSON.stringify(objCacDacType),
                url: '/Transactions/CommonServices/LTEGetAdditionalServices',
                success: function (response) {
                    that.SetDataToTable(response);
                }
            });

        },

        SetDataToTable: function (data) {
            var that = this,
             controls = that.getControls();
            controls.strPaqueteDeco = '';
            $('#tblAdiServBody').find('tbody').html('');
            var table = $("#tblAdiServBody").dataTable({
                "scrollY": "200px",
                "scrollCollapse": true,
                "paging": false,
                "searching": false,
                "destroy": true,
                "scrollX": true,
                "sScrollXInner": "100%",
                "autoWidth": true,
                "select": {
                    "style": "os",
                    "info": false
                },
                data: data,
                language: {
                    "lengthMenu": "Display _MENU_ records per page",
                    "zeroRecords": "No existen datos",
                    "info": " ",
                    "infoEmpty": " ",
                    "infoFiltered": "(filtered from _MAX_ total records)"
                },
                columns: [
                    { "data": null },
                    { "data": "DESCOSER" },
                    { "data": "COSTOPVU" },
                    { "data": "CARGOFIJO" }//Tarifa de retencion
                ],
                "columnDefs": [{
                    "orderable": false,
                    "className": 'select-radio',
                    "targets": 0,
                    "defaultContent": "",
                    "visible": true

                }
                ]
            });
        },

        tblAdiServBodyRow_Click: function () {
            var that = this,
            controls = that.getControls();
            var table = $("#tblAdiServBody").dataTable();

            $("#tblAdiServBody").on("click", "tbody tr", function () {

                let currentTr = $(this).parents("tr")[0] || $(this)[0];
                if (!$(currentTr).hasClass("selected")) {
                    var currentData = table.fnGetData(table.fnGetPosition(currentTr));
                    var strValidarTipDeco = strValidaDecos.split('|');

                    //validación de cantidad de decos.
                    if (currentData.TIPO_SERVICIO == strValidarTipDeco[0] && currentData.CODGRUPOSERV == strValidarTipDeco[2]) {
                        if ($('#txtCostInst').text() > 0) {
                            $('#divCostoInstalacion').css("display", "block");
                        } else {
                            $('#divCostoInstalacion').css("display", "none");
                        }
                        controls.divFlatAccionFranja.css("display", "block");
                        SessionTransf.DecodificatorSelected = currentData;
                        SessionTransf.isDecodificator = true;
                        AdditionalPointsModel.free = AdditionalPointsModel.freeTemp;

                        that.ValidaAgendamientoEnLinea();
                        controls.strPaqueteDeco = 'DECO';
                    } else {
                        $('#divCostoInstalacion').css("display", "none");
                        controls.divFlatAccionFranja.css("display", "none");
                        SessionTransf.isDecodificator = false;
                        SessionTransf.DecodificatorSelected = currentData;
                        controls.strPaqueteDeco = 'PAQUETE';
                    }
                }
            });
        },

        /*INICIO VALIDACIÓN DE DECOS INSTALADOS*/
        getDecosInstalados: function () {
            var cantidad = 0, cantDecos = 0,
                that = this,
                param = {},
                controls = that.getControls();

            that.objLteUninstallInstallDeco.CantidadListaEquipos = cantidad;


            param.strIdSession = Session.IDSESSION;
            param.strContratoID = Session.DATACUSTOMER.ContratoID;// en las fuentes de liz antes estaba ContractID
            param.strCustomerID = Session.DATACUSTOMER.CustomerID;

            $.ajax({
                type: 'POST',
                url: '/Transactions/LTE/UninstallInstallationOfDecoder/GetListDataProducts',
                data: JSON.stringify(param),
                contentType: 'application/json; charset=utf-8',
                datatype: 'json',
                async: true,
                cache: false,
                error: function () {
                    $.unblockUI();
                },
                success: function (response) {
                    var registros = response;
                    var contador = 0;
                    $.each(registros,
                        function (i, r) {
                            var descripcion40 = "";
                            if (r.tipoServicio === "TV SATELITAL") {
                                if (r.tipo_equipo === 'DECO') {
                                    var iconDeco = "";
                                    var txtTipo = "";
                                    if (r.tipo_deco === 'HD') {
                                        iconDeco = 'glyphicon-hd-video';
                                        txtTipo = "HD";
                                    } else if (r.tipo_deco.toUpperCase() === 'SD' || r.tipo_deco.toUpperCase() === 'REGULAR') {
                                        iconDeco = 'glyphicon-sd-video';
                                        txtTipo = "SD";
                                    } else {
                                        iconDeco = 'glyphicon-hdd';
                                        txtTipo = "DVR";
                                    }
                                    var peso = that.getWeightDeco(txtTipo);

                                    cantDecos = cantDecos + parseInt(peso);
                                }
                                cantidad++;

                            }
                        });

                    that.objLteUninstallInstallDeco.intCantidadDecos = cantidad;
                    that.objLteUninstallInstallDeco.intCantidadDecosTemp = cantDecos;


                    AdditionalPointsModel.free = parseInt(that.objLteUninstallInstallDeco.CantPuntosMax) - parseInt(that.objLteUninstallInstallDeco.intCantidadDecosTemp);
                    AdditionalPointsModel.freeTemp = AdditionalPointsModel.free;

                }
            });
        },

        getWeightDeco: function (tipodeco) {
            var that = this, valretorno = 0;
            for (var x = 0; x < that.objLteUninstallInstallDeco.MatrizDecos.length; x++) {
                if (tipodeco == that.objLteUninstallInstallDeco.MatrizDecos[x].descripcion ||
                    tipodeco == that.objLteUninstallInstallDeco.MatrizDecos[x].id) {

                    valretorno = parseInt(that.objLteUninstallInstallDeco.MatrizDecos[x].valor);
                }
            }
            return valretorno;
        },

        getDecosMatriz: function () {
            var that = this, controls = that.getControls(), param = {};
            param.strIdSession = Session.IDSESSION;

            $.ajax({
                type: "POST",
                url: "/Transactions/LTE/UninstallInstallationOfDecoder/GetDecoMatriz",
                data: JSON.stringify(param),
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                error: function () {
                },
                success: function (response) {
                    var filas = response.ListDecos, index = 0;
                    that.objLteUninstallInstallDeco.CantPuntosMax = response.numDecosMax;


                    if (filas != null) {
                        $.each(filas,
                            function () {
                                var mat = {
                                    id: filas[index].TipoDeco,
                                    descripcion: filas[index].Descripcion,
                                    valor: filas[index].Valor
                                }
                                that.objLteUninstallInstallDeco.MatrizDecos.push(mat);
                                index++;
                            });
                    }
                }
            });
        },
        /*FIN VALIDACIÓN DE DECOS INSTALADOS*/
        GetTotalInversion: function () {

            var that = this,
            controls = that.getControls(), objLstAccionType = {};

            objLstAccionType.strCoId = Session.DATACUSTOMER.ContratoID;

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objLstAccionType),
                url: '/Transactions/CommonServices/GetTotalInversion',
                success: function (response) {
                    if (response.data != null) {
                        $('#txtTotDescuento').val(response.data);
                    } else {
                        $('#txtTotDescuento').val("0,00");
                    }
                }
            });
        },

        GetCurrentDiscountFixedCharge: function () {
            var that = this,
           controls = that.getControls(),
            objLstAccionType = {};

            objLstAccionType.strCoId = Session.DATACUSTOMER.ContratoID;
            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objLstAccionType),
                url: '/Transactions/CommonServices/GetCurrentDiscountFixedCharge',
                success: function (response) {
                    if ((response.data != "") || (response != undefined)) {
                        strValidaDescuentoActivo = response.data;

                    }
                }
            });
        },
        loadActionSchedule: function () {
            var that = this,
            controls = that.getControls();
        },

        GetDefaultDecoderVariables: function () {
            var that = this;
            var param = {
                strIdSession: Session.IDSESSION
            };
            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(param),
                dataType: 'json',
                url: '/Transactions/LTE/UninstallInstallationOfDecoder/GetDefaultVariables',
                success: function (response) {
                    if (response != null) {
                        var data = response;
                        that.objLteUninstallInstallDeco.hdnmensajeConfirmacion = data.hdnmensajeConfirmacion;
                        that.objLteUninstallInstallDeco.hdnMensaje1 = data.hdnMensaje1;
                        that.objLteUninstallInstallDeco.hdnMensaje2 = data.hdnMensaje2;
                        that.objLteUninstallInstallDeco.hdnMensaje8 = data.hdnMensaje8;
                        that.objLteUninstallInstallDeco.hdnMensaje9 = data.hdnMensaje9;
                        that.objLteUninstallInstallDeco.hdnMensaje10 = data.hdnMensaje10;
                        that.objLteUninstallInstallDeco.hdnMensaje11 = data.hdnMensaje11;
                        that.objLteUninstallInstallDeco.hdnFechaActualServidor = data.hdnFechaActualServidor;
                        that.objLteUninstallInstallDeco.hdnTipoTrabajo = data.hdnTipoTrabajo;
                        that.objLteUninstallInstallDeco.hdnErrValidarAge = data.hdnErrValidarAge;
                        that.objLteUninstallInstallDeco.hdnListaGrupoCableLTE = data.hdnListaGrupoCableLTE;
                        that.objLteUninstallInstallDeco.hdnListaGrupoEquiposLTE = data.hdnListaGrupoEquiposLTE;
                        that.objLteUninstallInstallDeco.strMensajeValidaPlanComercial = data.strMensajeValidaPlanComercialLTE;
                        that.objLteUninstallInstallDeco.strMsgNoExisteDeco = data.strMsgNoExisteDeco;
                        that.objLteUninstallInstallDeco.strTRANSACCION_INSTALACION_DECO_ADICIONAL_LTE = data.strTRANSACCION_INSTALACION_DECO_ADICIONAL_LTE;
                        that.objLteUninstallInstallDeco.strMensajeNoExistenReglasDeNegocio = data.strMensajeNoExistenReglasDeNegocio;
                        that.objLteUninstallInstallDeco.strMensajeErrorConsultaIGV = data.strMensajeErrorConsultaIGV;
                        that.objLteUninstallInstallDeco.strMensajeValidationETA = data.strMensajeValidationETA;
                        that.objLteUninstallInstallDeco.strMensajeConfirmacionDeco = data.strMensajeConfirmacionDeco;
                        that.objLteUninstallInstallDeco.strMsgLimiteSdHd = data.strMsgLimiteSdHd;
                        that.objLteUninstallInstallDeco.strMsgLimiteDVR = data.strMsgLimiteDVR;
                        that.objLteUninstallInstallDeco.TypeLoyalty = data.intTypeLoyalty;
                        that.objLteUninstallInstallDeco.MotSotCode = data.strCodigoMotivoSot;
                        that.objLteUninstallInstallDeco.CodTipServLte = data.strCodTipServLte;
                        Session.ServerDate = data.hdnFechaActualServidor;
                    }
                }
            });
        },

        ValidateUser: function (option, fn_success, fn_failled, fn_cancel, fn_error) {
            var xthat = this;
            $.window.open({
                autoSize: true,

                url: '/Transactions/AuthUser/Auth/AuthUserHtml',

                type: 'POST',
                title: 'Autorización',
                modal: true,
                width: 931,
                height: 400,
                buttons: {
                    Aceptar: {
                        class: 'btn-primary',
                        click: function (sender, args) {
                            var usu = $('#txtUsernameAuth').val();
                            var pass = $('#txtPasswordAuth').val();
                            var $this = this;
                            $.ajax({
                                type: "POST",
                                cache: false,
                                dataType: "json",
                                url: '/Transactions/CommonServices/CheckingUser',
                                data: { strIdSession: Session.IDSESSION, user: usu, pass: pass, option: option },
                                error: function (ex) {
                                    if (fn_error != null) {
                                        fn_error.call(xthat, true);
                                    }
                                },

                                beforeSend: function () {
                                    $.blockUI({
                                        message: '<div align="center"><img src="../../../../../Images/loading2.gif"  width="25" height="25" /> Cargando .... </div>',
                                        baseZ: $.app.getMaxZIndex() + 1,
                                        css: {
                                            border: 'none',
                                            padding: '15px',
                                            backgroundColor: '#000',
                                            '-webkit-border-radius': '10px',
                                            '-moz-border-radius': '10px',
                                            opacity: .5,
                                            color: '#fff'
                                        }
                                    });

                                },

                                complete: function () {
                                    $.unblockUI();
                                },
                                success: function (response) {

                                    if (response.result && response.result == 1) {



                                        if (fn_success != null) {

                                            fn_success.call(xthat, true);
                                        }
                                        $this.close();



                                    } else if (response.result == 2 || response.result == 0) {
                                        $.unblockUI();
                                        alert('La validacion del usuario ingresado es incorrecto o no tiene permisos para continuar con el proceso, por favor verifiquelo.');
                                        if (fn_failled != null) {
                                            fn_failled.call(xthat, true);

                                        }
                                    } else if (response.result == 3) {
                                        $.unblockUI();
                                        alert('Ocurrio un error al Validar el Usuario.');
                                        if (fn_error != null) {
                                            fn_error.call(xthat, true);
                                        }
                                    }
                                }
                            });
                        }
                    }, Cancelar: {

                        click: function (sender, args) {
                            var $that = this;
                            if (fn_cancel != null) {
                                fn_cancel.call(xthat, false);
                            }
                            $that.close();
                        }
                    }
                }
            });
        },



        GetSubTypeWork_Action: function () {
            var that = this,
           controls = that.getControls(),
                param = {};

            param.strIdSession = Session.IDSESSION;
            param.strTipoTrabajo = controls.cboTypeWork_Action.val();


            $.ajax({
                type: "POST",
                url: '/Transactions/LTE/UninstallInstallationOfDecoder/GetJobSubType',
                data: JSON.stringify(param),
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                error: function (err) {

                },
                success: function (response) {
                    var intIndex = 0;

                    controls.cboSubTypeWork_Action.html("");
                    controls.cboSubTypeWork_Action.append($('<option>', { value: '', html: 'Seleccionar' }));
                    if (response.data != null) {
                        $.each(response.data, function (index, value) {
                            intIndex++;
                            controls.cboSubTypeWork_Action.append($('<option>', { value: value.Code, html: value.Description }));
                        });

                        if (intIndex == 0 && Session.ValidateETA == "1") {
                            alert("No se encontraron subtipos de trabajo disponibles.", "Alerta");
                        }

                    } else
                        alert("No se encontraron subtipos de trabajo disponibles.");

                    controls.cboSubTypeWork_Action
                        .attr('disabled', false)
                        .change();
                }
            });

        },

        InitMotiveSOTAction: function () {
            //
            var that = this,
           controls = that.getControls(),
           objLstMotiveSotType = {};

            objLstMotiveSotType.strIdSession = Session.IDSESSION;
            objLstMotiveSotType.vIdTypeWork = controls.cboTypeWork_Action.val();

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objLstMotiveSotType),
                url: '/Transactions/LTE/RetentionCancelServices/GetMotive_SOT',
                success: function (response) {
                    //
                    controls.cboMotiveSOT_Action.html("");
                    controls.cboMotiveSOT_Action.append($('<option>', { value: '-1', html: 'Seleccionar' }));
                    if (response.data != null) {
                        $.each(response.data, function (index, value) {
                            controls.cboMotiveSOT_Action.append($('<option>', { value: value.Code, html: value.Description }));
                        });

                        controls.cboMotiveSOT_Action.val(SessionTransf.strValueMotivoSOTDefecto);
                        controls.cboTypeWork_Action.val(SessionTransf.strValueTipoTrabajoDefecto);
                    }
                },
                error: function (XError) {

                },
            });
        },

        SummaryTipoTrabajoAccion: function () {
            var that = this,
                controls = that.getControls();

            if (controls.cboTypeWorkAccion.val() != "-1") {
                Smmry.set('TipoTrabajoAccion', $('#cboTypeWorkAccion option:selected').html());
            }
            else {
                Smmry.set('TipoTrabajoAccion', '');
            }
        },

        SummaryMotivoSotAccion: function () {
            var that = this,
                controls = that.getControls();

            if (controls.cboMotiveSOTAccion.val() != "-1") {
                Smmry.set('MotivoSotAccion', $('#cboMotiveSOTAccion option:selected').html());
            }
            else {
                Smmry.set('MotivoSotAccion', '');
            }
        },

        SummarySubTipoTrabajoAccion: function () {
            var that = this,
                controls = that.getControls();

            if (controls.cboSubTypeWorkAccion.val() != "-1") {
                Smmry.set('SubTipoTrabajoAccion', $('#cboSubTypeWorkAccion option:selected').html());
            }
            else {
                Smmry.set('SubTipoTrabajoAccion', '');
            }
        },

        getRoundDecimal: function (x) {
            return Math.round(parseFloat(x) * 100) / 100;
        },

        objLteUninstallInstallDeco: {
            ListaEquiposAdicionalesServer: {},
            ListaEquiposBajaServer: {},
            Typification: {},
            hndHistoryETA: "",
            hndValidateETA: "0",
            hidAgregaAsociar: 0,
            hidAgregaDesaso: 0,
            hidCerrar: 0,
            igv: 0,
            CodEquipAlSelec: "",
            strLetraF: "F",
            Slash: "/",
            strVariableEmpty: "",
            intNumeroCero: 0,
            intNumeroUno: 1,
            strNumeroCeroDecimal: "0.00",
            strNumeroMenosUno: "-1",
            strNumeroCero: "0",
            strNumeroUno: "1",
            strNumeroDos: "2",
            InstDesins: "0",
            hdnCodigoPlan: "",
            CostoAdicional: 0,
            CostoAdicionalCIGV: 0,
            CantAdicionalesInst: 0,
            CantAdicionalesDesint: 0,
            intCantidadDecos: 0,
            CantPuntosMax: 4,
            opcAct: 0,
            opcDesinst: 0,
            opcInst: 0,
            RequestActId: 0,
            FlajInstDesins: 0,
            ListaEquiposAdicionalesInst: [],
            ListaEquiposAdicionalesDesinst: [],
            MatrizDecos: [],
            TmCode: '',
            LoyaltyAmount: '0.00',
            strRutaPDF: "",
            LoyaltyFlag: 0,
            BaseCharge: 0,
            AdditionalCharge: 0,
            ServicesNumber: 0,
            LoyaltyAmountTemp: '',
            TotalAmountsIgv: 0,
            TotalAmountcIgv: 0,
            TypeLoyalty: 0,
            MotSotCode: '',
            intCantidadDecosTemp: 0,
            SubTipOrdCU: "",
            CodTipServLte: '',
            IdConsulta: '',
            IdInteraccion: '',
            FechaCompromiso: '',
            Franja: '',
            Idbucket: ''
        },


        //validar acceso
        f_VentanaAutorizacion: function () {
            var that = this;
            var controls = that.getControls();
            that.ValidateUser('strKeyValidacionNoRetenidoLTE', that.SaveTransactionNoRetention, null, null, null);

        },
        ValidateUser: function (option, fn_success, fn_failled, fn_cancel, fn_error) {
            var xthat = this;
            $.window.open({
                autoSize: true,

                url: '/Transactions/AuthUser/Auth/AuthUserHtml',

                type: 'POST',
                title: 'Autorización',
                modal: true,
                width: 931,
                height: 400,
                buttons: {
                    Aceptar: {
                        class: 'btn-primary',
                        click: function (sender, args) {
                            var usu = $('#txtUsernameAuth').val();
                            var pass = $('#txtPasswordAuth').val();
                            var $this = this;
                            $.ajax({
                                type: "POST",
                                cache: false,
                                dataType: "json",
                                url: '/Transactions/CommonServices/CheckingUser',
                                data: { strIdSession: Session.IDSESSION, user: usu, pass: pass, option: option },
                                error: function (ex) {
                                    if (fn_error != null) {
                                        fn_error.call(xthat, true);
                                    }
                                },

                                beforeSend: function () {
                                    $.blockUI({
                                        message: '<div align="center"><img src="../../../../../Images/loading2.gif"  width="25" height="25" /> Cargando .... </div>',
                                        baseZ: $.app.getMaxZIndex() + 1,
                                        css: {
                                            border: 'none',
                                            padding: '15px',
                                            backgroundColor: '#000',
                                            '-webkit-border-radius': '10px',
                                            '-moz-border-radius': '10px',
                                            opacity: .5,
                                            color: '#fff'
                                        }
                                    });

                                },

                                complete: function () {
                                    $.unblockUI();
                                },
                                success: function (response) {

                                    if (response.result && response.result == 1) {



                                        if (fn_success != null) {

                                            fn_success.call(xthat, true);
                                        }
                                        $this.close();



                                    } else if (response.result == 2 || response.result == 0) {
                                        $.unblockUI();
                                        alert('La validacion del usuario ingresado es incorrecto o no tiene permisos para continuar con el proceso, por favor verifiquelo.');
                                        if (fn_failled != null) {
                                            fn_failled.call(xthat, true);

                                        }
                                    } else if (response.result == 3) {
                                        $.unblockUI();
                                        alert('Ocurrio un error al Validar el Usuario.');
                                        if (fn_error != null) {
                                            fn_error.call(xthat, true);
                                        }
                                    }
                                }
                            });
                        }
                    },
                    Cancelar: {

                        click: function (sender, args) {
                            var $that = this;
                            if (fn_cancel != null) {
                                fn_cancel.call(xthat, false);
                            }
                            $that.close();
                        }
                    }
                }
            });
        },

        //PROY-32650

        getTypificationTransaction: function () {
            var that = this;

            var param = {
                strIdSession: Session.IDSESSION,
                strTransactionName: strTRANSACCION_INSTALACION_DECO_ADICIONAL_LTE
            }

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(param),
                dataType: 'json',
                url: '/Transactions/LTE/UninstallInstallationOfDecoder/GetTypificationTransaction',
                success: function (response) {
                    if (response != null) {
                        var data = response;
                        that.objLteUninstallInstallDeco.Typification.Type = data.Type;
                        that.objLteUninstallInstallDeco.Typification.Class = data.Class;
                        that.objLteUninstallInstallDeco.Typification.SubClass = data.SubClass;
                        that.objLteUninstallInstallDeco.Typification.InteractionCode = data.InteractionCode;
                        that.objLteUninstallInstallDeco.Typification.TypeCode = data.TypeCode;
                        that.objLteUninstallInstallDeco.Typification.ClassCode = data.ClassCode;
                        that.objLteUninstallInstallDeco.Typification.SubClassCode = data.SubClassCode;
                    }
                },
                complete: function () {

                }
            });
        },

        GetInstallCost: function () {
            var that = this,
            controls = that.getControls();
            var objLstDiscount = {};
            objLstDiscount.strIdSession = Session.IDSESSION;
            objLstDiscount.hfc_lte = 2;
            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objLstDiscount),
                url: '/Transactions/CommonServices/GetInstallationCost',
                success: function (response) {
                    var salida = response.data.split('|')
                    if ((salida[0] != "")) {
                        $('#txtCostInst').text(salida[0]);
                    } else {
                        $('#txtCostInst').text('0.00');
                        if (salida[1] !== "0") {
                            strMsgCostoError = salida[1];
                        }

                    }
                }
            });
        },

        //#region PROY-32650  II - Retención/Fidelización
        f_ValidacionETAAccion: function () {


            var that = this,
                         controls = that.getControls(),
                         model = {};

            model.IdSession = Session.IDSESSION;
            model.strJobTypes = controls.cboTypeWorkAccion.val();
            model.StrCodeUbigeo = Session.CLIENTE.CodeCenterPopulate;
            model.StrTypeService = SessionTransf.ServiceType;

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(model),
                url: '/Transactions/SchedulingToa/GetValidateETA',
                success: function (response) {
                    var oItem = response.data;
                    if (oItem.Codigo == '2' || oItem.Codigo == '1' || oItem.Codigo == '0') {
                        AdditionalPointsModel.strValidateETA = oItem.Codigo;
                        AdditionalPointsModel.strHistoryETA = oItem.Codigo2;

                        ValidateEtaAccion = oItem.Codigo;
                        Session.History = oItem.Codigo2;

                        if (oItem.Codigo == '2' || oItem.Codigo == '1') {
                            that.f_EnableAgendamientoAccion(true);
                            InitFranjasHorario2();
                        }
                        else {
                            alert("No aplica agendamiento en línea, favor de continuar con la operación.", "Informativo");
                            InitFranjasHorario2();
                            AdditionalPointsModel.strValidateETA = "0";

                            ValidateEtaAccion = "0";
                            Session.History = "";

                            that.f_EnableAgendamientoAccion(false);
                        }
                    } else {
                        if (oItem.Descripcion == null) {
                            oItem.Descripcion = " ";
                        }
                        alert(AdditionalPointsModel.strMessageValidationETA, "Alerta");
                        $("#tr_SubWorkType").attr("disabled", true);
                        InitFranjasHorario2();

                        that.f_EnableAgendamientoAccion(false);

                        AdditionalPointsModel.strValidateETA = "0";
                        AdditionalPointsModel.strHistoryETA = oItem.Codigo2;

                        ValidateEtaAccion = "0";
                        Session.History = oItem.Codigo2;
                    }


                }
            });

        },

        f_LoadCombo: function (URL, BE, Combo, bSelect, Event, basync) {
            var that = this,
              controls = that.getControls();

            if (bSelect == null) { bSelect = true; }
            if (basync == null) { basync = true }

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(BE),
                url: URL,
                async: basync,
                error: function (error) {

                },
                success: function (response) {
                    if (bSelect == true) {
                        Combo.html("");
                        Combo.append($('<option>', { value: '-1', html: 'Seleccionar' }));
                    }

                    if (response.objFinalResponse != null) {/*en vez de data, estaba objFinalResponse*/
                        $.each(response.objFinalResponse, function (index, value) {/*en vez de data, estaba objFinalResponse*/
                            if (Event == null) {
                                if (value.Code == response.strTypeJobInst) {
                                    Combo.append($('<option>', { value: value.Code, html: value.Description }));
                                }
                            } else {
                                Combo.append($('<option>', { value: value.Code, html: value.Description }));
                            }
                        });
                    }
                    if (Event != null) {
                        switch (Event) {
                            case 1:
                                that.cboTypeWorkAccion_Change();
                                break;
                            case 2:
                                if (response.data != null) {
                                    $.each(response.data.CacDacTypes, function (index, value) {
                                        Combo.append($('<option>', { value: value.Code, html: value.Description }));
                                    });
                                }
                                break;
                            case 3:
                                if (response.data != null) {
                                    Combo.empty();
                                    Combo.append($('<option>', { value: '-1', html: 'Seleccionar' }));
                                    $.each(response.data, function (index, value) {
                                        var codTipoSubTrabajo = value.Code.split("|");
                                        if (response.typeValidate.COD_SP == "0" && codTipoSubTrabajo[0] == response.typeValidate.COD_SUBTIPO_ORDEN) {
                                            Combo.append($('<option>', { value: value.Code, html: value.Description, typeservice: value.Code2, selected: true }));
                                            Combo.attr('disabled', true);
                                        }
                                        else {
                                            Combo.append($('<option>', { value: value.Code, html: value.Description, typeservice: value.Code2 }));
                                        }
                                    });
                                    Combo.prop("disabled", true);
                                }
                                break;
                            case 4:
                                if (response.data != null) {/*en vez de data, estaba objFinalResponse*/
                                    Combo.empty();
                                    $.each(response.data, function (index, value) {/*en vez de data, estaba objFinalResponse*/
                                        if (Event == null) {
                                            if (value.Code == response.strTypeJobInst) {
                                                Combo.append($('<option>', { value: value.Code, html: value.Description }));
                                            }
                                        } else {
                                            Combo.append($('<option>', { value: value.Code, html: value.Description }));
                                        }
                                    });
                                }
                                break;
                        }
                    }

                }
            });
        },

        GetTypeWorkAccion: function () {

            var that = this,
                controls = that.getControls(),
                model = {};

            model.strIdSession = Session.IDSESSION;
            model.strTransacType = "0";
            var URL_GetJobType = window.location.protocol + '//' + window.location.host + '/Transactions/LTE/UninstallInstallationOfDecoder/GetWorkType';
            that.f_LoadCombo(URL_GetJobType, model, controls.cboTypeWorkAccion, true, null, false);
            document.getElementById("cboTypeWorkAccion").selectedIndex = "1"; /*seleccionar el 1 elemento JUAN* */
            document.getElementById("cboTypeWorkAccion").disabled = true; /*debe estar deshabilitado JUAN* */
            that.GetSubTypeWorkAccion();
        },

        cboTypeWorkAccion_Change: function () {

            var that = this,
                   controls = that.getControls(),
                   oCustomer = Session.DATACUSTOMER,
                   model = {};
        },
        ValidaAgendamientoEnLinea: function () {
            var that = this,
                   controls = that.getControls(),
                   oCustomer = Session.DATACUSTOMER,
                   model = {};

            controls.txtDateCommitmentAccion.val("");
            controls.cboScheduleAccion.html("");
            controls.cboScheduleAccion.append($('<option>', { value: '-1', html: 'Seleccionar' }));
            model.IdSession = Session.IDSESSION;

            if (controls.cboTypeWorkAccion.val() == "480.|" || controls.cboTypeWorkAccion.val() == AdditionalPointsModel.strJobTypeComplementarySalesHFC) {
                $("#tr_ServicesType").show();

                model.strJobTypes = AdditionalPointsModel.strJobTypeComplementarySalesHFC;
                model.strInternetValue = AdditionalPointsModel.strInternetValue;
                model.strCellPhoneValue = AdditionalPointsModel.strCellPhoneValue;
            }
            else {
                var fechaServidor = new Date(Session.ServerDate);
                var fechaServidorMas7Dias = new Date(fechaServidor.setDate(fechaServidor.getDate() + 7));
                controls.txtDateCommitmentAccion.val([that.f_pad(fechaServidorMas7Dias.getDate()), that.f_pad(fechaServidorMas7Dias.getMonth() + 1), fechaServidorMas7Dias.getFullYear()].join("/"));
            }
            if (controls.cboTypeWorkAccion.val().indexOf(".|") == -1) {

                Validate = "1";

            } else {

                Validate = "0";
            }

            if (controls.cboTypeWorkAccion.val() != "-1") {
                that.f_ValidacionETAAccion();
            } else {
                controls.cboScheduleAccion.html("");
                controls.cboScheduleAccion.append($('<option>', { value: '-1', html: 'Seleccionar' }));
                controls.cboScheduleAccion.prop("disabled", false);
            }

        },
        GetSubTypeWorkAccion: function () {
            var that = this,
              controls = that.getControls(),
              model = {};
            model.IdSession = Session.IDSESSION;
            model.strJobTypes = controls.cboTypeWorkAccion.val();
            model.strContractId = Session.DATACUSTOMER.ContratoID;

            var URL_GetOrderType = window.location.protocol + '//' + window.location.host + '/Transactions/LTE/AdditionalPoints/GetOrderType';
            that.f_LoadCombo(URL_GetOrderType, model, controls.cboSubTypeWorkAccion, true, 3, false);
            document.getElementById("cboSubTypeWorkAccion").selectedIndex = "1";
            document.getElementById("cboSubTypeWorkAccion").disabled = false;
        },

        cboSubTypeWorkAccion_Change: function () {
            var that = this,
                 controls = that.getControls(),
                 strUrl = '';
            if (controls.cboSubTypeWorkAccion.val() == "-1") {
                return false;
            }
            if (ValidateEtaAccion == '1') {
                if (controls.cboSubTypeWorkAccion.val() != "-1") {
                    if (controls.txtDateCommitmentAccion.val() != "") {
                        InitFranjasHorario2();
                    }
                }
            }
        },

        GetMotiveSOTAccion: function (idTrabajo) {

            var that = this,
                controls = that.getControls(),
                model = {};

            model.strIdSession = Session.IDSESSION;
            var URL_GetMotiveSot = window.location.protocol + '//' + window.location.host + '/Transactions/HFC/AdditionalPoints/GetMotivoSot';
            that.f_LoadCombo(URL_GetMotiveSot, model, controls.cboMotiveSOTAccion, true, 4, false);
            controls.cboMotiveSOTAccion.val(strCboSelMotivoSOTLTE);
            document.getElementById("cboMotiveSOTAccion").disabled = true;
        },

        f_Loading: function () {
            var that = this,
             controls = that.getControls();
            $.blockUI({
                message: controls.ModalLoading,
                css: {
                    border: 'none',
                    padding: '15px',
                    backgroundColor: '#000000',
                    '-webkit-border-radius': '50px',
                    '-moz-border-radius': '50px',
                    opacity: .7,
                    color: '#fff'
                }
            });
        },

        f_GetParameter: function () {
            var that = this,
              controls = that.getControls(),
              oCustomer = Session.DATACUSTOMER,
              oUserAccess = Session.USERACCESS,
              oDataService = Session.DATASERVICE,
                Model = {};

            var myUrl = '/Transactions/HFC/AdditionalPoints/GetParameter';
            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(Model),
                url: myUrl,
                async: false,
                success: function (response) {
                    if (response != null) {
                        Session.ServerDate = response.strDateServer;
                        Session.DateNew = response.strDateNew;

                        AdditionalPointsModel.strMessageValidationETA = response.strMessageValidationETA;
                        AdditionalPointsModel.strJobTypeComplementarySalesHFC = response.strJobTypeComplementarySalesHFC;
                        AdditionalPointsModel.strInternetValue = oDataService.InternetValue;
                        AdditionalPointsModel.strCellPhoneValue = oDataService.TelephonyValue;

                        AdditionalPointsModel.strCustomerRequestId = response.strCustomerRequestId;
                    }
                    else {
                        alert("Hubo un problema al cargar las variables.", "Alerta");
                        that.btnClose_Click();
                    }
                }
            });

        },

        f_EnableAgendamientoAccion: function (bool) {
            var that = this,
                controls = that.getControls(),
                strUrl = '';
            if (bool == true) {
                controls.txtDateCommitmentAccion.prop("disabled", false);
                controls.cboScheduleAccion.prop("disabled", false);
            } else {

                var fechaServidor = new Date(Session.ServerDate);
                controls.txtDateCommitmentAccion.val([that.f_pad(fechaServidor.getDate()), that.f_pad(fechaServidor.getMonth() + 1), fechaServidor.getFullYear()].join("/"));

                if (controls.cboTypeWorkAccion.val().indexOf(".|") == -1) {
                    Validate = "1";
                }
                else {
                    Validate = "0";
                }
            }
        },
        loadActionSchedule: function () {
            var that = this,
            controls = that.getControls();
        },

        scheduleStripeAction: function () {

            var that = this,
            controls = that.getControls();

            var ValidaETA = Session.ValidateETA;
            var model = {};
            model.strIdSession = Session.IDSESSION;
            model.vUbigeo = CodUbigeoAux;
            model.vJobTypes = controls.cboTypeWork_Action.val();

            if (controls.txtDateCommitment_Action != null) {
                model.vCommitmentDate = controls.txtDateCommitment_Action.val();
            }

            model.vSubJobTypes = controls.cboSubTypeWork_Action.val();
            model.vValidateETA = ValidaETA;
            model.vHistoryETA = Session.History;
            model.vTimeZone = controls.cboSchedule_Action.val();

            var myUrl = window.location.protocol + '//' + window.location.host + "/Transactions/HFC/AdditionalPoints/GetTimeZone";

            $.ajax({
                url: myUrl,
                data: JSON.stringify(model),
                type: 'POST',
                contentType: "application/json charset=utf-8;",
                dataType: "json",
                success: function (response) {


                    controls.cboSchedule_Action.html("")
                        .append($('<option>', { value: '-1', html: 'Seleccionar' }));

                    var intIndex = 0;
                    if (response.data != null) {
                        if (response.data.length == 1 && response.data[0].Codigo == "-1") {
                            alert(response.data[0].Descripcion);
                            return;
                        }
                        $.each(response.data, function (index, value) {
                            intIndex++;
                            if (Session.ValidateETA == "1") {
                                if (value.Estado == "RED") {
                                    controls.cboSchedule_Action.append('<option style="background-color: #E60000;" value="' + value.Codigo + '" Disabled>' + value.Descripcion + '</option>');
                                }
                                else {
                                    controls.cboSchedule_Action.append('<option style="background-color: #FFFFFF;" value="' + value.Codigo + '+' + value.Codigo3 + '">' + value.Descripcion + '</option>');
                                }
                                if (value.Codigo2 != null) {
                                    try {
                                        Session.RequestActId = value.Codigo2;
                                    } catch (e) { }
                                }
                                else {
                                    try {
                                        Session.RequestActId = "";
                                    } catch (e) { }
                                }
                            }
                            else {
                                controls.cboSchedule_Action.append($('<option>', { value: value.Codigo, html: value.Descripcion }));
                            }
                        });
                    }
                }
            });
        },

        eventsScheduleAction: function () {
            var that = this,
           controls = that.getControls();

            controls.btnValidar_Action.click(function () {
                if (controls.cboTypeWork_Action.val() == "" || controls.cboTypeWork_Action.val() == "-1") {
                    alert(MESSAGE_SELECT_JOB_TYPES);
                    return false;
                }
                if (controls.txtDateCommitment_Action.val() == "") {
                    alert(MESSAGE_SELECT_DATE);
                    return false;
                }
                if (CodUbigeoAux == "") {
                    alert(MESSAGE_ERROR_UBIGEO);
                    return false;
                }

                if (controls.cboSchedule_Action.val() == "" || controls.cboSchedule_Action.val() == "-1") {
                    alert(MESSAGE_NS_TIME_ZONE);
                    return false;
                }

                that.ScheduleActionValidateRescheduling();
            });

            controls.cboSchedule_Action.change(function () {

                var vCod = $(this).val();
                if (vCod.indexOf("|") != -1) {
                    alert(MESSAGE_SELECCT_FRANJA);
                    $(this).val("-1");
                    return false;
                }

                if (controls.cboTypeWork_Action.val().indexOf(".|") == -1) {
                    Session.VALIDATE = "1";
                }
                else {
                    Session.VALIDATE = "0";
                }

            });

            controls.txtDateCommitment_Action.change(function () {
                var ValidaETA = Session.ValidateETA;
                var vJobType = controls.cboTypeWork_Action;
                var dDateProgramming = $(this);
                var cboSchedule = controls.cboSchedule_Action;

                if (dDateProgramming.val() == "") {
                    return false;
                }

                if (!f_FechaEsMayorQueHoy(dDateProgramming.val())) {
                    alert("La fecha debe ser mayor al día de hoy");
                    dDateProgramming.val("");
                    controls.cboSchedule_Action.html("");
                    controls.cboSchedule_Action.append($('<option>', { value: '-1', html: 'Seleccionar' }));
                    return false;
                }

                Session.VALIDATE = "0";

                if (vJobType.val() == "-1") {
                    alert(MESSAGE_SELECT_JOB_TYPES);
                    return false;
                }
                if (CodUbigeoAux == "") {
                    alert(MESSAGE_ERROR_UBIGEO);
                    return false;
                }


                if (ValidaETA == '1') {
                    if (vSubJobType.val() == "" || vSubJobType.val() == "-1" || vSubJobType.val() == "-- Seleccionar --") {
                        alert(MESSAGE_SELECT_SUB_JOB_TYPES);
                        dDateProgramming.val("");
                        return false;
                    }
                }

                LoadingSchedule();

                if (ValidaETA == '1') {
                    if ((vJobType.val() != "-1" || vJobType.val() != "") && dDateProgramming.val() != "" && (vSubJobType.val() != "-1" || vSubJobType.val() != "")) {
                        that.scheduleStripeAction();
                    }
                    else {
                        $.unblockUI();
                    }
                }
                else {
                    that.scheduleStripeAction();
                }
            });
        },

        ScheduleActionValidateRescheduling: function () {
            var that = this,
            controls = that.getControls();
            LoadingSchedule();
            var model = {};
            model.strIdSession = Session.IDSESSION;
            model.vJobTypes = controls.cboTypeWork_Action.val();
            model.vUbigeo = CodUbigeoAux;
            model.vCommitmentDate = controls.txtDateCommitment_Action.val();
            model.vTimeZona = controls.cboSchedule_Action.val();

            var myUrl = window.location.protocol + '//' + window.location.host + "/Transactions/HFC/AdditionalPoints/ValidateSchedule";
            $.ajax({
                url: myUrl,
                data: JSON.stringify(model),
                type: 'POST',
                contentType: "application/json charset=utf-8;",
                dataType: "json",
                success: function (response) {
                    if (response.data.Description == "1") {
                        alert(MESSAGE_VALIDATE);
                    } else {
                        alert(MESSAGE_NO_VALIDATE);
                    }
                    Session.VALIDATE = response.Description;
                },
                error: function (XError) {
                }
            });

        },

        cboTypeWorkAction_change: function () {
            var that = this, controls = that.getControls(), param = {};
            param.IdSession = Session.IDSESSION;;
            param.strJobTypes = controls.cboTypeWork_Action.val();
            param.StrCodeUbigeo = Session.CLIENTE.CodeCenterPopulate;
            param.StrTypeService = that.objLteUninstallInstallDeco.CodTipServLte;
            $.ajax({
                type: "POST",
                url: "/Transactions/SchedulingToa/GetValidateETA",
                data: JSON.stringify(param),
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                error: function () {
                },
                success: function (response) {
                    var oItem = response.data;
                    that.objLteUninstallInstallDeco.hndValidateETA = that.objLteUninstallInstallDeco.strNumeroCero;
                    var fechaServidor = new Date(Session.ServerDate);
                    controls.txtDateCommitment_Action.val([that.f_pad(fechaServidor.getDate()), that.f_pad(fechaServidor.getMonth() + 1), fechaServidor.getFullYear()].join("/"));

                    that.GetMotiveSOTByTypeJobAction(controls.cboTypeWork_Action.val());

                    if (oItem.Codigo == that.objLteUninstallInstallDeco.strNumeroUno || oItem.Codigo == that.objLteUninstallInstallDeco.strNumeroCero) {
                        that.objLteUninstallInstallDeco.hndValidateETA = oItem.Codigo;
                        that.objLteUninstallInstallDeco.hndHistoryETA = oItem.Codigo2;
                        Session.ValidateETA = oItem.Codigo;
                        Session.History = oItem.Codigo2;

                        if (oItem.Codigo == that.objLteUninstallInstallDeco.strNumeroUno) {
                            controls.cboSubTypeWork_Action.attr('disabled', false);
                            that.GetSubTypeWork_Action();


                        }
                        else {
                            alert("No aplica agendamiento en línea, favor de continuar con la operación.", "Alerta");
                            that.objLteUninstallInstallDeco.hndValidateETA = that.objLteUninstallInstallDeco.strNumeroCero;
                            controls.cboSubTypeWork_Action.html('<option value="-1">Seleccionar</option>').change();
                            controls.cboSubTypeWork_Action.attr('disabled', true);
                            Session.ValidateETA = "0";
                            Session.History = "";
                            that.scheduleStripeAction();
                        }
                    } else {
                        alert(that.objLteUninstallInstallDeco.strMensajeValidationETA, "Alerta");
                        that.objLteUninstallInstallDeco.hndValidateETA = that.objLteUninstallInstallDeco.strNumeroCero;

                        that.scheduleStripeAction();
                        that.objLteUninstallInstallDeco.hndHistoryETA = oItem.Codigo2;
                        Session.ValidateETA = "0";
                        Session.History = oItem.Codigo2;
                    }
                }
            });
        },

        cboMotiveSOTAction_change: function () {
            var that = this,
            controls = that.getControls();
            SessionTransf.hidenMotivoSotAction = controls.cboMotiveSOT_Action.val();
        },

        GetMotiveSOTByTypeJobAction: function (IdTipoTrabajo) {
            //
            var that = this,
                control = that.getControls(),
                param = {};

            param.strIdSession = Session.IDSESSION;
            param.IdTipoTrabajo = IdTipoTrabajo;

            $.ajax({
                type: "POST",
                url: "/Transactions/HFC/RetentionCancelServices/GetMotiveSOTByTypeJobs",
                data: JSON.stringify(param),
                contentType: "application/json; charset=utf-8",
                dataType: 'json',

                error: function (data) {
                    alert("Error JS : en llamar al GetMotiveSOTByTypeJob.", "Alerta");
                },

                success: function (response) {
                    control.cboMotiveSOT_Action.html("");
                    control.cboMotiveSOT_Action.append($('<option>', { value: '-1', html: 'Seleccionar' }));
                    if (response.data != null) {
                        $.each(response.data, function (index, value) {
                            control.cboMotiveSOT_Action.append($('<option>', { value: value.Codigo, html: value.Descripcion }));
                        });
                    }
                }
            });
        },
        f_pad: function (s) { return (s < 10) ? '0' + s : s; },

        GetDateAccion: function () {
            var that = this,
           controls = this.getControls();
            var fechaServidor = new Date(Session.ServerDate);
            var fechaServidorMas7Dias = new Date(fechaServidor.setDate(fechaServidor.getDate() + 7));
            controls.txtDateCommitmentAccion.val([that.pad(fechaServidorMas7Dias.getDate()), that.pad(fechaServidorMas7Dias.getMonth() + 1), fechaServidorMas7Dias.getFullYear()].join("/"));
        },


        //#endregion PROY-32650  II - Retención/Fidelización
        //#endregion
    };

    $('#txtTotInversion').on('keypress', function (e) {
        //
        var regexp = "";
        var field = $(this);
        var key = e.keyCode ? e.keyCode : e.which;

        if (key == 8) return true;
        if (key > 47 && key < 58) {
            if (document.getSelection() == field.val()) field.val("");
            if (field.val() === "") return true;
            var existePto = (/[.]/).test(field.val());
            if (existePto === false) {
                regexp = /.[0-9]{10}$/; //PARTE ENTERA
            }
            else {
                regexp = /.[0-9]{2}$/; //PARTE DECIMAL 2
            }
            return !(regexp.test(field.val()));
        }
        if (key == 46) {
            if (field.val() === "") return false;
            regexp = /^[0-9]+$/;
            return regexp.test(field.val());
        }
        return false;
    });

    $('#txtPenalidad').on('keypress', function (e) {
        //
        var regexp = "";
        var field = $(this);
        var key = e.keyCode ? e.keyCode : e.which;

        if (key == 8) return true;
        if (key > 47 && key < 58) {
            if (document.getSelection() == field.val()) field.val("");
            if (field.val() === "") return true;
            var existePto = (/[.]/).test(field.val());
            if (existePto === false) {
                regexp = /.[0-9]{10}$/; //PARTE ENTERA
            }
            else {
                regexp = /.[0-9]{2}$/; //PARTE DECIMAL 2
            }
            return !(regexp.test(field.val()));
        }
        if (key == 46) {
            if (field.val() === "") return false;
            regexp = /^[0-9]+$/;
            return regexp.test(field.val());
        }
        return false;
    });


    $.fn.LTERetentionCancelServices = function () {

        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {
            var $this = $(this),
                data = $this.data('LTERetentionCancelServices'),
                options = $.extend({}, $.fn.LTERetentionCancelServices.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('LTERetentionCancelServices', data);
            }

            if (typeof option === 'string') {
                if ($.inArray(option, allowedMethods) < 0) {
                    throw "Unknown method: " + option;
                }
                value = data[option](args[1]);
            } else {
                data.init();
                if (args[1]) {
                    value = data[args[1]].apply(data, [].slice.call(args, 2));
                }
            }

        });
        return value || this;
    };

    $.fn.LTERetentionCancelServices.defaults = {
    }

    $('#divBody').LTERetentionCancelServices();
})(jQuery);