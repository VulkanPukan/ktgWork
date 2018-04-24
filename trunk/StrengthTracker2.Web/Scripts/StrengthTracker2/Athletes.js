﻿//This file is used in pages that are related to Athlete 
//bind data to variable
var showTeamandCoachColumn;
function SetColumnVisibility(toShoworNot) {
    showTeamandCoachColumn = toShoworNot;
}
function toDate(value) {
    var dateRegExp = /^\/Date\((.*?)\)\/$/;
    var date = dateRegExp.exec(value);
    return new Date(parseInt(date[1]));
}
var athletedataSource = new kendo.data.DataSource({
    transport: {
        read: function (options) {
            $.ajax({
                type: "GET",
                url: "../Admin/GetAthletes",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    options.success(result.Data);
                    var athleteGrid = $("#athleteGrid").data("kendoGrid");
                    if (showTeamandCoachColumn !== "") {
                        var athleteGrid = $("#athleteGrid").data("kendoGrid");
                        athleteGrid.hideColumn(9);//Team
                        athleteGrid.hideColumn(10);//Coach
                    }
                }
            });
        }
    },
    pageSize: 10
});
$("#athleteGrid").kendoGrid({
    allowCopy: true,
    //height: 550,
    scrollable: true,
    sortable: true,
    filterable: true,

    columns: [
        {
            field: "UserID",
            hidden: true,
            attributes: {
                style: 'white-space: nowrap '
            }
        },
        {
            field: "",
            title: "",
            width: "2%",
            //template: '#=StatusDisplayIcon#',
            template: '<input type="checkbox" class="athleteSelectCheckbox" value="#=UserID#" onclick="SelectUser(#=UserID#, this);" />',
            attributes: {
                style: 'white-space: nowrap;text-align:center;'
            }
        },
        {
            field: "Status",
            title: "Status",
            width: "5%",
            filterable: false,
            sortable: false,
            //template: '#=StatusDisplayIcon#',
            template: '#if (PendingStatus == "Active") {# <i class="fa fa-circle" aria-hidden="true" style="color:green;vertical-align: text-top;"></i> #} else if (PendingStatus == "Deactivated") {# <i class="fa fa-circle" aria-hidden="true" style="color:red;vertical-align: text-top;"></i> #} else if (PendingStatus == "Pending") {# <i class="fa fa-circle" aria-hidden="true" style="color:orange;vertical-align: text-top;"></i> #}#',
            attributes: {
                style: 'white-space:nowrap;text-align:center;font-weight:bold;'
            }
        },
        {
            field: "AthleteName",
            title: "Name",
            width: "13%",
            template: ' <img src="#=(UserImagePath) == null ? "../Images/noimage.jpg" : UserImagePath #" onclick="ShowAthleteProfile(#=UserID#);"  alt="Image" width=45 height=45 class="img-thumbnail img-circle" style="cursor: pointer;"> &nbsp;<a href="javascript:void(0)" onclick="ShowAthleteProfile(#=UserID#);">#=AthleteName#</a>',
            attributes: {
                style: 'white-space: nowrap;'
                //style: 'max-width: 200px;overflow: hidden;text-overflow: ellipsis;'
            }
        },
        {
            field: "Phone",
            width: "8%",
            template: '#= (Phone) ? FormatPhoneNumber(Phone) : "none" #',
            //template: '<div class="tooltip">#= (Phone) ? FormatPhoneNumber(Phone) : "none" # <span class="tooltiptext">#= (Phone) ? FormatPhoneNumber(Phone) : "none" # </span></div>',
            attributes: {
                style: 'white-space: nowrap;text-align:center;'
            }
        },
        {
            field: "Age",
            width: "6%",
            attributes: {
                style: 'white-space: nowrap;text-align: center;'
            }
        },
        //{
        //    field: "School",
        //    attributes: {
        //        style: 'white-space: nowrap '
        //    }
        //},
        {
            field: "Sport",
            width: "7%",
            attributes: {
                style: 'white-space: nowrap '
            }
        },
        {
            field: "Program",
            width: "12%",
            attributes: {
                style: 'white-space: normal '
            }
        },
        {
            title: "Location",
            field: "Location",
            width: "8%",
            attributes: {
                style: 'white-space: nowrap;text-align: center;width: 5%;'
            }
        },
        {
            title: "Team",
            field: "TeamName",
            width: "7%",
            attributes: {
                style: 'white-space: normal '
            }
        },
        {
            title: "Coach",
            field: "CoachName",
            width: "10%",
            attributes: {
                style: 'white-space: normal '
            }
        },
        {
            title: "ActivationDate",
            field: "ActivationDate",
            width: "10%",
            template: '#= kendo.toString(ActivationDate, \"D\") #',
           // template: '<#= kendo.toString( toDate(ActivationDate), "dd MM yyyy" ) #>' ,
           
           // template: '#=ActivationDate#',
            attributes: {
                style: 'white-space: nowrap;text-align: center;'
            }
        },
        {
            field: "Actions",
            title: "Action",
            filterable: false,
            sortable: false,
            width: "120px",
            template: '<div class="btn-group mb-sm">' +
                           '<button type="button" data-toggle="dropdown" class="btn btn-info dropdown-toggle">Select…</button>' +
                           '<button type="button" data-toggle="dropdown" class="btn btn-info dropdown-toggle">' +
                            '<span class="caret"></span></button>' +
                           '<ul role="menu" class="dropdown-menu" style="min-width:102px !important;">' +
                             '<li><a href="javascript:void(0);" onclick="ShowDeleteConfirmationPopup(\'#=UserID#\');">Delete</a>' +
							  '</li>' +
							  '#if(IsActive==true){#' + '<li><a href="javascript:void(0);" onclick="UpdateAtheleteStatus(\'#=UserID#\');">' + ' #}else{# ' +
                                '<li><li><a href="javascript:void(0);" onclick="ShowActivationConfirmationPopup(\'#=UserID#\');">' + '#}#' +
                                '#if(IsActive==true){# ' + 'Deactivate' + ' #}else{# ' + 'Activate' + '#}#' + '</a>' +
							  '</li>' +
							  '#if(Isfreeevaluation==true){# ' + '<li style="display:visible" >' + ' #}else{# ' + '<li style="display:none" >' + '#}#<a href="javascript:void(0);" onclick="ShowFreeEvaluation();">Free Evaluation</a>' +
							  '</li>' +
							  '</li>' +
                              '<li><a href="javascript:void(0);" onclick="OpenReassignmentDialog(#=UserID#);">Reassign</a></li>' +
                              '<li><a href="javascript:void(0);" onclick="ShowUserHistory(#=UserID#);">View History</a></li>' +
							  '<li><a href="javascript:void(0);" onclick="ShowAthleteProfile(#=UserID#);">View Profile</a></li>' +
							  '<li><a href="javascript:void(0);" onclick="ShowDashboard(\'#=AthleteName#\');">View Dashboard</a></li>' +
							'</ul>' +
						'</div>'
        }
    ],
    dataSource: athletedataSource
});
$("#athletePager").kendoPager({
    autoBind: true,
    dataSource: athletedataSource
});
$("#athleteGrid").kendoTooltip({
    filter: "td:nth-child(5)", //this filter selects the second column's cells
    position: "right",
    content: function (e) {
        var dataItem = $("#athleteGrid").data("kendoGrid").dataItem(e.target.closest("tr"));
        var content = dataItem.Phone;
        return content;
    }
}).data("kendoTooltip");

