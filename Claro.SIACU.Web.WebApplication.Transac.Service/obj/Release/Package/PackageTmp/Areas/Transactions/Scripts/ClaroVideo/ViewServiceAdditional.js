
console.log(SessionTransac);

(function ($, undefined) {
    'use strict';
    var Form = function ($element, options) {
        $.extend(this, $.fn.ViewServiceAdditional.defaults, $element.data(), typeof options == 'object' && options);
        this.setControls({
            form: $element          

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
            console.log('populateGridServiceAdditional');
            that.populateGridServiceAdditional();

        },                    
        populateGridServiceAdditional: function () {

            var that = this;
            var controls = that.getControls();
            $('#tbServiceAdditional').find('tbody').html('');

            if (($.isEmptyObject(SessionTransac.ListServiciosAdicionalesFija)) == false) {
                if (SessionTransac.ListServiciosAdicionalesFija != "" && SessionTransac.ListServiciosAdicionalesFija != null) {
                    console.log('ListServiciosAdicionalesFija');
                    console.log(SessionTransac.ListServiciosAdicionalesFija);

                    var table = $('#tbServiceAdditional').DataTable();
                        table.clear().draw();

                        $('#tbServiceAdditional').DataTable({
                            "scrollY": "100%",
                            "scrollCollapse": true,
                            "paging": false,
                            "searching": false,
                            "destroy": true,
                            "scrollX": true,
                            "sScrollX": "100%",
                            "sScrollXInner": "100%",
                            "autoWidth": true,
                            //"order": [[1, "desc"]],
                            "lengthMenu": [[10, 15, 20, 25, -1], [10, 15, 20, 25, "Todos"]],
                            data: SessionTransac.ListServiciosAdicionalesFija,
                            columns: [
                                {
                                    "data": "ServiceDescription"
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
                }
            }

       

           

        },
        getControls: function () {
            return this.m_controls || {};
        },
        setControls: function (value) {
            this.m_controls = value;
        },
        strUrl: window.location.protocol + '//' + window.location.host,
    };
    $.fn.ViewServiceAdditional = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('ViewServiceAdditional'),
                options = $.extend({}, $.fn.ViewServiceAdditional.defaults,
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
    $.fn.ViewServiceAdditional.defaults = {}
    $('#ContentViewServiceAdditional').ViewServiceAdditional();

})(jQuery);