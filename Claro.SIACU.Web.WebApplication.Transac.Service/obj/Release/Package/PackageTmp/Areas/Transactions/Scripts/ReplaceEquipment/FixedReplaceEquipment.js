function CloseValidation(obj, pag, controls) {
    obj.hidAccion = 'G';
    if (obj.hidAccion === 'G') {
        var sUser = obj.hidUserValidator;
        FC_GrabarCommit(pag, controls, obj.NamesUserValidator, obj.EmailUserValidator);
    }

    var mensaje;

    if (obj.hidAccion == 'F') {
        var descripcion = $("#hidDescripcionProceso_Validar").val();
        mensaje = 'La validación del usuario ingresado es incorrecto o no tiene permisos para continuar con el proceso, por favor verifiquelo.';
        if (descripcion != '') {
            mensaje = 'La validación del usuario ingresado es incorrecto o no tiene permisos para ' + descripcion + ', por favor verifiquelo.';
        }
        alert(mensaje, "Alerta");
        $("#txtUsernameAuth").val("");
        $("#txtPasswordAuth").val("");

        return;
    }
};

function FC_GrabarCommit(pag, controls, NamesUserValidator, EmailUserValidator) {
   
    $("#chkLoyalty").prop("checked", true);
    $("#txChargeAmount").html("0.00");

};

function FC_Fallo() {
    var pag = this;
    pag.FC_Fallo();

};


