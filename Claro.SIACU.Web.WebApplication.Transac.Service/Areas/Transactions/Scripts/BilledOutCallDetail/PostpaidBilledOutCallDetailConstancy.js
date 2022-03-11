(function ($, undefined) {

 

    var Form = function ($element, options) {

        $.extend(this, $.fn.PostpaidBilledOutCallDetailConstancy.defaults, $element.data(), typeof options === 'object' && options);


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
            if (Session.USERACCESS == {} || Session.DATACUSTOMER == {}) {
                window.close();
                opener.parent.top.location.href = Session.RouteSiteSiacpo;
                return false;
            } else {
                var that = this;
                that.InitHeader();
                that.InitDetailLlamada();
            }
            return false;
        },

        InitHeader: function () {
            var that = this,
                controls = that.getControls(),
                oCustomer = Session.DATACUSTOMER;

            controls.lblTitular.text(oCustomer.NOMBRE_COMPLETO);
            controls.lblRepresentante.text(oCustomer.REPRESENTANTE_LEGAL);
            controls.lblNroClaro.text(oCustomer.TELEFONO_CONTACTO);
            controls.lblTipoDoc.text(oCustomer.TIPO_DOC);
            controls.lblNroDoc.text(oCustomer.NRO_DOC);
            controls.lblCustomerId.text(oCustomer.CONTRATO_ID);

            var urlBase = window.location.href;
            urlBase = urlBase.substr(0, urlBase.lastIndexOf('/'));
            var myUrlHeader = urlBase + "/GetBilledCallsContancyHeader";
            var myUrlContent = urlBase + "/GetBilledCallsContancyContent";

            //Header
            $.ajax({
                url: myUrlHeader,
                data: JSON.stringify(model),
                type: 'POST',
                contentType: "application/json charset=utf-8;",
                dataType: "json",
                success: function (response) {
                    controls.lblCACDAC.text(response.StrCadDac);
                    controls.lblCaso.text("");
                    controls.lblFecha.text(response.StrDate);
                },
                error: function (err) {
                    //console.logerr);
                }
            });

            //Content
            $.ajax({
                url: myUrlContent,
                data: JSON.stringify(model),
                type: 'POST',
                contentType: "application/json charset=utf-8;",
                dataType: "json",
                success: function (response) {
                    controls.lblCorreoDL.text(response.StrCorreoDl);
                    controls.lblMesesSolicitado.text(response.StrMessageSolicit);
                    if (response.ChkCorreoDl == 'True') {
                        $('#chkCorreoDL').is(':checked');
                    }
                },
                error: function (err) {
                    //console.logerr);
                }
            });

        },

        InitDetailLlamada: function () {
            var that = this,
                controls = that.getControls(),
                oCustomer = Session.DATACUSTOMER,
                oHidden = Session.HIDDEN,
                oDatLine = Session.DATLINE;

        },

        setControls: function (value) {
            this.m_controls = value;
        },

        getControls: function () {
            return this.m_controls || {};
        },

        strUrl: (window.location.href.substring(0, window.location.href.lastIndexOf('/'))).substring(0,
            (window.location.href.substring(0, window.location.href.lastIndexOf('/'))).lastIndexOf('/'))
    };


    $.fn.PostpaidBilledOutCallDetailConstancy = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('PostpaidBilledOutCallDetailConstancy'),
                options = $.extend({}, $.fn.PostpaidBilledOutCallDetailConstancy.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('PostpaidBilledOutCallDetailConstancy', data);
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

    $.fn.PostpaidBilledOutCallDetailConstancy.defaults = {
    }

    $('#PostpaidBilledOutCallDetailConstancy').PostpaidBilledOutCallDetailConstancy();
})(jQuery);
