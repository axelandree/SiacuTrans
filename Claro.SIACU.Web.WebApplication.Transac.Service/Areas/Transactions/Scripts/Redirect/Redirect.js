(function ($, undefined) {


    var Form = function ($element, options) {

        $.extend(this, $.fn.DataLoadRedirect.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({

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
            this.LoadRedirect();
        },
        LoadRedirect: function () {           
            var that = this,
             controls = this.getControls();

            var strSequence = $("#divContenidoRedirect").data("sequence");
            var session1 = "123123";  //Session.IDSESSION;
            var StrURL= this.getRoute();

            var urlRedirectVal = StrURL + "/Transactions/Redirect/GetRedirect";
           
            $.ajax({

                type: 'POST',
                url: urlRedirectVal,
                data: { strIdSession: session1, sequence: strSequence },
                dataType: 'json',
                error: function (data, status) {
                    alert("La página no se encuentra disponible en estos momentos. Vuelva intentarlo en breve.", "Informativo");
                },
                success: function (data) {
                    
                    if (data.length > 0) {
                       
                        var urlDest = data[0];
                        var Availability = data[1];
                        var jsonParameters = data[2];
                        var node = data[3];


                        var objparameters = JSON.stringify(jsonParameters);
                        Session.Parameters = JSON.parse(JSON.parse(objparameters));
                        sessionStorage.setItem("SessionTransac", JSON.parse(objparameters));                       
                        that.LoadTransaction(urlDest);
                        $("#lblNode").text(node);
                    }
                    else {
                        alert("No se encontró la página.", "Alerta");
                    }
                },
            });


        },
        LoadTransaction: function (urlDest) {           
            var session1 = "123123";  //Session.IDSESSION;
            var strURL= this.getRoute();          

            var strUrlContent = strURL + urlDest;            
            var dataContent = {};

            $.ajax({
                type: "POST",
                url: strUrlContent,
                data: dataContent,
                async: false,
                success: function (result3) {
                    ////console.logresult3);
                    if (result3 != null || result3 != "" || typeof (result3 != 'undefined')) {
                        $('#divContenidoRedirect').html(result3);
                    } else {
                        $(this).focus();
                        alert('No se Encontrarón Resultados', this.strTitleMessage);
                        $('#divContenidoRedirect').html("");
                    }
                },
                error: function (msg) {

                }
            });


        },

    
        getControls: function () {
            return this.m_controls || {};
        },
        setControls: function (value) {
            this.m_controls = value;
        },
        getRoute: function () {
            return window.location.protocol + '//' + window.location.host;  //window.location.href;
        },
        getRouteTemplate: function () {
            return window.location.href + '/Home/DialogTemplate';
        }
    };

    $.fn.DataLoadRedirect = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('DataLoadRedirect'),
                options = $.extend({}, $.fn.DataLoadRedirect.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('DataLoadRedirect', data);
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

    $.fn.DataLoadRedirect.defaults = {
    }

    $('#divContenidoRedirect').DataLoadRedirect();

})(jQuery);