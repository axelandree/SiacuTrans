console.log('ingreso');
var SessionTransac = JSON.parse(sessionStorage.getItem("SessionTransac"));
console.log(SessionTransac);
(function ($, undefined) {
    'use strict';

    var Form = function ($element, options) {
        $.extend(this, $.fn.RestricSellInfoPromotionUploadInfoProm.defaults, $element.data(), typeof options == 'object' && options);
        this.setControls({
            form: $element            
            , hdNumeroDocumento: $('#hdNumeroDocumento', $element)
            , radio_todas_Lineas: $('#radio_todas_Lineas', $element)
            , radio_algunas_Lineas: $('#radio_algunas_Lineas', $element)
            , dataFile: $('#dataFile', $element)
            , btnCarga: $('#btnCarga', $element)

        });
    };

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this,
            controls = that.getControls();

            that.render();
        },
        render: function () {
            var that = this,
            controls = that.getControls();           
            controls.btnCarga.addEvent(this, 'click', this.btnCarga_click);
            controls.radio_todas_Lineas.addEvent(this, 'click', this.radio_todas_Lineas_click);
            controls.radio_algunas_Lineas.addEvent(this, 'click', this.radio_algunas_Lineas_click);
            controls.dataFile.addEvent(this, 'change', this.dataFile_change);

        },
        btnGrabar_click: function (sender, args) {
           var that = this,
           controls = that.getControls();
            console.log('Se procedera a grabar.........');

            if($('#radio_algunas_Lineas').is(':checked')) {
                var files = $('#dataFile').val();
                if (files == '') {
                    alert("Debe seleccionar un archivo");
                    return false;
                }
            }

            var lenCheck = $('.myCheck').filter(':checked').length;
            if (lenCheck == 0) {
                alert("Debe seleccionar un tipo");
                return false;
            }

            confirm('¿Desea Continuar el tipo de Carga Masiva?', 'Confirmar', function (result) {
                if (result == true) {

                    console.log('Se procedera a grabar');

                    showLoadingPopup('Cargando...');

                    var strCheck = 0;
                    if ($('#radio_todas_Lineas').is(':checked')) {
                        strCheck = 1;
                    } else if ($('#radio_algunas_Lineas').is(':checked')) {
                        strCheck = 2;
                    }

                    var data = new FormData();
                    data.append('strDNI', $('#hdNumeroDocumento').val());
                    data.append('strTelefono', $('#hdstrTelefono').val());
                    data.append('strCheck', strCheck);
                    data.append('strProcessAnyLine', $('#hdstrProcessAnyLine').val());
                    $('#spanCargaMasivo').html('');

                    $.ajax({
                        url: '/Transactions/RestricSellInfoPromotion/GrabarTxt',
                        data: data,
                        cache: false,
                        contentType: false,
                        processData: false,
                        method: 'POST',
                        type: 'POST',
                        complete: function () {
                            hideLoading();
                        },
                        success: function (response) {
                            debugger;
                            if (response != null && response != undefined) {

                                console.log(response);
                                var tipo = $.trim(response.data.xxtipo);
                                var mensaje = $.trim(response.data.mensaje);

                                if (tipo == "C") {
                                    console.log(mensaje);

                                SessionTransac.TipoCargaMasiva = response.TipoCargaMasiva;

                                if (response.TipoCargaMasiva == "1") {
                                    for (var i = 0; i < response.listaLineas.length; i++) {
                                        if (response.listaLineas[i].length > 9) {
                                            response.listaLineas[i] = response.listaLineas[i].substr(response.listaLineas[i].length - 9);
                                        }
                                    }
                                SessionTransac.LeyPromoListaLineas = response.listaLineas;

                                    } else if (response.TipoCargaMasiva == "2") {
                                        for (var i = 0; i < SessionTransac.LeyPromoListaLineas.length; i++) {
                                            if (SessionTransac.LeyPromoListaLineas[i].length > 9) {
                                                SessionTransac.LeyPromoListaLineas[i] = SessionTransac.LeyPromoListaLineas[i].substr(SessionTransac.LeyPromoListaLineas[i].length - 9);
                                            }
                                }
                                        SessionTransac.LeyPromoListaLineas = SessionTransac.LeyPromoListaLineas;
                                }
                                    $("#btnCerrarUploadInfoProm").click();
                                }
                                else if (tipo == "I") {
                                    SessionTransac.TipoCargaMasiva = "0";
                                    alert(mensaje);
                                }
                                else if (tipo == "E") {
                                    SessionTransac.TipoCargaMasiva = "0";
                                    alert(mensaje);
                                }

                            }

                        }
                    });

                } else {

                }
            });


        },
        btnCarga_click: function (sender, args) {
            var that = this,
                controls = that.getControls();

            var files = $('#dataFile').val();
            if (files == '') {
                alert("Debe seleccionar un archivo");
                return false;
            }

            showLoadingPopup('Cargando...');
            $(".blockOverlay").css("z-index", "1050");

            var data = new FormData();
            $.each($('#dataFile')[0].files, function (i, file) {
                data.append('file-' + i, file);
            });

            data.append('strDNI', $('#hdNumeroDocumento').val());
            data.append('strTelefono', $('#hdstrTelefono').val());
            $('#spanCargaMasivo').html('');

            $.ajax({
                url: '/Transactions/RestricSellInfoPromotion/CargarTxt',
                data: data,
                cache: false,
                contentType: false,
                processData: false,
                method: 'POST',
                type: 'POST',
                success: function (response) {


                    //Hacer Algo
                    if (response != null && response != undefined) {
                        console.log(response);

                       // response = $.trim(response);
                       // var jsonDatos = $.parseJSON(response);
                        var tipo = $.trim(response.data.xxtipo);
                        var mensaje = $.trim(response.data.mensaje);

                        if (tipo == "C") {
                            var carga = parseInt(mensaje) > 1 ? " cargaron " : " cargo ";
                            var line = parseInt(mensaje) > 1 ? " líneas " : " línea ";
                            var oText = 'Se cargaron ' + mensaje + ' lineas en la carga por lineas';

                            SessionTransac.LeyPromoListaLineas = response.listaLineas;

                            $('#spanCargaMasivo').html(oText);
                            $('#hdstrProcessAnyLine').val('1');
                        }
                        else if (tipo == "I") {
                            alert(mensaje);
                            $('#spanCargaMasivo').html('');
                            $('#hdstrProcessAnyLine').val('0');
                        }
                        else if (tipo == "E") {
                            alert(mensaje);
                        }
                    }

                },
                complete: function () {
                   hideLoading();
                },
            });


        },
        dataFile_change: function (sender, args) {
            var val = $('#dataFile').val().toLowerCase(),
                regex = new RegExp("(.*?)\.(txt)$");

            if (!(regex.test(val))) {
                alert('Debe seleccionar un archivo .txt');
                document.getElementById("dataFile").value = null;
                return false;
            }
        },
        radio_todas_Lineas_click: function () {
            var that = this,
            controls = that.getControls();
            $('#btnCarga').attr('disabled', true);
            $('input#dataFile').prop('disabled', true); 
            document.getElementById("dataFile").value = null;
            $('#spanCargaMasivo').html('');
            $('#hdstrProcessAnyLine').val('0');
        },
        radio_algunas_Lineas_click: function () {
            var that = this,
            controls = that.getControls();
            $('#btnCarga').attr('disabled', false);
            $('input#dataFile').prop('disabled', false); 
            $('#spanCargaMasivo').html('');
            $('#hdstrProcessAnyLine').val('0');
        },
        getControls: function () {
            return this.m_controls || {};
        },
        setControls: function (value) {
            this.m_controls = value;
        },
        strUrl: window.location.protocol + '//' + window.location.host,

    };
    $.fn.RestricSellInfoPromotionUploadInfoProm = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = ['btnGrabar_click'];

        this.each(function () {

            var $this = $(this),
                data = $this.data('RestricSellInfoPromotion'),
                options = $.extend({}, $.fn.RestricSellInfoPromotionUploadInfoProm.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('RecordEquipment', data);
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
    $.fn.RestricSellInfoPromotionUploadInfoProm.defaults = {}
    $('#ContentRestricSellInfoPromotionUploadInfoProm').RestricSellInfoPromotionUploadInfoProm();

})(jQuery);


