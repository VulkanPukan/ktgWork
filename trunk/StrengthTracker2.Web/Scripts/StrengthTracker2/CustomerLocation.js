$(function () {
    $("#liLocation").addClass("active");
    $("#txtLocationPh").mask("(999) 999-9999");
    $("#txtLocationCtcPh").mask("(999) 999-9999");
    $("#txtLocationCtcPh1").mask("(999) 999-9999");
});

var LocationdataSource = new kendo.data.DataSource({
    data: [],
    pageSize: 10
});

$("#liLocations").addClass("active");

$("#LocationsGrid").kendoGrid({
    allowCopy: true,
    width: '800px',
    scrollable: false,
    sortable: true,
    filterable: true,
    resizable: true,
    columns: [
        {
            field: "LocationName",
            title: "Location",
            width: "150px",
            locked: true,
            lockable: true,
            template: '<a href="javascript:void(0)" onclick="GetLocationInfo(#=CustomerId#, #=LocationID#);">#=LocationName#</a>'
        },
        {
            field: "Address",
            title: "Address",
            locked: true,
            lockable: true,
            attributes: {
                style: 'white-space: nowrap '
            }
        },
        {
            field: "City",
            title: "City",
            locked: true,
            lockable: true,
            attributes: {
                style: 'white-space: nowrap '
            }
        },
        {
            field: "State",
            title: "State",
            locked: true,
            lockable: true,
            attributes: {
                style: 'white-space: nowrap '
            }
        },
        {
            field: "State",
            title: "State",
            locked: true,
            lockable: true,
            attributes: {
                style: 'white-space: nowrap '
            }
        },
        {
            field: "MaxAthletes",
            title: "Max Athletes",
            locked: true,
            lockable: true,
            attributes: {
                style: 'white-space: nowrap '
            }
        },
        {
            field: "ActiveAthlete",
            title: "Active Athlete",
            width: "100px",
            attributes: {
                style: 'white-space: nowrap '
            }
        },
        {
            field: "Status",
            title: "Status",
            width: "85px",
            attributes: {
                style: 'white-space: nowrap '
            }
        },
        {
            field: "Actions",
            title: "Actions",
            filterable: false,
            width: "140px",
            template: '<div class="btn-group mb-sm">' +
                   '<button type="button" data-toggle="dropdown" class="btn btn-info dropdown-toggle">Select…</button>' +
                   '<button type="button" data-toggle="dropdown" class="btn btn-info dropdown-toggle">' +
                   '<span class="caret"></span></button>' +
                   '<ul role="menu" class="dropdown-menu" style="min-width:110px !important;">' +
                      '<li><a href="javascript:void(0);" onclick="ShowActivationConfirmationPopup(#=FreeTrailNoOfDays#);">Activate</a></li>' +
                      '<li><a href="javascript:void(0);" onclick="ShowDeleteConfirmationPopup(#=LocationID#);">Delete</a></li>' +
                   '</ul>' +
                   '</div>'
        }

    ],
    dataSource: LocationdataSource
});

$("#LocationPager").kendoPager({
    autoBind: true,
    dataSource: LocationdataSource
});

var contactdataSource = new kendo.data.DataSource({
    data: [],
    pageSize: 10
});

$("#LocationsGrid").kendoTooltip({
    filter: "th",
    content: function (e) {
        var target = e.target; // element for which the tooltip is shown
        return $(target).data('title')
    }
}).data("kendoTooltip");

function ShowDeleteConfirmationPopup(id) {
    ConfirmationMessageBox({
        confirmationMessage: 'Are you sure you want to delete Location?',
        popupTitle: 'Delete Location',
        callback: function () {
            deleteLocation(id);
        }
    });
}

function ShowActivationConfirmationPopup(FreeNoOfDays) {
    $('#confirmActivation').modal('show');
}

function deleteLocation(id) {
    $.ajax({
        //url: "../Admin/DeleteCustomerLocation",
        url: window.DeleteCustomerLocationUrl,
        type: "POST",
        async: false,
        data: { id: id },
        success: function (resp) {
            if (resp.Status === 1) {
                alert(resp.Message);
                FilterLocationByCustomer();
            }
            else {
                alert(resp.Message);
            }

        }, error: function () {
            alert(resp.Message);
        }
    });
}

