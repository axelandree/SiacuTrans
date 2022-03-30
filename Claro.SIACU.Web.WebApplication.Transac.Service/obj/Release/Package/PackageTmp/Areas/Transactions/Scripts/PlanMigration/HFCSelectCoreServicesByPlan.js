(function ($, undefined) {



    var Form = function ($element, options) {

        $.extend(this, $.fn.FormSelectPlan.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
          , tblChooseServicesByPlanCable: $("#tblChooseCoreServicesByPlanCable", $element)
          , tblChooseServicesByPlanInternet: $("#tblChooseCoreServicesByPlanInternet", $element)
          , tblChooseServicesByPlanPhone: $("#tblChooseCoreServicesByPlanPhone", $element)
        });

    };

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this;
            var controls = this.getControls();

            that.getDataTable();
            $("thead tr th").removeClass("select-checkbox");
        },
        render: function () {
            var that = this;

        },
        getDataTable: function () {
            var controls = this.getControls();
            controls.tblChooseServicesByPlanCable.DataTable({
                "pagingType": "full_numbers",
                //"scrollY": "300px",
                "scrollCollapse": false,
                "paging": false,
                "pageLength": 10,
                "sort": false,
                "destroy": true,
                "searching": false,
                "language": {
                    "lengthMenu": "Display _MENU_ records per page",
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
                },
                "columnDefs": [
                {
                    orderable: false,
                    className: 'select-radio',
                    targets: 0
                },
                {
                    visible: false,
                    targets: 2,
                    width: 0
                },
                {
                    visible: false,
                    targets: 3,
                    width: 0
                },
                {
                    visible: false,
                    targets: 4,
                    width:0

                },
                {
                    visible: false,
                    targets: 5,
                    width: 0

                },
                {
                    visible: false,
                    targets: 6,
                    width: 0

                },
                {
                    visible: false,
                    targets: 7,
                    width: 0

                },
                {
                    visible: false,
                    targets: 8,
                    width: 0

                },
                {
                    visible: false,
                    targets: 9,
                    width: 0

                },
                {
                    visible: false,
                    targets: 10,
                    width: 0

                },
                {
                    visible: false,
                    targets: 11,
                    width: 0

                },
                {
                    visible: false,
                    targets: 12,
                    width: 0

                },
                {
                    visible: false,
                    targets: 13,
                    width: 0

                },
                {
                    visible: false,
                    targets: 14,
                    width: 0

                },
                {
                    visible: false,
                    targets: 15,
                    width: 0

                },
                {
                    visible: false,
                    targets: 16,
                    width: 0

                }],
                select: {
                    style: 'os',
                    info: false
                }
            });

            controls.tblChooseServicesByPlanInternet.DataTable({
                "pagingType": "full_numbers",
                //"scrollY": "300px",
                "scrollCollapse": false,
                "paging": false,
                "pageLength": 50,
                "sort": false,
                "destroy": true,
                "searching": false,
                "language": {
                    "lengthMenu": "Display _MENU_ records per page",
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
                , "columnDefs": [
                {
                    orderable: false,
                    className: 'select-radio',
                    targets: 0
                },
                {
                    visible: false,
                    targets: 2,
                    width: 0
                },
                {
                    visible: false,
                    targets: 3
                },
                {
                    visible: false,
                    targets: 4
                },
                {
                    visible: false,
                    targets: 5

                },
                {
                    visible: false,
                    targets: 6,
                    width: 0

                },
                {
                    visible: false,
                    targets: 7,
                    width: 0

                },
                {
                    visible: false,
                    targets: 8,
                    width: 0

                },
                {
                    visible: false,
                    targets: 9,
                    width: 0

                },
                {
                    visible: false,
                    targets: 10,
                    width: 0

                },
                {
                    visible: false,
                    targets: 11,
                    width: 0

                },
                {
                    visible: false,
                    targets: 12,
                    width: 0

                },
                {
                    visible: false,
                    targets: 13,
                    width: 0

                },
                {
                    visible: false,
                    targets: 14,
                    width: 0

                },
                {
                    visible: false,
                    targets: 15,
                    width: 0

                },
                {
                    visible: false,
                    targets: 16,
                    width: 0

                }],
                select: {
                    style: 'os',
                    info: false
                }
            }).on('select', function () {
                //console.log"se acaba de seleccionar");
            });
            controls.tblChooseServicesByPlanPhone.DataTable({
                "pagingType": "full_numbers",
                //"scrollY": "300px",
                "scrollCollapse": false,
                "paging": false,
                "pageLength": 50,
                "sort": false,
                "destroy": true,
                "searching": false,
                "language": {
                    "lengthMenu": "Display _MENU_ records per page",
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
                    visible: false,
                    targets: 2,
                    width: 0
                },
                {
                    visible: false,
                    targets: 3
                },
                {
                    visible: false,
                    targets: 4

                },
                {
                    visible: false,
                    targets: 5

                },
                {
                    visible: false,
                    targets: 6,
                    width: 0

                },
                {
                    visible: false,
                    targets: 7,
                    width: 0

                },
                {
                    visible: false,
                    targets: 8,
                    width: 0

                },
                {
                    visible: false,
                    targets: 9,
                    width: 0

                },
                {
                    visible: false,
                    targets: 10,
                    width: 0

                },
                {
                    visible: false,
                    targets: 11,
                    width: 0

                },
                {
                    visible: false,
                    targets: 12,
                    width: 0

                },
                {
                    visible: false,
                    targets: 13,
                    width: 0

                },
                {
                    visible: false,
                    targets: 14,
                    width: 0

                },
                {
                    visible: false,
                    targets: 15,
                    width: 0

                },
                {
                    visible: false,
                    targets: 16,
                    width: 0

                }],
                select: {
                    style: 'os',
                    info: false
                }
            })
            .on('select', function () {
                //console.log"se acaba de seleccionar");
            });
        },
        getControls: function () {
            return this.m_controls || {};
        },
        setControls: function (value) {
            this.m_controls = value;
        },
        getLoadDetailVisualizeCall: function () {
            var controls = this.getControls();

        }
    };
    $.fn.FormSelectPlan = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('FormSelectPlan'),
                options = $.extend({}, $.fn.FormSelectPlan.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('FormSelectPlan', data);
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
    $.fn.FormSelectPlan.defaults = {
    }
    $('#SelectCoreServicesByPlan', $('.modal:last')).FormSelectPlan();
})(jQuery);

