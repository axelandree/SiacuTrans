var SessionTransac = JSON.parse(sessionStorage.getItem("SessionTransac"));
console.log(SessionTransac);

(function ($, undefined) {
    'use strict';
    var Form = function ($element, options) {
        $.extend(this, $.fn.HistoryActivateService.defaults, $element.data(), typeof options == 'object' && options);
        this.setControls({
            form: $element,
            hdDescripcion: $('#hdDescripcion', $element),
            hdSession: $('#hdSession', $element),
            hdMobile: $('#hdMobile', $element)
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
            that.searhListarService();
        },

        searhListarService: function () {
            var that = this,
            controls = that.getControls();

            var objReq = {
                linea: controls.hdMobile.val().trim(),
                nombreServicio: controls.hdDescripcion.val().trim(),
                servicioName: 'HistorialServicios',
                tmcod: '1',
                tipoLinea: '1'

            };
            var historialServDispCVRequest = {
                strIdSession: Session.IDSESSION,
                flagIsTMCOD: '1',
                MessageRequest: {
                    Body: { historialServDispCVRequest: objReq }
                }
            };

            console.log('Request HistoryDeviceService - Lista de historial de servicios');
            console.log(historialServDispCVRequest);

            $('#tbHistoryActivateService').find('tbody').html('');

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '/Transactions/Fixed/ClaroVideo/HistoryDeviceService',
                data: JSON.stringify(historialServDispCVRequest),
                complete: function () {
                },
                success: function (response) {

                    console.log('Response HistoryDeviceService - Lista de historial de servicios');
                    console.log(response.data);
                    if (response.data.historialServDispCVResponse != null) {
                        if (response.data.historialServDispCVResponse.pHistorialServ != null) {
                            that.populateGridActivateService(response.data.historialServDispCVResponse.pHistorialServ);
                        }
                        else {
                            that.populateGridActivateService(null);
                        }
                    } else {
                        that.populateGridActivateService(null);
                    }
                },
                error: function (msger) {
                    console.log(msger);
                    that.populateGridActivateService(null);
                }
            });
        },

        populateGridActivateService: function (Lista) {
            var that = this;
            var controls = that.getControls();

            var table = $('#tbHistoryActivateService').DataTable();
            table.clear().draw();

            $('#tbHistoryActivateService').DataTable({
                "scrollY": "100%",
                "scrollCollapse": true,
                "paging": true,
                "searching": true,
                "destroy": true,
                "scrollX": true,
                "sScrollX": "100%",
                "sScrollXInner": "100%",
                "autoWidth": true,
                "order": [[1, "desc"]],
                "lengthMenu": [[5, 15, 20, 25, -1], [5, 15, 20, 25, "Todos"]],
                data: Lista,
                columns: [

                    {
                        "data": "nombreServicio"
                    },
                    {
                        "data": "estado"
                    },
                    {
                        "data": "fechaActivacion"
                    },
                    {
                        "data": "fechaExpiracion"
                    },
                     {
                         "data": "precio"
                     },
                    {
                        "data": "servicio"
                    }
                ],
                language: {
                    "sProcessing": "Procesando...",
                    "sLengthMenu": "Mostrar _MENU_ registros",
                    "sZeroRecords": "No se encontraron resultados",
                    "sEmptyTable": "Ningún dato disponible en esta tabla",
                    "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
                    "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
                    "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
                    "sInfoPostFix": "",
                    "sSearch": "Buscar:",
                    "sUrl": "",
                    "sInfoThousands": ",",
                    "sLoadingRecords": "Cargando...",
                    "oPaginate": {
                        "sFirst": "Primero",
                        "sLast": "Último",
                        "sNext": "Siguiente",
                        "sPrevious": "Anterior"
                    },
                    "oAria": {
                        "sSortAscending": ": Activar para ordenar la columna de manera ascendente",
                        "sSortDescending": ": Activar para ordenar la columna de manera descendente"
                    }
                },
            });
        },
        getControls: function () {
            return this.m_controls || {};
        },
        setControls: function (value) {
            this.m_controls = value;
        },
        strUrl: window.location.protocol + '//' + window.location.host,
    };
    $.fn.HistoryActivateService = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('HistoryActivateService'),
                options = $.extend({}, $.fn.HistoryActivateService.defaults,
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

                var timeReady = setInterval(function () {
                    if (!!$.fn.addEvent) {
                        clearInterval(timeReady);
                        data.init();
                    }
                }, 100);

                if (args[1]) {
                    value = data[args[1]].apply(data, [].slice.call(args, 2));
                }
            }
        });

        return value || this;
    };
    $.fn.HistoryActivateService.defaults = {}
    $('#ContentHistoryActivateService').HistoryActivateService();

})(jQuery);