function FilterChange() {
    var filters = [];

    //sport filter
    if ($("#lstFilterOptions").val() != '')
        filters.push({ field: "Sport", operator: "eq", value: $("#lstFilterOptions").val() });

    //location filter
    if ($("#lstLocation").val() != '')
        filters.push({ field: "LocationId", operator: "eq", value: $("#lstLocation").val() });

    // status filter
    if ($("#lstAthleteStatus").val() != '')
        filters.push({ field: "PendingStatus", operator: "eq", value: $("#lstAthleteStatus").val() });

    athletedataSource.filter({
        logic: "and",
        filters: filters
    });

    $("#athleteGrid").data("kendoGrid").setDataSource(athletedataSource);
    $('#athleteGrid').data('kendoGrid').refresh();
}
function SearchFilter() {
    var searchText = $("#txtSearch").val();
    $("#athleteGrid").data("kendoGrid").dataSource.filter({
        logic: "or",
        filters: [
          { field: "AthleteName", operator: "contains", value: searchText },
          {
              logic: "or",
              filters: [
                  { field: "Phone", operator: "contains", value: searchText }
              ]
          },
          {
              logic: "or",
              filters: [
                  { field: "School", operator: "contains", value: searchText }
              ]
          },
          {
              logic: "or",
              filters: [
                  { field: "Sport", operator: "contains", value: searchText }
              ]
          },
          {
              logic: "or",
              filters: [
                  { field: "Session", operator: "contains", value: searchText }
              ]
          },
          {
              logic: "or",
              filters: [
                  { field: "Program", operator: "contains", value: searchText }
              ]
          }
        ]
    });
}

