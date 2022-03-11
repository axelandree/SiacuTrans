(function ($) {
    var Form = function ($element, options) {

        $.extend(this, $.fn.HfcIncomingCallDetailPrint.defaults, $element.data(), typeof options === 'object' && options);
        this.setControls({
            form: $element
            , btnCloseConstancy: $('#btnCloseConstancy', $element)
            , btnPrintConstancy: $('#btnPrintConstancy', $element)
            , botonesConst: $('#botonesConst', $element)
            , lblTitle: $('#lblTitle', $element)
            , tblDetailVisualizeCall: $('#tblDetailVisualizeCall', $element)
        });
    }

    Form.prototype = {
        constructor: Form,

        init: function () {
            var that = this, controls = this.getControls();
            controls.btnCloseConstancy.addEvent(that, 'click', that.btnCloseConstancy_Click);
            controls.btnPrintConstancy.addEvent(that, 'click', that.btnPrintConstancy_Click);
            that.render();
        },

        render: function () {
            var that = this, controls = this.getControls();
            that.getLoad();
            that.btnPrintConstancy_Click();
        },

        getLoad: function(){
            var that = this,
            controls = this.getControls();       
            
            var session = JSON.parse(sessionStorage.getItem("SessionTransac"));
            
            if (session.UrlParams.SUREDIRECT === "HFC") {
                controls.lblTitle.text("DETALLE DE LLAMADAS ENTRANTES HFC");
            } else {
                controls.lblTitle.text("DETALLE DE LLAMADAS ENTRANTES LTE");              
            }


            
            var LstPhoneCall = JSON.parse(sessionStorage.getItem("HFCLTELstPhoneCall"));
            if (LstPhoneCall != null) {
                $.each(LstPhoneCall, function (index, value) {
                    controls.tblDetailVisualizeCall.append('<tr><td td class="col-sm-3">' + value.NroOrd + '</td><td td class="col-sm-3">' + value.MSISDN + '</td><td td class="col-sm-3">' + value.CallDate + '</td><td td class="col-sm-3">' + value.CallTime + '</td><td td class="col-sm-3">' + value.CallNumber + '</td><td td class="col-sm-3">' + value.CallDuration + '</td></tr>');
                });
            }



        },

        setControls: function (value) {
            this.m_controls = value;
        },

        getControls: function () {
            return this.m_controls || {};
        },

        btnCloseConstancy_Click: function () {
            parent.window.close();
        },
        btnPrintConstancy_Click: function () {
            var that = this;
            var controls = that.getControls();
            controls.botonesConst.hide();
            window.print();
            controls.botonesConst.show();
        }
    };

    $.fn.HfcIncomingCallDetailPrint = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('HfcIncomingCallDetailPrint'),
                options = $.extend({}, $.fn.HfcIncomingCallDetailPrint.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('HfcIncomingCallDetailPrint', data);
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

    $.fn.HfcIncomingCallDetailPrint.defaults = {
    }

    $('#divBody').HfcIncomingCallDetailPrint();
})(jQuery);

