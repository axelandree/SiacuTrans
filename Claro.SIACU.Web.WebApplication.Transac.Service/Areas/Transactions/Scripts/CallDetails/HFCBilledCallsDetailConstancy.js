sessionTransac = JSON.parse(sessionStorage.getItem("SessionTransac"));
//if (sessionTransac.UrlParams.IDSESSION == null || sessionTransac.UrlParams.IDSESSION == undefined) {
//    sessionTransac.UrlParams.IDSESSION = '0';
//}
(function ($, undefined) {

    

    var Form = function ($element, options) {

        $.extend(this, $.fn.HfcBilledCallsDetailConstancy.defaults, $element.data(), typeof options === 'object' && options);


        this.setControls({
            form: $element
            //loadSessionData
            , lblCACDAC: $('#lblCACDAC', $element)
            , lblFecha: $('#lblFecha', $element)
            , lblTitular: $('#lblTitular', $element)
            , lblCaso: $('#lblCaso', $element)
            , lblRepresentante: $('#lblRepresentante', $element)
            , lblNroClaro: $('#lblNroClaro', $element)
            , lblTipoDoc: $('#lblTipoDoc', $element)
            , lblContrato: $('#lblContrato', $element)
            , lblNroDoc: $('#lblNroDoc', $element)
            , lblCustomerId: $('#lblCustomerId', $element)
            , lblCorreoDL: $('#lblCorreoDL', $element)
            , lblMesesSolicitado: $('#lblMesesSolicitado', $element)
        });
    }

    Form.prototype = {
        constructor: Form,

        init: function () {
            var that = this,
                controls = this.getControls();
            that.render();
        },

        render: function () {
            var that = this, controls = this.getControls();
            that.loadSessionData();
        },

        loadSessionData: function () {
            if (sessionTransac.SessionParams.USERACCESS == {} || sessionTransac.SessionParams.DATACUSTOMER == {}) {
                window.close();
                opener.parent.top.location.href = Session.SessionParams.HIDDEN.hdnSiteUrl;
                return false;
            } else {
                var that = this;
                that.InitHeader();
            }
            return false;
        },

        InitHeader: function () {
            var that = this,
                controls = that.getControls(),
                oCustomer = sessionTransac.SessionParams.DATACUSTOMER,
                oSession = sessionTransac.SessionParams.USERACCESS;

            controls.lblTitular.text(oCustomer.FullName);
            controls.lblRepresentante.text(oCustomer.LegalAgent);
            controls.lblNroClaro.text(oCustomer.PhoneContact);
            controls.lblTipoDoc.text(oCustomer.DocumentType);
            controls.lblNroDoc.text(oCustomer.DocumentNumber);
            controls.lblCustomerId.text(oCustomer.ContractID);

            var objModel = {};
            objModel.strIdSession = "50548795"; //sessionTransac.UrlParams.IDSESSION;
            objModel.strInteraccionId = $('#strInteraccionId').val();

            ////Header
            $.ajax({
                url: '/Transactions/HFC/CallDetails/GetBilledCallsContancyHeader',
                data: JSON.stringify(objModel),
                type: 'POST',
                contentType: "application/json charset=utf-8;",
                dataType: "json",
                success: function (response) {
                    controls.lblCACDAC.text(response.StrCadDac);
                    controls.lblCaso.text("");
                    controls.lblFecha.text(response.StrDate);
                    controls.lblCorreoDL.text(response.StrCorreoDl);
                    controls.lblMesesSolicitado.text(response.StrMonthSolicit);

                    if (response.ChkCorreoDl == true) {
                        $('#chkCorreoDL').prop("checked", true);
                    }
                },
                error: function (err) {
                    //console.logerr);
                }
            });

        },

        setControls: function (value) {
            this.m_controls = value;
        },

        getControls: function () {
            return this.m_controls || {};
        }
    };


    $.fn.HfcBilledCallsDetailConstancy = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('HfcBilledCallsDetailConstancy'),
                options = $.extend({}, $.fn.HfcBilledCallsDetailConstancy.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('HfcBilledCallsDetailConstancy', data);
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

    $.fn.HfcBilledCallsDetailConstancy.defaults = {
    }

    $('#HfcBilledCallsDetailConstancy').HfcBilledCallsDetailConstancy();
})(jQuery);
