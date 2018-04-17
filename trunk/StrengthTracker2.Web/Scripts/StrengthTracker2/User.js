var permissionRowCount = 0;
var userdataSource = new kendo.data.DataSource({
    transport: {
        read: function (options) {
            $.ajax({
                type: "GET",
                url: "../Admin/GetTKGAdminUserList",
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                success: function (result) {
                    options.success(result.Data);
                }
            });
        }
    },
    pageSize: 10
});
var locationRoleUserList;
$("#usersGrid").kendoGrid({
    allowCopy: true,
    scrollable: true,
    sortable: true,
    filterable: true,
    columns: [
        {
            field: "IsActive",
            title: "Status",
			filterable: false,
            width: "3.5%",
            template: '#if (IsActive==true) {# <i class="fa fa-circle" aria-hidden="true" style="color:green;vertical-align: text-top;"></i> #} else {# <i class="fa fa-circle" aria-hidden="true" style="color:red;vertical-align: text-top;"></i> #}#',
            attributes: {
                style: 'white-space: nowrap;text-align:center;'
            }
        },
        {
            field: "FullName",
            title: "Name",
            width: "15%",
            template: ' <img src="#=(ProfilePicture) == null ? "../Images/noimage.jpg" : ProfilePicture #"  onclick="GetUserDetails(#=UserId#);" alt="Image" width=45 height=45 style="cursor: pointer;" class="img-thumbnail img-circle"> &nbsp;<a href="javascript:void(0)" onclick="GetUserDetails(#=UserId#);">#=FullName#</a>',
            attributes: {
                style: 'white-space: nowrap '
            }
        },
        {
            field: "UserName",
            title: "User Name",
			width: "15%"
        },
        {
            field: "Email",
            width: "16%",
            attributes: {
                style: 'white-space: nowrap '
            },
            template: '<a href="mailto:#=Email#" target="_top">#=Email#</a>'
        },
        {
            field: "RoleName",
            attributes: {
                style: 'white-space: nowrap '
            },
            width: "12%",
            title: 'Role'
        },
        //{
        //    field: "HasUserDataVisibility",
        //    title: "Has Data Visibility",
        //    attributes: {
        //        style: 'white-space: normal '
        //    },
        //    width: "7%",
        //},
        {
            field: "Actions",
            title: "Actions",
            width: "10%",
            filterable: false,
            template: '<div class="btn-group mb-sm">' +
						  '<button type="button" data-toggle="dropdown" class="btn btn-info dropdown-toggle">Select…</button>' +
						  '<button type="button" data-toggle="dropdown" class="btn btn-info dropdown-toggle">' +
						   '<span class="caret"></span></button>' +
						  '<ul role="menu" class="dropdown-menu" style="min-width:102px !important;">' +
						      '#if(IsActive == true && window.UserPermissions && window.UserPermissions.CanEditUser){#' +
						      '<li><a href="javascript:void(0);" onclick="ShowActivationConfirmationPopup(#=UserId#);">Deactivate</a></li>' +
                              '#}else{#' +
                                '#if(window.UserPermissions && window.UserPermissions.CanEditUser){#' +
                                    '<li><a href="javascript:void(0);" onclick="ShowActivationConfirmationPopup(#=UserId#);">Activate </a></li>' +
                                '#}#' +
                              '#}#' +
                              '#if(window.UserPermissions && window.UserPermissions.CanDeleteUser){#' +
						        '<li><a href="javascript:void(0);" onclick="ShowDeleteConfirmationPopup(#=UserId#);">Delete</a></li>' +
                              '#}#' +
						  '</ul>' +
					   '</div>'
        }
    ],
    dataSource: userdataSource
});
$("#userPager").kendoPager({
    autoBind: true,
    dataSource: userdataSource
});
$("#usersGrid").kendoTooltip({
    filter: "td:nth-child(2)", //this filter selects the second column's cells
    position: "right",
    content: function (e) {
        var dataItem = $("#usersGrid").data("kendoGrid").dataItem(e.target.closest("tr"));
        var content = dataItem.UserName;
        return content;
    }
}).data("kendoTooltip");

