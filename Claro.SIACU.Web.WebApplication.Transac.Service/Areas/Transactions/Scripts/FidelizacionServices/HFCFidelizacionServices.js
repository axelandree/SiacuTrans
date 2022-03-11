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
    SessionTransf.hdnValidaEta = "";
    SessionTransf.hdnTipoTrabCU = "";
    SessionTransf.hidClaseId = "";
    SessionTransf.hidSubClaseId = "";
    SessionTransf.hidTipo = "";
    SessionTransf.hidClaseDes = "";
    SessionTransf.hidSubClaseDes = "";
    SessionTransf.hdnFecAgCU = "";
    SessionTransf.hidFechaDefecto = "";
    SessionTransf.hidFechaServer = "";
    SessionTransf.hidFecMinimaCancel = "";
    SessionTransf.hidFechValida = "";
    SessionTransf.hdnCodigoRequestAct = "";

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
    SessionTransf.GeneroCaso = "";

    SessionTransf.hdnServName = "";
    SessionTransf.hdnLocalAdd = "";
    SessionTransf.UserHostName = "";
    SessionTransf.RutaArchivo = "";
    SessionTransf.Filename = "";
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

    SessionTransf.gConstMsgErrRecData = "";
    SessionTransf.gConstMsgErrCampNumeri = "";
    SessionTransf.gConstMsgSelCacDac = "";
    SessionTransf.gConstMensajeEsperaLoader = "";
    SessionTransf.AccesPage = "1";
    SessionTransf.gConstPerfHayCaso = "";
    SessionTransf.gConstFlagRetensionCancelacionEstado = "";
    SessionTransf.strFlagInhabTipTraMotSot = "";
    SessionTransf.strValueMotivoSOTDefecto = "";
    SessionTransf.strValueTipoTrabajoDefecto = "";
    SessionTransf.gConstFlagRetensionCancelacion = "";
    SessionTransf.hdnBajaTOTAL = "";
    SessionTransf.hdnValidado = "";
    SessionTransf.hdnFranjaHorariaCU = "";
    SessionTransf.hdnHaySubM = "";
    SessionTransf.strConsLineaDesaActiva = "DESACTIVO";
    SessionTransf.strMessageValidationETA = "";
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
    SessionTransf.strMsgTranGrabSatis = "La transacción se ha grabado satisfactoriamente.";
    SessionTransf.strMsgCreoInterErrTrans = "Se creó la interacción pero existe error en la transacción, el número insertado es: ";
    SessionTransf.gConstMsgLineaStatSuspe = "La linea no se encuentra activa";
    SessionTransf.strMensajeDeError = "No se pudo ejecutar la transacción. Informe o vuelva a intentar.";
    SessionTransf.strConstMsgNoSePProCanLi = "No se pudo Programar la Cancelación de la Línea";
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
    SessionTransf.hdnValorIgv = '0';
    SessionTransf.gConstMsgDescuentoActivo = "";
    SessionTransf.DE_SER = '';
    SessionTransf.strMontoDescuento = '0';
    //#endregion


    //#region PROY-32650  II - Retención/Fidelización
    SessionTransf.CODGRUPOSERV = '';
    SessionTransf.FranjaAccionDeco = '';
    SessionTransf.spCode = '';
    SessionTransf.IdEquipo = '';
    SessionTransf.CodTipoEquipo = '';
    SessionTransf.CantEquipos = '';
    SessionTransf.DE_GRP = '';  // 

    var AdditionalPointsModel = {};
    AdditionalPointsModel.strValidateETA = "";
    AdditionalPointsModel.strHistoryETA = "";
    AdditionalPointsModel.strJobTypeComplementarySalesHFC = "";
    AdditionalPointsModel.strInternetValue = "";
    AdditionalPointsModel.strCellPhoneValue = "";
    AdditionalPointsModel.strCustomerRequestId = "";


    SessionTransf.DecodificatorSelected = {};


    SessionTransf.nuevoCostoInstal = '0';
    SessionTransf.costoDescuentoInstal = '0';
    SessionTransf.gCantidadLimiteDeEquipos = '';
    SessionTransf.CantidadListaEquipos = '0';
    SessionTransf.strMsjCantidadLimiteDecos = '';
    SessionTransf.strShowChkPromAjustFact = '';
    //#endregion PROY-32650  II - Retención/Fidelización


    var Form = function ($element, options) {
        $.extend(this, $.fn.HFCFidelizacionServices.defaults, $element.data(), typeof options === 'object' && options);
        this.setControls({
            form: $element

            // Combo
            , cboCACDAC: $("#cboCACDAC", $element)
            , cboAccion: $("#cboAccion", $element)
            , cboMotCancelacion: $("#cboMotCancelacion", $element)
            , cboSubMotive: $("#cboSubMotive", $element)

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
            , lblDireccion: $("#lblDireccion", $element)
            , lblNotasDirec: $("#lblNotasDirec", $element)
            , lblDepartamento: $("#lblDepartamento", $element)
            , lblDistrito: $("#lblDistrito", $element)
            , lblCodPlano: $("#lblCodPlano", $element)
            , lblPais: $("#lblPais", $element)
            , lblProvincia: $("#lblProvincia", $element)
            , lblMensaje: $("#lblMensaje", $element)
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
            , divServAdicDeco: $("#divServAdicDeco", $element)

            , divReglas: $("#divReglas", $element)
            , divSubMotive: $("#divSubMotive", $element)
            , divErrorAlert: $("#divErrorAlert", $element)

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
            , tblAdiServBody: $("#tblAdiServBody", $element)
            //Fase 2
            , txtInstalationCost: $('#txtAmountIGV', $element)
            //#region PROY-32650  II - Retención/Fidelización
            , cboTypeWorkAccion: $("#cboTypeWorkAccion", $element)
            , cboSubTypeWorkAccion: $('#cboSubTypeWorkAccion', $element)
            , cboMotiveSOTAccion: $('#cboMotiveSOTAccion', $element)
            , txtDateCommitmentAccion: $("#txtDateCommitmentAccion", $element)
            , cboScheduleAccion: $("#cboScheduleAccion", $element)
            , divDecoder: $("#divDecoder", $element)
            // Modal
            , divFlatAccionFranja: $('#divFlatAccionFranja', $element)
            , ModalLoading: $('#ModalLoading', $element)
            , divchkPromFact: $('#divchkPromFact', $element)
            //#endregion PROY-32650  II - Retención/Fidelización 
            //#endregion

            //#region Proy-140319
            , txtInternetActual: $('#txtInternetActual', $element)
            , txtDescripcion: $('#txtDescripcion', $element)
            , txtVelocidadFinal: $('#txtVelocidadFinal', $element)
            , txtFechaActivacion: $('#txtFechaActivacion', $element)
            , txtVigencia: $('#txtVigencia', $element)
            , cboBonoFidDisponible: $('#cboBonoFidDisponible', $element)
            , cboVigBonoDisponible: $('#cboVigBonoDisponible', $element)
            , lblMensajeBonoInc: $('#lblMensajeBonoInc', $element)
            , lblMensajeBonoFull: $('#lblMensajeBonoFull', $element)
            , tbIntAct: $('#tbIntAct', $element)
            , tbDesc: $('#tbDesc', $element)
            //#endregion

            //INICIO - [PROY140496  IDEA-141399 Convivencia de bonos incremento de velocidad]
            , divBonoAumentaVelocidadFide: $('#divBonoAumentaVelocidadFide', $element)
	        , divSeccionBtnBonoDispVigBonoDisp: $('#divSeccionBtnBonoDispVigBonoDisp', $element)
            //FIN - [PROY140496  IDEA-141399 Convivencia de bonos incremento de velocidad]
        });
    };

    Form.prototype = {
        constructor: Form,

        init: function () {
            var that = this,
                controls = this.getControls();

            controls.cboMotCancelacion.addEvent(that, 'change', that.cboMotCancelacion_change);
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
            controls.cboBonoFidDisponible.addEvent(that, 'change', that.cboBonoFidDisponible_onchange);
            controls.cboVigBonoDisponible.addEvent(that, 'change', that.cboVigBonoDisponible_onchange);

            //#endregion PROY-32650  II - Retención/Fidelización 

            that.maximizarWindow();
            that.windowAutoSize();
            that.render();
            controls.divBonoAumentaVelocidadFide.hide();
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
            Smmry.set('FechaCompromiso', '');
            Smmry.set('Horario', '');
            Smmry.set('AplicaCaso', 'SI');

            Smmry.set('Accion', '');
            Smmry.set('Correo', '');
            Smmry.set('PuntoVenta', '');
            Smmry.set('Nota', '');

            control.divErrorAlert.hide();
            //Por Default Retenido
            SessionTransf.hidAccionTra = 'R';
            control.rdbRetenido.attr("checked", true);
            $('tr.retenido').hide();
            control.txtPenalidad.attr("readonly", true);
            control.txtPenalidad.val("0.00");
            control.txtTotInversion.val("0.00");
            //#region Proy-32650
            Smmry.set('Vigencia', '');
            Smmry.set('Descuento', '');
            control.txtCostInst.attr("readonly", true);
            control.txtCostInst.val("0.00");
            control.txtTotDescuento.attr("readonly", true);
            control.txtTotDescuento.val("0.00");
            //#endregion
            that.loadCustomerData();
            that.InitGetMessage();
            $("#lblPena").text("Penalidad (S/.) Con IGV");

            //#region PROY-32650  II - Retención/Fidelización


            $("#divFlatAccionFranja").css("display", "none");

            that.GetTypeWorkAccion();//nueva forma
            that.GetMotiveSOTAccion();
            that.GetDateAccion();
            that.f_GetParameter();
            that.tblAdiServBodyRow_Click();
            //#endregion PROY-32650  II - Retención/Fidelización

            //#region PROY-140319 III - Retencion/Fidelizacion
            Smmry.set('InternetActual', '');
            Smmry.set('BonoFidelizacion', '');
            //#endregion PROY-140319 III - Retencion/Fidelizacion
        },

        ShowChkPromAjustFact: function () {
            if (SessionTransf.strShowChkPromAjustFact == "0") {
                $("#divchkPromFact").css("display", "none");
            }
        },

        GetDateAccion: function () {
            var that = this,
           controls = this.getControls();
            var fechaServidor = new Date(Session.ServerDate);
            var fechaServidorMas7Dias = new Date(fechaServidor.setDate(fechaServidor.getDate() + 7));
            controls.txtDateCommitmentAccion.val([that.pad(fechaServidorMas7Dias.getDate()), that.pad(fechaServidorMas7Dias.getMonth() + 1), fechaServidorMas7Dias.getFullYear()].join("/"));
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
            Session.ServerDate = SessionTransf.FechaActualServidor;
            this.IniLoadPage();
        },


        GetOrderSubType: function () {
            var that = this,
                controls = that.getControls(),
                param = {};

            param.strIdSession = Session.IDSESSION;
            param.strTipoTrabajo = SessionTransf.strValueTipoTrabajoDefecto;

            $.ajax({
                type: "POST",
                url: '/Transactions/HFC/RetentionCancelServices/GetOrderSubType',
                data: JSON.stringify(param),
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                error: function (response) {
                    alert("ERROR JS : en llamar a GetOrderSubType.", "Alerta");

                },
                success: function (response) {
                    controls.cboSubTypeWork.html("");
                    controls.cboSubTypeWork.append($('<option>', { value: '-1', html: 'Seleccionar' }));

                    if (response.data != null) {
                        $.each(response.data, function (key, value) {
                            controls.cboSubTypeWork.append($('<option>', { value: value.Code, html: value.Description }));
                        });
                    }
                }
            });
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
                url: '/Transactions/HFC/FidelizacionServices/GetMessage',
                success: function (response) {
                    if (response != null) {
                        //
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

                        SessionTransf.gConstMsgErrRecData = response[14];
                        SessionTransf.gConstMsgErrCampNumeri = response[15];
                        SessionTransf.gConstMsgSelCacDac = response[16];
                        SessionTransf.gConstMensajeEsperaLoader = response[17];
                        SessionTransf.CambNumSinCosto = response[18];
                        SessionTransf.CostoCambioNumeroConsumer = response[19];
                        SessionTransf.SusTempSinCostoReconexion = response[20];
                        SessionTransf.MontoCobroReactivacionServicio = response[21];

                        SessionTransf.strEstadoContratoInactivo = response[22];
                        SessionTransf.strEstadoContratoReservado = response[23];
                        SessionTransf.strMsgValidacionContratoInactivo = response[24];
                        SessionTransf.strMsgValidacionContratoReservado = response[25];

                        SessionTransf.flagRestringirAccesoTemporalCR = response[26];
                        SessionTransf.gConstMsgOpcionTemporalmenteInhabilitada = response[27];

                        SessionTransf.strMsgDebeCargLinea = response[28];
                        SessionTransf.gConstMsgLineaStatSuspe = response[29];

                        SessionTransf.strConsLineaDesaActiva = response[30];
                        SessionTransf.strMessageValidationETA = response[31];
                        SessionTransf.gConstMsgDescuentoActivo = response[32]; //Proy-32650
                        SessionTransf.gCantidadLimiteDeEquipos = response[32]; //Proy-32650
                        SessionTransf.strMsjCantidadLimiteDecos = response[33]; //Proy-32650
                        SessionTransf.strShowChkPromAjustFact = response[34]; //Proy-32650
                        SessionTransf.gConstMsgSelBonoRetDisp = response[36];
                        SessionTransf.gConstMsgSelVigencia = response[37];
                        Session.VALIDAETA = "0";
                        Session.VALIDATE = "0";
                        //Session.RequestActId = "";
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
                url: '/Transactions/HFC/FidelizacionServices/LoadPage',
                async: false,
                error: function (data) {
                    alert("Error JS : en llamar al LoadPage.", "Alerta");
                },
                success: function (response) {

                    SessionTransf.FechaREsultado = response.FechaResultado;
                    SessionTransf.FlatReintegro = response.FlatReintegro;
                    SessionTransf.hidFlatMsj = response.data;
                    SessionTransf.PenalidadApadece = ((response.PenalidaAPADECE == "" || response.PenalidaAPADECE == undefined) ? "0.00" : response.PenalidaAPADECE);
                    SessionTransf.habilitaFecha = response.habilitaFecha;
                    SessionTransf.Message = response.Message;
                    SessionTransf.hdnValorIgv = response.valorIgv;//Proy-32650

                    if (SessionTransf.Message != "") {
                        controls.divErrorAlert.show();
                        controls.lblErrorMessage.text(SessionTransf.Message);
                    }


                    if (SessionTransf.hidFlatMsj) {

                        isPostBackFlag = NumeracionCERO;

                        Session.DeshabilitaIndi = NumeracionCERO;
                        SessionTransf.hidFechValida = new Date(new Date().getTime() + (2 * 24 * 3600 * 1000)).toLocaleDateString();


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
                        that.GetCurrentDiscountFixedCharge();//se puede mover al momento de cambiar a las acciones nuevas.
                        

                        //#endregion
                        controls.txtPenalidad.val((that.Round(SessionTransf.PenalidadApadece, 2)).toFixed(2));

                        if (SessionTransf.FlatReintegro) {
                            controls.txtPenalidad.attr("readonly", false);
                        } else {
                            controls.txtPenalidad.attr("readonly", true);
                        }


                        that.DeterminaSiHayCaso();
                    } else {
                        alert(SessionTransf.Message, "Alerta");
                    }
                },
            });
        },
        // Verifica el estado de las transacciones programandas

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
        //        url: '/Transactions/HFC/FidelizacionServices/GetTransactionScheduled',
        //        error: function (response) {
        //            alert("Error JS : en llamar a GetTransactionScheduled.", "Alerta");

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

        //                that.IniBegin();
        //                that.InitCacDat();
        //                that.getTypification();
        //            }
        //        }
        //    });
        //},

        // AccessPageProfile => Cargar desde las variables de Session
        DeterminaSiHayCaso: function () {
            SessionTransf.hayCaso = "1";
        },

        loadCustomerData: function () {
            var that = this;
            //
            var controls = that.getControls();
            controls = that.getControls();

            controls.lblTitle.text("Fidelización");
            var SessionTransac = JSON.parse(sessionStorage.getItem("SessionTransac"));

            Session.IDSESSION = '20170811110824824827183';
            Session.CLIENTE = SessionTransac.SessionParams.DATACUSTOMER;
            Session.LINEA = SessionTransac.SessionParams.DATASERVICE;
            Session.ACCESO = SessionTransac.SessionParams.USERACCESS;

            //
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
                    Segmento: Session.CLIENTE.CustomerType,
                    ProductType: Session.CLIENTE.ProductType,
                    LegalUrbanization: Session.CLIENTE.LegalUrbanization,
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
                };

            Session.DATALINEA =
                {
                    NroCelular: Session.LINEA.CellPhone,
                    StatusLinea: Session.LINEA.StateLine,
                    Plan: Session.LINEA.Plan,
                    Plazo_Contrato: Session.LINEA.TermContract,
                    StateAgreement: Session.LINEA.StateAgreement,
                };

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

            // Datos del Cliente
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
            // Dirección Facturación
            controls.lblDireccion.html((Session.DATACUSTOMER.Direccion_Despacho == null) ? '' : Session.DATACUSTOMER.Direccion_Despacho);
            controls.lblNotasDirec.html((Session.DATACUSTOMER.LegalUrbanization == null) ? '' : Session.DATACUSTOMER.LegalUrbanization);
            controls.lblPais.html((Session.DATACUSTOMER.Country == null) ? '' : Session.DATACUSTOMER.Country);
            controls.lblDepartamento.html((Session.DATACUSTOMER.Departament_Fact == null) ? '' : Session.DATACUSTOMER.Departament_Fact);
            controls.lblProvincia.html((Session.DATACUSTOMER.provincia == null) ? '' : Session.DATACUSTOMER.provincia);
            controls.lblDistrito.html((Session.DATACUSTOMER.District == null) ? '' : Session.DATACUSTOMER.District);
            controls.lblCodPlano.html((Session.DATACUSTOMER.PlaneCode == null) ? '' : Session.DATACUSTOMER.PlaneCode);
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

        cboSubMotive_change: function () {
            SessionTransf.hdnSubMot = ($('#cboSubMotive').val());
            SessionTransf.hdnSubMotDesc = $('#cboSubMotive option:selected').text();
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

        btnCerrar_Click: function () {
            parent.window.close();
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
                error: function (response) {
                    alert("Error JS : en llamar al GetParameterData.", "Alerta");
                },
                success: function (response) {
                    Value_C = response.data.Parameter.Value_C;
                },

            });

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
                that.loadPage();
                that.SaveTransactionFidelizacion();
            }, function () {
                $("#hidAccion").val("");
                return false;
            });
        },

        setControls: function (value) {
            this.m_controls = value;
        },

        getControls: function () {
            return this.m_controls || {};
        },


        rdbRetenido_click: function () {
            var that = this,
                control = that.getControls();

            SessionTransf.hidAccionTra = 'R';
            $('tr.retenido').hide();


            control.txtPenalidad.attr("readonly", true);
            control.txtPenalidad.val("0.00");

            that.CleanTypeJobAndMotiveSOT();
        },

        rdbNoRetenido_click: function () {
            var that = this,
                control = that.getControls();


            that.CleanTypeJobAndMotiveSOT();
        },

        VarlidateControl: function () {
            var that = this,
                control = that.getControls();
            //
            if (control.rdbNoRetenido[0].checked || control.rdbRetenido[0].checked) {

            } else {
                alert(SessionTransf.gConstMsgSelTr, "Alerta");
                return false;
            }

            if (control.cboMotCancelacion.val() == "-1") {
                alert(SessionTransf.gConstMsgSelMot, "Alerta");
                return false;
            }

            if (control.cboAccion.val() == "-1") {
                alert(SessionTransf.gConstMsgSelAc, "Alerta");
                return false;
            }

            if (control.cboCACDAC.val() == "-1") {
                alert(SessionTransf.gConstMsgSelCacDac, "Alerta");
                return false;
            }

            if ($.isNumeric(control.txtPenalidad.val()) && $.isNumeric(control.txtTotInversion.val())) {

            } else {
                alert(SessionTransf.gConstMsgErrCampNumeri, "Alerta");
                return false;
            }


            if ($("#chkEmail").prop("checked")) {
                if ($("#txtEmail").val() == "") {
                    alert("Ingresar email", 'Alerta', function () {
                        control.txtEmail.focus();
                    }); return false;
                }

                var regx = /^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$/;
                var blvalidar = regx.test($("#txtEmail").val());
                if (!blvalidar) {
                    alert("Ingresar email válido", 'Alerta', function () {
                        control.txtEmail.select();
                    }); return false;
                }
            }

            return true;
        },


        BlockControl: function () {
            //
            $("#txtPenalidad").attr("disabled", true);
            $("#txtTotInversion").attr("disabled", true);
            $("#rdbRetenido").attr("checked", false);
            $("#rdbNoRetenido").attr("checked", false);

            $("#cboMotCancelacion").attr("disabled", true);

            $("#cboAccion").attr("disabled", true);

            $("#txtNota").attr("disabled", true);
            $("#txtNota").val();

        },

        SaveTransactionFidelizacion: function () {
            var that = this,
                controls = that.getControls(),
                strUrl = '',
                model = {},
                modelTifi = {};
            //
            model.CicloFacturacion = Session.DATACUSTOMER.Ciclo_Facturacion;
            model.DesMotivos = $('#cboMotCancelacion option:selected').text();
            model.DesAccion = $('#cboAccion option:selected').text();
            model.ValAccion = SessionTransf.idAccion;
            model.Accion = SessionTransf.hidAccionTra;
            model.Destinatarios = controls.txtEmail.val();
            model.hidSupJef = SessionTransf.hidSupJef;
            model.Telephone = Session.DATALINEA.NroCelular;
            model.DescCacDac = $('#cboCACDAC option:selected').text();
            model.CacDac = $('#cboCACDAC option:selected').val();
            model.TotalInversion = controls.txtTotInversion.val();
            model.PagoAPADECE = SessionTransf.hidPagoAPADECE
            model.RazonSocial = Session.DATACUSTOMER.RazonSocial;
            model.TypeClient = Session.DATACUSTOMER.TipoCliente;
            model.NameComplet = Session.DATACUSTOMER.NameCompleto;
            model.RepresentLegal = Session.DATACUSTOMER.RepresentLegal;
            model.NroDoc = Session.DATACUSTOMER.Nro_Doc;
            model.DNI_RUC = Session.DATACUSTOMER.DNI_RUC;
            model.TypeDoc = Session.DATACUSTOMER.TipoDocumento;
            model.TelefonoReferencia = Session.DATACUSTOMER.telefono_Referencial;
            model.AdressDespatch = Session.DATACUSTOMER.Direccion_Despacho;
            model.Reference = Session.DATACUSTOMER.refencial;
            model.Departament_Fact = Session.DATACUSTOMER.Departament_Fact;
            model.District_Fac = Session.DATACUSTOMER.District_Fac;
            model.Pais_Fac = Session.DATACUSTOMER.Country_Fac;
            model.Provincia_Fac = Session.DATACUSTOMER.Provincia_Fac;

            if (controls.cboSubMotive.val() != '-1')
                model.DesSubMotivo = $('#cboSubMotive option:selected').text();
            else
                model.DesSubMotivo = '';

            model.NroCelular = Session.DATALINEA.NroCelular;// validar 
            model.Note = $('#txtNote').val();

            model.Email = controls.txtEmail.val();
            model.Reintegro = controls.txtPenalidad.val();
            model.Objid_Site = Session.DATACUSTOMER.Objid_Site;
            model.ContractId = Session.DATACUSTOMER.ContratoID;
            model.Sn = SessionTransf.hdnServName;
            model.IpServidor = SessionTransf.hdnLocalAdd;
            model.CustomerId = Session.DATACUSTOMER.CustomerID;
            model.IdSession = Session.IDSESSION;
            model.Plan = Session.DATACUSTOMER.PlaneCode;
            model.CurrentUser = Session.USERACCESS.Login;
            //
            if (controls.chkEmail[0].checked) {
                model.Flag_Email = 'true';
            } else {
                model.Flag_Email = 'false';
            }
            model.fechaActual = SessionTransf.FechaActualServidor; //validar
            model.vSchedule = controls.cboScheduleAccion.val();  //---->NORETENIDO
            model.CodTypeCustomer = Session.DATACUSTOMER.TipoCliente;
            model.hdnSubMotDesc = SessionTransf.hdnSubMotDesc;
            model.vServicesType = "";
            model.vMotiveSot = controls.cboMotiveSOTAccion.val();//---->NORETENIDO
            model.vJobTypes = controls.cboTypeWorkAccion.val();//---->NORETENIDO
            model.vValidateETA = Session.VALIDAETA; //1
            model.Cuenta = Session.DATACUSTOMER.Cuenta;
            model.Account = Session.DATACUSTOMER.Cuenta;
            model.StardDate = new Date().toLocaleDateString();
            model.FecAgCU = SessionTransf.hdnFecAgCU;    //---->NORETENIDO-VER
            model.EndDate = "";
            model.District = Session.DATACUSTOMER.District;
            model.Country = Session.DATACUSTOMER.Country;
            model.FechaCompromiso = controls.txtDateCommitmentAccion.val(); // validar


            
            model.CodePlanInst = Session.DATACUSTOMER.Codigo_Plano_Inst;
            //#region Proy-32650

            //region PROY-32650  II - Retención/Fidelización
            model.PlanActual = Session.DATALINEA.Plan
            model.EstadoLinea = Session.DATALINEA.StatusLinea;
            model.Ubigeo = Session.DATACUSTOMER.Ubigueo
            //endregion PROY-32650  II - Retención/Fidelización

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
            model.PaqueteODeco = controls.strPaqueteDeco;
            

            if (Session.DATACUSTOMER.Email == '' && (model.Flag_Email == 'true'))
                model.updateDataMen = true;
            else
                model.updateDataMen = false;

            if (SessionTransf.idAccion == strIdCargoFijo) {
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
                SessionTransf.snCode = SessionTransf.DecodificatorSelected.SNCODE;
                SessionTransf.codServAdic = SessionTransf.DecodificatorSelected.CO_SER;
                SessionTransf.descServAdic = SessionTransf.DecodificatorSelected.DESCOSER;
                SessionTransf.strTarifRegular = SessionTransf.DecodificatorSelected.COSTOPVU;
                SessionTransf.strTarifRet = SessionTransf.DecodificatorSelected.CARGOFIJO;
                SessionTransf.strMontoDescuento = SessionTransf.DecodificatorSelected.COSTOPVU - SessionTransf.DecodificatorSelected.CARGOFIJO;
                SessionTransf.strMontoDescuento = SessionTransf.strMontoDescuento - (SessionTransf.strMontoDescuento * parseFloat(SessionTransf.hdnValorIgv));
                SessionTransf.DE_SER = SessionTransf.DecodificatorSelected.DE_SER;

                // model.montoTotalSA = '';
                model.codServAdic = SessionTransf.codServAdic; // codigo del servicio seleccionado en la grilla
                model.descServAdic = SessionTransf.descServAdic; // descripcion del servicio adicional seleccionado(grilla)
                model.desServicioPVU = SessionTransf.DE_SER;//descServAdic; // descripcion del servicio adicional en PVU (no se muestra en la grilla(viene del select))
                model.montTariRete = SessionTransf.strTarifRet;//  enviar la tarifa de retencion de la grilla seleccionada
                model.mesVal = $('#cboMonthSA option:selected').val(); // 
                model.mesDesc = $('#cboMonthSA option:selected').text(); // 
                model.snCode = SessionTransf.snCode; // el snCode del servicio seleccionado en grilla
                model.costoServiciosinIGV = parseFloat(SessionTransf.strTarifRet) / parseFloat(1 + parseFloat(SessionTransf.hdnValorIgv)); // falta el metodo que obtiene 
                model.costoServicioconIGV = SessionTransf.strTarifRet;  // datos del rf3 - grilla ??  ---> ???--DUDA--???
                model.flagCargFijoServAdic = '1'; // 1 : Descuento servicio adicional
                model.DiscountDescription = $('#cboDiscountSA option:selected').text();  // descripcion % descuento combo
                model.RetentionBonusServAdic = SessionTransf.strTarifRet;
                model.RegularBonusServAdic = SessionTransf.strTarifRegular;
                model.flagServDeco = '0'; //NO MOVER si es Serv Adic es 0
                model.costInst = '0';
                model.MontoDescuento = SessionTransf.strMontoDescuento;

                //region PROY-32650  II - Retención/Fidelización
                SessionTransf.CODGRUPOSERV = SessionTransf.DecodificatorSelected.CODGRUPOSERV;
                SessionTransf.spCode = SessionTransf.DecodificatorSelected.SPCODE;
                SessionTransf.CantEquipos = SessionTransf.DecodificatorSelected.CANTEQUIPO;
                SessionTransf.CodTipoEquipo = SessionTransf.DecodificatorSelected.CODTIPOEQUIPO;
                SessionTransf.IdEquipo = SessionTransf.DecodificatorSelected.IDEQUIPO;
                SessionTransf.DE_GRP = SessionTransf.DecodificatorSelected.DE_GRP

                //endregion PROY-32650  II - Retención/Fidelización

                //region PROY-32650  II - Retención/Fidelización
                model.ValorIGV = SessionTransf.hdnValorIgv;
                //endregion PROY-32650  II - Retención/Fidelización
            } else if (SessionTransf.idAccion == strIdAumentoVelocidad) {
                model.flagCargFijoServAdic = '2'; // 2 : Aumento de velocidad
                model.BonoId = $('#cboBonoFidDisponible option:selected').val();
                model.CodId = Session.DATACUSTOMER.ContratoID;//Session.DATACUSTOMER.idContrato;
                model.PeriodoBono = $('#cboVigBonoDisponible option:selected').val();
                model.InternetActual = $('#txtInternetActual').val();
                model.BonoRetentionFidelizacion = $('#cboBonoFidDisponible option:selected').text();
                model.VigenciaRetFid = $('#cboVigBonoDisponible option:selected').html();
            }

            //--------------------------------------------------------------------------------------------
            model.CodigoAsesor = Session.ACCESO.login;
            model.NombreAsesor = Session.ACCESO.fullName;
            model.Transaction = 'Fidelización'; //Transaccion:Fidelizacion o Solicitud de Cancelacion/ Ivan
            model.ReferenceOfTransaction = controls.rdbRetenido[0].checked == true ? 'Retenido' : 'Cancelado'; // RESULTADO
            model.Segmento = Session.DATACUSTOMER.Segmento; /*== 'Consumer' ? 'MASIVO' : 'CORPORATIVO';*/
            model.Reintegro = controls.txtPenalidad.val();
            model.Constancia = controls.rdbRetenido[0].checked == true ? 'Retención' : 'Cancelación';
            model.DateProgrammingSot = controls.txtDateCommitmentAccion.val();

            //#region PROY-32650  II - Retención/Fidelización

            if (SessionTransf.isDecodificator) {
                model.flagCargFijoServAdic = '0';
                model.flagServDeco = '1';
                model.FechaActivacion = Session.DATACUSTOMER.FechaActivacion;
                model.costInst = SessionTransf.nuevoCostoInstal;//costo para tipis y constancia
                model.costoWSInst = SessionTransf.costoDescuentoInstal;//costo para registro de bonos y ws de hfc
                model.vMotiveSot = $('#cboMotiveSOTAccion option:selected').text().substring(0, 40);
                model.ValidaETA = AdditionalPointsModel.strValidateETA;
                var deco = {
                    id: '',
                    desc: SessionTransf.DecodificatorSelected.DESCOSER,
                    tipodeco: "",
                    Costosing: parseFloat(SessionTransf.strTarifRegular) / parseFloat(1 + parseFloat(SessionTransf.hdnValorIgv)),
                    Costocigv: SessionTransf.DecodificatorSelected.COSTOPVU,
                    CodeService: SessionTransf.DecodificatorSelected.CO_SER,
                    SnCode: SessionTransf.DecodificatorSelected.SNCODE,
                    Cf: SessionTransf.DecodificatorSelected.CARGOFIJO,
                    SpCode: SessionTransf.DecodificatorSelected.SPCODE,
                    ServiceName: SessionTransf.DecodificatorSelected.DE_SER,
                    ServiceType: SessionTransf.DecodificatorSelected.TIPO_SERVICIO,
                    ServiceGroup: SessionTransf.DecodificatorSelected.DE_GRP,
                    Equipment: "",
                    Quantity: SessionTransf.DecodificatorSelected.CANTEQUIPO,
                    Associated: "",
                    CodeInsSrv: "",
                    SerialNumber: "",
                    Flag: "",
                    CodServicePvu: SessionTransf.DecodificatorSelected.CODSERPVU,//Codigo PVU
                    IdEquipo: SessionTransf.DecodificatorSelected.IDEQUIPO,
                    CodTipEquipo: SessionTransf.DecodificatorSelected.CODTIPOEQUIPO
                };

                var vModel =
                {
                    Decos: [deco],

                    InsInteractionPlusModel: {
                        District: Session.CLIENTE.LegalDistrict,
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

                    RegistrarEtaSeleccion: {
                        IdConsulta: Session.RequestActId,
                        IdInteraccion: ("0000000000" + Session.RequestActId).slice(-10),
                        FechaCompromiso: controls.txtDateCommitmentAccion.val(),
                        Franja: controls.cboScheduleAccion.val().split('+')[0],
                        Idbucket: controls.cboScheduleAccion.val().split('+')[1],
                    },
                    RegistrarEta: {
                        IdPoblado: Session.DATACUSTOMER.PlaneCode,//Session.CLIENTE.CodeCenterPopulate,solo para HFC se considera plano.
                        DniTecnico: '',
                        Franja: controls.cboScheduleAccion.val().split('+')[0],
                        Idbucket: controls.cboScheduleAccion.val().split('+')[1],
                        IpCreacion: '',
                        SubTipoOrden: controls.cboSubTypeWorkAccion.val().split('|')[2],
                        UsrCrea: Session.ACCESO.login,
                        FechaProg: controls.txtDateCommitmentAccion.val(),
                    }
                };

                model = { oModel: model, objViewModel: vModel };

                strUrl = '/Transactions/HFC/FidelizacionServices/SaveTransactionRetentionDeco';
             
                if (SessionTransf.idAccion == strIdAumentoVelocidad) {
                  
                    model.flagCargFijoServAdic = '2'; // 2 : Aumento de velocidad
                }
            } else {
               
                strUrl = '/Transactions/HFC/FidelizacionServices/SaveTransactionFidelizacion';
            }


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
                    SessionTransf.hFlatInteraccion = response.vFlagInteraction;
                    SessionTransf.InteractionId = response.vInteractionId;
                    SessionTransf.RutaArchivo = response.strRutaArchivo;
                    SessionTransf.MensajeEmail = response.MensajeEmail;

                    if (SessionTransf.InteractionId != '' && SessionTransf.InteractionId != "null") {

                        $("#btnConstancia").attr('disabled', false); //Activa
                        alert(SessionTransf.strMsgTranGrabSatis, "Informativo");


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

        GetRegisterCustomerId: function () {
            var that = this,
                controls = that.getControls(),
                objType = {};



            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objType),
                url: '/Transactions/HFC/FidelizacionServices/GetRegisterCustomerId',
                success: function (response) {
                    CodError = response.data.vFlagConsulta;
                    DesError = response.data.rMsgText;
                    FlatResultado = response.data.Resultado;

                },


            });

        },

        SetDate: function () {
            $("#txtDateCommitmentAccion").val(SessionTransf.hidFechaProg);
            SessionTransf.hdnFecAgCU = SessionTransf.hidFechaProg;
        },

        InitAccion: function () {
            var that = this,
                controls = that.getControls(),
                objLstAccionType = {},
                arrCod;
            objLstAccionType.strIdSession = Session.IDSESSION;
            ////
            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objLstAccionType),
                url: '/Transactions/CommonServices/GetListarAccionesRC',
                success: function (response) {
                    //
                    controls.cboAccion.append($('<option>', { value: '', html: 'Seleccionar' }));
                    if (response.data != null) {
                        $.each(response.data, function (index, value) {
                            arrCod = value.Code.split('|');
                            if (strIdCargoFijo === arrCod[0] ||
                                strIdServicioAdicional === arrCod[0] ||
                                strIdAumentoVelocidad === arrCod[0]) {
                                controls.cboAccion.append($('<option>', { value: value.Code, html: value.Description }));
                            }
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
                url: '/Transactions/HFC/FidelizacionServices/GetMotCancelacion',
                success: function (response) {
                    //
                    controls.cboMotCancelacion.append($('<option>', { value: '-1', html: 'Seleccionar' }));
                    if (response.data != null) {
                        $.each(response.data, function (index, value) {
                            controls.cboMotCancelacion.append($('<option>', { value: value.Code, html: value.Description }));
                        });
                    }
                }
            });
        },

        cboMotCancelacion_change: function () {
            var that = this,
                controls = that.getControls(),
                param = {};

            SessionTransf.hdnMotivo = $('#cboMotCancelacion option:selected').text();
            param.strIdSession = Session.IDSESSION;
            param.IdMotive = controls.cboMotCancelacion.val();
            //
            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(param),
                url: '/Transactions/HFC/FidelizacionServices/GetSubMotiveCancel',
                success: function (response) {
                    controls.cboSubMotive.html("");
                    controls.cboSubMotive.append($('<option>', { value: '-1', html: 'Seleccionar' }));
                    if (response.data != null) {
                        //
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

        Round: function (cantidad, decimales) {
            var cantidad = parseFloat(cantidad);
            var decimales = parseFloat(decimales);
            decimales = (!decimales ? 2 : decimales);
            return Math.round(cantidad * Math.pow(10, decimales)) / Math.pow(10, decimales);
        },

        getTypification: function () {
            var that = this, controls = that.getControls();
            var transact = gConstCodigoTransRetCanServHFC;
            //
            $.app.ajax({
                type: "POST",
                url: "/Transactions/CommonServices/GetTypification",
                data: {
                    strIdSession: Session.IDSESSION,
                    strTransactionName: transact
                },
                success: function (result) {
                    var list = result.ListTypification;
                    if (list != null) {
                        if (list.length > 0) {
                            that.getBusinessRules(list[0].SUBCLASE_CODE);
                        }
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
                    strSubClase: SubClaseCode,

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
                        alert('No existe el archivo.', "Alerta");
                    } else {
                        var url = that.strUrl + '/GenerateRecord/showInvoice';
                        window.open(url + "?strFilePath=" + Session.filepath + "&strFileName=" + Session.Filename + "&strNameForm=" + "NO" + "&strIdSession=" + Session.IDSESSION, "FACTURA ELECTRÓNICA", "");
                    }

                },
                error: function (ex) {
                    alert('No existe el archivo.', "Alerta");
                }
            });

        },

        btnSummaryDT_click: function (e) {

            var that = this,
                control = that.getControls();
            var Flag = 'true';

            if (control.rdbNoRetenido[0].checked || control.rdbRetenido[0].checked) {



            } else {
                alert(SessionTransf.gConstMsgSelTr, "Alerta");
                Flag = 'false';
                return false;
            }

            if ($.isNumeric(control.txtPenalidad.val())) {
                control.txtPenalidad.val(parseFloat(control.txtPenalidad.val()).toFixed(2));

            } else {
                alert('Ingresar penalidad.', "Alerta");
                Flag = 'false';
                return false;
            }

            if (Flag == 'true') {
                navigateTabs(e);
            }



        },

        btnSummaryRetenido_click: function (e) {
            var that = this,
                control = that.getControls();

            if (control.cboMotCancelacion.val() == "-1" || control.cboMotCancelacion.val() == null) {
                alert('Seleccionar motivo de fidelización.', "Alerta");
                return false;
            }


            if ($('#divSubMotive').css('display') != 'none') {

                if (control.cboSubMotive.val() == "-1" || control.cboSubMotive.val() == null) {
                    alert(SessionTransf.gConstMsgSelSubMot, "Informativo");
                    return false;
                }
            }


            if ($.isNumeric(control.txtTotInversion.val())) {
                control.txtTotInversion.val(parseFloat(control.txtTotInversion.val()).toFixed(2));

            } else {
                alert('Ingresar el total de inversión.', "Alerta");
                return false;
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
                if ($("#txtEmail").val() == '') {
                    alert('Ingresar email.', 'Alerta', function () {
                        control.txtEmail.focus();
                    }); return false;
                }

                var regx = /^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$/;
                var blvalidar = regx.test($("#txtEmail").val());
                if (!blvalidar) {
                    alert('Ingresar email válido', 'Alerta', function () {
                        control.txtEmail.select();
                    }); return false;
                }
            }

            if ($('#cboCACDAC option:selected').html() == "Seleccionar" || control.cboCACDAC.val() == null) {
                alert(SessionTransf.gConstMsgSelCacDac, "Alerta");
                return false;
            }


            //#region Proy-32650
            if ((SessionTransf.idAccion == strIdCargoFijo) || (SessionTransf.idAccion == strIdServicioAdicional)){
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
            if (SessionTransf.idAccion == strIdAumentoVelocidad) {
                if ($('#cboBonoFidDisponible option:selected').html() == "Seleccionar" || control.cboBonoFidDisponible.val() == null) {
                    alert('Seleccione un Bono Disponible', "Alerta");
                    validaRetencion = false;
                    return false;
                }
                if ($('#cboVigBonoDisponible option:selected').html() == "Seleccionar" || control.cboVigBonoDisponible.val() == null) {
                    alert('Seleccione una Vigencia', "Alerta");
                    validaRetencion = false;
                    return false;
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


                //#region PROY-32650  II - Retención/Fidelización
                SessionTransf.snCode = SessionTransf.DecodificatorSelected.SNCODE;
                SessionTransf.codServAdic = SessionTransf.DecodificatorSelected.CO_SER;//CodServSisact;
                SessionTransf.descServAdic = SessionTransf.DecodificatorSelected.DESCOSER;//DesServSisact;
                SessionTransf.strTarifRegular = SessionTransf.DecodificatorSelected.COSTOPVU;//CF;
                SessionTransf.strTarifRet = SessionTransf.DecodificatorSelected.CARGOFIJO;//TarifaRetencion;
                SessionTransf.DE_SER = SessionTransf.DecodificatorSelected.DE_SER;
                SessionTransf.strMontoDescuento = SessionTransf.DecodificatorSelected.COSTOPVU - SessionTransf.DecodificatorSelected.CARGOFIJO;//TarifaRetencion;


                if (arrCod[0] == strIdServicioAdicional && SessionTransf.DecodificatorSelected.CODGRUPOSERV == '8' && SessionTransf.DecodificatorSelected.TIPO_SERVICIO == 'ADICIONAL') {

                    if ($('#cboTypeWorkAccion option:selected').html() == "Seleccionar" || control.cboTypeWorkAccion.val() == "-1") {
                        alert("Seleccionar Tipo de Trabajo.", "Alerta");
                        return false;
                    }
                    if ($('#cboSubTypeWorkAccion').prop("disabled") == false) {
                        if ($('#cboSubTypeWorkAccion option:selected').html() == "Seleccionar" || control.cboSubTypeWorkAccion.val() == "-1") {
                            alert("Seleccionar SubTipo de Trabajo.", "Alerta");
                            return false;
                        }
                    }
                    if ($('#cboScheduleAccion option:selected').html() == "Seleccionar" || control.cboScheduleAccion.val() == "-1") {
                        alert("Seleccionar Horario(*).", "Alerta");
                        return false;
                    }

                    if ($('#txtCostInst').text() > 0) {
                        if ($('#cboDiscountSA option:selected').html() == "Seleccionar" || control.cboDiscountSA.val() == null) {
                            alert("Seleccionar porcentaje de descuento.", "Alerta");
                            validaRetencion = false;
                            return false;
                        }
                    }

                    //Validar Cantidad Maxima de Decos
                    if (SessionTransf.isDecodificator) {
                        if ((SessionTransf.CantidadListaEquipos + 1) > SessionTransf.gCantidadLimiteDeEquipos) {
                            var mensaje = SessionTransf.strMsjCantidadLimiteDecos.replace("_num_", SessionTransf.gCantidadLimiteDeEquipos);
                            alert(mensaje, "Alerta");
                            return false;
                        }
                    }

                    that.SummaryTipoTrabajoAccion();
                    that.SummaryMotivoSotAccion();
                    that.SummarySubTipoTrabajoAccion();
                    that.SummaryFechaCompromisoAccion();
                    that.SummaryHorarioAccion();

                    $('tr.retenido').show();
                }


                //#endregion PROY-32650  II - Retención/Fidelización

            }
            //#endregion
            if (validaRetencion) {
                that.SummaryRetenido();
                that.SummaryMotivoCancelacion();
                that.SummaryTotalInversion();
                that.SummarySubMotivo();
                that.SummaryAccion();
                that.SummaryCorreo();
                that.SummaryPuntoVenta();
                that.SummaryNota();
                //#region Proy-32650
                that.SummaryDescuento();
                that.SummaryCosto();
                that.SummaryTotalDescuento();
                that.SummaryVigencia();
                    //#region PROY-140319
                    that.SummaryInternetActual();
                    that.SummaryDescripcion();
                    that.SummaryVigenciaFid();
                //#endregion


            if ($(e).attr('id') == 'btnSummary02') {
                $('.btn-circle.transaction-button').removeClass('transaction-button').addClass('btn-default');
                $(e).addClass('transaction-button').removeClass('btn-default').blur();
                var percent = $(e).attr('percent');
                document.getElementById('prog').style.width = percent;
            } else {

                        if (SessionTransf.idAccion == strIdAumentoVelocidad) {
                            if (SessionTransf.hidAccionTra == 'R') {
                                    navigateTabs(e);
                            }
                        } else {
                          
                navigateTabs(e);
            }

                    }
                }
        },

        SummaryRetenido: function () {
            var that = this,
                controls = that.getControls();

            if (controls.rdbRetenido[0].checked == true) {
                Smmry.set('Penalidad', controls.txtPenalidad.val());
                Smmry.set('Retenido', 'Fidelizado');
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
        // Condicion para mostrar en Summary el servicio de internet activo actual del cliente junto a la velocidad.
        SummaryInternetActual: function () {
            var that = this,
                controls = that.getControls();
            controls.tbIntAct.show();
            if ($.trim(controls.txtInternetActual.val()) != "") {
                if (SessionTransf.idAccion == strIdAumentoVelocidad) {

                    Smmry.set('InternetActual', controls.txtInternetActual.val());
                } else {
                    Smmry.set('InternetActual', '');
                }
            }
        },
        // Condicion para mostrar en Summary la descripcion.
        SummaryDescripcion: function () {
            var that = this,
                controls = that.getControls();
            controls.tbDesc.show();
            if ($.trim(controls.cboBonoFidDisponible.val()) != "") {
                if (SessionTransf.idAccion == strIdAumentoVelocidad) {
                    Smmry.set('Descripcion', $('#cboBonoFidDisponible option:selected').html());
                } else {
                    Smmry.set('Descripcion', '');
                }
            }
        },
        // Condicion para mostrar en Summary la velocidad de MB de los bonos de Incremento de Velocidad disponibles para el cliente.
        SummaryVigenciaFid: function () {
            var that = this,
                controls = that.getControls();
            if (controls.cboVigBonoDisponible.val() != null) {
                if ($('#cboVigBonoDisponible option:selected').html() != 'Seleccionar'){
                    if (SessionTransf.idAccion == strIdAumentoVelocidad) {
                        Smmry.set('Vigencia', $('#cboVigBonoDisponible option:selected').html());
                    } else
                    {
                        Smmry.set('Vigencia', '');
                    }
                 }

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

        //#region Proy-32650 f2
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

        SummaryFechaCompromisoAccion: function () {
            var that = this,
                controls = that.getControls();

            if (controls.txtDateCommitmentAccion.val() != "-1") {
                Smmry.set('FechaCompromiso', $('#txtDateCommitmentAccion').val());
            }
            else {
                Smmry.set('FechaCompromiso', '');
            }
        },

        SummaryHorarioAccion: function () {
            var that = this,
                controls = that.getControls();

            if (controls.cboScheduleAccion.val() != "-1") {
                Smmry.set('Horario', $('#cboScheduleAccion option:selected').html());
            }
            else {
                Smmry.set('Horario', '');
            }
        },

        //#region Proy-32650 f2

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
            ////
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
            ////
            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objLstMonths),
                url: '/Transactions/CommonServices/GetMonths',
                success: function (response) {
                    //
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
            ////
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
            ////
            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objLstAccionType),
                url: '/Transactions/CommonServices/GetListDiscount',
                success: function (response) {
                    //
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
        strValidaBonoAsignado: "",
        strValidaBono: "",
        cboAccion_change: function () {
            var that = this,
                controls = that.getControls(),
                value = $('#cboAccion option:selected').val(),
                arrCod = value.split('|');
            SessionTransf.idAccion = arrCod[0];
            controls.btnSummaryAccion.attr('disabled', false); //INC000004498068
            document.getElementById("lblTotDescuento").style.display = 'none';
            document.getElementById("txtTotDescuento").style.display = 'none';
            $('#divFlatAccionFranja').hide();
            $('#divCostoInstalacion').hide();
            if (arrCod[0] == strIdCargoFijo || arrCod[0] == strIdServicioAdicional) {

                document.getElementById("lblTotDescuento").style.display = 'block';
                document.getElementById("txtTotDescuento").style.display = 'block';

                if (arrCod[0] == strIdCargoFijo) {  //divDesCargFijo
                    controls.divDesCargFijo.show();
                    controls.divAditionalServices.hide();
                    controls.divBonoAumentaVelocidadFide.hide();
                    controls.divFlatAccionFranja.hide();
                    that.InitDiscountCF();//se movio, porque solo deberia ser invocado desde la nueva accion
                    that.InitMonthCF();
                } else if (arrCod[0] == strIdServicioAdicional) { //divDesServAdic
                    that.GetProductDetail();
                    controls.divDesCargFijo.hide();
                    controls.divAditionalServices.show();
                    controls.divBonoAumentaVelocidadFide.hide();
                    that.GetCommercialServices();
                    that.GetInstallCost();
                    that.InitMonthSA();
                    that.InitDiscountAS();
                }
                if ((strValidaDescuentoActivo == '') || (strValidaDescuentoActivo == undefined)) {
                }
                else {

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
                controls.divFlatAccionFranja.hide();
                controls.divBonoAumentaVelocidadFide.hide();
                $('#divFlatAccionFranja').hide();
                $('#divCostoInstalacion').hide();
            }

            // Validacion de mensaje para Bono Aumento Velocidad
            if (arrCod[0] == strIdAumentoVelocidad) {
                document.getElementById("lblTotDescuento").style.display = 'block';
                document.getElementById("txtTotDescuento").style.display = 'block';
                $('#divFlatAccionFranja').hide();

                $('#divCostoInstalacion').hide();
                controls.divBonoAumentaVelocidadFide.show();
                controls.divAditionalServices.hide();
                controls.divDesCargFijo.hide();
                that.GetConsultServiceBono(function () {
                    let letValidaBono = that.strValidaBono;
                    let flagbonoac = that.flagBonoActual;
                    controls.btnSummaryAccion.attr('disabled', true);
                    if (strMonthsFide == '0') {
                    if (letValidaBono != '' || flagbonoac >= 0) {
                        controls.lblMensajeBonoInc.text("");
                        if (that.flagBonoActual == "0") {
                                alert(letValidaBono, 'Aviso');
                                controls.divBonoAumentaVelocidadFide.show();
                                controls.divSeccionBtnBonoDispVigBonoDisp.show();
                                controls.cboBonoRetDisponible.attr('disabled', false);
                                controls.cboVigBonoDisponible.attr('disabled', false);
                                controls.btnSummaryAccion.attr('disabled', true);
                              }
                            if (that.flagBonoActual == 2) {
                                alert(letValidaBono, 'Aviso');
                                controls.divBonoAumentaVelocidadFide.show();
                                controls.cboBonoFidDisponible.attr('disabled', false);
                                controls.cboVigBonoDisponible.attr('disabled', false);
                                controls.btnSummaryAccion.attr('disabled', true);
                             }
                        }

                    } else {
                        that.PostValidateDeliveryBAVFide(function () {
                            let strMsjInfoValidateDeliveryBAVFide = that.strMsjInfoValidateDeliveryBAVFide;
                            let letFlagAplica = that.flagAplica;
                            if (letValidaBono != '' || letFlagAplica >= 0) {
                                if (letFlagAplica == 0) {
                                    if (letValidaBono != '' || flagbonoac >= 0) {
                                        if (that.flagBonoActual == "0") {
                                            alert(letValidaBono, 'Aviso');
                                            controls.divBonoAumentaVelocidadFide.show();
                                            controls.divSeccionBtnBonoDispVigBonoDisp.show();
                                            controls.cboBonoFidDisponible.attr('disabled', false);
                                            controls.cboVigBonoDisponible.attr('disabled', false);
                                            controls.btnSummaryAccion.attr('disabled', true);
                                        }
                                        if (that.flagBonoActual == "2") {
                                            alert(letValidaBono, 'Aviso');
                                            controls.divBonoAumentaVelocidadFide.show();
                                            controls.cboBonoFidDisponible.attr('disabled', false);
                                            controls.cboVigBonoDisponible.attr('disabled', false);
                                            controls.btnSummaryAccion.attr('disabled', true);
                                        }
                                    }
                                }
                                if (letFlagAplica == 1) {
                                    alert(strMsjInfoValidateDeliveryBAVFide, 'Aviso');
                                    controls.cboBonoFidDisponible.attr('disabled', true);
                                    controls.cboVigBonoDisponible.attr('disabled', true);
                                    controls.btnSummaryAccion.attr('disabled', true);
                                 }
                              }

                         });
                    }
                });

            } 

        },

        // Decos asociados al cliente
        GetProductDetail: function () {
            var cantidad = 0, param = {};

            param.strIdSession = Session.IDSESSION;
            param.strContratoID = Session.DATACUSTOMER.ContratoID;
            param.strCustomerID = Session.DATACUSTOMER.CustomerID;

            $.ajax({
                type: 'Post',
                url: '/Transactions/HFC/UnistallInstallationOfDecoder/GetProductDetail',
                data: JSON.stringify(param),
                contentType: 'application/json; charset=utf-8',
                datatype: 'json',
                async: true,
                cache: false,
                error: function (response) {
                    SessionTransf.CantidadListaEquipos = cantidad;
                },
                success: function (response) {
                    if (response != null & response.data != null) {
                        cantidad = response.data.length;
                        SessionTransf.CantidadListaEquipos = cantidad;
                    }

                }
            });
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
                    SessionTransf.nuevoCostoInstal = '0';
                    if (response.data != null && response.data !== "") {
                        SessionTransf.nuevoCostoInstal = response.data.nuevoCostoInstal;
                        SessionTransf.costoDescuentoInstal = response.data.costoDescuentoInstal;//bono para el servicio
                    }
                }
            });

        },

        chkPromFact_change: function (send, args) {
            //VALIDAR si selecciono un porcentaje anteriormente.

            if (($('#cboDiscountCF option:selected').val()) == '') {
                alert("Seleccionar porcentaje de descuento.", "Alerta");
                $("#chkPromFact").removeAttr("checked"); //desmarca el check
                return;
            }
            if (($('#cboMonthCF option:selected').val()) == '') {
                alert("Seleccionar meses a aplicar.", "Alerta");
                $("#chkPromFact").removeAttr("checked"); //desmarca el check
                return;
            }
            if ((($('#cboMonthCF option:selected').val()) == 1) || (($('#cboMonthCF option:selected').val()) == '1')) {
                alert("Aplica para periodo de descuento mayor a 1 mes.", "Alerta");
                $("#chkPromFact").removeAttr("checked"); //desmarca el check
                return;
            }
            if (!($('#chkPromFact').is(':checked'))) {

                return;
            }


            var that = this,
                controls = that.getControls(),
                objDatos = {};
            //POR LO PPRONTO COMENTADO, LUEGO DESCOMENTAR seteando los valores correctos.
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
                        $("#chkPromFact").attr("disabled", true); //deshabilita el check
                        $("#chkPromFact").removeAttr("checked"); //desmarca el check
                    }
                    else {
                        $("#chkPromFact").removeAttr("disabled"); //habilita el heck

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
                url: '/Transactions/CommonServices/HfcGetAdditionalServices',
                success: function (response) {
                    console.log(response); //INC000004211915//
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

                    if (currentData.TIPO_SERVICIO == strValidarTipDeco[0] && currentData.CODGRUPOSERV == strValidarTipDeco[1]) {

                        //CostoInstalacion
                        if ($('#txtCostInst').text() > 0) {
                            $('#divCostoInstalacion').css("display", "block");
                        } else {
                            $('#divCostoInstalacion').css("display", "none");
                        }

                        controls.divFlatAccionFranja.css("display", "block");
                        SessionTransf.DecodificatorSelected = currentData;
                        SessionTransf.isDecodificator = true;
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
                    //txtTotDescuento
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

        GetIncreaseSpeedBonus: function () {
            var that = this,
          controls = that.getControls(),
           objLstAccionType = {};

            objLstAccionType.strCoId = Session.DATACUSTOMER.ContratoID;
            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objLstAccionType),
                url: '/Transactions/CommonServices/GetIncreaseSpeedBonus',
                success: function (response) {
                    if ((response.data != "") || (response != undefined)) {
                        strValidaBonoIncremento = response.data;
                    }
                }
            });
        },



        //PROY-32650 - F2
        GetInstallCost: function () {
            var that = this,
                controls = that.getControls();
            var objLstDiscount = {};
            objLstDiscount.strIdSession = Session.IDSESSION;
            objLstDiscount.hfc_lte = 0;

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
                            alert(salida[1], "Alerta");
                            controls.btnSummaryAccion.attr('disabled', true);
                        }

                    }
                }
            });
        },

        f_ValidacionETAAccion: function () {


            var that = this,
                controls = that.getControls(),
                model = {};

            model.IdSession = Session.IDSESSION;
            model.strJobTypes = controls.cboTypeWorkAccion.val();
            model.strCodePlanInst = controls.lblCodPlano.text();


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
                            $("#tr_SubWorkType").attr("disabled", true);
                            InitFranjasHorario2();
                            AdditionalPointsModel.strValidateETA = "0";

                            ValidateEtaAccion = "0";
                            Session.History = "";

                            that.f_EnableAgendamientoAccion(true);
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
                        History = oItem.Codigo2;
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
                    if (response.data != null) {
                        $.each(response.data, function (index, value) {
                            if (Event == null) {
                                if (value.Code == response.strTipoTrabajo) {
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
            model.strInstDesins = "0";
            var URL_GetJobType = window.location.protocol + '//' + window.location.host + '/Transactions/HFC/UnistallInstallationOfDecoder/GetJobTypes';
            that.f_LoadCombo(URL_GetJobType, model, controls.cboTypeWorkAccion, true, null, false);//null
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
            model.strContractId = Session.DATACUSTOMER.ContractID;
            var URL_GetOrderType = window.location.protocol + '//' + window.location.host + '/Transactions/HFC/AdditionalPoints/GetOrderType';
            that.f_LoadCombo(URL_GetOrderType, model, controls.cboSubTypeWorkAccion, true, 3, false);
            document.getElementById("cboSubTypeWorkAccion").selectedIndex = "1"; /*seleccionar el 1 elemento JUAN* */
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

            that.f_LoadCombo(URL_GetMotiveSot, model, controls.cboMotiveSOTAccion, true, 1, false); 
            controls.cboMotiveSOTAccion.val(strCboSelMotivoSOTHFC);
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

        SetDataToTableBAVFide: function(data){
            var that = this,
                controls = that.getControls();

            var tableBAVFide = $("#tblBAVFide").dataTable({
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
                dataBAVFide: data,
                language: {
                    "lengthMenu": "Display _MENU_ records per page",
                    "zeroRecords": "No existen datos",
                    "info": " ",
                    "infoEmpty": " ",
                    "infoFiltered": "(filtered from _MAX_ total records)"
                },
                columns: [
                    { "dataBAVFide": "DescBonoActual" }, // Descripción del bono
                    { "dataBAVFide": "FecActBonoActual" }, // Fecha activación del bono
                    { "dataBAVFide": "FecVigenBonoActual" } //Fecha vigencia del bono
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

        bonoId: "",
        periodo: "",
        flagBonoActual: -1,
        GetConsultServiceBono: function (callbackfn) {
            var that = this,
                controls = that.getControls();
            var objPrincipal = {

                objRequest: {
                    coId: Session.DATACUSTOMER.ContratoID,

                },
                strIdSession: Session.IDSESSION
            };

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                async: true,
                url: location.protocol + '//' + location.host + '/Transactions/HFC/FidelizacionServices/GetConsultServiceBono',
                data: JSON.stringify(objPrincipal),
                complete: function () {
                    callbackfn();
                },
                beforeSend: function () {
                    $.blockUI({
                        message: '<div align="center"><img src="/Images/loading2.gif"  width="25" height="25" /> Espere un momento por favor .... </div>',
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
                success: function (response) {
                    if (response != null) {
                        if (response.objConsultServiceResponse != null) {
                            data = response.Data;
                            that.SetDataToTableBAVFide = response.SetDataToTableBAVFide;
                            that.strValidaBono = response.strValidaBono;
                            that.strValidaBonoAsignado = response.strValidaBonoAsignado;
                            that.flagBonoActual = response.objConsultServiceResponse.MessageResponse.Body.flagBonoActual;
                            if (response.objConsultServiceResponse.MessageResponse.Body.codigoRespuesta == '0') {
                                controls.txtInternetActual.val(response.objConsultServiceResponse.MessageResponse.Body.servicioInternet);
                                    
                                var table = $("#tblBAVFide").dataTable({
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
                                        "zeroRecords": "El cliente no cuenta con bonos actuales",
                                        "info": " ",
                                        "infoEmpty": " ",
                                        "infoFiltered": "(filtered from _MAX_ total records)"
                                    },
                                    columns: [
                                        { "data": "descBonoActual" }, // Descripción del bono
                                        { "data": "fecActBonoActual" }, // Fecha activación del bono
                                        { "data": "fecVigenBonoActual" } //Fecha vigencia del bono
                                    ],
                                    "columnDefs": [{
                                        "orderable": false,
                                        "className": "",
                                        "targets": 0,
                                        "defaultContent": "",
                                        "visible": true
                                       }
                                    ]
                                });
   
                                if (response.objConsultServiceResponse.MessageResponse.Body.listaBonoDisponible != null) {
                                  
                                    controls.cboBonoFidDisponible.html("");
                                    controls.cboBonoFidDisponible.append($('<option>', { value: '-2', html: 'Seleccionar' }));
                                    controls.cboVigBonoDisponible.append($('<option>', { value: '-2', html: 'Seleccionar' }));
                                    $.each(response.objConsultServiceResponse.MessageResponse.Body.listaBonoDisponible, function (index, value) {
                                        var option = $('<option>', { value: value.idBonoDisp, html: value.descBonoDisp });
                                        var option = option.attr("data-indice", index);

                                        controls.cboBonoFidDisponible.append(option);

                                    });

                                    that.lstBonoDisp = response.objConsultServiceResponse.MessageResponse.Body.listaBonoDisponible;
                                }
                            } else {
                                alert(SessionTransf.strMensajeDeError, "Alerta");
                                controls.divBonoAumentaVelocidadFide.hide();
                                controls.btnSummaryAccion.attr('disabled', true);
                            }
                        } else {
                            alert(SessionTransf.strMensajeDeError, "Alerta");
                            controls.divBonoAumentaVelocidadFide.hide();
                            controls.btnSummaryAccion.attr('disabled', true);
                        }

                    }
                }
            })
        },

        lstBonoDisp: null,
        cboBonoFidDisponible_onchange: function () {
            var that = this,
                controls = that.getControls();
            var selected = $("#cboBonoFidDisponible option:selected");

            if ($('#cboBonoFidDisponible').val() == -2) {
                $('#cboVigBonoDisponible').html("");
                $('#cboVigBonoDisponible').append($('<option>', { value: '-2', html: 'Seleccionar' }));
                
                controls.btnSummaryAccion.attr('disabled', true);
            } else {
            var data_index = parseInt(selected.data("indice"));
            if (that.lstBonoDisp != null && that.lstBonoDisp[data_index].periodosBonoDisp != null) {
                controls.cboVigBonoDisponible.html("");
                    controls.cboVigBonoDisponible.append($('<option>', { value: '-2', html: 'Seleccionar' }));
                $.each(that.lstBonoDisp[data_index].periodosBonoDisp, function (index, value) {
                    controls.cboVigBonoDisponible.append($('<option>', { value: value.idPeriodo, html: value.desPeriodo }));
                });

            }
            }
            controls.btnSummaryAccion.attr('disabled', true);
        },

        cboVigBonoDisponible_onchange: function () {
            var that = this,
                controls = that.getControls();
            if (controls.cboVigBonoDisponible.val() == -2) {
                controls.btnSummaryAccion.attr('disabled', true);

            } else {
                controls.btnSummaryAccion.attr('disabled', false);
            }
        },

        flagAplica: -1,
        strMsjInfoValidateDeliveryBAVFide: "",
        PostValidateDeliveryBAVFide: function (callback) {
            var that = this,
                controls = that.getControls();
            var lineahfc = '';
            if (Session.CLIENTE.CustomerID != null && Session.CLIENTE.CustomerID != undefined) {
                lineahfc = 'H' + Session.CLIENTE.CustomerID;
            } else if (oCustomer.CustomerId != null && oCustomer.CustomerId != undefined) {
                lineahfc = 'H' + Session.CLIENTE.CustomerId;
            }
            var objPrincipal = {

                coId: Session.DATACUSTOMER.ContratoID + '|' + lineahfc,
                strIdSession: Session.IDSESSION
            };

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                async: true,
                url: location.protocol + '//' + location.host + '/Transactions/HFC/FidelizacionServices/PostValidateDeliveryBAVFide',
                data: JSON.stringify(objPrincipal),
                complete: function () {
                    callback();
                },
                success: function (response) {
                    if (response != null) {
                        if (response.objPostValidateDeliveryBAVFideResponse != null) {
                            that.flagAplica = response.objPostValidateDeliveryBAVFideResponse.MessageResponse.Body.flagAplica;
                            if (response.objPostValidateDeliveryBAVFideResponse.MessageResponse.Body.codigoRespuesta == '0') {
                                that.strMsjInfoValidateDeliveryBAVFide = response.strMsjInfoValidateDeliveryBAVFide;
                            } else {
                                alert(SessionTransf.strMensajeDeError, "Alert");
                                controls.divBonoAumentaVelocidadFide.show();
                                controls.cboBonoFidDisponible.attr('disabled', true);
                                controls.cboVigBonoDisponible.attr('disabled', true);
                                controls.btnSummaryAccion.attr('disabled', true);
                            }

                        } else {
                            alert(SessionTransf.strMensajeDeError, "Alert");
                            controls.divBonoAumentaVelocidadFide.show();
                            controls.cboBonoFidDisponible.attr('disabled', true);
                            controls.cboVigBonoDisponible.attr('disabled', true);
                            controls.btnSummaryAccion.attr('disabled', true);
                        }

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

        f_pad: function (s) { return (s < 10) ? '0' + s : s; },

        rdbLinesNumberChange: function (table) {
        },

        cboMotiveSOTAccion_Change: function () {
            var that = this,
            control = that.getControls();

            SessionTransf.hidenMotivoSotAccion = control.cboMotiveSOTAccion.val();
        },

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

    $.fn.HFCFidelizacionServices = function () {

        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {
            var $this = $(this),
                data = $this.data('HFCFidelizacionServices'),
                options = $.extend({}, $.fn.HFCFidelizacionServices.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('HFCFidelizacionServices', data);
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

    $.fn.HFCFidelizacionServices.defaults = {
    }

    $('#divBody').HFCFidelizacionServices();
})(jQuery);