$(document).ready(function () {
    FilterChange();
    GetAllPrograms();
    BindMultiSelect();
});

function ShowDeleteConfirmationPopup(userId) {
    ConfirmationMessageBox({
        confirmationMessage: 'Are you sure you want to Delete Athlete?',
        popupTitle: 'Delete Athlete',
        callback: function () {
            deleteAtheletes(userId);
        }
    });
}
function deleteAtheletes(id) {
    $.ajax({
        url: "../Admin/DeleteAthleteById",
        type: "POST",
        async: false,
        data: { id: id },
        success: function (result) {
            if (result.success) {
                alert(result.message);
                athletedataSource.read();
            }
            else {
                alert(result.message);
            }

        }, error: function () {
            alert(result.message);
        }
    });
}

function ShowActivationConfirmationPopup(userId) {
    $("#hdActivationstatusUserID").val(userId);
    $("#hdUserCurrentActivationStatus").val(false);

    $("#txtAthleteActivationDate").mask('99/99/9999');
    $("#txtAthleteActivationDate").val(moment().format('MM/DD/YYYY'));
    $("#txtAthleteActivationDate").css('border-color', '#d4dae2');

    $('#confirmActivationModal').modal('show');
}
function UpdateAtheleteStatus(id) {
    ConfirmationMessageBox({
        confirmationMessage: 'Are you sure you want to deactivate Athlete?',
        popupTitle: 'Confirm Athlete deactivation',
        callback: function () {
            $.ajax({
                url: "../Admin/UpdateAthleteById",
                type: "POST",
                async: false,
                data: { id: id },
                success: function (result) {
                    if (result.success) {
                        alert(result.message);
                        athletedataSource.read();
                    }
                    else {
                        alert(result.message);
                    }
                }, error: function () {
                    alert(result.message);
                }
            });
        }
    });
}
function ShowRenewUser(userId) {
    $("#renewusers").data("id", userId);
    $("#athleteGridcontainer").hide();
    $("#renewusers").show();
    $("#freeevaluation").hide();
}
function ShowathleteGrid() {
    //GetSelectedValues();
    $("#athleteGridcontainer").show();
    $("#renewusers").hide();
    $("#freeevaluation").hide();
}
function SetStatusForSelectedAthletes(status) {
    var confirmationMessageDeactivation = 'Are you sure you want to deactivate selected athletes?';
    var popupTitleDeactivation = 'Confirm Athletes deactivation';
    var confirmationMessageAtivation = 'Are you sure you want to activate selected athletes?';
    var popupTitleActivation = 'Confirm Athletes activation';
    var confirmationMessage = '';
    var popupTitle = '';
    if (status) {
        confirmationMessage = confirmationMessageAtivation;
        popupTitle = popupTitleActivation;
    }
    else {
        confirmationMessage = confirmationMessageDeactivation;
        popupTitle = popupTitleDeactivation;
    }
    ConfirmationMessageBox({
        confirmationMessage: confirmationMessage,
        popupTitle: popupTitle,
        callback: function () {
            var ids = '';
            $('.athleteSelectCheckbox:checkbox:checked').each(function () {
                ids = ids + $(this).val() + ',';
            });
            $.ajax({
                url: "../Admin/SetAthleteListByIds",
                type: "POST",
                async: false,
                data: { status: status, ids: ids },
                success: function (result) {
                    if (result.success) {
                        alert(result.message);
                        athletedataSource.read();
                    }
                    else {
                        alert(result.message);
                    }
                    $("#dvActionBar").hide();
                }, error: function () {
                    alert(result.message);
                }
            });
        }
    });
}

