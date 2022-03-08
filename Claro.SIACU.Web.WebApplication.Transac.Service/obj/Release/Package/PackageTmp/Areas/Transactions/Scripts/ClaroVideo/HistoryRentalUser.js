var SessionTransac = JSON.parse(sessionStorage.getItem("SessionTransac"));
console.log(SessionTransac);

(function ($, undefined) {
    'use strict';
    var Form = function ($element, options) {
        $.extend(this, $.fn.HistoryRentalUser.defaults, $element.data(), typeof options == 'object' && options);
        this.setControls({
            form: $element,
            FechaDesde: $('#FechaDesde', $element),
            FechaHasta: $('#FechaHasta', $element),
            btnBuscarAlquiler: $('#btnBuscarAlquiler', $element),
            hdCustomerIDClaroVideo: $('#hdCustomerIDClaroVideo', $element)

        });
    };
    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this,
            controls = that.getControls();
            controls.FechaDesde.datepicker({ format: 'dd/mm/yyyy', endDate: "current" });
            controls.FechaHasta.datepicker({ format: 'dd/mm/yyyy', endDate: "current" });
            controls.btnBuscarAlquiler.addEvent(that, 'click', that.searchRental_click);

            that.render();
        },
        render: function () {
            var that = this,
            controls = that.getControls();
            that.LoadListRentas();

        },
        searchRental_click: function (sender, args) {
            var that = this,
           controls = that.getControls();
            that.LoadListRentas();          

        },
        LoadListRentas: function () {
            var that = this;
            that.dataListClaroVideoHistory.listaVisualizacionesRentas = [];
            that.searhRentalUser(function (flag) {
                if (flag) {
                    if (that.dataListClaroVideoHistory.listaVisualizacionesRentas.length > 0) {
                        $('#tbHistoryRentalUser').find('tbody').html('');
                        console.log(that.dataListClaroVideoHistory.listaVisualizacionesRentas);
                        that.populateGridRentalUser(that.dataListClaroVideoHistory.listaVisualizacionesRentas);
                    } else {
                        $('#tbHistoryRentalUser').find('tbody').html('');
                        that.populateGridRentalUser(null);
                    }
                }
            });
        },
        dataListClaroVideoHistory: {
            listaVisualizacionesRentas: [],
        },
        populateGridRentalUser: function (Lista) {
            var that = this;
            var controls = that.getControls();

            var table = $('#tbHistoryRentalUser').DataTable();
            table.clear().draw();

            $('#tbHistoryRentalUser').DataTable({
                "scrollY": "100%",
                "scrollCollapse": true,
                "paging": true,
                "searching": true,
                "destroy": true,
                "scrollX": true,
                "sScrollX": "100%",
                "sScrollXInner": "100%",
                "autoWidth": true,
                "order": [[5, "desc"]],
                "lengthMenu": [[10, 15, 20, 25, -1], [10, 15, 20, 25, "Todos"]],
                data: Lista,
                columns: [
                    {
                        "data": "descripcion"
                    },
                    {
                        "data": "ipUsuario"
                    },
                    {
                        "data": "ultimaVisualizacion"
                    },
                    {
                        "data": "tiempoMaximoVisualizacion"
                    },
                    {
                        "data": "fechaAlta"
                    },
                    {
                        "data": "fechaExpiracion"
                    },
                    {
                        "data": "precio"
                    },
                    {
                        "data": "medioPago"
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
        searhRentalUser: function (callback) {
            var that = this,
            controls = this.getControls();

            var loadTable = '';
            loadTable = loadTable + '<tr>';
            loadTable = loadTable + '<td colspan="8" style="text-align: center;"><div style="padding: 30px 30px 10px 30px;"><img src="/Images/loading.gif" height="45" width="45" /></div></td>';
            loadTable = loadTable + '</tr>';
            $('#tbHistoryRentalUserBody').html(loadTable);

            var StrPartnerID = GetKeyConfig("strPartnerIDconsultarClienteSN");
            var CorrelatorId = that.GeneratedCorrelatorId();
            var providerCorrelatorId = StrPartnerID + '' + CorrelatorId;

            var StrKeyPersonalizado = GetKeyConfig("strKeyPersonalizado");
            var IdPersonalizado = that.GeneratedCorrelatorId();
            var CorrelatorIdPersonalizado = StrKeyPersonalizado + '' + IdPersonalizado;

            var strFechaInicio = "";
            var strFechaFin = "";

            var desde = controls.FechaDesde.val().trim();
            var hasta = controls.FechaHasta.val().trim();

            var CustomerClaroVideo = controls.hdCustomerIDClaroVideo.val();

            if (desde == "" || hasta == "") {

                var strFilterMesesBusqueda = GetKeyConfig("strFilterMonthsconsultarrentasclienteHistory");
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
                invokeMethod: 'consultarrentascliente',
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
                    { key: "CUSTOMERID", value: CustomerClaroVideo }
                ]
            };

            var objqueryOttRequest = {
                strIdSession: Session.IDSESSION,
                MessageRequest: {
                    Body: { queryOttRequest: varqueryOttRequest }
                }
            };

            console.log('Request ConsultSN - Lista de historial de rentas');
            console.log(objqueryOttRequest);

            // $('#tbRentalUser').find('tbody').html('');
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
                    console.log('Response ConsultSN - Lista de historial de rentas')
                    console.log(response.data);
                    if (response.data.queryOttResponse != null) {
                        if (response.data.queryOttResponse.resultCode == "0") { //cambiar a 0 solo para pruebas
                            if (response.data.queryOttResponse.rentList.ListRentalUser != null) {
                                if (response.data.queryOttResponse.rentList.ListRentalUser.length > 0) {

                                    $.each(response.data.queryOttResponse.rentList.ListRentalUser, function (index, value) {

                                        var ListRentalUser = {
                                            descripcion: "",
                                            fechaAlta: "",
                                            fechaExpiracion: "",
                                            idRefRenta: "",
                                            idRenta: "",
                                            ipUsuario: "",
                                            medioPago: "",
                                            precio: "",
                                            tiempoMaximoVisualizacion: "",
                                            ultimaVisualizacion: ""
                                        }

                                        ListRentalUser.descripcion = value.descripcion;
                                        ListRentalUser.fechaAlta = value.fechaAlta;
                                        ListRentalUser.fechaExpiracion = value.fechaExpiracion;
                                        ListRentalUser.idRefRenta = value.idRefRenta;
                                        ListRentalUser.idRenta = value.idRenta;
                                        ListRentalUser.ipUsuario = (value.ipUsuario == 'Null' ? "" : value.ipUsuario);
                                        ListRentalUser.medioPago = value.medioPago;
                                        ListRentalUser.moneda = value.moneda;
                                        ListRentalUser.precio = value.precio;
                                        ListRentalUser.tiempoMaximoVisualizacion = (value.tiempoMaximoVisualizacion == 'Null' ? "" : value.tiempoMaximoVisualizacion);
                                        ListRentalUser.ultimaVisualizacion = (value.ultimaVisualizacion == 'Null' ? "" : value.ultimaVisualizacion);

                                        that.dataListClaroVideoHistory.listaVisualizacionesRentas.push(ListRentalUser);
                                    });


                                    // that.populateGridRentalUser(response.data.queryOttResponse.rentList.ListRentalUser);
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
                    console.log('Lista de rentas a cargar');
                    console.log(that.dataListClaroVideoHistory.listaVisualizacionesRentas);
                    callback(true);

                },
                error: function (msger) {
                    console.log('Error: Response ConsultSN - Lista de rentas : ' + msger);
                    callback(true);
                }
            });
        },
        datatableRentalUser: {},
        getControls: function () {
            return this.m_controls || {};
        },
        setControls: function (value) {
            this.m_controls = value;
        },
        strUrl: window.location.protocol + '//' + window.location.host,
    };
    $.fn.HistoryRentalUser = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('HistoryRentalUser'),
                options = $.extend({}, $.fn.HistoryRentalUser.defaults,
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
    $.fn.HistoryRentalUser.defaults = {}
    $('#ContentHistoryRentalUser').HistoryRentalUser();

})(jQuery);