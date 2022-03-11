var SessionPMHFC = {};
var STR_EMPTY = '';
var bVisitaTecnica = false;


$(".collapse").collapse();

function CloseValidation(obj, pag, controls) {
    var mensaje;
    if (obj.hidAccion === 'G') {// Correcto

    } else { //if (obj.hidAccion == 'F') {
        mensaje = 'La validación del usuario ingresado es incorrecto o no tiene permisos para continuar con el proceso, por favor verifiquelo.';
        alert(mensaje, "Alerta");
        $("#txtUsernameAuth").val("");
        $("#txtPasswordAuth").val("");
        $('#ddlFranjaHoraria option[value="-1"]').prop('selected', true);
        return;
    }
};

function CreateListParamConstancyPDF() {

    var list = [];
    var todaydate = new Date();
    list.push("MIGRACION_PLAN_FIJOS");//0 FORMATO_TRANSACCION
    list.push($("#cboCacDac option:selected").text());//01 CENTRO_ATENCION_AREA
    list.push(SessionPMHFC.DATACUSTOMER.LegalAgent);//02 REPRES_LEGAL
    list.push(SessionPMHFC.DATACUSTOMER.FullName);//03 TITULAR_CLIENTE
    list.push(SessionPMHFC.DATACUSTOMER.DocumentType);//04 TIPO_DOC_IDENTIDAD
    list.push(SessionPMHFC.DATASERVICE.Plan);//05 PLAN_ACTUAL
    list.push(SessionPMHFC.DATACUSTOMER.BillingCycle)//06 CICLO_FACTURACION
    list.push(moment(new Date()).format('DD/MM/YYYY'));//07 FECHA_TRANSACCION_PROGRAM dia de hoy
    list.push("{interaccion}");//08 CASO_INTER
    list.push(SessionPMHFC.DATACUSTOMER.ContractID); //09 NRO_CONTRATO
    list.push(SessionPMHFC.DATACUSTOMER.DocumentNumber);//10 NRO_DOC_IDENTIDAD
    list.push($("#lblPlanNuevo").text());//11 NUEVO_PLAN
    list.push($("#lblSolucionNueva").text());//12 SOLUCION
    list.push($("#lblCargoFijoTotalPlanCIGV").text());//13 CF_TOTAL_NUEVO
    list.push($("#txtFProgramacion").val());//14 FECHA_VISITA
    list.push($("#txtTotalPenalidad").val());//15 PENALIDAD
    list.push("{nrosot}");//16 SOT
    list.push('');//17 NOMBRE_SERVICIO
    list.push('');//18 TIPO_SERVICIO
    list.push('');//19 GRUPO_SERVICIO
    list.push('');//20 CF_TOTAL_IGV
    list.push($("#chkPresuscrito").prop("checked") ? "1" : "0");//21 PRESUSCRITO
    list.push($('#txtNroCarta').val());//22 NRO_CARTA
    list.push($("#sltOperator option:selected").text());//23 NOM_OPERADOR
    list.push($("#chkPublicar").prop("checked") ? "1" : "0");//24 PUB_NT_PA
    return list;
}

function ValidateEmail(email) {


    var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;

    if (email.length == 0) {
        $("#ErrorMessageEmail").text("Ingrese una direccion de correo valida.");
        $('#txtEmail').closest(".form-group").addClass("has-error");
        return false;
    }
    if (filter.test(email))
        return true;
    else {
        $("#ErrorMessageEmail").text("Ingrese una direccion de correo valida.");
        $('#txtEmail').closest(".form-group").addClass("has-error");
    }
    return false;
}