function GetAllPrograms() {
    var programs = $("select[name$=SelectProgram]");
    $.getJSON("../Admin/ListAllPrograms", function (response) {
        programs.empty(); // remove any existing options
        $.each(response, function (index, item) {
            programs.append($('<option></option>').text(item.Text).val(item.ID));
        });
    });
}
function GetSelectedValues() {
    var id = $("#renewusers").data("id");
    var schedule = GetSelectedSchedule();
    var program = GetSelectedProgram();
    var startDate = GeSelectedStartDate();
    var paymentMode = GetSelectedPaymentMode();
    var workoutTiming = GetSeletectWorkoutTiming();
    var renewModel = {
        'id': id,
        'Schedule': schedule,
        'Program': program,
        'StartDate': startDate,
        'PaymentMode': paymentMode,
        'WorkoutTiming': workoutTiming,
    };
    $.ajax({
        url: "../Admin/UpdateRenewDetails",
        type: "POST",
        async: false,
        data: JSON.stringify({ "renewModel": renewModel }),
        dataType: "json",
        contentType: "application/json",
        success: function (result) {
            if (result.success) {
                alert(result.message);
            }
            else {
                alert(result.message);
            }

        }, error: function () {
            alert(result.message);
        }
    });
}
function GetSelectedProgram() {
    var selected = $("select[name$=SelectProgram]").val();
    return selected;
}

function GeSelectedStartDate() {
    var selected = $('.datetimepicker1 input').val();
    return selected;
}
function GetSelectedSchedule() {
    var selected = $("select[name$=SelectSchedule] option:selected").map(function (a, item) {
        return item.value;
    }).toArray().toString();
    return selected;
}
function GetSelectedPaymentMode() {
    var selected = $("select[name$=PaymentMode]").val();
    return selected;
}
function GetSeletectWorkoutTiming() {
    var selected = $("select[name$=WorkoutTiming]").val();
    return selected;
}

function BindMultiSelect() {

    var obj = $("select[name$=SelectSchedule]");
    obj.multiselect({
        delimiterText: "-",
        onChange: function (option) {
            if ($(option).parent().val().length > 3) {
                obj.multiselect("deselect", $(option).val());
            }
        }
    });

    var options = [
        { label: "Mon", title: "Monday", value: "1" },
        { label: "Tue", title: "Tuesday", value: "2" },
        { label: "Wed", title: "Wednesday", value: "3" },
        { label: "Thu", title: "Thurday", value: "4" },
        { label: "Fri", title: "Friday", value: "5" }
    ];
    obj.multiselect("dataprovider", options);
}
function FormatPhoneNumber(rawPhNum) {
    var formattedPhNum = rawPhNum.replace(/(\d\d\d)(\d\d\d)(\d\d\d\d)/, "($1)$2-$3");
    return formattedPhNum;
}
function SelectUser(userID, ctrl) {
    var isCheckBoxChecked = false;
    //if (ctrl.checked == true) {
    //    $("#hdSelectedUsers").val($("#hdSelectedUsers").val() + userID + ",");
    //}
    //else {
    //    $("#hdSelectedUsers").val($("#hdSelectedUsers").val().replace(userID + ",", ""));
    //}
    $("input:checked").each(function () {
        isCheckBoxChecked = true;
        return false;
    });
    if (isCheckBoxChecked == true) {
        $("#dvActionBar").show();
    }
    else {
        $("#dvActionBar").hide();
    }
}