function ShowAddLocation() {
    $("#Locationgridcontainer").hide();
    $("#AddLocation").show();
    $("#ContactDetails").hide();
}
$("a[href='#aTop']").click(function () {
    alert('aa');
    $("html, body").animate({ scrollTop: 0 }, "slow");
    return false;
});
function ShowContactDetails() {
    $("#Locationgridcontainer").hide();
    $("#AddLocation").hide();
    $("#ContactDetails").show();
    $("#aTop").click();
}
function ShowActivationConfirmationPopup(FreeNoOfDays) {
    if (FreeNoOfDays > 0) {
        $("#ftrialDiv").show();
        $("#nftrialDiv").hide();
    }
    else {
        $("#ftrialDiv").hide();
        $("#nftrialDiv").show();
    }
    $('#confirmActivation').modal('show');
}

$("#locationContactGrid").kendoGrid({
    allowCopy: true,
    scrollable: true,
    sortable: true,
    filterable: true,
    columns: [
        {
            field: "FirstName",
            title: "Contact",
            template: ' <img src="#=(ImagePath) == null ? "../Images/noimage.jpg" : ImagePath #" alt="Image" width=45 height=45 class="img-thumbnail img-circle" style="cursor: pointer;"> &nbsp;<a style="cursor: pointer;" onclick="EditContactDetails(#=ID#);">#=FirstName# #=LastName#</a>',
        },
        {
            field: "Phone",
            title: "Phone",
            attributes: {
                style: 'white-space: nowrap '
            }
        },
        {
            field: "Email",
            title: "Email",
            attributes: {
                style: 'white-space: nowrap '
            },
            template: '<a href="mailto:#=Email#" target="_top">#=Email#</a>'
        },
        {
            field: "ContactType",
            title: "Contact Type ",
            attributes: {
                style: 'white-space: nowrap '
            }
        }

    ],
    dataSource: contactdataSource
});

contactdataSource.read();
$("#customerContact").kendoPager({
    autoBind: true,
    dataSource: contactdataSource
});

function CopyBillingAddress() {
    if ($("#locbillingchk").is(':checked')) {
        $("#txtLocationBillingAdd").val($("#txtLocationAdd").val());
        $("#lstLocBillingState").val($("#lstLocationState").val());
        $("#txtLocBillingCity").val($("#txtLocationCity").val());
        $("#txtLocationBillingZip").val($("#txtLocationZip").val());
    }
}
$(document).ready(function () {
    FilterLocationByCustomer();
    $('#locbillingchk').click(function () {
        if (!$(this).is(':checked')) {
            $("#locbillingDIV").show();
        }
        else {
            CopyBillingAddress();
            $("#locbillingDIV").hide();
        }
    });
});

