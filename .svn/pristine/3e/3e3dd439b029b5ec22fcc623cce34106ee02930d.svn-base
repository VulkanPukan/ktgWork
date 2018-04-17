//This file is used in pages that are related to customer 
//bind data to variable
var customerdataSource = new kendo.data.DataSource({
    transport: {
        read: function (options) {
            $.ajax({
                type: "GET",
                url: "../Customer/GetCustomerList",
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                cache: false,
                success: function (result) {
                    options.success(result.Data);
                }
            });
        }
    },
    pageSize: 20
});

//initialize grid with columns and bind data
$("#customersGrid").kendoGrid({
    allowCopy: true,
    scrollable: false,
    sortable: true,
    filterable: true,
    resizable: true,
    columns: [
        {
            field: "OrganizationName",
            title: "Organization",
            width: "150px",
            //locked: true,
            template: '<a href="javascript:void(0)" onclick="ShowEditCustomer(#=CustomerId#);">#=OrganizationName#</a>'
        },
        {
            field: "MaxNumberOfAthletes",
            title: "Max Athletes",
            width: "115px",
            //locked: true,
            attributes: {
                style: 'white-space: nowrap;text-align:center;'
            }
        },
        {
            field: "NumberOfActiveAthletes",
            title: "Active Athlete",
            width: "115px",
            //locked: true,
            attributes: {
                style: 'white-space: nowrap;text-align:center;'
            }
        },
        {
            field: "PricePerActiveAthelete",
            title: "Price/ Athlete",
            width: "115px",
            //locked: true,
            attributes: {
                style: 'white-space: nowrap;text-align:center;'
            }
        },
        {
            field: "NumberOfLocations",
            title: "# of Locations",
            width: "115px",
            template: '<a href="javascript:void(0)" onclick="ShowLocationPage();">#=NumberOfLocations#</a>',
            attributes: {
                style: 'white-space: nowrap;text-align:center;'
            }
        },
        {
            field: "BillingAmount",
            title: "Billing Amt",
            width: "90px",
            attributes: {
                style: 'white-space: nowrap;text-align:center;'
            },
            template: "<em class='fa fa-usd'></em> #=BillingAmount#"
        },
        {
            field: "StartDate",
            title: "Customer Since",
            width: "120px",
            template: "#= (StartDate == null ) ? \'\' : kendo.toString(kendo.parseDate(StartDate, 'yyyy-MM-dd'), 'MM/dd/yyyy') #",
            attributes: {
                style: 'white-space: nowrap '
            }
        },
        {
            field: "IsActive",
            title: "Status",
            width: "90px",
            template: '#if(IsActive==true){# ' + 'Active' + ' #}else{# ' + 'Inactive' + '#}#',
            attributes: {
                style: 'white-space: nowrap '
            }
        },
        //{
        //    field: 'FreeTrialStartDate',
        //    title: 'Free Trial Start Date',
        //    template: "#= kendo.toString(kendo.parseDate(StartDate, 'yyyy-MM-dd'), 'MM/dd/yyyy') #",
        //    width: "73px"
        //},
        //{
        //    field: 'FreeTrialEndDate',
        //    title: 'Free Trial End Date',
        //    template: "#= kendo.toString(kendo.parseDate(StartDate, 'yyyy-MM-dd'), 'MM/dd/yyyy') #",
        //    width: "73px"
        //},
        //{
        //    field: 'FreeTrailNoOfDays',
        //    title: 'Trial Days left',
        //    width: "73px"
        //},
        {
             field: 'TypeOfCustomer',
             template: '#if(TypeOfCustomer==1){# ' + 'Profit' + ' #}else if(TypeOfCustomer==1){# ' + 'Non Profit' + '#}#',
             title: 'Type',
             width: "80px"
         },
        {
            field: "Category",
            title: "Category",
            width: "110px",
            attributes: {
                style: 'white-space: nowrap;text-align:center;'
            },
            template: "#=Category#"
        },
        {
            field: 'InTrial',
            template: '#= CheckFreeTrialDate(kendo.parseDate(FreeTrialEndDate, "yyyy-MM-dd"))#',
            title: 'In Trial',
            width: "85px",
            attributes: {
                style: 'white-space: nowrap;text-align:center;'
            }
        },
        {
            field: "Actions",
            title: "Actions",
            filterable: false,
            width: "140px",
            template: '<div class="btn-group dropdown mb-sm">' +
                    '<button type="button" data-toggle="dropdown" class="btn btn-info dropdown-toggle" >Select…</button>' +
                    '<button type="button" data-toggle="dropdown" class="btn btn-info dropdown-toggle" ><span class="caret"></span></button>' +
                    '<ul role="menu" class="dropdown-menu" style="min-width:102px !important;" >' +
                      '#if(FreeTrailNoOfDays == 0){#' +
                            '#if(IsActive==true){#' + '<li><a href="javascript:void(0);" onclick="UpdateCustomerStatus(\'#=CustomerId#\');">' + ' #}else{# ' +
                            '<li><li><a href="javascript:void(0);" onclick="ShowActivationConfirmationPopup(\'#=CustomerId#\');">' + '#}#' +
                            '#if(IsActive==true){# ' + 'Deactivate' + ' #}else{# ' + 'Activate' + '#}#' + '</a>' +
                            '</li>' +
                      '#}else{#' +
                      //'<li><a href="javascript:void(0);" onclick="ShowActivationConfirmationPoup(#=FreeTrailNoOfDays#);">Enable Free Trial</a></li>' +
                      '#if(IsActive==true){#' + '<li><a href="javascript:void(0);" onclick="UpdateCustomerStatus(\'#=CustomerId#\');">' + ' #}else{# ' +
                            '<li><li><a href="javascript:void(0);" onclick="ShowActivationConfirmationPopup(\'#=CustomerId#\');">' + '#}#' +
                            '#if(IsActive==true){# ' + 'Deactivate' + ' #}else{# ' + 'Activate' + '#}#' + '</a>' +
                            '</li>' +
                      '#}#' +
                      '<li><a href="javascript:void(0);" onclick="ShowDeleteConfirmationPopup(#=CustomerId#);">Delete</a></li>' +
                      '<li><a href="javascript:void(0);" onclick="ShowEditCustomer(#=CustomerId#);">View Profile</a></li>' +
                      '<li><a href="javascript:void(0);" ">View Dashbaord</a></li>' +
                   '</ul>' +
                '</div>'
        }
    ],
    dataSource: customerdataSource
});


