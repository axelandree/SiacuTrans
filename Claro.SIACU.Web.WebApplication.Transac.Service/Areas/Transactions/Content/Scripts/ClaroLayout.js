function showDialogManagementInstant() {

    var that = this,

     strUrl = window.location.href;
    var strUrlTemplate = window.location.href + '/Home/DialogTemplate';
    var valValueSearch = $("#txtCriteriaValue").val();
    var valPhone = valValueSearch;

    var valAccount = Session.DATACUSTOMER.Account;
    var valContract = Session.DATACUSTOMER.ContractID;
    var valOriginType = Session.ORIGINTYPE;
    //console.log"este es: " + valOriginType)
    var strUrlModal = strUrl + '/Management/PrepaidInstant/Index';
    $.window.open({
        type: 'post',
        modal: false,
        title: "Instantaneas",
        template: strUrlTemplate,
        url: strUrlModal,
        data: { strIdSession: Session.IDSESSION, strPhone: valPhone, strOriginType: valOriginType },
        width: 900,
        height: 600,
        buttons: {
            Cerrar: {
                click: function (sender, args) {
                    this.close();
                }
            }
        }
    });
}

function blockUI(div) {
    var stUrlLogo = window.location.href + "/Images/loading2.gif";
    $(div).html('').append('<div align="center"><img src="' + stUrlLogo + '" width="25" height="25" /> Cargando .... </div>');
}

function showDialogSearch() {
    $.window.open({
        modal: false,
        title: "CONSULTA PRODUCTOS",
        url: 'Dashboard/Home/CustomersNames',
        width: 900,
        height: 600,
        buttons: {
            Seleccionar: {
                click: function (sender, args) {
                    var controls = $('#contenedor-customerNames').form('getControls'),
                        dataRow = controls.tblDatosCliente.DataTable().row({ selected: true }).data();

                    if (!$.isEmptyObject(dataRow)) {
                        Session.CUSTOMERPRODUCT = {};
                        Session.CUSTOMERPRODUCT.isRedirecBussines = 'ok';
                        Session.CUSTOMERPRODUCT.DocumentType = dataRow.DocumentType;
                        Session.CUSTOMERPRODUCT.DocumentIdentity = dataRow.DocumentIdentity;
                        Session.CUSTOMERPRODUCT.Names = dataRow.Names;

                        $.window.open({
                            modal: false,
                            title: "CONSULTA PRODUCTOS",
                            url: 'Dashboard/Home/Products',
                            width: 900,
                            height: 600,
                            buttons: {
                                Cerrar: {
                                    click: function () {
                                        this.close();
                                    }
                                }
                            }
                        });
                        this.close();


                    } else {
                        alert('Debe seleccionar un cliente.', this.strTitleMessage);
                        return;
                    }
                }
            },
            Cancelar: {
                click: function (sender, args) {
                    $('#ddlTipoBusqueda').val('');
                    $('#txtCriteriaValue').val('');
                    $('#btnSearch').html("Buscar");

                    this.close();
                }
            }
        }
    });

}

var btnDynamic;