function OpenReassignmentDialog(userID) {
    str = '';
    $('.athleteSelectCheckbox:checkbox:checked').each(function () {
        str = str + $(this).val() + ',';
    });
    if (userID) {
        $("#hdSelectedUsers").val(userID);
    }
    else {
        $("#hdSelectedUsers").val(str);
    }

    $("#trReassignTeam").hide();
    $("#trReassignCoach").hide();
    $("#trReassignProgram").hide();
    $('#lstReassignChoice').val(0);
    $('#lstCoachForReassignment').val(0);
    $('#lstTeamForReassignment').val(0);
    $('#lstProgramForReassignment').val(0);

    PopulatePrograms(userID);

    $('#reassignmentModal').modal('show');
}
function ReassignChoiceChange() {
    if ($("#lstReassignChoice").val() === "1") {
        $("#trReassignTeam").hide();
        $("#trReassignProgram").hide();
        $("#trReassignCoach").show();
    }
    else if ($("#lstReassignChoice").val() === "2") {
        $("#trReassignCoach").hide();
        $("#trReassignProgram").hide();
        $("#trReassignTeam").show();
    }
    else if ($("#lstReassignChoice").val() === "3") {
        $("#trReassignCoach").hide();
        $("#trReassignTeam").hide();
        $("#trReassignProgram").show();
    }
}
function ReassignChoiceToUser() {
    var reassignType = $("#lstReassignChoice").val();
    var objectID;

    switch (parseInt(reassignType)) {
        case 1:
            objectID = $("#lstCoachForReassignment").val();
            break;
        case 2:
            objectID = $("#lstTeamForReassignment").val();
            break;
        case 3:
            objectID = $("#lstProgramForReassignment").val();
            break;
    }

    loading_img(true);
    $.ajax({
        url: "../Admin/CompleteReassignment",
        type: "POST",
        contentType: 'application/json',
        data: JSON.stringify({ "reassignType": reassignType, "userIDs": $("#hdSelectedUsers").val(), "objectID": objectID }),
        dataType: "json",
        success: function (resp) {
            athletedataSource.read();
            $("#dvActionBar").hide();
            $("#hdSelectedUsers").val('');
            alert(resp.Message);
            $('#reassignmentModal').modal('hide');
            loading_img(false);
        }
    });
}

function PopulateCoaches() {
    $("#lstCoachForReassignment").empty();
    $.ajax({
        url: "../Admin/ListCoaches",
        type: "POST",
        contentType: 'application/json',
        dataType: "json",
        success: function (resp) {
            $("#lstCoachForReassignment").append("<option value='0'>Select Coach</option>");
            for (var i = 0; i < resp.length; i++) {
                $("#lstCoachForReassignment").append("<option value='" + resp[i].UserId + "'>" + resp[i].FirstName + " " + resp[i].LastName + "</option>");
            }
        }
    });
}
function PopulateTeams() {
    $("#lstTeamForReassignment").empty();
    $.ajax({
        url: "../Admin/ListTeams",
        type: "POST",
        contentType: 'application/json',
        dataType: "json",
        success: function (resp) {
            $("#lstTeamForReassignment").append("<option value='0'>Select Team</option>");
            for (var i = 0; i < resp.length; i++) {
                $("#lstTeamForReassignment").append("<option value='" + resp[i].ID + "'>" + resp[i].Name + "</option>");
            }
        }
    });
}
function PopulatePrograms(userID) {
    $("#lstProgramForReassignment").empty();
    var data = { userId: 0 };
    if (userID) {
        data.userId = userID;
    }
    $.ajax({
        url: "../Coach/GetActiveProgramList",
        type: "POST",
        data: data,
        //contentType: 'application/json',
        //dataType: "json",
        success: function (resp) {
            $("#lstProgramForReassignment").append("<option value='0'>Select Program</option>");
            for (var i = 0; i < resp.length; i++) {
                $("#lstProgramForReassignment").append("<option value='" + resp[i].Id + "'>" + resp[i].Title + "</option>");
            }
        }
    });
}

function ShowDeleteConfirmation() {
    $('#deleteModal').modal('show');
}
function DeleteConfirmation() {
    $.ajax({
        url: "../Admin/DeleteMultipleAthlete",
        type: "POST",
        contentType: 'application/json',
        data: JSON.stringify({ "IDs": $("#hdSelectedUsers").val() }),
        dataType: "json",
        success: function (resp) {
            athletedataSource.read();
            $("#hdSelectedUsers").val('');
            alert(resp.Message);
            $('#deleteModal').modal('hide');
        }
    });
}
function ShowUserHistory(userID) {
    window.location.href = "../Admin/UserHistory?userID=" + userID + "&filter=team";
}
