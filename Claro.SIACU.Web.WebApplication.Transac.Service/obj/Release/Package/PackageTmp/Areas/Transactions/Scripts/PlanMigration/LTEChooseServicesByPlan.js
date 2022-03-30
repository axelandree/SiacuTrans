(function ($, undefined) {
    var Form = function ($element, options) {

        $.extend(this, $.fn.FormSelectPlan.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
          , tblChooseServicesByPlanCable: $("#tblChooseServicesByPlanCable", $element)
          , tblChooseServicesByPlanInternet: $("#tblChooseServicesByPlanInternet", $element)
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
            that.loadSessionData();
            that.render();
            $("thead tr th").removeClass("select-checkbox");
        },
        render: function () {
            var that = this;
            that.getDataTable();

        },
        loadSessionData: function () {
            var that = this,
                controls = that.getControls(),
                objRequestDataModel = {};

            objRequestDataModel.strIdSession = Session.IDSESSION;
            objRequestDataModel.strIdContract = Session.DATACUSTOMER.ContractID;
            objRequestDataModel.strTypeProduct = Session.ProductType;
            objRequestDataModel.strIdPlan = Session.idPlan;

            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objRequestDataModel),
                url: '/Transactions/LTE/PlanMigration/LTEChooseServicesByPlanLoad',
                async: false,
                success: function (response) {
                    if (response.data != null) {
                        that.objLteMigrationPlanChoosePlanLoad = response.data;
                    }
                }
            });

            $.each(that.objLteMigrationPlanChoosePlanLoad.lstServicesByPlanCable, function (key, obj) {
                obj.CfWithIgv = Math.round((obj.CF * Session.dblIgvView) * 100) / 100;
            })

            $.each(that.objLteMigrationPlanChoosePlanLoad.lstServicesByPlanInternet, function (key, obj) {
                obj.CfWithIgv = Math.round((obj.CF * Session.dblIgvView) * 100) / 100;
            })

            $.each(that.objLteMigrationPlanChoosePlanLoad.lstServicesByPlanTelephone, function (key, obj) {
                obj.CfWithIgv = Math.round((obj.CF * Session.dblIgvView) * 100) / 100;
            })

            that.objSelectedServices = Session.objAditionalServices;
        },
        getDataTable: function () {
            var that = this,
                controls = that.getControls();
            controls.tblChooseServicesByPlanCable.DataTable({
                "pagingType": "full_numbers",
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
                "data": that.objLteMigrationPlanChoosePlanLoad.lstServicesByPlanCable,
                "columns": [
                    { orderable: false, "data": "", render: function (data) { return "&nbsp"; } },
                    { "data": "DesServSisact" },
                    { "data": "CfWithIgv" },
                    { "data": "CodServSisact" },
                    { "data": "ServiceType" },
                    { "data": "Sncode" },
                    { "data": "CodGroupServ" },
                    { "data": "Codtipequ" },
                    { "data": "Tipequ" },
                    { "data": "Spcode" },
                    { "data": "CantEquipment" },
                    { "data": "Equipment" },
                    { "data": "Tmcode" },
                    { "data": "CodPlanSisact" }
                ],
                "columnDefs": [
                 {
                     orderable: false,
                     className: 'select-checkbox',
                     targets: 0
                 },
                { visible: false, targets: 3, width: 0 },
                { visible: false, targets: 4, width: 0 },
                { visible: false, targets: 5, width: 0 },
                { visible: false, targets: 6, width: 0 },
                { visible: false, targets: 7, width: 0 },
                { visible: false, targets: 8, width: 0 },
                { visible: false, targets: 9, width: 0 },
                { visible: false, targets: 10, width: 0 },
                { visible: false, targets: 11, width: 0 },
                { visible: false, targets: 12, width: 0 },
                { visible: false, targets: 13, width: 0 }],
                "createdRow": function (row, data, dataIndex) {
                    if (typeof that.objSelectedServices !== 'undefined') {
                        $.each(that.objSelectedServices.lstCable, function (key, value) {
                            if (data.CodServSisact === value.CodServSisact) {
                                row.className = "selected";
                                row.onclick = function (e) {
                                    e.stopPropagation();
                                };
                            }
                        });
                    }

                },
                select: {
                    style: 'multi',
                    info: false
                }
            });

            controls.tblChooseServicesByPlanInternet.DataTable({
                "pagingType": "full_numbers",
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
                },
                "data": that.objLteMigrationPlanChoosePlanLoad.lstServicesByPlanInternet,
                "columns": [
                    { orderable: false, "data": "", render: function (data) { return "&nbsp"; } },
                    { "data": "DesServSisact" },
                    { "data": "CfWithIgv" },
                    { "data": "CodServSisact" },
                    { "data": "ServiceType" },
                    { "data": "Sncode" },
                    { "data": "CodGroupServ" },
                    { "data": "Codtipequ" },
                    { "data": "Tipequ" },
                    { "data": "Spcode" },
                    { "data": "CantEquipment" },
                    { "data": "Equipment" },
                    { "data": "Tmcode" },
                    { "data": "CodPlanSisact" }
                ],
                "columnDefs": [{
                    orderable: false,
                    className: 'select-checkbox',
                    targets: 0
                },
                { visible: false, targets: 3, width: 0 },
                { visible: false, targets: 4, width: 0 },
                { visible: false, targets: 5, width: 0 },
                { visible: false, targets: 6, width: 0 },
                { visible: false, targets: 7, width: 0 },
                { visible: false, targets: 8, width: 0 },
                { visible: false, targets: 9, width: 0 },
                { visible: false, targets: 10, width: 0 },
                { visible: false, targets: 11, width: 0 },
                { visible: false, targets: 12, width: 0 },
                { visible: false, targets: 13, width: 0 }],
                "createdRow": function (row, data, dataIndex) {
                    if (typeof that.objSelectedServices !== 'undefined') {
                        $.each(that.objSelectedServices.lstInternet, function (key, value) {
                            if (data.CodServSisact === value.CodServSisact) {
                                row.className = "selected";
                                row.onclick = function (e) {
                                    e.stopPropagation();
                                };
                            }
                        });
                    }

                },
                select: {
                    style: 'multi',
                    info: false
                }
            }).on('select', function () {
            });
            controls.tblChooseServicesByPlanPhone.DataTable({
                "pagingType": "full_numbers",
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
                },
                "data": that.objLteMigrationPlanChoosePlanLoad.lstServicesByPlanTelephone,
                "columns": [
                    { orderable: false, "data": "", render: function (data) { return "&nbsp"; } },
                    { "data": "DesServSisact" },
                    { "data": "CfWithIgv" },
                    { "data": "CodServSisact" },
                    { "data": "ServiceType" },
                    { "data": "Sncode" },
                    { "data": "CodGroupServ" },
                    { "data": "Codtipequ" },
                    { "data": "Tipequ" },
                    { "data": "Spcode" },
                    { "data": "CantEquipment" },
                    { "data": "Equipment" },
                    { "data": "Tmcode" },
                    { "data": "CodPlanSisact" }
                ],
                "columnDefs": [{
                    orderable: false,
                    className: 'select-checkbox',
                    targets: 0
                },
                { visible: false, targets: 3, width: 0 },
                { visible: false, targets: 4, width: 0 },
                { visible: false, targets: 5, width: 0 },
                { visible: false, targets: 6, width: 0 },
                { visible: false, targets: 7, width: 0 },
                { visible: false, targets: 8, width: 0 },
                { visible: false, targets: 9, width: 0 },
                { visible: false, targets: 10, width: 0 },
                { visible: false, targets: 11, width: 0 },
                { visible: false, targets: 12, width: 0 },
                { visible: false, targets: 13, width: 0 }],
                "createdRow": function (row, data, dataIndex) {
                    if (typeof that.objSelectedServices !== 'undefined') {
                        $.each(that.objSelectedServices.lstPhone, function (key, value) {
                            if (data.CodServSisact === value.CodServSisact) {
                                row.className = "selected";
                                row.onclick = function (e) {
                                    e.stopPropagation();
                                };
                            }
                        });
                    }

                },
                select: {
                    style: 'multi',
                    info: false
                }
            })
            .on('select', function () {
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
        strUrlLogo: window.location.protocol + '//' + window.location.host + '/Images/loading_3.gif',
        objSelectedServices: {}
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