(function ($, undefined) {



    var Form = function ($element, options) {

        $.extend(this, $.fn.form.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element,
            body: $('body'),
            btnCriteriaSearch: $('#btnCriteriaSearch', $element),
            btnResult: $('#btnResult', $element),
            btnInstant: $('#btnInstant', $element),
            btnCerrarInstant: $('#btncerrarinstant', $element),
            btnSearch: $('#btnSearch', $element),
            btnClean: $('#btnClean', $element),
            divButtonSearch: $('#div-button-search .dropdown-menu li a', $element),
            divInstant: $('#divInstant', $element),
            ddlTipoBusqueda: $('#ddlTipoBusqueda', $element),
            //divcontentprepaid: $('#divcontentprepaid', $element),
            divMainBody: $('#navbar-body'),
            divMainHeader: $('#main-header'),
            divMainFooter: $('#main-footer'),
            divModalError: $('#divModalError', $element),
            divContenido: $('#divContenido', $element),
            divPostLineInfo: $('#divPostLineInfo', $element),
            flagDivInstant: $('#flagDivInstant', $element),
            idmMenu: $('#idmMenu', $element),
            myModal: $('#myModal', $element),
            myModalLoad: $('#myModalLoad', $element),
            txtCriteriaValue: $('#txtCriteriaValue', $element),

            idmToolbar: $('#idmToolbar', $element),
            idulListPerfiles: $('#idulListPerfiles', $element),
            uiBarra: $('#uiBarra', $element),
            idUserName: $('#idUserName', $element),
            idSession: $('#idSession', $element),
            btnCriteria: $('#btnCriteria', $element),
            ddlBusquedaDesplegable: $('#ul-button-Search', $element),
            nroNodo: $('#nroNodo', $element),

        });

    }

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this,
                controls = this.getControls();
            
            Session.ORIGINTYPE = '';

            controls.divButtonSearch.addEvent(that, 'click', that.btnValidateTypeSearch_click);
            controls.body.addEvent(that, 'click', that.body_click);
            controls.divInstant.addEvent(that, 'click', that.divInstant_click);
            controls.btnCriteriaSearch.addEvent(that, 'click', that.btnCriteriaSearch_click);
            controls.btnResult.addEvent(that, 'click', that.searchbyDocumentNumber);
            controls.btnInstant.addEvent(that, 'click', that.btnInstant_click);
            controls.btnCerrarInstant.addEvent(that, 'click', that.btnCerrarInstant_click);
            controls.btnClean.addEvent(that, 'click', that.btnClean_click);
            $(window).addEvent(that, 'resize', that.resizeContent);

            that.render();
        },

        render: function () {
            this.createMenu();
        },
        addAttributeParentProduct: function (data, strValue) {
            for (var i = 0; i < data.length; i++) {
                data[i].parentProduct = strValue;
                if (data[i].items != null && data[i].items.length > 0) {
                    data[i].items = this.addAttributeParentProduct(data[i].items, strValue);
                }
            }
            return data;
        },
        addAttributeDescriptionAux: function (data, strValue) {
            for (var i = 0; i < data.length; i++) {
                data[i].parentProduct = strValue;
                if (data[i].isDefault == true) {
                    data[i].descriptionAux = strValue;
                }
                if (data[i].items != null && data[i].items.length > 0) {
                    data[i].items = this.addAttributeDescriptionAux(data[i].items, strValue);
                }
            }
            return data;
        },
        showDialogLoad: function () {


            var strUrl = window.location.href;

            var strUrlTemplate = strUrl + '/Home/DialogTemplate';
            var controls = this.getControls();
            var valValueSearch = $("#txtCriteriaValue").val();
            var valPhone = valValueSearch;
            var valAccount = Session.DATACUSTOMER.Account;
            var valContract = Session.DATACUSTOMER.ContractID;
            var valOriginType = Session.ORIGINTYPE;

            if (valOriginType == "PREPAID") {
                var strUrlFillInstantPrepaid = strUrl + '/Dashboard/Home/FillInstantPrepaid';
                var strUrlInstantPrepaid = strUrl + '/Dashboard/Home/InstantsPrepaid';
                var dataInstantPrepaid = { strIdSession: Session.IDSESSION, strPhone: valPhone, strOriginType: valOriginType };
                $.app.ajax({
                    async: false,
                    type: 'POST',
                    url: strUrlFillInstantPrepaid,
                    data: dataInstantPrepaid,
                    success: function (response) {
                        if (response.listInstant.length > 0) {
                            $.window.open({
                                type: 'POST',
                                buttons: {
                                    Cerrar: {
                                        click: function () {
                                            $('#hdnCantReg').val("0");
                                            Session.IDINSTANT = $('#hdnIdInstants').val();
                                            Session.DESCRIPTIONINSTANT = $('#hdnDescriptionInstants').val();
                                            Session.ORIGINAPP = 'PREPAGO';
                                            var flag = 1;

                                            for (var i = 0; i < response.listInstant.length; i++) {
                                                if (response.listInstant[0].Description == "CLIENTE TIENE PENDIENTE ACTUALIZACIÓN DE DATOS. Preguntar al cliente lo siguiente: Estimado cliente, usted es la persona que utiliza el servicio de manera permanente?. Si la respuesta es SI, indicar lo siguiente: Hemos verificado que necesitamos actualizar sus datos a fin de atender sus solicitudes sin ningún inconveniente Si la respuesta es NO, marcar No deseo ser titular") {

                                                    flag = 0;
                                                    break;
                                                }
                                            }
                                            if (flag == 1) {
                                                this.close();

                                            } else {
                                                $.redirect.GetParamsData('SU_SIACA_INS', "PREPAGO");

                                            }

                                        }
                                    }

                                },
                                title: "Instantaneas",
                                template: strUrlTemplate,
                                url: strUrlInstantPrepaid,
                                data: response,
                                width: 900,
                                height: 600,
                                closeOnEscape: false

                            });
                        } else {
                            $('#hdnCantReg').val("0");
                            $('#spidnotify').html("0");
                        }
                    },
                    error: function (ex) {
                        controls.divPostLineInfo.showMessageErrorLoading($.app.const.messageErrorLoading);
                    }
                });


            } else {
                var strUrlFillInstant = strUrl + '/Dashboard/Home/FillInstant';

                var strUrlInstant = strUrl + '/Dashboard/Home/Instants';
                var dataInstant = { strIdSession: Session.IDSESSION, strPhone: valPhone, strAccount: valAccount, strContract: valContract, strOriginType: valOriginType };
                $.app.ajax({
                    async: false,
                    type: 'POST',
                    url: strUrlFillInstant,
                    data: dataInstant,
                    success: function (response) {
                        if (response.listInstant.length > 0) {
                            $.window.open({
                                type: 'POST',
                                modal: false,
                                title: "Instantaneas",
                                template: strUrlTemplate,
                                url: strUrlInstant,
                                data: response,
                                width: 900,
                                height: 600,
                                buttons: {
                                    Cerrar: {
                                        click: function (sender, args) {
                                            this.close();
                                        }
                                    }
                                }
                            });
                        }
                    },
                    error: function (ex) {
                        controls.divPostLineInfo.showMessageErrorLoading($.app.const.messageErrorLoading);
                    }
                });


            }
        },
        aMenuReclamos_click: function (send, args) {
            var strUrl = window.location.href;
            var strUrlTemplate = strUrl + '/Home/DialogTemplate';

            $.window.open({
                modal: false,
                cache: false,
                title: "",
                iconClass: 'glyphicon-comment',
                text: 'Usted tiene un servicio consultado previamente, se limpiará la información consultada anteriormente, ¿Desea continuar?',
                buttons: {
                    Ok: {
                        click: function () {
                            this.close();
                            location.reload();
                            $.redirect.GetParamsData('SU_REC_RSGA', "RECLAMOS");
                        }
                    },
                    Cancelar: {
                        click: function () {
                            this.close();
                        }
                    },
                },
                template: strUrlTemplate,
                width: 400,
                height: 200,
            });

        },
        aMenuComplete_click: function (send, args) {
            var that = this;
            Session.ORIGINAPP = this.parentProduct;

            var strTittle = "";
            if ('SU_REC_RSGA' == that.code.toString()) {
                $.redirect.GetParamsData('SU_REC_RSGA', "RECLAMOS");
            }
            else if ('SU_ACP_AIN' == that.code.toString() || 'SU_SIACA_INST' == that.code.toString() || 'SU_HFC_LIN' == that.code.toString() || 'SU_ACP_PEP' == that.code.toString() || 'SU_SIACA_PEP' == that.code.toString()) {
                $.ValidateCode(this.code, Session.ORIGINAPP, null, true);
            }
            else if ("SU_ACP_IL" == that.code.toString()) {
                if (Session.ORIGINTYPE != '') {
                    if (Session.ORIGINTYPE == "POSTPAID" || Session.ORIGINTYPE == "DTH" || Session.ORIGINTYPE == "HFC" || Session.ORIGINTYPE == "LTE") {
                        if (Session.DATASERVICE == null) {
                            $.app.ajax({
                                async: false,
                                type: 'POST',
                                url: window.location.href.concat('/Dashboard/Postpaid/DataServiceContent'),
                                data: {
                                    strIdSession: Session.IDSESSION,
                                    strContratoID: Session.DATACUSTOMER.ContractID,
                                    strApplication: Session.DATACUSTOMER.Application,
                                    strCustomerType: Session.DATACUSTOMER.objPostDataAccount.CustomerType,
                                    strDocumentType: Session.DATACUSTOMER.DocumentType,
                                    strDocumentNumber: Session.DATACUSTOMER.DocumentNumber,
                                    strModality: Session.DATACUSTOMER.objPostDataAccount.Modality,
                                    strIsLTE: Session.DATACUSTOMER.IsLTE,
                                    strphone: Session.DATACUSTOMER.ValueSearch,
                                },
                                success: function (response) {
                                    Session.DATASERVICE = response.data;
                                },
                                error: function (ex) {
                                    controls.divPostLineInfo.showMessageErrorLoading($.app.const.messageErrorLoading);
                                }
                            });
                        }
                    }
                    Session.ORIGINAPP = this.parentProduct;
                   // Session.CO = this.id;
                    $.ValidateCode(this.code, Session.ORIGINAPP, null, true);
                }
            }
            else {
                if (Session.DATASERVICE == null) {
                    var obj = this;
                    $.app.ajax({
                        async: false,
                        type: 'POST',
                        url: window.location.href.concat('/Dashboard/Postpaid/DataServiceContent'),
                        data: {
                            strIdSession: Session.IDSESSION,
                            strContratoID: Session.DATACUSTOMER.ContractID,
                            strApplication: Session.DATACUSTOMER.Application,
                            strCustomerType: Session.DATACUSTOMER.objPostDataAccount.CustomerType,
                            strDocumentType: Session.DATACUSTOMER.DocumentType,
                            strDocumentNumber: Session.DATACUSTOMER.DocumentNumber,
                            strModality: Session.DATACUSTOMER.objPostDataAccount.Modality,
                            strIsLTE: Session.DATACUSTOMER.IsLTE,
                            strphone: Session.DATACUSTOMER.ValueSearch,
                        },
                        success: function (response) {
                            Session.DATASERVICE = response.data;
                        },
                        error: function (ex) {
                            controls.divPostLineInfo.showMessageErrorLoading($.app.const.messageErrorLoading);
                        }
                    });
                }
                //Session.CO = this.id;
                $.ValidateCode(this.code, Session.ORIGINTYPE);

            }

        },
        btnValidateTypeSearch_click: function (send, args) {
            var controls = this.getControls();
            var strName = send.attr('data-value');

            if (strName != "6") {
                controls.ddlTipoBusqueda.val(send.attr('data-value'));
                controls.btnSearch.html(send.text() + " <span class='caret'></span>");
            } else {
                controls.ddlTipoBusqueda.val("6");
            }
            this.initialiceValidationSearch(send);
        },
        btnCriteriaSearch_click: function (send, args) {
            this.validationSearch();
        },
        btnInstant_click: function (send, args) {
            var strUrl = window.location.href;
            var controls = this.getControls(),
                valValueSearch = Session.DATASERVICE.NumberCellphone,
                valOriginType = Session.ORIGINTYPE,
                objInstantPrepaid = {
                    instantPhone: null,
                    instantID: null,
                    instantDescription: null
                };
            if ((valValueSearch == undefined || valValueSearch == "") && valOriginType == "") {
                alert('Debes ingresar un valor válido para la búsqueda', 'Mensaje');
                $(this).focus();
                controls.txtCriteriaValue.val('');
            }
            else {
                $('#btnInstant').prop('title', 'Instantaneas');
                $('#spidnotify').html('');
                var valPhone = valValueSearch;
                var valAccount = Session.DATACUSTOMER.Account;
                var valContract = Session.DATACUSTOMER.ContractID;
                var valOriginType = Session.ORIGINTYPE;
                var strUrlTemplate = strUrl + '/Home/DialogTemplate';

                if (valOriginType == "PREPAID") {
                    var strUrlFillInstantPrepaid = strUrl + '/Dashboard/Home/FillInstantPrepaid';
                    var strUrlInstantPrepaid = strUrl + '/Dashboard/Home/InstantsPrepaid';
                    var dataInstantPrepaid = { strIdSession: Session.IDSESSION, strPhone: valPhone, strOriginType: valOriginType };
                    $.app.ajax({
                        async: false,
                        type: 'POST',
                        url: strUrlFillInstantPrepaid,
                        data: dataInstantPrepaid,
                        success: function (response) {
                            $.window.open({
                                type: 'POST',
                                buttons: {
                                    Cerrar: {
                                        click: function () {
                                            $('#hdnCantReg').val("0");
                                            Session.IDINSTANT = $('#hdnIdInstants').val();
                                            Session.DESCRIPTIONINSTANT = $('#hdnDescriptionInstants').val();
                                            Session.ORIGINAPP = 'PREPAGO';
                                            var flag = 1;

                                            for (var i = 0; i < response.listInstant.length; i++) {
                                                if (response.listInstant[0].Description == "CLIENTE TIENE PENDIENTE ACTUALIZACIÓN DE DATOS. Preguntar al cliente lo siguiente: Estimado cliente, usted es la persona que utiliza el servicio de manera permanente?. Si la respuesta es SI, indicar lo siguiente: Hemos verificado que necesitamos actualizar sus datos a fin de atender sus solicitudes sin ningún inconveniente Si la respuesta es NO, marcar No deseo ser titular") {

                                                    flag = 0;
                                                    break;
                                                }
                                            }
                                            if (flag == 1) {
                                                this.close();

                                            } else {
                                                $.redirect.GetParamsData('SU_SIACA_INS', "PREPAGO");

                                            }

                                        }
                                    }

                                },
                                title: "Instantaneas",
                                template: strUrlTemplate,
                                url: strUrlInstantPrepaid,
                                data: response,
                                width: 900,
                                height: 600,
                                closeOnEscape: false

                            });

                        }
                    });
                }
                else {
                    var strUrlFillInstant = strUrl + '/Dashboard/Home/FillInstant';
                    var strUrlInstant = strUrl + '/Dashboard/Home/Instants';
                    var dataInstant = { strIdSession: Session.IDSESSION, strPhone: valPhone, strAccount: valAccount, strContract: valContract, strOriginType: valOriginType };
                    $.app.ajax({
                        async: false,
                        type: 'POST',
                        url: strUrlFillInstant,
                        data: dataInstant,
                        success: function (response) {
                            $.window.open({
                                type: 'POST',
                                modal: false,
                                title: "Instantaneas",
                                template: strUrlTemplate,
                                url: strUrlInstant,
                                data: response,
                                width: 900,
                                height: 600,
                                buttons: {
                                    Cerrar: {
                                        click: function (sender, args) {
                                            this.close();
                                        }
                                    }
                                }

                            });
                        }
                    });
                }
            }
        },
        btnCerrarInstant_click: function (send, args) {
        },
        btnClean_click: function (send, args) {
            var controls = this.getControls();

            if (controls.txtCriteriaValue.val() != "") {
                controls.txtCriteriaValue.val("");
            }
        },
        body_click: function (send, args) {
            var controls = this.getControls();
        },
        createMenu: function () {
            var that = this,
                controls = this.getControls();
            $.blockUI({
                message: controls.myModalLoad,
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

            $.app.ajax({
                type: 'POST',
                dataType: 'json',
                url: window.location.href + '/Home/Logon',
                data: null,
                success: function (result) {
                    if (result.data.employee != null) {
                        $("#txtCriteriaValue").attr('maxlength', result.SearchLength);
                        Session.USERACCESS = result.data.employee;
                        Session.IDSESSION = result.idSession;
                        Session.NODO = result.nroNodo;
                        that.toAssignSession(Session.IDSESSION, controls.idSession);
                        controls.nroNodo.append('<b>' + Session.NODO + '</b>');
                        if (result.data != null) {
                            //Inicio Listar perfiles por usuario
                            that.tolistProfilesUser(result.data.profiles, controls.idulListPerfiles);
                            //Fin Listar perfiles por usuario

                            //Inicio de asignar nombre de usuario a etiqueta
                            that.toAssignUser(result.data.employee, controls.idUserName, controls.btnCriteria);

                            that.toFillCombo(result.data.options, controls.ddlBusquedaDesplegable)
                            var divButtonSearch = $('#div-button-search .dropdown-menu li a');
                            divButtonSearch.addEvent(that, 'click', that.btnValidateTypeSearch_click);
                            //Fin de Asignar nombre de usuario

                            result.data = that.newFieldData(result.data.menu);
                            Session.USERACCESS.optionPermissionsMenu = result.OptionPermissionsMenu;

                            for (var i = 0; i < result.data.length; i++) {
                                if (result.data[i].items != null) {
                                    result.data[i].items = that.addAttributeDescriptionAux(result.data[i].items, result.data[i].name);
                                }
                            }

                            var menu = new Object();
                            menu[0] = result.data[0];
                            menu.length = 1;

                            for (var i = 1; i < result.data.length ; i++) {
                                if (result.data[i].items != null) {
                                    menu[0].items = that.getReorderParent(menu[0].items, result.data[i].items);
                                    menu[0].items = menu[0].items.concat(result.data[i].items);
                                }
                            }

                            menu[0].items = that.getValidateOptionPopup(menu[0].items);

                            //Inicio Reordenar Items Menus por name
                            //Inicio Hijos Menus 
                            for (var i = 0; i < menu[0].items.length ; i++) {
                                menu[0].items[i].items.sort(function (a, b) {
                                    var nameA = a.name.toLowerCase(), nameB = b.name.toLowerCase()
                                    if (nameA < nameB) //sort string ascending
                                        return -1
                                    if (nameA > nameB)
                                        return 1
                                    return 0 //default return value (no sorting)
                                });
                                //Inicio Nietos Menus
                                for (var x = 0; x < menu[0].items[i].items.length ; x++) {
                                    if (menu[0].items[i].items[x].items != null) {
                                        menu[0].items[i].items[x].items.sort(function (a, b) {
                                            var nameA = a.name.toLowerCase(), nameB = b.name.toLowerCase()
                                            if (nameA < nameB) //sort string ascending
                                                return -1
                                            if (nameA > nameB)
                                                return 1
                                            return 0 //default return value (no sorting)
                                        });
                                    }

                                }
                                //Fin Nietos Menus
                            }
                            //Fin Hijos Menus
                            //Fin Reordenar Items Menus por name      

                            var strMenu = that.getMenu(menu, "Y");
                            controls.idmMenu.html(strMenu);
                            $.SmartMenus.Bootstrap.init();

                            $('#divContenido').html('');
                            $.unblockUI();
                        }

                        that.resizeContent();
                    } else {

                        $('#navbar-body').html('<br/><div align="center"><label class="control-label">Estimado usuario, usted no tiene acceso al aplicativo.  </label></div><br/>');
                    }
                },
                error: function (msg) {
                    $.unblockUI();
                    error({
                        id: 'divContenido',
                        message: msg,
                        click: function () { that.createMenu() }
                    });
                }
            });
        },
        toAssignUser: function (data, control1, control2) {
            var strFullName = '';
            var strIdCodigo = '';
            var strSite = '';
            strFullName = data.fullName;
            strIdCodigo = data.login;
            strSite = data.areaName;
            control1.append('<b>' + strIdCodigo + ' - ' + strFullName + '</b>');
            if (strSite != '') {
                control2.append('<span class="glyphicon glyphicon-cog text-left" style="width:185px;"> <label style="font-family: Arial, Verdana; font-weight:bold;"> ' + strSite + ' </label> </span>');
            } else {
                control2.append('');
            }

        },
        toAssignSession: function (data, control1) {
            control1.append('<b>Session ID: ' + data + '</b>');
        },
        toFillCombo: function (data, control1) {

            for (var i = 0; i < data.length; i++) {
                if (data[i] != null) {
                    if (data[i].name == "Teléfono") {
                        control1.append('<li><a data-value="' + data[i].code + '"><span class="glyphicon glyphicon-earphone"></span> &nbsp; ' + data[i].name + '</a></li>');
                    }
                    else if (data[i].name == "Cuenta") {
                        control1.append('<li><a data-value="' + data[i].code + '"><span class="glyphicon glyphicon-credit-card"></span> &nbsp; ' + data[i].name + '</a></li>');
                    }
                    else if (data[i].name == "Contrato") {
                        control1.append('<li><a data-value="' + data[i].code + '"><span class="glyphicon glyphicon-list-alt"></span> &nbsp; ' + data[i].name + '</a></li>');
                    }
                    else if (data[i].name == "Customer Id") {
                        control1.append('<li><a data-value="' + data[i].code + '"><span class="glyphicon glyphicon-king"></span> &nbsp; ' + data[i].name + '</a></li>');
                    }
                    else if (data[i].name == "N° Documento Identidad") {
                        control1.append('<li><a data-value="' + data[i].code + '"><span class="glyphicon glyphicon-eye-close"></span> &nbsp; ' + data[i].name + '</a></li>');
                    }
                    else if (data[i].name == "Nombres") {
                        control1.append('<li><a data-value="' + data[i].code + '" onclick="showDialogSearch()"><span class="glyphicon glyphicon-tasks"></span> &nbsp; ' + data[i].name + '</a></li>');
                    }
                    else if (data[i].name == "Razón Social") {
                        control1.append('<li><a data-value="' + data[i].code + '"><span class="glyphicon glyphicon-th-large"></span> &nbsp; ' + data[i].name + '</a></li>');
                    }
                    else if (data[i].name == "Número de Recibo") {
                        control1.append('<li><a data-value="' + data[i].code + '"><span class="glyphicon glyphicon-book"></span> &nbsp; ' + data[i].name + '</a></li>');
                    }
                    else if (data[i].name == "ICCID (Número de Chip)") {
                        control1.append('<li><a data-value="' + data[i].code + '"><span class="glyphicon glyphicon-erase"></span> &nbsp; ' + data[i].name + '</a></li>');
                    }
                    else if (data[i].name == "Cintillo") {
                        control1.append('<li><a data-value="' + data[i].code + '"><span class="glyphicon glyphicon-tent"></span> &nbsp; ' + data[i].name + '</a></li>');
                    }
                    else {
                        control1.append('<li><a data-value="' + data[i].code + '"><span class="glyphicon glyphicon-th-list"></span> &nbsp; ' + data[i].name + '</a></li>');
                    }
                }
            }


        },
        tolistProfilesUser: function (data, control) {

            for (var i = 0; i < data.length; i++) {
                if (data[i] != null) {
                    control.append('<li><a data-value="' + data[i].id + '"><span class="glyphicon glyphicon-chevron-right"></span>' + data[i].name + '</a></li>');
                }
            }


        },

        createMenuDefault: function (data, codeOption, counter) {

            if (data != null && data.length > 0) {

                var $ul = $('<ul>');

                if (counter != 0)

                    $ul.addClass('dropdown-menu');

                else

                    $ul.addClass('nav navbar-nav');


                for (var i = 0; i < data.length; i++) {
                    if (data[i].items == null && counter == 0) {
                    } else {
                        if (data[i].state == "1") {

                            var $li = $('<li>'),
                                $a = $('<a TypeOptions="1" profile="' + data[i].code + '">');

                            if (data[i].items != null && data[i].items.length > 0) {

                                $a.html(data[i].name);

                                $li
                                    .addClass('has-sub')
                                    .append($a)
                                    .append(this.createMenuDefault(data[i].items, null, 1));

                            }
                            else {

                                $a
                                   .text(data[i].name)
                                   .addEvent(data[i], 'click', this.openPopup_click);
                                $a.data('CodeValidate', 1);
                                $li.append($a);

                            }
                        }
                    }
                    $ul.append($li);
                }
            }

            return $ul;
        },
        createMenuComplete: function (data, codeOption, counter) {

            if (data != null && data.length > 0) {

                var $ul = $('<ul>');

                if (counter != 0)

                    $ul.addClass('dropdown-menu');

                else
                    $ul.addClass('nav navbar-nav');
                for (var i = 0; i < data.length; i++) {

                    if (data[i].items == null && counter == 0) {
                    } else {

                        var $li = $('<li>'),
                            $a = $('<a TypeOptions="1" profile="' + data[i].code + '">');

                        if (data[i].items != null && data[i].items.length > 0) {

                            $a.html(data[i].name + '&nbsp; &nbsp;');

                            $li
                                .addClass('has-sub')
                                .append($a)
                                .append(this.createMenuComplete(data[i].items, null, 1));

                        }
                        else {

                            if ((data[i].parentProduct) == "RECLAMOS") {
                                $a
                               .text(data[i].name)
                               .addEvent(data[i], 'click', this.aMenuReclamos_click);
                                $a.data('CodeValidate', 3);
                            } else {
                                $a
                               .text(data[i].name)
                               .addEvent(data[i], 'click', this.aMenuComplete_click);
                                $a.data('CodeValidate', 4);
                            }

                            $li.append($a);


                        }
                    }
                    $ul.append($li);
                }
            }

            return $ul;
        },
        divInstant_click: function (send, args) {
            var controls = this.getControls();
        },
        enterValidationSearch: function () {

            var that = this,
                controls = this.getControls();

            controls.txtCriteriaValue.keyup(function (e) {

                if (e.keyCode == 13)

                    that.validationSearch();

            });

        },
        getControls: function () {
            return this.m_controls || {};
        },
        getReorderParent: function (menu, data) {
            for (var i = 0; i < menu.length; i++) {
                for (var j = 0; j < data.length; j++) {
                    if (menu[i].postfix == data[j].postfix && data[j].state == "1" && data[j].items != null) {
                        if (data[j].items != null) {
                            menu[i].items = this.getReorderParent(menu[i].items, data[j].items)
                            menu[i].items = menu[i].items.concat(data[j].items);
                        }
                    }
                }

            }
            return menu;
        },
        getValidateOptionPopup: function (data) {
            var datai = data;
            var dataj = data;
            for (var i = 0; i < datai.length; i++) {

                var buttons = [];
                for (var j = i + 1; j < dataj.length; j++) {

                    if (datai[i].postfix == dataj[j].postfix && dataj[j].state == "1") {

                        dataj[j].state = '0';

                        if (datai[i].isDefault == true) {

                            if (datai[i].indicator == '') {
                                buttons.push(datai[i]);
                            }

                            buttons.push(datai[j]);
                            datai[i].indicator = buttons;
                        }
                    }
                }

                if (datai[i].items != null)

                    datai[i].items = this.getValidateOptionPopup(datai[i].items);

            }
            return datai;
        },
        getMenu: function (data, indicator) {
            var $divNav = $('<div>'),
                $divMenu = $('<div>');
            $divNav.addClass('nav');
            $divMenu
                .attr('id', 'cssmenu')
                .addClass('claro-black');

            for (var i = 0; i < data.length; i++) {

                if (indicator == "Y") {

                    $divMenu.append(this.createMenuDefault(data[i].items, null, 0));

                } else {

                    $divMenu.append(this.createMenuComplete(data[i].items, null, 0));

                }
            }

            $divNav
            .append($divMenu)

            return $divNav;
        },
        initialiceValidationSearch: function (send) {
            var $element = send,
                id = $element.data("value"),
                controls = this.getControls();

            if ($.inArray(id, [2]) != -1) {
                $.onlyNumbersPoint(controls.txtCriteriaValue);
            }

            if ($.inArray(id, [1, 3, 4, 5, 9]) != -1) {
                $.onlyNumbers(controls.txtCriteriaValue);
            }

            if ($.inArray(id, [6]) != -1) {
                $.onlyNumbersLetters(controls.txtCriteriaValue);
            }

            if ($.inArray(id, [7]) != -1) {
                $.onlyLettersSpaces(controls.txtCriteriaValue);
            }

            if ($.inArray(id, [8]) != -1) {
                $.onlyNumbersLettersLine(controls.txtCriteriaValue);
            }
            controls.txtCriteriaValue.val("").focus();
            this.enterValidationSearch();
        },
        newFieldData: function (data) {

            for (var i = 0; i < data.length; i++) {
                data[i].state = "1";
                data[i].indicator = "";
                data[i].postfix = data[i].code.substr(parseInt(data[i].code.length - 4), 4);
                if (data[i].items != null && data[i].items.length > 0) {
                    data[i].items = this.newFieldData(data[i].items);
                }
            }
            return data;
        },
        openPopup_click: function (send, args) {

            var that = this;
            var strUrl = window.location.href + '/Home/OptionsDefault';
            var strUrlTemplate = window.location.href + '/Home/DialogTemplate';
            //console.log'modal openpon');
            //console.logthis);
            if (that.indicator != '') {
                $.window.open({
                    buttons: {
                        Aceptar: {
                            click: function () {
                                $.redirect.GetParamsData(that.code.toString(), Session.ORIGINAPP);                             
                                this.close();
                            }
                        },
                        Cancelar: {
                            click: function (sender, args) {
                                this.close();
                            }
                        }
                    },
                    modal: false,
                    title: "Seleccione Producto",
                    template: strUrlTemplate,
                    url: strUrl,
                    width: 500,
                    height: 400,
                    maximize: false,
                    minimize: false
                });

                btnDynamic = that.indicator;

            }

        },
        searchbyDocumentNumber: function () {
            if (Session.CUSTOMERPRODUCT != null) {
                var strUrl = window.location.href + '/Dashboard/Home/Products';
                var strUrlTemplate = window.location.href + '/Home/DialogTemplate';
                $.window.open({
                    type: 'post',
                    modal: false,
                    title: "CONSULTA PRODUCTOS",
                    template: strUrlTemplate,
                    url: strUrl,
                    width: 900,
                    height: 600,
                    buttons: {
                        Cerrar: {
                            click: function (sender, args) {
                                this.close();
                            }
                        }
                    }
                });
            }



        },
        setControls: function (value) {
            this.m_controls = value;
        },
        sliderToolBar: function () {
            $('#carousel_ul li:first').before($('#carousel_ul li:last'));

            $('#right_scroll span').on('click', function () {
                var item_width = $('#carousel_ul li').outerWidth() + 10,
                    left_indent = parseInt($('#carousel_ul').css('left')) - item_width;

                $('#carousel_ul:not(:animated)').animate({ 'left': left_indent }, 500, function () {
                    $('#carousel_ul li:last').after($('#carousel_ul li:first'));
                    $('#carousel_ul').css({ 'left': '-10px' });
                });
            });

            $('#left_scroll span').on('click', function () {
                var item_width = $('#carousel_ul li').outerWidth() + 10,
                    left_indent = parseInt($('#carousel_ul').css('left')) + item_width;

                $('#carousel_ul:not(:animated)').animate({ 'left': left_indent }, 500, function () {
                    $('#carousel_ul li:first').before($('#carousel_ul li:last'));
                    $('#carousel_ul').css({ 'left': '-10px' });
                });
            });
        },
        validationSearch: function () {
            var that = this,
                controls = this.getControls();

            $('#spidnotify').html('');

            Session.CUSTOMERPRODUCT = null;

            Session.TELEPHONE = controls.txtCriteriaValue.val();
            controls.uiBarra.html("");

            var valSearchType = controls.ddlTipoBusqueda.val(),
             valSearchValue = controls.txtCriteriaValue.val(),
             strUrl = window.location.href,
             strUrlModal = '',
             strUrlTemplate = strUrl + '/Home/DialogTemplate',
             strTitleMessage = 'Busqueda';
            Session.SEARCHTYPE = valSearchType;

            // Validar Tipo de Busqueda
            if (that.validate(valSearchType, valSearchValue)) {

                $.blockUI({
                    message: controls.myModalLoad,
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

                if (valSearchType == '1' || valSearchType == '2' || valSearchType == '3' || valSearchType == '4' || valSearchType == '8' || valSearchType == '9' || valSearchType == '10') {

                    var strUrl = window.location.href + '/Home/ValidateQuery';
                    var data = { strIdSession: Session.IDSESSION, strSearchType: valSearchType, strSearchValue: valSearchValue }
                    $.app.ajax({
                        type: "POST",
                        dataType: "json",
                        url: strUrl,
                        data: data,
                        complete: function () {
                            if (data.strSearchType == '4' || data.strSearchType == '8') {

                                $('#btnSearch').html('   Cuenta <span class="caret"></span>');
                                controls.ddlTipoBusqueda.attr("value", 2);
                                controls.txtCriteriaValue.val(Session.DATACUSTOMER.Account);
                            }
                            if (data.strSearchType == '3' || data.strSearchType == '1') {
                                that.showDialogLoad();
                            }
                        },
                        success: function (result) {
                            if ((result.OriginType != "" && result.data != null)) {
                                if (result.OriginType != "NOPRECISADO")
                                    that.builtContent({ paramResult: result, paramSearchValue: valSearchValue });
                                else {
                                    $(this).focus();
                                    $('#txtCriteriaValue').val('');
                                    alert('No se encontraron resultados', this.strTitleMessage);
                                    $('#divContenido').html("");
                                }
                            } else {
                                $(this).focus();
                                $('#txtCriteriaValue').val('');
                                alert('No se encontraron resultados', this.strTitleMessage);
                                $('#divContenido').html("");
                            }

                        },
                        error: function (msger) {
                            error({
                                id: 'divContenido',
                                message: msger,
                                click: function () { that.validationSearch() }
                            });
                        }
                    });
                }
                else if (valSearchType == "5") {
                    //Validación previa para saber el tipo de opción
                    var valTypeSearchCode = '1',
                        valValueSearch = $('#txtCriteriaValue').val();

                    var data = { strIdSession: Session.IDSESSION, strSearchType: valTypeSearchCode, strSearchValue: valValueSearch };

                    $.app.ajax({
                        type: "POST",
                        dataType: "json",
                        url: 'Dashboard/Home/CustomerValidate',
                        data: data,
                        success: function (result) {
                            Session.CUSTOMERPRODUCT = {};
                            Session.CUSTOMERPRODUCT = result.data;

                            if (result.data != null) {
                                if (result.data.listDataCustomerModel.length > 1) {
                                    $.window.open({
                                        buttons: {
                                            Seleccionar: {
                                                click: function () {
                                                    var $this = this,
                                                        selectedData = $('#tblCustomersDocument', '#contenedor-customers-document').DataTable().rows({ selected: true }).data();

                                                    if ($.array.isEmptyOrNull(selectedData)) {
                                                        alert('Debe seleccionar un cliente.', this.strTitleMessage);
                                                        return;
                                                    }

                                                    var data = selectedData[0],
                                                        DocumentType = data.DocumentType.trim(),
                                                        DocumentIdentity = data.DocumentIdentity.trim(),
                                                        Names = data.Names.trim();

                                                    Session.CUSTOMERPRODUCT.isRedirecBussines = 'ok';
                                                    Session.CUSTOMERPRODUCT.DocumentType = DocumentType;
                                                    Session.CUSTOMERPRODUCT.DocumentIdentity = DocumentIdentity;
                                                    Session.CUSTOMERPRODUCT.Names = Names;

                                                    this.close();

                                                    $.window.open({
                                                        modal: false,
                                                        title: "Consulta de Productos",
                                                        url: 'Dashboard/Home/Products',
                                                        width: 900,
                                                        height: 600,
                                                        buttons: {
                                                            Cerrar: {
                                                                click: function (sender, args) {
                                                                    this.close();
                                                                }
                                                            }
                                                        }
                                                    });
                                                }
                                            },
                                            Cancelar: {
                                                click: function () {

                                                    that.getControls().ddlTipoBusqueda.val('');
                                                    that.getControls().txtCriteriaValue.val('');
                                                    that.getControls().btnSearch.html("Buscar");

                                                    this.close();

                                                }
                                            }
                                        },
                                        type: 'POST',
                                        modal: false,
                                        title: "Consulta de Clientes",
                                        url: 'Dashboard/Home/CustomersDocument',
                                        width: 900,
                                        height: 500,
                                    });
                                }
                                else {
                                    Session.CUSTOMERPRODUCT.isRedirecBussines = 'not';
                                    $.window.open({
                                        type: 'post',
                                        modal: false,
                                        title: 'Consulta de Productos',
                                        template: strUrlTemplate,
                                        url: 'Dashboard/Home/Products',
                                        width: 900,
                                        height: 600,
                                        buttons: {
                                            Cerrar: {
                                                click: function () {
                                                    this.close();
                                                }
                                            }
                                        }
                                    });

                                }
                            }
                            else {
                                alert("No se pudo realizar la consulta de productos");
                            }
                        },
                        error: function (msger) {

                            alert('El nro de documento de identidad ingresado no devuelve registro alguno.', this.strTitleMessage);
                            $('#divContenido').html('');
                            return;
                        }
                    });
                }
                else if (valSearchType == "7") {
                    $.window.open({
                        modal: false,
                        title: "CONSULTA DE CLIENTES",
                        url: '/Dashboard/Home/CustomersBusinessNames',
                        data: { strBusqueda: valSearchValue, strApellido: "", idModConsul: "1" },
                        width: 900,
                        height: 500,
                        buttons: {
                            Seleccionar: {
                                click: function () {
                                    var controls = $('#contenedor-customerBusinessNames').form('getControls'),
                                        dataRow = controls.tblDatosCliente.DataTable().row({ selected: true }).data();

                                    if (!$.isEmptyObject(dataRow)) {
                                        Session.CUSTOMERPRODUCT = {};
                                        Session.CUSTOMERPRODUCT.isRedirecBussines = 'ok';
                                        Session.CUSTOMERPRODUCT.DocumentType = dataRow.DocumentType;
                                        Session.CUSTOMERPRODUCT.DocumentIdentity = dataRow.DocumentIdentity;
                                        Session.CUSTOMERPRODUCT.Names = dataRow.Names;

                                        $.window.open({
                                            modal: false,
                                            title: "CONSULTA PRODUCTOS",
                                            url: '/Dashboard/Home/Products',
                                            width: 900,
                                            height: 600,
                                            buttons: {
                                                Cerrar: {
                                                    click: function () {
                                                        this.close();
                                                    }
                                                }
                                            }
                                        });

                                        this.close();
                                    } else {
                                        alert('Debe seleccionar un cliente.', this.strTitleMessage);
                                        return;
                                    }
                                }

                            },
                            Cancelar: {
                                click: function () {
                                    that.getControls().ddlTipoBusqueda.val('');
                                    that.getControls().txtCriteriaValue.val('');
                                    that.getControls().btnSearch.html("Buscar");
                                    this.close();

                                }
                            }
                        }
                    });
                    return;
                }
            }
            else {

            }
        },
        validate: function (valSearchType, valSearchValue) {
            var State = true;
            var strTitleMessage = "Buscar";
            var controls = this.getControls();

            if (valSearchType == '') {
                State = false;

                alert('Seleccione un tipo de busqueda e ingrese un criterio de busqueda valido.', this.strTitleMessage);
                $(this).focus();
                controls.txtCriteriaValue.val('');
            }

            // Validando Nro de Teléfono
            if (valSearchType == '1') {

                if (valSearchValue == '') {
                    State = false;

                    alert('Debe Ingresar un Número de Teléfono', this.strTitleMessage);
                    $(this).focus();
                    controls.txtCriteriaValue.val('');
                }

                else if (valSearchValue.length < 8 || valSearchValue.length > 9) {
                    State = false;

                    alert('Número de Teléfono Inválido.', this.strTitleMessage);
                    $(this).focus();
                    controls.txtCriteriaValue.val('');
                    controls.divContenido.html('');
                }

                else if (valSearchValue.length == 9 && valSearchValue.substr(0, 1) != '9' && valSearchValue.substr(0, 2) != '21') {
                    State = false;

                    alert('Número de Teléfono Inválido.', this.strTitleMessage);
                    $(this).focus();
                    controls.txtCriteriaValue.val('');
                    controls.divContenido.html('');
                }

                if (valSearchValue.substr(0, 1) == '0') {
                    State = false;

                    alert('Número Teléfono Inválido, no debe iniciar con Cero(0).', this.strTitleMessage);
                    $(this).focus();
                    controls.txtCriteriaValue.val('');
                }
            }

            // Validando Nro Cuenta 
            if (valSearchType == '2') {

                if (valSearchValue == '') {
                    State = false;

                    alert('Debe Ingresar el Número de la Cuenta', strTitleMessage);
                    $(this).focus();
                    controls.txtCriteriaValue.val('');
                }

                if (valSearchValue.length > 24) {
                    State = false;

                    alert('Número de Cuenta Inválido', strTitleMessage);
                    $(this).focus();
                    controls.txtCriteriaValue.val('');
                }
            }

            // Validando Nro Contrato 
            if (valSearchType == '3') {
                if (valSearchValue == '') {
                    State = false;

                    alert('Debe Ingresar el Número de Contrato', strTitleMessage);
                    $(this).focus();
                    controls.txtCriteriaValue.val('');
                }
            }

            // Validando el Customer Id 
            if (valSearchType == '4') {
                if (valSearchValue == '') {
                    State = false;

                    alert('Debe Ingresar el Customer ID', strTitleMessage);
                    $(this).focus();
                    controls.txtCriteriaValue.val('');
                }

                if (valSearchValue.length > 24) {
                    State = false;

                    alert('Customer ID Inválido', strTitleMessage);
                    $(this).focus();
                    controls.txtCriteriaValue.val('');
                }
            }

            // Validando Nro Documento de Identidad 
            if (valSearchType == '5') {
                if (valSearchValue == '') {
                    State = false;

                    alert('Debe Ingresar un Número de Documento', strTitleMessage);
                    $(this).focus();
                    controls.txtCriteriaValue.val('');
                }
                if (valSearchValue.length > 8) {
                    alert('Debe Ingresar un Número de Documento Correcto', strTitleMessage);
                    $(this).focus();
                    controls.txtCriteriaValue.val('');
                }
            }

            // Validando Razón Social
            if (valSearchType == '7') {
                if (valSearchValue == '') {
                    State = false;

                    alert('Debe Ingresar una Razón Social', strTitleMessage);
                    $(this).focus();
                    controls.txtCriteriaValue.val('');
                }
            }

            // Validando el Customer Id 
            if (valSearchType == '8') {
                if (valSearchValue == '') {
                    State = false;

                    alert('Debe Ingresar el Número de Recibo', strTitleMessage);
                    $(this).focus();
                    controls.txtCriteriaValue.val('');
                }

                if (valSearchValue.substr(4, 1) != '-') {
                    State = false;

                    alert('Número Recibo Inválido', strTitleMessage);
                    $(this).focus();
                    controls.txtCriteriaValue.val('');
                }
                if (valSearchValue.length > 24) {
                    State = false;

                    alert('Número Recibo Inválido', strTitleMessage);
                    $(this).focus();
                    controls.txtCriteriaValue.val('');
                }
            }

            // Validando el ICCID(Nro Chip)
            if (valSearchType == '9') {
                if (valSearchValue == '') {
                    State = false;

                    alert('Debe Ingresar el ICCID(Número de chip)', strTitleMessage);
                    $(this).focus();
                    controls.txtCriteriaValue.val('');
                }

                if (valSearchValue.length > 24) {
                    State = false;

                    alert('Nro ICCID Inválido', strTitleMessage);
                    $(this).focus();
                    controls.txtCriteriaValue.val('');
                }
            }

            // Validando el Cintillo
            if (valSearchType == '10') {
                if (valSearchValue == '') {
                    State = false;

                    alert('Debe Ingresar un Cintillo', strTitleMessage);
                    $(this).focus();
                    controls.txtCriteriaValue.val('');
                }
            }

            return State;
        },
        createToolbar: function (data) {

            var $ul = $('<ul>', {
                id: 'carousel_ul',
                class: 'carousel-ul'
            })

            for (var i = 0; i < data.length; i++) {

                var $li = $('<li >'),
                $a = $('<a typeoptions="2" profile="' + data[i].code + '"> '),
                
                $span = $('<span>');

                $span
                    .addClass('glyphicon glyphicon-th-large')
                    .css('margin-right', '2px');


                $a
                .append($span)
                .append(data[i].name)
                .addEvent(data[i], 'click', this.acreateToolbar_click);
                $a.data('CodeValidate', 2);
               
                $.DisabledToolbar($a, data[i].code, Session.ORIGINTYPE);
                $li.append($a);
                $ul.append($li);
            }

            return $ul;
        },
        acreateToolbar_click: function (send, args) {
            var that = this;
            if (Session.ORIGINTYPE != '') {
                if (Session.ORIGINTYPE == "POSTPAID" || Session.ORIGINTYPE == "DTH" || Session.ORIGINTYPE == "HFC" || Session.ORIGINTYPE == "LTE") {
                    if (Session.DATASERVICE == null) {
                        var obj = this;
                        $.app.ajax({
                            async: false,
                            type: 'POST',
                            url: window.location.href.concat('/Dashboard/Postpaid/DataServiceContent'),
                            data: {
                                strIdSession: Session.IDSESSION,
                                strContratoID: Session.DATACUSTOMER.ContractID,
                                strApplication: Session.DATACUSTOMER.Application,
                                strCustomerType: Session.DATACUSTOMER.objPostDataAccount.CustomerType,
                                strDocumentType: Session.DATACUSTOMER.DocumentType,
                                strDocumentNumber: Session.DATACUSTOMER.DocumentNumber,
                                strModality: Session.DATACUSTOMER.objPostDataAccount.Modality,
                                strIsLTE: Session.DATACUSTOMER.IsLTE,
                                strphone: Session.DATACUSTOMER.ValueSearch,
                            },
                            success: function (response) {
                                Session.DATASERVICE = response.data;


                            },
                            error: function (ex) {
                                controls.divPostLineInfo.showMessageErrorLoading($.app.const.messageErrorLoading);
                            }
                        });
                    }
                }
                Session.ORIGINAPP = this.parentProduct;
                Session.CO = this.id;
                $.ValidateCode(this.code, Session.ORIGINAPP);
            }
        },
        getToolbar: function (data) {
            var $divContainer = $('<div>'),
                $divInner = $('<div>'),
                $divScrollLeft = $('<div>'),
                $divScrollRight = $('<div>'),
                $spanLeft = $('<span>'),
                $spanRight = $('<span>');

            $divContainer.attr('id', 'carousel_container')

            $divScrollLeft
                            .attr('id', 'left_scroll')
                            .addClass('carousel-scroll');

            $spanLeft
                    .addClass('glyphicon glyphicon-chevron-left')
                    .attr('aria-hidden', 'true')

            $divScrollLeft.append($spanLeft);

            $divContainer.append($divScrollLeft);

            $divInner
                     .attr('id', 'carousel_inner')
                     .addClass('carousel-inner')


            $divInner.append(this.createToolbar(data));

            $divContainer.append($divInner);

            $divScrollRight
                            .attr('id', 'right_scroll')
                            .addClass('carousel-scroll');

            $spanRight
                     .addClass('glyphicon glyphicon-chevron-right')
                     .attr('aria-hidden', 'true')

            $divScrollRight.append($spanRight);

            $divContainer.append($divScrollRight);

            return $divContainer;

        },
        resizeContent: function () {
            var controls = this.getControls();

            controls.divMainBody.css('height', $(window).outerHeight() - controls.divMainHeader.outerHeight() - controls.divMainFooter.outerHeight());
        },
        builtContent: function (arResult) {
            var that = this;

            Session.DATACUSTOMER = arResult.paramResult.data;
            Session.DATASERVICE = Session.DATACUSTOMER.oDataService;
            Session.CODERESPONSEPREPAIDSERVICE = arResult.paramResult.strCodeResponseService;
            if (Session.DATASERVICE != undefined && Session.DATASERVICE.IsTFI != undefined && Session.DATASERVICE.IsTFI == true) {
                Session.DATASERVICE.IsTFI = "SI";
            }

            var strUrlMenu = window.location.href + '/Home/GetOptions';

            Session.ORIGINTYPE = arResult.paramResult.OriginType;
            Session.CUSTOMEROLD = (arResult.paramResult.OriginType == 'PREPAID' ? '1' : '0');
            Session.MESSAGEPREPAID = (arResult.paramResult.OriginType == 'PREPAID' ? '1' : '0');
            
            $.app.ajax({
                type: "POST",
                url: strUrlMenu,
                data: { strIdSession: Session.IDSESSION, strApplicationType: Session.ORIGINTYPE, strUserId: Session.USERACCESS.userId },
                async: false,
                complete: function () {
                    that.sliderToolBar();
                },
                success: function (resultdata) {
                    var strMenu = '';
                    if (resultdata.data != null) {
                        for (var i = 0; i < resultdata.data.menu.length; i++) {
                            if (resultdata.data.menu[i].items != null) {
                                resultdata.data.menu[i].items = that.addAttributeParentProduct(resultdata.data.menu[i].items, resultdata.data.menu[i].name);
                            }
                        }
                        strMenu = that.getMenu(resultdata.data.menu, 'N');

                        Session.MENU[Session.ORIGINTYPE] = strMenu;
                        $('#idmMenu').html(strMenu);
                        $.SmartMenus.Bootstrap.init();
                        var strToolbar = '';
                        if (resultdata.data.toolbar != null) {
                            strToolbar = that.getToolbar(resultdata.data.toolbar);
                            var parentProduct = "";
                            if (Session.ORIGINTYPE == "POSTPAID") {
                                if ($('#ddlTipoBusqueda').val() == "1") {

                                    Session.BUSQINSTANT = "Telefono";

                                } else if ($('#ddlTipoBusqueda').val() == "2") {
                                    Session.BUSQINSTANT = "Cuenta";
                                }
                            }
                            if (Session.ORIGINTYPE == "POSTPAID" || Session.ORIGINTYPE == "DTH") parentProduct = "POSTPAGO";
                            else if (Session.ORIGINTYPE == "PREPAID") parentProduct = "PREPAGO";
                            else parentProduct = Session.ORIGINTYPE;

                            for (var i = 0; i < resultdata.data.toolbar.length; i++) {
                                if (resultdata.data.toolbar != null) {
                                    resultdata.data.toolbar = that.addAttributeParentProduct(resultdata.data.toolbar, parentProduct);
                                }
                            }

                            $('#idmToolbar').html(strToolbar);
                        }
                    }
                },
                error: function (msg) {

                }
            });

            $.app.ajax({
                type: 'POST',
                dataType: 'json',
                url: window.location.href + '/Home/GetPortability',
                data: { strIdSession: Session.IDSESSION, vTelefono: arResult.paramSearchValue },
                success: function (result) {
                    var ArrayPortability = [];
                    ArrayPortability = result.data;
                    if (ArrayPortability.length > 0) {
                        ArrayPortability.push(result.respuesta);
                    }
                    else if (ArrayPortability.length == 0) {
                        ArrayPortability.push("");
                        ArrayPortability.push(result.respuesta);
                    }
                    Session.PORTABILITY = ArrayPortability;
                    Session.PORTABILITYRESPUESTA = result.respuesta;
                },
                error: function (msg) {

                }
            });

            var strUrlContent = window.location.href + '/Home/DisplayContent';

            var dataContent = { strOriginType: arResult.paramResult.OriginType };

            $.app.ajax({
                type: "POST",
                url: strUrlContent,
                data: dataContent,
                async: false,
                success: function (result3) {
                    if (result3 != null || result3 != "" || typeof (result3 != 'undefined')) {
                        $.window.close();
                        $('#divContenido').html(result3);
                    } else {
                        $(this).focus();
                        $('#txtCriteriaValue').val('');
                        alert('No se Encontraron Resultados', this.strTitleMessage);
                        $('#divContenido').html("");
                    }
                },
                error: function (msg) {

                }

            });
        }
    }

    $.fn.form = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = ['builtContent', 'showDialogLoad', 'getControls'];

        this.each(function () {
            var $this = $(this),
                data = $this.data('form'),
                options = $.extend({}, $.fn.form.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('form', data);
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

    $.fn.form.defaults = {
    }
})(jQuery, null);



function TemplateAdministrationInst(strTittle, strTypeMenu) {
    var strUrlTemplate = window.location.href.concat('/Home/DialogTemplate');
    var strUrlModal = window.location.href.concat('/Management/MassiveInstant/Index');
    $.window.open({
        id: 184209,
        modal: false,
        title: strTittle,
        template: strUrlTemplate,
        url: strUrlModal,
        data: { strIdSession: Session.IDSESSION, strAplication: Session.DATACUSTOMER.Application },
        buttons: {
            Guardar: {
                click: function () {
                    var txtDescription = $('#txtDescription').val();
                    var txtDateStart = $("#txtDateStart").val();
                    var txtDateEnd = $("#txtDateEnd").val();
                    var txthoursVig = $('#txthoursVig').val();
                    var txtminutesVig = $('#txtminutesVig').val();
                    var cboTypeVig = $('#cboTypeVig').val();
                    var txthoursCad = $('#txthoursCad').val();
                    var txtminutesCad = $('#txtminutesCad').val();
                    var cboTypeCad = $('#cboTypeCad').val();

                    if (txtDateStart.length != 0 && txtDateEnd.length != 0 && txthoursVig.length != 0 && txtminutesVig.length != 0 && txthoursCad != 0 && cboTypeCad != 0 && cboTypeVig != "-1" && cboTypeCad != "-1" && txtDescription.trim().length != 0) {
                        var stUrlLogo = window.location.href + "/Images/loading2.gif";
                        $('#184209_maindlg').block({
                            css: {
                                border: 'none', padding: '15px', backgroundColor: '#000',
                                '-webkit-border-radius': '10px', '-moz-border-radius': '10px',
                                opacity: .8, color: '#fff'
                            },
                            overlayCSS: { backgroundColor: '#FFFFFF', opacity: 0.0, cursor: 'wait' },
                            message: '<div align="center"><img src="' + stUrlLogo + '" width="25" height="25" /> Grabando ... </div>',
                        });

                        if (strTypeMenu == "INSTMASIVE") {
                            SaveInstMasive(txtDateStart, txtDateEnd, txthoursVig, txtminutesVig, cboTypeVig, txthoursCad, txtminutesCad, cboTypeCad, txtDescription);
                        }
                        else if (strTypeMenu == "MakeChargePetition") {
                            SaveProcessedOrders(txtDateStart, txtDateEnd, txthoursVig, txtminutesVig, cboTypeVig, txthoursCad, txtminutesCad, cboTypeCad, txtDescription);
                        }
                    }
                    else {
                        if (txtDescription.length <= 0) { alert('Debe Ingresar una descripción', 'Datos Incorrectos'); return false; }
                        if (validarFecha("txtDateStart") == false) { return false; }
                        if (validarFecha("txtDateEnd") == false) { return false; }
                        if (validarHoraIngreso('txthoursVig', 'txtminutesVig', document.getElementById('cboTypeVig').value) == false) { return false; }
                        if (validarHoraIngreso('txthoursCad', 'txtminutesCad', document.getElementById('cboTypeCad').value) == false) { return false; }
                    }
                }
            },
            Limpiar: {
                click: function () {

                    clearFormInstMasive();
                }
            },
            Cerrar: {
                click: function (sender, args) {
                    this.close();
                }
            }
        },
        width: 800,
        height: 500,
    });
}

function SaveInstMasive(txtDateStart, txtDateEnd, txthoursVig, txtminutesVig, cboTypeVig, txthoursCad, txtminutesCad, cboTypeCad, txtDescription) {
    if (Session.ListMassiveInstantCorrect.length > 0) {
        $.app.ajax({
            type: 'POST',
            url: window.location.href.concat('/management/massiveinstant/SaveInstantMasive'),
            data: {
                strIdSession: Session.IDSESSION,
                strAplication: Session.DATACUSTOMER.Application,
                DateValidityStart: txtDateStart,
                DateValidityEnd: txtDateEnd,
                StartHour: txthoursVig,
                StartMinutes: txtminutesVig,
                StartType: cboTypeVig,
                EndHour: txthoursCad,
                EndMinutes: txtminutesCad,
                EndType: cboTypeCad,
                Description: txtDescription,
                listInstant: Session.ListMassiveInstantCorrect,
                ArchiveName: Session.ArchivoIntantanea
            },
            success: function (response) {
                $('#184209_maindlg').unblock();
                $.window.close();


                alert('Proceso terminado satisfactoriamente', 'Mensaje');
            },
            error: function () {

                alert('Ocurrior un error al grabar, intentelo nuevamente', 'Mensaje');
                $('#184209_maindlg').unblock();
            }
        });
    } else {
        $('#184209_maindlg').unblock();
        if (Session.DATACUSTOMER.Application == "POSTPAID" || Session.DATACUSTOMER.Application == "PREPAID") {

            alert('Debe importar líneas', 'Mensaje');
        } else {


            alert('Debe importar contratos', 'Mensaje');
        }
    }
}

function SaveProcessedOrders(txtDateStart, txtDateEnd, txthoursVig, txtminutesVig, cboTypeVig, txthoursCad, txtminutesCad, cboTypeCad, txtDescription) {
    if (Session.NewArchivoIntantanea != "") {
        $.app.ajax({
            type: 'POST',
            url: window.location.href.concat('/management/massiveinstant/SaveProcessedOrders'),
            data: {
                strIdSession: Session.IDSESSION,
                strAplication: Session.DATACUSTOMER.Application,
                DateValidityStart: txtDateStart,
                DateValidityEnd: txtDateEnd,
                StartHour: txthoursVig,
                StartMinutes: txtminutesVig,
                StartType: cboTypeVig,
                EndHour: txthoursCad,
                EndMinutes: txtminutesCad,
                EndType: cboTypeCad,
                Description: txtDescription,
                ArchiveName: Session.ArchivoIntantanea,
                NewArchiveName: Session.NewArchivoIntantanea
            },
            success: function (response) {
                $('#184209_maindlg').unblock();
                clearFormInstMasive();


                alert('Proceso terminado satisfactoriamente', 'Mensaje');

            },
            error: function () {


                alert('Ocurrio un error al grabar, intentelo nuevamente', 'Mensaje');
            }
        });
    }
    else {


        alert('Debe seleccionar el archivo a importar', 'Atención');
        $('#184209_maindlg').unblock();
    }
}


function clearFormInstMasive() {
    $('#txtDescription').val("");
    $("#txtDateStart").val();
    $("#txtDateEnd").val();
    $('#txthoursVig').val("");
    $('#txtminutesVig').val("");
    $('#cboTypeVig').val("-1");
    $('#txthoursCad').val("");
    $('#txtminutesCad').val("");
    $('#cboTypeCad').val("-1");
    if (Session.TypeMenuInst == "INSTMASIVE") {
        Session.ListMassiveInstantCorrect = [];
        $('#tblInsMassCorrect tbody').empty();
    } else if (Session.TypeMenuInst == "MakeChargePetition") {
        $('#MakeChargePetitionFrame').hide();
        Session.ArchivoIntantanea = "";
        Session.NewArchivoIntantanea = "";
    }
}

function redrawClock() {
    var clock = $('#uiClock');
    var text = moment().format('[<p>]h:mm:ss a[</p><p>]DD/MM/YYYY</p>')
    clock.html(text);
    setTimeout(redrawClock, 1000);
}

$(document).ready(function () {
    $.window.toolbar = $('#uiBarra');
    moment.locale('es');
    setTimeout(redrawClock, 1000);
    $('#navbar-contenedor').form();

})