(function ($, undefined) {
    var Form = function ($element, options) {

        $.extend(this, $.fn.FormChoosePlan.defaults, $element.data(), typeof options === 'object' && options);

        this.setControles({
            form: $element
            , btnAddPlan: $('#btnAddPlan', $element)
            , tblPlans: $("#tblPlans", $element)
            , btnClean: $("#btnClean", $element)
            , btnSearchByDescription: $("#btnSearchByDescription", $element) 
            , btnSearchByCampaign: $("#btnSearchByCampaign", $element)
            , btnSearchBySolution: $("#btnSearchBySolution", $element) 
            , txtSearchByDescription: $("#txtSearchByDescription", $element)
            , sltSearchByCampaign: $("#sltSearchByCampaign", $element) 
            , sltSearchBySolution: $("#sltSearchBySolution", $element) 
            , tblPlansBody: $("#tblPlansBody", $element) 
            , sltSearchByOptions: $("#sltSearchByOptions", $element)
            , divSltVigencia: $("#divSltVigencia", $element)
        });

    }

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this;
            var controls = this.getControls();
            controls.btnClean.addEvent(that, 'click', that.btnClean_Click);
            controls.txtSearchByDescription.addEvent(that, 'keyup', that.txtSearchByDescription_keyup);
            controls.sltSearchByOptions.addEvent(that, 'change', that.sltSearchByOptions_Click);
            controls.sltSearchByCampaign.addEvent(that, 'change', that.btnSearchByCampaign_Click);
            controls.sltSearchBySolution.addEvent(that, 'change', that.btnSearchBySolution_Click);
            that.LTEPlanMigrationChoosePlanLoad();
            that.render();
        },
        
        LTEPlanMigrationChoosePlanLoad: function () {
            var that = this,
                controls = that.getControls(),
                objRequestDataModel = {};
            objRequestDataModel.strIdSession = Session.IDSESSION;
            objRequestDataModel.strCodeUser = Session.USERACCESS.login;
            objRequestDataModel.strIdContract = Session.DATACUSTOMER.ContractID;
            objRequestDataModel.strTypeProduct = Session.ProductType;
            objRequestDataModel.strPermitions = Session.USERACCESS.optionPermissions;
            objRequestDataModel.strOffice = Session.strOffice;
            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objRequestDataModel),
                url: location.protocol + '//' + location.host + '/Transactions/LTE/PlanMigration/LTEChoosePlanLoad',
                async: false,
                success: function (response) {
                    if (response.data != null) {
                        that.objLteMigrationPlanChoosePlanLoad = response.data;
                    }
                }
            });
        },
        render: function () {
            var that = this,
                controls = this.getControls();
            
            if (that.objLteMigrationPlanChoosePlanLoad.lstSearchOptions != null && that.objLteMigrationPlanChoosePlanLoad.lstSearchOptions.length > 0)
                that.getOption(that.objLteMigrationPlanChoosePlanLoad.lstSearchOptions);

            if (!that.objLteMigrationPlanChoosePlanLoad.bolPermition) {
                controls.divSltVigencia.css("display", "none");

                if (that.objLteMigrationPlanChoosePlanLoad.lstSolutions != null && that.objLteMigrationPlanChoosePlanLoad.lstSolutions.length > 0)
                    that.getSolution(that.objLteMigrationPlanChoosePlanLoad.lstSolutions.sort());

                if (that.objLteMigrationPlanChoosePlanLoad.lstCampaigns != null && that.objLteMigrationPlanChoosePlanLoad.lstCampaigns.length > 0)
                    that.getCampaign(that.objLteMigrationPlanChoosePlanLoad.lstCampaigns.sort());

                that.loadPlansDataTable(that.objLteMigrationPlanChoosePlanLoad.lstPlans, that.objLteMigrationPlanChoosePlanLoad.bolPermition);

            } else {
                controls.sltSearchByOptions.val("VIGENTES");
                that.loadPlansDataTable(that.searchObjeto(true, false), that.objLteMigrationPlanChoosePlanLoad.bolPermition);
            }

            
           
        },
        loadPlansDataTable: function (objDataPlans, bolPermition) {
            var that = this,
                controls = this.getControls();

             
            controls.tblPlans.DataTable({
                "pagingType": "full_numbers",
                "scrollY": "200px",
                "scrollCollapse": true,
                "processing": true,
                "serverSide": false,
                "paging": true,
                "pageLength": 10,
                "destroy": true,
                "searching": true,
                "language": {
                    "lengthMenu": "Mostrar _MENU_ registros por página.",
                    "zeroRecords": "No existen datos",
                    "loadingRecords": "&nbsp;",
                    "processing": "<img src=" + that.strUrlLogo + " width='25' height='25' /> Cargando ... </div>",
                    "info": " ",
                    "infoEmpty": " ",
                    "infoFiltered": "(filtered from _MAX_ total records)",
                    "search": "Busqueda General",
                    "oPaginate": {
                        "sFirst": "Primero",
                        "sPrevious": "Anterior",
                        "sNext": "Siguiente",
                        "sLast": "Último"
                    },
                    //"searching": true,
                    "emptyTable": "No existen datos"
                },
                "data": objDataPlans
                ,
                "columns": [
                    { "orderable": false, "data": null, className: "select-radio", "defaultContent": "", render: function (data) { return "&nbsp"; } },
                    { "orderable": true, order: "asc", "data": "strDesPlanSisact" },
                    { "data": "strCampaignDescription" },
                    { "data": "strSolucion" },
                    { "data": "strStatus" },
                    { "data": "strCodPlanSisact" },
                    { "data": "strCampaignCode" }
                ],
                "columnDefs": [
                //    {
                //    orderable: false,
                //    className: 'select-radio',
                //    "sort": false,
                //    targets: 0

                //},{
                //        targets: 1,
                //        order: "asc"
                //},
                {
                    targets: 4,
                    visible:bolPermition,
                    render: function (data, type, row) {
                        return data == '1' ? 'SI' : 'NO';
                    },

                },
                {
                    targets: 5,
                    visible: false
                },
                {
                    targets: 6,
                    visible: false
                }],
                select: {
                    style: 'os',
                    info: false
                }
            });
            $('.dataTables_filter').hide();

        },
        btnClean_Click: function () {
            var that = this,
                controls = this.getControls();
            controls.txtSearchByDescription.val("");
            if (!that.objLteMigrationPlanChoosePlanLoad.bolPermition) {

                if (that.objLteMigrationPlanChoosePlanLoad.lstSolutions != null)
                    that.getSolution(that.objLteMigrationPlanChoosePlanLoad.lstSolutions);

                if (that.objLteMigrationPlanChoosePlanLoad.lstCampaigns != null)
                    that.getCampaign(that.objLteMigrationPlanChoosePlanLoad.lstCampaigns);

                that.loadPlansDataTable(that.objLteMigrationPlanChoosePlanLoad.lstPlans, that.objLteMigrationPlanChoosePlanLoad.bolPermition);

            } else {
                controls.sltSearchByOptions.val("VIGENTES");
                that.loadPlansDataTable(that.searchObjeto(true, false), that.objLteMigrationPlanChoosePlanLoad.bolPermition);
            }
            
        },
        btnSearchByCampaign_Click: function () {
            var that = this,
                controls = this.getControls();
            that.loadPlansDataTable(that.searchObjeto(false, true), that.objLteMigrationPlanChoosePlanLoad.bolPermition);
        },
        btnSearchBySolution_Click: function () {
            var that = this,
                controls = this.getControls();
            that.loadPlansDataTable(that.searchObjeto(false, false), that.objLteMigrationPlanChoosePlanLoad.bolPermition);
        },
        txtSearchByDescription_keyup: function () {
            var that = this,
                controls = this.getControls();
            that.loadPlansDataTable(that.searchObjeto(false, false), that.objLteMigrationPlanChoosePlanLoad.bolPermition);
        },
        sltSearchByOptions_Click: function () {
            var that = this,
                controls = this.getControls();
            that.loadPlansDataTable(that.searchObjeto(true, false), that.objLteMigrationPlanChoosePlanLoad.bolPermition);
        },
        getSolution: function (lstSolutions) {
            if (lstSolutions.length > 0) {
                var that = this,
                    controls = that.getControls();
                controls.sltSearchBySolution.html("");
                controls.sltSearchBySolution.append($('<option>', { value: '', html: 'Seleccionar' }));
                $.each(lstSolutions, function (index, value) {
                    controls.sltSearchBySolution.append($('<option>', { value: value, html: value }));
                });
            }
        },
        getCampaign: function (lstCampaigns) {
            if (lstCampaigns.length > 0) {
                var that = this,
                    controls = that.getControls();
                controls.sltSearchByCampaign.html("");
                controls.sltSearchByCampaign.append($('<option>', { value: '', html: 'Seleccionar' }));
                $.each(lstCampaigns, function (index, value) {
                    controls.sltSearchByCampaign.append($('<option>', { value: value, html: value }));
                });
            }
        },
        getOption: function (lstSearchOptions) {
            if (lstSearchOptions.length > 0) {
                var that = this,
                    controls = that.getControls();
                $.each(lstSearchOptions, function (index, value) {
                    controls.sltSearchByOptions.append($('<option>', { value: value.Code, html: value.Description }));
                });
            }
        },
        getControls: function () {
            return this.m_controls || {};
        },
        getUnique: function(array, prop) {
            var uniques = [],
                hash = {};
  
            array.forEach(function(object) {
                var value = object[prop];
    
                if (!hash[value]) {
                    hash[value] = true;
                    uniques.push(object[prop]);
                }
            });
  
            return uniques;
        },
        setControles: function (value) {
            this.m_controls = value;
        },
        searchObjeto: function (bolSltOptionValidity, bolSltCampaigns) {
            var that = this,
                controls = this.getControls();
            var objFilter = that.objLteMigrationPlanChoosePlanLoad.lstPlans;
            if (controls.sltSearchByOptions.val() === "VIGENTES")
                objFilter = objFilter.filter(function (x) { return x.strStatus == "1" });
            else if (controls.sltSearchByOptions.val() === "NO VIGENTES")
                objFilter = objFilter.filter(function (x) { return x.strStatus == "0" });

            if (bolSltOptionValidity) {
                if (controls.sltSearchByOptions.val() === "VIGENTES" || controls.sltSearchByOptions.val() === "NO VIGENTES") {
                    var objFilterSolution = that.getUnique(objFilter, "strSolucion").sort();
                    that.getSolution(objFilterSolution);
                    var objFilterCampaign = that.getUnique(objFilter, "strCampaignDescription").sort();
                    that.getCampaign(objFilterCampaign);
                } else {
                    that.getSolution(that.objLteMigrationPlanChoosePlanLoad.lstSolutions);
                    that.getCampaign(that.objLteMigrationPlanChoosePlanLoad.lstCampaigns);
                }
            } else {
                if (controls.sltSearchByCampaign.val() != "" && controls.sltSearchByCampaign.val() != null) {
                objFilter= objFilter.filter(function(x) { return x.strCampaignDescription == controls.sltSearchByCampaign.val() });
                    if (bolSltCampaigns) {
                        var objFilterSolution = that.getUnique(objFilter, "strSolucion").sort();
                        that.getSolution(objFilterSolution);
                    } else if (controls.sltSearchBySolution.val() != "" && controls.sltSearchBySolution.val() != null)
                        objFilter = objFilter.filter(function (x) { return x.strSolucion == controls.sltSearchBySolution.val() });

                }
                else if (controls.sltSearchBySolution.val() != "" && controls.sltSearchBySolution.val() != null) {
                objFilter = objFilter.filter(function(x) {return x.strSolucion == controls.sltSearchBySolution.val()});
                } else that.getSolution(that.objLteMigrationPlanChoosePlanLoad.lstSolutions);

            }
            var strTextFilter = controls.txtSearchByDescription.val();
            if(strTextFilter!="")
                objFilter= objFilter.filter(function(x) {return x.strDesPlanSisact.toUpperCase().indexOf(strTextFilter.toUpperCase())>=0 || x.strSolucion.toUpperCase().indexOf(strTextFilter.toUpperCase())>=0 || x.strCampaignDescription.toUpperCase().indexOf(strTextFilter.toUpperCase())>=0 });

            return objFilter;

        },

        strUrlLogo: window.location.protocol + '//' + window.location.host + '/Images/loading_3.gif',
        objLteMigrationPlanChoosePlanLoad: {}
    };
    $.fn.FormChoosePlan = function () {
        var option = arguments[0],args = arguments,value,allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('FormChoosePlan'),
                options = $.extend({}, $.fn.FormChoosePlan.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('FormChoosePlan', data);
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
    $.fn.FormChoosePlan.defaults = {
    }
    $('#ChoosePlan', $('.modal:last')).FormChoosePlan();

})(jQuery);