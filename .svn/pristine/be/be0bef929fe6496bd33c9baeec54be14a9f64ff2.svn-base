function InitDataVisibility() {
    var userId = GetUserId();
    InitIsUserAthleticDirector();
    GetUserDataVisibility();
    InitSaveButton();
}
function GetUserId() {
    var userId = $('#hdUserID').val();
    return userId;
}


function GetCheckboxForLine(dataVisibility) {
    var rowClass = "";
    var id = dataVisibility.SportName + "-";

    if (dataVisibility.TeamId == 0) {
        id += "All";
        rowClass = "userDataVisibilitySportRow";
    }
    else
        id += dataVisibility.TeamName;

    var checked = "checked";
    if (dataVisibility.Selected == false)
        checked = "";

    var input = "<input id='" + id + "' type='checkbox' " + checked + " class='dataVisibilityCheckbox " + rowClass + "'/>";
    return input;
}
function AddRowToTable(tableId, dataVisibility) {
    $("#" + tableId).find('tbody')
    .append($('<tr>')
        .append($('<td>').append(GetCheckboxForLine(dataVisibility)))
        .append($('<td>').append(dataVisibility.SportName))
        .append($('<td>').append(dataVisibility.TeamName))
    );
}
function GetUserDataVisibility() {
    loading_img(true);
    var userId = GetUserId();
    $.ajax({
        url: "../UserDataVisibility/GetUserDataVisibilityListByUserId",
        type: "POST",
        data: JSON.stringify({ 'userId': userId }),
        contentType: 'application/json',
        dataType: "json",
        success: function (result) {
            window.UserDataVisibilityList = result;
            $("#userDataVisibilityTable tbody").empty();
            for (var i = 0; i < result.length; i++) {
                var item = result[i];
                AddRowToTable("userDataVisibilityTable", item);
            }
            InitDataVisibilityCheckboxs();
            InitSelectAllUserDataVisibility();
            loading_img(false);
        },
        error: function (req, status, errorObj) {
            console.log("GetUserDataVisibility error!");
            loading_img(false);

        }
    });

}
function InitDataVisibilityCheckboxs() {
    $(".dataVisibilityCheckbox").change(function () {
        var checkbox = $(this)[0]
        var checked = false;
        if ($(this).is(':checked'))
            checked = true;

        var id = checkbox.id;
        var sportName = id.split("-")[0];
        var teamName = id.split("-")[1];
        if (teamName == "All") {
            var searchString = "input[id^='" + sportName + "']";
            var allSportCheckboxs = $(searchString);
            for (var i = 0; i < allSportCheckboxs.length; i++) {
                var sportCheckbox = allSportCheckboxs[i];
                if (sportCheckbox.id == id)
                    continue;
                if (checked) {
                    sportCheckbox.checked = true;
                    $(sportCheckbox).addClass("userDataVisibilityDisabled");
                }
                else
                    $(sportCheckbox).removeClass("userDataVisibilityDisabled");;
            }
        }

        var responsibility = "Sport";
        if (teamName != "All") {
            responsibility = "Team";
        }
    });
}
function InitSelectAllUserDataVisibility() {
    $("#selectAllUserDataVisibility").click(function () {
        var allCheckboxs = $(".dataVisibilityCheckbox");
        for (var i = 0; i < allCheckboxs.length; i++) {
            var checkbox = allCheckboxs[i];
            checkbox.checked = true;
        }
    });
    $("#selectNoneUserDataVisibility").click(function () {
        var allCheckboxs = $(".dataVisibilityCheckbox");
        for (var i = 0; i < allCheckboxs.length; i++) {
            var checkbox = allCheckboxs[i];
            checkbox.checked = false;
        }
    });
}


