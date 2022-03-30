var hdnPermisoExp;
var hdnPermisoBus;
var sessionTransac = {};
var userValidatorAuth = "";
var dataSearch;

sessionTransac.SessionParams = {};
sessionTransac = JSON.parse(sessionStorage.getItem("SessionTransac"));
sessionTransac.HiddensTransact = {};

//console.logsessionTransac);

function restaMontos() {
    if ($("#txtAmountRetention").val() == "") {
        $("#txtTotalPay").val("");
        return false;
    }
    try {
        var t = $("#txtAmountRetention").val();
        if (t.substr(t.length - 1) == ".") {
            return false;
        }

        var v1 = parseFloat($("#txtAmountRetention").val());
        var v2 = parseFloat($("#txtAmountPay").val());

        if (!$.isNumeric($("#txtAmountRetention").val()) || !$.isNumeric($("#txtAmountPay").val())) {
            return false;
        }

        $("#txtAmountRetention").val(v1);

        if (v1 > v2) {
            alert("El monto de la retención no puede ser mayor al importe a pagar.", "Alerta");
            $("#txtAmountRetention").val("0");
            $("#txtTotalPay").val($("#txtAmountPay").val());
            return false;
        }

        var v3 = v2 - v1;
        $("#txtTotalPay").val(v3.toFixed(2));

    } catch (err) {

    }

}

function validaFechas(validaVacias) {
    var fechaSus = $("#txtSuspensionDate").val();
    fechaSus = fechaSus.substr(6, 4) + "/" + fechaSus.substr(3, 2) + "/" + fechaSus.substr(0, 2);

    var fechaRea = $("#txtReactivationDate").val();
    fechaRea = fechaRea.substr(6, 4) + "/" + fechaRea.substr(3, 2) + "/" + fechaRea.substr(0, 2);

    if (validaVacias) {
        if (fechaSus == "//" || fechaRea == "//") {
            alert("Necesita seleccionar las fechas de suspensión y reactivación.", "Alerta");
            return false;
        }
    }

    var fsus = new Date(fechaSus);
    var frea = new Date(fechaRea);
    var today = new Date();


    if ($.urlParam("Mode") !== "Edi") {
        if (fsus <= today) {
            alert("La fecha de suspensión debe ser mayor al día de hoy.", "Alerta");
            return false;
        }
    }

    //SD-232847 inicio
    if ($.urlParam("TipoServi") === sessionTransac.HiddensTransact.hidTipoTranSuspension) {
        if ($.urlParam("EstadoServi") === sessionTransac.HiddensTransact.hidEstadoTranPendiente) {
            if (fsus <= today) {
                alert("La fecha de suspensión debe ser mayor al día de hoy.", "Alerta");
                return false;
            }
        }
    }

    //SD-232847 fin                   
    if (frea <= fsus) {
        alert("La fecha de suspensión debe ser menor a la fecha de reactivación.", "Alerta");
        return false;
    }

    var oneDay = 24 * 60 * 60 * 1000;
    var diffDays = Math.round(Math.abs((fsus.getTime() - frea.getTime()) / (oneDay)));
    var dia1 = sessionTransac.HiddensTransact.hidDiasMinSuspension;
    var dia2 = sessionTransac.HiddensTransact.hidMaxDiasSuspension;
    var dia3 = sessionTransac.HiddensTransact.hidMaxDiasRetSuspension;
    var dia4 = sessionTransac.HiddensTransact.hidMinDiasRetSuspension;

    if (!$('#chkRetention').is(":checked")) {
        //No es retencion
        if (diffDays < dia1) {
            alert("La fecha de suspensión debe ser por lo menos menor por " + dia1 + " días de la fecha de reactivación.", "Alerta");
            return false;
        }

        if (diffDays > dia2) {
            alert("El período mínimo y máximo de Suspensión es de " + dia1 + " y " + dia2 + " días respectivamente. No es posible realizar la transacción.", "Alerta");
            return false;
        } else {
            return true;
        }
    } else {
        //Suspencion
        if (diffDays < dia4) {
            alert("La fecha de retención debe ser por lo menos menor por " + dia4 + " días de la fecha de reactivación.", "Alerta");
            return false;
        }

        if (diffDays > dia3) {
            alert("El periodo máximo de suspensión por retención no puede superar a los " + dia3 + " días. No es posible realizar la transacción.", "Alerta");
            return false;
        } else {
            return true;
        }
    }

    return true;
}

