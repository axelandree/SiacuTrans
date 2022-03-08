(function ($, undefined) {

    var Form = function ($element, options) {

        $.extend(this, $.fn.FormChooseCoreServicesByPlan.defaults, $element.data(), typeof options === 'object' && options);

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
            that.LTEPlanMigrationChooseCoreServicesByPlanLoad();
            that.render();
        },
        render: function () {
            var that = this,
            controls = that.getControls();
                that.loadServicesCoreByCable();
                that.loadServicesCoreByInternet();
                that.loadServicesCoreByTelephone();
                if (that.objLteMigrationPlanChooseCoreServicesByPlanLoad.lstServicesByPlanCable != null)
                    $('#tblChooseCoreServicesByPlanCable tr').find('input:radio:first').click();

                if (that.objLteMigrationPlanChooseCoreServicesByPlanLoad.lstServicesByPlanInternet != null)
                    $('#tblChooseCoreServicesByPlanInternet tr').find('input:radio:first').click();

                if (that.objLteMigrationPlanChooseCoreServicesByPlanLoad.lstServicesByPlanTelephone != null)
                    $('#tblChooseCoreServicesByPlanPhone tr').find('input:radio:first').click();

        },
        loadServicesCoreByCable:function() {
            var that = this,
                controls = this.getControls();
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
                "data": that.objLteMigrationPlanChooseCoreServicesByPlanLoad.lstServicesByPlanCable,
                "columns": [
                    { orderable: false, "data": "", render: function (data) { return "<input type='radio' style='display:none' />"; } },
                    { "data": "DesServSisact" },
                    { "data": "CfWithIgv" },
                    { "data": "CodServSisact" },
                    { "data": "ServiceType" },
                    { "data": "Equipment" },
                    { "data": "Sncode" },
                    { "data": "Spcode" },
                    { "data": "CantEquipment" },
                    { "data": "CodGroupServ" },
                    { "data": "Codtipequ" },
                    { "data": "GroupServ" },
                    { "data": "CodServiceType" },
                    { "data": "Tipequ" },
                    { "data": "Tmcode" },
                    { "data": "IDEquipment" }
                ],
                "columnDefs": [
                    { orderable: false, className: 'select-radio', targets: 0 },
                    
                    { visible: false, targets: 3},
                    { visible: false, targets: 4, width: 0},
                    { visible: false, targets: 5, width: 0 },
                    { visible: false, targets: 6, width: 0 },
                    { visible: false, targets: 7, width: 0 },
                    { visible: false, targets: 8, width: 0 },
                    { visible: false, targets: 9, width: 0 },
                    { visible: false, targets: 10, width: 0 },
                    { visible: false, targets: 11, width: 0 },
                    { visible: false, targets: 12, width: 0 },
                    { visible: false, targets: 13, width: 0 },
                    { visible: false, targets: 14, width: 0 },
                    { visible: false, targets: 15, width: 0 }
                  ],
                select: {
                    style: 'os',
                    info: false
                }
            });
        },
        loadServicesCoreByInternet: function () {
            var that = this,
                controls = this.getControls();
            controls.tblChooseServicesByPlanInternet.DataTable({
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
                "data": that.objLteMigrationPlanChooseCoreServicesByPlanLoad.lstServicesByPlanInternet,
                "columns": [
                    { orderable: false, "data": "", render: function (data) { return "<input type='radio' style='display:none' />"; } },

                    { "data": "DesServSisact" },
                    { "data": "CfWithIgv" },
                    { "data": "CodServSisact" },
                    { "data": "ServiceType" },
                    { "data": "Equipment" },
                    { "data": "Sncode" },
                    { "data": "Spcode" },
                    { "data": "CantEquipment" },
                    { "data": "CodGroupServ" },
                    { "data": "Codtipequ" },
                    { "data": "GroupServ" },
                    { "data": "CodServiceType" },
                    { "data": "Tipequ" },
                    { "data": "Tmcode" },
                    { "data": "IDEquipment" }
                ],
                "columnDefs": [
                    { orderable: false, className: 'select-radio', targets: 0 },
                    { visible: false, targets: 3 },
                    { visible: false, targets: 4, width: 0 },
                    { visible: false, targets: 5, width: 0 },
                    { visible: false, targets: 6, width: 0 },
                    { visible: false, targets: 7, width: 0 },
                    { visible: false, targets: 8, width: 0 },
                    { visible: false, targets: 9, width: 0 },
                    { visible: false, targets: 10, width: 0 },
                    { visible: false, targets: 11, width: 0 },
                    { visible: false, targets: 12, width: 0 },
                    { visible: false, targets: 13, width: 0 },
                    { visible: false, targets: 14, width: 0 },
                    { visible: false, targets: 15, width: 0 }
                ],
                select: {
                    style: 'os',
                    info: false
                }
            });

        },
        loadServicesCoreByTelephone: function () {
            var that = this,
                controls = this.getControls();
            controls.tblChooseServicesByPlanPhone.DataTable({
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
                "data": that.objLteMigrationPlanChooseCoreServicesByPlanLoad.lstServicesByPlanTelephone,
                "columns": [
                    { orderable: false, "data": "", render: function (data) { return "<input type='radio' style='display:none' />"; } },
                    { "data": "DesServSisact" },
                    { "data": "CfWithIgv" },
                    { "data": "CodServSisact" },
                    { "data": "ServiceType" },
                    { "data": "Equipment" },
                    { "data": "Sncode" },
                    { "data": "Spcode" },
                    { "data": "CantEquipment" },
                    { "data": "CodGroupServ" },
                    { "data": "Codtipequ" },
                    { "data": "GroupServ" },
                    { "data": "CodServiceType" },
                    { "data": "Tipequ" },
                    { "data": "Tmcode" },
                    { "data": "IDEquipment" }
                ],
                "columnDefs": [
                    { orderable: false, className: 'select-radio', targets: 0 },
                    { visible: false, targets: 3 },
                    { visible: false, targets: 4, width: 0 },
                    { visible: false, targets: 5, width: 0 },
                    { visible: false, targets: 6, width: 0 },
                    { visible: false, targets: 7, width: 0 },
                    { visible: false, targets: 8, width: 0 },
                    { visible: false, targets: 9, width: 0 },
                    { visible: false, targets: 10, width: 0 },
                    { visible: false, targets: 11, width: 0 },
                    { visible: false, targets: 12, width: 0 },
                    { visible: false, targets: 13, width: 0 },
                    { visible: false, targets: 14, width: 0 },
                    { visible: false, targets: 15, width: 0 }
                ],
                select: {
                    style: 'os',
                    info: false
                }
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
        LTEPlanMigrationChooseCoreServicesByPlanLoad: function() {
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
                url: '/Transactions/LTE/PlanMigration/LTEChooseCoreServicesByPlanLoad',
                async: false,
                success: function(response) {
                    if (response.data != null) {
                        that.objLteMigrationPlanChooseCoreServicesByPlanLoad = response.data;
                    }
                }
            });

            $.each(that.objLteMigrationPlanChooseCoreServicesByPlanLoad.lstServicesByPlanCable, function (key, obj) {
                obj.CfWithIgv = Math.round((obj.CF * Session.dblIgvView) * 100) / 100;
            })

            $.each(that.objLteMigrationPlanChooseCoreServicesByPlanLoad.lstServicesByPlanInternet, function (key, obj) {
                obj.CfWithIgv = Math.round((obj.CF * Session.dblIgvView) * 100) / 100;
            })

            $.each(that.objLteMigrationPlanChooseCoreServicesByPlanLoad.lstServicesByPlanTelephone, function (key, obj) {
                obj.CfWithIgv = Math.round((obj.CF * Session.dblIgvView) * 100) / 100;
            })
        },
        strUrlLogo: window.location.protocol + '//' + window.location.host + '/Images/loading2.gif',
        objLteMigrationPlanChooseCoreServicesByPlanLoad: {}
    };
    $.fn.FormChooseCoreServicesByPlan = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('FormChooseCoreServicesByPlan'),
                options = $.extend({}, $.fn.FormChooseCoreServicesByPlan.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('FormChooseCoreServicesByPlan', data);
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
    $.fn.FormChooseCoreServicesByPlan.defaults = {
    }
    $('#ChooseCoreServicesByPlan', $('.modal:last')).FormChooseCoreServicesByPlan();
})(jQuery);

