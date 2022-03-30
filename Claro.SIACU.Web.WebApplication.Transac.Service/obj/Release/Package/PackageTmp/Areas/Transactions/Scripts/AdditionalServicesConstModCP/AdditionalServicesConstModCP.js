
(function ($) {

    var Form = function ($element, options) {
        $.extend(this, $.fn.AdditionalServicesConstModCP.defaults, $element.data(), typeof options === 'object' && options);
        this.setControls({
            form: $element
            //divErrorAlert: $('#divErrorAlert', $element),

            //lblErrorMessage: $('#lblErrorMessage', $element),
            
            , lblNumberPhone: $('#lblNumberPhone', $element)
            , lblDNIRUC: $('#lblDNIRUC', $element)
            , lblTypeClient: $('#lblTypeClient', $element)
            , lblClient: $('#lblClient', $element)
            , lblContactClient: $('#lblContactClient', $element)
            , lblContact: $('#lblContact', $element)
            , lblServiceBusiness: $('#lblServiceBusiness', $element)
            , lblNewNumPerVal: $('#lblNewNumPerVal', $element)
            , lblQuotModify: $('#lblQuotModify', $element)
            , lblSendEmail: $('#lblSendEmail', $element)
            , lblEmail: $('#lblEmail', $element)
            , lblCacDac: $('#lblCacDac', $element)
            , lblChargeFixed: $('#lblChargeFixed', $element)
            , lblDateExec: $('#lblDateExec', $element)
            , lblNumPerVal: $('#lblNumPerVal', $element)
            , taskNoteModCP: $('#taskNoteModCP', $element)
            , lblLegend: $('#lblLegend', $element)

            , btnCloseCMCP: $('#btnCloseCMCP', $element)

            , myModalLoad: $('#myModalLoad', $element)

            , lblTitle: $('#lblTitle', $element)

        });
    };

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this, controls = this.getControls();
            controls.btnCloseCMCP.addEvent(that, 'click', that.btnCloseCMCP_click);
            that.maximizarWindow();
            that.render();
        },
        setControls: function (value) {
            this.m_controls = value;
        },
        getControls: function () {
            return this.m_controls || {};
        },
        render: function () {
            var that = this, controls = that.getControls();
            controls.lblTitle.text("Cambio de Cuota de Servicios");
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
            
            that.getPageLoad();

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


        btnCloseCMCP_click: function () {
            window.close();
        },
        getPageLoad: function () {
            var that = this,
                param = {
                    IdSession: "111111",
                    HidCaseId: "5746761"//HidCaseId: AdditionalServices.HidCaseId
                };
            var controls = that.getControls();
            //controls.divErrorAlert.hide();

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(param),
                url: location.protocol + "//" + location.host + "/Transactions/Postpaid/AdditionalServicesConstModCP/Page_Load",
                success: function (response) {
                    //alert(sessionTransacPost.SessionParams.DATACUSTOMER.CustomerID);
                    if (response.MessageCode == "A") {
                        alert(response.Message,"Error",
                            function() {
                                parent.window.close();
                            });
                    }

                    controls.lblNumberPhone.text(response.StrlblNumberPhone);
                    controls.lblDNIRUC.text(response.StrlblDNIRUC);
                    controls.lblTypeClient.text(response.StrlblTypeClient);
                    controls.lblClient.text(response.StrlblClient);
                    controls.lblContactClient.text(response.StrlblContactClient);
                    controls.lblContact.text(response.StrlblContact);
                    controls.lblServiceBusiness.text(response.StrlblServiceBusiness);
                    controls.lblNewNumPerVal.text(response.StrlblNewNumPerVal);
                    controls.lblQuotModify.text(response.StrlblQuotModify);
                    controls.lblSendEmail.text(response.StrlblSendEmail);
                    controls.lblEmail.text(response.StrlblEmail);
                    controls.lblCacDac.text(response.StrlblCacDac);
                    controls.lblChargeFixed.text(response.StrlblChargeFixed);
                    controls.lblDateExec.text(response.StrlblDateExec);
                    controls.lblNumPerVal.text(response.StrlblNumPerVal);
                    controls.taskNoteModCP.prop("disabled",true);
                    controls.taskNoteModCP.text(response.StrtaskNoteModCP);
                    if (response.StrStrlblLegendVisible == "T") {
                        controls.lblLegend.show();
                        controls.lblLegend.text(response.StrlblLegend);
                    } else {
                        controls.lblLegend.hide();
                    }


                }
            });

        }

    };

    $.fn.AdditionalServicesConstModCP = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('AdditionalServicesConstModCP'),
                options = $.extend({}, $.fn.AdditionalServicesConstModCP.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('AdditionalServicesConstModCP', data);
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

    $.fn.AdditionalServicesConstModCP.defaults = {
    };

    $('#divBody').AdditionalServicesConstModCP();
})(jQuery);