//pagination 
$("#customerPager").kendoPager({
    autoBind: true,
    dataSource: customerdataSource
});

function ShowDeleteConfirmationPopup(id) {
    ConfirmationMessageBox({
        confirmationMessage: 'Are you sure you want to Delete Customer?',
        popupTitle: 'Delete Customer',
        callback: function () {
            deleteCustomer(id);
        }
    });
}

function deleteCustomer(id) {
    $.ajax({
        url: "../Customer/DeleteCustomer",
        type: "POST",
        async: false,
        data: { id: id },
        success: function (resp) {
            if (resp.Status === 1) {
                customerdataSource.read();
                $('#customersGrid').data('kendoGrid').refresh();
                alert(resp.Message);

            }
            else {
                alert(resp.Message);
            }

        }, error: function () {
            alert(resp.Message);
        }
    });
}

function ValidateAndSaveCustomerInfo() {
    var isFormValidated = true;
    $('#accordionAdd :input[required]:visible').each(function () {
        console.log(this + "; value:" + this.value);
        var value = this.value;
        if ($(this).is("select")) {
            if (value == "0")
                value = "";
        }
        if (value === "") {
            $(this.parentElement).addClass("has-error");
            alert(this.placeholder + " is a required field");
            $(this).focus();
            isFormValidated = false;
            return false;
        }
    });

    if (isFormValidated != true) {
        return false;
    }

    ValidateOrganizationName();
}

