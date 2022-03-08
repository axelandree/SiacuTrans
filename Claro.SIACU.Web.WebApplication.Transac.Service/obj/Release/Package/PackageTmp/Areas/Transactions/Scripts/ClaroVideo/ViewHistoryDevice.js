var SessionTransac = JSON.parse(sessionStorage.getItem("SessionTransac"));
console.log(SessionTransac);
(function ($, undefined) {
    'use strict';
    var Form = function ($element, options) {
        $.extend(this, $.fn.ViewHistoryDevice.defaults, $element.data(), typeof options == 'object' && options);
        this.setControls({
            form: $element,
            txtFechaDesde: $('#txtFechaDesde', $element),
            txtFechaHasta: $('#txtFechaHasta', $element),
            btnSearchDevice: $('#btnSearchDevice', $element),
            hdSession: $('#hdSession', $element),
            hdTelefono: $('#hdTelefono', $element),
            hdType: $('#hdType', $element)

        });
    };
    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this,
            controls = that.getControls();
            controls.btnSearchDevice.addEvent(that, 'click', that.searchDevice_click);
            controls.txtFechaDesde.datepicker({ format: 'dd/mm/yyyy', endDate: "current" });
            controls.txtFechaHasta.datepicker({ format: 'dd/mm/yyyy', endDate: "current" });
            that.render();
        },
        render: function () {
            var that = this,
                controls = that.getControls();
            that.SearchHistoryDevice();
        },        
        searchDevice_click: function (sender, args) {
            var that = this;

            that.SearchHistoryDevice();
        },
        SearchHistoryDevice: function () {
            var that = this,
                controls = that.getControls(),
                desde = controls.txtFechaDesde.val().trim(),
                hasta = controls.txtFechaHasta.val().trim();

            var d = new Date(), months = 12;


            if (desde == "" || hasta == "") {
                
                hasta = AboveZero(d.getDate()) + "/" + (AboveZero(d.getMonth() + 1)) + "/" + d.getFullYear();

                months = parseInt(GetKeyConfig("strMonthsHistoryDeviceBack"));
                console.log('Cantidad de meses atras: ' + months);


                if (months >= 12) {

                    let ao = months / 12;

                    desde = AboveZero(d.getDate()) + "/" + (AboveZero(d.getMonth() + 1)) + "/" + (d.getFullYear() - ao);
                    
                } else {
                    
                    if (months > 0) {

                        let m = d.getMonth() + 1;


                        if (m == months) {

                            desde = AboveZero(d.getDate()) + "/12/" + (d.getFullYear() - 1);

                        } else if (m > months) {


                            desde = AboveZero(d.getDate()) + "/" + (AboveZero(d.getMonth() + 1 - months)) + "/" + d.getFullYear();
                        } else {
                            let resto = months - m;
                            
                            desde = AboveZero(d.getDate()) + "/" + AboveZero(12 - resto)+ "/" + (d.getFullYear() - 1);

                        }

                    } else {

                        desde = AboveZero(d.getDate()) + "/" + (AboveZero(d.getMonth() + 1)) + "/" + (d.getFullYear() - 1);

                    }
                    
                }

            }
            console.log('Desde: ' + desde + ' Hasta: ' + hasta);

            var objReq = {
                linea: controls.hdTelefono.val().trim(),
                fechaInicio: desde,
                fechaFin: hasta,
                servicioName: 'HistorialDispositivos',
                tipoLinea: controls.hdType.val().trim(),
                tmcod: '1'
            };
            var historialServDispCVRequest = {
                strIdSession: Session.IDSESSION,
                flagIsTMCOD :'1',
                MessageRequest: {
                    Body: { historialServDispCVRequest: objReq }
                }
            };

            console.log(historialServDispCVRequest);

            $('#tblDatadevice').find('tbody').html('');
            $('#btnSearchDevice').attr('data-loading-text', "<i class='fa fa-spinner fa-spin '></i> Cargando..");
            controls.btnSearchDevice.button('loading');
            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '/Transactions/Fixed/ClaroVideo/HistoryDeviceService',
                data: JSON.stringify(historialServDispCVRequest),
                complete: function () {
                    controls.btnSearchDevice.button('reset');
                },
                success: function (response) {
                   
                    console.log('Response HistoryDeviceService - Lista de historial de dispositivos');
                    console.log(response.data);
                    if (response.data.historialServDispCVResponse != null) {
                        if (response.data.historialServDispCVResponse.pHistorialDisp != null) {
                            that.populateGridHistoryService(response.data.historialServDispCVResponse.pHistorialDisp);
                        }
                        else {
                            that.populateGridHistoryService(null);
                        }
                    } else {
                        that.populateGridHistoryService(null);
                    }                   
                }
            });
        },
        populateGridHistoryService: function (Nodes) {
            var that = this;
            var controls = that.getControls();

            var table = $('#tblDatadevice').DataTable();
            table.clear().draw();

            $('#tblDatadevice').DataTable({
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
                "lengthMenu": [[10, 15, 20, 25, -1], [10, 15, 20, 25, "Todos"]],
                data: Nodes,
                columns: [
                    {
                        "data": "tipoDisp"
                    },
                    {
                        "data": "nombreDisp"
                    },
                    {
                        "data": "dispositivoId"
                    },
                    {
                        "data": "fechaAct"
                    },
                    {
                        "data": "fehaExp",
                        "render": 
                            function (data, type, row, meta) {

                                if (type === 'display') {

                                    if (row.fehaExp == null) {

                                        data = '';
                                
                                    } else {
                                        data = row.fehaExp;
                                    }
                                }
                                return data;
                            }
                
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
        strUrl: window.location.protocol + '//' + window.location.host
    };
    $.fn.ViewHistoryDevice = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('ViewHistoryDevice'),
                options = $.extend({}, $.fn.ViewHistoryDevice.defaults,
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
    $.fn.ViewHistoryDevice.defaults = {}
    $('#ContentSuscripcionClaroVideoViewHistoryDevice').ViewHistoryDevice();
})(jQuery);