(function ($, undefined) {
    var Smmry = new Summary('transfer');
    var Form = function ($element, options) {
        $.extend(this, $.fn.FixedReplaceEquipment.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({

            form: $element,
            btnDissasociateEquiment: $('#btnDissasociateEquiment', $element)
            , lblContract: $('#lblContract', $element)
            , lblCustomerId: $('#lblCustomerId', $element)
            , lblCustomerType: $('#lblCustomerType', $element)
            , lblContact: $('#lblContact', $element)
            , lblCustomer: $('#lblCustomer', $element)
            , lblIdentificationDocument: $('#lblIdentificationDocument', $element)
            , lblLegalRepresent: $('#lblLegalRepresent', $element)
            , lblIdentDocLegalRepresent: $('#lblIdentDocLegalRepresent', $element)
            , lblDateActivation: $('#lblDateActivation', $element)
            , lblPlanName: $('#lblPlanName', $element)
            , lblCycleFacture: $('#lblCycleFacture', $element)
            , lblLimitCredit: $('#lblLimitCredit', $element)
            , lblAddress: $('#lblAddress', $element)
            , lblAddressNote: $('#lblAddressNote', $element)
            , lblDepartament: $('#lblDepartament', $element)
            , lblDistrict: $('#lblDistrict', $element)
            , lblCountry: $('#lblCountry', $element)
            , lblProvince: $('#lblProvince', $element)
            , lblPopulation: $('#lblPopulation', $element)
            , lblUbigeoCode: $('#lblUbigeoCode', $element)
            , lblTitle: $('#lblTitle', $element)
            , sltCACDAC: $('#sltCacDac', $element)
            //, sltSOTList: $('#sltSOTList', $element)
            , sltSOTType: $('#sltSOTType', $element)
            , sltSOTReason: $('#sltSOTReason', $element)
            , spnBadgeEquipmentAssociate: $('#spnBadgeEquipmentAssociate', $element)
            , spnBadgeEquipmentAssociateSummary: $('#spnBadgeEquipmentAssociateSummary', $element)
            , cboTypeWork: $("#cboTypeWork", $element)
            , cboMotiveSOT: $("#cboMotiveSOT", $element)
            , txtEmailSend: $('#txtEmailSend', $element)
            //, txtAmountPay: $('#txtAmountToPay', $element)
            , txtConfirmationEmail: $('#txtConfirmationEmail', $element)
            , txtNotas: $('#txtNotas', $element)
            , chkEmail: $('#chkEmail', $element)
            , btnSave: $('#btnSave', $element)
            , btnClose: $('#btnClose', $element)
            , btnConstancy: $('#btnConstancy', $element)
            , divEmailEnviar: $('#divEmailEnviar', $element)
            , divRules: $("#divRules", $element)
            , tblProductTable: $('#tblCustomerAssociateEquiment', $element)
            , tblProductsSummary: $('#tblCustomerEquipmentSummary', $element)
            , btnSummaryAccion: $('#btnSummaryAccion', $element)
            , PuntoAtencion: $('#PuntoAtencion', $element)
            , email: $('#email', $element)
            //, SOTAsociado: $('#SOTAsociado', $element)
            , TipoTrabajo: $('#TipoTrabajo', $element)
            , MotivoSot: $('#MotivoSot', $element)
            , Nota: $('#Nota', $element)
            , lblAmmountToPay: $('#lblAmmountToPay', $element)
            , tabSummary: $('#tabSummary', $element)
            , btn1stClose: $('#btn1stClose', $element)
            , btn4thClose: $('#btn4thClose', $element)
            , btn5thClose: $('#btn5thClose', $element)
            , btnNextFirstStep: $('#btnNextFirstStep', $element)
            , btnIconSummary: $('#btnIconSummary', $element)
            , tblDisassociatedEquipmentSummary: $('#tblDisassociatedEquipmentSummary', $element)
            , tblAssociatedEquipmentSummary: $('#tblAssociatedEquipmentSummary', $element)
            , lblErrorMessage: $('#lblErrorMessage', $element)
            , txtReferencia: $('#txtReferencia', $element)
            , lblNode: $("#lblNode", $element)
            , lblNodeNew: $("#lblNodeNew", $element)
            , TelefonoReferencia: $('#TelefonoReferencia', $element)
            , chkLoyalty: $('#chkLoyalty', $element)
            , txChargeAmount: $('#txChargeAmount', $element)
            , AmmountToPay: $('#AmmountToPay', $element)
        });
    }
    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this,
                controls = this.getControls();
            controls.lblNodeNew.html('Nodo <span class="badge">' + controls.lblNode.get(0).outerHTML + '</span>');
            controls.chkEmail.addEvent(that, 'change', that.chkEmail_change);
            controls.btnDissasociateEquiment.addEvent(that, 'click', that.btnDissasociateEquiment_Click_Pop);
            controls.cboTypeWork.addEvent(that, 'change', that.cboTypeWork_change);
            controls.btn1stClose.addEvent(that, 'click', that.btn1stClose_click);
            controls.btn4thClose.addEvent(that, 'click', that.btn4thClose_click);
            controls.btn5thClose.addEvent(that, 'click', that.btn5thClose_click);
            controls.btnSave.addEvent(that, 'click', that.btnSave_click);
            controls.btnIconSummary.addEvent(that, 'click', that.btnIconSummary_click);
            controls.btnConstancy.addEvent(that, 'click', that.btnConstancy_click);
            controls.txtReferencia.addEvent(that, 'keyup', that.onlyNumbers_Keyup);
            controls.chkLoyalty.addEvent(that, 'change', that.f_ValidaterPermLoyalty);
            $('.next-trans').on('click', function (e) {
                that.shortCutStep(e);
            });

            $('.prev-trans').on('click', function (e) {
                that.shortCutPrev(e);
            });
 
            $('.array-trans').on('click', function (e) {
                that.arrayAct(e);
            });
            
            
            controls.lblTitle.text("Reposición de Equipo");
            that.loadingPage('Cargando datos');
            that.loadSessionData();
            that.FixedReplaceEquipmentLoad().done(function (data) { that.render(); });
            

        },
        loadIgv: function () {
            var that = this,
               controls = this.getControls();
            param = {};
            param.strIdSession = Session.IDSESSION;
            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(param),
                url: '/Transactions/CommonServices/GetConsultIGV',
                success: function (response) {
                    if (response.data != null) {
                        that.strIGV = response.data.igvD;
                    }
                    else {
                        alert(that.objReplaceEquipmentLoad.objReplaceEquipmentMessageModel.strMsjValidacionCampo.replace("###", 'IGV'), 'Alerta');
                    }
                },
                error: function (errormessage) {
                    alert(that.objReplaceEquipmentLoad.objReplaceEquipmentMessageModel.strMsjErrorPeticionAJAX.replace("###", 'IGV'), 'Error');
                }
            });
        },
        loadSessionData: function () {
            var SessionTransac = JSON.parse(sessionStorage.getItem("SessionTransac"));
            Session.DATACUSTOMER = SessionTransac.SessionParams.DATACUSTOMER;
            Session.IDSESSION = SessionTransac.UrlParams.IDSESSION;

            Session.DATASERVICE = SessionTransac.SessionParams.DATASERVICE;
            Session.USERACCESS = SessionTransac.SessionParams.USERACCESS;
            Session.UrlParams = SessionTransac.UrlParams;
        },
        FixedReplaceEquipmentLoad: function () {
            var that = this,
                objRequestDataModel = {};
            objRequestDataModel.strIdSession = Session.IDSESSION;
            objRequestDataModel.strCodeUser = Session.USERACCESS.login;
            objRequestDataModel.strIdContract = Session.DATACUSTOMER.ContractID;
            objRequestDataModel.strStateLine = Session.DATASERVICE.StateLine;
            objRequestDataModel.strCustomerId = Session.DATACUSTOMER.CustomerID;
            objRequestDataModel.strTypeProduct = Session.DATACUSTOMER.ProductType;
            return $.app.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '/Transactions/Fixed/ReplaceEquipment/FixedReplaceEquipmentLoad',
                data: JSON.stringify(objRequestDataModel),
                success: function (response) {
                    if (response.data != null) {
                        that.objReplaceEquipmentLoad = response.data;
                    }
                },
                error: function (errormessage) {
                    alert(that.objReplaceEquipmentLoad.objReplaceEquipmentMessageModel.strMsjErrorAlCargar.replace("###", 'los datos'), 'Error');
                }
            });
        },
        initCustomerProductsSummary_DataTable: function () {
            var that = this, controls = that.getControls();
            controls.tblProductsSummary.DataTable({
                "columnDefs": [
                    {
                        "targets": [0, 8, 11, 12, 13, 14, 15],
                        "visible": false

                    },
                    {
                        "targets": '_all',
                        "sortable": false
                    }
                ],
                "orderFixed": [[0, 'asc']],
                "rowGroup": {
                    "dataSrc": [0]
                },
                "columns": [
                    { "data": "EquipmentServiceType" },
                    { "data": "EquipmentSeriesNumber", "className": "fldEquipmentSeriesNumber" },
                    {
                        "data": "EquipmentDescription",
                        "width": '35%'
                    },
                    {
                        "data": "EquipmentType",
                        "width": '12%'
                    },
                    { "data": "DecoType", "defaultContent": "" },
                    { "data": "EquipmentMACAddress", "defaultContent": "" },
                    { "data": "NumberPhone", "defaultContent": "" },
                    { "data": "EquipmentOC" },
                    { "data": "EquipmentCodeType" },
                    { "data": "Action" },
                    {
                        "data": undefined, "render": function (data, type, row, meta) {
                            if (data == undefined) {
                                var strDisabled = "", strClassAdded = "";
                                if (row.Action === "Desasociar") {
                                    strDisabled = "disabled";
                                    strClassAdded = "btn-Edit-Disassociate";
                                }
                                var buttons =
                                    "<center><button class='btn btn-info btn-xs btn-edit-equipment " + strClassAdded + " ' style='margin-right:5px;'" + strDisabled + "><span class='fa fa-pencil'></spam></button>" +
                                        "<button class='btn btn-danger btn-xs btn-remove-equipment' ><span class='fa fa-trash'></spam></button></center>";
                                return buttons;
                            }
                            return data;
                        }
                    },
                    { "data": "OperationCode" },
                    { "data": "EquipmentTypeBD" },
                    { "data": "EquipmentTypeCodeBD" },
                    { "data": "EquipmentCodINSSRV" },                
                    { "data": "EquipmentAssociate" },
                    { "data": "Penalidad", "defaultContent": "0.00" }
                ],
                "drawCallback": function (settings) {
                    var api = this.api();
                    var rows = api.rows({ page: 'current' }).nodes();
                    var last = null;
                    var groupadmin = [];
                    api.column(0, { page: 'current' }).data().each(function (group, i) {
                        if (last !== group && groupadmin.indexOf(i) == -1) {
                            $(rows).eq(i).before(
                                '<tr class="group" id="' + i + '"><td colspan="12" style="background-color: #3697af; color:white; cursor:default;"><span class="badge-ce badge-ce-item" id="spnCountSummary' + group.substring(0, 2) + '" >0</span>' + group + '</td></tr>'
                            );
                            groupadmin.push(i);
                            last = group;
                        }
                    });
                    for (var k = 0; k < groupadmin.length; k++) {
                        $("#" + groupadmin[k]).nextUntil("#" + groupadmin[k + 1]).addClass(' group_' + groupadmin[k]);
                        $("#" + groupadmin[k]).click(function () {
                            that.rowEquipmentSelected = {};
                        });
                    }
                },
                "language": {
                    "lengthMenu": "Mostrar _MENU_ equipos por página",
                    "zeroRecords": "No existen datos",
                    "processing": "<img src=" + that.strUrlLogo + " width='25' height='25' /> Cargando ... </div>",
                    "info": " ",
                    "infoEmpty": " ",
                    "emptyTable": "No existen datos",
                    "infoFiltered": "(filtered from _MAX_ total records)"
                },
                "info": false,
                "scrollX": true,
                "scrollY": 300,
                "scrollCollapse": true,
                "paging": false,
                "searching": false,
                "destroy": true
            });
            var table = controls.tblProductsSummary.DataTable();
            table.on('rowgroup-datasrc', function (e, dt, val) {
                table.order.fixed({ pre: [[val, 'asc']] }).draw();
            });
            $('#tblCustomerEquipmentSummary tbody').on('click', '.btn-remove-equipment', function (e) {
                var data = table.row($(this).parents('tr')).data();
                confirm('¿Está seguro de quitar este equipo y sus asociados?', 'Confirmar', function (result) {
                    if (result) {
                        that.removeEquipmentFromSummaryTable(data);
                        that.updateBadgesPerServiceType(that.dataSummaryEquipment, true);

                    }
                });
            });
            $('#tblCustomerEquipmentSummary tbody').on('click', '.btn-edit-equipment', function (e) {
                that.strCurrentEquipmentToEdit = "";
                var index = table.row($(this).parents('tr')).index();
                var data = table.row($(this).parents('tr')).data();
                that.strCurrentEquipmentToEdit = table.cell(index, 1).data();
                that.editSeriesNumberField(index, data, true);
                that.updateBadgesPerServiceType(that.dataSummaryEquipment, true);

            });
            $('#tblCustomerEquipmentSummary tbody').on('click', '.btn-cancel-edit-equipment', function (e) {
                var index = table.row($(this).parents('tr')).index();
                that.editSeriesNumberField(index, '', false);
                that.updateBadgesPerServiceType(that.dataSummaryEquipment, true);
            });
            $('#tblCustomerEquipmentSummary tbody').on('click', '.btn-confirm-edit-equipment', function (e) {
                var index = table.row($(this).parents('tr')).index();
                var data = table.row($(this).parents('tr')).data();
                that.replaceRowFromSummary(index, data);
            });

        },
        replaceRowFromSummary: function (index, data) {
            var that = this, controls = that.getControls();
            var table = controls.tblProductsSummary.DataTable();
            var newSerieInput = $('#txtEditEquipment').val();
            if (newSerieInput != "") {
                if (newSerieInput != that.strCurrentEquipmentToEdit) {
                    that.ValidateEquipment(data.EquipmentCodeType,
                        newSerieInput,
                        false,
                        index,
                        data.EquipmentCodINSSRV,
                        data.DecoType);
                } else {
                    $('#idtab1').focus();
                    alert(that.objReplaceEquipmentLoad.objReplaceEquipmentMessageModel.strSeriesNumberNotEqual, "Alerta");
                    controls.tblProductsSummary.focus();
                    return false;
                }

            } else {
                $('#txtEditEquipment').focus();
                alert('No ha ingresado la serie del equipo', 'Alerta');
                return false;
            }
        },
        editSeriesNumberField: function (index, data, isEditing) {
            var that = this, controls = that.getControls();
            var table = controls.tblProductsSummary.DataTable();
            var buttons = "", contentField = "";
            if (isEditing) {
                buttons = "<center>" +
                    "<button class='btn btn-success btn-xs btn-confirm-edit-equipment' style='margin-right:5px;'><span class='fa fa-check'></spam></button>" +
                    "<button class='btn btn-danger btn-xs btn-cancel-edit-equipment'><span class='fa fa-times'></spam></button>" +
                    "</center>";
                contentField = '<input class="form-control input-sm active" type="text"  id="txtEditEquipment"  placeholder="' + that.strCurrentEquipmentToEdit + '" style="font-size:9pt;"/>';
                that.disabled_buttonOperations(isEditing);
            }
            else {
                buttons = "<center>" +
                    "<button class='btn btn-info btn-xs btn-edit-equipment' style='margin-right:5px;'><span class='fa fa-pencil'></spam></button>" +
                    "<button class='btn btn-danger btn-xs btn-remove-equipment' ><span class='fa fa-trash'></spam></button>" +
                    "</center>";
                contentField = that.strCurrentEquipmentToEdit;
                that.disabled_buttonOperations(isEditing);
            }
            table.cell(index, 1).data(contentField).draw();
            table.cell(index, 10).data(buttons).draw(true);
            if (isEditing) {
                $('#txtEditEquipment').addEvent($('#txtEditEquipment'), 'keyup', that.txtAssociateFieldEdit_keyPress);
                $('#txtEditEquipment').focus();
            }
        },
        txtAssociateFieldEdit_keyPress: function (e) {
            var regxNumber = /^[a-zA-Z0-9_]+$/;
            var text = e.val();
            var blvalidateNumber = regxNumber.test(text);
            e.val(text.toUpperCase());
            var textLenght = text.length;
            if (blvalidateNumber == false && text.length != 0 || textLenght > 50) {
                var valor = text.substring(0, text.length - 1);
                e.val(valor);
                e.focus();
            }
        },
        disabled_buttonOperations: function (isEditing) {
            var that = this, controls = that.getControls();
            $('.btn-edit-equipment').prop('disabled', isEditing);
            $('.btn-remove-equipment').prop('disabled', isEditing);
            controls.btnNextFirstStep.prop('disabled', isEditing);
            controls.btnDissasociateEquiment.prop('disabled', isEditing);
            if (!isEditing) $('.btn-Edit-Disassociate').prop('disabled', true);
        },
        mouseover: function (ctrl) {
            var hoverColor = 'rgba(107, 197, 225, 1)';
            var idElement = ctrl[0].id;
            if (idElement.indexOf('x') != -1) {
                $("#" + idElement).css({ 'background-color': hoverColor });
                $("#" + idElement.substr(0, idElement.length - 1)).css({ 'background-color': hoverColor });
            }
            else {
                $("#" + idElement).css({ 'background-color': hoverColor });
                $("#" + idElement + "x").css({ 'background-color': hoverColor });
            }
        },
        mouseout: function (ctrl) {
            var mouseOutColor = 'white';
            var idElement = ctrl[0].id;
            if (idElement.indexOf('x') != -1) {
                $("#" + idElement).css({ 'background-color': mouseOutColor });
                $("#" + idElement.substr(0, idElement.length - 1)).css({ 'background-color': mouseOutColor });
            }
            else {
                $("#" + idElement).css({ 'background-color': mouseOutColor });
                $("#" + idElement + "x").css({ 'background-color': mouseOutColor });
            }
        },
        shortCutStep: function (e) {
            var that = this;
            if ((e.ctrlKey && e.keyCode == 39) || e.keyCode == null) {
                var $activeTab = $('.step.tab-pane.active');
                that.validationsSteps($activeTab.prop('id'), function (response) {
                    if (response) {
                        var $nextBtn = $('.next-step');
                        navigateTabs($nextBtn);
                    } else {
                        return false;
                    }
                });
            }
        },

        validationsSteps: function (stepName, fn) {
            var that = this,
                controls = this.getControls();

            var objMsg = that.objReplaceEquipmentLoad.objReplaceEquipmentMessageModel;

            if (stepName == 'tabDisAssociation') {
                if (that.dataSummaryEquipment.length <= 0) {
                    alert(objMsg.strMessageSummary.replace("###", "Resumen de equipos a Desasociar y Asociar"), 'Alerta');

                    fn(false);
                    return false;
                }
            }

            if (stepName == 'tabTechnicalInfo') {
                that.SummaryEmail();
                that.SummarychkEmail();
                that.SummaryPuntoAtencion();
                //that.SummaryPSOTAsociado();
                that.SummaryMotivoSot();
                that.SummaryTipoTrabajo();
                that.SummaryNota();
                that.SummarylblAmmountToPay();
                that.SummaryTelefonoReferencia();

                //if (controls.sltSOTList.val() == '' || controls.sltSOTList.val() == null) {
                //    alert(objMsg.strMsjValidacionCampoSlt.replace("###", "SOT Asociado"), 'Alerta');
                //    fn(false);
                //    return false;
                //}
                if (controls.cboTypeWork.val() == '' || controls.cboTypeWork.val() == null) {
                    alert(objMsg.strMsjValidacionCampoSlt.replace("###", "Tipo de Trabajo"), 'Alerta');
                    fn(false);
                    return false;
                }
                if (controls.cboMotiveSOT.val() == '' || controls.cboMotiveSOT.val() == null) {
                    alert(objMsg.strMsjValidacionCampoSlt.replace("###", "Motivo de SOT"), 'Alerta');
                    fn(false);
                    return false;
                }
                if (controls.chkEmail.prop('checked')) {
                    if (controls.txtEmailSend.val() == '') {
                        alert(objMsg.strMsjValidacionCampo.replace("###", "E-Mail"), 'Alerta');
                        fn(false);
                        return false;
                    }
                    var regx = /^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$/;
                    var blvalidate = regx.test(controls.txtEmailSend.val());
                    if (blvalidate == false) {
                        alert(objMsg.strMsjValidacionCampoFormato.replace("###", "E-Mail"), 'Alerta');
                        fn(false);
                        return false;
                    }
                }
                //if (controls.sltCACDAC.val() == '' || controls.sltCACDAC.val() == null) {
                //    alert(objMsg.strMsjValidacionCampoSlt.replace("###", "Punto de Atencion"), 'Alerta');
                //    fn(false);
                //    return false;
                //}

                that.fillArraysForSummary();

                $(".paint").addEvent(that, 'mouseover', that.mouseover);
                $(".paint").addEvent(that, 'mouseout', that.mouseout);
            }

            fn(true);
            return true;
        },
        shortCutPrev: function (e) {
            var that = this;
            if ((e.ctrlKey && e.keyCode == 37) || e.keyCode == null) {
                var $prevBtn = $('.prev-step');
                navigateTabs($prevBtn);
            }
        },
        arrayAct: function (e) {
            var that = this;
            that.fillArraysForSummary();
        },
        fillProductsTable_DataTable: function (customerProductsTable, data) {
            var that = this,
                controls = that.getControls();
            customerProductsTable.DataTable({
                "columnDefs": [
                    {
                        "targets": [1, 10, 11, 12, 13, 14, 15],
                        "visible": false,
                        "searchable": false
                    },
                    {
                        "targets": '_all',
                        "sortable": false,
                        "searchable": false
                    },

                    {
                        "targets": [9],
                        "render": function (data, type, row, meta) {
                            var element =
                                '<a   style="background-color: ###; color:white;border-radius:13px; padding:4px" ><span class="fa fa-link" ><span></a>';
                            return (data % 2) > 0
                                ? element.replace("###", "#0097ae")
                                : element.replace("###", "#555");
                        }
                    },
                    {
                        'targets': [0],
                        'searchable': false,
                        'orderable': false,
                        'sortable': false,
                        'className': 'dt-body-center',
                        'render': function (data, type, full, meta) {

                            return '<center><span class="btnAddEquipmentToDisassociate" id="btnAddEquipmentToDisassociate' + meta.row + '" aria-hidden="true"></span></center>';
                        }
                    }
                ],
                "columns": [
                    {
                        "data": undefined,
                        "sortable": false,
                        "searchable": false,
                        "defaultContent":
                            "<center><input name='rdCustomerAssociateEquipment' type='radio' id='rdbEquipmentList' class='rdbSelectedRowEquipment'></center>"
                    },
                    { "data": "EquipmentServiceType" },
                    { "data": "EquipmentSeriesNumber" },
                    { "data": "EquipmentDescription" },
                    {
                        "data": "EquipmentType",
                        "width": '12%'
                    },
                    {
                        "data": "DecoType", "defaultContent": ""
                    },
                    { "data": "EquipmentMACAddress", "defaultContent": "" },
                    { "data": "NumberPhone", "defaultContent": "" },
                    { "data": "EquipmentOC", "defaultContent": "" },
                    { "data": "EquipmentAssociate" },
                    { "data": "EquipmentCodeType" },
                    { "data": "Action" },
                    { "data": "OperationCode" },
                    { "data": "EquipmentTypeBD" },
                    { "data": "EquipmentTypeCodeBD" },
                    { "data": "EquipmentCodINSSRV" },
                    { "data": "Penalidad", "defaultContent": "0.00"}
                ],
                "data": data,
                "drawCallback": function (settings) {
                    var api = this.api();
                    var rows = api.rows({ page: 'current' }).nodes();
                    var last = null;
                    var groupadmin = [];
                    api.column(1, { page: 'current' }).data().each(function (group, i) {
                        if (last !== group && groupadmin.indexOf(i) == -1) {
                            $(rows).eq(i).before(
                                '<tr class="group" id="' + i + '"><td colspan="13" style="background-color: #3697af; color:white;"><span class="badge-ce badge-ce-item" id="spnCount' + group.substring(0, 2) + '">0</span>' + group + '</td></tr>'
                            );
                            groupadmin.push(i);
                            last = group;
                        }
                    });
                    for (var k = 0; k < groupadmin.length; k++) {
                        $("#" + groupadmin[k]).nextUntil("#" + groupadmin[k + 1]).addClass(' group_' + groupadmin[k]);
                        $("#" + groupadmin[k]).click(function () {
                            that.rowEquipmentSelected = {};
                        });
                    }
                },
                "language": {
                    "lengthMenu": "Mostrar _MENU_ equipos por página",
                    "zeroRecords": "No existen datos",
                    "processing": "<img src=" + that.strUrlLogo + " width='25' height='25' /> Cargando ... </div>",
                    "info": " ",
                    "infoEmpty": " ",
                    "emptyTable": "No existen datos",
                    "infoFiltered": "(filtered from _MAX_ total records)"
                },
                "select": {
                    "style": 'os',
                    "selector": 'td:first-child'
                },
                "info": false,
                "scrollX": true,
                "scrollY": 280,
                "scrollCollapse": true,
                "paging": false,
                "searching": false,
                "destroy": true
            });
            var table = controls.tblProductTable.DataTable();
            that.updateBadgesPerServiceTypeInit(data, false);
            table.on('rowgroup-datasrc', function (e, dt, val) {
                table.order.fixed({ pre: [[val, 'asc']] }).draw();
            });
            $('#tblCustomerAssociateEquiment').on('onblur', '', function (e) {
                var $row = $(this).closest('tr');
                $row.css('cursor', 'pointer');
            });
            $('#tblCustomerAssociateEquiment').on('click', 'tbody td, thead th:first-child', function (e) {
                var $row = $(this).closest('tr');
                var data = table.row($row).data();
                var index = table.row($row).index();
                that.rowEquipmentSelected = data;
                if (that.checked) {
                    $row.addClass('selected');
                } else {
                    $row.removeClass('selected');
                }
                $('.btnAddEquipmentToDisassociate').removeClass('btnAddEquipmentToDisassociate-active fa fa-check selected');
                $('#btnAddEquipmentToDisassociate' + index).toggleClass('btnAddEquipmentToDisassociate-active fa fa-check');
                if ($('.btnAddEquipmentToDisassociate').hasClass('btnAddEquipmentToDisassociate-selected')) $('.btnAddEquipmentToDisassociate-selected').addClass('fa fa-check');
                e.stopPropagation();
            });
        },

    setControls: function (value) {
        this.m_controls = value;
    },
    getControls: function () {
        return this.m_controls || {};
    },
    setSessionData: function () {
        var that = this,
            controls = this.getControls();
            
        controls.lblNodeNew.html('Nodo <span class="badge">' + controls.lblNode.get(0).outerHTML + '</span>');
        controls.lblContact.html((Session.DATACUSTOMER.CustomerContact == null) ? '' : Session.DATACUSTOMER.CustomerContact);
        controls.lblCustomerId.html((Session.DATACUSTOMER.CustomerID == null) ? '' : Session.DATACUSTOMER.CustomerID);
        controls.lblCustomerType.html((Session.DATACUSTOMER.CustomerType == null) ? '' : Session.DATACUSTOMER.CustomerType.toUpperCase());
        controls.lblContract.html((Session.DATACUSTOMER.ContractID == null) ? '' : Session.DATACUSTOMER.ContractID);
        controls.lblCustomer.html((Session.DATACUSTOMER.FullName == null) ? '' : Session.DATACUSTOMER.FullName);
        controls.lblIdentificationDocument.html((Session.DATACUSTOMER.DNIRUC == null) ? '' : Session.DATACUSTOMER.DNIRUC);
        controls.lblDateActivation.html((Session.DATACUSTOMER.ActivationDate == null) ? '' : Session.DATACUSTOMER.ActivationDate);
        controls.lblPlanName.html((Session.DATASERVICE.Plan == null) ? '' : Session.DATASERVICE.Plan);
        controls.lblCycleFacture.html((Session.DATACUSTOMER.BillingCycle == null) ? '' : Session.DATACUSTOMER.BillingCycle);
        controls.lblLimitCredit.html((Session.DATACUSTOMER.objPostDataAccount.CreditLimit == null) ? '' : Session.DATACUSTOMER.objPostDataAccount.CreditLimit);

        controls.lblLegalRepresent.html((Session.DATACUSTOMER.LegalAgent == null) ? '' : Session.DATACUSTOMER.LegalAgent);
        controls.lblIdentDocLegalRepresent.html((Session.DATACUSTOMER.DocumentNumber == null) ? '' : Session.DATACUSTOMER.DocumentNumber);
        controls.lblAddress.html((Session.DATACUSTOMER.Address == null) ? '' : Session.DATACUSTOMER.Address);
        controls.lblDepartament.html((Session.DATACUSTOMER.Departament == null) ? '' : Session.DATACUSTOMER.Departament);
        controls.lblProvince.html((Session.DATACUSTOMER.Province == null) ? '' : Session.DATACUSTOMER.Province);
        controls.lblDistrict.html((Session.DATACUSTOMER.District == null) ? '' : Session.DATACUSTOMER.District);
        controls.lblCountry.html((Session.DATACUSTOMER.LegalCountry == null) ? '' : Session.DATACUSTOMER.LegalCountry.toUpperCase());
        controls.lblPopulation.html((Session.DATACUSTOMER.CodeCenterPopulate == null) ? '' : Session.DATACUSTOMER.CodeCenterPopulate);
        controls.lblUbigeoCode.html((Session.DATACUSTOMER.InstallUbigeo == null) ? '' : Session.DATACUSTOMER.InstallUbigeo);
        controls.txtEmailSend.val((Session.DATACUSTOMER.Email == null) ? '' : Session.DATACUSTOMER.Email);
        controls.txtEmailSend.prop("disabled", false);
        controls.btnConstancy.prop("disabled", true);
        controls.btnSave.prop("disabled", false);
        that.strCheckEmail = 'NO';
    },
    render: function () {
        var that = this,
            controls = this.getControls();
        that.setSessionData();

        //Validacion de estados del Contrato y Linea
        var objMsg = that.objReplaceEquipmentLoad.objReplaceEquipmentMessageModel;
        if (Session.DATASERVICE.StateLine == objMsg.strVariableEmpty){
            alert(objMsg.strMsgVariableEmpty, 'Alerta', function () { parent.window.close(); }); return;
        }
        if (objMsg.strStateLine.indexOf(Session.DATASERVICE.StateLine) != '-1') {
            alert(objMsg.strMsgStateLine.replace('###', Session.DATASERVICE.StateLine), 'Alerta', function () { parent.window.close(); }); return;
        }
        controls.chkLoyalty.prop("disabled", true);
        if (that.objReplaceEquipmentLoad.lstCacDacTypes != null && that.objReplaceEquipmentLoad.lstCacDacTypes.length > 0)
            that.getCacDacType(that.objReplaceEquipmentLoad.lstCacDacTypes, that.objReplaceEquipmentLoad.strUserCac);
        else {
            alert(that.objReplaceEquipmentLoad.strValidationChargueList.replace("###", "Punto de Atención"), 'Alerta', function () { });
            //that.disabledElement(true, true);
        }

        if (that.objReplaceEquipmentLoad.lstBusinessRules != null)
            that.getBusinessRules(that.objReplaceEquipmentLoad.lstBusinessRules);
        else {
            alert(that.objReplaceEquipmentLoad.strValidationChargueList.replace("###", "Reglas de Negocio"), 'Alerta', function () { });
            that.disabledElement(true, true);
        }
      
        if (that.objReplaceEquipmentLoad.lstServiceTypes != null)
            that.lstServiceTypes = that.objReplaceEquipmentLoad.lstServiceTypes;
        else {
            alert(objMsg.strMsjValidacionCargadoListado.replace("###", "Tipos de servicio"), 'Alerta', function () { });
            that.disabledElement(true, true);
        }
        //if (that.objReplaceEquipmentLoad.strAmountToPay != null)
        //    controls.txtAmountPay.val(that.objReplaceEquipmentLoad.strAmountToPay);

        if (that.objReplaceEquipmentLoad.lstEquimentAssociate != null && that.objReplaceEquipmentLoad.lstEquimentAssociate.length > 0) {
            that.fillProductsTable_DataTable(controls.tblProductTable, that.objReplaceEquipmentLoad.lstEquimentAssociate);
            that.initCustomerProductsSummary_DataTable();
            controls.spnBadgeEquipmentAssociate.html(that.objReplaceEquipmentLoad.lstEquimentAssociate.length);
            controls.spnBadgeEquipmentAssociateSummary.html(0);
        } else {
            alert(objMsg.strMsjValidacionCargadoListado.replace("###", "equipos asociados al cliente"), 'Alerta', function () { });
        }
        //if (that.objReplaceEquipmentLoad.lstSOTListypes != null && that.objReplaceEquipmentLoad.lstSOTListypes.length > 0){
        //    that.getsltSOTList(that.objReplaceEquipmentLoad.lstSOTListypes);
        //} else {
        //    alert(objMsg.strMsjValidacionCargadoListado.replace("###", "SOT Asociado"), 'Alerta', function () { });
        //}
        if (that.objReplaceEquipmentLoad.lstTypeWork != null && that.objReplaceEquipmentLoad.lstTypeWork.length > 0){
            that.getTypeWork(that.objReplaceEquipmentLoad.lstTypeWork);
        } else {
            alert(objMsg.strMsjValidacionCargadoListado.replace("###", "Tipo de Trabajo"), 'Alerta', function () { });
        }
        if (that.objReplaceEquipmentLoad.lstMotiveSOTByTypeJob != null && that.objReplaceEquipmentLoad.lstMotiveSOTByTypeJob.length > 0){
            that.GetMotiveSOTByTypeJob(that.objReplaceEquipmentLoad.lstMotiveSOTByTypeJob);
        } else {
            alert(objMsg.strMsjValidacionCargadoListado.replace("###", "Motivo de SOT"), 'Alerta', function () { });
        }

        if (that.objReplaceEquipmentLoad.strServidorLeerPDF != null)
            that.strServidorLeerPDF = that.objReplaceEquipmentLoad.strServidorLeerPDF;

        $('#divErrorAlert').hide();
        $('html, body').css('overflow-x', 'hidden');
        that.loadResumeEquipmentsAsoDataTable();
        that.loadResumeEquipmentsDesDataTable();

        Smmry.set('AmmountToPay', '');
        Smmry.set('chkemail_A', '');
        Smmry.set('email', '');
        Smmry.set('PuntoAtencion', '');
        Smmry.set('Nota', '');
        Smmry.set('MotivoSot', '');
        Smmry.set('TipoTrabajo', '');
        //Smmry.set('SOTAsociado', '');
        Smmry.set('TelefonoReferencia', '');
    },
    btnIconSummary_click: function () {
        var that = this;
        that.fillArraysForSummary();
    },
    onlyNumbers_Keyup: function () {
        var that = this,
            controls = that.getControls();
        if (controls.txtReferencia.val().match(/[^0-9]/g)) {
            controls.txtReferencia.val(controls.txtReferencia.val().replace(/[^0-9]/g, ''));
        }
    },
    loadResumeEquipmentsAsoDataTable: function (dataSummaryEquipment) {
        var that = this,
            controls = this.getControls();
        controls.tblAssociatedEquipmentSummary.DataTable({
            "data": dataSummaryEquipment,
            "columnDefs": [
                {
                    "targets": '_all',
                    "background": 'white'
                },
                {
                    "targets": 0,
                    "visible": false
                },
                {
                    "targets": 1,
                    "sortable": false,
                    "searchable": false,
                    "width": '30%'
                },
                {
                    "targets": 2,
                    "sortable": false,
                    "searchable": false,
                    "width": "50%"
                },
                {
                    "targets": 3,
                    "sortable": false,
                    "searchable": false
                },
                {
                    "targets": 4,
                    "sortable": false,
                    "searchable": false
                 }
            ],
            "order": [[0, 'asc']],
            "columns": [
                { "data": "OperationCode" },
                { "data": "EquipmentSeriesNumber" },
                { "data": "EquipmentDescription" },
                { "data": "EquipmentType" },
                {
                    "data": undefined,
                    "render": function (data, type, row, meta) {
                        var element = "<center><a class='btn-details-datatable btn-details-datatable-associate' ><span class='fa fa-angle-down'></span></a></center>";
                        return element;
                    }
                }
            ],
            "language": {
                "lengthMenu": "Mostrar _MENU_ equipos por página",
                "zeroRecords": "No existen datos",
                "processing": "<img src=" + that.strUrlLogo + " width='25' height='25' /> Cargando ... </div>",
                "info": " ",
                "infoEmpty": " ",
                "emptyTable": "No existen datos",
                "infoFiltered": "(filtered from _MAX_ total records)"
            },
            createdRow: function (row, data, index) {
                $(row).addClass('paint');
                $(row).css('background', 'white');
                $(row).attr('id', data.OperationCode + data.EquipmentType.replace(/ /g, "") + "x");
            },
            "info": false,
            "scrollX": true,
            "scrollY": false,
            "scrollCollapse": true,
            "paging": false,
            "searching": false,
            "destroy": true,
            "select": {
                "style": 'multi',
                "info": false
            }
        });

        $('.btn-details-datatable-associate').on('click', function (e) {
            var tr = $(this).closest('tr');
            var row = $('#tblAssociatedEquipmentSummary').DataTable().row(tr);
            var associateRow = {}, trElementRowAssociate = {}, isDisassociateTable = false;
            var currentRowId = tr[0].id;
            var data = {};
            trElementRowAssociate = $("#" + currentRowId.substr(0, currentRowId.length - 1));
            associateRow = $('#tblDisassociatedEquipmentSummary').DataTable().row(trElementRowAssociate);
            data = that.getAssociateItem(null, null, true, associateRow.data().EquipmentSeriesNumber);

            that.showDetailRow(row, tr, data, false);
            that.showDetailRow(associateRow, trElementRowAssociate, data, true);
        });
    },
    showDetailRow: function (row, tr, data, isDisassociateTable) {
        var title = isDisassociateTable ? "Equipo Asociado" : "Equipo al que se asociará";
        var that = this;
        if (row.child.isShown()) {
            $('div.slider', row.child()).slideUp(function () {
                row.child.hide();
                tr.removeClass('shown');
            });
            $('#' + tr[0].id + ' .btn-details-datatable').css({ 'transform': 'rotate(360deg)' });
        }
        else {
            row.child(that.formatSummaryRowDetail(data, title), 'tr-detail-row').show();
            tr.addClass('shown');
            $('#' + tr[0].id + ' .btn-details-datatable').css({ 'transform': 'rotate(180deg)' });
            $('div.slider', row.child()).slideDown();
        }
    },
    formatSummaryRowDetail: function (item, title) {
        var backgroundColor = 'rgba(213, 238, 246, 1)';
        var row = '<div class="slider" style="width:100%;background-color:' + backgroundColor + ';">' +
            '<table style="width:100%; background-color:' + backgroundColor + ';" >' +
            '<tr style="background-color:' + backgroundColor + ';">' +
            '<td colspan="3"><strong>' + title + '</strong></td>' +
            '</tr>' +
            '<tr style="background-color:' + backgroundColor + ';">' +
            '<td style="width:31%;vertical-align: super;">' + item.EquipmentSeriesNumber + '</td>' +
            '<td style="width:36%;vertical-align: super;padding-left:8px;">' + item.EquipmentDescription + '</td>' +
            '<td style="width:31%;vertical-align: super;padding-left:5px;">' + item.EquipmentType + '</td>' +
            '</tr>' +
            '</table>' +
            '</div>';
        $('#tblDisassociatedEquipmentSummary tbody .odd td:first,#tblAssociatedEquipmentSummary tbody .odd td:first').css('width', '30%');
        return row;
    },
    loadResumeEquipmentsDesDataTable: function (dataSummaryEquipment) {
        var that = this,
            controls = this.getControls();
        controls.tblDisassociatedEquipmentSummary.DataTable({
            "data": dataSummaryEquipment,
            "columnDefs": [
                {
                    "targets": 0,
                    "visible": false
                },
                {
                    "targets": 1,
                    "sortable": false,
                    "searchable": false,
                    "width": "30%"
                },
                {
                    "targets": 2,
                    "sortable": false,
                    "searchable": false,
                    "width": "50%"
                },
                {
                    "targets": 3,
                    "sortable": false,
                    "searchable": false
                }
            ],
            "order": [[0, 'asc']],
            "columns": [
                { "data": "OperationCode" },
                { "data": "EquipmentSeriesNumber" },
                { "data": "EquipmentDescription" },
                { "data": "EquipmentType" },
                {
                        "data": undefined,
                        "className": "cell-show-details",
                        "render": function (data, type, row, meta) {
                            var element =
                                "<center><a class='btn-details-datatable'><span class='fa fa-angle-down'></span></a></center>";
                            return element;
                    },
                    "orderable": false,
                    "sortable": false
                }
            ],
            "language": {
                "lengthMenu": "Mostrar _MENU_ equipos por página",
                "zeroRecords": "No existen datos",
                "processing": "<img src=" + that.strUrlLogo + " width='25' height='25' /> Cargando ... </div>",
                "info": " ",
                "infoEmpty": " ",
                "emptyTable": "No existen datos",
                "infoFiltered": "(filtered from _MAX_ total records)"
            },
            createdRow: function (row, data, index) {
                $(row).addClass('paint');
                $(row).css('background', 'white');
                $(row).attr('id', data.OperationCode + data.EquipmentType.replace(/ /g, ""));
            },
            "info": false,
            "scrollX": true,
            "scrollY": false,
            "scrollCollapse": true,
            "paging": false,
            "searching": false,
            "destroy": true,
            "sort": false
        });

        $('.btn-details-datatable').on('click', function (e) {
            var tr = $(this).closest('tr');
            var currentRowId = tr[0].id;
            var row = $('#tblDisassociatedEquipmentSummary').DataTable().row(tr);
            var associateRow = {}, trElementRowAssociate = {}, isDisassociateTable = true;
            var data = {};

            trElementRowAssociate = $("#" + currentRowId + "x");
            associateRow = $('#tblAssociatedEquipmentSummary').DataTable().row(trElementRowAssociate);
            data = that.getAssociateItem(null, null, true, row.data().EquipmentSeriesNumber);

            that.showDetailRow(row, tr, data, true);
            that.showDetailRow(associateRow, trElementRowAssociate, data, false);
        });
    },
    updateBadgesPerServiceType: function (array, isSummary) {
        var that = this, controls = that.getControls();
        var badgeLTE = 0, badgeDTH = 0;
        if (array != null && array != []) {
            for (var i = 0; i < array.length; i++) {
                if (array[i].EquipmentCodeType == '4' || array[i].EquipmentCodeType == '3') badgeLTE = badgeLTE + 1;
                else badgeDTH = badgeDTH + 1;
            }
        }

        //monto a cobrar
        var monto = 0.0;
        for (var i = 0; i < array.length; i++) {
            if (array[i].Action == 'Desasociar'){
                monto = monto + parseFloat(array[i].Penalidad);
            }
        }
       
        if (monto == 0) {
            controls.chkLoyalty.prop("disabled", true);
        } else {
            controls.chkLoyalty.prop("disabled", false);
            $("#chkLoyalty").prop("checked", false);
        }

        //var montoIGV = monto + (parseFloat(that.strIGV) * monto);
        controls.txChargeAmount.html(monto.toFixed(2));
        dblmontoFinal = monto.toFixed(2);
        dblmontoFinalHist = monto.toFixed(2);

        if (isSummary != true) {
            $('#spnCountLT').html(badgeLTE);
            $('#spnCountTV').html(badgeDTH);
        } else {
            $('#spnCountSummaryLT').html(badgeLTE);
            $('#spnCountSummaryTV').html(badgeDTH);
            controls.spnBadgeEquipmentAssociateSummary.html(badgeLTE + badgeDTH);
        }
    },
    updateBadgesPerServiceTypeInit: function (array, isSummary) {
        var that = this, controls = that.getControls();
        var badgeLTE = 0, badgeDTH = 0;
        if (array != null && array != []) {
            for (var i = 0; i < array.length; i++) {
                if (array[i].EquipmentCodeType == '4' || array[i].EquipmentCodeType == '3') badgeLTE = badgeLTE + 1;
                else badgeDTH = badgeDTH + 1;
            }
        }

        if (isSummary != true) {
            $('#spnCountLT').html(badgeLTE);
            $('#spnCountTV').html(badgeDTH);
        } else {
            $('#spnCountSummaryLT').html(badgeLTE);
            $('#spnCountSummaryTV').html(badgeDTH);
            controls.spnBadgeEquipmentAssociateSummary.html(badgeLTE + badgeDTH);
        }
    },
    getCacDacType: function (lstCacDacTypes, strCodeUser) {
        if (lstCacDacTypes.length > 0) {
            var that = this,
                controls = that.getControls();
            controls.sltCACDAC.append($('<option>', { value: '', html: 'Seleccionar' }));
            var itemSelect;
            $.each(lstCacDacTypes,
                function (index, value) {

                    if (strCodeUser == value.Description) {
                        controls.sltCACDAC.append($('<option>', { value: value.Code, html: value.Description }));
                        itemSelect = value.Code;

                    } else {
                        controls.sltCACDAC.append($('<option>', { value: value.Code, html: value.Description }));
                    }
                });
            if (itemSelect != null && itemSelect.toString != "undefined") {
                $("#sltCacDac option[value=" + itemSelect + "]").attr("selected", true);
            }

        }
    },
    chkEmail_change: function (sender, arg) {
        var that = this,
            controls = this.getControls();

        if (sender.prop('checked')) {
            controls.divEmailEnviar.show();
            controls.txtEmailSend.focus();
            that.strCheckEmail = 'SI';
        } else {
            controls.divEmailEnviar.hide();
            that.strCheckEmail = 'NO';
        }
    },
    btn1stClose_click: function () {
        parent.window.close();
    },
    btn4thClose_click: function () {
        parent.window.close();
    },
    btn5thClose_click: function () {
        parent.window.close();
    },
    getAssociateItem: function (associateCode, equipmentTypeCode, isSummaryTables, equipmentSeriesNumber) {
        var that = this;
        var data = that.objReplaceEquipmentLoad.lstEquimentAssociate;
        var item = {};
        if (!isSummaryTables) {
            for (var i = 0; i < data.length; i++) {
                if (data[i].EquipmentAssociate == associateCode && data[i].EquipmentCodeType != equipmentTypeCode) {
                    item = data[i];
                }
            }
        } else {
            $.each(data, function (index, value) {
                if (value.EquipmentSeriesNumber === equipmentSeriesNumber) {
                    item = that.getAssociateItem(value.EquipmentAssociate, value.EquipmentCodeType, false, null);
                }
            });
        }
        return item;
    },
    //getsltSOTList: function (lstSOTListypes) {
    //    var that = this,
    //        controls = that.getControls();
    //    if (lstSOTListypes.length > 0) {
    //        controls.sltSOTList.append($('<option>', { value: '', html: 'Seleccionar' }));
    //        var max = 0;
    //        $.each(lstSOTListypes,
    //            function (index, value) {
    //                controls.sltSOTList.append($('<option>', { value: value.Code, html: value.Code }));
    //                if (value.Code > max)
    //                    max = value.Code;
    //            });
    //        $("#sltSOTList option[value=" + max + "]").attr("selected", true);
    //        controls.sltSOTList.prop("disabled", true);
    //    }
    //},
    cboTypeWork_change: function (IdTipoTrabajo) {
        var that = this,
        controls = that.getControls();
        var typework = controls.cboTypeWork.val();
        $("#cboMotiveSOT option[value=" + typework + "]").attr("selected", true);

    },
    getBusinessRules: function (lstBusinessRules) {
        var that = this,
            controls = that.getControls();
        if (lstBusinessRules.length > 0) {
            controls.divRules.append(lstBusinessRules[0].Description);
        }
    },
    GetMotiveSOTByTypeJob: function (lstMotiveSOTByTypeJob) {
        if (lstMotiveSOTByTypeJob.length > 0) {
            var that = this,
            controls = that.getControls();
            controls.cboMotiveSOT.append($('<option>', { value: '', html: 'Seleccionar' }));

            $.each(lstMotiveSOTByTypeJob, function (index, value) {
                controls.cboMotiveSOT.append($('<option>', { value: value.Code, html: value.Description }));
            });

        }
    },
    disabledElement: function (bolResultControl, bolResultContancy) {
        var that = this,
            controls = that.getControls();
        controls.btnDissasociateEquiment.prop('disabled', bolResultControl);
        controls.btnSave.prop('disabled', bolResultControl);
        controls.btnConstancy.prop('disabled', bolResultContancy);

        $('.btn-circle').prop('disabled', bolResultControl);
        $('.prev-trans').prop('disabled', bolResultControl);
    },
    getTypeWork: function (lstTypeWork) {
        if (lstTypeWork.length > 0) {

            var that = this,
            controls = that.getControls();
            controls.cboTypeWork.append($('<option>', { value: '', html: 'Seleccionar' }));
            var itemSelect;
            $.each(lstTypeWork, function (index, value) {

                controls.cboTypeWork.append($('<option>', { value: value.Code, html: value.Description }));
                itemSelect = value.Code;

            });
            if (itemSelect != null && itemSelect.toString != "undefined") {
                $("#cboTypeWork option[value=" + itemSelect + "]").attr("selected", true);
            }
        }

    },
    btnSummaryAccion_click: function (e) {
        var that = this,
        controls = that.getControls();

        that.SummaryEmail();
        that.SummarychkEmail();
        that.SummaryPuntoAtencion();
       // that.SummaryPSOTAsociado();
        that.SummaryMotivoSot();
        that.SummaryTipoTrabajo();
        that.SummaryNota();
        that.SummarylblAmmountToPay();

        var objMsg = that.objReplaceEquipmentLoad.objReplaceEquipmentMessageModel;

        if (controls.cboTypeWork.val() == '' || controls.cboTypeWork.val() == null) {
            alert(objMsg.strMsjValidacionCampoSlt.replace("###", "Tipo de Trabajo"), 'Alerta');
            return false;
        }
        if (controls.cboMotiveSOT.val() == '' || controls.cboMotiveSOT.val() == null) {
            alert(objMsg.strMsjValidacionCampoSlt.replace("###", "Motivo de SOT"), 'Alerta');
            return false;
        }
        if (controls.chkEmail.prop('checked')) {
            if (controls.txtEmailSend.val() == '') {
                alert(objMsg.strMsjValidacionCampo.replace("###", "E-Mail"), 'Alerta');
                return false;
            }
            var regx = /^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$/;
            var blvalidate = regx.test(controls.txtEmailSend.val());
            if (blvalidate == false) {
                alert(objMsg.strMsjValidacionCampoFormato.replace("###", "E-Mail"), 'Alerta');
                return false;
            }
        }
        if (controls.sltCACDAC.val() == '' || controls.sltCACDAC.val() == null) {
            alert(objMsg.strMsjValidacionCampoSlt.replace("###", "Punto de Atencion"), 'Alerta');
            return false;
        }
        var $nextBtn = $('.next-step');
        navigateTabs($nextBtn);
    },
    SummarylblAmmountToPay: function () {
        var that = this,
            controls = that.getControls();
            Smmry.set('AmmountToPay', dblmontoFinal);
    },
    SummaryTelefonoReferencia: function () {
        var that = this,
            controls = that.getControls();

        if (controls.txtReferencia.val() != '') {
            Smmry.set('TelefonoReferencia', controls.txtReferencia.val());
        }
        else {
            Smmry.set('TelefonoReferencia', '');
        }
    },
    SummaryEmail: function () {
        var that = this,
            controls = that.getControls();

        if (controls.chkEmail[0].checked == true) {
            Smmry.set('email', controls.txtEmailSend.val());
        }
        else {
            Smmry.set('email', '');
        }
    },
    SummaryPuntoAtencion: function () {
        var that = this,
            controls = that.getControls();

        if (controls.sltCACDAC.val() != "") {
            Smmry.set('PuntoAtencion', $('#sltCacDac option:selected').html());
        }
        else {
            Smmry.set('PuntoAtencion', '');
        }
    },
    //SummaryPSOTAsociado: function () {
    //    var that = this,
    //        controls = that.getControls();

    //    if (controls.sltCACDAC.val() != "") {
    //        Smmry.set('SOTAsociado', $('#sltSOTList option:selected').html());
    //    }
    //    else {
    //        Smmry.set('SOTAsociado', '');
    //    }
    //},
    SummaryTipoTrabajo: function () {
        var that = this,
            controls = that.getControls();

        if (controls.sltCACDAC.val() != "") {
            Smmry.set('TipoTrabajo', $('#cboTypeWork option:selected').html());
        }
        else {
            Smmry.set('TipoTrabajo', '');
        }
    },
    SummaryMotivoSot: function () {
        var that = this,
            controls = that.getControls();

        if (controls.sltCACDAC.val() != "") {
            Smmry.set('MotivoSot', $('#cboMotiveSOT option:selected').html());
        }
        else {
            Smmry.set('MotivoSot', '');
        }
    },
    SummaryNota: function () {
        var that = this,
            controls = that.getControls();

        if ($.trim(controls.txtNotas.val()) != "") {
            var Notas = controls.txtNotas.val();
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

            Smmry.set('Nota', strFinal);
        }
        else {
            Smmry.set('Nota', '');
        }
    },
    SummarychkEmail: function () {
        var that = this,
            controls = that.getControls();

        if (controls.chkEmail[0].checked == true) {
            Smmry.set('chkemail_A', 'SI');
        }
        else {
            Smmry.set('chkemail_A', 'NO');
        }
    },
    btnSave_click: function () {
        $('#divErrorAlert').hide();
        var that = this,
            controls = that.getControls();
        var objRequestDataModel = {};

        objRequestDataModel.strSessionId = Session.IDSESSION;
        objRequestDataModel.strContractId = Session.DATACUSTOMER.ContractID;
        objRequestDataModel.strCustomerType = Session.DATACUSTOMER.CustomerType;
        objRequestDataModel.strCustomerID = Session.DATACUSTOMER.CustomerID;
        objRequestDataModel.strFullName = Session.DATACUSTOMER.FullName;
        objRequestDataModel.strCustomerContact = Session.DATACUSTOMER.CustomerContact;
        objRequestDataModel.strDocumentType = Session.DATACUSTOMER.DocumentType;
        objRequestDataModel.strDocumentNumber = Session.DATACUSTOMER.DocumentNumber;
        objRequestDataModel.blnCheckEmail = that.strCheckEmail == 'SI' ? true : false;
        objRequestDataModel.strCheckEmail = that.strCheckEmail;
        objRequestDataModel.strCacDac = controls.sltCACDAC.val();
        objRequestDataModel.strCacDacDescription = $('#sltCacDac option:selected').html();
        //objRequestDataModel.strSOTNumber = controls.sltSOTList.val();
        objRequestDataModel.strSendEmail = that.strCheckEmail == 'SI' ? controls.txtEmailSend.val() : '';
        objRequestDataModel.strAmmountToPay = dblmontoFinal;
        objRequestDataModel.strAddress = Session.DATACUSTOMER.Address;
        objRequestDataModel.strAddressNotes = Session.DATACUSTOMER.AddressInfo;
        objRequestDataModel.strDepartament = Session.DATACUSTOMER.Departament;
        objRequestDataModel.strDistrict = Session.DATACUSTOMER.District;
        objRequestDataModel.strCountry = Session.DATACUSTOMER.LegalCountry;
        objRequestDataModel.strProvince = Session.DATACUSTOMER.Province;
        objRequestDataModel.strCodPlan = Session.DATASERVICE.Plan;

        objRequestDataModel.strBillingCycle = Session.DATACUSTOMER.BillingCycle;

        objRequestDataModel.strCodeUser = Session.USERACCESS.login;
        objRequestDataModel.strfullNameUser = Session.USERACCESS.fullName;
        objRequestDataModel.strTypeWorkCode = controls.cboTypeWork.val();
        objRequestDataModel.strTypeWork = $('#cboTypeWork option:selected').html();
        objRequestDataModel.strMotiveSOTCode = controls.cboMotiveSOT.val();
        objRequestDataModel.strMotiveSOT = $('#cboMotiveSOT option:selected').html();
        objRequestDataModel.strNote = controls.txtNotas.val();
        objRequestDataModel.objTypification = that.objReplaceEquipmentLoad.objTypification;

        objRequestDataModel.numDoc = Session.DATACUSTOMER.DocumentNumber;
        objRequestDataModel.strAddress = Session.DATACUSTOMER.Address;
        that.AddComplementEquipments(that.dataSummaryEquipment);
        objRequestDataModel.lstEquimentsAssociate = that.lstEquipmentSummaryWithComplement;
        objRequestDataModel.strReferencia = controls.txtReferencia.val();
        var objValidateModelResult = {};
        that.loadingPage('Procesando');
        $.app.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            url: '/Transactions/Fixed/ReplaceEquipment/Save',
            data: JSON.stringify(objRequestDataModel),
            success: function (response) {
                objValidateModelResult = response.data;
                if (response.data != null) {
                    that.strFilePathConstancy = objValidateModelResult.strFilePathConstancy;

                    if (response.data.bErrorTransac == false) {
                        alert(objValidateModelResult.SaveSuccessfully + " Se generó la SOT Nro.: " + objValidateModelResult.strSOTNumber, "Informativo");
                        that.disabledElement(true, false);
                        $('#divErrorAlert').fadeIn();
                        $('#lblErrorMessage').html(objValidateModelResult.strMessageErrorTransac + ' Se generó la SOT: <strong>' + objValidateModelResult.strSOTNumber + '</strong>');
                    } else {
                        alert(objValidateModelResult.strResultMessage, "Alerta");
                        that.disabledElement(false, true);
                    }
                } else {
                    alert('Error: ' + objValidateModelResult.strMessageErrorTransac, "Alerta");
                    that.disabledElement(false, true);
                }
            },
            error: function (errormessage) {
                alert('Error: ' + objValidateModelResult.strMessageErrorTransac, "Alerta");
            }
        }
        );
    },
    AddComplementEquipments: function (data) {
        var that = this;

        that.lstEquipmentSummaryWithComplement = [];
        $.each(data, function (index, value) {
            that.lstEquipmentSummaryWithComplement.push(value);
        });

        $.each(that.lstEquipmentSummaryWithComplement, function (index, value) {
            if (value.Action === "C") {
                that.lstEquipmentSummaryWithComplement.splice(index, 1);
            }
        });
        $.each(that.lstEquipmentSummaryWithComplement, function (index, value) {
            //if (value.EquipmentCodeType!="3" && value.EquipmentCodeType!="4") {
            if (value.EquipmentAssociate != null) {
                var associateEquipmentExistInSummary = that.getExistEquipmentAssociateInSummary(value.EquipmentSeriesNumber, value.EquipmentAssociate);
                if (!associateEquipmentExistInSummary) {
                    var itemComplement = that.getAssociateItem(null, null, true, value.EquipmentSeriesNumber);
                    itemComplement.Action = "C";
                    itemComplement.OperationCode = value.OperationCode;
                    that.lstEquipmentSummaryWithComplement.push(itemComplement);
                }
            }
            //}


        });
    },
    getExistEquipmentAssociateInSummary: function (strEquipmentSeriesNumber, strEquipmentAssociateCode) {
        var that = this, result = false;
        var dataSummary = that.dataSummaryEquipment;
        $.each(dataSummary, function (index, value) {
            if (value.EquipmentSeriesNumber != strEquipmentSeriesNumber && value.EquipmentAssociate === strEquipmentAssociateCode) {
                result = true;
            }
        });
        return result;
    },
    btnConstancy_click: function () {
        var that = this, controls = that.getControls();
        that.loadingPage('Cargando');

        if (that.strFilePathConstancy != '' && that.strFilePathConstancy != null) {
            var newRoute = that.strFilePathConstancy.substring(that.strFilePathConstancy.indexOf('SIACUNICO'));
            newRoute = newRoute.replace(new RegExp('/', 'g'), '\\');
            newRoute = that.strServidorLeerPDF + newRoute;

            ReadRecordSharedFile(Session.USERACCESS.userId, newRoute);
        } else{
            alert(that.objReplaceEquipmentLoad.objReplaceEquipmentMessageModel.strConstancyError, "Informativo");
        }
        $.unblockUI();
    },
    btnDissasociateEquiment_Click_Pop: function () {
        var that = this, controls = that.getControls();
        var filaSeleccionada = that.rowEquipmentSelected;
        $('#idtab1').focus();
        if (filaSeleccionada == null || filaSeleccionada.EquipmentSeriesNumber == undefined) {
            alert(that.objReplaceEquipmentLoad.objReplaceEquipmentMessageModel.strEquipmentNotSelected, "Alerta");
            return false;
        }
        for (var j = 0; j < that.dataSummaryEquipment.length; j++) {
            if (that.dataSummaryEquipment[j].EquipmentSeriesNumber == filaSeleccionada.EquipmentSeriesNumber) {
                alert(that.objReplaceEquipmentLoad.objReplaceEquipmentMessageModel.strEquipmentBlockToSelected);
                return false;
            }
        }
        if (filaSeleccionada.EquipmentSeriesNumber != undefined) {
            if (that.GetVerifyLimitOfEquipments(filaSeleccionada.EquipmentCodeType) == false) {
                alert(that.objReplaceEquipmentLoad.objReplaceEquipmentMessageModel.strLimitEquipments.replace("###", filaSeleccionada.EquipmentType), "Alerta");
                return false;
            }
            var urlBase = window.location.href;
            urlBase = urlBase.substr(0, urlBase.lastIndexOf('/'));
            urlBase = location.protocol +
                '//' +
                location.host +
                '/Transactions/Fixed/ReplaceEquipment/FixedReplaceEquipmentAssociate';
            var objModel = {};
            objModel.currentEquipmentTypeSelected = filaSeleccionada.EquipmentCodeType;
            var limit = that.GetLimitPerType(filaSeleccionada.EquipmentCodeType);
            objModel.limitEquipmentsPerCustomer = limit[1];
            objModel.associateEquipmentsPerCustomer = limit[0];
            objModel.strEquipmentAssociateUsed = that.AssociateEquipmentIsUsed(filaSeleccionada.EquipmentAssociate, filaSeleccionada.EquipmentCodeType);
            var heightModal = 330;
            if (objModel.currentEquipmentTypeSelected == "1" ||
                objModel.currentEquipmentTypeSelected == "3") {
                heightModal = 260;
            } else if (objModel.currentEquipmentTypeSelected == "4" &&
                that.objReplaceEquipmentLoad.strActiveFullReplaceEquipmentForFixed == "0") {
                heightModal = 290;
            }
            var dialog = $.window.open({
                modal: true,
                type: 'post',
                title: "Asociar Equipo - " + filaSeleccionada.EquipmentType,
                url: urlBase,
                data: objModel,
                width: 500,
                height: heightModal,
                buttons: {
                    Asociar: {
                        click: function () {
                            var modalAssociate = this, objResultValidate = {}, associateOfEquipment = {};
                            if (that.rowEquipmentSelected == {}) {
                                alert(that.objReplaceEquipmentLoad.objReplaceEquipmentMessageModel.strEquipmentNotSelected, "Alerta");
                                return false;
                            }
                            var eType = that.rowEquipmentSelected.EquipmentCodeType;
                            that.lstEquipmentsToAssociate.push(that.rowEquipmentSelected);
                            var chkFullReplaceEquipment = document.getElementById($('#chkFullReplaceEquipment').attr("id"));
                            var strSecondCodinssrv = "";
                            if (eType == "4" || eType == "2") {
                                if (eType == "4") {
                                    var equipmentAssociateIsUsed = that.AssociateEquipmentIsUsed(that.rowEquipmentSelected.EquipmentAssociate, that.rowEquipmentSelected.EquipmentCodeType);
                                    if (equipmentAssociateIsUsed === "0") {
                                        associateOfEquipment = that.getAssociateItem(that.rowEquipmentSelected.EquipmentAssociate,
                                        that.rowEquipmentSelected.EquipmentType, false, null);
                                        that.lstEquipmentsToAssociate.push(associateOfEquipment);
                                    }
                                } else if (chkFullReplaceEquipment.checked == true) {
                                    associateOfEquipment = that.getAssociateItem(that.rowEquipmentSelected.EquipmentAssociate,
                                    that.rowEquipmentSelected.EquipmentType, false, null);
                                    that.lstEquipmentsToAssociate.push(associateOfEquipment);
                                }
                                strSecondCodinssrv = associateOfEquipment.EquipmentCodINSSRV;
                            }
                            that.currentModalWindow = modalAssociate;
                            that.ValidateEquipment(that.rowEquipmentSelected.EquipmentCodeType, "", false, null, that.rowEquipmentSelected.EquipmentCodINSSRV, strSecondCodinssrv);
                        }
                    },
                    Cancelar: {
                        click: function (sender, args) {
                            this.close();
                            that.lstEquipmentsToAssociate = [];
                        }
                    }
                }
            });
        }
    },
    AssociateEquipmentIsUsed: function (equipmentAssociateCode, equipmentType) {
        var that = this;
        var result = "0";
        var associateOfEquipment = that.getAssociateItem(equipmentAssociateCode, equipmentType, false, null);
        $.each(that.dataSummaryEquipment, function (index, value) {
            if (value.EquipmentSeriesNumber === associateOfEquipment.EquipmentSeriesNumber) {
                result = "1";
            }
        });
        return result;
    },
    GetLimitPerType: function (equipmentCodeType) {
        var that = this;
        var limits = [];
        switch (equipmentCodeType) {
            case "1":
                limits = that.limitEquipmentsDTHSmartCardPerOperation;
                break;
            case "2":
                limits = that.limitEquipmentsDTHSmartCardPerOperation;
                break;
            case "3":
                limits = that.limitEquipmentsLteSimCardPerOperation;
                break;
            case "4":
                limits = that.limitEquipmentsLteSimCardPerOperation;
                break;
        }
        return limits;
    },
    GetVerifyLimitOfEquipments: function (equipmentCodeType) {
        var that = this, result = false;
        switch (equipmentCodeType) {
            case "1":
                if (that.limitEquipmentsDTHSmartCardPerOperation[0] < that.limitEquipmentsDTHSmartCardPerOperation[1]) result = true;
                break;
            case "2":
                if (that.limitEquipmentsDTHDecoPerOperation[0] < that.limitEquipmentsDTHDecoPerOperation[1]) result = true;
                break;
            case "3":
                if (that.limitEquipmentsLteSimCardPerOperation[0] < that.limitEquipmentsLteSimCardPerOperation[1]) result = true;
                break;
            case "4":
                if (that.limitEquipmentsLteCPEPerOperation[0] < that.limitEquipmentsLteCPEPerOperation[1]) result = true;
                break;
        }
        return result;
    },
    ValidateEquipment: function (equipmentCodeType, strEquipmentSeriesForEdit, isSecondValidate, indexForEditOperation, strFirstInssrv, strSecondInssrv) {
        var that = this, controls = that.getControls(), objRequestDataModel = {};
        var equipmentSeries = "";
        var currentControlToFocus = {};
        var isRequiredSecondValidate = false, secondEquipmentSeriesNumber = "", secondEquipmentCodeType = "";
        var result = {}, seriesExists = false;
        var chkFullReplaceEquipment = document.getElementById($('#chkFullReplaceEquipment').attr("id"));
        if (strEquipmentSeriesForEdit == "") {
            if (equipmentCodeType == "1") { //SMARTCARD
                isRequiredSecondValidate = false;
                currentControlToFocus = $('#txtSMARTCARD');
                equipmentSeries = $('#txtSMARTCARD').val();
            } else if (equipmentCodeType == "2") { //DECO
                currentControlToFocus = $('#txtDecoSeries');
                equipmentSeries = $('#txtDecoSeries').val();
                if (chkFullReplaceEquipment.checked == true) {
                    isRequiredSecondValidate = true;
                    secondEquipmentSeriesNumber = $('#txtSMARTCARD').val();
                    secondEquipmentCodeType = "1";
                }
            } else if (equipmentCodeType == "3") { //CHIP
                currentControlToFocus = $('#txtSIMCARD');
                isRequiredSecondValidate = false;
                equipmentSeries = $('#txtSIMCARD').val();
            } else if (equipmentCodeType == "4") { //CPE
                currentControlToFocus = $('#txtCPESeries');
                equipmentSeries = $('#txtCPESeries').val();
                var simcardused = that.AssociateEquipmentIsUsed(that.rowEquipmentSelected.EquipmentAssociate,
                    equipmentCodeType);
                if (that.objReplaceEquipmentLoad.strActiveFullReplaceEquipmentForFixed === "0" && simcardused === "0") {
                    isRequiredSecondValidate = true;
                    secondEquipmentSeriesNumber = $('#txtSIMCARD').val();
                    secondEquipmentCodeType = "3";
                } else {
                    if (chkFullReplaceEquipment.checked == true) {
                        isRequiredSecondValidate = true;
                        secondEquipmentSeriesNumber = $('#txtSIMCARD').val();
                        secondEquipmentCodeType = "3";
                    }
                }
            }
        } else {
            currentControlToFocus = $('#txtEditEquipment');
            equipmentSeries = strEquipmentSeriesForEdit;
        }
        $.each(that.dataSummaryEquipment, function (index, value) {
            
            if (value.EquipmentSeriesNumber === equipmentSeries) {
                seriesExists = true;
            }
        });
        if (seriesExists == false) {
        var readyForValidate = true;
        if (isRequiredSecondValidate) {
            if (equipmentSeries == "" || secondEquipmentSeriesNumber == "") readyForValidate = false;
        } else if (equipmentSeries == "") {
            readyForValidate = false;
        }
        if (readyForValidate) {
            var oldSeriesNumberSimCard = "";
            if (equipmentCodeType == "3") {
                $.each(that.objReplaceEquipmentLoad.lstEquimentAssociate, function (index, value) {
                    if (value.EquipmentCodeType == "3") {
                        oldSeriesNumberSimCard = value.EquipmentSeriesNumber;
                    }
                });
            }
            objRequestDataModel.strSessionId = Session.IDSESSION;
            objRequestDataModel.strEquipmentTypeCode = equipmentCodeType;
            objRequestDataModel.strOldEquipmentSeriesNumber = oldSeriesNumberSimCard;
            objRequestDataModel.strEquipmentSeries = equipmentSeries;

            if (indexForEditOperation != null) {
                objRequestDataModel.strDecoType = equipmentCodeType === "2" ? strSecondInssrv : '';
            } else objRequestDataModel.strDecoType = equipmentCodeType === "2" ? that.rowEquipmentSelected.DecoType : '';

            objRequestDataModel.strContractId = Session.DATACUSTOMER.ContractID;
            objRequestDataModel.strCodInssrv = strFirstInssrv;
            if (isSecondValidate == false) {
                $('#' + that.currentModalWindow.getId()).hide();
                $('.modal-backdrop').toggle();
                that.loadingPage('Validando equipo');
            } else {
                objRequestDataModel.strCodInssrv = strSecondInssrv;
            }
            $.app.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '/Transactions/Fixed/ReplaceEquipment/GetValidateEquipment',
                data: JSON.stringify(objRequestDataModel),
                success: function (response) {
                    if (response.data != null) {
                        that.objValidateModelResult = response.data;
                        if (that.objValidateModelResult.objEquipmentToAssociate.EquipmentSeriesNumber != "" &&
                            that.objValidateModelResult.objEquipmentToAssociate.EquipmentSeriesNumber != null) {
                            that.lstEquipmentsToAssociate.push(that.objValidateModelResult.objEquipmentToAssociate);
                        }
                        var objResultValidate = that.objValidateModelResult;
                        if (strEquipmentSeriesForEdit == "") {
                            if (objResultValidate.strResultCode != null) {
                                if (objResultValidate.strResultCode == "0") {
                                    if (isRequiredSecondValidate === false || isSecondValidate) {
                                        that.fillCustomerEquipmentToProcces(that.lstEquipmentsToAssociate);
                                        that.blockEquipmentAdded();
                                        that.OperationCodeCount += 1;
                                        that.lstEquipmentsToAssociate = [];
                                        that.rowEquipmentSelected = {};
                                        that.updateBadgesPerServiceType(that.dataSummaryEquipment, true);
                                        that.UpdateEquipmentsQuantityForLimits();
                                        that.currentModalWindow.close();
                                    } else {
                                        that.ValidateEquipment(secondEquipmentCodeType, "", true, null, strFirstInssrv, strSecondInssrv);
                                    }

                                } else {
                                    that.lstEquipmentsToAssociate = [];
                                    $('.modal-backdrop').toggle();
                                    alert(that.getErrorMessageByEquipmentCodeType(objResultValidate.strResultCode), "Alerta - Validación de " + that.getEquipmentTypeNameByCode(equipmentCodeType));
                                    $('#' + that.currentModalWindow.getId()).show();
                                    $('#' + that.currentModalWindow.getId()).focus();
                                    return false;
                                }
                            }
                                } else {
                                    if (objResultValidate.strResultCode != null) {
                                        var table = controls.tblProductsSummary.DataTable();
                                        if (objResultValidate.strResultCode == "0") {
                                            var objNewEquipment = objResultValidate.objEquipmentToAssociate;
                                            table.cell(indexForEditOperation, 1)
                                                .data(objNewEquipment.EquipmentSeriesNumber).draw(true);
                                            table.cell(indexForEditOperation, 2)
                                                .data(objNewEquipment.EquipmentDescription).draw(true);
                                            table.cell(indexForEditOperation, 4).data(objNewEquipment.DecoType)
                                                .draw(true);
                                            table.cell(indexForEditOperation, 5)
                                                .data(objNewEquipment.EquipmentMACAddress).draw(true);
                                            table.cell(indexForEditOperation, 6).data(objNewEquipment.NumberPhone)
                                                .draw(true);
                                            table.cell(indexForEditOperation, 7).data(objNewEquipment.EquipmentOC)
                                                .draw(true);
                                            that.lstEquipmentsToAssociate = [];
                                            that.rowEquipmentSelected = {};
                                            that.strCurrentEquipmentToEdit = objNewEquipment.EquipmentSeriesNumber;
                                            that.editSeriesNumberField(indexForEditOperation, '', false);
                                            that.disabled_buttonOperations(false);
                                            that.updateBadgesPerServiceType(that.dataSummaryEquipment, true);
                                            alert(that.objReplaceEquipmentLoad.objReplaceEquipmentMessageModel.strSuccessModification, 'Informativo');
                                        } else {
                                            that.lstEquipmentsToAssociate = [];
                                            that.rowEquipmentSelected = {};
                                            $('.modal-backdrop').toggle();
                                            $('#' + that.currentModalWindow.getId()).show();
                                            alert(that.getErrorMessageByEquipmentCodeType(objResultValidate
                                                    .strResultCode),
                                                "Alerta - Validación de " +
                                                that.getEquipmentTypeNameByCode(equipmentCodeType));
                                            return false;
                                        }
                                    }
                                }
                            } else {
                                alert(that.objReplaceEquipmentLoad.objReplaceEquipmentMessageModel.strValidateEquipmentTechnicalErrors, "Error al validar " +
                                that.getEquipmentTypeNameByCode(equipmentCodeType));
                                return false;
                            }
                    if (isSecondValidate ||
                        strEquipmentSeriesForEdit != "" ||
                        isRequiredSecondValidate == false) {
                        $.unblockUI();
                    }

                    },
                    error: function (errormessage) {
                        alert('Error: ' + errormessage);
                    }
                    });
                } else {
                    that.objValidateModelResult = {};
                    that.lstEquipmentsToAssociate = [];
                    alert(that.objReplaceEquipmentLoad.objReplaceEquipmentMessageModel.strValidateIncompleteFields,
                        "Alerta - Validación de " +
                        that.getEquipmentTypeNameByCode(equipmentCodeType));
                    return false;
                }
            } else {
                that.objValidateModelResult = {};
                that.lstEquipmentsToAssociate = [];
                $('.modal-backdrop').show();
                alert(that.objReplaceEquipmentLoad.objReplaceEquipmentMessageModel.strEqualEquipmentExistInOperation,
                    "Alerta - Validación de " +
                    that.getEquipmentTypeNameByCode(equipmentCodeType));
                $('#' + that.currentModalWindow.getId()).show();
                $('#' + that.currentModalWindow.getId()).focus();
                return false;
            }
    },
        UpdateEquipmentsQuantityForLimits: function () {
            var that = this;
            var dataSummary = that.dataSummaryEquipment, dataToAssociate = [];
            that.limitEquipmentsDTHSmartCardPerOperation[0] = 0;
            that.limitEquipmentsDTHDecoPerOperation[0] = 0;
            that.limitEquipmentsLteSimCardPerOperation[0] = 0;
            that.limitEquipmentsLteCPEPerOperation[0] = 0;
            $.each(dataSummary, function (index, value) {
                if (value.Action == "Asociar") {
                    dataToAssociate.push(value);
                }
            });
            $.each(dataToAssociate, function (index, value) {
                switch (value.EquipmentCodeType) {
                    case "1":
                        that.limitEquipmentsDTHSmartCardPerOperation[0] = that.limitEquipmentsDTHSmartCardPerOperation[0] + 1;
                        break;
                    case "2":
                        that.limitEquipmentsDTHDecoPerOperation[0] = that.limitEquipmentsDTHDecoPerOperation[0] + 1;
                        break;
                    case "3":
                        that.limitEquipmentsLteSimCardPerOperation[0] = that.limitEquipmentsLteSimCardPerOperation[0] + 1;
                        break;
                    case "4":
                        that.limitEquipmentsLteCPEPerOperation[0] = that.limitEquipmentsLteCPEPerOperation[0] + 1;
                        break;
                }
            });
        },
    getEquipmentTypeNameByCode: function (strEquipmentTypeCode) {
        var typeName = "";
        if (strEquipmentTypeCode == "1") typeName = "SMARTCARD";
        if (strEquipmentTypeCode == "2") typeName = "DECODIFICADOR";
        if (strEquipmentTypeCode == "3") typeName = "SIMCARD";
        if (strEquipmentTypeCode == "4") typeName = "CPE";
        return typeName;
    },
    getErrorMessageByEquipmentCodeType: function (resultCode) {
        var that = this;
        var errorMessage = "";
        var objMessageModel = that.objReplaceEquipmentLoad.objReplaceEquipmentMessageModel;
        switch (resultCode) {
            case "1":
                errorMessage = objMessageModel.strValidateEquipmentActive;
                break;
            case "2":
                errorMessage = objMessageModel.strValidateEquipmentNotFound;
                break;
            case "3":
                errorMessage = objMessageModel.strValidateEquipmentMoreThanOneEquipment;
                break;
            case "-1":
                errorMessage = objMessageModel.strValidateEquipmentContractNotExist;
                break;
            case "-2":
                errorMessage = objMessageModel.strValidateEquipmentContractInvalid;
                break;
            case "-3":
                errorMessage = objMessageModel.strValidateEquipmentICCIDNotExist;
                break;
            case "-4":
                errorMessage = objMessageModel.strValidateEquipmentIMSI;
                break;
            case "-5":
                errorMessage = objMessageModel.strValidateEquipmentICCID;
                break;
            case "-99":
                errorMessage = objMessageModel.strValidateEquipmentTechnicalErrors;
                break;
            case "00":
                errorMessage = objMessageModel.strValidateEquipmentNotReceivedNewData;
                break;
            default:
                errorMessage = objMessageModel.strValidateEquipmentResponseCodeInvalid;
                break;
        }
        return errorMessage;

    },
    removeEquipmentFromSummaryTable: function (item) {
        var that = this, controls = that.getControls();
        var table = controls.tblProductsSummary.DataTable();
        var operationNumber = item.OperationCode;
        var index = 0;
        that.loadingPage('Eliminando');
        table.rows().every(function (rowIdx, tableLoop, rowLoop) {
            if (table.row(this.row(index).node()).data() != null) {
                var operationCode = table.row(this.row(index).node()).data().OperationCode;
                var arrayequipment = table.row(this.row(index).node()).data();
                if (operationCode == operationNumber) {
                    var searchequipment = that.dataSummaryEquipment.indexOf(arrayequipment);
                    table.rows(this.row(index).node()).remove().draw(false);
                    that.dataSummaryEquipment.splice(searchequipment, 1);
                    if (table.data().count() > 1) that.removeEquipmentFromSummaryTable(item);
                }
                index++;
            }
        });
        that.UpdateEquipmentsQuantityForLimits();
        that.blockEquipmentAdded();
        that.updateBadgesPerServiceType(that.dataSummaryEquipment, true);
        $.unblockUI();
    },
    blockEquipmentAdded: function () {
        var that = this, controls = that.getControls();
        var table = controls.tblProductTable.DataTable();
        $('.btnAddEquipmentToDisassociate').removeClass('btnAddEquipmentToDisassociate-active fa fa-check btnAddEquipmentToDisassociate-selected');
        table.rows().every(function (rowIdx, tableLoop, rowLoop) {
            if (table.row(this.row(rowIdx).node()).data() != null) {
                var data = table.row(this.row(rowIdx).node()).data();
                for (var i = 0; i < that.dataSummaryEquipment.length; i++) {
                    if (that.dataSummaryEquipment[i].EquipmentSeriesNumber == data.EquipmentSeriesNumber) {
                        $('#btnAddEquipmentToDisassociate' + rowIdx).toggleClass('btnAddEquipmentToDisassociate-selected fa fa-check');
                    }
                }
            }
        });
    },
    loadingPage: function (mensaje) {
        $.blockUI({
            message: '<div align="center"><img src="' + this.strUrlLogo + '" width="25" height="25" /> ' + mensaje + ' ... </div>',
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
    fillArraysForSummary: function () {
        var that = this,
            data = that.dataSummaryEquipment;
        that.ActionDisassociate = [];
        that.ActionAssociate = [];
        for (var j = 0; j < data.length; j++) {
            if (data[j].Action == 'Desasociar') {
                that.ActionDisassociate.push(data[j]);
            }
            if (data[j].Action == 'Asociar') {
                that.ActionAssociate.push(data[j]);
            }
        }

        that.loadResumeEquipmentsDesDataTable(that.ActionDisassociate);
        that.loadResumeEquipmentsAsoDataTable(that.ActionAssociate);
    },
    deleteForValidate: function (serie) {
        var that = this;
        var data = that.ActionDisassociate;
        for (var i = 0; i < data.length; i++) {
            if (data[i].EquipmentSeriesNumber == serie) {
                that.ActionDisassociate.splice(i, 1);
            }
        }
    },
    fillCustomerEquipmentToProcces: function (items) {
        var that = this, controls = that.getControls();
        var table = controls.tblProductsSummary.DataTable();
        for (var i = 0; i < items.length; i++) {
            items[i].OperationCode = that.OperationCodeCount;
            that.dataSummaryEquipment.push(items[i]);
            table.row.add(items[i]).draw();
        }

        controls.btnNextFirstStep.focus();
        var data = that.dataSummaryEquipment;

        var ActionAssociate = [];
        for (var j = 0; j < data.length; j++) {
            if (data[j].Action == 'Desasociar') {
                that.ActionDisassociate.push(data[j]);
            }
            if (data[j].Action == 'Asociar') {
                ActionAssociate.push(data[j]);
            }
        }

        that.loadResumeEquipmentsDesDataTable(that.ActionDisassociate);
        that.loadResumeEquipmentsAsoDataTable(ActionAssociate);

    },

    f_ValidaterPermLoyalty: function () {
        var that = this;
        var controls = that.getControls();
        if (controls.chkLoyalty.prop("checked")) {
            if (that.dataSummaryEquipment.length > 0 ) {
                var param = {
                    "strIdSession": "1234567",
                    'pag': '1',
                    'opcion': 'BUS',
                    'co': '1000000000000',
                    'telefono':''//SessionTransf.PhonfNro
                };
                controls.chkLoyalty.prop("checked", false);
                //summary
                Smmry.set('chkFidelizar', "NO");
                ValidateAccess(that, controls, 'BUS', 'gConstOpcTIEHabFidelizar', '1', param, 'Fixed');
                dblmontoFinal = 0;
                dblmontoFinal = dblmontoFinal.toFixed(2);
           }
        }
        else {
            $("#chkLoyalty").prop("checked", false);
            controls.txChargeAmount.html(dblmontoFinalHist);
            dblmontoFinal = dblmontoFinalHist;
        }

    },

        strUrlLogo: window.location.protocol + '//' + window.location.host + '/Images/loading_3.gif',
        lstServiceTypes: {},
        strCheckEmail: "",
        currentEquipmentTypeSelected: "",
        objReplaceEquipmentLoad: {},
        OperationCodeCount: 1,
        dataSummaryEquipment: [],
        ActionDisassociate: [],
        lstEquipmentsToAssociate: [],
        lstEquipmentSummaryWithComplement: [],
        strCurrentEquipmentToEdit: "",
        strCurrentRowIndexToEdit: 0,
        rowEquipmentSelected: {},
        strFilePathConstancy: "",
        ReplaceEquipmentMessageModel: {},
        objValidateModelResult: {},
        currentModalWindow: {},
        limitEquipmentsLteCPEPerOperation: [0, 1],
        limitEquipmentsLteSimCardPerOperation: [0, 1],
        limitEquipmentsDTHDecoPerOperation: [0, 4],
        limitEquipmentsDTHSmartCardPerOperation: [0, 4],
        strIGV: "",
        dblmontoFinal: 0.0,
        dblmontoFinalHist: 0.0,
        strServidorLeerPDF: ""
    }
    $.fn.FixedReplaceEquipment = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];
        this.each(function () {
            var $this = $(this),
                data = $this.data('FixedReplaceEquipment'),
                options = $.extend({}, $.fn.FixedReplaceEquipment.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('FixedReplaceEquipment', data);
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
    $.fn.FixedReplaceEquipment.defaults = {
    }
    $('#divBody').FixedReplaceEquipment();

    $('button[data-toggle="tab"]').on('shown.bs.tab', function (e) {
        $('#tblDisassociatedEquipmentSummary').DataTable().columns.adjust().draw();
        $('#tblAssociatedEquipmentSummary').DataTable().columns.adjust().draw();
    });
    })(jQuery);