(function ($, undefined) {

    function validationsSteps(stepName, fn) {

        var response;
        if (stepName == 'tabPlanYServiciosAdicionales') {

            Form.prototype.ValidateFirtsStep(function (response) {

                if (response) {
                    fn(true);
                }
                else fn(false);
            });
        } else {
            if (stepName == 'tabDatosTecnicos') {
                Form.prototype.ValidateFourthStep(function (response) {
                    if (response) fn(true); else fn(false);
                });
            } else {
                if (stepName == 'tabPenalidades') {
                    Form.prototype.ValidatePenaltyStep(function (response) {
                        if (response) fn(true); else fn(false);
                        //$.unblockUI();
                    });
                } else {
                    if (stepName == "tabServiciosComplementarios") {
                        Form.prototype.ValidateComplementaryServicesStep(function (response) {
                            if (response) fn(true); else fn(false);
                        });

                    } else {
                        if (stepName == "tabAgendamiento") {
                            Form.prototype.ValidateScheduling(function (response) {
                                if (response) fn(true); else fn(false);
                            });

                        } else {
                            fn(true);
                        }
                    }
                }
            }
        }
        return response;
    }

    var Smmry = new Summary('transfer');

    var Form = function ($element, options) {
        $.extend(this, $.fn.HfcPlanMigration.defaults, $element.data(), typeof options === 'object' && options);
        this.setControles({
            form: $element
          , btnBuscar: $('#btnBuscar', $element)
          , btnAgregarPlan: $('#btnAgregarPlan', $element)
            //, btnAgrSerAdCable: $('#btnAgrSerAdCable', $element)
            //, btnAgrSerAdPhone: $("#btnAgrSerAdPhone", $element)
            //, btnAgrSerAdInternet: $("#btnAgrSerAdInternet", $element)
          , btnBuscarV: $("#btnBuscarV", $element)
            //, btnAgServAdTelefono: $("#btnAgServAdTelefono", $element)
            //, btnAgServAdInternet: $("#btnAgServAdInternet", $element)
            //, btnAgServAdCable: $("#btnAgServAdCable", $element)
            //, btnEliminarServicio: $("#btnEliminarServicio", $element)
          , btnAgregarEquipos: $("#btnAgregarEquipos", $element)
          , btn1stClose: $('#btn1stClose', $element)
          , btn2ndClose: $('#btn2ndClose', $element)
          , btn3rdClose: $('#btn3rdClose', $element)
          , btn4thClose: $('#btn4thClose', $element)
          , btn5thClose: $('#btn5thClose', $element)
          , btn6thClose: $('#btn6thClose', $element)
          , btnClose: $('#btnClose', $element)
          , txtTopConsume: $('#txtTopConsume', $element)
          , dTextCorreo: $('#dTextCorreo', $element)
          , txtHFC_SendforEmail: $('#txtHFC_SendforEmail', $element)
          , sltWorkType: $("#sltWorkType", $element)
          , sltSubWorkType: $("#sltSubWorkType", $element)
          , txtFProgramacion: $("#txtFProgramacion", $element)
            /*cabecera*/
          , lblPost_Contrato: $("#lblPost_Contrato", $element)
          , lblPost_CustomerId: $("#lblPost_CustomerId", $element)
          , lblPost_TipoCliente: $("#lblPost_TipoCliente", $element)
          , lblPost_Contacto: $("#lblPost_Contacto", $element)
            /*Datos del Cliente*/
          , lblCliente: $("#lblCliente", $element)
          , lblNumDoc: $("#lblNumDoc", $element)
          , lblPlaAct: $("#lblPlaAct", $element)
          , lblCampania: $("#lblCampania", $element)
          , trGrupoCampania: $("#trGrupoCampania", $element)
          , lblFecAct: $("#lblFecAct", $element)
          , lblCicfac: $("#lblCicfac", $element)
          , lblTipAcu: $("#lblTipAcu", $element)
          , lblLimCre: $("#lblLimCre", $element)
          , lblEstSer: $("#lblEstSer", $element)
          , lblRepLeg: $("#lblRepLeg", $element)
          , lblFecVen: $("#lblFecVen", $element)
          , lblHUB: $("#lblHUB", $element)
          , lblCintillo: $("#lblCintillo", $element)
          , lblCMTS: $("#lblCMTS", $element)
            /*Dirección de Instalación del Cliente:*/
            , lblAddress: $('#lblAddress', $element)
            , lblReferencia: $('#lblReferencia', $element)
            , lblUrbanization: $('#lblUrbanization', $element)
            , lblCountry: $('#lblCountry', $element)
            , lblDepartment: $('#lblDepartment', $element)
            , lblProvince: $('#lblProvince', $element)
            , lblDistrict: $('#lblDistrict', $element)
            , lblPlaneCode: $('#lblPlaneCode', $element)
            , lblUbigeoCode: $('#lblUbigeoCode', $element)
          , lblMontoBase: $("#lblMontoBase", $element)
          , lblTitle: $('#lblTitle', $element)
          , divRules: $('#divRules', $element)
          , btnHabilitarResumenCargos: $('#btnHabilitarResumenCargos', $element)
          , cboCacDac: $('#cboCacDac', $element)
          , btnGuardar: $('#btnGuardar', $element)
          , btnConstancy: $('#btnConstancy', $element)
            /*Datos de Cargos Actuales*/
          , lblMontoActualBase: $('#lblMontoActualBase', $element)
          , lblMontoActualAdicional: $('#lblMontoActualAdicional', $element)
          , lblCargoFijoTotalPlanSIGVActual: $('#lblCargoFijoTotalPlanSIGVActual', $element)
          , lblCargoFijoTotalPlanCIGVActual: $('#lblCargoFijoTotalPlanCIGVActual', $element)
          , lblCantidadActual: $('#lblCantidadActual', $element)
          , collapseServices: $('#collapseServices', $element)
          , spanCollapseServices: $('#spanCollapseServices', $element)
          , collapseEquipments: $('#collapseEquipments', $element)
          , spanCollapseEquipments: $('#spanCollapseEquipments', $element)
          , txtNotas: $('#txtNotas', $element)
          , txtNroCarta: $('#txtNroCarta', $element)
          , sltOperator: $('#sltOperator', $element)
          , txtReintegro: $('#txtReintegro', $element)
          , txtMontoFideliza: $('#txtMontoFideliza', $element)
          , txtTotalPenalidad: $('#txtTotalPenalidad', $element)
          , chkSentEmail: $('#chkSentEmail', $element)
          , chkServAdCable: $('#chkServAdCable', $element)
          , chkServAdInternet: $("#chkServAdInternet", $element)
          , chkServAdTelefono: $("#chkServAdTelefono", $element)
          , chkPresuscrito: $("#chkPresuscrito", $element)
          , chkPublicar: $('#chkPublicar', $element)
          , chkFideliza: $('#chkFideliza', $element)
          , chkOCC: $('#chkOCC', $element)
          , ddlFranjaHoraria: $('#ddlFranjaHoraria', $element)
          , chkEmail: $('#chkEmail', $element)
          , txtEmail: $('#txtEmail', $element)
          , btn_selServ: $('#btn_selServ', $element)
          , btn_selTeam: $('#btn_selTeam', $element)
          , lstCurrentPlanCable: $('#lstCurrentPlanCable', $element)
          , lstCurrentPlanInternet: $('#lstCurrentPlanInternet', $element)
          , lstCurrentPlanTelephony: $('#lstCurrentPlanTelephony', $element)
          , lstSelectPlanCable: $('#lstSelectPlanCable', $element)
          , lstSelectPlanInternet: $('#lstSelectPlanInternet', $element)
          , lstSelectPlanTelephony: $('#lstSelectPlanTelephony', $element)
          , lstCurrentEquipCable: $('#lstCurrentEquipCable', $element)
          , lstCurrentEquipInternet: $('#lstCurrentEquipInternet', $element)
          , lstCurrentEquipTelephony: $('#lstCurrentEquipTelephony', $element)
          , lstSelectEquipCable: $('#lstSelectEquipCable', $element)
          , lstSelectEquipInternet: $('#lstSelectEquipInternet', $element)
          , lstSelectEquipTelephony: $('#lstSelectEquipTelephony', $element)
          , divEmail: $('#divEmail', $element)
          , CableEquipmentQty: $('#CableEquipmentQty', $element)
          , InternetEquipmentQty: $('#InternetEquipmentQty', $element)
          , PhoneEquipmentQty: $('#PhoneEquipmentQty', $element)
          , DecosQuantity: $('#DecosQuantity', $element)
          , lstResumenPlanActual: $('#lstResumenPlanActual', $element)
          , lstResumenPlanNuevo: $('#lstResumenPlanNuevo', $element)
          , lstCurrentPlanAdicionalCable: $('#lstCurrentPlanAdicionalCable', $element)
          , lstCurrentPlanAdicionalInternet: $('#lstCurrentPlanAdicionalInternet', $element)
          , lstCurrentPlanAdicionalTelephony: $('#lstCurrentPlanAdicionalTelephony', $element)

          , ModalLoading: $('#ModalLoading',$element)
            //  //Nuevo Plan Inicializando Campania
          , lblCampaniaColaborador: $("#lblCampaniaColaborador", $element)
          , trSelecCampaniaColab: $("#trSelecCampaniaColab", $element)
        });

    }

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this;
            var controls = this.getControls();
            Session.intCampania = 0;
            controls.chkSentEmail.addEvent(that, 'change', that.ocultarCorreo);
            controls.btnAgregarPlan.addEvent(that, 'click', that.btnAgregarPlan_Click_Pop);
            controls.chkServAdCable.addEvent(that, 'click', that.chkServAdCable_Click);
            //controls.btnAgrSerAdCable.addEvent(that, 'click', that.btnAgrSerAdCable_Click);
            //controls.btnAgrSerAdInternet.addEvent(that, 'click', that.btnAgrSerAdInternet_Click);
            //controls.btnAgrSerAdPhone.addEvent(that, 'click', that.btnAgrSerAdPhone_Click);
            controls.btnBuscarV.addEvent(that, 'click', that.btnBuscarV_Click);
            //controls.btnAgServAdTelefono.addEvent(that, 'click', that.btnAgServAdTelefono_Click);
            //controls.btnAgServAdCable.addEvent(that, 'click', that.btnAgServAdCable_Click);
            //controls.btnAgServAdInternet.addEvent(that, 'click', that.btnAgServAdInternet_Click);
            //controls.btnEliminarServicio.addEvent(that, 'click', that.btnEliminarServicio_Click);
            controls.sltWorkType.addEvent(that, 'change', that.f_sltWorkType_change);
            controls.sltSubWorkType.addEvent(that, 'change', that.f_sltSubWorkType_change);
            controls.btnAgregarEquipos.addEvent(that, 'click', that.btnAgregarEquipos_Click);
            controls.chkPresuscrito.addEvent(that, 'click', that.chkPresuscrito_Click);
            controls.btnGuardar.addEvent(that, 'click', that.btnGuardar_Click);
            controls.collapseServices.addEvent(that, 'click', that.collapseServices_Click);
            controls.collapseEquipments.addEvent(that, 'click', that.collapseEquipments_Click);
            controls.btn_selServ.addEvent(that, 'click', that.btn_selServ_Click);
            controls.btnConstancy.addEvent(that, 'click', that.f_Constancia);
            controls.btn_selTeam.addEvent(that, 'click', that.f_ListaEquipos);
            controls.sltOperator.addEvent(that, 'click', that.f_SeleccionOperatorclick);
            controls.sltOperator.change(function () { that.f_GetOperator(); });
            controls.cboCacDac.change(function () { that.GetCboCacDac(); });
            controls.chkPublicar.addEvent(that, 'click', that.chkPublicar_Click);
            controls.ddlFranjaHoraria.addEvent(that, 'click', that.ddlFranjaHorariachange);
            controls.btn1stClose.addEvent(that, 'click', that.btnClose_Click);
            controls.btn2ndClose.addEvent(that, 'click', that.btnClose_Click);
            controls.btn3rdClose.addEvent(that, 'click', that.btnClose_Click);
            controls.btn4thClose.addEvent(that, 'click', that.btnClose_Click);
            controls.btn5thClose.addEvent(that, 'click', that.btnClose_Click);
            controls.btn6thClose.addEvent(that, 'click', that.btnClose_Click);
            document.addEventListener('keyup', that.shortCutStep, false);
            controls.chkOCC.addEvent(that, 'click', that.chkOCC_Click);
            controls.chkEmail.addEvent(that, 'click', that.chkEmail_Click);

            $('.next-trans').on('click', function (e) {
                that.shortCutStep(e);
            });
            $('#txtFProgramacion').attr('tabindex', '32')
            $('#ddlFranjaHoraria').attr('tabindex', '33')


            //controls.chkFideliza.addEvent(that, 'click', that.chkFideliza_Click);  Aun no existe la funcionalidad para Penalidades!
            that.maximizarWindow();
            that.windowAutoSize();
            SessionPMHFC.FECHAACTUALSERVIDOR = CURRENTSERVERDATE;
            SessionPMHFC.HdnRequestActId = "";
            SessionPMHFC.HdnListaFTipoEquipos = '';
            SessionPMHFC.HdnListaFTMCode = '';
            SessionPMHFC.HdnListaFCodServ = '';
            SessionPMHFC.HdnListaFSNCode = '';
            SessionPMHFC.HdnListaFSPCode = '';
            SessionPMHFC.HdnListaCargosFijos = '';
            SessionPMHFC.strVisitTecAnotaciones = '';
            SessionPMHFC.strVisitTecSubTipo = '';

            that.Loading();
            that.loadSessionData();
            controls.trSelecCampaniaColab.attr("style", "display:none");
        },
        render: function () {
            var that = this;
            var controls = that.getControls();

            that.loadCustomerData();
            that.GetSessionParameters();
            that.getCurrentPlanServicesGroups();
            that.getServicesGroups();
            that.f_IniTablePrincipales();
            that.getIGV();
            var fechaServidor = new Date(SessionPMHFC.FECHAACTUALSERVIDOR);
            //$("#txtFProgramacion").val([that.pad(fechaServidor.getDate()), that.pad(fechaServidor.getMonth() + 1), fechaServidor.getFullYear()].join("/"));
            $("#btnConstancy").prop("disabled", true);
            $("#DivAgendamiento").find('*').attr('disabled', true);
            $('#btnConstancia').prop('disabled', true);
            $("#DivPenalidad").find('*').prop('disabled', true);
            $("#txtTotalPenalidad").prop('disabled', true);
            $('.next-two-step').on('click', function (e) {
                that.navigateTabsTwoStep(e);
            });
            controls.txtEmail.val(SessionPMHFC.DATACUSTOMER.Email);
            controls.divEmail.hide();
            that.f_habilitaDeshabilitaDertalleTelefonia(false);
            that.getTypification();
            that.getCurrentPlanServices();
            that.getEquipmentByCurrentPlan();

            //that.f_Get_JobTypes();
            that.f_LoadJobTypes();
            that.InitCacDat();
            that.f_GetCustomerPhoneCommon();

            Smmry.set('operador', '');
            Smmry.set('publicarnumero', 'NO');
            Smmry.set('presuscrito', 'NO');
            Smmry.set('numerocarta', '');
            Smmry.set('Reintegropenalidad', '');
            Smmry.set('ckhFidelizacion', 'NO');
            Smmry.set('Total-penalidad', '');
            Smmry.set('Montofidelizacionpenalidad', '');
            Smmry.set('chkOcc', 'NO');
            Smmry.set('chkemail', 'NO');
            Smmry.set('email', '');
            Smmry.set('PuntoAtencion', '');
            Smmry.set('Notas', '');
            Smmry.set('tipodetrabajo', '');
            Smmry.set('Subtipotrabajo', '');
            Smmry.set('fechaCompromiso', '');
            Smmry.set('Horario', '');
            Smmry.set('topeconsumo', '');



        },
        Loading: function () {
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
        GetSessionParameters: function () {
            var that = this;
            var controls = that.getControls();
            var SessionTransacHFC = sessionStorage.getItem("SessionTransac");

            that.Loading();

            $.ajax({
                type: "POST",
                url: that.strUrl + '/Transactions/HFC/PlanMigration/GetSessionParameters',
                data: { strIdSession: SessionPMHFC.IDSESSION, strContrato: SessionPMHFC.DATACUSTOMER.ContractID, strCustomerId: SessionPMHFC.DATACUSTOMER.CustomerID, strData: SessionTransacHFC },

                success: function (data) {
                    if (data.listHub != null) {
                        try {
                            controls.lblHUB.html(data.listHub[0].strHubDesc);
                            controls.lblCintillo.html(data.listHub[0].strCintillo);
                            controls.lblCMTS.html(data.listHub[0].strCmts);

                        } catch (e) {

                        }
                    }
                    else {
                        controls.lblHUB.html("");
                        controls.lblCintillo.html("");
                        controls.lblCMTS.html("");
                    }
                },
                failure: function (msg) {

                    $.unblockUI();
                },
                error: function (xhr, status, error) {

                    $.unblockUI();
                },
                complete: function () {
                    $.unblockUI();

                }

            });

        },
        getCurrentPlanServicesGroups: function () {
            var that = this;
            var controls = that.getControls();
            that.Loading();
            $.ajax({
                type: "POST",
                url: that.strUrl + '/Transactions/HFC/PlanMigration/GetCurrentPlanServicesGroups',
                data: { strIdSession: SessionPMHFC.IDSESSION },
                success: function (data) {
                    SessionPMHFC.strPlanActualCable = data.data.strPlanActualCable;
                    SessionPMHFC.strPlanActualInternet = data.data.strPlanActualInternet;
                    SessionPMHFC.strPlanActualTelephony = data.data.strPlanActualTelephony;
                },
                failure: function (msg) {
                    $.unblockUI();
                },
                error: function (xhr, status, error) {
                    $.unblockUI();
                },
                complete: function () {
                    $.unblockUI();

                }
            });
        },
        getServicesGroups: function () {
            var that = this;
            var controls = that.getControls();
            that.Loading();
            $.ajax({
                type: 'POST',
                url: that.strUrl + '/Transactions/HFC/PlanMigration/GetServicesGroups',
                data: { strIdSession: SessionPMHFC.IDSESSION },
                success: function (data) {
                    SessionPMHFC.strHFCGroupCable = data.data.strHFCGroupCable;
                    SessionPMHFC.strHFCGroupInternet = data.data.strHFCGroupInternet;
                    SessionPMHFC.strHFCGroupTelephony = data.data.strHFCGroupTelephony;

                },
                failure: function (msg) {
                    $.unblockUI();
                },
                error: function (xhr, status, error) {
                    $.unblockUI();
                },
                complete: function () {
                    $.unblockUI();

                }
            });
        },
        f_GetOperator: function () {
            var that = this;
            var controls = that.getControls();
            Smmry.set('topeconsumo', $("#txtTopConsume").val());
            if (controls.sltOperator.val() != "") {
                $("#sltOperator").closest(".form-group").removeClass("has-error");
                $("#ErrorMessageDllOperador").text("");

                Smmry.set('operador', $("#sltOperator option:selected").text());

            }
        },
        GetCboCacDac: function () {
            var that = this;
            var controls = that.getControls();
            Smmry.set('Reintegropenalidad', $("#txtReintegro").val());
            Smmry.set('Montofidelizacionpenalidad', $("#txtMontoFideliza").val());
            Smmry.set('Total-penalidad', $("#txtTotalPenalidad").val());
            Smmry.set('chkOcc', $('#chkOCC').is(':checked') ? 'SI' : 'NO');
            Smmry.set('ckhFidelizacion', $("#chkFideliza").prop("checked") ? "SI" : "NO");
            Smmry.set('presuscrito', $("#chkPresuscrito").prop("checked") ? "SI" : "NO");
            Smmry.set('publicarnumero', $("#chkPublicar").prop("checked") ? "SI" : "NO");
            Smmry.set('chkemail', $('#chkEmail').is(':checked') ? 'SI' : 'NO')

            if (controls.cboCacDac.val() != "") {
                $("#cboCacDac").closest(".form-group").removeClass("has-error");
                $("#ErrorMessageCacDac").text("");
                Smmry.set('PuntoAtencion', $("#cboCacDac option:selected").text());

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
        UpdateTotalServicesList: function () {
            var that = this;
            that.listServicesByPlan.length = 0;

            that.listServicesByPlan = that.listCoreServices;//Core and Core-Aditional Services
            that.listServicesByPlan = that.listServicesByPlan.concat(that.listAditionalServices);//Aditional Services 
            that.listServicesByPlan = that.listServicesByPlan.concat(that.listAditionalRentServices);//Aditional Services by Rent


            SessionPMHFC.ListEquipments = that.listCoreServices;
            SessionPMHFC.ListEquipmentsAS = that.listAditionalServices;
            SessionPMHFC.ListEquipmentsASR = that.listAditionalRentServices;
        },
        ValidateFirtsStep: function (fn) {
            var that = this,
                controls = that.getControls();
            that.UpdateTotalServicesList();

            $("#txtNroCarta").val("");
            $("#txtNroCarta").prop("disabled", true);
            Smmry.set('numerocarta', "");
            $("#chkPresuscrito").prop("checked", true);


            if (that.listServicesByPlan.length == 0) {
                alert("Tiene que seleccionar un nuevo plan.", "Alerta");
                fn(false);
                return false;
            }

            $.blockUI({
                message: '<div align="center"><img src="' + that.strUrlLogo + '" width="25" height="25" /> Cargando ... </div>',
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

            var data = {
                strIdSession: SessionPMHFC.IDSESSION,
                strIdContract: SessionPMHFC.DATACUSTOMER.ContractID,
                strCustomerId: SessionPMHFC.DATACUSTOMER.CustomerID,
                strCodPlanSisact: that.listServicesByPlan[0].CodPlanSisact,
                strTmCode: that.listServicesByPlan[0].Tmcode,
                listEquipments: SessionPMHFC.ListEquipments,
                listEquipmentsBase: SessionPMHFC.ListEquipmentsBase,
                listEquipmentsAS: SessionPMHFC.ListEquipmentsAS,
                listEquipmentsASR: SessionPMHFC.ListEquipmentsASR
                //listEquipmentsCTV:SessionPMHFC.ListEquipmentsCTV, 
                //listEquipmentsINT:SessionPMHFC.ListEquipmentsINT, 
                //listEquipmentsTLF:SessionPMHFC.ListEquipmentsTLF, 
            };
            $("#txtNotas").empty();
            $.ajax({
                type: "POST",
                url: that.strUrl + "/Transactions/HFC/PlanMigration/GetTechnicalVisitResult",
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                data: JSON.stringify(data),
                success: function (data) {
                    fn(true);
                    $("#btnGuardar").prop("disabled", false);
                    $("#sltSubWorkType").empty();
                    that.f_LoadSelectControl($("#sltSubWorkType"), Session.vListSubType, 'typeservice');
                    if (data.data.Anerror < 0) {
                        if (data.data.flag == 1) {
                            SessionPMHFC.CODMOTOT = data.data.Codmot;
                            SessionPMHFC.strVisitTecAnotaciones = data.data.Anotaciones;
                            SessionPMHFC.strVisitTecSubTipo = data.data.Subtipo;
                            $("#txtNotas").text(data.data.Anotaciones);
                        }
                        else {
                            $("#btnGuardar").prop("disabled", true);
                            alert("Error al validar visita técnica.", "Alerta");
                        }
                    } else {
                        if (data.data.Codmot != null && data.data.Codmot != "") {
                            if (data.data.Flag == "1") {
                                SessionPMHFC.EscenarioMigracion = "Con visita";

                                $("#txtFProgramacion").prop("disabled", false);
                                $("#ddlFranjaHoraria").prop("disabled", false);
                                $("#txtNotas").text(data.data.Anotaciones);
                                bVisitaTecnica = true;
                                alert("Se hará visita técnica.", "Informativo");

                                that.f_GetValidateETA();
                                SessionPMHFC.strVisitTecSubTipo = data.data.Subtipo;
                                that.f_ApplyTechnicalVisit(SessionPMHFC.strVisitTecSubTipo);
                            }
                            else {
                                SessionPMHFC.EscenarioMigracion = "Sin visita";
                                bVisitaTecnica = false;
                                $("#sltSubWorkType").prop("disabled", true);
                                $("#txtFProgramacion").prop("disabled", true);
                                $("#ddlFranjaHoraria").prop("disabled", true);
                                alert("No se hará visita técnica.", "Alerta");
                            }
                            SessionPMHFC.CODMOTOT = data.data.Codmot;
                            SessionPMHFC.strVisitTecAnotaciones = data.data.Anotaciones
                            SessionPMHFC.strVisitTecSubTipo = data.data.Subtipo;
                        }
                        else {
                            $("#btnGuardar").prop("disabled", true);
                            alert("Error al validar visita técnica.", "Alerta");
                        }
                    }
                    $.unblockUI();
                },
                failure: function (msg) {
                    $.unblockUI();
                },
                error: function (xhr, status, error) {
                    $.unblockUI();
                },
                complete: function () {
                    $.unblockUI();
                }
            });
        },
        f_ApplyTechnicalVisit: function (strSelectSubWorkType) {
            var that = this;

            if (strSelectSubWorkType != "NDEF") {
                var newListSubType = [];
                controls.sltSubWorkType.prop("disabled", false);
                $(Session.vListSubType).each(function (key, value) {
                    var strCodeValue = value.strCode;
                    if (strCodeValue != '') {
                        if (strCodeValue.indexOf('|') > 0) {
                            var strSubWorkType = strCodeValue.split('|')[0];
                            var intValidate = strSelectSubWorkType.toLowerCase().indexOf(strSubWorkType.toLowerCase());
                            if (intValidate >= 0) {
                                newListSubType.push(value);
                            }
                        }
                    }
                });

                if (newListSubType.length == 1) {
                    var selected = newListSubType[0].strCode;
                    controls.sltSubWorkType.val(selected).change();
                    controls.sltSubWorkType.prop("disabled", true);
                }
                else if (newListSubType.length == 0) {

                }
                else if (strSelectSubWorkType != "") {
                    that.f_LoadSelectControl(controls.sltSubWorkType, newListSubType, 'typeservice');
                }

            } else {
                $("#sltSubWorkType").prop("disabled", false);
            }


        },
        f_GetValidateETA: function () {
            var that = this, model = {};
            model.IdSession = SessionPMHFC.IDSESSION;
            model.strJobTypes = $("#sltWorkType").val();
            model.strCodePlanInst = SessionPMHFC.hdnIDPlano;

            $.ajax({
                type: "POST",
                url: '/Transactions/SchedulingToa/GetValidateETA',
                data: JSON.stringify(model),
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                error: function (xhr, status, error) {
                    alert("Error JS : en llamar a ValidacionETA.", "Alerta");
                },
                success: function (data) {
                    var oItem = data.data;
                    if (oItem.Codigo == '1' || oItem.Codigo == '0' || oItem.Codigo == '2') {
                        SessionPMHFC.strValidateETA = oItem.Codigo;
                        SessionPMHFC.strHistoryETA = oItem.Codigo2;
                        Session.ValidateETA = oItem.Codigo;
                        Session.History = oItem.Codigo2;
                        if (oItem.Codigo == '2') {
                            SessionPMHFC.strValidateETA = "2";
                            that.f_ActDesactivaCamposAgendamiento(SessionPMHFC.strValidateETA);
                        }
                        if (oItem.Codigo == '1') {
                            SessionPMHFC.strValidateETA = "1";
                            that.f_ActDesactivaCamposAgendamiento(SessionPMHFC.strValidateETA);
                        }
                        else {
                            SessionPMHFC.strValidateETA = "0";
                            controls.sltSubWorkType.prop('disabled', true);
                            that.f_ActDesactivaCamposAgendamiento(SessionPMHFC.strValidateETA);
                            alert("No aplica agendamiento en línea, favor de continuar con la operación.", "Alerta");
                        }

                    } else {
                        SessionPMHFC.strValidateETA = "0";
                        SessionPMHFC.strHistoryETA = oItem.Code2;
                        controls.sltSubWorkType.prop('disabled', true);
                        that.f_ActDesactivaCamposAgendamiento(SessionPMHFC.strValidateETA);
                        alert(SessionPMHFC.strMessageValidationETA, "Alerta");
                    }
                    Session.ValidateETA = SessionPMHFC.strValidateETA;
                    Session.History = SessionPMHFC.strHistoryETA;
                }

            });

        },
        f_SeleccionOperatorclick: function () {
            if ($("#sltOperator option:selected").text() != 'AMERICA MOVIL PERU SAC') {
                $("#txtNroCarta").val("");
                $("#txtNroCarta").prop("disabled", false);
                Smmry.set('numerocarta', "");
                Smmry.set('numerocarta', $('#txtNroCarta').val());
            } else {
                $("#txtNroCarta").prop("disabled", true);
                $("#txtNroCarta").val("");
                Smmry.set('numerocarta', $('#txtNroCarta').val());
            }

            Smmry.set('operador', $("#sltOperator option:selected").text());
        },
        getTypification: function () {
            var that = this, controls = that.getControls();

            that.Loading();
            $.ajax({
                type: "POST",
                url: that.strUrl + '/Transactions/CommonServices/GetTypification',
                data: {
                    strIdSession: SessionPMHFC.IDSESSION,
                    strTransactionName: that.strTransactionTypi
                },
                success: function (result) {
                    var list = result.ListTypification;
                    if (list != null) {
                        if (list.length > 0) {
                            that.strClase = list[0].CLASE;
                            that.strClaseCode = list[0].CLASE_CODE;
                            that.strInteraccionCode = list[0].INTERACCION_CODE;
                            that.strSubClase = list[0].SUBCLASE;
                            that.strSubClaseCode = list[0].SUBCLASE_CODE;
                            that.strTipo = list[0].TIPO;
                            that.strTipoCode = list[0].TIPO_CODE;
                            that.getBusinessRules();
                        } else {
                            var msg = 'No se reconoce la tipificación de esta transacción';

                            controls.btnConstancy.prop('disabled', true);
                        }
                    } else {
                        var msg = 'No se reconoce la tipificación de esta transacción';

                        controls.btnConstancy.prop('disabled', true);
                    }

                },
                failure: function (msg) {
                    $.unblockUI();
                },
                error: function (xhr, status, error) {
                    $.unblockUI();
                },
                complete: function () {
                    $.unblockUI();

                }
            });
        },
        getBusinessRules: function () {
            var that = this, controls = that.getControls();
            $.ajax({
                type: "POST",
                url: that.strUrl + '/Transactions/CommonServices/GetBusinessRules',
                data: {
                    strIdSession: SessionPMHFC.IDSESSION,
                    strSubClase: that.strSubClaseCode
                },
                success: function (result) {
                    if (result.data.ListBusinessRules != null) {
                        var list = result.data.ListBusinessRules;
                        if (list.length > 0) {
                            controls.divRules.append(list[0].REGLA);
                        }
                    }

                },
                failure: function (msg) {
                    $.unblockUI();
                },
                error: function (xhr, status, error) {
                    $.unblockUI();
                },
                complete: function () {
                    $.unblockUI();

                }
            });
        },
        getValidateProfile: function () {
            var that = this, controls = that.getControls();
            $.ajax({
                type: "POST",
                url: that.strUrl + '/Transactions/HFC/PlanMigration/ValidateProfile',
                data: { strIdSession: SessionPMHFC.IDSESSION },
                success: function (result) {
                    if (result.data != null) {
                        var objResponse = result.data;
                        var strUserAccess = SessionPMHFC.USERACCESS.optionPermissions;

                        $('#txtReintegro').attr('disabled', true);
                        $('#txtMontoFideliza').attr('disabled', true);

                        if (strUserAccess.indexOf(objResponse.strOpcActivaPuedeFideMP) !== -1) {
                            SessionPMHFC.hdnPuedeFidelizar = "1";
                            SessionPMHFC.hdnAutorizacionFidelizar = "0";
                        }

                        if (strUserAccess.indexOf(objResponse.strOpcActivaNoPuedeFideMP) !== -1) {
                            SessionPMHFC.hdnPuedeFidelizar = "1";
                        }

                        if (strUserAccess.indexOf(objResponse.strOpcActivaAutorizaFideMP) !== -1) {
                            SessionPMHFC.hdnPuedeFidelizar = "1";
                            SessionPMHFC.hdnAutorizacionFidelizar = "1";
                        }

                        //if (strUserAccess.indexOf(objResponse.strOpcActivaPuedeIngMonMP) !== -1) {
                        //    $('#hdnPuedeIngMontos').val("1");
                        //    $('#hdnAutorizacionIngMontos').val("0");
                        //    $('#txtReintegro').attr('disabled', false);
                        //    $('#txtMontoFideliza').attr('disabled', false);
                        //}

                        //if (strUserAccess.indexOf(objResponse.strOpcActivaAutorizaIngMonMP) !== -1) {
                        //    $('#hdnPuedeIngMontos').val("1");
                        //    $('#hdnAutorizacionIngMontos').val("1");
                        //    $('#txtReintegro').attr('disabled', false);
                        //    $('#txtMontoFideliza').attr('disabled', false);
                        //}

                        //if (strUserAccess.indexOf(objResponse.strOpcActivaNoPuedeIngMonMP) !== -1) {
                        //    $('#hdnPuedeIngMontos').val("0");
                        //    $('#txtReintegro').attr('disabled', true);
                        //    $('#txtMontoFideliza').attr('disabled', true);
                        //}
                    }

                },
                failure: function (msg) {
                    $.unblockUI();
                },
                error: function (xhr, status, error) {
                    $.unblockUI();
                },
                complete: function () {
                    $.unblockUI();

                }
            });
        },
        getCurrentPlanServices: function () {
            var that = this;
            var controls = that.getControls();
            that.Loading();
            $.ajax({
                type: "POST",
                url: that.strUrl + "/Transactions/HFC/PlanMigration/GetServicesByCurrentPlan",
                data: { strIdSession: SessionPMHFC.IDSESSION, strIdContract: SessionPMHFC.DATACUSTOMER.ContractID },

                success: function (data) {
                    var ncable = 0, ninternet = 0, nphone = 0;
                    var ncableadicionales = 0, ninternetadicionales = 0, nphoneadicionales = 0;


                    if (data.data.ServicesByCurrentPlanCharges != null) {
                        controls.lblMontoActualBase.text('S/ ' + data.data.ServicesByCurrentPlanCharges.MontoActualBase);
                        controls.lblMontoActualAdicional.text('S/ ' + data.data.ServicesByCurrentPlanCharges.MontoActualAdicional); controls.lblCargoFijoTotalPlanSIGVActual.text('S/ ' + data.data.ServicesByCurrentPlanCharges.MontoActualBase);
                        controls.lblCantidadActual.text(data.data.ServicesByCurrentPlanCharges.CantidadServicios);
                        var m = controls.lblMontoActualBase.text();
                        var ma = controls.lblMontoActualAdicional.text();
                        var mf = parseFloat(m.slice(3, m.length));
                        var maf = parseFloat(ma.slice(3, ma.length));
                        var cftsi = mf + maf;
                        controls.lblCargoFijoTotalPlanSIGVActual.text('S/ ' + cftsi.toFixed(2));
                        var cftci = (mf + maf) * parseFloat(that.strIGV);
                        controls.lblCargoFijoTotalPlanCIGVActual.text('S/ ' + cftci.toFixed(2));
                    }
                    if (data.data.ServicesByCurrentPlan != null) {
                        $.each(data.data.ServicesByCurrentPlan, function (index, item) {
                            if (item.ServiceType != "ADICIONAL") {

                                if (SessionPMHFC.strPlanActualCable.indexOf(item.NoGrp) > -1) {
                                    controls.lstCurrentPlanCable.append('<li class="transac-list-group-item"><span class="badge">S/ ' + item.CargoFijoConIgv + '</span> ' + item.DeSer + '</li>');
                                    ncable++;
                                    var servicesForResume = {};
                                    servicesForResume.Servicio = "Cable";
                                    servicesForResume.Tipo = item.ServiceType;
                                    servicesForResume.NombreServicio = item.DeSer;
                                    servicesForResume.CF = item.CargoFijo;
                                    servicesForResume.CodServSisact = item.CodServSisact;
                                    servicesForResume.CargoFijoConIgv = item.CargoFijoConIgv;

                                    controls.lstResumenPlanActual.append('<li class="transac-list-group-item"><div class="row"><div class="col-sm-2">' + servicesForResume.Servicio + '</div><div class="col-sm-2">' + servicesForResume.Tipo + '</div><div class="col-sm-6">' + servicesForResume.NombreServicio + '</div><div class="col-sm-2"><span class="badge transac-badge-row">' + servicesForResume.CargoFijoConIgv + '</span></div></div></li>');
                                }

                                if (SessionPMHFC.strPlanActualInternet.indexOf(item.NoGrp)) {
                                    controls.lstCurrentPlanInternet.append('<li class="transac-list-group-item"><span class="badge">S/ ' + item.CargoFijoConIgv + '</span> ' + item.DeSer + '</li>');
                                    ninternet++;
                                    var servicesForResume = {};
                                    servicesForResume.Servicio = "Internet";
                                    servicesForResume.Tipo = item.ServiceType;
                                    servicesForResume.NombreServicio = item.DeSer;
                                    servicesForResume.CF = item.CargoFijo;
                                    servicesForResume.CodServSisact = item.CodServSisact;
                                    servicesForResume.CargoFijoConIgv = item.CargoFijoConIgv;


                                    controls.lstResumenPlanActual.append('<li class="transac-list-group-item"><div class="row"><div class="col-sm-2">' + servicesForResume.Servicio + '</div><div class="col-sm-2">' + servicesForResume.Tipo + '</div><div class="col-sm-6">' + servicesForResume.NombreServicio + '</div><div class="col-sm-2"><span class="badge transac-badge-row">' + servicesForResume.CargoFijoConIgv + '</span></div></div></li>');
                                }


                                if (SessionPMHFC.strPlanActualTelephony.indexOf(item.NoGrp)) {
                                    controls.lstCurrentPlanTelephony.append('<li class="transac-list-group-item"><span class="badge">S/' + item.CargoFijoConIgv + '</span> ' + item.DeSer + '</li>');
                                    nphone++;
                                    var servicesForResume = {};
                                    servicesForResume.Servicio = "Teléfono";
                                    servicesForResume.Tipo = item.ServiceType;
                                    servicesForResume.NombreServicio = item.DeSer;
                                    servicesForResume.CF = item.CargoFijo;
                                    servicesForResume.CodServSisact = item.CodServSisact;
                                    servicesForResume.CargoFijoConIgv = item.CargoFijoConIgv;

                                    controls.lstResumenPlanActual.append('<li class="transac-list-group-item"><div class="row"><div class="col-sm-2">' + servicesForResume.Servicio + '</div><div class="col-sm-2">' + servicesForResume.Tipo + '</div><div class="col-sm-6">' + servicesForResume.NombreServicio + '</div><div class="col-sm-2"><span class="badge transac-badge-row">' + servicesForResume.CargoFijoConIgv + '</span></div></div></li>');
                                }
                            }
                            else {
                                if (SessionPMHFC.strPlanActualCable.indexOf(item.NoGrp) > -1) {
                                    controls.lstCurrentPlanCable.append('<li class="transac-list-group-item"><span class="badge">S/' + item.CargoFijoConIgv + '</span> ' + item.DeSer + '</li>');
                                    ncable++;
                                    var servicesForResume = {};
                                    servicesForResume.Servicio = "Cable";
                                    servicesForResume.Tipo = item.ServiceType;
                                    servicesForResume.NombreServicio = item.DeSer;
                                    servicesForResume.CF = item.CargoFijo;
                                    servicesForResume.CodServSisact = item.CodServSisact;
                                    servicesForResume.CargoFijoConIgv = item.CargoFijoConIgv;

                                    controls.lstResumenPlanActual.append('<li class="transac-list-group-item"><div class="row"><div class="col-sm-2">' + servicesForResume.Servicio + '</div><div class="col-sm-2">' + servicesForResume.Tipo + '</div><div class="col-sm-6">' + servicesForResume.NombreServicio + '</div><div class="col-sm-2"><span class="badge transac-badge-row">' + servicesForResume.CargoFijoConIgv + '</span></div></div></li>');
                                }
                                if (SessionPMHFC.strPlanActualInternet.indexOf(item.NoGrp) > -1) {
                                    controls.lstCurrentPlanInternet.append('<li class="transac-list-group-item"><span class="badge">S/ ' + item.CargoFijoConIgv + '</span> ' + item.DeSer + '</li>');
                                    ninternet++;
                                    var servicesForResume = {};
                                    servicesForResume.Servicio = "Internet";
                                    servicesForResume.Tipo = item.ServiceType;
                                    servicesForResume.NombreServicio = item.DeSer;
                                    servicesForResume.CF = item.CargoFijo;
                                    servicesForResume.CodServSisact = item.CodServSisact;
                                    servicesForResume.CargoFijoConIgv = item.CargoFijoConIgv;

                                    controls.lstResumenPlanActual.append('<li class="transac-list-group-item"><div class="row"><div class="col-sm-2">' + servicesForResume.Servicio + '</div><div class="col-sm-2">' + servicesForResume.Tipo + '</div><div class="col-sm-6">' + servicesForResume.NombreServicio + '</div><div class="col-sm-2"><span class="badge transac-badge-row">' + servicesForResume.CargoFijoConIgv + '</span></div></div></li>');
                                }
                                if (SessionPMHFC.strPlanActualTelephony.indexOf(item.NoGrp) > -1) {
                                    controls.lstCurrentPlanTelephony.append('<li class="transac-list-group-item"><span class="badge">S/ ' + item.CargoFijoConIgv + '</span> ' + item.DeSer + '</li>');
                                    nphone++;
                                    var servicesForResume = {};
                                    servicesForResume.Servicio = "Teléfono";
                                    servicesForResume.Tipo = item.ServiceType;
                                    servicesForResume.NombreServicio = item.DeSer;
                                    servicesForResume.CF = item.CargoFijo;
                                    servicesForResume.CodServSisact = item.CodServSisact;
                                    servicesForResume.CargoFijoConIgv = item.CargoFijoConIgv;

                                    controls.lstResumenPlanActual.append('<li class="transac-list-group-item"><div class="row"><div class="col-sm-2">' + servicesForResume.Servicio + '</div><div class="col-sm-2">' + servicesForResume.Tipo + '</div><div class="col-sm-6">' + servicesForResume.NombreServicio + '</div><div class="col-sm-2"><span class="badge transac-badge-row">' + servicesForResume.CargoFijoConIgv + '</span></div></div></li>');
                                }

                            }
                        });
                    }

                    if (ncable == 0) controls.lstCurrentPlanCable.append('<li class="transac-list-group-item transac-message-red text-center"> No existen registros </li>');
                    if (ninternet == 0) controls.lstCurrentPlanInternet.append('<li class="transac-list-group-item transac-message-red text-center"> No existen registros </li>');
                    if (nphone == 0) controls.lstCurrentPlanTelephony.append('<li class="transac-list-group-item transac-message-red text-center"> No existen registros </li>');

                },
                failure: function (msg) {
                    $.unblockUI();
                },
                error: function (xhr, status, error) {
                    $.unblockUI();
                },
                complete: function () {
                    $.unblockUI();

                }
            });
        },
        getCurrentPlanAditionalServices: function () {
            var that = this;
            var controls = that.getControls();
            $.ajax({
                type: "POST",
                url: that.strUrl + "/Transactions/HFC/PlanMigration/GetServicesByCurrentPlan",
                data: { strIdSession: SessionPMHFC.IDSESSION, strIdContract: SessionPMHFC.DATACUSTOMER.ContractID },

                success: function (data) {
                    var ncable = 0, ninternet = 0, nphone = 0;

                    $.each(data.data.ServicesByCurrentPlan, function (index, item) {
                        if (item.NoGrp == "3" || item.NoGrp == "5" || item.NoGrp == "4") {
                            controls.lstCurrentPlanAdicionalCable.append('<li class="transac-list-group-item"><span class="badge">S/ ' + item.CargoFijo + '</span> ' + item.DeSer + '</li>');
                            ncable++;
                        }
                        if (item.NoGrp == "2" || item.NoGrp == "7") {
                            controls.lstCurrentPlanAdicionalInternet.append('<li class="transac-list-group-item"><span class="badge">S/ ' + item.CargoFijo + '</span> ' + item.DeSer + '</li>');
                            ninternet++;
                        }
                        if (item.NoGrp == "1" || item.NoGrp == "6") {
                            controls.lstCurrentPlanAdicionalTelephony.append('<li class="transac-list-group-item"><span class="badge">S/ ' + item.CargoFijo + '</span> ' + item.DeSer + '</li>');
                            nphone++;
                        }
                    });

                    if (ncable == 0) controls.lstCurrentPlanAdicionalCable.append('<li class="transac-list-group-item transac-message-red text-center"> No existen registros </li>');
                    if (ninternet == 0) controls.lstCurrentPlanAdicionalInternet.append('<li class="transac-list-group-item transac-message-red text-center"> No existen registros </li>');
                    if (nphone == 0) controls.lstCurrentPlanAdicionalTelephony.append('<li class="transac-list-group-item transac-message-red text-center"> No existen registros </li>');
                },
                failure: function (msg) {
                    $.unblockUI();
                },
                error: function (xhr, status, error) {
                    $.unblockUI();
                },
                complete: function () {
                    $.unblockUI();

                }
            });
        },
        getEquipmentByCurrentPlan: function () {
            var that = this;
            controls = that.getControls();
            that.Loading();

            controls.lstCurrentEquipCable.append('<li class="transac-list-group-item"><div align="center"><img src="' + that.strUrlLogo + '" width="25" height="25" /> Cargando ... </div></li>');
            controls.lstCurrentEquipInternet.append('<li class="transac-list-group-item"><div align="center"><img src="' + that.strUrlLogo + '" width="25" height="25" /> Cargando ... </div></li>');
            controls.lstCurrentEquipTelephony.append('<li class="transac-list-group-item"><div align="center"><img src="' + that.strUrlLogo + '" width="25" height="25" /> Cargando ... </div></li>');
            $.ajax({
                type: "POST",
                url: that.strUrl + "/Transactions/HFC/PlanMigration/GetEquipmentByCurrentPlan",
                data: { strIdSession: SessionPMHFC.IDSESSION, strIdContract: SessionPMHFC.DATACUSTOMER.ContractID },

                success: function (data) {
                    var ncable = 0;
                    var ninternet = 0;
                    var nphone = 0;
                    controls.lstCurrentEquipCable.html('');
                    controls.lstCurrentEquipInternet.html('');
                    controls.lstCurrentEquipTelephony.html('');
                    $("#lblCantidadActualEquipos").text("0");
                    $.each(data.data, function (index, item) {
                        if (item.ServiceType == "CABLE") {
                            controls.lstCurrentEquipCable.append('<li class="transac-list-group-item"><span class="badge">' + item.CANTIDAD + '</span> ' + item.Description + '</li>');
                            that.f_AddEquipment(item.CANTIDAD, $("#lblCantidadActualEquipos"));
                            ncable++;
                        }
                        if (item.ServiceType == "INTERNET") {
                            controls.lstCurrentEquipInternet.append('<li class="transac-list-group-item"><span class="badge">' + item.CANTIDAD + '</span>' + item.Description + '</li>');
                            that.f_AddEquipment(item.CANTIDAD, $("#lblCantidadActualEquipos"));
                            ninternet++;
                        }
                        if (item.ServiceType == "TELEFONO") {
                            controls.lstCurrentEquipTelephony.append('<li class="transac-list-group-item"><span class="badge">' + item.CANTIDAD + '</span>' + item.Description + '</li>');
                            that.f_AddEquipment(item.CANTIDAD, $("#lblCantidadActualEquipos"));
                            nphone++;
                        }
                    });

                    if (ncable == 0) controls.lstCurrentEquipCable.append('<li class="transac-list-group-item transac-message-red text-center"> No existen registros </li>');
                    if (ninternet == 0) controls.lstCurrentEquipInternet.append('<li class="transac-list-group-item transac-message-red text-center"> No existen registros </li>');
                    if (nphone == 0) controls.lstCurrentEquipTelephony.append('<li class="transac-list-group-item transac-message-red text-center"> No existen registros </li>');
                },
                failure: function (msg) {
                    $.unblockUI();
                },
                error: function (xhr, status, error) {
                    $.unblockUI();
                },
                complete: function () {
                    $.unblockUI();

                }
            });
        },
        sendNewPlanServices: function () {
            var that = this;
            var controls = that.getControls();
            var objServicesByPlan = { strIdSession: SessionPMHFC.IDSESSION, ServicesByPlan: that.listServicesByPlan };
            $.ajax({
                type: "POST",
                url: that.strUrl + "/Transactions/HFC/PlanMigration/SendNewPlanServices",
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                data: JSON.stringify(objServicesByPlan),

                success: function (data) {
                    //console.logdata);
                },
                failure: function (msg) {
                    $.unblockUI();
                },
                error: function (xhr, status, error) {
                    $.unblockUI();
                },
                complete: function () {
                    $.unblockUI();

                }
            });
        },
        collapseServices_Click: function () {
            var that = this;
            var controls = that.getControls();
            if (controls.spanCollapseServices.hasClass("glyphicon-triangle-top")) {
                controls.spanCollapseServices.removeClass("glyphicon-triangle-top");
                controls.spanCollapseServices.addClass("glyphicon-triangle-bottom");
            }
            else {
                controls.spanCollapseServices.removeClass("glyphicon-triangle-bottom");
                controls.spanCollapseServices.addClass("glyphicon-triangle-top");
            }
        },
        collapseEquipments_Click: function () {
            var that = this;
            var controls = that.getControls();
            if (controls.spanCollapseEquipments.hasClass("glyphicon-triangle-top")) {
                controls.spanCollapseEquipments.removeClass("glyphicon-triangle-top");
                controls.spanCollapseEquipments.addClass("glyphicon-triangle-bottom");
            }
            else {
                controls.spanCollapseEquipments.removeClass("glyphicon-triangle-bottom");
                controls.spanCollapseEquipments.addClass("glyphicon-triangle-top");
            }
        },
        strUrl: window.location.protocol + '//' + window.location.host,
        strClase: '',
        strClaseCode: '',
        strInteraccionCode: '',
        strSubClase: '',
        strSubClaseCode: '',
        strTipo: '',
        strTipoCode: '',
        listServices: [],
        listServicesByPlan: [],
        listAllServicesByPlan: [],
        listAditionalServices: [],
        listAditionalRentServices: [],
        listDecosByPlan: [],
        listCoreServices: [],
        ConstanceXml: "",
        strTransactionTypi: 'TRANSACCION_MIGRACION_PLAN_HFC',
        navigateTabsTwoStep: function (e) {
            var $activeTab = $('.step.tab-pane.active');

            if (e.target != null) {
                e = e.target;
                //console.loge);
            }

            if ($(e).hasClass('next-two-step')) {

                var nextTabFirst = $activeTab.next('.tab-pane').attr('id');
                var percentFirst = $activeTab.next('.tab-pane').attr('percent');
                var nextTab = $activeTab.next('.tab-pane').next('.tab-pane').attr('id');
                var percent = $activeTab.next('.tab-pane').next('.tab-pane').attr('percent');
                //console.lognextTab);
                document.getElementById('prog').style.width = percent;


                $('[href="#' + nextTabFirst + '"]').addClass('transaction-button').removeClass('btn-default');
                $('[href="#' + nextTabFirst + '"]').tab('show');
                $('[href="#' + nextTabFirst + '"]').removeClass('disabled');
                $('[href="#' + nextTabFirst + '"]').prop('disabled', false);

                if (nextTab != null) {
                    $('.btn-circle.transaction-button').removeClass('transaction-button').addClass('btn-default');
                }
                $('[href="#' + nextTab + '"]').addClass('transaction-button').removeClass('btn-default');
                $('[href="#' + nextTab + '"]').tab('show');
                $('[href="#' + nextTab + '"]').removeClass('disabled');
                $('[href="#' + nextTab + '"]').prop('disabled', false);
            }
        },
        loadSessionData: function () {
            var that = this;
            var controls = this.getControls();
            controls.lblTitle.text("CAMBIO DE PLAN");
            var SessionTransac = JSON.parse(sessionStorage.getItem("SessionTransac"));

            SessionPMHFC.DATACUSTOMER = SessionTransac.SessionParams.DATACUSTOMER;
            SessionPMHFC.USERACCESS = SessionTransac.SessionParams.USERACCESS;
            SessionPMHFC.DATASERVICE = SessionTransac.SessionParams.DATASERVICE;
            SessionPMHFC.IDSESSION = SessionTransac.UrlParams.IDSESSION;
            SessionPMHFC.UrlParams = SessionTransac.UrlParams;
            SessionPMHFC.ProductType = ((SessionPMHFC.UrlParams.SUREDIRECT == 'HFC') ? '05' : '08');
            SessionPMHFC.CurrentPlanHasTelephony = controls.lstCurrentPlanTelephony.children.length > 0 ? 1 : 0;
            
            that.f_ValidatePage();

        },
        f_ValidatePage: function(){
            var that = this,
                controls = this.getControls();

            SessionPMHFC.strEstadoContratoInactivo = strEstadoContratoInactivo;
            SessionPMHFC.strEstadoContratoSuspendido = strEstadoContratoSuspendido;
            SessionPMHFC.strEstadoContratoReservado = strEstadoContratoReservado;
            SessionPMHFC.strMsjEstadoContratoInactivo = strMsjEstadoContratoInactivo;
            SessionPMHFC.strMsjEstadoContratoSuspendido = strMsjEstadoContratoSuspendido;
            SessionPMHFC.strMsjEstadoContratoReservado = strMsjEstadoContratoReservado;

            //EVALENZS - INICIO - CAMBIO DE PLAN
            var strPlano = SessionPMHFC.DATACUSTOMER.PlaneCodeInstallation;//SessionTransacHFC.SessionParams.DATACUSTOMER.PlaneCodeInstallation;

            if (strPlano.search(strPlanoFTTH) > 0 && strMensajeTransaccionFTTH != '') {
                alert(strMensajeTransaccionFTTH.replace('{0}', 'CAMBIO DE PLAN'), "Alerta", function () {
                    parent.window.close();
                });
                that.f_blockForm();
            }
            //EVALENZS - FIN

            if (SessionPMHFC.DATASERVICE.StateLine == SessionPMHFC.strEstadoContratoInactivo) {
                alert(SessionPMHFC.strMsjEstadoContratoInactivo, 'Alerta', function () {
                    parent.window.close();
                });
                that.f_blockForm();
            }
            if (SessionPMHFC.DATASERVICE.StateLine == SessionPMHFC.strEstadoContratoReservado) {
                alert(SessionPMHFC.strMsjEstadoContratoReservado, 'Alerta', function () {
                    parent.window.close();
                });
                that.f_blockForm();
            }
            if (SessionPMHFC.DATASERVICE.StateLine == SessionPMHFC.strEstadoContratoSuspendido) {
                alert(SessionPMHFC.strMsjEstadoContratoSuspendido, 'Alerta', function () {
                    parent.window.close();
                });
                that.f_blockForm();
            }

            that.render();

            controls.hidCodCampaniaMig = $("#hidCodCampaniaMig").val();
            controls.hidCambioPlanProy140245 = $("#hidCambioPlanProy140245").val();
            controls.hidMsgErrorConsultCam = $("#hidMsgErrorConsultCam").val();

            if (controls.hidCambioPlanProy140245 == 1)
            {
                that.ConsultCampaign();
            } else {
                controls.trGrupoCampania.attr("style", "display:none");
            }
            
        },
        f_blockForm: function () {
            var that = this,
                controls = this.getControls();

            controls.btn_selServ.prop('disabled', true);
            controls.btn_selTeam.prop('disabled', true);
            controls.btnAgregarEquipos.prop('disabled', true);
            controls.btnAgregarPlan.prop('disabled', true);
            controls.btnGuardar.prop('disabled', true);
            controls.btnConstancy.prop('disabled', true);
        },
        loadCustomerData: function () {
            var that = this;
            var controls = this.getControls();
            controls.lblPost_Contrato.html(SessionPMHFC.DATACUSTOMER.ContractID);
            controls.lblPost_CustomerId.html(SessionPMHFC.DATACUSTOMER.CustomerID);
            controls.lblPost_TipoCliente.html(SessionPMHFC.DATACUSTOMER.CustomerType);
            controls.lblPost_Contacto.text(SessionPMHFC.DATACUSTOMER.FullName);
            controls.lblCliente.text(SessionPMHFC.DATACUSTOMER.BusinessName);
            controls.lblNumDoc.html(SessionPMHFC.DATACUSTOMER.DNIRUC);
            controls.lblPlaAct.html(SessionPMHFC.DATASERVICE.Plan);
            controls.lblFecAct.html(SessionPMHFC.DATACUSTOMER.ActivationDate);
            controls.lblCicfac.html(SessionPMHFC.DATACUSTOMER.BillingCycle);
            //controls.lblTipAcu.html(SessionPMHFC.DATASERVICE.TermContract);
            controls.lblLimCre.html(SessionPMHFC.DATACUSTOMER.objPostDataAccount.CreditLimit);
            //controls.lblEstSer.html(SessionPMHFC.DATASERVICE.StateLine);
            controls.lblRepLeg.html(SessionPMHFC.DATACUSTOMER.LegalAgent);
            // controls.lblFecVen.html("");
            // controls.lblHUB.html("");
            //controls.lblCintillo.html("");
            //controls.lblCMTS.html("");
            controls.lblAddress.text(SessionPMHFC.DATACUSTOMER.InvoiceAddress);
            controls.lblReferencia.text(SessionPMHFC.DATACUSTOMER.InvoiceUrbanization);
            //controls.lblUrbanization.text(SessionPMHFC.DATACUSTOMER.InvoiceUrbanization);
            controls.lblCountry.text(SessionPMHFC.DATACUSTOMER.InvoiceCountry);
            controls.lblDepartment.text(SessionPMHFC.DATACUSTOMER.InvoiceDepartament);
            controls.lblProvince.text(SessionPMHFC.DATACUSTOMER.InvoiceProvince);
            controls.lblDistrict.text(SessionPMHFC.DATACUSTOMER.InvoiceDistrict);
            controls.lblPlaneCode.text(SessionPMHFC.DATACUSTOMER.PlaneCodeBilling);
            //controls.lblUbigeoCode.text(SessionPMHFC.DATACUSTOMER.InvoiceUbigeo);
            SessionPMHFC.hdnIDPlano = SessionPMHFC.DATACUSTOMER.PlaneCodeInstallation;
            SessionPMHFC.CoreId = "1";
            SessionPMHFC.CoreAdicionalId = "2";
            SessionPMHFC.AdicionalId = "3";
            SessionPMHFC.NINGUN_OPERADOR = "06";
            SessionPMHFC.RouteRecordPDF = "";
            SessionPMHFC.strServer = $("#HfcPlanMigration").data("server");
            SessionPMHFC.OneCoreCable = 0;
            SessionPMHFC.OneCoreInternet = 0;
            SessionPMHFC.OneCorePhone = 0;
            SessionPMHFC.strErrorMessageIgv = $("#HfcPlanMigration").data("errormessageigv");
            SessionPMHFC.strMessageValidationETA = $("#HfcPlanMigration").data("strmessagevalidationeta");
            SessionPMHFC.strMessageSave = $("#HfcPlanMigration").data("strmessagesave");
            Session.contractID = SessionPMHFC.DATACUSTOMER.ContractID;

        },
        chkPresuscrito_Click: function () {
            var that = this, controls = that.getControls();

            if ($("#chkPresuscrito").prop("checked")) {
                $("#sltOperator").prop("disabled", false);
                var textOperator = "AMERICA MOVIL PERU SAC";
                //that.Loading(); 
                $("#sltOperator option").each(function (a, b) {
                    //console.log$(this).html());
                    //console.logtextOperator);
                    if ($(this).html() == textOperator) {
                        $(this).attr("selected", "selected");
                    }
                });
                $("#txtNroCarta").prop("disabled", true);
            } else {
                $("#sltOperator").prop("disabled", true);
                $("#sltOperator").val("-1");
                $("#txtNroCarta").prop("disabled", false);
                $("#txtNroCarta").val("");
            }
            //summary
            Smmry.set('presuscrito', $("#chkPresuscrito").prop("checked") ? "SI" : "NO");
        },
        chkPublicar_Click: function () {
            if ($("#chkPublicar").prop("checked")) {
                SessionPMHFC.hdnCheckPublicarFinal = "1";
            } else {
                SessionPMHFC.hdnCheckPublicarFinal = "0";
            }
            Smmry.set('publicarnumero', $("#chkPublicar").prop("checked") ? "SI" : "NO");
        },
        pad: function (s) { return (s < 10) ? '0' + s : s; },
        f_IniTablePrincipales: function () {


            this.tblCableCandidateEquipment = $("#tblCableCandidateEquipment").dataTable({
                "pagingType": "full_numbers",
                "scrollY": "240px",
                "scrollCollapse": true,
                "paging": false,
                "pageLength": 50,
                "destroy": true,
                "searching": false,
                "sort": false,
                "language": {
                    "lengthMenu": "Mostrar _MENU_ registros por página.",
                    "zeroRecords": "No existen datos",
                    "info": " ",
                    "infoEmpty": " ",
                    "infoFiltered": "(filtered from _MAX_ total records)",
                    "oPaginate": {
                        "sFirst": "Primero",
                        "sPrevious": "Anterior",
                        "sNext": "Siguiente",
                        "sLast": "Último"
                    },
                    "emptyTable": "No existen datos"
                }
, "columnDefs": [
{
    targets: 0
},
{
    targets: 1,
    width: 260

}
],
                select: {
                    style: 'os',
                    selector: 'td:first-child'
                },
                order: [[1, 'asc']]
            })
.on('select', function () {

});
            this.tblInternetCandidateEquipment = $("#tblInternetCandidateEquipment").dataTable({
                "pagingType": "full_numbers",
                "scrollY": "240px",
                "scrollCollapse": true,
                "paging": false,
                "pageLength": 50,
                "destroy": true,
                "searching": false,
                "language": {
                    "lengthMenu": "Mostrar _MENU_ registros por página.",
                    "zeroRecords": "No existen datos",
                    "info": " ",
                    "infoEmpty": " ",
                    "infoFiltered": "(filtered from _MAX_ total records)",
                    "oPaginate": {
                        "sFirst": "Primero",
                        "sPrevious": "Anterior",
                        "sNext": "Siguiente",
                        "sLast": "Último"
                    },
                    "emptyTable": "No existen datos"
                }
, "columnDefs": [
{
    targets: 0
},
{
    targets: 1,
    width: 260

}
],
                select: {
                    style: 'os',
                    selector: 'td:first-child'
                },
                order: [[1, 'asc']]
            })
.on('select', function () {
    //console.log"se acaba de seleccionar");

});
            this.tblPhoneCandidateEquipment = $("#tblPhoneCandidateEquipment").dataTable({
                "pagingType": "full_numbers",
                "scrollY": "240px",
                "scrollCollapse": true,
                "paging": false,
                "pageLength": 50,
                "destroy": true,
                "searching": false,
                "language": {
                    "lengthMenu": "Mostrar _MENU_ registros por página.",
                    "zeroRecords": "No existen datos",
                    "info": " ",
                    "infoEmpty": " ",
                    "infoFiltered": "(filtered from _MAX_ total records)",
                    "oPaginate": {
                        "sFirst": "Primero",
                        "sPrevious": "Anterior",
                        "sNext": "Siguiente",
                        "sLast": "Último"
                    },
                    "emptyTable": "No existen datos"
                }
, "columnDefs": [
{
    targets: 0
},
{
    targets: 1,
    width: 260

}
],
                select: {
                    style: 'os',
                    selector: 'td:first-child'
                },
                order: [[1, 'asc']]
            })
.on('select', function () {

});


        },
        f_FechaEsMayorQueHoy: function (fechaString) {
            var ano = fechaString.substr(6, 4);
            var mes = fechaString.substr(3, 2);
            var dia = fechaString.substr(0, 2);

            var fechaHoy = new Date(SessionPMHFC.FECHAACTUALSERVIDOR);
            var str = ano + "/" + mes + "/" + dia;

            var fechaSeleccionada = new Date(str);

            if (fechaSeleccionada > fechaHoy) {
                return true;
            } else {
                return false;
            }

        },
        f_sltWorkType_change: function () {
            var that = this;
            Smmry.set('tipodetrabajo', $("#sltWorkType option:selected").text());
        },
        f_sltSubWorkType_change: function () {
            var that = this;
            Smmry.set('Subtipotrabajo', $("#sltSubWorkType option:selected").text());
        },
        ddlFranjaHorariachange: function () {
            var that = this,
               controls = that.getControls();
            Smmry.set('Horario', $("#ddlFranjaHoraria option:selected").html() == 'Seleccionar' ? '' : $("#ddlFranjaHoraria option:selected").html());
        },
        f_ActDesactivaCamposAgendamiento: function (varBool) {
            if (varBool == "1") {
                $("#txtFProgramacion").prop("disabled", false);
                $("#txtFProgramacion").datepicker({ format: 'dd/mm/yyyy' });
                $("#ddlFranjaHoraria").prop("disabled", false);
                $("#btnValidarHorario").prop("disabled", true);



            } else {
                var fechaServidor = new Date(SessionPMHFC.FECHASERVIDOR);
                if (SessionPMHFC.strValidateETA == "0") {
                    $("#txtFProgramacion").prop("disabled", false);
                    $("#txtFProgramacion").datepicker({ format: 'dd/mm/yyyy' });
                    $("#ddlFranjaHoraria").prop("disabled", false);
                    $("#btnValidarHorario").prop("disabled", true);

                }
                else {
                    $("#txtFProgramacion").val([pad(fechaServidor.getDate()), pad(fechaServidor.getMonth() + 1), fechaServidor.getFullYear()].join("/"));
                    $("#txtFProgramacion").datepicker({ format: 'dd/mm/yyyy' });
                    $("#txtFProgramacion").prop("disabled", true);
                    $("#ddlFranjaHoraria").prop("disabled", true);
                    $("#btnValidarHorario").prop("disabled", true);

                }
            }
        },
        btnBuscarV_Click: function () {

        },
        btnAgregarEquipos_Click: function () {
            var that = this;
            if (SessionPMHFC.CODIGOPLAN == "") {
                alert("Necesita seleccionar un plan.", "Alerta");
                return false;
            }
            that.f_ListaEquipos();
        },
        chkFideliza_Click: function () {
            var that = this;

            if (SessionPMHFC.hdnPuedeFidelizar != "1") {
                alert("Usted no tiene autorización para fidelizar.", "Alerta");
                $("#chkFideliza").prop("checked", false);
                return false;
            }

            if (SessionPMHFC.hdnAutorizacionFidelizar == "1") {
                that.f_abreAutorizacionFidelizar();
                return false;
            }


            if ($("#chkFideliza").prop("checked")) {

                SessionPMHFC.hdnCheckFidelizaFinal = "1";
            } else {

                SessionPMHFC.hdnCheckFidelizaFinal = "0";
            }

            if ($("#chkFideliza").prop("checked")) {
                $("#txtMontoFideliza").prop("disabled", false);
            } else {
                $("#txtMontoFideliza").prop("disabled", true);
                $("#txtMontoFideliza").val("0");
                that.f_sumaPenalidades();
            }
            Smmry.set('ckhFidelizacion', $("#chkFideliza").prop("checked") ? "SI" : "NO");
        },
        f_sumaPenalidades: function () {
            try {

                if (!$.isNumeric($("#txtReintegro").val()) || !$.isNumeric($("#txtMontoFideliza").val())) {
                    return false;
                }
                var monto1 = parseFloat($("#txtReintegro").val());
                var monto2 = parseFloat($("#txtMontoFideliza").val());
                var r = monto1 + monto2;
                $("#txtTotalPenalidad").val(r);
                if (r > 0) {
                    $("#chkOCC").prop("checked", true);
                } else {
                    $("#chkOCC").prop("checked", false);
                }

            } catch (err) {

            }
        },
        btnAgregarPlan_Click_Pop: function () {
            var urlBase = window.location.href;
            urlBase = urlBase.substr(0, urlBase.lastIndexOf('/'));
            urlBase = location.protocol + '//' + location.host + '/Transactions/HFC/PlanMigration/HFCChoosePlan';
            var objSearch = {};
            objSearch.strIdSession = SessionPMHFC.IDSESSION;
            objSearch.strProductType = SessionPMHFC.ProductType;

            var that = this;
            var controls = that.getControls();
            var dialog = $.window.open({
                modal: true,
                type: 'post',
                title: "Seleccionar Nuevo Plan",
                url: urlBase,
                data: objSearch,
                width: 1024,
                height: 600,
                buttons: {
                    Seleccionar: {/*btnAsignarPlan*/
                        click: function () {

                            //console.log"dentro de seleccionar de btnAgregarPlan_Click_Pop");
                            var rowPost = $('#tblPlans').DataTable().rows({ selected: true }).data();
                            var item = rowPost[0];
                            //console.logitem);
                            if (item === undefined) {
                                alert("Necesita seleccionar un plan.", "Alerta");
                                return;
                            }

                            if (controls.hidCambioPlanProy140245 == 1)
                            {
                                var ArrayCampColab = controls.hidCodCampaniaMig.split("|");

                                var i = ArrayCampColab.length;
                                controls.SelectCampaniaColab = "";
                                while (i--) {
                                    if (ArrayCampColab[i] == item.strCampaignCode) {

                                        controls.SelectCampaniaColab = item.strCampaignDescription;
                                        controls.itemCamp = item;
                                    }
                                }
                            }

                            controls.itemCampSelc = item;

                            that.f_cargaPlan_Pop(item.strCodPlanSisact, item.strDesPlanSisact, item.strSolucion, this);

                            $("#btnGuardar").prop("disabled", false);
                            that.disabledSteps();
                        }
                    },
                    Cancelar: {
                        click: function (sender, args) {
                            $.unblockUI();
                            this.close();
                            that.disabledSteps();
                        }
                    }
                }
            });

        },
        f_cargaPlan_Pop: function (idPlan, planNuevo, solucionNueva, popUp) {
            var that = this;
            var controls = that.getControls();

            confirm("Si cambia el plan se borrarán los datos cargados en las tablas, ¿desea cargar un nuevo plan?", "Confirmar", function () {
                $.blockUI({
                    message: '<div align="center"><img src="' + that.strUrlLogo + '" width="25" height="25" /> Cargando ... </div>',
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

                SessionPMHFC.CODIGOPLAN = idPlan;
                $("#lblPlanNuevo").text(planNuevo);
                $("#lblPlanNuevo2").text(planNuevo);
                $("#lblSolucionNueva").text(solucionNueva);

                if (controls.hidCambioPlanProy140245 == 1)
                {
                    if (controls.SelectCampaniaColab != "")
                    {
                        controls.trSelecCampaniaColab.attr("style", "display:block");
                        controls.lblCampaniaColaborador.html(controls.SelectCampaniaColab);
                    } else {
                        controls.trSelecCampaniaColab.attr("style", "display:none");
                    }
                }
                
                popUp.close();

                that.fnClearTables();
                that.fnClearLists();
                that.fnClearCounters();
                that.fn_getAllServicesByPlan();
                that.btn_selCoreServ_Click();

                that.listAllServicesByPlan = [];

                $.unblockUI();
            });
        },
        fnClearCounters: function () {
            SessionPMHFC.OneCoreCable = 0;
            SessionPMHFC.OneCoreInternet = 0;
            SessionPMHFC.OneCorePhone = 0;
        },
        fnClearLists: function () {
            var that = this;
            that.listAditionalServices.length = 0;
            that.listAditionalRentServices.length = 0;
            that.listServices.length = 0;
            that.listServicesByPlan.length = 0;
            that.listAllServicesByPlan.length = 0;
            that.listAditionalServices.length = 0;
            that.listDecosByPlan.length = 0;
            that.listCoreServices.length = 0;

            //listServices = [];
            //listServicesByPlan = [];
            //listAllServicesByPlan = [];
            //listAditionalServices = [];
            //listDecosByPlan = [];
            //listCoreServices = [];

            //listAditionalRentServices = [];
            SessionPMHFC.ListEquipments = [];
            SessionPMHFC.ListEquipmentsAS = [];
            SessionPMHFC.ListEquipmentsASR = [];
            SessionPMHFC.ListEquipmentsBase = [];
        },
        fn_getAllServicesByPlan: function () {
            var that = this;
            var controls = that.getControls();
            var data = { strIdSession: SessionPMHFC.IDSESSION, idPlan: SessionPMHFC.PlaneCode };
            $.ajax({
                type: 'POST',
                url: that.strUrl + '/Transactions/HFC/PlanMigration/ListServicesByPlan',
                data: "{strIdSession:'" + SessionPMHFC.IDSESSION + "',idplan:'" + SessionPMHFC.CODIGOPLAN
                    + "',strProductType:'" + SessionPMHFC.ProductType + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                error: function () { },
                success: function (data) {
                    var filas = data.data;
                    $.each(filas, function (index, item) {
                        var serviceByPlan = {};
                        serviceByPlan.CantEquipment = filas[index].CantEquipment;
                        serviceByPlan.CF = filas[index].CF;
                        serviceByPlan.CodServiceType = filas[index].CodServiceType;
                        serviceByPlan.Equipment = filas[index].Equipment;
                        serviceByPlan.CodServSisact = filas[index].CodServSisact;
                        serviceByPlan.GroupServ = filas[index].GroupServ;
                        serviceByPlan.DesServSisact = filas[index].DesServSisact;
                        serviceByPlan.ServiceType = filas[index].ServiceType;
                        serviceByPlan.CodServSisact = filas[index].CodServSisact;
                        serviceByPlan.Tipequ = filas[index].Tipequ;
                        serviceByPlan.CodGroupServ = filas[index].CodGroupServ;
                        serviceByPlan.Sncode = filas[index].Sncode;
                        serviceByPlan.Spcode = filas[index].Spcode;
                        serviceByPlan.CodPlanSisact = filas[index].CodPlanSisact;
                        serviceByPlan.Tmcode = filas[index].Tmcode;
                        serviceByPlan.Codtipequ = filas[index].Codtipequ;
                        serviceByPlan.IDEquipment = filas[index].IDEquipment;
                        serviceByPlan.CfWithIgv = filas[index].CfWithIgv;

                        that.listAllServicesByPlan.push(serviceByPlan);
                    });
                    //var rows = Object.keys(that.listAllServicesByPlan).length;
                    ////console.log'that.listAllServicesByPlan.length: ' + rows);
                    //if (rows > 0) {
                    //    $("#btnSelectCore").prop("disabled", false);
                    //} else {
                    //    $("#btnSelectCore").prop("disabled", true);
                    //}  
                }
            });
        },
        btn_selCoreServ_Click: function () {
            var urlBase = window.location.href;
            urlBase = urlBase.substr(0, urlBase.lastIndexOf('/'));
            var that = this;
            var listAditionalServices = that.listAditionalServices;
            var dialog = $.window.open({
                modal: true,
                title: "Seleccionar Servicios Core",
                url: '/Transactions/HFC/PlanMigration/ChooseCoreServicesByPlan',
                data: { strIdSession: SessionPMHFC.IDSESSION.toString(), strIdCustomer: SessionPMHFC.DATACUSTOMER.CustomerID, idPlan: SessionPMHFC.CODIGOPLAN, strProductType: SessionPMHFC.ProductType, ServicesList: JSON.stringify(listAditionalServices) },
                width: 1024,
                height: 600,
                buttons: {
                    Seleccionar: {/*btnAsignarPlan*/
                        id: "btnSelectCore",
                        click: function () {
                            var ModalConfirm = this;

                            var rowPostCable = $('#tblChooseCoreServicesByPlanCable').DataTable().rows({ selected: true }).data();

                            var rowPostInternet = $('#tblChooseCoreServicesByPlanInternet').DataTable().rows({ selected: true }).data();
                            //console.log"este es el core de internet escogido: ");
                            //console.logrowPostInternet[0]);

                            var rowPostPhone = $('#tblChooseCoreServicesByPlanPhone').DataTable().rows({ selected: true }).data();
                            //console.log"este es el core de telefonía escogido: ");
                            //console.logrowPostPhone[0]);

                            var rowPostCableTotal = $('#tblChooseCoreServicesByPlanCable').DataTable().rows().data();
                            var rowPostInternetTotal = $('#tblChooseCoreServicesByPlanInternet').DataTable().rows().data();
                            var rowPostPhoneTotal = $('#tblChooseCoreServicesByPlanPhone').DataTable().rows().data();

                            var arrayCable = rowPostCable[0];
                            var arrayInternet = rowPostInternet[0];
                            var arrayPhone = rowPostPhone[0];
                            var cantidadCables = rowPostCableTotal.length;
                            var cantidadInternets = rowPostInternetTotal.length;
                            var cantidadPhones = rowPostPhoneTotal.length;
                            var huboCable = arrayCable === undefined ? 1 : 0;
                            var huboInternet = arrayInternet === undefined ? 1 : 0;
                            var huboPhone = arrayPhone === undefined ? 1 : 0;

                            if (cantidadCables > 0 && arrayCable === undefined) {
                                alert("Necesita seleccionar un servicio Core Cable.", "Alerta");
                                return false;
                            }
                            if (cantidadInternets > 0 && arrayInternet === undefined) {
                                alert("Necesita seleccionar un servicio Core Internet.", "Alerta");
                                return false;
                            }
                            if (cantidadPhones > 0 && arrayPhone === undefined) {
                                alert("Necesita seleccionar un servicio Core Teléfono.", "Alerta");
                                return false;
                            }

                            arrayCable = [];
                            for (var i = 0; i < rowPostCable.length; i++) {
                                arrayCable.push(rowPostCable[i])
                            }
                            arrayInternet = [];
                            for (var i = 0; i < rowPostInternet.length; i++) {
                                arrayInternet.push(rowPostInternet[i])
                            }
                            arrayPhone = [];
                            for (var i = 0; i < rowPostPhone.length; i++) {
                                arrayPhone.push(rowPostPhone[i])
                            }


                            confirm('Los core seleccionados se cargarán, desea cargarlos?', 'Confirmar', function (result) {
                                if (result) {
                                    $.blockUI({
                                        message: '<div align="center"><img src="' + that.strUrlLogo + '" width="25" height="25" /> Cargando ... </div>',
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


                                    that.AddTotabla360CandidatePlanCoresCable(arrayCable);
                                    that.AddTotabla360CandidatePlanCoresInternet(arrayInternet);
                                    that.AddTotabla360CandidatePlanCoresPhone(arrayPhone);
                                    that.LoadEquipmentAll_Pop(arrayCable[0], arrayInternet[0], arrayPhone[0]);
                                    $.unblockUI();

                                    ModalConfirm.close();
                                }
                            });
                            that.disabledSteps();
                            controls.btn_selServ.removeAttr('disabled');
                            controls.btn_selTeam.removeAttr('disabled');

                        }
                    },
                    Cancelar: {
                        click: function (sender, args) {
                            $("#lblPlanNuevo").html("Seleccione un plan");
                            $("#lblPlanNuevo2").html("&nbsp;");
                            $("#lblSolucionNueva").html("&nbsp;");
                            that.fnClearTables();
                            that.disabledSteps();
                            $.unblockUI();
                            this.close();
                        }
                    }
                }
            });

        },
        fnClearTables: function () {
            var that = this;
            var controls = that.getControls();
            //if (that.tabla360CandidatePlanPhone!=null) {
            //    that.tabla360CandidatePlanPhone.fnClearTable();
            //}
            //if (that.tabla360CandidatePlanInternet!=null) {
            //    that.tabla360CandidatePlanInternet.fnClearTable();
            //}

            //if (that.tabla360CandidatePlanCable!=null) {
            //    that.tabla360CandidatePlanCable.fnClearTable();
            //}
            if (that.tblCableCandidateEquipment != null) {
                that.tblCableCandidateEquipment.fnClearTable();
            }
            if (that.tblInternetCandidateEquipment != null) {
                that.tblInternetCandidateEquipment.fnClearTable();
            }
            if (that.tblPhoneCandidateEquipment != null) {
                that.tblPhoneCandidateEquipment.fnClearTable();
            }
            controls.lstSelectPlanCable.html('');
            controls.lstSelectPlanCable.append('<li class="transac-list-group-item">Seleccionar un plan</li>');
            controls.lstSelectPlanInternet.html('');
            controls.lstSelectPlanInternet.append('<li class="transac-list-group-item">Seleccionar un plan</li>');
            controls.lstSelectPlanTelephony.html('');
            controls.lstSelectPlanTelephony.append('<li class="transac-list-group-item">Seleccionar un plan</li>');


            controls.lstSelectEquipCable.html('');
            controls.lstSelectEquipCable.append('<li class="transac-list-group-item">Seleccionar un plan</li>');
            controls.lstSelectEquipInternet.html('');
            controls.lstSelectEquipInternet.append('<li class="transac-list-group-item">Seleccionar un plan</li>');
            controls.lstSelectEquipTelephony.html('');
            controls.lstSelectEquipTelephony.append('<li class="transac-list-group-item">Seleccionar un plan</li>');



            controls.CableEquipmentQty.text("0");
            controls.InternetEquipmentQty.text("0");
            controls.PhoneEquipmentQty.text("0");
            controls.DecosQuantity.text("0");
            $("#lblMontoBase").text("S/ 0");
            $("#lblMontoAdicional").text("S/ 0");
            $("#lblCargoFijoTotalPlanSIGV").text("S/ 0");
            $("#lblCargoFijoTotalPlanCIGV").text("S/ 0");
            $("#lblCantidad").text("0");
            $("#lblCantidadEquipos").text("0");

            controls.lstResumenPlanNuevo.html('');
        },
        LoadEquipmentAll_Pop: function (cable, internet, phone) { // Persquash: Trae Los Equipos
            
            var strServiceCoreCable = "";
            var strServiceCoreInternet = "";
            var strServiceCoreTelephony = "";
            if (cable != null) {
                strServiceCoreCable = cable[3];
            }
            if (internet != null) {
                strServiceCoreInternet = internet[3];
            }
            if (phone != null) {
                strServiceCoreTelephony = phone[3];
            }
            var that = this,
                controls = that.getControls();
            var oConsolidate = { strIdSession: SessionPMHFC.IDSESSION, idPlan: SessionPMHFC.CODIGOPLAN, services: that.listServices, strServiceCoreCable: strServiceCoreCable, strServiceCoreInternet: strServiceCoreInternet, strServiceCoreTelephony: strServiceCoreTelephony };
            var data = { oConsolidate: oConsolidate, strProductType: SessionPMHFC.ProductType }
            $.ajax({
                type: "POST",
                url: that.strUrl + "/Transactions/HFC/PlanMigration/ListEquipmentsByService",
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(data),
                error: function (xhr, status, error) {
                    //console.logxhr);
                },
                success: function (data) {
                    var filas = data.data;
                    SessionPMHFC.ListEquipmentsBase = data.data;
                    SessionPMHFC.ListEquipmentsCTV = [];
                    SessionPMHFC.ListEquipmentsINT = [];
                    SessionPMHFC.ListEquipmentsTLF = [];

                    if (filas != null && filas.length > 0) {
                        controls.lstSelectEquipCable.html('');
                        controls.lstSelectEquipInternet.html('');
                        controls.lstSelectEquipTelephony.html('');
                    }
                    var nequipcable = 0;
                    $.each(filas, function (index, item) {
                        if (item.CodServSisact == strServiceCoreCable && SessionPMHFC.strHFCGroupCable.indexOf(item.CodGroupServ) > -1) {
                        //if (item.IDEquipment == identificadorcorecable && item.Equipment != '' && SessionPMHFC.strHFCGroupCable.indexOf(item.CodGroupServ) > -1) {
                            var descEquipment = item.Equipment.toLowerCase();
                            SessionPMHFC.ListEquipmentsCTV.push(item);
                            if (descEquipment.indexOf("deco") > -1) {
                                controls.lstSelectEquipCable.append('<li class="transac-list-group-item"><span class="badge">' + item.CantEquipment + '</span> ' + item.Dscequ + '</li>');
                                that.f_AddEquipment(item.CantEquipment, $("#lblCantidadEquipos"));
                                nequipcable++;
                            }
                            else {
                                controls.lstSelectEquipCable.append('<li class="transac-list-group-item">' + item.Dscequ + '</li>');
                                that.f_AddEquipment(item.CantEquipment, $("#lblCantidadEquipos"));
                                nequipcable++;

                            }
                        }
                    });
                    if (nequipcable == 0) {
                        controls.lstSelectEquipCable.append('<li class="transac-list-group-item transac-message-red text-center">No existen registros</li>');
                    }

                    var nequipinternet = 0;
                    $.each(filas, function (index, item) {
                        if (item.CodServSisact == strServiceCoreInternet && item.Equipment != '' && SessionPMHFC.strHFCGroupInternet.indexOf(item.CodGroupServ) > -1) {
                        //if (item.IDEquipment == identificadorcoreinternet && item.Equipment != '' && SessionPMHFC.strHFCGroupInternet.indexOf(item.CodGroupServ) > -1) {
                            SessionPMHFC.ListEquipmentsINT.push(item);

                            controls.lstSelectEquipInternet.append('<li class="transac-list-group-item"><span class="badge">' + item.CantEquipment + '</span> ' + item.Dscequ + '</li>');
                            that.f_AddEquipment(item.CantEquipment, $("#lblCantidadEquipos"));
                            nequipinternet++;
                        }
                    });
                    if (nequipinternet == 0) {
                        controls.lstSelectEquipInternet.append('<li class="transac-list-group-item transac-message-red text-center"> No existen registros </li>');
                    }

                    var nequipphone = 0;
                    $.each(data.data, function (index, item) {
                        if (item.CodServSisact == strServiceCoreTelephony && item.Equipment != '' && SessionPMHFC.strHFCGroupTelephony.indexOf(item.CodGroupServ) > -1) {
                        //if (item.IDEquipment == identificadorcoretelephony && item.Equipment != '' && SessionPMHFC.strHFCGroupTelephony.indexOf(item.CodGroupServ) > -1) {
                            SessionPMHFC.ListEquipmentsTLF.push(item);
                            controls.lstSelectEquipTelephony.append('<li class="transac-list-group-item"><span class="badge">' + item.CantEquipment + '</span> ' + item.Dscequ + '</li>');
                            that.f_AddEquipment(item.CantEquipment, $("#lblCantidadEquipos"));
                            nequipphone++;
                        }
                    });
                    if (nequipphone == 0) {
                        controls.lstSelectEquipTelephony.append('<li class="transac-list-group-item transac-message-red text-center"> No existen registros </li>');
                    }

                }
            });

        },
        loadEquipmentCable: function () {
            var that = this;
            var controls = that.getControls();
            var oConsolidate = { strIdSession: SessionPMHFC.IDSESSION, idPlan: SessionPMHFC.CODIGOPLAN, services: that.listServices };
            var data = { oConsolidate: oConsolidate, strProductType: SessionPMHFC.ProductType }
            $.ajax({
                type: "POST",
                url: that.strUrl + "/Transactions/HFC/PlanMigration/ListEquipmentsByService",
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(data),
                error: function (xhr, status, error) {
                    //console.logxhr);
                },
                success: function (data) {
                    var filas = data.data;
                    var wanted = $.grep(that.listServices, function (item) { return item.tipo == "Cable"; });

                    if (wanted[0] != null) {
                        if (filas != null && filas.length > 0) {
                            controls.lstSelectEquipCable.html('');
                        }
                        var nequipcable = 0;
                        $.each(filas, function (index, item) {
                            if (item.CodServSisact == wanted[0].id) {
                                if (item.Equipment != '') {


                                    controls.lstSelectEquipCable.append('<li class="transac-list-group-item"><span class="badge">S/ ' + item.CF + '</span> ' + item.Equipment + '</li>');


                                    nequipcable++;
                                }

                            }
                        });
                        if (nequipcable == 0) {
                            controls.lstSelectEquipCable.append('<li class="transac-list-group-item transac-message-red text-center">No existen registros</li>');
                        }
                    }
                    else {
                        controls.lstSelectEquipCable.html = ('');
                        controls.lstSelectEquipCable.append('<li class="transac-list-group-item transac-message-red text-center">No existen registros</li>');

                    }
                }
            });

        },
        loadEquipmentInternet: function () {
            var that = this;
            var controls = that.getControls();
            var oConsolidate = { strIdSession: SessionPMHFC.IDSESSION, idPlan: SessionPMHFC.CODIGOPLAN, services: that.listServices };
            var data = { oConsolidate: oConsolidate, strProductType: SessionPMHFC.ProductType }
            $.ajax({
                type: "POST",
                url: that.strUrl + "/Transactions/HFC/PlanMigration/ListEquipmentsByService",
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                data: JSON.stringify(data),
                error: function (xhr, status, error) { },
                success: function (data) {
                    var filas = data.data;
                    var wanted = $.grep(that.listServices, function (item) { return item.tipo == "Internet"; });
                    if (wanted[0] != null) {
                        if (filas != null && filas.length > 0) {
                            controls.lstSelectEquipInternet.html('');
                        }
                        var nequipinternet = 0;
                        $.each(filas, function (index, item) {
                            if (item.CodServSisact == wanted[0].id) {
                                if (item.Equipment != '') {

                                    controls.lstSelectEquipInternet.append('<li class="transac-list-group-item"><span class="badge">S/ ' + item.CF + '</span> ' + item.Equipment + '</li>');
                                    nequipinternet++;

                                }
                            }
                        });
                        if (nequipinternet == 0) {
                            controls.lstSelectEquipInternet.append('<li class="transac-list-group-item transac-message-red text-center"> No existen registros </li>');
                        }
                    }
                    else {
                        controls.lstSelectEquipInternet.html('');
                        controls.lstSelectEquipInternet.append('<li class="transac-list-group-item transac-message-red text-center"> No existen registros </li>');

                    }
                }
            });
        },
        loadEquipmentPhone: function () {
            var that = this;
            var controls = that.getControls();
            var oConsolidate = { strIdSession: SessionPMHFC.IDSESSION, idPlan: SessionPMHFC.CODIGOPLAN, services: that.listServices };
            var data = { oConsolidate: oConsolidate, strProductType: SessionPMHFC.ProductType }
            $.ajax({
                type: "POST",
                url: that.strUrl + "/Transactions/HFC/PlanMigration/ListEquipmentsByService",
                data: JSON.stringify(data),
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                error: function (xhr, status, error) {
                    //console.logxhr);
                },
                success: function (data) {
                    var filas = data.data;
                    var wanted = $.grep(that.listServices, function (item) { return item.tipo == "Phone"; });
                    if (wanted[0] != null) {
                        if (filas != null && filas.length) {
                            controls.lstSelectEquipTelephony.html('');
                        }
                        var nequipphone = 0;
                        $.each(data.data, function (index, item) {
                            if (item.CodServSisact == wanted[0].id) {
                                if (item.Equipment != '') {


                                    controls.lstSelectEquipTelephony.append('<li class="transac-list-group-item"><span class="badge">S/ ' + item.CF + '</span> ' + item.Equipment + '</li>');
                                    nequipphone++;
                                }
                            }
                        });
                        if (nequipphone == 0) {
                            controls.lstSelectEquipTelephony.append('<li class="transac-list-group-item transac-message-red text-center"> No existen registros </li>');
                        }
                    }
                    else {
                        controls.lstSelectEquipTelephony.html('');
                        controls.lstSelectEquipTelephony.append('<li class="transac-list-group-item transac-message-red text-center"> No existen registros </li>');

                    }
                }
            });
        },
        f_ListaEquiposLupa: function (IdServicio) {
            var that = this;
            if (SessionPMHFC.CODIGOPLAN == "")//($("#hdnCodigoPlan").val() == "")
            {
                alert("Necesita seleccionar un plan.", "Alerta");
                return false;
            }
            if (IdServicio == "") {
                alert("Necesita seleccionar un servicio.", "Alerta");
                return false;
            }
            var urlBase = window.location.href;
            urlBase = urlBase.substr(0, urlBase.lastIndexOf('/'));

            var strUrl = "/Transactions/HFC/PlanMigration/ListServicesByPlanWithEquipment";

            //console.log$("#tblConsultaEquipos").DataTable());
            $("#tblConsultaEquipos").DataTable().clear();



            $.window.open({
                modal: true,
                title: "Listado de Equipos",
                url: urlBase + strUrl,
                data: { strIdSession: SessionPMHFC.IDSESSION, idPlan: SessionPMHFC.CODIGOPLAN, idServicio: IdServicio, strProductType: SessionPMHFC.ProductType },
                width: 700,
                height: 400,
                buttons: {
                    Cancelar: {
                        click: function (sender, args) {
                            $.unblockUI();
                            this.close();
                            that.disabledSteps();
                        }
                    }
                }
            });
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
        InitCacDat: function () {
            var that = this,
                controls = that.getControls(),
                objCacDacType = {};

            that.Loading();

            objCacDacType.strIdSession = SessionPMHFC.USERACCESS.userId;
            var parameters = {};
            parameters.strIdSession = SessionPMHFC.USERACCESS.userId;
            parameters.strCodeUser = SessionPMHFC.USERACCESS.login;
            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(parameters),
                url: '/Transactions/CommonServices/GetUsers',
                error: function (xhr, status, error) {
                    //console.logxhr);
                },
                success: function (results) {
                    var cacdac = results.Cac;
                    //console.log"cacdac: " + cacdac);
                    $.ajax({
                        type: 'POST',
                        contentType: "application/json; charset=utf-8",
                        dataType: 'json',
                        data: JSON.stringify(objCacDacType),
                        url: '/Transactions/CommonServices/GetCacDacType',
                        success: function (response) {
                            controls.cboCacDac.append($('<option>', { value: '', html: 'Seleccionar' }));
                            if (response.data != null) { }
                            var itemSelect;
                            $.each(response.data.CacDacTypes, function (index, value) {

                                if (cacdac === value.Description) {
                                    controls.cboCacDac.append($('<option>', { value: value.Code, html: value.Description }));
                                    itemSelect = value.Code;

                                } else {
                                    controls.cboCacDac.append($('<option>', { value: value.Code, html: value.Description }));
                                }
                            });
                            if (itemSelect != null && itemSelect.toString != "undefined") {
                                //console.log"valor itemSelect: " + itemSelect);
                                $("#cboCacDac option[value=" + itemSelect + "]").attr("selected", true);
                            }
                        }
                    });
                }
            });
        },
        AddTotabla360CandidatePlanCoresCable: function (arrayCable) {

            var that = this;
            var controls = that.getControls();

            var ncable = parseInt(controls.CableEquipmentQty.text());


            controls.lstSelectPlanCable.html('');
            var coreSeleccionado = arrayCable[0];
            var coreDescripcion = ""; var coreId = ""; var coreEquipo = "";
            var coreIdentificador = coreDescripcion + coreId + coreEquipo;

            if (coreSeleccionado != null) {
                coreDescripcion = coreSeleccionado[1];
                coreId = coreSeleccionado[3];
                coreEquipo = coreSeleccionado[5];
                coreIdentificador = coreDescripcion + coreId + coreEquipo;
                coreIdentificador = coreIdentificador.replace(/&nbsp;/g, " ");
            }

            if (arrayCable.length > 0) {
                SessionPMHFC.OneCoreCable = 1;
                controls.btn_selTeam.removeAttr('disabled');
                controls.lstSelectPlanCable.append('<li class="transac-list-group-item"><span class="badge">S/ ' + coreSeleccionado[17] + '</span> ' + coreSeleccionado[1] + '</li>');
                var serviceByPlan = {};
                serviceByPlan.CF = coreSeleccionado[2];
                serviceByPlan.CodServSisact = coreSeleccionado[3];
                serviceByPlan.DesServSisact = coreSeleccionado[1];
                serviceByPlan.ServiceType = coreSeleccionado[4];
                serviceByPlan.Sncode = coreSeleccionado[12];
                serviceByPlan.Spcode = coreSeleccionado[13];
                serviceByPlan.CantEquipment = coreSeleccionado[6];
                serviceByPlan.Equipment = coreSeleccionado[5];
                serviceByPlan.CodGroupServ = coreSeleccionado[7];
                serviceByPlan.Codtipequ = coreSeleccionado[9];
                serviceByPlan.Tipequ = coreSeleccionado[14];
                serviceByPlan.Tmcode = coreSeleccionado[15];
                serviceByPlan.CodPlanSisact = coreSeleccionado[8];
                serviceByPlan.IDEquipment = coreSeleccionado[16];
                serviceByPlan.GroupServ = coreSeleccionado[10];


                that.listCoreServices.push(serviceByPlan);
                //    console.log('Core(CTV):'); console.log(serviceByPlan); //Core Seleccionado 
                var servicesForResume = {};
                servicesForResume.Servicio = "Cable";
                servicesForResume.Tipo = coreSeleccionado[4];
                servicesForResume.NombreServicio = coreSeleccionado[1];
                servicesForResume.CF = coreSeleccionado[2];
                servicesForResume.CfWithIgv = coreSeleccionado[17];

                controls.lstResumenPlanNuevo.append('<li class="transac-list-group-item"><div class="row"><div class="col-sm-2">' + servicesForResume.Servicio + '</div><div class="col-sm-2">' + servicesForResume.Tipo + '</div><div class="col-sm-6">' + servicesForResume.NombreServicio + '</div><div class="col-sm-2"><span class="badge transac-badge-row">' + servicesForResume.CfWithIgv + '</span></div></div></li>');

                that.f_SumaMontos("CORE", coreSeleccionado[2], "");
                ncable++;

            } else {
                controls.btn_selTeam.attr('disabled', true);
            }

            $.each(that.listAllServicesByPlan, function (index, item) {
                //var descripcion = item.DesServSisact;
                //var id = item.CodServSisact;
                //var equipo = "";
                //if (item.Equipment == null) {
                //    equipo = "";
                //}
                //else {
                //    equipo = item.Equipment;
                //}
                //var identificador = descripcion + id + equipo;
                //if (coreIdentificador.trim() == identificador.trim() && item.CodServiceType == SessionPMHFC.CoreId) {
                if (SessionPMHFC.strHFCGroupCable.indexOf(item.CodGroupServ) > 0 && item.CodServiceType == SessionPMHFC.CoreAdicionalId) {
                    controls.lstSelectPlanCable.append('<li class="transac-list-group-item"><span class="badge">S/ ' + item.CfWithIgv + '</span> ' + item.DesServSisact + '</li>');

                    var serviceByPlan = {};
                    serviceByPlan.CF = item.CF;
                    serviceByPlan.CodServSisact = item.CodServSisact;
                    serviceByPlan.DesServSisact = item.DesServSisact;
                    serviceByPlan.ServiceType = item.ServiceType;
                    serviceByPlan.Sncode = item.Sncode;
                    serviceByPlan.Spcode = item.Spcode;
                    serviceByPlan.CantEquipment = item.CantEquipment;
                    serviceByPlan.Equipment = item.Equipment;
                    serviceByPlan.CodGroupServ = item.CodGroupServ;
                    serviceByPlan.Codtipequ = item.Codtipequ;
                    serviceByPlan.Tipequ = item.Tipequ;
                    serviceByPlan.Tmcode = item.Tmcode;
                    serviceByPlan.CodPlanSisact = item.CodPlanSisact;
                    serviceByPlan.IDEquipment = item.IDEquipment;
                    serviceByPlan.GroupServ = item.GroupServ;
                    //  console.log('CoreAdicional(CTV):');console.log(serviceByPlan); // Core Adicional del Core Principal
                    that.listCoreServices.push(serviceByPlan);

                    var servicesForResume = {};
                    servicesForResume.Servicio = "Cable";
                    servicesForResume.Tipo = item.ServiceType;
                    servicesForResume.NombreServicio = item.DesServSisact;
                    servicesForResume.CF = item.CF;
                    servicesForResume.CfWithIgv = item.CfWithIgv;

                    controls.lstResumenPlanNuevo.append('<li class="transac-list-group-item"><div class="row"><div class="col-sm-2">' + servicesForResume.Servicio + '</div><div class="col-sm-2">' + servicesForResume.Tipo + '</div><div class="col-sm-6">' + servicesForResume.NombreServicio + '</div><div class="col-sm-2"><span class="badge transac-badge-row">' + servicesForResume.CfWithIgv + '</span></div></div></li>');
                    that.f_SumaMontos("CORE-ADICIONAL", item.CF, "");
                    ncable++;

                }
            });
            var n = $("#lstSelectPlanCable li:contains('Seleccionar un plan')").length;
            if (controls.lstSelectPlanCable.children().length == 1 && n > 0) {
                controls.lstSelectPlanCable.html('');
                controls.lstSelectPlanCable.append('<li class="transac-list-group-item transac-message-red text-center"> No existen registros </li>');
            }
        },
        AddTotabla360CandidatePlanCoresInternet: function (arrayInternet) {
            var that = this;
            var controls = that.getControls();

            //if (arrayInternet.length > 0 && parseInt(controls.InternetEquipmentQty.text()) == 0) { 
            //}
            var ninternet = parseInt(controls.InternetEquipmentQty.text());
            //var filaCargarPlan = $("#lstSelectPlanInternet li:contains('Seleccionar un plan')").length;
            //var filaNoHayResultados = $("#lstSelectPlanInternet li:contains('No existen registros')").length;
            //if (filaCargarPlan > 0 || filaNoHayResultados > 0) {
            //    controls.lstSelectPlanInternet.html('');
            //}
            controls.lstSelectPlanInternet.html('');
            var coreSeleccionado = arrayInternet[0];
            //console.log("el coreseleccionado de internet");
            //console.log(coreSeleccionado);
            var coreDescripcion = ""; var coreId = ""; var coreEquipo = "";
            var coreIdentificador = coreDescripcion + coreId + coreEquipo;

            if (coreSeleccionado != null) {
                coreDescripcion = coreSeleccionado[1];
                coreId = coreSeleccionado[3];
                coreEquipo = coreSeleccionado[5];
                //console.log("dentro de un coreSeleccioando no null");
                //console.log(coreDescripcion);
                //console.log(coreId);
                //console.log(coreEquipo);
                coreIdentificador = coreDescripcion + coreId + coreEquipo;
                coreIdentificador = coreIdentificador.replace(/&nbsp;/g, " ");
            }
            //console.log("las listas de todos los servicios");
            //console.log(that.listAllServicesByPlan);
            if (arrayInternet.length > 0) {
                SessionPMHFC.OneCoreInternet = 1;
                controls.lstSelectPlanInternet.append('<li class="transac-list-group-item"><span class="badge">S/ ' + coreSeleccionado[17] + '</span> ' + coreSeleccionado[1] + '</li>');
                var serviceByPlan = {};
                serviceByPlan.CF = coreSeleccionado[2];
                serviceByPlan.CodServSisact = coreSeleccionado[3];
                serviceByPlan.DesServSisact = coreSeleccionado[1];
                serviceByPlan.ServiceType = coreSeleccionado[4];
                serviceByPlan.Sncode = coreSeleccionado[12];
                serviceByPlan.Spcode = coreSeleccionado[13];
                serviceByPlan.CantEquipment = coreSeleccionado[6];
                serviceByPlan.Equipment = coreSeleccionado[5];
                serviceByPlan.CodGroupServ = coreSeleccionado[7];
                serviceByPlan.Codtipequ = coreSeleccionado[9];
                serviceByPlan.Tipequ = coreSeleccionado[14];
                serviceByPlan.Tmcode = coreSeleccionado[15];
                serviceByPlan.CodPlanSisact = coreSeleccionado[8];
                serviceByPlan.IDEquipment = coreSeleccionado[16];
                serviceByPlan.GroupServ = coreSeleccionado[10];
                //  console.log('Core(INT):'); console.log(serviceByPlan); //Core Seleccionado
                that.listCoreServices.push(serviceByPlan);

                var servicesForResume = {};
                servicesForResume.Servicio = "Internet";
                servicesForResume.Tipo = coreSeleccionado[4];
                servicesForResume.NombreServicio = coreSeleccionado[1];
                servicesForResume.CF = coreSeleccionado[2];
                servicesForResume.CfWithIgv = coreSeleccionado[17];

                controls.lstResumenPlanNuevo.append('<li class="transac-list-group-item"><div class="row"><div class="col-sm-2">' + servicesForResume.Servicio + '</div><div class="col-sm-2">' + servicesForResume.Tipo + '</div><div class="col-sm-6">' + servicesForResume.NombreServicio + '</div><div class="col-sm-2"><span class="badge transac-badge-row">' + servicesForResume.CfWithIgv + '</span></div></div></li>');

                that.f_SumaMontos("CORE", coreSeleccionado[2], "");

                ninternet++;
            }

            $.each(that.listAllServicesByPlan, function (index, item) {
                //var descripcion = item.DesServSisact;
                //var id = item.CodServSisact;
                //var equipo = "";
                //if (item.Equipment == null) {
                //    equipo = "";
                //}
                //else {
                //    equipo = item.Equipment;
                //}
                //var identificador = descripcion + id + equipo;
                //console.log(coreIdentificador);
                //console.log(identificador);
                if (SessionPMHFC.strHFCGroupInternet.indexOf(item.CodGroupServ) > 0 && item.CodServiceType == SessionPMHFC.CoreAdicionalId) {
                    controls.lstSelectPlanInternet.append('<li class="transac-list-group-item"><span class="badge">S/ ' + item.CfWithIgv + '</span> ' + item.DesServSisact + '</li>');


                    var serviceByPlan = {};
                    serviceByPlan.CF = item.CF;
                    serviceByPlan.CodServSisact = item.CodServSisact;
                    serviceByPlan.DesServSisact = item.DesServSisact;
                    serviceByPlan.ServiceType = item.ServiceType;
                    serviceByPlan.Sncode = item.Sncode;
                    serviceByPlan.Spcode = item.Spcode;
                    serviceByPlan.CantEquipment = item.CantEquipment;
                    serviceByPlan.Equipment = item.Equipment;
                    serviceByPlan.CodGroupServ = item.CodGroupServ;
                    serviceByPlan.Codtipequ = item.Codtipequ;
                    serviceByPlan.Tipequ = item.Tipequ;
                    serviceByPlan.Tmcode = item.Tmcode;
                    serviceByPlan.CodPlanSisact = item.CodPlanSisact;
                    serviceByPlan.IDEquipment = item.IDEquipment;
                    serviceByPlan.GroupServ = item.GroupServ;
                    //   console.log('CoreAdicional(INT):'); console.log(serviceByPlan); // Core Adicional del Core Principal
                    that.listCoreServices.push(serviceByPlan);


                    var servicesForResume = {};
                    servicesForResume.Servicio = "Internet";
                    servicesForResume.Tipo = item.ServiceType;
                    servicesForResume.NombreServicio = item.DesServSisact;
                    servicesForResume.CF = item.CF;
                    servicesForResume.CfWithIgv = item.CfWithIgv;

                    controls.lstResumenPlanNuevo.append('<li class="transac-list-group-item"><div class="row"><div class="col-sm-2">' + servicesForResume.Servicio + '</div><div class="col-sm-2">' + servicesForResume.Tipo + '</div><div class="col-sm-6">' + servicesForResume.NombreServicio + '</div><div class="col-sm-2"><span class="badge transac-badge-row">' + servicesForResume.CfWithIgv + '</span></div></div></li>');
                    that.f_SumaMontos("CORE", item.CF, "");

                    ninternet++;
                }
            });
            var n = $("#lstSelectPlanInternet li:contains('Seleccionar un plan')").length;
            if (controls.lstSelectPlanInternet.children().length == 1 && n > 0) {
                controls.lstSelectPlanInternet.html('');

                controls.lstSelectPlanInternet.append('<li class="transac-list-group-item transac-message-red text-center"> No existen registros </li>');
            }

        },
        AddTotabla360CandidatePlanCoresPhone: function (arrayPhone) {
            var that = this;
            var controls = that.getControls();

            //if (arrayPhone.length > 0 && parseInt(controls.PhoneEquipmentQty.text()) == 0) {
            //}
            var nphone = parseInt(controls.PhoneEquipmentQty.text());
            //var filaCargarPlan = $("#lstSelectPlanTelephony li:contains('Seleccionar un plan')").length;
            //var filaNoHayResultados = $("#lstSelectPlanTelephony li:contains('No existen registros')").length;
            //if (filaCargarPlan > 0 || filaNoHayResultados > 0) {
            //    controls.lstSelectPlanTelephony.html('');
            //}
            controls.lstSelectPlanTelephony.html('');
            var coreSeleccionado = arrayPhone[0];
            var coreDescripcion = ""; var coreId = ""; var coreEquipo = "";
            var coreIdentificador = coreDescripcion + coreId + coreEquipo;

            if (coreSeleccionado != null) {
                coreDescripcion = coreSeleccionado[1];
                coreId = coreSeleccionado[3];
                coreEquipo = coreSeleccionado[5];
                coreIdentificador = coreDescripcion + coreId + coreEquipo;
                coreIdentificador = coreIdentificador.replace(/&nbsp;/g, " ");
            }
            if (arrayPhone.length > 0) {
                SessionPMHFC.OneCorePhone = 1;
                controls.lstSelectPlanTelephony.append('<li class="transac-list-group-item"><span class="badge">S/ ' + coreSeleccionado[17] + '</span> ' + coreSeleccionado[1] + '</li>');
                var serviceByPlan = {};
                serviceByPlan.CF = coreSeleccionado[2];
                serviceByPlan.CodServSisact = coreSeleccionado[3];
                serviceByPlan.DesServSisact = coreSeleccionado[1];
                serviceByPlan.ServiceType = coreSeleccionado[4];
                serviceByPlan.Sncode = coreSeleccionado[12];
                serviceByPlan.Spcode = coreSeleccionado[13];
                serviceByPlan.CantEquipment = coreSeleccionado[6];
                serviceByPlan.Equipment = coreSeleccionado[5];
                serviceByPlan.CodGroupServ = coreSeleccionado[7];
                serviceByPlan.Codtipequ = coreSeleccionado[9];
                serviceByPlan.Tipequ = coreSeleccionado[14];
                serviceByPlan.Tmcode = coreSeleccionado[15];
                serviceByPlan.CodPlanSisact = coreSeleccionado[8];
                serviceByPlan.IDEquipment = coreSeleccionado[16];
                serviceByPlan.GroupServ = coreSeleccionado[10];
                //  console.log('Core(TLF):'); console.log(serviceByPlan); //Core Seleccionado
                that.listCoreServices.push(serviceByPlan);
                var servicesForResume = {};
                servicesForResume.Servicio = "Teléfono";
                servicesForResume.Tipo = coreSeleccionado[4];
                servicesForResume.NombreServicio = coreSeleccionado[1];
                servicesForResume.CF = coreSeleccionado[2];
                servicesForResume.CfWithIgv = coreSeleccionado[17];

                controls.lstResumenPlanNuevo.append('<li class="transac-list-group-item"><div class="row"><div class="col-sm-2">' + servicesForResume.Servicio + '</div><div class="col-sm-2">' + servicesForResume.Tipo + '</div><div class="col-sm-6">' + servicesForResume.NombreServicio + '</div><div class="col-sm-2"><span class="badge transac-badge-row">' + servicesForResume.CfWithIgv + '</span></div></div></li>');
                SessionPMHFC.HAYSERVICIOCORETELEFONO = 1;
                that.f_habilitaDeshabilitaDertalleTelefonia(true);

                that.f_SumaMontos("CORE", coreSeleccionado[2], "");

                nphone++;
            }
            $.each(that.listAllServicesByPlan, function (index, item) {
                //var descripcion = item.DesServSisact;
                //var id = item.CodServSisact;
                //var equipo = "";
                //if (item.Equipment == null) {
                //    equipo = "";
                //}
                //else {
                //    equipo = item.Equipment;
                //} 
                //var identificador = descripcion + id + equipo;

                if (SessionPMHFC.strHFCGroupTelephony.indexOf(item.CodGroupServ) > 0 && item.CodServiceType == SessionPMHFC.CoreAdicionalId) {
                    controls.lstSelectPlanTelephony.append('<li class="transac-list-group-item"><span class="badge">S/ ' + item.CfWithIgv + '</span> ' + item.DesServSisact + '</li>');

                    var serviceByPlan = {};
                    serviceByPlan.CF = item.CF;
                    serviceByPlan.CodServSisact = item.CodServSisact;
                    serviceByPlan.DesServSisact = item.DesServSisact;
                    serviceByPlan.ServiceType = item.ServiceType;
                    serviceByPlan.Sncode = item.Sncode;
                    serviceByPlan.Spcode = item.Spcode;
                    serviceByPlan.CantEquipment = item.CantEquipment;
                    serviceByPlan.Equipment = item.Equipment;
                    serviceByPlan.CodGroupServ = item.CodGroupServ;
                    serviceByPlan.Codtipequ = item.Codtipequ;
                    serviceByPlan.Tipequ = item.Tipequ;
                    serviceByPlan.Tmcode = item.Tmcode;
                    serviceByPlan.CodPlanSisact = item.CodPlanSisact;
                    serviceByPlan.IDEquipment = item.IDEquipment;
                    serviceByPlan.GroupServ = item.GroupServ;
                    //  console.log('CoreAdicional(TLF):'); console.log(serviceByPlan); // Core Adicional del Core Principal
                    that.listCoreServices.push(serviceByPlan);

                    var servicesForResume = {};
                    servicesForResume.Servicio = "Teléfono";
                    servicesForResume.Tipo = item.ServiceType;
                    servicesForResume.NombreServicio = item.DesServSisact;
                    servicesForResume.CF = item.CF;
                    servicesForResume.CfWithIgv = item.CfWithIgv;

                    controls.lstResumenPlanNuevo.append('<li class="transac-list-group-item"><div class="row"><div class="col-sm-2">' + servicesForResume.Servicio + '</div><div class="col-sm-2">' + servicesForResume.Tipo + '</div><div class="col-sm-6">' + servicesForResume.NombreServicio + '</div><div class="col-sm-2"><span class="badge transac-badge-row">' + servicesForResume.CfWithIgv + '</span></div></div></li>');
                    that.f_SumaMontos("CORE", item.CF, "");

                    nphone++;
                }
            });
            var n = $("#lstSelectPlanTelephony li:contains('Seleccionar un plan')").length;
            if (controls.lstSelectPlanTelephony.children().length == 1 && n > 0) {
                controls.lstSelectPlanTelephony.html('');
                controls.lstSelectPlanTelephony.append('<li class="transac-list-group-item transac-message-red text-center"> No existen registros </li>');
            }

        },
        f_SumaMontos: function (tipo, monto, tipoAdi) {
            var that = this;
            if (tipo == "CORE") {
                var m = $("#lblMontoBase").text();
                var montoF = parseFloat(m.slice(3, m.length));
                montoF = montoF + parseFloat(monto);
                $("#lblMontoBase").text('S/ ' + montoF.toFixed(2));
            }
            else {
                var ma = $("#lblMontoAdicional").text();
                var montoA = parseFloat(ma.slice(3, ma.length));
                montoA = montoA + parseFloat(monto);
                $("#lblMontoAdicional").text('S/ ' + montoA.toFixed(2));

            }
            var msi = $("#lblCargoFijoTotalPlanSIGV").text();
            var montoSIGV = parseFloat(msi.slice(3, msi.length));
            montoSIGV = montoSIGV + parseFloat(monto);
            $("#lblCargoFijoTotalPlanSIGV").text('S/ ' + montoSIGV.toFixed(2));
            var montoVarConIGV = parseFloat(monto) * parseFloat(that.strIGV);
            montoVarConIGV = montoVarConIGV.toFixed(2);
            montoVarConIGV = parseFloat(montoVarConIGV);
            var mci = $("#lblCargoFijoTotalPlanCIGV").text();
            var montoCIGV = parseFloat(mci.slice(3, mci.length));
            montoCIGV = montoCIGV + montoVarConIGV;
            $("#lblCargoFijoTotalPlanCIGV").text('S/ ' + montoCIGV.toFixed(2));

            var cantidadS = parseInt($("#lblCantidad").text());
            cantidadS = cantidadS + 1;
            $("#lblCantidad").text(cantidadS);

            if (tipoAdi == "TEAM") {
                var cantidadE = parseInt($("#lblCantidadEquipos").text());
                cantidadE = cantidadE + 1;
                $("#lblCantidadEquipos").text(cantidadE);
            }
        },
        f_AddAditionalService: function (type) {
            var totalEquipment = parseInt($("#" + type + "EquipmentQty").text());
            totalEquipment = totalEquipment + 1;
            $("#" + type + "EquipmentQty").text(totalEquipment);
        },
        f_AddDecoQuantity: function () {
            var totalEquipment = parseInt($("#DecosQuantity").text());
            totalEquipment = totalEquipment + 1;
            $("#DecosQuantity").text(totalEquipment);

        },
        f_RemoveDecoQuantity: function (type) {
            var total = parseInt($("#DecosQuantity").text());
            total = total - 1;
            $("#DecosQuantity").text(total);
        },
        f_RemoveAditionalService: function (type) {
            var totalAditionalServices = parseInt($("#" + type + "EquipmentQty").text());
            totalAditionalServices = totalAditionalServices - 1;
            $("#" + type + "EquipmentQty").text(totalAditionalServices);
        },
        f_AddEquipment: function (quantity, label) {
            var total = parseInt(label.text());
            total = total + parseInt(quantity);
            label.text(total);
        },
        f_habilitaDeshabilitaDertalleTelefonia: function (flag1) {
            var flag = !flag1;
            $("#chkPresuscrito").prop("disabled", flag);
            $("#txtNroCarta").prop("disabled", flag);
            if ($("#chkPresuscrito").prop("checked")) {
                $("#sltOperator").prop("disabled", false);
                $("#sltOperator").val("-1");
            } else {
                $("#sltOperator").prop("disabled", true);
                $("#sltOperator").val("-1");
            }
            $("#chkPublicar").prop("disabled", flag);

            if (!flag) {
                $("#chkPresuscrito").prop("checked", false);
                $("#txtNroCarta").val("");
                $("#sltOperator").val("-1");
                $("#chkPublicar").prop("checked", false);
            }

        },
        ocultarCorreo: function (sender, arg) {
            var that = this,
                controls = that.getControls();

            if (sender.prop("checked")) {
                controls.txtHFC_SendforEmail.prop("disabled", false);
            } else {
                controls.txtHFC_SendforEmail.prop("disabled", true);
                controls.txtHFC_SendforEmail.html("");
            }
        },
        getControls: function () {
            return this.m_controls || {};
        },
        setControles: function (value) {
            this.m_controls = value;
        },
        f_ListaEquipos: function () {
            var that = this;
            var idplan = SessionPMHFC.CODIGOPLAN;
            var strUrl = "/Transactions/HFC/PlanMigration/_ListServicesByPlan";
            $.window.open({
                modal: true,
                type: "POST",
                title: "Equipos en alquiler",
                url: strUrl,
                data: { strIdSession: SessionPMHFC.IDSESSION.toString(), idPlan: idplan, strProductType: SessionPMHFC.ProductType, strSelectEquipments: JSON.stringify(this.listAditionalRentServices) },
                width: 700,
                height: 400,
                buttons: {
                    Seleccionar: {/*btnAgregaEquipo*/
                        click: function () {

                            var selectedDecos = $('#tblResumenRE').DataTable().rows({ selected: true }).data();

                            var selectedDecosTotal = $('#tblResumenRE').DataTable().rows().data();
                            if (that.f_getDecosListedQuantity(controls.lstSelectEquipCable.selector) + selectedDecos.length > 5) {
                                alert("Solo se permite seleccionar un máximo de 5 equipos.", "Alerta");
                                return;
                            }

                            var arrayDecos = selectedDecos[0];
                            var cantDecos = selectedDecosTotal.length;
                            if (cantDecos > 0 && arrayDecos === undefined) {
                                alert("Necesita seleccionar un equipo en alquiler.", "Alerta");
                                return false;
                            }

                            arrayDecos = [];
                            for (var i = 0; i < selectedDecos.length; i++) {
                                arrayDecos.push(selectedDecos[i])
                            }


                            var item = selectedDecos[0];
                            if (item != null) {
                                that.f_setEquipoAlquilerSeleccionado(item[3] + item[6] + item[1]);
                                if (SessionPMHFC.CODEQUIPALSELEC == "") {
                                    alert("Necesita seleccionar un equipo de alquiler.", "Alerta");
                                    return false;
                                }
                                that.f_addRentalEquipment(arrayDecos);
                            }
                            this.close();
                            that.disabledSteps();
                        }
                    },
                    Cancelar: {
                        click: function (sender, args) {
                            $.unblockUI();
                            this.close();
                            that.disabledSteps();
                        }
                    }
                }
            });
        },
        f_getDecosListedQuantity: function (listDecosInTable) {
            var items = $(listDecosInTable).find('li').map(function () {
                var item = {};
                item.quantity = $(this).find("span").text() == "" ? 0 : $(this).find("span").text();
                item.title = $(this).text();
                return item;
            });
            var total = 0;
            $.each($(items), function (index, item) {
                total = total + parseInt(item.quantity);
            });
            return total;
        },
        f_setEquipoAlquilerSeleccionado: function (identificador) {
            SessionPMHFC.CODEQUIPALSELEC = identificador;
        },
        f_addRentalEquipment: function (equipments) {
            //alert('f_addRentalEquipment');
            //console.log("equipmentsequipmentsequipments");
            //  console.log("Deco Adicional Agregado:");
            //  console.log(equipments);
            var that = this;
            var controls = that.getControls();
            var currentIndex = parseInt($("#DecosQuantity").text());
            $.each(equipments, function (index, equipment) {
                var serviceByPlan = {};
                serviceByPlan.DesServSisact = equipment[1];
                serviceByPlan.CF = equipment[2];
                serviceByPlan.Equipment = equipment[3];
                serviceByPlan.CantEquipment = equipment[4];
                serviceByPlan.CodServSisact = equipment[6];
                serviceByPlan.ServiceType = equipment[7];
                serviceByPlan.Sncode = equipment[8];
                serviceByPlan.Spcode = equipment[9];
                serviceByPlan.CodGroupServ = equipment[10];
                serviceByPlan.Codtipequ = equipment[11];
                serviceByPlan.Tipequ = equipment[12];
                serviceByPlan.Tmcode = equipment[13];
                serviceByPlan.CodPlanSisact = equipment[14];
                serviceByPlan.IDEquipment = equipment[15];
                serviceByPlan.CfWithIgv = equipment[16];
                that.listAditionalRentServices.push(serviceByPlan);

                controls.lstSelectPlanCable.append('<li class="transac-list-group-item"><button id="close_' + equipment[6] + currentIndex + '" type="button" class="transac-close"  data-toggle="tooltip" title="Quitar de la lista"><span>&times;</span></button><span class="badge">S/ ' + equipment[16] + '</span> ' + equipment[1] + '</li>');
                controls.lstSelectEquipCable.append('<li class="transac-list-group-item" id="close_equ_' + equipment[6] + currentIndex + '"><span class="badge">1</span> ' + equipment[1] + '</li>');

                // Bloque - Agregar Nuevos Decos de ListEquipmentsCTV
                if (SessionPMHFC.ListEquipmentsCTV == null) SessionPMHFC.ListEquipmentsCTV = [];
                serviceByPlan.Solution = 'close_equ_' + equipment[6] + currentIndex;
                SessionPMHFC.ListEquipmentsCTV.push(serviceByPlan);
                // Fin Bloque

                var servicesForResume = {};
                servicesForResume.Servicio = "Equipos-Alquiler";
                servicesForResume.Tipo = equipment[7];
                servicesForResume.NombreServicio = equipment[1];
                servicesForResume.CF = equipment[2];
                servicesForResume.CodServSisact = equipment[6];
                servicesForResume.CfWithIgv = equipment[16];

                controls.lstResumenPlanNuevo.append('<li id="close_' + equipment[6] + currentIndex + '" class="transac-list-group-item"><div class="row"><div class="col-sm-2">' + servicesForResume.Servicio + '</div><div class="col-sm-2">' + servicesForResume.Tipo + '</div><div class="col-sm-6">' + servicesForResume.NombreServicio + '</div><div class="col-sm-2"><span class="badge transac-badge-row">' + servicesForResume.CfWithIgv + '</span></div></div></li>');


                that.f_AddDecoQuantity("EQUIPO-ALQUILER");
                that.f_SumaMontos("EQUIPO-ALQUILER", equipment[2], "TEAM");

                var equipo = equipment[6];
                var index = currentIndex;
                $("#close" + "_" + equipo + index).click(function () {

                    $(this).closest('li').remove();
                    $("#lstSelectEquipCable #close_equ_" + equipo + index).remove();

                    $("#lstResumenPlanNuevo #close_" + equipo + index).remove();

                    that.disabledSteps();
                    that.f_RestaMontos("EQUIPO-ALQUILER", equipment[2]);
                    that.f_RemoveDecoQuantity("EQUIPO-ALQUILER");
                    that.listDecosByPlan = $.grep(that.listDecosByPlan, function (el, idx) { return el.Id == value.CodServSisact }, true);
                    if (controls.lstSelectEquipCable.children().length == 0) {
                        controls.lstSelectEquipCable.append('<li class="transac-list-group-item transac-message-red text-center">No existen registros</li>');
                    }



                    var indexEquip = -1;
                    for (var i = 0, len = that.listAditionalRentServices.length ; i < len; i++) {
                        if (that.listAditionalRentServices[i].CodServSisact === equipment[6]) {
                            indexEquip = i;
                            break;
                        }
                    }


                    if (indexEquip > -1) {
                        that.listAditionalRentServices.splice(indexEquip, 1);
                    }
                    var cantidadEquip = parseInt($("#lblCantidadEquipos").text());
                    cantidadEquip = cantidadEquip - 1;
                    $("#lblCantidadEquipos").text(cantidadEquip);



                    // Bloque - Eliminar Decos de ListEquipmentsCTV
                    var indexOfList = -1;
                    var nameID = "close_equ_" + equipo + index;//$(this).attr('id');

                    for (i = 0; i < SessionPMHFC.ListEquipmentsCTV.length; i++) {
                        var equipmentCTV = SessionPMHFC.ListEquipmentsCTV[i];

                        if (equipmentCTV.Solution == nameID) {
                            indexOfList = i;
                            break;
                        }
                    }

                    SessionPMHFC.ListEquipmentsCTV.splice(indexOfList, 1);

                    // Fin Bloque
                });
                currentIndex++;

            });

        },
        f_RestaMontos: function (tipo, monto) {
            var that = this;
            if (tipo == "CORE") {
                var m = $("#lblMontoBase").text();
                var montoF = parseFloat(m.slice(3, m.length));
                montoF = montoF - parseFloat(monto);
                $("#lblMontoBase").text('S/ ' + montoF.toFixed(2));
            }
            else {
                var ma = $("#lblMontoAdicional").text();
                var montoA = parseFloat(ma.slice(3, ma.length));
                montoA = montoA - parseFloat(monto);
                $("#lblMontoAdicional").text('S/ ' + montoA.toFixed(2));

            }
            var msi = $("#lblCargoFijoTotalPlanSIGV").text();
            var montoSIGV = parseFloat(msi.slice(3, msi.length));
            montoSIGV = montoSIGV - parseFloat(monto);
            $("#lblCargoFijoTotalPlanSIGV").text('S/ ' + montoSIGV.toFixed(2));

            var montoVarConIGV = parseFloat(monto) * parseFloat(that.strIGV);
            montoVarConIGV = montoVarConIGV.toFixed(2);
            montoVarConIGV = parseFloat(montoVarConIGV);
            var mci = $("#lblCargoFijoTotalPlanCIGV").text();
            var montoCIGV = parseFloat(mci.slice(3, mci.length));
            montoCIGV = montoCIGV - montoVarConIGV;
            $("#lblCargoFijoTotalPlanCIGV").text('S/ ' + montoCIGV.toFixed(2));

            var cantidadS = parseInt($("#lblCantidad").text());
            cantidadS = cantidadS - 1;
            $("#lblCantidad").text(cantidadS);


            //var cantidadEquip = parseInt($("#lblCantidadEquipos").text());
            //cantidadEquip = cantidadEquip - 1;
            //$("#lblCantidadEquipos").text(cantidadEquip);

        },
        btnGuardar_Click: function () {
            var that = this;
            var controls = that.getControls();
            that.f_SetListasFinalesParaTransaccion();

            if ($("#txtReintegro").val() != "0" || $("#txtMontoFideliza").val() != "0") {

                if (SessionPMHFC.hdnAutorizacionIngMontos == "1") {
                    that.f_abreAutorizacionMontos();
                    return false;
                }
            }
            //console.log'CODMOTOT: ' + SessionPMHFC.CODMOTOT);
            //if (SessionPMHFC.CODMOTOT === null || SessionPMHFC.CODMOTOT == undefined) {
            //    alert("El código de motivo de visita es nulo, no se puede continuar.", "Alerta");
            //    return false;
            //}

            var tipi = {
                CLASE: that.strClase, CLASE_CODE: that.strClaseCode, INTERACCION_CODE: that.strInteraccionCode, SUBCLASE: that.strSubClase, SUBCLASE_CODE: that.strSubClaseCode, TIPO: that.strTipo, TIPO_CODE: that.strTipoCode
            };

            if (that.listServicesByPlan.length == 0) {
                alert("Necesita escoger un nuevo plan.", "Alerta");
                return;
            }
            var strPresuscritoStatus, strDdlOperatorStatus, strFidelizaFinalStatus, strOCCFinalStatus, strValidaETAStatus, strDdlOperator;
            if (controls.chkPresuscrito.is(":checked")) {
                strPresuscritoStatus = 1;
            }
            else {
                strPresuscritoStatus = 0;
            }
            if (!controls.sltOperator.is(":disabled")) {
                strDdlOperatorStatus = controls.sltOperator.val();
                strDdlOperator = controls.sltOperator.text();
            }
            else {
                strDdlOperatorStatus = "00";
                strDdlOperator = "AMERICA MOVIL DEL PERU SAC";
            }
            if (controls.chkFideliza.is(":checked")) {
                strFidelizaFinalStatus = 1;
            }
            else {
                strFidelizaFinalStatus = 0;
            }
            if (SessionPMHFC.strValidateETA == 2) {
                strValidaETAStatus = 2;
            }
            else if (SessionPMHFC.strValidateETA == 1) {
                strValidaETAStatus = 1;
            }
            else {
                strValidaETAStatus = 0;
            }

            var listParamConstancyPDF = CreateListParamConstancyPDF();
            var paramsSaved = {
                StrIdCustomer: SessionPMHFC.DATACUSTOMER.CustomerID,
                StrLogin: SessionPMHFC.USERACCESS.login,
                StrNotes: controls.txtNotas.val(),
                StrIdContract: SessionPMHFC.DATACUSTOMER.ContractID,
                StrPlanoInst: SessionPMHFC.DATACUSTOMER.PlaneCodeInstallation,
                StrBillingCycle: SessionPMHFC.DATACUSTOMER.objPostDataAccount.BillingCycle,
                StrActivationDate: SessionPMHFC.DATACUSTOMER.ActivationDate,
                StrTermContract: SessionPMHFC.DATASERVICE.TermContract,
                StrStateLine: SessionPMHFC.DATASERVICE.StateLine,
                StrExpirationDate: SessionPMHFC.DATACUSTOMER.objPostDataAccount.ExpirationDate,
                StrOfficeAddress: SessionPMHFC.DATACUSTOMER.OfficeAddress,
                StrCacDac: $("#cboCacDac option:selected").text(),
                StrLegalAddress: SessionPMHFC.DATACUSTOMER.LegalDepartament,
                StrLegalDistrict: SessionPMHFC.DATACUSTOMER.LegalDistrict,
                StrLegalCountry: SessionPMHFC.DATACUSTOMER.LegalCountry,
                StrLegalProvince: SessionPMHFC.DATACUSTOMER.LegalProvince,
                StrPlaneCodeInstallation: SessionPMHFC.DATACUSTOMER.PlaneCodeInstallation,
                StrPlan: SessionPMHFC.DATASERVICE.Plan,
                StrLegalUrbanization: SessionPMHFC.DATACUSTOMER.LegalUrbanization,
                StrDocumentNumber: SessionPMHFC.DATACUSTOMER.DocumentNumber,
                StrFullName: SessionPMHFC.DATACUSTOMER.FullName,
                StrLegalAgent: SessionPMHFC.DATACUSTOMER.LegalAgent,
                StrPresuscritoStatus: strPresuscritoStatus,
                StrNoLetter: controls.txtNroCarta.val(),
                StrDdlOperatorStatus: strDdlOperatorStatus,
                StrPublishFinalStatus: SessionPMHFC.hdnCheckPublicarFinal,
                StrReintegro: controls.txtReintegro.val(),
                StrMontoFideliza: controls.txtMontoFideliza.val(),
                StrTotalPenalty: controls.txtTotalPenalidad.val(),
                StrFidelizaFinalStatus: strFidelizaFinalStatus,
                StrOCCFinalStatus: SessionPMHFC.hdnCheckOCCFinal,
                StrDocumentType: SessionPMHFC.DATACUSTOMER.DocumentType,
                StrValidaETAStatus: strValidaETAStatus,
                StrCustomerContact: SessionPMHFC.DATACUSTOMER.BusinessName,
                StrContractID: SessionPMHFC.DATACUSTOMER.ContractID,
                StrHayServicioCoreTelefono: SessionPMHFC.HAYSERVICIOCORETELEFONO,
                StrFProgramacion: controls.txtFProgramacion.val(),
                StrCustomerType: SessionPMHFC.DATACUSTOMER.CustomerType,
                StrCargoFijoTotalPlanCIGV: $("#lblCargoFijoTotalPlanCIGV").text(),
                StrCustomerID: SessionPMHFC.DATACUSTOMER.CustomerID,
                StrFranjaHorariaETA: $("#ddlFranjaHoraria option:selected").val(),
                StrFranjaHora: $("#ddlFranjaHoraria option:selected").attr("idHorario"),
                StrSubTypeWork: $("#sltSubWorkType option:selected").val(),
                StrHdnListaFTMCode: SessionPMHFC.HdnListaFTMCode,
                StrHdnCodigoPlan: SessionPMHFC.CODIGOPLAN,
                StrEmail: controls.txtEmail.val(),
                StrCodMoTot: SessionPMHFC.CODMOTOT,
                ConstanceXml: that.ConstanceXml,
                TipoProducto: SessionPMHFC.UrlParams.SUREDIRECT,
                StrHdnValidaEta: SessionPMHFC.strValidateETA,
                StrDdlOperator: strDdlOperator,
                StrHdnRequestActId: Session.RequestActId,
                strVisitTecAnotaciones: SessionPMHFC.strVisitTecAnotaciones
                //StrHdnRequestActId: $("#hdnRequestActId").val()
            };
            //console.log'RequestActId: ' + Session.RequestActId);

            var flagcamp = 0;
            
            if (controls.hidCambioPlanProy140245 == 1 && controls.itemCamp != null && typeof controls.itemCamp != "undefined" )
            {
                var codeCampaing = controls.itemCamp.strCampaignCode;
                var ArrayCampColab = controls.hidCodCampaniaMig.split("|");

                i = ArrayCampColab.length;

                while (i--) {
                    if (ArrayCampColab[i] == codeCampaing) {

                        flagcamp = 1;

                        break;
                    }
                }
            }
            

            if (flagcamp == 1) listParamConstancyPDF.push($("#lblCampaniaColaborador").text());//25 CAMPAÑA colaborador   destino
            else listParamConstancyPDF.push("");//25 CAMPAÑA colaborador
            if (that.intCampania == 1) listParamConstancyPDF.push($("#lblCampania").text());//26 CAMPAÑA colaborador   origen
            else listParamConstancyPDF.push("");  //26 CAMPAÑA colaborador


            var data = {
                strIdSession: SessionPMHFC.IDSESSION,
                ServicesList: that.listServicesByPlan,
                tipification: tipi,
                paramSaved: paramsSaved,
                listParamConstancyPDF: listParamConstancyPDF
            };

            confirm(SessionPMHFC.strMessageSave, 'Confirmar', function (result) {
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
                            color: '#fff',
                        }
                    });
                    $.ajax({
                        type: "POST",
                        url: that.strUrl + "/Transactions/HFC/PlanMigration/SaveMigratedPlan",
                        contentType: "application/json;charset=utf-8",
                        dataType: "json",
                        data: JSON.stringify(data),
                        error: function (xhr, status, error) {
                            //console.logxhr);
                            $.unblockUI();
                        },
                        success: function (data) {
                            if (data.data.ConstancyRoute == null || data.data.ConstancyRoute == 'undefined') {
                                SessionPMHFC.RouteRecordPDF = '';

                            } else {
                                SessionPMHFC.RouteRecordPDF = data.data.ConstancyRoute;

                            }
                            if (data.data.result.substr(0, 5) == "Error") {
                                alert(data.data.result, "Alerta");
                            }
                            else {
                                if (controls.hidCambioPlanProy140245 == 1)
                                {
                                    that.intCampania = 0;
                                    that.ConsultCampaign();
                                    if (controls.SelectCampaniaColab != "" || that.intCampania == 1) {
                                        that.RegisterCampaign(controls.itemCampSelc);
                                    }
                                }

                                alert(data.data.result + "<br>" + data.CurrentDiscounts.Message, "Informativo");
                                $("#divlnknumsot").css("display", "block");
                                $("#lnknumsot").text(data.data.SotNumber);
                            }

                            $.unblockUI();
                            $("#btnGuardar").prop("disabled", true);
                            $("#btnConstancy").prop("disabled", false);

                        }
                    });
                }
            });
        },
        f_abreAutorizacionMontos: function () {

            SessionPMHFC.hdnTipoValidacion = "Monto";
            var pag = '1';
            var opcion = "HFC_IMMP";
            var co = "";
            var telefono = SessionPMHFC.DATASERVICE.CellPhone;

            var param =
            {
                "strIdSession": SessionPMHFC.IDSESSION,
                'pag': pag,
                'opcion': opcion,
                'co': co,
                'telefono': telefono
            };

            ValidateAccess(that, controls, opcion, '', '1', param, 'Fixed');

            return;
        },
        f_abreAutorizacionFidelizar: function () {
            SessionPMHFC.hdnTipoValidacion = "Fideliza";
            var pag = '1';
            var opcion = "HFC_PFMP";
            var co = "";
            var telefono = SessionPMHFC.DATASERVICE.CellPhone;
            var param =
            {
                "strIdSession": SessionPMHFC.IDSESSION,
                'pag': pag,
                'opcion': opcion,
                'co': co,
                'telefono': telefono
            };

            ValidateAccess(that, controls, opcion, '', '1', param, 'Fixed');
            //No se usa el hidden 'HidOpcion' Validar
            return;
        },
        FC_GrabarCommit: function () {
            if (SessionPMHFC.hdnTipoValidacion == "Fideliza") {
                alert("Se ha validado la autorizacion, ahora puede fidelizar.", "Alerta");
                SessionPMHFC.hdnAutorizacionFidelizar = "0";
            }
            if (SessionPMHFC.hdnTipoValidacion == "Monto") {
                alert("Se ha validado la autorizacion, ahora puede guardar la transacción.", "Alerta");
                SessionPMHFC.hdnAutorizacionIngMontos = "0";
            }
            SessionPMHFC.hdnTipoValidacion = "";
        },
        FC_Fallo: function () {
            alert("Ocurrió un error al validar el perfil.", "Alerta");
            SessionPMHFC.hdnTipoValidacion = "";
            return false;
        },
        FC_Cancelar: function () {
            SessionPMHFC.hdnTipoValidacion = "";
            return false;
        },
        f_Constancia: function () {
            var that = this,
                controls = that.getControls();
            if (SessionPMHFC.RouteRecordPDF != '') {
                var newRoute = SessionPMHFC.RouteRecordPDF.substring(SessionPMHFC.RouteRecordPDF.indexOf('SIACUNICO'));
                newRoute = newRoute.replace(new RegExp('/', 'g'), '\\');
                newRoute = SessionPMHFC.strServer + newRoute;
                ReadRecordSharedFile(SessionPMHFC.IDSESSION, newRoute);
            } else {
                alert('No se ha cargado correctamente el archivo de la constancia.', "Alerta");
            }

        },
        f_SetListasFinalesParaTransaccion: function () {
            var that = this;
            var oTable = that.listServicesByPlan;
            var i;
            var rowLength = oTable.length;
            var lista1 = "";
            var lista2 = "";
            var lista3 = "";
            var lista4 = "";
            var lista5 = "";
            var listaCargosFijos = "";

            $.each(that.listServicesByPlan, function (index, item) {
                var sncode = item.Sncode;
                var spcode = item.Spcode;
                var tipoeq = "";
                var tmcode = item.Tmcode;
                var codserv = item.CodServSisact;
                var cargoFijo = item.CF;

                if (index == 0) {
                    lista1 = sncode;
                    lista2 = spcode;
                    lista3 = tipoeq;
                    lista4 = tmcode;
                    lista5 = codserv;
                    listaCargosFijos = cargoFijo;
                } else {
                    lista1 = lista1 + ";" + sncode;
                    lista2 = lista2 + ";" + spcode;
                    lista3 = lista3 + ";" + tipoeq;
                    lista4 = tmcode;
                    lista5 = lista5 + "|" + codserv;
                    listaCargosFijos = listaCargosFijos + ";" + cargoFijo;
                }
            });
            //$("#hdnListaFTipoEquipos").val(lista3);
            //SessionPMHFC.HdnListaFTMCode = lista4;
            //$("#hdnListaFCodServ").val(lista5);
            //$("#hdnListaFSNCode").val(lista1);
            //$("#hdnListaFSPCode").val(lista2);
            //$("#hdnListaCargosFijos").val(listaCargosFijos);
            SessionPMHFC.HdnListaFTipoEquipos = lista3;
            SessionPMHFC.HdnListaFTMCode = lista4;
            SessionPMHFC.HdnListaFCodServ = lista5;
            SessionPMHFC.HdnListaFSNCode = lista1;
            SessionPMHFC.HdnListaFSPCode = lista2;
            SessionPMHFC.HdnListaCargosFijos = listaCargosFijos;
        },
        btn_selServ_Click: function () {
            //var urlBase = window.location.href;
            //urlBase = urlBase.substr(0, urlBase.lastIndexOf('/'));
            var that = this;
            var listAditionalServices = that.listAditionalServices;
            var dialog = $.window.open({
                modal: true,
                title: "Seleccionar Servicios Adicionales",
                url: '/Transactions/HFC/PlanMigration/ChooseServicesByPlan',
                data: { strIdSession: SessionPMHFC.IDSESSION.toString(), strIdCustomer: SessionPMHFC.DATACUSTOMER.CustomerID, idPlan: SessionPMHFC.CODIGOPLAN, strProductType: SessionPMHFC.ProductType, ServicesList: JSON.stringify(listAditionalServices), intOneCoreCable: SessionPMHFC.OneCoreCable, intOneCoreInternet: SessionPMHFC.OneCoreInternet, intOneCorePhone: SessionPMHFC.OneCorePhone },
                width: 1024,
                height: 600,
                buttons: {
                    Seleccionar: {/*btnAsignarPlan*/
                        click: function () {
                            var ModalConfirm = this;

                            var rowPostCable = $('#tblChooseServicesByPlanCable').DataTable().rows({ selected: true }).data();
                            var rowPostInternet = $('#tblChooseServicesByPlanInternet').DataTable().rows({ selected: true }).data();
                            var rowPostPhone = $('#tblChooseServicesByPlanPhone').DataTable().rows({ selected: true }).data();
                            var arrayCable = rowPostCable[0];
                            var arrayInternet = rowPostInternet[0];
                            var arrayPhone = rowPostPhone[0];
                            if (arrayCable === undefined && arrayInternet === undefined && arrayPhone === undefined) {
                                alert("Necesita seleccionar un servicio.", "Alerta");
                                return false;
                            }

                            arrayCable = [];
                            for (var i = 0; i < rowPostCable.length; i++) {
                                arrayCable.push(rowPostCable[i]);
                            }
                            arrayInternet = [];
                            for (var i = 0; i < rowPostInternet.length; i++) {
                                arrayInternet.push(rowPostInternet[i]);
                            }
                            arrayPhone = [];
                            for (var i = 0; i < rowPostPhone.length; i++) {
                                arrayPhone.push(rowPostPhone[i]);
                            }
                            confirm('Los adicionales seleccionados se cargarán, desea cargarlos?', 'Confirmar', function (result) {
                                if (result) {
                                    $.blockUI({
                                        message: '<div align="center"><img src="' + that.strUrlLogo + '" width="25" height="25" /> Cargando ... </div>',
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
                                    that.AddTotabla360CandidatePlanAdicionalesCable(arrayCable);
                                    that.AddTotabla360CandidatePlanAdicionalesInternet(arrayInternet);
                                    that.AddTotabla360CandidatePlanAdicionalesPhone(arrayPhone);
                                    $.unblockUI();

                                    ModalConfirm.close();
                                }
                            });
                            that.disabledSteps();
                        }
                    },
                    Cancelar: {
                        click: function (sender, args) {
                            $.unblockUI();
                            that.disabledSteps();
                            this.close();
                        }
                    }
                }
            });

        },
        AddTotabla360CandidatePlanAdicionalesCable: function (arrayCable) {
            //alert('AddTotabla360CandidatePlanAdicionalesCable');
            var that = this;
            var controls = that.getControls();
            //if (arrayCable.length > 0 && parseInt(controls.CableEquipmentQty.text()) == 0) {

            //}
            var ncable = parseInt(controls.CableEquipmentQty.text());
            var filaCargarPlan = $("#lstSelectPlanCable li:contains('Seleccionar un plan')").length;
            var filaNoHayResultados = $("#lstSelectPlanCable li:contains('No existen registros')").length;
            if (filaCargarPlan > 0 || filaNoHayResultados > 0) {
                controls.lstSelectPlanCable.html('');
            }
            $.each(arrayCable, function (index, item) {

                controls.lstSelectPlanCable.append('<li class="transac-list-group-item"><button id="close_' + item[3] + '" type="button" class="transac-close"  data-toggle="tooltip" title="Quitar de la lista"><span>&times;</span></button><span class="badge">S/ ' + item[16] + '</span> ' + item[1] + '</li>');
                var serviceByPlan = {};
                serviceByPlan.CF = item[2];
                serviceByPlan.CodServSisact = item[3];
                serviceByPlan.DesServSisact = item[1];
                serviceByPlan.ServiceType = item[4];
                serviceByPlan.Sncode = item[5];
                serviceByPlan.CodGroupServ = item[6];
                serviceByPlan.Codtipequ = item[7];
                serviceByPlan.Tipequ = item[8];
                serviceByPlan.Spcode = item[9];
                serviceByPlan.CantEquipment = item[10];
                serviceByPlan.Equipment = item[11];
                serviceByPlan.Tmcode = item[12];
                serviceByPlan.CodPlanSisact = item[13];
                serviceByPlan.IDEquipment = item[14];
                serviceByPlan.GroupServ = item[15];
                that.listAditionalServices.push(serviceByPlan);

                var servicesForResume = {};
                servicesForResume.Servicio = "Cable";
                servicesForResume.Tipo = item[4];
                servicesForResume.NombreServicio = item[1];
                servicesForResume.CF = item[2];
                servicesForResume.CfWithIgv = item[16];

                controls.lstResumenPlanNuevo.append('<li id="close_' + item[3] + '" class="transac-list-group-item"><div class="row"><div class="col-sm-2">' + servicesForResume.Servicio + '</div><div class="col-sm-2">' + servicesForResume.Tipo + '</div><div class="col-sm-6">' + servicesForResume.NombreServicio + '</div><div class="col-sm-2"><span class="badge transac-badge-row">' + servicesForResume.CfWithIgv + '</span></div></div></li>');
                that.f_AddAditionalService("Cable");
                that.f_SumaMontos("Cable", item[2], "SERV");
                ncable++;
                $("#close_" + item[3]).click(function () {

                    //ini ccv
                    //var searchTerm = item[3], index = -1;
                    //for (var i = 0, len = that.listAditionalServices.length ; i < len; i++) {
                    //    if (that.listAditionalServices[i].CodServSisact === searchTerm) {
                    //        index = i;
                    //        break;
                    //    }
                    //}
                    //if (index  === -1) {
                    //    console.log("resultado List a Eliminar");
                    //    console.log(that.listAditionalServices[index]);
                    //    that.listAditionalServices.splice[index, 1];
                    //    console.log("resultado List Final listAditionalServices: ");
                    //    console.log(that.listAditionalServices);
                    //}
                    //fin ccv


                    $(this).closest("li").remove();
                    $('#lstResumenPlanNuevo #close_' + item[3]).remove();
                    that.f_RestaMontos("Cable", item[2]);
                    that.f_RemoveAditionalService("Cable");
                    that.disabledSteps();

                    $.each(that.listAditionalServices, function (i, el) {
                        if (this.CodServSisact == item[3]) {
                            that.listAditionalServices.splice(i, 1);
                        }
                    });

                    if (controls.lstSelectPlanCable.children().length == 0) {
                        controls.lstSelectPlanCable.append('<li class="transac-list-group-item transac-message-red text-center"> No existen registros </li>');
                    }
                });
            });
            var n = $("#lstSelectPlanCable li:contains('Seleccionar un plan')").length;
            if (controls.lstSelectPlanCable.children().length == 1 && n > 0) {
                controls.lstSelectPlanCable.html('');
                controls.lstSelectPlanCable.append('<li class="transac-list-group-item transac-message-red text-center"> No existen registros </li>');
            }

        },
        AddTotabla360CandidatePlanAdicionalesInternet: function (arrayInternet) {
            var that = this;
            var controls = that.getControls();

            //if (arrayInternet.length > 0 && parseInt(controls.InternetEquipmentQty.text()) == 0) {
            //}
            var ninternet = parseInt(controls.InternetEquipmentQty.text());
            $.each(arrayInternet, function (index, item) {
                controls.lstSelectPlanInternet.append('<li class="transac-list-group-item"><button id="close_' + item[3] + '" type="button" class="transac-close"  data-toggle="tooltip" title="Quitar de la lista"><span>&times;</span></button><span class="badge">S/ ' + item[16] + '</span> ' + item[1] + '</li>');
                var serviceByPlan = {};
                serviceByPlan.CF = item[2];
                serviceByPlan.CodServSisact = item[3];
                serviceByPlan.DesServSisact = item[1];
                serviceByPlan.ServiceType = item[4];
                serviceByPlan.Sncode = item[5];
                serviceByPlan.CodGroupServ = item[6];
                serviceByPlan.Codtipequ = item[7];
                serviceByPlan.Tipequ = item[8];
                serviceByPlan.Spcode = item[9];
                serviceByPlan.CantEquipment = item[10];

                serviceByPlan.Equipment = item[11];
                serviceByPlan.Tmcode = item[12];
                serviceByPlan.CodPlanSisact = item[13];
                serviceByPlan.IDEquipment = item[14];
                serviceByPlan.GroupServ = item[15];
                that.listAditionalServices.push(serviceByPlan);
                var servicesForResume = {};
                servicesForResume.Servicio = "Internet";
                servicesForResume.Tipo = item[4];
                servicesForResume.NombreServicio = item[1];
                servicesForResume.CF = item[2];
                servicesForResume.CfWithIgv = item[16];

                controls.lstResumenPlanNuevo.append('<li id="close_' + item[3] + '" class="transac-list-group-item"><div class="row"><div class="col-sm-2">' + servicesForResume.Servicio + '</div><div class="col-sm-2">' + servicesForResume.Tipo + '</div><div class="col-sm-6">' + servicesForResume.NombreServicio + '</div><div class="col-sm-2"><span class="badge transac-badge-row">' + servicesForResume.CfWithIgv + '</span></div></div></li>');
                that.f_AddAditionalService("Internet");
                that.f_SumaMontos("Internet", item[2], "SERV");

                ninternet++;
                $("#close_" + item[3]).click(function () {
                    $(this).closest("li").remove();
                    $('#lstResumenPlanNuevo #close_' + item[3]).remove();
                    that.f_RestaMontos("Internet", item[2]);
                    that.f_RemoveAditionalService("Internet");
                    that.disabledSteps();
                    $.each(that.listAditionalServices, function (i, el) {
                        if (this.CodServSisact == item[3]) {
                            that.listAditionalServices.splice(i, 1);
                        }
                    });

                    if (controls.lstSelectPlanInternet.children().length == 0) {
                        controls.lstSelectPlanInternet.append('<li class="transac-list-group-item transac-message-red text-center"> No existen registros </li>');
                    }
                });
            });
            var n = $("#lstSelectPlanInternet li:contains('Seleccionar un plan')").length;
            if (controls.lstSelectPlanInternet.children().length == 1 && n > 0) {
                controls.lstSelectPlanInternet.html('');

                controls.lstSelectPlanInternet.append('<li class="transac-list-group-item transac-message-red text-center"> No existen registros </li>');
            }

        },
        AddTotabla360CandidatePlanAdicionalesPhone: function (arrayPhone) {
            var that = this;
            var controls = that.getControls();

            //if (arrayPhone.length > 0 && parseInt(controls.PhoneEquipmentQty.text()) == 0) {
            //}
            var nphone = parseInt(controls.PhoneEquipmentQty.text());
            $.each(arrayPhone, function (index, item) {
                controls.lstSelectPlanTelephony.append('<li class="transac-list-group-item"><button id="close_' + item[3] + '" type="button" class="transac-close"  data-toggle="tooltip" title="Quitar de la lista"><span>&times;</span></button><span class="badge">S/ ' + item[16] + '</span> ' + item[1] + '</li>');
                var serviceByPlan = {};
                serviceByPlan.CF = item[2];
                serviceByPlan.CodServSisact = item[3];
                serviceByPlan.DesServSisact = item[1];
                serviceByPlan.ServiceType = item[4];
                serviceByPlan.Sncode = item[5];
                serviceByPlan.CodGroupServ = item[6];
                serviceByPlan.Codtipequ = item[7];
                serviceByPlan.Tipequ = item[8];
                serviceByPlan.Spcode = item[9];
                serviceByPlan.CantEquipment = item[10];
                serviceByPlan.Equipment = item[11];
                serviceByPlan.Tmcode = item[12];
                serviceByPlan.CodPlanSisact = item[13];
                serviceByPlan.IDEquipment = item[14];
                serviceByPlan.GroupServ = item[15];
                that.listAditionalServices.push(serviceByPlan);
                var servicesForResume = {};
                servicesForResume.Servicio = "Teléfono";
                servicesForResume.Tipo = item[4];
                servicesForResume.NombreServicio = item[1];
                servicesForResume.CF = item[2];
                servicesForResume.CfWithIgv = item[16];

                controls.lstResumenPlanNuevo.append('<li id="close_' + item[3] + '" class="transac-list-group-item"><div class="row"><div class="col-sm-2">' + servicesForResume.Servicio + '</div><div class="col-sm-2">' + servicesForResume.Tipo + '</div><div class="col-sm-6">' + servicesForResume.NombreServicio + '</div><div class="col-sm-2"><span class="badge transac-badge-row">' + servicesForResume.CfWithIgv + '</span></div></div></li>');
                that.f_AddAditionalService("Phone");
                that.f_SumaMontos("Phone", item[2], "SERV");

                nphone++;
                $("#close_" + item[3]).click(function () {
                    $(this).closest("li").remove();
                    $('#lstResumenPlanNuevo #close_' + item[3]).remove();
                    that.f_RestaMontos("Phone", item[2]);
                    that.f_RemoveAditionalService("Phone");
                    that.disabledSteps();
                    $.each(that.listAditionalServices, function (i, el) {
                        if (this.CodServSisact == item[3]) {
                            that.listAditionalServices.splice(i, 1);
                        }
                    });

                    if (controls.lstSelectPlanTelephony.children().length == 0) {
                        controls.lstSelectPlanTelephony.append('<li class="transac-list-group-item transac-message-red text-center"> No existen registros </li>');
                    }
                });
            });
            var n = $("#lstSelectPlanTelephony li:contains('Seleccionar un plan')").length;
            if (controls.lstSelectPlanTelephony.children().length == 1 && n > 0) {
                controls.lstSelectPlanTelephony.html('');
                controls.lstSelectPlanTelephony.append('<li class="transac-list-group-item transac-message-red text-center"> No existen registros </li>');
            }

        },
        f_LoadJobTypes: function () {
            var that = this, controls = that.getControls();

            var data = {
                strIDSession: SessionPMHFC.IDSESSION,
                intType: 2
            };

            that.Loading();

            $.ajax({
                type: "POST",
                url: that.strUrl + '/Transactions/HFC/PlanMigration/LoadWorkTypes',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(data),
                success: function (data) {
                    if (data.data.lstSubWorkType != null) {
                        Session.vListSubType = data.data.lstSubWorkType;
                    }
                    that.f_LoadSelectControl(controls.sltWorkType, data.data.lstWorkType, null);
                    that.f_LoadSelectControl(controls.sltSubWorkType, data.data.lstSubWorkType, 'typeservice');
                    that.f_LoadSelectControl(controls.sltOperator, data.data.lstCarriers, null);
                },
                error: function (xhr, status, error) {
                }
            });

        },
        f_LoadSelectControl: function (sltControl, data, attr) {
            var strHTML = "";
            if (data != null) {
                if (data.length != 1) {
                    strHTML = strHTML + "<option value='-1'>Seleccionar</option>";
                }
                if (attr == null) {
                    $.each(data, function (key, value) {
                        strHTML = strHTML + "<option value='" + value.strCode + "' >" + value.strDescription + "</option>";
                    });
                } else {
                    $.each(data, function (key, value) {
                        strHTML = strHTML + "<option value='" + value.strCode + "' " + attr + "= '" + value.strTypeService + "' >" + value.strDescription + "</option>";
                    });
                }

                sltControl.html(strHTML);
            }
        },
        btnClose_Click: function () {
            parent.window.close();
        },
        chkOCC_Click: function () {
            if ($("#chkOCC").prop("checked")) {
                SessionPMHFC.hdnCheckOCCFinal = "1";
            } else {
                SessionPMHFC.hdnCheckOCCFinal = "0";
            }

            Smmry.set('chkOcc', $('#chkOCC').is(':checked') ? 'SI' : 'NO');
        },
        chkEmail_Click: function () {

            if ($('#chkEmail').is(':checked')) {
                Smmry.set('email', $('#txtEmail').val());
                controls.divEmail.show();
            }
            else {
                $("#txtEmail").closest(".form-group").removeClass("has-error");
                Smmry.set('email', "");
                controls.divEmail.hide();
            }
            Smmry.set('chkemail', $('#chkEmail').is(':checked') ? 'SI' : 'NO');
        },
        f_GetCustomerPhoneCommon: function () {
            var that = this,
                controls = that.getControls(),
                model = {};
            that.Loading();
            model.strIdSession = SessionPMHFC.IDSESSION;
            model.intIdContract = SessionPMHFC.DATACUSTOMER.ContractID;
            model.strTypeProduct = SessionPMHFC.UrlParams.SUREDIRECT;

            $.ajax({
                url: '/Transactions/HFC/CallDetails/GetCustomerPhone',
                data: JSON.stringify(model),
                type: 'POST',
                contentType: "application/json charset=utf-8;",
                dataType: "json",
                success: function (response) {
                    if (response != null) {
                        SessionPMHFC.hdntelephone = response;
                    } else {
                        alert("No existe una linea de teléfono.", "Alerta");
                    }
                },
                error: function (XError) {
                    //console.logXError);
                }
            });
        },
        ValidateFourthStep: function (fn) {

            if ($("#cboCacDac").val() == "") {
                $("#ErrorMessageCacDac").text("Debe Seleccionar un punto de Atención.");
                $("#cboCacDac").closest(".form-group").addClass("has-error");
                fn(false);
                return false;
            }
            else {
            }
            if ($('#chkEmail').is(':checked')) {
                if (!ValidateEmail($("#txtEmail").val())) {
                    fn(false);
                    return false;
                }
                else {
                    $("#txtEmail").closest(".form-group").removeClass("has-error");
                }
            }

            Smmry.set('fechaCompromiso', $("#ddlFranjaHoraria option:selected").html() == 'Seleccionar' ? '' : $("#txtFProgramacion").val());
            fn(true);
        },
        ValidatePenaltyStep: function (fn) {
            var that = this;
            fn(true);
        },
        ValidateComplementaryServicesStep: function (fn) {
            if ($("#sltOperator").val() == SessionPMHFC.NINGUN_OPERADOR) {
                $("#ErrorMessageDllOperador").text("Debe Seleccionar un OPERADOR.");
                $("#sltOperator").closest(".form-group").addClass("has-error");

                fn(false);
                return false;
            }
            fn(true);
        },
        ValidateScheduling: function (fn) {
            if (SessionPMHFC.strValidateETA == '2') {
                if ($("#txtFProgramacion").val() == "" || $("#txtFProgramacion").val() == null ||
                    $("#ddlFranjaHoraria").val() == null || $("#ddlFranjaHoraria").val() == "-1") {
                    $.each(Session.vMessageValidationList, function (index, value) {
                        if (value.ABREVIATURA_DET == "MSJ_OBLIG_ETA") {
                            alert(value.CODIGOC, "Alerta");
                        }
                    });
                    fn(false);
                    return false;
                }
            }

            Smmry.set('Subtipotrabajo', $("#sltSubWorkType option:selected").html() == 'Seleccionar' ? '' : $("#sltSubWorkType option:selected").html());
            Smmry.set('tipodetrabajo', $("#sltWorkType option:selected").html() == 'Seleccionar' ? '' : $("#sltWorkType option:selected").html());

            fn(true);
        },
        getIGV: function () {
            var that = this,
                controls = that.getControls(),
                objIGV = {};

            that.Loading();

            objIGV.strIdSession = SessionPMHFC.IDSESSION;

            $.app.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objIGV),
                url: '/Transactions/CommonServices/GetConsultIGV',
                success: function (result) {

                    if (result.data != null) {
                        var igv = parseFloat(result.data.igvD);
                        var igvEnt = igv + 1;
                        that.strIGV = igvEnt;

                    } else {

                        $("#btnNextFirstStep").prop("disabled", true);

                        alert(SessionPMHFC.strErrorMessageIgv, "Alerta");

                    }
                }
            });
        },
        intCampania: 0,
        ConsultCampaign: function () {
            
            var that = this,
               controls = that.getControls(),
                objParam = {
                    MessageRequest: {
                        Body: {
                            consultarCampaniaRequest: {
                                consultaCampania: {
                                    numLinea: ((SessionPMHFC.DATACUSTOMER.Telephone == '') ? ' ' : SessionPMHFC.DATACUSTOMER.Telephone),
                                    nroDoc: SessionPMHFC.DATACUSTOMER.DNIRUC,
                                    coId: SessionPMHFC.DATACUSTOMER.ContractID,
                                    tipoPrdCod: ((SessionPMHFC.UrlParams.SUREDIRECT == 'HFC') ? '05' : '08')
                                }
                            }
                        }
                    },
                    strIdSession: Session.IDSESSION,
                    strTipoDocumento: SessionPMHFC.DATACUSTOMER.DocumentType
                };

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                async: false,
                url: location.protocol + '//' + location.host + '/Transactions/HFC/PlanMigration/GetConsultCampaign',
                data: JSON.stringify(objParam),

                beforeSend: function () {
                    var stUrlLogo = "/Images/loading2.gif";
                    //controls.divNewSubscriptions.html('<div class="loading"><img src="' + stUrlLogo + '" width="25" height="25" /> Cargando ....</div>');
                },

                complete: function () {
                    $(".loading").hide();
                },

                success: function (response) {

                    if (response != null) {
                        if (response.MessageResponse != null) {
                            if (response.MessageResponse.Header.HeaderResponse.status.code == '0') {
                                if (response.MessageResponse.Body != null) {
                                    if (response.MessageResponse.Body.consultarCampaniaResponse != null) {
                                        if (response.MessageResponse.Body.consultarCampaniaResponse.auditResponse.codigoRespuesta == '0') {
                                            if (response.MessageResponse.Body.consultarCampaniaResponse.consultarCursor != null) {
                                                if (response.MessageResponse.Body.consultarCampaniaResponse.consultarCursor.cursor != null) {
                                                    if (response.MessageResponse.Body.consultarCampaniaResponse.consultarCursor.cursor[0].campaniaCodigo != "") {
                                                        var ArrayCampColab = controls.hidCodCampaniaMig.split("|");
                                                        i = ArrayCampColab.length;

                                                        while (i--) {
                                                            if (ArrayCampColab[i] == response.MessageResponse.Body.consultarCampaniaResponse.consultarCursor.cursor[0].campaniaCodigo)
                                                            {
                                                                that.intCampania = 1;
                                                            }
                                                        }

                                                        if (that.intCampania == 1) {
                                                            controls.lblCampania.html(response.MessageResponse.Body.consultarCampaniaResponse.consultarCursor.cursor[0].campaniaDescripcion);
                                                            Session.intCampania = 1;
                                                        } else {

                                                            controls.trGrupoCampania.attr("style", "display:none");
                                                        }
                                                        return;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            } else {
                                alert(controls.hidMsgErrorConsultCam, 'Alerta');
                            }
                        }
                    }
                    controls.trGrupoCampania.attr("style", "display:none");
                    return;
                },

                error: function () {
                    alert('Estimado usuario, presentamos intermitencia de aplicativo. ', 'Alerta');
                    controls.trGrupoCampania.attr("style", "display:none");
                    return;
                }
            });
        },

        RegisterCampaign: function (item)
        {
            var that = this,
               controls = that.getControls(),
                objParam = {
                    MessageRequest: {
                        Body: {
                            registrarCampaniaRequest: {
                                registrarCampania: {
                                    nroDocumento: SessionPMHFC.DATACUSTOMER.DNIRUC,
                                    nroLinea: ((SessionPMHFC.DATACUSTOMER.Telephone == '') ? '111111111' : SessionPMHFC.DATACUSTOMER.Telephone),
                                    planCodigo: item.strCodPlanSisact,
                                    planDescripcion: item.strDesPlanSisact,
                                    tmCode: item.strTmcode,
                                    tipoPrdCodigo: ((SessionPMHFC.UrlParams.SUREDIRECT == 'HFC') ? '05' : '08'),
                                    tipoPrdDescripcion: item.strTipoProd,
                                    campaniaCodigo: item.strCampaignCode,
                                    campaniaDescripcion: item.strCampaignDescription,
                                    coId: SessionPMHFC.DATACUSTOMER.ContractID,                                                                                                                                             
                                    usuarioCrea: SessionPMHFC.USERACCESS.login,
                                }
                            }
                        }
                    },
                    strIdSession: Session.IDSESSION,
                    strTipoDocumento: SessionPMHFC.DATACUSTOMER.DocumentType
                };

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                async: false,
                url: location.protocol + '//' + location.host + '/Transactions/HFC/PlanMigration/GetRegisterCampaign',
                data: JSON.stringify(objParam),

                beforeSend: function () {
                    var stUrlLogo = "/Images/loading2.gif";
                    //controls.divNewSubscriptions.html('<div class="loading"><img src="' + stUrlLogo + '" width="25" height="25" /> Cargando ....</div>');
                },

                complete: function () {
                    $(".loading").hide();
                },

                success: function (response)
                {
                    if (response != null) {
                        if (response.MessageResponse != null) {
                            if (response.MessageResponse.Body != null) {
                                if (response.MessageResponse.Body.registrarCampaniaResponse != null) {
                                    if (response.MessageResponse.Body.registrarCampaniaResponse.auditResponse != null) {
                                        if (response.MessageResponse.Body.registrarCampaniaResponse.auditResponse.codigoRespuesta == "0") {
                                            return;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    return;
                },

                error: function () {return;
                }
            });
        },

        strIGV: '',
        strUrlLogo: window.location.protocol + '//' + window.location.host + '/Images/loading2.gif',
        disabledSteps: function () {
            $('button[href="#tabServiciosComplementarios"]').prop("disabled", true);
            $('button[href="#tabPenalidades"]').prop("disabled", true);
            $('button[href="#tabAgendamiento"]').prop("disabled", true);
            $('button[href="#tabDatosTecnicos"]').prop("disabled", true);
            $('button[href="#tabSummary"]').prop("disabled", true);
        }
    };

    $("#txtEmail").change(function (e) {
        Smmry.set('email', $('#txtEmail').val());
    });
    $("#txtEmail").keyup(function (e) {
        Smmry.set('email', $('#txtEmail').val());
    });

    $("#txtTopConsume").keyup(function (e) {
        Smmry.set('topeconsumo', $("#txtTopConsume").val());
    });

    $("#txtTopConsume").change(function (e) {
        Smmry.set('topeconsumo', $("#txtTopConsume").val());
    });
    function f_checkEmail() {
        var email = document.getElementById('txtEmail');
        var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;

        if (!filter.test(email.value)) {
            $("#ErrorMessageEmail").text("Ingrese una direccion de correo valida.");
            $("#txtEmail").closest(".form-control").addClass("has-error");

            $("#txtEmail").focus();
            return false;
        }
        Smmry.set('email', $('#txtEmail').val());
    };
    $("#txtEmail").focusout(function (e) {
        var email = document.getElementById('txtEmail');
        var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        if (!filter.test(email.value)) {
            $("#txtEmail").closest(".form-control").addClass("has-error");
            $("#ErrorMessageEmail").text('Ingrese una direccion de correo valida.');
            $("#txtEmail").focus();
        }
        else {
            $("#txtEmail").closest(".form-control").removeClass("has-error");
            $("#ErrorMessageEmail").text("");
        }
    });
    $("#txtNotas").change(function (e) {
        Smmry.set('Notas', "");

        if ($("#txtNotas").val() != "") {

            var Notas = $("#txtNotas").val();
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


            Smmry.set('Notas', strFinal);

        } else {
            Smmry.set('Notas', "no han ingresado notas");
        }

    });
    $("#txtNotas").keyup(function (e) {
        Smmry.set('Notas', "");

        if ($("#txtNotas").val() != "") {

            var Notas = $("#txtNotas").val();
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


            Smmry.set('Notas', strFinal);

        } else {
            Smmry.set('Notas', "no han ingresado notas");
        }
    });
    $("#txtNroCarta").keyup(function (e) {
        if (e.keyCode == 8) {
        } else {
            if ($("#txtNroCarta").val().length > 10) {
                e.preventDefault();
                return false;
            }
            if (e.keyCode < 48 || e.keyCode > 57) {
                e.preventDefault();
                return false;
            }
        }

    });
    $("#txtNroCarta").change(function (e) {

        Smmry.set('numerocarta', $("#txtNroCarta").val());
    });
    $("#txtNroCarta").keyup(function (e) {
        var caracteres = 15;
        if ($("#txtNroCarta").val().length > caracteres) {
            $("#txtNroCarta").val($("#txtNroCarta").val().substr(0, caracteres));
        }
        Smmry.set('numerocarta', $("#txtNroCarta").val());
    });

    $("#txtNroCarta").keydown(function (e) {

        if (e.keyCode == 46 || e.keyCode == 8 || e.keyCode == 190 || e.keyCode == 9 || e.keyCode == 27 || e.keyCode == 13 ||
                    (e.keyCode == 65 && event.ctrlKey === true) ||
                   (e.keyCode >= 35 && event.keyCode <= 39)) {
            return;
        }
        else {
            if (e.shiftKey || (event.keyCode < 48 || e.keyCode > 57) && (e.keyCode < 96 || e.keyCode > 105) && e.keyCode != 110) {
                e.preventDefault();
            }
        }
    });
    $("#txtMontoFideliza").keyup(function (e) {
        var key = e.charCode || e.keyCode || 0;
        if (key == 190) {
            var content = $("#txtMontoFideliza").val();
            if (content.indexOf(".") != -1) {
                return false;
            }
        }
        Smmry.set('Montofidelizacionpenalidad', $("#txtMontoFideliza").val());

        return (
                key == 8 ||
                key == 9 ||
                key == 46 ||
                key == 110 ||
                key == 190 ||
                (key >= 35 && key <= 40) ||
                (key >= 48 && key <= 57) ||
                (key >= 96 && key <= 105));

    });
    $("#txtMontoFideliza").change(function (e) {
        var key = e.charCode || e.keyCode || 0;
        if (key == 190) {
            var content = $("#txtMontoFideliza").val();
            if (content.indexOf(".") != -1) {
                return false;
            }
        }
        Smmry.set('Montofidelizacionpenalidad', $("#txtMontoFideliza").val());

        return (
                key == 8 ||
                key == 9 ||
                key == 46 ||
                key == 110 ||
                key == 190 ||
                (key >= 35 && key <= 40) ||
                (key >= 48 && key <= 57) ||
                (key >= 96 && key <= 105));

    });
    $("#txtMontoFideliza").keyup(function (e) {
        that.f_sumaPenalidades();
    });
    $("#txtTotalPenalidad").keyup(function (e) {
        Smmry.set('Total-penalidad', $("#txtTotalPenalidad").val());

    });
    $("#txtTotalPenalidad").change(function (e) {
        Smmry.set('Total-penalidad', $("#txtTotalPenalidad").val());

    });

    $("#txtTopConsume").keyup(function (e) {
        Smmry.set('topeconsumo', $("#txtTopConsume").val());
    });
    $("#txtTopConsume").change(function (e) {
        Smmry.set('topeconsumo', $("#txtTopConsume").val());
    });
    $("#txtReintegro").keyup(function (e) {
        var key = e.charCode || e.keyCode || 0;
        if (key == 190) {
            var content = $("#txtReintegro").val();
            if (content.indexOf(".") != -1) {
                return false;
            }
        }
        Smmry.set('Reintegropenalidad', $("#txtReintegro").val());

        return (
                key == 8 ||
                key == 9 ||
                key == 46 ||
                key == 110 ||
                key == 190 ||
                (key >= 35 && key <= 40) ||
                (key >= 48 && key <= 57) ||
                (key >= 96 && key <= 105));

    });

    $("#txtReintegro").change(function (e) {
        var key = e.charCode || e.keyCode || 0;
        if (key == 190) {
            var content = $("#txtReintegro").val();
            if (content.indexOf(".") != -1) {
                return false;
            }
        }
        Smmry.set('Reintegropenalidad', $("#txtReintegro").val());

        return (
                key == 8 ||
                key == 9 ||
                key == 46 ||
                key == 110 ||
                key == 190 ||
                (key >= 35 && key <= 40) ||
                (key >= 48 && key <= 57) ||
                (key >= 96 && key <= 105));
    });
    $("#txtReintegro").keyup(function (e) {
        that.f_sumaPenalidades();
        var valor = $("#txtReintegro").val();
        try {
            if (parseFloat(valor) > 0) {
                $("#chkFideliza").prop("disabled", false);
            } else {
                $("#chkFideliza").prop("disabled", true);
            }
        } catch (asd) {
            $("#chkFideliza").prop("disabled", true);
        }

    });

    $.fn.HfcPlanMigration = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('HfcPlanMigration'),
                options = $.extend({}, $.fn.HfcPlanMigration.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('HfcPlanMigration', data);
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
    $.fn.HfcPlanMigration.defaults = {
    };

    $('#divBody').HfcPlanMigration();
})(jQuery);