function ValidateOrganizationName() {
    var customerID = $("#CustomerId").val() == '' ? 0 : $("#CustomerId").val();
    $.ajax({
        url: '../Customer/OrganizationExists',
        type: "POST",
        data: JSON.stringify({ 'organizationName': $("#OrganizationName").val(), 'customerID': customerID }),
        contentType: 'application/json',
        dataType: "json",
        success: function (result) {
            if (result.Status == 2) {
                $($("#OrganizationName")[0].parentElement).addClass("has-error");
                alert(result.Message);
                $("#OrganizationName")[0].focus();
                return false;
            }
            else {
                $($("#OrganizationName")[0].parentElement).removeClass("has-error")
                submitAddCustomer();
            }
        },
        error: function (req, status, errorObj) {
            alert('Error: Please try again');
        }
    });
}
//Add customer
function submitAddCustomer() {
    var isFormValidated = true;
    $(":text").each(function () {
        if (this.required == true) {
            if (this.value === "") {
                $(this.parentElement).addClass("has-error");
                alert(this.placeholder + " is a required field");
                $(this).focus();
                isFormValidated = false;
                return false;
            }
        }
    });
    if (isFormValidated != true) {
        return false;
    }

    //todo: do clien side validation
    var customerModel = {
        'CustomerId': $("#CustomerId").val(),
        'OrganizationName': $("#OrganizationName").val(),
        'Category': $("#lstCategory").val(),
        'Website': $("#Website").val(),
        //'CustomerPhone': $("#CustomerPhone").val(),
        'CustomerPhone': $("#AlternatePhone").val() == null ? "" : $("#AlternatePhone").val(),//Commenting above as per discussion on 3rd Mar 2017
        'AlternatePhone': $("#AlternatePhone").val(),
        'Address': $("#Address").val(),
        'Address2': $("#Address2").val(),
        'Country': $("#ddlCountry").val(),
        'State': $("#lstAddCustState").val(),
        'City': $("#lstAddCustCity").val(),
        'ZIPCode': $("#ZIPCode").val(),
        'ContactFirstName': $("#ContactFirstName").val(),
        'ContactLastName': $("#ContactLastName").val(),
        'ContactEmail': $("#ContactEmail").val(),
        'ContactPhone': $("#ContactPhone").val(),
        'BillingAddress': $("#BillingAddress").val(),
        'BillingAddress2': $("#BillingAddress2").val(),
        'BillingCountry': $("#ddlBillingCountry").val(),
        'BillingState': $("#lstAddCustBillingState").val(),
        'BillingCity': $("#lstAddCustBillingCity").val(),
        'BillingZIPCode': $("#BillingZIPCode").val(),
        //todo: below fields
        'FreeTrialStartDate': $("#FreeTrialStartDate").val(),
        'FreeTrialEndDate': $("#FreeTrialEndDate").val(),
        'ApplicationServerId': $("#lstServer").val(),
        'DatabaseServerId': $("#lstDatabase").val(),
        'SameAsAbove': $("#billingchk").is(":checked")
        //IsDeleted = false,
        //IsActive = truemodel.Website,
        //todo: below fields
        //IsDeleted = false,
        //IsActive = true
    };
    $.ajax({
        url: '../Customer/UpdateCustomerID',
        type: "POST",
        data: JSON.stringify({ 'CustomerModel': customerModel }),
        contentType: 'application/json',
        dataType: "json",
        async: true,
        success: function (result) {
            if (result.Status === 1) {
                $("#CustomerId").val(result.Message.split("|")[1]);
                $("#hdCustomerID").val(result.Message.split("|")[1]);
                alert(result.Message.split("|")[0]);
                LoadContactsForcustomer();
                //window.location.reload();
                //$.ajax({
                //    url: '../Customer/GetContactsForCustomer',
                //    type: "POST",
                //    data: JSON.stringify({ 'customerID': $("#hdCustomerID").val() }),
                //    contentType: 'application/json',
                //    dataType: "json",
                //    success: function (result) {
                //        $("#customerContactGrid").data("kendoGrid").dataSource.data(result);
                //    },
                //    error: function (req, status, errorObj) {
                //        alert('Error: Please try again');
                //    }
                //});
                //$.ajax({
                //    url: '../Customer/GetPricingHistoryForCustomer',
                //    type: "POST",
                //    data: JSON.stringify({ 'customerID': $("#hdCustomerID").val() }),
                //    contentType: 'application/json',
                //    dataType: "json",
                //    success: function (result) {
                //        $("#pricingHistoryGrid").data("kendoGrid").dataSource.data(result);
                //    },
                //    error: function (req, status, errorObj) {
                //        alert('Error: Please try again');
                //    }
                //});
            }
            else {
                alert(result.Message.split("|")[0]);
            }
        },
        error: function (req, status, errorObj) {
            alert('Error: Please try again');
        }
    });
    return true;
}

//response after add customer
function addCustomerResult(resp) {
    if (resp.Status === 1) {
        alert(resp.Message);
        customerdataSource.read();
        ShowCustomerGrid();
        //todo: scroll window to top
    }
    else {
        alert(resp.Message);
    }
}

//page divs visibility functions
function ShowCustomerGrid() {
    $("#btnShowCustomerDashboard").hide();
    $("#customergridcontainer").show();
    $("#AddCustomer").hide();
    $("#ContactDetails").hide();
    $('#CustTitle').text('TKG Customer Snapshot');

}