function f_Comprueba_Fechas(fechaMin) {

    var flag = true;

    var fechaInicial = FechadeInicio();
    var fechaFinal = FechadeFin();
    var diaMilisegundos = 60 * 60 * 24 * 1000; // milliseconds 
    var diff = Math.round(fechaFinal - fechaInicial);
    var diasDiferencia = Math.round(diff / diaMilisegundos);
    if (fechaMin == null)
        fechaMin = 1;

    if (parseInt(diasDiferencia) < parseInt(fechaMin)) {
        alert("La Fecha Suspención no puede ser menor de " + fechaMin + " dia(s) respecto a la Fecha Reactivación.", "Alerta");
        flag = false;
    }

    return flag;
    //else { saveSuspension(); }

}

function FechadeInicio() {
    var fechainicio = "";
    if ($.trim($("#txtSuspensionDate").val()) == "") {
        alert(sessionTransac.HiddensTransact.hdnMensaje2, "Alerta");
    }
    else {
        var fechaini = $("#txtSuspensionDate").val();
        var dia = fechaini.substring(0, 2);
        var mes = parseInt(fechaini.substring(3, 5)) - 1;
        var anio = fechaini.substring(6, 10);
        fechainicio = new Date(anio, mes, dia, 0, 0, 0, 0);
    }

    return fechainicio;
}

function FechadeFin() {
    var fechafin = "";
    if ($.trim($("#txtReactivationDate").val()) == "") {
        alert(sessionTransac.HiddensTransact.hdnMensaje3, "Alerta");
    }
    else {
        var fechafinal = $("#txtReactivationDate").val();
        var dia = fechafinal.substring(0, 2);
        var mes = parseInt(fechafinal.substring(3, 5)) - 1;
        var anio = fechafinal.substring(6, 10);
        fechafin = new Date(anio, mes, dia, 0, 0, 0, 0);
    }
    return fechafin;
}

