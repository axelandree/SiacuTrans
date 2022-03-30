(function ($, undefined) {
    var SessionTransac = function () { };
    var Smmry = new Summary('transfer');

    var Form = function ($element, options) {

        $.extend(this, $.fn.LtePlanMigration.defaults, $element.data(), typeof options === 'object' && options);

        this.setControles({
            form: $element
          , lblTitle: $('#lblTitle', $element)
          , btnAddPlan: $('#btnAddPlan', $element)
          , btnAddServ: $('#btnAddServ', $element)
          , btn1stClose: $('#btn1stClose', $element)
          , btn2ndClose: $('#btn2ndClose', $element)
          , btn3rdClose: $('#btn3rdClose', $element)
          , btn4thClose: $('#btn4thClose', $element)
          , btn5thClose: $('#btn5thClose', $element)
          , btnNextFirstStep: $('#btnNextFirstStep', $element)
          , btnSave: $('#btnSave', $element)
          , btnConstancy: $('#btnConstancy', $element)
          , btnAddEquip: $('#btnAddEquip', $element)
          , lblNode: $("#lblNode", $element)
          , lblNodeNew: $("#lblNodeNew", $element)
          , lblContract: $("#lblContract", $element)
          , lblCustomerId: $("#lblCustomerId", $element)
          , lblCustomerType: $("#lblCustomerType", $element)
          , lblContact: $("#lblContact", $element)
          , lblCustomer: $("#lblCustomer", $element)
          , lblDocNum: $("#lblDocNum", $element)
          , lblCurrentPlan: $("#lblCurrentPlan", $element)
          , lblCurrentDate: $("#lblCurrentDate", $element)
          , lblBillingCycle: $("#lblBillingCycle", $element)
          , lblCreditLimit: $("#lblCreditLimit", $element)
          , lblLegalRep: $("#lblLegalRep", $element)
          , lblAddress: $('#lblAddress', $element)
          , lblReference: $('#lblReference', $element)
          , lblCountry: $('#lblCountry', $element)
          , lblDepartment: $('#lblDepartment', $element)
          , lblProvince: $('#lblProvince', $element)
          , lblDistrict: $('#lblDistrict', $element)
          , lblUbigeoCode: $('#lblUbigeoCode', $element)
          , lblBaseAmount: $("#lblBaseAmount", $element)
          , divRules: $('#divRules', $element)
          , sltCacDac: $('#sltCacDac', $element)
          , txtNotes: $('#txtNotes', $element)
          , txtLetterNumber: $('#txtLetterNumber', $element)
          , sltOperator: $('#sltOperator', $element)
          , txtRefound: $('#txtRefound', $element)
          , txtLoyaltyAmount: $('#txtLoyaltyAmount', $element)
          , txtTotalPenality: $('#txtTotalPenality', $element)
          , chkPreSuscribed: $("#chkPreSuscribed", $element)
          , chkPublish: $('#chkPublish', $element)
          , chkLoyalty: $('#chkLoyalty', $element)
          , chkOCC: $('#chkOCC', $element)
          , chkEmail: $('#chkEmail', $element)
          , txtEmail: $('#txtEmail', $element)
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
          , spnCableEquipmentQty: $('#spnCableEquipmentQty', $element)
          , spnInternetEquipmentQty: $('#spnInternetEquipmentQty', $element)
          , spnPhoneEquipmentQty: $('#spnPhoneEquipmentQty', $element)
          , spnDecosQuantity: $('#spnDecosQuantity', $element)
          , lstCurrentPlanSummary: $('#lstCurrentPlanSummary', $element)
          , lstNewPlanSummary: $('#lstNewPlanSummary', $element)
          , spnNewPlan: $('#spnNewPlan', $element)
          , spnNewSolution: $('#spnNewSolution', $element)
          , spnCurrentBaseAmount: $('#spnCurrentBaseAmount', $element)
          , spnAdditionalCurrentAmount: $('#spnAdditionalCurrentAmount', $element)
          , spnCurrentTotalFixedChargeSIGV: $('#spnCurrentTotalFixedChargeSIGV', $element)
          , spnCurrentTotalFixedChargeCIGV: $('#spnCurrentTotalFixedChargeCIGV', $element)
          , spnQuantityCurrentServices: $('#spnQuantityCurrentServices', $element)
          , spnQuantityCurrentEquipment: $('#spnQuantityCurrentEquipment', $element)
          , spnNewBaseAmount: $('#spnNewBaseAmount', $element) 
          , spnAdditionalNewAmount: $('#spnAdditionalNewAmount', $element) 
          , spnQuantityNewServices: $('#spnQuantityNewServices', $element) 
          , spnQuantityNewEquipment: $('#spnQuantityNewEquipment', $element)
          , spnNewTotalFixedChargeSIGV: $('#spnNewTotalFixedChargeSIGV', $element) 
          , spnNewTotalFixedChargeCIGV: $('#spnNewTotalFixedChargeCIGV', $element) 
          , tblChooseServicesByPlanCable: $("#tblChooseCoreServicesByPlanCable", $element)
          , tblChooseServicesByPlanInternet: $("#tblChooseCoreServicesByPlanInternet", $element)
          , tblChooseServicesByPlanPhone: $("#tblChooseCoreServicesByPlanPhone", $element)
          , divEmailSend: $("#divEmailSend", $element)
          , spnValidateVelPlan: $("#spnValidateVelPlan", $element)

          , sltAction: $("#sltAction", $element)
          , sltOption: $("#sltOption", $element)
          , chkFreeOfCharge: $("#chkFreeOfCharge", $element)
          , chkaditional: $("#chkaditional", $element)
          , txtNewLimit: $("#txtNewLimit", $element)
          , divNewLimit: $("#divNewLimit", $element)
          , ddlTypeWork: $("#ddlTipoTrabajo", $element)
          , ddlTypeSubWork: $("#ddlSubTipoTrabajo", $element)
          , txtFProgramacion: $("#txtFProgramacion", $element)
          , ddlFranjaHoraria: $("#ddlFranjaHoraria", $element)
          , lblIdentDocLegalRepresent: $('#lblIdentDocLegalRepresent', $element)
          , spnCableAditionalServiceQty: $('#spnCableAditionalServiceQty', $element)
          , spnInternetAditionalServiceQty: $('#spnInternetAditionalServiceQty', $element)
          , spnPhoneAditionalServiceQty: $('#spnPhoneAditionalServiceQty', $element)
          , spnDecoAditionalServiceQty: $('#spnDecoAditionalServiceQty', $element)
        });

    }

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this,
                controls = this.getControls();

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
            $('button[data-toggle="tab"]').addEvent(that, 'shown.bs.tab', that.tabValidations);
            //controls.sltAction.addEvent(that, 'change', that.sltAction_change);
            controls.txtNewLimit.keypress(that.onlyNumbers_keypress);
            controls.btnAddPlan.addEvent(that, 'click', that.btnAddPlan_Pop);
            controls.btnAddServ.addEvent(that, 'click', that.btnAddServ_Pop);
            controls.btnAddEquip.addEvent(that, 'click', that.btnAddEquip_Pop);
            controls.chkEmail.addEvent(that, 'change', that.chkEmail_change);
            controls.txtEmail.change(function (e) { Smmry.set('email', controls.txtEmail.val()); });
            controls.txtEmail.keyup(function (e) { Smmry.set('email', controls.txtEmail.val()); });
            controls.txtNewLimit.keyup(function (e) { Smmry.set('topeconsumo', controls.txtNewLimit.val()); });
            controls.sltOption.change(function (e) { Smmry.set('topeconsumo', controls.sltOption.val()); });
            controls.txtLetterNumber.change(function (e) { Smmry.set('numerocarta', controls.txtLetterNumber.val()); });
            controls.txtLetterNumber.keyup(function (e) {
                if (controls.txtLetterNumber.val().length > 15) {
                    controls.txtLetterNumber.val(controls.txtLetterNumber.val().substr(0, caracteres));
                }
                Smmry.set('numerocarta', controls.txtLetterNumber.val());
            });
            controls.txtNotes.addEvent(that, 'change', that.txtNotes_change);
            controls.sltOperator.change(function () { that.ddlOperador_change(); });
            controls.sltCacDac.change(function () { that.sltCacDac_change(); });
            controls.chkPublish.addEvent(that, 'click', that.chkPublish_Click);
            controls.btnSave.addEvent(that, 'click', that.btnSave_Click);
            $(".next-trans").addEvent(that, 'click', that.shortCutStep);
            $(".paint").addEvent(that, 'mouseover', that.mouseover);
            $(".paint").addEvent(that, 'mouseout', that.mouseout);
            $(".closeWind").addEvent(that, 'click', that.closeWindow);
            controls.chkaditional.addEvent(that, 'change', that.chkaditional_change);
            document.addEventListener('keyup', that.shortCutStep);
            Session.FECHAACTUALSERVIDOR = CURRENTSERVERDATE;
            controls.btnConstancy.addEvent(that, 'click', that.btnConstancy_click);
            that.loadSessionData();
            that.LTEPlanMigrationLoad().done(function (data) {
                that.render();
            });
        },
        /*sltAction_change: function (ctrl) {
            var that = this,
               controls = that.getControls();
            if (ctrl.val() == 1) {
                controls.sltOption.prop("disabled", false);
                controls.chkFreeOfCharge.prop("disabled", false);
                controls.chkaditional.prop("disabled", false);
            } else {
                controls.sltOption.val("");
                controls.chkFreeOfCharge.prop("checked", false);
                controls.chkaditional.prop("checked", false);
                controls.divNewLimit.hide();

                controls.sltOption.prop("disabled", true);
                controls.chkFreeOfCharge.prop("disabled", true);
                controls.chkaditional.prop("disabled", true);
            }
        },*/
        chkaditional_change: function (sender, arg) {
            var that = this,
                controls = that.getControls();
            if (sender.prop('checked')) {
                that.objLteMigrationPlanValidation.intFlagConsumeCap = 1;
                controls.divNewLimit.show();
                controls.sltOption.val("");
                controls.sltOption.prop("disabled", true);
                controls.txtNewLimit.val("0");
                controls.txtNewLimit.focus();
            } else {
                that.objLteMigrationPlanValidation.intFlagConsumeCap = 0;
                controls.divNewLimit.hide();
                controls.sltOption.prop("disabled", false);
                controls.txtNewLimit.val("0");

            }
        },
        txtNotes_change: function () {
            var that = this,
                controls = that.getControls();
            if (controls.txtNotes.val() != "") {
                var Notas = controls.txtNotes.val();
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
                Smmry.set('Notas', "No se ingresó");
            }

        },
        ddlOperador_change: function () {
            var that = this;
            var controls = that.getControls();

            if (controls.sltOperator.val() != '') {
                Smmry.set('operador', $("#sltOperator option:selected").text());

            }
        },
        sltCacDac_change: function () {
            var that = this;
            var controls = that.getControls();
            Smmry.set('Reintegropenalidad', controls.txtRefound.val());
            Smmry.set('Montofidelizacionpenalidad', controls.txtLoyaltyAmount.val());
            Smmry.set('Total-penalidad', controls.txtTotalPenality.val());
            Smmry.set('chkOcc', controls.chkOCC.is(':checked') ? 'SI' : 'NO');
            Smmry.set('ckhFidelizacion', controls.chkLoyalty.prop("checked") ? "SI" : "NO");
            Smmry.set('presuscrito', controls.chkPreSuscribed.prop("checked") ? "SI" : "NO");
            Smmry.set('publicarnumero', controls.chkPublish.prop("checked") ? "SI" : "NO");
            Smmry.set('chkemail', controls.chkEmail.is(':checked') ? 'SI' : 'NO');
            if (controls.sltCacDac.val() != '') {
                controls.sltCacDac.closest(".form-group").removeClass("has-error");
                $("#ErrorMessageCacDac").text('');
                Smmry.set('PuntoAtencion', $("#sltCacDac option:selected").text());

            }

        },
        chkPublish_Click: function () {
            var that = this,
                controls = that.getControls();
            Smmry.set('publicarnumero', controls.chkPublish.prop("checked") ? "SI" : "NO");
        },
        chkEmail_change: function (sender, arg) {
            var that = this,
                controls = that.getControls();
            if (sender.prop('checked')) {
                controls.divEmailSend.show();
                controls.txtEmail.focus();
            } else {
                controls.divEmailSend.hide();
            }
            Smmry.set('chkemail', controls.chkEmail.is(':checked') ? 'SI' : 'NO');
        },
        chkOCC_Click: function () {
            var that = this,
                controls = that.getControls();
            Smmry.set('chkOcc', controls.chkOCC.is(':checked') ? 'SI' : 'NO');
        },
        mouseover: function (ctrl) {
            var idElement = ctrl[0].id;
            $("#" + idElement).addClass('mouseover');
            $("#" + idElement + "N").addClass('mouseover');
            $("#" + idElement.substr(0, idElement.length - 1)).addClass('mouseover');
        },
        mouseout: function (ctrl) {
            var idElement = ctrl[0].id;
            $("#" + idElement).removeClass('mouseover');
            $("#" + idElement + "N").removeClass('mouseover');
            $("#" + idElement.substr(0, idElement.length - 1)).removeClass('mouseover');
        },
        tabValidations: function (e) {
            var that = this;
            that.validationsTransac(that.objLteMigrationPlanValidation.YouAreValidatingVisit);
            var activeTab = $('.step.tab-pane.active');
            //if (typeof that.objLteMigrationPlanLoad.HaveTelephony != 'undefined')
            //    if (activeTab.prop('id') == "tabServiciosComplementarios" && that.objLteMigrationPlanLoad.HaveTelephony == false) {
            //        var nextBtn = $('.next-step');
            //        navigateTabs(nextBtn);
            //}
            if (typeof that.objLteMigrationPlanLoad.objTechnicalVisit != 'undefined' || that.objLteMigrationPlanLoad.objTechnicalVisit != null)
                if (activeTab.prop('id') == "tabAgendamiento" && that.objLteMigrationPlanLoad.objTechnicalVisit.TechnicalVisit == false) {
                    var nextBtn = $('.next-step');
                    navigateTabs(nextBtn);
            }
            
        },
        loadSessionData: function () {
            SessionTransac = JSON.parse(sessionStorage.getItem("SessionTransac"));
            Session.DATACUSTOMER = SessionTransac.SessionParams.DATACUSTOMER;
            Session.IDSESSION = SessionTransac.UrlParams.IDSESSION;
            Session.DATASERVICE = SessionTransac.SessionParams.DATASERVICE;
            Session.USERACCESS = SessionTransac.SessionParams.USERACCESS;
            Session.idUbigeo = SessionTransac.SessionParams.DATACUSTOMER.CodeCenterPopulate;
            Session.UrlParams = SessionTransac.UrlParams;
            Session.ProductType = ((Session.UrlParams.SUREDIRECT == 'LTE') ? '08' : '05');
        },
        LTEPlanMigrationLoad: function () {
            var that = this,
                controls = that.getControls(),
                objRequestDataModel = {};
            objRequestDataModel.strIdSession = Session.IDSESSION;
            objRequestDataModel.strCodeUser = Session.USERACCESS.login;
            objRequestDataModel.strIdContract = Session.DATACUSTOMER.ContractID;
            objRequestDataModel.strStateLine = Session.DATASERVICE.StateLine;
            objRequestDataModel.strUbigeo = Session.DATACUSTOMER.InstallUbigeo;
            
            
            return $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objRequestDataModel),
                url: '/Transactions/LTE/PlanMigration/LTEPlanMigrationLoad',
                success: function (response) {
                    if (response.data != null) {
                        that.objLteMigrationPlanLoad = response.data;
                        Session.dblIgvView = that.objLteMigrationPlanLoad.dblIgvView;
                    }
                }

            });
        },
        render: function () {
            var that = this,
                controls = this.getControls();

            controls.lblTitle.text("Cambio de Plan");
            controls.lblContract.html((Session.DATACUSTOMER.ContractID == null) ? '' : Session.DATACUSTOMER.ContractID);
            controls.lblCustomerId.html((Session.DATACUSTOMER.CustomerID == null) ? '' : Session.DATACUSTOMER.CustomerID);
            controls.lblCustomerType.html((Session.DATACUSTOMER.CustomerType == null) ? '' : Session.DATACUSTOMER.CustomerType);
            controls.lblCustomer.html((Session.DATACUSTOMER.BusinessName == null) ? '' : Session.DATACUSTOMER.BusinessName);
            controls.lblContact.html((Session.DATACUSTOMER.FullName == null) ? '' : Session.DATACUSTOMER.FullName);
            controls.lblDocNum.html((Session.DATACUSTOMER.DNIRUC == null) ? '' : Session.DATACUSTOMER.DNIRUC);
            controls.lblLegalRep.html((Session.DATACUSTOMER.LegalAgent == null) ? '' : Session.DATACUSTOMER.LegalAgent);
            controls.lblCurrentPlan.html((Session.DATASERVICE.Plan == null) ? '' : Session.DATASERVICE.Plan);
            controls.lblCurrentDate.html((Session.DATACUSTOMER.ActivationDate == null) ? '' : Session.DATACUSTOMER.ActivationDate);
            controls.lblBillingCycle.html((Session.DATACUSTOMER.BillingCycle == null) ? '' : Session.DATACUSTOMER.BillingCycle);
            controls.lblCreditLimit.html((Session.DATACUSTOMER.objPostDataAccount.CreditLimit == null) ? '' : Session.DATACUSTOMER.objPostDataAccount.CreditLimit);
            controls.lblAddress.html((Session.DATACUSTOMER.InvoiceAddress == null) ? '' : Session.DATACUSTOMER.InvoiceAddress);
            controls.lblReference.html((Session.DATACUSTOMER.InvoiceUrbanization == null) ? '' : Session.DATACUSTOMER.InvoiceUrbanization);
            controls.lblCountry.html((Session.DATACUSTOMER.InvoiceCountry == null) ? '' : Session.DATACUSTOMER.InvoiceCountry);
            controls.lblDepartment.html((Session.DATACUSTOMER.InvoiceDepartament == null) ? '' : Session.DATACUSTOMER.InvoiceDepartament);
            controls.lblProvince.html((Session.DATACUSTOMER.InvoiceProvince == null) ? '' : Session.DATACUSTOMER.InvoiceProvince);
            controls.lblDistrict.html((Session.DATACUSTOMER.InvoiceDistrict == null) ? '' : Session.DATACUSTOMER.InvoiceDistrict);
            controls.lblUbigeoCode.html((Session.DATACUSTOMER.InstallUbigeo == null) ? '' : Session.DATACUSTOMER.InstallUbigeo);
            controls.txtEmail.val((Session.DATACUSTOMER.Email == null) ? '' : Session.DATACUSTOMER.Email);
            controls.lblIdentDocLegalRepresent.html((Session.DATACUSTOMER.DocumentNumber == null) ? '' : Session.DATACUSTOMER.DocumentNumber);

            if (that.objLteMigrationPlanLoad.objLteMigrationPlanMessage.strValidationLine != "") {
                controls.btnAddPlan.prop("disabled", true);
                    alert(that.objLteMigrationPlanLoad.objLteMigrationPlanMessage.strValidationLine, 'Informativo', function () {
                        parent.window.close();
                    });
            }
               
            if (that.objLteMigrationPlanLoad.strMessage != null && that.objLteMigrationPlanLoad.strMessage != "") {
                controls.btnAddPlan.prop("disabled", true);
                alert(that.objLteMigrationPlanLoad.objLteMigrationPlanMessage.strDontLoadTipification, 'Informativo', function () {
                    parent.window.close();
                });
            }
            if (that.objLteMigrationPlanLoad.strPhone == "") {
                controls.btnAddPlan.prop("disabled", true);
                alert(that.objLteMigrationPlanLoad.objLteMigrationPlanMessage.strThereAreNoPhone, 'Informativo', function () {
                    parent.window.close();
                });
            }
            if (that.objLteMigrationPlanLoad.lstCacDacTypes != null && that.objLteMigrationPlanLoad.lstCacDacTypes.length > 0)
                that.getCacDacType(that.objLteMigrationPlanLoad.lstCacDacTypes, that.objLteMigrationPlanLoad.strUserCac);
            else if (that.objLteMigrationPlanLoad.objLteMigrationPlanMessage.strValidationChargueList != null && that.objLteMigrationPlanLoad.objLteMigrationPlanMessage.strValidationChargueList != "") {
                alert(that.objLteMigrationPlanLoad.objLteMigrationPlanMessage.strValidationChargueList.replace("###", "Punto de Atención"), 'Informativo', function () { });

            }
            if (that.objLteMigrationPlanLoad.lstBusinessRules != null)
                that.getBusinessRules(that.objLteMigrationPlanLoad.lstBusinessRules);
            else if (that.objLteMigrationPlanLoad.objLteMigrationPlanMessage.strValidationChargueList != null && that.objLteMigrationPlanLoad.objLteMigrationPlanMessage.strValidationChargueList != "") {
                alert(that.objLteMigrationPlanLoad.objLteMigrationPlanMessage.strValidationChargueList.replace("###", "Reglas de Negocio"), 'Informativo', function () { });
            }
            
            if (that.objLteMigrationPlanLoad.dblIgv == null) {
                alert(that.objLteMigrationPlanLoad.objLteMigrationPlanMessage.strErrorMessageIgv, "Informativo");
            }
            if (that.objLteMigrationPlanLoad.objServicesByCurrentPlanCharges != null) {
                that.getCurrentPlanServices(that.objLteMigrationPlanLoad.lstServicesByCurrentPlan, that.objLteMigrationPlanLoad.objServicesByCurrentPlanCharges);
            }
            if (that.objLteMigrationPlanLoad.lstEquipmentByCurrenPlan != null) {
                that.getEquipmentByCurrentPlan(that.objLteMigrationPlanLoad.lstEquipmentByCurrenPlan);
            }
            if (that.objLteMigrationPlanLoad.lstCarriers != null) {
                that.getCarries(that.objLteMigrationPlanLoad.lstCarriers);
            }

            Session.intLTEValidateVel = that.objLteMigrationPlanLoad.intLTEValidateVel;
            if (that.objLteMigrationPlanLoad.intLTEValidateVel == 0) {
                controls.spnValidateVelPlan.html((that.objLteMigrationPlanLoad.objLteMigrationPlanMessage.strMessageValidateVelPlan == null) ? '' : that.objLteMigrationPlanLoad.objLteMigrationPlanMessage.strMessageValidateVelPlan);
            }
            Session.strOffice = that.objLteMigrationPlanLoad.objOffice.strCodeOffice;
            Session.objQuantityTypeDeco = {};
            that.objLteMigrationPlanLoad.objTechnicalVisit = {};
            that.objLteMigrationPlanValidation.YouAreValidatingVisit = 0;
            that.objLteMigrationPlanValidation.intFlagConsumeCap = 0;
            controls.sltAction.prop("disabled", true);
            if (Session.DATACUSTOMER.Email != null)
            {
                if (Session.DATACUSTOMER.Email != "") {
                controls.chkEmail.attr('checked', true);
                controls.divEmailSend.show();
                controls.txtEmail.focus();
            }
            }
            Smmry.set('chkemail', controls.chkEmail.is(':checked') ? 'SI' : 'NO');
            Smmry.set('topeconsumo', 'No se ingresó');
            Smmry.set('Notas', "No se ingresó");
            Smmry.set('publicarnumero', 'NO');
            Smmry.set('presuscrito', 'NO');
            Smmry.set('numerocarta', 'No se ingresó');
            Smmry.set('operador', 'No seleccionado');
            Smmry.set('ckhFidelizacion', 'NO');
            Smmry.set('Reintegropenalidad', '0');
            Smmry.set('Montofidelizacionpenalidad', '0');
            Smmry.set('Total-penalidad', '0');
            Smmry.set('chkOcc', 'NO');
            Smmry.set('chkemail', 'NO');
            Smmry.set('PuntoAtencion', $('#sltCacDac option:selected').text());
            Smmry.set('chkemail', controls.chkEmail.is(':checked') ? 'SI' : 'NO');
            Smmry.set('email', controls.txtEmail.val());
            controls.btnConstancy.prop("disabled", true);

        },
        getCacDacType: function (lstCacDacTypes, strCodeUser) {
            var that = this,
                controls = that.getControls();
          
            if (lstCacDacTypes.length > 0) {
                controls.sltCacDac.append($('<option>', { value: '', html: 'Seleccionar' }));
                var itemSelect;
                $.each(lstCacDacTypes, function (index, value) {
                    if (strCodeUser === value.Description) {
                        controls.sltCacDac.append($('<option>', { value: value.Code, html: value.Description }));
                        itemSelect = value.Code;

                    } else {
                        controls.sltCacDac.append($('<option>', { value: value.Code, html: value.Description }));
                    }
                });
                if (itemSelect != null && itemSelect.toString != "undefined") {
                    $("#sltCacDac option[value=" + itemSelect + "]").attr("selected", true);
                }

            }
        },
        getBusinessRules: function (lstBusinessRules) {
            var that = this,
                controls = that.getControls();
            if (lstBusinessRules.length > 0) {
                controls.divRules.append(lstBusinessRules[0].Description);
            }

        },
        getActions: function (lstActions,strValueDefault) {
            var that = this,
                controls = that.getControls();
            if (lstActions.length > 0) {
                var itemSelect;
                $.each(lstActions, function (index, value) {
                    if (strValueDefault === value.Description) {
                        controls.sltAction.append($('<option>', { value: value.Code, html: value.Description }));
                        itemSelect = value.Code;

                    } else {
                        controls.sltAction.append($('<option>', { value: value.Code, html: value.Description }));
                    }

                });
            }
        },
        getOptions: function (lstOptions) {
            var that = this,
                controls = that.getControls();
            if (lstOptions.length > 0) {
                controls.sltOption.append($('<option>', { value: '', html: 'Seleccionar' }));
                var itemSelect;
                $.each(lstOptions, function (index, value) {
                    controls.sltOption.append($('<option>', { value: value.Code, html: value.Description }));
                });
            }
        },
        shortCutStep: function (e) {
            var that = this;
            var $activeTab = $('.step.tab-pane.active');
            if ((e.ctrlKey && e.keyCode == 39) || e.keyCode == null) {
                
                that.validationsSteps($activeTab.prop('id'), function (response) {
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
        getCurrentPlanServices: function (lstServicesByCurrentPlan, objServicesByCurrentPlanCharges) {
            var that = this;
            var controls = that.getControls();

            var intCable = 0, intInternet = 0, intPhone = 0;
            if (objServicesByCurrentPlanCharges != null) {
                var intAmountActualBaseWithIgv = parseFloat(objServicesByCurrentPlanCharges.MontoActualBase) * parseFloat(that.objLteMigrationPlanLoad.dblIgvView);
                controls.spnCurrentBaseAmount.text('S/.' + objServicesByCurrentPlanCharges.MontoActualBase.toFixed(2));
                var intChargeAdditionalCurrentAmount = parseFloat(objServicesByCurrentPlanCharges.MontoActualAdicional) * parseFloat(that.objLteMigrationPlanLoad.dblIgvView);
                controls.spnAdditionalCurrentAmount.text('S/.' + intChargeAdditionalCurrentAmount.toFixed(2));
                controls.spnCurrentTotalFixedChargeSIGV.text('S/.' + objServicesByCurrentPlanCharges.MontoActualBase);
                controls.spnQuantityCurrentServices.text(objServicesByCurrentPlanCharges.CantidadServicios);
                var intchargeTotalWithOutIGV = objServicesByCurrentPlanCharges.MontoActualBase + objServicesByCurrentPlanCharges.MontoActualAdicional;
                controls.spnCurrentTotalFixedChargeSIGV.text('S/.' + intchargeTotalWithOutIGV.toFixed(2));
                var intchargeTotalWithIGV = intAmountActualBaseWithIgv + intChargeAdditionalCurrentAmount;
                controls.spnCurrentTotalFixedChargeCIGV.text('S/.' + intchargeTotalWithIGV.toFixed(2));
            }   
            if (lstServicesByCurrentPlan != null) {
                $.each(lstServicesByCurrentPlan, function (index, item) {
                        if (that.objLteMigrationPlanLoad.objLteMigrationPlanMessage.strCurrentCablePlan.indexOf(item.NoGrp) > -1) {
                            if (item.ServiceType.toUpperCase() === "CORE") {
                            controls.lstCurrentPlanCable.append('<li class="transac-list-group-item"><span class="badge">S/.' + item.CargoFijo + '</span> ' + item.DeSer + '</li>');
                            } else {
                                controls.lstCurrentPlanCable.append('<li class="transac-list-group-item"><span class="badge">S/.' + item.CargoFijo + '</span> <b>' + item.DeSer + '</b></li>');
                            }
                            intCable++;
                            var servicesForSummary = {};
                            servicesForSummary.Service = "Cable";
                            servicesForSummary.Type = item.ServiceType;
                            servicesForSummary.ServiceName = item.DeSer;
                            servicesForSummary.FixedCharge = item.CargoFijo;
                            servicesForSummary.CodServSisact = item.CodServSisact;
                            controls.lstCurrentPlanSummary.append('<li class="transac-list-group-item"><div class="row"><div class="col-sm-2">' + servicesForSummary.Service + '</div><div class="col-sm-2">' + servicesForSummary.Type + '</div><div class="col-sm-6">' + servicesForSummary.ServiceName + '</div><div class="col-sm-2"><span class="badge transac-badge-row">' + servicesForSummary.FixedCharge + '</span></div></div></li>');
                        }
                        if (that.objLteMigrationPlanLoad.objLteMigrationPlanMessage.strCurrentInternetPlan.indexOf(item.NoGrp) > -1) {
                            if (item.ServiceType.toUpperCase() === "CORE") {
                            controls.lstCurrentPlanInternet.append('<li class="transac-list-group-item"><span class="badge">S/.' + item.CargoFijo + '</span> ' + item.DeSer + '</li>');
                        } else {
                            controls.lstCurrentPlanInternet.append('<li class="transac-list-group-item"><span class="badge">S/.' + item.CargoFijo + '</span> <b>' + item.DeSer + '</b></li>');
                        }
                            intInternet++;
                            var servicesForSummary = {};
                            servicesForSummary.Service = "Internet";
                            servicesForSummary.Type = item.ServiceType;
                            servicesForSummary.ServiceName = item.DeSer;
                            servicesForSummary.FixedCharge = item.CargoFijo;
                            servicesForSummary.CodServSisact = item.CodServSisact;
                            controls.lstCurrentPlanSummary.append('<li class="transac-list-group-item"><div class="row"><div class="col-sm-2">' + servicesForSummary.Service + '</div><div class="col-sm-2">' + servicesForSummary.Type + '</div><div class="col-sm-6">' + servicesForSummary.ServiceName + '</div><div class="col-sm-2"><span class="badge transac-badge-row">' + servicesForSummary.FixedCharge + '</span></div></div></li>');
                        }
                        if (that.objLteMigrationPlanLoad.objLteMigrationPlanMessage.strCurrentPhonePlan.indexOf(item.NoGrp) > -1) {
                            if (item.ServiceType === "CORE") {
                            controls.lstCurrentPlanTelephony.append('<li class="transac-list-group-item"><span class="badge">S/.' + item.CargoFijo + '</span> ' + item.DeSer + '</li>');
                        } else {
                            controls.lstCurrentPlanTelephony.append('<li class="transac-list-group-item"><span class="badge">S/.' + item.CargoFijo + '</span> <b>' + item.DeSer + '</b></li>');
                        }
                            intPhone++;
                            var servicesForSummary = {};
                            servicesForSummary.Service = "Teléfono";
                            servicesForSummary.Type = item.ServiceType;
                            servicesForSummary.ServiceName = item.DeSer;
                            servicesForSummary.FixedCharge = item.CargoFijo;
                            servicesForSummary.CodServSisact = item.CodServSisact;
                            controls.lstCurrentPlanSummary.append('<li class="transac-list-group-item"><div class="row"><div class="col-sm-2">' + servicesForSummary.Service + '</div><div class="col-sm-2">' + servicesForSummary.Type + '</div><div class="col-sm-6">' + servicesForSummary.ServiceName + '</div><div class="col-sm-2"><span class="badge transac-badge-row">' + servicesForSummary.FixedCharge + '</span></div></div></li>');
                        }
                });
                    }

            if (intCable == 0) controls.lstCurrentPlanCable.append('<li class="transac-list-group-item transac-message-red text-center"> ' + that.objLteMigrationPlanLoad.objLteMigrationPlanMessage.strThereAreNoRecords + ' </li>');
            if (intInternet == 0) controls.lstCurrentPlanInternet.append('<li class="transac-list-group-item transac-message-red text-center"> ' + that.objLteMigrationPlanLoad.objLteMigrationPlanMessage.strThereAreNoRecords + ' </li>');
            if (intPhone == 0) controls.lstCurrentPlanTelephony.append('<li class="transac-list-group-item transac-message-red text-center"> ' + that.objLteMigrationPlanLoad.objLteMigrationPlanMessage.strThereAreNoRecords + ' </li>');

        },
        getEquipmentByCurrentPlan: function (lstEquipmentByCurrenPlan) {
            var that = this,
            controls = that.getControls();
            controls.lstCurrentEquipCable.append('<li class="transac-list-group-item"><div align="center"><img src="' + that.strUrlLogo + '" width="25" height="25" /> Cargando ... </div></li>');
            controls.lstCurrentEquipInternet.append('<li class="transac-list-group-item"><div align="center"><img src="' + that.strUrlLogo + '" width="25" height="25" /> Cargando ... </div></li>');
            controls.lstCurrentEquipTelephony.append('<li class="transac-list-group-item"><div align="center"><img src="' + that.strUrlLogo + '" width="25" height="25" /> Cargando ... </div></li>');
            var intCable = 0, intInternet = 0, intPhone = 0;
            controls.lstCurrentEquipCable.html('');
            controls.lstCurrentEquipInternet.html('');
            controls.lstCurrentEquipTelephony.html('');
            var intQuantityCurrentEquipment = 0;
            $.each(lstEquipmentByCurrenPlan, function (index, item) {
                var strDesc = item.strDscequ;
                switch (item.strTipo) {
                    case "SD":
                        strDesc += ' <span class="glyphicon glyphicon-sd-video" aria-hidden="true"></span>';
                        break;
                    case "HD":
                        strDesc += ' <span class="glyphicon glyphicon-hd-video" aria-hidden="true"></span>';
                        break;
                    case "DVR":
                        strDesc += ' <span class="glyphicon glyphicon-hdd" aria-hidden="true"></span>';
                        break;
                }
                switch (item.strTipsrv) {
                    case "CABLE":
                        that.getEquipmentByCurrentPlanByService(controls.lstCurrentEquipCable, item.strTipo_srv, item.intCantidad, strDesc);
                        intCable++;
                    break;
                    case "INTERNET":
                        that.getEquipmentByCurrentPlanByService(controls.lstCurrentEquipInternet, item.strTipo_srv, item.intCantidad, strDesc);
                        intInternet++;
                    break;
                    case "TELEFONO":
                        that.getEquipmentByCurrentPlanByService(controls.lstCurrentEquipTelephony, item.strTipo_srv, item.intCantidad, strDesc);
                        //intQuantityCurrentEquipment = intQuantityCurrentEquipment + parseInt(item.intCantidad);
                        intPhone++;
                    break;
                }
                intQuantityCurrentEquipment = intQuantityCurrentEquipment + parseInt(item.intCantidad);
                });
            controls.spnQuantityCurrentEquipment.text(intQuantityCurrentEquipment);
            if (intCable == 0) controls.lstCurrentEquipCable.append('<li class="transac-list-group-item transac-message-red text-center"> ' + that.objLteMigrationPlanLoad.objLteMigrationPlanMessage.strThereAreNoRecords + ' </li>');
            if (intInternet == 0) controls.lstCurrentEquipInternet.append('<li class="transac-list-group-item transac-message-red text-center"> ' + that.objLteMigrationPlanLoad.objLteMigrationPlanMessage.strThereAreNoRecords + ' </li>');
            if (intPhone == 0) controls.lstCurrentEquipTelephony.append('<li class="transac-list-group-item transac-message-red text-center"> ' + that.objLteMigrationPlanLoad.objLteMigrationPlanMessage.strThereAreNoRecords + ' </li>');

        },
        getEquipmentByCurrentPlanByService: function(lstEquipment, strTipo_srv, intCantidad, strDesc) {
            if (strTipo_srv == "INCLUIDO") {
                lstEquipment.append('<li class="transac-list-group-item"><span class="badge">' + intCantidad + '</span>' + strDesc + '</li>');
            } else {
                lstEquipment.append('<li class="transac-list-group-item"><span class="badge">' + intCantidad + '</span><b>' + strDesc + '</b></li>');
            }
        },
        getCarries: function (lstCarriers) {
            if (lstCarriers.length > 0) {
                var that = this,
                    controls = that.getControls();
                $.each(lstCarriers, function (index, item) {
                    if (item.IDCARRIER == '00' && item.OPERADOR == 'AMERICA MOVIL PERU SAC') {
                        controls.sltOperator.append('<option selected value="' + item.IDCARRIER + '">' + item.OPERADOR + '</option>');
                    }
                    else {
                        controls.sltOperator.append($('<option>', { value: item.IDCARRIER, html: item.OPERADOR }));
                    }

                });
            }
        },
        getControls: function () {
            return this.m_controls || {};
        },
        setControles: function (value) {
            this.m_controls = value;
        },
        validationsTransac: function (YouAreValidatingVisit) {
            var that = this;
            if (YouAreValidatingVisit == 0)
                that.getTechnicalVisitResult();
        },
        validationsSteps: function (stepName, fn) {
            var that = this,
                controls = that.getControls();

            if (stepName == 'tabPlanYServiciosAdicionales') {
             fn(true);
                
            } else {
                if (stepName == 'tabDatosTecnicos') {
                    Form.prototype.ValidateTecnicData(function (response) {
                        if (response) fn(true); else fn(false);
                    });
                } else {
                    if (stepName == 'tabPenalidades') {
                        fn(true);
                       
                    }
                 else {
                        if (stepName == 'tabServiciosComplementarios') {
                            Form.prototype.ValidateComplementaryServices(that, function (response) {
                                if (response) fn(true); else fn(false);
                            });
                        }
                    }
                }
            }
            return true;
        },
        ValidateComplementaryServices: function (that, fn) {
            var controls = that.getControls();

            if (!controls.sltOption.val() == '' || (controls.chkaditional.is(":checked") && !controls.txtNewLimit.val() == '')) {
                fn(true);
                return true;  
            } else {
                if (controls.sltAction.val() == '')
                    alert(that.objLteMigrationPlanLoad.objLteMigrationPlanMessage.strFieldValidationSlt.replace("###", "una Acción"), "Alerta");
                if (!controls.chkaditional.is(":checked") && controls.sltOption.val() == '')
                    alert(that.objLteMigrationPlanLoad.objLteMigrationPlanMessage.strFieldValidationSlt.replace("###", "una Opción de Tope de Consumo"), "Alerta");
                if (controls.chkaditional.is(":checked") && controls.txtNewLimit.val() == '')
                    alert(that.objLteMigrationPlanLoad.objLteMigrationPlanMessage.strFieldValidation.replace("###", "nuevo Tope Adicional"), "Alerta");
                fn(false);
                return false;
            }
        },
        ValidateTecnicData: function (fn) {
            var that = this,
                controls = this.getControls();
            if ($("#sltCacDac").val() == '') {
                $("#ErrorMessageCacDac").text("Debe Seleccionar un punto de Atención.");
                $("#sltCacDac").closest(".form-group").addClass("has-error");
                fn(false);
                return false;
            }

            else {
            }
            if ($('#chkEmail').is(':checked')) {
                if (!that.ValidateEmail($("#txtEmail").val())) {
                    fn(false);
                    return false;
                }
                else {
                    $("#txtEmail").closest(".form-group").removeClass("has-error");
                }
            }
            fn(true);
        },
        ValidateEmail: function (email) {
            var that = this,
                filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
            if (email.length == 0) {
                $("#ErrorMessageEmail").text(that.MSG_ERROR_EMAIL);
                $('#txtEmail').closest(".form-group").addClass("has-error");
                return false;
            }
            if (filter.test(email))
                return true;
            else {
                $("#ErrorMessageEmail").text(that.MSG_ERROR_EMAIL);
                $('#txtEmail').closest(".form-group").addClass("has-error");
            }

            return false;
        },
        btnAddPlan_Pop: function () {
            var that = this,
                controls = that.getControls();

            var urlBase = window.location.href;
            urlBase = urlBase.substr(0, urlBase.lastIndexOf('/'));
            urlBase = location.protocol + '//' + location.host + '/Transactions/LTE/PlanMigration/LTEChoosePlan';
            

            var dialog = $.window.open({
                modal: true,
                type: 'post',
                title: "Seleccionar Nuevo Plan",
                url: urlBase,
                //data: {},
                width: 1024,
                height: 620,
                buttons: {
                    Seleccionar: {
                        click: function () {
                            var rowPost = $('#tblPlans').DataTable().rows({ selected: true }).data();
                            var item = rowPost[0];
                            if (item === undefined) {
                                alert(that.objLteMigrationPlanLoad.objLteMigrationPlanMessage.strFieldValidationSlt.replace("###", "un plan."), "Alerta");
                                return false;
                            }
                            that.charguePlan_Pop(item.strCodPlanSisact, item.strDesPlanSisact, item.strSolucion, this);
                            
                        }
                    },
                    Cancelar: {
                        click: function (sender, args) {
                            this.close();
                        }
                    }
                }
            });

        },
        btnAddServ_Pop: function () {
            var that = this,
                controls = that.getControls();

            var urlBase = window.location.href;
            urlBase = urlBase.substr(0, urlBase.lastIndexOf('/'));
            var that = this;
            
            var dialog = $.window.open({
                modal: true,
                title: "Seleccionar Servicios Adicionales",
                url: '/Transactions/LTE/PlanMigration/LTEChooseServicesByPlan',
                width: 1024,
                height: 600,
                buttons: {
                    Seleccionar: {
                        click: function () {
                            var ModalConfirm = this;
                            var rowPostCable = $('#tblChooseServicesByPlanCable').DataTable().rows({ selected: true }).data();
                            var rowPostInternet = $('#tblChooseServicesByPlanInternet').DataTable().rows({ selected: true }).data();
                            var rowPostPhone = $('#tblChooseServicesByPlanPhone').DataTable().rows({ selected: true }).data();
                            if (rowPostCable.length === 0 && rowPostInternet === 0 && rowPostPhone === 0) {
                                alert("Necesita seleccionar un servicio.", "Alerta");
                                return false;
                            }

                            confirm('Los adicionales seleccionados se cargarán, ¿desea cargarlos?', 'Confirmar', function (result) {
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

                                    if (typeof that.objLteMigrationPlanValidation.objAditionalServices.lstCable === 'undefined') {
                                        that.objLteMigrationPlanValidation.objAditionalServices.lstCable = [];
                                    }
                                    if (typeof that.objLteMigrationPlanValidation.objAditionalServices.lstInternet === 'undefined') {
                                        that.objLteMigrationPlanValidation.objAditionalServices.lstInternet = [];
                                    }
                                    if (typeof that.objLteMigrationPlanValidation.objAditionalServices.lstPhone === 'undefined') {
                                        that.objLteMigrationPlanValidation.objAditionalServices.lstPhone = [];
                                    }

                                    that.objLteMigrationPlanValidation.objAditionalServices.lstCable = that.addNewItemToList("Cable", controls.lstSelectPlanCable, rowPostCable, that.objLteMigrationPlanValidation.objAditionalServices.lstCable);
                                    that.objLteMigrationPlanValidation.objAditionalServices.lstInternet = that.addNewItemToList("Internet", controls.lstSelectPlanInternet, rowPostInternet, that.objLteMigrationPlanValidation.objAditionalServices.lstInternet);
                                    that.objLteMigrationPlanValidation.objAditionalServices.lstPhone = that.addNewItemToList("Phone", controls.lstSelectPlanTelephony, rowPostPhone, that.objLteMigrationPlanValidation.objAditionalServices.lstPhone);
                                    Session.objAditionalServices = that.objLteMigrationPlanValidation.objAditionalServices;
                                    that.objLteMigrationPlanLoad.objTechnicalVisit = {};
                                    that.objLteMigrationPlanValidation.YouAreValidatingVisit = 0;

                                    $.unblockUI();

                                    ModalConfirm.close();
                                }
                            });

                        }
        },
                    Cancelar: {
                        click: function (sender, args) {
                            this.close();
                        }
                    }
                }
            });
        },
        addNewItemToList: function (type, lst, items, obj) {
            var that = this,
                controls = that.getControls(),
                liHTML = '';

            if (items.length > 0) {
                $.each(items, function (index, item) {
                    obj.push(item);
                    if (type === "Deco") {
                        
                        if (item.Codtipequ == that.objLteMigrationPlanLoad.objLteMigrationPlanMessage.strCodeSD) {
                            liHTML = '<li id="' + item.CodServSisact + '" class="transac-list-group-item ' + type + '"><button id="delete_' + item.CodServSisact + '" type="button" class="transac-close"  data-toggle="tooltip" data-rfilter="' + item.Codtipequ + '" title="Quitar de la lista"><span>&times;</span></button><span class="badge">S/.' + item.CfWithIgv + '</span> <b>' + item.DesServSisact + ' <span class="glyphicon glyphicon-sd-video" aria-hidden="true"></span></b></li>';
                        }
                        
                        else if (item.Codtipequ == that.objLteMigrationPlanLoad.objLteMigrationPlanMessage.strCodeHD) {
                            liHTML = '<li id="' + item.CodServSisact + '" class="transac-list-group-item ' + type + '"><button id="delete_' + item.CodServSisact + '" type="button" class="transac-close"  data-toggle="tooltip" data-rfilter="' + item.Codtipequ + '" title="Quitar de la lista"><span>&times;</span></button><span class="badge">S/.' + item.CfWithIgv + '</span> <b>' + item.DesServSisact + ' <span class="glyphicon glyphicon-hd-video" aria-hidden="true"></span></b></li>';
                        }
                        
                        else if (item.Codtipequ == that.objLteMigrationPlanLoad.objLteMigrationPlanMessage.strCodeDVR) {
                            liHTML = '<li id="' + item.CodServSisact + '" class="transac-list-group-item ' + type + '"><button id="delete_' + item.CodServSisact + '" type="button" class="transac-close"  data-toggle="tooltip" data-rfilter="' + item.Codtipequ + '" title="Quitar de la lista"><span>&times;</span></button><span class="badge">S/.' + item.CfWithIgv + '</span> <b>' + item.DesServSisact + ' <span class="glyphicon glyphicon-hdd" aria-hidden="true"></span></b></li>';

                        }
                    } else {
                        liHTML = '<li id="' + item.CodServSisact + '" class="transac-list-group-item ' + type + '"><button id="delete_' + item.CodServSisact + '" type="button" class="transac-close"  data-toggle="tooltip" data-rfilter="' + item.Codtipequ + '" title="Quitar de la lista"><span>&times;</span></button><span class="badge">S/.' + item.CfWithIgv + '</span> <b>' + item.DesServSisact + '</b></li>';
                    }
                    lst.append(liHTML);
                    controls.lstNewPlanSummary.append('<li id="summary_' + item.CodServSisact + '" class="transac-list-group-item"><div class="row"><div class="col-sm-2">' + that.isnull(type, "") + '</div><div class="col-sm-2">' + that.replaceServiceType(that.isnull(item.ServiceType, "")) + '</div><div class="col-sm-6">' + that.isnull(item.DesServSisact, "") + '</div><div class="col-sm-2"><span class="badge transac-badge-row">' + that.isnull(item.CfWithIgv, "0") + '</span></div></div></li>');
                    $('#delete_' + item.CodServSisact).click(function () {
                        $.each($('.DecoQuantity'), function (id, item) {
                            $('#' + item.id).remove();

                        });
                        $(this).closest('li').remove();
                        var strDescription = $(this).data("rfilter");
                        $('#summary_' + item.CodServSisact).remove();
                        that.subtractAmounts(type, item.CF);
                        that.removeAditionalService(type);
                        obj = $.grep(obj, function (itm, id) { return itm.CodServSisact === item.CodServSisact }, true);
                        that.refreshSessionAditionalService(type, obj, strDescription);
                        if (lst.children().length == 0) {
                            lst.append('<li class="transac-list-group-item transac-message-red text-center">No existen registros</li>');
                        }
                        that.loadNewAdditionalDeco();
                        that.objLteMigrationPlanValidation.YouAreValidatingVisit = 0;
                    });
                    that.addAdditionalService(type);
                    that.addAmountNewPlan(type, item, type);
                });
            }

            return obj;
        },
        verifyQuantityMinMax: function (lstEquipmentDecoCableAdditional) {
            var that = this;
            var intQuantitySD = 0, intQuantityHD = 0, intQuantityDVR = 0;
            $.each(lstEquipmentDecoCableAdditional, function (index, value) {
                if (value.Codtipequ == that.objLteMigrationPlanLoad.objLteMigrationPlanMessage.strCodeSD) {
                    intQuantitySD++;
                }
                else if (value.Codtipequ == that.objLteMigrationPlanLoad.objLteMigrationPlanMessage.strCodeHD) {
                    intQuantityHD++;
                }
                else if (value.Codtipequ == that.objLteMigrationPlanLoad.objLteMigrationPlanMessage.strCodeDVR) {
                    intQuantityDVR++;
                }
            });
            Session.objQuantityTypeDeco.QuantityMaxDecoHD = intQuantityHD;
            Session.objQuantityTypeDeco.QuantityMaxDecoDVR = intQuantityDVR;
            Session.objQuantityTypeDeco.QuantityMaxDecoSD = intQuantitySD;

        },
        charguePlan_Pop: function (idNewPlan, strNewPlan, strNewSolution, objPopUp) {
            var that = this,
                controls = that.getControls(),
                strAlertMsg = 'Si cambia el plan se borrarán los datos cargados en las tablas, desea cargar un nuevo plan?';
            if (that.objLteMigrationPlanLoad.intLTEValidateVel == 0) {
                strAlertMsg += '<br /><span class="transac-message-red">' + that.objLteMigrationPlanLoad.objLteMigrationPlanMessage.strMessageValidateVelPlan + '</span>';
            }
            confirm(strAlertMsg, "Confirmar", function () {
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
                that.objLteMigrationPlanValidation.NewPlanName = strNewPlan;
                that.objLteMigrationPlanValidation.NewSolutionName = strNewSolution;
                that.objLteMigrationPlanValidation.NewPlanId = idNewPlan;

                objPopUp.close();
                that.btnChooseCoreServicesByPlan_Pop();
                $.unblockUI();
            });
        },
        btnAddEquip_Pop: function () {
            var that = this,
                controls = that.getControls();
            if (that.objLteMigrationPlanValidation.lstEquipmentDecoCableAdditional.length < 1) {
                alert(that.objLteMigrationPlanLoad.objLteMigrationPlanMessage.strThereAreNoRecordsIn.replace("###", "Puntos adicionales"), "Informativo");
                return;
            }
            Session.objQuantityTypeDeco.BeforeDecoHD = Session.objQuantityTypeDeco.DecoHD;
            Session.objQuantityTypeDeco.BeforeDecoDVR = Session.objQuantityTypeDeco.DecoDVR;
            Session.objQuantityTypeDeco.BeforeDecoSD = Session.objQuantityTypeDeco.DecoSD;
            that.verifyQuantityMinMax(that.objLteMigrationPlanValidation.lstEquipmentDecoCableAdditional);
            var intValEquipmentBefore = parseInt($("#spnDecoAditionalServiceQty").text());
            $.window.open({
                modal: true,
                title: "Equipos en alquiler",
                url: "/Transactions/LTE/PlanMigration/LTEChooseEquipmentByPlan",
                width: 400,
                height: 370,
                buttons: {
                    Seleccionar: {
                        click: function () {
                            that.objLteMigrationPlanLoad.objTechnicalVisit = {};
                            that.objLteMigrationPlanValidation.YouAreValidatingVisit = 0;
                            var strResult = that.validationAdditionalEquipment(that.objLteMigrationPlanValidation.lstEquipmentDecoCableAdditional);
                            if (strResult != "") {
                                Session.objQuantityTypeDeco.DecoHD = Session.objQuantityTypeDeco.BeforeDecoHD;
                                Session.objQuantityTypeDeco.DecoDVR = Session.objQuantityTypeDeco.BeforeDecoDVR;
                                Session.objQuantityTypeDeco.DecoSD = Session.objQuantityTypeDeco.BeforeDecoSD;
                                alert(that.objLteMigrationPlanLoad.objLteMigrationPlanMessage.strMessageValidationQuantityDecos.replace("###", strResult), 'Informativo', function () { });
                            }
                            else {
                                $("#spnDecoAditionalServiceQty").text("0");
                                that.assignAdditionalEquipment(that.objLteMigrationPlanValidation.lstEquipmentDecoCableAdditional);
                                $.each($('.Deco'), function (id, item) {
                                    $('#' + item.id).remove();
                                });
                                $.each($('.DecoQuantity'), function (id, item) {
                                    $('#' + item.id).remove();
                                });
                                that.objLteMigrationPlanValidation.lstAdditionalEquipmentQuantity = that.addNewItemToList("Deco", controls.lstSelectPlanCable, that.objLteMigrationPlanValidation.lstAdditionalEquipmentQuantity, []);
                                that.loadNewAdditionalDeco();   
                                var intQuantity = parseInt(controls.spnQuantityNewEquipment.text()) + parseInt($("#spnDecoAditionalServiceQty").text());
                                controls.spnQuantityNewEquipment.text(intQuantity);
                            }
                            
                            this.close();
                        }
                    },
                    Cancelar: {
                        click: function (sender, args) {
                            Session.objQuantityTypeDeco.DecoHD = Session.objQuantityTypeDeco.BeforeDecoHD;
                            Session.objQuantityTypeDeco.DecoDVR = Session.objQuantityTypeDeco.BeforeDecoDVR;
                            Session.objQuantityTypeDeco.DecoSD = Session.objQuantityTypeDeco.BeforeDecoSD;

                            this.close();
                        }
                    }
                }
            });
        },
        clearTables: function () {
            var that = this;
            var controls = that.getControls();
            controls.lstSelectPlanCable.html('');
            controls.lstSelectPlanInternet.html('');
            controls.lstSelectPlanTelephony.html('');
            controls.lstSelectEquipInternet.html('');
            controls.lstSelectEquipTelephony.html('');
            controls.lstSelectEquipCable.html('');
            controls.spnCableEquipmentQty.text("0");
            controls.spnInternetEquipmentQty.text("0");
            controls.spnPhoneEquipmentQty.text("0");
            controls.spnDecosQuantity.text("0");
            controls.spnNewBaseAmount.text("S/.0");
            controls.spnAdditionalNewAmount.text("S/.0");
            controls.spnNewTotalFixedChargeSIGV.text("S/.0");
            controls.spnNewTotalFixedChargeCIGV.text("S/.0");
            controls.spnQuantityNewServices.text("0");
            controls.lstNewPlanSummary.html('');
            controls.spnQuantityNewEquipment.text("0");
            controls.spnCableAditionalServiceQty.text("0");
            controls.spnInternetAditionalServiceQty.text("0");
            controls.spnPhoneAditionalServiceQty.text("0");
            controls.spnDecoAditionalServiceQty.text("0");

        },
        clearObjSave: function () {
            var that = this;
            var controls = that.getControls();
            that.objLteMigrationPlanValidation.lstServices = [];
            that.objLteMigrationPlanValidation.lstAllServicesByPlan = [];
            that.objLteMigrationPlanValidation.objAditionalServices = {};
            that.objLteMigrationPlanValidation.lstDecosByPlan = [];
            that.objLteMigrationPlanValidation.lstEquipmentCableNotDecos = [];
            that.objLteMigrationPlanValidation.lstCoreServices = [];
            that.objLteMigrationPlanValidation.lstCoreEquipment = [];
            that.objLteMigrationPlanValidation.lstAdditionalEquipmentQuantity = [];
            that.objLteMigrationPlanValidation.lstDecosCoreByPlan = [];
            that.objLteMigrationPlanValidation.OneCoreCable = 0;
            that.objLteMigrationPlanValidation.OneCoreInternet = 0;
            that.objLteMigrationPlanValidation.OneCorePhone = 0;
            that.objLteMigrationPlanValidation.objAditionalServices.lstCable = [];
            that.objLteMigrationPlanValidation.objAditionalServices.lstInternet = [];
            that.objLteMigrationPlanValidation.objAditionalServices.lstPhone = [];
            Session.objAditionalServices = that.objLteMigrationPlanValidation.objAditionalServices;
        },
        btnChooseCoreServicesByPlan_Pop: function () {
            var that = this,
                controls = that.getControls();
            var urlBase = window.location.href;
            urlBase = urlBase.substr(0, urlBase.lastIndexOf('/'));
            urlBase = location.protocol + '//' + location.host + '/Transactions/LTE/PlanMigration/LTEChooseCoreServicesByPlan';
            Session.idPlan = that.objLteMigrationPlanValidation.NewPlanId;

            var dialog = $.window.open({
                modal: true,
                title: "Seleccionar Servicios Core",
                url: urlBase,
                width: 1024,
                height: 600,
                buttons: {
                    Seleccionar: {
                        id: "btnSelectCore",
                        click: function (e) {
                            var ModalConfirm = this;
                            var objRowPostCableTotal = $('#tblChooseCoreServicesByPlanCable').DataTable().rows().data();
                            var objRowPostInternetTotal = $('#tblChooseCoreServicesByPlanInternet').DataTable().rows().data();
                            var objRowPostPhoneTotal = $('#tblChooseCoreServicesByPlanPhone').DataTable().rows().data();

                            var objRowPostCable = $('#tblChooseCoreServicesByPlanCable').DataTable().rows({ selected: true }).data();
                            var objRowPostInternet = $('#tblChooseCoreServicesByPlanInternet').DataTable().rows({ selected: true }).data();
                            var objRowPostPhone = $('#tblChooseCoreServicesByPlanPhone').DataTable().rows({ selected: true }).data();
                            if (objRowPostCableTotal.length > 0 && objRowPostCable.length == 0) {
                                alert(that.objLteMigrationPlanLoad.objLteMigrationPlanMessage.strFieldValidationSlt.replace("###", "un servicio core cable"), 'Informativo', function () { });
                                return false;
                            }
                            if (objRowPostInternetTotal.length > 0 && objRowPostInternet.length == 0) {

                                alert(that.objLteMigrationPlanLoad.objLteMigrationPlanMessage.strFieldValidationSlt.replace("###", "un servicio core internet"), 'Informativo', function () { });
                                return false;
                            }
                            if (objRowPostPhoneTotal.length > 0 && objRowPostPhone.length == 0) {
                                alert(that.objLteMigrationPlanLoad.objLteMigrationPlanMessage.strFieldValidationSlt
                                    .replace("###", "un servicio core telefono"),
                                    'Informativo',
                                    function() {});
                                return false;
                            } else {
                                Smmry.set('topeconsumo', 'No se ingresó');
                                 that.objLteMigrationPlanLoad.HaveTelephony = true;
                            }

                            if (objRowPostPhoneTotal.length == 0 && objRowPostPhone.length == 0) {
                                Smmry.set('topeconsumo', 'No aplica');
                                that.objLteMigrationPlanLoad.HaveTelephony = false;
                            }
                            

                            var strAlertMsg = that.objLteMigrationPlanLoad.objLteMigrationPlanMessage.strWantChargueCore;
                            if (that.objLteMigrationPlanLoad.intLTEValidateVel == 0) {
                                strAlertMsg += '<br /><span class="transac-message-red">' + that.objLteMigrationPlanLoad.objLteMigrationPlanMessage.strMessageValidateVelPlan + '</span>';
                            }
                            confirm(strAlertMsg, 'Confirmar', function (result) {
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
                                    
                                    that.objLteMigrationPlanValidation.idNewPlan = that.objLteMigrationPlanValidation.NewPlanId;
                                    controls.spnNewPlan.text(that.objLteMigrationPlanValidation.NewPlanName);
                                    controls.spnNewSolution.text(that.objLteMigrationPlanValidation.NewSolutionName);
                                    that.clearTables();
                                    that.clearObjSave();
                                    controls.btnAddServ.removeAttr('disabled');
                                    controls.btnAddEquip.removeAttr('disabled');
                                    controls.btnNextFirstStep.removeAttr('disabled');
                                    that.objLteMigrationPlanLoad.objTechnicalVisit = {};
                                    that.objLteMigrationPlanValidation.YouAreValidatingVisit = 0;
                                    that.getAdditionalAndCoreEquipment(objRowPostCable[0], objRowPostInternet[0], objRowPostPhone[0]);

                                    $.unblockUI();

                                    ModalConfirm.close();
                                }
                            });

                        }
                    },
                    Cancelar: {
                        click: function (sender, args) {
                            Session.idPlan = that.objLteMigrationPlanValidation.idNewPlan;
                            this.close();
                        }
                    }
                }
            });

        },
        loadNewCoreAndCoreAdditionalService: function (strService, obj, lst, lstSummary, objLst) {
            var that = this;
            var controls = that.getControls();
            var intCount = 0;

            if (obj != null && obj !== undefined) {
                lst.append('<li class="transac-list-group-item"><span class="badge">S/ ' + that.isnull(obj.CfWithIgv, "0") + '</span> ' + that.isnull(obj.DesServSisact, "") + '</li>');
                lstSummary.append('<li class="transac-list-group-item"><div class="row"><div class="col-sm-2">' + that.isnull(strService, "") + '</div><div class="col-sm-2">' + that.replaceServiceType(that.isnull(obj.ServiceType, "")) + '</div><div class="col-sm-6">' + that.isnull(obj.DesServSisact, "") + '</div><div class="col-sm-2"><span class="badge transac-badge-row">' + that.isnull(obj.CfWithIgv, "0") + '</span></div></div></li>');
                that.addAmountNewPlan("CORE", obj, "");
                that.objLteMigrationPlanValidation.lstCoreServices.push(obj);
                intCount++;
            }
            $.each(objLst, function (index, item) {
                lst.append('<li class="transac-list-group-item"><span class="badge">S/ ' + that.isnull(item.CfWithIgv, "0") + '</span> ' + that.isnull(item.DesServSisact, "") + '</li>');
                lstSummary.append('<li class="transac-list-group-item"><div class="row"><div class="col-sm-2">' + that.isnull(strService, "") + '</div><div class="col-sm-2">' + that.replaceServiceType(that.isnull(item.ServiceType, "")) + '</div><div class="col-sm-6">' + that.isnull(item.DesServSisact, "") + '</div><div class="col-sm-2"><span class="badge transac-badge-row">' + that.isnull(item.CfWithIgv, "0") + '</span></div></div></li>');
                that.addAmountNewPlan("CORE-ADICIONAL", item, "");
                intCount++;
                that.objLteMigrationPlanValidation.lstCoreServices.push(item);
            });
            if (intCount == 0) {
                lst.append('<li class="transac-list-group-item transac-message-red text-center"> No existen registros </li>');

            } else if (strService == "Teléfono")
                Session.ThereAreTelephone = 1;

        },
        loadAllCoreEquipment: function (objCable, objInternet, objTelephony, lstEquipment) {
            var that = this,
                controls = that.getControls();
            var intQuantityNewEquipment = parseInt(controls.spnQuantityNewEquipment.text());
            var intQuantityCable = 0, intQuantityInternet = 0, intQuantityTelephony = 0;
            $.each(lstEquipment, function (index, item) {
                if (objInternet != null && objInternet !== undefined)
                    if (item.CodServSisact == objInternet.CodServSisact && that.objLteMigrationPlanLoad.objLteMigrationPlanMessage.strLTEGroupInternet.indexOf(item.CodGroupServ) > -1) {
                    intQuantityInternet = intQuantityInternet + parseInt(that.isnull(item.CantEquipment, 0));
                    controls.lstSelectEquipInternet.append('<li class="transac-list-group-item"><span class="badge">' + item.CantEquipment + '</span> ' + item.Dscequ + '</li>');
                    }
                if (objTelephony != null && objTelephony !== undefined)
                    if (item.CodServSisact == objTelephony.CodServSisact && that.objLteMigrationPlanLoad.objLteMigrationPlanMessage.strLTEGroupTelephony.indexOf(item.CodGroupServ) > -1) {
                    intQuantityTelephony = intQuantityTelephony + parseInt(that.isnull(item.CantEquipment, 0));
                    controls.lstSelectEquipTelephony.append('<li class="transac-list-group-item"><span class="badge">' + item.CantEquipment + '</span> ' + item.Dscequ + '</li>');

                    }
                if (objCable != null && objCable !== undefined) {
                    if (item.CodServSisact == objCable.CodServSisact && that.objLteMigrationPlanLoad.objLteMigrationPlanMessage.strLTEGroupCable.indexOf(item.CodGroupServ) > -1) {
                    if (item.Codtipequ == that.objLteMigrationPlanLoad.objLteMigrationPlanMessage.strCodeHD) {
                        Session.objQuantityTypeDeco.DecoHDDefault++;
                    } else if (item.Codtipequ == that.objLteMigrationPlanLoad.objLteMigrationPlanMessage.strCodeSD) {
                        Session.objQuantityTypeDeco.DecoSDDefault++;
                    } else if (item.Codtipequ == that.objLteMigrationPlanLoad.objLteMigrationPlanMessage.strCodeDVR) {
                        Session.objQuantityTypeDeco.DecoDVRDefault++;
                    }
                    controls.lstSelectEquipCable.append('<li class="transac-list-group-item"><span class="badge">' + item.CantEquipment + '</span> ' + item.Dscequ + '</li>');
                    intQuantityCable = intQuantityCable + parseInt(that.isnull(item.CantEquipment, 0));
                    }
                }
                });
            
            if (intQuantityInternet == 0)
                controls.lstSelectEquipInternet.append('<li class="transac-list-group-item transac-message-red text-center">No existen registros</li>');
            if (intQuantityTelephony == 0)
                controls.lstSelectEquipTelephony.append('<li class="transac-list-group-item transac-message-red text-center">No existen registros</li>');
            if (intQuantityCable == 0)
                controls.lstSelectEquipCable.append('<li class="transac-list-group-item transac-message-red text-center">No existen registros</li>');
            intQuantityNewEquipment = intQuantityNewEquipment + intQuantityInternet + intQuantityCable + intQuantityTelephony;
            controls.spnQuantityNewEquipment.text(intQuantityNewEquipment);

        },
        loadNewAdditionalDeco: function () {
            var that = this,
                controls = that.getControls();
            if ((Session.objQuantityTypeDeco.DecoSD) > 0)
                controls.lstSelectEquipCable.append('<li id="SD" class="transac-list-group-item DecoQuantity"><span class="badge">' + Session.objQuantityTypeDeco.DecoSD + '</span> Adicional - SD</li>');
            if ((Session.objQuantityTypeDeco.DecoHD) > 0)
                controls.lstSelectEquipCable.append('<li id="HD" class="transac-list-group-item DecoQuantity"><span class="badge">' + Session.objQuantityTypeDeco.DecoHD + '</span> Adicional - HD</li>');
            if ((Session.objQuantityTypeDeco.DecoDVR) > 0)
                controls.lstSelectEquipCable.append('<li id="DVR" class="transac-list-group-item DecoQuantity"><span class="badge">' + Session.objQuantityTypeDeco.DecoDVR + '</span> Adicional - DVR</li>');
        },
        addAmountNewPlan: function (strType, objService, strCase) {
            var that = this,
                controls = that.getControls();

            if (objService != null && objService !== undefined) {
                var fltAmount = objService.CF;
        
                if (strType == "CORE") {
                    var fltFixedNewAmount = parseFloat(controls.spnNewBaseAmount.text().slice(3, controls.spnNewBaseAmount.text().length));
                    fltFixedNewAmount = fltFixedNewAmount + objService.CfWithIgv;//(parseFloat(fltAmount) * parseFloat(that.objLteMigrationPlanLoad.dblIgvView));
                    controls.spnNewBaseAmount.text('S/.' + fltFixedNewAmount.toFixed(2));
                } else {
                    var ma = $("#spnAdditionalNewAmount").text();
                    var amountA = parseFloat(ma.slice(3, ma.length));
                    amountA = amountA + (parseFloat(fltAmount) * parseFloat(that.objLteMigrationPlanLoad.dblIgvView));
                    $("#spnAdditionalNewAmount").text('S/.' + amountA.toFixed(2));

                }
                var fltNewTotalFixedChargeSIGV = parseFloat(controls.spnNewTotalFixedChargeSIGV.text().slice(3, controls.spnNewBaseAmount.text().length));
                fltNewTotalFixedChargeSIGV = fltNewTotalFixedChargeSIGV + parseFloat(fltAmount);
                controls.spnNewTotalFixedChargeSIGV.text('S/.' + fltNewTotalFixedChargeSIGV.toFixed(2));

                var fltNewAmountFixedChargeCIGV = parseFloat(fltAmount) * parseFloat(that.objLteMigrationPlanLoad.dblIgvView);
                fltNewAmountFixedChargeCIGV = fltNewAmountFixedChargeCIGV.toFixed(2);
                fltNewAmountFixedChargeCIGV = parseFloat(fltNewAmountFixedChargeCIGV);

                var fltNewTotalFixedChargeCIGV = parseFloat(controls.spnNewTotalFixedChargeCIGV.text().slice(3, controls.spnNewBaseAmount.text().length));
                fltNewTotalFixedChargeCIGV = fltNewTotalFixedChargeCIGV + fltNewAmountFixedChargeCIGV;
                controls.spnNewTotalFixedChargeCIGV.text('S/.' + fltNewTotalFixedChargeCIGV.toFixed(2));

                //if (strCase != "Deco") {
                var intQuantityServices = parseInt(controls.spnQuantityNewServices.text());
                intQuantityServices = intQuantityServices + 1;
                controls.spnQuantityNewServices.text(intQuantityServices);
            //}
            }
        },
        isnull: function (object, defaultValue) {
            if (object == null) 
                return defaultValue;
            return object;
        },
        addAdditionalService: function (type) {
            var total = parseInt($("#spn" + type + "AditionalServiceQty").text());
            total = total + 1;
            $("#spn" + type + "AditionalServiceQty").text(total);
               
        },
        subtractAmounts: function (type, amount) {
            var that = this,
                controls = that.getControls();
            if (type == "CORE") {
                var m = $("#spnNewBaseAmount").text();
                var amountF = parseFloat(m.slice(3, m.length));
                amountF = amountF - parseFloat(amount);
                $("#spnNewBaseAmount").text('S/.' + amountF.toFixed(2));
            }
            else {
                var ma = $("#spnAdditionalNewAmount").text();
                var amountA = parseFloat(ma.slice(3, ma.length));
                amountA = amountA - (parseFloat(amount) * parseFloat(that.objLteMigrationPlanLoad.dblIgvView));
                $("#spnAdditionalNewAmount").text('S/.' + amountA.toFixed(2));

            }
            var msi = $("#spnNewTotalFixedChargeSIGV").text();
            var amountSIGV = parseFloat(msi.slice(3, msi.length));
            amountSIGV = amountSIGV - parseFloat(amount);
            $("#spnNewTotalFixedChargeSIGV").text('S/.' + amountSIGV.toFixed(2));
            var amountCIGV = parseFloat(amount) * parseFloat(that.objLteMigrationPlanLoad.dblIgvView);
            amountCIGV = amountCIGV.toFixed(2);
            amountCIGV = parseFloat(amountCIGV);
            var mci = $("#spnNewTotalFixedChargeCIGV").text();
            var totalAmountCIGV = parseFloat(mci.slice(3, mci.length));
            totalAmountCIGV = totalAmountCIGV - amountCIGV;
            $("#spnNewTotalFixedChargeCIGV").text('S/.' + totalAmountCIGV.toFixed(2));

            var quantity = parseInt($("#spnQuantityNewServices").text());
            quantity = quantity - 1;
            $("#spnQuantityNewServices").text(quantity);

            if (type == "Deco") {
                var intQuantity = parseInt(controls.spnQuantityNewEquipment.text()) - 1;
                controls.spnQuantityNewEquipment.text(intQuantity);
            }
            
        },
        removeAditionalService: function (type) {
            var total = parseInt($("#spn" + type + "AditionalServiceQty").text());
            total = total - 1;
            $("#spn" + type + "AditionalServiceQty").text(total);
        },
        getTechnicalVisitResult: function () {
            var that = this,
                controls = that.getControls(),
                objRequestData = {};
            objRequestData.strIdSession = Session.IDSESSION;
            objRequestData.strCodeUser = Session.USERACCESS.login;
            objRequestData.strIdContract = Session.DATACUSTOMER.ContractID;
            objRequestData.strStateLine = Session.DATASERVICE.StateLine;
            objRequestData.strIdPlan = Session.IdPlan;
            objRequestData.strCustomerId = Session.DATACUSTOMER.CustomerID;
            objRequestData.strCodPlanSisact = that.objLteMigrationPlanValidation.lstCoreServices[0].CodPlanSisact;
            objRequestData.strTmCode = that.objLteMigrationPlanValidation.lstCoreServices[0].Tmcode;
            objRequestData.lstEquipmentVisitCore = that.objLteMigrationPlanValidation.lstEquipmentVisitCore;
            objRequestData.lstAdditionalEquipmentQuantity = that.objLteMigrationPlanValidation.lstAdditionalEquipmentQuantity;

            $.blockUI({
                message: '<div align="center"><img src="' + that.strUrlLogo + '" width="25" height="25" /> Validando ... </div>',
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

            that.objLteMigrationPlanLoad.objTechnicalVisit.Avanotacion = '';

            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objRequestData),
                async: false,
                url: '/Transactions/LTE/PlanMigration/GetTechnicalVisitResult',
                success: function (response) {
                        that.objLteMigrationPlanLoad.objTechnicalVisit = {};
                        that.objLteMigrationPlanLoad.objTechnicalVisit.Response = false;
                    if (response.data != null) {
                        if (response.data.Anerror < 0) {
                                if (response.data.Flag == "1") {
                                    if (response.data.Codmot == null || response.data.Codmot=="null")
                                        that.objLteMigrationPlanLoad.objTechnicalVisit.Codmot = that.objLteMigrationPlanLoad.objLteMigrationPlanMessage.strCodeTechnicalVisit;
                                    else 
                                that.objLteMigrationPlanLoad.objTechnicalVisit.Codmot = response.data.Codmot;
                            }
                                 else 
                                    that.objLteMigrationPlanLoad.objTechnicalVisit.Codmot = that.objLteMigrationPlanLoad.objLteMigrationPlanMessage.strCodeTechnicalVisit;
                                that.objLteMigrationPlanValidation.YouAreValidatingVisit = 0;
                                alert(that.objLteMigrationPlanLoad.objLteMigrationPlanMessage.strErrorValidating.replace("###", "visita técnica"), "Alerta");
                        } else {
                            that.objLteMigrationPlanValidation.YouAreValidatingVisit = 1;
                            that.objLteMigrationPlanLoad.objTechnicalVisit.SubType = response.data.Subtipo;
                                        if (response.data.Codmot != null) {
                                            if (response.data.Codmot != "" && response.data.Codmot != "null")
                                                that.objLteMigrationPlanLoad.objTechnicalVisit.Codmot = response.data.Codmot; 
                                            else
                                                that.objLteMigrationPlanLoad.objTechnicalVisit.Codmot = that.objLteMigrationPlanLoad.objLteMigrationPlanMessage.strCodeTechnicalVisit;
                                          }
                                if (response.data.Flag == "1") {
                                    that.objLteMigrationPlanLoad.objTechnicalVisit.MigrationScenary ="Con visita"; 
                                    that.objLteMigrationPlanLoad.objTechnicalVisit.TechnicalVisit = true; 
                                    that.objLteMigrationPlanLoad.objTechnicalVisit.Avanotacion = response.data.Anotaciones;
                                                that.getWorkType(that.objLteMigrationPlanLoad.strIdIdentifyTypeWork);
                                                alert(that.objLteMigrationPlanLoad.objLteMigrationPlanMessage.strYesTechnicalVisit.replace("###", "visita técnica"),"Informativo");
                                } else {
                                                that.objLteMigrationPlanLoad.objTechnicalVisit.MigrationScenary = "Sin visita";
                                    that.objLteMigrationPlanLoad.objTechnicalVisit.TechnicalVisit =  false; 
                                                alert(that.objLteMigrationPlanLoad.objLteMigrationPlanMessage.strNotTechnicalVisit.replace("###", "visita técnica"),"Informativo");
                            }
                            }
                    } else {
                        that.objLteMigrationPlanLoad.objTechnicalVisit.Codmot = that.objLteMigrationPlanLoad.objLteMigrationPlanMessage.strCodeTechnicalVisit;
                        that.objLteMigrationPlanValidation.YouAreValidatingVisit = 0;
                        alert("Hubo un error en la validación","Informativo");
                    }
                    $.unblockUI();
                },
                failure: function (msg) {
                    
                    that.objLteMigrationPlanLoad.objTechnicalVisit.Codmot = that.objLteMigrationPlanLoad.objLteMigrationPlanMessage.strCodeTechnicalVisit;
                    that.objLteMigrationPlanValidation.YouAreValidatingVisit = 0;
                    alert("Hubo un error en la validación", "Informativo");
                    $.unblockUI();
                }
            });
           
        },
        f_GetValidateETA: function () {
            var that = this,
            controls = that.getControls(), model = {};
            var strTypeWork = $("#ddlTipoTrabajo").val();
            model.IdSession = Session.IDSESSION;
            model.strJobTypes = strTypeWork;
            model.StrCodeUbigeo = Session.idUbigeo;

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
                    if (oItem.Code == '1' || oItem.Code == '0' || oItem.Code == '2') {
                        Session.ValidateETA = oItem.Code;
                        Session.History = oItem.Code2;
                        if (oItem.Code == '2') {
                            that.getWorkSubType(strTypeWork);
                            that.f_ActDesactivaCamposAgendamiento();
                        }
                        if (oItem.Code == '1') {
                            that.getWorkSubType(strTypeWork);
                            that.f_ActDesactivaCamposAgendamiento();
                        }
                        else {
                            controls.ddlTypeSubWork.prop('disabled', true);
                            that.f_ActDesactivaCamposAgendamiento();
                            alert("No aplica agendamiento en línea, favor de continuar con la operación.", "Alerta");
                        }

                    } else {
                        Session.ValidateETA = "0";
                        Session.History = oItem.Code2;
                        controls.ddlTypeSubWork.prop('disabled', true);
                        that.f_ActDesactivaCamposAgendamiento();
                        alert("Error al realizar validacion ETA", "Alerta");
                    }
                }

            });

        },
        f_ActDesactivaCamposAgendamiento: function () {
            var that = this,
               controls = that.getControls();
            var fechaServidor = new Date(Session.FECHAACTUALSERVIDOR);
            if (Session.ValidateETA != "0") {
                controls.txtFProgramacion.prop("disabled", false);
                controls.txtFProgramacion.datepicker({ format: 'dd/mm/yyyy' });
                controls.ddlFranjaHoraria.prop("disabled", false);
                controls.ddlTypeSubWork.prop("disabled", false);
                $("#btnValidarHorario").prop("disabled", true);

            }
            else {
                controls.txtFProgramacion.val([that.pad(fechaServidor.getDate()), that.pad(fechaServidor.getMonth() + 1), fechaServidor.getFullYear()].join("/"));
                controls.txtFProgramacion.datepicker({ format: 'dd/mm/yyyy' });
                controls.txtFProgramacion.prop("disabled", true);
                controls.ddlFranjaHoraria.prop("disabled", true);
                controls.ddlTypeSubWork.prop("disabled", true);
                $("#btnValidarHorario").prop("disabled", true);

            }
        },
        pad: function (s) { return (s < 10) ? '0' + s : s; },
        f_cargarSubtipoTrabajo: function (gConstTipTra) {
            var that = this;
            that.getWorkSubType(gConstTipTra);
        },
        getWorkSubType: function (pstrWorkType) {

            var that = this;
            var strIdSession = Session.IDSESSION;
            var strContractID = SessionTransac.SessionParams.DATACUSTOMER.ContractID;

            var urlBase = '/Transactions/LTE/PlanMigration';
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
            var i = 0;
            controls.ddlTypeSubWork.empty();
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

            var urlBase = '/Transactions/LTE/PlanMigration';

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: "{strIdSession:'" + strIdSession + "',strTransacType:'" + strTransacType + "'}",
                url: urlBase + '/GetWorkType',
                success: function (response) {
                    that.createDropdownWorkType(response, strTransacType);
                },
                complete: function () {
                    that.f_GetValidateETA();
                }
            });
        },
        createDropdownWorkType: function (response, strTransacType) {

            var that = this,
                controls = that.getControls();
            var i = 1;

            if (response.data != null) {
                controls.ddlTypeWork.empty();
                $.each(response.data.ListGeneric, function (index, value) {
                    controls.ddlTypeWork.append($('<option>', { value: value.Code, html: value.Description }));
                });
            }
            if (strTransacType == '4')
                that.f_GetValidateETA(strTransacType);
        },
        getAdditionalAndCoreEquipment: function (objRowPostCable, objRowPostInternet, objRowPostPhone) {
            var that = this,
                controls = that.getControls(),
                objRequestData = {};
            objRequestData.strIdSession = Session.IDSESSION;
            objRequestData.strIdContract = Session.DATACUSTOMER.ContractID;
            objRequestData.strTypeProduct = Session.ProductType;
            objRequestData.strIdPlan = Session.idPlan;
            objRequestData.strIgv = Session.dblIgvView;
            if (typeof objRowPostCable != 'undefined')
                objRequestData.strIdCable = objRowPostCable.CodServSisact;
            if (typeof objRowPostInternet != 'undefined')
                objRequestData.strIdInternet = objRowPostInternet.CodServSisact;
            if (typeof objRowPostPhone != 'undefined')
                objRequestData.strIdPhone = objRowPostPhone.CodServSisact;
            Session.objQuantityTypeDeco = {};
            Session.objQuantityTypeDeco.DecoSDDefault = 0;
            Session.objQuantityTypeDeco.DecoHDDefault = 0;
            Session.objQuantityTypeDeco.DecoDVRDefault = 0;
            Session.objQuantityTypeDeco.DecoSD = 0;
            Session.objQuantityTypeDeco.DecoHD = 0;
            Session.objQuantityTypeDeco.DecoDVR = 0;
            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objRequestData),
                url: '/Transactions/LTE/PlanMigration/GetAdditionalAndCoreEquipment',
                async: false,
                success: function (response) {
                    if (response.data != null) {
                        var obj = response.data;
                        if (obj.lstEquipmentTotalCore == null)
                            obj.lstEquipmentTotalCore = [];
                        that.objLteMigrationPlanValidation.lstEquipmentTotalCore = obj.lstEquipmentTotalCore;

                        if (obj.lstEquipmentVisitCore == null)//nuevoY
                            obj.lstEquipmentVisitCore = [];
                        that.objLteMigrationPlanValidation.lstEquipmentVisitCore = obj.lstEquipmentVisitCore;

                        if (obj.lstEquipmentCableAllAdditional == null)//nuevoY
                            obj.lstEquipmentCableAllAdditional = [];
                        that.objLteMigrationPlanValidation.lstEquipmentCableAllAdditional = obj.lstEquipmentCableAllAdditional;

                        if (obj.lstEquipmentDecoCableAdditional == null)//nuevoY
                            obj.lstEquipmentDecoCableAdditional = [];
                        that.objLteMigrationPlanValidation.lstEquipmentDecoCableAdditional = obj.lstEquipmentDecoCableAdditional;

                        for (var i in that.objLteMigrationPlanValidation.lstEquipmentCableAllAdditional) {

                            if (that.objLteMigrationPlanValidation.lstEquipmentCableAllAdditional[i].Codtipequ !==
                                that.objLteMigrationPlanLoad.objLteMigrationPlanMessage.strCodeSD &&
                                that.objLteMigrationPlanValidation.lstEquipmentCableAllAdditional[i].Codtipequ !==
                                that.objLteMigrationPlanLoad.objLteMigrationPlanMessage.strCodeHD &&
                                that.objLteMigrationPlanValidation.lstEquipmentCableAllAdditional[i].Codtipequ !==
                                that.objLteMigrationPlanLoad.objLteMigrationPlanMessage.strCodeDVR) 
                            {
                                that.objLteMigrationPlanValidation.lstEquipmentCableAllAdditional[i].CF = "0";
                                that.objLteMigrationPlanValidation.lstEquipmentCableAllAdditional[i].CfWithIgv = "0";
                            }

                        }

                        if (obj.lstServicesByPlanCableCoreAddi == null)//nuevoY
                            obj.lstServicesByPlanCableCoreAddi = [];
                        that.objLteMigrationPlanValidation.lstServicesByPlanCableCoreAddi = obj.lstServicesByPlanCableCoreAddi;

                        if (obj.lstServicesByPlanInternetCoreAddi == null)//nuevoY
                            obj.lstServicesByPlanInternetCoreAddi = [];
                        that.objLteMigrationPlanValidation.lstServicesByPlanInternetCoreAddi = obj.lstServicesByPlanInternetCoreAddi;

                        if (obj.lstServicesByPlanTelephoneCoreAddi == null)//nuevoY
                            obj.lstServicesByPlanTelephoneCoreAddi = [];
                        that.objLteMigrationPlanValidation.lstServicesByPlanTelephoneCoreAddi = obj.lstServicesByPlanTelephoneCoreAddi;

                        Session.ThereAreTelephone = 0;
                        that.loadNewCoreAndCoreAdditionalService("Cable", objRowPostCable, controls.lstSelectPlanCable, controls.lstNewPlanSummary, obj.lstServicesByPlanCableCoreAddi);
                        that.loadNewCoreAndCoreAdditionalService("Internet", objRowPostInternet, controls.lstSelectPlanInternet, controls.lstNewPlanSummary, obj.lstServicesByPlanInternetCoreAddi);
                        that.loadNewCoreAndCoreAdditionalService("Teléfono", objRowPostPhone, controls.lstSelectPlanTelephony, controls.lstNewPlanSummary, obj.lstServicesByPlanTelephoneCoreAddi);
                        that.loadAllCoreEquipment(objRowPostCable, objRowPostInternet, objRowPostPhone,that.objLteMigrationPlanValidation.lstEquipmentTotalCore);

                        for (var i in that.objLteMigrationPlanValidation.lstEquipmentTotalCore) {
                            that.objLteMigrationPlanValidation.lstEquipmentTotalCore[i].CF = "0";
                            that.objLteMigrationPlanValidation.lstEquipmentTotalCore[i].CfWithIgv = "0";
                            that.objLteMigrationPlanValidation.lstEquipmentTotalCore[i].CodGroupServ = that.objLteMigrationPlanValidation.lstEquipmentTotalCore[i].CodEquipmentGroup;
                        }

                    }

                 
                }

            });
        },
        assignAdditionalEquipment: function (lstEquipmentDecoCableAdditional) {
            var that = this;
            var intQuantitySD = 0, intQuantityHD = 0, intQuantityDVR = 0;
            $.each(that.objLteMigrationPlanValidation.lstAdditionalEquipmentQuantity, function (index, item) {
                that.subtractAmounts("Deco", that.isnull(item.CF, 0));

            });
            that.objLteMigrationPlanValidation.lstAdditionalEquipmentQuantity = [];

            $.each(lstEquipmentDecoCableAdditional, function (index, value) {
                if (value.Codtipequ == that.objLteMigrationPlanLoad.objLteMigrationPlanMessage.strCodeSD) {
                    intQuantitySD++;
                    that.addAdditionalEquipmentLst(intQuantitySD, parseInt(Session.objQuantityTypeDeco.DecoSD), value);
                }
                else if (value.Codtipequ == that.objLteMigrationPlanLoad.objLteMigrationPlanMessage.strCodeHD) {
                    intQuantityHD++;
                    that.addAdditionalEquipmentLst(intQuantityHD, parseInt(Session.objQuantityTypeDeco.DecoHD), value);
                }
                else if (value.Codtipequ == that.objLteMigrationPlanLoad.objLteMigrationPlanMessage.strCodeDVR) {
                    intQuantityDVR++;
                    that.addAdditionalEquipmentLst(intQuantityDVR, parseInt(Session.objQuantityTypeDeco.DecoDVR), value);
                }
            });

        },
        addAdditionalEquipmentLst: function (intQuantity, intNewQuantity, object) {
            var that = this;
            if (intQuantity <= intNewQuantity)
                that.objLteMigrationPlanValidation.lstAdditionalEquipmentQuantity.push(object);
        },
        validationAdditionalEquipment: function (lstEquipmentDecoCableAdditional) {

            var that = this;

            var intQuantitySD = 0, intQuantityHD = 0, intQuantityDVR = 0;
            $.each(lstEquipmentDecoCableAdditional, function (index, value) {
                if (value.Codtipequ == that.objLteMigrationPlanLoad.objLteMigrationPlanMessage.strCodeSD) {
                    intQuantitySD++;
                }
                else if (value.Codtipequ == that.objLteMigrationPlanLoad.objLteMigrationPlanMessage.strCodeHD) {
                    intQuantityHD++;
                }
                else if (value.Codtipequ == that.objLteMigrationPlanLoad.objLteMigrationPlanMessage.strCodeDVR) {
                    intQuantityDVR++;
                }
            });
            if (intQuantitySD < parseInt(Session.objQuantityTypeDeco.DecoSD))
                return "SD";
            else if (intQuantityHD < parseInt(Session.objQuantityTypeDeco.DecoHD))
                return "HD";
            else if (intQuantityDVR < parseInt(Session.objQuantityTypeDeco.DecoDVR))
                return "DVR";
            return "";

        },
        refreshSessionAditionalService: function (type, obj, description) {
            var that = this;
            if (type === "Cable") {
                that.objLteMigrationPlanValidation.objAditionalServices.lstCable = obj;
            }

            else if (type === "Internet") {
                that.objLteMigrationPlanValidation.objAditionalServices.lstInternet = obj;
            }

            else if (type === "Phone") {
                that.objLteMigrationPlanValidation.objAditionalServices.lstPhone = obj;
            }
            else if (type === "Deco") {
                that.objLteMigrationPlanValidation.lstAdditionalEquipmentQuantity = obj;
                if (description == that.objLteMigrationPlanLoad.objLteMigrationPlanMessage.strCodeSD) {
                    Session.objQuantityTypeDeco.DecoSD = parseInt(Session.objQuantityTypeDeco.DecoSD) - 1;
                }
                else if (description == that.objLteMigrationPlanLoad.objLteMigrationPlanMessage.strCodeDVR) {
                    Session.objQuantityTypeDeco.DecoDVR = parseInt(Session.objQuantityTypeDeco.DecoDVR) - 1;
                }
                else if (description == that.objLteMigrationPlanLoad.objLteMigrationPlanMessage.strCodeHD) {
                    Session.objQuantityTypeDeco.DecoHD = parseInt(Session.objQuantityTypeDeco.DecoHD) - 1;
                }
            }
            Session.objAditionalServices = that.objLteMigrationPlanValidation.objAditionalServices;
        },
        sumDayToDate: function () {
            var that = this;
            var dateNow = new Date();
            var intDays = parseInt(that.objLteMigrationPlanLoad.objLteMigrationPlanMessage.strDayInstallation);
            dateNow.setDate(dateNow.getDate() + intDays);
            return dateNow;

        },
        onlyNumbers_keypress: function (e) {
            if (e.key.match(/[^0-9]/g)) {
                e.preventDefault();
            }
        },
        createLstConstancyPDF: function () {
            var that = this,
                controls = that.getControls();
            var lstConstancy = [];
            var title = that.objLteMigrationPlanLoad.objLteMigrationPlanMessage.strTittleConstancy;
            lstConstancy.push(that.objLteMigrationPlanLoad.objLteMigrationPlanMessage.strNameFormatLte),
            lstConstancy.push(title),
            lstConstancy.push($("#sltCacDac option:selected").text());
            lstConstancy.push(Session.DATACUSTOMER.LegalAgent);
            lstConstancy.push(Session.DATACUSTOMER.FullName);
            lstConstancy.push(Session.DATACUSTOMER.DocumentType);
            lstConstancy.push(Session.DATASERVICE.Plan);
            lstConstancy.push(Session.DATACUSTOMER.BillingCycle);
            lstConstancy.push(moment(new Date()).format('DD/MM/YYYY'));
            lstConstancy.push("{interaccion}");
            lstConstancy.push(Session.DATACUSTOMER.ContractID); 
            lstConstancy.push(Session.DATACUSTOMER.DocumentNumber);
            lstConstancy.push(controls.spnNewPlan.text());
            lstConstancy.push(controls.spnNewSolution.text());
            lstConstancy.push(controls.spnCurrentTotalFixedChargeCIGV.text());
            lstConstancy.push($("#txtFProgramacion").val());
            lstConstancy.push(controls.txtTotalPenality.val());
            lstConstancy.push("{nrosot}");
            lstConstancy.push(that.objLteMigrationPlanLoad.objLteMigrationPlanMessage.strTextConstancy);
            lstConstancy.push(controls.chkPreSuscribed.prop("checked") ? "1" : "0");
            lstConstancy.push(controls.txtLetterNumber.val());
            lstConstancy.push($("#sltOperator option:selected").text());
            lstConstancy.push(controls.chkPublish.prop("checked") ? "1" : "0");
            lstConstancy.push("");
            lstConstancy.push("");
            lstConstancy.push(controls.spnNewTotalFixedChargeCIGV.text());
            lstConstancy.push(title.substring(title.indexOf('-') + 1));
            lstConstancy.push(Session.USERACCESS.login);
            lstConstancy.push(Session.USERACCESS.fullName);
            return lstConstancy;
        },
        validateTopeAmount: function(value) {
            var that = this;
            var arrayTypeTope = that.objLteMigrationPlanLoad.objLteMigrationPlanMessage.strValueTopeConsumo.split(",");
            return arrayTypeTope[value - 1];
        },
        btnSave_Click: function () {
            var that = this;
            var controls = that.getControls(),
                objSaveData = {};
            objSaveData.strCacDac = $("#sltCacDac option:selected").text();
            objSaveData.strNoLetter = controls.txtLetterNumber.val();
            objSaveData.strRefound = controls.txtRefound.val();
            objSaveData.strLoyalityAmount = controls.txtLoyaltyAmount.val();
            objSaveData.strTotalPenalty = controls.txtTotalPenality.val();
            objSaveData.spnNewTotalFixedChargeCIGV = controls.spnNewTotalFixedChargeCIGV.text();
            objSaveData.strFProgrammming = $("#txtFProgramacion").val(); 
            objSaveData.strEmail = controls.txtEmail.val();
            if (controls.chkPreSuscribed.is(":checked"))
                objSaveData.strPresuscritoStatus = 1;
            else
                objSaveData.strPresuscritoStatus = 0;

            if (!controls.sltOperator.is(":disabled")) {
                objSaveData.strDdlOperatorStatus = $("#sltOperator option:selected").val();
                objSaveData.strDdlOperator = $("#sltOperator option:selected").text();
            } else {
                objSaveData.strDdlOperatorStatus = "00"; 
                objSaveData.strDdlOperator = "AMERICA MOVIL DEL PERU SAC"; 
            }
            if (controls.chkPublish.is(":checked"))
                objSaveData.strPublishFinalStatus = 1;
            else
                objSaveData.strPublishFinalStatus = 0;

            if (controls.chkLoyalty.is(":checked"))
                objSaveData.strFinalLoyalityStatus = 1;
            else
                objSaveData.strFinalLoyalityStatus = 0;

            if (controls.chkOCC.is(":checked"))
                objSaveData.strOCCFinalStatus = 1;
            else
                objSaveData.strOCCFinalStatus = 0;
            objSaveData.lstPDFConstancyParameters = that.createLstConstancyPDF();
            that.objLteMigrationPlanValidation.lstAllServicesByPlan = that.objLteMigrationPlanValidation.lstCoreServices;
            if (that.objLteMigrationPlanValidation.objAditionalServices.lstCable.length>0)
                that.objLteMigrationPlanValidation.lstAllServicesByPlan = that.objLteMigrationPlanValidation.lstAllServicesByPlan.concat(that.objLteMigrationPlanValidation.objAditionalServices.lstCable);
            if (that.objLteMigrationPlanValidation.objAditionalServices.lstInternet.length > 0)
                that.objLteMigrationPlanValidation.lstAllServicesByPlan = that.objLteMigrationPlanValidation.lstAllServicesByPlan.concat(that.objLteMigrationPlanValidation.objAditionalServices.lstInternet);
            if (that.objLteMigrationPlanValidation.objAditionalServices.lstPhone.length > 0)
                that.objLteMigrationPlanValidation.lstAllServicesByPlan = that.objLteMigrationPlanValidation.lstAllServicesByPlan.concat(that.objLteMigrationPlanValidation.objAditionalServices.lstPhone);

            for (var i in that.objLteMigrationPlanValidation.lstAllServicesByPlan) {
                that.objLteMigrationPlanValidation.lstAllServicesByPlan[i].Codtipequ = "";
                that.objLteMigrationPlanValidation.lstAllServicesByPlan[i].Tipequ = "";
                that.objLteMigrationPlanValidation.lstAllServicesByPlan[i].Dscequ = "";
            }

            objSaveData.objItemTypification = that.objLteMigrationPlanLoad.objTypification;
            if (that.objLteMigrationPlanValidation.lstAllServicesByPlan.length == 0) {
                alert("Necesita escoger un nuevo plan.", "Alerta");
                return;
            }
            objSaveData.strIdCustomer = Session.DATACUSTOMER.CustomerID;
            objSaveData.strLogin = Session.USERACCESS.login;
            objSaveData.strNotes = 'Plan: ' + that.isnull(that.objLteMigrationPlanValidation.NewPlanName, " ") + '/ Teléfono de Referencia: ' + that.isnull(Session.DATACUSTOMER.PhoneReference, " ") + ' / ' + that.isnull(that.objLteMigrationPlanLoad.objTechnicalVisit.Avanotacion, " ") + ' ' + that.isnull(controls.txtNotes.val(), " ");
            objSaveData.strIdContract = Session.DATACUSTOMER.ContractID;
            objSaveData.strBillingCycle = Session.DATACUSTOMER.objPostDataAccount.BillingCycle;
            objSaveData.strActivationDate = Session.DATACUSTOMER.ActivationDate;
            objSaveData.strTermContract = Session.DATASERVICE.TermContract;
            objSaveData.strStateLine = Session.DATASERVICE.StateLine;
            objSaveData.strExpirationDate = Session.DATACUSTOMER.objPostDataAccount.ExpirationDate;
            objSaveData.strOfficeAddress = Session.DATACUSTOMER.OfficeAddress;
            objSaveData.strLegalAddress = Session.DATACUSTOMER.LegalDepartament;
            objSaveData.strLegalDistrict = Session.DATACUSTOMER.LegalDistrict;
            objSaveData.strLegalCountry = Session.DATACUSTOMER.LegalCountry;
            objSaveData.strLegalProvince = Session.DATACUSTOMER.LegalProvince;
            objSaveData.strPlaneCodeInstallation = Session.idUbigeo;
            objSaveData.strPlan = Session.DATASERVICE.Plan;
            objSaveData.strCodPlan = Session.DATACUSTOMER.PlaneCodeInstallation;
            objSaveData.strLegalUrbanization = Session.DATACUSTOMER.LegalUrbanization;
            objSaveData.strDocumentNumber = Session.DATACUSTOMER.DocumentNumber;
            objSaveData.strFullName = Session.DATACUSTOMER.FullName;
            objSaveData.strLegalAgent = Session.DATACUSTOMER.LegalAgent;
            objSaveData.strDocumentType = Session.DATACUSTOMER.DocumentType;
            objSaveData.strValidateETAStatus = 0; 
            objSaveData.strCustomerContact = Session.DATACUSTOMER.BusinessName;
            objSaveData.strIdContract = Session.DATACUSTOMER.ContractID;
            objSaveData.strExistCorePhoneService = Session.ThereAreTelephone;
            objSaveData.strCustomerType = Session.DATACUSTOMER.CustomerType;
            objSaveData.strIdCustomer = Session.DATACUSTOMER.CustomerID;
            objSaveData.strTmCode = that.objLteMigrationPlanValidation.lstCoreServices[0].Tmcode; 
            objSaveData.strHdnCodPlan = Session.idPlan; 
            objSaveData.strCodMoTot = that.objLteMigrationPlanLoad.objTechnicalVisit.Codmot;
            objSaveData.strConstanceXml = "";
            objSaveData.strProductType = Session.UrlParams.SUREDIRECT;
            objSaveData.strHdnListFTMCode = Session.DATASERVICE.CodePlanTariff;
            objSaveData.strHdnRequestActId = Session.RequestActId;
            objSaveData.strIdSession = Session.IDSESSION;

            objSaveData.lstServices = that.objLteMigrationPlanValidation.lstAllServicesByPlan;
            objSaveData.lstEquipmentTotalCore = that.objLteMigrationPlanValidation.lstEquipmentTotalCore;//PONER
            objSaveData.lstAdditionalEquipmentQuantity = that.objLteMigrationPlanValidation.lstAdditionalEquipmentQuantity;
            objSaveData.lstEquipmentCableAllAdditional = that.objLteMigrationPlanValidation.lstEquipmentCableAllAdditional;

            objSaveData.dblCreditLimit = Session.DATACUSTOMER.objPostDataAccount.CreditLimit;
            objSaveData.strAnotation = that.objLteMigrationPlanLoad.objTechnicalVisit.Avanotacion;
            objSaveData.strName = Session.DATACUSTOMER.Name;
            objSaveData.strLastname = Session.DATACUSTOMER.LastName;
            objSaveData.dblConsumeCap = 0;
            objSaveData.dblConsumeCapAmount = 0;
            objSaveData.strNewPlan = controls.spnNewPlan.text();
            objSaveData.strNewSolution = controls.spnNewSolution.text();
            objSaveData.strAddressReference = Session.DATACUSTOMER.Reference;

            if (controls.chkEmail.is(":checked"))
                objSaveData.bolSendEmail = 1;
            else
                objSaveData.bolSendEmail = 0;
            
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
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                data: JSON.stringify(objSaveData),
                url: "/Transactions/LTE/PlanMigration/SaveMigratedPlan",
                error: function (xhr, status, error) {
                    $.unblockUI();
                },
                success: function (data) {
                    if (data.data == null) {
                        that.objLteMigrationPlanValidation.RouteRecordPDF = "";
                        alert("Error en la conectividad", "Alerta");

                    } else {
                        that.objLteMigrationPlanValidation.RouteRecordPDF = data.data.ConstancyRoute;
                        if (data.data.Code < 0)
                            alert(data.data.result, "Alerta");
                        else if (data.data.Code == 0) {
                            controls.btnConstancy.prop("disabled", false);
                            alert('<span>' +
                                data.data.result +
                                '.<br />La SOT generada es: ' +
                                data.data.SotNumber +
                                '</span><br /><span> El código de interaccion es: ' +
                                data.data.InteractionCode +
                                '</span>',
                                'Mensaje');
                        }
                        else
                            alert(data.data.result, "Informativo");
                    }
                    $.unblockUI();
                    controls.btnSave.prop("disabled", true);

                }
            });
        },
        btnConstancy_click: function () {
            var that = this,
                controls = that.getControls();
            if (that.objLteMigrationPlanValidation.RouteRecordPDF != '') {
                var newRoute = that.objLteMigrationPlanValidation.RouteRecordPDF.substring(that.objLteMigrationPlanValidation.RouteRecordPDF.indexOf('SIACUNICO'));
                newRoute = newRoute.replace(new RegExp('/', 'g'), '\\');
                newRoute = that.objLteMigrationPlanLoad.objLteMigrationPlanMessage.strServerPDF + newRoute;
                ReadRecordSharedFile(Session.IDSESSION, newRoute);
                Session.PdfRoute = newRoute;
            } else {
                alert('No se ha cargado correctamente el archivo de la constancia.', "Alerta");
            }

        },
        closeWindow:function() {
            parent.window.close();
        },
        replaceServiceType: function(strTypeService) {
            if (strTypeService.toUpperCase() != "ADICIONAL")
                return "Incluído";
            else return "Adicional";
        },
        strUrlLogo: window.location.protocol + '//' + window.location.host + '/Images/loading_3.gif',
        objLteMigrationPlanLoad: {},
        objNewAditionalServices: {},
        objLteMigrationPlanValidation: {}
    };

    $.fn.LtePlanMigration = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('LtePlanMigration'),
                options = $.extend({}, $.fn.LtePlanMigration.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('LtePlanMigration', data);
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

    $.fn.LtePlanMigration.defaults = {
    };

    $('#divBody').LtePlanMigration();
})(jQuery);