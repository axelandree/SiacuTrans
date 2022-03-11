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
            , ModalLoading: $("#ModalLoading", $element)
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
            that.HFCPlanMigrationChoosePlanLoad(function () {
            that.render();

            });


            controls.hidCodCampania = $("#hidCodCampania").val();
            controls.hidMensajeResValCamp = $("#hidMensajeResValCamp").val();
            controls.hidMensajeResUsuClien = $("#hidMensajeResUsuClien").val();
            controls.hidMensajeCantidadMaxima = $("#hidMensajeCantidadMaxima").val();
            controls.hidCambioPlanProy140245 = $("#hidCambioPlanProy140245").val();
            controls.hidMsgErrorValidaCampColab = $("#hidMsgErrorValidaCampColab").val();
            controls.hidMsgErrorValidaCantCampColab = $("#hidMsgErrorValidaCantCampColab").val();
        },

        HFCPlanMigrationChoosePlanLoad: function (Fnrender) {
            var that = this,
                controls = that.getControls(),
                objRequestDataModel = {};
            objRequestDataModel.strIdSession = SessionPMHFC.IDSESSION;
            objRequestDataModel.strCodeUser = SessionPMHFC.USERACCESS.login;
            objRequestDataModel.strIdContract = SessionPMHFC.DATACUSTOMER.ContractID;
            objRequestDataModel.strTypeProduct = ((SessionPMHFC.UrlParams.SUREDIRECT == 'HFC') ? '05' : '08');
            objRequestDataModel.strPermitions = SessionPMHFC.USERACCESS.optionPermissions;
            objRequestDataModel.strPlaneCodeInst = SessionPMHFC.DATACUSTOMER.PlaneCodeInstallation;   

            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objRequestDataModel),
                url: location.protocol + '//' + location.host + '/Transactions/HFC/PlanMigration/HFCChoosePlanLoad',
                async: true,
                beforeSend: function () {
                    $.blockUI({
                        message: '<div align="center"><img src="/Images/loading2.gif"  width="25" height="25" /> Espere un momento por favor .... </div>',                      
                        css: {
                            border: 'none',
                            padding: '15px',
                            backgroundColor: '#000',
                            '-webkit-border-radius': '10px',
                            '-moz-border-radius': '10px',
                            opacity: .5,
                            color: '#fff'
                        }
                    });
                },
                success: function (response) {
                    if (response.data != null) {
                        that.objHFCMigrationPlanChoosePlanLoad = response.data;
                    }
                }, complete: function () { $.unblockUI(); Fnrender(); }
            });
        },
        f_Loading: function () {
            
            var that = this;
            $.blockUI({
                message: '<div align="center"><img src="/Images/loading2.gif"  width="25" height="25" /> Espere un momento por favor .... </div>',
                baseZ: $.app.getMaxZIndex() + 1,
                css: {
                    border: 'none',
                    padding: '15px',
                    backgroundColor: '#000',
                    '-webkit-border-radius': '10px',
                    '-moz-border-radius': '10px',
                    opacity: .5,
                    color: '#fff'
                }
            });
        },
        render: function () {
            
            var that = this,
                controls = this.getControls();

            if (that.objHFCMigrationPlanChoosePlanLoad.lstSearchOptions != null && that.objHFCMigrationPlanChoosePlanLoad.lstSearchOptions.length > 0)
                that.getOption(that.objHFCMigrationPlanChoosePlanLoad.lstSearchOptions);
            if (!that.objHFCMigrationPlanChoosePlanLoad.bolPermition) {
                controls.divSltVigencia.css("display", "none");

                if (that.objHFCMigrationPlanChoosePlanLoad.lstSolutions != null && that.objHFCMigrationPlanChoosePlanLoad.lstSolutions.length > 0)
                    that.getSolution(that.objHFCMigrationPlanChoosePlanLoad.lstSolutions.sort());

                if (that.objHFCMigrationPlanChoosePlanLoad.lstCampaigns != null && that.objHFCMigrationPlanChoosePlanLoad.lstCampaigns.length > 0)
                    that.getCampaign(that.objHFCMigrationPlanChoosePlanLoad.lstCampaigns.sort());

                that.loadPlansDataTable(that.objHFCMigrationPlanChoosePlanLoad.lstPlans, that.objHFCMigrationPlanChoosePlanLoad.bolPermition);

            } else {
                controls.sltSearchByOptions.val("VIGENTES");
                that.f_Loading();
                that.searchObjeto(true, false, function () {

                    that.loadPlansDataTable(xobjFilter, that.objHFCMigrationPlanChoosePlanLoad.bolPermition);

                    $.unblockUI();
                });


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
                "initComplete": function (settings, json) {
                    

                },
                "columns": [
                    { "orderable": false, "data": null,className: "select-radio","defaultContent":"", render: function (data) { return "&nbsp"; } },
                    { "orderable": true, order: "asc", "data": "strDesPlanSisact" },
                    { "data": "strCampaignDescription" },
                    { "data": "strSolucion" },
                    { "data": "strStatus" },
                    { "data": "strCodPlanSisact" },
                    { "data": "strCampaignCode" }
                ],
                "columnDefs": [
                    //{
                    //    orderable: false,
                    //    className: 'select-radio',
                    //    "sort": false,
                    //    targets: 0

                    //}, 
                    //{
                    //    targets: 1,
                    //    order: "asc"
                    //},
                    {
                        targets: 4,
                        visible: bolPermition,
                        render: function (data, type, row) {
                            return data == '1' ? 'SI' : 'NO';
                        }

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
            //controls.tblPlans.DataTable({
            //    "order": [[1, "asc"]]
            //});
        },
        btnClean_Click: function () {
            var that = this,
                controls = this.getControls();
            controls.txtSearchByDescription.val("");
            if (!that.objHFCMigrationPlanChoosePlanLoad.bolPermition) {

                if (that.objHFCMigrationPlanChoosePlanLoad.lstSolutions != null)
                    that.getSolution(that.objHFCMigrationPlanChoosePlanLoad.lstSolutions);

                if (that.objHFCMigrationPlanChoosePlanLoad.lstCampaigns != null)
                    that.getCampaign(that.objHFCMigrationPlanChoosePlanLoad.lstCampaigns);

                that.loadPlansDataTable(that.objHFCMigrationPlanChoosePlanLoad.lstPlans, that.objHFCMigrationPlanChoosePlanLoad.bolPermition);

            } else {
                controls.sltSearchByOptions.val("VIGENTES");
                that.f_Loading();
                that.searchObjeto(true, false, function () {

                    that.loadPlansDataTable(xobjFilter, that.objHFCMigrationPlanChoosePlanLoad.bolPermition);
                    $.unblockUI();
                });

            }
        },
        btnSearchByCampaign_Click: function () {
            var that = this,
                controls = this.getControls();
            that.f_Loading();
            setTimeout(function () {
                that.searchObjeto(false, true, function ()
                {
                    that.loadPlansDataTable(xobjFilter, that.objHFCMigrationPlanChoosePlanLoad.bolPermition);
                    $.unblockUI();
                });
            }, 1000);


        },
        btnSearchBySolution_Click: function () {
            var that = this,
                controls = this.getControls();
            that.f_Loading();
            that.searchObjeto(false, false, function ()
            {
                that.loadPlansDataTable(xobjFilter, that.objHFCMigrationPlanChoosePlanLoad.bolPermition);
                $.unblockUI();
            });


        },
        txtSearchByDescription_keyup: function () {
            var that = this,
                controls = this.getControls();

            that.searchObjeto(false, false, function () {
                that.loadPlansDataTable(xobjFilter, that.objHFCMigrationPlanChoosePlanLoad.bolPermition);
                $.unblockUI();
            });
        },
        sltSearchByOptions_Click: function () {
            var that = this,
                controls = this.getControls();
            that.f_Loading();
            that.searchObjeto(true, false, function () {
                that.loadPlansDataTable(xobjFilter, that.objHFCMigrationPlanChoosePlanLoad.bolPermition);
                $.unblockUI();
            });
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
        getUnique: function (array, prop) {
            var uniques = [],
                hash = {};

            array.forEach(function (object) {
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
        xobjFilter: null,
        searchObjeto: function (bolSltOptionValidity, bolSltCampaigns, fnCallback) {
            var that = this,
                controls = this.getControls();
            var objFilter = that.objHFCMigrationPlanChoosePlanLoad.lstPlans;
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
                    that.getSolution(that.objHFCMigrationPlanChoosePlanLoad.lstSolutions);
                    that.getCampaign(that.objHFCMigrationPlanChoosePlanLoad.lstCampaigns);
                }
            } else {
                if (controls.sltSearchByCampaign.val() != "" && controls.sltSearchByCampaign.val() != null) {
                    objFilter = objFilter.filter(function (x) { return x.strCampaignDescription == controls.sltSearchByCampaign.val() });
                    if (bolSltCampaigns) {
                        var objFilterSolution = that.getUnique(objFilter, "strSolucion").sort();
                        that.getSolution(objFilterSolution);
                    }else if (controls.sltSearchBySolution.val() != "" && controls.sltSearchBySolution.val() != null)
                        objFilter = objFilter.filter(function (x) { return x.strSolucion == controls.sltSearchBySolution.val() });

                }
                else if (controls.sltSearchBySolution.val() != "" && controls.sltSearchBySolution.val() != null) {
                    objFilter = objFilter.filter(function (x) { return x.strSolucion == controls.sltSearchBySolution.val() });
                }else that.getSolution(that.objHFCMigrationPlanChoosePlanLoad.lstSolutions);

            }
            var strTextFilter = controls.txtSearchByDescription.val();
            if (strTextFilter != "")
                objFilter = objFilter.filter(function (x) { return x.strDesPlanSisact.toUpperCase().indexOf(strTextFilter.toUpperCase()) >= 0 || x.strSolucion.toUpperCase().indexOf(strTextFilter.toUpperCase()) >= 0 || x.strCampaignDescription.toUpperCase().indexOf(strTextFilter.toUpperCase()) >= 0 });
            
  if (objFilter[0].strCampaignCode != null) {
                controls.strCampaignCode = objFilter[0].strCampaignCode;

                console.log(controls.hidCambioPlanProy140245);

                if (controls.hidCambioPlanProy140245 == 1)
                {
                    that.ValidateWhilist(function ()
                    {
                        if (controls.strWhilist == false && controls.CampaniaColab == 1) {
                            objFilter = that.objHFCMigrationPlanChoosePlanLoad.lstPlans;
                            that.getCampaign(that.objHFCMigrationPlanChoosePlanLoad.lstCampaigns.sort());
                            xobjFilter = objFilter;
                            fnCallback();
                            return;
                        } else {
                            xobjFilter = objFilter;
                            fnCallback();
                            return;
                        }
                    });
                } else {
                    xobjFilter = objFilter;
                    fnCallback();
                    return;
                }
                

            } else {
                xobjFilter = objFilter;
                fnCallback();
                return;
            }
        },

        ValidarCampaniaColab: function (fnCallback) {
            var that = this,
                controls = this.getControls(),
                objParam = {
                    MessageRequest: {
                        Body: {
                            validarColaboradorRequest: {
                                numeroDocumento: SessionPMHFC.DATACUSTOMER.DNIRUC
                            }
                        }
                    },
                    strIdSession: Session.IDSESSION,
                    strTipoDocumento: SessionPMHFC.DATACUSTOMER.DocumentType
                };
            controls.strValidaColaborador = false;
            controls.CampaniaColab = 0;

            var ArrayCampColab = controls.hidCodCampania.split("|");

            i = ArrayCampColab.length;
            while (i--) {
                if (ArrayCampColab[i] == controls.strCampaignCode) {
                    controls.CampaniaColab = 1;
                }
            }

            if (controls.CampaniaColab == 1)
            {
                $.app.ajax({
                    type: 'POST',
                    contentType: "application/json; charset=utf-8",
                    dataType: 'json',
                    async: true,
                    url: location.protocol + '//' + location.host + '/Transactions/HFC/PlanMigration/GetValidateCollaborator',
                    data: JSON.stringify(objParam),

                    complete: function () {
                        fnCallback();
                        $(".loading").hide();

                    },

                    success: function (response)
                    {
                        if (response != null) {
                            if (response.MessageResponse != null) {
                                if (response.MessageResponse.Header.HeaderResponse.status.code == "0") {
                                    if (response.MessageResponse.Body != null) {
                                        if (response.MessageResponse.Body.validarColaboradorResponse != null) {
                                            if (response.MessageResponse.Body.validarColaboradorResponse.auditResponse != null) {
                                                if (response.MessageResponse.Body.validarColaboradorResponse.auditResponse.codigoRespuesta == "0") {
                                                    if (response.MessageResponse.Body.validarColaboradorResponse.pExiste == "S") {
                                                        controls.strValidaColaborador = true;
                                                        return;
                                                    } else {
                                                        alert(controls.hidMensajeResValCamp, 'Alerta');
                                                        return;
                                                    }
                                                } else {
                                                    alert(controls.hidMsgErrorValidaCampColab, 'Alerta');
                                                }
                                            }
                                        }
                                    }
                                } else {
                                    alert(controls.hidMsgErrorValidaCampColab, 'Alerta');
                                }
                            } 
                        }
                    },

                    error: function () {
                        alert('Estimado usuario, presentamos intermitencia de aplicativo. ', 'Alerta');
                        return;
                    }
                });
            } else {
                fnCallback();
                return;
            }
        },

        ValidateUserLogin: function (fnCallback) {
            var that = this,
                controls = this.getControls(),
                objParam = {
                    strIdSession: Session.IDSESSION
                };
            controls.strValUserLogin = false;
            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                async: true,
                url: location.protocol + '//' + location.host + '/Transactions/HFC/PlanMigration/IsUserLogin',
                data: JSON.stringify(objParam),

                complete: function () {
                    fnCallback();
                    $(".loading").hide();
                },

                success: function (response)
                {
                    if (response != null) {
                        if (response.accessResponse != null) {
                            if (response.accessResponse.employee != null) {
                                if (response.accessResponse.employee.dni != SessionPMHFC.DATACUSTOMER.DNIRUC)
                                {
                                    controls.strValUserLogin = true;
                                    return;
                                }
                            }
                        }
                    }
                    alert(controls.hidMensajeResUsuClien, 'Alerta');
                    return;
                },

                error: function () {
                    alert('Estimado usuario, presentamos intermitencia de aplicativo. ', 'Alerta');
                    return;
                }
            });
        },

        ValidateQuantityCampaign: function (fnCallback) {
            var that = this,
               controls = this.getControls(),
               objParam = {
                   MessageRequest: {
                       Body: {
                           validarCantidadCampaniaRequest: {
                               numeroDocumento: SessionPMHFC.DATACUSTOMER.DNIRUC,
                               codTipoProducto: ((SessionPMHFC.UrlParams.SUREDIRECT == 'HFC') ? '05' : '08'),
                               descTipoProducto: SessionPMHFC.UrlParams.SUREDIRECT
                           }
                       }
                   },
                   strIdSession: Session.IDSESSION,
                   strTipoDocumento: SessionPMHFC.DATACUSTOMER.DocumentType
               };

            controls.strValidateQuantity = false;

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                async: true,
                url: location.protocol + '//' + location.host + '/Transactions/HFC/PlanMigration/GetValidateQuantityCampaign',
                data: JSON.stringify(objParam),

                complete: function () {
                    fnCallback();
                    $(".loading").hide();
                },

                success: function (response)
                {
                    if (response != null) {
                        if (response.MessageResponse != null) {
                            if (response.MessageResponse.Header.HeaderResponse.status.code == "0") {
                                if (response.MessageResponse.Body != null) {
                                    if (response.MessageResponse.Body.validarCantidadCampaniaResponse != null) {
                                        if (response.MessageResponse.Body.validarCantidadCampaniaResponse.auditResponse.codigoRespuesta == "0") {
                                            if (response.MessageResponse.Body.validarCantidadCampaniaResponse.listarCantMaxProducto != null) {
                                                if (response.MessageResponse.Body.validarCantidadCampaniaResponse.listarCantMaxProducto.tipoProducto != null)
                                                {
                                                    var intcantMaxProducto = response.MessageResponse.Body.validarCantidadCampaniaResponse.listarCantMaxProducto.tipoProducto[0].cantMaxProducto;
                                                    if (intcantMaxProducto > 0)
                                                    {
                                                        console.log(response.MessageResponse.Body.validarCantidadCampaniaResponse.listarCantMaxProducto);
                                                        controls.strValidateQuantity = true;
                                                        return;
                                                    } else {
                                                        alert(controls.hidMensajeCantidadMaxima, 'Alerta');
                                                        return;
                                                    }
                                                }
                                            }
                                        } else {
                                            alert(controls.hidMsgErrorValidaCantCampColab, 'Alerta');
                                        }
                                    }
                                }
                            } else {
                                alert(controls.hidMsgErrorValidaCantCampColab, 'Alerta');
                            }
                        }
                    }
                },

                error: function () {
                    alert('Estimado usuario, presentamos intermitencia de aplicativo. ', 'Alerta');
                    return;
                }
            });
        },

        ValidateWhilist: function (fnCallback) {
            var that = this,
                controls = this.getControls();

            controls.strWhilist = false;

            that.ValidarCampaniaColab(function () {

                if (controls.strValidaColaborador == true)
                {
                    that.ValidateUserLogin(function ()
                    {
                        if (controls.strValUserLogin == true)
                        {
                            if (Session.intCampania != "1"){
                            that.ValidateQuantityCampaign(function ()
                            {
                                if (controls.strValidateQuantity == true) {
                                    controls.strWhilist = true;
                                }
                                fnCallback();
                            });
                            } else {
                                controls.strWhilist = true;
                                fnCallback();
                            }
                        } else {
                            fnCallback();
                        }

                    });

                } else {
                    fnCallback();
                }

            });

        },

        strUrlLogo: window.location.protocol + '//' + window.location.host + '/Images/loading_3.gif',
        objHFCMigrationPlanChoosePlanLoad: {}
    };
    $.fn.FormChoosePlan = function () {
        var option = arguments[0], args = arguments, value, allowedMethods = [];

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