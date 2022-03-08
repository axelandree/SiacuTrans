$(document).ready(function () {
    var sessionTransac = JSON.parse(sessionStorage.getItem("SessionTransac"));
    //console.logsessionTransac);

    var oCustomer = sessionTransac.SessionParams.DATACUSTOMER;
    var oUserAccess = sessionTransac.SessionParams.USERACCESS;
    var oDataService = sessionTransac.SessionParams.DATASERVICE;     
 
    $("#lblContract").text((oCustomer.ContractID == null) ? "" : oCustomer.ContractID);
    $("#lblContact").text((oCustomer.FullName == null) ? "" : oCustomer.FullName);
    $("#lblCustomerName").text((oCustomer.BusinessName == null) ? "" : oCustomer.BusinessName);
    $("#lblDateActivation").text((oCustomer.ActivationDate == null) ? "" : oCustomer.ActivationDate);
    $("#lblReprLegal").text((oCustomer.LegalAgent == null) ? "" : oCustomer.LegalAgent);
    $("#lblIdentificationDocument").text((oCustomer.DocumentNumber == null) ? "" : oCustomer.DocumentNumber);

    
    if (oCustomer.CustomerType != null) {
        if (oCustomer.CustomerType != "") {
            $("#lblTypeCustomer").text(oCustomer.CustomerType);
        }
        else {
            if (oCustomer.objPostDataAccount.CustomerType != "") {
                $("#lblTypeCustomer").text(oCustomer.objPostDataAccount.CustomerType);
            }
        }
    }
    else {
        if (oCustomer.objPostDataAccount.CustomerType != "") {
            $("#lblTypeCustomer").text(oCustomer.objPostDataAccount.CustomerType);
        }
    }

  
    $("#lblDocReprLegal").text((oCustomer.DNIRUC == null) ? "" : oCustomer.DNIRUC);
    $("#lblCycleBilling").text((oCustomer.CustomerContact == null) ? "" : oCustomer.CustomerContact);
    $("#lblPlanName").text((oDataService.Plan == null) ? "" : oDataService.Plan);
 
    if (oCustomer.BillingCycle != null) {
        if (oCustomer.BillingCycle != "") {
            $("#lblCycleFacture").text(oCustomer.BillingCycle);
        }
        else {
            if (oCustomer.objPostDataAccount.BillingCycle != "") {
                $("#lblCycleFacture").text(oCustomer.objPostDataAccount.BillingCycle);
            }
        }
    }
    else {
        if (oCustomer.objPostDataAccount.BillingCycle != "") {
            $("#lblCycleFacture").text(oCustomer.objPostDataAccount.BillingCycle);
        }
    }

    $("#lblLimitCredit").text((oCustomer.objPostDataAccount.CreditLimit == null) ? "" : oCustomer.objPostDataAccount.CreditLimit);
    $("#lblCustomerId").text((oCustomer.CustomerID == null) ? "" : oCustomer.CustomerID);
    $("#lblAddress").text((oCustomer.InvoiceAddress == null) ? "" : oCustomer.InvoiceAddress);
    $("#lblAddressNote").text((oCustomer.Reference == null) ? "" : oCustomer.Reference);
    $("#lblDepartamento").text((oCustomer.InvoiceDepartament == null) ? "" : oCustomer.InvoiceDepartament);
    $("#lblDistrito").text((oCustomer.InvoiceDistrict == null) ? "" : oCustomer.InvoiceDistrict);
    $("#lblCodePlans").text((oCustomer.PlaneCodeInstallation == null || oCustomer.PlaneCodeInstallation==="null") ? "" : oCustomer.PlaneCodeInstallation);
    $("#lblPais").text((oCustomer.InvoiceCountry == null) ? "" : oCustomer.InvoiceCountry);
    $("#lblProvincia").text((oCustomer.InvoiceProvince == null) ? "" : oCustomer.InvoiceProvince);
    $("#lblUbigeo").text((oCustomer.InstallUbigeo == null) ? "" : oCustomer.InstallUbigeo);

    
});
 
