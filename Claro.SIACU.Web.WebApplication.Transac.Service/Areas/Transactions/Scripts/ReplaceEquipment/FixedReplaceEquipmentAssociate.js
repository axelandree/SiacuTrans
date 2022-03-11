
(function ($, undefined) {

    var Form = function ($element, options) {
        $.extend(this, $.fn.FixedReplaceEquipmentAssociate.defaults, $element.data(), typeof options === 'object' && options);
        this.setControls({
            form: $element,
            sltServiceTypes: $('#sltServiceTypes', $element),
            txtCPESeries: $('#txtCPESeries', $element),
            txtSIMCARD: $('#txtSIMCARD', $element),
            txtSMARTCARD: $('#txtSMARTCARD', $element),
            txtDecoSeries: $('#txtDecoSeries', $element),
            divCPESeries: $('#divCPESeries', $element),
            divSMARTCARD: $('#divSMARTCARD', $element),
            divSIMCARD: $('#divSIMCARD', $element),
            divDecoSeries: $('#divDecoSeries', $element),
            divFullChange: $('#divFullChange', $element),
            chkFullReplaceEquipment: $('#chkFullReplaceEquipment', $element),
            lblCheckFullReplace: $('#lblCheckFullReplace', $element)
        });
    }
    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this,
            controls = that.getControls();

            controls.divSMARTCARD.hide();
            controls.divDecoSeries.hide();
            controls.divCPESeries.hide();
            controls.divSIMCARD.hide();
            controls.divFullChange.hide();

            controls.txtSMARTCARD.addEvent(controls.txtSMARTCARD, 'keyup', that.txtAssociateFields_keyPress);
            controls.txtDecoSeries.addEvent(controls.txtDecoSeries, 'keyup', that.txtAssociateFields_keyPress);
            controls.txtCPESeries.addEvent(controls.txtCPESeries, 'keyup', that.txtAssociateFields_keyPress);
            controls.txtSIMCARD.addEvent(controls.txtSIMCARD, 'keyup', that.txtAssociateFields_keyPress);

            controls.chkFullReplaceEquipment.addEvent(this, 'click', that.chkFullChange_clicked);

            that.loadSessionData();
            that.FixedReplaceEquipmentLoadModalAssociate();
            that.render();
        },
        chkFullChange_clicked: function (e) {
            var that = this, controls = that.getControls();
            var objchkSmartCardChange = document.getElementById($('#chkFullReplaceEquipment').attr("id"));
            if (objchkSmartCardChange.checked == true) {
                if (that.objReplaceEquipmentModalAssociateLoad.strCurrentEquipmentTypeSelected == "3" || that.objReplaceEquipmentModalAssociateLoad.strCurrentEquipmentTypeSelected == "4") {
                    controls.divSIMCARD.show();
                } else {
                    controls.divSMARTCARD.show();
                }
            } else {
                if (that.objReplaceEquipmentModalAssociateLoad.strCurrentEquipmentTypeSelected == "3" && that.objReplaceEquipmentModalAssociateLoad.strCurrentEquipmentTypeSelected == "4") {
                    controls.divSIMCARD.hide();
                } else {
                    controls.divSMARTCARD.hide();
                }
            }
        },
        txtAssociateFields_keyPress: function (e) {
            var regxNumber = /^[a-zA-Z0-9_]+$/;
            var text = e.val();
            var blvalidateNumber = regxNumber.test(text);
            e.val(text.toUpperCase());
            var textLenght = text.length;
            if (blvalidateNumber == false && text.length != 0 || textLenght > 60) {
                var valor = text.substring(0, text.length - 1).trim();
                e.val(valor);
                e.focus();
            }
        },
        loadSessionData: function () {
            var SessionTransac = JSON.parse(sessionStorage.getItem("SessionTransac"));
            Session.IDSESSION = SessionTransac.UrlParams.IDSESSION;
        },
        FixedReplaceEquipmentLoadModalAssociate: function () {
            var that = this;
            $.app.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '/Transactions/Fixed/ReplaceEquipment/FixedReplaceEquipmentLoadModalAssociate',
                async: false,
                success: function (response) {
                    if (response.data != null) {
                        that.objReplaceEquipmentModalAssociateLoad = response.data;
                    }
                },
                error: function (errormessage) {
                    alert('Error: ' + errormessage);
                }
            });
        },
        showFields: function (strCurrentEquipmentTypeSelected) {
            var that = this, controls = that.getControls();
            var limit = parseInt(that.objReplaceEquipmentModalAssociateLoad.strEquipmentLimits);
            var equipmentUsed = that.objReplaceEquipmentModalAssociateLoad.strEquipmentAssociateUsed;
            var associateEquipments = parseInt(that.objReplaceEquipmentModalAssociateLoad.strEquipmentAssociate);

            if (strCurrentEquipmentTypeSelected == "1" || strCurrentEquipmentTypeSelected == "2") {
                controls.lblCheckFullReplace.append(that.strMsgCheckFullReplace.replace("###", "SMARTCARD"));
                if (strCurrentEquipmentTypeSelected != "1") { // !=SMARTCARD
                    controls.divDecoSeries.show();
                    controls.txtDecoSeries.focus();
                    controls.divFullChange.show();
                    if (associateEquipments >= limit || equipmentUsed === "1") {
                        $('#chkFullReplaceEquipment').prop('disabled', 'disabled');
                    } else {
                        $('#chkFullReplaceEquipment').removeAttr('disabled');
                    }
                } else {
                    controls.divSMARTCARD.show();
                    controls.txtSIMCARD.focus();
                }
            } else if (strCurrentEquipmentTypeSelected == "3" || strCurrentEquipmentTypeSelected == "4") {
                controls.lblCheckFullReplace.append(that.strMsgCheckFullReplace.replace("###", "SIMCARD"));
                if (strCurrentEquipmentTypeSelected == "4") {// != SIMCARD
                    controls.divCPESeries.show();
                    controls.txtCPESeries.focus();

                    if (that.objReplaceEquipmentModalAssociateLoad.strActiveFullReplaceEquipmentForFixed == "1") {
                        controls.divFullChange.show();
                        if (associateEquipments >= limit || equipmentUsed === "1") {
                            $('#chkFullReplaceEquipment').prop('disabled', 'disabled');
                        } else {
                            $('#chkFullReplaceEquipment').removeAttr('disabled');
                        }
                    } else {
                        controls.divSIMCARD.show();
                        if (associateEquipments >= limit || equipmentUsed === "1") {
                            $('#txtSIMCARD').prop('disabled', 'disabled');
                        } else {
                            $('#txtSIMCARD').removeAttr('disabled');
                        }
                    }
                } else {
                    controls.divSIMCARD.show();
                    controls.txtSIMCARD.focus();
                }
            }
        },
        getServiceTypes: function (lstServiceTypes, currentEquipmentTypeSelected) {
            if (currentEquipmentTypeSelected == "3" || currentEquipmentTypeSelected == "4")
                currentEquipmentTypeSelected = "LTE";
            else currentEquipmentTypeSelected = "TV";
            if (lstServiceTypes.length > 0) {
                $("#sltServiceTypes").val('');
                $("#sltServiceTypes").append($('<option>', { value: '', html: 'Seleccionar' }));
                var itemSelect;
                $.each(lstServiceTypes, function (index, value) {
                    if (currentEquipmentTypeSelected == value.Code) {
                        $("#sltServiceTypes").append($('<option>', { value: value.Code, html: value.Description }));
                        itemSelect = value.Code;

                    } else {
                        $("#sltServiceTypes").append($('<option>', { value: value.Code, html: value.Description }));
                    }
                });
                if (itemSelect != null && itemSelect.toString != "undefined") {
                    $("#sltServiceTypes option[value=" + itemSelect + "]").attr("selected", true);
                }
                $("#sltServiceTypes").prop('disabled', true);
            }
        },
        setControls: function (value) {
            this.m_controls = value;
        },
        getControls: function () {
            return this.m_controls || {};
        },
        render: function () {
            var that = this,
                controls = this.getControls();

            var objMsg = that.objReplaceEquipmentModalAssociateLoad.objReplaceEquipmentMessageModel;

            if (that.objReplaceEquipmentModalAssociateLoad.lstServiceTypes != null && that.objReplaceEquipmentModalAssociateLoad.lstServiceTypes.length > 0) {
                that.getServiceTypes(that.objReplaceEquipmentModalAssociateLoad.lstServiceTypes, that.objReplaceEquipmentModalAssociateLoad.strCurrentEquipmentTypeSelected);
            } else {
                alert(objMsg.strMsjValidacionCargadoListado.replace("###", "Tipos de Servicio"), 'Alerta', function () { });
            }
          
            if (objMsg.strMsgCheckFullReplace != null &&
             objMsg.strMsgCheckFullReplace != "") {
                that.strMsgCheckFullReplace = objMsg.strMsgCheckFullReplace;
            }
            if (that.objReplaceEquipmentModalAssociateLoad.strCurrentEquipmentTypeSelected != null &&
                that.objReplaceEquipmentModalAssociateLoad.strCurrentEquipmentTypeSelected != "") {
                that.showFields(that.objReplaceEquipmentModalAssociateLoad.strCurrentEquipmentTypeSelected);
            }

        },
        strUrlLogo: window.location.protocol + '//' + window.location.host + '/Images/loading2.gif',
        objReplaceEquipmentModalAssociateLoad: {},
        strMsgCheckFullReplace: ""
    }
    $.fn.FixedReplaceEquipmentAssociate = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];
        this.each(function () {
            var $this = $(this),
                data = $this.data('FixedReplaceEquipmentAssociate'),
                options = $.extend({}, $.fn.FixedReplaceEquipmentAssociate.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('FixedReplaceEquipmentAssociate', data);
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
    $.fn.FixedReplaceEquipmentAssociate.defaults = {
    }
    $('#modal-content').FixedReplaceEquipmentAssociate();
})(jQuery);