function CloseValidation(obj, pag, controls) {
    if (obj.hidAccion === 'G') {
        var sUser = obj.hidUserValidator;
        if (Session.tipoAutorizacion != 1){
        FC_GrabarCommit(pag, controls, obj.NamesUserValidator, obj.EmailUserValidator);
    }
    }

    var mensaje;

    if (obj.hidAccion == 'F') {
        mensaje = 'La validación del usuario ingresado es incorrecto o no tiene permisos para continuar con el proceso, por favor verifiquelo.';
        alert(mensaje, "Alerta");
        $("#txtUsernameAuth").val("");
        $("#txtPasswordAuth").val("");
        if (Session.tipoAutorizacion == 1) {
            $('#cboSchedule option[value="-1"]').prop('selected', true);
        }
        return;
    }
};

function FC_GrabarCommit(pag, controls, NamesUserValidator, EmailUserValidator) {
    document.getElementById('hidAccion').value = '';

    $("#chkLoyalty").prop("checked", true);
    $("#txChargeAmount").val("0.00");

};

function FC_Fallo() {
    var pag = this;
    pag.FC_Fallo();
};

(function ($, undefined) {
    var Smmry = new Summary('transfer');
    var hdnMontoFideFinal;
    var SessionTransac = JSON.parse(sessionStorage.getItem("SessionTransac"));
    var SessionTransf = function () { };
    SessionTransf.IDSESSION = SessionTransac.UrlParams.IDSESSION == null || SessionTransac.UrlParams.IDSESSION == '' || SessionTransac.UrlParams.IDSESSION == undefined ? "123456789874563210" : SessionTransac.UrlParams.IDSESSION;
    SessionTransf.PhonfNro = SessionTransac.SessionParams.DATACUSTOMER.PhoneReference;
    SessionTransf.CONTRATO_ID = SessionTransac.SessionParams.DATACUSTOMER.ContractID;
    SessionTransf.CUSTOMER_ID = SessionTransac.SessionParams.DATACUSTOMER.CustomerID;
    SessionTransf.NameCustomer = SessionTransac.SessionParams.DATACUSTOMER.FullName;
    SessionTransf.TypDocRepreCustomer = SessionTransac.SessionParams.DATACUSTOMER.DocumentType;
    SessionTransf.NumbDocRepreCustomer = SessionTransac.SessionParams.DATACUSTOMER.DocumentNumber;
    SessionTransf.cuentaCustomer = SessionTransac.SessionParams.DATACUSTOMER.Account;    
    SessionTransf.RepreCustomer = SessionTransac.SessionParams.DATACUSTOMER.BusinessName;
    SessionTransf.NotAddressCustomer = SessionTransac.SessionParams.DATACUSTOMER.LegalAddress;
    SessionTransf.CountryCustomer = SessionTransac.SessionParams.DATACUSTOMER.LegalCountry;
    SessionTransf.CountryCustomerFac = SessionTransac.SessionParams.DATACUSTOMER.InvoiceCountry;
    SessionTransf.PlanoIDCustomer = SessionTransac.SessionParams.DATACUSTOMER.PlaneCodeInstallation;
    SessionTransf.urbLegalCustomer = SessionTransac.SessionParams.DATACUSTOMER.LegalUrbanization;
    SessionTransf.DirecDespachoCustomer = SessionTransac.SessionParams.DATACUSTOMER.Address;
    SessionTransf.DepCustomer = SessionTransac.SessionParams.DATACUSTOMER.Departament;
    SessionTransf.ProvCustomer = SessionTransac.SessionParams.DATACUSTOMER.LegalProvince;
    SessionTransf.DistCustomer = SessionTransac.SessionParams.DATACUSTOMER.District; 
    SessionTransf.IdEdifCustomer = "";//SessionTransac.SessionParams.DATACUSTOMER.IdEdifCustomer; -- anterior
    SessionTransf.EmailCustomer = SessionTransac.SessionParams.DATACUSTOMER.Email;    
    SessionTransf.AccesPage = SessionTransac.SessionParams.USERACCESS.optionPermissions;
    SessionTransf.gConstOpcTIEHabFidelizarLTE = null;
    SessionTransf.DatosLineatelefonia = SessionTransac.SessionParams.DATASERVICE.TelephonyValue;
    SessionTransf.DatosLineainternet =  SessionTransac.SessionParams.DATASERVICE.InternetValue;
    SessionTransf.DatosLineacableTv = SessionTransac.SessionParams.DATASERVICE.CableValue;
    SessionTransf.DataCycleBilling = SessionTransac.SessionParams.DATACUSTOMER.BillingCycle;
    SessionTransf.CodeUbigeo = SessionTransac.SessionParams.DATACUSTOMER.InstallUbigeo; //SessionTransac.SessionParams.DATACUSTOMER.InvoiceUbigeo;
    SessionTransf.hdnPermisos = null;
    SessionTransf.hdnTienePerfilJefe = "1";
    SessionTransf.hdnHayFidelizaFinal = "0";
    SessionTransf.hdnMensaje1 = null;
    SessionTransf.hdnMensaje2 = null;
    SessionTransf.hdnMensaje3 = null;
    SessionTransf.hdnMensaje5 = null;
    SessionTransf.hdnMensaje7 = null;
    SessionTransf.hdnMensaje8 = null;
    SessionTransf.hdnMensaje9 = null;
    SessionTransf.strValidateETA = null;
    SessionTransf.ConsultationCoverageTitle = null;
    SessionTransf.ConsultationCoverageURL = null;
    SessionTransf.hdnTrasladoIterno = '4';
    SessionTransf.hdnTrasladoExterno = '3';
    SessionTransf.hdnTagSelection = null;
    SessionTransf.strMsgTranGrabSatis = null;
    SessionTransf.hdnMessageSendMail = null;
    SessionTransf.hdnddlSOT = null;
    SessionTransf.CriterioMensajeOK = null;
    SessionTransf.hdnMensajeCapacidadFull = null;
    SessionTransf.hdnIDTrabTI = null;
    SessionTransf.hdnIDTrabTE = null;
    SessionTransf.hdnFecAgCU = null;
    SessionTransf.hdnCUbiCU = null;
    SessionTransf.hdnMontoTI = null;
    SessionTransf.hdnMontoTE = null;
    SessionTransf.hdnMontoFideFinal = null;
    SessionTransf.strPath = null;
    SessionTransf.hdnProDes = "";
    SessionTransf.hdnDisDes = "";
    SessionTransf.hdnCenPobDes = "";
    SessionTransf.hdnCodEdi = null;
    SessionTransf.hdnUbiID = null;
    SessionTransf.hdnCodPla = null;
    SessionTransf.hdnIDPlano = null;
    SessionTransf.cargoOCCTE1PLTE = null;
    SessionTransf.cargoOCCTE2PLTE = null;
    SessionTransf.cargoOCCTE3PLTE = null;
    SessionTransf.cargoOCCTILTE = null;
    SessionTransf.strActivaRadioTrasInt = null;
    SessionTransf.strActivaRadioTrasExt = null;
    SessionTransf.strActCheckDirFactTra = null;
    SessionTransf.hdnMessageErrorIgv = null;
    SessionTransf.strMessageValidationETA = null;
    SessionTransf.hdnFranjaHorariaCU = null;
    SessionTransf.hdnValidado = '0';
    SessionTransf.agendaGetFecha = null;
    SessionTransf.agendaGetCodigoFranja = null;
    SessionTransf.flagvalidation = null;
    SessionTransf.flagSaveTransactions = null;
    SessionTransf.hdnValidaEta = "";
    SessionTransf.hdnHistorialEta = "";
    SessionTransf.hdnTipoTrabCU = null;
    SessionTransf.hdnSubTipOrdCU = "";
    Session.hdntelephone = SessionTransac.SessionParams.DATACUSTOMER.PhoneReference;
    Session.userId = SessionTransac.SessionParams.USERACCESS.userId;
    SessionTransf.Statuslinea = SessionTransac.SessionParams.DATACUSTOMER.objPostDataAccount.AccountStatus;
    SessionTransf.ValidateControls = null;
    SessionTransf.codOCCTIPLTE = null;
    SessionTransf.codOCCTE1PLTE= null;
    SessionTransf.codOCCTE2PLTE= null;
    SessionTransf.codOCCTE3PLTE = null;
    SessionTransf.codcargoOCC = null; 
    Session.contractID = SessionTransac.SessionParams.DATACUSTOMER.ContractID;
    function validationsSteps(stepName, fn) {
        var response;
        if (stepName == 'tabProductInternal') {
            Form.prototype.ValidateFirtsStep(function (response) {
                if (response) fn(true); else fn(false);
            });
        } else {
            fn(true);
        }
        return response;
    }
    
    var Form = function ($element, options) {

        $.extend(this, $.fn.LTEExternalInternalTransfer.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({

            form: $element
            , lblCustomerName: $('#lblCustomerName', $element)
            , lblContact: $('#lblContact', $element)
            , lblIdentificationDocument: $('#lblIdentificationDocument', $element)
            , CodeBuilding: $('#CodeBuilding', $element)
            , lblBillingAmount: $('#lblBillingAmount', $element)
            , lblCycleBilling: $('#lblCycleBilling', $element)
            , lblAddress: $('#lblAddress', $element)
            , lblAddressNote: $('#lblAddressNote', $element)
            , lblCountry: $('#lblCountry', $element)
            , lblDepartament: $('#lblDepartament', $element)
            , lblRepLegal: $('#lblRepLegal', $element)
            , lblCodeUbigeo: $('#lblCodeUbigeo', $element)
            , lblHUB: $('#lblHUB', $element)
            , lblBelt: $('#lblBelt', $element)
            , lblCMTS: $('#lblCMTS', $element)
             , lbltypeCustomer: $('#lbltypeCustomer', $element)
            , lblCodeFlat: $('#lblCodeFlat', $element)
            , lblStatus: $('#lblStatus', $element)
            , lblIdContract: $('#lblIdContract', $element)
            , lblCustomerID: $('#lblCustomerID', $element)
             , lblDateAct: $('#lblDateAct', $element)
            , lblPlanActual: $('#lblPlanActual', $element)
            , lblDateVcto: $('#lblDateVcto', $element)
            , lblCuenta: $('#lblCuenta', $element)
	    , lblProvince: $('#lblProvince', $element)
            , lblDistrict: $('#lblDistrict', $element)
            , lblCodePlane: $('#lblCodePlane', $element)
            , idtruelnkCob: $('#idtruelnkCob', $element)
            , idfalselnkCob: $('#idfalselnkCob', $element)
            , idtruelnkValEdi: $('#idtruelnkValEdi', $element)
            , idfalselnkValEdi: $('#idfalselnkValEdi', $element)
            , ddlStreet: $('#ddlStreet', $element)
            , txtNameStreet: $('#txtNameStreet', $element)
            , txtNumber: $('#txtNumber', $element)
            , chkSN: $('#chkSN', $element)
            , ddlTipMzBloEdi: $('#ddlTipMzBloEdi', $element)
            , ddlTipDptInt: $('#ddlTipDptInt', $element)
            , txtNumberBlock: $('#txtNumberBlock', $element)
            , ddlDepartment: $('#ddlDepartment', $element)
            , ddlNumberDepartment: $('#ddlNumberDepartment', $element)
            , ddlNoteUrbanization: $('#ddlNoteUrbanization', $element)
            , txtNoteUrbanization: $('#txtNoteUrbanization', $element)
            , ddlNoteZote: $('#ddlNoteZote', $element)
            , txtNoteNameZote: $('#txtNoteNameZote', $element)
            , txtNoteReference: $('#txtNoteReference', $element)
            , ddlNoteCountry: $('#ddlNoteCountry', $element)
            , ddlNoteDepartment: $('#ddlNoteDepartment', $element)
            , ddlNoteProvince: $('#ddlNoteProvince', $element)
            , ddlNoteDistrict: $('#ddlNoteDistrict', $element)
            , txtLot: $('#txtLot', $element)
            , txtConDir: $('#txtConDir', $element)
            , txtConNotDir: $('#txtConNotDir', $element)
            , liInternalTransf: $('#liInternalTransf', $element)
            , liExternalTransf: $('#liExternalTransf', $element)
            , txtNoteCodePostal: $('#txtNoteCodePostal', $element)
            , ddlNoteCenterPopulated: $('#ddlNoteCenterPopulated', $element)
            , ddlTypeWork: $('#ddlTypeWork', $element)
            , ddlTypeSubWork: $('#ddlTypeSubWork', $element)
            , ddlReasonSot: $('#ddlReasonSot', $element)
           , chkLoyalty: $('#chkLoyalty', $element)
            , txChargeAmount: $('#txChargeAmount', $element)
            , ddlSchedule: $('#ddlSchedule', $element)
            , dContenedorEmail: $('#dContenedorEmail', $element)
            , chkSendMail: $('#chkSendMail', $element)
            , chkUseChangeBilling: $('#chkUseChangeBilling', $element)
            , txtSendMail: $('#txtSendMail', $element)
            , ddlCACDAC: $('#ddlCACDAC', $element)
            , rbtntruelnkCob: $('#idtruelnkCob', $element)
            , rbtnFalselnkCb: $('#idfalselnkCob', $element)
            , rbtnTrueValEdi: $('#idtruelnkValEdi', $element)
            , rbtnFalseEdi: $('#idfalselnkValEdi', $element)
            , txtNote: $('#txtNote', $element)
            , tblIntransf: $('#tblIntransf', $element)
            , tblExtransf: $('#tblExtransf', $element)
            , lblErrorMessage: $('#lblErrorMessage', $element)
            , divErrorAlert: $('#divErrorAlert', $element)
            , btnSave: $('#btnSave', $element)
            , btnConstancy: $('#btnConstancy', $element)
            , btnClose: $('#btnClose', $element)
            , btnClosea: $('#btnClosea', $element)
            , btnCloseb: $('#btnCloseb', $element)
            , btnClosec: $('#btnClosec', $element)
            , btnCloseInternal: $('#btnCloseInternal', $element)
            , divRules: $('#BussinessRule', $element)
            , btnSaveInternal: $('#btnSaveInternal', $element)
            , btnConstancyInternal: $('#btnConstancyInternal', $element)
            , cboSchedule: $("#cboSchedule", $element)
            , btnValidateSchedule: $('#btnValidateSchedule', $element)
            , lnkNumSot: $('#lnkNumSot', $element)
            , txt_dDateProgramming: $("#txt_dDateProgramming", $element)
            , closeErrorAlert: $("#closeErrorAlert", $element)
            , lblTitle: $('#lblTitle', $element)
            , cboJobTypes: $('#cboJobTypes', $element)
            , cboSubJobTypes: $('#cboSubJobTypes', $element)
            , ddlSubTypeJob: $('#ddlSubTypeJob', $element)
        });

    }

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this,
                controls = this.getControls();

            controls.liInternalTransf.addEvent(that, 'click', that.f_liInternalTransf);
            controls.liExternalTransf.addEvent(that, 'click', that.f_liExternalTransf);
            controls.btnSave.addEvent(that, 'click', that.btnSave_click);
            controls.btnSaveInternal.addEvent(that, 'click', that.btnSave_click);
            controls.btnConstancy.addEvent(that, 'click', that.btnConstancy_click);
            controls.btnConstancyInternal.addEvent(that, 'click', that.btnConstancy_click);
            controls.btnClose.addEvent(that, 'click', that.btnClose_click);
            controls.btnClosea.addEvent(that, 'click', that.btnClose_click);
            controls.btnCloseb.addEvent(that, 'click', that.btnClose_click);
            controls.btnClosec.addEvent(that, 'click', that.btnClose_click);
            controls.btnCloseInternal.addEvent(that, 'click', that.btnClose_click);
            controls.chkSendMail.addEvent(that, 'change', that.chkSendMail_Change);
            controls.ddlTypeWork.change(function () { that.f_SetTipoTrabajo(); });
            controls.ddlTypeSubWork.change(function () { that.f_SetSubTipoTrabajo(); });
            controls.ddlCACDAC.change(function () { that.f_ddlCACDAC(); });
            controls.ddlNoteDepartment.change(function () { that.f_SetDepartments(); });
            controls.ddlNoteProvince.change(function () { that.f_SetProvinces(); });
            controls.chkLoyalty.addEvent(that, 'change', that.f_ValidaterPermLoyalty);
            controls.chkSN.addEvent(that, 'change', that.f_SetChkSn);
            controls.rbtntruelnkCob.addEvent(that, 'change', that.f_valdiar_True_Cobertura_RadioButton);
            controls.rbtnFalselnkCb.addEvent(that, 'change', that.f_validar_False_Cobertura_RadioButton);
            controls.rbtnTrueValEdi.addEvent(that, 'change', that.f_True_ValEdi);
            controls.rbtnFalseEdi.addEvent(that, 'change', that.f_false_ValEdi);
            controls.txtNote.addEvent(that, 'change', that.f_SeleccionarNotas);
            controls.chkUseChangeBilling.addEvent(that, 'change', that.f_validar_datos_cambio_direccion);
            controls.chkSN.change(function () { that.f_SumarDireccionDestino() });
            controls.ddlTypeWork.empty();
            controls.ddlReasonSot.attr("disabled", "disabled");
            controls.ddlNoteCountry.attr("disabled", "disabled");
            controls.txtNumber.keydown(function (event) { that.f_ValidateSoloNumeros(event); });
            controls.ddlNumberDepartment.keydown(function (event) { that.f_ValidateSoloNumeros(event); });
            controls.ddlStreet.change(function () { that.f_SumarDireccionDestino(); });
            controls.txtNameStreet.keyup(function () { that.f_SumarDireccionDestino(); });
            controls.txtNumber.keyup(function () { that.f_SumarDireccionDestino(); });
            controls.ddlTipMzBloEdi.change(function () { that.f_SumarDireccionDestino(); });
            controls.txtNumberBlock.keyup(function () { that.f_SumarDireccionDestino(); });
            controls.txtLot.keyup(function () { that.f_SumarDireccionDestino(); });
            controls.ddlDepartment.change(function () { that.f_SumarDireccionDestino(); });
            controls.ddlNumberDepartment.keyup(function () { that.f_SumarDireccionDestino(); });
            controls.ddlNoteDistrict.change(function () { that.f_ObtenerUbigeoID(); });
            controls.ddlNoteUrbanization.change(function () { that.f_SumarNotasDireccion(); });
            controls.txtNoteUrbanization.keyup(function () { that.f_SumarNotasDireccion(); });
            controls.ddlNoteZote.change(function () { that.f_SumarNotasDireccion(); });
            controls.txtNoteNameZote.keyup(function () { that.f_SumarNotasDireccion(); });
            controls.txtNoteReference.keyup(function () { that.f_SumarNotasDireccion(); });
            controls.txtNote.change(function () { that.f_txtNote(); });
            controls.txtSendMail.change(function () { that.f_txtSendMail(); });
            $('#btnAcePla').click(function () { that.f_SeleccionarPlano(); });
            $('#btnCerPla').click(function () { that.f_CerrarVentanaPlanos(); });
            $('#lnkCob').click(function () { return that.f_AbrirCobertura(); });
            $('#idPrev2').click(function () { return that.f_Prev(); });
            $('#idPrev3').click(function () { return that.f_Prev(); });
            $('#idPrev4').click(function () { return that.f_Prev(); });
            $('#idPrev5').click(function () { return that.f_Prev(); });
            $('#idNext1').click(function () { return that.f_Next(); });
            $('#idNext2').click(function () { return that.f_Next(); });
            $('#idNext3').click(function () { return that.f_Next(); });
            $('#idNext4').click(function () { return that.f_Next(); });
            $('#btnCoberturaClose').addEvent($.that, 'click', that.f_CoberturaClose);
            $('#btnAceEdi').click(function () { that.f_SeleccionarEdificio(); });
            $('#btnCerEdi').click(function () { that.f_CerrarVentanaEdificios(); });
            $('#lnkValEdi').click(function () { return that.f_AbrirValidarEdificio(); });
            $('#lnkCob').click(function () { return that.f_AbrirValidarPlano(); });
            that.maximizarWindow();
            that.windowAutoSize();
            document.addEventListener('keyup', that.shortCutStep, false);

            $('.next-trans').on('click', function (e) {
                that.shortCutStep(e);
            });

            $('#lnkNumSot').click(function () { return that.f_ConsultarSOT(); });
            controls.ddlNoteCenterPopulated.change(function () { that.f_ObtenerCobertura(); });
            controls.closeErrorAlert.click(function () { return that.f_closeErrorAlert(); });
            that.render();
        },
        f_closeErrorAlert: function () {
            var that = this;
            var controls = that.getControls();
            controls.divErrorAlert.hide();
        },
        setControls: function (value) {
            this.m_controls = value;
        },
        getControls: function () {
            return this.m_controls || {};
        },
        render: function () {
            var that = this;
            var controls = that.getControls();
            controls.lblTitle.text("TRASLADO INTERNO/EXTERNO");
            var SessionTransac = JSON.parse(sessionStorage.getItem("SessionTransac"));
            $.blockUI({
                message: '<div align="center"><img src="' + that.strUrlLogo + '" width="25" height="25" /> Cargando ... </div>',
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

            Smmry.set('ddlNombreCalle', '');
            Smmry.set('txtNumero', '');
            Smmry.set('txtSN', "NO");
            Smmry.set('ddlMz', '');
            Smmry.set('NroMz', '');
            Smmry.set('ddlTipo', '');
            Smmry.set('NumeroDpto', '');
            Smmry.set('ddlUrb', '');
            Smmry.set('NombreUrb', '');
            Smmry.set('ddlZona', '');
            Smmry.set('NombreZona', '');
            Smmry.set('Referencia', '');
            Smmry.set('txtLote', '');
            Smmry.set('Pais', '');
            Smmry.set('Departamento', '');
            Smmry.set('Provincia', '');
            Smmry.set('Distrito', '');
            Smmry.set('CodPostal', '');
            Smmry.set('CentroPoblado', '');
            Smmry.set('chkUpdate', "NO");
            Smmry.set('CodPlano', '');
            Smmry.set('ValidarCobertura', 'NO');
            Smmry.set('ValidarEdificio', 'NO');
            Smmry.set('CodEdificio', '');
            Smmry.set('TipTrabajo', '');
            Smmry.set('SubTipTrabajo', '');
            Smmry.set('FechCompromiso', '');
            Smmry.set('chkFidelizar', 'NO');
            Smmry.set('txtMonto', '');
            Smmry.set('chkEmail', 'NO');
            Smmry.set('Email', 'NO');
            Smmry.set('PuntVenta', '');
            Smmry.set('Notas', '-');
            Smmry.set('MotiveSoft', '');
            that.InitGetMessageConfiguration();
            controls.txtSendMail.val(SessionTransf.EmailCustomer);
            SessionTransf.hdnTagSelection = SessionTransf.hdnTrasladoIterno;
            controls.txtConDir.val('');
            controls.txtConNotDir.val('');
            controls.txtNumber.val('');
            controls.txChargeAmount.val('0.00');
            controls.btnConstancy.attr('disabled', true);
            controls.btnConstancyInternal.attr('disabled', true);
            controls.idtruelnkCob.attr('disabled', true);
            controls.idfalselnkCob.attr('disabled', true);
            controls.idtruelnkValEdi.attr('disabled', true);
            controls.idfalselnkValEdi.attr('disabled', true);

            $("#IDdivtabProducts").show();
            $("#IDdivtabProductsInternal").after($("#IDdivtabProducts"));
            $("#IDdivtabTypeWork").show();
            $("#IDdivtabTypeWorkInternal").after($("#IDdivtabTypeWork"));
            $("#divErrorAlert").hide();
            $("#divErrorInternal").after($("#divErrorAlert"));
            $("#btnConstancyInternal").prop("disabled", true);
            that.getCustomerData();
        },

        InitGetMessageConfiguration: function () {
            var that = this, controls = this.getControls();
            var objMessageConfiguration = {};
            objMessageConfiguration.strIdSession = Session.IDSESSION;

            var urlBase = '/Transactions/LTE/ExternalInternalTransfer';
            $.app.ajax({
                async: true,
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objMessageConfiguration),
                url: urlBase + '/InitGetMessageConfiguration',
                success: function (response) {
                    SessionTransf.hdnMensaje1 = response[0];
                    SessionTransf.hdnMensaje2 = response[1];
                    SessionTransf.hdnMensaje3 = response[2];
                    SessionTransf.hdnMensaje5 = response[3];
                    SessionTransf.hdnMensaje7 = response[4];
                    SessionTransf.hdnMensaje9 = response[5];
                    SessionTransf.hdnTrasladoIterno = response[6];//"4";
                    SessionTransf.hdnTrasladoExterno = response[7];//"3";
                    SessionTransf.hdnTagSelection = response[8];// "4"; Flag- Interno
                    SessionTransf.strMsgTranGrabSatis = response[9]; //
                    SessionTransf.hdnMessageSendMail = response[10];// 
                    SessionTransf.hdnddlSOT = response[11];
                    SessionTransf.CriterioMensajeOK = response[12]; //"OK";
                    SessionTransf.hdnMensajeCapacidadFull = response[13];//gStrMsjConsultaCapacidadNoDisp
                    SessionTransf.hdnIDTrabTI = response[14];//"418";
                    SessionTransf.hdnIDTrabTE = response[15];//"412";
                    SessionTransf.hdnMensaje4 = response[16];
                    SessionTransf.strMensajeEmail = response[17];
                    SessionTransf.gConstOpcTIEHabFidelizarLTE = response[18];//'LTE_FTIE'//gConstOpcTIEHabFidelizar 
                    SessionTransf.hdnHayFidelizaFinal = response[19];// "0";
                    SessionTransf.cargoOCCTE1PLTE = response[20];// "MTE1P";
                    SessionTransf.cargoOCCTE2PLTE = response[21];// "MTE2P";
                    SessionTransf.cargoOCCTE3PLTE = response[22];//"MTE3P";
                    SessionTransf.cargoOCCTILTE = response[23];//"MTI";

                    SessionTransf.strActivaRadioTrasInt = response[24];//"LTE_RTI";// 
                    SessionTransf.strActivaRadioTrasExt = response[25];//"LTE_RTE";// 
                    SessionTransf.strActCheckDirFactTra = response[26];//"LTE_CDFT";//
                    //
                    SessionTransf.codOCCTIPLTE = response[27];//"LTE_RTI";// 
                    SessionTransf.codOCCTE1PLTE = response[28];//"LTE_RTE";//
                    SessionTransf.codOCCTE2PLTE = response[29];//"LTE_CDFT";/
                    SessionTransf.codOCCTE3PLTE = response[30];//"LTE_CDFT";/
                    SessionTransf.hdnMessageErrorIgv = response[31];
                    SessionTransf.strMessageValidationETA = response[32];
                    SessionTransf.strCodTipServLteTI = response[36];
                    SessionTransf.strCodTipServLteTE = response[37];
                    Session.ServerDate = response[33];
                    SessionTransf.hdnMensaje8 = "Seleccione Edificio";
                    that.f_Privilegios();
                    that.getLoadMont();
                    that.GetZoneType();
                    that.GetDepartments();
                    that.f_ListarTipMzBloEdi();
                    that.f_ListarTipDptInt();
                    that.getddlStreet();
                    that.getddlNoteUrbanization();
                    that.f_cargarSubtipoTrabajo(SessionTransf.hdnIDTrabTI);
                    that.getReasonSot();
                    Session.CodUbigeo = SessionTransac.SessionParams.DATACUSTOMER.InstallUbigeo; 
                    SessionTransf.codOpcionTI = response[34];
                    SessionTransf.codOpcionTE = response[35];
                    Session.codOpcion = SessionTransf.codOpcionTI;
                    Session.tipoAutorizacion = 0;
                    that.getIGV();
                    that.f_ClearCentroPoblado();
                    that.f_getListDetailsProducto();
                    that.f_IniTablaPlano();
                }
            });
        },
        shortCutStep: function (e) {
            var that = this;
            if ((e.ctrlKey && e.keyCode == 39) || e.keyCode == null) {
                var $activeTab = $('.step.tab-pane.active');

                validationsSteps($activeTab.prop('id'), function (response) {
                    if (response) {
                        var $nextBtn = $('.next-step');
                        navigateTabs($nextBtn);
                    }
                });
            }

            if (e.ctrlKey && e.keyCode == 37) {
                var $prevBtn = $('.prev-step');
                navigateTabs($prevBtn);
            }
        },

        ValidateFirtsStep: function (fn) {
            var that = this,
                controls = that.getControls();

            switch (SessionTransf.flagvalidation) {
                case null:

                    if ($('#ddlStreet').val() == '' || $('#ddlStreet').val() == null) {
                        alert("Seleccione Calle/Av/Jr.", "Alerta");
                        fn(false);
                        return false;
                    }

                    if ($('#txtNameStreet').val() == '') {
                        alert("Ingrese nombre Calle/Av/Jr.", "Alerta");
                        fn(false);
                        return false;
                    }
                    if ($('#txtNumber').val() == '') {
                        alert("Ingrese número.", "Alerta");
                        fn(false);
                        return false;
                    }

                    SessionTransf.flagvalidation = '1';
                    fn(true);
                    return true;

                    break;

                case '1':

                    if ($('#ddlNoteUrbanization').val() == '' || $('#ddlNoteUrbanization').val() == null) {
                        alert("Seleccione Urb/Res/Pjo.", "Alerta");
                        fn(false);
                        return false;
                    }

                    if ($('#txtNoteUrbanization').val() == '') {
                        alert("Ingrese nombre Urb/Res/Pjo.", "Alerta");
                        fn(false);
                        return false;
                    }

                    if ($('#ddlNoteDepartment').val() == '' || $('#ddlNoteDepartment').val() == null) {
                        alert("Seleccione departamento.", "Alerta");
                        fn(false);
                        return false;
                    }
                    if ($('#ddlNoteProvince').val() == '' || $('#ddlNoteProvince').val() == null) {
                        alert("Seleccione provincia.", "Alerta");
                        fn(false);
                        return false;
                    }
                    if ($('#ddlNoteDistrict').val() == '' || $('#ddlNoteDistrict').val() == null) {
                        alert("Seleccione distrito.", "Alerta");
                        fn(false);
                        return false;
                    }
                    if ($('#ddlNoteCenterPopulated').val() == '' || $('#ddlNoteCenterPopulated').val() == null) {
                        alert("Seleccione centro poblado.", "Alerta");
                        fn(false);
                        return false;
                    }

                    that.F_ValidateETA('3'); //VALIDACION ETA

                    SessionTransf.flagvalidation = '2';
                    fn(true);
                    return true;

                    break;


                case '2':              
                    if ($('#ddlReasonSot').val() == '' || $('#ddlReasonSot').val() == null) {
                        alert("Seleccione motivo SOT.", "Alerta");
                        fn(false);
                        return false;
                    }
                    if ($('#ddlTypeWork').val() == '' || $('#ddlTypeWork').val() == null) {
                        alert("Seleccione tipo de trabajo.", "Alerta");
                        fn(false);
                        return false;
                    }

                    if (SessionTransf.strValidateETA != '0') {
                        if ($('#ddlTypeSubWork').val() == '' || $('#ddlTypeSubWork').val() == null) {
                            alert("Seleccione sub tipo de trabajo.", "Alerta");
                            fn(false);
                            return false;
                        }
                    }

                    if ($('#txt_dDateProgramming').val() == '') {
                        alert("Ingrese fecha de programación.", "Alerta");
                        fn(false);
                        return false;
                    }

                    if (SessionTransf.strValidateETA == '2') {
                    if ($('#cboSchedule').val() == '-1' || $('#cboSchedule').val() == '') {
                            $.each(Session.vMessageValidationList, function (index, value) {
                                    if (value.ABREVIATURA_DET == "MSJ_OBLIG_ETA"){
                                        alert(value.CODIGOC, "Alerta");
                                    }
                                });
                        fn(false);
                        return false;
                        }
                    }

                    if (!that.f_ValidateEmail()) {
                        fn(false);
                        return false;
                    }

                    if ($('#ddlCACDAC').val() == '' || $('#ddlCACDAC').val() == null) {
                        alert("Seleccione punto de atención.", "Alerta");
                        fn(false);
                        return false;
                    } else {
                        $('#ddlCACDAC').trigger("change");
                    }

                    SessionTransf.flagvalidation = '3';
                    fn(true);
                    return true;

                    break;
            }
        },

        f_ValidateStatusLinea: function () {

            var pageIsPostBack = true;
            if (SessionTransac.SessionParams.DATACUSTOMER.objPostDataAccount.AccountStatus == 'Desactivo') { 
                alert("Contrato está inactivo, no puede completar la operación.", "Alerta", function () {
                    parent.window.close();
                });

                pageIsPostBack = false;
                return false;
            }
            else {
                if (SessionTransac.SessionParams.DATACUSTOMER.objPostDataAccount.AccountStatus == 'Reservado') {

                    alert("Contrato está Reservado, no puede completar la operación.", "Alerta", function () { //strMsgValidacionContratoReservado
                        parent.window.close();
                    });

                    pageIsPostBack = false;
                    return false;
                }
                else {
                    if (SessionTransf.DatosLineacableTv == 'F' && SessionTransf.DatosLineainternet == 'F' && SessionTransf.DatosLineatelefonia == 'F') {
                        alert("No Tiene Servicio Cable - Internet activo.", "Alerta", function () { //strTextoNoTieneServicioCableYOInternetActivoLTETraslado
                            parent.window.close();
                        });

                        pageIsPostBack = false;
                        return false;
                    }
                    else {

                        if (SessionTransf.Statuslinea == 'Suspendido') {
                            alert("Contrato está suspendido, no puede completar la operación.", "Alerta", function () {//strMsgValidacionContratoReservado
                                parent.window.close();
                            });

                            pageIsPostBack = false;
                            return false;
                        }
                    }
                }
            }
        },

        f_Privilegios: function () {
            var strCadOpc = SessionTransf.AccesPage;
            var that = this;
            var controls = this.getControls();
            SessionTransf.hdnPermisos = null;
            SessionTransf.hdnPermisos = strCadOpc.indexOf(SessionTransf.strActivaRadioTrasInt) > -1 ? '1' : '0';
            SessionTransf.hdnPermisos = SessionTransf.hdnPermisos + '|';
            SessionTransf.hdnPermisos += strCadOpc.indexOf(SessionTransf.strActivaRadioTrasExt) > -1 ? '1' : '0';
            SessionTransf.hdnPermisos = SessionTransf.hdnPermisos + '|';
            SessionTransf.hdnPermisos += strCadOpc.indexOf(SessionTransf.strActCheckDirFactTra) > -1 ? '1' : '0';

            var arrhdnPermisos = SessionTransf.hdnPermisos.split('|');
            //if (SessionTransf.hdnPermisos.split('|')[0] == '0') {
            //    $('#liInternalTransf').prop('disabled', true);
            //   // $('#divDashboardTabs').tabs({ "disabled": [0] });

            //} else {
            //    $('#liInternalTransf').prop('disabled', false);
            //    alert("liInternalTransf");
            //}

            //if (SessionTransf.hdnPermisos.split('|')[1] == '0') {
            //    $('#divDashboardTabs').tabs({ "disabled": true });
            //    //$('#liExternalTransf').prop('disabled', false);

            //} else {
            //    $('#liExternalTransf').prop('disabled', false);
            //    alert("liExternalTransf");
            //}
            //if (arrhdnPermisos[2] == '0') {
            //    controls.chkUseChangeBilling.prop('disabled', true);
            //}
            //else {
            controls.chkUseChangeBilling.prop('disabled', false);
            //}

        },

        f_txtSendMail: function () {
            var controls = this.getControls();
            //summary
            Smmry.set('Email', controls.txtSendMail.val());
        },

        HidetxtEmail: function () {
            var controls = this.getControls();
            controls.txtSendMail.prop("style").display = "none";
            //summary
            Smmry.set('chkEmail', 'NO');
            Smmry.set('Email', '');
        },

        chkSendMail_Change: function (sender, arg) {
            var that = this;
            var controls = that.getControls();
            if (sender.prop("checked")) {
                controls.txtSendMail.prop("style").display = "block";
                //summary
                Smmry.set('chkEmail', 'SI');
                Smmry.set('Email', controls.txtSendMail.val());
            } else {
                controls.chkSendMail.html("");
                that.HidetxtEmail();
                //summary
                Smmry.set('chkEmail', 'NO');
                Smmry.set('Email', '');
            }

        },

        f_ddlCACDAC: function () {
            var that = this, controls = that.getControls();
            //summary
            Smmry.set('PuntVenta', $("#ddlCACDAC option:selected").html());
            //Smmry.set('FechCompromiso', controls.txt_dDateProgramming.val());
            Smmry.set('Horario', $("#cboSchedule option:selected").html() == 'Seleccionar' ? '' : $("#cboSchedule option:selected").html());
            Smmry.set('SubTipTrabajo', $("#ddlTypeSubWork option:selected").html() == 'Seleccionar' ? '' : $("#ddlTypeSubWork option:selected").html());
            Smmry.set('TipTrabajo', $("#ddlTypeWork option:selected").html() == 'Seleccionar' ? '' : $("#ddlTypeWork option:selected").html());
            Smmry.set('MotiveSoft', $('#ddlReasonSot option:selected').html());
        },
        
        f_txtNote: function () {
            //summary
            var that = this,
               controls = this.getControls();
            Smmry.set('Notas', controls.txtNote.val());
        },


        f_ValidaterPermLoyalty: function () {
            var that = this;            
            Session.tipoAutorizacion = 0;
            var controls = that.getControls();
            
            if (controls.chkLoyalty.prop("checked")) {
                if (SessionTransf.hdnTienePerfilJefe == "1") {
                    controls.chkLoyalty.prop("checked", true);
                    controls.txChargeAmount.val("0.00");
                    //summary
                    Smmry.set('txtMonto', controls.txChargeAmount.val());
                    //summary
                    Smmry.set('chkFidelizar', 'SI');
                }
                else {
                    //co= $("#hidCodOpcion").val();
                    var param = {
                        "strIdSession": "1234567",
                        'pag': '1',
                        'opcion': 'BUS',
                        'co': '1000000000000',
                        'telefono': SessionTransf.PhonfNro
                    };
                    controls.chkLoyalty.prop("checked", false);
                    //summary
                    Smmry.set('chkFidelizar', 'NO');
                    ValidateAccess(that, controls, 'BUS', 'gConstOpcTIEHabFidelizarLTE', '1', param, 'Fixed');
                }
            }
            else {
                $("#chkLoyalty").prop("checked", false);
                //summary
                Smmry.set('chkFidelizar', 'NO');
                if (SessionTransf.hdnTagSelection == SessionTransf.hdnTrasladoIterno) {
                    controls.txChargeAmount.val(SessionTransf.hdnMontoTI);
                    //summary
                    Smmry.set('txtMonto', controls.txChargeAmount.val());
                }
                else {
                    if (SessionTransf.hdnTagSelection == SessionTransf.hdnTrasladoExterno) {
                        controls.txChargeAmount.val(SessionTransf.hdnMontoTE);
                        //summary
                        Smmry.set('txtMonto', controls.txChargeAmount.val());
                    }
                }
            }
        },

        f_ObtenerUbigeoID: function () {
            var urlBase = '/Transactions/LTE/ExternalInternalTransfer';
            var that = this, controls = that.getControls();
            $("#txtCodPla").val('');
            $("#txtCodEdi").val('');

            if (controls.ddlNoteDepartment.val() == "") {
                alert(SessionTransf.hdnMensaje1, "Alerta");
                return false;
            }
            if (controls.ddlNoteProvince.val() == "") {
                alert(SessionTransf.hdnMensaje2, "Alerta");
                return false;
            }
            if (controls.ddlNoteDistrict.val() == "") {
                alert(SessionTransf.hdnMensaje3, "Alerta");
                return false;
                // summary
                Smmry.set('Distrito', "sin seleccionar");
            } else {
                //sumary
                Smmry.set('Distrito', $("#ddlNoteDistrict option:selected").html());
            }

            var objGetUbigeo = {};
            that.f_ClearCentroPoblado();
            objGetUbigeo.strIdSession = SessionTransf.IDSESSION;
            objGetUbigeo.vstrDisID = controls.ddlNoteDistrict.val();
            objGetUbigeo.vstrDepID = controls.ddlNoteDepartment.val();
            objGetUbigeo.vstrProvID = controls.ddlNoteProvince.val();
            $.ajax({
                async: true,
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objGetUbigeo),
                url: urlBase + '/GetUbigeoID',
                success: function (response) {
                    var f = response;
                    if (f != null) {
                        SessionTransf.hdnUbiID = f.data;
                        that.f_MostrarCodigoPostal();
                    }
                }
            });
        },

        f_SeleccionarNotas: function () {
            var that = this, controls = that.getControls();
            //sumary
            if (controls.txtNote.val() != "") {
                var Notas = controls.txtNote.val();
                var array = Notas.split(" ");

                var strFinal = "";
                array.forEach(function (item) {

                    if (item.length > 60) {
                        var cant = item.length;
                        var div = cant / 60;
                        //div = Math.truc(div);

                        for (var i = 1; i <= div; i++) {
                            var str = item.substring((60 * i) - 60, 60 * i);
                            strFinal = strFinal + str + " ";
                        }
                    }
                    else {
                        strFinal = strFinal + item + " ";
                    }
                });

                //Smmry.set('Notas', controls.txtNote.val());
                Smmry.set('Notas', strFinal);

            } else {
                Smmry.set('Notas', "no han ingresado notas");
            }
        },

        f_MostrarCodigoPostal: function () {
            var that = this, controls = that.getControls();
            var urlBase = '/Transactions/LTE/ExternalInternalTransfer';// window.location.href;

            var objMostrarCodigoPostal = {};
            objMostrarCodigoPostal.strIdSession = SessionTransf.IDSESSION;
            objMostrarCodigoPostal.vstrDisID = controls.ddlNoteDistrict.val();
            $.ajax({
                async: true,
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objMostrarCodigoPostal),
                url: urlBase + '/ObtenerCodigoPostal',
                success: function (response) {
                    var respuesta = response.data;
                    
                    controls.txtNoteCodePostal.val(respuesta);
                    that.f_ListarCentroPoblado();//<-- 
                }
            });

        },


        f_ListarCentroPoblado: function () {

            var that = this, controls = that.getControls();
            var urlBase = '/Transactions/LTE/ExternalInternalTransfer';
            var objCenterPob = {};
            objCenterPob.strIdSession = SessionTransf.IDSESSION;
            objCenterPob.strUbigeo = SessionTransf.hdnUbiID;
            $.ajax({
                async: true,
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objCenterPob),
                url: urlBase + '/GetListCenterPob',
                success: function (response) {
                    var filas = response.data.ListGeneric;
                  
                    controls.ddlNoteCenterPopulated.append($('<option>', { value: '', html: 'Seleccione' }));
                    $.each(filas, function (i, f) {
                        //controls.ddlNoteCenterPopulated.append('<option value="' + f.Code + '">' + f.Description + SessionTransf.hdnUbiID + '</option>');
                        controls.ddlNoteCenterPopulated.append($('<option>', { value: f.Code, html: f.Description + SessionTransf.hdnUbiID }));
                    });
                }
            });
        },

        f_liInternalTransf: function () {
            var that = this;
            var controls = this.getControls();
            //
            that.f_ValidateStatusLinea();
            //
            $("#cboSchedule").html("");
            $("#cboSchedule").append($('<option>', { value: '-1', html: 'Seleccionar' }));
            SessionTransf.flagvalidation = 'NULL';
            $("#ddlReasonSot").attr('disabled', true);
            $("#IDdivtabTypeWorkInternal").after($("#IDdivtabTypeWork"));
            $("#IDdivtabProductsInternal").after($("#IDdivtabProducts"));
            $("#divErrorAlert").hide();
            $("#divErrorInternal").after($("#divErrorAlert"));
            controls.txtNote.val('');
            //that.getCACDAC();
            that.InitCacDac();
            if (SessionTransf.flagSaveTransactions == 'I') {
                $("#IDdivtabTypeWork textarea, #IDdivtabTypeWork input, #IDdivtabTypeWork select").attr('disabled', true);
            }
            //controls.liExternalTransf.attr('class', 'disabled');
            //$('#liExternalTransf a').attr('href', '#');
            //$('#liExternalTransaf a').attr('data-toggle', '#');
            //$('#liExternalTransaf a').attr('data-parent', '#');
            //controls.liExternalTransf.off("click");
            //controls.liExternalTransf.empty();

            SessionTransf.hdnTagSelection = SessionTransf.hdnTrasladoIterno;
            that.getTypification();
            controls.ddlTypeWork.empty();
            $("#ddlTypeSubWork").attr('disabled', false);
            controls.ddlTypeSubWork.empty();

            SessionTransf.hdnTipoTrabCU = SessionTransf.hdnIDTrabTI;
            controls.txChargeAmount.val(SessionTransf.hdnMontoTI);
            that.getWorkType(SessionTransf.hdnTrasladoIterno);
            that.f_cargarSubtipoTrabajo(SessionTransf.hdnIDTrabTI);
            controls.chkLoyalty.prop("checked", false);
            controls.ddlTypeWork.prop("disabled", true);
            //summary
            Smmry.set('txtMonto', SessionTransf.hdnMontoTI);
            controls.btnValidateSchedule.prop("disabled", true);
            Session.codOpcion = SessionTransf.codOpcionTI;
        },

        f_liExternalTransf: function () {
            var that = this;
            var controls = this.getControls();
            SessionTransf.flagvalidation = null;
            $("#IDdivtabTypeWork textarea, #IDdivtabTypeWork input, #IDdivtabTypeWork select").attr('disabled', false);
            $("#cboSchedule").html("");
            $("#cboSchedule").append($('<option>', { value: '-1', html: 'Seleccionar' }));

            $("#ddlReasonSot").attr('disabled', true);
            $("#IDdivtabTypeWorkExternal").after($("#IDdivtabTypeWork"));
            $("#IDdivtabProductsExternal").after($("#IDdivtabProducts"));
            $("#divErrorAlert").hide();
            $("#divErrorExternal").after($("#divErrorAlert"));
            controls.txtNote.val('');
            //that.getCACDAC();
            that.InitCacDac();
            //controls.liInternalTransf.attr('class', 'disabled');
            //$('#liInternalTransf a').attr('href', '#');
            //$('#liInternalTransf a').attr('data-toggle', '#');
            //$('#liInternalTransf a').attr('data-parent', '#');
            //controls.liInternalTransf.off('click');
            //controls.liInternalTransf.empty();

            controls.ddlTypeWork.empty();
            $("#ddlTypeSubWork").attr('disabled', false);
            controls.ddlTypeSubWork.empty();

            SessionTransf.hdnTagSelection = SessionTransf.hdnTrasladoExterno;
            that.getTypification();
            SessionTransf.hdnTipoTrabCU = SessionTransf.hdnIDTrabTE;
            that.getWorkType(SessionTransf.hdnTrasladoExterno);

            controls.ddlTypeWork.val(SessionTransf.hdnIDTrabTE);
            controls.txChargeAmount.val(SessionTransf.hdnMontoTE);
            controls.ddlTypeSubWork.html("");
            controls.ddlTypeSubWork.html("<option value='-1'>Seleccione</option>"); //??

            that.f_cargarSubtipoTrabajo(SessionTransf.hdnIDTrabTE);

            controls.chkLoyalty.prop("checked", false);

            //summary
            Smmry.set('txtMonto', SessionTransf.hdnMontoTE);
            controls.ddlTypeWork.prop('disabled', true);
            controls.btnValidateSchedule.prop("disabled", true);
            Session.codOpcion = SessionTransf.codOpcionTE;
        },

        f_cargarSubtipoTrabajo: function (gConstTipTra) {
            var that = this;
            that.getWorkSubType(gConstTipTra);
        },

        InitCacDac: function () {

            var that = this,
                controls = that.getControls(),
                objCacDacType = {};
            objCacDacType.strIdSession = SessionTransac.SessionParams.USERACCESS.userId;

            var parameters = {};
            parameters.strIdSession = SessionTransac.SessionParams.USERACCESS.userId;
            parameters.strCodeUser = SessionTransac.SessionParams.USERACCESS.login;
            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(parameters),
                url: '/Transactions/CommonServices/GetUsers',
                success: function (results) {
                    var cacdac = results.Cac;
                   // console.log("cacdac: " + cacdac);
                    $.ajax({
                        type: 'POST',
                        contentType: "application/json; charset=utf-8",
                        dataType: 'json',
                        data: JSON.stringify(objCacDacType),
                        url: '/Transactions/CommonServices/GetCacDacType',
                        success: function (response) {
                            controls.ddlCACDAC.append($('<option>', { value: '', html: 'Seleccionar' }));
                            if (response.data != null) { }
                            var itemSelect;
                            $.each(response.data.CacDacTypes, function (index, value) {

                                if (cacdac === value.Description) {
                                    controls.ddlCACDAC.append($('<option>', { value: value.Code, html: value.Description }));
                                    itemSelect = value.Code;

                                } else {
                                    controls.ddlCACDAC.append($('<option>', { value: value.Code, html: value.Description }));
                                }
                            });
                            if (itemSelect != null && itemSelect.toString != "undefined") {
                                //console.log("valor itemSelect: " + itemSelect);
                                $("#ddlCACDAC option[value=" + itemSelect + "]").attr("selected", true);
                            }
                        }
                    });
                }
            });
        },

        f_SetSubTipoTrabajo: function () {
            var that = this, controls = that.getControls();;
            SessionTransf.hdnSubTipOrdCU = $("#ddlTypeSubWork").val();
            if (controls.ddlTypeSubWork.val() == '') {
                //$("#ddlHorarioUsrCtr").html("");
                //$("#ddlHorarioUsrCtr").html("<option value='-1'>-- Seleccionar --</option>");
                return false;
            }
            //alert('SessionTransf.hdnValidaEta: ' + SessionTransf.hdnValidaEta)
            if (SessionTransf.strValidateETA != '0') {
                InitFranjasHorario();
            }
            //sumary
            Smmry.set('SubTipTrabajo', $('#ddlTypeSubWork option:selected').html());
            Smmry.set('TipTrabajo', $('#ddlTypeWork option:selected').html());
            Smmry.set('MotiveSoft', $('#ddlReasonSot option:selected').html());
        },

        f_SetTipoTrabajo: function () {
            var that = this, controls = that.getControls();;
            if (controls.ddlTypeWork.val() != "-1" && controls.ddlTypeWork.val() != '') {
                controls.ddlTypeSubWork.empty();
                that.getWorkSubType(controls.ddlTypeWork.val());
            } else {
                controls.ddlTypeSubWork.empty();
            }

            //sumary
            Smmry.set('TipTrabajo', $('#ddlTypeWork option:selected').html());
            Smmry.set('MotiveSoft', $('#ddlReasonSot option:selected').html());
        },

        f_SetDepartments: function () {

            var that = this;
            var controls = that.getControls();
            $.blockUI({
                message: '<div align="center"><img src="' + that.strUrlLogo + '" width="25" height="25" /> Cargando ... </div>',
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
            controls.ddlNoteProvince.empty();
            controls.ddlNoteDistrict.empty();
            $("#txtCodPla").val('');
            $("#txtCodEdi").val('');
            //sumary
            if (controls.txtNoteNameZote.val() != "") {
                Smmry.set('NombreZona', controls.txtNoteNameZote.val());
            }

            if (controls.txtNoteReference.val() != "") {
                Smmry.set('Referencia', controls.txtNoteReference.val());
            }

            if (controls.ddlNoteDepartment.val() != "") {
                that.GetProvinces(controls.ddlNoteDepartment.val());
                //summary
                Smmry.set('Pais', $("#ddlNoteCountry option:selected").html());
                Smmry.set('Departamento', $("#ddlNoteDepartment option:selected").html());

            } else {
                controls.ddlNoteProvince.empty();
                controls.ddlNoteDistrict.empty();
                $("#txtCodPla").val('');
                $("#txtCodEdi").val('');
            }

        },

        f_SetProvinces: function () {
            var that = this;
            var controls = that.getControls();
            $.blockUI({
                message: '<div align="center"><img src="' + that.strUrlLogo + '" width="25" height="25" /> Cargando ... </div>',
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
            controls.ddlNoteDistrict.empty();
            if (controls.ddlNoteProvince.val() != "-1") {
                that.GetDistricts(controls.ddlNoteDepartment.val(), controls.ddlNoteProvince.val());
                Smmry.set('Provincia', $("#ddlNoteProvince option:selected").html());
            } else {
                Smmry.set('Provincia', "sin seleccionar");
                controls.ddlNoteDistrict.empty();
                $("#txtCodPla").val('');
                $("#txtCodEdi").val('');
            }
        },

        f_SetChkSn: function (sender, arg) {
            var controls = this.getControls();
            var that = this;
            if (sender.prop("checked")) {
                controls.txtNumber.val("S/N");
                controls.txtNumber.attr("disabled", true);
                //summary
                Smmry.set('txtSN', 'SI');


            } else {
                controls.txtNumber.val("");
                controls.txtNumber.attr("disabled", false);
                //summary
                Smmry.set('txtSN', 'NO');
            }
        },

        f_valdiar_True_Cobertura_RadioButton: function (sender, arg) {

            var controls = this.getControls();
            var that = this;
            //sumary
            if (sender.prop("checked")) {
                Smmry.set('CodPlano', $("#txtCodPla").val());
                Smmry.set('ValidarCobertura', 'SI');
            }
        },
        f_validar_False_Cobertura_RadioButton: function (sender, arg) {
            var controls = this.getControls();
            var that = this;
            //summary
            if (sender.prop("checked")) {
                Smmry.set('ValidarCobertura', 'NO');
            }
        },

        f_True_ValEdi: function (sender, arg) {
            var controls = this.getControls();
            var that = this;
            //sumary
            if (sender.prop("checked")) {
                Smmry.set('ValidarEdificio', 'SI');
            }
        },
        f_false_ValEdi: function (sender, arg) {
            var controls = this.getControls();
            var that = this;
            //sumary
            if (sender.prop('checked')) {
                Smmry.set('ValidarEdificio', 'NO');
            }
        },

        f_validar_datos_cambio_direccion: function (sender, arg) {
            var controls = this.getControls();
            var that = this;

            if (sender.prop("checked")) {
                Smmry.set('chkUpdate', 'Si');
            } else {
                Smmry.set('chkUpdate', 'No');
            }
        },

        f_ListarTipMzBloEdi: function () {
            var that = this;
            var urlBase = '/Transactions/LTE/ExternalInternalTransfer';
            var vstrIdSession = Session.IDSESSION;
            $.app.ajax({
                async: true,
                type: 'POST',
                dataType: 'json',
                data: { strIdSession: vstrIdSession },
                url: urlBase + '/GetMzBloEdiType',
                success: function (response) {
                    that.createGetMzBloEdiType(response);
                }
            });
        },

        createGetMzBloEdiType: function (response) {
            var that = this,
               controls = that.getControls();
            var i = 1;
            controls.ddlTipMzBloEdi.append($('<option>', { value: '', html: 'Seleccionar' }));
            if (response.data != null) {
                $.each(response.data.ListGeneric, function (index, value) {
                    controls.ddlTipMzBloEdi.append($('<option>', { value: value.Code, html: value.Description }));

                });
            }
        },

        f_ListarTipDptInt: function () {
            var that = this;
            var urlBase = '/Transactions/LTE/ExternalInternalTransfer';
            var vstrIdSession = Session.IDSESSION;
            $.app.ajax({
                async: true,
                type: 'POST',
                dataType: 'json',
                data: { strIdSession: vstrIdSession },
                url: urlBase + '/GetTipDptInt',
                success: function (response) {
                    that.createGetTipDptInt(response);
                }
            });
        },

        createGetTipDptInt: function (response) {
            var that = this,
               controls = that.getControls();
            var i = 1;
            controls.ddlDepartment.append($('<option>', { value: '', html: 'Seleccionar' }));
            if (response.data != null) {
                $.each(response.data.ListGeneric, function (index, value) {
                    controls.ddlDepartment.append($('<option>', { value: value.Code, html: value.Description }));

                });
            }
        },

        f_SumarDireccionDestino: function () {
            var that = this;
            var controls = that.getControls();
            var strDir = '';
            var ddlTipDir = controls.ddlStreet;
            var txtNomTipDir = controls.txtNameStreet;
            var txtNumDir = controls.txtNumber;
            var chkSN = controls.chkSN;
            var ddlTipMzBloEdi = controls.ddlTipMzBloEdi;
            var txtNroMzBloEdi = controls.txtNumberBlock;
            var txtLote = controls.txtLot;
            var ddlTipDptInt = controls.ddlDepartment;
            var txtNumDptInt = controls.ddlNumberDepartment;
            var txtConDir = controls.txtConDir;

            strDir += (ddlTipDir.val() != '' ? ddlTipDir.val() : '');
            strDir += ($.trim(txtNomTipDir.val()) != '' ? ' ' + $.trim(txtNomTipDir.val()) : '');
            strDir += (txtNumDir.val() != '' ? ' ' + txtNumDir.val() : '');
            strDir += (ddlTipMzBloEdi.val() != '' ? ' ' + ddlTipMzBloEdi.val() : '');
            strDir += ($.trim(txtNroMzBloEdi.val()) != '' ? ' ' + $.trim(txtNroMzBloEdi.val()) : '');
            strDir += ($.trim(txtLote.val()) != '' ? ' LT ' + $.trim(txtLote.val()) : '');
            strDir += (ddlTipDptInt.val() != '' ? ' ' + ddlTipDptInt.val() : '');
            strDir += ($.trim(txtNumDptInt.val()) != '' ? ' ' + $.trim(txtNumDptInt.val()) : '');

            txtConDir.val(strDir.length);
            txtConDir.css({ 'color': strDir.length > 100 ? '#FF0000' : '#000080' });
            if (strDir.length > 100) {
                alert("Se exedió la cantidad máxima de caracteres.", "Alerta");
            }
            else {
                $("#divErrorAlert").hide();
            }
            //sumary
            if (controls.ddlStreet.val() != "") {
                Smmry.set('Region', $("#ddlStreet option:selected").html());
            }
            if (controls.ddlTipMzBloEdi.val() != "") {
                Smmry.set('ddlMz', $("#ddlTipMzBloEdi option:selected").html());
            }
            if (controls.txtNameStreet.val() != "") {
                Smmry.set('ddlNombreCalle', controls.txtNameStreet.val());
            }
            if (controls.txtNumber.val() != "") {
                Smmry.set('txtNumero', controls.txtNumber.val());
            }
            if (controls.txtNumberBlock.val() != "") {
                Smmry.set('NroMz', controls.txtNumberBlock.val());
            }

            if (controls.txtLot.val != "") {
                Smmry.set('txtLote', (controls.txtLot.val()));
            }

            if (controls.ddlDepartment.val() != "") {
                Smmry.set('ddlTipo', $("#ddlDepartment option:selected").html());
            }
            if (controls.txtNoteNameZote.val() != "") {
                Smmry.set('NombreZona', controls.txtNoteNameZote.val());
            }            
        },

        f_SumarNotasDireccion: function () {
            var that = this;
            var controls = that.getControls();
            var strDir = '';
            var ddlTipUrbResPjo = controls.ddlNoteUrbanization;
            var txtNomUrbResPjo = controls.txtNoteUrbanization;
            var ddlTipZonEta = controls.ddlNoteZote;
            var txtNomZonEta = controls.txtNoteNameZote;
            var txtRef = controls.txtNoteReference;
            var txtConNotDir = controls.txtConNotDir;

            strDir += (ddlTipUrbResPjo.val() != '-1' ? ddlTipUrbResPjo.val() : '');
            strDir += ($.trim(txtNomUrbResPjo.val()) != '' ? ' ' + $.trim(txtNomUrbResPjo.val()) : '');
            strDir += (ddlTipZonEta.val() != '-1' ? ' ' + ddlTipZonEta.val() : '');
            strDir += ($.trim(txtNomZonEta.val()) != '' ? ' ' + $.trim(txtNomZonEta.val()) : '');
            strDir += ($.trim(txtRef.val()) != '' ? ' ' + $.trim(txtRef.val()) : '');
            txtConNotDir.val(strDir.length);
            txtConNotDir.css({ 'color': strDir.length > 100 ? '#FF0000' : '#000080' });
            if (strDir.length > 100) {
                alert("Se exedió la cantidad máxima de caracteres.", "Alerta");
            }
            else {
                $("#divErrorAlert").hide();
            }
            //summary
            if (controls.ddlNumberDepartment.val() != "") {
                Smmry.set('NumeroDpto', controls.ddlNumberDepartment.val());
            }
            if (controls.ddlNoteUrbanization.val() != "") {
                Smmry.set('ddlUrb', $("#ddlNoteUrbanization option:selected").html());
            }
            if (controls.txtNoteUrbanization.val() != "") {
                Smmry.set('NombreUrb', controls.txtNoteUrbanization.val());
            }
            if (controls.ddlNoteZote.val() != "") {
                Smmry.set('ddlZona', $('#ddlNoteZote option:selected').html());
            }
        },

        f_ConsultarSOT: function () {
            return false;
        },

        FC_Fallo: function () {
            var that = this;
            var controls = that.getControls();
            alert("Ocurrió un error al validar el permiso.", "Alerta");
            controls.chkLoyalty.prop("checked", false);
            if (SessionTransf.hdnTagSelection == SessionTransf.hdnTrasladoIterno) {
                controls.txChargeAmount.val(SessionTransf.hdnMontoTI);
            } else {
                if (SessionTransf.hdnTagSelection == SessionTransf.hdnTrasladoExterno) {
                    controls.txChargeAmount.val(SessionTransf.hdnMontoTE);
                }
            }
        },

        FC_Cancelar: function () {
            var that = this, controls = that.getControls();
            controls.chkLoyalty.prop("checked", false);
            if (SessionTransf.hdnTagSelection == SessionTransf.hdnTrasladoIterno) {
                controls.txChargeAmount.val(SessionTransf.hdnMontoTI);
            } else {
                if (SessionTransf.hdnTagSelection == SessionTransf.hdnTrasladoExterno) {
                    controls.txChargeAmount.val(SessionTransf.hdnMontoTE);
                }
            }
        },

        FC_GrabarCommit: function () {
            controls.chkLoyalty.prop("checked", true);
            controls.txChargeAmount.val("0.00");
        },

        getTypification: function () {
            var that = this,
			controls = that.getControls(),
			objTypi = {};
            var strTransactionNameType = null;

            if (SessionTransf.hdnTagSelection == '4'){
                strTransactionNameType= "TRANSACCION_DTH_TRASLADO_INTERNO_LTE";
            }
            else{
                strTransactionNameType= "TRANSACCION_DTH_TRASLADO_EXTERNO_LTE";
            }

            objTypi.strIdSession = SessionTransf.IDSESSION;
            objTypi.strTransactionName = strTransactionNameType;

            $.app.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '/Transactions/CommonServices/GetTypification',
                data: JSON.stringify(objTypi),
                success: function (result) {
                    var list = result.ListTypification;
                    if (list != null) {
                        if (list.length > 0) {
                            that.strClaseCode = list[0].CLASE_CODE;
                            that.strSubClaseCode = list[0].SUBCLASE_CODE;
                            that.strTipo = list[0].TIPO;
                            that.strClase = list[0].CLASE;
                            that.strSubClase = list[0].SUBCLASE;

                            that.getBusinessRules();
                        } else {
                            var msg = 'No se reconoce la tipificación de esta transacción.';
                            controls.divErrorAlert.show(); controls.lblErrorMessage.text(msg);
                            if (SessionTransf.hdnTagSelection == '4') {
                                controls.btnConstancyInternal.attr("disabled", true);
                                controls.btnSaveInternal.attr("disabled", true);
                            }
                            else {
                                controls.btnSave.attr("disabled", true);
                                controls.btnConstancy.attr("disabled", true);
                            }
                        }
                    } else {
                        var msg = 'No se reconoce la tipificación de esta transacción.';
                        controls.divErrorAlert.show(); controls.lblErrorMessage.text(msg);
                        if (SessionTransf.hdnTagSelection == '4') {
                            controls.btnConstancyInternal.attr("disabled", true);
                            controls.btnSaveInternal.attr("disabled", true);
                        }
                        else {
                            controls.btnSave.attr("disabled", true);
                            controls.btnConstancy.attr("disabled", true);
                        }
                    }
                }
            });
        },

        getBusinessRules: function () {
            var that = this,
                controls = that.getControls(),
                objRules = {};
            controls.divRules.empty();
            objRules.strIdSession = Session.IDSESSION;
            objRules.strSubClase = that.strSubClaseCode;
        
            $.app.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '/Transactions/CommonServices/GetBusinessRules',
                data: JSON.stringify(objRules),
                success: function (result) {
                    if (result.data.ListBusinessRules != null) {
                        var list = result.data.ListBusinessRules;
                        if (list.length > 0) {
                            controls.divRules.append(list[0].REGLA);
                        }
                    }
                }
            });
        },

        getCustomerData: function () {
            var that = this,
            controls = that.getControls();
            controls.lblCodeUbigeo.text(SessionTransac.SessionParams.DATACUSTOMER.InstallUbigeo);
            controls.lblCustomerName.text(SessionTransf.NameCustomer);
            controls.lblIdentificationDocument.text(SessionTransf.NumbDocRepreCustomer);
            controls.CodeBuilding.text(SessionTransf.IdEdifCustomer);
            controls.lblBillingAmount.text(SessionTransac.SessionParams.DATACUSTOMER.objPostDataAccount.CreditLimit);
            controls.lblStatus.text(SessionTransac.SessionParams.DATACUSTOMER.objPostDataAccount.AccountStatus);
            controls.lblIdContract.text(SessionTransac.SessionParams.DATACUSTOMER.ContractID);
            controls.lblCustomerID.text(SessionTransac.SessionParams.DATACUSTOMER.CustomerID);
            controls.lblDateAct.text(SessionTransac.SessionParams.DATACUSTOMER.objPostDataAccount.ActivationDate);
            controls.lblDateVcto.text(SessionTransac.SessionParams.DATACUSTOMER.objPostDataAccount.ExpirationDate);
            controls.lblCuenta.text(SessionTransac.SessionParams.DATACUSTOMER.Account);
            controls.lblCycleBilling.text(SessionTransac.SessionParams.DATACUSTOMER.objPostDataAccount.BillingCycle);
            controls.lblAddress.text(SessionTransac.SessionParams.DATACUSTOMER.InvoiceAddress);
            controls.lblAddressNote.text(SessionTransac.SessionParams.DATACUSTOMER.InvoiceUrbanization);
            controls.lblCountry.text(SessionTransac.SessionParams.DATACUSTOMER.InvoiceCountry);
            controls.lblDepartament.text(SessionTransac.SessionParams.DATACUSTOMER.InvoiceDepartament);
            controls.lblProvince.text(SessionTransac.SessionParams.DATACUSTOMER.InvoiceProvince);
            controls.lblDistrict.text(SessionTransac.SessionParams.DATACUSTOMER.InvoiceDistrict);
            controls.lblRepLegal.text(SessionTransac.SessionParams.DATACUSTOMER.LegalAgent);
            controls.lblPlanActual.text(SessionTransac.SessionParams.DATASERVICE.Plan == null ? '' : SessionTransac.SessionParams.DATASERVICE.Plan);
            //controls.lblCodePlane.text(SessionTransac.SessionParams.DATACUSTOMER.PlaneCodeInstallation);
            controls.lblCodePlane.text(SessionTransac.SessionParams.DATACUSTOMER.CodeCenterPopulate);            
            controls.lbltypeCustomer.text(SessionTransac.SessionParams.DATACUSTOMER.CustomerType);
            controls.lblContact.text(SessionTransac.SessionParams.DATACUSTOMER.BusinessName);
            controls.lblHUB.text('');
            controls.lblBelt.text('');
            controls.lblCMTS.text('');
            controls.lblCodeFlat.text(SessionTransf.PlanoIDCustomer);
        },

        f_getListDetailsProducto: function () {
            var self = this;
            var that = this,
            objDetProduct = {};
            objDetProduct.strIdSession = Session.IDSESSION;
            objDetProduct.CUSTOMER_ID = SessionTransac.SessionParams.DATACUSTOMER.CustomerID;
            objDetProduct.CONTRATO_ID = SessionTransac.SessionParams.DATACUSTOMER.ContractID;
            var strParam = '';
            var strUrl = '/Transactions/LTE/ExternalInternalTransfer/GetListDataProducts';

            $.ajax({
                type: 'Post',
                url: strUrl,
                data: JSON.stringify(objDetProduct),
                contentType: 'application/json; charset=utf-8',
                datatype: 'json',
                success: function (response) {
                    if (response.data != null) {
                        var registros = response.data.ListDataProducts;
                        that.tblExtransf_Load(registros);
                    }
                }
            });
        },

        tblExtransf_Load: function (data) {
            var that = this,
            controls = that.getControls();

            controls.tblExtransf.DataTable({
                 scrollCollapse: true
                , paging: false
                , scrollY: 180
                , scrollX: true
                , searching: false
                , destroy: true
                , data: data
                , language: {
                    "lengthMenu": "Mostrar _MENU_ registros por página.",
                    "zeroRecords": "No existen datos",
                    "info": " ",
                    "infoEmpty": " ",
                    "infoFiltered": "(filtrado de  _MAX_ total registros)"
                }
                , columns: [
                   { "data": "MaterialCode", "width": "100px" },
                    { "data": "SapCode", "width": "100px" },
                    { "data": "SerieNumber", "width": "100px" },
                    { "data": "AdressMac", "width": "100px" },
                    { "data": "MaterialDescripcion", "width": "200px" },
                    { "data": "EquipmentType", "width": "200px" },
                    { "data": "ProductId", "width": "200px" },
                    { "data": "Type", "width": "100px" },//Modelo
                    { "data": "ConvertType", "width": "100px" },
                    { "data": "ServiceType", "width": "100px" },
                    { "data": "Headend", "width": "50px" },
                    { "data": "PricipalService", "width": "100px" },//Perfil
                    { "data": "EphomeexChange", "width": "50px" }//Número
                ]
            });
        },

        f_AbrirCobertura: function () {
            return false;
        },

        //EDIFICIO - PLANOS INICIO------------

        f_AbrirValidarEdificio: function () {
            var that = this;
            var controls = that.getControls();
            if ($("#txtCodPla").val() == "") {
                alert(SessionTransf.hdnMensaje9, "Alerta");
                return false;
            }
            if ($("#ddlNoteDistrict").val() == "-1") {
                alert(SessionTransf.hdnMensaje2, "Alerta");
                return false;
            }

            $("#pnlValidarEdificio").dialog({
                height: 450,
                width: 800,
                resizable: false,
                draggable: false,
                scrollY: true,
                overflow: true,
                modal: true,
                open: function () {
                    that.f_ListarEdificios();
                }
            });

            return false;
        },

        f_ListarEdificios: function () {
            var urlBase = '/Transactions/LTE/ExternalInternalTransfer';
            var that = this;
            var controls = that.getControls();
            var objListarEdificios = {};
            objListarEdificios.strIdSession = Session.IDSESSION;
            objListarEdificios.vstrCodPlano = $("#txtCodPla").val();

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objListarEdificios),
                url: urlBase + "/GetListEdificios",
                success: function (response) {
                    var registros = response.data.ListGeneric;
                    if (registros == null) {
                        alert(SessionTransf.hdnMensaje4, "Alerta");
                    }
                    else {
                        that.f_IniTablaEdificios(registros);
                    }

                }
            });

        },

        f_IniTablaEdificios: function (data) {
            var that = this,
           controls = that.getControls();
            $('#tblListaEdificio').DataTable({
                 scrollCollapse: true
                , paging: true
                , scrollY: 180
                , scrollX: true
                , searching: true
                , destroy: true
                , overflow: true
                , data: data
                , language: {
                    "lengthMenu": "Mostrar _MENU_ registros por página.",
                    "zeroRecords": "No existen datos",
                    "paging": true,
                    "sProcessing": "Procesando...",
                    "sSearch": "Buscar: ",
                    "pageLength": 10,
                    "oPaginate": {
                        "sFirst": "Primero",
                        "sPrevious": "Anterior",
                        "sNext": "Siguiente",
                        "sLast": "Último"
                    },
                    "info": " ",
                    "infoEmpty": " ",
                    "infoFiltered": "(filtered from _MAX_ total records)"
                }
                , columns: [
                     { "data": null },
                    { "data": "Number" },
                    { "data": "Code" },
                    { "data": "Code2" },
                    { "data": "Code3" },
                    { "data": "Description" },
                    { "data": "Description2" },
                    { "data": "Type" }

                ],
                "columnDefs": [
                        {
                            targets: 0,
                            render: function (data, type, full, meta) {
                                var $rb = $('<input>', {
                                    type: 'radio',
                                    value: full.Type
                                    , name: 'tblListaEdificio_rbnID'
                                    , class: "tblListaEdificio_rbn"
                                });
                                return $rb[0].outerHTML;
                            }
                        }
                ]
            });
        },

        f_SeleccionarEdificio: function () {
            var val = $('input[class="tblListaEdificio_rbn"]:checked').val();

            if (val == undefined) {
                alert(SessionTransf.hdnMensaje8, "Alerta");
            }
            else {
                $('#txtCodEdi').val(val);
                //summary
                Smmry.set('CodEdificio', $('#txtCodEdi').val());
                SessionTransf.hdnCodEdi = val;
                this.f_CerrarVentanaEdificios();
            }
        },

        f_CerrarVentanaEdificios: function () {
            $('#pnlValidarEdificio').dialog("close");
        },

        f_AbrirValidarPlano: function () {

            var that = this,
           controls = that.getControls();

            if (controls.ddlNoteDistrict.val() == null) {
                return false;
            }

                    that.f_ListarPlanos();

            //$("#pnlValidarPlano").dialog({
            //    height: 450,
            //    width: 800,
            //    resizable: false,
            //    draggable: false,
            //    overflowy: true,
            //    modal: true,
            //    open: function () {
            //        //that.f_ListarPlanos();
            //        that.f_IniTablaPlano(SessionTransf.ListPlanos);
            //    }
            //});

            return false;
        },

        f_ListarPlanos: function () {
            var urlBase = '/Transactions/LTE/ExternalInternalTransfer';
            var that = this,
           objListarPlanos = {};
            objListarPlanos.strIdSession = SessionTransf.IDSESSION;
            objListarPlanos.vCodUbigeo = SessionTransf.hdnUbiID;
            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objListarPlanos),
                url: urlBase + "/GetListPlanos",
                success: function (response) {
                    if (response.data != null && response.data != '') {
                    var registros = response.data.ListGeneric;
                        $("#pnlValidarPlano").dialog({
                            height: 450,
                            width: 800,
                            resizable: false,
                            draggable: false,
                            overflowy: true,
                            modal: true,
                            open: function () {
                        that.f_IniTablaPlano(registros);
                            }
                        });
                    }
                    else {
                        alert("No se encontró Planos Configurados.", "Alerta");
                     }
                }
            });



        },

        f_IniTablaPlano: function (data) {
            var that = this,
           controls = that.getControls();
            $('#tblListaPlano').DataTable({
                  scrollCollapse: true
                , paging: true
                , scrollY: 180
                , destroy: true
                , searching: true
                , language: {
                    "lengthMenu": "Mostrar _MENU_ registros por página.",
                    "zeroRecords": "No existen datos",
                    "paging": true,
                    "sProcessing": "Procesando...",
                    "sSearch": "Buscar: ",
                    "pageLength": 10,
                    "oPaginate": {
                        "sFirst": "Primero",
                        "sPrevious": "Anterior",
                        "sNext": "Siguiente",
                        "sLast": "Último"
                    },
                    "info": " ",
                    "infoEmpty": " ",
                    "infoFiltered": "(filtered from _MAX_ total records)"
                }
                , data: data
                , columns: [
                     { "data": null },
                     { "data": "Number" },
                     { "data": "Code" },
                     { "data": "Code2" },
                     { "data": "Code3" }
                ],
                "columnDefs": [
                        {
                            targets: 0,
                            render: function (data, type, full, meta) {
                                var $rb = $('<input>', {
                                    type: 'radio',
                                    value: full.Date
                                    , name: 'tblListaPlano_rbnID'
                                    , class: "tblListaPlano_rbn"
                                });
                                return $rb[0].outerHTML;
                            }
                        }

                ]
            });



        },

        f_SeleccionarPlano: function () {
            var that = this;
            var val = $('input[class="tblListaPlano_rbn"]:checked').val();

            if (val == undefined) {
                alert(SessionTransf.hdnMensaje9, "Alerta");
            }
            else {
                $('#txtCodPla').val(val);
                //sumary
                Smmry.set('CodPlano', $('#txtCodPla').val());
                SessionTransf.hdnCodPla = $('#txtCodPla').val();
                SessionTransf.hdnIDPlano = val;
                $('#txtCodEdi').val('');
                that.f_CerrarVentanaPlanos();
            }
        },

        f_CerrarVentanaPlanos: function () {
            $('#pnlValidarPlano').dialog("close");
        },

        getIGV: function () {
            var that = this,
                controls = that.getControls(),
                objIGV = {};

            objIGV.strIdSession = Session.IDSESSION;

            $.app.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objIGV),
                url: '/Transactions/CommonServices/GetConsultIGV',
                error: function (ex) {
                    that.f_NextIgv();
                },
                success: function (result) {

                    if (result.data != null) {
                        var igv = parseFloat(result.data.igvD);
                        var igvEnt = igv;
                        that.strIGV = igvEnt;
                    }
                    else {
                        that.f_NextIgv();
                    }
                }
            });
        },

        //EDIFICIO - PLANOS FIN  --------------------

        f_ObtenerCobertura: function () {
            $("#txtCodPla").val('');
            $("#txtCodEdi").val('');
            var urlBase = '/Transactions/LTE/ExternalInternalTransfer';
            var that = this;
            var controls = that.getControls();
            that.f_ClearCobertura();
            var objObtenerCobertura = {};
            objObtenerCobertura.strIdSession = Session.IDSESSION;
            objObtenerCobertura.valNoteCenterPopulated = controls.ddlNoteCenterPopulated.val();

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objObtenerCobertura),
                url: urlBase + "/GetCobertura",
                success: function (response) {
                   
                    var cobertura = response.data;
                    if (cobertura.Code == '1') {
                        $('#idtruelnkCob').prop("checked", true);
                        //smmry
                        Smmry.set('ValidarCobertura', 'SI');
                    }
                    else {
                        $('#idfalselnkCob').prop("checked", true);
                        //smry
                        Smmry.set('ValidarCobertura', 'NO');
                    }
                }
            });
            //sumary
            if (controls.ddlNoteCenterPopulated.val() != "") {
                Smmry.set('CentroPoblado', $("#ddlNoteCenterPopulated option:selected").html());
            }
            if (controls.txtNoteCodePostal.val() != "") {
                Smmry.set('CodPostal', controls.txtNoteCodePostal.val());
            }
        },

        f_ClearCobertura: function () {
            $('#idtruelnkCob').attr('checked', false);
            $('#idfalselnkCob').attr('checked', false);
        },

        F_ValidateETA: function (pTipoTrabajo) {
            var that = this, model = {};
            model.IdSession = SessionTransf.IDSESSION;;
            model.strJobTypes = $('#ddlTypeWork').val();
            if (pTipoTrabajo == '3'){
                model.strCodeUbigeo = $('#ddlNoteCenterPopulated').val();
                model.StrTypeService = SessionTransf.strCodTipServLteTE;
            } else{
                model.strCodeUbigeo = $('#lblCodePlane').text();
                model.StrTypeService = SessionTransf.strCodTipServLteTI;
            }
            
            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(model),
                url: '/Transactions/SchedulingToa/GetValidateETA',
                success: function(response) {
                    var oItem = response.data;
                    var fechaServidor = new Date(Session.ServerDate);
                    $('#txt_dDateProgramming').val([that.f_pad(fechaServidor.getDate()), that.f_pad(fechaServidor.getMonth() + 1),fechaServidor.getFullYear()].join("/"));

                    if (oItem.Codigo == '0' || oItem.Codigo == '1' || oItem.Codigo == '2') {
                        SessionTransf.strValidateETA = oItem.Codigo;
                        SessionTransf.strHistoryETA = oItem.Codigo2;
                        Session.ValidateETA = oItem.Codigo;
                        Session.History = oItem.Codigo2;

                        if (oItem.Codigo == '2') {
                            $("#tr_SubWorkType").show();
                            that.f_EnableAgendamiento(true);
                        }else if (oItem.Codigo == '1') {
                            $("#tr_SubWorkType").show();
                            that.f_EnableAgendamiento(true);
                        }else {
                            Session.ValidateETA = "0";
                            Session.History = "";
                            SessionTransf.strValidateETA = "0";
                            $("#tr_SubWorkType").prop('disabled', true);
                            that.f_EnableAgendamiento(false);
                            InitFranjasHorario();
                            $("#ddlTypeSubWork").prop('disabled', true);
                            alert("No aplica agendamiento en línea, favor de continuar con la operación.", "Alerta");
                        }
                    }else {
                        if (oItem.Descripcion == null)
                            oItem.Descripcion = " ";

                        Session.ValidateETA = "0";
                        SessionTransf.strValidateETA = "0";
                        SessionTransf.strHistoryETA = oItem.Codigo2;
                        that.f_EnableAgendamiento(false);
                        Session.History = oItem.Codigo2;
                        InitFranjasHorario();
                        $("#ddlTypeSubWork").prop('disabled', true);
                        alert(SessionTransf.strMessageValidationETA, "Alerta");
                    }
                }
            });
        },

        f_pad: function (s) { return (s < 10) ? '0' + s : s; },

        f_EnableAgendamiento: function (bool) {
            var that = this;
            if (bool == true) {
                $('#txt_dDateProgramming').prop("disabled", false);
                $('#cboSchedule').prop("disabled", false);
                $('#btnValidateSchedule').prop("disabled", true);
            }
            else {
                var fechaServidor = new Date(Session.ServerDate);

                $('#txt_dDateProgramming').val([that.f_pad(fechaServidor.getDate()), that.f_pad(fechaServidor.getMonth() + 1), fechaServidor.getFullYear()].join("/"));

                if ($('#ddlTypeWork').val().indexOf(".|") == -1) {
                    $('#btnValidateSchedule').prop("disabled", true);
                    Session.VALIDATE = "1";
                }
                else {
                    $('#btnValidateSchedule').prop("disabled", false);
                    Session.VALIDATE = "0";
                }
            }
        },

        f_ClearUbigeos: function () {
            this.f_ClearProvincia();
            this.f_ClearDistrito();
        },

        f_ClearProvincia: function () {
            var pro = $("#ddlProDes");
            pro.html('');
            pro.append('<option value=-1>' + 'seleccione' + '</option>');
        },

        f_ClearDistrito: function () {
            var that = this;
            var controls = that.getControls();
            controls.ddlNoteDistrict.html('');
            controls.txtNoteCodePostal.val('');
            this.f_ClearCentroPoblado();
        },

        f_ClearCentroPoblado: function () {
            var that = this;
            var controls = that.getControls();
            controls.ddlNoteCenterPopulated.html('');
            that.f_ClearCobertura();
        },

        // CLEAR - CONTROLS FIN----------

        f_GenerateAddressNote: function () {
            var that = this;
            var controls = that.getControls();
            var strEnd;
            var strBldDireccion = "";

            if (controls.ddlStreet.val() > 0) {
                strBldDireccion = strBldDireccion + string.format("{0} {1}", $("#ddlStreet option:selected").html().substring(0, 2), controls.txtNameStreet.val());
            }
            else {
                strBldDireccion = strBldDireccion + string.format(" {0}", "CA")//ValueNoTieneTipoVia
            }

            if (controls.txtNumber.val() != '') {
                strBldDireccion = strBldDireccion + string.format(" {0}", controls.txtNumber.val().trim());
            }

            if (controls.txtNumber.val().trim() == '' && controls.txtNumberBlock.val().trim() == '' && controls.txtLot.val().trim() == '') {
                strBldDireccion = strBldDireccion + string.format(" {0}", 'S/N');//gConstKeyStrSinNumero
            }

            if (controls.txtNoteUrbanization.val() > 0) {
                strBldDireccion = strBldDireccion + string.format(" {0} {1}", $("#ddlNoteUrbanization option:selected").html(), controls.txtNoteUrbanization.val().toUpperCase());
                if (controls.txtNote.val() != '') {
                    strBldDireccion = strBldDireccion + string.format(" {0} {1}", 'LT', controls.txtLot.val().trim());//APOCOPE_LOTE 
                }
            }

            if (controls.ddlDepartment.val() > 0) {
                strBldDireccion = strBldDireccion + string.format(" {0} {1}", $("#ddlDepartment option:selected").html(), controls.ddlNumberDepartment.val());
            }

            strEnd = strBldDireccion;

            return strEnd;
        },

        btnConstancy_click: function () {
            var that = this,
               controls = that.getControls();

            //if (SessionTransf.strPath == "") {
            //    if (SessionTransf.hdnTagSelection == '4') {
            //        controls.btnConstancyInternal.attr("disabled", true); 
            //    }
            //    else {
            //        controls.btnConstancy.attr("disabled", true); 
            //    }

            //}
            //else{
            //    if (SessionTransf.hdnTagSelection == '4'){
            //        controls.btnConstancyInternal.attr("disabled", false);
            //    }
            //    else{
            //        controls.btnConstancy.attr("disabled", false); 
            //    }
                ReadRecordSharedFile(Session.IDSESSION, SessionTransf.strPath);
           // }
   

        },

        btnClose_click: function () {
            parent.window.close();
        },

        btnSave_click: function () {
            var that = this,
                controls = that.getControls();
                $('#ddlCACDAC').trigger("change");
            //if (!controls.chkLoyalty.prop('checked')) {
            //    if (that.strIGV == null || that.strIGV == '') {

            //        if (SessionTransf.hdnTagSelection == SessionTransf.hdnTrasladoExterno)
            //        {
            //            $("#btnSave").attr("disabled", true);
            //        }
            //        else
            //        {
            //            $("#btnSaveInternal").attr("disabled", true);
            //        }

            //        that.f_NextIgv();
                
            //        return false;
            //    }
            //}

            if (controls.txt_dDateProgramming.val() == '' || controls.txt_dDateProgramming.val() == '-1') {
                alert("Seleccione Fecha de compromiso.", "Alerta");
                return false;
            }

            if (SessionTransf.hdnTagSelection == SessionTransf.hdnTrasladoExterno) {
                if (!that.f_ValidateCampoNumeroDireccion()) {
                return false;
                };

                /*if ($("#txtCodPla").val() == "") {
                    alert(SessionTransf.hdnMensaje9, "Alerta");
                    return false;
                }*/

                if (SessionTransf.strValidateETA == '2') {
                    if (controls.ddlTypeSubWork.val() == '') {
                        alert("Seleccione sub tipo de trabajo.", "Alerta");
                return false;
            }

                    if ($('#cboSchedule').val() == '-1' || $('#cboSchedule').val() == '') {
                        $.each(Session.vMessageValidationList, function (index, value) {
                            if (value.ABREVIATURA_DET == "MSJ_OBLIG_ETA") {
                                alert(value.CODIGOC, "Alerta");
                    return false;
                }
                        });
                    }
                }
            }
            else {
                if (SessionTransf.strValidateETA == '2') {
                    if (controls.ddlTypeSubWork.val() == '') {
                        alert("Seleccione Sub tipo de trabajo.", "Alerta");
                        return false;
                    }

                    if ($('#cboSchedule').val() == '-1' || $('#cboSchedule').val() == '' || $('#txt_dDateProgramming').val() == '') {
                        $.each(Session.vMessageValidationList, function (index, value) {
                            if (value.ABREVIATURA_DET == "MSJ_OBLIG_ETA") {
                                alert(value.CODIGOC, "Alerta");                                
                            }
                        });
                        return false;
                    }
                }
                }

            if (controls.ddlCACDAC.val() == "-1" || controls.ddlCACDAC.val() == "") {
                alert("Seleccione punto de atención.", "Alerta");
                return false;
            }

            if (!this.f_ValidateEmail())
                return;

            SessionTransf.hdnHayFidelizaFinal = "0";
            if ($("#chkLoyalty").prop("checked")) {
                SessionTransf.hdnHayFidelizaFinal = "1";
            }
            SessionTransf.hdnMontoFideFinal = controls.txChargeAmount.val();
            SessionTransf.hdnProDes = controls.ddlNoteProvince.val() == '-1' ? '' : $("#ddlNoteProvince option:selected").text();
            SessionTransf.hdnDisDes = controls.ddlNoteDistrict.val() == '-1' ? '' : $("#ddlNoteDistrict option:selected").text();
            SessionTransf.hdnCenPobDes = controls.ddlNoteCenterPopulated.val() == '-1' ? '' : $("#ddlNoteCenterPopulated option:selected").text();
            that.GetRecordTransaction();
        },

        /*f_CoberturaClose: function () {
            $("#pnlValidarCoberturaURL").dialog("close");
        },*/

        GetRecordTransaction: function () {
            var REFERENCIA = this.f_GenerateAddressNote();
            var that = this;
            var controls = that.getControls();
            var urlBase = '/Transactions/LTE/ExternalInternalTransfer';
            var objLTEBETransfer = {};

            objLTEBETransfer.strIdSession = SessionTransf.IDSESSION;
            objLTEBETransfer.strtypetransaction = SessionTransf.hdnTagSelection;
            objLTEBETransfer.ConID = SessionTransf.CONTRATO_ID;
            objLTEBETransfer.CustomerID = SessionTransf.CUSTOMER_ID;
            objLTEBETransfer.InterCasoID = "";
            objLTEBETransfer.Telephone = SessionTransf.PhonfNro;
            objLTEBETransfer.TipoVia = controls.ddlStreet.val();
            objLTEBETransfer.NomVia = controls.txtNameStreet.val();
            objLTEBETransfer.TipoUrb = controls.ddlNoteUrbanization.val();
            objLTEBETransfer.hdnTipoUrb = $("#ddlNoteUrbanization option:selected").html();
            objLTEBETransfer.NomUrb = controls.txtNoteUrbanization.val();
            objLTEBETransfer.NumLote = controls.txtLot.val();
            objLTEBETransfer.Ubigeo = SessionTransf.hdnUbiID;
            objLTEBETransfer.ZonaID = "";
            objLTEBETransfer.PlanoID = SessionTransac.SessionParams.DATACUSTOMER.PlaneCodeInstallation;
            objLTEBETransfer.EdificioID = SessionTransf.hdnCodEdi;//
            objLTEBETransfer.Referencia = controls.txtNoteReference.val();
            objLTEBETransfer.RefNoteDirec = REFERENCIA;
            objLTEBETransfer.Observacion = controls.txtNote.val(); 
            objLTEBETransfer.MotivoID = controls.ddlReasonSot.val();
            objLTEBETransfer.NroVia = controls.txtNumber.val();
            objLTEBETransfer.chkUseChangeBillingChecked = controls.chkUseChangeBilling.prop('checked');
            objLTEBETransfer.chkEmailChecked = controls.chkSendMail.prop('checked');
            objLTEBETransfer.Email = controls.txtSendMail.val();
            objLTEBETransfer.chkSN = controls.chkSN.prop('checked');
            objLTEBETransfer.txtNotText = controls.txtNote.val();
            objLTEBETransfer.hdnCodigoRequestAct = (Session.RequestActId == '' || Session.RequestActId == null) ? '0' : Session.RequestActId;// SessionTransf.CodigoRequestAct;
            objLTEBETransfer.Cargo = SessionTransf.hdnMontoFideFinal;
            objLTEBETransfer.DescripCADDAC = $("#ddlCACDAC option:selected").html();
            objLTEBETransfer.DescrpCountry = $("#ddlNoteCountry option:selected").html();
            objLTEBETransfer.ddlDepDes = $("#ddlNoteDepartment option:selected").html();
            objLTEBETransfer.hdnProDes = $("#ddlNoteProvince option:selected").html();
            objLTEBETransfer.hdnDisDes = $("#ddlNoteDistrict option:selected").html();
            objLTEBETransfer.ddlNoteDepartment = controls.ddlNoteDepartment.val();
            objLTEBETransfer.hdnCodEdi = SessionTransf.hdnCodEdi;
            objLTEBETransfer.hdnCenPobDes = SessionTransf.hdnCenPobDes;
            objLTEBETransfer.hdnCodPla = SessionTransf.hdnCodPla;
            objLTEBETransfer.hdnUbiID = SessionTransf.hdnUbiID;
            objLTEBETransfer.ddlTipMzBloEdi = controls.ddlTipMzBloEdi.val();
            objLTEBETransfer.hdnTipMzBloEdi = controls.txtNumberBlock.val();
            objLTEBETransfer.ddlDepartment = controls.ddlDepartment.val();
            objLTEBETransfer.hdnDepartment = $("#ddlDepartment option:selected").html();
            objLTEBETransfer.hdnNumberDepartment = controls.ddlNumberDepartment.val();
            objLTEBETransfer.hdnTipoVia = $("#ddlStreet option:selected").html();
            objLTEBETransfer.NameCustomer = SessionTransf.NameCustomer;
            objLTEBETransfer.NotAddressCustomer = SessionTransf.NotAddressCustomer;
            objLTEBETransfer.CountryCustomer = SessionTransf.CountryCustomer;
            objLTEBETransfer.PAIS_LEGAL = SessionTransac.SessionParams.DATACUSTOMER.LegalCountry;
            objLTEBETransfer.DepCustomer = SessionTransf.DepCustomer;
            objLTEBETransfer.ProvCustomer = SessionTransf.ProvCustomer;
            objLTEBETransfer.DistCustomer = SessionTransf.DistCustomer;
            objLTEBETransfer.IdEdifCustomer = SessionTransf.IdEdifCustomer;
            objLTEBETransfer.EmailCustomer = SessionTransf.EmailCustomer;
            objLTEBETransfer.PlanoIDCustomer = '';
            objLTEBETransfer.urbLegalCustomer = SessionTransf.urbLegalCustomer;
            objLTEBETransfer.DirecDespachoCustomer = objLTEBETransfer.DirecDespachoCustomer;
            objLTEBETransfer.TypDocRepreCustomer = SessionTransf.TypDocRepreCustomer;
            objLTEBETransfer.NumbDocRepreCustomer = SessionTransf.NumbDocRepreCustomer;
            objLTEBETransfer.RepreCustomer = SessionTransf.RepreCustomer;
            objLTEBETransfer.cuenta = SessionTransf.cuentaCustomer;
            objLTEBETransfer.CurrentUser = SessionTransac.SessionParams.USERACCESS.login;
            objLTEBETransfer.USRREGIS = SessionTransac.SessionParams.USERACCESS.login;
            objLTEBETransfer.agendaGetFecha = $('#txt_dDateProgramming').val();
            objLTEBETransfer.FechaProgramada = $('#txt_dDateProgramming').val();
            objLTEBETransfer.agendaGetCodigoFranja = $("#cboSchedule option:selected").val();
            objLTEBETransfer.agendaGetValidaEta = SessionTransf.strValidateETA;
            objLTEBETransfer.agendaGetTipoTrabajo = controls.ddlTypeWork.val();
            objLTEBETransfer.ObtenerHoraAgendaETA = $("#cboSchedule option:selected").html();
            objLTEBETransfer.FranjaHora = $("#cboSchedule option:selected").attr("idHorario");
            objLTEBETransfer.CicloFact = SessionTransac.SessionParams.DATACUSTOMER.BillingCycle;
            objLTEBETransfer.chkLoyalty = controls.chkLoyalty.prop('checked');
            objLTEBETransfer.DEPARTAMENTO = SessionTransac.SessionParams.DATACUSTOMER.Departament;
            objLTEBETransfer.CodPos = SessionTransac.SessionParams.DATACUSTOMER.LegalPostal;
            objLTEBETransfer.hdnCodPos = controls.txtNoteCodePostal.val();
            objLTEBETransfer.RefAddressCustomer = SessionTransac.SessionParams.DATACUSTOMER.LegalUrbanization;
            objLTEBETransfer.AddressCustomer = SessionTransac.SessionParams.DATACUSTOMER.InvoiceAddress;
            objLTEBETransfer.hdnDepDes = $("#ddlNoteDepartment option:selected").html();
            objLTEBETransfer.strIgv = that.strIGV == null || that.strIGV == '' ? '0.00' : that.strIGV;
            objLTEBETransfer.strSubTypeWork = $("#ddlTypeSubWork option:selected").val();
            objLTEBETransfer.strtypeCliente = SessionTransac.SessionParams.DATACUSTOMER.CustomerType == 'Consumer' ? 'MASIVO' : 'CORPORATIVO';
            
objLTEBETransfer.strCodOCC = SessionTransf.codcargoOCC;
            objLTEBETransfer.codCenPob =SessionTransf.hdnTagSelection=='3'? controls.ddlNoteCenterPopulated.val():SessionTransac.SessionParams.DATACUSTOMER.CodeCenterPopulate;
            
            confirm(SessionTransf.hdnMensaje7, 'Confirmar', function (result) {
                if (result == true) {
                    $.blockUI({
                        message: '<div align="center"><img src="' + that.strUrlLogo + '" width="25" height="25" /> Cargando ... </div>',
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
                    $.app.ajax({
                        async: true,
                        type: 'POST',
                        contentType: "application/json; charset=utf-8",
                        dataType: 'json',
                        data: JSON.stringify(objLTEBETransfer),
                        url: urlBase + '/GetRecordTransactionIntExt',
                        success: function (response) {
                            that.ReturnGetRecordTransactionIntExt(response);
                        }
                    });
                }
            });
        },

        ReturnGetRecordTransactionIntExt: function (response) {
            var that = this,
            controls = that.getControls();
            var message;
            if (response.data != null) {
                if (response.data.ItemGeneric.Number > 0){
                    message = string.format("{0}. - Nro. Sot: {1}.", response.data.ItemGeneric.Description,response.data.ItemGeneric.Number);
                    SessionTransf.strPath = response.data.ItemGeneric.Code3;
                    controls.lnkNumSot.html(response.data.ItemGeneric.Number);

                    if (SessionTransf.hdnTagSelection == '4') {
                        controls.btnConstancyInternal.attr("disabled", false);
                        controls.btnSaveInternal.attr("disabled", true);
                        $("#IDdivtabTypeWork textarea, #IDdivtabTypeWork input, #IDdivtabTypeWork select").attr('disabled', true);
                        SessionTransf.flagSaveTransactions = 'I';
                    }
                    else {
                        controls.btnConstancy.attr("disabled", false);
                        controls.btnSave.attr("disabled", true);
                        $('.btn-circle').attr("disabled", true);
                        $('#idPrev5').attr("disabled", true);
                    }
                    alert(response.data.ItemGeneric.Description, "Informativo");
                }
                else{
                    if (SessionTransf.hdnTagSelection == '4') {
                        controls.btnSaveInternal.attr("disabled", true);
                        controls.btnConstancyInternal.attr("disabled", true);
                    }
                    else {
                        controls.btnSave.attr("disabled", true);
                        controls.btnConstancy.attr("disabled", true);
                        $('.btn-circle').attr("disabled", true);
                        $('#idPrev5').attr("disabled", true);
                    }
                    alert(response.data.ItemGeneric.Description, "Alerta");
                    message = response.data.ItemGeneric.Description;
                }

                if (response.data.ItemGeneric.Code2 == "0" || response.data.ItemGeneric.Code2 == null) {
                    alert(response.data.ItemGeneric.Description2, "Alerta");
                    message = message + "<br>" + response.data.ItemGeneric.Description2;
                }
                else {
                    SessionTransf.hdnInterID = response.data.ItemGeneric.Code2;
                }
                controls.lblErrorMessage.html(message);
                controls.divErrorAlert.show();
            }

        },

        //LOAD CONTROLS------------------------------------

        getLoadMont: function () {
            var that = this;
            var vstrIdSession = Session.IDSESSION;
            that.creategetLoadMont();

        },
        creategetLoadMont: function () {
            var that = this,
             controls = that.getControls();

            var playCounter = 0;
            var gstrVariableT = 'T';
            
            if (SessionTransf.DatosLineatelefonia == gstrVariableT) {
                playCounter = playCounter + 1;
            }

            if (SessionTransf.DatosLineainternet == gstrVariableT) {
                playCounter = playCounter + 1;
            }

            if (SessionTransf.DatosLineacableTv == gstrVariableT) {
                playCounter = playCounter + 1;
            }

            if (SessionTransf.hdnTagSelection == '4') {
                SessionTransf.hdnMontoTI = SessionTransf.cargoOCCTILTE;
                SessionTransf.codcargoOCC = SessionTransf.codOCCTIPLTE;
            }
            else {
                switch (playCounter) {
                case 1:
                    SessionTransf.hdnMontoTE = SessionTransf.cargoOCCTE1PLTE;
                    SessionTransf.codcargoOCC = SessionTransf.codOCCTE1PLTE;
                    break;
                case 2:
                    SessionTransf.hdnMontoTE = SessionTransf.cargoOCCTE2PLTE;
                    SessionTransf.codcargoOCC = SessionTransf.codOCCTE2PLTE;
                    break;
                case 3:
                    SessionTransf.hdnMontoTE = SessionTransf.cargoOCCTE3PLTE;
                    SessionTransf.codcargoOCC = SessionTransf.codOCCTE3PLTE;
                    break;
                }
            }

            if (SessionTransf.AccesPage.indexOf(SessionTransf.gConstOpcTIEHabFidelizarLTE) > -1) {
                SessionTransf.hdnTienePerfilJefe = "1";
            }

            that.f_liInternalTransf();
        },

        getddlStreet: function () {
            var that = this;
            var vstrIdSession = Session.IDSESSION;
            var vstridList = "TIPO_VIA";
            var urlBase = '/Transactions/LTE/ExternalInternalTransfer';

            $.app.ajax({
                type: 'POST',
                dataType: 'json',
                contentType: "application/json; charset=utf-8",
                data: "{strIdSession:'" + vstrIdSession + "' , idList:'" + vstridList + "'}",
                url: urlBase + '/GetStateType',
                success: function (response) {
                    that.createDropdownStreet(response);
                }
            });
        },

        createDropdownStreet: function (response) {
            var that = this,
               controls = that.getControls();
            var i = 1;
            controls.ddlStreet.append($('<option>', { value: '', html: 'Seleccionar' }));

            if (response.data != null) {
                $.each(response.data.ListGeneric, function (index, value) {
                    controls.ddlStreet.append($('<option>', { value: value.Code, html: value.Description }));

                });
            }
        },

        getddlNoteUrbanization: function () {
            var that = this;
            var strIdSession = Session.IDSESSION;
            var idList = "TIPO_URB";
            var urlBase = '/Transactions/LTE/ExternalInternalTransfer';

            $.app.ajax({
                type: 'POST',
                dataType: 'json',
                contentType: "application/json; charset=utf-8",
                data: "{strIdSession:'" + strIdSession + "' , idList:'" + idList + "'}",
                url: urlBase + '/GetStateType',
                success: function (response) {
                    that.createDropdownNoteUrbanization(response);
                }
            });
        },

        createDropdownNoteUrbanization: function (response) {
            var that = this,
               controls = that.getControls();
            var i = 1;
            controls.ddlNoteUrbanization.append($('<option>', { value: '', html: 'Seleccionar' }));

            if (response.data != null) {
                $.each(response.data.ListGeneric, function (index, value) {
                    controls.ddlNoteUrbanization.append($('<option>', { value: value.Code, html: value.Description }));

                });
            }
        },

        GetDepartments: function () {
            var that = this;
            var urlBase = '/Transactions/LTE/ExternalInternalTransfer';

            var strIdSession = Session.IDSESSION;
            $.app.ajax({
                type: 'POST',
                dataType: 'json',
                contentType: "application/json; charset=utf-8",
                data: "{strIdSession:'" + strIdSession + "'}",
                url: urlBase + '/GetDepartments',
                success: function (response) {
                    that.createDropdownDepartments(response);
                }
            });
        },

        createDropdownDepartments: function (response) {
            var that = this,
               controls = that.getControls();
            var i = 1;
            controls.ddlNoteDepartment.append($('<option>', { value: '', html: 'Seleccionar' }));

            if (response.data != null) {
                $.each(response.data.ListGeneric, function (index, value) {
                    controls.ddlNoteDepartment.append($('<option>', { value: value.Code, html: value.Description }));
                });
            }
        },

        GetProvinces: function (strDepartments) {
            var that = this;
            that.f_ClearUbigeos();
            var strIdSession = Session.IDSESSION;
            var urlBase = '/Transactions/LTE/ExternalInternalTransfer';

            $.app.ajax({
                type: 'POST',
                dataType: 'json',
                contentType: "application/json; charset=utf-8",
                data: "{strIdSession:'" + strIdSession + "' , strDepartments:'" + strDepartments + "'}",
                url: urlBase + '/GetProvinces',
                success: function (response) {
                    that.createDropdownProvinces(response);
                }
            });
        },

        createDropdownProvinces: function (response) {
            var that = this,
               controls = that.getControls();
            var i = 1;
            controls.ddlNoteProvince.append($('<option>', { value: '', html: 'Seleccionar' }));

            if (response.data != null) {
                $.each(response.data.ListGeneric, function (index, value) {
                    controls.ddlNoteProvince.append($('<option>', { value: value.Code, html: value.Description }));
                });
            }
        },

        GetDistricts: function (strDepartments, strProvinces) {
            var that = this;
            var strIdSession = Session.IDSESSION;
            var urlBase = '/Transactions/LTE/ExternalInternalTransfer';
            that.f_ClearDistrito();

            $.app.ajax({
                type: 'POST',
                dataType: 'json',
                contentType: "application/json; charset=utf-8",
                data: "{strIdSession:'" + strIdSession + "' , strDepartments:'" + strDepartments + "' , strProvinces:'" + strProvinces + "'}",
                url: urlBase + '/GetDistricts',
                success: function (response) {
                    that.createDropdownDistricts(response);
                }
            });
        },

        createDropdownDistricts: function (response) {
            var that = this,
               controls = that.getControls();
            var i = 1;
            controls.ddlNoteDistrict.append($('<option>', { value: '', html: 'Seleccionar' }));

            if (response.data != null) {
                $.each(response.data.ListGeneric, function (index, value) {
                    controls.ddlNoteDistrict.append($('<option>', { value: value.Code, html: value.Description }));
                });
            }
        },

        getReasonSot: function () {
            var that = this, objReasonSot = {};
            objReasonSot.strIdSession = Session.IDSESSION;
            var urlBase = '/Transactions/LTE/ExternalInternalTransfer';

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objReasonSot), //----------------------
                url: urlBase + '/GetMotiveSot',
                success: function (response) {
                    that.createDropdownReasonSot(response);
                }
            });
        },

        createDropdownReasonSot: function (response) {
            var that = this,
                controls = that.getControls();
            var i = 1;
            controls.ddlReasonSot.append($('<option>', { value: '', html: 'Seleccionar' }));

            if (response.data != null) {
                $.each(response.data.ListGeneric, function (index, value) {
                    if (value.Code == SessionTransf.hdnddlSOT) {
                        controls.ddlReasonSot.append($('<option>', { value: value.Code, html: value.Description }).attr('selected', 'selected'));
                    }

                    controls.ddlReasonSot.append($('<option>', { value: value.Code, html: value.Description }));

                });
            }
        },

        GetZoneType: function () {
            var that = this,
            objGetZoneType = {};
            objGetZoneType.strIdSession = Session.IDSESSION;
            var urlBase = '/Transactions/LTE/ExternalInternalTransfer';

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objGetZoneType),
                url: urlBase + '/GetZoneTypes',
                success: function (response) {
                    that.createGetZoneType(response);
                }
            });
        },

        createGetZoneType: function (response) {
            var that = this,
                controls = that.getControls();
            var i = 1;
            controls.ddlNoteZote.append($('<option>', { value: '', html: 'Seleccionar' }));

            if (response.data != null) {
                $.each(response.data.ListGeneric, function (index, value) {
                    controls.ddlNoteZote.append($('<option>', { value: value.Code, html: value.Description }));

                });
            }
        },

        getWorkSubType: function (pstrWorkType) {
            var that = this;
            var strIdSession = Session.IDSESSION;
            var strContractID = SessionTransac.SessionParams.DATACUSTOMER.ContractID;
            var urlBase = '/Transactions/LTE/ExternalInternalTransfer';

            $.app.ajax({
                type: 'POST',
                dataType: 'json',
                contentType: "application/json; charset=utf-8",
                data: "{strIdSession:'" + strIdSession + "',strCodTypeWork:'" + pstrWorkType + "',strContractID:'" + strContractID + "'}",
                url: urlBase + '/GetWorkSubType',
                success: function (response) {
                    that.createDropdownWorkSubType(response);
                }
            });
        },

        createDropdownWorkSubType: function (response) {
            var that = this,
                controls = that.getControls();
            var i = 1;
            controls.ddlTypeSubWork.html('');
            controls.ddlTypeSubWork.append($('<option>', { value: '', html: 'Seleccionar' }));

            if (response.data != null) {
                $.each(response.data.ListGeneric, function (index, value) {
                    var codTipoSubTrabajo = value.Code.split("|");
                    if (response.typeValidate.COD_SP == "0" && codTipoSubTrabajo[0] == response.typeValidate.COD_SUBTIPO_ORDEN) {
                        controls.ddlTypeSubWork.append($('<option>', { value: value.Code, html: value.Description, typeservice: value.Code2, selected: true }));
                        controls.ddlTypeSubWork.attr('disabled', true);
                    }
                    else {
                        controls.ddlTypeSubWork.append($('<option>', { value: value.Code, html: value.Description, typeservice: value.Code2 }));
                    }
                });
            }
        },

        getWorkType: function (strTransacType) {
            var that = this;
            var strIdSession = Session.IDSESSION;
            var urlBase = '/Transactions/LTE/ExternalInternalTransfer';

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: "{strIdSession:'" + strIdSession + "',strTransacType:'" + strTransacType + "'}",
                url: urlBase + '/GetWorkType',
                success: function (response) {
                    that.createDropdownWorkType(response, strTransacType);
                }
            });
        },

        createDropdownWorkType: function (response, strTransacType) {
            var that = this,
                controls = that.getControls();
            var i = 1;

            controls.ddlTypeSubWork.empty();
            if (response.data != null) {
                $.each(response.data.ListGeneric, function (index, value) {
                    controls.ddlTypeWork.append($('<option>', { value: value.Code, html: value.Description }));
                });

                if(Session.codOpcion == SessionTransf.codOpcionTI)
                    $("#ddlTypeWork").val(SessionTransf.hdnIDTrabTI);
                else
                    $("#ddlTypeWork").val(SessionTransf.hdnIDTrabTE);
            }

            if (strTransacType == '4')
                that.F_ValidateETA(strTransacType);
        },

        getCACDAC: function () {
            var that = this,
                controls = that.getControls(),
                objCacDacType = {};
            objCacDacType.strIdSession = Session.IDSESSION;

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objCacDacType),
                url: '/Transactions/CommonServices/GetCacDacType',
                success: function (response) {
                    that.createDropdownCACDAC(response);
                }
            });
        },

        createDropdownCACDAC: function (response) {
            var that = this,
                controls = that.getControls();
            controls.ddlCACDAC.empty();
            var i = 1;
            controls.ddlCACDAC.append($('<option>', { value: '', html: 'Seleccionar' }));

            if (response.data != null) {
                $.each(response.data.CacDacTypes, function (index, value) {
                    controls.ddlCACDAC.append($('<option>', { value: value.Code, html: value.Description }));
                });
            }
        },

        //VALIDATION--------------------

        f_ValidateEmail: function () {
            if ($('#chkSendMail').is(':checked')) {
                var regExp = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
                if ($('#txtSendMail').val() != '') {
                    if (regExp.test($('#txtSendMail').val()))
                        return true;
                    else
                        alert(SessionTransf.strMensajeEmail, "Alerta");
                    return false;
                }
                else {
                    alert(SessionTransf.hdnMessageSendMail, "Alerta");
                    return false;
                }
            } else {
                return true;
            }
        },

        f_ValidateSoloNumeros: function (e) {
            if (e.keyCode == 46 || e.keyCode == 8 || e.keyCode == 9 || e.keyCode == 27 || e.keyCode == 13 ||
           (e.keyCode == 65 && e.ctrlKey === true) || (e.keyCode >= 35 && e.keyCode <= 39))
                return;
            else
                if (e.shiftKey || (e.keyCode < 48 || e.keyCode > 57) && (e.keyCode < 96 || e.keyCode > 105) || e.KeyCode == 187 || e.KeyCode == 186 || e.KeyCode == 192)
                    e.preventDefault();
        },

        f_ValidateCampoNumeroDireccion: function () {
            var num = $('#txtNumber');
            var chk = $('#chkSN');

            if (chk.is(':checked')) {
                return true;
            } else {

                if ($.trim(num.val()) == '') {
                    alert("Ingrese número dirección.", "Alerta");
                    num.focus();
                    return false;
                } else {
                    return true;
                }
            }
        },

        f_ValidateLongitudDirecciones: function () {
            var that = this;
            var controls = that.getControls();

            if (parseInt(controls.txtConDir.val(), 0) > 40 || parseInt(controls.txtConNotDir.val(), 0) > 40) {
                alert("Se exedió la cantidad máxima de caracteres.","Alerta");
                return false;
            }
            return true;
        },

        f_MostrarMensajeError: function (o) {

            alert(o.msn, "Alerta");
            o.obj.addClass('error');
            o.obj.focus();
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

        f_Prev: function () {
            var that = this,
                controls = that.getControls();

            switch (SessionTransf.flagvalidation) {
                case null:

                    break;

                case '1':
                    SessionTransf.flagvalidation = null;
                    break;
                case '2':
                    SessionTransf.flagvalidation = '1';
                    break;

                case '3':
                    SessionTransf.flagvalidation = '2';
                    break;
            }

        },

        f_NextIgv: function () {
            var that = this,
                controls = that.getControls();
            if (that.strIGV == null || that.strIGV == '') {
                alert(SessionTransf.hdnMessageErrorIgv, "Alerta");
                controls.lblErrorMessage.html(SessionTransf.hdnMessageErrorIgv);
                controls.divErrorAlert.show();
            }
        },

        f_Next: function () {
            var that = this,
                controls = that.getControls();
        },

        //f_tab: function () { },

        strUrlLogo: window.location.protocol + '//' + window.location.host + '/Images/loading2.gif',
        strIGV: '',
        strClaseCode: '',
        strSubClaseCode: '',
        strTipo: '',
        strClase: '',
        strSubClase: ''//,

/*         f_SeleccionarCACDAC: function () {
            var that = this, controls = that.getControls();;
            //summary
            if (controls.ddlCACDAC.val() != "-1") {
                Smmry.set('PuntVenta', $('#ddlCACDAC option:selected').html());
                Smmry.set('FechCompromiso', controls.txtFProgramacion.val());
                Smmry.set('Horario', $("#cboFranjaHoraria option:selected").html() == 'Seleccionar' ? '' : $("#cboFranjaHoraria option:selected").html());
                //sumary
                Smmry.set('SubTipTrabajo', $("#ddlTypeSubWork option:selected").html() == 'Seleccionar' ? '' : $("#ddlTypeSubWork option:selected").html());
                Smmry.set('TipTrabajo', $("#ddlTypeWork option:selected").html() == 'Seleccionar' ? '' : $("#ddlTypeWork option:selected").html());
                Smmry.set('MotiveSoft', $('#ddlReasonSot option:selected').html());

            } else {
                summary.set('PuntVenta', "Sin seleccionar");
                Smmry.set('FechCompromiso', controls.txtFProgramacion.val());
                Smmry.set('Horario', $("#cboFranjaHoraria option:selected").html() == 'Seleccionar' ? '' : $("#cboFranjaHoraria option:selected").html());
            }

        },

        f_Seleccionar_NombreCalle: function () {
            var that = this, controls = that.getControls();
            //summary
            if (controls.txtNameStreet.val() != "") {
                Smmry.set('ddlNombreCalle', controls.txtNameStreet.val());
            }
            else {

                Smmry.set('ddlNombreCalle', "sin llenar");
            }

        },

        f_SeleccionarNumero: function () {
            var that = this, controls = that.getControls();
            //summary
            if (controls.txtNumber.val() != "") {
                Smmry.set('txtNumero', controls.txtNumber.val());

            } else {
                Smmry.set('txtNumero', "no se ingreso numero");

            }

        },

        f_Seleccionar_MZ_BLOCK_EDI: function () {
            var that = this, controls = that.getControls();
            //sumary
            if (controls.txtNumberBlock.val() != "") {
                Smmry.set('NroMz', controls.txtNumberBlock.val());
            }
            else {
                Smmry.set('NroMz', controls.txtNumberBlock.val());

            }

        },

        f_Seleccionar_Numero_Dpo_Int: function () {
            var that = this, controls = that.getControls();
            //sumary
            if (controls.ddlNumberDepartment.val() != "") {
                Smmry.set('NumeroDpto', controls.ddlNumberDepartment.val());
            } else {
                Smmry.set('NumeroDpto', "no se ingreso numero departamento");
            }
        },

        f_Seleccionar_NombreUrbanization: function () {
            var that = this, controls = that.getControls();
            //sumary
            if (controls.txtNoteUrbanization.val() != "") {
                Smmry.set('NombreUrb', controls.txtNoteUrbanization.val());
            } else {
                Smmry.set('NombreUrb', "no se ingreso nombre de urbanizacion");
            }
        },

        f_SeleccionaZona: function () {
            var that = this, controls = that.getControls();
            //sumary
            if (controls.txtNoteNameZote.val() != "") {
                Smmry.set('NombreZona', controls.txtNoteNameZote.val());
            }
            else {
                Smmry.set('NombreZona', "no se ingreso nombre zona");
            }

        },

        f_SeleccionarReferencia: function () {
            var that = this, controls = that.getControls();
            //sumary
            if (controls.txtNoteReference.val() != "") {
                Smmry.set('Referencia', controls.txtNoteReference.val());
            }
            else {
                Smmry.set('Referencia', "No se ingreso referencia.");
            }

        },

        f_SeleccionarCodigoPostal: function () {
            var that = this, controls = that.getControls();
            //sumary
            if (controls.txtNoteCodePostal.val() != "") {
                Smmry.set('CodPostal', controls.txtNoteCodePostal.val());
            } else {
                Smmry.set('CodPostal', "no se ingreso codigo postal");
            }

        },

        f_SeleccionarLote: function () {
            var that = this, controls = that.getControls();
            //sumary
            if (controls.txtLot.val() != "") {
                Smmry.set('txtLote', controls.txtLot.val());
            } else {
                Smmry.set('txtLote', "selecciona un lote");
            }
        },*/
    };

    /*function ConsultationCoverage(pag, controls) {
        $("#idfrCobertura").attr("src", SessionTransf.ConsultationCoverageURL);
        $("#pnlValidarCoberturaURL").dialog({
            title: SessionTransf.ConsultationCoverageTitle,
            height: 600,
            width: 900,
            draggable: false,
            modal: true,
            open: function () {
                //
            }
        });
    };*/

    $("#ddlHorarioUsrCtr").change(function () {
        if ($("#ddlHorarioUsrCtr option:selected").val().indexOf("|") != -1) {
            alert('$("#hdnMen8").val()', "Alerta");
            $("#ddlHorarioUsrCtr").val("-1");
            return false;
        }

        if (SessionTransf.hdnTipoTrabCU.val().indexOf(".|") == -1) {
            SessionTransf.hdnValidado = "1";
        } else {
            SessionTransf.hdnValidado = "0";
        }

        SessionTransf.hdnFranjaHorariaCU.val($("#ddlHorarioUsrCtr option:selected").html());
        SessionTransf.hdnCodigoFranja.val($("#ddlHorarioUsrCtr").val());// ???
    });

    $("#txtFechaProgramacionUsrCtr").keydown(function () {
        return false;
    });

    $("#txtFechaProgramacionUsrCtr").keypress(function (e) {
        return false;
    });


    $.fn.LTEExternalInternalTransfer = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('LTEExternalInternalTransfer'),
                options = $.extend({}, $.fn.LTEExternalInternalTransfer.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('LTEExternalInternalTransfer', data);
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

    $.fn.LTEExternalInternalTransfer.defaults = {
    }

    $('#divBody').LTEExternalInternalTransfer();

})(jQuery);

