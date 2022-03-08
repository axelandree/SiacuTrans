﻿

(function ($, undefined) {



    var Form = function ($element, options) {

        $.extend(this, $.fn.PlansTable.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , tblResumen: $('#tblListadoV', $element)
        });

    }

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this;
            var controls = this.getControls();
            that.render();
            that.loadSessionData();
        },
        render: function () {
            var that = this;

            that.getLoadDetailVisualizeCall();
        },
        loadSessionData: function () {
            //Session.IDSESSION = JSON.parse(sessionStorage.idSession);
            //Session.DATACUSTOMER = JSON.parse(sessionStorage.dataCustomer);
            //Session.CUSTOMERPRODUCT = JSON.parse(sessionStorage.dataProduct);
            //Session.DATASERVICE = JSON.parse(sessionStorage.dataService);
            //Session.TELEPHONE = JSON.parse(sessionStorage.dataPhone);
        },
        getControls: function () {
            return this.m_controls || {};
        },
        setControls: function (value) {
            this.m_controls = value;
        },
        getLoadDetailVisualizeCall: function () {
            var controls = this.getControls();

            controls.tblResumen.DataTable({
                "pagingType": "full_numbers",
                "scrollY": "300px",
                "scrollCollapse": true,
                "paging": true,
                "pageLength": 50,
                "destroy": true,
                "searching": false,
                "language": {
                    "lengthMenu": "Mostrar _MENU_ registros por página.",
                    "zeroRecords": "No existen datos",
                    "info": " ",
                    "infoEmpty": " ",
                    "infoFiltered": "(filtered from _MAX_ total records)",
                    "oPaginate": {
                        "sFirst": "Primero",
                        "sPrevious": "Anterior",
                        "sNext": "Siguiente",
                        "sLast": "Último"
                    },
                    "emptyTable": "No existen datos"
                }
                , "columnDefs": [{
                    orderable: false,
                    className: 'select-radio',
                    targets: 0
                },
                {
                    targets: 3,
                    visible: false
                }],
                select: {
                    style: 'os',
                    info: false
                }
            })
            .on('select', function () {
                //console.log"se acaba de seleccionar");
                //controls.tblListadoV.DataTable().rows().deselect();
            });
        }
    };
    $.fn.PlansTable = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('PlansTable'),
                options = $.extend({}, $.fn.PlansTable.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('PlansTable', data);
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
    $.fn.PlansTable.defaults = {
    };

    $('#PlansTable').PlansTable();
})(jQuery);




