(function ($, undefined) {

    var Form = function ($element, options) {

        $.extend(this, $.fn.RentalEquipment.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , tblEquipmentsAditional: $('#objAdditionalEquipments', $element)
            , spnQuantityTotal: $('#spnQuantityTotal', $element)
            , spnQuantitySD: $('#spnQuantitySD', $element)
            , spnQuantityHD: $('#spnQuantityHD', $element)
            , spnQuantityDVR: $('#spnQuantityDVR', $element)
            , btnMinSD: $('#btnMinSD', $element)
            , btnMinHD: $('#btnMinHD', $element)
            , btnMinDVR: $('#btnMinDVR', $element)
            , btnMoreSD: $('#btnMoreSD', $element)
            , btnMoreHD: $('#btnMoreHD', $element)
            , btnMoreDVR: $('#btnMoreDVR', $element)
            , spnValueDefault: $('#spnValueDefault', $element)
            , spnMsjDecosNotAvailable: $('#spnMsjDecosNotAvailable', $element)
            
        });

    }

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this;

            $(".btnMore").addEvent(that, 'click', that.AddEquipment);
            $(".btnMin").addEvent(that, 'click', that.SubstractEquipment);
            that.LTEPlanMigrationChooseEquipmentByPlanLoad();
            that.render();
        },
        render: function () {
            var that = this,
                controls = that.getControls();
            controls.spnQuantitySD.text(Session.objQuantityTypeDeco.DecoSD);
            controls.spnQuantityHD.text(Session.objQuantityTypeDeco.DecoHD);
            controls.spnQuantityDVR.text(Session.objQuantityTypeDeco.DecoDVR);
            
            var strCambioPlan = "*Servicio Actual: " + Session.objQuantityTypeDeco.DecoHDDefault + "-HD, " + Session.objQuantityTypeDeco.DecoSDDefault + "-SD, " + Session.objQuantityTypeDeco.DecoDVRDefault + "-DVR";
            controls.spnValueDefault.text(strCambioPlan);
            that.VerifyQuantityMin();
            that.CalculateTotal();
            if (that.objLtePlanMigrationChooseEquipmentByPlanLoad.objDecoMatriz == null) {
                that.DisabledButton();
                alert(that.objLtePlanMigrationChooseEquipmentByPlanLoad.objMessage.strMessageErrorValidationEquipment,
                    "Alerta");
            } else if (that.objLtePlanMigrationChooseEquipmentByPlanLoad.objDecoMatriz.ListaMatrizDecos == null) {
                    that.DisabledButton();
                    alert(that.objLtePlanMigrationChooseEquipmentByPlanLoad.objMessage
                        .strMessageErrorValidationEquipment,
                        "Alerta");
                }
            
            $("thead tr th").removeClass("select-checkbox");
        },
        LTEPlanMigrationChooseEquipmentByPlanLoad: function () {
            var that = this,
                controls = that.getControls(),
                objRequestDataModel = {};
            objRequestDataModel.strIdSession = Session.IDSESSION;
            objRequestDataModel.strIdContract = Session.DATACUSTOMER.ContractID;
            objRequestDataModel.strTypeProduct = Session.ProductType;
            objRequestDataModel.strIdPlan = Session.idPlan;
            

            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objRequestDataModel),
                url: '/Transactions/LTE/PlanMigration/LTEChooseEquipmentByPlanLoad',
                async: false,
                success: function (response) {
                    if (response.data != null) {
                        that.objLtePlanMigrationChooseEquipmentByPlanLoad = response.data;
                    }
                }
            });
        },
        AddEquipment: function (ctrl) {
            var that = this;
            var strType = $(ctrl).data("type");
            var intQuantity = parseInt($("#spnQuantity" + strType).text());
            intQuantity = intQuantity + 1;

            that.ModifyDataSessionDeco(intQuantity, strType);
            var intResult = that.VerifyMatriz();
            if (intResult == 0) {
                that.ModifyDataSessionDeco(intQuantity-1, strType);
                alert(that.objLtePlanMigrationChooseEquipmentByPlanLoad.objMessage.strMessageQuantityEquipment, "Alerta");
            }
            else if (intResult == 1) {
                $("#spnQuantity" + strType).text(intQuantity);
            }
            that.CalculateTotal();
        },
        SubstractEquipment: function (ctrl) {
            var that = this;
            var strType = $(ctrl).data("type");
            var intQuantity = parseInt($("#spnQuantity" + strType).text());
            if (intQuantity > 0) {
                intQuantity = intQuantity - 1;
                    that.ModifyDataSessionDeco(intQuantity, strType);
                    var intResult = that.VerifyMatriz();
                    if (intResult == 0) {
                        $("#spnQuantity" + strType).text(intQuantity);
                        alert(that.objLtePlanMigrationChooseEquipmentByPlanLoad.objMessage.strMessageQuantityEquipment, "Alerta");
                        
                    }
                    else if (intResult == 1) {
                        $("#spnQuantity" + strType).text(intQuantity);
                    }
                   
                that.CalculateTotal();
            }
        },
        ModifyDataSessionDeco: function (intQuantity, strType) {
            switch (strType) {
            case "HD":
                Session.objQuantityTypeDeco.DecoHD = intQuantity;
                break;
            case "DVR":
                Session.objQuantityTypeDeco.DecoDVR = intQuantity;
                break;
            default:
                Session.objQuantityTypeDeco.DecoSD = intQuantity;
            }
        },
        VerifyMatriz: function () {

                var that = this;
                if (that.objLtePlanMigrationChooseEquipmentByPlanLoad.objDecoMatriz != null) {
                    var objMatriz = that.objLtePlanMigrationChooseEquipmentByPlanLoad.objDecoMatriz.ListaMatrizDecos;
                    var intTotal = 0;
                    if (objMatriz != null) {
                        $.each(objMatriz,function (index, value) {
                                switch (value.Descripcion) {
                                case "HD":
                                    intTotal = intTotal + (Session.objQuantityTypeDeco.DecoHD + Session.objQuantityTypeDeco.DecoHDDefault) * parseInt(value.Valor);
                                    break;
                                case "DVR":
                                    intTotal = intTotal + (Session.objQuantityTypeDeco.DecoDVR + Session.objQuantityTypeDeco.DecoDVRDefault )* parseInt(value.Valor);
                                    break;
                                default:
                                    intTotal = intTotal + (Session.objQuantityTypeDeco.DecoSD + Session.objQuantityTypeDeco.DecoSDDefault) * parseInt(value.Valor);
                                }
                            });
                        var intMaxEquipment = parseInt(that.objLtePlanMigrationChooseEquipmentByPlanLoad.objMessage.intQuantityMaxPoint);
                        if (intTotal > intMaxEquipment)
                            return 0; 
                        return 1; 
                    } else {
                        that.DisabledButton();
                        return 3;
                    } 
                    
                    
                } else {
                    that.DisabledButton();
                    return 3;
                } 


        },
        DisabledButton: function () {
            var that = this,
                controls = that.getControls();
                controls.btnMinSD.prop('disabled', true);
                controls.btnMinHD.prop('disabled', true);
                controls.btnMinDVR.prop('disabled', true);
                controls.btnMoreSD.prop('disabled', true);
                controls.btnMoreHD.prop('disabled', true);
                controls.btnMoreDVR.prop('disabled', true);
        },
        VerifyQuantityMin: function () {
            var that = this,
                controls = that.getControls();
            var strMsjServicesNotAvailable = "-";
            if (Session.objQuantityTypeDeco.QuantityMaxDecoHD == 0) {
                controls.btnMoreHD.prop('disabled', true);
                controls.btnMinHD.prop('disabled', true);
                strMsjServicesNotAvailable = (strMsjServicesNotAvailable == "-") ? "HD" : ",HD";
            }
            if (Session.objQuantityTypeDeco.QuantityMaxDecoDVR == 0) {
                controls.btnMoreDVR.prop('disabled', true);
                controls.btnMinDVR.prop('disabled', true);
                strMsjServicesNotAvailable = (strMsjServicesNotAvailable == "-") ? "DVR" : ",DVR";

            }
            if (Session.objQuantityTypeDeco.QuantityMaxDecoSD == 0) {
                controls.btnMoreSD.prop('disabled', true);
                controls.btnMinSD.prop('disabled', true);
                strMsjServicesNotAvailable = (strMsjServicesNotAvailable == "-") ? "SD" : strMsjServicesNotAvailable + ",SD";
            }
            controls.spnMsjDecosNotAvailable.text((strMsjServicesNotAvailable == "-") ? "" : "*Servicio No disponible: " + strMsjServicesNotAvailable);
        },
        /*VerifyValueDefault: function (strType, intValor) {
            var that = this;
            var intQuantityValueDefault = 0;
            switch (strType) {
            case "HD":
                if (intValor < parseInt(Session.objQuantityTypeDeco.DecoHDDefault))
                    return that.objLtePlanMigrationChooseEquipmentByPlanLoad.objMessage.strMessageQuantityXEquipment
                        .replace("####", "HD").replace("###", Session.objQuantityTypeDeco.DecoHDDefault);
                break;
            case "DVR":
                if (intValor < parseInt(Session.objQuantityTypeDeco.DecoDVRDefault))
                    return that.objLtePlanMigrationChooseEquipmentByPlanLoad.objMessage.strMessageQuantityXEquipment
                        .replace("####", "DVR").replace("###", Session.objQuantityTypeDeco.DecoDVRDefault);
                break;
            default:
                intQuantityValueDefault = parseInt(that.objLtePlanMigrationChooseEquipmentByPlanLoad.objMessage.intQuantityDefaultSD);
                if (intValor < parseInt(Session.objQuantityTypeDeco.DecoSDDefault))
                    return that.objLtePlanMigrationChooseEquipmentByPlanLoad.objMessage.strMessageQuantityXEquipment
                        .replace("####", "SD").replace("###", Session.objQuantityTypeDeco.DecoSDDefault);
                break;
            }
            return "";
        },*/
        CalculateTotal: function() {
            var that = this,
                controls = that.getControls();
            var intTotal = parseInt(controls.spnQuantitySD.text()) + parseInt(controls.spnQuantityHD.text()) + parseInt(controls.spnQuantityDVR.text());
            controls.spnQuantityTotal.text("TOTAL " + intTotal);
        },
        getControls: function () {
            return this.m_controls || {};
        },
        setControls: function (value) {
            this.m_controls = value;
        },
        objLtePlanMigrationChooseEquipmentByPlanLoad: {}
    };
    $.fn.RentalEquipment = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('RentalEquipment'),
                options = $.extend({}, $.fn.RentalEquipment.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('RentalEquipment', data);
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
    $.fn.RentalEquipment.defaults = {
    };

    $('#RentalEquipment').RentalEquipment();
})(jQuery);