function saveSuspension(pag, controls) {

    if (!validaFechas(true)) {
        return false;
    }

    if (!validarInteraccion()) {
        return false;
    }

    if ($("#cboAtentionLocal").val() === "-1") {
        alert("Seleccione un local de atención.", "Alerta");
        return false;
    }

    confirm(sessionTransac.HiddensTransact.hdnMensaje1, 'Confirmar', function () {
        sessionTransac.HiddensTransact.hidTotalImportePagar = $('#txtTotalPay').val();
        sessionTransac.HiddensTransact.hidAccion = "G";

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

        var checkRetencion;

        var objCheck = $('#chkRetention');

        if (objCheck.prop("checked")) {
            checkRetencion = "1";
        }
        else {
            checkRetencion = "0";
        }

        var param = {
            strIdSession: Session.IDSESSION,
            strTransaction: Session.IDSESSION,
            hidAccion: sessionTransac.HiddensTransact.hidAccion,
            contractId: sessionTransac.SessionParams.DATACUSTOMER.ContractID,
            txtImpPagar: $('#txtAmountPay').val(),
            fullName: sessionTransac.SessionParams.DATACUSTOMER.FullName,
            currentUser: sessionTransac.SessionParams.USERACCESS.login,
            notes: $('#txtNotes').val(),
            customerId: sessionTransac.SessionParams.DATACUSTOMER.CustomerID,
            codePlanInst: sessionTransac.SessionParams.DATACUSTOMER.PlaneCodeInstallation,
            nroCelular: sessionTransac.SessionParams.DATACUSTOMER.Telephone,
            socialReason: sessionTransac.SessionParams.DATACUSTOMER.FullName,
            representanteLegal: sessionTransac.SessionParams.DATACUSTOMER.LegalAgent,
            tipoDoc: sessionTransac.SessionParams.DATACUSTOMER.DocumentType,
            nroDoc: sessionTransac.SessionParams.DATACUSTOMER.DocumentNumber,
            plan: sessionTransac.SessionParams.DATASERVICE.Plan,
            cicloFacturacion: sessionTransac.SessionParams.DATACUSTOMER.BillingCycle,
            fechaSuspension: $('#txtSuspensionDate').val(),
            fechaReactivacion: $('#txtReactivationDate').val(),
            checkRetencion: checkRetencion,
            cuenta: sessionTransac.SessionParams.DATACUSTOMER.Account,
            txtMontoRet: $('#txtAmountRetention').val(),
            hidImportePagar: sessionTransac.HiddensTransact.hidImportePagar,
            cacDac: $('#cboAtentionLocal').val(),
            hidTotalImportePagar: sessionTransac.HiddensTransact.hidTotalImportePagar,
            hidMontoCobrarUnitario: sessionTransac.HiddensTransact.hidMontoCobrarUnitario,
            //se pasaron 3 parametros mas para los datos de linea
            strTypeProduct: sessionTransac.DATACUSTOMER.ProductType,
            strDateActivation: sessionTransac.DATASERVICE.ActivationDate,
            strDateEndAcuerdo: sessionTransac.DATASERVICE.StateAgreement,
            strStatusLine: sessionTransac.DATASERVICE.StateLine
        };

        var strUrlController = pag.strUrl + '/SuspensionService/HfcSuspensionService_PostSuspendProgramTask';

        $.app.ajax({
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            data: JSON.stringify(param),
            url: strUrlController,
            success: function (response) {
                controls.btnSave.attr('disabled', response.btnSuspenderDisabled);
                controls.btnConstancy.attr('disabled', response.btnImprimirDisabled);
                controls.chkRetention.attr('disabled', response.chkRetencionDisabled);

                //Guardando datos del Pdf en un Hidden
                sessionTransac.HiddensTransact.hdnRutaPdf = response.hidRutaPDF;
                sessionTransac.HiddensTransact.hdnBoolReturn = response.hidBoolReturn;

                alert(response.lblMensajeText, "Alerta", function() {
                    //location.reload();
                });
            }
        });
    });    
}

function validarInteraccion() {

    var strNotas = $('#txtNotes').val();

    if (strNotas.length > 3800) {
        alert(sessionTransac.HiddensTransact.hdnMensaje4, "Alerta");
        $('#txtNotes').val(strNotas.substring(0, 3800));
        $('#txtNotes').focus();
        return false;
    }

    return true;
}

function f_Imprimir() {

    var boolReturn = sessionTransac.HiddensTransact.hdnBoolReturn;

    if (boolReturn) {
        var index = response.FullPathPDF.lastIndexOf("\\") + 1;
        var filename = response.FullPathPDF.substring(index);

        var split = response.FullPathPDF.split('\\');

        var rutapdf = split[3] + "//" + split[4];
        var filetransaction = split[5];

        ReadRecord(sessionTransac.SessionParams.USERACCESS.userId, rutapdf, filetransaction, filename);
    } 
};