function ShowActionsDiv(obj, event) {
    event.stopPropagation();
    var leftP = $(obj).offset();
    var l = leftP.left;
    var t = leftP.top;

    if (navigator.userAgent.match(/Trident/i)) {
        l = leftP.left;
        if ($(obj)[0].innerHTML.indexOf('caret') != -1) {
            l = leftP.left - 82;
        }
    }
    else if (navigator.userAgent.match(/AppleWebKit/i)) {
        l = leftP.left - 250;
        if ($(obj)[0].innerHTML.indexOf('caret') != -1) {
            l = leftP.left - 332;
        }
    }
    else if (navigator.userAgent.match(/Firefox/i)) {
        l = leftP.left - 250;
        if ($(obj)[0].innerHTML.indexOf('caret') != -1) {
            l = leftP.left - 332;
        }
    }
    else {
    }

    $($(obj)[0].parentNode).find('.custom-dropdown').css('left', l);
    $($(obj)[0].parentNode).find('.custom-dropdown').css('top', t)
    $($(obj)[0].parentNode).find('.custom-dropdown').toggleClass('show-div');
}

function CustomDropdownClick(event) {
    event.stopPropagation();
}
$(document).click(function () {
    $('.custom-dropdown').removeClass('show-div');
});

$(document).mousedown(function (event) {
    // var s = event;
    //$('.custom-dropdown').removeClass('show-div');
});

$(window).resize(function () {
    $('.custom-dropdown').removeClass('show-div');
});

$(document.body).scroll(function () {

    $('.custom-dropdown').removeClass('show-div');
});

function CalculateFreeTrialDate(start) {
    if ($("#FreeTrialEndDate").val() !== "" && $("#FreeTrialStartDate").val() !== "") {
        var freeTrailStartDate = new Date($("#FreeTrialStartDate").val());
        var freeTrailEndDate = new Date($("#FreeTrialEndDate").val());
        var diff = new Date(freeTrailEndDate - freeTrailStartDate);
        var diffDays = diff / 1000 / 60 / 60 / 24;
        if (freeTrailEndDate > freeTrailStartDate) {
            $("#lblFreeTrialDays").text(diffDays);
        }
        else {
            alert("Start date cannot be later than End date");
            if (start) {
                $("#FreeTrialStartDate").val('');
            } else {
                $("#FreeTrialEndDate").val('');
            }
        }
    }
}
function CheckFreeTrialDate(freeTrialEndDate) {
    var inTrialIndicator = '';
    if (freeTrialEndDate == null)
        return inTrialIndicator;
    FreeTrialEndDate = Date.parse(freeTrialEndDate);
    if (Date.parse(freeTrialEndDate) - Date.parse(new Date()) < 0) {
        inTrialIndicator = '';
    }
    else {
        inTrialIndicator = '<i class="fa fa-circle" aria-hidden="true" style="color:green;"></i>';
    }
    return inTrialIndicator;
}
function initTrialDateControls(result) {
    var freeTrialStartDate = (result.FreeTrialStartDate != null && result.FreeTrialStartDate != undefined) ? new Date(parseInt(result.FreeTrialStartDate.substr(6))) : null;
    if (freeTrialStartDate != null) {
        $("#FreeTrialStartDate").val(('0' + (freeTrialStartDate.getMonth() + 1)).slice(-2) + '/' + ('0' + freeTrialStartDate.getDate()).slice(-2) + '/' + freeTrialStartDate.getFullYear());
    }

    var freeTrialEndDate = (result.FreeTrialEndDate != null && result.FreeTrialEndDate != undefined) ? new Date(parseInt(result.FreeTrialEndDate.substr(6))) : null;
    if (freeTrialEndDate != null) {
        $("#FreeTrialEndDate").val(('0' + (freeTrialEndDate.getMonth() + 1)).slice(-2) + '/' + ('0' + freeTrialEndDate.getDate()).slice(-2) + '/' + freeTrialEndDate.getFullYear());
    }
    var minDateStart = new Date();
    minDateStart.setDate(minDateStart.getDate() - 1);
    $('#custStatrtDatepickerFT').datetimepicker({
        pickTime: false,
        format: 'L',
        minDate: minDateStart
    }).on('change', function (ev) {
        CalculateFreeTrialDate(true);
        var newMinDate = new Date($("#FreeTrialStartDate").val());
        $("#custStatrtDatepickerET").datetimepicker('setStartDate', newMinDate);
    });
    $('#custStatrtDatepickerET').datetimepicker({
        pickTime: false,
        format: 'L',
        minDate: minDateStart
    }).on('change', function (ev) {
        CalculateFreeTrialDate(false);
    });

    $('.datetimepicker2').datetimepicker({
        pickTime: false,
        format: 'L',
    });
}