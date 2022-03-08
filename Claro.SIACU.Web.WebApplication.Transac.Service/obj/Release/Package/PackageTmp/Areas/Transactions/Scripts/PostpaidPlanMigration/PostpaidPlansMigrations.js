
(function ($, undefined) {



    var Form = function ($element, options) {

        $.extend(this, $.fn.PlansMigrationsDetail.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
          , tblServiciosNuevoMigracionPlan_Post: $('#tblServiciosNuevoMigracionPlan_Post', $element)
          , tblServiciosNuevoMigracionPlan_PostBody: $('#tblServiciosNuevoMigracionPlan_Post tbody', $element)
          , drPlanBasico: $('input[name="rdSeleccionPlan"]:radio', $element)
          , ddlMigracionModalidad: $('#ddlMigracionModalidad', $element)
          , ddlMigracionFamilia: $('#ddlMigracionFamilia', $element)
          , dvModFamilia: $('#dvModFamilia', $element)
        });

    }

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this,
            controls = this.getControls();
            controls.drPlanBasico.addEvent(that, 'change', that.drPlanBasico_change);
            controls.ddlMigracionModalidad.addEvent(that, 'change', that.UploadPlans);
            controls.ddlMigracionFamilia.addEvent(that, 'change', that.UploadPlans);
            that.render();
        },
        render: function () {
            var that = this,
                controls = that.getControls();
            that.getLoadtblServiciosNuevoMigracionPlan_Post();
            controls.dvModFamilia.hide()
        },

        getControls: function () {
            return this.m_controls || {};
        },
        setControls: function (value) {
            this.m_controls = value;
        },
        InicializeControl: function () {
            var that = this,
                controls = that.getControls();
            controls.tblServiciosNuevoMigracionPlan_Post.DataTable().clear().draw();
            controls.ddlMigracionFamilia.val(null);
            controls.ddlMigracionModalidad.val(null);
        },
        drPlanBasico_change: function (sender, args) {
            
            var vLlenadoGrilla;
            var that = this;
            that.InicializeControl();
            var ValorTipoProducto;
            if (sender[0].id == "drPlanBasico") {
                ValorTipoProducto = "01";
                vLlenadoGrilla=that.ShowComboBox(true);
            }
            else if (sender[0].id == "drPlanCombos") {
                ValorTipoProducto = "02";
                vLlenadoGrilla = that.ShowComboBox(false);
            }
            else if (sender[0].id == "drPlanArmaPostPago") {
                ValorTipoProducto = "03";
                vLlenadoGrilla = that.ShowComboBox(false);
            }
            if (vLlenadoGrilla=="0") {
            this.getNewPlans(ValorTipoProducto)
            }

        },
        tblServiciosNuevoMigracionPlan_Post_click: function () {
            
            var that = this,
            controls = that.getControls();
            //console.log$(this));
            $(this).find('input[type=radio]').prop('checked', true);
        },
        ShowComboBox: function (flag)
        {
            var retorno;
            var codigoPlanTarifario = Session.codigoPlanTarifario;//Modificar Roberto
            var that = this,
                controls = that.getControls();
            if (flag == true) {
                var objShow = {};
                objShow.strIdSession = Session.IDSESSION;
            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                async:false,
                url: '/Transactions/Postpaid/PlanMigration/GetConfigBamTFI',
                data: JSON.stringify(objShow),//Modificar Roberto
                success: function (response) {
                    Session.DATACUSTOMER.TipoCliente = "Consumereee";//Modificar Roberto
                    Session.PlanBAM = response.data[1];
                    Session.PlanTFI = response.data[2];
                    if (Session.DATACUSTOMER.TipoCliente == response.data[0] && that.IsPlanTFI(codigoPlanTarifario, response.data[2]) == false) {//cadena BAM=[1], TFI=[2]

                        if (that.IsPlanBAM(codigoPlanTarifario, response.data[1])==true) {
                            controls.dvModFamilia.hide();
                            return retorno = "0";
                        }
                        else {
                            controls.dvModFamilia.show();
                            that.FillCombos();
                            that.UploadPlans("01");
                            return retorno = "1";
                        }
                    }
                    else {
                        controls.dvModFamilia.hide();
                        return retorno = "0";
                    }
                }
            });
            }
            else {
                controls.dvModFamilia.hide();
                return retorno = "0";
            }
           return retorno;

        },
        UploadPlans: function (TipoProducto) {
            var that = this,
                controls = that.getControls();
            if ((controls.ddlMigracionFamilia.val() != "" || controls.ddlMigracionModalidad.val() != "")
                && (controls.ddlMigracionFamilia.val() != null || controls.ddlMigracionModalidad.val() != null)) {
                if (controls.ddlMigracionFamilia.val() > 0 && controls.ddlMigracionModalidad.val() >0) {

                var objNewPlans = {};
                objNewPlans.strIdSession = Session.IDSESSION;
                objNewPlans.CategoriaProducto = "BU5";//Modificar Roberto
                objNewPlans.MigracionPlan = "65551";//Modificar Roberto
                objNewPlans.PlanActual = "PLAN MYPE FÁCIL 55";//Modificar Roberto
                objNewPlans.Modalidad = controls.ddlMigracionModalidad.val();//Modificar Roberto
                objNewPlans.Familia = controls.ddlMigracionFamilia.val();//Modificar Roberto
                objNewPlans.ValorTipoProducto = TipoProducto;//Modificar Roberto
                $.app.ajax({
                    type: 'POST',
                    contentType: "application/json; charset=utf-8",
                    dataType: 'json',
                    url: '/Transactions/Postpaid/PlanMigration/GetPlansMigrations',
                    data: JSON.stringify(objNewPlans),
                    success: function (response) {
                        that.getNewPlanMigration(response.data.lstNewPlan, ValorTipoProducto);
                    }
                });
                }
            }

        },
        createComboLoadModalidadType: function (response) {
            var that = this,
                controls = that.getControls();
            controls.ddlMigracionModalidad.append($('<option>', { value: '', html: 'Seleccionar' }));
            $.each(response.data, function (index, value) {
                controls.ddlMigracionModalidad.append($('<option>', { value: value.strCode, html: value.strDescription }));
            });
        },
        createComboLoadFamiliaType: function (response) {
            var that = this,
                controls = that.getControls();
            controls.ddlMigracionFamilia.append($('<option>', { value: '', html: 'Seleccionar' }));
            $.each(response.data, function (index, value) {
                controls.ddlMigracionFamilia.append($('<option>', { value: value.strCode, html: value.strDescription}));
            });
        },
        FillCombos: function () {
            var that = this,
                controls = that.getControls();
            var iModalidad=controls.ddlMigracionModalidad.find("option").length;
            var iFamilia = controls.ddlMigracionFamilia.find("option").length;
            if (iModalidad == 0) {
                if (Session.PlanesMigracion[4] == null) {
                    var objList = {};
                    objList.strIdSession = "12312312";
                    objList.strClave = "MODALIDAD_PLANES_POSTPAGO";
                        $.app.ajax({
                            type: 'POST',
                            contentType: "application/json; charset=utf-8",
                            dataType: 'json',
                            url: '/CommonServices/GetListGeneric',
                            data: JSON.stringify(objList),//Modificar Roberto
                            success: function (response) {
                                Session.PlanesMigracion[4] = response;
                                that.createComboLoadModalidadType(response);
                            }
                        });
                }
                else {
                    that.createComboLoadModalidadType(Session.PlanesMigracion[4]);
                }

            }
            if (iFamilia == 0) {
                if (Session.PlanesMigracion[5] == null) {
                    var objList = {};
                    objList.strIdSession = Session.IDSESSION;
                    objList.strClave = "FAMILIA_PLANES_POSTPAGO";//Modificar
                    $.app.ajax({
                        type: 'POST',
                        contentType: "application/json; charset=utf-8",
                        dataType: 'json',
                        url: '/CommonServices/GetListGeneric',
                        data: JSON.stringify(objList),//Modificar Roberto
                        success: function (response) {
                            Session.PlanesMigracion[5] = response;
                            that.createComboLoadFamiliaType(response);
                        }
                    });
                }
                else {
                    that.createComboLoadFamiliaType(Session.PlanesMigracion[5]);
                }
            }
        },
        IsPlanTFI: function(CodPlanTarifario,CodPlanesTarifariosTFI)
        {
            var datos = new Array();
            var existe = false;
            datos = CodPlanesTarifariosTFI.split('|');
            var plan = CodPlanTarifario;
            for (var i = 0; i < datos.length; i++) {
                if (plan==datos[i])

                { existe = true; break; }
            }
            return existe;
        },
        IsPlanBAM: function (CodPlanTarifario, CodPlanesTarifariosBAM)
        {
            var datos = new Array();
            var existe = false;
            datos = CodPlanesTarifariosBAM.split('|');
            var plan = CodPlanTarifario;
            for (var i = 0; i < datos.length; i++) {
                if (plan == datos[i])

                { existe = true; break; }
            }
            return existe;
        },
        //ddlMigracionModalidadFamilia_change: function()
        //{
        //    var that = this,
        //        controls = that.getControls(),
        //    objNewPlans = {};

        //    objNewPlans.strIdSession = "125625";
        //    objNewPlans.CategoriaProducto = "BU5";
        //    objNewPlans.MigracionPlan = "65551";
        //    objNewPlans.PlanActual = "PLAN MYPE FÁCIL 55";
        //    objNewPlans.Modalidad = controls.ddlMigracionModalidad.val();
        //    objNewPlans.Familia = controls.ddlMigracionFamilia.val();
        //    objNewPlans.ValorTipoProducto = ValorTipoProducto;
        //    $.app.ajax({
        //        type: 'POST',
        //        contentType: "application/json; charset=utf-8",
        //        dataType: 'json',
        //        url: '/Transactions/Postpaid/PlanMigration/GetPlansMigrations',
        //        data: JSON.stringify(objNewPlans),
        //        success: function (response) {
        //            that.getNewPlanMigration(response.data.lstNewPlan, ValorTipoProducto);
        //        }
        //    });
        //},
        getNewPlanMigration: function myfunction(responseNewPlan, ValorTipoProducto) {
            var that = this,
                controls = that.getControls();
            if (ValorTipoProducto=="01") {//setear por primera vez variables de session
                Session.PlanesMigracion[0] = responseNewPlan;
            } else if (ValorTipoProducto == "02") {
                Session.PlanesMigracion[1] = responseNewPlan;
            } else if (ValorTipoProducto == "03") {
                Session.PlanesMigracion[2] = responseNewPlan;
            }
            that.tblServiciosNuevoMigracionPlan_Post = controls.tblServiciosNuevoMigracionPlan_Post.DataTable({
                "info": false,
                "scrollY": "250px",
                "scrollCollapse": true,
                "paging": false,
                "searching": false,
                "select" : "single",
                "data": responseNewPlan,
                "destroy": true,
                "language": {
                    "lengthMenu": "Mostrar _MENU_ registros por página.",
                    "zeroRecords": "No existen datos",
                    "info": " ",
                    "infoEmpty": " ",
                    "infoFiltered": "(filtered from _MAX_ total records)"
                },
                "columns": [
                    { "data": null },
                    { "data": "COD_PROD" },
                    { "data": "DESC_PLAN" },
                    { "data": "TMCODE" },
                    { "data": "VERSION" },
                    { "data": "CAT_PROD" },
                    { "data": "COD_CARTA_INFO" },
                    { "data": "FECHA_INI_VIG" },
                    { "data": "FECHA_FIN_VIG" },
                    { "data": "ID_TIPO_PROD" },
                    { "data": "USUARIO" },
                ],

                "columnDefs": [
                    {
                        targets: 0,
                        className:'select-radio'
                    },
                        {
                            targets: [1,3,4,5,6,7,8,9,10],
                            visible: false
                        }
                ]
                
            });

        },
        getNewPlans: function (ValorTipoProducto) {
            var that = this,
                objNewPlans = {};
            var existe = false;
            var arrListado = {};
            if (ValorTipoProducto == "01" && Session.PlanesMigracion[0] != null) {//[0]=planes basicos, [1]=Planes Combos, [3]=Arma tu Postpago
                existe = true;
                arrListado = Session.PlanesMigracion[0];
            }
            if (ValorTipoProducto == "02" && Session.PlanesMigracion[1] != null) {//[0]=planes basicos, [1]=Planes Combos, [3]=Arma tu Postpago
                existe = true;
                arrListado = Session.PlanesMigracion[1];
            }
            if (ValorTipoProducto == "03" && Session.PlanesMigracion[2] != null) {//[0]=planes basicos, [1]=Planes Combos, [3]=Arma tu Postpago
                existe = true;
                arrListado = Session.PlanesMigracion[2];
            }
            if (existe ==false) {

                objNewPlans.strIdSession = Session.IDSESSION;
                    objNewPlans.CategoriaProducto = "BU5";//Modificar Roberto
                    objNewPlans.MigracionPlan = "65551";//Modificar Roberto
                    objNewPlans.PlanActual = "PLAN MYPE FÁCIL 55";//Modificar Roberto
                    objNewPlans.CodPlanTarifario = "195";//Modificar Roberto
                    objNewPlans.ValorTipoProducto = ValorTipoProducto;
                    $.app.ajax({
                        type: 'POST',
                        contentType: "application/json; charset=utf-8",
                        dataType: 'json',
                        url: '/Transactions/Postpaid/PlanMigration/GetNewPlans',
                        data: JSON.stringify(objNewPlans),
                        success: function (response) {
                            that.getNewPlanMigration(response.data.lstNewPlan, ValorTipoProducto);
                        }
                    });
            }
            else {
                that.getNewPlanMigration(arrListado, ValorTipoProducto);
            }
        },
        getLoadtblServiciosNuevoMigracionPlan_Post: function () {
            var controls = this.getControls();

            controls.tblServiciosNuevoMigracionPlan_Post.DataTable({
                info: false,
                select: "single",
                paging: false,
                searching: false,
                scrollY: 250,
                scrollX: true,
                scrollCollapse: true,
                destroy: true,
                language: {
                    lengthMenu: "Mostrar _MENU_ registros por página.",
                    zeroRecords: "No existen datos",
                    info: " ",
                    infoEmpty: " ",
                    infoFiltered: "(filtered from _MAX_ total records)"
                },
                columnDefs: [
                    {
                            targets: [1,3,4,5,6,7,8,9,10],
                            visible: false
                        }
                ]
            });
        },
    };
    $.fn.PlansMigrationsDetail = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('PlansMigrationsDetail'),
                options = $.extend({}, $.fn.PlansMigrationsDetail.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('PlansMigrationsDetail', data);
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
    $.fn.PlansMigrationsDetail.defaults = {
    }

    $('#dvPlanesNuevosMigracion').PlansMigrationsDetail();
})(jQuery);