(function ($) {

    $('#idtdMontoRet').attr("style", "display:none");
    $("#txtAmountPay").prop("disabled", true);
    $("#txtSuspensionDate").bind("cut copy paste", function (e) {
        e.preventDefault();
    });
    $("#txtAmountRetention").keydown(function (e) {
        if (e.keyCode === 46 || e.keyCode === 8) {

        } else {
            var text = $("#txtAmountRetention").val();
            if (text.indexOf(".") > -1) {
                var p = text.indexOf(".");
                var t = text.substr(p + 1);

                if (t.length > 1) {
                    return false;
                }
            }
        }
    });

    $("#txtAmountRetention").keyup(function () {
        restaMontos();
    });

    $("#txtSuspensionDate").keypress(function (e) {
        return false;
    });

    $("#txtReactivationDate").bind("cut copy paste", function (e) {
        e.preventDefault();
    });

    $("#txtReactivationDate").keypress(function (e) {
        return false;
    });

    $("#txtReactivationDate").change(function () {
        sessionTransac.HiddensTransact.hdnFRecEdi = $("#txtReactivationDate").val()
        if (!validaFechas(false)) {
            $("#FechaReactivacion").val("");
            return false;
        }
    });

    $("#txtSuspensionDate").change(function () {
        sessionTransac.HiddensTransact.hdnFSusEdi = $("#txtSuspensionDate").val()
        if (!validaFechas(false)) {
            $("#txtSuspensionDate").val("");
            return false;
        }
    });

    var Form = function ($element, options) {
        $.extend(this, $.fn.ProgramTask.defaults, $element.data(), typeof options === 'object' && options);
        this.setControls({
            form: $element
            , lblContract: $('#lblContract', $element)
            , lblTypeCustomer: $('#lblTypeCustomer', $element)
            , lblContactName: $('#lblContactName', $element)
            , lblCustomerName: $('#lblCustomerName', $element)
            , lblDniRuc: $('#lblDniRuc', $element)
            , lblDateActivation: $('#lblDateActivation', $element)
            , lblLegalRepresentative: $('#lblLegalRepresentative', $element)
            , lblDniLegalRepres: $('#lblDniLegalRepres', $element)

            , spnMainTitle: $('#spnMainTitle')

            , myModalLoad: $('#myModalLoad', $element)
            , divRules: $('#divRules', $element)

            , ContainerCheckRetention: $('#ContainerCheckRetention', $element)

            , txtSuspensionDate: $('#txtSuspensionDate', $element)
            , txtReactivationDate: $('#txtReactivationDate', $element)
            , txtAmountPay: $('#txtAmountPay', $element)
            , txtAmountRetention: $('#txtAmountRetention', $element)
            , txtTotalPay: $('#txtTotalPay', $element)
            , txtNotes: $('#txtNotes', $element)

            , chkRetention: $('#chkRetention', $element)

            , cboAtentionLocal: $('#cboAtentionLocal', $element)

            , btnSave: $('#btnSave', $element)
            , btnClose: $('#btnClose', $element)
            , btnConstancy: $('#btnConstancy', $element)
        });
    }
    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this, controls = this.getControls();
            controls.btnSave.addEvent(that, 'click', that.btnSave_Click);
            controls.btnClose.addEvent(that, 'click', that.btnClose_Click);
            controls.btnConstancy.addEvent(that, 'click', that.btnConstancy_Click);
            controls.chkRetention.addEvent(that, 'click', that.chkRetention_Click);
            controls.txtSuspensionDate.datepicker({ format: 'dd/mm/yyyy', endDate: '+90d' });
            controls.txtReactivationDate.datepicker({ format: 'dd/mm/yyyy', endDate: '+90d' });
            that.maximizarWindow();
            that.render();
        },
        render: function () {
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

            that.getCustomerData();
            that.PageLoadSuspensionService();
        },
        chkRetention_Click: function() {
            var that = this;
            that.ShowAmount();
        },
        btnSave_Click: function () {
            var that = this, controls = this.getControls();
            saveSuspension(that, controls);
        },
        btnConstancy_Click: function () {
            f_Imprimir();
        },
        btnClose_Click: function () {
            parent.window.close();
        },
        getControls: function () {
            return this.m_controls || {};
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

        setControls: function (value) {
            this.m_controls = value;
        },
        setMainTitle: function (titlePage) {
            var that = this, controls = that.getControls();
            controls.spnMainTitle.html('<b>' + titlePage + '</b>');
        },
        PageLoadSuspensionService: function () {
            var controls = this.getControls(), that = this;
            if (Session.USERACCESS == {} && Session.USERACCESS.CODEUSER == "") {
                parent.window.close();
                opener.parent.top.location.href = Session.RouteSiteStart;
                return;
            }

            var strUrlController = that.strUrl + '/SuspensionService/HfcSuspensionService_PageLoad';
            var param = { "strIdSession": Session.IDSESSION, "strTransaction": Session.IDSESSION, 'estadoLinea': sessionTransac.SessionParams.DATASERVICE.StateLine, 'strPermisos': sessionTransac.SessionParams.USERACCESS.optionPermissions, 'contractId': sessionTransac.SessionParams.DATACUSTOMER.ContractID }

            $.ajax({
                type: 'POST',
                cache: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'JSON',
                url: strUrlController,
                data: JSON.stringify(param),
                success: function (response) {

                    if (response.lblMensajeVisible) {
                        alert(response.lblMensajeText, "Alerta");
                        return;
                    }

                    if (response.EstadoLinea != "") {
                        alert(response.EstadoLinea, "Alerta");
                        parent.window.close();
                        return;
                    }

                    if (response.lblMensajeVisible) {
                        alert(response.lblMensajeText, "Alerta");
                        return;
                    }

                    sessionTransac.HiddensTransact.hidFlagContingenciaHP = response.hidFlagContingenciaHP;
                    sessionTransac.HiddensTransact.hdnSiteUrl = response.hdnSiteUrl;
                    sessionTransac.HiddensTransact.EstadoLinea = response.EstadoLinea;
                    sessionTransac.HiddensTransact.flagRestringirAccesoTemporalSrasc = response.flagRestringirAccesoTemporalSrasc;
                    sessionTransac.HiddensTransact.msgRestringirAccesoTemporalSrasc = response.msgRestringirAccesoTemporalSrasc;
                    sessionTransac.HiddensTransact.strConsLineaDesaActiva = response.strConsLineaDesaActiva;
                    sessionTransac.HiddensTransact.gConstMsgLineaStatSuspe = response.gConstMsgLineaStatSuspe;
                    sessionTransac.HiddensTransact.hidMinDiasSuspencion = response.hidMinDiasSuspencion;
                    sessionTransac.HiddensTransact.hidDiasMinSuspension = response.hidDiasMinSuspension;
                    sessionTransac.HiddensTransact.hidMaxDiasSuspension = response.hidMaxDiasSuspension;
                    sessionTransac.HiddensTransact.hidMinDiasRetSuspension = response.hidMinDiasRetSuspension;
                    sessionTransac.HiddensTransact.hidMaxDiasRetSuspension = response.hidMaxDiasRetSuspension;
                    sessionTransac.HiddensTransact.hidTipoTranSuspension = response.hidTipoTranSuspension;
                    sessionTransac.HiddensTransact.hidEstadoTranPendiente = response.hidEstadoTranPendiente;
                    sessionTransac.HiddensTransact.chkRetencionVisible = response.chkRetencion;
                    sessionTransac.HiddensTransact.lblMensajeVisible = response.lblMensajeVisible;
                    sessionTransac.HiddensTransact.ModoEdicion = response.ModoEdicion;
                    sessionTransac.HiddensTransact.txtMontoRet = response.txtMontoRet;
                    sessionTransac.HiddensTransact.txtTotalImportePagar = response.txtTotalImportePagar;
                    sessionTransac.HiddensTransact.TipoServi = response.TipoServi;
                    sessionTransac.HiddensTransact.EstadoServi = response.EstadoServi;

                    controls.txtAmountRetention.val(sessionTransac.HiddensTransact.txtMontoRet);
                    controls.txtTotalPay.val(sessionTransac.HiddensTransact.txtTotalImportePagar);

                    if (sessionTransac.HiddensTransact.chkRetencionVisible === false) {
                        controls.ContainerCheckRetention.hide();
                    }

                    that.LoadHiddenMessages();
                    that.InitCacDat();
                },
                error: function (error) {
                    alert('Error: ' + error, "Alerta");
                }
            });
        },
        LoadHiddenMessages: function () {
            var controls = this.getControls(), that = this;           

            var strUrlController = that.strUrl + '/SuspensionService/HfcSuspensionService_LoadHiddenMessages';
            var param = { "strIdSession": Session.IDSESSION, "strTransaction": Session.IDSESSION }

            $.ajax({
                type: 'POST',
                cache: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'JSON',
                url: strUrlController,
                data: JSON.stringify(param),
                success: function (response) {
                    sessionTransac.HiddensTransact.hdnTituloPagina = response.hdnTituloPagina;
                    sessionTransac.HiddensTransact.hdnMensaje1 = response.hdnMensaje1;
                    sessionTransac.HiddensTransact.hdnMensaje2 = response.hdnMensaje2;
                    sessionTransac.HiddensTransact.hdnMensaje3 = response.hdnMensaje3;
                    sessionTransac.HiddensTransact.hdnMensaje4 = response.hdnMensaje4;                    

                    that.setMainTitle(sessionTransac.HiddensTransact.hdnTituloPagina);
                    that.LoadInfoCustomer();
                },
                error: function (error) {
                    alert('Error: ' + error, "Alerta");
                }
            });
        },
        LoadInfoCustomer: function () {
            var controls = this.getControls(), that = this;

            var strUrlController = that.strUrl + '/SuspensionService/HfcSuspensionService_LoadInfoCustomer';
            var param = { "strIdSession": Session.IDSESSION, "strTransaction": Session.IDSESSION }

            $.ajax({
                type: 'POST',
                cache: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'JSON',
                url: strUrlController,
                data: JSON.stringify(param),
                success: function (response) {
                    if (response.lblMensajeVisible) {
                        alert(response.lblMensajeText, "Alerta");
                        return;
                    }

                    sessionTransac.HiddensTransact.txtImpPagar = response.txtImpPagar;
                    sessionTransac.HiddensTransact.hidImportePagar = response.hidImportePagar;
                    sessionTransac.HiddensTransact.hidMontoCobrarUnitario = response.hidMontoCobrarUnitario;
                    sessionTransac.HiddensTransact.btnImprimirDisabled = response.btnImprimirDisabled;
                    sessionTransac.HiddensTransact.btnSuspenderDisabled = response.lblMensajeVisible;

                    controls.txtAmountPay.val(sessionTransac.HiddensTransact.txtImpPagar);
                    controls.btnSave.attr('disabled', sessionTransac.HiddensTransact.btnSuspenderDisabled);
                    controls.btnConstancy.attr('disabled', sessionTransac.HiddensTransact.btnImprimirDisabled);
                    that.LoadTypification();
                },
                error: function (error) {
                    alert('Error: ' + error, "Alerta");
                }
            });
        },
        LoadTypification: function () {
            var that = this, param = {};
            param.strIdSession = Session.IDSESSION;
            param.strTransaction = Session.IDSESSION;

            var strUrlController = that.strUrl + '/SuspensionService/HfcSuspensionService_LoadTypification';

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(param),
                url: strUrlController,
                success: function (response) {
                    if (response.lblMensajeVis) {
                        alert(response.lblMensajeTxt, "Alerta");
                        controls.btnSave.attr('disabled', response.btnGuardarDisabled);
                        controls.btnConstancy.val('disabled', response.btnConstanciaDisabled);
                    } else {
                        sessionTransac.HiddensTransact.hidClaseId = response.hidClaseId;
                        sessionTransac.HiddensTransact.hidSubClaseId = response.hidSubClaseId;
                        sessionTransac.HiddensTransact.hidTipo = response.hidTipo;
                        sessionTransac.HiddensTransact.hidClaseDes = response.hidClaseDes;
                        sessionTransac.HiddensTransact.hidSubClaseDes = response.hidSubClaseDes;
                    }
                    that.LoadBusinessRules(Session.IDSESSION, response.hidSubClaseId);
                    that.LoadDataProgramTask();
                }
            });
        },
        LoadDataProgramTask: function () {
            var that = this, param = {}, controls = this.getControls();;
            param.strIdSession = Session.IDSESSION;
            param.strTransaction = Session.IDSESSION;
            param.contractId = sessionTransac.SessionParams.DATACUSTOMER.ContractID;
            param.accountNumber = sessionTransac.SessionParams.DATACUSTOMER.Account;
            var strUrlController = that.strUrl + '/SuspensionService/HfcSuspensionService_LoadDataProgramTask';

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(param),
                url: strUrlController,
                success: function (response) {
                    var objProgramTransaction = response.ProgramTransaction;
                    var objDataInteractionTemplate = response.dataInteractionTemplate;
                    if (sessionTransac.HiddensTransact.ModoEdicion) {
                        if (sessionTransac.HiddensTransact.TipoServi == "4") {
                            controls.txtSuspensionDate.val(objProgramTransaction.SERVD_FECHAPROG);
                            controls.txtSuspensionDate.attr('disabled', true);
                        } else {
                            controls.txtReactivationDate.val(objProgramTransaction.SERVD_FECHAPROG);
                            controls.txtReactivationDate.attr('disabled', true);
                        }

                        if (response.CODIGO_INTERACCION !== "") {
                            controls.txtAmountPay.val(objDataInteractionTemplate.X_INTER_5);

                            if (objDataInteractionTemplate.X_INTER_3 === "1") {
                                controls.chkRetention.attr('checked', true);
                                controls.txtAmountRetention.val(objDataInteractionTemplate.X_INTER_7);
                            } else {
                                controls.chkRetention.attr('checked', false);
                                controls.txtAmountRetention.val("0");
                            }

                            controls.txtNotes.val(objDataInteractionTemplate.X_INTER_7);

                            controls.chkRetention.attr('disabled', true);
                            controls.txtAmountPay.attr('disabled', true);
                            controls.txtAmountRetention.attr('disabled', true);
                            controls.txtTotalPay.attr('disabled', true);
                            controls.txtNotes.attr('disabled', false);
                        }
                    }
                    else {
                        if (response.gConstMsgLineaPOTPVisble) {
                            alert(response.gConstMsgLineaPOTP, "Alerta", function() {
                                parent.window.close();
                            });
                            return;
                        }
                    }

                    that.ShowAmount();
                }
            });
        },
        ShowAmount: function () {
            var controls = this.getControls(), that = this;
            var objCheck = $('#chkRetention');

            if (objCheck.prop("checked")) {
                $('#idtdMontoRet').attr("style", "display:block");
            }
            else {
                $('#idtdMontoRet').attr("style", "display:none");
            }
        },
        LoadBusinessRules: function (userId, hdnSubClassCode) {
            var that = this,
                controls = that.getControls(),
                objRules = {};

            objRules.strIdSession = userId;
            objRules.strSubClase = hdnSubClassCode;

            $.app.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '/CommonServices/GetBusinessRules',
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
            var that = this, controls = that.getControls();
            controls.lblContract.text(sessionTransac.SessionParams.DATACUSTOMER.ContractID);
            controls.lblTypeCustomer.text(sessionTransac.SessionParams.DATACUSTOMER.CustomerType);
            controls.lblContactName.text(sessionTransac.SessionParams.DATACUSTOMER.CustomerContact);

            controls.lblCustomerName.text(sessionTransac.SessionParams.DATACUSTOMER.FullName);
            controls.lblDniRuc.text(sessionTransac.SessionParams.DATACUSTOMER.DNIRUC);
            controls.lblDateActivation.text(sessionTransac.SessionParams.DATACUSTOMER.ActivationDate);
            controls.lblLegalRepresentative.text(sessionTransac.SessionParams.DATACUSTOMER.LegalAgent);
            controls.lblDniLegalRepres.text(sessionTransac.SessionParams.DATACUSTOMER.DocumentNumber);
        },
        InitCacDat: function () {
            var that = this,
                controls = that.getControls(),
                objCacDacType = {};

            objCacDacType.strIdSession = sessionTransac.SessionParams.USERACCESS.userId;
            var parameters = {};
            parameters.strIdSession = sessionTransac.SessionParams.USERACCESS.userId;
            parameters.strCodeUser = sessionTransac.SessionParams.USERACCESS.login;

            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(parameters),
                url: '/Transactions/CommonServices/GetUsers',
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
                                //console.log"valor itemSelect: " + itemSelect);
                                $("#ddlCACDAC option[value=" + itemSelect + "]").attr("selected", true);
                            }
                        }
                    });
                }
            });
        },
        strUrl: window.location.protocol + '//' + window.location.host + '/Transactions/HFC/'
    };

    $.fn.ProgramTask = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('ProgramTask'),
                options = $.extend({}, $.fn.ProgramTask.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('ProgramTask', data);
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

    $.fn.ProgramTask.defaults = {
    }
    $('#divBody').ProgramTask();
})(jQuery);

$.urlParam = function (name) {
    var results = new RegExp('[\?&]' + name + '=([^&#]*)').exec(window.location.href);
    if (results == null) {
        return null;
    }
    else {
        return results[1] || 0;
    }
}
