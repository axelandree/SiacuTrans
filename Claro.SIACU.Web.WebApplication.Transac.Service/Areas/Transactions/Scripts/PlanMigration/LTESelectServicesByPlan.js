(function ($, undefined) {



    var Form = function ($element, options) {

        $.extend(this, $.fn.FormSelectPlan.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
          , tblChooseServicesByPlanCable: $("#tblChooseServicesByPlanCable",$element)
          , tblChooseServicesByPlanInternet: $("#tblChooseServicesByPlanInternet",$element)
          , tblChooseServicesByPlanPhone: $("#tblChooseServicesByPlanPhone", $element)
        });

    };

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this;
            var controls = this.getControls();

            $.blockUI({
                message: '<div align="center"><img src="' + that.strUrlLogo + '" width="25" height="25" /> Cargando ... </div>',
                css: {
                    border: 'none',
                    padding: '15px',
                    backgroundColor: '#000',
                    '-webkit-border-radius': '10px',
                    '-moz-border-radius': '10px',
                    opacity: .5,
                    color: '#fff',
                }
            });
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
                "columnDefs": [{
        orderable: false,
        className: 'select-checkbox',
                    targets: 0
    },
                {
                    visible: false,
                    targets: 3,
                    width:0
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

                }],
                select: {
                    style: 'multi',
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
                , "columnDefs": [{
                    orderable: false,
                    className: 'select-checkbox',
                    targets: 0
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

                }],
                select: {
                    style: 'multi',
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
                    className: 'select-checkbox',
                    targets: 0
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

                }
                ],
                select: {
                    style: 'multi',
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

        },
        strUrlLogo: window.location.protocol + '//' + window.location.host + '/Images/loading2.gif'
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
    $('#SelectServicesByPlan', $('.modal:last')).FormSelectPlan();
})(jQuery);