function GetIsUserAthleticDirector(userId) {
    $.ajax({
        url: "../UserDataVisibility/IsUserAthleticDirector",
        type: "POST",
        data: JSON.stringify({ 'userId': userId }),
        contentType: 'application/json',
        dataType: "json",
        success: function (result) {
            $("#athleticDirectorCheckbox").prop("checked", result);
            InitUserDataVisibilityChechboxsByIsAthleticDirector(result);
        },
        error: function (req, status, errorObj) {
            console.log("IsUserAthleticDirector error!");
        }
    });
}
function InitUserDataVisibilityChechboxsByIsAthleticDirector(value) {
    var allCheckboxs = $(".dataVisibilityCheckbox");
    if (value) {
        for (var i = 0; i < allCheckboxs.length; i++) {
            var checkbox = allCheckboxs[i];
            checkbox.checked = true;
            $(checkbox).addClass("userDataVisibilityDisabled");
        }
        $("#selectAllUserDataVisibility").addClass("userDataVisibilityDisabled");
        $("#selectNoneUserDataVisibility").addClass("userDataVisibilityDisabled");
    } else {
        for (var i = 0; i < allCheckboxs.length; i++) {
            var checkbox = allCheckboxs[i];
            $(checkbox).removeClass("userDataVisibilityDisabled");
        }
        $("#selectAllUserDataVisibility").removeClass("userDataVisibilityDisabled");
        $("#selectNoneUserDataVisibility").removeClass("userDataVisibilityDisabled");
    }
}
function SetIsUserAthleticDirector(userId, value) {
    loading_img(true);
    $.ajax({
        url: "../UserDataVisibility/SetUserAthleticDirector",
        type: "POST",
        data: JSON.stringify({ 'userId': userId, 'value': value }),
        contentType: 'application/json',
        dataType: "json",
        success: function (result) {
            console.log("SetUserAthleticDirector success!");
            loading_img(false);
        },
        error: function (req, status, errorObj) {
            console.log("IsUserAthleticDirector error!");
            loading_img(false);
        }
    });
}
function InitIsUserAthleticDirector() {
    var userId = GetUserId();
    GetIsUserAthleticDirector(userId);
    $("#athleticDirectorCheckbox").change(function (e) {
        var value = $("#athleticDirectorCheckbox").prop("checked");
        var userId = GetUserId();
        InitUserDataVisibilityChechboxsByIsAthleticDirector(value)
    });
    $("#btnSaveIsAthleticDirector").click(function () {
        SaveIsUserAthleticDirector();
        SaveUserDataVisibility();
    })
}
function SaveIsUserAthleticDirector() {
    var value = $("#athleticDirectorCheckbox").prop("checked");
    SetIsUserAthleticDirector(userId, value);
}


function DeleteAllBeforeSave() {
    $.ajax({
        url: "../UserDataVisibility/DeleteAllBeforeSave",
        type: "POST",
        data: JSON.stringify({
            'UserId': GetUserId()
        }),
        contentType: 'application/json',
        dataType: "json",
        success: function (result) {
            console.log("DeleteAllBeforeSave success!");
        },
        error: function (req, status, errorObj) {
            console.log("DeleteAllBeforeSave error!");
        }
    });
}
function SaveDataVisibilitySimple(sportName, teamName) {
    var responsibility = "Team";
    if (teamName.indexOf("All") != -1) {
        responsibility = "Sport";
    }
    SaveDataVisibilityByNames(responsibility, sportName, teamName);
}
function SaveDataVisibilityByNames(responsibility, sportName, teamName) {
    loading_img(true);
    $.ajax({
        url: "../UserDataVisibility/AddUserDataVisibility",
        type: "POST",
        data: JSON.stringify({
            'UserId': GetUserId(),
            'ResponsibilityType': responsibility,
            'SportName': sportName,
            'TeamName': teamName,
        }),
        contentType: 'application/json',
        dataType: "json",
        success: function (result) {
            console.log("SaveDataVisibility success!");
            loading_img(false);
        },
        error: function (req, status, errorObj) {
            console.log("SaveDataVisibility error!");
            loading_img(false);
        }
    });
}
function SaveDataVisibility(responsibility, sportId, teamId) {
    $.ajax({
        url: "../UserDataVisibility/AddUserDataVisibility",
        type: "POST",
        data: JSON.stringify({
            'UserId': GetUserId(),
            'ResponsibilityType': responsibility,
            'SportId': sportId,
            'TeamId': teamId,
        }),
        contentType: 'application/json',
        dataType: "json",
        success: function (result) {
            GetUserDataVisibility();
            console.log("SaveDataVisibility success!");
        },
        error: function (req, status, errorObj) {
            console.log("SaveDataVisibility error!");
        }
    });
}
function InitSaveButton() {
    $("#btnSaveUserDataVisibility").click(function () {
        SaveIsUserAthleticDirector();
        SaveUserDataVisibility();
    });
}
function SaveUserDataVisibility() {
    DeleteAllBeforeSave();
    var allCheckboxs = $(".dataVisibilityCheckbox");
    for (var i = 0; i < allCheckboxs.length; i++) {
        var checkbox = allCheckboxs[i];
        if (checkbox.checked) {
            var tr = $(checkbox).closest("tr");
            var tds = $(tr).find('td');
            var sportName = tds[1].innerText;
            var teamName = tds[2].innerText;
            SaveDataVisibilitySimple(sportName, teamName);
        }
    }
}