$("#btnSaveLocationRole").on('click', SaveLocationRole);
$("#btnAddRole").on('click', SaveLocationRole);
$("#lstLocation").change(function () {
    UpdateUserRoleForLocation();
});
function ShowDeleteConfirmationPopup(id) {
    ConfirmationMessageBox({
        confirmationMessage: 'Are you sure you want to Delete User?',
        popupTitle: 'Delete User',
        callback: function () {
            DeleteUser(id);
            userdataSource.read();
        }
    });
}
function ShowActivationConfirmationPopup(userId) {
    ConfirmationMessageBox({
        confirmationMessage: 'Are you sure you want to Update User status?',
        popupTitle: 'Confirm User Updation',
        callback: function () {
            UpdateUserStatus(userId);
        }
    });
}
function ShowDeleteLocationConfirmationPopup() {
    $('#confirmDeletionLocation').modal('show');
}
function ShowAddUser() {
    $("#usergridcontainer").hide();
    $("#EditUsers").hide();
    $("#AddUsers").show();
    $("#userPicture").show();
    $("#locationAddEdit").hide();
    $("#locationsGrid").hide();
    $("#UserlocationRoleAdd").hide();
    $("#userIconDiv").show();

}
function ShowEditUser(userId) {
    $("#usergridcontainer").hide();
    $("#EditUsers").show();
    $("#AddUsers").hide();
    $("#userPicture").show();

    //$("#locationAddEdit").show();
    $("#locationsGrid").show();
    $("#UserlocationRoleAdd").show();

}
function ShowAddEditLocation() {
    $('input:checkbox[name="RoleName"]').removeAttr('checked');
    $("#usergridcontainer").hide();
    $("#EditUsers").hide();
    $("#AddUsers").hide(); //show()
    $("#locationAddEdit").hide(); //show()
    $("#userPicture").show(); //hide()
}
function ShowUserGrid() {
    $("#usersGrid").show();
    $("#userPager").show();
    $("#usergridcontainer").show();
    $("#EditUsers").hide();
    $("#AddUsers").hide();
    $("#userPicture").hide();
    $("#locationAddEdit").hide();
    $("#userIconDiv").hide();

}
function DeleteUser(id) {
    $.ajax({
        url: "../Admin/DeleteUser",
        type: "POST",
        async: false,
        data: { id: id },
        success: function (resp) {
            if (resp.Status === 1) {
                alert(resp.Message);
                userdataSource.read();
            }
            else {
                alert(resp.Message);
            }

        }, error: function () {
            alert(resp.Message);
        }
    });
}
function UpdateUserStatus(id) {
    $.ajax({
        url: "../Admin/UpdateUserStatusById",
        type: "POST",
        async: false,
        data: { userId: id },
        success: function (result) {

            if (result.Message) {
                alert(result.Message);
                userdataSource.read();
            }
            else {
                alert(result.Message);
            }

        }, error: function () {
            alert(result.Message);
        }
    });
}
function DeleteCustomerLocatonRoles() {
    var mappingId = $("#confirmDeletionLocation").data("mappingId");
    var id = mappingId;
    deleteUserLocation(mappingId);

}
function ShowAddUserLocationRole() {
    //$("#locationAddEdit").show();
    $("#divLocationRole").show();
}
function ToggleCoachControlEdit() {
    if (document.getElementById('chkCoachDetailsEdit').checked == true) {
        $("#divCoachRoleDetailsEdit").fadeIn().css("display", "block");
    }
    else {
        $("#divCoachRoleDetailsEdit").hide();
    }
}
function ToggleCoachControlAdd() {
    if (document.getElementById('chkCoachDetailsAdd').checked == true) {
        $("#divCoachRoleDetailsAdd").fadeIn().css("display", "block");
    }
    else {
        $("#divCoachRoleDetailsAdd").hide();
    }
}
function ToggleSelectionEdit(chk) {
    var $cb = $(chk);
    var checked = $cb.is(':checked');
    var grid = $('#coachAthleteGridEdit').data('kendoGrid');
    grid.table.find("tr").find("td:last input").attr("checked", checked);

    //now making all the rows value to 1 or 0
    var items = $("#coachAthleteGridEdit").data("kendoGrid").dataSource.data();
    for (i = 0; i < items.length; i++) {
        var item = items[i];
        item.set('Selected', checked);
    }
}
function ToggleSelectionAdd(chk) {
    var $cb = $(chk);
    var checked = $cb.is(':checked');
    var grid = $('#coachAthleteGridAdd').data('kendoGrid');
    grid.table.find("tr").find("td:last input").attr("checked", checked);

    //now making all the rows value to 1 or 0
    var items = $("#coachAthleteGridAdd").data("kendoGrid").dataSource.data();
    for (i = 0; i < items.length; i++) {
        var item = items[i];
        item.set('Selected', checked);
    }
}
function SaveUser(userModel) {
    // Checking whether FormData is available in browser
    if (window.FormData !== undefined) {

        var fileUpload = $("input[id='my_file']").get(0);
        var files = fileUpload.files;

        // Create FormData object
        var fileData = new FormData();

        // Looping over all files and add it to FormData object
        for (var i = 0; i < files.length; i++) {
            fileData.append(files[i].name, files[i]);
        }

        // Adding one more key to FormData object
        fileData.append('userViewModel', JSON.stringify(userModel));

        $.ajax({
            url: "../Admin/SaveUser",
            type: "POST",
            data: fileData,
            contentType: false, // Not to set any content header
            processData: false, // Not to process data
            dataType: "json",
            success: function (result) {
                if (result.Status == 1) {
                    alert(result.Message);
                    userdataSource.read();
                    //window.location.reload();
                }
                else {
                    alert(result.Message);
                    //window.location.reload();
                }

            }, error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert(errorThrown.Message);
            }
        });
    }
    else {
        alert("FormData is not supported.");
    }
}
function SaveAddUser() {
    if ($('#hdUserID').val() === "") {
        $('#hdUserID').val("0");
    }
    if ($('#hdContactID').val() === "") {
        $('#hdContactID').val("0");
    }
    if ($("#txtAddUserPassword").val() !== $("#txtAddUserConfirmPassword").val()) {
        $("#dvAddAdminUserMsg").html("Password needs to be same as confirm password");
        $("#dvAddAdminUserMsg").addClass("errorMsg");
        $("#dvAddAdminUserMsg").css("margin-left", "300px");
        $("#dvAddAdminUserMsg").css("margin-bottom", "15px");
        $("#dvAddAdminUserMsg").show();
        $("#dvAddAdminUserMsg").fadeOut(4500);
        $("#txtAddUserPassword").focus();
        return false;
    }
    var formIsValid = true;

    if ($("#txtAddAddress1").val() === "") {
        $("#txtAddAddress1").focus();
        formIsValid = false;
        return false;
    }
    if (formIsValid) {
        var fileUpload = $("input[id='my_file']").get(0);
        var fileName = '';
        if (fileUpload.files.length > 0) {
            fileName = fileUpload.files[0].name;
        }

        var SportIDs = '';
        $("input[name='SportName']").each(function () {
            if ($(this).prop("checked") == true) {
                SportIDs = SportIDs + $(this).val() + ",";
            }
        });
        var TeamIDs = '';
        $("input[name='TeamName']").each(function () {
            if ($(this).prop("checked") == true) {
                TeamIDs = TeamIDs + $(this).val() + ",";
            }
        });

        var addUserInfo = {
            'UserID': $('#hdUserID').val(),
            'UserName': $("#txtAddUserName").val(),
            'FirstName': $("#txtAddUserFirstName").val(),
            'LastName': $("#txtAddUserLastName").val(),
            'Gender': $("#lstGender").val(),
            'DOB': $("#txtAddDOB").val(),
            'Password': $("#txtAddUserPassword").val(),
            'Email': $("#txtAddUserEmail").val(),
            'IsAthleticDirector': $("#chkAthleticDirector").prop("checked"),
            'IsStrengthCoach': $("#chkStrengthCoach").prop("checked"),
            'UserType': 1,
            'CoachType': $("#lstCoachOptions").val(),
            'SportIDs': SportIDs,
            'TeamIDs': TeamIDs,
            'ContactInformation': {
                'Email': $("#txtAddUserEmail").val(),
                'AddressOne': $("#txtAddAddress1").val(),
                'AddressTwo': $("#txtAddAddress2").val(),
                'City': $("#txtAddUserCity").val(),
                'Zip': $("#txtAddUserZip").val(),
                'StateID': $("#ddlState").val(),
                'Country': $("#ddlCountry").val()
            },
            'UserImage': {
                'ImagePath': fileName
            }
        };
        SaveUser(addUserInfo);
    }
}
function SaveLocationRole(){
    var locationId = $("#lstLocation").val();
    var role = GetSelectedRoles();
    var userId = $("#hdUserID").val();
    $.ajax({
        url: "../Admin/SaveLocationRole",
        type: "POST",
        data: { locationID: locationId, UserId: userId, role: role },
        dataType: "json",
        success: function (result) {
            if (result != null) {
                alert(result.Message);
                BindUserLocationRole(userId);
                $("#btnSaveLocationRole").on('click', SaveLocationRole);
            }
        },
        error: function (req, status, errorObj) {
            alert("Error: Please try again later");
        }
    });
};
function BindUserLocationRole(id) {
    $.ajax({
        url: "../Admin/GetLocationRoleUserList",
        type: "POST",
        data: JSON.stringify({ 'id': id }),
        contentType: 'application/json',
        dataType: "json",
        success: function (result) {
            locationRoleUserList = result;
            UpdateUserRoleForLocation();
            initAutoSuggestCombobox();
        },
        error: function (req, status, errorObj) {
            alert('Error: Please try again');
        }
    });
}
function GetUserDetails(id) {
    $.ajax({
        url: "../Admin/GetUserInfoByID",
        type: "POST",
        data: { 'userId': id },
        dataType: "json",
        success: function (result) {
            if (result != null) {
                $("#usersGrid").hide();
                $("#userPager").hide();
                $("#usergridcontainer").hide();
                $("#EditUsers").hide();
                $("#AddUsers").show();
                $("#userPicture").show();
                //Changed from .hide to .show
                //$("#locationAddEdit").show();
                $("#locationsGrid").show();
                $("#userIconDiv").show();
                $("#UserlocationRoleAdd").show();
                $("#spnUser").text("Edit User");
                $('#hdUserID').val(result.UserId);
                $("#txtAddUserName").val(result.UserName);
                $("#txtAddUserFirstName").val(result.FirstName);
                $("#txtAddUserLastName").val(result.LastName);
                $("#lstGender").val(result.Gender);
                $("#txtAddDOB").val(result.DOB);
                $("#ddlState").val(result.ContactInformation.StateID);
                $("#ddlCountry").val(result.ContactInformation.Country);
                $("#txtAddUserPassword").val(result.Password),
                $("#txtAddUserConfirmPassword").val(result.Password);
                $("#lstCoachOptions").val(result.CoachType);
                CoachOptionChange();
                if (result.CoachType == 2 || result.CoachType == 3) {
                    if (result.SportIDs != null && result.SportIDs !== "") {
                        //$("input[name='SportName']").each(function () {
                        //});
                        $.each(result.SportIDs.split(","), function (intIndex, objValue) {
                            $("#chkSportID" + objValue).attr("checked", "true");
                        });
                    }
                    if (result.TeamIDs != null && result.TeamIDs !== "") {
                        //$("input[name='TeamName']").each(function () {

                        //});
                        $.each(result.TeamIDs.split(","), function (intIndex, objValue) {
                            $("#chkTeamID" + objValue).attr("checked", "true");
                        });
                    }
                }
                //else if (result.CoachType == 3) {
                //    if (result.TeamIDs != null && result.TeamIDs !== "") {
                //        //$("input[name='TeamName']").each(function () {
                //        //});
                //        $.each(result.TeamIDs.split(","), function (intIndex, objValue) {
                //            $("#chkTeamID" + objValue).attr("checked", "true");
                //        });
                //    }
                //}
                if (result.IsAthleticDirector == true) {
                    $("#chkAthleticDirector").prop("checked", true);
                }
                if (result.IsStrengthCoach == true) {
                    $("#chkStrengthCoach").prop("checked", true);
                }
                if (result.ContactInformation != null) {
                    $('#hdContactID').val(result.ContactInformation.ContactID),
                    $("#txtAddUserEmail").val(result.ContactInformation.Email);
                    $("#txtAddAddress1").val(result.ContactInformation.AddressOne);
                    $("#txtAddAddress2").val(result.ContactInformation.AddressTwo);
                    $("#txtAddUserCity").val(result.ContactInformation.City);
                    $("#txtAddUserZip").val(result.ContactInformation.Zip);
                }
                if (result.UserImage != null) {
                    //$("#profilePicture").attr("src", (result.UserImage.ImagePath != null && result.UserImage.ImagePath.length > 1) ? result.UserImage.ImagePath : "../images/noimage.jpg");
                    $("#profilePicture").attr("src", (result.ProfilePicture != null && result.ProfilePicture.length > 1) ? result.ProfilePicture : "../images/noimage.jpg");
                }
                // added for userlocation role grid by srinivas
                BindUserLocationRole($('#hdUserID').val());
                var qryPV = getUrlVars()["pv"];
                if (window.UserPermissions && !window.UserPermissions.CanEditUser) {
                    $("#AddUsers input").prop("disabled", true);
                    $("#AddUsers select").prop("disabled", true);
                    $("#AddUsers textarea").prop("disabled", true);
                }
                if (qryPV === "true") {
                    $("#AddUsers input").prop("disabled", false);
                    $("#AddUsers select").prop("disabled", false);
                    $("#AddUsers textarea").prop("disabled", false);
                    $("#btnSaveAddUser").show();
                }
                InitDataVisibility(result.UserId);

                $("#userGridLegend").hide();
            }
        },
        error: function (req, status, errorObj) {
            alert("Error: Please try again later");
        }
    });
}
function GetSelectedRoles(){
    var checboxs = $("input[name=RoleName]");
    var selectedRoles = [];
    selectedRoles.push($("#lstRole").val());
    //for(var i=0;i<checboxs.length;i++){
    //    var checkbox = checboxs[i];
    //    if (checkbox.checked) {
    //        selectedRoles.push(checkbox.value);
    //    }
    //}
    if(selectedRoles.length > 0){
        return selectedRoles.join(';');
    }
    return "";
}
function UpdateUserRoleForLocation() {
    var locationId = $("#lstLocation").val();
    var checboxs = $("input[name=RoleName]");
    var currectLocationRoles;
    for (var i = 0; i < locationRoleUserList.length; i++) {
        var locationRoles = locationRoleUserList[i];
        if (locationRoles.LocationId == locationId) {
            currectLocationRoles = locationRoles;
            break;
        }
    }
    if (currectLocationRoles) {
        $("#lstRole").val(currectLocationRoles.RoleId);
        var roles = currectLocationRoles.RoleId;
        for (var c = 0; c < checboxs.length; c++) {
            var checkbox = checboxs[c];
            if (roles.indexOf(checkbox.value) != -1) {
                checkbox.checked = true;
            }
            else{
                checkbox.checked = false;
            }
        }
    }
    else {
        for (var c = 0; c < checboxs.length; c++) {
            var checkbox = checboxs[c];
            checkbox.checked = false;
        }
    }
}
function CoachOptionChange() {
    if ($("#lstCoachOptions").val() === "0") {//Other
        $("#dvTeamList").hide();
        $("#dvSportList").hide();
        $("#tblVisibility").hide();
        $("#tblSportCoachVisibility").hide();
        $("#tblTeamCoachVisibility").hide();
        //$("#btnAddResponsibility").hide();
        $("#chkAthleticDirector").prop("disabled", "");
        $("#chkStrengthCoach").prop("disabled", "");
    }
    else if ($("#lstCoachOptions").val() === "2") {//Sport Coach
        //$("#chkAthleticDirector").prop("checked", false);
        //$("#chkStrengthCoach").prop("checked", false);
        //$("#chkAthleticDirector").prop("disabled", "disabled");
        //$("#chkStrengthCoach").prop("disabled", "disabled");
        $("#dvTeamList").hide();
        $("#dvSportList").show();
        $("#tblVisibility").hide();
        $("#tblSportCoachVisibility").show();
        $("#tblTeamCoachVisibility").hide();
        $("#btnAddResponsibility").show();
    }
    else if ($("#lstCoachOptions").val() === "3") {//Team Coach
        //$("#chkAthleticDirector").prop("checked", false);
        //$("#chkStrengthCoach").prop("checked", false);
        //$("#chkAthleticDirector").prop("disabled", "disabled");
        //$("#chkStrengthCoach").prop("disabled", "disabled");
        $("#dvTeamList").show();
        $("#dvSportList").show();
        //$("#tblVisibility").show();
        $("#tblVisibility").hide();
        $("#tblSportCoachVisibility").hide();
        $("#tblTeamCoachVisibility").show();
        $("#btnAddResponsibility").show();
    }
    else {//Coach
        //$("#chkAthleticDirector").prop("checked", false);
        //$("#chkStrengthCoach").prop("checked", false);
        //$("#chkAthleticDirector").prop("disabled", "disabled");
        //$("#chkStrengthCoach").prop("disabled", "disabled");
        $("#dvTeamList").hide();
        $("#dvSportList").hide();
        $("#tblVisibility").hide();
        $("#tblSportCoachVisibility").hide();
        $("#tblTeamCoachVisibility").hide();
        //$("#btnAddResponsibility").hide();
    }
}
function ToggleActiveStatus(userId, input) {
    ConfirmationMessageBox({
        confirmationMessage: 'Are you sure you want to Update User status?',
        popupTitle: 'Confirm User Updation',
        callback: function () {
            UpdateUserStatus(userId);
        },
        callbackCancel: function (input) {
            input.checked = !input.checked;
        },
        callbackCancelParam: input
    });
}
function AddPermissionsToUser() {
    //$("#tblVisibility tr:last").after('<tr><td style="text-align: center;">' + $("#lstCoachOptions option:selected").text() + '</td><td style="text-align: center;">' + $("#lstSport option:selected").text() + '</td><td style="text-align: center;">' + $("#lstTeam option:selected").text() + '</td></tr>');
    if ($("#lstCoachOptions").val() === "2") {//Sport Coach
        $("#tblVisibility tr:last").after('<tr id="tr' + permissionRowCount + '"><td style="text-align: center;">' + $("#lstCoachOptions option:selected").text() + '</td><td style="text-align: center;">' + $("#lstSport option:selected").text() + '</td><td style="text-align: center;">All Team</td><td><a href="JavaScript:void(0);" onclick="ShowDeleteConfirmation(\'' + permissionRowCount + '\');"><span class="icon-delete" title="Delete" style="font-size:24px;"></span></td></a></tr>');
        permissionRowCount = permissionRowCount + 1;
    }
    else if ($("#lstCoachOptions").val() === "3") {//Team Coach
        $("#tblVisibility tr:last").after('<tr id="tr' + permissionRowCount + '"><td style="text-align: center;">' + $("#lstCoachOptions option:selected").text() + '</td><td style="text-align: center;">' + $("#lstSport option:selected").text() + '</td><td style="text-align: center;">' + $("#lstTeam option:selected").text() + '</td><td><a href="JavaScript:void(0);" onclick="ShowDeleteConfirmation(\'' + permissionRowCount + '\');"><span class="icon-delete" title="Delete" style="font-size:24px;"></span></td></a></tr>');
        permissionRowCount = permissionRowCount + 1;
    }
    
}


function ShowADToolTipModal() {
    $('#ADTooltipModal').modal('show');
}
function ShowSCToolTipModal() {
    $('#SCTooltipModal').modal('show');
}