document.getElementById("my_file").onchange = function () {

    var reader = new FileReader();

    reader.onload = function (e) {
        // get loaded data and render thumbnail.
        document.getElementById("profilePicture").src = e.target.result;
    };

    if (this.files[0]) {
        // read the image file as a data URL.
        reader.readAsDataURL(this.files[0]);
    }
};
$("#profilePicture").click(function () {
    $("input[id='my_file']").click();
});
function GetLocationInfo(customerID, LocationID) {
    if (customerID > 0) {
        $.ajax({
            //url: '@Url.Action("GetLocationInfo", "Admin")',
            url: window.GetLocationInfoUrl,
            type: "POST",
            data: JSON.stringify({ 'CustomerId': customerID, 'locationID': LocationID }),
            contentType: 'application/json',
            dataType: "json",
            success: function (result) {
                ShowAddLocation();
                $("#lstAddContactCustomer").val(result[0].CustomerId);
                $("#hdCustomerLocationID").val(result[0].LocationID);
                $("#hdCustomerID").val(result[0].CustomerId);
                //$("#txtLocationShortName").val(result[0].LocationShortName);
                $("#txtLocationName").val(result[0].LocationName);
                $("#txtLocationPh").val(result[0].LocationPhone);
                $("#txtLocationAdd").val(result[0].Address);
                $("#lstLocationState").val(result[0].State);
                $("#txtLocationCity").val(result[0].City);
                $("#lstLocBillingState").val(result[0].BillingState);
                $("#txtLocBillingCity").val(result[0].BillingCity);
                $("#txtLocationZip").val(result[0].ZIPCode);
                $("#txtLocationFirstName").val(result[0].ContactFirstName);
                $("#txtLocationLastName").val(result[0].ContactLastName);
                $("#txtLocationCtcEmail").val(result[0].ContactEmail);
                $("#txtLocationCtcPh").val(result[0].ContactPhone);
                $("#txtLocationBillingAdd").val(result[0].BillingAddress);
                $("#txtLocationBillingZip").val(result[0].BillingZIPCode);
                if (result[0].SameAsAbove == true) {
                    $("#locbillingchk").prop('checked', true);
                    $("#locbillingDIV").hide();
                }
                else {
                    $("#locbillingchk").prop('checked', false);
                    $("#locbillingDIV").show();
                }

                $.ajax({
                    //url: '@Url.Action("GetContactsforLocation", "Admin")',
                    url: window.GetContactsforLocationUrl,
                    type: "POST",
                    data: JSON.stringify({ 'locationID': result[0].LocationID }),
                    contentType: 'application/json',
                    dataType: "json",
                    success: function (result) {
                        $("#locationContactGrid").data("kendoGrid").dataSource.data(result);
                    },
                    error: function (req, status, errorObj) {
                        alert('Error: Please try again');
                    }
                });
            },
            error: function (req, status, errorObj) {
                alert('Error: Please try again');
            }
        });
    }
    else {
        ShowAddLocation();
        $("#hdCustomerLocationID").val('');
        $("#lstAddContactCustomer").val($("#lstCustomerFilter").val());
        $("#hdCustomerID").val($("#lstAddContactCustomer").val());
        $("#txtLocationName").val('');
        $("#txtLocationPh").val('');
        $("#txtLocationAdd").val('');
        $("#txtLocationZip").val('');
        $("#txtLocationFirstName").val('');
        $("#txtLocationLastName").val('');
        $("#txtLocationCtcEmail").val('');
        $("#txtLocationCtcPh").val('');
        $("#txtLocationBillingAdd").val('');
        $("#txtLocationBillingZip").val('');
    }
}
function AddLocationInfo() {
    GetLocationInfo(0);
}

function ValidateLocationName() {
    var customerLocationID = $("#hdCustomerLocationID").val() == '' ? 0 : $("#hdCustomerLocationID").val();
    var data = { customerId: $("#lstAddContactCustomer").val(), customerLocationId: customerLocationID, locationName: $("#txtLocationName").val() };
    $.ajax({
        //url: '../Admin/LocationNameExists',
        url: window.LocationNameExistsUrl,
        type: "POST",
        data: data,
        success: function (result) {
            if (result.Status == 2) {
                $($("#txtLocationName")[0].parentElement).addClass("has-error");
                alert(result.Message);
                $("#txtLocationName")[0].focus();
                return false;
            }
            else {
                $($("#txtLocationName")[0].parentElement).removeClass("has-error")
                SaveLocationInfo();
            }
        },
        error: function (req, status, errorObj) {
            alert('Error: Please try again');
        }
    });
}

function SaveLocationInfo() {
    var locationModel = {
        'CustomerId': $("#lstAddContactCustomer").val(),
        'LocationID': $("#hdCustomerLocationID").val(),
        'LocationName': $("#txtLocationName").val(),
        'LocationPhone': $("#txtLocationPh").val(),
        'Address': $("#txtLocationAdd").val(),
        'State': $("#lstLocationState").val(),
        'City': $("#txtLocationCity").val(),
        'ZIPCode': $("#txtLocationZip").val(),
        'ContactFirstName': $("#txtLocationFirstName").val(),
        'ContactLastName': $("#txtLocationLastName").val(),
        'ContactEmail': $("#txtLocationCtcEmail").val(),
        'ContactPhone': $("#txtLocationCtcPh").val(),
        'BillingAddress': $("#txtLocationBillingAdd").val(),
        'BillingState': $("#lstLocBillingState").val(),
        'BillingCity': $("#txtLocBillingCity").val(),
        'BillingZIPCode': $("#txtLocationBillingZip").val(),
        'SameAsAbove': $("#locbillingchk").is(":checked")
    };
    $.ajax({
        //url: '../Admin/SaveLocationInfo',
        url: window.SaveLocationInfoUrl,
        type: "POST",
        data: JSON.stringify({ 'lvm': locationModel }),
        contentType: 'application/json',
        dataType: "json",
        success: function (result) {
            alert(result.Message.split('|')[0]);
            $("#hdCustomerLocationID").val(result.Message.split('|')[1]);
        },
        error: function (req, status, errorObj) {
            alert('Error: Please try again');
            $.ajax({
                //url: '@Url.Action("GetPricingHistoryForCustomer", "Customer")',
                url: window.GetPricingHistoryForCustomerUrl,
                type: "POST",
                data: JSON.stringify({ 'customerID': $("#hdCustomerID").val() }),
                contentType: 'application/json',
                dataType: "json",
                success: function (result) {
                },
                error: function (req, status, errorObj) {
                    alert('Error: Please try again');
                }
            });
        }
    });
}
function ValidateAndSaveLocationInfo() {
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
    ValidateLocationName();
}

