var SessionTransac = JSON.parse(sessionStorage.getItem("SessionTransac"));
console.log(SessionTransac);
(function ($, undefined) {
    'use strict';
    var Form = function ($element, options) {
        $.extend(this, $.fn.HistoryVisualizationClient.defaults, $element.data(), typeof options == 'object' && options);
        this.setControls({
            form: $element,
            FechaDesde: $('#FechaDesde', $element),
            FechaHasta: $('#FechaHasta', $element),
            hdidRefSuscripcionHistory: $('#hdidRefSuscripcionHistory', $element),
            hdCustomerIDClaroVideo: $('#hdCustomerIDClaroVideo', $element),
            btnSearchHistory: $('#btnSearchHistory', $element)
            
        });
    };
    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this,
            controls = that.getControls();
            controls.FechaDesde.datepicker({ format: 'dd/mm/yyyy', endDate: "current" });
            controls.FechaHasta.datepicker({ format: 'dd/mm/yyyy', endDate: "current" });
            controls.btnSearchHistory.addEvent(that, 'click', that.searcHistory_click);
            that.render();
        },
        render: function () {
            var that = this,
            controls = that.getControls();

            that.LoadVisualizationHistory();

            console.log(controls.hdidRefSuscripcionHistory.val());
        },
        searcHistory_click: function (sender, args) {
            var that = this;
            that.LoadVisualizationHistory();
        },
        dataListClaroVideoHistory: {
            listaVisualizacionesCliente: [],

        },
        LoadVisualizationHistory: function () {

            var that = this,
            controls = this.getControls();
            var idRefSuscripcionHistory = controls.hdidRefSuscripcionHistory.val()
            var ListidRefSuscripcionHistory = idRefSuscripcionHistory.split("|");

            $('#btnSearchHistory').attr('data-loading-text', "<i class='fa fa-spinner fa-spin '></i> Cargando..");
            controls.btnSearchHistory.button('loading');

          

            if (ListidRefSuscripcionHistory != null) {
                if (ListidRefSuscripcionHistory.length > 0) {

                    var loadTableHistory = '';
                    loadTableHistory = loadTableHistory + '<tr>';
                    loadTableHistory = loadTableHistory + '<td colspan="7" style="text-align: center;"><div style="padding: 30px 30px 10px 30px;"><img src="/Images/loading.gif" height="45" width="45" /></div></td>';
                    loadTableHistory = loadTableHistory + '</tr>';
                    $('#tbVisualizationListBody').html(loadTableHistory);

                    that.dataListClaroVideoHistory.listaVisualizacionesCliente = [];

                    $.each(ListidRefSuscripcionHistory, function (index, value) {

                        that.SearchVisualization(value, function (flag) {

                            if (index == ListidRefSuscripcionHistory.length - 1) {
                                if (that.dataListClaroVideoHistory.listaVisualizacionesCliente.length > 0) {
                                    $('#tbVisualizationsList').find('tbody').html('');
                                    that.populateGridVisualizations(that.dataListClaroVideoHistory.listaVisualizacionesCliente);
                                    controls.btnSearchHistory.button('reset');
                                   
                                } else {
                                    $('#tbVisualizationsList').find('tbody').html('');
                                    that.populateGridVisualizations(null);
                                    controls.btnSearchHistory.button('reset');
                                    
                                }
                            }

                        });
                    });
                } else {
                    $('#tbVisualizationsList').find('tbody').html('');
                    that.populateGridVisualizations(null);
                    controls.btnSearchHistory.button('reset');
                 
                }
            } else {
                $('#tbVisualizationsList').find('tbody').html('');
                that.populateGridVisualizations(null);
                controls.btnSearchHistory.button('reset');
               
            }

            
        },

        SearchVisualization: function (idRefSuscripcion, callback) {

            var that = this,
                controls = this.getControls();

            var StrPartnerID = GetKeyConfig("strPartnerIDconsultarClienteSN");
            var CorrelatorId = that.GeneratedCorrelatorId();
            var providerCorrelatorId = StrPartnerID + '' + CorrelatorId;

            var StrKeyPersonalizado = GetKeyConfig("strKeyPersonalizado");
            var IdPersonalizado = that.GeneratedCorrelatorId();
            var CorrelatorIdPersonalizado = StrKeyPersonalizado + '' + IdPersonalizado;

            var strFechaInicio = "";
            var strFechaFin = "";
            var CustomerClaroVideo = controls.hdCustomerIDClaroVideo.val();

            var desde = controls.FechaDesde.val().trim();
            var hasta = controls.FechaHasta.val().trim();


            if (desde == "" || hasta == "") {

                var strFilterMesesBusqueda = GetKeyConfig("strFilterMonthsconsultarvisualizacionesHistory");
                var StrFilterDate = GetFilterDateMonth(strFilterMesesBusqueda, 1, "-", 1, function (Datos) {
                    strFechaInicio = Datos.desde;
                    strFechaFin = Datos.hasta;
                });

            } else {             
                
                var diaDesde = desde.substring(0, 2);
                var mesDesde = desde.substring(3, 5);
                var anioDesde = desde.substring(6, 10);

                var diaHasta = hasta.substring(0, 2);
                var mesHasta = hasta.substring(3, 5);
                var anioHasta = hasta.substring(6, 10);

                strFechaInicio = anioDesde + '-' + mesDesde + '-' + diaDesde + GetKeyConfig("strFilterFormatInicio");
                strFechaFin = anioHasta + '-' + mesHasta + '-' + diaHasta + GetKeyConfig("strFilterFormatFin");
               
            }


            var varqueryOttRequest = {
                invokeMethod: 'consultarvisualizacionessuscripcion',
                correlatorId: providerCorrelatorId,
                countryId: 'PE',
                employeeId: GetKeyConfig("strEmployeeId"),
                origin: 'SIAC',
                serviceName: 'queryOtt',
                providerId: StrPartnerID,
                startDate: strFechaInicio,
                endDate: strFechaFin,
                iccidManager: 'AMCO',
                extensionInfo: [
                    { key: "CUSTOMERID", value: CustomerClaroVideo },
                    { key: "idRefSuscripcion", value: idRefSuscripcion }
                ]
            };

            var objqueryOttRequest = {
                strIdSession: Session.IDSESSION,
                MessageRequest: {
                    Body: { queryOttRequest: varqueryOttRequest }
                }
            }

            console.log('Request Visualizacion');
            console.log(objqueryOttRequest);

            //$('#tbVisualizationsList').find('tbody').html('');

           

            // controls.btnbuscar.button('loading');
            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '/Transactions/Fixed/ClaroVideo/ConsultSN',
                data: JSON.stringify(objqueryOttRequest),
                complete: function () {
                 
                },
                success: function (response) {
                    console.log('Response ConsultSN - Lista de visualizaciones');
                    console.log(response.data);
                    if (response.data.queryOttResponse != null) {
                        if (response.data.queryOttResponse.resultCode == "0") { //cambiar a 0 solo para pruebas
                            if (response.data.queryOttResponse.visualizationsList.ListVisualizationUser != null) {
                                if (response.data.queryOttResponse.visualizationsList.ListVisualizationUser.length > 0) {
                                    $.each(response.data.queryOttResponse.visualizationsList.ListVisualizationUser, function (index, value) {

                                        var ListaVisualizationUser = {
                                            idContenido: "",
                                            titulo: "",
                                            ipUsuario: "",
                                            fechaTiempoVisualizacion: "",
                                            ultimoTiempoVisualizacion: "",
                                            fechaMaximoVisualizacion: "",
                                            tiempoMaximoVisualizacion: "",
                                            fechaTiempoVisualizacionConvert: ""
                                        }

                                        ListaVisualizationUser.idContenido = value.idContenido;
                                        ListaVisualizationUser.titulo = value.titulo;
                                        ListaVisualizationUser.ipUsuario = value.ipUsuario;
                                        ListaVisualizationUser.fechaTiempoVisualizacion = (value.fechaTiempoVisualizacion == 'null' ? "" : value.fechaTiempoVisualizacion);
                                        ListaVisualizationUser.fechaTiempoVisualizacionConvert = (value.fechaTiempoVisualizacion == 'null' ? "" : new Date(value.fechaTiempoVisualizacion));
                                        ListaVisualizationUser.ultimoTiempoVisualizacion = (value.ultimoTiempoVisualizacion == 'null' ? "" : value.ultimoTiempoVisualizacion);
                                        ListaVisualizationUser.fechaMaximoVisualizacion = (value.fechaMaximoVisualizacion == 'null' ? "" : value.fechaMaximoVisualizacion);
                                        ListaVisualizationUser.tiempoMaximoVisualizacion = (value.tiempoMaximoVisualizacion == 'null' ? "" : value.tiempoMaximoVisualizacion);


                                        that.dataListClaroVideoHistory.listaVisualizacionesCliente.push(ListaVisualizationUser);


                                    });
                                }

                            }

                        } else if (response.data.queryOttResponse.resultCode == GetKeyConfig("strResultCodePersonalizado")) {

                            var Mensaje = response.data.queryOttResponse.resultMessage;

                            that.getPersonalizaMensajeOTT(CorrelatorIdPersonalizado, Mensaje, function (flag) {
                                if (flag != '') {
                                    alert(flag, 'Alerta');
                                }
                            });
                        }
                    }

                    console.log(that.dataListClaroVideoHistory.listaVisualizacionesCliente);
                    callback(true);
                },
                error: function (msger) {
                    console.log('Error: Response ConsultSN - Lista de visualizaciones: ' + msger);
                    callback(false);
                }
            });
        },
        GeneratedCorrelatorId: function () {
            var that = this;

            var fecha = new Date();
            var yyyy = fecha.getFullYear().toString();
            var MM = that.pad(fecha.getMonth() + 1, 2);
            var dd = that.pad(fecha.getDate(), 2);
            var hh = that.pad(fecha.getHours(), 2);
            var mm = that.pad(fecha.getMinutes(), 2);
            var ss = that.pad(fecha.getSeconds(), 2);
            return yyyy + MM + dd + hh + mm + ss;
        },
        pad: function (number, length) {
            var str = '' + number;
            while (str.length < length) {
                str = '0' + str;
            }
            return str;
        },
        datatableVisualizationsList: {},
        populateGridVisualizations: function (Lista) {
            var that = this;
            var controls = that.getControls();

            var table = $('#tbVisualizationsList').DataTable();

            table.clear().draw();

            $('#tbVisualizationsList').DataTable({
                "scrollY": "100%",
                "scrollCollapse": true,
                "paging": true,
                "searching": true,
                "destroy": true,
                "scrollX": true,
                "sScrollX": "100%",
                "sScrollXInner": "100%",
                "autoWidth": true,
                "order": [[7, "desc"]],
                "lengthMenu": [[5, 15, 20, 25, -1], [5, 15, 20, 25, "Todos"]],
                data: Lista,
                columns: [

                   {
                       "data": "idContenido"
                   },
                    {
                        "data": "titulo"
                    },
                    {
                        "data": "ipUsuario"
                    },
                    {
                        "data": "fechaTiempoVisualizacion"
                    },
                    {
                        "data": "ultimoTiempoVisualizacion",
                        "render":
                       function (data, type, row, meta) {
                           if (type === 'display') {

                               data = '<p class="text-right">' + row.ultimoTiempoVisualizacion + '</p>';

                           }
                           return data;
                       }
                    },
                    {
                        "data": "fechaMaximoVisualizacion"
                    },
                    {
                        "data": "tiempoMaximoVisualizacion",
                        "render":
                        function (data, type, row, meta) {
                            if (type === 'display') {

                                data = '<p class="text-right">' + row.tiempoMaximoVisualizacion + '</p>';

                            }
                            return data;
                        }
                    },
                    {
                        "data": "fechaTiempoVisualizacionConvert",
                        "visible": false

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
    $.fn.HistoryVisualizationClient = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('HistoryVisualizationClient'),
                options = $.extend({}, $.fn.HistoryVisualizationClient.defaults,
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
    $.fn.HistoryVisualizationClient.defaults = {}
    $('#ContentHistoryVisualizationClient').HistoryVisualizationClient();

})(jQuery);