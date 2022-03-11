(function ($, undefined) {
   

    var Form = function ($element, options) {

        $.extend(this, $.fn.ChangePhoneNumber.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , lblPhoneNumber: $('#lblPhoneNumber', $element)
            , lblCustomerType: $('#lblCustomerType', $element)
            , lblContact: $('#lblContact', $element)
            , lblCustomerName: $('#lblCustomerName', $element)
            , lblCustomerContact: $('#lblCustomerContact', $element)
            , lblContactCust: $('#lblContactCust', $element)
            , lblLegalRepresentative: $('#lblLegalRepresentative', $element)
            , lblIdentificationDocument: $('#lblIdentificationDocument', $element)
            , lblIdentificationDocumentoLR: $('#lblIdentificationDocumentoLR', $element)
            , lblActivationDate: $('#lblActivationDate', $element)
            //, lblLastDateRecharge: $('#lblLastDateRecharge', $element)
            , lblBornDate: $('#lblBornDate', $element)
            //, lblLastMountRecharge: $('#lblLastMountRecharge', $element)
            , lblNumberTriado: $('#lblNumberTriado', $element)
            , lblErrorMessage: $('#lblErrorMessage', $element)
            , ddlLocation: $('#ddlLocation', $element)
            , ddlCostCorp: $('#ddlCostCorp', $element)
            , ddlCACDAC: $('#ddlCACDAC', $element)
            , chkTranslationNumber: $('#chkTranslationNumber', $element)
            , txtStartHLR: $('#txtStartHLR', $element)
            , txtNewNumber: $('#txtNewNumber', $element)
            , txtEndHLR: $('#txtEndHLR', $element)
            , txtCostTrans: $('#txtCostTrans', $element)
            , rdFidelizeYes: $('#rdFidelizeYes', $element)
            , rdFidelizeNot: $('#rdFidelizeNot', $element)
            , taNotas: $('#taNotas', $element)
            , btnSearch: $('#btnSearch', $element)
            , btnSave: $('#btnSave', $element)
            , btnConstancy: $('#btnConstancy', $element)
            , btnClose: $('#btnClose', $element)
            , divRules: $('#divRules', $element)
            , divErrorAlert: $('#divErrorAlert', $element)
            , trNroTriados: $('#trNroTriados', $element)
            , lblTitle: $('#lblTitle', $element)   //Redirect 1.0
            , divClose: $('#divClose', $element)
        });
    }

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this,
                controls = this.getControls();

            document.addEventListener('keydown', that.shortCutClose, false);
            controls.chkTranslationNumber.addEvent(that, 'change', that.chkTranslationNumber_change);
            controls.ddlCACDAC.addEvent(that, 'change', that.ddlCACDAC_change);
            controls.rdFidelizeYes.addEvent(that, 'change', that.rdbFidelize_change);
            controls.rdFidelizeNot.addEvent(that, 'change', that.rdbFidelize_change);
            controls.btnSearch.addEvent(that, 'click', that.btnSearch_click);
            controls.btnSave.addEvent(that, 'click', that.btnSave_click);
            controls.btnConstancy.addEvent(that, 'click', that.btnConstancy_click);
            controls.btnClose.addEvent(that, 'click', that.btnClose_click);
            controls.divClose.addEvent(that, 'click', that.divClose_click);
            that.maximizarWindow();
            that.windowAutoSize();
            that.loadSessionData();
            that.render();
        },
        loadSessionData: function () {
            //Redirect ini  2.0
            var that = this,
            controls = this.getControls();

            controls.lblTitle.text("CAMBIO DE NÚMERO");
            var SessionTransac = JSON.parse(sessionStorage.getItem("SessionTransac"));
            //Session.IDSESSION = sessionStorage.idSession;
            Session.IDSESSION = '20170518150515737831';
            Session.DATACUSTOMER = SessionTransac.SessionParams.DATACUSTOMER;
            Session.DATASERVICE = SessionTransac.SessionParams.DATASERVICE;
            Session.USERACCESS = SessionTransac.SessionParams.USERACCESS;
            //Session.DATACUSTOMER =
            //{
            //    Account: "3.837.00.00.100001",
            //    ActivationDate: "15/04/2002 12:00:00 a.m.",
            //    Address: "SMILDAYYN6",
            //    AddressNote: null,
            //    Application: "POSTPAID",
            //    Assessor: "",
            //    BannerMessage: "--- <strong style='FONT-WEIGHT: bold; COLOR: #000080;'>Prueba</strong>",
            //    BillingCycle: "",
            //    BirthDate: "21/06/1966",
            //    BirthPlace: "Peru",
            //    BirthPlaceID: "154",
            //    BusinessName: "U6V33JIG11",
            //    ChangeApplication: false,
            //    CivilStatus: "Casado (a)",
            //    CivilStatusID: "2",
            //    CodCustomerType: "1",
            //    CodeCenterPopulate: "",
            //    CodePlanTariff: "",
            //    ContactCode: null,
            //    ContactFlag: "1",
            //    ContractID: "2257312",
            //    Country: "Peru",
            //    CustomerContact: "E0TTT9IQFK",
            //    CustomerID: "19242",
            //    CustomerType: "Business",
            //    CustomerTypeCode: null,
            //    DNIRUC: "233132",
            //    Departament: "LIMA",
            //    DescriptionCenterPopulate: "",
            //    District: "EL AGUSTINO",
            //    DocumentNumber: "233132",
            //    DocumentType: "RUC",
            //    Email: "EEPDYIVVNK",
            //    Fax: "",
            //    FullName: "ZFS59EFW9R 9EGZFS2J32",
            //    HubCode: "",
            //    IdContactObject: "268581426",
            //    InstallUbigeo: "",
            //    InvoiceAddress: "SMILDAYYN6",
            //    InvoiceCode: "L10",
            //    InvoiceCountry: "Peru",
            //    InvoiceDepartament: "LIMA",
            //    InvoiceDistrict: "EL AGUSTINO",
            //    InvoicePostal: null,
            //    InvoiceProvince: "BIPRCFCDJ6",
            //    InvoiceUbigeo: "",
            //    InvoiceUrbanization: "LUYQTBV5IT",
            //    IsLTE: false,
            //    LastName: "9EGZFS2J32",
            //    LegalAddress: "SMILDAYYN6",
            //    LegalAgent: "ODJ6Q4BR30",
            //    LegalCode: "",
            //    LegalCountry: "",
            //    LegalDepartament: "",
            //    LegalDistrict: "",
            //    LegalPostal: null,
            //    LegalProvince: "",
            //    LegalUrbanization: "",
            //    Membership: false,
            //    Modality: "Corporativo",
            //    Name: "ZFS59EFW9R",
            //    OfficeAddress: "",
            //    OriginType: null,
            //    PaymentMethod: "CONTADO",
            //    PhoneContact: "",
            //    PhoneReference: "3640032",
            //    PlaneCode: "",
            //    PlaneCodeBilling: "",
            //    PlaneCodeInstallation: "",
            //    Position: "U7OJSG7Q8C",
            //    PostalCode: null,
            //    ProductType: "",
            //    ProductTypeText: "PostPago - Telefonía Móvil",
            //    Province: "BIPRCFCDJ6",
            //    Reference: "LUYQTBV5IT",
            //    Segment2: "C",
            //    Sex: null,
            //    SiteCode: null,
            //    Telephone: "997357786",
            //    Tradename: "BI3QAH1N0S",
            //    Urbanization: null,
            //    ValueSearch: "997357786",
            //    objPostDataAccount: {
            //        AccountId: null,
            //        AccountParent: null,
            //        AccountStatus: "Activo",
            //        ActivationDate: "15/04/2002 12:00:00 a.m.",
            //        Balance: null,
            //        BillingCycle: "28",
            //        Consultant: "",
            //        Consultant_Account: "",
            //        CreditLimit: "120",
            //        CustomerId: null,
            //        CustomerType: "Business",
            //        ExpirationDate: null,
            //        LastName: null,
            //        Level: null,
            //        Modality: "Corporativo - JANUS",
            //        Name: null,
            //        Niche: "01272",
            //        ResponsiblePayment: "X",
            //        Segment: "Medio",
            //        TotalAccounts: "1",
            //        TotalLines: "1",
            //        lstPostDataAccount: null
            //    }
            //};

            //Session.DATASERVICE = {
            //    Account: null,
            //    ActivationDate: "15/04/2002 12:00:00 a.m.",
            //    Application: "POSTPAID",
            //    Cable: "",
            //    CableTv: null,
            //    CableValue: null,
            //    Campaign: "",
            //    CellPhone: "997357786",
            //    ChangedBy: null,
            //    CodReturn: null,
            //    CodePlanTariff: "1099",
            //    ContractID: "2257312",
            //    DeactivationDate: null,
            //    EnableEquipment: true,
            //    FlagPlatform: "R",
            //    FlagTFI: "NO",
            //    Internet: "",
            //    InternetValue: null,
            //    Introduced: "/Date(-62135578800000)/",
            //    IntroducedBy: null,
            //    IsLTE: "false",
            //    IsNot3Play: null,
            //    IsTFI: null,
            //    LimitConsume: "Activo",
            //    MSISDN: null,
            //    MobileBanking: "SI",
            //    NumberICCID: "8951100120040025497",
            //    NumberIMSI: "716100104002549",
            //    PIN1: " 1111",
            //    PIN2: " 1062",
            //    PUK1: " 24427813",
            //    PUK2: " 55093054",
            //    Plan: "Plan MyPE Fácil 55",
            //    PlanTariff: null,
            //    Portability: false,
            //    PortabilityState: "",
            //    ProviderID: null,
            //    Reason: "Migración de Control a Postpago",
            //    Roaming: false,
            //    Seller: "",
            //    ServiceA: "glyphicon glyphicon-ok",
            //    ServiceCombo: "Plan MyPE Fácil 55",
            //    ServiceI: "",
            //    ServicePackage: "",
            //    ServiceTFI: "",
            //    StateAgreement: null,
            //    StateDTH: false,
            //    StateDate: "27/03/2017 12:55:33 p.m.",
            //    StateLine: "Activo",
            //    StateServiceCombo: false,
            //    StateServicePackage: false,
            //    StateServiceTFI: false,
            //    Telephony: "",
            //    TelephonyValue: null,
            //    TermContract: "",
            //    TypeProduct: null,
            //    TypeService: "POSTPAGO",
            //    TypeSolution: "null",
            //    ValidFrom: "/Date(-62135578800000)/"
            //}
        },
        setControls: function (value) {
            this.m_controls = value;
        },
        getControls: function () {
            return this.m_controls || {};
        },
        render: function () {
            var that = this,
                controls = this.getControls();
            that.loadConfigVariables();
            that.loadHeaderTransaction();
        },
        loadConfigVariables: function () {
            var that = this, controls = that.getControls();
            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '/Transactions/Postpaid/ChangePhoneNumber/GetMessage',
                data: {},
                success: function (response) {
                    that.strConsumerChangeNumberCost = response[0];
                    that.strConsumerTranslateNumberCost = response[1];
                    that.strIGVPercentageType2 = response[2];
                    that.strStatusZANS = response[3];
                    that.calculateCost();
                }
            });
        },
        loadHeaderTransaction: function () {
            var that = this,
                controls = this.getControls();

            controls.lblPhoneNumber.text(Session.DATACUSTOMER.Telephone);
            controls.lblCustomerType.text(Session.DATACUSTOMER.CustomerType);
            controls.lblContact.text(Session.DATACUSTOMER.FullName);

            controls.lblCustomerName.text(Session.DATACUSTOMER.BusinessName);
            controls.lblCustomerContact.text(Session.DATACUSTOMER.CustomerContact);
            controls.lblContactCust.text(Session.DATACUSTOMER.FullName);
            controls.lblLegalRepresentative.text(Session.DATACUSTOMER.LegalAgent);
            controls.lblIdentificationDocument.text(Session.DATACUSTOMER.DNIRUC);
            controls.lblIdentificationDocumentoLR.text(Session.DATACUSTOMER.DocumentNumber);
            controls.lblActivationDate.text(Session.DATACUSTOMER.ActivationDate);
            controls.lblBornDate.text(Session.DATACUSTOMER.BirthDate);
            controls.lblNumberTriado.text('');

            controls.chkTranslationNumber.prop('checked', false);
            controls.ddlLocation.prop('disabled', true);
            controls.rdFidelizeNot.prop('checked', true);
            controls.divErrorAlert.hide();

            that.loadValidatePage();

        },
        loadValidatePage: function () {
            var that = this,
                controls = this.getControls();

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

            if (Session.DATASERVICE.FlagPlatform == 'C') {
                alert('El cliente no es Postpago.',"Alerta");
                parent.window.close();
            }

            if (Session.DATACUSTOMER.ContractID != '0') {
                that.validatePortability();
            }

            if (Session.DATASERVICE.FlagTFI == 'SI') {
                controls.trNroTriados.hide();
            }

            that.loadAlignTransacService();
        },
        loadAlignTransacService: function () {
            var that = this,
                controls = this.getControls(),
                objServ = {};

            objServ.strIdSession = Session.IDSESSION;
            objServ.strPhone = Session.DATACUSTOMER.Telephone;
            objServ.strContractID = Session.DATACUSTOMER.ContractID;

            $.app.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '/Transactions/Postpaid/ChangePhoneNumber/AlignService',
                data: JSON.stringify(objServ),
                success: function (result) {
                    that.strPhone = result.data;
                    that.loadTransaction();
                },
                error: function (xhr, status, error) {
                    //console.logxhr);
                    that.strPhone= Session.DATACUSTOMER.Telephone;
                    that.loadTransaction();
                },
                complete: function () {
                    //console.log"complete align");
                }
            });

        },
        loadTransaction: function () {
            var that = this,
                controls = this.getControls();

            that.getStatusPhone();

            if (Session.DATASERVICE.FlagPlatform != 'C') {
                that.getStriations();
            }

            that.getRegions();
            that.getHLRLocation();
            that.getCACDAC();
            that.getIGV();

            if (Session.DATACUSTOMER.Modality.toUpperCase() == 'CORPORATIVO') {
                that.getCorporativeCost();
                $('#divCostTransCorp').show();
                $('#divCostoTrans').hide();
            }
            else {
                $('#divCostTransCorp').hide();
                $('#divCostoTrans').show();
            }

            that.getTypification();

        },
        getIGV: function () {
            var that = this,
                controls = that.getControls(),
                objIGV = {};

            objIGV.strIdSession = Session.IDSESSION;

            $.app.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objIGV),
                url: '/Transactions/CommonServices/GetConsultIGV',
                success: function (result) {
                    if (result.data != null) {
                        var igv = parseFloat(result.data.igvD);
                        var igvEnt = igv + 1;
                        that.strIGV = igvEnt;
                    }
                }
            });
        },
        getStatusPhone: function () {
            var that = this,
			controls = that.getControls(),
			objStatus = {};

            objStatus.strIdSession = Session.IDSESSION;
            objStatus.strNumberPhone = that.strPhone;
            $.app.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '/Transactions/Postpaid/ChangePhoneNumber/GetStatusPhone',
                data: JSON.stringify(objStatus),
                success: function (result) {
                    if (result.data.RESPONSE_SERVICE == '0') {
                        if (result.data.RESPONSE_MESSAGE != that.strStatusZANS) {
                            var msg = 'El número ' + that.strPhone + ' no se encuentra con el estado ' + that.strStatusZANS + ' en SANS. Estado actual: ' + result.data.RESPONSE_MESSAGE;
                            controls.divErrorAlert.show(); controls.lblErrorMessage.text(msg);
                            controls.btnSave.prop('disabled', true);
                            controls.btnSearch.prop('disabled', true);
                        }
                    } else {
                        var msg = 'Error : Al consultar consultarTelefonoTodos  con la linea : ' + that.strPhone;
                        controls.divErrorAlert.show(); controls.lblErrorMessage.text(msg);
                        controls.btnSave.prop('disabled', true);
                        controls.btnSearch.prop('disabled', true);
                    }
                }
            });
        },
        getStriations: function () {
            var that = this,
                controls = that.getControls(),
                oCustomer = Session.DATACUSTOMER,
                objTriaciones = {};

            objTriaciones.ContractId = oCustomer.ContractID;
            objTriaciones.strIdSession = Session.IDSESSION;

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '/Transactions/Postpaid/ChangePhoneNumber/GetStriations',
                data: JSON.stringify(objTriaciones),
                success: function (response) {
                    var lista = response.data.Striations;
                    if (lista != null) {
                        if (lista.length > 0) {
                            var phones = lista.map(function (x) { return x.Telephone }).join(', ');
                            controls.lblNumberTriado.text(phones);
                        }
                    }
                }
            });
        },
        getCorporativeCost: function () {
            var that = this,
                controls = that.getControls(),
                objCost = {};

            objCost.strIdSession = Session.IDSESSION;
            objCost.strNameFunction = "ListaCostoCambioNroCorporativo";
            objCost.strFlagCode = "";
            objCost.fileName = "Data.xml";

            $.app.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '/Transactions/CommonServices/GetListValueXmlMethod',
                data: JSON.stringify(objCost),
                success: function (result) {
                    if (result.data != null) {
                        $.each(result.data, function (index, value) {
                            controls.ddlCostCorp.append($('<option>', { value: value.Code, html: value.Description }));
                        });
                    }
                }
            });
        },
        getRegions: function () {
            var that = this,
                controls = that.getControls(),
                objReg = {};

            objReg.strIdSession = Session.IDSESSION;

            $.app.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '/Transactions/CommonServices/GetRegions',
                data: JSON.stringify(objReg),
                success: function (result) {
                    controls.ddlLocation.append($('<option>', { value: '', html: 'Seleccionar' }));
                    if (result.data != null) {
                        $.each(result.data.ListRegion, function (index, value) {
                            controls.ddlLocation.append($('<option>', { value: value.ID_REGION, html: value.DESCRIPCION }));
                        });
                    }
                }
            });
        },
        getHLRLocation: function () {
            var that = this,
			controls = that.getControls(),
			objHLR = {};

            objHLR.strIdSession = Session.IDSESSION;
            objHLR.strNumberPhone = '51' + Session.DATACUSTOMER.Telephone;
            objHLR.strRangeType = '2';

            $.app.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '/Transactions/Postpaid/ChangePhoneNumber/GetHLR',
                data: JSON.stringify(objHLR),
                success: function (result) {
                    controls.txtStartHLR.val('HLR0' + result.data.LOCATION);
                }
            });
        },
        getCACDAC: function () {
            var that = this,
                controls = that.getControls(),
                objCacDacType = {};

            objCacDacType.strIdSession = Session.IDSESSION;

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objCacDacType),
                url: '/Transactions/CommonServices/GetCacDacType',
                success: function (response) {
                    controls.ddlCACDAC.append($('<option>', { value: '', html: 'Seleccionar' }));
                    if (response.data != null) {
                        $.each(response.data.CacDacTypes, function (index, value) {
                            controls.ddlCACDAC.append($('<option>', { value: value.Code, html: value.Description }));
                        });
                    }
                }
            });
        },
        getTypification: function () {
            var that = this,
			controls = that.getControls(),
			objTypi = {};

            objTypi.strIdSession = Session.IDSESSION;
            objTypi.strTransactionName = that.strTransactionTypi;

            $.app.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '/Transactions/CommonServices/GetTypification',
                data: JSON.stringify(objTypi),
                success: function (result) {
                    var list = result.ListTypification;
                    if (list != null) {
                        if (list.length > 0) {
                            that.strClaseCode = list[0].CLASE_CODE;
                            that.strSubClaseCode = list[0].SUBCLASE_CODE;
                            that.strTipo = list[0].TIPO;
                            that.strClase = list[0].CLASE;
                            that.strSubClase = list[0].SUBCLASE;

                            that.getBusinessRules();
                        } else {
                            var msg = 'No se reconoce la tipificación de esta transacción';
                            controls.divErrorAlert.show(); controls.lblErrorMessage.text(msg);
                            controls.btnSave.prop('disabled', true);
                            controls.btnSearch.prop('disabled', true);
                            controls.btnConstancy.prop('disabled', true);
                        }
                    } else {
                        var msg = 'No se reconoce la tipificación de esta transacción';
                        controls.divErrorAlert.show(); controls.lblErrorMessage.text(msg);
                        controls.btnSave.prop('disabled', true);
                        controls.btnSearch.prop('disabled', true);
                        controls.btnConstancy.prop('disabled', true);
                    }
                }
            });
        },
        getBusinessRules: function () {
            var that = this,
                controls = that.getControls(),
                objRules = {};

            objRules.strIdSession = Session.IDSESSION;
            objRules.strSubClase = that.strSubClaseCode;

            $.app.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '/Transactions/CommonServices/GetBusinessRules',
                data: JSON.stringify(objRules),
                success: function (result) {
                    if (result.data.ListBusinessRules != null) {
                        var list = result.data.ListBusinessRules;
                        if (list.length > 0) {
                            controls.divRules.append(list[0].REGLA);
                        }
                    }

                }
            });
        },
        calculateCost: function () {
            var that = this, controls = that.getControls();
            if (Session.DATACUSTOMER.Modality.toUpperCase() == 'CORPORATIVO') {
                if (controls.rdFidelizeYes.prop('checked')) {
                    controls.ddlCostCorp.prop('disabled', true);
                } else {
                    controls.ddlCostCorp.prop('disabled', false);
                }
            }
            else {
                if (controls.rdFidelizeYes.prop('checked')) {
                    controls.txtCostTrans.val('0.00');
                } else {
                    if (controls.chkTranslationNumber.prop('checked'))
                        controls.txtCostTrans.val(that.strConsumerTranslateNumberCost);
                    else
                        controls.txtCostTrans.val(that.strConsumerChangeNumberCost);
                }
            }
        },
        validatePortability: function () {
            var that = this,
			controls = that.getControls(),
			objPorta = {};

            objPorta.strIdSession = Session.IDSESSION;
            objPorta.strPhone = Session.DATACUSTOMER.Telephone;

            $.app.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '/Transactions/Postpaid/ChangePhoneNumber/GetPortability',
                data: JSON.stringify(objPorta),
                success: function (result) {
                    if (result.data.Respuesta == 'En proceso de Portabilidad') {
                        alert('La Línea se encuentra en proceso de Portabilidad',"Informativo");
                        parent.window.close();
                    }

                    if (result.data.Respuesta == 'Portado PORT-OUT') {
                        alert('La línea se encuentra portado PORT-OUT', "Informativo");
                        parent.window.close();
                    }
                }
            });
        },
        chkTranslationNumber_change: function (sender, arg) {
            var that = this,
                controls = that.getControls();

            if (sender.prop("checked")) {
                controls.ddlLocation.prop("disabled", false);
            } else {
                controls.ddlLocation.prop("disabled", true);
            }
        },
        rdbFidelize_change: function (sender, arg) {
            var that = this,
                controls = that.getControls();
            that.calculateCost();
        },
        ddlCACDAC_change: function (sender, arg) {
            var that = this, controls = that.getControls();
            var desc = sender.prop('selectedOptions')[0].innerHTML;
            if (sender.val() == '') {
                desc = '';
            }
            that.strSalePointDesc = desc;
        },
        btnSearch_click: function () {
            var that = this,
                controls = that.getControls();

            if (controls.chkTranslationNumber.prop('checked')) {
                if (controls.ddlLocation.val() == '') {
                    alert('Usted va realizar un traslado, seleccione una localidad','Alerta');
                    return false;
                }
            }

            confirm('¿Está seguro de buscar el nuevo número ?', 'Confirmar', function (resul) {
                if (resul) {
                    that.searchNumber();
                }
            })
        },
        searchNumber: function () {
            var that = this, controls = that.getControls();
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

            that.validateChangeNumber();
        },
        validateChangeNumber: function () {
            var that = this,
			controls = that.getControls(),
			objVal = {};

            var flagFidelize;
            if (Session.DATACUSTOMER.CustomerType.toUpperCase() == 'BUSINESS')
                flagFidelize = '1';
            else
                flagFidelize = '0';

            objVal.strIdSession = Session.IDSESSION;
            objVal.strContract = that.strPhone;
            objVal.strFlagFidelize = flagFidelize;
            $.app.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '/Transactions/Postpaid/ChangePhoneNumber/ValidateChangeNumber',
                data: JSON.stringify(objVal),
                success: function (result) {
                    if (!result.data.RESPONSE) {
                        controls.divErrorAlert.show();
                        controls.lblErrorMessage.text(result.data.RESPONSE_MESSAGE);
                    } else {
                        that.getAvailableLine();
                    }
                }
            });
        },
        maximizarWindow: function () {
            top.window.moveTo(0, 0);
            if (document.all) {
                top.window.resizeTo(screen.availWidth, screen.availHeight);
            } else if (document.layers || document.getElementById) {
                if (top.window.outerHeight < screen.availHeight || top.window.outerWidth < screen.availWidth) {
                    top.window.outerHeight = screen.availHeight;
                    top.window.outerWidth = screen.availWidth;
                }
            }
        },
        windowAutoSize: function () {
            var hsize = Math.max(
                    document.documentElement.clientHeight,
                    document.body.scrollHeight,
                    document.documentElement.scrollHeight,
                    document.body.offsetHeight,
                    document.documentElement.offsetHeight
                );
            hsize = hsize - 72;
            $('#content').css({ 'height': hsize + 'px' });
        },


        getAvailableLine: function () {
            var that = this,
                controls = that.getControls(),
                objAvailable = {};

            var location, startHLR;
            if (controls.chkTranslationNumber.prop('checked')) {
                location = control.ddlLocation.val();
            } else
                location = '';

            startHLR = controls.txtStartHLR.val();

            objAvailable.strIdSession = Session.IDSESSION;
            objAvailable.strPhone = Session.DATACUSTOMER.Telephone;
            objAvailable.strICCID = Session.DATASERVICE.NumberICCID;
            objAvailable.strFlagTFI = Session.DATASERVICE.FlagTFI;
            objAvailable.strModality = Session.DATACUSTOMER.Modality;
            objAvailable.strLocation = location;
            objAvailable.strHLR = startHLR;

            $.app.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '/Transactions/Postpaid/ChangePhoneNumber/GetNewPhoneNumber',
                data: JSON.stringify(objAvailable),
                success: function (result) {
                    var res = result.data;
                    that.strRollback = res.ROLLBACK;
                    if (res.ERROR == '') {
                        controls.txtEndHLR.val(res.END_HLR);
                        controls.txtNewNumber.val(res.NEW_PHONE)
                    } else {
                        var msg = 'Error : Al consultar consultarTelefonoTodos  con la linea : ' + that.strPhone;
                        controls.divErrorAlert.show(); controls.lblErrorMessage.text(res.MESSAGE);
                    }

                }
            });
        },
        divClose_click: function () {
            var that = this, controls = that.getControls();
            controls.divErrorAlert.hide();
        },
        btnSave_click: function () {
            var that = this,
                controls = that.getControls();

            if (controls.txtNewNumber.val() == '') {
                alert('No se generó el nuevo número', "Alerta");
                return false;
            }

            if (controls.txtStartHLR.val() == '') {
                alert('El HLR Inicial esta vacio', "Alerta");
                return false;
            }

            if (controls.txtEndHLR.val() == '') {
                alert('El HLR Final esta vacio', "Alerta");
                return false;
            }

            confirm('¿Está seguro de realizar la transacción?', 'Confirmar', function (resul) {
                if (resul) {
                    that.executeChangePhoneNumber();
                }
            })
        },
        executeChangePhoneNumber: function () {
            var that = this,
                controls = that.getControls();

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

            var data = {};
            data.SESSION = Session.IDSESSION;
            data.CONTRACT = Session.DATACUSTOMER.ContractID;
            data.ACCOUNT = Session.DATACUSTOMER.Account;
            data.CONTACTOBJID = Session.DATACUSTOMER.IdContactObject;
            data.TRANSACTION = that.strTransactionTypi;
            data.CUSTOMER_TYPE = Session.DATACUSTOMER.CustomerType;
            data.TYPE = that.strTipo;
            data.CLASS = that.strClase;
            data.SUB_CLASS = that.strSubClase;
            data.CURRENT_PHONE = that.strPhone;
            data.NEW_PHONE = controls.txtNewNumber.val();
            if (controls.chkTranslationNumber.prop('checked')) {
                data.TRANSACTION_TYPE = "1";
                data.LOCATION = control.ddlLocation.val();
            } else {
                data.TRANSACTION_TYPE = "0";
                data.LOCATION = '';
            }
            data.START_HLR = controls.txtStartHLR.val();
            data.END_HLR = controls.txtEndHLR.val();
            data.CHIP = "";
            if (controls.rdFidelizeYes.prop('checked')) {
                data.FIDELIZE = "S";
                data.COST = "0.00";
            } else {
                data.FIDELIZE = "N";
                if (Session.DATACUSTOMER.Modality.toUpperCase() == 'CORPORATIVO') {
                    data.COST = controls.ddlCostCorp.val();
                } else {
                    data.COST = controls.txtCostTrans.val();
                }
            }
            data.SALE_POINT = that.strSalePointDesc;
            data.NOTES = controls.taNotas.val();
            data.IGV = that.strIGV;
            data.USER = Session.USERACCESS.login;

            data.TITRE = Session.DATACUSTOMER.FullName;
            data.LEGALREP = Session.DATACUSTOMER.LegalAgent;
            data.DOCUMENT_TYPE = Session.DATACUSTOMER.DocumentType;
            data.DOCUMENT = Session.DATACUSTOMER.DocumentNumber;

            $.ajax({
                async: true,
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(data),
                url: '/Transactions/Postpaid/ChangePhoneNumber/ExecuteChangePhoneNumber',
                success: function (response) {
                    data = response.data;
                    if (data != null) {
                        //console.logdata);
                        that.strInteraction = data.INTERACTION_ID;

                        if (that.strInteraction != null) {
                            that.strRollback = data.ROLLBACK;

                            that.strPath_PDF = data.PATH_PDF;
                            that.strName_PDF = data.NAME_PDF;

                            alert(data.MESSAGE,"Informativo");
                            controls.btnConstancy.prop('disabled', false);
                            controls.btnSave.prop('disabled', true);
                            controls.btnSearch.prop('disabled', true);
                        } else {
                            controls.divErrorAlert.show(); controls.lblErrorMessage.text(data.MESSAGE);
                        }
                    }
                }
            });
        },
        btnClose_click: function () {
            var that = this;
            that.executeRollback();
        },
        executeRollback: function () {
            var that = this,
                controls = that.getControls(),
                objRb = {};

            if (that.strRollback == 'S') {
                objRb.strIdSession = Session.IDSESSION;
                objRb.strPhone = that.strPhone;
                objRb.strNewPhone = controls.txtNewNumber.val();

                $.app.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataType: 'json',
                    url: '/Transactions/Postpaid/ChangePhoneNumber/RollbackChangePhoneNumber',
                    data: JSON.stringify(objRb),
                    success: function (result) {
                        parent.window.close();
                    }
                });
            } else {
                parent.window.close();
            }
        },
        btnConstancy_click: function () {
            alert('Constancia', "Informativo");

            //var that = this;
            //var rutaArchivo = that.strPath_PDF;
            //var nombreArchivo = that.strName_PDF;
            //var FlagBill = "1";
            //var IdSession = Session.IDSESSION;

            //ReadRecordPdf(rutaArchivo, nombreArchivo, IdSession, FlagBill);
        },
        strUrl: window.location.protocol + '//' + window.location.host,
        strUrlLogo: window.location.protocol + '//' + window.location.host + '/Images/loading2.gif',
        strTransactionTypi: 'TRANSACCION_CAMBIO_TRAS_NUMERO',
        strClaseCode: '',
        strSubClaseCode: '',
        strTipo: '',
        strClase: '',
        strSubClase: '',
        strConsumerChangeNumberCost: '',
        strConsumerTranslateNumberCost: '',
        strIGVPercentageType2: '',
        strPhone: '',
        strStatusZANS: '',
        strRollback: '',
        strIGV: '',
        strSalePointDesc: '',
        strInteraction: '',
        strPath_PDF: '',
        strName_PDF: ''
    }

    $.fn.ChangePhoneNumber = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('PostPaidChangePhoneNumber'),
                options = $.extend({}, $.fn.ChangePhoneNumber.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('PostPaidChangePhoneNumber', data);
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

    $.fn.ChangePhoneNumber.defaults = {

    }

    $('#divBody').ChangePhoneNumber(); //Redirect ini  3.0
    //$('#PostPaidChangePhoneNumber').ChangePhoneNumber();


})(jQuery);