(function ($, undefined) {
   

    var Form = function ($element, options) {
        $.extend(this, $.fn.ChangePhoneNumber.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , lblContract: $('#lblContract', $element)
            , lblCustomerType: $('#lblCustomerType', $element)
            , lblCustomerContact: $('#lblCustomerContact', $element)
            , lblCustomerName: $('#lblCustomerName', $element)
            , lblIdentificationDocument: $('#lblIdentificationDocument', $element)
            , lblLegalRepresentative: $('#lblLegalRepresentative', $element)
            , lblIdentificationDocumentoLR: $('#lblIdentificationDocumentoLR', $element)
            , lblActivationDate: $('#lblActivationDate', $element)
            , lblErrorMessage: $('#lblErrorMessage', $element)
            , ddlCACDAC: $('#ddlCACDAC', $element)
            , chkFidelizeTransac: $('#chkFidelizeTransac', $element)
            , chkLocution: $('#chkLocution', $element)
            , chkLocutionFidelize: $('#chkLocutionFidelize', $element)
            , chkEmail: $('#chkEmail', $element)
            , txtCurrentPhone: $('#txtCurrentPhone', $element)
            , txtTransacCost: $('#txtTransacCost', $element)
            , txtLocutionCost: $('#txtLocutionCost', $element)
            , txtEmail: $('#txtEmail', $element)
            , taNotas: $('#taNotas', $element)
            , btnSave: $('#btnSave', $element)
            , btnConstancy: $('#btnConstancy', $element)
            , btnClose: $('#btnClose', $element)
            , divRules: $('#divRules', $element)
            , divErrorAlert: $('#divErrorAlert', $element)
            , divEmail: $('#divEmail', $element)
            , divLocution: $('#divLocution', $element)
            , lblTitle: $('#lblTitle', $element)
            , lblAddress: $('#lblAddress', $element)
            , lblUrbanization: $('#lblUrbanization', $element)
            , lblCountry: $('#lblCountry', $element)
            , lblDepartment: $('#lblDepartment', $element)
            , lblProvince: $('#lblProvince', $element)
            , lblDistrict: $('#lblDistrict', $element)
            , lblPlaneCode: $('#lblPlaneCode', $element)
            , lblUbigeoCode: $('#lblUbigeoCode', $element)
        });
    }

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this,
                controls = this.getControls();
            controls.chkFidelizeTransac.addEvent(that, 'change', that.chkFidelizeTransac_change);
            controls.chkLocution.addEvent(that, 'change', that.chkLocution_change);
            controls.chkLocutionFidelize.addEvent(that, 'change', that.chkLocutionFidelize_change);
            controls.ddlCACDAC.addEvent(that, 'change', that.ddlCACDAC_change);
            controls.chkEmail.addEvent(that, 'change', that.chkEmail_change)
            controls.btnSave.addEvent(that, 'click', that.btnSave_click);
            controls.btnConstancy.addEvent(that, 'click', that.btnConstancy_click);
            controls.btnClose.addEvent(that, 'click', that.btnClose_click);
            that.maximizarWindow();
            that.windowAutoSize();
            that.loadSessionData();
            that.render();
        },
        loadSessionData: function () {
            var that = this,
            controls = this.getControls();
            controls.lblTitle.text("CAMBIO DE NÚMERO");
            var SessionTransac = JSON.parse(sessionStorage.getItem("SessionTransac"));

            //if (SessionTransac.UrlParams.IDSESSIONX == null || SessionTransac.UrlParams.IDSESSIONX == undefined) {
            //    Session.IDSESSION = '0';
            //} else {
            //    Session.IDSESSION = SessionTransac.UrlParams.IDSESSION;
            //}

            Session.IDSESSION = '565656565';
            
            Session.DATACUSTOMER = SessionTransac.SessionParams.DATACUSTOMER;
            Session.DATASERVICE = SessionTransac.SessionParams.DATASERVICE;
            Session.USERACCESS = SessionTransac.SessionParams.USERACCESS;

            //Session Data Guide
            //Session.DATACUSTOMER = {
            //    Account:"1.10097698",
            //    ActivationDate:"31/08/2004 12:00:00 a.m.",
            //    Address:"CA AV LAS TORRES 12 TO 1 INT 123",
            //    Assessor:"",
            //    BillingCycle:"20",
            //    BirthDate:"27/05/1972",
            //    BirthPlace:"Adserbeiyan",
            //    BirthPlaceID:"15",
            //    BusinessName:"APWZ2T1NB2",
            //    CivilStatus:"Soltero (a)",
            //    CivilStatusID:"1",
            //    CodCustomerType:"2",
            //    CodeCenterPopulate:"LMRC007",
            //    ContactCode:null,
            //    ContractID:"5745264",
            //    CustomerContact:"",
            //    CustomerID:"119744",
            //    CustomerType:"Consumer",
            //    DNIRUC:"22412131",
            //    Departament:"LIMA",
            //    District:"LA MOLINA",
            //    DocumentNumber:"22412131",
            //    DocumentType:"DNI",
            //    Email:"hola@gmail.com",
            //    Fax:"3333",
            //    FullName:"H7VWRBSWRF I1I80Y7L0H",
            //    InstallUbigeo:"150128",
            //    InvoiceAddress:"CA AV LAS TORRES 12 TO 1 INT 123",
            //    InvoiceCountry:"Peru",
            //    InvoiceDepartament:"LIMA",
            //    InvoiceDistrict:"LA MOLINA",
            //    InvoicePostal:null,
            //    InvoiceProvince:"LIMA",
            //    InvoiceUbigeo:"150128",
            //    InvoiceUrbanization:"ASO 123 SEC 1 ZONA FRENTE METRO",
            //    LastName:"I1I80Y7L0H",
            //    LegalAddress:"CA AV LAS TORRES 12 TO 1 INT 123",
            //    LegalAgent:"RNG1EAPAAM",
            //    LegalCountry:"Peru",
            //    LegalDepartament:"LIMA",
            //    LegalDistrict:"RIMAC",
            //    LegalPostal:null,
            //    LegalProvince:"LIMA",
            //    LegalUrbanization:"BW4ULDBVXZ",
            //    Modality:"",
            //    Name:"H7VWRBSWRF",
            //    OfficeAddress:"5TCFZAO4K3",
            //    PaymentMethod:"CONTADO",
            //    PhoneContact:"963963963",
            //    PhoneReference:"429285",
            //    PlaneCodeBilling:"LMRC007",
            //    PlaneCodeInstallation:"LMRC007",
            //    Position:"NJ74FNJP8B",
            //    ProductType:"",
            //    Province:"LIMA",
            //    Reference:"ASO 123 SEC 1 ZONA FRENTE METRO",
            //    Segment2:"",
            //    Sex:null,
            //    SiteCode:null,
            //    Telephone:"",
            //    Tradename:"UI7UC2PMGJ",
            //    Urbanization:null
            //}
            
            //Session.DATASERVICE = {
            //    ActivationDate:"2014-12-11 19:41:52",
            //    CableValue:"F",
            //    Campaign:"CAMPANIA NAVIDENIA",
            //    CellPhone:null,
            //    ChangedBy:null,
            //    CodePlanTariff:"1435",
            //    ContractID:"",
            //    FlagPlatform:null,
            //    FlagTFI:null,
            //    InternetValue:"F",
            //    Introduced:"/Date(-62135578800000)/",
            //    IntroducedBy:null,
            //    IsLTE:"false",
            //    IsNot3Play:"0",
            //    MSISDN:null,
            //    NumberICCID:null,
            //    NumberIMSI:null,
            //    PIN1:null,
            //    PIN2:null,
            //    PUK1:null,
            //    PUK2:null,
            //    Plan:"Plan 2 Play Cable -Telefono",
            //    PlanTariff:null,
            //    ProviderID:null,
            //    Reason:"A solicitud Cliente",
            //    Seller:"",
            //    StateAgreement:null,
            //    StateDate:"15/02/2017 09:47:46 a.m.",
            //    StateLine:"Activo",
            //    TelephonyValue:"T",
            //    TermContract:"",
            //    TypeProduct:null,
            //    ValidFrom:"/Date(-62135578800000)/"
            //}
            
            //Session.USERACCESS = {
            //    accessStatus:"0",
            //    areaId:382,
            //    areaName:"Sistemas Administrativos",
            //    firstName:"SAMUEL SALOMON",
            //    fullName: "SAMUEL SALOMON INGA BORJA",
            //    lastName1:"INGA",
            //    lastName2:"BORJA",
            //    login:"C12640",
            //    optionPermissions:"ACP_CCF,ACP_COA,ACP_CHR,ACP_DMC,ACP_RVM,ACP_NRCP,ACP_EDI,ACP_RBO,ACP_DECB,ACP_DLFRA,ACP_EMD,ACP_CTD,ACP_EPS,ACP_PROT,ACP_BDI,ADT_SRV,ACP_DCR,ACP_BCCR,ACP_ARW847,ACP_ERW847,ACP_EPW847,ACP_ECW847,ACP_THFC,ACP_DCQ,ACP_AIA,ACP_DLCB,ACP_BNR,ACP_BICCID,ACP_GLC,ACP_DDC,ACP_DESON,ACP_RECANC,ACP_BUTELE,ACP_BUCUEN,ACP_BUNUSE,NO_BIOMETR,ACP_ITA,ACP_BCCM,ACP_PRI,ACP_CSE,ACP_CMD,ACP_VTD,ACP_DCC,ACP_DEM,ACP_ARC,ACP_TVP,ACP_CDI,ACP_ADL,ACP_BELI,ACP_RAO,ACP_BPRO,ACP_BDSC,ACP_ETC,ACP_ADPP,ACP_ATD,ACP_COCT,ACP_BTE,ACP_CAP,ACP_MPF,ACP_MASW,ACP_OTL,ACP_EBDP,ACP_EDPM,ACP_ACDO,ACP_CCW847,ACP_CTS,ACP_DBQ,ACP_GNICH,ACP_BUNOMB,TVS_BNR,ACP_ORDT,ACP_APST,ACP_AUBOLS,ACP_CERTA,ACP_CHL,ACP_GAR,ACP_CRP,ACP_CCS,ACP_ECE,ACP_ECA,ACP_DLN,ACP_ALI,ACP_PPB,ACP_AFM,ACP_DCA,ACP_LKHR,ACP_ETD,ACP_TSTV,ACP_111,ACP_RTI,ACP_RADI,ACP_SNCT,ACP_BRS,ACP_MFA,ACP_MFP,ACP_IPW,ACP_OTR,APC_ICR,ACP_RCB,ACP_CDOA,ACP_CSEG,ACP_EXW847,ACP_AMCP,ACP_RLTCSC,ACP_CLICRE,ACP_ADDL,ACP_GAPN,ACP_TIO,ACP_BNS,TVS_BDI,ADT_BTN,ACP_CSUTA,ACP_DIT,ACP_LMC,ACP_CTL,ACP_CPA,ACP_CTT,ACP_CTM,ACP_CVF,ACP_PCP,ACP_TCI,ACP_DEQ,ACP_RDL,ACP_MVM,ACP_DAR,ACP_BDM,ACP_VFP,ACP_DEAS,ACP_DECB,ACP_BFRA,ACP_AMD,ACP_ZM,ABC_XY,ACP_TSRV,ACP_TTD,ACP_OVM,ACP_RLD,ACP_CDFT,ACP_APD,ACP_AVIP,ACP_PMAT,ACP_RMW,ACP_MCC,ACP_IRW,ACP_SIM,ACP_ACCR,ACP_FPCT,ACP_CRW847,ACP_IMR,ACP_BDT,ACP_ATE,ACP_USFP,TVS_BCT,TVS_BCI,TVS_BICCID,ACP_DCT,ACP_TCT,ACP_CHS,ACP_CAT,ACP_CPP,ACP_CPE,ACP_BDL,ACP_CLD,ACP_PVC,ACP_DTA,ACP_VNS,ACP_ACT,ACP_RBX,ACP_RBR,ACP_DCAE,ACP_ADEE,ACP_DELI,ACP_HR,ACP_HBA,ACP_DPRO,ACP_SFD,ACP_DSD,ACP_TVNT,ACP_FPRT,ADT_TIP,ACP_APW,ACP_EPW,ACP_CRW,ACP_ERW,ACP_EXW,ACP_MRS,ACP_RGC,ACP_CBDP,ACP_FPC,ACP_ADSC,ACP_PRC,ACP_ADHL,ACP_DRE,ACP_ASEG,ACP_BUDOCI,ACP_ASIFC,ACP_DCL,ACP_BCL,ACP_CHP,ACP_CAE,ACP_CFT,ACP_CAA,ACP_CDN,ACP_PCD,ACP_DTO,ACP_ECD,ACP_ECF,ACP_DEP,ACP_AAS,ACP_ETR,ACP_RBL,ACP_RBD,ACP_VFA,ACP_ITPD,ACP_RRF,ACP_ECHR,ACP_DDSC,ACP_FAD,ACP_MTD,ACP_DSCB,ACP_BNO,ACP_PCC,ADT_ACCS,ACP_HDE,ACP_ACCS,ACP_BLC,ACP_ISHL,ACP_DED,ACP_ADA,ACP_RLCFP,ACP_BURASO,TVS_BNO,TVS_BRS,ACP_DLI,ACP_CNT,ACP_CAC,ACP_DCO,ACP_PEC,ACP_ECB,ACP_LVI,ACP_DGR,ACP_CVM,ACP_DMME,ACP_DAC,ACP_ECL,ACP_RCR,ACP_RBE,ACP_EXD,ACP_VFS,ACP_DLAS,ACP_BROB,ACP_MNZM,ACP_TMD,ACP_RTD,ACP_IEA,ACP_TSMV,ACP_ADED,ACP_RSF,ACP_RTE,ACP_CCVIP,ACP_ARW,ACP_CCW,ACP_ECW,ACP_PAC,ACP_PACD,ACP_ATF,ACP_RMC847,ACP_CRTV,SST_VCC,ACP_INGV,ACP_PVR,ACP_AA4G,ACP_GDI1,ACP_PERF,ACP_ASDL,ACP_AUCA,ACP_PBDE,ACP_AUBOLZ,ACP_CCBIOS,ACP_ECTEN,ACP_DFA,ACP_CTR,ACP_CSA,ACP_CLL,ACP_CRO,ACP_PFP,ACP_CHA,ACP_ECC,ACP_CTC,ACP_CDL,ACP_CNC,ACP_AMC,ACP_ARA,ACP_DRCE,ACP_PFPM,ACP_EEL,ACP_IDL,ACP_RDI,ACP_RBI,ACP_VFC,ACP_SLD,ACP_CCT,ACP_BCT,ACP_UCR,ACP_CDBP,ACP_IRW847,ACP_IPW847,ACP_APW847,ACP_RLSNCT,ACP_CFP,ACP_AFIC,ACP_TCII,ACP_PVCO,ACP_SIC,ACP_HLIN,ACP_BCID,ACP_GLCR,ACP_PFCE,ACP_VBIO,ACP_STAT,ACP_TLLE,ACP_VBDE,ACP_GTA,ACP_ETA,ACP_CONTA,ACP_BS20,ACP_EAMV,",
            //    profileId:"4",
            //    profiles:"4",
            //    sapVendorId:0,
            //    searchUser:"0",
            //    userId:1100,
            //    UrlParams: "",
            //    SUREDIRECT: "HFC"
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
        },
        loadConfigVariables: function () {
            var that = this, controls = that.getControls();
            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '/Transactions/HFC/ChangePhoneNumber/GetMessage',
                data: {},
                success: function (response) {
                    that.strHFCCambioNumeroCostoTrans = response[0];
                    that.strHFCCambioNumeroCostoLocucion = response[1];
                    that.strEstadoContratoInactivo = response[2];
                    that.strEstadoContratoSuspendido = response[3];
                    that.strEstadoContratoReservado = response[4];
                    that.strMsjContratoInactivo = response[5];
                    that.strMsjContratoSuspendido = response[6];
                    that.strMsjContratoReservado = response[7];
                    that.strMsjSinTelefonoFijo = response[8];
                    that.strPDFServer = response[9];
                    that.strSubClaseCode = response[10];
                    that.strMensajeErrorConsultaIGV = response[11];
                    that.strMensajeTransaccionFTTH = response[12];//EVALENZS - INICIO
                    that.strPlanoFTTH = response[13];//EVALENZS - FIN
                    controls.txtTransacCost.val(that.strHFCCambioNumeroCostoTrans);
                    controls.txtLocutionCost.val(that.strHFCCambioNumeroCostoLocucion);
                    that.IniGetConsultIGV();
                    
                    that.validatePage();
                }
            });
        },
        IniGetConsultIGV: function () {
            var that = this,
            controls = that.getControls(),
            param = {};

            param.strIdSession = Session.IDSESSION;
            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(param),
                url: '/Transactions/CommonServices/GetConsultIGV',
                success: function (response) {
                    if (response.data != null) {
                        var CostNoIGV = that.strHFCCambioNumeroCostoTrans;
                        var CostLocNoIGV = that.strHFCCambioNumeroCostoLocucion;
                        var igv = parseFloat(response.data.igvD) + 1.00;
                        that.strHFCCambioNumeroCostoTransSinIGV = (parseFloat(CostNoIGV) / parseFloat(igv)).toFixed(2);
                        that.strHFCCambioNumeroCostoLocuSinIGV = (parseFloat(CostLocNoIGV) / parseFloat(igv)).toFixed(2);
                        that.strValidateIGV = '0';
                    } else {
                        that.strValidateIGV = '1';
                        controls.divErrorAlert.show(); controls.lblErrorMessage.text(that.strMensajeErrorConsultaIGV);
                    }
                },
                error: function (response) {
                    that.strValidateIGV = '1';
                    controls.divErrorAlert.show(); controls.lblErrorMessage.text(that.strMensajeErrorConsultaIGV);
                }
            });
        },
        validatePage: function(){
            var that = this,
                controls = this.getControls();

            //EVALENZS - INICIO - CAMBIO DE NUMERO
            var strPlano = Session.DATACUSTOMER.PlaneCodeInstallation;
            if (strPlano.search(that.strPlanoFTTH) > 0 && that.strMensajeTransaccionFTTH != '') {
                alert(that.strMensajeTransaccionFTTH.replace('{0}', 'CAMBIO DE NÚMERO'), "Alerta", function () {
                    parent.window.close();
                });
                that.blockForm();
            }
            //EVALENZS - FIN

            if (Session.DATASERVICE.StateLine == that.strEstadoContratoInactivo) {
                alert(that.strMsjContratoInactivo, 'Alerta', function () {
                    parent.window.close();
                });
                that.blockForm();
            }
            if (Session.DATASERVICE.StateLine == that.strEstadoContratoReservado) {
                alert(that.strMsjContratoReservado, 'Alerta', function () {
                    parent.window.close();
                });
                that.blockForm();
            }
            if (Session.DATASERVICE.StateLine == that.strEstadoContratoSuspendido) {
                alert(that.strMsjContratoSuspendido, 'Alerta', function () {
                    parent.window.close();
                });
                that.blockForm();
            }
            if (Session.DATASERVICE.TelephonyValue != 'T') {
                alert(that.strMsjSinTelefonoFijo, 'Alerta', function () {
                    parent.window.close();
                });
                that.blockForm();
            }

            controls.btnConstancy.prop('disabled', true);
            that.load();
        },
        load: function () {
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

            controls.divEmail.hide();
            controls.divLocution.hide();
            controls.divErrorAlert.hide();
            controls.txtEmail.val(Session.DATACUSTOMER.Email);
            that.getBusinessRules();
            that.getCACDAC();
            that.getCurrentPhone();
            that.loadHeaderTransaction();
        },
        blockForm: function () {
            var that = this,
                controls = this.getControls();

            controls.btnSave.prop('disabled', true);
            controls.btnConstancy.prop('disabled', true);
        },
        loadHeaderTransaction: function () {
            var that = this,
                controls = this.getControls();

            controls.lblContract.text(Session.DATACUSTOMER.ContractID);
            controls.lblCustomerType.text(Session.DATACUSTOMER.CustomerType);
            controls.lblCustomerContact.text(Session.DATACUSTOMER.FullName);
            controls.lblCustomerName.text(Session.DATACUSTOMER.BusinessName);
            controls.lblIdentificationDocument.text(Session.DATACUSTOMER.DNIRUC);
            controls.lblLegalRepresentative.text(Session.DATACUSTOMER.LegalAgent);
            controls.lblIdentificationDocumentoLR.text(Session.DATACUSTOMER.DocumentNumber);
            controls.lblActivationDate.text(Session.DATACUSTOMER.ActivationDate);

            controls.lblAddress.text(Session.DATACUSTOMER.InvoiceAddress);
            controls.lblUrbanization.text(Session.DATACUSTOMER.InvoiceUrbanization);
            controls.lblCountry.text(Session.DATACUSTOMER.InvoiceCountry);
            controls.lblDepartment.text(Session.DATACUSTOMER.InvoiceDepartament);
            controls.lblProvince.text(Session.DATACUSTOMER.InvoiceProvince);
            controls.lblDistrict.text(Session.DATACUSTOMER.InvoiceDistrict);
            controls.lblPlaneCode.text(Session.DATACUSTOMER.PlaneCodeBilling);
            controls.lblUbigeoCode.text(Session.DATACUSTOMER.InvoiceUbigeo);

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
        getCACDAC: function () {
            var that = this,
                controls = that.getControls(),
                objCacDacType = {};

            objCacDacType.strIdSession = Session.IDSESSION;
            var parameters = {};
            parameters.strIdSession = Session.IDSESSION;
            parameters.strCodeUser = Session.USERACCESS.login;

            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(parameters),
                url: '/Transactions/CommonServices/GetUsers',

                success: function (results) {
                    var cacdac = results.Cac;
                    //console.log"cacdac: " + cacdac);
                    $.ajax({
                        type: 'POST',
                        contentType: "application/json; charset=utf-8",
                        dataType: 'json',
                        data: JSON.stringify(objCacDacType),
                        url: '/Transactions/CommonServices/GetCacDacType',
                        success: function (response) {
                            controls.ddlCACDAC.append($('<option>', { value: '', html: 'Seleccionar' }));
                            if (response.data != null) {}
                            var itemSelect;
                                $.each(response.data.CacDacTypes, function (index, value) {
                                
                                    if (cacdac === value.Description) {
                                        controls.ddlCACDAC.append($('<option>', { value: value.Code, html: value.Description }));
                                        itemSelect = value.Code;
                                        
                                    } else {
                                        controls.ddlCACDAC.append($('<option>', { value: value.Code, html: value.Description }));
                                    }
                                });
                                if (itemSelect != null && itemSelect.toString != "undefined") {
                                    //console.log"valor itemSelect: " + itemSelect);
                                    $("#ddlCACDAC option[value=" + itemSelect + "]").attr("selected", true);
                            }
                        }
                    });
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
        getCurrentPhone: function () {
            var that = this,
                controls = that.getControls(),
                objPhone = {};

            objPhone.strIdSession = Session.IDSESSION;
            objPhone.strContractID = Session.DATACUSTOMER.ContractID;
            objPhone.strProductType = 'HFC';

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '/Transactions/HFC/ChangePhoneNumber/GetCurrentPhone',
                data: JSON.stringify(objPhone),
                success: function (response) {
                    if (response.NRO_CELULAR != null) {
                    controls.txtCurrentPhone.val(response.NRO_CELULAR);
                    } else {
                        var msg = 'Error al obtener el número del cliente.';
                        controls.divErrorAlert.show(); controls.lblErrorMessage.text(msg);
                        controls.btnSave.prop('disabled', true);
                    }
                }
            });
        },
        chkFidelizeTransac_change: function (sender, arg) {
            var that = this,
                controls = this.getControls();

            if (sender.prop('checked')) {
                controls.txtTransacCost.val('0.00');
            } else {
                controls.txtTransacCost.val(that.strHFCCambioNumeroCostoTrans);
            }
        },
        chkLocutionFidelize_change: function (sender, arg) {
            var that = this,
                controls = this.getControls();

            if (sender.prop('checked')) {
                controls.txtLocutionCost.val('0.00');
            } else {
                controls.txtLocutionCost.val(that.strHFCCambioNumeroCostoLocucion);
            }
        },
        chkLocution_change: function (sender, arg) {
            var that = this,
                controls = this.getControls();

            if (sender.prop('checked')) {
                controls.divLocution.show();
            } else {
                controls.divLocution.hide();
            }
        },
        ddlCACDAC_change: function (sender, arg) {
            var that = this, controls = that.getControls();
            var desc = $("#ddlCACDAC option:selected").text();
            if (controls.ddlCACDAC.val() == '') {
                desc = '';
            }
            that.strSalePointDesc = desc;
        },
        chkEmail_change: function (sender, arg) {
            var that = this,
                controls = this.getControls();

            if (sender.prop('checked')) {
                controls.divEmail.show();
                controls.txtEmail.focus();
            } else {
                controls.divEmail.hide();
        }
        },
        btnSave_click: function () {
            var that = this,
                controls = that.getControls();

            //if (that.strValidateIGV == '1') {
            //    if (!controls.chkFidelizeTransac.prop('checked')) {
            //        alert(that.strMensajeErrorConsultaIGV, 'Alerta');
            //        return false;
            //    }

            //    if (controls.chkLocution.prop('checked')) {
            //        if (!controls.chkLocutionFidelize.prop('checked')) {
            //            alert(that.strMensajeErrorConsultaIGV, 'Alerta');
            //            return false;
            //        }
            //    }
            //}

            if (controls.chkEmail.prop('checked')) {
                if (controls.txtEmail.val() == '') {
                    alert('Ingresar un correo electrónico.', 'Alerta');
                    return false;
                }

                var regx = /^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$/;
                var blvalidate = regx.test(controls.txtEmail.val());
                if (blvalidate == false) {
                    alert('Ingresar un correo válido.', 'Alerta');
                    return false;
                }
            }

            if (controls.ddlCACDAC.val() == '') {
                alert('Seleccionar punto de atención.', 'Alerta');
                return false;
            }

            that.getNewPhoneNumber();
        },
        getNewPhoneNumber: function(){
            var that = this,
                controls = that.getControls(),
                objChangeNumber = {};

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

            objChangeNumber.IDSESSION = Session.IDSESSION;
            objChangeNumber.CLASIF_RED = "0";
            objChangeNumber.CONTRACT_ID = Session.DATACUSTOMER.ContractID;
            objChangeNumber.CUSTOMER_ID = Session.DATACUSTOMER.CustomerID;
            objChangeNumber.TYPE_NUMBER = 'HFC';
            
            if (controls.chkFidelizeTransac.prop('checked')) {
                objChangeNumber.FLAG_FIDEL_TRANS = '1';
                objChangeNumber.COST_TRANS = '0';
                objChangeNumber.COST_TRANS_IGV = '0';
            } else {
                objChangeNumber.FLAG_FIDEL_TRANS = '0';
                objChangeNumber.COST_TRANS = that.strHFCCambioNumeroCostoTransSinIGV;
                objChangeNumber.COST_TRANS_IGV = controls.txtTransacCost.val();
            }
            
            if (controls.chkLocution.prop('checked')) {
                objChangeNumber.FLAG_LOCU = '1';
                if (controls.chkLocutionFidelize.prop('checked')) {
                    objChangeNumber.FLAG_FIDEL_LOCU = '1';
                    objChangeNumber.COST_LOCU = '0';
                    objChangeNumber.COST_LOCU_IGV = '0';
                } else {
                    objChangeNumber.FLAG_FIDEL_LOCU = '0';
                    objChangeNumber.COST_LOCU = that.strHFCCambioNumeroCostoLocuSinIGV;
                    objChangeNumber.COST_LOCU_IGV = controls.txtLocutionCost.val();
                   
                }
            } else {
                objChangeNumber.FLAG_LOCU = '0';
                objChangeNumber.FLAG_FIDEL_LOCU = '0';
                objChangeNumber.COST_LOCU = '0';
                objChangeNumber.COST_LOCU_IGV = '0';
            }

            if (controls.chkEmail.prop('checked')) {
                objChangeNumber.FLAG_SEND_EMAIL = '1';
                objChangeNumber.EMAIL = controls.txtEmail.val();
            } else {
                objChangeNumber.FLAG_SEND_EMAIL = '0';
                objChangeNumber.EMAIL = "0";
            }

            objChangeNumber.NATIONAL_CODE = "";
            objChangeNumber.NUMBER_PHONES = '5';
            objChangeNumber.TYPE_PHONE = "";
            objChangeNumber.NRO_TELEF = controls.txtCurrentPhone.val();
            objChangeNumber.HLR_CODE = "";
            objChangeNumber.FLAG_PLAN_TIPI = "1";
            objChangeNumber.POINT_ATTENTION = that.strSalePointDesc;
            objChangeNumber.NOTES = controls.taNotas.val();
            objChangeNumber.CUSTOMER_TYPE = Session.DATACUSTOMER.CustomerType;
            objChangeNumber.CONTACT = Session.DATACUSTOMER.BusinessName;
            objChangeNumber.FULL_NAME = Session.DATACUSTOMER.FullName;
            objChangeNumber.DOCUMENT = Session.DATACUSTOMER.DNIRUC;
            controls.divErrorAlert.hide();

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '/Transactions/HFC/ChangePhoneNumber/ExecuteChangePhoneNumber',
                data: JSON.stringify(objChangeNumber),
                success: function (response) {
                    if (response != null) {
                        if (response.RESPONSE_CODE == '0') {
                            var msg = response.RESPONSE_MESSAGE + '<br />' + 'El nuevo número es: ' + response.NEW_PHONE;
                            alert(msg, "Informativo");
                            controls.divErrorAlert.show(); controls.lblErrorMessage.text('El nuevo número es: ' + response.NEW_PHONE);
                            that.strPDFRoute = response.ROUTE_PDF;
                            controls.btnSave.prop('disabled', true);
                            controls.btnConstancy.prop('disabled', false);
                        } else {
                            alert('Error: ' + response.RESPONSE_MESSAGE, "Alerta");
                            controls.btnSave.prop('disabled', true);
                        }
                    }
                },
                error: function (xhr, status, error) {
                    var msg = 'Error en el servicio de Cambio de Número.';
                    controls.divErrorAlert.show(); controls.lblErrorMessage.text(msg);
                    controls.btnSave.prop('disabled', true);
                }
            });
        },
        btnConstancy_click: function () {
            var that = this,
              controls = that.getControls();

            if (that.strPDFRoute != '') {
                var newRoute = that.strPDFRoute.substring(that.strPDFRoute.indexOf('SIACUNICO'));
                newRoute = newRoute.replace(new RegExp('/', 'g'), '\\');
                newRoute = that.strPDFServer + newRoute;

                ReadRecordSharedFile(Session.IDSESSION, newRoute);
            } else {
                alert('No se ha cargado correctamente el archivo de la constancia.', "Alerta");
            }
            
        }, 
        btnClose_click: function () {
            parent.window.close();
        },
        strUrlLogo: window.location.protocol + '//' + window.location.host + '/Images/loading2.gif',
        strHFCCambioNumeroCostoTrans: '',
        strHFCCambioNumeroCostoTransSinIGV: '',
        strHFCCambioNumeroCostoLocucion: '',
        strHFCCambioNumeroCostoLocuSinIGV: '',
        strEstadoContratoInactivo: '',
        strEstadoContratoSuspendido: '',
        strEstadoContratoReservado: '',
        strMsjContratoInactivo: '',
        strMsjContratoSuspendido: '',
        strMsjContratoReservado: '',
        strMsjSinTelefonoFijo: '',
        strPDFRoute: '',
        strSalePointDesc: '',
        strPDFServer: '',
        strSubClaseCode: '',
        strIGV: '',
        strMensajeErrorConsultaIGV: '',
        strValidateIGV: ''
    }

    $.fn.ChangePhoneNumber = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('FixedChangePhoneNumber'),
                options = $.extend({}, $.fn.ChangePhoneNumber.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('FixedChangePhoneNumber', data);
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

    //$('#FixedChangePhoneNumber').ChangePhoneNumber();
    $('#divBody').ChangePhoneNumber();

})(jQuery);