function EditContactDetails(locationContactID) {
    $.ajax({
        //url: '@Url.Action("GetLocationContactInfo", "Admin")',
        url: window.GetLocationContactInfoUrl,
        type: "POST",
        data: JSON.stringify({ 'locationContactID': locationContactID }),
        contentType: 'application/json',
        dataType: "json",
        success: function (result) {
            ShowContactDetails();
            $("#hdLocationContactID").val(result.ID);
            $("#txtLocationCtcFirstName").val(result.FirstName);
            $("#txtLocationCtcLastName").val(result.LastName);
            $("#txtLocationCtcPh1").val(result.Phone);
            $("#txtLocationCTCEmail").val(result.Email);
            $("#txtLocationCtcType").val(result.ContactType);
            $("#profilePicture").attr("src", result.ImagePath);
        },
        error: function (req, status, errorObj) {
            alert('Error: Please try again');
        }
    });
}
function SaveLocationContactInfo() {
    if (window.FormData !== undefined) {
        var fileUpload = $("input[id='my_file']").get(0);
        var files = fileUpload.files;

        var fileData = new FormData();

        for (var i = 0; i < files.length; i++) {
            fileData.append(files[i].name, files[i]);
        }
        var locationID = $("#hdCustomerLocationID").val().length > 0 ? $("#hdCustomerLocationID").val() : 0;
        var id = $("#hdLocationContactID").val().length > 0 ? $("#hdLocationContactID").val() : 0;
        var locContact = {
            'LocationID': locationID,
            'ID': id,
            'FirstName': $("#txtLocationCtcFirstName").val(),
            'LastName': $("#txtLocationCtcLastName").val(),
            'Phone': $("#txtLocationCtcPh1").val(),
            'Email': $("#txtLocationCTCEmail").val(),
            'ContactType': $("#txtLocationCtcType").val()
        };

        fileData.append('locContact', JSON.stringify(locContact));

        $.ajax({
            //url: '@Url.Action("SaveLocationContact", "Admin")',
            url: window.SaveLocationContactUrl,
            type: "POST",
            data: fileData,
            contentType: false, // Not to set any content header
            processData: false, // Not to process data
            dataType: "json",
            success: function (result) {
                alert(result.Message);
                $("#txtLocationCtcFirstName").val('');
                $("#txtLocationCtcLastName").val('');
                $("#txtLocationCtcPh1").val('');
                $("#txtLocationCTCEmail").val('');
                $("#txtLocationCtcType").val('');
                $("#profilePicture").attr("src", result.ImagePath);
                var fileInput = $('#my_file');
                fileInput.replaceWith(fileInput.val('').clone(true));

                ShowAddLocation();
                $.ajax({
                    //url: '@Url.Action("GetContactsforLocation", "Admin")',
                    url: window.GetContactsforLocationUrl,
                    type: "POST",
                    data: JSON.stringify({ 'locationID': locationID }),
                    contentType: 'application/json',
                    dataType: "json",
                    success: function (result) {
                        $("#locationContactGrid").data("kendoGrid").dataSource.data(result);
                    },
                    error: function (req, status, errorObj) {
                        alert('Error: Please try again');
                    }
                });
            },
            error: function (req, status, errorObj) {
                alert('Error: Please try again');
            }
        });
    }
    else {
        alert("FormData is not supported.");
    }
}

function FilterLocationByCustomer() {
    $.ajax({
        //url: '@Url.Action("GetLocationListForCustomer", "Admin")',
        url: window.GetLocationListForCustomerUrl,
        type: "POST",
        data: JSON.stringify({ 'customerID': $("#lstCustomerFilter").val() }),
        contentType: 'application/json',
        dataType: "json",
        success: function (result) {
            $("#LocationsGrid").data("kendoGrid").dataSource.data(result);
        },
        error: function (req, status, errorObj) {
            alert('Error: Please try again');
        }
    });
}