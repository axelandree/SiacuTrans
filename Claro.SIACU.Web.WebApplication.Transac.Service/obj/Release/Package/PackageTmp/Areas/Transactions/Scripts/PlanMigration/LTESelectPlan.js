(function ($, undefined) {



    var Form = function ($element, options) {

        $.extend(this, $.fn.FormSelectPlan.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
          , tblListadoV: $("#tblListadoV", $element)
          , btnBuscarDesc: $("#btnBuscarDesc", $element)
          , btnBuscarCamp: $("#btnBuscarCamp", $element)
          , btnBuscarSol: $("#btnBuscarSol", $element)
          , txtSearchByDesc: $("#txtSearchByDesc", $element)
          , ddlSearchByCampaign: $("#ddlSearchByCampaign", $element)
          , ddlSearchBySolution: $("#ddlSearchBySolution", $element)
          , tblListadoVBody: $("#tblListadoVBody", $element)
          , ddlSearchOptions: $("#ddlSearchOptions",$element)
        });

    };

    Form.prototype = {
        constructor: Form,
        init: function () {
            SessionPMLTE.DefaultOption = "TODOS";
            SessionPMLTE.SelectedOption = "TODOS";

            var that = this,
                controls = this.getControls();
            
            controls.txtSearchByDesc.keyup(function (event) {
                if (event.keyCode == 13) {
                    that.searchNewPlan();
                }
                return false;
            });
            controls.btnBuscarDesc.addEvent(that, 'click', that.btnBuscarDesc_Click);
            controls.btnBuscarCamp.addEvent(that, 'click', that.btnBuscarCamp_Click);
            controls.btnBuscarSol.addEvent(that, 'click', that.btnBuscarSol_Click);
            
            controls.txtSearchByDesc.focus();
            that.GetPlanSearchOptions();
            that.getDataTable();
        },
        render: function () {
            var that = this;
           
        },
        getDataTable: function () {
            var that = this,
                controls = this.getControls();
            that.searchData = controls.tblListadoV.dataTable({
                "pagingType": "full_numbers",
                "scrollY": "200px",
                "processing": true,
                "serverSide": false,
                "scrollCollapse": true,
                "paging": true,
                "pageLength": 50,
                "destroy": true,
                "searching": false,
                "language": {
                    "lengthMenu": "Mostrar _MENU_ registros por página.",
                    "zeroRecords": "No existen datos",
                    "processing": "<img src=" + that.strUrlLogo + " width='25' height='25' /> Cargando ... </div>",
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

                "columns": [
                    { "data": "", render: function (data) { return "&nbsp"; } },
                    { "data": "strDesPlanSisact" },
                    { "data": "strSolucion" },
                    { "data": "strStatus" },
                    { "data": "strCodPlanSisact" }
                ],
                "columnDefs": [{
                    //className: 'select-radio',
                    //"orderable": false,
                    //"sort": false,
                    //"targets": 0
                    targets: 0,
                    className: 'select-radio',
                    defaultContent: "",
                    orderable: false,
                    sort: false,
                    bSortable: false
                    //$("thead tr th").removeClass("select-checkbox");
                },
                {
                    targets: 3,
                    render: function (data, type, row) {
                        return data == '1' ? 'SI' : 'NO'
                    },

                },
                {
                    targets: 4,
                    visible:false
                }],
                "ajax": {
                    "url": "/Transactions/LTE/PlanMigration/SearchPlans",
                    "data": function (d) {
                        d.strIdSession = SessionPMLTE.IDSESSION;
                        d.strCampaign = controls.ddlSearchByCampaign.val();
                        d.strSolution = controls.ddlSearchBySolution.val();
                        d.tbuscar = controls.txtSearchByDesc.val();
                        d.strProductType = SessionPMLTE.ProductType;
                        d.strSearchOption = SessionPMLTE.SelectedOption;
                    },
                    "type": "post"
                },
                select: {
                    style: 'os',
                    info: false
                }
            })
            .on('select', function () {
                //console.log"se acaba de seleccionar");
            });
            $("select1").removeClass("sorting_asc");
        },
        GetPlanSearchOptions: function () {

            var that = this;
            var controls = that.getControls();
            //SessionPMLTE.USERACCESS.optionPermissions = SessionPMLTE.USERACCESS.optionPermissions + "HFC_VN,HFC_VT,LTE_VN,LTE_VT,";

            $.ajax({
                type: "POST",
                url: that.strUrl + '/Transactions/LTE/PlanMigration/GetPlanSearchOptions',
                data: {
                    strIdSession: SessionPMLTE.IDSESSION,
                    strUserAccess: SessionPMLTE.USERACCESS.optionPermissions,
                    product: SessionPMLTE.UrlParams.SUREDIRECT
                },
                error: function (xhr, status, error) {
                    //console.logxhr);
                },
                success: function (data) {
                    var list = data.data;
                    $.each(list, function (index, item) {
                        $("#ddlSearchOptions").append($("<option>", { value: item.Code, html: item.Description }));
                    });
                }
            });
        },
        getControls: function () {
            return this.m_controls || {};
        },
        setControls: function (value) {
            this.m_controls = value;
        },
        btnBuscarDesc_Click: function () {
            var that = this,
                controls = this.getControls();

            controls.ddlSearchByCampaign.val('0');
            controls.ddlSearchBySolution.val('0');

            that.searchNewPlan();
        },
        btnBuscarCamp_Click: function(){
            var that = this,
                controls = this.getControls();

            controls.txtSearchByDesc.val('');
            controls.ddlSearchBySolution.val('0');

            that.searchNewPlan();
        },
        btnBuscarSol_Click: function(){
            var that = this,
                controls = this.getControls();

            controls.txtSearchByDesc.val('');
            controls.ddlSearchByCampaign.val('0');

            that.searchNewPlan();
        },
        searchNewPlan: function () {
            var that = this,
                controls = this.getControls();
            SessionPMLTE.SelectedOption = $("#ddlSearchOptions option:selected").text();
            //that.searchData.DataTable({
            //    ajax: {
            //        "url": "/Transactions/LTE/PlanMigration/SearchPlans",
            //        "data": function (d) {
            //            d.strIdSession = SessionPMLTE.IDSESSION;
            //            d.strCampaign = controls.ddlSearchByCampaign.val();
            //            d.strSolution = controls.ddlSearchBySolution.val();
            //            d.tbuscar = controls.txtSearchByDesc.val();
            //            d.strProductType = SessionPMLTE.ProductType;
            //            d.strSearchOption = SessionPMLTE.SelectedOption;

            //        }
            //    }
            //});
            that.searchData.DataTable().ajax.reload();
        },
        strUrl:window.location.protocol + '//' + window.location.host,
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
    $('#SelectPlan', $('.modal:last')).FormSelectPlan();
})(jQuery);

